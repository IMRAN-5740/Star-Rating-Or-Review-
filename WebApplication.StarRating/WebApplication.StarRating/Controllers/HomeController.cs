using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.StarRating.Models;
using WebApplication.StarRating.ViewModel;

namespace WebApplication.StarRating.Controllers
{
    public class HomeController : Controller
    {
       private readonly RatingDBEntities _ratingDBEntities;

        public HomeController()
        {
            _ratingDBEntities=new RatingDBEntities();
        }
        public ActionResult Index()
        {
            List<ArticleViewModel> listOfArticle=new List<ArticleViewModel>();
            listOfArticle=(from article in _ratingDBEntities.Articles
                          select  new ArticleViewModel()
                          {
                              ArticleId=article.ArticleId,
                              ArticleName=article.ArticleName,
                              ArticleDescription=article.ArticleDescription

                          }).ToList();

            return View(listOfArticle);
        }

        public ActionResult ShowComment(int articleId)
        {
            List<CommentViewModel> listOfComment = new List<CommentViewModel>();
            listOfComment=(from comment in _ratingDBEntities.Comments
                           where comment.ArticleId==articleId
                           select new CommentViewModel()
                           {
                               ArticleId=comment.ArticleId,
                               CommentId=comment.CommentId,
                               CommentDescription=comment.CommentDescription,
                               Rating=comment.Rating,
                               CommmentedOn=comment.CommentedOn

                           }).ToList();
            ViewBag.ArticleId=articleId;    
            return View(listOfComment);
        }
        [HttpPost]
        public ActionResult AddComment(int articleId, int rating, string articleComment)
        {
            Comment listComment = new Comment();
            listComment.ArticleId = articleId;
            listComment.Rating = rating;
            listComment.CommentDescription=articleComment;
            listComment.CommentedOn = DateTime.Now;
            listComment.Rating = rating;
            listComment.GuestId = Guid.NewGuid();
            _ratingDBEntities.Comments.Add(listComment);
            _ratingDBEntities.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}