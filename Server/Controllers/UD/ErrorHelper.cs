using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telerik.Blazor.Components;

namespace DOOR.Server.Controllers.UD
{
    public class ErrorHelper
    {
        public static string HandleDBException(DOOROracleContext context, OraTransMsgs oraMessages, Exception exception)
        {
            if (exception is DbUpdateException)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException((DbUpdateException)exception, oraMessages);
                return Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors);
            }

            context.Database.RollbackTransaction();
            List<OraError> errors = new List<OraError>();
            errors.Add(new OraError(1, exception.Message.ToString()));
            return Newtonsoft.Json.JsonConvert.SerializeObject(errors);
            
        }
    }
}
