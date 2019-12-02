using CodeForces.Units.PipelineNet.Handlers;
using PipelineNet.ChainsOfResponsibility;
using PipelineNet.MiddlewareResolver;
using System;

namespace CodeForces.Units.PipelineNet
{
    public static class PipelineNetUnit
    {
        // https://github.com/ipvalverde/PipelineNet
        public static void Run()
        {
            RunChain();
        }
        private static void RunChain()
        {
            var exceptionHandlersChain = new ResponsibilityChain<Exception, bool>(new ActivatorMiddlewareResolver())
                .Chain<OutOfMemoryExceptionHandler>()
                .Chain<ArgumentExceptionHandler>()
                .Finally((parameter) =>
                {
                    Console.WriteLine("Finally");
                    return true;
                });
            var result = exceptionHandlersChain.Execute(new InvalidOperationException());

            Console.WriteLine(result);
        }

        // Example fore using of pipelines could be founded here: https://github.com/ipvalverde/PipelineNet#pipelines
        private static void RunPipeline()
        {
            //var pipeline = new Pipeline<Bitmap>(new ActivatorMiddlewareResolver())
            //    .Add<RoudCornersMiddleware>()
            //    .Add<AddTransparencyMiddleware>()
            //    .Add<AddWatermarkMiddleware>();

            //Bitmap image1 = (Bitmap)Image.FromFile("party-photo.png");
            //Bitmap image2 = (Bitmap)Image.FromFile("marriage-photo.png");
            //Bitmap image3 = (Bitmap)Image.FromFile("matrix-wallpaper.png");

            //pipeline.Execute(image1);
            //pipeline.Execute(image2);
            //pipeline.Execute(image3);
        }
        private static async void RunPipelineAsync()
        {
            //var pipeline = new AsyncPipeline<Bitmap>(new ActivatorMiddlewareResolver())
            //    .Add<RoudCornersAsyncMiddleware>()
            //    .Add<AddTransparencyAsyncMiddleware>()
            //    .Add<AddWatermarkAsyncMiddleware>();

            //Bitmap image1 = (Bitmap)Image.FromFile("party-photo.png");
            //Task task1 = pipeline.Execute(image1); // you can also simply use "await pipeline.Execute(image1);"

            //Bitmap image2 = (Bitmap)Image.FromFile("marriage-photo.png");
            //Task task2 = pipeline.Execute(image2);

            //Bitmap image3 = (Bitmap)Image.FromFile("matrix-wallpaper.png");
            //Task task3 = pipeline.Execute(image3);

            //Task.WaitAll(new Task[] { task1, task2, task3 });
        }
    }
}