Imports System.Threading.Tasks
Imports System.Security.Cryptography
Imports System.Text
Public Class Kosumen
    Dim tempKode_Kucing As Integer
    Dim lst As ListViewItem
    Private Sub FBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call loadGrid(Nothing)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String

        If tempKode_Kucing = 0 Then
            sql = "insert into Kosumen (Na_Kucing, Jen_Kelamin, Keterangan, Berat, Rek_DrHewan, Na_Pemilik, No_Telepon, Alamat) " & _
            "values ('" & TextBox2.Text.Trim & "', '" & ComboBox2.Text.Trim & "', '" & TextBox5.Text.Trim & "', '" & TextBox6.Text.Trim & "', '" & TextBox10.Text.Trim & "', '" & TextBox9.Text.Trim & "', '" & TextBox8.Text.Trim & "', '" & TextBox7.Text.Trim & "')"
        Else
            sql = "update Kosumen set Na_Kucing = '" & TextBox2.Text.Trim & "', " & _
                "Jen_Kelamin = '" & ComboBox2.Text.Trim & "', Keterangan = '" & TextBox5.Text.Trim & "' , Berat = '" & TextBox6.Text.Trim & "' , Rek_DrHewan = '" & TextBox10.Text.Trim & "' , Na_Pemilik = '" & TextBox9.Text.Trim & "' , No_Pemilik = '" & TextBox8.Text.Trim & "' , Telepon = '" & TextBox7.Text.Trim & "' , Alamat = '" & _
                "where Kode_Kucing = " & tempKode_Kucing
        End If

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub
    Private Sub kosong()
        tempKode_Kucing = Nothing
        TextBox2.Text = Nothing
        ComboBox2.Text = Nothing
        TextBox5.Text = Nothing
        TextBox6.Text = Nothing
        TextBox10.Text = Nothing
        TextBox9.Text = Nothing
        TextBox8.Text = Nothing
        TextBox7.Text = Nothing

    End Sub
    Async Function loadGrid(ByVal cari As String) As Task
        MProgress.showProgress(ProgressBar1)

        Dim sql As String

        If cari = Nothing Then
            sql = "select * from Kosumen"
        Else
            sql = "select * from Kosumen " & _
                    "where Kode_Kucing like '%" & cari & "%'"
        End If

        Dim dt As DataTable = Await Task(Of DataTable).Factory.StartNew(Function() MKoneksi.getList(sql))

        ListView1.Items.Clear()
        For Each dr As DataRow In dt.Rows
            lst = ListView1.Items.Add(dr(0))
            lst.SubItems.Add(dr(1))
            lst.SubItems.Add(dr(2))
            lst.SubItems.Add(dr(3))
            lst.SubItems.Add(dr(4))
            lst.SubItems.Add(dr(5))
            lst.SubItems.Add(dr(6))
            lst.SubItems.Add(dr(7))
            lst.SubItems.Add(dr(8))
        Next

        tempKode_Kucing = Nothing
        MProgress.hideProgress(ProgressBar1)
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sql As String = "delete from Kosumen where Kode_Kucing = " & tempKode_Kucing

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        With ListView1
            tempKode_Kucing = .SelectedItems.Item(0).Text
            TextBox2.Text = .SelectedItems.Item(0).SubItems(1).Text
            ComboBox2.Text = .SelectedItems.Item(0).SubItems(2).Text
            TextBox5.Text = .SelectedItems.Item(0).SubItems(3).Text
            TextBox6.Text = .SelectedItems.Item(0).SubItems(4).Text
            TextBox10.Text = .SelectedItems.Item(0).SubItems(5).Text
            TextBox9.Text = .SelectedItems.Item(0).SubItems(6).Text
            TextBox8.Text = .SelectedItems.Item(0).SubItems(7).Text
            TextBox7.Text = .SelectedItems.Item(0).SubItems(8).Text
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call loadGrid(TextBox3.Text.Trim)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class