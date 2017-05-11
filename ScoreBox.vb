Public Class ScoreBox
    Protected backImage As Image
    Protected myImage As Image
    Protected myPosition As Point = New Point(0, 0)
    Protected grafix As System.Drawing.Graphics
    Protected mysize As Size = New Size(100, 50)
    Protected myColor As Color = Color.GreenYellow
    Protected myBackColor As Color = Color.Black
    Protected myvalue As String = ""
    Protected myFont As Font
    Protected myBorderColor As Color = Color.White
    Protected myBorderWidth As Integer = 2
    Protected MyBrush As SolidBrush
    Protected Const Fontscaler As Single = 0.6
    Protected myNumDigits = 6
    Protected myLabel As String = ""

    Public Sub New(ByVal BoxHeight As Integer, ByVal MaxNumberOfDisplayedDigits As Integer, ByVal Location As Point, ByVal Label As String)
        mysize = New Size((MaxNumberOfDisplayedDigits + Label.Length) / 2 * BoxHeight + 6 * myBorderWidth, BoxHeight)
        myNumDigits = MaxNumberOfDisplayedDigits
        myLabel = Label
        myvalue = 0

        myPosition = Location
        myLabel = Label
        setUpBox()



    End Sub
    Protected Overridable Sub setUpBox()
        MyBrush = New SolidBrush(myColor)
        backImage = New Bitmap(mysize.Width, mysize.Height)
        myImage = New Bitmap(mysize.Width, mysize.Height)
        myFont = New Font("Ariel", mysize.Height * Fontscaler, FontStyle.Bold)
        grafix = System.Drawing.Graphics.FromImage(backImage)
        grafix.FillRectangle(New SolidBrush(myBorderColor), 0, 0, mysize.Width, mysize.Height)
        grafix.FillRectangle(New SolidBrush(myBackColor), 0 + myBorderWidth, 0 + myBorderWidth, mysize.Width - 2 * myBorderWidth, mysize.Height - 2 * myBorderWidth)
        grafix = System.Drawing.Graphics.FromImage(myImage)
        DrawScore()

    End Sub
    Protected Overridable Sub DrawScore()
        grafix.Clear(Color.Transparent)
        grafix.DrawImage(backImage, 0, 0)
        grafix.DrawString(myLabel + myvalue, myFont, MyBrush, 2 * myBorderWidth * Fontscaler, 2 * myBorderWidth * Fontscaler)
    End Sub
   
    Public Property Image As Image
        Get
            Return myImage
        End Get
        Set(ByVal value As Image)
            backImage = value
            mysize = backImage.Size
            setUpBox()

        End Set
    End Property
    Public Property Height As Integer
        Get
            Return mysize.Height
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set for scorebox height")
        End Set
    End Property

    Public Property Width As Integer
        Get
            Return mysize.Width
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set for scorebox width")
        End Set
    End Property
    Public Property Center As Point
        Get
            Return New Point(Width / 2, Height / 2)
        End Get
        Set(ByVal value As Point)
            MsgBox("no set for center on scorebox")
        End Set
    End Property
    Public Property Position As Point
        Get
            Return myPosition

        End Get
        Set(ByVal value As Point)
            myPosition = value
        End Set
    End Property
    Public Sub DrawBox(ByVal inGrafix As System.Drawing.Graphics)
        inGrafix.DrawImage(Image, myPosition)
    End Sub
    Public Property Value As String
        Get
            Return myvalue
        End Get
        Set(ByVal value As String)

        End Set
    End Property
End Class
