using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OdnoWindowsApp.Model
{
    internal class MusicBrainzObjectModel
    {
        /* how to use */
        // using System.Xml.Serialization;
        // XmlSerializer serializer = new XmlSerializer(typeof(Metadata));
        // using (StringReader reader = new StringReader(xml))
        // {
        //    var test = (Metadata)serializer.Deserialize(reader);
        // }

        [XmlRoot(ElementName = "release-group")]
        public class Releasegroup
        {
            [XmlElement(ElementName = "first-release-date")]
            public DateTime Firstreleasedate;
        }

        [XmlRoot(ElementName = "release")]
        public class Release
        {

            [XmlElement(ElementName = "release-group")]
            public Releasegroup Releasegroup;

            [XmlAttribute(AttributeName = "id")]
            public string Id;

            [XmlText]
            public string Text;
        }

        [XmlRoot(ElementName = "metadata")]
        public class Metadata
        {

            [XmlElement(ElementName = "release")]
            public Release Release;

            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns;

            [XmlText]
            public string Text;
        }
    }
}
