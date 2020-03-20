Imports System.Data.SqlClient
Public Class Form6
    Dim baglan As SqlConnection = New SqlConnection("Data Source=DESKTOP-6RUFCV1;Initial Catalog=bys;Integrated Security=True")

    Sub model_goster()
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from Modell", baglan)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        DataGridView1.Visible = False
        Button18.Visible = False
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        model_goster()
        DataGridView1.Visible = False
        Button18.Visible = False
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        model_goster()
        DataGridView1.Visible = True
        Button18.Visible = True
    End Sub ' VERİLERİ GÖSTER butonunun kodları

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim kayit_kontrol As Boolean

        Try
            baglan.Open()
            kayit_kontrol = True
            Dim ara As String
            ara = "select * from Modell where Modell_ID  like'" + TextBox1.Text + "'"
            Dim dt As DataTable = New DataTable()
            Dim da As SqlDataAdapter = New SqlDataAdapter(ara, baglan)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            baglan.Close()
            MessageBox.Show("Modell bulunmuştur.", "SKY Persone Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            baglan.Close()
            MessageBox.Show("Modell  Bulunamadı.", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub ' ARA butonunun kodları

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            baglan.Open()
            Dim sil_komut As SqlCommand = New SqlCommand("delete from Modell where Modell_ID='" + TextBox1.Text + "'", baglan)
            sil_komut.ExecuteNonQuery()
            baglan.Close()
            MessageBox.Show("Modell başarıyla silinmiştir.", "SKY PERSONEL TAKIP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            baglan.Close()

        End Try
    End Sub ' SİL butonunun kodları

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan.Open()
        Dim kayit_kontrol As Boolean
        kayit_kontrol = False
        Dim komut As SqlCommand = New SqlCommand("select * from Modell where Modell_ID='" + TextBox1.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = komut.ExecuteReader()

        While oku.Read
            kayit_kontrol = True
            Exit While
        End While
        baglan.Close()
        If kayit_kontrol = False Then
            Try
                baglan.Open()
                Dim ekle_komut As SqlCommand = New SqlCommand("insert into Modell values('" + TextBox1.Text.ToString() + "','" + TextBox3.Text.ToString() + "')", baglan)
                ekle_komut.ExecuteReader()
                MessageBox.Show("Modell Oluşturuldu.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                baglan.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                baglan.Close()
            End Try
        End If
        If kayit_kontrol = True Then
            MessageBox.Show("Bu ID numaralı Modell zaten mevcut.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If
    End Sub ' EKLE butonunun kodları

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglan.Open()

        Dim update_komut As SqlCommand = New SqlCommand("update Modell set  Modell_Name='" + TextBox3.Text + "' where Modell_ID='" + TextBox1.Text + "'", baglan)
        update_komut.ExecuteNonQuery()
        baglan.Close()
        MessageBox.Show("Modell Başarıyla Güncellenmiştir.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub ' GÜNCELLE butonunun kodları
End Class