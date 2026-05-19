<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TCONA403
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GRIDZOOM = New System.Windows.Forms.DataGridView()
        Me.CodArt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnidEx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NomArt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnidNu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Importe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo_E_N = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Combinados = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Raciones = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ButtonCABSALIR = New System.Windows.Forms.Button()
        Me.ButtonGRIDAbajo = New System.Windows.Forms.Button()
        Me.ButtonGRIDArriba = New System.Windows.Forms.Button()
        Me.ButtonVerCombi = New System.Windows.Forms.Button()
        CType(Me.GRIDZOOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GRIDZOOM
        '
        Me.GRIDZOOM.AllowUserToAddRows = False
        Me.GRIDZOOM.AllowUserToDeleteRows = False
        Me.GRIDZOOM.AllowUserToResizeColumns = False
        Me.GRIDZOOM.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.GRIDZOOM.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDZOOM.BackgroundColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDZOOM.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDZOOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDZOOM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CodArt, Me.UnidEx, Me.NomArt, Me.UnidNu, Me.Precio, Me.Importe, Me.Tipo_E_N, Me.Combinados, Me.Raciones})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDZOOM.DefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDZOOM.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GRIDZOOM.GridColor = System.Drawing.SystemColors.Info
        Me.GRIDZOOM.Location = New System.Drawing.Point(12, 12)
        Me.GRIDZOOM.MultiSelect = False
        Me.GRIDZOOM.Name = "GRIDZOOM"
        Me.GRIDZOOM.ReadOnly = True
        Me.GRIDZOOM.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.GRIDZOOM.RowHeadersWidth = 4
        Me.GRIDZOOM.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GRIDZOOM.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDZOOM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDZOOM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDZOOM.Size = New System.Drawing.Size(661, 578)
        Me.GRIDZOOM.TabIndex = 64
        '
        'CodArt
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.CodArt.DefaultCellStyle = DataGridViewCellStyle3
        Me.CodArt.HeaderText = "Código"
        Me.CodArt.Name = "CodArt"
        Me.CodArt.ReadOnly = True
        Me.CodArt.Width = 80
        '
        'UnidEx
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.UnidEx.DefaultCellStyle = DataGridViewCellStyle4
        Me.UnidEx.HeaderText = "Unid. Mesa"
        Me.UnidEx.Name = "UnidEx"
        Me.UnidEx.ReadOnly = True
        Me.UnidEx.Width = 60
        '
        'NomArt
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.NomArt.DefaultCellStyle = DataGridViewCellStyle5
        Me.NomArt.HeaderText = "Nombre del Producto"
        Me.NomArt.Name = "NomArt"
        Me.NomArt.ReadOnly = True
        Me.NomArt.Width = 200
        '
        'UnidNu
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.UnidNu.DefaultCellStyle = DataGridViewCellStyle6
        Me.UnidNu.HeaderText = "Nuevas Unid."
        Me.UnidNu.Name = "UnidNu"
        Me.UnidNu.ReadOnly = True
        Me.UnidNu.Width = 60
        '
        'Precio
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Precio.DefaultCellStyle = DataGridViewCellStyle7
        Me.Precio.HeaderText = "Precio"
        Me.Precio.Name = "Precio"
        Me.Precio.ReadOnly = True
        Me.Precio.Width = 60
        '
        'Importe
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Importe.DefaultCellStyle = DataGridViewCellStyle8
        Me.Importe.HeaderText = "Importe"
        Me.Importe.Name = "Importe"
        Me.Importe.ReadOnly = True
        Me.Importe.Width = 60
        '
        'Tipo_E_N
        '
        Me.Tipo_E_N.HeaderText = "Tipo"
        Me.Tipo_E_N.Name = "Tipo_E_N"
        Me.Tipo_E_N.ReadOnly = True
        Me.Tipo_E_N.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Tipo_E_N.Visible = False
        Me.Tipo_E_N.Width = 10
        '
        'Combinados
        '
        Me.Combinados.HeaderText = ""
        Me.Combinados.Name = "Combinados"
        Me.Combinados.ReadOnly = True
        Me.Combinados.Visible = False
        '
        'Raciones
        '
        Me.Raciones.HeaderText = ""
        Me.Raciones.Name = "Raciones"
        Me.Raciones.ReadOnly = True
        Me.Raciones.Visible = False
        '
        'ButtonCABSALIR
        '
        Me.ButtonCABSALIR.BackColor = System.Drawing.Color.Salmon
        Me.ButtonCABSALIR.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCABSALIR.Location = New System.Drawing.Point(440, 596)
        Me.ButtonCABSALIR.Name = "ButtonCABSALIR"
        Me.ButtonCABSALIR.Size = New System.Drawing.Size(232, 53)
        Me.ButtonCABSALIR.TabIndex = 65
        Me.ButtonCABSALIR.Text = "Salir"
        Me.ButtonCABSALIR.UseVisualStyleBackColor = False
        '
        'ButtonGRIDAbajo
        '
        Me.ButtonGRIDAbajo.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDAbajo.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDAbajo.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDAbajo.Location = New System.Drawing.Point(83, 596)
        Me.ButtonGRIDAbajo.Name = "ButtonGRIDAbajo"
        Me.ButtonGRIDAbajo.Size = New System.Drawing.Size(65, 53)
        Me.ButtonGRIDAbajo.TabIndex = 159
        Me.ButtonGRIDAbajo.Text = "ä"
        Me.ButtonGRIDAbajo.UseVisualStyleBackColor = False
        '
        'ButtonGRIDArriba
        '
        Me.ButtonGRIDArriba.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDArriba.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDArriba.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDArriba.Location = New System.Drawing.Point(12, 596)
        Me.ButtonGRIDArriba.Name = "ButtonGRIDArriba"
        Me.ButtonGRIDArriba.Size = New System.Drawing.Size(65, 53)
        Me.ButtonGRIDArriba.TabIndex = 158
        Me.ButtonGRIDArriba.Text = "ã"
        Me.ButtonGRIDArriba.UseVisualStyleBackColor = False
        '
        'ButtonVerCombi
        '
        Me.ButtonVerCombi.BackColor = System.Drawing.Color.Blue
        Me.ButtonVerCombi.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonVerCombi.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonVerCombi.Location = New System.Drawing.Point(178, 596)
        Me.ButtonVerCombi.Name = "ButtonVerCombi"
        Me.ButtonVerCombi.Size = New System.Drawing.Size(167, 53)
        Me.ButtonVerCombi.TabIndex = 160
        Me.ButtonVerCombi.Text = "Ver Combinados"
        Me.ButtonVerCombi.UseVisualStyleBackColor = False
        '
        'TCONA403
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 661)
        Me.Controls.Add(Me.ButtonVerCombi)
        Me.Controls.Add(Me.ButtonGRIDAbajo)
        Me.Controls.Add(Me.ButtonGRIDArriba)
        Me.Controls.Add(Me.ButtonCABSALIR)
        Me.Controls.Add(Me.GRIDZOOM)
        Me.Name = "TCONA403"
        Me.Text = "Form1"
        CType(Me.GRIDZOOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GRIDZOOM As DataGridView
    Friend WithEvents CodArt As DataGridViewTextBoxColumn
    Friend WithEvents UnidEx As DataGridViewTextBoxColumn
    Friend WithEvents NomArt As DataGridViewTextBoxColumn
    Friend WithEvents UnidNu As DataGridViewTextBoxColumn
    Friend WithEvents Precio As DataGridViewTextBoxColumn
    Friend WithEvents Importe As DataGridViewTextBoxColumn
    Friend WithEvents Tipo_E_N As DataGridViewTextBoxColumn
    Friend WithEvents Combinados As DataGridViewTextBoxColumn
    Friend WithEvents Raciones As DataGridViewTextBoxColumn
    Friend WithEvents ButtonCABSALIR As Button
    Friend WithEvents ButtonGRIDAbajo As Button
    Friend WithEvents ButtonGRIDArriba As Button
    Friend WithEvents ButtonVerCombi As Button
End Class
