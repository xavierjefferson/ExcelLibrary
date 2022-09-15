using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace QiHe.CodeLib
{
    /// <summary>
    /// XmlFile load and save object from and to xml file;
    /// </summary>	
    public class XmlFile
    {
        /// <summary>
        /// Loads the specified xmlfile.
        /// </summary>
        /// <param name="xmlfile">The xmlfile.</param>
        /// <param name="datatype">The datatype.</param>
        /// <returns></returns>
        public static object Load(string xmlfile, Type datatype)
        {
            object data = null;
            XmlSerializer mySerializer = new XmlSerializer(datatype);
            if (File.Exists(xmlfile))
            {
                XmlTextReader reader = new XmlTextReader(xmlfile);
                data = mySerializer.Deserialize(reader);
                reader.Close();
            }
            else
            {
                //throw new FileNotFoundException(xmlfile);
                return null;
            }
            return data;
        }
        /// <summary>
        /// Saves the specified xmlfile.
        /// </summary>
        /// <param name="xmlfile">The xmlfile.</param>
        /// <param name="data">The data.</param>
        public static void Save(string xmlfile, object data)
        {
            XmlSerializer mySerializer = new XmlSerializer(data.GetType());
            XmlTextWriter writer = new XmlTextWriter(xmlfile, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            mySerializer.Serialize(writer, data);
            writer.Close();
        }
        /// <summary>
        /// Saves the specified xmlfile.
        /// </summary>
        /// <param name="xmlfile">The xmlfile.</param>
        /// <param name="root">The root.</param>
        /// <param name="data">The data.</param>
        public static void Save(string xmlfile, string root, object data)
        {
            XmlRootAttribute rootattr = new XmlRootAttribute(root);
            XmlSerializer mySerializer = new XmlSerializer(data.GetType(), rootattr);
            XmlTextWriter writer = new XmlTextWriter(xmlfile, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            mySerializer.Serialize(writer, data);
            writer.Close();
        }
    }
}