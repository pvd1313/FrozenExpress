using System.Collections.Generic;
using UnityEngine;

namespace FrozenExpress.Game.DiFramework.Installers
{
    public class ModuleGroupInstaller : Zenject.ScriptableObjectInstaller
    {
        [SerializeField] List<AModuleInstaller> _installers;

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
