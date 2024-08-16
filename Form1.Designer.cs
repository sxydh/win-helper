using System.Windows.Forms;
using win_helper.Services;

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
            string active = "不要睡眠";
            string inactive = "正常睡眠";
            Button stayAwakeButton = new Button
            {
                Width = 100,
                Height = 50,
                Text = active,
            };
            stayAwakeButton.Click += new EventHandler((sender, e) =>
            {
                if (stayAwakeButton.Text == active)
                {
                    StayAwakeService.Inactive();
                    stayAwakeButton.Text = inactive;
                }
                else if (stayAwakeButton.Text == inactive)
                {
                    StayAwakeService.Active();
                    stayAwakeButton.Text = active;
                }
            });
            return stayAwakeButton;
        }

    }
}
