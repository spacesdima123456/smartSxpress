using System;
using Wms.ViewModel.Dialog;

namespace Wms.Services.Window.Contract
{
    public interface IWindowBranch
    {
        void Close();
        void Create(Action<object> action);
        void Delete(Action<object> action);
        void Edit(Action<DisplayAlertBranchViewModel> action);
    }
}
