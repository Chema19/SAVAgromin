using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using Agromin.SAV.Data;
using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;

namespace Agromin.SAV.Api.Controllers
{
    [RoutePrefix("SAV")]
    public class ProductApiController : BaseApiController
    {
        [HttpGet]
        [Route("products")]
        public IHttpActionResult ListProducts()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Product.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                      ProductId = x.ProductId,
                      Name = x.Name,
                      Creation_Date = x.Creation_Date,
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

        [HttpGet]
        [Route("products/{ProductId}")]
        public IHttpActionResult ViewProducts(Int32? ProductId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Product.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO && x.ProductId == ProductId).Select(x => new
                    {
                        ProductId = x.ProductId,
                        Name = x.Name,
                        Creation_Date = x.Creation_Date,
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
        [Route("products")]
        public IHttpActionResult AddProduct(ProductEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Product product = new Product();
                    if (!model.ProductId.HasValue)
                    {
                        context.Product.Add(product);
                        product.Status = ConstantHelpers.ESTADO.ACTIVO;
                        product.Creation_Date = DateTime.Now;
                    }

                    product.Name = model.Name;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Product added";
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
        [Route("products")]
        public IHttpActionResult EditProduct(ProductEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Product product = new Product();
                    if (model.ProductId.HasValue)
                    {
                        product = context.Product.FirstOrDefault(x => x.ProductId == model.ProductId);
                    }

                    product.Name = model.Name;
                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Product added";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("products/{Id}")]
        public IHttpActionResult DeleteProduct(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var Product = context.Product.FirstOrDefault(x => x.ProductId == Id);
                        Product.Status = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                        validacion = true;
                    }
                    ts.Complete();
                }
                response.Data = "Product deleted";
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
