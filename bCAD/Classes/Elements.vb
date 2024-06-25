Imports System.Runtime.Remoting

Module Elements
    Class Point2D
        Dim pX As Double = Nothing
        Dim pY As Double = Nothing
        Property X As Integer
            Get
                Return pX
            End Get
            Set(value As Integer)
                pX = value
            End Set
        End Property
        Property Y As Integer
            Get
                Return pY
            End Get
            Set(value As Integer)
                pY = value
            End Set
        End Property
    End Class
    Class Line
        Private length As Double
        Private rotation As Double
        Dim sX As Double = Nothing
        Dim sY As Double = Nothing
        Dim eX As Double = Nothing
        Dim eY As Double = Nothing
        Dim clickZoom As Decimal
        Dim clickOffsetx As Integer
        Dim clickOffsetY As Integer
        Dim LevelName As String
        Dim colorPick As Color
        Dim lineWeightF As Integer
        Dim uniqueID As String
        Dim snapPointer As Boolean
        Dim snapDistanceInPr As Integer = 5


        Property snapDistance As Integer
            Get
                Return snapDistanceInPr
            End Get
            Set(value As Integer)
                snapDistanceInPr = value
            End Set
        End Property


        ReadOnly Property snapBool(px As Double, py As Double) As Boolean
            Get
                snapDistanceInPr = Form1.zoom * 15
                If snapDistanceInPr < 10 Then snapDistanceInPr = 10
                Dim SdistanceDiff As Double
                Dim EdistanceDiff As Double
                SdistanceDiff = Math.Sqrt((py - sY) ^ 2 + (px - sX) ^ 2)
                EdistanceDiff = Math.Sqrt((py - eY) ^ 2 + (px - eX) ^ 2)
                If SdistanceDiff < snapDistance Or EdistanceDiff < snapDistance Then
                    snapPointer = True
                Else
                    snapPointer = False
                End If
                Return snapPointer
            End Get
        End Property
        Public Sub New()   'constructor
            'Console.WriteLine("Object is being created")
        End Sub


        Property lineWeight As Integer
            Get
                Return lineWeightF
            End Get
            Set(value As Integer)
                lineWeightF = value
            End Set
        End Property

        Property Level As String
            Get
                Return LevelName
            End Get
            Set(value As String)
                LevelName = value
            End Set
        End Property

        Property color As Color
            Get
                Return colorPick
            End Get
            Set(value As Color)
                colorPick = value
            End Set
        End Property

        Property clickedZoom As Decimal
            Get
                Return clickZoom
            End Get
            Set(value As Decimal)
                clickZoom = value
            End Set
        End Property
        Property initoffsetX As Integer
            Get
                Return clickOffsetx
            End Get
            Set(value As Integer)
                clickOffsetx = value
            End Set
        End Property
        Property initoffsetY As Integer
            Get
                Return clickOffsetY
            End Get
            Set(value As Integer)
                clickOffsetY = value
            End Set
        End Property
        Property startPointX As Double
            Get
                Return sX
            End Get
            Set(value As Double)
                sX = value
            End Set
        End Property
        Property startPointY As Double
            Get
                Return sY
            End Get
            Set(value As Double)
                sY = value
            End Set
        End Property

        Property endPointX As Double
            Get
                Return eX
            End Get
            Set(value As Double)
                eX = value
            End Set
        End Property
        Property endPointY As Double
            Get
                Return eY
            End Get
            Set(value As Double)
                eY = value
            End Set
        End Property

        Public Function getLength() As Double
            Try
                length = Math.Sqrt(((eX - sX) ^ 2) + ((eY - sY) ^ 2))
            Catch ex As Exception
                length = 0
            End Try
            Return length
        End Function
        Public Function getRotate() As Double
            Try
                Dim RotatesUp As Double
                Dim RotatesDown As Double
                Dim ResultRotates As Double
                RotatesUp = eY - sY
                RotatesDown = eX - sX
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
                rotation = ResultRotates
            Catch ex As Exception

            End Try
            Return rotation
        End Function



    End Class

    Class Shape
        Implements IDisposable
#Region "IDisposable Support"
        Private disposedValue As Boolean
        Private length As Double
        Private rotation As Double
        Dim sX As Double = Nothing
        Dim sY As Double = Nothing
        Dim eX As Double = Nothing
        Dim eY As Double = Nothing
        Dim clickZoom As Decimal
        Dim clickOffsetx As Integer
        Dim clickOffsetY As Integer
        Dim LevelName As String
        Dim colorPick As Color
        Dim lineWeightF As Integer
        Dim fverticies As New List(Of Point)
        Dim areaInformation As Double
        Dim snapPointer As Boolean
        Dim snapDistanceInPr As Integer = 5
        Dim pCentroid As Point
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO#1: dispose managed state (managed objects).
                    GC.SuppressFinalize(Me)
                End If
                ' TODO#2: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO#3: set large fields to null.

            End If
            disposedValue = True
        End Sub
        Sub Dispose() Implements IDisposable.Dispose
            'GC.SuppressFinalize(Me)
            Dispose(True)

        End Sub
        Property snapDistance As Integer
            Get
                Return snapDistanceInPr
            End Get
            Set(value As Integer)
                snapDistanceInPr = value
            End Set
        End Property

        ReadOnly Property IsInRange(aPnt As Point) As Boolean
            Get
                CheckinRange(aPnt)
                Return snapPointer
            End Get
        End Property
        Sub Remove()
            RemoveRoom(Me)
        End Sub
        Sub CheckinRange(aPnt As Point)

        End Sub

        Function det(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
            Return x1 * y2 - x2 * y1
        End Function

        Function getIrregularPolygonArea() As Double
            If fverticies.Count < 3 Then
                Return 0
            End If
            Dim shapearea As Double
            shapearea = det(fverticies(fverticies.Count - 1).X, fverticies(fverticies.Count - 1).Y, fverticies(0).X, fverticies(0).Y)
            For i As Integer = 1 To fverticies.Count - 1
                shapearea += det(fverticies(i - 1).X, fverticies(i - 1).Y, fverticies(i).X, fverticies(i).Y)
            Next
            shapearea = shapearea / 2
            areaInformation = shapearea
            If areaInformation < 0 Then areaInformation = areaInformation * -1
            Return areaInformation
        End Function
        ReadOnly Property Area As Double
            Get
                getIrregularPolygonArea()
                Return areaInformation
            End Get
        End Property

        ReadOnly Property getVerticies As Point()
            Get
                Return fverticies.ToArray
            End Get
        End Property
        Public Sub New()   'constructor
            'Console.WriteLine("Object is being created")
        End Sub

        ReadOnly Property centroid As Point
            Get
                pCentroid = GetCentroidPoint(fverticies.ToArray)
                Return pCentroid
            End Get
        End Property
        ReadOnly Property verticiesCount As Integer
            Get
                Return fverticies.Count - 1
            End Get
        End Property

        Sub AddVertex(px As Double, py As Double)
            fverticies.Add(New Point(px, py))
        End Sub
        Sub AddVertex(px As Double, py As Double, InsertIndex As Integer)
            fverticies.Insert(InsertIndex, New Point(px, py))
        End Sub
        Sub DeleteVertex(px As Double, py As Double)
            Dim tmpDiff As Double
            Dim tmpD2 As Double
            Dim vertexNumber As Integer
            Dim m_Vertex As Point
            For i = 0 To fverticies.Count - 1
                tmpDiff = Distance2Pts(New Point(px, py), fverticies(i))
                If tmpDiff < 0 Then tmpDiff *= -1
                If i = 0 Then
                    m_Vertex = fverticies(0)
                    tmpD2 = tmpDiff
                    vertexNumber = 0
                Else
                    If tmpDiff < tmpD2 Then
                        m_Vertex = fverticies(i)
                        tmpD2 = tmpDiff
                        vertexNumber = i
                    End If
                End If
            Next
            If m_Vertex = New Point(startPointX, startPointY) Then
                startPointX = fverticies(1).X
                startPointY = fverticies(1).Y
                endPointX = fverticies(1).X
                endPointY = fverticies(1).Y
                fverticies.RemoveAt(0)
                fverticies.RemoveAt(fverticies.Count - 1)
            Else
                'fverticies.Remove(m_Vertex)
                fverticies.RemoveAt(vertexNumber)
            End If
        End Sub
        Sub DeleteVertex(VertexIndex As Integer)
            If VertexIndex = 0 Then
                startPointX = fverticies(1).X
                startPointY = fverticies(1).Y
                endPointX = fverticies(1).X
                endPointY = fverticies(1).Y
                fverticies.RemoveAt(0)
                fverticies.RemoveAt(fverticies.Count - 1)
            Else
                fverticies.RemoveAt(VertexIndex)
            End If
        End Sub
        Sub getCentroidwithTriangle()
            Dim centerX As Integer
            Dim centerY As Integer
            If fverticies.Count > 4 Then
                For i = 0 To fverticies.Count - 3
                    centerX = centerX + (fverticies(i).X * 0.333333) + (fverticies(i + 1).X * 0.333333) + (fverticies(i + 2).X * 0.333333)
                    centerY = centerY + (fverticies(i).Y * 0.333333) + (fverticies(i + 1).Y * 0.333333) + (fverticies(i + 2).Y * 0.333333)
                Next
            End If
            ' MsgBox((centerX / (fverticies.Count - 2)) & "," & (centerY / (fverticies.Count - 2)))
        End Sub
        Sub VertexSortCW()
            Dim lstPnts = New List(Of Point)()
            lstPnts = getVerticies.ToList
            Dim avgPoint As Point = New Point(lstPnts.Average(Function(t) t.X), lstPnts.Average(Function(t) t.Y))
            Dim ordered() As Point = lstPnts.OrderBy(Function(t) Math.Atan2(avgPoint.Y - t.Y, avgPoint.X - t.X)).ToArray()
            Dim ordered2() As Point = ordered.ToList.OrderBy(Function(t) Math.Atan2(avgPoint.Y - t.Y, avgPoint.X - t.X)).ToArray()
            fverticies = ordered2.ToList
        End Sub

        Sub RenewVertex(OldVertex As Point, NewVertex As Point)
            For i = 0 To fverticies.Count - 1
                If fverticies(i) = OldVertex Then
                    If i = 0 Or i = fverticies.Count - 1 Then
                        fverticies(0) = NewVertex
                        fverticies(fverticies.Count - 1) = NewVertex
                        sX = NewVertex.X
                        sY = NewVertex.Y
                        eX = NewVertex.X
                        eY = NewVertex.Y
                    Else
                        fverticies(i) = NewVertex
                    End If
                End If
            Next
        End Sub
        Sub DeleteSameVertices()
            Dim NewVertices As New List(Of Point)
            For i = 0 To fverticies.Count - 1
                If NewVertices.Contains(fverticies(i)) = False Then
                    NewVertices.Add(fverticies(i))
                End If
            Next
            fverticies = NewVertices
        End Sub
        Property lineWeight As Integer
            Get
                Return lineWeightF
            End Get
            Set(value As Integer)
                lineWeightF = value
            End Set
        End Property

        Property Level As String
            Get
                Return LevelName
            End Get
            Set(value As String)
                LevelName = value
            End Set
        End Property

        Property color As Color
            Get
                Return colorPick
            End Get
            Set(value As Color)
                colorPick = value
            End Set
        End Property

        Property clickedZoom As Decimal
            Get
                Return clickZoom
            End Get
            Set(value As Decimal)
                clickZoom = value
            End Set
        End Property
        Property initoffsetX As Integer
            Get
                Return clickOffsetx
            End Get
            Set(value As Integer)
                clickOffsetx = value
            End Set
        End Property
        Property initoffsetY As Integer
            Get
                Return clickOffsetY
            End Get
            Set(value As Integer)
                clickOffsetY = value
            End Set
        End Property
        Property startPointX As Double
            Get
                Return sX
            End Get
            Set(value As Double)
                sX = value
            End Set
        End Property
        Property startPointY As Double
            Get
                Return sY
            End Get
            Set(value As Double)
                sY = value
            End Set
        End Property

        Property endPointX As Double
            Get
                Return eX
            End Get
            Set(value As Double)
                eX = value
            End Set
        End Property
        Property endPointY As Double
            Get
                Return eY
            End Get
            Set(value As Double)
                eY = value
            End Set
        End Property
        Public Function getVertexIndex(Pt As Point) As Integer
            getVertexIndex = Nothing
            For i = 0 To fverticies.Count - 1
                If fverticies(i) = Pt Then
                    getVertexIndex = i
                    Exit For
                End If
            Next
            Return getVertexIndex
        End Function
        Public Function getLength() As Double
            Try
                length = 0
            Catch ex As Exception
                length = 0
            End Try
            Return length
        End Function


#End Region
    End Class
    Class Raster
        Dim sX As Double = Nothing
        Dim sY As Double = Nothing
        Dim eX As Double = Nothing
        Dim eY As Double = Nothing
        Dim pFullPath As String = Nothing
        Dim pName As String = Nothing
        Dim ptfwPath As String = Nothing
        Dim pSkewX As Double
        Dim pSkewY As Double
        Dim pPixelScaleX As Double
        Dim pPixelScaleY As Double
        Dim pPX As Integer
        Dim pPY As Integer
        Dim RasterPoints(0 To 2) As Point
        Dim protation As Double
        Dim m_bitmap As Bitmap

        Public Sub New(Path As String)
            pFullPath = Path
            If FileorFolderExists(pFullPath) = True Then
                pName = GetFileName(pFullPath, True)
                ptfwPath = GetFilePath(pFullPath) & GetFileName(pFullPath, False) & ".tfw"
                Using tmpBitmap As New Bitmap(pFullPath)
                    pPX = tmpBitmap.Width
                    pPY = tmpBitmap.Height
                    tmpBitmap.Dispose()
                End Using
                If FileorFolderExists(ptfwPath) = True Then
                    '(A')=A*Cos(angle)
                    '(B')=A*Sin(angle)
                    '(C')=-D*Sin(angle)
                    '(D')=D*Cos(angle)
                    '(E')=E
                    '(F')=F 
                    Dim tmpStream As String = StreamReaderProgram(ptfwPath)
                    pPixelScaleX = CDbl(tmpStream.Split(vbCrLf)(0))
                    pPixelScaleY = CDbl(tmpStream.Split(vbCrLf)(3))
                    pSkewX = CDbl(tmpStream.Split(vbCrLf)(1))
                    pSkewY = CDbl(tmpStream.Split(vbCrLf)(2))

                    sX = Math.Round((Math.Sqrt((pPixelScaleX ^ 2) + (pSkewX ^ 2)) * 0.5) - CDbl(tmpStream.Split(vbCrLf)(4)) * 1000)
                    sY = Math.Round((Math.Sqrt((pPixelScaleY ^ 2) + (pSkewY ^ 2)) * 0.5) + CDbl(tmpStream.Split(vbCrLf)(5))) * 1000
                    eX = Math.Round(((Math.Sqrt((pPixelScaleX ^ 2) + (pSkewX ^ 2))) * pPX) + CDbl(tmpStream.Split(vbCrLf)(4))) * 1000
                    eY = Math.Round(((Math.Sqrt((pPixelScaleY ^ 2) + (pSkewY ^ 2))) * pPY) + CDbl(tmpStream.Split(vbCrLf)(5))) * 1000
                    ' Debug.Print(eX)
                    If Not pSkewY + pSkewX = 0 Then
                        GetRotation()
                        ' pPixelScaleX = (1 / Math.Cos(protation * Math.PI / 180))
                        ' pPixelScaleY = (1 / Math.Sin(protation * Math.PI / 180))
                        sX = ((Math.Sqrt((PixelScaleX ^ 2) + (pSkewX ^ 2)) * 0.5) - CDbl(tmpStream.Split(vbCrLf)(4))) * -10000
                        sY = ((Math.Sqrt((PixelScaleY ^ 2) + (pSkewY ^ 2)) * 0.5) + CDbl(tmpStream.Split(vbCrLf)(5))) * 10000
                        eX = ((Math.Sqrt((PixelScaleX ^ 2) + (pSkewX ^ 2)) * pPX) + CDbl(tmpStream.Split(vbCrLf)(4))) * 10000
                        eY = ((Math.Sqrt((PixelScaleY ^ 2) + (pSkewY ^ 2)) * pPY) + CDbl(tmpStream.Split(vbCrLf)(5))) * 10000
                        'RasterPoints(0).X = sX
                        'RasterPoints(0).Y = sY
                        'RasterPoints(0) = RotationofAxes(RasterPoints(0), protation)
                        'RasterPoints(1).X = eX
                        'RasterPoints(1).Y = sY
                        'RasterPoints(1) = RotationofAxes(RasterPoints(1), protation)
                        'RasterPoints(2).X = sX
                        'RasterPoints(2).Y = eY
                        'RasterPoints(2) = RotationofAxes(RasterPoints(2), protation)
                    End If
                    'protation = 0
                    'RasterPoints(0) = RotationofAxes(RasterPoints(0), protation)
                    'RasterPoints(1) = RotationofAxes(RasterPoints(1), protation)
                    'RasterPoints(2) = RotationofAxes(RasterPoints(2), protation)
                Else
                    Dim NewTfwContent As String
                    NewTfwContent = "1" & vbCrLf & "0" & vbCrLf & "0" & vbCrLf & "-1" & vbCrLf & "0.5" & vbCrLf & "0.5"
                    StreamWriterProgram(ptfwPath, NewTfwContent)
                    sX = 0
                    sY = 0
                    eX = pPX
                    eY = pPY
                    pSkewX = 0
                    pSkewY = 0
                    protation = 0
                End If
            Else
                Exit Sub
            End If

            If pFullPath = Nothing Then
                Exit Sub
            End If
        End Sub

        Sub GetRotation()
            Dim result As Double
            result = pSkewX / pPixelScaleX
            Dim r2d As Double
            r2d = 180 / Math.PI
            protation = Math.Atan(result) * r2d
            If protation < 0 Then
                protation += 360
            End If
        End Sub
        Private Function RotationofAxes(OldPoint As Point, Angle As Double) As Point
            Dim d2r As Double
            Angle =  Angle
            d2r = Math.PI / 180
            RotationofAxes.X = (OldPoint.X * Math.Cos(Angle * d2r)) + (OldPoint.Y * Math.Sin(Angle * d2r))
            RotationofAxes.Y = (OldPoint.X * Math.Sin(Angle * d2r) * -1) + (OldPoint.Y * Math.Cos(Angle * d2r))
            Return RotationofAxes
        End Function
        ReadOnly Property Rotation As Double
            Get
                Return protation
            End Get
        End Property
        ReadOnly Property BoundryPoints As Point()
            Get
                Return RasterPoints
            End Get
        End Property
        Property Skew As Double
            Get
                Return pPixelScaleX
            End Get
            Set(value As Double)
                pPixelScaleX = value
            End Set
        End Property
        Property PixelScaleX As Double
            Get
                Return pPixelScaleX
            End Get
            Set(value As Double)
                pPixelScaleX = value
            End Set
        End Property
        Property PixelScaleY As Double
            Get
                Return pPixelScaleY
            End Get
            Set(value As Double)
                pPixelScaleY = value
            End Set
        End Property
        ReadOnly Property PixelCountX As Integer
            Get
                Return pPX
            End Get
        End Property
        ReadOnly Property PixelCountY As Integer
            Get
                Return pPY
            End Get
        End Property
        ReadOnly Property Name As String
            Get
                Return pName
            End Get
        End Property
        ReadOnly Property TFWPath As String
            Get
                Return ptfwPath
            End Get
        End Property
        Property FullPath As String
            Get
                Return pFullPath
            End Get
            Set(value As String)
                pFullPath = value
            End Set
        End Property

        Property startPointX As Double
            Get
                Return sX
            End Get
            Set(value As Double)
                sX = value
            End Set
        End Property
        Property startPointY As Double
            Get
                Return sY
            End Get
            Set(value As Double)
                sY = value
            End Set
        End Property

        Property endPointX As Double
            Get
                Return eX
            End Get
            Set(value As Double)
                eX = value
            End Set
        End Property
        Property endPointY As Double
            Get
                Return eY
            End Get
            Set(value As Double)
                eY = value
            End Set
        End Property

    End Class
End Module

