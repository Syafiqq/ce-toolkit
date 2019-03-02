using System;
using NUnit.Framework;
using Org.BouncyCastle.Utilities.Encoders;

namespace Core.Unit.Test.Xml
{
    public class StingHexExample
    {
        [Test]
        public void TestDecodeHex()
        {
            var x = "F";
            var y = Hex.Decode($"{"".PadRight(16 - x.Length, '0')}{x}");
            var a = new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF};

            Assert.AreEqual(a, y);
            Assert.AreEqual(a.Length, y.Length);
        }

        [Test]
        public void TestBytesToLong()
        {
            var x = new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF};
            if (BitConverter.IsLittleEndian)
                Array.Reverse(x);
            var y = BitConverter.ToInt64(x, 0);
            var a = 15;
            Assert.AreEqual(a, y);
        }

        [Test]
        public void TestHexToLong()
        {
            var x = "F";
            var y = Hex.Decode($"{"".PadRight(16 - x.Length, '0')}{x}");
            if (BitConverter.IsLittleEndian)
                Array.Reverse(y);
            var y1 = BitConverter.ToInt64(y, 0);
            var a = 15L;
            
            Assert.AreEqual(a, y1);
            Assert.AreEqual(8, y.Length);
        }
        
        [Test]
        public void TestDecodeMinusHex()
        {
            var x = "-0000000F";
            var s = '+';
            if (x[0] == '-' || x[0] == '+')
            {
                s = x[0];
                x = x.Remove(0, 1);
            }

            var y = Hex.Decode($"{"".PadRight(16 - x.Length, '0')}{x}");
            var a = new byte[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF};

            Assert.AreEqual(a, y);
            Assert.AreEqual(a.Length, y.Length);
        }
        
        [Test]
        public void TestMinusHexToLong()
        {
            var x = "-F";
            var s = '+';
            if (x[0] == '-' || x[0] == '+')
            {
                s = x[0];
                x = x.Remove(0, 1);
            }
            var y = Hex.Decode($"{"".PadRight(16 - x.Length, '0')}{x}");
            if (BitConverter.IsLittleEndian)
                Array.Reverse(y);
            var y1 = BitConverter.ToInt64(y, 0);
            if (s == '-')
                y1 = 0 - y1;
            var a = -15L;
            
            Assert.AreEqual(a, y1);
            Assert.AreEqual(8, y.Length);
        }
    }
}