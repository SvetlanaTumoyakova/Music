using Microsoft.AspNetCore.Mvc;

namespace Music.Helper
{
    public static class ControllerHelper
    {
        public static string GetName<TController>() where TController : Controller
        {
            var type = typeof(TController);
            var nameController = type.Name;
            return nameController.Replace("Controller", "");
        }
    }
}
