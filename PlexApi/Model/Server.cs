using System.Xml.Serialization;

namespace PlexApi.Model
{
    [XmlRoot(ElementName="Server")]
    public class Server
    {
        [XmlAttribute(AttributeName="accessToken")] 
        public string AccessToken { get; set; } 

        [XmlAttribute(AttributeName="name")] 
        public string Name { get; set; } 

        [XmlAttribute(AttributeName="address")] 
        public string Address { get; set; } 

        [XmlAttribute(AttributeName="port")] 
        public int Port { get; set; } 

        [XmlAttribute(AttributeName="version")] 
        public string Version { get; set; } 

        [XmlAttribute(AttributeName="scheme")] 
        public string Scheme { get; set; } 

        [XmlAttribute(AttributeName="host")] 
        public string Host { get; set; } 

        [XmlAttribute(AttributeName="localAddresses")] 
        public string LocalAddresses { get; set; } 

        [XmlAttribute(AttributeName="machineIdentifier")] 
        public string MachineIdentifier { get; set; } 

        [XmlAttribute(AttributeName="createdAt")] 
        public int CreatedAt { get; set; } 

        [XmlAttribute(AttributeName="updatedAt")] 
        public int UpdatedAt { get; set; } 

        [XmlAttribute(AttributeName="owned")] 
        public int Owned { get; set; }

        public bool IsOwned => Owned == 1;

        [XmlAttribute(AttributeName="synced")] 
        public int Synced { get; set; } 

        [XmlAttribute(AttributeName="sourceTitle")] 
        public string SourceTitle { get; set; } 

        [XmlAttribute(AttributeName="ownerId")] 
        public int OwnerId { get; set; } 

        [XmlAttribute(AttributeName="home")] 
        public int Home { get; set; } 
    }
}