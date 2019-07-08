using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.Brand
{
    public class ListBrandViewModel
    {
        public List<BrandEntity> LstBrand { set; get; }
        
        public async System.Threading.Tasks.Task Fill(CargarDatosContext dataContext)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var result = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/brands");
            LstBrand = JsonConvert.DeserializeObject<List<BrandEntity>>(result.Data.ToString());
        }
    }
}