﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// Этот исходный код был создан с помощью xsd, версия=4.8.3928.0.
// 
namespace SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetPartners")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetPartners", IsNullable = false)]
    public partial class SetPartnersResponse
    {

        private string successMessageField;

        /// <remarks/>
        public string successMessage
        {
            get
            {
                return this.successMessageField;
            }
            set
            {
                this.successMessageField = value;
            }
        }
    }
}
