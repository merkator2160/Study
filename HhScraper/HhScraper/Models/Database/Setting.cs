//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HhScraper.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Setting
    {
        public string UrlTb { get; set; }
        public string XPathTb { get; set; }
        public Nullable<bool> UseExpressionsCb { get; set; }
        public Nullable<bool> CollectDataRb { get; set; }
        public Nullable<bool> CollectLinksRb { get; set; }
        public int SettingsID { get; set; }
        public string AttributeTb { get; set; }
        public string DelayDeviationTb { get; set; }
        public string DelayPodiumTb { get; set; }
    }
}