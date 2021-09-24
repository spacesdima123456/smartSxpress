using Ninject.Modules;
using Wms.Services.Window.Contract;
using Wms.Services.Window.WindowDialogs;
using Wms.UnitOfWorkAPI;
using Wms.UnitOfWorkAPI.Contract;
using Wms.ViewModel;
using Wms.ViewModel.Page;

namespace Wms.DependencyInjection
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            //services
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<IWindowBranch>().To<WindowBranch>().InSingletonScope();

            //view models 
            Bind<AdminViewModel>().ToSelf().InTransientScope();
            Bind<BranchesViewModel>().ToSelf().InTransientScope();
        }
    }
}
