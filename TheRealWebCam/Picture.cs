namespace TheRealWebCam
{
    public class Picture
    {
        DataAccessLayer da = new DataAccessLayer();

        public byte[] Image { get;  set; }
        public string Name { get;  set; }

        public bool SavePicture()
        {
            return da.InsertPicture(this);
        }

    }
}