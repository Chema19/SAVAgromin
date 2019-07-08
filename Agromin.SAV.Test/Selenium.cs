using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Agromin.SAV.Data;
using System.Linq;

namespace Agromin.SAV.Test
{
    [TestClass]
    public class Selenium
    {
        IWebDriver driver = new ChromeDriver();
        public AgrominSAVEntities context { set; get; } = new AgrominSAVEntities();


        [TestMethod]
        public void IniciarSessionSelenium()
        {
            var user = context.User.FirstOrDefault();

            driver.Navigate().GoToUrl("http://64.202.186.215/AgrominWeb/");
            var username = driver.FindElement(By.Id("Credential"));
            var password = driver.FindElement(By.Id("Password"));
            var button = driver.FindElement(By.ClassName("btn-block"));

            username.SendKeys(user.Credential);
            password.SendKeys(user.Password);
            button.Submit();

            driver.Close();
        }

        [TestMethod]
        public void ConfirmSendSaleSelenium()
        {
            driver.Navigate().GoToUrl("http://64.202.186.215/AgrominWeb/Sale/ListSale");         
            var button = driver.FindElement(By.Id("4120"));
            button.Click();
            var buttonAccept = driver.FindElement(By.ClassName("swal2-confirm"));
            buttonAccept.Click();
            driver.Close();
        }

        [TestMethod]
        public void ConfirmPaySaleSelenium()
        {
            driver.Navigate().GoToUrl("http://64.202.186.215/AgrominWeb/Sale/ListSale");
            var button = driver.FindElement(By.Id("4120"));
            button.Click();
            var buttonAccept = driver.FindElement(By.ClassName("swal2-confirm"));
            buttonAccept.Click();
            driver.Close();
        }


        [TestMethod]
        public void DeleteProductSelenium()
        {
            var user = context.User.FirstOrDefault();

            driver.Navigate().GoToUrl("http://64.202.186.215/AgrominWeb/");
            var username = driver.FindElement(By.Id("Credential"));
            var password = driver.FindElement(By.Id("Password"));
            var buttonlogin = driver.FindElement(By.ClassName("btn-block"));

            username.SendKeys(user.Credential);
            password.SendKeys(user.Password);
            buttonlogin.Submit();


            var buttonProductBrand = driver.FindElement(By.Id("btnp"));
            buttonProductBrand.Click();
            
            var button = driver.FindElement(By.Id("8"));
            button.Click();
            var buttonAccept = driver.FindElement(By.ClassName("swal2-confirm"));
            buttonAccept.Click();
            driver.Close();
        }

        [TestMethod]
        public void AddProductBrandSelenium()
        {
            var user = context.User.FirstOrDefault();

            driver.Navigate().GoToUrl("http://64.202.186.215/AgrominWeb/");
            var username = driver.FindElement(By.Id("Credential"));
            var password = driver.FindElement(By.Id("Password"));
            var buttonlogin = driver.FindElement(By.ClassName("btn-block"));

            username.SendKeys(user.Credential);
            password.SendKeys(user.Password);
            buttonlogin.Submit();

            var buttonProductBrand = driver.FindElement(By.Id("btnpb"));
            buttonProductBrand.Click();

            var button = driver.FindElement(By.ClassName("btn-success"));
            button.Click();

            var product = driver.FindElement(By.Id("ProductId"));
            var brand = driver.FindElement(By.Id("BrandId"));
            var buttonSubmit = driver.FindElement(By.ClassName("btn-xs"));

            product.SendKeys("S-BRASSA");
            brand.SendKeys("INTI");
            buttonSubmit.Submit();

            driver.Close();
        }

        [TestMethod]
        public void AddSaleSelenium()
        {
            var user = context.User.FirstOrDefault();

            driver.Navigate().GoToUrl("http://64.202.186.215/AgrominWeb/");
            var username = driver.FindElement(By.Id("Credential"));
            var password = driver.FindElement(By.Id("Password"));
            var buttonlogin = driver.FindElement(By.ClassName("btn-block"));

            username.SendKeys(user.Credential);
            password.SendKeys(user.Password);
            buttonlogin.Submit();

            var button = driver.FindElement(By.ClassName("btn-success"));
            button.Click();

            var local = driver.FindElement(By.Id("LocalId"));
            var usuario = driver.FindElement(By.Id("UserId"));
            var cliente = driver.FindElement(By.Id("CustomerId"));
            var producto = driver.FindElement(By.Name("product-1"));
            var cantidad = driver.FindElement(By.Name("quantity-1"));
            var preciounitario = driver.FindElement(By.Name ("unitprice-1"));

            var buttonSubmit = driver.FindElement(By.ClassName("btn-success"));

            local.SendKeys("LOCAL LA QUEBRADA");
            usuario.SendKeys("Miguel Alberto");
            cliente.SendKeys("Josemaria Alonso ");
            producto.SendKeys("FLOZINA - MOLINO");
            cantidad.SendKeys("3");
            preciounitario.SendKeys("4.5");

            buttonSubmit.Submit();

            driver.Close();
        }
    }
}
