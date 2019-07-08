using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Agromin.SAV.Helpers.Helpers
{
    public static class ConstantHelpers
    {

        public static string AddKey(String name)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(name);
        }

        public static async Task<ResultRequestEntity> GetUrlAsync(String BaseUrl, String RestUrl) {
            try
            {
                String resultContent = String.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(RestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        resultContent = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return JsonConvert.DeserializeObject<ResultRequestEntity>(resultContent);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<ResultRequestEntity> PostUrlAsync(String BaseUrl, String RestUrl, Object Content)
        {
            try
            {
                String resultContent = String.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync(RestUrl, Content).Result;
                    resultContent = response.Content.ReadAsStringAsync().Result;
                }
                return JsonConvert.DeserializeObject<ResultRequestEntity>(resultContent);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<ResultRequestEntity> PutUrlAsync(String BaseUrl, String RestUrl, Object Data)
        {
            try
            {
                String resultContent = String.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PutAsJsonAsync(RestUrl, Data).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        resultContent = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return JsonConvert.DeserializeObject<ResultRequestEntity>(resultContent);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<ResultRequestEntity> DeleteUrlAsync(String BaseUrl, String RestUrl)
        {
            try
            {
                String resultContent = String.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.DeleteAsync(RestUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        resultContent = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return JsonConvert.DeserializeObject<ResultRequestEntity>(resultContent);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static class ESTADO
        {
            public const string ACTIVO = "ACT";
            public const string INACTIVO = "INA";
            public const string ENTRADA = "ENT";
            public const string SALIDA = "SAL";
            public const string PREVENTA = "PRE";
            public const string NOENTREGADO = "NEN";
            public const string CANCELADO = "CAN";
            public const string ENTREGADO = "ETG";

            public static string GetNameEstado(string Estado)
            {
                switch (Estado)
                {
                    case ACTIVO:
                        return "ACTIVO";
                    case INACTIVO:
                        return "INACTIVO";
                    case ENTRADA:
                        return "ENTRADA";
                    case SALIDA:
                        return "SALIDA";
                    case PREVENTA:
                        return "PREVENTA";
                    case NOENTREGADO:
                        return "NOENTREGADO";
                    case CANCELADO:
                        return "CANCELADO";
                    case ENTREGADO:
                        return "ENTREGADO";
                }

                return string.Empty;
            }
            public static string GetLabelEstado(string Estado)
            {
                switch (Estado)
                {
                    case ACTIVO:
                        return "<label class='badge badge-success'>ACTIVO</label>";
                    case INACTIVO:
                        return "<label class='badge badge-danger'>INACTIVO</label>";
                    case ENTRADA:
                        return "<label class='badge badge-primary'>ENTRADA</label>";
                    case SALIDA:
                        return "<label class='badge badge-primary'>SALIDA</label>";
                    case PREVENTA:
                        return "<label class='badge badge-primary'>PREVENTA</label>";
                    case NOENTREGADO:
                        return "<label class='badge badge-primary'>NO ENTREGADO</label>";
                    case CANCELADO:
                        return "<label class='badge badge-primary'>CANCELADO</label>";
                    case ENTREGADO:
                        return "<label class='badge badge-primary'>ENTREGADO</label>";
                }
                return string.Empty;
            }
        }

        public class ResultRequestEntity
        {
            public Object Data { set; get; }
            public Boolean Error { set; get; }
            public String Message { set; get; }
        }
    }
}
