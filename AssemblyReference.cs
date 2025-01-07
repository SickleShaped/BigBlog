using System.Reflection.Metadata;
using System.Reflection;

namespace BigBlog
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
