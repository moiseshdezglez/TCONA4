Module Tcona4VarsVIP
    '---------------------------------------------------------------------------------
    ' Aqui se incluyen algunas de las Variables mas significativas para 
    '   el desempeño de la aplicación, por ser de importancia Vital 
    '   para facil localización y tratamiento de las mismas.
    '---------------------------------------------------------------------------------
    '
    ' Definición de los Formularios del Proyecto.
    ' Estas Variables Gobiernan TODOS los FORMULARIOS 
    '   usados en la aplicación.
    ' Se definen sus Propiedades mas importantes en el Procedimiento 
    '   Principal de Entrada a la aplicacion "Main", llamando para ello
    '   al procedimiento "DefineFORMULARIOS" del Módulo "Tcona4Varios".
    '
    Public MyFrm1 As New TCONA401  ' MESAS.
    Public MyFrm2 As New TCONA402  ' PRODUCTOS.
    Public MyFrm3 As New TCONA403  ' Vista Ampliada Lista Productos (Zoom).
    Public MyFrm4 As New TCONA404  ' Cambio / Cobros
    Public MyFrm5 As New TCONA405  ' Z, X, Ref. Gen, Etc.
    Public MyFrm6 As New TCONA406  ' Referencias Generales.
    Public MyFrm7 As New TCONA407  ' Mantenimiento de COMBINADOS.
    Public MyFrm8 As New TCONA408  ' Pantalla COMBINADOS.
    Public MyFrm9 As New TCONA409  ' Pantalla Pedir VENDEDOR.
    Public MyFrm10 As New TCONA410 ' Mantenimientos Varios (Menu.)
    Public MyFrm11 As New TCONA411 ' Consulta Movimientos MESAS
    Public MyFrm12 As New TCONA412 ' Cambio de Mesa
    Public MyFrm13 As New TCONA413 ' Separar Cuenta ...
    Public MyFrm14 As New TCONA414 ' Mesas Ocupadas x Salas
    Public MyFrm15 As New TCONA415 ' Tecclados "Alf./ Num."
    Public MyFrm16 As New TCONA416 ' Botón Enviar ( Visualiza Datos a Areas )
    Public MyFrm17 As New TCONA417 ' Mant. de MODELOS de impresora
    Public MyFrm18 As New TCONA418 ' Mensajes a AREAS (COCINA, Etc.)
    Public MyFrm19 As New TCONA419 ' Eleccion del Nro. Caja para el TPV.
    Public MyFrm20 As New TCONA420 ' Mant. de Claves.
    Public MyFrm21 As New TCONA421 ' Pedidos a Domicilio.
    Public MyFrm22 As New TCONA422 ' Clientes Contado/Crédito (Factura A4).
    '
    ' Determina si se abre el FORMULARIO para pedir Nro. de Caja
    '
    Public PideCajas As Boolean = False
    '
    '  Define FIRNULARIO de Inicio.
    '  (Se Configura desde Ref. Generales.)
    '    0 = TCONA401 ' SALAS / MESAS
    '    1 = TCONA402 ' Productos
    '
    Public FormularioInicial As Integer
    '
    '   Composicion de la cadena de conexión a la BASE DE DATOS [GESTITRV].
    '   Define la INSTANCIA/CATALOGO de trabajo, asi como otros parámetros
    '     necesarios para establecer Conexión a la Base de datos.
    '   Para su construcción se apoya en el archivo externo 
    '     "C:\TRIVAGES\DATOS\CONA4CFG.txt", en el cual se define la INSTANCIA.
    '   Ej.: 1:Data Source=SERVIDOR\SQLTRV;
    '
    '   USO.: SQL_CadenaConexion = SQL_Instancia & SQL_Catalogo & SQL_Seguridad_Otros
    '
    Public SQL_Instancia As String = ""
    Public SQL_Catalogo As String = "Initial Catalog=GESTITRV;"
    Public SQL_Catalogo1 As String = "Initial Catalog=CONTATRV001;" ' Por Defecto
    '
    Public SQL_Seguridad_Otros As String = "Integrated Security=True;
                                         Connect Timeout=15;
                                         Encrypt=False;
                                         TrustServerCertificate=True;
                                         ApplicationIntent=ReadWrite;
                                         MultiSubnetFailover=False"
    Public SQL_CadenaConexion As String = ""  ' Almacen
    Public SQL_CadenaConexion1 As String = "" ' Contabilidad
    '
    Public FechaMESAC As String = ""     ' Controla la Fecha para MESA (Líneas), MESAC (Cabecera)
    Public HoraMESAC As String = ""      ' Controla la Hora para  MESA, MESAC
    '
    Public SwAparca As Boolean = False   ' Controla la acción de APARCAR
    Public SwCobrando As Boolean = False ' Controla la accion de COBROS
    Public SwZeta As Boolean = False     ' Controla la accion de la "Z"
    '
    Public wCaja As Integer = 0          ' De manera GENERAL, define la CAJA en la que estamos trabajando.
    Public wCodSala As String = ""       ' De manera GENERAL, define la SALA ACTUAL en la que estamos trabajando.
    Public wCodSalaC As String = ""      ' Idem. Cod. SALA para Cambio de MESAS.
    Public wCodMesa As String = ""       ' De manera GENERAL, define la MESA ACTUAL en la que estamos trabajando.
    Public wCodFamilia As String = ""    ' Defina la FAMILIA ACTUAL de productos, en pantalla Productos.
    '
    ' *** IMPORTANTE ***
    ' Esta variable en GENEREAL, Gobierna el CONTADOR DE FACTURAS.
    '
    Public wFacturaN As Integer = 0
    '
    Public wFacturaNSep As Integer = 0      ' Usado Al separar CUENTAS/MESAS
    Public wVendedorApertura As Integer = 0 ' Vendedor de Apertura de la Mesa ACTUAL
    Public wMesaLibre As Boolean = True     ' Determina Cuando una MESA esta LIBRE/OCUPADA.
    Public ContadorPlato As Integer = 1     ' Gobierna el Nro. de Plato, Para Informar en AREAS (COCINA)
    '
    ' En GENERAL informa del Almacen y la Empresa
    '  actuales para la aplicación.
    '
    Public wAlmacen As String = "1"         ' Se recoge del TCONA4 (Ref. Gen.)
    Public wEmpresa As Integer = 1          ' Se recoge del TCONA4 (Ref. Gen.)
    '
    ' Gestiona Cuenta Clientes Crédito FAC A4.
    ' Por defecto 430000000 = Sin Cliente asignado.
    Public wCliente As Integer = 430000000
    '
    Public WMesacNIFCIF As String = ""      ' Gestiona NIF/CIF Clientes Contado FAC A4.
    Public WMesacTlfPed As String = ""      ' Gestiona Nro. de Telefono de un Pedido a Domicilio.
    '
    ' PassWords para Programadores. Me da "verguenza!!!" ver esto en el código fuente.
    ' Algo habrá que hacer para enmascarar/cifrar/ocultar o lo que sea estas Pwds.
    ' Pdte. ... ... ...
    '
    Public PassTRIVALLE() As String = {"603335", "758132"}
    '
End Module
