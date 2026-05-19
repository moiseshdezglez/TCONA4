Imports System.Data.SqlClient

Public Class TCONA413
    Dim NumFacReten As Integer = 0
    Dim AparcaSep As Boolean = False
    Private Sub CerrarForm()
        '
        Me.Close()
        '
    End Sub
    Private Sub TCONA413_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                CerrarForm()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA413_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Al cargar FORM cambio MESA
        '
        '
        ' Cargar Lista Cambio Mesa
        '
        Dim wPos As Integer = 0
        AparcaSep = False
        ButtonSepAparcar.Enabled = True
        ButtonSepCobrar.Enabled = True
        '
        GRID1_a_GRIDZOOM(2)
        LabelTotComandaSep.Text = MyFrm2.LabelTotComanda.Text
        Me.GRID1SepaMesa1.Rows.Clear()
        '
        LeeSALA(wCodSala)
        TextBoxSepNumSala.Text = wCodSala.ToString.Trim
        TextBoxSepNombreSala.Text = wrLeeSALA.Sala_NOMBRE
        TextBoxSepNumMesa.Text = MyFrm2.TextBoxNumMesa.Text
        TextBoxSepPAX.Text = MyFrm2.TextBoxPax.Text
        TextBoxSepCamarero.Text = MyFrm2.TextBoxCamarero.Text
        TextBoxSepFactura.Text = MyFrm2.TextBoxFactura.Text
        '
        ' Num. Mesa y Num. Factura Destino (Cuenta Separada)
        '
        'Dim MyTime As String = TimeOfDay.ToLongTimeString
        'Dim words As String() = MyTime.Split(New Char() {":"c})
        'MyTime = words(0).Trim & words(1).Trim & words(2).Trim
        '
        ' El contador actual en Ref. Generales está OCUPADO en una MESA
        ' Por tanto siempre Contador de FACTURA + 1
        '
        If LeeTCONA4Cfg("General") = True Then
            wrLeeTCONA4.Tcona4_FACTURA += 1
            wFacturaNSep = wrLeeTCONA4.Tcona4_FACTURA
            TextBoxSepFactura1.Text = wFacturaNSep.ToString.Trim
            '
            ' Mesa Destino
            '
            wPos = InStr(TextBoxSepNumMesa.Text.Trim, "-")
            If wPos > 0 Then
                Dim words As String() = TextBoxSepNumMesa.Text.Trim.Split(New Char() {"-"c})
                TextBoxSepNumMesa1.Text = words(0).Trim() & "-" & Microsoft.VisualBasic.Right(wrLeeTCONA4.Tcona4_FACTURA.ToString.Trim, 3)
            Else
                TextBoxSepFactura1.Text = wrLeeTCONA4.Tcona4_FACTURA.ToString.Trim
                TextBoxSepNumMesa1.Text = MyFrm2.TextBoxNumMesa.Text & "-" & Microsoft.VisualBasic.Right(wrLeeTCONA4.Tcona4_FACTURA.ToString.Trim, 3)
            End If
        End If
        '
    End Sub

    Private Sub ButtonSepCancelar_Click(sender As Object, e As EventArgs) Handles ButtonSepCancelar.Click
        CerrarForm()
    End Sub

    Private Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        '
        GRID1_a_GRIDZOOM(2)
        LabelTotComandaSep.Text = MyFrm2.LabelTotComanda.Text
        Me.GRID1SepaMesa1.Rows.Clear()
        CalculaTotales()
        '
    End Sub

    Private Sub Button1a2_Click(sender As Object, e As EventArgs) Handles Button1a2.Click
        '
        ' Datos GRID1 a GRID2
        '
        '   0 Cod. Art           (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N           (No visible)
        '   6 Cod. Combinados    (No Visible)
        '   7 Raciones           (No Visible)
        '   8 Orden Plato        (No Visible)
        '
        '
        ' Localiza Por Descripcion, ya que debe ser unica.
        ' Si ya esta en DESTINO (Cuenta Separada)
        '     suma  1 unidad a DESTINO
        '     Resta 1 unidad en ORIGEN
        '
        If GRID1SepaMesa.SelectedRows.Count > 0 Then
            '
            ' Recogemos Unidades Existentes
            '
            Dim MiUnid As Double = CDbl(GRID1SepaMesa.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
            Dim MiPVP As Double =
                (CDbl(GRID1SepaMesa.SelectedCells(4).Value.ToString.Trim.Replace(".", ",")) /
                CDbl(GRID1SepaMesa.SelectedCells(1).Value.ToString.Trim.Replace(".", ",")))
            Dim MiImpoO As Double = CDbl(GRID1SepaMesa.SelectedCells(4).Value.ToString.Trim.Replace(".", ","))
            Dim MiImpoD As Double = MiPVP
            MiImpoO -= MiPVP
            '
            If MiraLineaGrid2(GRID1SepaMesa.SelectedCells(2).Value.ToString, MiImpoD) = False Then

                '
                ' Si no esta pasamos 1 unidad
                '
                GRID1SepaMesa1.Rows.Add(GRID1SepaMesa.SelectedCells(0).Value.ToString,
                                        "1",
                                        GRID1SepaMesa.SelectedCells(2).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(3).Value.ToString,
                                        MiImpoD.ToString(fmtImporte).Replace(",", "."),
                                        GRID1SepaMesa.SelectedCells(5).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(6).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(7).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(8).Value.ToString
                                        )
                GRID1SepaMesa.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            ' Resta Unidad a Existentes, Si UNID=0 Borra linea de ORIGEN
            '
            MiUnid -= 1
            If MiUnid = 0 Then
                Me.GRID1SepaMesa.Rows.Remove(Me.GRID1SepaMesa.SelectedRows(0))
            Else
                GRID1SepaMesa.SelectedCells(1).Value = MiUnid.ToString(fmtUnid).Replace(",", ".")
                GRID1SepaMesa.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            CalculaTotales()
            '
        End If
        '
        If GRID1SepaMesa1.Rows.Count > 0 Then
            Button2a1.Enabled = True
            ButtonTODO2a1.Enabled = True
        End If
        '
    End Sub

    Private Sub Button2a1_Click(sender As Object, e As EventArgs) Handles Button2a1.Click
        '
        ' Datos GRID2 a GRID1
        '
        '   0 Cod. Art           (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N           (No visible)
        '   6 Cod. Combinados    (No Visible)
        '   7 Raciones           (No Visible)
        '   8 Orden Mesa         (No Visible)
        '
        '
        ' Localiza Por Descripcion, ya que debe ser unica.
        ' Si ya esta en DESTINO (Cuenta Separada)
        '     suma  1 unidad a DESTINO
        '     Resta 1 unidad en ORIGEN
        '
        If GRID1SepaMesa1.SelectedRows.Count > 0 Then
            '
            ' Recogemos Unidades Existentes
            '
            Dim MiUnid As Double = CDbl(GRID1SepaMesa1.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
            Dim MiPVP As Double =
                (CDbl(GRID1SepaMesa1.SelectedCells(4).Value.ToString.Trim.Replace(".", ",")) /
                CDbl(GRID1SepaMesa1.SelectedCells(1).Value.ToString.Trim.Replace(".", ",")))
            Dim MiImpoO As Double = CDbl(GRID1SepaMesa1.SelectedCells(4).Value.ToString.Trim.Replace(".", ","))
            Dim MiImpoD As Double = MiPVP
            MiImpoO -= MiPVP
            '
            If MiraLineaGrid1(GRID1SepaMesa1.SelectedCells(2).Value.ToString, MiImpoD) = False Then

                '
                ' Si no esta pasamos 1 unidad
                '
                GRID1SepaMesa.Rows.Add(GRID1SepaMesa1.SelectedCells(0).Value.ToString,
                                       "1",
                                       GRID1SepaMesa1.SelectedCells(2).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(3).Value.ToString,
                                       MiImpoD.ToString(fmtImporte).Replace(",", "."),
                                       GRID1SepaMesa1.SelectedCells(5).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(6).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(7).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(8).Value.ToString
                                        )
                GRID1SepaMesa1.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            ' Resta Unidad a Existentes, Si UNID=0 Borra linea de ORIGEN
            '
            MiUnid -= 1
            If MiUnid = 0 Then
                Me.GRID1SepaMesa1.Rows.Remove(Me.GRID1SepaMesa1.SelectedRows(0))
            Else
                GRID1SepaMesa1.SelectedCells(1).Value = MiUnid.ToString(fmtUnid).Replace(",", ".")
                GRID1SepaMesa1.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            CalculaTotales()
            '
        End If
        '
        If GRID1SepaMesa1.Rows.Count = 0 Then
            Button2a1.Enabled = False
            ButtonTODO2a1.Enabled = False
        End If
        '
    End Sub

    Private Function MiraLineaGrid2(BusDescrip As String, wImpo As Double) As Boolean
        '
        ' Comprobar Existencia de un PRODUCTO en GRID Destino
        '
        MiraLineaGrid2 = False
        If GRID1SepaMesa1.Rows.Count = 0 Then
            Exit Function
        End If
        '
        GRID1SepaMesa1.Visible = False
        For Each row As DataGridViewRow In GRID1SepaMesa1.Rows
            If row.Cells(2).Value.ToString.Trim = BusDescrip Then
                '
                ' Suma Unidad / Importe a Existentes
                '
                Dim MiUnid1 As Double = CDbl(row.Cells(1).Value.ToString.Trim.Replace(".", ","))
                MiUnid1 += 1
                row.Cells(1).Value = MiUnid1.ToString(fmtUnid).Replace(",", ".")
                wImpo += CDbl(row.Cells(4).Value.ToString.Trim.Replace(".", ","))
                row.Cells(4).Value = wImpo.ToString(fmtUnid).Replace(",", ".")
                MiraLineaGrid2 = True
            End If
        Next
        GRID1SepaMesa1.Visible = True
        '
    End Function

    Private Function MiraLineaGrid1(BusDescrip As String, wImpo As Double) As Boolean
        '
        ' Comprobar Existencia de un PRODUCTO en GRID Origen
        '
        MiraLineaGrid1 = False
        If GRID1SepaMesa.Rows.Count = 0 Then
            Exit Function
        End If
        '
        GRID1SepaMesa.Visible = False
        For Each row As DataGridViewRow In GRID1SepaMesa.Rows
            If row.Cells(2).Value.ToString.Trim = BusDescrip Then
                '
                ' Suma Unidad / Importe a Existentes
                '
                Dim MiUnid1 As Double = CDbl(row.Cells(1).Value.ToString.Trim.Replace(".", ","))
                MiUnid1 += 1
                row.Cells(1).Value = MiUnid1.ToString(fmtUnid).Replace(",", ".")
                wImpo += CDbl(row.Cells(4).Value.ToString.Trim.Replace(".", ","))
                row.Cells(4).Value = wImpo.ToString(fmtUnid).Replace(",", ".")
                MiraLineaGrid1 = True
            End If
        Next
        GRID1SepaMesa.Visible = True
        '
    End Function

    Private Sub CalculaTotales()
        '
        ' Recalculamos TOTALES
        '
        Dim MiTOTAL As Double = 0
        '
        GRID1SepaMesa1.Visible = False
        For Each row As DataGridViewRow In GRID1SepaMesa1.Rows
            '
            ' Suma Unidad a Existentes
            '
            MiTOTAL += CDbl(row.Cells(4).Value.ToString.Trim.Replace(".", ","))
        Next
        LabelTotComandaSep1.Text = MiTOTAL.ToString(fmtImporte).Replace(",", ".")
        '
        GRID1SepaMesa1.Visible = True

        If GRID1SepaMesa1.Rows.Count = 0 Then
            Exit Sub
        End If
        '
        MiTOTAL = 0
        '
        GRID1SepaMesa.Visible = False
        For Each row As DataGridViewRow In GRID1SepaMesa.Rows
            '
            ' Suma Unidad a Existentes
            '
            MiTOTAL += CDbl(row.Cells(4).Value.ToString.Trim.Replace(".", ","))
        Next
        LabelTotComandaSep.Text = MiTOTAL.ToString(fmtImporte).Replace(",", ".")
        '
        GRID1SepaMesa.Visible = True
        '
    End Sub

    Private Sub ButtonTODO1a2_Click(sender As Object, e As EventArgs) Handles ButtonTODO1a2.Click
        '
        ' Datos GRID1 a GRID2
        '
        '   0 Cod. Art           (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N           (No visible)
        '   6 Cod. Combinados    (No Visible)
        '   7 Raciones           (No Visible)
        '   8 Orden Plato        (No Visible)
        '
        '
        ' Localiza Por Descripcion, ya que debe ser unica.
        ' Si ya esta en DESTINO (Cuenta Separada)
        '     suma  TODAS las unidades a DESTINO
        '     Resta TODAS las unidades en ORIGEN
        '
        If GRID1SepaMesa.SelectedRows.Count > 0 Then
            '
            ' Recogemos Unidades Existentes
            '
            Dim MiUnid As Double = CDbl(GRID1SepaMesa.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
            Dim MiPVP As Double =
                (CDbl(GRID1SepaMesa.SelectedCells(4).Value.ToString.Trim.Replace(".", ",")) /
                CDbl(GRID1SepaMesa.SelectedCells(1).Value.ToString.Trim.Replace(".", ",")))
            Dim MiImpoO As Double = CDbl(GRID1SepaMesa.SelectedCells(4).Value.ToString.Trim.Replace(".", ","))
            Dim MiImpoD As Double = MiPVP
            MiImpoO -= MiPVP
            '
            If MiraLineaGrid2(GRID1SepaMesa.SelectedCells(2).Value.ToString, MiImpoD) = False Then

                '
                ' Si no esta pasamos TODAS las unidades
                '
                GRID1SepaMesa1.Rows.Add(GRID1SepaMesa.SelectedCells(0).Value.ToString,
                                        MiUnid.ToString(fmtImporte).Replace(",", "."),
                                        GRID1SepaMesa.SelectedCells(2).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(3).Value.ToString,
                                        MiImpoD.ToString(fmtImporte).Replace(",", "."),
                                        GRID1SepaMesa.SelectedCells(5).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(6).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(7).Value.ToString,
                                        GRID1SepaMesa.SelectedCells(8).Value.ToString
                                        )
                GRID1SepaMesa.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            ' Resta Unidad a Existentes, Si UNID=0 Borra linea de ORIGEN
            '
            MiUnid -= MiUnid
            If MiUnid = 0 Then
                Me.GRID1SepaMesa.Rows.Remove(Me.GRID1SepaMesa.SelectedRows(0))
            Else
                GRID1SepaMesa.SelectedCells(1).Value = MiUnid.ToString(fmtUnid).Replace(",", ".")
                GRID1SepaMesa.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            CalculaTotales()
            '
        End If
        '
        If GRID1SepaMesa1.Rows.Count > 0 Then
            Button2a1.Enabled = True
            ButtonTODO2a1.Enabled = True
        End If
        '
    End Sub

    Private Sub ButtonTODO2a1_Click(sender As Object, e As EventArgs) Handles ButtonTODO2a1.Click
        '
        ' Datos GRID2 a GRID1
        '
        '   0 Cod. Art           (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N           (No visible)
        '   6 Cod. Combinados    (No Visible)
        '   7 Raciones           (No Visible)
        '   8 Orden Mesa         (No Visible)
        '
        '
        ' Localiza Por Descripcion, ya que debe ser unica.
        ' Si ya esta en DESTINO (Cuenta Separada)
        '     suma  TODAS las unidades a DESTINO
        '     Resta unidades en ORIGEN
        '
        If GRID1SepaMesa1.SelectedRows.Count > 0 Then
            '
            ' Recogemos Unidades Existentes
            '
            Dim MiUnid As Double = CDbl(GRID1SepaMesa1.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
            Dim MiPVP As Double =
                (CDbl(GRID1SepaMesa1.SelectedCells(4).Value.ToString.Trim.Replace(".", ",")) /
                CDbl(GRID1SepaMesa1.SelectedCells(1).Value.ToString.Trim.Replace(".", ",")))
            Dim MiImpoO As Double = CDbl(GRID1SepaMesa1.SelectedCells(4).Value.ToString.Trim.Replace(".", ","))
            Dim MiImpoD As Double = MiPVP
            MiImpoO -= MiPVP
            '
            If MiraLineaGrid1(GRID1SepaMesa1.SelectedCells(2).Value.ToString, MiImpoD) = False Then

                '
                ' Si no esta pasamos 1 unidad
                '
                GRID1SepaMesa.Rows.Add(GRID1SepaMesa1.SelectedCells(0).Value.ToString,
                                       MiUnid.ToString(fmtImporte).Replace(",", "."),
                                       GRID1SepaMesa1.SelectedCells(2).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(3).Value.ToString,
                                       MiImpoD.ToString(fmtImporte).Replace(",", "."),
                                       GRID1SepaMesa1.SelectedCells(5).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(6).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(7).Value.ToString,
                                       GRID1SepaMesa1.SelectedCells(8).Value.ToString
                                        )
                GRID1SepaMesa1.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            ' Resta Unidad a Existentes, Si UNID=0 Borra linea de ORIGEN
            '
            MiUnid -= MiUnid
            If MiUnid = 0 Then
                Me.GRID1SepaMesa1.Rows.Remove(Me.GRID1SepaMesa1.SelectedRows(0))
            Else
                GRID1SepaMesa1.SelectedCells(1).Value = MiUnid.ToString(fmtUnid).Replace(",", ".")
                GRID1SepaMesa1.SelectedCells(4).Value = MiImpoO.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            CalculaTotales()
            '
        End If
        '
        If GRID1SepaMesa1.Rows.Count = 0 Then
            Button2a1.Enabled = False
            ButtonTODO2a1.Enabled = False
        End If
        '
    End Sub

    Private Sub ButtonSepAparcar_Click(sender As Object, e As EventArgs) Handles ButtonSepAparcar.Click
        '
        ' Aparcar la cuenta separada a una NUEVA mesa en Sala 999
        '
        HazAparcarSep()
        CerrarForm()
        '
    End Sub

    Private Sub HazAparcarSep()
        '
        ' Aparcar la cuenta separada a una NUEVA mesa en Sala 999
        '
        If GRID1SepaMesa1.Rows.Count > 0 Then
            ButtonSepAparcar.Enabled = False
            CreaMESASepa()   ' [SALA1]
            CreaMESAC()      ' [MESAC]
            CreaMESA()       ' [MESA]
            '
            ' Para actualizar [MESAC] la CABECERA me vale este procedimiento Global
            ' Básicamente se actualiza el IMPORTE
            '
            ActualizaDatosMESAC(TextBoxSepNumMesa.Text.Trim, 3)
            '
            ' Esto puede NO ser necesario
            ' Aparcar en TCONA402 lo solucionará...
            '
            MyFrm2.LabelTotComanda.Text = Me.LabelTotComandaSep.Text.Trim
            '
            ActualizaMESA()  ' [MESA]
            CargaListaMESAs(TextBoxSepNumMesa.Text.Trim)
            AparcaSep = True
        End If
        '
    End Sub

    Private Sub CreaMESASepa()
        '
        ' NUEVA MESA [SALA1]
        '
        ' Leemos Datos de la MESA Origen
        '
        LeeMesa_SALA1(TextBoxSepNumSala.Text.Trim,
                      TextBoxSepNumMesa.Text.Trim, 1)
        '
        Dim queryString As String = ""
        '
        ' INSERT
        '
        queryString = queryString & "INSERT INTO [SALA1] ("
        queryString = queryString & "[SALA1].[CAJA],"
        queryString = queryString & "[SALA1].[CODIGO],"
        queryString = queryString & "[SALA1].[MESA],"
        queryString = queryString & "[SALA1].[MESA1],"
        queryString = queryString & "[SALA1].[DESCMESA],"
        queryString = queryString & "[SALA1].[PVP],"
        queryString = queryString & "[SALA1].[PIDEPAX],"
        queryString = queryString & "[SALA1].[LOGO],"
        queryString = queryString & "[SALA1].[VISIBLE],"
        queryString = queryString & "[SALA1].[FACTURA],"
        queryString = queryString & "[SALA1].[FECAPERTURA],"
        queryString = queryString & "[SALA1].[VENDEDOR],"
        queryString = queryString & "[SALA1].[PAX], "
        queryString = queryString & "[SALA1].[HORAAPAERTURA] "
        queryString = queryString & ") Values ("
        queryString = queryString & wCaja & ", "
        queryString = queryString & "'999', "                                  ' SALA 999
        queryString = queryString & "'" & TextBoxSepNumMesa1.Text.Trim & "', " ' NUEVA MESA
        queryString = queryString & "'" & TextBoxSepNumMesa1.Text.Trim & "', " ' NUEVA MESA
        queryString = queryString & "'Cuenta Separada', "                      ' DESCRIP
        queryString = queryString & "'" & CDbl(wrLeeSALA1.Sala1_PVP).ToString(fmtPrecio).Replace(",", ".") & "', " ' PVP
        If wrLeeSALA1.Sala1_PIDEPAX = "True" Then                              ' PIDEPAX
            queryString = queryString & 1 & ", "
        Else
            queryString = queryString & 0 & ", "
        End If
        queryString = queryString & "'" & wrLeeSALA1.Sala1_LOGO & "', "  ' LOGO
        If wrLeeSALA1.Sala1_VISIBLE = "True" Then                        ' VISIBLE
            queryString = queryString & 1 & ", "
        Else
            queryString = queryString & 0 & ", "
        End If
        '
        ' Nro. de Factura. Actual + 1 (Actual ocupa una MESA)
        ' Por si no lee TCONA4, improbable, pero vete a saber
        '  le asigno 999999999
        '
        queryString = queryString & wFacturaNSep.ToString.Trim & ", "
        '
        ' Aqui sumamos 1 al contador de FACTURAS
        '
        NumFacReten = wFacturaNSep
        Actualiza_TCONA4(wCaja, "FacturaSep")
        '
        ' Fec. Apertura de MESA Origen o FECHA ACTUAL ?
        ' Por ahora Fecha Actual ... ... ...
        '
        Dim MiFecAper As String = Date.Now.ToShortDateString
        Dim MiHoraAper As String = Date.Now.ToShortTimeString
        'queryString = queryString & "'" & wrLeeSALA1.Sala1_FECAPERTURA & "', " ' FEC. APERTURA
        '
        queryString = queryString & "'" & MiFecAper & "', " ' FEC. APERTURA
        queryString = queryString & wrLeeSALA1.Sala1_VENDEDOR & ", "           ' VENDEDOR
        queryString = queryString & wrLeeSALA1.Sala1_PAX & ", "               ' PAX
        queryString = queryString & "'" & MiHoraAper & "' "   ' HORA APERTURA
        queryString = queryString & ")"
        '
        ' Se crea la MESA !!!
        '
        Dim conexion As New SqlClient.SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                    MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly,
                   "Comprobar Tabla Grabando Datos [SALA1]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CreaMESAC()
        '
        ' Genera Cabecera de MESAS [MESAC] para la NUEVA mesa. 
        ' Cuenta separada.
        '
        ' MESAC :: Cabecera
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '
        Dim queryString As String = ""
        '
        ' Recogemos los DATOS para CEBECERA MESAS
        '
        ' Validación: 
        '   Vendedor (Camarero)
        '  
        '   IMPORTE Cuenta Separada
        '
        If LabelTotComandaSep1.Text.Length = 0 Or Not IsNumeric(LabelTotComandaSep1.Text) Then
            LabelTotComandaSep1.Text = "0"
        End If
        Dim WiMPORTE As String = LabelTotComandaSep1.Text.ToString.Trim
        WiMPORTE.Replace(",", ".")
        '
        ' Cálculos % IGIC // IMPORTE IGIC
        '
        LeeTCONA4Cfg("General")
        Dim Mitotal As Double = CDbl(LabelTotComandaSep1.Text.Replace(".", ",").Trim)
        Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
        Dim Micalculo As Double = (MiPorIGIC / 100) + 1
        Dim MiBase As Double = Math.Round((Mitotal / Micalculo), 2)
        Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
        Dim MiFecAper As String = Date.Now.ToShortDateString
        '
        With wrMESAC
            .Mesac_EMPRESA = 1
            .Mesac_CLIENTE = wCliente
            .Mesac_VENDEDOR = CInt(TextBoxSepCamarero.Text.Trim)
            .Mesac_PIGIC = MiPorIGIC.ToString.Replace(",", ".")
            .Mesac_PDTO = "0.00"
            .Mesac_IMPDTO = "0.00"
            .Mesac_IMPORTE = WiMPORTE
            .Mesac_ENTREGA = "0.00"
            .Mesac_TARJETA = "0.00"
            .Mesac_VALEDTO = "0.00"
            .Mesac_CHEQUES = "0.00"
            .Mesac_OTROS = "0.00"
            .Mesac_EFECTIVO = "0.00"
            .Mesac_IMPIGIC = MiImpIGIC.ToString.Replace(",", ".")
            '
            ' Pedidos / Clientes Contado (NIF/CIF)
            ' Clientes Crédito se recoge en wCliente
            '
            .Mesac_NIFCIF = WMesacNIFCIF
            .Mesac_TLFPEDIDOS = WMesacTlfPed
            '
        End With
        '
        ' INSERT
        '
        queryString = queryString & "INSERT INTO [MESAC] ("
        queryString = queryString & "[MESAC].[NUMCAJA],"
        queryString = queryString & "[MESAC].[FECHA],"
        queryString = queryString & "[MESAC].[SALA],"
        queryString = queryString & "[MESAC].[MESA],"
        queryString = queryString & "[MESAC].[FACTURA],"
        queryString = queryString & "[MESAC].[EMPRESA],"
        queryString = queryString & "[MESAC].[CLIENTE],"
        queryString = queryString & "[MESAC].[VENDEDOR],"
        queryString = queryString & "[MESAC].[PIGIC],"
        queryString = queryString & "[MESAC].[PDTO],"
        queryString = queryString & "[MESAC].[IMPDTO],"
        queryString = queryString & "[MESAC].[IMPORTE],"
        queryString = queryString & "[MESAC].[ENTREGA],"
        queryString = queryString & "[MESAC].[TARJETA],"
        queryString = queryString & "[MESAC].[VALEDTO],"
        queryString = queryString & "[MESAC].[CHEQUES],"
        queryString = queryString & "[MESAC].[OTROS],"
        queryString = queryString & "[MESAC].[EFECTIVO],"
        queryString = queryString & "[MESAC].[IMPIGIC],"
        queryString = queryString & "[MESAC].[TLFPEDIDOS]"
        queryString = queryString & ") Values ("
        queryString = queryString & wCaja & ", "
        queryString = queryString & "'" & MiFecAper & "', "
        queryString = queryString & "'999', "
        queryString = queryString & "'" & TextBoxSepNumMesa1.Text.Trim & "', "
        queryString = queryString & NumFacReten & ", "
        queryString = queryString & wrMESAC.Mesac_EMPRESA & ", "
        queryString = queryString & wrMESAC.Mesac_CLIENTE & ", "
        queryString = queryString & wrMESAC.Mesac_VENDEDOR & ", "
        queryString = queryString & "'" & wrMESAC.Mesac_PIGIC & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_PDTO & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_IMPDTO & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_IMPORTE & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_ENTREGA & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_TARJETA & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_VALEDTO & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_CHEQUES & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_OTROS & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_EFECTIVO & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_IMPIGIC & "', "
        queryString = queryString & "'" & wrMESAC.Mesac_TLFPEDIDOS & "' "
        queryString = queryString & ")"
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Grabando Datos [MESAC]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CreaMESA()
        '
        ' MESA :: Datos Nueva MESA Cuenta Separada.
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '      ARTI =  GRID1
        '      COMBINA = String COMBINADOS
        '      MEDIAPRECIO = Datos Varios ...
        '
        If TextBoxSepCamarero.Text.Length = 0 Or Not IsNumeric(TextBoxSepCamarero.Text) Then
            TextBoxSepCamarero.Text = "1"
        End If
        '
        ' Almacen ...
        '
        LeeTCONA4Cfg("Almacen")
        '
        ' Fecha / Hora
        '
        Dim MiFecAper As String = Date.Now.ToShortDateString
        Dim HoraMESA As String = TimeOfDay.ToLongTimeString.ToString
        '
        Dim wTmpUnid As Decimal = 0 : Dim wTmpImporte As Decimal = 0
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        '   Recorrer las Lineas en el GRID.
        '
        If GRID1SepaMesa1.Rows.Count > 0 Then
            For Each row As DataGridViewRow In GRID1SepaMesa1.Rows
                '
                ' Recogemos los DATOS para MESA
                '
                ' row.Cells(0).Value.ToString | Cod. Articulo
                ' row.Cells(1).Value.ToString | Unidades Ex.
                ' row.Cells(2).Value.ToString | Descripcion Articulo
                ' row.Cells(3).Value.ToString | Unidades Nue
                ' row.Cells(4).Value.ToString | importe
                ' row.Cells(5).Value.ToString | "E", "N" : Existente, Nueva
                ' row.Cells(6).Value.ToString | Codigos COMBINADOS
                ' row.Cells(7).Value.ToString | MediaPrecio, Raciones
                ' row.Cells(8).Value.ToString | Orden Plato
                '
                LeeMar(row.Cells(0).Value.ToString.Trim)
                '
                With wrMESA
                    '
                    ' Aqui tomamos las Unid. Existentes
                    '
                    .Mesa_UNID = row.Cells(1).Value.ToString.Replace(",", ".")
                    .Mesa_IMPORTE = row.Cells(4).Value.ToString.Replace(",", ".")
                    .Mesa_PDTO = "0.00"
                    .Mesa_IMPDTO = "0.00"
                    .Mesa_VENDEDOR = CInt(TextBoxSepCamarero.Text.Trim)
                    .Mesa_HORA = HoraMESA
                    .Mesa_COSTO = wrLeeMAR.Mar_PRECOSTO.Replace(",", ".")
                    .Mesa_ALMACEN = wAlmacen
                    .Mesa_IGIC = wrLeeMAR.Mar_IVAVENTA.Replace(",", ".")
                    .Mesa_SALA = "999"
                    .Mesa_ORDENPLATO = CInt(row.Cells(8).Value.ToString)
                    '
                    ' NOZETA, SALA
                    '
                    If LeeSALA("999") = True Then
                        .Mesa_NOZETA = wrLeeSALA.Sala_NOZETA
                    Else
                        .Mesa_NOZETA = 0
                    End If
                End With
                '
                ' COMBINADO
                '
                wStringCombinados = " "
                If row.Cells(6).Value.ToString.Trim.Length > 0 Then
                    wStringCombinados = row.Cells(6).Value.ToString
                End If
                '
                ' MEDIAPRECIO (RACIONES)
                '
                wMediaPrecio = " "
                If row.Cells(7).Value.ToString.Trim.Length > 0 Then
                    wMediaPrecio = row.Cells(7).Value.ToString
                End If
                '
                queryString = ""
                '
                ' INSERT - Registro MESA Nuevo.
                '
                wTmpUnid = CDec(wrMESA.Mesa_UNID.ToString.Replace(".", ","))
                '
                queryString = queryString & "INSERT INTO [MESA] ("
                queryString = queryString & "[MESA].[NUMCAJA],"
                queryString = queryString & "[MESA].[FECHA],"
                queryString = queryString & "[MESA].[SALA],"
                queryString = queryString & "[MESA].[MESA],"
                queryString = queryString & "[MESA].[FACTURA],"
                queryString = queryString & "[MESA].[ARTI],"
                queryString = queryString & "[MESA].[COMBINA],"
                queryString = queryString & "[MESA].[MEDIAPRECIO],"
                queryString = queryString & "[MESA].[UNID], "
                queryString = queryString & "[MESA].[IMPORTE], "
                queryString = queryString & "[MESA].[PDTO], "
                queryString = queryString & "[MESA].[IMPDTO], "
                queryString = queryString & "[MESA].[VENDEDOR], "
                queryString = queryString & "[MESA].[HORA], "
                queryString = queryString & "[MESA].[COSTO], "
                queryString = queryString & "[MESA].[ALMACEN], "
                queryString = queryString & "[MESA].[IGIC], "
                queryString = queryString & "[MESA].[NOZETA], "
                queryString = queryString & "[MESA].[NUMZETA], "
                queryString = queryString & "[MESA].[ORDENPLATO] "
                queryString = queryString & ") Values ("
                queryString = queryString & wCaja & ", "
                queryString = queryString & "'" & MiFecAper & "', "
                queryString = queryString & "'999', "
                queryString = queryString & "'" & TextBoxSepNumMesa1.Text.Trim & "', "
                queryString = queryString & NumFacReten & ", "
                queryString = queryString & "'" & row.Cells(0).Value.ToString.Trim & "', "
                queryString = queryString & "'" & wStringCombinados & "', "
                queryString = queryString & "'" & wMediaPrecio & "', "
                queryString = queryString & "'" & wTmpUnid.ToString.Replace(",", ".") & "', "
                queryString = queryString & "'" & wrMESA.Mesa_IMPORTE & "', "
                queryString = queryString & "'" & wrMESA.Mesa_PDTO & "', "
                queryString = queryString & "'" & wrMESA.Mesa_IMPDTO & "', "
                queryString = queryString & wrMESA.Mesa_VENDEDOR & ", "
                queryString = queryString & "'" & wrMESA.Mesa_HORA & "', "
                queryString = queryString & "'" & wrMESA.Mesa_COSTO & "', "
                queryString = queryString & "'" & wrMESA.Mesa_ALMACEN & "', "
                queryString = queryString & "'" & wrMESA.Mesa_IGIC & "', "
                queryString = queryString & wrMESA.Mesa_NOZETA & ", "
                queryString = queryString & 0 & ", " ' Num. Zeta=0
                queryString = queryString & wrMESA.Mesa_ORDENPLATO & " "
                queryString = queryString & ")"
                Try
                    cmd.CommandText = queryString
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Comprobar Tabla, Grabando Datos [MESA]")
                End Try
            Next
        End If
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub ActualizaMESA()
        '
        ' Actualizamos los DATOS de la MESA Origen
        ' Se hace en dos pasos:
        '  (1) Borra lo que hay en la MESA por su Nro. FACTURA
        '  (2) Creamos de nuevo con los DATOS despues de separar...
        '
        '
        ' MESA :: Datos MESA Origen.
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '      ARTI =  GRID1
        '      COMBINA = String COMBINADOS
        '      MEDIAPRECIO = Datos Varios ...
        '
        If TextBoxSepCamarero.Text.Length = 0 Or Not IsNumeric(TextBoxSepCamarero.Text) Then
            TextBoxSepCamarero.Text = "1"
        End If
        '
        ' Almacen ...
        '
        LeeTCONA4Cfg("Almacen")
        '
        ' Fecha / Hora
        '
        Dim MiFecAper As String = FechaMESAC
        Dim HoraMESA As String = TimeOfDay.ToLongTimeString.ToString
        '
        Dim wTmpUnid As Decimal = 0 : Dim wTmpImporte As Decimal = 0
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Eliminamos Los Movimientos Existentes de la FACTURA ORIGEN
        '
        queryString = "DELETE FROM [MESA] WHERE "
        queryString &= "[MESA].[NUMCAJA]=" & wCaja & " AND"
        queryString &= "[MESA].[FACTURA]=" & CInt(TextBoxSepFactura.Text.Trim) & " "
        cmd.CommandText = queryString
        cmd.Connection = conexion
        cmd.ExecuteNonQuery()
        '
        '   Recorrer las Lineas en el GRID.
        '   Actualizamos con los NUEVOS datos despues de SEPARAR.
        '
        If GRID1SepaMesa.Rows.Count > 0 Then
            For Each row As DataGridViewRow In GRID1SepaMesa.Rows
                '
                ' Recogemos los DATOS para MESA
                '
                ' row.Cells(0).Value.ToString | Cod. Articulo
                ' row.Cells(1).Value.ToString | Unidades Ex.
                ' row.Cells(2).Value.ToString | Descripcion Articulo
                ' row.Cells(3).Value.ToString | Unidades Nue
                ' row.Cells(4).Value.ToString | importe
                ' row.Cells(5).Value.ToString | "E", "N" : Existente, Nueva
                ' row.Cells(6).Value.ToString | Codigos COMBINADOS
                ' row.Cells(7).Value.ToString | MediaPrecio, Raciones
                ' row.Cells(8).Value.ToString | Orden Plato
                '
                LeeMar(row.Cells(0).Value.ToString.Trim)
                '
                With wrMESA
                    '
                    ' Aqui tomamos las Unid. Existentes
                    '
                    .Mesa_UNID = row.Cells(1).Value.ToString.Replace(",", ".")
                    .Mesa_IMPORTE = row.Cells(4).Value.ToString.Replace(",", ".")
                    .Mesa_PDTO = "0.00"
                    .Mesa_IMPDTO = "0.00"
                    .Mesa_VENDEDOR = CInt(TextBoxSepCamarero.Text.Trim)
                    .Mesa_HORA = HoraMESA
                    .Mesa_COSTO = wrLeeMAR.Mar_PRECOSTO.Replace(",", ".")
                    .Mesa_ALMACEN = wAlmacen
                    .Mesa_IGIC = wrLeeMAR.Mar_IVAVENTA.Replace(",", ".")
                    .Mesa_SALA = wCodSala
                    .Mesa_ORDENPLATO = CInt(row.Cells(8).Value.ToString)
                    '
                    ' NOZETA, SALA
                    '
                    If LeeSALA(wCodSala) = True Then
                        .Mesa_NOZETA = wrLeeSALA.Sala_NOZETA
                    Else
                        .Mesa_NOZETA = 0
                    End If
                End With
                '
                ' COMBINADO
                '
                wStringCombinados = " "
                If row.Cells(6).Value.ToString.Trim.Length > 0 Then
                    wStringCombinados = row.Cells(6).Value.ToString
                End If
                '
                ' MEDIAPRECIO (RACIONES)
                '
                wMediaPrecio = " "
                If row.Cells(7).Value.ToString.Trim.Length > 0 Then
                    wMediaPrecio = row.Cells(7).Value.ToString
                End If
                '
                queryString = ""
                '
                ' INSERT - Registro MESA Nuevo.
                '
                wTmpUnid = CDec(wrMESA.Mesa_UNID.ToString.Replace(".", ","))
                '
                queryString = queryString & "INSERT INTO [MESA] ("
                queryString = queryString & "[MESA].[NUMCAJA],"
                queryString = queryString & "[MESA].[FECHA],"
                queryString = queryString & "[MESA].[SALA],"
                queryString = queryString & "[MESA].[MESA],"
                queryString = queryString & "[MESA].[FACTURA],"
                queryString = queryString & "[MESA].[ARTI],"
                queryString = queryString & "[MESA].[COMBINA],"
                queryString = queryString & "[MESA].[MEDIAPRECIO],"
                queryString = queryString & "[MESA].[UNID], "
                queryString = queryString & "[MESA].[IMPORTE], "
                queryString = queryString & "[MESA].[PDTO], "
                queryString = queryString & "[MESA].[IMPDTO], "
                queryString = queryString & "[MESA].[VENDEDOR], "
                queryString = queryString & "[MESA].[HORA], "
                queryString = queryString & "[MESA].[COSTO], "
                queryString = queryString & "[MESA].[ALMACEN], "
                queryString = queryString & "[MESA].[IGIC], "
                queryString = queryString & "[MESA].[NOZETA], "
                queryString = queryString & "[MESA].[NUMZETA], "
                queryString = queryString & "[MESA].[ORDENPLATO] "
                queryString = queryString & ") Values ("
                queryString = queryString & wCaja & ", "
                queryString = queryString & "'" & MiFecAper & "', "
                queryString = queryString & "'" & wCodSala & "', "
                queryString = queryString & "'" & TextBoxSepNumMesa.Text.Trim & "', "
                queryString = queryString & CInt(TextBoxSepFactura.Text.Trim) & ", "
                queryString = queryString & "'" & row.Cells(0).Value.ToString.Trim & "', "
                queryString = queryString & "'" & wStringCombinados & "', "
                queryString = queryString & "'" & wMediaPrecio & "', "
                queryString = queryString & "'" & wTmpUnid.ToString.Replace(",", ".") & "', "
                queryString = queryString & "'" & wrMESA.Mesa_IMPORTE & "', "
                queryString = queryString & "'" & wrMESA.Mesa_PDTO & "', "
                queryString = queryString & "'" & wrMESA.Mesa_IMPDTO & "', "
                queryString = queryString & wrMESA.Mesa_VENDEDOR & ", "
                queryString = queryString & "'" & wrMESA.Mesa_HORA & "', "
                queryString = queryString & "'" & wrMESA.Mesa_COSTO & "', "
                queryString = queryString & "'" & wrMESA.Mesa_ALMACEN & "', "
                queryString = queryString & "'" & wrMESA.Mesa_IGIC & "', "
                queryString = queryString & wrMESA.Mesa_NOZETA & ", "
                queryString = queryString & 0 & ", " ' Num. Zeta=0
                queryString = queryString & wrMESA.Mesa_ORDENPLATO & " "
                queryString = queryString & ")"
                Try
                    cmd.CommandText = queryString
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Comprobar Tabla, Grabando Datos [MESA]")
                End Try
            Next
        End If
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub ButtonSepCobrar_Click(sender As Object, e As EventArgs) Handles ButtonSepCobrar.Click
        '
        ' COBRAR Cuenta Separada
        ' (1) Aparcar la cuenta separada a una NUEVA mesa en Sala 999
        ' (2) Proceso de COBRO
        '
        If Not AparcaSep Then
            HazAparcarSep()
        End If
        ButtonSepAparcar.Enabled = False
        ButtonSepCobrar.Enabled = False
        '
        If GRID1SepaMesa1.Rows.Count > 0 Then
            OpenFrom = 413
            MyFrm4.ShowDialog(Me)
        End If
        '
    End Sub

    Private Sub ButtonGRIDArribaIzq_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArribaIzq.Click
        '
        ' Subir una linea en el GRID
        '
        With GRID1SepaMesa
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

    Private Sub ButtonGRIDAbajoIzq_Click(sender As Object, e As EventArgs) Handles ButtonGRIDAbajoIzq.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRID1SepaMesa
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

    Private Sub ButtonGRIDArribaDer_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArribaDer.Click
        '
        ' Subir una linea en el GRID
        '
        With GRID1SepaMesa1
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

    Private Sub ButtonGRIDAbajoDer_Click(sender As Object, e As EventArgs) Handles ButtonGRIDAbajoDer.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRID1SepaMesa1
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
End Class