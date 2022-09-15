using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QiHe.CodeLib
{
    /// <summary>
    /// BinFile
    /// </summary>
    public class BinFile
    {
        /// <summary>
        /// Reads the specified datafile.
        /// </summary>
        /// <param name="datafile">The datafile.</param>
        /// <returns></returns>
        public static object Read(string datafile)
        {
            if (File.Exists(datafile))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(datafile, FileMode.Open, FileAccess.Read, FileShare.Read);
                object obj = formatter.Deserialize(stream);
                stream.Close();
                return obj;
            }
            else
            {
                throw new FileNotFoundException(datafile);
            }
        }
        /// <summary>
        /// Writes the specified datafile.
        /// </summary>
        /// <param name="datafile">The datafile.</param>
        /// <param name="obj">The obj.</param>
        public static void Write(string datafile, object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(datafile, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
    }
}