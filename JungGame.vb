Public Class JungGame
    Dim botpos As Integer
    Dim myImage As Image
    Dim grafix As System.Drawing.Graphics
    Public sprites As PixieBucket
    Dim panel As Rectangle
    Dim mypaddle As paddle
    Dim myboard As Board
    Dim playerpixie As PlayerPixie

    Public Sub New(panelsize As Size)
        Randomize()
        panel = New Rectangle(0, 0, panelsize.Width, panelsize.Height)
        botpos = panel.Height - 180
        myImage = New Bitmap(panel.Width, panel.Height)
        grafix = System.Drawing.Graphics.FromImage(myImage)
        sprites = New PixieBucket
        mypaddle = New paddle()
        myboard = New Board(New Point(0, 0), 5, 6, panel.Height - botpos, grafix)
        playerpixie = New PlayerPixie(New Size(panel.Width / myboard.cols / 2, panel.Width / myboard.cols / 2), New Point(panel.Width / 2 - panel.Width / myboard.cols / 4, botpos - panel.Width / myboard.cols / (4 / 3)))
    End Sub
    Public Function getImage()
        grafix.Clear(Color.Transparent)

        'myboard.move(1)
        myboard.drawBoard(grafix)
        grafix.DrawLine(Pens.Yellow, New Point(0, botpos), New Point(panel.Width, botpos))
        playerpixie.Master(myboard, botpos)
        playerpixie.move()
        playerpixie.WallBounce(New Rectangle(0, 0, panel.Width, botpos))
        playerpixie.DrawPixie(grafix)
        Return myImage
    End Function
    Public Sub addsprite(ByVal inSprite As pixie)
        sprites.AddPixie(inSprite)
    End Sub
    Public Sub keydown(ByVal inkey As System.Windows.Forms.KeyEventArgs)
        playerpixie.KeyDown(inkey)
    End Sub
    Public Sub keyup(ByVal inkey As System.Windows.Forms.KeyEventArgs)
        playerpixie.KeyUp(inkey)
    End Sub
End Class

