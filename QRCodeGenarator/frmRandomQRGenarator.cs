using System;
using System.Windows.Forms;
using ZXing;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Collections;

namespace QRCodeGenarator
{
    public partial class frmRandomQRGenarator : Form
    {

        ArrayList randomArr = new ArrayList();

        public frmRandomQRGenarator()
        {
            InitializeComponent();
        }

        private void btnGenarate_Click(object sender, EventArgs e)
        {

            string path = txtPath.Text;

            if (path.Length > 0)
            {

                for (int i = randomArr.Count; i < 100; i++)
                {
                    string randomString = Guid.NewGuid().ToString("n").Substring(0, 10);

                    if (!randomArr.Contains(randomString))
                    {
                        randomArr.Add(randomString);
                        list.Items.Add(randomString);
                    }
                }

                for (int a = 0; a < randomArr.Count; a++)
                {
                    int count = a + 1;
                    pgrBar.Value = count;
                    lblCompleted.Text = count.ToString();

                    genarateQRCode(randomArr[a].ToString(), path + "\\" + randomArr[a].ToString()+ ".png");
                }

                MessageBox.Show(this, "Task Completed!" , "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtPath.Text = "";
                pgrBar.Value = 0;
                lblCompleted.Text = "";
            }
            else
            {
                MessageBox.Show(this, "Please Select Folder Path", "Path Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void genarateQRCode(string value, string filename)
        {

            
            BarcodeWriter barcodeWriter = new BarcodeWriter();

            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            barcodeWriter.Options.Height = 256;
            barcodeWriter.Options.Width = 256;

            barcodeWriter.Write(value).Save(filename);

            
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();

            dialog.InitialDirectory = "C:\\";

            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtPath.Text = dialog.FileName;
            }
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
