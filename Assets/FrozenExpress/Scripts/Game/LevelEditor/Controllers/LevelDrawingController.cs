using FrozenExpress.Scripts.Game.Dungeon.Interfaces;
using FrozenExpress.Scripts.Game.GameCycle.Data;
using FrozenExpress.Scripts.Game.GameCycle.Enums;
using FrozenExpress.Scripts.Game.GameCycle.Interfaces;
using FrozenExpress.Scripts.Game.LevelEditor.Data;
using FrozenExpress.Scripts.Game.LevelEditor.Interfaces;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.LevelEditor.Controllers
{
    public class LevelDrawingController : IInitializable, ITerminatable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly IDungeonMeshBuilder _meshBuilder;
        private readonly LevelDrawingConfig _config;
        private readonly ILevelEditorModel _dungeonModel;
        
        private Mesh _mesh;
        private bool _isViewDirty;

        public LevelDrawingController(ILevelEditorModel dungeonModel,
                                      IDungeonMeshBuilder dungeonBuilder,
                                      LevelDrawingConfig config)
        {
            _dungeonModel = dungeonModel;
            _meshBuilder = dungeonBuilder;
            _config = config;

            _dungeonModel.OnLayoutChanged += SetMeshDirty;
        }

        public void Initialize()
        {
            _mesh = new Mesh();
        }

        public void Update(FrameUpdateData frameData)
        {
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
