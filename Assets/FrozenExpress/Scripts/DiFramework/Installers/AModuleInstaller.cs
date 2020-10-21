using System;
using UnityEngine;

namespace FrozenExpress
{
    public abstract class AModuleInstaller : Zenject.ScriptableObjectInstaller
    {
        protected new object Container => throw new InvalidOperationException();

        protected void Install<T>(params object[] arguments)
        {
            if (arguments.Length > 0)
            {
                base.Container.BindInterfacesTo<T>().AsSingle().WithArguments(arguments);
            }
            else
            {
                base.Container.BindInterfacesTo<T>().AsSingle();
            }
        }

        protected void Spawn<T>(T prefab) where T : MonoBehaviour
        {
            base.Container.BindInterfacesTo<T>().FromComponentInNewPrefab(prefab).AsSingle();
        }
    }
}
