using Agromin.SAV.Data;
using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.Http;

namespace Agromin.SAV.Api.Controllers
{
    [RoutePrefix("SAV")]
    public class UserApiController : BaseApiController
    {
        [HttpGet]
        [Route("users")]
        public IHttpActionResult ListUsers()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.User.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        UserId = x.UserId,
                        Names = x.Names,
                        Credential = x.Credential,
                        Password = x.Password,
                        Last_Names = x.Last_Names,
                        Sex = x.Sex,
                        Identity_Document = x.Identity_Document,
                        Email = x.Email,
                        Birthdate = x.Birthdate,
                        Creation_date = x.Creation_date,
                        Update_date = x.Update_date,
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

        [HttpGet]
        [Route("users/{userid}")]
        public IHttpActionResult ViewUsers(Int32? UserId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.User.Where(x => x.Status == ConstantHelpers.ESTADO.ACTIVO && x.UserId == UserId).Select(x => new
                    {
                        UserId = x.UserId,
                        Names = x.Names,
                        Credential = x.Credential,
                        Password = x.Password,
                        Last_Names = x.Last_Names,
                        Sex = x.Sex,
                        Identity_Document = x.Identity_Document,
                        Email = x.Email,
                        Birthdate = x.Birthdate,
                        Creation_date = x.Creation_date,
                        Update_date = x.Update_date,
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
        [Route("users")]
        public IHttpActionResult AddUser(UserEntity model)
        {
            try
            {
                var dniExiste = context.User.FirstOrDefault(x => x.Identity_Document == model.Identity_Document);

                if (model.Identity_Document.Length != 8)
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "Document need 8 characteres";
                    return Content(HttpStatusCode.Conflict, response);
                }
                if (!email_bien_escrito(model.Email))
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "Document need 8 characteres";
                    return Content(HttpStatusCode.Conflict, response);
                }


                using (var ts = new TransactionScope())
                {

                    User user = new User();
                    if (!model.UserId.HasValue)
                    {
                        if (dniExiste != null)
                        {
                            response.Data = "failed";
                            response.Error = false;
                            response.Message = "Document Identity is necesary";
                            return Content(HttpStatusCode.Conflict, response);
                        }

                        context.User.Add(user);
                        user.Status = ConstantHelpers.ESTADO.ACTIVO;
                        user.Creation_date = DateTime.Now;
                        user.Update_date = DateTime.Now;
                    }

                    user.Names = model.Names;
                    user.Last_Names = model.Last_Names;
                    user.Credential = model.Credential;
                    user.Password = model.Password;
                    user.Sex = model.Sex;
                    user.Identity_Document = model.Identity_Document;
                    user.Email = model.Email;
                    user.Birthdate = model.Birthdate;
                    user.LocalId = model.LocalId.Value;
                    user.DistrictId = model.DistrictId;
                    user.Master = "";
                    user.Phone = model.Phone;

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
        [Route("users")]
        public IHttpActionResult EditUser(UserEntity model)
        {
            try
            {
                var dniExiste = context.User.FirstOrDefault(x => x.Identity_Document == model.Identity_Document);

                if (model.Identity_Document.Length != 8)
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "Document need 8 characteres";
                    return Content(HttpStatusCode.Conflict, response);
                }
                if (!email_bien_escrito(model.Email))
                {
                    response.Data = "failed";
                    response.Error = false;
                    response.Message = "Document need 8 characteres";
                    return Content(HttpStatusCode.Conflict, response);
                }


                using (var ts = new TransactionScope())
                {

                    User user = new User();
                    if (model.UserId.HasValue)
                    {
                        user = context.User.FirstOrDefault(x => x.UserId == model.UserId);
                        user.Update_date = DateTime.Now;
                    }

                    user.Names = model.Names;
                    user.Last_Names = model.Last_Names;
                    user.Credential = model.Credential;
                    user.Password = model.Password;
                    user.Sex = model.Sex;
                    user.Identity_Document = model.Identity_Document;
                    user.Email = model.Email;
                    user.Birthdate = model.Birthdate;
                    user.LocalId = model.LocalId.Value;
                    user.DistrictId = model.DistrictId;
                    user.Master = "";
                    user.Phone = model.Phone;

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
