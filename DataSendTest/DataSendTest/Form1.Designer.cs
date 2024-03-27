namespace DataSendTest
{
    partial class FormTest
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbx_Num1 = new System.Windows.Forms.TextBox();
            this.lbl_SendTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_Reciver = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.tbx_Num2 = new System.Windows.Forms.TextBox();
            this.lbl_Num1 = new System.Windows.Forms.Label();
            this.lbl_Num2 = new System.Windows.Forms.Label();
            this.tbx_String = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_SelectOne = new System.Windows.Forms.ComboBox();
            this.lbl_ReciveFirst = new System.Windows.Forms.Label();
            this.tm_Tcount = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_DelayInput = new System.Windows.Forms.TextBox();
            this.lbl_DelaySecond = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbx_Num1
            // 
            this.tbx_Num1.Location = new System.Drawing.Point(251, 38);
            this.tbx_Num1.Name = "tbx_Num1";
            this.tbx_Num1.Size = new System.Drawing.Size(118, 22);
            this.tbx_Num1.TabIndex = 0;
            this.tbx_Num1.Visible = false;
            // 
            // lbl_SendTitle
            // 
            this.lbl_SendTitle.AutoSize = true;
            this.lbl_SendTitle.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SendTitle.Location = new System.Drawing.Point(27, 27);
            this.lbl_SendTitle.Name = "lbl_SendTitle";
            this.lbl_SendTitle.Size = new System.Drawing.Size(97, 35);
            this.lbl_SendTitle.TabIndex = 1;
            this.lbl_SendTitle.Text = "傳遞:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(505, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "接收數值(次數)";
            this.label2.Visible = false;
            // 
            // tbx_Reciver
            // 
            this.tbx_Reciver.Location = new System.Drawing.Point(544, 38);
            this.tbx_Reciver.Name = "tbx_Reciver";
            this.tbx_Reciver.Size = new System.Drawing.Size(74, 22);
            this.tbx_Reciver.TabIndex = 2;
            this.tbx_Reciver.Visible = false;
            // 
            // btn_Send
            // 
            this.btn_Send.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Send.Location = new System.Drawing.Point(472, 106);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(110, 73);
            this.btn_Send.TabIndex = 4;
            this.btn_Send.Text = "Send(傳)";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // tbx_Num2
            // 
            this.tbx_Num2.Location = new System.Drawing.Point(386, 37);
            this.tbx_Num2.Name = "tbx_Num2";
            this.tbx_Num2.Size = new System.Drawing.Size(118, 22);
            this.tbx_Num2.TabIndex = 6;
            this.tbx_Num2.Visible = false;
            // 
            // lbl_Num1
            // 
            this.lbl_Num1.AutoSize = true;
            this.lbl_Num1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_Num1.Location = new System.Drawing.Point(285, 19);
            this.lbl_Num1.Name = "lbl_Num1";
            this.lbl_Num1.Size = new System.Drawing.Size(51, 16);
            this.lbl_Num1.TabIndex = 7;
            this.lbl_Num1.Text = "數字1";
            this.lbl_Num1.Visible = false;
            // 
            // lbl_Num2
            // 
            this.lbl_Num2.AutoSize = true;
            this.lbl_Num2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_Num2.Location = new System.Drawing.Point(415, 18);
            this.lbl_Num2.Name = "lbl_Num2";
            this.lbl_Num2.Size = new System.Drawing.Size(51, 16);
            this.lbl_Num2.TabIndex = 8;
            this.lbl_Num2.Text = "數字2";
            this.lbl_Num2.Visible = false;
            // 
            // tbx_String
            // 
            this.tbx_String.Location = new System.Drawing.Point(33, 106);
            this.tbx_String.Multiline = true;
            this.tbx_String.Name = "tbx_String";
            this.tbx_String.Size = new System.Drawing.Size(407, 135);
            this.tbx_String.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(202, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "字串";
            // 
            // cbx_SelectOne
            // 
            this.cbx_SelectOne.FormattingEnabled = true;
            this.cbx_SelectOne.Location = new System.Drawing.Point(129, 36);
            this.cbx_SelectOne.Name = "cbx_SelectOne";
            this.cbx_SelectOne.Size = new System.Drawing.Size(60, 20);
            this.cbx_SelectOne.TabIndex = 11;
            this.cbx_SelectOne.Visible = false;
            // 
            // lbl_ReciveFirst
            // 
            this.lbl_ReciveFirst.AutoSize = true;
            this.lbl_ReciveFirst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_ReciveFirst.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_ReciveFirst.Location = new System.Drawing.Point(468, 211);
            this.lbl_ReciveFirst.Name = "lbl_ReciveFirst";
            this.lbl_ReciveFirst.Size = new System.Drawing.Size(121, 21);
            this.lbl_ReciveFirst.TabIndex = 12;
            this.lbl_ReciveFirst.Text = "等待收MFC";
            // 
            // tm_Tcount
            // 
            this.tm_Tcount.Tick += new System.EventHandler(this.tm_Tcount_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(626, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "--";
            // 
            // tbx_DelayInput
            // 
            this.tbx_DelayInput.Location = new System.Drawing.Point(613, 210);
            this.tbx_DelayInput.Name = "tbx_DelayInput";
            this.tbx_DelayInput.Size = new System.Drawing.Size(38, 22);
            this.tbx_DelayInput.TabIndex = 14;
            // 
            // lbl_DelaySecond
            // 
            this.lbl_DelaySecond.AutoSize = true;
            this.lbl_DelaySecond.Location = new System.Drawing.Point(608, 194);
            this.lbl_DelaySecond.Name = "lbl_DelaySecond";
            this.lbl_DelaySecond.Size = new System.Drawing.Size(49, 12);
            this.lbl_DelaySecond.TabIndex = 15;
            this.lbl_DelaySecond.Text = "延遲(秒)";
            // 
            // FormTest
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(674, 253);
            this.Controls.Add(this.lbl_DelaySecond);
            this.Controls.Add(this.tbx_DelayInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_ReciveFirst);
            this.Controls.Add(this.cbx_SelectOne);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_String);
            this.Controls.Add(this.lbl_Num2);
            this.Controls.Add(this.lbl_Num1);
            this.Controls.Add(this.tbx_Num2);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbx_Reciver);
            this.Controls.Add(this.lbl_SendTitle);
            this.Controls.Add(this.tbx_Num1);
            this.Name = "FormTest";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C#FORM";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbx_Num1;
        private System.Windows.Forms.Label lbl_SendTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_Reciver;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox tbx_Num2;
        private System.Windows.Forms.Label lbl_Num1;
        private System.Windows.Forms.Label lbl_Num2;
        private System.Windows.Forms.TextBox tbx_String;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_SelectOne;
        private System.Windows.Forms.Label lbl_ReciveFirst;
        private System.Windows.Forms.Timer tm_Tcount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_DelayInput;
        private System.Windows.Forms.Label lbl_DelaySecond;
    }
}

