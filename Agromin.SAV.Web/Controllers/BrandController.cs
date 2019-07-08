using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.ViewModel.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agromin.SAV.Web.Controllers
{
    public class BrandController : BaseController
    {
        String Baseurl = ConstantHelpers.AddKey("urlbase");
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ListBrand()
        {
            var vm = new ListBrandViewModel();
            await vm.Fill(CargarDatosContext());
            return View(vm);
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> AddBrand()
        {
            var vm = new AddEditBrandViewModel();
            return View(vm);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddBrand(AddEditBrandViewModel model)
        {
            try
            {
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/brands", model).Result;
                if (postResult.Message.Equals("Success"))
                {
                    return RedirectToAction("ListBrand", "Brand");
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
        public async System.Threading.Tasks.Task<JsonResult> DeleteBrand(Int32? Id)
        {
            var validacion = false;
            try
            {
                var postResult = ConstantHelpers.DeleteUrlAsync(Baseurl, "SAV/brands/" + Id.ToString()).Result;
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