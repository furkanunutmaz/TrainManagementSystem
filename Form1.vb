Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    Public Shared sayac As Integer = 100

    Dim baglan As SqlConnection = New SqlConnection("Data Source=DESKTOP-6RUFCV1;Initial Catalog=bys;Integrated Security=True")
    Sub musterileri_listele()
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from Kunden", baglan)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub
    Sub iscileri_listele()
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from Arbeiter", baglan)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        DataGridView2.DataSource = ds.Tables(0)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label19.Text = giris.isim + giris.soyisim
        musterileri_listele()
        iscileri_listele()
        DataGridView1.Visible = False
        Button18.Visible = False
        DataGridView2.Visible = False
        Button19.Visible = False
        Try

            PictureBox1.Image = Image.FromFile(Application.StartupPath + "\\yöneticiresimler\\" + giris.nickname + ".jpg")

        Catch ex As Exception

            PictureBox1.Image = Image.FromFile(Application.StartupPath + "\\yöneticiresimler\\resimyok.png")
        End Try

        Timer1.Enabled = True
        Timer1.Interval = 1000



    End Sub

    '"KUNDEN" tarafı kaydet butonu kodları
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan.Open()
        Dim geschleht As String
        geschleht = ""
        Dim kayit_kontrol As Boolean
        kayit_kontrol = False
        Dim komut As SqlCommand = New SqlCommand("select * from Kunden where Kunden_ID='" + TextBox13.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = komut.ExecuteReader()

        While oku.Read
            kayit_kontrol = True
            Exit While
        End While
        baglan.Close()

        If kayit_kontrol = False Then

            If RadioButton1.Checked = True Then
                geschleht = "0"
            End If

            If RadioButton2.Checked = True Then
                geschleht = "1"
            End If

            Try
                baglan.Open()

                Dim ekle_komut As SqlCommand = New SqlCommand("insert into Kunden values('" + TextBox13.Text.ToString() + "','" + TextBox2.Text.ToString() + "','" + TextBox3.Text.ToString() + "','" + TextBox4.Text.ToString() + "','" + geschleht + "','" + DateTimePicker1.Value.ToShortTimeString + "','" + TextBox14.Text.ToString() + "','" + RichTextBox1.Text.ToString() + "','" + TextBox5.Text.ToString() + "')", baglan)
                ekle_komut.ExecuteNonQuery()
                MessageBox.Show("Kunde wird erstellt.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                baglan.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                baglan.Close()
            End Try
        End If

        If kayit_kontrol = True Then
            MessageBox.Show("Diese ID ist in der Kundendatenbank vorhanden.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub 'KAYDET butonunun kodları

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            baglan.Open()
            Dim sil_komut As SqlCommand = New SqlCommand("delete from Kunden where Kunden_ID='" + TextBox13.Text + "'", baglan)
            sil_komut.ExecuteNonQuery()
            baglan.Close()
            MessageBox.Show("Kunde wird gelöscht.", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            baglan.Close()
        End Try
    End Sub 'SİL butonunun kodları

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TextBox13.Text <> "" Then
            baglan.Open()
            Dim geschleht As String
            geschleht = ""

            If RadioButton1.Checked = True Then
                geschleht = "0"
            End If

            If RadioButton2.Checked = True Then
                geschleht = "1"
            End If
            Dim update_komut As SqlCommand = New SqlCommand("update Kunden set Vorname='" + TextBox2.Text + "',Nachname='" + TextBox3.Text + "',Telefonnummer='" + TextBox4.Text + "',Geschlecht_ID='" + geschleht + "',Geburtsdatum='" + DateTimePicker1.Value.ToShortTimeString + "',Stadt_ID='" + TextBox14.Text + "',Adress='" + RichTextBox1.Text + "',Kartennummer='" + TextBox5.Text + "' where Kunden_ID='" + TextBox13.Text + "'", baglan)
            update_komut.ExecuteNonQuery()
            baglan.Close()
            MessageBox.Show("Müşteri güncellendi.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Else
            MessageBox.Show("Müşteri güncellenemedi.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub 'GÜNCELLE butonunun kodları

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        DataGridView1.Visible = True
        Button18.Visible = True
        baglan.Open()
        Dim kayit_kontrol As Boolean
        kayit_kontrol = True
        Dim ara As String
        ara = "select * from Kunden where Vorname like'" + TextBox2.Text + "'"

        Dim ara_komut As SqlCommand = New SqlCommand("select * from Kunden where Vorname like'" + TextBox2.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = ara_komut.ExecuteReader()
        While oku.Read
            kayit_kontrol = False
            Exit While
        End While
        baglan.Close()
        If kayit_kontrol = False Then
            Try
                baglan.Open()
                Dim dt As DataTable = New DataTable()
                Dim da As SqlDataAdapter = New SqlDataAdapter(ara, baglan)
                da.Fill(dt)
                DataGridView1.DataSource = dt
                baglan.Close()
                MessageBox.Show("Kunde wird gefunden.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Catch ex As Exception
                baglan.Close()
                MessageBox.Show("Kunde hat nicht gefunden.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        If kayit_kontrol = True Then
            MessageBox.Show("Kunde hat nicht gefunden.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If


    End Sub 'ARA butonunun kodları

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        musterileri_listele()
        DataGridView1.Visible = True
        Button18.Visible = True

    End Sub 'VERİLERİ GÖSTER butonunun kodları

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        RichTextBox1.Text = ""
        RadioButton1.Checked = True

    End Sub 'FORMU TEMİZLE butonunun kodları
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        DataGridView1.Visible = False
        Button18.Visible = False

    End Sub ' çarpı butonunun kodları

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim form As New Form2
        form.Show()

    End Sub 'FORM2ye geçen kodlar
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form As New Form3
        form.Show()
    End Sub 'FORM3 e geçen kodlar

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim form As New Form4
        form.Show()
    End Sub 'FORM4 e geçen kodlar

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim form As New Form5
        form.Show()
    End Sub 'FORM5 e geçen kodlar

    Private Sub Button17_Click(sender As Object, e As EventArgs) 
        Dim form As New Form7
        form.Show()
    End Sub


    'ARBEITER SEKMESİNİN KODLARI ...

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        DataGridView2.Visible = True
        Button19.Visible = True
        baglan.Open()
        Dim kayit_kontrol As Boolean
        kayit_kontrol = True
        Dim ara As String
        ara = "select * from Arbeiter where Arbeiter_ID like'" + TextBox6.Text + "'"

        Dim ara_komut As SqlCommand = New SqlCommand("select * from Arbeiter where Arbeiter_ID like'" + TextBox6.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = ara_komut.ExecuteReader()
        While oku.Read
            kayit_kontrol = False
            Exit While
        End While
        baglan.Close()
        If kayit_kontrol = False Then
            Try
                baglan.Open()
                Dim dt As DataTable = New DataTable()
                Dim da As SqlDataAdapter = New SqlDataAdapter(ara, baglan)
                da.Fill(dt)
                DataGridView2.DataSource = dt
                baglan.Close()
                MessageBox.Show("İşçi bulundu", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Catch ex As Exception
                baglan.Close()
                MessageBox.Show("İşçi bulunamadı.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        If kayit_kontrol = True Then
            MessageBox.Show("İşçi bulunamadı.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If
    End Sub 'ARA butonunun kodları

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        baglan.Open()
        Dim kayit_kontrol As Boolean
        kayit_kontrol = False
        Dim komut As SqlCommand = New SqlCommand("select * from Arbeiter where Arbeiter_ID='" + TextBox6.Text.ToString() + "'", baglan)
        Dim oku As SqlDataReader = komut.ExecuteReader()
        While oku.Read
            kayit_kontrol = True
            Exit While
        End While
        baglan.Close()
        If kayit_kontrol = False Then
            Try
                baglan.Open()
                Dim ekle_komut As SqlCommand = New SqlCommand("insert into Arbeiter values('" + TextBox6.Text.ToString() + "','" + TextBox7.Text.ToString() + "','" + TextBox8.Text.ToString() + "','" + TextBox9.Text.ToString() + "','" + TextBox10.Text.ToString() + "','" + TextBox11.Text.ToString() + "','" + TextBox12.Text.ToString() + "')", baglan)
                ekle_komut.ExecuteNonQuery()
                MessageBox.Show("İşçi oluşturuldu.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                baglan.Close()
            Catch ex As Exception
                MessageBox.Show("İşçi oluşturuldu.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Error)
                baglan.Close()

            End Try
        End If
        If kayit_kontrol = True Then
            MessageBox.Show("İşçi oluşturulamadı.", "ZUG PROJECT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub 'KAYDET butonunun kodları

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            baglan.Open()
            Dim sil_komut As SqlCommand = New SqlCommand("delete from Arbeiter where Arbeiter_ID='" + TextBox6.Text + "'", baglan)
            sil_komut.ExecuteNonQuery()
            baglan.Close()
            MessageBox.Show("İşçi Silindi.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("İşçi Silinemedi.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            baglan.Close()
        End Try
    End Sub 'SİL butonunun kodları

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        baglan.Open()

        Dim update_komut As SqlCommand = New SqlCommand("update Arbeiter set Vorname='" + TextBox7.Text + "',Nachname='" + TextBox8.Text + "',Telefonnummer='" + TextBox9.Text + "',EmailAdress='" + TextBox10.Text + "',Niederlassung_ID='" + TextBox11.Text + "',Aufgabe_ID='" + TextBox12.Text + "' where Arbeiter_ID='" + TextBox6.Text + "'", baglan)

        update_komut.ExecuteNonQuery()
        baglan.Close()
        MessageBox.Show("İşçi güncellendi.", "ZUG PROJEKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)




    End Sub 'GÜNCELLE butonunun kodları

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox12.Text = ""
    End Sub 'FORMU TEMİZLE butonunun kodları

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        iscileri_listele()
        DataGridView2.Visible = True
        Button19.Visible = True

    End Sub 'VERİLERİ GÖSTER butonunun kodları

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        DataGridView2.Visible = False
        Button19.Visible = False
    End Sub ' X butonunun kodları

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        baglan.Open()
        Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
        TextBox13.Text = DataGridView1.Rows(i).Cells(0).Value.ToString()
        TextBox2.Text = DataGridView1.Rows(i).Cells(1).Value.ToString()
        TextBox3.Text = DataGridView1.Rows(i).Cells(2).Value.ToString()
        TextBox4.Text = DataGridView1.Rows(i).Cells(3).Value.ToString()
        If DataGridView1.Rows(i).Cells(4).Value.ToString() = True Then

            RadioButton1.Checked = True

        ElseIf DataGridView1.Rows(i).Cells(4).Value.ToString() = False Then

            RadioButton2.Checked = True
        End If

        DateTimePicker1.Value = DataGridView1.Rows(i).Cells(5).Value.ToString()
        TextBox14.Text = DataGridView1.Rows(i).Cells(6).Value.ToString()
        RichTextBox1.Text = DataGridView1.Rows(i).Cells(7).Value.ToString()
        TextBox5.Text = DataGridView1.Rows(i).Cells(8).Value.ToString()
        baglan.Close()
    End Sub
    Private Sub DataGridView2_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        baglan.Open()
        Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
        TextBox6.Text = DataGridView1.Rows(i).Cells(0).Value.ToString()
        TextBox7.Text = DataGridView1.Rows(i).Cells(1).Value.ToString()
        TextBox8.Text = DataGridView1.Rows(i).Cells(2).Value.ToString()
        TextBox9.Text = DataGridView1.Rows(i).Cells(3).Value.ToString()
        TextBox10.Text = DataGridView1.Rows(i).Cells(4).Value.ToString()
        TextBox11.Text = DataGridView1.Rows(i).Cells(5).Value.ToString()
        TextBox12.Text = DataGridView1.Rows(i).Cells(6).Value.ToString()
        baglan.Close()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Dim form As New giris
        form.Show()
        Me.Hide()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label21.Text = sayac & "sn"
        sayac = Val(sayac) - 1
        If sayac = 0 Then
            Timer1.Enabled = False
            Me.Close()
            MsgBox("Oturum sona erdi.")
            giris.Show()

        End If
    End Sub

End Class
