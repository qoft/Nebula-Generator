partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
        this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
        this.gunaLineTextBox1 = new Guna.UI.WinForms.GunaLineTextBox();
        this.metroLabel1 = new MetroSuite.MetroLabel();
        this.metroLabel2 = new MetroSuite.MetroLabel();
        this.gunaLineTextBox2 = new Guna.UI.WinForms.GunaLineTextBox();
        this.gunaButton4 = new Guna.UI.WinForms.GunaButton();
        this.timer1 = new System.Windows.Forms.Timer(this.components);
        this.SuspendLayout();
        // 
        // gunaControlBox2
        // 
        this.gunaControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.gunaControlBox2.Animated = true;
        this.gunaControlBox2.AnimationHoverSpeed = 0.07F;
        this.gunaControlBox2.AnimationSpeed = 0.03F;
        this.gunaControlBox2.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
        this.gunaControlBox2.IconColor = System.Drawing.Color.White;
        this.gunaControlBox2.IconSize = 15F;
        this.gunaControlBox2.Location = new System.Drawing.Point(314, 7);
        this.gunaControlBox2.Name = "gunaControlBox2";
        this.gunaControlBox2.OnHoverBackColor = System.Drawing.Color.DarkRed;
        this.gunaControlBox2.OnHoverIconColor = System.Drawing.Color.White;
        this.gunaControlBox2.OnPressedColor = System.Drawing.Color.Black;
        this.gunaControlBox2.Size = new System.Drawing.Size(45, 29);
        this.gunaControlBox2.TabIndex = 25;
        // 
        // gunaControlBox1
        // 
        this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.gunaControlBox1.Animated = true;
        this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
        this.gunaControlBox1.AnimationSpeed = 0.03F;
        this.gunaControlBox1.IconColor = System.Drawing.Color.White;
        this.gunaControlBox1.IconSize = 15F;
        this.gunaControlBox1.Location = new System.Drawing.Point(365, 7);
        this.gunaControlBox1.Name = "gunaControlBox1";
        this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.DarkRed;
        this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
        this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
        this.gunaControlBox1.Size = new System.Drawing.Size(45, 29);
        this.gunaControlBox1.TabIndex = 24;
        // 
        // gunaLineTextBox1
        // 
        this.gunaLineTextBox1.Animated = true;
        this.gunaLineTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.gunaLineTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.gunaLineTextBox1.FocusedLineColor = System.Drawing.Color.Red;
        this.gunaLineTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.gunaLineTextBox1.LineColor = System.Drawing.Color.Gainsboro;
        this.gunaLineTextBox1.LineSize = 1;
        this.gunaLineTextBox1.Location = new System.Drawing.Point(30, 75);
        this.gunaLineTextBox1.MaxLength = 24;
        this.gunaLineTextBox1.Name = "gunaLineTextBox1";
        this.gunaLineTextBox1.PasswordChar = '\0';
        this.gunaLineTextBox1.Size = new System.Drawing.Size(352, 26);
        this.gunaLineTextBox1.TabIndex = 26;
        // 
        // metroLabel1
        // 
        this.metroLabel1.AutoSize = true;
        this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
        this.metroLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.metroLabel1.Location = new System.Drawing.Point(27, 51);
        this.metroLabel1.Name = "metroLabel1";
        this.metroLabel1.Size = new System.Drawing.Size(93, 15);
        this.metroLabel1.TabIndex = 27;
        this.metroLabel1.Text = "Your username:";
        // 
        // metroLabel2
        // 
        this.metroLabel2.AutoSize = true;
        this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
        this.metroLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.metroLabel2.Location = new System.Drawing.Point(27, 112);
        this.metroLabel2.Name = "metroLabel2";
        this.metroLabel2.Size = new System.Drawing.Size(90, 15);
        this.metroLabel2.TabIndex = 29;
        this.metroLabel2.Text = "Your password:";
        // 
        // gunaLineTextBox2
        // 
        this.gunaLineTextBox2.Animated = true;
        this.gunaLineTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.gunaLineTextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.gunaLineTextBox2.FocusedLineColor = System.Drawing.Color.Red;
        this.gunaLineTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.gunaLineTextBox2.LineColor = System.Drawing.Color.Gainsboro;
        this.gunaLineTextBox2.LineSize = 1;
        this.gunaLineTextBox2.Location = new System.Drawing.Point(30, 136);
        this.gunaLineTextBox2.MaxLength = 80;
        this.gunaLineTextBox2.Name = "gunaLineTextBox2";
        this.gunaLineTextBox2.PasswordChar = '●';
        this.gunaLineTextBox2.Size = new System.Drawing.Size(352, 26);
        this.gunaLineTextBox2.TabIndex = 28;
        this.gunaLineTextBox2.UseSystemPasswordChar = true;
        // 
        // gunaButton4
        // 
        this.gunaButton4.Animated = true;
        this.gunaButton4.AnimationHoverSpeed = 0.07F;
        this.gunaButton4.AnimationSpeed = 0.03F;
        this.gunaButton4.BackColor = System.Drawing.Color.Transparent;
        this.gunaButton4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
        this.gunaButton4.BorderColor = System.Drawing.Color.Transparent;
        this.gunaButton4.DialogResult = System.Windows.Forms.DialogResult.None;
        this.gunaButton4.FocusedColor = System.Drawing.Color.Empty;
        this.gunaButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.gunaButton4.ForeColor = System.Drawing.Color.White;
        this.gunaButton4.Image = null;
        this.gunaButton4.ImageSize = new System.Drawing.Size(24, 24);
        this.gunaButton4.Location = new System.Drawing.Point(30, 174);
        this.gunaButton4.Name = "gunaButton4";
        this.gunaButton4.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
        this.gunaButton4.OnHoverBorderColor = System.Drawing.Color.Black;
        this.gunaButton4.OnHoverForeColor = System.Drawing.Color.White;
        this.gunaButton4.OnHoverImage = null;
        this.gunaButton4.OnPressedColor = System.Drawing.Color.Black;
        this.gunaButton4.Size = new System.Drawing.Size(352, 42);
        this.gunaButton4.TabIndex = 32;
        this.gunaButton4.Text = "Login now";
        this.gunaButton4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.gunaButton4.Click += new System.EventHandler(this.gunaButton4_Click);
        // 
        // timer1
        // 
        this.timer1.Interval = 1000;
        this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        // 
        // vapp
        // 
        this.AccentColor = System.Drawing.Color.Red;
        this.AllowResize = false;
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.ClientSize = new System.Drawing.Size(417, 247);
        this.Controls.Add(this.gunaButton4);
        this.Controls.Add(this.metroLabel2);
        this.Controls.Add(this.gunaLineTextBox2);
        this.Controls.Add(this.metroLabel1);
        this.Controls.Add(this.gunaLineTextBox1);
        this.Controls.Add(this.gunaControlBox2);
        this.Controls.Add(this.gunaControlBox1);
        this.ForeColor = System.Drawing.SystemColors.ControlDark;
        this.Name = "LoginForm";
        this.ShowIcon = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.State = MetroSuite.MetroForm.FormState.Custom;
        this.Style = MetroSuite.Design.Style.Dark;
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.vapp_FormClosing);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
    private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
    private Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox1;
    private MetroSuite.MetroLabel metroLabel1;
    private MetroSuite.MetroLabel metroLabel2;
    private Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox2;
    private Guna.UI.WinForms.GunaButton gunaButton4;
    private System.Windows.Forms.Timer timer1;
}