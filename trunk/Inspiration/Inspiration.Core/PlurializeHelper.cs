using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
namespace Inspiration.Core
{
    public class PlurializeHelper
    {
        public static string Pluralize(string s)
        {
            PluralizationService plural = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            return plural.Pluralize(s);
        }
        public static string Singularize(string s)
        {
            PluralizationService plural = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            return plural.Singularize(s);
        }
    }
}
