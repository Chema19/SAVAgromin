using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agromin.SAV.Entities.Entities
{
    public class LocalEntity
    {
        public Int32? LocalId { set; get; }
        public Int32? DistrictId { set; get; }
        public String Status { set; get; }
        public String Name { set; get; }
        public String Phone { set; get; }
        public String Address { set; get; }
    }
}
