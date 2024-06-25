Public Class ContentBox
    Friend Shared aLyrList As Guna.UI2.WinForms.Guna2DataGridView
    Friend Shared aIndSecList As Guna.UI2.WinForms.Guna2DataGridView
    Friend Shared activeCode As Integer = 0
    Private Sub ContentBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        P3.Visible = False
        P2.Visible = False
        P1.Visible = True
        aLyrList = LyrList
        aIndSecList = IndSecList
        P31.Visible = True
        P32.Visible = False
        P33.Visible = False
        P34.Visible = False
        P31.Location = New Point(0, 56)
        P32.Location = New Point(0, 56)
        P33.Location = New Point(0, 56)
        P34.Location = New Point(0, 56)
        P31.Size = New Size(271, 307)
        P32.Size = New Size(271, 307)
        P33.Size = New Size(271, 307)
        P34.Size = New Size(271, 307)
        PA1.Visible = True
        PA2.Visible = False
        PA3.Visible = False
        PA4.Visible = False
        G1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        G2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        G3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        G4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        'Storage.
    End Sub
    Private Sub PanelViewer(sender As Object, e As EventArgs) Handles LayerBtn.Click, IndSecBtn.Click, RoomTypeBtn.Click
        If sender.Name = "LayerBtn" Then
            P3.Visible = False
            P2.Visible = False
            P1.Visible = True
        ElseIf sender.Name = "IndSecBtn" Then
            P3.Visible = False
            P2.Visible = True
            P1.Visible = False
        ElseIf sender.Name = "RoomTypeBtn" Then
            P3.Visible = True
            P2.Visible = False
            P1.Visible = False
        End If
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        AddLayerForm.Show()
    End Sub
    Function getActiveCode() As Integer
        getActiveCode = activeCode
        Return getActiveCode
    End Function
    Private Sub Guna2Button8_Click(sender As Object, e As EventArgs) Handles Guna2Button8.Click
        Try
            If LyrList.SelectedCells(0).Value IsNot Nothing Then
                AddIndSecForm.Show()
            Else
                MsgBox("Lütfen bir tane kat/katman seçiniz.")
            End If
        Catch ex As Exception
            MsgBox("Lütfen bir tane kat/katman seçiniz.")
        End Try
    End Sub

    Private Sub IndSecList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles IndSecList.CellClick
        Try
            Form1.ActiveBBLbl.Text = IndSecList.SelectedCells(0).Value
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LyrList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles LyrList.CellClick
        Try
            Form1.ActiveLayerLbl.Text = LyrList.SelectedCells(0).Value
        Catch ex As Exception

        End Try
    End Sub


    Private Sub b_Click(sender As Object, e As EventArgs) Handles b_1001.Click, b_1002.Click, b_1003.Click,
                                                                  b_1004.Click, b_1005.Click, b_1006.Click,
                                                                  b_1008.Click, b_1009.Click, b_1022.Click,
                                                                  b_1007.Click, b_1010.Click, b_1011.Click,
                                                                  b_1012.Click, b_1013.Click, b_1014.Click,
                                                                  b_1015.Click, b_1016.Click, b_1017.Click,
                                                                  b_1018.Click, b_1019.Click, b_1020.Click,
                                                                  b_1021.Click, b_1022.Click, b_1024.Click,
                                                                  b_1025.Click, b_1026.Click, b_1027.Click,
                                                                  b_1028.Click, b_1029.Click,
                                                                  b_1100.Click, b_1099.Click, b_1098.Click, b_1095.Click, b_1094.Click, b_1093.Click, 'OutBoundry, Door, Window
                                                                  b_1097.Click, b_1096.Click ' Terrace, Balcoon
        Dim getSenderName As Integer = sender.Name.Replace("b_", "")

        If getSenderName = 1100 Then
            If Not Form1.ActiveLayerLbl.Text = "X" Then
                Form1.activeLineWeight = 10
                Form1.activeColor = Color.Blue
                Form1.SelectElementModule(2)
                activeCode = 1100
                Exit Sub
            Else
                MsgBox("Lütfen bir kat/katman seçiniz.")
                activeCode = 0
                Exit Sub
            End If
        End If
        If getSenderName = 1022 Then
            If Not Form1.ActiveLayerLbl.Text = "X" Then
                Form1.activeLineWeight = 5
                Form1.activeColor = Color.Aquamarine
                Form1.SelectElementModule(2)
                activeCode = 1022
                Exit Sub
            Else
                MsgBox("Lütfen bir kat/katman seçiniz.")
                activeCode = 0
                Exit Sub
            End If
        End If
        If Not Form1.ActiveLayerLbl.Text = "X" And Not Form1.ActiveBBLbl.Text = "X" Then
            Form1.SelectElementModule(2)
            If getSenderName = 1001 Or getSenderName = 1002 Or getSenderName = 1004 Or getSenderName = 1006 Or getSenderName = 1003 Then
                Form1.activeLineWeight = 7
                Form1.activeColor = Color.Red
                activeCode = getSenderName
                Exit Sub
            End If
            If getSenderName = 1008 Then
                Form1.activeLineWeight = 7
                Form1.activeColor = Color.Yellow
                activeCode = getSenderName
                Exit Sub
            End If
            If getSenderName = 1005 Then
                Form1.activeLineWeight = 7
                Form1.activeColor = Color.Brown
                activeCode = getSenderName
                Exit Sub
            End If
            If getSenderName = 1096 Or getSenderName = 1097 Then
                Form1.activeLineWeight = 7
                Form1.activeColor = Color.Beige
                If getSenderName = 1097 Then
                    Form1.activeColor = Color.LimeGreen
                End If
                activeCode = getSenderName
                Exit Sub
            End If
            If getSenderName = 1099 Or getSenderName = 1098 Or getSenderName = 1095 Or getSenderName = 1094 Or getSenderName = 1093 Then
                '-------DÜZENLE - ELEMENT KAYDEDERKEN HANGİ TİP OLDUĞUNU YAZACAK.
                Form1.SelectElementModule(1)
                Form1.activeLineWeight = 4
                Form1.activeColor = Color.Orange
                If getSenderName = 1098 Then
                    Form1.activeColor = Color.Green
                End If
                activeCode = getSenderName
                Exit Sub
            End If
        Else
            MsgBox("Lütfen bir kat/katman veya bağımsız bölüm seçiniz.")
            activeCode = 0
            Exit Sub
        End If
    End Sub
    Private Sub MainClick(sender As Object, e As EventArgs) Handles G1.Click, G2.Click, G3.Click, G4.Click

        If sender.Name = "G1" Then
            P31.Visible = True
            P32.Visible = False
            P33.Visible = False
            P34.Visible = False
            PA1.Visible = True
            PA2.Visible = False
            PA3.Visible = False
            PA4.Visible = False
        ElseIf sender.Name = "G2" Then
            P31.Visible = False
            P32.Visible = True
            P33.Visible = False
            P34.Visible = False
            PA1.Visible = False
            PA2.Visible = True
            PA3.Visible = False
            PA4.Visible = False
        ElseIf sender.Name = "G3" Then
            P31.Visible = False
            P32.Visible = False
            P33.Visible = True
            P34.Visible = False
            PA1.Visible = False
            PA2.Visible = False
            PA3.Visible = True
            PA4.Visible = False
        ElseIf sender.Name = "G4" Then
            P31.Visible = False
            P32.Visible = False
            P33.Visible = False
            P34.Visible = True
            PA1.Visible = False
            PA2.Visible = False
            PA3.Visible = False
            PA4.Visible = True
        End If
        MainClickFont(sender)
    End Sub
    Sub MainClickFont(sender As Object)
        If sender.Name = "G1" Then
            G1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        ElseIf sender.Name = "G2" Then
            G1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        ElseIf sender.Name = "G3" Then
            G1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G3.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        ElseIf sender.Name = "G4" Then
            G1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            G4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        End If
    End Sub


End Class
