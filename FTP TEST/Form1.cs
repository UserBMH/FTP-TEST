using System.Net;

namespace FTP_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = Get_Data_From_My_FTP_Server();
        }

        private string Get_Data_From_My_FTP_Server()
        {
            //result data from file
            string result = string.Empty;

            //do FTPwebrequest
            //requires active web server
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("yourwebsite" + "sample.txt");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //setup credentials
            request.Credentials = new NetworkCredential("yourlogin", "yourpassword");

            //initialize ftp response
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //open readers
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            //data from file
            result = reader.ReadToEnd();
            
            //set to save file locally
            using (StreamWriter file = File.CreateText("states.txt"))
            {
                file.Write(result);
                file.Close();
            }

            //closing
            reader.Close();
            response.Close();

            return result;



        }



    }
}