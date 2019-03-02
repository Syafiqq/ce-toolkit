using System;
using System.Xml;

namespace Core
{
    public static class XmlTransversal
    {
        public static void Transverse(XmlNode node, Action<XmlElement> action)
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