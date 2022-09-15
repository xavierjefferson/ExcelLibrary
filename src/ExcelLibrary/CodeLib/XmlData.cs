using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace QiHe.CodeLib
{
    /// <summary>
    /// XmlFile load and save object from and to xml file;
    /// </summary>	
    public class XmlData<DataType>
    {
        /// <summary>
        /// Loads the specified xmlfile.
        /// </summary>
        /// <param name="xmlfile">The xmlfile.</param>
        /// <returns></returns>
        public static DataType Load(string xmlfile)
        {
            DataType data;
            Type datatype = typeof(DataType);
            XmlSerializer mySerializer = new XmlSerializer(datatype);
            if (File.Exists(xmlfile))
            {
                using (XmlTextReader reader = new XmlTextReader(xmlfile))
                {
                    data = (DataType)mySerializer.Deserialize(reader);
                }
            }
            else
            {
                //throw new FileNotFoundException(xmlfile);
                return default(DataType);
            }
            return data;
        }
        /// <summary>
        /// Loads the specified xmldata.
        /// </summary>
        /// <param name="xmldata">The xmldata.</param>
        /// <returns></returns>
        public static DataType Load(Stream xmldata)
        {
            using (XmlTextReader reader = new XmlTextReader(xmldata))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(DataType));
                return (DataType)mySerializer.Deserialize(reader);
            }
        }
        /// <summary>
        /// Loads the specified xmlfile.
        /// </summary>
        /// <param name="xmlfile">The xmlfile.</param>
        /// <param name="root">The root.</param>
        /// <returns></returns>
        public static DataType Load(string xmlfile, string root)
        {
            DataType data;
            Type datatype = typeof(DataType);
            XmlRootAttribute rootattr = new XmlRootAttribute(root);
            XmlSerializer mySerializer = new XmlSerializer(datatype, rootattr);
            if (File.Exists(xmlfile))
            {
                XmlTextReader reader = new XmlTextReader(xmlfile);
                data = (DataType)mySerializer.Deserialize(reader);
                reader.Close();
            }
            else
            {
                //throw new FileNotFoundException(xmlfile);
                return default(DataType);
            }
            return data;
        }
        /// <summary>
        /// Saves the specified xmlfile.
        /// </summary>
        /// <param name="xmlfile">The xmlfile.</param>
        /// <param name="data">The data.</param>
        public static void Save(string xmlfile, DataType data)
        {
            XmlSerializer mySerializer = new XmlSerializer(data.GetType());
            XmlTextWriter writer = new XmlTextWriter(xmlfile, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            mySerializer.Serialize(writer, data);
            writer.Close();
        }
    }
}
