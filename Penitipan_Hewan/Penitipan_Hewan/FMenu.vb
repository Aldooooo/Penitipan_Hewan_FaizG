Public Class FMenu

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        Jenis_Paket.Show()
        Jenis_Paket.MdiParent = Me
    End Sub

    Private Sub PenggunaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenggunaToolStripMenuItem.Click
        Kosumen.Show()
        Kosumen.MdiParent = Me
    End Sub

    Private Sub LoginToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginToolStripMenuItem.Click
        Login.Show()
        Login.MdiParent = Me

    End Sub

    Private Sub PenggunaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenggunaToolStripMenuItem1.Click
        Pengguna.Show()
        Pengguna.MdiParent = Me
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        End
    End Sub

  
    Private Sub PemantauanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemantauanToolStripMenuItem.Click
        Pemantauan.Show()
        Pemantauan.MdiParent = Me
    End Sub

    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem.Click
        Pendaftaran.Show()
        Pendaftaran.MdiParent = Me
    End Sub

End Class