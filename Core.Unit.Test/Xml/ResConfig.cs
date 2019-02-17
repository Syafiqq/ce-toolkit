using System.IO;

namespace Core.Unit.Test.Xml
{
    public static class ResConfig
    {
        public static readonly string ExePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string ResPath = @"..\..\..\Resources";
        public static readonly string FileName = @"citra-qt-fe-fates.CT";
    }
}