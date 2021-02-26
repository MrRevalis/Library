using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder links = new StringBuilder();

            for(int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selectedPage");
                }
                tag.AddCssClass("pageLinks");

                links.Append(tag.ToString());
            }
            return MvcHtmlString.Create(links.ToString());
        }
    }
}