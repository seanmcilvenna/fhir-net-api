using Hl7.Fhir.Navigation;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.FhirPath.InstanceTree
{
    internal class XAttributeConversionStrategy : INodeConversionStrategy<XObject>
    {
        public bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XAttribute) && ((XAttribute)docNode).Name.NamespaceName == "";
        }

        public FhirInstanceTree ConstructTreeNode(XObject docNode, FhirInstanceTree parent)
        {
            var attr = (XAttribute)docNode;

            var newNodeName = attr.Name.LocalName;
            var result = parent.AddLastChild(newNodeName, (IFhirPathValue)new UntypedValue(attr.Value));
            result.AddAnnotation(new XmlRenderHints() { IsXmlAttribute = true });
            result.AddAnnotation(docNode);
            return result;
        }

        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirInstanceTree treeNode)
        {
            return null;
        }

        public void PostProcess(FhirInstanceTree convertedNode)
        {
            return;
        }
    }
}