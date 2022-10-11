using Extensions.UiExtensions.MenuExtensions.NavMenuHelper;

namespace BarameBroad.WebUI.NavMenuHelper
{
    public class ServiceNavMenuBuild : INavMenuBuild
    {
        public SubMenuItemModel SubMenuItemModel { get; set; }

        public ServiceNavMenuBuild()
        {
            SubMenuItemModel =
                new SubMenuBuilder().Text("Program Modülü").Icon("flaticon-users-1").MenuItems(menuItems =>
              {
                  menuItems.Add(new MenuItemBuilder().Text("Programlar").Index(0).Href("/Program/Program/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Ülkeler").Index(1).Href("/Program/Country/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Şehirler").Index(2).Href("/Program/City/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Okullar").Index(3).Href("/Program/School/Index").Build());
              }).Build();
        }
    }
}
