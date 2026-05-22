Imports System.Data.SqlClient

Public Class Inventory

    ' Connection string to local SQL Server database
    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"

    ' The Refresh Tool (This clears and reloads DataGridView)
    Private Sub RefreshInventoryGrid()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM Inventory"
                Dim adapter As New SqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                dgvInventory.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    ' 1. LOAD DATA (Runs when form opens or refreshes)
    Private Sub LoadData()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim adapter As New SqlDataAdapter("SELECT * FROM Inventory", conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                dgvInventory.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearFields()
        txtItemName.Clear()
        txtCategory.Clear()
        txtStock.Clear()
        dtpExpiration.Value = Date.Now ' Resets the date to today
    End Sub

    Private Sub InventoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    ' 2. ADD BUTTON (Create new item)
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Validate stock is an integer
            Dim qty As Integer
            If Not Integer.TryParse(txtStock.Text, qty) Then
                MessageBox.Show("Please enter a valid numeric stock quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' JOB 1: Add or Update the Inventory Table
                Dim invQuery As String = "INSERT INTO Inventory (item_name, stock_quantity, expiration_date, category) VALUES (@item, @qty, @expDate, @category)"
                Using invCmd As New SqlCommand(invQuery, conn)
                    invCmd.Parameters.AddWithValue("@item", txtItemName.Text)
                    invCmd.Parameters.AddWithValue("@qty", qty)
                    invCmd.Parameters.AddWithValue("@expDate", dtpExpiration.Value)
                    invCmd.Parameters.AddWithValue("@category", txtCategory.Text)
                    invCmd.ExecuteNonQuery()
                End Using

                ' JOB 2: Save the History to Donation Logs
                Dim logQuery As String = "INSERT INTO DonationLogs (item_name, quantity_donated, donor_name, date_received) VALUES (@item, @qty, @donor, GETDATE())"
                Using logCmd As New SqlCommand(logQuery, conn)
                    logCmd.Parameters.AddWithValue("@item", txtItemName.Text)
                    logCmd.Parameters.AddWithValue("@qty", qty)

                    ' If the textbox is empty, save it as "Anonymous"
                    Dim donor As String = If(String.IsNullOrWhiteSpace(txtDonorName.Text), "Anonymous", txtDonorName.Text)
                    logCmd.Parameters.AddWithValue("@donor", donor)

                    logCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Item added to inventory and logged successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

            ' Refresh UI and clear fields
            LoadData()
            ClearFields()
            txtDonorName.Clear()

        Catch ex As Exception
            MessageBox.Show("Error saving data: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 3. UPDATE BUTTON
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If dgvInventory.SelectedRows.Count > 0 Then
                Dim selectedId As Integer = Convert.ToInt32(dgvInventory.SelectedRows(0).Cells("item_id").Value)

                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Dim query As String = "UPDATE Inventory SET item_name = @name, category = @category, expiration_date = @date, stock_quantity = @stock WHERE item_id = @id"
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@name", txtItemName.Text)
                        cmd.Parameters.AddWithValue("@category", txtCategory.Text)
                        cmd.Parameters.AddWithValue("@date", dtpExpiration.Value)
                        cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text))
                        cmd.Parameters.AddWithValue("@id", selectedId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData() ' Refresh the table
                ClearFields()
            Else
                MessageBox.Show("Please select a row to update.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating item: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 4. DELETE BUTTON (Remove item)
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Make sure an item is selected in the DataGridView
        If dgvInventory.SelectedRows.Count > 0 Then
            Dim selectedId As Integer = Convert.ToInt32(dgvInventory.SelectedRows(0).Cells("item_id").Value)

            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Dim deleteQuery As String = "DELETE FROM Inventory WHERE item_id = @id"
                    Using cmd As New SqlCommand(deleteQuery, conn)
                        cmd.Parameters.AddWithValue("@id", selectedId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Item successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RefreshInventoryGrid() ' Refresh the screen

            Catch ex As Exception
                MessageBox.Show("Error deleting item: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' 5. SEARCH BUTTON (Filter by name)
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM Inventory WHERE item_name LIKE @search"
                Dim adapter As New SqlDataAdapter(query, conn)

                ' The % signs allow partial matches
                adapter.SelectCommand.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")

                Dim table As New DataTable()
                adapter.Fill(table)
                dgvInventory.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching data: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvInventory_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInventory.CellContentClick
        Try
            ' Make sure they clicked a real row, not the header
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = dgvInventory.Rows(e.RowIndex)

                txtItemName.Text = row.Cells("item_name").Value.ToString()
                txtCategory.Text = row.Cells("category").Value.ToString()
                dtpExpiration.Value = Convert.ToDateTime(row.Cells("expiration_date").Value)
                txtStock.Text = row.Cells("stock_quantity").Value.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting item: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub txtItemName_TextChanged(sender As Object, e As EventArgs) Handles txtItemName.TextChanged

    End Sub
End Class