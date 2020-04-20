using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using static System.Console;

namespace TennisTracker.Console
{
	public class Options
	{
		[Option('i', "input", Required = false, HelpText = "Input file.", Default = "input.txt")]
		public string Input { get; set; }
		
		[Option('o', "output", Required = false, HelpText = "Output file.", Default = "output.txt")]
		public string Output { get; set; }
	}
	
	class Program
	{
		static void Main(string[] args)
		{
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed(o =>
				{
					if (File.Exists(o.Output)) File.Delete(o.Output);
					
					var lines = File.ReadLines(o.Input);
					var output = new List<string>();
					
					foreach(var line in lines)
					{
						var match = new Match(line);
						output.Add(match.ToString());
					}
					
					File.AppendAllLines(o.Output, output);
				});
		}
	}
}