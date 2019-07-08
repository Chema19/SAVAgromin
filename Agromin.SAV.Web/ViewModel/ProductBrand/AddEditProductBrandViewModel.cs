using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.ProductBrand
{
    public class AddEditProductBrandViewModel
    {
        public Int32? ProductBrandId { set; get; }

        [Display(Name = "Precio : ")]
        [Required(ErrorMessage = "El campo precio es requerido")]
        public Decimal? Price { set; get; }

        [Display(Name = "Producto : ")]
        [Required(ErrorMessage = "El campo producto es requerido")]
        public Int32? ProductId { set; get; }
        public List<ProductEntity> LstProduct { set; get; }

        [Display(Name = "Marca : ")]
        [Required(ErrorMessage = "El campo marca es requerido")]
        public Int32? BrandId { set; get; }
        public List<BrandEntity> LstBrand { set; get; }


        public AddEditProductBrandViewModel()
        {
            LstProduct = new List<ProductEntity>();
            LstBrand = new List<BrandEntity>();
        }

        public async System.Threading.Tasks.Task FillAdd(CargarDatosContext datosContext)
        {
            String Baseurl = ConstantHelpers.AddKey("urlbase");
            var resultProduct = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/products");
            LstProduct = JsonConvert.DeserializeObject<List<ProductEntity>>(resultProduct.Data.ToString());
            
            var resultBrand = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/brands");
            LstBrand = JsonConvert.DeserializeObject<List<BrandEntity>>(resultBrand.Data.ToString());
        }
    }
}