using Boo.Lang;
using UnityEngine;
using Zenject;

namespace FrozenExpress.Scripts.Game.DiFramework.Installers
{
    public class ModuleGroupInstaller : Zenject.ScriptableObjectInstaller
    {
        [SerializeField] List<ScriptableObjectInstaller> _installers;

        public override void InstallBindings()
        {
            foreach (var item in _installers)
            {
                Container.Inject(item);
                item.InstallBindings();
            }
        }
    }
}
