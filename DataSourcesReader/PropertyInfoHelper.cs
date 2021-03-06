﻿using System;
using System.Linq;
using System.Reflection;

namespace DataSourcesReaders
{
    public static class PropertyInfoHelper
    {
        public static void SetCastedValue(this object target, string propertyName, object value)
        {
            var property = target.GetType().GetProperties().First(s => s.Name == propertyName);

            SetCastedValue(target, property, value);
        }

        public static void SetCastedValue(this object target, PropertyInfo property, object value)
        {
            var castedValue = CastValueToPropertyType(property, value);
            property.SetValue(target, castedValue);
        }

        public static object CastValueToPropertyType(this PropertyInfo property, object value)
        {
            if (property.PropertyType.IsEnum)
            {
                var enumType = property.PropertyType;

                return Enum.Parse(enumType, value.ToString());
            }

            return Convert.ChangeType(value, property.PropertyType);
        }
    }
}