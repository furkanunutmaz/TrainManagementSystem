Imports System.Data.SqlClient
Public Class Form7

    Dim baglan As SqlConnection = New SqlConnection("Data Source=DESKTOP-6RUFCV1;Initial Catalog=bys;Integrated Security=True")
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        yolculuk_listele()

        Dim kayit_kontrol As Boolean

        Try
            baglan.Open()
            Dim ara As String
            ara = "select * from Kunden where Kunden_ID like'" + TextBox13.Text + "'"

            Dim komut As SqlCommand = New SqlCommand(ara, baglan)
            Dim oku As SqlDataReader = komut.ExecuteReader()

            While oku.Read
                Dim ekle As ListViewItem = New ListViewItem()
                kayit_kontrol = True
                Label1.Text = oku("Vorname").ToString()
                Label7.Text = oku("Nachname").ToString()
                Label10.Text = oku("Telefonnummer").ToString()
                Label14.Text = oku("Geburtsdatum").ToString()
                Label11.Text = oku("Stadt_ID").ToString()
                Label12.Text = oku("Adress").ToString()
                Label13.Text = oku("Kartennummer").ToString()
                If oku("Geschlecht_ID") = False Then
                    RadioButton1.Checked = True
                ElseIf oku("Geschlecht_ID") = True Then
                    RadioButton2.Checked = True
                End If
                Exit While

            End While

            baglan.Close()
            If kayit_kontrol = True Then
                MessageBox.Show("Kullanıcı bulunmuştur.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            baglan.Close()
            MessageBox.Show("Kullanıcı bulunamadı.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If kayit_kontrol = False Then
            MessageBox.Show("Kullanıcı bulunamadı.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If

    End Sub
    Sub yolculuk_listele()
        Dim dt As DataTable = New DataTable()
        Dim da As SqlDataAdapter = New SqlDataAdapter("select Kunden_ID, Fahrkarte_ID,Stadt_Name as AbfahrtStadt
from Fahrkarte as f inner join Linie as l
on f.Linie_ID = l.Linie_ID
inner join Stadt as s on l.Abfahrtstadt_ID=s.Stadt_ID where Kunden_ID='" + TextBox13.Text + "'", baglan)
        da.Fill(dt)
        DataGridView1.DataSource = dt

    End Sub
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label19.Text = giris.isim + giris.soyisim
        yolculuk_listele()
        DataGridView1.Visible = False
        Button19.Visible = False
        Try

            PictureBox1.Image = Image.FromFile(Application.StartupPath + "\\memurresimler\\" + giris.nickname + ".jpg")

        Catch ex As Exception

            PictureBox1.Image = Image.FromFile(Application.StartupPath + "\\memurresimler\\resimyok.png")
        End Try
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        DataGridView1.Visible = False
        Button19.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        yolculuk_listele()
        DataGridView1.Visible = True
        Button19.Visible = True

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Dim form As New giris
        form.Show()
        Me.Hide()

    End Sub

End Class