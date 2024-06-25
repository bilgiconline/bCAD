Imports System
Imports System.Drawing

Namespace YLScsDrawing.Geometry
    Public Structure Vector
        Private _x, _y As Double

        Public Sub New(ByVal x As Double, ByVal y As Double)
            _x = x
            _y = y
        End Sub
        Public Sub New(ByVal pt As PointF)
            _x = pt.X
            _y = pt.Y
        End Sub
        Public Sub New(ByVal st As PointF, ByVal [end] As PointF)
            _x = [end].X - st.X
            _y = [end].Y - st.Y
        End Sub

        Public Property X As Double
            Get
                Return _x
            End Get
            Set(ByVal value As Double)
                _x = value
            End Set
        End Property

        Public Property Y As Double
            Get
                Return _y
            End Get
            Set(ByVal value As Double)
                _y = value
            End Set
        End Property

        Public ReadOnly Property Magnitude As Double
            Get
                Return Math.Sqrt(X * X + Y * Y)
            End Get
        End Property

        Public Shared Operator +(ByVal v1 As Vector, ByVal v2 As Vector) As Vector
            Return New Vector(v1.X + v2.X, v1.Y + v2.Y)
        End Operator

        Public Shared Operator -(ByVal v1 As Vector, ByVal v2 As Vector) As Vector
            Return New Vector(v1.X - v2.X, v1.Y - v2.Y)
        End Operator

        Public Shared Operator -(ByVal v As Vector) As Vector
            Return New Vector(-v.X, -v.Y)
        End Operator

        Public Shared Operator *(ByVal c As Double, ByVal v As Vector) As Vector
            Return New Vector(c * v.X, c * v.Y)
        End Operator

        Public Shared Operator *(ByVal v As Vector, ByVal c As Double) As Vector
            Return New Vector(c * v.X, c * v.Y)
        End Operator

        Public Shared Operator /(ByVal v As Vector, ByVal c As Double) As Vector
            Return New Vector(v.X / c, v.Y / c)
        End Operator

        ' A * B =|A|.|B|.sin(angle AOB)
        Public Function CrossProduct(ByVal v As Vector) As Double
            Return _x * v.Y - v.X * _y
        End Function

        ' A. B=|A|.|B|.cos(angle AOB)
        Public Function DotProduct(ByVal v As Vector) As Double
            Return _x * v.X + _y * v.Y
        End Function

        Public Shared Function IsClockwise(ByVal pt1 As PointF, ByVal pt2 As PointF, ByVal pt3 As PointF) As Boolean
            Dim V21 As Vector = New Vector(pt2, pt1)
            Dim v23 As Vector = New Vector(pt2, pt3)
            Return V21.CrossProduct(v23) < 0 ' sin(angle pt1 pt2 pt3) > 0, 0<angle pt1 pt2 pt3 <180
        End Function

        Public Shared Function IsCCW(ByVal pt1 As PointF, ByVal pt2 As PointF, ByVal pt3 As PointF) As Boolean
            Dim V21 As Vector = New Vector(pt2, pt1)
            Dim v23 As Vector = New Vector(pt2, pt3)
            Return V21.CrossProduct(v23) > 0  ' sin(angle pt2 pt1 pt3) < 0, 180<angle pt2 pt1 pt3 <360
        End Function

        Public Shared Function DistancePointLine(ByVal pt As PointF, ByVal lnA As PointF, ByVal lnB As PointF) As Double
            Dim v1 As Vector = New Vector(lnA, lnB)
            Dim v2 As Vector = New Vector(lnA, pt)
            v1 /= v1.Magnitude
            Return Math.Abs(v2.CrossProduct(v1))
        End Function

        Public Sub Rotate(ByVal Degree As Integer)
            Dim radian = Degree * Math.PI / 180.0
            Dim sin = Math.Sin(radian)
            Dim cos = Math.Cos(radian)
            Dim nx = _x * cos - _y * sin
            Dim ny = _x * sin + _y * cos
            _x = nx
            _y = ny
        End Sub

        Public Function ToPointF() As PointF
            Return New PointF(_x, _y)
        End Function
    End Structure
End Namespace
