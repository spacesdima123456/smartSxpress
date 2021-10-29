using Refit;
using Wms.ViewModel;
using Wms.API.Models;
using System.Windows;
using System.Threading.Tasks;

namespace Wms.Helpers
{
    public static class ErrorValidation
    {
        public static async Task HandleErrorsAsync<T>(ApiException e, T vm) where T : ValidateViewModel
        {
            var content = await e.GetContentAsAsync<Error>();
            if (content.Errors != null)
                vm.HandleErrors(content);
            else
                MessageBox.Show(content.Text);
        }

        public static void HandleGeneralErrors(Error error)
        {
            if (error!=null)
            {
                if (error.Code==4)
                {
                    Application.Current.Shutdown();
                    System.Windows.Forms.Application.Restart();
                }
            }
        }

        public static async Task HandleGeneralErrorsAsync(ApiException e)
        {
            var content = await e.GetContentAsAsync<Error>();
            if (content!=null)
                HandleGeneralErrors(content);
        }
    }
}
