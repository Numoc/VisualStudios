Imports Jung

Public Class PlayerPixie
    Inherits pixie
    Dim keysdown As BitArray
    Dim moveamount As Integer
    Public Sub New(insize As Size, position As Point)
        Myvelocity = New Point(0, 0)
        keysdown = New BitArray(10)
        MySize = insize
        moveamount = 5
        MyPosition = position
        MakePixie()
    End Sub
    Public Overrides Sub KeyDown(e As KeyEventArgs)
        If e.KeyCode = Keys.W Then
            'Myvelocity = New Point(0, -1)
            keysdown(0) = True
        End If
        If e.KeyCode = Keys.S Then
            'Myvelocity = New Point(0, 1)
            keysdown(1) = True
        End If
        If e.KeyCode = Keys.A Then
            'Myvelocity = New Point(-1, 0)
            keysdown(2) = True
        End If
        If e.KeyCode = Keys.D Then
            'Myvelocity = New Point(1, 0)
            keysdown(3) = True
        End If
    End Sub
    Public Overrides Sub KeyUp(e As KeyEventArgs)
        If e.KeyCode = Keys.W Then
            'Myvelocity = New Point(0, 0)
            keysdown(0) = False
        End If
        If e.KeyCode = Keys.S Then
            'Myvelocity = New Point(0, 0)
            keysdown(1) = False
        End If
        If e.KeyCode = Keys.A Then
            'Myvelocity = New Point(0, 0)
            keysdown(2) = False
        End If
        If e.KeyCode = Keys.D Then
            'Myvelocity = New Point(0, 0)
            keysdown(3) = False
        End If
    End Sub
    Public Sub Master(board As Board, botpos As Integer)
        If keysdown(0) Then
            'MyPosition.Y -= moveamount
            board.move(moveamount)
            board.Cycle(botpos)
        End If
        'If keysdown(1) Then
        '    MyPosition.Y += moveamount
        'End If
        If keysdown(2) Then
            MyPosition.X -= moveamount
        End If
        If keysdown(3) Then
            MyPosition.X += moveamount
        End If
    End Sub
End Class
