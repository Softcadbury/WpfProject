using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace LoginModule
{
    public class LoginModule : IModule
    {
        private readonly IRegionManager regionManager;

        public LoginModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.LoginView));
        }
    }
}