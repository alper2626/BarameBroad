using BaseEntities.Database;
using Core.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BarameBroad.WebUI.Controllers
{
    [Route("yorum")]
    public class UserCommentController : Controller
    {
        private IEntityRepositoryBase<BaseEntities.Database.UserComment> _userComment;

        public UserCommentController(IEntityRepositoryBase<BaseEntities.Database.UserComment> userComment)
        {
            _userComment = userComment;
        }

        [Route("anasayfa")]
        public IActionResult UserCommentsPartial()
        {

            return PartialView("Partials/_UserCommentsPartial", _userComment.GetList().ToList());

        }
    }
}
