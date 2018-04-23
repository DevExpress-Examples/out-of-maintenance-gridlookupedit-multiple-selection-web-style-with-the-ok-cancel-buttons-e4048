Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraGrid.Columns

Namespace GridLookUpEditCBMultipleSelection
	Public Class GridCheckMarksSelection
		Protected _view As GridView
        Protected _selection As ArrayList
        Private column As GridColumn
        Private edit As RepositoryItemCheckEdit
        Private Const CheckboxIndent As Integer = 4

        Public Sub New(ByVal view As GridView)
            Me.New()
            Me.View = view
        End Sub

        Public Property View() As GridView
            Get
                Return _view
            End Get
            Set(ByVal value As GridView)
                If _view IsNot value Then
                    Detach()
                    Attach(value)
                End If
            End Set
        End Property
        Public ReadOnly Property CheckMarkColumn() As GridColumn
            Get
                Return column
            End Get
        End Property

        Public Sub New()
            _selection = New ArrayList()
            Me.OnSelectionChanged()
        End Sub

        Public Property Selection() As ArrayList
            Get
                Return _selection
            End Get
            Set(ByVal value As ArrayList)
                _selection = value
            End Set
        End Property

        Public ReadOnly Property SelectedCount() As Integer
            Get
                Return _selection.Count
            End Get
        End Property
        Public Function GetSelectedRow(ByVal index As Integer) As Object
            Return _selection(index)
        End Function

        Public Function GetSelectedIndex(ByVal row As Object) As Integer
            Return _selection.IndexOf(row)
        End Function

        Public Sub ClearSelection()
            _selection.Clear()
            Invalidate()
        End Sub
        Public Sub SelectAll()
            SelectAll(Nothing)
        End Sub

        Public Sub SelectAll(ByVal dataSource As Object)
            _selection.Clear()
            If dataSource IsNot Nothing Then
                If TypeOf dataSource Is ICollection Then
                    _selection.AddRange((DirectCast(dataSource, ICollection)))
                End If
                Me.OnSelectionChanged()
            Else
                For i As Integer = 0 To _view.DataRowCount - 1
                    _selection.Add(_view.GetRow(i))
                Next i
            End If
            Invalidate()
        End Sub
        Public Delegate Sub SelectionChangedEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        Public Event SelectionChanged As SelectionChangedEventHandler
        Public Sub OnSelectionChanged()
            If SelectionChangedEvent IsNot Nothing Then
                Dim e As New EventArgs()
                RaiseEvent SelectionChanged(Me, e)
            End If
        End Sub
        Public Sub SelectGroup(ByVal rowHandle As Integer, ByVal [select] As Boolean)
            If IsGroupRowSelected(rowHandle) AndAlso [select] Then
                Return
            End If
            Dim i As Integer = 0
            Do While i < _view.GetChildRowCount(rowHandle)
                Dim childRowHandle As Integer = _view.GetChildRowHandle(rowHandle, i)
                If _view.IsGroupRow(childRowHandle) Then
                    SelectGroup(childRowHandle, [select])
                Else
                    SelectRow(childRowHandle, [select], False)
                End If
                i += 1
            Loop
            Invalidate()
        End Sub
        Public Sub SelectRow(ByVal rowHandle As Integer, ByVal [select] As Boolean)
            SelectRow(rowHandle, [select], True)
        End Sub

        Public Sub InvertRowSelection(ByVal rowHandle As Integer)
            If View.IsDataRow(rowHandle) Then
                SelectRow(rowHandle, Not IsRowSelected(rowHandle))
            End If
            If View.IsGroupRow(rowHandle) Then
                SelectGroup(rowHandle, Not IsGroupRowSelected(rowHandle))
            End If
        End Sub
        Public Function IsGroupRowSelected(ByVal rowHandle As Integer) As Boolean
            Dim i As Integer = 0
            Do While i < _view.GetChildRowCount(rowHandle)
                Dim row As Integer = _view.GetChildRowHandle(rowHandle, i)
                If _view.IsGroupRow(row) Then
                    If Not IsGroupRowSelected(row) Then
                        Return False
                    End If
                Else
                    If Not IsRowSelected(row) Then
                        Return False
                    End If
                End If
                i += 1
            Loop
            Return True
        End Function
        Public Function IsRowSelected(ByVal rowHandle As Integer) As Boolean
            If _view.IsGroupRow(rowHandle) Then
                Return IsGroupRowSelected(rowHandle)
            End If

            Dim row As Object = _view.GetRow(rowHandle)
            Return GetSelectedIndex(row) <> -1
        End Function

        Protected Overridable Sub Attach(ByVal view As GridView)
            If view Is Nothing Then
                Return
            End If
            _selection.Clear()
            Me._view = view
            view.BeginUpdate()
            Try
                edit = TryCast(view.GridControl.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)

                column = view.Columns.Add()
                column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                column.Visible = True
                column.VisibleIndex = 0
                column.FieldName = "CheckMarkSelection"
                column.Caption = "Mark"
                column.OptionsColumn.ShowCaption = False
                column.OptionsColumn.AllowEdit = False
                column.OptionsColumn.AllowSize = False
                column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean
                column.Width = GetCheckBoxWidth()
                column.ColumnEdit = edit

                AddHandler view.Click, AddressOf View_Click
                AddHandler view.CustomDrawColumnHeader, AddressOf View_CustomDrawColumnHeader
                AddHandler view.CustomDrawGroupRow, AddressOf View_CustomDrawGroupRow
                AddHandler view.CustomUnboundColumnData, AddressOf view_CustomUnboundColumnData
                AddHandler view.KeyDown, AddressOf view_KeyDown
            Finally
                view.EndUpdate()
            End Try
        End Sub
        Protected Overridable Sub Detach()
            If _view Is Nothing Then
                Return
            End If
            If column IsNot Nothing Then
                column.Dispose()
            End If
            If edit IsNot Nothing Then
                _view.GridControl.RepositoryItems.Remove(edit)
                edit.Dispose()
            End If
            RemoveHandler _view.Click, AddressOf View_Click
            RemoveHandler _view.CustomDrawColumnHeader, AddressOf View_CustomDrawColumnHeader
            RemoveHandler _view.CustomDrawGroupRow, AddressOf View_CustomDrawGroupRow
            RemoveHandler _view.CustomUnboundColumnData, AddressOf view_CustomUnboundColumnData
            RemoveHandler _view.KeyDown, AddressOf view_KeyDown
            _view = Nothing
        End Sub
        Protected Function GetCheckBoxWidth() As Integer
            Dim info As DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo = TryCast(edit.CreateViewInfo(), DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)
            Dim width As Integer = 0
            GraphicsInfo.Default.AddGraphics(Nothing)
            Try
                width = info.CalcBestFit(GraphicsInfo.Default.Graphics).Width
            Finally
                GraphicsInfo.Default.ReleaseGraphics()
            End Try
            Return width + CheckboxIndent * 2
        End Function
        Protected Sub DrawCheckBox(ByVal g As Graphics, ByVal r As Rectangle, ByVal Checked As Boolean)
            Dim info As DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo
            Dim painter As DevExpress.XtraEditors.Drawing.CheckEditPainter
            Dim args As DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs
            info = TryCast(edit.CreateViewInfo(), DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)
            painter = TryCast(edit.CreatePainter(), DevExpress.XtraEditors.Drawing.CheckEditPainter)
            info.EditValue = Checked
            info.Bounds = r
            info.CalcViewInfo(g)
            args = New DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, New DevExpress.Utils.Drawing.GraphicsCache(g), r)
            painter.Draw(args)
            args.Cache.Dispose()
        End Sub
        Private Sub Invalidate()
            _view.BeginUpdate()
            _view.EndUpdate()
        End Sub
        Private Sub SelectRow(ByVal rowHandle As Integer, ByVal [select] As Boolean, ByVal invalidate As Boolean)
            If IsRowSelected(rowHandle) = [select] Then
                Return
            End If
            Dim row As Object = _view.GetRow(rowHandle)
            If [select] Then
                _selection.Add(row)
            Else
                _selection.Remove(row)
            End If
			If invalidate Then
				Me.Invalidate()
			End If
			'OnSelectionChanged();
		End Sub
		Private Sub view_CustomUnboundColumnData(ByVal sender As Object, ByVal e As CustomColumnDataEventArgs)
			If e.Column Is CheckMarkColumn Then
				If e.IsGetData Then
					e.Value = IsRowSelected(View.GetRowHandle(e.ListSourceRowIndex))
				Else
					SelectRow(View.GetRowHandle(e.ListSourceRowIndex), DirectCast(e.Value, Boolean))
				End If
			End If
		End Sub
		Private Sub view_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
			If View.FocusedColumn IsNot column OrElse e.KeyCode <> Keys.Space Then
				Return
			End If
			InvertRowSelection(View.FocusedRowHandle)
		End Sub
		Private Sub View_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim info As GridHitInfo
			Dim pt As Point = _view.GridControl.PointToClient(Control.MousePosition)
			info = _view.CalcHitInfo(pt)
			If info.Column Is column Then
				If info.InColumn Then
					If SelectedCount = _view.DataRowCount Then
						ClearSelection()
					Else
						SelectAll()
					End If
				End If
				If info.InRowCell Then
					InvertRowSelection(info.RowHandle)
				End If
			End If
			If info.InRow AndAlso _view.IsGroupRow(info.RowHandle) AndAlso info.HitTest <> GridHitTest.RowGroupButton Then
				InvertRowSelection(info.RowHandle)
			End If
		End Sub
		Private Sub View_CustomDrawColumnHeader(ByVal sender As Object, ByVal e As ColumnHeaderCustomDrawEventArgs)
			If e.Column Is column Then
				e.Info.InnerElements.Clear()
				e.Painter.DrawObject(e.Info)
				DrawCheckBox(e.Cache.Graphics, e.Bounds, SelectedCount = _view.DataRowCount)
				e.Handled = True
			End If
		End Sub
		Private Sub View_CustomDrawGroupRow(ByVal sender As Object, ByVal e As RowObjectCustomDrawEventArgs)
			Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo
			info = TryCast(e.Info, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo)

			info.GroupText = "         " & info.GroupText.TrimStart()
			e.Cache.FillRectangle(e.Appearance.GetBackBrush(e.Cache), e.Bounds)
			'e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
			e.Painter.DrawObject(e.Info)

			Dim r As Rectangle = info.ButtonBounds
			r.Offset(r.Width + CheckboxIndent * 2 - 1, 0)
			DrawCheckBox(e.Cache.Graphics, r, IsGroupRowSelected(e.RowHandle))
			e.Handled = True
		End Sub
	End Class
End Namespace
