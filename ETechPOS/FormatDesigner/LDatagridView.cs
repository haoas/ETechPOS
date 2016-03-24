using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETech.FormatDesigner
{
    public static class LDatagridView
    {
        public static void FormatColumnsWidth(this DataGridView thisDataGridView)
        {
            thisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            thisDataGridView.Columns[thisDataGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (DataGridViewColumn dataGridViewColumn in thisDataGridView.Columns)
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public static void FillColumn(this DataGridView DGV, List<string> ColumnNames)
        {
            foreach(string CN in ColumnNames)
                DGV.Columns[CN].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public static void Standardize(this DataGridView DGV)
        {
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.ReadOnly = true;
            DGV.RowHeadersVisible = false;
            DGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.MultiSelect = false;

            //To double check
            DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DGV.ScrollBars = ScrollBars.Vertical;

            //Columns
            DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
             foreach (DataGridViewColumn DGVC in DGV.Columns)
                DGVC.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public static void HideColumn(this DataGridView DGV, List<string> columnnames)
        {
            foreach (string columnname in columnnames)
            {
                DGV.Columns[columnname].Visible = false;
                DGV.Columns[columnname].MinimumWidth = 2;
                DGV.Columns[columnname].Width = 2;
            }
        }

        public static void SelectPreviousRow(this DataGridView DGV)
        {
            if (DGV.Rows.Count <= 0)
                return;

            int row_index = DGV.CurrentCell.RowIndex;
            int row_index_next = row_index - 1;

            int visiblecolumnindex = 0;
            foreach (DataGridViewColumn DGVC in DGV.Columns)
            {
                visiblecolumnindex++;
                if (DGVC.Visible == true)
                    break;
            }

            if (row_index_next <= -1)
            {
                DGV.Rows[row_index].Selected = true;
                DGV.CurrentCell = DGV[visiblecolumnindex, row_index];
            }
            else
            {
                DGV.Rows[row_index_next].Selected = true;
                DGV.CurrentCell = DGV[visiblecolumnindex, row_index_next];
            }
        }

        public static void SelectNextRow(this DataGridView DGV)
        {
            if (DGV.Rows.Count <= 0)
                return;

            int row_index = DGV.CurrentCell.RowIndex;
            int row_index_next = row_index + 1;

            int visiblecolumnindex = 0;
            foreach (DataGridViewColumn DGVC in DGV.Columns)
            {
                visiblecolumnindex++;
                if (DGVC.Visible == true)
                    break;
            }

            if (row_index_next >= DGV.RowCount)
            {
                DGV.Rows[row_index].Selected = true;
                DGV.CurrentCell = DGV[visiblecolumnindex, row_index];
            }
            else
            {
                DGV.Rows[row_index_next].Selected = true;
                DGV.CurrentCell = DGV[visiblecolumnindex, row_index_next];
            }
        }

    }
}
