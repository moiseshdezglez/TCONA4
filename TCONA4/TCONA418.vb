Public Class TCONA418
    Private Sub TCONA418_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        ' Cuando este FORM se activa
        '
        With wrTecladoFlotante
            If TCONA418_Started = False Then
                TCONA418_Started = True
                '
                CargaListaTKFavoritos()
                '
                ' Acciones al Entrar y Activarse.
                '
                TextBoxMensaL1.Text = ""
                TextBoxMensaL2.Text = ""
                TextBoxMensaL3.Text = ""
                TextBoxMensaL4.Text = ""
                TextBoxMensaL1.Focus()
                .CodigoRetorno = 1
                .MensaUsuario = ""
                LabelIndicador.Top = TextBoxMensaL1.Top
            Else
                '
                ' Acciones Cuando se mantiene Visible y se re-activa.
                ' Recogemos Texto desde el teclado...
                '
                Select Case .CodigoRetorno
                    Case 1
                        TextBoxMensaL1.Text = .CadenaVisor
                        LabelIndicador.Top = TextBoxMensaL1.Top
                    Case 2
                        TextBoxMensaL2.Text = .CadenaVisor
                        LabelIndicador.Top = TextBoxMensaL2.Top
                    Case 3
                        TextBoxMensaL3.Text = .CadenaVisor
                        LabelIndicador.Top = TextBoxMensaL3.Top
                    Case 4
                        TextBoxMensaL4.Text = .CadenaVisor
                        LabelIndicador.Top = TextBoxMensaL4.Top
                End Select
            End If
        End With
        '
    End Sub

    Private Sub TCONA418_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA418_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Carga del Form
        '
        PreparaGRIDs()
        CargaListaAREAS(wCaja)
        '
    End Sub

    Private Sub PreparaGRIDs()
        '
        ' Define Algunas Propiedades de los GIRDs
        '
        With GRIDAREAS1
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
        With GRIDTEXTOSPREDEF
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
    End Sub


    Private Sub Button18Salir_Click(sender As Object, e As EventArgs) Handles Button18Salir.Click
        Me.Hide()
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDAREAS1
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 > 0 Then
                    CursorGRID1 -= 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(2)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonGRIDAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDAREAS1
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                Dim GrNumRows As Integer = .Rows.Count - 1
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 < GrNumRows Then
                    CursorGRID1 += 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(2)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonArriTxt_Click(sender As Object, e As EventArgs) Handles ButtonArriTxt.Click
        '
        ' Tecla Arriba, Controla el Codigo de retorno y el Foco del control TXT actual
        '
        With wrTecladoFlotante
            .CodigoRetorno -= 1
            Select Case .CodigoRetorno
                Case < 1
                    .CodigoRetorno = 4
                    TextBoxMensaL4.Focus()
                    LabelIndicador.Top = TextBoxMensaL4.Top
                Case 1
                    TextBoxMensaL1.Focus()
                    LabelIndicador.Top = TextBoxMensaL1.Top
                Case 2
                    TextBoxMensaL2.Focus()
                    LabelIndicador.Top = TextBoxMensaL2.Top
                Case 3
                    TextBoxMensaL3.Focus()
                    LabelIndicador.Top = TextBoxMensaL3.Top
                Case 4
                    TextBoxMensaL4.Focus()
                    LabelIndicador.Top = TextBoxMensaL4.Top
            End Select
        End With
        '
    End Sub

    Private Sub ButtonAbajTxt_Click(sender As Object, e As EventArgs) Handles ButtonAbajTxt.Click
        '
        ' Tecla Aabajo, Controla el Codigo de retorno y el Foco del control TXT actual
        '
        With wrTecladoFlotante
            .CodigoRetorno += 1
            Select Case .CodigoRetorno
                Case > 4
                    .CodigoRetorno = 1
                    TextBoxMensaL1.Focus()
                    LabelIndicador.Top = TextBoxMensaL1.Top
                Case 1
                    TextBoxMensaL1.Focus()
                    LabelIndicador.Top = TextBoxMensaL1.Top
                Case 2
                    TextBoxMensaL2.Focus()
                    LabelIndicador.Top = TextBoxMensaL2.Top
                Case 3
                    TextBoxMensaL3.Focus()
                    LabelIndicador.Top = TextBoxMensaL3.Top
                Case 4
                    TextBoxMensaL4.Focus()
                    LabelIndicador.Top = TextBoxMensaL4.Top
            End Select
        End With
        '
    End Sub

    Private Sub Button18Cls_Click(sender As Object, e As EventArgs) Handles Button18Cls.Click
        '
        TextBoxMensaL1.Text = ""
        TextBoxMensaL2.Text = ""
        TextBoxMensaL3.Text = ""
        TextBoxMensaL4.Text = ""
        wrTecladoFlotante.CodigoRetorno = 1
        TextBoxMensaL1.Focus()
        LabelIndicador.Top = TextBoxMensaL1.Top
        '
    End Sub

    Private Sub ButtonTeclado_Click(sender As Object, e As EventArgs) Handles ButtonTeclado.Click
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
            '
            ' Mensaje al usuario
            '
            .MensaUsuario = "Por favor digite un texto."
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
            .MaxChar = 40
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

    Private Sub ButtonGrabaTextos_Click(sender As Object, e As EventArgs) Handles ButtonGrabaTextos.Click
        '
        ' Grabar Textos Favoritos...
        '
        '
        GrabaTkFavoritos()
        CargaListaTKFavoritos()
        '
    End Sub

    Private Sub ButtonGrdArri_Click(sender As Object, e As EventArgs) Handles ButtonGrdArri.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDTEXTOSPREDEF
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

    Private Sub ButtonGrdAbaj_Click(sender As Object, e As EventArgs) Handles ButtonGrdAbaj.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDTEXTOSPREDEF
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                Dim GrNumRows As Integer = .Rows.Count - 1
                CursorGRID1 = .CurrentCell.RowIndex
                '
                If CursorGRID1 < GrNumRows Then
                    CursorGRID1 += 1
                    .CurrentCell = .Rows(CursorGRID1).Cells(2)
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonSelTextos_Click(sender As Object, e As EventArgs) Handles ButtonSelTextos.Click
        '
        If GRIDTEXTOSPREDEF.SelectedRows.Count > 0 Then
            TextBoxMensaL1.Text = GRIDTEXTOSPREDEF.SelectedCells(0).Value.ToString
            TextBoxMensaL2.Text = GRIDTEXTOSPREDEF.SelectedCells(1).Value.ToString
            TextBoxMensaL3.Text = GRIDTEXTOSPREDEF.SelectedCells(2).Value.ToString
            TextBoxMensaL4.Text = GRIDTEXTOSPREDEF.SelectedCells(3).Value.ToString
        End If
        '
    End Sub

    Private Sub ButtonBorraTextos_Click(sender As Object, e As EventArgs) Handles ButtonBorraTextos.Click
        '
        ' Borra Frase Favoritas
        '
        If GRIDTEXTOSPREDEF.SelectedRows.Count > 0 Then
            msg = "¿Desea Borrar este texto?" & vbCrLf
            msg &= "[" & GRIDTEXTOSPREDEF.SelectedCells(4).Value.ToString & "]" & vbCrLf
            msg &= GRIDTEXTOSPREDEF.SelectedCells(0).Value.ToString & vbCrLf
            msg &= GRIDTEXTOSPREDEF.SelectedCells(1).Value.ToString & vbCrLf
            msg &= GRIDTEXTOSPREDEF.SelectedCells(2).Value.ToString & vbCrLf
            msg &= GRIDTEXTOSPREDEF.SelectedCells(3).Value.ToString & vbCrLf
            style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
            title = "Borrar Textos Favoritos."
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                BorraTKFavoritos(CInt(GRIDTEXTOSPREDEF.SelectedCells(4).Value.ToString))
                CargaListaTKFavoritos()
            End If
        End If
        '
    End Sub

    Private Sub GRIDAREAS1_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDAREAS1.SelectionChanged
        '
        If GRIDAREAS1.SelectedRows.Count > 0 Then
            LabelNomArea.Text = GRIDAREAS1.SelectedCells(1).Value.ToString
        End If
        '
    End Sub

    Private Sub GRIDTEXTOSPREDEF_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDTEXTOSPREDEF.SelectionChanged
        '
        If GRIDTEXTOSPREDEF.SelectedRows.Count > 0 Then
            TextBoxMensaL1.Text = GRIDTEXTOSPREDEF.SelectedCells(0).Value.ToString
            TextBoxMensaL2.Text = GRIDTEXTOSPREDEF.SelectedCells(1).Value.ToString
            TextBoxMensaL3.Text = GRIDTEXTOSPREDEF.SelectedCells(2).Value.ToString
            TextBoxMensaL4.Text = GRIDTEXTOSPREDEF.SelectedCells(3).Value.ToString
        End If
        '
    End Sub

    Private Sub ButtonENviaMensaje_Click(sender As Object, e As EventArgs) Handles ButtonENviaMensaje.Click
        '
        If GRIDAREAS1.SelectedRows.Count > 0 Then
            MENSAJESAImpresora(GRIDAREAS1.SelectedCells(0).Value.ToString)
        End If
        '
    End Sub

    Private Sub ButtonTerminarMESA_Click(sender As Object, e As EventArgs) Handles ButtonTerminarMESA.Click
        '
        ' Terminar MESA
        '
        If GRIDAREAS1.SelectedRows.Count > 0 Then
            MENSAJESATerminarMESA(GRIDAREAS1.SelectedCells(0).Value.ToString, Me.ButtonTerminarMESA.Text.Trim)
        End If
        '
    End Sub
End Class