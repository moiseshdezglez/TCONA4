Public Class TCONA420

    Dim wPosTeclado As Integer = 0
    Private Sub TCONA420_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Button20Salir_Click(sender As Object, e As EventArgs) Handles Button20Salir.Click
        Me.Hide()
    End Sub

    Private Sub ButtonGRIDCLArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCLArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDCLRFM
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 > 0 Then
                    CursorGRID1 -= 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(0)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonGRIDCLAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCLAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDCLRFM
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                Dim GrNumRows As Integer = .Rows.Count - 1
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 < GrNumRows Then
                    CursorGRID1 += 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(0)
                End If
            End If
        End With
        '
    End Sub

    Private Sub TCONA420_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        PreparaGrids()
        CargaListaClaves(999999)
        CargaVendedores(4)
        '
    End Sub

    Private Sub PreparaGrids()
        '
        With GRIDCLRFM
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            '
            ' Colores
            '
            .BackgroundColor = SystemColors.Info
            .DefaultCellStyle.BackColor = .BackgroundColor
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            '
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .RowHeadersWidth = 21
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
        '
        With GRIDLV
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            '
            ' Colores
            '
            .BackgroundColor = SystemColors.Info
            .DefaultCellStyle.BackColor = .BackgroundColor
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            '
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .RowHeadersWidth = 21
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
        '
    End Sub

    Private Sub ButtonGRIDLVArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDLVArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDLV
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 > 0 Then
                    CursorGRID1 -= 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(0)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonGRIDLVAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDLVAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDLV
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                Dim GrNumRows As Integer = .Rows.Count - 1
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 < GrNumRows Then
                    CursorGRID1 += 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(0)
                End If
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCnivel_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCnivel.GotFocus
        '
        wPosTeclado = 0
        ButtonTecladoPwd.Left = TextBoxCnivel.Left
        '
    End Sub

    Private Sub TextBoxCDescrip_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCDescrip.GotFocus
        '
        wPosTeclado = 1
        ButtonTecladoPwd.Left = TextBoxCDescrip.Left
        '
    End Sub

    Private Sub TextBoxCclave_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCclave.GotFocus
        '
        wPosTeclado = 2
        ButtonTecladoPwd.Left = TextBoxCclave.Left
        '
    End Sub

    Private Sub GRIDCLRFM_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDCLRFM.SelectionChanged
        '
        With GRIDCLRFM
            If .SelectedRows.Count > 0 Then
                SacaDatosClave()
            End If
        End With
        '
    End Sub

    Private Sub SacaDatosClave()
        '
        With GRIDCLRFM
            TextBoxCnivel.Text = .SelectedCells(0).Value.ToString
            TextBoxCnivel1.Text = .SelectedCells(0).Value.ToString
            TextBoxCDescrip.Text = .SelectedCells(1).Value.ToString
            TextBoxCclave.Text = .SelectedCells(2).Value.ToString
            ComboBoxCrefGen.Text = .SelectedCells(9).Value.ToString
            '
            If .SelectedCells(3).Value.ToString = "True" Then
                CheckBoxCX.Checked = True
            Else
                CheckBoxCX.Checked = False
            End If
            If .SelectedCells(4).Value.ToString = "True" Then
                CheckBoxCZ.Checked = True
            Else
                CheckBoxCZ.Checked = False
            End If
            If .SelectedCells(5).Value.ToString = "True" Then
                CheckBoxCAPPs.Checked = True
            Else
                CheckBoxCAPPs.Checked = False
            End If
            If .SelectedCells(6).Value.ToString = "True" Then
                CheckBoxCMenos.Checked = True
            Else
                CheckBoxCMenos.Checked = False
            End If
            If .SelectedCells(7).Value.ToString = "True" Then
                CheckBoxCPrecio.Checked = True
            Else
                CheckBoxCPrecio.Checked = False
            End If
            If .SelectedCells(8).Value.ToString = "True" Then
                CheckBoxCTarifa.Checked = True
            Else
                CheckBoxCTarifa.Checked = False
            End If
            '
        End With
        '
    End Sub

    Private Sub GRIDCLRFM_Click(sender As Object, e As EventArgs) Handles GRIDCLRFM.Click
        '
        With GRIDCLRFM
            If .SelectedRows.Count > 0 Then
                SacaDatosClave()
            End If
        End With
        '
    End Sub

    Private Sub ButtonModNivel_Click(sender As Object, e As EventArgs) Handles ButtonModNivel.Click
        '
        ' Se quiere modificar el NIVEL de ACCESO de un Vendedor determinado.
        '
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        '
        With GRIDLV
            If .SelectedRows.Count > 0 Then
                '
                msg = "¿Desea Moficar el NIVEL de Acceso del vendedor" & vbCrLf
                msg &= .SelectedCells(1).Value.ToString & "?"
                msg &= vbCrLf & vbCrLf
                msg &= "NIVEL ACTUAL.: " & .SelectedCells(2).Value.ToString
                msg &= vbCrLf & vbCrLf
                msg &= "NIVEL NUEVO.: " & TextBoxCnivel1.Text.Trim
                '
                style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
                title = "Cambio de NIVEL en vendedor."
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.Yes Then
                    ActualizaVendedor(CInt(.SelectedCells(0).Value.ToString))
                    CargaVendedores(4)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonDuplica_Click(sender As Object, e As EventArgs) Handles ButtonDuplica.Click
        '
        ' Genera un registro NUEVO a partir de uno seleccionado.
        ' El nuevo NIVEL será el Utimo hallado + 1
        '
        With GRIDCLRFM
            If .Rows.Count = 0 Or .SelectedRows.Count = 0 Then
                Exit Sub
            End If
        End With
        '
        Dim UltimaClave As Integer = 0
        Dim NuevaClave As Integer = 0
        UltimaClave = CInt(CargaListaClaves(999999999, 98))
        NuevaClave = UltimaClave + 1
        '
        Dim ChkX As Integer = 0 : Dim ChkZ As Integer = 0
        Dim ChkApp As Integer = 0 : Dim ChkBm As Integer = 0
        Dim ChkBp As Integer = 0 : Dim ChkBt As Integer = 0
        '
        If CheckBoxCX.Checked = True Then
            ChkX = 1
        End If
        If CheckBoxCZ.Checked = True Then
            ChkZ = 1
        End If
        If CheckBoxCAPPs.Checked = True Then
            ChkApp = 1
        End If
        If CheckBoxCMenos.Checked = True Then
            ChkBm = 1
        End If
        If CheckBoxCPrecio.Checked = True Then
            ChkBp = 1
        End If
        If CheckBoxCTarifa.Checked = True Then
            ChkBt = 1
        End If
        '
        TEMP = ""
        TEMP &= "¿Desea crear nuevo NIVEL " & NuevaClave.ToString & vbCrLf
        TEMP &= "Con estos valores.:? " & vbCrLf & vbCrLf
        TEMP &= "Descripcion .....: " & TextBoxCDescrip.Text.Trim & vbCrLf
        TEMP &= "Clave ...........: " & TextBoxCclave.Text.Trim & vbCrLf
        TEMP &= "Acceso Ref.Gen. .: " & ComboBoxCrefGen.Text.Trim & vbCrLf
        TEMP &= "Acceso X ........: " & ChkX.ToString & vbCrLf
        TEMP &= "Acceso Z ........: " & ChkZ.ToString & vbCrLf
        TEMP &= "Acceso Apps .....: " & ChkApp.ToString & vbCrLf
        TEMP &= "Acceso Btn [ - ] : " & ChkBm.ToString & vbCrLf
        TEMP &= "Acceso Btn [PVP] : " & ChkBp.ToString & vbCrLf
        TEMP &= "Acceso Btn [TAR] : " & ChkBt.ToString & vbCrLf
        msg = TEMP
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        title = "Creación Nuevo Nivel [ " & NuevaClave.ToString & " ]"
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            MantenimientoClaves(NuevaClave)
            CargaListaClaves(999999)
        End If
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
            .PidePwd = 0
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
            Select Case wPosTeclado
                Case 0
                    .CodigoRetorno = 1
                    '
                    ' Mensaje al usuario
                    '
                    .MensaUsuario = "Por favor digite Número de NIVEL."
                Case 1
                    .CodigoRetorno = 2
                    '
                    ' Mensaje al usuario
                    '
                    .MensaUsuario = "Por favor digite una Descripción."
                Case 2
                    .CodigoRetorno = 3
                    '
                    ' Mensaje al usuario
                    '
                    .MensaUsuario = "Por favor digite una Contraseña."
            End Select
            '
        End With
        MyFrm15.ShowDialog(Me)
        '
    End Sub

    Private Sub TCONA420_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        With wrTecladoFlotante
            If TCONA420_Started = False Then
                TCONA420_Started = True
                '
                CargaListaTKFavoritos()
                '
                ' Acciones al Entrar y Activarse.
                '
                .CodigoRetorno = 0
                .MensaUsuario = ""
                wPosTeclado = 1
                ButtonTecladoPwd.Left = TextBoxCDescrip.Left
            Else
                '
                ' Acciones Cuando se mantiene Visible y se re-activa.
                ' Recogemos Texto desde el teclado...
                '
                Select Case .CodigoRetorno
                   'Case 1 ' Numero de Nivel ...
                    Case 2
                        If .CadenaVisor.Length > 0 Then
                            TextBoxCDescrip.Text = .CadenaVisor
                        End If
                    Case 3
                        If .CadenaVisor.Length > 0 Then
                            TextBoxCclave.Text = .CadenaVisor
                        End If
                End Select
                .CodigoRetorno = 0
                .MensaUsuario = ""
            End If
        End With
        '
    End Sub

    Private Sub ButtonAceptaClave_Click(sender As Object, e As EventArgs) Handles ButtonAceptaClave.Click
        '
        ' Desde este Botón Actualizamos NIVELES.
        '
        ' Validacion Campo ACCESOREFGEN
        '
        With GRIDCLRFM
            If .Rows.Count = 0 Or .SelectedRows.Count = 0 Then
                Exit Sub
            End If
        End With
        '
        If ComboBoxCrefGen.Text.Trim.Length > 0 Then
            Select Case ComboBoxCrefGen.Text.Trim
                Case "TODAS", "0/1", "NADA"
                    ' Seguir...
                Case Else
                    msg = "Por Favor elija un Valor Correcto." & vbCrLf
                    msg &= " Valores Permitidos:" & vbCrLf
                    msg &= "   TODAS" & vbCrLf
                    msg &= "   0/1  " & vbCrLf
                    msg &= "   NADA " & vbCrLf
                    style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
                    title = "Error Acceso Ref. Gen. Incorrecto."
                    MsgBox(msg, style, title)
                    Exit Sub
            End Select
        Else
            msg = "Por Favor elija un Valor Correcto." & vbCrLf
            msg &= " Valores Permitidos:" & vbCrLf
            msg &= "   TODO" & vbCrLf
            msg &= "   0/1 " & vbCrLf
            msg &= "   NADA" & vbCrLf
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error.: Acceso Ref. Gen. Incorrecto."
            MsgBox(msg, style, title)
            ComboBoxCrefGen.Focus()
            Exit Sub
        End If
        '
        ' Validacion Otros Valores
        '
        If TextBoxCDescrip.Text.Trim.Length = 0 Then
            msg = "Por favor, defina una descripción."
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error.: Descripcion requerida."
            MsgBox(msg, style, title)
            TextBoxCDescrip.Focus()
            Exit Sub
        End If
        If TextBoxCclave.Text.Trim.Length = 0 Then
            msg = "Por favor, defina una Clave."
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error.: Clave requerida."
            MsgBox(msg, style, title)
            TextBoxCclave.Focus()
            Exit Sub
        End If
        '
        Dim ChkX As Integer = 0 : Dim ChkZ As Integer = 0
        Dim ChkApp As Integer = 0 : Dim ChkBm As Integer = 0
        Dim ChkBp As Integer = 0 : Dim ChkBt As Integer = 0
        '
        If CheckBoxCX.Checked = True Then
            ChkX = 1
        End If
        If CheckBoxCZ.Checked = True Then
            ChkZ = 1
        End If
        If CheckBoxCAPPs.Checked = True Then
            ChkApp = 1
        End If
        If CheckBoxCMenos.Checked = True Then
            ChkBm = 1
        End If
        If CheckBoxCPrecio.Checked = True Then
            ChkBp = 1
        End If
        If CheckBoxCTarifa.Checked = True Then
            ChkBt = 1
        End If
        '
        TEMP = ""
        TEMP &= "¿Desea Actualizar el NIVEL " & TextBoxCnivel.Text.Trim & vbCrLf
        TEMP &= "a estos valores .:? " & vbCrLf & vbCrLf
        TEMP &= "Descripcion .....: " & TextBoxCDescrip.Text.Trim & vbCrLf
        TEMP &= "Clave ...........: " & TextBoxCclave.Text.Trim & vbCrLf
        TEMP &= "Acceso Ref.Gen. .: " & ComboBoxCrefGen.Text.Trim & vbCrLf
        TEMP &= "Acceso X ........: " & ChkX.ToString & vbCrLf
        TEMP &= "Acceso Z ........: " & ChkZ.ToString & vbCrLf
        TEMP &= "Acceso Apps .....: " & ChkApp.ToString & vbCrLf
        TEMP &= "Acceso Btn [ - ] : " & ChkBm.ToString & vbCrLf
        TEMP &= "Acceso Btn [PVP] : " & ChkBp.ToString & vbCrLf
        TEMP &= "Acceso Btn [TAR] : " & ChkBt.ToString & vbCrLf
        msg = TEMP
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        title = "Actualización Datos. NIVEL [ " & TextBoxCnivel.Text.Trim & " ]"
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            MantenimientoClaves(CInt(TextBoxCnivel.Text.Trim))
            CargaListaClaves(999999)
        End If
    End Sub

    Private Sub ButtonEliminaClave_Click(sender As Object, e As EventArgs) Handles ButtonEliminaClave.Click
        '
        ' Borrado de niveles > 5
        '
        With GRIDCLRFM
            If .Rows.Count > 0 And .SelectedRows.Count > 0 Then
                '
                If CInt(TextBoxCnivel.Text.Trim) > 5 Then
                    '
                    Dim ChkX As Integer = 0 : Dim ChkZ As Integer = 0
                    Dim ChkApp As Integer = 0 : Dim ChkBm As Integer = 0
                    Dim ChkBp As Integer = 0 : Dim ChkBt As Integer = 0
                    '
                    If CheckBoxCX.Checked = True Then
                        ChkX = 1
                    End If
                    If CheckBoxCZ.Checked = True Then
                        ChkZ = 1
                    End If
                    If CheckBoxCAPPs.Checked = True Then
                        ChkApp = 1
                    End If
                    If CheckBoxCMenos.Checked = True Then
                        ChkBm = 1
                    End If
                    If CheckBoxCPrecio.Checked = True Then
                        ChkBp = 1
                    End If
                    If CheckBoxCTarifa.Checked = True Then
                        ChkBt = 1
                    End If
                    '
                    TEMP = ""
                    TEMP &= "¿Desea BORRAR el NIVEL " & TextBoxCnivel.Text.Trim & vbCrLf
                    TEMP &= "con estos datos? " & vbCrLf & vbCrLf
                    TEMP &= "Descripcion .....: " & TextBoxCDescrip.Text.Trim & vbCrLf
                    TEMP &= "Clave ...........: " & TextBoxCclave.Text.Trim & vbCrLf
                    TEMP &= "Acceso Ref.Gen. .: " & ComboBoxCrefGen.Text.Trim & vbCrLf
                    TEMP &= "Acceso X ........: " & ChkX.ToString & vbCrLf
                    TEMP &= "Acceso Z ........: " & ChkZ.ToString & vbCrLf
                    TEMP &= "Acceso Apps .....: " & ChkApp.ToString & vbCrLf
                    TEMP &= "Acceso Btn [ - ] : " & ChkBm.ToString & vbCrLf
                    TEMP &= "Acceso Btn [PVP] : " & ChkBp.ToString & vbCrLf
                    TEMP &= "Acceso Btn [TAR] : " & ChkBt.ToString & vbCrLf
                    msg = TEMP
                    style = MsgBoxStyle.DefaultButton2 Or
                            MsgBoxStyle.Critical Or
                            MsgBoxStyle.YesNo
                    title = "BORRADO !!!. NIVEL [ " & TextBoxCnivel.Text.Trim & " ]"
                    response = MsgBox(msg, style, title)
                    If response = MsgBoxResult.Yes Then
                        BorrarClave(CInt(TextBoxCnivel.Text.Trim))
                        CargaListaClaves(999999)
                    End If
                Else
                    msg = "Sólo se pemite BORRAR niveles superiores a 5."
                    style = MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly
                    title = "Acción de Borrado no permitido."
                    MsgBox(msg, style, title)
                End If
                '
            End If
        End With
        '
    End Sub
End Class