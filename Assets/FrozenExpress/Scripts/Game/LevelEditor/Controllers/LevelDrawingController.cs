using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Debugging;
using FrozenExpress.Game.Dungeon;
using FrozenExpress.Game.GameCycle;
using UnityEngine;

namespace FrozenExpress.Game.LevelEditor
{
    public class LevelDrawingController : IInitializable, ITerminatable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly IDungeonMeshBuilder _meshBuilder;
        private readonly LevelDrawingConfig _config;
        private readonly IDungeonTransform _dungeonTransform;
        private readonly ILevelEditorModel _dungeonModel;
        private readonly ADebugDrawer _navigationDrawer;
        
        private Mesh _mesh;
        private bool _isViewDirty;

        public LevelDrawingController(ILevelEditorModel dungeonModel,
                                      IDungeonMeshBuilder dungeonBuilder,
                                      LevelDrawingConfig config,
                                      ADebugFactory debugFactory,
                                      IDungeonTransform dungeonTransform)
        {
            _dungeonModel = dungeonModel;
            _meshBuilder = dungeonBuilder;
            _config = config;
            _dungeonTransform = dungeonTransform;
            
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
                _meshBuilder.BuildMesh(_dungeonModel.Layout, _mesh);
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
            _dungeonModel.OnLayoutChanged -= SetMeshDirty;
        }
    }
}
