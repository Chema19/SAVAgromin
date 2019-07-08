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
    public class LocalApiController : BaseApiController
    {
        [HttpGet]
        [Route("locals")]
        public IHttpActionResult ListLocals()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Local.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        LocalId = x.LocalId,
                        Name = x.Name,
                        Status = x.Status,
                        DistrictId = x.DistrictId,
                        Phone = x.Phone,
                        Address = x.Address
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
        [Route("locals/{localid}")]
        public IHttpActionResult ListLocals(Int32? LocalId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Local.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO && x.LocalId == LocalId).Select(x => new
                    {
                        LocalId = x.LocalId,
                        Name = x.Name,
                        Status = x.Status,
                        DistrictId = x.DistrictId,
                        Phone = x.Phone,
                        Address = x.Address
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
        [Route("locals")]
        public IHttpActionResult AddLocal(LocalEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Local local = new Local();
                    if (!model.LocalId.HasValue)
                    {
                        context.Local.Add(local);
                        local.Status = ConstantHelpers.ESTADO.ACTIVO;
                    }

                    local.Name = model.Name;
                    local.Address = model.Address;
                    local.DistrictId = model.DistrictId;
                    local.Phone = model.Phone;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Local added";
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
        [Route("locals")]
        public IHttpActionResult EditLocal(LocalEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Local local = new Local();
                    if (model.LocalId.HasValue)
                    {
                        local = context.Local.FirstOrDefault(x => x.LocalId == model.LocalId);
                    }

                    local.Name = model.Name;
                    local.Address = model.Address;
                    local.DistrictId = model.DistrictId;
                    local.Phone = model.Phone;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Local added";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}
