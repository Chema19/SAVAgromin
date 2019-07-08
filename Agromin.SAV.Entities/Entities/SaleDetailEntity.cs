using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class SaleDetailEntity
    {
        public Int32? SaleDetailId { set; get; }
        public Decimal? Amount { set; get; }
        public Decimal? Price { set; get; }
        public String Status { set; get; }
        public Int32? SaleId { set; get; }
        public Int32? ProductBrandId { set; get; }
        public Decimal? Total { set; get; }
    }
}
