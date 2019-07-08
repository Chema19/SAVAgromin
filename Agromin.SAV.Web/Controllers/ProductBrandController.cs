using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.ViewModel.ProductBrand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agromin.SAV.Web.Controllers
{
    public class ProductBrandController : BaseController
    {
        String Baseurl = ConstantHelpers.AddKey("urlbase");

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ListProductBrand()
        {
            var vm = new ListProductBrandViewModel();
            await vm.Fill(CargarDatosContext());
            return View(vm);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> AddProductBrand()
        {
            var vm = new AddEditProductBrandViewModel();
            await vm.FillAdd(CargarDatosContext());
            return View(vm);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddProductBrand(AddEditProductBrandViewModel model, FormCollection fc)
        {
            try
            {
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/productbrands", model).Result;
                if (postResult.Message.Equals("Success"))
                {
                    return RedirectToAction("ListProductBrand", "ProductBrand");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TryUpdateModel(model);
                return View(model);
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> DeleteProductBrand(Int32? Id)
        {
            var validacion = false;
            try
            {
                var postResult = ConstantHelpers.DeleteUrlAsync(Baseurl, "SAV/productbrands/" + Id.ToString()).Result;
                if (postResult.Message.Equals("Success"))
                {
                    validacion = true;
                    return Json(new { validacion });
                }
                return Json(new { });
            }
            catch (Exception ex)
            {
                return Json(new { });
            }
        }
    }
}