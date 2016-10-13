using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;
using PhotoSharingApplication.Models;
using PhotoSharingApplication.Controllers;

namespace PhotoSharingTests
{
    [TestClass]
    public class PhotoControllerTests
    {
        [TestMethod]
        public void Test_Index_Return_View()
        {
            PhotoController controller = new PhotoController();

            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_PhotoGallery_ModelType()
        {
            var controller = new PhotoController();
            var result = controller._PhotoGallery() as PartialViewResult;
            Assert.AreEqual(typeof(List<Photo>), result.GetType());
        }

        [TestMethod]
        public void Test_GetImage_Return_Type()
        {
            var controller = new PhotoController();
            var result = controller.GetImage(1) as ActionResult;
            Assert.AreEqual(typeof(FileContentResult), result.GetType());
        }
    }
}
