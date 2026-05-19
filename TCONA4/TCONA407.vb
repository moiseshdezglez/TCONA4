Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class TCONA407
    Dim GrupoPreSeleccion As String = ""
    Dim wMant As Integer = 0
    Dim wTopeFavoritos As Boolean = False
    Dim wGTipoFav As String = "BEBIDAS"
    Private Sub TCONA407_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                If wMant = 1 Then
                    wMant = 0
                    ArenaFavoritos(True)
                    Exit Sub
                End If
                '
                Select Case BtnAccion
                    Case 0
                        Me.Hide()
                    Case 1 To 2
                        CancelaNuevoGrupo()
                        GRIDGrupos.Focus()
                    Case 11
                        CancelaNuevoGrupo()
                        CancelaNuevoArt()
                        GRID1.Focus()
                    Case 111, 222
                        CancelaNuevoGrupo()
                        CancelaNuevoArt()
                        CancelaNuevoGf()
                        GRID2.Focus()
                End Select
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA407_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        PreparEntorno()
        '
        ' GRUPOS // Productos y Familias del Grupo
        '
        CargaGridGrupos()
        If HayGruposCombi = True Then
            HazSeleccionGrupo()
            GrupoPreSeleccion = GRIDGrupos.SelectedCells(0).Value.ToString.Trim
            CargaGridProdu(GRIDGrupos.SelectedCells(0).Value.ToString)
            CargaGridGF(GRIDGrupos.SelectedCells(0).Value.ToString)
        End If
        '
        ' Familias / Articulos
        '
        CargaGridFamilias("*") ' Todas las Familias
        If HayFamCombi = True Then
            GRIDFAM.CurrentCell = GRIDFAM.Rows(0).Cells(0)
            CargaGridArt(GRIDFAM.SelectedCells(0).Value.ToString, "*") ' Todos los Articulos
        End If
        '
        ' FAVORITOS
        '
        Me.RadioButtonBEBIDAS.Checked = True
        CargaGridFAVORITOS("BEBIDAS")
        wGTipoFav = "BEBIDAS"
        If GridFavoritos.Rows.Count > 0 Then
            ButtonEliminaFAV.Enabled = True
        End If
        '
    End Sub


    Private Sub PreparEntorno()
        '
        ' Controles para Mant. Favoritos
        '
        TEMP = ""
        TEMP &= "*** FAVORITOS (30 Registros Máximos.) ***" & vbCrLf
        TEMP &= "[Esc] Terminar Favoritos." & vbCrLf
        LabelFavoritos.Text = TEMP
        LabelNumRegFav.Text = ""
        LabelNumRegFav.Top = 193
        '
        GridFavoritos.Top = GRID1.Top
        TextBoxCodFAV.Top = TextBoxCodGF.Top
        TextBoxNomFAV.Top = TextBoxCodGF.Top
        'ButtonNuevoFAV.Top = ButtonNuevoGF.Top
        'ButtonAceptaFAV.Top = ButtonNuevoGF.Top
        ButtonEliminaFAV.Top = ButtonNuevoGF.Top
        LabelFavoritos.Top = 160
        PanelFavBC.Top = 193
        '
        ' Controles Varios ...
        '
        With GRIDGrupos
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
        With GridFavoritos
            .Height = 421
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
            .Visible = False
        End With
        '
        With GRIDFAM
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            '
            ' Colores
            '
            .BackgroundColor = Color.FromArgb(192, 255, 192)
            .DefaultCellStyle.BackColor = .BackgroundColor
            .AlternatingRowsDefaultCellStyle.BackColor = Color.PaleGreen
            '
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .RowHeadersWidth = 21
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
        '
        With GRIDARTI
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            '
            ' Colores
            '
            .BackgroundColor = Color.FromArgb(192, 255, 255)
            .DefaultCellStyle.BackColor = .BackgroundColor
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
            '
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .RowHeadersWidth = 21
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
        '
        TextBoxCodGrupo.BackColor = Color.White
        TextBoxNomGrupo.BackColor = Color.White
        TextBoxCodArt.BackColor = Color.White
        '
        LimpiaCajasTexto()
        '
    End Sub

    Private Sub CargaGridGrupos()
        '
        ' Carga la Lista de GRUPOS 
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Orden de Carga.: Cod. Grupo
        '
        Dim queryString As String = ""
        queryString = queryString & "SELECT * FROM [TG] "
        queryString = queryString & "ORDER BY [TG].[GUPO]"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "TG")
            '
            Me.GRIDGrupos.Visible = False
            Me.GRIDGrupos.Rows.Clear()
            HayGruposCombi = False
            '
            If dt.Tables("TG").Rows.Count > 0 Then
                HayGruposCombi = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("TG").Rows
                    Me.GRIDGrupos.Rows.Add(pRow("GUPO").ToString(), pRow("NOMBRE").ToString())
                Next
            End If
            Me.GRIDGrupos.Visible = True
            '
            ' Si hay Grupos.
            '
            If GRIDGrupos.Rows.Count > 0 Then
                HazSeleccionGrupo()
                If GRIDGrupos.SelectedRows.Count > 0 Then
                    TextBoxCodGrupo.Text = GRIDGrupos.SelectedCells(0).Value.ToString
                    TextBoxNomGrupo.Text = GRIDGrupos.SelectedCells(1).Value.ToString
                    '
                    ButtonModifica.Enabled = True
                    ButtonElimina.Enabled = True
                    ButtonNuevoArt.Enabled = True
                    ButtonConsArt.Enabled = True
                    '
                    ButtonAutoArt.Enabled = True
                    ButtonAutoFam.Enabled = True
                    '
                    ButtonModificaGF.Enabled = True
                    ButtonEliminaGF.Enabled = True
                    ButtonNuevoGF.Enabled = True
                    '
                End If
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [TG]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub HazSeleccionGrupo()
        '
        ' Determinar El Grupo Preseleccionado.
        '
        For Each row As DataGridViewRow In GRIDGrupos.Rows
            If row.Cells(0).Value.ToString.Trim = GrupoPreSeleccion Then
                GRIDGrupos.CurrentCell = GRIDGrupos.Rows(row.Index).Cells(0)
            End If
        Next
        '
    End Sub

    Private Sub CargaGridFamilias(NombreFam As String)
        '
        ' Carga la Lista de FAMILIAS
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Orden de Carga.: NOMBRE de FAMILIA.
        ' Aqui se lleva a cabo el filtrado por NOMBRE si procede.
        '
        Dim queryString As String = ""
        '
        If NombreFam = "*" Then
            queryString = queryString & "SELECT * FROM [FAM] ORDER BY "
            queryString = queryString & "[FAM].[NOMBRE]"
        Else
            Select Case FiltroFami
                Case 0
                    queryString = queryString & "SELECT * FROM [FAM] "
                    queryString = queryString & " WHERE [FAM].[NOMBRE] LIKE '%" & NombreFam & "'"
                    queryString = queryString & " ORDER BY [FAM].[NOMBRE]"
                Case 1
                    queryString = queryString & "SELECT * FROM [FAM] "
                    queryString = queryString & " WHERE [FAM].[NOMBRE] LIKE '%" & NombreFam & "%'"
                    queryString = queryString & " ORDER BY [FAM].[NOMBRE]"
                Case 2
                    queryString = queryString & "SELECT * FROM [FAM] "
                    queryString = queryString & " WHERE [FAM].[NOMBRE] LIKE '" & NombreFam & "%'"
                    queryString = queryString & " ORDER BY [FAM].[NOMBRE]"
            End Select
        End If
        '
        If LeeTCONA4Cfg("General") = True Then
            If wrLeeTCONA4.Tcona4_ORDENFAM = "True" Then
                queryString = ""
                queryString = queryString & "SELECT * FROM [FAM] ORDER BY "
                queryString = queryString & "[FAM].[CODIGO]"
            End If
        End If
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FAM")
            '
            Me.GRIDFAM.Visible = False
            Me.GRIDFAM.Rows.Clear()
            HayFamCombi = False
            '
            If dt.Tables("FAM").Rows.Count > 0 Then
                HayFamCombi = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FAM").Rows
                    Me.GRIDFAM.Rows.Add(pRow("CODIGO").ToString(), pRow("NOMBRE").ToString())
                Next
            End If
            Me.GRIDFAM.Visible = True
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FAM]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CargaGridArt(MiFamilia As String, NombreArti As String)
        '
        ' Carga GRID de ARTICULOS de la FAMILIA Seleccionada
        '
        Dim TmpPVPART As Double
        Dim TmpAreaART As Integer
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Orden de Carga.: NOMBRE de PRODUCTO.
        ' Aqui se lleva a cabo el filtrado por NOMBRE si procede.
        '
        Dim queryString As String = ""
        If NombreArti = "*" Then
            queryString = queryString & "SELECT * FROM [MAR] "
            queryString = queryString & " WHERE [MAR].[FAMILIA]='" & MiFamilia & "'"
            queryString = queryString & " ORDER BY [MAR].[DESCRIPCION]"
        Else
            Select Case FiltroArti
                Case 0
                    queryString = queryString & "SELECT * FROM [MAR] "
                    queryString = queryString & " WHERE [MAR].[FAMILIA]='" & MiFamilia & "'"
                    queryString = queryString & " AND [MAR].[DESCRIPCION] LIKE '%" & NombreArti & "'"
                    queryString = queryString & " ORDER BY [MAR].[DESCRIPCION]"
                Case 1
                    queryString = queryString & "SELECT * FROM [MAR] "
                    queryString = queryString & " WHERE [MAR].[FAMILIA]='" & MiFamilia & "'"
                    queryString = queryString & " AND [MAR].[DESCRIPCION] LIKE '%" & NombreArti & "%'"
                    queryString = queryString & " ORDER BY [MAR].[DESCRIPCION]"
                Case 2
                    queryString = queryString & "SELECT * FROM [MAR] "
                    queryString = queryString & " WHERE [MAR].[FAMILIA]='" & MiFamilia & "'"
                    queryString = queryString & " AND [MAR].[DESCRIPCION] LIKE '" & NombreArti & "%'"
                    queryString = queryString & " ORDER BY [MAR].[DESCRIPCION]"
            End Select
        End If
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MAR")
            '
            Me.GRIDARTI.Visible = False
            Me.GRIDARTI.Rows.Clear()

            If dt.Tables("MAR").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MAR").Rows
                    TmpPVPART = CDbl(pRow("PREPVP3").ToString())
                    '
                    ' Area -NULL- ???
                    '
                    If String.IsNullOrEmpty(pRow("AREA").ToString) = True Then
                        TmpAreaART = 0
                    Else
                        TmpAreaART = CInt(pRow("AREA").ToString)
                    End If
                    Me.GRIDARTI.Rows.Add(pRow("NARTICULO").ToString(), pRow("DESCRIPCION").ToString(), TmpPVPART.ToString(fmtPrecio), TmpAreaART.ToString())
                Next
            End If
            Me.GRIDARTI.Visible = True
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [MAR]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub LimpiaCajasTexto()
        '
        ' Limpieza de las Cajas de TEXTO
        ' - PRODUCTOS
        ' - FAMILIAS
        '
        With MyFrm7
            .TextBoxCodArt.Text = ""
            .TextBoxNomArt.Text = ""
            .TextBoxPVP3.Text = ""
            .TextBoxOrden.Text = ""
            .TextBoxCodGF.Text = ""
            .TextBoxNomGF.Text = ""
        End With
        '
    End Sub

    Private Sub CargaGridProdu(wCodGrupo As String)
        '
        ' Carga Lista de PRODUCTOS del GRUPO Seleccionado.
        '
        LimpiaCajasTexto()
        '
        Dim TmpPVP3 As Double
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        If wCodGrupo.Trim.Length = 0 Then
            Exit Sub
        End If
        '
        queryString = queryString & "SELECT * FROM [FABO1] "
        queryString = queryString & " WHERE [FABO1].[GRUPO]='" & wCodGrupo & "'"
        queryString = queryString & " ORDER BY [FABO1].[GRUPO], [FABO1].[ARTICULO]"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FABO1")
            '
            Me.GRID1.Visible = False
            Me.GRID1.Rows.Clear()
            '
            If dt.Tables("FABO1").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FABO1").Rows
                    TmpPVP3 = CDbl(pRow("PRECIO").ToString())
                    '
                    ' Lee Datos Articulo
                    '
                    If LeeMar(pRow("ARTICULO").ToString().Trim) = False Then
                        wrLeeMAR.Mar_DESCRIPCION = " * Articulo No Leido * "
                    End If
                    Me.GRID1.Rows.Add(pRow("ARTICULO").ToString(), wrLeeMAR.Mar_DESCRIPCION, TmpPVP3.ToString(fmtPrecio), pRow("ORDEN").ToString())
                Next
            End If
            '
            ' Si hay Productos.
            '
            If GRID1.Rows.Count > 0 Then
                GRID1.CurrentCell = GRID1.Rows(0).Cells(0)
                If GRID1.SelectedRows.Count > 0 Then
                    TextBoxCodArt.Text = GRID1.SelectedCells(0).Value.ToString
                    TextBoxNomArt.Text = GRID1.SelectedCells(1).Value.ToString
                    TextBoxPVP3.Text = GRID1.SelectedCells(2).Value.ToString
                    TextBoxOrden.Text = GRID1.SelectedCells(3).Value.ToString
                    '
                    'ButtonModificaArt.Enabled = True
                    ButtonEliminaArt.Enabled = True
                    '
                    ' El Grupo ya no se PODRA Borrar, integridad Referencial Asegurada...
                    '
                    ButtonElimina.Enabled = False
                End If
            Else
                'ButtonModificaArt.Enabled = False
                ButtonEliminaArt.Enabled = False
            End If
            '
            ' Determina Si el Boton de eliminar GRUPOS se activará o no.
            ' Grupos se relaciona a varias TABLAS y se debe CONSERVAR
            ' la integridad Referencial.
            '
            CompruebaBotonEliminaGrupos()
            '
            Me.GRID1.Visible = True
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO1]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CargaGridGF(wCodGrupo As String)
        '
        ' Carga Lista de FAMILIAS del GRUPO Seleccionado
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        If wCodGrupo.Trim.Length = 0 Then
            Exit Sub
        End If
        '
        ' Oden Por NOMBRE 
        '
        queryString = queryString & "SELECT * FROM [FABO2] "
        queryString = queryString & " WHERE [FABO2].[GRUPO]='" & wCodGrupo & "'"
        queryString = queryString & " ORDER BY [FABO2].[NOMBRE]"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FABO2")
            '
            Me.GRID2.Visible = False
            Me.GRID2.Rows.Clear()
            '
            If dt.Tables("FABO2").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FABO2").Rows
                    Me.GRID2.Rows.Add(pRow("FAMILIA").ToString(), pRow("NOMBRE").ToString())
                Next
            End If
            '
            ' Si hay Productos ... ...
            '
            If GRID2.Rows.Count > 0 Then
                GRID2.CurrentCell = GRID2.Rows(0).Cells(0)
                If GRID2.SelectedRows.Count > 0 Then
                    TextBoxCodGF.Text = GRID2.SelectedCells(0).Value.ToString
                    TextBoxNomGF.Text = GRID2.SelectedCells(1).Value.ToString
                    '
                    ButtonModificaGF.Enabled = True
                    ButtonEliminaGF.Enabled = True
                    '
                    ' El Grupo ya no se PODRA Borrar, Integridad Referencial Asegurada...
                    '
                    ButtonElimina.Enabled = False
                End If
            Else
                ButtonModificaGF.Enabled = False
                ButtonEliminaGF.Enabled = False
            End If
            '
            ' Determina Si el Boton de eliminar GRUPOS se activará o no.
            ' Grupos se relaciona a varias TABLAS y se debe CONSERVAR
            ' la integridad Referencial.
            '
            CompruebaBotonEliminaGrupos()
            '
            Me.GRID2.Visible = True
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO2]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CargaGridFAVORITOS(wTipoFav As String)
        '
        ' Carga Lista de FAVORITOS del Tipo Seleccionado:
        ' wTipoFav "BEBIDAS", "COMIDAS"
        '
        TextBoxCodFAV.Text = "" : TextBoxNomFAV.Text = ""
        ButtonEliminaFAV.Enabled = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        If wTipoFav.Trim.Length = 0 Then
            Exit Sub
        End If
        '
        ' Oden Por NOMBRE 
        '
        Dim queryString As String = ""
        queryString &= "SELECT "
        queryString &= "[FABO3].[TIPO], [FABO3].[ARTICULO], [MAR].[DESCRIPCION] "
        queryString &= "FROM [FABO3], [MAR] "
        queryString &= "WHERE [FABO3].[TIPO]='" & wTipoFav & "' AND "
        queryString &= "[MAR].[NARTICULO]=[FABO3].[ARTICULO] "
        queryString &= "ORDER BY [MAR].[DESCRIPCION]"
        '
        Dim dt As DataSet = New DataSet
        Dim NombreTabla As String = "FABOMAR"
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, NombreTabla)
            GridFavoritos.Visible = False
            GridFavoritos.Rows.Clear()
            '
            If dt.Tables(NombreTabla).Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables(NombreTabla).Rows
                    GridFavoritos.Rows.Add(pRow("ARTICULO").ToString(), pRow("DESCRIPCION").ToString())
                Next
            End If
            '
            If wMant = 1 Then
                GridFavoritos.Visible = True
                If GridFavoritos.Rows.Count > 0 Then
                    GridFavoritos.CurrentCell = GridFavoritos.Rows(0).Cells(0)
                    TextBoxCodFAV.Text = GridFavoritos.SelectedCells(0).Value.ToString
                    TextBoxNomFAV.Text = GridFavoritos.SelectedCells(1).Value.ToString
                    ButtonEliminaFAV.Enabled = True
                End If
            End If
            '
            ' Bloqueamos Creacion de favoritos a 30 registos Máximos.
            '
            wTopeFavoritos = False
            LabelNumRegFav.Text = GridFavoritos.Rows.Count.ToString
            LabelNumRegFav.Text &= " Registros Creados."
            If GridFavoritos.Rows.Count > 29 Then
                wTopeFavoritos = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla Favoritos: [FABO3]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub


    Private Sub CompruebaBotonEliminaGrupos()
        '
        ' Determina Si el Boton de eliminar GRUPOS se activará o no.
        ' Dependerá de Si Hay Productos o bien Familias en el Grupo.
        ' Esto se hace para preservar la integridad referencial...
        '
        If GRID1.Rows.Count = 0 And GRID2.Rows.Count = 0 Then
            ButtonElimina.Enabled = True
        End If
        '
    End Sub

    Private Sub GRIDFAM_Click(sender As Object, e As EventArgs) Handles GRIDFAM.Click
        '
        If Me.GRIDFAM.SelectedRows.Count > 0 Then
            CargaGridArt(Me.GRIDFAM.SelectedCells(0).Value.ToString, "*")
        End If
        '
    End Sub

    Private Sub TextBoxFiltraNombreFam_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFiltraNombreFam.TextChanged
        '
        If Me.TextBoxFiltraNombreFam.Text.Trim.Length > 0 Then
            CargaGridFamilias(Me.TextBoxFiltraNombreFam.Text.Trim)
        Else
            CargaGridFamilias("*")
        End If
        '
        If HayFamCombi = True Then
            CargaGridArt(Me.GRIDFAM.SelectedCells(0).Value.ToString, "*")
        End If
        '
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        FiltroFami = 0
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        FiltroFami = 1
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        FiltroFami = 2
    End Sub

    Private Sub ButtonNuevoGrupo_Click(sender As Object, e As EventArgs) Handles ButtonNuevoGrupo.Click
        '
        '   Nuevo Grupo
        '
        BtnAccion = 1
        TextBoxCodGrupo.ReadOnly = False
        TextBoxNomGrupo.ReadOnly = False
        TextBoxCodGrupo.Text = ""
        TextBoxNomGrupo.Text = ""
        TextBoxCodGrupo.Focus()
        '
        ButtonModificaArt.Enabled = False
        ButtonEliminaArt.Enabled = False
        ButtonNuevoGF.Enabled = False
        ButtonModificaGF.Enabled = False
        ButtonEliminaGF.Enabled = False
        '
        ButtonAutoArt.Enabled = False
        ButtonAutoFam.Enabled = False
        '
        GRID1.Enabled = False : GRID2.Enabled = False
        GRIDFAM.Enabled = False : GRIDARTI.Enabled = False
        GRIDGrupos.Enabled = False
        '
        ButtonNuevoGrupo.Enabled = False
        ButtonModifica.Enabled = False
        ButtonElimina.Enabled = False
        ButtonNuevoArt.Enabled = False
        ButtonConsArt.Enabled = False
        ButtonAcepta.Enabled = True
        '
    End Sub

    Private Sub CancelaNuevoGrupo()
        '
        ' Cancela Acciones NUEVO / MODIFICAR GRUPO
        '
        BtnAccion = 0
        TextBoxCodGrupo.ReadOnly = True
        TextBoxNomGrupo.ReadOnly = True
        TextBoxCodGrupo.Text = ""
        TextBoxNomGrupo.Text = ""
        GRIDGrupos.Focus()
        '
        GRIDFAM.Enabled = True
        GRIDARTI.Enabled = True
        GRIDGrupos.Enabled = True
        GRID1.Enabled = True
        GRID2.Enabled = True
        '
        ButtonNuevoGrupo.Enabled = True
        ButtonModifica.Enabled = False
        ButtonElimina.Enabled = False
        ButtonAcepta.Enabled = False
        '
        ' Refresca Lista GRUPOS
        '
        CargaGridGrupos()
        '
        ' Conveniente Refrescar Listas de PRODUCTOS y FAMILIAS
        '    del GRUPO
        '
        If HayGruposCombi = True Then
            HazSeleccionGrupo()
            CargaGridProdu(GRIDGrupos.SelectedCells(0).Value.ToString)
            CargaGridGF(GRIDGrupos.SelectedCells(0).Value.ToString)
        End If
        '
    End Sub

    Private Sub CancelaNuevoArt()
        '
        ' Cancela Acciones NUEVO / MODIFICAR PRODUCTOS DEL GRUPO
        '
        BtnAccion = 0
        '
        TextBoxCodArt.Text = ""
        TextBoxNomArt.Text = ""
        TextBoxPVP3.Text = ""
        TextBoxOrden.Text = ""
        TextBoxCodArt.ReadOnly = True
        TextBoxNomArt.ReadOnly = True
        TextBoxPVP3.ReadOnly = True
        TextBoxOrden.ReadOnly = True
        '
        ButtonAceptaArt.Enabled = False
        CargaGridProdu(TextBoxCodGrupo.Text.Trim)
        '
    End Sub

    Private Sub CancelaNuevoGf()
        '
        ' Cancela Acciones NUEVO / MODIFICAR FAMILIAS DEL GRUPO
        '
        BtnAccion = 0
        '
        TextBoxCodGF.Text = ""
        TextBoxNomGF.Text = ""
        TextBoxCodGF.ReadOnly = True
        TextBoxNomGF.ReadOnly = True
        GRID2.Enabled = True
        '
        ButtonAceptaGF.Enabled = False
        CargaGridGF(TextBoxCodGrupo.Text.Trim)
        '
    End Sub



    Private Sub ButtonAcepta_Click(sender As Object, e As EventArgs) Handles ButtonAcepta.Click
        '
        ' Aceptar (Grabar) Nuevos Grupos
        '
        Select Case BtnAccion
            Case 1
                GrabaRegistroGrupos()
            Case 2
                ModificaRegistroGrupos()
        End Select
        '
    End Sub

    Private Sub GrabaRegistroGrupos()
        '
        '   Grabamos un Registro de GRUPO.
        '       - Validación de algunos datos. -
        '
        If TextBoxCodGrupo.Text.Length = 0 Or TextBoxNomGrupo.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Grupo son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
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
        ' Paso (1) - Existe ya el Grupo a Crear?
        '
        If ExisteGrupo() = False Then
            '
            ' Paso (2) - Se crea NUEVO registro SOLO si NO EXISTE
            '
            '
            Dim queryString As String = ""
            queryString = "Insert Into [TG] ("
            queryString = queryString & " [GUPO],"
            queryString = queryString & " [NOMBRE]"
            queryString = queryString & ") Values ("
            queryString = queryString & "'" & TextBoxCodGrupo.Text.Trim & "',"
            queryString = queryString & "'" & TextBoxNomGrupo.Text.Trim & "'"
            queryString = queryString & ")"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [TG], Grabando Grupo.")
            End Try
            '
            CancelaNuevoGrupo()
        Else
            MsgBox("Atención este GRUPO ya existe." & vbCr &
                   "Bórrelo antes o bien modifique el existente.",
                    MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub ModificaRegistroGrupos()
        '
        '   Grabamos un Registro de GRUPO.
        '       - Validación de algunos datos. -
        '
        If TextBoxCodGrupo.Text.Length = 0 Or TextBoxNomGrupo.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Grupo son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
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
        ' Conviene que exista para MODIFICAR
        '
        If ExisteGrupo() = True Then
            '
            Dim queryString As String = ""
            queryString = "UPDATE [TG] SET "
            queryString = queryString & " [NOMBRE]='" & TextBoxNomGrupo.Text.Trim & "'"
            queryString = queryString & " WHERE"
            queryString = queryString & " [GUPO]='" & TextBoxCodGrupo.Text.Trim & "'"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [TG], Modificando Grupo.")
            End Try
            '
            CancelaNuevoGrupo()
        Else
            MsgBox("Atención GRUPO NO existe.",
                    MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Function ExisteGrupo() As Boolean
        '
        '   Se comprueba la Existencia o No de un Grupo.
        '
        ExisteGrupo = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [TG] where "
        queryString = queryString & "[TG].[GUPO]='" & TextBoxCodGrupo.Text.Trim & "'"
        Dim dt As DataSet = New DataSet
        '
        Try
            Dim TblGrupos As SqlDataAdapter = New SqlDataAdapter(queryString, conexion)
            TblGrupos.Fill(dt, "TG")
            '
            If dt.Tables("TG").Rows.Count > 0 Then
                '
                ' Si La Tabla tiene SOLO una FILA ...
                '
                wrLeeTG.Tg_GRUPO = dt.Tables("TG").Rows(0).ItemArray(0).ToString()
                wrLeeTG.TG_NOMBRE = dt.Tables("TG").Rows(0).ItemArray(1).ToString()
                '
                ExisteGrupo = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [TG], Lectura Grupo.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Function ExisteGF_Fam(wMiFamilia As String, wOpc As Integer) As Boolean
        '
        ' wOpc:
        '   0 :: Se comprueba la Existencia o No de Familias del un Grupo.
        '   1 :: Se comprueba la Existencia o No de UNA Familia.
        '-------------------------------------------------------------
        ExisteGF_Fam = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        Dim wMiTabla As String = ""
        Select Case wOpc
            Case 0
                queryString = "SELECT * FROM [FABO2] where "
                queryString = queryString & "[FABO2].[GRUPO]='" & TextBoxCodGrupo.Text.Trim & "' AND "
                queryString = queryString & "[FABO2].[FAMILIA]='" & wMiFamilia.Trim & "'"
                wMiTabla = "FABO2"
            Case 1
                queryString = "SELECT * FROM [FAM] where "
                queryString = queryString & "[FAM].[CODIGO]='" & wMiFamilia.Trim & "'"
                wMiTabla = "FAM"
        End Select
        '
        Dim dt As DataSet = New DataSet
        '
        Try
            Dim TblGrupos As SqlDataAdapter = New SqlDataAdapter(queryString, conexion)
            TblGrupos.Fill(dt, wMiTabla)
            '
            If dt.Tables(wMiTabla).Rows.Count > 0 Then
                '
                ' Si La Tabla tiene SOLO una FILA ...
                '
                Select Case wOpc
                    Case 0
                        wrLeeCODNOM.CODIGO = dt.Tables(wMiTabla).Rows(0).ItemArray(1).ToString()
                        wrLeeCODNOM.NOMBRE = dt.Tables(wMiTabla).Rows(0).ItemArray(2).ToString()
                    Case 1
                        wrLeeCODNOM.CODIGO = dt.Tables(wMiTabla).Rows(0).ItemArray(0).ToString()
                        wrLeeCODNOM.NOMBRE = dt.Tables(wMiTabla).Rows(0).ItemArray(1).ToString()
                End Select
                '
                ExisteGF_Fam = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla " & wMiTabla & ", Lectura Grupo.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Function ExisteProducto() As Boolean
        '
        '   Se comprueba la Existencia o No de un Producto en el Grupo.
        '
        ExisteProducto = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [FABO1] where "
        queryString = queryString & "[FABO1].[GRUPO]='" & TextBoxCodGrupo.Text.Trim & "' AND "
        queryString = queryString & "[FABO1].[ARTICULO]='" & TextBoxCodArt.Text.Trim & "'"
        Dim dt As DataSet = New DataSet
        '
        Try
            Dim TblGrupos As SqlDataAdapter = New SqlDataAdapter(queryString, conexion)
            TblGrupos.Fill(dt, "FABO1")
            '
            If dt.Tables("FABO1").Rows.Count > 0 Then
                ExisteProducto = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO1], Lectura Productos del Grupo.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Sub GRIDGrupos_Click(sender As Object, e As EventArgs) Handles GRIDGrupos.Click
        '
        '   Mostramos los datos de un registro en particular (Un GRUPO).
        '   Recuperamos Datos desde una de las Filas del Grid.
        '
        If GRIDGrupos.SelectedRows.Count > 0 Then
            GrupoPreSeleccion = GRIDGrupos.SelectedCells(0).Value.ToString.Trim
            TextBoxCodGrupo.Text = GRIDGrupos.SelectedCells(0).Value.ToString
            TextBoxNomGrupo.Text = GRIDGrupos.SelectedCells(1).Value.ToString
            '
            ' Refrescar Listas de Datos PRODUCTOS y FAMILIAS del Grupo
            '
            LimpiaCajasTexto()
            CargaGridProdu(TextBoxCodGrupo.Text.Trim)
            CargaGridGF(TextBoxCodGrupo.Text.Trim)
            '
        End If
        '
    End Sub

    Private Sub ButtonModifica_Click(sender As Object, e As EventArgs) Handles ButtonModifica.Click
        '
        ' Modificar Grupo Existente
        '
        BtnAccion = 2
        '
        TextBoxNomGrupo.ReadOnly = False
        TextBoxNomGrupo.Focus()
        '
        GRIDFAM.Enabled = False
        GRIDARTI.Enabled = False
        GRIDGrupos.Enabled = False
        GRID1.Enabled = False
        '
        ButtonNuevoGrupo.Enabled = False
        ButtonModifica.Enabled = False
        ButtonElimina.Enabled = False
        ButtonNuevoArt.Enabled = False
        ButtonConsArt.Enabled = False
        ButtonAcepta.Enabled = True
        '
    End Sub

    Private Sub ButtonElimina_Click(sender As Object, e As EventArgs) Handles ButtonElimina.Click
        '
        ' Eliminar Grupos.
        '
        BorrarGrupos()
        '
    End Sub

    Private Sub BorrarGrupos()
        '
        '   Borrado de GRUPOS existentes.
        '
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        Dim VbResp = MsgBox("¿Desea Borrar este GRUPO? .: " & vbCrLf &
           Me.TextBoxCodGrupo.Text & vbCrLf &
           Me.TextBoxNomGrupo.Text, style, "Borrar Registro!")
        '
        If VbResp = vbNo Then
            Exit Sub
        End If
        '
        If TextBoxCodGrupo.Text.Length = 0 Or TextBoxNomGrupo.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Grupo son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
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
        ' Existe ya el GRUPO?
        '
        If ExisteGrupo() = True Then
            '
            ' Si existe BORRADO !!!!
            '
            Dim queryString As String = ""
            queryString = "Delete [TG]"
            queryString = queryString & " WHERE"
            queryString = queryString & " [GUPO]='" & TextBoxCodGrupo.Text.Trim & "'"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [TG], Borrando Registro.")
            End Try
            '
            ' Si el GRUPO se ha eliminado, es porque
            '   no hay PRODUCTOS / FAMILIAS, por tanto no hay que refrescar Listas.
            '
            CancelaNuevoGrupo()
            '
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub BorrarProductos()
        '
        '   Borrado de PRODUCTOS existentes del GRUPO Actual.
        '
        If TextBoxCodArt.Text.Length = 0 Or TextBoxNomArt.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Producto son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArt.Focus()
            Exit Sub
        End If
        '
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        TEMP = ""
        TEMP = TEMP & "¿Desea Borrar el Producto.: " & vbCrLf
        TEMP = TEMP & Me.TextBoxCodArt.Text & vbCrLf
        TEMP = TEMP & Me.TextBoxNomArt.Text & vbCrLf & vbCrLf
        TEMP = TEMP & "perteneciente al GRUPO.: " & vbCrLf
        TEMP = TEMP & Me.TextBoxCodGrupo.Text & vbCrLf
        TEMP = TEMP & Me.TextBoxNomGrupo.Text
        Dim VbResp = MsgBox(TEMP, style, "Borrar Registro!")
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
        ' Existe ya el PRODUCTO de este GRUPO?
        '
        If ExisteProducto() = True Then
            '
            ' Si existe BORRADO !!!
            '
            Dim queryString As String = ""
            queryString = "Delete [FABO1]"
            queryString = queryString & " WHERE"
            queryString = queryString & " [GRUPO]='" & TextBoxCodGrupo.Text.Trim & "' AND "
            queryString = queryString & " [ARTICULO]='" & TextBoxCodArt.Text.Trim & "'"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO1], Borrando Registro.")
            End Try
            '
            CancelaNuevoGrupo()
            CancelaNuevoArt()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub TextBoxCodGrupo_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCodGrupo.GotFocus
        '
        If BtnAccion = 1 Then
            TextBoxCodGrupo.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxCodGrupo_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCodGrupo.LostFocus
        TextBoxCodGrupo.BackColor = Color.White
    End Sub

    Private Sub TextBoxCodGrupo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCodGrupo.KeyPress
        '
        '   Al entrar un código de GRUPO.
        '   Con validación solo numéricos, usando un método algo mas radical: 
        '      Esto admite SOLO lo indicado en allowedChars!!!
        '      (El uso de Cursores y Tecla Del estan contemplados de manera implícita)
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            If BtnAccion = 1 Then
                Select Case e.KeyChar
                    Case ChrW(Keys.Enter)
                        If ValidaCodNomGrupo(TextBoxCodGrupo.Text.Trim) = True Then
                            '
                            ' Si el GRUPO esta creado Mostramos sus datos en pantalla
                            '
                            If ExisteGrupo() Then
                                TextBoxCodGrupo.Text = wrLeeTG.Tg_GRUPO
                                TextBoxNomGrupo.Text = wrLeeTG.TG_NOMBRE
                            Else
                                Dim wCodGrupo As String = TextBoxCodGrupo.Text
                                TextBoxCodGrupo.Text = ""
                                TextBoxNomGrupo.Text = ""
                                TextBoxCodGrupo.Text = wCodGrupo
                            End If
                            TextBoxNomGrupo.Focus()
                        End If
                End Select
                e = Nothing
            End If
        End If
        '
    End Sub

    Private Function ValidaCodNomGrupo(WCodGrupo As String) As Boolean
        '
        '   Evitar Código / Nombre de GRUPO en vacío...
        '
        ValidaCodNomGrupo = True
        If Len(WCodGrupo) = 0 Or IsNothing(WCodGrupo) Then
            ValidaCodNomGrupo = False
        End If
        '
    End Function

    Private Sub TextBoxNomGrupo_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNomGrupo.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxNomGrupo.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxNomGrupo_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNomGrupo.LostFocus
        TextBoxNomGrupo.BackColor = Color.White
    End Sub

    Private Sub TextBoxNomGrupo_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNomGrupo.KeyDown
        '
        If BtnAccion = 1 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxCodGrupo.Focus()
                Case Keys.Enter, Keys.Down
                    If ValidaCodNomGrupo(TextBoxNomGrupo.Text.Trim) = True Then
                        ButtonAcepta.Select()
                    End If
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        FiltroArti = 2
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        FiltroArti = 1
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        FiltroArti = 0
    End Sub

    Private Sub TextBoxFiltraNombreArt_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFiltraNombreArt.TextChanged
        '
        If Me.TextBoxFiltraNombreArt.Text.Trim.Length > 0 Then
            CargaGridArt(GRIDFAM.SelectedCells(0).Value.ToString, Me.TextBoxFiltraNombreArt.Text.Trim)
        Else
            CargaGridArt(GRIDFAM.SelectedCells(0).Value.ToString, "*")
        End If
        '
    End Sub

    Private Sub ButtonNuevoArt_Click(sender As Object, e As EventArgs) Handles ButtonNuevoArt.Click
        '
        '   Nuevo PRODUCTO del GRUPO
        '      Validación GRUPO SELECCIONADO necesario
        '
        If TextBoxCodGrupo.Text.Length = 0 Or TextBoxNomGrupo.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Grupo son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
            Exit Sub
        End If
        '
        BtnAccion = 11
        '
        ButtonNuevoArt.Enabled = False
        ButtonModificaArt.Enabled = False
        ButtonEliminaArt.Enabled = False
        ButtonAceptaArt.Enabled = True
        '
        ButtonNuevoGrupo.Enabled = False
        ButtonModifica.Enabled = False
        ButtonElimina.Enabled = False
        '
        ButtonNuevoGF.Enabled = False
        ButtonModificaGF.Enabled = False
        ButtonEliminaGF.Enabled = False
        ButtonAutoFam.Enabled = False
        '
        GRID1.Enabled = False : GRID2.Enabled = False
        GRIDGrupos.Enabled = False
        '
        TextBoxCodArt.Text = ""
        TextBoxNomArt.Text = ""
        TextBoxPVP3.Text = ""
        TextBoxOrden.Text = ""
        '
        TextBoxCodArt.ReadOnly = False
        '
        TextBoxCodArt.Focus()
        '
    End Sub

    Private Sub TextBoxCodArt_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCodArt.GotFocus
        '
        If BtnAccion = 11 Then
            TextBoxCodArt.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxCodArt_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCodArt.LostFocus
        TextBoxCodArt.BackColor = Color.White
    End Sub

    Private Sub ButtonAceptaArt_Click(sender As Object, e As EventArgs) Handles ButtonAceptaArt.Click
        '
        ' Aceptar (Grabar) Nuevos Productos del Grupo.
        ' En este caso MODIFICAR, no se contempla.
        '
        Select Case BtnAccion
            Case 11
                GrabaRegistroProdu()
        End Select
        '
    End Sub

    Private Sub TextBoxCodArt_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCodArt.KeyDown
        '
        If BtnAccion = 11 Then
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Down
                    If ValidaCodNomGrupo(TextBoxCodArt.Text.Trim) = True Then
                        If LeeMar(TextBoxCodArt.Text.Trim) = True Then
                            SacaDatosArti()
                        Else
                            MsgBox("Atención Producto no Creado." & vbCr &
                                   "No existe un producto con ese código.",
                                   MsgBoxStyle.Information Or
                                   MsgBoxStyle.OkOnly, "Aviso.")
                            TextBoxCodArt.Focus()
                        End If
                    End If
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub SacaDatosArti()
        ' 
        ' Datos del Producto.
        '
        With wrLeeMAR
            TextBoxNomArt.Text = .Mar_DESCRIPCION
            TextBoxPVP3.Text = CDec(.Mar_PREPVP3).ToString(fmtPrecio)
            TextBoxOrden.Text = "0"
        End With
        '
    End Sub

    Private Sub GRIDARTI_DoubleClick(sender As Object, e As EventArgs) Handles GRIDARTI.DoubleClick
        '
        ' Entrada Automatica de Productos al GRUPO.
        '
        Select Case wMant
            ' Normal
            Case 0
                If BtnAccion = 1 Or BtnAccion = 2 Or
                   BtnAccion = 111 Or BtnAccion = 222 Then
                    Exit Sub
                End If
                '
                TextBoxCodArt.Text = GRIDARTI.SelectedCells(0).Value.ToString
                TextBoxNomArt.Text = GRIDARTI.SelectedCells(1).Value.ToString
                TextBoxPVP3.Text = GRIDARTI.SelectedCells(2).Value.ToString
                TextBoxOrden.Text = "0"
                ButtonAceptaArt.Select()
                BtnAccion = 11
                GrabaRegistroProdu()
                '
            ' Mant. de Favoritos
            Case 1
                If wTopeFavoritos = False Then
                    TextBoxCodFAV.Text = GRIDARTI.SelectedCells(0).Value.ToString
                    TextBoxNomFAV.Text = GRIDARTI.SelectedCells(1).Value.ToString
                    GrabaFavorito()
                End If
        End Select
        '
    End Sub

    Private Sub GrabaRegistroProdu()
        '
        '   Grabamos un Registro de PRODUCTOS del GRUPO.
        '       - Validación de algunos datos. -
        '
        If TextBoxCodArt.Text.Length = 0 Or TextBoxNomArt.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Producto son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArt.Focus()
            Exit Sub
        End If
        '
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Paso (1) - Existe ya el Producto a Crear?
        '
        If ExisteProducto() = False Then
            '
            ' Paso (2) - Se crea NUEVO registro SOLO si NO EXISTE
            '
            '
            Dim queryString As String = ""
            queryString = "Insert Into [FABO1] ("
            queryString = queryString & " [GRUPO],"
            queryString = queryString & " [ARTICULO],"
            queryString = queryString & " [PRECIO],"
            queryString = queryString & " [ORDEN]"
            queryString = queryString & ") Values ("
            queryString = queryString & "'" & TextBoxCodGrupo.Text.Trim & "',"
            queryString = queryString & "'" & TextBoxCodArt.Text.Trim & "',"
            queryString = queryString & "'" & TextBoxPVP3.Text.Trim.ToString.Replace(",", ".") & "',"
            queryString = queryString & CInt(TextBoxOrden.Text)
            queryString = queryString & ")"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO1], Grabando Productos del Grupo.")
            End Try
            '
            CancelaNuevoGrupo()
            CancelaNuevoArt()
            '
            ' Refresca Lista de PROUCTOS del Grupo
            '
            If HayGruposCombi = True Then
                HazSeleccionGrupo()
                CargaGridProdu(GRIDGrupos.SelectedCells(0).Value.ToString)
            End If
        Else
            MsgBox("Atención este Pructo ya existe en el GRUPO ." & vbCr &
                   "Bórrelo antes o bien modifique el existente.",
                    MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArt.Focus()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub GRID1_Click(sender As Object, e As EventArgs) Handles GRID1.Click
        '
        '   Mostramos los datos de un registro en particular (Un Producto del GRUPO).
        '   Recuperamos Datos desde una de las Filas del Grid.
        '
        If GRID1.SelectedRows.Count > 0 Then
            TextBoxCodArt.Text = GRID1.SelectedCells(0).Value.ToString
            TextBoxNomArt.Text = GRID1.SelectedCells(1).Value.ToString
            TextBoxPVP3.Text = GRID1.SelectedCells(2).Value.ToString
            TextBoxOrden.Text = GRID1.SelectedCells(3).Value.ToString
        End If
        '
    End Sub

    Private Sub ButtonEliminaArt_Click(sender As Object, e As EventArgs) Handles ButtonEliminaArt.Click
        '
        ' Eliminar PRODUCTOS del Grupo Actual.
        '
        BorrarProductos()
        '
    End Sub

    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonExit.Click
        '
        If wMant = 1 Then
            wMant = 0
            ArenaFavoritos(True)
        End If
        Me.Close()
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonAutoArt.Click
        '
        ' Entrada Automatica de Productos al GRUPO
        '
        Select Case wMant
            '
            ' Normal
            '
            Case 0
                If GRIDARTI.SelectedRows.Count > 0 Then
                    TextBoxCodArt.Text = GRIDARTI.SelectedCells(0).Value.ToString
                    TextBoxNomArt.Text = GRIDARTI.SelectedCells(1).Value.ToString
                    TextBoxPVP3.Text = GRIDARTI.SelectedCells(2).Value.ToString
                    TextBoxOrden.Text = "0"
                    ButtonAceptaArt.Select()
                    BtnAccion = 11
                    GrabaRegistroProdu()
                End If
            '
            ' Modo Mant. de Favoritos
            '
            Case 1
                If GRIDARTI.SelectedRows.Count > 0 Then
                    If wTopeFavoritos = False Then
                        TextBoxCodFAV.Text = GRIDARTI.SelectedCells(0).Value.ToString
                        TextBoxNomFAV.Text = GRIDARTI.SelectedCells(1).Value.ToString
                        GrabaFavorito()
                    End If
                End If
        End Select
        '
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonAutoFam.Click
        '
        ' Familia Seleccionada a Cajas de textos
        '
        HazNuevoGF(0)
        If GRIDFAM.SelectedRows.Count > 0 Then
            TextBoxCodGF.Text = GRIDFAM.SelectedCells(0).Value.ToString
            TextBoxNomGF.Text = GRIDFAM.SelectedCells(1).Value.ToString
            TextBoxNomGF.Focus()
        End If
        '
    End Sub

    Private Sub ButtonNuevoGF_Click(sender As Object, e As EventArgs) Handles ButtonNuevoGF.Click
        '
        ' Nueva Familia del Grupo
        '      Validación GRUPO SELECCIONADO necesario
        '
        ' Encapsulamiento, se reutilizará este procedimiento
        '  desde otras zonas del código.
        '
        HazNuevoGF(0)
        '
    End Sub

    Private Sub HazNuevoGF(Opt As Integer)
        '
        If TextBoxCodGrupo.Text.Length = 0 Or TextBoxNomGrupo.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Grupo son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGrupo.Focus()
            Exit Sub
        End If
        '
        Select Case Opt
            Case 0
                BtnAccion = 111
                '
                ButtonAutoArt.Enabled = False
                ButtonAceptaArt.Enabled = False
                ButtonNuevoArt.Enabled = False
                ButtonModificaArt.Enabled = False
                ButtonEliminaArt.Enabled = False
                '
                ButtonNuevoGrupo.Enabled = False
                ButtonModifica.Enabled = False
                ButtonElimina.Enabled = False
                '
                ButtonNuevoGF.Enabled = False
                ButtonModificaGF.Enabled = False
                ButtonEliminaGF.Enabled = False
                ButtonAceptaGF.Enabled = True
                '
                GRID1.Enabled = False : GRID2.Enabled = False
                GRIDGrupos.Enabled = False : GRIDARTI.Enabled = False
                '
                TextBoxCodGF.Text = ""
                TextBoxNomGF.Text = ""
                '
                TextBoxCodGF.ReadOnly = False
                TextBoxNomGF.ReadOnly = False
                '
                TextBoxCodGF.Focus()
        '
            Case 1
                BtnAccion = 222
                '
                ButtonAutoArt.Enabled = False
                ButtonAutoFam.Enabled = False
                ButtonAceptaArt.Enabled = False
                ButtonNuevoArt.Enabled = False
                ButtonModificaArt.Enabled = False
                ButtonEliminaArt.Enabled = False
                '
                ButtonNuevoGrupo.Enabled = False
                ButtonModifica.Enabled = False
                ButtonElimina.Enabled = False
                '
                ButtonNuevoGF.Enabled = False
                ButtonModificaGF.Enabled = False
                ButtonEliminaGF.Enabled = False
                ButtonAceptaGF.Enabled = True
                '
                GRID1.Enabled = False : GRID2.Enabled = False
                GRIDGrupos.Enabled = False : GRIDARTI.Enabled = False
                '
                TextBoxNomGF.ReadOnly = False
                TextBoxNomGF.Focus()
                '
        End Select
    End Sub

    Private Sub ButtonAceptaGF_Click(sender As Object, e As EventArgs) Handles ButtonAceptaGF.Click
        '
        ' Aceptar (Grabar) Nuevos Productos del Grupo.
        ' En este caso MODIFICAR, no se contempla.
        '
        Select Case BtnAccion
            Case 111
                GrabaRegistroGF(0)
            Case 222
                GrabaRegistroGF(1)
        End Select
        '
    End Sub

    Private Sub ButtonModificaGF_Click(sender As Object, e As EventArgs) Handles ButtonModificaGF.Click
        '
        ' Modificar
        '
        HazNuevoGF(1)
        '
    End Sub

    Private Sub ButtonEliminaGF_Click(sender As Object, e As EventArgs) Handles ButtonEliminaGF.Click
        '
        ' Eliminar FAMILIAS del Grupo Actual.
        '
        BorrarFamiliasGF()
        '
    End Sub

    Private Sub TextBoxCodGF_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCodGF.GotFocus
        '
        If BtnAccion = 111 Then
            TextBoxCodGF.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxCodGF_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCodGF.LostFocus
        TextBoxCodGF.BackColor = Color.White
    End Sub

    Private Sub TextBoxCodGF_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCodGF.KeyDown
        '
        If BtnAccion = 111 Then
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Down
                    If ValidaCodNomGrupo(TextBoxCodGF.Text.Trim) = True Then
                        '
                        ' Si la FAMILIA DEL GRUPO esta creada 
                        ' Mostramos sus datos en pantalla
                        '
                        If ExisteGF_Fam(TextBoxCodGF.Text.Trim, 0) Then
                            TextBoxCodGF.Text = wrLeeCODNOM.CODIGO
                            TextBoxNomGF.Text = wrLeeCODNOM.NOMBRE
                        Else
                            Dim wCodGF As String = TextBoxCodGF.Text
                            TextBoxCodGF.Text = ""
                            TextBoxNomGF.Text = ""
                            TextBoxCodGF.Text = wCodGF
                            '
                            ' Existe UNA Familia con este Código?
                            '
                            If ExisteGF_Fam(TextBoxCodGF.Text.Trim, 1) Then
                                TextBoxCodGF.Text = wrLeeCODNOM.CODIGO
                                TextBoxNomGF.Text = wrLeeCODNOM.NOMBRE
                            End If
                        End If
                        TextBoxNomGF.Focus()
                    End If
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxNomGF_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNomGF.KeyDown
        '
        If BtnAccion = 111 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxCodGF.Focus()
                Case Keys.Enter, Keys.Down
                    If ValidaCodNomGrupo(TextBoxNomGF.Text.Trim) = True Then
                        ButtonAceptaGF.Select()
                    End If
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub GrabaRegistroGF(Opt As Integer)
        '
        '   Grabamos un Registro de FAMILIAS del GRUPO.
        '       - Validación de algunos datos. -
        '
        If TextBoxCodGF.Text.Length = 0 Or TextBoxNomGF.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Familia son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGF.Focus()
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
        ' Paso (1) - Existe ya la FAMIILIA en este GRUPO?
        '
        If ExisteFamiliaGF() = False Then
            '
            ' Paso (2) - Se crea NUEVO registro SOLO si NO EXISTE
            '
            '
            Dim queryString As String = ""
            queryString = "Insert Into [FABO2] ("
            queryString = queryString & " [GRUPO],"
            queryString = queryString & " [FAMILIA],"
            queryString = queryString & " [NOMBRE]"
            queryString = queryString & ") Values ("
            queryString = queryString & "'" & TextBoxCodGrupo.Text.Trim & "',"
            queryString = queryString & "'" & TextBoxCodGF.Text.Trim & "',"
            queryString = queryString & "'" & TextBoxNomGF.Text.Trim & "' "
            queryString = queryString & ")"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO2], Grabando Productos del Grupo.")
            End Try
            '
            CancelaNuevoGrupo()
            CancelaNuevoArt()
            CancelaNuevoGf()
            '
            ' Refresca Lista de FAMILIAS del Grupo
            '
            If HayGruposCombi = True Then
                HazSeleccionGrupo()
                'CargaGridProdu(GRIDGrupos.SelectedCells(0).Value.ToString)
                CargaGridGF(GRIDGrupos.SelectedCells(0).Value.ToString)
            End If
        Else
            If Opt = 0 Then
                MsgBox("Atención esta Familia ya existe en el GRUPO ." & vbCr &
                   "Bórrela antes o bien modifique la existente.",
                    MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly, "Aviso.")
                TextBoxCodGF.Focus()
            Else
                Dim queryString As String = ""
                queryString = "UPDATE [FABO2] SET "
                queryString = queryString & "[NOMBRE]='" & TextBoxNomGF.Text.Trim & "' "
                queryString = queryString & "WHERE "
                queryString = queryString & "[GRUPO]='" & TextBoxCodGrupo.Text.Trim & "' AND "
                queryString = queryString & "[FAMILIA]='" & TextBoxCodGF.Text.Trim & "' "
                '
                Try
                    cmd.CommandText = queryString
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO2], Grabando Productos del Grupo.")
                End Try
                '
                CancelaNuevoGrupo()
                CancelaNuevoArt()
                CancelaNuevoGf()
                '
                ' Refresca Lista de FAMILIAS del Grupo
                '
                If HayGruposCombi = True Then
                    HazSeleccionGrupo()
                    'CargaGridProdu(GRIDGrupos.SelectedCells(0).Value.ToString)
                    CargaGridGF(GRIDGrupos.SelectedCells(0).Value.ToString)
                End If
            End If
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Function ExisteFamiliaGF() As Boolean
        '
        '   Se comprueba la Existencia o No de una Familia en el Grupo.
        '
        ExisteFamiliaGF = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [FABO2] where "
        queryString = queryString & "[FABO2].[GRUPO]='" & TextBoxCodGrupo.Text.Trim & "' AND "
        queryString = queryString & "[FABO2].[FAMILIA]='" & TextBoxCodGF.Text.Trim & "'"
        Dim dt As DataSet = New DataSet
        '
        Try
            Dim TblGrupos As SqlDataAdapter = New SqlDataAdapter(queryString, conexion)
            TblGrupos.Fill(dt, "FABO2")
            '
            If dt.Tables("FABO2").Rows.Count > 0 Then
                ExisteFamiliaGF = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO2], Lectura Productos del Grupo.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Sub BorrarFamiliasGF()
        '
        '   Borrado de FAMILIAS existentes del GRUPO Actual.
        '
        If TextBoxCodGF.Text.Length = 0 Or TextBoxNomGF.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Familia son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodGF.Focus()
            Exit Sub
        End If
        '
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        TEMP = ""
        TEMP = TEMP & "¿Desea Borrar la Familia.: " & vbCrLf
        TEMP = TEMP & Me.TextBoxCodGF.Text & vbCrLf
        TEMP = TEMP & Me.TextBoxNomGF.Text & vbCrLf & vbCrLf
        TEMP = TEMP & "perteneciente al GRUPO.: " & vbCrLf
        TEMP = TEMP & Me.TextBoxCodGrupo.Text & vbCrLf
        TEMP = TEMP & Me.TextBoxNomGrupo.Text
        Dim VbResp = MsgBox(TEMP, style, "Borrar Registro!")
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
        ' Existe ya el PRODUCTO de este GRUPO?
        '
        If ExisteFamiliaGF() = True Then
            '
            ' Si existe BORRADO !!!
            '
            Dim queryString As String = ""
            queryString = "Delete [FABO2]"
            queryString = queryString & " WHERE"
            queryString = queryString & " [GRUPO]='" & TextBoxCodGrupo.Text.Trim & "' AND "
            queryString = queryString & " [FAMILIA]='" & TextBoxCodGF.Text.Trim & "'"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO2], Borrando Registro.")
            End Try
            '
            CancelaNuevoGrupo()
            CancelaNuevoArt()
            CancelaNuevoGf()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub GRID2_Click(sender As Object, e As EventArgs) Handles GRID2.Click
        '
        If GRID2.SelectedRows.Count > 0 Then
            TextBoxCodGF.Text = GRID2.SelectedCells(0).Value.ToString
            TextBoxNomGF.Text = GRID2.SelectedCells(1).Value.ToString
        End If
        '
    End Sub

    Private Sub TextBoxCodGrupo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCodGrupo.TextChanged

    End Sub

    Private Sub ButtonConsArt_Click(sender As Object, e As EventArgs) Handles ButtonConsArt.Click

    End Sub

    Private Sub ButtonFavoritos_Click(sender As Object, e As EventArgs) Handles ButtonFavoritos.Click
        '
        ' Escenario FAVORITOS
        '
        wMant = 1
        ArenaFavoritos(False)
        '
    End Sub

    Private Sub ArenaFavoritos(OnOff As Boolean)
        '
        Select Case OnOff
            Case True
                ButtonAutoArt.Top = 344
            Case False
                ButtonAutoArt.Top = ButtonAutoFam.Top
        End Select
        '
        GRID1.Visible = OnOff
        GRID2.Visible = OnOff
        GRIDGrupos.Visible = OnOff
        '
        TextBoxCodGrupo.Visible = OnOff
        TextBoxNomGrupo.Visible = OnOff
        TextBoxCodArt.Visible = OnOff
        TextBoxNomArt.Visible = OnOff
        TextBoxPVP3.Visible = OnOff
        TextBoxOrden.Visible = OnOff
        TextBoxCodGF.Visible = OnOff
        TextBoxNomGF.Visible = OnOff
        ButtonAutoFam.Visible = OnOff
        ButtonNuevoGrupo.Visible = OnOff
        ButtonAcepta.Visible = OnOff
        ButtonModifica.Visible = OnOff
        ButtonElimina.Visible = OnOff
        ButtonNuevoArt.Visible = OnOff
        ButtonAceptaArt.Visible = OnOff
        ButtonModificaArt.Visible = OnOff
        ButtonEliminaArt.Visible = OnOff
        ButtonNuevoGF.Visible = OnOff
        ButtonAceptaGF.Visible = OnOff
        ButtonModificaGF.Visible = OnOff
        ButtonEliminaGF.Visible = OnOff
        ButtonFavoritos.Visible = OnOff
        'ButtonConsArt.Visible = OnOff 
        '
        GridFavoritos.Visible = Not OnOff
        TextBoxCodFAV.Visible = Not OnOff
        TextBoxNomFAV.Visible = Not OnOff
        'ButtonNuevoFAV.Visible = Not OnOff
        'ButtonAceptaFAV.Visible = Not OnOff
        ButtonEliminaFAV.Visible = Not OnOff
        LabelFavoritos.Visible = Not OnOff
        LabelNumRegFav.Visible = Not OnOff
        PanelFavBC.Visible = Not OnOff
        RadioButtonBEBIDAS.Visible = Not OnOff
        RadioButtonCOMIDAS.Visible = Not OnOff
        '
    End Sub

    Private Sub RadioButtonBEBIDAS_Click(sender As Object, e As EventArgs) Handles RadioButtonBEBIDAS.Click
        '
        ' BEBIDAS FAVORITAS
        '
        CargaGridFAVORITOS("BEBIDAS")
        wGTipoFav = "BEBIDAS"
        '
    End Sub

    Private Sub RadioButtonCOMIDAS_Click(sender As Object, e As EventArgs) Handles RadioButtonCOMIDAS.Click
        '
        ' COMIDAS FAVORITAS
        '
        CargaGridFAVORITOS("COMIDAS")
        wGTipoFav = "COMIDAS"
        '
    End Sub

    Private Sub BorrarFavoritos()
        '
        '   Borrado de FAVORITOS existentes
        '
        If TextBoxCodFAV.Text.Length = 0 Or TextBoxNomFAV.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Producto son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodFAV.Focus()
            Exit Sub
        End If
        '
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        TEMP = ""
        TEMP = TEMP & "¿Desea Borrar este favorito.: " & vbCrLf
        TEMP = TEMP & Me.TextBoxCodFAV.Text & vbCrLf
        TEMP = TEMP & Me.TextBoxNomFAV.Text & vbCrLf & vbCrLf
        TEMP = TEMP & "perteneciente a.: " & vbCrLf
        TEMP = TEMP & wGTipoFav & vbCrLf
        Dim VbResp = MsgBox(TEMP, style, "Borrar Registro!")
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
        ' Existe ya el PRODUCTO de este GRUPO?
        '
        If ExisteFavorito() = True Then
            '
            ' Si existe BORRADO !!!
            '
            Dim queryString As String = ""
            queryString = "Delete [FABO3]"
            queryString = queryString & " WHERE"
            queryString = queryString & " [TIPO]='" & wGTipoFav & "' AND "
            queryString = queryString & " [ARTICULO]='" & TextBoxCodFAV.Text.Trim & "'"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla Favoritos [FABO3], Borrando Registro.")
            End Try
            '
            CargaGridFAVORITOS(wGTipoFav)
            '
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Function ExisteFavorito() As Boolean
        '
        '   Se comprueba la Existencia o No de un Favorito.
        '
        ExisteFavorito = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [FABO3] where "
        queryString = queryString & "[FABO3].[TIPO]='" & wGTipoFav & "' AND "
        queryString = queryString & "[FABO3].[ARTICULO]='" & TextBoxCodFAV.Text.Trim & "'"
        Dim dt As DataSet = New DataSet
        '
        Try
            Dim TblGrupos As SqlDataAdapter = New SqlDataAdapter(queryString, conexion)
            TblGrupos.Fill(dt, "FABO3")
            '
            If dt.Tables("FABO3").Rows.Count > 0 Then
                ExisteFavorito = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO3], Lectura Favoritos.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Sub GridFavoritos_Click(sender As Object, e As EventArgs) Handles GridFavoritos.Click
        '
        If GridFavoritos.SelectedRows.Count > 0 Then
            TextBoxCodFAV.Text = GridFavoritos.SelectedCells(0).Value.ToString
            TextBoxNomFAV.Text = GridFavoritos.SelectedCells(1).Value.ToString
        End If
        '
    End Sub

    Private Sub ButtonEliminaFAV_Click(sender As Object, e As EventArgs) Handles ButtonEliminaFAV.Click
        '
        ' Eliminar FAVORITO del Grupo Actual.
        '
        BorrarFavoritos()
        '
    End Sub

    Private Sub GrabaFavorito()
        '
        '   Grabamos un Registro de FAVORITOS.
        '       - Validación de algunos datos. -
        '
        If TextBoxCodFAV.Text.Length = 0 Or TextBoxNomFAV.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de Producto son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodFAV.Focus()
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
        If ExisteFavorito() = False Then
            '
            ' Se crea NUEVO registro SOLO si NO EXISTE
            '
            Dim queryString As String = ""
            queryString = "Insert Into [FABO3] ("
            queryString = queryString & " [TIPO],"
            queryString = queryString & " [ARTICULO]"
            queryString = queryString & ") Values ("
            queryString = queryString & "'" & wGTipoFav.Trim & "',"
            queryString = queryString & "'" & TextBoxCodFAV.Text.Trim & "'"
            queryString = queryString & ")"
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO3], Grabando Favoritos.")
            End Try
            '
            ' Refresca Lista de FAVORITOS
            '
            CargaGridFAVORITOS(wGTipoFav)
        Else
            MsgBox("Atención Registro Existente." & vbCr &
                   "Este Favorito ya esta creado en.: " & wGTipoFav,
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodFAV.Focus()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

End Class
