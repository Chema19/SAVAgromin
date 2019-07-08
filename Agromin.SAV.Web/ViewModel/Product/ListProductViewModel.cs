using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.Product
{
    public class ListProductViewModel
    {
        public List<ProductEntity> LstProduct { set; get; } = new List<ProductEntity>();

        public async System.Threading.Tasks.Task Fill(CargarDatosContext dataContext)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var result = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/products");
            LstProduct = JsonConvert.DeserializeObject<List<ProductEntity>>(result.Data.ToString());
        }
    }
}