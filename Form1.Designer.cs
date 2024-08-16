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
            Button stayAwakeButton = new Button
            {
                Width = 100,
                Height = 50,
                Text = "睡眠",
                BackColor = Color.Gray,
            };
            stayAwakeButton.Click += new EventHandler((sender, e) =>
            {
                if (stayAwakeButton.BackColor == Color.Gray)
                {
                    StayAwakeService.Active();
                    stayAwakeButton.BackColor = Color.Green;
                }
                else if (stayAwakeButton.BackColor == Color.Green)
                {
                    StayAwakeService.Inactive();
                    stayAwakeButton.BackColor = Color.Gray;
                }
            });
            return stayAwakeButton;
        }

    }
}
