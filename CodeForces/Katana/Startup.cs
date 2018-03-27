using Katana.Components;
using Owin;

namespace Katana
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseApiComponent();
            app.UseTestComponentOne();
            app.UseTestComponentTwo();
        }
    }
}