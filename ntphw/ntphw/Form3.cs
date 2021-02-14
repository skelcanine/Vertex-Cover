using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntphw
{
    public partial class Form3 : Form
    {

        graf graf = new graf();
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        public Form3(graf grafx)
        {
            InitializeComponent();
            graf = grafx;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            List<string> testlist = new List<string>();
            List<string> verticelist = new List<string>();
            testlist = graf.edges.ToList();

            while(testlist.Any())
            {
                Random rand =new Random();
                int rastgele = rand.Next(0, testlist.Count);
                string[] remove = testlist[rastgele].Split('-');
                List<string> removelist = new List<string>();

                foreach (string s in testlist)
                {
                    string[] wbt=s.Split('-');

                    if (remove[0]==wbt[0] || remove[0] == wbt[1] || remove[1] == wbt[0] || remove[1] == wbt[1] )
                    {
                        removelist.Add(s);
                        verticelist.Add(remove[0]);
                        verticelist.Add(remove[1]);
                    }
                }

                for (int j = 0; j < removelist.Count; j++)
                {
                    testlist.Remove(removelist[j]);
                }

            }
            verticelist = verticelist.Distinct().ToList();
            string vertices="";
            foreach (string s in verticelist)
            {
                vertices += (s+",");
            }

            MessageBox.Show(vertices, "Minimum Ortu Tepesi", MessageBoxButtons.OK);

            foreach (string edge in graf.edges)
            {
                string[] edgepairs = edge.Split('-');

                graph.AddEdge(edgepairs[0], edgepairs[1]);
                graph.AddEdge(edgepairs[1], edgepairs[0]);
                Microsoft.Msagl.Drawing.Node c = graph.FindNode(edgepairs[0]);
                Microsoft.Msagl.Drawing.Node d = graph.FindNode(edgepairs[1]);
                c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                d.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }

            string[] verticearr = vertices.Split(',');

            for (int i = 0; i < verticearr.Length-1; i++)
            {
                graph.FindNode(verticearr[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            }


            viewer.Graph = graph;
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
        }
    }
}
