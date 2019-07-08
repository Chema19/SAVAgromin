using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.ViewModel.EntryExitProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agromin.SAV.Web.Controllers
{
    public class EntryExitProductController : BaseController
    {
        String Baseurl = ConstantHelpers.AddKey("urlbase");
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ListStockProduct()
        {
            var vm = new ListStockProductViewModel();
            await vm.Fill(CargarDatosContext());
            return View(vm);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ListEntryExitProduct(Int32? ProductBrandId)
        {
            var vm = new ListEntryExitProductViewModel();
            await vm.Fill(CargarDatosContext(), ProductBrandId);
            return View(vm);
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> AddEditEntryExitProduct()
        {
            var vm = new AddEditEntryExitProductViewModel();
            await vm.FillAdd(CargarDatosContext());
            return View(vm);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddEditEntryExitProduct(AddEditEntryExitProductViewModel model)
        {
            try
            {
                model.UserId = Session.GetUserId();
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/entryexitproducts", model).Result;
                if (postResult.Message.Equals("Success"))
                {
                    return RedirectToAction("ListStockProduct", "EntryExitProduct");
                }
                await model.FillAdd(CargarDatosContext());
                return View(model);
                
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
    }
}