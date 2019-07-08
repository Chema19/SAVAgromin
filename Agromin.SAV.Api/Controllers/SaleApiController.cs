using Agromin.SAV.Data;
using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace Agromin.SAV.Api.Controllers
{
    [RoutePrefix("SAV")]
    public class SaleApiController : BaseApiController
    {
        [HttpGet]
        [Route("sales")]
        public IHttpActionResult ListSales()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Sale.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        SaleId = x.SaleId,
                        General_Price = x.General_Price,
                        CustomerId = x.CustomerId,
                        FullName = x.Customer.Names + " " + x.Customer.Last_Names,
                        Creation_Date = x.Creation_Date,
                        UserId = x.UserId,
                        LocalId = x.LocalId,
                        LocalName = x.Local.Name,
                        Update_Date = x.Update_Date,
                        CodeVoucher = x.CodeVoucher,
                        StatusPayment = x.StatusPayment,
                        StatusDelivery = x.StatusDelivery,
                        Status = x.Status,
                    }).ToList();

                    response.Error = false;
                    response.Message = "Success";
                    ts.Complete();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        [Route("sales")]
        public IHttpActionResult AddSale(SaleEntity model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    Sale sale = new Sale();
                    if (!model.SaleId.HasValue)
                    {
                        context.Sale.Add(sale);
                        sale.Status = ConstantHelpers.ESTADO.ACTIVO;
                        sale.StatusPayment = ConstantHelpers.ESTADO.PREVENTA;
                        sale.StatusDelivery = ConstantHelpers.ESTADO.NOENTREGADO;
                        sale.Creation_Date = DateTime.Now;
                    }

                    sale.Update_Date = DateTime.Now;
                    sale.LocalId = model.LocalId;
                    sale.UserId = model.UserId;
                    sale.CodeVoucher = model.CodeVoucher;
                    sale.CustomerId = model.CustomerId;
                    context.SaveChanges();

                    Decimal? totalGeneral = 0;
                    foreach (var item in model.LstSaleDetail)
                    {

                        SaleDetail saleDetail = new SaleDetail();
                        context.SaleDetail.Add(saleDetail);
                        saleDetail.SaleId = sale.SaleId;
                        saleDetail.ProductBrandId = item.ProductBrandId;
                        saleDetail.Amount = item.Amount;
                        saleDetail.Price = item.Price;
                        saleDetail.Total = saleDetail.Price * saleDetail.Amount;
                        saleDetail.Status = ConstantHelpers.ESTADO.ACTIVO;

                        EntryExitProduct entryExitProduct = new EntryExitProduct();
                        context.EntryExitProduct.Add(entryExitProduct);
                        entryExitProduct.Amount = item.Amount;
                        entryExitProduct.Creation_Date = DateTime.Now;
                        entryExitProduct.StatusType = ConstantHelpers.ESTADO.SALIDA;
                        entryExitProduct.UserId = model.UserId;
                        entryExitProduct.ProductBrandId = item.ProductBrandId;
                        entryExitProduct.SaleId = sale.SaleId;

                        StockProduct stockProduct = new StockProduct();
                        stockProduct = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == item.ProductBrandId);
                        stockProduct.Amount -= item.Amount;

                        totalGeneral += saleDetail.Total;
                    }
                    sale.General_Price = totalGeneral;
                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "User added";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("sales")]
        public IHttpActionResult EditSale(SaleEntity model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    Sale sale = new Sale();
                    if (model.SaleId.HasValue)
                    {
                        sale = context.Sale.FirstOrDefault(x => x.SaleId == model.SaleId);
                        var saleDetailEdit = context.SaleDetail.Where(x => x.SaleId == sale.SaleId).ToList();
                        var entryExitProductEdit = context.EntryExitProduct.Where(x => x.SaleId == sale.SaleId).ToList();
                        foreach (var item in saleDetailEdit)
                        {
                            var stockProductEdit = new StockProduct();
                            stockProductEdit = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == item.ProductBrandId);
                            stockProductEdit.Amount = stockProductEdit.Amount + item.Amount;
                        }
                        saleDetailEdit.ForEach(x => context.SaleDetail.Remove(x));
                        entryExitProductEdit.ForEach(x => context.EntryExitProduct.Remove(x));
                    }

                    sale.Update_Date = DateTime.Now;
                    sale.LocalId = model.LocalId;
                    sale.UserId = model.UserId;
                    sale.CodeVoucher = model.CodeVoucher;
                    sale.CustomerId = model.CustomerId;
                    context.SaveChanges();

                    Decimal? totalGeneral = 0;
                    foreach (var item in model.LstSaleDetail)
                    {

                        SaleDetail saleDetail = new SaleDetail();
                        context.SaleDetail.Add(saleDetail);
                        saleDetail.SaleId = sale.SaleId;
                        saleDetail.ProductBrandId = item.ProductBrandId;
                        saleDetail.Amount = item.Amount;
                        saleDetail.Price = item.Price;
                        saleDetail.Total = saleDetail.Price * saleDetail.Amount;
                        saleDetail.Status = ConstantHelpers.ESTADO.ACTIVO;

                        EntryExitProduct entryExitProduct = new EntryExitProduct();
                        context.EntryExitProduct.Add(entryExitProduct);
                        entryExitProduct.Amount = item.Amount;
                        entryExitProduct.Creation_Date = DateTime.Now;
                        entryExitProduct.StatusType = ConstantHelpers.ESTADO.SALIDA;
                        entryExitProduct.UserId = model.UserId;
                        entryExitProduct.ProductBrandId = item.ProductBrandId;
                        entryExitProduct.SaleId = sale.SaleId;

                        StockProduct stockProduct = new StockProduct();
                        stockProduct = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == item.ProductBrandId);
                        stockProduct.Amount -= item.Amount;

                        totalGeneral += saleDetail.Total;
                    }
                    sale.General_Price = totalGeneral;
                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "User edited";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("confirmpayment/{SaleId}")]
        public IHttpActionResult ConfirmPaymentSale(Int32? SaleId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (SaleId.HasValue)
                    {
                        var sale = context.Sale.FirstOrDefault(x => x.SaleId == SaleId);
                        sale.StatusPayment = ConstantHelpers.ESTADO.CANCELADO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "successfull change";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("confirmdelivery/{SaleId}")]
        public IHttpActionResult ConfirmDeliverySale(Int32? SaleId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (SaleId.HasValue)
                    {
                        var sale = context.Sale.FirstOrDefault(x => x.SaleId == SaleId);
                        sale.StatusDelivery = ConstantHelpers.ESTADO.ENTREGADO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "successfull change";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }
    }
}
