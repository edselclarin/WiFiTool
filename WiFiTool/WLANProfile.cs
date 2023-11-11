using System.Xml.Serialization;

namespace WiFiTool
{
    [XmlRoot(ElementName = "SSID")]
    public class SSID
    {

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "SSIDConfig")]
    public class SSIDConfig
    {

        [XmlElement(ElementName = "SSID")]
        public SSID SSID { get; set; }
    }

    [XmlRoot(ElementName = "authEncryption")]
    public class AuthEncryption
    {

        [XmlElement(ElementName = "authentication")]
        public string Authentication { get; set; }

        [XmlElement(ElementName = "encryption")]
        public string Encryption { get; set; }

        [XmlElement(ElementName = "useOneX")]
        public bool UseOneX { get; set; }
    }

    [XmlRoot(ElementName = "sharedKey")]
    public class SharedKey
    {

        [XmlElement(ElementName = "keyType")]
        public string KeyType { get; set; }

        [XmlElement(ElementName = "protected")]
        public bool Protected { get; set; }

        [XmlElement(ElementName = "keyMaterial")]
        public string KeyMaterial { get; set; }
    }

    [XmlRoot(ElementName = "security")]
    public class Security
    {

        [XmlElement(ElementName = "authEncryption")]
        public AuthEncryption AuthEncryption { get; set; }

        [XmlElement(ElementName = "sharedKey")]
        public SharedKey SharedKey { get; set; }
    }

    [XmlRoot(ElementName = "MSM")]
    public class MSM
    {

        [XmlElement(ElementName = "security")]
        public Security Security { get; set; }
    }

    [XmlRoot(ElementName = "WLANProfile", Namespace = "http://www.microsoft.com/networking/WLAN/profile/v1")]
    public class WLANProfile
    {

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "SSIDConfig")]
        public SSIDConfig SSIDConfig { get; set; }

        [XmlElement(ElementName = "connectionType")]
        public string ConnectionType { get; set; }

        [XmlElement(ElementName = "connectionMode")]
        public string ConnectionMode { get; set; }

        [XmlElement(ElementName = "MSM")]
        public MSM MSM { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
