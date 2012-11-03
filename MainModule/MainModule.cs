using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using Tools;

namespace MainModule
{
    public class StartModule : IModule
    {
        public StartModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(NameOfRegions.MainRegion, typeof(Views.MainView));
            _container.RegisterType<Object, Views.MainView>(NameOfViews.MainView);
        }
    }
}