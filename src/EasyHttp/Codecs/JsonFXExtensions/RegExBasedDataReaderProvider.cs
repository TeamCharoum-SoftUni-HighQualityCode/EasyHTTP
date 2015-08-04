﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JsonFx.Serialization;
using JsonFx.Serialization.Providers;

namespace EasyHttp.Codecs.JsonFXExtensions
{
    public class RegExBasedDataReaderProvider: IDataReaderProvider
    {
        readonly IDictionary<string, IDataReader> readersByMime = new Dictionary<string, IDataReader>(StringComparer.OrdinalIgnoreCase);

        public RegExBasedDataReaderProvider(IEnumerable<IDataReader> dataReaders)
        {
            if (dataReaders != null)
            {
                foreach (IDataReader reader in dataReaders)
                {
                    foreach (string contentType in reader.ContentType)
                    {
                        if (String.IsNullOrEmpty(contentType) ||
                            this.readersByMime.ContainsKey(contentType))
                        {
                            continue;
                        }

                        this.readersByMime[contentType] = reader;
                    }
                }
            }

        }

        //ToDo method name should be changed
        public IDataReader Find(string contentTypeHeader)
        {
            var type = DataProviderUtility.ParseMediaType(contentTypeHeader);

            var readers = this.readersByMime.Where(reader => Regex.Match(type, reader.Key, RegexOptions.Singleline).Success);
			 
            return readers.Any() ? readers.First().Value : null;
        }
    }
}