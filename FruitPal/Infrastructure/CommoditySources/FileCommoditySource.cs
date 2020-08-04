using System.IO;
using System.Reflection;

namespace FruitPal
{
    public class FileCommoditySource : ICommoditySource
    {
        public string GetCommodityFromSource()
        {
            return File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "commoditydata.json"));
        }
    }
}
