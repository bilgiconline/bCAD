Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports Guna.UI2.Native.WinApi

Namespace YLScsDrawing.Imaging
    ''' <summary>
    ''' Using InteropServices.Marshal mathods to get image channels (R,G,B,A) byte
    ''' </summary>
    Public Class ImageData
        Implements IDisposable
        Private _red, _green, _blue, _alpha As Byte(,)
        Private _disposed As Boolean = False

        Public Property A As Byte(,)
            Get
                Return _alpha
            End Get
            Set(ByVal value As Byte(,))
                _alpha = value
            End Set
        End Property
        Public Property B As Byte(,)
            Get
                Return _blue
            End Get
            Set(ByVal value As Byte(,))
                _blue = value
            End Set
        End Property
        Public Property G As Byte(,)
            Get
                Return _green
            End Get
            Set(ByVal value As Byte(,))
                _green = value
            End Set
        End Property
        Public Property R As Byte(,)
            Get
                Return _red
            End Get
            Set(ByVal value As Byte(,))
                _red = value
            End Set
        End Property

        Public Function Clone() As ImageData
            Dim cb As ImageData = New ImageData()
            cb.A = CType(_alpha.Clone(), Byte(,))
            cb.B = CType(_blue.Clone(), Byte(,))
            cb.G = CType(_green.Clone(), Byte(,))
            cb.R = CType(_red.Clone(), Byte(,))
            Return cb
        End Function

#Region "InteropServices.Marshal mathods"
        Public Sub FromBitmap(ByVal srcBmp As Bitmap)
            Try




                Dim w As Integer = srcBmp.Width
                Dim h As Integer = srcBmp.Height

                _alpha = New Byte(w - 1, h - 1) {}
                _blue = New Byte(w - 1, h - 1) {}
                _green = New Byte(w - 1, h - 1) {}
                _red = New Byte(w - 1, h - 1) {}

                ' Lock the bitmap's bits.  
                Dim bmpData As Drawing.Imaging.BitmapData = srcBmp.LockBits(New Rectangle(0, 0, w, h), Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb)
                ' Get the address of the first line.
                Dim ptr As IntPtr = bmpData.Scan0

                ' Declare an array to hold the bytes of the bitmap.
                Dim bytes As Integer = bmpData.Stride * srcBmp.Height
                Dim rgbValues = New Byte(bytes - 1) {}

                ' Copy the RGB values
                Marshal.Copy(ptr, rgbValues, 0, bytes)

                Dim offset As Integer = bmpData.Stride - w * 4

                Dim index = 0
                Try
                    For y = 0 To h - 1
                        For x = 0 To w - 1
                            _blue(x, y) = rgbValues(index)
                            _green(x, y) = rgbValues(index + 1)
                            _red(x, y) = rgbValues(index + 2)
                            _alpha(x, y) = rgbValues(index + 3)
                            index += 4
                        Next
                        index += offset
                    Next
                Catch ex As Exception

                End Try


                ' Unlock the bits.
                srcBmp.UnlockBits(bmpData)
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        End Sub

        Private Function convertToBitonal(ByVal original As Bitmap) As Bitmap
            Dim sourceStride As Integer
            Dim sourceBuffer As Byte() = extractBytes(original, sourceStride)

            ' Create destination bitmap
            Dim destination As New Bitmap(original.Width, original.Height, PixelFormat.Format1bppIndexed)

            destination.SetResolution(original.HorizontalResolution, original.VerticalResolution)

            ' Lock destination bitmap in memory
            Dim destinationData As BitmapData = destination.LockBits(New System.Drawing.Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.[WriteOnly], PixelFormat.Format1bppIndexed)

            ' Create buffer for destination bitmap bits
            Dim imageSize As Integer = destinationData.Stride * destinationData.Height
            Dim destinationBuffer As Byte() = New Byte(imageSize - 1) {}

            Dim sourceIndex As Integer = 0
            Dim destinationIndex As Integer = 0
            Dim pixelTotal As Integer = 0
            Dim destinationValue As Byte = 0
            Dim pixelValue As Integer = 128
            Dim height As Integer = destination.Height
            Dim width As Integer = destination.Width
            Dim threshold As Integer = 500

            For y As Integer = 0 To height - 1
                sourceIndex = y * sourceStride
                destinationIndex = y * destinationData.Stride
                destinationValue = 0
                pixelValue = 128

                For x As Integer = 0 To width - 1
                    ' Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = sourceBuffer(sourceIndex + 1) + CType(sourceBuffer(sourceIndex + 2), Integer) + sourceBuffer(sourceIndex + 3)

                    If pixelTotal > threshold Then
                        destinationValue += CByte(pixelValue)
                    End If

                    If pixelValue = 1 Then
                        destinationBuffer(destinationIndex) = destinationValue
                        destinationIndex += 1
                        destinationValue = 0
                        pixelValue = 128
                    Else
                        pixelValue >>= 1
                    End If

                    sourceIndex += 4
                Next

                If pixelValue <> 128 Then
                    destinationBuffer(destinationIndex) = destinationValue
                End If
            Next

            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize)
            destination.UnlockBits(destinationData)
            Return destination
        End Function
        Private Function extractBytes(ByVal original As Bitmap, ByRef stride As Integer) As Byte()
            Dim source As Bitmap = Nothing

            Try
                ' If original bitmap is not already in 32 BPP, ARGB format, then convert
                If original.PixelFormat <> PixelFormat.Format32bppArgb Then
                    source = New Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb)
                    source.SetResolution(original.HorizontalResolution, original.VerticalResolution)
                    Using g As Graphics = Graphics.FromImage(source)
                        g.DrawImageUnscaled(original, 0, 0)
                    End Using
                Else
                    source = original
                End If

                ' Lock source bitmap in memory
                Dim sourceData As BitmapData = source.LockBits(New System.Drawing.Rectangle(0, 0, source.Width, source.Height), ImageLockMode.[ReadOnly], PixelFormat.Format32bppArgb)

                ' Copy image data to binary array
                Dim imageSize As Integer = sourceData.Stride * sourceData.Height
                Dim sourceBuffer As Byte() = New Byte(imageSize - 1) {}
                Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize)

                ' Unlock source bitmap
                source.UnlockBits(sourceData)

                stride = sourceData.Stride
                Return sourceBuffer
            Finally
                If Not Object.ReferenceEquals(source, original) Then
                    source.Dispose()
                End If
            End Try

        End Function

        Public Function ToBitmap() As Bitmap
            Dim width = 0, height = 0
            If _alpha IsNot Nothing Then
                width = Math.Max(width, _alpha.GetLength(0))
                height = Math.Max(height, _alpha.GetLength(1))
            End If
            If _blue IsNot Nothing Then
                width = Math.Max(width, _blue.GetLength(0))
                height = Math.Max(height, _blue.GetLength(1))
            End If
            If _green IsNot Nothing Then
                width = Math.Max(width, _green.GetLength(0))
                height = Math.Max(height, _green.GetLength(1))
            End If
            If _red IsNot Nothing Then
                width = Math.Max(width, _red.GetLength(0))
                height = Math.Max(height, _red.GetLength(1))
            End If
            'width = 10
            ' height = 10
            Dim bmp As Bitmap = New Bitmap(width, height, PixelFormat.Format32bppPArgb)
            'bmp = convertToBitonal(bmp)
            Dim bmpData As Drawing.Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, width, height), Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb)
            '   Dim bmpData As Drawing.Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, width, height), Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb)

            ' Get the address of the first line.
            Dim ptr As IntPtr = bmpData.Scan0

            ' Declare an array to hold the bytes of the bitmap.
            Dim bytes As Integer = bmpData.Stride * bmp.Height
            Dim rgbValues = New Byte(bytes - 1) {}

            ' set rgbValues
            Dim offset As Integer = bmpData.Stride - width * 4
            Dim i = 0
            Try

                For y = 0 To height - 1
                    For x = 0 To width - 1
                        ' For k = 0 To 4
                        rgbValues(i * 1) = If(checkArray(_blue, x, y), _blue(x, y), CByte(0))
                            rgbValues((i + 1) * 1) = If(checkArray(_green, x, y), _green(x, y), CByte(0))
                            rgbValues((i + 2) * 1) = If(checkArray(_red, x, y), _red(x, y), CByte(0))
                            rgbValues((i + 3) * 1) = If(checkArray(_alpha, x, y), _alpha(x, y), CByte(255))
                            i += 4
                        '  Next
                        ' i += 4 * 5
                    Next
                    i += offset '* 5
                Next
            Catch ex As Exception
                Debug.Print(i)
            End Try


            ' Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, bytes)

            ' ' Unlock the bits.
            bmp.UnlockBits(bmpData)
            'bmp.Save()
            'Dim img As Bitmap = New Bitmap(width, height, PixelFormat.Format1bppIndexed)

            'Dim rect As New System.Drawing.Rectangle(0, 0, img.Width, img.Height)

            'Dim bmp As Bitmap = img.Clone(RECT, PixelFormat.Format1bppIndexed)

            ''-----

            ''If bmp.Width < bmp.Height Then

            ''    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone)

            ''End If

            ''-----


            'Dim bits As Byte() = Nothing
            ''  Try

            '' Lock the managed memory
            ''If img.Width < img.Height Then
            ''    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone)

            ''End If
            ''If img.PixelFormat <> PixelFormat.Format1bppIndexed Then
            'bmp = img
            '    ' End If

            '    Dim bmpdata As BitmapData = bmp.LockBits(RECT, ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed)

            '    ' Declare an array to hold the bytes of the bitmap.
            '    bits = New Byte(bmpdata.Stride * bmpdata.Height - 1) {}

            '    ' Copy the sample values into the array.
            '    Marshal.Copy(bmpdata.Scan0, bits, 0, bits.Length)

            '    ' Release managed memory


            '    bmp.UnlockBits(bmpdata)




            Return bmp
        End Function

#End Region

        Private Shared Function checkArray(ByVal array As Byte(,), ByVal x As Integer, ByVal y As Integer) As Boolean
            If array Is Nothing Then Return False
            If x < array.GetLength(0) AndAlso y < array.GetLength(1) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)

            ' Use SupressFinalize in case a subclass
            ' of this type implements a finalizer.
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            ' If you need thread safety, use a lock around these 
            ' operations, as well as in your methods that use the resource.
            If Not _disposed Then
                If disposing Then
                    _alpha = Nothing
                    _blue = Nothing
                    _green = Nothing
                    _red = Nothing
                End If

                ' Indicate that the instance has been disposed.
                _disposed = True
            End If
        End Sub
    End Class
End Namespace
