using FlickrTest.Models;

namespace FlickrTest.Repository
{
    /// <summary>
    /// The Repository interface
    /// </summary>
    public interface IRepository
    {
				SearchModel GetImagesByTags(string tags);
    }
}
