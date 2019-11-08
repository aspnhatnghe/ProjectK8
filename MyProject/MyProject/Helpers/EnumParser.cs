using Entities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MyProject.Helpers
{
    public static class EnumParser
    {
        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Error: enum is null");
            }

            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("Error: " + value + " cannot be found in the enum collection");
            }
        }

        public static T ToEnum<T>(this int value)
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("Error: " + value + " cannot be found in the enum collection");
            }
        }

        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                                                        typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }

        public static string GetFieldDimension(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = fi.GetCustomAttributes(
                typeof(FieldDimensionAttribute), false) as FieldDimensionAttribute[];

            if (attributes != null && attributes.Length > 0)
                return attributes[0].FieldDimension;
            else
                return enumValue.ToString();
        }

        public static string GetClassName(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = fi.GetCustomAttributes(
                typeof(ClassNameAttribute), false) as ClassNameAttribute[];

            if (attributes != null && attributes.Length > 0)
                return attributes[0].ClassName;
            else
                return enumValue.ToString();
        }

        public static string GetFieldName(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = fi.GetCustomAttributes(
                typeof(FieldNameAttribute), false) as FieldNameAttribute[];

            return (attributes != null && attributes.Length > 0)
                ? attributes[0].FieldName
                : enumValue.ToString();
        }

        public static string GetFieldFormat(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = (FieldFormatAttribute[])fi.GetCustomAttributes(
                                                        typeof(FieldFormatAttribute), false);

            return attributes.Length > 0 ? attributes[0].FieldFormat : "";
        }

        public static T GetEnumFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Enum cannot be found by the description");
        }

        public static IEnumerable<Enum> GetFlags(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
        }

        public static IEnumerable<T> Split<T>(this int value)
        {
            foreach (object cur in Enum.GetValues(typeof(T)))
            {
                var number = (int)(object)(T)cur;
                if (0 != (number & value))
                {
                    yield return (T)cur;
                }
            }
        }
    }
}
