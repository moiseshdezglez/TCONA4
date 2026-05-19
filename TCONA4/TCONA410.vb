Public Class TCONA410
    Private Sub TCONA410_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonMant2.Click
        '
        ' Formulario Mantenimiento de COMBINADOS -MODAL-
        '
        MyFrm7.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonMant1_Click(sender As Object, e As EventArgs) Handles ButtonMant1.Click
        '
        ' Mantenimiento SALAS/MESAS
        ' Usamos SwModSala para informar de que puden haber cambios de COLOR
        '   en la botonera SALAS o MESAS.
        '
        SwModSala = True
        '
        Dim startInfo As System.Diagnostics.ProcessStartInfo
        Dim pStart As New System.Diagnostics.Process
        startInfo = New System.Diagnostics.ProcessStartInfo("C:\TRIVAGES\WTPMESAS.exe")
        '
        pStart.StartInfo = startInfo
        pStart.Start()
        pStart.WaitForExit()
        '
    End Sub

    Private Sub ButtonMVSalir_Click(sender As Object, e As EventArgs) Handles ButtonMVSalir.Click
        Me.Hide()
    End Sub

    Private Sub ButtonMant3_Click(sender As Object, e As EventArgs) Handles ButtonMant3.Click
        '
        Dim startInfo As System.Diagnostics.ProcessStartInfo
        Dim pStart As New System.Diagnostics.Process
        startInfo = New System.Diagnostics.ProcessStartInfo("C:\TRIVAGES\TAFAMI.exe")
        '
        pStart.StartInfo = startInfo
        pStart.Start()
        pStart.WaitForExit()
        '
    End Sub

    Private Sub ButtonMant5_Click(sender As Object, e As EventArgs) Handles ButtonMant5.Click
        '
        Dim startInfo As System.Diagnostics.ProcessStartInfo
        Dim pStart As New System.Diagnostics.Process
        startInfo = New System.Diagnostics.ProcessStartInfo("C:\TRIVAGES\TACALM.exe")
        '
        pStart.StartInfo = startInfo
        pStart.Start()
        pStart.WaitForExit()
        '
    End Sub

    Private Sub ButtonMant4_Click(sender As Object, e As EventArgs) Handles ButtonMant4.Click
        '
        Dim startInfo As System.Diagnostics.ProcessStartInfo
        Dim pStart As New System.Diagnostics.Process
        startInfo = New System.Diagnostics.ProcessStartInfo("C:\TRIVAGES\WDAAPER.exe")
        '
        pStart.StartInfo = startInfo
        pStart.Start()
        pStart.WaitForExit()
        '
    End Sub

    Private Sub ButtonMant6_Click(sender As Object, e As EventArgs) Handles ButtonMant6.Click
        '
        Dim startInfo As System.Diagnostics.ProcessStartInfo
        Dim pStart As New System.Diagnostics.Process
        startInfo = New System.Diagnostics.ProcessStartInfo("C:\TRIVAGES\TAVENDEDORES.exe")
        '
        pStart.StartInfo = startInfo
        pStart.Start()
        pStart.WaitForExit()
        '
    End Sub

    Private Sub ButtonConsu1_Click(sender As Object, e As EventArgs) Handles ButtonConsu1.Click
        '
        ' Formulario Mantenimiento de COMBINADOS -MODAL-
        '
        MyFrm11.ShowDialog(Me)
        '
    End Sub

    Private Sub ButtonMantClaves_Click(sender As Object, e As EventArgs) Handles ButtonMantClaves.Click
        '
        TCONA420_Started = False
        MyFrm20.ShowDialog(Me)
        '
    End Sub
End Class