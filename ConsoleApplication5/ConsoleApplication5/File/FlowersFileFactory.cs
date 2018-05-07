using FlowersGirl.Models;
using System;
using System.Collections.Generic;

namespace FlowersGirl.File
{
    public class FlowersFileFactory
    {
        private static FileReaderWriter TextFileReader = new TextFileReaderWriter();

        private static FileReaderWriter XmlFileReader = new XmlFileReaderWriter();

        private static FileReaderWriter BinaryFileReaderWriter = new BinaryFileReaderWriter();

        private static FileReaderWriter JsonFileReader = new JsonFileReaderWriter();

        public string Write(string filePath, List<Flower> flowers, FileSourceType type)
        {
            return GetReaderWriter(type).Write(flowers, filePath);
        }

        public List<Flower> Read(string filePath, FileSourceType type)
        {
            return GetReaderWriter(type).Read(filePath);
        }

        private FileReaderWriter GetReaderWriter(FileSourceType type)
        {
            FileReaderWriter readerWriter;

            switch (type)
            {
                case FileSourceType.TextFile:
                    readerWriter = TextFileReader;
                    break;
                case FileSourceType.BinaryFile:
                    readerWriter = BinaryFileReaderWriter;
                    break;
                case FileSourceType.XMLFile:
                    readerWriter = XmlFileReader;
                    break;
                case FileSourceType.JSONFile:
                    readerWriter = JsonFileReader;
                    break;
                default:
                    throw new InvalidOperationException("Invalid type of file source");
            }

            return readerWriter;
        }
    }
}
