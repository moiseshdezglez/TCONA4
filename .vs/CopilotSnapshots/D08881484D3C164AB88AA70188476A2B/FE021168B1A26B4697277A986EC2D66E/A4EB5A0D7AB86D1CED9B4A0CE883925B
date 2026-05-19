Imports System.Data.SqlClient

Public Class TCONA412
    '
    ' Algunas variables globales a la clase.
    '
    Dim SalaOrigen As String = "" : Dim MesaOrigen As String = ""
    Dim SalaDestino As String = "" : Dim MesaDestino As String = ""
    Dim FechaTraspasa As String = ""

    Private Sub TCONA412_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                CerraForm()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub CerraForm()
        '
        Me.Close()
        '
    End Sub

    Private Sub TCONA412_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Al cargar FORM cambio MESA
        '
        '
        ' Cargar Lista Cambio Mesa
        '
        GRID1_a_GRIDZOOM(1)
        LabelTotComandaC.Text = MyFrm2.LabelTotComanda.Text
        '
        wCodSalaC = wCodSala
        LeeSALA(wCodSalaC)
        TextBoxCNumSala.Text = wCodSalaC.ToString.Trim
        TextBoxCNombreSala.Text = wrLeeSALA.Sala_NOMBRE
        TextBoxCSALADESTINO.Text = wCodSalaC.ToString.Trim
        TextBoxCNumMesa.Text = MyFrm2.TextBoxNumMesa.Text
        IniciaBotonesSalas()
        IniciaBotonesMesas()
        CargaSalas()
        If HaySalas Then
            CargaMesas(wCodSalaC)
        End If
        '
    End Sub

    Private Sub IniciaBotonesSalas()
        '
        '   Manejamos la coleccion de Controles "Botones Salas"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 11) = "ButtonCSala" Then
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

    Public Sub IniciaBotonesMesas()
        '
        '   Manejamos la coleccion de Controles "Botones Mesas"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 11) = "ButtonCMesa" Then
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

    Private Sub CargaSalas()
        '
        '   Botones para las SALAS...
        '   wCaja es GLOBAL y se define su valor en Tcona4Main
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnSALAS As Integer = 6 : Dim IndBTnSALAS As Integer = 0
        HaySalas = False : wCodSalaC = ""
        '
        Dim queryString As String = "SELECT * FROM [SALA] WHERE "
        queryString = queryString & "[SALA].[CAJA]=" & wCaja
        queryString = queryString & " AND [SALA].[CODIGO] <> 999 "
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
                For Each pRow In dt.Tables("SALA").Rows
                    IndBTnSALAS += 1
                    If IndBTnSALAS > NumBTnSALAS Then
                        Exit For
                    End If
                    Select Case IndBTnSALAS
                        Case 1
                            ButtonCSala1.Tag = pRow("CODIGO").ToString()
                            ButtonCSala1.Text = pRow("NOMBRE").ToString()
                            wCodSalaC = pRow("CODIGO").ToString()
                            'LabelNombreSala.Text = ButtonCSala1.Text.ToString.Trim
                            ButtonCSala1.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonCSala1.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonCSala1.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonCSala1.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonCSala1.Image = Nothing
                                    ButtonCSala1.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonCSala1.Image = Nothing
                                ButtonCSala1.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonCSala1.Enabled = True
                            HaySalas = True
                        Case 2
                            ButtonCSala2.Tag = pRow("CODIGO").ToString()
                            ButtonCSala2.Text = pRow("NOMBRE").ToString()
                            ButtonCSala2.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonCSala2.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonCSala2.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonCSala3.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonCSala2.Image = Nothing
                                    ButtonCSala2.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonCSala2.Image = Nothing
                                ButtonCSala2.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonCSala2.Enabled = True
                        Case 3
                            ButtonCSala3.Tag = pRow("CODIGO").ToString()
                            ButtonCSala3.Text = pRow("NOMBRE").ToString()
                            ButtonCSala3.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonCSala3.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonCSala3.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonCSala3.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonCSala3.Image = Nothing
                                    ButtonCSala3.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonCSala3.Image = Nothing
                                ButtonCSala3.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonCSala3.Enabled = True
                        Case 4
                            ButtonCSala4.Tag = pRow("CODIGO").ToString()
                            ButtonCSala4.Text = pRow("NOMBRE").ToString()
                            ButtonCSala4.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonCSala4.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonCSala4.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonCSala4.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonCSala4.Image = Nothing
                                    ButtonCSala4.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonCSala4.Image = Nothing
                                ButtonCSala4.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonCSala4.Enabled = True
                        Case 5
                            ButtonCSala5.Tag = pRow("CODIGO").ToString()
                            ButtonCSala5.Text = pRow("NOMBRE").ToString()
                            ButtonCSala5.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonCSala5.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonCSala5.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonCSala5.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonCSala5.Image = Nothing
                                    ButtonCSala5.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonCSala5.Image = Nothing
                                ButtonCSala5.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonCSala5.Enabled = True
                        Case 6
                            ButtonCSala6.Tag = pRow("CODIGO").ToString()
                            ButtonCSala6.Text = pRow("NOMBRE").ToString()
                            ButtonCSala6.BackColor = Color.FromArgb(CInt(pRow("COLORFONDO")))
                            ButtonCSala6.ForeColor = Color.FromArgb(CInt(pRow("COLORTEXTO")))
                            If Len(pRow("LOGO").ToString().Trim) > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(pRow("LOGO").ToString().Trim)
                                If MiFileExist = True Then
                                    ButtonCSala6.Image = Image.FromFile(pRow("LOGO").ToString().Trim)
                                    ButtonCSala6.TextAlign = ContentAlignment.BottomCenter
                                Else
                                    ButtonCSala6.Image = Nothing
                                    ButtonCSala6.TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                ButtonCSala6.Image = Nothing
                                ButtonCSala6.TextAlign = ContentAlignment.MiddleCenter
                            End If
                            ButtonCSala6.Enabled = True
                    End Select
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
        Dim NumBTnMESAS As Integer = 42 : Dim IndBTnMESAS As Integer = 0
        Dim Wnfac As Integer = 0
        '
        Dim queryString As String = "SELECT * FROM [SALA1] WHERE "
        queryString = queryString & "[SALA1].[CAJA]=" & wCaja & " AND "
        queryString = queryString & "[SALA1].[CODIGO]='" & wCrgCodSala & "' "
        queryString = queryString & "ORDER BY CAST([SALA1].[MESA] AS INTEGER) ASC"
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA1")
            '
            If dt.Tables("SALA1").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("SALA1").Rows
                    IndBTnMESAS += 1
                    If IndBTnMESAS > NumBTnMESAS Then
                        Exit For
                    End If
                    '
                    ' Num. Factura :
                    '   -NULL-, 0 :: LIBRE
                    '   > 0      :: OCUPADA
                    '
                    If IsDBNull(pRow("FACTURA")) Then
                        Wnfac = 0
                    Else
                        Wnfac = CInt(pRow("FACTURA"))
                    End If
                    EstableceBotonMesa(IndBTnMESAS,
                                       pRow("MESA").ToString(),
                                       pRow("LOGO").ToString(),
                                       Wnfac)
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
        '   Se Activan aqui los botones para las mesas existentes de una SALA determinada.
        '
        Dim wImpo As Double
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 11) = "ButtonCMesa" AndAlso Mid$(NombreBoton, 12, 2) = IndiceMesa.ToString Then
                    With wControl
                        '
                        ' Texto a reflejar en la MESA.
                        '  LIBRES   = Cod. MESA  
                        '  OCUPADAS = Cod. MESA / IMPORTE Actual...
                        '
                        .Text = CodMesa
                        '
                        ' ... IMPORTE ...
                        If NumFac > 0 Then
                            wFacturaN = CInt(NumFac)
                            If ExisteRegistroMESAC(CodMesa, 1) Then
                                wImpo = CDbl(wrLeeMESAC.Mesac_IMPORTE)
                                .Text += vbCrLf & wImpo.ToString(fmtUnid).Replace(",", ".")
                            End If
                            .BackColor = Color.Red
                            .Enabled = False
                        Else
                            .BackColor = Color.Green
                            .Enabled = True
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

    Private Sub ButtonCCancelar_Click(sender As Object, e As EventArgs) Handles ButtonCCancelar.Click
        CerraForm()
    End Sub

    Private Sub ButtonCSala1_Click(sender As Object, e As EventArgs) Handles ButtonCSala1.Click
        '
        IniciaBotonesMesas()
        wCodSalaC = ButtonCSala1.Tag.ToString.Trim
        TextBoxCSALADESTINO.Text = wCodSalaC
        CargaMesas(wCodSalaC)
        '
    End Sub

    Private Sub ButtonCSala2_Click(sender As Object, e As EventArgs) Handles ButtonCSala2.Click
        '
        IniciaBotonesMesas()
        wCodSalaC = ButtonCSala2.Tag.ToString.Trim
        TextBoxCSALADESTINO.Text = wCodSalaC
        CargaMesas(wCodSalaC)
        '
    End Sub

    Private Sub ButtonCSala3_Click(sender As Object, e As EventArgs) Handles ButtonCSala3.Click
        '
        IniciaBotonesMesas()
        wCodSalaC = ButtonCSala3.Tag.ToString.Trim
        TextBoxCSALADESTINO.Text = wCodSalaC
        CargaMesas(wCodSalaC)
        '
    End Sub

    Private Sub ButtonCSala4_Click(sender As Object, e As EventArgs) Handles ButtonCSala4.Click
        '
        IniciaBotonesMesas()
        wCodSalaC = ButtonCSala4.Tag.ToString.Trim
        TextBoxCSALADESTINO.Text = wCodSalaC
        CargaMesas(wCodSalaC)
        '
    End Sub

    Private Sub ButtonCSala5_Click(sender As Object, e As EventArgs) Handles ButtonCSala5.Click
        '
        IniciaBotonesMesas()
        wCodSalaC = ButtonCSala5.Tag.ToString.Trim
        TextBoxCSALADESTINO.Text = wCodSalaC
        CargaMesas(wCodSalaC)
        '
    End Sub

    Private Sub ButtonCSala6_Click(sender As Object, e As EventArgs) Handles ButtonCSala6.Click
        '
        IniciaBotonesMesas()
        wCodSalaC = ButtonCSala6.Tag.ToString.Trim
        TextBoxCSALADESTINO.Text = wCodSalaC
        CargaMesas(wCodSalaC)
        '
    End Sub

    Private Sub ButtonCMesa1_Click(sender As Object, e As EventArgs) _
        Handles ButtonCMesa1.Click, ButtonCMesa9.Click, ButtonCMesa8.Click, ButtonCMesa7.Click,
        ButtonCMesa6.Click, ButtonCMesa5.Click, ButtonCMesa42.Click, ButtonCMesa41.Click,
        ButtonCMesa40.Click, ButtonCMesa4.Click, ButtonCMesa39.Click, ButtonCMesa38.Click, ButtonCMesa37.Click,
        ButtonCMesa36.Click, ButtonCMesa35.Click, ButtonCMesa34.Click, ButtonCMesa33.Click, ButtonCMesa32.Click,
        ButtonCMesa31.Click, ButtonCMesa30.Click, ButtonCMesa3.Click, ButtonCMesa29.Click, ButtonCMesa28.Click,
        ButtonCMesa27.Click, ButtonCMesa26.Click, ButtonCMesa25.Click, ButtonCMesa24.Click, ButtonCMesa23.Click,
        ButtonCMesa22.Click, ButtonCMesa21.Click, ButtonCMesa20.Click, ButtonCMesa2.Click, ButtonCMesa19.Click,
        ButtonCMesa18.Click, ButtonCMesa17.Click, ButtonCMesa16.Click, ButtonCMesa15.Click, ButtonCMesa14.Click,
        ButtonCMesa13.Click, ButtonCMesa12.Click, ButtonCMesa11.Click, ButtonCMesa10.Click
        '
        Me.TextBoxCMESADESTINO.Text = CType(sender, Button).Text
        '
    End Sub

    Private Sub ButtonCConfirma_Click(sender As Object, e As EventArgs) Handles ButtonCConfirma.Click
        '
        ' Confirmar Cambio de MESA
        '
        SalaOrigen = TextBoxCNumSala.Text.Trim
        MesaOrigen = TextBoxCNumMesa.Text.Trim
        SalaDestino = TextBoxCSALADESTINO.Text.Trim
        MesaDestino = TextBoxCMESADESTINO.Text.Trim
        '
        ' Algunas validaciones.
        '
        If SalaOrigen.Length = 0 Or MesaOrigen.Length = 0 Or
            SalaDestino.Length = 0 Or MesaDestino.Length = 0 Then
            msg = "Por favor entre SALAS/MESAS válidas. "
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Datos SALAS/MESAS Incorrectos."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        If SalaOrigen = "0" Or MesaOrigen = "0" Or
            SalaDestino = "0" Or MesaDestino = "0" Then
            msg = "Por favor entre SALAS/MESAS válidas. "
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Datos SALAS/MESAS Incorrectos."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        If LeeMesa_SALA1(SalaOrigen, MesaOrigen, 1) = False Or
                LeeMesa_SALA1(SalaDestino, MesaDestino, 1) = False Then
            msg = "Error leyendo Mesas Origen o Destino."
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error lectura en SALAS/MESAS."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        ' Nota: Llevamos los Datos de Tabla MESA Origen a Detino.
        '       Para Tabla MESAC (La cabecera) el registro es creado por la aplicación 
        '               automaticamente, ya que se esta actualizando constantemente.)
        '
        Cambio_MESA()
        CerraForm()
        '
    End Sub

    Private Sub Cambio_MESA()
        '
        ' Nota: Llevamos los Datos de Tabla MESA Origen a Detino.
        '       Para Tabla MESAC (La cabecera) el registro es creado por la aplicación 
        '               automaticamente, ya que se esta actualizando constantemente.)
        '-----------------------------------------------------------------------------
        '
        ' Efectuamos cambio Datos MESA sobre:
        '     Tabla SALA1.
        '     Tabla MESA.
        ' Efectuamos BORRADO Datos MESA sobre:
        '     Tabla MESA.
        '     Tabla MESAC.
        '
        ' wMesaLibre = True/False
        ' wFacturaN = CInt(pRow("FACTURA").ToString())
        ' FechaMESAC = Format(pRow("FECAPERTURA"), "dd/MM/yyyy")
        ' wVendedorApertura = CInt(pRow("VENDEDOR").ToString)
        '
        If LeeMesa_SALA1(SalaOrigen, MesaOrigen, 1) = True Then
            If wMesaLibre = False Then
                '
                ' Tomamos la Fecha Correcta = Fecha Apartura de la Mesa.
                '
                FechaMESAC = wrLeeSALA1.Sala1_FECAPERTURA.Trim
                '
                ' Destino: Factura, Vendedor, Fecha de Apertura
                '
                ActualizaMesa_SALA1(wCaja, SalaDestino, MesaDestino, 0)
                ActualizaMesa_SALA1(wCaja, SalaDestino, MesaDestino, 1)
                '
                ' Origen: Factura, Vendedor
                ' Nota, Fecha se puede quedar como esta, no afecta...
                '
                ActualizaMesa_SALA1(wCaja, SalaOrigen, MesaOrigen, 3)
                '
                ' Actualizamos SALA / MESA en Form TCONA402
                '
                MyFrm2.TextBoxNumSala.Text = SalaDestino
                MyFrm2.TextBoxNumMesa.Text = MesaDestino
                wCodSala = SalaDestino
                wCodMesa = MesaDestino
                '
                ' Datos a MESA Destino.
                '
                Origen_a_Dest_MESA()
                '
                ' BORRAR Datos en Origen!!!
                '
                BorrarOrigenes()
            End If
        Else
            Exit Sub
        End If
        '
    End Sub

    Private Sub Origen_a_Dest_MESA()
        '
        ' Datos de MESA ORIGEN a MESA DESTINO Tabla [MESA] 
        '
        Dim queryString As String = ""
        '
        ' Insert ...
        '
        queryString &= "INSERT INTO [MESA] ("
        '
        queryString &= "[MESA].[SALA],"
        queryString &= "[MESA].[MESA],"
        '
        queryString &= "[MESA].[NUMCAJA],"
        queryString &= "[MESA].[FECHA],"
        queryString &= "[MESA].[FACTURA],"
        queryString &= "[MESA].[ARTI],"
        queryString &= "[MESA].[COMBINA],"
        queryString &= "[MESA].[MEDIAPRECIO],"
        queryString &= "[MESA].[UNID], "
        queryString &= "[MESA].[IMPORTE], "
        queryString &= "[MESA].[PDTO], "
        queryString &= "[MESA].[IMPDTO], "
        queryString &= "[MESA].[VENDEDOR], "
        queryString &= "[MESA].[HORA], "
        queryString &= "[MESA].[COSTO], "
        queryString &= "[MESA].[ALMACEN], "
        queryString &= "[MESA].[IGIC], "
        queryString &= "[MESA].[NOZETA], "
        queryString &= "[MESA].[ORDENPLATO] "
        queryString &= ") "
        '
        ' Select ...
        '
        queryString &= "SELECT "
        queryString &= SalaDestino & ", "
        queryString &= MesaDestino & ", "
        queryString &= "[MESA].[NUMCAJA],"
        queryString &= "[MESA].[FECHA],"
        queryString &= "[MESA].[FACTURA],"
        queryString &= "[MESA].[ARTI],"
        queryString &= "[MESA].[COMBINA],"
        queryString &= "[MESA].[MEDIAPRECIO],"
        queryString &= "[MESA].[UNID], "
        queryString &= "[MESA].[IMPORTE], "
        queryString &= "[MESA].[PDTO], "
        queryString &= "[MESA].[IMPDTO], "
        queryString &= "[MESA].[VENDEDOR], "
        queryString &= "[MESA].[HORA], "
        queryString &= "[MESA].[COSTO], "
        queryString &= "[MESA].[ALMACEN], "
        queryString &= "[MESA].[IGIC], "
        queryString &= "[MESA].[NOZETA], "
        queryString &= "[MESA].[ORDENPLATO] "
        queryString &= " FROM [MESA] "
        '
        ' Filtro (Where) ...
        '
        queryString &= "WHERE "
        queryString &= "[MESA].[NUMCAJA]=" & wCaja & " AND "
        queryString &= "[MESA].[FECHA]='" & FechaMESAC & "' AND "
        queryString &= "[MESA].[SALA]='" & SalaOrigen & "' AND "
        queryString &= "[MESA].[MESA]='" & MesaOrigen & "' AND "
        queryString &= "[MESA].[FACTURA]=" & wFacturaN
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Cambio de Mesa : Comprobar Tabla [MESA]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub BorrarOrigenes()
        '
        ' Borramos los datos en MESA Origen
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        ' DELETE MESA.
        '
        queryString &= "DELETE [MESA] "
        '
        ' Filtro (Where) ...
        '
        queryString &= "WHERE "
        queryString &= "[MESA].[NUMCAJA]=" & wCaja & " AND "
        queryString &= "[MESA].[FECHA]='" & FechaMESAC & "' AND "
        queryString &= "[MESA].[SALA]='" & SalaOrigen & "' AND "
        queryString &= "[MESA].[MESA]='" & MesaOrigen & "' AND "
        queryString &= "[MESA].[FACTURA]=" & wFacturaN
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Borrando Datos Origen : [MESA]")
        End Try
        '
        '
        ' DELETE MESA.
        '
        queryString = ""
        '
        queryString &= "DELETE [MESAC] "
        '
        ' Filtro (Where) ...
        '
        queryString &= "WHERE "
        queryString &= "[MESAC].[NUMCAJA]=" & wCaja & " AND "
        queryString &= "[MESAC].[FECHA]='" & FechaMESAC & "' AND "
        queryString &= "[MESAC].[SALA]='" & SalaOrigen & "' AND "
        queryString &= "[MESAC].[MESA]='" & MesaOrigen & "' AND "
        queryString &= "[MESAC].[FACTURA]=" & wFacturaN
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Borrando Datos Origen : [MESAC]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

End Class