using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using Extensions.UiExtensions.MenuExtensions.NavMenuHelper;

namespace BarameBroad.WebUI
{
    [HtmlTargetElement("navbar-menu")]
    public class NavMenuTagHelper : TagHelper
    {
        [HtmlAttributeName("table-id")]
        public string Id { get; set; }
        StringBuilder sb;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            sb = new StringBuilder();
            sb.AppendLine(@"<!--begin::Aside Menu-->");
            sb.AppendLine(@"<div class=""kt-aside-menu-wrapper kt-grid__item kt-grid__item--fluid"" id=""kt_aside_menu_wrapper"">");
            sb.AppendLine(@"<!--begin::Menu Container-->");
            sb.AppendLine(@"<div id=""kt_aside_menu"" class=""aside-menu"" data-menu-vertical=""1"" data-menu-scroll=""1"" data-menu-dropdown-timeout=""500"">");
            sb.AppendLine(@"<!--begin::Menu Nav-->");
            sb.AppendLine(@"<ul class=""menu-nav"">");
            foreach (var item in MenuStatics.SubMenuItemModelList.OrderBy(c=>c.OrderIndex))
            {
                sb.AppendLine(item.ToString());
            }
            sb.AppendLine(@"</ul>");
            sb.AppendLine(@"<!--end::Menu Nav-->");
            sb.AppendLine(@"</div>");
            sb.AppendLine(@"<!--end::Menu Container-->");
            sb.AppendLine(@"</div>");
            sb.AppendLine(@"<!--end::Aside Menu-->");
            output.Content.SetHtmlContent(sb.ToString());
            base.Process(context, output);
        }
    }
}