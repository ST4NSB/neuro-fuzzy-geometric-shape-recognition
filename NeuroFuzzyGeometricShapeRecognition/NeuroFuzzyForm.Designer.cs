namespace NeuroFuzzyGeometricShapeRecognition
{
    partial class NeuroFuzzyView
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPreprocessed = new System.Windows.Forms.Panel();
            this.panelPaint = new System.Windows.Forms.Panel();
            this.comboBoxShape = new System.Windows.Forms.ComboBox();
            this.comboBoxSession = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSendShape = new System.Windows.Forms.Button();
            this.btnResetImage = new System.Windows.Forms.Button();
            this.btnShowCenterPoint = new System.Windows.Forms.Button();
            this.btnShowSegments = new System.Windows.Forms.Button();
            this.btnPreprocessedShape = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnInterestPoints = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1108, 595);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "test button";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(519, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 24);
            this.label1.TabIndex = 39;
            this.label1.Text = "Processed from";
            // 
            // panelPreprocessed
            // 
            this.panelPreprocessed.BackColor = System.Drawing.Color.White;
            this.panelPreprocessed.Location = new System.Drawing.Point(463, 138);
            this.panelPreprocessed.Name = "panelPreprocessed";
            this.panelPreprocessed.Size = new System.Drawing.Size(300, 300);
            this.panelPreprocessed.TabIndex = 44;
            // 
            // panelPaint
            // 
            this.panelPaint.BackColor = System.Drawing.Color.White;
            this.panelPaint.Location = new System.Drawing.Point(121, 138);
            this.panelPaint.Name = "panelPaint";
            this.panelPaint.Size = new System.Drawing.Size(300, 300);
            this.panelPaint.TabIndex = 43;
            this.panelPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPaint_MouseDown);
            this.panelPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelPaint_MouseMove);
            // 
            // comboBoxShape
            // 
            this.comboBoxShape.FormattingEnabled = true;
            this.comboBoxShape.Location = new System.Drawing.Point(270, 102);
            this.comboBoxShape.Name = "comboBoxShape";
            this.comboBoxShape.Size = new System.Drawing.Size(121, 21);
            this.comboBoxShape.TabIndex = 41;
            // 
            // comboBoxSession
            // 
            this.comboBoxSession.FormattingEnabled = true;
            this.comboBoxSession.Items.AddRange(new object[] {
            "Training",
            "Testing"});
            this.comboBoxSession.Location = new System.Drawing.Point(130, 102);
            this.comboBoxSession.Name = "comboBoxSession";
            this.comboBoxSession.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSession.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 45;
            this.label2.Text = "Session type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(308, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "Shape type";
            // 
            // btnSendShape
            // 
            this.btnSendShape.BackColor = System.Drawing.Color.Orange;
            this.btnSendShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendShape.Location = new System.Drawing.Point(10, 138);
            this.btnSendShape.Name = "btnSendShape";
            this.btnSendShape.Size = new System.Drawing.Size(105, 52);
            this.btnSendShape.TabIndex = 42;
            this.btnSendShape.Text = "Send Shape";
            this.btnSendShape.UseVisualStyleBackColor = false;
            this.btnSendShape.Click += new System.EventHandler(this.btnSendShape_Click);
            // 
            // btnResetImage
            // 
            this.btnResetImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnResetImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetImage.Location = new System.Drawing.Point(12, 207);
            this.btnResetImage.Name = "btnResetImage";
            this.btnResetImage.Size = new System.Drawing.Size(103, 60);
            this.btnResetImage.TabIndex = 47;
            this.btnResetImage.Text = "Reset Image";
            this.btnResetImage.UseVisualStyleBackColor = false;
            this.btnResetImage.Click += new System.EventHandler(this.btnResetImage_Click);
            // 
            // btnShowCenterPoint
            // 
            this.btnShowCenterPoint.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnShowCenterPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowCenterPoint.Location = new System.Drawing.Point(10, 288);
            this.btnShowCenterPoint.Name = "btnShowCenterPoint";
            this.btnShowCenterPoint.Size = new System.Drawing.Size(105, 68);
            this.btnShowCenterPoint.TabIndex = 48;
            this.btnShowCenterPoint.Text = "Show Center Point";
            this.btnShowCenterPoint.UseVisualStyleBackColor = false;
            this.btnShowCenterPoint.Click += new System.EventHandler(this.btnShowCenterPoint_Click);
            // 
            // btnShowSegments
            // 
            this.btnShowSegments.BackColor = System.Drawing.SystemColors.Info;
            this.btnShowSegments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowSegments.Location = new System.Drawing.Point(10, 377);
            this.btnShowSegments.Name = "btnShowSegments";
            this.btnShowSegments.Size = new System.Drawing.Size(105, 48);
            this.btnShowSegments.TabIndex = 49;
            this.btnShowSegments.Text = "Show Segments";
            this.btnShowSegments.UseVisualStyleBackColor = false;
            this.btnShowSegments.Click += new System.EventHandler(this.btnShowSegments_Click);
            // 
            // btnPreprocessedShape
            // 
            this.btnPreprocessedShape.BackColor = System.Drawing.SystemColors.Info;
            this.btnPreprocessedShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreprocessedShape.Location = new System.Drawing.Point(12, 444);
            this.btnPreprocessedShape.Name = "btnPreprocessedShape";
            this.btnPreprocessedShape.Size = new System.Drawing.Size(121, 66);
            this.btnPreprocessedShape.TabIndex = 50;
            this.btnPreprocessedShape.Text = "Show Preprocessed Shape";
            this.btnPreprocessedShape.UseVisualStyleBackColor = false;
            this.btnPreprocessedShape.Click += new System.EventHandler(this.btnPreprocessedShape_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Orange;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(554, 460);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 79);
            this.button2.TabIndex = 51;
            this.button2.Text = "Show original shape";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnInterestPoints
            // 
            this.btnInterestPoints.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnInterestPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInterestPoints.Location = new System.Drawing.Point(144, 444);
            this.btnInterestPoints.Name = "btnInterestPoints";
            this.btnInterestPoints.Size = new System.Drawing.Size(105, 66);
            this.btnInterestPoints.TabIndex = 52;
            this.btnInterestPoints.Text = "Show Interest Points";
            this.btnInterestPoints.UseVisualStyleBackColor = false;
            this.btnInterestPoints.Click += new System.EventHandler(this.btnInterestPoints_Click);
            // 
            // NeuroFuzzyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 624);
            this.Controls.Add(this.btnInterestPoints);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnPreprocessedShape);
            this.Controls.Add(this.btnShowSegments);
            this.Controls.Add(this.btnShowCenterPoint);
            this.Controls.Add(this.btnResetImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelPreprocessed);
            this.Controls.Add(this.panelPaint);
            this.Controls.Add(this.btnSendShape);
            this.Controls.Add(this.comboBoxShape);
            this.Controls.Add(this.comboBoxSession);
            this.Controls.Add(this.button1);
            this.Name = "NeuroFuzzyView";
            this.Text = "Neuro-Fuzzy Main Application System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPreprocessed;
        private System.Windows.Forms.Panel panelPaint;
        private System.Windows.Forms.ComboBox comboBoxShape;
        private System.Windows.Forms.ComboBox comboBoxSession;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSendShape;
        private System.Windows.Forms.Button btnResetImage;
        private System.Windows.Forms.Button btnShowCenterPoint;
        private System.Windows.Forms.Button btnShowSegments;
        private System.Windows.Forms.Button btnPreprocessedShape;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnInterestPoints;
    }
}

