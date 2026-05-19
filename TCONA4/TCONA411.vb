Public Class TCONA411

    Dim Panel1FecDH As Integer = 0
    Dim allowCoolMove As Boolean = False
    Dim myCoolPoint As New Point()
    Dim TFillLista As Integer = 0

    Private Sub TCONA411_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA411_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Consulta Movimientos Mesas, Carga del FORM.
        '
        PreparEntorno()
        '
    End Sub

    Private Sub PreparEntorno()
        '
        wTipoConsu = 3 ' x Articulos
        wTipoResuMES = 2 ' Lineas de Unid + Importe / Resu MES
        Panel1.Width = 188 : ButtonAcceptFecha.Width = 167
        Panel1.Visible = False : Panel3.Visible = False
        '
        With GRID1
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
        With GRID2
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
        '
        With GRID3
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
        With GRIDLISTAS
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

    Private Sub ButtonConsuSalir_Click(sender As Object, e As EventArgs) Handles ButtonConsuSalir.Click
        Me.Hide()
    End Sub

    Private Sub ButtonCDFEC_Click(sender As Object, e As EventArgs) Handles ButtonCDFEC.Click
        '
        ' Panel: Desde Fecha
        '
        Panel1FecDH = 0
        Panel3.Visible = False
        With Panel1
            .Top = ButtonCDFEC.Top
            .Left = ButtonCDFEC.Left + 40
            .Visible = True
        End With
        '
    End Sub

    Private Sub ButtonAcceptFecha_Click(sender As Object, e As EventArgs) Handles ButtonAcceptFecha.Click
        '
        ' Aceptar, Panel Fecha
        '
        Panel1.Visible = False
        Select Case Panel1FecDH
            Case 0
                TextBoxCDFEC.Text = CStr(Me.MonthCalendar1.SelectionRange.Start)
            Case 1
                TextBoxCHFEC.Text = CStr(Me.MonthCalendar1.SelectionRange.Start)
        End Select
        '
    End Sub

    Private Sub ButtonCHFEC_Click(sender As Object, e As EventArgs) Handles ButtonCHFEC.Click
        '
        ' Panel: Desde Fecha
        '
        Panel1FecDH = 1
        Panel3.Visible = False
        With Panel1
            .Top = ButtonCHFEC.Top
            .Left = ButtonCHFEC.Left + 40
            .Visible = True
        End With
        '
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        '
        ' Move Panel Fecha
        '
        allowCoolMove = True
        myCoolPoint = New Point(e.X, e.Y)
        Me.Cursor = Cursors.SizeAll
        '
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        '
        ' Move Panel Fecha
        '
        If allowCoolMove = True Then
            Panel1.Location = New Point(Panel1.Location.X + e.X - myCoolPoint.X, Panel1.Location.Y + e.Y - myCoolPoint.Y)
        End If
        '
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        '
        ' Move Panel Fecha
        '
        allowCoolMove = False
        Me.Cursor = Cursors.Default
        '
    End Sub

    Private Sub ButtonConsAcepta_Click(sender As Object, e As EventArgs) Handles ButtonConsAcepta.Click
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        HazConsulta()
        '
    End Sub

    Private Sub HazConsulta()
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        Dim MicadConex As String = ""
        '
        ' Validación Fechas
        '
        Dim MiFecDesde = TextBoxCDFEC.Text.Trim
        Dim MiFecHasta = TextBoxCHFEC.Text.Trim
        '
        msg = "Por favor entre fechas válidas. [DD/MM/AAAA] " & vbCrLf
        msg &= "[DESDE] < [HASTA]" & vbCrLf
        msg &= "Desde.: " & MiFecDesde.Trim & " // Hasta.: " & MiFecHasta.Trim
        '
        If ValidarFechasDH(MiFecDesde, MiFecHasta) = False Then
            style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly
            title = "Fecha(s) incorecta(s)"   ' Define title.
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        ' Validación Otros Datos
        '
        If TextBoxCCodArt.Text.Trim.Length = 0 Then
            TextBoxCCodArt.Text = "0"
        End If
        If TextBoxCCodVEN.Text.Trim.Length = 0 Then
            TextBoxCCodVEN.Text = "0"
        End If
        If TextBoxCNumMESA.Text.Trim.Length = 0 Then
            TextBoxCNumMESA.Text = "0"
        End If
        If TextBoxCNumSALA.Text.Trim.Length = 0 Then
            TextBoxCNumSALA.Text = "0"
        End If
        '
        MicadConex = "SELECT * FROM [MESAH] "
        MicadConex &= "WHERE [MESAH].[FECHA] Between '" & MiFecDesde.Trim & "' "
        MicadConex &= "AND '" & MiFecHasta.Trim & "' "
        If TextBoxCCodArt.Text.Trim.Length > 0 And TextBoxCCodArt.Text.Trim <> "0" Then
            MicadConex &= "AND [MESAH].[ARTI]='" & TextBoxCCodArt.Text.Trim & "' "
        End If
        If TextBoxCCodVEN.Text.Trim.Length > 0 And TextBoxCCodVEN.Text.Trim <> "0" Then
            MicadConex &= "AND [MESAH].[VENDEDOR]='" & TextBoxCCodVEN.Text.Trim & "' "
        End If
        If TextBoxCNumMESA.Text.Trim.Length > 0 And TextBoxCNumMESA.Text.Trim <> "0" Then
            MicadConex &= "AND [MESAH].[MESA]='" & TextBoxCNumMESA.Text.Trim & "' "
        End If
        If TextBoxCNumSALA.Text.Trim.Length > 0 And TextBoxCNumSALA.Text.Trim <> "0" Then
            MicadConex &= "AND [MESAH].[SALA]='" & TextBoxCNumSALA.Text.Trim & "' "
        End If
        '
        ConsGeneralMESAH(MicadConex)
        '
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        wTipoConsu = 0
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        wTipoConsu = 1 ' Vendedor
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        wTipoConsu = 2
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        wTipoConsu = 3 ' Articulos
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        wTipoConsu = 4 ' Familias
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        wTipoResuMES = 0
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        If SwINICIADO_412 And Me.RadioButton6.Focused Then
            HazConsulta()
        End If
        '
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        wTipoResuMES = 1
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        If SwINICIADO_412 And Me.RadioButton7.Focused Then
            HazConsulta()
        End If
        '
        '
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        wTipoResuMES = 2
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        '
        ' Consultar Movimientos de MESA / MESAH
        '
        If SwINICIADO_412 And Me.RadioButton8.Focused Then
            HazConsulta()
        End If
        '
    End Sub

    Private Sub ButtonTODoSArt_Click(sender As Object, e As EventArgs) Handles ButtonTODoSArt.Click
        TextBoxCCodArt.Text = "0"
        TextBoxCNomArt.Text = ""
    End Sub

    Private Sub ButtonTODoSVen_Click(sender As Object, e As EventArgs) Handles ButtonTODoSVen.Click
        TextBoxCCodVEN.Text = "0"
        TextBoxCNomVEN.Text = ""
    End Sub

    Private Sub ButtonTODASMesas_Click(sender As Object, e As EventArgs) Handles ButtonTODASMesas.Click
        TextBoxCNumMESA.Text = "0"
    End Sub

    Private Sub ButtonTODASSalas_Click(sender As Object, e As EventArgs) Handles ButtonTODASSalas.Click
        TextBoxCNumSALA.Text = "0"
    End Sub

    Private Sub ButtonCArti_Click(sender As Object, e As EventArgs) Handles ButtonCArti.Click
        '
        ' Panel: Localizar Articulos
        '
        TFillLista = 0
        TextBoxLocalizaNombre.Text = ""
        Panel1.Visible = False
        With Panel3
            .Top = 132
            .Left = 330
            .Visible = True
        End With
        CargaArticulos(1)
        '
    End Sub

    Private Sub ButtonCVEND_Click(sender As Object, e As EventArgs) Handles ButtonCVEND.Click
        '
        ' Panel: Localizar Vendedores
        '
        TFillLista = 1
        TextBoxLocalizaNombre.Text = ""
        Panel1.Visible = False
        With Panel3
            .Top = 132
            .Left = 330
            .Visible = True
        End With
        CargaVendedores(1)
        '
    End Sub

    Private Sub ButtonAceptaArtVen_Click(sender As Object, e As EventArgs) Handles ButtonAceptaArtVen.Click
        '
        ' Aceptar, Panel Lista Art. / Vendedores
        '
        Panel3.Visible = False
        If Me.GRIDLISTAS.SelectedRows.Count > 0 Then
            '
            Select Case TFillLista
                Case 0
                    TextBoxCCodArt.Text = GRIDLISTAS.SelectedCells(0).Value.ToString
                    TextBoxCNomArt.Text = GRIDLISTAS.SelectedCells(1).Value.ToString
                Case 1
                    TextBoxCCodVEN.Text = GRIDLISTAS.SelectedCells(0).Value.ToString
                    TextBoxCNomVEN.Text = GRIDLISTAS.SelectedCells(1).Value.ToString
            End Select
            '
        End If
        '
    End Sub

    Private Sub GRIDLISTAS_Click(sender As Object, e As EventArgs) Handles GRIDLISTAS.Click
        '
        If Me.GRIDLISTAS.SelectedRows.Count > 0 Then
            '
            Select Case TFillLista
                Case 0
                    TextBoxCCodArt.Text = GRIDLISTAS.SelectedCells(0).Value.ToString
                    TextBoxCNomArt.Text = GRIDLISTAS.SelectedCells(1).Value.ToString
                Case 1
                    TextBoxCCodVEN.Text = GRIDLISTAS.SelectedCells(0).Value.ToString
                    TextBoxCNomVEN.Text = GRIDLISTAS.SelectedCells(1).Value.ToString
            End Select
            '
        End If
        '
    End Sub

    Private Sub GRIDLISTAS_DoubleClick(sender As Object, e As EventArgs) Handles GRIDLISTAS.DoubleClick
        '
        ' Aceptar, Panel Lista Art. / Vendedores
        '
        Panel3.Visible = False
        If Me.GRIDLISTAS.SelectedRows.Count > 0 Then
            '
            Select Case TFillLista
                Case 0
                    TextBoxCCodArt.Text = GRIDLISTAS.SelectedCells(0).Value.ToString
                    TextBoxCNomArt.Text = GRIDLISTAS.SelectedCells(1).Value.ToString
                Case 1
                    TextBoxCCodVEN.Text = GRIDLISTAS.SelectedCells(0).Value.ToString
                    TextBoxCNomVEN.Text = GRIDLISTAS.SelectedCells(1).Value.ToString
            End Select
            '
        End If
        '
    End Sub

    Private Sub TextBoxLocalizaNombre_TextChanged(sender As Object, e As EventArgs) Handles TextBoxLocalizaNombre.TextChanged
        '
        ' Localizar por nombre
        '
        If TextBoxLocalizaNombre.Text.Trim.Length = 0 Then
            Select Case TFillLista
                Case 0
                    CargaArticulos(1)
                Case 1
                    CargaVendedores(1)
            End Select
            Exit Sub
        End If
        '
        ' Lista Filtrada Por Nombre ...
        '
        Select Case TFillLista
            Case 0
                CargaArticulos(2)
            Case 1
                CargaVendedores(2)
        End Select
        '
    End Sub

    Private Sub ButtonImpre1_Click(sender As Object, e As EventArgs) Handles ButtonImpre1.Click
        If GRID1.Rows.Count > 0 Then
            Me.ButtonImpre1.Enabled = False
            ImprimeInformesConsulta(0)
        End If
    End Sub

    Private Sub ButtonImpre2_Click(sender As Object, e As EventArgs) Handles ButtonImpre2.Click
        If GRID2.Rows.Count > 0 Then
            Me.ButtonImpre2.Enabled = False
            ImprimeInformesConsulta(1)
        End If
    End Sub

    Private Sub ButtonImpre3_Click(sender As Object, e As EventArgs) Handles ButtonImpre3.Click
        If GRID3.Rows.Count > 0 Then
            Me.ButtonImpre3.Enabled = False
            ImprimeInformesConsulta(2)
        End If
    End Sub

    Private Sub TCONA411_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        SwINICIADO_412 = True
        '
    End Sub
End Class