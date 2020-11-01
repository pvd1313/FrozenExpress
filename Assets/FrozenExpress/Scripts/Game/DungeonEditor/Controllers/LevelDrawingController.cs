using System.Collections.Generic;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Dungeon;
using FrozenExpress.Game.DungeonCollision;
using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.DungeonEditor
{
    public class LevelDrawingController : IInitializable, ITerminatable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly IDungeonMeshBuilder _meshBuilder;
        private readonly LevelDrawingConfig _config;
        private readonly IDungeonTransform _dungeonTransform;
        private readonly IDungeonPatternController _patternController;
        private readonly IDungeonCollisionController _dungeonCollisionController;
        private readonly ILevelEditorModel _dungeonModel;
        private readonly ADebugDrawer _navigationDrawer;
        
        private readonly List<EntityPatternTransform> _entitiesBuffer;
        
        private Mesh _mesh;
        private bool _isViewDirty;

        public LevelDrawingController(ILevelEditorModel dungeonModel,
                                      IDungeonMeshBuilder dungeonBuilder,
                                      LevelDrawingConfig config,
                                      ADebugFactory debugFactory,
                                      IDungeonTransform dungeonTransform,
                                      IDungeonPatternController patternController,
                                      IDungeonCollisionController dungeonCollisionController)
        {
            _dungeonModel = dungeonModel;
            _meshBuilder = dungeonBuilder;
            _config = config;
            _dungeonTransform = dungeonTransform;
            _patternController = patternController;
            _dungeonCollisionController = dungeonCollisionController;
            _entitiesBuffer = new List<EntityPatternTransform>();
            
            _navigationDrawer = debugFactory.CreateDrawer(this);
            _navigationDrawer?.SetColor(EDebugColor.Green);
            _navigationDrawer?.SetShape(EDebugShape.Sphere3D);
            _navigationDrawer?.SetScale(Vector3.one * 0.5f);

            _dungeonModel.OnLayoutChanged += SetMeshDirty;
        }

        public void Initialize()
        {
            _mesh = new Mesh();
        }

        public void Update(FrameUpdateData frameData)
        {
            foreach (var kvp in _dungeonModel.Layout)
            {
                var cell = kvp.Key;
                var layer = kvp.Value;
                if ((layer & 2) > 0)
                {
                    var position = _dungeonTransform.TransformPosition(cell);
                    _navigationDrawer?.Draw(position);
                }
            }
            
            if (_isViewDirty)
            {
                _isViewDirty = false;
                
                _patternController.ProcessMap(_dungeonModel.Layout, _entitiesBuffer);
                _meshBuilder.BuildMesh(_entitiesBuffer, _mesh);
                _dungeonCollisionController.InitializeColliders(_entitiesBuffer);
            }

            var material = _config.DrawDiffuse ? _config.DiffuseMaterial : _config.DebugMaterial;
            Graphics.DrawMesh(_mesh, Matrix4x4.identity, material, 1);
        }

        private void SetMeshDirty()
        {
            _isViewDirty = true;
        }

        public void Terminate()
        {
            _mesh.Clear();
            _dungeonModel.OnLayoutChanged -= SetMeshDirty;
        }
    }
}
