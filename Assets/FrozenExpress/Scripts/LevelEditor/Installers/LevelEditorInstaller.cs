using OneLine;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FrozenExpress.LevelEditor
{
    public class LevelEditorInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DungeonLayoutEditorCameraView _dungeonLayoutEditorCameraViewPrefab;
        [SerializeField] private LevelEditorCameraConfig _dungenLayoutEditorCameraControllerConfig;
        [SerializeField] private LevelDrawingConfig _drawingConfig;

        [SerializeField] [OneLine] private List<MeshVertexColor> _meshVertexColors;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DungeonLayoutEditorCameraView>().FromComponentInNewPrefab(_dungeonLayoutEditorCameraViewPrefab).AsSingle();

            Container.BindInterfacesTo<LevelEditorMeshColorProvider>().AsSingle().WithArguments(_meshVertexColors);
            Container.BindInterfacesTo<LevelEditorCameraController>().AsSingle().WithArguments(_dungenLayoutEditorCameraControllerConfig);
            Container.BindInterfacesTo<LevelEditorModel>().AsSingle();
            Container.BindInterfacesTo<LevelDrawingController>().AsSingle().WithArguments(_drawingConfig);
        }
    }
}
