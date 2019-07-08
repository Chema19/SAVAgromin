using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.EntryExitProduct
{
    public class ListEntryExitProductViewModel
    {
        public Int32 p { set; get; }
        public List<EntryExitProductEntity> LstEntryExitProduct { set; get; }
        public async System.Threading.Tasks.Task Fill(CargarDatosContext dataContext, Int32? productBrandId)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var result = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/entryexitproducs/" + productBrandId.ToString());
            LstEntryExitProduct = JsonConvert.DeserializeObject<List<EntryExitProductEntity>>(result.Data.ToString());
        }
    }
}