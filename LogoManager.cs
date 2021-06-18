using System;
using System.Collections.Generic;

namespace CsFetch
{
	internal class LogoManager
	{
		public static List<string> Windows()
		{
			List<string> OsLogo = new()
			{
				{ "                                  .., " },
				{"                      ....,,:;+ccllll "},
				{"        ...,,+:;  cllllllllllllllllll "},
				{"  ,cclllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"                                      "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  llllllllllllll  lllllllllllllllllll "},
				{"  `'ccllllllllll  lllllllllllllllllll "},
				{"         `'\"\"*::  :ccllllllllllllllll "},
				{"                        ````''\"*::cll "},
				{"                                   `` "}
			};

			return OsLogo;
		}

		public static List<string> Linux()
		{
			List<string> OsLogo = new()
			{
				{ $"                            " },
				{ $"                            " },
				{ $"                            " },
				{ $"         #####              " },
				{ $"        #######             " },
				{ $"        ##O#O##             " },
				{ $"        #######             " },
				{ $"      ###########           " },
				{ $"     #############          " },
				{ $"    ###############         " },
				{ $"    ################        " },
				{ $"   #################        " },
				{ $" #####################      " },
				{ $" #####################      " },
				{ $"   #################        " },
				{ $"                            " },
				{ $"                            " },
				{ $"                            " }
			};

			return OsLogo;
		}
	}
}
