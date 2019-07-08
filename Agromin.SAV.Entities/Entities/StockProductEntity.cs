using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class StockProductEntity
    {
        public Int32? StockProductId { set; get; }
        public String NameProduct { set; get; }
        public String NameBrand { set; get; }
        public Decimal? Amount { set; get; }
        public Int32? ProductBrandId { set; get; }
        public String Status { set; get; }
    }
}
