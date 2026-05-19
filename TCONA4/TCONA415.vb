Public Class TCONA415

    Dim Mayus As Boolean = False
    Dim TimeLapse As Integer = 5
    Dim SwSimbolos As Boolean = False
    '
    ' Para el Timer ...
    '
    Dim TimerMensaLapse As Integer = 1
    Dim MiMensaSegu As String = ""
    Dim MiContador As Integer = 0

    Private Sub TCONA415_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA415_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Carga FORM Teclados en Pantalla
        '
        With Me
            .Height = 519
            .VisorTeclado.Text = ""
            .VisorTecladoALFA.Text = ""
            .PanelTecladoALFA.Visible = False
            .PanelTecladoNUMEROS.Visible = False
            .Refresh()
        End With
        '
        ' Teclado de simbolos inicialmente esta Oculto y
        '    fijamos su posicion inicial.
        '
        SwSimbolos = False
        PanelTecladoSimbolos.Top = PanelTecladoALFA.Top + 80
        PanelTecladoSimbolos.Left = PanelTecladoALFA.Left
        Select Case SwSimbolos
            Case True
                PanelTecladoSimbolos.Visible = True
                ButtonLetrASim.BackColor = Color.Red
            Case False
                PanelTecladoSimbolos.Visible = False
                ButtonLetrASim.BackColor = Color.Salmon
        End Select

        '
        ' Recupera las Frases Favoritas
        '
        LeeTCONA4Cfg("General")
        With wrLeeTCONA4
            TextBoxMensaFAV1.Text = .Tcona4_TECLADOFAV1
            TextBoxMensaFAV2.Text = .Tcona4_TECLADOFAV2
            TextBoxMensaFAV3.Text = .Tcona4_TECLADOFAV3
            TextBoxMensaFAV4.Text = .Tcona4_TECLADOFAV4
            TextBoxMensaFAV5.Text = .Tcona4_TECLADOFAV5
        End With
        '
        ' Teclado Flotante Propiedades:
        ' .CadenaVisor, Iniciamos = ""
        ' .Tipo, Define el tipo de Teclado.
        '     0=Numérico, 1=Alfabético, 2=Alfanumérico
        ' .Top, .Left, .Height, .Width:
        '     Tamaño y Posicion del FORM que llama al teclado.
        ' .BackColor
        '     Color Fondo, Viene dado al llamar al teclado (Opcionalmente.) 
        '
        With wrTecladoFlotante
            Try
                Me.BackColor = .BackColor
            Catch ex As Exception
                Me.BackColor = WcolFondoTCONA401
            End Try
            '
            .CadenaVisor = ""
            '
            ' Indica se se Pide Password (0=No/1=Si).
            '
            If .PidePwd = 0 Then
                VisorTeclado.PasswordChar = CChar("")
                VisorTecladoALFA.PasswordChar = CChar("")
            Else
                VisorTeclado.PasswordChar = CChar("*")
                VisorTecladoALFA.PasswordChar = CChar("*")
            End If
            '
            LabelMensaUSR1.Visible = False
            LabelMensaUSR2.Visible = False
            '
            Select Case .Tipo
                Case 0
                    LabelMensaUSR1.Text = .MensaUsuario
                    LabelMensaUSR1.Visible = True
                    PanelFavoritas.Visible = False
                    PanelTecladoNUMEROS.Visible = True
                    Width = PanelTecladoNUMEROS.Width + 20
                Case 1
                    LabelMensaUSR2.Text = .MensaUsuario
                    LabelMensaUSR2.Visible = True
                    PanelTecladoALFA.Left = PanelTecladoNUMEROS.Left
                    PanelTecladoALFA.Visible = True
                    Width = PanelTecladoALFA.Width + 20
                    PanelFavoritas.Left = 243
                    PanelFavoritas.Visible = True
                Case 2
                    LabelMensaUSR2.Text = .MensaUsuario
                    LabelMensaUSR2.Visible = True
                    ButtonCalEnter.Visible = False
                    VisorTeclado.Visible = False
                    ButtonClr.Visible = False
                    PanelTecladoNUMEROS.Width = 233
                    PanelTecladoALFA.Left = 251
                    Width = 950
                    PanelTecladoNUMEROS.Visible = True
                    PanelTecladoALFA.Visible = True
                    PanelFavoritas.Left = PanelTecladoALFA.Left
                    PanelFavoritas.Visible = True
            End Select
            '
            ' Iniciliza el Cursor...
            '
            TextBoxMensaFAV1.Focus()
            .CodigoInterno = 1
            LabelIndicador.Top = TextBoxMensaFAV1.Top
        End With
        '
        CentraMe()
        '
    End Sub

    Private Sub CentraMe()
        '
        ' Teclado Centrado
        '
        Dim rect As Rectangle = Screen.PrimaryScreen.WorkingArea
        Top = CInt((rect.Height / 2) - (Height / 2))
        Left = CInt((rect.Width / 2) - (Width / 2))
        rect = Nothing ' Libera Recursos
        '
    End Sub

    Private Sub ButtonEnter_Click(sender As Object, e As EventArgs) Handles ButtonCalEnter.Click
        '
        ' ENTER Numérico
        '
        Me.Close()
        '
    End Sub

    Private Sub ButtonLetraENTER_Click(sender As Object, e As EventArgs) Handles ButtonLetrAENTER.Click
        '
        ' ENTER AlfaNumérico
        '
        Me.Close()
        '
    End Sub

    Private Sub ButtonTecladoArriba_Click(sender As Object, e As EventArgs) Handles ButtonTecladoArriba.Click
        '
        ' Teclado Arriba
        '
        With wrTecladoFlotante
            Do While Me.Top > (.Top + 10)
                Me.Top -= 30
                Threading.Thread.Sleep(TimeLapse)
            Loop
            Me.Top = .Top
        End With
        Me.Refresh()
        '
    End Sub

    Private Sub ButtonTecladoCentro_Click(sender As Object, e As EventArgs) Handles ButtonTecladoCentro.Click
        '
        ' Teclado Centrado
        '
        CentraMe()
        '
    End Sub

    Private Sub ButtonTecladoAbajo_Click(sender As Object, e As EventArgs) Handles ButtonTecladoAbajo.Click
        '
        ' Teclado Arriba
        '
        With wrTecladoFlotante
            Do While Me.Top < (((.Top + .height) - Me.Height) - 30)
                Me.Top += 30
                Threading.Thread.Sleep(TimeLapse)
            Loop
            Me.Top = (.Top + .height) - Me.Height
        End With
        Me.Refresh()
        '
    End Sub

    Private Sub ButtonTecladoDerecha_Click(sender As Object, e As EventArgs) Handles ButtonTecladoDerecha.Click
        '
        ' Teclado Izquierda
        '
        With wrTecladoFlotante
            Do While Me.Left < (((.Left + .width) - Me.Width) - 30)
                Me.Left += 30
                Threading.Thread.Sleep(TimeLapse)
            Loop
            Me.Left = (.Left + .width) - Me.Width
        End With
        Me.Refresh()
        '
    End Sub

    Private Sub ButtonTecladoIzquierda_Click(sender As Object, e As EventArgs) Handles ButtonTecladoIzquierda.Click
        '
        ' Teclado Izquierda
        '
        With wrTecladoFlotante
            Do While Me.Left > .Left + 30
                Me.Left -= 30
                Threading.Thread.Sleep(TimeLapse)
            Loop
            Me.Left = .Left
        End With
        Me.Refresh()
        '
    End Sub

    Private Sub MiraFoco(wBtnCal As Button)
        '
        ' Determinamos el Visor que esta recibiendo el FOCO.
        '
        With wrTecladoFlotante
            If .CadenaVisor.Length <= (.MaxChar - 1) Then
                If TimerMensaje.Enabled = False Then
                    '
                    ' Tratamiento especial para &, (&&)
                    '
                    If wBtnCal.Text = "&&" Then
                        .CadenaVisor &= "&"
                    Else
                        .CadenaVisor &= wBtnCal.Text
                    End If
                Else
                    '
                    ' Mensaje Temporizado al Visor
                    '
                    MensaTempoVisor(1)
                End If
            Else
                '
                ' Mensaje Temporizado al Visor
                '
                MensaTempoVisor(0)
            End If
            Select Case .Tipo
                Case 0
                    '
                    ' Teclado NUMERICO
                    '
                    VisorTeclado.Text = .CadenaVisor
                Case 1, 2
                    '
                    ' Teclado ALFA/NUMERICO
                    '
                    VisorTecladoALFA.Text = .CadenaVisor
            End Select
        End With
        '
    End Sub

    Private Sub MensaTempoVisor(mOpc As Integer)
        '
        ' Mensajes Temporizados al Visor
        '
        If mOpc = 1 Then
            With wrTecladoFlotante
                .CadenaVisor = .MaxChar.ToString & " Caracteres ٩(͡๏̯͡๏)۶ (No!)"
            End With
            Exit Sub
        End If
        '
        With wrTecladoFlotante
            MiMensaSegu = .CadenaVisor
            MiContador = 0
            TimerMensaLapse = 2
            TimerMensaje.Enabled = True
            .CadenaVisor = "*Máximo " & .MaxChar.ToString & " Caracteres*"
            Select Case .Tipo
                Case 0
                    '
                    ' Teclado NUMERICO
                    '
                    VisorTeclado.Text = .CadenaVisor
                Case 1, 2
                    '
                    ' Teclado ALFA/NUMERICO
                    '
                    VisorTecladoALFA.Text = .CadenaVisor
            End Select
        End With
        '
    End Sub

    Private Sub TimerMensaje_Tick(sender As Object, e As EventArgs) Handles TimerMensaje.Tick
        '
        ' Timer
        '
        MiContador += 1
        If MiContador > TimerMensaLapse Then
            MiContador = 0
            With wrTecladoFlotante
                .CadenaVisor = MiMensaSegu
                Select Case .Tipo
                    Case 0
                        '
                        ' Teclado NUMERICO
                        '
                        VisorTeclado.Text = .CadenaVisor
                    Case 1, 2
                        '
                        ' Teclado ALFA/NUMERICO
                        '
                        VisorTecladoALFA.Text = .CadenaVisor
                End Select
            End With
            TimerMensaje.Enabled = False
        End If
        '
    End Sub


    Private Sub ButtonClr_Click(sender As Object, e As EventArgs) Handles ButtonClr.Click
        wrTecladoFlotante.CadenaVisor = ""
        VisorTeclado.Text = ""
    End Sub

    Private Sub ButtonLetraCLR_Click(sender As Object, e As EventArgs) Handles ButtonLetrACLR.Click
        wrTecladoFlotante.CadenaVisor = ""
        VisorTecladoALFA.Text = ""
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

    Private Sub ButtonLetrASPACE_Click(sender As Object, e As EventArgs) Handles ButtonLetrASPACE.Click
        wrTecladoFlotante.CadenaVisor &= " "
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

    Private Sub ButtonSet_Click(sender As Object, e As EventArgs) Handles ButtonSet.Click
        '
        ' Almacena un Favorito...
        '
        With wrTecladoFlotante
            Select Case .CodigoInterno
                Case 1
                    TextBoxMensaFAV1.Text = .CadenaVisor
                Case 2
                    TextBoxMensaFAV2.Text = .CadenaVisor
                Case 3
                    TextBoxMensaFAV3.Text = .CadenaVisor
                Case 4
                    TextBoxMensaFAV4.Text = .CadenaVisor
                Case 5
                    TextBoxMensaFAV5.Text = .CadenaVisor
            End Select
        End With
        '
        ' Guardamos las Frases Favoritas en la DB.
        '
        Actualiza_TCONA4(wCaja, "Teclado")
        '
    End Sub

    Private Sub ButtonArriTxt_Click(sender As Object, e As EventArgs) Handles ButtonArriTxt.Click
        '
        ' Tecla Arriba, Controla el Codigo de retorno y el Foco del control TXT actual
        '
        With wrTecladoFlotante
            .CodigoInterno -= 1
            Select Case .CodigoInterno
                Case < 1
                    .CodigoInterno = 5
                    TextBoxMensaFAV5.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV5.Top
                Case 1
                    TextBoxMensaFAV1.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV1.Top
                Case 2
                    TextBoxMensaFAV2.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV2.Top
                Case 3
                    TextBoxMensaFAV3.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV3.Top
                Case 4
                    TextBoxMensaFAV4.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV4.Top
                Case 5
                    TextBoxMensaFAV5.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV5.Top
            End Select
        End With
        '
    End Sub

    Private Sub ButtonAbajTxt_Click(sender As Object, e As EventArgs) Handles ButtonAbajTxt.Click
        '
        ' Tecla Aabajo, Controla el Codigo de retorno y el Foco del control TXT actual
        '
        With wrTecladoFlotante
            .CodigoInterno += 1
            Select Case .CodigoInterno
                Case > 5
                    .CodigoInterno = 1
                    TextBoxMensaFAV1.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV1.Top
                Case 1
                    TextBoxMensaFAV1.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV1.Top
                Case 2
                    TextBoxMensaFAV2.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV2.Top
                Case 3
                    TextBoxMensaFAV3.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV3.Top
                Case 4
                    TextBoxMensaFAV4.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV4.Top
                Case 5
                    TextBoxMensaFAV5.Focus()
                    LabelIndicador.Top = TextBoxMensaFAV5.Top
            End Select
        End With
        '
    End Sub

    Private Sub ButtonAVisor_Click(sender As Object, e As EventArgs) Handles ButtonAVisor.Click
        '
        ' Recupera un Favorito...
        '
        With wrTecladoFlotante
            Select Case .CodigoInterno
                Case 1
                    .CadenaVisor = TextBoxMensaFAV1.Text.Trim
                Case 2
                    .CadenaVisor = TextBoxMensaFAV2.Text.Trim
                Case 3
                    .CadenaVisor = TextBoxMensaFAV3.Text.Trim
                Case 4
                    .CadenaVisor = TextBoxMensaFAV4.Text.Trim
                Case 5
                    .CadenaVisor = TextBoxMensaFAV5.Text.Trim
            End Select
            '
            VisorTecladoALFA.Text = .CadenaVisor
        End With
        '
    End Sub

    Private Sub ButtonLetrATras_Click(sender As Object, e As EventArgs) Handles ButtonLetrATras.Click
        '
        ' Quita Ultimo Carcter
        '
        Dim TextoTemp As String = ""
        With wrTecladoFlotante
            If .CadenaVisor.Length > 0 Then
                TextoTemp = .CadenaVisor
                .CadenaVisor = TextoTemp.Remove((.CadenaVisor.Length - 1), 1)
                VisorTecladoALFA.Text = .CadenaVisor
            End If
        End With
        '
    End Sub

    Private Sub ButtonLetrASim_Click(sender As Object, e As EventArgs) Handles ButtonLetrASim.Click
        '
        ' Teclado de Simbolos
        '
        SwSimbolos = Not SwSimbolos
        PanelTecladoSimbolos.Top = PanelTecladoALFA.Top + 80
        PanelTecladoSimbolos.Left = PanelTecladoALFA.Left
        Select Case SwSimbolos
            Case True
                PanelTecladoSimbolos.Visible = True
                ButtonLetrASim.BackColor = Color.Red
            Case False
                PanelTecladoSimbolos.Visible = False
                ButtonLetrASim.BackColor = Color.Salmon
        End Select
        '
    End Sub

End Class