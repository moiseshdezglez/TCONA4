Public Class TCONA421
    Dim Mayus As Boolean = False
    Dim CtlFoco As Integer = 0
    Dim SwSimbolos As Boolean = False
    Private Sub TCONA421_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub PreparaGRIDs()
        '
        ' Define Algunas Propiedades de los GRDs
        '
        With GRIDPEDCLI
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

    Private Sub TCONA421_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        TextBoxCamareroPD.Text = MyFrm2.TextBoxCamarero.Text
        TextBoxNomCamareroPD.Text = MyFrm2.LabelNomCamarero.Text
        TextBoxNumSalaPD.Text = MyFrm2.TextBoxNumSala.Text
        TextBoxNumMesaPD.Text = MyFrm2.TextBoxNumMesa.Text
        PreparaGRIDs()
        LimpiaCajasTexto(1)
        '
        ' Hay Pedido asignado?
        '
        ButtonDesvincularPedido.Enabled = False
        ButtonBorrar.Enabled = False
        '
        If WMesacTlfPed.Trim.Length > 0 Then
            TextBoxPDTlf.Text = WMesacTlfPed
            SacadatosPEDCLI(WMesacTlfPed)
            ButtonDesvincularPedido.Enabled = True
        End If
        CtlFoco = 0 : TextBoxPDTlf.Focus()
        '
    End Sub

    Private Sub LimpiaCajasTexto(wopt As Integer)
        '
        If wopt = 1 Then
            TextBoxPDTlf.Text = ""
        End If
        TextBoxPDNombre.Text = ""
        TextBoxPDDirec.Text = ""
        TextBoxPDPobla.Text = ""
        TextBoxPDCP.Text = ""
        TextBoxPDeMail.Text = ""
        TextBoxPDObser.Text = ""
        CheckBoxEnvEmail.Checked = False
        CheckBoxEnvSMS.Checked = False
        ButtonEnvEmail.Enabled = False
        ButtonEnvSMS.Enabled = False
        ButtonBorrar.Enabled = False
        '
    End Sub

    Private Sub Button21Salir_Click(sender As Object, e As EventArgs) Handles Button21Salir.Click
        Me.Hide()
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        CtlFoco = 999
        With GRIDPEDCLI
            .Focus()
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

    Private Sub ButtonGRIDAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        CtlFoco = 999
        With GRIDPEDCLI
            .Focus()
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

    Private Sub ButtonLetrAMinMay_Click(sender As Object, e As EventArgs) Handles ButtonLetrAMinMay.Click
        '
        ' Tecla SHIFT
        ' Letras MAYUSCULAS / minisculas
        '
        Dim myFont As System.Drawing.Font
        '
        ' Swicht MAYUS. / MINUS.
        '
        Mayus = Not Mayus
        If Mayus Then
            myFont = New System.Drawing.Font("Wingdings", 14, FontStyle.Regular)
            ButtonLetrAMinMay.Font = myFont
        Else
            myFont = New System.Drawing.Font("Wingdings", 18, FontStyle.Bold)
            ButtonLetrAMinMay.Font = myFont
        End If
        '
        ' Establecer MAYUS. / MINUS.
        '
        For Each wControl In Me.PanelTecladoALFA.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 11) = "ButtonLetra" Then
                    With wControl
                        If Mayus Then
                            .Text = .Text.ToLower
                        Else
                            .Text = .Text.ToUpper
                        End If
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Function EvitarCaracteres() As Boolean
        '
        '   NO permito Determinados Caracteres en:
        '   0 - Num. TLF Pedido
        '
        EvitarCaracteres = False
        Select Case CtlFoco
            Case 0
                EvitarCaracteres = True
        End Select
        '
    End Function

    Private Sub TeclasAlfaNumericas_Click(sender As Object, e As EventArgs) _
        Handles ButtonLetraA.Click, ButtonLetraZ.Click, ButtonLetraY.Click, ButtonLetraX.Click,
        ButtonLetraW.Click, ButtonLetraV.Click, ButtonLetraU.Click, ButtonLetraT.Click, ButtonLetraS.Click,
        ButtonLetraR.Click, ButtonLetraQ.Click, ButtonLetraP.Click, ButtonLetraO.Click, ButtonLetraN.Click,
        ButtonLetraM.Click, ButtonLetraL.Click, ButtonLetraK.Click, ButtonLetraJ.Click, ButtonLetraI.Click,
        ButtonLetraH.Click, ButtonLetraG.Click, ButtonLetraF.Click, ButtonLetraEnie.Click,
        ButtonLetraE.Click, ButtonLetraD.Click, ButtonLetraC.Click, ButtonLetraB.Click
        '
        ' Gestion del Teclado AlfaNumérico
        '
        If EvitarCaracteres() = False Then
            MiraFoco(CType(sender, Button))
        End If
        '
    End Sub

    Private Sub TeclasNumericas_Click(sender As Object, e As EventArgs) _
        Handles ButtonCal7.Click, ButtonCalMENOS.Click, ButtonCalCOMA.Click, ButtonCal9.Click,
        ButtonCal8.Click, ButtonCal6.Click, ButtonCal5.Click, ButtonCal4.Click, ButtonCal3.Click,
        ButtonCal2.Click, ButtonCal1.Click, ButtonCal0.Click
        '
        ' Gestion del Teclado Numerico
        '
        MiraFoco(CType(sender, Button))
        '
    End Sub

    Private Sub TeclasSimbolos_Click(sender As Object, e As EventArgs) _
        Handles ButtonLetrAS1.Click, ButtonLetrAS9.Click, ButtonLetrAS8.Click,
        ButtonLetrAS7.Click, ButtonLetrAS6.Click, ButtonLetrAS5.Click,
        ButtonLetrAS4.Click, ButtonLetrAS3.Click, ButtonLetrAS25.Click,
        ButtonLetrAS24.Click, ButtonLetrAS23.Click, ButtonLetrAS22.Click,
        ButtonLetrAS21.Click, ButtonLetrAS20.Click, ButtonLetrAS2.Click,
        ButtonLetrAS19.Click, ButtonLetrAS18.Click, ButtonLetrAS17.Click,
        ButtonLetrAS16.Click, ButtonLetrAS15.Click, ButtonLetrAS14.Click,
        ButtonLetrAS13.Click, ButtonLetrAS12.Click, ButtonLetrAS11.Click,
        ButtonLetrAS10.Click
        '
        MiraFoco(CType(sender, Button))
        '
    End Sub

    Private Sub MiraFoco(wBtnCal As Button)
        '
        ' Determinamos el Visor que esta recibiendo el FOCO.
        ' Tratamiento especial para &, (&&)
        '
        If wBtnCal.Text = "&&" Then
            VisorTecladoALFA.Text &= "&"
        Else
            VisorTecladoALFA.Text &= wBtnCal.Text
        End If
        '
        Visor_A_Textos()
        '
    End Sub

    Private Sub Visor_A_Textos()
        '
        ' Envia el texto del Visor a la Caja de Textos 
        ' que tiene el foco.
        '
        Select Case CtlFoco
            Case 0
                TextBoxPDTlf.Text = VisorTecladoALFA.Text
            Case 1
                TextBoxPDNombre.Text = VisorTecladoALFA.Text
            Case 2
                TextBoxPDDirec.Text = VisorTecladoALFA.Text
            Case 3
                TextBoxPDCP.Text = VisorTecladoALFA.Text
            Case 4
                TextBoxPDPobla.Text = VisorTecladoALFA.Text
            Case 5
                TextBoxPDeMail.Text = VisorTecladoALFA.Text
            Case 6
                TextBoxPDObser.Text = VisorTecladoALFA.Text
        End Select
        '
    End Sub

    Private Sub ButtonLetrATras_Click(sender As Object, e As EventArgs) Handles ButtonLetrATras.Click
        '
        ' Quita Ultimo Carcter
        '
        Dim TextoTemp As String = ""
        With wrTecladoFlotante
            .CadenaVisor = VisorTecladoALFA.Text
            If .CadenaVisor.Length > 0 Then
                TextoTemp = .CadenaVisor
                .CadenaVisor = TextoTemp.Remove((.CadenaVisor.Length - 1), 1)
                VisorTecladoALFA.Text = .CadenaVisor
            End If
        End With
        Visor_A_Textos()
        '
    End Sub

    Private Sub ButtonLetrACLR_Click(sender As Object, e As EventArgs) Handles ButtonLetrACLR.Click
        VisorTecladoALFA.Text = ""
        Visor_A_Textos()
    End Sub

    Private Sub ButtonLetrASPACE_Click(sender As Object, e As EventArgs) Handles ButtonLetrASPACE.Click
        VisorTecladoALFA.Text &= " "
        Visor_A_Textos()
    End Sub

    Private Sub TextBoxPDTlf_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDTlf.LostFocus
        TextBoxPDTlf.BackColor = Color.White
    End Sub

    Private Sub TextBoxPDTlf_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxPDTlf.KeyDown
        '
        ' Teléfono del Cliente / Pedido a Domicilio
        '
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Down
                TextBoxPDNombre.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA421_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        CtlFoco = 0 : TextBoxPDTlf.Focus()
        '
    End Sub

    Private Sub ButtonLetrArroba_Click(sender As Object, e As EventArgs) Handles ButtonLetrArroba.Click
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "@"
            Visor_A_Textos()
        End If
    End Sub

    Private Sub ButtonLetrAMas_Click(sender As Object, e As EventArgs) Handles ButtonLetrAMas.Click
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "+"
            Visor_A_Textos()
        End If
    End Sub

    Private Sub ButtonLetrABarra_Click(sender As Object, e As EventArgs) Handles ButtonLetrABarra.Click
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "/"
            Visor_A_Textos()
        End If
    End Sub

    Private Sub ButtonLetrAPunto_Click(sender As Object, e As EventArgs) Handles ButtonLetrAPunto.Click
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "."
            Visor_A_Textos()
        End If
    End Sub

    Private Sub ButtonLetrASim_Click(sender As Object, e As EventArgs) Handles ButtonLetrASim.Click
        '
        ' Teclado de Simbolos
        '
        If EvitarCaracteres() = False Then
            SwSimbolos = Not SwSimbolos
            PanelTecladoSimbolos.Left = PanelTecladoALFA.Left
            Select Case SwSimbolos
                Case True
                    PanelTecladoSimbolos.Visible = True
                    ButtonLetrASim.BackColor = Color.Red
                Case False
                    PanelTecladoSimbolos.Visible = False
                    ButtonLetrASim.BackColor = Color.Salmon
            End Select
        End If
        '
    End Sub

    Private Sub ButtonCLS_Click(sender As Object, e As EventArgs) Handles ButtonCLS.Click
        '
        CtlFoco = 0
        LimpiaCajasTexto(1)
        GRIDPEDCLI.Rows.Clear()
        TextBoxPDTlf.Focus()
        '
    End Sub

    Private Sub TextBoxPDTlf_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPDTlf.TextChanged
        '
        ' Al entrar un Telefono, disparamos busqueda de coincidencias
        '   en la tabla de pedidos a domicilio...
        '
        If CtlFoco = 0 Then
            If TextBoxPDTlf.Text.Trim.Length > 0 Then
                CargaListaPedClie(TextBoxPDTlf.Text.Trim)
                '
                ' Si solo hay una línea en el grid, aceptamos la coincidencia
                '
                With GRIDPEDCLI
                    If .Rows.Count = 1 Then
                        .CurrentCell = .Rows(0).Cells(0)
                        TextBoxPDTlf.Text = .SelectedCells(0).Value.ToString
                    End If
                End With
                '
                If LeePEDCLIE(TextBoxPDTlf.Text.Trim) = True Then
                    SacadatosPEDCLI(TextBoxPDTlf.Text.Trim)
                Else
                    LimpiaCajasTexto(0)
                End If
            Else
                GRIDPEDCLI.Rows.Clear()
                LimpiaCajasTexto(0)
            End If
        End If
        '
    End Sub

    Private Sub MiraContenidoTXT()
        '
        ' Gestiona el contenido de la propiedad .TEXT de los
        '   controles TextBox
        '
        Select Case CtlFoco
            Case 0
                If TextBoxPDTlf.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDTlf.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 1
                If TextBoxPDNombre.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDNombre.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 2
                If TextBoxPDDirec.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDDirec.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 3
                If TextBoxPDCP.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDCP.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 4
                If TextBoxPDPobla.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDPobla.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 5
                If TextBoxPDeMail.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDeMail.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 6
                If TextBoxPDObser.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxPDObser.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
        End Select

        '
    End Sub

    Private Sub TextBoxPDTlf_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDTlf.GotFocus
        '
        CtlFoco = 0
        MiraContenidoTXT()
        '
        With TextBoxPDTlf
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxPDNombre_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDNombre.GotFocus
        '
        CtlFoco = 1
        MiraContenidoTXT()
        '
        With TextBoxPDNombre
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxPDDirec_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDDirec.GotFocus
        '
        CtlFoco = 2
        MiraContenidoTXT()
        '
        With TextBoxPDDirec
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxPDCP_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDCP.GotFocus
        '
        CtlFoco = 3
        MiraContenidoTXT()
        '
        With TextBoxPDCP
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxPDPobla_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDPobla.GotFocus
        '
        CtlFoco = 4
        MiraContenidoTXT()
        '
        With TextBoxPDPobla
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxPDeMail_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDeMail.GotFocus
        '
        CtlFoco = 5
        MiraContenidoTXT()
        '
        With TextBoxPDeMail
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxPDObser_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPDObser.GotFocus
        '
        CtlFoco = 6
        MiraContenidoTXT()
        '
        With TextBoxPDObser
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub ButtonLetrACR_Click(sender As Object, e As EventArgs) Handles ButtonLetrACR.Click
        '
        ' Tratamiento especial (CR).
        ' Limita CR sólo a la caja de textos Observaciones.
        '
        If CtlFoco = 6 Then
            VisorTecladoALFA.Text &= vbCrLf
        End If
        '
    End Sub

    Private Sub TextBoxPDNombre_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDNombre.LostFocus
        TextBoxPDNombre.BackColor = Color.White
    End Sub

    Private Sub TextBoxPDDirec_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDDirec.LostFocus
        TextBoxPDDirec.BackColor = Color.White
    End Sub

    Private Sub TextBoxPDCP_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDCP.LostFocus
        TextBoxPDCP.BackColor = Color.White
    End Sub

    Private Sub TextBoxPDPobla_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDPobla.LostFocus
        TextBoxPDPobla.BackColor = Color.White
    End Sub

    Private Sub TextBoxPDeMail_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDeMail.LostFocus
        TextBoxPDeMail.BackColor = Color.White
    End Sub

    Private Sub TextBoxPDObser_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPDObser.LostFocus
        TextBoxPDObser.BackColor = Color.White
    End Sub

    Private Sub ButtonActualiza_Click(sender As Object, e As EventArgs) Handles ButtonActualiza.Click
        '
        ' Graba los Datos de un pedido a Domicilio.
        '
        If TextBoxPDTlf.Text.Trim.Length > 0 Then
            MantenimientoPedidos(TextBoxPDTlf.Text.Trim)
            CargaListaPedClie(TextBoxPDTlf.Text.Trim)
        End If
        '
    End Sub

    Private Sub GRIDPEDCLI_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDPEDCLI.SelectionChanged
        '
        If CtlFoco = 0 Then
            Exit Sub
        End If
        With GRIDPEDCLI
            If .SelectedRows.Count > 0 Then
                SacadatosPEDCLI(.SelectedCells(0).Value.ToString.Trim)
            End If
        End With
        '
    End Sub

    Private Sub SacadatosPEDCLI(wTelefono As String)
        '
        ' Muestra en pantalla los datos del PEDIDO, si este existe
        '
        '
        ButtonBorrar.Enabled = False
        If LeePEDCLIE(wTelefono) = True Then
            With wrLeePEDCLIE
                TextBoxPDTlf.Text = wTelefono
                If ExisteRegistroMESAC(TextBoxNumMesaPD.Text.Trim, 3, wTelefono) = False Then
                    ButtonBorrar.Enabled = True
                End If
                TextBoxPDNombre.Text = .NOMBRE
                TextBoxPDDirec.Text = .DIRECCION
                TextBoxPDPobla.Text = .POBLACION
                TextBoxPDCP.Text = .CODPOSTAL.ToString
                TextBoxPDeMail.Text = .EMAIL
                TextBoxPDObser.Text = .OBSER
                If .EMAILSN = "True" Then
                    CheckBoxEnvEmail.Checked = True
                    ButtonEnvEmail.Enabled = True
                Else
                    CheckBoxEnvEmail.Checked = False
                    ButtonEnvEmail.Enabled = False
                End If
                If .SMSSN = "True" Then
                    CheckBoxEnvSMS.Checked = True
                    ButtonEnvSMS.Enabled = True
                Else
                    CheckBoxEnvSMS.Checked = False
                    ButtonEnvSMS.Enabled = False
                End If
            End With
        Else
            LimpiaCajasTexto(0)
        End If
        '
    End Sub

    Private Sub GRIDPEDCLI_Click(sender As Object, e As EventArgs) Handles GRIDPEDCLI.Click
        '
        CtlFoco = 999
        With GRIDPEDCLI
            .Focus()
            If .SelectedRows.Count > 0 Then
                SacadatosPEDCLI(.SelectedCells(0).Value.ToString.Trim)
            End If
        End With
        '
    End Sub

    Private Sub ButtonSelecciona_Click(sender As Object, e As EventArgs) Handles ButtonSelecciona.Click
        '
        ' Asigna el pedido seleccionado a la mesa actual.
        ' Antes graba los datos del pedido.
        '
        If TextBoxPDTlf.Text.Trim.Length > 0 Then
            MantenimientoPedidos(TextBoxPDTlf.Text.Trim)
            WMesacTlfPed = TextBoxPDTlf.Text.Trim
            ActualizaDatosMESAC(TextBoxNumMesaPD.Text.Trim, 4)
            MyFrm2.LblDatosPedidoDomi.Text = TextBoxPDTlf.Text.Trim & " - " & TextBoxPDNombre.Text.Trim
            Me.Hide()
        End If
        '
    End Sub

    Private Sub ButtonDesvincularPedido_Click(sender As Object, e As EventArgs) Handles ButtonDesvincularPedido.Click
        '
        ' Desvincular el pedido seleccionado de la mesa actual.
        '
        WMesacTlfPed = ""
        ActualizaDatosMESAC(TextBoxNumMesaPD.Text.Trim, 4)
        MyFrm2.LblDatosPedidoDomi.Text = ""
        Me.Hide()
        '
    End Sub

    Private Sub TextBoxPDNombre_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPDNombre.TextChanged

    End Sub

    Private Sub ButtonLetrAENTER_Click(sender As Object, e As EventArgs) Handles ButtonLetrAENTER.Click
        '
        ' Le damos utilidad a la tecla ENTER
        '
        Select Case CtlFoco
            Case 0
                TextBoxPDNombre.Focus()
            Case 1
                TextBoxPDDirec.Focus()
            Case 2
                TextBoxPDCP.Focus()
            Case 3
                TextBoxPDPobla.Focus()
            Case 4
                TextBoxPDeMail.Focus()
            Case 5
                TextBoxPDObser.Focus()
        End Select
        '
    End Sub

    Private Sub ButtonBorrar_Click(sender As Object, e As EventArgs) Handles ButtonBorrar.Click
        '
        ' Borrado de Registros Pedidos de Clientes.
        ' Paso (1), Si el TLF actual ya esta vinculado a una mesa
        '          no borrar!
        '
        'If WMesacTlfPed.Trim.Length > 0 And
        'WMesacTlfPed.Trim = TextBoxPDTlf.Text.Trim Then
        'msg = "Atención registro vinculado a una mesa." & vbCrLf
        'msg &= TextBoxPDTlf.Text.Trim & " " & TextBoxPDNombre.Text.Trim & vbCrLf
        'style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
        'title = "No se permite Borrar estos Datos."
        'response = MsgBox(msg, style, title)
        'Exit Sub
        'End If
        '
        '   ** Mantenimiento de integridad referencial **
        '
        ' Aqui se requiere una comprobacion adicional.
        ' Hay que realizar una consulta de este TLF
        '  en [MESAC], si hay coincidencia es que ya esta vinculado 
        '  a mesa actual u otra mesa y no es posible borrar!!!
        '
        ButtonBorrar.Enabled = False
        '
        If ExisteRegistroMESAC(TextBoxNumMesaPD.Text.Trim, 3, TextBoxPDTlf.Text.Trim) = True Then
            msg = "Atención registro vinculado a una mesa." & vbCrLf
            msg &= TextBoxPDTlf.Text.Trim & " " & TextBoxPDNombre.Text.Trim & vbCrLf
            style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            title = "No se permite Borrar estos Datos."
            response = MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        If TextBoxPDTlf.Text.Trim.Length > 0 Then
            msg = "¿Desea Borra este registro?" & vbCrLf
            msg& = TextBoxPDTlf.Text.Trim & " " & TextBoxPDNombre.Text.Trim & vbCrLf
            style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.YesNo
            title = "Borrar Datos Cliente Pedido."
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                BorraPedido(TextBoxPDTlf.Text.Trim)
                TextBoxPDTlf.Text = WMesacTlfPed.Trim
                CargaListaPedClie(TextBoxPDTlf.Text.Trim)
            End If
        End If
        '
    End Sub
End Class