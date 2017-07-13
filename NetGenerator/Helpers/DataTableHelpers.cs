using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourPrj.BusinessLogic.Helpers
{
    public class SortingColumnAttribute : Attribute
    {
        public int Index { get; }

        public SortingColumnAttribute(int index)
        {
            Index = index;
        }
    }

    public class DisplayColumnAttribute : Attribute
    {
        public int Index { get; }

        public DisplayColumnAttribute(int index)
        {
            Index = index;
        }
    }



    public static class DataTableQueryString
    {
        public static string OrderingColumn = "order[0][column]";
        public static string OrderingDir = "order[0][dir]";
        public static string Searching = "search[value]";
    }

    public static class DataTableHelper
    {
        public static IList<string> DisplayColumns<T>()
        {
            var result = new Dictionary<int, string>();

            var props = typeof(T).GetProperties();
            foreach (var propertyInfo in props)
            {
                var propAttr =
                    propertyInfo
                        .GetCustomAttributes(false)
                        .OfType<DisplayColumnAttribute>()
                        .FirstOrDefault();
                if (propAttr != null)
                {
                    result.Add(propAttr.Index,propertyInfo.Name);
                }
            }

            return result.OrderBy(x => x.Key).Select(x => x.Value).ToList();
        }
        public static string SoringColumnName<T>(string columnIndex)
        {
            int index;
            if (!int.TryParse(columnIndex, out index))
            {
                throw new ArgumentOutOfRangeException();
            }

            return SoringColumnName<T>(index);
        }

        public static string SoringColumnName<T>(int index)
        {
            var props = typeof(T).GetProperties();
            foreach (var propertyInfo in props)
            {
                var propAttr =
                    propertyInfo
                        .GetCustomAttributes(false)
                        .OfType<SortingColumnAttribute>()
                        .FirstOrDefault();
                if (propAttr != null && propAttr.Index == index)
                {
                    return propertyInfo.Name;
                }
            }

            return "";
        }
    }
}
