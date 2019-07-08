using Agromin.SAV.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agromin.SAV.Web.Controllers;
using PagedList;
using Agromin.SAV.Entities.Entities;
using Newtonsoft.Json;

namespace Agromin.SAV.Web.ViewModel.Sale
{
    public class ListSaleViewModel
    {
        public Int32 p { set; get; }
        public List<SaleEntity> LstSale { set; get; }
        public async System.Threading.Tasks.Task Fill(CargarDatosContext dataContext)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var result = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/sales");
            LstSale = JsonConvert.DeserializeObject<List<SaleEntity>>(result.Data.ToString());
        }
    }
}