using System;
using System.IO;
using System.Xml;
using NUnit.Framework;
using static Core.Unit.Test.Xml.ResConfig;

namespace Core.Unit.Test.Xml
{
    public class XmlReaderTest
    {
        [Test]
        public void TestExecutablePath()
        {
            Console.WriteLine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void TestReadXml()
        {
            var file = Path.Combine(ExePath, ResPath, FileName);
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