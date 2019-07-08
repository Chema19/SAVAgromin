using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using Agromin.SAV.Entities.Entities;

namespace Agromin.SAV.Api.Controllers
{
    [RoutePrefix("SAV")]
    public class ProductBrandApiController : BaseApiController
    {
        [HttpGet]
        [Route("productbrands")]
        public IHttpActionResult ListProductBrand()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.ProductBrand.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        ProductBrandId = x.ProductBrandId,
                        ProductId = x.ProductId,
                        NameProduct = x.Product.Name,
                        BrandId = x.BrandId,
                        NameBrand = x.Brand.Name,
                        NameProductBrand = x.Product.Name + " - " + x.Brand.Name,
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
        [Route("productbrands")]
        public IHttpActionResult AddProductBrand(ProductBrandEntity model)
        {
            try
            {
                var existe = context.ProductBrand.FirstOrDefault(x => x.ProductId == model.ProductId && x.BrandId == model.BrandId && x.Status == ConstantHelpers.ESTADO.ACTIVO);

                using (var ts = new TransactionScope())
                {

                    ProductBrand ProductBrand = new ProductBrand();
                    if (model.ProductBrandId.HasValue)
                    {
                        ProductBrand = context.ProductBrand.FirstOrDefault(x => x.ProductBrandId == model.ProductBrandId);
                    }
                    else
                    {
                        if (existe != null)
                        {
                            response.Data = "Marca producto ya registrado";
                            response.Error = true;
                            response.Message = "Error";
                            return Content(HttpStatusCode.Conflict, response);
                        }

                        context.ProductBrand.Add(ProductBrand);
                        ProductBrand.Status = ConstantHelpers.ESTADO.ACTIVO;
                    }

                    ProductBrand.BrandId = model.BrandId;
                    ProductBrand.ProductId = model.ProductId;

                    context.SaveChanges();

                    //CREAR STOCK PRODUCTO

                    if (!model.ProductBrandId.HasValue)
                    {
                        StockProduct stockProduct = new StockProduct();
                        context.StockProduct.Add(stockProduct);
                        stockProduct.Amount = 0;
                        stockProduct.ProductBrandId = ProductBrand.ProductBrandId;
                        stockProduct.Status = ConstantHelpers.ESTADO.ACTIVO;
                        context.SaveChanges();
                    }

                    ts.Complete();
                }
                response.Data = "Product brand added";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }

        }

        [HttpDelete]
        [Route("productbrands/{Id}")]
        public IHttpActionResult DeleteProductBrand(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var StockProduct = context.StockProduct.FirstOrDefault(x => x.ProductBrandId == Id);

                        if (StockProduct.Amount == 0)
                        {
                            var ProductBrand = context.ProductBrand.FirstOrDefault(x => x.ProductBrandId == Id);
                            ProductBrand.Status = ConstantHelpers.ESTADO.INACTIVO;
                            //INACTIVO STOCK
                            StockProduct.Status = ConstantHelpers.ESTADO.INACTIVO;
                            context.SaveChanges();
                            validacion = true;
                        }
                        else
                        {
                            validacion = false;
                        }
                    }
                    ts.Complete();
                }
                response.Data = "Brand Product deleted";
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
