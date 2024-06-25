Public Class AddLayerForm
    Friend Shared NewLayerName As Guna.UI2.WinForms.Guna2TextBox
    Friend Shared GroundDiff As Guna.UI2.WinForms.Guna2TextBox
    Friend Shared LayerHeight As Guna.UI2.WinForms.Guna2TextBox
    Private Sub NotesForDigitBtn_Click(sender As Object, e As EventArgs) Handles NotesForDigitBtn.Click
        If Not Guna2TextBox1.Text = "" And Not Guna2TextBox3.Text = "" And Not Guna2TextBox2.Text = "" Then
            AddLayer(New Layer(Guna2TextBox1.Text, Guna2TextBox3.Text, Guna2TextBox2.Text))
            Dim m_MainForm As Form1
            m_MainForm.ContentInformationPnl.LyrList.Rows.Add(Guna2TextBox2.Text)
            m_MainForm.ActiveLayerLbl.Text = Guna2TextBox2.Text
            Me.Close()
        End If
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Me.Close()
    End Sub

    Private Sub AddLayerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NewLayerName = Guna2TextBox2
        GroundDiff = Guna2TextBox1
        LayerHeight = Guna2TextBox3
    End Sub
End Class