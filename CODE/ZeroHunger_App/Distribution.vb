Imports System.Data.SqlClient

Public Class Distribution

    ' 1. Database Connection String
    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"

    ' 2. Form Activated Event (Refreshes data every time you open or return to this screen)
    Private Sub DistributionForm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        LoadInventoryItems()
    End Sub

    ' 3. Method to Load Available Stock into the ComboBox
    Private Sub LoadInventoryItems()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT item_id, item_name + ' (Qty: ' + CAST(stock_quantity AS VARCHAR) + ')' AS display_info FROM Inventory WHERE stock_quantity > 0"
                Dim adapter As New SqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)

                ' Safely reset and fill the ComboBox
                cmbItems.DataSource = Nothing
                cmbItems.DisplayMember = "display_info"
                cmbItems.ValueMember = "item_id"
                cmbItems.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading items: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 4. The Save Button (Processes the distribution)
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            ' Validation: Check if everything is filled out
            If cmbItems.SelectedValue Is Nothing OrElse String.IsNullOrWhiteSpace(txtBeneficiary.Text) OrElse String.IsNullOrWhiteSpace(txtQuantity.Text) Then
                MessageBox.Show("Please fill in all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedItemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
            Dim qtyToGive As Integer = Convert.ToInt32(txtQuantity.Text)
            Dim beneficiary As String = txtBeneficiary.Text

            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' JOB A: Check if we have enough stock left in the database
                Dim checkQuery As String = "SELECT stock_quantity FROM Inventory WHERE item_id = @id"
                Dim currentStock As Integer = 0
                Using checkCmd As New SqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@id", selectedItemId)
                    currentStock = Convert.ToInt32(checkCmd.ExecuteScalar())
                End Using

                If qtyToGive > currentStock Then
                    MessageBox.Show("Not enough stock! We only have " & currentStock & " left.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' JOB B: Save the record to the Distributions table using the ITEM NAME
                Dim itemName As String = cmbItems.Text.Split("("c)(0).Trim() ' Cleans up the name

                Dim distQuery As String = "INSERT INTO Distributions (item_name, beneficiary_name, quantity_given) VALUES (@name, @beneficiary, @qty)"
                Using distCmd As New SqlCommand(distQuery, conn)
                    distCmd.Parameters.AddWithValue("@name", itemName)
                    distCmd.Parameters.AddWithValue("@beneficiary", beneficiary)
                    distCmd.Parameters.AddWithValue("@qty", qtyToGive)
                    distCmd.ExecuteNonQuery()
                End Using

                ' JOB C: Subtract the given amount from the Inventory table
                Dim updateQuery As String = "UPDATE Inventory SET stock_quantity = stock_quantity - @qty WHERE item_id = @id"
                Using updateCmd As New SqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@qty", qtyToGive)
                    updateCmd.Parameters.AddWithValue("@id", selectedItemId)
                    updateCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Distribution saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

            ' Clear textboxes and instantly refresh the dropdown list
            txtBeneficiary.Clear()
            txtQuantity.Clear()
            LoadInventoryItems()

        Catch ex As Exception
            MessageBox.Show("Error processing distribution: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 5. The Back Button
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' Closes the window and reveals the dashboard behind it
        Me.Close()
    End Sub

    Private Sub lblStock_Click(sender As Object, e As EventArgs) Handles lblStock.Click

    End Sub
End Class