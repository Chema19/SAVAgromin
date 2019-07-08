using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.ViewModel.Sale;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Agromin.SAV.Web.Controllers
{
    public class SaleController : BaseController
    {
        String Baseurl = ConstantHelpers.AddKey("urlbase");

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ListSale()
        {
            var vm = new ListSaleViewModel();
            await vm.Fill(CargarDatosContext());
            return View(vm);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> AddEditSale()
        {
            var vm = new AddEditSaleViewModel();
            await vm.FillAdd(CargarDatosContext());
            return View(vm);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddEditSale(AddEditSaleViewModel model, FormCollection fc)
        {
            try {
                foreach (var key in fc.AllKeys.Where(x => x.StartsWith("product-")).ToList())
                {
                    var num = key.Split('-');
                    var keyProduct = "product-" + num[1];
                    var keyQuantity = "quantity-" + num[1];
                    var keyUnitPrice = "unitprice-" + num[1];

                    model.LstSaleDetail.Add(new SaleDetailEntity() {
                        ProductBrandId = Convert.ToInt32(fc[keyProduct]),
                        Amount = Convert.ToDecimal(fc[keyQuantity]),
                        Price = Convert.ToDecimal(fc[keyUnitPrice]),
                        Total = Convert.ToDecimal(fc[keyUnitPrice]) * Convert.ToDecimal(fc[keyQuantity]),
                    });
                }

                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/sales", model).Result;
                if (postResult.Message.Equals("Success"))
                {
                    return RedirectToAction("ListSale", "Sale");
                }
                await model.FillAdd(CargarDatosContext());
                return View(model);
            } catch (Exception ex) {
                TryUpdateModel(model);
                await model.FillAdd(CargarDatosContext());
                return View(model);
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> ConfirmPaymentSale(Int32? Id)
        {
            var validacion = false;
            try
            {
                var obj = new Object();
                var postResult = ConstantHelpers.PutUrlAsync(Baseurl, "SAV/confirmpayment/"+Id.ToString(), obj).Result;
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

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> ConfirmDeliverySale(Int32? Id)
        {
            var validacion = false;
            try
            {
                var obj = new Object();
                var postResult = ConstantHelpers.PutUrlAsync(Baseurl, "SAV/confirmdelivery/" + Id.ToString(), obj).Result;
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