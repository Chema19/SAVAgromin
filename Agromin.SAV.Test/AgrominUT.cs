using System;
using static NUnit.Framework.Assert;
using NUnit.Framework;
using Agromin.SAV.Helpers.Helpers;
using Agromin.SAV.Entities.Entities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Agromin.SAV.Test
{
    [TestClass]
    public class AgrominUT
    {
        String Baseurl = "http://64.202.186.215/AgrominApi/";


        [TestMethod]
        public void ListSaleTest()
        {
            try
            {
                List<SaleEntity> LstSale = new List<SaleEntity>();
                var result = ConstantHelpers.GetUrlAsync(Baseurl, "SAV/sales").Result;
                LstSale = JsonConvert.DeserializeObject<List<SaleEntity>>(result.Data.ToString());
                AreEqual(11, LstSale.Count);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void ListEntryExitTest()
        {
            try
            {
                List<EntryExitProductEntity> LstEntryExitProduct = new List<EntryExitProductEntity>();
                var result = ConstantHelpers.GetUrlAsync(Baseurl, "SAV/entryexitproducs/4").Result;
                LstEntryExitProduct = JsonConvert.DeserializeObject<List<EntryExitProductEntity>>(result.Data.ToString());
                AreEqual(15, LstEntryExitProduct.Count);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void ListProductBrandTest()
        {
            try
            {
                List<ProductBrandEntity> LstProductBrand = new List<ProductBrandEntity>();
                var result = ConstantHelpers.GetUrlAsync(Baseurl, "SAV/productbrands").Result;
                LstProductBrand = JsonConvert.DeserializeObject<List<ProductBrandEntity>>(result.Data.ToString());
                AreEqual(4, LstProductBrand.Count);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void AddSaleTest()
        {
            try
            {
                var listDetalle = new List<SaleDetailEntity>();
                listDetalle.Add(new SaleDetailEntity()
                {
                    Amount = 10,
                    ProductBrandId = 4,
                    Price = 3,
                });
                listDetalle.Add(new SaleDetailEntity()
                {
                    Amount = 5,
                    ProductBrandId = 5,
                    Price = 3,
                });
                var model = new SaleEntity()
                {
                    LocalId =1,
                    UserId = 12,
                    CodeVoucher = "CODPRUEBA1",
                    CustomerId = 3,
                    LstSaleDetail = listDetalle,
                };
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/sales", model).Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void AddBrandTest()
        {
            try
            {
                var model = new BrandEntity()
                {
                    Name = "PRUEBAS BRAND",
                };
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/brands", model).Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [TestMethod]
        public void AddProductTest()
        {
            try
            {
                var model = new ProductEntity()
                {
                    Name = "PRUEBAS PRODUCT"
                };
                var postResult = ConstantHelpers.PostUrlAsync(Baseurl, "SAV/products", model).Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void UpdateStatusConfirmPaymentTest()
        {
            try
            {
                var obj = new Object();
                var postResult = ConstantHelpers.PutUrlAsync(Baseurl, "SAV/confirmpayment/4126" , obj).Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void UpdateStatusConfirmDeliveryTest()
        {
            try
            {
                var obj = new Object();
                var postResult = ConstantHelpers.PutUrlAsync(Baseurl, "SAV/confirmdelivery/4126" , obj).Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void DeleteBrandTest()
        {
            try
            {
                var postResult = ConstantHelpers.DeleteUrlAsync(Baseurl, "SAV/brands/6").Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            try
            {
                var postResult = ConstantHelpers.DeleteUrlAsync(Baseurl, "SAV/products/4").Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod]
        public void DeleteProductBrandTest()
        {
            try
            {
                var postResult = ConstantHelpers.DeleteUrlAsync(Baseurl, "SAV/productbrands/5").Result;
                AreEqual("Success", postResult.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
