using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agromin.SAV.Web.Controllers
{
    public class ProductController : BaseController
    {
        String Baseurl = ConstantHelpers.AddKey("urlbase");

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ListProduct()
        {
            var vm = new ListProductViewModel();
            await vm.Fill(CargarDatosContext());
            return View(vm);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> AddProduct()
        {
            var vm = new AddEditProductViewModel();
            return View(vm);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddProduct(AddEditProductViewModel model, FormCollection fc)
        {
            try
            {
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/products", model).Result;
                if (postResult.Message.Equals("Success"))
                {
                    return RedirectToAction("ListProduct", "Product");
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
        public async System.Threading.Tasks.Task<JsonResult> DeleteProduct(Int32? Id)
        {
            var validacion = false;
            try
            {
                var postResult = ConstantHelpers.DeleteUrlAsync(Baseurl, "SAV/products/" + Id.ToString()).Result;
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