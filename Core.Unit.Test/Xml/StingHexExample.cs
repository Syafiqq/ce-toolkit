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
    }
}