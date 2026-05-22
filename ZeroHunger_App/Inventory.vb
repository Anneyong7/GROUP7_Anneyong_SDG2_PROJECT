Imports System.Data.SqlClient

Public Class Inventory

    ' Connection string to your local SQL Server database
    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"

    ' 1. LOAD DATA (Runs when form opens or refreshes)
    Private Sub LoadData()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim adapter As New SqlDataAdapter("SELECT * FROM FoodInventory", conn)
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
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "INSERT INTO FoodInventory (item_name, category, expiration_date, stock_quantity) VALUES (@name, @category, @date, @stock)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", txtItemName.Text)
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text)
                    cmd.Parameters.AddWithValue("@date", dtpExpiration.Value)
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData() ' Refresh the table
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error adding item: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 3. UPDATE BUTTON (Modify existing item)
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If dgvInventory.SelectedRows.Count > 0 Then
                Dim selectedId As Integer = Convert.ToInt32(dgvInventory.SelectedRows(0).Cells("item_id").Value)

                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Dim query As String = "UPDATE FoodInventory SET item_name = @name, category = @category, expiration_date = @date, stock_quantity = @stock WHERE item_id = @id"
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
        Try
            If dgvInventory.SelectedRows.Count > 0 Then
                Dim selectedId As Integer = Convert.ToInt32(dgvInventory.SelectedRows(0).Cells("item_id").Value)

                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Dim query As String = "DELETE FROM FoodInventory WHERE item_id = @id"
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", selectedId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData() ' Refresh the table
            Else
                MessageBox.Show("Please select a row to delete.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting item: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 5. SEARCH BUTTON (Filter by name)
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM FoodInventory WHERE item_name LIKE @search"
                Dim adapter As New SqlDataAdapter(query, conn)

                ' The % signs allow partial matches (e.g., "app" finds "apple")
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
End Class