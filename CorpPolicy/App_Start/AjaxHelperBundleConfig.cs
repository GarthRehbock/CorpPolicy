using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(CorpPolicy.App_Start.AjaxHelperBundleConfig), "RegisterBundles")]

namespace CorpPolicy.App_Start
{
	public class AjaxHelperBundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/ajaxhelper").Include("~/Scripts/jquery.ajaxhelper.js"));
		}
	}
}