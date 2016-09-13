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
        private readonly List<NetAddress> _address = new List<NetAddress>();
        readonly List<NetAddress> _deleteList = new List<NetAddress>();
        private bool _noBtn = true;
        private int _rowCount;
        private bool _delete;
        private string _oldData;
        private bool _flag;

        //JUST FOR TEST1
        
        public Form1()
        {            
            InitializeComponent();
            FileStatus();            
            ReadFromFile("old");
            ShowData();
           
        }

        protected void FileStatus()
        {
            
            List<NetAddress> dummy = new List<NetAddress>();
            NetAddress netAddress = new NetAddress();
            netAddress.PcAddress = "1";
            dummy.Add(netAddress);
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string archiveFolder = Path.Combine(currentDirectory, "PortForward.txt");
            string[] data = File.ReadAllLines(archiveFolder);

            if (data.Length==0)
            {
                _flag = true;
                string serializeData = JsonConvert.SerializeObject(dummy);
                StreamWriter writer = new StreamWriter(archiveFolder);
                writer.WriteLine(serializeData);
                writer.Close();
                writer.Dispose();
            }

            else if (data[0] == "" || data[0] == "[]")
            {
                _flag = true;
                string serializeData = JsonConvert.SerializeObject(dummy);
                StreamWriter writer = new StreamWriter(archiveFolder);
                writer.WriteLine(serializeData);
                writer.Close();
                writer.Dispose();
            }
            else _flag = false;

            
          
            
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
                _address.Clear();
                int start = output.LastIndexOf("----------", StringComparison.Ordinal);
                int length = output.Length - start;
                string selectedText = output.Substring(start, length);
                string[] array = selectedText.Split('\r');

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
                        _address.Add(netAddress);
                    }
                }
            }
           
            CheckDataWithFile(_address);
        }

        private void CheckDataWithFile(List<NetAddress> netAddresses)
        {
            var _oldData = JsonConvert.DeserializeObject<List<NetAddress>>(this._oldData);
            var realData = netAddresses;
            List<NetAddress> newlist = new List<NetAddress>();           
                foreach (var old in _oldData)
                {
                    foreach (var real in realData)
                    {
                        if (old.PcAddress == real.PcAddress && old.PcPort == real.PcPort) newlist.Add(old);                        
                    }
 
                }

                List<NetAddress> missing = new List<NetAddress>();
                foreach (var item in realData)
                {
                    int count = 0;
                    foreach (var innerItem in newlist)
                    {
                        if (item.PcAddress != innerItem.PcAddress || item.PcPort != innerItem.PcPort || item.ServerAddress != innerItem.ServerAddress || item.ServerPort != innerItem.ServerPort)
                        {
                            count++;
                            if (count == newlist.Count) missing.Add(item);

                        }
                    }
                    if (newlist.Count == 0 && realData.Count>0)
                    {
                        newlist.Add(item);
                    }
                }
            SaveToFile(newlist.Concat(missing).ToList(), "saveonly");
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
                btn.UseColumnTextForButtonValue = true;
                dgt.CellClick += dataGridView1_CellClick;
                _noBtn = false;
            }           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var col = e.ColumnIndex;
            NetAddress netAddress = new NetAddress();
            var row = e.RowIndex;
            netAddress.ServerName = (string)dgt.Rows[row].Cells["serverName"].Value;
            netAddress.PcAddress = (string)dgt.Rows[row].Cells["pcAddress"].Value;
            netAddress.PcPort = (string)dgt.Rows[row].Cells["pcPort"].Value;
            netAddress.ServerAddress = (string)dgt.Rows[row].Cells["serverAddress"].Value;
            netAddress.ServerPort = (string)dgt.Rows[row].Cells["serverPort"].Value;

            if (dgt.CurrentCell.ColumnIndex.Equals(7) && e.RowIndex != -1)
            {               
                 dgt.Rows[row].Visible = false;
                _deleteList.Add(netAddress);               
            }
            else if (dgt.CurrentCell.ColumnIndex.Equals(4))
            {                
                var permission = dgt.Rows[e.RowIndex].Cells[4].Tag;
                DialogResult confirmResult;              
                if (permission == "no")
                {
                    confirmResult = MessageBox.Show(Resources.Form1_dataGridView1_CellClick_Are_you_sure_to_Add_Firewall_Permission___, Resources.Form1_dataGridView1_CellClick_Firewall_Permission__, MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    FirewallPermission(netAddress,"add");                    
                }
                else
                {
                     confirmResult = MessageBox.Show(@"Are you sure you want to remove Firewall Permission??","Firewall Permission",MessageBoxButtons.YesNo);
                     if (confirmResult == DialogResult.Yes)                       
                     FirewallPermission(netAddress, "remove");

                }
            }
            if (col != 7 && col != -1)
                dgt.BeginEdit(true);

        }     

        private void FirewallPermission(NetAddress netAddress, string action)
        {
            ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
            si.WindowStyle = ProcessWindowStyle.Hidden;
            si.RedirectStandardInput = true;            
            si.UseShellExecute = false;
            si.CreateNoWindow = true;
            Process p = Process.Start(si);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (p != null)
            {
                //netsh advfirewall firewall delete rule name=rule name protocol=udp localport=500 
                ////netsh advfirewall firewall add rule name="test" dir=in action=allow enable=yes protocol=tcp localip=123123 localport=asdas remoteip=asdas remoteport=adsa
                string command;
                if(action=="remove")
                   command =@"netsh advfirewall firewall delete rule name=" + netAddress.ServerName + " protocol=tcp  localport=" + netAddress.PcPort;
                else
                   command = @"netsh advfirewall firewall add rule name=" + netAddress.ServerName +
                          " dir=in action=allow enable=yes protocol=tcp localip=" + netAddress.PcAddress + " localport=" +
                          netAddress.PcPort + " remoteip=" + netAddress.ServerAddress + " remoteport=" +
                          netAddress.ServerPort;

                p.StandardInput.WriteLine(command);
                p.StandardInput.WriteLine(@"exit");                                              
                p.Close();
            }
            ShowData();
        }

        private int checkFirewall(string name)
        {
            ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
            si.WindowStyle =  ProcessWindowStyle.Hidden;
            si.RedirectStandardInput = true;
            si.RedirectStandardOutput = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;
            Process p = Process.Start(si);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            p.StandardInput.WriteLine(@"netsh advfirewall firewall show rule name=" + name + " dir=in");
            p.StandardInput.WriteLine(@"exit");
            string output = p.StandardOutput.ReadToEnd();
            int exist = output.IndexOf("No rules match the specified criteria", StringComparison.Ordinal);
            p.Close();
            return exist;
        }

        private void ShowData()
        {
            ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
            si.WindowStyle = ProcessWindowStyle.Hidden;
            si.RedirectStandardInput = true;
            si.RedirectStandardOutput = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;
            Process p = Process.Start(si);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (p != null)
            {
                p.StandardInput.WriteLine(@"netsh interface portproxy show all");
                p.StandardInput.WriteLine(@"exit");                
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
                si.WindowStyle = ProcessWindowStyle.Hidden;
                si.RedirectStandardInput = true;
                si.RedirectStandardOutput = true;
                si.UseShellExecute = false;
                si.CreateNoWindow = true;
                Process p = Process.Start(si);
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (p != null)
                {
                    p.StandardInput.WriteLine(cmd);
                    p.StandardInput.WriteLine(@"exit");
                    p.Close();
                }                
            }
            if (action == "add")
                SaveToFile(netAddress);
            else if (netAddress.Count>0)
            {
                DeleteFromFile(netAddress, deletelist);
            }
        }

        private void DeleteFromFile(List<NetAddress> netAddress, int[] arr)
        {
            List<NetAddress> newList = new List<NetAddress>();
            var _oldData = JsonConvert.DeserializeObject<List<NetAddress>>(this._oldData);
            foreach (var item in _oldData)
            {
                int count = 0;
                foreach (var deletelist in netAddress)
                {
                    if (item.PcAddress != deletelist.PcAddress || item.PcPort != deletelist.PcPort)
                    {
                        count++;
                        if (count == netAddress.Count)
                            newList.Add(item);
                    }
                }
            }
            _deleteList.Clear();
            SaveToFile(newList, "saveonly");
        }

        private void SaveToFile(List<NetAddress> netAddress, string saveonly = null)
        {
            string serializeData = JsonConvert.SerializeObject(netAddress);
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string archiveFolder = Path.Combine(currentDirectory, "PortForward.txt");
            StreamWriter writer = new StreamWriter(archiveFolder);
            writer.WriteLine(serializeData);               
            writer.Close(); 
            writer.Dispose();            
            if (saveonly != "saveonly") ReadFromFile();            
            else ReadFromFile("old");            
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
                    _oldData = data[0];
                    DisplayTable(null, _oldData);
                }
            }
            else DisplayTable(data[0], _oldData);
          
        }

        private void DisplayTable(string data = null, string olddata = null)
        {
            List<NetAddress> alldata;

            var oldData = JsonConvert.DeserializeObject<List<NetAddress>>(olddata);
            if (data != null)
            {
                var newData = JsonConvert.DeserializeObject<List<NetAddress>>(data);
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
                string archiveFolder;                            
               
                var serverName = (string)alldata[row].ServerName;
                var isFirewallAllowed = checkFirewall(serverName);
                
                if (isFirewallAllowed > 1)
                {
                    archiveFolder = Path.Combine(currentDirectory, "red32.png");
                    dgt.Rows[row].Cells["firewallPermission"].Tag = "no";
                }
                else
                {
                    archiveFolder = Path.Combine(currentDirectory, "green32.png");
                    dgt.Rows[row].Cells["firewallPermission"].Tag = "yes";
                }
                

                dgt.Rows[row].Cells["serverName"].Value = serverName;
                dgt.Rows[row].Cells["accessType"].Value = alldata[row].AccessType;
                dgt.Rows[row].Cells["pcAddress"].Value = alldata[row].PcAddress;
                dgt.Rows[row].Cells["pcPort"].Value = alldata[row].PcPort;
                //dgt.Rows[row].Cells["firewallPermission"].Value = alldata[row].FirewallPermission;
                dgt.Rows[row].Cells["firewallPermission"].Value = Image.FromFile(archiveFolder);
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
                    btn.UseColumnTextForButtonValue = true;                   
                    dgt.CellClick += dataGridView1_CellClick;
                    _noBtn = false;
                }
               
                    _rowCount = dgt.Rows.Count;
               
            }

        }


        private void _add_Click(object sender, EventArgs e)
        {           
            dgt.Rows.Add();           
        }

        private void Save_Click(object sender, EventArgs e)
        {
          
            FileStatus();
            if (_flag) _rowCount = 0;           
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
            NetshCommand(_deleteList, "delete");
            ShowData();
  
        }
  

        private void btnCurrentIp_Click(object sender, EventArgs e)
        {
            string ip = GetIp();
            foreach (DataGridViewRow row in dgt.Rows)
            {
                row.Cells[2].Value = ip;
            }
        }

        public string GetIp()
        {
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            return ip.AddressList[4].ToString();
        }


    }

    public class NetAddress
    {
        public string ServerName { get; set; }
        public string AccessType { get; set; }
        public string PcAddress { get; set; }
        public string PcPort { get; set; }
        public string FirewallPermission { get; set; }
        public string ServerAddress { get; set; }
        public string ServerPort { get; set; }

    }
}
