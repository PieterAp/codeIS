using somiodApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Application = somiodApp.Models.Application;

namespace somiodApp
{
    public partial class AppB : Form
    {
        public AppB()
        {
            InitializeComponent();
            getAllApplications();
        }

        private void btnAddModule_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44340/api/somiod/" + cbApp.Text;
            HttpWebResponse response = postXMLData(url, "<module><name>" + txtNameModule.Text + "</name><applicationID>" + cbApp.SelectedValue + "</applicationID></module>");
            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Module " + txtNameModule.Text + " was created sucessfully");
                    getModules(cbApp.Text);
                    cbModules.SelectedIndex = cbModules.FindStringExact(txtNameModule.Text);
                    txtNameModule.Text = "";

                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("An module with such name already exists for this application!");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Body content is not well formated");
                }
            }
        }
        public List<Module> xmlModuleStringToList(string xmlString)
        {
            XDocument doc = new XDocument();
            //Check for empty string.
            if (!string.IsNullOrEmpty(xmlString))
            {
                doc = XDocument.Parse(xmlString);
            }
            List<Module> modules = new List<Module>();
            //Check if xml has any elements 
            if (!string.IsNullOrEmpty(xmlString) && doc.Root.Elements().Any())
            {
                modules = doc.Descendants("module").Select(d =>
                new Module
                {
                    id = int.Parse(d.Element("id").Value),
                    name = d.Element("name").Value,
                    creation_dt = DateTime.Parse(d.Element("creation_dt").Value),
                    applicationID = int.Parse(d.Element("applicationID").Value)

                }).ToList();
            }

            return modules;
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
        public HttpWebResponse postXMLData(string destinationUrl, string requestXml)
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

                return response;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    return resp;
                }
            }

            return null;
        }
        public void getModules(string appName)
        {
            cbModules.DataSource = null;
            cbModules.Items.Clear();
            string url = "https://localhost:44340/api/somiod/" + appName;
            List<Module> modules = xmlModuleStringToList(getXMLData(url));
            cbModules.DataSource = modules;
            cbModules.DisplayMember = "name";
            cbModules.ValueMember = "id";           
        }
        public void getAllApplications()
        {
            string url = "https://localhost:44340/api/somiod";
            List<Application> applications = xmlApplicationStringToList(getXMLData(url));
            cbApp.SelectedIndexChanged -= new EventHandler(cbApp_SelectedIndexChanged);
            cbApp.DataSource = applications;
            cbApp.DisplayMember = "name";
            cbApp.ValueMember = "id";
            cbApp.SelectedIndexChanged += new EventHandler(cbApp_SelectedIndexChanged);

            if (applications.Count != 0)
            {
                gbModules.Enabled = true;
                getModules(applications[0].name);
            }
        }
        public List<Application> xmlApplicationStringToList(string xmlString)
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
                    creation_dt = DateTime.Parse(d.Element("creation_dt").Value)

                }).ToList();
            }

            return applications;
        }
        private void cbApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            getModules(cbApp.Text);
        }     
        private void btSend_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44340/api/somiod/" + cbApp.Text+"/"+cbModules.Text;
            string xmlString = "<data><content>"+tbmessage.Text+"</content></data>";           
            HttpWebResponse response =  postXMLData(url, xmlString);
            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Data " + txtNameModule.Text + " was sent sucessfully");                   
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("An module with such name already exists for this application!");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Body content is not well formated");
                }
            }
        }
    }
}
