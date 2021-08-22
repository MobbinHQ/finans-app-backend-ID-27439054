using FinansApp.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinansApp.Api.Helpers
{
    public interface IGenerateJwt
    {
        string generateJwtToken(User user);
    }
}
