using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Xml.Linq;

using System.Globalization;
using System.Web.UI;

namespace VerserAssetleasingServiceInterface.Utils
{
    public class PDFReportExporter
    {
       
        public string filePath;
        public string ssn;
        bool IsDiskInserted = false;
        public XDocument ExportedReport { get; private set; }
        public string ErrorMessage { get; private set; }
        public PDFReportExporter( string filePath, string ssn)
        {
          
            this.filePath = filePath;
            this.ssn = ssn;
        }
        public byte[] GetReportXML()
        {
            this.ErrorMessage = null;
            // Read file data
            byte[] data;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);

                string text = Encoding.UTF8.GetString(data);
                text = text.Replace("search_value", ssn);

                data = Encoding.UTF8.GetBytes(text);
                fs.Close();
            }
            catch (Exception)
            {
                return null;
            }

            return ExportReportBytes(data);
        }
        public byte[] ExportReportBytes(byte[] data)
        {
            string serverAddress = ConfigurationManager.AppSettings["ServerAddress"];
            string user = ConfigurationManager.AppSettings["User"];
            string password = ConfigurationManager.AppSettings["Password"];

            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("xmlRequest", new FileParameter(data, "blanccoXML", "application/xml"));

            // Create request and receive response
            string postURL = serverAddress;
            if (!postURL.EndsWith("/"))
                postURL += "/";
            postURL += "rest-service/report/export/pdf";
            string userAgent = "BlanccoXMLReport";
            try
            {
                string formDataBoundary = "-----------------------------28947758029299";
                string contentType = "multipart/form-data; boundary=" + formDataBoundary;
                byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);
                HttpWebRequest request;
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(postURL);
                    if (request == null)
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }

                // Set so we accept all kinds of ssl certs
                // In order to use a cert, that is not always 100% as it should, we need to override the validation check.
                CertificateOverride certificateOverride = new CertificateOverride();
                ServicePointManager.ServerCertificateValidationCallback = certificateOverride.RemoteCertificateValidationCallback;

                // Set up the request properties
                request.Timeout = 180 * 1000; // 3 minutes timeout (180 * 1000 milliseconds)
                request.Method = "POST";
                request.ContentType = contentType;
                request.UserAgent = userAgent;
                request.CookieContainer = new CookieContainer();
                request.ContentLength = formData.Length;  // We need to count how many bytes we're sending. 
                CredentialCache basicCache = new CredentialCache();
                basicCache.Add(new Uri(postURL), "Basic", new NetworkCredential(user, password));
                request.Credentials = basicCache;
                request.Accept = "*/*";
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(user + ":" + password)));

                using (Stream requestStream = request.GetRequestStream())
                {
                    // Push it out there
                    requestStream.Write(formData, 0, formData.Length);
                    requestStream.Close();
                }

                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

                // Process response
                // ExportedReport = XDocument.Load(new StreamReader(webResponse.GetResponseStream()));

                Stream stream1 = webResponse.GetResponseStream();
                StreamReader responseReader1 = new StreamReader(stream1);
                var bytes = default(byte[]);
                using (var memstream = new MemoryStream())
                {
                    responseReader1.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }

                System.IO.File.WriteAllBytes(@"c:/temp/" + ssn + ".pdf", bytes);
                // sr.Read(ds, 0, ds.Length);

               
                //return RedirectToAction("AllBookingEntries", "Home");

                webResponse.Close();
                return bytes;
            }
            catch (WebException web)
            {
                string str = web.ToString();
                HttpWebResponse response = (HttpWebResponse)web.Response;
                if (response != null)
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        if (responseReader != null)
                        {
                            string fullResponse = responseReader.ReadToEnd();
                            ErrorMessage = fullResponse.TrimEnd('\n');
                        }
                    }

                    int statusCode = (int)response.StatusCode;
                    if (response != null)
                        response.Close();
                    return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return null;
            }

            return null;
        }
        public static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new MemoryStream();

            foreach (var param in postParameters)
            {
                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(Encoding.UTF8.GetBytes(header), 0, header.Length);

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(Encoding.UTF8.GetBytes(postData), 0, postData.Length);
                }
            }

            // Add the end of the request
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(Encoding.UTF8.GetBytes(footer), 0, footer.Length);

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
        class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
        class CertificateOverride
        {
            public bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }
        }
    }
}