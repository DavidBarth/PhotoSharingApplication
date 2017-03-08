using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Globalization;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    [ValueReporter]
    public class PhotoController : Controller
    {
        private IPhotoSharingContext _context;

        public PhotoController()
        {
            _context = new PhotoSharingContext();
        }

        public PhotoController(IPhotoSharingContext Context)
        {
            _context = Context;
        }

       
        //
        // GET: /Photo/
        //10 minute caching duration, server location
        [OutputCache(Duration=600, Location=OutputCacheLocation.Server, VaryByParam="none")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;

            if (number == 0)
            {
                photos = _context.Photos.ToList();
            }
            else
            {
                photos = (from p in _context.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }

            return PartialView("_PhotoGallery", photos);
        }


        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = _context.FindPhotoByTitle(title);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }


        public ActionResult Display(int id)
        {
            Photo photo = _context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }

        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View("Create", photo);
            }
            else
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile
                        ,0
                        , image.ContentLength);
                }
                _context.Add<Photo>(photo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            Photo photo = _context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Delete", photo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = _context.FindPhotoById(id);
            _context.Delete<Photo>(photo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public FileContentResult GetImage(int id)
        {
            Photo photo = _context.FindPhotoById(id);
            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ViewResult SlideShow()
        {
            return View("SlideShow", _context.Photos.ToList());
        }


        //store and retrieve values in the session state
        //return a slideshow view that is populated with the favorite photos
        #region
        public ActionResult FavoritesSlideShow()
        {
            List<Photo> favPhotos = new List<Photo>();
            List<int> favoriteIds = Session["Favorites"] as List<int>;
            if(favoriteIds==null)
            {
                favoriteIds = new List<int>();    
            }
            Photo currentPhoto;

            foreach(int currentId in favoriteIds)
            {
                currentPhoto = _context.FindPhotoById(currentId);
                if (currentPhoto != null)
                {
                    favPhotos.Add(currentPhoto);
                }
            }

            return View("Slideshow", favPhotos);
        }


        //retrieve value from session state
        public ContentResult AddFavorite(int PhotoId)
        {
            List<int> favoriteIds = Session["Favorites"] as List<int>;

            if (favoriteIds == null)
            {
                favoriteIds = new List<int>();
            }

            favoriteIds.Add(PhotoId);

            return Content("The picture has been added to your favorites"
                ,"text/plain"
                ,System.Text.Encoding.Default);
        }

    #endregion
    }
}
