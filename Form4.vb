﻿Imports System.Data.SqlClient
Public Class Form4

    Dim baglan As SqlConnection = New SqlConnection("Data Source=DESKTOP-6RUFCV1;Initial Catalog=bys;Integrated Security=True")
    Sub sehir_listele()
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from Stadt", baglan)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim kayit_kontrol As Boolean

        Try
            baglan.Open()
            kayit_kontrol = True
            Dim ara As String
            ara = "select * from Stadt  where Stadt_ID like'" + TextBox1.Text + "'"
            Dim dt As DataTable = New DataTable()
            Dim da As SqlDataAdapter = New SqlDataAdapter(ara, baglan)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            baglan.Close()
            MessageBox.Show("Kullanıcı bulunmuştur.", "SKY Persone Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            baglan.Close()
            MessageBox.Show("KUllanıcı Bulunamadı.", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub 'ARA butonun kodları 

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan.Open()
        Dim kayit_kontrol As Boolean
        kayit_kontrol = False
        Dim komut As SqlCommand = New SqlCommand("select * from Stadt where Stadt_ID='" + TextBox1.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = komut.ExecuteReader()

        While oku.Read
            kayit_kontrol = True
            Exit While
        End While
        baglan.Close()
        If kayit_kontrol = False Then
            Try
                baglan.Open()
                Dim ekle_komut As SqlCommand = New SqlCommand("insert into Stadt values('" + TextBox1.Text.ToString() + "','" + TextBox2.Text.ToString() + "')", baglan)
                ekle_komut.ExecuteReader()
                MessageBox.Show("Şehir Oluşturuldu.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                baglan.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                baglan.Close()
            End Try
        End If
        If kayit_kontrol = True Then
            MessageBox.Show("Bu ID numaralı Şehir zaten mevcut.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If
    End Sub 'EKLE butonun kodları 

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglan.Open()

        Dim update_komut As SqlCommand = New SqlCommand("update Stadt set Stadt_Name ='" + TextBox2.Text + "' where Stadt_ID='" + TextBox1.Text + "'", baglan)
        update_komut.ExecuteNonQuery()
        baglan.Close()
        MessageBox.Show("Şehir Başarıyla Güncellenmiştir.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub 'GÜNCELLE butonun kodları 

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            baglan.Open()
            Dim sil_komut As SqlCommand = New SqlCommand("delete from Stadt where Stadt_ID='" + TextBox1.Text + "'", baglan)
            sil_komut.ExecuteNonQuery()
            baglan.Close()
            MessageBox.Show("Stadt başarıyla silinmiştir.", "SKY PERSONEL TAKIP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            baglan.Close()

        End Try
    End Sub 'SİL butonun kodları 

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub 'Form Temizle butonun kodları 

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        sehir_listele()
        DataGridView1.Visible = True
        Button19.Visible = True
    End Sub ' Verileri Yenile Butonunun kodları

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sehir_listele()
        DataGridView1.Visible = False
        Button19.Visible = False
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        DataGridView1.Visible = False
        Button19.Visible = False
    End Sub
End Class