Public Class TCONA422

    Dim Mayus As Boolean = False
    Dim CtlFoco As Integer = 0
    Dim OpcBusca As Integer = 0
    Dim SwSimbolos As Boolean = False

    Private Sub TCONA422_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
        With GRIDCLIFAC
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

    Private Sub LimpiaCajasTexto(Optional wopt As Integer = 0)
        '
        ' Nota, normalmente limpiamos estas ajas de texto
        '       excepto cuando wopt=1
        '
        If wopt = 0 Then
            TextBoxCLCodCli.Text = ""
            TextBoxCLNIFCIF.Text = ""
        End If
        '
        TextBoxCLLocNom.Text = ""
        TextBoxNCInfo.Text = ""
        TextBoxCLNombre.Text = ""
        TextBoxCLDirec.Text = ""
        TextBoxCLPobla.Text = ""
        TextBoxCLCP.Text = ""
        TextBoxCLeMail.Text = ""
        TextBoxCLTLF1.Text = ""
        TextBoxCLTLF2.Text = ""
        TextBoxCLDto.Text = ""
        '
    End Sub

    Private Sub TCONA422_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        OpcBusca = 0
        LabelInfoEmp.Text = "Empresa Actual (" & wEmpresa.ToString.Trim & ")"
        TextBoxCamareroCL.Text = MyFrm2.TextBoxCamarero.Text
        TextBoxNomCamareroCL.Text = MyFrm2.LabelNomCamarero.Text
        TextBoxNumSalaCL.Text = MyFrm2.TextBoxNumSala.Text
        TextBoxNumMesaCL.Text = MyFrm2.TextBoxNumMesa.Text
        PreparaGRIDs()
        LimpiaCajasTexto(0)
        '
        ' Hay asignado Cliente CONTADO?
        '
        ButtonDesvincularCliente.Enabled = False
        If WMesacNIFCIF.Trim.Length > 0 Then
            TextBoxCLNIFCIF.Text = WMesacNIFCIF
            SacadatosCLIMCO(WMesacNIFCIF, 1)
            CtlFoco = 1 : TextBoxCLNIFCIF.Focus()
            'ButtonDesvincularCliente.Enabled = True
        End If
        '
        ' Hay asignado Cliente CREDITO?
        '
        If wCliente > 0 And wCliente <> 430000000 Then
            TextBoxCLCodCli.Text = wCliente.ToString
            SacadatosCLIMCO(wCliente.ToString, 0)
            CtlFoco = 0 : TextBoxCLCodCli.Focus()
            'ButtonDesvincularCliente.Enabled = True
        End If
        '
    End Sub

    Private Sub Button21Salir_Click(sender As Object, e As EventArgs) Handles Button21Salir.Click
        Me.Hide()
    End Sub

    Private Sub TextBoxCLCodCli_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCLCodCli.TextChanged
        '
        ' Al entrar un Código de Cliente CREDITO, 
        '   disparamos busqueda de coincidencias
        '   en la tabla MCO.
        '
        If CtlFoco = 0 Then
            If TextBoxCLCodCli.Text.Trim.Length > 0 Then
                TextBoxCLNIFCIF.Text = ""
                CargaListaClieMCO(TextBoxCLCodCli.Text.Trim, 0)
                '
                ' Si solo hay una línea en el grid, aceptamos la coincidencia
                '
                With GRIDCLIFAC
                    If .Rows.Count = 1 Then
                        .CurrentCell = .Rows(0).Cells(0)
                        TextBoxCLCodCli.Text = .SelectedCells(0).Value.ToString
                    End If
                End With
                '
                If LeeClienteMCO(CInt(TextBoxCLCodCli.Text.Trim)) = True Then
                    SacadatosCLIMCO(TextBoxCLCodCli.Text.Trim, 0)
                Else
                    LimpiaCajasTexto(1)
                End If
            Else
                GRIDCLIFAC.Rows.Clear()
                LimpiaCajasTexto(0)
            End If
        End If
        '
    End Sub

    Private Sub TextBoxCLLocNom_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCLLocNom.TextChanged
        '
        ' Al entrar un Nombre de Cliente CREDITO / CONTADO, 
        '   disparamos busqueda por coincidencia.
        '
        If TextBoxCLLocNom.Text.Trim.Length > 0 Then
            Select Case OpcBusca
                Case 0
                    '
                    ' Crédito
                    '
                    CargaListaClieMCO(TextBoxCLLocNom.Text.Trim, 1)
                Case 1
                    '
                    ' Contado
                    '
                    CargaListaClieCONTA(TextBoxCLLocNom.Text.Trim, 1)
            End Select
        End If
        '
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        CtlFoco = 999
        With GRIDCLIFAC
            .Focus()
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 > 0 Then
                    CursorGRID1 -= 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(1)
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
        With GRIDCLIFAC
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
                    .CurrentCell = .Rows(CursorGRID1).Cells(1)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonLetrAMinMay_Click(sender As Object, e As EventArgs) Handles ButtonLetrAMinMay.Click
        '
        ' Tecla SHIFT
        ' Letras MAYUSCULAS / minusculas
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

    Private Function EvitarCaracteres() As Boolean
        '
        '   NO permito Determinados Caracteres en:
        '   0 - Cod. de Clientes Credito 
        '   4 - Cod. Postal
        '   5 - TLF1
        '   6 - TLF2
        '   9 - % Dto.
        '
        EvitarCaracteres = False
        Select Case CtlFoco
            Case 0, 4, 5, 6, 9
                EvitarCaracteres = True
        End Select
        '
    End Function

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
            '
            ' Para Limitar el Numero de caracteres 
            ' a determinadas cajas de texto
            ' se gestiona aqui ...
            '
            Select Case CtlFoco
                '
                ' Cod. Cliente Crédito ( 430NNNNNN ) <= 9 Dígitos
                '
                Case 0
                    If TextBoxCLCodCli.Text.Trim.Length < 9 Then
                        VisorTecladoALFA.Text &= wBtnCal.Text
                    End If
                Case Else
                    VisorTecladoALFA.Text &= wBtnCal.Text
            End Select
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
                TextBoxCLCodCli.Text = VisorTecladoALFA.Text
            Case 1
                TextBoxCLNIFCIF.Text = VisorTecladoALFA.Text
            Case 2
                TextBoxCLNombre.Text = VisorTecladoALFA.Text
            Case 3
                TextBoxCLDirec.Text = VisorTecladoALFA.Text
            Case 4
                TextBoxCLCP.Text = VisorTecladoALFA.Text
            Case 5
                TextBoxCLTLF1.Text = VisorTecladoALFA.Text
            Case 6
                TextBoxCLTLF2.Text = VisorTecladoALFA.Text
            Case 7
                TextBoxCLPobla.Text = VisorTecladoALFA.Text
            Case 8
                TextBoxCLeMail.Text = VisorTecladoALFA.Text
            Case 9
                TextBoxCLDto.Text = VisorTecladoALFA.Text
            Case 888
                '
                ' Localizar Por Nombre
                '
                TextBoxCLLocNom.Text = VisorTecladoALFA.Text
        End Select
        '
    End Sub

    Private Sub ButtonLetrATras_Click(sender As Object, e As EventArgs) Handles ButtonLetrATras.Click
        '
        ' Quita Ultimo Carcter (Carácter Atrás)
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
        '
        VisorTecladoALFA.Text = ""
        Visor_A_Textos()
        '
    End Sub

    Private Sub ButtonLetrASPACE_Click(sender As Object, e As EventArgs) Handles ButtonLetrASPACE.Click
        VisorTecladoALFA.Text &= " "
        Visor_A_Textos()
    End Sub


    Private Sub ButtonLetrArroba_Click(sender As Object, e As EventArgs) Handles ButtonLetrArroba.Click
        '
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "@"
            Visor_A_Textos()
        End If
        '
    End Sub

    Private Sub ButtonLetrAMas_Click(sender As Object, e As EventArgs) Handles ButtonLetrAMas.Click
        '
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "+"
            Visor_A_Textos()
        End If
        '
    End Sub

    Private Sub ButtonLetrABarra_Click(sender As Object, e As EventArgs) Handles ButtonLetrABarra.Click
        '
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "/"
            Visor_A_Textos()
        End If
        '
    End Sub

    Private Sub ButtonLetrAPunto_Click(sender As Object, e As EventArgs) Handles ButtonLetrAPunto.Click
        '
        If EvitarCaracteres() = False Then
            VisorTecladoALFA.Text &= "."
            Visor_A_Textos()
        End If
        '
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
        CtlFoco = 0 : LimpiaCajasTexto(0)
        GRIDCLIFAC.Rows.Clear()
        VisorTecladoALFA.Text = ""
        Visor_A_Textos()
        TextBoxCLCodCli.Focus()
        '
    End Sub

    Private Sub ButtonLetrACR_Click(sender As Object, e As EventArgs) Handles ButtonLetrACR.Click
        '
        ' Tratamiento especial (CR).
        ' Limita CR sólo a Determinadas cajas de textos.
        '
        ' --- Opcional ---
        '
    End Sub

    Private Sub TextBoxCLCodCli_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLCodCli.GotFocus
        '
        CtlFoco = 0
        MiraContenidoTXT()
        GRIDCLIFAC.Columns("CLcodigo").Visible = True
        '
        With TextBoxCLCodCli
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub MiraContenidoTXT()
        '
        ' Gestiona el contenido de la propiedad .TEXT de los
        '   controles TextBox
        '
        Select Case CtlFoco
            Case 0
                If TextBoxCLCodCli.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLCodCli.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 1
                If TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLNIFCIF.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 2
                If TextBoxCLNombre.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLNombre.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 3
                If TextBoxCLDirec.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLDirec.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 4
                If TextBoxCLCP.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLCP.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 5
                If TextBoxCLTLF1.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLTLF1.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 6
                If TextBoxCLTLF2.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLTLF2.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 7
                If TextBoxCLPobla.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLPobla.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 8
                If TextBoxCLeMail.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLeMail.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 9
                If TextBoxCLDto.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLDto.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
            Case 888
                '
                ' Localizador Por Nombre
                '
                If TextBoxCLLocNom.Text.Trim.Length > 0 Then
                    VisorTecladoALFA.Text = TextBoxCLLocNom.Text.Trim
                Else
                    VisorTecladoALFA.Text = ""
                End If
        End Select
        '
    End Sub

    Private Sub TextBoxCLCodCli_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLCodCli.LostFocus
        TextBoxCLCodCli.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLNIFCIF_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLNIFCIF.GotFocus
        '
        CtlFoco = 1
        MiraContenidoTXT()
        GRIDCLIFAC.Columns("CLcodigo").Visible = False
        '
        With TextBoxCLNIFCIF
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLNIFCIF_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLNIFCIF.LostFocus
        TextBoxCLNIFCIF.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLNIFCIF_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCLNIFCIF.TextChanged
        '
        ' Al entrar un Código de Cliente CREDITO, 
        '   disparamos busqueda de coincidencias
        '   en la tabla MCO.
        '
        If CtlFoco = 1 Then
            If TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
                TextBoxCLCodCli.Text = ""
                CargaListaClieCONTA(TextBoxCLNIFCIF.Text.Trim, 0)
                '
                ' Si solo hay una línea en el grid, aceptamos la coincidencia
                '
                With GRIDCLIFAC
                    If .Rows.Count = 1 Then
                        .CurrentCell = .Rows(0).Cells(1)
                        TextBoxCLNIFCIF.Text = .SelectedCells(1).Value.ToString
                    End If
                End With
                '
                If LeeClienteCONTA(TextBoxCLNIFCIF.Text.Trim) = True Then
                    SacadatosCLIMCO(TextBoxCLNIFCIF.Text.Trim, 1)
                Else
                    LimpiaCajasTexto(1)
                End If
            Else
                GRIDCLIFAC.Rows.Clear()
                LimpiaCajasTexto(0)
            End If
        End If
        '
    End Sub

    Private Sub TextBoxCLCodCli_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLCodCli.KeyDown
        '
        ' Código Cliente Crédito
        '
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Down
                TextBoxCLNombre.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLLocNom_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLLocNom.GotFocus
        '
        CtlFoco = 888
        MiraContenidoTXT()
        '
    End Sub

    Private Sub SacadatosCLIMCO(wCodCli As String, wTbl As Integer)
        '
        ' Muestra en pantalla los datos de:
        '    wTbl = 0 :: CLIENTE CREDITO (MCO)
        '    wTbl = 1 :: CLIENTE CONTADO (CLICONTA)
        '
        Select Case wTbl
            Case 0
                If LeeClienteMCO(CInt(wCodCli.Trim)) = True Then
                    With wrLeeCLIEMCO
                        TextBoxCLCodCli.Text = wCodCli
                        TextBoxCLNombre.Text = .NOMBRE
                        TextBoxNCInfo.Text = .CIF
                        TextBoxCLDirec.Text = .DIRECCION
                        TextBoxCLPobla.Text = .POBLACION
                        TextBoxCLCP.Text = .CODPOSTAL.ToString
                        TextBoxCLeMail.Text = .EMAIL
                        TextBoxCLTLF1.Text = .TELEFONO
                        TextBoxCLTLF2.Text = .TELEFONO2
                        TextBoxCLDto.Text = .DTO
                    End With
                Else
                    LimpiaCajasTexto(0)
                End If
            Case 1
                If LeeClienteCONTA(wCodCli.Trim) = True Then
                    With wrLeeCLIEMCO
                        TextBoxCLNIFCIF.Text = wCodCli
                        TextBoxCLNombre.Text = .NOMBRE
                        TextBoxNCInfo.Text = .CIF
                        TextBoxCLDirec.Text = .DIRECCION
                        TextBoxCLPobla.Text = .POBLACION
                        TextBoxCLCP.Text = .CODPOSTAL.ToString
                        TextBoxCLeMail.Text = .EMAIL
                        TextBoxCLTLF1.Text = .TELEFONO
                        TextBoxCLTLF2.Text = .TELEFONO2
                        TextBoxCLDto.Text = .DTO
                    End With
                Else
                    LimpiaCajasTexto(0)
                End If
        End Select
        '
    End Sub

    Private Sub GRIDCLIFAC_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDCLIFAC.SelectionChanged
        '
        If CtlFoco = 0 Or CtlFoco = 1 Then
            Exit Sub
        End If
        '
        With GRIDCLIFAC
            If .SelectedRows.Count > 0 Then
                If .SelectedCells(0).Value.ToString.Trim = "--" Then
                    SacadatosCLIMCO(.SelectedCells(1).Value.ToString.Trim, 1)
                Else
                    SacadatosCLIMCO(.SelectedCells(0).Value.ToString.Trim, 0)
                End If
            End If
        End With
        '
    End Sub

    Private Sub GRIDCLIFAC_Click(sender As Object, e As EventArgs) Handles GRIDCLIFAC.Click
        '
        CtlFoco = 999
        With GRIDCLIFAC
            If .SelectedRows.Count > 0 Then
                If .SelectedCells(0).Value.ToString.Trim = "--" Then
                    SacadatosCLIMCO(.SelectedCells(1).Value.ToString.Trim, 1)
                Else
                    SacadatosCLIMCO(.SelectedCells(0).Value.ToString.Trim, 0)
                End If
            End If
        End With
        '
    End Sub

    Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
        '
        ' Crédito
        '
        OpcBusca = 0
        TextBoxCLNIFCIF.Text = ""
        If TextBoxCLLocNom.Text.Trim.Length > 0 Then
            CargaListaClieMCO(TextBoxCLLocNom.Text.Trim, 1)
        End If
        TextBoxCLLocNom.Focus()
        '
    End Sub
    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
        '
        ' Contado
        '
        OpcBusca = 1
        TextBoxCLCodCli.Text = ""
        If TextBoxCLLocNom.Text.Trim.Length > 0 Then
            CargaListaClieCONTA(TextBoxCLLocNom.Text.Trim, 1)
        End If
        TextBoxCLLocNom.Focus()
        '
    End Sub

    Private Sub TextBoxCLCodCli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCLCodCli.KeyPress
        '
        ' Sólo Números Para Evitar Error en Converion a Integer
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxCLNombre_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLNombre.GotFocus
        '
        CtlFoco = 2
        MiraContenidoTXT()
        '
        With TextBoxCLNombre
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLDirec_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLDirec.GotFocus
        '
        CtlFoco = 3
        MiraContenidoTXT()
        '
        With TextBoxCLDirec
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLCP_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLCP.GotFocus
        '
        CtlFoco = 4
        MiraContenidoTXT()
        '
        With TextBoxCLCP
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLTLF1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLTLF1.GotFocus
        '
        CtlFoco = 5
        MiraContenidoTXT()
        '
        With TextBoxCLTLF1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLTLF2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLTLF2.GotFocus
        '
        CtlFoco = 6
        MiraContenidoTXT()
        '
        With TextBoxCLTLF2
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLPobla_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLPobla.GotFocus
        '
        CtlFoco = 7
        MiraContenidoTXT()
        '
        With TextBoxCLPobla
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLeMail_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLeMail.GotFocus
        '
        CtlFoco = 8
        MiraContenidoTXT()
        '
        With TextBoxCLeMail
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLDto_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCLDto.GotFocus
        '
        CtlFoco = 9
        MiraContenidoTXT()
        '
        With TextBoxCLDto
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxCLNombre_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLNombre.LostFocus
        TextBoxCLNombre.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLDirec_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLDirec.LostFocus
        TextBoxCLDirec.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLCP_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLCP.LostFocus
        TextBoxCLCP.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLTLF1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLTLF1.LostFocus
        TextBoxCLTLF1.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLTLF2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLTLF2.LostFocus
        TextBoxCLTLF2.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLPobla_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLPobla.LostFocus
        TextBoxCLPobla.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLeMail_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLeMail.LostFocus
        TextBoxCLeMail.BackColor = Color.White
    End Sub

    Private Sub TextBoxCLDto_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCLDto.LostFocus
        TextBoxCLDto.BackColor = Color.White
    End Sub

    Private Sub TextBoxNCInfo_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNCInfo.GotFocus
        TextBoxCLDto.Focus()
    End Sub

    Private Sub TextBoxCLNIFCIF_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLNIFCIF.KeyDown
        '
        ' NIF / CIF, Clientes Contado
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLCodCli.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLNombre.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLNombre_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLNombre.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLNIFCIF.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLDirec.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLDirec_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLDirec.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLNombre.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLCP.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLCP_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLCP.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLDirec.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLTLF1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLTLF1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLTLF1.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLCP.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLTLF2.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLTLF2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLTLF2.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLTLF1.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLPobla.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLPobla_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLPobla.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLTLF2.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLeMail.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLeMail_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLeMail.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLPobla.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLDto.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxCLDto_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCLDto.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxCLeMail.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxCLCodCli.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub ButtonLetrAENTER_Click(sender As Object, e As EventArgs) Handles ButtonLetrAENTER.Click
        '
        ' Le damos utilidad a la tecla ENTER
        '
        Select Case CtlFoco
            Case 0, 1
                TextBoxCLNombre.Focus()
            Case 2
                TextBoxCLDirec.Focus()
            Case 3
                TextBoxCLCP.Focus()
            Case 4
                TextBoxCLTLF1.Focus()
            Case 5
                TextBoxCLTLF2.Focus()
            Case 6
                TextBoxCLPobla.Focus()
            Case 7
                TextBoxCLeMail.Focus()
            Case 8
                TextBoxCLDto.Focus()
            Case 9
                TextBoxCLCodCli.Focus()
        End Select
        '
    End Sub

    Private Sub ButtonActualiza_Click(sender As Object, e As EventArgs) Handles ButtonActualiza.Click
        '
        ' Grabar Datos Clientes
        '    CREDITO (MCO) -> Depende de Ref. Generales
        '    CONTADO (CLICONTA)
        '
        If TextBoxCLCodCli.Text.Trim.Length > 0 And TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
            msg = "Sólo se permite Cod. Clientes [430] " & vbCrLf
            msg &= "o NIF/CIF. de clientes." & vbCrLf & vbCrLf
            msg &= "Por favor tenga en cuenta esto.:" & vbCrLf & vbCrLf
            msg &= "Los datos para un código de cliente [430]" & vbCrLf
            msg &= "y los Datos para un NIF/CIF determinado," & vbCrLf
            msg &= "diferencia DOS grupos de clientes distintos." & vbCrLf
            style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            title = "Error en Datos de Cliente."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        LeeTCONA4Cfg("General")
        '
        ' CONTADO (CLICONTA)
        '
        '
        ' Cod. Cli = 0, Nif./Cif. > 0
        '
        If TextBoxCLCodCli.Text.Trim.Length = 0 And
                TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
            ' El cliente debe tener al menos un Nombre.
            If TextBoxCLNombre.Text.Trim.Length > 0 Then
                '
                MantenimientoCLICONTA(TextBoxCLNIFCIF.Text.Trim)
                CargaListaClieCONTA(TextBoxCLNIFCIF.Text.Trim, 0)
            Else
                msg = "Se requiere al menos un nombre" & vbCrLf
                msg &= "para actualizar datos del cliente." & vbCrLf & vbCrLf
                style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                title = "Error en Datos de Cliente."
                MsgBox(msg, style, title)
                TextBoxCLNombre.Focus()
            End If
            Exit Sub
        End If
        '
        ' Credito [430, MCO]
        ' Se permite Crear/Modificar Crédito?
        '
        If wrLeeTCONA4.Tcona4_CREACLICREDITO = "True" Then
            '
            ' Cod. Cli > 0, Nif./Cif. = 0
            '
            If TextBoxCLCodCli.Text.Trim.Length > 0 And
                TextBoxCLNIFCIF.Text.Trim.Length = 0 Then
                '
                If Mid(TextBoxCLCodCli.Text.Trim, 1, 3) = "430" Then
                    ' El cliente debe tener al menos un Nombre.
                    If TextBoxCLNombre.Text.Trim.Length > 0 Then
                        '
                        MantenimientoMCO(TextBoxCLCodCli.Text.Trim)
                        CargaListaClieMCO(TextBoxCLCodCli.Text.Trim, 0)
                    Else
                        msg = "Se requiere al menos un nombre" & vbCrLf
                        msg &= "para actualizar datos del cliente." & vbCrLf & vbCrLf
                        style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                        title = "Error en Datos de Cliente."
                        MsgBox(msg, style, title)
                        TextBoxCLNombre.Focus()
                    End If
                Else
                    msg = "Sólo se permite Cod. Clientes [430.nnnnnn]. " & vbCrLf
                    msg &= "Por ejemplo.: 430000001" & vbCrLf & vbCrLf
                    style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                    title = "Error en Datos de Cliente."
                    MsgBox(msg, style, title)
                End If
            End If
        Else
            msg = "La administración no permite crear/modificar " & vbCrLf
            msg &= "Nuevos Clientes [430.nnnnnn]. " & vbCrLf
            style = MsgBoxStyle.Information Or MsgBoxStyle.OkOnly
            title = "Actualización Datos Cliente no permitido."
            MsgBox(msg, style, title)
        End If
        '
    End Sub

    Private Sub ButtonSelecciona_Click(sender As Object, e As EventArgs) Handles ButtonSelecciona.Click
        '
        ' Asigna el Cliente Contado o Crédito seleccionado a la mesa actual.
        ' Comprobación Necesaria:
        '
        If TextBoxCLCodCli.Text.Trim.Length > 0 And TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
            msg = "Sólo se permite Cod. Clientes [430] " & vbCrLf
            msg &= "o NIF/CIF. de clientes." & vbCrLf & vbCrLf
            msg &= "Por favor tenga en cuenta esto.:" & vbCrLf & vbCrLf
            msg &= "Los datos para un código de cliente [430]" & vbCrLf
            msg &= "y los Datos para un NIF/CIF determinado," & vbCrLf
            msg &= "diferencia DOS grupos de clientes distintos." & vbCrLf
            style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            title = "Error en Datos de Cliente."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        ' CREDITO, [430, MCO]
        '
        If TextBoxCLCodCli.Text.Trim.Length > 0 Then
            LeeTCONA4Cfg("General")
            '
            ' Se permite Crear/Modificar Crédito?
            '
            If wrLeeTCONA4.Tcona4_CREACLICREDITO = "True" Then
                '
                ' Cod. Cli > 0, Nif./Cif. = 0
                '
                If TextBoxCLCodCli.Text.Trim.Length > 0 And
                TextBoxCLNIFCIF.Text.Trim.Length = 0 Then
                    '
                    If Mid(TextBoxCLCodCli.Text.Trim, 1, 3) = "430" Then
                        ' El cliente debe tener al menos un Nombre.
                        If TextBoxCLNombre.Text.Trim.Length > 0 Then
                            '
                            ' Graba datos del Cliente y actualiza la Mesa.
                            '
                            MantenimientoMCO(TextBoxCLCodCli.Text.Trim)
                            Exit Sub
                        Else
                            msg = "Se requiere al menos un nombre" & vbCrLf
                            msg &= "para actualizar datos del cliente." & vbCrLf & vbCrLf
                            style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                            title = "Error en Datos de Cliente."
                            MsgBox(msg, style, title)
                            TextBoxCLNombre.Focus()
                            Exit Sub
                        End If
                    Else
                        msg = "Sólo se permite Cod. Clientes [430.nnnnnn]. " & vbCrLf
                        msg &= "Por ejemplo.: 430000001" & vbCrLf & vbCrLf
                        style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                        title = "Error en Datos de Cliente."
                        MsgBox(msg, style, title)
                        Exit Sub
                    End If
                End If
            End If
            '
            ' Independiente de si se permite o no actualizar clientes credito
            ' la asiganacion a la mesa debe realizarse...
            '
            wCliente = CInt(TextBoxCLCodCli.Text.Trim)
            WMesacNIFCIF = ""
            ActualizaDatosMESAC(TextBoxNumMesaCL.Text.Trim, 52) ' 52 = Crédto
            MyFrm2.LabelNomCliente.Text = TextBoxCLNombre.Text.Trim
            MyFrm2.LabelNomCliente.ForeColor = Color.Cyan
            Me.Hide()
            Exit Sub
        End If
        '
        ' CONTADO (CLICONTA), Cod. Cli = 0, Nif./Cif. > 0
        '
        If TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
            '
            ' Este apartado Graba el Cliente CONTADO
            '  con la intencion de no perder datos si el usuario 
            '   no graba los datos antes de asignar a la mesa.
            '
            If TextBoxCLCodCli.Text.Trim.Length = 0 And
                TextBoxCLNIFCIF.Text.Trim.Length > 0 Then
                ' El cliente debe tener al menos un Nombre.
                If TextBoxCLNombre.Text.Trim.Length > 0 Then
                    '
                    ' Graba datos del Cliente y actualiza la Mesa.
                    '
                    MantenimientoCLICONTA(TextBoxCLNIFCIF.Text.Trim)
                    WMesacNIFCIF = TextBoxCLNIFCIF.Text.Trim
                    wCliente = 430000000
                    ActualizaDatosMESAC(TextBoxNumMesaCL.Text.Trim, 51) ' 51 = Contado
                    MyFrm2.LabelNomCliente.Text = TextBoxCLNombre.Text.Trim
                    MyFrm2.LabelNomCliente.ForeColor = Color.Yellow
                    Me.Hide()
                Else
                    msg = "Se requiere al menos un nombre" & vbCrLf
                    msg &= "para actualizar datos del cliente." & vbCrLf & vbCrLf
                    style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                    title = "Error en Datos de Cliente."
                    MsgBox(msg, style, title)
                    TextBoxCLNombre.Focus()
                End If
            End If
        End If
        '
    End Sub

End Class