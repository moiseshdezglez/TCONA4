Imports System.Data.SqlClient

Public Class TCONA417
    Private Sub TCONA417_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Button17Salir_Click(sender As Object, e As EventArgs) Handles Button17Salir.Click
        Me.Hide()
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDMIMP
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
        With GRIDMIMP
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

    Private Sub GRIDMIMP_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDMIMP.SelectionChanged
        '
        If Me.GRIDMIMP.SelectedRows.Count > 0 Then
            TextBoxNomModelo.Text = GRIDMIMP.SelectedCells(0).Value.ToString
            MuestraDatosImpresora(GRIDMIMP.SelectedCells(0).Value.ToString.Trim)
        End If
        '
    End Sub

    Private Sub GRIDMIMP_Click(sender As Object, e As EventArgs) Handles GRIDMIMP.Click
        '
        If Me.GRIDMIMP.SelectedRows.Count > 0 Then
            TextBoxNomModelo.Text = GRIDMIMP.SelectedCells(0).Value.ToString
            MuestraDatosImpresora(GRIDMIMP.SelectedCells(0).Value.ToString.Trim)
        End If
        '
    End Sub

    Private Sub MuestraDatosImpresora(wPrinter As String)
        '
        TextCorteUSR.Text = ""
        TextCajonUSR.Text = ""
        Text10Cpp.Text = ""
        TextDblAlto.Text = ""
        TextDblAlto12Cpp.Text = ""
        TextDblAncho.Text = ""
        TextAvance.Text = ""
        Text12Cpp.Text = ""
        TextProporcional.Text = ""
        TextCompri.Text = ""
        TextNegrita.Text = ""
        TextCursiva.Text = ""
        '
        LeeDatosImpresora(wPrinter.Trim)
        With wrIMPRESORA
            LblCorte.Text = .CORTE.Trim
            LblCajon.Text = .ABRECAJON.Trim
            Lbl10Cpp.Text = .DIEZ_CPP.Trim
            LblDblAlto.Text = .DOBLEALTO.Trim
            LblDblAlto12Cpp.Text = .DOBLEALTO12CPP.Trim
            LblDblAncho.Text = .DOBLEANCHO.Trim
            LblAvance.Text = .AVAZALINEA.Trim
            Lbl12Cpp.Text = .DOCECPP
            LblProporcional.Text = .PROPORCIONAL
            LblCompri.Text = .COMPRIMIDO
            LblNegrita.Text = .NEGRITA
            LblCursiva.Text = .CURSIVA
            '
            ' Obtener Cadenas "/nn/nn ... /nn" a partir de ESC/POS
            '
            TextCorteUSR.Text = DameESCPOS_ConcatBarras(.CORTE)
            TextCajonUSR.Text = DameESCPOS_ConcatBarras(.ABRECAJON)
            Text10Cpp.Text = DameESCPOS_ConcatBarras(.DIEZ_CPP)
            TextDblAlto.Text = DameESCPOS_ConcatBarras(.DOBLEALTO)
            TextDblAlto12Cpp.Text = DameESCPOS_ConcatBarras(.DOBLEALTO12CPP)
            TextDblAncho.Text = DameESCPOS_ConcatBarras(.DOBLEANCHO)
            TextAvance.Text = DameESCPOS_ConcatBarras(.AVAZALINEA)
            Text12Cpp.Text = DameESCPOS_ConcatBarras(.DOCECPP)
            TextProporcional.Text = DameESCPOS_ConcatBarras(.PROPORCIONAL)
            TextCompri.Text = DameESCPOS_ConcatBarras(.COMPRIMIDO)
            TextNegrita.Text = DameESCPOS_ConcatBarras(.NEGRITA)
            TextCursiva.Text = DameESCPOS_ConcatBarras(.CURSIVA)
            '
        End With
        '
    End Sub

    Private Sub ButtonESCPOSDefecto_Click(sender As Object, e As EventArgs) Handles ButtonESCPOSDefecto.Click
        '
        ' Establecer Los Códigos ESC/POS por Defecto
        '
        TextCorteUSR.Text = "/27/105"
        TextCajonUSR.Text = "/27/112/48/10/50"
        Text10Cpp.Text = "/27/33/0"
        Text12Cpp.Text = "/27/33/1"
        TextProporcional.Text = "/27/33/2"
        TextCompri.Text = "/27/33/4"
        TextNegrita.Text = "/27/33/8"
        TextCursiva.Text = "/27/33/64"
        TextDblAlto.Text = "/27/33/16"
        TextDblAlto12Cpp.Text = "/27/33/17"
        TextDblAncho.Text = "/27/33/32"
        TextAvance.Text = "/10"
        '
        Dim micadena As String = ""
        '
        With wrIMPRESORA
            '
            ' /nn a Concat. chr(n)
            '
            micadena = DameConcatChr(TextCorteUSR.Text)
            .CORTE = micadena
            micadena = DameConcatChr(TextCajonUSR.Text)
            .ABRECAJON = micadena
            micadena = DameConcatChr(Text10Cpp.Text)
            .DIEZ_CPP = micadena
            micadena = DameConcatChr(TextDblAlto.Text)
            .DOBLEALTO = micadena
            micadena = DameConcatChr(TextDblAlto12Cpp.Text)
            .DOBLEALTO12CPP = micadena
            micadena = DameConcatChr(TextDblAncho.Text)
            .DOBLEANCHO = micadena
            micadena = DameConcatChr(TextAvance.Text)
            .AVAZALINEA = micadena
            micadena = DameConcatChr(Text12Cpp.Text)
            .DOCECPP = micadena
            micadena = DameConcatChr(TextProporcional.Text)
            .PROPORCIONAL = micadena
            micadena = DameConcatChr(TextCompri.Text)
            .COMPRIMIDO = micadena
            micadena = DameConcatChr(TextNegrita.Text)
            .NEGRITA = micadena
            micadena = DameConcatChr(TextCursiva.Text)
            .CURSIVA = micadena
            '
            LblCorte.Text = .CORTE
            LblCajon.Text = .ABRECAJON
            Lbl10Cpp.Text = .DIEZ_CPP
            LblDblAlto.Text = .DOBLEALTO
            LblDblAlto12Cpp.Text = .DOBLEALTO12CPP
            LblDblAncho.Text = .DOBLEANCHO
            LblAvance.Text = .AVAZALINEA
            Lbl12Cpp.Text = .DOCECPP
            LblProporcional.Text = .PROPORCIONAL
            LblCompri.Text = .COMPRIMIDO
            LblNegrita.Text = .NEGRITA
            LblCursiva.Text = .CURSIVA
        End With
        '
    End Sub

    Private Sub ButtonESCPOSClear_Click(sender As Object, e As EventArgs) Handles ButtonESCPOSClear.Click
        '
        ButtonCreaActuMODELO.Enabled = False
        TextCorteUSR.Text = ""
        TextCajonUSR.Text = ""
        Text10Cpp.Text = ""
        TextDblAlto.Text = ""
        TextDblAlto12Cpp.Text = ""
        TextDblAncho.Text = ""
        TextAvance.Text = ""
        Text12Cpp.Text = ""
        TextProporcional.Text = ""
        TextCompri.Text = ""
        TextNegrita.Text = ""
        TextCursiva.Text = ""
        '
        LblCorte.Text = ""
        LblCajon.Text = ""
        Lbl10Cpp.Text = ""
        LblDblAlto.Text = ""
        LblDblAlto12Cpp.Text = ""
        LblDblAncho.Text = ""
        LblAvance.Text = ""
        Lbl12Cpp.Text = ""
        LblProporcional.Text = ""
        LblCompri.Text = ""
        LblNegrita.Text = ""
        LblCursiva.Text = ""
        '
    End Sub

    Private Sub ButtonVerificaESCPOS_Click(sender As Object, e As EventArgs) Handles ButtonVerificaESCPOS.Click
        '
        ' Establecer Los Códigos ESC/POS entrados por el USUARIO.
        ' Validación ...
        '
        HazVerificar()
        '
    End Sub

    Private Function HazVerificar() As Boolean
        '
        ' Verifica las cadenas ESC/POS entradas en las Cajas de texto.
        '
        HazVerificar = False
        '
        If ValidaESCPOS_Usr(TextCorteUSR.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextCajonUSR.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(Text10Cpp.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextDblAlto.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextDblAlto12Cpp.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextDblAncho.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextAvance.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(Text12Cpp.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextProporcional.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextCompri.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextNegrita.Text.Trim) = False Then
            Exit Function
        End If
        If ValidaESCPOS_Usr(TextCursiva.Text.Trim) = False Then
            Exit Function
        End If
        '
        Dim micadena As String = ""
        '
        With wrIMPRESORA
            '
            ' /nn a Concat. chr(n)
            '
            micadena = DameConcatChr(TextCorteUSR.Text)
            .CORTE = micadena
            micadena = DameConcatChr(TextCajonUSR.Text)
            .ABRECAJON = micadena
            micadena = DameConcatChr(Text10Cpp.Text)
            .DIEZ_CPP = micadena
            micadena = DameConcatChr(TextDblAlto.Text)
            .DOBLEALTO = micadena
            micadena = DameConcatChr(TextDblAlto12Cpp.Text)
            .DOBLEALTO12CPP = micadena
            micadena = DameConcatChr(TextDblAncho.Text)
            .DOBLEANCHO = micadena
            micadena = DameConcatChr(TextAvance.Text)
            .AVAZALINEA = micadena
            micadena = DameConcatChr(Text12Cpp.Text)
            .DOCECPP = micadena
            micadena = DameConcatChr(TextProporcional.Text)
            .PROPORCIONAL = micadena
            micadena = DameConcatChr(TextCompri.Text)
            .COMPRIMIDO = micadena
            micadena = DameConcatChr(TextNegrita.Text)
            .NEGRITA = micadena
            micadena = DameConcatChr(TextCursiva.Text)
            .CURSIVA = micadena
            '
            LblCorte.Text = .CORTE
            LblCajon.Text = .ABRECAJON
            Lbl10Cpp.Text = .DIEZ_CPP
            LblDblAlto.Text = .DOBLEALTO
            LblDblAlto12Cpp.Text = .DOBLEALTO12CPP
            LblDblAncho.Text = .DOBLEANCHO
            LblAvance.Text = .AVAZALINEA
            Lbl12Cpp.Text = .DOCECPP
            LblProporcional.Text = .PROPORCIONAL
            LblCompri.Text = .COMPRIMIDO
            LblNegrita.Text = .NEGRITA
            LblCursiva.Text = .CURSIVA
        End With
        '
        HazVerificar = True
        ButtonCreaActuMODELO.Enabled = True
        '
    End Function

    Private Sub TextCorteUSR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextCorteUSR.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextCajonUSR.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextCajonUSR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextCajonUSR.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    Text10Cpp.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub Text10Cpp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Text10Cpp.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    Text12Cpp.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextDblAlto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextDblAlto.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextDblAlto12Cpp.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextDblAlto12Cpp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextDblAlto12Cpp.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextDblAncho.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextDblAncho_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextDblAncho.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextAvance.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextAvance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextAvance.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    ButtonVerificaESCPOS.Select()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TCONA417_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Carga del FORM
        '
        CargaModelosImpresoras()
        '
    End Sub

    Private Sub ButtonCreaActuMODELO_Click(sender As Object, e As EventArgs) Handles ButtonCreaActuMODELO.Click
        '
        ' Crear / Actualizar un Modelo de impresora
        '
        If TextBoxNomModelo.Text.Trim.Length > 0 Then
            If HazVerificar() = True Then
                ActualizaDatosImpresora(TextBoxNomModelo.Text.Trim)
                CargaModelosImpresoras()
                ButtonCreaActuMODELO.Enabled = False
            Else
                ButtonCreaActuMODELO.Enabled = False
            End If
        End If
        '
    End Sub

    Private Sub ButtonBorraModelo_Click(sender As Object, e As EventArgs) Handles ButtonBorraModelo.Click
        '
        ' Borra la Impresora Actual
        '
        LeeTCONA4Cfg("General")
        If wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim = TextBoxNomModelo.Text.Trim Then
            msg = "La impresora que intenta BORRAR, es actualmente el " & vbCrLf
            msg &= "  modelo Fijado para trabajar con la aplicación. " & vbCrLf
            msg &= "Si desea Borrarlo, salga de este formulario y fije" & vbCrLf
            msg &= "  otro modelo de trabajo. " & vbCrLf
            msg &= "Luego regrese a este formulario e intente BORRAR" & vbCrLf
            msg &= "  de nuevo este Modelo." & vbCrLf
            style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly
            title = "Accion de borrado no permitida."   ' Define title.
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        If LeeDatosImpresora(TextBoxNomModelo.Text.Trim) = True Then
            BorrarModelo(TextBoxNomModelo.Text.Trim)
            ButtonCreaActuMODELO.Enabled = False
        End If
        '
    End Sub

    Private Sub BorrarModelo(sImpre As String)
        '
        '   Borrado de MODELOS impresora existentes.
        '
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        Dim VbResp = MsgBox("¿Desea Borrar esta IMPRESORA? .: " & vbCrLf &
          sImpre, style, "Borrar Registro!")
        '
        If VbResp = vbNo Then
            Exit Sub
        End If
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        queryString = "Delete [IMPRESORAS] WHERE "
        queryString &= "[IMPRESORA]='" & sImpre.Trim & "'"
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [IMPRESORAS], Borrando Registro.")
        End Try
        conexion.Close()
        CargaModelosImpresoras()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub Text12Cpp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Text12Cpp.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextProporcional.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextProporcional_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextProporcional.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextCompri.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextCompri_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextCompri.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextNegrita.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextCursiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextCursiva.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextDblAlto.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextNegrita_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextNegrita.KeyPress
        '
        Dim allowedChars As String = "0123456789/" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextCursiva.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextCorteUSR_KeyDown(sender As Object, e As KeyEventArgs) Handles TextCorteUSR.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxNomModelo.Focus()
            Case Keys.Enter, Keys.Down
                TextCajonUSR.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxNomModelo_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNomModelo.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Down
                TextCorteUSR.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextCajonUSR_KeyDown(sender As Object, e As KeyEventArgs) Handles TextCajonUSR.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextCorteUSR.Focus()
            Case Keys.Enter, Keys.Down
                Text10Cpp.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Text10Cpp_KeyDown(sender As Object, e As KeyEventArgs) Handles Text10Cpp.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextCajonUSR.Focus()
            Case Keys.Enter, Keys.Down
                Text12Cpp.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Text12Cpp_KeyDown(sender As Object, e As KeyEventArgs) Handles Text12Cpp.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                Text10Cpp.Focus()
            Case Keys.Enter, Keys.Down
                TextProporcional.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextProporcional_KeyDown(sender As Object, e As KeyEventArgs) Handles TextProporcional.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                Text12Cpp.Focus()
            Case Keys.Enter, Keys.Down
                TextCompri.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextCompri_KeyDown(sender As Object, e As KeyEventArgs) Handles TextCompri.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextProporcional.Focus()
            Case Keys.Enter, Keys.Down
                TextNegrita.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextNegrita_KeyDown(sender As Object, e As KeyEventArgs) Handles TextNegrita.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextCompri.Focus()
            Case Keys.Enter, Keys.Down
                TextCursiva.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextCursiva_KeyDown(sender As Object, e As KeyEventArgs) Handles TextCursiva.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextNegrita.Focus()
            Case Keys.Enter, Keys.Down
                TextDblAlto.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextDblAlto_KeyDown(sender As Object, e As KeyEventArgs) Handles TextDblAlto.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextCursiva.Focus()
            Case Keys.Enter, Keys.Down
                TextDblAlto12Cpp.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextDblAlto12Cpp_KeyDown(sender As Object, e As KeyEventArgs) Handles TextDblAlto12Cpp.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextDblAlto.Focus()
            Case Keys.Enter, Keys.Down
                TextDblAncho.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextDblAncho_KeyDown(sender As Object, e As KeyEventArgs) Handles TextDblAncho.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextDblAlto12Cpp.Focus()
            Case Keys.Enter, Keys.Down
                TextAvance.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextAvance_KeyDown(sender As Object, e As KeyEventArgs) Handles TextAvance.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextDblAncho.Focus()
            Case Keys.Enter, Keys.Down
                ButtonVerificaESCPOS.Select()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub ButtonModExport_Click(sender As Object, e As EventArgs) Handles ButtonModExport.Click
        '
        ' Exporta Modelo de Impresora y 
        ' Cadenas para generar códigos ESC/POS
        '
        If TextBoxNomModelo.Text.Trim.Length > 0 Then
            GeneraTXTMODELOIMPRE(TextBoxNomModelo.Text.Trim)
        Else
            MsgBox("Indique un Nombre de Modelo para exportar.",
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Information,
                   "Exportar datos.")
        End If
        '
    End Sub

    Private Sub ButtonModImport_Click(sender As Object, e As EventArgs) Handles ButtonModImport.Click
        '
        '   Abrir Ficheros con Datos ESC/POS
        '   Si pasa la verificación, se autogenera el MODELO.
        '
        With Me.OpenFileDialog1
            .Title = "Elija un Archivo TXT ..."
            .Multiselect = False
            .FileName = ""
            .FilterIndex = 1
            .Filter = "TXT files (*.txt)|"
            If (OpenFileDialog1.ShowDialog() = DialogResult.OK) Then
                CargaTXTMODELOIMPRE(.FileName)
                If HazVerificar() = True Then
                    If TextBoxNomModelo.Text.Trim.Length > 0 Then
                        ActualizaDatosImpresora(TextBoxNomModelo.Text.Trim)
                        CargaModelosImpresoras()
                        ButtonCreaActuMODELO.Enabled = False
                    End If
                End If
            End If
        End With
        '
    End Sub

End Class