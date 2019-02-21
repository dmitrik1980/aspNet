using System;
using System.Web;

namespace FlickrTest.Cache {
	public static class CacheHelper<T> {
		/// <summary>
		/// Simple cache helper
		/// </summary>
		/// <param name="key">The cache key used to reference the item.</param>
		/// <param name="function">The underlying method that referes to the object to be stored in the cache.</param>
		/// <param name="fromCache">If true, then cached version is used.</param>
		/// <returns>The item</returns>
		public static T Get(string key, Func<T> function, out bool fromCache) {
			fromCache = true;
			// Empty param => nothing.
			if (null == key) {
				fromCache = false;
				return default(T);
			}
			var obj = (T)HttpContext.Current.Cache[key];
			// Found? Return.
			if (!Equals(obj, default(T))) {
				return obj;
			}
			fromCache = false;
			obj = function.Invoke();
			HttpContext.Current.Cache.Add(key, obj, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
			return obj;
		}
	}
}