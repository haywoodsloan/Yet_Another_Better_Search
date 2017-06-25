using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yet_Another_Better_Search
{
    public static class EnumEx
    {
        public static string[] GetDescriptions(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException();

            string[] names = Enum.GetNames(enumType);
            string[] descriptions = new string[names.Length];

            for(int i = 0; i < names.Length; i++)
            {
                FieldInfo enumFieldInfo = enumType.GetField(names[i]);
                DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])
                    enumFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                
                if(enumAttributes.Length > 0)
                {
                    descriptions[i] = enumAttributes[0].Description;
                }
                else
                {
                    descriptions[i] = names[i];
                }
            }

            return descriptions;
        }

        public static object GetDescription(Type enumType, object value)
        {
            if (!enumType.IsEnum) throw new ArgumentException();

            FieldInfo enumField = enumType.GetField(value.ToString());
            DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])
                   enumField.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].Description;
            }
            else
            {
                return enumField.Name;
            }
        }

        public static object GetValueFromDescription(Type enumType, string description)
        {
            if (!enumType.IsEnum) throw new ArgumentException();

            foreach(FieldInfo enumField in enumType.GetFields())
            {
                DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])
                   enumField.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (enumAttributes.Length > 0)
                {
                    if(description == enumAttributes[0].Description)
                    {
                        return enumField.GetValue(null);
                    }
                }
                else
                {
                    if (description == enumField.Name)
                    {
                        return enumField.GetValue(null);
                    }
                }
            }

            throw new ArgumentException();
        }
    }
}
