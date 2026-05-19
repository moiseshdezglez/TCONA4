<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TCONA410
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
        Me.LabelNombreSala = New System.Windows.Forms.Label()
        Me.ButtonMant1 = New System.Windows.Forms.Button()
        Me.ButtonMant2 = New System.Windows.Forms.Button()
        Me.ButtonMant4 = New System.Windows.Forms.Button()
        Me.ButtonMant3 = New System.Windows.Forms.Button()
        Me.ButtonMant5 = New System.Windows.Forms.Button()
        Me.ButtonMVSalir = New System.Windows.Forms.Button()
        Me.ButtonMant6 = New System.Windows.Forms.Button()
        Me.ButtonConsu1 = New System.Windows.Forms.Button()
        Me.ButtonMantClaves = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelNombreSala
        '
        Me.LabelNombreSala.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabelNombreSala.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNombreSala.ForeColor = System.Drawing.Color.Yellow
        Me.LabelNombreSala.Location = New System.Drawing.Point(15, 9)
        Me.LabelNombreSala.Name = "LabelNombreSala"
        Me.LabelNombreSala.Size = New System.Drawing.Size(981, 30)
        Me.LabelNombreSala.TabIndex = 99
        Me.LabelNombreSala.Text = "MANTENIMIENTOS DE APOYO"
        Me.LabelNombreSala.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonMant1
        '
        Me.ButtonMant1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMant1.Location = New System.Drawing.Point(15, 56)
        Me.ButtonMant1.Name = "ButtonMant1"
        Me.ButtonMant1.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMant1.TabIndex = 100
        Me.ButtonMant1.Text = "SALAS / MESAS"
        Me.ButtonMant1.UseVisualStyleBackColor = False
        '
        'ButtonMant2
        '
        Me.ButtonMant2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMant2.Location = New System.Drawing.Point(15, 153)
        Me.ButtonMant2.Name = "ButtonMant2"
        Me.ButtonMant2.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMant2.TabIndex = 101
        Me.ButtonMant2.Text = "COMBINADOS (GRUPOS)"
        Me.ButtonMant2.UseVisualStyleBackColor = False
        '
        'ButtonMant4
        '
        Me.ButtonMant4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMant4.Location = New System.Drawing.Point(314, 153)
        Me.ButtonMant4.Name = "ButtonMant4"
        Me.ButtonMant4.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMant4.TabIndex = 103
        Me.ButtonMant4.Text = "ARTICULOS"
        Me.ButtonMant4.UseVisualStyleBackColor = False
        '
        'ButtonMant3
        '
        Me.ButtonMant3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMant3.Location = New System.Drawing.Point(314, 56)
        Me.ButtonMant3.Name = "ButtonMant3"
        Me.ButtonMant3.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMant3.TabIndex = 102
        Me.ButtonMant3.Text = "FAMILIAS"
        Me.ButtonMant3.UseVisualStyleBackColor = False
        '
        'ButtonMant5
        '
        Me.ButtonMant5.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMant5.Location = New System.Drawing.Point(618, 56)
        Me.ButtonMant5.Name = "ButtonMant5"
        Me.ButtonMant5.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMant5.TabIndex = 104
        Me.ButtonMant5.Text = "ALMACENES"
        Me.ButtonMant5.UseVisualStyleBackColor = False
        '
        'ButtonMVSalir
        '
        Me.ButtonMVSalir.BackColor = System.Drawing.Color.Salmon
        Me.ButtonMVSalir.Location = New System.Drawing.Point(732, 636)
        Me.ButtonMVSalir.Name = "ButtonMVSalir"
        Me.ButtonMVSalir.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMVSalir.TabIndex = 105
        Me.ButtonMVSalir.Text = "Salir"
        Me.ButtonMVSalir.UseVisualStyleBackColor = False
        '
        'ButtonMant6
        '
        Me.ButtonMant6.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMant6.Location = New System.Drawing.Point(618, 153)
        Me.ButtonMant6.Name = "ButtonMant6"
        Me.ButtonMant6.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMant6.TabIndex = 106
        Me.ButtonMant6.Text = "VENDEDORES"
        Me.ButtonMant6.UseVisualStyleBackColor = False
        '
        'ButtonConsu1
        '
        Me.ButtonConsu1.BackColor = System.Drawing.SystemColors.Info
        Me.ButtonConsu1.Location = New System.Drawing.Point(18, 286)
        Me.ButtonConsu1.Name = "ButtonConsu1"
        Me.ButtonConsu1.Size = New System.Drawing.Size(264, 81)
        Me.ButtonConsu1.TabIndex = 107
        Me.ButtonConsu1.Text = "CONSULTA MOVIM. MESAS"
        Me.ButtonConsu1.UseVisualStyleBackColor = False
        '
        'ButtonMantClaves
        '
        Me.ButtonMantClaves.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMantClaves.Enabled = False
        Me.ButtonMantClaves.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonMantClaves.Location = New System.Drawing.Point(314, 285)
        Me.ButtonMantClaves.Name = "ButtonMantClaves"
        Me.ButtonMantClaves.Size = New System.Drawing.Size(264, 81)
        Me.ButtonMantClaves.TabIndex = 216
        Me.ButtonMantClaves.Text = "Tareas de Control Aplicación"
        Me.ButtonMantClaves.UseVisualStyleBackColor = False
        '
        'TCONA410
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.ButtonMantClaves)
        Me.Controls.Add(Me.ButtonConsu1)
        Me.Controls.Add(Me.ButtonMant6)
        Me.Controls.Add(Me.ButtonMVSalir)
        Me.Controls.Add(Me.ButtonMant5)
        Me.Controls.Add(Me.ButtonMant4)
        Me.Controls.Add(Me.ButtonMant3)
        Me.Controls.Add(Me.ButtonMant2)
        Me.Controls.Add(Me.ButtonMant1)
        Me.Controls.Add(Me.LabelNombreSala)
        Me.Name = "TCONA410"
        Me.Text = "TCONA410"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelNombreSala As Label
    Friend WithEvents ButtonMant1 As Button
    Friend WithEvents ButtonMant2 As Button
    Friend WithEvents ButtonMant4 As Button
    Friend WithEvents ButtonMant3 As Button
    Friend WithEvents ButtonMant5 As Button
    Friend WithEvents ButtonMVSalir As Button
    Friend WithEvents ButtonMant6 As Button
    Friend WithEvents ButtonConsu1 As Button
    Friend WithEvents ButtonMantClaves As Button
End Class
