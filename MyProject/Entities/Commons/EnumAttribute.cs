using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Commons
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldDimensionAttribute : Attribute
    {
        public FieldDimensionAttribute(string text)
        {
            FieldDimension = text;
        }

        public string FieldDimension { get; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(string text)
        {
            FieldName = text;
        }

        public string FieldName { get; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FieldFormatAttribute : Attribute
    {
        public FieldFormatAttribute(string text)
        {
            FieldFormat = text;
        }

        public string FieldFormat { get; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ClassNameAttribute : Attribute
    {
        public ClassNameAttribute(string text)
        {
            ClassName = text;
        }

        public string ClassName { get; }
    }
}
