using HelperKit.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agromin.SAV.Web.Controllers
{
    public class BaseController : HelperController
    {
        private CargarDatosContext cargarDatosContext;
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        public override void InvalidateContext()
        {
            throw new NotImplementedException();
        }

        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { session = Session };
            }

            return cargarDatosContext;
        }

    }
    public class CargarDatosContext
    {
        public HttpSessionStateBase session { get; set; }
    }
}