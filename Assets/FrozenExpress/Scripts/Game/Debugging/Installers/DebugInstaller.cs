using FrozenExpress.Game.Debugging;
using UnityEngine;

namespace FrozenExpress.Game.Debugging.Installers
{
    public class DebugInstaller : Zenject.ScriptableObjectInstaller
    {
        [SerializeField] private DebugDrawingConfig _drawingConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<ADebugFactory>().To<UnityDebugFactory>().AsSingle();
            
            Container.BindInterfacesTo<DebugDrawingController>().AsSingle().WithArguments(_drawingConfig);
        }
    }
}
