using System;
using System.ComponentModel;
using System.Reflection;

namespace Tango.Types.Extensions
{
    /// <summary>
    /// Provides extension methods to improve <see cref="Enum"/> types
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the value of <see cref="DescriptionAttribute"/> of an <see cref="Enum"/> item.
        /// </summary>
        /// <param name="enumValue">the input value.</param>
        /// <returns>Returns the description in <see cref="DescriptionAttribute"/>. Otherwise, returns the <see cref="Enum"/> item name.</returns>
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
