using FrozenExpress.Game.Common.GameCycle;
using FrozenExpress.Game.Core.DungeonLayout;
using UnityEngine;

namespace FrozenExpress.Game.Core.DungeonLayoutEditor
{
    public class DungeonLayoutEditorModelDrawingController : IInitializable, ITerminatable, IFrameUpdatable
    {
        public EInitializationOrder InitializationOrder => EInitializationOrder.Common;

        public EFrameUpdateOrder UpdateOrder => EFrameUpdateOrder.Late;

        private readonly IDungeonLayoutEditorModelView _view;
        private readonly IDungeonLayoutMeshBuilder _dungeonBuilder;
        private readonly IDungeonLayoutEditorModel _dungeonModel;
        
        private Mesh _dungeonMesh;
        private bool _isViewDirty;
        private EditorDungeonLayoutData _layoutData;

        public DungeonLayoutEditorModelDrawingController(IDungeonLayoutEditorModel dungeonModel,
                                                         IDungeonLayoutEditorModelView view,
                                                         IDungeonLayoutMeshBuilder dungeonBuilder)
        {
            _dungeonModel = dungeonModel;
            _view = view;
            _dungeonBuilder = dungeonBuilder;

            _dungeonModel.OnLayoutChanged += SetViewDirty;
        }

        public void Initialize()
        {
            _dungeonMesh = new Mesh();
            _layoutData = new EditorDungeonLayoutData();

            _view.SetMesh(_dungeonMesh);
        }

        public void Update(FrameUpdateData frameData)
        {
            if (!_isViewDirty)
            {
                return;
            }

            _isViewDirty = false;

            _layoutData.Initialize(_dungeonModel);

            _dungeonBuilder.BuildMesh(_layoutData, _dungeonMesh);
        }

        private void SetViewDirty()
        {
            _isViewDirty = true;
        }

        public void Terminate()
        {
            _dungeonModel.OnLayoutChanged -= SetViewDirty;
        }
    }
}
