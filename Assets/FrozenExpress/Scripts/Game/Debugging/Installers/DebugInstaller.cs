using FrozenExpress.Scripts.Game.Debugging.Abstractions;
using FrozenExpress.Scripts.Game.Debugging.Controllers;
using FrozenExpress.Scripts.Game.Debugging.Views;
using UnityEngine;

namespace FrozenExpress.Scripts.Game.Debugging.Installers
{
    public class DebugInstaller : Zenject.ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GizmosDrawingView>().FromNewComponentOnRoot().AsSingle();
            
            Container.Bind<ADebugFactory>().To<UnityDebugFactory>().AsSingle();
            Container.BindInterfacesTo<GizmosDrawingController>().AsSingle();
        }
    }
}
