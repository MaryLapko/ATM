using FlowersGirl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersGirl.File
{
    class JsonFileReaderWriter : FileReaderWriter
    {
        public List<Flower> Read(string filePath)
        {
            List<Flower> flowers;
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                flowers = JsonConvert.DeserializeObject<BucketModel>(json).Flowers;
            }

            return flowers;
        }

        public string Write(List<Flower> flowers, string filePath)
        {
            BucketModel model = new BucketModel();
            model.Flowers = flowers;
            using (StreamWriter file = System.IO.File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, model);
            }

            return filePath;
        }
    }
}
