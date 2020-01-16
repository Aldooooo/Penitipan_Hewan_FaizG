Imports System.Threading.Tasks
Imports System.Security.Cryptography
Imports System.Text

Public Class Pendaftaran

    Dim tempNo_Form As Integer
    Dim lst As ListViewItem

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String

        If tempNo_Form = 0 Then
            sql = "insert into Tanggal ( Tanggal, No_Kamar, Nama_Pemilik, Harga, Keterangan_Harga) " & _
            "values ('" & DateTimePicker1.Text.Trim & "',  '" & TextBox2.Text.Trim & "', '" & TextBox5.Text.Trim & "',  '" & TextBox4.Text.Trim & "',  '" & TextBox6.Text.Trim & "')"
        Else
            sql = "update Tanggal set Tanggal = '" & DateTimePicker1.Text.Trim & "', " & _
                "No_Kamar = '" & TextBox2.Text.Trim & "', Nama_Pemilik = '" & TextBox5.Text.Trim & "', Nama_Pemilik = '" & TextBox4.Text.Trim & "', Nama_Pemilik = '" & TextBox6.Text.Trim & "'  " & _
                "where No_Form = " & tempNo_Form
        End If

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub
    Private Sub kosong()
        tempNo_Form = Nothing
        DateTimePicker1.Text = Nothing
        TextBox2.Text = Nothing
        TextBox5.Text = Nothing
        TextBox4.Text = Nothing
        TextBox6.Text = Nothing
    End Sub
    Async Function loadGrid(ByVal cari As String) As Task
        MProgress.showProgress(ProgressBar1)

        Dim sql As String

        If cari = Nothing Then
            sql = "select * from Tanggal"
        Else
            sql = "select * from Tanggal " & _
                    "where No_Form like '%" & cari & "%'"
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
        Next

        tempNo_Form = Nothing
        MProgress.hideProgress(ProgressBar1)
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sql As String = "delete from Tanggal where No_Form = " & tempNo_Form

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        With ListView1
            tempNo_Form = .SelectedItems.Item(0).Text
            DateTimePicker1.Text = .SelectedItems.Item(0).SubItems(1).Text
            TextBox2.Text = .SelectedItems.Item(0).SubItems(2).Text
            TextBox5.Text = .SelectedItems.Item(0).SubItems(3).Text
            TextBox4.Text = .SelectedItems.Item(0).SubItems(3).Text
            TextBox6.Text = .SelectedItems.Item(0).SubItems(3).Text
        End With

    End Sub

  
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call loadGrid(TextBox3.Text.Trim)
    End Sub

    Private Sub Pendaftaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call loadGrid(Nothing)
    End Sub
End Class