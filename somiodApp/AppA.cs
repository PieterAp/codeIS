using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Application = somiodApp.Models.Application;

namespace somiodApp
{
    public partial class AppA : Form
    {
        public AppA()
        {
            InitializeComponent();
            //get all applications
            string url = "https://localhost:44340/api/somiod";

            cbApp.DataSource = xmlStringToList(getXMLData(url));

        }
      
        private void btnApplicationName_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44340/api/somiod";
            postXMLData(url, "<application><name>" + txtNameApp.Text + "</name></application>");
        }

        public string postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response = null;
             
            try
            {
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {                    
                    MessageBox.Show("Application " + txtNameApp.Text + " was created sucessfully");
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        if (resp.StatusCode == HttpStatusCode.Conflict)
                        {
                            MessageBox.Show("An application with such name already exists!");
                        }
                        if (resp.StatusCode == HttpStatusCode.BadRequest)
                        {
                            MessageBox.Show("Body content is not well formated");
                        }
                    }                   
                }                                                                 
            }

            return null;
        }

        public string getXMLData(string destinationUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);       
            request.ContentType = "text/xml; encoding='utf-8'";   
            request.Method = "GET";        
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {                    
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;                   
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        if (resp.StatusCode == HttpStatusCode.Conflict)
                        {
                            MessageBox.Show("An application with such name already exists!");
                        }
                        if (resp.StatusCode == HttpStatusCode.BadRequest)
                        {
                            MessageBox.Show("Body content is not well formated");
                        }
                    }
                }
            }

            return null;
        }

        public List<Application> xmlStringToList(string xmlString)
        {
            XDocument doc = new XDocument();
            //Check for empty string.
            if (!string.IsNullOrEmpty(xmlString))
            {
                doc = XDocument.Parse(xmlString);
            }
            List<Application> applications = new List<Application>();
            //Check if xml has any elements 
            if (!string.IsNullOrEmpty(xmlString) && doc.Root.Elements().Any())
            {
                applications = doc.Descendants("application").Select(d =>
                new Application
                {                
                    id = int.Parse(d.Element("id").Value),
                    name = d.Element("name").Value,
                    creation_dt = DateTime.Parse( d.Element("creation_dt").Value)

                }).ToList();
            }

            return applications;
        }

    }
}
