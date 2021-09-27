using Wms.ViewModel;
using Ninject.Modules;
using Wms.UnitOfWorkAPI;
using Wms.ViewModel.Page;
using Wms.UnitOfWorkAPI.Contract;
using Wms.Services.Window.Contract;
using Wms.Services.Window.WindowDialogs;

namespace Wms.DependencyInjection
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            //services
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<IWindowBranch>().To<WindowBranch>().InSingletonScope();
            Bind<IWindowSettings>().To<WindowSettings>().InSingletonScope();

            //view models 
            Bind<AdminViewModel>().ToSelf().InTransientScope();
            Bind<BranchesViewModel>().ToSelf().InTransientScope();
        }
    }
}
