using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NetworkWinApp.Properties;
using Newtonsoft.Json;

namespace NetworkWinApp
{
    public partial class Form1 : Form
    {
        private List<NetAddress> address = new List<NetAddress>();
        private bool _noBtn = true;
        private int _rowCount;
        private bool _delete;
        private readonly List<int> _deleteIndex = new List<int>();
        private string oldData;

        public Form1()
        {
            InitializeComponent();
            
            
            ReadFromFile("old");
            ShowData();
        }

        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;
            g = e.Graphics;
            Pen myPen = new Pen(Color.Gray);
            myPen.Width = 1;
            g.DrawLine(myPen, 43, 55, 1140, 55);

        }

        private void SaveData(string output)
        {
            if (output.Length > 260)
            {
                address.Clear();
                int start = output.LastIndexOf("----------", StringComparison.Ordinal);
                int length = output.Length - start;
                string selectedText = output.Substring(start, length);
                string[] array = selectedText.Split('\r');


                //Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string[] addr;
                for (int i = 1; i < array.Length - 2; i++)
                {
                    NetAddress netAddress = new NetAddress();
                    if (array[i] != "\n")
                    {
                        Regex r = new Regex(@"\s+");
                        addr = r.Replace(array[i].Replace("\n", ""), @" ").Split(' ');
                        netAddress.PcAddress = addr[0];
                        netAddress.PcPort = addr[1];
                        netAddress.ServerAddress = addr[2];
                        netAddress.ServerPort = addr[3];
                        address.Add(netAddress);
                    }

                }
            }
            //SaveAddress(address);
            CheckDataWithFile(address);
        }

        private void CheckDataWithFile(List<NetAddress> netAddresses)
        {
            var _oldData = JsonConvert.DeserializeObject<List<NetAddress>>(oldData);
            var realData = netAddresses;
            List<NetAddress> newlist = new List<NetAddress>();
            int count = 0;
                foreach (var old in _oldData)
                {
                    foreach (var real in realData)
                    {
                        if (old.PcAddress == real.PcAddress && old.PcPort == real.PcPort)
                        {
                            //var address = new NetAddress(old.ServerName,old.AccessType,old.PcAddress,old.PcPort,old.FirewallPermission,old.ServerAddress,old.ServerPort);

                            newlist.Add(old);

                        }
                    }
 
                }

        }


        public void SaveAddress(List<NetAddress> netAddress)
        {

            string abc = JsonConvert.SerializeObject(netAddress);
            var abc1 = JsonConvert.DeserializeObject<List<NetAddress>>(abc);

            dgt.Rows.Clear();
            for (int row = 0; row < abc1.Count; row++)
            {

                dgt.Rows.Add();
                dgt.Rows[row].Cells["pcAddress"].Value = abc1[row].PcAddress;
                dgt.Rows[row].Cells["pcPort"].Value = abc1[row].PcPort;
                dgt.Rows[row].Cells["serverAddress"].Value = abc1[row].ServerAddress;
                dgt.Rows[row].Cells["serverPort"].Value = abc1[row].ServerPort;


            }
            if (_noBtn)
            {
                //_rowCount = dgt.Rows.Count;
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dgt.Columns.Add(btn);
                btn.HeaderText = Resources.Form1_SaveAddress_Action;
                btn.Text = "Delete";
                btn.Name = "btn_delete";
                btn.FlatStyle = FlatStyle.Flat;
                btn.DefaultCellStyle.ForeColor = Color.White;
                btn.DefaultCellStyle.BackColor = Color.Crimson;
                btn.DefaultCellStyle.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);

                //btn.DefaultCellStyle.
                btn.UseColumnTextForButtonValue = true;

                //dgt.Columns.Add(btn);

                dgt.CellClick += dataGridView1_CellClick;
                _noBtn = false;
            }
            _rowCount = dgt.Rows.Count;
            // dgt.ColumnHeadersHeight
            // dgt.CurrentCell.Selected=false;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var col = e.ColumnIndex;
            if (dgt.CurrentCell.ColumnIndex.Equals(7) && e.RowIndex != -1)
            {
                var row = e.RowIndex;
                _deleteIndex.Add(row);
                dgt.Rows[row].Visible = false;
                //dgt.Rows.RemoveAt(row);
                _delete = true;
            }

            if (col != 7 && col != -1)
                dgt.BeginEdit(true);

        }


        private void ShowData()
        {
            ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
            // Redirect both streams so we can write/read them.
            si.RedirectStandardInput = true;
            si.RedirectStandardOutput = true;
            si.UseShellExecute = false;
            // Start the procses.
            Process p = Process.Start(si);
            if (p != null)
            {
                p.StandardInput.WriteLine(@"netsh interface portproxy show all");
                p.StandardInput.WriteLine(@"exit");
                // Read all the output generated from it.
                string output = p.StandardOutput.ReadToEnd();
                SaveData(output);
                p.Close();
            }
        }

        private void NetshCommand(List<NetAddress> netAddress, string action, int[] deletelist = null)
        {
            // ReSharper disable once InconsistentNaming
            foreach (var _netAddress in netAddress)
            {

                string cmd = @"netsh interface portproxy show all";
                if (action == "delete")
                {
                    cmd = @"netsh interface portproxy delete v4tov4 listenport=" + _netAddress.PcPort +
                          " listenaddress=" +
                          _netAddress.PcAddress;
                }
                if (action == "add")
                {
                    cmd = @"netsh interface portproxy add v4tov4 listenport=" + _netAddress.PcPort + " listenaddress=" +
                          _netAddress.PcAddress + " connectport=" + _netAddress.ServerPort + " connectaddress=" +
                          _netAddress.ServerAddress;

                }

                ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
                si.RedirectStandardInput = true;
                si.RedirectStandardOutput = true;
                si.UseShellExecute = false;
                Process p = Process.Start(si);
                if (p != null)
                {
                    p.StandardInput.WriteLine(cmd);
                    p.StandardInput.WriteLine(@"exit");
                    p.Close();
                }
                //ShowData();

            }
            if (action == "add")
                SaveToFile(netAddress);
            else
            {
                DeleteFromFile(netAddress, deletelist);
            }
        }

        private void DeleteFromFile(List<NetAddress> netAddress, int[] arr)
        {
            var _oldData = JsonConvert.DeserializeObject<List<NetAddress>>(oldData);
            int count = _oldData.Count;
            for (int row = count - 1; row >= 0; row--)
            {

                if (arr.Contains(row))
                {
                    _oldData.RemoveAt(row);
                }
            }
            SaveToFile(_oldData, "saveonly");
        }

        private void SaveToFile(List<NetAddress> netAddress, string saveonly = null)
        {
            string serializeData = JsonConvert.SerializeObject(netAddress);
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string archiveFolder = Path.Combine(currentDirectory, "PortForward.txt");
            StreamWriter writer = new StreamWriter(archiveFolder);
            writer.WriteLine(serializeData);
                //write the current date to the file. change this with your date or something.
            writer.Close(); //remember to close the file again.
            writer.Dispose();
            //var abc1 = JsonConvert.DeserializeObject<List<NetAddress>>(serializeData);
            if (saveonly != "saveonly")
            {
                ReadFromFile();
            }
            else
            {
                ReadFromFile("old");
            }
        }



        private void ReadFromFile(string old = null)
        {

            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string archiveFolder = Path.Combine(currentDirectory, "PortForward.txt");
            string[] data = File.ReadAllLines(archiveFolder);
            if (old == "old")
            {
                if (data.Length > 0)
                {
                    oldData = data[0];
                    DisplayTable(null, oldData);
                }
            }
            else
            {
                DisplayTable(data[0], oldData);
            }


        }

        private void DisplayTable(string data = null, string olddata = null)
        {
            List<NetAddress> alldata = new List<NetAddress>();
            List<NetAddress> newData = null;

            var oldData = JsonConvert.DeserializeObject<List<NetAddress>>(olddata);
            if (data != null)
            {
                newData = JsonConvert.DeserializeObject<List<NetAddress>>(data);
                alldata = oldData.Concat(newData).ToList();
                SaveToFile(alldata, "saveonly");
            }
            else
            {
                alldata = oldData;
            }


            dgt.Rows.Clear();
            for (int row = 0; row < alldata.Count; row++)
            {

                dgt.Rows.Add();

                string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string archiveFolder = Path.Combine(currentDirectory, "1.png");
                // if ((int.Parse(alldata[row].FirewallPermission) > 0))
                //  {
                archiveFolder = Path.Combine(currentDirectory, "2.png");
                // }


                dgt.Rows[row].Cells["serverName"].Value = alldata[row].ServerName;
                dgt.Rows[row].Cells["accessType"].Value = alldata[row].AccessType;
                dgt.Rows[row].Cells["pcAddress"].Value = alldata[row].PcAddress;
                dgt.Rows[row].Cells["pcPort"].Value = alldata[row].PcPort;
                dgt.Rows[row].Cells["firewallPermission"].Value = alldata[row].FirewallPermission;
                //dgt.Rows[row].Cells["firewallPermission"].Value = Image.FromFile(archiveFolder);
                dgt.Rows[row].Cells["serverAddress"].Value = alldata[row].ServerAddress;
                dgt.Rows[row].Cells["serverPort"].Value = alldata[row].ServerPort;

                if (_noBtn)
                {

                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    dgt.Columns.Add(btn);
                    btn.HeaderText = Resources.Form1_SaveAddress_Action;
                    btn.Text = "Delete";
                    btn.Name = "btn_delete";
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.DefaultCellStyle.ForeColor = Color.White;
                    btn.DefaultCellStyle.BackColor = Color.Crimson;
                    btn.DefaultCellStyle.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);

                    //btn.DefaultCellStyle.
                    btn.UseColumnTextForButtonValue = true;
                    //dgt.Columns.Add(btn);
                    dgt.CellClick += dataGridView1_CellClick;
                    _noBtn = false;
                }
                _rowCount = dgt.Rows.Count;
            }

        }

        private void _add_Click(object sender, EventArgs e)
        {
            //rowCount++;
            dgt.Rows.Add();
            dgt.Columns[0].ReadOnly = false;
            dgt.Columns[1].ReadOnly = false;
            dgt.Columns[2].ReadOnly = false;
            dgt.Columns[3].ReadOnly = false;
            dgt.Columns[4].ReadOnly = false;
            dgt.Columns[5].ReadOnly = false;
            dgt.Columns[6].ReadOnly = false;


        }

        private void Save_Click(object sender, EventArgs e)
        {
            // ReadFromFile();
            if (!_delete)
            {
                int count = 0;
                List<NetAddress> addList = new List<NetAddress>();
                foreach (DataGridViewRow row in dgt.Rows)
                {
                    NetAddress netAddress = new NetAddress();
                    count++;
                    if (count > _rowCount)
                    {

                        netAddress.ServerName = (string) row.Cells[0].Value;
                        netAddress.AccessType = (string) row.Cells[1].Value;
                        netAddress.PcAddress = (string) row.Cells[2].Value;
                        netAddress.PcPort = (string) row.Cells[3].Value;
                        netAddress.FirewallPermission = (string) row.Cells[4].Value;
                        netAddress.ServerAddress = (string) row.Cells[5].Value;
                        netAddress.ServerPort = (string) row.Cells[6].Value;
                        addList.Add(netAddress);

                    }

                }
                NetshCommand(addList, "add");
            }
            else
            {

                List<NetAddress> deleteList = new List<NetAddress>();
                int[] arr = _deleteIndex.ToArray();
                foreach (DataGridViewRow row in dgt.Rows)
                {
                    NetAddress netAddress = new NetAddress();
                    if (arr.Contains(row.Index))
                    {

                        netAddress.PcAddress = (string) row.Cells[2].Value;
                        netAddress.PcPort = (string) row.Cells[3].Value;
                        deleteList.Add(netAddress);
                    }
                }
                NetshCommand(deleteList, "delete", arr);
            }
        }

        private void dgt_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[4].Value = Resources.Image1;
        }

        private void btnCurrentIp_Click(object sender, EventArgs e)
        {
            string ip = getIP();
            foreach (DataGridViewRow row in dgt.Rows)
            {
                row.Cells[2].Value = ip;
            }
        }

        public string getIP()
        {
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            return ip.AddressList[4].ToString();
        }


    }

    public class NetAddress
    {
        public NetAddress(string serverName, string accessType, string pcAddress, string pcPort, string firewallPermission, string serverAddress, string serverPort)
        {
            ServerName = serverName;
            AccessType = accessType;
            PcAddress = pcAddress;
            PcPort = pcPort;
            FirewallPermission = firewallPermission;
            ServerAddress = serverAddress;
            ServerPort = serverPort;
        }

        public NetAddress()
        {
            
        }

        public string ServerName { get; set; }
        public string AccessType { get; set; }
        public string PcAddress { get; set; }
        public string PcPort { get; set; }
        public string FirewallPermission { get; set; }
        public string ServerAddress { get; set; }
        public string ServerPort { get; set; }

    }
}
