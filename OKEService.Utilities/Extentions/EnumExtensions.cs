using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OKEService.Utilities.Extentions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attributes = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes.FirstOrDefault()).Description;
            return description;
        }
    }
}
