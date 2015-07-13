using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rental.UI.Resources;
namespace Rental.UI.Utility
{
    public static class LanguageHelper
    {
        /// <summary>
        /// 根据给定的key 获得从相关的resource 中获取值 并将该方法添加到HtmlHelper方法中
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLanguageByKey(this HtmlHelper htmlHelper, string key)
        {
            string cultureName = Thread.CurrentThread.CurrentCulture.Name;
            Type resourceType = null;
            if (cultureName == "en-US")
            {
                resourceType = typeof(Resources.en_US);
            }
            else if (cultureName == "zh-CN")
            {
                resourceType = typeof(Resources.zh_CN);
            }
            else
            {
                resourceType = typeof(Resources.zh_TW);
            }

            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
            {
                return p.GetValue(null, null).ToString();
            }
            else
            {
                return "undefined";
            }
        }

        /// <summary>
        /// 给js调用 通过相关的resouce来获得数据
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLanguageJs(this HtmlHelper htmlHelper, string key)
        {
            string cultureName = Thread.CurrentThread.CurrentCulture.Name;
            Type resourceType = null;
            if (cultureName == "en-US")
            {
                resourceType = typeof(Resources.en_US);
            }
            else if (cultureName == "zh-CN")
            {
                resourceType = typeof(Resources.zh_CN);
            }
            else
            {
                resourceType = typeof(Resources.zh_TW);
            }
            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
            {
                return string.Format("var {0} = '{1}'", key, p.GetValue(null, null).ToString());
            }
            else
            {
                return string.Format("var {0} = '{1}'", key, "undefined");
            }
        }
    }
}