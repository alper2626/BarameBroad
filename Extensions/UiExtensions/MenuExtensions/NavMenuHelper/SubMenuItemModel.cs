using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions.UiExtensions.MenuExtensions.NavMenuHelper
{
    public class SubMenuItemModel
    {
        /*
          <li class="menu-item menu-item-submenu" aria-haspopup="true" data-menu-toggle="hover">
                <a href="javascript:;" class="menu-link menu-toggle">
                    <i class="menu-icon flaticon-web"></i>
                    <span class="menu-text">İşletme</span>
                    <i class="menu-arrow"></i>
                </a>
                <div class="menu-submenu">
                    <i class="menu-arrow"></i>
                    <ul class="menu-subnav">  
                        ******Items*****
                    </ul>
                </div>
            </li>
         */
        public int OrderIndex { get; set; }
        public SubMenuItemModel()
        {
            SubMenuList = new List<SubMenuItemModel>();
            MenuItemList = new List<MenuItemModel>();
      
        }    
        public string Icon { get; set; }
        public string Text { get; set; } = "flaticon-circle";
        public List<SubMenuItemModel> SubMenuList { get; set; }
        public List<MenuItemModel> MenuItemList { get; set; }
        private string GetSubMenuListStr()
        {
            var SubMenuListStr = new StringBuilder();
            foreach (var item in SubMenuList)
            {
                SubMenuListStr.AppendLine(item.ToString());
            }
            return SubMenuListStr.ToString();
        } 
        private string GetMenuItemListStr()
        {
            var MenuItemListStr = new StringBuilder();
            foreach (var item in MenuItemList.OrderBy(w=>w.OrderIndex))
            {
                MenuItemListStr.AppendLine(item.ToString());
            }
            return MenuItemListStr.ToString();
        }
        public override string ToString()
        {
           

            return $@" <li class=""menu-item menu-item-submenu"" aria-haspopup=""true"" data-menu-toggle=""hover"">
                <a href=""javascript:;"" class=""menu-link menu-toggle"">
                    <i class=""menu-icon {Icon}""></i>
                    <span class=""menu-text"">{Text}</span>
                    <i class=""menu-arrow""></i>
                </a>
                <div class=""menu-submenu"">
                    <i class=""menu-arrow""></i>
                    <ul class=""menu-subnav"">  
                        {GetSubMenuListStr()}
                        {GetMenuItemListStr()}
                    </ul>
                </div>
            </li>
            ";
        }
    }
}
