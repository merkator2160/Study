using System.Collections.Generic;

namespace CodeForces.Units.XmlUnite
{
    public class PropertyValueWithNameEqualityComparer : IEqualityComparer<PropertyValueWithName>
    {
        public bool Equals(PropertyValueWithName x, PropertyValueWithName y)
        {
            return Equals(x.PropertyId, y.PropertyId) && Equals(x.ValueId, y.ValueId);
        }
        public int GetHashCode(PropertyValueWithName obj)
        {
            var propertyIdCode = obj.PropertyId.GetHashCode();
            var valueIdCode = 1;
            var propertyNameCode = 2;


            if (obj.ValueId != null)
            {
                valueIdCode = obj.ValueId.GetHashCode();
            }
            if (obj.Name != null)
            {
                propertyNameCode = obj.Name.GetHashCode();
            }

            return propertyIdCode ^ valueIdCode ^ propertyNameCode;

        }
    }
}