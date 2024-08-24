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

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            this.Controls.Add(panel);
            this.ResumeLayout(false);
        }

        private Button BuildStayAwakeButton()
        {
            MyButton stayAwakeButton = new MyButton(new StayAwakeService())
            {
                Width = 100,
                Height = 50,
                Text = "禁用睡眠",
                BackColor = Color.White,
            };
            stayAwakeButton.Click += new EventHandler((sender, e) =>
            {
                if (stayAwakeButton.BackColor == Color.White)
                {
                    stayAwakeButton.Service.Active();
                    stayAwakeButton.BackColor = Color.GreenYellow;
                }
                else if (stayAwakeButton.BackColor == Color.GreenYellow)
                {
                    stayAwakeButton.Service.Inactive();
                    stayAwakeButton.BackColor = Color.White;
                }
            });
            return stayAwakeButton;
        }

    }
}
