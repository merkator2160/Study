using System;
using System.Runtime.InteropServices;

namespace HhScraper.Extensions
{
    public static partial class BrowserExtensions
    {
        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleServiceProvider
        {
            [PreserveSig]
            Int32 QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out Object ppvObject);
        }
    }
}