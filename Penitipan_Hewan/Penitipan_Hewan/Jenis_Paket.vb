Imports System.Threading.Tasks
Imports System.Security.Cryptography
Imports System.Text
Public Class Jenis_Paket
    Dim tempKode_Paket As Integer
    Dim lst As ListViewItem
    Private Sub FBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call loadGrid(Nothing)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String

        If tempKode_Paket = 0 Then
            sql = "insert into Jenis_Paket ( Jenis_Paket, Harga, Keterangan_Paket) " & _
            "values ('" & TextBox2.Text.Trim & "',  '" & ComboBox1.Text.Trim & "', '" & TextBox4.Text.Trim & "')"
        Else
            sql = "update Jenis_Paket set Jenis_Paket = '" & TextBox2.Text.Trim & "', " & _
                "Harga = '" & ComboBox1.Text.Trim & "', Keterangan_Paket = '" & TextBox4.Text.Trim & "'  " & _
                "where Kode_Paket = " & tempKode_Paket
        End If

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub
    Private Sub kosong()
        tempKode_Paket = Nothing
        TextBox2.Text = Nothing
        ComboBox1.SelectedIndex = 0
        TextBox4.Text = Nothing
    End Sub
    Async Function loadGrid(ByVal cari As String) As Task
        MProgress.showProgress(ProgressBar1)

        Dim sql As String

        If cari = Nothing Then
            sql = "select * from Jenis_Paket"
        Else
            sql = "select * from Jenis_Paket " & _
                    "where Kode_Paket like '%" & cari & "%'"
        End If

        Dim dt As DataTable = Await Task(Of DataTable).Factory.StartNew(Function() MKoneksi.getList(sql))

        ListView1.Items.Clear()
        For Each dr As DataRow In dt.Rows
            lst = ListView1.Items.Add(dr(0))
            lst.SubItems.Add(dr(1))
            lst.SubItems.Add(dr(2))
            lst.SubItems.Add(dr(3))

        Next

        tempKode_Paket = Nothing
        MProgress.hideProgress(ProgressBar1)
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sql As String = "delete from Jenis_Paket where Kode_Paket = " & tempKode_Paket

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        With ListView1
            tempKode_Paket = .SelectedItems.Item(0).Text
            TextBox2.Text = .SelectedItems.Item(0).SubItems(1).Text
            ComboBox1.Text = .SelectedItems.Item(0).SubItems(2).Text
            TextBox4.Text = .SelectedItems.Item(0).SubItems(3).Text
        End With

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call loadGrid(TextBox3.Text.Trim)
    End Sub
End Class