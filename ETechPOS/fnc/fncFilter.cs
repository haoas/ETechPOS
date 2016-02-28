using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using System.Drawing;

namespace ETech.fnc
{
    class fncFilter
    {
        public static int getIntegerValue(string str)
        {
            decimal ret;
            int ret_int;
            str = str.Replace(",", "");
            bool isNum = decimal.TryParse(str, out ret);

            if (isNum)
            {
                return ret_int = (int)Math.Round(ret);
            }
            else
            {
                return 0;
            }
        }

        public static decimal getDecimalValue(string str)
        {
            decimal ret;
            str = str.Replace(",", "");
            bool isNum = decimal.TryParse(str, out ret);
            if (isNum)
            {
                return ret;
            }
            else
            {
                return 0;
            }
        }

        public static int get_permission_login() { return 101; }
        public static int get_permission_void() { return 102; }
        public static int get_permission_delete() { return 103; }
        public static int get_permission_return() { return 104; }
        public static int get_permission_opendrawer() { return 105; }
        public static int get_permission_collectioncash() { return 106; }
        public static int get_permission_reprint() { return 107; }
        public static int get_permission_wholesale() { return 108; }
        public static int get_permission_discount() { return 109; }
        public static int get_permission_nonvat() { return 110; }
        public static int get_permission_senior() { return 111; }
        public static int get_permission_debt() { return 112; }
        public static int get_permission_retail() { return 113; }
        public static int get_permission_forcereturn() { return 116; }

        public static bool check_permission_login(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(101)) return true;
            return false;
        }
        public static bool check_permission_void(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(102)) return true;
            return false;
        }
        public static bool check_permission_delete(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(103)) return true;
            return false;
        }
        public static bool check_permission_return(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(104)) return true;
            return false;
        }
        public static bool check_permission_opendrawer(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(105)) return true;
            return false;
        }
        public static bool check_permission_collectioncash(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(106)) return true;
            return false;
        }
        public static bool check_permission_reprint(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(107)) return true;
            return false;
        }
        public static bool check_permission_wholesale(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(108)) return true;
            return false;
        }
        public static bool check_permission_discount(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(109)) return true;
            return false;
        }
        public static bool check_permission_nonvat(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(110)) return true;
            return false;
        }
        public static bool check_permission_senior(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(111)) return true;
            return false;
        }
        public static bool check_permission_debt(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(112)) return true;
            return false;
        }
        public static bool check_permission_retail(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(113)) return true;
            return false;
        }
        public static bool check_permission_forcereturn(List<int> permissions)
        {
            if (permissions.Contains(100)) return true;
            if (permissions.Contains(116)) return true;
            return false;
        }

        public static void alert(string str)
        {
            frmError Errorform = new frmError();
            Errorform.errormessage = str;
            Errorform.ShowDialog();
        }

        public static void gridview_selectrow(DataGridView dgv, int row_index)
        {
            dgv.Rows[row_index].Selected = true;
            foreach(DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                {
                    dgv.CurrentCell = dgv[col.Index, row_index];
                    break;
                }
            }
        }
        public static void gridview_selectnextrow(DataGridView dgv)
        {
            if (dgv.Rows.Count <= 0)
                return;

            int row_index = dgv.CurrentCell.RowIndex;
            int row_index_next = row_index + 1;

            if (row_index_next >= dgv.RowCount)
                fncFilter.gridview_selectrow(dgv, row_index);
            else
                fncFilter.gridview_selectrow(dgv, row_index_next);
        }
        public static void gridview_selectpreviousrow(DataGridView dgv)
        {
            if (dgv.Rows.Count <= 0)
                return;

            int row_index = dgv.CurrentCell.RowIndex;
            int row_index_next = row_index - 1;

            if (row_index_next <= -1)
                fncFilter.gridview_selectrow(dgv, row_index);
            else
                fncFilter.gridview_selectrow(dgv, row_index_next);
        }

        public static string string_align_center(string str, int totalwidth)
        {
            int spaceleft = totalwidth - str.Length;
            int leftindent = spaceleft / 2;
            int totallength = leftindent + str.Length;
            return String.Format("{0, " + totallength + "}", str);
        }

        public static void set_theme_color(Form form)
        {
            try
            {
                form.AutoScaleMode = AutoScaleMode.None;
                form.StartPosition = FormStartPosition.CenterScreen;
                //form.FormBorderStyle = FormBorderStyle.None;
                form.ControlBox = false;

                if (cls_globalvariables.colortheme_v == "-1")
                    form.BackgroundImage = Properties.Resources.diagmonds_paleblue;
                else if (cls_globalvariables.colortheme_v == "-2")
                    form.BackgroundImage = Properties.Resources.diagmonds_forestgreen;
                else if (cls_globalvariables.colortheme_v == "-3")
                    form.BackgroundImage = Properties.Resources.diagmonds_gold;
                else if (cls_globalvariables.colortheme_v == "-4")
                    form.BackgroundImage = Properties.Resources.diagmonds_goodblue;
                else if (cls_globalvariables.colortheme_v == "-5")
                    form.BackgroundImage = Properties.Resources.diagmonds_gray;
                else if (cls_globalvariables.colortheme_v == "-6")
                    form.BackgroundImage = Properties.Resources.diagmonds_red;
                else if (cls_globalvariables.colortheme_v == "-7")
                    form.BackgroundImage = Properties.Resources.diagmonds_blue;
                else if (cls_globalvariables.colortheme_v == "0")
                    form.BackColor = Color.FromArgb(255, 192, 128);
                else if (cls_globalvariables.colortheme_v == "1")
                    form.BackColor = Color.DarkSeaGreen;
                else if (cls_globalvariables.colortheme_v == "2")
                    form.BackColor = Color.MediumTurquoise;
                else if (cls_globalvariables.colortheme_v == "3")
                    form.BackColor = Color.Silver;
                else if (cls_globalvariables.colortheme_v == "4")
                    form.BackColor = Color.LightSteelBlue;
                else if (cls_globalvariables.colortheme_v == "5")
                    form.BackColor = Color.Thistle;
                else if (cls_globalvariables.colortheme_v == "6")
                    form.BackColor = Color.LightPink;

                foreach (Control control in form.Controls) //get controls from Form
                    set_all_controls(control); //set control container or not
            }
            catch
            {
            }
        }

        public static void set_all_controls(Control container) //All Controls
        {
            set_theme_controls(container);
            if (container is TextBox)
            {
                TextBox textbox;
                textbox = (TextBox)container;
                cls_globalfunc.set_txtbox_controls(textbox);
            }

            foreach (Control ctrl in container.Controls)
                set_all_controls(ctrl);
        }

        public static void txtbx_length_qty(TextBox txtbx_qty)
        {
            txtbx_qty.MaxLength = 3;
        }
        public static void txtbx_length_cash(TextBox txtbx_cash)
        {
            txtbx_cash.MaxLength = 13;
        }

        public static void set_dgv_inherit(DataGridView dgv) {

            //DGV Properties
            int screen_width = Screen.PrimaryScreen.WorkingArea.Width;
            float custom_FontSize = 12F;
            if (cls_globalvariables.AutoShowKeyboard_v)
                custom_FontSize = 14F;

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", custom_FontSize, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Calibri", custom_FontSize, FontStyle.Regular);

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = true;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[i].Resizable = DataGridViewTriState.False;
            }
            if (screen_width <= 1024)
                set_dgv_controls(dgv);
        }
        public static void set_dgv_controls(DataGridView dgv)
        {
            //DGV Properties
            //int screen_width = Screen.PrimaryScreen.WorkingArea.Width;
            float custom_FontSize = 12F;
            if (cls_globalvariables.AutoShowKeyboard_v)
                custom_FontSize = 14F;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", custom_FontSize, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Calibri", custom_FontSize, FontStyle.Regular);

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = true;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[i].Resizable = DataGridViewTriState.False;
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            set_dgv_display(dgv);
        }
        public static void set_dgv_display(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            List<int> col_index = new List<int>();
            decimal col_width = 0;
            decimal res_width = 0;
            int col_each = 0;
            int new_colwidth = 0;
            int dgv_width = 0;
            int counter_col = 0;
            dgv_width = dgv.Width;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible == true)
                {
                    col_index.Add(i);
                    counter_col++;
                    col_width += dgv.Columns[i].GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true);
                }
            }
            if (dgv.ColumnCount != 0)
            {
                res_width = dgv_width - col_width - 2;
                col_each = Convert.ToInt32(res_width) / counter_col;
                for (int i = 0; i < col_index.Count; i++)
                {
                    int width = Convert.ToInt32(dgv.Columns[col_index[i]].GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true) + col_each);
                    dgv.Columns[col_index[i]].MinimumWidth = (width < 2) ? 2 : width;
                    dgv.Columns[col_index[i]].Width = width;
                    new_colwidth += width;
                }

                if (dgv_width > new_colwidth)
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
        }

        public static void set_theme_controls(Control control)
        {
            GroupBox gb;
            CheckBox cb;
            TextBox tb;
            Panel pnl;
            Label lbl;
            Button btn;
            TabControl tbctrl;

            if (cls_globalvariables.colortheme_v == "-1" ||
                cls_globalvariables.colortheme_v == "-2" ||
                cls_globalvariables.colortheme_v == "-3" ||
                cls_globalvariables.colortheme_v == "-4" ||
                cls_globalvariables.colortheme_v == "-5" ||
                cls_globalvariables.colortheme_v == "-6" ||
                cls_globalvariables.colortheme_v == "-7")
            {
                if (control is GroupBox)
                {
                    gb = (GroupBox)control;
                    gb.BackColor = Color.Transparent;
                    gb.ForeColor = Color.Orange;
                    gb.Font = new Font("Calibri", gb.Font.Size, FontStyle.Bold);
                }
                else if (control is CheckBox)
                {
                    cb = (CheckBox)control;
                    cb.BackColor = Color.Transparent;
                    cb.ForeColor = Color.White;
                    cb.Font = new Font("Calibri", cb.Font.Size, FontStyle.Regular);
                }
                else if (control is TextBox)
                {
                    tb = (TextBox)control;
                    tb.ForeColor = Color.Black;
                    tb.Font = new Font("Calibri", tb.Font.Size, FontStyle.Regular);
                }
                else if (control is Panel)
                {
                    pnl = (Panel)control;
                    pnl.BackColor = Color.Transparent;
                    pnl.ForeColor = Color.White;
                }
                else if (control is Label)
                {
                    lbl = (Label)control;
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Calibri", lbl.Font.Size, FontStyle.Regular);

                    if (lbl.Tag != null && lbl.Tag.ToString() == "lightGreen")
                        lbl.ForeColor = Color.FromArgb(192, 255, 192);
                    else if (lbl.Tag != null && lbl.Tag.ToString() == "lightBlue")
                        lbl.ForeColor = Color.FromArgb(128, 255, 255);
                    else if (lbl.Tag != null && lbl.Tag.ToString() == "lightRed")
                        lbl.ForeColor = Color.FromArgb(255, 128, 128);
                    else if (lbl.Tag != null && lbl.Tag.ToString() == "lightGold")
                        lbl.ForeColor = Color.FromArgb(255, 255, 128);
                }

            }

            if (control is TabControl)
            {
                tbctrl = (TabControl)control;
                foreach (TabPage tp in tbctrl.TabPages)
                {
                    if (cls_globalvariables.colortheme_v == "-1")
                        tp.BackgroundImage = Properties.Resources.diagmonds_paleblue;
                    else if (cls_globalvariables.colortheme_v == "-2")
                        tp.BackgroundImage = Properties.Resources.diagmonds_forestgreen;
                    else if (cls_globalvariables.colortheme_v == "-3")
                        tp.BackgroundImage = Properties.Resources.diagmonds_gold;
                    else if (cls_globalvariables.colortheme_v == "-4")
                        tp.BackgroundImage = Properties.Resources.diagmonds_goodblue;
                    else if (cls_globalvariables.colortheme_v == "-5")
                        tp.BackgroundImage = Properties.Resources.diagmonds_gray;
                    else if (cls_globalvariables.colortheme_v == "-6")
                        tp.BackgroundImage = Properties.Resources.diagmonds_red;
                    else if (cls_globalvariables.colortheme_v == "-7")
                        tp.BackgroundImage = Properties.Resources.diagmonds_blue;
                }
            }

            if (control is Button)
            {
                btn = (Button)control;
                btn.ForeColor = Color.White;

                if (cls_globalvariables.colortheme_v == "-1" ||
                    cls_globalvariables.colortheme_v == "-2" ||
                    cls_globalvariables.colortheme_v == "-3" ||
                    cls_globalvariables.colortheme_v == "-4" ||
                    cls_globalvariables.colortheme_v == "-5" ||
                    cls_globalvariables.colortheme_v == "-6" ||
                    cls_globalvariables.colortheme_v == "-7")
                {
                    btn.Font = new Font("Calibri", btn.Font.Size, FontStyle.Regular);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Black;
                }

                if (cls_globalvariables.colortheme_v == "-1")
                    btn.BackColor = Color.SteelBlue;
                else if (cls_globalvariables.colortheme_v == "-2")
                    btn.BackColor = Color.Green;
                else if (cls_globalvariables.colortheme_v == "-3")
                    btn.BackColor = Color.DarkGoldenrod;
                else if (cls_globalvariables.colortheme_v == "-4")
                    btn.BackColor = Color.DarkSlateBlue;
                else if (cls_globalvariables.colortheme_v == "-5")
                    btn.BackColor = Color.Gray;
                else if (cls_globalvariables.colortheme_v == "-6")
                    btn.BackColor = Color.Firebrick;
                else if (cls_globalvariables.colortheme_v == "-7")
                    btn.BackColor = Color.Teal;
                else if (cls_globalvariables.colortheme_v == "0")
                    btn.BackColor = Color.FromArgb(0, 0, 64);
                else if (cls_globalvariables.colortheme_v == "1")
                    btn.BackColor = Color.FromArgb(0, 0, 64);
                else if (cls_globalvariables.colortheme_v == "2")
                    btn.BackColor = Color.FromArgb(0, 0, 64);
                else if (cls_globalvariables.colortheme_v == "3")
                    btn.BackColor = Color.Black;
                else if (cls_globalvariables.colortheme_v == "4")
                    btn.BackColor = Color.FromArgb(0, 0, 64);
                else if (cls_globalvariables.colortheme_v == "5")
                    btn.BackColor = Color.FromArgb(0, 0, 64);
                else if (cls_globalvariables.colortheme_v == "6")
                    btn.BackColor = Color.FromArgb(0, 0, 64);
            }
        }

        /* not used
        public static void set_theme_color(List<Button> btnlist)
        {
            foreach (Button btn in btnlist)
                set_theme_color(btn);
        }
        public static void set_theme_color(List<Label> lbllist)
        {
            foreach (Label lbl in lbllist)
                set_theme_color(lbl);
        }
        public static void set_theme_color(List<Panel> pnllist)
        {
            foreach (Panel pnl in pnllist)
                set_theme_color(pnl);
        }
        public static void set_theme_color(Form frm)
        {
            if (cls_globalvariables.colortheme_v == "0")
                frm.BackColor = Color.FromArgb(255, 192, 128);
            else if (cls_globalvariables.colortheme_v == "1")
                frm.BackColor = Color.DarkSeaGreen;
            else if (cls_globalvariables.colortheme_v == "2")
                frm.BackColor = Color.MediumTurquoise;
            else if (cls_globalvariables.colortheme_v == "3")
                frm.BackColor = Color.Silver;
            else if (cls_globalvariables.colortheme_v == "4")
                frm.BackColor = Color.LightSteelBlue;
            else if (cls_globalvariables.colortheme_v == "5")
                frm.BackColor = Color.Thistle;
            else if (cls_globalvariables.colortheme_v == "6")
                frm.BackColor = Color.LightPink;
        }
        public static void set_theme_color(Button btn)
        {
            if (cls_globalvariables.colortheme_v == "0")
            {
                btn.BackColor = Color.FromArgb(0, 0, 64);
                btn.ForeColor = Color.White;
            }
            else if (cls_globalvariables.colortheme_v == "1")
            {
                btn.BackColor = Color.FromArgb(0, 0, 64);
                btn.ForeColor = Color.White;
            }
            else if (cls_globalvariables.colortheme_v == "2")
            {
                btn.BackColor = Color.FromArgb(0, 0, 64);
                btn.ForeColor = Color.White;
            }
            else if (cls_globalvariables.colortheme_v == "3")
            {
                btn.BackColor = Color.Black;
                btn.ForeColor = Color.White;
            }
            else if (cls_globalvariables.colortheme_v == "4")
            {
                btn.BackColor = Color.FromArgb(0, 0, 64);
                btn.ForeColor = Color.White;
            }
            else if (cls_globalvariables.colortheme_v == "5")
            {
                btn.BackColor = Color.FromArgb(0, 0, 64);
                btn.ForeColor = Color.White;
            }
            else if (cls_globalvariables.colortheme_v == "6")
            {
                btn.BackColor = Color.FromArgb(0, 0, 64);
                btn.ForeColor = Color.White;
            }
        }
        public static void set_theme_color(Label lbl)
        {
            if (cls_globalvariables.colortheme_v == "0")
                lbl.BackColor = Color.FromArgb(255, 192, 128);
            else if (cls_globalvariables.colortheme_v == "1")
                lbl.BackColor = Color.DarkSeaGreen;
            else if (cls_globalvariables.colortheme_v == "2")
                lbl.BackColor = Color.MediumTurquoise;
            else if (cls_globalvariables.colortheme_v == "3")
                lbl.BackColor = Color.Silver;
            else if (cls_globalvariables.colortheme_v == "4")
                lbl.BackColor = Color.LightSteelBlue;
            else if (cls_globalvariables.colortheme_v == "5")
                lbl.BackColor = Color.Thistle;
            else if (cls_globalvariables.colortheme_v == "6")
                lbl.BackColor = Color.LightPink;
        }
        public static void set_theme_color(Panel pnl)
        {
            if (cls_globalvariables.colortheme_v == "0")
                pnl.BackColor = Color.FromArgb(255, 192, 128);
            else if (cls_globalvariables.colortheme_v == "1")
                pnl.BackColor = Color.DarkSeaGreen;
            else if (cls_globalvariables.colortheme_v == "2")
                pnl.BackColor = Color.MediumTurquoise;
            else if (cls_globalvariables.colortheme_v == "3")
                pnl.BackColor = Color.Silver;
            else if (cls_globalvariables.colortheme_v == "4")
                pnl.BackColor = Color.LightSteelBlue;
            else if (cls_globalvariables.colortheme_v == "5")
                pnl.BackColor = Color.Thistle;
            else if (cls_globalvariables.colortheme_v == "6")
                pnl.BackColor = Color.LightPink;
        }
         * */
    }
}