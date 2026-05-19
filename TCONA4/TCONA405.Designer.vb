<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TCONA405
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TCONA405))
        Me.ButtonX = New System.Windows.Forms.Button()
        Me.ButtonZ = New System.Windows.Forms.Button()
        Me.ButtonRefer = New System.Windows.Forms.Button()
        Me.ButtonCABSALIR = New System.Windows.Forms.Button()
        Me.ButtonMantVarios = New System.Windows.Forms.Button()
        Me.LabelMuestra1 = New System.Windows.Forms.Label()
        Me.TextBoxPwdGeneral = New System.Windows.Forms.TextBox()
        Me.ButtonTecladoPwd = New System.Windows.Forms.Button()
        Me.ButtonPwdCls = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonX
        '
        Me.ButtonX.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonX.Enabled = False
        Me.ButtonX.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonX.Location = New System.Drawing.Point(23, 208)
        Me.ButtonX.Name = "ButtonX"
        Me.ButtonX.Size = New System.Drawing.Size(232, 81)
        Me.ButtonX.TabIndex = 0
        Me.ButtonX.Text = "X"
        Me.ButtonX.UseVisualStyleBackColor = False
        '
        'ButtonZ
        '
        Me.ButtonZ.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonZ.Enabled = False
        Me.ButtonZ.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonZ.Location = New System.Drawing.Point(23, 308)
        Me.ButtonZ.Name = "ButtonZ"
        Me.ButtonZ.Size = New System.Drawing.Size(232, 81)
        Me.ButtonZ.TabIndex = 1
        Me.ButtonZ.Text = "Z"
        Me.ButtonZ.UseVisualStyleBackColor = False
        '
        'ButtonRefer
        '
        Me.ButtonRefer.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonRefer.Enabled = False
        Me.ButtonRefer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRefer.Location = New System.Drawing.Point(340, 208)
        Me.ButtonRefer.Name = "ButtonRefer"
        Me.ButtonRefer.Size = New System.Drawing.Size(232, 81)
        Me.ButtonRefer.TabIndex = 2
        Me.ButtonRefer.Text = "Referencias Generales"
        Me.ButtonRefer.UseVisualStyleBackColor = False
        '
        'ButtonCABSALIR
        '
        Me.ButtonCABSALIR.BackColor = System.Drawing.Color.Salmon
        Me.ButtonCABSALIR.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCABSALIR.Location = New System.Drawing.Point(340, 408)
        Me.ButtonCABSALIR.Name = "ButtonCABSALIR"
        Me.ButtonCABSALIR.Size = New System.Drawing.Size(232, 81)
        Me.ButtonCABSALIR.TabIndex = 3
        Me.ButtonCABSALIR.Text = "Salir"
        Me.ButtonCABSALIR.UseVisualStyleBackColor = False
        '
        'ButtonMantVarios
        '
        Me.ButtonMantVarios.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonMantVarios.Enabled = False
        Me.ButtonMantVarios.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonMantVarios.Location = New System.Drawing.Point(340, 308)
        Me.ButtonMantVarios.Name = "ButtonMantVarios"
        Me.ButtonMantVarios.Size = New System.Drawing.Size(232, 81)
        Me.ButtonMantVarios.TabIndex = 4
        Me.ButtonMantVarios.Text = "Aplicaciones de Apoyo"
        Me.ButtonMantVarios.UseVisualStyleBackColor = False
        '
        'LabelMuestra1
        '
        Me.LabelMuestra1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabelMuestra1.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMuestra1.ForeColor = System.Drawing.Color.Yellow
        Me.LabelMuestra1.Location = New System.Drawing.Point(23, 25)
        Me.LabelMuestra1.Name = "LabelMuestra1"
        Me.LabelMuestra1.Size = New System.Drawing.Size(232, 31)
        Me.LabelMuestra1.TabIndex = 100
        Me.LabelMuestra1.Text = "Clave De Acceso"
        Me.LabelMuestra1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBoxPwdGeneral
        '
        Me.TextBoxPwdGeneral.BackColor = System.Drawing.Color.LightCoral
        Me.TextBoxPwdGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPwdGeneral.Location = New System.Drawing.Point(23, 59)
        Me.TextBoxPwdGeneral.MaxLength = 20
        Me.TextBoxPwdGeneral.Name = "TextBoxPwdGeneral"
        Me.TextBoxPwdGeneral.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPwdGeneral.Size = New System.Drawing.Size(232, 44)
        Me.TextBoxPwdGeneral.TabIndex = 101
        '
        'ButtonTecladoPwd
        '
        Me.ButtonTecladoPwd.BackgroundImage = CType(resources.GetObject("ButtonTecladoPwd.BackgroundImage"), System.Drawing.Image)
        Me.ButtonTecladoPwd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonTecladoPwd.Location = New System.Drawing.Point(128, 109)
        Me.ButtonTecladoPwd.Name = "ButtonTecladoPwd"
        Me.ButtonTecladoPwd.Size = New System.Drawing.Size(127, 57)
        Me.ButtonTecladoPwd.TabIndex = 210
        Me.ButtonTecladoPwd.UseVisualStyleBackColor = True
        '
        'ButtonPwdCls
        '
        Me.ButtonPwdCls.BackColor = System.Drawing.Color.Blue
        Me.ButtonPwdCls.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPwdCls.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonPwdCls.Location = New System.Drawing.Point(23, 109)
        Me.ButtonPwdCls.Name = "ButtonPwdCls"
        Me.ButtonPwdCls.Size = New System.Drawing.Size(99, 57)
        Me.ButtonPwdCls.TabIndex = 214
        Me.ButtonPwdCls.Text = "Clr"
        Me.ButtonPwdCls.UseVisualStyleBackColor = False
        '
        'TCONA405
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.MediumAquamarine
        Me.ClientSize = New System.Drawing.Size(584, 501)
        Me.Controls.Add(Me.ButtonPwdCls)
        Me.Controls.Add(Me.ButtonTecladoPwd)
        Me.Controls.Add(Me.TextBoxPwdGeneral)
        Me.Controls.Add(Me.LabelMuestra1)
        Me.Controls.Add(Me.ButtonMantVarios)
        Me.Controls.Add(Me.ButtonCABSALIR)
        Me.Controls.Add(Me.ButtonRefer)
        Me.Controls.Add(Me.ButtonZ)
        Me.Controls.Add(Me.ButtonX)
        Me.Name = "TCONA405"
        Me.Text = "TCONA405"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonX As Button
    Friend WithEvents ButtonZ As Button
    Friend WithEvents ButtonRefer As Button
    Friend WithEvents ButtonCABSALIR As Button
    Friend WithEvents ButtonMantVarios As Button
    Friend WithEvents LabelMuestra1 As Label
    Friend WithEvents TextBoxPwdGeneral As TextBox
    Friend WithEvents ButtonTecladoPwd As Button
    Friend WithEvents ButtonPwdCls As Button
End Class
