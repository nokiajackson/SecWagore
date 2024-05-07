using SecWagore.Models;
using System.ComponentModel;
using System;

namespace SecWagore.Heplers
{
    public static class EnumeratorHelper
    {
        /// <summary>
        /// 將列舉轉陣列，可供下拉式選單使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyName> GetEnumItems<T>() where T : struct, IConvertible
        {
            var result = new List<KeyName>();

            var type = typeof(T);
            var values = type.GetEnumValues();

            foreach (var i in values)
            {
                result.Add(new KeyName
                {
                    Key = (int)i,
                    Name = type.GetEnumName(i)
                });
            }

            return result;
        }

        /// <summary>
        /// 取得列舉陣列 DescriptionAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyName> GetEnumDescriptions<T>() where T : struct, Enum
        {
            var result = new List<KeyName>();

            var enumNames = Enum.GetNames<T>();
            var enumType = typeof(T);

            foreach (var enumName in enumNames)
            {
                var fieldInfo = enumType.GetField(enumName);
                var attribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

                result.Add(new KeyName()
                {
                    Key = (int)Enum.Parse(enumType, enumName),
                    Name = attribute?.Description ?? enumName
                });
            }

            return result;
        }

        /// <summary>
        /// 取得Enum 的Desc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetEnumDesc<T>(T input) where T : struct, System.Enum
        {
            var fi = typeof(T).GetField(input.ToString());

            var descriptionAttributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return descriptionAttributes?.Description ?? input.ToString();
        }

        /// <summary>
        /// 從Enum中尋找Member Name 包含Keyword的物件
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static List<TEnum> EnumFinder<TEnum>(string keyword) where TEnum : struct, System.Enum
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return new List<TEnum>();
            }

            return Enum.GetNames<TEnum>()
                .Where(r => r.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Select(r => Enum.Parse<TEnum>(r))
                .ToList();
        }

        /// <summary>
        /// 取得列舉上Attribute內設定的某Prop Value
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TAttr"></typeparam>
        /// <param name="enumInput"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string? GetEnumAttributePropValue<TEnum, TAttr>(TEnum enumInput, string propName) where TEnum : Enum where TAttr : Attribute
        {
            var enumName = enumInput.ToString();

            var fieldInfo = typeof(TEnum).GetFields()
                .First(r => r.Name == enumName);

            var customerAttr =
                fieldInfo.GetCustomAttributes(typeof(TAttr), false).FirstOrDefault() as TAttr;

            var propertyInfo = customerAttr?.GetType()?.GetProperty(propName);
            if (propertyInfo == null)
            {
                return enumName;
            }

            return propertyInfo.GetValue(customerAttr)?.ToString();
        }
    }
}