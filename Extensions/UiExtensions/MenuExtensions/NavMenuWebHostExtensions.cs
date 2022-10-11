using Microsoft.Extensions.DependencyInjection;
using System;
using Extensions.UiExtensions.MenuExtensions.NavMenuHelper;

namespace Extensions.UiExtensions.MenuExtensions
{
    public static class NavMenuWebHostExtensions
    {
        public static void RunWithTasks(this IServiceProvider webHost)
        {
            try
            {
                // Load all tasks from DI
                var tasks = webHost.GetServices<INavMenuBuild>();
                // Execute all the tasks
                MenuStatics.SubMenuItemModelList = new System.Collections.Generic.List<SubMenuItemModel>();
                foreach (var task in tasks)
                {
                    MenuStatics.SubMenuItemModelList.Add(task.SubMenuItemModel);
                }
            }
            catch
            {
                throw;
            }



        }
    }
}
