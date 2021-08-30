using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace Update
{
    class Program
    {
        static void Main()
        {
            using (var archive = RarArchive.Open("update.rar"))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(Directory.GetCurrentDirectory(), new ExtractionOptions { Overwrite = true });
                }
            }
            Process.Start("Wms.exe");
            Environment.Exit(0);
        }
    }
}
