using Wms.DependencyInjection;
using Wms.ViewModel.Page;

namespace Wms.ViewModel
{
    public class ViewModelLocator
    {
        public static AdminViewModel AdminViewModel => IocKernel.Get<AdminViewModel>();
        public static PackageViewModel PackageViewModel => IocKernel.Get<PackageViewModel>();
        public static BranchesViewModel BranchesViewModel => IocKernel.Get<BranchesViewModel>();
    }
}
