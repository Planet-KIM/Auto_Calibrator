
namespace AddOnProject
{
    partial class Mysql
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameBox = new System.Windows.Forms.TextBox();
            this.pwdBox = new System.Windows.Forms.TextBox();
            this.TextLabel = new System.Windows.Forms.Label();
            this.PasswordText = new System.Windows.Forms.Label();
            this.UserInsert = new System.Windows.Forms.Button();
            this.FmuInsert = new System.Windows.Forms.Button();
            this.FmuLabel = new System.Windows.Forms.Label();
            this.FmuBox = new System.Windows.Forms.TextBox();
            this.FmuSelect = new System.Windows.Forms.Button();
            this.SELECT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(138, 42);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(105, 21);
            this.nameBox.TabIndex = 0;
            // 
            // pwdBox
            // 
            this.pwdBox.Location = new System.Drawing.Point(138, 83);
            this.pwdBox.Name = "pwdBox";
            this.pwdBox.PasswordChar = '*';
            this.pwdBox.Size = new System.Drawing.Size(105, 21);
            this.pwdBox.TabIndex = 1;
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.BackColor = System.Drawing.Color.DarkGray;
            this.TextLabel.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextLabel.Location = new System.Drawing.Point(29, 44);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(105, 19);
            this.TextLabel.TabIndex = 2;
            this.TextLabel.Text = "이      름 :";
            this.TextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordText
            // 
            this.PasswordText.AutoSize = true;
            this.PasswordText.BackColor = System.Drawing.Color.DarkGray;
            this.PasswordText.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PasswordText.Location = new System.Drawing.Point(29, 85);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(103, 19);
            this.PasswordText.TabIndex = 3;
            this.PasswordText.Text = "비밀번호 :";
            // 
            // UserInsert
            // 
            this.UserInsert.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.UserInsert.Location = new System.Drawing.Point(249, 39);
            this.UserInsert.Name = "UserInsert";
            this.UserInsert.Size = new System.Drawing.Size(165, 65);
            this.UserInsert.TabIndex = 5;
            this.UserInsert.Text = "INSERT";
            this.UserInsert.UseVisualStyleBackColor = true;
            this.UserInsert.Click += new System.EventHandler(this.button_Mysql_Insert);
            // 
            // FmuInsert
            // 
            this.FmuInsert.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FmuInsert.Location = new System.Drawing.Point(236, 216);
            this.FmuInsert.Name = "FmuInsert";
            this.FmuInsert.Size = new System.Drawing.Size(178, 41);
            this.FmuInsert.TabIndex = 10;
            this.FmuInsert.Text = "INSERT";
            this.FmuInsert.UseVisualStyleBackColor = true;
            this.FmuInsert.Click += new System.EventHandler(this.button_Fmu_Insert);
            // 
            // FmuLabel
            // 
            this.FmuLabel.AutoSize = true;
            this.FmuLabel.BackColor = System.Drawing.Color.DarkGray;
            this.FmuLabel.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FmuLabel.Location = new System.Drawing.Point(43, 182);
            this.FmuLabel.Name = "FmuLabel";
            this.FmuLabel.Size = new System.Drawing.Size(89, 19);
            this.FmuLabel.TabIndex = 8;
            this.FmuLabel.Text = "F  M  U :";
            this.FmuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FmuBox
            // 
            this.FmuBox.Location = new System.Drawing.Point(138, 180);
            this.FmuBox.Name = "FmuBox";
            this.FmuBox.Size = new System.Drawing.Size(276, 21);
            this.FmuBox.TabIndex = 6;
            // 
            // FmuSelect
            // 
            this.FmuSelect.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FmuSelect.Location = new System.Drawing.Point(33, 216);
            this.FmuSelect.Name = "FmuSelect";
            this.FmuSelect.Size = new System.Drawing.Size(165, 41);
            this.FmuSelect.TabIndex = 11;
            this.FmuSelect.Text = "File Select";
            this.FmuSelect.UseVisualStyleBackColor = true;
            this.FmuSelect.Click += new System.EventHandler(this.button_File_Select);
            // 
            // SELECT
            // 
            this.SELECT.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SELECT.Location = new System.Drawing.Point(33, 278);
            this.SELECT.Name = "SELECT";
            this.SELECT.Size = new System.Drawing.Size(165, 41);
            this.SELECT.TabIndex = 12;
            this.SELECT.Text = "SELECT";
            this.SELECT.UseVisualStyleBackColor = true;
            this.SELECT.Click += new System.EventHandler(this.button_Fmu_Select);
            // 
            // Mysql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 408);
            this.Controls.Add(this.SELECT);
            this.Controls.Add(this.FmuSelect);
            this.Controls.Add(this.FmuInsert);
            this.Controls.Add(this.FmuLabel);
            this.Controls.Add(this.FmuBox);
            this.Controls.Add(this.UserInsert);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.pwdBox);
            this.Controls.Add(this.nameBox);
            this.Name = "Mysql";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox pwdBox;
        private System.Windows.Forms.Label PasswordText;
        private System.Windows.Forms.Button UserInsert;
        private System.Windows.Forms.Button FmuInsert;
        private System.Windows.Forms.Label FmuLabel;
        private System.Windows.Forms.TextBox FmuBox;
        private System.Windows.Forms.Button FmuSelect;
        private System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.Button SELECT;
    }
}
