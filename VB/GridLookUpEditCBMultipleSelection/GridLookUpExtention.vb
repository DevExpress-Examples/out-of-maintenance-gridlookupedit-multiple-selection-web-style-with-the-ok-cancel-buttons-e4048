Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Popup
Imports System.IO
Imports System.Collections
Imports System.Data
Imports System.Windows.Forms

Namespace GridLookUpEditCBMultipleSelection
	<UserRepositoryItem("RegisterMyRepositoryItemGridLookUpEdit")> _
	Public Class MyRepositoryItemGridLookUpEdit
		Inherits RepositoryItemGridLookUpEdit
		'The static constructor which calls the registration method
		Shared Sub New()
			RegisterMyRepositoryItemGridLookUpEdit()
		End Sub


		Friend gridSelection_ As GridCheckMarksSelection
		Public Property GridSelection() As GridCheckMarksSelection
			Get
				Return gridSelection_
			End Get
			Set(ByVal value As GridCheckMarksSelection)
				If gridSelection_ IsNot Nothing Then
					RemoveHandler gridSelection_.SelectionChanged, AddressOf gridSelection__SelectionChanged
				End If
				gridSelection_ = value
				AddHandler gridSelection_.SelectionChanged, AddressOf gridSelection__SelectionChanged
			End Set

		End Property

		Private Sub gridSelection__SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)

			Dim sb As New StringBuilder()
			Dim collection As PropertyDescriptorCollection = ListBindingHelper.GetListItemProperties(GridSelection.Selection)
			Dim desc As PropertyDescriptor = collection(DisplayMember)
			For Each rv As Object In GridSelection.Selection
				If sb.ToString().Length > 0 Then
					sb.Append(", ")
				End If
				sb.Append(desc.GetValue(rv).ToString())
			Next rv
			If OwnerEdit IsNot Nothing Then
				OwnerEdit.Text = sb.ToString()
			End If
		End Sub

		'Initialize new properties
		Public Sub New()
			GridSelection = New GridCheckMarksSelection()
		End Sub

		'The unique name for the custom editor
		Public Const CustomEditName As String = "MyGridLookUpEdit"

		'Return the unique name
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return CustomEditName
			End Get
		End Property

		'Register the editor
		Public Shared Sub RegisterMyRepositoryItemGridLookUpEdit()
            EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(CustomEditName, GetType(MyGridLookUpEdit), GetType(MyRepositoryItemGridLookUpEdit), GetType(GridLookUpEditBaseViewInfo), New ButtonEditPainter(), True))
		End Sub

		Public Overrides Sub Assign(ByVal item As RepositoryItem)
			BeginUpdate()
			Try
				MyBase.Assign(item)
				Dim source As MyRepositoryItemGridLookUpEdit = TryCast(item, MyRepositoryItemGridLookUpEdit)
				If source Is Nothing Then
					Return
				End If
				GridSelection = source.GridSelection
			Finally
				EndUpdate()
			End Try
		End Sub
	End Class

	Public Class MyGridLookUpEdit
		Inherits GridLookUpEdit
		'The static constructor which calls the registration method
		Shared Sub New()
			MyRepositoryItemGridLookUpEdit.RegisterMyRepositoryItemGridLookUpEdit()
		End Sub

		'Initialize the new instance
		Public Sub New()
			'...
			AddHandler CustomDisplayText, AddressOf MyGridLookUpEdit_CustomDisplayText
		End Sub

		Private Sub MyGridLookUpEdit_CustomDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs)
			Dim sb As New StringBuilder()
			Dim collection As PropertyDescriptorCollection = ListBindingHelper.GetListItemProperties(Properties.GridSelection.Selection)
			Dim desc As PropertyDescriptor = collection(Properties.DisplayMember)
			For Each rv As Object In Properties.GridSelection.Selection
				If sb.ToString().Length > 0 Then
					sb.Append(", ")
				End If
				sb.Append(desc.GetValue(rv).ToString())
			Next rv
			e.DisplayText = sb.ToString()
		End Sub

		'Return the unique name
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return MyRepositoryItemGridLookUpEdit.CustomEditName
			End Get
		End Property

		'Override the Properties property
		'Simply type-cast the object to the custom repository item type
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As MyRepositoryItemGridLookUpEdit
			Get
				Return TryCast(MyBase.Properties, MyRepositoryItemGridLookUpEdit)
			End Get
		End Property

		Protected Overrides Function CreatePopupForm() As DevExpress.XtraEditors.Popup.PopupBaseForm
			Return New MyPopupGridLookUpEditForm(Me)
		End Function

		Protected Overrides Sub OnPopupShown()
			MyBase.OnPopupShown()
		End Sub
	End Class

	Public Class MyPopupGridLookUpEditForm
		Inherits PopupGridLookUpEditForm
		Public Sub New(ByVal edit As GridLookUpEdit)
			MyBase.New(edit)
		End Sub

		Private tempSelection As New ArrayList()

		Protected Overrides Sub SetupButtons()
			Me.fShowOkButton = True
			Me.fCloseButtonStyle = BlobCloseButtonStyle.Caption
		End Sub

		Protected Overrides Sub OnCloseButtonClick()
			MyBase.OnCloseButtonClick()
			RedefineSelection(tempSelection, (TryCast(OwnerEdit, MyGridLookUpEdit)).Properties.GridSelection.Selection)
		End Sub

		Protected Overrides Sub OnOkButtonClick()
			MyBase.OnOkButtonClick()
			TryCast(OwnerEdit, MyGridLookUpEdit).Properties.GridSelection.OnSelectionChanged()
		End Sub

		Protected Overrides Sub OnBeforeShowPopup()
			MyBase.OnBeforeShowPopup()
			RedefineSelection((TryCast(OwnerEdit, MyGridLookUpEdit)).Properties.GridSelection.Selection, tempSelection)
		End Sub

		Friend Sub RedefineSelection(ByVal listSource As ArrayList, ByVal listDestination As ArrayList)
			listDestination.Clear()
			For Each item In listSource
				listDestination.Add(item)
			Next item
		End Sub

		Protected Overrides Function CreateViewInfo() As PopupBaseFormViewInfo
			Return New MyCustomBlobPopupFormViewInfo(Me)
		End Function
	End Class

	Public Class MyCustomBlobPopupFormViewInfo
		Inherits CustomBlobPopupFormViewInfo
		Public Sub New(ByVal form As PopupBaseForm)
			MyBase.New(form)
		End Sub

		' recalculate buttons sizes if needed
		Protected Overrides Sub CalcContentRect(ByVal bounds As System.Drawing.Rectangle)
			MyBase.CalcContentRect(bounds)
			' recalculate buttons bounds if needed
			Me.fOkButtonRect = New System.Drawing.Rectangle(Me.fOkButtonRect.X, Me.fOkButtonRect.Y + 1, Me.fOkButtonRect.Width, Me.fOkButtonRect.Height - 2)
			Me.fCloseButtonRect = New System.Drawing.Rectangle(Me.fCloseButtonRect.X, Me.fCloseButtonRect.Y + 1, Me.fCloseButtonRect.Width, Me.fCloseButtonRect.Height - 2)

			' recalculate footer bounds if needed
			Me.SizeBarRect = New System.Drawing.Rectangle(Me.SizeBarRect.X, Me.SizeBarRect.Y - 20, Me.SizeBarRect.Width, Me.SizeBarRect.Height + 20)
			Me.fContentRect = New System.Drawing.Rectangle(Me.fContentRect.X, Me.fContentRect.Y, Me.fContentRect.Width, Me.fContentRect.Height - 20)
			Me.fOkButtonRect = New System.Drawing.Rectangle(Me.fOkButtonRect.X, Me.fOkButtonRect.Y - 10, Me.fOkButtonRect.Width, Me.fOkButtonRect.Height)
			Me.fCloseButtonRect = New System.Drawing.Rectangle(Me.fCloseButtonRect.X, Me.fCloseButtonRect.Y - 10, Me.fCloseButtonRect.Width, Me.fCloseButtonRect.Height)
		End Sub
	End Class
End Namespace
