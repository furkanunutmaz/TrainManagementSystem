Imports System.Data.SqlClient
Public Class giris

    Dim baglan As SqlConnection = New SqlConnection("Data Source=DESKTOP-6RUFCV1;Initial Catalog=bys;Integrated Security=True")

    Dim giris_hakki As Integer = 3
    Dim durum As Boolean = False
    Public Shared isim, soyisim, nickname As String
    Dim yetki As String


    Private Sub giris_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        label5.Text = Convert.ToString(giris_hakki)
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = 0
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If giris_hakki <> 0 Then

            If ComboBox1.SelectedIndex = 0 Then
                Try
                    baglan.Open()
                    Dim sorgu As String = "select * from admin where KullaniciAdi=@kullaniciadi and sifre = @sifre and yetki=@yetki"
                    Dim prm_ad As SqlParameter = New SqlParameter("kullaniciadi", textBox1.Text.Trim())
                    Dim prm_sifre As SqlParameter = New SqlParameter("sifre", textBox2.Text.Trim())
                    Dim prm_yetki As SqlParameter = New SqlParameter("yetki", ComboBox1.SelectedItem.Trim())

                    Dim komut As SqlCommand = New SqlCommand(sorgu, baglan)

                    komut.Parameters.Add(prm_ad)
                    komut.Parameters.Add(prm_sifre)
                    komut.Parameters.Add(prm_yetki)

                    Dim dt As DataTable = New DataTable()
                    Dim da As SqlDataAdapter = New SqlDataAdapter(komut)
                    da.Fill(dt)
                    Dim oku As SqlDataReader = komut.ExecuteReader
                    While oku.Read
                        Dim ekle As ListViewItem = New ListViewItem()
                        isim = oku("Vorname").ToString()
                        soyisim = oku("Nachname").ToString()
                        nickname = oku("KullaniciAdi").ToString()
                        yetki = oku("Yetki").ToString()

                    End While

                    If dt.Rows.Count > 0 Then
                        durum = True
                        ProgressBar1.Increment(25)
                        Dim form As New Form1
                        form.Show()
                        Me.Hide()
                    End If

                    baglan.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    baglan.Close()
                End Try
            ElseIf ComboBox1.SelectedIndex = 1 Then

                Try
                    baglan.Open()
                    Dim sorgu As String = "select * from admin where KullaniciAdi=@kullaniciadi and sifre = @sifre and yetki=@yetki"
                    Dim prm_ad As SqlParameter = New SqlParameter("kullaniciadi", textBox1.Text.Trim())
                    Dim prm_sifre As SqlParameter = New SqlParameter("sifre", textBox2.Text.Trim())
                    Dim prm_yetki As SqlParameter = New SqlParameter("yetki", ComboBox1.SelectedItem.Trim())

                    Dim komut As SqlCommand = New SqlCommand(sorgu, baglan)

                    komut.Parameters.Add(prm_ad)
                    komut.Parameters.Add(prm_sifre)
                    komut.Parameters.Add(prm_yetki)

                    Dim dt As DataTable = New DataTable()
                    Dim da As SqlDataAdapter = New SqlDataAdapter(komut)
                    da.Fill(dt)
                    Dim oku As SqlDataReader = komut.ExecuteReader
                    While oku.Read
                        Dim ekle As ListViewItem = New ListViewItem()
                        isim = oku("Vorname").ToString()
                        soyisim = oku("Nachname").ToString()
                        nickname = oku("KullaniciAdi").ToString()
                        yetki = oku("Yetki").ToString()

                    End While

                    If dt.Rows.Count > 0 Then
                        durum = True
                        Dim form As New Form7
                        form.Show()
                        Me.Hide()
                    End If

                    baglan.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    baglan.Close()
                End Try

            End If
            End If

            If durum = False Then
            giris_hakki = giris_hakki - 1
            label5.Text = Convert.ToString(giris_hakki)
        End If

        If giris_hakki = 0 Then
            Button1.Enabled = False
            MessageBox.Show("3 defa hatalı giriş yaptınız için programa girişiniz engellenmiştir.", "ZUG PROJEKT",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

End Class