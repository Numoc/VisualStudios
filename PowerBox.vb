Public Class PowerBox
    Protected backImage As Image
    Protected myImage As Image
    Protected myPosition As Point = New Point(0, 0)
    Protected grafix As System.Drawing.Graphics
    Protected mysize As Size = New Size(100, 50)
    Protected myColor As Color = Color.GreenYellow
    Protected myBackColor As Color = Color.Black
    Protected myvalue As Integer = 100
    Protected myFont As Font
    Protected myBorderColor As Color = Color.Red
    Protected myBorderWidth As Integer = 2
    Protected myBarPosition As Point
    Protected myBarSize As Size
    Protected myBarSeparation As Integer = 2
    Protected MyBrush As SolidBrush
    Protected Const Fontscaler As Single = 0.6
    Protected myNumDigits = 6
    Protected mylabel As String
    Public Sub New(ByVal BoxHeight As Integer, ByVal BoxWidth As Integer, ByVal Location As Point, ByVal Label As String)
        mysize = New Size(BoxWidth, BoxHeight)
        mylabel = Label
        myPosition = Location
        setUpBox()

    End Sub
    Protected Sub setUpBox()
        Dim fp As Integer = CInt(myPosition.X + mylabel.Length) / 2 * mysize.Height + 6 * myBorderWidth

        myBarPosition = New Point(fp, myBorderWidth + myBarSeparation)
        myBarSize = New Size(mysize.Width - 2 * myBarSeparation - fp, mysize.Height - 2 * myBorderWidth - 2 * myBarSeparation)
        MyBrush = New SolidBrush(myColor)
        backImage = New Bitmap(mysize.Width, mysize.Height)
        myImage = New Bitmap(mysize.Width, mysize.Height)
        myFont = New Font("Ariel", mysize.Height * Fontscaler, FontStyle.Bold)
        grafix = System.Drawing.Graphics.FromImage(backImage)
        grafix.FillRectangle(New SolidBrush(myBorderColor), 0, 0, mysize.Width, mysize.Height)
        grafix.FillRectangle(New SolidBrush(myBackColor), myBarPosition.X - 1, myBarPosition.Y - 1, myBarSize.Width + 2, myBarSize.Height + 2)
        grafix = System.Drawing.Graphics.FromImage(myImage)
        DrawScore()

    End Sub
    Protected Sub DrawScore()
        grafix.Clear(Color.Transparent)
        grafix.DrawImage(backImage, 0, 0)
        Dim drawWidth As Integer = (Int(Value * myBarSize.Width / 100))
        grafix.FillRectangle(MyBrush, myBarPosition.X, myBarPosition.Y, drawWidth, myBarSize.Height)
        grafix.DrawRectangle(New Pen(myBorderColor, 2), myBarPosition.X, myBarPosition.Y, myBarSize.Width, myBarSize.Height)
        grafix.DrawString(mylabel, myFont, Brushes.Yellow, myBorderWidth + myBarSeparation, myBarPosition.Y - 2)
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
            MsgBox("no set for PowerBox height")
        End Set
    End Property
    Public Overridable Sub Decrement(ByVal amount As Integer)
        Value = Value - amount
    End Sub
    Public Overridable Sub Increment(ByVal amount As Integer)
        Value = Value + amount
    End Sub
    Public Property Width As Integer
        Get
            Return mysize.Width
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set for PowerBox width")
        End Set
    End Property
    Public Property Center As Point
        Get
            Return New Point(Width / 2, Height / 2)
        End Get
        Set(ByVal value As Point)
            MsgBox("no set for center on PowerBox")
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
    Public Property Value As Integer
        Get
            Return myvalue
        End Get
        Set(ByVal value As Integer)
            myvalue = value
            If myvalue > 100 Then
                myvalue = 100
            End If
            DrawScore()
        End Set
    End Property
End Class
