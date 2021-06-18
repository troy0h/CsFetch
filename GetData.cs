using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CsFetch
{
	internal class GetData
	{
		public string ComputerName { get; set; }
		public string UserName { get; set; }
		public string OS { get; set; }
		public string Version { get; set; }
		public string Uptime { get; set; }
		public string Mobo { get; set; }
		public string Terminal { get; set; }
		public string CPU { get; set; }
		public string Cores { get; set; }
		public string Threads { get; set; }
		public double RAM { get; set; }
		public double UsedRAM { get; set; }
		public string Page { get; set; }
		public string GPU { get; set; }
		public double VRAM { get; set; }

		public GetData()
		{
				ComputerName = Environment.MachineName;
				UserName = Environment.UserName;
				GetOSAndVerion();
				Uptime = GetUptime();
				Terminal = Environment.GetEnvironmentVariable("ComSpec").ToString();
				SetPCInfo();
		}

		private void GetOSAndVerion()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				using RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				OS = $"{reg.GetValue("ProductName")}";
				Version = $"{reg.GetValue("ReleaseId")} (OS Build {reg.GetValue("CurrentBuildNumber")}.{reg.GetValue("UBR")})";
			}

			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				// Linux crap here
			}
        }

		private void SetPCInfo()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				using ManagementObjectSearcher
				win32Proc = new("select * from Win32_Processor"),
				win32CompSys = new("select * from Win32_ComputerSystem"),
				win32Memory = new("select * from Win32_OperatingSystem"),
				win32Mobo = new("select * from Win32_BaseBoard"),
				win32GPU = new("select * from Win32_VideoController"),
				win32Page = new("select * from Win32_PageFileUsage");

				foreach (ManagementObject obj in win32Mobo.Get())
				{
					Mobo = obj["Product"].ToString();
					break;
				}

				foreach (ManagementObject obj in win32Proc.Get())
				{
					CPU = obj["Name"].ToString();
					Cores = obj["NumberOfCores"].ToString();
					Threads = obj["NumberOfLogicalProcessors"].ToString();
					break;
				}

				foreach (ManagementObject obj in win32Memory.Get())
				{
					double x = Convert.ToDouble(obj["TotalVisibleMemorySize"]);
					x /= (1024 * 1024);
					RAM = Math.Round(x, 2);

					x = Convert.ToDouble(obj["FreePhysicalMemory"]);
					x /= (1024 * 1024);
					x = RAM - x;
					UsedRAM = Math.Round(x, 2);
					break;
				}

				foreach (ManagementObject obj in win32Page.Get())
				{
					double x = Convert.ToDouble(obj["AllocatedBaseSize"]);
					x = Math.Round((x / (1024)), 2);
					Page = $"{x} GB";
					break;
				}

				foreach (ManagementObject obj in win32GPU.Get())
				{
					GPU = obj["Name"].ToString();
					double x = Convert.ToDouble(obj["AdapterRam"]);
					x = Math.Round((x / (1024 * 1024 * 1024)), 2);
					VRAM = x;
					break;
				}
			}

			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				// Linux crap here
			}
		}

        public static string GetUptime()
		{
			int ticks = Environment.TickCount;
			int days = ticks / (1000 * 60 * 60 * 24);
			if (days > 0)
				ticks %= (1000 * 60 * 60 * 24);

			int hours = ticks / (1000 * 60 * 60);
			if (days > 0)
				ticks %= (1000 * 60 * 60);

			int mins = ticks / (1000 * 60);

			string uptime = $"{mins}m";
			if (days > 0)
				uptime = $"{days}d {hours}h {mins}m";
			else if (hours > 0)
				uptime = $"{hours}h {mins}m";

			return uptime;
		}

		List<Data> PCData;
		public List<Data> GetWithLabels()
		{
			if (PCData == null)
			{
				PCData = new List<Data>()
				{
					{ new Data { Value=$"{UserName}@{ComputerName}" } },
					{ new Data { Label="OS", Value=OS} },
					{ new Data { Label="Version", Value=Version } },
					{ new Data { Label="Motherboard", Value=Mobo} },
					{ new Data { Label="Uptime", Value=Uptime} },
					{ new Data { Label="Terminal", Value=Terminal} },
					{ new Data { Label="CPU", Value=$"{CPU.Trim()} ({Cores}C/{Threads}T)"} },
					{ new Data { Label="RAM", Value=$"{UsedRAM} GB / {RAM} GB"} },
					{ new Data {Label="Virtual Memory", Value=Page} },
					{ new Data {Label="GPU", Value=GPU} },
					{ new Data {Label="VRAM", Value=$"{VRAM} GB"} },
					{ new Data { } }
				};
			}
			return PCData;
		}

		public class Data
		{
			public string Label { get; set; }
			public string Value { get; set; }
		}
	}
}
