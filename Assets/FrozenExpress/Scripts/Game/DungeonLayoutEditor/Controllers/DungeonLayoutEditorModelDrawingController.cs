using FrozenExpress.Game.GameCycle;
using FrozenExpress.Game.DungeonLayout;
using UnityEngine;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public class DungeonLayoutEditorModelDrawingController : IInitializable, ITerminatable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly IDungeonMeshBuilder _meshBuilder;
        private readonly DungeonEditorDrawingConfig _config;
        private readonly IDungeonLayoutEditorModel _dungeonModel;
        
        private Mesh _mesh;
        private bool _isViewDirty;
        private EditorDungeonLayoutData _layout;

        public DungeonLayoutEditorModelDrawingController(IDungeonLayoutEditorModel dungeonModel,
                                                         IDungeonMeshBuilder dungeonBuilder,
                                                         DungeonEditorDrawingConfig config)
        {
            _dungeonModel = dungeonModel;
            _meshBuilder = dungeonBuilder;
            _config = config;

            _dungeonModel.OnLayoutChanged += SetMeshDirty;
        }

        public void Initialize()
        {
            _mesh = new Mesh();
            _layout = new EditorDungeonLayoutData();
        }

        public void Update(FrameUpdateData frameData)
        {
            if (_isViewDirty)
            {
                _isViewDirty = false;
                _layout.Initialize(_dungeonModel);
                _meshBuilder.BuildMesh(_layout, _mesh);
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
