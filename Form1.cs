using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NetworkWinApp.Properties;
using Newtonsoft.Json;

namespace NetworkWinApp
{
    public partial class Form1 : Form
    {
        List<NetAddress> address = new List<NetAddress>();
        private bool _noBtn = true;
        private int _rowCount;
        private bool _delete;
        readonly List<int> _deleteIndex = new List<int>(); 
        public Form1()
        {
            InitializeComponent();
            ShowData();
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;
            g = e.Graphics;
            Pen myPen = new Pen(Color.Gray);
            myPen.Width = 1;
            g.DrawLine(myPen, 43, 55, 795, 55);
           
        }
      
     

        private void SaveData(string output)
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

            SaveAddress(address);

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
                _rowCount = dgt.Rows.Count;
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

           // dgt.ColumnHeadersHeight
           // dgt.CurrentCell.Selected=false;


        }

        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var col = e.ColumnIndex;
           if (dgt.CurrentCell.ColumnIndex.Equals(4) && e.RowIndex != -1)
           {           
                var row = e.RowIndex;
               _deleteIndex.Add(row);
               dgt.Rows[row].Visible = false;
               //dgt.Rows.RemoveAt(row);
               _delete = true;
               
           }

           if (col != 4 && col != -1)
           {
               
               dgt.BeginEdit(true);

           }

           

           
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

    

        private void NetshCommand(List<NetAddress> netAddress, string action)
        {
            // ReSharper disable once InconsistentNaming
            foreach (var _netAddress in netAddress)
            {


                string cmd = @"netsh interface portproxy show all";
                if (action == "delete")
                {
                    cmd = @"netsh interface portproxy delete v4tov4 listenport=" + _netAddress.PcPort + " listenaddress=" +
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
                ShowData();
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

        }

        private void Save_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<NetAddress> addList = new List<NetAddress>();
            
            foreach (DataGridViewRow row in dgt.Rows)
            {
                NetAddress netAddress = new NetAddress();
                 count++;
                if (count > _rowCount)
                {
                    netAddress.PcAddress = (string)row.Cells[0].Value;
                    netAddress.PcPort = (string)row.Cells[1].Value;
                    netAddress.ServerAddress = (string)row.Cells[2].Value;
                    netAddress.ServerPort = (string)row.Cells[3].Value;
                    addList.Add(netAddress);
                    
                }
                
            }
            NetshCommand(addList, "add");

            if (_delete)
            {
                
                List<NetAddress> deleteList = new List<NetAddress>();
                int[] arr = _deleteIndex.ToArray();
                foreach (DataGridViewRow row in dgt.Rows)
                {
                    NetAddress netAddress = new NetAddress();
                    if (arr.Contains(row.Index))
                    {
                        
                        netAddress.PcAddress = (string)row.Cells[0].Value;
                        netAddress.PcPort = (string)row.Cells[1].Value;
                        deleteList.Add(netAddress);
                    }
                    

                }
                NetshCommand(deleteList, "delete");
            }
        }

      
        
    }

    public class NetAddress
    {

        public string PcAddress;
        public string PcPort;
        public string ServerAddress;
        public string ServerPort;

    }
}
