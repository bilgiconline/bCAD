<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemInformation
    Inherits System.Windows.Forms.UserControl

    'UserControl, bileşen listesini temizlemeyi bırakmayı geçersiz kılar.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form Tasarımcısı tarafından gerektirilir
    Private components As System.ComponentModel.IContainer

    'NOT: Aşağıdaki yordam Windows Form Tasarımcısı için gereklidir
    'Windows Form Tasarımcısı kullanılarak değiştirilebilir.  
    'Kod düzenleyicisini kullanarak değiştirmeyin.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ItemInformationLes = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ElementClassNameLbl = New System.Windows.Forms.Label()
        Me.P1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.centerPLbl = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.verticesCntLbl = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.endPLbl = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.startPLbl = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BottomPnl = New Guna.UI2.WinForms.Guna2Panel()
        Me.Btn3 = New Guna.UI2.WinForms.Guna2Button()
        Me.Btn2 = New Guna.UI2.WinForms.Guna2Button()
        Me.Btn1 = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2DragControl1 = New Guna.UI2.WinForms.Guna2DragControl(Me.components)
        Me.P2 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.rotationLbl = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.areaLbl = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lengthLbl = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.P3 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.colorLbl = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lastModified = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lvlNameLbl = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lineweightLbl = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.P1.SuspendLayout()
        Me.BottomPnl.SuspendLayout()
        Me.P2.SuspendLayout()
        Me.P3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ItemInformationLes
        '
        Me.ItemInformationLes.BorderRadius = 10
        Me.ItemInformationLes.ContainerControl = Me
        Me.ItemInformationLes.DockForm = False
        Me.ItemInformationLes.DockIndicatorTransparencyValue = 0.6R
        Me.ItemInformationLes.ResizeForm = False
        Me.ItemInformationLes.TransparentWhileDrag = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label1.Location = New System.Drawing.Point(114, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Eleman Tipi"
        '
        'ElementClassNameLbl
        '
        Me.ElementClassNameLbl.AutoSize = True
        Me.ElementClassNameLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ElementClassNameLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.ElementClassNameLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.ElementClassNameLbl.Location = New System.Drawing.Point(115, 69)
        Me.ElementClassNameLbl.Name = "ElementClassNameLbl"
        Me.ElementClassNameLbl.Size = New System.Drawing.Size(47, 23)
        Me.ElementClassNameLbl.TabIndex = 1
        Me.ElementClassNameLbl.Text = "Çizgi"
        '
        'P1
        '
        Me.P1.BackColor = System.Drawing.Color.Transparent
        Me.P1.Controls.Add(Me.centerPLbl)
        Me.P1.Controls.Add(Me.Label11)
        Me.P1.Controls.Add(Me.verticesCntLbl)
        Me.P1.Controls.Add(Me.Label7)
        Me.P1.Controls.Add(Me.endPLbl)
        Me.P1.Controls.Add(Me.Label5)
        Me.P1.Controls.Add(Me.startPLbl)
        Me.P1.Controls.Add(Me.Label2)
        Me.P1.EdgeWidth = 1
        Me.P1.FillColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.P1.Location = New System.Drawing.Point(25, 107)
        Me.P1.Name = "P1"
        Me.P1.Radius = 10
        Me.P1.ShadowColor = System.Drawing.Color.Black
        Me.P1.Size = New System.Drawing.Size(364, 361)
        Me.P1.TabIndex = 2
        '
        'centerPLbl
        '
        Me.centerPLbl.AutoSize = True
        Me.centerPLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.centerPLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.centerPLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.centerPLbl.Location = New System.Drawing.Point(27, 322)
        Me.centerPLbl.Name = "centerPLbl"
        Me.centerPLbl.Size = New System.Drawing.Size(47, 23)
        Me.centerPLbl.TabIndex = 8
        Me.centerPLbl.Text = "Çizgi"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label11.Location = New System.Drawing.Point(26, 280)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 28)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Merkez Noktası:"
        '
        'verticesCntLbl
        '
        Me.verticesCntLbl.AutoSize = True
        Me.verticesCntLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.verticesCntLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.verticesCntLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.verticesCntLbl.Location = New System.Drawing.Point(27, 236)
        Me.verticesCntLbl.Name = "verticesCntLbl"
        Me.verticesCntLbl.Size = New System.Drawing.Size(47, 23)
        Me.verticesCntLbl.TabIndex = 6
        Me.verticesCntLbl.Text = "Çizgi"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label7.Location = New System.Drawing.Point(26, 194)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(124, 28)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Nokta Sayısı:"
        '
        'endPLbl
        '
        Me.endPLbl.AutoSize = True
        Me.endPLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.endPLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.endPLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.endPLbl.Location = New System.Drawing.Point(27, 153)
        Me.endPLbl.Name = "endPLbl"
        Me.endPLbl.Size = New System.Drawing.Size(47, 23)
        Me.endPLbl.TabIndex = 4
        Me.endPLbl.Text = "Çizgi"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label5.Location = New System.Drawing.Point(26, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(124, 28)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Bitiş Noktası:"
        '
        'startPLbl
        '
        Me.startPLbl.AutoSize = True
        Me.startPLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startPLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.startPLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.startPLbl.Location = New System.Drawing.Point(27, 69)
        Me.startPLbl.Name = "startPLbl"
        Me.startPLbl.Size = New System.Drawing.Size(47, 23)
        Me.startPLbl.TabIndex = 2
        Me.startPLbl.Text = "Çizgi"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label2.Location = New System.Drawing.Point(26, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 28)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Başlangıç Noktası:"
        '
        'BottomPnl
        '
        Me.BottomPnl.Controls.Add(Me.Btn3)
        Me.BottomPnl.Controls.Add(Me.Btn2)
        Me.BottomPnl.Controls.Add(Me.Btn1)
        Me.BottomPnl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottomPnl.Location = New System.Drawing.Point(0, 492)
        Me.BottomPnl.Name = "BottomPnl"
        Me.BottomPnl.Size = New System.Drawing.Size(413, 46)
        Me.BottomPnl.TabIndex = 3
        '
        'Btn3
        '
        Me.Btn3.Animated = True
        Me.Btn3.AnimatedGIF = True
        Me.Btn3.BackColor = System.Drawing.Color.Transparent
        Me.Btn3.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Btn3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Btn3.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Btn3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Btn3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Btn3.FillColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Btn3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Btn3.ForeColor = System.Drawing.Color.White
        Me.Btn3.Location = New System.Drawing.Point(276, 0)
        Me.Btn3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Btn3.Name = "Btn3"
        Me.Btn3.Size = New System.Drawing.Size(134, 46)
        Me.Btn3.TabIndex = 20
        Me.Btn3.Text = "Çizim Bilgileri"
        Me.Btn3.UseTransparentBackground = True
        '
        'Btn2
        '
        Me.Btn2.Animated = True
        Me.Btn2.AnimatedGIF = True
        Me.Btn2.BackColor = System.Drawing.Color.Transparent
        Me.Btn2.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Btn2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Btn2.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Btn2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Btn2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Btn2.FillColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Btn2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Btn2.ForeColor = System.Drawing.Color.White
        Me.Btn2.Location = New System.Drawing.Point(130, 0)
        Me.Btn2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Btn2.Name = "Btn2"
        Me.Btn2.Size = New System.Drawing.Size(146, 46)
        Me.Btn2.TabIndex = 18
        Me.Btn2.Text = "Uzunluk, Alan Bilgileri"
        Me.Btn2.UseTransparentBackground = True
        '
        'Btn1
        '
        Me.Btn1.Animated = True
        Me.Btn1.BackColor = System.Drawing.Color.Transparent
        Me.Btn1.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Btn1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Btn1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Btn1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Btn1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Btn1.FillColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Btn1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Btn1.ForeColor = System.Drawing.Color.White
        Me.Btn1.IndicateFocus = True
        Me.Btn1.Location = New System.Drawing.Point(0, 0)
        Me.Btn1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Btn1.Name = "Btn1"
        Me.Btn1.Size = New System.Drawing.Size(130, 46)
        Me.Btn1.TabIndex = 19
        Me.Btn1.Text = "Nokta Bilgileri"
        Me.Btn1.UseTransparentBackground = True
        '
        'Guna2DragControl1
        '
        Me.Guna2DragControl1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2DragControl1.DragMode = Guna.UI2.WinForms.Enums.DragMode.Control
        Me.Guna2DragControl1.TargetControl = Me
        Me.Guna2DragControl1.UseTransparentDrag = True
        '
        'P2
        '
        Me.P2.BackColor = System.Drawing.Color.Transparent
        Me.P2.Controls.Add(Me.rotationLbl)
        Me.P2.Controls.Add(Me.Label4)
        Me.P2.Controls.Add(Me.areaLbl)
        Me.P2.Controls.Add(Me.Label8)
        Me.P2.Controls.Add(Me.lengthLbl)
        Me.P2.Controls.Add(Me.Label10)
        Me.P2.EdgeWidth = 1
        Me.P2.FillColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.P2.Location = New System.Drawing.Point(24, 107)
        Me.P2.Name = "P2"
        Me.P2.Radius = 10
        Me.P2.ShadowColor = System.Drawing.Color.Black
        Me.P2.Size = New System.Drawing.Size(364, 361)
        Me.P2.TabIndex = 4
        '
        'rotationLbl
        '
        Me.rotationLbl.AutoSize = True
        Me.rotationLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rotationLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.rotationLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.rotationLbl.Location = New System.Drawing.Point(27, 241)
        Me.rotationLbl.Name = "rotationLbl"
        Me.rotationLbl.Size = New System.Drawing.Size(47, 23)
        Me.rotationLbl.TabIndex = 6
        Me.rotationLbl.Text = "Çizgi"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label4.Location = New System.Drawing.Point(26, 199)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(177, 28)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Dönüklük: (Derece)"
        '
        'areaLbl
        '
        Me.areaLbl.AutoSize = True
        Me.areaLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.areaLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.areaLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.areaLbl.Location = New System.Drawing.Point(27, 153)
        Me.areaLbl.Name = "areaLbl"
        Me.areaLbl.Size = New System.Drawing.Size(47, 23)
        Me.areaLbl.TabIndex = 4
        Me.areaLbl.Text = "Çizgi"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label8.Location = New System.Drawing.Point(26, 111)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 28)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Alan: (m²)"
        '
        'lengthLbl
        '
        Me.lengthLbl.AutoSize = True
        Me.lengthLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lengthLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lengthLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.lengthLbl.Location = New System.Drawing.Point(27, 69)
        Me.lengthLbl.Name = "lengthLbl"
        Me.lengthLbl.Size = New System.Drawing.Size(47, 23)
        Me.lengthLbl.TabIndex = 2
        Me.lengthLbl.Text = "Çizgi"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label10.Location = New System.Drawing.Point(26, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 28)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Uzunluk: (m)"
        '
        'P3
        '
        Me.P3.BackColor = System.Drawing.Color.Transparent
        Me.P3.Controls.Add(Me.colorLbl)
        Me.P3.Controls.Add(Me.Label6)
        Me.P3.Controls.Add(Me.lastModified)
        Me.P3.Controls.Add(Me.Label9)
        Me.P3.Controls.Add(Me.lvlNameLbl)
        Me.P3.Controls.Add(Me.Label13)
        Me.P3.Controls.Add(Me.lineweightLbl)
        Me.P3.Controls.Add(Me.Label15)
        Me.P3.EdgeWidth = 1
        Me.P3.FillColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.P3.Location = New System.Drawing.Point(28, 106)
        Me.P3.Name = "P3"
        Me.P3.Radius = 10
        Me.P3.ShadowColor = System.Drawing.Color.Black
        Me.P3.Size = New System.Drawing.Size(364, 361)
        Me.P3.TabIndex = 9
        '
        'colorLbl
        '
        Me.colorLbl.AutoSize = True
        Me.colorLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.colorLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.colorLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.colorLbl.Location = New System.Drawing.Point(27, 237)
        Me.colorLbl.Name = "colorLbl"
        Me.colorLbl.Size = New System.Drawing.Size(47, 23)
        Me.colorLbl.TabIndex = 8
        Me.colorLbl.Text = "Çizgi"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label6.Location = New System.Drawing.Point(26, 195)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(212, 28)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Eleman Rengi: (System)"
        '
        'lastModified
        '
        Me.lastModified.AutoSize = True
        Me.lastModified.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lastModified.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lastModified.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.lastModified.Location = New System.Drawing.Point(27, 323)
        Me.lastModified.Name = "lastModified"
        Me.lastModified.Size = New System.Drawing.Size(17, 23)
        Me.lastModified.TabIndex = 6
        Me.lastModified.Text = "-"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label9.Location = New System.Drawing.Point(26, 281)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(150, 28)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Son Değiştirme:"
        '
        'lvlNameLbl
        '
        Me.lvlNameLbl.AutoSize = True
        Me.lvlNameLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lvlNameLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lvlNameLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.lvlNameLbl.Location = New System.Drawing.Point(27, 153)
        Me.lvlNameLbl.Name = "lvlNameLbl"
        Me.lvlNameLbl.Size = New System.Drawing.Size(47, 23)
        Me.lvlNameLbl.TabIndex = 4
        Me.lvlNameLbl.Text = "Çizgi"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label13.Location = New System.Drawing.Point(26, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(123, 28)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Katman İsmi:"
        '
        'lineweightLbl
        '
        Me.lineweightLbl.AutoSize = True
        Me.lineweightLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lineweightLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lineweightLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.lineweightLbl.Location = New System.Drawing.Point(27, 69)
        Me.lineweightLbl.Name = "lineweightLbl"
        Me.lineweightLbl.Size = New System.Drawing.Size(47, 23)
        Me.lineweightLbl.TabIndex = 2
        Me.lineweightLbl.Text = "Çizgi"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label15.Location = New System.Drawing.Point(26, 28)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(134, 28)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Çizgi Kalınlığı:"
        '
        'ItemInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Controls.Add(Me.BottomPnl)
        Me.Controls.Add(Me.ElementClassNameLbl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.P2)
        Me.Controls.Add(Me.P3)
        Me.Controls.Add(Me.P1)
        Me.DoubleBuffered = True
        Me.Name = "ItemInformation"
        Me.Size = New System.Drawing.Size(413, 538)
        Me.P1.ResumeLayout(False)
        Me.P1.PerformLayout()
        Me.BottomPnl.ResumeLayout(False)
        Me.P2.ResumeLayout(False)
        Me.P2.PerformLayout()
        Me.P3.ResumeLayout(False)
        Me.P3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ItemInformationLes As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents ElementClassNameLbl As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents P1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents endPLbl As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents startPLbl As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BottomPnl As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Btn3 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Btn2 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Btn1 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents verticesCntLbl As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Guna2DragControl1 As Guna.UI2.WinForms.Guna2DragControl
    Friend WithEvents P3 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents lastModified As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lvlNameLbl As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lineweightLbl As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents centerPLbl As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents P2 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents areaLbl As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lengthLbl As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents rotationLbl As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents colorLbl As Label
    Friend WithEvents Label6 As Label
End Class
