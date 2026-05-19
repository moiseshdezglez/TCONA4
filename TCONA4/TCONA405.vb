Public Class TCONA405
    '
    Dim ResizeFRM As Boolean = False

    Private Sub TCONA405_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                AccionSalir()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub ButtonCABSALIR_Click(sender As Object, e As EventArgs) Handles ButtonCABSALIR.Click
        '
        AccionSalir()
        '
    End Sub

    Private Sub AccionSalir()
        '
        ' Cerrar Form X, Z, Etc...
        '
        LimpiaClave()
        Me.Hide()
        '
    End Sub

    Private Sub ButtonRefer_Click(sender As Object, e As EventArgs) Handles ButtonRefer.Click
        '
        TCONA406_Started = False
        MyFrm6.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonX_Click(sender As Object, e As EventArgs) Handles ButtonX.Click
        '
        ' Comprobacion Impresora Encendida, ON-LINE.
        ' Impresora Predeterminada.
        '
        If LeeTCONA4Cfg("General") = True Then
            '
            ' Si el MODELO de impresora no esta ON-LINE y 
            '  no hacemos "PREVIEW" pregunta.
            '
            If ImpresoraEstaONLINE(ObtenerImpresoraPredeterminada.Trim) = False And
               wrLeeTCONA4.Tcona4_COBVIEWPDSN = "False" Then
                title = "¿Impresora Apagada?"
                style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
                msg = "Por favor, compruebe su impresora." & vbCrLf
                msg &= "Si desea contiuar con el proceso," & vbCrLf
                msg &= "no se imprimirá el TICKET." & vbCrLf & vbCrLf
                msg &= "Se detectan " & wrProp_IMPRESORA.JobCountSinceLastReset & " trabajos Pendientes."
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        End If
        '
        ButtonX.Enabled = False
        ButtonZ.Enabled = False
        GeneraX_Z("X")
        '
    End Sub

    Private Sub ButtonZ_Click(sender As Object, e As EventArgs) Handles ButtonZ.Click
        '
        ' La Z será lanzada si:
        '   - No hay mesas OCUPADAS.
        '   - Hay MESAS OCUPADAS, pero Ref. Generales da permiso.
        '
        ' Comprobacion Impresora Encendida, ON-LINE.
        ' Impresora Predeterminada.
        '
        If LeeTCONA4Cfg("General") = True Then
            '
            ' Si el MODELO de impresora no esta ON-LINE y 
            '  no hacemos "PREVIEW" pregunta.
            '
            If ImpresoraEstaONLINE(ObtenerImpresoraPredeterminada.Trim) = False And
               wrLeeTCONA4.Tcona4_COBVIEWPDSN = "False" Then
                title = "¿Impresora Apagada?"
                style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
                msg = "Por favor, compruebe su impresora." & vbCrLf
                msg &= "Si desea contiuar con el proceso," & vbCrLf
                msg &= "no se imprimirá el TICKET." & vbCrLf & vbCrLf
                msg &= "Se detectan " & wrProp_IMPRESORA.JobCountSinceLastReset & " trabajos Pendientes."
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        End If
        '
        If MiraMesasOcupadas() = False Then
            ButtonX.Enabled = False
            ButtonZ.Enabled = False
            GeneraX_Z("Z")
        Else
            LeeTCONA4Cfg("General")
            If wrLeeTCONA4.Tcona4_ZETAMESASOCU = "True" Then
                ButtonX.Enabled = False
                ButtonZ.Enabled = False
                GeneraX_Z("Z")
                '
                ' Libera TODAS las mesas de esta CAJA
                ' Por Tanto SALA y MESA = "*" 
                '      (Aqui da igual el valor que les pase!)
                '
                ActualizaMesa_SALA1(wCaja, "*", "*", 2)
            Else
                msg = "Mesas Ocupadas. No es posible realizar la Z."
                style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                title = "Denegacion Permisos Z."   ' Define title.
                MsgBox(msg, style, title)
            End If
        End If
        '
    End Sub

    Private Sub ButtonMantVarios_Click(sender As Object, e As EventArgs) Handles ButtonMantVarios.Click
        '
        MyFrm10.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonTecladoPwd_Click(sender As Object, e As EventArgs) Handles ButtonTecladoPwd.Click
        '
        ' Llamada al Teclado Flotante.
        '
        With wrTecladoFlotante
            '
            ' 0=Numérico, 1=Alfabético, 2=Alfanumérico
            '
            .Tipo = 2
            '
            ' 0=No, 1=Pide Pwd
            '
            .PidePwd = 1
            '
            ' Mensaje al usuario
            '
            .MensaUsuario = "Por favor digite una clave."
            '
            ' Tamaño y Posicion del FORM que llama al teclado.
            '
            .Top = Me.Top
            .Left = Me.Left
            .width = Me.Width
            .height = Me.Height
            '
            ' Opcionalmente un Color de fondo
            '
            .BackColor = Color.DarkSeaGreen
            '
            ' Caracteres Maximos Permitidos.
            '
            .MaxChar = 20
            '
            ' Codigo que indica el control al que retornar el texto.
            ' Se controla en la aplicación ...
            '
            '.CodigoRetorno 
            '
        End With
        MyFrm15.ShowDialog(Me)
        '
    End Sub

    Private Sub TextBoxPwdGeneral_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPwdGeneral.TextChanged
        '
        ' Recoge los caracteres de la Password, Valida si es correcta o NO 
        '   y otorga NIVELES de Acceso.
        '
        If TextBoxPwdGeneral.Text.Trim.Length > 0 Then
            If compruebaPWDS(TextBoxPwdGeneral.Text.Trim) = True Then
                Select Case wrLeeCODNOM.NIVELACCESO
                    Case 5
                        '
                        ' Entidad.: "Programadores/Trivalle"
                        ' Clave(s) Programadores, NO dependen de la TABLA [CLAVES]
                        ' ( Se tratan como NIVEL 5, con acceso=TODO )
                        ' Permitirá Acceso a Mant. de Claves.
                        '
                        If TextBoxPwdGeneral.Text.Trim = PassTRIVALLE(0) Or
                           TextBoxPwdGeneral.Text.Trim = PassTRIVALLE(1) Then
                            '
                            ' X, Z, Mant. Varios, Ref. Generales (TODO)
                            '
                            TextBoxPwdGeneral.BackColor = Color.PaleGreen
                            ButtonX.Enabled = True
                            ButtonZ.Enabled = True
                            ButtonRefer.Enabled = True
                            ButtonMantVarios.Enabled = True
                            ButtonX.ForeColor = Color.Blue
                            ButtonZ.ForeColor = Color.Blue
                            ButtonRefer.ForeColor = Color.Blue
                            ButtonMantVarios.ForeColor = Color.Blue
                            '
                            ' Mant. Claves, Programadores !!!
                            '
                            MyFrm10.ButtonMantClaves.Enabled = True
                            MyFrm10.ButtonMantClaves.ForeColor = Color.Blue
                        End If
                        '
                        ' Entidad.: " SUPER ", NIVEL Acceso 5. 
                        ' Depende de Tabla [CLAVES]
                        ' [ Normalmente Acceso a TODO ]
                        '
                        If TextBoxPwdGeneral.Text.Trim = wrCLAVES.CLAVE.Trim Then
                            TextBoxPwdGeneral.BackColor = Color.PaleGreen
                            '
                            ' X, Z, Mant. Varios, Ref. Generales:
                            ' Dado que podemos parametrizar los accesos, 
                            '    los comprobamos...
                            '
                            With wrCLAVES
                                If .ACCESOX = "True" Then
                                    ButtonX.Enabled = True
                                    ButtonX.ForeColor = Color.Blue
                                End If
                                If .ACCESOZ = "True" Then
                                    ButtonZ.Enabled = True
                                    ButtonZ.ForeColor = Color.Blue
                                End If
                                If .ACCESOAPPS = "True" Then
                                    ButtonMantVarios.Enabled = True
                                    ButtonMantVarios.ForeColor = Color.Blue
                                    '
                                    ' Mant. Claves, "SUPER" !!!
                                    '
                                    MyFrm10.ButtonMantClaves.Enabled = True
                                    MyFrm10.ButtonMantClaves.ForeColor = Color.Blue
                                End If
                                '
                                ' Si se indica TODO o hay Nro. pestañas
                                '  se dará acceso a dichas pestañas.
                                ' (Esto se gestionará al Accder a Ref. Generales.)
                                '
                                If .ACCESOREFGEN.Trim.Length > 0 Then
                                    ButtonRefer.Enabled = True
                                    ButtonRefer.ForeColor = Color.Blue
                                End If
                            End With
                        End If
                        '
                    Case 4
                        '
                        ' Entidad.: " EL JEFE ", NIVEL Acceso 4. 
                        ' Depende de Tabla [CLAVES]
                        '
                        '
                        ' X, Z, Mant. Varios, Ref. Generales:
                        ' Dado que podemos parametrizar los accesos, 
                        '    los comprobamos...
                        '
                        TextBoxPwdGeneral.BackColor = Color.PaleGreen
                        With wrCLAVES
                            If .ACCESOX = "True" Then
                                ButtonX.Enabled = True
                                ButtonX.ForeColor = Color.Blue
                            End If
                            If .ACCESOZ = "True" Then
                                ButtonZ.Enabled = True
                                ButtonZ.ForeColor = Color.Blue
                            End If
                            If .ACCESOAPPS = "True" Then
                                ButtonMantVarios.Enabled = True
                                ButtonMantVarios.ForeColor = Color.Blue
                                '
                                ' Mant. Claves, "EL JEFE" !!!
                                '
                                MyFrm10.ButtonMantClaves.Enabled = True
                                MyFrm10.ButtonMantClaves.ForeColor = Color.Blue

                            End If
                            '
                            ' Si se indica TODO o hay Nro. pestañas
                            '  se dará acceso a dichas pestañas.
                            ' (Esto se gestionará al Accder a Ref. Generales.)
                            '
                            If .ACCESOREFGEN.Trim.Length > 0 And .ACCESOREFGEN.Trim <> "NADA" Then
                                ButtonRefer.Enabled = True
                                ButtonRefer.ForeColor = Color.Blue
                            End If
                        End With
                    Case 3
                        '
                        ' Entidad.: " ENCARGADO ", NIVEL Acceso 3. 
                        ' Depende de Tabla [CLAVES]
                        '
                        '
                        ' X, Z, Mant. Varios, Ref. Generales:
                        ' Dado que podemos parametrizar los accesos, 
                        '    los comprobamos...
                        '
                        TextBoxPwdGeneral.BackColor = Color.PaleGreen
                        With wrCLAVES
                            If .ACCESOX = "True" Then
                                ButtonX.Enabled = True
                                ButtonX.ForeColor = Color.Blue
                            End If
                            If .ACCESOZ = "True" Then
                                ButtonZ.Enabled = True
                                ButtonZ.ForeColor = Color.Blue
                            End If
                            If .ACCESOAPPS = "True" Then
                                ButtonMantVarios.Enabled = True
                                ButtonMantVarios.ForeColor = Color.Blue
                            End If
                            '
                            ' Si se indica TODO o hay Nro. pestañas
                            '  se dará acceso a dichas pestañas.
                            ' (Esto se gestionará al Accder a Ref. Generales.)
                            '
                            If .ACCESOREFGEN.Trim.Length > 0 And .ACCESOREFGEN.Trim <> "NADA" Then
                                ButtonRefer.Enabled = True
                                ButtonRefer.ForeColor = Color.Blue
                            End If
                        End With
                    Case Else
                        '
                        ' Resto de NIVELES inferiores a NIVEL 3=(ENCARGADO).
                        ' Estos niveles de momento se entienden sin ACCESO
                        '    para X, Z, Mant. Varios, Ref. Generales:
                        '
                        TextBoxPwdGeneral.BackColor = Color.LightCoral
                        ButtonX.Enabled = False
                        ButtonZ.Enabled = False
                        ButtonRefer.Enabled = False
                        ButtonMantVarios.Enabled = False
                        MyFrm10.ButtonMantClaves.Enabled = False
                        '
                        ButtonX.ForeColor = SystemColors.ControlText
                        ButtonZ.ForeColor = SystemColors.ControlText
                        ButtonRefer.ForeColor = SystemColors.ControlText
                        ButtonMantVarios.ForeColor = SystemColors.ControlText
                        MyFrm10.ButtonMantClaves.ForeColor = SystemColors.ControlText
                End Select
            Else
                '
                ' Passwords inválidas, Niveles de Acceso no comprobados
                '  ningún tipo de acceso.
                '
                TextBoxPwdGeneral.BackColor = Color.LightCoral
                ButtonX.Enabled = False
                ButtonZ.Enabled = False
                ButtonRefer.Enabled = False
                ButtonMantVarios.Enabled = False
                MyFrm10.ButtonMantClaves.Enabled = False
                '
                ButtonX.ForeColor = SystemColors.ControlText
                ButtonZ.ForeColor = SystemColors.ControlText
                ButtonRefer.ForeColor = SystemColors.ControlText
                ButtonMantVarios.ForeColor = SystemColors.ControlText
                MyFrm10.ButtonMantClaves.ForeColor = SystemColors.ControlText
            End If
        Else
            '
            ' Passwords inválidas, Niveles de Acceso no comprobados
            '  ningún tipo de acceso.
            '
            TextBoxPwdGeneral.BackColor = Color.LightCoral
            ButtonX.Enabled = False
            ButtonZ.Enabled = False
            ButtonRefer.Enabled = False
            ButtonMantVarios.Enabled = False
            MyFrm10.ButtonMantClaves.Enabled = False
            '
            ButtonX.ForeColor = SystemColors.ControlText
            ButtonZ.ForeColor = SystemColors.ControlText
            ButtonRefer.ForeColor = SystemColors.ControlText
            ButtonMantVarios.ForeColor = SystemColors.ControlText
            MyFrm10.ButtonMantClaves.ForeColor = SystemColors.ControlText
        End If
        '
    End Sub

    Private Sub ButtonPwdCls_Click(sender As Object, e As EventArgs) Handles ButtonPwdCls.Click
        '
        LimpiaClave()
        '
    End Sub

    Private Sub LimpiaClave()
        '
        TextBoxPwdGeneral.Text = ""
        TextBoxPwdGeneral.BackColor = Color.LightCoral
        '
        ButtonX.Enabled = False
        ButtonZ.Enabled = False
        ButtonRefer.Enabled = False
        ButtonMantVarios.Enabled = False
        MyFrm10.ButtonMantClaves.Enabled = False
        '
        ButtonX.ForeColor = SystemColors.ControlText
        ButtonZ.ForeColor = SystemColors.ControlText
        ButtonRefer.ForeColor = SystemColors.ControlText
        ButtonMantVarios.ForeColor = SystemColors.ControlText
        MyFrm10.ButtonMantClaves.ForeColor = SystemColors.ControlText
        '
    End Sub

    Private Sub TCONA405_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        With wrTecladoFlotante
            If TCONA405_Started = False Then
                TCONA405_Started = True
                '
                CargaListaTKFavoritos()
                '
                ' Acciones al Entrar y Activarse.
                '
                TextBoxPwdGeneral.Text = ""
                TextBoxPwdGeneral.Focus()
                .CodigoRetorno = 1
                .MensaUsuario = ""
            Else
                '
                ' Acciones Cuando se mantiene Visible y se re-activa.
                ' Recogemos Texto desde el teclado...
                '
                Select Case .CodigoRetorno
                    Case 1
                        TextBoxPwdGeneral.Text = .CadenaVisor
                End Select
            End If
        End With
        '
    End Sub

End Class