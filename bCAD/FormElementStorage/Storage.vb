Module Storage
    Dim LayersList As New List(Of Layer)
    Dim IndSecList As New List(Of IndependentSection)
    Dim RoomList As New List(Of RoomType)
    Dim ComponentList As New List(Of RoomComponents)
    Dim OuterBuildingList As New List(Of OuterBuilding)
    Sub AddLayer(NewLayer As Layer)
        LayersList.Add(NewLayer)
    End Sub
    Function aLyr() As String

        Return aLyr
    End Function
    Function SelectLayer(LayerName As String) As Layer
        For i = 0 To LayersList.Count - 1
            If LayersList(i).Name = LayerName Then
                SelectLayer = LayersList(i)
                Exit For
            End If
        Next
        Return SelectLayer
    End Function
    Sub AddIndependentSection(ActiveLayer As Layer, IndSecNumber As Integer, MaksN As Integer, dZemRef As Integer)
        Dim aNewIndepen As IndependentSection = New IndependentSection(IndSecNumber, ActiveLayer, MaksN, dZemRef)
        IndSecList.Add(aNewIndepen)
    End Sub

    Function SelectIndependentSection(IndSecNumber As Integer) As IndependentSection
        For i = 0 To IndSecList.Count - 1
            If IndSecList(i).IndSecNum = IndSecNumber Then
                SelectIndependentSection = IndSecList(i)
                Exit For
            End If
        Next
        Return SelectIndependentSection
    End Function
    Sub AddRoom(LayerName As String, IndSecNumber As String, Commercial As Boolean, RoomTypeIndex As Integer, Element As Object)
        Dim tmpLyr As Layer = SelectLayer(LayerName)
        If tmpLyr Is Nothing Then
            MsgBox("Aktif kat/katman seçili değil.")
            Exit Sub
        End If
        If IndSecNumber = "X" Then IndSecNumber = "1"
        Dim tmpIndSec As IndependentSection = SelectIndependentSection(IndSecNumber)
        If tmpIndSec Is Nothing Then
            MsgBox("Aktif bir bağımsız bölüm seçili değil.")
            Exit Sub
        End If
        If RoomTypeIndex = 0 Then
            MsgBox("Aktif bir oda tipi seçili değil.")
            Exit Sub
        End If
        Dim aNewRoom As RoomType = New RoomType(RoomTypeIndex, tmpIndSec, tmpLyr, Commercial, Element)
        RoomList.Add(aNewRoom)
    End Sub
    Sub RemoveComponents(Element As Object)
        For i = 0 To ComponentList.Count - 1
            Try
                If ComponentList(i).DrawnElement.GetType = Element.GetType Then
                    If TypeOf ComponentList(i).DrawnElement Is Shape Then
                        Dim tmpShape1 As Shape = ComponentList(i).DrawnElement
                        Dim tmpShape2 As Shape = Element
                        If tmpShape1 Is tmpShape2 Then
                            ComponentList.RemoveAt(i)
                            tmpShape1.Remove()
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub
    Sub RemoveRoom(Element As Object)
        For i = 0 To RoomList.Count - 1
            If RoomList(i).DrawnElement.GetType = Element.GetType Then
                If TypeOf RoomList(i).DrawnElement Is Shape Then
                    Dim tmpShape1 As Shape = RoomList(i).DrawnElement
                    Dim tmpShape2 As Shape = Element
                    If tmpShape1 Is tmpShape2 Then
                        RoomList.RemoveAt(i)
                        tmpShape1.Remove()
                    End If
                End If
            End If
        Next
    End Sub
    Sub RemoveOuterBuild(Element As Object)
        For i = 0 To OuterBuildingList.Count - 1
            If OuterBuildingList(i).DrawnElement.GetType = Element.GetType Then
                If TypeOf OuterBuildingList(i).DrawnElement Is Shape Then
                    Dim tmpShape1 As Shape = OuterBuildingList(i).DrawnElement
                    Dim tmpShape2 As Shape = Element
                    If tmpShape1 Is tmpShape2 Then
                        OuterBuildingList.RemoveAt(i)
                        tmpShape1.Remove()
                    End If
                End If
            End If
        Next
    End Sub
    Function GetActiveOuterBuild(ActiveLayerName As String) As List(Of Shape)
        GetActiveOuterBuild = New List(Of Shape)
        For i = 0 To OuterBuildingList.Count - 1
            If OuterBuildingList(i).LayerName = ActiveLayerName Then
                GetActiveOuterBuild.Add(OuterBuildingList(i).DrawnElement)
            End If
        Next
        Return GetActiveOuterBuild
    End Function
    Function GetActiveRoomComponents(ActiveLayerName As String) As List(Of Shape)
        GetActiveRoomComponents = New List(Of Shape)
        For i = 0 To ComponentList.Count - 1
            If ComponentList(i).LayerName = ActiveLayerName Then
                GetActiveRoomComponents.Add(ComponentList(i).DrawnElement)
            End If
        Next
        Return GetActiveRoomComponents
    End Function
    Function GetActiveLayerRooms(ActiveLayerName As String) As List(Of RoomType)
        GetActiveLayerRooms = New List(Of RoomType)
        For i = 0 To RoomList.Count - 1
            If RoomList(i).LayerName = ActiveLayerName Then
                GetActiveLayerRooms.Add(RoomList(i))
            End If
        Next
        Return GetActiveLayerRooms
    End Function
    Function GetDrawnActiveLayerItems(ActiveLayerName As String) As List(Of Object)
        GetDrawnActiveLayerItems = New List(Of Object)
        For i = 0 To RoomList.Count - 1
            If RoomList(i).LayerName = ActiveLayerName Then
                GetDrawnActiveLayerItems.Add(RoomList(i).DrawnElement)
            End If
        Next
        For i = 0 To ComponentList.Count - 1
            If ComponentList(i).LayerName = ActiveLayerName Then
                GetDrawnActiveLayerItems.Add(ComponentList(i).DrawnElement)
            End If
        Next
        For i = 0 To OuterBuildingList.Count - 1
            If OuterBuildingList(i).LayerName = ActiveLayerName Then
                GetDrawnActiveLayerItems.Add(OuterBuildingList(i).DrawnElement)
            End If
        Next
        Return GetDrawnActiveLayerItems
    End Function
    Function GetDrawnShapeActiveLayerItems(ActiveLayerName As String) As List(Of Shape)
        GetDrawnShapeActiveLayerItems = New List(Of Shape)
        For i = 0 To RoomList.Count - 1
            If RoomList(i).LayerName = ActiveLayerName Then
                If TypeOf RoomList(i).DrawnElement Is Shape Then
                    GetDrawnShapeActiveLayerItems.Add(RoomList(i).DrawnElement)
                End If
            End If
        Next
        For i = 0 To ComponentList.Count - 1
            If ComponentList(i).LayerName = ActiveLayerName Then
                GetDrawnShapeActiveLayerItems.Add(ComponentList(i).DrawnElement)
            End If
        Next
        For i = 0 To OuterBuildingList.Count - 1
            If OuterBuildingList(i).LayerName = ActiveLayerName Then
                GetDrawnShapeActiveLayerItems.Add(OuterBuildingList(i).DrawnElement)
            End If
        Next
        Return GetDrawnShapeActiveLayerItems
    End Function
    Sub AddComponents(LayerName As String, Element As Object, Code As Integer)
        Dim tmpLyr As Layer = SelectLayer(LayerName)
        If tmpLyr Is Nothing Then
            MsgBox("Aktif kat/katman seçili değil.")
            Exit Sub
        End If
        Dim Rooms As List(Of RoomType) = GetActiveLayerRooms(tmpLyr.Name)
        Dim tmpShape As Shape = Element
        Dim tmpList As New List(Of RoomType)
        Dim OutDoorShape As New List(Of RoomType)
        If Rooms.Count > 1 Then
            If TypeOf Element Is Shape Then
                For i = 0 To Rooms.Count - 1
                    For k As Integer = 0 To UBound(tmpShape.getVerticies) - 1
                        If MoveOnShapeLineCheckForPoint(tmpShape.getVerticies(k), Rooms(i).DrawnElement.getVerticies) = True Or PointInPolygon(tmpShape.getVerticies(k), Rooms(i).DrawnElement.getVerticies) Then
                            If Rooms(i).OutBoundry = False Then
                                If tmpList.Contains(Rooms(i)) = False Then
                                    tmpList.Add(Rooms(i))
                                    ' Exit For
                                End If
                            Else
                                If tmpList.Contains(Rooms(i)) = False Then
                                    OutDoorShape.Add(Rooms(i))
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Next
            Else
                MsgBox("Kapı veya pencere çizimi için lütfen poligon kullanın.")
            End If
        Else
            MsgBox("Kapı veya pencere çizimi için lütfen bağımsız bölüm kısımları, ortakalanları çizin.")
        End If

        Dim aComponents As RoomComponents = New RoomComponents(Element, Code, tmpList)
        If aComponents IsNot Nothing Then
            If aComponents.DrawnElement Is Nothing Then
                aComponents.Dispose()
                OutDoorShape.AddRange(tmpList)
                aComponents = New RoomComponents(Element, Code, OutDoorShape)
                If aComponents.DrawnElement Is Nothing Then
                    aComponents.Dispose()
                Else
                    ComponentList.Add(aComponents)
                End If
            Else
                ComponentList.Add(aComponents)
            End If
        End If
    End Sub
    Sub AddOuterBuild(LayerName As String, Element As Object, Balcony As Boolean)
        Dim tmpLyr As Layer = SelectLayer(LayerName)
        If tmpLyr Is Nothing Then
            MsgBox("Aktif kat/katman seçili değil.")
            Exit Sub
        End If
        Dim Rooms As List(Of RoomType) = GetActiveLayerRooms(tmpLyr.Name)
        Dim tmpShape As Shape = Element
        Dim tmpList As New List(Of RoomType)
        Dim OutDoorShape As New List(Of RoomType)
        If Rooms.Count > 1 Then
            If TypeOf Element Is Shape Then
                For i = 0 To Rooms.Count - 1
                    If Rooms(i).OutBoundry = True Then
                        If tmpList.Contains(Rooms(i)) = False Then
                            OutDoorShape.Add(Rooms(i))
                        End If
                    End If
                Next
            End If
        Else
            MsgBox("Balkon veya teras çizimi için lütfen bağımsız bölümleri ve bina dış sınırını çiziniz.")
        End If

        Dim aOuterBuilding As OuterBuilding = New OuterBuilding(Element, Balcony, OutDoorShape)
        If aOuterBuilding IsNot Nothing Then
            If aOuterBuilding.DrawnElement Is Nothing Then
                aOuterBuilding.Dispose()
            Else
                OuterBuildingList.Add(aOuterBuilding)
            End If
        End If
    End Sub
End Module
