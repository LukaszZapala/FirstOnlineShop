using AlkusStore.Models.ViewModels;
using System;
using System.Text;
using System.Web.Mvc;

namespace AlkusStore.HtmlHelpers
{
    public static class PaginHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {           
            var innerHtml = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages ; i++)
            {
                var aTag = new TagBuilder("a");
                aTag.MergeAttribute("href", pageUrl(i));
                aTag.InnerHtml = i.ToString();

                var liTag = new TagBuilder("li");
                liTag.AddCssClass(i == pagingInfo.CurrentPage ? "active" : "");
                liTag.InnerHtml = aTag.ToString();

                innerHtml.Append(liTag.ToString());
            }

            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            ulTag.InnerHtml = innerHtml.ToString();

            return MvcHtmlString.Create(ulTag.ToString());
        }
    }
}
