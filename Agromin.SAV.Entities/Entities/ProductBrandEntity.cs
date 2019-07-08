using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class ProductBrandEntity
    {
        public Int32? ProductBrandId { set; get; }
        public Int32? ProductId { set; get; }
        public String NameProduct { set; get; }
        public String NameBrand { set; get; }
        public String NameProductBrand { set; get; }
        public Int32? BrandId { set; get; }
        public String Status { set; get; }
    }
}
