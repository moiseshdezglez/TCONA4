Public Class TCONA409
    '
    Dim Mayus As Boolean = False

    Private Sub TCONA409_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' FORM Pedir VENDEDOR
        '
        Label2.Text = "POR FAVOR INTRODUZCA SUS CREDENCIALES (CLAVE, BIOMETRICAS, ETC...)"
        TextoVisorPassword = ""
        TextBoxPwdVend.Text = TextoVisorPassword
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Wingdings", 18, FontStyle.Bold)
        ButtonLetrAMinMay.Font = myFont
        CargaVendedores(0)
        GRIDVENDEDORES.ClearSelection()
        TextBoxPwdVend.Focus()
        '
    End Sub

    Private Sub ButtonCLR_Click(sender As Object, e As EventArgs) Handles ButtonCLR.Click
        '
        TextoVisorPassword = ""
        TextBoxPwdVend.Text = TextoVisorPassword
        GRIDVENDEDORES.ClearSelection()
        TextBoxPwdVend.Focus()
        '
    End Sub

    Private Sub ButtonLetrAMinMay_Click(sender As Object, e As EventArgs) Handles ButtonLetrAMinMay.Click
        '
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
        For Each wControl In Me.Controls
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

    Private Sub ButtonCal7_Click(sender As Object, e As EventArgs) _
        Handles ButtonCal7.Click, ButtonCalPunto.Click, ButtonCalPComa.Click, ButtonCalGuion.Click,
        ButtonCalComma.Click, ButtonCalAsterisco.Click, ButtonCal9.Click, ButtonCal8.Click, ButtonCal6.Click,
        ButtonCal5.Click, ButtonCal4.Click, ButtonCal3.Click, ButtonCal2.Click, ButtonCal1.Click,
        ButtonCal00.Click, ButtonCal0.Click
        '
        ' Botones Numéricos (Calculadora)
        '
        TextoVisorPassword &= CType(sender, Button).Text
        TextBoxPwdVend.Text = TextoVisorPassword
        '
    End Sub

    Private Sub ButtonLetraA_Click(sender As Object, e As EventArgs) _
        Handles ButtonLetraA.Click, ButtonLetraZ.Click, ButtonLetraY.Click, ButtonLetraX.Click,
        ButtonLetraW.Click, ButtonLetraV.Click, ButtonLetraU.Click, ButtonLetraT.Click, ButtonLetraS.Click,
        ButtonLetraR.Click, ButtonLetraQ.Click, ButtonLetraP.Click, ButtonLetraO.Click, ButtonLetraN.Click,
        ButtonLetraM.Click, ButtonLetraL.Click, ButtonLetraK.Click, ButtonLetraJ.Click, ButtonLetraI.Click,
        ButtonLetraH.Click, ButtonLetraG.Click, ButtonLetraF.Click, ButtonLetraEnie.Click, ButtonLetraE.Click,
        ButtonLetraD.Click, ButtonLetraC.Click, ButtonLetraB.Click
        '
        ' Botones Alfabeticos (A - Z), (a - z)
        '
        TextoVisorPassword &= CType(sender, Button).Text
        TextBoxPwdVend.Text = TextoVisorPassword
        '
    End Sub

    Private Sub GRIDVENDEDORES_Click(sender As Object, e As EventArgs) Handles GRIDVENDEDORES.Click
        '
        ' Seleccion de un vendedor en la lista.
        '
        If GRIDVENDEDORES.SelectedRows.Count > 0 Then
            If GRIDVENDEDORES.SelectedCells(2).Value.ToString.Trim.Length = 0 Then
                Select Case FormularioInicial
                    Case 0
                        MyFrm1.TextBoxOPC1.Text = GRIDVENDEDORES.SelectedCells(0).Value.ToString.Trim
                        If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
                            MyFrm1.TextBoxNomCamarero.Text = wrLeeCODNOM.NOMBRE
                            MyFrm2.LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE
                        Else
                            MyFrm1.TextBoxNomCamarero.Text = " No leido!"
                            MyFrm2.LabelNomCamarero.Text = " No leido!"
                        End If
                        MiraNivelAcceso()
                    Case 1
                        MyFrm1.TextBoxOPC1.Text = GRIDVENDEDORES.SelectedCells(0).Value.ToString.Trim
                        MyFrm2.TextBoxCamarero.Text = GRIDVENDEDORES.SelectedCells(0).Value.ToString.Trim
                        If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
                            MyFrm1.TextBoxNomCamarero.Text = wrLeeCODNOM.NOMBRE
                            MyFrm2.LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE
                        Else
                            MyFrm1.TextBoxNomCamarero.Text = " No leido!"
                            MyFrm2.LabelNomCamarero.Text = " No leido!"
                        End If
                End Select
                Me.Hide()
                MyFrm1.Focus()
            Else
                Me.TextBoxPwdVend.Focus()
            End If
        End If
        '
    End Sub

    Private Sub MiraNivelAcceso()
        '
        ' Comprobamos el NIVEL que indica Vendedores (0 a 5). Tabla [CLAVES]
        ' Basicamente aqui para saber si activamos Botón CABECERA o NO.
        '
        If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
        Else
            wrLeeCODNOM.NIVELACCESO = 0
        End If
        '
        ' Niveles 5 a 3 : Activan Boton CABECERA.
        '  por defecto.
        '
        If wrLeeCODNOM.NIVELACCESO > 2 Then
            MyFrm1.ButtonCabecera.Enabled = True
        Else
            MyFrm1.ButtonCabecera.Enabled = False
        End If
        '
    End Sub

    Private Sub TCONA409_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        TextBoxPwdVend.Focus()
    End Sub

    Private Sub ButtonEnter_Click(sender As Object, e As EventArgs) Handles ButtonEnter.Click
        '
        ' Validación CREDENCIALES del VENDEDOR.
        '
        LocalizaClaveVendedor(1)
        '
    End Sub

    Private Sub LocalizaClaveVendedor(wLocOpc As Integer)
        '
        ' Localizar CREDENCIALES del VENDEDOR.
        ' wLocOpc :: 
        '    0 = Solo localiza y marca la fila en el GRID.
        '    1 = Localiza, marca y sale del FORMULARIO.
        '
        If Me.TextBoxPwdVend.Text.Trim.Length > 0 Then
            With GRIDVENDEDORES
                If .Rows.Count > 0 Then
                    .Visible = False
                    For Each row As DataGridViewRow In .Rows
                        '
                        ' Contrastamos la PWD
                        '
                        If row.Cells(2).Value.ToString.Trim = TextBoxPwdVend.Text.Trim Then
                            row.Selected = True
                            .CurrentCell = row.Cells(1)
                            Select Case wLocOpc
                                Case 1
                                    Select Case FormularioInicial
                                        Case 0
                                            MyFrm1.TextBoxOPC1.Text = GRIDVENDEDORES.SelectedCells(0).Value.ToString.Trim
                                            If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
                                                MyFrm1.TextBoxNomCamarero.Text = wrLeeCODNOM.NOMBRE
                                                MyFrm2.LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE
                                            Else
                                                MyFrm1.TextBoxNomCamarero.Text = " No leido!"
                                                MyFrm2.LabelNomCamarero.Text = " No leido!"
                                            End If
                                            MiraNivelAcceso()
                                        Case 1
                                            MyFrm1.TextBoxOPC1.Text = GRIDVENDEDORES.SelectedCells(0).Value.ToString.Trim
                                            MyFrm2.TextBoxCamarero.Text = GRIDVENDEDORES.SelectedCells(0).Value.ToString.Trim
                                            If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
                                                MyFrm1.TextBoxNomCamarero.Text = wrLeeCODNOM.NOMBRE
                                                MyFrm2.LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE
                                            Else
                                                MyFrm1.TextBoxNomCamarero.Text = " No leido!"
                                                MyFrm2.LabelNomCamarero.Text = " No leido!"
                                            End If
                                    End Select
                                    Me.Hide()
                                    MyFrm1.Focus()
                            End Select
                        End If
                    Next
                    .Visible = True
                End If
            End With
        End If
        '
    End Sub

    Private Sub TextBoxPwdVend_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPwdVend.TextChanged
        '
        ' Localizacion del Vendedor de forma interactiva...
        '
        LocalizaClaveVendedor(0)
        '
    End Sub

End Class