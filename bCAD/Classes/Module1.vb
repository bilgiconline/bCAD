Imports System.Drawing.Drawing2D
Imports System.Net
Imports ClipperLib
Imports TheArtOfDevHtmlRenderer.Adapters.Entities
Imports System.Linq
Imports Microsoft.Spatial
Imports System.Data.Spatial.DbGeometry
Imports System.Data.Entity.Spatial
Imports System.Data.Spatial
Imports System.Text
Imports BitMiracle.LibTiff.Classic


Module Module1
    Dim d As Microsoft.Spatial.GeographyPolygon
    Dim df As GeometryLineString
    Dim dff As GeographyPoint

    Public Sub ClosestLocation_WithThreePoints()
        Dim gebze = DbGeography.FromText("POLYGON(5 5, 10 5, 10 10, 5 10, 5 5)")
        Dim pendik = DbGeography.FromText("POLYGON(7 7, 12 7, 12 9, 7 9, 7 7)")
        Dim izmit = DbGeography.FromText("POINT(40.764946 29.950905)", 4326)
        MsgBox(gebze.ToString())
        MsgBox(gebze.Intersection(pendik))
        ' Dim gebze_pendik = gebze.Distance(pendik)
        ' Dim gebze_izmit = gebze.Distance(izmit)
        Dim asdf As GeographicCoordinate

    End Sub
    'Public Function IntersectionOf(ByVal line1 As Line, ByVal line2 As Line) As Point
    '    '  Fail if either line segment is zero-length.
    '    If line1.startPointX = line1.endPointX AndAlso line1.startPointY = line1.endPointY OrElse line2.startPointX = line2.endPointX AndAlso line2.startPointY = line2.endPointY Then Return New Point(Integer.MaxValue, Integer.MaxValue)

    '    If line1.startPointX = line2.startPointX AndAlso line1.startPointY = line2.startPointY OrElse line1.endPointX = line2.startPointX AndAlso line1.endPointY = line2.startPointY Then Return New Point(Integer.MaxValue, Integer.MaxValue)
    '    If line1.startPointX = line2.endPointX AndAlso line1.startPointY = line2.endPointY OrElse line1.endPointX = line2.endPointX AndAlso line1.endPointY = line2.endPointY Then Return New Point(Integer.MaxValue, Integer.MaxValue)

    '    '  (1) Translate the system so that point A is on the origin.
    '    line1.endPointX -= line1.startPointX
    '    line1.endPointY -= line1.startPointY
    '    line2.startPointX -= line1.startPointX
    '    line2.startPointY -= line1.startPointY
    '    line2.endPointX -= line1.startPointX
    '    line2.endPointY -= line1.startPointY

    '    '  Discover the length of segment A-B.
    '    Dim distAB = Math.Sqrt(line1.endPointX * line1.endPointX + line1.endPointY * line1.endPointY)

    '    '  (2) Rotate the system so that point B is on the positive X axis.
    '    Dim theCos As Double = line1.endPointX / distAB
    '    Dim theSin As Double = line1.endPointY / distAB
    '    Dim newX As Double = line2.startPointX * theCos + line2.startPointY * theSin
    '    line2.startPointY = line2.startPointY * theCos - line2.startPointX * theSin
    '    line2.startPointX = newX
    '    newX = line2.endPointX * theCos + line2.endPointY * theSin
    '    line2.endPointY = line2.endPointY * theCos - line2.endPointX * theSin
    '    line2.endPointX = newX

    '    '  Fail if segment C-D doesn't cross line A-B.
    '    If line2.startPointY < 0 AndAlso line2.endPointY < 0 OrElse line2.startPointY >= 0 AndAlso line2.endPointY >= 0 Then Return New Point(Integer.MaxValue, Integer.MaxValue)

    '    '  (3) Discover the position of the intersection point along line A-B.
    '    Dim posAB As Double = line2.endPointX + (line2.startPointX - line2.endPointX) * line2.endPointY / (line2.endPointY - line2.startPointY)

    '    '  Fail if segment C-D crosses line A-B outside of segment A-B.
    '    If posAB < 0 OrElse posAB > distAB Then Return New Point(Integer.MaxValue, Integer.MaxValue)

    '    '  (4) Apply the discovered position to line A-B in the original coordinate system.
    '    Return posAB
    'End Function


    Function testSh() As Shape
        testSh = Nothing
        dff = GeographyPoint.Create(3, 3)

        Dim asd As GeographyPolygon


        Return testSh
    End Function
    Function IntersectShapes(Intersector As Shape, IntersectArea As Shape, IntersectArea2 As Shape) As Shape
        Dim tmpShape1 As Shape
        Dim tmpShape2 As Shape
        Dim tmpShape3 As Shape
        Dim tmpShape4 As Shape
        Try
            tmpShape1 = ClipByClipper(Intersector, IntersectArea, ClipType.ctIntersection)
            tmpShape2 = ClipByClipper(tmpShape1, Intersector, ClipType.ctDifference)

            ' tmpShape3 = ClipByClipper(Intersector, IntersectArea2, ClipType.ctIntersection)

            ' tmpShape4 = ClipByClipper(IntersectArea2, tmpShape2, ClipType.ctDifference)
            tmpShape3 = ClipByClipper(Intersector, IntersectArea2, ClipType.ctIntersection)
            tmpShape4 = ClipByClipper(tmpShape3, tmpShape1, ClipType.ctDifference)
            If tmpShape4 IsNot Nothing Then
                tmpShape4.startPointX = tmpShape4.getVerticies(0).X
                tmpShape4.startPointY = tmpShape4.getVerticies(0).Y
                tmpShape4.endPointX = tmpShape4.getVerticies(0).X
                tmpShape4.endPointY = tmpShape4.getVerticies(0).Y
                tmpShape4.AddVertex(tmpShape4.getVerticies(0).X, tmpShape4.getVerticies(0).Y)
                tmpShape4.lineWeight = Intersector.lineWeight
                tmpShape4.color = Intersector.color
            End If
            IntersectShapes = tmpShape4
            Exit Function
        Catch ex As Exception

        End Try
        Return IntersectShapes
    End Function

    Function IntersectShapes(Intersector As Shape, IntersectArea As Shape) As Shape
        Dim tmpShape4 As Shape
        Try
            tmpShape4 = ClipByClipper(IntersectArea, Intersector, ClipType.ctDifference)
            If tmpShape4 IsNot Nothing Then
                tmpShape4.startPointX = tmpShape4.getVerticies(0).X
                tmpShape4.startPointY = tmpShape4.getVerticies(0).Y
                tmpShape4.endPointX = tmpShape4.getVerticies(0).X
                tmpShape4.endPointY = tmpShape4.getVerticies(0).Y
                tmpShape4.AddVertex(tmpShape4.getVerticies(0).X, tmpShape4.getVerticies(0).Y)
                tmpShape4.lineWeight = Intersector.lineWeight
                tmpShape4.color = Intersector.color
            End If
            IntersectShapes = tmpShape4
            Exit Function
        Catch ex As Exception

        End Try
        Return IntersectShapes
    End Function
    Function IntersectShapes2(Intersector As Shape, IntersectArea As Shape, IntersectArea2 As Shape) As Shape
        Dim tmpShape1 As Shape
        Dim tmpShape2 As Shape
        Dim tmpShape3 As Shape
        Dim tmpShape4 As Shape
        Try
            tmpShape1 = ClipByClipper(Intersector, IntersectArea, ClipType.ctIntersection)
            tmpShape2 = ClipByClipper(tmpShape1, Intersector, ClipType.ctDifference)

            ' tmpShape3 = ClipByClipper(Intersector, IntersectArea2, ClipType.ctIntersection)

            ' tmpShape4 = ClipByClipper(IntersectArea2, tmpShape2, ClipType.ctDifference)
            tmpShape3 = ClipByClipper(IntersectArea2, tmpShape2, ClipType.ctIntersection)
            tmpShape4 = ClipByClipper(tmpShape3, tmpShape2, ClipType.ctDifference)
            If tmpShape4 IsNot Nothing Then
                tmpShape4.startPointX = tmpShape4.getVerticies(0).X
                tmpShape4.startPointY = tmpShape4.getVerticies(0).Y
                tmpShape4.endPointX = tmpShape4.getVerticies(0).X
                tmpShape4.endPointY = tmpShape4.getVerticies(0).Y
                tmpShape4.AddVertex(tmpShape4.getVerticies(0).X, tmpShape4.getVerticies(0).Y)
                tmpShape4.lineWeight = Intersector.lineWeight
                tmpShape4.color = Intersector.color
            End If
            IntersectShapes2 = tmpShape4
            Exit Function
        Catch ex As Exception

        End Try
        Return IntersectShapes2
    End Function
    Public Function ClipByClipper(ByVal pol1 As Shape, ByVal pol2 As Shape, cliptypeCons As ClipType) As Shape
        Dim subd As List(Of IntPoint) = New List(Of IntPoint)
        '  subd.AddRange(pol2.getVerticies.ToList)
        For Each p In pol2.getVerticies
            subd.Add(New IntPoint(p.X, p.Y))
        Next
        Dim clip As List(Of IntPoint) = New List(Of IntPoint)
        '  clip.AddRange(pol1.getVerticies.ToList)
        For Each p In pol1.getVerticies
            clip.Add(New IntPoint(p.X, p.Y))
        Next
        Dim solution As List(Of List(Of IntPoint)) = New List(Of List(Of IntPoint))()
        Dim c As Clipper = New Clipper()
        c.AddPolygon(subd, PolyType.ptSubject)
        c.AddPolygon(clip, PolyType.ptClip)
        c.Execute(cliptypeCons, solution, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd)
        Dim ToReturn As Shape = New Shape()
        If solution.Count > 0 Then
            For Each p In solution(0)
                ToReturn.AddVertex(p.X, p.Y)
            Next
        End If
        Return ToReturn
    End Function


    Public Class GeographicCoordinate
        Private Const Tolerance As Double = 10.0 * 0.1

        Public Sub New(ByVal longitude As Double, ByVal latitude As Double)
            Me.Longitude = longitude
            Me.Latitude = latitude
        End Sub

        Public Property Latitude As Double
        Public Property Longitude As Double

        Public Shared Operator =(ByVal a As GeographicCoordinate, ByVal b As GeographicCoordinate) As Boolean
            ' If both are null, or both are same instance, return true.
            If ReferenceEquals(a, b) Then
                Return True
            End If

            ' If one is null, but not both, return false.
            If a Is Nothing OrElse b Is Nothing Then
                Return False
            End If

            Dim latResult = Math.Abs(a.Latitude - b.Latitude)
            Dim lonResult = Math.Abs(a.Longitude - b.Longitude)
            Return latResult < Tolerance AndAlso lonResult < Tolerance
        End Operator

        Public Shared Operator <>(ByVal a As GeographicCoordinate, ByVal b As GeographicCoordinate) As Boolean
            Return Not a Is b
        End Operator

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            ' Check for null values and compare run-time types.
            If obj Is Nothing OrElse [GetType]() IsNot obj.GetType() Then
                Return False
            End If

            Dim p = CType(obj, GeographicCoordinate)
            Dim latResult = Math.Abs(Latitude - p.Latitude)
            Dim lonResult = Math.Abs(Longitude - p.Longitude)
            Return latResult < Tolerance AndAlso lonResult < Tolerance
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (Latitude.GetHashCode() * 397) Xor Longitude.GetHashCode()
        End Function
        Public Shared Function ConvertStringArrayToGeographicCoordinates(ByVal pointString As String) As IEnumerable(Of GeographicCoordinate)
            Dim points = pointString.Split(","c)
            Dim coordinates = New List(Of GeographicCoordinate)()

            For i = 0 To points.Length / 2 - 1
                Dim geoPoint = points.Skip(i * 2).Take(2).ToList()
                coordinates.Add(New GeographicCoordinate(Double.Parse(geoPoint.First()), Double.Parse(geoPoint.Last())))
            Next

            Return coordinates
        End Function
        Public Shared Function ConvertGeoCoordinatesToPolygon(ByVal coordinates As IEnumerable(Of GeographicCoordinate)) As DbGeography
            Dim coordinateList = coordinates.ToList()
            If coordinateList.First() IsNot coordinateList.Last() Then
                Throw New Exception("First and last point do not match. This is not " & vbTab & vbTab & "a valid polygon")
            End If

            Dim count = 0
            Dim sb = New StringBuilder()
            sb.Append("POLYGON((")
            For Each coordinate In coordinateList
                If count = 0 Then
                    sb.Append(coordinate.Longitude & " " + coordinate.Latitude)
                Else
                    sb.Append("," & coordinate.Longitude & " " + coordinate.Latitude)
                End If

                count += 1
            Next

            sb.Append("))")

            Return DbGeography.PolygonFromText(sb.ToString(), 4326)
        End Function




    End Class

End Module
