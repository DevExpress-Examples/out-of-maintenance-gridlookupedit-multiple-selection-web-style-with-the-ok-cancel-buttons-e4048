using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;

namespace GridLookUpEditCBMultipleSelection
{
    public partial class Form1 : Form
    {
        public Form1()
        { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = FillDataTable();
            myGridLookUpEdit1.Properties.View.OptionsBehavior.AutoPopulateColumns = false;
            myGridLookUpEdit1.Properties.DataSource = dt;
            myGridLookUpEdit1.Properties.DisplayMember = "Fruit";
            myGridLookUpEdit1.Properties.View.OptionsSelection.MultiSelect = true;

            myGridLookUpEdit1.Properties.GridSelection = new GridCheckMarksSelection(myGridLookUpEdit1.Properties.View);
            myGridLookUpEdit1.Properties.GridSelection.SelectAll(dt.DefaultView);

            GridColumn col = myGridLookUpEdit1.Properties.View.Columns.AddField("Fruit");
            col.Visible = true;
            col.Caption = "Fruit";

            BindingList<Customer> bl = new BindingList<Customer>();
            for (int i = 1; i < 6; i++)
			{
			    bl.Add(new Customer(i, "Name " + i.ToString()));
			}

            myGridLookUpEdit2.Properties.View.OptionsBehavior.AutoPopulateColumns = false;
            myGridLookUpEdit2.Properties.DataSource = bl;
            myGridLookUpEdit2.Properties.DisplayMember = "Name";
            myGridLookUpEdit2.Properties.View.OptionsSelection.MultiSelect = true;

            myGridLookUpEdit2.Properties.GridSelection = new GridCheckMarksSelection(myGridLookUpEdit2.Properties.View);
            myGridLookUpEdit2.Properties.GridSelection.SelectAll(bl);

            GridColumn colName = myGridLookUpEdit2.Properties.View.Columns.AddField("Name");
            colName.Visible = true;
            colName.Caption = "Name";

        }

        DataTable FillDataTable()
        {
            DataTable _dataTable = new DataTable();
            DataColumn col;
            DataRow row;

            col = new DataColumn();
            col.ColumnName = "Bool";
            col.DataType = System.Type.GetType("System.Boolean");
            _dataTable.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Fruit";
            col.DataType = System.Type.GetType("System.String");
            _dataTable.Columns.Add(col);

            row = _dataTable.NewRow();
            row["Fruit"] = "Peach";
            _dataTable.Rows.Add(row);
            row = _dataTable.NewRow();
            row["Fruit"] = "Apple";
            _dataTable.Rows.Add(row);
            row = _dataTable.NewRow();
            row["Fruit"] = "Banana";
            _dataTable.Rows.Add(row);

            return _dataTable;
        }
    }

    public class Customer
    {
        int id_;
        public int ID
        {
            get { return id_; }
            set { id_ = value; }
        }

        string name_;
        public string Name
        {
            get { return name_; }
            set { name_ = value; }
        }

        public Customer(int pID, string pName) 
        {
            ID = pID;
            Name = pName;
        }
    }
}
