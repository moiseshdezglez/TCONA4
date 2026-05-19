Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class TCONA406

    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Dim NomdbCheck As String = ""
    Private Sub TCONA406_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Select Case BtnAccion
                    Case 0
                        '
                        ' (1) Actualizamos Ref. Generales.
                        '
                        Actualiza_TCONA4(wCaja, "RefGen")
                        '
                        ' (2) Leemos de nuevo los datos de Referencia "General".
                        '     Esto "actualiza" el comportamiento de aplicación
                        '      con los NUEVOS datos de ref. Gen. 
                        '
                        LeeTCONA4Cfg("General")
                        '
                        Me.Hide()
                        TCONA405_Started = False
                    Case 1, 2
                        CancelaNuevaArea()
                End Select
        End Select
        e = Nothing
        '
    End Sub

    Private Sub PreparaGRIDs()
        '
        ' Define Algunas Propiedades de los GRDs
        '
        With GRIDAREAS
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
        With GRIDIMPRESYS
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
        With GRIDIMPREMODELOS
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
        With GRIDCLRF
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

    Private Sub TCONA406_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' INICIO FORM REF.GENERALES
        '
        LeeTCONA4Cfg("General")
        ButtonListaTablasDB1.Text = "Lista de Tablas " & Mid(DameCatalogoEmpresa(wEmpresa, "CONTATRV"), 17, 11)
        ListBoxTablasDB.Items.Clear()
        ListBoxCampos.Items.Clear()
        ListBoxPropCampos.Items.Clear()
        '
        LabelTituloRG.Text = "REFERENCIAS GENERALES - Caja actual.: " & wCaja.ToString & " " & wrLeeTCONA4.Tcona4_NOMBRECAJA
        TEMP = ""
        TEMP &= "AREAS HABITUALES" & vbCrLf
        TEMP &= "================" & vbCrLf
        TEMP &= "0=PIZZERIA      " & vbCrLf
        TEMP &= "1=BARRA         " & vbCrLf
        TEMP &= "4=COCINA        " & vbCrLf
        LabelInfoHabituales.Text = TEMP
        '
        DatosLabelInfo()
        '
        CheckBoxImpreFACCajon.Text = "Forzar Apertura CAJÓN" & vbCrLf
        CheckBoxImpreFACCajon.Text &= "(1=Siempre / 0=seguen Drivers si admite.)"
        '
        PreparaGRIDs()
        '
        LimpiaCajasTexto()
        CargaGridColores()
        '
        ' Cargamos la Lista de Claves, en funcion del NIVEL 
        ' de acceso.
        '
        CargaListaClaves(wrLeeCODNOM.NIVELACCESO)
        '
        GRIDColores.ClearSelection()
        CargaLineasTicket()
        CargaOtrosDatos()
        '
        ' Impresoras Instaladas en Windows.
        ' Impresora Por Defecto (Predeterminada).
        '
        CargaListaImpresoras()
        txtImpresoraPredeterminada.Text = ObtenerImpresoraPredeterminada()
        '
        ' Los diferentes MODELOS de impresoras con que trabajará la aplicación.
        '
        CargaModelosImpresoras()
        '
        ' Areas
        '
        If CargaListaAREAS(wCaja) = True Then
            ButtonModificaArea.Enabled = True
            ButtonEliminaArea.Enabled = True
            GRIDAREAS.CurrentCell = GRIDAREAS.Rows(0).Cells(0)
            DatosGridaCajaTextos()
        Else
            ButtonModificaArea.Enabled = False
            ButtonEliminaArea.Enabled = False
        End If
        '
        ' Lista de Cajas, Para Areas - Importar Areas
        '
        'CargaListaCajas(wCaja)
        '
        ControlarTabs()
        '
    End Sub

    Private Sub ControlarTabs()
        '
        ' Gestión Colección Pestañas del TabControl.
        '
        ' Determinamos si Para el NIVEL de ACCESO
        '  se indican a que pestañas se puede acceder.
        ' Si NO son "TODAS", "", -NULL-
        '  indica que existe un rango de pestañas y lo recogemos.
        '
        Dim words As String() = Nothing
        Dim Inx As Integer = 0
        '
        ' En este Punto me Aseguro de leer el Nivel y Credenciales
        '  correctas para el Vendedor Actual.
        '
        If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
            If LeeClaves(wrLeeCODNOM.NIVELACCESO) = False Then
                msg = "Error Determinando Nivel de Acceso para.: " & vbCrLf & MyFrm1.TextBoxOPC1.Text.Trim
                style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
                title = "Error en Control de Acceso."
                MsgBox(msg, style, title)
                Me.Hide()
                Exit Sub
            End If
        Else
            msg = "Error en la lectura del Vendedor.: " & vbCrLf & MyFrm1.TextBoxOPC1.Text.Trim
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error en la lectura del Vendedor."
            MsgBox(msg, style, title)
            Me.Hide()
            Exit Sub
        End If
        '
        ' Acciones Para NIVEL 5: "Programadores/TRIVALLE", "SUPER"
        ' Redefinimos las Tabs, (se accederá a TODAS)
        '
        If MyFrm5.TextBoxPwdGeneral.Text.Trim = PassTRIVALLE(0) Or
           MyFrm5.TextBoxPwdGeneral.Text.Trim = PassTRIVALLE(1) Or
           wrLeeCODNOM.NIVELACCESO = 5 Then
            '
            wrCLAVES.ACCESOREFGEN = "TODAS"
            words = Nothing
            '
            ' En este Orden !!!
            '
            TabPage1.Parent = TabControlREFGEN
            TabPage2.Parent = TabControlREFGEN
            TabPage7.Parent = TabControlREFGEN
            TabPage3.Parent = TabControlREFGEN
            TabPage4.Parent = TabControlREFGEN
            TabPage6.Parent = TabControlREFGEN
            TabPage8.Parent = TabControlREFGEN
            TabPage5.Parent = TabControlREFGEN
            Exit Sub
        End If
        '
        ' Acciones Para NIVELES 4 "EL JEFE" y 3 "ENCARGADO"
        ' Posibilidad NIVELES > 5
        ' Recogemos la lista de pestañas ACCESIBLES
        '   definida en la tabla [CLAVES].
        '
        If wrCLAVES.ACCESOREFGEN.Trim.Length > 0 Then
            If wrCLAVES.ACCESOREFGEN.Trim <> "TODAS" Then
                words = wrCLAVES.ACCESOREFGEN.Trim.Split(New Char() {"/"c})
            End If
        End If
        '
        For Each MyTab As TabPage In Me.TabControlREFGEN.TabPages
            Select Case wrLeeCODNOM.NIVELACCESO
                Case 3 To 4, Is > 5
                    '
                    ' "EL JEFE", "ENCARGADO"
                    ' Posibilidad NIVELES > 5
                    ' Pestañas en funcion del Campo CLAVES.ACCESOREFGEN
                    '
                    If wrCLAVES.ACCESOREFGEN.Trim.Length > 0 Then
                        If wrCLAVES.ACCESOREFGEN.Trim = "TODAS" Then
                            MyTab.Parent = TabControlREFGEN
                        Else
                            If Not IsNothing(words) And words.Length > 0 Then
                                '
                                MyTab.Parent = Nothing
                                If MyTab.TabIndex < words.Length Then
                                    Inx = CInt(words(MyTab.TabIndex))
                                    If MyTab.TabIndex = Inx Then
                                        MyTab.Parent = TabControlREFGEN
                                    End If
                                End If
                            End If
                        End If
                    Else
                        '
                        ' Si no hay Tabs Definidas ...
                        '
                        If MyTab.TabIndex <> 0 And MyTab.TabIndex <> 1 Then
                            MyTab.Parent = Nothing
                        End If
                    End If
            End Select
        Next
        TabControlREFGEN.Refresh()
        '
    End Sub

    Private Sub DatosLabelInfo()
        '
        ' Mostrar cierta información sobre la Aplicación.
        '
        Dim NumcajasCreadas As String = CargaListaCajas(wCaja)
        MiraMesasOcupadas()
        '
        With wrLeeTCONA4
            TEMP = "" & vbCrLf
            'getVersionByClassicalMethod

            TEMP &= "Versión Sistema Operativo: [" & Environment.OSVersion.ToString() & "], " & InfoOsVersion() & vbCrLf
            TEMP &= "Nombre de la aplicación..: " & Application.ProductName & vbCrLf
            TEMP &= "Paths de la aplicación ..: " & Application.StartupPath & vbCrLf
            TEMP &= " - Arch. Configuración ..: C:\TRIVAGES\DATOS\CONA4Cfg.txt" & vbCrLf
            TEMP &= " - DATOS ................: C:\TRIVAGES\DATOS" & vbCrLf
            TEMP &= " - Path Iconos ..........: C:\TRIVAGES\iconos" & vbCrLf
            TEMP &= " - Informes .............: C:\TRIVAGES\InformesCobview" & vbCrLf
            TEMP &= " - Arch. Temp. Impresión.: C:\TRIVAGES\DATOS\TMP_IMPRE" & vbCrLf
            TEMP &= "Caja Actual .............: " & wCaja.ToString & " " & wrLeeTCONA4.Tcona4_NOMBRECAJA.Trim & " - " & "Cajas Creadas .: " & NumcajasCreadas.Remove((NumcajasCreadas.Trim.Length - 1), 1) & vbCrLf
            TEMP &= "Empresa .................: " & .Tcona4_EMPRESA & vbCrLf
            TEMP &= "Almacen .................: " & .Tcona4_ALMACEN & vbCrLf
            If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = False Then
                wrLeeCODNOM.NOMBRE = ""
            End If
            TEMP &= "Camarero/Vendedor Actual.: " & MyFrm1.TextBoxOPC1.Text & " " & wrLeeCODNOM.NOMBRE.Trim & vbCrLf
            Select Case .Tcona4_FORMINICIAL
                Case 0
                    TEMP &= "Formulario Inicial ......: 0 - [TCONA401] - Mesas." & vbCrLf
                Case 1
                    TEMP &= "Formulario Inicial ......: 1 - [TCONA402] - Productos." & vbCrLf
            End Select
            TEMP &= "Nro. Factura Actual .....: " & .Tcona4_FACTURA & vbCrLf
            TEMP &= "Nro. Z       Actual .....: " & .Tcona4_NUMZ & vbCrLf
            TEMP &= "Nro. X       Actual .....: " & .Tcona4_NUMX & vbCrLf
            If CodsMesasOcu.Trim.Length > 0 Then
                TEMP &= "Mesas Ocupadas ..........: [" & NumMesasOcu.ToString.Trim & "]-> " & CodsMesasOcu.Remove((CodsMesasOcu.Trim.Length - 1), 1) & vbCrLf
                TEMP &= "Mesas Ocupadas IMPORTE...: " & TotImpoOCU.ToString.Trim.Replace(",", ".") & "€" & vbCrLf
            Else
                TEMP &= "Mesas Ocupadas ..........: [" & NumMesasOcu.ToString.Trim & "]-> -Todas Libres-" & vbCrLf
            End If
            '
            ' Impresora Predeterminada y estado ...
            '
            Dim ImpDef As String = ObtenerImpresoraPredeterminada()
            TEMP &= "Impresora Predet. Windows: " & ImpDef & " "
            If ImpresoraEstaONLINE(ImpDef) = True Then
                TEMP &= "Estado .: Encendida, Trabajos Pendientes.: " & wrProp_IMPRESORA.JobCountSinceLastReset & vbCrLf
            Else
                TEMP &= "Estado .: Apagada, Trabajos Pendientes.: " & wrProp_IMPRESORA.JobCountSinceLastReset & vbCrLf
            End If
            '
            If .Tcona4_MODIMPREFIJO.Trim.Length > 0 Then
                TEMP &= "MODELO Impresora Fijado .: " & .Tcona4_MODIMPREFIJO & " en " & .Tcona4_TKFACPUERTO & vbCrLf
            Else
                TEMP &= "MODELO Impresora Fijado .: Sin Fijar, No hay Modelo definido para la caja actual." & vbCrLf
            End If
            TEMP &= "Instancia(s) SQL ........: " & SQL_CadenaConexion & vbCrLf
            TEMP &= "                           " & SQL_CadenaConexion1 & vbCrLf
        End With
        LabelInformacionAPP.Text = TEMP
        '
    End Sub

    Private Sub CargaOtrosDatos()
        '
        ' Otros Valores Referenciales
        '
        With wrLeeTCONA4
            TextBoxNombreCaja.Text = .Tcona4_NOMBRECAJA
            TextBoxEmpresa.Text = .Tcona4_EMPRESA.ToString
            TextBoxNumFactura.Text = .Tcona4_FACTURA.ToString
            TextBoxAlmacen.Text = .Tcona4_ALMACEN.ToString
            TextBoxFRMInicial.Text = .Tcona4_FORMINICIAL.ToString
            TextBoxRacionFAM1.Text = .Tcona4_VARIOSFAM1
            TextBoxRacionFAM2.Text = .Tcona4_VARIOSFAM2
            TextBoxRacionFAM3.Text = .Tcona4_VARIOSFAM3
            TextBoxNumX.Text = .Tcona4_NUMX.ToString
            TextBoxNumZ.Text = .Tcona4_NUMZ.ToString
            TextBoxTipoPvp.Text = .Tcona4_TIPOPVPARTI.ToString
            TextBoxFechaZ.Text = .Tcona4_FECHAZ
            TextBoxFechaDIA.Text = .Tcona4_FECHADIASESION
            TextBoxRetardoSplash.Text = .Tcona4_SPLASHRETARDO.ToString
            '
            If .Tcona4_ORDENART = "True" Then
                CheckBoxOrdenArt.Checked = True
            Else
                CheckBoxOrdenArt.Checked = False
            End If
            If .Tcona4_ORDENFAM = "True" Then
                CheckBoxOrdenFam.Checked = True
            Else
                CheckBoxOrdenFam.Checked = False
            End If
            '
            ' Orden Productos en la MESA (GRID1)
            '
            If .Tcona4_ORDENPRODUCTOS = "True" Then
                CheckBoxOrdenProdu.Checked = True
            Else
                CheckBoxOrdenProdu.Checked = False
            End If
            If .Tcona4_PIDEVENDEDOR = "True" Then
                CheckBoxPVendedores.Checked = True
            Else
                CheckBoxPVendedores.Checked = False
            End If
            If .Tcona4_SEPARARACIONES = "True" Then
                CheckBoxSeparaRacion.Checked = True
            Else
                CheckBoxSeparaRacion.Checked = False
            End If
            If .Tcona4_REFRESCABOTONES = "True" Then
                CheckBoxBotoneraSN.Checked = True
            Else
                CheckBoxBotoneraSN.Checked = False
            End If
            If .Tcona4_ZETAMESASOCU = "True" Then
                CheckBoxZetaOcu.Checked = True
            Else
                CheckBoxZetaOcu.Checked = False
            End If
            If .Tcona4_SPLASHSCREEN = "True" Then
                CheckBoxSplahScreen.Checked = True
            Else
                CheckBoxSplahScreen.Checked = False
            End If
            If .Tcona4_PIDEPAX = "True" Then
                CheckBoxPidePAX.Checked = True
            Else
                CheckBoxPidePAX.Checked = False
            End If
            '
            ' Favoritos
            '
            If .Tcona4_CARGAFAVORITOS = "True" Then
                CheckBoxCargarFav.Checked = True
                GroupBoxFAV.Enabled = True
            Else
                CheckBoxCargarFav.Checked = False
                GroupBoxFAV.Enabled = False
            End If
            Select Case .Tcona4_BOTONFAVORITO
                Case "BEBIDAS"
                    RadioButtonBEBIDAS.Checked = True
                    RadioButtonCOMIDAS.Checked = False
                Case "COMIDAS"
                    RadioButtonBEBIDAS.Checked = False
                    RadioButtonCOMIDAS.Checked = True
            End Select
            '
            ' Nombre Tarifas
            '
            TextBoxTPVP1.Text = .Tcona4_NOMTARIPVP1
            TextBoxTPVP2.Text = .Tcona4_NOMTARIPVP2
            TextBoxTPVP5.Text = .Tcona4_NOMTARIPVP5
            TextBoxTPVP6.Text = .Tcona4_NOMTARIPVP6
            TextBoxTPVP7.Text = .Tcona4_NOMTARIPVP7
            TextBoxTPVP8.Text = .Tcona4_NOMTARIPVP8
            TextBoxTPVP9.Text = .Tcona4_NOMTARIPVP9
            '
            ' MODELO de IMPRESORA de Trabajo
            '
            LabelMODELOFijado.Text = .Tcona4_MODIMPREFIJO
            '
            ' Imprime TK FACTURA s/n
            '
            If .Tcona4_IMPRIMETKFAC = "True" Then
                CheckBoxImpreFACTURA.Checked = True
                CheckBoxCOBVIEWPDSN.Enabled = True
                TextBoxImpoMimImpreTKFAC.Enabled = True
            Else
                CheckBoxImpreFACTURA.Checked = False
                CheckBoxCOBVIEWPDSN.Enabled = False
                TextBoxImpoMimImpreTKFAC.Enabled = False
            End If
            '
            ' Impo. Mínimo Impresion TK FACTURA
            '
            TextBoxImpoMimImpreTKFAC.Text = .Tcona4_IMPOMINIMPRE.ToString
            '
            ' Salto Lineas TK AREAS
            '
            TextBoxNumSaltoLineas.Text = .Tcona4_SALTOLINPIETK.ToString
            '
            ' Abre Cajón cuando Imprime TK FACTURA s/n
            '
            If .Tcona4_TKFACABRECAJON = "True" Then
                CheckBoxImpreFACCajon.Checked = True
            Else
                CheckBoxImpreFACCajon.Checked = False
            End If
            '
            ' Puerto TK FACTURAS
            '
            TextBoxTKFACPtoCaptura.Text = .Tcona4_TKFACPUERTO
            '
            ' TK FACTURA Detalle Combinados s/n
            '
            If .Tcona4_TKFACIMPDETCOMBI = "True" Then
                CheckBoxTKFACDetCombi.Checked = True
            Else
                CheckBoxTKFACDetCombi.Checked = False
            End If
            '
            ' Comprobar Impresora al Iniciar la App s/n
            '
            If .Tcona4_COMPRUIMPREINI = "True" Then
                CheckBoxMiraImpreIni.Checked = True
            Else
                CheckBoxMiraImpreIni.Checked = False
            End If
            '
            ' Orden Productos en la MESA (GRID1)
            '
            If .Tcona4_PIDECAJAINICIO = "True" Then
                CheckBoxPCajaInicial.Checked = True
            Else
                CheckBoxPCajaInicial.Checked = False
            End If
            '
            ' Pide Confirmacion Borrado Lineas Cuenta Mesa?
            '
            If .Tcona4_BORLINCUENTA = "True" Then
                CheckBoxBorLinCtaMesa.Checked = True
            Else
                CheckBoxBorLinCtaMesa.Checked = False
            End If
            '
            ' Crear Clientes Crédito?
            '
            If .Tcona4_CREACLICREDITO = "True" Then
                CheckBoxCreaCliConta.Checked = True
            Else
                CheckBoxCreaCliConta.Checked = False
            End If
            '
            ' Efecto FADEOUT al salir?
            '
            If .Tcona4_FADEOUTSALIR = "True" Then
                CheckBoxFADEOUT.Checked = True
            Else
                CheckBoxFADEOUT.Checked = False
            End If
        End With
        '
    End Sub

    Private Sub LimpiaCajasTexto()
        With Me
            '
            ' TICKETs
            '
            .TextBoxDetCab1.Text = ""
            .TextBoxDetCab2.Text = ""
            .TextBoxDetCab3.Text = ""
            .TextBoxDetCab4.Text = ""
            .TextBoxDetCab5.Text = ""
            .TextBoxDetCab6.Text = ""
            '
            .TextBoxIGICTKFAC.Text = ""
            .TextBoxLinCab1.Text = ""
            .TextBoxLinCab2.Text = ""
            .TextBoxLinCab3.Text = ""
            .TextBoxLinCab4.Text = ""
            .TextBoxLinCab5.Text = ""
            .TextBoxLinCab6.Text = ""
            .TextBoxLinCab7.Text = ""
            .TextBoxLinCab8.Text = ""
            .TextBoxLinCab9.Text = ""
            .TextBoxLinCab10.Text = ""
            .TextBoxLinPie1.Text = ""
            .TextBoxLinPie2.Text = ""
            .TextBoxLinPie3.Text = ""
            .TextBoxLinPie4.Text = ""
            .TextBoxLinPie5.Text = ""
            .TextBoxLinPie6.Text = ""
            .TextBoxLinPie7.Text = ""
            .TextBoxLinPie8.Text = ""
            .TextBoxLinPie9.Text = ""
            .TextBoxLinPie10.Text = ""
            .TextBoxLinPie11.Text = ""
            .TextBoxLinPie12.Text = ""
            .TextBoxLinPie13.Text = ""
            .TextBoxLinPie14.Text = ""
            .TextBoxLinPie15.Text = ""
            .TextBoxLinPie16.Text = ""
            .TextBoxLinPie17.Text = ""
            .TextBoxLinPie18.Text = ""
            .TextBoxLinPie19.Text = ""
            .TextBoxLinPie20.Text = ""
            .TextBoxImpoMimImpreTKFAC.Text = ""
            .TextBoxTKFACPtoCaptura.Text = ""
            .TextBoxNumSaltoLineas.Text = ""

            '
            ' Otros Valores Referenciales
            '
            .TextBoxNombreCaja.Text = ""
            .TextBoxEmpresa.Text = ""
            .TextBoxNumFactura.Text = ""
            .TextBoxAlmacen.Text = ""
            .TextBoxFRMInicial.Text = ""
            .TextBoxRacionFAM1.Text = ""
            .TextBoxRacionFAM2.Text = ""
            .TextBoxRacionFAM3.Text = ""
            .TextBoxNumX.Text = ""
            .TextBoxNumZ.Text = ""
            .TextBoxTipoPvp.Text = ""
            .TextBoxFechaZ.Text = ""
            .TextBoxFechaDIA.Text = ""
            .TextBoxTPVP1.Text = ""
            .TextBoxTPVP2.Text = ""
            .TextBoxTPVP5.Text = ""
            .TextBoxTPVP6.Text = ""
            .TextBoxTPVP7.Text = ""
            .TextBoxTPVP8.Text = ""
            .TextBoxTPVP9.Text = ""
            .TextBoxRetardoSplash.Text = ""
            '
            .CheckBoxOrdenFam.Checked = False
            .CheckBoxOrdenArt.Checked = False
            .CheckBoxOrdenProdu.Checked = False
            .CheckBoxPVendedores.Checked = False
            .CheckBoxSeparaRacion.Checked = False
            .CheckBoxBotoneraSN.Checked = False
            .CheckBoxZetaOcu.Checked = False
            .CheckBoxSplahScreen.Checked = False
            .CheckBoxPidePAX.Checked = False
            .CheckBoxCargarFav.Checked = False
            .CheckBoxPCajaInicial.Checked = False
            '
            .CheckBoxLogoTKF.Checked = False
            .CheckBoxImpreTKFACDirWin.Checked = False
            .CheckBoxImpreFACTURA.Checked = False
            .CheckBoxCOBVIEWPDSN.Checked = False
            .CheckBoxTKFACDetCombi.Checked = False
            .CheckBoxImpreFACCajon.Checked = False
            .CheckBoxLogoTKXZ.Checked = False
            '
        End With
    End Sub

    Private Sub CargaLineasTicket()
        '
        ' Lineas Pie y Cabecera TICKET
        '
        With wrLeeTCONA4
            ' Detalle FAC
            TextBoxDetCab1.Text = .Tcona4_TKFDETLIN1
            TextBoxDetCab2.Text = .Tcona4_TKFDETLIN2
            TextBoxDetCab3.Text = .Tcona4_TKFDETLIN3
            ' Detalle X,Z
            TextBoxDetCab4.Text = .Tcona4_TKXZDETLIN1
            TextBoxDetCab5.Text = .Tcona4_TKXZDETLIN2
            TextBoxDetCab6.Text = .Tcona4_TKXZDETLIN3
            ' Cab
            TextBoxLinCab1.Text = .Tcona4_TKFCABLI1
            TextBoxLinCab2.Text = .Tcona4_TKFCABLI2
            TextBoxLinCab3.Text = .Tcona4_TKFCABLI3
            TextBoxLinCab4.Text = .Tcona4_TKFCABLI4
            TextBoxLinCab5.Text = .Tcona4_TKFCABLI5
            TextBoxLinCab6.Text = .Tcona4_TKFCABLI6
            TextBoxLinCab7.Text = .Tcona4_TKFCABLI7
            TextBoxLinCab8.Text = .Tcona4_TKFCABLI8
            TextBoxLinCab9.Text = .Tcona4_TKFCABLI9
            TextBoxLinCab10.Text = .Tcona4_TKFCABLI10
            ' Pie
            TextBoxLinPie1.Text = .Tcona4_TKFPIELI1
            TextBoxLinPie2.Text = .Tcona4_TKFPIELI2
            TextBoxLinPie3.Text = .Tcona4_TKFPIELI3
            TextBoxLinPie4.Text = .Tcona4_TKFPIELI4
            TextBoxLinPie5.Text = .Tcona4_TKFPIELI5
            TextBoxLinPie6.Text = .Tcona4_TKFPIELI6
            TextBoxLinPie7.Text = .Tcona4_TKFPIELI7
            TextBoxLinPie8.Text = .Tcona4_TKFPIELI8
            TextBoxLinPie9.Text = .Tcona4_TKFPIELI9
            TextBoxLinPie10.Text = .Tcona4_TKFPIELI10
            TextBoxLinPie11.Text = .Tcona4_TKFPIELI11
            TextBoxLinPie12.Text = .Tcona4_TKFPIELI12
            TextBoxLinPie13.Text = .Tcona4_TKFPIELI13
            TextBoxLinPie14.Text = .Tcona4_TKFPIELI14
            TextBoxLinPie15.Text = .Tcona4_TKFPIELI15
            TextBoxLinPie16.Text = .Tcona4_TKFPIELI16
            TextBoxLinPie17.Text = .Tcona4_TKFPIELI17
            TextBoxLinPie18.Text = .Tcona4_TKFPIELI18
            TextBoxLinPie19.Text = .Tcona4_TKFPIELI19
            TextBoxLinPie20.Text = .Tcona4_TKFPIELI20
            '
            ' Logo S/N, Factura 
            '
            If .Tcona4_TKFACLOGO = "True" Then
                CheckBoxLogoTKF.Checked = True
            Else
                CheckBoxLogoTKF.Checked = False
            End If
            '
            ' % IGIC
            '
            TextBoxIGICTKFAC.Text = .Tcona4_TKFACIGIC.ToString
            '
            ' Logo S/N, X/Z
            '
            If .Tcona4_TKZETALOGO = "True" Then
                CheckBoxLogoTKXZ.Checked = True
            Else
                CheckBoxLogoTKXZ.Checked = False
            End If
            '
            ' COBVIEW Previsualiza S/N
            '
            If .Tcona4_COBVIEWPDSN = "True" Then
                CheckBoxCOBVIEWPDSN.Checked = True
            Else
                CheckBoxCOBVIEWPDSN.Checked = False
            End If
            '
            ' Tipo Impresion TK. FAC
            ' 0 Windows / 1 Directa
            '
            If .Tcona4_TKFACDIRWIN = "True" Then
                CheckBoxImpreTKFACDirWin.Checked = True
            Else
                CheckBoxImpreTKFACDirWin.Checked = False
            End If
        End With
        '
    End Sub

    Private Sub CargaGridColores()
        '
        ' Cargamos Lista de Colores Definibles Por el Usuario
        '
        Dim row1 As String() = New String() {" ", " "}
        '
        ' Botón en GRID, para Seleccionar Colores.
        '
        GRIDColores.Rows.Clear()
        '
        ' Líneas deL GRID
        '-----------------
        '   Colores (wrLeeTCONA4)
        '     .Tcona4_COLORFTCONA401
        '     .Tcona4_COLORFTCONA402
        '     .Tcona4_COLORFF
        '     .Tcona4_COLORLF
        '     .Tcona4_COLORFA
        '     .Tcona4_COLORLA
        '
        Dim i As Integer = 0
        With GRIDColores
            row1 = New String() {"Color Fondo FORMULARIO MESAS", " "}
            .Rows.Add(row1) : Poncolores(i, wrLeeTCONA4.Tcona4_COLORFTCONA401) : i += 1
            row1 = New String() {"Color Fondo FORMULARIO PRODUCTOS", " "}
            .Rows.Add(row1) : Poncolores(i, wrLeeTCONA4.Tcona4_COLORFTCONA402) : i += 1
            row1 = New String() {"Color Fondo Botones FAMILIAS", " "}
            .Rows.Add(row1) : Poncolores(i, wrLeeTCONA4.Tcona4_COLORFF) : i += 1
            row1 = New String() {"Color Letra Botones FAMILIAS", " "}
            .Rows.Add(row1) : Poncolores(i, wrLeeTCONA4.Tcona4_COLORLF) : i += 1
            row1 = New String() {"Color Fondo Botones PRODUCTOS", " "}
            .Rows.Add(row1) : Poncolores(i, wrLeeTCONA4.Tcona4_COLORFA) : i += 1
            row1 = New String() {"Color Letra Botones PRODUCTOS", " "}
            .Rows.Add(row1) : Poncolores(i, wrLeeTCONA4.Tcona4_COLORLA) : i += 1
        End With
        '
        ' Muestra de FORMULARIOS Con Colores Aplicados.
        '
        '    MESAS - Fondo
        '
        PanelMuestraMESAS.BackColor = wrLeeTCONA4.Tcona4_COLORFTCONA401
        '
        '    Productos - Fondo
        '
        PanelMuestraProductos.BackColor = wrLeeTCONA4.Tcona4_COLORFTCONA402
        '
        ' Buttons Familias
        '
        ButtonFAM1m.BackColor = wrLeeTCONA4.Tcona4_COLORFF
        ButtonFAM1m.ForeColor = wrLeeTCONA4.Tcona4_COLORLF
        ButtonFAM2m.BackColor = wrLeeTCONA4.Tcona4_COLORFF
        ButtonFAM2m.ForeColor = wrLeeTCONA4.Tcona4_COLORLF
        ButtonFAM3m.BackColor = wrLeeTCONA4.Tcona4_COLORFF
        ButtonFAM3m.ForeColor = wrLeeTCONA4.Tcona4_COLORLF
        ButtonFAM4m.BackColor = wrLeeTCONA4.Tcona4_COLORFF
        ButtonFAM4m.ForeColor = wrLeeTCONA4.Tcona4_COLORLF
        ButtonFAM5m.BackColor = wrLeeTCONA4.Tcona4_COLORFF
        ButtonFAM5m.ForeColor = wrLeeTCONA4.Tcona4_COLORLF
        ButtonFAM6m.BackColor = wrLeeTCONA4.Tcona4_COLORFF
        ButtonFAM6m.ForeColor = wrLeeTCONA4.Tcona4_COLORLF
        '
        ' Buttons Productos
        '
        ButtonART1m.BackColor = wrLeeTCONA4.Tcona4_COLORFA
        ButtonART1m.ForeColor = wrLeeTCONA4.Tcona4_COLORLA
        ButtonART2m.BackColor = wrLeeTCONA4.Tcona4_COLORFA
        ButtonART2m.ForeColor = wrLeeTCONA4.Tcona4_COLORLA
        ButtonART4m.BackColor = wrLeeTCONA4.Tcona4_COLORFA
        ButtonART4m.ForeColor = wrLeeTCONA4.Tcona4_COLORLA
        ButtonART5m.BackColor = wrLeeTCONA4.Tcona4_COLORFA
        ButtonART5m.ForeColor = wrLeeTCONA4.Tcona4_COLORLA
        ButtonART7m.BackColor = wrLeeTCONA4.Tcona4_COLORFA
        ButtonART7m.ForeColor = wrLeeTCONA4.Tcona4_COLORLA
        ButtonART8m.BackColor = wrLeeTCONA4.Tcona4_COLORFA
        ButtonART8m.ForeColor = wrLeeTCONA4.Tcona4_COLORLA
        '
    End Sub

    Private Sub Poncolores(i As Integer, MyColor As Color)
        '
        ' Establecer Color al fondo de las celdas indicadas....
        '
        For ColNo As Integer = 1 To 2
            If Not GRIDColores.Rows(i).Cells(ColNo).Value Is DBNull.Value Then
                GRIDColores.Rows(i).Cells(ColNo).Style.BackColor = MyColor
                GRIDColores.Rows(i).Height = 30
            End If
        Next
        '
    End Sub

    Private Sub GRIDColores_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDColores.CellClick
        '
        ' Al hacer Clik en los botones del GRID...
        '
        If e.ColumnIndex = 2 And e.RowIndex > -1 Then
            SelecColor(e.RowIndex)
        End If
        '
    End Sub

    Private Sub SelecColor(Btn As Integer)
        '
        '   Seleccion de colores
        '
        With ColorDialogREFGEN
            .Color = GRIDColores.Rows(Btn).Cells(2).Style.BackColor
            If (.ShowDialog() = DialogResult.OK) Then
                GRIDColores.Rows(Btn).Cells(2).Style.BackColor = .Color
                CambioColores = True
                GRIDColores.ClearSelection()
                '
                ' Muestra de FORMULARIOS Con Colores Aplicados.
                '
                Select Case Btn
                    Case 0 ' Fondo Mesas
                        PanelMuestraMESAS.BackColor = .Color
                    Case 1 ' Fondo Productos
                        PanelMuestraProductos.BackColor = .Color
                    Case 2 ' Fondo Bot. Fam.
                        ButtonFAM1m.BackColor = .Color
                        ButtonFAM2m.BackColor = .Color
                        ButtonFAM3m.BackColor = .Color
                        ButtonFAM4m.BackColor = .Color
                        ButtonFAM5m.BackColor = .Color
                        ButtonFAM6m.BackColor = .Color
                    Case 3 ' Letra Bot. Fam.    
                        ButtonFAM1m.ForeColor = .Color
                        ButtonFAM2m.ForeColor = .Color
                        ButtonFAM3m.ForeColor = .Color
                        ButtonFAM4m.ForeColor = .Color
                        ButtonFAM5m.ForeColor = .Color
                        ButtonFAM6m.ForeColor = .Color
                    Case 4 ' Fondo Bot. Prod.   
                        ButtonART1m.BackColor = .Color
                        ButtonART2m.BackColor = .Color
                        ButtonART4m.BackColor = .Color
                        ButtonART5m.BackColor = .Color
                        ButtonART7m.BackColor = .Color
                        ButtonART8m.BackColor = .Color
                    Case 5 ' Letra Bot. Prod.   
                        ButtonART1m.ForeColor = .Color
                        ButtonART2m.ForeColor = .Color
                        ButtonART4m.ForeColor = .Color
                        ButtonART5m.ForeColor = .Color
                        ButtonART7m.ForeColor = .Color
                        ButtonART8m.ForeColor = .Color
                End Select
            End If
        End With
    End Sub

    Private Sub TextBoxIGICTKFAC_KeyPress(sender As Object, e As KeyPressEventArgs)
        '
        ' % IGIC
        '
        Dim allowedChars As String = "0123456789.," & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxLinPie1.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxAlmacen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxAlmacen.KeyPress
        '
        ' Cod. Almacen
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxFRMInicial.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxEmpresa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxEmpresa.KeyPress
        '
        ' Cod. Empresa
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxNumFactura.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxNumFactura_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxNumFactura.KeyPress
        '
        ' Num. Factura (Contador Factura)
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxAlmacen.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxFRMInicial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxFRMInicial.KeyPress
        '
        ' Formulario Inicial
        '
        Dim allowedChars As String = "01" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxRacionFAM1.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxNumX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxNumX.KeyPress
        '
        ' Num. X (Contador X)
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxNumZ.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxNumZ_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxNumZ.KeyPress
        '
        ' Num. Z (Contador Z)
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxTipoPvp.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxRacionFAM1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxRacionFAM1.KeyDown
        '
        ' Cod. Familia 1 (ALFANUMERICO)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxFRMInicial.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxRacionFAM2.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxRacionFAM2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxRacionFAM2.KeyDown
        '
        ' Cod. Familia 2 (ALFANUMERICO)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxRacionFAM1.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxRacionFAM3.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxRacionFAM3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxRacionFAM3.KeyDown
        '
        ' Cod. Familia 3 (ALFANUMERICO)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxRacionFAM2.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxNumX.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxNumFactura_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNumFactura.KeyDown
        '
        ' Num. Factura
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxEmpresa.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxAlmacen.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxEmpresa_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxEmpresa.KeyDown
        '
        ' Cod. Empresa
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxNombreCaja.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxNumFactura.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxAlmacen.KeyDown
        '
        ' Cod. Almacen
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxNumFactura.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxFRMInicial.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxFRMInicial_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxFRMInicial.KeyDown
        '
        ' FORM. Inicial
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxAlmacen.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxRacionFAM1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxNumX_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNumX.KeyDown
        '
        ' Num. X
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxRacionFAM3.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxNumZ.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxNumZ_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNumZ.KeyDown
        '
        ' Num. Z
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxNumX.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTipoPvp.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxEmpresa_GotFocus(sender As Object, e As EventArgs) Handles TextBoxEmpresa.GotFocus
        '
        With TextBoxEmpresa
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxEmpresa_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEmpresa.LostFocus
        TextBoxEmpresa.BackColor = Color.White
    End Sub

    Private Sub TextBoxNumFactura_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNumFactura.GotFocus
        '
        With TextBoxNumFactura
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxNumFactura_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNumFactura.LostFocus
        TextBoxNumFactura.BackColor = Color.White
    End Sub

    Private Sub TextBoxAlmacen_GotFocus(sender As Object, e As EventArgs) Handles TextBoxAlmacen.GotFocus
        '
        With TextBoxAlmacen
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxFRMInicial_GotFocus(sender As Object, e As EventArgs) Handles TextBoxFRMInicial.GotFocus
        '
        With TextBoxFRMInicial
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxRacionFAM1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxRacionFAM1.GotFocus
        '
        With TextBoxRacionFAM1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxRacionFAM2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxRacionFAM2.GotFocus
        '
        With TextBoxRacionFAM2
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxRacionFAM3_GotFocus(sender As Object, e As EventArgs) Handles TextBoxRacionFAM3.GotFocus
        '
        With TextBoxRacionFAM3
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxNumX_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNumX.GotFocus
        '
        With TextBoxNumX
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxNumZ_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNumZ.GotFocus
        '
        With TextBoxNumZ
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxAlmacen_LostFocus(sender As Object, e As EventArgs) Handles TextBoxAlmacen.LostFocus
        TextBoxAlmacen.BackColor = Color.White
    End Sub

    Private Sub TextBoxFRMInicial_LostFocus(sender As Object, e As EventArgs) Handles TextBoxFRMInicial.LostFocus
        TextBoxFRMInicial.BackColor = Color.White
    End Sub

    Private Sub TextBoxRacionFAM1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxRacionFAM1.LostFocus
        TextBoxRacionFAM1.BackColor = Color.White
    End Sub

    Private Sub TextBoxRacionFAM2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxRacionFAM2.LostFocus
        TextBoxRacionFAM2.BackColor = Color.White
    End Sub

    Private Sub TextBoxRacionFAM3_LostFocus(sender As Object, e As EventArgs) Handles TextBoxRacionFAM3.LostFocus
        TextBoxRacionFAM3.BackColor = Color.White
    End Sub

    Private Sub TextBoxNumX_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNumX.LostFocus
        TextBoxNumX.BackColor = Color.White
    End Sub

    Private Sub TextBoxNumZ_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNumZ.LostFocus
        TextBoxNumZ.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab1.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie20.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab2.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab2.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab1.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab3.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab3.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab2.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab4.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab4.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab3.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab5.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab5.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab4.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab6.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab6.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab5.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab7.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab7.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab6.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab8.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab8.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab7.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab9.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab9_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab9.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab8.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab10.Focus()
        End Select
        e = Nothing
        '
    End Sub
    Private Sub TextBoxLinCab10_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinCab10.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab9.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxDetCab1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinCab1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab1.GotFocus
        '
        With TextBoxLinCab1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab2.GotFocus
        '
        With TextBoxLinCab2
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab3_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab3.GotFocus
        '
        With TextBoxLinCab3
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab4_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab4.GotFocus
        '
        With TextBoxLinCab4
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab5_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab5.GotFocus
        '
        With TextBoxLinCab5
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab6_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab6.GotFocus
        '
        With TextBoxLinCab6
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab7_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab7.GotFocus
        '
        With TextBoxLinCab7
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab8_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab8.GotFocus
        '
        With TextBoxLinCab8
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab9_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab9.GotFocus
        '
        With TextBoxLinCab9
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab10_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab10.GotFocus
        '
        With TextBoxLinCab10
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinCab1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab1.LostFocus
        TextBoxLinCab1.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab2.LostFocus
        TextBoxLinCab2.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab3_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab3.LostFocus
        TextBoxLinCab3.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab4_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab4.LostFocus
        TextBoxLinCab4.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab5_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab5.LostFocus
        TextBoxLinCab5.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab6_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab6.LostFocus
        TextBoxLinCab6.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab7_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab7.LostFocus
        TextBoxLinCab7.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab8_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab8.LostFocus
        TextBoxLinCab8.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab9_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab9.LostFocus
        TextBoxLinCab9.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinCab10_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinCab10.LostFocus
        TextBoxLinCab10.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie1.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxDetCab6.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie2.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie2.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie1.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie3.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie3.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie2.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie4.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie4.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie3.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie5.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie5.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie4.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie6.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie6.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie5.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie7.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie7.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie6.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie8.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie8.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie7.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie9.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie9_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie9.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie8.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie10.Focus()
        End Select
        e = Nothing
        '
    End Sub
    Private Sub TextBoxLinPie10_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie10.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie9.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie11.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie1.GotFocus
        '
        With TextBoxLinPie1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie2.GotFocus
        '
        With TextBoxLinPie2
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie3_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie3.GotFocus
        '
        With TextBoxLinPie3
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie4_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie4.GotFocus
        '
        With TextBoxLinPie4
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie5_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie5.GotFocus
        '
        With TextBoxLinPie5
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie6_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie6.GotFocus
        '
        With TextBoxLinPie6
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie7_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie7.GotFocus
        '
        With TextBoxLinPie7
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie8_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie8.GotFocus
        '
        With TextBoxLinPie8
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie9_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie9.GotFocus
        '
        With TextBoxLinPie9
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie10_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie10.GotFocus
        '
        With TextBoxLinPie10
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie1.LostFocus
        TextBoxLinPie1.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie2.LostFocus
        TextBoxLinPie2.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie3_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie3.LostFocus
        TextBoxLinPie3.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie4_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie4.LostFocus
        TextBoxLinPie4.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie5_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie5.LostFocus
        TextBoxLinPie5.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie6_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie6.LostFocus
        TextBoxLinPie6.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie7_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie7.LostFocus
        TextBoxLinPie7.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie8_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie8.LostFocus
        TextBoxLinPie8.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie9_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie9.LostFocus
        TextBoxLinPie9.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie10_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie10.LostFocus
        TextBoxLinPie10.BackColor = Color.White
    End Sub





    Private Sub TextBoxLinPie11_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie11.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie10.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie12.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie12_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie12.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie11.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie13.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie13_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie13.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie12.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie14.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie14_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie14.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie13.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie15.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie15_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie15.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie14.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie16.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie16_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie16.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie15.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie17.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie17_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie17.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie16.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie18.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie18_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie18.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie17.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie19.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie19_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie19.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie18.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie20.Focus()
        End Select
        e = Nothing
        '
    End Sub
    Private Sub TextBoxLinPie20_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxLinPie20.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinPie19.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinCab1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxLinPie11_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie11.GotFocus
        '
        With TextBoxLinPie11
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie12_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie12.GotFocus
        '
        With TextBoxLinPie12
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie13_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie13.GotFocus
        '
        With TextBoxLinPie13
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie14_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie14.GotFocus
        '
        With TextBoxLinPie14
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie15_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie15.GotFocus
        '
        With TextBoxLinPie15
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie16_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie16.GotFocus
        '
        With TextBoxLinPie16
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie17_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie17.GotFocus
        '
        With TextBoxLinPie17
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie18_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie18.GotFocus
        '
        With TextBoxLinPie18
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie19_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie19.GotFocus
        '
        With TextBoxLinPie19
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie20_GotFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie20.GotFocus
        '
        With TextBoxLinPie20
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxLinPie11_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie11.LostFocus
        TextBoxLinPie11.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie12_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie12.LostFocus
        TextBoxLinPie12.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie13_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie13.LostFocus
        TextBoxLinPie13.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie14_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie14.LostFocus
        TextBoxLinPie14.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie15_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie15.LostFocus
        TextBoxLinPie15.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie16_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie16.LostFocus
        TextBoxLinPie16.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie17_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie17.LostFocus
        TextBoxLinPie17.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie18_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie18.LostFocus
        TextBoxLinPie18.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie19_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie19.LostFocus
        TextBoxLinPie19.BackColor = Color.White
    End Sub

    Private Sub TextBoxLinPie20_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLinPie20.LostFocus
        TextBoxLinPie20.BackColor = Color.White
    End Sub

    Private Sub TextBoxIGICTKFAC_KeyDown(sender As Object, e As KeyEventArgs)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab10.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxImpoMimImpreTKFAC.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxIGICTKFAC_LostFocus(sender As Object, e As EventArgs)
        TextBoxIGICTKFAC.BackColor = Color.White
    End Sub

    Private Sub TextBoxIGICTKFAC_GotFocus(sender As Object, e As EventArgs)
        '
        With TextBoxIGICTKFAC
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTipoPvp_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTipoPvp.KeyDown
        '
        ' Tipo PVP 1 a 9
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxNumZ.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTipoPvp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxTipoPvp.KeyPress
        '
        ' Tipo PVP 1 a 9
        '
        Dim allowedChars As String = "01256789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    CheckBoxOrdenFam.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxTipoPvp_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTipoPvp.GotFocus
        '
        With TextBoxTipoPvp
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTipoPvp_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTipoPvp.LostFocus
        TextBoxTipoPvp.BackColor = Color.White
    End Sub

    Private Sub ButtonRFExit_Click(sender As Object, e As EventArgs) Handles ButtonRFExit.Click
        '
        ' (1) Actualizamos Ref. Generales.
        '
        ' Validación Retardo < 5 Segundos / 2,5s por defecto...
        '
        If IsNumeric(TextBoxRetardoSplash.Text.Trim) Then
            If CInt(TextBoxRetardoSplash.Text.Trim) > 5000 Then
                TextBoxRetardoSplash.Text = "5000"
            End If
        Else
            TextBoxRetardoSplash.Text = "2500"
        End If
        '
        Actualiza_TCONA4(wCaja, "RefGen")
        '
        ' (2) Leemos de nuevo los datos de Referencia "General".
        '     Esto "actualiza" el comportamiento de aplicación
        '      con los NUEVOS datos de ref. Gen. 
        '
        LeeTCONA4Cfg("General")
        '
        Me.Hide()
        TCONA405_Started = False
        '
    End Sub

    Private Sub GRIDAREAS_Click(sender As Object, e As EventArgs) Handles GRIDAREAS.Click
        '
        If GRIDAREAS.SelectedRows.Count > 0 Then
            DatosGridaCajaTextos()
        End If
        '
    End Sub

    Private Sub DatosGridaCajaTextos()
        '
        TextBoxCodArea.Text = GRIDAREAS.SelectedCells(0).Value.ToString
        TextBoxNomArea.Text = GRIDAREAS.SelectedCells(1).Value.ToString
        TextBoxPtoImpre.Text = GRIDAREAS.SelectedCells(2).Value.ToString
        TextBoxArea2.Text = GRIDAREAS.SelectedCells(3).Value.ToString
        TextBoxArea3.Text = GRIDAREAS.SelectedCells(4).Value.ToString
        TextBoxArea4.Text = GRIDAREAS.SelectedCells(5).Value.ToString
        TextBoxTipo.Text = GRIDAREAS.SelectedCells(6).Value.ToString
        If GRIDAREAS.SelectedCells(7).Value.ToString = "True" Then
            CheckBoxTImpre.Checked = True
        Else
            CheckBoxTImpre.Checked = False
        End If
        ComboBoxModImpreAREAS.Text = GRIDAREAS.SelectedCells(8).Value.ToString
        '
    End Sub

    Private Sub ButtonNuevaArea_Click(sender As Object, e As EventArgs) Handles ButtonNuevaArea.Click
        '
        ' Nuevo Registro Areas
        '
        BtnAccion = 1
        TextBoxCodArea.Text = ""
        TextBoxNomArea.Text = ""
        TextBoxPtoImpre.Text = ""
        TextBoxArea2.Text = ""
        TextBoxArea3.Text = ""
        TextBoxArea4.Text = ""
        TextBoxTipo.Text = ""
        ComboBoxModImpreAREAS.Text = ""
        CheckBoxTImpre.Checked = False
        CheckBoxTImpre.Enabled = True
        '
        TextBoxCodArea.ReadOnly = False
        TextBoxNomArea.ReadOnly = False
        TextBoxPtoImpre.ReadOnly = False
        TextBoxArea2.ReadOnly = False
        TextBoxArea3.ReadOnly = False
        TextBoxArea4.ReadOnly = False
        TextBoxTipo.ReadOnly = False
        ComboBoxModImpreAREAS.Enabled = True
        '
        GRIDAREAS.Enabled = False
        ButtonNuevaArea.Enabled = False
        ButtonModificaArea.Enabled = False
        ButtonEliminaArea.Enabled = False
        ButtonAceptaArea.Enabled = True
        '
        TextBoxCodArea.Focus()
        '
    End Sub

    Private Sub CancelaNuevaArea()
        '
        BtnAccion = 0
        '
        CheckBoxTImpre.Enabled = False
        TextBoxCodArea.ReadOnly = True
        TextBoxNomArea.ReadOnly = True
        TextBoxPtoImpre.ReadOnly = True
        TextBoxArea2.ReadOnly = True
        TextBoxArea3.ReadOnly = True
        TextBoxArea4.ReadOnly = True
        TextBoxTipo.ReadOnly = True
        ComboBoxModImpreAREAS.Enabled = False
        GRIDAREAS.Enabled = True
        ButtonNuevaArea.Enabled = True
        ButtonAceptaArea.Enabled = False
        '
        ' Lista de Areas
        '
        GRIDAREAS.Focus()
        If CargaListaAREAS(wCaja) = True Then
            ButtonModificaArea.Enabled = True
            ButtonEliminaArea.Enabled = True
            GRIDAREAS.CurrentCell = GRIDAREAS.Rows(0).Cells(0)
            DatosGridaCajaTextos()
        Else
            ButtonModificaArea.Enabled = False
            ButtonEliminaArea.Enabled = False
        End If
        '
    End Sub

    Private Sub TextBoxCodArea_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCodArea.KeyPress
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            If BtnAccion = 1 Then
                Select Case e.KeyChar
                    Case ChrW(Keys.Enter)
                        If ValidaCodNomArea(TextBoxCodArea.Text.Trim) = True Then
                            '
                            If ExisteArea() = False Then
                                Dim wCodarea As String = TextBoxCodArea.Text
                                TextBoxCodArea.Text = ""
                                TextBoxNomArea.Text = ""
                                TextBoxPtoImpre.Text = ""
                                TextBoxArea2.Text = ""
                                TextBoxArea3.Text = ""
                                TextBoxArea4.Text = ""
                                TextBoxTipo.Text = ""
                                ComboBoxModImpreAREAS.Text = ""
                                CheckBoxTImpre.Checked = False
                                TextBoxCodArea.Text = wCodarea
                            End If
                            TextBoxNomArea.Focus()
                        End If
                End Select
                e = Nothing
            End If
        End If
        '
    End Sub

    Private Function ValidaCodNomArea(WDatosArea As String) As Boolean
        '
        '   Evitar Código / Nombre vacío...
        '
        ValidaCodNomArea = True
        If Len(WDatosArea) = 0 Or IsNothing(WDatosArea) Then
            ValidaCodNomArea = False
        End If
        '
    End Function

    Private Function ExisteArea() As Boolean
        '
        '   Se comprueba la Existencia o No de un Area.
        '
        ExisteArea = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [AREAS] where "
        queryString = queryString & "[AREAS].[AREA]=" & CInt(TextBoxCodArea.Text.Trim) & " AND "
        queryString = queryString & "[AREAS].[NUMCAJA]=" & wCaja
        Dim dt As DataSet = New DataSet
        '
        Try
            Dim TblAreas As SqlDataAdapter = New SqlDataAdapter(queryString, conexion)
            TblAreas.Fill(dt, "AREAS")
            '
            If dt.Tables("AREAS").Rows.Count > 0 Then
                '
                ' Si La Tabla tiene SOLO una FILA ...
                '
                With dt.Tables("AREAS")
                    If BtnAccion <> 2 Then
                        TextBoxCodArea.Text = .Rows(0)("AREA").ToString()
                        TextBoxNomArea.Text = .Rows(0)("DESCRIPCION").ToString()
                        TextBoxPtoImpre.Text = .Rows(0)("PUERTOIMPRE").ToString()
                        TextBoxArea2.Text = .Rows(0)("AREA2").ToString()
                        TextBoxArea3.Text = .Rows(0)("AREA3").ToString()
                        TextBoxArea4.Text = .Rows(0)("AREA4").ToString()
                        TextBoxTipo.Text = .Rows(0)("REPLICAR").ToString()
                        '
                        If .Rows(0)("TIPOIMPRESION").ToString() = "True" Then
                            CheckBoxTImpre.Checked = True
                        Else
                            CheckBoxTImpre.Checked = False
                        End If
                        ComboBoxModImpreAREAS.Text = .Rows(0)("MODELOIMPRE").ToString()
                        '
                    End If
                End With
                '
                ExisteArea = True
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [AREAS], Lectura Area.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Sub TextBoxCodArea_GotFocus(sender As Object, e As EventArgs) Handles TextBoxCodArea.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxCodArea.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxCodArea_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCodArea.LostFocus
        TextBoxCodArea.BackColor = Color.White
    End Sub

    Private Sub TextBoxNomArea_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNomArea.KeyDown
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxCodArea.Focus()
                Case Keys.Enter, Keys.Down
                    If ValidaCodNomArea(TextBoxNomArea.Text.Trim) = True Then
                        TextBoxPtoImpre.Focus()
                    End If
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxNomArea_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNomArea.LostFocus
        TextBoxNomArea.BackColor = Color.White
    End Sub

    Private Sub TextBoxNomArea_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNomArea.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxNomArea.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxPtoImpre_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPtoImpre.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxPtoImpre.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxArea2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxArea2.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxArea2.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxArea3_GotFocus(sender As Object, e As EventArgs) Handles TextBoxArea3.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxArea3.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxArea4_GotFocus(sender As Object, e As EventArgs) Handles TextBoxArea4.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxArea4.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxTipo_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTipo.GotFocus
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            TextBoxTipo.BackColor = Color.Cyan
            SendKeys.Send("{Home}+{End}")
        End If
        '
    End Sub

    Private Sub TextBoxPtoImpre_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPtoImpre.LostFocus
        TextBoxPtoImpre.BackColor = Color.White
    End Sub

    Private Sub TextBoxTipo_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTipo.LostFocus
        TextBoxTipo.BackColor = Color.White
    End Sub

    Private Sub TextBoxArea2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxArea2.LostFocus
        TextBoxArea2.BackColor = Color.White
    End Sub

    Private Sub TextBoxArea3_LostFocus(sender As Object, e As EventArgs) Handles TextBoxArea3.LostFocus
        TextBoxArea3.BackColor = Color.White
    End Sub

    Private Sub TextBoxArea4_LostFocus(sender As Object, e As EventArgs) Handles TextBoxArea4.LostFocus
        TextBoxArea4.BackColor = Color.White
    End Sub

    Private Sub TextBoxPtoImpre_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxPtoImpre.KeyDown
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxNomArea.Focus()
                Case Keys.Enter, Keys.Down
                    If ValidaCodNomArea(TextBoxPtoImpre.Text.Trim) = True Then
                        TextBoxArea2.Focus()
                    End If
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxArea2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxArea2.KeyDown
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxPtoImpre.Focus()
                Case Keys.Enter, Keys.Down
                    TextBoxArea3.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxArea3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxArea3.KeyDown
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxArea2.Focus()
                Case Keys.Enter, Keys.Down
                    TextBoxArea4.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxArea4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxArea4.KeyDown
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxArea3.Focus()
                Case Keys.Enter, Keys.Down
                    TextBoxTipo.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTipo.KeyDown
        '
        If BtnAccion = 1 Or BtnAccion = 2 Then
            Select Case e.KeyCode
                Case Keys.Up
                    TextBoxArea4.Focus()
                Case Keys.Enter, Keys.Down
                    ButtonAceptaArea.Select()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub ButtonAceptaArea_Click(sender As Object, e As EventArgs) Handles ButtonAceptaArea.Click
        '
        ' Aceptar (Grabar) Nuevas Areas
        '
        Select Case BtnAccion
            Case 1
                GrabaRegistroAreas()
            Case 2
                ModificaRegistroareas()
        End Select
        '
    End Sub

    Private Sub GrabaRegistroAreas()
        '
        If TextBoxCodArea.Text.Length = 0 Or TextBoxNomArea.Text.Length = 0 Or TextBoxPtoImpre.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código, Nombre y Puerto son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArea.Focus()
            Exit Sub
        End If
        If ComboBoxModImpreAREAS.Text.Length = 0 Then
            TEMP = ""
            TEMP &= "Modelo de Impresora necesario." & vbCrLf
            TEMP &= "Si no hay Modelo en la lista, " & vbCrLf
            TEMP &= "por favor cree los MODELOS de impresoras." & vbCrLf
            MsgBox("Atención Datos incorrectos." & vbCr &
                   TEMP,
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            ComboBoxModImpreAREAS.Focus()
            Exit Sub
        End If
        '
        ' Evita Errores en Conversion Cint()
        '
        If TextBoxArea2.Text.Trim.Length = 0 Then
            TextBoxArea2.Text = "0"
        End If
        If TextBoxArea3.Text.Trim.Length = 0 Then
            TextBoxArea3.Text = "0"
        End If
        If TextBoxArea4.Text.Trim.Length = 0 Then
            TextBoxArea4.Text = "0"
        End If
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Paso (1) - Existe ya el AREA a Crear?
        '
        If ExisteArea() = False Then
            '
            ' Paso (2) - Se crea NUEVO registro SOLO si NO EXISTE
            '
            '
            Dim queryString As String = ""
            queryString = "Insert Into [AREAS] ("
            queryString = queryString & " [NUMCAJA],"
            queryString = queryString & " [AREA],"
            queryString = queryString & " [DESCRIPCION],"
            queryString = queryString & " [PUERTOIMPRE],"
            queryString = queryString & " [AREA2],"
            queryString = queryString & " [AREA3],"
            queryString = queryString & " [AREA4],"
            queryString = queryString & " [REPLICAR],"
            queryString = queryString & " [TIPOIMPRESION], "
            queryString = queryString & " [MODELOIMPRE] "
            queryString = queryString & ") Values ("
            queryString = queryString & wCaja & ","
            queryString = queryString & CInt(TextBoxCodArea.Text.Trim) & ","
            queryString = queryString & "'" & TextBoxNomArea.Text.Trim & "',"
            queryString = queryString & "'" & TextBoxPtoImpre.Text.Trim & "',"
            queryString = queryString & CInt(TextBoxArea2.Text.Trim) & ","
            queryString = queryString & CInt(TextBoxArea3.Text.Trim) & ","
            queryString = queryString & CInt(TextBoxArea4.Text.Trim) & ","
            queryString = queryString & "'" & TextBoxTipo.Text.Trim & "', "
            If CheckBoxTImpre.Checked = True Then
                queryString = queryString & "'" & 1 & "', "
            Else
                queryString = queryString & "'" & 0 & "', "
            End If
            queryString = queryString & "'" & ComboBoxModImpreAREAS.Text.Trim & "' "

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
                                "Comprobar tabla [TG], Grabando Area.")
            End Try
            '
            CancelaNuevaArea()
        Else
            MsgBox("Atención este AREA ya existe." & vbCr &
                   "Bórrela antes o bien modifique el AREA existente.",
                    MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArea.Focus()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub ModificaRegistroareas()
        '
        If TextBoxCodArea.Text.Length = 0 Or TextBoxNomArea.Text.Length = 0 Or TextBoxPtoImpre.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código, Nombre y Puerto son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxNomArea.Focus()
            Exit Sub
        End If
        If ComboBoxModImpreAREAS.Text.Length = 0 Then
            TEMP = ""
            TEMP &= "Modelo de Impresora necesario." & vbCrLf
            TEMP &= "Si no hay Modelo en la lista, " & vbCrLf
            TEMP &= "por favor cree los MODELOS de impresoras." & vbCrLf
            MsgBox("Atención Datos incorrectos." & vbCr &
                   TEMP,
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            ComboBoxModImpreAREAS.Focus()
            Exit Sub
        End If
        '
        ' Evita Errores en Conversion Cint()
        '
        If TextBoxArea2.Text.Trim.Length = 0 Then
            TextBoxArea2.Text = "0"
        End If
        If TextBoxArea3.Text.Trim.Length = 0 Then
            TextBoxArea3.Text = "0"
        End If
        If TextBoxArea4.Text.Trim.Length = 0 Then
            TextBoxArea4.Text = "0"
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
        If ExisteArea() = True Then
            '
            Dim queryString As String = ""
            queryString = "UPDATE [AREAS] SET "
            queryString = queryString & " [DESCRIPCION]='" & TextBoxNomArea.Text.Trim & "',"
            queryString = queryString & " [PUERTOIMPRE]='" & TextBoxPtoImpre.Text.Trim & "',"
            queryString = queryString & " [AREA2]=" & CInt(TextBoxArea2.Text.Trim) & ","
            queryString = queryString & " [AREA3]=" & CInt(TextBoxArea3.Text.Trim) & ","
            queryString = queryString & " [AREA4]=" & CInt(TextBoxArea4.Text.Trim) & ","
            queryString = queryString & " [REPLICAR]='" & TextBoxTipo.Text.Trim & "',"
            If CheckBoxTImpre.Checked = True Then
                queryString = queryString & " [TIPOIMPRESION]='" & 1 & "', "
            Else
                queryString = queryString & " [TIPOIMPRESION]='" & 0 & "', "
            End If
            queryString = queryString & " [MODELOIMPRE]='" & ComboBoxModImpreAREAS.Text.Trim & "' "
            '
            queryString = queryString & " WHERE "
            queryString = queryString & " [NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & " [AREA]=" & CInt(TextBoxCodArea.Text.Trim)
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [AREA], Modificando Area.")
            End Try
            '
            CancelaNuevaArea()
        Else
            MsgBox("Atención AREA NO existente.",
                    MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArea.Focus()
        End If
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Private Sub ButtonModificaArea_Click(sender As Object, e As EventArgs) Handles ButtonModificaArea.Click
        '
        ' Modificar Registro Areas
        '
        BtnAccion = 2
        '
        CheckBoxTImpre.Enabled = True
        TextBoxNomArea.ReadOnly = False
        TextBoxPtoImpre.ReadOnly = False
        TextBoxArea2.ReadOnly = False
        TextBoxArea3.ReadOnly = False
        TextBoxArea4.ReadOnly = False
        TextBoxTipo.ReadOnly = False
        ComboBoxModImpreAREAS.Enabled = True
        '
        GRIDAREAS.Enabled = False
        ButtonNuevaArea.Enabled = False
        ButtonModificaArea.Enabled = False
        ButtonEliminaArea.Enabled = False
        ButtonAceptaArea.Enabled = True
        '
        TextBoxNomArea.Focus()
        '
    End Sub

    Private Sub ButtonEliminaArea_Click(sender As Object, e As EventArgs) Handles ButtonEliminaArea.Click
        '
        '   Borrado de AREAS existentes.
        '
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        Dim VbResp = MsgBox("¿Desea Borrar este AREA? .: " & vbCrLf &
           Me.TextBoxCodArea.Text & vbCrLf &
           Me.TextBoxNomArea.Text, style, "Borrar Registro!")
        '
        If VbResp = vbNo Then
            Exit Sub
        End If
        '
        If TextBoxCodArea.Text.Length = 0 Or TextBoxNomArea.Text.Length = 0 Then
            MsgBox("Atención Datos incorrectos." & vbCr &
                   "Código y Nombre de AREA son necesarios.",
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly, "Aviso.")
            TextBoxCodArea.Focus()
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
        ' Existe ya el AREA?
        '
        If ExisteArea() = True Then
            '
            ' Si existe BORRADO !!!!
            '
            Dim queryString As String = "Delete [AREAS] WHERE "
            queryString = queryString & " [NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & " [AREA]=" & CInt(TextBoxCodArea.Text.Trim)
            '
            Try
                cmd.CommandText = queryString
                cmd.Connection = conexion
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [AREAS], Borrando Registro.")
            End Try
            CancelaNuevaArea()
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

    Private Sub ButtonRGColDefecto_Click(sender As Object, e As EventArgs) Handles ButtonRGColDefecto.Click
        '
        ' Establece los colores por defecto para la aplicación ...
        '
        If Me.GRIDColores.Rows.Count > 0 Then
            CambioColores = True
            '
            GRIDColores.Rows(0).Cells(1).Style.BackColor = Color.FromArgb(109, 217, 248) ' Fondo FORM MESAS
            GRIDColores.Rows(1).Cells(1).Style.BackColor = Color.DarkTurquoise           ' Fondo FORM PRODUCTOS
            GRIDColores.Rows(2).Cells(1).Style.BackColor = Color.DodgerBlue              ' Fondo Botones Familias
            GRIDColores.Rows(3).Cells(1).Style.BackColor = Color.Yellow                  ' Letra Botones Familias
            GRIDColores.Rows(4).Cells(1).Style.BackColor = Color.Orange                  ' Fondo Botones Articulos
            GRIDColores.Rows(5).Cells(1).Style.BackColor = Color.Black                   ' Letra Botones Articulos
            '
            GRIDColores.Rows(0).Cells(2).Style.BackColor = Color.FromArgb(109, 217, 248) ' Fondo FORM MESAS
            GRIDColores.Rows(1).Cells(2).Style.BackColor = Color.DarkTurquoise           ' Fondo FORM PRODUCTOS
            GRIDColores.Rows(2).Cells(2).Style.BackColor = Color.DodgerBlue              ' Fondo Botones Familias
            GRIDColores.Rows(3).Cells(2).Style.BackColor = Color.Yellow                  ' Letra Botones Familias
            GRIDColores.Rows(4).Cells(2).Style.BackColor = Color.Orange                  ' Fondo Botones Articulos
            GRIDColores.Rows(5).Cells(2).Style.BackColor = Color.Black                   ' Letra Botones Articulos
        End If
        '
    End Sub

    Private Sub TCONA406_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        If GRIDColores.Rows.Count > 0 Then
            GRIDColores.ClearSelection()
        End If
        '
        With wrTecladoFlotante
            If TCONA406_Started = False Then
                TCONA406_Started = True
                '
                CargaListaTKFavoritos()
                '
                ' Acciones al Entrar y Activarse.
                '
                TextBoxClActual.Text = ""
                TextBoxClNueva.Text = ""
                TextBoxClNueva1.Text = ""
                ButtonActualizaClave.Enabled = False
                TextBoxClActual.Focus()
                .CodigoRetorno = 0
                .MensaUsuario = ""
            Else
                '
                ' Acciones Cuando se mantiene Visible y se re-activa.
                ' Recogemos Texto desde el teclado...
                '
                Select Case .CodigoRetorno
                    Case 1
                        '
                        ' Este "E.Egg" nos permitirá ver las claves si
                        ' somos programadores.
                        '
                        TextBoxClActual.Text = .CadenaVisor
                        If TextBoxClActual.Text.Trim.Length > 0 Then
                            If TextBoxClActual.Text.Trim = PassTRIVALLE(0) Or
                               TextBoxClActual.Text.Trim = PassTRIVALLE(1) Then
                                TextBoxClActual.Text = ""
                                CargaListaClaves(5, 1)
                            Else
                                '
                                ' Aqui se valida la clave Actual del NIVEL 
                                '  seleccionado.
                                '
                                If TextBoxClActual.Text.Trim = wrCLAVES.CLAVE.Trim Then
                                    TextBoxClNueva.Enabled = True
                                    ButtonTecladoPwdCN.Enabled = True
                                    ButtonTecladoPwdCN.Select()
                                Else
                                    TextBoxClNueva.Enabled = False
                                    ButtonTecladoPwdCN.Enabled = False
                                    ButtonTecladoPwdCA.Select()
                                End If
                                CargaListaClaves(wrLeeCODNOM.NIVELACCESO)
                            End If
                        End If
                    Case 2
                        TextBoxClNueva.Text = .CadenaVisor
                        If TextBoxClNueva.Text.Trim.Length > 0 Then
                            TextBoxClNueva1.Enabled = True
                            ButtonTecladoPwdCN1.Enabled = True
                            ButtonTecladoPwdCN1.Select()
                        Else
                            TextBoxClNueva1.Enabled = False
                            ButtonTecladoPwdCN1.Enabled = False
                            ButtonTecladoPwdCN.Select()
                        End If
                    Case 3
                        TextBoxClNueva1.Text = .CadenaVisor
                        ButtonActualizaClave.Enabled = False
                        '
                        If TextBoxClNueva.Text.Trim.Length > 0 And
                           TextBoxClNueva1.Text.Trim.Length > 0 Then
                            If TextBoxClNueva.Text.Trim = TextBoxClNueva1.Text.Trim Then
                                ButtonActualizaClave.Enabled = True
                                ButtonActualizaClave.Select()
                            End If
                        End If
                End Select
            End If
        End With
        '
    End Sub

    Private Sub TextBoxRetardoSplash_GotFocus(sender As Object, e As EventArgs) Handles TextBoxRetardoSplash.GotFocus
        '
        With TextBoxRetardoSplash
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
    End Sub

    Private Sub TextBoxRetardoSplash_LostFocus(sender As Object, e As EventArgs) Handles TextBoxRetardoSplash.LostFocus
        TextBoxRetardoSplash.BackColor = Color.White
    End Sub

    Private Sub TextBoxRetardoSplash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxRetardoSplash.KeyPress
        '
        ' Retardo en milisegundos Splah Screen
        ' No mas de 5 segundos ...
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    If IsNumeric(TextBoxRetardoSplash.Text.Trim) Then
                        If CInt(TextBoxRetardoSplash.Text.Trim) > 5000 Then
                            TextBoxRetardoSplash.Text = "5000"
                        End If
                    Else
                        TextBoxRetardoSplash.Text = "2500"
                    End If
                    ButtonRFExit.Select()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub CheckBoxCargarFav_Click(sender As Object, e As EventArgs) Handles CheckBoxCargarFav.Click
        '
        ' Group Seleccionar Boton Favorito
        '
        If CheckBoxCargarFav.Checked Then
            GroupBoxFAV.Enabled = True
        Else
            GroupBoxFAV.Enabled = False
        End If
        '
    End Sub

    Private Sub RadioButtonBEBIDAS_Click(sender As Object, e As EventArgs) Handles RadioButtonBEBIDAS.Click
        '
        ' BEBIDAS SELECCIONADA
        '
        wrLeeTCONA4.Tcona4_BOTONFAVORITO = "BEBIDAS"
        '
    End Sub

    Private Sub RadioButtonCOMIDAS_Click(sender As Object, e As EventArgs) Handles RadioButtonCOMIDAS.Click
        '
        ' BEBIDAS SELECCIONADA
        '
        wrLeeTCONA4.Tcona4_BOTONFAVORITO = "COMIDAS"
        '
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDAREAS
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
        With GRIDAREAS
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

    Private Sub TextBoxTPVP1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP1.GotFocus
        '
        With TextBoxTPVP1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP2.GotFocus
        '
        With TextBoxTPVP2
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP5_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP5.GotFocus
        '
        With TextBoxTPVP5
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP6_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP6.GotFocus
        '
        With TextBoxTPVP6
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP7_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP7.GotFocus
        '
        With TextBoxTPVP7
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP8_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP8.GotFocus
        '
        With TextBoxTPVP8
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP9_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP9.GotFocus
        '
        With TextBoxTPVP9
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTPVP1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP1.LostFocus
        TextBoxTPVP1.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP2.LostFocus
        TextBoxTPVP2.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP5_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP5.LostFocus
        TextBoxTPVP5.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP6_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP6.LostFocus
        TextBoxTPVP6.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP7_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP7.LostFocus
        TextBoxTPVP7.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP8_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP8.LostFocus
        TextBoxTPVP8.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP9_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTPVP9.LostFocus
        TextBoxTPVP9.BackColor = Color.White
    End Sub

    Private Sub TextBoxTPVP1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP1.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTipoPvp.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP2.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTPVP2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP2.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP1.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP5.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTPVP5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP5.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP2.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP6.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTPVP6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP6.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP5.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP7.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTPVP7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP7.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP6.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP8.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTPVP8_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP8.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP7.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTPVP9.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTPVP9_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTPVP9.KeyDown
        '
        ' Nombre TipoPVP
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP8.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxNombreCaja.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonPredetemina.Click
        '
        ' Predeterminar una Impresora en Windows.
        '
        If LabelImpreSel.Text.Trim.Length > 0 Then
            SetDefaultPrinter(LabelImpreSel.Text.Trim)
            txtImpresoraPredeterminada.Text = LabelImpreSel.Text.Trim
        End If
        '
    End Sub

    Private Sub GRIDIMPRESYS_Click(sender As Object, e As EventArgs) Handles GRIDIMPRESYS.Click
        '
        If Me.GRIDIMPRESYS.SelectedRows.Count > 0 Then
            LabelImpreSel.Text = GRIDIMPRESYS.SelectedCells(0).Value.ToString
        End If
        '
    End Sub

    Private Sub ButtonLIUp_Click(sender As Object, e As EventArgs) Handles ButtonLIUp.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDIMPRESYS
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

    Private Sub ButtonLiDwn_Click(sender As Object, e As EventArgs) Handles ButtonLiDwn.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDIMPRESYS
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

    Private Sub GRIDAREAS_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDAREAS.SelectionChanged
        '
        If GRIDAREAS.SelectedRows.Count > 0 Then
            DatosGridaCajaTextos()
        End If
        '
    End Sub

    Private Sub GRIDIMPRESYS_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDIMPRESYS.SelectionChanged
        '
        If Me.GRIDIMPRESYS.SelectedRows.Count > 0 Then
            LabelImpreSel.Text = GRIDIMPRESYS.SelectedCells(0).Value.ToString
            '
            ImpresoraEstaONLINE(GRIDIMPRESYS.SelectedCells(0).Value.ToString.Trim)
            TEMP = "Estado de la impresora.: " & vbCrLf
            Select Case wrProp_IMPRESORA.WorkOffline
                Case "True"
                    TEMP &= " > Apagada " & vbCrLf
                Case "False"
                    TEMP &= " > Encendida " & vbCrLf
            End Select
            TEMP &= " > Trabajos pendientes.: " & wrProp_IMPRESORA.JobCountSinceLastReset
            LabelInfoPrinter.Text = TEMP
        End If
        '
    End Sub

    Private Sub ButtonLIUp1_Click(sender As Object, e As EventArgs) Handles ButtonLIUp1.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDIMPREMODELOS
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

    Private Sub ButtonLiDwn1_Click(sender As Object, e As EventArgs) Handles ButtonLiDwn1.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDIMPREMODELOS
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

    Private Sub GRIDIMPREMODELOS_Click(sender As Object, e As EventArgs) Handles GRIDIMPREMODELOS.Click
        '
        If Me.GRIDIMPREMODELOS.SelectedRows.Count > 0 Then
            LabelImpreSelMODEL.Text = GRIDIMPREMODELOS.SelectedCells(0).Value.ToString
        End If
        '
    End Sub

    Private Sub GRIDIMPREMODELOS_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDIMPREMODELOS.SelectionChanged
        '
        If Me.GRIDIMPREMODELOS.SelectedRows.Count > 0 Then
            LabelImpreSelMODEL.Text = GRIDIMPREMODELOS.SelectedCells(0).Value.ToString
        End If
        '
    End Sub

    Private Sub ButtonFijaMODEL_Click(sender As Object, e As EventArgs) Handles ButtonFijaMODEL.Click
        '
        ' Fijamos el MODELO seleccionado como nueva impresora de TICKETS para
        '  la aplicación.
        ' Se guardará este MODELO en Ref. Genereales como MODELO de trabajo y
        '  la aplicación usará dicha IMPRESORA.
        ' NOTA, es posible que se tabaje con mas de un MODELO al mismo tiempo
        '  por lo que habria que adaptarse a dicho escenario en un futuro ...
        '
        LabelMODELOFijado.Text = LabelImpreSelMODEL.Text.Trim
        '
    End Sub

    Private Sub ButtonCreaESCPOS_Click(sender As Object, e As EventArgs) Handles ButtonCreaESCPOS.Click
        '
        ' Desde aqui gestionamos la creación de MODELOS de impresora
        '  y a su vez crearemos los distintos códigos ESC/POS o de otra índole
        ' para trabajar con dcicho MODELO de impresora.
        '
        ' Formulario MODELOS impresora
        ' -MODAL-
        '
        MyFrm17.ShowDialog(Me)
        '
    End Sub

    Private Sub CheckBoxImpreFACTURA_Click(sender As Object, e As EventArgs)
        '
        ' Imprime TK FAC? S/N
        '
        If CheckBoxImpreFACTURA.Checked = True Then
            CheckBoxCOBVIEWPDSN.Enabled = True
            TextBoxImpoMimImpreTKFAC.Enabled = True
        Else
            CheckBoxCOBVIEWPDSN.Enabled = False
            TextBoxImpoMimImpreTKFAC.Enabled = False
        End If
        '
    End Sub

    Private Sub TextBoxImpoMimImpreTKFAC_GotFocus(sender As Object, e As EventArgs)
        '
        With TextBoxImpoMimImpreTKFAC
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxImpoMimImpreTKFAC_LostFocus(sender As Object, e As EventArgs)
        TextBoxImpoMimImpreTKFAC.BackColor = Color.White
    End Sub

    Private Sub TextBoxImpoMimImpreTKFAC_KeyDown(sender As Object, e As KeyEventArgs)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxIGICTKFAC.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxTKFACPtoCaptura.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxImpoMimImpreTKFAC_KeyPress(sender As Object, e As KeyPressEventArgs)
        '
        ' Importe Minimo Impresión [999,99 / 999.99]
        '
        Dim allowedChars As String = "0123456789.," & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxNumSaltoLineas.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxNumSaltoLineas_GotFocus(sender As Object, e As EventArgs)
        '
        With TextBoxNumSaltoLineas
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxNumSaltoLineas_LostFocus(sender As Object, e As EventArgs)
        TextBoxNumSaltoLineas.BackColor = Color.White
    End Sub

    Private Sub TextBoxNumSaltoLineas_KeyDown(sender As Object, e As KeyEventArgs)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTKFACPtoCaptura.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxNumSaltoLineas_KeyPress(sender As Object, e As KeyPressEventArgs)
        '
        ' Salto Líneas TK AREAS [99]
        '
        Dim allowedChars As String = "0123456789" & vbBack
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
            Select Case e.KeyChar
                Case ChrW(Keys.Enter)
                    TextBoxLinPie1.Focus()
            End Select
            e = Nothing
        End If
        '
    End Sub

    Private Sub TextBoxTKFACPtoCaptura_GotFocus(sender As Object, e As EventArgs)
        '
        With TextBoxTKFACPtoCaptura
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxTKFACPtoCaptura_KeyDown(sender As Object, e As KeyEventArgs)
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxImpoMimImpreTKFAC.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxNumSaltoLineas.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxTKFACPtoCaptura_LostFocus(sender As Object, e As EventArgs)
        TextBoxTKFACPtoCaptura.BackColor = Color.White
    End Sub

    Private Sub Button9991_Click(sender As Object, e As EventArgs) Handles Button9991.Click
        TextBoxArea2.Text = "999"
    End Sub

    Private Sub Button9992_Click(sender As Object, e As EventArgs) Handles Button9992.Click
        TextBoxArea3.Text = "999"
    End Sub

    Private Sub Button9993_Click(sender As Object, e As EventArgs) Handles Button9993.Click
        TextBoxArea4.Text = "999"
    End Sub

    Private Sub Button9994_Click(sender As Object, e As EventArgs) Handles Button9994.Click
        '
        TextBoxArea2.Text = "999"
        TextBoxArea3.Text = "999"
        TextBoxArea4.Text = "999"
        '
    End Sub

    Private Sub TextBoxDetCab1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab1.GotFocus
        '
        With TextBoxDetCab1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxDetCab2_GotFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab2.GotFocus
        '
        With TextBoxDetCab2
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxDetCab3_GotFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab3.GotFocus
        '
        With TextBoxDetCab3
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxDetCab4_GotFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab4.GotFocus
        '
        With TextBoxDetCab4
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxDetCab5_GotFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab5.GotFocus
        '
        With TextBoxDetCab5
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxDetCab6_GotFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab6.GotFocus
        '
        With TextBoxDetCab6
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxDetCab1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab1.LostFocus
        TextBoxDetCab1.BackColor = Color.White
    End Sub

    Private Sub TextBoxDetCab2_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab2.LostFocus
        TextBoxDetCab2.BackColor = Color.White
    End Sub

    Private Sub TextBoxDetCab3_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab3.LostFocus
        TextBoxDetCab3.BackColor = Color.White
    End Sub

    Private Sub TextBoxDetCab4_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab4.LostFocus
        TextBoxDetCab4.BackColor = Color.White
    End Sub

    Private Sub TextBoxDetCab5_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab5.LostFocus
        TextBoxDetCab5.BackColor = Color.White
    End Sub

    Private Sub TextBoxDetCab6_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDetCab6.LostFocus
        TextBoxDetCab6.BackColor = Color.White
    End Sub

    Private Sub TextBoxDetCab1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDetCab1.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxLinCab10.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxDetCab2.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxDetCab2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDetCab2.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxDetCab1.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxDetCab3.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxDetCab3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDetCab3.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxDetCab2.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxDetCab4.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxDetCab4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDetCab4.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxDetCab3.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxDetCab5.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxDetCab5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDetCab5.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxDetCab4.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxDetCab6.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TextBoxDetCab6_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDetCab6.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxDetCab5.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxLinPie1.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub ButtonExportar_Click(sender As Object, e As EventArgs) Handles ButtonExportar.Click
        '
        ' Exportar los Datos de Cabeceras y Pie Tickets.
        '
        GeneraTXTDATOSTICKETS()
        '
    End Sub

    Private Sub ButtonImportar_Click(sender As Object, e As EventArgs) Handles ButtonImportar.Click
        '
        ' Importar los Datos de Cabeceras y Pie Tickets.
        '
        CargaTXTDATOSTICKETS()
        '
    End Sub

    Private Sub TextBoxNombreCaja_GotFocus(sender As Object, e As EventArgs) Handles TextBoxNombreCaja.GotFocus
        '
        With TextBoxNombreCaja
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxNombreCaja_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNombreCaja.LostFocus
        TextBoxNombreCaja.BackColor = Color.White
    End Sub

    Private Sub TextBoxNombreCaja_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxNombreCaja.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Up
                TextBoxTPVP9.Focus()
            Case Keys.Enter, Keys.Down
                TextBoxEmpresa.Focus()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub ButtonComprobarFics_Click(sender As Object, e As EventArgs) Handles ButtonComprobarFics.Click
        '
        CompruebaExisteFicheros("EXT")
        '
    End Sub

    Private Sub ButtonComprobarApps_Click(sender As Object, e As EventArgs) Handles ButtonComprobarApps.Click
        '
        CompruebaExisteFicheros("APP")
        '
    End Sub

    Private Sub ButtonListaTablasDB_Click(sender As Object, e As EventArgs) Handles ButtonListaTablasDB.Click
        '
        NomdbCheck = "GESTITRV"
        CargaTablas(SQL_Instancia.Trim, NomdbCheck.Trim)
        '
    End Sub

    Private Sub ListBoxTablasDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxTablasDB.SelectedIndexChanged
        '
        CargaCamposTabla(SQL_Instancia.Trim, NomdbCheck, ListBoxTablasDB.SelectedItem.ToString)
        '
    End Sub

    Private Sub ListBoxCampos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxCampos.SelectedIndexChanged
        '
        InfoCampos(SQL_Instancia.Trim, NomdbCheck, ListBoxTablasDB.SelectedItem.ToString, ListBoxCampos.SelectedItem.ToString)
        '
    End Sub

    Private Sub ButtonLDBUp_Click(sender As Object, e As EventArgs) Handles ButtonLDBUp.Click
        '
        ' Subir una linea en la Lista
        '
        With ListBoxTablasDB
            If .Items.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                CursorGRID1 = .SelectedIndex
                '
                If CursorGRID1 > 0 Then
                    CursorGRID1 -= 1
                    .SelectedIndex = CursorGRID1
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonLDBDWn_Click(sender As Object, e As EventArgs) Handles ButtonLDBDWn.Click
        '
        ' Bajar una linea en la Lista
        '
        With ListBoxTablasDB
            If .Items.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                Dim GrNumRows As Integer = .Items.Count - 1
                CursorGRID1 = .SelectedIndex
                '
                If CursorGRID1 < GrNumRows Then
                    CursorGRID1 += 1
                    .SelectedIndex = CursorGRID1
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonLCAMUp_Click(sender As Object, e As EventArgs) Handles ButtonLCAMUp.Click
        '
        ' Subir una linea en la Lista
        '
        With ListBoxCampos
            If .Items.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                CursorGRID1 = .SelectedIndex
                '
                If CursorGRID1 > 0 Then
                    CursorGRID1 -= 1
                    .SelectedIndex = CursorGRID1
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonLCAMDWn_Click(sender As Object, e As EventArgs) Handles ButtonLCAMDWn.Click
        '
        ' Bajar una linea en la Lista
        '
        With ListBoxCampos
            If .Items.Count > 0 Then
                '
                ' Num. de Filas y Fila Actual
                '
                Dim GrNumRows As Integer = .Items.Count - 1
                CursorGRID1 = .SelectedIndex
                '
                If CursorGRID1 < GrNumRows Then
                    CursorGRID1 += 1
                    .SelectedIndex = CursorGRID1
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonGRIDCLArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCLArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDCLRF
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

    Private Sub ButtonGRIDCLAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCLAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDCLRF
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

    Private Sub GRIDCLRF_Click(sender As Object, e As EventArgs) Handles GRIDCLRF.Click
        '
        If Me.GRIDCLRF.SelectedRows.Count > 0 Then
            LblUsrActual.Text = GRIDCLRF.SelectedCells(1).Value.ToString
            LeeClaves(CInt(GRIDCLRF.SelectedCells(0).Value.ToString))
        End If
        '
    End Sub

    Private Sub GRIDCLRF_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDCLRF.SelectionChanged
        '
        If Me.GRIDCLRF.SelectedRows.Count > 0 Then
            LblUsrActual.Text = GRIDCLRF.SelectedCells(1).Value.ToString
            LeeClaves(CInt(GRIDCLRF.SelectedCells(0).Value.ToString))
        End If
        '
    End Sub

    Private Sub ButtonClsClaves_Click(sender As Object, e As EventArgs) Handles ButtonClsClaves.Click
        '
        HazClr()
        '
    End Sub

    Private Sub HazClr()
        '
        wrTecladoFlotante.CodigoRetorno = 0
        TextBoxClActual.Text = ""
        TextBoxClNueva.Text = ""
        TextBoxClNueva1.Text = ""
        TextBoxClNueva.Enabled = False
        TextBoxClNueva1.Enabled = False
        ButtonTecladoPwdCN.Enabled = False
        ButtonTecladoPwdCN1.Enabled = False
        ButtonActualizaClave.Enabled = False
        TextBoxClActual.Focus()
        CargaListaClaves(wrLeeCODNOM.NIVELACCESO)
        '
    End Sub

    Private Sub ButtonTecladoPwdCA_Click(sender As Object, e As EventArgs) Handles ButtonTecladoPwdCA.Click
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
            ' Se controla en la aplicación ...
            '
            .CodigoRetorno = 1
            '
        End With
        MyFrm15.ShowDialog(Me)
        '
    End Sub

    Private Sub TextBoxClNueva_GotFocus(sender As Object, e As EventArgs) Handles TextBoxClNueva.GotFocus
        '
        With TextBoxClNueva
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxClActual_GotFocus(sender As Object, e As EventArgs) Handles TextBoxClActual.GotFocus
        '
        With TextBoxClActual
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxClNueva1_GotFocus(sender As Object, e As EventArgs) Handles TextBoxClNueva1.GotFocus
        '
        With TextBoxClNueva1
            .BackColor = Color.Cyan
            If (Not String.IsNullOrEmpty(.Text)) Then
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End If
        End With
        '
    End Sub

    Private Sub TextBoxClActual_LostFocus(sender As Object, e As EventArgs) Handles TextBoxClActual.LostFocus
        TextBoxClActual.BackColor = Color.White
    End Sub

    Private Sub TextBoxClNueva_LostFocus(sender As Object, e As EventArgs) Handles TextBoxClNueva.LostFocus
        TextBoxClNueva.BackColor = Color.White
    End Sub

    Private Sub TextBoxClNueva1_LostFocus(sender As Object, e As EventArgs) Handles TextBoxClNueva1.LostFocus
        TextBoxClNueva1.BackColor = Color.White
    End Sub

    Private Sub ButtonTecladoPwdCN_Click(sender As Object, e As EventArgs) Handles ButtonTecladoPwdCN.Click
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
            ' Se controla en la aplicación ...
            '
            .CodigoRetorno = 2
            '
        End With
        MyFrm15.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonTecladoPwdCN1_Click(sender As Object, e As EventArgs) Handles ButtonTecladoPwdCN1.Click
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
            ' Se controla en la aplicación ...
            '
            .CodigoRetorno = 3
            '
        End With
        MyFrm15.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonActualizaClave_Click(sender As Object, e As EventArgs) Handles ButtonActualizaClave.Click
        '
        ' Actualizar la Clave del NIVEL/Entidad Seleccionado
        '
        If TextBoxClNueva.Text.Trim.Length > 0 And
           TextBoxClNueva1.Text.Trim.Length > 0 Then
            If TextBoxClNueva.Text.Trim = TextBoxClNueva1.Text.Trim Then
                If Me.GRIDCLRF.SelectedRows.Count > 0 Then
                    LblUsrActual.Text = GRIDCLRF.SelectedCells(1).Value.ToString
                    If LeeClaves(CInt(GRIDCLRF.SelectedCells(0).Value.ToString)) = True Then
                        ActualizaClave(CInt(GRIDCLRF.SelectedCells(0).Value.ToString))
                        CargaListaClaves(wrLeeCODNOM.NIVELACCESO)
                        HazClr()
                    End If
                End If
            End If
        End If
        '
    End Sub

    Private Sub ButtonComprobarCobViewdat_Click(sender As Object, e As EventArgs) Handles ButtonComprobarCobViewdat.Click
        '
        CompruebaExisteFicheros("VIEWDAT")
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonRunViewCfg.Click
        '        
        CompruebaExisteFicheros("VIEWCFG")
        '
    End Sub

    Private Sub ButtonConsolaMSDOS_Click(sender As Object, e As EventArgs) Handles ButtonConsolaMSDOS.Click
        '
        ' Una consola MSDOS
        '
        Dim myProcess As New Process()
        Try
            '
            ' Lanzar Proceso ...
            '
            With myProcess
                With .StartInfo
                    .UseShellExecute = True
                    .FileName = "CMD"
                End With
                ' <> Modificar Titulo
                .Start()
            End With
        Catch mye As Exception
            MsgBox("ERROR: " & mye.Source & vbCrLf & mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamando una Consola de comandos.")
        End Try
        '
    End Sub

    Private Sub TextBoxPwdCMD_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPwdCMD.TextChanged
        '
        ' Clave para consola MS-DOS
        '
        ButtonConsolaMSDOS.Enabled = False
        ButtonConsolaMSDOS.ForeColor = Color.Black
        ButtonEditText.Enabled = False
        ButtonEditText.ForeColor = Color.Black
        '
        If TextBoxPwdCMD.Text.Trim.Length > 0 Then
            If TextBoxPwdCMD.Text = PassTRIVALLE(0) Or TextBoxPwdCMD.Text = PassTRIVALLE(1) Then
                ButtonConsolaMSDOS.Enabled = True
                ButtonConsolaMSDOS.ForeColor = Color.Blue
                ButtonEditText.Enabled = True
                ButtonEditText.ForeColor = Color.Blue
            End If
        End If
        '
    End Sub

    Private Sub ButtonEditText_Click(sender As Object, e As EventArgs) Handles ButtonEditText.Click
        '
        ' Un pequeño Editor de textos.
        '        
        CompruebaExisteFicheros("EDITOR")
        '
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDCAJASRef
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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDCAJASRef
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

    Private Sub GRIDCAJASRef_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDCAJASRef.SelectionChanged
        '
        ' Si la Caja Seleccionada Tiene AREAS creadas
        '   hay registros que se pueden importar.
        ' Si se detecta que la Caja actual ya tiene registros, no permite importar.
        '
        With GRIDCAJASRef
            If .SelectedRows.Count > 0 Then
                If CargaListaAREAS(CInt(.SelectedCells(0).Value.ToString), 1) = True And
                    CargaListaAREAS(wCaja, 1) = False Then
                    ButtonImportarAreas.Enabled = True
                Else
                    ButtonImportarAreas.Enabled = False
                End If
            End If
        End With
        '
    End Sub

    Private Sub GRIDCAJASRef_Click(sender As Object, e As EventArgs) Handles GRIDCAJASRef.Click
        '
        ' Si la Caja Seleccionada Tiene AREAS creadas
        '   hay registros que se pueden importar.
        ' Si se detecta que la Caja actual ya tiene registros, no permite importar.
        '
        With GRIDCAJASRef
            If .SelectedRows.Count > 0 Then
                If CargaListaAREAS(CInt(.SelectedCells(0).Value.ToString), 1) = True And
                    CargaListaAREAS(wCaja, 1) = False Then
                    ButtonImportarAreas.Enabled = True
                Else
                    ButtonImportarAreas.Enabled = False
                End If
            End If
        End With
        '
    End Sub

    Private Sub ButtonImportarAreas_Click(sender As Object, e As EventArgs) Handles ButtonImportarAreas.Click
        '
        ' Importar las AREAS desde la Caja Selecionada a la Caja
        '   actual, solo si no tiene registros ...
        '
        ButtonImportarAreas.Enabled = False
        If CargaListaAREAS(wCaja, 1) = False Then
            TraspasaAreasEntreCajas(CInt(GRIDCAJASRef.SelectedCells(0).Value.ToString), wCaja)
            CargaListaAREAS(wCaja)
        End If
        '
    End Sub

    Private Sub ButtonListaTablasDB1_Click(sender As Object, e As EventArgs) Handles ButtonListaTablasDB1.Click
        '
        ' Initial Catalog=CONTATRVnnn
        ' CONTATRVnnn
        '
        NomdbCheck = Mid(DameCatalogoEmpresa(wEmpresa, "CONTATRV"), 17, 11)
        CargaTablas(SQL_Instancia.Trim, NomdbCheck)
        '
    End Sub
End Class