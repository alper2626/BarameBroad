using Extensions.UiExtensions.MenuExtensions.NavMenuHelper;

namespace BarameBroad.WebUI.NavMenuHelper
{
    public class SiteConfigNavMenuBuild : INavMenuBuild
    {
        public SubMenuItemModel SubMenuItemModel { get; set; }

        public SiteConfigNavMenuBuild()
        {
            SubMenuItemModel =
                new SubMenuBuilder().Text("Site Ayarları").Icon("flaticon-cogwheel").MenuItems(menuItems =>
              {
                  menuItems.Add(new MenuItemBuilder().Text("Site Bilgileri").Index(0).Href("/SiteConfig/SiteConfig/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Personel Bilgileri").Index(1).Href("/SiteConfig/Team/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Sosyal Medya Hesapları").Index(2).Href("/SiteConfig/SocialNetwork/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Referanslar").Index(3).Href("/SiteConfig/Reference/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("Kullanıcı Yorumları").Index(4).Href("/SiteConfig/UserComment/Index").Build());
                  menuItems.Add(new MenuItemBuilder().Text("S.s.s").Index(5).Href("/SiteConfig/Sss/Index").Build());
              }).OrderIndex(9).Build();
        }
    }


    public class ImageNavMenuBuild : INavMenuBuild
    {
        public SubMenuItemModel SubMenuItemModel { get; set; }

        public ImageNavMenuBuild()
        {
            SubMenuItemModel =
                new SubMenuBuilder().Text("Resim İçerikleri").Icon("flaticon2-list-3").MenuItems(menuItems =>
                {
                    menuItems.Add(new MenuItemBuilder().Text("Logo").Index(0).Href("/SiteConfig/SiteConfig/UploadLogo").Build());
                    menuItems.Add(new MenuItemBuilder().Text("Footer Logo").Index(1).Href("/SiteConfig/SiteConfig/UploadFooterLogo").Build());
                    menuItems.Add(new MenuItemBuilder().Text("Fav Icon").Index(2).Href("/SiteConfig/SiteConfig/UploadFav").Build());
                }).OrderIndex(11).Build();
        }
    }

}
