Public Class paddle
    Inherits pixie
    Protected paddleVel As Integer = 10
    Protected LeftKey As Boolean = False
    Protected RightKey As Boolean = False
    Protected UpKey As Boolean = False
    Protected DownKey As Boolean = False

    Public Sub New(ByVal InSize As Size) ' makes a pixie of the input size
        Type = "Paddle"
        MySize = InSize
        MakePixie()
    End Sub
    Public Sub New(ByVal InPosition As Point)  'makes a pixie at the input position
        Type = "Paddle"
        Position = InPosition
        MakePixie()
    End Sub
    Public Sub New(ByVal inPosition As Point, ByVal inVelocity As Point)  'makes a pixie at the input position and input velocity
        Position = inPosition
        Type = "Paddle"
        Velocity = inVelocity
        MakePixie()
    End Sub
    Public Sub New(ByVal Inposition As Point, ByVal InSize As Size) 'makes a pixie at the input position and of the input size
        Position = Inposition
        Type = "Paddle"
        MySize = InSize
        MakePixie()
    End Sub
    Public Sub New(ByVal InPic As Image) ' makes a pixie with the input image
        Type = "Paddle"
        myImage = InPic
        MySize = InPic.Size
    End Sub
    Public Sub New(ByVal InPic As Image, ByRef inWindow As Rectangle) 'makes a pixie with the input image and in a random position with the input window
        Type = "Paddle"
        myImage = InPic
        MySize = InPic.Size
        RandomPosition(inWindow)
    End Sub
    Public Sub New(ByVal InPic As Image, ByVal InPosition As Point) 'makes a pixie with the input image and at the input position
        Type = "Paddle"
        Position = InPosition
        myImage = InPic
        MySize = InPic.Size
    End Sub
    Public Sub New(ByRef InPic As Image, ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer) ' makes a pixie with the input image at a random position and random speed between min and max
        Type = "Paddle"
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
    End Sub
    Public Sub New(ByVal MinimumSpeed As Integer, ByVal MaximumSpeed As Integer) ' makes a pixie with a random speed between min and max
        Type = "Paddle"
        RandomVelocity(MinimumSpeed, MaximumSpeed)
    End Sub
    Public Sub New(ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer) 'makes a pixie with a random speed between min and max and at a random position within the window
        Type = "Paddle"
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
    End Sub
    Public Sub New(ByVal position As Point, ByVal Size As Size, ByVal Color As Color) 'makes a pixie at the input position and of the input size

        Type = "Paddle"
        position = position
        MySize = Size
        MakePixie()
    End Sub
    Public Sub New(ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer, ByVal minimumSize As Integer, ByVal maximumSize As Integer) ' makes a pixie of random size, velocity and within the window
        Type = "Paddle"
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
        RandomSize(minimumSize, maximumSize)
        RandomColor()
    End Sub

    Public Sub New()

        MyBase.New()
        Type = "Paddle"
        Color = Drawing.Color.Black
        Size = New Size(20, 100)
        MakePixie()
    End Sub
    Public Sub New(ByVal InWindow As Rectangle)
        MyBase.new()
        Type = "Paddle"
        Color = Drawing.Color.Black
        Size = New Size(20, 100)
        Velocity = New Point(0, 0)
        Position = New Point(20, InWindow.Height / 2 - Size.Height / 2)
        MakePixie()

    End Sub
    Protected Overrides Sub MakePixie()
        myImage = New Bitmap(Size.Width, Size.Height)
        Dim Grafix As System.Drawing.Graphics
        Grafix = System.Drawing.Graphics.FromImage(myImage)
        Grafix.FillRectangle(New SolidBrush(Color), 0, 0, MyBase.Size.Width, MyBase.Size.Height)

    End Sub

    Public Overrides Sub KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Right
                If Not RightKey Then
                    Velocity = New Point(paddleVel, Velocity.Y)
                    RightKey = True
                    LeftKey = False
                End If

            Case Keys.Left
                If Not LeftKey Then
                    Velocity = New Point(-paddleVel, Velocity.Y)
                    RightKey = False
                    LeftKey = True

                End If

            Case Keys.Up
                If Not UpKey Then
                    Velocity = New Point(Velocity.X, -paddleVel)
                    DownKey = False
                    UpKey = True
                End If

            Case Keys.Down
                If Not DownKey Then
                    Velocity = New Point(Velocity.X, paddleVel)
                    UpKey = False
                    DownKey = True
                End If

            Case Keys.Space
                'WarpSpeed = 2
        End Select
    End Sub
    Public Overrides Sub KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Right
                If RightKey Then
                    Velocity = New Point(0, Velocity.Y)
                    RightKey = False
                End If

            Case Keys.Left
                If LeftKey Then
                    Velocity = New Point(0, Velocity.Y)
                    LeftKey = False
                End If

            Case Keys.Up
                If UpKey Then
                    Velocity = New Point(Velocity.X, 0)
                    UpKey = False
                End If

            Case Keys.Down
                If DownKey Then
                    Velocity = New Point(Velocity.X, 0)
                    DownKey = False
                End If

            Case Keys.Space
                'WarpSpeed = 2
        End Select
    End Sub
    Public Overrides Function WallBounce(ByRef InWall As Rectangle) As WallBounce

        BounceAction = "Stop"
        Dim myWallBounce = New WallBounce()
        If Right() > InWall.Right Then
            Velocity = New Point(0, Velocity.Y)
            Right = InWall.Right
            myWallBounce.Score("Right")
        End If
        If Left() < InWall.Left Then
            Velocity = New Point(0, Velocity.Y)
            Left = InWall.Left
            myWallBounce.Score("Left")
        End If
        If Bottom() > InWall.Bottom Then
            Velocity = New Point(Velocity.X, 0)
            Bottom = InWall.Bottom
            myWallBounce.Score("Bottom")
        End If
        If Top() < InWall.Top Then
            Velocity = New Point(Velocity.X, 0)
            Top = InWall.Top
            myWallBounce.Score("Top")
        End If
        Return myWallBounce
    End Function
    Protected Overrides Sub StopY()
        Velocity = New Point(Velocity.X, 0)
        If DownKey Then
            DownKey = False
        End If
        If UpKey Then
            UpKey = False
        End If
    End Sub
    Protected Overrides Sub stopX()
        Velocity = New Point(0, Velocity.Y)
        If RightKey Then
            RightKey = False
        End If
        If LeftKey Then
            LeftKey = False
        End If
    End Sub
End Class
