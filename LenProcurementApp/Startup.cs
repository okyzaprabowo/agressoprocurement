using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LenProcurementApp.Startup))]

namespace LenProcurementApp
{
    /// <summary>
    /// generate otomatis
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="app">app</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
