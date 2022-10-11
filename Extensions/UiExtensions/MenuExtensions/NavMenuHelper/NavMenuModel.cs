namespace Extensions.UiExtensions.MenuExtensions.NavMenuHelper
{
    public class MenuItemModel
    {
        
        /*
         <li class="menu-item">
                <a href="/" class="menu-link">
                    <i class="menu-icon flaticon-home"></i>
                    <span class="menu-text">Ana aaSayfa</span>
                </a>
            </li>
         */

        public string Href { get; set; } 

        public string Icon { get; set; } = "fas fa-angle-double-right";
        public string Text { get; set; }

        public int OrderIndex { get; set; } = 0;

        public override string ToString()
        {
            
            return $@"
                <li class=""menu-item"">
                    <a href=""{Href}"" class=""menu-link"">
                        <i class=""menu-icon {Icon}""></i>
                        <span class=""menu-text"">{Text}</span>
                    </a>
                </li>
            ";
        }
    }
    public enum NavMenuTypes
    {
        MenuItem,
        MenuItemSubMenu,
    }
}
