using UnityEngine;
using Zenject;

public class Installer : MonoInstaller<Installer>
{
    public override void InstallBindings()
    {
        Container.Bind<Magnet>().AsSingle();
        Container.Bind<IInitializable>().To<Magnet>().AsSingle();
    }
}