Imports System.Data.SqlClient

Public Class TCONA414
    Dim NumBTnMESAS As Integer = 96 : Dim IndBTnMESAS As Integer = 0
    Dim wFacturaNOCU As Integer = 0
    Dim wSALAOCU As String = "" : Dim NumeroMesasOCU As Integer = 0
    Dim SalasExistentes() As String = {"", "", "", "", ""}
    Dim TodasMesas As Boolean = False
    Dim TodosVendedores As Boolean = True
    Private Sub TCONA414_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA414_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Mesas Ocupadas
        '
        TodasMesas = False
        TodosVendedores = True
        '
        NumeroMesasOCU = 0
        IniciaBotonesSalas()
        IniciaBotonesMesas(0)
        CargaSalas()
        With LabelMensajeMO
            .Text = "(" & NumeroMesasOCU.ToString & ") "
            .Text &= "MESAS OCUPADAS EN LAS SALAS, Vendedor Actual.: "
            .Text &= MyFrm1.TextBoxOPC1.Text.Trim & " "
            .Text &= MyFrm1.TextBoxNomCamarero.Text.Trim
        End With
        '
    End Sub

    Private Sub ButtonSalir_Click(sender As Object, e As EventArgs) Handles ButtonSalir.Click
        Me.Hide()
    End Sub

    Private Sub IniciaBotonesSalas()
        '
        '   Manejamos la coleccion de Controles "Botones Salas"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 10) = "ButtonSala" Then
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

    Private Sub IniciaBotonesMesas(wOpcIniMesa As Integer)
        '
        '   Manejamos la coleccion de Controles "Botones Mesas"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 10) = "ButtonMesa" Then
                    With wControl
                        Select Case wOpcIniMesa
                            Case 0
                                .Text = ""
                                .Enabled = False
                                .BackgroundImage = Nothing
                                .BackColor = WcolDefFondo
                                .Visible = True
                            Case 1
                                If .Enabled = False Then
                                    .Visible = Not .Visible
                                End If
                        End Select
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub CargaSalas()
        '
        ' Leemos Todas las Salas, Señalamos sus MESAS OCUPADAS.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnSALAS As Integer = 6 : Dim IndBTnSALAS As Integer = 0
        HaySalas = False
        '
        ' Cargar TODAS las SALAS, Incluida la 999 = MESAS SEPARADAS....
        '
        Dim queryString As String = "SELECT * FROM [SALA] WHERE "
        queryString = queryString & "[SALA].[CAJA]=" & wCaja
        queryString = queryString & " ORDER BY CAST([SALA].[CODIGO] AS INTEGER) ASC"
        '
        Dim dt As DataSet = New DataSet
        '
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA")
            '
            '   Hasta SEIS Salas
            '
            If dt.Tables("SALA").Rows.Count > 0 Then
                Dim pRow As DataRow
                HaySalas = True
                '
                For Each pRow In dt.Tables("SALA").Rows
                    '
                    SalasExistentes(IndBTnSALAS) = pRow("CODIGO").ToString()
                    '
                    IndBTnSALAS += 1
                    If IndBTnSALAS > NumBTnSALAS Then
                        Exit For
                    End If
                    Select Case IndBTnSALAS
                        Case 1
                            ButtonSala1.Tag = pRow("CODIGO").ToString()
                            ButtonSala1.Text = pRow("NOMBRE").ToString()
                            ButtonSala1.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonSala1.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonSala1.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonSala1.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonSala1.Image = Nothing
                                    ButtonSala1.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonSala1.Image = Nothing
                                ButtonSala1.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonSala1.Enabled = True
                        Case 2
                            ButtonSala2.Tag = pRow("CODIGO").ToString()
                            ButtonSala2.Text = pRow("NOMBRE").ToString()
                            ButtonSala2.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonSala2.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonSala2.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonSala2.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonSala2.Image = Nothing
                                    ButtonSala2.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonSala2.Image = Nothing
                                ButtonSala2.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonSala2.Enabled = True
                        Case 3
                            ButtonSala3.Tag = pRow("CODIGO").ToString()
                            ButtonSala3.Text = pRow("NOMBRE").ToString()
                            ButtonSala3.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonSala3.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonSala3.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonSala3.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonSala3.Image = Nothing
                                    ButtonSala3.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonSala3.Image = Nothing
                                ButtonSala3.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonSala3.Enabled = True
                        Case 4
                            ButtonSala4.Tag = pRow("CODIGO").ToString()
                            ButtonSala4.Text = pRow("NOMBRE").ToString()
                            ButtonSala4.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonSala4.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonSala4.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonSala4.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonSala4.Image = Nothing
                                    ButtonSala4.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonSala4.Image = Nothing
                                ButtonSala4.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonSala4.Enabled = True
                        Case 5
                            ButtonSala5.Tag = pRow("CODIGO").ToString()
                            ButtonSala5.Text = pRow("NOMBRE").ToString()
                            ButtonSala5.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonSala5.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonSala5.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonSala5.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonSala5.Image = Nothing
                                    ButtonSala5.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonSala5.Image = Nothing
                                ButtonSala5.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonSala5.Enabled = True
                        Case 6
                            ButtonSala6.Tag = pRow("CODIGO").ToString()
                            ButtonSala6.Text = pRow("NOMBRE").ToString()
                            ButtonSala6.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonSala6.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonSala6.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonSala6.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonSala6.Image = Nothing
                                    ButtonSala6.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonSala6.Image = Nothing
                                ButtonSala6.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonSala6.Enabled = True
                    End Select
                    '
                    ' Según la SALA establecemos INDICE inicial para Botonera Mesas
                    '
                    Select Case pRow("NOMBRE").ToString().Trim
                        Case Is = ButtonSala1.Text.Trim
                            IndBTnMESAS = 0
                        Case Is = ButtonSala2.Text.Trim
                            IndBTnMESAS = 16
                        Case Is = ButtonSala3.Text.Trim
                            IndBTnMESAS = 32
                        Case Is = ButtonSala4.Text.Trim
                            IndBTnMESAS = 48
                        Case Is = ButtonSala5.Text.Trim
                            IndBTnMESAS = 64
                        Case Is = ButtonSala6.Text.Trim
                            IndBTnMESAS = 81
                    End Select
                    '
                    ' ... y Cargamos las MESAS OCUPADAS en la botonera.
                    '
                    CargaMesas(pRow("CODIGO").ToString().Trim)
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [SALA]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CargaMesas(wCrgCodSala As String)
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim Wnfac As Integer = 0
        '
        ' Si Mesas de la SALA=999 (Separadas), evitar MESA=999
        ' Las Mesas Separadas pueden llevar Guión "-", en ese caso evitar
        ' conversiones y compraraciones INTEGER
        '
        Dim queryString As String = ""
        If wCrgCodSala.Trim = "999" Then
            queryString = "SELECT * FROM [SALA1] WHERE "
            queryString = queryString & "[SALA1].[CAJA]=" & wCaja & " AND "
            queryString = queryString & "[SALA1].[CODIGO]='" & wCrgCodSala & "' AND "
            queryString = queryString & "[SALA1].[MESA] <> '999' "
            queryString = queryString & "ORDER BY [SALA1].[MESA] "
        Else
            queryString = "SELECT * FROM [SALA1] WHERE "
            queryString = queryString & "[SALA1].[CAJA]=" & wCaja & " AND "
            queryString = queryString & "[SALA1].[CODIGO]='" & wCrgCodSala & "' AND "
            queryString = queryString & "[SALA1].[MESA] <> '999' "
            queryString = queryString & "ORDER BY CAST([SALA1].[MESA] AS INTEGER) ASC"
        End If
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA1")
            '
            If dt.Tables("SALA1").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("SALA1").Rows
                    '
                    ' Discriminación Por Vendedor ACTUAL / TODOS
                    '
                    If TodosVendedores = False Then
                        If pRow("VENDEDOR").ToString <> MyFrm1.TextBoxOPC1.Text.Trim Then
                            Continue For
                        End If
                    End If
                    '
                    ' Mostramos Solo las MESAS OCUPADAS: FACTURA > 0
                    '
                    If IsDBNull(pRow("FACTURA")) Then
                        Wnfac = 0
                    Else
                        Wnfac = CInt(pRow("FACTURA"))
                        '
                        ' Publico en Nro. de Factura de la MESA para este Módulo
                        '
                        wFacturaNOCU = Wnfac
                    End If
                    '
                    ' Publico Cod. de SALA para este Módulo.
                    '
                    wSALAOCU = wCrgCodSala
                    '
                    Select Case TodasMesas
                        Case True
                            IndBTnMESAS += 1
                            If Wnfac > 0 Then
                                NumeroMesasOCU += 1 ' Total MESAS OCUPADAS
                            End If
                            '
                            ' Llenamos Datos en TODAS las MESAS.
                            '
                            EstableceBotonMesa(IndBTnMESAS,
                                   pRow("MESA").ToString(),
                                   pRow("LOGO").ToString(),
                                   Wnfac)
                        Case False
                            If Wnfac > 0 Then
                                IndBTnMESAS += 1
                                NumeroMesasOCU += 1 ' Total MESAS OCUPADAS
                                '
                                ' Llenamos Datos en cada MESA OCUPADA
                                '
                                EstableceBotonMesa(IndBTnMESAS,
                                   pRow("MESA").ToString(),
                                   pRow("LOGO").ToString(),
                                   Wnfac)
                            End If
                    End Select
                    '
                    If IndBTnMESAS > NumBTnMESAS Then
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [SALA1]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub EstableceBotonMesa(IndiceMesa As Integer, CodMesa As String,
                                   LogoMesa As String, NumFac As Integer)
        '
        '   Se Activan aqui los botones para las mesas OCUPADAS de una SALA determinada.
        '
        Dim wImpo As Double
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 10) = "ButtonMesa" AndAlso Mid$(NombreBoton, 11, 2) = IndiceMesa.ToString Then
                    With wControl
                        '
                        ' Texto a reflejar en la MESA.
                        '  LIBRES   = Cod. MESA  
                        '  OCUPADAS = Cod. MESA / IMPORTE Actual.
                        '
                        .Text = CodMesa
                        '
                        ' ... OBTENGO DATOS desde [MESAC] ...
                        '
                        ' Adapto este PROCEDIMIENTO SOLO para uso en este módulo.
                        ' No interesa para NADA USAR el Publico de la aplicacion aqui.
                        ' Se evitan acciones que NO HAY QUE REALIZAR desde este punto !!!
                        '
                        If ExisteRegistroMESAC_OCUPADAS(wSALAOCU, CodMesa, 0) Then
                            wImpo = CDbl(wrLeeMESAC.Mesac_IMPORTE)
                            .Text += vbCrLf & wImpo.ToString(fmtUnid).Replace(",", ".")
                        End If
                        '
                        ' Experimental Nombre Vendedor en
                        ' Botón MESA, (Primeros 5 Caracteres)
                        ' SOLO para OCUPADAS !!!
                        '
                        If NumFac > 0 Then
                            .Enabled = True
                            '
                            ' Color de fondo
                            '   - Ocupadas = Rojo
                            '   - Ocupadas + Pedido a Domicilio = Amarillo
                            '
                            .BackColor = Color.Red
                            '
                            ' Si la mesa tiene un Pedido a Domicilio
                            '
                            If Not IsDBNull(wrLeeMESAC.Mesac_TLFPEDIDOS) Then
                                If wrLeeMESAC.Mesac_TLFPEDIDOS.Trim.Length > 0 Then
                                    .BackColor = Color.Yellow
                                End If
                            End If
                            '
                            If LeeVendedor(wrLeeMESAC.Mesac_VENDEDOR) = True Then
                                .Text += vbCrLf & Mid(wrLeeCODNOM.NOMBRE.Trim, 1, 7)
                            End If
                        Else
                            .Enabled = False
                            .BackColor = Color.Green
                        End If
                        '
                        If Len(LogoMesa.Trim) > 0 Then
                            MiFileExist = My.Computer.FileSystem.FileExists(LogoMesa.Trim)
                            If MiFileExist = True Then
                                .BackgroundImage = Image.FromFile(LogoMesa)
                            Else
                                .BackgroundImage = Nothing
                            End If
                        Else
                            .BackgroundImage = Nothing
                        End If
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub ButtonTipoVista_Click(sender As Object, e As EventArgs) Handles ButtonTipoVista.Click
        '
        ' MESAS LIBRES VISIBLES SI / NO
        '
        TodasMesas = Not TodasMesas
        RefrescaEscenario()
        '
    End Sub

    Private Sub ButtonTipoVistaVen_Click(sender As Object, e As EventArgs) Handles ButtonTipoVistaVen.Click
        '
        TodosVendedores = Not TodosVendedores
        RefrescaEscenario()
        '
    End Sub

    Private Sub RefrescaEscenario()
        '
        NumeroMesasOCU = 0
        IniciaBotonesSalas()
        IniciaBotonesMesas(0)
        CargaSalas()
        With LabelMensajeMO
            .Text = "(" & NumeroMesasOCU.ToString & ") "
            .Text &= "MESAS OCUPADAS EN LAS SALAS, Vendedor Actual.: "
            .Text &= MyFrm1.TextBoxOPC1.Text.Trim & " "
            .Text &= MyFrm1.TextBoxNomCamarero.Text.Trim
        End With
        '
        If TodasMesas = False Then
            IniciaBotonesMesas(1)
        End If
        '
    End Sub

    Private Function ExisteRegistroMESAC_OCUPADAS(ExisteSALAC As String,
                                                  ExisteMESAC As String,
                                                  ExMesacOPC As Integer) As Boolean
        '
        ' Lectura de registros de CABECERA de MESAS.
        ' Adaptacion para este Modulo.
        '
        ' ExMesacOPC =
        '    0 - Lee MESAC por la KEY completa.
        '
        ExisteRegistroMESAC_OCUPADAS = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Comprobar SALA1, para MESAS NORMALES / SEPARADAS
        '
        If LeeMesa_SALA1(ExisteSALAC, ExisteMESAC, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
            End If
        End If
        '
        Dim ExisteFechaMESAC As String = FechaMESAC : Dim ComparaFecha As String = ""
        '
        Dim queryString As String = ""
        Select Case ExMesacOPC
            Case 0
                queryString = "SELECT * FROM [MESAC] WHERE "
                queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
                queryString = queryString & "[MESAC].[FECHA]='" & ExisteFechaMESAC & "' AND "
                queryString = queryString & "[MESAC].[SALA]='" & ExisteSALAC & "' AND "
                queryString = queryString & "[MESAC].[MESA]='" & ExisteMESAC & "' AND "
                queryString = queryString & "[MESAC].[FACTURA]=" & wFacturaNOCU
        End Select
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESAC")
            '
            If dt.Tables("MESAC").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MESAC").Rows
                    '
                    ' pRow("FECHA").ToString() = DD/MM/AAAA 00:00:00
                    ' ComparaFecha = "DD/MM/AAAA"
                    '
                    ComparaFecha = Format(pRow("FECHA"), "dd/MM/yyyy")
                    If pRow("NUMCAJA").ToString() = wCaja.ToString And
                       ComparaFecha = ExisteFechaMESAC And
                       pRow("MESA").ToString() = ExisteMESAC And
                       pRow("FACTURA").ToString() = wFacturaNOCU.ToString Then
                        '
                        ' Recogemos datos...
                        '
                        With wrLeeMESAC
                            .Mesac_EMPRESA = CInt(pRow("EMPRESA").ToString())
                            .Mesac_CLIENTE = CInt(pRow("CLIENTE").ToString())
                            .Mesac_VENDEDOR = CInt(pRow("VENDEDOR").ToString())
                            .Mesac_PIGIC = pRow("PIGIC").ToString()
                            .Mesac_PDTO = pRow("PDTO").ToString()
                            .Mesac_IMPDTO = pRow("IMPDTO").ToString()
                            .Mesac_IMPORTE = pRow("IMPORTE").ToString() ' <<<---- !!!
                            .Mesac_ENTREGA = pRow("ENTREGA").ToString()
                            .Mesac_TARJETA = pRow("TARJETA").ToString()
                            .Mesac_VALEDTO = pRow("VALEDTO").ToString()
                            .Mesac_CHEQUES = pRow("CHEQUES").ToString()
                            .Mesac_OTROS = pRow("OTROS").ToString()
                            .Mesac_TLFPEDIDOS = pRow("TLFPEDIDOS").ToString() & "" ' <<<---- !!!
                            '
                            ' Ticket Fra. Impreso?
                            '
                            If Not IsDBNull(pRow("TKFACIMPRESO")) Then
                                .Mesac_TKFACIMPRESO = pRow("TKFACIMPRESO").ToString
                            Else
                                .Mesac_TKFACIMPRESO = "False"
                            End If
                        End With
                        ExisteRegistroMESAC_OCUPADAS = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [MESAC]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Sub TCONA414_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        ' Mesas Libres Visibles S/N
        ' Depende de la variable Global al proyecto BtnMesasLibresVisible
        '
        If FormOcuIni = False Then
            FormOcuIni = True
            NumeroMesasOCU = 0
            IniciaBotonesSalas()
            IniciaBotonesMesas(0)
            CargaSalas()
            With LabelMensajeMO
                .Text = "(" & NumeroMesasOCU.ToString & ") "
                .Text &= "MESAS OCUPADAS EN LAS SALAS, Vendedor Actual.: "
                .Text &= MyFrm1.TextBoxOPC1.Text.Trim & " "
                .Text &= MyFrm1.TextBoxNomCamarero.Text.Trim
            End With
            IniciaBotonesMesas(1)
        End If
    End Sub

    Private Sub ButtonMesa1_Click(sender As Object, e As EventArgs) _
        Handles ButtonMesa1.Click, ButtonMesa96.Click, ButtonMesa95.Click, ButtonMesa94.Click,
        ButtonMesa93.Click, ButtonMesa92.Click, ButtonMesa91.Click, ButtonMesa90.Click,
        ButtonMesa9.Click, ButtonMesa89.Click, ButtonMesa88.Click, ButtonMesa87.Click,
        ButtonMesa86.Click, ButtonMesa85.Click, ButtonMesa84.Click, ButtonMesa83.Click,
        ButtonMesa82.Click, ButtonMesa81.Click, ButtonMesa80.Click, ButtonMesa8.Click,
        ButtonMesa79.Click, ButtonMesa78.Click, ButtonMesa77.Click, ButtonMesa76.Click,
        ButtonMesa75.Click, ButtonMesa74.Click, ButtonMesa73.Click, ButtonMesa72.Click,
        ButtonMesa71.Click, ButtonMesa70.Click, ButtonMesa7.Click, ButtonMesa69.Click,
        ButtonMesa68.Click, ButtonMesa67.Click, ButtonMesa66.Click, ButtonMesa65.Click,
        ButtonMesa64.Click, ButtonMesa63.Click, ButtonMesa62.Click, ButtonMesa61.Click,
        ButtonMesa60.Click, ButtonMesa6.Click, ButtonMesa59.Click, ButtonMesa58.Click,
        ButtonMesa57.Click, ButtonMesa56.Click, ButtonMesa55.Click, ButtonMesa54.Click,
        ButtonMesa53.Click, ButtonMesa52.Click, ButtonMesa51.Click, ButtonMesa50.Click,
        ButtonMesa5.Click, ButtonMesa49.Click, ButtonMesa48.Click, ButtonMesa47.Click,
        ButtonMesa46.Click, ButtonMesa45.Click, ButtonMesa44.Click, ButtonMesa43.Click,
        ButtonMesa42.Click, ButtonMesa41.Click, ButtonMesa40.Click, ButtonMesa4.Click,
        ButtonMesa39.Click, ButtonMesa38.Click, ButtonMesa37.Click, ButtonMesa36.Click,
        ButtonMesa35.Click, ButtonMesa34.Click, ButtonMesa33.Click, ButtonMesa32.Click,
        ButtonMesa31.Click, ButtonMesa30.Click, ButtonMesa3.Click, ButtonMesa29.Click,
        ButtonMesa28.Click, ButtonMesa27.Click, ButtonMesa26.Click, ButtonMesa25.Click,
        ButtonMesa24.Click, ButtonMesa23.Click, ButtonMesa22.Click, ButtonMesa21.Click,
        ButtonMesa20.Click, ButtonMesa2.Click, ButtonMesa19.Click, ButtonMesa18.Click,
        ButtonMesa17.Click, ButtonMesa16.Click, ButtonMesa15.Click, ButtonMesa14.Click,
        ButtonMesa13.Click, ButtonMesa12.Click, ButtonMesa11.Click, ButtonMesa10.Click
        '
        '
        '   Evento CLICK en Botones MESAS
        '
        HazClicMesas(CType(sender, Button))
        '
    End Sub

    Private Sub HazClicMesas(wMibotonMesa As Button)
        '
        '   Acciones del Evento CLICK para los Botones MESAS
        '   Localizamos la sala de la MESA Ocupada.
        '   Lee datos de la mesa.
        '
        ' Comprobar el vendedor de apertura de mesa...
        '
        Dim SplitMesa() As String
        Dim wIndBtn As Integer = CInt(Mid$(wMibotonMesa.Name.Trim, 11, 2))
        '
        SplitMesa = wMibotonMesa.Text.ToString.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        Select Case wIndBtn
            Case 1 To 16
                wCodSala = SalasExistentes(0)
            Case 17 To 32
                wCodSala = SalasExistentes(1)
            Case 33 To 48
                wCodSala = SalasExistentes(2)
            Case 49 To 64
                wCodSala = SalasExistentes(3)
            Case 65 To 80
                wCodSala = SalasExistentes(4)
            Case 81 To 96
                wCodSala = SalasExistentes(5)
        End Select
        '
        If LeeMesa_SALA1(wCodSala, SplitMesa(0).Trim, 0) = True Then
            '
            If wVendedorApertura > 0 Then
                If CInt(MyFrm1.TextBoxOPC1.Text.Trim) <> wVendedorApertura Then
                    msg = "Mesa asignada a otro camarero."
                    style = MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly
                    title = "No se puede Aperturar esta mesa."
                    MsgBox(msg, style, title)
                    Exit Sub
                End If
            End If
            '
            FormOcuON = True
            '
            MyFrm1.Hide()
            Me.Hide()
            '
            SwAparca = True
            SwEntraMesa = True
            '
            With MyFrm2
                .Visible = False
                wCodMesa = SplitMesa(0).Trim
                .TextBoxNumMesa.Text = SplitMesa(0).Trim
                .TextBoxFactura.Text = wrLeeSALA1.Sala1_FACTURA.ToString
                .TextBoxCamarero.Text = wrLeeSALA1.Sala1_VENDEDOR.ToString
                .TextBoxPax.Text = wrLeeSALA1.Sala1_PAX.ToString.Trim
                '
                ' Fecha / Hora Apartura
                '
                If wrLeeSALA1.Sala1_HORAAPAERTURA.Trim.Length = 0 Then
                    wrLeeSALA1.Sala1_HORAAPAERTURA = Format(Date.Now.ToShortTimeString, "HH:MM:SS")
                End If
                If wrLeeSALA1.Sala1_FECAPERTURA.Trim.Length > 0 Then
                    .LabelFecAper.Text = "Aper.: " & Format(CDate(wrLeeSALA1.Sala1_FECAPERTURA.Trim), "dd/MM/yy")
                    Dim dtTime As DateTime = Convert.ToDateTime(wrLeeSALA1.Sala1_HORAAPAERTURA.Trim)
                    .LabelFecAper.Text &= " " & Format(dtTime, "HH:mm")
                Else
                    .LabelFecAper.Text = "Aper.: " & Format(Date.Now.ToShortDateString, "dd/MM/yy")
                    .LabelFecAper.Text &= " " & Format(Date.Now.ToShortTimeString, "HH:MM:SS")
                End If
                '
                '
                ' Datos de un Pedido a Domicilio si la mesa lo tiene asignado.
                '
                MyFrm2.LblDatosPedidoDomi.Text = ""
                wrLeeMESAC.Mesac_TLFPEDIDOS = ""
                If ExisteRegistroMESAC(SplitMesa(0).Trim, 1) = True Then
                    If Not IsDBNull(wrLeeMESAC.Mesac_TLFPEDIDOS) Then
                        If wrLeeMESAC.Mesac_TLFPEDIDOS.Trim.Length > 0 Then
                            If LeePEDCLIE(wrLeeMESAC.Mesac_TLFPEDIDOS.Trim) = True Then
                                MyFrm2.LblDatosPedidoDomi.Text = wrLeeMESAC.Mesac_TLFPEDIDOS & " - " & wrLeePEDCLIE.NOMBRE
                            End If
                        End If
                    End If
                End If
                '
                .Show()
                CargaListaMESAs(SplitMesa(0).Trim)
                .Visible = True
            End With
        End If
        '
    End Sub

End Class