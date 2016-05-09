using System.Linq;
using System.Xml.Linq;
using NHapi.Base.Model;
using NHapi.Base.Model.Primitive;

namespace NHapi.Base.Conf
{
    public static class Extensions
    {
        public static string GetAttribute(this XElement anXmlElement, string attributeName)
        {
            string rval = "";
            XAttribute attributeOfInterest = anXmlElement.Attribute(attributeName);
            if (attributeOfInterest != null)
            {
                rval = attributeOfInterest.Value;
            }
            return rval;
        }
    }
}