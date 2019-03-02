using System;
using System.Xml;

namespace Core
{
    public class XmlModifier
    {
        public long Modifier { get; private set; }

        private XmlModifier()
        {
        }

        public static XmlModifier WithConfig(long modifier)
        {
            var instance = new XmlModifier();
            instance.Modifier = modifier;
            return instance;
        }

        public void Filter(XmlElement el)
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
            if (addr != 0L)
                el.InnerText = Convert.ToString(addr + Modifier, 16).ToUpperInvariant();
        }

        private void ProcLastState(XmlElement el)
        {
            if (el.HasAttribute("RealAddress"))
            {
                var addr = Convert.ToInt64(el.GetAttribute("RealAddress"), 16);
                if (addr != 0L)
                    el.SetAttribute("RealAddress", Convert.ToString(addr + Modifier, 16).ToUpperInvariant());
            }
        }

        public void Transverse(XmlNode node, Action<XmlElement> action)
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