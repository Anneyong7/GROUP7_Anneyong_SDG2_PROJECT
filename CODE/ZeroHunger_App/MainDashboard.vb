Public Class MainDashboard

    ' Variable to store the role of the person logged in
    Dim currentUserRole As String

    ' This is the part that fixes your error. It catches the role sent from LoginForm.
    Public Sub New(role As String)
        InitializeComponent()
        currentUserRole = role
    End Sub

    ' This runs exactly when the dashboard opens
    Private Sub MainDashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If currentUserRole = "Admin" Then
            ' Admin sees everything
            btnInventory.Visible = True
            btnDistribute.Visible = True
            btnReports.Visible = True

        ElseIf currentUserRole = "staff" Then
            ' Staff restrictions
            btnInventory.Visible = False
            btnDistribute.Visible = True
            btnReports.Visible = True
        End If
    End Sub

    Private Sub btnDistribute_Click(sender As Object, e As EventArgs) Handles btnDistribute.Click
        Dim distForm As New Distribution()
        distForm.ShowDialog()
    End Sub

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        Dim invForm As New Inventory()
        invForm.ShowDialog()
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        Dim invForm As New Reports()
        invForm.ShowDialog()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Show a polite message
        MessageBox.Show("You have been logged out.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Restart the application to show a fresh Login screen
        Application.Restart()
    End Sub
End Class