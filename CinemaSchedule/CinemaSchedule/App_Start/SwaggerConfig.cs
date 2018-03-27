using Swashbuckle.Application;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;

namespace CinemaSchedule
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Cinema schedule")
                        .Description("A sample cinema schedule API for testing and prototyping Swashbuckle features.");

                    var xmlCommentsPath = GetCommentsFilePath();
                    c.IncludeXmlComments(xmlCommentsPath);
                })
                .EnableSwaggerUi();
        }
        private static String GetCommentsFilePath()
        {
            var baseDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}\bin";
            var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
            var commentsFilePath = Path.Combine(baseDirectory, commentsFileName);
            return commentsFilePath;
        }
    }
}
