using System.Collections.Generic;
using FlickrTest.Cache;
using FlickrTest.Models;

namespace FlickrTest.Repository {
	/// <summary>
	/// Acting as a cacheable repository for Flickr
	/// </summary>
	public class FlickrCacheableRepository : IRepository {
		readonly FlickrRepository flickrRepository;

		public FlickrCacheableRepository() {
			flickrRepository = new FlickrRepository();
		}

		/// <summary>
		/// Returns image based on the tag
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public SearchModel GetImagesByTags(string tags) {
			// Load the data from the cache if it exist
			bool fromCache;
			var model = CacheHelper<SearchModel>.Get(tags, () => flickrRepository.GetImagesByTags(tags), out fromCache);
			model.Cached = fromCache;
			return model;
		}
	}
}
