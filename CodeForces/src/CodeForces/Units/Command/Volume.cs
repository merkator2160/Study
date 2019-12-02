using System;

namespace CodeForces.Units.Command
{
	public class Volume
	{
		public const Int32 Off = 0;
		public const Int32 High = 20;
		private Int32 _level;


		public Volume()
		{
			_level = Off;
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void RaiseLevel()
		{
			if(_level < High)
				_level++;
			Console.WriteLine("Уровень звука {0}", _level);
		}
		public void DropLevel()
		{
			if(_level > Off)
				_level--;
			Console.WriteLine("Уровень звука {0}", _level);
		}
	}
}