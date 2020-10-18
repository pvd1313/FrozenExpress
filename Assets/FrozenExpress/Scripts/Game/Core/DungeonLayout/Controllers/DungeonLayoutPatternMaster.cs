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
        private readonly List<EDungeonLayoutPatternRotation> _knownRotations;
        private readonly Dictionary<EDungeonLayoutPatternFlip, Vector3> _flipScales;
        private readonly Dictionary<EDungeonLayoutPatternFlip, Func<Vector3Int, Vector3Int>> _flipFuncs;
        private readonly List<EDungeonLayoutPatternFlip> _knownFlips;
        private readonly List<EDungeonLayoutPatternFlip> _flipFlagBuffer;
        private readonly List<EDungeonLayoutPatternRotation> _rotationFlagBuffer;
        private readonly List<DungeonLayoutPatternData> _patternBuffer;

        public DungeonLayoutPatternMaster(IReadOnlyList<DungeonLayoutPatternData> patterns)
        {
            _patterns = patterns;
            _flipFlagBuffer = new List<EDungeonLayoutPatternFlip>();
            _rotationFlagBuffer = new List<EDungeonLayoutPatternRotation>();
            _patternBuffer = new List<DungeonLayoutPatternData>();

            _rotationFuncs = new Dictionary<EDungeonLayoutPatternRotation, Func<Vector3Int, Vector3Int>>()
            {
                [EDungeonLayoutPatternRotation.Identity] = d => d,
                [EDungeonLayoutPatternRotation.YAxis90] = d => new Vector3Int(d.z, d.y, -d.x),
                [EDungeonLayoutPatternRotation.YAxis180] = d => new Vector3Int(-d.x, d.y, -d.z),
                [EDungeonLayoutPatternRotation.YAxis270] = d => new Vector3Int(-d.z, d.y, d.x)
            };

            _rotationQuaternions = new Dictionary<EDungeonLayoutPatternRotation, Quaternion>()
            {
                [EDungeonLayoutPatternRotation.Identity] = Quaternion.identity,
                [EDungeonLayoutPatternRotation.YAxis90] = Quaternion.AngleAxis(90, Vector3.up),
                [EDungeonLayoutPatternRotation.YAxis180] = Quaternion.AngleAxis(180, Vector3.up),
                [EDungeonLayoutPatternRotation.YAxis270] = Quaternion.AngleAxis(270, Vector3.up)
            };

            _flipFuncs = new Dictionary<EDungeonLayoutPatternFlip, Func<Vector3Int, Vector3Int>>()
            {
                [EDungeonLayoutPatternFlip.Identity] = s => s,
                [EDungeonLayoutPatternFlip.FlipX] = s => new Vector3Int(-s.x, s.y, s.z),
                [EDungeonLayoutPatternFlip.FlipY] = s => new Vector3Int(s.x, -s.y, s.z),
                [EDungeonLayoutPatternFlip.FlipZ] = s => new Vector3Int(s.x, s.y, -s.z)
            };

            _flipScales = new Dictionary<EDungeonLayoutPatternFlip, Vector3>()
            {
                [EDungeonLayoutPatternFlip.Identity] = Vector3.one,
                [EDungeonLayoutPatternFlip.FlipX] = new Vector3(-1, 1, 1),
                [EDungeonLayoutPatternFlip.FlipY] = new Vector3(1, -1, 1),
                [EDungeonLayoutPatternFlip.FlipZ] = new Vector3(1, 1, -1)
            };

            _knownRotations = (Enum.GetValues(typeof(EDungeonLayoutPatternRotation)) as EDungeonLayoutPatternRotation[])
                .Where(r => r != EDungeonLayoutPatternRotation.None).ToList();

            foreach (var rot in _knownRotations.Except(_rotationFuncs.Keys))
            {
                Debug.LogError($"{this} :: Missing rotation function for '{rot}'");
            }

            foreach (var rot in _knownRotations.Except(_rotationQuaternions.Keys))
            {
                Debug.LogError($"{this} :: Missing quaternion for '{rot}'");
            }

            _knownFlips = (Enum.GetValues(typeof(EDungeonLayoutPatternFlip)) as EDungeonLayoutPatternFlip[])
                .Where(r => r != EDungeonLayoutPatternFlip.None).ToList();

            foreach (var flip in _knownFlips.Except(_flipScales.Keys))
            {
                Debug.LogError($"{this} :: Missing flip scale for '{flip}");
            }

            foreach (var flip in _knownFlips.Except(_flipFuncs.Keys))
            {
                Debug.LogError($"{this} :: Missing flip function for '{flip}");
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
                    ExtractFlips(pattern.Flip, _knownFlips, _flipFlagBuffer);
                    ExtractRotations(pattern.Rotation, _knownRotations, _rotationFlagBuffer);
                    foreach (var flipFlag in _flipFlagBuffer)
                    {
                        if (!_flipFuncs.TryGetValue(flipFlag, out var flipFunc))
                        {
                            continue;
                        }

                        foreach (var rotationFlag in _rotationFlagBuffer)
                        {
                            if (!_rotationFuncs.TryGetValue(rotationFlag, out var rotationFunc))
                            {
                                continue;
                            }

                            if (!CheckPatternConditionsMet(map, pattern, cell, flipFunc, rotationFunc))
                            {
                                continue;
                            }

                            if (!_rotationQuaternions.TryGetValue(rotationFlag, out var rotationQuaternion))
                            {
                                continue;
                            }

                            if (!_flipScales.TryGetValue(flipFlag, out var flipScale))
                            {
                                continue;
                            }

                            yield return new DungeonLayoutEntityMesh()
                            {
                                Mesh = pattern.Mesh,
                                Position = cell,
                                Scale = flipScale,
                                Rotation = rotationQuaternion,
                            };
                        }
                    }
                }
            }
        }

        private static bool CheckPatternConditionsMet(IDungeonLayoutMap map,
                                                      DungeonLayoutPatternData pattern,
                                                      Vector3Int origin,
                                                      Func<Vector3Int, Vector3Int> flipFunc,
                                                      Func<Vector3Int, Vector3Int> rotationFunc)
        {
            if (pattern.CellExistConditions.Count > 0)
            {
                foreach (var condition in pattern.CellExistConditions)
                {
                    var flipedOffset = flipFunc(condition.Offset);
                    var rotatedOffset = rotationFunc(flipedOffset);
                    var cellExists = map.CheckCellExists(origin + rotatedOffset);
                    if (cellExists != condition.MustExist)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void ExtractRotations(EDungeonLayoutPatternRotation rotation,
                                             List<EDungeonLayoutPatternRotation> knownRotations,
                                             List<EDungeonLayoutPatternRotation> result)
        {
            result.Clear();
            for (var i = 0; i < knownRotations.Count; i++)
            {
                var flag = knownRotations[i];
                if (rotation.HasFlag(knownRotations[i]))
                    result.Add(flag);
            }
        }

        private static void ExtractFlips(EDungeonLayoutPatternFlip flip,
                                         List<EDungeonLayoutPatternFlip> knownFlips,
                                         List<EDungeonLayoutPatternFlip> result)
        {
            result.Clear();
            for (var i = 0; i < knownFlips.Count; i++)
            {
                var flag = knownFlips[i];
                if (flip.HasFlag(flag))
                    result.Add(flag);
            }
        }
    }
}
