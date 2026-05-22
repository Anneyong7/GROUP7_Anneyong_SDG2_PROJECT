<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainDashboard
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
        Me.btnInventory = New System.Windows.Forms.Button()
        Me.btnDistribute = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnInventory
        '
        Me.btnInventory.Location = New System.Drawing.Point(67, 83)
        Me.btnInventory.Name = "btnInventory"
        Me.btnInventory.Size = New System.Drawing.Size(152, 42)
        Me.btnInventory.TabIndex = 0
        Me.btnInventory.Text = "Manage Inventory"
        Me.btnInventory.UseVisualStyleBackColor = True
        '
        'btnDistribute
        '
        Me.btnDistribute.Location = New System.Drawing.Point(67, 152)
        Me.btnDistribute.Name = "btnDistribute"
        Me.btnDistribute.Size = New System.Drawing.Size(152, 42)
        Me.btnDistribute.TabIndex = 1
        Me.btnDistribute.Text = "Distribute Food"
        Me.btnDistribute.UseVisualStyleBackColor = True
        '
        'btnReports
        '
        Me.btnReports.Location = New System.Drawing.Point(67, 224)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(152, 42)
        Me.btnReports.TabIndex = 2
        Me.btnReports.Text = "View reports"
        Me.btnReports.UseVisualStyleBackColor = True
        '
        'MainDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 329)
        Me.Controls.Add(Me.btnReports)
        Me.Controls.Add(Me.btnDistribute)
        Me.Controls.Add(Me.btnInventory)
        Me.Name = "MainDashboard"
        Me.Text = "MainDashboard"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnInventory As Button
    Friend WithEvents btnDistribute As Button
    Friend WithEvents btnReports As Button
End Class
