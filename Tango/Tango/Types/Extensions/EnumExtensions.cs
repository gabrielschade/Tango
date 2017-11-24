using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tango.Types.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute atributoDescription
                    = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return atributoDescription == null ? enumValue.ToString() : atributoDescription.Description;
        }
    }
}
