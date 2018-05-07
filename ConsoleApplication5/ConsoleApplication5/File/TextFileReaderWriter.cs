using FlowersGirl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlowersGirl.File
{
    public class TextFileReaderWriter : FileReaderWriter
    {
        public List<Flower> Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentOutOfRangeException("File path must be specified");
            }

            List<Flower> flowers = new List<Flower>();
            string line;
            string[] headers = null; // One of this headers: Name, Price, Currency, Colour

            using (StreamReader file = new StreamReader(filePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (headers == null)
                    {
                        headers = line.Split(null);
                    }
                    else
                    {
                        string[] values = line.Split(null);

                        var flower = CreateFlowerFromFileString(values, headers);

                        flowers.Add(flower);
                    }
                }
            }

            return flowers;
        }

        private Flower CreateFlowerFromFileString(string[] values, string[] headers)
        {
            Flower flower = null;

            for (int i = 0; i < values.Length; i++)
            {
                string value = values[i];
                string header = headers[i];

                switch (header)
                {
                    case "Name":
                        flower = Flower.CreateFlower(value);
                        break;
                    case "Price":
                        if (flower == null)
                        {
                            throw new ArgumentOutOfRangeException("Header 'Name' must be the first in file headers not 'Price'");
                        }

                        flower.Price = int.Parse(value);
                        break;
                    case "Currency":
                        if (flower == null)
                        {
                            throw new ArgumentOutOfRangeException("Header 'Name' must be the first in file headers not 'Currency'");
                        }

                        flower.Currency = (Currency)Enum.Parse(typeof(Currency), value);
                        break;
                    case "Colour":
                        if (flower == null)
                        {
                            throw new ArgumentOutOfRangeException("Header 'Name' must be the first in file headers not 'Colour'");
                        }

                        flower.Colour = (Colour)Enum.Parse(typeof(Colour), value);
                        break;
                    default:
                        throw new FileFormatException(string.Format("Incorrect file format exception. Unexpected file column header: {0}", value));
                }
            }

            return flower;
        }

        public string Write(List<Flower> flowers, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine("Name Price Currency Colour");
                foreach (var flower in flowers)
                {
                    StringBuilder flowerLineBuilder = new StringBuilder();

                    flowerLineBuilder.Append(flower.Name);
                    flowerLineBuilder.Append(" ");
                    flowerLineBuilder.Append(flower.Price);
                    flowerLineBuilder.Append(" ");
                    flowerLineBuilder.Append(flower.Currency);
                    flowerLineBuilder.Append(" ");
                    flowerLineBuilder.Append(flower.Colour);

                    file.WriteLine(flowerLineBuilder.ToString());
                }
            }

            return filePath;
        }
    }
}
