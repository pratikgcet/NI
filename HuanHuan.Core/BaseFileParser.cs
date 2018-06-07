using System.Collections.Generic;
using System.IO;
using System.Xml;
using HuanHuan.Domain;

namespace HuanHuan.Core
{
    public abstract class BaseFileParser<T>
    {
        private FileType GetFileTypeFromFile(string filepath)
        {
            var resolvedFileType = FileType.Undefined;

            if (!File.Exists(filepath))
                return resolvedFileType;

            var fi = new FileInfo(filepath);
            
            switch (fi.Extension)
            {
                case Constants.WellKnownFileExtensions.Csv:
                    resolvedFileType = FileType.Csv;
                    break;
                case Constants.WellKnownFileExtensions.Xml:
                    resolvedFileType = FileType.Xml;
                    break;
                case Constants.WellKnownFileExtensions.Json:
                    resolvedFileType = FileType.Json;
                    break;
                default:
                    resolvedFileType = FileType.Csv;
                    break; ;
            }

            return resolvedFileType;
        }

        // Entry method to read data
        protected IEnumerable<T> DoParse(string filepath)
        {
            var res = this.ReadData(filepath);
            return res ?? new T[0];
        }       
        protected IEnumerable<T> ReadData(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var fileType = GetFileTypeFromFile(filePath);

            switch (fileType)
            {
                case FileType.Csv:
                    return ReadDataFromCsv(filePath);

                case FileType.Xml:
                    return ReadDataFromXml(filePath);

                case FileType.Json:
                    return ReadDataFromJson(filePath);

                default:
                    return null;
            }
        }

        // == Template pattern for reading Csv data
        protected abstract IEnumerable<T> ReadDataFromCsv(string filePath);

        // == Template pattern for reading JSON data
        protected abstract IEnumerable<T> ReadDataFromJson(string filePath);

        // == Template pattern for reading XML data
        protected abstract IEnumerable<T> ReadDataFromXml(string filePath);

    }
}