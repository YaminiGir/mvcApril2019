using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace mvcApril2019.Models
{
    [Serializable]
    [XmlRoot("product")]
    public class ProductMetaData
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public Nullable<decimal> Price { get; set; }

        [XmlElement("quantity")]
        public Nullable<int> Quantity { get; set; }
    }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
}