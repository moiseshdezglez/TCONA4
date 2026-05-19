Public Class TCONA419

    Dim CajaSeleccion As Integer = 1

    Private Sub TCONA419_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        Select Case e.KeyCode
            Case Keys.Escape
                HazSALIR()
        End Select
        e = Nothing
        '
    End Sub

    Private Sub TCONA419_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Mostramos en Una Lista las cajas disponibles.
        '
        CargaListaCajas()
        '
    End Sub

    Private Sub HazSALIR()
        '
        ' Al salir sabremos con que Caja se va a trabajar
        '  y cual será en FORM inicial de la Aplicación.
        '
        wCaja = CajaSeleccion
        HayCambiodeCaja(0)
        '
        Select Case FormularioInicial
            Case 0
                With MyFrm1
                    .ShowDialog(Me)
                End With
            Case 1
                With MyFrm2
                    .ShowDialog(Me)
                End With
        End Select
        Me.Hide()
        '
    End Sub

    Private Sub Button19Salir_Click(sender As Object, e As EventArgs) Handles Button19Salir.Click
        HazSALIR()
    End Sub

    Private Sub GRIDCAJAS_SelectionChanged(sender As Object, e As EventArgs) Handles GRIDCAJAS.SelectionChanged
        '
        If GRIDCAJAS.SelectedRows.Count > 0 Then
            CajaSeleccion = CInt(GRIDCAJAS.SelectedCells(0).Value.ToString)
        End If
        '
    End Sub

    Private Sub GRIDCAJAS_Click(sender As Object, e As EventArgs) Handles GRIDCAJAS.Click
        '
        If GRIDCAJAS.SelectedRows.Count > 0 Then
            CajaSeleccion = CInt(GRIDCAJAS.SelectedCells(0).Value.ToString)
        End If
        '
    End Sub

    Private Sub ButtonGRIDCLArriba_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCLArriba.Click
        '
        ' Subir una linea en el GRID
        '
        With GRIDCAJAS
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

    Private Sub ButtonGRIDCLAbajo_Click(sender As Object, e As EventArgs) Handles ButtonGRIDCLAbajo.Click
        '
        ' Bajar una linea en el GRID
        '
        With GRIDCAJAS
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
End Class