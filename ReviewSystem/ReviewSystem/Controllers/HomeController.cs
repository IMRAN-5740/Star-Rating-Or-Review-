using ReviewSystem.Models;
using ReviewSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _reviewSystemEntities;
        public HomeController()
        {
            _reviewSystemEntities =new ApplicationDbContext();
        }
        // GET: Home
        public ActionResult Index()
        {
            List<ArticleViewModel> listOfArticle = new List<ArticleViewModel>();
            listOfArticle = (from article in _reviewSystemEntities.Articles
                             select new ArticleViewModel()
                             {
                                 ArticleId = article.ArticleId,
                                 ArticleName=article.ArticleName,
                                 ArticleDescription=article.ArticleDescription
                             }).ToList();
            return View(listOfArticle);
        }

        public ActionResult ShowComment(int articleId)
        {
            List<CommentViewModel> listOfComments = new List<CommentViewModel>();
            listOfComments = (from comment in _reviewSystemEntities.Comments
                              where comment.ArticleId == articleId
                              select new CommentViewModel()
                              {
                                  ArticleId = comment.ArticleId,
                                  CommentId = comment.CommentId,
                                  CommentDescription = comment.CommentDescription,
                                  Rating = comment.Rating,
                                  CommentedOn = comment.CommentedOn

                              }).ToList();
            
            ViewBag.ArticleId = articleId;
            return View(listOfComments);
        }
        [HttpPost]
        public ActionResult AddComment(int articleId, int rating, string articleComment)
        {
            Comment listComment = new Comment();
            listComment.ArticleId = articleId;
            listComment.Rating = rating;
            listComment.CommentDescription = articleComment;
            listComment.CommentedOn = DateTime.Now;
            listComment.Rating = rating;
            listComment.GuestId = Guid.NewGuid();
            _reviewSystemEntities.Comments.Add(listComment);
            _reviewSystemEntities.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}