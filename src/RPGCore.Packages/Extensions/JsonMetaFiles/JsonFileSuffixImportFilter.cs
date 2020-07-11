﻿using System.IO;

namespace RPGCore.Packages.Extensions.MetaFiles
{
	internal class JsonFileSuffixImportFilter : ImportFilter
	{
		private readonly string suffix;

		public JsonFileSuffixImportFilter(string suffix)
		{
			this.suffix = suffix;
		}

		public override bool AllowFile(FileInfo file)
		{
			return !file.Name.EndsWith(suffix);
		}
	}
}
