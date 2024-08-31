namespace win_helper.Services
{
    internal class CaptureService : BaseService
    {
        public override void Active()
        {
            ScreenshotForm screenshotForm = new();
            screenshotForm.Show();
        }

        public override void Inactive()
        {

        }
    }

    public class ScreenshotForm : Form
    {
        private bool _isSelecting;
        private Point _startPoint;
        private Rectangle _selectionRect;

        public ScreenshotForm()
        {
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Red;
            this.TransparencyKey = Color.Red;
            this.Cursor = Cursors.Cross;
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isSelecting = true;
            _startPoint = e.Location;
            _selectionRect = new Rectangle(e.X, e.Y, 0, 0);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isSelecting)
            {
                _selectionRect = new Rectangle(
                    Math.Min(_startPoint.X, e.X),
                    Math.Min(_startPoint.Y, e.Y),
                    Math.Abs(_startPoint.X - e.X),
                    Math.Abs(_startPoint.Y - e.Y));
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_isSelecting)
            {
                _isSelecting = false;

                Invalidate();
                Application.DoEvents();

                CaptureScreen();
                this.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_isSelecting && _selectionRect != Rectangle.Empty)
            {
                e.Graphics.DrawRectangle(Pens.Yellow, _selectionRect);
            }
        }

        private void CaptureScreen()
        {
            if (_selectionRect != Rectangle.Empty)
            {
                using (Bitmap bitmap = new(_selectionRect.Width, _selectionRect.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(_selectionRect.Location, Point.Empty, _selectionRect.Size);
                    }
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                    string fileName = $"screenshot_{timestamp}.png";
                    string savePath = Path.Combine(desktopPath, fileName);
                    bitmap.Save(savePath);
                }
            }
        }
    }

}
