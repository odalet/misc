using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Delta.Compression.LZ77;

namespace Delta.WinHelp.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////var input = "Hello, World!";  
            ////using (var inms = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            ////using (var lz = new LZ77Stream(inms, CompressionMode.Decompress))
            ////using (var outms = new MemoryStream())
            ////{
            ////    lz.CopyTo(outms);                
            ////    var output = Encoding.UTF8.GetString(outms.ToArray());
            ////}

            var here = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var tests = new string[] 
            {
                Path.Combine(here, "data", "ENHMET.HLP"),           
                Path.Combine(here, "data", "Css.hlp"),              // Has LZ7 Compression and Hall Phrase compression
                Path.Combine(here, "data", "html40.HLP"),           // Has LZ7 Compression and Hall Phrase compression
                Path.Combine(here, "data", "html40.GID"),
                Path.Combine(here, "data", "OLEMSG2.HLP"),
                Path.Combine(here, "data", "OLEMSG2.GID"),

                Path.Combine(here, "data", "Borland", "MCISTRWH.HLP"),
                Path.Combine(here, "data", "Borland", "PENAPIWH.HLP"),
                Path.Combine(here, "data", "Borland", "SHED.HLP"),
                Path.Combine(here, "data", "Borland", "TCWHELP.HLP"),
                Path.Combine(here, "data", "Borland", "WIN31MWH.HLP"),
                Path.Combine(here, "data", "Borland", "WORKHELP.HLP"),

                Path.Combine(here, "data", "VB3", "DATAMGR.HLP"),
                Path.Combine(here, "data", "VB3", "VB.HLP"),                // Has Phrases and no compression

                Path.Combine(here, "data", "VB5CCE", "ccreadme.hlp"),
                Path.Combine(here, "data", "VB5CCE", "ctlcrwzd.hlp"),
                Path.Combine(here, "data", "VB5CCE", "proppgwz.hlp"),
                Path.Combine(here, "data", "VB5CCE", "setupwiz.hlp"),
                Path.Combine(here, "data", "VB5CCE", "vb5.hlp"),             // Has NLevels == 2!
                Path.Combine(here, "data", "VB5CCE", "vb5def.hlp"),
                Path.Combine(here, "data", "VB5CCE", "vb5pss.hlp"),
                Path.Combine(here, "data", "VB5CCE", "vbcmn96.hlp"),
                Path.Combine(here, "data", "VB5CCE", "vbenlr3.hlp"),
                Path.Combine(here, "data", "VB5CCE", "veENdf3.hlp"),

                Path.Combine(here, "data", "Watcom", "MCISTRWH.HLP"),
                Path.Combine(here, "data", "Watcom", "PENAPIWH.HLP"),
                Path.Combine(here, "data", "Watcom", "SHED.HLP"),
                Path.Combine(here, "data", "Watcom", "WIN31MWH.HLP"),
                Path.Combine(here, "data", "Watcom", "WINHELP.HLP")
            };

            var docsWithPhrases = new List<WinHelpDocument>();
            foreach (var test in tests.Where(f => !f.ToLowerInvariant().EndsWith(".gid")))
            {
                var doc = WinHelpDocument.Load(test);
                if (doc.Info.Compression == WinHelpCompression.None)
                {
                    var found = doc.Files.Where(f => f.IsPhrasesFile).ToArray();
                    if (found.Length > 0)
                        docsWithPhrases.Add(doc);
                }
            }

            
            

            
        }
    }
}
