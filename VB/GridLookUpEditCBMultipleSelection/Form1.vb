Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns

Namespace GridLookUpEditCBMultipleSelection
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim dt As DataTable = FillDataTable()
			myGridLookUpEdit1.Properties.View.OptionsBehavior.AutoPopulateColumns = False
			myGridLookUpEdit1.Properties.DataSource = dt
			myGridLookUpEdit1.Properties.DisplayMember = "Fruit"
			myGridLookUpEdit1.Properties.View.OptionsSelection.MultiSelect = True

			myGridLookUpEdit1.Properties.GridSelection = New GridCheckMarksSelection(myGridLookUpEdit1.Properties.View)
			myGridLookUpEdit1.Properties.GridSelection.SelectAll(dt.DefaultView)

			Dim col As GridColumn = myGridLookUpEdit1.Properties.View.Columns.AddField("Fruit")
			col.Visible = True
			col.Caption = "Fruit"

			Dim bl As New BindingList(Of Customer)()
			For i As Integer = 1 To 5
				bl.Add(New Customer(i, "Name " & i.ToString()))
			Next i

			myGridLookUpEdit2.Properties.View.OptionsBehavior.AutoPopulateColumns = False
			myGridLookUpEdit2.Properties.DataSource = bl
			myGridLookUpEdit2.Properties.DisplayMember = "Name"
			myGridLookUpEdit2.Properties.View.OptionsSelection.MultiSelect = True

			myGridLookUpEdit2.Properties.GridSelection = New GridCheckMarksSelection(myGridLookUpEdit2.Properties.View)
			myGridLookUpEdit2.Properties.GridSelection.SelectAll(bl)

			Dim colName As GridColumn = myGridLookUpEdit2.Properties.View.Columns.AddField("Name")
			colName.Visible = True
            colName.Caption = "Name"
		End Sub

		Private Function FillDataTable() As DataTable
			Dim _dataTable As New DataTable()
			Dim col As DataColumn
			Dim row As DataRow

			col = New DataColumn()
			col.ColumnName = "Bool"
			col.DataType = System.Type.GetType("System.Boolean")
			_dataTable.Columns.Add(col)

			col = New DataColumn()
			col.ColumnName = "Fruit"
			col.DataType = System.Type.GetType("System.String")
			_dataTable.Columns.Add(col)

			row = _dataTable.NewRow()
			row("Fruit") = "Peach"
			_dataTable.Rows.Add(row)
			row = _dataTable.NewRow()
			row("Fruit") = "Apple"
			_dataTable.Rows.Add(row)
			row = _dataTable.NewRow()
			row("Fruit") = "Banana"
			_dataTable.Rows.Add(row)

			Return _dataTable
		End Function
	End Class

	Public Class Customer
		Private id_ As Integer
		Public Property ID() As Integer
			Get
				Return id_
			End Get
			Set(ByVal value As Integer)
				id_ = value
			End Set
		End Property

		Private name_ As String
		Public Property Name() As String
			Get
				Return name_
			End Get
			Set(ByVal value As String)
				name_ = value
			End Set
		End Property

		Public Sub New(ByVal pID As Integer, ByVal pName As String)
			ID = pID
			Name = pName
		End Sub
	End Class
End Namespace
