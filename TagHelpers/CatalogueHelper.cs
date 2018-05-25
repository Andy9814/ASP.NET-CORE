using Microsoft.AspNetCore.Http;
using System.Text;
using ASPNETExercises.Models;
using Newtonsoft.Json;
using ASPNETExercises.Utils;using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace ASPNETExercises.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = CategoryIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private const string CategoryIdAttribute = "category";
        [HtmlAttributeName(CategoryIdAttribute)]
        public string CategoryId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.Get<MenuItemViewModel[]>("menu") != null && Convert.ToInt32(CategoryId) > 0)
            {
                var innerHtml = new StringBuilder();
                MenuItemViewModel[] menu = _session.Get<MenuItemViewModel[]>("menu");
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:x-large;\"><span>Catalogue</span></div>");
                foreach (MenuItemViewModel item in menu)
                {
                    if (item.CategoryId == Convert.ToInt32(CategoryId))
                    {
                        // remove double apostrophe
                        item.Description = item.Description.Contains("''") ? item.Description.Replace("''", "") : item.Description;
                        item.JsonData = JsonConvert.SerializeObject(item);
                        innerHtml.Append("<div class=\"col-sm-3 col-xs-12 text-center\" style=\"border:solid;\">");
                        innerHtml.Append("<span class=\"col-xs-12\"><img src=\"/images/burger.jpg\" /></span>");
                        innerHtml.Append("<p><span style=\"font-size:large;\">" + item.Description.Substring(0, 10) + "...</span></p><div>");
                        innerHtml.Append("<span>For Nutritional Info.<br />Click Details</span></div>");
                        innerHtml.Append("<div style=\"padding-bottom: 10px;\"><a href=\"#details_popup\" data-toggle=\"modal\" class=\"btn btn-default\"");
                        innerHtml.Append(" id=\"modalbtn" + @item.Id + "\" data-id=\"" + @item.Id + "\" data-details='" + @item.JsonData + "'>Details</a></div></div>");
                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }
        }


    }
}

