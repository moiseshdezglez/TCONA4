Public Class TCONA404
    Private Sub TCONA404_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxEntraCambio_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEntraCambio.GotFocus
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub TCONA404_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        SwFoco_404 = 4
        VariablesCero()
        LimpaCajasTexto()
        '
        Select Case OpenFrom
            Case 402 ' Cobros desde TCONA402 -> Familias/Productos
                With MyFrm4
                    '
                    ' Activamos algunos controles.
                    ' (Por Si han sido descativados)
                    '
                    .ButtonCTarjeta.Visible = True
                    .ButtonCCheques.Visible = True
                    .ButtonCOtros.Visible = True
                    .Label7.Visible = True
                    .Label8.Visible = True
                    .Label9.Visible = True
                    .TextBoxEntraTarjeta.Visible = True
                    .TextBoxEntraCheques.Visible = True
                    .TextBoxEntraOtros.Visible = True
                    '
                    .TextBoxCMesa.Text = MyFrm2.TextBoxNumMesa.Text.Trim
                    .TextBoxCPax.Text = MyFrm2.TextBoxPax.Text.Trim
                    .TextBoxCTOTALMesa.Text = MyFrm2.LabelTotComanda.Text.Trim
                    .TextBoxEntraEfectivo.Text = MyFrm2.LabelTotComanda.Text.Trim
                    .VisorTeclado.Text = ""
                    .TextBoxEntraEntrega.BackColor = WcolFocoCobros
                    .TextBoxEntraEntrega.Focus()
                    .TextBoxEntraEntrega.SelectionStart = 0
                    .TextBoxEntraEntrega.SelectionLength = TextBoxEntraEfectivo.Text.Length
                End With
            Case 413 ' Cobros desde TCONA413 -> Cuentas Separadas
                With MyFrm4
                    '
                    ' Desactivamos algunos controles.
                    ' (Por ahora Solo Cobros en Efectivo)
                    '
                    .ButtonCTarjeta.Visible = False
                    .ButtonCCheques.Visible = False
                    .ButtonCOtros.Visible = False
                    .Label7.Visible = False
                    .Label8.Visible = False
                    .Label9.Visible = False
                    .TextBoxEntraTarjeta.Visible = False
                    .TextBoxEntraCheques.Visible = False
                    .TextBoxEntraOtros.Visible = False
                    '
                    .TextBoxCMesa.Text = MyFrm13.TextBoxSepNumMesa1.Text.Trim
                    .TextBoxCPax.Text = MyFrm13.TextBoxSepPAX.Text.Trim
                    .TextBoxCTOTALMesa.Text = MyFrm13.LabelTotComandaSep1.Text.Trim
                    .TextBoxEntraEfectivo.Text = MyFrm13.LabelTotComandaSep1.Text.Trim
                    .VisorTeclado.Text = ""
                    .TextBoxEntraEntrega.BackColor = WcolFocoCobros
                    .TextBoxEntraEntrega.Focus()
                    .TextBoxEntraEntrega.SelectionStart = 0
                    .TextBoxEntraEntrega.SelectionLength = TextBoxEntraEfectivo.Text.Length
                End With
        End Select
        '
        If CALCULADO = False Then
            HazCalculosCOBRO()
        End If
        '
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonCobroCancela.Click
        Me.Hide()
    End Sub

    Private Sub LimpaCajasTexto()
        '
        With MyFrm4
            .TextBoxEntraEfectivo.Text = "0.00"
            .TextBoxEntraCambio.Text = "0.00"
            .TextBoxEntraCheques.Text = "0.00"
            .TextBoxEntraEntrega.Text = "0.00"
            .TextBoxEntraOtros.Text = "0.00"
            .TextBoxEntraTarjeta.Text = "0.00"
        End With
        '
    End Sub

    Private Sub VariablesCero()
        '
        CobroTOTALMesa = 0
        CobroEfectivo = 0
        CobroTarjetas = 0
        CobroCheques = 0
        CobroOtros = 0
        CobroEntrega = 0
        CobroCambio = 0
        CobroActual = 0
        CobroPendiente = 0
        '
    End Sub

    Public Sub HazCalculosCOBRO()
        '
        ' Calculos Generales COBRO / CAMBIO
        '
        CALCULADO = True
        '
        ' Validación, Evitar Errores Conversión...
        '
        If TextBoxEntraEfectivo.Text.Trim.Length = 0 Then
            TextBoxEntraEfectivo.Text = "0.00"
        End If
        If TextBoxEntraTarjeta.Text.Trim.Length = 0 Then
            TextBoxEntraTarjeta.Text = "0.00"
        End If
        If TextBoxEntraCheques.Text.Trim.Length = 0 Then
            TextBoxEntraCheques.Text = "0.00"
        End If
        If TextBoxEntraOtros.Text.Trim.Length = 0 Then
            TextBoxEntraOtros.Text = "0.00"
        End If
        If TextBoxEntraEntrega.Text.Trim.Length = 0 Then
            TextBoxEntraEntrega.Text = "0.00"
        End If
        '
        CobroTOTALMesa = CDec(TextBoxCTOTALMesa.Text.Trim.Replace(".", ","))
        '
        CobroEfectivo = CDec(TextBoxEntraEfectivo.Text.Trim.Replace(".", ","))
        CobroTarjetas = CDec(TextBoxEntraTarjeta.Text.Trim.Replace(".", ","))
        CobroCheques = CDec(TextBoxEntraCheques.Text.Trim.Replace(".", ","))
        CobroOtros = CDec(TextBoxEntraOtros.Text.Trim.Replace(".", ","))
        CobroActual = CobroEfectivo + CobroTarjetas + CobroCheques + CobroOtros
        CobroPendiente = CobroTOTALMesa - CobroActual
        '
        LabelTotActual.Text = CobroActual.ToString(fmtImporte).Replace(",", ".")
        LabelTotPendiente.Text = CobroPendiente.ToString(fmtImporte).Replace(",", ".")
        '
        '   CAMBIO
        '
        CobroEntrega = CDec(TextBoxEntraEntrega.Text.Trim.Replace(".", ","))
        If CobroEntrega = 0 Then
            CobroCambio = 0
        Else
            CobroCambio = CobroEntrega - CobroEfectivo
        End If
        TextBoxEntraCambio.Text = CobroCambio.ToString(fmtImporte).Replace(",", ".")
        If CobroCambio < 0 Then
            TextBoxEntraCambio.ForeColor = Color.Red
        Else
            TextBoxEntraCambio.ForeColor = Color.White
        End If
        '
    End Sub

    Private Sub ButtonCEfectivo_Click(sender As Object, e As EventArgs) Handles ButtonCEfectivo.Click
        '
        ' PAGO En Efectivo
        '
        LimpaCajasTexto()
        Me.TextBoxEntraEfectivo.Text = Me.TextBoxCTOTALMesa.Text.Trim
        HazCalculosCOBRO()
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub ButtonCTarjeta_Click(sender As Object, e As EventArgs) Handles ButtonCTarjeta.Click
        '
        ' PAGO Con Tarjeta
        '
        LimpaCajasTexto()
        TextBoxEntraTarjeta.Text = Me.TextBoxCTOTALMesa.Text.Trim
        HazCalculosCOBRO()
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub ButtonCCheques_Click(sender As Object, e As EventArgs) Handles ButtonCCheques.Click
        '
        ' PAGO Mediante Checques
        '
        LimpaCajasTexto()
        Me.TextBoxEntraCheques.Text = Me.TextBoxCTOTALMesa.Text.Trim
        HazCalculosCOBRO()
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub ButtonCOtros_Click(sender As Object, e As EventArgs) Handles ButtonCOtros.Click
        '
        ' PAGO Otros Medios
        '
        LimpaCajasTexto()
        Me.TextBoxEntraOtros.Text = Me.TextBoxCTOTALMesa.Text.Trim
        HazCalculosCOBRO()
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub TextBoxCMesa_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCMesa.GotFocus
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub TextBoxCPax_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCPax.GotFocus
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub TextBoxCTOTALMesa_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCTOTALMesa.GotFocus
        TextBoxEntraEntrega.Focus()
    End Sub

    Private Sub TextBoxEntraEfectivo_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEntraEfectivo.GotFocus
        SwFoco_404 = 0
        DesactivaFocos()
        TextBoxEntraEfectivo.BackColor = WcolFocoCobros
    End Sub

    Private Sub TextBoxEntraEfectivo_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEntraEfectivo.LostFocus
        TextBoxEntraEfectivo.BackColor = Color.White
    End Sub

    Private Sub TextBoxEntraTarjeta_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEntraTarjeta.GotFocus
        SwFoco_404 = 1
        DesactivaFocos()
        TextBoxEntraTarjeta.BackColor = WcolFocoCobros
    End Sub

    Private Sub TextBoxEntraTarjeta_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEntraTarjeta.LostFocus
        TextBoxEntraTarjeta.BackColor = Color.White
    End Sub

    Private Sub TextBoxEntraCheques_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEntraCheques.GotFocus
        SwFoco_404 = 2
        DesactivaFocos()
        TextBoxEntraCheques.BackColor = WcolFocoCobros
    End Sub

    Private Sub TextBoxEntraCheques_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEntraCheques.LostFocus
        TextBoxEntraCheques.BackColor = Color.White
    End Sub

    Private Sub TextBoxEntraOtros_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEntraOtros.GotFocus
        SwFoco_404 = 3
        DesactivaFocos()
        TextBoxEntraOtros.BackColor = WcolFocoCobros
    End Sub

    Private Sub TextBoxEntraOtros_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEntraOtros.LostFocus
        TextBoxEntraOtros.BackColor = Color.White
    End Sub

    Private Sub TextBoxEntraEntrega_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEntraEntrega.GotFocus
        SwFoco_404 = 4
        DesactivaFocos()
        TextBoxEntraEntrega.BackColor = WcolFocoCobros
    End Sub

    Private Sub TextBoxEntraEntrega_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEntraEntrega.LostFocus
        TextBoxEntraEntrega.BackColor = Color.White
    End Sub

    Private Sub HazClickBTNCalculadora(wMiBtnCALC As Button)
        '
        '   Manejo del evento CLICK para botones CALCULADORA
        '
        If VisorTeclado.Text.Length > 9 Then
            Exit Sub
        End If
        '
        Dim wPos As Integer = 0
        Select Case wMiBtnCALC.Text
            Case "ENTER"
                HazCalculosCOBRO()
                VisorTeclado.Text = ""
                Exit Sub
            Case "-"
                '
                ' Sólo un "-", y si hay una coma y es el último carácter
                '  completamos ,00-"
                '
                wPos = InStr(VisorTeclado.Text, "-")
                If wPos = 0 Then
                    If VisorTeclado.Text.Length > 0 Then
                        If Microsoft.VisualBasic.Right(VisorTeclado.Text.Trim, 1) = "," Then
                            VisorTeclado.Text += "00" + wMiBtnCALC.Text
                        Else
                            VisorTeclado.Text += wMiBtnCALC.Text
                        End If
                    End If
                End If
                '
            Case ","
                '
                ' Sólo una "," y nunca despues de un "-"
                '
                wPos = InStr(VisorTeclado.Text, ",")
                If wPos = 0 Then
                    If VisorTeclado.Text.Length > 0 Then
                        wPos = InStr(VisorTeclado.Text, "-")
                        If wPos = 0 Then
                            VisorTeclado.Text += wMiBtnCALC.Text
                        End If
                    End If
                End If
                '
            Case Else
                '
                ' Nunca digitos despues del "-"
                '
                wPos = InStr(VisorTeclado.Text, "-")
                If wPos = 0 Then
                    VisorTeclado.Text += wMiBtnCALC.Text
                End If
        End Select
        '
        MiraFoco()
        '
    End Sub

    Private Sub DesactivaFocos()
        '
        TextBoxEntraEfectivo.BackColor = Color.White
        TextBoxEntraTarjeta.BackColor = Color.White
        TextBoxEntraCheques.BackColor = Color.White
        TextBoxEntraOtros.BackColor = Color.White
        TextBoxEntraEntrega.BackColor = Color.White
        '
    End Sub

    Private Sub MiraFoco()
        '
        ' Determinamos el Control que esta recibiendo el FOCO de forma
        '    PREDETRMINADA...
        '
        DesactivaFocos()
        '
        Select Case SwFoco_404
            Case 0
                TextBoxEntraEfectivo.Text = VisorTeclado.Text
                TextBoxEntraEfectivo.BackColor = WcolFocoCobros
            Case 1
                TextBoxEntraTarjeta.Text = VisorTeclado.Text
                TextBoxEntraTarjeta.BackColor = WcolFocoCobros
            Case 2
                TextBoxEntraCheques.Text = VisorTeclado.Text
                TextBoxEntraCheques.BackColor = WcolFocoCobros
            Case 3
                TextBoxEntraOtros.Text = VisorTeclado.Text
                TextBoxEntraOtros.BackColor = WcolFocoCobros
            Case 4
                TextBoxEntraEntrega.Text = VisorTeclado.Text
                TextBoxEntraCambio.Text = ""
                TextBoxEntraEntrega.BackColor = WcolFocoCobros
        End Select
        '
    End Sub

    Private Sub ButtonCal7_Click(sender As Object, e As EventArgs) _
        Handles ButtonCal7.Click, ButtonGuion.Click, ButtonEnter.Click,
        ButtonCalComma.Click, ButtonCal9.Click, ButtonCal8.Click,
        ButtonCal6.Click, ButtonCal5.Click, ButtonCal4.Click, ButtonCal3.Click,
        ButtonCal2.Click, ButtonCal1.Click, ButtonCal00.Click, ButtonCal0.Click
        '
        HazClickBTNCalculadora(CType(sender, Button))
        '
    End Sub

    Private Sub ButtonCLR_Click(sender As Object, e As EventArgs) Handles ButtonCLR.Click
        VisorTeclado.Text = ""
        MiraFoco()
    End Sub

    Private Sub ButtonCobroAcepta_Click(sender As Object, e As EventArgs) Handles ButtonCobroAcepta.Click
        '
        ' Se puede COBRAR? ...
        '
        HazCalculosCOBRO()
        If CobroPendiente = 0 And CobroCambio = 0 And
            CobroEntrega = 0 And CobroEfectivo = 0 Then
            Me.Hide()
            wTarifaBarra = 0
            Select Case OpenFrom
                Case 402
                    AccionCobroAceptar()
                Case 413
                    AccionCobroAceptarSepa()
            End Select
            Exit Sub
        End If
        '
        If CobroPendiente = 0 And CobroEntrega >= CobroEfectivo Then
            Me.Hide()
            Select Case OpenFrom
                Case 402
                    AccionCobroAceptar()
                Case 413
                    AccionCobroAceptarSepa()
            End Select
            Exit Sub
        End If
        '
    End Sub

    Private Sub ButtonCRestaurar_Click(sender As Object, e As EventArgs) Handles ButtonCRestaurar.Click
        '
        '   Botón Restaurar Ventana COBROS
        '   Personalización según gustos del Usuario Final.
        '
        myfrm4_Restaurado = Not myfrm4_Restaurado
        '
        With Me
            If myfrm4_Restaurado Then
                .Top = MyFrm2_Top
                .Left = MyFrm2_Left
                .Height = MyFrm2_Height
                .Width = MyFrm2_Width
                .ButtonCRestaurar.Left =
                    (.Height - .ButtonCRestaurar.Height - 20) + 260
            Else
                .Top = MyFrm4_Top
                .Left = MyFrm4_Left
                .Height = MyFrm4_Height
                .Width = MyFrm4_Width
                .ButtonCRestaurar.Left = .Height - .ButtonCRestaurar.Height - 20
            End If
            SituaCalculadora()
        End With
        '
    End Sub

    Private Sub SituaCalculadora()
        '
        '  POSICION de la CALCULADORA
        '   Manejamos la coleccion de Controles "Botones Calculadora"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Or TypeOf wControl Is Label Then
                'NombreBoton = CType(wControl, Button).Name
                NombreBoton = wControl.Name
                If Mid$(NombreBoton, 1, 9) = "ButtonCal" Or
                        NombreBoton = "ButtonCLR" Or
                    NombreBoton = "ButtonEnter" Or
                    NombreBoton = "ButtonGuion" Or
                    NombreBoton = "VisorTeclado" Then
                    With wControl
                        If myfrm4_Restaurado = True Then
                            .Left += 650
                            .Top -= 20
                        Else
                            .Left -= 650
                            .Top += 20
                        End If
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub TCONA404_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        MyFrm2_Height = MyFrm2.Height
        MyFrm2_Width = MyFrm2.Width
        MyFrm2_Top = MyFrm2.Top
        MyFrm2_Left = MyFrm2.Left
        With Me
            If myfrm4_Restaurado = False Then
                MyFrm4_Height = Me.Height
                MyFrm4_Width = Me.Width
                MyFrm4_Top = Me.Top
                MyFrm4_Left = Me.Left
                .ButtonCRestaurar.Left = Me.Height - .ButtonCRestaurar.Height - 20
            End If
        End With
        '
    End Sub

    Private Sub LabelTotPendiente_Click(sender As Object, e As EventArgs) Handles LabelTotPendiente.Click
        '
        ' La Cantidad va al TextBox con Foco activo.
        '
        Select Case SwFoco_404
            Case 0
                TextBoxEntraEfectivo.Text = LabelTotPendiente.Text
                TextBoxEntraEfectivo.BackColor = WcolFocoCobros
            Case 1
                TextBoxEntraTarjeta.Text = LabelTotPendiente.Text
                TextBoxEntraTarjeta.BackColor = WcolFocoCobros
            Case 2
                TextBoxEntraCheques.Text = LabelTotPendiente.Text
                TextBoxEntraCheques.BackColor = WcolFocoCobros
            Case 3
                TextBoxEntraOtros.Text = LabelTotPendiente.Text
                TextBoxEntraOtros.BackColor = WcolFocoCobros
            Case 4
                TextBoxEntraEntrega.Text = LabelTotPendiente.Text
                TextBoxEntraCambio.Text = ""
                TextBoxEntraEntrega.BackColor = WcolFocoCobros
        End Select
        '
    End Sub

    Private Sub TextBoxEntraEfectivo_Click(sender As Object, e As EventArgs) Handles TextBoxEntraEfectivo.Click
        '
        TextBoxEntraEfectivo.SelectionStart = 0
        TextBoxEntraEfectivo.SelectionLength = TextBoxEntraEfectivo.Text.Length
        '
    End Sub

    Private Sub TextBoxEntraTarjeta_Click(sender As Object, e As EventArgs) Handles TextBoxEntraTarjeta.Click
        '
        TextBoxEntraTarjeta.SelectionStart = 0
        TextBoxEntraTarjeta.SelectionLength = TextBoxEntraTarjeta.Text.Length
        '
    End Sub

    Private Sub TextBoxEntraCheques_Click(sender As Object, e As EventArgs) Handles TextBoxEntraCheques.Click
        '
        TextBoxEntraCheques.SelectionStart = 0
        TextBoxEntraCheques.SelectionLength = TextBoxEntraCheques.Text.Length
        '
    End Sub

    Private Sub TextBoxEntraOtros_Click(sender As Object, e As EventArgs) Handles TextBoxEntraOtros.Click
        '
        TextBoxEntraOtros.SelectionStart = 0
        TextBoxEntraOtros.SelectionLength = TextBoxEntraOtros.Text.Length
        '
    End Sub

    Private Sub TextBoxEntraEntrega_Click(sender As Object, e As EventArgs) Handles TextBoxEntraEntrega.Click
        '
        TextBoxEntraEntrega.SelectionStart = 0
        TextBoxEntraEntrega.SelectionLength = TextBoxEntraEntrega.Text.Length
        '
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        '
        TextBoxEntraEntrega.Text = TextBoxEntraEfectivo.Text.Trim
        '
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        '
        TextBoxEntraEntrega.Text = TextBoxEntraTarjeta.Text.Trim
        '
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        '
        TextBoxEntraEntrega.Text = TextBoxEntraCheques.Text.Trim
        '
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        '
        TextBoxEntraEntrega.Text = TextBoxEntraOtros.Text.Trim
        '
    End Sub
End Class