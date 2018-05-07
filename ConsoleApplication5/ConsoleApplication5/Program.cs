using FlowersGirl.Database;
using FlowersGirl.File;
using FlowersGirl.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace FlowersGirl
{
    public class Program
    {
        private static FlowersFileFactory FileFactory = new FlowersFileFactory();

        private static FlowerDAO DAO = new FlowerDAO();

        private const string XML_FILE_PATH = @"..\..\FileResources\Bucket.xml";

        private const string TEXT_FILE_PATH = @"..\..\FileResources\Bucket.txt";

        private const string BINARY_FILE_PATH = @"..\..\FileResources\Bucket.dat";

        private const string JSON_FILE_PATH = @"..\..\FileResources\Bucket.json";

        public static void Main(string[] args)
        {
            TestSerializationDeserialization();
            TestDatabaseOperations();
        }

        private static void TestSerializationDeserialization()
        {
            FlowersBucket bucket = CreateBucketDirectly();
            Console.WriteLine("Created directly flower bucket: " + Environment.NewLine + bucket.ToString());

            FlowersBucket bucketFromXml = CreateBucketFromXml();
            Console.WriteLine("Created from XML flower bucket: " + Environment.NewLine + bucketFromXml.ToString());

            FlowersBucket bucketFromTextFile = CreateBucketFromTextFile();
            Console.WriteLine("Created from text file flower bucket: " + Environment.NewLine + bucketFromTextFile.ToString());

            FlowersBucket bucketFromBinaryFile = CreateBucketFromBinaryFile();
            Console.WriteLine("Created from binary file flower bucket: " + Environment.NewLine + bucketFromBinaryFile.ToString());

            FlowersBucket bucketFromJsonFile = CreateBucketFromJsonFile();
            Console.WriteLine("Created from json file flower bucket: " + Environment.NewLine + bucketFromJsonFile.ToString());

            Console.ReadLine();
        }

        private static void TestDatabaseOperations()
        {
            DAO.InitOrReInit();
            DAO.InsertFlowerBucket(CreateBucketDirectly());
            int roseId = DAO.InsertFlower(new Rose(180, Colour.White));
            DAO.InsertFlower(new Rose(280, Colour.Red));
            DAO.InsertFlower(new Rose(500, Colour.Black));
            DAO.InsertFlower(new Lily(120, Colour.Green));
            var expensiveLily = DAO.InsertFlower(new Lily(150, Colour.White));
            DAO.InsertFlower(new Lily(110, Colour.Yellow));
            DAO.InsertFlower(new Lily(80, Colour.Yellow));
            DAO.InsertFlower(new Lily(10, Colour.Yellow));
            DAO.InsertFlower(new Lily(50, Colour.White));
            DAO.DeleteFlower(roseId);

            Console.WriteLine("Top 5 most expensive Lilies: ");

            foreach (var flower in DAO.FindTop5ExpensiveFlowersByName("Lily"))
            {
                Console.WriteLine(flower.ToString());
            }

            DAO.UpdateFlowerPrice(1000, Currency.BYN, expensiveLily);

            Console.WriteLine("All Flowers: ");

            foreach (var flower in DAO.FindAllFlowers())
            {
                Console.WriteLine(flower.ToString());
            }

            Console.ReadLine();
        }

        private static FlowersBucket CreateBucketDirectly()
        {
            return CreateBucket(CreateFlowers());
        }

        private static List<Flower> CreateFlowers()
        {
            List<Flower> flowers = new List<Flower>();

            flowers.Add(new Rose(120, Colour.Red));
            flowers.Add(new Lily(100, Colour.White));
            flowers.Add(new Iris(90, Colour.Blue));
            flowers.Add(new Tulip(150, Colour.Yellow));
            flowers.Add(new Chamomile(52, Colour.White));

            return flowers;
        }

        private static FlowersBucket CreateBucketFromXml()
        {
            string filePath = FileFactory.Write(XML_FILE_PATH, CreateFlowers(), FileSourceType.XMLFile);
            return CreateBucket(FileFactory.Read(XML_FILE_PATH, FileSourceType.XMLFile));
        }

        private static FlowersBucket CreateBucketFromTextFile()
        {
            string filePath= FileFactory.Write(TEXT_FILE_PATH, CreateFlowers(), FileSourceType.TextFile);
            return CreateBucket(FileFactory.Read(filePath, FileSourceType.TextFile));
        }

        private static FlowersBucket CreateBucketFromBinaryFile()
        {
            string filePath = FileFactory.Write(BINARY_FILE_PATH, CreateFlowers(), FileSourceType.BinaryFile);
            return CreateBucket(FileFactory.Read(filePath, FileSourceType.BinaryFile));
        }

        private static FlowersBucket CreateBucketFromJsonFile()
        {
            string filePath = FileFactory.Write(JSON_FILE_PATH, CreateFlowers(), FileSourceType.JSONFile);
            return CreateBucket(FileFactory.Read(filePath, FileSourceType.JSONFile));
        }

        private static FlowersBucket CreateBucket(List<Flower> flowers)
        {
            FlowersBucket.Builder bucketBuilder = new FlowersBucket.Builder();

            foreach (var flower in flowers)
            {
                bucketBuilder.AddFlower(flower);
            }

            return bucketBuilder.Build();
        }
    }
}