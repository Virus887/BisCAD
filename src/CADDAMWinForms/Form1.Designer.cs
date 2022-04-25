using OpenTK;

namespace BisCAD
{
    partial class MainForm
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
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.glControl = new OpenTK.GLControl();
            this.rightLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.userControlPanel = new System.Windows.Forms.Panel();
            this.leftTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.addElementGroupBox = new System.Windows.Forms.GroupBox();
            this.addElementFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addTorusButton = new System.Windows.Forms.Button();
            this.addPointButton = new System.Windows.Forms.Button();
            this.addBezierCurve = new System.Windows.Forms.Button();
            this.addBezierCurveC2 = new System.Windows.Forms.Button();
            this.groupBoxElements = new System.Windows.Forms.GroupBox();
            this.listBoxElements = new System.Windows.Forms.ListBox();
            this.buttonDeleteSelectedItems = new System.Windows.Forms.Button();
            this.CheckboxShowPlatform = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainLayoutPanel.SuspendLayout();
            this.rightLayoutPanel.SuspendLayout();
            this.leftTableLayoutPanel.SuspendLayout();
            this.addElementGroupBox.SuspendLayout();
            this.addElementFlowLayoutPanel.SuspendLayout();
            this.groupBoxElements.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
            this.mainLayoutPanel.ColumnCount = 3;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1200F));
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.mainLayoutPanel.Controls.Add(this.glControl, 1, 0);
            this.mainLayoutPanel.Controls.Add(this.rightLayoutPanel, 2, 0);
            this.mainLayoutPanel.Controls.Add(this.leftTableLayoutPanel, 0, 0);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 1;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 904F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(1800, 904);
            this.mainLayoutPanel.TabIndex = 0;
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(203, 3);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(1194, 898);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = true;
            this.glControl.Load += new System.EventHandler(this.GLControl_Load);
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);
            this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyDown);
            this.glControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyUp);
            this.glControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseClick);
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            this.glControl.Resize += new System.EventHandler(this.GLControl_Resize);
            this.glControl.Disposed += new System.EventHandler(this.GLControl_Dispose);
            this.glControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseWheel);
            // 
            // rightLayoutPanel
            // 
            this.rightLayoutPanel.ColumnCount = 1;
            this.rightLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightLayoutPanel.Controls.Add(this.userControlPanel, 0, 1);
            this.rightLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightLayoutPanel.Location = new System.Drawing.Point(1403, 3);
            this.rightLayoutPanel.Name = "rightLayoutPanel";
            this.rightLayoutPanel.RowCount = 3;
            this.rightLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.rightLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 848F));
            this.rightLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.rightLayoutPanel.Size = new System.Drawing.Size(394, 898);
            this.rightLayoutPanel.TabIndex = 1;
            // 
            // userControlPanel
            // 
            this.userControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlPanel.Location = new System.Drawing.Point(3, 25);
            this.userControlPanel.Name = "userControlPanel";
            this.userControlPanel.Size = new System.Drawing.Size(388, 842);
            this.userControlPanel.TabIndex = 0;
            // 
            // leftTableLayoutPanel
            // 
            this.leftTableLayoutPanel.ColumnCount = 1;
            this.leftTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftTableLayoutPanel.Controls.Add(this.addElementGroupBox, 0, 0);
            this.leftTableLayoutPanel.Controls.Add(this.groupBoxElements, 0, 1);
            this.leftTableLayoutPanel.Controls.Add(this.buttonDeleteSelectedItems, 0, 2);
            this.leftTableLayoutPanel.Controls.Add(this.CheckboxShowPlatform, 0, 3);
            this.leftTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.leftTableLayoutPanel.Name = "leftTableLayoutPanel";
            this.leftTableLayoutPanel.RowCount = 5;
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.42857F));
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.57143F));
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 488F));
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.leftTableLayoutPanel.Size = new System.Drawing.Size(194, 898);
            this.leftTableLayoutPanel.TabIndex = 16;
            // 
            // addElementGroupBox
            // 
            this.addElementGroupBox.Controls.Add(this.addElementFlowLayoutPanel);
            this.addElementGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addElementGroupBox.Location = new System.Drawing.Point(3, 3);
            this.addElementGroupBox.Name = "addElementGroupBox";
            this.addElementGroupBox.Size = new System.Drawing.Size(188, 83);
            this.addElementGroupBox.TabIndex = 16;
            this.addElementGroupBox.TabStop = false;
            this.addElementGroupBox.Text = "Add element";
            // 
            // addElementFlowLayoutPanel
            // 
            this.addElementFlowLayoutPanel.Controls.Add(this.addTorusButton);
            this.addElementFlowLayoutPanel.Controls.Add(this.addPointButton);
            this.addElementFlowLayoutPanel.Controls.Add(this.addBezierCurve);
            this.addElementFlowLayoutPanel.Controls.Add(this.addBezierCurveC2);
            this.addElementFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addElementFlowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.addElementFlowLayoutPanel.Name = "addElementFlowLayoutPanel";
            this.addElementFlowLayoutPanel.Size = new System.Drawing.Size(182, 64);
            this.addElementFlowLayoutPanel.TabIndex = 0;
            // 
            // addTorusButton
            // 
            this.addTorusButton.Location = new System.Drawing.Point(3, 3);
            this.addTorusButton.Name = "addTorusButton";
            this.addTorusButton.Size = new System.Drawing.Size(75, 23);
            this.addTorusButton.TabIndex = 0;
            this.addTorusButton.Text = "TORUS";
            this.addTorusButton.UseVisualStyleBackColor = true;
            this.addTorusButton.Click += new System.EventHandler(this.addTorusButton_Click);
            // 
            // addPointButton
            // 
            this.addPointButton.Location = new System.Drawing.Point(84, 3);
            this.addPointButton.Name = "addPointButton";
            this.addPointButton.Size = new System.Drawing.Size(75, 23);
            this.addPointButton.TabIndex = 1;
            this.addPointButton.Text = "Point";
            this.addPointButton.UseVisualStyleBackColor = true;
            this.addPointButton.Click += new System.EventHandler(this.addPointButton_Click);
            // 
            // addBezierCurve
            // 
            this.addBezierCurve.Location = new System.Drawing.Point(3, 32);
            this.addBezierCurve.Name = "addBezierCurve";
            this.addBezierCurve.Size = new System.Drawing.Size(75, 23);
            this.addBezierCurve.TabIndex = 2;
            this.addBezierCurve.Text = "Bezier Curve";
            this.addBezierCurve.UseVisualStyleBackColor = true;
            this.addBezierCurve.Click += new System.EventHandler(this.addBezierCurve_Click);
            // 
            // addBezierCurveC2
            // 
            this.addBezierCurveC2.Location = new System.Drawing.Point(84, 32);
            this.addBezierCurveC2.Name = "addBezierCurveC2";
            this.addBezierCurveC2.Size = new System.Drawing.Size(95, 23);
            this.addBezierCurveC2.TabIndex = 3;
            this.addBezierCurveC2.Text = "Bezier Curve C2";
            this.addBezierCurveC2.UseVisualStyleBackColor = true;
            this.addBezierCurveC2.Click += new System.EventHandler(this.addBezierCurveC2_Click);
            // 
            // groupBoxElements
            // 
            this.groupBoxElements.Controls.Add(this.listBoxElements);
            this.groupBoxElements.Location = new System.Drawing.Point(3, 92);
            this.groupBoxElements.Name = "groupBoxElements";
            this.groupBoxElements.Size = new System.Drawing.Size(188, 255);
            this.groupBoxElements.TabIndex = 17;
            this.groupBoxElements.TabStop = false;
            this.groupBoxElements.Text = "Scene elements";
            // 
            // listBoxElements
            // 
            this.listBoxElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxElements.FormattingEnabled = true;
            this.listBoxElements.Location = new System.Drawing.Point(3, 16);
            this.listBoxElements.Name = "listBoxElements";
            this.listBoxElements.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxElements.Size = new System.Drawing.Size(182, 236);
            this.listBoxElements.TabIndex = 0;
            this.listBoxElements.SelectedIndexChanged += new System.EventHandler(this.listBoxElements_SelectedIndexChanged);
            this.listBoxElements.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxElements_MouseDown);
            // 
            // buttonDeleteSelectedItems
            // 
            this.buttonDeleteSelectedItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDeleteSelectedItems.Location = new System.Drawing.Point(3, 353);
            this.buttonDeleteSelectedItems.Name = "buttonDeleteSelectedItems";
            this.buttonDeleteSelectedItems.Size = new System.Drawing.Size(188, 33);
            this.buttonDeleteSelectedItems.TabIndex = 18;
            this.buttonDeleteSelectedItems.Text = "Delete selected items";
            this.buttonDeleteSelectedItems.UseVisualStyleBackColor = true;
            this.buttonDeleteSelectedItems.Click += new System.EventHandler(this.buttonDeleteSelectedItems_Click);
            // 
            // CheckboxShowPlatform
            // 
            this.CheckboxShowPlatform.AutoSize = true;
            this.CheckboxShowPlatform.Location = new System.Drawing.Point(3, 392);
            this.CheckboxShowPlatform.Name = "CheckboxShowPlatform";
            this.CheckboxShowPlatform.Size = new System.Drawing.Size(93, 17);
            this.CheckboxShowPlatform.TabIndex = 15;
            this.CheckboxShowPlatform.Text = "Show platform";
            this.CheckboxShowPlatform.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 928);
            this.Controls.Add(this.mainLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "BisCAD";
            this.mainLayoutPanel.ResumeLayout(false);
            this.rightLayoutPanel.ResumeLayout(false);
            this.leftTableLayoutPanel.ResumeLayout(false);
            this.leftTableLayoutPanel.PerformLayout();
            this.addElementGroupBox.ResumeLayout(false);
            this.addElementFlowLayoutPanel.ResumeLayout(false);
            this.groupBoxElements.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private GLControl glControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel rightLayoutPanel;
        private System.Windows.Forms.CheckBox CheckboxShowPlatform;
        private System.Windows.Forms.TableLayoutPanel leftTableLayoutPanel;
        private System.Windows.Forms.GroupBox addElementGroupBox;
        private System.Windows.Forms.FlowLayoutPanel addElementFlowLayoutPanel;
        private System.Windows.Forms.Button addTorusButton;
        private System.Windows.Forms.Panel userControlPanel;
        private System.Windows.Forms.Button addPointButton;
        private System.Windows.Forms.GroupBox groupBoxElements;
        private System.Windows.Forms.ListBox listBoxElements;
        private System.Windows.Forms.Button buttonDeleteSelectedItems;
        private System.Windows.Forms.Button addBezierCurve;
        private System.Windows.Forms.Button addBezierCurveC2;
    }
}

