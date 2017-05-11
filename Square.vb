Public Class Square
    Public position As New Point
    Public size As New Integer
    Public color As New Color
    Public Sub New(inposition As Point, insize As Integer)
        position = inposition
        size = insize
        changecolor()
    End Sub
    Public Sub DrawSquare(grafix As System.Drawing.Graphics)
        grafix.FillRectangle(New SolidBrush(color), position.X, position.Y, size, size)
    End Sub
    Public Sub move(distance As Integer)
        position.Y += distance
    End Sub
    Public Sub changecolor()
        Dim randdouble As Double
        randdouble = Rnd()
        If randdouble >= 0.5 Then
            color = Color.Blue
        Else
            color = Color.Black
        End If
    End Sub
End Class
