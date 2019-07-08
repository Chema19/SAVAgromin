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
    public class BrandApiController : BaseApiController
    {
        [HttpGet]
        [Route("brands")]
        public IHttpActionResult ListBrands()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Brand.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        BrandId = x.BrandId,
                        Name = x.Name,
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
        [Route("brands/{BrandId}")]
        public IHttpActionResult ViewBrands(Int32? BrandId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Brand.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO && x.BrandId == BrandId).Select(x => new
                    {
                        BrandId = x.BrandId,
                        Name = x.Name,
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
        [Route("brands")]
        public IHttpActionResult AddBrand(BrandEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Brand product = new Brand();
                    if (!model.BrandId.HasValue)
                    {
                        context.Brand.Add(product);
                        product.Status = ConstantHelpers.ESTADO.ACTIVO;
                    }

                    product.Name = model.Name;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Brand added";
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
        [Route("brands")]
        public IHttpActionResult EditBrand(BrandEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Brand product = new Brand();
                    if (model.BrandId.HasValue)
                    {
                        product = context.Brand.FirstOrDefault(x => x.BrandId == model.BrandId);
                    }

                    product.Name = model.Name;
                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Brand added";
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
        [Route("brands/{Id}")]
        public IHttpActionResult DeleteBrand(Int32? Id) {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var Brand = context.Brand.FirstOrDefault(x => x.BrandId == Id);
                        Brand.Status = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                        validacion = true;
                    }
                    ts.Complete();
                }
                response.Data = "Brand deleted";
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
