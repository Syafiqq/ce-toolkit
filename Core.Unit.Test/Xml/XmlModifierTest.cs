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
            XmlNode root = doc.DocumentElement;
            Transverse(root, Filter);
            doc.Save(file);
        }

        private void Filter(XmlElement el)
        {
            if (el?.Name == null)
                return;

            switch (el.Name.ToLowerInvariant())
            {
                case "laststate":
                    ProcLastState(el);
                    break;
                case "address":
                    ProcAddress(el);
                    break;
            }
        }

        private void ProcAddress(XmlElement el)
        {
            var addr = Convert.ToInt64(el.InnerText, 16);
            if(addr != 0L)
                el.InnerText = Convert.ToString(addr + 1, 16).ToUpperInvariant();
        }

        private void ProcLastState(XmlElement el)
        {
            if (el.HasAttribute("RealAddress"))
            {
                var addr = Convert.ToInt64(el.GetAttribute("RealAddress"), 16);
                if(addr != 0L)
                    el.SetAttribute("RealAddress", Convert.ToString(addr + 1, 16).ToUpperInvariant());
            }
        }

        void Transverse(XmlNode node, Action<XmlElement> action)
        {
            if (node == null)
                return;
            if (node is XmlElement n1)
                action(n1);

            if (node.HasChildNodes)
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n is XmlElement n2)
                        Transverse(n2, action);
                }
            }
        }
    }
}