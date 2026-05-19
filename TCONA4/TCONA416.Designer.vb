<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TCONA416
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button16Salir = New System.Windows.Forms.Button()
        Me.Button16Envia = New System.Windows.Forms.Button()
        Me.GRIDENVAREAS = New System.Windows.Forms.DataGridView()
        Me.Colu1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colu2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colu3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ButtonGRIDAbajo = New System.Windows.Forms.Button()
        Me.ButtonGRIDArriba = New System.Windows.Forms.Button()
        CType(Me.GRIDENVAREAS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button16Salir
        '
        Me.Button16Salir.BackColor = System.Drawing.Color.Salmon
        Me.Button16Salir.Font = New System.Drawing.Font("Consolas", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button16Salir.ForeColor = System.Drawing.Color.Black
        Me.Button16Salir.Location = New System.Drawing.Point(329, 667)
        Me.Button16Salir.Name = "Button16Salir"
        Me.Button16Salir.Size = New System.Drawing.Size(111, 50)
        Me.Button16Salir.TabIndex = 65
        Me.Button16Salir.Text = "VOLVER"
        Me.Button16Salir.UseVisualStyleBackColor = False
        '
        'Button16Envia
        '
        Me.Button16Envia.BackColor = System.Drawing.Color.Blue
        Me.Button16Envia.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button16Envia.ForeColor = System.Drawing.Color.Yellow
        Me.Button16Envia.Location = New System.Drawing.Point(185, 667)
        Me.Button16Envia.Name = "Button16Envia"
        Me.Button16Envia.Size = New System.Drawing.Size(126, 50)
        Me.Button16Envia.TabIndex = 66
        Me.Button16Envia.Text = "ENVIAR"
        Me.Button16Envia.UseVisualStyleBackColor = False
        '
        'GRIDENVAREAS
        '
        Me.GRIDENVAREAS.AllowUserToAddRows = False
        Me.GRIDENVAREAS.AllowUserToDeleteRows = False
        Me.GRIDENVAREAS.AllowUserToOrderColumns = True
        Me.GRIDENVAREAS.AllowUserToResizeColumns = False
        Me.GRIDENVAREAS.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.GRIDENVAREAS.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDENVAREAS.BackgroundColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDENVAREAS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDENVAREAS.ColumnHeadersHeight = 4
        Me.GRIDENVAREAS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GRIDENVAREAS.ColumnHeadersVisible = False
        Me.GRIDENVAREAS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Colu1, Me.Colu2, Me.Colu3})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDENVAREAS.DefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDENVAREAS.GridColor = System.Drawing.SystemColors.Info
        Me.GRIDENVAREAS.Location = New System.Drawing.Point(12, 12)
        Me.GRIDENVAREAS.MultiSelect = False
        Me.GRIDENVAREAS.Name = "GRIDENVAREAS"
        Me.GRIDENVAREAS.ReadOnly = True
        Me.GRIDENVAREAS.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.GRIDENVAREAS.RowHeadersVisible = False
        Me.GRIDENVAREAS.RowHeadersWidth = 4
        Me.GRIDENVAREAS.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GRIDENVAREAS.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDENVAREAS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDENVAREAS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDENVAREAS.Size = New System.Drawing.Size(428, 646)
        Me.GRIDENVAREAS.TabIndex = 67
        '
        'Colu1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Colu1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Colu1.HeaderText = ""
        Me.Colu1.Name = "Colu1"
        Me.Colu1.ReadOnly = True
        '
        'Colu2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Colu2.DefaultCellStyle = DataGridViewCellStyle4
        Me.Colu2.HeaderText = ""
        Me.Colu2.Name = "Colu2"
        Me.Colu2.ReadOnly = True
        Me.Colu2.Width = 300
        '
        'Colu3
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Colu3.DefaultCellStyle = DataGridViewCellStyle5
        Me.Colu3.HeaderText = ""
        Me.Colu3.Name = "Colu3"
        Me.Colu3.ReadOnly = True
        Me.Colu3.Visible = False
        Me.Colu3.Width = 200
        '
        'ButtonGRIDAbajo
        '
        Me.ButtonGRIDAbajo.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDAbajo.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDAbajo.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDAbajo.Location = New System.Drawing.Point(80, 667)
        Me.ButtonGRIDAbajo.Name = "ButtonGRIDAbajo"
        Me.ButtonGRIDAbajo.Size = New System.Drawing.Size(62, 50)
        Me.ButtonGRIDAbajo.TabIndex = 159
        Me.ButtonGRIDAbajo.Text = "ä"
        Me.ButtonGRIDAbajo.UseVisualStyleBackColor = False
        '
        'ButtonGRIDArriba
        '
        Me.ButtonGRIDArriba.BackColor = System.Drawing.Color.Blue
        Me.ButtonGRIDArriba.Font = New System.Drawing.Font("Wingdings 3", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonGRIDArriba.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonGRIDArriba.Location = New System.Drawing.Point(12, 667)
        Me.ButtonGRIDArriba.Name = "ButtonGRIDArriba"
        Me.ButtonGRIDArriba.Size = New System.Drawing.Size(62, 50)
        Me.ButtonGRIDArriba.TabIndex = 158
        Me.ButtonGRIDArriba.Text = "ã"
        Me.ButtonGRIDArriba.UseVisualStyleBackColor = False
        '
        'TCONA416
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 729)
        Me.Controls.Add(Me.ButtonGRIDAbajo)
        Me.Controls.Add(Me.ButtonGRIDArriba)
        Me.Controls.Add(Me.GRIDENVAREAS)
        Me.Controls.Add(Me.Button16Envia)
        Me.Controls.Add(Me.Button16Salir)
        Me.Name = "TCONA416"
        Me.Text = "TCONA416"
        CType(Me.GRIDENVAREAS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button16Salir As Button
    Friend WithEvents Button16Envia As Button
    Friend WithEvents GRIDENVAREAS As DataGridView
    Friend WithEvents Colu1 As DataGridViewTextBoxColumn
    Friend WithEvents Colu2 As DataGridViewTextBoxColumn
    Friend WithEvents Colu3 As DataGridViewTextBoxColumn
    Friend WithEvents ButtonGRIDAbajo As Button
    Friend WithEvents ButtonGRIDArriba As Button
End Class
