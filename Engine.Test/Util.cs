using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EngineTest
{
    public class Util
    {
        // https://www.codeproject.com/Tips/5256504/Using-Embedded-Resources-in-Unit-Tests-with-NET
        public static Stream GetEmbeddedResourceStream(Assembly assembly, string relativeResourcePath)
        {
            if (string.IsNullOrEmpty(relativeResourcePath))
                throw new ArgumentNullException("relativeResourcePath");

            var resourcePath = String.Format("{0}.{1}",
                Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$", string.Empty, RegexOptions.IgnoreCase), relativeResourcePath);

            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new ArgumentException(String.Format("The specified embedded resource \"{0}\" is not found.", relativeResourcePath));
            return stream;
        
        }
    }
}