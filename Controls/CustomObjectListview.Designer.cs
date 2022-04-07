
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
            this.SuspendLayout();
            // 
            // CustomObjectListview
            // 
            this.Name = "CustomObjectListview";
            this.Size = new System.Drawing.Size(650, 350);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullRowSelect = true;
            this.HeaderWordWrap = true;
            this.HideSelection = false;
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
            this.View = System.Windows.Forms.View.Details;
            this.ResumeLayout(false);
        }

        #endregion
    }
}
