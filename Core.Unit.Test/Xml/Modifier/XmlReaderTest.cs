using System;
using System.IO;
using System.Xml;
using NUnit.Framework;

namespace Core.Unit.Test.Xml.Modifier
{
    public class XmlReaderTest
    {
        public static readonly string ExePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string ResPath = @"..\..\..\Resources";
        public static readonly string FileName = @"citra-qt-fe-fates.CT";

        [Test]
        public void TestExecutablePath()
        {
            Console.WriteLine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void TestReadXml()
        {
            var file = Path.Combine(ExePath, ResPath, FileName);
            Console.WriteLine(file);
            using (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        Console.WriteLine(reader.Name);
                    }
                }
            }
        }
    }
}