namespace LambdaGUI
{
    partial class CodeEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.fileTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.codeWindow = new ICSharpCode.TextEditor.TextEditorControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.resultWindow = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.debugTab = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.AstView = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.fileTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.layout.SuspendLayout();
            this.debugTab.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1174, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuFileNew.Size = new System.Drawing.Size(146, 22);
            this.menuFileNew.Text = "&New";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpen.Size = new System.Drawing.Size(146, 22);
            this.menuFileOpen.Text = "&Open";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Close";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 100);
            this.tabPage2.TabIndex = 0;
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.Location = new System.Drawing.Point(3, 3);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(75, 21);
            this.btnEvaluate.TabIndex = 6;
            this.btnEvaluate.Text = "evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
            // 
            // fileTabs
            // 
            this.fileTabs.Controls.Add(this.tabPage1);
            this.fileTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTabs.Location = new System.Drawing.Point(3, 38);
            this.fileTabs.Name = "fileTabs";
            this.fileTabs.SelectedIndex = 0;
            this.fileTabs.Size = new System.Drawing.Size(874, 341);
            this.fileTabs.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.codeWindow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(866, 315);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NewFile";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // codeWindow
            // 
            this.codeWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeWindow.IsReadOnly = false;
            this.codeWindow.Location = new System.Drawing.Point(3, 3);
            this.codeWindow.Name = "codeWindow";
            this.codeWindow.Size = new System.Drawing.Size(860, 309);
            this.codeWindow.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 521);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(874, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 505);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Direct Window:";
            // 
            // resultWindow
            // 
            this.resultWindow.BackColor = System.Drawing.Color.Black;
            this.resultWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultWindow.ForeColor = System.Drawing.Color.White;
            this.resultWindow.Location = new System.Drawing.Point(3, 398);
            this.resultWindow.Name = "resultWindow";
            this.resultWindow.ReadOnly = true;
            this.resultWindow.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.resultWindow.Size = new System.Drawing.Size(874, 104);
            this.resultWindow.TabIndex = 3;
            this.resultWindow.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Result:";
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layout.Controls.Add(this.label2, 0, 2);
            this.layout.Controls.Add(this.resultWindow, 0, 3);
            this.layout.Controls.Add(this.label3, 0, 4);
            this.layout.Controls.Add(this.textBox1, 0, 5);
            this.layout.Controls.Add(this.fileTabs, 0, 1);
            this.layout.Controls.Add(this.btnEvaluate, 0, 0);
            this.layout.Controls.Add(this.debugTab, 1, 1);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 24);
            this.layout.Name = "layout";
            this.layout.RowCount = 7;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.246571F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.75343F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Size = new System.Drawing.Size(1174, 559);
            this.layout.TabIndex = 2;
            // 
            // debugTab
            // 
            this.debugTab.Controls.Add(this.tabPage3);
            this.debugTab.Controls.Add(this.tabPage4);
            this.debugTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugTab.Location = new System.Drawing.Point(883, 38);
            this.debugTab.Name = "debugTab";
            this.debugTab.SelectedIndex = 0;
            this.debugTab.Size = new System.Drawing.Size(288, 341);
            this.debugTab.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.AstView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(280, 315);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "current AST";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // AstView
            // 
            this.AstView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AstView.Location = new System.Drawing.Point(3, 3);
            this.AstView.Name = "AstView";
            this.AstView.Size = new System.Drawing.Size(274, 309);
            this.AstView.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(280, 315);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Enviorment";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.evaluateToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // evaluateToolStripMenuItem
            // 
            this.evaluateToolStripMenuItem.Name = "evaluateToolStripMenuItem";
            this.evaluateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.evaluateToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.evaluateToolStripMenuItem.Text = "&evaluate";
            this.evaluateToolStripMenuItem.Click += new System.EventHandler(this.evaluateToolStripMenuItem_Click);
            // 
            // CodeEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 583);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CodeEditorForm";
            this.Text = " λ -  The ultimate Interpreter ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.fileTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.debugTab.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.TabControl fileTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private ICSharpCode.TextEditor.TextEditorControl codeWindow;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox resultWindow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.TabControl debugTab;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView AstView;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evaluateToolStripMenuItem;
    }
}