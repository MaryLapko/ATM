using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlowersGirl.Models
{
    [Serializable]
    [XmlRoot(ElementName = "Bucket")]
    public class BucketModel
    {
        [XmlElement("Flower")]
        public List<Flower> Flowers = new List<Flower>();
    }
}
