Public Class SimpleFilmStripPixie
    Inherits pixie
    Protected RightFilm As FilmStrip 'Filmstrip for right movement
    Protected LeftFilm As FilmStrip  'filmstrip for Left Movement
    Protected UpFilm As FilmStrip     'filmstrip for up movement
    Protected DownFilm As FilmStrip   'Filmstrip for Down Movement
    Protected defaultVelocity As Integer = 2 'velocity when changed
    Protected PointedDirection As Integer '0 = up, 1 = right, 2 = down, 3 = left



    Public Sub New()
        Velocity = New Point(0, 0)
        setupfilmStrips()
    End Sub
    Protected Overridable Sub setUpFilmStrips()



        ' Your filmstrips should all be the same size, with the pixels/inch around 96.
        ' Draw them in Photoshop.  I find that four pictures look best for each direction.
        ' While the Right button is pressed, the rightfilm will run. The same happens for the other
        ' directions.  The final picture should be a stopped image, for instance a character in a 
        ' simple standing pose.
        'MySize = My.Resources.rocketRight1.Size
        'this is the filmstrip for right motion.  Load the pictures into your resources first.
        RightFilm = New FilmStrip(MySize)
        'RightFilm.Image = 
        'RightFilm.Image = 
        'RightFilm.Image =
        'RightFilm.Image = 
        'RightFilm.StoppedImage =  'this is the image to display when there is no motion.  
        RightFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        'is a rocket engine firing, I set it to true.  if it were an explosion, it would be false

        'Size = My.Resources.rocketRightStopped.Size  ' sets the size of your pixie to the size of your pictures.  remember, all your pictures
        ' need to be the same size!


        'Now I load the filmstrips for up, down and left motion

        LeftFilm = New FilmStrip(MySize)
        'LeftFilm.Image = 
        'LeftFilm.Image = 
        'LeftFilm.Image = 
        'LeftFilm.Image = 
        'LeftFilm.StoppedImage =  'this is the image to display when there is no motion.
        LeftFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        'is a rocket engine firing, I set it to true.  if it were an explosion, it would be false

        UpFilm = New FilmStrip(MySize)
        'UpFilm.Image = 
        'UpFilm.Image = 
        'UpFilm.Image = 
        'UpFilm.Image = 
        'UpFilm.StoppedImage =  'this is the image to display when there is no motion.
        UpFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        'is a rocket engine firing, I set it to true.  if it were an explosion, it would be false

        DownFilm = New FilmStrip(MySize)
        'DownFilm.Image =
        'DownFilm.Image = 
        'DownFilm.Image = 
        'DownFilm.Image = 
        'DownFilm.StoppedImage = My.Resources.rocketDownStopped 'this is the image to display when there is no motion.
        DownFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        'is a rocket engine firing, I set it to true.  if it were an explosion, it would be false



        'other housekeeping

        myImage = RightFilm.StoppedImage
        PointedDirection = 1 'right direction
    End Sub
    Public Overridable Property Direction As Integer
        Get
            Return PointedDirection
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    Public Overrides Property Image As System.Drawing.Image
        Get
            If RightFilm.FilmStripRunning Then
                Return RightFilm.Image
            ElseIf LeftFilm.FilmStripRunning Then
                Return LeftFilm.Image
            ElseIf DownFilm.FilmStripRunning Then
                Return DownFilm.Image
            ElseIf UpFilm.FilmStripRunning Then
                Return UpFilm.Image
            Else
                Return myImage
            End If
        End Get
        Set(ByVal value As System.Drawing.Image)

        End Set
    End Property
   
    Public Overridable Sub StopAllFilms()
        LeftFilm.Halt()
        UpFilm.Halt()
        DownFilm.Halt()
        RightFilm.Halt()  'fire moving to right

    End Sub
    Public Overridable Sub SetUpRightMovement()
        StopAllFilms()
        myImage = RightFilm.StoppedImage
        PointedDirection = 1

    End Sub
    Public Overridable Sub SetupleftMovement()
        StopAllFilms()
        myImage = LeftFilm.StoppedImage
        PointedDirection = 3
    End Sub


    Public Overridable Sub SetupDownwardMovement()
        StopAllFilms()
        myImage = DownFilm.StoppedImage
        PointedDirection = 2
    End Sub
    Public Overridable Sub SetupUpwardMovement()
        StopAllFilms()
        myImage = UpFilm.StoppedImage
        PointedDirection = 0
    End Sub
    Public Overrides Sub KeyDown(ByVal key As System.Windows.Forms.KeyEventArgs)
        Select Case key.KeyCode
            Case Keys.Right
                SetUpRightMovement()
                RightFilm.Start()
                Velocity = New Point(defaultVelocity, 0)
            Case Keys.Left
                SetupleftMovement()
                LeftFilm.Start()
                Velocity = New Point(-defaultVelocity, 0)
            Case Keys.Up
                SetupUpwardMovement()
                UpFilm.Start()

                Velocity = New Point(0, -defaultVelocity)
            Case Keys.Down
                SetupDownwardMovement()
                DownFilm.Start()
                Velocity = New Point(0, defaultVelocity)
        End Select

    End Sub
    Public Overrides Sub KeyUp(ByVal key As System.Windows.Forms.KeyEventArgs)
        Select Case key.KeyCode
            Case Keys.Right
                If RightFilm.FilmStripRunning Then
                    Velocity = New Point(0, 0)
                    RightFilm.Halt()
                End If

            Case Keys.Left
                If LeftFilm.FilmStripRunning Then
                    Velocity = New Point(0, 0)
                    LeftFilm.Halt()
                End If
            Case Keys.Up
                If UpFilm.FilmStripRunning Then
                    Velocity = New Point(0, 0)
                    UpFilm.Halt()
                End If
            Case Keys.Down
                If DownFilm.FilmStripRunning Then
                    Velocity = New Point(0, 0)
                    DownFilm.Halt()
                End If
        End Select
    End Sub
    Public Overrides Function WallBounce(ByRef InWall As Rectangle) As WallBounce

        Dim myWallBounce = New WallBounce()
        If Right() > InWall.Right Then
            If PointedDirection Mod 2 = 1 Then  'we are pointing left or right
                SetupleftMovement()
            End If
            Right = InWall.Right
            myWallBounce.Score("Right")
            Myvelocity.X = Myvelocity.X * -1
        End If
        If Left() < InWall.Left Then
            If PointedDirection Mod 2 = 1 Then  'we are pointing left or right
                SetUpRightMovement()
            End If
            Left = InWall.Left
            myWallBounce.Score("Left")
            Myvelocity.X = Myvelocity.X * -1
        End If
        If Bottom() > InWall.Bottom Then
            If PointedDirection Mod 2 = 0 Then  'we are pointing up or down
                SetupUpwardMovement()
            End If

            Bottom = InWall.Bottom
            myWallBounce.Score("Bottom")
            Myvelocity.Y = Myvelocity.Y * -1
        End If
        If Top() < InWall.Top Then
            If PointedDirection Mod 2 = 0 Then  'we are pointing up or down
                SetupDownwardMovement()
            End If

            Top = InWall.Top
            myWallBounce.Score("Top")
            Myvelocity.Y = Myvelocity.Y * -1
        End If
        Return myWallBounce
    End Function

End Class

