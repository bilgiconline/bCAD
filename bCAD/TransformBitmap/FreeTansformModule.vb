Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace YLScsDrawing.Imaging.Filters
    Public Class FreeTransform
        Private vertex As PointF() = New PointF(3) {}
        Private AB, BC, CD, DA As YLScsDrawing.Geometry.Vector
        Private rect As Rectangle = New Rectangle()
        Private srcCB As Imaging.ImageData = New ImageData()
        Private srcW As Integer = 0
        Private srcH As Integer = 0

        Public Property Bitmap As Bitmap
            Set(ByVal value As Bitmap)
                Try
                    srcCB.FromBitmap(value)
                    srcH = value.Height
                    srcW = value.Width
                Catch
                    srcW = 0
                    srcH = 0
                End Try
            End Set
            Get
                Return getTransformedBitmap()
            End Get
        End Property

        Public Property ImageLocation As Point
            Set(ByVal value As Point)
                rect.Location = value
            End Set
            Get
                Return rect.Location
            End Get
        End Property

        Private isBilinear As Boolean = False
        Public Property IsBilinearInterpolation As Boolean
            Set(ByVal value As Boolean)
                isBilinear = value
            End Set
            Get
                Return isBilinear
            End Get
        End Property

        Public ReadOnly Property ImageWidth As Integer
            Get
                Return rect.Width
            End Get
        End Property

        Public ReadOnly Property ImageHeight As Integer
            Get
                Return rect.Height
            End Get
        End Property

        Public Property VertexLeftTop As PointF
            Set(ByVal value As PointF)
                vertex(0) = value
                setVertex()
            End Set
            Get
                Return vertex(0)
            End Get
        End Property

        Public Property VertexTopRight As PointF
            Set(ByVal value As PointF)
                vertex(1) = value
                setVertex()
            End Set
            Get
                Return vertex(1)
            End Get
        End Property

        Public Property VertexRightBottom As PointF
            Set(ByVal value As PointF)
                vertex(2) = value
                setVertex()
            End Set
            Get
                Return vertex(2)
            End Get
        End Property

        Public Property VertexBottomLeft As PointF
            Set(ByVal value As PointF)
                vertex(3) = value
                setVertex()
            End Set
            Get
                Return vertex(3)
            End Get
        End Property

        Public Property FourCorners As PointF()
            Set(ByVal value As PointF())
                vertex = value
                setVertex()
            End Set
            Get
                Return vertex
            End Get
        End Property

        Private Sub setVertex()
            Dim xmin = Single.MaxValue
            Dim ymin = Single.MaxValue
            Dim xmax = Single.MinValue
            Dim ymax = Single.MinValue

            For i = 0 To 3
                xmax = Math.Max(xmax, vertex(i).X)
                ymax = Math.Max(ymax, vertex(i).Y)
                xmin = Math.Min(xmin, vertex(i).X)
                ymin = Math.Min(ymin, vertex(i).Y)
            Next

            rect = New Rectangle(xmin, ymin, xmax - xmin, ymax - ymin)

            AB = New YLScsDrawing.Geometry.Vector(vertex(0), vertex(1))
            BC = New YLScsDrawing.Geometry.Vector(vertex(1), vertex(2))
            CD = New YLScsDrawing.Geometry.Vector(vertex(2), vertex(3))
            DA = New YLScsDrawing.Geometry.Vector(vertex(3), vertex(0))

            ' get unit vector
            AB /= AB.Magnitude
            BC /= BC.Magnitude
            CD /= CD.Magnitude
            DA /= DA.Magnitude
        End Sub

        Private Function isOnPlaneABCD(ByVal pt As PointF) As Boolean '  including point on border
            If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(0), vertex(1)) Then
                If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(1), vertex(2)) Then
                    If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(2), vertex(3)) Then
                        If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(3), vertex(0)) Then Return True
                    End If
                End If
            End If
            Return False
        End Function

        Private Function getTransformedBitmap() As Bitmap
            If srcH = 0 OrElse srcW = 0 Then Return Nothing

            Dim destCB As ImageData = New ImageData()
            destCB.A = New Byte(rect.Width - 1, rect.Height - 1) {}
            destCB.B = New Byte(rect.Width - 1, rect.Height - 1) {}
            destCB.G = New Byte(rect.Width - 1, rect.Height - 1) {}
            destCB.R = New Byte(rect.Width - 1, rect.Height - 1) {}


            Dim ptInPlane As PointF = New PointF()
            Dim x1, x2, y1, y2 As Integer
            Dim dab, dbc, dcd, dda As Double
            Dim dx1, dx2, dy1, dy2, dx1y1, dx1y2, dx2y1, dx2y2, nbyte As Single

            For y = 0 To rect.Height - 1
                For x = 0 To rect.Width - 1
                    Dim srcPt As Point = New Point(x, y)
                    srcPt.Offset(rect.Location)

                    If isOnPlaneABCD(srcPt) Then
                        dab = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(0), srcPt)).CrossProduct(AB))
                        dbc = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(1), srcPt)).CrossProduct(BC))
                        dcd = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(2), srcPt)).CrossProduct(CD))
                        dda = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(3), srcPt)).CrossProduct(DA))
                        ptInPlane.X = CSng(srcW * (dda / (dda + dbc)))
                        ptInPlane.Y = CSng(srcH * (dab / (dab + dcd)))

                        x1 = CInt(ptInPlane.X)
                        y1 = CInt(ptInPlane.Y)

                        If x1 >= 0 AndAlso x1 < srcW AndAlso y1 >= 0 AndAlso y1 < srcH Then
                            If isBilinear Then
                                x2 = If(x1 = srcW - 1, x1, x1 + 1)
                                y2 = If(y1 = srcH - 1, y1, y1 + 1)

                                dx1 = ptInPlane.X - x1
                                If dx1 < 0 Then dx1 = 0
                                dx1 = 1.0F - dx1
                                dx2 = 1.0F - dx1
                                dy1 = ptInPlane.Y - y1
                                If dy1 < 0 Then dy1 = 0
                                dy1 = 1.0F - dy1
                                dy2 = 1.0F - dy1

                                dx1y1 = dx1 * dy1
                                dx1y2 = dx1 * dy2
                                dx2y1 = dx2 * dy1
                                dx2y2 = dx2 * dy2


                                nbyte = srcCB.A(x1, y1) * dx1y1 + srcCB.A(x2, y1) * dx2y1 + srcCB.A(x1, y2) * dx1y2 + srcCB.A(x2, y2) * dx2y2
                                destCB.A(x, y) = nbyte
                                nbyte = srcCB.B(x1, y1) * dx1y1 + srcCB.B(x2, y1) * dx2y1 + srcCB.B(x1, y2) * dx1y2 + srcCB.B(x2, y2) * dx2y2
                                destCB.B(x, y) = nbyte
                                nbyte = srcCB.G(x1, y1) * dx1y1 + srcCB.G(x2, y1) * dx2y1 + srcCB.G(x1, y2) * dx1y2 + srcCB.G(x2, y2) * dx2y2
                                destCB.G(x, y) = nbyte
                                nbyte = srcCB.R(x1, y1) * dx1y1 + srcCB.R(x2, y1) * dx2y1 + srcCB.R(x1, y2) * dx1y2 + srcCB.R(x2, y2) * dx2y2
                                destCB.R(x, y) = nbyte
                            Else
                                destCB.A(x, y) = srcCB.A(x1, y1)
                                destCB.B(x, y) = srcCB.B(x1, y1)
                                destCB.G(x, y) = srcCB.G(x1, y1)
                                destCB.R(x, y) = srcCB.R(x1, y1)
                            End If
                        End If
                    End If
                Next
            Next
            Return destCB.ToBitmap()
        End Function
    End Class
End Namespace
