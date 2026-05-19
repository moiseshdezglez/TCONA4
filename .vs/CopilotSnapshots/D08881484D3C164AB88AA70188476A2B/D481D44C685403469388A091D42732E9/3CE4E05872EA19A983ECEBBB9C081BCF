Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Management

Module Tcona4Main
    '
    '   Main, La entrada Principal al PROYECTO TCONA4.
    '   Variables, Procedimientos y Funciones GLOBALES al PROYECTO.
    '
    Public MiFileExist As Boolean
    Public TEMP As String = ""                 ' Uso general (Depuración, Otros...)
    Public SwFoco_401 As Integer = 0           ' Foco Por Defecto al Visor
    Public SwFoco_402 As Integer = 2           ' Foco Por Defecto a Cod. Barras.
    Public SwFoco_404 As Integer = 4           ' Foco Por Defecto a Entrega (Cobros).
    Public SwINICIADO_412 As Boolean = False   ' Form Iniciado S/N
    Public TCONA418_Started As Boolean = False ' Usado Para Evento Activated.
    Public TCONA405_Started As Boolean = False ' Usado Para Evento Activated.
    Public TCONA406_Started As Boolean = False ' Usado Para Evento Activated.
    Public TCONA420_Started As Boolean = False ' Usado Para Evento Activated.
    Public NumMesasOcu As Integer = 0          ' Nro. de MESAS OCUPADAS.
    Public CodsMesasOcu As String = ""         ' Lista de MESAS OCUPADAS.
    Public TotImpoOCU As Double = 0            ' TOTAL Importe MESAS OCUPADAS.
    '
    ' Al Modificar un PVP en productos de la MESA abierta.
    '
    Public ModUnids As Double = 0
    Public ModImporte As Double = 0
    Public NewImpo As Double = 0
    '
    ' Calculos en la Cuenta de la Mesa.
    '
    Public MisUnidadesN As Decimal = 0
    Public MisUnidadesE As Decimal = 0
    Public MiImporte As Double = 0
    Public MiPVPMedio As Double = 0
    '
    Public BtnAccion As Integer = 0            ' Acciones del mantenimiento: Crear, Modificar, Eliminar Registros ...
    '
    ' Estos Campos Contendrán Temporalmente:
    '   La Cadena de Combinados "concatenados" (1245/3450/aaaa ... ),
    '   datos y el control relativos a Medias Raciones, etc, que forman 
    '   parte de la clave de los registros para la Tabla MESA.
    '
    Public wStringCombinados As String = " "
    Public wMediaPrecio As String = " "
    Public wMiBtnARTCombi As Button
    Public wMiPrecCombi As String
    Public queryStringC As String = ""
    '
    ' Pedir PAX  (TCONA402)
    '
    Public PidePAXOnOff As Boolean = False
    '
    ' Pedir (Cambiar) la TARIFA (TCONA402)
    '
    Public PideTARIFAOnOff As Boolean = False
    '
    ' Pedir Camarero (TCONA402)
    '
    Public PideVENDOnOff As Boolean = False
    '
    ' Raciones ( 1/2 Racion )
    '
    Public RacionOnOff As Boolean = False
    '
    ' VariosCeroOnOff : Precio = 0, Articulos de las Familias VARIOS.
    ' ModiPVPOnOff    : Modificar PVP a un Producto en la MESA.
    '
    Public VariosCeroOnOff As Boolean = False
    Public ModiPVPOnOff As Boolean = False
    '
    ' Control de Algunas de las Propiedades de detrminados
    ' FORMULARIOS.
    '
    Public myfrm4_Restaurado As Boolean = False
    Public MyFrm2_Height As Integer = MyFrm2.Height
    Public MyFrm2_Width As Integer = MyFrm2.Width
    Public MyFrm4_Height As Integer = MyFrm2.Height
    Public MyFrm4_Width As Integer = MyFrm2.Width
    Public MyFrm2_Top As Integer = MyFrm2.Top
    Public MyFrm2_Left As Integer = MyFrm2.Left
    Public MyFrm4_Top As Integer = MyFrm4.Top
    Public MyFrm4_Left As Integer = MyFrm4.Left
    '
    ' Conversiones y Formato
    ' Usado en GENERAL para Cantidades, Importes, Totales y Similares.
    '
    Public fmtCero As String = "0;0;0" ' Cuando es CERO.
    Public fmtUnid As String = "##0.00;-##0.00;00.00"
    Public fmtPrecio As String = "###0.00;-###0.00;00.00"
    Public fmtImporte As String = "####0.00;-####0.00;00.00"
    Public fmtTOTAL As String = "##000.00;-##000.00;000.00"
    Public fmtEMPRESA As String = "000;000;000"
    '
    ' Cálculos Varios ... ... ...
    '
    Public wDUnid1 As Decimal = 0 : Public wDUnid2 As Decimal = 0 : Public wDUnid3 As Decimal = 0
    Public wDPrecio As Decimal = 0
    Public wDImporte As Decimal = 0
    Public wDTotal As Decimal = 0
    '
    ' COBROS / CAMBIO
    '
    Public OpenFrom As Integer = 0
    Public CALCULADO As Boolean = False
    Public CobroTOTALMesa As Decimal = 0
    Public CobroEfectivo As Decimal = 0
    Public CobroTarjetas As Decimal = 0
    Public CobroCheques As Decimal = 0
    Public CobroOtros As Decimal = 0
    Public CobroEntrega As Decimal = 0
    Public CobroCambio As Decimal = 0
    Public CobroActual As Decimal = 0
    Public CobroPendiente As Decimal = 0
    '
    ' Gobierno de VARIADOS aspectos de la aplicación en GENERAL.
    '
    Public ClaveModificar As String = ""
    Public TextoCalculadora As String = ""
    Public TextoVisorPassword As String = ""
    Public HaySalas As Boolean = False
    Public HayFamilias As Boolean = False
    Public HayFamCombi As Boolean = False
    Public HayCombinados As Boolean = False
    Public HayGF As Boolean = False ' Familias del Grupo
    Public HayGruposCombi As Boolean = False
    Public FiltroFami As Integer = 2 ' A*
    Public FiltroArti As Integer = 2 ' A*
    Public SwModSala As Boolean = False
    Public RetenNumZeta As Integer = 0
    Public RetenFecZeta As String = ""
    Public TimerTick As Boolean = False
    '
    ' BARRA 1 // BARRA 2 ( TARIFA )
    '
    Public wTarifaBarra As Integer = 0
    '
    ' Relativo a FOVORITOS
    '
    Public wCodFABOGrupo As String = ""
    Public wCodFABOArti As String = ""
    '
    Public CursorGRID1 As Integer = 0               ' Cursor Movimiento Lineas Grid Arr./Abajo
    Public FormOcuIni As Boolean = False            ' Form MESAS INICIADO
    Public FormOcuON As Boolean = False             ' Form MESAS OCUPADAS INICIADO
    Public SwEntraMesa As Boolean = False
    '
    ' Gobierno de Calculos Para la CUENTA en FORM Productos.
    ' Controlarán UNIDADES EXISTENTES, NUEVAS, Etc ...
    '
    Public wImporteN As Double = 0
    Public wUnidN As Double = 0
    Public wUnidE As Double = 0
    Public wTotalN As Double = 0
    '
    ' Consulta General
    '
    Public wTipoConsu As Integer = 3
    Public wTipoResuMES As Integer = 2
    '
    ' Botones [Adelante] y [Atras]
    '   FAMILIAS: Orden x Código, Orden x Nombre
    '
    Public FamAdelante As String = ""
    Public FamAtras(500) As String
    Public FamCodAdelante As String = ""
    Public FamCodAtras(500) As String
    '
    ' Botones [Adelante] y [Atras]
    '   ARTICULOS: Orden x Código, Orden x Nombre
    '
    Public ArtAdelante As String = ""
    Public ArtAtras(500) As String
    Public ArtCodAdelante As String = ""
    Public ArtCodAtras(500) As String
    '
    ' Indice Botoneras ...
    '
    Public wIndFAMAtras As Integer
    Public wIndARTAtras As Integer
    '
    '   MessageBox (MsgBox)
    '   Para los Mensajes en general en la Aplicación.
    '
    Public msg As String             ' Mensaje al Usuario.
    Public title As String           ' Título del Mensaje.
    Public style As MsgBoxStyle      ' Define el estilo de MesageBox.
    Public response As MsgBoxResult  ' Recogerá Respuestas del Usuario desde MesageBox.
    '
    ' Colores Predefinidos.
    '    - FONDO "BACKCOLOR" en Determinados FORMULARIOS.
    '    - COLORES LETRAS.
    '    - COLORES "BACKCOLOR" de Determinados Controles.
    ' Algunos de estos Colores estarán Configurados y se recogerán
    '  desde Ref. Generales.
    '
    Public CambioColores As Boolean = False ' Informa a Ref. Generales si se han hecho cambio de Colores.
    Public WcolBtnVendedoresF As Color = Color.White
    Public WcolBtnVendedoresB As Color = Color.DarkBlue
    Public WcolDefFondo As Color = Color.FromArgb(224, 224, 224)
    Public WcolFondoTCONA401 As Color = TCONA401.BackColor
    Public WcolFondoTCONA402 As Color = TCONA402.BackColor
    Public WcolFondoTCONA404 As Color = TCONA404.BackColor ' COBROS/CAMBIO
    Public WcolFondoTCONA408 As Color = TCONA408.BackColor ' COMBINADOS
    Public WcolFF As Color = TCONA402.ButtonFAM1.BackColor
    Public WcolLF As Color = TCONA402.ButtonFAM1.ForeColor
    Public WcolFA As Color = TCONA402.ButtonART1.BackColor
    Public WcolLA As Color = TCONA402.ButtonART1.ForeColor
    Public WcolReservar As Color = TCONA402.ButtonART1.BackColor
    Public WcolFocoCobros As Color = Color.FromArgb(255, 192, 192)
    '
    Public wcolLineasNuevas As Color = Color.AliceBlue     ' Color GRID1, Nuevas
    Public wcolLineasDefecto As Color = SystemColors.Info  ' Color GRID1, Por Defecto
    '
    ' Coleccion de Controles (Normalmente Botonesras)
    '
    Public wControl As Control     ' Gestiona la coleccion de controles
    Public wTempArtBoton As Button ' Control Boton, Articulos Pulsado.
    Public wTempFamBoton As Button ' Control Boton, Familia Pulsado.
    Public NombreBoton As String   ' Texto de Botones.
    '
    '   Tabla de uso general.
    '
    Public TblTPVS As SqlDataAdapter = Nothing
    Public TblTPVS1 As SqlDataAdapter = Nothing

    Sub Main()
        '
        Try
            '----------------------------------------------------------------------------------------------------------------
            ' Se permite solo una instancia de esta aplicación en la maquina.
            ' Si es necesario, se implementa esta característica.
            ' Ojo!, de implementarse hay que evaluar el nivel restrictivo:
            ' \local, (Varios Usuarios, una instancia por cada usuario.
            '         Entornos tipo Termial Services)
            ' \global (Unica instancia por máquina, PC)
            '
            '->
            'Dim esCreadoNuevo As Boolean
            'Dim instanciaMutex As System.Threading.Mutex = New System.Threading.Mutex(True, Application.ProductName, esCreadoNuevo)
            'If esCreadoNuevo = False Then
            'msg = "Solo se permite una instancia de" & vbCrLf
            'msg &= "esta aplicación en este PC."
            'style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            'title = "La Aplicación ya está ejecutandose."
            'MsgBox(msg, style, title)
            'System.Environment.ExitCode = 7
            'System.Environment.Exit(7)
            'End If
            '<-
            '----------------------------------------------------------------------------------------------------------------
            ' El nro de caja. se la pasa a la apliacion como un parámetro.
            ' De no ser así, ASIGNAMOS Caja=01 por defecto.
            ' NOTA: Se espera recibir solo un parámetro en este caso.
            '       Si se desean recibir otros parametros, se reescribirá esta rutina
            '       mediante el uso de 'SPLIT'y teniendo en cuenta un separador, por ej. "coma (,)"
            '
            wCaja = 1
            If Len(Command$()) > 0 And Not IsNothing(Command$()) Then
                If IsNumeric(Command$()) Then
                    wCaja = CInt(Command$())
                End If
            End If
            '
            ' Leer algun(os) Valor(es) desde el Reg. de Windows.
            '
            ValoresRegWindows()
            '
            '   String Conexión, Configuración, Etc.
            '   Solución Eventual a la cadena de conexión AL SERVER.
            '   Nota.: Se esta externalizando en "C:\TRIVAGES\DATOS\CONA4Cfg.txt" 
            '          la INSTANCIA.
            '
            CargaConfig()
            '
            ' Para las conexiones en PC-FARI, Depuracion y desarrollo!
            ' Se solicita Password para no comprometer la seguridad de mis Bases de datos.
            ' SOLO CON FINES DE DEPURACION!
            '
            If SQL_Instancia = "Data Source=DESKTOP-FARI\SQLEXPRESS_2014;" Then
                Dim MyValue As String = InputBox("MODO DEPURACIÓN!" & vbCrLf & "Esc para abandonar.", "Password requerida para conexion SQL.")
                If MyValue.Trim.Length = 0 Then
                    ' Abandono del entorno (Aplicación)
                    System.Environment.ExitCode = 7
                    System.Environment.Exit(7)
                End If
                SQL_Seguridad_Otros = "Integrated Security=False;User ID=sa;Password=" & MyValue & ";
                                       Connect Timeout=15;
                                       Encrypt=False;
                                       TrustServerCertificate=True;
                                       ApplicationIntent=ReadWrite;
                                       MultiSubnetFailover=False"
            End If
            '
            ' Conexión Almacen [GESTITRV.mdf]
            SQL_CadenaConexion = SQL_Instancia & SQL_Catalogo & SQL_Seguridad_Otros
            ' Conexión  Contabilidad [CONTATRVnnn.mdf]
            SQL_CadenaConexion1 = SQL_Instancia & SQL_Catalogo1 & SQL_Seguridad_Otros
            '
            ' Comprobación Adicional existencia de bases de datos
            '    SOLO para Instalaciones TRIVALLE.
            ' En entornos diferentes no tendrá validez.
            ' Dado que se espera que la Instancia sea = 
            '    "1:Data Source=SERVIDOR\SQLTRV;"
            ' Y el directorio para las bases de datos sea = 
            '    "C:\TRIVAGES\DATOS"
            '
            ' EN MODO DEPURACION ESTO SE SALTA!
            '
            If SQL_Instancia = "1:Data Source=SERVIDOR\SQLTRV;" Then
                If Comprueba_Existe_MDF("GESTITRV.mdf") = False Then
                    '
                    ' Abandono del entorno (Aplicación)
                    '
                    System.Environment.ExitCode = 7
                    System.Environment.Exit(7)
                End If
                '
                ' Comprobamos solamente CONTATRV001.mdf
                ' Se espera que al menos este archivo exista!
                '
                'If Comprueba_Existe_MDF("CONTATRV001.mdf") = False Then
                'System.Environment.ExitCode = 7
                'System.Environment.Exit(7)
                'End If
            End If
            '
            GeneraTXTVarios()
            '
            '======================================================================
            ' La Llamada a este procedimiento desencadena ciertas acciones
            '  tanto al entrar a la aplicacón, como al realizar un cambio de caja.
            ' 0/1 Indica que acciones segun en que escenario sea llamado.
            ' El encapsulado evita "desfase" en las correciones/implementaciones 
            '    en el código fuente.
            '
            HayCambiodeCaja(1)
            '
            '======================================================================
            '
            ' Iniciamos el FORM Primario, en funcion de la variable
            '  en Ref. Generales.
            '
            ' Si se quiere Poder seleccionar una caja determinada 
            '   al entrar, se gestiona desde aqui.
            ' Está determinado por Ref. Generales o bien
            ' PideCajas, el usuario solicita cambio de caja.
            '
            If wrLeeTCONA4.Tcona4_PIDECAJAINICIO = "True" Or PideCajas = True Then
                Application.Run(MyFrm19)
            Else
                Select Case FormularioInicial
                    Case 0
                        Application.Run(MyFrm1)
                    Case 1
                        Application.Run(MyFrm2)
                End Select
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " *** Errores al inciar la aplicación *** ")
        End Try
        '
    End Sub

    Public Sub ValoresRegWindows()
        ' 
        ' Leemos algun(os) VALOR(es) desde el registro de Windows.
        ' BD2RegKkeys(n) podremos redimensionar para usar mas variables...
        '
        Dim readValue As String : Dim BD2RegKkeys(0) As String
        PideCajas = False
        BD2RegKkeys(0) = "HKEY_CURRENT_USER\TCONA4\Cajas"
        Try
            If My.Computer.Registry.GetValue(BD2RegKkeys(0), "PideCaja", Nothing) Is Nothing Then
                My.Computer.Registry.SetValue(BD2RegKkeys(0), "PideCaja", "0")
            End If
            readValue = My.Computer.Registry.GetValue(BD2RegKkeys(0), "PideCaja", Nothing).ToString
            If readValue.Trim = "1" Then
                PideCajas = True
                My.Computer.Registry.SetValue(BD2RegKkeys(0), "PideCaja", "0")
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Error al actualizar Registro de Windows.")
        End Try
        '
    End Sub

    Public Sub HayCambiodeCaja(wOpc As Integer)
        '--------------------------------------------------------------------------------------------
        ' Acciones al CAMBIAR o Trabajar con un Nro. de Caja.
        ' Depende de la variable Global  wCaja.
        ' 
        ' Procedimiento encapsulado para reutilizar:
        '   - Al entrar a la aplicación.
        '   - Al realizar un cambio de caja.
        ' 
        ' wOpc :: 0/1 - Indica si ciertas acciones se realizan o no.
        '--------------------------------------------------------------------------------------------
        '
        '
        ' TCONA4, Tabla Ref. Generales para la aplicación.
        ' Aqui si no hay registro de Ref. Generales se crea uno para esta Caja.
        '
        ' LeeTCONA4Cfg, internamente hace una COMPROBACION de la CONEXIÓN.
        ' Si la instancia u otro factor no permiten conectar,
        '    informa de error de conexión y termina la aplicación.
        '
        If LeeTCONA4Cfg("General") = False Then
            CreaTCONA4Cfg(wCaja)
            LeeTCONA4Cfg("General")
        End If
        '
        ' Si es posible conectar, ya podemos entrar a las bases de datos.
        ' No obstante:
        ' El nombre lógico de las bases de datos, no tiene porque ser
        '    igual al nombre del "fichero.mdf".
        '
        ' Ejemplo.: GESTITRV.mdf, dbo.GESTION
        '
        ' Normalmente no serán distintos, pero...
        ' ... como dice el Sr. Jose Mota   " Y si sí?... "
        '
        If CompruebaNombreDB("GESTITRV") = False Then
            msg = "Error en la Base de datos." & vbCrLf
            msg &= "GESTITRV no hallado en la coleccion Bases de datos."
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error, no existe la base de datos."
            MsgBox(msg, style, title)
            '
            System.Environment.ExitCode = 7
            System.Environment.Exit(7)
        End If
        If CompruebaNombreDB("CONTATRV001") = False Then
            msg = "Error en la Base de datos." & vbCrLf
            msg &= "CONTATRV001 no hallado en la coleccion Tablas de datos."
            style = MsgBoxStyle.Exclamation Or
                    MsgBoxStyle.OkOnly
            title = "Error, no existe la base de datos."
            MsgBox(msg, style, title)
            '
            System.Environment.ExitCode = 7
            System.Environment.Exit(7)
        End If
        '
        ' Presentación (Splah Screen), Depende de Ref. Generales.
        ' Si/No, y Retardo en ms.
        ' SOLO al entrar a la Aplicacion, no en cambio de Caja.
        '
        If wOpc = 1 Then
            If wrLeeTCONA4.Tcona4_SPLASHSCREEN = "True" And PideCajas = False Then
                Dim MyMilliseconds As Integer = wrLeeTCONA4.Tcona4_SPLASHRETARDO
                If MyMilliseconds = 0 Then
                    MyMilliseconds = 2500
                End If
                SplashTCONA4.Show()
                SplashTCONA4.Refresh()
                Threading.Thread.Sleep(MyMilliseconds)
                SplashTCONA4.Hide()
            End If
        End If
        '
        ' La SALA 999 y MESA 999 son necesarias para el correcto 
        '    funcionamiento de la aplicación. 
        ' Por tanto si no existen la aplicación las creará automaticamente.
        '
        If LeeSALA("999") = False Then
            CreaSala999()
        End If
        If LeeMesa_SALA1("999", "999", 1) = False Then
            CreaMesa999()
        End If
        '
        ' Creamos una serie de NIVELES de ACCESO de uso
        '  común en la Aplicacion. Niveles 0 a 5.
        ' Solo si no existen en la Tabla.
        '
        For i = 0 To 5
            If LeeClaves(i) = False Then
                CreaClave(i)
            End If
        Next
        '
        ' Gestionamos FECHA "Z" en Ref. Generales.
        ' Intervinene dos fechas para la Z
        '    FECHA DIA -> Se establece Fecha actual, si tablas MESAC, MESA estan vaciás.
        '    FECHA Z ---> Se Pasará Fecha DIA a --> FECHA Z, al hacer la Z.
        '
        If MESACVacio() = True Then
            Actualiza_TCONA4(wCaja, "FecDia")
        End If
        '
        ' Definicion Propiedades de los Formularios.
        '
        ''  1 MESAS
        ''  2 ARTICULOS
        ''  3 Vista Ampliada GRID1 (ARTICULOS)
        ''  4 COBROS / CAMBIO
        ''  5 Form SubMemu: CABECERA, X, Z, Ref. Gen.
        ''  6 Referencias Generales
        ''  7 Mantenimiento de COMBINADOS
        ''  8 Pantalla para fichar COMBINADOS
        ''  9 Pantalla Pedir VENDEDORES
        '' 10 Mantenimientos Varios (Menu)
        '' 11 Consulta Movimientos MESAS
        '' 12 Cambio de  MESA
        '' 13 Separar Cuenta
        '' 14 Mesas Ocupadas x Salas
        '' 15 Tecclados "Alf./ Num."
        '' 16 Enviar ( Visualiza Datos a Areas )
        '' 17 Mant. de MODELOS de impresora
        '' 18 Mensajes a AREAS (COCINA, Etc.)
        '' 19 Eleccion del Nro. Caja para el TPV.
        '' 20 Mant. de Claves.
        '' 21 Pedidos a Domicilio.
        '' 22 Clientes Contado/Crédito (Factura A4).
        '
        ' SOLO al entrar a la Aplicacion, no en cambio de Caja.
        '
        If wOpc = 1 Then
            For i As Integer = 1 To 22
                DefineFORMULARIOS("MyFrm" & i.ToString.Trim)
            Next
        End If
        '
        ' Impresora de TICKETS Determinada Por Defecto 
        '   en Referencia Generales.
        ' La impresora deberá existir en la TABLA.
        ' ActualizaDatosImpresora, 
        '    inserta los comandos correctos para el MODELO DADO.
        '
        If wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim.Length > 0 Then
            If LeeDatosImpresora(wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim) = False Then
                '
                '(Para Depuración)
                'If LeeDatosImpresora(wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim) = True Then
                'ActualizaDatosImpresora(wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim)
                'Else
                '
                ' El MODELO de Impresora no esta creado en la Tabla
                ' IMPRESORAS, y damos un Aviso
                '
                msg = "Caja.: " & wCaja.ToString & " " & wrLeeTCONA4.Tcona4_NOMBRECAJA.Trim & vbCrLf & vbCrLf
                msg &= "El MODELO de Impresora " & vbCrLf
                msg &= wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim & vbCrLf
                msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
                style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                title = "Modelo de impresora no Creado."
                MsgBox(msg, style, title)
            End If
        Else
            '
            ' No hay MODELO de Impresora definido
            ' en Ref. Generales.
            '
            msg = "Caja.: " & wCaja.ToString & " " & wrLeeTCONA4.Tcona4_NOMBRECAJA.Trim & vbCrLf & vbCrLf
            msg &= "No hay un MODELO de Impresora " & vbCrLf
            msg &= "definido para la aplicación." & vbCrLf & vbCrLf
            msg &= "Se recomienda crear al menos un modelo o bien " & vbCrLf
            msg &= "utilice la función importar modelos de impresora." & vbCrLf & vbCrLf
            msg &= "Recuerde.: Si esta aperturando o accediendo" & vbCrLf
            msg &= "a una caja nueva, tal vez desee revisar" & vbCrLf
            msg &= "sus 'valores referenciales', para una correcta" & vbCrLf
            msg &= "configuración." & vbCrLf & vbCrLf
            style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
            title = "Modelo de impresora requerido en Ref. Generales."
            MsgBox(msg, style, title)
        End If
        '
        ' Comprobacion de trabajos pendientes para la impresora por defecto
        ' en windows. Depende de Rf. Generales.
        '
        If wrLeeTCONA4.Tcona4_COMPRUIMPREINI = "True" Then
            Dim MiImpDef As String = ObtenerImpresoraPredeterminada()
            If ImpresoraEstaONLINE(MiImpDef) = True Then
                If wrProp_IMPRESORA.JobCountSinceLastReset <> "0" Then
                    msg = "Atención, la impresora " & vbCrLf
                    msg &= MiImpDef & vbCrLf
                    msg &= "informa " & wrProp_IMPRESORA.JobCountSinceLastReset
                    msg &= " trabajos pendientes."
                    style = MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly
                    title = "Por favor compruebe la impresora."
                    MsgBox(msg, style, title)
                End If
            Else
                msg = "Atención, la impresora " & vbCrLf
                msg &= MiImpDef & vbCrLf
                msg &= "parece estar apagada." & vbCrLf & vbCrLf
                msg &= "Informa " & wrProp_IMPRESORA.JobCountSinceLastReset
                msg &= " trabajos pendientes."
                style = MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly
                title = "Por favor compruebe la impresora."
                MsgBox(msg, style, title)
            End If
        End If
        '
    End Sub

    Public Function LeeDatosImpresora(iModelo As String) As Boolean
        '
        ' Leemos las distintas secuencias ESC según Modelo Impresora 
        '   con la que vamos a Trabajar.
        ' Las secuencias ESC/POS vienen en capsuladas entre corchetes [ ]
        '   por tanto primero las desempaquetamos...
        '
        LeeDatosImpresora = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [IMPRESORAS] "
        queryString = queryString & "WHERE [IMPRESORAS].[IMPRESORA]='" & iModelo & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "IMPRESORAS")
            '
            If dt.Tables("IMPRESORAS").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeDatosImpresora = True
                For Each pRow In dt.Tables("IMPRESORAS").Rows
                    If pRow("IMPRESORA").ToString() = iModelo Then
                        '
                        ' Recogemos datos...
                        '
                        With wrIMPRESORA
                            '
                            ' Desempaquetado, quitamos "[" y "]"
                            '
                            .CORTE = UnPack(pRow("CORTE").ToString().Trim & "")
                            .ABRECAJON = UnPack(pRow("ABRECAJON").ToString().Trim & "")
                            .DIEZ_CPP = UnPack(pRow("DIEZCPP").ToString().Trim & "")
                            .DOBLEALTO = UnPack(pRow("DOBLEALTO").ToString().Trim & "")
                            .DOBLEALTO12CPP = UnPack(pRow("DOBLEALTO12CPP").ToString().Trim & "")
                            .DOBLEANCHO = UnPack(pRow("DOBLEANCHO").ToString().Trim & "")
                            .AVAZALINEA = UnPack(pRow("AVAZALINEA").ToString().Trim & "")
                            .AVAZAnLINEAS = UnPack(pRow("AVAZAnLINEAS").ToString().Trim & "")
                            .ATRASnLINEAS = UnPack(pRow("ATRASnLINEAS").ToString().Trim & "")
                            .DOCECPP = UnPack(pRow("DOCECPP").ToString().Trim & "")
                            .PROPORCIONAL = UnPack(pRow("PROPORCIONAL").ToString().Trim & "")
                            .COMPRIMIDO = UnPack(pRow("COMPRIMIDO").ToString().Trim & "")
                            .NEGRITA = UnPack(pRow("NEGRITA").ToString().Trim & "")
                            .CURSIVA = UnPack(pRow("CURSIVA").ToString().Trim & "")
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [IMPRESORAS]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Function UnPack(sUnpack As String) As String
        '
        ' Qquitamos los caracteres "[" y "]"
        '
        UnPack = ""
        For i As Integer = 1 To sUnpack.Length - 2
            UnPack &= sUnpack(i)
        Next
        '
    End Function

    Public Sub ActualizaDatosImpresora(iModelo As String)
        '
        ' Actualizamos Datos en un Modelo de IMPRESORA determinado
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        With wrIMPRESORA
            '
            ' INSERTAR / ACTUALIZAR
            '
            If LeeDatosImpresora(iModelo.Trim) = True Then
                '
                ' Recogemos las Nuevas ESC/POS
                '
                .CORTE = MyFrm17.LblCorte.Text
                .ABRECAJON = MyFrm17.LblCajon.Text
                .DIEZ_CPP = MyFrm17.Lbl10Cpp.Text
                .DOBLEALTO = MyFrm17.LblDblAlto.Text
                .DOBLEALTO12CPP = MyFrm17.LblDblAlto12Cpp.Text
                .DOBLEANCHO = MyFrm17.LblDblAncho.Text
                .AVAZALINEA = MyFrm17.LblAvance.Text
                .DOCECPP = MyFrm17.Lbl12Cpp.Text
                .PROPORCIONAL = MyFrm17.LblProporcional.Text
                .COMPRIMIDO = MyFrm17.LblCompri.Text
                .NEGRITA = MyFrm17.LblNegrita.Text
                .CURSIVA = MyFrm17.LblCursiva.Text
                '
                ' Nota: a estas secuencias añadimos en el código fuente 
                '    & chr(n), donde n=lineas
                ' Por ahora no le doy entrada del USUARIO...
                '
                .AVAZAnLINEAS = Chr(27) & Chr(100) & Chr(1)
                .ATRASnLINEAS = Chr(27) & Chr(101) & Chr(1)
                '
                ' Encapsulo el Código ESC/POS entre corchetes [ ]
                '
                queryString &= "UPDATE [IMPRESORAS] SET "
                queryString &= "[IMPRESORAS].[CORTE]='[" & .CORTE & "]', "
                queryString &= "[IMPRESORAS].[ABRECAJON]='[" & .ABRECAJON & "]', "
                queryString &= "[IMPRESORAS].[DIEZCPP]='[" & .DIEZ_CPP & "]', "
                queryString &= "[IMPRESORAS].[DOBLEALTO]='[" & .DOBLEALTO & "]', "
                queryString &= "[IMPRESORAS].[DOBLEALTO12CPP]='[" & .DOBLEALTO12CPP & "]', "
                queryString &= "[IMPRESORAS].[DOBLEANCHO]='[" & .DOBLEANCHO & "]', "
                queryString &= "[IMPRESORAS].[AVAZALINEA]='[" & .AVAZALINEA & "]', "
                queryString &= "[IMPRESORAS].[AVAZAnLINEAS]='[" & .AVAZAnLINEAS & "]', "
                queryString &= "[IMPRESORAS].[ATRASnLINEAS]='[" & .ATRASnLINEAS & "]', "
                queryString &= "[IMPRESORAS].[DOCECPP]='[" & .DOCECPP & "]', "
                queryString &= "[IMPRESORAS].[PROPORCIONAL]='[" & .PROPORCIONAL & "]', "
                queryString &= "[IMPRESORAS].[COMPRIMIDO]='[" & .COMPRIMIDO & "]', "
                queryString &= "[IMPRESORAS].[NEGRITA]='[" & .NEGRITA & "]', "
                queryString &= "[IMPRESORAS].[CURSIVA]='[" & .CURSIVA & "]' "
                queryString &= "WHERE "
                queryString &= "[IMPRESORAS].[IMPRESORA]='" & iModelo.Trim & "' "
            Else
                '
                ' Recogemos las Nuevas ESC/POS
                '
                .CORTE = MyFrm17.LblCorte.Text
                .ABRECAJON = MyFrm17.LblCajon.Text
                .DIEZ_CPP = MyFrm17.Lbl10Cpp.Text
                .DOBLEALTO = MyFrm17.LblDblAlto.Text
                .DOBLEALTO12CPP = MyFrm17.LblDblAlto12Cpp.Text
                .DOBLEANCHO = MyFrm17.LblDblAncho.Text
                .AVAZALINEA = MyFrm17.LblAvance.Text
                .DOCECPP = MyFrm17.Lbl12Cpp.Text
                .PROPORCIONAL = MyFrm17.LblProporcional.Text
                .COMPRIMIDO = MyFrm17.LblCompri.Text
                .NEGRITA = MyFrm17.LblNegrita.Text
                .CURSIVA = MyFrm17.LblCursiva.Text
                '
                ' Nota: a estas secuencias añadimos en el código fuente 
                '    & chr(n), donde n=lineas
                ' Por ahora no le doy entrada del USUARIO...
                '
                .AVAZAnLINEAS = Chr(27) & Chr(100) & Chr(1)
                .ATRASnLINEAS = Chr(27) & Chr(101) & Chr(1)
                '
                ' Encapsulo el Código ESC/POS entre corchetes [ ]
                '
                queryString = "Insert Into [IMPRESORAS] ("
                queryString = queryString & " [IMPRESORA],"
                queryString = queryString & " [CORTE],"
                queryString = queryString & " [ABRECAJON],"
                queryString = queryString & " [DIEZCPP],"
                queryString = queryString & " [DOBLEALTO],"
                queryString = queryString & " [DOBLEALTO12CPP],"
                queryString = queryString & " [DOBLEANCHO],"
                queryString = queryString & " [AVAZALINEA],"
                queryString = queryString & " [AVAZAnLINEAS],"
                queryString = queryString & " [ATRASnLINEAS],"
                queryString = queryString & " [DOCECPP],"
                queryString = queryString & " [PROPORCIONAL],"
                queryString = queryString & " [COMPRIMIDO],"
                queryString = queryString & " [NEGRITA],"
                queryString = queryString & " [CURSIVA]"
                '
                queryString = queryString & ") Values ("
                '
                queryString = queryString & "'" & iModelo & "', "
                queryString &= "'[" & .CORTE & "]', "
                queryString &= "'[" & .ABRECAJON & "]', "
                queryString &= "'[" & .DIEZ_CPP & "]', "
                queryString &= "'[" & .DOBLEALTO & "]', "
                queryString &= "'[" & .DOBLEALTO12CPP & "]', "
                queryString &= "'[" & .DOBLEANCHO & "]', "
                queryString &= "'[" & .AVAZALINEA & "]', "
                queryString &= "'[" & .AVAZAnLINEAS & "]', "
                queryString &= "'[" & .ATRASnLINEAS & "]', "
                queryString &= "'[" & .DOCECPP & "]', "
                queryString &= "'[" & .PROPORCIONAL & "]', "
                queryString &= "'[" & .COMPRIMIDO & "]', "
                queryString &= "'[" & .NEGRITA & "]', "
                queryString &= "'[" & .CURSIVA & "]' "
                '
                queryString = queryString & ")"
                '
            End If
        End With
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Grabando Datos [IMPRESORAS]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CreaSala999()
        '
        ' La aplicacion crea la Sala 999
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim wColFondo As Integer = -8355585 : Dim wColLetra As Integer = -256
        Dim queryString As String = ""
        queryString = "Insert Into [SALA] ("
        queryString = queryString & " [CAJA],"
        queryString = queryString & " [CODIGO],"
        queryString = queryString & " [NOMBRE],"
        queryString = queryString & " [COLORFONDO],"
        queryString = queryString & " [COLORTEXTO],"
        queryString = queryString & " [LOGO],"
        queryString = queryString & " [NOZETA],"
        queryString = queryString & " [NUMFAC],"
        queryString = queryString & " [COPIAFAC],"
        queryString = queryString & " [REPARTO],"
        queryString = queryString & " [PVP],"
        queryString = queryString & " [CLAVENOZETA]"
        '
        queryString = queryString & ") Values ("
        '
        queryString = queryString & wCaja & ","
        queryString = queryString & "'999',"
        queryString = queryString & "'MESAS SEPARADAS',"
        queryString = queryString & wColFondo & ","
        queryString = queryString & wColLetra & ","
        queryString = queryString & "'',"   ' LOGO
        queryString = queryString & 0 & "," ' NOZETA
        queryString = queryString & 0 & "," ' NUMFAC
        queryString = queryString & 0 & "," ' COPIAFAC
        queryString = queryString & 0 & "," ' REPARTO
        queryString = queryString & 0 & "," ' PVP
        queryString = queryString & "' '"   ' CLAVENOZETA
        '
        queryString = queryString & ")"
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Regsitro [SALA=999]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub CreaMesa999()
        '
        ' La aplicacion crea la Mesa 999
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        queryString = "Insert Into [SALA1] ("
        queryString = queryString & " [CAJA],"
        queryString = queryString & " [CODIGO],"
        queryString = queryString & " [MESA],"
        queryString = queryString & " [MESA1],"
        queryString = queryString & " [DESCMESA],"
        queryString = queryString & " [PVP],"
        queryString = queryString & " [PIDEPAX],"
        queryString = queryString & " [LOGO],"
        queryString = queryString & " [VISIBLE],"
        queryString = queryString & " [FACTURA],"
        queryString = queryString & " [FECAPERTURA],"
        queryString = queryString & " [VENDEDOR],"
        queryString = queryString & " [PAX], "
        queryString = queryString & " [HORAAPAERTURA],"
        queryString = queryString & " [IMPFACTU] "
        '
        queryString = queryString & ") Values ("
        '
        queryString = queryString & wCaja & "," ' CAJA
        queryString = queryString & "'999',"    ' SALA
        queryString = queryString & "'999',"    ' MESA
        queryString = queryString & "'999',"    ' MESA1
        queryString = queryString & "' ',"      ' DESCRIPCION 
        queryString = queryString & 0 & ","     ' PVP 
        queryString = queryString & 0 & ","     ' PIDEPAX (No=1)
        queryString = queryString & "'',"       ' LOGO
        queryString = queryString & 1 & ","     ' VISIBLE
        queryString = queryString & 0 & ","     ' FACTURA
        queryString = queryString & "'01/01/2000'," ' FECAPERTURA
        queryString = queryString & 0 & ","     ' VENDEDOR
        queryString = queryString & 0 & ", "    ' PAX
        queryString = queryString & "'00:00',"  ' HORAAPERTURA
        queryString = queryString & 0 & " "     ' IMPRIME FACTURA? 0=NO
        '
        queryString = queryString & ")"
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Regsitro [SALA1=999]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Function MESACVacio() As Boolean
        '
        ' Comprobamos si Tabla MESAC esta vacia, (sin registros), 
        '   obviamos que MESA también lo estará.
        '
        MESACVacio = True
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim queryString As String = "SELECT * FROM [MESAC] WHERE [MESAC].[NUMCAJA]=" & wCaja & " "
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESAC")
            '
            If dt.Tables("MESAC").Rows.Count > 0 Then
                MESACVacio = False
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

    Public Function LeeTCONA4Cfg(WTcona4OPC As String) As Boolean
        '
        '   Leemos valores de la tabla TCONA4 (Ref. Generales)
        '
        '   WTcona4OPC:
        '      Contador de Factura ----> "Factura" <- Importante, Contador Factura!!! (wFacturaN)
        '      Número   de Almacen ----> "Almacen" <- Almacen ...
        '      Datos en General -------> "General" ' Se usará por defecto.
        '                                            Gestiona COLORES tambien aqui ...
        '                                            HAGA LO QUE HAGA AQUI
        '                                            NUNCA VARIAR VALOR DE LA VARIABLE FACTURA (wFacturaN) !!!!
        '
        '
        LeeTCONA4Cfg = False
        Dim conexion As New SqlConnection
        '
        Try
            conexion.ConnectionString = SQL_CadenaConexion
            conexion.Open()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar la Instancia.")
            System.Environment.ExitCode = 7
            System.Environment.Exit(7)
            Application.Exit()
        End Try
        '
        Dim queryString As String = "SELECT * FROM [TCONA4] "
        queryString = queryString & "WHERE [TCONA4].[CAJA]=" & wCaja
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "TCONA4")
            '
            If dt.Tables("TCONA4").Rows.Count > 0 Then
                LeeTCONA4Cfg = True
                Dim pRow As DataRow
                For Each pRow In dt.Tables("TCONA4").Rows
                    '
                    ' Ref. Generales de la Caja Actual
                    '
                    If CInt(pRow("CAJA").ToString) = wCaja Then
                        '
                        ' En cada Lectura a TCONA4, indico que datos quiero gestionar.
                        ' NOTA: En teoría bastaría con usar "General", las demas opciones
                        '       se mantien por compatibilidad, porque ya se estaban usando,
                        '       y "General" se implementó mas tarde...
                        '
                        ' En ese sentido "Factura" es IMPORTANTE !!!! y se debe mantener.
                        '
                        Select Case WTcona4OPC
                            Case "Factura"
                                '
                                ' Nro. de Factura.
                                '
                                If Not IsDBNull(pRow("FACTURA")) Then
                                    wFacturaN = CInt(pRow("FACTURA").ToString)
                                Else
                                    wFacturaN = 0
                                End If
                            Case "Almacen"
                                '
                                ' Nro. de Almacen
                                '
                                If Not IsDBNull(pRow("ALMACEN")) AndAlso
                                    pRow("ALMACEN").ToString IsNot "0" Then
                                    wAlmacen = pRow("ALMACEN").ToString
                                Else
                                    wAlmacen = "1"
                                End If
                            Case "General"
                                '
                                ' Lectura de Datos en General.
                                '
                                With wrLeeTCONA4
                                    .Tcona4_NOMBRECAJA = pRow("NOMBRECAJA").ToString & ""
                                    '
                                    '  *** COLORES DE LA APLICACION ***
                                    '
                                    If Not IsDBNull(pRow("COLORFTCONA401")) Then
                                        WcolFondoTCONA401 = Color.FromArgb(CInt(pRow("COLORFTCONA401")))
                                        .Tcona4_COLORFTCONA401 = WcolFondoTCONA401
                                        MyFrm1.BackColor = WcolFondoTCONA401
                                    End If
                                    '
                                    If Not IsDBNull(pRow("COLORFTCONA402")) Then
                                        WcolFondoTCONA402 = Color.FromArgb(CInt(pRow("COLORFTCONA402")))
                                        .Tcona4_COLORFTCONA402 = WcolFondoTCONA402
                                        MyFrm2.BackColor = WcolFondoTCONA402
                                    End If
                                    '
                                    If Not IsDBNull(pRow("COLORFF")) Then
                                        WcolFF = Color.FromArgb(CInt(pRow("COLORFF")))
                                        .Tcona4_COLORFF = WcolFF
                                    End If
                                    '
                                    If Not IsDBNull(pRow("COLORLF")) Then
                                        WcolLF = Color.FromArgb(CInt(pRow("COLORLF")))
                                        .Tcona4_COLORLF = WcolLF
                                    End If
                                    '
                                    If Not IsDBNull(pRow("COLORFA")) Then
                                        WcolFA = Color.FromArgb(CInt(pRow("COLORFA")))
                                        .Tcona4_COLORFA = WcolFA
                                    End If
                                    '
                                    If Not IsDBNull(pRow("COLORLA")) Then
                                        WcolLA = Color.FromArgb(CInt(pRow("COLORLA")))
                                        .Tcona4_COLORLA = WcolLA
                                    End If
                                    '
                                    ' Empresa
                                    '
                                    If Not IsDBNull(pRow("EMPRESA")) AndAlso
                                    pRow("EMPRESA").ToString IsNot "0" Then
                                        wEmpresa = CInt(pRow("EMPRESA").ToString)
                                        .Tcona4_EMPRESA = CInt(pRow("EMPRESA").ToString)
                                    Else
                                        wEmpresa = 1
                                        .Tcona4_EMPRESA = 1
                                    End If
                                    '
                                    ' IGIC
                                    '
                                    If Not IsDBNull(pRow("IGIC")) AndAlso
                                    pRow("IGIC").ToString IsNot "0" Then
                                        .Tcona4_IGIC = CDbl(pRow("IGIC").ToString)
                                    Else
                                        .Tcona4_IGIC = 0
                                    End If
                                    '
                                    ' Nro. de Factura.
                                    '-----------------
                                    ' para Evitar PROBLEMAS CON LA VARIABLE wFacturaN, 
                                    '    NO SE DEBE CARGAR AQUI....
                                    ' Gestiona el contador de FACTURAS en la aplicación.
                                    ' Y depende SIEMPRE del estado de la MESA Actual...
                                    '
                                    If Not IsDBNull(pRow("FACTURA")) Then
                                        .Tcona4_FACTURA = CInt(pRow("FACTURA").ToString)
                                    Else
                                        .Tcona4_FACTURA = 0
                                    End If
                                    '
                                    ' Nro. de Almacen
                                    '
                                    If Not IsDBNull(pRow("ALMACEN")) AndAlso
                                    pRow("ALMACEN").ToString IsNot "0" Then
                                        wAlmacen = pRow("ALMACEN").ToString
                                        .Tcona4_ALMACEN = CInt(pRow("ALMACEN").ToString)
                                    Else
                                        wAlmacen = "1"
                                        .Tcona4_ALMACEN = 1
                                    End If
                                    '
                                    ' ORDEN Familias
                                    '
                                    If Not IsDBNull(pRow("ORDENFAM")) Then
                                        .Tcona4_ORDENFAM = pRow("ORDENFAM").ToString
                                    Else
                                        .Tcona4_ORDENFAM = "False"
                                    End If
                                    '
                                    ' ORDEN Articulos
                                    '
                                    If Not IsDBNull(pRow("ORDENART")) Then
                                        .Tcona4_ORDENART = pRow("ORDENART").ToString
                                    Else
                                        .Tcona4_ORDENART = "False"
                                    End If
                                    '
                                    '  Formulario INICIAL
                                    '
                                    If Not IsDBNull(pRow("FORMINICIAL")) AndAlso
                                    pRow("FORMINICIAL").ToString IsNot "0" Then
                                        wAlmacen = pRow("FORMINICIAL").ToString
                                        .Tcona4_FORMINICIAL = CInt(pRow("FORMINICIAL").ToString)
                                        FormularioInicial = .Tcona4_FORMINICIAL
                                    Else
                                        FormularioInicial = 0
                                        .Tcona4_FORMINICIAL = 0
                                    End If
                                    '
                                    ' Logo Ticket Factura
                                    '
                                    If Not IsDBNull(pRow("TKFACLOGO")) Then
                                        .Tcona4_TKFACLOGO = pRow("TKFACLOGO").ToString
                                    Else
                                        .Tcona4_TKFACLOGO = "False"
                                    End If
                                    '
                                    ' Logo Ticket ZETA
                                    '
                                    If Not IsDBNull(pRow("TKZETALOGO")) Then
                                        .Tcona4_TKZETALOGO = pRow("TKZETALOGO").ToString
                                    Else
                                        .Tcona4_TKZETALOGO = "False"
                                    End If
                                    '
                                    ' Líneas Cabecera TK Factura
                                    '
                                    .Tcona4_TKFCABLI1 = pRow("TKFCABLIN1").ToString & ""
                                    .Tcona4_TKFCABLI2 = pRow("TKFCABLIN2").ToString & ""
                                    .Tcona4_TKFCABLI3 = pRow("TKFCABLIN3").ToString & ""
                                    .Tcona4_TKFCABLI4 = pRow("TKFCABLIN4").ToString & ""
                                    .Tcona4_TKFCABLI5 = pRow("TKFCABLIN5").ToString & ""
                                    .Tcona4_TKFCABLI6 = pRow("TKFCABLIN6").ToString & ""
                                    .Tcona4_TKFCABLI7 = pRow("TKFCABLIN7").ToString & ""
                                    .Tcona4_TKFCABLI8 = pRow("TKFCABLIN8").ToString & ""
                                    .Tcona4_TKFCABLI9 = pRow("TKFCABLIN9").ToString & ""
                                    .Tcona4_TKFCABLI10 = pRow("TKFCABLIN10").ToString & ""
                                    '
                                    ' Líneas Cabecera TK Factura
                                    '
                                    .Tcona4_TKFPIELI1 = pRow("TKFPIELIN1").ToString & ""
                                    .Tcona4_TKFPIELI2 = pRow("TKFPIELIN2").ToString & ""
                                    .Tcona4_TKFPIELI3 = pRow("TKFPIELIN3").ToString & ""
                                    .Tcona4_TKFPIELI4 = pRow("TKFPIELIN4").ToString & ""
                                    .Tcona4_TKFPIELI5 = pRow("TKFPIELIN5").ToString & ""
                                    .Tcona4_TKFPIELI6 = pRow("TKFPIELIN6").ToString & ""
                                    .Tcona4_TKFPIELI7 = pRow("TKFPIELIN7").ToString & ""
                                    .Tcona4_TKFPIELI8 = pRow("TKFPIELIN8").ToString & ""
                                    .Tcona4_TKFPIELI9 = pRow("TKFPIELIN9").ToString & ""
                                    .Tcona4_TKFPIELI10 = pRow("TKFPIELIN10").ToString & ""
                                    .Tcona4_TKFPIELI11 = pRow("TKFPIELIN11").ToString & ""
                                    .Tcona4_TKFPIELI12 = pRow("TKFPIELIN12").ToString & ""
                                    .Tcona4_TKFPIELI13 = pRow("TKFPIELIN13").ToString & ""
                                    .Tcona4_TKFPIELI14 = pRow("TKFPIELIN14").ToString & ""
                                    .Tcona4_TKFPIELI15 = pRow("TKFPIELIN15").ToString & ""
                                    .Tcona4_TKFPIELI16 = pRow("TKFPIELIN16").ToString & ""
                                    .Tcona4_TKFPIELI17 = pRow("TKFPIELIN17").ToString & ""
                                    .Tcona4_TKFPIELI18 = pRow("TKFPIELIN18").ToString & ""
                                    .Tcona4_TKFPIELI19 = pRow("TKFPIELIN19").ToString & ""
                                    .Tcona4_TKFPIELI20 = pRow("TKFPIELIN20").ToString & ""
                                    '
                                    ' % IGIC TICKET FACTURA
                                    '
                                    If Not IsDBNull(pRow("TKFACIGIC")) AndAlso
                                    pRow("TKFACIGIC").ToString IsNot "0,000" Then
                                        .Tcona4_TKFACIGIC = pRow("TKFACIGIC").ToString
                                    Else
                                        .Tcona4_TKFACIGIC = "0,00"
                                    End If
                                    '
                                    ' Pide Vendedores S/N (True/False)
                                    '
                                    If Not IsDBNull(pRow("PIDEVENDEDOR")) Then
                                        .Tcona4_PIDEVENDEDOR = pRow("PIDEVENDEDOR").ToString
                                    Else
                                        .Tcona4_PIDEVENDEDOR = "False"
                                    End If
                                    '
                                    ' Separa RACIONES?
                                    '
                                    If Not IsDBNull(pRow("SEPARARACIONES")) Then
                                        .Tcona4_SEPARARACIONES = pRow("SEPARARACIONES").ToString
                                    Else
                                        .Tcona4_SEPARARACIONES = "False"
                                    End If
                                    '
                                    ' Raciones FAM1 / 2 / 3
                                    '
                                    .Tcona4_VARIOSFAM1 = pRow("VARIOSFAM1").ToString & ""
                                    .Tcona4_VARIOSFAM2 = pRow("VARIOSFAM2").ToString & ""
                                    .Tcona4_VARIOSFAM3 = pRow("VARIOSFAM3").ToString & ""
                                    '
                                    ' Nro. de X
                                    '
                                    If Not IsDBNull(pRow("NUMX")) AndAlso
                                    pRow("NUMX").ToString IsNot "0" Then
                                        .Tcona4_NUMX = CInt(pRow("NUMX").ToString)
                                    Else
                                        .Tcona4_NUMX = 1
                                    End If
                                    '
                                    ' Nro. de X
                                    '
                                    If Not IsDBNull(pRow("NUMZ")) AndAlso
                                    pRow("NUMZ").ToString IsNot "0" Then
                                        .Tcona4_NUMZ = CInt(pRow("NUMZ").ToString)
                                    Else
                                        .Tcona4_NUMZ = 1
                                    End If
                                    '
                                    ' Refrescar Botonera Familias/Articulos?
                                    '
                                    If Not IsDBNull(pRow("REFRESCABOTONES")) And
                                        pRow("REFRESCABOTONES").ToString = "True" Then
                                        .Tcona4_REFRESCABOTONES = pRow("REFRESCABOTONES").ToString
                                    Else
                                        .Tcona4_REFRESCABOTONES = "False"
                                    End If
                                    '
                                    ' Tipo Precio Articulo PVP1 a PVP9
                                    '
                                    If Not IsDBNull(pRow("TIPOPVPARTI")) Then
                                        .Tcona4_TIPOPVPARTI = CInt(pRow("TIPOPVPARTI").ToString)
                                    Else
                                        .Tcona4_TIPOPVPARTI = 1
                                    End If
                                    '
                                    ' Permite Z Con mesas OCUPADAS?
                                    '
                                    If Not IsDBNull(pRow("ZETAMESASOCU")) Then
                                        .Tcona4_ZETAMESASOCU = pRow("ZETAMESASOCU").ToString
                                    Else
                                        .Tcona4_ZETAMESASOCU = "False"
                                    End If
                                    '
                                    ' Fecha Z, Fecha Dia
                                    '
                                    If IsDBNull(pRow("FECHAZ")) Then
                                        .Tcona4_FECHAZ = Date.Now.ToShortDateString
                                    Else
                                        .Tcona4_FECHAZ = Format(pRow("FECHAZ"), "dd/MM/yyyy")
                                    End If
                                    '
                                    If IsDBNull(pRow("FECHADIASESION")) Then
                                        .Tcona4_FECHADIASESION = Date.Now.ToShortDateString
                                    Else
                                        .Tcona4_FECHADIASESION = Format(pRow("FECHADIASESION"), "dd/MM/yyyy")
                                    End If
                                    '
                                    ' Splah Screen ...
                                    '
                                    If Not IsDBNull(pRow("SPLASHSCREEN")) Then
                                        .Tcona4_SPLASHSCREEN = pRow("SPLASHSCREEN").ToString
                                    Else
                                        .Tcona4_SPLASHSCREEN = "False"
                                    End If
                                    '
                                    ' Splah Screen Retardo
                                    '
                                    If Not IsDBNull(pRow("SPLASHRETARDO")) AndAlso
                                    pRow("SPLASHRETARDO").ToString IsNot "0" Then
                                        .Tcona4_SPLASHRETARDO = CInt(pRow("SPLASHRETARDO").ToString)
                                    Else
                                        .Tcona4_SPLASHRETARDO = 2000
                                    End If
                                    '
                                    ' Splah Screen ...
                                    '
                                    If Not IsDBNull(pRow("PIDEPAX")) Then
                                        .Tcona4_PIDEPAX = pRow("PIDEPAX").ToString
                                    Else
                                        .Tcona4_PIDEPAX = "False"
                                    End If
                                    '
                                    ' FAVORITOS
                                    '
                                    If Not IsDBNull(pRow("CARGAFAVORITOS")) Then
                                        .Tcona4_CARGAFAVORITOS = pRow("CARGAFAVORITOS").ToString
                                    Else
                                        .Tcona4_CARGAFAVORITOS = "False"
                                    End If
                                    .Tcona4_BOTONFAVORITO = pRow("BOTONFAVORITO").ToString & ""
                                    '
                                    ' Nombre Tarifas
                                    '
                                    .Tcona4_NOMTARIPVP1 = pRow("NOMTARIPVP1").ToString & ""
                                    .Tcona4_NOMTARIPVP2 = pRow("NOMTARIPVP2").ToString & ""
                                    .Tcona4_NOMTARIPVP5 = pRow("NOMTARIPVP5").ToString & ""
                                    .Tcona4_NOMTARIPVP6 = pRow("NOMTARIPVP6").ToString & ""
                                    .Tcona4_NOMTARIPVP7 = pRow("NOMTARIPVP7").ToString & ""
                                    .Tcona4_NOMTARIPVP8 = pRow("NOMTARIPVP8").ToString & ""
                                    .Tcona4_NOMTARIPVP9 = pRow("NOMTARIPVP9").ToString & ""
                                    '
                                    ' COBVIEW Previsualiza S/N
                                    ' MODELO de IMPRESORA para trabajar con la aplicacion.
                                    '
                                    If Not IsDBNull(pRow("COBVIEWPDSN")) Then
                                        .Tcona4_COBVIEWPDSN = pRow("COBVIEWPDSN").ToString
                                    Else
                                        .Tcona4_COBVIEWPDSN = "False"
                                    End If
                                    .Tcona4_MODIMPREFIJO = pRow("MODIMPREFIJO").ToString & ""
                                    '
                                    ' Imprime TK FACTURA S/N
                                    ' Importe Minimo Imprimir
                                    ' Salto Lineas TK Varios AREAS, X, Z, etc ....
                                    '
                                    If Not IsDBNull(pRow("IMPRIMETKFAC")) Then
                                        .Tcona4_IMPRIMETKFAC = pRow("IMPRIMETKFAC").ToString
                                    Else
                                        .Tcona4_IMPRIMETKFAC = "False"
                                    End If
                                    If Not IsDBNull(pRow("IMPOMINIMPRE")) AndAlso
                                    pRow("IMPOMINIMPRE").ToString IsNot "0" Then
                                        .Tcona4_IMPOMINIMPRE = CDbl(pRow("IMPOMINIMPRE").ToString)
                                    Else
                                        .Tcona4_IMPOMINIMPRE = 0
                                    End If
                                    If Not IsDBNull(pRow("SALTOLINPIETK")) AndAlso
                                    pRow("SALTOLINPIETK").ToString IsNot "0" Then
                                        .Tcona4_SALTOLINPIETK = CInt(pRow("SALTOLINPIETK").ToString)
                                    Else
                                        .Tcona4_SALTOLINPIETK = 0
                                    End If
                                    If Not IsDBNull(pRow("TKFACABRECAJON")) Then
                                        .Tcona4_TKFACABRECAJON = pRow("TKFACABRECAJON").ToString
                                    Else
                                        .Tcona4_TKFACABRECAJON = "False"
                                    End If
                                    .Tcona4_TKFACPUERTO = pRow("TKFACPUERTO").ToString & ""
                                    '
                                    ' TK FAC, Detalle Combinados S/N
                                    '
                                    If Not IsDBNull(pRow("TKFACIMPDETCOMBI")) Then
                                        .Tcona4_TKFACIMPDETCOMBI = pRow("TKFACIMPDETCOMBI").ToString
                                    Else
                                        .Tcona4_TKFACIMPDETCOMBI = "False"
                                    End If
                                    '
                                    ' TK FAC, Detalle Combinados S/N
                                    '
                                    If Not IsDBNull(pRow("COMPRUIMPREINI")) Then
                                        .Tcona4_COMPRUIMPREINI = pRow("COMPRUIMPREINI").ToString
                                    Else
                                        .Tcona4_COMPRUIMPREINI = "False"
                                    End If
                                    '
                                    ' Frases Favoritas Teclado Flotante
                                    '
                                    .Tcona4_TECLADOFAV1 = pRow("TECLADOFAV1").ToString & ""
                                    .Tcona4_TECLADOFAV2 = pRow("TECLADOFAV2").ToString & ""
                                    .Tcona4_TECLADOFAV3 = pRow("TECLADOFAV3").ToString & ""
                                    .Tcona4_TECLADOFAV4 = pRow("TECLADOFAV4").ToString & ""
                                    .Tcona4_TECLADOFAV5 = pRow("TECLADOFAV5").ToString & ""
                                    '
                                    ' Detalles Tickets Fac, X y Z
                                    '
                                    .Tcona4_TKFDETLIN1 = pRow("TKFDETLIN1").ToString & ""
                                    .Tcona4_TKFDETLIN2 = pRow("TKFDETLIN2").ToString & ""
                                    .Tcona4_TKFDETLIN3 = pRow("TKFDETLIN3").ToString & ""
                                    .Tcona4_TKXZDETLIN1 = pRow("TKXZDETLIN1").ToString & ""
                                    .Tcona4_TKXZDETLIN2 = pRow("TKXZDETLIN2").ToString & ""
                                    .Tcona4_TKXZDETLIN3 = pRow("TKXZDETLIN3").ToString & ""
                                    '
                                    ' ORDEN Articulos la Lista de productos de MESA ()
                                    '
                                    If Not IsDBNull(pRow("ORDENPRODUCTOS")) Then
                                        .Tcona4_ORDENPRODUCTOS = pRow("ORDENPRODUCTOS").ToString
                                    Else
                                        .Tcona4_ORDENPRODUCTOS = "False"
                                    End If
                                    '
                                    ' Pedir Caja al Inicio?
                                    '
                                    If Not IsDBNull(pRow("PIDECAJAINICIO")) Then
                                        .Tcona4_PIDECAJAINICIO = pRow("PIDECAJAINICIO").ToString
                                    Else
                                        .Tcona4_PIDECAJAINICIO = "False"
                                    End If
                                    '
                                    ' Tipo Impresion Ticket Factura?
                                    ' 0=Windows / 1=Directa
                                    '
                                    If Not IsDBNull(pRow("TKFACDIRWIN")) Then
                                        .Tcona4_TKFACDIRWIN = pRow("TKFACDIRWIN").ToString
                                    Else
                                        .Tcona4_TKFACDIRWIN = "False"
                                    End If
                                    '
                                    ' Pide Confirmacion BORRAR lineas Cuenta Mesa
                                    ' 0=No / 1=Si
                                    '
                                    If Not IsDBNull(pRow("BORLINCUENTA")) Then
                                        .Tcona4_BORLINCUENTA = pRow("BORLINCUENTA").ToString
                                    Else
                                        .Tcona4_BORLINCUENTA = "False"
                                    End If
                                    '
                                    ' Crear Clientes Crédito? / 0=No / 1=Si
                                    '
                                    If Not IsDBNull(pRow("CREACLICREDITO")) Then
                                        .Tcona4_CREACLICREDITO = pRow("CREACLICREDITO").ToString
                                    Else
                                        .Tcona4_CREACLICREDITO = "False"
                                    End If
                                    '
                                    ' Efecto FADEOUT al salir? / 0=No / 1=Si
                                    '
                                    If Not IsDBNull(pRow("FADEOUTSALIR")) Then
                                        .Tcona4_FADEOUTSALIR = pRow("FADEOUTSALIR").ToString
                                    Else
                                        .Tcona4_FADEOUTSALIR = "False"
                                    End If
                                End With
                        End Select
                        '
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [TCONA4]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Sub CargaConfig()
        '
        ' (1) Comprobamos si existe "C:\TRIVAGES\DATOS\CONA4Cfg.txt".
        '     Se crea si no existe, con valor Defecto.
        ' (2) Leemos el fichero "C:\TRIVAGES\DATOS\CONA4Cfg.txt".
        '
        Dim DirDefecto As String = "C:\TRIVAGES\DATOS"
        Dim FicheroDefecto As String = "C:\TRIVAGES\DATOS\CONA4Cfg.txt"
        '
        If My.Computer.FileSystem.DirectoryExists(DirDefecto) = False Then
            My.Computer.FileSystem.CreateDirectory(DirDefecto)
        End If
        '
        Dim MiFileExist As Boolean
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            Try
                Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
                sw.WriteLine("1:Data Source=SERVIDOR\SQLTRV;") ' Instancia
                sw.Flush()
                sw.Close()
            Catch MyE As Exception
                MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar C:\TRIVAGES\DATOS\CONA4Cfg.txt.")
            End Try
        End If
        '
        Try
            Dim sr As StreamReader = New StreamReader(FicheroDefecto)
            Dim line, CaracterPrimero As String
            Do
                line = sr.ReadLine()
                CaracterPrimero = Mid$(line, 1, 1)
                Select Case CaracterPrimero
                    '
                    ' La estructura del archivo plano .txt
                    '  esta pensada para recoger mas de un parámetro si se necesita...
                    '
                    Case "1" ' Instancia
                        SQL_Instancia = Mid$(line, 3, Len(line)).Trim
                End Select
            Loop Until line = "END" Or line = Nothing Or sr.EndOfStream
            sr.Close()
        Catch MyE As Exception
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "C:\TRIVAGES\DATOS\CONA4Cfg.txt.")
        End Try
        '
    End Sub

    Public Sub CompruebaExisteFicheros(wTipoComp As String)
        '
        ' Este procedimiento nos ayuda a chequear 
        '  la Existencia o no de determinados archivos externos
        '  importantes para la aplicacion.
        '
        Dim FicheroComprobar As String = "" : TEMP = ""
        Dim MiFileExist As Boolean
        Dim TipoArchivos As String = ""
        '
        Select Case wTipoComp
            Case "EXT"
                TipoArchivos = "Externos"
                '
                ' Ficheros Db .mdf
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\GESTITRV.mdf"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\CONTATRV001.mdf"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\CONA4Cfg.txt"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\AbreCajon.bat"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\Cortar.bat"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\CAJON.TXT"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\DATOS\CORTE.TXT"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\CONSGEN1.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\CONSGEN2.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\CONSGEN3.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKAREAS.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKFACTURA.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKFACTURAL.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKFACTA4.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKFACTA4L.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKMENSAJES.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKZ.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\InformesCobview\TKZL.DEF"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
            Case "APP"
                TipoArchivos = "Aplicaciones"
                '
                FicheroComprobar = "C:\TRIVAGES\COBVIEW.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\TACALM.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\TACONA1.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\TAFAMI.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\TAVENDEDORES.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\WDAAPER.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
                '
                FicheroComprobar = "C:\TRIVAGES\WTPMESAS.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                End If
            Case "VIEWDAT"
                TipoArchivos = " cobview.dat "
                '
                FicheroComprobar = "C:\TRIVAGES\cobview.dat"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    TEMP += "[ OK  ] " & FicheroComprobar & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "NOTA.: Para evitar que queden informes residentes" & vbCrLf
                    TEMP += "despues de ser visualizados, editar internamente" & vbCrLf
                    TEMP += "este archivo, modificando:" & vbCrLf
                    TEMP += "AutoDelete=0 por AutoDelete=1" & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "Puede usar el boton Ejecutar ViewConfig.exe" & vbCrLf
                    TEMP += "para este fin." & vbCrLf
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "No es imprescindible, pero si conveniente." & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "NOTA.: Para evitar que queden informes residentes" & vbCrLf
                    TEMP += "despues de ser visualizados, editar internamente" & vbCrLf
                    TEMP += "este archivo, modificando:" & vbCrLf
                    TEMP += "AutoDelete=0 por AutoDelete=1" & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "Puede usar el boton Ejecutar ViewConfig.exe" & vbCrLf
                    TEMP += "para este fin." & vbCrLf
                End If
                '
                msg = TEMP
                style = MsgBoxStyle.Information Or MsgBoxStyle.OkOnly
                title = "Comprobación de Archivos.: [" & TipoArchivos & "]"
                MsgBox(msg, style, title)
                Exit Sub
            Case "VIEWCFG"
                TipoArchivos = " ViewConfig.exe "
                '
                FicheroComprobar = "C:\TRIVAGES\ViewConfig.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    Dim myProcess As New Process()
                    Try
                        '
                        ' Lanzar Proceso ...
                        '
                        With myProcess
                            With .StartInfo
                                .UseShellExecute = True
                                .FileName = "C:\TRIVAGES\ViewConfig.exe"
                            End With
                            ' <> Modificar Titulo
                            .Start()
                        End With
                    Catch e As Exception
                        MsgBox("ERROR: " & e.Source & vbCrLf & e.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: C:\TRIVAGES\ViewConfig.exe.")
                    End Try
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "No es imprescindible, pero si conveniente." & vbCrLf
                    msg = TEMP
                    style = MsgBoxStyle.Information Or MsgBoxStyle.OkOnly
                    title = "Comprobación de Archivos.: [" & TipoArchivos & "]"
                    MsgBox(msg, style, title)
                End If
                '
                Exit Sub
            Case "EDITOR"
                TipoArchivos = " Editor.exe "
                '
                FicheroComprobar = "C:\TRIVAGES\Editor.exe"
                MiFileExist = My.Computer.FileSystem.FileExists(FicheroComprobar)
                If MiFileExist = True Then
                    Dim myProcess As New Process()
                    Try
                        '
                        ' Lanzar Proceso ...
                        '
                        With myProcess
                            With .StartInfo
                                .UseShellExecute = True
                                .FileName = "C:\TRIVAGES\Editor.exe"
                            End With
                            ' <> Modificar Titulo
                            .Start()
                        End With
                    Catch e As Exception
                        MsgBox("ERROR: " & e.Source & vbCrLf & e.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: C:\TRIVAGES\Editor.exe.exe.")
                    End Try
                Else
                    TEMP += "[FALTA] " & FicheroComprobar & vbCrLf
                    TEMP += vbCrLf
                    TEMP += "No es imprescindible." & vbCrLf
                    msg = TEMP
                    style = MsgBoxStyle.Information Or MsgBoxStyle.OkOnly
                    title = "Comprobación de Archivos.: [" & TipoArchivos & "]"
                    MsgBox(msg, style, title)
                End If
                '
                Exit Sub
        End Select
        '
        ' Resultados
        '
        msg = TEMP
        style = MsgBoxStyle.Information Or MsgBoxStyle.OkOnly
        title = "Comprobación de Archivos.: [" & TipoArchivos & "]"
        MsgBox(msg, style, title)
        '
    End Sub

    Private Sub GeneraTXTVarios()
        '-----------------------------------------------------------------------
        ' En este Procedimiento se GENERAN Varios Archivos de USO
        '  para la Aplicacion, SOLO si No estan CREADOS.
        '-----------------------------------------------------------------------
        Dim FicheroDefecto As String = "" : Dim MiFileExist As Boolean
        '
        ' Genera el Fichero con la secuencia:
        '    AVPOS TC20 : CORTE H"1B69" :: Chr :: Chr(27) & Chr(105)
        '
        FicheroDefecto = "C:\TRIVAGES\DATOS\CORTE.TXT"
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            Try
                Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
                sw.WriteLine(Chr(27) & Chr(105))
                sw.Flush()
                sw.Close()
            Catch MyE As Exception
                MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
            End Try
        End If
        '
        ' Genera el Fichero con la secuencia: 
        '    ABRIR CAJÓN H"1B70300A32"
        '    AVPOS TC20 : Chr :: Chr(27) & Chr(112) & Chr(48) & Chr(10) & Chr(50)
        '
        FicheroDefecto = "C:\TRIVAGES\DATOS\CAJON.TXT"
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            Try
                Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
                sw.WriteLine(Chr(27) & Chr(112) & Chr(48) & Chr(10) & Chr(50))
                sw.Flush()
                sw.Close()
            Catch MyE As Exception
                MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
            End Try
        End If
        '
        ' Creo algunos ficheros por lotes, .bat relacionados y necesarios, 
        '    si no estan creados.
        '
        ' AbreCajon.bat
        '
        FicheroDefecto = "C:\TRIVAGES\DATOS\AbreCajon.bat"
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            Try
                Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
                sw.WriteLine("PRINT /D:LPT1 C:\TRIVAGES\DATOS\CAJON.TXT")
                sw.Flush()
                sw.Close()
            Catch MyE As Exception
                MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
            End Try
        End If
        '
        ' Cortar.bat
        '
        FicheroDefecto = "C:\TRIVAGES\DATOS\Cortar.bat"
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            Try
                Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
                sw.WriteLine("PRINT /D:LPT1 CORTE.TXT")
                sw.Flush()
                sw.Close()
            Catch MyE As Exception
                MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
            End Try
        End If
        '
    End Sub

    Public Sub AbandonaAplicacion()
        '
        '   Abandono de la aplicación, Preguntamos al usuario.
        '
        msg = "¿Desea Salir de la Aplicación?"
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.YesNo
        title = "Abandonar la aplicación."
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            '
            ' Si el Form Cambio de Caja esta visible lo ocultamos.
            ' Es una cuestion mas estética que práctica,
            '   para efecto de fundido al cerrar.
            '
            If MyFrm19.Visible = True Then
                MyFrm19.Visible = False
            End If
            '
            LeeTCONA4Cfg("General")
            If wrLeeTCONA4.Tcona4_FADEOUTSALIR = "True" Then
                FORM_FadeOut(FormularioInicial)
            End If
            Application.Exit()
        End If
        '
    End Sub

    Public Sub FORM_FadeOut(wFrm As Integer)
        '
        ' Efecto Sobre el Form, al cerrar la Aplicación.
        ' wFrm
        '   0 - MESAS
        '   1 - PRODUCTOS
        '
        Dim MyOpacity As Double = 0
        Select Case wFrm
            Case 0
                For MyOpacity = 100 To 0 Step -15
                    MyFrm1.Opacity = MyOpacity / 100
                    MyFrm1.Refresh()
                    Threading.Thread.Sleep(8)
                Next
            Case 1
                For MyOpacity = 100 To 0 Step -15
                    MyFrm2.Opacity = MyOpacity / 100
                    MyFrm3.Refresh()
                    Threading.Thread.Sleep(8)
                Next
        End Select
        '
    End Sub

    Public Function LeeSALA(wLeeCodSala As String) As Boolean
        '
        ' Lectura Datos de una SALA.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        LeeSALA = False
        '
        Dim queryString As String = "SELECT * FROM [SALA] WHERE "
        queryString = queryString & "[SALA].[CAJA]=" & wCaja & " AND "
        queryString = queryString & "[SALA].[CODIGO]='" & wLeeCodSala & "' "
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA")
            '
            If dt.Tables("SALA").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("SALA").Rows
                    If pRow("CODIGO").ToString() = wLeeCodSala Then
                        '
                        ' La SALA esta Creada, luego EXISTE!!!
                        '
                        LeeSALA = True
                        '
                        With wrLeeSALA
                            .Sala_NOMBRE = pRow("NOMBRE").ToString()
                            If pRow("NOZETA").ToString() = "False" Or
                            IsDBNull(pRow("NOZETA")) Then
                                .Sala_NOZETA = 0
                            Else
                                .Sala_NOZETA = 1
                            End If
                            '
                            '   Reparto a Domicilio?.
                            '
                            If Not IsDBNull(pRow("REPARTO")) And
                                        pRow("REPARTO").ToString = "True" Then
                                .Sala_REPARTO = "True"
                            Else
                                .Sala_REPARTO = "False"
                            End If
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [SALA]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function MiraMesasOcupadas() As Boolean
        '
        ' Detecta si HAY MESAS OCUPADAS
        '
        MiraMesasOcupadas = False
        NumMesasOcu = 0 : CodsMesasOcu = ""
        Dim i As Integer = 0
        TotImpoOCU = 0
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim queryString As String = "SELECT * FROM [SALA1] WHERE "
        queryString = queryString & "[SALA1].[FACTURA] > 0 AND "
        queryString = queryString & "[SALA1].[CAJA] = " & wCaja & " "
        queryString = queryString & "ORDER BY [SALA1].[CODIGO], [SALA1].[MESA]"
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA1")
            '
            If dt.Tables("SALA1").Rows.Count > 0 Then
                MiraMesasOcupadas = True
                NumMesasOcu = dt.Tables("SALA1").Rows.Count
                '
                For Each pRow As DataRow In dt.Tables("SALA1").Rows
                    '
                    ' Importe TOTAL Ocupadas, OPC = 2
                    ' Lee Solo CAJA / MESA, independiente de la factura 
                    '   que tenga asignada.
                    '
                    If ExisteRegistroMESAC(pRow("MESA").ToString().Trim, 2) Then
                        TotImpoOCU += CDbl(wrLeeMESAC.Mesac_IMPORTE)
                    End If
                    '
                    i += 1
                    CodsMesasOcu &= "Mesa " & pRow("MESA").ToString() & " en Sala " & pRow("CODIGO").ToString() & ", "
                    If i > 4 Then
                        i = 0
                        CodsMesasOcu &= vbCrLf & "                           "
                    End If
                Next
                '
            End If
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [SALA1]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeeMesa_SALA1(wLeeCodSala As String, wLeeCodMesa As String, wOpci As Integer) As Boolean
        '
        ' Lectura Datos de una MESA - [SALA1]
        ' Entre otros Datos, este procedimiento me indica si la MESA
        '  esta LIBRE     / OCUPADA
        '       --------- / -------------
        '       factura=0 / Factura=Valor
        '
        ' wOpci = 0 :: Lee controlando Numero de Factura.
        '              Si hay Factura Solo Lee datos, indica OCUPADA.
        '              Si NO hay Factura Incrementa CONTADOR Factura.
        '           ( Es la operativa habitual de la aplicación ).
        '
        ' wOpci = 1 :: Solo queremos Leer Datos de una SALA/MESA.
        '              Comprobar su existencia, 
        '                NO influyendo sobre CONTADOR de factura.
        '            Necesario para Cambio de MESAS y Otras llamadas.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Inicialmente asumimos que una MESA esta Libre.
        ' Tambien que NO existe, excepto que ... ... ...
        '
        LeeMesa_SALA1 = False : wFacturaN = 0 : wMesaLibre = True
        '
        Dim queryString As String = "SELECT * FROM [SALA1] WHERE "
        queryString = queryString & "[SALA1].[CAJA]=" & wCaja & " AND "
        queryString = queryString & "[SALA1].[CODIGO]='" & wLeeCodSala & "' AND "
        queryString = queryString & "[SALA1].[MESA]='" & wLeeCodMesa & "' "
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA1")
            '
            If dt.Tables("SALA1").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("SALA1").Rows
                    If pRow("MESA").ToString() = wLeeCodMesa Then
                        '
                        ' La Mesa esta Creada, luego EXISTE!!!
                        '
                        LeeMesa_SALA1 = True
                        '
                        ' Gestionamos Nro. Factura de la MESA
                        ' Informa si MESA LIBRE / OCUPADA
                        '
                        If pRow("FACTURA").ToString() = "0" Or
                            IsDBNull(pRow("FACTURA")) Then
                            '
                            ' La MESA esta LIBRE
                            ' Lee CONTADOR de factura, suma 1, 
                            '     pero NO lo actualizamos aqui...
                            '
                            If LeeTCONA4Cfg("Factura") = False Then
                                CreaTCONA4Cfg(wCaja)
                                LeeTCONA4Cfg("Factura")
                            End If
                            '
                            ' Si wOpci = 1 Solo queremos LEER
                            '
                            If wOpci = 0 Then
                                wFacturaN += 1
                            End If
                            '
                            ' Si MESA ESTA LIBRE ...
                            '
                            wrLeeSALA1.Sala1_FACTURA = wFacturaN
                            wrLeeSALA1.Sala1_FECAPERTURA = Date.Now.ToShortDateString
                            wrLeeSALA1.Sala1_HORAAPAERTURA = Date.Now.ToShortTimeString
                            wMesaLibre = True
                        Else
                            '
                            ' La MESA esta OCUPADA, hay una FACTURA en curso.
                            ' Datos:
                            '   Factura y Fecha de Apertura.
                            '
                            wMesaLibre = False
                            '
                            wFacturaN = CInt(pRow("FACTURA").ToString())
                            wrLeeSALA1.Sala1_FACTURA = CInt(pRow("FACTURA").ToString())
                            If IsDBNull(pRow("FECAPERTURA")) Then
                                FechaMESAC = Date.Now.ToShortDateString
                                HoraMESAC = Date.Now.ToShortTimeString
                                wrLeeSALA1.Sala1_HORAAPAERTURA = HoraMESAC
                            Else
                                FechaMESAC = Format(pRow("FECAPERTURA"), "dd/MM/yyyy")
                                HoraMESAC = pRow("HORAAPAERTURA").ToString
                                wrLeeSALA1.Sala1_HORAAPAERTURA = HoraMESAC
                            End If
                            wrLeeSALA1.Sala1_FECAPERTURA = FechaMESAC
                        End If
                        '
                        '   Datos Varios de la MESA LIBRE u OCUPADA
                        '
                        '   Vendedor de Apertura.
                        '
                        If IsDBNull(pRow("VENDEDOR")) Then
                            wVendedorApertura = 0
                        Else
                            wVendedorApertura = CInt(pRow("VENDEDOR").ToString)
                        End If
                        wrLeeSALA1.Sala1_VENDEDOR = wVendedorApertura
                        '
                        '   MESA Visible?.
                        '
                        If Not IsDBNull(pRow("VISIBLE")) And
                                        pRow("VISIBLE").ToString = "True" Then
                            wrLeeSALA1.Sala1_VISIBLE = "True"
                        Else
                            wrLeeSALA1.Sala1_VISIBLE = "False"
                        End If
                        '
                        '   Pide PAX en esta MESA?.
                        '
                        If Not IsDBNull(pRow("PIDEPAX")) And
                                        pRow("PIDEPAX").ToString = "True" Then
                            wrLeeSALA1.Sala1_PIDEPAX = "True"
                        Else
                            wrLeeSALA1.Sala1_PIDEPAX = "False"
                        End If
                        '
                        '   PAX, PERSONAS en la MESA?.
                        '
                        If Not IsDBNull(pRow("PAX")) Then
                            wrLeeSALA1.Sala1_PAX = CInt(pRow("PAX"))
                        Else
                            wrLeeSALA1.Sala1_PAX = 0
                        End If
                        '
                        '   PVP
                        '
                        If Not IsDBNull(pRow("PVP")) Then
                            wrLeeSALA1.Sala1_PVP = pRow("PVP").ToString
                        Else
                            wrLeeSALA1.Sala1_PVP = "0,00"
                        End If
                        '
                        '   LOGO
                        '
                        If Not IsDBNull(pRow("LOGO")) Then
                            wrLeeSALA1.Sala1_LOGO = pRow("LOGO").ToString
                        Else
                            wrLeeSALA1.Sala1_LOGO = " "
                        End If
                        '
                        '   DESCRIPCION
                        '
                        If Not IsDBNull(pRow("DESCMESA")) Then
                            wrLeeSALA1.Sala1_DESCMESA = pRow("DESCMESA").ToString
                        Else
                            wrLeeSALA1.Sala1_DESCMESA = " "
                        End If
                        '
                        '  IMPRIME FACTURA PARA ESTA MESA?
                        '  Se considera valor NULL como = True
                        '
                        If IsDBNull(pRow("IMPFACTU")) Or
                           pRow("IMPFACTU").ToString = "True" Then
                            wrLeeSALA1.Sala1_IMPFACTU = "True"
                        Else
                            wrLeeSALA1.Sala1_IMPFACTU = "False"
                        End If
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [SALA1]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeeMar(wCodArt As String) As Boolean
        '
        ' Lectura Datos de un artículo.
        '
        LeeMar = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        'Dim NumBTnART As Integer = 30 : Dim IndBTnART As Integer = 0
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [MAR] "
        queryString = queryString & "WHERE [MAR].[NARTICULO]='" & wCodArt & "' "
        queryString = queryString & "ORDER BY [MAR].[NARTICULO] "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MAR")
            '
            If dt.Tables("MAR").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeMar = True
                For Each pRow In dt.Tables("MAR").Rows
                    If pRow("NARTICULO").ToString() = wCodArt Then
                        '
                        ' Recogemos datos...
                        '
                        With wrLeeMAR
                            .Mar_FAMILIA = pRow("FAMILIA").ToString() & ""
                            .Mar_DESCRIPCION = pRow("DESCRIPCION").ToString()
                            '
                            ' PRECIOS ::
                            '    PREPVP1, a PREPVP9
                            '    PRECOSTO
                            '    Mar_PREPVPTPV (Según Ref. Generales)
                            '
                            If Not IsDBNull(pRow("PREPVP1")) Then
                                .Mar_PREPVP1 = pRow("PREPVP1").ToString()
                            Else
                                .Mar_PREPVP1 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP2")) Then
                                .Mar_PREPVP2 = pRow("PREPVP2").ToString()
                            Else
                                .Mar_PREPVP2 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP3")) Then
                                .Mar_PREPVP3 = pRow("PREPVP3").ToString()
                            Else
                                .Mar_PREPVP3 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP4")) Then
                                .Mar_PREPVP4 = pRow("PREPVP4").ToString()
                            Else
                                .Mar_PREPVP4 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP5")) Then
                                .Mar_PREPVP5 = pRow("PREPVP5").ToString()
                            Else
                                .Mar_PREPVP5 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP6")) Then
                                .Mar_PREPVP6 = pRow("PREPVP6").ToString()
                            Else
                                .Mar_PREPVP6 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP7")) Then
                                .Mar_PREPVP7 = pRow("PREPVP7").ToString()
                            Else
                                .Mar_PREPVP7 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP8")) Then
                                .Mar_PREPVP8 = pRow("PREPVP8").ToString()
                            Else
                                .Mar_PREPVP8 = "0,00"
                            End If
                            If Not IsDBNull(pRow("PREPVP9")) Then
                                .Mar_PREPVP9 = pRow("PREPVP9").ToString()
                            Else
                                .Mar_PREPVP9 = "0,00"
                            End If
                            '
                            If Not IsDBNull(pRow("PRECOSTO")) Then
                                .Mar_PRECOSTO = pRow("PRECOSTO").ToString()
                            Else
                                .Mar_PRECOSTO = "0,00"
                            End If
                            '
                            ' Otros ... 
                            '
                            If Not IsDBNull(pRow("IVAVENTA")) Then
                                .Mar_IVAVENTA = pRow("IVAVENTA").ToString()
                            Else
                                .Mar_IVAVENTA = "0,00"
                            End If
                            '
                            ' En este caso MAR.COMBINADO = Codigo de Grupo.
                            '
                            If Not IsDBNull(pRow("COMBINADO")) Then
                                .Mar_COMBINADO = pRow("COMBINADO").ToString()
                            Else
                                .Mar_COMBINADO = ""
                            End If
                            '
                            ' Area.
                            ' Si AREA es NULL y dado que el AREA 0 (CERO) puede existir
                            ' le muevo 999999999 para identificar 
                            ' que este AREA no HA DE SER IMPRESA
                            '
                            If Not IsDBNull(pRow("AREA")) Then
                                .Mar_AREA = CInt(pRow("AREA").ToString)
                            Else
                                .Mar_AREA = 999999999
                            End If
                            '
                            ' Imagen del Producto
                            '
                            If Not IsDBNull(pRow("IMAGEN")) Then
                                .Mar_IMAGEN = pRow("IMAGEN").ToString.Trim
                            Else
                                .Mar_IMAGEN = ""
                            End If
                            '
                            ' Precio que usaremos según Ref. Generales.
                            ' PVP3/4 No permitidos
                            '
                            LeeTCONA4Cfg("General")
                            If wrLeeTCONA4.Tcona4_TIPOPVPARTI.ToString.Length > 0 Then
                                Select Case wrLeeTCONA4.Tcona4_TIPOPVPARTI.ToString.Trim
                                    Case "1"
                                        .Mar_PREPVPTPV = .Mar_PREPVP1
                                    Case "2"
                                        .Mar_PREPVPTPV = .Mar_PREPVP2
                                    Case "5"
                                        .Mar_PREPVPTPV = .Mar_PREPVP5
                                    Case "6"
                                        .Mar_PREPVPTPV = .Mar_PREPVP6
                                    Case "7"
                                        .Mar_PREPVPTPV = .Mar_PREPVP7
                                    Case "8"
                                        .Mar_PREPVPTPV = .Mar_PREPVP8
                                    Case "9"
                                        .Mar_PREPVPTPV = .Mar_PREPVP9
                                    Case Else ' Defecto
                                        .Mar_PREPVPTPV = .Mar_PREPVP1
                                End Select
                                '
                                ' Si el usuario elige un PVP se aplica
                                '   dicho PVP hasta Aparcar.
                                ' Aqui tambien PVP3/4 No estan permitidos
                                '
                                If MyFrm2.ComboBoxTarifas.Text.ToString.Length > 0 Then
                                    Select Case MyFrm2.ComboBoxTarifas.SelectedIndex
                                        Case 0
                                            .Mar_PREPVPTPV = .Mar_PREPVP1
                                        Case 1
                                            .Mar_PREPVPTPV = .Mar_PREPVP2
                                        Case 2
                                            .Mar_PREPVPTPV = .Mar_PREPVP5
                                        Case 3
                                            .Mar_PREPVPTPV = .Mar_PREPVP6
                                        Case 4
                                            .Mar_PREPVPTPV = .Mar_PREPVP7
                                        Case 5
                                            .Mar_PREPVPTPV = .Mar_PREPVP8
                                        Case 6
                                            .Mar_PREPVPTPV = .Mar_PREPVP9
                                        Case Else ' Por Defecto
                                            .Mar_PREPVPTPV = .Mar_PREPVP1
                                    End Select
                                End If

                            Else ' Defecto
                                .Mar_PREPVPTPV = .Mar_PREPVP1
                            End If
                        End With
                        '
                        ' Salimos del bucle si ya tenemos el Art. Deseado
                        '
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [MAR]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function DameCatalogoEmpresa(wCodEmpre As Integer, wCatalogo As String) As String
        '
        ' Función Publica para Montar Catálogos 
        ' a una empresa dada.
        '
        ' Entrada:
        '    wCodEmpre = Codigo Empresa
        '    wCatalogo = Catálogo al que conectar.
        ' Ejemplo Salida:
        '    "Initial Catalog=CONTATRV001;"
        '
        DameCatalogoEmpresa = "Initial Catalog="
        DameCatalogoEmpresa &= wCatalogo.Trim
        DameCatalogoEmpresa &= wCodEmpre.ToString(fmtEMPRESA)
        DameCatalogoEmpresa &= ";"
        '
    End Function

    Public Function LeeClienteMCO(wCodCli As Integer) As Boolean
        '
        ' Lectura Datos de un Cliente. ( Contabilidad )
        ' Montamos el Catálogo en función de la empresa Actual.
        '
        LeeClienteMCO = False
        '
        Dim conexion As New SqlConnection
        SQL_Catalogo1 = DameCatalogoEmpresa(wEmpresa, "CONTATRV")
        SQL_CadenaConexion1 = SQL_Instancia & SQL_Catalogo1 & SQL_Seguridad_Otros
        conexion.ConnectionString = SQL_CadenaConexion1
        '
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [MCO] "
        queryString = queryString & "WHERE [MCO].[CUENTA]=" & wCodCli & " "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MCO")
            '
            If dt.Tables("MCO").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeClienteMCO = True
                For Each pRow In dt.Tables("MCO").Rows
                    If CInt(pRow("CUENTA").ToString()) = wCodCli Then
                        '
                        ' Recogemos datos...
                        '
                        With wrLeeCODNOM
                            .CODIGO = pRow("CUENTA").ToString() & ""
                            .NOMBRE = pRow("NOMBRE").ToString() & ""
                        End With
                        '
                        With wrLeeCLIEMCO
                            .NOMBRE = pRow("NOMBRE").ToString() & ""
                            .CIF = pRow("CIF").ToString() & ""
                            .DIRECCION = pRow("DIRECCION").ToString() & ""
                            .POBLACION = pRow("POBLACION").ToString() & ""
                            .CODPOSTAL = CInt(pRow("CODPOSTAL").ToString() & "")
                            .TELEFONO = pRow("TELEFONO").ToString() & ""
                            .TELEFONO2 = pRow("TELEFONO2").ToString() & ""
                            .EMAIL = pRow("EMAIL").ToString() & ""
                            .DTO = pRow("DTO").ToString() & ""
                        End With
                        '
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [MCO]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeeFam(wCodFam As String) As Boolean
        '
        ' Lectura Datos de un Familia.
        '
        LeeFam = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [FAM] "
        queryString = queryString & "WHERE [FAM].[CODIGO]='" & wCodFam & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "FAM")
            '
            If dt.Tables("FAM").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeFam = True
                For Each pRow In dt.Tables("FAM").Rows
                    If pRow("CODIGO").ToString() = wCodFam Then
                        '
                        ' Recogemos datos...
                        '
                        With wrLeeCODNOM
                            .CODIGO = pRow("CODIGO").ToString() & ""
                            .NOMBRE = pRow("NOMBRE").ToString()
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [FAM]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeePEDCLIE(wTelefono As String) As Boolean
        '
        ' Lectura Datos de Pedidos de Clientes a Domicilio.
        ' Key.: Teléfono.
        '
        LeePEDCLIE = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [PEDCLIE] "
        queryString = queryString & "WHERE [PEDCLIE].[TELEFONO]='" & wTelefono & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "PEDCLIE")
            '
            If dt.Tables("PEDCLIE").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeePEDCLIE = True
                For Each pRow In dt.Tables("PEDCLIE").Rows
                    If pRow("TELEFONO").ToString() = wTelefono Then
                        '
                        ' Recogemos datos...
                        '
                        With wrLeePEDCLIE
                            .NOMBRE = pRow("NOMBRE").ToString() & ""
                            .DIRECCION = pRow("DIRECCION").ToString() & ""
                            .POBLACION = pRow("POBLACION").ToString() & ""
                            .EMAIL = pRow("EMAIL").ToString() & ""
                            .CODPOSTAL = CInt(pRow("CODPOSTAL").ToString() & "")
                            .OBSER = pRow("OBSER").ToString() & ""
                            If Not IsDBNull(pRow("EMAILSN")) Then
                                .EMAILSN = pRow("EMAILSN").ToString
                            Else
                                .EMAILSN = "False"
                            End If
                            If Not IsDBNull(pRow("SMSSN")) Then
                                .SMSSN = pRow("SMSSN").ToString
                            Else
                                .SMSSN = "False"
                            End If
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [PEDCLIE]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeeClienteCONTA(wNifCif As String) As Boolean
        '
        ' Lectura Datos de Clientes CONTADO.
        ' Key.: NIF/CIF.
        '
        LeeClienteCONTA = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [CLICONTA] "
        queryString = queryString & "WHERE [CLICONTA].[NIFCIF]='" & wNifCif & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "CLICONTA")
            '
            If dt.Tables("CLICONTA").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeClienteCONTA = True
                For Each pRow In dt.Tables("CLICONTA").Rows
                    If pRow("NIFCIF").ToString() = wNifCif Then
                        '
                        ' Recogemos datos.
                        ' Para Clientes Contado Reutilizo wrLeeCLIEMCO
                        '
                        With wrLeeCLIEMCO
                            .NOMBRE = pRow("NOMBRE").ToString() & ""
                            .CIF = pRow("NIFCIF").ToString() & ""
                            .DIRECCION = pRow("DIRECCION").ToString() & ""
                            .POBLACION = pRow("POBLACION").ToString() & ""
                            .CODPOSTAL = CInt(pRow("CODPOSTAL").ToString() & "")
                            .TELEFONO = pRow("TELEFONO1").ToString() & ""
                            .TELEFONO2 = pRow("TELEFONO2").ToString() & ""
                            .EMAIL = pRow("EMAIL").ToString() & ""
                            .DTO = pRow("DTO").ToString() & ""
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [PEDCLIE]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeeArea(wCodArea As Integer) As Boolean
        '
        ' Lectura Datos de AREAS de Impresion.
        '
        LeeArea = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [AREAS] WHERE "
        queryString = queryString & "[AREAS].[NUMCAJA]=" & wCaja & " AND "
        queryString = queryString & "[AREAS].[AREA]=" & wCodArea
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "AREAS")
            '
            If dt.Tables("AREAS").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeArea = True
                For Each pRow In dt.Tables("AREAS").Rows
                    If CInt(pRow("AREA").ToString) = wCodArea Then
                        '
                        ' Recogemos datos...
                        '
                        With wrLeeAREAS
                            .DESCRIPCION = pRow("DESCRIPCION").ToString() & ""
                            .PUERTOIMPRE = pRow("PUERTOIMPRE").ToString() & ""
                            '
                            If Not IsDBNull(pRow("AREA2")) Then
                                .AREA2 = CInt(pRow("AREA2").ToString())
                            End If
                            If Not IsDBNull(pRow("AREA3")) Then
                                .AREA3 = CInt(pRow("AREA3").ToString())
                            End If
                            If Not IsDBNull(pRow("AREA4")) Then
                                .AREA4 = CInt(pRow("AREA4").ToString())
                            End If
                            '
                            .REPLICAR = pRow("REPLICAR").ToString()
                            .MODELOIMPRE = pRow("MODELOIMPRE").ToString() & ""
                            '
                            If Not IsDBNull(pRow("TIPOIMPRESION")) Then
                                .TIPOIMPRESION = pRow("TIPOIMPRESION").ToString
                            Else
                                .TIPOIMPRESION = "False"
                            End If
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [AREAS]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Sub GrabaDatosMesa(wTblMESA As String, wGrabaMESA As String)
        '
        '   Grabamos los datos de UNA MESA   ...(APARCAR)...
        '      Datos Cabecera :: MESAC
        '      Datos Mesas    :: MESA
        '-----------------------------------------------------------------
        ' (1) Comprobar si la mesa está LIBRE (FACTURA=0).
        ' (2) De estar Libre, Aqui volvemos a leer el Contador de Factura
        '     se lo asignamos a esta MESA e INCREMENTAMOS el CONTADOR.
        '-----------------------------------------------------------------
        Select Case FormularioInicial
            Case 0
                If LeeMesa_SALA1(wCodSala, wGrabaMESA, 0) = True Then
                    If wMesaLibre = True Then
                        ActualizaMesa_SALA1(wCaja, wCodSala, wGrabaMESA, 0)
                        Actualiza_TCONA4(wCaja, "Factura")
                    End If
                End If
            Case 1
                wFacturaN = CInt(MyFrm2.TextBoxFactura.Text.Trim)
                Actualiza_TCONA4(wCaja, "Factura")
        End Select
        '
        Select Case wTblMESA
            Case "MESAC"
                GrabaRegistroMESAC(wGrabaMESA)
            Case "MESA"
                GrabaRegistroMESA(wGrabaMESA)
        End Select
        '
    End Sub

    Private Sub GrabaRegistroMESAC(GrabaMESA As String)
        '
        ' Este procedimiento se usa para GRABAR / ACTUALIZAR datos desde la lista de productos
        '   normalmente al APARCAR.
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
        ' Fecha de la mesa.
        '   Inicialmente = Fecha del Día.
        '   Si la mesa esta ocupada la fecha de apertura.
        '
        If LeeMesa_SALA1(wCodSala, GrabaMESA, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        '
        ' Recogemos los DATOS para CEBECERA MESAS
        '
        ' Validación: 
        '   Vendedor (Camarero)
        '
        If MyFrm2.TextBoxCamarero.Text.Length = 0 Or Not IsNumeric(MyFrm2.TextBoxCamarero.Text) Then
            MyFrm2.TextBoxCamarero.Text = "1"
        End If
        '  
        '   IMPORTE COMANDA
        '
        If MyFrm2.LabelTotComanda.Text.Length = 0 Or Not IsNumeric(MyFrm2.LabelTotComanda.Text) Then
            MyFrm2.LabelTotComanda.Text = "0"
        End If
        Dim WiMPORTE As String = MyFrm2.LabelTotComanda.Text.ToString.Trim
        WiMPORTE.Replace(",", ".")
        '
        ' Cálculos % IGIC // IMPORTE IGIC
        '
        Dim Mitotal As Double = CDbl(MyFrm2.LabelTotComanda.Text.Replace(".", ",").Trim)
        Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
        Dim Micalculo As Double = (MiPorIGIC / 100) + 1
        Dim MiBase As Double = Math.Round((Mitotal / Micalculo), 2)
        Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
        '
        With wrMESAC
            .Mesac_EMPRESA = 1
            .Mesac_CLIENTE = wCliente
            .Mesac_VENDEDOR = CInt(MyFrm2.TextBoxCamarero.Text.Trim)
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
        End With
        '
        If ExisteRegistroMESAC(GrabaMESA, 0) = True Then
            '
            ' UPDATE
            '
            queryString = queryString & "UPDATE [MESAC] SET "
            queryString = queryString & "[MESAC].[EMPRESA]=" & wrMESAC.Mesac_EMPRESA & ", "
            queryString = queryString & "[MESAC].[CLIENTE]=" & wrMESAC.Mesac_CLIENTE & ", "
            queryString = queryString & "[MESAC].[VENDEDOR]=" & wrMESAC.Mesac_VENDEDOR & ", "
            queryString = queryString & "[MESAC].[PIGIC]='" & wrMESAC.Mesac_PIGIC & "', "
            queryString = queryString & "[MESAC].[PDTO]='" & wrMESAC.Mesac_PDTO & "', "
            queryString = queryString & "[MESAC].[IMPDTO]='" & wrMESAC.Mesac_IMPDTO & "', "
            queryString = queryString & "[MESAC].[IMPORTE]='" & wrMESAC.Mesac_IMPORTE & "', "
            queryString = queryString & "[MESAC].[ENTREGA]='" & wrMESAC.Mesac_ENTREGA & "', "
            queryString = queryString & "[MESAC].[TARJETA]='" & wrMESAC.Mesac_TARJETA & "', "
            queryString = queryString & "[MESAC].[VALEDTO]='" & wrMESAC.Mesac_VALEDTO & "', "
            queryString = queryString & "[MESAC].[CHEQUES]='" & wrMESAC.Mesac_CHEQUES & "', "
            queryString = queryString & "[MESAC].[OTROS]='" & wrMESAC.Mesac_OTROS & "', "
            queryString = queryString & "[MESAC].[EFECTIVO]='" & wrMESAC.Mesac_EFECTIVO & "', "
            queryString = queryString & "[MESAC].[IMPIGIC]='" & wrMESAC.Mesac_IMPIGIC & "', "
            queryString = queryString & "[MESAC].[HORAAPAERTURA]='" & HoraMESAC & "' "
            queryString = queryString & "WHERE "
            queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & "[MESAC].[FECHA]='" & FechaMESAC & "' AND "
            queryString = queryString & "[MESAC].[SALA]='" & wCodSala & "' AND "
            queryString = queryString & "[MESAC].[MESA]='" & GrabaMESA & "' AND "
            queryString = queryString & "[MESAC].[FACTURA]=" & wFacturaN
        Else
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
            queryString = queryString & "[MESAC].[HORAAPAERTURA]"
            queryString = queryString & ") Values ("
            queryString = queryString & wCaja & ", "
            queryString = queryString & "'" & FechaMESAC & "', "
            queryString = queryString & "'" & wCodSala & "', "
            queryString = queryString & "'" & GrabaMESA & "', "
            queryString = queryString & wFacturaN & ", "
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
            queryString = queryString & "'" & HoraMESAC & "' "
            queryString = queryString & ")"
            '
        End If
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

    Public Sub ActualizaDatosMESAC(ActualizaMESA As String, ActMesaOPC As Integer)
        '
        ' Este procedimiento se usa para ACTUALIZAR datos en MESAC (Cabecera para las MESAS)
        ' Flexibiliza el procedimiento ActMesaOPC, que determinará 
        '    que datos queremos actualizar:
        ' ----------
        ' ActMesaOPC
        ' ----------
        ' 0  = Actualiza datos al realizar un COBRO, entre ellos FECHA y HORA Cobro.
        ' 1  = Actualiza IMPORTE= 0 en MESAC, al anular TODAS las lineas en MESA.
        ' 2  = Actualiza TKFACIMPRESO = 1 (TICKET IMPRESO ).
        ' 3  = Actualiza IMPORTE al Separar Cuentas.
        '      También Hora Apeertura.
        ' 4  = Actualiza el TELEFONO de un Pedido a Domicilio.
        ' 5n = Actualiza el Cod. Cliente para FACTURA A4.
        '      Puden ser Clientes 
        '      51 = Contado - NIF/CIF <> "", " "
        '      52 = Crédito - 430nnnnnn <> 430000000
        '
        ' MESAC :: Cabecera
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '
        Dim queryString As String = ""
        If LeeMesa_SALA1(wCodSala, ActualizaMESA, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        '
        ' Recogemos los DATOS para CABECERA MESAS
        '
        ' Validación: 
        '   Vendedor (Camarero)
        '
        If MyFrm2.TextBoxCamarero.Text.Length = 0 Or Not IsNumeric(MyFrm2.TextBoxCamarero.Text) Then
            MyFrm2.TextBoxCamarero.Text = "1"
        End If
        '  
        '   IMPORTE COMANDA
        '
        If MyFrm2.LabelTotComanda.Text.Length = 0 Or Not IsNumeric(MyFrm2.LabelTotComanda.Text) Then
            MyFrm2.LabelTotComanda.Text = "0"
        End If
        '
        If ExisteRegistroMESAC(ActualizaMESA, 0) = True Then
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
                        queryString = queryString & "[MESAC].[EFECTIVO]='" & .Mesac_EFECTIVO & "', "
                        queryString = queryString & "[MESAC].[FECHACOBRO]='" & Date.Now.ToShortDateString & "', "
                        queryString = queryString & "[MESAC].[HORACOBRO]='" & Date.Now.ToLongTimeString & "' "
                    End With
                Case 1
                    CobroTOTALMesa = 0
                    With wrMESAC
                        .Mesac_IMPORTE = CobroTOTALMesa.ToString.Replace(",", ".")
                        queryString = queryString & "[MESAC].[IMPORTE]='" & .Mesac_IMPORTE & "' "
                    End With
                Case 2
                    queryString = queryString & "[MESAC].[TKFACIMPRESO]=1 "
                Case 3
                    Dim TotalRestoSEPA As Double =
                        CDbl(MyFrm13.LabelTotComandaSep.Text.Trim.Replace(".", ","))
                    With wrMESAC
                        .Mesac_IMPORTE = TotalRestoSEPA.ToString.Replace(",", ".")
                        queryString = queryString & "[MESAC].[IMPORTE]='" & .Mesac_IMPORTE & "', "
                        queryString = queryString & "[MESAC].[HORAAPAERTURA]='" & HoraMESAC & "' "
                    End With
                Case 4
                    '
                    ' Asigna / Desvincula un Pedido a una mesa.
                    '
                    queryString = queryString & "[MESAC].[TLFPEDIDOS]='" & WMesacTlfPed.Trim & "'"
                Case 51
                    '
                    ' Asigna / Desvincula un Cliente CONTADO a una mesa.
                    ' WMesacNIFCIF As String = ""
                    '
                    queryString = queryString & "[MESAC].[NIFCIF]='" & WMesacNIFCIF.Trim & "'"
                Case 52
                    '
                    ' Asigna / Desvincula un Cliente CREDITO a una mesa.
                    ' WMesacCliCred As Integer = 0
                    '
                    queryString = queryString & "[MESAC].[CLIENTE]=" & wCliente & " "
            End Select
            '
            queryString = queryString & "WHERE "
            queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & "[MESAC].[FECHA]='" & FechaMESAC & "' AND "
            queryString = queryString & "[MESAC].[SALA]='" & wCodSala & "' AND "
            queryString = queryString & "[MESAC].[MESA]='" & ActualizaMESA & "' AND "
            queryString = queryString & "[MESAC].[FACTURA]=" & wFacturaN
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
                                "Comprobar Tabla ACTUALIZANDO Datos [MESAC]")
            End Try
            conexion.Close()
            cmd.Dispose()
            conexion.Dispose()
            '
        End If
        '
    End Sub

    Public Function ExisteRegistroMESAC(ExisteMESAC As String,
                                        ExMesacOPC As Integer,
                                        Optional DatoExtra As String = "") As Boolean
        '
        ' Lectura de registros de CABECERA de MESAS.
        '
        ' DatoExtra = Opcional, Se pasa algun dato para determinadas comprobaciones.
        '
        ' ExMesacOPC =
        '    0 - Lee MESAC por la KEY completa.
        '    1 - Lee MESAC por Número de Factura (Cuando ya tiene Factura asignada).
        '    2 - La finalidad es Obtener el IMPORTE de las MESAS OCUPADAS.
        '        Se llamará para cada MESA OCUPADA, Caja Actual.
        '    3 - La finalidad es Comprobar si UN PEDIDO, esta vinculado a alguna mesa.
        '        DatoExtra = Tlf a comprobar.
        '  999 - Lee MESAC por Número de Factura para ( Cuentas Separadas ).
        '
        ExisteRegistroMESAC = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Comprobar SALA1, para MESAS NORMALES <> 999 / SEPARADAS = 999
        ' Esta comprobacion es importante tanto para FECHA como HORA de Apertura.
        '
        Select Case ExMesacOPC
            Case 999
                '
                ' Mesas SEPARADAS, aqui SALA=999 Siempre.
                ' Solo comprueba si existe (OPC=1), sin procesar el Nro. de Factura.
                '
                If LeeMesa_SALA1("999", ExisteMESAC, 1) = True Then
                    If wMesaLibre = True Then
                        FechaMESAC = Date.Now.ToShortDateString
                        HoraMESAC = Date.Now.ToShortTimeString
                        ActualizaMesa_SALA1(wCaja, "999", MyFrm13.TextBoxSepNumMesa1.Text.Trim, 1)
                    End If
                End If
            Case Else
                If LeeMesa_SALA1(wCodSala, ExisteMESAC, 0) = True Then
                    If wMesaLibre = True Then
                        FechaMESAC = Date.Now.ToShortDateString
                        HoraMESAC = Date.Now.ToShortTimeString
                        ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
                    End If
                End If
        End Select
        '
        Dim ExisteFechaMESAC As String = FechaMESAC
        Dim ComparaFecha As String = ""
        '
        ' Definimos Tipo de Lectura -> (Consulta SQL, SELECT)
        '
        Dim queryString As String = ""
        Select Case ExMesacOPC
            Case 0
                queryString = "SELECT * FROM [MESAC] WHERE "
                queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
                queryString = queryString & "[MESAC].[FECHA]='" & ExisteFechaMESAC & "' AND "
                queryString = queryString & "[MESAC].[SALA]='" & wCodSala & "' AND "
                queryString = queryString & "[MESAC].[MESA]='" & ExisteMESAC & "' AND "
                queryString = queryString & "[MESAC].[FACTURA]=" & wFacturaN
            Case 1
                queryString = "SELECT * FROM [MESAC] WHERE "
                queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
                queryString = queryString & "[MESAC].[MESA]='" & ExisteMESAC & "' AND "
                queryString = queryString & "[MESAC].[FACTURA]=" & wFacturaN
            Case 2
                '
                ' La finalidad es Obtener el IMPORTE de las MESAS OCUPADAS.
                ' Caja / Mesa 
                '
                queryString = "SELECT * FROM [MESAC] WHERE "
                queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
                queryString = queryString & "[MESAC].[MESA]='" & ExisteMESAC & "' "
            Case 3
                '
                '    La finalidad es Comprobar si UN PEDIDO, esta vinculado a alguna mesa.
                '    DatoExtra = Tlf. a comprobar.
                '
                queryString = "SELECT * FROM [MESAC] WHERE "
                queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
                queryString = queryString & "[MESAC].[TLFPEDIDOS]='" & DatoExtra.Trim & "' "
            Case 999
                '
                ' Comprobacion Cabecera MESA [MESAC] para Cuentas Separadas
                '
                queryString = "SELECT * FROM [MESAC] WHERE "
                queryString = queryString & "[MESAC].[NUMCAJA]=" & wCaja & " AND "
                queryString = queryString & "[MESAC].[MESA]='" & ExisteMESAC & "' AND "
                queryString = queryString & "[MESAC].[FACTURA]=" & CInt(MyFrm13.TextBoxSepFactura1.Text.Trim)
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
                    '
                    Select Case ExMesacOPC
                        '
                        ' La finalidad es Obtener el IMPORTE de las MESAS OCUPADAS
                        '
                        Case 2
                            If pRow("NUMCAJA").ToString() = wCaja.ToString And
                               ComparaFecha = ExisteFechaMESAC And
                               pRow("MESA").ToString() = ExisteMESAC Then
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
                                    .Mesac_IMPORTE = pRow("IMPORTE").ToString()
                                    .Mesac_ENTREGA = pRow("ENTREGA").ToString()
                                    .Mesac_TARJETA = pRow("TARJETA").ToString()
                                    .Mesac_VALEDTO = pRow("VALEDTO").ToString()
                                    .Mesac_CHEQUES = pRow("CHEQUES").ToString()
                                    .Mesac_OTROS = pRow("OTROS").ToString()
                                    .Mesac_TLFPEDIDOS = pRow("TLFPEDIDOS").ToString() & ""
                                    .Mesac_NIFCIF = pRow("NIFCIF").ToString() & ""
                                    '
                                    ' Ticket Fra. Impreso?
                                    '
                                    If Not IsDBNull(pRow("TKFACIMPRESO")) Then
                                        .Mesac_TKFACIMPRESO = pRow("TKFACIMPRESO").ToString
                                    Else
                                        .Mesac_TKFACIMPRESO = "False"
                                    End If
                                End With
                                ExisteRegistroMESAC = True
                                Exit For
                            End If
                        Case 3
                            '
                            ' La finalidad es Comprobar si UN PEDIDO, esta vinculado a alguna mesa.
                            ' Si hay registros, hay vinculación.
                            '
                            ExisteRegistroMESAC = True
                            Exit For
                        Case Else
                            If pRow("NUMCAJA").ToString() = wCaja.ToString And
                               ComparaFecha = ExisteFechaMESAC And
                               pRow("MESA").ToString() = ExisteMESAC And
                               pRow("FACTURA").ToString() = wFacturaN.ToString Then
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
                                    .Mesac_IMPORTE = pRow("IMPORTE").ToString()
                                    .Mesac_ENTREGA = pRow("ENTREGA").ToString()
                                    .Mesac_TARJETA = pRow("TARJETA").ToString()
                                    .Mesac_VALEDTO = pRow("VALEDTO").ToString()
                                    .Mesac_CHEQUES = pRow("CHEQUES").ToString()
                                    .Mesac_OTROS = pRow("OTROS").ToString()
                                    .Mesac_TLFPEDIDOS = pRow("TLFPEDIDOS").ToString() & ""
                                    .Mesac_NIFCIF = pRow("NIFCIF").ToString() & ""
                                    '
                                    ' Ticket Fra. Impreso?
                                    '
                                    If Not IsDBNull(pRow("TKFACIMPRESO")) Then
                                        .Mesac_TKFACIMPRESO = pRow("TKFACIMPRESO").ToString
                                    Else
                                        .Mesac_TKFACIMPRESO = "False"
                                    End If
                                End With
                                ExisteRegistroMESAC = True
                                Exit For
                            End If
                    End Select
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

    Public Sub GrabaRegistroMESA(GrabaMESA As String)
        '
        ' MESA :: Datos MESA
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '      ARTI =  GRID1
        '      COMBINA = String COMBINADOS
        '      MEDIAPRECIO = Datos Varios ...
        '
        ' (1) Recorremos la comanda y TOMAMOS SOLO lo ULTIMO FICHADO
        '     ignorando aquellas LÍNEAS que ya existen en la MESA.
        ' (2) Comprobar si un Producto ya se ha servido anteriormente, 
        '     si es asi SUMA UNIDADES, SUMA IMPORTE sobre EXISTENTE.
        ' (3) UPDATE / INSERT Según convenga...    
        '
        ' Validación: 
        '   Vendedor (Camarero)
        '
        If MyFrm2.TextBoxCamarero.Text.Length = 0 Or Not IsNumeric(MyFrm2.TextBoxCamarero.Text) Then
            MyFrm2.TextBoxCamarero.Text = "1"
        End If
        '
        ' Almacen ...
        '
        LeeTCONA4Cfg("Almacen")
        '
        ' Fecha / Hora
        '
        'Dim FechaMESA As String = Date.Now.ToShortDateString
        If LeeMesa_SALA1(wCodSala, GrabaMESA, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        '
        Dim FechaMESA As String = FechaMESAC
        '
        ' MESAC.HORAAPAERTURA -> (Cabecera)         Guarda la Hora de Apertura.
        ' MESA.HORA           -> (Lineas Productos) Guarda la Hora Actual de Fichado.
        '
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
        '   Recorrer las Lineas en el GRID1.
        '   En este momento SOLO debemos grabar las líneas NUEVAS.
        '
        If MyFrm2.GRID1.Rows.Count > 0 Then
            For Each row As DataGridViewRow In MyFrm2.GRID1.Rows
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
                ' Sólo Columnas NUEVAS...
                '
                If row.Cells(5).Value.ToString.Trim IsNot "N" Then
                    Continue For
                End If
                '
                LeeMar(row.Cells(0).Value.ToString.Trim)
                '
                With wrMESA
                    .Mesa_UNID = row.Cells(3).Value.ToString.Replace(",", ".")
                    .Mesa_IMPORTE = row.Cells(4).Value.ToString.Replace(",", ".")
                    .Mesa_PDTO = "0.00"
                    .Mesa_IMPDTO = "0.00"
                    .Mesa_VENDEDOR = CInt(MyFrm2.TextBoxCamarero.Text.Trim)
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
                ' Si El PRODUCTO ya existe, se le SUMAN las UNID, IMPORTE
                '
                queryString = ""
                If ExisteRegistroMESA(GrabaMESA, row.Cells(0).Value.ToString, wStringCombinados, wMediaPrecio) = True Then
                    '
                    ' UPDATE
                    '   Las UNIDAES, IMPORTE se suman a las existentes.
                    '
                    wTmpUnid = CDec(row.Cells(1).Value.ToString.Replace(".", ","))
                    wTmpImporte = CDec(wrMESA.Mesa_IMPORTE.ToString.Replace(".", ","))
                    '
                    queryString = queryString & "UPDATE [MESA] SET "
                    queryString = queryString & "[MESA].[UNID]='" & wTmpUnid.ToString.Replace(",", ".") & "', "
                    queryString = queryString & "[MESA].[IMPORTE]='" & wTmpImporte.ToString.Replace(",", ".") & "', "
                    queryString = queryString & "[MESA].[PDTO]='" & wrMESA.Mesa_PDTO & "', "
                    queryString = queryString & "[MESA].[IMPDTO]='" & wrMESA.Mesa_IMPDTO & "', "
                    queryString = queryString & "[MESA].[VENDEDOR]=" & wrMESA.Mesa_VENDEDOR & ", "
                    queryString = queryString & "[MESA].[HORA]='" & wrMESA.Mesa_HORA & "', "
                    queryString = queryString & "[MESA].[COSTO]='" & wrMESA.Mesa_COSTO & "', "
                    queryString = queryString & "[MESA].[ALMACEN]='" & wrMESA.Mesa_ALMACEN & "', "
                    queryString = queryString & "[MESA].[IGIC]='" & wrMESA.Mesa_IGIC & "', "
                    queryString = queryString & "[MESA].[NOZETA]=" & wrMESA.Mesa_NOZETA & ", "
                    queryString = queryString & "[MESA].[NUMZETA]=" & 0 & ", "
                    queryString = queryString & "[MESA].[ORDENPLATO]=" & wrMESA.Mesa_ORDENPLATO & " "
                    queryString = queryString & "WHERE "
                    queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " AND "
                    queryString = queryString & "[MESA].[FECHA]='" & FechaMESA & "' AND "
                    queryString = queryString & "[MESA].[SALA]='" & wCodSala & "' AND "
                    queryString = queryString & "[MESA].[MESA]='" & GrabaMESA & "' AND "
                    queryString = queryString & "[MESA].[FACTURA]=" & wFacturaN & " AND "
                    queryString = queryString & "[MESA].[ARTI]='" & row.Cells(0).Value.ToString.Trim & "' AND "
                    queryString = queryString & "[MESA].[COMBINA]='" & wStringCombinados & "' AND "
                    queryString = queryString & "[MESA].[MEDIAPRECIO]='" & wMediaPrecio & "' "
                Else
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
                    queryString = queryString & "'" & FechaMESA & "', "
                    queryString = queryString & "'" & wCodSala & "', "
                    queryString = queryString & "'" & GrabaMESA & "', "
                    queryString = queryString & wFacturaN & ", "
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
                End If
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

    Public Sub ActualizaDatosMESA(GrabaMESA As String, FechaMesa As String, wArticulo As String,
                                  wStringCombi As String, wMediaPre As String,
                                  Optional wOpc As Integer = 0)
        '
        ' Procediento para actualizar DATOS Concretos en Líneas de [MESA]
        ' Ej.. Actualizar el IMPORTE, por Modificación del PVP.
        '
        ' MESA :: Datos MESA
        '   Key:
        '      NUMCAJA = wCAja
        '      FECHA = Fecha Día
        '      MESA = GrabaMESA
        '      FACTURA = wFacturaN
        '      ARTI =  GRID1
        '      COMBINA = String COMBINADOS
        '      MEDIAPRECIO = Datos Varios ...
        '
        ' wOpc :: Indica que datos actualizamos. (0=Nada, Salir !)
        '
        If wOpc = 0 Then
            Exit Sub
        End If
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' UPDATE
        '
        queryString = ""
        queryString = queryString & "UPDATE [MESA] SET "
        '
        Select Case wOpc
            Case 1
                queryString = queryString & "[MESA].[IMPORTE]='" & NewImpo.ToString.Trim.Replace(",", ".") & "' "
        End Select
        '
        queryString = queryString & "WHERE "
        queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " AND "
        queryString = queryString & "[MESA].[FECHA]='" & FechaMesa & "' AND "
        queryString = queryString & "[MESA].[SALA]='" & wCodSala & "' AND "
        queryString = queryString & "[MESA].[MESA]='" & GrabaMESA & "' AND "
        queryString = queryString & "[MESA].[FACTURA]=" & wFacturaN & " AND "
        queryString = queryString & "[MESA].[ARTI]='" & wArticulo & "' AND "
        queryString = queryString & "[MESA].[COMBINA]='" & wStringCombi & "' AND "
        queryString = queryString & "[MESA].[MEDIAPRECIO]='" & wMediaPre & "' "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                               MsgBoxStyle.Exclamation Or
                               MsgBoxStyle.OkOnly,
                               "Comprobar Tabla, Actualizando Datos [MESA]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub



    Public Function ExisteRegistroMESA(ExisteMESA As String,
                                       ExisteARTI As String,
                                       ExisteCOMBI As String,
                                       ExisteMediaPre As String) As Boolean
        '
        ' Lectura de registros ('LINEAS') de DATOS en MESA
        '
        ExisteRegistroMESA = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Fecha
        '
        'Dim ExisteFechaMESA As String = Date.Now.ToShortDateString
        If LeeMesa_SALA1(wCodSala, ExisteMESA, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        Dim ExisteFechaMESA As String = FechaMESAC
        Dim ComparaFecha As String = ""
        '
        Dim queryString As String = ""
        queryString = "SELECT * FROM [MESA] WHERE "
        queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " AND "
        queryString = queryString & "[MESA].[FECHA]='" & ExisteFechaMESA & "' AND "
        queryString = queryString & "[MESA].[SALA]='" & wCodSala & "' AND "
        queryString = queryString & "[MESA].[MESA]='" & ExisteMESA & "' AND "
        queryString = queryString & "[MESA].[FACTURA]=" & wFacturaN & " AND "
        queryString = queryString & "[MESA].[ARTI]='" & ExisteARTI & "' AND "
        queryString = queryString & "[MESA].[COMBINA]='" & ExisteCOMBI.Trim & "' AND "
        queryString = queryString & "[MESA].[MEDIAPRECIO]='" & ExisteMediaPre & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESA")
            '
            If dt.Tables("MESA").Rows.Count > 0 Then
                '
                ' Recogemos datos ...
                ' UNID / IMPORTE
                '
                With wrLeeMESA
                    .Mesa_UNID = dt.Tables("MESA").Rows(0).Item("UNID").ToString().Replace(",", ".")
                    .Mesa_IMPORTE = dt.Tables("MESA").Rows(0).Item("IMPORTE").ToString().Replace(",", ".")
                End With
                ExisteRegistroMESA = True
                '
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [MESA]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Sub BorraRegistroMESA(ExisteMESA As String,
                                  ExisteARTI As String,
                                  ExisteCOMBI As String,
                                  ExisteMediaPre As String)
        '
        ' Este procedimiento BORRA lineas de la tabla MESA.
        ' Nolmalmente desde el botón [Borrar Linea] o Botón [-] del GRID1,
        '   cuando el registro esta grabado (Unidades Existentes).
        ' Se dara este caso Solo si las Unid. Existentes 
        '    quedan a CERO !!!
        '
        Dim ExisteFechaMESA As String = FechaMESAC
        Dim ComparaFecha As String = "" : Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        If LeeMesa_SALA1(wCodSala, ExisteMESA, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Borrado de la Línea de [MESA]
        '
        queryString = "Delete [MESA]"
        queryString = queryString & " WHERE"
        queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " AND "
        queryString = queryString & "[MESA].[FECHA]='" & ExisteFechaMESA & "' AND "
        queryString = queryString & "[MESA].[SALA]='" & wCodSala & "' AND "
        queryString = queryString & "[MESA].[MESA]='" & ExisteMESA & "' AND "
        queryString = queryString & "[MESA].[FACTURA]=" & wFacturaN & " AND "
        queryString = queryString & "[MESA].[ARTI]='" & ExisteARTI & "' AND "
        queryString = queryString & "[MESA].[COMBINA]='" & ExisteCOMBI.Trim & "' AND "
        queryString = queryString & "[MESA].[MEDIAPRECIO]='" & ExisteMediaPre & "' "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [MESA], Borrando Registro.")
        End Try
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Public Sub BorraTKFavoritos(MiId As Integer)
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        queryString = "Delete [TKFAVORITOS] WHERE [TKFAVORITOS].[ID]=" & MiId
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [TKFAVORITOS], Borrando Registro.")
        End Try
        '
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Public Sub BorraPedido(wBorTlf As String)
        '
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        queryString = "Delete [PEDCLIE] WHERE [PEDCLIE].[TELEFONO]='" & wBorTlf.Trim & "'"
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [PEDCLIE], Borrando Registro.")
        End Try
        '
        conexion.Close()
        '
        ' Liberar Recursos
        '
        conexion.Dispose()
        cmd.Dispose()
        '
    End Sub

    Public Sub Actualiza_TCONA4(wActCajaRefer As Integer, wActOPC As String)
        '
        '   Actualizamos Datos en Referencias Generales.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        '   wActOPC, determinará que Datos Actualizamos.
        '
        Dim queryString As String = ""
        Select Case wActOPC
            Case "X"
                wrLeeTCONA4.Tcona4_NUMX += 1
                queryString = "UPDATE [TCONA4] "
                queryString = queryString & "SET [TCONA4].[NUMX]=" & wrLeeTCONA4.Tcona4_NUMX
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "Z"
                wrLeeTCONA4.Tcona4_NUMZ += 1
                queryString = "UPDATE [TCONA4] "
                queryString = queryString & "SET [TCONA4].[NUMZ]=" & wrLeeTCONA4.Tcona4_NUMZ
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "FecDia" ' Para la Z (Fecha dia, Inicio Sesion)
                queryString = "UPDATE [TCONA4] "
                queryString = queryString & "SET [TCONA4].[FECHADIASESION]='" & Date.Now.ToShortDateString & "'"
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "FecZ" ' Para la Z, (Fecha Z == Fecha dia)
                queryString = "UPDATE [TCONA4] "
                queryString = queryString & "SET [TCONA4].[FECHAZ]='" & wrLeeTCONA4.Tcona4_FECHADIASESION.Trim & "'"
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "Factura"
                ' 
                ' Ojo!!! Si la mesa existiera
                '  lo mas probable es que Esto haya que quitarlo
                '  o podría Duplicar el CONTADOR.
                '
                If FormularioInicial = 1 Then
                    wFacturaN += 1
                End If
                '
                queryString = "UPDATE [TCONA4] "
                queryString = queryString & "SET [TCONA4].[FACTURA]=" & wFacturaN
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "FacturaSep"
                ' 
                ' Al separar cuentas, hay que que crear MESA NUEVA y asignar Nro. de Factura.
                ' El numero de Factura YA ha sido Leido e Incrementado en 1.
                ' Aqui solo actualizamos.
                '
                queryString = "UPDATE [TCONA4] "
                queryString = queryString & "SET [TCONA4].[FACTURA]=" & wFacturaNSep
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "Teclado"
                ' 
                ' Se actualizan Frases Favoritas Para el teclado Flotante
                '
                queryString = "UPDATE [TCONA4] SET "
                queryString = queryString & "[TCONA4].[TECLADOFAV1]='" & MyFrm15.TextBoxMensaFAV1.Text.Trim & "', "
                queryString = queryString & "[TCONA4].[TECLADOFAV2]='" & MyFrm15.TextBoxMensaFAV2.Text.Trim & "', "
                queryString = queryString & "[TCONA4].[TECLADOFAV3]='" & MyFrm15.TextBoxMensaFAV3.Text.Trim & "', "
                queryString = queryString & "[TCONA4].[TECLADOFAV4]='" & MyFrm15.TextBoxMensaFAV4.Text.Trim & "', "
                queryString = queryString & "[TCONA4].[TECLADOFAV5]='" & MyFrm15.TextBoxMensaFAV5.Text.Trim & "' "
                queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
            Case "RefGen"
                With MyFrm6
                    queryString = "UPDATE [TCONA4] SET"
                    '
                    queryString = queryString & " [TCONA4].[NOMBRECAJA]='" & .TextBoxNombreCaja.Text.Trim & "',"
                    '
                    ' Colores de la aplicación.
                    '
                    queryString = queryString & " [TCONA4].[COLORFTCONA401]=" & .GRIDColores.Rows(0).Cells(2).Style.BackColor.ToArgb & ","
                    queryString = queryString & " [TCONA4].[COLORFTCONA402]=" & .GRIDColores.Rows(1).Cells(2).Style.BackColor.ToArgb & ","
                    queryString = queryString & " [TCONA4].[COLORFF]=" & .GRIDColores.Rows(2).Cells(2).Style.BackColor.ToArgb & ","
                    queryString = queryString & " [TCONA4].[COLORLF]=" & .GRIDColores.Rows(3).Cells(2).Style.BackColor.ToArgb & ","
                    queryString = queryString & " [TCONA4].[COLORFA]=" & .GRIDColores.Rows(4).Cells(2).Style.BackColor.ToArgb & ","
                    queryString = queryString & " [TCONA4].[COLORLA]=" & .GRIDColores.Rows(5).Cells(2).Style.BackColor.ToArgb & ","
                    '
                    ' Datos TICKET Factura:
                    ' Lineas Cab, Pie, Logo S/N
                    '
                    If .CheckBoxLogoTKF.Checked = False Then
                        queryString = queryString & " [TCONA4].[TKFACLOGO]=" & 0 & ","
                    Else
                        queryString = queryString & " [TCONA4].[TKFACLOGO]=" & 1 & ","
                    End If
                    '
                    ' Logo X/Z
                    '
                    If .CheckBoxLogoTKXZ.Checked = False Then
                        queryString = queryString & " [TCONA4].[TKZETALOGO]=" & 0 & ","
                    Else
                        queryString = queryString & " [TCONA4].[TKZETALOGO]=" & 1 & ","
                    End If
                    queryString = queryString & " [TCONA4].[TKFCABLIN1]='" & .TextBoxLinCab1.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN2]='" & .TextBoxLinCab2.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN3]='" & .TextBoxLinCab3.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN4]='" & .TextBoxLinCab4.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN5]='" & .TextBoxLinCab5.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN6]='" & .TextBoxLinCab6.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN7]='" & .TextBoxLinCab7.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN8]='" & .TextBoxLinCab8.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN9]='" & .TextBoxLinCab9.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFCABLIN10]='" & .TextBoxLinCab10.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN1]='" & .TextBoxLinPie1.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN2]='" & .TextBoxLinPie2.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN3]='" & .TextBoxLinPie3.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN4]='" & .TextBoxLinPie4.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN5]='" & .TextBoxLinPie5.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN6]='" & .TextBoxLinPie6.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN7]='" & .TextBoxLinPie7.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN8]='" & .TextBoxLinPie8.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN9]='" & .TextBoxLinPie9.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN10]='" & .TextBoxLinPie10.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN11]='" & .TextBoxLinPie11.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN12]='" & .TextBoxLinPie12.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN13]='" & .TextBoxLinPie13.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN14]='" & .TextBoxLinPie14.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN15]='" & .TextBoxLinPie15.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN16]='" & .TextBoxLinPie16.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN17]='" & .TextBoxLinPie17.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN18]='" & .TextBoxLinPie18.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN19]='" & .TextBoxLinPie19.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFPIELIN20]='" & .TextBoxLinPie20.Text & "',"
                    '
                    ' IGIC TK Factura
                    '
                    If .TextBoxIGICTKFAC.Text.Trim.Length = 0 Then
                        .TextBoxIGICTKFAC.Text = "0"
                    End If
                    queryString = queryString & " [TCONA4].[TKFACIGIC]='" & .TextBoxIGICTKFAC.Text.Trim.Replace(",", ".") & "',"
                    '
                    ' Varios
                    '
                    '    Pide Vendedor? S/N
                    '
                    If .CheckBoxPVendedores.Checked = False Then
                        queryString = queryString & " [TCONA4].[PIDEVENDEDOR]=" & 0 & ","
                    Else
                        queryString = queryString & " [TCONA4].[PIDEVENDEDOR]=" & 1 & ","
                    End If
                    '
                    '    Separa Raciones? S/N
                    '
                    If .CheckBoxSeparaRacion.Checked = False Then
                        queryString = queryString & " [TCONA4].[SEPARARACIONES]=" & 0 & ","
                    Else
                        queryString = queryString & " [TCONA4].[SEPARARACIONES]=" & 1 & ","
                    End If
                    '
                    ' FAMILIAS 1, 2, 3 (1/2 Raciones)
                    '
                    queryString = queryString & " [TCONA4].[VARIOSFAM1]='" & .TextBoxRacionFAM1.Text.Trim & "',"
                    queryString = queryString & " [TCONA4].[VARIOSFAM2]='" & .TextBoxRacionFAM2.Text.Trim & "',"
                    queryString = queryString & " [TCONA4].[VARIOSFAM3]='" & .TextBoxRacionFAM3.Text.Trim & "',"
                    '
                    ' Para Evitar Posible errores en la conversion a Cint
                    '
                    If .TextBoxNumX.Text.Trim.Length = 0 Then
                        .TextBoxNumX.Text = "1"
                    End If
                    If .TextBoxNumZ.Text.Trim.Length = 0 Then
                        .TextBoxNumZ.Text = "1"
                    End If
                    If .TextBoxEmpresa.Text.Trim.Length = 0 Then
                        .TextBoxEmpresa.Text = "1"
                    End If
                    If .TextBoxNumFactura.Text.Trim.Length = 0 Then
                        .TextBoxNumFactura.Text = "1"
                    End If
                    If .TextBoxAlmacen.Text.Trim.Length = 0 Then
                        .TextBoxAlmacen.Text = "1"
                    End If
                    If .TextBoxFRMInicial.Text.Trim.Length = 0 Then
                        .TextBoxFRMInicial.Text = "0"
                    End If
                    If .TextBoxTipoPvp.Text.Trim.Length = 0 Then
                        .TextBoxTipoPvp.Text = "1"
                    End If
                    '
                    ' Nro X, Nro Z
                    '
                    queryString = queryString & " [TCONA4].[NUMX]=" & CInt(.TextBoxNumX.Text.Trim) & ","
                    queryString = queryString & " [TCONA4].[NUMZ]=" & CInt(.TextBoxNumZ.Text.Trim) & ","
                    ' 
                    ' EMPRESA, FACTURA, ALMACEN, ORDENFAM, ORDENART, FORMINICIAL
                    '
                    queryString = queryString & " [TCONA4].[EMPRESA]=" & CInt(.TextBoxEmpresa.Text.Trim) & ","
                    queryString = queryString & " [TCONA4].[FACTURA]=" & CInt(.TextBoxNumFactura.Text.Trim) & ","
                    queryString = queryString & " [TCONA4].[ALMACEN]=" & CInt(.TextBoxAlmacen.Text.Trim) & ","
                    queryString = queryString & " [TCONA4].[FORMINICIAL]=" & CInt(.TextBoxFRMInicial.Text.Trim) & ","
                    queryString = queryString & " [TCONA4].[TIPOPVPARTI]=" & CInt(.TextBoxTipoPvp.Text.Trim) & ","
                    '
                    '    Orden Articulos  0/1
                    '
                    If .CheckBoxOrdenArt.Checked = False Then
                        queryString = queryString & " [TCONA4].[ORDENART]=" & 0 & ","
                    Else
                        queryString = queryString & " [TCONA4].[ORDENART]=" & 1 & ","
                    End If
                    '
                    '    Orden Familias  0/1
                    '
                    If .CheckBoxOrdenFam.Checked = False Then
                        queryString = queryString & " [TCONA4].[ORDENFAM]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[ORDENFAM]=" & 1 & ", "
                    End If
                    '
                    '    Botonera Familias/Articulos  0/1
                    '
                    If .CheckBoxBotoneraSN.Checked = False Then
                        queryString = queryString & " [TCONA4].[REFRESCABOTONES]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[REFRESCABOTONES]=" & 1 & ", "
                    End If
                    '
                    '    Zeta Con Mesas Ocupadas 0 / 1=SI
                    '
                    If .CheckBoxZetaOcu.Checked = False Then
                        queryString = queryString & " [TCONA4].[ZETAMESASOCU]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[ZETAMESASOCU]=" & 1 & ", "
                    End If
                    '
                    ' TKZETALOGO = OK = se le da valor mas arriba
                    '
                    '    Splash Screen 0 / 1=SI
                    '
                    If .CheckBoxSplahScreen.Checked = False Then
                        queryString = queryString & " [TCONA4].[SPLASHSCREEN]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[SPLASHSCREEN]=" & 1 & ", "
                    End If
                    '
                    ' Retardo Splash Screen (2 segundos defecto...)
                    '
                    If .TextBoxRetardoSplash.Text.Trim.Length = 0 Or .TextBoxRetardoSplash.Text.Trim = "0" Then
                        .TextBoxRetardoSplash.Text = "2000"
                    End If
                    queryString = queryString & " [TCONA4].[SPLASHRETARDO]=" & CInt(.TextBoxRetardoSplash.Text.Trim) & ", "
                    '
                    '    Pide PAX? 0 / 1=SI
                    '
                    If .CheckBoxPidePAX.Checked = False Then
                        queryString = queryString & " [TCONA4].[PIDEPAX]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[PIDEPAX]=" & 1 & ", "
                    End If
                    '
                    '    FAVORITOS
                    '
                    If .CheckBoxCargarFav.Checked = False Then
                        queryString = queryString & " [TCONA4].[CARGAFAVORITOS]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[CARGAFAVORITOS]=" & 1 & ", "
                    End If
                    queryString = queryString & " [TCONA4].[BOTONFAVORITO]='" & wrLeeTCONA4.Tcona4_BOTONFAVORITO.Trim & "', "
                    '
                    ' Nombre Tarifas
                    '
                    queryString = queryString & " [TCONA4].[NOMTARIPVP1]='" & .TextBoxTPVP1.Text.Trim & "', "
                    queryString = queryString & " [TCONA4].[NOMTARIPVP2]='" & .TextBoxTPVP2.Text.Trim & "', "
                    queryString = queryString & " [TCONA4].[NOMTARIPVP5]='" & .TextBoxTPVP5.Text.Trim & "', "
                    queryString = queryString & " [TCONA4].[NOMTARIPVP6]='" & .TextBoxTPVP6.Text.Trim & "', "
                    queryString = queryString & " [TCONA4].[NOMTARIPVP7]='" & .TextBoxTPVP7.Text.Trim & "', "
                    queryString = queryString & " [TCONA4].[NOMTARIPVP8]='" & .TextBoxTPVP8.Text.Trim & "', "
                    queryString = queryString & " [TCONA4].[NOMTARIPVP9]='" & .TextBoxTPVP9.Text.Trim & "', "
                    '
                    ' COBVIEW Previsualiza S/N
                    ' MODELO de IMPRESORA para trabajar con la aplicacion.
                    '
                    If .CheckBoxCOBVIEWPDSN.Checked = False Then
                        queryString = queryString & " [TCONA4].[COBVIEWPDSN]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[COBVIEWPDSN]=" & 1 & ", "
                    End If
                    queryString = queryString & " [TCONA4].[MODIMPREFIJO]='" & .LabelMODELOFijado.Text.Trim & "', "
                    '
                    ' Impresion TK FACTURA? S/N
                    '
                    If .CheckBoxImpreFACTURA.Checked = False Then
                        queryString = queryString & " [TCONA4].[IMPRIMETKFAC]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[IMPRIMETKFAC]=" & 1 & ", "
                    End If
                    '
                    ' Importe Min. Impresion TK Factura
                    '
                    If .TextBoxImpoMimImpreTKFAC.Text.Trim.Length = 0 Then
                        .TextBoxImpoMimImpreTKFAC.Text = "0"
                    End If
                    queryString = queryString & " [TCONA4].[IMPOMINIMPRE]='" & .TextBoxImpoMimImpreTKFAC.Text.Trim.Replace(",", ".") & "',"
                    '
                    ' Salto Líneas TK AREAS
                    '
                    If .TextBoxNumSaltoLineas.Text.Trim.Length = 0 Then
                        .TextBoxNumSaltoLineas.Text = "0"
                    End If
                    queryString = queryString & " [TCONA4].[SALTOLINPIETK]='" & .TextBoxNumSaltoLineas.Text.Trim.Replace(",", ".") & "', "
                    '
                    ' Abre Cajón en Impresion TK FACTURA? S/N
                    '
                    If .CheckBoxImpreFACCajon.Checked = False Then
                        queryString = queryString & " [TCONA4].[TKFACABRECAJON]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[TKFACABRECAJON]=" & 1 & ", "
                    End If
                    queryString = queryString & " [TCONA4].[TKFACPUERTO]='" & .TextBoxTKFACPtoCaptura.Text.Trim & "', "
                    '
                    'TK FACTURA Detalle Combinados S/N
                    '
                    If .CheckBoxTKFACDetCombi.Checked = False Then
                        queryString = queryString & " [TCONA4].[TKFACIMPDETCOMBI]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[TKFACIMPDETCOMBI]=" & 1 & ", "
                    End If
                    '
                    'Comprobar Impresora al INICIO S/N
                    '
                    If .CheckBoxMiraImpreIni.Checked = False Then
                        queryString = queryString & " [TCONA4].[COMPRUIMPREINI]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[COMPRUIMPREINI]=" & 1 & ", "
                    End If
                    '
                    ' Detalles Tickets Fac, X y Z
                    '
                    queryString = queryString & " [TCONA4].[TKFDETLIN1]='" & .TextBoxDetCab1.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFDETLIN2]='" & .TextBoxDetCab2.Text & "',"
                    queryString = queryString & " [TCONA4].[TKFDETLIN3]='" & .TextBoxDetCab3.Text & "',"
                    queryString = queryString & " [TCONA4].[TKXZDETLIN1]='" & .TextBoxDetCab4.Text & "',"
                    queryString = queryString & " [TCONA4].[TKXZDETLIN2]='" & .TextBoxDetCab5.Text & "',"
                    queryString = queryString & " [TCONA4].[TKXZDETLIN3]='" & .TextBoxDetCab6.Text & "', "
                    '
                    '    Orden Articulos de la MESA (GRID1)  0 / 1=Por Familias
                    '
                    If .CheckBoxOrdenProdu.Checked = False Then
                        queryString = queryString & " [TCONA4].[ORDENPRODUCTOS]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[ORDENPRODUCTOS]=" & 1 & ", "
                    End If
                    '
                    '    Pide CAJA al inicio? 0/1
                    '
                    If .CheckBoxPCajaInicial.Checked = False Then
                        queryString = queryString & " [TCONA4].[PIDECAJAINICIO]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[PIDECAJAINICIO]=" & 1 & ", "
                    End If
                    '
                    '    Tipo Impresion TK. FAC? 0 Windows / 1 Directa
                    '
                    If .CheckBoxImpreTKFACDirWin.Checked = False Then
                        queryString = queryString & " [TCONA4].[TKFACDIRWIN]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[TKFACDIRWIN]=" & 1 & ", "
                    End If
                    '
                    '    Confirmar BORRADO lineas Cuenta Mesa
                    '    0 No / 1 Si
                    '
                    If .CheckBoxBorLinCtaMesa.Checked = False Then
                        queryString = queryString & " [TCONA4].[BORLINCUENTA]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[BORLINCUENTA]=" & 1 & ", "
                    End If
                    '
                    '    Crear Clientes Crédito? / 0 No / 1 Si
                    '
                    If .CheckBoxCreaCliConta.Checked = False Then
                        queryString = queryString & " [TCONA4].[CREACLICREDITO]=" & 0 & ", "
                    Else
                        queryString = queryString & " [TCONA4].[CREACLICREDITO]=" & 1 & ", "
                    End If
                    '
                    '    Efecto FADEOUT al salir? / 0 No / 1 Si
                    '
                    If .CheckBoxFADEOUT.Checked = False Then
                        queryString = queryString & " [TCONA4].[FADEOUTSALIR]=" & 0 & " "
                    Else
                        queryString = queryString & " [TCONA4].[FADEOUTSALIR]=" & 1 & " "
                    End If
                    '
                    ' Where ...
                    '
                    queryString = queryString & " WHERE [TCONA4].[CAJA]=" & wActCajaRefer
                End With
        End Select
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Error: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Grabando Datos [TCONA4]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub GrabaTkFavoritos()
        '
        ' Graba Textos Favoritos Para FORM MENSAJES a AREAS
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        queryString = ""
        '
        queryString = queryString & "INSERT INTO [TKFAVORITOS] ("
        queryString = queryString & "[TKFAVORITOS].[TEXTO1],"
        queryString = queryString & "[TKFAVORITOS].[TEXTO2],"
        queryString = queryString & "[TKFAVORITOS].[TEXTO3],"
        queryString = queryString & "[TKFAVORITOS].[TEXTO4] "
        queryString = queryString & ") Values ("
        queryString = queryString & "'" & MyFrm18.TextBoxMensaL1.Text.Trim & "', "
        queryString = queryString & "'" & MyFrm18.TextBoxMensaL2.Text.Trim & "', "
        queryString = queryString & "'" & MyFrm18.TextBoxMensaL3.Text.Trim & "', "
        queryString = queryString & "'" & MyFrm18.TextBoxMensaL4.Text.Trim & "' "
        queryString = queryString & ")"
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Comprobar Tabla, Grabando Datos [TKFAVORITOS]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub


    Public Sub ActualizaMesa_SALA1(wActMesaCaja As Integer,
                                   wActMesaSala As String,
                                   wActMesa As String,
                                   WoPci As Integer)
        '
        ' WoPci:
        '    0 :: Actualiza FACTURA y VENDEDOR
        '    1 :: Actualiza la FECHA/HORA de APERTURA cuando corresponda.
        '    2 :: Si se permite hacer Z con mesas OCUPADAS, 
        '         se liberan (DESOCUPAN) desde aquí
        '    3 :: FACTURA=0, VENDEDOR=0, (Cambio Mesa, Solo para Origen)
        '    4 :: Actualiza el Numero de Personas en la mesa. (MyFrm2.TextBoxPax.Text)
        ' 
        '   Key:
        '     CAJA = wActMesaCaja
        '     CODIGO (SALA) = wActMesaSala
        '     MESA = wActMesa
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        Select Case WoPci
            Case 0
                ' La MESA actual pasa a estar 
                '    OCUPADA - Se le asigna un Nro. de FACTURA.
                '    LIBRE   - Se le asigna FACTURA = 0.
                ' wFacturaN, es variable Global. 
                '-------------------------------------------------------
                ' Nro de Factura, Valor Numerico o CERO
                ' Vendedor,       Valor Numerico o CERO
                '
                queryString &= "UPDATE [SALA1] Set "
                queryString &= "[SALA1].[FACTURA]=" & wFacturaN & ", "
                '
                ' Si FACTURA = 0 Libera la MESA:
                '  FACTURA  = 0
                '  VENDEDOR = 0
                '  PAX      = 0
                '
                If wFacturaN = 0 Then
                    queryString &= "[SALA1].[VENDEDOR]=0, "
                    queryString &= "[SALA1].[PAX]=0 "
                Else
                    queryString &= "[SALA1].[VENDEDOR]=" & CInt(MyFrm2.TextBoxCamarero.Text.Trim) & ", "
                    queryString &= "[SALA1].[PAX]=" & CInt(MyFrm2.TextBoxPax.Text.Trim) & " "
                End If
                queryString &= "WHERE "
                queryString &= "[SALA1].[CAJA]=" & wActMesaCaja & " AND "
                queryString &= "[SALA1].[CODIGO]='" & wActMesaSala & "' AND "
                queryString &= "[SALA1].[MESA]='" & wActMesa & "' "
            Case 1
                '
                ' Establece la Fecha / Hora de Apertura en la mesa
                '
                queryString &= "UPDATE [SALA1] Set "
                queryString &= "[SALA1].[FECAPERTURA]='" & FechaMESAC & "', "
                queryString &= "[SALA1].[HORAAPAERTURA]='" & HoraMESAC & "' "
                queryString &= "WHERE "
                queryString &= "[SALA1].[CAJA]=" & wActMesaCaja & " AND "
                queryString &= "[SALA1].[CODIGO]='" & wActMesaSala & "' AND "
                queryString &= "[SALA1].[MESA]='" & wActMesa & "' "
            Case 2
                '
                ' Desde la Z
                '  Libera TODAS las mesas (Caja Actual)
                '
                queryString &= "UPDATE [SALA1] Set "
                queryString &= "[SALA1].[FACTURA]=0, "
                queryString &= "[SALA1].[PAX]=0, "
                queryString &= "[SALA1].[VENDEDOR]=0 "
                queryString &= "WHERE "
                queryString &= "[SALA1].[CAJA]=" & wActMesaCaja
            Case 3
                '-------------------------------------------------------
                ' Cambio Mesa.
                '    Nro de Factura=0, Vendedor=0, PAX(Personas)=0
                '    Obviamos Fecha Sesion, No afecta ...
                '
                queryString &= "UPDATE [SALA1] SET "
                queryString &= "[SALA1].[FACTURA]=0, "
                queryString &= "[SALA1].[VENDEDOR]=0, "
                queryString &= "[SALA1].[PAX]=0 "
                queryString &= "WHERE "
                queryString &= "[SALA1].[CAJA]=" & wActMesaCaja & " AND "
                queryString &= "[SALA1].[CODIGO]='" & wActMesaSala & "' AND "
                queryString &= "[SALA1].[MESA]='" & wActMesa & "' "
            Case 4
                '-------------------------------------------------------
                ' Número de Personas en la mesa (PAX).
                '
                queryString &= "UPDATE [SALA1] SET "
                queryString &= "[SALA1].[PAX]=" & CInt(MyFrm2.TextBoxPax.Text.Trim) & " "
                queryString &= "WHERE "
                queryString &= "[SALA1].[CAJA]=" & wActMesaCaja & " AND "
                queryString &= "[SALA1].[CODIGO]='" & wActMesaSala & "' AND "
                queryString &= "[SALA1].[MESA]='" & wActMesa & "' "
        End Select
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

    Public Sub CreaTCONA4Cfg(wCreaCfgCaja As Integer)
        '
        ' Creamos el registro de Ref. Generales si no existe.
        ' Se asignan Valores Por Defecto a sus CAMPOS.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        queryString = "Insert Into [TCONA4] ("
        queryString = queryString & " [CAJA],"
        queryString = queryString & " [NOMBRECAJA],"
        queryString = queryString & " [COLORFTCONA401],"
        queryString = queryString & " [COLORFTCONA402],"
        queryString = queryString & " [COLORFF],"
        queryString = queryString & " [COLORLF],"
        queryString = queryString & " [COLORFA],"
        queryString = queryString & " [COLORLA],"
        queryString = queryString & " [EMPRESA],"
        queryString = queryString & " [IGIC],"
        queryString = queryString & " [FACTURA],"
        queryString = queryString & " [ALMACEN],"
        queryString = queryString & " [FORMINICIAL],"
        queryString = queryString & " [TKFACLOGO],"
        queryString = queryString & " [TKFCABLIN1],"
        queryString = queryString & " [TKFCABLIN2],"
        queryString = queryString & " [TKFCABLIN3],"
        queryString = queryString & " [TKFCABLIN4],"
        queryString = queryString & " [TKFCABLIN5],"
        queryString = queryString & " [TKFCABLIN6],"
        queryString = queryString & " [TKFCABLIN7],"
        queryString = queryString & " [TKFCABLIN8],"
        queryString = queryString & " [TKFCABLIN9],"
        queryString = queryString & " [TKFCABLIN10],"
        queryString = queryString & " [TKFPIELIN1],"
        queryString = queryString & " [TKFPIELIN2],"
        queryString = queryString & " [TKFPIELIN3],"
        queryString = queryString & " [TKFPIELIN4],"
        queryString = queryString & " [TKFPIELIN5],"
        queryString = queryString & " [TKFPIELIN6],"
        queryString = queryString & " [TKFPIELIN7],"
        queryString = queryString & " [TKFPIELIN8],"
        queryString = queryString & " [TKFPIELIN9],"
        queryString = queryString & " [TKFPIELIN10],"
        queryString = queryString & " [TKFPIELIN11],"
        queryString = queryString & " [TKFPIELIN12],"
        queryString = queryString & " [TKFPIELIN13],"
        queryString = queryString & " [TKFPIELIN14],"
        queryString = queryString & " [TKFPIELIN15],"
        queryString = queryString & " [TKFPIELIN16],"
        queryString = queryString & " [TKFPIELIN17],"
        queryString = queryString & " [TKFPIELIN18],"
        queryString = queryString & " [TKFPIELIN19],"
        queryString = queryString & " [TKFPIELIN20],"
        queryString = queryString & " [TKFACIGIC],"
        queryString = queryString & " [PIDEVENDEDOR],"
        queryString = queryString & " [SEPARARACIONES],"
        queryString = queryString & " [VARIOSFAM1],"
        queryString = queryString & " [VARIOSFAM2],"
        queryString = queryString & " [VARIOSFAM3],"
        queryString = queryString & " [NUMX],"
        queryString = queryString & " [NUMZ],"
        queryString = queryString & " [REFRESCABOTONES],"
        queryString = queryString & " [TIPOPVPARTI],"
        queryString = queryString & " [ZETAMESASOCU],"
        queryString = queryString & " [TKZETALOGO],"
        queryString = queryString & " [SPLASHSCREEN],"
        queryString = queryString & " [SPLASHRETARDO],"
        queryString = queryString & " [PIDEPAX],"
        queryString = queryString & " [CARGAFAVORITOS],"
        queryString = queryString & " [BOTONFAVORITO],"
        queryString = queryString & " [NOMTARIPVP1],"
        queryString = queryString & " [NOMTARIPVP2],"
        queryString = queryString & " [NOMTARIPVP5],"
        queryString = queryString & " [NOMTARIPVP6],"
        queryString = queryString & " [NOMTARIPVP7],"
        queryString = queryString & " [NOMTARIPVP8],"
        queryString = queryString & " [NOMTARIPVP9],"
        queryString = queryString & " [COBVIEWPDSN],"
        queryString = queryString & " [MODIMPREFIJO],"
        queryString = queryString & " [IMPRIMETKFAC],"
        queryString = queryString & " [IMPOMINIMPRE],"
        queryString = queryString & " [SALTOLINPIETK],"
        queryString = queryString & " [TKFACABRECAJON],"
        queryString = queryString & " [TKFACPUERTO],"
        queryString = queryString & " [TKFACIMPDETCOMBI],"
        queryString = queryString & " [COMPRUIMPREINI], "
        queryString = queryString & " [TECLADOFAV1],"
        queryString = queryString & " [TECLADOFAV2],"
        queryString = queryString & " [TECLADOFAV3],"
        queryString = queryString & " [TECLADOFAV4],"
        queryString = queryString & " [TECLADOFAV5], "
        queryString = queryString & " [TKFDETLIN1], "
        queryString = queryString & " [TKFDETLIN2], "
        queryString = queryString & " [TKFDETLIN3], "
        queryString = queryString & " [TKXZDETLIN1], "
        queryString = queryString & " [TKXZDETLIN2], "
        queryString = queryString & " [TKXZDETLIN3], "
        queryString = queryString & " [ORDENPRODUCTOS], "
        queryString = queryString & " [PIDECAJAINICIO], "
        queryString = queryString & " [TKFACDIRWIN], "
        queryString = queryString & " [BORLINCUENTA], "
        queryString = queryString & " [CREACLICREDITO], "
        queryString = queryString & " [FADEOUTSALIR] "
        '
        ' Nota [FECHAZ], [FECHADIASESION] no se tocan
        '  es preferible dejarlos con valor NULL.
        '
        queryString = queryString & ") Values ("
        '
        queryString = queryString & wCaja & ","
        queryString = queryString & "'NUEVA CAJA " & wCaja.ToString & "'," ' Nombre Caja ...
        queryString = queryString & WcolFondoTCONA401.ToArgb & ","
        queryString = queryString & WcolFondoTCONA402.ToArgb & ","
        queryString = queryString & WcolFF.ToArgb & ","
        queryString = queryString & WcolLF.ToArgb & ","
        queryString = queryString & WcolFA.ToArgb & ","
        queryString = queryString & WcolLA.ToArgb & ","
        '
        ' [EMPRESA], [IGIC], [FACTURA], [ALMACEN], 
        ' [FORMINICIAL], [TKFACLOGO]
        '
        queryString = queryString & 1 & ","
        queryString = queryString & 0 & ","
        queryString = queryString & 1 & ","
        queryString = queryString & 1 & ","
        queryString = queryString & 0 & ","
        queryString = queryString & 0 & ","
        '
        ' LINEAS TICKET FACTURA CABECERA / PIE 
        '
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        '
        ' % IGIC TK FACTURA
        '
        queryString = queryString & 0 & ", "
        '
        ' [PIDEVENDEDOR], [SEPARARACIONES], [VARIOSFAM1],
        ' [VARIOSFAM2], [VARIOSFAM3], [NUMX], [NUMZ],
        ' [REFRESCABOTONES], [TIPOPVPARTI=1], [ZETAMESASOCU]
        ' Nota [FECHAZ], [FECHADIASESION] no se tocan
        '  es preferible dejarlos con valor NULL.
        '
        queryString = queryString & 0 & ", "
        queryString = queryString & 0 & ", "
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & "' ',"
        queryString = queryString & 0 & ", "
        queryString = queryString & 0 & ", "
        queryString = queryString & 0 & ", "
        queryString = queryString & 1 & ", "
        queryString = queryString & 0 & ", "
        queryString = queryString & 0 & ", "
        queryString = queryString & 1 & ", "    ' Splash Screen 1=Si
        queryString = queryString & 2500 & ", " ' Retardo ms
        queryString = queryString & 0 & ", "    ' Pide PAX?
        '
        queryString = queryString & 0 & ", "    ' Cargar Favoritos
        queryString = queryString & "'BEBIDAS'" & "," ' Boton Favorito
        '
        ' Nombre Tarifas
        '
        queryString = queryString & "'PVP1',"
        queryString = queryString & "'PVP2',"
        queryString = queryString & "'PVP5',"
        queryString = queryString & "'PVP6',"
        queryString = queryString & "'PVP7',"
        queryString = queryString & "'PVP8',"
        queryString = queryString & "'PVP9',"
        '
        queryString = queryString & 1 & ", "    ' COBVIEW Previsualizar S/N
        queryString = queryString & "' '," ' Modelo Impresora de Trabajo
        queryString = queryString & 1 & ", "    ' Imprime TK FACTURA S/N, 1 = S
        queryString = queryString & 0 & ", "    ' Importe Minimo Impre TK FAC = 0
        queryString = queryString & 1 & ", "    ' Salto Líneas TK AREAS = 1 Lin. x Defecto
        queryString = queryString & 0 & ", "    ' Abrir Cajon al Imprimir TK FACTURA S/N
        queryString = queryString & "' ', "  ' Puerto para TK FACTURA "LPTn:", "COMn"
        queryString = queryString & 0 & ", "    ' TK FACTURA Detalle Combinados S/N
        queryString = queryString & 0 & ",  "    ' Comprobar Impresora al Iniciar App S/N
        queryString = queryString & "' ', "     ' Frases Favoritas para teclado flotante
        queryString = queryString & "' ', "
        queryString = queryString & "' ', "
        queryString = queryString & "' ', "
        queryString = queryString & "' ', "
        '
        ' Detalles Por Defecto
        ' Tickets Fac
        queryString = queryString & "'--------- ------------------- -----------', "
        queryString = queryString & "'   UNI.        ARTICULO         IMPORTE   ', "
        queryString = queryString & "'--------- ------------------- -----------', "
        ' Tickets X Y Z
        queryString = queryString & "'------------------- ----------- ---------', "
        queryString = queryString & "'   F A M I L I A       UNID.    IMPORTE  ', "
        queryString = queryString & "'------------------- ----------- ---------', "
        '
        queryString = queryString & 0 & ", "  ' Orden Productos (0 Cod. Art. / 1 Nombre Fam.)
        queryString = queryString & 0 & ", "  ' Pide Caja Inicio S/N
        queryString = queryString & 0 & ", "  ' Impresion Fac. 0 Wind. / 1 Directa
        queryString = queryString & 1 & ", "  ' Pide Confirmacion Borrar Lineas, 0 No / 1 Si
        queryString = queryString & 0 & ", "  ' Crear Clientes Crédito, 0 No / 1 Si
        queryString = queryString & 0 & "  "  ' Efecto FADEOUT al salir, 0 No / 1 Si
        '
        queryString = queryString & ")"
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Regsitro [TCONA4]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub ColoreaGRID1(wFormOrigen As String)
        '
        ' Colorear GRID1, Grid Principal de Entrada de Productos.
        ' Diferenciar Nuevas Líneas de las Exsistentes.
        ' wFormOrigen, Nombre del Form que llama al procedimiento.
        '    ( Futuros usos )
        '
        MyFrm2.GRID1.Visible = False
        If MyFrm2.GRID1.Rows.Count > 0 Then
            For Each row As DataGridViewRow In MyFrm2.GRID1.Rows
                If row.Cells(5).Value.ToString.Trim = "N" Then
                    For ColNo As Integer = 0 To 4
                        If Not row.Cells(ColNo).Value Is DBNull.Value Then
                            row.Cells(ColNo).Style.BackColor = wcolLineasNuevas
                            row.DefaultCellStyle.Font =
                                New Font(Control.DefaultFont, FontStyle.Italic)
                        End If
                    Next
                End If
            Next
        End If
        MyFrm2.GRID1.Visible = True
    End Sub

    Public Sub GRID1_a_GRIDZOOM(wZoomOPC As Integer)
        '
        ' wZoomOPC :: 
        '   0 - Datos del GRID1 a GRID VISTA AMPLIADA (zoom).
        '   3 - Datos del GRID1 a GRID VISTA AMPLIADA +Combinados (zoom).
        '   1 - Datos del GRID1 a GRID Cambio Mesa.
        '   2 - Datos del GRID1 a GRID Separa Cuenta.
        '----------------------------------------------------------------------------
        '   0 Cod. Art        (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N        (No visible)
        '   6 Cod. Combinados (No Visible)
        '   7 Raciones        (No Visible)
        '   8 Orden Mesa      (No Visible)
        '
        MyFrm2.GRID1.Visible = False
        MyFrm3.GRIDZOOM.Visible = False
        MyFrm12.GRID1CMesa.Visible = False
        MyFrm13.GRID1SepaMesa.Visible = False
        '
        MyFrm3.GRIDZOOM.Rows.Clear()
        MyFrm12.GRID1CMesa.Rows.Clear()
        MyFrm13.GRID1SepaMesa.Rows.Clear()
        '
        ' Desactivar ordenar en cabeceras.
        ' (Vista Ampliada)
        '
        For i = 0 To MyFrm3.GRIDZOOM.Columns.Count - 1
            MyFrm3.GRIDZOOM.Columns.Item(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next i
        '
        wDTotal = 0
        If MyFrm2.GRID1.Rows.Count > 0 Then
            For Each row As DataGridViewRow In MyFrm2.GRID1.Rows
                '
                LeeMar(row.Cells(0).Value.ToString.Trim)
                '
                wDUnid1 = CDec(row.Cells(1).Value.ToString.Replace(".", ",").Trim)
                wDUnid2 = CDec(row.Cells(3).Value.ToString.Replace(".", ",").Trim)
                wDImporte = CDec(row.Cells(4).Value.ToString.Replace(".", ",").Trim)
                'wDPrecio = CDec(wrLeeMAR.Mar_PREPVP1.Replace(".", ",").Trim)
                '
                ' PREPVPTPV = Precio PVP1 a PVP9 en funcion de Ref. Generales. 
                '
                'wDPrecio = CDec(wrLeeMAR.Mar_PREPVPTPV.Replace(".", ",").Trim)
                '
                ' PRECIO Calculado: (IMP. / UNID. Existentes.)
                '
                wDPrecio = wDImporte / wDUnid1
                wDTotal += wDImporte
                '
                Select Case wZoomOPC
                    Case 0, 3
                        MyFrm3.GRIDZOOM.Rows.Add(
                    row.Cells(0).Value.ToString.Trim,
                    wDUnid1.ToString(fmtUnid).Replace(",", "."),
                    row.Cells(2).Value.ToString.Trim,
                    wDUnid2.ToString(fmtUnid).Replace(",", "."),
                    wDPrecio.ToString(fmtPrecio).Replace(",", "."),
                    wDImporte.ToString(fmtImporte).Replace(",", "."),
                    row.Cells(5).Value.ToString.Trim,
                    row.Cells(6).Value.ToString.Trim & "",
                    row.Cells(7).Value.ToString.Trim & "")
                        '
                        ' wZoomOPC = 3 :: *** Combinados *** Si Hay
                        '
                        If row.Cells(6).Value.ToString.Trim.Length > 0 And
                            wZoomOPC = 3 Then
                            '
                            Dim words As String() = row.Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                            For i As Integer = 0 To words.Length - 1
                                If LeeMar(words(i)) = False Then
                                    wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                Else
                                    MyFrm3.GRIDZOOM.Rows.Add(" ", " ",
                                                             "[+] " & wrLeeMAR.Mar_DESCRIPCION,
                                                             " ", " ", " ", " ",
                                                             " ", " ")

                                End If
                            Next
                        End If
                        '
                    Case 1
                        MyFrm12.GRID1CMesa.Rows.Add(
                    row.Cells(0).Value.ToString.Trim,
                    row.Cells(1).Value.ToString.Trim,
                    row.Cells(2).Value.ToString.Trim,
                    row.Cells(3).Value.ToString.Trim,
                    row.Cells(4).Value.ToString.Trim,
                    row.Cells(5).Value.ToString.Trim,
                    row.Cells(6).Value.ToString.Trim & "",
                    row.Cells(7).Value.ToString.Trim & "")
                    Case 2
                        MyFrm13.GRID1SepaMesa.Rows.Add(
                    row.Cells(0).Value.ToString.Trim,
                    row.Cells(1).Value.ToString.Trim,
                    row.Cells(2).Value.ToString.Trim,
                    row.Cells(3).Value.ToString.Trim,
                    row.Cells(4).Value.ToString.Trim,
                    row.Cells(5).Value.ToString.Trim,
                    row.Cells(6).Value.ToString.Trim & "",
                    row.Cells(7).Value.ToString.Trim & "",
                    row.Cells(8).Value.ToString.Trim & "")
                End Select
            Next
        End If
        '
        If wZoomOPC = 0 Or wZoomOPC = 3 Then
            '
            ' TOTAL (Vista Ampliada)
            '
            MyFrm3.GRIDZOOM.Rows.Add(
                    " ", ' Cod. Art.
                    " ", ' Unid. Ex.
                    " ", ' Descripcion
                    " ", ' Unid. Nuevas
                    " ", ' Precio
                    " ", ' Importe
                    " ", ' Tipo Ex/Nu
                    " ", ' Combinados
                    " ") ' Raciones
            MyFrm3.GRIDZOOM.Rows.Add(
                    " ", ' Cod. Art.
                    " ", ' Unid. Ex.
                    " TOTAL MESA ( " & MyFrm2.TextBoxNumMesa.Text.Trim & " ).........: ", ' Descripcion
                    " ", ' Unid. Nuevas
                    " ", ' Precio
                    wDTotal.ToString(fmtImporte).Replace(",", "."), ' Importe
                    " ", ' Tipo Ex/Nu
                    " ", ' Combinados
                    " ") ' Raciones
            '
            ' TOTAL En Italica / Negrita
            ' Color Letra (ForeColor) en Azul
            '
            MyFrm3.GRIDZOOM.Rows(MyFrm3.GRIDZOOM.Rows.Count - 1).DefaultCellStyle.Font =
                                New Font(Control.DefaultFont, FontStyle.Italic Or FontStyle.Bold)
            MyFrm3.GRIDZOOM.Rows(MyFrm3.GRIDZOOM.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Blue
        End If
        '
        MyFrm2.GRID1.Visible = True
        MyFrm3.GRIDZOOM.Visible = True
        MyFrm12.GRID1CMesa.Visible = True
        MyFrm13.GRID1SepaMesa.Visible = True
        '
    End Sub

    Public Function LocalizarArtGRID1(LocART As String,
                                      UbiUnid As String,
                                      UbiPrecio As String,
                                      CodigosCombi As String,
                                      PreRaciones As String) As Boolean
        '
        ' Localiza Productos Existentes en la Lista y le suma las unidades NUEVAS ("N").
        '
        ' GRID:
        '   0 Cod. Art        (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N        (No visible)
        '   6 Cod. Combinados (No Visible)
        '   7 Raciones        (No Visible)
        '   8 Nro. Plato      (No Visible)
        '
        LocalizarArtGRID1 = False
        '
        Dim ColUnidEX As Integer = 1
        Dim ColUnidN As Integer = 3
        Dim ColImporte As Integer = 4
        Dim ColNueva As Integer = 5
        '
        Dim wTmpUnidAnt As Decimal = 0 ' Unid. Nuevas en la lista
        Dim wTmpUnidNue As Decimal = CDec(UbiUnid.ToString.Replace(".", ",")) ' Unid. Nuevas que entran
        Dim wTmpUnidEx As Decimal = 0 ' Unid. Existentes, ya grabadas en MESA
        Dim wTmpUnid As Decimal = 0 ' Nuevas + Exist...
        Dim wTmpUnidNN As Decimal = 0 ' Solo Nuevas...
        Dim wTmpImporte As Decimal = 0
        '
        MyFrm2.GRID1.Visible = False
        If MyFrm2.GRID1.Rows.Count > 0 Then
            For Each row As DataGridViewRow In MyFrm2.GRID1.Rows
                If row.Cells(0).Value.ToString.Trim = LocART And
                    row.Cells(6).Value.ToString = CodigosCombi And
                    row.Cells(7).Value.ToString = PreRaciones Then
                    '
                    LocalizarArtGRID1 = True
                    If Not row.Cells(ColUnidEX).Value Is DBNull.Value Then
                        '
                        ' Adicion Unidades Nuevas, Calculo Importe.
                        '
                        wTmpUnidEx = CDec(row.Cells(ColUnidEX).Value.ToString.Replace(".", ","))
                        wTmpUnidAnt = CDec(row.Cells(ColUnidN).Value.ToString.Replace(".", ","))
                        wTmpUnid = wTmpUnidNue + wTmpUnidAnt : wTmpUnidNN = wTmpUnidEx + wTmpUnidNue
                        row.Cells(ColUnidEX).Value = wTmpUnidNN.ToString(fmtUnid).Replace(",", ".")
                        row.Cells(ColUnidN).Value = wTmpUnid.ToString(fmtUnid).Replace(",", ".")
                        row.Cells(ColNueva).Value = "N"
                        '
                        ' Nro. Plato
                        '
                        row.Cells(8).Value = ContadorPlato.ToString
                        '
                        ' Style
                        '
                        row.Cells(ColUnidN).Style.BackColor = wcolLineasNuevas
                        row.DefaultCellStyle.Font =
                            New Font(Control.DefaultFont, FontStyle.Italic)
                    End If
                    '
                    ' Calculo el IMPORTE = Unid. Ex. + Unid Nue * PRECIO ART.
                    '
                    With wrRACIONES
                        .RACIONES_IMPORTE = row.Cells(ColImporte).Value.ToString.Trim
                        Select Case .RACIONES_indicador
                            Case 0
                                wImporteN = (wTmpUnidNue *
                                CDec(UbiPrecio.ToString.Trim.Replace(".", ",")) +
                                CDec(.RACIONES_IMPORTE.Replace(".", ",")))
                            Case 1
                                wImporteN = (wTmpUnidNue *
                                CDec(.RACIONES_PVPRacion.Replace(".", ","))) +
                                CDec(.RACIONES_IMPORTE.Replace(".", ","))
                            Case 2
                                wImporteN = (wTmpUnidNue *
                                CDec(.RACIONES_PVPMediaRacion.Replace(".", ","))) +
                                CDec(.RACIONES_IMPORTE.Replace(".", ","))
                        End Select
                    End With
                    '
                    row.Cells(ColImporte).Value = wImporteN.ToString(fmtUnid).Replace(",", ".")
                End If
            Next
        End If
        MyFrm2.GRID1.Visible = True
        With wrRACIONES
            .RACIONES_indicador = 0
            .RACIONES_PVPRacion = ""
            .RACIONES_PVPMediaRacion = ""
            .RACIONES_IMPORTE = ""
        End With
        '
    End Function

    Public Function CalculaTotalGRID1() As String
        '
        '   Cada vez que se necesite RECALCULAR el TOTAL IMPORTE del GRID
        '     recurrimos a este procedimiento.
        '
        CalculaTotalGRID1 = "0.00"
        '
        MyFrm2.GRID1.Visible = False
        If MyFrm2.GRID1.Rows.Count > 0 Then
            '
            Dim MiImpo As Decimal = 0 : Dim MiTotImpo As Decimal = 0
            '
            For Each row As DataGridViewRow In MyFrm2.GRID1.Rows
                If Not row.Cells(4).Value Is DBNull.Value Then
                    '
                    ' Adicion Unidades Nuevas, Calculo Importe.
                    '
                    MiImpo = CDec(row.Cells(4).Value.ToString.Replace(".", ","))
                    MiTotImpo += MiImpo
                End If
            Next
            '
            CalculaTotalGRID1 = MiTotImpo.ToString.Replace(",", ".")
            '
        End If
        MyFrm2.GRID1.Visible = True
        '
    End Function

    Public Sub ConsGeneralMESAH(wConsCadena As String)
        '
        ' Consulta General Filtrada Para registros de MESAS
        ' [MESA] / [MESAH]
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Recogemos la cadena con la SENTENCIA SQL para la consulta
        ' Si llega vacía, nos vamos!
        '
        If wConsCadena.Trim.Length = 0 Then
            Exit Sub
        End If
        Dim queryString = wConsCadena.Trim : Dim queryString1 As String = ""
        Dim NumeroMES As String = ""
        '
        With MyFrm11
            .GRID1.Visible = False
            .GRID2.Visible = False
            .GRID3.Visible = False
            .GRID1.Rows.Clear()
            .GRID2.Rows.Clear()
            .GRID3.Rows.Clear()
        End With
        '
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        ' Borrado TOTAL de las filas de la Tabla [ZZTablaXZTemp]
        ' Esta Tabla es "Pseudo-Temporal", y se utiliza con el fin 
        ' de generar el Resumen. Caja Actual.
        '
        queryString1 = " DELETE FROM [ZZTablaXZTemp] "
        queryString1 &= "WHERE "
        queryString1 &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja
        '
        cmd.CommandText = queryString1
        cmd.Connection = conexion
        cmd.ExecuteNonQuery()
        '
        Dim dt As DataSet = New DataSet
        Dim wUnid As Double = 0 : Dim wImpo As Double = 0
        Dim TmpUnid As Double = 0 : Dim TmpImporte As Double = 0
        '
        Try
            '
            ' Ejecutamos CONSULTA en funcion de la cadena SQL recibida
            '  sobre HISTORICO MESAS [MESAH]
            '
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESAH")
            '
            If dt.Tables("MESAH").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MESAH").Rows
                    '
                    ' Lectura del PRODUCTO
                    '
                    If LeeMar(pRow("ARTI").ToString().Trim) = False Then
                        wrLeeMAR.Mar_DESCRIPCION = "ART. NO LEIDO"
                        wrLeeMAR.Mar_FAMILIA = " "
                    End If
                    '
                    ' GRID1:
                    '   0 Fecha
                    '   1 HH:MM (12:59)
                    '   2 Mesa
                    '   3 Factura
                    '   4 Cod. Art
                    '   5 Nom. Art
                    '   6 Unid
                    '   7 PVP
                    '   8 Importe
                    '   9 Sala
                    '
                    wUnid = CDbl(pRow("UNID").ToString())
                    wImpo = CDbl(pRow("IMPORTE").ToString())
                    '
                    MyFrm11.GRID1.Rows.Add(Format(pRow("FECHA"), "dd/MM/yyyy"),
                                           pRow("HORA").ToString(),
                                           pRow("MESA").ToString(),
                                           pRow("FACTURA").ToString(),
                                           pRow("ARTI").ToString(),
                                           wrLeeMAR.Mar_DESCRIPCION,
                                           wUnid.ToString(fmtUnid),
                                           CDbl(wImpo / wUnid).ToString(fmtUnid).Replace(",", "."),
                                           wImpo.ToString(fmtImporte),
                                           pRow("SALA").ToString())
                    '
                    '
                    ' Acumulado Unidades / Importe
                    '
                    TmpUnid = CDec(pRow("UNID").ToString.Trim.Replace(".", ","))
                    TmpImporte = CDec(pRow("IMPORTE").ToString.Trim.Replace(".", ","))
                    '
                    ' Obtener Nro del MES
                    '
                    NumeroMES = Mid(Format(pRow("FECHA"), "dd/MM/yyyy"), 4, 2)
                    '
                    ' Acciones según Tipo de Consulta
                    '
                    Select Case wTipoConsu
                        Case 0 ' x Salas
                            wrLeeSALA.Sala_NOMBRE = "**SALA NO CREADA**"
                            If LeeSALA(pRow("SALA").ToString().Trim) = True Then
                                If LeeTablaXZTemp(pRow("SALA").ToString().Trim) = True Then
                                    '
                                    ' Acumulamos Unid. // Importe
                                    '
                                    TmpUnid += CDec(wrACUMXZ.ACUMXZ_Unid.ToString.Trim.Replace(".", ","))
                                    TmpImporte += CDec(wrACUMXZ.ACUMXZ_Importe.ToString.Trim.Replace(".", ","))
                                    '
                                    queryStringC = "UPDATE [ZZTablaXZTemp] SET "
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid]='" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo]='" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    '
                                    queryStringC &= MiraMesConsu(0, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[Sala]='" & pRow("SALA").ToString().Trim & "' "
                                    queryStringC &= "WHERE "
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia]='" & pRow("SALA").ToString().Trim & "'"
                                Else
                                    queryStringC = "INSERT INTO [ZZTablaXZTemp] ("
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja],"
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[NombreFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid],"
                                    '
                                    queryStringC &= MiraMesConsu(1, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo],"
                                    queryStringC &= "[ZZTablaXZTemp].[Sala] "
                                    queryStringC &= ") Values ("
                                    queryStringC &= "" & wCaja & ", "
                                    queryStringC &= "'" & pRow("SALA").ToString().Trim & "', "
                                    queryStringC &= "'" & wrLeeSALA.Sala_NOMBRE & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & pRow("SALA").ToString().Trim & "'"
                                    queryStringC &= ")"
                                End If
                                '
                                ' Ejecutamos Comando SQL...
                                '
                                cmd.CommandText = queryStringC
                                cmd.Connection = conexion
                                cmd.ExecuteNonQuery()
                                '
                            End If
                        Case 1 ' x Vendedor
                            wrLeeCODNOM.NOMBRE = "**VENDEDOR NO CREADO**"
                            If LeeVendedor(CInt(pRow("VENDEDOR").ToString().Trim)) = True Then
                                If LeeTablaXZTemp(pRow("VENDEDOR").ToString().Trim) = True Then
                                    '
                                    ' Acumulamos Unid. // Importe
                                    '
                                    TmpUnid += CDec(wrACUMXZ.ACUMXZ_Unid.ToString.Trim.Replace(".", ","))
                                    TmpImporte += CDec(wrACUMXZ.ACUMXZ_Importe.ToString.Trim.Replace(".", ","))
                                    '
                                    queryStringC = "UPDATE [ZZTablaXZTemp] SET "
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid]='" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo]='" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    '
                                    queryStringC &= MiraMesConsu(0, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[Sala]='" & pRow("SALA").ToString().Trim & "' "
                                    queryStringC &= "WHERE "
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia]='" & pRow("VENDEDOR").ToString().Trim & "'"
                                Else
                                    queryStringC = "INSERT INTO [ZZTablaXZTemp] ("
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja],"
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[NombreFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid],"
                                    '
                                    queryStringC &= MiraMesConsu(1, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo],"
                                    queryStringC &= "[ZZTablaXZTemp].[Sala] "
                                    queryStringC &= ") Values ("
                                    queryStringC &= "" & wCaja & ", "
                                    queryStringC &= "'" & pRow("VENDEDOR").ToString().Trim & "', "
                                    queryStringC &= "'" & wrLeeCODNOM.NOMBRE & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & pRow("SALA").ToString().Trim & "'"
                                    queryStringC &= ")"
                                End If
                                '
                                ' Ejecutamos Comando SQL...
                                '
                                cmd.CommandText = queryStringC
                                cmd.Connection = conexion
                                cmd.ExecuteNonQuery()
                                '
                            End If
                        Case 2 ' x Nro. Z
                            If LeeTablaXZTemp(pRow("NUMZETA").ToString().Trim) = True Then
                                '
                                ' Acumulamos Unid. // Importe
                                '
                                TmpUnid += CDec(wrACUMXZ.ACUMXZ_Unid.ToString.Trim.Replace(".", ","))
                                TmpImporte += CDec(wrACUMXZ.ACUMXZ_Importe.ToString.Trim.Replace(".", ","))
                                '
                                queryStringC = "UPDATE [ZZTablaXZTemp] SET "
                                queryStringC &= "[ZZTablaXZTemp].[TotUnid]='" & TmpUnid.ToString.Replace(",", ".") & "', "
                                queryStringC &= "[ZZTablaXZTemp].[TotImpo]='" & TmpImporte.ToString.Replace(",", ".") & "', "
                                '
                                queryStringC &= MiraMesConsu(0, NumeroMES, TmpUnid, TmpImporte)
                                '
                                queryStringC &= "[ZZTablaXZTemp].[Sala]='" & pRow("SALA").ToString().Trim & "' "
                                queryStringC &= "WHERE "
                                queryStringC &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
                                queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia]='" & pRow("NUMZETA").ToString().Trim & "'"
                            Else
                                queryStringC = "INSERT INTO [ZZTablaXZTemp] ("
                                queryStringC &= "[ZZTablaXZTemp].[NumCaja],"
                                queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia],"
                                queryStringC &= "[ZZTablaXZTemp].[NombreFamilia],"
                                queryStringC &= "[ZZTablaXZTemp].[TotUnid],"
                                '
                                queryStringC &= MiraMesConsu(1, NumeroMES, TmpUnid, TmpImporte)
                                '
                                queryStringC &= "[ZZTablaXZTemp].[TotImpo],"
                                queryStringC &= "[ZZTablaXZTemp].[Sala] "
                                queryStringC &= ") Values ("
                                queryStringC &= "" & wCaja & ", "
                                queryStringC &= "'" & pRow("NUMZETA").ToString().Trim & "', "
                                queryStringC &= "' *** Resumen Nro. Zeta *** ', "
                                queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                queryStringC &= "'" & pRow("SALA").ToString().Trim & "'"
                                queryStringC &= ")"
                            End If
                            '
                            ' Ejecutamos Comando SQL...
                            '
                            cmd.CommandText = queryStringC
                            cmd.Connection = conexion
                            cmd.ExecuteNonQuery()
                                '
                        Case 3 ' x Artículos
                            If LeeMar(pRow("ARTI").ToString().Trim) = True Then
                                If LeeTablaXZTemp(pRow("ARTI").ToString().Trim) = True Then
                                    '
                                    ' Acumulamos Unid. // Importe
                                    '
                                    TmpUnid += CDec(wrACUMXZ.ACUMXZ_Unid.ToString.Trim.Replace(".", ","))
                                    TmpImporte += CDec(wrACUMXZ.ACUMXZ_Importe.ToString.Trim.Replace(".", ","))
                                    '
                                    queryStringC = "UPDATE [ZZTablaXZTemp] SET "
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid]='" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo]='" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    '
                                    queryStringC &= MiraMesConsu(0, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[Sala]='" & pRow("SALA").ToString().Trim & "' "
                                    queryStringC &= "WHERE "
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia]='" & pRow("ARTI").ToString().Trim & "'"
                                Else
                                    queryStringC = "INSERT INTO [ZZTablaXZTemp] ("
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja],"
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[NombreFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid],"
                                    '
                                    queryStringC &= MiraMesConsu(1, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo],"
                                    queryStringC &= "[ZZTablaXZTemp].[Sala] "
                                    queryStringC &= ") Values ("
                                    queryStringC &= "" & wCaja & ", "
                                    queryStringC &= "'" & pRow("ARTI").ToString().Trim & "', "
                                    queryStringC &= "'" & wrLeeMAR.Mar_DESCRIPCION & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & pRow("SALA").ToString().Trim & "'"
                                    queryStringC &= ")"
                                End If
                                '
                                ' Ejecutamos Comando SQL...
                                '
                                cmd.CommandText = queryStringC
                                cmd.Connection = conexion
                                cmd.ExecuteNonQuery()
                                '
                            End If
                        Case 4 ' x Familias
                            If LeeMar(pRow("ARTI").ToString().Trim) = True Then
                                '
                                ' Nombre Familia
                                '
                                If LeeFam(wrLeeMAR.Mar_FAMILIA.Trim) = False Then
                                    wrLeeCODNOM.NOMBRE = "**FAMILIA NO LEIDA**"
                                End If
                                '
                                If LeeTablaXZTemp(wrLeeMAR.Mar_FAMILIA.Trim) = True Then
                                    '
                                    ' Acumulamos Unid. // Importe
                                    '
                                    TmpUnid += CDec(wrACUMXZ.ACUMXZ_Unid.ToString.Trim.Replace(".", ","))
                                    TmpImporte += CDec(wrACUMXZ.ACUMXZ_Importe.ToString.Trim.Replace(".", ","))
                                    '
                                    queryStringC = "UPDATE [ZZTablaXZTemp] SET "
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid]='" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo]='" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    '
                                    queryStringC &= MiraMesConsu(0, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[Sala]='" & pRow("SALA").ToString().Trim & "' "
                                    queryStringC &= "WHERE "
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia]='" & wrLeeMAR.Mar_FAMILIA.Trim & "'"
                                Else
                                    queryStringC = "INSERT INTO [ZZTablaXZTemp] ("
                                    queryStringC &= "[ZZTablaXZTemp].[NumCaja],"
                                    queryStringC &= "[ZZTablaXZTemp].[CodigoFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[NombreFamilia],"
                                    queryStringC &= "[ZZTablaXZTemp].[TotUnid],"
                                    '
                                    queryStringC &= MiraMesConsu(1, NumeroMES, TmpUnid, TmpImporte)
                                    '
                                    queryStringC &= "[ZZTablaXZTemp].[TotImpo],"
                                    queryStringC &= "[ZZTablaXZTemp].[Sala] "
                                    queryStringC &= ") Values ("
                                    queryStringC &= "" & wCaja & ", "
                                    queryStringC &= "'" & wrLeeMAR.Mar_FAMILIA.Trim & "', "
                                    queryStringC &= "'" & wrLeeCODNOM.NOMBRE.Trim & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                                    queryStringC &= "'" & pRow("SALA").ToString().Trim & "'"
                                    queryStringC &= ")"
                                End If
                                '
                                ' Ejecutamos Comando SQL...
                                '
                                cmd.CommandText = queryStringC
                                cmd.Connection = conexion
                                cmd.ExecuteNonQuery()
                                '
                            End If
                    End Select
                Next
                ' Mostrar el Resumen:
                '   GRID2 :: Resumen
                '   GRID3 :: Resumen Meses
                '
                Resumen_a_GRID2_3()
                '
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Consulta [MESAH]")
        End Try
        With MyFrm11
            .GRID1.Visible = True
            .GRID2.Visible = True
            .GRID3.Visible = True
        End With
        conexion.Close() : conexion.Dispose()
        dt.Dispose()
        '
    End Sub

    Private Function MiraMesConsu(wTipoConcat As Integer,
                                  wMesConcat As String,
                                  wTmpunid As Double,
                                  wTmpImpo As Double) As String
        '
        ' En función del MES, acumulamos UNID/IMPORTE sobre la variable 
        '  de dicho MES.
        '
        MiraMesConsu = ""
        Select Case wTipoConcat
            Case 0 ' parte de UPDATE
                Select Case wMesConcat
                    Case "01"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidEnero]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoEnero]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "02"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidFebrero]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoFebrero]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "03"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidMarzo]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoMarzo]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "04"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidAbril]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoAbril]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "05"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidMayo]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoMayo]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "06"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidJunio]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoJunio]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "07"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidJulio]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoJulio]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "08"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidAgosto]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoAgosto]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "09"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidSeptiembre]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoSeptiembre]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "10"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidOctubre]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoOctubre]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "11"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidNoviembre]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoNoviembre]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                    Case "12"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidDiciembre]='" & wTmpunid.ToString.Replace(",", ".") & "', "
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoDiciembre]='" & wTmpImpo.ToString.Replace(",", ".") & "', "
                End Select
            Case 1 ' parte de INSERT
                Select Case wMesConcat
                    Case "01"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidEnero],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoEnero],"
                    Case "02"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidFebrero],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoFebrero],"
                    Case "03"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidMarzo],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoMarzo],"
                    Case "04"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidAbril],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoAbril],"
                    Case "05"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidMayo],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoMayo],"
                    Case "06"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidJunio],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoJunio],"
                    Case "07"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidJulio],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoJulio],"
                    Case "08"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidAgosto],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoAgosto],"
                    Case "09"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidSeptiembre],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoSeptiembre],"
                    Case "10"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidOctubre],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoOctubre],"
                    Case "11"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidNoviembre],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoNoviembre],"
                    Case "12"
                        MiraMesConsu = "[ZZTablaXZTemp].[TotUnidDiciembre],"
                        MiraMesConsu &= "[ZZTablaXZTemp].[TotImpoDiciembre],"
                End Select
        End Select
        '
    End Function

    Public Sub Resumen_a_GRID2_3()
        '
        ' Mostrar el Resumen en pantalla CONSULTA 
        '   GRID2 :: Resumen
        '   GRID3 :: Resumen Meses
        '
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        queryString = "SELECT * FROM [ZZTablaXZTemp] "
        queryString &= "WHERE [ZZTablaXZTemp].[Numcaja]=" & wCaja & " "
        queryString &= "ORDER BY "
        queryString &= "[ZZTablaXZTemp].[NombreFamilia] ASC"
        '
        Dim TmpUnid As Decimal = 0 : Dim TmpImporte As Decimal = 0
        Dim TmpTOTAL As Decimal = 0 : Dim TmpTOTAL1 As Decimal = 0
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "ZZTablaXZTemp")
            '
            Dim Descripcion As String = ""
            '
            If dt.Tables("ZZTablaXZTemp").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("ZZTablaXZTemp").Rows
                    '
                    TmpUnid = CDec(pRow("TotUnid").ToString().Trim.Replace(".", ","))
                    TmpImporte = CDec(pRow("TotImpo").ToString().Trim.Replace(".", ","))
                    '
                    TmpTOTAL =
                    CDec(pRow("TotUnidEnero").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidFebrero").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidMarzo").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidAbril").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidMayo").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidJunio").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidJulio").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidAgosto").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidSeptiembre").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidOctubre").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidNoviembre").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotUnidDiciembre").ToString().Trim.Replace(".", ","))
                    '
                    TmpTOTAL1 =
                    CDec(pRow("TotImpoEnero").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoFebrero").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoMarzo").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoAbril").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoMayo").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoJunio").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoJulio").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoAgosto").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoSeptiembre").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoOctubre").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoNoviembre").ToString().Trim.Replace(".", ",")) +
                    CDec(pRow("TotImpoDiciembre").ToString().Trim.Replace(".", ","))
                    '
                    ' Línea Para Resumen (GRID2)
                    '
                    MyFrm11.GRID2.Rows.Add(pRow("CodigoFamilia").ToString(),
                                           pRow("NombreFamilia").ToString(),
                                           TmpUnid.ToString(fmtUnid),
                                           CDbl(TmpImporte / TmpUnid).ToString(fmtUnid).Replace(",", "."),
                                           TmpImporte.ToString(fmtImporte),
                                           pRow("SALA").ToString())
                    Select Case wTipoResuMES
                        Case 0
                            '
                            ' Línea UNIDADES x MESES  (GRID3)
                            '
                            MyFrm11.GRID3.Rows.Add(pRow("CodigoFamilia").ToString(),
                                           pRow("NombreFamilia").ToString(),
                            CDec(pRow("TotUnidEnero").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidFebrero").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidMarzo").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidAbril").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidMayo").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidJunio").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidJulio").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidAgosto").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidSeptiembre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidOctubre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidNoviembre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidDiciembre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            TmpTOTAL.ToString(fmtImporte).Trim.Replace(".", ","),
                            "U"
                            )
                        Case 1
                            '
                            ' Línea IMPORTES x MESES  (GRID3)
                            '
                            MyFrm11.GRID3.Rows.Add(pRow("CodigoFamilia").ToString(),
                                           pRow("NombreFamilia").ToString(),
                            CDec(pRow("TotImpoEnero").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoFebrero").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoMarzo").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoAbril").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoMayo").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoJunio").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoJulio").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoAgosto").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoSeptiembre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoOctubre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoNoviembre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoDiciembre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            TmpTOTAL1.ToString(fmtImporte).Trim.Replace(".", ","),
                            "I"
                            )
                        Case 2
                            '
                            ' Línea UNIDADES x MESES  (GRID3)
                            '
                            MyFrm11.GRID3.Rows.Add(pRow("CodigoFamilia").ToString(),
                                           pRow("NombreFamilia").ToString(),
                            CDec(pRow("TotUnidEnero").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidFebrero").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidMarzo").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidAbril").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidMayo").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidJunio").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidJulio").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidAgosto").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidSeptiembre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidOctubre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidNoviembre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            CDec(pRow("TotUnidDiciembre").ToString().Trim.Replace(".", ",")).ToString(fmtUnid),
                            TmpTOTAL.ToString(fmtImporte).Trim.Replace(".", ","),
                            "U"
                            )
                            '
                            ' Línea IMPORTES x MESES  (GRID3)
                            '
                            MyFrm11.GRID3.Rows.Add(" ",
                                           " Tot. Importes -> ",
                            CDec(pRow("TotImpoEnero").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoFebrero").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoMarzo").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoAbril").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoMayo").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoJunio").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoJulio").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoAgosto").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoSeptiembre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoOctubre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoNoviembre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            CDec(pRow("TotImpoDiciembre").ToString().Trim.Replace(".", ",")).ToString(fmtImporte),
                            TmpTOTAL1.ToString(fmtImporte).Trim.Replace(".", ","),
                            "I"
                            )
                    End Select
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR:  " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Resumen [ZZTablaXZTemp]")
        End Try
        '
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub CargaListaMESAs(ListarMESA As String)
        '
        ' Lectura de registros ('LINEAS') de DATOS de MESAS
        ' TODAS las líneas de una MESA
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        'Dim ExisteFechaMESA As String = Date.Now.ToShortDateString
        If LeeMesa_SALA1(wCodSala, ListarMESA, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        Dim ExisteFechaMESA As String = FechaMESAC
        Dim ComparaFecha As String = ""
        '
        ' En este caso para la lectura por la KEY FACTURA diferencia un GRUPO de registros.
        ' ORDENAMOS por Cód. Articulo / Nombre Familia, determinado por
        '    referencias generales.
        '
        LeeTCONA4Cfg("General")
        If wrLeeTCONA4.Tcona4_ORDENPRODUCTOS = "False" Then
            '
            ' Para ORDERNAR Por Código del PRODUCTO
            '
            queryString = "SELECT * FROM [MESA] WHERE "
            queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & "[MESA].[FECHA]='" & ExisteFechaMESA & "' AND "
            queryString = queryString & "[MESA].[SALA]='" & wCodSala & "' AND "
            queryString = queryString & "[MESA].[MESA]='" & ListarMESA & "' AND "
            queryString = queryString & "[MESA].[FACTURA]=" & wFacturaN & " ORDER BY "
            queryString = queryString & "[MESA].[ARTI]"
        Else
            '
            ' Para ORDERNAR Por NOMBRE de Familia
            '
            queryString = "SELECT * FROM [MESA], [MAR], [FAM] WHERE "
            queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " AND "
            queryString = queryString & "[MESA].[FECHA]='" & ExisteFechaMESA & "' AND "
            queryString = queryString & "[MESA].[SALA]='" & wCodSala & "' AND "
            queryString = queryString & "[MESA].[MESA]='" & ListarMESA & "' AND "
            queryString = queryString & "[MESA].[FACTURA]=" & wFacturaN & " AND "
            queryString = queryString & "[MAR].[NARTICULO]=[MESA].[ARTI] AND "
            queryString = queryString & "[FAM].[CODIGO]=[MAR].[FAMILIA] "
            queryString = queryString & " ORDER BY [FAM].[NOMBRE]"
        End If
        '
        MyFrm2.GRID1.Visible = False
        MyFrm2.GRID1.Rows.Clear()
        '
        Dim dt As DataSet = New DataSet
        Dim wUnid As Double : Dim wImpo As Double
        Dim wTOTAL As Decimal = 0
        Dim CodPriCombi As String = ""
        Dim NomPriCombi As String = ""
        Dim TempNoMaRT As String = ""
        '
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESA")
            '
            If dt.Tables("MESA").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MESA").Rows
                    '
                    ' Si Hay Combinados, Leemos el primero de ellos
                    '
                    NomPriCombi = "" : TempNoMaRT = ""
                    If pRow("COMBINA").ToString().Trim.Length > 0 Then
                        CodPriCombi = pRow("COMBINA").ToString().Trim
                        Dim words As String() = CodPriCombi.Split(New Char() {"/"c})
                        CodPriCombi = words(0).Trim
                        If LeeMar(CodPriCombi) = False Then
                            wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                        End If
                        NomPriCombi = wrLeeMAR.Mar_DESCRIPCION.Trim
                    End If
                    '
                    ' Lectura del PRODUCTO
                    '    Orden Por Cod. Arti, lee MAR
                    '    Orden Por Familia, Mar Descripcion lo tenemos
                    '     evitamos esta lectura.
                    '
                    If wrLeeTCONA4.Tcona4_ORDENPRODUCTOS = "False" Then
                        If LeeMar(pRow("ARTI").ToString().Trim) = False Then
                            wrLeeMAR.Mar_DESCRIPCION = "ART. NO LEIDO"
                        End If
                    Else
                        wrLeeMAR.Mar_DESCRIPCION = pRow("DESCRIPCION").ToString().Trim
                    End If
                    '
                    If NomPriCombi.Length > 0 Then
                        TempNoMaRT = wrLeeMAR.Mar_DESCRIPCION & "  +[" & NomPriCombi & "]"
                    Else
                        TempNoMaRT = wrLeeMAR.Mar_DESCRIPCION
                    End If
                    '
                    ' GRID:
                    '   0 Cod. Art               (No Visible)
                    '   1 Unid. Existentes
                    '   2 Descripcion
                    '   3 Unid. Nuevas
                    '   4 Importe
                    '   5 Tipo E/N               (No visible)
                    '   6 Codigos COMBINADOS     (No visible)
                    '   7 MediaPrecio / Raciones (No visible)
                    '   8 Orden Plato            (No visible)
                    '
                    wImpo = CDbl(pRow("IMPORTE").ToString())
                    wUnid = CDbl(pRow("UNID").ToString())
                    MyFrm2.GRID1.Rows.Add(pRow("ARTI").ToString(),
                                                wUnid.ToString(fmtUnid).Replace(",", "."),
                                               TempNoMaRT,
                                               "0.00",
                                               wImpo.ToString(fmtUnid).Replace(",", "."),
                                               "E",
                                               pRow("COMBINA").ToString(),
                                               pRow("MEDIAPRECIO").ToString(),
                                               pRow("ORDENPLATO").ToString() & "")
                    '
                    wTOTAL += (CDec(pRow("IMPORTE").ToString().Replace(".", ",")))
                    '
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla LISTAR [MESA]")
        End Try
        '
        ' TOTAL
        '
        wTotalN = wTOTAL
        MyFrm2.LabelTotComanda.Text = wTOTAL.ToString(fmtUnid).Replace(",", ".")
        '
        MyFrm2.GRID1.Visible = True
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub


    Public Sub CargaModelosImpresoras()
        '
        ' Lectura de LOS MODELOS de Impresoras a un GRID.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [IMPRESORAS] "
        queryString = queryString & "ORDER BY "
        queryString = queryString & "[IMPRESORAS].[IMPRESORA]"
        '
        With MyFrm6
            .GRIDIMPREMODELOS.Visible = False
            .GRIDIMPREMODELOS.Rows.Clear()
            .ComboBoxModImpreAREAS.Items.Clear()
            .ComboBoxModImpreAREAS.Visible = False
        End With
        With MyFrm17
            .GRIDMIMP.Visible = False
            .GRIDMIMP.Rows.Clear()
        End With
        '
        Dim dt As DataSet = New DataSet
        '
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "IMPRESORAS")
            '
            If dt.Tables("IMPRESORAS").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("IMPRESORAS").Rows
                    '
                    MyFrm6.GRIDIMPREMODELOS.Rows.Add(pRow("IMPRESORA").ToString())
                    MyFrm17.GRIDMIMP.Rows.Add(pRow("IMPRESORA").ToString())
                    MyFrm6.ComboBoxModImpreAREAS.Items.Add(pRow("IMPRESORA").ToString())
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla LISTAR [IMPRESORAS]")
        End Try
        '
        With MyFrm6
            If .GRIDIMPREMODELOS.Rows.Count > 0 Then
                .GRIDIMPREMODELOS.CurrentCell = .GRIDIMPREMODELOS.Rows(0).Cells(0)
            End If
            .GRIDIMPREMODELOS.Visible = True
            .ComboBoxModImpreAREAS.Visible = True
        End With
        With MyFrm17
            .GRIDMIMP.Visible = True
        End With
        '
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub CargaListaTKFavoritos()
        '
        ' Lista de Textos Favoritos Para MENSAJES AREAS.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [TKFAVORITOS] ORDER BY "
        queryString &= "[TKFAVORITOS].[ID]"
        '
        MyFrm18.GRIDTEXTOSPREDEF.Visible = False
        MyFrm18.GRIDTEXTOSPREDEF.Rows.Clear()
        '
        Dim dt As DataSet = New DataSet
        '
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "TKFAVORITOS")
            '
            If dt.Tables("TKFAVORITOS").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("TKFAVORITOS").Rows
                    '
                    MyFrm18.GRIDTEXTOSPREDEF.Rows.Add(pRow("TEXTO1").ToString(),
                                              pRow("TEXTO2").ToString(),
                                              pRow("TEXTO3").ToString(),
                                              pRow("TEXTO4").ToString(),
                                              pRow("ID").ToString())
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla LISTAR [TKFAVORITOS]")
        End Try
        MyFrm18.GRIDTEXTOSPREDEF.Visible = True
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub TraspasaAreasEntreCajas(wCajaOrigen As Integer,
                                       wCajaDestino As Integer)
        '
        ' Este procedimiento Traspasa las AREAS de una Caja a Otra.
        ' La finalidad es la de facilitar la implementacion de AREAS en las
        '  distintas Cajas.
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        queryString = ""
        queryString &= "INSERT INTO [AREAS] ( "
        queryString &= "[AREAS].[NUMCAJA], "
        queryString &= "[AREAS].[AREA], "
        queryString &= "[AREAS].[DESCRIPCION], "
        queryString &= "[AREAS].[PUERTOIMPRE], "
        queryString &= "[AREAS].[AREA2], "
        queryString &= "[AREAS].[AREA3], "
        queryString &= "[AREAS].[AREA4], "
        queryString &= "[AREAS].[REPLICAR], "
        queryString &= "[AREAS].[TIPOIMPRESION], "
        queryString &= "[AREAS].[MODELOIMPRE] ) "
        queryString &= " SELECT "
        queryString &= wCajaDestino & ", "
        queryString &= "[AREAS].[AREA], "
        queryString &= "[AREAS].[DESCRIPCION], "
        queryString &= "[AREAS].[PUERTOIMPRE], "
        queryString &= "[AREAS].[AREA2], "
        queryString &= "[AREAS].[AREA3], "
        queryString &= "[AREAS].[AREA4], "
        queryString &= "[AREAS].[REPLICAR], "
        queryString &= "[AREAS].[TIPOIMPRESION], "
        queryString &= "[AREAS].[MODELOIMPRE] "
        queryString &= " FROM [AREAS] "
        queryString &= " WHERE [AREAS].[NUMCAJA] =" & wCajaOrigen
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                MsgBoxStyle.Exclamation Or
                MsgBoxStyle.OkOnly,
                               "Comprobar Tabla, Grabando Datos [MESABORLIN]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Function CargaListaAREAS(wCaja As Integer,
                                    Optional wOpc As Integer = 0) As Boolean
        '
        ' Lectura de registros AREAS de CAJA actual.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [AREAS] WHERE "
        queryString = queryString & "[AREAS].[NUMCAJA]=" & wCaja & " "
        queryString = queryString & "ORDER BY "
        queryString = queryString & "[AREAS].[AREA]"
        '
        If wOpc = 0 Then
            MyFrm6.GRIDAREAS.Visible = False
            MyFrm6.GRIDAREAS.Rows.Clear()
            MyFrm18.GRIDAREAS1.Visible = False
            MyFrm18.GRIDAREAS1.Rows.Clear()
        End If
        '
        Dim dt As DataSet = New DataSet
        CargaListaAREAS = False
        '
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "AREAS")
            '
            If dt.Tables("AREAS").Rows.Count > 0 Then
                CargaListaAREAS = True
                '
                ' Solo queremos saber Si la Caja, Tiene AREAS creadas.
                '
                If wOpc = 1 Then
                    conexion.Close()
                    dt.Dispose()
                    conexion.Dispose()
                    Exit Function
                End If
                '
                Dim pRow As DataRow
                For Each pRow In dt.Tables("AREAS").Rows
                    '
                    ' GRIDAREAS:
                    '   0 Cod. Area
                    '   1 Descripcion
                    '   2 Puerto Impresora
                    '   3 Area 2
                    '   4 Area 3
                    '   5 Area 4
                    '   6 Tipo
                    '   7 Tipo Impresion (0=Directa / 1=Windows)
                    '   8 Modelo de IMPRESORA
                    '
                    Dim Mitipo As String = ""
                    If IsDBNull(pRow("TIPOIMPRESION")) Or pRow("TIPOIMPRESION").ToString = "False" Then
                        Mitipo = "False"
                    Else
                        Mitipo = "True"
                    End If
                    '
                    ' Mant. de AREAS
                    '
                    MyFrm6.GRIDAREAS.Rows.Add(pRow("AREA").ToString(),
                                              pRow("DESCRIPCION").ToString(),
                                              pRow("PUERTOIMPRE").ToString(),
                                              pRow("AREA2").ToString(),
                                              pRow("AREA3").ToString(),
                                              pRow("AREA4").ToString(),
                                              pRow("REPLICAR").ToString(),
                                              Mitipo,
                                              pRow("MODELOIMPRE").ToString()
                                              )
                    '
                    ' Mensajes a AREAS
                    '
                    MyFrm18.GRIDAREAS1.Rows.Add(pRow("AREA").ToString(),
                                              pRow("DESCRIPCION").ToString(),
                                              pRow("PUERTOIMPRE").ToString(),
                                              pRow("AREA2").ToString(),
                                              pRow("AREA3").ToString(),
                                              pRow("AREA4").ToString(),
                                              pRow("REPLICAR").ToString(),
                                              Mitipo,
                                              pRow("MODELOIMPRE").ToString()
                                              )
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla LISTAR [AREAS]")
        End Try
        '
        If wOpc = 0 Then
            With MyFrm6
                If .GRIDAREAS.Rows.Count > 0 Then
                    .GRIDAREAS.CurrentCell = .GRIDAREAS.Rows(0).Cells(0)
                End If
                .GRIDAREAS.Visible = True
            End With
            MyFrm18.GRIDAREAS1.Visible = True
        End If
        '
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Sub AccionCobroAceptarSepa()
        '
        ' Proceso de Cobro para las MESAS SEPARADAS:
        '    [ 1 ] Actualizar el COBRO en [MESAC].
        '    [ 2 ] Impresion del TICKET ...
        '    [ 3 ] La Mesa SEPARADA será BORRADA de [SALA1]
        '
        If ExisteRegistroMESAC(MyFrm13.TextBoxSepNumMesa1.Text.Trim, 999) Then
            '
            ' Si hay registro lo actualiza:
            '    Efectivo = Total Cuenta
            '    Tarjeta, Cheques, Otros, Etc = 0
            '
            ActualizaDatosMESAC_Sep(MyFrm13.TextBoxSepNumMesa1.Text.Trim, 0)
            '
            '
            ' Imprime Factura
            '
            If MyFrm13.GRID1SepaMesa1.Rows.Count > 0 Then
                If wrLeeMESAC.Mesac_TKFACIMPRESO = "False" Then
                    ImprimeTicketFactura("S")
                End If
            End If
            '
            ' Borramos esta mesa separada de la SALA 999
            ' Evitamos acumulacion excesiva de registros muertos o huerfanos
            ' en [SALA1]
            ' Para asegurar se Borra por Num. MESA y Num. FACTURA
            ' 
            If MyFrm13.TextBoxSepNumMesa1.Text.Trim <> "999" Then
                BorraMesaSepa(MyFrm13.TextBoxSepNumMesa1.Text.Trim, CInt(MyFrm13.TextBoxSepFactura1.Text.Trim))
            End If
            '
            ' Vaciar Lista Separada
            '
            MyFrm13.GRID1SepaMesa1.Rows.Clear()
            MyFrm13.Hide()
            '
        End If
        '
    End Sub

    Public Sub AccionCobroAceptar()
        '
        ' Proceso de Cobro, la Mesa queda Libre, FACTURA=0 [SALA1]
        ' OJO!, en este ORDEN, wFacturaN se usará para determinar
        '    existencia de MESAC.
        '
        SwAparca = False ' importante!!!
        '
        ' Ojo!, establecer en True, aun cuando no se lleve a cabo el cobro.
        ' Esto informa a FORM de mesas que debe refrescar la informacion en pantalla.
        '
        SwCobrando = True ' importante!!!
        '
        ' Si no hemos fichado NADA, No hay que COBRAR, salimos sin MAS.
        '
        If ExisteRegistroMESAC(MyFrm2.TextBoxNumMesa.Text.Trim, 0) Then
            '
            ' Si hay registro lo actualiza:
            '    Efectivo, Tarjeta, Cheques, Otros, Etc...
            '
            ActualizaDatosMESAC(MyFrm2.TextBoxNumMesa.Text.Trim, 0)
            '
            ' Y liberamos esta MESA, O bien
            '
            ' Borramos mesas separadas de la SALA 999
            ' Evitamos acumulacion excesiva de registros muertos o huerfanos
            ' en [SALA1]
            ' Para asegurar se Borra por Num. MESA y Num. FACTURA
            ' OJO! aqui se evita la Mesa 999
            '
            If wCodSala = "999" And MyFrm2.TextBoxNumMesa.Text.Trim <> "999" Then
                BorraMesaSepa(MyFrm2.TextBoxNumMesa.Text.Trim, CInt(MyFrm2.TextBoxFactura.Text.Trim))
            Else
                wFacturaN = 0
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 0)
            End If
            '
            ' En este Punto comprobamos si se ha IMPRESO el TICKET Factura.
            ' Si no se ha hecho, se fuerza su impresión Automaticamente.
            '
            If MyFrm2.GRID1.Rows.Count > 0 Then
                If wrLeeMESAC.Mesac_TKFACIMPRESO = "False" Then
                    ImprimeTicketFactura("N")
                End If
            End If
        End If
        '
        ' Dependiendo de Si el Form Incial es MESAS o PRODUCTOS.
        '
        If FormularioInicial = 0 Then
            MyFrm2.Hide()
            MyFrm1.Show()
        Else
            '
            ' Refrescamos el CONTADOR de FACTURA 
            ' y otros aspectos en Pantalla TCONA402
            '
            If LeeTCONA4Cfg("Factura") = True Then
                With MyFrm2
                    .TextBoxFactura.Text = wFacturaN.ToString
                    .GRID1.Rows.Clear()
                    .LabelTotComanda.Text = "0.00"
                End With
            End If
        End If
        '
    End Sub

    Public Sub ImprimeFacturaA4(iOPC As String)
        '
        ' Procediemiento Para la Impresion de FACTURA formato A4
        '   Lanzamos informe a COBVIEW / DIRECTA, segun Ref. Generales
        '   En este caso la salida son las filas contenidas en el GRID1.
        '
        ' iOPC :: Por si se necesita ...
        '         
        '
        ' Definimos.:
        '    Número de líneas Máximas a Reflejar en la FACTURA.
        '    Las líneas que sean SPACES se ignorarán.
        '
        Dim MiSala As String = "" : Dim MiMesa As String = ""
        Dim MiTotal As Double = 0 : Dim MiCalcImp As Double = 0
        Dim MiCalcPVP As Decimal = 0 : Dim MiCalcDto As Decimal = 0
        '
        MiTotal = CDbl(MyFrm2.LabelTotComanda.Text.Replace(".", ",").Trim)
        MiSala = wCodSala
        MiMesa = MyFrm2.TextBoxNumMesa.Text.Trim
        '
        ' ABRIR el CAJÓN. (Al principio)
        '
        If LeeTCONA4Cfg("General") = True Then
            '
            ' Es impresion Directa ("True") o Windows("False")?
            '
            '
            ' NOTA.: Por ahora Fuerzo siempre impresion WINDOWS -> COBVIEW
            '
            wrLeeTCONA4.Tcona4_TKFACDIRWIN = "False"

            Select Case wrLeeTCONA4.Tcona4_TKFACDIRWIN
                Case "True"
                    '
                    ' *** IMPRESION DIRECTA A PUERTO DE CAPTURA ***
                    '
                    ' ABRIR el Cajón.
                    '
                    Try
                        AbreCajon_Corte(0, wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, "WINDOWS")
                    Catch ex As Exception
                        MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                        MsgBoxStyle.Exclamation Or
                                        MsgBoxStyle.OkOnly,
                                        " *** Error de Comados a Impresora. *** ")
                    End Try
                Case "False"
                    '
                    ' *** IMPRESION COBVIEW o a Impresora WINDOWS ***
                    '
                    ' Si el MODELO de impresora no esta ON-LINE y 
                    '  no hacemos "PREVIEW" salimos.
                    '
                    If ImpresoraEstaONLINE(ObtenerImpresoraPredeterminada.Trim) = False And
                       wrLeeTCONA4.Tcona4_COBVIEWPDSN = "False" Then
                        title = "¿Impresora Apagada?"
                        style = MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly
                        msg = "Por favor, compruebe su impresora." & vbCrLf
                        msg &= "NO se imprimirá el TICKET." & vbCrLf & vbCrLf
                        msg &= "Se detectan " & wrProp_IMPRESORA.JobCountSinceLastReset & " trabajos Pendientes."
                        response = MsgBox(msg, style, title)
                        Exit Sub
                    End If
                    '
                    ' Si lo indica Ref. Generales ABRIR el Cajón
                    '
                    If wrLeeTCONA4.Tcona4_TKFACABRECAJON = "true" Then
                        Try
                            AbreCajon_Corte(0, wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, "WINDOWS")
                        Catch ex As Exception
                            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                        MsgBoxStyle.Exclamation Or
                                        MsgBoxStyle.OkOnly,
                                        " *** Error de Comados a Impresora. *** ")
                        End Try
                    End If
            End Select
            ' Desde Aqui controlo SI queremos que la FACTURA se IMPRIMA o no:
            '  - Primer Filtro Ref. Generales IMPRIMIR S/N.
            '  - Segundo Filtro Ref. Generales Depende de importe Minimo.
            '  - Tercer Filtro La MESA indica si se debe imprimir factura o no.
            '
            ' Filtro (1) > (2)(3)
            '
            If wrLeeTCONA4.Tcona4_IMPRIMETKFAC = "False" Then
                Exit Sub
            Else
                '
                ' Filtro (2) > (3)
                '
                If MiTotal < wrLeeTCONA4.Tcona4_IMPOMINIMPRE Then
                    Exit Sub
                Else
                    '
                    ' Filtro (3)
                    ' Excepto para BARRA 1 / BARRA 2
                    '
                    LeeMesa_SALA1(wCodSala, MiMesa, 1)
                    If wrLeeSALA1.Sala1_IMPFACTU = "False" And
                        wTarifaBarra = 0 Then
                        Exit Sub
                    End If
                End If
            End If
        End If
        '
        Dim DatosCab(20) As String '  Lineas CABECERA
        Dim DatosPie(30) As String '  Lineas PIE
        '
        Dim TKLogo As Boolean = True
        Dim counter As Integer
        Dim MiCadenaWIEW As String = ""
        '
        If LeeMesa_SALA1(wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 0) = True Then
            If wMesaLibre = True Then
                FechaMESAC = Date.Now.ToShortDateString
                HoraMESAC = Date.Now.ToShortTimeString
                ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End If
        Dim FechaTK As String = FechaMESAC
        '
        ' Hora de Apertura MESA ???
        ' Hora de Impresion de la Factura ???
        '
        Dim HoraTK As String = TimeOfDay.ToLongTimeString.ToString
        '
        ' Aqui hay DOs vías posibles:
        '   - Impresion Directa ("True") o Windows("False")?
        '
        '
        '  NOTA.: Por ahora Fuerzo siempre impresion WINDOWS -> COBVIEW
        '
        wrLeeTCONA4.Tcona4_TKFACDIRWIN = "False"
        '
        Select Case wrLeeTCONA4.Tcona4_TKFACDIRWIN
            '
            ' *** IMPRESION WINDOWS ***
            '
            Case "False"
                Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\TKFACTA4.TXT", True, System.Text.Encoding.Default)
                    '
                    ' Logo SI/NO depende de Ref. Generales
                    '
                    If LeeTCONA4Cfg("General") = True Then
                        If wrLeeTCONA4.Tcona4_TKFACLOGO = "True" Then
                            sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKFACTA4L.def'>")
                        Else
                            sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKFACTA4.def'>")
                        End If
                    Else
                        '
                        ' Si no lograse leer el Ref. Genrales...
                        '
                        sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKFACTA4.def'>")
                    End If
                    '
                    ' Previsualizacion en COBVIEW, ahora determinado por Ref. Generales
                    '
                    If wrLeeTCONA4.Tcona4_COBVIEWPDSN = "True" Then
                        sw.WriteLine("<SET PRINTDIRECT='NO'>")
                    Else
                        sw.WriteLine("<SET PRINTDIRECT='YES'>")
                    End If
                    '
                    ' Líneas Cabecera en Ref. Generales
                    ' Las Primeras líneas incluirán Datos del Cliente que
                    '   puede ser:
                    '
                    ' CONTADO
                    '
                    wrLeeCLIEMCO.DTO = "0,00"
                    Dim LineasCliCabe As String() = {"", "", "", "", ""}
                    If WMesacNIFCIF.Trim.Length > 0 Then
                        If LeeClienteCONTA(WMesacNIFCIF.Trim) = True Then
                            With wrLeeCLIEMCO
                                LineasCliCabe(0) = .NOMBRE
                                LineasCliCabe(1) = .DIRECCION
                                LineasCliCabe(2) = .CODPOSTAL & " " & .POBLACION
                                LineasCliCabe(3) = .TELEFONO & " " & .TELEFONO2
                                LineasCliCabe(4) = .CIF
                            End With
                        End If
                    End If
                    '
                    ' CREDITO.
                    '
                    If wCliente > 0 And wCliente <> 430000000 Then
                        If LeeClienteMCO(wCliente) = True Then
                            With wrLeeCLIEMCO
                                LineasCliCabe(0) = .NOMBRE
                                LineasCliCabe(1) = .DIRECCION
                                LineasCliCabe(2) = .CODPOSTAL & " " & .POBLACION
                                LineasCliCabe(3) = .TELEFONO & " " & .TELEFONO2
                                LineasCliCabe(4) = .CIF
                            End With
                        End If
                    End If
                    '
                    Dim SepaSpace As String = ""
                    For i As Integer = 0 To 20
                        SepaSpace &= " "
                    Next
                    '
                    With wrLeeTCONA4
                        DatosCab(0) = .Tcona4_TKFCABLI1.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(0).Trim
                        DatosCab(1) = .Tcona4_TKFCABLI2.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(1).Trim
                        DatosCab(2) = .Tcona4_TKFCABLI3.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(2).Trim
                        DatosCab(3) = .Tcona4_TKFCABLI4.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(3).Trim
                        DatosCab(4) = .Tcona4_TKFCABLI5.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(4).Trim
                        DatosCab(5) = .Tcona4_TKFCABLI6
                        DatosCab(6) = .Tcona4_TKFCABLI7
                        DatosCab(7) = .Tcona4_TKFCABLI8
                        DatosCab(8) = .Tcona4_TKFCABLI9
                        DatosCab(9) = .Tcona4_TKFCABLI10
                    End With
                    '
                    For Each element As String In DatosCab
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                sw.WriteLine("<BR>")
                            End If
                            If element.Trim.Length > 0 Then
                                sw.WriteLine(element)
                                sw.WriteLine("<BR>")
                            End If
                        End If
                    Next
                    '
                    sw.WriteLine("<b>MESA.: " & MyFrm2.TextBoxNumMesa.Text.Trim & "</b><BR>")
                    'sw.WriteLine(MyFrm6.TextBoxDetCab1.Text & "<BR>")
                    'sw.WriteLine(MyFrm6.TextBoxDetCab2.Text & "<BR>")
                    'sw.WriteLine(MyFrm6.TextBoxDetCab3.Text & "<BR>")
                    sw.WriteLine("--------------- ------------------------- --------- --------- --------- ---------<BR>")
                    sw.WriteLine("C ó d i g o     D e s c r i p c i ó n     Unidades  P.V.P.    % Dto.    Importe  <BR>")
                    sw.WriteLine("--------------- ------------------------- --------- --------- --------- ---------<BR>")
                    '
                    ' Líneas GRID
                    '
                    MiTotal = 0
                    For counter = 0 To (MyFrm2.GRID1.Rows.Count - 1)
                        '
                        ' <COL #1> ... <COL #n>
                        ' ----- -------- -------
                        '
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm2.GRID1.Rows(counter).Cells(0).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString
                        '
                        ' UNID.
                        '
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                        MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")).ToString(fmtUnid).Replace(",", ".")
                        '
                        ' PVP
                        '
                        MiCalcPVP = (CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")) / CDec(MyFrm2.GRID1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")))
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                        MiCadenaWIEW = MiCadenaWIEW & MiCalcPVP.ToString(fmtImporte).Replace(",", ".")
                        '
                        ' DTO
                        '
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                        MiCadenaWIEW = MiCadenaWIEW & CDec(wrLeeCLIEMCO.DTO.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".")
                        '
                        ' Importe - Dto
                        '
                        MiCalcDto =
                            ((CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")) _
                            * CDec(wrLeeCLIEMCO.DTO.ToString.Replace(".", ","))) / 100)
                        MiCalcImp = CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ","))
                        MiCalcImp = MiCalcImp - MiCalcDto
                        '
                        ' Recalculamos Total
                        '
                        MiTotal += MiCalcImp
                        '
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                        MiCadenaWIEW = MiCadenaWIEW & MiCalcImp.ToString(fmtImporte).Replace(",", ".")
                        '
                        sw.WriteLine(MiCadenaWIEW)
                        '
                        ' *** Combinados *** Si Hay y Si esta marcado en Ref. Generales
                        '
                        If wrLeeTCONA4.Tcona4_TKFACIMPDETCOMBI = "True" Then
                            If MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Length > 0 Then
                                Dim words As String() = MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                                For i As Integer = 0 To words.Length - 1
                                    If LeeMar(words(i)) = False Then
                                        wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                    Else
                                        MiCadenaWIEW = "<COL #1>"
                                        MiCadenaWIEW = MiCadenaWIEW & "  [+]"
                                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                                        MiCadenaWIEW = MiCadenaWIEW & wrLeeMAR.Mar_DESCRIPCION.Trim
                                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                                        MiCadenaWIEW = MiCadenaWIEW & ""
                                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                                        MiCadenaWIEW = MiCadenaWIEW & ""
                                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                                        MiCadenaWIEW = MiCadenaWIEW & ""
                                        MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                                        MiCadenaWIEW = MiCadenaWIEW & ""
                                        sw.WriteLine(MiCadenaWIEW)
                                    End If
                                Next
                            End If
                        End If
                    Next
                    '
                    ' Linea de Totales // IGIC
                    '
                    '  Base   %   IGIC
                    '-------------------
                    '999.99 99.99 999.99
                    '
                    '                          TOTAL: 9999.99
                    '                          --------------
                    sw.WriteLine("<BR>")
                    sw.WriteLine(" Base       %     IGIC   <BR>")
                    sw.WriteLine("------------------------ <BR>")
                    '
                    Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
                    Dim Micalculo As Double = (MiPorIGIC / 100) + 1
                    Dim MiBase As Double = Math.Round((MiTotal / Micalculo), 2)
                    Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
                    '
                    ' Números con formato Ajustados a la DERECHA y Tamaño Fijo.
                    '
                    MiCadenaWIEW = ""
                    MiCadenaWIEW &= MiBase.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiPorIGIC.ToString(fmtUnid).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiImpIGIC.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    sw.WriteLine(MiCadenaWIEW)
                    '
                    ' Total
                    '
                    SepaSpace = ""
                    For i As Integer = 0 To 42
                        SepaSpace &= " "
                    Next
                    '
                    sw.WriteLine("<BR><BR>")
                    MiCadenaWIEW = "<font face='Consolas' size='14'>"
                    sw.WriteLine(MiCadenaWIEW)
                    MiCadenaWIEW = "<B><i>" & SepaSpace & "Total: " & MiTotal.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" ")) & "</i></B>"
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("<BR><BR>")
                    MiCadenaWIEW = "<font face='Consolas' size='09'>"
                    sw.WriteLine(MiCadenaWIEW)
                    '
                    ' Líneas Pie Fijas
                    '
                    'DatosPie(0) = "FACTURA SIMPLIFICADA  -  Num: " & MyFrm2.TextBoxFactura.Text.Trim
                    DatosPie(0) = "FACTURA Num.: " & MyFrm2.TextBoxFactura.Text.Trim
                    DatosPie(1) = "Ven: " & MyFrm2.TextBoxCamarero.Text.Trim &
                          "       Fecha: " & FechaTK & " " & HoraTK
                    '
                    ' Líneas Pie en Ref. Generales.
                    '
                    With wrLeeTCONA4
                        DatosPie(2) = .Tcona4_TKFPIELI1
                        DatosPie(3) = .Tcona4_TKFPIELI2
                        DatosPie(4) = .Tcona4_TKFPIELI3
                        DatosPie(5) = .Tcona4_TKFPIELI4
                        DatosPie(6) = .Tcona4_TKFPIELI5
                        DatosPie(7) = .Tcona4_TKFPIELI6
                        DatosPie(8) = .Tcona4_TKFPIELI7
                        DatosPie(9) = .Tcona4_TKFPIELI8
                        DatosPie(10) = .Tcona4_TKFPIELI9
                        DatosPie(11) = .Tcona4_TKFPIELI10
                        DatosPie(12) = .Tcona4_TKFPIELI11
                        DatosPie(13) = .Tcona4_TKFPIELI12
                        DatosPie(14) = .Tcona4_TKFPIELI13
                        DatosPie(15) = .Tcona4_TKFPIELI14
                        DatosPie(16) = .Tcona4_TKFPIELI15
                        DatosPie(17) = .Tcona4_TKFPIELI16
                        DatosPie(18) = .Tcona4_TKFPIELI17
                        DatosPie(19) = .Tcona4_TKFPIELI18
                        DatosPie(20) = .Tcona4_TKFPIELI19
                        DatosPie(21) = .Tcona4_TKFPIELI20
                    End With
                    '
                    For Each element As String In DatosPie
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                sw.WriteLine("<BR>")
                            End If
                            If element.Trim.Length > 0 Then
                                sw.WriteLine(element)
                                sw.WriteLine("<BR>")
                            End If
                        End If
                    Next
                    '
                    sw.Flush()
                    sw.Close()
                    '
                End Using
                '
                ' Llamada a COBVIEW pasandole como parámetro el informe generado...
                '
                Dim myProcess As New Process()
                Dim StringArguments As String
                Try
                    StringArguments = ""
                    myProcess.StartInfo.UseShellExecute = True
                    myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
                    StringArguments = "C:\TRIVAGES\InformesCobview\TKFACTA4.TXT"
                    myProcess.StartInfo.Arguments = StringArguments
                    myProcess.Start()
                    '
                    ' Si este proceso va bien, El ticket ha sido impreso...
                    '
                    ActualizaDatosMESAC(MyFrm2.TextBoxNumMesa.Text.Trim, 2)
                    '
                Catch Mye As Exception
                    MsgBox("ERROR: " & Mye.Source & vbCrLf & Mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: COBVIEW.")
                End Try
            Case "True"
                '
                ' *** IMPRESION DIRECTA A PUERTO de CAPTURA ***
                '
                ' Comprobar que HAY UN PUERTO definido.
                '
                If wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim.Length = 0 Then
                    title = "¿Puerto de Captura No Definido.?"
                    style = MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly
                    msg = "Por favor, compruebe el Puerto de Captura," & vbCrLf
                    msg = " para TICKETS FACTURA cuando es impresión directa." & vbCrLf
                    msg &= "NO se imprimirá el TICKET." & vbCrLf
                    response = MsgBox(msg, style, title)
                    Exit Sub
                End If
                '
                ' Comprobar, Modelo esta creado en IMPRESORAS?
                '
                If LeeDatosImpresora(wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim) = False Then
                    '
                    ' El MODELO de Impresora no esta creado en la Tabla
                    ' IMPRESORAS, y damos un Aviso
                    '
                    msg = "El MODELO de Impresora " & vbCrLf
                    msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                    msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
                    style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                    title = "Modelo de impresora no Creado."
                    MsgBox(msg, style, title)
                    Exit Sub
                End If
                '
                ' Comprobación del estado de la impresora...
                ' NOTA: Ya se realiza la coprobación antes de llegar a este punto.
                '
                Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
                fh = CType(Win32API.CreateFile(wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                           Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
                Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
                FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
                SW = New StreamWriter(FS)
                '
                ' *** TICKET ***
                '
                Try
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    '
                    ' Líneas Cabecera en Ref. Generales
                    ' Las Primeras líneas incluirán Datos del Cliente que
                    '   puede ser:
                    '
                    ' CONTADO
                    '
                    wrLeeCLIEMCO.DTO = "0,00"
                    Dim LineasCliCabe As String() = {"", "", "", "", ""}
                    If WMesacNIFCIF.Trim.Length > 0 Then
                        If LeeClienteCONTA(WMesacNIFCIF.Trim) = True Then
                            With wrLeeCLIEMCO
                                LineasCliCabe(0) = .NOMBRE
                                LineasCliCabe(1) = .DIRECCION
                                LineasCliCabe(2) = .CODPOSTAL & " " & .POBLACION
                                LineasCliCabe(3) = .TELEFONO & " " & .TELEFONO2
                                LineasCliCabe(4) = .CIF
                            End With
                        End If
                    End If
                    '
                    ' CREDITO.
                    '
                    If wCliente > 0 And wCliente <> 430000000 Then
                        If LeeClienteMCO(wCliente) = True Then
                            With wrLeeCLIEMCO
                                LineasCliCabe(0) = .NOMBRE
                                LineasCliCabe(1) = .DIRECCION
                                LineasCliCabe(2) = .CODPOSTAL & " " & .POBLACION
                                LineasCliCabe(3) = .TELEFONO & " " & .TELEFONO2
                                LineasCliCabe(4) = .CIF
                            End With
                        End If
                    End If
                    '
                    Dim SepaSpace As String = ""
                    For i As Integer = 0 To 20
                        SepaSpace &= " "
                    Next
                    '
                    With wrLeeTCONA4
                        DatosCab(0) = .Tcona4_TKFCABLI1.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(0).Trim
                        DatosCab(1) = .Tcona4_TKFCABLI2.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(1).Trim
                        DatosCab(2) = .Tcona4_TKFCABLI3.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(2).Trim
                        DatosCab(3) = .Tcona4_TKFCABLI4.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(3).Trim
                        DatosCab(4) = .Tcona4_TKFCABLI5.PadRight(40, CChar(" ")) & SepaSpace & LineasCliCabe(4).Trim
                        DatosCab(5) = .Tcona4_TKFCABLI6
                        DatosCab(6) = .Tcona4_TKFCABLI7
                        DatosCab(7) = .Tcona4_TKFCABLI8
                        DatosCab(8) = .Tcona4_TKFCABLI9
                        DatosCab(9) = .Tcona4_TKFCABLI10
                    End With
                    '
                    For Each element As String In DatosCab
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                            End If
                            If element.Trim.Length > 0 Then
                                SW.WriteLine(element)
                            End If
                        End If
                    Next
                    '
                    SW.WriteLine(wrIMPRESORA.DOBLEANCHO)
                    '
                    SW.WriteLine("MESA.: " & MyFrm2.TextBoxNumMesa.Text.Trim)
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    '
                    'SW.WriteLine(MyFrm6.TextBoxDetCab1.Text)
                    'SW.WriteLine(MyFrm6.TextBoxDetCab2.Text)
                    'SW.WriteLine(MyFrm6.TextBoxDetCab3.Text)
                    SW.WriteLine("--------------- ------------------------- --------- --------- --------- ---------<BR>")
                    SW.WriteLine("C ó d i g o     D e s c r i p c i ó n     Unidades  P.V.P.    % Dto.    Importe  <BR>")
                    SW.WriteLine("--------------- ------------------------- --------- --------- --------- ---------<BR>")
                    '
                    ' Líneas GRID
                    '
                    For counter = 0 To (MyFrm2.GRID1.Rows.Count - 1)
                        '
                        MiCadenaWIEW = ""
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm2.GRID1.Rows(counter).Cells(0).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString
                        '
                        ' UNID.
                        '
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")).ToString(fmtUnid).Replace(",", ".")
                        '
                        ' PVP
                        '
                        MiCalcPVP = (CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")) / CDec(MyFrm2.GRID1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")))
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & MiCalcPVP.ToString(fmtImporte).Replace(",", ".")
                        '
                        ' DTO
                        '
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & CDec(wrLeeCLIEMCO.DTO.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".")
                        '
                        ' Importe
                        '
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".")
                        '
                        SW.WriteLine(MiCadenaWIEW)
                        '
                        ' *** Combinados *** Si Hay y Si esta marcado en Ref. Generales
                        '
                        If wrLeeTCONA4.Tcona4_TKFACIMPDETCOMBI = "True" Then
                            If MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Length > 0 Then
                                Dim words As String() = MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                                For i As Integer = 0 To words.Length - 1
                                    If LeeMar(words(i)) = False Then
                                        wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                    Else
                                        MiCadenaWIEW = "       "
                                        MiCadenaWIEW = MiCadenaWIEW & " "
                                        MiCadenaWIEW = MiCadenaWIEW & "  [+]"
                                        MiCadenaWIEW = MiCadenaWIEW & " "
                                        MiCadenaWIEW = MiCadenaWIEW & wrLeeMAR.Mar_DESCRIPCION.Trim
                                        SW.WriteLine(MiCadenaWIEW)
                                    End If
                                Next
                            End If
                        End If
                    Next
                    '
                    ' Linea de Totales // IGIC
                    '
                    '  Base   %   IGIC
                    '-------------------
                    '999.99 99.99 999.99
                    '
                    '                          TOTAL: 9999.99
                    '                          --------------
                    SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    SW.WriteLine(" Base       %     IGIC   ")
                    SW.WriteLine("------------------------ ")
                    '
                    Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
                    Dim Micalculo As Double = (MiPorIGIC / 100) + 1
                    Dim MiBase As Double = Math.Round((MiTotal / Micalculo), 2)
                    Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
                    '
                    ' Números con formato Ajustados a la DERECHA y Tamaño Fijo.
                    '
                    MiCadenaWIEW = ""
                    MiCadenaWIEW &= MiBase.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiPorIGIC.ToString(fmtUnid).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiImpIGIC.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    SW.WriteLine(MiCadenaWIEW)
                    '
                    ' Total
                    '
                    SepaSpace = ""
                    For i As Integer = 0 To 42
                        SepaSpace &= " "
                    Next
                    '
                    SW.WriteLine(wrIMPRESORA.DOBLEALTO)
                    MiCadenaWIEW = SepaSpace & "Total: " & MiTotal.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" ")) & ""
                    SW.WriteLine(MiCadenaWIEW)
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    '
                    ' Líneas Pie Fijas
                    '
                    'DatosPie(0) = "FACTURA SIMPLIFICADA  -  Num: " & MyFrm2.TextBoxFactura.Text.Trim
                    DatosPie(0) = "FACTURA Num.: " & MyFrm2.TextBoxFactura.Text.Trim
                    DatosPie(1) = "Ven: " & MyFrm2.TextBoxCamarero.Text.Trim &
                          "       Fecha: " & FechaTK & " " & HoraTK
                    '
                    ' Líneas Pie en Ref. Generales.
                    '
                    With wrLeeTCONA4
                        DatosPie(2) = .Tcona4_TKFPIELI1
                        DatosPie(3) = .Tcona4_TKFPIELI2
                        DatosPie(4) = .Tcona4_TKFPIELI3
                        DatosPie(5) = .Tcona4_TKFPIELI4
                        DatosPie(6) = .Tcona4_TKFPIELI5
                        DatosPie(7) = .Tcona4_TKFPIELI6
                        DatosPie(8) = .Tcona4_TKFPIELI7
                        DatosPie(9) = .Tcona4_TKFPIELI8
                        DatosPie(10) = .Tcona4_TKFPIELI9
                        DatosPie(11) = .Tcona4_TKFPIELI10
                        DatosPie(12) = .Tcona4_TKFPIELI11
                        DatosPie(13) = .Tcona4_TKFPIELI12
                        DatosPie(14) = .Tcona4_TKFPIELI13
                        DatosPie(15) = .Tcona4_TKFPIELI14
                        DatosPie(16) = .Tcona4_TKFPIELI15
                        DatosPie(17) = .Tcona4_TKFPIELI16
                        DatosPie(18) = .Tcona4_TKFPIELI17
                        DatosPie(19) = .Tcona4_TKFPIELI18
                        DatosPie(20) = .Tcona4_TKFPIELI19
                        DatosPie(21) = .Tcona4_TKFPIELI20
                    End With
                    '
                    For Each element As String In DatosPie
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                            End If
                            If element.Trim.Length > 0 Then
                                SW.WriteLine(element)
                            End If
                        End If
                    Next
                    '
                    ' Lineas Finales
                    '
                    ' Lineas <= 9
                    ' Por si se diera el caso, no derrochar MUCHO Papel ...
                    '
                    If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                            wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                        End If
                        For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                            SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                        Next
                    Else
                        SW.WriteLine(wrIMPRESORA.AVAZALINEA) ' Por defecto al menos 1 Linea de salto
                    End If
                    '
                    ' Corte PAPEL
                    '
                    SW.WriteLine(wrIMPRESORA.CORTE)
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                        MsgBoxStyle.Exclamation Or
                                                        MsgBoxStyle.OkOnly,
                                                        "TICKET FACTURA.: Error de Impresora.")
                End Try
                FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
                '
        End Select
        '
    End Sub

    Public Sub ImprimeTicketFactura(iOPC As String)
        '
        ' Procediemiento Para la Impresion de TICKETS -FACTURA-
        '   Lanzamos informe a COBVIEW / DIRECTA, segun Ref. Generales
        '   En este caso la salida son las filas contenidas en el GRID1
        '
        ' iOPC :: "N" Normalmente desde Productos.
        '         "S" Desde Mesas Separadas.
        '
        ' Definimos.:
        '    Número de líneas Máximas a Reflejar en el TICKET FACTURA.
        '    Las líneas que sean SPACES se ignorarán.
        '    El TOTAL de líneas comprende las PREDEFINIDAS DEL TICKET +
        '       las que contengan TEXTO en Ref. Generales.
        '
        ' TOTAL del TICKET FACTURA
        '
        Dim MiTotal As Double = 0 : Dim MiMesa As String = ""
        Dim MiSala As String = ""
        '
        Select Case iOPC
            Case "N"
                MiTotal = CDbl(MyFrm2.LabelTotComanda.Text.Replace(".", ",").Trim)
                MiSala = wCodSala
                MiMesa = MyFrm2.TextBoxNumMesa.Text.Trim
            Case "S"
                MiTotal = CDbl(MyFrm13.LabelTotComandaSep1.Text.Replace(".", ",").Trim)
                MiSala = "999"
                MiMesa = MyFrm13.TextBoxSepNumMesa1.Text.Trim
        End Select
        '
        ' ABRIR el CAJÓN. (Al principio)
        '
        If LeeTCONA4Cfg("General") = True Then
            '
            ' Es impresion Directa ("True") o Windows("False")?
            '
            Select Case wrLeeTCONA4.Tcona4_TKFACDIRWIN
                Case "True"
                    '
                    ' *** IMPRESION DIRECTA A PUERTO DE CAPTURA ***
                    '
                    ' ABRIR el Cajón.
                    '
                    Try
                        AbreCajon_Corte(0, wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, "WINDOWS")
                    Catch ex As Exception
                        MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                        MsgBoxStyle.Exclamation Or
                                        MsgBoxStyle.OkOnly,
                                        " *** Error de Comados a Impresora. *** ")
                    End Try
                Case "False"
                    '
                    ' *** IMPRESION COBVIEW o a Impresora WINDOWS ***
                    '
                    ' Si el MODELO de impresora no esta ON-LINE y 
                    '  no hacemos "PREVIEW" salimos.
                    '
                    If ImpresoraEstaONLINE(ObtenerImpresoraPredeterminada.Trim) = False And
                       wrLeeTCONA4.Tcona4_COBVIEWPDSN = "False" Then
                        title = "¿Impresora Apagada?"
                        style = MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly
                        msg = "Por favor, compruebe su impresora." & vbCrLf
                        msg &= "NO se imprimirá el TICKET." & vbCrLf & vbCrLf
                        msg &= "Se detectan " & wrProp_IMPRESORA.JobCountSinceLastReset & " trabajos Pendientes."
                        response = MsgBox(msg, style, title)
                        Exit Sub
                    End If
                    '
                    ' Si lo indica Ref. Generales ABRIR el Cajón
                    '
                    If wrLeeTCONA4.Tcona4_TKFACABRECAJON = "true" Then
                        Try
                            AbreCajon_Corte(0, wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, "WINDOWS")
                        Catch ex As Exception
                            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                        MsgBoxStyle.Exclamation Or
                                        MsgBoxStyle.OkOnly,
                                        " *** Error de Comados a Impresora. *** ")
                        End Try
                    End If
            End Select
            ' Desde Aqui controlo SI queremos que la FACTURA se IMPRIMA o no:
            '  - Primer Filtro Ref. Generales IMPRIMIR S/N.
            '  - Segundo Filtro Ref. Generales Depende de importe Minimo.
            '  - Tercer Filtro La MESA indica si se debe imprimir factura o no.
            '
            ' Filtro (1) > (2)(3)
            '
            If wrLeeTCONA4.Tcona4_IMPRIMETKFAC = "False" Then
                Exit Sub
            Else
                '
                ' Filtro (2) > (3)
                '
                If MiTotal < wrLeeTCONA4.Tcona4_IMPOMINIMPRE Then
                    Exit Sub
                Else
                    '
                    ' Filtro (3)
                    ' Excepto para BARRA 1 / BARRA 2
                    '
                    LeeMesa_SALA1(wCodSala, MiMesa, 1)
                    If wrLeeSALA1.Sala1_IMPFACTU = "False" And
                        wTarifaBarra = 0 Then
                        Exit Sub
                    End If
                End If
            End If
        End If
        '
        Dim DatosCab(20) As String '  Lineas CABECERA
        Dim DatosPie(30) As String '  Lineas PIE
        '
        Dim TKLogo As Boolean = True
        Dim counter As Integer
        Dim MiCadenaWIEW As String = ""
        '
        ' FACTURA MESAS NORMALES / SEPARADAS
        '
        Select Case iOPC
            Case "N"
                If LeeMesa_SALA1(wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 0) = True Then
                    If wMesaLibre = True Then
                        FechaMESAC = Date.Now.ToShortDateString
                        HoraMESAC = Date.Now.ToShortTimeString
                        ActualizaMesa_SALA1(wCaja, wCodSala, MyFrm2.TextBoxNumMesa.Text.Trim, 1)
                    End If
                End If
            Case "S"
                If LeeMesa_SALA1("999", MyFrm13.TextBoxSepNumMesa1.Text.Trim, 1) = True Then
                    If wMesaLibre = True Then
                        FechaMESAC = Date.Now.ToShortDateString
                        HoraMESAC = Date.Now.ToShortTimeString
                        ActualizaMesa_SALA1(wCaja, "999", MyFrm13.TextBoxSepNumMesa1.Text.Trim, 1)
                    End If
                End If
        End Select
        '
        Dim FechaTK As String = FechaMESAC
        '
        ' Hora de Apertura MESA ???
        ' Hora de Impresion del ticket ???
        '
        'Dim HoraTK As String = HoraMESAC
        Dim HoraTK As String = TimeOfDay.ToLongTimeString.ToString
        '
        ' Aqui hay DOs vías posibles:
        '   - Impresion Directa ("True") o Windows("False")?
        '
        Select Case wrLeeTCONA4.Tcona4_TKFACDIRWIN
            '
            ' *** IMPRESION WINDOWS ***
            '
            Case "False"
                Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\TKFACTURA.TXT", True, System.Text.Encoding.Default)
                    '
                    ' Logo SI/NO depende de Ref. Generales
                    '
                    If LeeTCONA4Cfg("General") = True Then
                        If wrLeeTCONA4.Tcona4_TKFACLOGO = "True" Then
                            sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKFACTURAL.def'>")
                        Else
                            sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKFACTURA.def'>")
                        End If
                    Else
                        '
                        ' Si no lograse leer el Ref. Genrales...
                        '
                        sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKFACTURA.def'>")
                    End If
                    '
                    ' Previsualizacion en COBVIEW, ahora determinado por Ref. Generales
                    '
                    If wrLeeTCONA4.Tcona4_COBVIEWPDSN = "True" Then
                        sw.WriteLine("<SET PRINTDIRECT='NO'>")
                    Else
                        sw.WriteLine("<SET PRINTDIRECT='YES'>")
                    End If
                    '
                    ' Líneas Cabecera en Ref. Generales
                    '
                    With wrLeeTCONA4
                        DatosCab(0) = .Tcona4_TKFCABLI1
                        DatosCab(1) = .Tcona4_TKFCABLI2
                        DatosCab(2) = .Tcona4_TKFCABLI3
                        DatosCab(3) = .Tcona4_TKFCABLI4
                        DatosCab(4) = .Tcona4_TKFCABLI5
                        DatosCab(5) = .Tcona4_TKFCABLI6
                        DatosCab(6) = .Tcona4_TKFCABLI7
                        DatosCab(7) = .Tcona4_TKFCABLI8
                        DatosCab(8) = .Tcona4_TKFCABLI9
                        DatosCab(9) = .Tcona4_TKFCABLI10
                    End With
                    '
                    For Each element As String In DatosCab
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                sw.WriteLine("<BR>")
                            End If
                            If element.Trim.Length > 0 Then
                                sw.WriteLine(element)
                                sw.WriteLine("<BR>")
                            End If
                        End If
                    Next
                    '
                    Select Case iOPC
                        Case "N"
                            sw.WriteLine("<b>MESA.: " & MyFrm2.TextBoxNumMesa.Text.Trim & "</b><BR>")
                        Case "S"
                            sw.WriteLine("<b>MESA.: " & MyFrm13.TextBoxSepNumMesa1.Text.Trim & "</b><BR>")
                    End Select
                    '
                    sw.WriteLine(MyFrm6.TextBoxDetCab1.Text & "<BR>")
                    sw.WriteLine(MyFrm6.TextBoxDetCab2.Text & "<BR>")
                    sw.WriteLine(MyFrm6.TextBoxDetCab3.Text & "<BR>")
                    '
                    ' Líneas GRID
                    '
                    Select Case iOPC
                        Case "N"
                            For counter = 0 To (MyFrm2.GRID1.Rows.Count - 1)
                                '
                                ' <COL #1> ... <COL #n>
                                ' ----- -------- -------
                                ' UNID. ARTICULO IMPORTE
                                '
                                MiCadenaWIEW = "<COL #1>"
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")).ToString(fmtUnid).Replace(",", ".")
                                MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                                MiCadenaWIEW = MiCadenaWIEW & MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString
                                MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".")
                                '
                                sw.WriteLine(MiCadenaWIEW)
                                '
                                ' *** Combinados *** Si Hay y Si esta marcado en Ref. Generales
                                '
                                If wrLeeTCONA4.Tcona4_TKFACIMPDETCOMBI = "True" Then
                                    If MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Length > 0 Then
                                        Dim words As String() = MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                                        For i As Integer = 0 To words.Length - 1
                                            If LeeMar(words(i)) = False Then
                                                wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                            Else
                                                MiCadenaWIEW = "<COL #1>"
                                                MiCadenaWIEW = MiCadenaWIEW & "  [+]"
                                                MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                                                MiCadenaWIEW = MiCadenaWIEW & wrLeeMAR.Mar_DESCRIPCION.Trim
                                                MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                                                MiCadenaWIEW = MiCadenaWIEW & ""
                                                sw.WriteLine(MiCadenaWIEW)
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        Case "S"
                            For counter = 0 To (MyFrm13.GRID1SepaMesa1.Rows.Count - 1)
                                '
                                ' <COL #1> ... <COL #n>
                                ' ----- -------- -------
                                ' UNID. ARTICULO IMPORTE
                                MiCadenaWIEW = "<COL #1>"
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")).ToString(fmtUnid).Replace(",", ".")
                                MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                                MiCadenaWIEW = MiCadenaWIEW & MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(2).Value.ToString
                                MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".")
                                '
                                sw.WriteLine(MiCadenaWIEW)
                                '
                                ' *** Combinados *** Si Hay y Si esta marcado en Ref. Generales
                                '
                                If wrLeeTCONA4.Tcona4_TKFACIMPDETCOMBI = "True" Then
                                    If MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(6).Value.ToString().Trim.Length > 0 Then
                                        Dim words As String() = MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                                        For i As Integer = 0 To words.Length - 1
                                            If LeeMar(words(i)) = False Then
                                                wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                            Else
                                                MiCadenaWIEW = "<COL #1>"
                                                MiCadenaWIEW = MiCadenaWIEW & "  [+]"
                                                MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                                                MiCadenaWIEW = MiCadenaWIEW & wrLeeMAR.Mar_DESCRIPCION.Trim
                                                MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                                                MiCadenaWIEW = MiCadenaWIEW & ""
                                                sw.WriteLine(MiCadenaWIEW)
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                    End Select
                    '
                    ' Linea de Totales // IGIC
                    '
                    '  Base   %   IGIC
                    '-------------------
                    '999.99 99.99 999.99
                    '
                    '                          TOTAL: 9999.99
                    '                          --------------
                    sw.WriteLine("<BR>")
                    sw.WriteLine(" Base       %     IGIC   <BR>")
                    sw.WriteLine("------------------------ <BR>")
                    '
                    Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
                    Dim Micalculo As Double = (MiPorIGIC / 100) + 1
                    Dim MiBase As Double = Math.Round((MiTotal / Micalculo), 2)
                    Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
                    '
                    ' Números con formato Ajustados a la DERECHA y Tamaño Fijo.
                    '
                    MiCadenaWIEW = ""
                    MiCadenaWIEW &= MiBase.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiPorIGIC.ToString(fmtUnid).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiImpIGIC.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    sw.WriteLine(MiCadenaWIEW)
                    '
                    ' Total
                    '
                    sw.WriteLine("<BR><BR>")
                    MiCadenaWIEW = "<font face='Consolas' size='14'>"
                    sw.WriteLine(MiCadenaWIEW)
                    MiCadenaWIEW = "<B><i>             Total: " & MiTotal.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" ")) & "</i></B>"
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("<BR><BR>")
                    MiCadenaWIEW = "<font face='Consolas' size='09'>"
                    sw.WriteLine(MiCadenaWIEW)
                    '
                    ' Líneas Pie Fijas
                    '
                    Select Case iOPC
                        Case "N"
                            DatosPie(0) = "FACTURA SIMPLIFICADA  -  Num: " & MyFrm2.TextBoxFactura.Text.Trim
                            DatosPie(1) = "Ven: " & MyFrm2.TextBoxCamarero.Text.Trim &
                          "       Fecha: " & FechaTK & " " & HoraTK
                        Case "S"
                            DatosPie(0) = "FACTURA SIMPLIFICADA  -  Num: " & MyFrm13.TextBoxSepFactura.Text.Trim
                            DatosPie(1) = "Ven: " & MyFrm13.TextBoxSepCamarero.Text.Trim &
                          "       Fecha: " & FechaTK & " " & HoraTK
                    End Select
                    '
                    ' Líneas Pie en Ref. Generales.
                    '
                    With wrLeeTCONA4
                        DatosPie(2) = .Tcona4_TKFPIELI1
                        DatosPie(3) = .Tcona4_TKFPIELI2
                        DatosPie(4) = .Tcona4_TKFPIELI3
                        DatosPie(5) = .Tcona4_TKFPIELI4
                        DatosPie(6) = .Tcona4_TKFPIELI5
                        DatosPie(7) = .Tcona4_TKFPIELI6
                        DatosPie(8) = .Tcona4_TKFPIELI7
                        DatosPie(9) = .Tcona4_TKFPIELI8
                        DatosPie(10) = .Tcona4_TKFPIELI9
                        DatosPie(11) = .Tcona4_TKFPIELI10
                        DatosPie(12) = .Tcona4_TKFPIELI11
                        DatosPie(13) = .Tcona4_TKFPIELI12
                        DatosPie(14) = .Tcona4_TKFPIELI13
                        DatosPie(15) = .Tcona4_TKFPIELI14
                        DatosPie(16) = .Tcona4_TKFPIELI15
                        DatosPie(17) = .Tcona4_TKFPIELI16
                        DatosPie(18) = .Tcona4_TKFPIELI17
                        DatosPie(19) = .Tcona4_TKFPIELI18
                        DatosPie(20) = .Tcona4_TKFPIELI19
                        DatosPie(21) = .Tcona4_TKFPIELI20
                    End With
                    '
                    For Each element As String In DatosPie
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                sw.WriteLine("<BR>")
                            End If
                            If element.Trim.Length > 0 Then
                                sw.WriteLine(element)
                                sw.WriteLine("<BR>")
                            End If
                        End If
                    Next
                    '
                    sw.Flush()
                    sw.Close()
                    '
                End Using
                '
                ' Llamada a COBVIEW pasandole como parámetro el informe generado...
                '
                Dim myProcess As New Process()
                Dim StringArguments As String
                Try
                    StringArguments = ""
                    myProcess.StartInfo.UseShellExecute = True
                    myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
                    StringArguments = "C:\TRIVAGES\InformesCobview\TKFACTURA.TXT"
                    myProcess.StartInfo.Arguments = StringArguments
                    myProcess.Start()
                    '
                    ' Si este proceso va bien, El ticket ha sido impreso...
                    '
                    Select Case iOPC
                        Case "N"
                            ActualizaDatosMESAC(MyFrm2.TextBoxNumMesa.Text.Trim, 2)
                        Case "S"
                            ActualizaDatosMESAC_Sep(MyFrm13.TextBoxSepNumMesa.Text.Trim, 2)
                    End Select
                    '
                Catch Mye As Exception
                    MsgBox("ERROR: " & Mye.Source & vbCrLf & Mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: COBVIEW.")
                End Try
            Case "True"
                '
                ' *** IMPRESION DIRECTA A PUERTO de CAPTURA ***
                '
                ' Comprobar que HAY UN PUERTO definido.
                '
                If wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim.Length = 0 Then
                    title = "¿Puerto de Captura No Definido.?"
                    style = MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly
                    msg = "Por favor, compruebe el Puerto de Captura," & vbCrLf
                    msg = " para TICKETS FACTURA cuando es impresión directa." & vbCrLf
                    msg &= "NO se imprimirá el TICKET." & vbCrLf
                    response = MsgBox(msg, style, title)
                    Exit Sub
                End If
                '
                ' Comprobar, Modelo esta creado en IMPRESORAS?
                '
                If LeeDatosImpresora(wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim) = False Then
                    '
                    ' El MODELO de Impresora no esta creado en la Tabla
                    ' IMPRESORAS, y damos un Aviso
                    '
                    msg = "El MODELO de Impresora " & vbCrLf
                    msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                    msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
                    style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                    title = "Modelo de impresora no Creado."
                    MsgBox(msg, style, title)
                    Exit Sub
                End If
                '
                ' Comprobación del estado de la impresora...
                ' NOTA: Ya se realiza la coprobación antes de llegar a este punto.
                '
                Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
                fh = CType(Win32API.CreateFile(wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                           Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
                Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
                FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
                SW = New StreamWriter(FS)
                '
                ' *** TICKET ***
                '
                Try
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    '
                    ' Líneas Cabecera en Ref. Generales
                    '
                    With wrLeeTCONA4
                        DatosCab(0) = .Tcona4_TKFCABLI1
                        DatosCab(1) = .Tcona4_TKFCABLI2
                        DatosCab(2) = .Tcona4_TKFCABLI3
                        DatosCab(3) = .Tcona4_TKFCABLI4
                        DatosCab(4) = .Tcona4_TKFCABLI5
                        DatosCab(5) = .Tcona4_TKFCABLI6
                        DatosCab(6) = .Tcona4_TKFCABLI7
                        DatosCab(7) = .Tcona4_TKFCABLI8
                        DatosCab(8) = .Tcona4_TKFCABLI9
                        DatosCab(9) = .Tcona4_TKFCABLI10
                    End With
                    '
                    For Each element As String In DatosCab
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                            End If
                            If element.Trim.Length > 0 Then
                                SW.WriteLine(element)
                            End If
                        End If
                    Next
                    '
                    SW.WriteLine(wrIMPRESORA.DOBLEANCHO)
                    '
                    Select Case iOPC
                        Case "N"
                            SW.WriteLine("MESA.: " & MyFrm2.TextBoxNumMesa.Text.Trim)
                        Case "S"
                            SW.WriteLine("MESA.: " & MyFrm13.TextBoxSepNumMesa1.Text.Trim)
                    End Select
                    '
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    SW.WriteLine(MyFrm6.TextBoxDetCab1.Text)
                    SW.WriteLine(MyFrm6.TextBoxDetCab2.Text)
                    SW.WriteLine(MyFrm6.TextBoxDetCab3.Text)
                    '
                    ' Líneas GRID
                    '
                    Select Case iOPC
                        Case "N"
                            For counter = 0 To (MyFrm2.GRID1.Rows.Count - 1)
                                '
                                ' <COL #1> ... <COL #n>
                                ' ----- -------- -------
                                ' UNID. ARTICULO IMPORTE
                                '
                                MiCadenaWIEW = ""
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")).ToString(fmtUnid).Replace(",", ".").PadLeft(9, CChar(" "))
                                MiCadenaWIEW = MiCadenaWIEW & " "
                                MiCadenaWIEW = MiCadenaWIEW & MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString.PadRight(22, CChar(" "))
                                MiCadenaWIEW = MiCadenaWIEW & " "
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm2.GRID1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".").PadLeft(8, CChar(" "))
                                '
                                SW.WriteLine(MiCadenaWIEW)
                                '
                                ' *** Combinados *** Si Hay y Si esta marcado en Ref. Generales
                                '
                                If wrLeeTCONA4.Tcona4_TKFACIMPDETCOMBI = "True" Then
                                    If MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Length > 0 Then
                                        Dim words As String() = MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                                        For i As Integer = 0 To words.Length - 1
                                            If LeeMar(words(i)) = False Then
                                                wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                            Else
                                                MiCadenaWIEW = "       "
                                                MiCadenaWIEW = MiCadenaWIEW & " "
                                                MiCadenaWIEW = MiCadenaWIEW & "  [+]"
                                                MiCadenaWIEW = MiCadenaWIEW & " "
                                                MiCadenaWIEW = MiCadenaWIEW & wrLeeMAR.Mar_DESCRIPCION.Trim
                                                SW.WriteLine(MiCadenaWIEW)
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        Case "S"
                            For counter = 0 To (MyFrm13.GRID1SepaMesa1.Rows.Count - 1)
                                '
                                ' <COL #1> ... <COL #n>
                                ' ----- -------- -------
                                ' UNID. ARTICULO IMPORTE
                                '
                                MiCadenaWIEW = ""
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(1).Value.ToString.Replace(".", ",")).ToString(fmtUnid).Replace(",", ".").PadLeft(9, CChar(" "))
                                MiCadenaWIEW = MiCadenaWIEW & " "
                                MiCadenaWIEW = MiCadenaWIEW & MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(2).Value.ToString.PadRight(22, CChar(" "))
                                MiCadenaWIEW = MiCadenaWIEW & " "
                                MiCadenaWIEW = MiCadenaWIEW & CDec(MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(4).Value.ToString.Replace(".", ",")).ToString(fmtImporte).Replace(",", ".").PadLeft(8, CChar(" "))
                                '
                                SW.WriteLine(MiCadenaWIEW)
                                '
                                ' *** Combinados *** Si Hay y Si esta marcado en Ref. Generales
                                '
                                If wrLeeTCONA4.Tcona4_TKFACIMPDETCOMBI = "True" Then
                                    If MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(6).Value.ToString().Trim.Length > 0 Then
                                        Dim words As String() = MyFrm13.GRID1SepaMesa1.Rows(counter).Cells(6).Value.ToString().Trim.Split(New Char() {"/"c})
                                        For i As Integer = 0 To words.Length - 1
                                            If LeeMar(words(i)) = False Then
                                                wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                            Else
                                                MiCadenaWIEW = "       "
                                                MiCadenaWIEW = MiCadenaWIEW & "  [+]"
                                                MiCadenaWIEW = MiCadenaWIEW & " "
                                                MiCadenaWIEW = MiCadenaWIEW & wrLeeMAR.Mar_DESCRIPCION.Trim
                                                SW.WriteLine(MiCadenaWIEW)
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                    End Select
                    '
                    ' Linea de Totales // IGIC
                    '
                    '  Base   %   IGIC
                    '-------------------
                    '999.99 99.99 999.99
                    '
                    '                          TOTAL: 9999.99
                    '                          --------------
                    SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    SW.WriteLine(" Base       %     IGIC   ")
                    SW.WriteLine("------------------------ ")
                    '
                    Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
                    Dim Micalculo As Double = (MiPorIGIC / 100) + 1
                    Dim MiBase As Double = Math.Round((MiTotal / Micalculo), 2)
                    Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
                    '
                    ' Números con formato Ajustados a la DERECHA y Tamaño Fijo.
                    '
                    MiCadenaWIEW = ""
                    MiCadenaWIEW &= MiBase.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiPorIGIC.ToString(fmtUnid).Replace(",", ".").PadLeft(7, CChar(" "))
                    MiCadenaWIEW &= " "
                    MiCadenaWIEW &= MiImpIGIC.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    SW.WriteLine(MiCadenaWIEW)
                    '
                    ' Total
                    '
                    SW.WriteLine(wrIMPRESORA.DOBLEALTO)
                    MiCadenaWIEW = "                          Total: " & MiTotal.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" ")) & ""
                    SW.WriteLine(MiCadenaWIEW)
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    '
                    ' Líneas Pie Fijas
                    '
                    Select Case iOPC
                        Case "N"
                            DatosPie(0) = "FACTURA SIMPLIFICADA  -  Num: " & MyFrm2.TextBoxFactura.Text.Trim
                            DatosPie(1) = "Ven: " & MyFrm2.TextBoxCamarero.Text.Trim &
                          "       Fecha: " & FechaTK & " " & HoraTK
                        Case "S"
                            DatosPie(0) = "FACTURA SIMPLIFICADA  -  Num: " & MyFrm13.TextBoxSepFactura.Text.Trim
                            DatosPie(1) = "Ven: " & MyFrm13.TextBoxSepCamarero.Text.Trim &
                          "       Fecha: " & FechaTK & " " & HoraTK
                    End Select
                    '
                    ' Líneas Pie en Ref. Generales.
                    '
                    With wrLeeTCONA4
                        DatosPie(2) = .Tcona4_TKFPIELI1
                        DatosPie(3) = .Tcona4_TKFPIELI2
                        DatosPie(4) = .Tcona4_TKFPIELI3
                        DatosPie(5) = .Tcona4_TKFPIELI4
                        DatosPie(6) = .Tcona4_TKFPIELI5
                        DatosPie(7) = .Tcona4_TKFPIELI6
                        DatosPie(8) = .Tcona4_TKFPIELI7
                        DatosPie(9) = .Tcona4_TKFPIELI8
                        DatosPie(10) = .Tcona4_TKFPIELI9
                        DatosPie(11) = .Tcona4_TKFPIELI10
                        DatosPie(12) = .Tcona4_TKFPIELI11
                        DatosPie(13) = .Tcona4_TKFPIELI12
                        DatosPie(14) = .Tcona4_TKFPIELI13
                        DatosPie(15) = .Tcona4_TKFPIELI14
                        DatosPie(16) = .Tcona4_TKFPIELI15
                        DatosPie(17) = .Tcona4_TKFPIELI16
                        DatosPie(18) = .Tcona4_TKFPIELI17
                        DatosPie(19) = .Tcona4_TKFPIELI18
                        DatosPie(20) = .Tcona4_TKFPIELI19
                        DatosPie(21) = .Tcona4_TKFPIELI20
                    End With
                    '
                    For Each element As String In DatosPie
                        If Not String.IsNullOrEmpty(element) Then
                            If element = " " Then
                                SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                            End If
                            If element.Trim.Length > 0 Then
                                SW.WriteLine(element)
                            End If
                        End If
                    Next
                    '
                    ' Lineas Finales
                    '
                    ' Lineas <= 9
                    ' Por si se diera el caso, no derrochar MUCHO Papel ...
                    '
                    If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                            wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                        End If
                        For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                            SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                        Next
                    Else
                        SW.WriteLine(wrIMPRESORA.AVAZALINEA) ' Por defecto al menos 1 Linea de salto
                    End If
                    '
                    ' Corte PAPEL
                    '
                    SW.WriteLine(wrIMPRESORA.CORTE)
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                        MsgBoxStyle.Exclamation Or
                                                        MsgBoxStyle.OkOnly,
                                                        "TICKET FACTURA.: Error de Impresora.")
                End Try
                FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
                '
        End Select
        '
    End Sub

    Public Sub ImprimeX_Z_Directa(ImpXZOPC As String)
        '
        ' Procediemiento Para la Impresion de X, Z
        '   Impresión Directa, Puerto Captura !!!
        '
        Dim DatosCab(20) As String '  Lineas CABECERA
        Dim MiCadenaWIEW As String = ""
        Dim FechaTK As String = Date.Now.ToShortDateString
        Dim HoraTK As String = TimeOfDay.ToLongTimeString.ToString
        '
        Dim TmpUnid As Decimal = 0 : Dim TmpImporte As Decimal = 0
        Dim TmpTOTImporte As Decimal = 0
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim queryString As String = ""
        '
        ' Comprobar que HAY UN PUERTO definido.
        '
        LeeTCONA4Cfg("General")
        If wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim.Length = 0 Then
            title = "¿Puerto de Captura No Definido.?"
            style = MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly
            msg = "Por favor, compruebe el Puerto de Captura," & vbCrLf
            msg = " para TICKETS FACTURA cuando es impresión directa." & vbCrLf
            msg &= "NO se imprimirá el TICKET." & vbCrLf
            response = MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        ' Comprobar, Modelo esta creado en IMPRESORAS?
        '
        If LeeDatosImpresora(wrLeeTCONA4.Tcona4_MODIMPREFIJO.Trim) = False Then
            '
            ' El MODELO de Impresora no esta creado en la Tabla
            ' IMPRESORAS, y damos un Aviso
            '
            msg = "El MODELO de Impresora " & vbCrLf
            msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
            msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
            style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
            title = "Modelo de impresora no Creado."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        ' Comprobación del estado de la impresora...
        '
        Dim MiImpDef As String = ObtenerImpresoraPredeterminada()
        If ImpresoraEstaONLINE(MiImpDef) = True Then
            If wrProp_IMPRESORA.JobCountSinceLastReset <> "0" Then
                msg = "Atención, la impresora " & vbCrLf
                msg &= MiImpDef & vbCrLf
                msg &= "informa " & wrProp_IMPRESORA.JobCountSinceLastReset
                msg &= " trabajos pendientes."
                style = MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly
                title = "Por favor compruebe la impresora."
                MsgBox(msg, style, title)
                Exit Sub
            End If
        Else
            msg = "Atención, la impresora " & vbCrLf
            msg &= MiImpDef & vbCrLf
            msg &= "parece estar apagada." & vbCrLf & vbCrLf
            msg &= "Informa " & wrProp_IMPRESORA.JobCountSinceLastReset
            msg &= " trabajos pendientes."
            style = MsgBoxStyle.Information Or
                    MsgBoxStyle.OkOnly
            title = "Por favor compruebe la impresora."
            MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
        fh = CType(Win32API.CreateFile(wrLeeTCONA4.Tcona4_TKFACPUERTO.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                           Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
        Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
        FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
        '
        SW = New StreamWriter(FS)
        Using SW
            '
            SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
            '
            ' Líneas Cabecera en Ref. Generales
            '
            With wrLeeTCONA4
                DatosCab(0) = .Tcona4_TKFCABLI1
                DatosCab(1) = .Tcona4_TKFCABLI2
                DatosCab(2) = .Tcona4_TKFCABLI3
                DatosCab(3) = .Tcona4_TKFCABLI4
                DatosCab(4) = .Tcona4_TKFCABLI5
                DatosCab(5) = .Tcona4_TKFCABLI6
                DatosCab(6) = .Tcona4_TKFCABLI7
            End With
            '
            For Each element As String In DatosCab
                If Not String.IsNullOrEmpty(element) Then
                    If element = " " Then
                        SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    End If
                    If element.Trim.Length > 0 Then
                        SW.WriteLine(element)
                    End If
                End If
            Next
            SW.WriteLine(wrIMPRESORA.AVAZALINEA)
            '
            ' X, Z Num.
            '
            SW.WriteLine(wrIMPRESORA.DOBLEALTO)
            Select Case ImpXZOPC
                Case "X"
                    SW.WriteLine("X.: " & wrLeeTCONA4.Tcona4_NUMX & "")
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    '
                    ' X, Fecha de la máquina, Fecha del dia
                    '
                    SW.WriteLine(Date.Now.ToShortDateString & " " & Date.Now.ToShortTimeString)
                Case "Z"
                    '
                    ' Establecemos Fecha Z = Fecha Dia
                    '
                    Actualiza_TCONA4(wCaja, "FecZ")
                    '
                    RetenNumZeta = wrLeeTCONA4.Tcona4_NUMZ
                    SW.WriteLine("Z.: " & wrLeeTCONA4.Tcona4_NUMZ & "")
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    '
                    ' Z, Fecha Z=Fecha Dia, Inico sesion
                    '
                    RetenFecZeta = wrLeeTCONA4.Tcona4_FECHADIASESION.Trim

                    SW.WriteLine(wrLeeTCONA4.Tcona4_FECHADIASESION.Trim & " " & Date.Now.ToShortTimeString)
            End Select
            '
            SW.WriteLine(MyFrm6.TextBoxDetCab4.Text)
            SW.WriteLine(MyFrm6.TextBoxDetCab5.Text)
            SW.WriteLine(MyFrm6.TextBoxDetCab6.Text)
            '
            queryString = "SELECT * FROM [ZZTablaXZTemp] "
            queryString &= "WHERE [ZZTablaXZTemp].[Numcaja]=" & wCaja & " "
            queryString &= "ORDER BY "
            queryString &= "[ZZTablaXZTemp].[NombreFamilia] ASC"
            '
            Dim dt As DataSet = New DataSet
            Try
                TblTPVS = New SqlDataAdapter(queryString, conexion)
                TblTPVS.Fill(dt, "ZZTablaXZTemp")
                '
                If dt.Tables("ZZTablaXZTemp").Rows.Count > 0 Then
                    Dim pRow As DataRow
                    For Each pRow In dt.Tables("ZZTablaXZTemp").Rows
                        '
                        TmpUnid = CDec(pRow("TotUnid").ToString().Trim.Replace(".", ","))
                        TmpImporte = CDec(pRow("TotImpo").ToString().Trim.Replace(".", ","))
                        TmpTOTImporte += TmpImporte
                        '
                        If pRow("CodigoFamilia").ToString().Trim.Length > 0 Then
                            MiCadenaWIEW = ""
                            MiCadenaWIEW = MiCadenaWIEW & pRow("NombreFamilia").ToString().Trim.PadRight(21, CChar(" "))
                            MiCadenaWIEW = MiCadenaWIEW & " "
                            MiCadenaWIEW = MiCadenaWIEW & TmpUnid.ToString(fmtUnid).Replace(",", ".").PadLeft(9, CChar(" "))
                            MiCadenaWIEW = MiCadenaWIEW & " "
                            MiCadenaWIEW = MiCadenaWIEW & TmpImporte.ToString(fmtImporte).Replace(",", ".").PadLeft(9, CChar(" "))
                            SW.WriteLine(MiCadenaWIEW)
                        End If
                    Next
                End If
                SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                SW.WriteLine(" Base       %     IGIC  ")
                SW.WriteLine("------------------------")
                '
                Dim Mitotal As Double = CDbl(TmpTOTImporte.ToString.Replace(".", ",").Trim)
                Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
                Dim Micalculo As Double = (MiPorIGIC / 100) + 1
                Dim MiBase As Double = Math.Round((Mitotal / Micalculo), 2)
                Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
                '
                ' Números con formato Ajustados a la DERECHA y Tamaño Fijo.
                '
                MiCadenaWIEW = ""
                MiCadenaWIEW &= MiBase.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                MiCadenaWIEW &= " "
                MiCadenaWIEW &= MiPorIGIC.ToString(fmtUnid).Replace(",", ".").PadLeft(7, CChar(" "))
                MiCadenaWIEW &= " "
                MiCadenaWIEW &= MiImpIGIC.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                SW.WriteLine(MiCadenaWIEW)
                '
                ' TOTAL IMPORTE
                '
                SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                SW.WriteLine(wrIMPRESORA.DOBLEANCHO)
                MiCadenaWIEW = ""
                MiCadenaWIEW = MiCadenaWIEW & "      TOTAL-> "
                MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                SW.WriteLine(MiCadenaWIEW)
                '
                ' Resumen Totales Segun Forma de Pago
                '
                With wrRESUMENXZ
                    SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL EFECTIVO -> "
                    MiCadenaWIEW = MiCadenaWIEW & .EFECTIVO.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    SW.WriteLine(MiCadenaWIEW)
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL TARJETA  -> "
                    MiCadenaWIEW = MiCadenaWIEW & .TARJETA.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    SW.WriteLine(MiCadenaWIEW)
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL CHEQUES  -> "
                    MiCadenaWIEW = MiCadenaWIEW & .CHEQUES.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    SW.WriteLine(MiCadenaWIEW)
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL VALE DTO.-> "
                    MiCadenaWIEW = MiCadenaWIEW & .VALEDTO.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    SW.WriteLine(MiCadenaWIEW)
                End With
                '
                ' TOTAL MESAS ABIERTAS
                '
                SW.WriteLine(wrIMPRESORA.PROPORCIONAL)
                MiCadenaWIEW = ""
                MiCadenaWIEW = MiCadenaWIEW & "                 MESAS ABIERTAS-> "
                MiCadenaWIEW = MiCadenaWIEW & ImporteMesasAbiertas.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                SW.WriteLine(MiCadenaWIEW)
                '
                ' Lineas Finales
                ' Lineas <= 9
                ' Por si se diera el caso, no derrochar MUCHO Papel ...
                '
                If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                    If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                        wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                    End If
                    For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                        SW.WriteLine(wrIMPRESORA.AVAZALINEA)
                    Next
                Else
                    SW.WriteLine(wrIMPRESORA.AVAZALINEA) ' Por defecto al menos 1 Linea de salto
                End If
                '
                ' Corte PAPEL
                '
                SW.WriteLine(wrIMPRESORA.CORTE)
                '
            Catch ex As Exception
                MsgBox("ERROR:  " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla LISTAR [ZZTablaXZTemp]")
            End Try
            '
            conexion.Close()
            dt.Dispose()
            conexion.Dispose()
            '
            FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
            '
        End Using
        '
    End Sub
    Public Sub ImprimeX_Z(ImpXZOPC As String)
        '
        ' Procediemiento Para la Impresion de X, Z
        '   Windows : Lanzamos informe a COBVIEW !!!
        '
        Dim DatosCab(20) As String '  Lineas CABECERA
        Dim MiCadenaWIEW As String = ""
        Dim FechaTK As String = Date.Now.ToShortDateString
        Dim HoraTK As String = TimeOfDay.ToLongTimeString.ToString
        Dim myProcess As New Process()
        Dim StringArguments As String
        '
        Dim TmpUnid As Decimal = 0 : Dim TmpImporte As Decimal = 0
        Dim TmpTOTImporte As Decimal = 0
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim queryString As String = ""
        '
        Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\TKZ.TXT", True, System.Text.Encoding.Default)
            '
            ' Logo SI/NO depende de Ref. Generales
            '
            If LeeTCONA4Cfg("General") = True Then
                If wrLeeTCONA4.Tcona4_TKZETALOGO = "True" Then
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKZL.def'>")
                Else
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKZ.def'>")
                End If
            Else
                '
                ' Si no lograse leer el Ref. Genrales...
                '
                sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKZ.def'>")
            End If
            '
            ' Previsualizacion en COBVIEW, ahora determinado por Ref. Generales
            '
            If wrLeeTCONA4.Tcona4_COBVIEWPDSN = "True" Then
                sw.WriteLine("<SET PRINTDIRECT='NO'>")
            Else
                sw.WriteLine("<SET PRINTDIRECT='YES'>")
            End If
            '
            sw.WriteLine("<font face='Consolas' size='09'>")
            '
            ' Líneas Cabecera en Ref. Generales
            '
            With wrLeeTCONA4
                DatosCab(0) = .Tcona4_TKFCABLI1
                DatosCab(1) = .Tcona4_TKFCABLI2
                DatosCab(2) = .Tcona4_TKFCABLI3
                DatosCab(3) = .Tcona4_TKFCABLI4
                DatosCab(4) = .Tcona4_TKFCABLI5
                DatosCab(5) = .Tcona4_TKFCABLI6
                DatosCab(6) = .Tcona4_TKFCABLI7
            End With
            '
            For Each element As String In DatosCab
                If Not String.IsNullOrEmpty(element) Then
                    If element = " " Then
                        sw.WriteLine("<BR>")
                    End If
                    If element.Trim.Length > 0 Then
                        sw.WriteLine(element)
                        sw.WriteLine("<BR>")
                    End If
                End If
            Next
            sw.WriteLine("<BR>")
            '
            ' X, Z Num.
            '
            sw.WriteLine("<font face='Consolas' size='20'>")
            Select Case ImpXZOPC
                Case "X"
                    sw.WriteLine("<b>X.: " & wrLeeTCONA4.Tcona4_NUMX & "</b>")
                    sw.WriteLine("<font face='Consolas' size='09'>")
                    sw.WriteLine("<set Y='+0.1'>")
                    '
                    ' X, Fecha de la máquina, Fecha del dia
                    '
                    sw.WriteLine("      " & Date.Now.ToShortDateString & " " & Date.Now.ToShortTimeString)
                    sw.WriteLine("<BR><BR>")
                Case "Z"
                    '
                    ' Establecemos Fecha Z = Fecha Dia
                    '
                    Actualiza_TCONA4(wCaja, "FecZ")
                    '
                    RetenNumZeta = wrLeeTCONA4.Tcona4_NUMZ
                    sw.WriteLine("<b>Z.: " & wrLeeTCONA4.Tcona4_NUMZ & "</b>")
                    sw.WriteLine("<font face='Consolas' size='09'>")
                    sw.WriteLine("<set Y='+0.1'>")
                    '
                    ' Z, Fecha Z=Fecha Dia, Inico sesion
                    '
                    RetenFecZeta = wrLeeTCONA4.Tcona4_FECHADIASESION.Trim

                    sw.WriteLine("      " & wrLeeTCONA4.Tcona4_FECHADIASESION.Trim & " " & Date.Now.ToShortTimeString)
                    sw.WriteLine("<BR><BR>")
            End Select
            '
            sw.WriteLine(MyFrm6.TextBoxDetCab4.Text & "<BR>")
            sw.WriteLine(MyFrm6.TextBoxDetCab5.Text & "<BR>")
            sw.WriteLine(MyFrm6.TextBoxDetCab6.Text & "<BR>")
            '
            queryString = "SELECT * FROM [ZZTablaXZTemp] "
            queryString &= "WHERE [ZZTablaXZTemp].[Numcaja]=" & wCaja & " "
            queryString &= "ORDER BY "
            queryString &= "[ZZTablaXZTemp].[NombreFamilia] ASC"
            '
            Dim dt As DataSet = New DataSet
            Try
                TblTPVS = New SqlDataAdapter(queryString, conexion)
                TblTPVS.Fill(dt, "ZZTablaXZTemp")
                '
                If dt.Tables("ZZTablaXZTemp").Rows.Count > 0 Then
                    Dim pRow As DataRow
                    For Each pRow In dt.Tables("ZZTablaXZTemp").Rows
                        '
                        TmpUnid = CDec(pRow("TotUnid").ToString().Trim.Replace(".", ","))
                        TmpImporte = CDec(pRow("TotImpo").ToString().Trim.Replace(".", ","))
                        TmpTOTImporte += TmpImporte
                        '
                        If pRow("CodigoFamilia").ToString().Trim.Length > 0 Then
                            MiCadenaWIEW = "<COL #1>"
                            MiCadenaWIEW = MiCadenaWIEW & pRow("NombreFamilia").ToString().Trim
                            MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                            MiCadenaWIEW = MiCadenaWIEW & TmpUnid.ToString(fmtUnid).Replace(",", ".")
                            MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                            MiCadenaWIEW = MiCadenaWIEW & TmpImporte.ToString(fmtImporte).Replace(",", ".")
                            sw.WriteLine(MiCadenaWIEW)
                        End If
                    Next
                End If
                sw.WriteLine("<BR>")
                sw.WriteLine(" Base       %     IGIC   <BR>")
                sw.WriteLine("------------------------ <BR>")
                '
                Dim Mitotal As Double = CDbl(TmpTOTImporte.ToString.Replace(".", ",").Trim)
                Dim MiPorIGIC As Double = CDbl(wrLeeTCONA4.Tcona4_TKFACIGIC.Replace(".", ",").Trim)
                Dim Micalculo As Double = (MiPorIGIC / 100) + 1
                Dim MiBase As Double = Math.Round((Mitotal / Micalculo), 2)
                Dim MiImpIGIC As Double = Math.Round(((MiBase * MiPorIGIC) / 100), 2)
                '
                ' Números con formato Ajustados a la DERECHA y Tamaño Fijo.
                '
                MiCadenaWIEW = ""
                MiCadenaWIEW &= MiBase.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                MiCadenaWIEW &= " "
                MiCadenaWIEW &= MiPorIGIC.ToString(fmtUnid).Replace(",", ".").PadLeft(7, CChar(" "))
                MiCadenaWIEW &= " "
                MiCadenaWIEW &= MiImpIGIC.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                sw.WriteLine(MiCadenaWIEW)
                '
                ' TOTAL IMPORTE
                '
                sw.WriteLine("<font face='Consolas' size='15'>")
                sw.WriteLine("<BR><B>")
                MiCadenaWIEW = ""
                MiCadenaWIEW = MiCadenaWIEW & "         TOTAL-> "
                MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                sw.WriteLine(MiCadenaWIEW)
                sw.WriteLine("</B>")
                '
                ' Resumen Totales Segun Forma de Pago
                '
                With wrRESUMENXZ
                    sw.WriteLine("<font face='Consolas' size='9'>")
                    sw.WriteLine("<BR><BR><BR><BR><B>")
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL EFECTIVO -> "
                    MiCadenaWIEW = MiCadenaWIEW & .EFECTIVO.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("<BR>")
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL TARJETA  -> "
                    MiCadenaWIEW = MiCadenaWIEW & .TARJETA.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("<BR>")
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL CHEQUES  -> "
                    MiCadenaWIEW = MiCadenaWIEW & .CHEQUES.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("<BR>")
                    MiCadenaWIEW = ""
                    MiCadenaWIEW = MiCadenaWIEW & "                TOTAL VALE DTO.-> "
                    MiCadenaWIEW = MiCadenaWIEW & .VALEDTO.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("</B>")
                End With
                '
                ' TOTAL MESAS ABIERTAS
                '
                sw.WriteLine("<font face='Consolas' size='09'>")
                sw.WriteLine("<BR><BR><BR><B>")
                MiCadenaWIEW = ""
                MiCadenaWIEW = MiCadenaWIEW & "                 MESAS ABIERTAS-> "
                MiCadenaWIEW = MiCadenaWIEW & ImporteMesasAbiertas.ToString(fmtImporte).Replace(",", ".").PadLeft(7, CChar(" "))
                sw.WriteLine(MiCadenaWIEW)
                sw.WriteLine("</B>")
                '
                StringArguments = ""
                myProcess.StartInfo.UseShellExecute = True
                myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
                StringArguments = "C:\TRIVAGES\InformesCobview\TKZ.TXT"
                myProcess.StartInfo.Arguments = StringArguments
                myProcess.Start()
                '
            Catch ex As Exception
                MsgBox("ERROR:  " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla LISTAR [ZZTablaXZTemp]")
            End Try
            '
            conexion.Close()
            dt.Dispose()
            conexion.Dispose()
            sw.Flush()
            sw.Close()
            '
        End Using
        '
    End Sub

    Public Function ImporteMesasAbiertas() As Double
        '
        ' Nos devuelve el TOTAL IMPORTE de las MESAS OCUPADAS
        ' Caja Actual
        '
        ImporteMesasAbiertas = 0
        '
        Dim conexion As New SqlConnection
        '
        Dim queryString As String = ""
        queryString &= "SELECT * FROM [SALA1] "
        queryString &= "WHERE [SALA1].[CAJA] = " & wCaja & " "
        queryString &= "AND [SALA1].[FACTURA] > '" & 0 & "' "
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "SALA1")
            If dt.Tables("SALA1").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("SALA1").Rows
                    ImporteMesasAbiertas += ImporteRegistrosMesasAbiertas(CInt(pRow("FACTURA").ToString))
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Calc. Importe OCUPADAS, Comprobar Tabla [SALA1]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function ImporteRegistrosMesasAbiertas(wMiFactura As Integer) As Double
        '
        ' Nos devuelve el TOTAL IMPORTE de las MESAS OCUPADAS
        ' Caja Actual
        '
        ImporteRegistrosMesasAbiertas = 0
        '
        Dim conexion As New SqlConnection
        Dim queryString As String = ""
        queryString &= "SELECT * FROM [MESA] "
        queryString &= "WHERE [MESA].[NUMCAJA] = " & wCaja & " "
        queryString &= "AND [MESA].[FACTURA] = " & wMiFactura & " "
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESA")
            '
            If dt.Tables("MESA").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MESA").Rows
                    ImporteRegistrosMesasAbiertas += CDbl(pRow("IMPORTE").ToString())
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Calc. Importe OCUPADAS, Comprobar Tabla [MESA]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Sub CargaArticulos(wListaCargar As Integer)
        '
        ' Carga una lista con los Articulos.
        ' wListaCargar
        '   0=LiBRE ... ( Otros Usos )
        '   1=Lista Articulos CONSULTA GENERAL.
        '   2=Lista Articulos CONSULTA GENERAL (Filtrada DESCRIPCION).
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [MAR] "
        '
        ' Filtro Nombre
        '
        If wListaCargar = 2 Then
            queryString = queryString & "WHERE [MAR].[DESCRIPCION] LIKE '" & MyFrm11.TextBoxLocalizaNombre.Text.Trim & "%' "
            'queryString = queryString & "WHERE [MAR].[DESCRIPCION] LIKE '%" & MyFrm11.TextBoxLocalizaNombre.Text.Trim & "%'"
        End If
        '
        queryString = queryString & "ORDER BY [MAR].[DESCRIPCION] "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MAR")
            '
            MyFrm11.GRIDLISTAS.Visible = False
            MyFrm11.GRIDLISTAS.Rows.Clear()
            '
            If dt.Tables("MAR").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MAR").Rows
                    Select Case wListaCargar
                        Case 1, 2
                            MyFrm11.GRIDLISTAS.Rows.Add(pRow("NARTICULO").ToString(), pRow("DESCRIPCION").ToString())
                    End Select
                Next
            End If
            '
            MyFrm11.GRIDLISTAS.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [MAR]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub PedirVendedor()
        '
        ' En este procedimiento gestionamos la apertura del FORM para
        '   pedir el VENDEDOR. Depende de la variable de Ref. Generales.
        ' Se hace Público, ya que se llama desde varios punto del código y
        '    facilitará implementar funcionalidad respecto a vendedores.
        '
        MyFrm9.ShowDialog(MyFrm1)
        '
    End Sub

    Public Sub CargaGRIDTarifas()
        '
        ' Cargamos las tarifas en, MyFrm2.GRIDLISTAVend. (Se reutiliza).
        ' Las TARIFAS estan precargadas en MyFrm2.ComboBoxTarifas.
        '
        With MyFrm2.GRIDLISTAVend
            .Visible = False
            .Rows.Clear()
        End With
        With MyFrm2.ComboBoxTarifas
            For i As Integer = 0 To .Items.Count - 1
                MyFrm2.GRIDLISTAVend.Rows.Add((i + 1).ToString, .Items(i).ToString)
            Next
        End With
        MyFrm2.GRIDLISTAVend.Visible = True
        '
    End Sub

    Public Sub CargaVendedores(wListaCargar As Integer)
        '
        ' Carga una lista con los vendedores.
        ' wListaCargar ::
        '   Para TCONA409, FORMULARIO de CREDENCIALES VENDEDORES:
        '     0=Lista Vendedores FORM credenciales VENDEDORES.
        '   Para TCONA4011, FORMULARIO de CONSULTA GENERAL:
        '     1=Lista Vendedores Filtrado NORMAL.
        '     2=Lista Vendedores Filtrado por NOMBRE.
        '   Para TCONA402:
        '     3=Lista Vendedores (Cambio Vendedor).
        '   Para TCONA420:
        '     4=Lista Vendedores (Nivel de Acceso, Claves).
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [VEN] "
        '
        ' Filtro Nombre
        '
        If wListaCargar = 2 Then
            queryString = queryString & "WHERE [VEN].[NOMBRE] LIKE '" & MyFrm11.TextBoxLocalizaNombre.Text.Trim & "%' "
        End If
        '
        queryString = queryString & "ORDER BY [VEN].[NOMBRE] "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "VEN")
            '
            With MyFrm2.GRIDLISTAVend
                .Visible = False
                .Rows.Clear()
            End With
            With MyFrm9.GRIDVENDEDORES
                .Visible = False
                .Rows.Clear()
            End With
            With MyFrm11.GRIDLISTAS
                .Visible = False
                .Rows.Clear()
            End With
            With MyFrm20.GRIDLV
                .Visible = False
                .Rows.Clear()
            End With
            '
            If dt.Tables("VEN").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("VEN").Rows
                    Select Case wListaCargar
                        Case 0
                            MyFrm9.GRIDVENDEDORES.Rows.Add(pRow("CODIGO").ToString(), pRow("NOMBRE").ToString(), pRow("CLAVE").ToString() & "")
                        Case 1, 2
                            MyFrm11.GRIDLISTAS.Rows.Add(pRow("CODIGO").ToString(), pRow("NOMBRE").ToString())
                        Case 3
                            MyFrm2.GRIDLISTAVend.Rows.Add(pRow("CODIGO").ToString(), pRow("NOMBRE").ToString())
                        Case 4
                            MyFrm20.GRIDLV.Rows.Add(pRow("CODIGO").ToString(), pRow("NOMBRE").ToString(), pRow("NIVELACCESO").ToString())
                    End Select
                Next
            End If
            '
            MyFrm2.GRIDLISTAVend.Visible = True
            MyFrm9.GRIDVENDEDORES.Visible = True
            MyFrm11.GRIDLISTAS.Visible = True
            MyFrm20.GRIDLV.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [VEN]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Function CargaListaCajas(Optional wCajaEvitar As Integer = 0) As String
        '
        ' Carga una lista con las Cajas Existentes.
        ' Esta Función tambien devuelve los Nros. de cajas credas, separadas por ",".
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        CargaListaCajas = ""
        '
        Dim queryString As String = "SELECT * FROM [TCONA4] "
        queryString = queryString & "ORDER BY [TCONA4].[CAJA] "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "TCONA4")
            '
            With MyFrm19
                .GRIDCAJAS.Visible = False
                .GRIDCAJAS.Rows.Clear()
            End With
            With MyFrm6
                .GRIDCAJASRef.Visible = False
                .GRIDCAJASRef.Rows.Clear()
            End With
            '
            If dt.Tables("TCONA4").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("TCONA4").Rows
                    MyFrm19.GRIDCAJAS.Rows.Add(pRow("CAJA").ToString(), pRow("NOMBRECAJA").ToString().Trim)
                    '
                    ' En ref. Generales Carga TODAS menos la ACTUAL.
                    '
                    If CInt(pRow("CAJA").ToString()) <> wCajaEvitar Then
                        MyFrm6.GRIDCAJASRef.Rows.Add(pRow("CAJA").ToString(), pRow("NOMBRECAJA").ToString().Trim)
                    End If
                    CargaListaCajas &= pRow("CAJA").ToString().Trim & ", "
                Next
            End If
            '
            MyFrm19.GRIDCAJAS.Visible = True
            MyFrm6.GRIDCAJASRef.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Listar CAJAS: Comprobar tabla [TCONA4]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function CargaListaClaves(wTopeNivel As Integer,
                                     Optional wOpt As Integer = 0) As String
        '
        ' Carga una lista con las Claves Existentes.
        '
        ' wTopeNivel : Nivel Máximo visible en la lista !!!
        '   Esto limitará la lista en funcion del NIVEL de acceso de la entidad.
        '   Si NO se desea limitacion pasamos un nivel alto, ej.: (999999999)
        '
        ' wOpt : Opcional, Normalmente = 0
        '  98, nos devuelve el ultimo NIVEL creado.
        '   0, 1 = Determina si la clave es visible o no (0=No/1=Si).
        '
        Dim conexion As New SqlConnection
        Dim queryString As String = ""
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        CargaListaClaves = ""
        '
        queryString = "SELECT * FROM [CLAVES] "
        queryString = queryString & "ORDER BY [CLAVES].[NIVELACCESO] "
        '
        ' Con 98 solo necesitamos recoger el ultimo NIVEL creado.
        '
        If wOpt = 98 Then
            queryString = "SELECT * FROM [CLAVES] "
            queryString = queryString & "ORDER BY [CLAVES].[NIVELACCESO] DESC "
        Else
            With MyFrm6.GRIDCLRF
                .Visible = False
                .Rows.Clear()
            End With
            With MyFrm20.GRIDCLRFM
                .Visible = False
                .Rows.Clear()
            End With
        End If
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "CLAVES")
            If dt.Tables("CLAVES").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("CLAVES").Rows
                    If CInt(pRow("NIVELACCESO").ToString()) <= wTopeNivel Then
                        '
                        ' Con 98 solo necesitamos recoger el ultimo NIVEL creado.
                        '
                        If wOpt = 98 Then
                            CargaListaClaves = pRow("NIVELACCESO").ToString()
                            Exit For
                        End If
                        If wOpt = 0 Then
                            MyFrm6.GRIDCLRF.Rows.Add(pRow("NIVELACCESO").ToString(), pRow("DESCRIPCION").ToString().Trim, "******")
                        Else
                            MyFrm6.GRIDCLRF.Rows.Add(pRow("NIVELACCESO").ToString(), pRow("DESCRIPCION").ToString().Trim, pRow("CLAVE").ToString().Trim)
                        End If
                        '
                        ' Mant. de Claves, independiente de wOpt
                        '
                        MyFrm20.GRIDCLRFM.Rows.Add(pRow("NIVELACCESO").ToString(),
                                                   pRow("DESCRIPCION").ToString().Trim,
                                                   pRow("CLAVE").ToString().Trim,
                                                   pRow("ACCESOX").ToString().Trim,
                                                   pRow("ACCESOZ").ToString().Trim,
                                                   pRow("ACCESOAPPS").ToString().Trim,
                                                   pRow("BOTONMENOS").ToString().Trim,
                                                   pRow("BOTONPRECIO").ToString().Trim,
                                                   pRow("BOTONTARIFA").ToString().Trim,
                                                   pRow("ACCESOREFGEN").ToString().Trim)
                    End If
                Next
            End If
            MyFrm6.GRIDCLRF.Visible = True
            MyFrm20.GRIDCLRFM.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Listar CAJAS: Comprobar tabla [CLAVES]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function CargaListaPedClie(wTelefono As String, Optional wOpt As Integer = 0) As String
        '
        ' Carga una lista con datos de clientes / pedidos a domicilio.
        ' wOpt : Opcional, Normalmente = 0
        '
        Dim conexion As New SqlConnection
        Dim queryString As String = ""
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        CargaListaPedClie = ""
        '
        queryString = "SELECT * FROM [PEDCLIE] WHERE "
        queryString &= "[PEDCLIE].[TELEFONO] LIKE '" & wTelefono & "%' "
        queryString &= queryString & "ORDER BY [PEDCLIE].[NOMBRE] "
        With MyFrm21.GRIDPEDCLI
            .Visible = False
            .Rows.Clear()
        End With
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "PEDCLIE")
            If dt.Tables("PEDCLIE").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("PEDCLIE").Rows
                    MyFrm21.GRIDPEDCLI.Rows.Add(pRow("TELEFONO").ToString(),
                                               pRow("NOMBRE").ToString().Trim,
                                               pRow("DIRECCION").ToString().Trim,
                                               pRow("CODPOSTAL").ToString().Trim,
                                               pRow("POBLACION").ToString().Trim,
                                               pRow("EMAIL").ToString().Trim)
                Next
            End If
            MyFrm21.GRIDPEDCLI.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Listar, Comprobar tabla [PEDCLIE]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function CargaListaClieMCO(wBuscarCliMCO As String,
                                      wOpt As Integer) As String
        '
        ' Carga una lista con datos de clientes CREDITO (MCO).
        ' wOpt : Opcional, Normalmente = 0
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim Separador As String = " / "
        '
        SQL_Catalogo1 = DameCatalogoEmpresa(wEmpresa, "CONTATRV")
        SQL_CadenaConexion1 = SQL_Instancia & SQL_Catalogo1 & SQL_Seguridad_Otros
        conexion.ConnectionString = SQL_CadenaConexion1
        conexion.Open()
        '
        CargaListaClieMCO = ""
        '
        Select Case wOpt
            Case 0
                queryString = "SELECT * FROM [MCO] WHERE "
                queryString &= "[MCO].[CUENTA] LIKE '" & wBuscarCliMCO & "%' "
                queryString &= queryString & "ORDER BY [MCO].[NOMBRE] "
            Case 1
                queryString = "SELECT * FROM [MCO] WHERE "
                queryString &= "[MCO].[NOMBRE] LIKE '" & wBuscarCliMCO & "%' "
                queryString &= queryString & "ORDER BY [MCO].[NOMBRE] "
        End Select
        '
        With MyFrm22.GRIDCLIFAC
            .Visible = False
            .Columns("CLcodigo").Visible = True
            .Rows.Clear()
        End With
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MCO")
            If dt.Tables("MCO").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MCO").Rows
                    '
                    ' Gestionamos el Separador " / " en los teléfonos
                    '
                    Separador = " / "
                    If IsDBNull(pRow("TELEFONO").ToString().Trim) And IsDBNull(pRow("TELEFONO2").ToString().Trim) Then
                        Separador = ""
                    Else
                        If pRow("TELEFONO").ToString.Trim.Length = 0 Or pRow("TELEFONO2").ToString.Trim.Length = 0 Then
                            Separador = ""
                        End If
                    End If
                    '
                    MyFrm22.GRIDCLIFAC.Rows.Add(pRow("CUENTA").ToString(),
                                                pRow("CIF").ToString().Trim,
                                                pRow("NOMBRE").ToString().Trim,
                                                pRow("DIRECCION").ToString().Trim,
                                                pRow("TELEFONO").ToString().Trim & Separador & pRow("TELEFONO2").ToString().Trim,
                                                pRow("CODPOSTAL").ToString().Trim,
                                                pRow("POBLACION").ToString().Trim,
                                                pRow("EMAIL").ToString().Trim)
                Next
            End If
            MyFrm22.GRIDCLIFAC.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Listar, Comprobar tabla [MCO]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function CargaListaClieCONTA(wBuscarClConta As String, wOpc As Integer) As String
        '
        ' Carga una lista con datos de clientes CONTADO (CLICONTA).
        ' wOpt : Opcional, Normalmente = 0
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim Separador As String = " / "
        '
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        CargaListaClieCONTA = ""
        '
        Select Case wOpc
            Case 0
                queryString = "SELECT * FROM [CLICONTA] WHERE "
                queryString &= "[CLICONTA].[NIFCIF] LIKE '" & wBuscarClConta & "%' "
                queryString &= queryString & "ORDER BY [CLICONTA].[NOMBRE] "
            Case 1
                queryString = "SELECT * FROM [CLICONTA] WHERE "
                queryString &= "[CLICONTA].[NOMBRE] LIKE '" & wBuscarClConta & "%' "
                queryString &= queryString & "ORDER BY [CLICONTA].[NOMBRE] "
        End Select
        '
        '
        With MyFrm22.GRIDCLIFAC
            .Visible = False
            .Columns("CLcodigo").Visible = False
            .Rows.Clear()
        End With
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "CLICONTA")
            If dt.Tables("CLICONTA").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("CLICONTA").Rows
                    '
                    ' Gestionamos el Separador " / " en los teléfonos
                    '
                    Separador = " / "
                    If IsDBNull(pRow("TELEFONO1").ToString().Trim) And IsDBNull(pRow("TELEFONO2").ToString().Trim) Then
                        Separador = ""
                    Else
                        If pRow("TELEFONO1").ToString.Trim.Length = 0 Or pRow("TELEFONO2").ToString.Trim.Length = 0 Then
                            Separador = ""
                        End If
                    End If
                    '
                    MyFrm22.GRIDCLIFAC.Rows.Add("--",
                                                pRow("NIFCIF").ToString().Trim,
                                                pRow("NOMBRE").ToString().Trim,
                                                pRow("DIRECCION").ToString().Trim,
                                                pRow("TELEFONO1").ToString().Trim & Separador & pRow("TELEFONO2").ToString().Trim,
                                                pRow("CODPOSTAL").ToString().Trim,
                                                pRow("POBLACION").ToString().Trim,
                                                pRow("EMAIL").ToString().Trim)
                Next
            End If
            MyFrm22.GRIDCLIFAC.Visible = True
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Listar, Comprobar tabla [CLICONTA]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LeeVendedor(wCodVen As Integer) As Boolean
        '
        ' Leer vendedor.
        '
        LeeVendedor = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = "SELECT * FROM [VEN] WHERE "
        queryString = queryString & "[VEN].[CODIGO] =" & wCodVen
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "VEN")
            If dt.Tables("VEN").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("VEN").Rows
                    If pRow("CODIGO").ToString() = wCodVen.ToString Then
                        LeeVendedor = True
                        With wrLeeCODNOM
                            .CODIGO = pRow("CODIGO").ToString().Trim
                            .NOMBRE = pRow("NOMBRE").ToString().Trim
                            '
                            If IsDBNull(pRow("nivelacceso")) Then
                                .NIVELACCESO = 0
                            Else
                                .NIVELACCESO = CInt(pRow("nivelacceso".ToString.Trim))
                            End If
                        End With
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR       " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar tabla [VEN]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Sub GeneraX_Z(XZOPC As String)
        '
        ' Generación X, Z, XZOPC = "X", "Z".
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        Dim queryString1 As String = ""
        Dim queryString2 As String = ""
        '
        Dim TmpUnid As Double = 0 : Dim TmpImporte As Double = 0
        '
        ' Borrado TOTAL de las filas de la Tabla [ZZTablaXZTemp]
        ' Esta Tabla es "Pseudo-Temporal", y se utiliza con el fin de generar la Z.
        ' Nota: Aqui he preferido evitar Tablas temporales reales tipo 
        '       #Nomtabla, ##Nomtabla por razones de programación.
        '
        queryString1 = " DELETE FROM [ZZTablaXZTemp] "
        queryString1 &= "WHERE "
        queryString1 &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja
        '
        cmd.CommandText = queryString1
        cmd.Connection = conexion
        cmd.ExecuteNonQuery()
        '
        queryString = "SELECT * FROM [MESA] WHERE "
        queryString = queryString & "[MESA].[NUMCAJA]=" & wCaja & " "
        '
        queryString2 = "SELECT * FROM [MESAC] WHERE "
        queryString2 = queryString2 & "[MESAC].[NUMCAJA]=" & wCaja & " "
        '
        Dim pRow As DataRow = Nothing
        '
        Dim dt As DataSet = New DataSet
        Dim dt1 As DataSet = New DataSet
        Try
            ' [MESA]
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESA")
            ' [MESAC]
            TblTPVS1 = New SqlDataAdapter(queryString2, conexion)
            TblTPVS1.Fill(dt1, "MESAC")
            '
            If dt.Tables("MESA").Rows.Count > 0 Then
                For Each pRow In dt.Tables("MESA").Rows
                    '
                    ' Acumulado Por Familias
                    '
                    TmpUnid = CDec(pRow("UNID").ToString.Trim.Replace(".", ","))
                    TmpImporte = CDec(pRow("IMPORTE").ToString.Trim.Replace(".", ","))
                    '
                    If LeeMar(pRow("ARTI").ToString().Trim) Then
                        If LeeTablaXZTemp(wrLeeMAR.Mar_FAMILIA.Trim) = True Then
                            '
                            ' Acumulamos Unid. // Importe
                            '
                            TmpUnid += CDec(wrACUMXZ.ACUMXZ_Unid.ToString.Trim.Replace(".", ","))
                            TmpImporte += CDec(wrACUMXZ.ACUMXZ_Importe.ToString.Trim.Replace(".", ","))
                            '
                            queryString1 = "UPDATE [ZZTablaXZTemp] SET "
                            queryString1 &= "[ZZTablaXZTemp].[TotUnid]='" & TmpUnid.ToString.Replace(",", ".") & "', "
                            queryString1 &= "[ZZTablaXZTemp].[TotImpo]='" & TmpImporte.ToString.Replace(",", ".") & "', "
                            queryString1 &= "[ZZTablaXZTemp].[Sala]=' ' "
                            queryString1 &= "WHERE "
                            queryString1 &= "[ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
                            queryString1 &= "[ZZTablaXZTemp].[CodigoFamilia]='" & wrLeeMAR.Mar_FAMILIA.Trim & "'"
                        Else
                            If LeeFam(wrLeeMAR.Mar_FAMILIA.Trim) = False Then
                                wrLeeCODNOM.NOMBRE = "** Fam. No Leida **"
                            End If
                            queryString1 = "INSERT INTO [ZZTablaXZTemp] ("
                            queryString1 &= "[ZZTablaXZTemp].[NumCaja],"
                            queryString1 &= "[ZZTablaXZTemp].[CodigoFamilia],"
                            queryString1 &= "[ZZTablaXZTemp].[NombreFamilia],"
                            queryString1 &= "[ZZTablaXZTemp].[TotUnid],"
                            queryString1 &= "[ZZTablaXZTemp].[TotImpo],"
                            queryString1 &= "[ZZTablaXZTemp].[Sala] "
                            queryString1 &= ") Values ("
                            queryString1 &= "" & wCaja & ", "
                            queryString1 &= "'" & wrLeeMAR.Mar_FAMILIA.Trim & "', "
                            queryString1 &= "'" & wrLeeCODNOM.NOMBRE & "', "
                            queryString1 &= "'" & TmpUnid.ToString.Replace(",", ".") & "', "
                            queryString1 &= "'" & TmpImporte.ToString.Replace(",", ".") & "', "
                            queryString1 &= "' '"
                            queryString1 &= ")"
                        End If
                        '
                        ' Ejecutamos Comando SQL...
                        '
                        cmd.CommandText = queryString1
                        cmd.Connection = conexion
                        cmd.ExecuteNonQuery()
                        '
                    End If
                    '
                Next
                '
                ' Inicia la Structure para ACUMULADOS TOTALES
                ' por formas de PAGO.
                '
                With wrRESUMENXZ
                    .ENTREGA = 0
                    .CHEQUES = 0
                    .EFECTIVO = 0
                    .IMPDTO = 0
                    .IMPIGIC = 0
                    .IMPORTE = 0
                    .OTROS = 0
                    .TARJETA = 0
                    .VALEDTO = 0
                End With
                '
                ' Lectura de la CABECERA de las MESAS =  [MESAC], 
                ' para acumulados totales x Tipo de Pago.
                '
                If dt1.Tables("MESAC").Rows.Count > 0 Then
                    For Each pRow In dt1.Tables("MESAC").Rows
                        '
                        ' ACUMULADOS TOTALES por formas de PAGO.
                        '
                        With wrRESUMENXZ
                            .ENTREGA += CDec(pRow("ENTREGA").ToString.Trim.Replace(".", ","))
                            .CHEQUES += CDec(pRow("CHEQUES").ToString.Trim.Replace(".", ","))
                            .EFECTIVO += CDec(pRow("EFECTIVO").ToString.Trim.Replace(".", ","))
                            .IMPDTO += CDec(pRow("IMPDTO").ToString.Trim.Replace(".", ","))
                            .IMPIGIC += CDec(pRow("IMPIGIC").ToString.Trim.Replace(".", ","))
                            .IMPORTE += CDec(pRow("IMPORTE").ToString.Trim.Replace(".", ","))
                            .OTROS += CDec(pRow("OTROS").ToString.Trim.Replace(".", ","))
                            .TARJETA += CDec(pRow("TARJETA").ToString.Trim.Replace(".", ","))
                            .VALEDTO += CDec(pRow("VALEDTO").ToString.Trim.Replace(".", ","))
                        End With
                    Next
                End If
                '
                ' Si hay Movimientos Imprimir. 
                ' Aqui hay DOs vías posibles:
                '   - Impresion Directa ("True") o Windows("False")?
                '
                LeeTCONA4Cfg("General")
                Select Case wrLeeTCONA4.Tcona4_TKFACDIRWIN
                   '
                   ' *** IMPRESION WINDOWS ***
                   '
                    Case "False"
                        ImprimeX_Z(XZOPC)
                   '
                   ' *** IMPRESION DIRECTA ***
                   '
                    Case "True"
                        ImprimeX_Z_Directa(XZOPC)
                End Select
                '
                ' Aumentar Contador X o Z
                '
                Actualiza_TCONA4(wCaja, XZOPC)
                '
                ' Sólo si ez "Z"
                '
                If XZOPC = "Z" Then
                    '
                    ' Grabamos un regsitro para el HISTÓRICO de Zs.
                    '
                    GrabaHistZ(RetenFecZeta, RetenNumZeta)
                    '
                    ' Primero Actualizamos el Nro de Z en
                    ' las tablas MESAC, MESA
                    ' Despues de imprimir la Z, tenemos num Z en:
                    '    wrLeeTCONA4.Tcona4_NUMZ
                    '
                    queryString1 = "UPDATE [MESAC] SET "
                    queryString1 &= "[MESAC].[NUMZETA]=" & RetenNumZeta & " "
                    queryString1 &= "WHERE [MESAC].[NUMCAJA]=" & wCaja & " "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    queryString1 = "UPDATE [MESA] SET "
                    queryString1 &= "[MESA].[NUMZETA]=" & RetenNumZeta & " "
                    queryString1 &= "WHERE [MESA].[NUMCAJA]=" & wCaja & " "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    ' Paso Datos a Histórico ---->
                    '
                    queryString1 = "INSERT INTO [MESACH] Select * From [MESAC] "
                    queryString1 &= "WHERE [MESAC].[NUMCAJA]=" & wCaja & " "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    queryString1 = "INSERT INTO [MESAH] Select * From [MESA] "
                    queryString1 &= "WHERE [MESA].[NUMCAJA]=" & wCaja & " "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    queryString1 = "DELETE FROM [MESAC] "
                    queryString1 &= "WHERE [MESAC].[NUMCAJA]=" & wCaja & " "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    queryString1 = "DELETE FROM [MESA] "
                    queryString1 &= "WHERE [MESA].[NUMCAJA]=" & wCaja & " "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    ' Borrado de TODAS las (MESAS) CUENTAS Separadas, SALA=999
                    ' Excepto la MESA 999, que siempre debe estar creada!!!
                    '
                    queryString1 = "DELETE FROM [SALA1] WHERE "
                    queryString1 &= "[SALA1].[CAJA]=" & wCaja & " AND "
                    queryString1 &= "[SALA1].[CODIGO]='999' AND "
                    queryString1 &= "[SALA1].[MESA] <> '999' "
                    cmd.CommandText = queryString1
                    cmd.Connection = conexion
                    cmd.ExecuteNonQuery()
                    '
                    ' Importante si se hace Z con MESAS OCUPADAs:
                    '    Necesario hacer esto, REFRESCA
                    '    las botoneras en Form MESAS.
                    '
                    SwZeta = True
                    MyFrm5.Hide()
                    MyFrm1.Focus()
                    MyFrm1.Activate()
                End If
            End If
            '
            With MyFrm5
                .ButtonX.Enabled = True
                .ButtonZ.Enabled = True
            End With
        Catch ex As Exception
            MsgBox("Error: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Generar X/Z, Comprobar Tabla Lectura [MESA]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub GrabaHistZ(wZFecha As String, wZetaNumero As Integer)
        '
        ' Este Procedimiento graba un registro en Histórico de Z
        ' Tabla [ZETA]
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [MESAC] WHERE "
        queryString &= "[MESAC].[NUMCAJA]=" & wCaja & " "
        '
        Dim whIMPORTE As Double = 0 : Dim whDTO As Double = 0
        Dim whEFECTIVO As Double = 0 : Dim whTARJETAS As Double = 0
        Dim whGASTOS As Double = 0
        '
        ' Obtenemos SUMA de Importes varios
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "MESAC")
            '
            If dt.Tables("MESAC").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("MESAC").Rows
                    whIMPORTE += (CDbl(pRow("IMPORTE").ToString().Replace(".", ",")))
                    whDTO += (CDbl(pRow("IMPDTO").ToString().Replace(".", ",")))
                    whEFECTIVO += (CDbl(pRow("EFECTIVO").ToString().Replace(".", ",")))
                    whTARJETAS += (CDbl(pRow("TARJETA").ToString().Replace(".", ",")))
                    whGASTOS += (CDbl(pRow("IMPIGIC").ToString().Replace(".", ",")))
                Next
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Grabando Hist. Z, Comprobar Tabla Lectura [MESAC]")
        End Try
        '
        ' Registro Histórico Z
        '
        queryString = " INSERT INTO [ZETA] ("
        queryString &= "[ZETA].[ZETACAJA], "
        queryString &= "[ZETA].[ZETAFECHA], "
        queryString &= "[ZETA].[ZETANUMERO], "
        queryString &= "[ZETA].[ZETAFECHADIA], "
        queryString &= "[ZETA].[ZETAIMPORTE], "
        queryString &= "[ZETA].[ZETADTO], "
        queryString &= "[ZETA].[ZETAIMPEFECTIVO], "
        queryString &= "[ZETA].[ZETAIMPTARJETAS], "
        queryString &= "[ZETA].[ZETAIMPGASTOS], "
        queryString &= "[ZETA].[ZETAHORA] "
        queryString &= ") VALUES ( "
        queryString &= wCaja & ", "
        queryString &= "'" & wZFecha & "', "
        queryString &= wZetaNumero & ", "
        queryString &= "'" & Date.Now.ToShortDateString & "', "
        queryString &= "'" & whIMPORTE.ToString.Replace(",", ".") & "', "  ' ZETAIMPORTE
        queryString &= "'" & whDTO.ToString.Replace(",", ".") & "', "      ' ZETADTO
        queryString &= "'" & whEFECTIVO.ToString.Replace(",", ".") & "', " ' ZETAIMPEFECTIVO
        queryString &= "'" & whTARJETAS.ToString.Replace(",", ".") & "', " ' ZETAIMPTARJETAS
        queryString &= "'" & whGASTOS.ToString.Replace(",", ".") & "', "   ' ZETAIMPGASTOS
        queryString &= "'" & TimeOfDay.ToLongTimeString.ToString & "' "
        queryString &= ") "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Error: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Grabando Hist. Z, [ZETA]")
        End Try
        '
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Function LeeTablaXZTemp(wCodFam As String) As Boolean
        '
        ' Lectura de una Familia en la Tabla Temporal "X", "Z".
        ' Lectura de Varios en Resumen Consulta.
        '
        ' Key: 
        '   Caja                ( wCaja   )->Global
        '   Cod.Familia / Otros ( wCodFam )
        '
        LeeTablaXZTemp = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        queryString = "SELECT * FROM [ZZTablaXZTemp] "
        queryString &= "WHERE [ZZTablaXZTemp].[NumCaja]=" & wCaja & " AND "
        queryString &= "[ZZTablaXZTemp].[CodigoFamilia]='" & wCodFam & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "ZZTablaXZTemp")
            '
            If dt.Tables("ZZTablaXZTemp").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeTablaXZTemp = True
                For Each pRow In dt.Tables("ZZTablaXZTemp").Rows
                    If pRow("CodigoFamilia").ToString() = wCodFam Then
                        With wrACUMXZ
                            '
                            ' X, Z, y Resumen Consulta
                            '
                            If Not IsDBNull(pRow("TotUnid")) Then
                                .ACUMXZ_Unid = pRow("TotUnid").ToString()
                            Else
                                .ACUMXZ_Unid = "0,00"
                            End If
                            If Not IsDBNull(pRow("TotImpo")) Then
                                .ACUMXZ_Importe = pRow("TotImpo").ToString()
                            Else
                                .ACUMXZ_Importe = "0,00"
                            End If
                            '
                            ' Para Resumen Consulta
                            '
                            If Not IsDBNull(pRow("Sala")) Then
                                .ACUMXZ_Sala = pRow("Sala").ToString()
                            Else
                                .ACUMXZ_Sala = " "
                            End If
                        End With
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [ZZTablaXZTemp]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function ValidarFechasDH(wFecDesde As String, wfechasta As String) As Boolean
        '
        ' Validacion de Fechas: [DD/MM/YYYY(AAAA)], Ej.: 25/10/2016
        '   In.: Desde Fecha / Hasta Fecha   (Str)
        '   Out: True si válida, False Si NO (Bool)
        '
        ValidarFechasDH = True
        '
        Dim fechaProblemaD As Date
        Dim fechaProblemaH As Date
        Dim isValidDateD As Boolean = IsDate(wFecDesde)
        Dim isValidDateH As Boolean = IsDate(wfechasta)
        If wFecDesde.Trim.Length <> 10 Or wfechasta.Trim.Length <> 10 Then
            ValidarFechasDH = False
        End If
        If Not isValidDateD Or Not isValidDateH Then
            ValidarFechasDH = False
        End If
        If Not Date.TryParse(wFecDesde, fechaProblemaD) Then
            ValidarFechasDH = False
        End If
        If Not Date.TryParse(wfechasta, fechaProblemaH) Then
            ValidarFechasDH = False
        End If
        '
        ' DESDE < HASTA
        '
        Dim dias As Double = fechaProblemaH.Subtract(fechaProblemaD).TotalDays
        If dias < 0 Then
            ValidarFechasDH = False
        End If
        '
    End Function

    Public Sub ImprimeInformesConsulta(ImpInforOPC As Integer)
        '
        ' Impresion (a COBVIEW), informes de CONSULTA GENERAL 
        '
        ' ImpInforOPC :
        '    0 = Detallado
        '    1 = Resumen
        '    2 = Resumen x MESES
        '
        Dim MiCadenaWIEW As String = ""
        Dim MiCadenaVAR As String = ""
        Dim TmpTOTUnid(25) As Decimal : Dim TmpTOTImporte(25) As Decimal
        '
        For indexA = 0 To TmpTOTImporte.Length - 1
            TmpTOTUnid(indexA) = 0
            TmpTOTImporte(indexA) = 0
        Next
        '
        Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\CONSGEN.TXT", True, System.Text.Encoding.Default)
            '
            ' Para la consulta, por ahora SIEMPRE privisualizar los INFORMES
            '    en COBVIEW.
            '
            sw.WriteLine("<SET PRINTDIRECT='NO'>")
            '
            ' D./H. Fecha
            '
            MiCadenaVAR = "<VAR VDFEC = "
            MiCadenaVAR &= MyFrm11.TextBoxCDFEC.Text.Trim
            MiCadenaVAR &= ">"
            sw.WriteLine(MiCadenaVAR)
            MiCadenaVAR = "<VAR VHFEC = "
            MiCadenaVAR &= MyFrm11.TextBoxCHFEC.Text.Trim
            MiCadenaVAR &= ">"
            sw.WriteLine(MiCadenaVAR)
            '
            ' Líneas GRID 1, 2, 3
            '
            Select Case ImpInforOPC
                Case 0
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\CONSGEN1.def'>")
                    For counter = 0 To (MyFrm11.GRID1.Rows.Count - 1)
                        '
                        ' <COL #1> ... <COL #n>
                        ' ----- -------- -------
                        '
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(0).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(1).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(2).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(3).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(4).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(5).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #7>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(6).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #8>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(7).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #9>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(8).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #10>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID1.Rows(counter).Cells(9).Value.ToString
                        sw.WriteLine(MiCadenaWIEW)
                        '
                        TmpTOTUnid(0) += CDec(MyFrm11.GRID1.Rows(counter).Cells(6).Value.ToString.Trim.Replace(".", ","))
                        TmpTOTImporte(0) += CDec(MyFrm11.GRID1.Rows(counter).Cells(8).Value.ToString.Trim.Replace(".", ","))
                    Next
                    '
                    ' Linea Totales
                    '
                    sw.WriteLine("<BR><B>")
                    MiCadenaWIEW = "<COL #1>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                    MiCadenaWIEW = MiCadenaWIEW & " TOTALES -> "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #7>"
                    MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(0).ToString(fmtUnid)
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #8>"
                    MiCadenaWIEW = MiCadenaWIEW & (TmpTOTImporte(0) / TmpTOTUnid(0)).ToString(fmtImporte)
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #9>"
                    MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(0).ToString(fmtImporte)
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #10>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("</B>")
                    MyFrm11.ButtonImpre1.Enabled = True
                Case 1
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\CONSGEN2.def'>")
                    For counter = 0 To (MyFrm11.GRID2.Rows.Count - 1)
                        '
                        ' <COL #1> ... <COL #n>
                        ' ----- -------- -------
                        '
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID2.Rows(counter).Cells(0).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID2.Rows(counter).Cells(1).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID2.Rows(counter).Cells(2).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID2.Rows(counter).Cells(3).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID2.Rows(counter).Cells(4).Value.ToString
                        sw.WriteLine(MiCadenaWIEW)
                        '
                        TmpTOTUnid(0) += CDec(MyFrm11.GRID2.Rows(counter).Cells(2).Value.ToString.Trim.Replace(".", ","))
                        TmpTOTImporte(0) += CDec(MyFrm11.GRID2.Rows(counter).Cells(4).Value.ToString.Trim.Replace(".", ","))
                    Next
                    '
                    ' Linea Totales
                    '
                    sw.WriteLine("<BR><B>")
                    MiCadenaWIEW = "<COL #1>"
                    MiCadenaWIEW = MiCadenaWIEW & " "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                    MiCadenaWIEW = MiCadenaWIEW & " TOTALES -> "
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                    MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(0).ToString(fmtUnid)
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                    MiCadenaWIEW = MiCadenaWIEW & (TmpTOTImporte(0) / TmpTOTUnid(0)).ToString(fmtImporte)
                    MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                    MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(0).ToString(fmtImporte)
                    sw.WriteLine(MiCadenaWIEW)
                    sw.WriteLine("</B>")
                    MyFrm11.ButtonImpre2.Enabled = True
                Case 2
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\CONSGEN3.def'>")
                    '
                    Dim TipoLin As String = ""
                    Dim HayU As Boolean = False
                    Dim HayI As Boolean = False
                    '
                    For counter = 0 To (MyFrm11.GRID3.Rows.Count - 1)
                        '
                        ' <COL #1> ... <COL #n>
                        ' ----- -------- -------
                        '
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(0).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(1).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(2).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(3).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(4).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(5).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #7>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(6).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #8>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(7).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #9>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(8).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #10>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(9).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #11>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(10).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #12>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(11).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #13>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(12).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #14>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(13).Value.ToString
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #15>"
                        MiCadenaWIEW = MiCadenaWIEW & MyFrm11.GRID3.Rows(counter).Cells(14).Value.ToString
                        sw.WriteLine(MiCadenaWIEW)
                        '
                        ' Tipo Linea "U"nidades / "I"mporte
                        '
                        TipoLin = MyFrm11.GRID3.Rows(counter).Cells(15).Value.ToString.Trim
                        '
                        For i = 2 To 14
                            Select Case TipoLin
                                Case "U"
                                    HayU = True
                                    TmpTOTUnid(i) += CDec(MyFrm11.GRID3.Rows(counter).Cells(i).Value.ToString.Trim.Replace(".", ","))
                                Case "I"
                                    HayI = True
                                    TmpTOTImporte(i) += CDec(MyFrm11.GRID3.Rows(counter).Cells(i).Value.ToString.Trim.Replace(".", ","))
                            End Select
                        Next
                        '
                    Next
                    '
                    ' Linea Totales
                    '
                    sw.WriteLine("<BR><B>")
                    If HayU Then
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                        MiCadenaWIEW = MiCadenaWIEW & " TOT. Unidades -> "
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(2).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(3).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(4).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(5).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #7>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(6).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #8>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(7).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #9>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(8).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #10>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(9).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #11>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(10).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #12>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(11).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #13>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(12).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #14>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(13).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #15>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTUnid(14).ToString(fmtUnid)
                        sw.WriteLine(MiCadenaWIEW)
                    End If
                    '
                    If HayI Then
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW = MiCadenaWIEW & " "
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #2>"
                        MiCadenaWIEW = MiCadenaWIEW & " TOT. Importes -> "
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #3>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(2).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #4>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(3).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #5>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(4).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #6>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(5).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #7>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(6).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #8>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(7).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #9>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(8).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #10>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(9).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #11>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(10).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #12>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(11).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #13>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(12).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #14>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(13).ToString(fmtUnid)
                        MiCadenaWIEW = MiCadenaWIEW & "<COL #15>"
                        MiCadenaWIEW = MiCadenaWIEW & TmpTOTImporte(14).ToString(fmtUnid)
                        sw.WriteLine(MiCadenaWIEW)
                    End If
                    sw.WriteLine("</B>")
                    MyFrm11.ButtonImpre3.Enabled = True
            End Select
            '
            sw.Flush()
            sw.Close()
            '
        End Using
        '
        ' Llamada a COBVIEW pasandole como parámetro el informe generado...
        '
        Dim myProcess As New Process()
        Dim StringArguments As String
        Try
            StringArguments = ""
            myProcess.StartInfo.UseShellExecute = True
            myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
            StringArguments = "C:\TRIVAGES\InformesCobview\CONSGEN.TXT"
            myProcess.StartInfo.Arguments = StringArguments
            myProcess.Start()
        Catch Mye As Exception
            MsgBox("ERROR: " & Mye.Source & vbCrLf & Mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: COBVIEW.")
        End Try
        '
    End Sub

    Public Function HayLineasNuevasGRID1(Optional wOpt As Integer = 0) As Boolean
        '------------------------------------------------------------'
        ' Esta Función me informa de si hay líneas Nuevas "N"***
        '   en la CUENTA DE LA MESA ACTUAL. Pesimista -> Optimista.
        '
        ' wOpt es opcional y determina si queremos evaluar otros 
        '      elementos además de las marcadas como "U"NIDADES NUEVAS.
        '
        ' MyFrm2.GRID1
        '------------------------------------------------------------'
        '   0 Cod. Art           (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N           (No visible)  ***
        '   6 Cod. Combinados    (No Visible)
        '   7 Raciones           (No Visible)
        '   8 Nro. Plato         (No Visible)
        '
        '
        HayLineasNuevasGRID1 = False
        With MyFrm2.GRID1
            For counter = 0 To (.Rows.Count - 1)
                Select Case wOpt
                    Case 0
                        If .Rows(counter).Cells(5).Value.ToString.Trim = "N" Then
                            HayLineasNuevasGRID1 = True
                            Exit For
                        End If
                    Case 1
                        If .Rows(counter).Cells(5).Value.ToString.Trim = "N" And
                           .Rows(counter).Cells(3).Value.ToString.Trim <> "0.00" Then
                            HayLineasNuevasGRID1 = True
                        End If
                End Select
            Next
        End With
        '
    End Function

    Public Sub GeneraTICKETSaAreas(iModoWork As Integer)
        '------------------------------------------------------------'
        ' Al aparcar se imprimen los TICKETS en las diferentes AREAS '
        '    segun el producto de que se trate.                      '
        ' Aqui se tendrá en Cuenta el Nro. de Plato.    
        '
        ' iModoWork:
        '    0 - Impresion   TICKETS a Areas determinadas.
        '    1 - Visualizacion TICKETS a Areas determinadas. (Botón ENVIA)
        '------------------------------------------------------------'
        ' GRID1
        '   0 Cod. Art           (No Visible)
        '   1 Unid. Existentes
        '   2 Descripcion
        '   3 Unid. Nuevas
        '   4 Importe
        '   5 Tipo E/N           (No visible)  ***
        '   6 Cod. Combinados    (No Visible)
        '   7 Raciones           (No Visible)
        '   8 Nro. Plato         (No Visible)
        '
        ' Borrado del Contenido Previo.
        '
        BorraZZTablaOAreasTemp()
        '
        Dim HayNuevas As Boolean = False
        Dim NroPlato As Integer = 0
        '
        For counter = 0 To (MyFrm2.GRID1.Rows.Count - 1)
            '
            ' SOLO lo Nuevo Fichado, "N"
            ' Ademas Compruebo que Unidades NUEVAS > 0
            '  ya que una linea del GRID1 puede ser MARCADA como "N", 
            '   para fines de actualizacion sin que necesariamente vayan unidades NUEVAS, 
            '   (por ejemplo al Borrar unidades existentes).
            '
            With MyFrm2.GRID1
                If .Rows(counter).Cells(5).Value.ToString.Trim = "N" And
                   .Rows(counter).Cells(3).Value.ToString.Trim <> "0.00" Then
                    '
                    HayNuevas = True
                    If LeeMar(.Rows(counter).Cells(0).Value.ToString.Trim) = True Then
                        '
                        ' Leemos Art. / Conocemos Area
                        ' El AREA=999999999 indica que es NULL y NO SE IMPRIME Nada.
                        ' Si area vale 0 y el AREA cero no esta creada tampoco.
                        '
                        If Not IsDBNull(wrLeeMAR.Mar_AREA) And
                               wrLeeMAR.Mar_AREA.ToString.Trim.Length > 0 And
                               wrLeeMAR.Mar_AREA <> 999999999 Then
                            '
                            '  Leemos AREA, Existe?
                            '
                            If LeeArea(wrLeeMAR.Mar_AREA) = True Then
                                '
                                ' Si el Area esta creada, Hay donde imprimir un TICKET
                                '
                                '
                                ' Nro. PLATO
                                '
                                NroPlato = CInt(.Rows(counter).Cells(8).Value)
                                '
                                wrMESA.Mesa_UNID = .Rows(counter).Cells(3).Value.ToString.Replace(",", ".")
                                wrMESA.Mesa_IMPORTE = .Rows(counter).Cells(4).Value.ToString.Replace(",", ".")
                                '
                                OrdenaPorAreas(wrLeeMAR.Mar_AREA,
                                               NroPlato,
                                               .Rows(counter).Cells(0).Value.ToString.Trim,
                                               .Rows(counter).Cells(6).Value.ToString.Trim,
                                               .Rows(counter).Cells(2).Value.ToString.Trim,
                                               wrMESA.Mesa_UNID,
                                               wrMESA.Mesa_IMPORTE)
                                '
                                ' Gestionamos aqui, Si un AREA, debe imprimir a OTRAS AREAS.
                                '
                                With wrLeeAREAS
                                    If Not IsDBNull(.AREA2) Then
                                        If Not String.IsNullOrEmpty(.AREA2.ToString) And
                                        .AREA2.ToString.Length > 0 Then
                                            If .AREA2 <> 999 Then
                                                OrdenaPorAreas(.AREA2, NroPlato,
                                               MyFrm2.GRID1.Rows(counter).Cells(0).Value.ToString.Trim,
                                               MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString.Trim,
                                               MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString.Trim,
                                               wrMESA.Mesa_UNID,
                                               wrMESA.Mesa_IMPORTE)
                                            End If
                                        End If
                                    End If
                                    If Not IsDBNull(.AREA3) Then
                                        If Not String.IsNullOrEmpty(.AREA3.ToString) And
                                        .AREA3.ToString.Length > 0 Then
                                            If .AREA3 <> 999 Then
                                                OrdenaPorAreas(.AREA3, NroPlato,
                                               MyFrm2.GRID1.Rows(counter).Cells(0).Value.ToString.Trim,
                                               MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString.Trim,
                                               MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString.Trim,
                                               wrMESA.Mesa_UNID,
                                               wrMESA.Mesa_IMPORTE)
                                            End If
                                        End If
                                    End If
                                    If Not IsDBNull(.AREA4) Then
                                        If Not String.IsNullOrEmpty(.AREA4.ToString) And
                                        .AREA4.ToString.Length > 0 Then
                                            If .AREA4 <> 999 Then
                                                OrdenaPorAreas(.AREA4, NroPlato,
                                               MyFrm2.GRID1.Rows(counter).Cells(0).Value.ToString.Trim,
                                               MyFrm2.GRID1.Rows(counter).Cells(6).Value.ToString.Trim,
                                               MyFrm2.GRID1.Rows(counter).Cells(2).Value.ToString.Trim,
                                               wrMESA.Mesa_UNID,
                                               wrMESA.Mesa_IMPORTE)
                                            End If
                                        End If
                                    End If
                                End With
                            End If
                        End If
                    End If
                End If
            End With
        Next
        '
        ' Finalmente si hay Unidades NUEVAS:
        '    0 = Lanzamos los TICKETS a las AREAS correspondientes.
        '    1 = Solo queremos Visualizar, no hay salida a los impresores.
        '
        If HayNuevas Then
            Select Case iModoWork
                Case 0
                    ImprimeTICKETSaAreas()
                Case 1
                    VisualizaContenidoAreas()
            End Select
        End If
        '
    End Sub

    Public Sub VisualizaContenidoAreas()
        '
        ' Este procedimiento Muestra Visualmente lo que se va a ENVIAR
        '   a las distintas AREAS.
        '
        '
        Dim swPlato As Boolean = False
        Dim swArea As Boolean = False
        Dim MyPlato As String = "1"
        Dim MyArea As Integer = 0
        '
        MyFrm16.GRIDENVAREAS.Visible = False
        MyFrm16.GRIDENVAREAS.Rows.Clear()
        '
        Try
            Dim conexion As New SqlConnection
            conexion.ConnectionString = SQL_CadenaConexion
            conexion.Open()
            Dim queryString As String = ""
            queryString = "SELECT * FROM [ZZTablaOAreasTemp] "
            queryString &= " ORDER BY [AREA], [PLATO] "
            '
            Dim dt As DataSet = New DataSet
            Dim LineaImpre As String = ""
            '
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "AREAS")
            '
            If dt.Tables("AREAS").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("AREAS").Rows
                    '
                    ' Area
                    '
                    If swArea = False Then
                        swArea = True
                        MyArea = CInt(pRow("AREA").ToString)
                        LeeArea(MyArea)
                        MyFrm16.GRIDENVAREAS.Rows.Add(wrLeeAREAS.DESCRIPCION, "", "")
                    End If
                    If CInt(pRow("AREA").ToString()) <> MyArea Then
                        MyArea = CInt(pRow("AREA").ToString)
                        LeeArea(MyArea)
                        MyFrm16.GRIDENVAREAS.Rows.Add(wrLeeAREAS.DESCRIPCION, "", "")
                    End If
                    '
                    ' Orden de Plato
                    '
                    If Not IsDBNull(pRow("PLATO")) Then
                        If swPlato = False Then
                            swPlato = True
                            MyPlato = pRow("PLATO").ToString()
                            If pRow("PLATO").ToString() = "2" Then
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      "-------------- 2do Plato ---------------",
                                                      "")
                            End If
                            If pRow("PLATO").ToString() = "3" Then
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      "-------------- 3er Plato ---------------",
                                                      "")
                            End If
                            If pRow("PLATO").ToString() = "4" Then
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      "-------------- 4to Plato ---------------",
                                                      "")
                            End If
                        End If
                        If pRow("PLATO").ToString() <> MyPlato Then
                            MyPlato = pRow("PLATO").ToString()
                            If pRow("PLATO").ToString() = "2" Then
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      "-------------- 2do Plato ---------------",
                                                      "")
                            End If
                            If pRow("PLATO").ToString() = "3" Then
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      "-------------- 3er Plato ---------------",
                                                      "")
                            End If
                            If pRow("PLATO").ToString() = "4" Then
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      "-------------- 4to Plato ---------------",
                                                      "")
                            End If
                        End If
                    End If
                    MyFrm16.GRIDENVAREAS.Rows.Add(CDbl(pRow("UNID").ToString().Replace(".", ",")).ToString(fmtUnid).Replace(",", "."),
                                                      pRow("NOMBREART").ToString(),
                                                      "")
                    '
                    ' *** Combinados *** Si Hay
                    '
                    If pRow("COMBINADO").ToString().Trim.Length > 0 Then
                        Dim words As String() = pRow("COMBINADO").ToString().Trim.Split(New Char() {"/"c})
                        For i As Integer = 0 To words.Length - 1
                            If LeeMar(words(i)) = False Then
                                wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                            Else
                                MyFrm16.GRIDENVAREAS.Rows.Add("",
                                                      " + " & wrLeeMAR.Mar_DESCRIPCION.Trim,
                                                      "")
                            End If
                        Next
                    End If
                    '
                Next
            End If
            '
            conexion.Close()
            dt.Dispose()
            conexion.Dispose()
            '
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    "Error Lectura datos Tabla AREA ")
        End Try
        With MyFrm16
            .GRIDENVAREAS.ClearSelection()
            .GRIDENVAREAS.Visible = True
            .Button16Envia.Select()
        End With
        '
    End Sub

    Private Sub BorraZZTablaOAreasTemp()
        '
        ' Borrado TOTAL de las filas de la Tabla [ZZTablaOAreasTemp]
        ' Esta Tabla es "Pseudo-Temporal".
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString1 As String = " DELETE FROM [ZZTablaOAreasTemp] "
        '
        ' Ejecutamos Comando SQL...
        '
        cmd.CommandText = queryString1
        cmd.Connection = conexion
        cmd.ExecuteNonQuery()
        '
        conexion.Close()
        conexion.Dispose()
        '
    End Sub

    Private Sub OrdenaPorAreas(OArea As Integer,
                               OPlato As Integer,
                               OCodArt As String,
                               OCombi As String,
                               OnomArt As String,
                               OUnid As String,
                               Oimporte As String)
        '
        ' Ordenamos los PRODUCTOS por areas.
        '    ID
        '    AREA
        '    PLATO
        '    Cod. Art
        '    Combinados
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        ' Generamos un ID unico.
        '
        Dim MiID As Integer = 0
        Dim MiHora As String = DateTime.Now.ToString("hhmmssffff")
        MiHora = MiHora.Replace(":", "")
        MiID += CInt(MiHora)
        MiID += CInt(MyFrm2.TextBoxFactura.Text.Trim)
        '
        Dim queryString1 As String = "INSERT INTO [ZZTablaOAreasTemp] ("
        queryString1 &= "[ID],"
        queryString1 &= "[AREA],"
        queryString1 &= "[PLATO],"
        queryString1 &= "[ARTICULO],"
        queryString1 &= "[COMBINADO],"
        queryString1 &= "[NOMBREART],"
        queryString1 &= "[UNID], "
        queryString1 &= "[IMPORTE] "
        queryString1 &= ") Values ("
        queryString1 &= MiID & ", "
        queryString1 &= OArea & ", "
        queryString1 &= OPlato & ", "
        queryString1 &= "'" & OCodArt & "', "
        queryString1 &= "'" & OCombi & "', "
        queryString1 &= "'" & OnomArt & "', "
        queryString1 &= "'" & OUnid & "', "
        queryString1 &= "'" & Oimporte & "' "
        queryString1 &= ")"
        '
        ' Ejecutamos Comando SQL...
        '
        cmd.CommandText = queryString1
        cmd.Connection = conexion
        cmd.ExecuteNonQuery()
        '
        conexion.Close()
        conexion.Dispose()
        '
    End Sub

    Public Sub ImprimeTICKETSaAreas()
        '
        ' Imprime TICKET de CADA AREA.
        ' Impresion Directa a Areas / Impresora WINDOWS.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT DISTINCT [AREA] FROM [ZZTablaOAreasTemp]"
        '
        ' wAREAS = SELECT ... [ZZTablaOAreasTemp]
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "wAREAS")
            If dt.Tables("wAREAS").Rows.Count > 0 Then
                Dim pRow As DataRow
                For Each pRow In dt.Tables("wAREAS").Rows
                    '
                    ' Impresion Directa a Areas / Impresora WINDOWS
                    '
                    LeeArea(CInt(pRow("AREA").ToString))
                    If IsDBNull(wrLeeAREAS.TIPOIMPRESION) Or wrLeeAREAS.TIPOIMPRESION.Trim = "False" Then
                        AreaAImpresora(pRow("AREA").ToString())
                    Else
                        AreaAImpresoraWINDOWS(pRow("AREA").ToString())
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [ZZTablaOAreasTemp]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Sub

    Private Sub AreaAImpresora(iArea As String)
        '
        ' Imprime UN TICKET en el AREA indicada.
        ' Cada Area indica el modelo de impresor a utilizar:
        '   [AREAS], pRow("MODELOIMPRE").ToString()
        ' En funcion de esto para Cada Modelo podremos tener distintos
        '  códigos ESC/POS si fuera necesario.
        '
        If LeeArea(CInt(iArea)) Then
            '
            ' Impresion Directa Al puerto determinado Por AREA.
            '
            ' Debemos atender al MODELO de impresora de cada AREA
            '   para poder enviar los códigos ESC/POS correctos.
            ' Lo primero es Comprobar si es MODELO de Impresora esta creado o no
            '  en la Tabla [IMPRESORAS].
            ' Luego Comprobar si es MODELO de Impresora esta definido
            '  en la Tabla [AREAS].[MODELOIMPRE].
            '
            If wrLeeAREAS.MODELOIMPRE.Length > 0 And Not IsDBNull(wrLeeAREAS.MODELOIMPRE) Then
                '
                ' Modelo esta creado en IMPRESORAS?
                '
                If LeeDatosImpresora(wrLeeAREAS.MODELOIMPRE.Trim) = False Then
                    '
                    ' El MODELO de Impresora no esta creado en la Tabla
                    ' IMPRESORAS, y damos un Aviso
                    '
                    msg = "El MODELO de Impresora " & vbCrLf
                    msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                    msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
                    style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                    title = "Modelo de impresora no Creado."
                    MsgBox(msg, style, title)
                    Exit Sub
                End If
            Else
                msg = "El MODELO de Impresora " & vbCrLf
                msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                msg = "para el Area " & vbCrLf
                msg &= iArea.Trim & vbCrLf
                msg &= "no está Definido en la tabla [AREAS].[MODELOIMPRE] " & vbCrLf
                style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                title = "Modelo de impresora no Definido."
                MsgBox(msg, style, title)
                Exit Sub
            End If
            '
            Dim swPlato As Boolean = False
            Dim MyPlato As String = "1"
            '
            Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
            fh = CType(Win32API.CreateFile(wrLeeAREAS.PUERTOIMPRE.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                       Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
            Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
            FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
            SW = New StreamWriter(FS)
            '
            ' *** TICKET ***
            '
            Try
                Dim conexion As New SqlConnection
                conexion.ConnectionString = SQL_CadenaConexion
                conexion.Open()
                Dim queryString As String = ""
                queryString = "SELECT * FROM [ZZTablaOAreasTemp] WHERE "
                queryString &= "[AREA]=" & CInt(iArea)
                queryString &= " ORDER BY [PLATO] "
                '
                Dim dt As DataSet = New DataSet
                Dim LineaImpre As String = ""
                '
                TblTPVS = New SqlDataAdapter(queryString, conexion)
                TblTPVS.Fill(dt, "AREAS")
                '
                With wrIMPRESORA
                    LeeVendedor(CInt(MyFrm2.TextBoxCamarero.Text.Trim))
                    SW.WriteLine(.DOBLEALTO)
                    SW.WriteLine("Mesa.: " & MyFrm2.TextBoxNumMesa.Text.Trim & ", Camarero.: " & MyFrm2.TextBoxCamarero.Text.Trim & " " & wrLeeCODNOM.NOMBRE)
                    SW.WriteLine(.DOBLEANCHO)
                    SW.WriteLine(Format(Date.Now, "dd/MM/yy") & " " & TimeOfDay.ToShortTimeString.ToString & ", " & wrLeeAREAS.DESCRIPCION.Trim)
                    SW.WriteLine(.PROPORCIONAL)
                    SW.WriteLine(.AVAZALINEA)
                    '
                    If dt.Tables("AREAS").Rows.Count > 0 Then
                        Dim pRow As DataRow
                        For Each pRow In dt.Tables("AREAS").Rows
                            '
                            ' Orden de Plato
                            '
                            If Not IsDBNull(pRow("PLATO")) Then
                                If swPlato = False Then
                                    swPlato = True
                                    MyPlato = pRow("PLATO").ToString()
                                    If pRow("PLATO").ToString() = "1" Then
                                        SW.WriteLine(.AVAZALINEA)
                                    End If
                                    If pRow("PLATO").ToString() = "2" Then
                                        SW.WriteLine(.AVAZALINEA)
                                        SW.WriteLine("-------------- 2do Plato ---------------")
                                    End If
                                    If pRow("PLATO").ToString() = "3" Then
                                        SW.WriteLine(.AVAZALINEA)
                                        SW.WriteLine("-------------- 3er Plato ---------------")
                                    End If
                                    If pRow("PLATO").ToString() = "4" Then
                                        SW.WriteLine(.AVAZALINEA)
                                        SW.WriteLine("-------------- 4to Plato ---------------")
                                    End If
                                End If
                                If pRow("PLATO").ToString() <> MyPlato Then
                                    MyPlato = pRow("PLATO").ToString()
                                    If pRow("PLATO").ToString() = "2" Then
                                        SW.WriteLine(.AVAZALINEA)
                                        SW.WriteLine("-------------- 2do Plato ---------------")
                                    End If
                                    If pRow("PLATO").ToString() = "3" Then
                                        SW.WriteLine(.AVAZALINEA)
                                        SW.WriteLine("-------------- 3er Plato ---------------")
                                    End If
                                    If pRow("PLATO").ToString() = "4" Then
                                        SW.WriteLine(.AVAZALINEA)
                                        SW.WriteLine("-------------- 4to Plato ---------------")
                                    End If
                                End If
                            End If
                            '
                            LineaImpre = ""
                            LineaImpre &= CDbl(pRow("UNID").ToString().Replace(".", ",")).ToString(fmtUnid).Replace(",", ".")
                            LineaImpre &= " "
                            LineaImpre &= pRow("NOMBREART").ToString()
                            SW.WriteLine(LineaImpre)
                            '
                            ' *** Combinados *** Si Hay
                            '
                            If pRow("COMBINADO").ToString().Trim.Length > 0 Then
                                Dim words As String() = pRow("COMBINADO").ToString().Trim.Split(New Char() {"/"c})
                                For i As Integer = 0 To words.Length - 1
                                    If LeeMar(words(i)) = False Then
                                        wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                    Else
                                        SW.WriteLine(" + " & wrLeeMAR.Mar_DESCRIPCION.Trim)
                                    End If
                                Next
                            End If
                            '
                        Next
                    End If
                    '
                    ' Avanza N lineas, determinadas en Ref. Generales
                    '
                    LeeTCONA4Cfg("General")
                    '
                    ' Lineas <= 9
                    ' Por si se diera el caso, no derrochar MUCHO Papel ...
                    '
                    If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                            wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                        End If
                        For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                            SW.WriteLine(.AVAZALINEA)
                        Next
                    Else
                        SW.WriteLine(.AVAZALINEA) ' Por defecto al menos 1 Linea de salto
                    End If
                    '
                    ' Corte PAPEL
                    '
                    SW.WriteLine(.CORTE)
                End With
                '
                conexion.Close()
                dt.Dispose()
                conexion.Dispose()
                '
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    "TICKET.: Error de Impresora, AREA: " & iArea)
            End Try
            FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
        End If
        '
    End Sub

    Private Sub AreaAImpresoraWINDOWS(iArea As String)
        '
        ' Imprime UN TICKET en el AREA indicada, 
        '    a la impresora Predeterminada de (WINDOWS).
        ' Salida a COBVIEW.
        '
        Dim swPlato As Boolean = False
        Dim MyPlato As String = "1"
        Dim MiCadenaWIEW As String = ""
        '
        If LeeTCONA4Cfg("General") = True Then
            '
            ' Si el MODELO de impresora no esta ON-LINE y 
            '  no hacemos "PREVIEW" salimos.
            '
            If ImpresoraEstaONLINE(ObtenerImpresoraPredeterminada.Trim) = False And
               wrLeeTCONA4.Tcona4_COBVIEWPDSN = "False" Then
                title = "¿Impresora Apagada?"
                style = MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly
                msg = "Por favor, compruebe su impresora." & vbCrLf
                msg &= "NO se imprimirá el TICKET." & vbCrLf & vbCrLf
                msg &= "Se detectan " & wrProp_IMPRESORA.JobCountSinceLastReset & " trabajos Pendientes."
                response = MsgBox(msg, style, title)
                Exit Sub
            End If
        End If
        '
        ' Ticket a TEXTO (Para COBVIEW)
        '
        Dim MiID As Integer = 0
        Dim MiHora As String = DateTime.Now.ToString("hhmmssffff")
        MiHora = MiHora.Replace(":", "")
        MiID += CInt(MiHora)
        MiID += CInt(MyFrm2.TextBoxFactura.Text.Trim)
        '
        Dim NombreTXT As String = ""
        NombreTXT &= iArea.ToString & MiID
        '
        Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\" & NombreTXT, True, Text.Encoding.Default)
            '
            ' *** TICKET ***
            ' Previsualizacion en COBVIEW, ahora determinado por Ref. Generales
            '
            If wrLeeTCONA4.Tcona4_COBVIEWPDSN = "True" Then
                sw.WriteLine("<SET PRINTDIRECT='NO'>")
            Else
                sw.WriteLine("<SET PRINTDIRECT='YES'>")
            End If
            '
            sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKAREAS.def'>")
            '
            Try
                Dim conexion As New SqlConnection
                conexion.ConnectionString = SQL_CadenaConexion
                conexion.Open()
                Dim queryString As String = ""
                queryString = "SELECT * FROM [ZZTablaOAreasTemp] WHERE "
                queryString &= "[AREA]=" & CInt(iArea)
                queryString &= " ORDER BY [PLATO] "
                '
                Dim dt As DataSet = New DataSet
                Dim LineaImpre As String = ""
                '
                TblTPVS = New SqlDataAdapter(queryString, conexion)
                TblTPVS.Fill(dt, "AREAS")
                '
                LeeVendedor(CInt(MyFrm2.TextBoxCamarero.Text.Trim))
                '
                sw.WriteLine("<font face='Consolas' size='14'>")
                sw.WriteLine("Mesa.: " & MyFrm2.TextBoxNumMesa.Text.Trim & ", Camarero.: " & MyFrm2.TextBoxCamarero.Text.Trim & " " & wrLeeCODNOM.NOMBRE)
                sw.WriteLine("<BR>")
                sw.WriteLine("<font face='Consolas' size='10'>")
                sw.WriteLine(Format(Date.Now, "dd/MM/yy") & " " & TimeOfDay.ToShortTimeString.ToString & ", " & wrLeeAREAS.DESCRIPCION.Trim)
                sw.WriteLine("<BR>")
                '
                If dt.Tables("AREAS").Rows.Count > 0 Then
                    Dim pRow As DataRow
                    For Each pRow In dt.Tables("AREAS").Rows
                        '
                        ' Orden de Plato
                        '
                        If Not IsDBNull(pRow("PLATO")) Then
                            If swPlato = False Then
                                swPlato = True
                                MyPlato = pRow("PLATO").ToString()
                                If pRow("PLATO").ToString() = "1" Then
                                    sw.WriteLine("<BR>")
                                End If
                                If pRow("PLATO").ToString() = "2" Then
                                    sw.WriteLine("<BR>")
                                    sw.WriteLine("-------------- 2do Plato ---------------<BR>")
                                End If
                                If pRow("PLATO").ToString() = "3" Then
                                    sw.WriteLine("<BR>")
                                    sw.WriteLine("-------------- 3er Plato ---------------<BR>")
                                End If
                                If pRow("PLATO").ToString() = "4" Then
                                    sw.WriteLine("<BR>")
                                    sw.WriteLine("-------------- 4to Plato ---------------<BR>")
                                End If
                            End If
                            If pRow("PLATO").ToString() <> MyPlato Then
                                MyPlato = pRow("PLATO").ToString()
                                If pRow("PLATO").ToString() = "2" Then
                                    sw.WriteLine("<BR>")
                                    sw.WriteLine("-------------- 2do Plato ---------------<BR>")
                                End If
                                If pRow("PLATO").ToString() = "3" Then
                                    sw.WriteLine("<BR>")
                                    sw.WriteLine("-------------- 3er Plato ---------------<BR>")
                                End If
                                If pRow("PLATO").ToString() = "4" Then
                                    sw.WriteLine("<BR>")
                                    sw.WriteLine("-------------- 4to Plato ---------------<BR>")
                                End If
                            End If
                        End If
                        '
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW &= CDbl(pRow("UNID").ToString().Replace(".", ",")).ToString(fmtUnid).Replace(",", ".")
                        MiCadenaWIEW &= "<COL #2>"
                        MiCadenaWIEW &= pRow("NOMBREART").ToString()
                        sw.WriteLine(MiCadenaWIEW)
                        '
                        ' *** Combinados *** Si Hay
                        '
                        If pRow("COMBINADO").ToString().Trim.Length > 0 Then
                            Dim words As String() = pRow("COMBINADO").ToString().Trim.Split(New Char() {"/"c})
                            For i As Integer = 0 To words.Length - 1
                                If LeeMar(words(i)) = False Then
                                    wrLeeMAR.Mar_DESCRIPCION = "[*COMBI NO LEIDO*]"
                                Else
                                    MiCadenaWIEW = "<COL #1>"
                                    MiCadenaWIEW &= " "
                                    MiCadenaWIEW &= "<COL #2>"
                                    MiCadenaWIEW &= " + " & wrLeeMAR.Mar_DESCRIPCION.Trim
                                    sw.WriteLine(MiCadenaWIEW)
                                End If
                            Next
                        End If
                        '
                    Next
                End If
                '
                ' Avanza N lineas, determinadas en Ref. Generales
                '
                LeeTCONA4Cfg("General")
                '
                ' Por si se diera el caso, no derrochar MUCHO Papel ...
                '
                If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                    If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                        wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                    End If
                    For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                        sw.WriteLine("<BR>")
                    Next
                Else
                    sw.WriteLine("<BR>") ' Por defecto al menos 1 Linea de salto
                End If
                '
                ' Corte PAPEL
                '
                'sw.WriteLine(.CORTE)
                '
                conexion.Close()
                dt.Dispose()
                conexion.Dispose()
                '
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    "TICKET.: Error de Impresora, AREA: " & iArea)
            End Try
            '
            sw.Flush()
            sw.Close()
        End Using
        '
        ' Llamada a COBVIEW pasandole como parámetro el informe generado...
        '
        Dim myProcess As New Process()
        Dim StringArguments As String
        Try
            StringArguments = ""
            myProcess.StartInfo.UseShellExecute = True
            myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
            StringArguments = "C:\TRIVAGES\InformesCobview\" & NombreTXT
            myProcess.StartInfo.Arguments = StringArguments
            myProcess.Start()
        Catch Mye As Exception
            MsgBox("ERROR: " & Mye.Source & vbCrLf & Mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: COBVIEW.")
        End Try
        '
        ' Nota, Borrar Archivo:
        '    Se puede delegar a COBVIEW que borre estos archivos.
        '
        'My.Computer.FileSystem.DeleteFile("C:\TRIVAGES\InformesCobview\" & NombreTXT)
        '
    End Sub


    Public Sub CargaListaImpresoras()
        '
        ' Lista de Impresoras del Sistema.
        '
        Dim i As Integer
        Dim pkInstalledPrinters As String
        '
        MyFrm6.GRIDIMPRESYS.Visible = False
        MyFrm6.GRIDIMPRESYS.Rows.Clear()
        '
        For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
            pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i)
            MyFrm6.GRIDIMPRESYS.Rows.Add(pkInstalledPrinters)
        Next
        MyFrm6.GRIDIMPRESYS.Visible = True
        '
    End Sub

    Public Function ObtenerImpresoraPredeterminada() As String
        '
        ObtenerImpresoraPredeterminada = ""
        Dim ConfiguracionesDeImpresion As New PrinterSettings
        Try
            ObtenerImpresoraPredeterminada = ConfiguracionesDeImpresion.PrinterName
        Catch ex As Exception
            ObtenerImpresoraPredeterminada = ""
        End Try
        '
    End Function

    Public Sub AbreCajon_Corte(iComando As Integer, iPuerto As String, iOpcion As String)
        '
        ' Desde este Procedimeinto se intentará abrir el Cajón / Cortar Papel cuando sea Necesario.
        '
        ' iComando:
        '   0 = Abre Cajón.
        '   1 = Cortar Papel.
        '
        ' iOpcion:
        '    - "DOS" -----> Comandos directos desde un .BAT + Códigos en .TXT
        '                   para entornos donde el uso desde consola MS-DOS sea necesario.
        '    - "WINDOWS" -> Se envía el impulso directamente al Puerto desde VB.NET
        '                   ( StreamWriter + FileStream )
        '
        ' Para Minimizar Errores, al menos comprobar
        '  si la impresora MODELO de trabajo esta ON-LINE...
        '
        If ImpresoraEstaONLINE(ObtenerImpresoraPredeterminada.Trim) = False Then
            title = "¿Impresora Apagada?"
            style = MsgBoxStyle.Critical Or
                MsgBoxStyle.OkOnly
            msg = "Por favor, compruebe su impresora." & vbCrLf
            msg &= "Se detectan " & wrProp_IMPRESORA.JobCountSinceLastReset & " trabajos Pendientes."
            response = MsgBox(msg, style, title)
            Exit Sub
        End If
        '
        Try
            Select Case iComando
                '
                ' Abrir Cajón
                '
                Case 0
                    Select Case iOpcion
                        Case "DOS"
                            '
                            ' Método Linea de Comandos .BAT + .TXT
                            '
                            Dim newProc As Process : Dim MiFileExist As Boolean
                            Dim FicheroDefecto = "C:\TRIVAGES\DATOS\AbreCajon.bat"
                            MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
                            If MiFileExist = False Then
                                MsgBox("No se ha podido leer el fichero " & FicheroDefecto,
                           MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                           "Revisar Fichero.")
                            Else
                                newProc = Process.Start(FicheroDefecto)
                                With newProc
                                    .StartInfo.Arguments = ""
                                End With
                            End If
                        Case "WINDOWS"
                            '
                            ' Impresion Directa A puerto: LPT1: / COM1:
                            '
                            Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
                            fh = CType(Win32API.CreateFile(iPuerto.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                       Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
                            Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
                            FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
                            SW = New StreamWriter(FS)
                            '
                            ' Apertura Cajón.
                            '
                            Try
                                SW.WriteLine(wrIMPRESORA.ABRECAJON.ToString.Trim)
                            Catch ex As Exception
                                MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    " *** Error de Comados a Impresora. *** ")
                            End Try
                            FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
                    End Select
                Case 1
                    '
                    ' Corte Papel
                    '
                    Select Case iOpcion
                        Case "DOS"
                            '
                            ' Método Linea de Comandos .BAT + .TXT
                            '
                            Dim newProc As Process : Dim MiFileExist As Boolean
                            Dim FicheroDefecto = "C:\TRIVAGES\DATOS\Cortar.bat"
                            MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
                            If MiFileExist = False Then
                                MsgBox("No se ha podido leer el fichero " & FicheroDefecto,
                           MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                           "Revisar Fichero.")
                            Else
                                newProc = Process.Start(FicheroDefecto)
                                With newProc
                                    .StartInfo.Arguments = ""
                                End With
                            End If
                        Case "WINDOWS"
                            '
                            ' Impresion Directa A puerto: LPT1: / COM1:
                            '
                            Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
                            fh = CType(Win32API.CreateFile(iPuerto.Trim, Win32API.GENERIC_WRITE, 0, 0,
                           Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
                            Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
                            FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
                            SW = New StreamWriter(FS)
                            '
                            ' Corte Papel.
                            '
                            Try
                                SW.WriteLine(wrIMPRESORA.CORTE.ToString.Trim)
                            Catch ex As Exception
                                MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    " *** Error de Comados a Impresora. *** ")
                            End Try

                            '
                            FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
                    End Select
            End Select
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " *** Error de Comados a Impresora. *** ")
        End Try
        '
    End Sub


    Public Function ImpresoraEstaONLINE(ImpreNombre As String) As Boolean
        '
        ' Pesimista / Optimista
        '
        ' printer("JobCountSinceLastReset").ToString() -> Trabajos Pendientes.
        ' printer("WorkOffline").ToString() > Encendida / Apagada.
        '
        ImpresoraEstaONLINE = False
        '
        Dim scope As New ManagementScope("\root\cimv2")
        scope.Connect()
        ' Select Printers from WMI Object Collections 
        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")
        Dim printerName As String = ""
        For Each printer As ManagementObject In searcher.[Get]()
            printerName = printer("Name").ToString().Trim
            '
            ' Para Chequear una IMPRESORA  en concreto:
            '
            With wrProp_IMPRESORA
                If printerName.Trim = ImpreNombre.Trim Then
                    If printer("WorkOffline").ToString().ToLower().Equals("false") Then
                        .WorkOffline = "False"
                        ImpresoraEstaONLINE = True
                    Else
                        .WorkOffline = "True"
                    End If
                    '
                    ' Aqui podemos recoger algunas de las Propiedades de la Impresora...
                    '
                    .JobCountSinceLastReset = printer("JobCountSinceLastReset").ToString()
                    '
                    Exit For
                End If
            End With
        Next
        '
    End Function

    Public Function DameConcatChr(wCadToSplit As String) As String
        '
        ' Obtenemos la Cocatenación de Códigos Chr desde una cadena 
        ' "/nn/nn ... /nn"
        '
        DameConcatChr = ""
        Dim words As String() = wCadToSplit.Split(New Char() {"/"c})
        For i = 0 To words.Length - 1
            If words(i).Trim.Length > 0 Then
                '
                '  Para Chr(n), Se deben evitar Valores mayor a 255
                '
                If words(i).Trim.Length > 2 And CInt(words(i)) > 255 Then
                    MensaESCPOSinvalido(DameConcatChr)
                    DameConcatChr = ""
                    Exit Function
                End If
                DameConcatChr &= Chr(CInt(words(i)))
            End If
        Next
        '
    End Function

    Public Function DameESCPOS_ConcatBarras(wCadToConcat As String) As String
        '
        ' ESC/POS a /nn/nn ... /nn
        ' Aqui llega desempaquetado, sin "["  "]"
        '
        DameESCPOS_ConcatBarras = "/"
        Dim CadESCPOS As String = wCadToConcat
        Dim l As Integer = CadESCPOS.Length - 1
        '
        For i = 0 To l
            If i = l Then
                DameESCPOS_ConcatBarras &= Asc(CadESCPOS(i))
            Else
                DameESCPOS_ConcatBarras &= Asc(CadESCPOS(i)) & "/"
            End If
        Next
        '
        ' Correccion para 10 Cpp, ya que al terminar con Chr(0)
        '  este caracter final no lo concatena.
        ' En ASCII CHR(0) == Caracter Null.
        '
        If DameESCPOS_ConcatBarras.Trim = "/27/33" Then
            DameESCPOS_ConcatBarras = "/27/33/0"
        End If
        '
    End Function

    Public Function ValidaESCPOS_Usr(sCadenaComprobar As String) As Boolean
        '
        ' Optimista->Pesimista
        ' Validación de cadenas ESC/POS con formato /nn/nn .... /nn
        '
        Dim wpos As Integer = 0 : ValidaESCPOS_Usr = True
        '
        ' La Cajas de Textos en este Caso Solo Filtran
        '    los digitos 0 ... 9 y Caracter "/".
        ' Obviamos por tanto comprobacion de que llegan 
        '  siempre valores Numéricos separados por "/"
        '
        If sCadenaComprobar.Trim.Length > 0 Then
            '
            ' Comprobación Primer Caracter = "/"
            '
            If sCadenaComprobar(0) <> "/" Then
                MensaESCPOSinvalido(sCadenaComprobar)
                ValidaESCPOS_Usr = False
                Exit Function
            End If
            '
            ' Comprobación Último Caracter <> "/"
            '
            If sCadenaComprobar(sCadenaComprobar.Trim.Length - 1) = "/" Then
                MensaESCPOSinvalido(sCadenaComprobar)
                ValidaESCPOS_Usr = False
                Exit Function
            End If
            '
            ' Comprobar existencia de al menos un caracter "/"
            ' Sinceramente creo que esto sobra, no obstante aqui se queda ...
            '
            wpos = InStr(sCadenaComprobar.Trim, "/")
            If wpos = 0 Then
                MensaESCPOSinvalido(sCadenaComprobar)
            End If
        Else
            '
            ' Cadena Vacía
            '
            MensaESCPOSinvalido(sCadenaComprobar)
            ValidaESCPOS_Usr = False
            Exit Function
        End If
        '
    End Function

    Private Sub MensaESCPOSinvalido(sCadErronea As String)
        '
        ' Cadena ESC/POS no Válida
        '
        msg = "Por favor entre Cadenas ESC/POS válidas. [/nn/nn ... /nn] " & vbCrLf
        msg &= " 0 >= nn =< 255 " & vbCrLf
        msg &= "Ejemplo.: /27/112/48/10/50 o /10 son válidos. " & vbCrLf
        msg &= "la cadena entrada " & sCadErronea & " parece no ser válida."
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
        title = "Dato(s) incorecto(s)"
        MsgBox(msg, style, title)
    End Sub

    Public Sub MENSAJESAImpresora(iArea As String)
        '
        ' Imprime UN MENSAJE en el AREA indicada.
        ' Cada Area indica el modelo de impresor a utilizar:
        '   [AREAS], pRow("MODELOIMPRE").ToString()
        ' En funcion de esto para Cada Modelo podremos tener distintos
        '  códigos ESC/POS si fuera necesario.
        '
        ' Evitar Mensajes en Blanco (Lineas Vacias)
        '
        With MyFrm18
            If .TextBoxMensaL1.Text.Trim.Length = 0 And .TextBoxMensaL2.Text.Trim.Length = 0 And
               .TextBoxMensaL3.Text.Trim.Length = 0 And .TextBoxMensaL4.Text.Trim.Length = 0 Then
                Exit Sub
            End If
        End With
        '
        If LeeArea(CInt(iArea)) Then
            '
            ' Impresion Directa Al puerto determinado Por AREA.
            '
            ' Debemos atender al MODELO de impresora de cada AREA
            '   para poder enviar los códigos ESC/POS correctos.
            ' Lo primero es Comprobar si es MODELO de Impresora esta creado o no
            '  en la Tabla [IMPRESORAS].
            ' Luego Comprobar si es MODELO de Impresora esta definido
            '  en la Tabla [AREAS].[MODELOIMPRE].
            '
            If wrLeeAREAS.MODELOIMPRE.Length > 0 And Not IsDBNull(wrLeeAREAS.MODELOIMPRE) Then
                '
                ' Modelo esta creado en IMPRESORAS?
                '
                If LeeDatosImpresora(wrLeeAREAS.MODELOIMPRE.Trim) = False Then
                    '
                    ' El MODELO de Impresora no esta creado en la Tabla
                    ' IMPRESORAS, y damos un Aviso
                    '
                    msg = "El MODELO de Impresora " & vbCrLf
                    msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                    msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
                    style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                    title = "Modelo de impresora no Creado."
                    MsgBox(msg, style, title)
                    Exit Sub
                End If
            Else
                msg = "El MODELO de Impresora " & vbCrLf
                msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                msg = "para el Area " & vbCrLf
                msg &= iArea.Trim & vbCrLf
                msg &= "no está Definido en la tabla [AREAS].[MODELOIMPRE] " & vbCrLf
                style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                title = "Modelo de impresora no Definido."
                MsgBox(msg, style, title)
                Exit Sub
            End If
            '
            Dim swPlato As Boolean = False
            Dim MyPlato As String = "1"
            '
            ' Impresion MENSAJES Directa / Windows
            '
            If IsDBNull(wrLeeAREAS.TIPOIMPRESION) Or wrLeeAREAS.TIPOIMPRESION.Trim = "False" Then
                '
                ' Directa
                '
                Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
                fh = CType(Win32API.CreateFile(wrLeeAREAS.PUERTOIMPRE.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                       Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
                Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
                FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
                SW = New StreamWriter(FS)
                '
                ' *** MENSAJE ***
                '
                Try
                    Dim LineaImpre As String = ""
                    '
                    With wrIMPRESORA
                        SW.WriteLine(.PROPORCIONAL)
                        SW.WriteLine("Mensajes a.: " & wrLeeAREAS.DESCRIPCION.Trim)
                        SW.WriteLine(.AVAZALINEA)
                        SW.WriteLine(.DOBLEANCHO)
                        '
                        ' Mensaje ...
                        '
                        If MyFrm18.TextBoxMensaL1.Text.Trim.Length > 0 Then
                            SW.WriteLine(MyFrm18.TextBoxMensaL1.Text.Trim)
                        End If
                        If MyFrm18.TextBoxMensaL2.Text.Trim.Length > 0 Then
                            SW.WriteLine(MyFrm18.TextBoxMensaL2.Text.Trim)
                        End If
                        If MyFrm18.TextBoxMensaL3.Text.Trim.Length > 0 Then
                            SW.WriteLine(MyFrm18.TextBoxMensaL3.Text.Trim)
                        End If
                        If MyFrm18.TextBoxMensaL4.Text.Trim.Length > 0 Then
                            SW.WriteLine(MyFrm18.TextBoxMensaL4.Text.Trim)
                        End If
                        SW.WriteLine(.PROPORCIONAL)
                        '
                        ' Avanza N lineas, determinadas en Ref. Generales
                        '
                        LeeTCONA4Cfg("General")
                        '
                        ' Lineas <= 9
                        ' Por si se diera el caso, no derrochar MUCHO Papel ...
                        '
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                            If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                                wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                            End If
                            For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                                SW.WriteLine(.AVAZALINEA)
                            Next
                        Else
                            SW.WriteLine(.AVAZALINEA) ' Por defecto al menos 1 Linea de salto
                        End If
                        '
                        ' Corte PAPEL
                        '
                        SW.WriteLine(.CORTE)
                    End With
                    '
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    "MENSAJES, Error de Impresora, AREA: " & iArea)
                End Try
                FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
            Else
                '
                ' Windows > Cobview
                '
                Dim MiCadenaWIEW As String = ""
                Dim NombreTXT As String = "TKMENSAJES" & iArea.Trim & ".TXT"
                Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\" & NombreTXT, True, Text.Encoding.Default)
                    '
                    ' *** TICKET ***
                    ' Previsualizacion en COBVIEW, ahora determinado por Ref. Generales
                    '
                    If wrLeeTCONA4.Tcona4_COBVIEWPDSN = "True" Then
                        sw.WriteLine("<SET PRINTDIRECT='NO'>")
                    Else
                        sw.WriteLine("<SET PRINTDIRECT='YES'>")
                    End If
                    '
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKMENSAJES.def'>")
                    '
                    Try
                        Dim LineaImpre As String = ""
                        '
                        sw.WriteLine("<font face='Consolas' size='14'>")
                        '

                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW &= "Mensajes a.: " & wrLeeAREAS.DESCRIPCION.Trim
                        sw.WriteLine(MiCadenaWIEW)
                        sw.WriteLine("<BR>")
                        '
                        ' Mensaje ...
                        '
                        If MyFrm18.TextBoxMensaL1.Text.Trim.Length > 0 Then
                            MiCadenaWIEW = "<COL #1>"
                            MiCadenaWIEW &= MyFrm18.TextBoxMensaL1.Text.Trim
                            sw.WriteLine(MiCadenaWIEW)
                        End If
                        If MyFrm18.TextBoxMensaL2.Text.Trim.Length > 0 Then
                            MiCadenaWIEW = "<COL #1>"
                            MiCadenaWIEW &= MyFrm18.TextBoxMensaL2.Text.Trim
                            sw.WriteLine(MiCadenaWIEW)
                        End If
                        If MyFrm18.TextBoxMensaL3.Text.Trim.Length > 0 Then
                            MiCadenaWIEW = "<COL #1>"
                            MiCadenaWIEW &= MyFrm18.TextBoxMensaL3.Text.Trim
                            sw.WriteLine(MiCadenaWIEW)
                        End If
                        If MyFrm18.TextBoxMensaL4.Text.Trim.Length > 0 Then
                            MiCadenaWIEW = "<COL #1>"
                            MiCadenaWIEW &= MyFrm18.TextBoxMensaL4.Text.Trim
                            sw.WriteLine(MiCadenaWIEW)
                        End If
                        '
                        ' Avanza N lineas, determinadas en Ref. Generales
                        '
                        LeeTCONA4Cfg("General")
                        '
                        ' Por si se diera el caso, no derrochar MUCHO Papel ...
                        '
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                            If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                                wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                            End If
                            For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                                sw.WriteLine("<BR>")
                            Next
                        Else
                            sw.WriteLine("<BR>") ' Por defecto al menos 1 Linea de salto
                        End If
                        '
                    Catch ex As Exception
                        MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    "TICKET.: Error de Impresora, AREA: " & iArea)
                    End Try
                    sw.Flush()
                    sw.Close()
                End Using
                '
                ' Llamada a COBVIEW pasandole como parámetro el informe generado...
                '
                Dim myProcess As New Process()
                Dim StringArguments As String
                Try
                    StringArguments = ""
                    myProcess.StartInfo.UseShellExecute = True
                    myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
                    StringArguments = "C:\TRIVAGES\InformesCobview\" & NombreTXT
                    myProcess.StartInfo.Arguments = StringArguments
                    myProcess.Start()
                Catch Mye As Exception
                    MsgBox("ERROR: " & Mye.Source & vbCrLf & Mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: COBVIEW.")
                End Try
            End If
        End If
        '
    End Sub

    Public Sub MENSAJESATerminarMESA(iArea As String, iMensaje As String)
        '
        ' Imprime UN MENSAJE "Terminar MESA" en el AREA indicada.
        '
        If LeeArea(CInt(iArea)) Then
            '
            ' Impresion Directa Al puerto determinado Por AREA.
            '
            ' Debemos atender al MODELO de impresora de cada AREA
            '   para poder enviar los códigos ESC/POS correctos.
            ' Lo primero es Comprobar si es MODELO de Impresora esta creado o no
            '  en la Tabla [IMPRESORAS].
            ' Luego Comprobar si es MODELO de Impresora esta definido
            '  en la Tabla [AREAS].[MODELOIMPRE].
            '
            If wrLeeAREAS.MODELOIMPRE.Length > 0 And Not IsDBNull(wrLeeAREAS.MODELOIMPRE) Then
                '
                ' Modelo esta creado en IMPRESORAS?
                '
                If LeeDatosImpresora(wrLeeAREAS.MODELOIMPRE.Trim) = False Then
                    '
                    ' El MODELO de Impresora no esta creado en la Tabla
                    ' IMPRESORAS, y damos un Aviso
                    '
                    msg = "El MODELO de Impresora " & vbCrLf
                    msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                    msg &= "no está Creado en la tabla [IMPRESORAS]" & vbCrLf
                    style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                    title = "Modelo de impresora no Creado."
                    MsgBox(msg, style, title)
                    Exit Sub
                End If
            Else
                msg = "El MODELO de Impresora " & vbCrLf
                msg &= wrLeeAREAS.MODELOIMPRE.Trim & vbCrLf
                msg = "para el Area " & vbCrLf
                msg &= iArea.Trim & vbCrLf
                msg &= "no está Definido en la tabla [AREAS].[MODELOIMPRE] " & vbCrLf
                style = MsgBoxStyle.Information Or
                MsgBoxStyle.OkOnly
                title = "Modelo de impresora no Definido."
                MsgBox(msg, style, title)
                Exit Sub
            End If
            '
            ' Directa // Windows
            '
            If IsDBNull(wrLeeAREAS.TIPOIMPRESION) Or wrLeeAREAS.TIPOIMPRESION.Trim = "False" Then
                ' Directa
                Dim fh As IntPtr : Dim SW As StreamWriter : Dim FS As FileStream
                fh = CType(Win32API.CreateFile(wrLeeAREAS.PUERTOIMPRE.Trim, Win32API.GENERIC_WRITE, 0, 0,
                                           Win32API.CREATE_ALWAYS, 0, 0), IntPtr)
                Dim sfh As New Microsoft.Win32.SafeHandles.SafeFileHandle(fh, True)
                FS = New FileStream(sfh, FileAccess.Write) : FS.Flush()
                SW = New StreamWriter(FS)
                '
                ' *** MENSAJE ***
                '
                Try
                    Dim LineaImpre As String = ""
                    '
                    With wrIMPRESORA
                        SW.WriteLine(.PROPORCIONAL)
                        SW.WriteLine("Mensajes a.: " & wrLeeAREAS.DESCRIPCION.Trim)
                        SW.WriteLine(.AVAZALINEA)
                        SW.WriteLine(.DOBLEANCHO)
                        '
                        ' Mensaje ...
                        '
                        If iMensaje.Trim.Length > 0 Then
                            SW.WriteLine(iMensaje.Trim)
                        End If
                        SW.WriteLine(.PROPORCIONAL)
                        '
                        ' Avanza N lineas, determinadas en Ref. Generales
                        '
                        LeeTCONA4Cfg("General")
                        '
                        ' Lineas <= 9
                        ' Por si se diera el caso, no derrochar MUCHO Papel ...
                        '
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                            If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                                wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                            End If
                            For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                                SW.WriteLine(.AVAZALINEA)
                            Next
                        Else
                            SW.WriteLine(.AVAZALINEA) ' Por defecto al menos 1 Linea de salto
                        End If
                        '
                        ' Corte PAPEL
                        '
                        SW.WriteLine(.CORTE)
                    End With
                    '
                Catch ex As Exception
                    MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                        MsgBoxStyle.Exclamation Or
                                                        MsgBoxStyle.OkOnly,
                                                        "MENSAJES, Error de Impresora, AREA: " & iArea)
                End Try
                FS.Flush() : SW.Close() : FS.Close() : sfh.Close()
            Else
                '
                ' Windows > Cobview
                '
                Dim MiCadenaWIEW As String = ""
                Dim NombreTXT As String = "TKMENSAJES" & iArea.Trim & ".TXT"
                Using sw As StreamWriter = New StreamWriter("C:\TRIVAGES\InformesCobview\" & NombreTXT, True, Text.Encoding.Default)
                    '
                    ' *** TICKET ***
                    ' Previsualizacion en COBVIEW, ahora determinado por Ref. Generales
                    '
                    If wrLeeTCONA4.Tcona4_COBVIEWPDSN = "True" Then
                        sw.WriteLine("<SET PRINTDIRECT='NO'>")
                    Else
                        sw.WriteLine("<SET PRINTDIRECT='YES'>")
                    End If
                    '
                    sw.WriteLine("<include 'C:\TRIVAGES\InformesCobview\TKMENSAJES.def'>")
                    '
                    Try
                        Dim LineaImpre As String = ""
                        '
                        sw.WriteLine("<font face='Consolas' size='14'>")
                        '
                        MiCadenaWIEW = "<COL #1>"
                        MiCadenaWIEW &= "Mensajes a.: " & wrLeeAREAS.DESCRIPCION.Trim
                        sw.WriteLine(MiCadenaWIEW)
                        sw.WriteLine("<BR>")
                        '
                        ' Mensaje ...
                        '
                        If iMensaje.Trim.Length > 0 Then
                            MiCadenaWIEW = "<COL #1>"
                            MiCadenaWIEW &= iMensaje.Trim
                            sw.WriteLine(MiCadenaWIEW)
                        End If
                        '
                        ' Avanza N lineas, determinadas en Ref. Generales
                        '
                        LeeTCONA4Cfg("General")
                        '
                        ' Por si se diera el caso, no derrochar MUCHO Papel ...
                        '
                        If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 0 Then
                            If wrLeeTCONA4.Tcona4_SALTOLINPIETK > 9 Then
                                wrLeeTCONA4.Tcona4_SALTOLINPIETK = 9
                            End If
                            For i As Integer = 0 To wrLeeTCONA4.Tcona4_SALTOLINPIETK
                                sw.WriteLine("<BR>")
                            Next
                        Else
                            sw.WriteLine("<BR>") ' Por defecto al menos 1 Linea de salto
                        End If
                        '
                    Catch ex As Exception
                        MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                                    MsgBoxStyle.Exclamation Or
                                                    MsgBoxStyle.OkOnly,
                                                    "TICKET.: Error de Impresora, AREA: " & iArea)
                    End Try
                    sw.Flush()
                    sw.Close()
                End Using
                '
                ' Llamada a COBVIEW pasandole como parámetro el informe generado...
                '
                Dim myProcess As New Process()
                Dim StringArguments As String
                Try
                    StringArguments = ""
                    myProcess.StartInfo.UseShellExecute = True
                    myProcess.StartInfo.FileName = "C:\TRIVAGES\COBVIEW"
                    StringArguments = "C:\TRIVAGES\InformesCobview\" & NombreTXT
                    myProcess.StartInfo.Arguments = StringArguments
                    myProcess.Start()
                Catch Mye As Exception
                    MsgBox("ERROR: " & Mye.Source & vbCrLf & Mye.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                " Llamada a Librería: COBVIEW.")
                End Try
            End If
        End If
        '
    End Sub

    Public Sub GeneraTXTDATOSTICKETS()
        '-----------------------------------------------------------------------
        ' Exporta Los Datos de Cabecera y Pie TICKETS.
        '-----------------------------------------------------------------------
        Dim FicheroDefecto As String = "" : Dim MiFileExist As Boolean
        '
        FicheroDefecto = "C:\TRIVAGES\DATOS\DatosPieCabTKFAC.TXT"
        Try
            Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
            '
            ' Datos a Fichero de texto.
            '
            With MyFrm6
                '
                ' Cabecera TICKETS FACTURA
                '
                sw.WriteLine("LC01>" & .TextBoxLinCab1.Text)
                sw.WriteLine("LC02>" & .TextBoxLinCab2.Text)
                sw.WriteLine("LC03>" & .TextBoxLinCab3.Text)
                sw.WriteLine("LC04>" & .TextBoxLinCab4.Text)
                sw.WriteLine("LC05>" & .TextBoxLinCab5.Text)
                sw.WriteLine("LC06>" & .TextBoxLinCab6.Text)
                sw.WriteLine("LC07>" & .TextBoxLinCab7.Text)
                sw.WriteLine("LC08>" & .TextBoxLinCab8.Text)
                sw.WriteLine("LC09>" & .TextBoxLinCab9.Text)
                sw.WriteLine("LC10>" & .TextBoxLinCab10.Text)
                '
                ' Detalle TICKETS Factura.
                '
                sw.WriteLine("DT01>" & .TextBoxDetCab1.Text)
                sw.WriteLine("DT02>" & .TextBoxDetCab2.Text)
                sw.WriteLine("DT03>" & .TextBoxDetCab3.Text)
                '
                ' Detalle TICKETS X, Z.
                '
                sw.WriteLine("DT04>" & .TextBoxDetCab4.Text)
                sw.WriteLine("DT05>" & .TextBoxDetCab5.Text)
                sw.WriteLine("DT06>" & .TextBoxDetCab6.Text)
                '
                ' PIE Ticket FACTURA
                '
                sw.WriteLine("PI01>" & .TextBoxLinPie1.Text)
                sw.WriteLine("PI02>" & .TextBoxLinPie2.Text)
                sw.WriteLine("PI03>" & .TextBoxLinPie3.Text)
                sw.WriteLine("PI04>" & .TextBoxLinPie4.Text)
                sw.WriteLine("PI05>" & .TextBoxLinPie5.Text)
                sw.WriteLine("PI06>" & .TextBoxLinPie6.Text)
                sw.WriteLine("PI07>" & .TextBoxLinPie7.Text)
                sw.WriteLine("PI08>" & .TextBoxLinPie8.Text)
                sw.WriteLine("PI09>" & .TextBoxLinPie9.Text)
                sw.WriteLine("PI10>" & .TextBoxLinPie10.Text)
                sw.WriteLine("PI11>" & .TextBoxLinPie11.Text)
                sw.WriteLine("PI12>" & .TextBoxLinPie12.Text)
                sw.WriteLine("PI13>" & .TextBoxLinPie13.Text)
                sw.WriteLine("PI14>" & .TextBoxLinPie14.Text)
                sw.WriteLine("PI15>" & .TextBoxLinPie15.Text)
                sw.WriteLine("PI16>" & .TextBoxLinPie16.Text)
                sw.WriteLine("PI17>" & .TextBoxLinPie17.Text)
                sw.WriteLine("PI18>" & .TextBoxLinPie18.Text)
                sw.WriteLine("PI18>" & .TextBoxLinPie19.Text)
                sw.WriteLine("PI20>" & .TextBoxLinPie20.Text)
            End With
            '
            MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
            If MiFileExist = True Then
                MsgBox("Se ha Creado el fichero " & FicheroDefecto,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Information,
                   "Datos Exportados.")
            Else
                MsgBox("No se ha podido Escribir en el fichero " & FicheroDefecto,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
            End If
            '
            sw.Flush()
            sw.Close()
        Catch MyE As Exception
            MsgBox("No se ha podido Escribir en el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
        End Try
        '
    End Sub

    Public Sub CargaTXTDATOSTICKETS()
        '
        ' Comprobamos si existe "C:\TRIVAGES\DATOS\DatosPieCabTKFAC.TXT"
        '  y cargamos sus datos...
        '
        Dim FicheroDefecto As String = "C:\TRIVAGES\DATOS\DatosPieCabTKFAC.TXT"
        '
        '
        Dim MiFileExist As Boolean
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Error al importar Datos.")
            Exit Sub
        End If
        '
        Try
            Dim sr As StreamReader = New StreamReader(FicheroDefecto)
            Dim line, CaracterPrimero As String
            Do
                line = sr.ReadLine()
                CaracterPrimero = Mid$(line, 1, 5)
                With MyFrm6
                    Select Case CaracterPrimero
                    '
                    ' La estructura del archivo plano .txt
                    '  esta pensada para recoger mas de un parámetro si se necesita...
                    '
                        Case "LC01>"
                            '
                            ' Cabecera TICKETS FACTURA
                            '
                            .TextBoxLinCab1.Text = Mid$(line, 6, Len(line))
                        Case "LC02>"
                            .TextBoxLinCab2.Text = Mid$(line, 6, Len(line))
                        Case "LC03>"
                            .TextBoxLinCab3.Text = Mid$(line, 6, Len(line))
                        Case "LC04>"
                            .TextBoxLinCab4.Text = Mid$(line, 6, Len(line))
                        Case "LC05>"
                            .TextBoxLinCab5.Text = Mid$(line, 6, Len(line))
                        Case "LC06>"
                            .TextBoxLinCab6.Text = Mid$(line, 6, Len(line))
                        Case "LC07>"
                            .TextBoxLinCab7.Text = Mid$(line, 6, Len(line))
                        Case "LC08>"
                            .TextBoxLinCab8.Text = Mid$(line, 6, Len(line))
                        Case "LC09>"
                            .TextBoxLinCab9.Text = Mid$(line, 6, Len(line))
                        Case "LC10>"
                            .TextBoxLinCab10.Text = Mid$(line, 6, Len(line))
                        Case "DT01>"
                            .TextBoxDetCab1.Text = Mid$(line, 6, Len(line))
                            '
                            ' Detalle TICKETS Factura.
                            '
                        Case "DT02>"
                            .TextBoxDetCab2.Text = Mid$(line, 6, Len(line))
                        Case "DT03>"
                            .TextBoxDetCab3.Text = Mid$(line, 6, Len(line))
                        Case "DT04>"
                            '
                            ' Detalle TICKETS X, Z.
                            '
                            .TextBoxDetCab4.Text = Mid$(line, 6, Len(line))
                        Case "DT05>"
                            .TextBoxDetCab5.Text = Mid$(line, 6, Len(line))
                        Case "DT06>"
                            .TextBoxDetCab6.Text = Mid$(line, 6, Len(line))
                        Case "PI01>"
                            '
                            ' PIE TICKETS Factura
                            '
                            .TextBoxLinPie1.Text = Mid$(line, 6, Len(line))
                        Case "PI02>"
                            .TextBoxLinPie2.Text = Mid$(line, 6, Len(line))
                        Case "PI03>"
                            .TextBoxLinPie3.Text = Mid$(line, 6, Len(line))
                        Case "PI04>"
                            .TextBoxLinPie4.Text = Mid$(line, 6, Len(line))
                        Case "PI05>"
                            .TextBoxLinPie5.Text = Mid$(line, 6, Len(line))
                        Case "PI06>"
                            .TextBoxLinPie6.Text = Mid$(line, 6, Len(line))
                        Case "PI07>"
                            .TextBoxLinPie7.Text = Mid$(line, 6, Len(line))
                        Case "PI08>"
                            .TextBoxLinPie8.Text = Mid$(line, 6, Len(line))
                        Case "PI09>"
                            .TextBoxLinPie9.Text = Mid$(line, 6, Len(line))
                        Case "PI10>"
                            .TextBoxLinPie10.Text = Mid$(line, 6, Len(line))

                        Case "PI11>"
                            .TextBoxLinPie11.Text = Mid$(line, 6, Len(line))
                        Case "PI12>"
                            .TextBoxLinPie12.Text = Mid$(line, 6, Len(line))
                        Case "PI13>"
                            .TextBoxLinPie13.Text = Mid$(line, 6, Len(line))
                        Case "PI14>"
                            .TextBoxLinPie14.Text = Mid$(line, 6, Len(line))
                        Case "PI15>"
                            .TextBoxLinPie15.Text = Mid$(line, 6, Len(line))
                        Case "PI16>"
                            .TextBoxLinPie16.Text = Mid$(line, 6, Len(line))
                        Case "PI17>"
                            .TextBoxLinPie17.Text = Mid$(line, 6, Len(line))
                        Case "PI18>"
                            .TextBoxLinPie18.Text = Mid$(line, 6, Len(line))
                        Case "PI19>"
                            .TextBoxLinPie19.Text = Mid$(line, 6, Len(line))
                        Case "PI20>"
                            .TextBoxLinPie20.Text = Mid$(line, 6, Len(line))
                    End Select
                End With
            Loop Until line = "END" Or line = Nothing Or sr.EndOfStream
            sr.Close()
        Catch MyE As Exception
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "C:\TRIVAGES\DATOS\DatosPieCabTKFAC.TXT")
        End Try
        '
    End Sub

    Public Function SuperReplace(ByRef field As String, ByVal ReplaceString As String) As String
        '
        ' Reemplazar (Evitar Caracteres no Permitidos en Nombre De Ficheros)
        '
        ' field: 
        '    -> Cadena en la que reemplazar los caracteres indicados en ReplaceArray()
        ' ReplaceString: 
        '    -> Caracter por el que serán reemplazados
        ' Ejemplo de Uso: 
        '    Dim QuitaCaracteres As String = SuperReplace(wNomFicModelo, "_")
        '    wNomFicModelo = QuitaCaracteres.Trim
        '
        Dim ReplaceArray As String() = {"\", "/", ":", "*", "?", "““", "<", ">", "."}
        '
        Dim i As Integer
        For i = LBound(ReplaceArray) To UBound(ReplaceArray)
            field = Replace(field, ReplaceArray(i), ReplaceString)
        Next i
        SuperReplace = field
    End Function

    Public Sub GeneraTXTMODELOIMPRE(wNomFicModelo As String)
        '-----------------------------------------------------------------------
        ' Exporta Los Datos de Modelos de Impresora.
        '-----------------------------------------------------------------------
        Dim FicheroDefecto As String = "" : Dim MiFileExist As Boolean
        '
        ' Windows no acepta estos símbolos en el nombre del archivo.:
        ' la barra invertida (\), la barra (/), 
        ' los dos puntos (:), el asterisco (*), el interrogante (?), 
        ' las comillas(“), menor (<), mayor (>) o la barra vertical (|)
        '
        Dim QuitaCaracteres As String = SuperReplace(wNomFicModelo, "_")
        wNomFicModelo = QuitaCaracteres.Trim
        '
        FicheroDefecto = "C:\TRIVAGES\DATOS\" & wNomFicModelo & ".TXT"
        Try
            Dim sw As StreamWriter = File.CreateText(FicheroDefecto)
            '
            ' Datos a Fichero de texto.
            '
            With MyFrm17
                '
                ' Códigos ESC/POS
                '
                sw.WriteLine("LE00>" & wNomFicModelo.Trim)
                sw.WriteLine("LE01>" & .TextCorteUSR.Text)
                sw.WriteLine("LE02>" & .TextCajonUSR.Text)
                sw.WriteLine("LE03>" & .Text10Cpp.Text)
                sw.WriteLine("LE04>" & .Text12Cpp.Text)
                sw.WriteLine("LE05>" & .TextProporcional.Text)
                sw.WriteLine("LE06>" & .TextCompri.Text)
                sw.WriteLine("LE07>" & .TextNegrita.Text)
                sw.WriteLine("LE08>" & .TextCursiva.Text)
                sw.WriteLine("LE09>" & .TextDblAlto.Text)
                sw.WriteLine("LE10>" & .TextDblAlto12Cpp.Text)
                sw.WriteLine("LE11>" & .TextDblAncho.Text)
                sw.WriteLine("LE12>" & .TextAvance.Text)
            End With
            '
            MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
            If MiFileExist = True Then
                MsgBox("Se ha Creado el fichero " & FicheroDefecto,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Information,
                   "Datos Exportados.")
            Else
                MsgBox("No se ha podido Escribir en el fichero " & FicheroDefecto,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
            End If
            '
            sw.Flush()
            sw.Close()
        Catch MyE As Exception
            MsgBox("No se ha podido Escribir en el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Revisar " & FicheroDefecto)
        End Try
        '
    End Sub

    Public Sub CargaTXTMODELOIMPRE(wNomFicModelo As String)
        '
        ' Comprobamos si existe wNomFicModelo y cargamos sus datos.
        ' wNomFicModelo Ej.: C:\TRIVAGES\DATOS\AVPOS TC20.TXT
        '
        Dim FicheroDefecto As String = wNomFicModelo.Trim
        '
        Dim MiFileExist As Boolean
        MiFileExist = My.Computer.FileSystem.FileExists(FicheroDefecto)
        If MiFileExist = False Then
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "Error al importar Datos.")
            Exit Sub
        End If
        '
        Try
            Dim sr As StreamReader = New StreamReader(FicheroDefecto)
            Dim line, CaracterPrimero As String
            Do
                line = sr.ReadLine()
                CaracterPrimero = Mid$(line, 1, 5)
                With MyFrm17
                    Select Case CaracterPrimero
                    '
                    ' La estructura del archivo plano .txt
                    '  esta pensada para recoger mas de un parámetro si se necesita...
                    '
                            '
                            ' Nombre del Modelo
                            '
                        Case "LE00>"
                            .TextBoxNomModelo.Text = Mid$(line, 6, Len(line))
                            '
                            ' Cadenas ESC/POS
                            '
                        Case "LE01>"
                            .TextCorteUSR.Text = Mid$(line, 6, Len(line))
                        Case "LE02>"
                            .TextCajonUSR.Text = Mid$(line, 6, Len(line))
                        Case "LE03>"
                            .Text10Cpp.Text = Mid$(line, 6, Len(line))
                        Case "LE04>"
                            .Text12Cpp.Text = Mid$(line, 6, Len(line))
                        Case "LE05>"
                            .TextProporcional.Text = Mid$(line, 6, Len(line))
                        Case "LE06>"
                            .TextCompri.Text = Mid$(line, 6, Len(line))
                        Case "LE07>"
                            .TextNegrita.Text = Mid$(line, 6, Len(line))
                        Case "LE08>"
                            .TextCursiva.Text = Mid$(line, 6, Len(line))
                        Case "LE09>"
                            .TextDblAlto.Text = Mid$(line, 6, Len(line))
                        Case "LE10>"
                            .TextDblAlto12Cpp.Text = Mid$(line, 6, Len(line))
                        Case "LE11>"
                            .TextDblAncho.Text = Mid$(line, 6, Len(line))
                        Case "LE12>"
                            .TextAvance.Text = Mid$(line, 6, Len(line))
                    End Select
                End With
            Loop Until line = "END" Or line = Nothing Or sr.EndOfStream
            sr.Close()
        Catch MyE As Exception
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "C:\TRIVAGES\DATOS\" & wNomFicModelo.Trim & ".TXT")
        End Try
        '
    End Sub



    Public Sub CargaTablas(Server As String, MiDb As String)
        '
        '   Tablas de la base de datos seleccionada...
        '
        Dim CadenaConexion As String
        Dim SelectSQL As String
        '
        CadenaConexion = Server & "Initial Catalog=" & MiDb &
                                      ";Integrated Security=True;
                                         Connect Timeout=15;
                                         Encrypt=False;
                                         TrustServerCertificate=True;
                                         ApplicationIntent=ReadWrite;
                                         MultiSubnetFailover=False"
        '
        MyFrm6.ListBoxTablasDB.Visible = False
        MyFrm6.ListBoxTablasDB.Items.Clear()
        MyFrm6.ListBoxCampos.Items.Clear()
        MyFrm6.ListBoxPropCampos.Items.Clear()
        '
        Using con As New SqlConnection(CadenaConexion)
            con.Open()
            SelectSQL = "select name from sysobjects where type='U' Order by name;"
            Dim Com As SqlCommand = New SqlCommand(SelectSQL, con)
            Dim dr As SqlDataReader = Com.ExecuteReader
            '
            While (dr.Read())
                MyFrm6.ListBoxTablasDB.Items.Add(dr(0).ToString)
            End While
            '
            con.Close()
        End Using
        MyFrm6.ListBoxTablasDB.Visible = True
        '
    End Sub

    Public Sub CargaCamposTabla(Server As String, MiDb As String, NombrteTabla As String)
        '
        '   Campos de la tabla seleccionada...
        '
        Dim CadenaConexion As String
        Dim SelectSQL As String
        '
        CadenaConexion = Server & "Initial Catalog=" & MiDb &
                                      ";Integrated Security=True;
                                         Connect Timeout=15;
                                         Encrypt=False;
                                         TrustServerCertificate=True;
                                         ApplicationIntent=ReadWrite;
                                         MultiSubnetFailover=False"
        '
        MyFrm6.ListBoxCampos.Visible = False
        MyFrm6.ListBoxCampos.Items.Clear()
        MyFrm6.ListBoxPropCampos.Items.Clear()
        '
        Using con As New SqlConnection(CadenaConexion)
            con.Open()
            SelectSQL = "SELECT COLUMN_NAME FROM Information_Schema.Columns WHERE TABLE_NAME = '" &
                         NombrteTabla & "'" &
                        " ORDER BY COLUMN_NAME;"
            Dim Com As SqlCommand = New SqlCommand(SelectSQL, con)
            Dim dr As SqlDataReader = Com.ExecuteReader
            '
            While (dr.Read())
                MyFrm6.ListBoxCampos.Items.Add(dr(0).ToString)
            End While
            '
            con.Close()
        End Using
        MyFrm6.ListBoxCampos.Visible = True
        '
    End Sub

    Public Function CompruebaNombreDB(MiDb As String) As Boolean
        '
        ' Lista de Objetos DataBase.
        ' Comprobar existencia de un objeto database por su nombre. 
        '
        Dim CadenaConexion As String = "" : Dim SelectSQL As String = ""
        '
        CompruebaNombreDB = False
        '
        CadenaConexion = SQL_Instancia & "Initial Catalog=master" &
                                         ";Integrated Security=True;
                                         Connect Timeout=15;
                                         Encrypt=False;
                                         TrustServerCertificate=True;
                                         ApplicationIntent=ReadWrite;
                                         MultiSubnetFailover=False"
        '
        Using con As New SqlConnection(CadenaConexion)
            con.Open()
            SelectSQL = "SELECT name FROM master.dbo.sysdatabases "
            SelectSQL &= "WHERE name ='" & MiDb.Trim & "'"
            Dim Com As SqlCommand = New SqlCommand(SelectSQL, con)
            Dim dr As SqlDataReader = Com.ExecuteReader
            '
            While (dr.Read())
                If dr(0).ToString.Trim = MiDb Then
                    CompruebaNombreDB = True
                End If
            End While
            '
            con.Close()
        End Using
        '
    End Function

    Public Sub InfoCampos(Server As String, MiDb As String, NombrteTabla As String, NombreCampo As String)
        '
        '   Propiedades del CAMPO Seleccionado
        '
        Try
            Using cn As New SqlConnection()
                cn.ConnectionString = Server & "Initial Catalog=" & MiDb &
                                      ";Integrated Security=True;
                                         Connect Timeout=15;
                                         Encrypt=False;
                                         TrustServerCertificate=True;
                                         ApplicationIntent=ReadWrite;
                                         MultiSubnetFailover=False"

                cn.Open()
                Dim comando As New SqlCommand()
                comando.Connection = cn
                comando.CommandText = "SELECT * FROM " & "[" & NombrteTabla & "]"
                '
                Using SqlDataReader As SqlDataReader = comando.ExecuteReader()
                    Dim esquema As DataTable = SqlDataReader.GetSchemaTable()
                    MyFrm6.ListBoxPropCampos.Visible = False
                    MyFrm6.ListBoxPropCampos.Items.Clear()
                    For Each campo As DataRow In esquema.Rows
                        If campo(0).ToString() = NombreCampo Then
                            MyFrm6.ListBoxPropCampos.Items.Add("Columna: " & campo(0).ToString())
                            MyFrm6.ListBoxPropCampos.Items.Add("-------------------------------")
                            For Each propiedad As DataColumn In esquema.Columns
                                MyFrm6.ListBoxPropCampos.Items.Add("    " & propiedad.ColumnName & " = " &
                                                   campo(propiedad).ToString)
                            Next
                            MyFrm6.ListBoxPropCampos.Items.Add("")
                        End If
                    Next
                    MyFrm6.ListBoxPropCampos.Visible = True
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical)
        End Try
        '
    End Sub

    Public Function compruebaPWDS(MiPwdEntrada As String) As Boolean
        '
        ' Aqui Validamos si las distintas Passwords entradas
        ' son válidas o no y determina el NIVEL 
        ' de acceso otorgado.
        '
        ' Se Apoya en la TABLA [CLAVES]
        ' El nivel de acceso lo define el VENDEDOR actual.
        '-----------------------------------------------------
        ' 5 >  - "SUPER" y Programador        NIVEL ALTO
        ' 4, 3 - "EL JEFE" y "ENCARGADO"      NIVEL MEDIO
        ' < 3  - RESTO.: (USRn)               NIVEL BAJO
        '-----------------------------------------------------
        ' Pesismista -> Optimista
        '
        compruebaPWDS = False
        '
        ' Comprobamos Claves NIVEL Programadores/TRIVALLE.
        ' Si hay Coincidencia se entiende NIVEL 5.
        '
        If MiPwdEntrada = PassTRIVALLE(0) Or MiPwdEntrada = PassTRIVALLE(1) Then
            compruebaPWDS = True
            wrLeeCODNOM.NIVELACCESO = 5
            Exit Function
        End If
        '
        ' Comprobamos Clave segun NIVEL de ACCESO del Vendedor, Tabla [CLAVES]
        '
        If LeeVendedor(CInt(MyFrm1.TextBoxOPC1.Text.Trim)) = True Then
            LeeClaves(wrLeeCODNOM.NIVELACCESO)
            If wrCLAVES.CLAVE.Trim = MiPwdEntrada.Trim Then
                compruebaPWDS = True
            Else
                compruebaPWDS = False
            End If
        Else
            wrLeeCODNOM.NIVELACCESO = 0
            wrCLAVES.DESCRIPCION = "**NO LEIDO**"
            wrCLAVES.CLAVE = ""
            wrCLAVES.ACCESOX = "False"
            wrCLAVES.ACCESOZ = "False"
            wrCLAVES.ACCESOAPPS = "False"
            wrCLAVES.ACCESOREFGEN = ""
        End If
        '
    End Function

    Public Function LeeClaves(wNivel As Integer) As Boolean
        '
        ' Leemos CLAVES por NIVEL de acceso.
        ' Determina que ACCESOS/PERMISOS se otorgan a una entidad.
        '
        LeeClaves = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [CLAVES] "
        queryString = queryString & "WHERE [CLAVES].[NIVELACCESO]=" & wNivel & " "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "CLAVES")
            '
            If dt.Tables("CLAVES").Rows.Count > 0 Then
                Dim pRow As DataRow
                LeeClaves = True
                For Each pRow In dt.Tables("CLAVES").Rows
                    If CInt(pRow("NIVELACCESO").ToString()) = wNivel Then
                        '
                        ' Recogemos datos...
                        '
                        With wrCLAVES
                            '
                            '   NIVEL de Acceso
                            '
                            If Not IsDBNull(pRow("NIVELACCESO")) And
                                        CInt(pRow("NIVELACCESO").ToString) > 0 Then
                                .NIVELACCESO = CInt(pRow("NIVELACCESO").ToString)
                            Else
                                .NIVELACCESO = 0
                            End If
                            '
                            .DESCRIPCION = pRow("DESCRIPCION").ToString() & ""
                            .CLAVE = pRow("CLAVE").ToString() & ""
                            .ACCESOREFGEN = pRow("ACCESOREFGEN").ToString() & ""
                            '
                            '   Acceso a "X"
                            '
                            If Not IsDBNull(pRow("ACCESOX")) And
                                        pRow("ACCESOX").ToString = "True" Then
                                .ACCESOX = "True"
                            Else
                                .ACCESOX = "False"
                            End If
                            '
                            '   Acceso a "Z"
                            '
                            If Not IsDBNull(pRow("ACCESOZ")) And
                                        pRow("ACCESOZ").ToString = "True" Then
                                .ACCESOZ = "True"
                            Else
                                .ACCESOZ = "False"
                            End If
                            '
                            '   Acceso a APPs
                            '
                            If Not IsDBNull(pRow("ACCESOAPPS")) And
                                        pRow("ACCESOAPPS").ToString = "True" Then
                                .ACCESOAPPS = "True"
                            Else
                                .ACCESOAPPS = "False"
                            End If
                            '
                            '   Botón [-]
                            '
                            If Not IsDBNull(pRow("BOTONMENOS")) And
                                        pRow("BOTONMENOS").ToString = "True" Then
                                .BOTONMENOS = "True"
                            Else
                                .BOTONMENOS = "False"
                            End If
                            '
                            '   Botón PRECIO
                            '
                            If Not IsDBNull(pRow("BOTONPRECIO")) And
                                        pRow("BOTONPRECIO").ToString = "True" Then
                                .BOTONPRECIO = "True"
                            Else
                                .BOTONPRECIO = "False"
                            End If
                            '
                            '   Botón TARIFA
                            '
                            If Not IsDBNull(pRow("BOTONTARIFA")) And
                                        pRow("BOTONTARIFA").ToString = "True" Then
                                .BOTONTARIFA = "True"
                            Else
                                .BOTONTARIFA = "False"
                            End If
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Lectura [CLAVES]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Public Function LocalizaClave(wLocClave As String) As Boolean
        '
        ' Localizamos una CLAVE determinada y su NIVEL de acceso.
        '
        LocalizaClave = False
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        Dim queryString As String = ""
        '
        queryString = "SELECT * FROM [CLAVES] "
        queryString = queryString & "WHERE [CLAVES].[CLAVE]='" & wLocClave & "' "
        '
        Dim dt As DataSet = New DataSet
        Try
            TblTPVS = New SqlDataAdapter(queryString, conexion)
            TblTPVS.Fill(dt, "CLAVES")
            '
            If dt.Tables("CLAVES").Rows.Count > 0 Then
                Dim pRow As DataRow
                LocalizaClave = True
                For Each pRow In dt.Tables("CLAVES").Rows
                    If pRow("CLAVE").ToString() = wLocClave Then
                        '
                        ' Recogemos datos...
                        '
                        With wrCLAVES
                            '
                            '   NIVEL de Acceso
                            '
                            If Not IsDBNull(pRow("NIVELACCESO")) And
                                        CInt(pRow("NIVELACCESO").ToString) > 0 Then
                                .NIVELACCESO = CInt(pRow("NIVELACCESO").ToString)
                            Else
                                .NIVELACCESO = 0
                            End If
                            '
                            .DESCRIPCION = pRow("DESCRIPCION").ToString() & ""
                            .CLAVE = pRow("CLAVE").ToString() & ""
                            .ACCESOREFGEN = pRow("ACCESOREFGEN").ToString() & ""
                            '
                            '   Acceso a "X"
                            '
                            If Not IsDBNull(pRow("ACCESOX")) And
                                        pRow("ACCESOX").ToString = "True" Then
                                .ACCESOX = "True"
                            Else
                                .ACCESOX = "False"
                            End If
                            '
                            '   Acceso a "Z"
                            '
                            If Not IsDBNull(pRow("ACCESOZ")) And
                                        pRow("ACCESOZ").ToString = "True" Then
                                .ACCESOZ = "True"
                            Else
                                .ACCESOZ = "False"
                            End If
                            '
                            '   Acceso a APPs
                            '
                            If Not IsDBNull(pRow("ACCESOAPPS")) And
                                        pRow("ACCESOAPPS").ToString = "True" Then
                                .ACCESOAPPS = "True"
                            Else
                                .ACCESOAPPS = "False"
                            End If
                            '
                            '   Botón [-]
                            '
                            If Not IsDBNull(pRow("BOTONMENOS")) And
                                        pRow("BOTONMENOS").ToString = "True" Then
                                .BOTONMENOS = "True"
                            Else
                                .BOTONMENOS = "False"
                            End If
                            '
                            '   Botón PRECIO
                            '
                            If Not IsDBNull(pRow("BOTONPRECIO")) And
                                        pRow("BOTONPRECIO").ToString = "True" Then
                                .BOTONPRECIO = "True"
                            Else
                                .BOTONPRECIO = "False"
                            End If
                            '
                            '   Botón TARIFA
                            '
                            If Not IsDBNull(pRow("BOTONTARIFA")) And
                                        pRow("BOTONTARIFA").ToString = "True" Then
                                .BOTONTARIFA = "True"
                            Else
                                .BOTONTARIFA = "False"
                            End If
                        End With
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Localizando [CLAVES]")
        End Try
        conexion.Close()
        dt.Dispose()
        conexion.Dispose()
        '
    End Function

    Private Sub CreaClave(wCrClave As Integer)
        '
        ' Crea Registros de NIVELES de Claves.
        ' Este procedimiento Genera Claves 0 a 5 de manera automática
        '  si no estan creadas.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        queryString = "Insert Into [CLAVES] ("
        queryString = queryString & " [NIVELACCESO],"
        queryString = queryString & " [DESCRIPCION],"
        queryString = queryString & " [CLAVE],"
        queryString = queryString & " [ACCESOX],"
        queryString = queryString & " [ACCESOZ],"
        queryString = queryString & " [ACCESOAPPS],"
        queryString = queryString & " [ACCESOREFGEN],"
        queryString = queryString & " [BOTONMENOS],"
        queryString = queryString & " [BOTONPRECIO],"
        queryString = queryString & " [BOTONTARIFA]"
        '
        queryString = queryString & ") Values ("
        '
        queryString = queryString & wCrClave & ","
        '
        Select Case wCrClave
            Case 0
                queryString = queryString & "'USR0',"
                queryString = queryString & "'00',"   ' Clave
                queryString = queryString & 0 & ","   ' ACCESOX
                queryString = queryString & 0 & ","   ' ACCESOZ
                queryString = queryString & 0 & ","   ' ACCESOSAPPS
                queryString = queryString & "'NADA'," ' ACCESOSREFGEN 
                queryString = queryString & 0 & ", "  ' BOTONMENOS
                queryString = queryString & 0 & ", "  ' BOTONPRECIO
                queryString = queryString & 1 & " "   ' BOTONTARIFA
            Case 1
                queryString = queryString & "'USR1',"
                queryString = queryString & "'01',"   ' Clave
                queryString = queryString & 0 & ","   ' ACCESOX
                queryString = queryString & 0 & ","   ' ACCESOZ
                queryString = queryString & 0 & ","   ' ACCESOSAPPS
                queryString = queryString & "'NADA'," ' ACCESOSREFGEN 
                queryString = queryString & 0 & ", "  ' BOTONMENOS
                queryString = queryString & 0 & ", "  ' BOTONPRECIO
                queryString = queryString & 1 & " "   ' BOTONTARIFA
            Case 2
                queryString = queryString & "'USR2',"
                queryString = queryString & "'02',"   ' Clave
                queryString = queryString & 0 & ","   ' ACCESOX
                queryString = queryString & 0 & ","   ' ACCESOZ
                queryString = queryString & 0 & ","   ' ACCESOSAPPS
                queryString = queryString & "'NADA'," ' ACCESOSREFGEN 
                queryString = queryString & 0 & ", "  ' BOTONMENOS
                queryString = queryString & 0 & ", "  ' BOTONPRECIO
                queryString = queryString & 1 & " "   ' BOTONTARIFA
            Case 3
                queryString = queryString & "'ENCARGADO',"
                queryString = queryString & "'03',"  ' Clave
                queryString = queryString & 1 & ","  ' ACCESOX
                queryString = queryString & 1 & ","  ' ACCESOZ
                queryString = queryString & 0 & ","  ' ACCESOSAPPS
                queryString = queryString & "'0/1'," ' ACCESOSREFGEN 
                queryString = queryString & 1 & ", " ' BOTONMENOS
                queryString = queryString & 1 & ", " ' BOTONPRECIO
                queryString = queryString & 1 & " "  ' BOTONTARIFA
            Case 4
                queryString = queryString & "'EL JEFE',"
                queryString = queryString & "'04',"  ' Clave
                queryString = queryString & 1 & ","  ' ACCESOX
                queryString = queryString & 1 & ","  ' ACCESOZ
                queryString = queryString & 1 & ","  ' ACCESOSAPPS
                queryString = queryString & "'0/1'," ' ACCESOSREFGEN 
                queryString = queryString & 1 & ", " ' BOTONMENOS
                queryString = queryString & 1 & ", " ' BOTONPRECIO
                queryString = queryString & 1 & " "  ' BOTONTARIFA
            Case 5
                queryString = queryString & "'SUPER',"
                queryString = queryString & "'05',"    ' Clave
                queryString = queryString & 1 & ","    ' ACCESOX
                queryString = queryString & 1 & ","    ' ACCESOZ
                queryString = queryString & 1 & ","    ' ACCESOSAPPS
                queryString = queryString & "'TODAS'," ' ACCESOSREFGEN 
                queryString = queryString & 1 & ", "   ' BOTONMENOS
                queryString = queryString & 1 & ", "   ' BOTONPRECIO
                queryString = queryString & 1 & " "    ' BOTONTARIFA
            Case Else
                '
                ' Fuera de rango 0 a 5. No deberia darse, pero...
                ' Si ocurre se crea un USR0 y listos!
                '
                queryString = queryString & "'USR0',"
                queryString = queryString & "'00',"   ' Clave
                queryString = queryString & 0 & ","   ' ACCESOX
                queryString = queryString & 0 & ","   ' ACCESOZ
                queryString = queryString & 0 & ","   ' ACCESOSAPPS
                queryString = queryString & "'NADA'," ' ACCESOSREFGEN 
                queryString = queryString & 0 & ", "  ' BOTONMENOS
                queryString = queryString & 0 & ", "  ' BOTONPRECIO
                queryString = queryString & 1 & " "   ' BOTONTARIFA
        End Select
        '
        queryString = queryString & ")"
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Registro [CLAVES]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub


    Public Sub ActualizaClaves(wCrClave As Integer)
        '
        ' Crea/Modifica Registros de NIVELES de Claves.
        ' Procedimiento mejor adapatado para el Mantenimiento de NIVELES.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        '
        If LeeClaves(wCrClave) = True Then
            '
            ' UPDATE
            '
            queryString = "UPDATE [CLAVES] SET "
            queryString = queryString & " [DESCRIPCION]='" & MyFrm20.TextBoxCDescrip.Text.Trim & "'," ' Descrip.
            queryString = queryString & " [CLAVE]='" & MyFrm20.TextBoxCclave.Text.Trim & "',"   ' Clave
            If MyFrm20.CheckBoxCX.Checked = False Then
                queryString = queryString & " [ACCESOX]=0,"   ' ACCESOX
            Else
                queryString = queryString & " [ACCESOX]=1,"   ' ACCESOX
            End If
            If MyFrm20.CheckBoxCZ.Checked = False Then
                queryString = queryString & " [ACCESOZ]=0,"   ' ACCESOZ
            Else
                queryString = queryString & " [ACCESOZ]=1,"   ' ACCESOZ
            End If
            If MyFrm20.CheckBoxCAPPs.Checked = False Then
                queryString = queryString & " [ACCESOAPPS]=0,"   ' ACCESOAPPS
            Else
                queryString = queryString & " [ACCESOAPPS]=1,"   ' ACCESOAPPS
            End If
            queryString = queryString & " [ACCESOREFGEN]='" & MyFrm20.ComboBoxCrefGen.Text.Trim & "'," ' ACCESOREFGEN
            If MyFrm20.CheckBoxCMenos.Checked = False Then
                queryString = queryString & " [BOTONMENOS]=0 "   ' BOTONMENOS
            Else
                queryString = queryString & " [BOTONMENOS]=1 "   ' BOTONMENOS
            End If
            queryString = queryString & " WHERE [NIVELACCESO] = " & wCrClave
        Else
            '
            ' INSERT
            '
            queryString = "Insert Into [CLAVES] ("
            queryString = queryString & " [NIVELACCESO],"
            queryString = queryString & " [DESCRIPCION],"
            queryString = queryString & " [CLAVE],"
            queryString = queryString & " [ACCESOX],"
            queryString = queryString & " [ACCESOZ],"
            queryString = queryString & " [ACCESOAPPS],"
            queryString = queryString & " [ACCESOREFGEN],"
            queryString = queryString & " [BOTONMENOS]"
            queryString = queryString & ") Values ("
            queryString = queryString & wCrClave & ","
            queryString = queryString & "'" & MyFrm20.TextBoxCDescrip.Text.Trim & "'," ' Descrip.
            queryString = queryString & "'" & MyFrm20.TextBoxCclave.Text.Trim & "',"   ' Clave
            If MyFrm20.CheckBoxCX.Checked = False Then
                queryString = queryString & 0 & ","   ' ACCESOX
            Else
                queryString = queryString & 1 & ","   ' ACCESOX
            End If
            If MyFrm20.CheckBoxCZ.Checked = False Then
                queryString = queryString & 0 & ","   ' ACCESOZ
            Else
                queryString = queryString & 1 & ","   ' ACCESOZ
            End If
            If MyFrm20.CheckBoxCAPPs.Checked = False Then
                queryString = queryString & 0 & ","   ' ACCESOSAPPS
            Else
                queryString = queryString & 1 & ","   ' ACCESOSAPPS
            End If
            queryString = queryString & "'" & MyFrm20.ComboBoxCrefGen.Text.Trim & "'," ' ACCESOSREFGEN 
            If MyFrm20.CheckBoxCMenos.Checked = False Then
                queryString = queryString & 0 & " "   ' BOTONMENOS
            Else
                queryString = queryString & 1 & " "   ' BOTONMENOS
            End If
            queryString = queryString & ")"
        End If
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Registro [CLAVES]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub MantenimientoClaves(wCrClave As Integer)
        '
        ' Crea/Modifica Registros de NIVELES de Claves.
        ' Procedimiento mejor adapatado para el Mantenimiento de NIVELES.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        '
        If LeeClaves(wCrClave) = True Then
            '
            ' UPDATE
            '
            queryString = "UPDATE [CLAVES] SET "
            queryString = queryString & " [DESCRIPCION]='" & MyFrm20.TextBoxCDescrip.Text.Trim & "'," ' Descrip.
            queryString = queryString & " [CLAVE]='" & MyFrm20.TextBoxCclave.Text.Trim & "',"   ' Clave
            If MyFrm20.CheckBoxCX.Checked = False Then
                queryString = queryString & " [ACCESOX]=0,"   ' ACCESOX
            Else
                queryString = queryString & " [ACCESOX]=1,"   ' ACCESOX
            End If
            If MyFrm20.CheckBoxCZ.Checked = False Then
                queryString = queryString & " [ACCESOZ]=0,"   ' ACCESOZ
            Else
                queryString = queryString & " [ACCESOZ]=1,"   ' ACCESOZ
            End If
            If MyFrm20.CheckBoxCAPPs.Checked = False Then
                queryString = queryString & " [ACCESOAPPS]=0,"   ' ACCESOAPPS
            Else
                queryString = queryString & " [ACCESOAPPS]=1,"   ' ACCESOAPPS
            End If
            queryString = queryString & " [ACCESOREFGEN]='" & MyFrm20.ComboBoxCrefGen.Text.Trim & "'," ' ACCESOREFGEN
            '
            If MyFrm20.CheckBoxCMenos.Checked = False Then
                queryString = queryString & " [BOTONMENOS]=0, "   ' BOTONMENOS
            Else
                queryString = queryString & " [BOTONMENOS]=1, "   ' BOTONMENOS
            End If
            '
            If MyFrm20.CheckBoxCPrecio.Checked = False Then
                queryString = queryString & " [BOTONPRECIO]=0, "   ' BOTONPRECIO
            Else
                queryString = queryString & " [BOTONPRECIO]=1, "   ' BOTONPRECIO
            End If
            '
            If MyFrm20.CheckBoxCTarifa.Checked = False Then
                queryString = queryString & " [BOTONTARIFA]=0 "   ' BOTONTARIFA
            Else
                queryString = queryString & " [BOTONTARIFA]=1 "   ' BOTONTARIFA
            End If
            queryString = queryString & " WHERE [NIVELACCESO] = " & wCrClave
        Else
            '
            ' INSERT
            '
            queryString = "Insert Into [CLAVES] ("
            queryString = queryString & " [NIVELACCESO],"
            queryString = queryString & " [DESCRIPCION],"
            queryString = queryString & " [CLAVE],"
            queryString = queryString & " [ACCESOX],"
            queryString = queryString & " [ACCESOZ],"
            queryString = queryString & " [ACCESOAPPS],"
            queryString = queryString & " [ACCESOREFGEN],"
            queryString = queryString & " [BOTONMENOS],"
            queryString = queryString & " [BOTONPRECIO],"
            queryString = queryString & " [BOTONTARIFA]"
            queryString = queryString & ") Values ("
            queryString = queryString & wCrClave & ","
            queryString = queryString & "'" & MyFrm20.TextBoxCDescrip.Text.Trim & "'," ' Descrip.
            queryString = queryString & "'" & MyFrm20.TextBoxCclave.Text.Trim & "',"   ' Clave
            If MyFrm20.CheckBoxCX.Checked = False Then
                queryString = queryString & 0 & ","   ' ACCESOX
            Else
                queryString = queryString & 1 & ","   ' ACCESOX
            End If
            If MyFrm20.CheckBoxCZ.Checked = False Then
                queryString = queryString & 0 & ","   ' ACCESOZ
            Else
                queryString = queryString & 1 & ","   ' ACCESOZ
            End If
            If MyFrm20.CheckBoxCAPPs.Checked = False Then
                queryString = queryString & 0 & ","   ' ACCESOSAPPS
            Else
                queryString = queryString & 1 & ","   ' ACCESOSAPPS
            End If
            queryString = queryString & "'" & MyFrm20.ComboBoxCrefGen.Text.Trim & "'," ' ACCESOSREFGEN 
            If MyFrm20.CheckBoxCMenos.Checked = False Then
                queryString = queryString & 0 & ", "   ' BOTONMENOS
            Else
                queryString = queryString & 1 & ", "   ' BOTONMENOS
            End If
            If MyFrm20.CheckBoxCPrecio.Checked = False Then
                queryString = queryString & 0 & ", "   ' BOTONPRECIO
            Else
                queryString = queryString & 1 & ", "   ' BOTONPRECIO
            End If
            If MyFrm20.CheckBoxCTarifa.Checked = False Then
                queryString = queryString & 0 & " "   ' BOTONTARIFA
            Else
                queryString = queryString & 1 & " "   ' BOTONTARIFA
            End If
            queryString = queryString & ")"
        End If
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Registro [CLAVES]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub MantenimientoPedidos(wTelefono As String)
        '
        ' Crea/Modifica Registros de Pedidos a Domicilio.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        '
        With MyFrm21
            '
            ' Evita error conversion Cint()
            '
            If .TextBoxPDCP.Text.Trim.Length = 0 Then
                .TextBoxPDCP.Text = "0"
            End If
            If LeePEDCLIE(wTelefono) = True Then
                '
                ' UPDATE
                '
                queryString = "UPDATE [PEDCLIE] SET "
                queryString = queryString & " [NOMBRE]='" & .TextBoxPDNombre.Text.Trim & "',"
                queryString = queryString & " [DIRECCION]='" & .TextBoxPDDirec.Text.Trim & "',"
                queryString = queryString & " [POBLACION]='" & .TextBoxPDPobla.Text.Trim & "',"
                queryString = queryString & " [EMAIL]='" & .TextBoxPDeMail.Text.Trim & "',"
                queryString = queryString & " [CODPOSTAL]=" & CInt(.TextBoxPDCP.Text.Trim) & ","
                '
                ' Email S/N, SMS S/N
                '
                If .CheckBoxEnvEmail.Checked = False Then
                    queryString = queryString & " [EMAILSN]=0,"
                Else
                    queryString = queryString & " [EMAILSN]=1,"
                End If
                If .CheckBoxEnvSMS.Checked = False Then
                    queryString = queryString & " [SMSSN]=0,"
                Else
                    queryString = queryString & " [SMSSN]=1,"
                End If
                '
                queryString = queryString & " [OBSER]='" & .TextBoxPDObser.Text.Trim & "' "
                queryString = queryString & " WHERE [TELEFONO] = '" & wTelefono & "' "
            Else
                '
                ' INSERT
                '
                queryString = "Insert Into [PEDCLIE] ("
                queryString = queryString & " [TELEFONO],"
                queryString = queryString & " [NOMBRE],"
                queryString = queryString & " [DIRECCION],"
                queryString = queryString & " [POBLACION],"
                queryString = queryString & " [EMAIL],"
                queryString = queryString & " [CODPOSTAL],"
                queryString = queryString & " [EMAILSN],"
                queryString = queryString & " [SMSSN],"
                queryString = queryString & " [OBSER]"
                queryString = queryString & ") Values ("
                queryString = queryString & wTelefono.Trim & ","
                queryString = queryString & "'" & .TextBoxPDNombre.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxPDDirec.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxPDPobla.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxPDeMail.Text.Trim & "',"
                queryString = queryString & CInt(.TextBoxPDCP.Text.Trim) & ","
                '
                ' Email S/N, SMS S/N
                '
                If .CheckBoxEnvEmail.Checked = False Then
                    queryString = queryString & 0 & ","
                Else
                    queryString = queryString & 1 & ","
                End If
                If .CheckBoxEnvSMS.Checked = False Then
                    queryString = queryString & 0 & ","
                Else
                    queryString = queryString & 1 & ","
                End If
                '
                queryString = queryString & "'" & .TextBoxPDObser.Text.Trim & "' "
                queryString = queryString & ")"
            End If
        End With
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Registro [PEDCLIE]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub MantenimientoMCO(wCod430 As String)
        '
        ' Crea/Modifica Registros de Clientes Crédito (MCO).
        '
        Dim conexion As New SqlConnection
        SQL_Catalogo1 = DameCatalogoEmpresa(wEmpresa, "CONTATRV")
        SQL_CadenaConexion1 = SQL_Instancia & SQL_Catalogo1 & SQL_Seguridad_Otros
        conexion.ConnectionString = SQL_CadenaConexion1
        conexion.Open()
        '
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        '
        With MyFrm22
            '
            ' Evita error conversion Cint()
            '
            If .TextBoxCLCP.Text.Trim.Length = 0 Then
                .TextBoxCLCP.Text = "0"
            End If
            If .TextBoxCLDto.Text.Trim.Length = 0 Then
                .TextBoxCLDto.Text = "0"
            End If
            If LeeClienteMCO(CInt(wCod430.Trim)) = True Then
                '
                ' UPDATE
                '
                queryString = "UPDATE [MCO] SET "
                queryString = queryString & " [NOMBRE]='" & .TextBoxCLNombre.Text.Trim & "',"
                queryString = queryString & " [DIRECCION]='" & .TextBoxCLDirec.Text.Trim & "',"
                queryString = queryString & " [CODPOSTAL]=" & CInt(.TextBoxCLCP.Text.Trim) & ","
                queryString = queryString & " [TELEFONO]='" & .TextBoxCLTLF1.Text.Trim & "',"
                queryString = queryString & " [TELEFONO2]='" & .TextBoxCLTLF2.Text.Trim & "',"
                queryString = queryString & " [POBLACION]='" & .TextBoxCLPobla.Text.Trim & "',"
                queryString = queryString & " [EMAIL]='" & .TextBoxCLeMail.Text.Trim & "',"
                queryString = queryString & " [DTO]='" & CDbl(.TextBoxCLDto.Text.Trim.Replace(".", ",")).ToString.Replace(",", ".") & "' "
                queryString = queryString & " WHERE [CUENTA] = " & CInt(wCod430.Trim) & " "
            Else
                '
                ' INSERT
                '
                queryString = "Insert Into [MCO] ("
                queryString = queryString & " [CUENTA],"
                queryString = queryString & " [NOMBRE],"
                queryString = queryString & " [DIRECCION],"
                queryString = queryString & " [CODPOSTAL],"
                queryString = queryString & " [TELEFONO],"
                queryString = queryString & " [TELEFONO2],"
                queryString = queryString & " [POBLACION],"
                queryString = queryString & " [EMAIL],"
                queryString = queryString & " [DTO]"
                queryString = queryString & ") Values ("
                queryString = queryString & CInt(wCod430.Trim) & ","
                queryString = queryString & "'" & .TextBoxCLNombre.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLDirec.Text.Trim & "',"
                queryString = queryString & CInt(.TextBoxCLCP.Text.Trim) & ","
                queryString = queryString & "'" & .TextBoxCLTLF1.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLTLF2.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLPobla.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLeMail.Text.Trim & "',"
                queryString = queryString & CDbl(.TextBoxCLDto.Text.Trim.Replace(".", ",")).ToString.Replace(",", ".")
                queryString = queryString & ")"
            End If
        End With
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Registro [MCO]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub MantenimientoCLICONTA(wNifCif As String)
        '
        ' Crea/Modifica Registros de Clientes CONTADO (CLICONTA).
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        '
        With MyFrm22
            '
            ' Evita error conversion Cint()
            '
            If .TextBoxCLCP.Text.Trim.Length = 0 Then
                .TextBoxCLCP.Text = "0"
            End If
            If .TextBoxCLDto.Text.Trim.Length = 0 Then
                .TextBoxCLDto.Text = "0"
            End If
            If LeeClienteCONTA(wNifCif) = True Then
                '
                ' UPDATE
                '
                queryString = "UPDATE [CLICONTA] SET "
                queryString = queryString & " [NOMBRE]='" & .TextBoxCLNombre.Text.Trim & "',"
                queryString = queryString & " [DIRECCION]='" & .TextBoxCLDirec.Text.Trim & "',"
                queryString = queryString & " [POBLACION]='" & .TextBoxCLPobla.Text.Trim & "',"
                queryString = queryString & " [CODPOSTAL]=" & CInt(.TextBoxCLCP.Text.Trim) & ","
                queryString = queryString & " [TELEFONO1]='" & .TextBoxCLTLF1.Text.Trim & "',"
                queryString = queryString & " [TELEFONO2]='" & .TextBoxCLTLF2.Text.Trim & "',"
                queryString = queryString & " [DTO]='" & CDbl(.TextBoxCLDto.Text.Trim.Replace(".", ",")).ToString.Replace(",", ".") & "',"
                queryString = queryString & " [EMAIL]='" & .TextBoxCLeMail.Text.Trim & "' "
                queryString = queryString & " WHERE [NIFCIF] = '" & wNifCif & "' "
            Else
                '
                ' INSERT
                '
                queryString = "Insert Into [CLICONTA] ("
                queryString = queryString & " [NIFCIF],"
                queryString = queryString & " [NOMBRE],"
                queryString = queryString & " [DIRECCION],"
                queryString = queryString & " [POBLACION],"
                queryString = queryString & " [CODPOSTAL],"
                queryString = queryString & " [TELEFONO1],"
                queryString = queryString & " [TELEFONO2],"
                queryString = queryString & " [DTO],"
                queryString = queryString & " [EMAIL]"
                queryString = queryString & ") Values ("
                queryString = queryString & "'" & wNifCif.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLNombre.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLDirec.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLPobla.Text.Trim & "',"
                queryString = queryString & CInt(.TextBoxCLCP.Text.Trim) & ", "
                queryString = queryString & "'" & .TextBoxCLTLF1.Text.Trim & "',"
                queryString = queryString & "'" & .TextBoxCLTLF2.Text.Trim & "',"
                queryString = queryString & "'" & CDbl(.TextBoxCLDto.Text.Trim.Replace(".", ",")).ToString.Replace(",", ".") & "', "
                queryString = queryString & "'" & .TextBoxCLeMail.Text.Trim & "' "
                queryString = queryString & ")"
            End If
        End With
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Creando Registro [CLICONTA]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub ActualizaClave(wCrClave As Integer)
        '
        ' Actualiza Registros de NIVELES de Claves.
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        queryString = "UPDATE [CLAVES] SET "
        queryString = queryString & " [CLAVE]='" & MyFrm6.TextBoxClNueva.Text.Trim & "' "
        queryString &= "WHERE "
        queryString &= "[CLAVES].[NIVELACCESO]=" & wCrClave & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Actualizando Registro [CLAVES]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub BorrarClave(wCrClave As Integer)
        '
        ' BORRADO! de Registros de NIVELES de Claves.
        '
        ' Medida de seguridad
        '
        If wCrClave < 6 Then
            Exit Sub
        End If
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        queryString = "DELETE [CLAVES] WHERE "
        queryString &= "[CLAVES].[NIVELACCESO]=" & wCrClave & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Borrado de Registro [CLAVES]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub ActualizaVendedor(wVendedor As Integer)
        '
        ' Actualiza Registros de VENDEDORES.
        '
        If LeeVendedor(wVendedor) = False Then
            Exit Sub
        End If
        '
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        Dim queryString As String = ""
        queryString = "UPDATE [VEN] SET "
        queryString = queryString & " [VEN].[NIVELACCESO]=" & CInt(MyFrm20.TextBoxCnivel1.Text.Trim) & " "
        queryString &= "WHERE "
        queryString &= "[VEN].[CODIGO]=" & wVendedor & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Actualizando Registro [VEN]")
        End Try
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub



    Public Sub RestaUnaBotonMenos()
        '
        ' Boton [-] Formulario PRODUCTOS.
        ' Necesario para tema de credenciales y niveles de acceso.
        ' Al borrar una unidad Existente, creamos un registro
        '  de lineas borradas.
        '
        With MyFrm2.GRID1
            If .Rows.Count > 0 Then
                If .SelectedRows.Count > 0 Then
                    MisUnidadesE = CDec(.SelectedCells(1).Value.ToString.Trim.Replace(".", ","))
                    MisUnidadesN = CDec(.SelectedCells(3).Value.ToString.Trim.Replace(".", ","))
                    MiPVPMedio = CDbl(.SelectedCells(4).Value.ToString.Trim.Replace(".", ",")) / MisUnidadesE
                    '
                    If MisUnidadesN > 0 Then
                        MisUnidadesN -= 1
                        MisUnidadesE -= 1
                        .SelectedCells(1).Value = MisUnidadesE.ToString.Trim.Replace(",", ".")
                        .SelectedCells(3).Value = MisUnidadesN.ToString.Trim.Replace(",", ".")
                    Else
                        '
                        ' En borrado de Unid. Existentes creamos un registro de línea Anulada!!!
                        ' Pide Confirmacion si Ref. Generales lo indica.
                        '
                        LeeTCONA4Cfg("General")
                        If wrLeeTCONA4.Tcona4_BORLINCUENTA = "True" Then
                            msg = "Por favor confirme BORRADO.: " & vbCrLf
                            msg &= .SelectedCells(0).Value.ToString & " " & .SelectedCells(2).Value.ToString & vbCrLf
                            style = MsgBoxStyle.DefaultButton2 Or
                                    MsgBoxStyle.Critical Or
                                    MsgBoxStyle.YesNo
                            title = "Borrar Unidad."
                            response = MsgBox(msg, style, title)
                            If response = MsgBoxResult.Yes Then
                                '
                                MisUnidadesE -= 1
                                .SelectedCells(1).Value = MisUnidadesE.ToString.Trim.Replace(",", ".")
                                GrabaMESABORLIN(0)
                                '
                            End If
                        Else
                            MisUnidadesE -= 1
                            .SelectedCells(1).Value = MisUnidadesE.ToString.Trim.Replace(",", ".")
                            GrabaMESABORLIN(0)
                        End If
                    End If
                    '
                    ' Calculo Nuevo Importe / Basado en Media Precio
                    '
                    MiImporte = MisUnidadesE * MiPVPMedio
                    .SelectedCells(4).Value = MiImporte.ToString(fmtImporte).Replace(",", ".")
                    .SelectedCells(5).Value = "N"
                End If
                '
                ' Si existentes quedan a CERO o Por algun Motivo pasan a negativas
                '    se elimina la linea actual del GRID1,
                '    y de la TABLA MESA Si existe el Registro.
                '
                If MisUnidadesE = 0 Or MisUnidadesE < 0 Then
                    If ExisteRegistroMESA(MyFrm2.TextBoxNumMesa.Text.Trim,
                                  .SelectedCells(0).Value.ToString.Trim,
                                  wStringCombinados, wMediaPrecio) = True Then
                        '
                        ' BORRA! Líneas
                        '
                        BorraRegistroMESA(MyFrm2.TextBoxNumMesa.Text.Trim,
                                  .SelectedCells(0).Value.ToString.Trim,
                                  wStringCombinados, wMediaPrecio)
                        '
                    End If
                    '
                    .Rows.Remove(.SelectedRows(0))
                    .ClearSelection()
                    MyFrm2.TextBoxCodBarras.Focus()
                    MyFrm2.ButtonRestaUnidN.Enabled = False
                    MyFrm2.ButtonSumaUnidN.Enabled = False
                End If
                '
                wTotalN = CDbl(CalculaTotalGRID1.ToString.Replace(".", ","))
                MyFrm2.LabelTotComanda.Text = wTotalN.ToString(fmtImporte).Replace(",", ".")
            End If
            '
            ' SI Se han BORRADO TODAS las Líneas
            '   Poner MESAC a CERO, pero sin DESOCUPARLA.
            '
            If .Rows.Count = 0 Then
                ActualizaDatosMESAC(MyFrm2.TextBoxNumMesa.Text.Trim, 1)
            End If
        End With
        '
    End Sub

    Public Sub GrabaMESABORLIN(wOPC As Integer)
        '
        ' Este procedimiento almacena un registro en la tabla [MESABORLIN]
        ' de unidades existentes (normalmente=1) eliminadas de la tabla MESA.
        '
        ' wOPC:
        '   0 = Boton [-] Normalmente se esta anulando 1 Unidad 
        '   1 = Boton  [Anula Línea Seleccion] 
        '       se estan anulando TODAS las existentes.
        '
        Dim HHMESA As String = TimeOfDay.ToLongTimeString.ToString
        Dim FFMESA As String = Date.Now.ToShortDateString
        Dim wTmpUnid As Decimal = 0 : Dim wTmpImporte As Decimal = 0
        Dim wTmpUnidE As Decimal = 0
        Dim MicodArt As String = ""
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        '
        ' Recogemos los DATOS para MESABORLIN
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
        With MyFrm2.GRID1
            If .Rows.Count > 0 Then
                If .SelectedRows.Count > 0 Then
                    MicodArt = .SelectedCells(0).Value.ToString.Trim
                    '
                    Select Case wOPC
                        Case 0
                            wrMESA.Mesa_UNID = "1.00" '<- Normalmente se esta anulando una Unidad 
                        Case 1
                            wrMESA.Mesa_UNID = .SelectedCells(1).Value.ToString.Replace(".", ",") ' Unidades Existentes
                    End Select
                    wTmpUnidE = CDec(.SelectedCells(1).Value.ToString.Replace(".", ",")) ' Unidades Existentes
                    wrMESA.Mesa_IMPORTE = .SelectedCells(4).Value.ToString.Replace(",", ".")
                    wrMESA.Mesa_PDTO = "0.00"
                    wrMESA.Mesa_IMPDTO = "0.00"
                    wrMESA.Mesa_VENDEDOR = CInt(MyFrm2.TextBoxCamarero.Text.Trim)
                    wrMESA.Mesa_HORA = HHMESA
                    wrMESA.Mesa_COSTO = wrLeeMAR.Mar_PRECOSTO.Replace(",", ".")
                    wrMESA.Mesa_ALMACEN = wAlmacen
                    wrMESA.Mesa_IGIC = wrLeeMAR.Mar_IVAVENTA.Replace(",", ".")
                    wrMESA.Mesa_SALA = wCodSala
                    wrMESA.Mesa_ORDENPLATO = CInt(.SelectedCells(8).Value.ToString)
                    '
                    wStringCombinados = .SelectedCells(6).Value.ToString.Trim
                    wMediaPrecio = .SelectedCells(7).Value.ToString.Trim
                    '
                    ' COMBINADO
                    '
                    wStringCombinados = " "
                    If .SelectedCells(6).Value.ToString.Trim.Length > 0 Then
                        wStringCombinados = .SelectedCells(6).Value.ToString
                    End If
                    '
                    ' MEDIAPRECIO (RACIONES)
                    '
                    wMediaPrecio = " "
                    If .SelectedCells(7).Value.ToString.Trim.Length > 0 Then
                        wMediaPrecio = .SelectedCells(7).Value.ToString
                    End If
                    '
                    '
                    ' NOZETA, SALA
                    '
                    If LeeSALA(wCodSala) = True Then
                        wrMESA.Mesa_NOZETA = wrLeeSALA.Sala_NOZETA
                    Else
                        wrMESA.Mesa_NOZETA = 0
                    End If
                    '
                    LeeMar(.SelectedCells(0).Value.ToString.Trim)
                    '
                    'wTmpImporte = ???
                    wTmpUnid = CDec(wrMESA.Mesa_UNID.ToString.Replace(".", ","))
                    '
                End If
            End If
        End With
        '
        ' INSERT - Registro MESABORLIN Nuevo.
        '
        queryString = queryString & "INSERT INTO [MESABORLIN] ("
        queryString = queryString & "[MESABORLIN].[NUMCAJA],"
        queryString = queryString & "[MESABORLIN].[FECHA],"
        queryString = queryString & "[MESABORLIN].[SALA],"
        queryString = queryString & "[MESABORLIN].[MESA],"
        queryString = queryString & "[MESABORLIN].[FACTURA],"
        queryString = queryString & "[MESABORLIN].[ARTI],"
        queryString = queryString & "[MESABORLIN].[COMBINA],"
        queryString = queryString & "[MESABORLIN].[MEDIAPRECIO],"
        queryString = queryString & "[MESABORLIN].[UNIDANUL], "
        queryString = queryString & "[MESABORLIN].[UNIDEX], "
        queryString = queryString & "[MESABORLIN].[IMPORTE], "
        queryString = queryString & "[MESABORLIN].[PDTO], "
        queryString = queryString & "[MESABORLIN].[IMPDTO], "
        queryString = queryString & "[MESABORLIN].[VENDEDOR], "
        queryString = queryString & "[MESABORLIN].[HORA], "
        queryString = queryString & "[MESABORLIN].[COSTO], "
        queryString = queryString & "[MESABORLIN].[ALMACEN], "
        queryString = queryString & "[MESABORLIN].[IGIC], "
        queryString = queryString & "[MESABORLIN].[NOZETA], "
        queryString = queryString & "[MESABORLIN].[NUMZETA], "
        queryString = queryString & "[MESABORLIN].[ORDENPLATO] "
        queryString = queryString & ") Values ("
        queryString = queryString & wCaja & ", "
        queryString = queryString & "'" & FFMESA & "', "
        queryString = queryString & "'" & wCodSala & "', "
        queryString = queryString & "'" & MyFrm2.TextBoxNumMesa.Text.Trim & "', "
        queryString = queryString & wFacturaN & ", "
        queryString = queryString & "'" & MicodArt.Trim & "', "
        queryString = queryString & "'" & wStringCombinados & "', "
        queryString = queryString & "'" & wMediaPrecio & "', "
        queryString = queryString & "'" & wTmpUnid.ToString.Replace(",", ".") & "', " ' Unid. ANULADA
        queryString = queryString & "'" & wTmpUnidE.ToString.Replace(",", ".") & "', " ' Unid(S). Exist.
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
                               "Comprobar Tabla, Grabando Datos [MESABORLIN]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub

    Public Sub AnulaLineasSEL_GRID1()
        '
        ' Anula la línea Seleccionada.
        ' Necesario para tema de credenciales y niveles de acceso.
        '-----------------------------------------------------------------------------------------
        '
        '   "E" - Líneas Existentes.
        '   "N" - Líneas Nuevas / Nuevas Unidades.
        '
        ' GRID:
        '   0 Cod. Art         (No Visible)
        '   1 Unid. Existentes 
        '   2 Descripcion      
        '   3 Unid. Nuevas     
        '   4 Importe          
        '   5 Tipo E/N          (No visible)
        '   6 Cod. Combinados   (No visible)
        '   7 Raciones          (No visible)
        '   8 Nro. Plato        (No visible)
        '-----------------------------------------------------------------------------------------
        If MyFrm2.GRID1.SelectedRows.Count > 0 Then
            '
            ' Anulamos lineas.
            '
            ' 1.- Si ya Existe en Tabla MESA: = "E"
            '     Anula (BORRA) la Línea de la TABLA MESA.
            '     ( El TOTAL de lineas existentes. )
            '
            ' 2.- Quitamos la Línea del GRID1.
            '
            ' COMBINADO y MEDIAPRECIO...
            wMediaPrecio = " "
            wStringCombinados = MyFrm2.GRID1.SelectedCells(6).Value.ToString.Trim
            wMediaPrecio = MyFrm2.GRID1.SelectedCells(7).Value.ToString.Trim
            '
            If ExisteRegistroMESA(MyFrm2.TextBoxNumMesa.Text.Trim,
                                  MyFrm2.GRID1.SelectedCells(0).Value.ToString.Trim,
                                  wStringCombinados, wMediaPrecio) = True Then
                '
                ' BORRA! Líneas
                '
                BorraRegistroMESA(MyFrm2.TextBoxNumMesa.Text.Trim,
                                  MyFrm2.GRID1.SelectedCells(0).Value.ToString.Trim,
                                  wStringCombinados, wMediaPrecio)
                '
                ' Guardamos un registro de UNIDADES EXISTENTES borradas!
                '
                GrabaMESABORLIN(1)
                '
            End If
            '
            MyFrm2.GRID1.Rows.Remove(MyFrm2.GRID1.SelectedRows(0))
        End If
        '
        MyFrm2.LabelTotComanda.Text = CalculaTotalGRID1()
        '
        ' SI Se han BORRADO TODAS las Líneas
        '   Poner MESAC a CERO, pero sin DESOCUPARLA.
        '
        If MyFrm2.GRID1.Rows.Count = 0 Then
            ActualizaDatosMESAC(MyFrm2.TextBoxNumMesa.Text.Trim, 1)
        End If
        '
    End Sub

    Public Sub CambiaVendedorMesa(wCVenFact As Integer)
        '
        ' Modificamos el Vendedor a una MESA, segun su Nro. de Factura.
        ' [SALA], [MESA], [MESAC]
        '
        Dim queryString As String = ""
        Dim conexion As New SqlConnection
        conexion.ConnectionString = SQL_CadenaConexion
        conexion.Open()
        Dim cmd As New System.Data.SqlClient.SqlCommand
        cmd.CommandType = System.Data.CommandType.Text
        '
        ' [ SALA1 ]
        '
        queryString = ""
        queryString = "UPDATE [SALA1] SET "
        queryString = queryString & " [SALA1].[VENDEDOR]='" & MyFrm2.TextBoxCamarero.Text.Trim & "' "
        queryString &= "WHERE "
        queryString &= "[SALA1].[FACTURA]=" & wCVenFact & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Actualizando Camarero [SALA1]")
        End Try
        '
        ' [ MESAC ]
        '
        queryString = ""
        queryString = "UPDATE [MESAC] SET "
        queryString = queryString & " [MESAC].[VENDEDOR]='" & MyFrm2.TextBoxCamarero.Text.Trim & "' "
        queryString &= "WHERE "
        queryString &= "[MESAC].[FACTURA]=" & wCVenFact & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Actualizando Camarero [MESAC]")
        End Try
        '
        ' [ MESA ]
        '
        queryString = ""
        queryString = "UPDATE [MESA] SET "
        queryString = queryString & " [MESA].[VENDEDOR]='" & MyFrm2.TextBoxCamarero.Text.Trim & "' "
        queryString &= "WHERE "
        queryString &= "[MESA].[FACTURA]=" & wCVenFact & " "
        '
        Try
            cmd.CommandText = queryString
            cmd.Connection = conexion
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Source & vbCrLf & ex.Message,
                                MsgBoxStyle.Exclamation Or
                                MsgBoxStyle.OkOnly,
                                "Comprobar Tabla Actualizando Camarero [MESA]")
        End Try
        '
        conexion.Close()
        cmd.Dispose()
        conexion.Dispose()
        '
    End Sub


End Module
