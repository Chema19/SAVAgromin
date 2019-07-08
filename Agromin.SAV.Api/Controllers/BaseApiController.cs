using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Agromin.SAV.Data;

namespace Agromin.SAV.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        protected AgrominSAVEntities context { set; get; } = new AgrominSAVEntities();
        private CargarDatosContext cargarDatosContext { set; get; }
        public ResultRequest response { set; get; }

        public BaseApiController()
        {
            //context = new AgrominSAVEntities();
            response = new ResultRequest();
        }

        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { context = context, response = response };
            }

            return cargarDatosContext;
        }
    }

    public class CargarDatosContext
    {
        public AgrominSAVEntities context { get; set; }
        public ResultRequest response { set; get; }
    }

    public class ResultRequest
    {
        public Object Data { set; get; }
        public Boolean Error { set; get; }
        public String Message { set; get; }
    }
}
