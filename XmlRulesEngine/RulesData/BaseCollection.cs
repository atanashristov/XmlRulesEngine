using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    public abstract class BaseCollection<T> : BaseElement
        where T : BaseElement
    {

        [XmlElement("add")]
        public List<T> Data { get; set; }



        protected BaseCollection()
        {
            Data = new List<T>();
        }


        public T this[string name]
        {
            get
            {
                try
                {
                    if (Data == null)
                        return null;

                    return Data.FirstOrDefault(e => e.Name == name);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
