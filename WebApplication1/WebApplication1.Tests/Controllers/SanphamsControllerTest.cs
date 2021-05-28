using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using WebApplication1.Models;
namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class SanphamsControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new SanphamsController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<Sanpham>;
            Assert.IsNotNull(model);

            var db = new CT25Team11Entities();
            Assert.AreEqual(db.Sanphams.Count() , model.Count);

        }
    }
}
