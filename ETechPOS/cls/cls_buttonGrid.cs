using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_buttonGrid
    {
        public string table;
        public string columnshown;
        public string condition;
        public int buttonwidth;
        public int buttonheight;
        public int currentpage;
        public int columnCntPerPage;
        public int rowCntPerPage;
        public int initLeftSpacing;
        public int initTopSpacing;
        public int LeftSpacing;
        public int RightSpacing;
        public int totaldata;
        public DataTable DT;

        public cls_buttonGrid()
        {
            this.table = "Department";
            this.columnshown = "SyncId";
            this.condition = "";
            this.buttonwidth = 93;
            this.buttonheight = 75;
            this.currentpage = 1;
            this.columnCntPerPage = 2;
            this.rowCntPerPage = 5;
            this.initLeftSpacing = 10;
            this.initTopSpacing = 25;
            this.LeftSpacing = 5;
            this.RightSpacing = 5;
            this.DT = null;
            this.totaldata = 0;
        }

        public string getTable()
        {
            return this.table;
        }
        public void setTable(string Table_d)
        {
            this.table = Table_d;
        }
        public string getColumnShown()
        {
            return this.columnshown;
        }
        public void setColumnShown(string columnshown_d)
        {
            this.columnshown = columnshown_d;
        }
        public void setCondition(string condition_d)
        {
            this.condition = condition_d;
        }
        public int getColumnCntPerPage()
        {
            return this.columnCntPerPage;
        }
        public void setColumnCntPerPage(int columnCntPerPage_d)
        {
            this.columnCntPerPage = columnCntPerPage_d;
        }
        public int getRowCntPerPage()
        {
            return this.rowCntPerPage;
        }
        public void setRowCntPerPage(int rowCntPerPage_d)
        {
            this.rowCntPerPage = rowCntPerPage_d;
        }
        public int getDataPerPage()
        {
            return this.rowCntPerPage * this.columnCntPerPage;
        }
        public int getCurrentPage()
        {
            return this.currentpage;
        }
        public void GoToPreivousPage()
        {
            int previouspage = (this.currentpage - 1) < 1 ? 1 : (this.currentpage - 1);
            setCurrentPage(previouspage);
        }
        public void GoToNextPage()
        {
            int nextpage = (this.DT.Rows.Count == this.columnCntPerPage * this.rowCntPerPage) ? this.currentpage + 1 : this.currentpage;
            setCurrentPage(nextpage);
        }
        public void GoToPage(int page)
        {
            setCurrentPage(page);
        }
        public void setCurrentPage(int currentpage_d)
        {
            this.currentpage = currentpage_d;

            int startindex = (this.currentpage - 1) * getDataPerPage();
            string sql = "SELECT * FROM " + this.table + " " + this.condition + @" ORDER BY " + this.columnshown + @" LIMIT " + startindex + @"," + getDataPerPage();
            this.DT = mySQLFunc.getdb(sql);

            this.setTotalData();
        }

        public void setTotalData()
        {
            string sql = "SELECT COUNT(*) as cnt FROM " + this.table + " " + this.condition;
            this.totaldata = Convert.ToInt32(mySQLFunc.getdb(sql).Rows[0]["cnt"]);
        }

        public int getTotalData()
        {
            return this.totaldata;
        }

        public int getTotalPages()
        {
            double ans = (double)this.totaldata / (double)(this.columnCntPerPage * this.rowCntPerPage);
            return (int)(Math.Ceiling(ans));
        }
    }
}
