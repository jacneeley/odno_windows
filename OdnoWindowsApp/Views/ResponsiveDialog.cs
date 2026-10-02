using OdnoWindowsApp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Views
{
    public class ResponsiveDialog : ResponsiveForm
    {

        public static readonly Rectangle _bounds = Screen.PrimaryScreen.Bounds;

        private int[] _pos = new int[2];

        public ResponsiveDialog(bool controls = true, string size = "MEDIUM") {
            if (controls)
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;
                this.ShowInTaskbar = false;
                this.TopMost = true;
            }
            else {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;
                this.ControlBox = false;
                this.ShowInTaskbar = false;
                this.TopMost = true;
            }

            if (size.Equals("TINY"))
            {
                this.Size = new Size(150, 80);
                return;
            }

            Size maxSize;
            Size minSize = new Size(Convert.ToInt32(_bounds.Width * .20), Convert.ToInt32(_bounds.Height * .30));

            if (size.Equals("X-SMALL"))
            {
                maxSize = minSize;
            }

            else if (size.Equals("SMALL"))
            {
                maxSize = new Size(Convert.ToInt32(_bounds.Width * .25), Convert.ToInt32(_bounds.Height * .33));
            }

            else if (size.Equals("MEDIUM"))
            {
                maxSize = new Size(Convert.ToInt32(_bounds.Width * .50), Convert.ToInt32(_bounds.Height * .66));
            }

            else if (size.Equals("LARGE"))
            {
                maxSize = new Size(Convert.ToInt32(_bounds.Width * .75), Convert.ToInt32(_bounds.Height * .82));
            }

            else
            {
                maxSize = new Size(Convert.ToInt32(_bounds.Width * .85), Convert.ToInt32(_bounds.Height * .9));
            }

            this.MaximumSize = maxSize;
            this.MinimumSize = minSize;
            this.Size = maxSize;

        }

        //public ResponsiveDialog(string size = "MEDIUM")
        //{
        //    this.FormBorderStyle = FormBorderStyle.FixedDialog;
        //    this.StartPosition = FormStartPosition.CenterScreen;
        //    this.AutoScroll = true;
        //    this.ShowInTaskbar = false;
        //    this.TopMost = true;
        //    Size maxSize;
        //    Size minSize = new Size(Convert.ToInt32(_bounds.Width * .20), Convert.ToInt32(_bounds.Height * .30));

        //    if (size.Equals("SMALL"))
        //    {
        //        maxSize = new Size(Convert.ToInt32(_bounds.Width * .25), Convert.ToInt32(_bounds.Height * .33));
        //    }

        //    else if (size.Equals("MEDIUM"))
        //    {
        //        maxSize = new Size(Convert.ToInt32(_bounds.Width * .50), Convert.ToInt32(_bounds.Height * .66));
        //    }

        //    else if (size.Equals("LARGE"))
        //    {
        //        maxSize = new Size(Convert.ToInt32(_bounds.Width * .75), Convert.ToInt32(_bounds.Height * .82));
        //    }

        //    else {
        //        maxSize = new Size(Convert.ToInt32(_bounds.Width * .85), Convert.ToInt32(_bounds.Height * .9));
        //    }

        //    this.MaximumSize = maxSize;
        //    this.MinimumSize = minSize;
        //    this.Size = maxSize;

        //}

        public Label ResponsiveLabel(string text, int[] pos) {
            if (pos == null || pos.Length == 0) {
                pos = new int[2] { 50, 50 }; //assumed default. must be changed.
            }
            return new Label() {
                Top = pos[0],
                Left = pos[1],
                Text = text,
                Width = (int)(Width * .33 + text.Count() + 5),
            };
        }

        public TextBox ResponsiveTextBox(int[] pos, string text = "") {
            if (pos == null || pos.Length == 0)
            {
                pos = new int[2] { 50, 50 }; //assumed default. must be changed.
            }
            return new TextBox() { 
                Text= text,
                Top = pos[0],
                Left = pos[1],
                Width = (int)(Width * .33 + text.Count() + 5)
            };
        }

        public ComboBox ResponsiveComboBox(int[] pos, string text, IList src) {
            return new ComboBox()
            {
                Top = pos[0],
                Left = pos[1],
                Text = text,
                DataSource = src,
                Width = (int)(Width * .33 + text.Count() + 5),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        public Button ResponsiveButton(int[] pos, string text) {
            return new Button() {
                Top = pos[0],
                Left = pos[1],
                Width = (int)(Width * .15 + text.Count() + 5),
                Text = text,
            };
        }

        public DataGridView ResponsiveDataGridView(int[] pos) {
            int padding = 50;
            var dgv = new DataGridView() {
                Top= pos[0],
                Left = pos[1],
                
                Width = (int)(Width * .66),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            };

            return dgv;
        }

        public Label CenterLabel(string text, int dialogSize = 0) {
            if (dialogSize == 0) {
                dialogSize = this.Width;
            }
            return new Label() {
                Dock = DockStyle.Fill,
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = (int)(dialogSize * 0.2 + (text.Count())),
            };
        }

        public ProgressBar MarqueeProgressBar(DockStyle dock) { 
            return new ProgressBar()
            {
                Dock = dock,
                Height = (int)(this.Size.Height * .25),
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 1
            };
        }
    }
}
