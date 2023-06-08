using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(Partners));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Partners)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "Partner")]
	public class Partner
	{

		[XmlElement(ElementName = "code")]
		public string Code { get; set; }

		[XmlElement(ElementName = "fullName")]
		public string FullName { get; set; }

		[XmlElement(ElementName = "contacts")]
		public double Contacts { get; set; }

		[XmlElement(ElementName = "active")]
		public bool Active { get; set; }
	}

	[XmlRoot(ElementName = "Partners", Namespace = "urn://artefacts-russiatourism-ru/services/message-exchange/types/Partners")]
	public class Partners
	{

		[XmlElement(ElementName = "Partner")]
		public List<Partner> Partner { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}


}
