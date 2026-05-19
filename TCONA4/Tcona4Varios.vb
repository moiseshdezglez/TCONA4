
Imports System.IO
Imports System.Management

Module Tcona4Varios
    '-------------------------------------------------------------------------------------------
    ' Este Módulo este destinado a Contener Mayormente las Estructuras 
    '   (Structures) y sus elementos.
    ' Es un Punto facil para su localización y desarrollo.
    '
    ' ---
    '
    ' *** DIFINICION de PROPIEDADES de los FORMULARIOS DE LA APLICACION ***
    ' El Procedimiento DefineFORMULARIOS se gestiona en este Módulo.
    '
    '-------------------------------------------------------------------------------------------
    ' Declaracion de Clase :: Impresion DIRECTA LPTn: // COMn:
    '
    Public Class Win32API
        Public Const GENERIC_WRITE = &H40000000
        Public Const CREATE_ALWAYS = 2
        Public Const OPEN_EXISTING = 3
        Public Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, ByVal dwShareMode As Integer, ByVal lpSecurityAttributes As Integer, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As Integer
        Public Declare Function CloseHandle Lib "kernel32" Alias "CloseHandle" (ByVal hObject As Long) As Long
    End Class
    '
    '------------------------------------------------------------------------------------------------------------------------------
    ' Structure(S) - Estructuras de Datos.
    '------------------------------------------------------------------------------------------------------------------------------
    Public Structure struct_Prop_IMPRESORA
        ' Algunas de las Propiedades de una Impresora Determinada.
        ' Normalmente la Impresora Por Defecto y/o Modelo de trabajo.
        '
        Public WorkOffline As String ' True/False
        Public JobCountSinceLastReset As String ' Tabajos en Cola...
    End Structure

    Public Structure struct_IMPRESORAS
        ' Datos desde la Tabla [IMPRESORAS]
        ' Información sobre el Modelo de impresora establecido para la Aplicación.
        ' Basicamente recoge los distintos Códigos ESC/POS para trabajar 
        '   con la impresora.
        '
        Public CORTE As String
        Public ABRECAJON As String
        Public DIEZ_CPP As String
        Public DOBLEALTO As String
        Public DOBLEALTO12CPP As String
        Public DOBLEANCHO As String
        Public AVAZALINEA As String
        Public AVAZAnLINEAS As String
        Public ATRASnLINEAS As String
        '
        Public DOCECPP As String
        Public PROPORCIONAL As String
        Public COMPRIMIDO As String
        Public NEGRITA As String
        Public CURSIVA As String
    End Structure

    Public Structure struct_RACIONES
        '
        ' RACIONES 
        ' 
        Public RACIONES_indicador As Integer
        Public RACIONES_PVPRacion As String
        Public RACIONES_PVPMediaRacion As String
        Public RACIONES_IMPORTE As String
    End Structure

    Public Structure struct_CLAVES
        '
        ' CLAVES / NIVELES DE ACCESO
        ' 
        Public NIVELACCESO As Integer
        Public DESCRIPCION As String
        Public CLAVE As String
        Public ACCESOX As String  ' "True/False"
        Public ACCESOZ As String  ' "True/False"
        Public ACCESOAPPS As String  ' "True/False"
        Public BOTONMENOS As String  ' "True/False"
        Public BOTONPRECIO As String  ' "True/False"
        Public BOTONTARIFA As String  ' "True/False"
        '
        ' Define las Pestañas en Mant. Ref. Gen.
        '  a las que se tiene acceso...
        '
        Public ACCESOREFGEN As String  ' Concatenacion ...
    End Structure

    Public Structure struct_TecladoFlotante
        '
        ' Teclado Flotante en pantalla
        ' 
        '
        ' Tamaño Posición del FORM que llama al teclado.
        ' Limita Movimientos del teclado al "Area" del Form.
        '
        Public Top As Integer
        Public width As Integer
        Public Left As Integer
        Public height As Integer
        '
        Public PidePwd As Integer        ' Indica se se Pide Password (0=No/1=Si).
        '
        Public Tipo As Integer           ' Tipo de teclado a mostrar: 0=Numérico, 1=Alfabético, 2=Alfanumérico.
        Public MensaUsuario As String    ' Mensajes al Usuario.
        Public CadenaVisor As String     ' Cadena entrada en el teclado.
        Public BackColor As Color        ' Color de Fondo para ele teclado.
        Public MaxChar As Integer        ' 0 = Sin Límite
        Public CodigoRetorno As Integer  ' Determina el control al que retornar el texto entrado. Debe ser > 0
        Public CodigoInterno As Integer  ' Determina el control texto Favoritos del teclado.      Debe ser > 0
    End Structure

    Public Structure struct_ACUMXZ
        '
        ' Acumulados Por Familias (Tabla Temporal [ZZTablaXZTemp])
        ' Util Para Consulta general MESA/MESAH
        '
        Public ACUMXZ_Unid As String
        Public ACUMXZ_Importe As String
        Public ACUMXZ_Sala As String
    End Structure

    Public Structure ReSumenTOT_XZ
        '
        '  Uso: Resumen IMPORTES TOTALES X/Z
        '
        Public IMPDTO As Double
        Public IMPORTE As Double
        Public ENTREGA As Double
        Public TARJETA As Double
        Public VALEDTO As Double
        Public CHEQUES As Double
        Public OTROS As Double
        Public EFECTIVO As Double
        Public IMPIGIC As Double
    End Structure

    Public Structure RegistroMAR
        '
        '   Estructura Datos MAR
        '   NOTA: Todos los campos numericos
        '         se pasan como string con formato double "0.00"
        '
        Public Mar_FAMILIA As String
        Public Mar_DESCRIPCION As String
        Public Mar_PRECOSTO As String
        '
        ' PVP 1 a 9
        '
        Public Mar_PREPVP1 As String
        Public Mar_PREPVP2 As String
        Public Mar_PREPVP3 As String
        Public Mar_PREPVP4 As String
        Public Mar_PREPVP5 As String
        Public Mar_PREPVP6 As String
        Public Mar_PREPVP7 As String
        Public Mar_PREPVP8 As String
        Public Mar_PREPVP9 As String
        '
        ' PVP que Tipifica Ref. Generales. Se Usará este PVP en el TPV.
        ' Si el Usuario elige precio distinto, entonces contendrá dicho PVP
        '    hasta aparcar.
        '
        Public Mar_PREPVPTPV As String
        '
        Public Mar_COMBINADO As String
        Public Mar_IVAVENTA As String
        Public Mar_AREA As Integer
        Public Mar_IMAGEN As String
    End Structure

    Public Structure RegistroPEDCLIE
        '
        '   Estructura Datos PEDCLIE
        '
        Public NOMBRE As String
        Public DIRECCION As String
        Public POBLACION As String
        Public EMAIL As String
        Public CODPOSTAL As Integer
        Public EMAILSN As String ' True/False
        Public SMSSN As String ' True/False
        Public OBSER As String
    End Structure

    Public Structure RegistroCLIEMCO
        '
        '   Estructura Datos MCO
        '
        Public NOMBRE As String
        Public CIF As String
        Public DIRECCION As String
        Public POBLACION As String
        Public EMAIL As String
        Public CODPOSTAL As Integer
        Public TELEFONO As String
        Public TELEFONO2 As String
        Public DTO As String ' Double, recupero como String...
    End Structure

    Public Structure RegistroSALA
        '
        '   Estructura Datos SALA
        '
        Public Sala_NOMBRE As String
        Public Sala_NOZETA As Integer
        Public Sala_REPARTO As String ' True/False
    End Structure

    Public Structure RegistroSALA1
        '
        '   Estructura Datos SALA1 (Datos de una MESA)
        '
        Public Sala1_DESCMESA As String
        Public Sala1_PVP As String
        Public Sala1_PIDEPAX As String
        Public Sala1_LOGO As String
        Public Sala1_VISIBLE As String
        Public Sala1_FACTURA As Integer
        Public Sala1_FECAPERTURA As String
        Public Sala1_HORAAPAERTURA As String
        Public Sala1_VENDEDOR As Integer
        Public Sala1_PAX As Integer
        Public Sala1_IMPFACTU As String
    End Structure

    Public Structure RegistroTCONA4
        '
        '   Estructura Datos TCONA4 - Ref. Generales.
        '
        Public Tcona4_NOMBRECAJA As String ' Nombre Caja
        '
        Public Tcona4_COLORFTCONA401 As Color
        Public Tcona4_COLORFTCONA402 As Color
        Public Tcona4_COLORFF As Color
        Public Tcona4_COLORLF As Color
        Public Tcona4_COLORFA As Color
        Public Tcona4_COLORLA As Color
        '
        Public Tcona4_EMPRESA As Integer
        Public Tcona4_IGIC As Double
        Public Tcona4_FACTURA As Integer
        Public Tcona4_ALMACEN As Integer
        Public Tcona4_ORDENFAM As String      ' "True/False"
        Public Tcona4_ORDENART As String      ' "True/False"
        Public Tcona4_FORMINICIAL As Integer  ' Formulario Inicial
        '
        Public Tcona4_TKFACLOGO As String     ' "True/False"
        '
        Public Tcona4_TKFCABLI1 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI2 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI3 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI4 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI5 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI6 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI7 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI8 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI9 As String     ' Líneas Cabecera Ticket Factura
        Public Tcona4_TKFCABLI10 As String    ' Líneas Cabecera Ticket Factura
        '
        Public Tcona4_TKFPIELI1 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI2 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI3 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI4 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI5 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI6 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI7 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI8 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI9 As String     ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI10 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI11 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI12 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI13 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI14 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI15 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI16 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI17 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI18 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI19 As String    ' Líneas Pie Ticket Factura
        Public Tcona4_TKFPIELI20 As String    ' Líneas Pie Ticket Factura
        '
        Public Tcona4_TKFACIGIC As String       ' IGIG
        Public Tcona4_PIDEVENDEDOR As String    ' Pide Ven? "True/False"
        Public Tcona4_SEPARARACIONES As String  ' Pide 1/2 Rac.? "True/False"
        Public Tcona4_VARIOSFAM1 As String      ' Fam1 - Familias para Articulos VARIOS
        Public Tcona4_VARIOSFAM2 As String      ' Fam2 - ( Se permite PVP1=0 )
        Public Tcona4_VARIOSFAM3 As String      ' Fam3
        Public Tcona4_NUMX As Integer
        Public Tcona4_NUMZ As Integer
        Public Tcona4_REFRESCABOTONES As String ' "True/False" (Botonera Familias/Articulos, Persistente o Refrescable)
        Public Tcona4_TIPOPVPARTI As Integer ' PVP 1 a 9
        '
        Public Tcona4_FECHAZ As String           ' Fecha Z
        Public Tcona4_FECHADIASESION As String   ' Fecha Dia
        Public Tcona4_ZETAMESASOCU As String     ' "True/False" (Zeta con MESAS OCUPADAS?)
        Public Tcona4_TKZETALOGO As String       ' "True/False"
        Public Tcona4_SPLASHSCREEN As String     ' "True/False"
        Public Tcona4_SPLASHRETARDO As Integer   ' Retardo (ms)
        Public Tcona4_PIDEPAX As String          ' "True/False"
        Public Tcona4_CARGAFAVORITOS As String   ' "True/False"
        Public Tcona4_BOTONFAVORITO As String    ' "BEBIDAS/COMIDAS"
        '
        ' Nombre Tarifas
        '
        Public Tcona4_NOMTARIPVP1 As String
        Public Tcona4_NOMTARIPVP2 As String
        Public Tcona4_NOMTARIPVP5 As String
        Public Tcona4_NOMTARIPVP6 As String
        Public Tcona4_NOMTARIPVP7 As String
        Public Tcona4_NOMTARIPVP8 As String
        Public Tcona4_NOMTARIPVP9 As String
        '
        Public Tcona4_COBVIEWPDSN As String ' "True/False"
        Public Tcona4_MODIMPREFIJO As String
        '
        Public Tcona4_IMPRIMETKFAC As String ' "True/False"
        Public Tcona4_IMPOMINIMPRE As Double
        Public Tcona4_SALTOLINPIETK As Integer
        Public Tcona4_TKFACABRECAJON As String ' "True/False"
        Public Tcona4_TKFACPUERTO As String
        Public Tcona4_TKFACIMPDETCOMBI As String ' "True/False"
        Public Tcona4_COMPRUIMPREINI As String ' "True/False"
        '
        ' Frases Favoritas Teclado Flotante
        '
        Public Tcona4_TECLADOFAV1 As String ' 40 Carecteres...
        Public Tcona4_TECLADOFAV2 As String ' 40 Carecteres...
        Public Tcona4_TECLADOFAV3 As String ' 40 Carecteres...
        Public Tcona4_TECLADOFAV4 As String ' 40 Carecteres...
        Public Tcona4_TECLADOFAV5 As String ' 40 Carecteres...
        '
        ' Definicion Detalles TICKETs Fac, X, Z
        '
        Public Tcona4_TKFDETLIN1 As String
        Public Tcona4_TKFDETLIN2 As String
        Public Tcona4_TKFDETLIN3 As String
        Public Tcona4_TKXZDETLIN1 As String
        Public Tcona4_TKXZDETLIN2 As String
        Public Tcona4_TKXZDETLIN3 As String
        '
        Public Tcona4_ORDENPRODUCTOS As String      ' "True/False"
        Public Tcona4_PIDECAJAINICIO As String      ' "True/False"
        Public Tcona4_TKFACDIRWIN As String         ' "True/False"
        Public Tcona4_BORLINCUENTA As String        ' "True/False"
        Public Tcona4_CREACLICREDITO As String      ' "True/False"
        Public Tcona4_FADEOUTSALIR As String        ' "True/False", Efecto al Salir.
    End Structure

    Public Structure RegistroMESAC
        '
        '   Estructura Datos MESAC ( Cabecera )
        '   NOTA: Todos los campos numericos
        '         se pasan como string con formato double "0.00"
        '
        Public Mesac_EMPRESA As Integer
        Public Mesac_CLIENTE As Integer
        Public Mesac_VENDEDOR As Integer
        Public Mesac_PIGIC As String
        Public Mesac_PDTO As String
        Public Mesac_IMPDTO As String
        Public Mesac_IMPORTE As String
        Public Mesac_ENTREGA As String
        Public Mesac_TARJETA As String
        Public Mesac_VALEDTO As String
        Public Mesac_CHEQUES As String
        Public Mesac_OTROS As String
        Public Mesac_EFECTIVO As String
        Public Mesac_IMPIGIC As String
        Public Mesac_TKFACIMPRESO As String
        Public Mesac_TLFPEDIDOS As String
        Public Mesac_NIFCIF As String
    End Structure

    Public Structure RegistroMESA
        '
        '   Estructura Datos MESA ( Líneas Cuenta Mesa )
        '   NOTA: Todos los campos numericos
        '         se pasan como string con formato double "0.00"
        '
        Public Mesa_UNID As String
        Public Mesa_IMPORTE As String
        Public Mesa_PDTO As String
        Public Mesa_IMPDTO As String
        Public Mesa_VENDEDOR As Integer
        Public Mesa_HORA As String
        Public Mesa_COSTO As String
        Public Mesa_ALMACEN As String
        Public Mesa_IGIC As String
        Public Mesa_NOZETA As Integer
        Public Mesa_SALA As String
        Public Mesa_ORDENPLATO As Integer
    End Structure

    Public Structure RegistroTG
        '
        '   Estructura Datos GRUPO
        '
        Public Tg_GRUPO As String
        Public TG_NOMBRE As String
    End Structure

    Public Structure RegistroCODNOM
        '
        '   Estructura Datos Sólo COD. y NOMBRE
        '   Familias y Similares.
        '
        Public CODIGO As String
        Public NOMBRE As String
        Public NIVELACCESO As Integer ' Para Vendedores
    End Structure

    Public Structure RegistroAREAS
        '
        '   Estructura Datos AREAS
        '
        Public DESCRIPCION As String
        Public PUERTOIMPRE As String
        Public AREA2 As Integer
        Public AREA3 As Integer
        Public AREA4 As Integer
        Public REPLICAR As String
        Public TIPOIMPRESION As String ' Trur/False
        Public MODELOIMPRE As String
    End Structure
    '
    ' Uso: Leer Registros
    '
    Public wrLeeMESAC As RegistroMESAC = Nothing
    Public wrLeeMESA As RegistroMESA = Nothing
    Public wrLeeMAR As RegistroMAR = Nothing
    Public wrLeeSALA As RegistroSALA = Nothing
    Public wrLeeSALA1 As RegistroSALA1 = Nothing
    Public wrLeeTCONA4 As RegistroTCONA4 = Nothing
    Public wrLeeTG As RegistroTG = Nothing
    Public wrLeeCODNOM As RegistroCODNOM = Nothing
    Public wrLeeAREAS As RegistroAREAS = Nothing
    Public wrLeePEDCLIE As RegistroPEDCLIE = Nothing
    Public wrLeeCLIEMCO As RegistroCLIEMCO = Nothing
    '
    '  Uso: Grabar Registros
    '
    Public wrMESAC As RegistroMESAC = Nothing
    Public wrMESA As RegistroMESA = Nothing
    '
    '  Uso: Tratar RACIONES
    '
    Public wrRACIONES As struct_RACIONES = Nothing
    '
    '  Uso: Teclado Flotante
    '
    Public wrTecladoFlotante As struct_TecladoFlotante = Nothing
    '
    '  Uso: Claves/Niveles Acceso
    '
    Public wrCLAVES As struct_CLAVES = Nothing
    '
    '  Uso: Tratar ACUMULADO Por Familia, X/Z
    '  Uso: Resumen IMPORTES TOTALES X/Z
    '
    Public wrRESUMENXZ As ReSumenTOT_XZ = Nothing
    Public wrACUMXZ As struct_ACUMXZ = Nothing
    '
    '  Uso: Leer Secuencias ESCAPE ESC/POS Según MODELOS de IMPRESORA
    '       y Propiedades de una impresora determinada.
    '
    Public wrIMPRESORA As struct_IMPRESORAS = Nothing
    Public wrProp_IMPRESORA As struct_Prop_IMPRESORA = Nothing

    '
    ' *** DIFINICION de PROPIEDADES de los FORMULARIOS DE LA APLICACION ***
    '
    Public Sub DefineFORMULARIOS(wMiFormName As String)
        '
        ' Gestionamos las Propiedades de cada FORMULARIO de la aplicacion.
        '
        ' wMiFormName = "MyFrmN" ::
        '  N
        ' ---
        ' MyFrm1 As New TCONA401  ' MESAS.
        ' MyFrm2 As New TCONA402  ' PRODUCTOS.
        ' MyFrm3 As New TCONA403  ' Vista Ampliada Lista Productos (Zoom).
        ' MyFrm4 As New TCONA404  ' Cambio / Cobros
        ' MyFrm5 As New TCONA405  ' Z, X, Ref. Gen, Etc.
        ' MyFrm6 As New TCONA406  ' Referencias Generales.
        ' MyFrm7 As New TCONA407  ' Mantenimiento de COMBINADOS.
        ' MyFrm8 As New TCONA408  ' Pantalla COMBINADOS.
        ' MyFrm9 As New TCONA409  ' Pantalla Pedir VENDEDOR.
        ' MyFrm10 As New TCONA410 ' Mantenimientos Varios (Menu.)
        ' MyFrm11 As New TCONA411 ' Consulta Movimientos MESAS
        ' MyFrm12 As New TCONA412 ' Cambio de Mesa
        ' MyFrm13 As New TCONA413 ' Separar Cuenta ...
        ' MyFrm14 As New TCONA414 ' Mesas Ocupadas x Salas
        ' MyFrm15 As New TCONA415 ' "Alf./ Num."
        ' MyFrm16 As New TCONA416 ' Botón Enviar ( Visualiza Datos a Areas )
        ' MyFrm18 As New TCONA418 ' Mensajes a AREAS (COCINA, Etc.)
        ' MyFrm19 As New TCONA419 ' Eleccion del Nro. Caja para el TPV.
        ' MyFrm20 As New TCONA420 ' Mant. de Claves.
        ' MyFrm21 As New TCONA421 ' Pedidos a Domicilio.
        ' MyFrm22 As New TCONA421 ' Clientes Contado/Crédito (Factura A4).
        '
        Select Case wMiFormName
            Case "MyFrm1"
                '
                '   Definimos las carácteristicas de TCONA401
                '   Salas / Mesas
                '
                With MyFrm1
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA401
                    .KeyPreview = True
                End With
            Case "MyFrm2"
                '
                '   Definimos las carácteristicas de TCONA402
                '   Familias / Articulos
                '
                With MyFrm2
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA402
                    .KeyPreview = True
                    '
                    If FormularioInicial = 1 Then
                        .ButtonAPARCAR.Enabled = False
                        .ButtonAPARCAR.Visible = False
                        .ButtonCABE.Enabled = False
                        .ButtonCABE.Visible = True
                    Else
                        .ButtonAPARCAR.Enabled = True
                        .ButtonAPARCAR.Visible = True
                        .ButtonCABE.Enabled = False
                        .ButtonCABE.Visible = False
                    End If
                End With
            Case "MyFrm3"
                '
                '   Definimos las carácteristicas de TCONA403
                '   Form Zoom GRID1
                '
                With MyFrm3
                    .Width = 700
                    .Height = 700
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA402
                    .KeyPreview = True
                    '
                    ' Lista Ampliada...
                    '
                    With .GRIDZOOM
                        .Height = 578
                        .Width = 661
                    End With
                End With
            Case "MyFrm4"
                '
                '   Definimos las carácteristicas de TCONA404
                '   Form CAMBIO (COBROS)
                '
                With MyFrm4
                    .Width = 625
                    .Height = 643
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA404
                    .KeyPreview = True
                End With
            Case "MyFrm5"
                '
                '   Definimos las carácteristicas de TCONA405
                '   Form X, Z, Ref. Generales
                '
                With MyFrm5
                    .Width = 600
                    .Height = 540
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    '.BackColor = WcolFondoTCONA404
                    .KeyPreview = True
                End With
            Case "MyFrm6"
                '
                '   Definimos las carácteristicas de TCONA406
                '   Form Ref. Generales
                '
                Dim btn As New DataGridViewButtonColumn()
                With MyFrm6
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA404
                    .KeyPreview = True
                    '
                    ' En la tercera Columna del GRID, definimos un Botón al uso.
                    '
                    btn.HeaderText = "Seleccione Color..."
                    btn.Text = " ... "
                    btn.Name = "GridBtn"
                    btn.UseColumnTextForButtonValue = True
                    .GRIDColores.Columns.Add(btn)
                End With
            Case "MyFrm7"
                '
                '   Definimos las carácteristicas de TCONA407
                '   Form Mantenimiento de COMBINADOS
                '
                With MyFrm7
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA404
                    .KeyPreview = True
                End With
            Case "MyFrm8"
                '
                '   Definimos las carácteristicas de TCONA408
                '   COMBINADOS
                '
                With MyFrm8
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
            Case "MyFrm9"
                '
                '   Definimos las carácteristicas de TCONA409
                '   Pedir Vendedor
                '
                With MyFrm9
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
                '
            Case "MyFrm10"
                '
                '   Definimos las carácteristicas de TCONA410
                '   Mant. Varios (Menu)
                '
                With MyFrm10
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
                '
            Case "MyFrm11"
                '
                '   Definimos las carácteristicas de TCONA411
                '   Consulta Movimientos MESAS
                '
                With MyFrm11
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
                '
            Case "MyFrm12"
                '
                '   Definimos las carácteristicas de TCONA412
                '   Cambio de MESA
                '
                With MyFrm12
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
                '
            Case "MyFrm13"
                '
                '   Definimos las carácteristicas de TCONA413
                '   Separar Cuenta
                '
                With MyFrm13
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
            Case "MyFrm14"
                '
                '   Definimos las carácteristicas de TCONA414
                '   Salas / Mesas Ocupadas
                '
                With MyFrm14
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA401
                    .KeyPreview = True
                End With
            Case "MyFrm15"
                '
                '   Definimos las carácteristicas de TCONA415
                '   Teclados
                '
                With MyFrm15
                    .Width = 1024
                    .Height = 485
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA402
                    .KeyPreview = True
                End With
                '
            Case "MyFrm16"
                '
                '   Definimos las carácteristicas de TCONA416
                '   Botón Enviar ( Visualiza Datos a Areas )
                '
                With MyFrm16
                    '.Width = 1024
                    '.Height = 485
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA401
                    .KeyPreview = True
                End With
                '
            Case "MyFrm17"
                '
                '   Definimos las carácteristicas de TCONA417
                '   Mant. de MODELOS de impresora
                '
                With MyFrm17
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA402
                    .KeyPreview = True
                End With
            Case "MyFrm18"
                '
                '   Definimos las carácteristicas de TCONA418
                '   Mensajes a AREAS
                '
                With MyFrm18
                    .Width = 1024
                    .Height = 845
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
            Case "MyFrm19"
                '
                '   Definimos las carácteristicas de TCONA419
                '   Eleccion del Nro. Caja para el TPV.
                '
                With MyFrm19
                    '.Width = 1024
                    '.Height = 845
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
            Case "MyFrm20"
                '
                '   Definimos las carácteristicas de TCONA420
                '   Mant. de Claves.
                '
                With MyFrm20
                    .Width = 1024
                    .Height = 768
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
            Case "MyFrm21"
                '
                '   Definimos las carácteristicas de TCONA421
                '   Pedidos a Domicilio.
                '
                With MyFrm21
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
            Case "MyFrm22"
                '
                '   Definimos las carácteristicas de TCONA422
                '   Clientes Contado/Crédito (Factura A4).
                '
                With MyFrm22
                    .Width = 1024
                    .Height = 805
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.FixedToolWindow
                    .ControlBox = False
                    .MinimizeBox = False
                    .ShowIcon = False
                    .Text = ""
                    .BackColor = WcolFondoTCONA408
                    .KeyPreview = True
                End With
        End Select
        '
    End Sub

    Public Function InfoOsVersion() As String
        '
        ' Obtener información del sistema operativo 
        '
        InfoOsVersion = ""
        '
        Dim consultaSQLSO As String = "SELECT * FROM Win32_OperatingSystem"
        Dim objSO As New ManagementObjectSearcher(consultaSQLSO)
        For Each info As ManagementObject In objSO.Get()
            '
            InfoOsVersion &= info.Properties("Caption").Value.ToString().Trim() & vbCrLf
            '
            InfoOsVersion &= "                           " & info.Properties("Version").Value.ToString() &
                " Service Pack " &
                info.Properties("ServicePackMajorVersion").Value.ToString() &
                "." &
            info.Properties("ServicePackMinorVersion").Value.ToString() & ", "
        Next info
        '
        ' Obtener número de cores del procesador 
        '
        Dim consultaSQLCores As String = "SELECT * FROM Win32_ComputerSystem"
        Dim objCores As New ManagementObjectSearcher(consultaSQLCores)
        For Each info As ManagementObject In objCores.Get()
            InfoOsVersion &= "Núcleos " & info.Properties("NumberOfLogicalProcessors").Value.ToString() & ", "
        Next info
        '
        ' obtener arquitectura procesador (64 bits 32 bits) 
        '
        Dim consultaSQLArquitectura As String = "SELECT * FROM Win32_Processor"
        Dim objArquitectura As New ManagementObjectSearcher(consultaSQLArquitectura)
        For Each info As ManagementObject In objArquitectura.Get()
            InfoOsVersion &= "Arquitectura " & info.Properties("AddressWidth").Value.ToString() & " bits."
        Next info
        '
    End Function

    Public Sub DatosLineaMyfrm2GRID1()
        '
        ' Procediemiento para depuración ...
        '
        TEMP = ""
        With MyFrm2.GRID1
            If .Rows.Count > 0 Then
                If .SelectedRows.Count > 0 Then
                    TEMP &= "0 Cod. Art         " & .SelectedCells(0).Value.ToString.Trim & vbCrLf
                    TEMP &= "1 Unid. Existentes " & .SelectedCells(1).Value.ToString.Trim & vbCrLf
                    TEMP &= "2 Descripcion      " & .SelectedCells(2).Value.ToString.Trim & vbCrLf
                    TEMP &= "3 Unid. Nuevas     " & .SelectedCells(3).Value.ToString.Trim & vbCrLf
                    TEMP &= "4 Importe          " & .SelectedCells(4).Value.ToString.Trim & vbCrLf
                    TEMP &= "5 Tipo E/N         " & .SelectedCells(5).Value.ToString.Trim & vbCrLf
                    TEMP &= "6 Cod. Combinados  " & .SelectedCells(6).Value.ToString.Trim & vbCrLf
                    TEMP &= "7 Raciones         " & .SelectedCells(7).Value.ToString.Trim & vbCrLf
                    TEMP &= "8 Nro. Plato       " & .SelectedCells(8).Value.ToString.Trim & vbCrLf
                End If
            End If
        End With
        MsgBox(TEMP)
        '
    End Sub

    Public Function Comprueba_Existe_MDF(wMidatabseMdf As String) As Boolean
        '
        ' Funcion para comprobar la existencia de determinadas
        '    bases de datos .mdf (SQL SERVER)
        ' Se comprueba aqui si los "archivos" .mdf existen o no.
        '
        Comprueba_Existe_MDF = True
        '
        If File.Exists("C:\TRIVAGES\DATOS\" & wMidatabseMdf) = False Then
            Comprueba_Existe_MDF = False
            msg = "Error en la Base de datos." & vbCrLf
            msg &= "C:\TRIVAGES\DATOS\" & wMidatabseMdf
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error, no existe la base de datos."
            MsgBox(msg, style, title)
        End If
        '
    End Function

End Module
