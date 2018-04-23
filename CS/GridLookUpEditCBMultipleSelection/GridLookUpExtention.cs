using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.ComponentModel;
using DevExpress.XtraEditors.Popup;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace GridLookUpEditCBMultipleSelection
{
    [UserRepositoryItem("RegisterMyRepositoryItemGridLookUpEdit")]
    public class MyRepositoryItemGridLookUpEdit : RepositoryItemGridLookUpEdit
    {
        //The static constructor which calls the registration method
        static MyRepositoryItemGridLookUpEdit() { RegisterMyRepositoryItemGridLookUpEdit(); }


        internal GridCheckMarksSelection gridSelection_;
        public GridCheckMarksSelection GridSelection
        {
            get { return gridSelection_; }
            set 
            {
                if (gridSelection_ != null)
                {
                    gridSelection_.SelectionChanged -= new GridCheckMarksSelection.SelectionChangedEventHandler(gridSelection__SelectionChanged);   
                }                
                gridSelection_ = value;
                gridSelection_.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridSelection__SelectionChanged);
            }
             
        }

        void gridSelection__SelectionChanged(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            PropertyDescriptorCollection collection = ListBindingHelper.GetListItemProperties(GridSelection.Selection);
            PropertyDescriptor desc = collection[DisplayMember];
            foreach (object rv in GridSelection.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); }
                sb.Append(desc.GetValue(rv).ToString());
            }
            if (OwnerEdit != null)
            {
                OwnerEdit.Text = sb.ToString();
            }            
        }

        //Initialize new properties
        public MyRepositoryItemGridLookUpEdit() 
        {
            GridSelection = new GridCheckMarksSelection();
        }

        //The unique name for the custom editor
        public const string CustomEditName = "MyGridLookUpEdit";

        //Return the unique name
        public override string EditorTypeName { get { return CustomEditName; } }
        
        //Register the editor
        public static void RegisterMyRepositoryItemGridLookUpEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(
                CustomEditName,
                typeof(MyGridLookUpEdit),
                typeof(MyRepositoryItemGridLookUpEdit),
                typeof(GridLookUpEditBaseViewInfo),
                new ButtonEditPainter(), true, null));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                MyRepositoryItemGridLookUpEdit source = item as MyRepositoryItemGridLookUpEdit;
                if (source == null) return;
                GridSelection = source.GridSelection;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    public class MyGridLookUpEdit : GridLookUpEdit
    {
        //The static constructor which calls the registration method
        static MyGridLookUpEdit() { MyRepositoryItemGridLookUpEdit.RegisterMyRepositoryItemGridLookUpEdit(); }

        //Initialize the new instance
        public MyGridLookUpEdit()
        {
            //...
            CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(MyGridLookUpEdit_CustomDisplayText);
        }

        void MyGridLookUpEdit_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            PropertyDescriptorCollection collection = ListBindingHelper.GetListItemProperties(Properties.GridSelection.Selection);
            PropertyDescriptor desc = collection[Properties.DisplayMember];
            foreach (object rv in Properties.GridSelection.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); }
                sb.Append(desc.GetValue(rv).ToString());
            }
            e.DisplayText = sb.ToString();            
        }

        //Return the unique name
        public override string EditorTypeName 
        { 
            get 
            { 
                return MyRepositoryItemGridLookUpEdit.CustomEditName; 
            } 
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new MyRepositoryItemGridLookUpEdit Properties
        {
            get { return base.Properties as MyRepositoryItemGridLookUpEdit; }
        }

        protected override DevExpress.XtraEditors.Popup.PopupBaseForm CreatePopupForm()
        {
            return new MyPopupGridLookUpEditForm(this);
        }

        protected override void OnPopupShown()
        {
            base.OnPopupShown();
        }
    }

    public class MyPopupGridLookUpEditForm : PopupGridLookUpEditForm
    {
        public MyPopupGridLookUpEditForm(GridLookUpEdit edit) : base(edit) { }

        ArrayList tempSelection = new ArrayList();

        protected override void SetupButtons()
        {
            this.fShowOkButton = true;
            this.fCloseButtonStyle = BlobCloseButtonStyle.Caption;
        }

        protected override void OnCloseButtonClick()
        {
            base.OnCloseButtonClick();
            RedefineSelection(tempSelection, (OwnerEdit as MyGridLookUpEdit).Properties.GridSelection.Selection);
        }

        protected override void OnOkButtonClick()
        {
            base.OnOkButtonClick();
            (OwnerEdit as MyGridLookUpEdit).Properties.GridSelection.OnSelectionChanged();
        }

        protected override void OnBeforeShowPopup()
        {
            base.OnBeforeShowPopup();
            RedefineSelection((OwnerEdit as MyGridLookUpEdit).Properties.GridSelection.Selection, tempSelection);
        }

        internal void RedefineSelection(ArrayList listSource, ArrayList listDestination)
        {
            listDestination.Clear();
            foreach (var item in listSource)
                listDestination.Add(item);
        }

        protected override PopupBaseFormViewInfo CreateViewInfo()
        {
            return new MyCustomBlobPopupFormViewInfo(this);
        }
    }

    public class MyCustomBlobPopupFormViewInfo : CustomBlobPopupFormViewInfo
    {
        public MyCustomBlobPopupFormViewInfo(PopupBaseForm form) : base(form) { }

        // recalculate buttons sizes if needed
        protected override void CalcContentRect(System.Drawing.Rectangle bounds)
        {
            base.CalcContentRect(bounds);
            // recalculate buttons bounds if needed
            this.fOkButtonRect = new System.Drawing.Rectangle(this.fOkButtonRect.X, this.fOkButtonRect.Y + 1, this.fOkButtonRect.Width, this.fOkButtonRect.Height - 2);
            this.fCloseButtonRect = new System.Drawing.Rectangle(this.fCloseButtonRect.X, this.fCloseButtonRect.Y + 1, this.fCloseButtonRect.Width, this.fCloseButtonRect.Height - 2);

            // recalculate footer bounds if needed
            this.SizeBarRect = new System.Drawing.Rectangle(this.SizeBarRect.X, this.SizeBarRect.Y - 20, this.SizeBarRect.Width, this.SizeBarRect.Height + 20);
            this.fContentRect = new System.Drawing.Rectangle(this.fContentRect.X, this.fContentRect.Y, this.fContentRect.Width, this.fContentRect.Height - 20);
            this.fOkButtonRect = new System.Drawing.Rectangle(this.fOkButtonRect.X, this.fOkButtonRect.Y - 10, this.fOkButtonRect.Width, this.fOkButtonRect.Height);
            this.fCloseButtonRect = new System.Drawing.Rectangle(this.fCloseButtonRect.X, this.fCloseButtonRect.Y - 10, this.fCloseButtonRect.Width, this.fCloseButtonRect.Height);
        }
    }
}
