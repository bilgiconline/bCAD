Imports System.Windows
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip

Module LayersClass
    Class Layer
        Dim p_isActive As Boolean = False
        Dim p_isHide As Boolean = False
        Dim p_Name As String
        Dim p_Height As Double
        Dim p_DiffConcrete As Double
        Public Sub New(DiffLayer As Double, LayerHeight As Double, LayerName As String)
            DiffConcrete = DiffLayer
            Height = LayerHeight
            Name = LayerName
        End Sub
        Property DiffConcrete As Double
            Get
                Return p_DiffConcrete
            End Get
            Set(value As Double)
                p_DiffConcrete = value
            End Set
        End Property
        Property Height As Double
            Get
                Return p_Height
            End Get
            Set(value As Double)
                p_Height = value
            End Set
        End Property
        Property IsActive As Boolean
            Get
                Return p_isActive
            End Get
            Set(value As Boolean)
                p_isActive = value
            End Set
        End Property
        Property IsHide As Integer
            Get
                Return p_isHide
            End Get
            Set(value As Integer)
                p_isHide = value
            End Set
        End Property

        Property Name As String
            Get
                Return p_Name
            End Get
            Set(value As String)
                p_Name = value
            End Set
        End Property
        Public Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
    Class IndependentSection
        Dim p_IndSecNum As String
        Dim p_LayerName As String
        Dim p_MAKSID As Integer
        Dim p_ZmRefNum As Integer
        Dim p_Layer As Layer
        Dim p_Name As String

        Public Sub New(IndSecName As String, LyrName As Layer, MaksIdNumber As Integer, ZemRef As Integer)
            IndSecNum = IndSecName
            ActiveLayer = LyrName
            MAKSID = MaksIdNumber
            IndSecRef = ZemRef
        End Sub
        Property Name As String
            Get
                Return p_Name
            End Get
            Set(value As String)
                p_Name = value
            End Set
        End Property
        Property ActiveLayer As Layer
            Get
                Return p_Layer
            End Get
            Set(value As Layer)
                p_Layer = value
            End Set
        End Property
        Property MAKSID As Integer
            Get
                Return p_MAKSID
            End Get
            Set(value As Integer)
                p_MAKSID = value
            End Set
        End Property
        Property IndSecRef As Integer
            Get
                Return p_ZmRefNum
            End Get
            Set(value As Integer)
                p_ZmRefNum = value
            End Set
        End Property
        Property IndSecNum As String
            Get
                Return p_IndSecNum
            End Get
            Set(value As String)
                p_IndSecNum = value
            End Set
        End Property
        ReadOnly Property LayerName As String
            Get
                p_LayerName = p_Layer.Name
                Return p_LayerName
            End Get
        End Property
        Public Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
    Class RoomComponents
        Implements IDisposable
        Dim p_isPartDoor As Boolean = False
        Dim p_isBuildDoor As Boolean = False
        Dim p_isIndSecDoor As Boolean = False
        Dim p_isOutDoor As Boolean = False
        Dim p_isRoomWindow As Boolean = False
        Dim p_Layer As Layer
        Dim p_IndSecNum As IndependentSection
        Dim p_Room As New List(Of RoomType)
        Dim p_Object As Object
        Dim p_Height As Integer
        Dim p_Width As Integer
        Dim p_SurfaceDiff As Integer
        Dim disposedValue As Boolean
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    GC.SuppressFinalize(Me)
                End If
            End If
            disposedValue = True
        End Sub
        Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub
        Public Sub New(Element As Object, Code As Integer, Rooms As List(Of RoomType))
            Dim tmpShape As Shape
            Dim tmpShape2 As Shape
            If Rooms.Count > 1 Then
                For i = 0 To Rooms.Count - 2
                    If Rooms(i).OutBoundry = True Or Rooms(i + 1).OutBoundry = True Then
                        tmpShape2 = IntersectShapes(Element, Rooms(i).DrawnElement, Rooms(i + 1).DrawnElement)
                    Else
                        tmpShape2 = IntersectShapes2(Element, Rooms(i).DrawnElement, Rooms(i + 1).DrawnElement)
                    End If
                    If tmpShape2 IsNot Nothing Then
                        DrawnElement = tmpShape2
                        For k = 0 To tmpShape2.getVerticies.ToList.Count - 1
                            Debug.Print(tmpShape2.getVerticies(k).ToString)
                        Next
                        Layer = Rooms(i).GetLayer
                    End If
                Next
            End If
            If DrawnElement Is Nothing Then
                If Rooms.Count > 1 Then
                    For i = 0 To Rooms.Count - 2
                        tmpShape = GetIntersectionShape(Element, Rooms(i).DrawnElement, Rooms(i + 1).DrawnElement)
                        If tmpShape IsNot Nothing Then
                            DrawnElement = tmpShape
                            Layer = Rooms(i).GetLayer
                            Exit For
                        End If
                    Next
                End If
            End If
            If Code = 1099 Then
                BuildDoor = True
            ElseIf Code = 1098 Then
                RoomWindow = True
            ElseIf Code = 1095 Then
                PartDoor = True
            ElseIf Code = 1094 Then
                OutDoor = True
            ElseIf Code = 1093 Then
                IndSecDoor = True
            End If
        End Sub
        Public Sub New(Element As Object, Code As Integer, Rooms As List(Of RoomType), ComponentHeight As Integer, GroundDiff As Integer)
            Dim tmpShape As Shape
            Dim tmpShape2 As Shape
            If Rooms.Count > 1 Then
                For i = 0 To Rooms.Count - 2
                    If Rooms(i).OutBoundry = True Or Rooms(i + 1).OutBoundry = True Then
                        tmpShape2 = IntersectShapes(Element, Rooms(i).DrawnElement, Rooms(i + 1).DrawnElement)
                    Else
                        tmpShape2 = IntersectShapes2(Element, Rooms(i).DrawnElement, Rooms(i + 1).DrawnElement)
                    End If
                    If tmpShape2 IsNot Nothing Then
                        DrawnElement = tmpShape2
                        For k = 0 To tmpShape2.getVerticies.ToList.Count - 1
                            Debug.Print(tmpShape2.getVerticies(k).ToString)
                        Next
                        Layer = Rooms(i).GetLayer
                    End If
                Next
            End If
            If DrawnElement Is Nothing Then
                If Rooms.Count > 1 Then
                    For i = 0 To Rooms.Count - 2
                        tmpShape = GetIntersectionShape(Element, Rooms(i).DrawnElement, Rooms(i + 1).DrawnElement)
                        If tmpShape IsNot Nothing Then
                            DrawnElement = tmpShape
                            Layer = Rooms(i).GetLayer
                            Exit For
                        End If
                    Next
                End If
            End If
            If Code = 1099 Then
                BuildDoor = True
            ElseIf Code = 1098 Then
                RoomWindow = True
            ElseIf Code = 1095 Then
                PartDoor = True
            ElseIf Code = 1094 Then
                OutDoor = True
            ElseIf Code = 1093 Then
                IndSecDoor = True
            End If
            HeightAboveGroundSurface = GroundDiff
            Height = ComponentHeight
        End Sub
        Property HeightAboveGroundSurface As Integer
            Get
                Return p_SurfaceDiff
            End Get
            Set(value As Integer)
                p_SurfaceDiff = value
            End Set
        End Property
        Property RoomWindow As Boolean
            Get
                Return p_isRoomWindow
            End Get
            Set(value As Boolean)
                p_isRoomWindow = value
            End Set
        End Property
        Property OutDoor As Boolean
            Get
                Return p_isOutDoor
            End Get
            Set(value As Boolean)
                p_isOutDoor = value
            End Set
        End Property
        Property IndSecDoor As Boolean
            Get
                Return p_isIndSecDoor
            End Get
            Set(value As Boolean)
                p_isIndSecDoor = value
            End Set
        End Property
        Property BuildDoor As Boolean
            Get
                Return p_isBuildDoor
            End Get
            Set(value As Boolean)
                p_isBuildDoor = value
            End Set
        End Property
        Property PartDoor As Boolean
            Get
                Return p_isPartDoor
            End Get
            Set(value As Boolean)
                p_isPartDoor = value
            End Set
        End Property
        Property Width As Integer
            Get
                Return p_Width
            End Get
            Set(value As Integer)
                p_Width = value
            End Set
        End Property
        Property Height As Integer
            Get
                Return p_Height
            End Get
            Set(value As Integer)
                p_Height = value
            End Set
        End Property
        Property DrawnElement As Object
            Get
                Return p_Object
            End Get
            Set(value As Object)
                p_Object = value
            End Set
        End Property
        ReadOnly Property LayerName As String
            Get
                Return p_Layer.Name
            End Get
        End Property
        Property Layer As Layer
            Get
                Return p_Layer
            End Get
            Set(value As Layer)
                p_Layer = value
            End Set
        End Property
        Property IndSecNum As IndependentSection
            Get
                Return p_IndSecNum
            End Get
            Set(value As IndependentSection)
                p_IndSecNum = value
            End Set
        End Property
        Property IntersectRoom As List(Of RoomType)
            Get
                Return p_Room
            End Get
            Set(value As List(Of RoomType))
                p_Room = value
            End Set
        End Property
    End Class
    Class RoomType
        Dim p_isRoom As Boolean = False
        Dim p_isSaloon As Boolean = False
        Dim p_isKitchen As Boolean = False
        Dim p_isWC As Boolean = False
        Dim p_isHole As Boolean = False
        Dim p_isWorkplace As Boolean = False
        Dim p_isCellar As Boolean = False
        Dim p_isInRoomElevator As Boolean = False
        Dim p_isInRoomStairs As Boolean = False
        Dim p_isOutBoundry As Boolean = False
        Dim p_isPublicAreas As Boolean = False
        Dim p_isBathroom As Boolean = False
        Dim p_isSunporch As Boolean = False
        Dim p_isShelter As Boolean = False
        Dim p_isCentralHeating As Boolean = False
        Dim p_isRadiatorRoom As Boolean = False
        Dim p_isPorterFlat As Boolean = False
        Dim p_isElectricCentral As Boolean = False
        Dim p_isParkingLot As Boolean = False
        Dim p_isSecurityRoom As Boolean = False
        Dim p_isPoolPart As Boolean = False
        Dim p_isSocialArea As Boolean = False
        Dim p_isSportSaloon As Boolean = False
        Dim p_isGarbageRoom As Boolean = False
        Dim p_isHydrophore As Boolean = False
        Dim p_isReligiousFacility As Boolean = False
        Dim p_isOutBuilding As Boolean = False
        Dim p_isPlantRoom As Boolean = False
        Dim p_ishelpDesk As Boolean = False
        Dim p_isHide As Boolean = False
        Dim p_IndSecNum As IndependentSection
        Dim p_IsCommercial As Boolean = False
        Dim p_Layer As Layer
        Dim p_Element As Object

        Public Sub New(RoomCode As Integer, IndSecName As IndependentSection, LyrName As Layer, Commercial As Boolean, AddedElement As Object)
            'yeni oda kodlarını eklemek gerekiyor
            If RoomCode = 1001 Then Room = True
            If RoomCode = 1002 Then BathRoom = True
            If RoomCode = 1003 Then Saloon = True
            If RoomCode = 1004 Then Kitchen = True
            If RoomCode = 1005 Then Cellar = True
            If RoomCode = 1006 Then WC = True
            If RoomCode = 1007 Then Sunporch = True
            If RoomCode = 1008 Then Hole = True
            If RoomCode = 1009 Then Workplace = True
            If RoomCode = 1010 Then Shelter = True
            If RoomCode = 1011 Then HelpDesk = True
            If RoomCode = 1012 Then RadiatorRoom = True
            If RoomCode = 1013 Then PorterFlat = True
            If RoomCode = 1014 Then ElectricCentral = True
            If RoomCode = 1015 Then CentralHeating = True
            If RoomCode = 1016 Then ParkingLot = True
            If RoomCode = 1017 Then SecurityRoom = True
            If RoomCode = 1018 Then PoolPart = True
            If RoomCode = 1019 Then SocialArea = True
            If RoomCode = 1020 Then SportSaloon = True
            If RoomCode = 1021 Then GarbageRoom = True
            If RoomCode = 1022 Then PublicArea = True
            If RoomCode = 1024 Then InRoomStairs = True
            If RoomCode = 1025 Then Hydrophore = True
            If RoomCode = 1026 Then ReligiousFacility = True
            If RoomCode = 1027 Then OutBuilding = True
            If RoomCode = 1028 Then PlantRoom = True
            If RoomCode = 1029 Then InRoomElevator = True
            If RoomCode = 1100 Then OutBoundry = True
            If RoomCode = 1100 Or RoomCode = 1022 Then

            End If
            IndSecNum = IndSecName
            GetLayer = LyrName
            p_IsCommercial = Commercial
            DrawnElement = AddedElement
        End Sub
        Property DrawnElement As Object
            Get
                Return p_Element
            End Get
            Set(value As Object)
                p_Element = value
            End Set
        End Property

        Property Commercial As Boolean
            Get
                Return p_IsCommercial
            End Get
            Set(value As Boolean)
                p_IsCommercial = value
            End Set
        End Property
        Property PlantRoom As Boolean
            Get
                Return p_isPlantRoom
            End Get
            Set(value As Boolean)
                p_isPlantRoom = value
            End Set
        End Property
        Property OutBuilding As Boolean
            Get
                Return p_isOutBuilding
            End Get
            Set(value As Boolean)
                p_isOutBuilding = value
            End Set
        End Property
        Property ReligiousFacility As Boolean
            Get
                Return p_isReligiousFacility
            End Get
            Set(value As Boolean)
                p_isReligiousFacility = value
            End Set
        End Property
        Property GarbageRoom As Boolean
            Get
                Return p_isGarbageRoom
            End Get
            Set(value As Boolean)
                p_isGarbageRoom = value
            End Set
        End Property
        Property Hydrophore As Boolean
            Get
                Return p_isHydrophore
            End Get
            Set(value As Boolean)
                p_isHydrophore = value
            End Set
        End Property
        Property SportSaloon As Boolean
            Get
                Return p_isSportSaloon
            End Get
            Set(value As Boolean)
                p_isSportSaloon = value
            End Set
        End Property
        Property SocialArea As Boolean
            Get
                Return p_isSocialArea
            End Get
            Set(value As Boolean)
                p_isSocialArea = value
            End Set
        End Property
        Property PoolPart As Boolean
            Get
                Return p_isPoolPart
            End Get
            Set(value As Boolean)
                p_isPoolPart = value
            End Set
        End Property
        Property SecurityRoom As Boolean
            Get
                Return p_isSecurityRoom
            End Get
            Set(value As Boolean)
                p_isSecurityRoom = value
            End Set
        End Property
        Property ParkingLot As Boolean
            Get
                Return p_isParkingLot
            End Get
            Set(value As Boolean)
                p_isParkingLot = value
            End Set
        End Property
        Property RadiatorRoom As Boolean
            Get
                Return p_isRadiatorRoom
            End Get
            Set(value As Boolean)
                p_isRadiatorRoom = value
            End Set
        End Property
        Property ElectricCentral As Boolean
            Get
                Return p_isElectricCentral
            End Get
            Set(value As Boolean)
                p_isElectricCentral = value
            End Set
        End Property
        Property PorterFlat As Boolean
            Get
                Return p_isPorterFlat
            End Get
            Set(value As Boolean)
                p_isPorterFlat = value
            End Set
        End Property
        Property CentralHeating As Boolean
            Get
                Return p_isCentralHeating
            End Get
            Set(value As Boolean)
                p_isCentralHeating = value
            End Set
        End Property
        Property HelpDesk As Boolean
            Get
                Return p_ishelpDesk
            End Get
            Set(value As Boolean)
                p_ishelpDesk = value
            End Set
        End Property
        Property Shelter As Boolean
            Get
                Return p_isShelter
            End Get
            Set(value As Boolean)
                p_isShelter = value
            End Set
        End Property
        Property Sunporch As Boolean
            Get
                Return p_isSunporch
            End Get
            Set(value As Boolean)
                p_isSunporch = value
            End Set
        End Property
        Property OutBoundry As Boolean
            Get
                p_IsCommercial = False
                Return p_isOutBoundry
            End Get
            Set(value As Boolean)
                p_isOutBoundry = value
                p_IsCommercial = False
            End Set
        End Property
        Property InRoomElevator As Boolean
            Get
                Return p_isInRoomElevator
            End Get
            Set(value As Boolean)
                p_isInRoomElevator = value
            End Set
        End Property
        Property InRoomStairs As Boolean
            Get
                Return p_isInRoomStairs
            End Get
            Set(value As Boolean)
                p_isInRoomStairs = value
            End Set
        End Property
        Property PublicArea As Boolean
            Get
                Return p_isPublicAreas
            End Get
            Set(value As Boolean)
                p_isPublicAreas = value
            End Set
        End Property
        Property Cellar As Boolean
            Get
                Return p_isCellar
            End Get
            Set(value As Boolean)
                p_isCellar = value
            End Set
        End Property
        Property Workplace As Boolean
            Get
                Return p_isWorkplace
            End Get
            Set(value As Boolean)
                p_isWorkplace = value
            End Set
        End Property
        Property WC As Boolean
            Get
                Return p_isWC
            End Get
            Set(value As Boolean)
                p_isWC = value
            End Set
        End Property
        Property Hole As Boolean
            Get
                Return p_isHole
            End Get
            Set(value As Boolean)
                p_isHole = value
            End Set
        End Property
        Property Kitchen As Boolean
            Get
                Return p_isKitchen
            End Get
            Set(value As Boolean)
                p_isKitchen = value
            End Set
        End Property
        Property Saloon As Boolean
            Get
                Return p_isSaloon
            End Get
            Set(value As Boolean)
                p_isSaloon = value
            End Set
        End Property
        Property BathRoom As Boolean
            Get
                Return p_isBathroom
            End Get
            Set(value As Boolean)
                p_isBathroom = value
            End Set
        End Property
        Property Room As Boolean
            Get
                Return p_isRoom
            End Get
            Set(value As Boolean)
                p_isRoom = value
            End Set
        End Property

        Property IndSecNum As IndependentSection
            Get
                Return p_IndSecNum
            End Get
            Set(value As IndependentSection)
                p_IndSecNum = value
            End Set
        End Property
        ReadOnly Property LayerName As String
            Get
                Return GetLayer.Name
            End Get
        End Property
        Property GetLayer As Layer
            Get
                Return p_Layer
            End Get
            Set(value As Layer)
                p_Layer = value
            End Set
        End Property
        Property IsHide As Integer
            Get
                Return p_isHide
            End Get
            Set(value As Integer)
                p_isHide = value
            End Set
        End Property
        Public Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class

    Class OuterBuilding
        Implements IDisposable
        Dim p_Balcony As Boolean = False
        Dim p_Terrace As Boolean = False
        Dim p_Object As Object
        Dim p_Layer As Layer
        Dim p_IndSecNum As IndependentSection
        Dim disposedValue As Boolean
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    GC.SuppressFinalize(Me)
                End If
            End If
            disposedValue = True
        End Sub
        Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub
        Property Balcony As Boolean
            Get
                Return p_Balcony
            End Get
            Set(value As Boolean)
                p_Balcony = value
            End Set
        End Property
        Property Terrace As Boolean
            Get
                Return p_Terrace
            End Get
            Set(value As Boolean)
                p_Terrace = value
            End Set
        End Property
        Property IndSecNum As IndependentSection
            Get
                Return p_IndSecNum
            End Get
            Set(value As IndependentSection)
                p_IndSecNum = value
            End Set
        End Property
        Property DrawnElement As Object
            Get
                Return p_Object
            End Get
            Set(value As Object)
                p_Object = value
            End Set
        End Property
        Property GetLayer As Layer
            Get
                Return p_Layer
            End Get
            Set(value As Layer)
                p_Layer = value
            End Set
        End Property
        ReadOnly Property LayerName As String
            Get
                Return GetLayer.Name
            End Get
        End Property
        Public Sub New(Element As Object, Balcony As Boolean, Rooms As List(Of RoomType))
            Dim tmpShape As Shape
            If DrawnElement Is Nothing Then
                If Rooms.Count > 0 Then
                    For i = 0 To Rooms.Count - 1
                        tmpShape = IntersectShapes(Element, Rooms(i).DrawnElement)
                        If tmpShape IsNot Nothing Then
                            DrawnElement = tmpShape
                            GetLayer = Rooms(i).GetLayer
                            If Balcony = True Then
                                Me.Balcony = True
                            Else
                                Me.Terrace = True
                            End If
                            Exit For
                        End If
                    Next
                End If
            End If
        End Sub
    End Class

End Module
