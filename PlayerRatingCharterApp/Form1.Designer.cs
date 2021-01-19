namespace PlayerRatingCharterApp
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
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Simulate = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.PlayerCount = new System.Windows.Forms.NumericUpDown();
            this.GamesPlayed = new System.Windows.Forms.NumericUpDown();
            this.kFactor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MinPlayerSkillLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MinPlayerSkill = new System.Windows.Forms.NumericUpDown();
            this.MaxPlayerSkill = new System.Windows.Forms.NumericUpDown();
            this.gamesPlayedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skillDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gamesWonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gamesLostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.PredictionAccuracy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesPlayed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinPlayerSkill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxPlayerSkill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 93);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(673, 347);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // Simulate
            // 
            this.Simulate.Location = new System.Drawing.Point(12, 12);
            this.Simulate.Name = "Simulate";
            this.Simulate.Size = new System.Drawing.Size(94, 75);
            this.Simulate.TabIndex = 1;
            this.Simulate.Text = "Simulate";
            this.Simulate.UseVisualStyleBackColor = true;
            this.Simulate.Click += new System.EventHandler(this.Simulate_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gamesPlayedDataGridViewTextBoxColumn,
            this.skillDataGridViewTextBoxColumn,
            this.ratingDataGridViewTextBoxColumn,
            this.gamesWonDataGridViewTextBoxColumn,
            this.gamesLostDataGridViewTextBoxColumn});
            this.dataGrid.DataSource = this.playerBindingSource;
            this.dataGrid.Location = new System.Drawing.Point(12, 446);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(556, 150);
            this.dataGrid.TabIndex = 3;
            // 
            // PlayerCount
            // 
            this.PlayerCount.Location = new System.Drawing.Point(115, 28);
            this.PlayerCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PlayerCount.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.PlayerCount.Name = "PlayerCount";
            this.PlayerCount.Size = new System.Drawing.Size(120, 20);
            this.PlayerCount.TabIndex = 4;
            this.PlayerCount.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // GamesPlayed
            // 
            this.GamesPlayed.Location = new System.Drawing.Point(241, 30);
            this.GamesPlayed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.GamesPlayed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GamesPlayed.Name = "GamesPlayed";
            this.GamesPlayed.Size = new System.Drawing.Size(120, 20);
            this.GamesPlayed.TabIndex = 5;
            this.GamesPlayed.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // kFactor
            // 
            this.kFactor.Location = new System.Drawing.Point(115, 67);
            this.kFactor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.kFactor.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.kFactor.Name = "kFactor";
            this.kFactor.Size = new System.Drawing.Size(120, 20);
            this.kFactor.TabIndex = 6;
            this.kFactor.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "PlayerPool";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "GamesPlayed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "kFactor";
            // 
            // MinPlayerSkillLabel
            // 
            this.MinPlayerSkillLabel.AutoSize = true;
            this.MinPlayerSkillLabel.Location = new System.Drawing.Point(364, 11);
            this.MinPlayerSkillLabel.Name = "MinPlayerSkillLabel";
            this.MinPlayerSkillLabel.Size = new System.Drawing.Size(75, 13);
            this.MinPlayerSkillLabel.TabIndex = 10;
            this.MinPlayerSkillLabel.Text = "Min player skill";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(490, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Max player skill";
            // 
            // MinPlayerSkill
            // 
            this.MinPlayerSkill.Location = new System.Drawing.Point(367, 30);
            this.MinPlayerSkill.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.MinPlayerSkill.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinPlayerSkill.Name = "MinPlayerSkill";
            this.MinPlayerSkill.Size = new System.Drawing.Size(120, 20);
            this.MinPlayerSkill.TabIndex = 12;
            this.MinPlayerSkill.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // MaxPlayerSkill
            // 
            this.MaxPlayerSkill.Location = new System.Drawing.Point(493, 30);
            this.MaxPlayerSkill.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxPlayerSkill.Name = "MaxPlayerSkill";
            this.MaxPlayerSkill.Size = new System.Drawing.Size(120, 20);
            this.MaxPlayerSkill.TabIndex = 13;
            this.MaxPlayerSkill.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // gamesPlayedDataGridViewTextBoxColumn
            // 
            this.gamesPlayedDataGridViewTextBoxColumn.DataPropertyName = "gamesPlayed";
            this.gamesPlayedDataGridViewTextBoxColumn.HeaderText = "gamesPlayed";
            this.gamesPlayedDataGridViewTextBoxColumn.Name = "gamesPlayedDataGridViewTextBoxColumn";
            this.gamesPlayedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skillDataGridViewTextBoxColumn
            // 
            this.skillDataGridViewTextBoxColumn.DataPropertyName = "skill";
            this.skillDataGridViewTextBoxColumn.HeaderText = "skill";
            this.skillDataGridViewTextBoxColumn.Name = "skillDataGridViewTextBoxColumn";
            this.skillDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ratingDataGridViewTextBoxColumn
            // 
            this.ratingDataGridViewTextBoxColumn.DataPropertyName = "rating";
            this.ratingDataGridViewTextBoxColumn.HeaderText = "rating";
            this.ratingDataGridViewTextBoxColumn.Name = "ratingDataGridViewTextBoxColumn";
            this.ratingDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gamesWonDataGridViewTextBoxColumn
            // 
            this.gamesWonDataGridViewTextBoxColumn.DataPropertyName = "gamesWon";
            this.gamesWonDataGridViewTextBoxColumn.HeaderText = "gamesWon";
            this.gamesWonDataGridViewTextBoxColumn.Name = "gamesWonDataGridViewTextBoxColumn";
            this.gamesWonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gamesLostDataGridViewTextBoxColumn
            // 
            this.gamesLostDataGridViewTextBoxColumn.DataPropertyName = "gamesLost";
            this.gamesLostDataGridViewTextBoxColumn.HeaderText = "gamesLost";
            this.gamesLostDataGridViewTextBoxColumn.Name = "gamesLostDataGridViewTextBoxColumn";
            this.gamesLostDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // playerBindingSource
            // 
            this.playerBindingSource.DataSource = typeof(PlayerRatingCharterApp.Player);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(575, 447);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Prediction accuracy:";
            // 
            // PredictionAccuracy
            // 
            this.PredictionAccuracy.AutoSize = true;
            this.PredictionAccuracy.Location = new System.Drawing.Point(575, 464);
            this.PredictionAccuracy.Name = "PredictionAccuracy";
            this.PredictionAccuracy.Size = new System.Drawing.Size(22, 13);
            this.PredictionAccuracy.TabIndex = 15;
            this.PredictionAccuracy.Text = "0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 626);
            this.Controls.Add(this.PredictionAccuracy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MaxPlayerSkill);
            this.Controls.Add(this.MinPlayerSkill);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MinPlayerSkillLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kFactor);
            this.Controls.Add(this.GamesPlayed);
            this.Controls.Add(this.PlayerCount);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.Simulate);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesPlayed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinPlayerSkill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxPlayerSkill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button Simulate;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.BindingSource playerBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn gamesPlayedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skillDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gamesWonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gamesLostDataGridViewTextBoxColumn;
        private System.Windows.Forms.NumericUpDown PlayerCount;
        private System.Windows.Forms.NumericUpDown GamesPlayed;
        private System.Windows.Forms.NumericUpDown kFactor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label MinPlayerSkillLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown MinPlayerSkill;
        private System.Windows.Forms.NumericUpDown MaxPlayerSkill;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PredictionAccuracy;
    }
}

