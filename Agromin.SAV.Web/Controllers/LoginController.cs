using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Newtonsoft.Json;

namespace Agromin.SAV.Web.Controllers
{
    public class LoginController : BaseController
    {

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginEntity model)
        {
            try
            {
                String Baseurl = ConstantHelpers.AddKey("urlbase");
                using (var client = new HttpClient())
                {
                  
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Credential", model.Credential),
                        new KeyValuePair<string, string>("Password", model.Password)
                    });
                    var result = await client.PostAsync("SAV/login", content);
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    var obj = JsonConvert.DeserializeObject<ResultRequestEntity>(resultContent);

                    var user = JsonConvert.DeserializeObject<List<UserEntity>>(obj.Data.ToString());

                    Session.Set(SessionKey.UserId, user[0].UserId);
                    Session.Set(SessionKey.FullName, user[0].Names + " " + user[0].Last_Names);
                    Session.Set(SessionKey.LocalId, user[0].LocalId);

                    if (!obj.Error) {
                        return RedirectToAction("ListSale", "Sale");
                    }
                    PostMessage(HelperKit.Mvc.MessageType.Danger, obj.Data.ToString());
                    return View(model);
                }
            }
            catch (Exception ex) {
                TryUpdateModel(model);
                return View(model);
            }
        }
    }
}