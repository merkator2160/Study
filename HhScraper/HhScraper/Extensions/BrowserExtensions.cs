using System;
using System.Reflection;
using System.Windows.Controls;

namespace HhScraper.Extensions
{
    public static partial class BrowserExtensions
    {
        /// <summary>
        /// It makes IE silent (no java script errors)
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="silent"></param>
        public static void SuppressJsErrors(this WebBrowser browser, Boolean silent)
        {
            if (browser == null)
                throw new ArgumentNullException(nameof(browser));

            var sp = browser.Document as IOleServiceProvider;
            if (sp == null)
                return;

            var iidIWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
            var iidIWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

            sp.QueryService(ref iidIWebBrowserApp, ref iidIWebBrowser2, out Object webBrowser);
            webBrowser?.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new Object[] { silent });
        }
    }
}