Public Class SpaceShip
    Inherits SimpleFilmStripPixie
    
    Protected MaxVelocity As Integer = 5
    Protected velx As Integer = 0
    Protected vely As Integer = 0
    'Protected myExplosionSize As Size = New Size(400, 400)
    Protected ImDead As Boolean = False
    Protected ShieldHitFilm As FilmStrip
    Protected ShieldUp As Boolean = False

    Public Sub New()
        Velocity = New Point(0, 0)
       


    End Sub
    Public Overrides Property Image As System.Drawing.Image
        Get
            If Not ShieldHitFilm.FilmStripRunning Then
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
            Else

                Return ShieldHitFilm.Image
            End If

        End Get
        Set(ByVal value As System.Drawing.Image)

        End Set
    End Property
    Public Sub New(ByVal InWindow As Rectangle)

        'MySize = My.Resources.rocketRight1.Size
        Position = New Point(InWindow.Width / 2 - Size.Width / 2, InWindow.Height - Size.Height - 60)
        setupFilmstrips()
        Myvelocity = New Point(0, 0)


    End Sub
    Protected Overrides Sub MakePixie()
        'MySize = My.Resources.rocketRight1.Size
        setUpFilmStrips()
        setUpShields()
       
    End Sub
    Protected Overridable Sub setUpShields()
        ShieldHitFilm = New FilmStrip(MySize)
        Dim S As pixie = New pixie
        S.Color = Color.Yellow
        S.Size = MySize

        ShieldHitFilm.Image = S.Image
        ShieldHitFilm.Image = S.Image
        ShieldHitFilm.Image = S.Image

        ShieldHitFilm.Image = S.Image

        ShieldHitFilm.Image = S.Image
        'ShieldHitFilm.StoppedImage = My.Resources.RocketShipStopped
        ShieldHitFilm.ContinuousLoop = False  'sets if the filmstrip will play in a loop until stopped.  since this 
    End Sub


    Protected Overrides Sub setUpFilmStrips()
        ' Your filmstrips should all be the same size, with the pixels/inch around 96.
        ' Draw them in Photoshop.  I find that four pictures look best for each direction.
        ' While the Right button is pressed, the rightfilm will run. The same happens for the other
        ' directions.  The final picture should be a stopped image, for instance a character in a 
        ' simple standing pose.

        'this is the filmstrip for right motion.  Load the pictures into your resources first.
        'RightFilm = New FilmStrip(MySize)
        'RightFilm.Image = My.Resources.rocketRight1
        'RightFilm.Image = My.Resources.rocketRight2
        'RightFilm.Image = My.Resources.rocketRight3
        'RightFilm.Image = My.Resources.rocketRight4
        'RightFilm.StoppedImage = My.Resources.RocketShipStopped
        RightFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        'is a rocket engine firing, I set it to true.  if it were an explosion, it would be false

        'Size = My.Resources.rocketRightStopped.Size  ' sets the size of your pixie to the size of your pictures.  remember, all your pictures
        ' need to be the same size!


        'Now I load the filmstrips for up, down and left motion

        LeftFilm = New FilmStrip(MySize)
        'LeftFilm.Image = My.Resources.rocketLeft1
        'LeftFilm.Image = My.Resources.rocketLeft2
        'LeftFilm.Image = My.Resources.rocketLeft3
        'LeftFilm.Image = My.Resources.rocketLeft4
        'LeftFilm.StoppedImage = My.Resources.rocketLeftStopped
        'LeftFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        ''is a rocket engine firing, I set it to true.  if it were an explosion, it would be false

        'UpFilm = New FilmStrip(MySize)
        'UpFilm.Image = My.Resources.rocketUp1
        'UpFilm.Image = My.Resources.rocketUp2
        'UpFilm.Image = My.Resources.rocketUp3
        'UpFilm.Image = My.Resources.rocketUp4
        'UpFilm.StoppedImage = My.Resources.rocketUpStopped
        'UpFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        ''is a rocket engine firing, I set it to true.  if it were an explosion, it would be false

        'DownFilm = New FilmStrip(MySize)
        'DownFilm.Image = My.Resources.rocketDown1
        'DownFilm.Image = My.Resources.rocketDown2
        'DownFilm.Image = My.Resources.rocketDown3
        'DownFilm.Image = My.Resources.rocketDown4
        'DownFilm.StoppedImage = My.Resources.rocketDownStopped 'this is the image to display when there is no motion.
        DownFilm.ContinuousLoop = True  'sets if the filmstrip will play in a loop until stopped.  since this 
        'is a rocket engine firing, I set it to true.  if it were an explosion, it would be false



        'other housekeeping
        Velocity = New Point(0, 0)
        myImage = RightFilm.StoppedImage
    End Sub
    Public Sub accelerateX(ByVal direction As Integer)
        If Myvelocity.X = 0 Then
            Myvelocity.X = direction

        Else
            Myvelocity.X = Myvelocity.X + direction * 1
            If Myvelocity.X > 5 Then
                Myvelocity.X = MaxVelocity
            Else
                If Myvelocity.X < -MaxVelocity Then
                    Myvelocity.X = -MaxVelocity
                End If
            End If
        End If

    End Sub
    Public Sub accelerateY(ByVal direction As Integer)
        If Myvelocity.Y = 0 Then
            Myvelocity.Y = direction

        Else
            Myvelocity.Y = Myvelocity.Y + direction * 1
            If Myvelocity.Y > 5 Then
                Myvelocity.Y = MaxVelocity
            Else
                If Myvelocity.Y < -MaxVelocity Then
                    Myvelocity.Y = -MaxVelocity
                End If
            End If
        End If

    End Sub
   
    Public Overrides Sub KeyDown(ByVal key As System.Windows.Forms.KeyEventArgs)
        Select Case key.KeyCode
            Case Keys.Right
                SetUpRightMovement()
                RightFilm.Start()
                If Myvelocity.X = 0 Then
                    Myvelocity.X = defaultVelocity
                Else
                    accelerateX(1)
                End If

            Case Keys.Left
                SetupleftMovement()
                LeftFilm.Start()
                If Myvelocity.X = 0 Then
                    Myvelocity.X = -defaultVelocity
                Else
                    accelerateX(-1)
                End If

            Case Keys.Up
                SetupUpwardMovement()
                UpFilm.Start()
                If Myvelocity.Y = 0 Then
                    Myvelocity.Y = -defaultVelocity
                Else
                    accelerateY(-1)
                End If
            Case Keys.Down
                SetupDownwardMovement()
                DownFilm.Start()
                If Myvelocity.Y = 0 Then
                    Myvelocity.Y = defaultVelocity
                Else
                    accelerateY(1)
                End If

        End Select

    End Sub
    Public Overrides Sub KeyUp(ByVal key As System.Windows.Forms.KeyEventArgs)
        Select Case key.KeyCode
            Case Keys.Right
                If RightFilm.FilmStripRunning Then
                    'Velocity = New Point(0, 0)
                    RightFilm.Halt()
                End If

            Case Keys.Left
                If LeftFilm.FilmStripRunning Then
                    'Velocity = New Point(0, 0)
                    LeftFilm.Halt()
                End If
            Case Keys.Up
                If UpFilm.FilmStripRunning Then
                    'Velocity = New Point(0, 0)
                    UpFilm.Halt()
                End If
            Case Keys.Down
                If DownFilm.FilmStripRunning Then
                    'Velocity = New Point(0, 0)
                    DownFilm.Halt()
                End If
        End Select
    End Sub
    
    Public Overrides Sub IveBeenHit(ByVal damage As Integer)
       
        ShieldHitFilm.Start()





    End Sub
End Class
