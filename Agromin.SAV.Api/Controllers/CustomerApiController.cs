using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using Agromin.SAV.Data;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Entities.Entities;
using System.Text.RegularExpressions;

namespace Agromin.SAV.Api.Controllers
{
    [RoutePrefix("SAV")]
    public class CustomerApiController : BaseApiController
    {
        [HttpGet]
        [Route("customers")]
        public IHttpActionResult ListCustomers()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Customer.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        CustomerId = x.CustomerId,
                        Names = x.Names,
                        Last_Names = x.Last_Names,
                        Sex = x.Sex,
                        Identity_Document = x.Identity_Document,
                        Email = x.Email,
                        Birthdate = x.Birthdate,
                        Creation_Date = x.Creation_Date,
                        Update_Date = x.Update_Date,
                        Status = x.Status,
                        DistrictId = x.DistrictId,
                        Phone = x.Phone
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
        [Route("customers")]
        public IHttpActionResult AddCustomer(CustomerEntity model)
        {
            try
            {
                if (model.Identity_Document.Length != 8)
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "Document need 8 characteres";
                    return Ok(response);
                }
                if (!email_bien_escrito(model.Email))
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "wrong Email";
                    return Ok(response);
                }

                using (var ts = new TransactionScope())
                {

                    Customer customer = new Customer();
                    if (!model.CustomerId.HasValue)
                    {
                        context.Customer.Add(customer);
                        customer.Status = ConstantHelpers.ESTADO.ACTIVO;
                        customer.Creation_Date = DateTime.Now;
                        customer.Update_Date = DateTime.Now;
                    }

                    customer.Names = model.Names;
                    customer.Last_Names = model.Last_Names;
                    customer.Sex = model.Sex;
                    customer.Identity_Document = model.Identity_Document;
                    customer.Email = model.Email;
                    customer.Birthdate = model.Birthdate;
                    customer.DistrictId = model.DistrictId;
                    customer.Phone = model.Phone;

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
        [Route("customers")]
        public IHttpActionResult EditCustomer(CustomerEntity model)
        {
            try
            {
                if (model.Identity_Document.Length != 8)
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "Document need 8 characteres";
                    return Ok(response);
                }
                if (!email_bien_escrito(model.Email))
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "wrong Email";
                    return Ok(response);
                }

                using (var ts = new TransactionScope())
                {

                    Customer customer = new Customer();
                    if (model.CustomerId.HasValue)
                    {
                        customer = context.Customer.FirstOrDefault(x => x.CustomerId == model.CustomerId);
                        customer.Update_Date = DateTime.Now;
                    }

                    customer.Names = model.Names;
                    customer.Last_Names = model.Last_Names;
                    customer.Sex = model.Sex;
                    customer.Identity_Document = model.Identity_Document;
                    customer.Email = model.Email;
                    customer.Birthdate = model.Birthdate;
                    customer.DistrictId = model.DistrictId;
                    customer.Phone = model.Phone;

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

        public Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
