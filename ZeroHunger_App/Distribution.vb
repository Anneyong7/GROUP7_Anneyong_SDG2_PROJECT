Imports System.Data.SqlClient

Public Class Distribution

    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"

    ' 1. Load the items when the form opens
    Private Sub DistributionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using conn As New SqlConnection(connectionString)
                ' Only show items that have stock and are not expired
                Dim query As String = "SELECT item_id, item_name, stock_quantity FROM FoodInventory WHERE stock_quantity > 0 AND expiration_date >= GETDATE()"
                Dim adapter As New SqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)

                ' Link the data to the drop-down menu
                cmbItems.DataSource = table
                cmbItems.DisplayMember = "item_name" ' What the user sees
                cmbItems.ValueMember = "item_id"     ' The hidden ID it sends to the database
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading items: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 2. Update the stock label when they pick a new item
    Private Sub cmbItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItems.SelectedIndexChanged
        If cmbItems.SelectedIndex <> -1 Then
            Dim row As DataRowView = DirectCast(cmbItems.SelectedItem, DataRowView)
            lblStock.Text = "Available Stock: " & row("stock_quantity").ToString()
        End If
    End Sub

    ' 3. Submit the distribution
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        If cmbItems.SelectedIndex = -1 Or txtBeneficiary.Text = "" Or txtQuantity.Text = "" Then
            MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Grab the hidden item_id from the ComboBox
        Dim selectedItemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)

        Dim service As New DistributionService()
        Dim result As String = service.ProcessDistribution(selectedItemId, txtBeneficiary.Text, Convert.ToInt32(txtQuantity.Text))

        If result = "Success" Then
            MessageBox.Show("Food distributed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBeneficiary.Clear()
            txtQuantity.Clear()
            DistributionForm_Load(Nothing, Nothing) ' Refresh the drop-down to show the new stock
        Else
            MessageBox.Show(result, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class