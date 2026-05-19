<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TCONA419
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
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button19Salir = New System.Windows.Forms.Button()
        Me.GRIDCAJAS = New System.Windows.Forms.DataGridView()
        Me.CodVen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NomVen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pwd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ButtonGRIDCLAbajo = New System.Windows.Forms.Button()
        Me.ButtonGRIDCLArriba = New System.Windows.Forms.Button()
        CType(Me.GRIDCAJAS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button19Salir
        '
        Me.Button19Salir.BackColor = System.Drawing.Color.Blue
        Me.Button19Salir.Font = New System.Drawing.Font("Consolas", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button19Salir.ForeColor = System.Drawing.Color.Yellow
        Me.Button19Salir.Location = New System.Drawing.Point(12, 452)
        Me.Button19Salir.Name = "Button19Salir"
        Me.Button19Salir.Size = New System.Drawing.Size(298, 54)
        Me.Button19Salir.TabIndex = 162
        Me.Button19Salir.Text = "Seleccionar"
        Me.Button19Salir.UseVisualStyleBackColor = False
        '
        'GRIDCAJAS
        '
        Me.GRIDCAJAS.AllowUserToAddRows = False
        Me.GRIDCAJAS.AllowUserToDeleteRows = False
        Me.GRIDCAJAS.AllowUserToOrderColumns = True
        Me.GRIDCAJAS.AllowUserToResizeColumns = False
        Me.GRIDCAJAS.AllowUserToResizeRows = False
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black
        Me.GRIDCAJAS.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle19
        Me.GRIDCAJAS.BackgroundColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDCAJAS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.GRIDCAJAS.ColumnHeadersHeight = 4
        Me.GRIDCAJAS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GRIDCAJAS.ColumnHeadersVisible = False
        Me.GRIDCAJAS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CodVen, Me.NomVen, Me.pwd})
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle23.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCAJAS.DefaultCellStyle = DataGridViewCellStyle23
        Me.GRIDCAJAS.GridColor = System.Drawing.SystemColors.Info
        Me.GRIDCAJAS.Location = New System.Drawing.Point(12, 12)
        Me.GRIDCAJAS.MultiSelect = False
        Me.GRIDCAJAS.Name = "GRIDCAJAS"
        Me.GRIDCAJAS.ReadOnly = True
        Me.GRIDCAJAS.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.GRIDCAJAS.RowHeadersVisible = False
        Me.GRIDCAJAS.RowHeadersWidth = 4
        Me.GRIDCAJAS.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GRIDCAJAS.RowsDefaultCellStyle = DataGridViewCellStyle24
        Me.GRIDCAJAS.RowTemplate.Height = 60
        Me.GRIDCAJAS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDCAJAS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDCAJAS.Size = New System.Drawing.Size(298, 434)
        Me.GRIDCAJAS.TabIndex = 163
        '
        'CodVen
        '
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.CodVen.DefaultCellStyle = DataGridViewCellStyle21
        Me.CodVen.HeaderText = ""
        Me.CodVen.Name = "CodVen"
        Me.CodVen.ReadOnly = True
        Me.CodVen.Visible = False
        Me.CodVen.Width = 50
        '
        'NomVen
        '
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.NomVen.DefaultCellStyle = DataGridViewCellStyle22
        Me.NomVen.HeaderText = ""
        Me.NomVen.Name = "NomVen"
        Me.NomVen.ReadOnly = True
        Me.NomVen.Width = 400
        '
        'pwd
        '
        Me.pwd.HeaderText = ""
        Me.pwd.Name = "pwd"
        Me.pwd.ReadOnly = True
        Me.pwd.Visible = False
        '
        'ButtonGRIDCLAbajo
        '
        Me.ButtonGRIDCLAbajo.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDCLAbajo.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDCLAbajo.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDCLAbajo.Location = New System.Drawing.Point(316, 234)
        Me.ButtonGRIDCLAbajo.Name = "ButtonGRIDCLAbajo"
        Me.ButtonGRIDCLAbajo.Size = New System.Drawing.Size(60, 50)
        Me.ButtonGRIDCLAbajo.TabIndex = 165
        Me.ButtonGRIDCLAbajo.Text = "ä"
        Me.ButtonGRIDCLAbajo.UseVisualStyleBackColor = False
        '
        'ButtonGRIDCLArriba
        '
        Me.ButtonGRIDCLArriba.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDCLArriba.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDCLArriba.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDCLArriba.Location = New System.Drawing.Point(316, 145)
        Me.ButtonGRIDCLArriba.Name = "ButtonGRIDCLArriba"
        Me.ButtonGRIDCLArriba.Size = New System.Drawing.Size(60, 50)
        Me.ButtonGRIDCLArriba.TabIndex = 164
        Me.ButtonGRIDCLArriba.Text = "ã"
        Me.ButtonGRIDCLArriba.UseVisualStyleBackColor = False
        '
        'TCONA419
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 520)
        Me.Controls.Add(Me.ButtonGRIDCLAbajo)
        Me.Controls.Add(Me.ButtonGRIDCLArriba)
        Me.Controls.Add(Me.GRIDCAJAS)
        Me.Controls.Add(Me.Button19Salir)
        Me.Name = "TCONA419"
        Me.Text = "TCONA419"
        CType(Me.GRIDCAJAS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button19Salir As Button
    Friend WithEvents GRIDCAJAS As DataGridView
    Friend WithEvents CodVen As DataGridViewTextBoxColumn
    Friend WithEvents NomVen As DataGridViewTextBoxColumn
    Friend WithEvents pwd As DataGridViewTextBoxColumn
    Friend WithEvents ButtonGRIDCLAbajo As Button
    Friend WithEvents ButtonGRIDCLArriba As Button
End Class
