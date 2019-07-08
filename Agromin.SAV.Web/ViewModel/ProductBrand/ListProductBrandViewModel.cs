using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.ProductBrand
{
    public class ListProductBrandViewModel
    {
        public List<ProductBrandEntity> LstProductBrand { set; get; } = new List<ProductBrandEntity>();

        public async System.Threading.Tasks.Task Fill(CargarDatosContext dataContext)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var result = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/productbrands");
            LstProductBrand = JsonConvert.DeserializeObject<List<ProductBrandEntity>>(result.Data.ToString());
        }
    }
}