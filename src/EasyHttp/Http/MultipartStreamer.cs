namespace EasyHttp.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using EasyHttp.Infrastructure;

    public class MultiPartStreamer
    {
        private static readonly string BoundaryCode = DateTime.Now.Ticks.GetHashCode() + "548130";
        private static readonly string Boundary = string.Format("\r\n----------------{0}", BoundaryCode);

        private readonly string boundary;
        private readonly string boundaryCode;
        private readonly IList<FileData> multipartFileData;
        private readonly IDictionary<string, object> multipartFormData;

        public MultiPartStreamer(IDictionary<string, object> multipartFormData, IList<FileData> multipartFileData)
        {
            // TODO we can think for other logic here
            this.boundaryCode = BoundaryCode;
            this.boundary = Boundary;

            this.multipartFormData = multipartFormData;
            this.multipartFileData = multipartFileData;
        }

        public void StreamMultiPart(Stream stream)
        {
            stream.WriteString(this.boundary);

            if (this.multipartFormData != null)
            {
                foreach (var entry in this.multipartFormData)
                {
                    stream.WriteString(CreateFormBoundaryHeader(entry.Key, entry.Value));
                    stream.WriteString(this.boundary);
                }
            }

            if (this.multipartFileData != null)
            {
                foreach (var fileData in this.multipartFileData)
                {
                    using (var file = new FileStream(fileData.FileName, FileMode.Open))
                    {
                        stream.WriteString(CreateFileBoundaryHeader(fileData));

                        StreamFileContents(file, fileData, stream);

                        stream.WriteString(this.boundary);
                    }
                }
            }

            stream.WriteString("--");
        }

        public string GetContentType()
        {
            return string.Format("multipart/form-data; boundary=--------------{0}", this.boundaryCode);
        }

        public long GetContentLength()
        {
            var ascii = new ASCIIEncoding();
            long contentLength = ascii.GetBytes(this.boundary).Length;

            // Multipart Form
            if (this.multipartFormData != null)
            {
                foreach (var entry in this.multipartFormData)
                {
                    contentLength += ascii.GetBytes(CreateFormBoundaryHeader(entry.Key, entry.Value)).Length; // header
                    contentLength += ascii.GetBytes(this.boundary).Length;
                }
            }

            if (this.multipartFileData != null)
            {
                foreach (var fileData in this.multipartFileData)
                {
                    contentLength += ascii.GetBytes(CreateFileBoundaryHeader(fileData)).Length;
                    contentLength += new FileInfo(fileData.FileName).Length;
                    contentLength += ascii.GetBytes(this.boundary).Length;
                }
            }

            contentLength += ascii.GetBytes("--").Length; // ending -- to the boundary

            return contentLength;
        }

        private static void StreamFileContents(Stream file, FileData fileData, Stream requestStream)
        {
            var buffer = new byte[8192];

            int count;

            while ((count = file.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (fileData.ContentTransferEncoding == HttpContentTransferEncoding.Base64)
                {
                    string str = Convert.ToBase64String(buffer, 0, count);

                    requestStream.WriteString(str);
                }
                else if (fileData.ContentTransferEncoding == HttpContentTransferEncoding.Binary)
                {
                    requestStream.Write(buffer, 0, count);
                }
            }
        }

        private static string CreateFileBoundaryHeader(FileData fileData)
        {
            return string.Format(
                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                "Content-Type: {2}\r\n" +
                "Content-Transfer-Encoding: {3}\r\n\r\n",
                fileData.FieldName,
                Path.GetFileName(fileData.FileName),
                fileData.ContentType,
                fileData.ContentTransferEncoding);
        }

        private static string CreateFormBoundaryHeader(string name, object value)
        {
            return string.Format("\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", name, value);
        }
    }
}