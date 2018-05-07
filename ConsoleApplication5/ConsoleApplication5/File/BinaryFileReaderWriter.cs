using FlowersGirl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FlowersGirl.File
{
    public class BinaryFileReaderWriter : FileReaderWriter
    {
        public List<Flower> Read(string filePath)
        {
            List<Flower> flowers;
            FileStream fs = new FileStream(filePath, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                flowers = (List<Flower>)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return flowers;
        }

        public string Write(List<Flower> flowers, string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, flowers);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return filePath;
        }
    }
}
