namespace mp3Normalization
{
    partial class Form1
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLoadConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textFilePath = new System.Windows.Forms.TextBox();
            this.progressConvert = new System.Windows.Forms.ProgressBar();
            this.textView = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textGain = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.chkConverFilename = new System.Windows.Forms.CheckBox();
            this.chkUsingTempfile = new System.Windows.Forms.CheckBox();
            this.buttonSaveList = new System.Windows.Forms.Button();
            this.buttonLoadList = new System.Windows.Forms.Button();
            this.chkDisableMp3gain = new System.Windows.Forms.CheckBox();
            this.textTempDir = new System.Windows.Forms.TextBox();
            this.buttonTempDir = new System.Windows.Forms.Button();
            this.buttonAddFolders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLoadConvert
            // 
            this.buttonLoadConvert.Location = new System.Drawing.Point(31, 112);
            this.buttonLoadConvert.Name = "buttonLoadConvert";
            this.buttonLoadConvert.Size = new System.Drawing.Size(94, 23);
            this.buttonLoadConvert.TabIndex = 0;
            this.buttonLoadConvert.Text = "loadConvert";
            this.buttonLoadConvert.UseVisualStyleBackColor = true;
            this.buttonLoadConvert.Click += new System.EventHandler(this.buttonLoadConvert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "File path";
            // 
            // textFilePath
            // 
            this.textFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilePath.Location = new System.Drawing.Point(131, 54);
            this.textFilePath.Name = "textFilePath";
            this.textFilePath.Size = new System.Drawing.Size(496, 21);
            this.textFilePath.TabIndex = 2;
            // 
            // progressConvert
            // 
            this.progressConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressConvert.Location = new System.Drawing.Point(131, 110);
            this.progressConvert.Name = "progressConvert";
            this.progressConvert.Size = new System.Drawing.Size(496, 23);
            this.progressConvert.TabIndex = 3;
            // 
            // textView
            // 
            this.textView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textView.Location = new System.Drawing.Point(131, 142);
            this.textView.Multiline = true;
            this.textView.Name = "textView";
            this.textView.Size = new System.Drawing.Size(496, 114);
            this.textView.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Target Gain";
            // 
            // textGain
            // 
            this.textGain.Location = new System.Drawing.Point(131, 23);
            this.textGain.Name = "textGain";
            this.textGain.Size = new System.Drawing.Size(100, 21);
            this.textGain.TabIndex = 6;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(131, 279);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(496, 236);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Location = new System.Drawing.Point(31, 142);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(94, 23);
            this.buttonAddFiles.TabIndex = 8;
            this.buttonAddFiles.Text = "addFiles";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // chkConverFilename
            // 
            this.chkConverFilename.AutoSize = true;
            this.chkConverFilename.Location = new System.Drawing.Point(252, 25);
            this.chkConverFilename.Name = "chkConverFilename";
            this.chkConverFilename.Size = new System.Drawing.Size(121, 16);
            this.chkConverFilename.TabIndex = 9;
            this.chkConverFilename.Text = "convert file name";
            this.chkConverFilename.UseVisualStyleBackColor = true;
            // 
            // chkUsingTempfile
            // 
            this.chkUsingTempfile.AutoSize = true;
            this.chkUsingTempfile.Location = new System.Drawing.Point(379, 25);
            this.chkUsingTempfile.Name = "chkUsingTempfile";
            this.chkUsingTempfile.Size = new System.Drawing.Size(107, 16);
            this.chkUsingTempfile.TabIndex = 10;
            this.chkUsingTempfile.Text = "using temp file";
            this.chkUsingTempfile.UseVisualStyleBackColor = true;
            // 
            // buttonSaveList
            // 
            this.buttonSaveList.Location = new System.Drawing.Point(31, 200);
            this.buttonSaveList.Name = "buttonSaveList";
            this.buttonSaveList.Size = new System.Drawing.Size(94, 23);
            this.buttonSaveList.TabIndex = 11;
            this.buttonSaveList.Text = "saveList";
            this.buttonSaveList.UseVisualStyleBackColor = true;
            this.buttonSaveList.Click += new System.EventHandler(this.buttonSaveList_Click);
            // 
            // buttonLoadList
            // 
            this.buttonLoadList.Location = new System.Drawing.Point(31, 233);
            this.buttonLoadList.Name = "buttonLoadList";
            this.buttonLoadList.Size = new System.Drawing.Size(94, 23);
            this.buttonLoadList.TabIndex = 12;
            this.buttonLoadList.Text = "loadList";
            this.buttonLoadList.UseVisualStyleBackColor = true;
            this.buttonLoadList.Click += new System.EventHandler(this.buttonLoadList_Click);
            // 
            // chkDisableMp3gain
            // 
            this.chkDisableMp3gain.AutoSize = true;
            this.chkDisableMp3gain.Location = new System.Drawing.Point(501, 24);
            this.chkDisableMp3gain.Name = "chkDisableMp3gain";
            this.chkDisableMp3gain.Size = new System.Drawing.Size(129, 16);
            this.chkDisableMp3gain.TabIndex = 13;
            this.chkDisableMp3gain.Text = "don\'t use mp3gain";
            this.chkDisableMp3gain.UseVisualStyleBackColor = true;
            // 
            // textTempDir
            // 
            this.textTempDir.Location = new System.Drawing.Point(131, 83);
            this.textTempDir.Name = "textTempDir";
            this.textTempDir.Size = new System.Drawing.Size(496, 21);
            this.textTempDir.TabIndex = 15;
            // 
            // buttonTempDir
            // 
            this.buttonTempDir.Location = new System.Drawing.Point(31, 83);
            this.buttonTempDir.Name = "buttonTempDir";
            this.buttonTempDir.Size = new System.Drawing.Size(94, 23);
            this.buttonTempDir.TabIndex = 16;
            this.buttonTempDir.Text = "Temp dir.";
            this.buttonTempDir.UseVisualStyleBackColor = true;
            this.buttonTempDir.Click += new System.EventHandler(this.buttonTempDir_Click);
            // 
            // buttonAddFolders
            // 
            this.buttonAddFolders.Location = new System.Drawing.Point(31, 171);
            this.buttonAddFolders.Name = "buttonAddFolders";
            this.buttonAddFolders.Size = new System.Drawing.Size(94, 23);
            this.buttonAddFolders.TabIndex = 17;
            this.buttonAddFolders.Text = "addFolders";
            this.buttonAddFolders.UseVisualStyleBackColor = true;
            this.buttonAddFolders.Click += new System.EventHandler(this.buttonAddFolders_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 665);
            this.Controls.Add(this.buttonAddFolders);
            this.Controls.Add(this.buttonTempDir);
            this.Controls.Add(this.textTempDir);
            this.Controls.Add(this.chkDisableMp3gain);
            this.Controls.Add(this.buttonLoadList);
            this.Controls.Add(this.buttonSaveList);
            this.Controls.Add(this.chkUsingTempfile);
            this.Controls.Add(this.chkConverFilename);
            this.Controls.Add(this.buttonAddFiles);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textGain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textView);
            this.Controls.Add(this.progressConvert);
            this.Controls.Add(this.textFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLoadConvert);
            this.Name = "Form1";
            this.Text = "mp3Normalization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonLoadConvert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFilePath;
        private System.Windows.Forms.ProgressBar progressConvert;
        private System.Windows.Forms.TextBox textView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textGain;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.CheckBox chkConverFilename;
        private System.Windows.Forms.CheckBox chkUsingTempfile;
        private System.Windows.Forms.Button buttonSaveList;
        private System.Windows.Forms.Button buttonLoadList;
        private System.Windows.Forms.CheckBox chkDisableMp3gain;
        private System.Windows.Forms.TextBox textTempDir;
        private System.Windows.Forms.Button buttonTempDir;
        private System.Windows.Forms.Button buttonAddFolders;
    }
}

