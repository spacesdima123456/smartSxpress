using System;
using Wms.API.Models;

namespace Wms.Services.TokenVerify.Contract
{
    public interface ITokenVerify
    {
        event EventHandler<LoginRes> VerifySuccess;
        event EventHandler VerifyError;
        void VerifyApiToken();
    }
}
