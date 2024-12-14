using System;

namespace BugFindingTale {
	class Program {
		static void Main(string[] args) {
			try 
			{
                var line = Console.ReadLine();
                if (line == null || !int.TryParse(line, out int a) || a < 0)
                {
                    Console.WriteLine("Error!");
                    return;
                }

                line = Console.ReadLine();
                if (line == null || !int.TryParse(line, out int b) || b < 0)
                {
                    Console.WriteLine("Error!");
                    return;
                }

				int result = (a > b) ? a - b : b - a;
				
				Console.WriteLine("Result: {0}", result);
			} 
			catch (FormatException) 
			{
				Console.WriteLine("Error!");
			}
		}
	}
}