Public Class Board
    Dim squares As ArrayList
    Dim mygrafix As System.Drawing.Graphics
    Dim rows As Integer
    Public cols As Integer
    Dim upleftcorner As Point
    Dim botrow As Integer
    Dim squaresize As Integer
    Public Sub New(inupleftcorner As Point, incols As Integer, inrows As Integer, insquaresize As Integer, grafix As System.Drawing.Graphics)
        rows = inrows
        botrow = inrows
        cols = incols
        upleftcorner = inupleftcorner
        squaresize = insquaresize
        mygrafix = grafix
        squares = New ArrayList()
        For currow As Integer = 0 To (rows - 1)
            For curcol As Integer = 0 To (cols - 1)
                squares.Add(New Square(New Point(curcol * squaresize, currow * squaresize) + upleftcorner, squaresize))
                squares(currow * cols + curcol).DrawSquare(grafix)
            Next
        Next
    End Sub
    Public Sub move(distance As Integer)
        For Each square As Square In squares
            square.move(distance)
        Next
    End Sub
    Public Sub drawBoard(grafix)
        For Each square As Square In squares
            square.DrawSquare(mygrafix)
        Next
    End Sub
    Public Sub Cycle(botpos As Integer)
        If squares(botrow * cols - 1).position.y >= botpos Then
            For cursquare As Integer = (botrow * cols - cols) To ((botrow * cols) - 1)
                squares(cursquare).move(-squaresize * rows)
                squares(cursquare).changecolor()
            Next
        End If

        botrow -= 1
        If botrow < 1 Then
            botrow = rows
        End If
    End Sub
End Class
