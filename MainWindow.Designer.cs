namespace MetalBallsTriangulation
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_InputParameters = new System.Windows.Forms.GroupBox();
            this.numUpDown_PointsCountY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDown_PointsCountX = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_SmallRadius = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_BigRadius = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_PhysicalSystem = new System.Windows.Forms.GroupBox();
            this.pictureBox_System = new System.Windows.Forms.PictureBox();
            this.button_DrawSystem = new System.Windows.Forms.Button();
            this.groupBox_Lines = new System.Windows.Forms.GroupBox();
            this.checkIsolines = new System.Windows.Forms.CheckBox();
            this.checkFieldLines = new System.Windows.Forms.CheckBox();
            this.button_PhysWork = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownQ = new System.Windows.Forms.NumericUpDown();
            this.groupBox_InputParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_PointsCountY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_PointsCountX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SmallRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BigRadius)).BeginInit();
            this.groupBox_PhysicalSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_System)).BeginInit();
            this.groupBox_Lines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQ)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_InputParameters
            // 
            this.groupBox_InputParameters.Controls.Add(this.numericUpDownQ);
            this.groupBox_InputParameters.Controls.Add(this.label5);
            this.groupBox_InputParameters.Controls.Add(this.numUpDown_PointsCountY);
            this.groupBox_InputParameters.Controls.Add(this.label4);
            this.groupBox_InputParameters.Controls.Add(this.numUpDown_PointsCountX);
            this.groupBox_InputParameters.Controls.Add(this.label3);
            this.groupBox_InputParameters.Controls.Add(this.numericUpDown_SmallRadius);
            this.groupBox_InputParameters.Controls.Add(this.label2);
            this.groupBox_InputParameters.Controls.Add(this.numericUpDown_BigRadius);
            this.groupBox_InputParameters.Controls.Add(this.label1);
            this.groupBox_InputParameters.Location = new System.Drawing.Point(12, 12);
            this.groupBox_InputParameters.Name = "groupBox_InputParameters";
            this.groupBox_InputParameters.Size = new System.Drawing.Size(207, 156);
            this.groupBox_InputParameters.TabIndex = 1;
            this.groupBox_InputParameters.TabStop = false;
            this.groupBox_InputParameters.Text = "Входные параметры";
            // 
            // numUpDown_PointsCountY
            // 
            this.numUpDown_PointsCountY.Location = new System.Drawing.Point(139, 97);
            this.numUpDown_PointsCountY.Name = "numUpDown_PointsCountY";
            this.numUpDown_PointsCountY.Size = new System.Drawing.Size(57, 20);
            this.numUpDown_PointsCountY.TabIndex = 7;
            this.numUpDown_PointsCountY.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Количество точек по Y:";
            // 
            // numUpDown_PointsCountX
            // 
            this.numUpDown_PointsCountX.Location = new System.Drawing.Point(139, 71);
            this.numUpDown_PointsCountX.Name = "numUpDown_PointsCountX";
            this.numUpDown_PointsCountX.Size = new System.Drawing.Size(57, 20);
            this.numUpDown_PointsCountX.TabIndex = 5;
            this.numUpDown_PointsCountX.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Количество точек по X:";
            // 
            // numericUpDown_SmallRadius
            // 
            this.numericUpDown_SmallRadius.Location = new System.Drawing.Point(139, 45);
            this.numericUpDown_SmallRadius.Name = "numericUpDown_SmallRadius";
            this.numericUpDown_SmallRadius.Size = new System.Drawing.Size(57, 20);
            this.numericUpDown_SmallRadius.TabIndex = 3;
            this.numericUpDown_SmallRadius.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Радиус малого шара:";
            // 
            // numericUpDown_BigRadius
            // 
            this.numericUpDown_BigRadius.Location = new System.Drawing.Point(139, 19);
            this.numericUpDown_BigRadius.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_BigRadius.Name = "numericUpDown_BigRadius";
            this.numericUpDown_BigRadius.Size = new System.Drawing.Size(57, 20);
            this.numericUpDown_BigRadius.TabIndex = 1;
            this.numericUpDown_BigRadius.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Радиус большого шара:";
            // 
            // groupBox_PhysicalSystem
            // 
            this.groupBox_PhysicalSystem.Controls.Add(this.pictureBox_System);
            this.groupBox_PhysicalSystem.Location = new System.Drawing.Point(330, 14);
            this.groupBox_PhysicalSystem.Name = "groupBox_PhysicalSystem";
            this.groupBox_PhysicalSystem.Size = new System.Drawing.Size(1014, 825);
            this.groupBox_PhysicalSystem.TabIndex = 2;
            this.groupBox_PhysicalSystem.TabStop = false;
            this.groupBox_PhysicalSystem.Text = "Система из 2 шаров";
            // 
            // pictureBox_System
            // 
            this.pictureBox_System.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox_System.Location = new System.Drawing.Point(6, 19);
            this.pictureBox_System.Name = "pictureBox_System";
            this.pictureBox_System.Size = new System.Drawing.Size(1000, 800);
            this.pictureBox_System.TabIndex = 0;
            this.pictureBox_System.TabStop = false;
            // 
            // button_DrawSystem
            // 
            this.button_DrawSystem.Location = new System.Drawing.Point(12, 299);
            this.button_DrawSystem.Name = "button_DrawSystem";
            this.button_DrawSystem.Size = new System.Drawing.Size(207, 65);
            this.button_DrawSystem.TabIndex = 3;
            this.button_DrawSystem.Text = "Нарисовать систему";
            this.button_DrawSystem.UseVisualStyleBackColor = true;
            this.button_DrawSystem.Click += new System.EventHandler(this.button_DrawSystem_Click);
            // 
            // groupBox_Lines
            // 
            this.groupBox_Lines.Controls.Add(this.checkFieldLines);
            this.groupBox_Lines.Controls.Add(this.checkIsolines);
            this.groupBox_Lines.Location = new System.Drawing.Point(12, 193);
            this.groupBox_Lines.Name = "groupBox_Lines";
            this.groupBox_Lines.Size = new System.Drawing.Size(207, 89);
            this.groupBox_Lines.TabIndex = 4;
            this.groupBox_Lines.TabStop = false;
            this.groupBox_Lines.Text = "Параметры линий";
            // 
            // checkIsolines
            // 
            this.checkIsolines.AutoSize = true;
            this.checkIsolines.Location = new System.Drawing.Point(7, 20);
            this.checkIsolines.Name = "checkIsolines";
            this.checkIsolines.Size = new System.Drawing.Size(126, 17);
            this.checkIsolines.TabIndex = 0;
            this.checkIsolines.Text = "Включить изолинии";
            this.checkIsolines.UseVisualStyleBackColor = true;
            // 
            // checkFieldLines
            // 
            this.checkFieldLines.AutoSize = true;
            this.checkFieldLines.Location = new System.Drawing.Point(7, 44);
            this.checkFieldLines.Name = "checkFieldLines";
            this.checkFieldLines.Size = new System.Drawing.Size(155, 17);
            this.checkFieldLines.TabIndex = 1;
            this.checkFieldLines.Text = "Включить силовые линии";
            this.checkFieldLines.UseVisualStyleBackColor = true;
            // 
            // button_PhysWork
            // 
            this.button_PhysWork.Location = new System.Drawing.Point(12, 370);
            this.button_PhysWork.Name = "button_PhysWork";
            this.button_PhysWork.Size = new System.Drawing.Size(207, 65);
            this.button_PhysWork.TabIndex = 5;
            this.button_PhysWork.Text = "Рассчет поля";
            this.button_PhysWork.UseVisualStyleBackColor = true;
            this.button_PhysWork.Click += new System.EventHandler(this.button_PhysWork_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Заряд Q(+/-):";
            // 
            // numericUpDownQ
            // 
            this.numericUpDownQ.Location = new System.Drawing.Point(139, 123);
            this.numericUpDownQ.Name = "numericUpDownQ";
            this.numericUpDownQ.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownQ.TabIndex = 8;
            this.numericUpDownQ.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 851);
            this.Controls.Add(this.button_PhysWork);
            this.Controls.Add(this.groupBox_Lines);
            this.Controls.Add(this.button_DrawSystem);
            this.Controls.Add(this.groupBox_PhysicalSystem);
            this.Controls.Add(this.groupBox_InputParameters);
            this.Name = "MainWindow";
            this.Text = "2 металических шара";
            this.groupBox_InputParameters.ResumeLayout(false);
            this.groupBox_InputParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_PointsCountY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_PointsCountX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SmallRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BigRadius)).EndInit();
            this.groupBox_PhysicalSystem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_System)).EndInit();
            this.groupBox_Lines.ResumeLayout(false);
            this.groupBox_Lines.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_InputParameters;
        private System.Windows.Forms.NumericUpDown numericUpDown_SmallRadius;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_BigRadius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_PhysicalSystem;
        private System.Windows.Forms.PictureBox pictureBox_System;
        private System.Windows.Forms.Button button_DrawSystem;
        private System.Windows.Forms.NumericUpDown numUpDown_PointsCountX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numUpDown_PointsCountY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox_Lines;
        private System.Windows.Forms.CheckBox checkFieldLines;
        private System.Windows.Forms.CheckBox checkIsolines;
        private System.Windows.Forms.Button button_PhysWork;
        private System.Windows.Forms.NumericUpDown numericUpDownQ;
        private System.Windows.Forms.Label label5;
    }
}

