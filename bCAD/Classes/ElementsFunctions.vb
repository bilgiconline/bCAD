Module ElementsFunctions
    Function PointInPolygon(p As Point, poly As Point()) As Boolean
        Dim cn As Integer = 0
        For i As Integer = 0 To poly.Length - 1
            If ((poly(i).Y <= p.Y) AndAlso (poly((i + 1) Mod poly.Length).Y > p.Y)) OrElse ((poly(i).Y > p.Y) AndAlso (poly((i + 1) Mod poly.Length).Y <= p.Y)) Then
                Dim vt As Single = CSng(p.Y - poly(i).Y) / (poly((i + 1) Mod poly.Length).Y - poly(i).Y)
                If p.X < poly(i).X + vt * (poly((i + 1) Mod poly.Length).X - poly(i).X) Then cn += 1
            End If
        Next
        Return (cn Mod 2 = 1)
    End Function
    Function Distance2Pts(Pt1 As Point, Pt2 As Point) As Double
        Distance2Pts = Math.Sqrt(((Pt2.X - Pt1.X) ^ 2) + ((Pt2.Y - Pt1.Y) ^ 2))
        If Distance2Pts < 0 Then Distance2Pts = Distance2Pts * -1
        Return Distance2Pts
    End Function
    Function RotatelineSupport(Sp As Point, Ep As Point) As Point
        RotatelineSupport.X = Ep.X
        RotatelineSupport.Y = Ep.Y
        Dim RotatesUp As Double
        Dim RotatesDown As Double
        Dim ResultRotates As Double
        RotatesUp = Ep.Y - Sp.Y
        RotatesDown = Ep.X - Sp.X
        ' +/+ +/- -/- -/+
        If RotatesUp > 0 And RotatesDown > 0 Then
            ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
        ElseIf RotatesUp > 0 And RotatesDown < 0 Then
            ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
            ResultRotates += 180
        ElseIf RotatesUp < 0 And RotatesDown < 0 Then
            ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
            ResultRotates += 180
        ElseIf RotatesUp < 0 And RotatesDown > 0 Then
            ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
            ResultRotates += 360
        ElseIf RotatesUp = 0 Then
            ResultRotates = 0
            Exit Function
        ElseIf RotatesDown = 0 Then
            ResultRotates = 90
            Exit Function
        End If
        If ResultRotates < 0 Then ResultRotates += 360
        Dim tmpLength As Double
        Try
            tmpLength = Math.Sqrt(((RotatesUp) ^ 2) + ((RotatesDown) ^ 2))
        Catch ex As Exception
            tmpLength = 0
        End Try

        If ResultRotates > 89 And ResultRotates < 91 Then
            RotatelineSupport.X = Sp.X
            RotatelineSupport.Y = Sp.Y + tmpLength
            Exit Function
        End If

        If ResultRotates > 179 And ResultRotates < 181 Then
            RotatelineSupport.X = Sp.X - tmpLength
            RotatelineSupport.Y = Sp.Y
            Exit Function
        End If

        If ResultRotates > 269 And ResultRotates < 271 Then
            RotatelineSupport.X = Sp.X
            RotatelineSupport.Y = Sp.Y - tmpLength
            Exit Function
        End If

        If ResultRotates > 0 And ResultRotates < 1 Or ResultRotates > 359 Then
            RotatelineSupport.X = tmpLength + Sp.X
            RotatelineSupport.Y = Sp.Y
            Exit Function
        End If

        Return RotatelineSupport
    End Function
    Function getRotate(Pt1 As Point, Pt2 As Point) As Double
        Try
            getRotate = 0
            Dim RotatesUp As Double
            Dim RotatesDown As Double
            Dim ResultRotates As Double
            RotatesUp = (-1 * Pt2.Y) - (-1 * Pt1.Y)
            RotatesDown = Pt2.X - Pt1.X
            ' +/+ +/- -/- -/+
            If RotatesUp > 0 And RotatesDown > 0 Then
                ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
            ElseIf RotatesUp > 0 And RotatesDown < 0 Then
                ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
                ResultRotates += 180
            ElseIf RotatesUp < 0 And RotatesDown < 0 Then
                ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
                ResultRotates += 180
            ElseIf RotatesUp < 0 And RotatesDown > 0 Then
                ResultRotates = Math.Atan(RotatesUp / RotatesDown) * (180 / Math.PI)
                ResultRotates += 360
            ElseIf RotatesUp = 0 Then
                ResultRotates = 0
            ElseIf RotatesDown = 0 Then
                ResultRotates = 90
            End If
            getRotate = ResultRotates
        Catch ex As Exception

        End Try
        Return getRotate
    End Function
    Function GetCentroidPoint(Points() As Point) As Point
        GetCentroidPoint = Nothing
        Dim collectX As Double
        Dim collectY As Double
        For i = 0 To UBound(Points)
            collectX = collectX + Points(i).X
            collectY = collectY + Points(i).Y
        Next
        GetCentroidPoint = New Point(collectX / (Points.Length), -1 * (collectY / (Points.Length)))
        Return GetCentroidPoint
    End Function
    Function GetBetweenCoordinatesInShape(ShapeCoordinates() As Point, addVertex As Point) As List(Of Point)
        Dim CentroidPt As Point
        GetBetweenCoordinatesInShape = New List(Of Point)
        CentroidPt = GetCentroid(ShapeCoordinates)
        CentroidPt.Y = -1 * CentroidPt.Y
        Dim activeAngle As Double = getRotate(addVertex, CentroidPt)
        Dim tmpAngle As Double = activeAngle
        Debug.Print("activeClickPointAngle: " & tmpAngle)
        '  Dim tmpListPolarCoordinates As New List(Of String)
        For i = 0 To ShapeCoordinates.Count - 2
            'tmpListPolarCoordinates.Add(getRotate(ShapeCoordinates(i), CentroidPt) & "|" & Distance2Pts(ShapeCoordinates(i), CentroidPt))
            Dim getAngle1, getAngle2 As Double
            ' Debug.Print(ShapeCoordinates(i).X & "|" & ShapeCoordinates(i).Y)
            getAngle1 = getRotate(New Point(ShapeCoordinates(i).X, -1 * ShapeCoordinates(i).Y), CentroidPt)
            getAngle2 = getRotate(New Point(ShapeCoordinates(i + 1).X, -1 * ShapeCoordinates(i + 1).Y), CentroidPt)
            Debug.Print("Shape Points Angle: " & getAngle1 & "|" & getAngle2)
            If getAngle1 > 270 And getAngle2 < 90 Then
                'If tmpAngle < getAngle1 And getAngle2 > tmpAngle Then
                GetBetweenCoordinatesInShape.Clear()
                GetBetweenCoordinatesInShape.Add(ShapeCoordinates(i))
                GetBetweenCoordinatesInShape.Add(ShapeCoordinates(i + 1))
                ' Exit For
                ' End If
            End If
            If tmpAngle < getAngle2 And getAngle1 < tmpAngle Then
                ' Debug.Print("Centroid: " & CentroidPt.X & "|" & CentroidPt.Y)
                'Debug.Print("activeClickPoint: " & addVertex.X & "|" & addVertex.Y)
                'Debug.Print("activeClickPointAngle: " & activeAngle)
                'Debug.Print("Shape Points: " & ShapeCoordinates(i).X & "," & ShapeCoordinates(i).Y & "|" & ShapeCoordinates(i + 1).X & "," & ShapeCoordinates(i + 1).Y)
                'Debug.Print("Shape Points Angle: " & getAngle1 & "|" & getAngle2)
                GetBetweenCoordinatesInShape.Clear()
                GetBetweenCoordinatesInShape.Add(ShapeCoordinates(i))
                GetBetweenCoordinatesInShape.Add(ShapeCoordinates(i + 1))
                Exit For
            End If
        Next
        Return GetBetweenCoordinatesInShape
    End Function
    Public Function GetCentroid(ByVal nodes As Point()) As Point
        Dim count As Integer = nodes.Length
        Dim k As Double, x As Double = 0, y As Double = 0, area As Double = 0
        Dim a As Point, b As Point = nodes(count - 1)

        For i As Integer = 0 To count - 1
            a = nodes(i)
            k = a.Y * b.X - a.X * b.Y
            area += k
            x += (a.X + b.X) * k
            y += (a.Y + b.Y) * k
            b = a
        Next
        area = area * 3
        Return If((area = 0), Point.Empty, New Point((x / area), (y / area)))
    End Function
    Public Function GetVertices(ActiveShape As Shape) As List(Of Point)
        GetVertices = New List(Of Point)
        If Form1.SnappedList.Count > 0 Then
            For i As Integer = 0 To ActiveShape.verticiesCount
                If ActiveShape.getVerticies(i) = New Point(Form1.SnappedList(0).X, -1 * Form1.SnappedList(0).Y) Then
                    If i = 0 Then
                        GetVertices.Add(ActiveShape.getVerticies(ActiveShape.verticiesCount - 1))
                        GetVertices.Add(ActiveShape.getVerticies(i + 1))
                        GetVertices.Add(ActiveShape.getVerticies(i))
                    ElseIf i = ActiveShape.verticiesCount Then
                        GetVertices.Add(ActiveShape.getVerticies(ActiveShape.verticiesCount - 1))
                        GetVertices.Add(ActiveShape.getVerticies(1))
                        GetVertices.Add(ActiveShape.getVerticies(i))
                    Else
                        GetVertices.Add(ActiveShape.getVerticies(i - 1))
                        GetVertices.Add(ActiveShape.getVerticies(i + 1))
                        GetVertices.Add(ActiveShape.getVerticies(i))
                    End If
                End If
            Next
        End If
        Return GetVertices
    End Function
    Public Function DiffOtherPoints(ActiveShape As Shape, ClickPoint As Point) As List(Of Point)
        DiffOtherPoints = New List(Of Point)
        Dim vIndex As Integer = Nothing
        vIndex = ActiveShape.getVertexIndex(ClickPoint)

        If vIndex = 0 Or vIndex = ActiveShape.verticiesCount Then
            For i As Integer = 0 To ActiveShape.verticiesCount
                Dim tmpPoint As Point
                tmpPoint.X = ClickPoint.X - (ActiveShape.getVerticies(i).X)
                tmpPoint.Y = ClickPoint.Y - (ActiveShape.getVerticies(i).Y)
                DiffOtherPoints.Add(tmpPoint)
            Next
        Else
            For k As Integer = vIndex + 1 To ActiveShape.verticiesCount
                Dim tmpPoint As Point
                tmpPoint.X = ClickPoint.X - (ActiveShape.getVerticies(k).X)
                tmpPoint.Y = ClickPoint.Y - (ActiveShape.getVerticies(k).Y)
                DiffOtherPoints.Add(tmpPoint)
            Next
            For k As Integer = 0 To vIndex - 1
                Dim tmpPoint As Point
                tmpPoint.X = ClickPoint.X - (ActiveShape.getVerticies(k).X)
                tmpPoint.Y = ClickPoint.Y - (ActiveShape.getVerticies(k).Y)
                DiffOtherPoints.Add(tmpPoint)
            Next
        End If

        Return DiffOtherPoints
    End Function
    Sub MoveShape(ActiveShape As Shape, MovedEdge As Point, ClickPoint As Point)
        Dim pointsDiff As Point
        pointsDiff = MovedEdge - ClickPoint
        For i = 0 To ActiveShape.verticiesCount - 1
            ActiveShape.RenewVertex(ActiveShape.getVerticies(i), ActiveShape.getVerticies(i) - pointsDiff)
        Next
    End Sub
    Function simplifiedRectangles2PolygonPoints(Rectangles() As RectangleF) As Point()
        Dim listPoints As New List(Of Point)
        'For Each rect As RectangleF In Rectangles
        '    listPoints.Add(New Point(rect.Location.X, rect.Location.Y))
        'Next
        For Each rect As RectangleF In Rectangles
            listPoints.Add(New Point(rect.Location.X + rect.Width, rect.Location.Y))
        Next

        'For Each rect As RectangleF In Rectangles
        '    listPoints.Add(New Point(rect.Location.X, rect.Location.Y + rect.Height))
        'Next
        Try
            For i = 0 To listPoints.Count - 2
                Dim CheckDirection1 As Double = Math.Round(getRotate(listPoints(i), listPoints(i + 1)), 1)
                Dim CheckDirection2 As Double = Math.Round(getRotate(listPoints(i), listPoints(i + 2)), 1)
                If CheckDirection1 = CheckDirection2 Then
                    listPoints.RemoveAt(i + 1)
                    i = i - 1
                End If
            Next
        Catch ex As Exception

        End Try
        For Each rect As RectangleF In Rectangles
            listPoints.Add(New Point(rect.Location.X + rect.Width, rect.Location.Y + rect.Height))
        Next
        Try
            For i = 0 To listPoints.Count - 2
                Dim CheckDirection1 As Double = Math.Round(getRotate(listPoints(i), listPoints(i + 1)), 4)
                Dim CheckDirection2 As Double = Math.Round(getRotate(listPoints(i), listPoints(i + 2)), 4)
                If CheckDirection1 = CheckDirection2 Then
                    listPoints.RemoveAt(i + 1)
                    i = i - 1
                End If
            Next
        Catch ex As Exception

        End Try
        ' listPoints = listPoints.Distinct.ToList
        ' listPoints.Sort()
        '    listPoints = listPoints.OrderBy(Function(x) Math.Atan2(x.X, x.Y)).ToList()
        Try
            For i = 0 To listPoints.Count - 1
                If listPoints(i) = listPoints(i + 1) Then
                    listPoints.RemoveAt(i + 1)
                    i = i - 1
                End If
            Next
        Catch ex As Exception

        End Try
        simplifiedRectangles2PolygonPoints = listPoints.ToArray
        Debug.Print(simplifiedRectangles2PolygonPoints.Length - 1)
        Return simplifiedRectangles2PolygonPoints
    End Function
    Function GetLineIntersectXY1(ByVal Line1_Point1 As Point, ByVal Line1_Point2 As Point, ByVal Line2_Point1 As Point, ByVal Line2_Point2 As Point) As Point
        'Equation of Line : y = mx + c
        Dim result As Point = New Point(Integer.MaxValue, Integer.MaxValue)
        Dim m1 As Double = 0
        Dim m2 As Double = 0
        Dim c1 As Double = 0
        Dim c2 As Double = 0
        Dim Temp As Double = 0

        If Line1_Point2.X - Line1_Point1.X <> 0 AndAlso Line2_Point2.X - Line2_Point1.X <> 0 Then
            m1 = (Line1_Point2.Y - Line1_Point1.Y) / (Line1_Point2.X - Line1_Point1.X)
            m2 = (Line2_Point2.Y - Line2_Point1.Y) / (Line2_Point2.X - Line2_Point1.X)
            c1 = Line1_Point1.Y - m1 * Line1_Point1.X
            c2 = Line2_Point1.Y - m2 * Line2_Point1.X

            If 0 = m2 Then 'Cannot divide a value by zero in Mathematics
                Temp = m1
                m1 = m2
                m2 = Temp
                Temp = c1
                c1 = c2
                c2 = Temp
            End If
            If m2 - m1 = 0 Then
                GetLineIntersectXY1 = New Point(Integer.MaxValue, Integer.MaxValue)
                Exit Function
            End If
            result.X = (c1 - c2) / (m2 - m1)
            result.Y = (m1 * c2 / m2 - c1) / (m1 / m2 - 1)
            '   result.Z = 0
        ElseIf Line1_Point2.X - Line1_Point1.X = 0 AndAlso Line2_Point2.X - Line2_Point1.X <> 0 Then
            m2 = (Line2_Point2.Y - Line2_Point1.Y) / (Line2_Point2.X - Line2_Point1.X)
            c2 = Line2_Point1.Y - m2 * Line2_Point1.X

            result.X = Line1_Point1.X
            result.Y = m2 * Line1_Point1.X + c2
            '  result.Z = 0
        ElseIf Line1_Point2.X - Line1_Point1.X <> 0 AndAlso Line2_Point2.X - Line2_Point1.X = 0 Then
            m1 = (Line1_Point2.Y - Line1_Point1.Y) / (Line1_Point2.X - Line1_Point1.X)
            c1 = Line1_Point1.Y - m1 * Line1_Point1.X

            result.X = Line2_Point1.X
            result.Y = m1 * Line2_Point1.X + c1
            'result.Z = 0
        Else
            result = New Point(Integer.MaxValue, Integer.MaxValue)
            Exit Function
        End If
        Return result
    End Function
    Function lineLineIntersection(ByVal A As Point, ByVal B As Point, ByVal C As Point, ByVal D As Point) As Point
        'Line AB represented as a1x + b1y = c1
        Dim a1 As Double = B.Y - A.Y
        Dim b1 As Double = A.X - B.X
        Dim c1 As Double = a1 * A.X + b1 * A.Y

        ' Line CD represented as a2x + b2y = c2
        Dim a2 As Double = D.Y - C.Y
        Dim b2 As Double = C.X - D.X
        Dim c2 As Double = a2 * C.X + b2 * C.Y

        Dim determinant = a1 * b2 - a2 * b1

        If determinant = 0 Then
            ' The lines are parallel. This is simplified
            ' by returning a pair of FLT_MAX
            Return New Point(Integer.MaxValue, Integer.MaxValue)
        Else
            Dim x = (b2 * c1 - b1 * c2) / determinant
            Dim y = (a1 * c2 - a2 * c1) / determinant
            Return New Point(x, y)
        End If

        ''Dim c1 As Double
        ''Dim c2 As Double
        ''c1 = A.X + B.Y
        ''c2 = C.X + D.Y

        ''If (A.X * D.Y) - (C.Y * B.X) = 0 Then
        ''    Return New Point(Integer.MaxValue, Integer.MaxValue)
        ''Else
        ''    Dim x As Double = ((c1 * D.Y) - (B.X * c2)) / ((A.X * D.Y))
        ''    Dim y As Double = A.Y + (dy1 / dx1) * (x - A.X)
        ''    p = New Point(x, y)
        ''    Return p
        ''End If


        'Dim dy1 As Double = B.Y - A.Y
        'Dim dx1 As Double = B.X - A.X
        'Dim dy2 As Double = D.Y - C.Y
        'Dim dx2 As Double = D.X - C.X
        'Dim p As New Point
        ''check whether the two line parallel
        'If dy1 * dx2 = dy2 * dx1 Then
        '    'MessageBox.Show("no point")
        '    'Return P with a specific data
        '    Return New Point(Integer.MaxValue, Integer.MaxValue)
        'Else
        '    ' (a.x+b.y=c1, c.x+d.y=c2. if  a1b2−a2b1=0)
        '    Dim x As Double = ((C.Y - A.Y) * dx1 * dx2 + dy1 * dx2 * A.X - dy2 * dx1 * C.X) / (dy1 * dx2 - dy2 * dx1)
        '    Dim y As Double = A.Y + (dy1 / dx1) * (x - A.X)
        '    Dim CheckDirection1 As Double = Math.Round(getRotate(C, D), 2)
        '    Dim CheckDirection2 As Double = Math.Round(getRotate(C, New Point(x, y)), 2)
        '    If CheckDirection1 = CheckDirection2 Then
        '        p = New Point(x, y)
        '        Return p
        '    Else
        '        Return New Point(Integer.MaxValue, Integer.MaxValue)
        '    End If
        'End If
    End Function
    Function MoveOnShapeLineCheckForPoint(Pt As Point, Points() As Point) As Boolean
        Dim tmpPt As Point
        tmpPt = New Point(Pt.X, Pt.Y + 1)
        If PointInPolygon(tmpPt, Points) = True Then
            MoveOnShapeLineCheckForPoint = True
            Exit Function
        End If
        tmpPt = New Point(Pt.X, Pt.Y + 1)
        If PointInPolygon(tmpPt, Points) = True Then
            MoveOnShapeLineCheckForPoint = True
            Exit Function
        End If
        tmpPt = New Point(Pt.X, Pt.Y - 1)
        If PointInPolygon(tmpPt, Points) = True Then
            MoveOnShapeLineCheckForPoint = True
            Exit Function
        End If
        tmpPt = New Point(Pt.X - 1, Pt.Y)
        If PointInPolygon(tmpPt, Points) = True Then
            MoveOnShapeLineCheckForPoint = True
            Exit Function
        End If
        Return MoveOnShapeLineCheckForPoint
    End Function
    Function GetIntersectionShape(Intersector As Shape, IntersectionShape1 As Shape, IntersectionShape2 As Shape) As Shape
        GetIntersectionShape = New Shape
        Dim getShapePoint1 As New List(Of Point)
        Dim getShapePoint2 As New List(Of Point)
        Dim getShapePoint3 As New List(Of Point)
        getShapePoint1 = Intersector.getVerticies.ToList
        getShapePoint2 = IntersectionShape1.getVerticies.ToList
        getShapePoint3 = IntersectionShape2.getVerticies.ToList

        '---GetIntersectionArea1
        For i = 0 To getShapePoint1.Count - 2
            For k = 0 To getShapePoint2.Count - 2
                Dim tmpP As Point = GetLineIntersectXY1(getShapePoint1(i), getShapePoint1(i + 1), getShapePoint2(k), getShapePoint2(k + 1))
                If Not tmpP.X = Integer.MaxValue Or Not tmpP.Y = Integer.MaxValue Then
                    If Not tmpP.X = 0 And Not tmpP.Y = 0 Then
                        If GetIntersectionShape.getVerticies.ToList.Contains(tmpP) = False Then
                            If PointInPolygon(tmpP, Intersector.getVerticies) = True And MoveOnShapeLineCheckForPoint(tmpP, IntersectionShape1.getVerticies) = True Then
                                GetIntersectionShape.AddVertex(tmpP.X, tmpP.Y)
                            Else
                                If MoveOnShapeLineCheckForPoint(tmpP, Intersector.getVerticies) = True And MoveOnShapeLineCheckForPoint(tmpP, IntersectionShape1.getVerticies) = True Then
                                    GetIntersectionShape.AddVertex(tmpP.X, tmpP.Y)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Next
        For i = 0 To getShapePoint2.Count - 2
            If MoveOnShapeLineCheckForPoint(IntersectionShape1.getVerticies(i), Intersector.getVerticies) = True Then
                GetIntersectionShape.AddVertex(IntersectionShape1.getVerticies(i).X, IntersectionShape1.getVerticies(i).Y)
            End If
        Next


        '---GetIntersectionArea2
        For i = 0 To getShapePoint1.Count - 2
            For k = 0 To getShapePoint3.Count - 2
                Dim tmpP As Point = GetLineIntersectXY1(getShapePoint1(i), getShapePoint1(i + 1), getShapePoint3(k), getShapePoint3(k + 1))
                If Not tmpP.X = Integer.MaxValue Or Not tmpP.Y = Integer.MaxValue Then
                    If Not tmpP.X = 0 And Not tmpP.Y = 0 Then
                        If GetIntersectionShape.getVerticies.ToList.Contains(tmpP) = False Then
                            If PointInPolygon(tmpP, Intersector.getVerticies) = True And MoveOnShapeLineCheckForPoint(tmpP, IntersectionShape2.getVerticies) = True Then
                                GetIntersectionShape.AddVertex(tmpP.X, tmpP.Y)
                            Else
                                If MoveOnShapeLineCheckForPoint(tmpP, Intersector.getVerticies) = True And MoveOnShapeLineCheckForPoint(tmpP, IntersectionShape2.getVerticies) = True Then
                                    GetIntersectionShape.AddVertex(tmpP.X, tmpP.Y)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Next
        For i = 0 To getShapePoint3.Count - 2
            If MoveOnShapeLineCheckForPoint(IntersectionShape2.getVerticies(i), Intersector.getVerticies) = True Then
                GetIntersectionShape.AddVertex(IntersectionShape2.getVerticies(i).X, IntersectionShape2.getVerticies(i).Y)
            End If
        Next

        '--If points count bigger then 3, create shape else result is nothing. 
        If UBound(GetIntersectionShape.getVerticies) > 2 Then
            GetIntersectionShape.lineWeight = Intersector.lineWeight
            GetIntersectionShape.color = Intersector.color
            GetIntersectionShape.VertexSortCW()
            GetIntersectionShape.AddVertex(GetIntersectionShape.getVerticies(0).X, GetIntersectionShape.getVerticies(0).Y)
            GetIntersectionShape.startPointX = GetIntersectionShape.getVerticies(0).X
            GetIntersectionShape.startPointY = GetIntersectionShape.getVerticies(0).Y
            GetIntersectionShape.endPointY = GetIntersectionShape.getVerticies(0).Y
            GetIntersectionShape.endPointY = GetIntersectionShape.getVerticies(0).Y
        Else
            GetIntersectionShape = Nothing
        End If
        Return GetIntersectionShape
    End Function

    Function GetRoomIntersect(Intersector As Shape, ActiveLayerName As String) As Shape
        Dim ListActiveShapes As List(Of Shape)
        ListActiveShapes = Storage.GetDrawnShapeActiveLayerItems(ActiveLayerName)
        For i = 0 To ListActiveShapes.Count - 1
            For k = 0 To ListActiveShapes.Count - 1
                If ListActiveShapes(i) IsNot ListActiveShapes(k) Then
                    Dim tmpShp As Shape = GetIntersectionShape(Intersector, ListActiveShapes(i), ListActiveShapes(k))
                    If tmpShp IsNot Nothing Then
                        GetRoomIntersect = tmpShp

                    End If
                End If
            Next
        Next
        Return GetRoomIntersect
    End Function
End Module
