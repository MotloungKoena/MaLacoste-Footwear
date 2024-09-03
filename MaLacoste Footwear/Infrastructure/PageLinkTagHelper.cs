using MaLacoste_Footwear.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Cryptography.Xml;

namespace MaLacoste_Footwear.Infrastructure
{
    [HtmlTargetElement("div", Attributes ="page-model")] //  <div page-model="" ...></div>
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        //html attribute : page-model
        public PagingInfoViewModel PageModel { get; set; }

        //html attribute : page-action
        public string PageAction { get; set; }
        //html attribute: page-classes-enabled
        public bool PageClassesEnabled { get; set; }
        //html attribute: page-class
        public string PageClass { get; set; }
        //html attribute: page-class-normal
        public string PageClassNormal { get; set; }
        //html attribute: page-class-selected
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new("div");
            for (int i = 1; i <= PageModel.TotalPages; i++) 
            {
                TagBuilder tag = new("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                if( PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
