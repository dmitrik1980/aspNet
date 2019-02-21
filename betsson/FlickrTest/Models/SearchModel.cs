using System.Collections.Generic;

namespace FlickrTest.Models {

	/// <summary>Simple model for search output.</summary>
	public class SearchModel {
		/// <summary>Determines, if response is cached.</summary>
		public bool Cached;
		/// <summary>Preset tags, if any.</summary>
		public string Tags;
		/// <summary>List of items found.</summary>
		public List<FlickrImage> Images;
	}
}