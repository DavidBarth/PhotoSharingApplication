using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;
using PhotoSharingApplication.Models;
using PhotoSharingApplication.Controllers;
using System.Linq;
using PhotoSharingTests.Doubles;

namespace PhotoSharingTests
{
    //using the FakePhotoSharingContext to create tests in isolation
    [TestClass]
    public class PhotoControllerTests
    {
        [TestMethod]
        public void Test_Index_Return_View()
        {
            //create context, pass to constructor
            var context = new FakePhotoSharingContext();
            var controller = new PhotoController(context);
        }

        //create photo objects
        [TestMethod]
        public void Test_PhotoGallery_ModelType()
        {
            var context = new FakePhotoSharingContext();
            context.Photos = new[] { 
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo()}.AsQueryable();

            var controller = new PhotoController(context);
            
        }

        //image types
        [TestMethod]
        public void Test_GetImage_Return_Type()
        {
            var context = new FakePhotoSharingContext();
            var controller = new PhotoController(context);
            
            context.Photos = new[] {
                new Photo{ PhotoID= 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo{ PhotoID= 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo{ PhotoID= 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo{ PhotoID= 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo{ PhotoID= 5, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"}}.AsQueryable();

        }
    }
}
