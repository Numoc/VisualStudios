Public Class pixie
    Protected MyPosition As Point
    Protected Myvelocity As Point = New Point(4, 5) ' this is the default speed at start 
    Protected myImage As Image
    Protected MySize As Size = New Size(50, 50)
    Protected MyColor As Color = Color.Red ' default color
    Protected mySpeedLimit As Integer = 15  'the pixie will never get beyond this value in speed
    Public Enabled As Boolean = True
    Protected BounceAction As String  'flag if we should flip or stop the pixie when it bounces
    Protected myRectangle As Rectangle
    Protected myMaxLives As Integer = 100 ' measure of how much damage power it talkes to kill me
    Protected myLifeForce As Integer = myMaxLives
    Protected myType As String = "Pixie"
    Protected myPower As Integer = 1  ' how much damage you do to other objects.  1 == min, 100 = max


    '   Class Pixie Version 2.1 11/30/15 Grant Bower
    '   A pixie is an object that can move around the screen and interact with the walls and other pixie objects.  It has position, velocity, image, size (for collision boundries) and a default color and round shape (if no image is input)
    '

    Public Sub New()
        MakePixie()
    End Sub
    Public Sub New(ByVal InWindow As Rectangle)  '  if you make a pixie with an input window, the pixie will be made starting in a random position inside that window.
        RandomPosition(InWindow)
        MakePixie()
    End Sub
    Public Sub New(ByVal InSize As Size) ' makes a pixie of the input size
        MySize = InSize
        MakePixie()
    End Sub
    Public Sub New(ByVal InPosition As Point)  'makes a pixie at the input position
        Position = New Point(InPosition.X - MySize.Width / 2, InPosition.Y - MySize.Height / 2)
        MakePixie()
    End Sub
    Public Sub New(ByVal inPosition As Point, ByVal inVelocity As Point)  'makes a pixie at the input position and input velocity
        Position = inPosition
        Velocity = inVelocity
        MakePixie()
    End Sub
    Public Sub New(ByVal Inposition As Point, ByVal InSize As Size) 'makes a pixie at the input position and of the input size
        Position = Inposition
        MySize = InSize
        MakePixie()
    End Sub
    Public Sub New(ByVal InPic As Image) ' makes a pixie with the input image
        myImage = InPic
        MySize = InPic.Size
    End Sub
    Public Sub New(ByVal InPic As Image, ByRef inWindow As Rectangle) 'makes a pixie with the input image and in a random position with the input window
        myImage = InPic
        MySize = InPic.Size
        RandomPosition(inWindow)
    End Sub
    Public Sub New(ByVal InPic As Image, ByVal InPosition As Point) 'makes a pixie with the input image and at the input position
        Position = InPosition
        myImage = InPic
        MySize = InPic.Size
    End Sub
    Public Sub New(ByRef InPic As Image, ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer) ' makes a pixie with the input image at a random position and random speed between min and max
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
    End Sub
    Public Sub New(ByVal MinimumSpeed As Integer, ByVal MaximumSpeed As Integer) ' makes a pixie with a random speed between min and max
        RandomVelocity(MinimumSpeed, MaximumSpeed)
    End Sub
    Public Sub New(ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer) 'makes a pixie with a random speed between min and max and at a random position within the window
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
    End Sub
    Public Sub New(ByVal position As Point, ByVal Size As Size, ByVal Color As Color) 'makes a pixie at the input position and of the input size
        position = position
        MySize = Size
        MakePixie()
    End Sub
    Public Sub New(ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer, ByVal minimumSize As Integer, ByVal maximumSize As Integer) ' makes a pixie of random size, velocity and within the window
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)
        RandomSize(minimumSize, maximumSize)
        RandomColor()
    End Sub

    Protected Overridable Sub ScaleImage(ByVal inImage As Image)

        Dim myScaleX As Single = (MySize.Width / inImage.Width)
        Dim myScaleY As Single = (MySize.Width / inImage.Width)

        myImage = New Bitmap(MySize.Width, MySize.Height)
        Dim grafix As System.Drawing.Graphics
        grafix = System.Drawing.Graphics.FromImage(myImage)
        grafix.ResetTransform()
        grafix.Clear(Drawing.Color.Transparent)
        grafix.ScaleTransform(myScaleX, myScaleY)
        'grafix.TranslateTransform(0, 0)

        grafix.DrawImage(inImage, New Point(0, 0))
        grafix.Dispose()

    End Sub

    Public Overridable Sub RandomPosition(ByRef inWindow As Rectangle)
        Position = New Point(Fix((InWindow.Width - MySize.Width) * Rnd(Now.Millisecond)), Fix((InWindow.Height - MySize.Height) * Rnd()))
    End Sub
    Public Overridable Sub RandomVelocity(ByVal MinSpeed As Integer, ByVal maxSpeed As Integer)
        Dim vel As Point = New Point(Fix((maxSpeed - MinSpeed) * Rnd()) + MinSpeed, Fix((maxSpeed - MinSpeed) * Rnd()) + MinSpeed)
        If Rnd() > 0.5 Then
            vel.X = vel.X * -1
        End If
        If Rnd() > 0.5 Then
            vel.Y = vel.Y * -1
        End If
        Velocity = vel
        mySpeedLimit = SpeedLimit
    End Sub
    Public Overridable Sub RandomSize(ByVal MinSize As Integer, ByVal MaxSize As Integer)
        Dim newsize As Integer = Fix((MaxSize - MinSize) * Rnd()) + MinSize
        MySize = New Point(newsize, newsize)
        MakePixie()
    End Sub
    Public Overridable Sub RandomColor()
        Color = Color.FromArgb(255, Fix(128 * Rnd()) + 40, Fix(128 * Rnd()) + 40, Fix(128 * Rnd()) + 40)
        MakePixie()
    End Sub

    Protected Overridable Sub MakePixie()
        Dim Grafix As System.Drawing.Graphics
        myImage = New Bitmap(MySize.Width, MySize.Height)
        Grafix = System.Drawing.Graphics.FromImage(myImage)

        Dim MyBrush As New System.Drawing.Drawing2D.LinearGradientBrush(New Rectangle(0, 0, MySize.Width, MySize.Height), Color.White, MyColor, Drawing2D.LinearGradientMode.ForwardDiagonal)
        Grafix.FillEllipse(MyBrush, 0, 0, MySize.Width, MySize.Height)
        Grafix.DrawImage(My.Resources.Chrysanthemum, 0, 0)
        Grafix.Dispose()
    End Sub
    Public Overridable Sub reDraw()
        MakePixie()
    End Sub
    Public Overridable Property Type As String
        Get
            Return myType
        End Get
        Set(ByVal value As String)
            myType = value
        End Set
    End Property

    Public Overridable Property Size() As Size
        Get
            Return MySize
        End Get
        Set(ByVal value As Size)
            MySize = value
            MakePixie()
        End Set
    End Property
    Public Overridable Property Image() As Image
        Get
            Return myImage
        End Get
        Set(ByVal value As Image)
            myImage = value
            Size = myImage.Size
        End Set
    End Property
    Public Overridable Property Color() As Color
        Get
            Color = MyColor
        End Get
        Set(ByVal value As Color)
            MyColor = value
            MakePixie()
        End Set
    End Property
    Public Overridable Property Position() As Point
        Get
            Position = MyPosition
        End Get
        Set(ByVal value As Point)
            MyPosition = value
        End Set
    End Property
    Public Overridable Property Velocity() As Point
        Get
            Velocity = Myvelocity
        End Get
        Set(ByVal value As Point)
            Myvelocity = value
        End Set
    End Property
    Public Overridable Property XPosition As Integer
        Get
            Return MyPosition.X

        End Get
        Set(value As Integer)
            MyPosition = New Point(value, MyPosition.Y)
        End Set
    End Property
    Public Overridable Property YPosition As Integer
        Get
            Return MyPosition.Y

        End Get
        Set(value As Integer)
            MyPosition = New Point(MyPosition.X, value)
        End Set
    End Property
    Public Overridable Property SpeedLimit As Integer
        Get
            Return mySpeedLimit

        End Get
        Set(ByVal value As Integer)
            mySpeedLimit = value
        End Set
    End Property
    Public Overridable Property DamagePower As Integer
        Get
            Return myPower

        End Get
        Set(ByVal value As Integer)
            myPower = value
        End Set
    End Property
    Public Overridable Property LifeForce As Integer
        Get
            Return myLifeForce
        End Get
        Set(ByVal value As Integer)

            myLifeForce = value
            If myLifeForce > 100 Then
                myLifeForce = 100
            End If
        End Set
    End Property
    Public Overridable Property MaximumLifeForce As Integer
        Get
            Return myMaxLives
        End Get
        Set(ByVal value As Integer)
            myMaxLives = value
            myLifeForce = myMaxLives
        End Set
    End Property

    Public Overridable Property Center() As Point
        Get
            Center = New Point(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
        End Get
        Set(ByVal value As Point)
            MyPosition = New Point(value.X - Fix(Size.Width / 2), value.Y - Fix(Size.Height / 2))

        End Set
    End Property
    Public Property Radius() As Integer
        Get
            Radius = Fix(Size.Width / 2)
        End Get
        Set(ByVal value As Integer)
            Size = New Size(value * 2, value * 2)
            MakePixie()
        End Set
    End Property
    
    Public Overridable Property Right As Integer
        Get
            Return Position.X + Size.Width
        End Get
        Set(ByVal value As Integer)
            Position = New Point(value - MySize.Width, Position.Y)
        End Set
    End Property
    Public Overridable Property Left As Integer
        Get
            Return Position.X
        End Get
        Set(ByVal value As Integer)
            Position = New Point(value, Position.Y)
        End Set
    End Property
    Public Overridable Property Top As Integer
        Get
            Return Position.Y
        End Get
        Set(ByVal value As Integer)
            Position = New Point(Position.X, value)
        End Set
    End Property
    Public Overridable Property Bottom As Integer
        Get
            Return Position.Y + Size.Height
        End Get
        Set(ByVal value As Integer)
            Position = New Point(Position.X, value - Size.Height)
        End Set
    End Property
    Public Overridable Property Width As Integer
        Get
            Return MySize.Width
        End Get
        Set(value As Integer)
            MsgBox("width of pixie cannot be set directly")
        End Set
    End Property
    Public Overridable Property Height As Integer
        Get
            Return MySize.Height
        End Get
        Set(value As Integer)
            MsgBox("Height cannot be set directly")
        End Set
    End Property
    Public Overridable Property Rectangle As Rectangle 'returns the collision rectangle
        Get
            Return New Rectangle(Left, Top, Right - Left, Bottom - Top)
        End Get
        Set(value As Rectangle)
            Position = value.Location
            Size = value.Size
        End Set
    End Property
    Public Overridable Sub DrawPixie(ByRef Grafix As System.Drawing.Graphics)
        If Enabled Then
            Grafix.DrawImage(Image, Position)
        End If

    End Sub
    Public Overridable Sub move()
        Position = Position + Velocity
    End Sub
    Public Overridable Sub move(ByVal SpeedMultiplier As Integer)

        Position = Position + New Point(Myvelocity.X * SpeedMultiplier, Myvelocity.Y * SpeedMultiplier)
    End Sub
    
   
    Public Overridable Function WallBounce(ByRef InWall As Rectangle) As WallBounce

        Dim myWallBounce = New WallBounce()
        If Right() > InWall.Right Then
            Velocity = New Point(Velocity.X * -1, Velocity.Y)
            Right = InWall.Right
            myWallBounce.Score("Right")
        End If
        If Left() < InWall.Left Then
            Velocity = New Point(Velocity.X * -1, Velocity.Y)
            Left = InWall.Left
            myWallBounce.Score("Left")
        End If
        If Bottom() > InWall.Bottom Then
            Velocity = New Point(Velocity.X, Velocity.Y * -1)
            Bottom = InWall.Bottom
            myWallBounce.Score("Bottom")
        End If
        If Top() < InWall.Top Then
            Velocity = New Point(Velocity.X, Velocity.Y * -1)
            Top = InWall.Top
            myWallBounce.Score("Top")
        End If
        Return myWallBounce
    End Function

    Protected Overridable Sub StopY()
        Myvelocity.Y = 0
    End Sub
    Protected Overridable Sub stopX()
        Myvelocity.X = 0
    End Sub
    Public Overridable Function RoundCollision(ByVal InObject As pixie) As Boolean

        If Math.Sqrt((Center.X - InObject.Center.X) ^ 2 + (Center.Y - InObject.Center.Y) ^ 2) <= Radius + InObject.Radius Then
            RoundCollision = True

        End If

    End Function

    Public Overridable Function Collision(ByRef InObject As pixie) As Boolean
        If Enabled Then
            If InObject.Right >= Left() Then
                If InObject.Left <= Right() Then
                    If InObject.Bottom >= Top() Then
                        If InObject.Top <= Bottom() Then

                            Return True
                        End If
                    End If
                End If
            End If
        End If

        Return False
    End Function
    Public Overridable Function Collision(ByRef InObject As Rectangle) As Boolean
        If Enabled Then
            If InObject.Right >= Left() Then
                If InObject.Left <= Right() Then
                    If InObject.Bottom >= Top() Then
                        If InObject.Top <= Bottom() Then

                            Return True
                        End If
                    End If
                End If
            End If
        End If

        Return False
    End Function

    Public Overridable Function BounceBoth(ByRef InObject As pixie) As Boolean
        If Collision(InObject) Then

            Dim temp As New Point(Velocity)
            Velocity = InObject.Velocity
            InObject.Velocity = temp

            Do Until Collision(InObject) = False
                move()
                If Collision(InObject) Then
                    move()
                End If
                If Velocity.X = InObject.Velocity.X And Velocity.Y = InObject.Velocity.Y Then
                    If Velocity.X <> -1 Then
                        Velocity = New Point(Velocity.X + 1, Velocity.Y)
                    Else
                        Velocity = New Point(-2, Velocity.Y)
                    End If
                End If
            Loop
            Return True
        Else
            Return False
        End If

    End Function
    Public Overridable Function BounceMe(ByRef InObject As pixie) As Boolean
        If Enabled Then
            If InObject.Enabled Then

                Select Case InObject.Type
                    Case "Pixie"

                        If Collision(InObject) Then
                            IveBeenHit(InObject.DamagePower)
                            InObject.IveBeenHit(DamagePower)
                            Return True
                        End If
                    Case "Weapon"
                        If Collision(InObject) Then
                            IveBeenHit(InObject.DamagePower)
                            InObject.IveBeenHit(DamagePower)
                            Return True
                        End If
                    Case "Paddle"
                        Dim cb As String = CalcBounce(InObject)
                        If cb = "" Then
                            Return False
                        ElseIf cb = "X" Then
                            Velocity = New Point(Velocity.X * -1, Velocity.Y)

                        Else
                            Velocity = New Point(Velocity.X, Velocity.Y * -1)
                        End If
                        AddVelocity(InObject.Velocity)
                        Return True

                    Case "Food"
                        If Collision(InObject) Then
                            InObject.IveBeenHit(DamagePower)
                            Return True
                        End If
                    Case "Wall"
                        Dim cb As String = CalcBounce(InObject)
                        If cb = "" Then
                            Return False
                        ElseIf cb = "X" Then
                            Velocity = New Point(Velocity.X * -1, Velocity.Y)

                        Else
                            Velocity = New Point(Velocity.X, Velocity.Y * -1)
                        End If
                    Case "Brick"
                        Dim cb As String = CalcBounce(InObject)
                        If cb = "" Then
                            Return False
                        ElseIf cb = "X" Then
                            Velocity = New Point(Velocity.X * -1, Velocity.Y)

                        Else
                            Velocity = New Point(Velocity.X, Velocity.Y * -1)
                        End If
                        InObject.Enabled = False
                    Case Else
                        Return False
                End Select

            End If
        End If

        Return False
    End Function
   

    Protected Overridable Function CalcBounce(ByRef InObject As pixie) As String

        If Collision(InObject) Then
            'find where the object came from
            Dim OldPosition As New Point(Position.X - Velocity.X, Position.Y - Velocity.Y)
            Dim OldCenter As New Point(OldPosition.X + CInt(Size.Width / 2), OldPosition.Y + CInt(Size.Height / 2))

            If OldCenter.Y < InObject.Top Then
                'above
                If OldCenter.X < InObject.Left Then
                    'upper left corner
                    If Bottom > InObject.Top Then

                        Right = InObject.Left
                        Return "X"
                    ElseIf Right > InObject.Left Then

                        Bottom = InObject.Top
                        Return "Y"
                    Else
                        If InObject.Left - OldCenter.X > InObject.Top - OldCenter.Y Then

                            Right = InObject.Left
                            Return "X"
                        Else


                            Bottom = InObject.Top
                            Return "Y"
                        End If
                    End If
                ElseIf OldCenter.X > InObject.Right Then
                    'upper right hand corner
                    If Bottom > InObject.Top Then

                        Left = InObject.Right
                        Return "X"
                    ElseIf Left < InObject.Right Then

                        Bottom = InObject.Bottom
                        Return "Y"
                    Else
                        If OldCenter.X - InObject.Right > InObject.Top - OldCenter.Y Then

                            Left = InObject.Right
                            Return "X"
                        Else


                            Bottom = InObject.Top
                            Return "Y"
                        End If
                    End If


                Else
                    'top of pixie


                    Bottom = InObject.Top
                    Return "Y"
                End If


            ElseIf OldCenter.Y > InObject.Bottom Then
                'below paddle
                If OldCenter.X < InObject.Left Then
                    'lower left hand corner
                    If Top < InObject.Bottom Then

                        Right = InObject.Left
                        Return "X"
                    ElseIf Right > InObject.Left Then

                        Top = InObject.Bottom
                        Return "Y"

                    Else
                        If InObject.Left - OldCenter.X > OldCenter.Y - InObject.Bottom Then

                            Right = InObject.Left
                            Return "X"
                        Else


                            Top = InObject.Bottom
                            Return "Y"
                        End If
                    End If

                ElseIf OldCenter.X > InObject.Right Then
                    'lower right hand corner
                    If Left > InObject.Right Then

                        Top = InObject.Bottom
                        Return "Y"
                    ElseIf Top < InObject.Bottom Then

                        Left = InObject.Right
                        Return "X"
                    ElseIf OldCenter.X - InObject.Right > OldCenter.Y - InObject.Bottom Then


                        Left = InObject.Right
                        Return "X"
                    Else

                        Top = InObject.Bottom
                        Return "Y"
                    End If
                Else
                    'center bottom

                    Top = InObject.Bottom
                    Return "Y"
                End If


            Else
                'if we get here then we came from the right or left of the inobject
                If OldCenter.X <= InObject.Left Then
                    'left side


                    Right = InObject.Left
                    Return "X"
                Else
                    'right side


                    Left = InObject.Right
                    Return "X"
                End If
            End If


        End If
        Return ""
    End Function

    Public Overridable Function BounceMe(ByRef InLayer As PixieBucket) As Integer

        Dim count As Integer
        Dim i As Integer
        For i = 0 To InLayer.Length - 1
            If Enabled Then
                If InLayer.GetPixie(i).Enabled Then
                    If BounceMe(InLayer.GetPixie(i)) Then
                        count = count + 1
                    End If
                End If
            End If
        Next
        Return count
    End Function

    Private Sub AddVelocity(ByVal inVel As Point)

        Velocity = Velocity + inVel
        If Math.Abs(Velocity.X) > SpeedLimit Then

            Velocity = New Point(Math.Sign(Velocity.X) * SpeedLimit, Velocity.Y)
        End If
        If Math.Abs(Velocity.Y) > SpeedLimit Then
            Velocity = New Point(Velocity.X, Math.Sign(Myvelocity.Y) * SpeedLimit)
        End If

    End Sub
    Public Overridable Sub KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub
    Public Overridable Sub KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub
    Public Overridable Sub IveBeenHit(ByVal damage As Integer)

    End Sub
End Class
