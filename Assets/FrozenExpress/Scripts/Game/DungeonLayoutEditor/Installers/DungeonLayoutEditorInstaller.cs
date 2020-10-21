using UnityEngine;
using Zenject;

namespace FrozenExpress.Game.Core.DungeonLayoutEditor
{
    public class DungeonLayoutEditorInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DungeonLayoutEditorCameraView _dungeonLayoutEditorCameraViewPrefab;
        [SerializeField] private DungeonLayoutEditorCameraControllerConfig _dungenLayoutEditorCameraControllerConfig;
        [SerializeField] private DungeonLayoutEditorModelView _dungeonLayoutEditorModelViewPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DungeonLayoutEditorCameraView>().FromComponentInNewPrefab(_dungeonLayoutEditorCameraViewPrefab).AsSingle();
            Container.BindInterfacesTo<DungeonLayoutEditorModelView>().FromComponentInNewPrefab(_dungeonLayoutEditorModelViewPrefab).AsSingle();

            Container.BindInterfacesTo<DungeonLayoutEditorCameraController>().AsSingle().WithArguments(_dungenLayoutEditorCameraControllerConfig);
            Container.BindInterfacesTo<DungeonLayoutEditorModel>().AsSingle();
            Container.BindInterfacesTo<DungeonLayoutEditorModelDrawingController>().AsSingle();
        }
    }
}
