Public Class WallBounce
    Public Right As Integer
    Public Left As Integer
    Public Top As Integer
    Public Bottom As Integer
    Public Sub New()
    End Sub
    Public Sub Add(ByVal InWB As WallBounce)
        Right = Right + InWB.Right
        Left = Left + InWB.Left
        Top = Top + InWB.Top
        Bottom = Bottom + InWB.Bottom
    End Sub
    Public Sub Score(ByVal instr As String)
        Select Case instr
            Case "Left"
                Left = Left + 1
            Case "Right"
                Right = Right + 1
            Case "Top"
                Top = Top + 1
            Case "Bottom"
                Bottom = Bottom + 1
        End Select
    End Sub
End Class
