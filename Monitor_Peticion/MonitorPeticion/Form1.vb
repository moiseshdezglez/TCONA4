
Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    '
    Dim SwTrueFalse As Boolean = True
    Dim SQL_Instancia As String = "Data Source=DESKTOP-FARI\SQLEXPRESS_2014;"
    Dim MyValue As String = "di9390996619"
    Dim SQL_Catalogo As String = "Initial Catalog=GESTITRV;"
    Dim SQL_Seguridad_Otros As String = "Integrated Security=False;User ID=sa;Password=" & MyValue & ";
                                       Connect Timeout=15;
                                       Encrypt=False;
                                       TrustServerCertificate=True;
                                       ApplicationIntent=ReadWrite;
                                       MultiSubnetFailover=False;MultipleActiveResultSets=True"

    Dim connectionString As String = SQL_Instancia & SQL_Catalogo & SQL_Seguridad_Otros
    Dim query As String = "SELECT * FROM PETICION WHERE NOT CAMPO05 = '9'"
    Dim updateQuery As String = "UPDATE PETICION SET CAMPO05 = '9' WHERE PDA = @PDA"
    Dim FicheroTxt As String = ""
    '
    ' Pruebas y Depuración ( INSERTA un nuevo registro! )
    Dim cmdINSERT As String = ""
    Dim NumPda As Integer = 1
    Dim Insertar As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        PreparaForm()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        '
    End Sub

    Private Sub disparador()
        '
        ListBox2.Items.Add(" *** MODO MONITORIZACION = ON! ***")
        '
        Using connection As New SqlConnection(connectionString)
            Dim PDA As Integer = 0
            connection.Open()
            While SwTrueFalse
                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            PDA = reader("PDA")
                            '
                            ' Formato registro a escribir
                            'Dim record As String = String.Format(
                            '"PDA: {0}, TIPO: {1}, ESTADO: {2}, VALOR: {3}, CAMPO01: {4}, CAMPO02: {5}, CAMPO03: {6}, CAMPO04: {7}, CAMPO05: {8}",
                            'reader("PDA"), reader("TIPO"), reader("ESTADO"), reader("VALOR"), reader("CAMPO01"), reader("CAMPO02"), reader("CAMPO03"),
                            'reader("CAMPO04"), reader("CAMPO05"))
                            '
                            ' Formato registro a escribir
                            Dim record As String = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
                            reader("PDA"), reader("TIPO"), reader("ESTADO"), reader("VALOR"), reader("CAMPO01"), reader("CAMPO02"), reader("CAMPO03"),
                            reader("CAMPO04"), reader("CAMPO05"))
                            '
                            ' Escribe en un archivo externo
                            FicheroTxt = ""
                            FicheroTxt &= "C:\DATOS\Peticion_TXT\"
                            FicheroTxt &= "PdaPeticion" + String.Format("{0:00}", PDA) + ".txt"
                            File.AppendAllText(FicheroTxt.Trim, record.ToString & Environment.NewLine)
                            ListBox1.Items.Add(record & Environment.NewLine)
                            '
                            ' Actualiza el campo CAMPO05='9'
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@PDA", PDA)
                                updateCommand.ExecuteNonQuery()
                                ListBox2.Items.Add(" ACTUALIZANDO TABLA [PETICION] | CAMPO05 = '9'")
                            End Using
                            '
                        End While
                    End Using
                    '
                    ' Para Depuracion genero nuevos registros!
                    ' Siempre que CAMPO05 no sea = 9, dispara las acciones oportunas
                    '
                    If Insertar = True Then
                        NumPda += 1
                        cmdINSERT = ""
                        cmdINSERT &= "INSERT INTO [dbo].[PETICION] ([PDA], [TIPO], [ESTADO], [VALOR], [CAMPO01], [CAMPO02], [CAMPO03], [CAMPO04], [CAMPO05]) "
                        cmdINSERT &= "VALUES "
                        cmdINSERT &= "(" & NumPda & ", 123, 99, 'valor','','','','','') "
                        Using updateCommand As New SqlCommand(cmdINSERT, connection)
                            updateCommand.ExecuteNonQuery()
                        End Using
                        ListBox2.Items.Add(" INSERTO REGISTRO NUEVO! TABLA -> [PETICION]")
                        ListBox2.Items.Add("   |-> " & cmdINSERT)
                        Insertar = False
                    End If
                    '
                End Using
                Application.DoEvents()
                Threading.Thread.Sleep(500) ' Espera de milisegundos antes de volver a verificar
                Application.DoEvents()
                Me.Refresh()
            End While
            '
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
            Application.Exit()
            '
        End Using
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        Button1.Enabled = False
        Button3.Enabled = True
        disparador()
        '
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '
        SwTrueFalse = False
        Button1.Enabled = True
        '
    End Sub

    Private Sub PreparaForm()
        '
        With Me
            .BackColor = Color.PaleTurquoise
            .KeyPreview = True
            .FormBorderStyle = FormBorderStyle.FixedToolWindow
        End With
        '
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '
        Insertar = True
        '
    End Sub
End Class
