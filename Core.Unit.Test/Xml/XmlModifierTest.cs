using System;
using System.IO;
using System.Reflection;
using System.Xml;
using NUnit.Framework;
using static Core.Unit.Test.Xml.ResConfig;

namespace Core.Unit.Test.Xml
{
    public class XmlModifierTest
    {
        [Test]
        public void TestExecutablePath()
        {
            Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void TestReadXml()
        {
            var file = Path.Combine(ExePath, ResPath, FileName);
            var doc = new XmlDocument();
            doc.Load(file);
            doc.Save(file);
        }
    }
}