using System;
using Wms.API.Models;
using Wms.ViewModel.Dialog;

namespace Wms.Services.Window.Contract
{
    public interface IWindowBranch
    {
        void Close();
        void Delete(Action<object> action);
        void Create(Action<DisplayAlertBranchBaseViewModel> action);
        void Edit(Branches branches, Action<DisplayAlertBranchBaseViewModel> action);
    }
}
