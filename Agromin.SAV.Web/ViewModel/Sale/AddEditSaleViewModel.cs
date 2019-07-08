using Agromin.SAV.Entities.Entities;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agromin.SAV.Web.ViewModel.Sale
{
    public class AddEditSaleViewModel
    {
        public Int32? SaleId { set; get; }

        [Display(Name = "Local : ")]
        public Int32 LocalId { set; get; }
        public List<LocalEntity> LstLocal { set; get; } 

        [Display(Name = "Usuario : ")]
        public Int32? UserId { set; get; }
        public List<UserEntity> LstUser { set; get; }

        [Display(Name = "Cliente : ")]
        public Int32? CustomerId { set; get; }
        public List<CustomerEntity> LstCustomer { set; get; }

        [Display(Name = "Producto : ")]
        public Int32? ProductBrandId { set; get; }
        public List<ProductBrandEntity> LstProductBrand { set; get; }

        [Display(Name = "Numero Comprobante : ")]
        public String CodeVoucher { set; get; }

        public List<SaleDetailEntity> LstSaleDetail { set; get; } 

        public AddEditSaleViewModel()
        {
            LstCustomer = new List<CustomerEntity>();
            LstSaleDetail = new List<SaleDetailEntity>();
            LstLocal = new List<LocalEntity>();
            LstUser = new List<UserEntity>();
            LstProductBrand = new List<ProductBrandEntity>();
        }

        public async System.Threading.Tasks.Task FillAdd(CargarDatosContext dataContext) {

            var localId = dataContext.session.GetLocalId();
            var userId = dataContext.session.GetUserId();
            
            String Baseurl = ConstantHelpers.AddKey("urlbase");

            LstSaleDetail = new List<SaleDetailEntity>();
            
            var resultLocal = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/locals");
            LstLocal = JsonConvert.DeserializeObject<List<LocalEntity>>(resultLocal.Data.ToString());

            var resultUser = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/users");
            LstUser = JsonConvert.DeserializeObject<List<UserEntity>>(resultUser.Data.ToString());
            
            var resultSale = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/sales");
            var LstSale = JsonConvert.DeserializeObject<List<SaleEntity>>(resultSale.Data.ToString());

            var resultCustomer = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/customers");
            LstCustomer = JsonConvert.DeserializeObject<List<CustomerEntity>>(resultCustomer.Data.ToString());

            var resultProductBrand = await ConstantHelpers.GetUrlAsync(Baseurl, "SAV/productbrands");
            LstProductBrand = JsonConvert.DeserializeObject<List<ProductBrandEntity>>(resultProductBrand.Data.ToString());


            CodeVoucher = NumberVoucher(LstSale);
        }

        public void FillEdit(CargarDatosContext dataContext, Int32? saleId)
        {
            //this.SaleId = saleId;
            //Sagromin.Models.Sale sale = dataContext.context.Sale.FirstOrDefault(x => x.SaleId == saleId);
            //LstSaleDetail = dataContext.context.SaleDetail.Where(x => x.SaleId == saleId).ToList();
            //CodeVoucher = sale.CodeVoucher;
            //NameLocal = sale.Local.Name;
            //NameUser = sale.User.Names;
            //CustomerId = sale.CustomerId;
            //CustomerIdHidden = sale.CustomerId;
        }

        public String NumberVoucher(List<SaleEntity> listSale)
        {
            String code = "N°";
            SaleEntity lastSale = listSale.OrderByDescending(x => x.SaleId).FirstOrDefault();
            if (listSale.Count() > 0)
            {
                Int32 numberVoucher = listSale.Count() + 1;
                for (int i = numberVoucher.ToString().Length; i < 8; i++)
                {
                    code = code + "0";
                }
                code = code + numberVoucher.ToString();
            }
            else
            {
                code = code + "00000001";
            }
            return code;
        }
    }
}