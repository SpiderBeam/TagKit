using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    internal interface IValidationEventHandling
    {

        // This is a ValidationEventHandler, but it is not strongly typed due to dependencies on System.Xml.Schema
        object EventHandler { get; }

        // The exception is XmlSchemaException, but it is not strongly typed due to dependencies on System.Xml.Schema
        void SendEvent(Exception exception, TagSeverityType severity);
    }
}
