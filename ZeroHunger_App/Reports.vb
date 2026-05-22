Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Reports

    Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=SDG2_ZeroHungerDB;Integrated Security=True;"
    Dim expiringData As New DataTable()
    Dim donationData As New DataTable() ' <-- Holds the donation history

    ' 1. Load ALL the SQL data when the form opens
    Private Sub ReportsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                ' Job A: Load Expiring Food
                Dim adapterExpiring As New SqlDataAdapter("SELECT * FROM View_ExpiringStock", conn)
                adapterExpiring.Fill(expiringData)

                ' Job B: Load Donation Logs (NEW code)
                ' We use ORDER BY so the newest donations show up at the top
                Dim adapterDonations As New SqlDataAdapter("SELECT * FROM DonationLogs ORDER BY date_received DESC", conn)
                adapterDonations.Fill(donationData)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' REPORT 1: EXPIRING FOOD 
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        PrintPreviewDialog1.Document = PrintDocument1 ' Tell it to look at Document 1
        PrintPreviewDialog1.WindowState = FormWindowState.Maximized
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim titleFont As New Font("Arial", 18, FontStyle.Bold)
        Dim headerFont As New Font("Arial", 12, FontStyle.Bold)
        Dim normalFont As New Font("Arial", 12)
        Dim yPos As Integer = 100

        e.Graphics.DrawString("EXPIRING FOOD REPORT", titleFont, Brushes.Black, 250, yPos)
        yPos += 60

        e.Graphics.DrawString("Item Name", headerFont, Brushes.Black, 100, yPos)
        e.Graphics.DrawString("Expiration Date", headerFont, Brushes.Black, 350, yPos)
        e.Graphics.DrawString("Stock Left", headerFont, Brushes.Black, 550, yPos)

        yPos += 25
        e.Graphics.DrawLine(Pens.Black, 100, yPos, 700, yPos)
        yPos += 15

        For Each row As DataRow In expiringData.Rows
            e.Graphics.DrawString(row("item_name").ToString(), normalFont, Brushes.Black, 100, yPos)
            e.Graphics.DrawString(Convert.ToDateTime(row("expiration_date")).ToShortDateString(), normalFont, Brushes.Black, 350, yPos)
            e.Graphics.DrawString(row("stock_quantity").ToString(), normalFont, Brushes.Black, 550, yPos)
            yPos += 30
        Next

        yPos += 50
        e.Graphics.DrawString("Report Generated On: " & DateTime.Now.ToString(), normalFont, Brushes.Gray, 100, yPos)
    End Sub

    ' REPORT 2: DONATION LOGS
    Private Sub btnDonationReport_Click(sender As Object, e As EventArgs) Handles btnDonationReport.Click
        PrintPreviewDialog1.Document = PrintDocument2 ' Tell it to look at Document 2
        PrintPreviewDialog1.WindowState = FormWindowState.Maximized
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument2.PrintPage
        Dim titleFont As New Font("Arial", 18, FontStyle.Bold)
        Dim headerFont As New Font("Arial", 12, FontStyle.Bold)
        Dim normalFont As New Font("Arial", 12)
        Dim yPos As Integer = 100

        e.Graphics.DrawString("OFFICIAL DONATION LOGS", titleFont, Brushes.Black, 250, yPos)
        yPos += 60

        e.Graphics.DrawString("Date", headerFont, Brushes.Black, 50, yPos)
        e.Graphics.DrawString("Donor Name", headerFont, Brushes.Black, 200, yPos)
        e.Graphics.DrawString("Item Donated", headerFont, Brushes.Black, 450, yPos)
        e.Graphics.DrawString("Qty", headerFont, Brushes.Black, 650, yPos)

        yPos += 25
        e.Graphics.DrawLine(Pens.Black, 50, yPos, 700, yPos)
        yPos += 15

        For Each row As DataRow In donationData.Rows
            ' Format the data
            Dim dateRec As String = Convert.ToDateTime(row("date_received")).ToShortDateString()
            Dim donor As String = row("donor_name").ToString()
            Dim item As String = row("item_name").ToString()
            Dim qty As String = row("quantity_donated").ToString()

            e.Graphics.DrawString(dateRec, normalFont, Brushes.Black, 50, yPos)
            e.Graphics.DrawString(donor, normalFont, Brushes.Black, 200, yPos)
            e.Graphics.DrawString(item, normalFont, Brushes.Black, 450, yPos)
            e.Graphics.DrawString(qty, normalFont, Brushes.Black, 650, yPos)

            yPos += 30
        Next

        yPos += 50
        e.Graphics.DrawString("Report Generated On: " & DateTime.Now.ToString(), normalFont, Brushes.Gray, 50, yPos)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
End Class