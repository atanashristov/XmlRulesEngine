using System;
using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    [XmlRoot("rules-data")]
    public abstract class BaseElement
    {

        [XmlAttribute("name")]
        public string Name { get; set; }



        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlIgnore]
        public bool DescriptionSpecified { get { return string.IsNullOrEmpty(Description); } }




        [XmlAttribute("disabled")]
        public bool Disabled { get; set; }

        [XmlIgnore]
        public bool DisabledSpecified { get { return Disabled; } }




        [XmlAttribute("start-date-time")]
        public string StartDateTimeSerialized { get; set; }

        [XmlIgnore]
        public bool StartDateTimeSerializedSpecified { get { return !string.IsNullOrEmpty(StartDateTimeSerialized); } }

        [XmlIgnore]
        public DateTime? StartDateTime
        {
            get
            {
                DateTime result;
                if (DateTime.TryParse(StartDateTimeSerialized, out result))
                    return result;

                return null;
            }
            set { StartDateTimeSerialized = (value.HasValue) ? value.Value.ToString("s") : null; }
        }




        [XmlAttribute("end-date-time")]
        public string EndDateTimeSerialized { get; set; }

        [XmlIgnore]
        public bool EndDateTimeSerializedSpecified { get { return !string.IsNullOrEmpty(EndDateTimeSerialized); } }

        [XmlIgnore]
        public DateTime? EndDateTime
        {
            get
            {
                DateTime result;
                if (DateTime.TryParse(EndDateTimeSerialized, out result))
                    return result;

                return null;
            }
            set { EndDateTimeSerialized = (value.HasValue) ? value.Value.ToString("s") : null; }
        }
    }
}
