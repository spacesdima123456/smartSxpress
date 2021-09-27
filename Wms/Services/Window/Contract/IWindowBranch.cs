using System;
using Wms.API.Models;
using Wms.ViewModel.Dialog;

namespace Wms.Services.Window.Contract
{
    public interface IWindowBranch
    {
        void Close();
        void Delete(Action<object> action);
        void Edit(Branches branches, Action<DisplayAlertBranchViewModel> action);
        void Create(Action<DisplayAlertBranchViewModel> action);
    }
}
