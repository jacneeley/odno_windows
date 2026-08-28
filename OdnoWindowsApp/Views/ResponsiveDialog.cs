using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Views
{
    public class ResponsiveDialog : ResponsiveForm
    {
        public ResponsiveDialog() {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
        }

        public ResponsiveDialog(Size maxSize, Size minSize)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.MaximumSize = maxSize;
            this.MinimumSize = minSize;
        }
    }
}
