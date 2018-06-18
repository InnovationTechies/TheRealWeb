using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

using System.Web.Services;




namespace TheRealWebCam
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.InputStream.Length > 0)
                {
                    using (StreamReader reader = new StreamReader(Request.InputStream))
                    {
                        string hexString = Server.UrlEncode(reader.ReadToEnd());
                        string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                        string imagePath = string.Format("~/Captures/{0}.png", imageName);
                        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                        Session["CapturedImage"] = ResolveUrl(imagePath);
                    }
                }
            }

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {

            //string str_Photo = Request.Form["image_Data"]; //Get the image from flash file
            //byte[] photo = Convert.FromBase64String(str_Photo);
            //FileStream f_s = new FileStream("C:\\capture.jpg", FileMode.OpenOrCreate, FileAccess.Write);
            //BinaryWriter b_r = new BinaryWriter(f_s);
            //b_r.Write(photo);
            //b_r.Flush();
            //b_r.Close();
            //f_s.Close();

            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            //if (ShowDialog(this) == DialogResult.OK)
            {
                VideoCaptureDevice videoSource = new VideoCaptureDevice();
                videoSource = form.VideoDevice;
                videoSource.NewFrame += new NewFrameEventHandler(abcVideo_NewFrame);// Thiet lap xu li newFrame
                videoSource.Start(); // start video

            }
        }
        void abcVideo_NewFrame(object sender, NewFrameEventArgs evenrArgs)
        {

            System.Web.UI.WebControls.Image video = (System.Web.UI.WebControls.Image)evenrArgs.Frame.Clone();
            // pictureBoxWeb.Image = video;
            // imgWeb.SizeMode = PictureBoxSizeMode.StretchImage;

            System.Web.UI.WebControls.Image img = video;
            //img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            imgWeb = img;


            //pictureBoxWeb.Size.Height = this.Height;
            //pictureBoxWeb.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //pictureBoxWeb.Left += -1;
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {

        }

        protected void btnTakePhoto_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (img.Image != null)
            //    {
            //        //Save First
            //        Picture pic = new Picture();

            //        Bitmap varBmp = new Bitmap(pictureBoxWeb.Image);
            //        Bitmap newBitmap = new Bitmap(varBmp);
            //        // varBmp.Save(@"C:\Users\s2132\source\repos\WebCamStreaming\WebCamStreaming\Pictures\" + DateTime.Now.ToString() + ".png", ImageFormat.Png);
            //        byte[] data;
            //        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            //        {
            //            varBmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //            data = stream.ToArray();
            //        }
            //        pic.Image = data;
            //        pic.Name = "MyPicture" + DateTime.Now.ToString("yyyymmdd") + '_' + DateTime.Now.ToString("hhmmss");

            //        if (pic.SavePicture())
            //        {
            //            MessageBox.Show("Saved");
            //        }


            //        //Now Dispose to free the memory
            //        varBmp.Dispose();
            //        varBmp = null;

            //        MessageBox.Show("Picture is saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    { MessageBox.Show("null exception"); }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            string url = HttpContext.Current.Session["CapturedImage"].ToString();
            HttpContext.Current.Session["CapturedImage"] = null;
            return url;
        }

    }
}