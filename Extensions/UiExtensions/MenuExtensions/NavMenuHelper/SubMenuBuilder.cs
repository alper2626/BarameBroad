using System;
using System.Collections.Generic;

namespace Extensions.UiExtensions.MenuExtensions.NavMenuHelper
{
    public class SubMenuBuilder
    {
        private SubMenuItemModel MenuItemSubMenu;
        public SubMenuBuilder()
        {
            MenuItemSubMenu = new SubMenuItemModel();
        }
        public SubMenuBuilder Text(string text)
        {
            MenuItemSubMenu.Text = text;
            return this;
        }
          public SubMenuBuilder OrderIndex(int i)
        {
            MenuItemSubMenu.OrderIndex = i;
            return this;
        }

        public SubMenuBuilder Icon(string icon)
        {
            MenuItemSubMenu.Icon = icon;
            return this;
        }

        public SubMenuBuilder MenuItems(Action<List<MenuItemModel>> MenuItems)
        {
            MenuItems(MenuItemSubMenu.MenuItemList);
            return this;
        }
        public SubMenuBuilder SubMenuItems(Action<List<SubMenuItemModel>> SubMenuItems)
        {
            SubMenuItems(MenuItemSubMenu.SubMenuList);
            return this;
        }
        public SubMenuItemModel Build()
        {
            return MenuItemSubMenu;
        }
    }
}
