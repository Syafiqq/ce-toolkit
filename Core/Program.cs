using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using IniParser;

namespace Core
{
    class Program
    {
        public static string Path { get; set; }
        public static long? Modifier { get; set; }

        static void Main(string[] args)
        {
            Init(args);
            if (Path == null)
            {
                Console.WriteLine("Invalid Path");
                Environment.Exit(1);
            }

            if (Modifier == null)
            {
                Console.WriteLine("Invalid Modifier");
                Environment.Exit(1);
            }

            DoProcess();
        }

        private static void DoProcess()
        {
            Debug.Assert(Modifier != null, nameof(Modifier) + " != null");

            var doc = new XmlDocument();
            doc.Load(Path);
            XmlNode root = doc.DocumentElement;
            XmlTransversal.Transverse(root, XmlModifier.WithConfig((long) Modifier).Filter);
            doc.Save(Path);
        }

        private static void Init(string[] args)
        {
            InitArgs(args);
            InitIni();
        }

        private static void InitIni()
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile("config.ini");
            if (Path == null && data?["Config"]?["Path"] != null)
            {
                TryGetPath(data["Config"]["Path"]);
            }

            if (Modifier == null && data?["Config"]?["Modifier"] != null)
            {
                TryGetModifier(data["Config"]["Modifier"]);
            }
        }

        private static void InitArgs(string[] args)
        {
            if (args == null || args.Length <= 0)
                return;

            if (args.Length == 1)
            {
                TryGetModifier(args[0]);
            }
            else
            {
                for (int c = -1, cs = args.Length; ++c < cs;)
                {
                    switch (args[c])
                    {
                        case "/p":
                            TryGetPath(args[++c]);
                            break;
                        case "/m":
                            TryGetModifier(args[++c]);
                            break;
                    }
                }
            }
        }

        private static void TryGetPath(string s)
        {
            try
            {
                if (File.Exists(s))
                    Path = s;
            }
            catch
            {
                // ignored
            }

            if (Path != null) return;
            Console.WriteLine("Invalid Path");
            Environment.Exit(1);
        }

        private static void TryGetModifier(string s)
        {
            try
            {
                Modifier = HexParser.HexStringToLong(s);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Modifier");
                Environment.Exit(1);
            }
        }
    }
}