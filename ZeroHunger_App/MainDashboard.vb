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

        ' If they are not an Admin, hide the restricted buttons
        If currentUserRole <> "Admin" Then
            btnInventory.Visible = False
            btnReports.Visible = False
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
End Class