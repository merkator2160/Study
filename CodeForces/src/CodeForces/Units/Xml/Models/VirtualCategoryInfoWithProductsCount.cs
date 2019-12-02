using System;
using System.Collections.Generic;

namespace CodeForces.Units.Xml.Models
{
    public class VirtualCategoryInfoWithProductsCount
    {
        public HashSet<String> PhysicalIds => _physicalIds ?? (_physicalIds = new HashSet<String>());
        private HashSet<String> _physicalIds;
        public Int32? MaxProductsCount { get; set; }
        public Int32 ProcessedProductsCount { get; set; }
        public Boolean HasProductsCountRestriction => MaxProductsCount.HasValue;
        public Boolean IsProcessedGreatThanMax
        {
            get
            {
                var result = false;

                if (MaxProductsCount.HasValue)
                {
                    result = ProcessedProductsCount >= MaxProductsCount.Value;
                }

                return result;
            }
        }
    }
}