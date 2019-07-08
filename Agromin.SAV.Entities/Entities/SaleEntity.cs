using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class SaleEntity
    {
        public Int32? SaleId { set; get; }
        public String General_Price { set; get; }
        public Int32? CustomerId { set; get; }
        public String FullName { set; get; }
        public DateTime? Creation_Date { set; get; }
        public Int32? UserId { set; get; }
        public Int32? LocalId { set; get; }
        public String LocalName { set; get; }
        public DateTime? Update_Date { set; get; }
        public String CodeVoucher { set; get; }
        public String StatusPayment { set; get; }
        public String StatusDelivery { set; get; }
        public String Status { set; get; }
        public List<SaleDetailEntity> LstSaleDetail { set; get; } = new List<SaleDetailEntity>();
    }
}
