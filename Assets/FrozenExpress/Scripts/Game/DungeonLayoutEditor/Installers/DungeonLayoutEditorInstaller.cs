using OneLine;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.DungeonLayoutEditor
{
    public class DungeonLayoutEditorInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DungeonLayoutEditorCameraView _dungeonLayoutEditorCameraViewPrefab;
        [SerializeField] private DungeonLayoutEditorCameraControllerConfig _dungenLayoutEditorCameraControllerConfig;
        [SerializeField] private DungeonEditorDrawingConfig _drawingConfig;

        [SerializeField] [OneLine] private List<DungeonMeshVertexColor> _meshVertexColors;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DungeonLayoutEditorCameraView>().FromComponentInNewPrefab(_dungeonLayoutEditorCameraViewPrefab).AsSingle();

            Container.BindInterfacesTo<EditorDungeonMeshVertexColorProvider>().AsSingle().WithArguments(_meshVertexColors);
            Container.BindInterfacesTo<DungeonLayoutEditorCameraController>().AsSingle().WithArguments(_dungenLayoutEditorCameraControllerConfig);
            Container.BindInterfacesTo<DungeonLayoutEditorModel>().AsSingle();
            Container.BindInterfacesTo<DungeonLayoutEditorModelDrawingController>().AsSingle().WithArguments(_drawingConfig);
        }
    }
}
