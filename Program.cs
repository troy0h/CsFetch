using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CsFetch
{
	class Program
	{
		static void Main(string[] args)
		{
			var prev = Console.ForegroundColor;
			try
			{
				Console.WriteLine();

				// Initialize colours and settings
				var OsLogoColor = ConsoleColor.Blue;
				var OsLabelColor = ConsoleColor.Cyan;
				GetData Settings = new();
				var OsLogo = new List<string>();


				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					// Add Logo (From LogoManager.cs)
					OsLogo.AddRange(LogoManager.Windows());
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					// Add Logo (From LogoManager.cs)
					OsLogo.AddRange(LogoManager.Linux());
				}
                else
                { return; }

				// Makes Fetch Screen
				List<GetData.Data> Data = Settings.GetWithLabels();
				int i = 0;
				foreach (string line in OsLogo)
				{
					Console.ForegroundColor = OsLogoColor;
					if (i < (Data.Count - 1))
					{
						Console.Write($"  {line}");
						Console.ForegroundColor = OsLabelColor;
						if (!string.IsNullOrEmpty(Data[i].Label))
						{
							Console.Write($"  {Data[i].Label}:");
							Console.ForegroundColor = prev;
							Console.WriteLine($" {Data[i].Value}");
						}
						else
							Console.WriteLine($"  {Data[i].Value}");
					}
					else
						Console.WriteLine($"  {line}");

					i++;
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("Aaaaaagh, this should have never happened!");
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Console.ForegroundColor = prev;
				if (Debugger.IsAttached)
					Console.ReadLine();
			}
		}
	}
}
