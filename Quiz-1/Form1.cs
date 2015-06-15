using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Xml;

namespace Quiz_1
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = null;
        OleDbDataAdapter adapter = null;
        DataSet dataSet = null;
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

            dataSet = new DataSet();

            
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int a, b, c, d = 0;

            a = random.Next(3);
            radioButtonAntwort1.Text = reader.getValue(a + 2).toString();
            do { b = random.Next(3); } while (b == a);
            radioButtonAntwort2.Text = reader.getValue(b + 2).toString();
            do { c = random.Next(3); } while (c == a || c == b);
            radioButtonAntwort3.Text = reader.getValue(c + 2).toString();
            do { c = random.Next(3); } while (d == a || d == b || d == c);
            radioButtonAntwort4.Text = reader.getValue(d + 2).toString();
            
        }

        private void runProgressBar()
        {
          
        }

        private void ausDatenbankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection = new OleDbConnection();
            connection.ConnectionString = Properties.Settings.Default.DbVerbindung;
            adapter = new OleDbDataAdapter("Select * FROM tFrage", connection);
            adapter.Fill(dataSet);
            adapter.FillSchema(dataSet, SchemaType.Source, "Fragen");
            adapter.Fill(dataSet, "Fragen");
            dataSet.WriteXmlSchema("Fragen.xsd");
            dataSet.WriteXml("Fragen.xml");
            
        }

        private void ausXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSet.ReadXmlSchema("Fragen.xsd");
            dataSet.ReadXml("Fragen.xml");
        }

        private void quizStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTableReader reader = dataSet.Tables["Fragen"].CreateDataReader();
        }

    }
}
