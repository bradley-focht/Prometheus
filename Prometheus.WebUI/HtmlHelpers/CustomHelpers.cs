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
		public static MvcHtmlString BreadrumbTrail(this HtmlHelper html, IList<KeyValuePair<string, string>> 
			links, string olCssClass, string activeLiCssClass)
		{

			TagBuilder list = new TagBuilder("ol");					//outermost list, assumes a css file to format properly
			list.AddCssClass(olCssClass);

			int i = 0;												//keep track of iterations to find active link
			foreach(KeyValuePair<string, string> link in links)		//add link items, start with / to clear the path
			{
				TagBuilder listItem = new TagBuilder("li");

				if (i == links.Count - 1)							//do not make the last a link
				{
					listItem.AddCssClass(activeLiCssClass);         //apply active css style
					listItem.InnerHtml += link.Key;
				}
				else
				{
					listItem.InnerHtml += "<a href=\"/" + link.Value + "\">" + link.Key + "</a>";
				}
				list.InnerHtml += listItem;							//add last item
				i++;
			}
			
			return new MvcHtmlString(list.ToString());
		}
	}
}