using PipelineNet.Middleware;
using System;

namespace CodeForces.Units.PipelineNet.Handlers
{
    public class ArgumentExceptionHandler : IMiddleware<Exception, bool>
    {
        public bool Run(Exception parameter, Func<Exception, bool> next)
        {
            if (parameter is ArgumentException)
            {
                // Handle somehow
                return true;
            }
            return next(parameter);
        }
    }
}