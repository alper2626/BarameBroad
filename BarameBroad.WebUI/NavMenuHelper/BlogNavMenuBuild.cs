using System.Security.Policy;
using Extensions.UiExtensions.MenuExtensions.NavMenuHelper;

namespace BarameBroad.WebUI.NavMenuHelper
{
    public class NavMenuBuild : INavMenuBuild
    {
        public SubMenuItemModel SubMenuItemModel { get; set; }

        public NavMenuBuild()
        {
            SubMenuItemModel =
                new SubMenuBuilder().Text("Blog Modülü").Icon("flaticon2-list-2").MenuItems(menuItems =>
              {
                  menuItems.Add(new MenuItemBuilder().Text("Blog Kategorileri").Href("/Blog/BlogCategory/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Bloglar").Href("/Blog/Blog/Index").Build());

              }).OrderIndex(5).Build();
        }
    }
}
