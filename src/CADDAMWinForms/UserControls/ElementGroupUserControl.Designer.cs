
using BisCAD.ParametricModels;
using OpenTK;
using System;

namespace BisCAD.UserControls
{
    partial class ElementGroupUserControl
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
            //Apply rotations to models
            _selectedItems.ForEach(model =>
            {
                Matrix4 transform = Matrix4.Identity;

                float rotationX = (float)numericRotationX.Value;
                float rotationY = (float)numericRotationY.Value;
                float rotationZ = (float)numericRotationZ.Value;

                float scaleX = (float)numericScaleX.Value;
                float scaleY = (float)numericScaleY.Value;
                float scaleZ = (float)numericScaleZ.Value;

                Vector3 cursorPos = _cursor.Position;
                Vector3 modelPos = model.Position;

                transform = Matrix4.CreateTranslation(modelPos - cursorPos)
                * Matrix4.CreateScale(scaleX, scaleY, scaleZ)
                * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationZ))
                * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY))
                * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationX))
                * Matrix4.CreateTranslation(cursorPos - modelPos);


                if (model is TorusParametricModel)
                {
                    model.PrevModel = transform; //* model.CurrentModel ;
                    model.PrevModel = Matrix4.Identity;

                    var x = transform * model.CurrentModel;

                    model.PositionX = x.M41;
                    model.PositionY = x.M42;
                    model.PositionZ = x.M43;

                    //x.Transpose();

                    var rotX = (float)Math.Atan2(x[2, 1], x[2, 2]);
                    var rotY = (float)Math.Atan2(-x[2, 0], Math.Sqrt(Math.Pow(x[2, 1], 2) + Math.Pow(x[2, 2], 2)));
                    var rotZ = (float)Math.Atan2(x[1, 0], x[0, 0]);

                    //var beta = (float)Math.Atan2(-x[0, 2], Math.Sqrt(Math.Pow(x[0, 0], 2) + Math.Pow(x[0, 1], 2))); //beta
                    //var rotX = (float)Math.Atan2(x[1, 2] / Math.Cos(beta), x[2, 2] / Math.Cos(beta)); //alpha
                    //var rotZ = (float)Math.Atan2(-x[0, 2] / Math.Cos(beta), x[0, 0] / Math.Cos(beta)); //gamma
                    //float rotY = beta;

                    model.RotationX = MathHelper.RadiansToDegrees(rotX);
                    model.RotationY = MathHelper.RadiansToDegrees(rotY);
                    model.RotationZ = MathHelper.RadiansToDegrees(rotZ);
                }
            });
        

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxScreenPosition = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numericScreenPositionX = new System.Windows.Forms.NumericUpDown();
            this.numericScreenPositionY = new System.Windows.Forms.NumericUpDown();
            this.groupBoxScale = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericScaleX = new System.Windows.Forms.NumericUpDown();
            this.numericScaleY = new System.Windows.Forms.NumericUpDown();
            this.numericScaleZ = new System.Windows.Forms.NumericUpDown();
            this.groupBoxRotation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericRotationX = new System.Windows.Forms.NumericUpDown();
            this.numericRotationY = new System.Windows.Forms.NumericUpDown();
            this.numericRotationZ = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPosition = new System.Windows.Forms.GroupBox();
            this.positionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericPositionX = new System.Windows.Forms.NumericUpDown();
            this.numericPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericPositionZ = new System.Windows.Forms.NumericUpDown();
            this.textboxName = new System.Windows.Forms.TextBox();
            this.mainTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxScreenPosition.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScreenPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScreenPositionY)).BeginInit();
            this.groupBoxScale.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleZ)).BeginInit();
            this.groupBoxRotation.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationZ)).BeginInit();
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
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.11583F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.88417F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(483, 606);
            this.mainTableLayoutPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxScreenPosition, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxScale, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxRotation, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxPosition, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textboxName, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(477, 479);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBoxScreenPosition
            // 
            this.groupBoxScreenPosition.Controls.Add(this.tableLayoutPanel4);
            this.groupBoxScreenPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxScreenPosition.Location = new System.Drawing.Point(3, 275);
            this.groupBoxScreenPosition.Name = "groupBoxScreenPosition";
            this.groupBoxScreenPosition.Size = new System.Drawing.Size(471, 48);
            this.groupBoxScreenPosition.TabIndex = 4;
            this.groupBoxScreenPosition.TabStop = false;
            this.groupBoxScreenPosition.Text = "Cursor screen position";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.numericScreenPositionX, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.numericScreenPositionY, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(465, 29);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 29);
            this.label10.TabIndex = 0;
            this.label10.Text = "X:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(235, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 29);
            this.label12.TabIndex = 1;
            this.label12.Text = "Y:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericScreenPositionX
            // 
            this.numericScreenPositionX.DecimalPlaces = 2;
            this.numericScreenPositionX.Enabled = false;
            this.numericScreenPositionX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScreenPositionX.Location = new System.Drawing.Point(33, 3);
            this.numericScreenPositionX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericScreenPositionX.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericScreenPositionX.Name = "numericScreenPositionX";
            this.numericScreenPositionX.Size = new System.Drawing.Size(92, 20);
            this.numericScreenPositionX.TabIndex = 3;
            this.numericScreenPositionX.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // numericScreenPositionY
            // 
            this.numericScreenPositionY.DecimalPlaces = 2;
            this.numericScreenPositionY.Enabled = false;
            this.numericScreenPositionY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScreenPositionY.Location = new System.Drawing.Point(265, 3);
            this.numericScreenPositionY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericScreenPositionY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericScreenPositionY.Name = "numericScreenPositionY";
            this.numericScreenPositionY.Size = new System.Drawing.Size(92, 20);
            this.numericScreenPositionY.TabIndex = 4;
            this.numericScreenPositionY.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // groupBoxScale
            // 
            this.groupBoxScale.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxScale.Location = new System.Drawing.Point(3, 133);
            this.groupBoxScale.Name = "groupBoxScale";
            this.groupBoxScale.Size = new System.Drawing.Size(471, 44);
            this.groupBoxScale.TabIndex = 2;
            this.groupBoxScale.TabStop = false;
            this.groupBoxScale.Text = "Scale";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericScaleX, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericScaleY, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericScaleZ, 5, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(465, 25);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "X:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(313, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "Z:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(158, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 25);
            this.label9.TabIndex = 1;
            this.label9.Text = "Y:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericScaleX
            // 
            this.numericScaleX.DecimalPlaces = 2;
            this.numericScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleX.Location = new System.Drawing.Point(33, 3);
            this.numericScaleX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericScaleX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleX.Name = "numericScaleX";
            this.numericScaleX.Size = new System.Drawing.Size(92, 20);
            this.numericScaleX.TabIndex = 3;
            this.numericScaleX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericScaleX.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // numericScaleY
            // 
            this.numericScaleY.DecimalPlaces = 2;
            this.numericScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleY.Location = new System.Drawing.Point(188, 3);
            this.numericScaleY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericScaleY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleY.Name = "numericScaleY";
            this.numericScaleY.Size = new System.Drawing.Size(92, 20);
            this.numericScaleY.TabIndex = 4;
            this.numericScaleY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericScaleY.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // numericScaleZ
            // 
            this.numericScaleZ.DecimalPlaces = 2;
            this.numericScaleZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleZ.Location = new System.Drawing.Point(343, 3);
            this.numericScaleZ.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericScaleZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleZ.Name = "numericScaleZ";
            this.numericScaleZ.Size = new System.Drawing.Size(93, 20);
            this.numericScaleZ.TabIndex = 5;
            this.numericScaleZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericScaleZ.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // groupBoxRotation
            // 
            this.groupBoxRotation.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxRotation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRotation.Location = new System.Drawing.Point(3, 83);
            this.groupBoxRotation.Name = "groupBoxRotation";
            this.groupBoxRotation.Size = new System.Drawing.Size(471, 44);
            this.groupBoxRotation.TabIndex = 1;
            this.groupBoxRotation.TabStop = false;
            this.groupBoxRotation.Text = "Rotation";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericRotationX, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericRotationY, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericRotationZ, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(465, 25);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "X:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(313, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Z:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(158, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "Y:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericRotationX
            // 
            this.numericRotationX.DecimalPlaces = 2;
            this.numericRotationX.Location = new System.Drawing.Point(33, 3);
            this.numericRotationX.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericRotationX.Name = "numericRotationX";
            this.numericRotationX.Size = new System.Drawing.Size(92, 20);
            this.numericRotationX.TabIndex = 3;
            this.numericRotationX.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // numericRotationY
            // 
            this.numericRotationY.DecimalPlaces = 2;
            this.numericRotationY.Location = new System.Drawing.Point(188, 3);
            this.numericRotationY.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericRotationY.Name = "numericRotationY";
            this.numericRotationY.Size = new System.Drawing.Size(92, 20);
            this.numericRotationY.TabIndex = 4;
            this.numericRotationY.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // numericRotationZ
            // 
            this.numericRotationZ.DecimalPlaces = 2;
            this.numericRotationZ.Location = new System.Drawing.Point(343, 3);
            this.numericRotationZ.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericRotationZ.Name = "numericRotationZ";
            this.numericRotationZ.Size = new System.Drawing.Size(93, 20);
            this.numericRotationZ.TabIndex = 5;
            this.numericRotationZ.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // groupBoxPosition
            // 
            this.groupBoxPosition.Controls.Add(this.positionTableLayoutPanel);
            this.groupBoxPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPosition.Location = new System.Drawing.Point(3, 33);
            this.groupBoxPosition.Name = "groupBoxPosition";
            this.groupBoxPosition.Size = new System.Drawing.Size(471, 44);
            this.groupBoxPosition.TabIndex = 0;
            this.groupBoxPosition.TabStop = false;
            this.groupBoxPosition.Text = "Center position";
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
            this.positionTableLayoutPanel.Size = new System.Drawing.Size(465, 25);
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
            this.label3.Location = new System.Drawing.Point(313, 0);
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
            this.label2.Location = new System.Drawing.Point(158, 0);
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
            500,
            0,
            0,
            131072});
            this.numericPositionX.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147352576});
            this.numericPositionX.Name = "numericPositionX";
            this.numericPositionX.Size = new System.Drawing.Size(92, 20);
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
            this.numericPositionY.Location = new System.Drawing.Point(188, 3);
            this.numericPositionY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericPositionY.Name = "numericPositionY";
            this.numericPositionY.Size = new System.Drawing.Size(92, 20);
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
            this.numericPositionZ.Location = new System.Drawing.Point(343, 3);
            this.numericPositionZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericPositionZ.Name = "numericPositionZ";
            this.numericPositionZ.Size = new System.Drawing.Size(93, 20);
            this.numericPositionZ.TabIndex = 5;
            this.numericPositionZ.ValueChanged += new System.EventHandler(this.RefreshOnValueChanged);
            // 
            // textboxName
            // 
            this.textboxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxName.Enabled = false;
            this.textboxName.Location = new System.Drawing.Point(3, 3);
            this.textboxName.Name = "textboxName";
            this.textboxName.Size = new System.Drawing.Size(471, 20);
            this.textboxName.TabIndex = 3;
            this.textboxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ElementGroupUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "ElementGroupUserControl";
            this.Size = new System.Drawing.Size(483, 606);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxScreenPosition.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScreenPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScreenPositionY)).EndInit();
            this.groupBoxScale.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleZ)).EndInit();
            this.groupBoxRotation.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationZ)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxScale;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericScaleX;
        private System.Windows.Forms.NumericUpDown numericScaleY;
        private System.Windows.Forms.NumericUpDown numericScaleZ;
        private System.Windows.Forms.GroupBox groupBoxRotation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericRotationX;
        private System.Windows.Forms.NumericUpDown numericRotationY;
        private System.Windows.Forms.NumericUpDown numericRotationZ;
        private System.Windows.Forms.GroupBox groupBoxPosition;
        private System.Windows.Forms.TableLayoutPanel positionTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericPositionX;
        private System.Windows.Forms.NumericUpDown numericPositionY;
        private System.Windows.Forms.NumericUpDown numericPositionZ;
        private System.Windows.Forms.TextBox textboxName;
        private System.Windows.Forms.GroupBox groupBoxScreenPosition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericScreenPositionX;
        private System.Windows.Forms.NumericUpDown numericScreenPositionY;
    }
}
