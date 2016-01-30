using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Search
{
    public class SearchModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public SearchModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            var view = this.UnityContainer.Resolve<Views.Search>();            
            this.RegionManager.AddToRegion(RegionNames.LeftRegion, view);
        }
    }
}
