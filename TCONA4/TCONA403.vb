Public Class TCONA403
    Private Sub TCONA403_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        ' Cargar Lista Zoom (Vista Ampliada)
        '
        GRID1_a_GRIDZOOM(0)
        '
    End Sub

    Private Sub TCONA403_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Hide()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub ButtonCABSALIR_Click(sender As Object, e As EventArgs) Handles ButtonCABSALIR.Click
        Me.Hide()
    End Sub

    Private Sub ButtonGRIDArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDZOOM
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
        With GRIDZOOM
            If .Rows.Count > 0 Then
                '
                ' Num. de Filas - 3, Evitamos Lineas Finales Espacio y TOTAL
                ' Fila Actual
                '
                Dim GrNumRows As Integer = .Rows.Count - 3
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

    Private Sub ButtonVerCombi_Click(sender As Object, e As EventArgs) Handles ButtonVerCombi.Click
        '
        ' Mostrar Los Combinados en el GRID...
        '
        GRID1_a_GRIDZOOM(3)
        '
    End Sub
End Class