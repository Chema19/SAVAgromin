using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.EntryExitProduct
{
    public class AddEditEntryExitProductViewModel
    {
        public Int32? EntryExitProductId { set; get; }
        [Display(Name = "Cantidad de producto : ")]
        [Required(ErrorMessage = "El campo cantidad de producto es requerido")]
        public Decimal Amount { set; get; }
        [Display(Name = "Producto : ")]
        [Required(ErrorMessage = "El campo producto es requerido")]
        public Int32? ProductBrandId { set; get; }
        public List<ProductBrandEntity> LstProductBrand { set; get; }
        public Int32? UserId { set; get; }

        public AddEditEntryExitProductViewModel()
        {
            LstProductBrand = new List<ProductBrandEntity>();
        }

        public async System.Threading.Tasks.Task FillAdd(CargarDatosContext dataContext)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var resultProductBrand = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/productbrands");
            LstProductBrand = JsonConvert.DeserializeObject<List<ProductBrandEntity>>(resultProductBrand.Data.ToString());
        }
    }
}