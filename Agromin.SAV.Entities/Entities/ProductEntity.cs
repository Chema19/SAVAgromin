using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class ProductEntity
    {
        public Int32? ProductId { set; get; }
        public String Name { set; get; }
        public String Status { set; get; }
        public DateTime? Creation_Date { set; get; }
    }
}
