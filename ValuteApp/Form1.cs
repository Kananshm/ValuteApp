using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ValuteApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_mezenne_Click(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
           string url = "https://www.cbar.az/currencies/12.12.2019.xml";
            Stream fs = http.GetAsync(url).GetAwaiter().GetResult().Content.ReadAsStreamAsync().GetAwaiter().GetResult();
           

        }

        private void Btn_Value_Click(object sender, EventArgs e)
        {
            ValCurs valCurs = new ValCurs();
            HttpClient http = new HttpClient();
            string url = $"https://www.cbar.az/currencies/{dateTimePicker1.Value.ToString(dateTimePicker1.CustomFormat)}.xml";

            Stream fs = http.GetAsync(url).GetAwaiter().GetResult().Content.ReadAsStreamAsync().GetAwaiter().GetResult();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ValCurs));

            using (fs)
            {
                valCurs = (ValCurs)xmlSerializer.Deserialize(fs);

            
            }

            dataGridView1.DataSource = valCurs.ValType.SelectMany(x => x.Valute).ToList();
        }
    }
}
