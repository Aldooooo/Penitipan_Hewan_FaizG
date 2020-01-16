Imports System.Threading.Tasks
Imports System.Security.Cryptography
Imports System.Text

Public Class Pemantauan

    Dim tempNo_Kp As Integer
    Dim lst As ListViewItem

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String

        If tempNo_Kp = 0 Then
            sql = "insert into Pemantauan (Jumlah_Kitten, Tanggal, Keterangan_Harian, Pemeriksa, No_Form) " & _
            "values ('" & TextBox2.Text.Trim & "', '" & DateTimePicker1.Text.Trim & "', '" & TextBox1.Text.Trim & "', '" & TextBox4.Text.Trim & "', '" & TextBox5.Text.Trim & "')"
        Else
            sql = "update Pemantauan set Jumlah_Kitten = '" & TextBox2.Text.Trim & "', " & _
                "Tanggal = '" & DateTimePicker1.Text.Trim & "', Keterangan_Harian = '" & TextBox1.Text.Trim & "' , Pemeriksa = '" & TextBox4.Text.Trim & "' , No_Form = '" & TextBox5.Text.Trim & "' '" & _
                "where No_Kp = " & tempNo_Kp
        End If

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub
    Private Sub kosong()
        tempNo_Kp = Nothing
        TextBox2.Text = Nothing
        DateTimePicker1.Text = Nothing
        TextBox1.Text = Nothing
        TextBox4.Text = Nothing
        TextBox5.Text = Nothing

    End Sub
    Async Function loadGrid(ByVal cari As String) As Task
        MProgress.showProgress(ProgressBar1)

        Dim sql As String

        If cari = Nothing Then
            sql = "select * from Pemantauan"
        Else
            sql = "select * from Pemantauan " & _
                    "where No_Kp like '%" & cari & "%'"
        End If

        Dim dt As DataTable = Await Task(Of DataTable).Factory.StartNew(Function() MKoneksi.getList(sql))

        ListView1.Items.Clear()
        For Each dr As DataRow In dt.Rows
            lst = ListView1.Items.Add(dr(0))
            lst.SubItems.Add(dr(1))
            lst.SubItems.Add(dr(2))
            lst.SubItems.Add(dr(3))
            lst.SubItems.Add(dr(4))
        Next

        tempNo_Kp = Nothing
        MProgress.hideProgress(ProgressBar1)
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sql As String = "delete from Pemantauan where No_Kp = " & tempNo_Kp

        MProgress.showProgress(ProgressBar1)
        Dim myTask = Task.Factory.StartNew(Sub() MKoneksi.exec(sql))
        Task.WaitAll(myTask) 'menunggu hingga selesai
        MProgress.hideProgress(ProgressBar1)

        kosong()
        Call loadGrid(Nothing)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call loadGrid(TextBox3.Text.Trim)
    End Sub

    Private Sub Pemantauan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call loadGrid(Nothing)
    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        With ListView1
            tempNo_Kp = .SelectedItems.Item(0).Text
            TextBox2.Text = .SelectedItems.Item(0).SubItems(1).Text
            DateTimePicker1.Text = .SelectedItems.Item(0).SubItems(2).Text
            TextBox1.Text = .SelectedItems.Item(0).SubItems(3).Text
            TextBox4.Text = .SelectedItems.Item(0).SubItems(4).Text
            TextBox5.Text = .SelectedItems.Item(0).SubItems(5).Text
        End With
    End Sub

    
End Class