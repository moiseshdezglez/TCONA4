Imports System.Data.SqlClient

Public Class TCONA408


    Dim TempPreArt As String = ""
    Dim TempCombi1 As String = ""
    Dim TempCombi2 As String = ""
    Dim TotalFinal As Double = 0
    Private Sub TCONA408_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                CierraCOMBINADOS()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub CierraCOMBINADOS()
        '
        ' Acciones al ABANDONAR el FORMULARIO.
        '

        '
        wStringCombinados = " " ' Por defecto me interesa espacio " "
        TempCombi1 = ""
        Dim TempNoMaRT As String = "" : Dim TempNoMCombi As String = ""
        '
        '   Determinamos Si Hay Combinados en la Lista...
        '
        GRIDCOMBINADOS.Visible = False
        If GRIDCOMBINADOS.Rows.Count > 0 Then
            wStringCombinados = "" ' Vacio si vamos a concatenar
            For Each row As DataGridViewRow In GRIDCOMBINADOS.Rows
                If row.Index = 0 Then
                    TempCombi1 = row.Cells(0).Value.ToString.Trim
                End If
                wStringCombinados = wStringCombinados & row.Cells(0).Value.ToString.Trim & "/"
            Next
            '
            ' Quitamos el ultimo caracter "/".
            '
            wStringCombinados = Mid(wStringCombinados, 1, wStringCombinados.Length - 1)
            '
        End If
        GRIDCOMBINADOS.Visible = True
        '
        ' Lectura de la descripcion, PRIMER combinado.
        ' (Solo se muestra el PRIMER combinado del producto.)
        '
        If LeeMar(TempCombi1) = True Then
            TempNoMCombi = wrLeeMAR.Mar_DESCRIPCION.Trim
            TempNoMaRT = TempNoMaRT & LabelCombiNomArti.Text.Trim & "  +[" & TempNoMCombi & "]"
        Else
            TempNoMaRT = LabelCombiNomArti.Text.Trim
        End If
        '
        ' Comprobar existencia del producto en la lista.
        ' De existir YA, lo que se hace es sumarle las Unidades NUEVAS,
        '    y se gestiona en el procedimiento LocalizarArtGRID1.
        ' En caso contrario se añade la linea al GRID como NUEVA Línea.
        '-------------------------------------------------------------------------------------
        '
        ' 1/2 Ración + COMBINADO.
        '
        wMediaPrecio = " "
        If LeeTCONA4Cfg("General") = True Then
            If wrLeeTCONA4.Tcona4_SEPARARACIONES = "True" Then
                If wrRACIONES.RACIONES_indicador = 2 Then
                    wMediaPrecio = wrRACIONES.RACIONES_PVPMediaRacion.Trim
                End If
            End If
        End If
        '
        ' MyFrm2.TextBoxPrecio.Text.ToString.Trim
        '
        If LocalizarArtGRID1(wMiBtnARTCombi.Tag.ToString,
                             wUnidN.ToString,
                             LabelCombiTotComanda.Text.ToString.Trim,
                             wStringCombinados, wMediaPrecio) = False Then
            '
            ' Aqui Solo ENTRAN al GRID las Nuevas Líneas COMBINADOS.
            ' UNID. EXISTENTES = UNID. ENTRANTES
            '
            wImporteN = wUnidN * CDbl(LabelCombiTotComanda.Text.ToString.Trim.Replace(".", ","))
            '
            MyFrm2.GRID1.Rows.Add(wMiBtnARTCombi.Tag.ToString,
                                   wUnidN.ToString(fmtUnid),
                                   TempNoMaRT,
                                   wUnidN.ToString(fmtUnid),
                                   wImporteN.ToString(fmtImporte), "N",
                                   wStringCombinados, wMediaPrecio,
                                   ContadorPlato.ToString)
        End If
        '
        '   Acciones Necesarias antes de regresar...
        '
        wUnidN = 0 : wImporteN = 0
        ColoreaGRID1("TCONA408")
        wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
        With MyFrm2
            .TextBoxUnidades.Text = ""
            .TextBoxPrecio.Text = ""
            .LabelTotComanda.Text = wTotalN.ToString.Replace(",", ".")
            .LabelInfoUSR.Text = " "
            .LabelInfoUSR.Visible = False
            .Timer1.Enabled = False
            .TextBoxCodBarras.Focus()
            .VisorTeclado.Text = ""
        End With
        SwFoco_402 = 2
        'wTempArtBoton.BackColor = WcolReservar
        '
        Me.Hide()
        '
    End Sub

    Private Sub TCONA408_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        '   Carga del FORM COMBINADOS.
        '
        IniciaBotonesCombinados() : IniciaBotonesGF()
        '
        '   Datos del Articulo
        '
        With wrLeeMAR
            LabelCombiNomArti.Text = .Mar_DESCRIPCION
            TempCombi2 = .Mar_COMBINADO
            '
            ' Tema RACIONES // Combinados.
            '
            With wrRACIONES
                Select Case .RACIONES_indicador
                    Case 0, 1
                        '
                        ' PVP1 a 9 del ARTICULO
                        '
                        'LabelCombiTotComanda.Text = CDec(wrLeeMAR.Mar_PREPVP1).ToString(fmtPrecio).Replace(",", ".")
                        'TotalFinal = CDbl(wrLeeMAR.Mar_PREPVP1.Replace(".", ","))
                        LabelCombiTotComanda.Text = CDec(wrLeeMAR.Mar_PREPVPTPV).ToString(fmtPrecio).Replace(",", ".")
                        TotalFinal = CDbl(wrLeeMAR.Mar_PREPVPTPV.Replace(".", ","))
                    Case 2
                        '
                        ' PVP4 del ARTICULO
                        '
                        LabelCombiTotComanda.Text = CDec(wrLeeMAR.Mar_PREPVP4).ToString(fmtPrecio).Replace(",", ".")
                        TotalFinal = CDbl(wrLeeMAR.Mar_PREPVP4.Replace(".", ","))
                End Select
            End With
            '
            TempPreArt = LabelCombiTotComanda.Text.Trim
            CargaCombinados(TempCombi2)
            CargaGF(TempCombi2)
        End With
        GRIDCOMBINADOS.Rows.Clear()
        '
    End Sub

    Private Sub IniciaBotonesCombinados()
        '
        '   Manejamos la coleccion de Controles "Botones Combinados"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 12) = "ButtonPCOMBI" Then
                    With wControl
                        .Text = ""
                        .Enabled = False
                        .BackgroundImage = Nothing
                        .BackColor = WcolDefFondo
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub IniciaBotonesGF()
        '
        '   Manejamos la coleccion de Controles "Botones Familias/Grupo"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 8) = "ButtonGF" Then
                    With wControl
                        .Text = ""
                        .Enabled = False
                        .BackgroundImage = Nothing
                        .BackColor = WcolDefFondo
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub CargaCombinados(wMigrupo As String)
        '
        ' Carga de COMBINADOS del GRUPO a Botones COMBINADOS
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnCombi As Integer = 33 : Dim IndBTnCombi As Integer = 0
        HayCombinados = False
        Dim wCodGruArti As String = ""
        '
        ' Por Defecto ORDEN ALFABETICO
        '
        Dim queryString As String = ""
        queryString = queryString & "SELECT * FROM [FABO1] "
        queryString = queryString & "WHERE [FABO1].[GRUPO]='" & wMigrupo & "' "
        queryString = queryString & "ORDER BY [FABO1].[ARTICULO]"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FABO1")
            '
            If dt.Tables("FABO1").Rows.Count > 0 Then
                HayCombinados = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FABO1").Rows
                    IndBTnCombi += 1
                    '
                    ' Botones Adelante / Atras...
                    '
                    'If IndBTnCombi = 1 Then
                    'wCodFABOGrupo = pRow("GRUPO").ToString().Trim
                    'wCodFABOArti = pRow("ARTICULO").ToString().Trim
                    'End If
                    '
                    'If IndBTnCombi > NumBTnCombi Then
                    'ButtonCombiAdelante.Enabled = True
                    'Exit For
                    'End If
                    '
                    ' Descripcion del Producto
                    '
                    If LeeMar(pRow("ARTICULO").ToString()) = False Then
                        wrLeeMAR.Mar_DESCRIPCION = "*NO LEIDO*"
                    End If
                    '
                    wCodGruArti = pRow("GRUPO").ToString() & vbCrLf & pRow("ARTICULO").ToString()
                    EstableceBotonCOMBI(IndBTnCombi, wCodGruArti, wrLeeMAR.Mar_DESCRIPCION)
                Next
            End If
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
    Private Sub EstableceBotonCOMBI(IndiceCombi As Integer, CodGruArti As String, NomArti As String)
        '
        '   Se Activan aqui los botones para COMBINADOS.
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 12) = "ButtonPCOMBI" AndAlso Mid$(NombreBoton, 13, 2) = IndiceCombi.ToString Then
                    With wControl
                        .Text = NomArti
                        .Enabled = True
                        .Tag = CodGruArti
                        .BackColor = Color.DodgerBlue
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub HazClickBTNCombi(wMiBtnART As Button)
        '
        '   Manejo del evento CLICK para botones COMBINADOS
        '   Lectura del Artículo, para obtener datos necesarios
        '   Genera línea nueva línea.
        '
        '   Recuperamos el Cod. del Producto ...
        '   Trim("GRUPO + vbcrlf + CODIGOPRODUCTO")
        '
        Dim SplitCombi() As String
        SplitCombi = wMiBtnART.Tag.ToString.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        '
        If LeeMar(SplitCombi(1)) = True Then
            With wrLeeMAR
                GRIDCOMBINADOS.Rows.Add(SplitCombi(1), .Mar_DESCRIPCION, CDec(.Mar_PREPVP3).ToString(fmtPrecio))
                TotalFinal += CDbl(.Mar_PREPVP3.Replace(".", ","))
                LabelCombiTotComanda.Text = CDec(TotalFinal).ToString(fmtImporte).Replace(",", ".")
            End With
        End If
        '
    End Sub

    Private Sub ButtonCombiAnuTODO_Click(sender As Object, e As EventArgs) Handles ButtonCombiAnuTODO.Click
        LabelCombiTotComanda.Text = TempPreArt
        GRIDCOMBINADOS.Rows.Clear()
    End Sub

    Private Sub ButtonCombiAnuLIN_Click(sender As Object, e As EventArgs) Handles ButtonCombiAnuLIN.Click
        '
        ' Anular una linea de COMBINADOS de la lista
        '
        If Me.GRIDCOMBINADOS.SelectedRows.Count > 0 Then
            '
            '   Ajustamos el importe
            '
            Dim wMiImporte As Decimal = CDec(GRIDCOMBINADOS.SelectedCells(2).Value)
            TotalFinal -= wMiImporte
            LabelCombiTotComanda.Text = CDec(TotalFinal).ToString(fmtImporte)
            '
            Me.GRIDCOMBINADOS.Rows.Remove(Me.GRIDCOMBINADOS.SelectedRows(0))
        End If
        '
    End Sub

    Private Sub ButtonCombICERRAR_Click(sender As Object, e As EventArgs) Handles ButtonCombICERRAR.Click
        '
        CierraCOMBINADOS()
        '
    End Sub

    Private Sub CargaGF(wMigrupo As String)
        '
        ' Carga de FAMILIA del GRUPO a Botones FAMILIA
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnGF As Integer = 20 : Dim IndBTnGF As Integer = 0
        HayGF = False
        Dim wCodGruGF As String = ""
        '
        ' Por Defecto ORDEN ALFABETICO
        '
        Dim queryString As String = ""
        queryString = queryString & "SELECT * FROM [FABO2] "
        queryString = queryString & "WHERE [FABO2].[GRUPO]='" & wMigrupo & "' "
        queryString = queryString & "ORDER BY [FABO2].[NOMBRE]"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FABO2")
            '
            If dt.Tables("FABO2").Rows.Count > 0 Then
                HayGF = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FABO2").Rows
                    IndBTnGF += 1
                    '
                    ' Botones Adelante / Atras
                    'If IndBTnGF  = 1 Then
                    'wCodFABOGrupo = pRow("GRUPO").ToString().Trim
                    'wCodFABOArti = pRow("ARTICULO").ToString().Trim
                    'End If
                    'If IndBTnGF > NumBTnGF Then
                    'ButtonCombiAdelante.Enabled = True
                    'Exit For
                    'End If
                    '
                    ' Descripcion del Producto
                    '
                    wCodGruGF = pRow("GRUPO").ToString() & vbCrLf & pRow("FAMILIA").ToString()
                    EstableceBotonGF(IndBTnGF, wCodGruGF, pRow("NOMBRE").ToString())
                Next
            End If
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
    Private Sub EstableceBotonGF(IndiceCombi As Integer, CodGruArti As String, NomArti As String)
        '
        '   Se Activan aqui los botones para COMBINADOS.
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 8) = "ButtonGF" AndAlso Mid$(NombreBoton, 9, 2) = IndiceCombi.ToString Then
                    With wControl
                        .Text = NomArti
                        .Enabled = True
                        .Tag = CodGruArti
                        .BackColor = Color.DodgerBlue
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub ButtonGF1_Click(sender As Object, e As EventArgs) _
        Handles ButtonGF1.Click, ButtonGF9.Click, ButtonGF8.Click, ButtonGF7.Click,
        ButtonGF6.Click, ButtonGF5.Click, ButtonGF4.Click, ButtonGF3.Click, ButtonGF20.Click,
        ButtonGF2.Click, ButtonGF19.Click, ButtonGF18.Click, ButtonGF17.Click, ButtonGF16.Click,
        ButtonGF15.Click, ButtonGF14.Click, ButtonGF13.Click, ButtonGF12.Click, ButtonGF11.Click,
        ButtonGF10.Click
        '
        HazClickBTNGF(CType(sender, Button))
        '
    End Sub


    Private Sub HazClickBTNGF(wMiBtnGF As Button)
        '
        '   Manejo del evento CLICK para botones FAMILIAS
        '   Lectura del Artículos de la FAMILIA, para obtener datos necesarios 
        '
        '   Recuperamos el Cod. de FAMILIA ...
        '   Trim("GRUPO + vbcrlf + CODIGOFAMILIA")
        '
        Dim SplitCombi() As String
        SplitCombi = wMiBtnGF.Tag.ToString.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        '
        IniciaBotonesCombinados()
        CargaProductosFamilia(SplitCombi(1).Trim)
        '
    End Sub

    Private Sub CargaProductosFamilia(wFamilia As String)
        '
        ' Localiza TODOS los PRODUCTOS de la FAMILIA
        '  y los cargamos en los BOTONES de COMBINADOS
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnCombi As Integer = 33 : Dim IndBTnCombi As Integer = 0
        HayCombinados = False
        Dim wCodGruArti As String = ""
        '
        ' Por Defecto ORDEN ALFABETICO
        '
        Dim queryString As String = ""
        queryString = queryString & "SELECT * FROM [MAR] "
        queryString = queryString & "WHERE [MAR].[FAMILIA]='" & wFamilia & "' "
        queryString = queryString & "ORDER BY [MAR].[DESCRIPCION]"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MAR")
            '
            If dt.Tables("MAR").Rows.Count > 0 Then
                HayCombinados = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MAR").Rows
                    IndBTnCombi += 1
                    '
                    ' Botones Adelante / Atras...
                    '
                    'If IndBTnCombi = 1 Then
                    'wCodFABOGrupo = pRow("GRUPO").ToString().Trim
                    'wCodFABOArti = pRow("ARTICULO").ToString().Trim
                    'End If
                    '
                    'If IndBTnCombi > NumBTnCombi Then
                    'ButtonCombiAdelante.Enabled = True
                    'Exit For
                    'End If
                    '
                    wCodGruArti = pRow("FAMILIA").ToString() & vbCrLf & pRow("NARTICULO").ToString()
                    EstableceBotonCOMBI(IndBTnCombi, wCodGruArti, pRow("DESCRIPCION").ToString())
                Next
            End If
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

    Private Sub ButtonCOMBINADO_Click(sender As Object, e As EventArgs) Handles ButtonCOMBINADO.Click
        '
        ' Recargamos Los Combinados a los Botones COMBINADOS
        '
        IniciaBotonesCombinados()
        CargaCombinados(TempCombi2)
        '
    End Sub

    Private Sub ButtonPCOMBI1_Click(sender As Object, e As EventArgs) Handles _
        ButtonPCOMBI1.Click, ButtonPCOMBI9.Click, ButtonPCOMBI8.Click, ButtonPCOMBI7.Click,
        ButtonPCOMBI6.Click, ButtonPCOMBI5.Click, ButtonPCOMBI4.Click, ButtonPCOMBI33.Click,
        ButtonPCOMBI32.Click, ButtonPCOMBI31.Click, ButtonPCOMBI30.Click, ButtonPCOMBI3.Click,
        ButtonPCOMBI29.Click, ButtonPCOMBI28.Click, ButtonPCOMBI27.Click, ButtonPCOMBI26.Click,
        ButtonPCOMBI25.Click, ButtonPCOMBI24.Click, ButtonPCOMBI23.Click, ButtonPCOMBI22.Click,
        ButtonPCOMBI21.Click, ButtonPCOMBI20.Click, ButtonPCOMBI2.Click, ButtonPCOMBI19.Click,
        ButtonPCOMBI18.Click, ButtonPCOMBI17.Click, ButtonPCOMBI16.Click, ButtonPCOMBI15.Click,
        ButtonPCOMBI14.Click, ButtonPCOMBI13.Click, ButtonPCOMBI12.Click, ButtonPCOMBI11.Click,
        ButtonPCOMBI10.Click
        '
        HazClickBTNCombi(CType(sender, Button))
        '
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDCOMBINADOS
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
        With GRIDCOMBINADOS
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
End Class