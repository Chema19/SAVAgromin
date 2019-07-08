﻿using Agromin.SAV.Web.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.Brand
{
    public class AddEditBrandViewModel
    {
        public Int32? BrandId { set; get; }

        [Display(Name = "Nombres : ")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public String Name { set; get; }

        public void Fill(CargarDatosContext datosContext, Int32? BrandId)
        {
            this.BrandId = BrandId;
            if (this.BrandId.HasValue)
            {
                //var Brand = datosContext.context.Brand.FirstOrDefault(x => x.BrandId == this.BrandId);
                //this.Name = Brand.Name;
            }
        }
    }
}