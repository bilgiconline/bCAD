Public Class ItemInformation
    Private Sub ItemInformation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        P1.Visible = True
        P2.Visible = False
        P3.Visible = False
    End Sub
    Private Sub PanelViewer(sender As Object, e As EventArgs) Handles Btn1.Click, Btn2.Click, Btn3.Click
        If sender.Name = "Btn1" Then
            P1.Visible = True
            P2.Visible = False
            P3.Visible = False
        ElseIf sender.Name = "Btn2" Then
            P1.Visible = False
            P2.Visible = True
            P3.Visible = False
        ElseIf sender.Name = "Btn3" Then
            P1.Visible = False
            P2.Visible = False
            P3.Visible = True
        End If
    End Sub
End Class
