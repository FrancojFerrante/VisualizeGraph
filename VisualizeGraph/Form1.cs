using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VisualizeGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        Microsoft.Msagl.Drawing.Graph graph;
        private void Form1_Load(object sender, EventArgs e)
        {
            // creo el objeto viewer
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            // creo el objeto graph 
            graph = new Microsoft.Msagl.Drawing.Graph("graph");

            // Asocio el grafo al viewer
            viewer.Graph = graph;

            // Asocio el viewer con el splitContainer 
            splitContainer1.Panel2.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(viewer);
            splitContainer1.Panel2.ResumeLayout();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Función que dispara al clickearse el btnAgregar. Agrega un nodo al graph
            // Si un nodo con el mismo nombre no fue agregado anteriormente, lo agrego a mis comboBoxs.
            Microsoft.Msagl.Drawing.Node nodoAux = graph.Nodes.FirstOrDefault(q => q.Id == txtNombreNodo.Text);
            if (nodoAux == null)
            {
                graph.AddNode(txtNombreNodo.Text).Attr.Id = txtNombreNodo.Text; // Agrego un nuevo nodo y se le agrega el id que está en el txtNombreNodo
                graph.FindNode(txtNombreNodo.Text).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue; // Le pongo color azul
                viewer.Graph = graph;
                splitContainer1.Panel2.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(viewer);
                splitContainer1.Panel2.ResumeLayout();
                cmbDesde.Items.Add(txtNombreNodo.Text); // Agrego el nuevo nodo a los comboBox
                cmbHasta.Items.Add(txtNombreNodo.Text);
            }



        }

        private void btnAgregarConexion_Click(object sender, EventArgs e)
        {
            // Agrego un borde con su Id
            graph.AddEdge(cmbDesde.SelectedItem.ToString(), cmbHasta.SelectedItem.ToString()).Attr.Id = txtNombreConexion.Text;
            //y le seteo la punta de la flecha muy chiquita para que simule un grafo no direccional
            graph.EdgeById(txtNombreConexion.Text).Attr.ArrowheadLength = 0.1;
            // Le asigno un peso
            graph.EdgeById(txtNombreConexion.Text).Attr.Weight = Convert.ToInt32(numPesoConexion.Value);

            // Actualizo el grafico
            viewer.Graph = graph;
            splitContainer1.Panel2.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(viewer);
            splitContainer1.Panel2.ResumeLayout();
        }
    }
}

