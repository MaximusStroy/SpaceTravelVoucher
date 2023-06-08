using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models
{

	[XmlRoot(ElementName = "TourAgent")]
	public class TourAgent
	{

		[XmlElement(ElementName = "name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "email")]
		public string Email { get; set; }

		[XmlElement(ElementName = "phoneNumber")]
		public string PhoneNumber { get; set; }

		[XmlElement(ElementName = "address")]
		public string Address { get; set; }

		[XmlElement(ElementName = "inn")]
		public string Inn { get; set; }

		[XmlElement(ElementName = "active")]
		public bool Active { get; set; }
	}

	[XmlRoot(ElementName = "TourAgents")]
	public class TourAgents
	{

		[XmlElement(ElementName = "TourAgent")]
		public List<TourAgent> TourAgent { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
