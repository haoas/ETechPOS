using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using System.Drawing;

namespace ETech.fnc
{
    class fncFullScreen
    {
        Form form;
        public int prevwidth;
        public int prevheight;
        decimal width_multiplier;
        decimal height_multiplier;
        public float factor;
        bool resizeonce = false;

        public fncFullScreen(Form form)
        {
            this.form = form;
            prevwidth = form.Size.Width;
            prevheight = form.Size.Height;
        }

        public bool ResizeScreen(bool gofullscreen)
        {
            if (gofullscreen)
            {
                FullScreen(true, true);
                return false;
            }
            else
            {
                MaximizeScreen(true);
                return true;
            }
        }
        public void ResizeFormsControls()
        {
            if (!cls_globalvariables.is4By3ratio_v)
                return;
            width_multiplier = Screen.PrimaryScreen.Bounds.Width / Convert.ToDecimal(cls_globalvariables.origwidth_v);
            height_multiplier = Screen.PrimaryScreen.Bounds.Height / Convert.ToDecimal(cls_globalvariables.origheight_v);

            ResizeForm(form);
            foreach (Control ctrl in form.Controls)
                GetAllControls(ctrl);
        }

        public void FullScreen(bool withtaskbar, bool firstscreen)
        {
            if (resizeonce)
            {
                prevwidth = form.Size.Width;
                prevheight = form.Size.Height;
            }
            else
                resizeonce = true;

            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (withtaskbar)
            {
                if (firstscreen)
                    form.Bounds = Screen.AllScreens.First().WorkingArea;
                else
                    form.Bounds = Screen.AllScreens.Last().WorkingArea;
                form.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            }
            else
            {
                if (firstscreen)
                    form.Bounds = Screen.AllScreens.First().WorkingArea;
                else
                    form.Bounds = Screen.AllScreens.Last().WorkingArea;
                form.MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            }

            width_multiplier = form.Size.Width / (decimal)prevwidth;
            height_multiplier = form.Size.Height / (decimal)prevheight;

            foreach (Control ctrl in form.Controls)
                GetAllControls(ctrl);
        }
        public void MaximizeScreen(bool withtaskbar)
        {
            if (resizeonce)
            {
                prevwidth = form.Size.Width;
                prevheight = form.Size.Height;
            }
            else
                resizeonce = true;

            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            if (withtaskbar)
            {
                form.Bounds = Screen.PrimaryScreen.WorkingArea;
                form.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            }
            else
            {
                form.Bounds = Screen.PrimaryScreen.Bounds;
                form.MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            }

            width_multiplier = form.Size.Width / Convert.ToDecimal(prevwidth);
            height_multiplier = form.Size.Height / Convert.ToDecimal(prevheight);

            foreach (Control ctrl in form.Controls)
                GetAllControls(ctrl);
        }
        public void GetAllControls(Control container)
        {
            FitControlFont(container);
            ResizeControl(container);
            foreach (Control ctrl in container.Controls)
                GetAllControls(ctrl);
        }
        public void ResizeControl(Control control)
        {
            int controlX = Convert.ToInt32(width_multiplier * control.Location.X);
            int controlY = Convert.ToInt32(height_multiplier * control.Location.Y);

            control.Location = new Point(controlX, controlY);

            control.Width = Convert.ToInt32(control.Width * width_multiplier);
            control.Height = Convert.ToInt32(control.Height * height_multiplier);
        }
        public void ResizeForm(Form form)
        {
            form.Width = Convert.ToInt32(form.Width * width_multiplier);
            form.Height = Convert.ToInt32(form.Height * height_multiplier);
            form.StartPosition = FormStartPosition.CenterParent;
        }
        public void ResizeForm(Form form, decimal width_multiplier, decimal height_multiplier)
        {
            if (form.Name != "frmMenu")
                form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2,
                              (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);
            else
                form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, 0);
        }
        public void FitControlFont(Control control)
        {
            try
            {
                Font currentFont = control.Font;
                Graphics graphics = control.CreateGraphics();
                SizeF newSize = graphics.MeasureString(control.Text, control.Font);
                graphics.Dispose();
                factor = height_multiplier < width_multiplier ? (float)height_multiplier: (float)width_multiplier;
                if (factor > 1)
                    factor = factor + 0.11f;
                else if (factor < 1)
                    factor = factor - 0.11f;
                if (control is TabControl || control is TabPage)
                    return;
                if (control is DataGridView)
                {
                    DataGridView dgv = (DataGridView)control;
                    if (width_multiplier != 1)
                        for (int i = 0; i < dgv.ColumnCount; i++)
                            dgv.Columns[i].Width = Convert.ToInt32(dgv.Columns[i].Width * factor);
                    return;
                }
                if (control.InvokeRequired)
                    control.Invoke(new MethodInvoker(delegate { control.Font = new Font(currentFont.Name, currentFont.SizeInPoints * (float)factor, currentFont.Style); }));
                else
                    control.Font = new Font(currentFont.Name, (currentFont.SizeInPoints) * (float)factor, currentFont.Style);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
