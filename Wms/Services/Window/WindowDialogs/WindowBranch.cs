using System;
using Wms.Services.Window.Contract;
using Wms.WindowDialog;

namespace Wms.Services.Window.WindowDialogs
{
    public class WindowBranch : IWindowBranch
    {
        public void Delete(Action<object> action)
        {
           var  delete = new WindowDialog(new BranchDelete(action));
           delete.CreateWindow();
        }

        public void Edit(Action<object> action)
        {
           
        }
    }
}
