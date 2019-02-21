using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using FlickrTest.Repository;

namespace FlickrTest.App_Start {
	public class ControllerFactory : DefaultControllerFactory {
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
			// Get config name
			string repositoryTypeName = ConfigurationManager.AppSettings["RepositoryTypeName"];
			// Get type, if not configured - default type.
			Type type = Type.GetType(repositoryTypeName ?? "FlickrTest.Repository.FlickrRepository");
			object repository = null;
			if (null != type) {
				repository = Activator.CreateInstance(type) as IRepository;
			}
			// Fallback - config value/default is not of correct type.
			if (null == repository) {
				repository = new FlickrRepository();
			}
			IController controller = Activator.CreateInstance(controllerType, new[] { repository }) as Controller;
			return controller;
		}
	}
}