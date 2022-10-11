namespace Extensions.UiExtensions.MenuExtensions.NavMenuHelper
{
    public class MenuItemBuilder
    {
        private MenuItemModel MenuItem;
        public MenuItemBuilder()
        {
            MenuItem = new MenuItemModel();
        }

        public MenuItemBuilder Text(string text)
        {
            MenuItem.Text = text;
            return this;
        }

        public MenuItemBuilder Index(int index)
        {
            MenuItem.OrderIndex = index;
            return this;
        }

        public MenuItemBuilder Href(string href)
        {
            MenuItem.Href= href;
            return this;
        }
        public MenuItemBuilder Icon(string icon)
        {
            MenuItem.Icon = icon;
            return this;
        }

        public MenuItemModel Build()
        {
            return MenuItem;
        }
    }
}
