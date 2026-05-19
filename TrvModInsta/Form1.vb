Imports System.IO

Public Class Form1
    '
    '   USO.: SQL_CadenaConexion = SQL_Instancia & SQL_Catalogo & SQL_Seguridad_Otros
    '
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult
    '
    Dim FileContent() As String
    Dim Flen As Integer = 0
    Dim Count As Integer = 0
    '
    Dim SQL_Instancia As String = ""
    Dim SQL_Catalogo As String = "Initial Catalog=GESTITRV;"
    Dim SQL_Catalogo1 As String = "Initial Catalog=CONTATRV001;" ' Por Defecto
    Dim SQL_Seguridad_Otros As String = "Integrated Security=True;
                                         Connect Timeout=15;
                                         Encrypt=False;
                                         TrustServerCertificate=True;
                                         ApplicationIntent=ReadWrite;
                                         MultiSubnetFailover=False"
    Dim SQL_CadenaConexion As String = ""  ' Almacen
    Dim SQL_CadenaConexion1 As String = "" ' Contabilidad
    '

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        Button2.Enabled = False
        TextBox3.Text = ""
        CargaConfig()
        CargaTodo()
        Label3.Text = SQL_Instancia.Trim
        TextBox1.Text = SQL_Instancia.Trim
        '
    End Sub

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
            Flen = 0
            Do
                line = sr.ReadLine()
                Flen += 1
                CaracterPrimero = Mid$(line, 1, 1)
                Select Case CaracterPrimero
                    '
                    ' La estructura del archivo plano .txt
                    '  esta pensada para recoger mas de un parámetro si se necesita...
                    '
                    Case "1" ' Instancia
                        SQL_Instancia = Mid$(line, 3, Len(line)).Trim
                End Select
                'Loop Until line = "END" Or line = Nothing Or sr.EndOfStream
            Loop Until line = Nothing Or sr.EndOfStream
            sr.Close()
            ReDim FileContent(Flen)
            '
        Catch MyE As Exception
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "C:\TRIVAGES\DATOS\CONA4Cfg.txt.")
        End Try
        '
    End Sub

    Public Sub CargaTodo()
        '
        ' Leemos el fichero "C:\TRIVAGES\DATOS\CONA4Cfg.txt".
        ' TODO su contenido y se almacena en FileContent.
        '
        Dim FicheroDefecto As String = "C:\TRIVAGES\DATOS\CONA4Cfg.txt"
        FileContent(0) = "" : Count = 0
        Try
            Dim sr As StreamReader = New StreamReader(FicheroDefecto)
            Dim line As String
            Do
                line = sr.ReadLine() & vbCrLf
                If Count <= Flen Then
                    FileContent(Count) = line
                End If
                Count += 1
            Loop Until line = Nothing Or sr.EndOfStream
            sr.Close()
            '
            TextBox3.Text = ""
            For Count = 0 To Flen - 1
                If FileContent(Count).Trim.Length > 0 Then
                    TextBox3.Text &= FileContent(Count)
                End If
            Next
            '
        Catch MyE As Exception
            MsgBox("No se ha podido leer el fichero " & FicheroDefecto & vbCrLf & MyE.Message,
                   MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation,
                   "C:\TRIVAGES\DATOS\CONA4Cfg.txt.")
            MsgBox(MyE.ToString)
        End Try
        '
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Application.Exit()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        Application.Exit()
        '
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        '
        If TextBox2.Text = "603335" Or TextBox2.Text = "758132" Then
            Button2.Enabled = True
            Button2.ForeColor = Color.Blue
        Else
            Button2.Enabled = False
            Button2.ForeColor = Color.Black
        End If
        '
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '
        SQL_CadenaConexion = SQL_Instancia & SQL_Catalogo & SQL_Seguridad_Otros
        '
        title = "¿Desea ACTUALIZAR la nueva instancia?"
        style = MsgBoxStyle.DefaultButton2 Or
                MsgBoxStyle.Critical Or
                MsgBoxStyle.YesNo
        msg = "Se reescribirá el archivo " & vbCrLf
        msg &= "C:\TRIVAGES\DATOS\CONA4Cfg.txt." & vbCrLf & vbCrLf
        msg &= "Nueva Instancia.: " & vbCrLf
        msg &= TextBox1.Text.Trim & vbCrLf & vbCrLf
        msg &= "Nueva Conexión.: " & vbCrLf
        msg &= SQL_CadenaConexion.Trim & vbCrLf
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            ActualizaInstancia()
        End If
        '
    End Sub

    Private Sub ActualizaInstancia()
        '
        ' Aqui se reescribe C:\TRIVAGES\DATOS\CONA4Cfg.txt
        '
        FileContent(0) = "1:" & TextBox1.Text.Trim & vbCrLf
        Label3.Text = TextBox1.Text.Trim
        TextBox3.Text = ""
        For Count = 0 To Flen - 1
            If FileContent(Count).Trim.Length > 0 Then
                TextBox3.Text &= FileContent(Count)
            End If
        Next
        '
        ' Rescritura C:\TRIVAGES\DATOS\CONA4Cfg.txt
        '
        File.WriteAllText("C:\TRIVAGES\DATOS\CONA4Cfg.txt", TextBox3.Text)
        '
    End Sub


End Class
