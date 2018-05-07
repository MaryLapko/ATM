using FlowersGirl.Models;
using System.Collections.Generic;

namespace FlowersGirl.File
{
    public interface FileReaderWriter
    {
        List<Flower> Read(string filePath);

        string Write(List<Flower> flowers, string filePath);
    }
}
