'
'   TPVS :: TCONA402 (CONA5)
'
Imports System.Data.SqlClient

Public Class TCONA402
    '
    ' Usado para mover el dezplazamiento en la Coordenada X (.left)
    '    para Botones Racion, 1/2 Racion y Cancelar
    '    cuando corresponda ...
    '
    Dim DesplaX As Integer = 907
    Dim CountDown As Integer = 4 ' Nota Datos Mesa...
    Private Sub TCONA402_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            '
            ' Teclas pulsadas...
            '
            Case Keys.Escape
                '
                ' Informa si antes se ha de aparcar
                '
                Select Case SwAparca
                    Case False
                        AbandonaAplicacion()
                    Case True
                        If RacionOnOff = False And
                           VariosCeroOnOff = False And
                           wTarifaBarra = 0 Then
                            '
                            Aparcar(0)
                        End If
                End Select
        End Select
        e = Nothing
        '
    End Sub

    Private Sub PreparaGRIDS()
        With GRID1
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = True
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .RowHeadersWidth = 21
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '
            ' Colores
            '
            .BackgroundColor = SystemColors.Info
            '
            '.DefaultCellStyle.BackColor = SystemColors.GradientActiveCaption
            '.RowsDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption
            '.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption
            '
            '.DefaultCellStyle.BackColor = Color.Khaki
            .DefaultCellStyle.BackColor = Color.Wheat
            .RowsDefaultCellStyle.BackColor = .DefaultCellStyle.BackColor
            .AlternatingRowsDefaultCellStyle.BackColor = .DefaultCellStyle.BackColor
        End With
        '
        With GRIDLISTAVend
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

    Private Sub TCONA402_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Carga del Formulario
        '
        PreparaGRIDS()
        '
        With PanelLisVen
            .Left = 6
            .Top = 409
        End With
        '
        With ButtonCancelar
            .Left = DesplaX
            .Visible = False
        End With
        With ButtonRacion
            '.Top = 543
            .Left = DesplaX
            .Visible = False
        End With
        With ButtonMediaRacion
            '.Top = 650
            .Left = DesplaX
            .Visible = False
        End With
        With wrRACIONES
            .RACIONES_indicador = 0
            .RACIONES_PVPRacion = ""
            .RACIONES_PVPMediaRacion = ""
            .RACIONES_IMPORTE = ""
        End With
        '
        ' Botón Contador Nro. Plato
        '
        ContadorPlato = 1
        Button1Plato.Text = ContadorPlato.ToString & "er Plato"
        '
        WcolReservar = ButtonART1.BackColor
        Timer1.Enabled = False
        Timer1.Interval = 700
        '
        LimpiaCajasTexto()
        '
        ' Si es FORM PRINCIPAL, Cargamos Ciertos Datos.
        ' Nro. Factura, Vendedor, etc...
        '
        If FormularioInicial = 1 Then
            With MyFrm2
                '
                ' Correcion a posicion original PRECIO / UNIDADES
                '
                Label1.Left = 561
                Label2.Left = 648
                TextBoxPrecio.Left = 646
                TextBoxUnidades.Left = 561
                '
                ' Necesario leer para Num. Factura!!!
                ' La Variable Num. Factura Se gestiona Aparte.
                '
                If LeeTCONA4Cfg("Factura") = True Then
                    .TextBoxFactura.Text = wFacturaN.ToString
                Else
                    wFacturaN = 1
                    .TextBoxFactura.Text = wFacturaN.ToString
                End If
                .TextBoxCamarero.Text = "1"
                .TextBoxNumMesa.Text = "999"
                .ButtonCAMARERO.Text = "VENDEDOR"
                .ButtonOCHO.Text = " --- "
            End With
            PedirVendedor()
        End If
        '
        ' Botonera Familias / Articulos / Favoritos :: Persistente / Refrescable.
        '
        With ButtonBEBIDAS
            .BackColor = WcolFF
            .ForeColor = WcolLF
            .Text = "BEBIDAS" & vbCrLf & "Favoritas"
        End With
        With ButtonCOMIDAS
            .BackColor = WcolFF
            .ForeColor = WcolLF
            .Text = "COMIDAS" & vbCrLf & "Favoritas"
        End With
        With ButtonOpcional
            .BackColor = WcolFF
            .ForeColor = WcolLF
            '.Text = "COMIDAS" & vbCrLf & "Favoritas"
        End With
        '
        ' Al cargar el Formulario las Botoneras se refrescan siempre.
        ' Si hay Favoritos, Prevalece su carga.
        ' Si NO, cargamos la Primera Familia Por Defecto.
        '
        If MiraFavoritos() = False Then
            IniciaBotonesFamilias()
            IniciaBotonesArticulos()
            FamAdelante = Me.ButtonFAM28.Text
            ButtonFamAtras.Enabled = False
            ButtonFamAdelante.Enabled = False
            ButtonArtAtras.Enabled = False
            ButtonArtAdelante.Enabled = False
            CargaFamilias()
            If HayFamilias Then
                CargaArticulos(wCodFamilia)
            End If
        Else
            '
            ' Aun cuando hay favoritos
            ' la botonera de familias se refresca siempre
            ' (las familias han de estar disponibles...)
            '
            IniciaBotonesFamilias()
            FamAdelante = Me.ButtonFAM28.Text
            ButtonFamAtras.Enabled = False
            ButtonFamAdelante.Enabled = False
            CargaFamilias()
        End If
    End Sub

    Public Sub LimpiaCajasTexto()
        '
        Me.GRID1.Rows.Clear()
        LabelTotComanda.Text = ""
        TextBoxUnidades.Text = ""
        TextBoxPrecio.Text = ""
        TextBoxCodBarras.Text = ""
        VisorTeclado.Text = ""
        '
    End Sub

    Private Sub IniciaBotonesFamilias()
        '
        '   Manejamos la coleccion de Controles "Botones Familias"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 9) = "ButtonFAM" Then
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

    Private Sub IniciaBotonesArticulos()
        '
        '   Manejamos la coleccion de Controles "Botones Articulos"...
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 9) = "ButtonART" Then
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

    Private Sub CargaFamilias()
        '
        ' Carga de FAMILIAS a Botones FAMILIAS
        ' Solo las TPV=True
        ' Orden de Carga según REF. Generales
        '    False = POR DESCRIPCION
        '    True =  POR CÓDIGO
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnFAM As Integer = 28 : Dim IndBTnFAM As Integer = 0
        HayFamilias = False
        '
        ' Orden de Carga Determinado por REF. Generales.
        ' Por Defecto ORDEN ALFABETICO
        '
        Dim queryString As String = ""
        queryString = queryString & "SELECT * FROM [FAM] ORDER BY "
        queryString = queryString & "[FAM].[NOMBRE]"
        '
        If LeeTCONA4Cfg("General") = True Then
            If wrLeeTCONA4.Tcona4_ORDENFAM = "True" Then
                queryString = ""
                queryString = queryString & "SELECT * FROM [FAM] ORDER BY "
                queryString = queryString & "CAST([FAM].[CODIGO] AS INTEGER) ASC"
            End If
        End If
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FAM")
            '
            If dt.Tables("FAM").Rows.Count > 0 Then
                HayFamilias = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FAM").Rows
                    '
                    ' TPV, Determina si se debe cargar o no la familia.
                    '
                    If pRow("TPV").ToString() = "True" Then
                        IndBTnFAM += 1
                        If IndBTnFAM = 1 Then
                            wCodFamilia = pRow("CODIGO").ToString()
                        End If
                        If IndBTnFAM > NumBTnFAM Then
                            ButtonFamAdelante.Enabled = True
                            Exit For
                        End If
                        EstableceBotonFAM(IndBTnFAM, pRow("CODIGO").ToString(), pRow("NOMBRE").ToString())
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
    Private Sub EstableceBotonFAM(Indicefam As Integer, CodFam As String, NomFam As String)
        '
        '   Se Activan aqui los botones para las FAMILIAS.
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 9) = "ButtonFAM" AndAlso Mid$(NombreBoton, 10, 2) = Indicefam.ToString Then
                    With wControl
                        .Text = NomFam
                        .Enabled = True
                        .Tag = CodFam
                        .BackColor = WcolFF
                        .ForeColor = WcolLF
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub CargaArticulos(wCrgFAM As String)
        '
        ' ARTICULOS de la FAMILIA Seleccionada.
        ' Inicialmente los que tienen PVP1=0 no se cargan.
        '
        Dim PVPControl As Double = 0
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnART As Integer = 30 : Dim IndBTnART As Integer = 1
        HayFamilias = False
        '
        ' Orden de Carga Determinado por REF. Generales.
        ' Por Defecto ORDEN ALFABETICO
        '
        Dim queryString As String = ""
        queryString = queryString & "SELECT * FROM [MAR]"
        queryString = queryString & " WHERE [MAR].[FAMILIA]='" & wCrgFAM & "'"
        queryString = queryString & " ORDER BY [MAR].[DESCRIPCION]"
        '
        ' En referencias sabemos el ORDEN deseado y
        ' las FAMILIAS tipificadas para 1/2 Raciones
        ' cuyos ARTICULOS si pasan con PVP1=0
        '
        If LeeTCONA4Cfg("General") = True Then
            If wrLeeTCONA4.Tcona4_ORDENART = "True" Then
                queryString = ""
                queryString = queryString & "SELECT * FROM [MAR]"
                queryString = queryString & " WHERE [MAR].[FAMILIA]='" & wCrgFAM & "'"
                queryString = queryString & " ORDER BY CAST([MAR].[NARTICULO] AS INTEGER) ASC"
            End If
        End If
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MAR")
            '
            If dt.Tables("MAR").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MAR").Rows
                    '
                    ' Contro del PVP1 y Familias 1/2 Raciones
                    '
                    PVPControl = CDbl(pRow("PREPVP1").ToString().Trim)
                    If PVPControl > 0 Or compruebaFamMediaRacion(pRow("NARTICULO").ToString()) = True Then
                        MiFileExist = My.Computer.FileSystem.FileExists(pRow("IMAGEN").ToString())
                        If MiFileExist = True Then
                            EstableceBotonART(IndBTnART, pRow("NARTICULO").ToString(), pRow("DESCRIPCION").ToString(), pRow("IMAGEN").ToString())
                        Else
                            EstableceBotonART(IndBTnART, pRow("NARTICULO").ToString(), pRow("DESCRIPCION").ToString(), "")
                        End If
                        IndBTnART += 1
                    End If
                    If IndBTnART > NumBTnART Then
                        ButtonArtAdelante.Enabled = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.ToString & vbCrLf & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [MAR]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Function compruebaFamMediaRacion(wARti As String) As Boolean
        '
        ' Comprobamos si la FAMILIA del producto es igual 
        '   a alguna de las FAMILIAS de Ref. Genrales.
        '
        ' Func. Pesimista --> Optimista
        '
        compruebaFamMediaRacion = False
        '
        LeeMar(wARti)
        Dim wMIFAMART As String = wrLeeMAR.Mar_FAMILIA.Trim
        Dim wMIFAMRAC1 As String = wrLeeTCONA4.Tcona4_VARIOSFAM1.Trim
        Dim wMIFAMRAC2 As String = wrLeeTCONA4.Tcona4_VARIOSFAM2.Trim
        Dim wMIFAMRAC3 As String = wrLeeTCONA4.Tcona4_VARIOSFAM3.Trim
        '
        If wMIFAMART.Length > 0 Then
            If wMIFAMRAC1.Length > 0 And wMIFAMART = wMIFAMRAC1 Then
                compruebaFamMediaRacion = True
                Exit Function
            End If
            If wMIFAMRAC2.Length > 0 And wMIFAMART = wMIFAMRAC2 Then
                compruebaFamMediaRacion = True
                Exit Function
            End If
            If wMIFAMRAC3.Length > 0 And wMIFAMART = wMIFAMRAC3 Then
                compruebaFamMediaRacion = True
                Exit Function
            End If
        End If
        '
    End Function

    Private Sub EstableceBotonART(Indiceart As Integer, CodArt As String,
                                  NomArt As String, ImgArt As String)
        '
        '   Se Activan aqui los botones para los ARTICULOS.
        '
        For Each wControl In Me.Controls
            If TypeOf wControl Is Button Then
                NombreBoton = CType(wControl, Button).Name
                If Mid$(NombreBoton, 1, 9) = "ButtonART" AndAlso Mid$(NombreBoton, 10, 2) = Indiceart.ToString Then
                    With wControl
                        .Text = NomArt
                        .Enabled = True
                        .Tag = CodArt
                        .BackColor = WcolFA
                        .ForeColor = WcolLA
                        '
                        ' Imagen del botón
                        '
                        Try
                            If ImgArt.Trim.Length > 0 Then
                                MiFileExist = My.Computer.FileSystem.FileExists(ImgArt.Trim)
                                If MiFileExist = True Then
                                    .BackgroundImage = Image.FromFile(ImgArt)
                                    .BackgroundImageLayout = ImageLayout.Stretch
                                    CType(wControl, Button).TextAlign = ContentAlignment.BottomCenter
                                Else
                                    .BackgroundImage = Nothing
                                    CType(wControl, Button).TextAlign = ContentAlignment.MiddleCenter
                                End If
                            Else
                                .BackgroundImage = Nothing
                                CType(wControl, Button).TextAlign = ContentAlignment.MiddleCenter
                            End If
                        Catch ex As Exception
                            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Error en la imagen del botón.")
                        End Try
                    End With
                End If
            End If
        Next
        '
    End Sub

    Private Sub ButtonFAM1_Click(sender As Object, e As EventArgs) _
        Handles ButtonFAM1.Click, ButtonFAM2.Click, ButtonFAM3.Click,
                ButtonFAM4.Click, ButtonFAM5.Click, ButtonFAM6.Click,
                ButtonFAM7.Click, ButtonFAM8.Click, ButtonFAM9.Click,
                ButtonFAM10.Click, ButtonFAM11.Click, ButtonFAM12.Click,
                ButtonFAM13.Click, ButtonFAM14.Click, ButtonFAM15.Click,
                ButtonFAM16.Click, ButtonFAM17.Click, ButtonFAM18.Click,
                ButtonFAM19.Click, ButtonFAM20.Click, ButtonFAM21.Click,
                ButtonFAM22.Click, ButtonFAM23.Click, ButtonFAM24.Click,
                ButtonFAM25.Click, ButtonFAM26.Click, ButtonFAM27.Click,
                ButtonFAM28.Click
        HazClickBTNFam(CType(sender, Button))
        '
        ' Podría ser Interesante ...
        '
        'wTempFamBoton = CType(sender, Button)
        'wTempFamBoton.BackColor = Color.BlueViolet
        '
    End Sub


    Private Sub HazClickBTNFam(wMiBtnFAM As Button)
        '
        '   Manejo del evento CLICK para botones FAMILIAS
        '
        ButtonArtAdelante.Enabled = False
        ButtonArtAtras.Enabled = False
        IniciaIndecesArt()
        IniciaBotonesArticulos()
        CargaArticulos(wMiBtnFAM.Tag.ToString)
        wCodFamilia = wMiBtnFAM.Tag.ToString
        '
    End Sub

    Private Sub IniciaIndecesArt()
        '
        Dim Ii As Integer
        ArtAdelante = ""
        For Ii = 0 To 500
            ArtAtras(Ii) = ""
        Next
        '
    End Sub

    Private Sub ButtonFamAdelante_Click(sender As Object, e As EventArgs) Handles ButtonFamAdelante.Click
        '
        FamAdelante = Me.ButtonFAM28.Text
        FamAtras(wIndFAMAtras) = Me.ButtonFAM1.Text
        '
        FamCodAdelante = Me.ButtonFAM28.Tag.ToString.Trim
        FamCodAtras(wIndFAMAtras) = Me.ButtonFAM1.Tag.ToString.Trim
        '
        wIndFAMAtras += 1
        '
        IniciaBotonesFamilias()
        IniciaBotonesArticulos()
        DesplazaFamilias(">>")
        If HayFamilias Then
            CargaArticulos(wCodFamilia)
        End If
        ButtonFamAtras.Enabled = True
        '
    End Sub

    Private Sub ButtonFamAtras_Click(sender As Object, e As EventArgs) Handles ButtonFamAtras.Click
        '
        If wIndFAMAtras = 0 Then
            ButtonFamAtras.Enabled = False
        Else
            wIndFAMAtras -= 1
        End If
        '
        FamAdelante = Me.ButtonFAM28.Text
        FamCodAdelante = Me.ButtonFAM28.Tag.ToString.Trim
        '
        IniciaBotonesFamilias()
        IniciaBotonesArticulos()
        DesplazaFamilias("<<")
        '
        ' Despues de cargar las famiias, 
        '    si primera de indice(0) = Texto de primera en la botonera
        '    atrás queda desactivado
        '
        If FamAtras(0) = Me.ButtonFAM1.Text Then
            ButtonFamAtras.Enabled = False
        End If
        '
        If HayFamilias Then
            CargaArticulos(wCodFamilia)
        End If
        '
    End Sub

    Private Sub ButtonArtAdelante_Click(sender As Object, e As EventArgs) Handles ButtonArtAdelante.Click
        '
        ArtAdelante = Me.ButtonART30.Text
        ArtAtras(wIndARTAtras) = Me.ButtonART1.Text
        '
        ArtCodAdelante = Me.ButtonART30.Tag.ToString.Trim
        ArtCodAtras(wIndARTAtras) = Me.ButtonART1.Tag.ToString.Trim
        '
        wIndARTAtras += 1
        '
        IniciaBotonesArticulos()
        DesplazaArticulos(">>", wCodFamilia)
        ButtonArtAtras.Enabled = True
        '
    End Sub

    Private Sub ButtonArtAtras_Click(sender As Object, e As EventArgs) Handles ButtonArtAtras.Click
        '
        If wIndARTAtras = 0 Then
            ButtonArtAtras.Enabled = False
        Else
            wIndARTAtras -= 1
        End If
        '
        ArtAdelante = Me.ButtonART28.Text
        ArtCodAdelante = Me.ButtonART28.Tag.ToString
        '
        IniciaBotonesArticulos()
        DesplazaArticulos("<<", wCodFamilia)
        '
        ' Despues de cargar los Articulos, 
        '    si primero de indice(0) = Texto del primero en la botonera
        '    atrás queda desactivado
        '
        If ArtAtras(0) = Me.ButtonART1.Text Then
            ButtonArtAtras.Enabled = False
        End If
        '
    End Sub

    Private Sub DesplazaFamilias(Direc As String)
        '
        '   Siguientes 28 Familias, Si las hay...
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnFAM As Integer = 28 : Dim IndBTnFAM As Integer = 0
        Dim queryString As String = ""
        HayFamilias = False
        '
        ' Debemos tener en cuenta el ORDEN marcado en Ref. Generales.
        '
        Select Case Direc
            Case ">>"
                queryString = "SELECT * FROM [FAM] "
                queryString = queryString & "WHERE [FAM].[NOMBRE] > '" & FamAdelante & "' "
                queryString = queryString & "ORDER BY [FAM].[NOMBRE] "
                '
                ' Orden Por Código
                '
                If LeeTCONA4Cfg("General") = True Then
                    If wrLeeTCONA4.Tcona4_ORDENFAM = "True" Then
                        queryString = ""
                        queryString = queryString & "SELECT * FROM [FAM] "
                        queryString = queryString & "Where CAST([FAM].[CODIGO] As Integer) > " & CInt(FamCodAdelante.Trim) & " "
                        queryString = queryString & "ORDER BY CAST([FAM].[CODIGO] AS INTEGER) ASC"
                    End If
                End If
            Case "<<"
                queryString = "SELECT * FROM [FAM] "
                queryString = queryString & "WHERE [FAM].[NOMBRE] >= '" & FamAtras(wIndFAMAtras) & "' "
                queryString = queryString & "ORDER BY [FAM].[NOMBRE] "
                '
                ' Orden Por Código
                '
                If LeeTCONA4Cfg("General") = True Then
                    If wrLeeTCONA4.Tcona4_ORDENFAM = "True" Then
                        queryString = ""
                        queryString = queryString & "SELECT * FROM [FAM] "
                        queryString = queryString & "Where CAST([FAM].[CODIGO] As Integer) >= " & CInt(FamCodAtras(wIndFAMAtras).Trim) & " "
                        queryString = queryString & "ORDER BY CAST([FAM].[CODIGO] AS INTEGER) ASC"
                    End If
                End If
        End Select
        '
        ButtonFamAdelante.Enabled = False
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FAM")
            '
            If dt.Tables("FAM").Rows.Count > 0 Then
                HayFamilias = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("FAM").Rows
                    If pRow("TPV").ToString() = "True" Then
                        IndBTnFAM += 1
                        If IndBTnFAM = 1 Then
                            wCodFamilia = pRow("CODIGO").ToString()
                        End If
                        If IndBTnFAM > NumBTnFAM Then
                            ButtonFamAdelante.Enabled = True
                            Exit For
                        End If
                        EstableceBotonFAM(IndBTnFAM, pRow("CODIGO").ToString(), pRow("NOMBRE").ToString())
                    End If
                Next
            End If
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

    Private Sub DesplazaArticulos(Direc As String, wCrgFAM As String)
        '
        '   Siguientes 30 Articulos, De la Familia Seleccionada, Si los hay...
        '
        Dim PVPControl As Double = 0
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnART As Integer = 30 : Dim IndBTnART As Integer = 1
        Dim queryString As String = ""
        '
        Select Case Direc
            Case ">>"
                queryString = "SELECT * FROM [MAR] "
                queryString = queryString & "WHERE [MAR].[DESCRIPCION] > '" & ArtAdelante & "' "
                queryString = queryString & "AND [MAR].[FAMILIA]='" & wCrgFAM & "' "
                queryString = queryString & "ORDER BY [MAR].[DESCRIPCION] "
                '
                ' Orden Por Código
                '
                If LeeTCONA4Cfg("General") = True Then
                    If wrLeeTCONA4.Tcona4_ORDENART = "True" Then
                        queryString = ""
                        queryString = queryString & "SELECT * FROM [MAR] WHERE "
                        queryString = queryString & "[MAR].[FAMILIA]='" & wCrgFAM & "' AND "
                        queryString = queryString & "CAST([MAR].[NARTICULO] AS INTEGER) > " & CInt(ArtCodAdelante.Trim) & " "
                        queryString = queryString & "ORDER BY CAST([MAR].[NARTICULO] AS INTEGER) ASC"
                    End If
                End If
            Case "<<"
                queryString = "SELECT * FROM [MAR] "
                queryString = queryString & "WHERE [MAR].[DESCRIPCION] >= '" & ArtAtras(wIndARTAtras) & "' "
                queryString = queryString & "AND [MAR].[FAMILIA]='" & wCrgFAM & "' "
                queryString = queryString & "ORDER BY [MAR].[DESCRIPCION] "
                '
                ' Orden Por Código
                '
                If LeeTCONA4Cfg("General") = True Then
                    If wrLeeTCONA4.Tcona4_ORDENART = "True" Then
                        queryString = ""
                        queryString = queryString & "SELECT * FROM [MAR] WHERE "
                        queryString = queryString & "[MAR].[FAMILIA]='" & wCrgFAM & "' AND "
                        queryString = queryString & "CAST([MAR].[NARTICULO] AS INTEGER) >= " & CInt(ArtCodAtras(wIndARTAtras).Trim) & " "
                        queryString = queryString & "ORDER BY CAST([MAR].[NARTICULO] AS INTEGER) ASC"
                    End If
                End If
        End Select
        ButtonArtAdelante.Enabled = False
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MAR")
            '
            If dt.Tables("MAR").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MAR").Rows
                    '
                    ' Contro del PVP1 y Familias 1/2 Raciones
                    '
                    PVPControl = CDbl(pRow("PREPVP1").ToString().Trim)
                    If PVPControl > 0 Or
                    compruebaFamMediaRacion(pRow("NARTICULO").ToString()) = True Then
                        EstableceBotonART(IndBTnART, pRow("NARTICULO").ToString(), pRow("DESCRIPCION").ToString(), pRow("IMAGEN").ToString())
                        IndBTnART += 1
                    End If
                    If IndBTnART > NumBTnART Then
                        ButtonArtAdelante.Enabled = True
                        Exit For
                    End If
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

    Private Sub Aparcar(AparcaOpcion As Integer)
        '
        ' Accion APARCAR una MESA.
        ' Si hay líneas en la comanda, algo habrá que actualizar.
        '
        ' AparcaOpcion = 0 :: Botón Aparcar [NORMALMENTE]
        ' AparcaOpcion = 1 :: Botón COBRAR, Aparca y Cobra...
        '
        If GRID1.Rows.Count > 0 Then
            '
            ' En este PUNTO interesa Actualizar en Nro. de Personas (PAX) en la mesa.
            ' [SALA1].[PAX]
            '
            If TextBoxPax.Text.Trim.Length = 0 Then
                TextBoxPax.Text = "0"
            End If
            ActualizaMesa_SALA1(wCaja, TextBoxNumSala.Text.Trim, TextBoxNumMesa.Text.Trim, 4)
            '
            ' La llamada a estos procedimientos Actualizará
            '    controlando en los mismos que SOLO
            '    actualiza las líneas NUEVAS, ignorando las YA existentes...
            '
            GrabaDatosMesa("MESAC", TextBoxNumMesa.Text.Trim)
            GrabaDatosMesa("MESA", TextBoxNumMesa.Text.Trim)
            '
            ' Si hay un Pedido Asignado a la Mesa, lo Actualizo aqui.
            '
            If LblDatosPedidoDomi.Text.Trim.Length > 0 Then
                Dim words As String() = Nothing
                words = LblDatosPedidoDomi.Text.Trim.Split(New Char() {"-"c})
                If words(0).Length > 0 Then
                    WMesacTlfPed = words(0)
                    ActualizaDatosMESAC(TextBoxNumMesa.Text.Trim, 4)
                End If
            End If
        End If
        '
        ' Impresion TICKETS a Areas determinadas.
        '
        GeneraTICKETSaAreas(0)
        '
        If AparcaOpcion = 0 Then
            Me.Hide()
            MyFrm1.Show()
        End If
        '
    End Sub

    Private Sub ButtonART1_Click(sender As Object, e As EventArgs) _
        Handles ButtonART1.Click, ButtonART9.Click, ButtonART8.Click,
        ButtonART7.Click, ButtonART6.Click, ButtonART5.Click, ButtonART4.Click,
        ButtonART30.Click, ButtonART3.Click, ButtonART29.Click, ButtonART28.Click,
        ButtonART27.Click, ButtonART26.Click, ButtonART25.Click,
        ButtonART24.Click, ButtonART23.Click, ButtonART22.Click,
        ButtonART21.Click, ButtonART20.Click, ButtonART2.Click,
        ButtonART19.Click, ButtonART18.Click, ButtonART17.Click,
        ButtonART16.Click, ButtonART15.Click, ButtonART14.Click,
        ButtonART13.Click, ButtonART12.Click, ButtonART11.Click,
        ButtonART10.Click
        '
        ' Evento Click Botones ARTICULOS
        '
        wMiBtnARTCombi = CType(sender, Button) : wTempArtBoton = CType(sender, Button)
        HazClickBTNArt(CType(sender, Button))
        '
    End Sub

    Private Sub HazClickBTNArt(wMiBtnART As Button)
        '
        '   Llamado desde evento CLICK para botones ARTICULOS.
        '   Lectura del Artículo, para obtener datos necesarios.
        '   Genera línea nueva línea, excepto COMBINADOS, RACIONES, 
        '      que serán gestionados en otros procedimientos.
        '
        Try
            If LeeMar(wMiBtnART.Tag.ToString.Trim) = True Then
                '
                ' Unidades
                '
                If TextBoxUnidades.Text.ToString.Length = 0 Then
                    TextBoxUnidades.Text = "0"
                End If
                If TextBoxUnidades.Text = "0" Then
                    TextBoxUnidades.Text = "1"
                End If
                '
                ' "*U" ---> Ej: "*5" = 5 Unidades
                '
                If TextBoxCodBarras.Text.ToString.Length > 0 Then
                    Dim wPos As Integer = 0 : Dim wLen As Integer = 0
                    wPos = InStr(TextBoxCodBarras.Text.ToString.Trim, "*")
                    If wPos > 0 Then
                        '
                        ' Se espera que "*" sea el último caracter de la cadena
                        '
                        If Microsoft.VisualBasic.Right(TextBoxCodBarras.Text.ToString.Trim, 1) = "*" Then
                            wLen = TextBoxCodBarras.Text.ToString.Length - 1
                            TextBoxUnidades.Text = Microsoft.VisualBasic.Left(TextBoxCodBarras.Text.ToString.Trim, wLen)
                        Else
                            msg = "Se espera formato Unidades*,   Ej.:  5*,   10* "
                            style = MsgBoxStyle.Information Or
                                MsgBoxStyle.OkOnly
                            title = "Error en formato de Multiplicador de Unidades"
                            MsgBox(msg, style, title)
                            TextBoxCodBarras.Text = ""
                            TextBoxCodBarras.Focus()
                            Exit Sub
                        End If
                    End If
                End If
                '-------------------------------------------------------------------------------------
                '  * * *    T E M A    R A C I O N E S    * * *
                '-------------------------------------------------------------------------------------
                ' Desde Este Punto Gestionamos TEMA RACIONES.
                ' Se entiende cuando MAR.PREPVP4 > 0
                '
                If Not String.IsNullOrEmpty(wrLeeMAR.Mar_PREPVP4.Trim) Then
                    If CDbl(wrLeeMAR.Mar_PREPVP4.Trim) > 0 Then
                        Raciones2Arena()
                        Exit Sub
                    End If
                End If
                '-------------------------------------------------------------------------------------
                '-------------------------------------------------------------------------------------
                wUnidN = CDbl(TextBoxUnidades.Text.ToString)
                TextBoxCodBarras.Text = ""
                '
                ' Precio: Manual o MAR.PREPVP1 A 9
                '
                If TextBoxPrecio.Text.ToString.Length = 0 Then
                    TextBoxPrecio.Text = "0"
                End If
                '
                ' Comprobar si Precio producto = 0, pedirlo
                '
                If TextBoxPrecio.Text.ToString = "0" Then
                    If CDbl(wrLeeMAR.Mar_PREPVPTPV.Trim) > 0 Then
                        'TextBoxPrecio.Text = wrLeeMAR.Mar_PREPVP1.Trim
                        TextBoxPrecio.Text = wrLeeMAR.Mar_PREPVPTPV.Trim
                    Else
                        '
                        ' PEDIR PRECIO CUANDO ES VARIOS Y PRECIO=0
                        ' Dado que los productos con PRECIO=0 SE filtran para que NO carguen
                        '  por logica SOLO los que pertenecen a Familia VARIOS
                        '  aparecen aquí.
                        ' Dicho CONTROL se establece al cargar los botones Artículos.
                        '
                        Escenario_PVPCERO(1)
                        '
                        If TextBoxUnidades.Text = "0" Or TextBoxUnidades.Text.ToString.Length = 0 Then
                            TextBoxUnidades.Text = "1"
                        End If
                        '
                        LabelInfoUSR.Text = "Por favor, introduzca PRECIO."
                        LabelInfoUSR.Visible = True
                        TextBoxPrecio.BackColor = Color.Red
                        '
                        ' 'Blink' Boton en rojo.
                        ' Por ahora desactivado !!!
                        '
                        'Timer1.Enabled = True
                        'wTempBoton.BackColor = Color.Red
                        '
                        TextBoxPrecio.Focus()
                        Exit Sub
                    End If
                End If
                '-------------------------------------------------------------------------------------
                '  * * *    C O M B I N A D O S    * * *
                '-------------------------------------------------------------------------------------
                ' Desde Este Punto Gestionamos TEMA COMBINADOS.
                ' En este caso, Primero Gestionamos los Posibles COMBINADOS
                '    sobre el PRODUCTO desde el FORM de COMBINADOS. 
                ' Luego desde dicho FORM ya pasará al GRID.
                '
                '  Cuando es COMBINADO, se gustiona en otro(s) procedimiento(s).
                '  Por tanto [+]/[-] Unidades o Fichar sobre identicos COMBINADOS
                '     siempre funcionará correctamente para UNIDADES.
                ' El Exit Sub siguiente hará que es resto de este codigo 
                '   sea ignorado para COMBINADOS.
                '
                If Not String.IsNullOrEmpty(wrLeeMAR.Mar_COMBINADO.Trim) Then
                    MyFrm8.ShowDialog(Me)
                    Exit Sub
                End If
                '-------------------------------------------------------------------------------------
                '-------------------------------------------------------------------------------------
                ' Comprobar existencia del producto en la lista.
                ' De existir YA, lo que se hace es sumarle las Unidades NUEVAS,
                '    y se gestiona en el procedimiento LocalizarArtGRID1.
                ' En caso contrario se añade la linea al GRID como NUEVA Línea.
                '
                ' NOTA para LocalizarArtGRID1: 
                '  Pasamos COMBINADO = " ".
                '  Es correcto!, ya que si NO es COMBINADO siempre vale " ".
                '
                If LocalizarArtGRID1(wMiBtnART.Tag.ToString,
                             wUnidN.ToString,
                             TextBoxPrecio.Text.ToString.Trim,
                             " ", " ") = False Then
                    '
                    ' Aqui Solo ENTRAN al GRID las Nuevas Líneas.
                    ' UNID. EXISTENTES = UNID. ENTRANTES
                    '
                    wImporteN = wUnidN * CDbl(TextBoxPrecio.Text.ToString.Trim.Replace(".", ","))
                    Me.GRID1.Rows.Add(wMiBtnART.Tag.ToString,
                           wUnidN.ToString(fmtUnid),
                           wMiBtnART.Text,
                           wUnidN.ToString(fmtUnid),
                           wImporteN.ToString(fmtImporte), "N", " ", " ",
                           ContadorPlato.ToString)
                End If
                '
                wUnidN = 0 : wImporteN = 0
                ColoreaGRID1("TCONA402")
                '
                wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
                '
                TextBoxUnidades.Text = "" : TextBoxPrecio.Text = ""
                LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                            MsgBoxStyle.Exclamation Or
                            MsgBoxStyle.OkOnly,
                            " Botones: Seleccion de producto.")
        End Try
        '
        LabelInfoUSR.Text = " "
        LabelInfoUSR.Visible = False
        Timer1.Enabled = False
        VisorTeclado.Text = ""
        'wTempArtBoton.BackColor = WcolReservar
        SwFoco_402 = 2
        TextBoxCodBarras.Focus()
        '
    End Sub

    Private Sub Raciones2Arena()
        '
        ' Preparamos Escenario Para entrada de Raciones
        '
        ' (1) Preparamos el escenario para Raciones.
        '
        With wrRACIONES
            .RACIONES_indicador = 0
            .RACIONES_PVPRacion = ""
            .RACIONES_PVPMediaRacion = ""
            .RACIONES_IMPORTE = ""
        End With
        '
        EscenarioRaciones(1)
        TextBoxPrecio.Focus()
        '
    End Sub

    Private Sub Escenario_PVPCERO(wOnOff As Integer)
        '
        ' Preparamos el Escenario Entrada de Precio.
        '
        ' wOnOff ::
        ' 0 = Desactiva esta Opción.
        ' 1 = Pedir PRECIO, cuando PVP del producto es=0, (Ej.: Familia Varios...)
        '     VariosCeroOnOff=True
        ' 2 = Modificar el PVP de un producto en la MESA Actual.
        '     ModiPVPOnOff=True
        '
        Select Case wOnOff
            Case 0
                VariosCeroOnOff = False
                ModiPVPOnOff = False
                '
                With LabelInfoUSR
                    .Top = 409
                    .Text = " "
                    .Visible = False
                End With
                With LabelMensaGrande
                    .Text = " "
                    .Visible = False
                End With
                '
                Label3.Visible = True
                TextBoxCodBarras.Visible = True
                '
                Label1.Left = 561
                Label2.Left = 648
                TextBoxPrecio.Left = 646
                TextBoxUnidades.Left = 561
                '
                For Each wControl In Me.Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = True
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = True
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" And NombreBoton <> "ButtonCLR" _
                            And NombreBoton <> "ButtonGuion" And NombreBoton <> "ButtonPrecio" _
                            And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" _
                            And NombreBoton <> "ButtonRacion" And NombreBoton <> "ButtonMediaRacion" Then
                            With wControl
                                'If wControl.Name IsNot wTempBoton.Name Then
                                .Visible = True
                                'Else
                                .Enabled = True
                                'End If
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = False
                TextBoxCodBarras.Focus()
            Case 1, 2
                '
                ' 1 = Pedir PRECIO, cuando PVP del producto es=0
                '     (Ej.: Familia Varios...)
                ' 2 = Modificar el PVP de un producto en la MESA Actual.
                '
                If wOnOff = 1 Then
                    VariosCeroOnOff = True
                End If
                If wOnOff = 2 Then
                    ModiPVPOnOff = True
                End If
                With LabelInfoUSR
                    .Top = 409
                    .Text = "Por Favor, introduzca PRECIO."
                    .Visible = True
                End With
                With LabelMensaGrande
                    TEMP = ""
                    If wOnOff = 1 Then
                        TEMP += wTempArtBoton.Text
                    End If
                    If wOnOff = 2 Then
                        '
                        ModUnids = CDbl(GRID1.SelectedCells(1).Value.ToString.Replace(".", ","))
                        ModImporte = CDbl(GRID1.SelectedCells(4).Value.ToString.Replace(".", ","))
                        Dim miPvpInfo As Double = ModImporte / ModUnids
                        '
                        wrLeeMAR.Mar_DESCRIPCION = "No Lee Articulo!"
                        If GRID1.SelectedRows.Count > 0 Then
                            LeeMar(GRID1.SelectedCells(0).Value.ToString)
                        End If
                        TEMP += wrLeeMAR.Mar_DESCRIPCION & vbCrLf
                        '
                        ' Precio Actual!
                        '
                        TEMP += "PVP Actual.: " & miPvpInfo.ToString(fmtPrecio).Trim.Replace(",", ".") & vbCrLf
                    End If
                    '
                    TEMP += vbCrLf & vbCrLf
                    TEMP += "Por favor"
                    TEMP += vbCrLf
                    TEMP += "Digite NUEVO Precio"
                    .Visible = True
                    .Text = TEMP
                End With
                '
                Label3.Visible = False
                TextBoxCodBarras.Visible = False
                '
                Label1.Left = Label1.Left + 50
                Label2.Left = Label2.Left + 50
                TextBoxPrecio.Left = TextBoxPrecio.Left + 50
                TextBoxUnidades.Left = TextBoxUnidades.Left + 50
                Label1.Left += 50
                Label2.Left += 50
                TextBoxPrecio.Left += 50
                TextBoxUnidades.Left += 50
                '
                For Each wControl In Me.Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = False
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = False
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" And NombreBoton <> "ButtonCLR" _
                            And NombreBoton <> "ButtonGuion" And NombreBoton <> "ButtonRacion" _
                            And NombreBoton <> "ButtonMediaRacion" And NombreBoton <> "ButtonPrecio" _
                            And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" Then
                            With wControl
                                .Visible = False
                                .Enabled = False
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = True
                TextBoxPrecio.BackColor = Color.Red
                TextBoxPrecio.Focus()
        End Select
        '
    End Sub

    Private Sub EscenarioRaciones(wOnOff As Integer)
        '
        ' Preparamos el Escenario Para Pedir Raciones / 1/2 Raciones, Precio Manual
        '    RacionOnOff
        '
        Select Case wOnOff
            Case 0
                RacionOnOff = False
                With LabelInfoUSR
                    .Top = 409
                    .Text = " "
                    .Visible = False
                End With
                With LabelMensaGrande
                    .Text = " "
                    .Visible = False
                End With
                '
                Label3.Visible = True
                TextBoxCodBarras.Visible = True
                '
                Label1.Left = 561
                Label2.Left = 648
                TextBoxPrecio.Left = 646
                TextBoxUnidades.Left = 561
                '
                For Each wControl In Me.Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = True
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = True
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" _
                    And NombreBoton <> "ButtonCLR" _
                    And NombreBoton <> "ButtonGuion" _
                    And NombreBoton <> "ButtonPrecio" _
                    And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" Then
                            With wControl
                                'If wControl.Name IsNot wTempBoton.Name Then
                                .Visible = True
                                'Else
                                .Enabled = True
                                'End If
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = False
                ButtonRacion.Visible = False
                ButtonRacion.Text = "Ración"
                ButtonMediaRacion.Visible = False
                ButtonMediaRacion.Text = "1/2 Ración"
                TextBoxCodBarras.Focus()
            Case 1
                RacionOnOff = True
                With LabelInfoUSR
                    .Top = 409
                    .Text = "Digite Precio o Elija Racion Deseada."
                    .Visible = True
                End With
                With LabelMensaGrande
                    TEMP = ""
                    TEMP += wTempArtBoton.Text
                    TEMP += vbCrLf & vbCrLf
                    TEMP += "Por favor"
                    TEMP += vbCrLf
                    TEMP += "Digite Precio o"
                    TEMP += vbCrLf
                    TEMP += " Elija Racion Deseada."
                    .Text = TEMP
                    .Visible = True
                End With
                '
                Label3.Visible = False
                TextBoxCodBarras.Visible = False
                '
                Label1.Left = Label1.Left + 50
                Label2.Left = Label2.Left + 50
                TextBoxPrecio.Left = TextBoxPrecio.Left + 50
                TextBoxUnidades.Left = TextBoxUnidades.Left + 50
                '
                For Each wControl In Me.Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = False
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = False
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" And NombreBoton <> "ButtonCLR" _
                            And NombreBoton <> "ButtonGuion" And NombreBoton <> "ButtonRacion" _
                            And NombreBoton <> "ButtonMediaRacion" And NombreBoton <> "ButtonPrecio" _
                            And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" Then
                            With wControl
                                'If wControl.Name IsNot wTempBoton.Name Then
                                .Visible = False
                                'Else
                                .Enabled = False
                                'End If
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = True
                ButtonRacion.Visible = True
                'ButtonRacion.Text = "Ración" & vbCrLf & CDbl(wrLeeMAR.Mar_PREPVP1).ToString(fmtPrecio).Replace(",", ".")
                ButtonRacion.Text = "Ración" & vbCrLf & CDbl(wrLeeMAR.Mar_PREPVPTPV).ToString(fmtPrecio).Replace(",", ".")
                ButtonMediaRacion.Visible = True
                ButtonMediaRacion.Text = "1/2 Ración" & vbCrLf & CDbl(wrLeeMAR.Mar_PREPVP4).ToString(fmtPrecio).Replace(",", ".")
                TextBoxPrecio.BackColor = Color.Red
                TextBoxPrecio.Focus()
        End Select
        '
    End Sub

    Private Sub TerminarRacion()
        '
        ' Este procedimiento termina el proceso RACIONES
        '
        EscenarioRaciones(0)
        '
        ' Unidades
        '
        If TextBoxUnidades.Text.ToString.Length = 0 Then
            TextBoxUnidades.Text = "0"
        End If
        If TextBoxUnidades.Text = "0" Then
            TextBoxUnidades.Text = "1"
        End If
        '
        ' "*U" ---> Ej: "*5" = 5 Unidades
        '
        If TextBoxCodBarras.Text.ToString.Length > 0 Then
            Dim wPos As Integer = 0 : Dim wLen As Integer = 0
            wPos = InStr(TextBoxCodBarras.Text.ToString.Trim, "*")
            If wPos > 0 Then
                '
                ' Se espera que "*" sea el último caracter de la cadena
                '
                If Microsoft.VisualBasic.Right(TextBoxCodBarras.Text.ToString.Trim, 1) = "*" Then
                    wLen = TextBoxCodBarras.Text.ToString.Length - 1
                    TextBoxUnidades.Text = Microsoft.VisualBasic.Left(TextBoxCodBarras.Text.ToString.Trim, wLen)
                Else
                    msg = "Se espera formato Unidades*,   Ej.:  5*,   10* "
                    style = MsgBoxStyle.Information Or
                                MsgBoxStyle.OkOnly
                    title = "Error en formato de Multiplicador de Unidades"
                    MsgBox(msg, style, title)
                    TextBoxCodBarras.Text = ""
                    TextBoxCodBarras.Focus()
                    Exit Sub
                End If
            End If
        End If
        wUnidN = CDbl(TextBoxUnidades.Text.ToString)
        TextBoxCodBarras.Text = ""
        '
        '-------------------------------------------------------------------------------------
        '  * * *    C O M B I N A D O S    * * *
        '-------------------------------------------------------------------------------------
        ' Desde Este Punto Gestionamos TEMA COMBINADOS.
        ' En este caso, Primero Gestionamos los Posibles COMBINADOS
        '    sobre el PRODUCTO desde el FORM de COMBINADOS. 
        ' Luego desde dicho FORM ya pasará al GRID.
        '
        '  Cuando es COMBINADO, se gustiona en otro(s) procedimiento(s).
        '  Por tanto [+]/[-] Unidades o Fichar sobre identicos COMBINADOS
        '     siempre funcionará correctamente para UNIDADES.
        ' El Exit Sub siguiente hará que es resto de este codigo 
        '   sea ignorado para COMBINADOS.
        '
        If Not String.IsNullOrEmpty(wrLeeMAR.Mar_COMBINADO.Trim) Then
            MyFrm8.ShowDialog(Me)
            Exit Sub
        End If
        '-------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------
        ' Comprobar existencia del producto en la lista.
        ' De existir YA, lo que se hace es sumarle las Unidades NUEVAS,
        '    y se gestiona en el procedimiento LocalizarArtGRID1.
        ' En caso contrario se añade la linea al GRID como NUEVA Línea.
        '
        ' NOTA para LocalizarArtGRID1: 
        '  Pasamos COMBINADO = " ".
        '  Es correcto!, ya que si NO es COMBINADO siempre vale " ".
        '-------------------------------------------------------------------------------------
        '
        ' 1/2 Ración, Controlamos aquí cuando NO es COMBINADO.
        ' 1/2 Ración + COMBINADO. (En FORM COMBINADOS)
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
        If LocalizarArtGRID1(wTempArtBoton.Tag.ToString, wUnidN.ToString,
                                 TextBoxPrecio.Text.ToString.Trim, " ", wMediaPrecio) = False Then
            '
            ' Aqui Solo ENTRAN al GRID las Nuevas Líneas.
            ' UNID. EXISTENTES = UNID. ENTRANTES
            '
            wImporteN = wUnidN * CDbl(TextBoxPrecio.Text.ToString.Trim.Replace(".", ","))
            Me.GRID1.Rows.Add(wTempArtBoton.Tag.ToString,
                                   wUnidN.ToString(fmtUnid),
                                   wTempArtBoton.Text,
                                   wUnidN.ToString(fmtUnid),
                                   wImporteN.ToString(fmtImporte), "N", " ", wMediaPrecio,
                                   ContadorPlato.ToString)
        End If
        '
        wUnidN = 0 : wImporteN = 0
        ColoreaGRID1("TCONA402")
        '
        wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
        '
        TextBoxUnidades.Text = "" : TextBoxPrecio.Text = ""
        LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")

        '
        LabelInfoUSR.Text = " "
        LabelInfoUSR.Visible = False
        Timer1.Enabled = False
        VisorTeclado.Text = ""
        'wTempArtBoton.BackColor = WcolReservar
        SwFoco_402 = 2
        TextBoxCodBarras.Focus()
        '
    End Sub

    Private Sub MiraFoco()
        '
        ' Determinamos el Control que esta recibiendo el FOCO.
        '
        Select Case SwFoco_402
            Case 0
                Me.TextBoxUnidades.Text = VisorTeclado.Text
            Case 1
                Me.TextBoxPrecio.Text = VisorTeclado.Text
            Case 2
                Me.TextBoxCodBarras.Text = VisorTeclado.Text
            Case 4
                'If wMesaLibre Then
                If PidePAXOnOff = True Then
                    TextBoxPax.Text = VisorTeclado.Text
                End If
            Case 5
                'If wMesaLibre Then
                If PideVENDOnOff = True Then
                    TextBoxCamarero.Text = VisorTeclado.Text
                End If
        End Select
        '
    End Sub

    Private Sub ButtonAnuTODO_Click(sender As Object, e As EventArgs) Handles ButtonAnuTODO.Click
        '
        ' Anula Lo ultimo que ha sido fichado.
        ' El camino mas fácil en este caso es volver a cargar lo que hay en la MESA (E),
        '  DESCARTANDO LO ULTIMO FICHADO (N)...
        '
        CargaListaMESAs(Me.TextBoxNumMesa.Text.Trim)
        '
    End Sub

    Private Sub ButtonAnuLIN_Click(sender As Object, e As EventArgs) Handles ButtonAnuLIN.Click
        '
        ' Para poder BORRAR LINEAS de Unidades existentes, 
        '   se ha de tener PRIVILEGIOS elevados o pedir credenciales.
        '----------------------------------------------------------------------------------------------
        ' Comprobamos Claves y NIVEL del Vendedor Actual. Tabla [CLAVES]
        '
        If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
        Else
            wrLeeCODNOM.NIVELACCESO = 0
        End If
        '
        ' >5, 5 "SUPER", 4 "JEFE", 3 "ENCARGADO", permitidos.
        '
        If wrLeeCODNOM.NIVELACCESO < 3 Then
            '
            ' Llamada al Teclado Flotante, pedir Clave.
            '
            With wrTecladoFlotante
                '
                ' 0=Numérico, 1=Alfabético, 2=Alfanumérico
                '
                .Tipo = 2
                '
                ' Mensaje al usuario
                '
                .MensaUsuario = "Por favor digite una clave."
                '
                ' 0=No, 1=Pide Pwd
                '
                .PidePwd = 1
                '
                ' Mensaje al usuario
                '
                .MensaUsuario = "Por favor digite una clave."
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
                .MaxChar = 20
                '
                ' Codigo que indica el control al que retornar el texto.
                ' Se controla en la aplicación.
                ' Para este Caso le envío 998
                '
                .CodigoRetorno = 998
                '
            End With
            MyFrm15.ShowDialog(Me)
            Exit Sub
        End If
        '
        ' Podemos eliminar unidades Nuevas / Existentes 
        '   para Niveles elevados o con credenciales.
        ' Pedir Confirmacion de Borrado de Linea dependerá de Ref. Generales.
        '
        LeeTCONA4Cfg("General")
        If wrLeeTCONA4.Tcona4_BORLINCUENTA = "True" Then
            msg = "Por favor confirme BORRADO de linea.: " & vbCrLf
            msg &= GRID1.SelectedCells(0).Value.ToString & " " & GRID1.SelectedCells(2).Value.ToString & vbCrLf
            style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
            title = "Borrar línea Seleccionada."
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                AnulaLineasSEL_GRID1()
            End If
        Else
            AnulaLineasSEL_GRID1()
        End If
        '
    End Sub

    Private Sub ButtonCal7_Click(sender As Object, e As EventArgs) _
        Handles ButtonCal7.Click, ButtonPrecio.Click, ButtonGuion.Click,
        ButtonEnter.Click, ButtonCalComma.Click, ButtonCal9.Click,
        ButtonCal8.Click, ButtonCal6.Click, ButtonCal5.Click, ButtonCal4.Click,
        ButtonCal3.Click, ButtonCal2.Click, ButtonCal1.Click, ButtonCal00.Click,
        ButtonCal0.Click
        '
        HazClickBTNCalculadora(CType(sender, Button))
        '
    End Sub

    Private Sub HazClickBTNCalculadora(wMiBtnCALC As Button)
        '
        '   Manejo del evento CLICK para botones CALCULADORA
        '
        If VisorTeclado.Text.Length > 9 Then
            Exit Sub
        End If
        '
        Dim wPos As Integer = 0
        Select Case wMiBtnCALC.Text
            Case "ENTER"
                '
                ' Pulsacion Tecla Enter, Pide Camarero
                '
                If PideVENDOnOff = True Then
                    If TextBoxCamarero.Text.Trim.Length = 0 Then
                        TextBoxCamarero.Text = "1"
                    End If
                    EscenarioPideVEND(0)
                    CambiaVendedorMesa(CInt(TextBoxFactura.Text.Trim))
                    If LeeVendedor(CInt(Me.TextBoxCamarero.Text.Trim)) = True Then
                        LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE
                    Else
                        LabelNomCamarero.Text = "*NO LEIDO*"
                    End If
                    '
                    ' Boton [-] del teclado Fijo. El Camarero Debe tener 
                    '   PRIVILEGIOS 3 a 5 o >5 y BOTONMENOS debe estar ACTIVADO.
                    '
                    ButtonGuion.Enabled = False
                    If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
                        If wrLeeCODNOM.NIVELACCESO > 2 Then
                            If LeeClaves(wrLeeCODNOM.NIVELACCESO) = True Then
                                If wrCLAVES.BOTONMENOS = "True" Then
                                    ButtonGuion.Enabled = True
                                End If
                            End If
                        End If
                    End If
                    '
                    PideVENDOnOff = False
                End If
                '
                ' Pulsacion Tecla Enter, Pide PAX
                '
                If PidePAXOnOff = True Then
                    If Me.TextBoxPax.Text.Trim.Length = 0 Then
                        Me.TextBoxPax.Text = "0"
                    End If
                    EscenarioPidePAX(0)
                    PidePAXOnOff = False
                End If
                '
                ' Pulsacion Tecla Enter, Control RACIONES
                '
                If RacionOnOff = True Then
                    If Me.TextBoxUnidades.Text.Trim.Length = 0 Then
                        Me.TextBoxUnidades.Text = "1"
                    End If
                    If Me.TextBoxPrecio.Text.Trim.Length > 0 Then
                        TerminarRacion()
                    Else
                        msg = "Se espera un precio. Por favor elija.: " & vbCrLf & "RACION, 1/2 RACION o Digite un precio."
                        style = MsgBoxStyle.Information Or
                                MsgBoxStyle.OkOnly
                        title = "Error en formato de precio."
                        MsgBox(msg, style, title)
                        TextBoxPrecio.Focus()
                        Exit Sub
                    End If
                End If
                '
                ' Pulsacion Tecla Enter, Control VARIOS, PVP=0
                '
                If VariosCeroOnOff = True Then
                    If Me.TextBoxUnidades.Text.Trim.Length = 0 Then
                        Me.TextBoxUnidades.Text = "1"
                    End If
                    If Me.TextBoxPrecio.Text.Trim.Length > 0 Then
                        TerminarVariosPVP0()
                    Else
                        msg = "Por favor digite Precio."
                        style = MsgBoxStyle.Information Or
                                MsgBoxStyle.OkOnly
                        title = "Error en formato de precio."
                        MsgBox(msg, style, title)
                        TextBoxPrecio.Focus()
                        Exit Sub
                    End If
                End If
                '
                ' Pulsacion Tecla Enter, Modificar PVP
                '
                If ModiPVPOnOff = True Then
                    If Me.TextBoxUnidades.Text.Trim.Length = 0 Then
                        Me.TextBoxUnidades.Text = "1"
                    End If
                    If Me.TextBoxPrecio.Text.Trim.Length > 0 Then
                        Escenario_PVPCERO(0)
                        GrabaPVPModificado(CDbl(TextBoxPrecio.Text.Trim.Replace(".", ",")))
                        TextBoxUnidades.Text = ""
                        TextBoxPrecio.Text = ""
                    Else
                        msg = "Por favor digite Precio."
                        style = MsgBoxStyle.Information Or
                                MsgBoxStyle.OkOnly
                        title = "Error en formato de precio."
                        MsgBox(msg, style, title)
                        TextBoxPrecio.Focus()
                        Exit Sub
                    End If
                End If
                '
            Case "-"
                '
                ' Sólo un "-", y si hay una coma y es el último carácter
                '  completamos ,00-"
                '
                wPos = InStr(VisorTeclado.Text, "-")
                If wPos = 0 Then
                    If VisorTeclado.Text.Length > 0 Then
                        If Microsoft.VisualBasic.Right(VisorTeclado.Text.Trim, 1) = "," Then
                            VisorTeclado.Text += "00" + wMiBtnCALC.Text
                        Else
                            VisorTeclado.Text += wMiBtnCALC.Text
                        End If
                    End If
                End If
                '
            Case ","
                '
                ' Sólo una "," y nunca despues de un "-"
                '
                wPos = InStr(VisorTeclado.Text, ",")
                If wPos = 0 Then
                    If VisorTeclado.Text.Length > 0 Then
                        wPos = InStr(VisorTeclado.Text, "-")
                        If wPos = 0 Then
                            VisorTeclado.Text += wMiBtnCALC.Text
                        End If
                    Else
                        '
                        ' Si no hay caracteres y pulsamos ","
                        '
                        wPos = InStr(VisorTeclado.Text, "-")
                        If wPos = 0 Then
                            VisorTeclado.Text += "0" & wMiBtnCALC.Text
                        End If
                    End If
                End If
                '
            Case Else
                '
                ' Nunca digitos despues del "-"
                '
                wPos = InStr(VisorTeclado.Text, "-")
                If wPos = 0 Then
                    VisorTeclado.Text += wMiBtnCALC.Text
                End If
        End Select
        '
        If VisorTeclado.Text = PassTRIVALLE(0) Or VisorTeclado.Text = PassTRIVALLE(1) Then
            ButtonCABE.Enabled = True
        Else
            ButtonCABE.Enabled = False
        End If
        '
        MiraFoco()
        '
    End Sub

    Private Sub GrabaPVPModificado(wPrecio As Double)
        '
        ' Este Procedimiento Graba el PVP Modificado en la MESA
        '    y refleja el cambio en GRID1.
        '
        If ExisteRegistroMESA(TextBoxNumMesa.Text.Trim,
                              GRID1.SelectedCells(0).Value.ToString,
                              GRID1.SelectedCells(6).Value.ToString,
                              GRID1.SelectedCells(7).Value.ToString) = True Then
            '
            ModUnids = CDbl(GRID1.SelectedCells(1).Value.ToString.Replace(".", ","))
            ModImporte = CDbl(GRID1.SelectedCells(4).Value.ToString.Replace(".", ","))
            NewImpo = ModUnids * wPrecio
            '
            ' Depuración
            '
            TEMP = ""
            TEMP &= "Caja    .: " & wCaja.ToString & vbCrLf
            TEMP &= "Fecha   .: " & FechaMESAC & vbCrLf
            TEMP &= "Sala    .: " & wCodSala & vbCrLf
            TEMP &= "Mesa    .: " & TextBoxNumMesa.Text.Trim & vbCrLf
            TEMP &= "Factura .: " & TextBoxFactura.Text.Trim & " // " & wFacturaN.ToString & vbCrLf
            TEMP &= "Artic. ..: " & GRID1.SelectedCells(0).Value.ToString & vbCrLf
            TEMP &= "Combi  ..: " & GRID1.SelectedCells(6).Value.ToString & vbCrLf
            TEMP &= "M/Pre  ..: " & GRID1.SelectedCells(7).Value.ToString & vbCrLf
            TEMP &= "=========================" & vbCrLf & vbCrLf
            TEMP &= "Unidades ....: " & ModUnids.ToString & vbCrLf
            TEMP &= "Importe .....: " & ModImporte.ToString & vbCrLf
            TEMP &= "Nuevo Precio.: " & wPrecio.ToString & vbCrLf
            TEMP &= "Nuevo Importe: " & NewImpo.ToString & vbCrLf
            '
            ActualizaDatosMESA(TextBoxNumMesa.Text.Trim, FechaMESAC,
                               GRID1.SelectedCells(0).Value.ToString,
                               GRID1.SelectedCells(6).Value.ToString,
                               GRID1.SelectedCells(7).Value.ToString, 1)
            '
            ' Refrescamos la Lista de la Cuenta de MESA.
            '
            CargaListaMESAs(TextBoxNumMesa.Text.Trim)
            '
        Else
            msg = "Mesa no leida, imposible actualizar datos."
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error en la lectura de la MESA actual."
            MsgBox(msg, style, title)
        End If
        '
    End Sub

    Private Sub TerminarVariosPVP0()
        '
        ' Este procedimiento termina el proceso Pedir PRECIO=0
        '
        Escenario_PVPCERO(0)
        '
        ' Unidades
        '
        If TextBoxUnidades.Text.ToString.Length = 0 Then
            TextBoxUnidades.Text = "0"
        End If
        If TextBoxUnidades.Text = "0" Then
            TextBoxUnidades.Text = "1"
        End If
        '
        ' "*U" ---> Ej: "*5" = 5 Unidades
        '
        If TextBoxCodBarras.Text.ToString.Length > 0 Then
            Dim wPos As Integer = 0 : Dim wLen As Integer = 0
            wPos = InStr(TextBoxCodBarras.Text.ToString.Trim, "*")
            If wPos > 0 Then
                '
                ' Se espera que "*" sea el último caracter de la cadena
                '
                If Microsoft.VisualBasic.Right(TextBoxCodBarras.Text.ToString.Trim, 1) = "*" Then
                    wLen = TextBoxCodBarras.Text.ToString.Length - 1
                    TextBoxUnidades.Text = Microsoft.VisualBasic.Left(TextBoxCodBarras.Text.ToString.Trim, wLen)
                Else
                    msg = "Se espera formato Unidades*,   Ej.:  5*,   10* "
                    style = MsgBoxStyle.Information Or
                                MsgBoxStyle.OkOnly
                    title = "Error en formato de Multiplicador de Unidades"
                    MsgBox(msg, style, title)
                    TextBoxCodBarras.Text = ""
                    TextBoxCodBarras.Focus()
                    Exit Sub
                End If
            End If
        End If
        wUnidN = CDbl(TextBoxUnidades.Text.ToString)
        '-------------------------------------------------------------------------------------
        '  * * *    C O M B I N A D O S    * * *
        '-------------------------------------------------------------------------------------
        ' Desde Este Punto Gestionamos TEMA COMBINADOS.
        ' En este caso, Primero Gestionamos los Posibles COMBINADOS
        '    sobre el PRODUCTO desde el FORM de COMBINADOS. 
        ' Luego desde dicho FORM ya pasará al GRID.
        '
        '  Cuando es COMBINADO, se gustiona en otro(s) procedimiento(s).
        '  Por tanto [+]/[-] Unidades o Fichar sobre identicos COMBINADOS
        '     siempre funcionará correctamente para UNIDADES.
        ' El Exit Sub siguiente hará que es resto de este codigo 
        '   sea ignorado para COMBINADOS.
        '
        If Not String.IsNullOrEmpty(wrLeeMAR.Mar_COMBINADO.Trim) Then
            MyFrm8.ShowDialog(Me)
            Exit Sub
        End If
        '-------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------
        ' Comprobar existencia del producto en la lista.
        ' De existir YA, lo que se hace es sumarle las Unidades NUEVAS,
        '    y se gestiona en el procedimiento LocalizarArtGRID1.
        ' En caso contrario se añade la linea al GRID como NUEVA Línea.
        '
        ' NOTA para LocalizarArtGRID1: 
        '  Pasamos COMBINADO = " ".
        '  Es correcto!, ya que si NO es COMBINADO siempre vale " ".
        '
        If LocalizarArtGRID1(wTempArtBoton.Tag.ToString,
                             wUnidN.ToString,
                             TextBoxPrecio.Text.ToString.Trim,
                             " ", " ") = False Then
            '
            ' Aqui Solo ENTRAN al GRID las Nuevas Líneas.
            ' UNID. EXISTENTES = UNID. ENTRANTES
            '
            wImporteN = wUnidN * CDbl(TextBoxPrecio.Text.ToString.Trim.Replace(".", ","))
            Me.GRID1.Rows.Add(wTempArtBoton.Tag.ToString,
                           wUnidN.ToString(fmtUnid),
                           wTempArtBoton.Text,
                           wUnidN.ToString(fmtUnid),
                           wImporteN.ToString(fmtImporte), "N", " ", " ",
                           ContadorPlato.ToString)
        End If
        '
        wUnidN = 0 : wImporteN = 0
        ColoreaGRID1("TCONA402")
        '
        wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
        '
        TextBoxUnidades.Text = "" : TextBoxPrecio.Text = ""
        LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")
        '
        LabelInfoUSR.Text = " "
        LabelInfoUSR.Visible = False
        Timer1.Enabled = False
        VisorTeclado.Text = ""
        'wTempArtBoton.BackColor = WcolReservar
        SwFoco_402 = 2
        TextBoxCodBarras.Focus()
        '
    End Sub

    Private Sub ButtonCLR_Click(sender As Object, e As EventArgs) Handles ButtonCLR.Click
        VisorTeclado.Text = ""
        ButtonCABE.Enabled = False
        MiraFoco()
    End Sub

    Private Sub TextBoxUnidades_GotFocus(sender As Object, e As EventArgs) Handles TextBoxUnidades.GotFocus
        SwFoco_402 = 0
        TextBoxUnidades.BackColor = Color.Red
    End Sub

    Private Sub TextBoxPrecio_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPrecio.GotFocus
        SwFoco_402 = 1
        TextBoxPrecio.BackColor = Color.Red
    End Sub

    Private Sub TextBoxCodBarras_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCodBarras.GotFocus
        SwFoco_402 = 2
        TextBoxCodBarras.BackColor = Color.Red
    End Sub

    Private Function MiraFavoritos() As Boolean
        '
        ' Si esta MARCADO, prevalece la CARGA DE FAVORITOS
        ' Tambien indica si deseamos Favoritos BEBIDAS o COMIDAS
        '
        MiraFavoritos = False
        '
        If wrLeeTCONA4.Tcona4_CARGAFAVORITOS = "True" Then
            MiraFavoritos = True
            Select Case wrLeeTCONA4.Tcona4_BOTONFAVORITO
                Case "BEBIDAS"
                    '
                    ' BEBIDAS FAVORITAS
                    '
                    IniciaBotonesArticulos()
                    ButtonArtAtras.Enabled = False
                    ButtonArtAdelante.Enabled = False
                    '
                    CargaFavoritos("BEBIDAS")
                Case "COMIDAS"
                    '
                    ' COMIDAS FAVORITAS
                    '
                    IniciaBotonesArticulos()
                    ButtonArtAtras.Enabled = False
                    ButtonArtAdelante.Enabled = False
                    '
                    CargaFavoritos("COMIDAS")
            End Select
        End If
        '
    End Function

    Private Sub TCONA402_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        ' Todas las acciones que necesitan PRIVILEGIOS y pide clave
        '    se controlan desde aqui al regresar del teclado.
        ' En este sentido, debe ocurrir siempre que se dispara el Evento 
        '    Activated del FORM.
        '
        With wrTecladoFlotante
            '
            ' Para el Boton [TARIFAS], Modificar Tarifa
            '
            If .CodigoRetorno = 995 Then
                .CodigoRetorno = 0
                .MensaUsuario = ""
                '
                ' Programadores / Trivalle
                '
                If .CadenaVisor = PassTRIVALLE(0) Or .CadenaVisor = PassTRIVALLE(1) Then
                    VisorTeclado.Text = ""
                    EscenarioPideTARIFA(1)
                Else
                    '
                    ' Determina si la Clave aportada corresponde a un NIVEL
                    '  permitido >5, 5 a 3.
                    ' Modifica si pasa estos filtros.
                    '
                    If LocalizaClave(.CadenaVisor) = True Then
                        If wrCLAVES.NIVELACCESO > 2 Then
                            If wrCLAVES.BOTONTARIFA = "True" Then
                                VisorTeclado.Text = ""
                                EscenarioPideTARIFA(1)
                            End If
                        End If
                    End If
                End If
                Exit Sub
            End If
            '
            ' Para el Boton [Precio], Modificar PVP
            '
            If .CodigoRetorno = 996 Then
                .CodigoRetorno = 0
                .MensaUsuario = ""
                '
                ' Programadores / Trivalle
                '
                If .CadenaVisor = PassTRIVALLE(0) Or .CadenaVisor = PassTRIVALLE(1) Then
                    VisorTeclado.Text = ""
                    Escenario_PVPCERO(2)
                Else
                    '
                    ' Determina si la Clave aportada corresponde a un NIVEL
                    '  permitido >5, 5 a 3.
                    ' Modifica si pasa estos filtros.
                    '
                    If LocalizaClave(.CadenaVisor) = True Then
                        If wrCLAVES.NIVELACCESO > 2 Then
                            If wrCLAVES.BOTONPRECIO = "True" Then
                                VisorTeclado.Text = ""
                                Escenario_PVPCERO(2)
                            End If
                        End If
                    End If
                End If
                Exit Sub
            End If
            '
            ' Para el Boton Cambio de Camarero [Camarero]
            '
            If .CodigoRetorno = 997 Then
                .CodigoRetorno = 0
                .MensaUsuario = ""
                '
                ' Programadores / Trivalle
                '
                If .CadenaVisor = PassTRIVALLE(0) Or .CadenaVisor = PassTRIVALLE(1) Then
                    VisorTeclado.Text = ""
                    EscenarioPideVEND(1)
                Else
                    '
                    ' Determina si la Clave aportada corresponde a un NIVEL
                    '  permitido >5, 5 a 3.
                    ' Finalmente tambien se comprueba si Boton Menos 
                    '   tiene permiso asignado.
                    ' Borra si pasa estos filtros.
                    '
                    If LocalizaClave(.CadenaVisor) = True Then
                        If wrCLAVES.NIVELACCESO > 2 Then
                            VisorTeclado.Text = ""
                            EscenarioPideVEND(1)
                        End If
                    End If
                End If
                Exit Sub
            End If
            '
            ' Para el Boton [Anula Línea Seleccion]
            '
            If .CodigoRetorno = 998 Then
                .CodigoRetorno = 0
                .MensaUsuario = ""
                '
                ' Programadores / Trivalle
                '
                If .CadenaVisor = PassTRIVALLE(0) Or .CadenaVisor = PassTRIVALLE(1) Then
                    AnulaLineasSEL_GRID1()
                Else
                    '
                    ' Determina si la Clave aportada corresponde a un NIVEL
                    '  permitido >5, 5 a 3.
                    ' Finalmente tambien se comprueba si Boton Menos 
                    '   tiene permiso asignado.
                    ' Borra si pasa estos filtros.
                    '
                    If LocalizaClave(.CadenaVisor) = True Then
                        If wrCLAVES.NIVELACCESO > 2 Then
                            AnulaLineasSEL_GRID1()
                        End If
                    End If
                End If
                Exit Sub
            End If
            '
            ' Para el Boton [-]
            '
            If .CodigoRetorno = 999 Then
                .CodigoRetorno = 0
                .MensaUsuario = ""
                '
                ' Programadores / Trivalle
                '
                If .CadenaVisor = PassTRIVALLE(0) Or .CadenaVisor = PassTRIVALLE(1) Then
                    RestaUnaBotonMenos()
                Else
                    '
                    ' Determina si la Clave aportada corresponde a un NIVEL
                    '  permitido >5, 5 a 3.
                    ' Finalmente tambien se comprueba si Boton Menos 
                    '   tiene permiso asignado.
                    ' Borra si pasa estos filtros.
                    '
                    If LocalizaClave(.CadenaVisor) = True Then
                        If wrCLAVES.NIVELACCESO > 2 Then
                            If wrCLAVES.BOTONMENOS = "True" Then
                                RestaUnaBotonMenos()
                            End If
                        End If
                    End If
                End If
                Exit Sub
            End If
        End With
        '======================================================================================================================
        ' Fin Controlador de ENTRADAS desde Teclado Flotante.
        '======================================================================================================================
        '
        ' Boton [-] del teclado Fijo. El Camarero Debe tener 
        '   PRIVILEGIOS 3 a 5 o >5 y BOTONMENOS debe estar ACTIVADO.
        '
        ButtonGuion.Enabled = False
        If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
            If wrLeeCODNOM.NIVELACCESO > 2 Then
                If LeeClaves(wrLeeCODNOM.NIVELACCESO) = True Then
                    If wrCLAVES.BOTONMENOS = "True" Then
                        ButtonGuion.Enabled = True
                    End If
                End If
            End If
        End If
        '
        ' Boton Ver Combinados.
        '
        ButtonVerCombi.Enabled = False
        ButtonVerCombi.BackColor = SystemColors.ButtonFace
        If GRID1.Rows.Count > 0 Then
            If GRID1.SelectedRows.Count > 0 Then
                If GRID1.SelectedCells(6).Value.ToString.Trim.Length > 0 Then
                    ButtonVerCombi.Enabled = True
                    ButtonVerCombi.BackColor = SystemColors.Info
                End If
            End If
        End If
        '
        ' Botonera Familias / Articulos / Favoritos :: Persistente / Refrescable.
        '
        If LeeTCONA4Cfg("General") = True Then
            If wrLeeTCONA4.Tcona4_REFRESCABOTONES = "True" Or CambioColores = True Then
                With ButtonBEBIDAS
                    .BackColor = WcolFF
                    .ForeColor = WcolLF
                    .Text = "BEBIDAS" & vbCrLf & "Favoritas"
                End With
                With ButtonCOMIDAS
                    .BackColor = WcolFF
                    .ForeColor = WcolLF
                    .Text = "COMIDAS" & vbCrLf & "Favoritas"
                End With
                '
                ' En este caso Favoritos se ignora aun cuando 
                '    se marque su precargado en Ref. Generales.
                ' Mantenemos la familia seleccionada, esto favorece 
                '    trabajar con combinados por ejemplo.
                '
                IniciaBotonesFamilias()
                IniciaBotonesArticulos()
                FamAdelante = Me.ButtonFAM28.Text
                ButtonFamAtras.Enabled = False
                ButtonFamAdelante.Enabled = False
                ButtonArtAtras.Enabled = False
                ButtonArtAdelante.Enabled = False
                CambioColores = False
                CargaFamilias()
                If HayFamilias Then
                    CargaArticulos(wCodFamilia)
                End If
            End If
        End If
        '
        ' BARRA 1 = Tarifa 1, BARRA 2 = Tarifa 2
        ' Aparcar Desactivado
        '
        ButtonAPARCAR.Text = "APARCAR"
        Select Case wTarifaBarra
            Case 1
                ComboBoxTarifas.SelectedIndex = 0
                ButtonAPARCAR.Text = "B1, ATRAS"
            Case 2
                ComboBoxTarifas.SelectedIndex = 1
                ButtonAPARCAR.Text = "B2, ATRAS"
        End Select
        '
        ' Panel Informativo...
        '
        PanelDatosMesa.Visible = False
        '
        ' Correción a posicion original para controles, PRECIO / UNIDADES.
        ' Depende de determinadas variables.
        ' Dificil de controlar, pero bueno, a apechugar con esto...
        '
        If PideVENDOnOff = False And
           PidePAXOnOff = False And
           RacionOnOff = False And
           VariosCeroOnOff = False And
           PideTARIFAOnOff = False And
           ModiPVPOnOff = False Then
            '
            Label1.Left = 561
            Label2.Left = 648
            TextBoxPrecio.Left = 646
            TextBoxUnidades.Left = 561
            '
        End If
        '
        ' Si el FOM inicial es este Mismo!
        '
        If FormularioInicial = 1 Then
            wCodSala = "999"
        End If
        '
        ' Establecemos Ciertos Valores.
        '    Refresca Sala, Mesa.
        '    Fija el Foco en TextBox Cod. Barras por defecto.
        '
        TextBoxNumSala.Text = wCodSala
        TextBoxNumMesa.Text = wCodMesa
        '
        VisorTeclado.Text = "" : TextBoxCodBarras.Text = ""
        SwFoco_402 = 2 : TextBoxCodBarras.Focus()
        '
        ' Refresca el Nombre del Vendedor Actual.
        '
        If TextBoxCamarero.Text.Trim.Length > 0 Then
            If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
                LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE.Trim
            Else
                LabelNomCamarero.Text = "No lee camarero!"
            End If
        Else
            LabelNomCamarero.Text = "Sin Camarero!"
        End If
        '
        ' Refresca el Nombre de Clientes CONTADO / CREDITO.
        ' Si hay Cliente Crédito mesac.CLIENTE > 430000000
        ' Si hay Cliente Contado mesac.NIFCIF <> "" / " "
        ' Puede que NO se de ninguno de los dos casos
        '  sin embargo nunca los dos a un mismo tiempo.
        '
        'If wCliente > 0 And wCliente <> 43000000 Then
        ''
        '' Hay Clente y es Crédito
        ''
        'If LeeClienteMCO(wCliente) = True Then
        'LabelNomCliente.Text = wrLeeCODNOM.NOMBRE.Trim
        'Else
        'LabelNomCliente.Text = " "
        'End If
        'Else
        ''
        '' Comprobamos Si hay Cliente Contado
        ''
        'LabelNomCliente.Text = " "
        'End If
        '
        ' El evento Activate se puede producir varias veces.
        ' SwEntraMesa hara que lo que se encapsula aqui, OCURRA solo UNA Vez.
        ' Por tanto Aqui disparamos Acciones que no han de refrescarse
        '    cada vez que ocurra dicho evento.
        '
        If SwEntraMesa = True Then
            SwEntraMesa = False
            '
            ' Tarifas, Nombre de Tarifas.
            ' Se cargan aqui una vez para agilizar la aplicacion.
            '
            With ComboBoxTarifas
                .Text = ""
                .Items.Clear()
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP1.Trim)
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP2.Trim)
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP5.Trim)
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP6.Trim)
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP7.Trim)
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP8.Trim)
                .Items.Add(wrLeeTCONA4.Tcona4_NOMTARIPVP9.Trim)
            End With
            '
            ' Orden Plato, al entrar al Form, 1er Plato Siempre.
            '
            ContadorPlato = 1
            Select Case ContadorPlato
                Case 1, 3
                    Button1Plato.Text = ContadorPlato.ToString & "er Plato"
                Case 2
                    Button1Plato.Text = ContadorPlato.ToString & "do Plato"
                Case 4
                    Button1Plato.Text = ContadorPlato.ToString & "to Plato"
            End Select
            '
            ' PEDIR PAX
            ' Para BARRA 1 / BARRA 2, no se pide PAX.
            '
            If wMesaLibre = True And
               wrLeeTCONA4.Tcona4_PIDEPAX = "True" And
               wTarifaBarra = 0 Then
                '
                If LeeMesa_SALA1(Me.TextBoxNumSala.Text.Trim, Me.TextBoxNumMesa.Text.Trim, 1) = True Then
                    '
                    ' Si esta marcado = "True", == "NO Pide PAX"
                    '
                    If wrLeeSALA1.Sala1_PIDEPAX = "False" Then
                        EscenarioPidePAX(1)
                    End If
                End If
            End If
            '
            ' Comprobar si hay Pedidos a Domicilio Asignado a la MESA
            '    y si NO Hay ver si la Sala indica Pedirlo.
            ' Si la MESA tiene Pedido asignado, 
            '    TCONA401, carga dichos datos al llamar a la MESA.
            '
            If LblDatosPedidoDomi.Text.Trim.Length = 0 Then
                If LeeSALA(TextBoxNumSala.Text.Trim) = True Then
                    If wrLeeSALA.Sala_REPARTO = "True" Then
                        With MyFrm21
                            wrLeeMESAC.Mesac_TLFPEDIDOS = "" : WMesacTlfPed = ""
                            MyFrm21.ButtonDesvincularPedido.Enabled = False
                            .ShowDialog(Me)
                        End With
                    End If
                End If
            End If
        End If
        '
    End Sub

    Private Sub ButtonAPARCAR_Click(sender As Object, e As EventArgs) Handles ButtonAPARCAR.Click
        '
        ' Boton APARCAR
        ' Si es BARRA1 / BARRA2, solo aparca si NO HAY NADA FICHADO !!!
        '
        If wTarifaBarra > 0 Then
            If GRID1.Rows.Count = 0 Then
                Aparcar(0)
            Else
                msg = "Accion de APARCAR no permitida para BARRA."
                style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly
                title = "Por favor cierre al COBRAR."
                MsgBox(msg, style, title)
            End If
        Else
            If SwAparca Then
                Aparcar(0)
            End If
        End If
        '
    End Sub

    Private Sub ButtonCOBRAR_Click(sender As Object, e As EventArgs) Handles ButtonCOBRAR.Click
        '
        ' Formulario de COBBROS / CAMBIO, -MODAL-
        ' Si no hemos fichado NADA, No hay que COBRAR.
        '
        If MyFrm2.GRID1.Rows.Count > 0 Then
            '
            ' Si se ha fichado algo, Forzamos APARCAR
            '      para grabar los NUEVOS Movimientos en MESA
            ' CargaListaMESAs, refresca la lista e informa que
            '      las NUEVAS, pasan a Existentes.
            '
            If SwAparca Or FormularioInicial = 1 Then
                Aparcar(1)
                CargaListaMESAs(TextBoxNumMesa.Text.Trim)
            End If
            OpenFrom = 402
            MyFrm4.ShowDialog(Me)
        Else
            '
            ' No hay nada Fichado, pero se quiere COBRAR
            ' Ej.: Cerrar una mesa, en la que se han anulado sus Lineas
            '      pero OCUPA UN Nro. Factura....
            '
            If ExisteRegistroMESAC(TextBoxNumMesa.Text.Trim, 1) Then
                Aparcar(1)
                CargaListaMESAs(TextBoxNumMesa.Text.Trim)
                OpenFrom = 402
                MyFrm4.ShowDialog(Me)
            End If
        End If
        '
    End Sub

    Private Sub ButtonZoomGRID1_Click(sender As Object, e As EventArgs) Handles ButtonZoomGRID1.Click
        '
        ' Formulario de vista ampliada GRID1
        ' -MODAL-
        '
        If Me.GRID1.Rows.Count > 0 Then
            MyFrm3.ShowDialog(Me)
        End If
        '
    End Sub

    Private Sub TextBoxPrecio_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPrecio.LostFocus
        TextBoxPrecio.BackColor = Color.Black
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '
        '  Timer para usos varios. [Por ahora esta desctivado Siempre]
        '  Inialmente se usó para hacer Blink en Color Rojo al Boton Articulos Pulsado...
        '
        LabelNotaCountDown.Text = CountDown.ToString(fmtCero)
        CountDown -= 1
        If CountDown < 0 Then
            PanelDatosMesa.Visible = False
            Timer1.Stop()
        End If
        '
    End Sub

    Private Sub Button1Plato_Click(sender As Object, e As EventArgs) Handles Button1Plato.Click
        '
        ContadorPlato += 1
        If ContadorPlato > 4 Then
            ContadorPlato = 1
        End If
        Select Case ContadorPlato
            Case 1, 3
                Button1Plato.Text = ContadorPlato.ToString & "er Plato"
            Case 2
                Button1Plato.Text = ContadorPlato.ToString & "do Plato"
            Case 4
                Button1Plato.Text = ContadorPlato.ToString & "to Plato"
        End Select
        '
    End Sub

    Private Sub ButtonRestaUnidN_Click(sender As Object, e As EventArgs) Handles ButtonRestaUnidN.Click
        '
        ' Resta Unidades, NUEVA y EXISTENTES.
        ' Marcar como "N", para APARCAR.
        ' Para pode BORRAR existentes, 
        '   se ha de tener PRIVILEGIOS elevados o pedir credenciales.
        '----------------------------------------------------------------------------------------------
        ' Comprobamos Claves y NIVEL del Vendedor Actual. Tabla [CLAVES]
        '
        If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
        Else
            wrLeeCODNOM.NIVELACCESO = 0
        End If
        '
        ' >5, 5 "SUPER", 4 "JEFE", 3 "ENCARGADO", permitidos.
        ' Conocemos Las Unidades Nuevas y Existentes que tenemos.
        '
        If GRID1.Rows.Count > 0 Then
            If GRID1.SelectedRows.Count > 0 Then
                MisUnidadesE = CDec(GRID1.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
                MisUnidadesN = CDec(GRID1.SelectedCells(3).Value.ToString.Trim.Replace(".", ","))
            End If
        Else
            Exit Sub
        End If
        '
        ' Unidades nuevas se podran eliminar siempre,
        '  para NIVELES acceso inferiores. ( <3 ).
        '
        If wrLeeCODNOM.NIVELACCESO < 3 And MisUnidadesN = 0 Then
            '
            ' Llamada al Teclado Flotante, pedir Clave.
            '
            With wrTecladoFlotante
                '
                ' 0=Numérico, 1=Alfabético, 2=Alfanumérico
                '
                .Tipo = 2
                '
                ' Mensaje al usuario
                '
                .MensaUsuario = "Por favor digite una clave."
                '
                ' 0=No, 1=Pide Pwd
                '
                .PidePwd = 1
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
                .MaxChar = 20
                '
                ' Codigo que indica el control al que retornar el texto.
                ' Se controla en la aplicación.
                ' Para este Caso le envío 999
                '
                .CodigoRetorno = 999
                '
            End With
            MyFrm15.ShowDialog(Me)
            Exit Sub
        End If
        '
        ' Podemos eliminar unidades Nuevas / Existentes 
        ' SIEMPRE para NUEVAS !!!
        '
        ' Existentes SOLO Niveles elevados o con credenciales.
        ' No obstante aqui hay un filtro mas para NIVELES ALTOS.
        ' Boton Menos debe tener permiso asignado.
        '
        If wrCLAVES.BOTONMENOS = "True" Or MisUnidadesN > 0 Then
            RestaUnaBotonMenos()
        End If
        '
    End Sub

    Private Sub ButtonSumaUnidN_Click(sender As Object, e As EventArgs) Handles ButtonSumaUnidN.Click
        '
        ' Suma Unidades, NUEVA y EXISTENTES
        ' Marcar como "N", para APARCAR
        '
        Dim MisUnidadesN As Decimal = 0
        Dim MisUnidadesE As Decimal = 0
        Dim MiImporte As Double = 0
        Dim MiPVPMedio As Double = 0
        '
        If GRID1.Rows.Count > 0 Then
            If GRID1.SelectedRows.Count > 0 Then
                MisUnidadesE = CDec(GRID1.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
                MisUnidadesN = CDec(GRID1.SelectedCells(3).Value.ToString.Trim.Replace(".", ","))
                MiPVPMedio = CDbl(GRID1.SelectedCells(4).Value.ToString.Trim.Replace(".", ",")) / MisUnidadesE
                MisUnidadesN += 1
                MisUnidadesE += 1
                GRID1.SelectedCells(1).Value = MisUnidadesE.ToString.Trim.Replace(",", ".")
                GRID1.SelectedCells(3).Value = MisUnidadesN.ToString.Trim.Replace(",", ".")
                '
                ' Calculo Nuevo Importe / Basado en Media Precio
                '
                MiImporte = MisUnidadesE * MiPVPMedio
                GRID1.SelectedCells(4).Value = MiImporte.ToString(fmtImporte).Replace(",", ".")
                GRID1.SelectedCells(5).Value = "N"
            End If
            '
            wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
            LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")
            '
        End If
        '
    End Sub

    Private Sub GRID1_Click(sender As Object, e As EventArgs) Handles GRID1.Click
        '
        If GRID1.Rows.Count > 0 Then
            ButtonRestaUnidN.Enabled = True
            ButtonSumaUnidN.Enabled = True
            If GRID1.SelectedRows.Count > 0 Then
                DameImagenProducto(GRID1.SelectedCells(0).Value.ToString.Trim)
                If GRID1.SelectedCells(6).Value.ToString.Trim.Length > 0 Then
                    ButtonVerCombi.Enabled = True
                    ButtonVerCombi.BackColor = SystemColors.Info
                Else
                    ButtonVerCombi.Enabled = False
                    ButtonVerCombi.BackColor = SystemColors.ButtonFace
                End If
            End If
        End If
        '
    End Sub

    Private Sub ButtonFACTU_Click(sender As Object, e As EventArgs) Handles ButtonFACTU.Click
        '
        ' Impresion TICKET (FACTURA)
        '
        If MyFrm2.GRID1.Rows.Count > 0 Then
            '
            ' 1 - Aparcar:
            ' Si se ha fichado algo, Forzamos APARCAR
            '      para grabar los NUEVOS Movimientos en MESA.
            ' CargaListaMESAs, refresca la lista e informa que
            '      las NUEVAS, pasan a Existentes.
            '
            If SwAparca Or FormularioInicial = 1 Then
                Aparcar(1)
                CargaListaMESAs(TextBoxNumMesa.Text.Trim)
            End If
            '
            ' 2 - Imprime la factura.
            '
            If ExisteRegistroMESAC(Me.TextBoxNumMesa.Text.Trim, 0) = True Then
                ImprimeTicketFactura("N")
            End If
        End If
        '
    End Sub

    Private Sub ButtonFACTUA4_Click(sender As Object, e As EventArgs) Handles ButtonFACTUA4.Click
        '
        ' Impresion FACTURA A4
        '
        If MyFrm2.GRID1.Rows.Count > 0 Then
            '
            ' 1 - Aparcar:
            ' Si se ha fichado algo, Forzamos APARCAR
            '      para grabar los NUEVOS Movimientos en MESA.
            ' CargaListaMESAs, refresca la lista e informa que
            '      las NUEVAS, pasan a Existentes.
            '
            If SwAparca Or FormularioInicial = 1 Then
                Aparcar(1)
                CargaListaMESAs(TextBoxNumMesa.Text.Trim)
            End If
            '
            ' 2 - Imprime la factura (A4).
            '     iOpc="*", por ahora es ignorado para A4.
            '
            If ExisteRegistroMESAC(Me.TextBoxNumMesa.Text.Trim, 0) = True Then
                ImprimeFacturaA4("*")
            End If
        End If
        '
    End Sub

    Private Sub ButtonRacion_Click(sender As Object, e As EventArgs) Handles ButtonRacion.Click
        '
        ' Ración - PVP1
        '
        'Me.TextBoxPrecio.Text = CDbl(wrLeeMAR.Mar_PREPVP1.Trim).ToString(fmtPrecio).Replace(",", ".")
        Me.TextBoxPrecio.Text = CDbl(wrLeeMAR.Mar_PREPVPTPV.Trim).ToString(fmtPrecio).Replace(",", ".")
        With wrRACIONES
            .RACIONES_indicador = 1
            .RACIONES_PVPRacion = Me.TextBoxPrecio.Text.Trim
            .RACIONES_PVPMediaRacion = ""
            .RACIONES_IMPORTE = ""
        End With
        TerminarRacion()
        '
    End Sub

    Private Sub ButtonMediaRacion_Click(sender As Object, e As EventArgs) Handles ButtonMediaRacion.Click
        '
        ' 1/2 Ración - PVP4
        '
        Me.TextBoxPrecio.Text = CDbl(wrLeeMAR.Mar_PREPVP4.Trim).ToString(fmtPrecio).Replace(",", ".")
        With wrRACIONES
            .RACIONES_indicador = 2
            .RACIONES_PVPRacion = ""
            .RACIONES_PVPMediaRacion = Me.TextBoxPrecio.Text.Trim
            .RACIONES_IMPORTE = ""
        End With
        TerminarRacion()
        '
    End Sub

    Private Sub ButtonCancelar_Click(sender As Object, e As EventArgs) Handles ButtonCancelar.Click
        '
        ' Botón Cancelar para ciertas operaciones
        '
        ' Al pedir Cambio Tarifa.
        '
        If PideTARIFAOnOff Then
            EscenarioPideTARIFA(0)
            TextBoxCodBarras.Text = ""
            LabelInfoUSR.Text = " "
            LabelInfoUSR.Visible = False
            Timer1.Enabled = False
            VisorTeclado.Text = ""
            SwFoco_402 = 2
            TextBoxCodBarras.Focus()
            Exit Sub
        End If
        '
        ' Al pedir Vendedor.
        '
        If PideVENDOnOff Then
            EscenarioPideVEND(0)
            TextBoxCodBarras.Text = ""
            LabelInfoUSR.Text = " "
            LabelInfoUSR.Visible = False
            Timer1.Enabled = False
            VisorTeclado.Text = ""
            SwFoco_402 = 2
            TextBoxCodBarras.Focus()
            Exit Sub
        End If
        '
        ' Al pedir Personas en la mesa (PAX).
        '
        If PidePAXOnOff Then
            EscenarioPidePAX(0)
            TextBoxCodBarras.Text = ""
            LabelInfoUSR.Text = " "
            LabelInfoUSR.Visible = False
            Timer1.Enabled = False
            VisorTeclado.Text = ""
            SwFoco_402 = 2
            TextBoxCodBarras.Focus()
            Exit Sub
        End If
        '
        ' Raciones.
        '
        If RacionOnOff Then
            EscenarioRaciones(0)
            wUnidN = CDbl(TextBoxUnidades.Text.ToString)
            TextBoxCodBarras.Text = ""
            wUnidN = 0 : wImporteN = 0
            ColoreaGRID1("TCONA402")
            wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
            TextBoxUnidades.Text = "" : TextBoxPrecio.Text = ""
            LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")
            LabelInfoUSR.Text = " "
            LabelInfoUSR.Visible = False
            Timer1.Enabled = False
            VisorTeclado.Text = ""
            'wTempArtBoton.BackColor = WcolReservar
            SwFoco_402 = 2
            TextBoxCodBarras.Focus()
        End If
        '
        ' PVP=0 o Modificar PVP.
        '
        If VariosCeroOnOff Or ModiPVPOnOff Then
            Escenario_PVPCERO(0)
            '
            If TextBoxUnidades.Text.Trim.Length = 0 Then
                TextBoxUnidades.Text = "0,00"
            End If
            wUnidN = CDbl(TextBoxUnidades.Text.ToString)
            '
            TextBoxCodBarras.Text = ""
            wUnidN = 0 : wImporteN = 0
            ColoreaGRID1("TCONA402")
            wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
            TextBoxUnidades.Text = "" : TextBoxPrecio.Text = ""
            LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")
            LabelInfoUSR.Text = " "
            LabelInfoUSR.Visible = False
            Timer1.Enabled = False
            VisorTeclado.Text = ""
            'wTempArtBoton.BackColor = WcolReservar
            SwFoco_402 = 2
            TextBoxCodBarras.Focus()
        End If
        '
    End Sub

    Private Sub TextBoxCodBarras_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCodBarras.LostFocus
        TextBoxCodBarras.BackColor = Color.Black
    End Sub

    Private Sub TextBoxUnidades_LostFocus(sender As Object, e As EventArgs) Handles TextBoxUnidades.LostFocus
        TextBoxUnidades.BackColor = Color.Black
    End Sub

    Private Sub ButtonCABE_Click(sender As Object, e As EventArgs) Handles ButtonCABE.Click
        '
        MyFrm5.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonCambioMesa_Click(sender As Object, e As EventArgs) Handles ButtonCambioMesa.Click
        '
        ' Formulario de Cambio de MESA
        ' -MODAL-
        '
        If Me.GRID1.Rows.Count > 0 Then
            '
            ' Si se ha fichado algo, Forzamos APARCAR
            '      para grabar los NUEVOS Movimientos en MESA
            ' CargaListaMESAs, refresca la lista e informa que
            '      las NUEVAS, pasan a Existentes.
            '
            If SwAparca Or FormularioInicial = 1 Then
                Aparcar(1)
                CargaListaMESAs(TextBoxNumMesa.Text.Trim)
            End If
            '
            MyFrm12.ShowDialog(Me)
        End If
        '
    End Sub


    Private Sub EscenarioPidePAX(wOnOff As Integer)
        '
        ' Preparamos el Escenario Para Pedir PAX, SOLO MESAS Libres
        '    PidePAXOnOff
        '
        Select Case wOnOff
            Case 0
                PidePAXOnOff = False
                With LabelInfoUSR
                    .Top = .Top + 409
                    .Text = " "
                    .Visible = False
                End With
                With LabelMensaGrande
                    .Text = " "
                    .Visible = False
                End With
                '
                Label1.Visible = True
                Label2.Visible = True
                Label3.Visible = True
                '
                TextBoxPrecio.Visible = True
                TextBoxUnidades.Visible = True
                TextBoxCodBarras.Visible = True
                '
                Label1.Left = 561
                Label2.Left = 648
                TextBoxPrecio.Left = 646
                TextBoxUnidades.Left = 561
                '
                For Each wControl In Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = True
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = True
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" _
                    And NombreBoton <> "ButtonCLR" _
                    And NombreBoton <> "ButtonGuion" _
                    And NombreBoton <> "ButtonPrecio" _
                    And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" _
                    And NombreBoton <> "ButtonRacion" And NombreBoton <> "ButtonMediaRacion" Then
                            With wControl
                                'If wControl.Name IsNot wTempBoton.Name Then
                                .Visible = True
                                'Else
                                .Enabled = True
                                'End If
                            End With
                        End If
                    End If
                Next
                '
                'ButtonRacion.Enabled = False
                'ButtonMediaRacion.Enabled = False
                'ButtonRacion.Visible = False
                'ButtonMediaRacion.Visible = False
                '
                ButtonCancelar.Visible = False
                TextBoxCodBarras.Focus()
            Case 1
                PidePAXOnOff = True
                With LabelInfoUSR
                    .Top = 455
                    .Text = "Digite Número de Personas."
                    .Visible = True
                End With
                With LabelMensaGrande
                    TEMP = ""
                    TEMP += "Sala.: " & TextBoxNumSala.Text & " / "
                    TEMP += "Mesa.: " & TextBoxNumMesa.Text
                    TEMP += vbCrLf & vbCrLf
                    TEMP += "Por favor"
                    TEMP += vbCrLf
                    TEMP += "Digite Número de "
                    TEMP += vbCrLf
                    TEMP += "Personas a la mesa."
                    .Text = TEMP
                    .Visible = True
                End With
                '
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                '
                TextBoxPrecio.Visible = False
                TextBoxUnidades.Visible = False
                TextBoxCodBarras.Visible = False
                '
                For Each wControl In Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = False
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = False
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" And NombreBoton <> "ButtonCLR" _
                            And NombreBoton <> "ButtonGuion" And NombreBoton <> "ButtonPrecio" _
                            And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" Then
                            With wControl
                                'If wControl.Name IsNot wTempBoton.Name Then
                                .Visible = False
                                'Else
                                .Enabled = False
                                'End If
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = True
                TextBoxPax.BackColor = Color.Red
                TextBoxPax.Focus()
        End Select
        '
    End Sub

    Private Sub EscenarioPideVEND(wOnOff As Integer)
        '
        ' Preparamos el Escenario Para Pedir Camarero.
        '
        Select Case wOnOff
            Case 0
                PideVENDOnOff = False
                With LabelInfoUSR
                    .Top = .Top + 409
                    .Text = " "
                    .Visible = False
                End With
                With LabelMensaGrande
                    .Top = 209
                    .Text = " "
                    .Visible = False
                End With
                PanelLisVen.Visible = False
                '
                Label1.Visible = True
                Label2.Visible = True
                Label3.Visible = True
                '
                TextBoxPrecio.Visible = True
                TextBoxUnidades.Visible = True
                TextBoxCodBarras.Visible = True
                '
                Label1.Left = 561
                Label2.Left = 648
                TextBoxPrecio.Left = 646
                TextBoxUnidades.Left = 561
                '
                For Each wControl In Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = True
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = True
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" _
                    And NombreBoton <> "ButtonCLR" _
                    And NombreBoton <> "ButtonGuion" _
                    And NombreBoton <> "ButtonPrecio" _
                    And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" _
                    And NombreBoton <> "ButtonRacion" And NombreBoton <> "ButtonMediaRacion" Then
                            With wControl
                                .Visible = True
                                .Enabled = True
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = False
                TextBoxCamarero.BackColor = Color.Black
                TextBoxCodBarras.Focus()
            Case 1
                PideVENDOnOff = True
                With LabelInfoUSR
                    .Top = 455
                    .Text = "Seleccione Camarero."
                    .Visible = True
                End With
                With LabelMensaGrande
                    .Top = 17
                    TEMP = ""
                    TEMP += "Sala.: " & TextBoxNumSala.Text & " / "
                    TEMP += "Mesa.: " & TextBoxNumMesa.Text
                    TEMP += vbCrLf & vbCrLf
                    TEMP += "Por favor"
                    TEMP += vbCrLf
                    TEMP += "Seleccione un nuevo"
                    TEMP += vbCrLf
                    TEMP += "Camarero para la mesa."
                    .Text = TEMP
                    .Visible = True
                End With
                CargaVendedores(3)
                PanelLisVen.Top = 409
                PanelLisVen.Visible = True
                '
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                '
                TextBoxPrecio.Visible = False
                TextBoxUnidades.Visible = False
                TextBoxCodBarras.Visible = False
                '
                For Each wControl In Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = False
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = False
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" And NombreBoton <> "ButtonCLR" _
                            And NombreBoton <> "ButtonGuion" And NombreBoton <> "ButtonPrecio" _
                            And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" Then
                            With wControl
                                .Visible = False
                                .Enabled = False
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = True
                TextBoxCamarero.BackColor = Color.Red
                TextBoxCamarero.Focus()
        End Select
        '
    End Sub

    Private Sub EscenarioPideTARIFA(wOnOff As Integer)
        '
        ' Preparamos el Escenario Para Pedir (Cambio de) TARIFA.
        '
        Select Case wOnOff
            Case 0
                PideTARIFAOnOff = False
                With LabelInfoUSR
                    .Top = .Top + 409
                    .Text = " "
                    .Visible = False
                End With
                With LabelMensaGrande
                    .Top = 209
                    .Text = " "
                    .Visible = False
                End With
                PanelLisVen.Visible = False
                '
                Label1.Visible = True
                Label2.Visible = True
                Label3.Visible = True
                '
                TextBoxPrecio.Visible = True
                TextBoxUnidades.Visible = True
                TextBoxCodBarras.Visible = True
                '
                Label1.Left = 561
                Label2.Left = 648
                TextBoxPrecio.Left = 646
                TextBoxUnidades.Left = 561
                '
                For Each wControl In Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = True
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = True
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" _
                    And NombreBoton <> "ButtonCLR" _
                    And NombreBoton <> "ButtonGuion" _
                    And NombreBoton <> "ButtonPrecio" _
                    And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" _
                    And NombreBoton <> "ButtonRacion" And NombreBoton <> "ButtonMediaRacion" Then
                            With wControl
                                .Visible = True
                                .Enabled = True
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = False
                TextBoxCamarero.BackColor = Color.Black
                TextBoxCodBarras.Focus()
            Case 1
                PideTARIFAOnOff = True
                With LabelInfoUSR
                    .Top = 455
                    .Text = "Seleccione una Tarifa."
                    .Visible = True
                End With
                With LabelMensaGrande
                    .Top = 17
                    TEMP = ""
                    TEMP += "Sala.: " & TextBoxNumSala.Text & " / "
                    TEMP += "Mesa.: " & TextBoxNumMesa.Text
                    TEMP += vbCrLf & vbCrLf
                    TEMP += "Por favor"
                    TEMP += vbCrLf
                    TEMP += "Seleccione una TARIFA."
                    TEMP += vbCrLf
                    TEMP += " "
                    .Text = TEMP
                    .Visible = True
                End With
                CargaGRIDTarifas()
                PanelLisVen.Top = 409
                PanelLisVen.Visible = True
                '
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                '
                TextBoxPrecio.Visible = False
                TextBoxUnidades.Visible = False
                TextBoxCodBarras.Visible = False
                '
                For Each wControl In Controls
                    '
                    If wControl.Name = "Panel1" Then
                        wControl.Visible = False
                    End If
                    '
                    If wControl.Name = "GRID1" Then
                        wControl.Enabled = False
                    End If
                    '
                    If TypeOf wControl Is Button Then
                        NombreBoton = CType(wControl, Button).Name
                        '
                        If Mid$(NombreBoton, 1, 9) <> "ButtonCal" And NombreBoton <> "ButtonCLR" _
                            And NombreBoton <> "ButtonGuion" And NombreBoton <> "ButtonPrecio" _
                            And NombreBoton <> "ButtonEnter" And NombreBoton <> "ButtonCancelar" Then
                            With wControl
                                .Visible = False
                                .Enabled = False
                            End With
                        End If
                    End If
                Next
                '
                ButtonCancelar.Visible = True
                GRIDLISTAVend.Focus()
        End Select
        '
    End Sub


    Private Sub TextBoxPax_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPax.GotFocus
        '
        ' Se permite aun cuando este OCUPADA...
        '
        'If wMesaLibre Then
        SwFoco_402 = 4
        TextBoxPax.BackColor = Color.Red
        'End If
        '
    End Sub

    Private Sub TextBoxPax_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPax.LostFocus
        TextBoxPax.BackColor = Color.Black
    End Sub

    Private Sub ButtonPAX_Click(sender As Object, e As EventArgs) Handles ButtonPAX.Click
        '
        ' Se permite modificar PAX, estando OCUPADA.
        '
        'If wMesaLibre = True Then
        VisorTeclado.Text = ""
        EscenarioPidePAX(1)
        'End If
        '
    End Sub

    Private Sub ButtonSEPARAR_Click(sender As Object, e As EventArgs) Handles ButtonSEPARAR.Click
        '
        ' Formulario Separar Cuenta
        ' -MODAL-
        '
        If Me.GRID1.Rows.Count > 1 Then
            '
            ' Si se ha fichado algo, Forzamos APARCAR
            '      para grabar los NUEVOS Movimientos en MESA
            ' CargaListaMESAs, refresca la lista e informa que
            '      las NUEVAS, pasan a Existentes.
            '
            If SwAparca Or FormularioInicial = 1 Then
                Aparcar(1)
                CargaListaMESAs(TextBoxNumMesa.Text.Trim)
            End If
            '
            MyFrm13.ShowDialog(Me)
        End If
        '
    End Sub

    Private Sub ButtonBEBIDAS_Click(sender As Object, e As EventArgs) Handles ButtonBEBIDAS.Click
        '
        ' BEBIDAS FAVORITAS
        '
        IniciaBotonesArticulos()
        ButtonArtAtras.Enabled = False
        ButtonArtAdelante.Enabled = False
        '
        CargaFavoritos("BEBIDAS")
        '
    End Sub

    Private Sub CargaFavoritos(wCrgFAVO As String)
        '
        ' ARTICULOS FAVORITOS.
        '
        Dim PVPControl As Double = 0
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim NumBTnART As Integer = 30 : Dim IndBTnART As Integer = 1
        '
        ' Orden de Carga Por Defecto ORDEN ALFABETICO
        '
        Dim queryString As String = ""
        queryString &= "SELECT "
        queryString &= "[FABO3].[TIPO], [FABO3].[ARTICULO], [MAR].[DESCRIPCION], [MAR].[IMAGEN] "
        queryString &= "FROM [FABO3], [MAR] "
        queryString &= "WHERE [FABO3].[TIPO]='" & wCrgFAVO & "' AND "
        queryString &= "[MAR].[NARTICULO]=[FABO3].[ARTICULO] "
        queryString &= "ORDER BY [MAR].[DESCRIPCION]"
        '
        Dim dt As DataSet = New DataSet
        Dim NombreTabla As String = "FABOMAR"
        '
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, NombreTabla)
            '
            If dt.Tables(NombreTabla).Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables(NombreTabla).Rows
                    If LeeMar(pRow("ARTICULO").ToString()) Then
                        '
                        ' Control del PVP1 y Familias 1/2 Raciones
                        '
                        PVPControl = CDbl(wrLeeMAR.Mar_PREPVP1.Trim.Replace(".", ","))
                        If PVPControl > 0 Or
                        compruebaFamMediaRacion(pRow("ARTICULO").ToString()) = True Then
                            EstableceBotonART(IndBTnART, pRow("ARTICULO").ToString(), pRow("DESCRIPCION").ToString(), pRow("IMAGEN").ToString())
                            IndBTnART += 1
                        End If
                    End If
                    'If IndBTnART > NumBTnART Then
                    'ButtonArtAdelante.Enabled = True
                    'Exit For
                    'End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [FABO3]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub ButtonCOMIDAS_Click(sender As Object, e As EventArgs) Handles ButtonCOMIDAS.Click
        '
        ' COMIDAS FAVORITAS
        '
        IniciaBotonesArticulos()
        ButtonArtAtras.Enabled = False
        ButtonArtAdelante.Enabled = False
        '
        CargaFavoritos("COMIDAS")
        '
    End Sub

    Private Sub ButtonOCHO_Click(sender As Object, e As EventArgs) Handles ButtonOCHO.Click
        '
        ' Datos de la MESA, algo vistoso... :)
        '
        If LeeMesa_SALA1(wCodSala, TextBoxNumMesa.Text.Trim, 1) = True Then
            '
            ' Datos Mesa
            '
            LabelNotaMesa.Text = "Mesa.: " & TextBoxNumMesa.Text.Trim
            LabelNotaSala.Text = ".Sala ... " & wCodSala
            LabelNotaCamarero.Text = ".Camarero ... " & TextBoxCamarero.Text.Trim
            LabelNotaPAX.Text = ".Personas ... " & TextBoxPax.Text.Trim
            LabelNotaFAC.Text = ".Factura ... " & TextBoxFactura.Text.Trim
            '
            ' Fecha / Hora Apartura
            '
            If wrLeeSALA1.Sala1_HORAAPAERTURA.Trim.Length = 0 Then
                wrLeeSALA1.Sala1_HORAAPAERTURA = Format(Date.Now.ToShortTimeString, "HH:MM:SS")
            End If
            If wrLeeSALA1.Sala1_FECAPERTURA.Trim.Length > 0 Then
                LabelNotaFECHA.Text = ".Apertura ... " & Format(CDate(wrLeeSALA1.Sala1_FECAPERTURA.Trim), "dd/MM/yy")
                Dim dtTime As DateTime = Convert.ToDateTime(wrLeeSALA1.Sala1_HORAAPAERTURA.Trim)
                LabelNotaFECHA.Text &= " " & Format(dtTime, "HH:mm")
            Else
                LabelNotaFECHA.Text = ".Apertura ... " & Format(Date.Now.ToShortDateString, "dd/MM/yy")
                LabelNotaFECHA.Text &= " " & Format(Date.Now.ToShortTimeString, "HH:MM:SS")
            End If
            LabelNotaImporte.Text = " > Importe.: " & LabelTotComanda.Text.Trim & "€"
            '
            CountDown = 5 'Segundos
            Timer1.Start()
            '
            With PanelDatosMesa
                .Left = GRID1.Left
                .Top = 9
                .Visible = True
            End With
        End If
        '
    End Sub

    Private Sub LabelNotaVISTO_Click(sender As Object, e As EventArgs) Handles LabelNotaVISTO.Click
        '
        ' Cerrar Panel Datos Mesa.
        '
        Timer1.Stop()
        CountDown = 0
        PanelDatosMesa.Visible = False
        '
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRID1
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
        With GRID1
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

    Private Sub ButtonTARIFAS_Click(sender As Object, e As EventArgs) Handles ButtonTARIFAS.Click
        'ComboBoxTarifas.DropDownStyle = ComboBoxStyle.DropDown
        'ComboBoxTarifas.DroppedDown = True
        '----------------------------------------------------------------------------------------------
        '  Cambio de TARIFA.
        '  Se ha de tener PRIVILEGIOS elevados o pedir credenciales.
        '----------------------------------------------------------------------------------------------
        ' Comprobamos Claves y NIVEL del Vendedor Actual. Tabla [CLAVES]
        '
        If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
        Else
            wrLeeCODNOM.NIVELACCESO = 0
        End If
        '
        ' >5, 5 "SUPER", 4 "JEFE", 3 "ENCARGADO", permitidos.
        '
        If wrLeeCODNOM.NIVELACCESO < 3 Then
            '
            ' Llamada al Teclado Flotante, pedir Clave.
            '
            With wrTecladoFlotante
                '
                ' 0=Numérico, 1=Alfabético, 2=Alfanumérico
                '
                .Tipo = 2
                '
                ' 0=No, 1=Pide Pwd
                '
                .PidePwd = 1
                '
                ' Mensaje al usuario
                '
                .MensaUsuario = "Por favor digite una clave."
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
                .MaxChar = 20
                '
                ' Codigo que indica el control al que retornar el texto.
                ' Se controla en la aplicación.
                ' Para este Caso le envío 995
                '
                .CodigoRetorno = 995
                '
            End With
            MyFrm15.ShowDialog(Me)
            Exit Sub
        End If
        '
        ' Podemos Modificar la Tarifa
        '   para Niveles elevados o con credenciales.
        ' Pasa un ultimo filtro, BOTONTARIFA.
        '
        If wrCLAVES.BOTONTARIFA = "True" Then
            VisorTeclado.Text = ""
            EscenarioPideTARIFA(1)
        End If
        '
    End Sub

    Private Sub ComboBoxTarifas_TextChanged(sender As Object, e As EventArgs) Handles ComboBoxTarifas.TextChanged
        '
        ComboBoxTarifas.DropDownStyle = ComboBoxStyle.Simple
        ButtonTARIFAS.Select()
        '
    End Sub

    Private Sub ButtonMENSA_Click(sender As Object, e As EventArgs) Handles ButtonMENSA.Click
        '
        ' Mensajes a Ciertas Areas...
        ' - FROM MODAL-
        '
        With MyFrm18
            TCONA418_Started = False
            .ButtonTerminarMESA.Text = "Terminar Mesa " & Me.TextBoxNumMesa.Text.Trim
            .ShowDialog(Me)
        End With
        '
    End Sub

    Private Sub ButtonEnviar_Click(sender As Object, e As EventArgs) Handles ButtonEnviar.Click
        '
        ' Muestra lo que se va a enviar a las distintas AREAS.
        ' 0 - Impresion     TICKETS a Areas determinadas.
        ' 1 - Visualizacion TICKETS a Areas determinadas.
        '
        ' Formulario de vista Datos AREAS
        ' -MODAL-
        '
        If Me.GRID1.Rows.Count > 0 Then
            '
            ' Se comprueba si Hay Lineas/Unidades NUEVAS.
            '
            If HayLineasNuevasGRID1(1) = True Then
                '
                ' Se genera lo que hay que enviar a AREAS.
                ' En este caso es Para VISUALIZAR.
                '
                GeneraTICKETSaAreas(1)
                '
                ' Y Si hay algo que enviar a Areas
                '  muestra el resultado.
                '
                With MyFrm16
                    If .GRIDENVAREAS.Rows.Count > 0 Then
                        .ShowDialog(Me)
                    End If
                End With
            End If
        End If
        '
    End Sub

    Private Sub ButtonVerCombi_Click(sender As Object, e As EventArgs) Handles ButtonVerCombi.Click
        '
        ' Si hay combinados, permiter verlos temporalmente
        '
        If Me.GRID1.Rows.Count > 0 Then
            If GRID1.SelectedRows.Count > 0 Then
                If GRID1.SelectedCells(6).Value.ToString.Trim.Length > 0 Then
                    '
                    ' wZoomOPC = 3 :: *** Combinados *** Si Hay
                    '
                    Dim words As String() = GRID1.SelectedCells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                    TEMP = ""
                    TEMP &= GRID1.SelectedCells(2).Value.ToString & vbCrLf
                    For i As Integer = 0 To words.Length - 1
                        If LeeMar(words(i)) = False Then
                            wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                        Else
                            TEMP &= "  [+] " & wrLeeMAR.Mar_DESCRIPCION & vbCrLf
                        End If
                    Next
                    '
                    ' A pantalla
                    '
                    msg = TEMP
                    style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                    title = "*** Combinados Para este Producto ***"
                    MsgBox(msg, style, title)
                End If
            End If
        End If
        '
    End Sub

    Private Sub GRID1_SelectionChanged(sender As Object, e As EventArgs) Handles GRID1.SelectionChanged
        '
        If GRID1.Rows.Count > 0 Then
            ButtonRestaUnidN.Enabled = True
            ButtonSumaUnidN.Enabled = True
            If GRID1.SelectedRows.Count > 0 Then
                DameImagenProducto(GRID1.SelectedCells(0).Value.ToString.Trim)
                If GRID1.SelectedCells(6).Value.ToString.Trim.Length > 0 Then
                    ButtonVerCombi.Enabled = True
                    ButtonVerCombi.BackColor = SystemColors.Info
                Else
                    ButtonVerCombi.Enabled = False
                    ButtonVerCombi.BackColor = SystemColors.ButtonFace
                End If
            End If
        End If
        '
    End Sub

    Private Sub DameImagenProducto(wArti As String)
        '
        ' Muestra la imagen del producto si la tiene...
        ' PARATE AQUI
        '
        ProductoImagen.Image = Nothing
        If LeeMar(wArti) = True Then
            If wrLeeMAR.Mar_IMAGEN.Trim.Length > 0 Then
                MiFileExist = My.Computer.FileSystem.FileExists(wrLeeMAR.Mar_IMAGEN.Trim)
                If MiFileExist = True Then
                    ProductoImagen.Image = Image.FromFile(wrLeeMAR.Mar_IMAGEN)
                End If
            End If
        End If
        '
    End Sub

    Private Sub ButtonCAMARERO_Click(sender As Object, e As EventArgs) Handles ButtonCAMARERO.Click
        '
        '  Cambio de Camarero (Vendedor).
        '  Se ha de tener PRIVILEGIOS elevados o pedir credenciales.
        '----------------------------------------------------------------------------------------------
        ' Comprobamos Claves y NIVEL del Vendedor Actual. Tabla [CLAVES]
        '
        If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
        Else
            wrLeeCODNOM.NIVELACCESO = 0
        End If
        '
        ' >5, 5 "SUPER", 4 "JEFE", 3 "ENCARGADO", permitidos.
        '
        If wrLeeCODNOM.NIVELACCESO < 3 Then
            '
            ' Llamada al Teclado Flotante, pedir Clave.
            '
            With wrTecladoFlotante
                '
                ' 0=Numérico, 1=Alfabético, 2=Alfanumérico
                '
                .Tipo = 2
                '
                ' 0=No, 1=Pide Pwd
                '
                .PidePwd = 1
                '
                ' Mensaje al usuario
                '
                .MensaUsuario = "Por favor digite una clave."
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
                .MaxChar = 20
                '
                ' Codigo que indica el control al que retornar el texto.
                ' Se controla en la aplicación.
                ' Para este Caso le envío 997
                '
                .CodigoRetorno = 997
                '
            End With
            MyFrm15.ShowDialog(Me)
            Exit Sub
        End If
        '
        ' Podemos Modificar el Camarero
        '   para Niveles elevados o con credenciales.
        '
        VisorTeclado.Text = ""
        EscenarioPideVEND(1)
        '
    End Sub

    Private Sub TextBoxCamarero_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCamarero.GotFocus
        '
        SwFoco_402 = 5
        TextBoxPax.BackColor = Color.Red
        '
    End Sub

    Private Sub TextBoxCamarero_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCamarero.LostFocus
        TextBoxPax.BackColor = Color.Black
    End Sub

    Private Sub ButtonGRIDCaArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCaArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDLISTAVend
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

    Private Sub ButtonGRIDCaAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCaAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDLISTAVend
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

    Private Sub ButtonCamareroSel_Click(sender As Object, e As EventArgs) Handles ButtonCamareroSel.Click
        '
        ' Selecciona un Elemento de la lista
        '
        With GRIDLISTAVend
            If .SelectedRows.Count > 0 Then
                '
                ' Si Pide Camarero
                '
                If PideVENDOnOff = True Then
                    '
                    ' Selecciona un camarero de la lista
                    '
                    TextBoxCamarero.Text = .SelectedCells(0).Value.ToString
                    LabelNomCamarero.Text = .SelectedCells(1).Value.ToString
                    If Me.TextBoxCamarero.Text.Trim.Length = 0 Then
                        Me.TextBoxCamarero.Text = "0"
                    End If
                    EscenarioPideVEND(0)
                    CambiaVendedorMesa(CInt(TextBoxFactura.Text.Trim))
                    If LeeVendedor(CInt(Me.TextBoxCamarero.Text.Trim)) = True Then
                        LabelNomCamarero.Text = wrLeeCODNOM.NOMBRE
                    Else
                        LabelNomCamarero.Text = "*NO LEIDO*"
                    End If
                    '
                    ' Boton [-] del teclado Fijo. El Camarero Debe tener 
                    '   PRIVILEGIOS 3 a 5 o >5 y BOTONMENOS debe estar ACTIVADO.
                    '
                    ButtonGuion.Enabled = False
                    If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
                        If wrLeeCODNOM.NIVELACCESO > 2 Then
                            If LeeClaves(wrLeeCODNOM.NIVELACCESO) = True Then
                                If wrCLAVES.BOTONMENOS = "True" Then
                                    ButtonGuion.Enabled = True
                                End If
                            End If
                        End If
                    End If
                    '
                    PideVENDOnOff = False
                End If
                '
                ' Si Pide TARIFA
                '
                If PideTARIFAOnOff = True Then
                    '
                    ' Selecciona una TARIFA de la lista
                    '
                    ComboBoxTarifas.Text = .SelectedCells(1).Value.ToString
                    EscenarioPideTARIFA(0)
                    PideTARIFAOnOff = False
                End If

            End If
        End With
        '
    End Sub

    Private Sub ButtonModiPVP_Click(sender As Object, e As EventArgs) Handles ButtonModiPVP.Click
        '
        ' Modificar PVP.
        ' Se ha de tener PRIVILEGIOS elevados o pedir credenciales.
        '----------------------------------------------------------------------------------------------
        ' Comprobamos Claves y NIVEL del Vendedor Actual. Tabla [CLAVES]
        '
        If LeeVendedor(CInt(TextBoxCamarero.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
        Else
            wrLeeCODNOM.NIVELACCESO = 0
        End If
        '
        ' >5, 5 "SUPER", 4 "JEFE", 3 "ENCARGADO", permitidos.
        '
        If wrLeeCODNOM.NIVELACCESO < 3 Then
            '
            ' Llamada al Teclado Flotante, pedir Clave.
            '
            With wrTecladoFlotante
                '
                ' 0=Numérico, 1=Alfabético, 2=Alfanumérico
                '
                .Tipo = 2
                '
                ' 0=No, 1=Pide Pwd
                '
                .PidePwd = 1
                '
                ' Mensaje al usuario
                '
                .MensaUsuario = "Por favor digite una clave."
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
                .MaxChar = 20
                '
                ' Codigo que indica el control al que retornar el texto.
                ' Se controla en la aplicación.
                ' Para este Caso le envío 996
                '
                .CodigoRetorno = 996
                '
            End With
            MyFrm15.ShowDialog(Me)
            Exit Sub
        End If
        '
        ' Podemos Modificar PVP, SI Filtro BOTONPRECIO lo permite
        '   para Niveles elevados o con credenciales.
        '
        If wrCLAVES.BOTONPRECIO = "True" Then
            If GRID1.Rows.Count > 0 And GRID1.SelectedRows.Count > 0 Then
                VisorTeclado.Text = ""
                Escenario_PVPCERO(2)
            End If
        End If
        '
    End Sub

    Private Sub ButtonDOMI_Click(sender As Object, e As EventArgs) Handles ButtonDOMI.Click
        '
        ' Pedidos a Domicilio...
        ' - FROM MODAL-
        '
        With MyFrm21
            'TCONA421_Started = False
            .ShowDialog(Me)
        End With
        '
    End Sub

    Private Sub ButtonFACTURA_Click(sender As Object, e As EventArgs) Handles ButtonFACTURA.Click
        '
        ' Formato de Factura (A4)
        ' - FROM MODAL-
        '
        With MyFrm22
            'TCONA422_Started = False
            .ShowDialog(Me)
        End With
        '
    End Sub

End Class