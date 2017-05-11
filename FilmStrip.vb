Public Class FilmStrip
    Protected MyImages As ArrayList
    Protected ImagePointer As Integer = 0
    Protected FrameCounter As Integer = 0
    Protected FrameLimit As Integer = 3
    Protected myScale As Single
    Protected myPixieSize As Point
    Protected MySize As Size
    Protected MyStoppedImage As Image = New Bitmap(1, 1)
    Public ContinuousLoop As Boolean = False
    Public FilmStripRunning As Boolean = False
    Public FilmStripHasRun As Boolean = False
    Public enabled As Boolean = True
    Public StopAction As Boolean = False

    'Public Sub New()

    'End Sub
    Public Sub New(ByVal DrawSize As Size)

        MyImages = New ArrayList
        MySize = DrawSize


    End Sub
    Public Property FilmSpeed() As Integer
        Get
            FilmSpeed = FrameLimit
        End Get
        Set(ByVal value As Integer)
            FrameLimit = value
        End Set
    End Property
    Public Property FramePointer As Integer
        Get
            Return ImagePointer
        End Get
        Set(ByVal value As Integer)
            If value < MyImages.Count And value >= 0 Then
                ImagePointer = value
            End If
        End Set
    End Property
    Public Overridable Property Image() As Image
        Get
            'we will return one image in the animation.  First we count the number of frames we have received, and when the 
            'count gets over frameLimit we will point to the next stored frame.  when we get to the end of the animation frames stored
            'we will reset to frame0, and if we are not looping the animation we will stop.
            If StopAction Then
                Return MyImages.Item(ImagePointer)
            Else
                If FilmStripRunning Then
                    If MyImages.Count > 0 Then
                        FrameCounter = FrameCounter + 1
                        If FrameCounter >= FrameLimit Then ' framecounter slows down the action by number of frames in framelimit
                            FrameCounter = 0
                            ImagePointer = ImagePointer + 1


                            If ImagePointer < MyImages.Count Then

                                Return MyImages.Item(ImagePointer)
                            Else

                                If ContinuousLoop Then
                                    ImagePointer = 0
                                    FilmStripHasRun = True

                                    Return MyImages.Item(ImagePointer)
                                Else
                                    Halt()
                                    Try
                                        Return MyStoppedImage
                                    Catch ex As Exception
                                        Return MyImages(0)
                                    End Try
                                End If
                            End If
                        Else
                            Return MyImages.Item(ImagePointer)

                        End If


                    End If

                End If
                ImagePointer = 0
                Try
                    Return MyStoppedImage
                Catch ex As Exception
                    Return MyImages(0)
                End Try
            End If


        End Get
        Set(ByVal value As Image)
            If value.Size.Equals(MySize) Then
                MyImages.Add(value)
            Else
                MyImages.Add(ScaleImage(value))
            End If

        End Set
    End Property
    Public Sub Start()
        ImagePointer = 0
        FilmStripRunning = True
    End Sub
    Public Overridable Property StoppedImage() As Image
        Get
            Return MyStoppedImage
        End Get
        Set(value As Image)
            MyStoppedImage = value

        End Set
    End Property
    Public Property FilmStripOffset() As Point
        Get
            If FilmStripRunning Then
                Return New Point((myPixieSize.X - Mysize.Width) / 2, (myPixieSize.Y - Mysize.Height) / 2)
            Else
                Return New Point(0, 0)
            End If

        End Get
        Set(value As Point)
            Mysize = New Size(myPixieSize.X - value.X, myPixieSize.Y - value.Y)
        End Set
    End Property
    Public Sub Halt()
        FilmStripRunning = False
        FilmStripHasRun = True

    End Sub
    Public Overridable Property Done As Boolean
        Get
            If ContinuousLoop Then
                Return False
            ElseIf FilmStripRunning Then
                Return False
            ElseIf ImagePointer >= MyImages.Count Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
    Protected Function ScaleImage(ByVal inImage As Image) As Image

        Dim myScaleX = Int(MySize.Width / inImage.Width)
        Dim myScaleY = Int(MySize.Width / inImage.Width)
        Dim MyFrame As Image
        MyFrame = New Bitmap(MySize.Width, MySize.Height)
        Dim grafix As System.Drawing.Graphics
        grafix = System.Drawing.Graphics.FromImage(MyFrame)
        grafix.ResetTransform()
        grafix.Clear(Drawing.Color.Transparent)
        grafix.ScaleTransform(myScaleX, myScaleY)
        'grafix.TranslateTransform(0, 0)

        grafix.DrawImage(inImage, New Point(0, 0))
        grafix.Dispose()
        Return MyFrame
    End Function
End Class
