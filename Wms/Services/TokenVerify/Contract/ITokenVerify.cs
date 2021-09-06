using System;

namespace Wms.Services.TokenVerify.Contract
{
    public interface ITokenVerify
    {
        event EventHandler VerifySuccess;
        event EventHandler VerifyError;
        void VerifyApiToken();
    }
}
