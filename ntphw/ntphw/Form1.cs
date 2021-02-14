using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntphw
{
    public partial class Form1 : Form
    {
        graf graf = new graf();
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        bool uretildi = false;


        static byte[] binary;
        static List<int> subset;
        static int[] set;
        static int n;
        static List<string> allsubsets = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string JSONstring = JsonConvert.SerializeObject(graf);


            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Grafi Kaydedecek Yeri Secin";
            save.Filter = "JSON (*.json)|*.json";

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.Write(JSONstring);
                write.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Lutfen Graf Seciniz";
            open.Filter = "JSON (*.json)|*.json";

            if (open.ShowDialog() == DialogResult.OK)
            {
                StreamReader read = new StreamReader(File.OpenRead(open.FileName));
                string readresult = read.ReadToEnd();
                graf = JsonConvert.DeserializeObject<graf>(readresult);
                label1.Text = "Grafi Basariyla Sectiniz Algoritmalari Uygulayabilirisiniz";
                label1.ForeColor = Color.Green;
                button1.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int n;
            if(uretildi)
            { button4.Enabled = false;
                label4.Text = "Graf Zaten Olusturuldu";
                label4.ForeColor = Color.Red;
            }
            else if (!String.IsNullOrEmpty(textBox1.Text) && int.TryParse(textBox1.Text, out n) && n < 17)
            {
                button4.Enabled = true;
                label4.Text = "Gecerli Sayi Girdiniz Grafi Olusturabilirsiniz";
                label4.ForeColor = Color.Green;
            }
             else
            {
                label4.Text = "Gecerli ve 17'ten kucuk bir sayi girin";
                label4.ForeColor = Color.Red;
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int vertex = Convert.ToInt32(textBox1.Text);
            int pair = 0;
            for (int i = 1; i < vertex; i++)
            {   
                Random rand = new Random();
                int totaledges = rand.Next(1, vertex);
                for (int j = 0; j < totaledges; j++)
                {
                    pair = rand.Next(i+1,vertex+1);
                    graf.edges.Add(Convert.ToString(i) + "-" + Convert.ToString(pair));
                }

                
            }


            graf.edges = graf.edges.Distinct().ToList();



            foreach (string edge in graf.edges)
            {
                string[] edgepairs = edge.Split('-');

                if (graf.vertices.ContainsKey(edgepairs[0]))
                {
                    graf.vertices[edgepairs[0]]++;
                }
                else
                {
                    graf.vertices.Add(edgepairs[0],1);
                }
                if (graf.vertices.ContainsKey(edgepairs[1]))
                {
                    graf.vertices[edgepairs[1]]++;
                }
                else
                {
                    graf.vertices.Add(edgepairs[1],1);
                }
            }

            






            label1.Text = "Grafi Basariyla Olusturdunuz Algoritmalari Uygulayabilirisiniz";
            label1.ForeColor = Color.Green;
            button1.Enabled = true;
            button2.Enabled = true;
            button5.Enabled = true;
            button4.Enabled = false;
            uretildi = true;
            
        }

        private void previsgraph()
        {
            foreach (string edge in graf.edges)
            {
                string[] edgepairs = edge.Split('-');

                graph.AddEdge(edgepairs[0], edgepairs[1]);
                graph.AddEdge(edgepairs[1], edgepairs[0]);
            }

            viewer.Graph = graph;
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //Getting set size

            int setsize = graf.vertices.Count;

            //Creating all subsets of graph

            
            n = setsize;
            
            
            

            set = new int[n];
            subset = new List<int>(n);
            binary = new byte[n];
            string[] items= new string[n];

            for (int i = 0; i < setsize; i++)
            {
                items[i] = Convert.ToString(i+1);
            }


            for (int i = 0; i < items.Length; i++)
            {
                int.TryParse(items[i], out set[i]);
            }

            
            int size = 2 << (n - 1);

            for (int i = 0; i < size; i++)
            {
                Createsubset();
                BinaryIncrement(n);
            }

            allsubsets = allsubsets.OrderBy(n => n.Length).ToList();




            Form2 form2 = new Form2(graf,allsubsets);
            form2.ShowDialog();






            
            
        }
        static void BinaryIncrement(int n)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                if (binary[i] == 0)
                {
                    binary[i] = 1;
                    return;
                }
                binary[i] = 0;
            }
        }
        static void Createsubset()
        {
            subset.Clear();

            for (int i = n - 1; i >= 0; i--)
            {
                if (binary[i] == 1) subset.Add(set[n - 1 - i]);
            }

            
            string tolist = "";
            foreach (int number in subset)
            {
                tolist += Convert.ToString(number)+",";
            }
            tolist = tolist.TrimEnd(',');
            allsubsets.Add(tolist);

            


        }
        IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            
            var sorted = from s in e
                         orderby s.Length ascending
                         select s;
            return sorted;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(graf);
            form3.ShowDialog();
        }
    }
}
