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

				// Add Logo (From LogoManager.cs)
				
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					OsLogo.AddRange(LogoManager.Windows());
					List<GetData.Data> WinData = Settings.GetWithLabels();

					int i = 0;
					foreach (string line in OsLogo)
					{
						Console.ForegroundColor = OsLogoColor;
						if (i < (WinData.Count - 1))
						{
							Console.Write($"  {line}");
							Console.ForegroundColor = OsLabelColor;
							if (!string.IsNullOrEmpty(WinData[i].Label))
							{
								Console.Write($"  {WinData[i].Label}:");
								Console.ForegroundColor = prev;
								Console.WriteLine($" {WinData[i].Value}");
							}
							else
								Console.WriteLine($"  {WinData[i].Value}");
						}
						else
							Console.WriteLine($"  {line}");

						i++;
					}
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					OsLogo.AddRange(LogoManager.Linux());
				}
                else
                { }

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
