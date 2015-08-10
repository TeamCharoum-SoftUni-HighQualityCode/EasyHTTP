using System;
using EasyHttp.Http;

namespace EasyHttp.Infrastructure
{
    /// <summary>
    /// A class to hold information on all files and related data
    /// </summary>
    public class FileData
    {
        private string fieldName;
        private string fileName;
        private string contentType;
        private string contentTransferEncoding;

        public string FieldName
        {
            get
            {
                return this.fieldName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.fieldName = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
                
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.fileName = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.contentType = value;
            }
        }

        public string ContentTransferEncoding
        {
            get
            {
                return this.contentTransferEncoding;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.contentTransferEncoding = value;
            }
        }

        public FileData()
        {
            this.ContentTransferEncoding = HttpContentTransferEncoding.Binary;
        }
    }
}