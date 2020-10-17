using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayout
{
    public class DungeonLayoutPatternMaster : IDungeonLayoutPatternMaster
    {
        private readonly IReadOnlyList<DungeonLayoutPatternData> _patterns;
        private readonly Dictionary<EDungeonLayoutPatternRotation, Func<Vector3Int, Vector3Int>> _rotationFuncs;
        private readonly Dictionary<EDungeonLayoutPatternRotation, Quaternion> _rotationQuaternions;
        private readonly List<EDungeonLayoutPatternRotation> _allRotation;

        private readonly List<EDungeonLayoutPatternRotation> _rotationFlagBuffer;
        private readonly List<DungeonLayoutPatternData> _patternBuffer;

        public DungeonLayoutPatternMaster(IReadOnlyList<DungeonLayoutPatternData> patterns)
        {
            _patterns = patterns;
            _rotationFlagBuffer = new List<EDungeonLayoutPatternRotation>();
            _patternBuffer = new List<DungeonLayoutPatternData>();
            
            _rotationFuncs = new Dictionary<EDungeonLayoutPatternRotation, Func<Vector3Int, Vector3Int>>()
            {
                [EDungeonLayoutPatternRotation.Identity] = d => d,
                [EDungeonLayoutPatternRotation.Up90] = d => new Vector3Int(d.z, d.y, -d.x),
                [EDungeonLayoutPatternRotation.Up180] = d => new Vector3Int(-d.x, d.y, -d.z),
                [EDungeonLayoutPatternRotation.Up270] = d => new Vector3Int(-d.z, d.y, d.x),
            };

            _rotationQuaternions = new Dictionary<EDungeonLayoutPatternRotation, Quaternion>()
            {
                [EDungeonLayoutPatternRotation.Identity] = Quaternion.identity,
                [EDungeonLayoutPatternRotation.Up90] = Quaternion.AngleAxis(90, Vector3.up),
                [EDungeonLayoutPatternRotation.Up180] = Quaternion.AngleAxis(180, Vector3.up),
                [EDungeonLayoutPatternRotation.Up270] = Quaternion.AngleAxis(270, Vector3.up)
            };

            _allRotation = (Enum.GetValues(typeof(EDungeonLayoutPatternRotation)) as EDungeonLayoutPatternRotation[])
                .Where(r => r != EDungeonLayoutPatternRotation.None).ToList();

            foreach (var rot in _allRotation.Except(_rotationFuncs.Keys))
            {
                Debug.LogError($"{this} :: Missing rotation function for '{rot}'");
            }

            foreach (var rot in _allRotation.Except(_rotationQuaternions.Keys))
            {
                Debug.LogError($"{this} :: Missing quaternion for '{rot}'");
            }
        }

        public IEnumerable<DungeonLayoutEntityMesh> IterateEntities(IDungeonLayoutMap map)
        {
            _patternBuffer.Clear();
            _patternBuffer.AddRange(_patterns.Where(p => p.Trigger == EDungeonLayoutPatternTrigger.Cell));
            foreach (var cell in map.Cells)
            {
                foreach (var pattern in _patternBuffer)
                {
                    ExtractRotations(pattern.Rotation, _rotationFlagBuffer);
                    foreach (var rotationFlag in _rotationFlagBuffer)
                    {
                        if (!_rotationFuncs.TryGetValue(rotationFlag, out var rotationFunc))
                        {
                            continue;
                        }

                        if (!CheckPatternConditionsMet(map, pattern, cell, rotationFunc))
                        {
                            continue;
                        }

                        if (!_rotationQuaternions.TryGetValue(rotationFlag, out var rotationQuaternion))
                        {
                            continue;
                        }

                        yield return new DungeonLayoutEntityMesh()
                        {
                            Mesh = pattern.Mesh,
                            Position = cell,
                            Rotation = rotationQuaternion
                        };
                    }
                }
            }
        }

        private bool CheckPatternConditionsMet(IDungeonLayoutMap map, DungeonLayoutPatternData pattern, Vector3Int origin, Func<Vector3Int, Vector3Int> rotationFunc)
        {
            if (pattern.CellExistConditions.Count > 0)
            {
                foreach (var condition in pattern.CellExistConditions)
                {
                    var rotatedOffset = rotationFunc(condition.Offset);
                    var cellExists = map.CheckCellExists(origin + rotatedOffset);
                    if (cellExists != condition.MustExist)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ExtractRotations(EDungeonLayoutPatternRotation rotation, List<EDungeonLayoutPatternRotation> result)
        {
            result.Clear();
            for (var i = 0; i < _allRotation.Count; i++)
            {
                var flag = _allRotation[i];
                if (rotation.HasFlag(_allRotation[i]))
                    result.Add(flag);
            }
        }
    }
}
