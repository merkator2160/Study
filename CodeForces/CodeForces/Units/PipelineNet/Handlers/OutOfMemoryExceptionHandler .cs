using PipelineNet.Middleware;
using System;

namespace CodeForces.Units.PipelineNet.Handlers
{
    public class OutOfMemoryExceptionHandler : IMiddleware<Exception, bool>
    {
        public bool Run(Exception parameter, Func<Exception, bool> next)
        {
            if (parameter is OutOfMemoryException)
            {
                // Handle somehow
                return true;
            }
            return next(parameter);
        }
    }
}