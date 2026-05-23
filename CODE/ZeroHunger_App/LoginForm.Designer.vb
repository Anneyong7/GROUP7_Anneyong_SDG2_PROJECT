<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(187, 94)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(100, 22)
        Me.txtUsername.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(187, 156)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(100, 22)
        Me.txtPassword.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(108, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Username:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(111, 162)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Password:"
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(212, 213)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(75, 23)
        Me.btnLogin.TabIndex = 4
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(51, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(355, 47)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "SDG2: ZeroHunger"
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 281)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Name = "LoginForm"
        Me.Text = "SDG2: ZeroHunger App"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnLogin As Button
    Friend WithEvents Label3 As Label
End Class
