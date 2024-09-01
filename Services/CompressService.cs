using System.Drawing.Imaging;

namespace win_helper.Services
{
    internal class CompressService : BaseService
    {

        public CompressService()
        {
            this.Quality = 75;
        }

        public int Quality { get; set; }

        public override void Active()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "图片文件 (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = true,
                Title = "请选择文件"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] inputPaths = openFileDialog.FileNames;
                foreach (string inputPath in inputPaths) {
                    string? dir = Path.GetDirectoryName(inputPath);
                    if (dir != null)
                    {
                        string fileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_{DateTime.Now:HHmmss}.jpg";
                        string outputPath = Path.Combine(dir, fileName);
                        CompressHelper.CompressImage(inputPath, outputPath, this.Quality);
                    }
                }
            }
        }

        public override void Inactive()
        {

        }

    }

    internal class CompressHelper
    {

        public static void CompressImage(string inputPath, string outputPath, int quality)
        {
            using (Bitmap bmp = new(inputPath))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                EncoderParameters parameters = new(1);
                parameters.Param[0] = new(Encoder.Quality, quality);

                bmp.Save(outputPath, jpgEncoder, parameters);
            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            throw new InvalidOperationException();
        }

    }

}
