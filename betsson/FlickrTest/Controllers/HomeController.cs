using System.Web.Mvc;
using FlickrTest.Repository;

namespace FlickrTest.Controllers {
	/// <summary>
	/// The home controller
	/// </summary>
	public class HomeController : Controller {
		private readonly IRepository flickrRepository;

		public HomeController(IRepository repository) {
			flickrRepository = repository;
		}

		/// <summary>
		/// GET: /Home/
		/// </summary>
		/// <returns>Returns an action result containing the view</returns>
		public ActionResult Index() {
			return View();
		}

		/// <summary>Get SEO-friendly version or "as link".</summary>
		/// <param name="tags">Tags to search for.</param>
		/// <returns>View with static Html.</returns>
		[HttpGet]
		public ActionResult Index(string tags) {
			if (null == tags) {
				return View();
			}
			return View(flickrRepository.GetImagesByTags(tags));
		}

		/// <summary>
		/// Gets images from repository
		/// </summary>
		/// <param name="tags">Tags that should be searched for in the repository</param>
		/// <returns>A Json object containing the images from the repository</returns>
		[HttpPost]
		public ActionResult GetImages(string tags) {
			return Json(flickrRepository.GetImagesByTags(tags), JsonRequestBehavior.DenyGet);
		}

	}
}
