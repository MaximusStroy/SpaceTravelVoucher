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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetTourAgents")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetTourAgents", IsNullable = false)]
    public partial class SetTourAgentsRequest
    {

        private SetTourAgentsRequestTourAgent[] tourAgentsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TourAgent", IsNullable = false)]
        public SetTourAgentsRequestTourAgent[] TourAgents
        {
            get
            {
                return this.tourAgentsField;
            }
            set
            {
                this.tourAgentsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn://artefacts-russiatourism-ru/services/message-exchange/types/SetTourAgents")]
    public partial class SetTourAgentsRequestTourAgent
    {

        private string nameField;

        private string innField;

        private string addressField;

        private string phoneNumberField;

        private string emailField;

        private bool activeField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string inn
        {
            get
            {
                return this.innField;
            }
            set
            {
                this.innField = value;
            }
        }

        /// <remarks/>
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public string phoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        public bool active
        {
            get
            {
                return this.activeField;
            }
            set
            {
                this.activeField = value;
            }
        }
    }
}