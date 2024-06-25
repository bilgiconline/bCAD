Public Class ColorPicker
    Private Sub ColorPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Me.TopMost = False
    End Sub
    Private Sub ColorPicker_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Me.Visible = False
        Me.TopMost = False
    End Sub
    Private Sub ColorBoxForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Visible = False
    End Sub
    Private Sub ColorBoxForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Opacity = 100
        Me.TopMost = True
        Me.Visible = True
    End Sub
    Private Sub Answer_Click(sender As Object, e As EventArgs) Handles Button1.Click,
    Button10.Click, Button11.Click, Button12.Click, Button2.Click, Button3.Click, Button4.Click,
    Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button10.Click
        Dim btn As Button = CType(sender, Button)
        Form1.ColorPickBtn.BackColor = btn.BackColor
        Form1.ColorPickBtn.FillColor = btn.BackColor
        Form1.ColorPickBtn.FillColor2 = btn.BackColor
        Form1.activeColor = btn.BackColor
    End Sub
End Class