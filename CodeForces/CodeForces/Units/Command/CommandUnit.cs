using CodeForces.Units.Command.Commands;

namespace CodeForces.Units.CommandUnit
{
    public static class CommandUnit
    {
        public static void Run()
        {
            //Example1();
            //Example2();
            //Example3();
        }
        private static void Example1()
        {
            var invoker = new Invoker();
            var receiver = new Receiver();
            var command = new ConcreteCommand(receiver);

            invoker.SetCommand(command);
            invoker.Run();
        }
        private static void Example2()
        {
            var pult = new Pult();
            var tvSet = new TvSet();
            pult.SetCommand(new TvSetCommand(tvSet));
            pult.PressButton();
            pult.PressUndo();
        }
        private static void Example3()
        {
            var pult = new Pult();
            var tv = new TvSet();
            pult.SetCommand(new TvSetCommand(tv));
            pult.PressButton();
            pult.PressUndo();

            var microwave = new Microwave();
            pult.SetCommand(new MicrowaveCommand(microwave, 5000));     // 5000 - время нагрева пищи
            pult.PressButton();
        }
        private static void Example4()
        {
            var tv = new TvSet();
            var volume = new Volume();
            var mPult = new MultiPult();
            mPult.SetCommand(0, new TvSetCommand(tv));
            mPult.SetCommand(1, new VolumeCommand(volume));

            // включаем телевизор
            mPult.PressButton(0);

            // увеличиваем громкость
            mPult.PressButton(1);
            mPult.PressButton(1);
            mPult.PressButton(1);

            // действия отмены
            mPult.PressUndoButton();
            mPult.PressUndoButton();
            mPult.PressUndoButton();
            mPult.PressUndoButton();
        }
        private static void Example5()
        {
            //var programmer = new Programmer();
            //var tester = new Tester();
            //var marketolog = new Marketolog();

            //var commands = new List<ICommand>
            //{
            //    new CodeCommand(programmer),
            //    new TestCommand(tester),
            //    new AdvertizeCommand(marketolog)
            //};
            //var manager = new Manager();
            //manager.SetCommand(new MacroCommand(commands));
            //manager.StartProject();
            //manager.StopProject();
        }
    }
}