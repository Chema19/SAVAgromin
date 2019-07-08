using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class EntryExitProductEntity
    {
        public Int32? EntryExitProductId { set; get; }
        public Decimal? Amount { set; get; }
        public DateTime Creation_Date { set; get; }
        public String StatusType { set; get; }
        public Int32? UserId { set; get; }
        public Int32? ProductBrandId { set; get; }
        public String NameBrand { set; get; }
        public String NameProduct { set; get; }
        public String NameUser { set; get; }
        public Int32? SaleId { set; get; }
    }
}
