Public Class TCONA416
    Private Sub TCONA416_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub Button16Salir_Click(sender As Object, e As EventArgs) Handles Button16Salir.Click
        Me.Hide()
    End Sub

    Private Sub Button16Envia_Click(sender As Object, e As EventArgs) Handles Button16Envia.Click
        '
        ' Boton ENVIA = Botón APARCAR
        '
        Me.Hide()
        '
        ' Boton APARCAR
        '
        If SwAparca Then
            Aparcar(0)
        End If
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
        If MyFrm2.GRID1.Rows.Count > 0 Then
            '
            ' En este PUNTO interesa Actualizar en Nro. de Personas (PAX) en la mesa.
            ' [SALA1].[PAX]
            '
            If MyFrm2.TextBoxPax.Text.Trim.Length = 0 Then
                MyFrm2.TextBoxPax.Text = "0"
            End If
            ActualizaMesa_SALA1(wCaja, MyFrm2.TextBoxNumSala.Text.Trim, MyFrm2.TextBoxNumMesa.Text.Trim, 4)
            '
            ' La llamada a estos procedimientos Actualizará
            '    controlando en los mismos que SOLO
            '    actualiza las líneas NUEVAS, ignorando las YA existentes...
            '
            GrabaDatosMesa("MESAC", MyFrm2.TextBoxNumMesa.Text.Trim)
            GrabaDatosMesa("MESA", MyFrm2.TextBoxNumMesa.Text.Trim)
        End If
        '
        ' Impresion TICKETS a Areas determinadas.
        '
        GeneraTICKETSaAreas(0)
        '
        If AparcaOpcion = 0 Then
            Me.Hide()
            MyFrm2.Hide()
            MyFrm1.Show()
        End If
    End Sub



    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDENVAREAS
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

    Private Sub ButtonGRIDAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDENVAREAS
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