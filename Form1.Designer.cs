using win_helper.Services;
using win_helper.Views;

namespace win_helper
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;

            panel.Controls.Add(BuildStayAwakeButton());
            panel.Controls.Add(BuildTopButton());
            panel.Controls.Add(BuildCaptureButton());

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Win工具";
            this.Controls.Add(panel);
            this.ResumeLayout(false);
        }

        private Button BuildStayAwakeButton()
        {
            MyButton button = new MyButton(new StayAwakeService())
            {
                Width = 100,
                Height = 50,
                Text = "禁用睡眠",
                BackColor = Color.White,
            };
            button.Click += new EventHandler((sender, e) =>
            {
                if (button.BackColor == Color.White)
                {
                    button.Service.Active();
                    button.BackColor = Color.GreenYellow;
                }
                else if (button.BackColor == Color.GreenYellow)
                {
                    button.Service.Inactive();
                    button.BackColor = Color.White;
                }
            });
            return button;
        }

        private Button BuildTopButton()
        {
            MyButton button = new MyButton(new TopService())
            {
                Width = 100,
                Height = 50,
                Text = "置顶助手",
                BackColor = Color.White,
            };
            button.Click += new EventHandler((sender, e) =>
            {
                if (button.BackColor == Color.White)
                {
                    button.Service.Active();
                    button.BackColor = Color.GreenYellow;
                }
                else if (button.BackColor == Color.GreenYellow)
                {
                    button.Service.Inactive();
                    button.BackColor = Color.White;
                }
            });
            return button;
        }

        private Button BuildCaptureButton()
        {
            MyButton button = new MyButton(new CaptureService())
            {
                Width = 100,
                Height = 50,
                Text = "快速截图",
                BackColor = Color.White,
            };
            button.Click += new EventHandler((sender, e) =>
            {
                this.WindowState = FormWindowState.Minimized;
                button.Service.Active();
            });
            return button;
        }

    }
}
