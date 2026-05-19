Imports System.Data.SqlClient

Module TCONA4CuentasSep
    '
    ' Módulo para gestionar Cuentas (Mesas) Separadas
    ' Procedimientos y Funciones Adaptados al COBRO desde mesas separadas...
    '
    Public Sub ActualizaDatosMESAC_Sep(ActualizaMESA As String, ActMesaOPC As Integer)
        '
        ' Este procedimiento se usa para ACTUALIZAR datos en MESAC 
        ' ( Cabecera para las MESAS Separadas, SALA=999 )
        ' Flexibiliza el procedimiento ActMesaOPC, que determinará 
        '    que datos queremos actualizar:
        ' ----------
        ' ActMesaOPC
        ' ----------
        ' 0 = Actualiza datos al realizar un COBRO.
        ' 2 = Actualiza TKFACIMPRESO = 1 (TICKET IMPRESO ).
        '
        ' MESAC :: Cabecera
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '
        Dim queryString As String = ""
        If LeeMesa_SALA1("999", ActualizaMESA, 1) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                '
                ' Reutilizable, pasando datos correctos.
                '
                ActualizaMesa_SALA1(wCaja, "999", MyFrm13.TextBoxSepNumMesa1.Text.Trim, 1)
            End If
        End If
        '
        ' Recogemos los DATOS para CEBECERA MESAS
        '
        ' Validación: 
        '   Vendedor (Camarero)
        '
        If MyFrm13.TextBoxSepCamarero.Text.Length = 0 Or Not IsNumeric(MyFrm13.TextBoxSepCamarero.Text) Then
            MyFrm13.TextBoxSepCamarero.Text = "1"
        End If
        '  
        '   IMPORTE COMANDA
        '
        If MyFrm13.LabelTotComandaSep1.Text.Length = 0 Or Not IsNumeric(MyFrm13.LabelTotComandaSep1.Text) Then
            MyFrm13.LabelTotComandaSep1.Text = "0"
        End If
        '
        ' ExisteRegistroMESAC -> Reutilizable
        '
        If ExisteRegistroMESAC(ActualizaMESA, 999) = True Then
            '
            ' UPDATE MESAC ...
            '
            queryString = queryString & "UPDATE [MESAC] SET "
            '
            Select Case ActMesaOPC
                '
                ' Cobrar :: 
                '    EFECTIVO  <- CobroEfectivo
                '    ENTREGA   <- CobroEntrega
                '    TARJETA   <- CobroTarjetas
                '    CHEQUES   <- CobroCheques
                '    OTROS     <- CobroOtros
                '    IMPORTE   <- CobroTOTALMesa
                '
                Case 0
                    With wrMESAC
                        .Mesac_IMPORTE = CobroTOTALMesa.ToString.Replace(",", ".")
                        .Mesac_ENTREGA = CobroEntrega.ToString.Replace(",", ".")
                        .Mesac_TARJETA = CobroTarjetas.ToString.Replace(",", ".")
                        .Mesac_CHEQUES = CobroCheques.ToString.Replace(",", ".")
                        .Mesac_OTROS = CobroOtros.ToString.Replace(",", ".")
                        .Mesac_EFECTIVO = CobroEfectivo.ToString.Replace(",", ".")
                        '
                        queryString = queryString & "[MESAC].[IMPORTE]='" & .Mesac_IMPORTE & "', "
                        queryString = queryString & "[MESAC].[ENTREGA]='" & .Mesac_ENTREGA & "', "
                        queryString = queryString & "[MESAC].[TARJETA]='" & .Mesac_TARJETA & "', "
                        queryString = queryString & "[MESAC].[CHEQUES]='" & .Mesac_CHEQUES & "', "
                        queryString = queryString & "[MESAC].[OTROS]='" & .Mesac_OTROS & "', "
                        queryString = queryString & "[MESAC].[EFECTIVO]='" & .Mesac_EFECTIVO & "' "
                    End With
                Case 2
                    queryString = queryString & "[MESAC].[TKFACIMPRESO]=1 "
            End Select
            '
            queryString = queryString & "WHERE "
            queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & "[MESAC].[FECHA]='" & FechaMESAC & "' AND "
            queryString = queryString & "[MESAC].[SALA]='999' AND "
            queryString = queryString & "[MESAC].[MESA]='" & ActualizaMESA & "' AND "
            queryString = queryString & "[MESAC].[FACTURA]=" & CInt(MyFrm13.TextBoxSepFactura1.Text.Trim)
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
                                "Comprobar Tabla ACTUALIZANDO Datos [MESAC]-[Cta. Sepa.]")
            End Try
            conexion.Close()
            cmd.Dispose()
            conexion.Dispose()
            '
        End If
        '
    End Sub

    Public Sub BorraMesaSepa(wBorraMesaSep As String, wFactuSep As Integer)
        '
        ' Borrado de MESAS SEPARADAS que ya no serán útiles
        ' Borra [SALA1], registro físico de la MESA.
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Eliminamos La MESA Separada
        '
        queryString = "DELETE FROM [SALA1] WHERE "
        queryString &= "[SALA1].[CAJA]=" & wCaja & " AND"
        queryString &= "[SALA1].[CODIGO]='999' " & " AND"
        queryString &= "[SALA1].[MESA]='" & wBorraMesaSep & "' AND "
        queryString &= "[SALA1].[FACTURA]=" & wFactuSep & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " [SALA1] Borrando MESA SEPARADA " & wBorraMesaSep)
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    '
End Module
