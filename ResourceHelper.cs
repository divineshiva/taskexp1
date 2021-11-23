using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace APIAssignment
{
    public class ResourceHelper
    {
        public static string expectedAllUsersObject => ReadEmbeddedResource($"APIAssignment.ExpectedAllUsersData.txt");

        private static Assembly Assembly => typeof(ResourceHelper).GetTypeInfo().Assembly;
        private static string ReadEmbeddedResource(string name)
        {
            using var stream = Assembly.GetManifestResourceStream(name);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
