using System;

namespace Wan.Release.Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RelIdAttribute : System.Attribute
    {
        public string Name { get; protected set; }

        public RelIdAttribute(string name = null)
        {
            Name = name;
        }
    }
}
