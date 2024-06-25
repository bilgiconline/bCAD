Imports System.Drawing.Drawing2D
Imports System.IO
Imports bCAD.YLScsDrawing.Imaging.Filters
Imports System.Numerics
Imports System.Security.Cryptography
Imports TheArtOfDevHtmlRenderer
Imports System.Drawing.Drawing2D.Matrix
Imports System.Runtime.Remoting
Imports Accord.Imaging.Filters
Imports Accord.Math


Public Class Form1
    Private zoomstart As Point
    Private zoomfirst As Point
    Private zoomrect As Rectangle
    Private ReadOnly maxzoom As Decimal = 200
    Private ReadOnly minzoom As Decimal = 0.001
    Private WithEvents TmrMarch As New Timer
    Private MarchOffset As Integer = 0
    Private OffsetDelta As Double = 0.5
    Private DashPattern() As Single = {5, 5}

    Friend Shared zoom As Decimal = 1
    Private startx As Integer = 0
    Private starty As Integer = 0
    Private offsetx As Integer = 0
    Private offsety As Integer = 0
    Private mouseDownPt As Point
    Private initialwidth As Integer
    Dim coordY As Integer = 0
    Dim coordX As Integer = 0
    Dim oldZoomFactork As Decimal = 1
    Public WithEvents Canvas1 As New Canvas
    Dim grCnvs As Graphics

    Friend Shared SnappedList As New List(Of Point)
    Friend Shared isInRangeList As New List(Of Object)
    Friend Shared activeColor As Color = Color.Blue
    Friend Shared activeLineWeight As Integer = 3
    Dim isDown As Boolean = False
    Dim initialX, initialY As Integer
    Dim ClickInitialX, ClickinitialY As Integer
    Dim clickOffsetX, clickOffsetY As Integer
    Private clickZoom As Decimal
    Dim start As Boolean = False
    Dim rect As Rectangle
    Dim line As Line
    Dim shape As Shape
    Dim aGrph As Graphics
    Dim aPen As Pen = New Pen(Color.Blue, 3)
    Dim aListRect As New List(Of Rectangle)
    Dim aListLine As New List(Of String)
    Dim isLine, isRectangle, isShape As Boolean
    Dim ListLine As New List(Of Line)
    Dim shapeList As New List(Of Shape)
    Dim shapePathList As New List(Of Point)
    Dim bitmap As Bitmap
    Dim firstStart As Boolean
    Dim LastSnapPoint As Point
    Dim lastCoordinate As Point

    Dim rectClick As Integer = 0
    Dim drawnRectElements As New List(Of Point)
    Dim drawSupportOrtho As Boolean = True
    Dim ColorPickerForm As ColorPicker
    Dim isDraw As Boolean = False
    Friend Shared ItemInformationPnl As New ItemInformation
    Friend Shared ContentInformationPnl As New ContentBox
    Dim ItemInformationPnlVisible As Boolean = False
    Dim ContentInformationPnlVisible As Boolean = False

    Friend Shared ActiveLayerLbl As Label
    Friend Shared ActiveBBLbl As Label

    Dim addVertexClick As Boolean = False
    Dim addVertexPoint As Point
    Dim mouseMoveVertexPoints As Point()
    Dim addedVertex As Boolean = False

    Dim deleteVertexClick As Boolean = False

    Dim moveVertex As Boolean = False
    Dim moveVertices() As Point
    Dim moveVertexActive As Boolean = False
    Dim moveVertexList As New List(Of Point)

    Dim moveElement As Boolean = False
    Dim moveElementVertices As New List(Of Point)
    Dim moveElementActive As Boolean = False
    Dim moveElementDiff As Point
    Dim moveElementClickVertex As Point
    Dim vertexDiffList As List(Of Point)

    Dim addedTif As Boolean = False
    Dim warpStart As Boolean = False
    Dim warpPointList As New List(Of Point)



    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        ClosestLocation_WithThreePoints()
    End Sub
    Private Enum T_MouseAction
        RectangleZooming
        Panning
        None
    End Enum
    Private MouseAction As T_MouseAction = T_MouseAction.None
    Friend Sub SelectElementModule(sender As Integer)
        isDraw = True
        If sender = 1 Then
            isRectangle = True
            isLine = False
            isShape = False
        ElseIf sender = 2 Then
            isRectangle = False
            isLine = False
            isShape = True
        Else
            isRectangle = False
            isLine = True
            isShape = False
        End If
    End Sub
    Private Sub SelectShape(sender As Object, e As EventArgs) Handles SelectLineBtn.Click, SelectRectBtn.Click, SelectShapeBtn.Click
        isDraw = True
        If sender.Name = "SelectRectBtn" Then
            isRectangle = True
            isLine = False
            isShape = False
        ElseIf sender.Name = "SelectShapeBtn" Then
            isRectangle = False
            isLine = False
            isShape = True
        Else
            isRectangle = False
            isLine = True
            isShape = False
        End If
    End Sub
    Sub SharedFormContainers()
        ActiveLayerLbl = ActiveLayerLb
        ActiveBBLbl = ActiveBBLb
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Canvas1.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Width)
        Canvas1.AutoScroll = False
        initialwidth = Canvas1.Width
        Canvas1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Panel1.Controls.Add(Canvas1)
        Panel1.Visible = True
        Canvas1.BackColor = Color.Black
        ClickInitialX = 0
        ClickinitialY = 0
        clickZoom = 1
        cX.Text = "0,0"
        activeColor = aPen.Color
        ItemInformationPnl.BringToFront()
        ItemInformationPnl.BringToFront()
        ItemInformationPnl.BringToFront()
        ItemInformationPnl.Location = New Point(Guna2Button6.Location.X, Guna2Button6.Location.Y + 25)
        ItemInformationPnl.Visible = False
        Me.Controls.Add(ItemInformationPnl)
        ItemInformationPnl.BringToFront()
        ItemInformationPnl.BringToFront()
        ItemInformationPnl.BringToFront()

        ContentInformationPnl.BringToFront()
        ContentInformationPnl.BringToFront()
        ContentInformationPnl.BringToFront()
        ContentInformationPnl.Location = New Point(ContentBtn.Location.X + 33, ContentBtn.Location.Y)
        ContentInformationPnl.Visible = False
        Me.Controls.Add(ContentInformationPnl)
        ContentInformationPnl.BringToFront()
        ContentInformationPnl.BringToFront()
        ContentInformationPnl.BringToFront()
        SharedFormContainers()
        ' dd()
    End Sub
    Sub DrawAllElements(ByVal gr As Graphics)
        For i = 0 To aListRect.Count - 1
            Dim NewRect As Rectangle
            aPen.Color = activeColor
            NewRect = aListRect(i)
            gr.DrawRectangle(aPen, NewRect)
        Next
        If SelectLayer(ActiveLayerLbl.Text) IsNot Nothing Then
            shapeList = GetDrawnShapeActiveLayerItems(ActiveLayerLbl.Text)
            'shapeList = GetDrawnActiveLayerItems(ActiveLayerLbl.Text)
            For i = 0 To shapeList.Count - 1
                If CDbl(shapeList(i).Area / (1000 ^ 2)) > 1 Then
                    Using myFont As New Font("Zipper", 200, FontStyle.Regular)
                        Dim myBrush As Brush = Brushes.Red
                        Dim line1 As String = CStr(Math.Round(CDbl(shapeList(i).Area / (1000 ^ 2)), 2))
                        Dim format As New StringFormat With {
                            .Alignment = StringAlignment.Far
                        }
                        '  Debug.Print(shapeList(i).centroid.X & "," & shapeList(i).centroid.Y)
                        gr.DrawString(line1, myFont, myBrush, shapeList(i).centroid.X, -1 * shapeList(i).centroid.Y)
                        ' gr.DrawString(i.ToString, myFont, myBrush, shapeList(i).centroid.X, -1 * shapeList(i).centroid.Y)
                        myFont.Dispose()
                    End Using
                End If
            Next
            shapeList.AddRange(GetActiveRoomComponents(ActiveLayerLbl.Text))
        End If
        Try
            If warpPointList.Count - 1 > 0 Then
                For w = 1 To warpPointList.Count - 1
                    Dim aPt1 As New Point(warpPointList(w))
                    If w Mod 2 = 0 Then
                        gr.FillEllipse(Brushes.Orange, CInt(aPt1.X - (10 / zoom)), CInt(aPt1.Y - (10 / zoom)), 20 / zoom, 20 / zoom)
                    Else
                        gr.FillEllipse(Brushes.Green, CInt(aPt1.X - (10 / zoom)), CInt(aPt1.Y - (10 / zoom)), 20 / zoom, 20 / zoom)
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
        For i = 0 To shapeList.Count - 1
            Dim aNewPen As Pen = New Pen(shapeList(i).color, shapeList(i).lineWeight * 5)
            If shapeList(i).color = Color.Orange Or shapeList(i).color = Color.Green Then
                Dim newBrush As SolidBrush = New SolidBrush(shapeList(i).color)
                gr.FillPolygon(newBrush, shapeList(i).getVerticies)
            Else
                Try
                    gr.DrawPolygon(aNewPen, shapeList(i).getVerticies)
                Catch ex As Exception

                End Try

            End If

        Next

        If shapePathList.Count > 1 Then
            For i = 0 To shapePathList.Count - 1
                aPen.Color = activeColor
                aPen.Width = activeLineWeight
                gr.DrawLines(aPen, shapePathList.ToArray)
            Next
        End If

        For i As Integer = 0 To ListLine.Count - 1
            Dim P1 As Integer = ListLine(i).startPointX
            Dim P2 As Integer = ListLine(i).startPointY
            Dim P3 As Integer = ListLine(i).endPointX
            Dim P4 As Integer = ListLine(i).endPointY
            aPen.Color = ListLine(i).color
            gr.DrawLine(aPen, P1, P2, P3, P4)
        Next
        If rectClick = 2 Then
            If drawnRectElements.Count > 0 Then
                If drawnRectElements.Count - 1 = 1 Then
                    gr.DrawLine(aPen, drawnRectElements(0), drawnRectElements(1))
                End If
            End If
        End If
        If isInRangeList.Count > 0 Then
            If TypeOf isInRangeList(0) Is Line Then
                Dim tmpLineElement As Line
                tmpLineElement = isInRangeList(0)
                Dim tmpPen As Pen = New Pen(Color.Purple, tmpLineElement.lineWeight * 4)
                Dim startPoint As New Point
                Dim endPoint As New Point
                startPoint.X = tmpLineElement.startPointX
                startPoint.Y = tmpLineElement.startPointY
                endPoint.X = tmpLineElement.endPointX
                endPoint.Y = tmpLineElement.endPointY
                gr.DrawLine(tmpPen, startPoint, endPoint)
            ElseIf TypeOf isInRangeList(0) Is Shape Then
                Dim tmpShapeElement As Shape
                tmpShapeElement = isInRangeList(0)
                Dim tmpPen As Pen = New Pen(Color.Purple, tmpShapeElement.lineWeight * 4)
                gr.DrawPolygon(tmpPen, tmpShapeElement.getVerticies)
            End If
        End If
        If SnappedList.Count > 0 Then
            Dim aPt1 As New Point(SnappedList(0))
            Dim newtmpPen As Pen = New Pen(Color.Red, 3)
            gr.DrawEllipse(newtmpPen, aPt1.X - (10 / zoom), -1 * aPt1.Y - (10 / zoom), 20 / zoom, 20 / zoom)
        End If
        gr.DrawEllipse(Pens.Blue, -5, -5, 10, 10)
        Using myFont As New Font("Zipper", 15, FontStyle.Regular)
            Dim myBrush As Brush = Brushes.Red
            Dim line1 As String = "(" & "0 , 0" & ")"
            Dim format As New StringFormat With {
                .Alignment = StringAlignment.Far
            }
            gr.DrawString(line1, myFont, myBrush, 0, 0)
        End Using
    End Sub
    Private Sub Canvas1_MouseClick(sender As Object, e As MouseEventArgs) Handles Canvas1.MouseClick
        If e.Button = MouseButtons.Middle Then
            Exit Sub
        End If
        If e.Button = MouseButtons.Right Then
            start = False
            addVertexClick = False
            addedVertex = False
            deleteVertexClick = False
            moveVertex = False
            moveElement = False
            moveVertexActive = False
            moveElementActive = False
            isDraw = False
            rectClick = 0
            drawnRectElements.Clear()
            moveElementVertices.Clear()
            isInRangeList.Clear()
            warpStart = False
            warpPointList.Clear()
            InfoLbl.Text = "İşlem iptal edildi."
            Exit Sub
        End If
        If warpStart = True Then
            isDraw = False
            If warpPointList.Count = 0 Then
                InfoLbl.Text = "Affin dönüşümüne başlandı"
                warpPointList.Add(New Point(0, 0))
            ElseIf warpPointList.Count - 1 < 8 And Not warpPointList.Count = 0 Then
                If warpPointList.Count Mod 2 = 0 Then
                    InfoLbl.Text = warpPointList.Count + 1 & ". nokta. Raster üzerinde bir nokta belirleyin."
                    warpPointList.Add(New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1))))
                Else
                    InfoLbl.Text = warpPointList.Count + 1 & ". nokta. Referans bir nokta belirleyin."
                    warpPointList.Add(New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1))))
                End If
            ElseIf warpPointList.Count = 9 Then
                InfoLbl.Text = "Son nokta. Referans bir nokta belirleyin."
                '   WarpMethod()
                'CalculateTfwMatrix()
                dd()
                'WriteTfwFileFromCoords()
                warpStart = False
                Exit Sub
            End If
        End If
        If isDraw = True Then
            warpStart = False
            If start = False Then
                If LastSnapPoint.IsEmpty = True Then
                    initialX = e.X
                    initialY = e.Y
                Else
                    initialX = TransformatorForCanvasX(LastSnapPoint.X)
                    initialY = TransformatorForCanvasY(LastSnapPoint.Y)
                    If isShape = True Then
                        If shape Is Nothing Then
                            shape = New Shape
                            shape.AddVertex(LastSnapPoint.X, -1 * LastSnapPoint.Y)
                            shapePathList.Clear()
                            shapePathList.Add(New Point(LastSnapPoint.X, -1 * LastSnapPoint.Y))
                        End If
                    End If
                End If

                ClickInitialX = initialX '(e.X - offsetx) / zoom
                ClickinitialY = initialY '(e.Y - offsety) / zoom
                clickOffsetX = offsetx
                clickOffsetY = offsety
                clickZoom = zoom
                oldZoomFactork = zoom
                oldZoomLbl.Text = oldZoomFactork
                ' Canvas1.Refresh()
                start = True
                If isLine = True And isRectangle = False And isShape = False Then
                    If line Is Nothing Then line = New Line
                    If LastSnapPoint.IsEmpty = True Then
                        line.startPointX = TakeOriginalCoordinateX(e.X)
                        line.startPointY = TakeOriginalCoordinateY(e.Y)
                    Else
                        line.startPointX = LastSnapPoint.X
                        line.startPointY = -1 * LastSnapPoint.Y
                        '  initialX = TransformatorForCanvasX(LastSnapPoint.X)
                        initialX = TransformatorForCanvasX(LastSnapPoint.X)
                        '   initialY = TransformatorForCanvasY(-1 * LastSnapPoint.Y)
                        initialY = TransformatorForCanvasY(LastSnapPoint.Y)
                        ClickInitialX = initialX '(e.X - offsetx) / zoom
                        ClickinitialY = initialY '(e.Y - offsety) / zoom
                    End If
                    line.lineWeight = activeLineWeight
                    line.color = activeColor
                    '   Debug.Print(e.X & "|" & e.Y)
                    rect = Nothing
                    shape = Nothing
                ElseIf isLine = False And isRectangle = True And isShape = False Then
                    If rectClick = 0 Then
                        drawnRectElements.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                        rectClick += 1
                    End If
                    line = Nothing
                    shape = Nothing
                ElseIf isLine = False And isRectangle = False And isShape = True Then
                    If shape Is Nothing Then shape = New Shape
                    shape.startPointX = TakeOriginalCoordinateX(e.X)
                    shape.startPointY = TakeOriginalCoordinateY(e.Y)
                    shape.AddVertex(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y))
                    shapePathList.Clear()
                    shape.color = activeColor
                    shapePathList.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                    line = Nothing
                    rect = Nothing
                End If
            Else
                If isLine = False And isRectangle = True And isShape = False Then
                    line = Nothing
                    rect = Nothing
                    If rectClick = 1 Then
                        If drawSupportOrtho = True Then
                            Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                            If Not newP = New Point(e.X, e.Y) Then
                                drawnRectElements.Add(New Point(TakeOriginalCoordinateX(newP.X), TakeOriginalCoordinateY(newP.Y)))
                                initialX = newP.X
                                initialY = newP.Y
                                ClickinitialY = newP.Y
                                ClickInitialX = newP.X
                            Else
                                drawnRectElements.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                                initialX = e.X
                                initialY = e.Y
                                ClickinitialY = e.Y
                                ClickInitialX = e.X
                            End If
                        Else
                            drawnRectElements.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                            initialX = e.X
                            initialY = e.Y
                            ClickinitialY = e.Y
                            ClickInitialX = e.X
                        End If
                        rectClick += 1
                    ElseIf rectClick = 2 Then
                        If drawSupportOrtho = True Then
                            Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                            If Not newP = New Point(e.X, e.Y) Then
                                drawnRectElements.Add(New Point(TakeOriginalCoordinateX(newP.X), TakeOriginalCoordinateY(newP.Y)))
                                Dim LastPoint As Point = GetRectangleLastPoint(New Point(TakeOriginalCoordinateX(newP.X), TakeOriginalCoordinateY(newP.Y)))
                                drawnRectElements.Add(LastPoint)
                                initialX = newP.X
                                initialY = newP.Y
                                ClickinitialY = newP.Y
                                ClickInitialX = newP.X
                            Else
                                drawnRectElements.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                                Dim LastPoint As Point = GetRectangleLastPoint(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                                drawnRectElements.Add(LastPoint)
                                initialX = e.X
                                initialY = e.Y
                                ClickinitialY = e.Y
                                ClickInitialX = e.X
                            End If
                        Else
                            drawnRectElements.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                            Dim LastPoint As Point = GetRectangleLastPoint(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                            drawnRectElements.Add(LastPoint)
                            initialX = e.X
                            initialY = e.Y
                            ClickinitialY = e.Y
                            ClickInitialX = e.X
                        End If
                        Dim atmpShape As New Shape
                        For k = 0 To drawnRectElements.Count - 1
                            If k = 0 Then
                                atmpShape.startPointX = drawnRectElements(k).X
                                atmpShape.startPointY = drawnRectElements(k).Y
                                atmpShape.endPointX = drawnRectElements(k).Y
                                atmpShape.endPointX = drawnRectElements(k).Y
                            End If
                            atmpShape.AddVertex(drawnRectElements(k).X, drawnRectElements(k).Y)
                        Next
                        atmpShape.AddVertex(drawnRectElements(0).X, drawnRectElements(0).Y)
                        atmpShape.color = activeColor
                        atmpShape.lineWeight = activeLineWeight
                        shapeList.Add(atmpShape)
                        If ContentInformationPnl.getActiveCode = 1099 Or ContentInformationPnl.getActiveCode = 1098 Or ContentInformationPnl.getActiveCode = 1095 _
                            Or ContentInformationPnl.getActiveCode = 1094 _
                            Or ContentInformationPnl.getActiveCode = 1093 Then
                            AddComponents(ActiveLayerLbl.Text, atmpShape, ContentInformationPnl.getActiveCode)
                        Else
                            AddRoom(ActiveLayerLbl.Text, ActiveBBLbl.Text, False, ContentInformationPnl.getActiveCode, atmpShape)
                        End If
                        rectClick = 0
                        drawnRectElements.Clear()
                        start = False
                    End If
                ElseIf isLine = True And isRectangle = False And isShape = False Then
                    If LastSnapPoint.IsEmpty = True Then
                        If drawSupportOrtho = True Then
                            Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                            If Not newP = New Point(e.X, e.Y) Then
                                aGrph.DrawLine(aPen, initialX, initialY, newP.X, newP.Y)
                                line.endPointX = TakeOriginalCoordinateX(newP.X)
                                line.endPointY = TakeOriginalCoordinateY(newP.Y)
                            Else
                                aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                                line.endPointX = TakeOriginalCoordinateX(e.X)
                                line.endPointY = TakeOriginalCoordinateY(e.Y)
                            End If
                        Else
                            aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                            line.endPointX = TakeOriginalCoordinateX(e.X)
                            line.endPointY = TakeOriginalCoordinateY(e.Y)
                        End If
                    Else
                        line.endPointX = LastSnapPoint.X
                        line.endPointY = -1 * LastSnapPoint.Y
                    End If
                    line.lineWeight = activeLineWeight
                    line.color = activeColor
                    ListLine.Add(line)
                    line = New Line
                    'Nothing
                    start = False
                ElseIf isLine = False And isRectangle = False And isShape = True Then
                    If e.Button = MouseButtons.Right Then
                        shapePathList.Clear()
                        shape = New Shape
                        shape = Nothing
                        start = False
                        Exit Sub
                    End If
                    If IsLastPoint(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y), shape.startPointX, shape.startPointY) = True Then
                        shape.endPointX = shape.startPointX
                        shape.endPointY = shape.startPointY
                        shape.AddVertex(shape.endPointX, shape.endPointY)
                        shape.color = activeColor
                        shape.lineWeight = activeLineWeight
                        shapeList.Add(shape)
                        If ContentInformationPnl.getActiveCode = 1099 Or ContentInformationPnl.getActiveCode = 1098 Or ContentInformationPnl.getActiveCode = 1095 _
                            Or ContentInformationPnl.getActiveCode = 1094 _
                            Or ContentInformationPnl.getActiveCode = 1093 Then
                            AddComponents(ActiveLayerLbl.Text, shape, ContentInformationPnl.getActiveCode)
                        ElseIf ContentInformationPnl.getActiveCode = 1097 Or ContentInformationPnl.getActiveCode = 1096 Then
                            If ContentInformationPnl.getActiveCode = 1097 Then
                                AddOuterBuild(ActiveLayerLbl.Text, shape, True)
                            Else
                                AddOuterBuild(ActiveLayerLbl.Text, shape, False)
                            End If
                        Else
                            AddRoom(ActiveLayerLbl.Text, ActiveBBLbl.Text, False, ContentInformationPnl.getActiveCode, shape)
                        End If
                        shape = New Shape
                        shape = Nothing

                        shapePathList.Clear()
                        start = False
                    Else
                        If LastSnapPoint.IsEmpty = True Then
                            If drawSupportOrtho = True Then
                                Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                                If Not newP = New Point(e.X, e.Y) Then
                                    shape.AddVertex(TakeOriginalCoordinateX(newP.X), TakeOriginalCoordinateY(newP.Y))
                                    shapePathList.Add(New Point(TakeOriginalCoordinateX(newP.X), TakeOriginalCoordinateY(newP.Y)))
                                    initialX = newP.X
                                    initialY = newP.Y
                                    ClickinitialY = newP.Y
                                    ClickInitialX = newP.X
                                Else
                                    shape.AddVertex(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y))
                                    shapePathList.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                                    initialX = e.X
                                    initialY = e.Y
                                    ClickinitialY = e.Y
                                    ClickInitialX = e.X
                                End If
                            Else
                                shape.AddVertex(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y))
                                shapePathList.Add(New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                                initialX = e.X
                                initialY = e.Y
                                ClickinitialY = e.Y
                                ClickInitialX = e.X
                            End If
                        Else
                            shape.AddVertex(LastSnapPoint.X, -1 * LastSnapPoint.Y)
                            shapePathList.Add(New Point(LastSnapPoint.X, -1 * LastSnapPoint.Y))
                            initialX = TransformatorForCanvasX(LastSnapPoint.X)
                            '   initialY = TransformatorForCanvasY(-1 * LastSnapPoint.Y)
                            initialY = TransformatorForCanvasY(LastSnapPoint.Y)
                            ' initialX = LastSnapPoint.X
                            '  initialY = LastSnapPoint.Y
                            ClickinitialY = initialY
                            ClickInitialX = initialX
                        End If
                        clickZoom = zoom
                        clickOffsetX = offsetx
                        clickOffsetY = offsety
                    End If
                End If
            End If
        ElseIf addVertexClick = True Then
            If mouseMoveVertexPoints IsNot Nothing Then
                If TypeOf isInRangeList(0) Is Shape Then
                    Dim activetmpShape As Shape
                    activetmpShape = isInRangeList(0)
                    For inspt = 0 To UBound(activetmpShape.getVerticies)
                        If activetmpShape.getVerticies(inspt) = mouseMoveVertexPoints(0) Then
                            activetmpShape.AddVertex(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y), inspt + 1)
                            Exit For
                        End If
                        'renewshape
                    Next
                    ' 
                End If
                addedVertex = False
                addVertexClick = False
                deleteVertexClick = False
                moveVertex = False
                warpStart = False
                moveVertexActive = False
                '  Array.Clear(mouseMoveVertexPoints, 0, mouseMoveVertexPoints.Length)
                Erase mouseMoveVertexPoints
            Else
                If isInRangeList.Count > 0 Then
                    If TypeOf isInRangeList(0) Is Shape Then
                        Dim activetmpShape As Shape
                        addVertexClick = True
                        deleteVertexClick = False
                        start = False
                        isDraw = False
                        activetmpShape = isInRangeList(0)
                        addVertexPoint = New Point(TakeOriginalCoordinateX(e.X), -1 * TakeOriginalCoordinateY(e.Y))
                        mouseMoveVertexPoints = GetBetweenCoordinatesInShape(activetmpShape.getVerticies, addVertexPoint).ToArray
                        addedVertex = True
                        moveVertex = False
                        moveVertexActive = False
                        ' addVertexClick = False
                    Else
                        MsgBox("Seçilmiş bir kapalı alan bulamadık, lütfen bir kapalı alan seçiniz.")
                        addVertexClick = False
                        Erase mouseMoveVertexPoints
                        deleteVertexClick = False
                        moveVertex = False
                        moveVertexActive = False
                    End If
                Else
                    MsgBox("Seçilmiş bir kapalı alan bulamadık, lütfen bir kapalı alan seçiniz.")
                    addVertexClick = False
                    Erase mouseMoveVertexPoints
                    deleteVertexClick = False
                    moveVertex = False
                    moveVertexActive = False
                End If
            End If
        ElseIf deleteVertexClick = True Then
            If isInRangeList.Count > 0 Then
                If TypeOf isInRangeList(0) Is Shape Then
                    Dim activetmpShape As Shape
                    addVertexClick = False
                    start = False
                    isDraw = False
                    activetmpShape = isInRangeList(0)
                    If activetmpShape.getVerticies.ToList.Count - 1 > 3 Then
                        activetmpShape.DeleteVertex(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y))
                    End If
                End If
            End If
            moveVertex = False
            moveVertexActive = False
            deleteVertexClick = True
            addVertexClick = False
            start = False
            isDraw = False
            warpStart = False
        ElseIf moveVertex = True Then
            deleteVertexClick = False
            addVertexClick = False
            start = False
            warpStart = False
            isDraw = False
            If moveVertexActive = False Then
                If isInRangeList.Count > 0 Then
                    If TypeOf isInRangeList(0) Is Shape Then
                        Dim activetmpShape As Shape
                        addVertexClick = False
                        start = False
                        isDraw = False
                        activetmpShape = isInRangeList(0)
                        moveVertexList = GetVertices(activetmpShape)
                        If moveVertexList.Count > 0 Then
                            moveVertexActive = True
                        Else
                            moveVertexActive = False
                        End If
                    End If
                End If
            Else
                If isInRangeList.Count > 0 Then
                    If TypeOf isInRangeList(0) Is Shape Then
                        Dim activetmpShape As Shape
                        addVertexClick = False
                        start = False
                        isDraw = False
                        activetmpShape = isInRangeList(0)
                        activetmpShape.RenewVertex(moveVertexList(2), New Point(TakeOriginalCoordinateX(e.X), TakeOriginalCoordinateY(e.Y)))
                        moveVertexList.Clear()
                        moveVertexActive = False
                    End If
                End If
            End If
        ElseIf moveElement = True Then
            moveVertexActive = False
            moveVertex = False
            deleteVertexClick = False
            addVertexClick = False
            moveElement = True
            warpStart = False
            start = False
            isDraw = False
            If moveElementActive = False Then
                If isInRangeList.Count > 0 Then
                    If TypeOf isInRangeList(0) Is Shape Then
                        Dim activetmpShape As Shape
                        activetmpShape = isInRangeList(0)
                        moveElementVertices = GetVertices(activetmpShape)
                        If moveElementVertices.Count > 0 Then
                            moveElementClickVertex = moveElementVertices(2)
                            vertexDiffList = DiffOtherPoints(activetmpShape, moveElementClickVertex)
                            moveElementActive = True
                        Else
                            moveElementActive = False
                            moveElementVertices.Clear()
                        End If
                    Else
                        moveElementVertices.Clear()
                        moveElementActive = False
                    End If
                Else
                    moveElementVertices.Clear()
                    moveElementActive = False
                End If
            Else
                If TypeOf isInRangeList(0) Is Shape Then
                    Dim activetmpShape As Shape
                    activetmpShape = isInRangeList(0)
                    MoveShape(activetmpShape, moveElementClickVertex, New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1))))
                    moveElementActive = False
                End If
                '  MoveShape
            End If
        Else
            moveElementVertices.Clear()
            moveVertexList.Clear()
            moveVertexActive = False
            moveVertex = False
            deleteVertexClick = False
            addVertexClick = False
            moveElement = False
            start = False
            '  warpStart = False
            isDraw = False
            SelectElement()
        End If
    End Sub
    Private Sub Canvas1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Canvas1.MouseMove
        If e.Button = MouseButtons.Middle Then
            Cursor = Cursors.Hand
            Dim mousePosNow As Point = e.Location
            Dim deltaX, deltaY As Integer
            deltaX = CInt((mousePosNow.X - mouseDownPt.X) / zoom)
            deltaY = CInt((mousePosNow.Y - mouseDownPt.Y) / zoom)
            offsetx = CInt(startx + deltaX)
            offsety = CInt(starty + deltaY)
            Canvas1.Refresh()
            Exit Sub
        End If

        If lastCoordinate.X = e.X And lastCoordinate.Y = e.Y Then
            Exit Sub
        Else
            lastCoordinate = New Point(e.X, e.Y)
        End If
        CanvasCoor.Text = "(" & e.X & " , " & e.Y & ")"
        ofX.Text = CStr(offsetx) & " , " & CStr(offsety)
        pX.Text = "(" & CInt(e.X + offsetx) & " , " & e.Y + offsety & ")"
        Dim tmpValue1 As Integer
        tmpValue1 = (offsetx * (zoom - 1)) + offsetx
        Dim tmpValue2 As Integer
        tmpValue2 = (offsety * (zoom - 1)) + offsety
        Dim activeCoordinatX = (e.X) - tmpValue1
        Dim activeCoordinatY = ((e.Y - tmpValue2) * -1)
        activeCoordinatX = activeCoordinatX / zoom
        activeCoordinatY = activeCoordinatY / zoom
        cX.Text = CStr((activeCoordinatX) & "," & CStr(activeCoordinatY))
        Cxx.Text = CStr(Math.Round(activeCoordinatX / 10000, 3)) & "," & CStr(Math.Round(activeCoordinatY / 10000, 3))
        If e.Button = MouseButtons.Right Then
            start = False
            Exit Sub
        End If
        'If BackgroundWorker1.IsBusy = False Then
        '    BackgroundWorker1.RunWorkerAsync()
        'End If
        '     RectIsInRange(activeCoordinatX, activeCoordinatY)


        '//////////////////////New, will test.
        If Distance2Pts(LastSnapPoint, New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1)))) > 70 Then
            SnapPoint()
        End If
        '/////////////////////



        If start = True Then
            If isLine = False And isRectangle = True And isShape = False Then
                Canvas1.Refresh()
                'rect = New Rectangle(Math.Min(e.X, initialX),
                '               Math.Min(e.Y, initialY),
                '               Math.Abs(e.X - initialX),
                '               Math.Abs(e.Y - initialY))
                aGrph = Canvas1.CreateGraphics
                aPen.Width = activeLineWeight
                aPen.Color = activeColor

                If rectClick = 0 Then

                ElseIf rectClick = 1 Then
                    If drawSupportOrtho = True Then
                        Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                        If Not newP = New Point(e.X, e.Y) Then
                            aGrph.DrawLine(aPen, initialX, initialY, newP.X, newP.Y)
                        Else
                            aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                        End If
                    Else
                        aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                    End If
                ElseIf rectClick = 2 Then
                    If drawSupportOrtho = True Then
                        Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                        If Not newP = New Point(e.X, e.Y) Then
                            '  aGrph.DrawLine(aPen, initialX, initialY, newP.X, newP.Y)
                            Try
                                aGrph.DrawPolygon(aPen, GetRectangle(New Point(newP.X, newP.Y)).ToArray)
                            Catch ex As Exception

                            End Try

                        Else
                            '  aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                            Try
                                aGrph.DrawPolygon(aPen, GetRectangle(New Point(e.X, e.Y)).ToArray)
                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        ' aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                        Try
                            aGrph.DrawPolygon(aPen, GetRectangle(New Point(e.X, e.Y)).ToArray)
                        Catch ex As Exception

                        End Try
                    End If
                End If
            ElseIf isLine = True And isRectangle = False And isShape = False Then
                Canvas1.Refresh()
                aGrph = Canvas1.CreateGraphics
                aPen.Color = activeColor
                If drawSupportOrtho = True Then
                    Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                    If Not newP = New Point(e.X, e.Y) Then
                        aGrph.DrawLine(aPen, initialX, initialY, newP.X, newP.Y)
                    Else
                        aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                    End If
                Else
                    aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                End If

            ElseIf isLine = False And isRectangle = False And isShape = True Then
                Canvas1.Refresh()
                aGrph = Canvas1.CreateGraphics
                aPen.Color = activeColor
                If drawSupportOrtho = True Then
                    Dim newP As Point = RotatelineSupport(New Point(initialX, initialY), New Point(e.X, e.Y))
                    If Not newP = New Point(e.X, e.Y) Then
                        aGrph.DrawLine(aPen, initialX, initialY, newP.X, newP.Y)
                    Else
                        aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                    End If
                Else
                    aGrph.DrawLine(aPen, initialX, initialY, e.X, e.Y)
                End If
            End If
        ElseIf start = False And addVertexClick = True And addedVertex = True And moveVertex = False Then
            Canvas1.Refresh()
            aGrph = Canvas1.CreateGraphics
            Try
                If Not mouseMoveVertexPoints.Length < 1 Then
                    aGrph.DrawLine(aPen, CInt(TransformatorForCanvasX(mouseMoveVertexPoints(0).X)), CInt(TransformatorForCanvasY(-1 * mouseMoveVertexPoints(0).Y)), e.X, e.Y)
                    aGrph.DrawLine(aPen, CInt(TransformatorForCanvasX(mouseMoveVertexPoints(1).X)), CInt(TransformatorForCanvasY(-1 * mouseMoveVertexPoints(1).Y)), e.X, e.Y)
                End If
            Catch ex As Exception

            End Try
        ElseIf start = False And addVertexClick = False And addedVertex = False And moveVertex = True And moveVertexActive = True And moveVertexList.Count > 0 Then
            Canvas1.Refresh()
            aGrph = Canvas1.CreateGraphics
            Try
                aGrph.DrawLine(aPen, CInt(TransformatorForCanvasX(moveVertexList(0).X)), CInt(TransformatorForCanvasY(-1 * moveVertexList(0).Y)), e.X, e.Y)
                aGrph.DrawLine(aPen, CInt(TransformatorForCanvasX(moveVertexList(1).X)), CInt(TransformatorForCanvasY(-1 * moveVertexList(1).Y)), e.X, e.Y)
            Catch ex As Exception

            End Try
        ElseIf start = False And addVertexClick = False And addedVertex = False And moveVertex = False And moveVertexActive = False And moveVertexList.Count = 0 And moveElementActive = True Then
            Canvas1.Refresh()
            aGrph = Canvas1.CreateGraphics
            Try
                Dim activetmpShape As Shape
                Dim tmpList As New List(Of Point)
                If TypeOf isInRangeList(0) Is Shape Then
                    activetmpShape = isInRangeList(0)
                Else
                    Exit Sub
                End If
                'LAST POINT
                Dim activePoint As Point
                activePoint = New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1)))
                Dim clickPoint As Point = moveElementClickVertex
                moveElementDiff = New Point(clickPoint.X - activePoint.X, clickPoint.Y - activePoint.Y)
                tmpList.Clear()
                tmpList.Add(New Point(e.X, e.Y))
                ' Debug.Print(tmpList(0).ToString)
                ' canvas coordinate  add
                For i As Integer = 0 To vertexDiffList.Count - 1
                    ' activetmpShape.RenewVertex(activetmpShape.getVerticies(0))
                    '  Debug.Print(vertexDiffList(i).X & "|" & vertexDiffList(i).Y)
                    ' Debug.Print(e.X - TakeOriginalCoordinateX(vertexDiffList(i).X))
                    'Debug.Print(e.Y + TakeOriginalCoordinateY((-1 * vertexDiffList(i).Y)))

                    tmpList.Add(New Point(e.X - ((vertexDiffList(i).X) * zoom), e.Y + ((-1 * (vertexDiffList(i).Y)) * zoom)))
                    'Debug.Print(tmpList(i + 1).ToString)
                    'Debug.Print(tmpList(i).X & "|" & tmpList(i).Y)
                Next
                'tmpList.Add(New Point(e.X, e.Y))

                '  Debug.Print(tmpList.Count - 1)
                aGrph.DrawPolygon(aPen, tmpList.ToArray)
            Catch ex As Exception

            End Try
        End If
    End Sub
    Function GetRectangle(ActivePoint As Point) As List(Of Point)
        Dim GetRectangled = New List(Of Point)
        GetRectangled.Add(New Point(CInt(TransformatorForCanvasX(drawnRectElements(0).X)), CInt(TransformatorForCanvasY(-1 * drawnRectElements(0).Y))))
        GetRectangled.Add(New Point(CInt(TransformatorForCanvasX(drawnRectElements(1).X)), CInt(TransformatorForCanvasY(-1 * drawnRectElements(1).Y))))
        GetRectangled.Add(New Point(ActivePoint.X, ActivePoint.Y))
        '  GetRectangled.Add(New Point(initialX, initialY))
        Dim lastPoint As Point
        lastPoint.X = ActivePoint.X + (GetRectangled(0).X - GetRectangled(1).X)
        lastPoint.Y = (-1 * ActivePoint.Y + ((-1 * GetRectangled(0).Y) - (-1 * GetRectangled(1).Y))) * -1
        GetRectangled.Add(lastPoint)
        Return GetRectangled
    End Function
    Function GetRectangleLastPoint(ActivePoint As Point) As Point
        Dim GetRectangled = New List(Of Point)
        GetRectangled.Add(New Point(drawnRectElements(0).X, CInt(drawnRectElements(0).Y)))
        GetRectangled.Add(New Point(drawnRectElements(1).X, CInt(drawnRectElements(1).Y)))
        GetRectangled.Add(New Point(ActivePoint.X, ActivePoint.Y))
        Dim lastPoint As Point
        lastPoint.X = ActivePoint.X + (GetRectangled(0).X - GetRectangled(1).X)
        lastPoint.Y = (-1 * ActivePoint.Y + ((-1 * GetRectangled(0).Y) - (-1 * GetRectangled(1).Y))) * -1
        GetRectangled.Add(lastPoint)
        Return lastPoint
    End Function
    Private Sub ModifyVertexBtns(sender As Object, e As EventArgs) Handles DeleteVertexBtn.Click, AddVertexBtn.Click
        If isInRangeList.Count > 0 Then
            If TypeOf isInRangeList(0) Is Shape Then
                If sender.Name = "AddVertexBtn" Then
                    addVertexClick = True
                    deleteVertexClick = False
                ElseIf sender.Name = "DeleteVertexBtn" Then
                    deleteVertexClick = True
                    addVertexClick = False
                End If
            Else
                addVertexClick = False
                deleteVertexClick = False
                MsgBox("Seçilmiş bir kapalı alan bulamadık, lütfen bir kapalı alan seçiniz.")
            End If
        Else
            addVertexClick = False
            deleteVertexClick = False
            MsgBox("Seçilmiş bir kapalı alan bulamadık, lütfen bir kapalı alan seçiniz.")
        End If
    End Sub
    Sub SelectElement()
        isInRangeList.Clear()
        isInRangeList = New List(Of Object)
        Dim activePoint As Point
        activePoint = New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1)))
        Dim tmpDist As Double = 70
        If ListLine.Count > 0 Then
            For ls = 0 To ListLine.Count - 1
                Dim s1P As New Point(ListLine(ls).startPointX, ListLine(ls).startPointY)
                Dim s2P As New Point(ListLine(ls).endPointX, ListLine(ls).endPointY)
                If Distance2Pts(s1P, activePoint) < tmpDist Then
                    If tmpDist > Distance2Pts(s1P, activePoint) Then
                        tmpDist = Distance2Pts(s1P, activePoint)
                        isInRangeList.Add(ListLine(ls))
                    End If
                End If
                If Distance2Pts(s2P, activePoint) < tmpDist Then
                    If tmpDist > Distance2Pts(s2P, activePoint) Then
                        tmpDist = Distance2Pts(s2P, activePoint)
                        isInRangeList.Clear()
                        isInRangeList.Add(ListLine(ls))
                    End If
                End If
            Next
        End If
        If shapeList.Count > 0 Then
            For ls = 0 To shapeList.Count - 1
                Dim tmpPointList As List(Of Point)
                tmpPointList = shapeList(ls).getVerticies.ToList
                For ptl = 0 To tmpPointList.Count - 1
                    Dim tmpPt As New Point(tmpPointList(ptl))
                    If Distance2Pts(tmpPt, activePoint) < tmpDist Then
                        If tmpDist > Distance2Pts(tmpPt, activePoint) Then
                            tmpDist = Distance2Pts(tmpPt, activePoint)
                            isInRangeList.Clear()
                            isInRangeList.Add(shapeList(ls))
                        End If
                    End If
                Next
            Next
        End If
    End Sub
    Function SnapPoint() As Point
        SnapPoint = Nothing
        LastSnapPoint = Nothing
        Dim activePoint As Point
        Dim tmpDist As Double = 70
        If SnappedList.Count > 0 Then
            SnappedList.Clear()
            Canvas1.Refresh()
        End If

        activePoint = New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1)))
        If ListLine.Count > 0 Then
            For ls = 0 To ListLine.Count - 1
                Dim s1P As New Point(ListLine(ls).startPointX, ListLine(ls).startPointY)
                Dim s2P As New Point(ListLine(ls).endPointX, ListLine(ls).endPointY)
                If Distance2Pts(s1P, activePoint) < tmpDist Then

                    If tmpDist > Distance2Pts(s1P, activePoint) Then
                        tmpDist = Distance2Pts(s1P, activePoint)
                        SnapPoint = s1P
                    End If
                End If
                If Distance2Pts(s2P, activePoint) < tmpDist Then
                    If tmpDist > Distance2Pts(s2P, activePoint) Then
                        tmpDist = Distance2Pts(s2P, activePoint)
                        SnapPoint = s2P
                    End If
                End If
            Next
        End If
        If shapeList.Count > 0 Then
            For ls = 0 To shapeList.Count - 1
                Dim tmpPointList As List(Of Point)
                tmpPointList = shapeList(ls).getVerticies.ToList
                For ptl = 0 To tmpPointList.Count - 1
                    Dim tmpPt As New Point(tmpPointList(ptl))
                    If Distance2Pts(tmpPt, activePoint) < tmpDist Then
                        If tmpDist > Distance2Pts(tmpPt, activePoint) Then
                            tmpDist = Distance2Pts(tmpPt, activePoint)
                            SnapPoint = tmpPt
                        End If
                    End If
                Next
            Next
        End If

        If SnapPoint.IsEmpty = False Then
            SnappedList.Clear()
            Dim aPt1 As Point = New Point(CInt(SnapPoint.X), CInt(-1 * (SnapPoint.Y)))
            SnappedList.Add(aPt1)
            LastSnapPoint = aPt1
            aGrph = Canvas1.CreateGraphics
            Canvas1.Refresh()
        End If

        Return SnapPoint
    End Function
    Function IsLastPoint(px As Integer, py As Integer, p2x As Integer, p2y As Integer) As Boolean
        If Math.Sqrt(((p2x - px) ^ 2) + ((p2y - py) ^ 2)) < (10 / zoom) Then
            IsLastPoint = True
        Else
            IsLastPoint = False
        End If
        Return IsLastPoint
    End Function
    Sub RectIsInRange(Px As Integer, Py As Integer)
        Dim aPend As Pen = New Pen(Color.Purple, 3)
        For i = 0 To shapeList.Count - 1
            If PointInPolygon(New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1))), shapeList(i).getVerticies) = True Then
                aGrph = Canvas1.CreateGraphics
                aGrph.DrawPolygon(aPend, shapeList(i).getVerticies)
                '  FlushMemory()
            Else
                Canvas1.Refresh()
            End If
        Next
        For rectListCount = 0 To aListRect.Count - 1
            If aListRect(rectListCount).Contains(New Point(Px, Py * -1)) = True Then
                Dim NewRect As Rectangle
                NewRect.Location = New Point(aListRect(rectListCount).Location)
                Dim rectpX As Double = (NewRect.Location.X + offsetx) * zoom
                Dim rectpY As Double = (NewRect.Location.Y + offsety) * zoom
                NewRect.Width = aListRect(rectListCount).Width * (zoom)
                NewRect.Height = aListRect(rectListCount).Height * (zoom)
                NewRect.Location = New Point(rectpX, rectpY)
                aGrph = Canvas1.CreateGraphics
                aGrph.DrawRectangle(aPend, NewRect)
            Else
                Canvas1.Refresh()
            End If
        Next
    End Sub
    Function TransformatorForCanvasX(px As Double)
        TransformatorForCanvasX = (px + offsetx) * zoom
        Return TransformatorForCanvasX
    End Function
    Function TransformatorForCanvasY(py As Double)
        TransformatorForCanvasY = ((-1 * py + offsety) * zoom)
        Return TransformatorForCanvasY
    End Function
    Function TakeOriginalCoordinateX(px As Double) As Double
        TakeOriginalCoordinateX = (px / zoom) - offsetx
        Return TakeOriginalCoordinateX
    End Function
    Function TakeOriginalCoordinateY(py As Double) As Double
        TakeOriginalCoordinateY = ((py / zoom) - offsety)
        Return TakeOriginalCoordinateY
    End Function
    Private Sub ExportImageDistort(Path As String)
        '   Dim abtmp As Bitmap = New Bitmap(Path)
        '  abtmp.
        ' bitmap = DrawImage2(abtmp)
    End Sub
    Private Sub DrawImage(ByVal gr As Graphics)
        If bitmap IsNot Nothing Then
            bitmap.Dispose()
            bitmap = Nothing
        End If
        If addedTif = False Then
            '  ExportImageDistort("D:\56-528-145952-4727-20\C_680.tif")
            FlushMemory()
            addedTif = True
        End If
        Dim aNewBitmap As Bitmap = New Bitmap("D:\a.tif")
        Dim scaleFactor As Double = 100
        Dim X1 As Double = -1.12 * scaleFactor
        Dim X2 As Double = -19.96 * scaleFactor
        Dim X3 As Double = aNewBitmap.Width * scaleFactor
        Dim X4 As Double = aNewBitmap.Height * scaleFactor
        Dim aRect As Rectangle = New Rectangle(X1, X2, X3, X4)
        Try
            gr.DrawImage(aNewBitmap, aRect)
        Catch ex As Exception

        End Try
        FlushMemory()
    End Sub
    Public Sub Intersect_RectF_Example(ByVal e As PaintEventArgs)

        ' Create the first rectangle and draw it to the screen in black.
        'Dim regionRect As Rectangle = New Rectangle(1100, 400, 300, 400)
        Dim regionRect As Rectangle = New Rectangle(7728, 15796, 900, 500)
        ' e.Graphics.DrawRectangle(Pens.Blue, regionRect)
        Dim path As GraphicsPath = New GraphicsPath
        ' create the second rectangle and draw it to the screen in red.
        Dim complementRect As Rectangle = New Rectangle(1300, 200, 700, 700)
        Dim complementRect2 As Rectangle = New Rectangle(200, 200, 1000, 1000)
        ' e.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(complementRect))
        '   e.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(complementRect2))

        ' Create a region using the first rectangle.
        Dim myRegion As Region = New Region(regionRect)
        Dim Pointsk(0 To 4) As Point
        Pointsk(0) = New Point(2000, 200)
        Pointsk(1) = New Point(2000, 900)
        Pointsk(2) = New Point(1300, 900)
        Pointsk(3) = New Point(1230, 700)
        Pointsk(4) = New Point(1300, 200)
        ' Get the area of intersection for myRegion when combined with
        'path.AddRectangle(regionRect)
        ' path.AddRectangle(complementRect)
        'path.AddRectangle(complementRect2)
        ' Dim pointFa() As PointF
        'path.AddPolygon(Pointsk)
        'Dim tmpShape As Shape = shapeList(7)
        'tmpShape.VertexSortCW()
        'path.AddPolygon(tmpShape.getVerticies)
        'tmpShape = shapeList(8)
        'tmpShape.VertexSortCW()
        'path.AddPolygon(shapeList(8).getVerticies)
        ''path.AddRectangle(complementRect2)
        ' complementRect.
        'myRegion.Exclude(path)
        'myRegion.Exclude(path)
        ' myRegion.GetBounds(e.Graphics)

        '  myRegion.GetRegionData(0)
        'myRegion.
        ' MsgBox(myRegion.IsEmpty)
        'Region.Exclude(path)
        ' Fill the intersection area of myRegion with blue.
        'Dim myBrush As SolidBrush = New SolidBrush(Color.Yellow)
        '   e.Graphics.FillRegion(myBrush, myRegion)
        ' e.Graphics.
        ' Dim rects = myRegion.GetRegionScans(New Matrix)
        'aPen.Width = 1

        'Dim NewRects As New List(Of Rectangle)
        'For Each aRect As RectangleF In rects
        '    NewRects.Add(New Rectangle(New Point(aRect.Location.X, aRect.Location.Y), New Size(aRect.Width, aRect.Height)))
        '    'MsgBox(aRect.Location.ToString & "," & aRect.Size.ToString)
        '    'Dim text As String
        '    'text = "a," & aRect.Location.X & "," & aRect.Location.Y & ",0" & vbCrLf
        '    'text = text & "a," & aRect.Location.X + aRect.Width & "," & aRect.Location.Y + aRect.Height & ",0"
        '    'My.Computer.FileSystem.WriteAllText("D:\getRegion.txt", text & vbCrLf, True)
        'Next
        ' e.Graphics.DrawRectangles(aPen, rects.ToArray)
        '  e.Graphics.DrawPolygon(aPen, simplifiedRectangles2PolygonPoints(rects))
        'rects.

        'MsgBox(myRegion.Clone)
        '  For i = 0 To myRegion.GetRegionData.Data.Length - 1
        'My.Computer.FileSystem.WriteAllBytes("D:\getRegion.png", myRegion.GetRegionData.Data, False)
        '  Next
        ' MsgBox(myRegion.GetRegionData.Data(2).ToString)
        'e.Graphics.FillPath(myBrush, path)
        'e.Graphics.FillRectangle(myBrush, regionRect)
        '   e.Graphics.FillRectangle(myBrush,regionRect)
    End Sub
    Private Sub ColorPickBtn_Click(sender As Object, e As EventArgs) Handles ColorPickBtn.Click
        If ColorPickerForm Is Nothing Then
            ColorPickerForm = New ColorPicker
            ColorPickerForm.Show()
        End If
        Dim aLocation As New Point
        aLocation.X = Cursor.Position.X
        aLocation.Y = Cursor.Position.Y + 10
        ColorPickerForm.Location = aLocation
        ColorPickerForm.Visible = True
        ColorPickerForm.Opacity = 100
        ColorPickerForm.TopMost = True
    End Sub
    Private Sub ColorPickBtn_MouseHover(sender As Object, e As EventArgs) Handles ColorPickBtn.MouseHover
        Panel3.BackColor = Color.White
    End Sub
    Private Sub ColorPickBtn_MouseLeave(sender As Object, e As EventArgs) Handles ColorPickBtn.MouseLeave
        Panel3.BackColor = Color.Transparent
    End Sub
    Private Sub Guna2ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox2.SelectedIndexChanged
        aPen.Width = Guna2ComboBox2.Text
        activeLineWeight = Guna2ComboBox2.Text
    End Sub
    Private Sub ElementSelectBtn_Click(sender As Object, e As EventArgs) Handles ElementSelectBtn.Click
        line = Nothing
        shape = Nothing
        start = False
        addVertexClick = False
        addedVertex = False
        deleteVertexClick = False
        moveVertex = False
        moveElement = False
        moveVertexActive = False
        moveElementActive = False
        isDraw = False
        addVertexClick = False
        deleteVertexClick = False
        moveVertex = False
        moveVertexActive = False
        moveVertexList.Clear()
        shapePathList.Clear()
        SnappedList.Clear()
        rect = Nothing
        Canvas1.Refresh()
    End Sub
    Private Sub DelEleBtn_Click(sender As Object, e As EventArgs) Handles DelEleBtn.Click
        If isInRangeList.Count > 0 Then
            If TypeOf isInRangeList(0) Is Line Then
                ListLine.Remove(isInRangeList(0))
            ElseIf TypeOf isInRangeList(0) Is Shape Then
                Dim removeShape As Shape = isInRangeList(0)
                RemoveRoom(removeShape)
                RemoveComponents(removeShape)
                RemoveOuterBuild(removeShape)
                removeShape.Remove()
                shapeList.Remove(isInRangeList(0))
                removeShape.Dispose()
            End If
            isInRangeList.Clear()
            Canvas1.Refresh()
        End If
    End Sub
    Private Sub SupportOrthoBtn_Click(sender As Object, e As EventArgs) Handles SupportOrthoBtn.Click
        If drawSupportOrtho = True Then
            Me.SupportOrthoBtn.CustomImages.Image = Global.bCAD.My.Resources.Resources.passiveOrtho
            drawSupportOrtho = False
        Else
            Me.SupportOrthoBtn.CustomImages.Image = Global.bCAD.My.Resources.Resources.activeOrtho
            drawSupportOrtho = True
        End If
    End Sub
    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        If isInRangeList.Count > 0 Then
            If TypeOf isInRangeList(0) Is Line Then
                Dim tmpLine As Line
                tmpLine = isInRangeList(0)
                ItemInformationPnl.ElementClassNameLbl.Text = "Çizgi"
                ItemInformationPnl.lineweightLbl.Text = tmpLine.lineWeight
                ItemInformationPnl.lvlNameLbl.Text = tmpLine.Level
                ItemInformationPnl.colorLbl.Text = tmpLine.color.ToString
                ItemInformationPnl.lastModified.Text = "-"
                ItemInformationPnl.startPLbl.Text = New Point(tmpLine.startPointX / 10000, tmpLine.startPointY / 10000).ToString
                ItemInformationPnl.endPLbl.Text = New Point(tmpLine.endPointX / 10000, tmpLine.endPointY / 10000).ToString
                ItemInformationPnl.verticesCntLbl.Text = "2"
                ItemInformationPnl.centerPLbl.Text = Math.Round(CDbl((tmpLine.startPointX / 10000 + tmpLine.endPointX / 10000) / 2), 3) & "," & Math.Round(CDbl((tmpLine.startPointY / 10000 + tmpLine.endPointY / 10000) / 2), 3)
                ItemInformationPnl.lengthLbl.Text = tmpLine.getLength.ToString / 10000
                ItemInformationPnl.areaLbl.Text = "0"
                ItemInformationPnl.rotationLbl.Text = tmpLine.getRotate.ToString
            ElseIf TypeOf isInRangeList(0) Is Shape Then
                Dim tmpShape As Shape
                tmpShape = isInRangeList(0)
                ItemInformationPnl.ElementClassNameLbl.Text = "Poligon (Kapalı Alan)"
                ItemInformationPnl.lineweightLbl.Text = tmpShape.lineWeight
                ItemInformationPnl.lvlNameLbl.Text = tmpShape.Level
                ItemInformationPnl.colorLbl.Text = tmpShape.color.ToString
                ItemInformationPnl.lastModified.Text = "-"
                ItemInformationPnl.startPLbl.Text = New Point(tmpShape.startPointX, tmpShape.startPointY).ToString
                ItemInformationPnl.endPLbl.Text = New Point(tmpShape.endPointX, tmpShape.endPointY).ToString
                ItemInformationPnl.verticesCntLbl.Text = tmpShape.getVerticies.Length - 1
                ItemInformationPnl.centerPLbl.Text = tmpShape.centroid.ToString
                ItemInformationPnl.lengthLbl.Text = tmpShape.getLength.ToString
                ItemInformationPnl.areaLbl.Text = tmpShape.Area.ToString
                ItemInformationPnl.rotationLbl.Text = "-"
            End If
        End If
        If ItemInformationPnlVisible = True Then
            ItemInformationPnl.Visible = True
            ItemInformationPnlVisible = False
            ItemInformationPnl.BringToFront()
            ItemInformationPnl.BringToFront()
            ItemInformationPnl.BringToFront()
        Else
            ItemInformationPnl.Visible = False
            ItemInformationPnlVisible = True
        End If
    End Sub
    Private Sub ContentBtn_Click(sender As Object, e As EventArgs) Handles ContentBtn.Click
        If ContentInformationPnlVisible = True Then
            ContentInformationPnl.Visible = True
            ContentInformationPnlVisible = False
            ContentInformationPnl.BringToFront()
            ContentInformationPnl.BringToFront()
            ContentInformationPnl.BringToFront()
        Else
            ContentInformationPnl.Visible = False
            ContentInformationPnlVisible = True
        End If
    End Sub
    Private Sub Canvas1_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Canvas1.MouseWheel
        If MouseAction = T_MouseAction.Panning Then
            Exit Sub
        End If
        Dim oldzoom As Decimal = zoom
        Dim zoomDeltaF As Decimal
        If oldzoom > 0.4 And oldzoom < 3 Then
            zoomDeltaF = 0.2
        ElseIf oldzoom > 3 And oldzoom < 10 Then
            zoomDeltaF = 0.7
        ElseIf oldzoom > 10 And oldzoom < 20 Then
            zoomDeltaF = 3
        ElseIf oldzoom > 20 Then
            zoomDeltaF = 20
        ElseIf oldzoom < 0.4 And oldzoom > 0.031 Then
            zoomDeltaF = 0.03
        ElseIf oldzoom < 0.021 Then
            zoomDeltaF = 0.003
        Else
            zoomDeltaF = 0.003
        End If
        If e.Delta > 0 Then
            zoom = Math.Min(zoom + zoomDeltaF, maxzoom)
        End If
        If e.Delta < 0 Then
            zoom = Math.Max(zoom - zoomDeltaF, minzoom)
        End If
        zoomFactorLbl.Text = zoom
        Dim mousePosNow As Point = e.Location
        Dim x, y As Integer
        x = mousePosNow.X
        y = mousePosNow.Y
        Dim oldoffsetx, oldoffsety As Integer
        Dim newoffsetx, newoffsety As Integer
        oldoffsetx = CInt(x / oldzoom)
        oldoffsety = CInt(y / oldzoom)
        newoffsetx = CInt(x / zoom)
        newoffsety = CInt(y / zoom)
        offsetx = newoffsetx - oldoffsetx + offsetx
        offsety = newoffsety - oldoffsety + offsety
        Dim tmpValueX As Integer
        Dim tmpValueY As Integer
        tmpValueX = (ClickInitialX - (clickOffsetX * clickZoom)) / clickZoom
        tmpValueY = (ClickinitialY - (clickOffsetY * clickZoom)) / clickZoom
        initialX = ((tmpValueX + offsetx) * zoom)
        initialY = ((tmpValueY + offsety) * zoom)
        Canvas1.Refresh()
    End Sub
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        aListLine.Clear()
        aListRect.Clear()
        shapeList.Clear()
        shapePathList.Clear()
        ListLine.Clear()
        isInRangeList.Clear()
        start = False
        isDraw = False
        isLine = False
        isRectangle = False
        isShape = False
        SnappedList.Clear()
        Canvas1.Refresh()
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        offsetx = 0
        offsety = 0
        zoom = 1
        zoomFactorLbl.Text = 1
        oldZoomLbl.Text = 1
        oldZoomFactork = 1
        ofX.Text = CStr(0) & " , " & CStr(0)
        Canvas1.Refresh()
    End Sub
    Private Sub Canvas1_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles Canvas1.MouseEnter
        Canvas1.Focus()
    End Sub
    Private Sub Canvas1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Canvas1.MouseLeave
        Me.Focus()
    End Sub
    Private Sub Canvas1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Canvas1.MouseDown
        If e.Button = MouseButtons.Middle Then
            MouseAction = T_MouseAction.Panning
            mouseDownPt = e.Location
            startx = offsetx
            starty = offsety
        End If
        If e.Button = MouseButtons.Left Then
            zoomstart = e.Location
            TmrMarch.Interval = 100
            TmrMarch.Enabled = True
        End If
    End Sub
    Private Sub Canvas1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Canvas1.MouseUp
        Cursor = Cursors.Default
        TmrMarch.Enabled = False
        If MouseAction = T_MouseAction.RectangleZooming Then
            Dim oldzoom As Decimal = zoom
            zoom = Canvas1.Width / zoomrect.Width * zoom
            zoom = Math.Round(zoom / 0.2) * 0.2
            zoom = Math.Max(zoom, minzoom)
            zoom = Math.Min(zoom, maxzoom)
            Dim oldoffsetx, oldoffsety As Integer
            Dim newoffsetx, newoffsety As Integer
            oldoffsetx = CInt((zoomrect.X + zoomrect.Width / 2) / oldzoom)
            oldoffsety = CInt((zoomrect.Y + zoomrect.Height / 2) / oldzoom)
            newoffsetx = CInt((zoomrect.X + zoomrect.Width / 2) / zoom)
            newoffsety = CInt((zoomrect.Y + zoomrect.Height / 2) / zoom)
            offsetx = newoffsetx - oldoffsetx + offsetx
            offsety = newoffsety - oldoffsety + offsety
        End If
        MouseAction = T_MouseAction.None
        Canvas1.Refresh()
    End Sub
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub
    Private Sub MoveVertexBtn_Click(sender As Object, e As EventArgs) Handles MoveVertexBtn.Click
        moveVertex = True
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        moveElement = True
    End Sub
    Private Sub MinimizedButton_Click(sender As Object, e As EventArgs) Handles MinimizedButton.Click
        Me.WindowState = WindowState.Minimized
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim aPend As Pen = New Pen(Color.Purple, 3)
            For i = 0 To shapeList.Count - 1
                If PointInPolygon(New Point(cX.Text.Split(",")(0), -1 * CInt(cX.Text.Split(",")(1))), shapeList(i).getVerticies) = True Then
                    aGrph = Canvas1.CreateGraphics
                    aGrph.DrawPolygon(aPend, shapeList(i).getVerticies)
                    FlushMemory()
                Else
                    Canvas1.Refresh()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        FlushMemory()
    End Sub
    Private Sub TmrMarch_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TmrMarch.Tick
        Canvas1.Refresh()
    End Sub
    Private Sub Canvas1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Canvas1.Paint
        '  grCnvs = e.Graphics
        e.Graphics.SmoothingMode = SmoothingMode.None
        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed
        e.Graphics.CompositingQuality = CompositingQuality.HighSpeed
        e.Graphics.ScaleTransform(CSng(zoom), CSng(zoom))
        e.Graphics.TranslateTransform(offsetx, offsety)
        ' DrawImage(e.Graphics)
        AddImageChatGpt(e.Graphics)
        DrawAllElements(e.Graphics)

        ' Intersect_RectF_Example(e)
        ' fdf(e)
        e.Graphics.ResetTransform()
        'e.Graphics.Flush()
        If MouseAction = T_MouseAction.RectangleZooming Then
            MarchOffset = MarchOffset + OffsetDelta
            Dim pen As New Pen(Color.Black, 2) With {
                .DashPattern = DashPattern,
                .DashOffset = MarchOffset,
                .Color = Color.Red
            }
            e.Graphics.DrawRectangle(pen, zoomrect)
        End If
    End Sub

    Private Sub warpBtn_Click(sender As Object, e As EventArgs) Handles warpBtn.Click
        start = False
        addVertexClick = False
        addedVertex = False
        deleteVertexClick = False
        moveVertex = False
        moveElement = False
        moveVertexActive = False
        moveElementActive = False
        isDraw = False
        rectClick = 0
        drawnRectElements.Clear()
        moveElementVertices.Clear()
        isInRangeList.Clear()
        warpPointList.Clear()
        warpStart = True
    End Sub

    'Sub wp2()
    '    Dim inputPoints As List(Of PointF)
    '    Dim outputPoints As List(Of PointF)

    '    'Dim homographyEstimation As New 'Accord.Imaging.HomographyEstimation(inputPoints, outputPoints)
    '    Dim homographyMatrix As Accord.Math.Matrix = Accord.Math.Matrix.Homography(pts1, pts2)
    '    ' Homography matrisini hesaplayın
    '    Dim homographyMatrix As Accord.Math.Matrix = homographyEstimation.Estimate()

    '    Dim tfwValues As Double()
    '    Dim pixelWidth As Double = tfwValues(0)
    '    Dim pixelHeight As Double = tfwValues(3)
    '    Dim startX As Double = tfwValues(4)
    '    Dim startY As Double = tfwValues(5)

    '    'Yeni tfw değerlerini hesaplama'
    '    Dim newStartX As Double = homographyMatrix(0, 2) + startX * homographyMatrix(0, 0) + startY * homographyMatrix(0, 1)
    '    Dim newStartY As Double = homographyMatrix(1, 2) + startX * homographyMatrix(1, 0) + startY * homographyMatrix(1, 1)
    '    Dim newPixelWidth As Double = homographyMatrix(0, 0) * pixelWidth
    '    Dim newSkewX As Double = homographyMatrix(0, 1) / newPixelWidth
    '    Dim newSkewY As Double = homographyMatrix(1, 0) / pixelHeight
    '    Dim newPixelHeight As Double = homographyMatrix(1, 1) * pixelHeight
    'End Sub
    Sub WarpMethod()
        Dim worldFile As String = "D:\56-528-145952-4727-20\C_680.tfw"
        Dim sourcePoints As List(Of PointF) = New List(Of PointF)()
        ' add the 4 source points to the list

        Dim referencePoints As List(Of PointF) = New List(Of PointF)()
        ' add the 4 reference points to the list
        sourcePoints.Add(warpPointList(1))
        sourcePoints.Add(warpPointList(3))
        sourcePoints.Add(warpPointList(5))
        sourcePoints.Add(warpPointList(7))

        referencePoints.Add(warpPointList(2))
        referencePoints.Add(warpPointList(4))
        referencePoints.Add(warpPointList(6))
        referencePoints.Add(warpPointList(8))


        ' Compute the transformation matrix
        Dim matrix As Drawing2D.Matrix = New Drawing2D.Matrix()
        matrix.Translate(-sourcePoints(0).X, -sourcePoints(0).Y)
        matrix.Scale((referencePoints(1).X - referencePoints(0).X) / (sourcePoints(1).X - sourcePoints(0).X), (referencePoints(2).Y - referencePoints(0).Y) / (sourcePoints(2).Y - sourcePoints(0).Y))
        matrix.RotateAt(Math.Atan2(sourcePoints(1).Y - sourcePoints(0).Y, sourcePoints(1).X - sourcePoints(0).X) * 180 / Math.PI, New PointF(0, 0))
        matrix.Translate(referencePoints(0).X, referencePoints(0).Y)
        'Debug.Print(matrix.Elements.Length - 1)
        ' Write the TFW file
        'Dim tfwFile As String = "path/to/tfw/file.tfw"
        Dim dx1 As Single = referencePoints(1).X - referencePoints(0).X
        Dim dx2 As Single = referencePoints(2).X - referencePoints(0).X
        Dim dy1 As Single = referencePoints(1).Y - referencePoints(0).Y
        Dim dy2 As Single = referencePoints(2).Y - referencePoints(0).Y

        Dim sx As Single = (dx1 * dy2 - dx2 * dy1) / (dx1 * dx2 * (dy2 - dy1))
        Dim sy As Single = (dy1 * dx2 - dy2 * dx1) / (dy1 * dy2 * (dx2 - dx1))

        Dim matrix1 As New Drawing2D.Matrix()
        matrix1.Reset()
        matrix1.Translate(-sourcePoints(0).X, -sourcePoints(0).Y)
        matrix1.Scale(referencePoints(0).X / sourcePoints(0).X, referencePoints(0).Y / sourcePoints(0).Y)
        matrix1.Shear((sourcePoints(2).X / sourcePoints(0).X) - (sourcePoints(1).X / sourcePoints(0).X), (sourcePoints(2).Y / sourcePoints(0).Y) - (sourcePoints(1).Y / sourcePoints(0).Y))
        matrix1.Translate(referencePoints(0).X, referencePoints(0).Y)

        Dim elements As Single() = matrix.Elements

        Dim shearX As Single = elements(1)
        Dim shearY As Single = elements(3)
        Dim scaleX As Single = Math.Sqrt(elements(0) * elements(0) + elements(1) * elements(1))
        Dim scaleY As Single = Math.Sqrt(elements(2) * elements(2) + elements(3) * elements(3))

        Debug.WriteLine(shearX)
        Debug.WriteLine(shearY)
        Debug.WriteLine(scaleX)
        Debug.WriteLine(scaleY)


        Debug.WriteLine(sx)
        Debug.WriteLine(sy)

        'Debug.WriteLine((matrix.Elements(0)).ToString("F6"))
        'Debug.WriteLine((matrix.Elements(1) / 10000).ToString("F6"))
        'Debug.WriteLine((matrix.Elements(2) / 10000).ToString("F6"))
        'Debug.WriteLine((matrix.Elements(3)).ToString("F6"))
        'Debug.WriteLine((matrix.Elements(4 / 10000)).ToString("F6"))
        'Debug.WriteLine((matrix.Elements(5) / 10000).ToString("F6"))
        warpPointList.Clear()
        InfoLbl.Text = "Affin dönüşümü işlemi başlatıldı."
        Exit Sub

        Using writer As New StreamWriter("D:\56-528-145952-4727-20\C_680.tfw", False)
            writer.WriteLine((matrix.Elements(0)).ToString("F6"))
            Debug.Write((matrix.Elements(0)).ToString("F6"))
            writer.WriteLine((matrix.Elements(1) / 10000).ToString("F6"))
            Debug.Write((matrix.Elements(1) / 10000).ToString("F6"))
            writer.WriteLine((matrix.Elements(2) / 10000).ToString("F6"))
            Debug.Write((matrix.Elements(2) / 10000).ToString("F6"))
            writer.WriteLine((matrix.Elements(3)).ToString("F6"))
            Debug.Write((matrix.Elements(3)).ToString("F6"))
            writer.WriteLine((matrix.Elements(4) / 10000).ToString("F6"))
            Debug.Write((matrix.Elements(4 / 10000)).ToString("F6"))
            writer.WriteLine((matrix.Elements(5) / 10000).ToString("F6"))
            Debug.Write((matrix.Elements(5) / 10000).ToString("F6"))
        End Using

        warpPointList.Clear()
        InfoLbl.Text = "Affin dönüşümü işlemi başlatıldı."
    End Sub
    Public Function GetPerspectiveTransformMatrix(src As PointF(), dst As PointF()) As Single(,)
        Dim A(,) As Single = {{src(0).X, src(0).Y, 1, 0, 0, 0, -dst(0).X * src(0).X, -dst(0).X * src(0).Y},
                          {0, 0, 0, src(0).X, src(0).Y, 1, -dst(0).Y * src(0).X, -dst(0).Y * src(0).Y},
                          {src(1).X, src(1).Y, 1, 0, 0, 0, -dst(1).X * src(1).X, -dst(1).X * src(1).Y},
                          {0, 0, 0, src(1).X, src(1).Y, 1, -dst(1).Y * src(1).X, -dst(1).Y * src(1).Y},
                          {src(2).X, src(2).Y, 1, 0, 0, 0, -dst(2).X * src(2).X, -dst(2).X * src(2).Y},
                          {0, 0, 0, src(2).X, src(2).Y, 1, -dst(2).Y * src(2).X, -dst(2).Y * src(2).Y},
                          {src(3).X, src(3).Y, 1, 0, 0, 0, -dst(3).X * src(3).X, -dst(3).X * src(3).Y},
                          {0, 0, 0, src(3).X, src(3).Y, 1, -dst(3).Y * src(3).X, -dst(3).Y * src(3).Y}}
        Dim B() As Single = {dst(0).X, dst(0).Y, dst(1).X, dst(1).Y, dst(2).X, dst(2).Y, dst(3).X, dst(3).Y}
        Dim h() As Single = Solve(A, B)
        Dim Hmatrx As Single(,) = {{h(0), h(1), h(2)},
                                {h(3), h(4), h(5)},
                                {h(6), h(7), 1}}
        Dim dx1 As Single = (src(1).X - src(2).X) / 10000
        Dim dy1 As Single = (src(1).Y - src(2).Y) / 10000
        Dim dx2 As Single = (src(2).X - src(3).X) / 10000
        Dim dy2 As Single = (src(2).Y - src(3).Y) / 10000

        ' Yatay ve dikey mesafeler arasındaki oranı hesapla
        Dim skew_angle As Single = (dx1 * dx2 + dy1 * dy2) / Math.Sqrt((dx1 ^ 2 + dy1 ^ 2) * (dx2 ^ 2 + dy2 ^ 2))

        ' Dönüklük açısına göre a12 ve a22 değerlerini hesapla
        Dim a12 As Single = -Math.Tan(skew_angle)
        Dim a22 As Single = 1 / Math.Cos(skew_angle)

        Dim worldFile As String = "D:\56-528-145952-4727-20\C_680.tfw"
        Dim readLine() As String = My.Computer.FileSystem.ReadAllText(worldFile).Split(vbLf)
        Debug.WriteLine(Hmatrx(0, 0).ToString * readLine(0))
        Debug.WriteLine((Hmatrx(0, 1).ToString / 10000) * readLine(3) + (readLine(2)))
        Debug.WriteLine((Hmatrx(1, 0).ToString / -10000) * readLine(0) + readLine(1))
        Debug.WriteLine(Hmatrx(1, 1).ToString * readLine(3))
        Debug.WriteLine(Hmatrx(0, 2).ToString / 10000 + readLine(4))
        Debug.WriteLine(Hmatrx(1, 2).ToString / 10000 + readLine(5))
        Debug.WriteLine(a12 & vbCrLf & a22)
        Return Hmatrx
    End Function
    Public Function Solve(A(,) As Single, B() As Single) As Single()
        Dim n As Integer = B.Length
        For i As Integer = 0 To n - 1
            Dim maxEl As Single = Math.Abs(A(i, i))
            Dim maxRow As Integer = i
            For k As Integer = i + 1 To n - 1
                If Math.Abs(A(k, i)) > maxEl Then
                    maxEl = Math.Abs(A(k, i))
                    maxRow = k
                End If
            Next

            For k As Integer = i To n - 1
                Dim tmp As Single = A(maxRow, k)
                A(maxRow, k) = A(i, k)
                A(i, k) = tmp
            Next

            Dim temp As Single = B(maxRow)
            B(maxRow) = B(i)
            B(i) = temp

            For k As Integer = i + 1 To n - 1
                Dim c As Single = -A(k, i) / A(i, i)
                For j As Integer = i To n - 1
                    If i = j Then
                        A(k, j) = 0
                    Else
                        A(k, j) += c * A(i, j)
                    End If
                Next
                B(k) += c * B(i)
            Next
        Next

        Dim x(n - 1) As Single
        For i As Integer = n - 1 To 0 Step -1
            x(i) = B(i) / A(i, i)
            For k As Integer = i - 1 To 0 Step -1
                B(k) -= A(k, i) * x(i)
            Next
        Next

        Return x
    End Function
    Sub dd()
        'Dim A1 As New PointF(1221.28308F, -1008.22589F)
        'Dim A2 As New PointF(1968.2F, -1008.22589F)
        'Dim A3 As New PointF(1968.2F, -1556.41406F)
        'Dim A4 As New PointF(1221.28308F, -1556.41406F)
        'Dim R1 As New PointF(7.5584F, -6.2188F)
        'Dim R2 As New PointF(12.1371F, -6.2188F)
        'Dim R3 As New PointF(12.1371F, -9.6923F)
        'Dim R4 As New PointF(7.5584F, -9.6923F)



        Dim sourcePoints As List(Of PointF) = New List(Of PointF)()
        ' add the 4 source points to the list

        Dim referencePoints As List(Of PointF) = New List(Of PointF)()
        ' add the 4 reference points to the list
        sourcePoints.Add(New Point(warpPointList(1).X, -1 * warpPointList(1).Y))
        sourcePoints.Add(New Point(warpPointList(3).X, -1 * warpPointList(3).Y))
        sourcePoints.Add(New Point(warpPointList(5).X, -1 * warpPointList(5).Y))
        sourcePoints.Add(New Point(warpPointList(7).X, -1 * warpPointList(7).Y))

        referencePoints.Add(New Point(warpPointList(2).X, -1 * warpPointList(2).Y))
        referencePoints.Add(New Point(warpPointList(4).X, -1 * warpPointList(4).Y))
        referencePoints.Add(New Point(warpPointList(6).X, -1 * warpPointList(6).Y))
        referencePoints.Add(New Point(warpPointList(8).X, -1 * warpPointList(8).Y))
        Debug.Print("[" & String.Join("], [", sourcePoints).Replace("Y=", "").Replace("{X=", "").Replace("}", "") & "]")
        Debug.Print(vbCrLf & "----------------------")
        Debug.Print("[" & String.Join("], [", referencePoints).Replace("Y=", "").Replace("{X=", "").Replace("}", "") & "]")

        Dim aSngle As Single(,) = GetPerspectiveTransformMatrix(sourcePoints.ToArray, referencePoints.ToArray)
        Dim worldFile As String = "D:\56-528-145952-4727-20\C_680.tfw"
        Dim readLine() As String = My.Computer.FileSystem.ReadAllText(worldFile).Split(vbLf)

        Using writer As New StreamWriter(worldFile, False)
            'writer.WriteLine(aSngle(0, 0).ToString)
            'writer.WriteLine(aSngle(0, 1).ToString)
            'writer.WriteLine(aSngle(1, 0).ToString)
            'writer.WriteLine(-1 * aSngle(1, 1).ToString)
            'writer.WriteLine(aSngle(0, 2).ToString)
            'writer.WriteLine(aSngle(1, 2).ToString)
            writer.WriteLine(aSngle(0, 0).ToString * readLine(0))
            writer.WriteLine(aSngle(0, 1).ToString / 10000 + (readLine(1)))
            writer.WriteLine(aSngle(1, 0).ToString / 10000 + (readLine(2)))
            writer.WriteLine(aSngle(1, 1).ToString * readLine(3))
            writer.WriteLine(aSngle(0, 2).ToString / 10000 + readLine(4))
            writer.WriteLine(aSngle(1, 2).ToString / 10000 + readLine(5))
        End Using
        warpPointList.Clear()
        InfoLbl.Text = "Affin dönüşümü işlemi başlatıldı."
    End Sub

    Sub AddImageChatGpt(g As Graphics)
        '   Dim tfwFileName As String = "D:\56-528-145952-4727-20\C_680.tfw"
        Dim imageFile As String = "D:\56-528-145952-4727-20\C_680.tif"
        Dim worldFile As String = "D:\56-528-145952-4727-20\C_680.tfw"

        ' World File dosyasındaki değerleri oku
        Dim values As Double() = File.ReadAllLines(worldFile).Select(Function(x) Double.Parse(x)).ToArray()

        ' Koordinat dönüşüm matrisini hesapla
        Dim matrix As New Drawing2D.Matrix(values(0), values(1), values(2), values(3), values(4), values(5))

        ' Görüntüyü yükle
        Dim image As New Bitmap(imageFile)

        ' Görüntüyü konumlandır
        Dim destRect As New Rectangle(0, 0, image.Width, image.Height)
        Dim srcRect As New RectangleF(0, 0, image.Width, image.Height)
        Dim points As PointF() = {srcRect.Location, New PointF(srcRect.Right, srcRect.Top), New PointF(srcRect.Left, srcRect.Bottom)}
        matrix.TransformPoints(points)

        Dim destPoints As Point() = Array.ConvertAll(points, Function(p) New Point(CInt(p.X * 10000), CInt(-10000 * p.Y)))
        Dim destRegion As New Region(destRect)
        Dim srcRegion As New Region(srcRect)
        srcRegion.Transform(matrix)
        destRegion.Intersect(srcRegion.GetBounds(g))
        destPoints(0).X = destPoints(0).X - (values(0) * 10000 / 2)
        destPoints(0).Y = destPoints(0).Y + (values(3) * 10000 / 2)
        destPoints(1).Y = destPoints(1).Y + (values(3) * 10000 / 2)
        destPoints(2).X = destPoints(2).X - (values(0) * 10000 / 2)
        g.DrawImage(image, destPoints)
        Debug.Print(UBound(destPoints))
        Dim amtrx As Drawing2D.Matrix = g.Transform.Clone()
        Debug.Print(matrix.Elements(0).ToString)
        Debug.Print(matrix.Elements(1).ToString)
        Debug.Print(matrix.Elements(2).ToString)
        Debug.Print(matrix.Elements(3).ToString)
        Debug.Print(matrix.Elements(4).ToString)
        Debug.Print(matrix.Elements(5).ToString)
        'Debug.Print(amtrx.Elements.ToString)

    End Sub

    Private filter As FreeTransform = New FreeTransform()
    Private vertex As PointF() = New PointF(3) {}
    Private imageLocationField As Point = New Point()
    Function DrawImage2(pictureItem As Bitmap) As Bitmap
        DrawImage2 = New Bitmap(pictureItem.Clone, New Size(pictureItem.Size))
        If DrawImage2 IsNot Nothing Then
            filter.Bitmap = DrawImage2
            vertex(0) = New PointF(-1.13, -10.62)
            vertex(1) = New PointF(1648.44, -19.96)
            vertex(2) = New PointF(1665.49, 3072.71)
            vertex(3) = New PointF(15.92, 3082.04)

            filter.FourCorners = vertex
            DrawImage2 = filter.Bitmap
            If FileorFolderExists("D:\a.tif") = True Then
                My.Computer.FileSystem.DeleteFile("D:\a.tif")
            End If
            DrawImage2.Save("D:\a.tif")
            DrawImage2.Dispose()
            pictureItem.Dispose()
            pictureItem = Nothing
        End If
    End Function
End Class




Public Class Canvas
    Inherits Panel
    Public Sub New()
        Me.DoubleBuffered = True
        '  Me.
        ' Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
    End Sub
End Class




'' Author, Creator: by Bilgic
'' Created for cityGML. U can draw any shapes, lines in this form. If u use correct topology, u will have a gml. 
'' 23-11-2022
'' Created Elements (Line, Shape), Created Snap Mode
'' 24-11-2022
'' Transform Elements Calculation (Canvas2Local,Local2Canvas), snap will test
'' 25-11-2022
'' ShapeElement can be drawable now.
'' 26-11-2022
'' Can be add any image in canvas.
'' 27-11-2022
'' Image coordinated with tfw values.
'' 28-11-2022
'' Select line element and you can delete.
'' 29-11-2022
'' Snap can be use now.
'' 30-11-2022
'' ++++ snapList will add (+), addlastsnappoint (+), check mousepos is down 5 (+), addselectshape(+), startorfinish element on snappoint(?,zoom factor may be change.)
'' ++ elements info form (+)
'' Insert new vertex into the element.
'' 4-12-2022
'' Delete vertex (+), move vertex (+), will add move element property.

