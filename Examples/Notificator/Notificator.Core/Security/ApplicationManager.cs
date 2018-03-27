using System.Linq;
using Notificator.Core.Model;

namespace Notificator.Core.Security
{
    public static class ApplicationManager
    {
        internal static int GetApplicationId(this NotificatorEntities data, string applicationName)
        {
            var application = data.GetApplicationByName(applicationName).FirstOrDefault();
            if (application == null)
            {
                application = new Application { Name = applicationName };
                data.Applications.AddObject(application);
                data.SaveChanges();
            }

            return application.Id;
        }

        internal static Application GetApplication(this NotificatorEntities data, int applicationId)
        {
            return data.GetApplicationById(applicationId).FirstOrDefault();
        }

    }
}
