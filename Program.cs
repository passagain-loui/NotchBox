using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotchBox
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NotchForm());
        }
    }

    public class NotchForm : Form
    {
        private readonly Label lblStatus;

        public NotchForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.FromArgb(15, 23, 42);

            int w = 420;
            int h = 45;
            int x = (Screen.PrimaryScreen?.WorkingArea.Width ?? 1920 - w) / 2;
            this.SetBounds(x, 0, w, h);

            lblStatus = new Label
            {
                Text = "Drop files here (NotchBox v0.8)",
                ForeColor = Color.FromArgb(209, 213, 219),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblStatus);

            this.AllowDrop = true;
            this.DragEnter += (s, e) => {
                if (e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                    lblStatus.Text = "Release to Drop";
                    lblStatus.ForeColor = Color.FromArgb(0, 229, 255);
                }
            };
            this.DragLeave += (s, e) => {
                lblStatus.Text = "Drop files here (NotchBox v0.8)";
                lblStatus.ForeColor = Color.FromArgb(209, 213, 219);
            };
            this.DragDrop += (s, e) => {
                if (e != null && e.Data != null)
                {
                    if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
                    {
                        lblStatus.Text = $"Stored {files.Length} item(s) successfully";
                        lblStatus.ForeColor = Color.FromArgb(52, 211, 153);
                    }
                }
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Exit NotchBox", null, (s, e) => Application.Exit());
            this.ContextMenuStrip = contextMenu;
        }
    }
}
