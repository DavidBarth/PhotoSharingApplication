using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    public class CommentController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        public CommentController()
        {
            context = new PhotoSharingContext();
        }

        public CommentController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: /Comment/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Comment comment = context.FindCommentById(id);
            ViewBag.PhotoID = comment.PhotoID;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = context.FindCommentById(id);
            context.Delete<Comment>(comment);
            context.SaveChanges();
            return RedirectToAction("Display", "Photo", new { id = comment.PhotoID });
        }

        [ChildActionOnly]
        public PartialViewResult _CommentsForPhoto(int PhotoId)
        {
            //select all comments in DB that have PhotoID equal to PhotoId
            var comments = from c in context.Comments
                           where c.PhotoID == PhotoId
                           select c;
            //savind PhotoID parameter in viewbag for later use in the view
            ViewBag.PhotoId = PhotoId;

            //return partialview that is in Views/Comment/_CommentsForPhoto
            return PartialView("_CommentsForPhoto", comments.ToList());
           
        }

        public PartialViewResult _Create(int PhotoId)
        {
            //creates comments with PHotoID set to PhotoId
            Comment comment = new Comment();
            comment.PhotoID = PhotoId;

            //savind PhotoID parameter in viewbag for later use in the view
            ViewBag.PhotoId = PhotoId;

            //returns _CreateAComment view from Views/Shared
            return PartialView("_CreateAComment");

        }

        [HttpPost]
        public PartialViewResult _CommentsForPhoto(Comment comment, int PhotoId)
        {
            context.Add<Comment>(comment);
            context.SaveChanges();

            var comments = from c in context.Comments
                           where c.PhotoID == PhotoId
                           select c;

            ViewBag.PhotoID = PhotoId;

            return PartialView("_CommentsForPhoto", comments.ToList());
        }

    }
}
