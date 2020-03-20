Imports System.Data.SqlClient
Public Class Form3
    Dim baglan As SqlConnection = New SqlConnection("Data Source=DESKTOP-6RUFCV1;Initial Catalog=bys;Integrated Security=True")
    Sub trenleri_listele()
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from Zug", baglan)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form As New Form6
        form.Show()
    End Sub 'MODEL butonunun kodları
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            baglan.Open()
            Dim sil_komut As SqlCommand = New SqlCommand("delete from Zug where Zug_ID='" + TextBox1.Text + "'", baglan)
            sil_komut.ExecuteNonQuery()
            baglan.Close()
            MessageBox.Show("Zug başarıyla silinmiştir.", "SKY PERSONEL TAKIP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            baglan.Close()

        End Try
    End Sub 'sil butonunun kodları
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim kayit_kontrol As Boolean

        Try
            baglan.Open()
            kayit_kontrol = True
            Dim ara As String
            ara = "select * from Zug  where Zug_ID  like'" + TextBox1.Text + "'"
            Dim dt As DataTable = New DataTable()
            Dim da As SqlDataAdapter = New SqlDataAdapter(ara, baglan)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            baglan.Close()
            MessageBox.Show("Zug bulunmuştur.", "SKY Persone Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            baglan.Close()
            MessageBox.Show("Zug Bulunamadı.", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub ' ARA butonunun kodlar
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan.Open()
        Dim kayit_kontrol As Boolean
        kayit_kontrol = False
        Dim komut As SqlCommand = New SqlCommand("select * from Zug where Zug_ID='" + TextBox1.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = komut.ExecuteReader()

        While oku.Read
            kayit_kontrol = True
            Exit While
        End While
        baglan.Close()
        If kayit_kontrol = False Then
            Try
                baglan.Open()
                Dim ekle_komut As SqlCommand = New SqlCommand("insert into Zug values('" + TextBox1.Text.ToString() + "','" + TextBox2.Text.ToString() + "','" + TextBox3.Text.ToString() + "')", baglan)
                ekle_komut.ExecuteReader()
                MessageBox.Show("Zug Oluşturuldu.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                baglan.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                baglan.Close()
            End Try
        End If
        If kayit_kontrol = True Then
            MessageBox.Show("Bu ID numaralı Zug zaten mevcut.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If
    End Sub 'EKLE butonun kodları

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglan.Open()

        Dim update_komut As SqlCommand = New SqlCommand("update Zug set Kapazitat ='" + TextBox2.Text + "', Modell_ID='" + TextBox3.Text + "' where Zug_ID='" + TextBox1.Text + "'", baglan)
        update_komut.ExecuteNonQuery()
        baglan.Close()
        MessageBox.Show("Zug Başarıyla Güncellenmiştir.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub 'GÜNCELLE butonunun kodları
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        DataGridView1.Visible = False
        Button18.Visible = False
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        trenleri_listele()
        DataGridView1.Visible = False
        Button18.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        trenleri_listele()
        DataGridView1.Visible = True
        Button18.Visible = True
    End Sub ' VERİLERİ GÖSTER butonunun kodları




End Class