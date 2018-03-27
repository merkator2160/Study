using GameStore.WebUI.Models;
using System;
using System.Text;
using System.Web.Mvc;


namespace GameStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<Int32, String> pageUrl)
        {
            var result = new StringBuilder();

            for (Int32 i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}