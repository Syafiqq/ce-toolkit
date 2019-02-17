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
            Console.WriteLine(el.Name);
        }

        void Transverse(XmlNode node, Action<XmlElement> action)
        {
            if (node == null)
                return;
            if(node is XmlElement n1)
                action(n1);

            if (node.HasChildNodes)
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    if(n is XmlElement n2)
                        Transverse(n2, action);
                }
            }
        }
    }
}