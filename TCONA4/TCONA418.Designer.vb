<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TCONA418
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TCONA418))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button18Salir = New System.Windows.Forms.Button()
        Me.GRIDAREAS1 = New System.Windows.Forms.DataGridView()
        Me.Area = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descrip = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PtoImpre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Area2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Area3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Area4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoImpre = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Modelo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ButtonGRIDAbajo = New System.Windows.Forms.Button()
        Me.ButtonGRIDArriba = New System.Windows.Forms.Button()
        Me.ButtonENviaMensaje = New System.Windows.Forms.Button()
        Me.TextBoxMensaL1 = New System.Windows.Forms.TextBox()
        Me.TextBoxMensaL2 = New System.Windows.Forms.TextBox()
        Me.TextBoxMensaL3 = New System.Windows.Forms.TextBox()
        Me.TextBoxMensaL4 = New System.Windows.Forms.TextBox()
        Me.ButtonTeclado = New System.Windows.Forms.Button()
        Me.ButtonAbajTxt = New System.Windows.Forms.Button()
        Me.ButtonArriTxt = New System.Windows.Forms.Button()
        Me.LabelIndicador = New System.Windows.Forms.Label()
        Me.Button18Cls = New System.Windows.Forms.Button()
        Me.GRIDTEXTOSPREDEF = New System.Windows.Forms.DataGridView()
        Me.ColText1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColText2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColText3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColText4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ButtonGrdAbaj = New System.Windows.Forms.Button()
        Me.ButtonGrdArri = New System.Windows.Forms.Button()
        Me.ButtonSelTextos = New System.Windows.Forms.Button()
        Me.ButtonGrabaTextos = New System.Windows.Forms.Button()
        Me.ButtonBorraTextos = New System.Windows.Forms.Button()
        Me.LabelNomArea = New System.Windows.Forms.Label()
        Me.ButtonTerminarMESA = New System.Windows.Forms.Button()
        CType(Me.GRIDAREAS1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDTEXTOSPREDEF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button18Salir
        '
        Me.Button18Salir.BackColor = System.Drawing.Color.Salmon
        Me.Button18Salir.Font = New System.Drawing.Font("Consolas", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button18Salir.ForeColor = System.Drawing.Color.Black
        Me.Button18Salir.Location = New System.Drawing.Point(802, 188)
        Me.Button18Salir.Name = "Button18Salir"
        Me.Button18Salir.Size = New System.Drawing.Size(115, 54)
        Me.Button18Salir.TabIndex = 161
        Me.Button18Salir.Text = "SALIR"
        Me.Button18Salir.UseVisualStyleBackColor = False
        '
        'GRIDAREAS1
        '
        Me.GRIDAREAS1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDAREAS1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Area, Me.Descrip, Me.PtoImpre, Me.Area2, Me.Area3, Me.Area4, Me.T, Me.TipoImpre, Me.Modelo})
        Me.GRIDAREAS1.Location = New System.Drawing.Point(12, 12)
        Me.GRIDAREAS1.Name = "GRIDAREAS1"
        Me.GRIDAREAS1.Size = New System.Drawing.Size(908, 170)
        Me.GRIDAREAS1.TabIndex = 162
        '
        'Area
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Area.DefaultCellStyle = DataGridViewCellStyle1
        Me.Area.HeaderText = "Area"
        Me.Area.Name = "Area"
        Me.Area.Width = 75
        '
        'Descrip
        '
        Me.Descrip.HeaderText = "Descripcion"
        Me.Descrip.Name = "Descrip"
        Me.Descrip.Width = 150
        '
        'PtoImpre
        '
        Me.PtoImpre.HeaderText = "Puerto"
        Me.PtoImpre.Name = "PtoImpre"
        '
        'Area2
        '
        Me.Area2.HeaderText = "Area 2"
        Me.Area2.Name = "Area2"
        Me.Area2.Width = 50
        '
        'Area3
        '
        Me.Area3.HeaderText = "Area 3"
        Me.Area3.Name = "Area3"
        Me.Area3.Width = 50
        '
        'Area4
        '
        Me.Area4.HeaderText = "Area 4"
        Me.Area4.Name = "Area4"
        Me.Area4.Width = 50
        '
        'T
        '
        Me.T.HeaderText = "Replicar"
        Me.T.Name = "T"
        Me.T.Width = 45
        '
        'TipoImpre
        '
        Me.TipoImpre.HeaderText = "T/Impre"
        Me.TipoImpre.Name = "TipoImpre"
        Me.TipoImpre.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TipoImpre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Modelo
        '
        Me.Modelo.HeaderText = "Modelo Impresora"
        Me.Modelo.Name = "Modelo"
        Me.Modelo.Width = 150
        '
        'ButtonGRIDAbajo
        '
        Me.ButtonGRIDAbajo.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDAbajo.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDAbajo.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDAbajo.Location = New System.Drawing.Point(926, 86)
        Me.ButtonGRIDAbajo.Name = "ButtonGRIDAbajo"
        Me.ButtonGRIDAbajo.Size = New System.Drawing.Size(70, 50)
        Me.ButtonGRIDAbajo.TabIndex = 164
        Me.ButtonGRIDAbajo.Text = "ä"
        Me.ButtonGRIDAbajo.UseVisualStyleBackColor = False
        '
        'ButtonGRIDArriba
        '
        Me.ButtonGRIDArriba.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDArriba.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDArriba.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDArriba.Location = New System.Drawing.Point(926, 30)
        Me.ButtonGRIDArriba.Name = "ButtonGRIDArriba"
        Me.ButtonGRIDArriba.Size = New System.Drawing.Size(70, 50)
        Me.ButtonGRIDArriba.TabIndex = 163
        Me.ButtonGRIDArriba.Text = "ã"
        Me.ButtonGRIDArriba.UseVisualStyleBackColor = False
        '
        'ButtonENviaMensaje
        '
        Me.ButtonENviaMensaje.BackColor = System.Drawing.Color.Blue
        Me.ButtonENviaMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonENviaMensaje.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonENviaMensaje.Location = New System.Drawing.Point(541, 188)
        Me.ButtonENviaMensaje.Name = "ButtonENviaMensaje"
        Me.ButtonENviaMensaje.Size = New System.Drawing.Size(255, 54)
        Me.ButtonENviaMensaje.TabIndex = 204
        Me.ButtonENviaMensaje.Text = "Enviar Mensaje al AREA seleccionada."
        Me.ButtonENviaMensaje.UseVisualStyleBackColor = False
        '
        'TextBoxMensaL1
        '
        Me.TextBoxMensaL1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxMensaL1.Location = New System.Drawing.Point(12, 198)
        Me.TextBoxMensaL1.MaxLength = 40
        Me.TextBoxMensaL1.Name = "TextBoxMensaL1"
        Me.TextBoxMensaL1.Size = New System.Drawing.Size(257, 20)
        Me.TextBoxMensaL1.TabIndex = 205
        '
        'TextBoxMensaL2
        '
        Me.TextBoxMensaL2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxMensaL2.Location = New System.Drawing.Point(12, 226)
        Me.TextBoxMensaL2.MaxLength = 40
        Me.TextBoxMensaL2.Name = "TextBoxMensaL2"
        Me.TextBoxMensaL2.Size = New System.Drawing.Size(257, 20)
        Me.TextBoxMensaL2.TabIndex = 206
        '
        'TextBoxMensaL3
        '
        Me.TextBoxMensaL3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxMensaL3.Location = New System.Drawing.Point(12, 254)
        Me.TextBoxMensaL3.MaxLength = 40
        Me.TextBoxMensaL3.Name = "TextBoxMensaL3"
        Me.TextBoxMensaL3.Size = New System.Drawing.Size(257, 20)
        Me.TextBoxMensaL3.TabIndex = 207
        '
        'TextBoxMensaL4
        '
        Me.TextBoxMensaL4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxMensaL4.Location = New System.Drawing.Point(12, 282)
        Me.TextBoxMensaL4.MaxLength = 40
        Me.TextBoxMensaL4.Name = "TextBoxMensaL4"
        Me.TextBoxMensaL4.Size = New System.Drawing.Size(257, 20)
        Me.TextBoxMensaL4.TabIndex = 208
        '
        'ButtonTeclado
        '
        Me.ButtonTeclado.BackgroundImage = CType(resources.GetObject("ButtonTeclado.BackgroundImage"), System.Drawing.Image)
        Me.ButtonTeclado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonTeclado.Location = New System.Drawing.Point(317, 248)
        Me.ButtonTeclado.Name = "ButtonTeclado"
        Me.ButtonTeclado.Size = New System.Drawing.Size(110, 54)
        Me.ButtonTeclado.TabIndex = 209
        Me.ButtonTeclado.UseVisualStyleBackColor = True
        '
        'ButtonAbajTxt
        '
        Me.ButtonAbajTxt.BackColor = System.Drawing.Color.Blue
        Me.ButtonAbajTxt.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonAbajTxt.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonAbajTxt.Location = New System.Drawing.Point(375, 188)
        Me.ButtonAbajTxt.Name = "ButtonAbajTxt"
        Me.ButtonAbajTxt.Size = New System.Drawing.Size(52, 54)
        Me.ButtonAbajTxt.TabIndex = 211
        Me.ButtonAbajTxt.Text = "ä"
        Me.ButtonAbajTxt.UseVisualStyleBackColor = False
        '
        'ButtonArriTxt
        '
        Me.ButtonArriTxt.BackColor = System.Drawing.Color.Blue
        Me.ButtonArriTxt.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonArriTxt.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonArriTxt.Location = New System.Drawing.Point(317, 188)
        Me.ButtonArriTxt.Name = "ButtonArriTxt"
        Me.ButtonArriTxt.Size = New System.Drawing.Size(52, 54)
        Me.ButtonArriTxt.TabIndex = 210
        Me.ButtonArriTxt.Text = "ã"
        Me.ButtonArriTxt.UseVisualStyleBackColor = False
        '
        'LabelIndicador
        '
        Me.LabelIndicador.Font = New System.Drawing.Font("Wingdings 3", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LabelIndicador.ForeColor = System.Drawing.Color.Red
        Me.LabelIndicador.Location = New System.Drawing.Point(275, 198)
        Me.LabelIndicador.Name = "LabelIndicador"
        Me.LabelIndicador.Size = New System.Drawing.Size(24, 24)
        Me.LabelIndicador.TabIndex = 212
        Me.LabelIndicador.Text = "á"
        Me.LabelIndicador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button18Cls
        '
        Me.Button18Cls.BackColor = System.Drawing.Color.Blue
        Me.Button18Cls.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button18Cls.ForeColor = System.Drawing.Color.Yellow
        Me.Button18Cls.Location = New System.Drawing.Point(433, 188)
        Me.Button18Cls.Name = "Button18Cls"
        Me.Button18Cls.Size = New System.Drawing.Size(99, 54)
        Me.Button18Cls.TabIndex = 213
        Me.Button18Cls.Text = "Clr"
        Me.Button18Cls.UseVisualStyleBackColor = False
        '
        'GRIDTEXTOSPREDEF
        '
        Me.GRIDTEXTOSPREDEF.AllowUserToAddRows = False
        Me.GRIDTEXTOSPREDEF.AllowUserToDeleteRows = False
        Me.GRIDTEXTOSPREDEF.AllowUserToOrderColumns = True
        Me.GRIDTEXTOSPREDEF.AllowUserToResizeColumns = False
        Me.GRIDTEXTOSPREDEF.AllowUserToResizeRows = False
        Me.GRIDTEXTOSPREDEF.BackgroundColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDTEXTOSPREDEF.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDTEXTOSPREDEF.ColumnHeadersHeight = 4
        Me.GRIDTEXTOSPREDEF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GRIDTEXTOSPREDEF.ColumnHeadersVisible = False
        Me.GRIDTEXTOSPREDEF.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColText1, Me.ColText2, Me.ColText3, Me.ColText4, Me.ColID})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDTEXTOSPREDEF.DefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDTEXTOSPREDEF.GridColor = System.Drawing.SystemColors.Info
        Me.GRIDTEXTOSPREDEF.Location = New System.Drawing.Point(12, 357)
        Me.GRIDTEXTOSPREDEF.MultiSelect = False
        Me.GRIDTEXTOSPREDEF.Name = "GRIDTEXTOSPREDEF"
        Me.GRIDTEXTOSPREDEF.ReadOnly = True
        Me.GRIDTEXTOSPREDEF.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.GRIDTEXTOSPREDEF.RowHeadersVisible = False
        Me.GRIDTEXTOSPREDEF.RowHeadersWidth = 4
        Me.GRIDTEXTOSPREDEF.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.GRIDTEXTOSPREDEF.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDTEXTOSPREDEF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDTEXTOSPREDEF.Size = New System.Drawing.Size(908, 437)
        Me.GRIDTEXTOSPREDEF.TabIndex = 214
        '
        'ColText1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColText1.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColText1.HeaderText = ""
        Me.ColText1.Name = "ColText1"
        Me.ColText1.ReadOnly = True
        Me.ColText1.Width = 200
        '
        'ColText2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColText2.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColText2.HeaderText = ""
        Me.ColText2.Name = "ColText2"
        Me.ColText2.ReadOnly = True
        Me.ColText2.Width = 200
        '
        'ColText3
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColText3.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColText3.HeaderText = ""
        Me.ColText3.Name = "ColText3"
        Me.ColText3.ReadOnly = True
        Me.ColText3.Width = 200
        '
        'ColText4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColText4.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColText4.HeaderText = ""
        Me.ColText4.Name = "ColText4"
        Me.ColText4.ReadOnly = True
        Me.ColText4.Width = 200
        '
        'ColID
        '
        Me.ColID.HeaderText = ""
        Me.ColID.Name = "ColID"
        Me.ColID.ReadOnly = True
        Me.ColID.Visible = False
        '
        'ButtonGrdAbaj
        '
        Me.ButtonGrdAbaj.BackColor = System.Drawing.Color.Blue
        Me.ButtonGrdAbaj.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGrdAbaj.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGrdAbaj.Location = New System.Drawing.Point(926, 417)
        Me.ButtonGrdAbaj.Name = "ButtonGrdAbaj"
        Me.ButtonGrdAbaj.Size = New System.Drawing.Size(70, 54)
        Me.ButtonGrdAbaj.TabIndex = 216
        Me.ButtonGrdAbaj.Text = "ä"
        Me.ButtonGrdAbaj.UseVisualStyleBackColor = False
        '
        'ButtonGrdArri
        '
        Me.ButtonGrdArri.BackColor = System.Drawing.Color.Blue
        Me.ButtonGrdArri.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGrdArri.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGrdArri.Location = New System.Drawing.Point(926, 357)
        Me.ButtonGrdArri.Name = "ButtonGrdArri"
        Me.ButtonGrdArri.Size = New System.Drawing.Size(70, 54)
        Me.ButtonGrdArri.TabIndex = 215
        Me.ButtonGrdArri.Text = "ã"
        Me.ButtonGrdArri.UseVisualStyleBackColor = False
        '
        'ButtonSelTextos
        '
        Me.ButtonSelTextos.BackColor = System.Drawing.Color.Blue
        Me.ButtonSelTextos.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSelTextos.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonSelTextos.Location = New System.Drawing.Point(926, 477)
        Me.ButtonSelTextos.Name = "ButtonSelTextos"
        Me.ButtonSelTextos.Size = New System.Drawing.Size(70, 54)
        Me.ButtonSelTextos.TabIndex = 217
        Me.ButtonSelTextos.Text = "Selec."
        Me.ButtonSelTextos.UseVisualStyleBackColor = False
        '
        'ButtonGrabaTextos
        '
        Me.ButtonGrabaTextos.BackColor = System.Drawing.Color.Blue
        Me.ButtonGrabaTextos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonGrabaTextos.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGrabaTextos.Location = New System.Drawing.Point(12, 309)
        Me.ButtonGrabaTextos.Name = "ButtonGrabaTextos"
        Me.ButtonGrabaTextos.Size = New System.Drawing.Size(257, 43)
        Me.ButtonGrabaTextos.TabIndex = 218
        Me.ButtonGrabaTextos.Text = "Guardar Frases Favoritas"
        Me.ButtonGrabaTextos.UseVisualStyleBackColor = False
        '
        'ButtonBorraTextos
        '
        Me.ButtonBorraTextos.BackColor = System.Drawing.Color.Blue
        Me.ButtonBorraTextos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonBorraTextos.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonBorraTextos.Location = New System.Drawing.Point(278, 308)
        Me.ButtonBorraTextos.Name = "ButtonBorraTextos"
        Me.ButtonBorraTextos.Size = New System.Drawing.Size(257, 43)
        Me.ButtonBorraTextos.TabIndex = 219
        Me.ButtonBorraTextos.Text = "Borrar Favorita Selecionada"
        Me.ButtonBorraTextos.UseVisualStyleBackColor = False
        '
        'LabelNomArea
        '
        Me.LabelNomArea.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabelNomArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelNomArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNomArea.Location = New System.Drawing.Point(543, 245)
        Me.LabelNomArea.Name = "LabelNomArea"
        Me.LabelNomArea.Size = New System.Drawing.Size(251, 22)
        Me.LabelNomArea.TabIndex = 220
        Me.LabelNomArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonTerminarMESA
        '
        Me.ButtonTerminarMESA.BackColor = System.Drawing.Color.Blue
        Me.ButtonTerminarMESA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonTerminarMESA.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonTerminarMESA.Location = New System.Drawing.Point(543, 270)
        Me.ButtonTerminarMESA.Name = "ButtonTerminarMESA"
        Me.ButtonTerminarMESA.Size = New System.Drawing.Size(251, 43)
        Me.ButtonTerminarMESA.TabIndex = 221
        Me.ButtonTerminarMESA.Text = "Terminar Mesa "
        Me.ButtonTerminarMESA.UseVisualStyleBackColor = False
        '
        'TCONA418
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 806)
        Me.Controls.Add(Me.ButtonTerminarMESA)
        Me.Controls.Add(Me.LabelNomArea)
        Me.Controls.Add(Me.ButtonBorraTextos)
        Me.Controls.Add(Me.ButtonGrabaTextos)
        Me.Controls.Add(Me.ButtonSelTextos)
        Me.Controls.Add(Me.ButtonGrdAbaj)
        Me.Controls.Add(Me.ButtonGrdArri)
        Me.Controls.Add(Me.GRIDTEXTOSPREDEF)
        Me.Controls.Add(Me.Button18Cls)
        Me.Controls.Add(Me.LabelIndicador)
        Me.Controls.Add(Me.ButtonAbajTxt)
        Me.Controls.Add(Me.ButtonArriTxt)
        Me.Controls.Add(Me.ButtonTeclado)
        Me.Controls.Add(Me.TextBoxMensaL4)
        Me.Controls.Add(Me.TextBoxMensaL3)
        Me.Controls.Add(Me.TextBoxMensaL2)
        Me.Controls.Add(Me.TextBoxMensaL1)
        Me.Controls.Add(Me.ButtonENviaMensaje)
        Me.Controls.Add(Me.ButtonGRIDAbajo)
        Me.Controls.Add(Me.ButtonGRIDArriba)
        Me.Controls.Add(Me.GRIDAREAS1)
        Me.Controls.Add(Me.Button18Salir)
        Me.Name = "TCONA418"
        Me.Text = "TCONA418"
        CType(Me.GRIDAREAS1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDTEXTOSPREDEF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button18Salir As Button
    Friend WithEvents GRIDAREAS1 As DataGridView
    Friend WithEvents Area As DataGridViewTextBoxColumn
    Friend WithEvents Descrip As DataGridViewTextBoxColumn
    Friend WithEvents PtoImpre As DataGridViewTextBoxColumn
    Friend WithEvents Area2 As DataGridViewTextBoxColumn
    Friend WithEvents Area3 As DataGridViewTextBoxColumn
    Friend WithEvents Area4 As DataGridViewTextBoxColumn
    Friend WithEvents T As DataGridViewTextBoxColumn
    Friend WithEvents TipoImpre As DataGridViewCheckBoxColumn
    Friend WithEvents Modelo As DataGridViewTextBoxColumn
    Friend WithEvents ButtonGRIDAbajo As Button
    Friend WithEvents ButtonGRIDArriba As Button
    Friend WithEvents ButtonENviaMensaje As Button
    Friend WithEvents TextBoxMensaL1 As TextBox
    Friend WithEvents TextBoxMensaL2 As TextBox
    Friend WithEvents TextBoxMensaL3 As TextBox
    Friend WithEvents TextBoxMensaL4 As TextBox
    Friend WithEvents ButtonTeclado As Button
    Friend WithEvents ButtonAbajTxt As Button
    Friend WithEvents ButtonArriTxt As Button
    Friend WithEvents LabelIndicador As Label
    Friend WithEvents Button18Cls As Button
    Friend WithEvents GRIDTEXTOSPREDEF As DataGridView
    Friend WithEvents ButtonGrdAbaj As Button
    Friend WithEvents ButtonGrdArri As Button
    Friend WithEvents ButtonSelTextos As Button
    Friend WithEvents ButtonGrabaTextos As Button
    Friend WithEvents ButtonBorraTextos As Button
    Friend WithEvents ColText1 As DataGridViewTextBoxColumn
    Friend WithEvents ColText2 As DataGridViewTextBoxColumn
    Friend WithEvents ColText3 As DataGridViewTextBoxColumn
    Friend WithEvents ColText4 As DataGridViewTextBoxColumn
    Friend WithEvents ColID As DataGridViewTextBoxColumn
    Friend WithEvents LabelNomArea As Label
    Friend WithEvents ButtonTerminarMESA As Button
End Class
