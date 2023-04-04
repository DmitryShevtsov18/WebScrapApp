using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace WebScrapApp.Core
{
    public class SSerialization
    {
        private XmlSerializer xmlSerializer;
        private object objectSerialize;
        private Type typeSerialize;

        public SSerialization(Type _type)
        {
            typeSerialize = _type;
            this.Load();
        }

        public SSerialization(object _obj)
        {
            objectSerialize = _obj;
            typeSerialize = objectSerialize.GetType();
            this.Load();
        }

        private void Load()
        {
            xmlSerializer = new XmlSerializer(typeSerialize);
            xmlSerializer.UnknownAttribute += XmlSerializer_UnknownAttribute;
            xmlSerializer.UnknownNode += XmlSerializer_UnknownNode;
        }

        private void XmlSerializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void XmlSerializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Write(string _filename)
        {
            if (objectSerialize != null && xmlSerializer != null)
            {
                using (TextWriter tw = new StreamWriter(_filename))
                {
                    xmlSerializer.Serialize(tw, objectSerialize);
                }
            }
        }

        public object Read(string _filename)
        {
            object ret = null;

            if (_filename != "" && xmlSerializer != null)
            {
                using (FileStream fs = new FileStream(_filename, FileMode.Open))
                {
                    ret = xmlSerializer.Deserialize(fs);
                }
            }

            return ret;
        }
    }
}
