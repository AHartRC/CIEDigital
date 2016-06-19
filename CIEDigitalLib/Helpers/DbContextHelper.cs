using System.Data.Entity;
using CIEDigitalLib.Constants;

namespace CIEDigitalLib.Helpers
{
    public class DbContextHelper
    {
        public static void AddElmahFunctionality(DbContext context)
        {
            context.Database.ExecuteSqlCommand(StringConstants.Create_Elmah_Tables);
            context.Database.ExecuteSqlCommand(StringConstants.Create_Elmah_LogError_Proc);
            context.Database.ExecuteSqlCommand(StringConstants.Create_Elmah_GetErrorsXml_Proc);
            context.Database.ExecuteSqlCommand(StringConstants.Create_Elmah_GetErrorXml_Proc);
            context.SaveChanges();
        }
    }
}