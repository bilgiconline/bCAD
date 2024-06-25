Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports bCAD.YLScsDrawing.Imaging.Filters

Public Class CanvasModule


    Private filter As FreeTransform = New FreeTransform()
    Private recthandle As RectangleF() = New RectangleF(3) {}
    Private vertex As PointF() = New PointF(3) {}

    Private originalCanvas As Rectangle = New Rectangle(0, 0, 400, 600)
    Public Property CanvasSize As Size
        Set(ByVal value As Size)
            originalCanvas.Size = value
            'setup()
        End Set
        Get
            Return originalCanvas.Size
        End Get
    End Property

    Private canvasBackColorField As Color = Color.Transparent
    Public Property CanvasBackColor As Color
        Set(ByVal value As Color)
            canvasBackColorField = value
        End Set
        Get
            Return canvasBackColorField
        End Get
    End Property

    'Private zoomFactorField As Single = 1.0F
    'Public Property ZoomFactor As Single
    '    Get
    '        Return zoomFactorField
    '    End Get
    '    Set(ByVal value As Single)
    '        zoomFactorField = Math.Max(0.001F, value) ' if =0, tranform matrix will be thrown exception
    '        setup()
    '    End Set
    'End Property

    Public Property IsBilinearInterpolation As Boolean
        Set(ByVal value As Boolean)
            filter.IsBilinearInterpolation = value
        End Set
        Get
            Return filter.IsBilinearInterpolation
        End Get
    End Property

    Private pictureItem As Bitmap
    Public Property CanvasImage As Bitmap
        Set(ByVal value As Bitmap)
            pictureItem = value

            startFT()
            pictureItem = filter.Bitmap
        End Set
        Get
            Return pictureItem
        End Get
    End Property

    Private imageLocationField As Point = New Point()
    Public Property ImageLocation As Point
        Set(ByVal value As Point)
            imageLocationField = value
        End Set
        Get
            Return imageLocationField
        End Get
    End Property

    Private Sub startFT()
        ' Private pictureItem As Bitmap
        If pictureItem IsNot Nothing Then
            filter.Bitmap = pictureItem
            vertex(0) = New PointF(imageLocationField.X, imageLocationField.Y)
            vertex(1) = New PointF(imageLocationField.X + pictureItem.Width, imageLocationField.Y)
            vertex(2) = New PointF(imageLocationField.X + pictureItem.Width, imageLocationField.Y + pictureItem.Height)
            vertex(3) = New PointF(imageLocationField.X, imageLocationField.Y + pictureItem.Height)

            For i = 0 To 3
                recthandle(i) = New RectangleF(vertex(i).X - 2, vertex(i).Y - 2, 4, 4)
            Next
            filter.FourCorners = vertex
        End If
    End Sub

    Private zoomedCanvas As Rectangle = New Rectangle()
    Private visibleCanvas As Rectangle = New Rectangle()
    Private mxCanvasToControl, mxControlToCanvas As Matrix ' transform matrix

    'Private Sub setup()
    '    ' setup zoomed canvas Rectangle
    '    zoomedCanvas.Width = CInt(originalCanvas.Width * zoomFactorField)
    '    zoomedCanvas.Height = CInt(originalCanvas.Height * zoomFactorField)

    '    ' setup transform matrix
    '    mxCanvasToControl = New Matrix()
    '    mxCanvasToControl.Scale(zoomFactorField, zoomFactorField)
    '    '  mxCanvasToControl.Translate(canvasLoc.X, canvasLoc.Y, MatrixOrder.Append)

    '    mxControlToCanvas = mxCanvasToControl.Clone()
    '    mxControlToCanvas.Invert()

    '    visibleCanvas.Intersect(zoomedCanvas)

    'End Sub

    Private Function toCanvas(ByVal pt As Point) As Point
        Dim pts = New Point() {pt}
        If mxControlToCanvas IsNot Nothing Then mxControlToCanvas.TransformPoints(pts)
        Return pts(0)
    End Function

    Private ptOnCanvas As Point = New Point()

    Private isDrag As Boolean = False
    Private moveFlag As Integer

    Public Sub New()
    End Sub

    Sub OnPaint(ByVal e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.FillRectangle(New SolidBrush(canvasBackColorField), visibleCanvas)
        g.Transform = mxCanvasToControl

        If pictureItem IsNot Nothing Then
            g.DrawImage(pictureItem, imageLocationField)

            g.DrawPolygon(New Pen(Color.Yellow), vertex)
            For i = 0 To 3
                g.FillRectangle(New SolidBrush(Color.Red), recthandle(i))
            Next
        End If
    End Sub
End Class
