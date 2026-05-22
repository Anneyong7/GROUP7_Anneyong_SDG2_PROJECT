Imports System.Data.SqlClient

Public Class LoginForm
    ' Update this connection string if your LocalDB instance name is different
    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' 1. Basic Input Validation
        If txtUsername.Text = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Database Connection and Query
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' We query the role so we can use it later (Admin vs Standard User)
                Dim query As String = "SELECT user_role FROM Users WHERE username = @username AND password_hash = @password"

                Using cmd As New SqlCommand(query, conn)
                    ' Parameterized queries prevent SQL injection
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text)
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text)

                    ' ExecuteScalar returns the first column of the first row (the user_role)
                    Dim role As Object = cmd.ExecuteScalar()

                    If role IsNot Nothing Then
                        MessageBox.Show("Login successful! Role: " & role.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Open your Main Dashboard and pass the role over
                        Dim dashboard As New MainDashboard(role.ToString())
                        dashboard.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

        Catch ex As Exception
            ' 3. Error Handling
            MessageBox.Show("Database connection error: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
