using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Debugging.Controllers;
using FrozenExpress.Scripts.Game.Debugging.Data;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Installers
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
