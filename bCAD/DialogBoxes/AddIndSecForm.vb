Imports Guna.UI2.WinForms

Public Class AddIndSecForm
    Private Sub AddIndSecForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Form1.ActiveLayerLbl.Text = "X" Then
            LayerNameTB.Text = Form1.ActiveLayerLbl.Text
        Else
            MsgBox("Bir kat/katman seçin ve işleme daha sonra devam edin.")
            Me.Close()
        End If
    End Sub
    Private Sub NotesForDigitBtn_Click(sender As Object, e As EventArgs) Handles NotesForDigitBtn.Click
        If Not MaksIDTb.Text = "" And Not ZmRfTb.Text = "" And Not LayerNameTB.Text = "" And Not IndSecNmTb.Text = "" Then
            Dim atmpLyr As Layer = SelectLayer(LayerNameTB.Text)
            If atmpLyr IsNot Nothing Then
                AddIndependentSection(atmpLyr, IndSecNmTb.Text, MaksIDTb.Text, ZmRfTb.Text)
                Dim m_MainForm As Form1
                m_MainForm.ContentInformationPnl.IndSecList.Rows.Add(IndSecNmTb.Text)
                Me.Close()
            Else
                MsgBox("Kat veya katman hatalı idi. Tekrar deneyin.")
            End If
        End If
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Me.Close()
    End Sub

End Class