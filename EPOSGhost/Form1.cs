using Sunny.UI;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EPOSGhost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            uiComboBox1.SelectedIndex = 0;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            (string Role, string PNbase, string PNCompared) = (uiComboBox1.Text, uiTextBox3.Text.Trim(), uiTextBox4.Text.Trim());
            Task firstTask = new Task(() =>
            {
                
                switch (Role)
                {
                    case "A_sample related":
                        CompareListInterface taskAsample = new ASample_CompareListController();
                        taskAsample.CompareListGenerate(PNbase, PNCompared);
                        break;
                    case "HWPR":
                        CompareListInterface taskHWPR = new HWPR_CompareList();
                        taskHWPR.CompareListGenerate(PNbase, PNCompared);
                        break;
                }
            });
            firstTask.Start();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {

            Task secondTask = new Task(() =>
            {
                (string Bnumber, string SampleStage, string destFilePath) = (uiTextBox6.Text, uiTextBox5.Text, uiTextBox7.Text.Trim());
                CompareListInterface taskEPSWSearch = new EPSW_Autosearch();
                taskEPSWSearch.EPSW_AutoSearch(Bnumber, SampleStage, destFilePath, uiProcessBar1);
            });
            secondTask.Start();


        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    uiTextBox7.Text = path;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
