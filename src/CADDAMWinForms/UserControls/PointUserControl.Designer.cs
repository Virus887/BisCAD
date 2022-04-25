
namespace BisCAD.UserControls
{
    partial class PointUserControl
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
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxPosition = new System.Windows.Forms.GroupBox();
            this.positionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericPositionX = new System.Windows.Forms.NumericUpDown();
            this.numericPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericPositionZ = new System.Windows.Forms.NumericUpDown();
            this.texboxName = new System.Windows.Forms.TextBox();
            this.mainTableLayoutPanel.SuspendLayout();
            this.groupBoxPosition.SuspendLayout();
            this.positionTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionZ)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxPosition, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.texboxName, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(581, 547);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // groupBoxPosition
            // 
            this.groupBoxPosition.Controls.Add(this.positionTableLayoutPanel);
            this.groupBoxPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPosition.Location = new System.Drawing.Point(3, 33);
            this.groupBoxPosition.Name = "groupBoxPosition";
            this.groupBoxPosition.Size = new System.Drawing.Size(575, 44);
            this.groupBoxPosition.TabIndex = 0;
            this.groupBoxPosition.TabStop = false;
            this.groupBoxPosition.Text = "Position";
            // 
            // positionTableLayoutPanel
            // 
            this.positionTableLayoutPanel.ColumnCount = 6;
            this.positionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.positionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.positionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.positionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.positionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.positionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.positionTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.positionTableLayoutPanel.Controls.Add(this.label3, 4, 0);
            this.positionTableLayoutPanel.Controls.Add(this.label2, 2, 0);
            this.positionTableLayoutPanel.Controls.Add(this.numericPositionX, 1, 0);
            this.positionTableLayoutPanel.Controls.Add(this.numericPositionY, 3, 0);
            this.positionTableLayoutPanel.Controls.Add(this.numericPositionZ, 5, 0);
            this.positionTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.positionTableLayoutPanel.Name = "positionTableLayoutPanel";
            this.positionTableLayoutPanel.RowCount = 1;
            this.positionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.positionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.positionTableLayoutPanel.Size = new System.Drawing.Size(569, 25);
            this.positionTableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(381, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(192, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericPositionX
            // 
            this.numericPositionX.DecimalPlaces = 2;
            this.numericPositionX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericPositionX.Location = new System.Drawing.Point(33, 3);
            this.numericPositionX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericPositionX.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericPositionX.Name = "numericPositionX";
            this.numericPositionX.Size = new System.Drawing.Size(120, 20);
            this.numericPositionX.TabIndex = 3;
            this.numericPositionX.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // numericPositionY
            // 
            this.numericPositionY.DecimalPlaces = 2;
            this.numericPositionY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericPositionY.Location = new System.Drawing.Point(222, 3);
            this.numericPositionY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericPositionY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericPositionY.Name = "numericPositionY";
            this.numericPositionY.Size = new System.Drawing.Size(120, 20);
            this.numericPositionY.TabIndex = 4;
            this.numericPositionY.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // numericPositionZ
            // 
            this.numericPositionZ.DecimalPlaces = 2;
            this.numericPositionZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericPositionZ.Location = new System.Drawing.Point(411, 3);
            this.numericPositionZ.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericPositionZ.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericPositionZ.Name = "numericPositionZ";
            this.numericPositionZ.Size = new System.Drawing.Size(120, 20);
            this.numericPositionZ.TabIndex = 5;
            this.numericPositionZ.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // texboxName
            // 
            this.texboxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.texboxName.Location = new System.Drawing.Point(3, 3);
            this.texboxName.Name = "texboxName";
            this.texboxName.Size = new System.Drawing.Size(575, 20);
            this.texboxName.TabIndex = 1;
            this.texboxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PointUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "PointUserControl";
            this.Size = new System.Drawing.Size(581, 547);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.groupBoxPosition.ResumeLayout(false);
            this.positionTableLayoutPanel.ResumeLayout(false);
            this.positionTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.GroupBox groupBoxPosition;
        private System.Windows.Forms.TableLayoutPanel positionTableLayoutPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericPositionX;
        private System.Windows.Forms.NumericUpDown numericPositionY;
        private System.Windows.Forms.NumericUpDown numericPositionZ;
        private System.Windows.Forms.TextBox texboxName;
    }
}
