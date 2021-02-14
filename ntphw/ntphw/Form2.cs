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
    public partial class Form2 : Form
    {
        graf graf = new graf();
        private static graf grafd = new graf();
        
        List<string> allsubsets;
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        

        public Form2(graf grafx,List<string> subsets)
        {
            InitializeComponent();
            
            grafd = grafx;
            graf = grafx;  
            allsubsets = subsets;
        }

        private void Form2_Load(object sender, EventArgs e)
        {


            List<string> wbr = new List<string>();
            int whichset=0;

            


            for (int i = 1; i < allsubsets.Count; i++)
            {

                List<string> testlist = graf.edges.ToList() ;
                

                string[] subsetvertices = allsubsets[i].Split(',');

                foreach (string s in subsetvertices)
                {
                    foreach (string ss in graf.edges)
                    {
                        string[] check = ss.Split('-');
                        if(s==check[0] || s==check[1])
                        {
                            string adddd = ss;
                            wbr.Add(adddd);
                        }
                    }

                            
                }

                for (int j = 0; j < wbr.Count; j++)
                {
                    testlist.Remove(wbr[j]);
                }


                if (!testlist.Any())
                {
                    whichset = i;
                    break;
                }
                wbr.Clear();
            }

            
            MessageBox.Show(allsubsets[whichset],"Minimum Ortu Tepesi", MessageBoxButtons.OK);





             foreach (string edge in grafd.edges)
            {
                string[] edgepairs = edge.Split('-');

                graph.AddEdge(edgepairs[0], edgepairs[1]);
                graph.AddEdge(edgepairs[1], edgepairs[0]);
                Microsoft.Msagl.Drawing.Node c = graph.FindNode(edgepairs[0]);
                Microsoft.Msagl.Drawing.Node d = graph.FindNode(edgepairs[1]);
                c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                d.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            
        }

            string[] verticearr = allsubsets[whichset].Split(',');

            for (int i = 0; i < verticearr.Length; i++)
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
