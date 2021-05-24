using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlexApi.Model
{
    [XmlRoot(ElementName="MediaContainer")]
    public class ServerList
    {
        [XmlElement(ElementName="Server")] 
        public List<Server> Servers { get; set; } 

        [XmlAttribute(AttributeName="friendlyName")] 
        public string FriendlyName { get; set; } 

        [XmlAttribute(AttributeName="identifier")] 
        public string Identifier { get; set; } 

        [XmlAttribute(AttributeName="machineIdentifier")] 
        public string MachineIdentifier { get; set; } 

        [XmlAttribute(AttributeName="size")] 
        public int Size { get; set; }
    }
}