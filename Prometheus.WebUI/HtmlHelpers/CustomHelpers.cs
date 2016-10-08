using System.Collections.Generic;
using System.Web.Mvc;

/// <summary>
///	Custom HTML Helpers
/// </summary>
namespace Prometheus.WebUI.HtmlHelpers
{
	/// <summary>
	/// Home of the HTMHelpers
	/// </summary>
	public static class CustomHelpers
	{
		/// <summary>
		/// Makes a breadcrumb trail of links
		/// KVP needs to be in the form of text : link
		/// </summary>
		/// <param name=""></param>
		/// <param name="links"></param>
		/// <param name="cssClass"></param>
		/// <returns></returns>
		public static MvcHtmlString BreadrumbTrail(this HtmlHelper html, IEnumerable<KeyValuePair<string, string>> 
			links, int activeLinkId, string CssClass, string activeLiCssClass, string containerCss)
		{
			//build an outer, set, div size
			TagBuilder container = new TagBuilder("div");
			container.AddCssClass(containerCss);

			TagBuilder list = new TagBuilder("ol");
			list.AddCssClass(CssClass);

			int i = 0;												//keep track of iterations to find active link
			foreach(KeyValuePair<string, string> link in links)		//add link items, start with / to clear the path
			{
				TagBuilder listItem = new TagBuilder("li");

				if (i == activeLinkId)
					listItem.AddCssClass(activeLiCssClass);

				listItem.InnerHtml +="<a href=\"/" + link.Value + "\">" + link.Key + "</a>";
				list.InnerHtml += listItem;
				container.InnerHtml += list;
				i++;
			}
			container.InnerHtml += list;

			return new MvcHtmlString(list.ToString());
		}
	}
}