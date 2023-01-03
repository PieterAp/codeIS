using somiodApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Application = somiodApp.Models.Application;

namespace somiodApp
{
    public partial class AppA : Form
    {
        MqttClient mClient;
        public AppA()
        {
            AppB appB = new AppB();
            appB.Show();

            InitializeComponent();
            getAllApplications();              
        }
        private void btnApplicationName_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44340/api/somiod";
            HttpWebResponse response = postXMLData(url, "<application><name>" + txtNameApp.Text + "</name></application>");

            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Application " + txtNameApp.Text + " was created sucessfully");
                    getAllApplications();
                    cbApp.SelectedIndex = cbApp.FindStringExact(txtNameApp.Text);
                    txtNameApp.Text = "";
                    txtNameModule.Text = "";
                }

                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("An application with such name already exists!");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Body content is not well formated");
                }

            }
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
        private void btnAddSubs_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44340/api/somiod/" + cbApp.Text + "/" + cbModules.Text;

            HttpWebResponse response = postXMLData(url, "<subscription>" +
                "<name>" + txtSubscriptions.Text + "</name>" +
                "<endpoint>" + txtEndpoint.Text + "</endpoint>" +
                "<moduleID>" + cbModules.SelectedValue + "</moduleID>" +
                "<eventType>creation</eventType>" +
                "</subscription>");

            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Subscription " + txtSubscriptions.Text + " was created sucessfully");
                    connectSubscribe(txtEndpoint.Text, cbModules.Text);
                    txtSubscriptions.Text = "";
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("A subscription with such name already exists for this module!");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Body content is not well formated");
                }
            }
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
        public void getModules(string appName)
        {
            cbModules.DataSource = null;
            cbModules.Items.Clear();
            string url = "https://localhost:44340/api/somiod/" + appName;
            List<Module> modules = xmlModuleStringToList(getXMLData(url));
            cbModules.DataSource = modules;
            cbModules.DisplayMember = "name";
            cbModules.ValueMember = "id";

            if (modules.Count == 0)
                gbSubscriptions.Enabled = false;
            else
                gbSubscriptions.Enabled = true;

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
        private void cbApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            getModules(cbApp.Text);
        }
        private void connectSubscribe(string endpoint, string channel)
        {
            string ip = endpoint.Substring(endpoint.IndexOf('/') + 2, endpoint.Length - (endpoint.IndexOf('/') + 2));
            mClient = new MqttClient(ip);
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                MessageBox.Show("Error connecting to message broker...");
                return;
            }

            //This client's subscription operation id done
            mClient.MqttMsgSubscribed += client_MqttMsgSubscribed;
            //New Msg Arrived
            mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS            
            mClient.Subscribe(new string[] { channel }, qosLevels);
        }
        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            lblDataData.Items.Add("---SUBSCRIBED WITH SUCCESS---");
            lblDataData.Items.Add("Now listening in channel " + cbModules.Text);
        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            lblDataData.Items.Add("Received msg: " + message);

            if (message == "on")
            {
                pboff.Visible = false;
                pbon.Visible = true;
            }
            else if (message == "off")
            {
                pbon.Visible = false;
                pboff.Visible = true;
            }
        }
        private void AppA_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}
