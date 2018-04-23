namespace GridLookUpEditCBMultipleSelection
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GridLookUpEditCBMultipleSelection.GridCheckMarksSelection gridCheckMarksSelection1 = new GridLookUpEditCBMultipleSelection.GridCheckMarksSelection();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            GridLookUpEditCBMultipleSelection.GridCheckMarksSelection gridCheckMarksSelection2 = new GridLookUpEditCBMultipleSelection.GridCheckMarksSelection();
            this.myGridLookUpEdit1 = new GridLookUpEditCBMultipleSelection.MyGridLookUpEdit();
            this.myGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.myGridLookUpEdit2 = new GridLookUpEditCBMultipleSelection.MyGridLookUpEdit();
            this.myGridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit2View)).BeginInit();
            this.SuspendLayout();
            // 
            // myGridLookUpEdit1
            // 
            this.myGridLookUpEdit1.Location = new System.Drawing.Point(11, 11);
            this.myGridLookUpEdit1.Name = "myGridLookUpEdit1";
            this.myGridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            gridCheckMarksSelection1.Selection = ((System.Collections.ArrayList)(resources.GetObject("gridCheckMarksSelection1.Selection")));
            gridCheckMarksSelection1.View = null;
            this.myGridLookUpEdit1.Properties.GridSelection = gridCheckMarksSelection1;
            this.myGridLookUpEdit1.Properties.View = this.myGridLookUpEdit1View;
            this.myGridLookUpEdit1.Size = new System.Drawing.Size(328, 20);
            this.myGridLookUpEdit1.TabIndex = 0;
            // 
            // myGridLookUpEdit1View
            // 
            this.myGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.myGridLookUpEdit1View.Name = "myGridLookUpEdit1View";
            this.myGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.myGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // myGridLookUpEdit2
            // 
            this.myGridLookUpEdit2.Location = new System.Drawing.Point(17, 69);
            this.myGridLookUpEdit2.Name = "myGridLookUpEdit2";
            this.myGridLookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            gridCheckMarksSelection2.Selection = ((System.Collections.ArrayList)(resources.GetObject("gridCheckMarksSelection2.Selection")));
            gridCheckMarksSelection2.View = null;
            this.myGridLookUpEdit2.Properties.GridSelection = gridCheckMarksSelection2;
            this.myGridLookUpEdit2.Properties.View = this.myGridLookUpEdit2View;
            this.myGridLookUpEdit2.Size = new System.Drawing.Size(321, 20);
            this.myGridLookUpEdit2.TabIndex = 1;
            // 
            // myGridLookUpEdit2View
            // 
            this.myGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.myGridLookUpEdit2View.Name = "myGridLookUpEdit2View";
            this.myGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.myGridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 113);
            this.Controls.Add(this.myGridLookUpEdit2);
            this.Controls.Add(this.myGridLookUpEdit1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridLookUpEdit2View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyGridLookUpEdit myGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView myGridLookUpEdit1View;
        private MyGridLookUpEdit myGridLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView myGridLookUpEdit2View;

    }
}

