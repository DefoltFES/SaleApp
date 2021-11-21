using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SaleApp
{
    class MaterialConvertor:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var list = value as ICollection<Production>;
                if (list.Count == 0)
                {
                    return "Материал: Нет";
                }
                string result = "Материал:";
                foreach (var item in list)
                {
                    var material = item as Production;
                    if (list.Last()==material)
                    { 
                        result += $"{material.Material.name}";
                        continue;
                    }
                    result+=$"{material.Material.name}, ";
                }
                return result;
            }
            return "Материал: Нет";
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
