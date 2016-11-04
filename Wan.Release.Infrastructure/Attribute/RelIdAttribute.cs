using System;

namespace Wan.Release.Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RelIdAttribute : System.Attribute
    {
        public RelIdAttribute(string name = null)
        {
            Name = name;
        }

        public string Name { get; protected set; }
    }
}