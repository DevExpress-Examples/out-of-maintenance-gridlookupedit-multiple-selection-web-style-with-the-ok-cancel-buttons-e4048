Namespace GridLookUpEditCBMultipleSelection
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim gridCheckMarksSelection1 As New GridLookUpEditCBMultipleSelection.GridCheckMarksSelection()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(Form1))
			Dim gridCheckMarksSelection2 As New GridLookUpEditCBMultipleSelection.GridCheckMarksSelection()
			Me.myGridLookUpEdit1 = New GridLookUpEditCBMultipleSelection.MyGridLookUpEdit()
			Me.myGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.myGridLookUpEdit2 = New GridLookUpEditCBMultipleSelection.MyGridLookUpEdit()
			Me.myGridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.myGridLookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.myGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.myGridLookUpEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.myGridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' myGridLookUpEdit1
			' 
			Me.myGridLookUpEdit1.Location = New System.Drawing.Point(11, 11)
			Me.myGridLookUpEdit1.Name = "myGridLookUpEdit1"
			Me.myGridLookUpEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			gridCheckMarksSelection1.Selection = (CType(resources.GetObject("gridCheckMarksSelection1.Selection"), System.Collections.ArrayList))
			gridCheckMarksSelection1.View = Nothing
			Me.myGridLookUpEdit1.Properties.GridSelection = gridCheckMarksSelection1
			Me.myGridLookUpEdit1.Properties.View = Me.myGridLookUpEdit1View
			Me.myGridLookUpEdit1.Size = New System.Drawing.Size(328, 20)
			Me.myGridLookUpEdit1.TabIndex = 0
			' 
			' myGridLookUpEdit1View
			' 
			Me.myGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
			Me.myGridLookUpEdit1View.Name = "myGridLookUpEdit1View"
			Me.myGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
			Me.myGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
			' 
			' myGridLookUpEdit2
			' 
			Me.myGridLookUpEdit2.Location = New System.Drawing.Point(17, 69)
			Me.myGridLookUpEdit2.Name = "myGridLookUpEdit2"
			Me.myGridLookUpEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			gridCheckMarksSelection2.Selection = (CType(resources.GetObject("gridCheckMarksSelection2.Selection"), System.Collections.ArrayList))
			gridCheckMarksSelection2.View = Nothing
			Me.myGridLookUpEdit2.Properties.GridSelection = gridCheckMarksSelection2
			Me.myGridLookUpEdit2.Properties.View = Me.myGridLookUpEdit2View
			Me.myGridLookUpEdit2.Size = New System.Drawing.Size(321, 20)
			Me.myGridLookUpEdit2.TabIndex = 1
			' 
			' myGridLookUpEdit2View
			' 
			Me.myGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
			Me.myGridLookUpEdit2View.Name = "myGridLookUpEdit2View"
			Me.myGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
			Me.myGridLookUpEdit2View.OptionsView.ShowGroupPanel = False
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(349, 113)
			Me.Controls.Add(Me.myGridLookUpEdit2)
			Me.Controls.Add(Me.myGridLookUpEdit1)
			Me.Name = "Form1"
			Me.Text = "Form1"
            CType(Me.myGridLookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.myGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.myGridLookUpEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.myGridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private myGridLookUpEdit1 As MyGridLookUpEdit
		Private myGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
		Private myGridLookUpEdit2 As MyGridLookUpEdit
		Private myGridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView

	End Class
End Namespace

