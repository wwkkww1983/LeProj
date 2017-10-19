using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class EnumExtensions
    {
        public static MvcHtmlString DisplayEnumDescription(this HtmlHelper htmlHelper, string enumTypeName, int value)
        {
            Type enumType = Type.GetType("ZdflCount.Models." + enumTypeName);
            if (!Enum.IsDefined(enumType, value))
            {
                return MvcHtmlString.Empty;
            }

            DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])(enumType.GetField(Enum.GetName(enumType, value))).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (EnumAttributes.Length > 0)
            {
                return MvcHtmlString.Create(EnumAttributes[0].Description);
            }

            //如果只是要简单的返回枚举的Name值就只要这一行了
            return MvcHtmlString.Create(Enum.GetName(enumType, value));
        }
    }
}
