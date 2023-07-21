using Microsoft.SqlServer.Server;
using MovieLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Services
{
    internal class DataSerializer
    {

        public static void  BinarySerialize(List<Movie> data,string filepath)
        {
           
            using (FileStream fileStream = new FileStream(filepath, FileMode.
                OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                if (data.Count > 0)
                {
                    foreach (Movie movie in data)
                    {
                        binaryFormatter.Serialize(fileStream, movie);
                    }
                }
              
            }
        }
       
        public static List<Movie> BinaryDeserialize(string filepath)
        {
           List<Movie>m = new List<Movie>();
           

            using (FileStream fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    while (fileStream.Position < fileStream.Length)
                    {
                        m.Add((Movie)binaryFormatter.Deserialize(fileStream));
                    }
                    return m;
                }
                catch (Exception ex)
                {
                    return m;
                }

            }
         
            
           
           
            
        }

    }
}
