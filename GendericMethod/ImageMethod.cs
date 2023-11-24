namespace Clone_Main_Project_0710.GendericMethod
{
    public class ImageMethod
    {
        /// <summary>
        /// Chuyển ảnh thành chuỗi, lưu vào database
        /// </summary>
        /// <param name="fileImage">IFormFile</param>
        /// <returns>string</returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 19/10/2023
        /// Update: 19/10/2023
        public static string ConvertImageToString(IFormFile fileImage)
        {
            byte[] bytes = null;
            using (Stream fs = fileImage.OpenReadStream())
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
            }
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}