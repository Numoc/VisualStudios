Public Class ExplodingPixie
    Inherits SimpleFilmStripPixie
    Protected Explosion As FilmStrip


    Public Sub New()
        SetExplosion()
        MySize = Explosion.StoppedImage.Size
    End Sub


    Public Sub New(ByVal inPic As Image)

        SetExplosion()
        Explosion.StoppedImage = inPic
        MySize = inPic.Size


    End Sub
    Public Sub New(ByRef InPic As Image, ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer) ' makes a pixie with the input image at a random position and random speed between min and max
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
        SetExplosion()
        Explosion.StoppedImage = InPic
    End Sub
    Protected Sub SetExplosion()

        'MySize = My.Resources.exp1.Size
        'Explosion = New FilmStrip(Size)
        'Explosion.StoppedImage = New Bitmap(1, 1)
        'Explosion.Image = My.Resources.exp1
        'Explosion.Image = My.Resources.exp2
        'Explosion.Image = My.Resources.exp3
        'Explosion.Image = My.Resources.exp4
        'Explosion.Image = My.Resources.exp5
        'Explosion.Image = My.Resources.exp6
        'Explosion.Image = My.Resources.exp7
        'Explosion.Image = My.Resources.exp8
        'Explosion.Image = My.Resources.exp9
        'Explosion.Image = My.Resources.exp10
        'Explosion.Image = My.Resources.exp11
        'Explosion.Image = My.Resources.exp12
        Explosion.FilmSpeed = 1


        Explosion.ContinuousLoop = False
    End Sub
    Protected Overrides Sub MakePixie()
     
    End Sub
    Public Overridable Property Exploding As Boolean
        Get
            If Explosion.FilmStripHasRun = False Then

                If Explosion.FilmStripRunning Then
                    Return True
                End If
                Return False

            Else
                Enabled = False
                Return False
            End If

        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
  
    Public Overrides Property Image() As Image
        'if we are running a filmstrip, it is because we are blowing up.  at the end of the film, mark
        'visible false and die.
        Get
            If Enabled Then
                

                If Not Explosion.FilmStripHasRun Then
                    If Explosion.FilmStripRunning Then
                        Return Explosion.Image
                        If Explosion.FilmStripHasRun Then
                            Enabled = False
                            Explosion.StoppedImage = New Bitmap(1, 1)
                        End If
                    End If
                End If
            End If
            Return Explosion.StoppedImage
        End Get
        Set(ByVal value As Image)
            MyBase.image = value
        End Set
    End Property
    Public Overrides Sub DrawPixie(ByRef Grafix As System.Drawing.Graphics)
        If Enabled Then
            Dim myImage = Image
            If myImage.Size.Width > 1 Then
                Grafix.DrawImage(myImage, Position)
            Else
                Enabled = False
            End If
        End If


    End Sub
    Public Overrides Function bounceMe(ByRef InObject As pixie) As Boolean
        Dim startPosition As Point = MyPosition
        Dim startvelocity As Point = Myvelocity
        If Not Exploding Then
            If MyBase.BounceMe(InObject) Then

                IveBeenHit(1)
                'MyPosition = startPosition
                'Myvelocity = New Point(MySize.Width / 2 * Math.Sign(startvelocity.X), MySize.Width / 2 / Math.Abs(startvelocity.X) * Myvelocity.Y * -1)
                'move()
                Velocity = New Point(0, 0)

                'Dim x As Integer = Math.Sign((MyPosition.X - InObject.Position.X)) * MySize.Width / 2
                'Dim y As Integer = Math.Sign(MyPosition.Y - InObject.Position.Y) * MySize.Height / 2
                'MyPosition = MyPosition - New Point(x, y)

                Return True
            End If

        End If
        Return False
    End Function

    Public Overrides Sub IveBeenHit(ByVal damage As Integer)
        If Not Exploding Then

            
            Explosion.Start()
            Myvelocity = New Point(0, 0)

        End If


    End Sub



End Class
