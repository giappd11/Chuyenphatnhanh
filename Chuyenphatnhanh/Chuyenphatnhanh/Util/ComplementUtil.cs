using System;
using System.Web;
using System.Reflection;

namespace Chuyenphatnhanh.Util
{
    public static class ComplementUtil
    {
        public static void complement(Object source, Object destination)
        {
            Type typeB = destination.GetType();
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;
                PropertyInfo other = typeB.GetProperty(property.Name);
                if ((other != null) && (other.CanWrite))
                    other.SetValue(destination, property.GetValue(source, null), null);
            }
        }
    }
}