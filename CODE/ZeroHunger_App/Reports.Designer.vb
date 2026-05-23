<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Reports
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reports))
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnDonationReport = New System.Windows.Forms.Button()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.SuspendLayout()
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Location = New System.Drawing.Point(95, 103)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(165, 50)
        Me.btnPreview.TabIndex = 0
        Me.btnPreview.Text = "Generate Expiring" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Food Report"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(50, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(257, 47)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "View Reports"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(129, 248)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(87, 29)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnDonationReport
        '
        Me.btnDonationReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDonationReport.Location = New System.Drawing.Point(95, 159)
        Me.btnDonationReport.Name = "btnDonationReport"
        Me.btnDonationReport.Size = New System.Drawing.Size(165, 50)
        Me.btnDonationReport.TabIndex = 3
        Me.btnDonationReport.Text = "Generate Donation Logs"
        Me.btnDonationReport.UseVisualStyleBackColor = True
        '
        'PrintDocument2
        '
        '
        'Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 289)
        Me.Controls.Add(Me.btnDonationReport)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnPreview)
        Me.Name = "Reports"
        Me.Text = "Reports"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnPreview As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents Label1 As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents btnDonationReport As Button
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
End Class
