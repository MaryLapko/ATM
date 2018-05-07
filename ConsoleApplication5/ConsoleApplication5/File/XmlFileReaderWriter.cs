using FlowersGirl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FlowersGirl.File
{
    public class XmlFileReaderWriter : FileReaderWriter
    {
        public List<Flower> Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentOutOfRangeException("File path must be specified");
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(BucketModel));
            TextReader reader = new StreamReader(filePath);
            object obj = deserializer.Deserialize(reader);
            BucketModel bucketModel = (BucketModel)obj;
            reader.Close();
            return bucketModel.Flowers;
        }

        public string Write(List<Flower> flowers, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentOutOfRangeException("File path must be specified");
            }

            BucketModel bucketModel = new BucketModel();
            bucketModel.Flowers = flowers;
            XmlSerializer writer = new XmlSerializer(typeof(BucketModel));
            FileStream file = System.IO.File.Create(filePath);
            writer.Serialize(file, bucketModel);
            file.Close();
            return filePath;
        }
    }
}
