Imports System.Data.SqlClient

Public Class DistributionService

    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"

    ' This function contains our core Business Logic
    Public Function ProcessDistribution(itemId As Integer, beneficiary As String, quantity As Integer) As String
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' Rule 1: Check current stock and expiration date
                Dim checkCmd As New SqlCommand("SELECT stock_quantity, expiration_date FROM FoodInventory WHERE item_id = @id", conn)
                checkCmd.Parameters.AddWithValue("@id", itemId)

                Dim reader As SqlDataReader = checkCmd.ExecuteReader()
                If Not reader.Read() Then
                    Return "Item ID not found."
                End If

                Dim currentStock As Integer = Convert.ToInt32(reader("stock_quantity"))
                Dim expDate As Date = Convert.ToDateTime(reader("expiration_date"))
                reader.Close()

                ' Enforce rules
                If expDate < Date.Now Then Return "Error: This item is expired."
                If currentStock < quantity Then Return "Error: Insufficient stock."

                ' Rule 2: Execute safe transaction (Subtract stock AND log distribution)
                Dim transaction As SqlTransaction = conn.BeginTransaction()
                Try
                    ' Update Inventory
                    Dim updateCmd As New SqlCommand("UPDATE FoodInventory SET stock_quantity = stock_quantity - @qty WHERE item_id = @id", conn, transaction)
                    updateCmd.Parameters.AddWithValue("@qty", quantity)
                    updateCmd.Parameters.AddWithValue("@id", itemId)
                    updateCmd.ExecuteNonQuery()

                    ' Log Distribution
                    Dim insertCmd As New SqlCommand("INSERT INTO Distributions (item_id, beneficiary_name, quantity_given) VALUES (@id, @ben, @qty)", conn, transaction)
                    insertCmd.Parameters.AddWithValue("@id", itemId)
                    insertCmd.Parameters.AddWithValue("@ben", beneficiary)
                    insertCmd.Parameters.AddWithValue("@qty", quantity)
                    insertCmd.ExecuteNonQuery()

                    transaction.Commit()
                    Return "Success"
                Catch ex As Exception
                    transaction.Rollback()
                    Return "Transaction Failed: " & ex.Message
                End Try

            End Using
        Catch ex As Exception
            Return "Database Error: " & ex.Message
        End Try
    End Function

End Class