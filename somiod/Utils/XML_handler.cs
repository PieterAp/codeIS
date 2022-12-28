using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace somiod.Utils
{
    public class XML_handler
    {
        public XML_handler(XElement xmlFile)
        {
            XmlFile = xmlFile;
        }

        public XML_handler(XElement xmlFile, string xsdFile)
        {
            XmlFile = xmlFile;
            XsdFilePath = xsdFile;
        }

        public XElement XmlFile { get; set; }
        public string XsdFilePath { get; set; }

        private bool isValid = true;
        private string validationMessage;
        public string ValidationMessage
        {
            get { return validationMessage; }
        }

        public bool ValidateXML()
        {

            isValid = true;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(XmlFile.ToString());
                ValidationEventHandler eventHandler = new ValidationEventHandler(MyValidateMethod);
                doc.Schemas.Add(null, XsdFilePath);
                doc.Validate(eventHandler);
            }
            catch (XmlException ex)
            {
                isValid = false;
                validationMessage = string.Format("ERROR: {0}", ex.ToString());
            }
            return isValid;
        }

        private void MyValidateMethod(object sender, ValidationEventArgs args)
        {
            isValid = false;
            switch (args.Severity)
            {
                case XmlSeverityType.Error:
                    validationMessage = string.Format("ERROR: {0}", args.Message);
                    break;
                case XmlSeverityType.Warning:
                    validationMessage = string.Format("WARNING: {0}", args.Message);
                    break;
                default:
                    break;
            }
        }
    }
}