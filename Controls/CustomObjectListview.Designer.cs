
namespace Controls
{
    partial class CustomObjectListview
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xloadinglabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.BackColor = System.Drawing.Color.Transparent;
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(0, 0);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(100, 23);
            this.xloadinglabel.TabIndex = 0;
            this.xloadinglabel.Text = "Loading...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            // 
            // CustomObjectListview
            // 
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullRowSelect = true;
            this.HeaderWordWrap = true;
            this.Location = new System.Drawing.Point(6, 66);
            this.MenuLabelColumns = "kolommen";
            this.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.MenuLabelLockGroupingOn = "Groepering vergrendelen op \'{0}\'";
            this.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.MenuLabelUnsort = "Uitsorteren";
            this.OwnerDraw = false;
            this.ShowCommandMenuOnRightClick = true;
            this.ShowGroups = false;
            this.ShowItemCountOnGroups = true;
            this.ShowItemToolTips = true;
            this.Size = new System.Drawing.Size(650, 350);
            this.SpaceBetweenGroups = 10;
            this.TabIndex = 26;
            this.TileSize = new System.Drawing.Size(300, 120);
            this.UseCompatibleStateImageBehavior = false;
            this.UseExplorerTheme = true;
            this.UseFiltering = true;
            this.UseHotControls = false;
            this.UseHotItem = true;
            this.UseOverlays = false;
            this.UseTranslucentHotItem = true;
            this.UseTranslucentSelection = true;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label xloadinglabel;
    }
}
