Public Class PixieBucket
    Protected MyPixies(-1) As pixie
    Protected myVelocity As Point
    Protected mySize As Size
    Protected myPosition As Point
    Protected FileName As String = "Pixies.ini"
    Protected CurDir As String
   

    Public Sub New()

    End Sub
    Public Overridable Sub ReadPixies()
        Dim Filr As System.IO.StreamReader
        Try
            Filr = New System.IO.StreamReader(FileName)

            Try
                Do Until Filr.Peek = -1
                    Dim pix As pixie = New pixie
                    pix.Position = ParsePoint(Filr.ReadLine)
                    Dim Sizr As Point = ParsePoint(Filr.ReadLine)
                    pix.Size = New Size(Sizr.X, Sizr.Y)
                    pix.Velocity = ParsePoint(Filr.ReadLine)
                    AddPixie(pix)
                Loop
                Filr.Close()
            Catch
                Filr.Close()
            End Try
        Catch
        End Try

    End Sub
    Protected Function ParsePoint(ByVal InStr As String) As Point
        Dim i, k As Integer
        Dim x, y As Integer
        i = InStr.IndexOf("=")
        k = InStr.IndexOf(",")

        x = CInt(InStr.Substring(i + 1, k - i - 1))

        i = InStr.IndexOf("=", i + 1)
        k = InStr.IndexOf("}", k + 1)

        y = CInt(InStr.Substring(i + 1, k - i - 1))
        Return New Point(x, y)
    End Function
    Public Overridable Sub writePixieLocations()
        Dim Filr As System.IO.StreamWriter
        Dim i As Integer
        Try
            Filr = New System.IO.StreamWriter(FileName)
            For i = 0 To MyPixies.Length - 1

                Dim temp As pixie
                temp = MyPixies(i)

                Filr.WriteLine(temp.Position)
                Filr.WriteLine(temp.Size)
                Filr.WriteLine(temp.Velocity)

            Next
            Filr.Close()
        Catch
            Dim j As String = Err.ToString
        End Try
    End Sub
    Public Overridable Property Velocity As Point
        Get
            Return myVelocity
        End Get
        Set(ByVal value As Point)
            myVelocity = value
        End Set
    End Property
    Public Overridable Property Rectangle As Rectangle
        Get

            Return New Rectangle(myPosition, mySize)

        End Get
        Set(ByVal value As Rectangle)
            MsgBox("no set on pixiebucket size")
        End Set
    End Property
   
    Public Overridable Property Right As Integer
        Get
            Return myPosition.X + mySize.Width
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set on pixiebucket right")
        End Set
    End Property
    Public Overridable Property Left As Integer
        Get
            Return myPosition.X
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set on pixiebucket Left")
        End Set
    End Property
    Public Overridable Property Top As Integer
        Get
            Return myPosition.Y
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set on pixiebucket top")
        End Set
    End Property
    Public Overridable Property Bottom As Integer
        Get
            Return myPosition.Y + mySize.Height
        End Get
        Set(ByVal value As Integer)
            MsgBox("no set on pixiebucket bottom")
        End Set
    End Property
    Public Overridable Property Length As Integer
        Get
            Return MyPixies.Length
        End Get
        Set(value As Integer)

        End Set
    End Property
    Public Overridable Sub MoveAsABlock()
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            MyPixies(i).Position = MyPixies(i).Position + myVelocity

        Next
        myPosition = myPosition + myVelocity
    End Sub
    Public Overridable Sub MoveAsABlock(ByVal inVel As Point)
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            MyPixies(i).Position = MyPixies(i).Position + inVel
        Next
        myPosition = myPosition + inVel
    End Sub

    Public Overridable Function WallBounceAllAsBlock(ByVal inWall As Rectangle) As WallBounce
        Dim mywb As WallBounce
        mywb = WallBounceAllAction(inWall)
        If mywb.Left > 0 Or mywb.Right > 0 Then 'bounced in x
            myVelocity.X = myVelocity.X * -1
        End If
        If mywb.Top > 0 Or mywb.Bottom > 0 Then 'bounced in Y
            myVelocity.Y = myVelocity.Y * -1
        End If
        Return mywb
    End Function
    Protected Sub CalcSizeAndPosition()
        If MyPixies.Length > 0 Then
            Dim minX As Integer = MyPixies(0).Left
            Dim maxX As Integer = MyPixies(0).Right
            Dim minY As Integer = MyPixies(0).Top
            Dim MaxY As Integer = MyPixies(0).Bottom
            Dim i As Integer
            For i = 1 To MyPixies.Length - 1
                If MyPixies(i).Left < minX Then
                    minX = MyPixies(i).Left
                End If
                If MyPixies(i).Right > maxX Then
                    maxX = MyPixies(i).Right
                End If
                If MyPixies(i).Top < minY Then
                    minY = MyPixies(i).Top
                End If
                If MyPixies(i).Bottom > MaxY Then
                    MaxY = MyPixies(i).Bottom
                End If
            Next
            myPosition = New Point(minX, minY)
            mySize = New Size(maxX - minX, MaxY - minY)
        End If

    End Sub

    Protected Overridable Function WallBounceAllAction(ByVal InWall As Rectangle) As WallBounce

        Dim myWallBounce = New WallBounce()
        If Right() > InWall.Right Then


            myWallBounce.Score("Right")
        End If
        If Left() < InWall.Left Then


            myWallBounce.Score("Left")
        End If
        If Bottom() > InWall.Bottom Then


            myWallBounce.Score("Bottom")
        End If
        If Top() < InWall.Top Then


            myWallBounce.Score("Top")
        End If

        Return myWallBounce
    End Function

    Public Overridable Sub AddPixie(ByVal inObject As pixie)
        ReDim Preserve MyPixies(MyPixies.Length)

        MyPixies(MyPixies.Length - 1) = inObject
        CalcSizeAndPosition()
    End Sub
    Public Overridable Sub AddPixie(ByVal inObject As pixie, ByVal limits As Rectangle)
        inObject.RandomPosition(limits)
        Dim vel As Point = inObject.Velocity
        inObject.RandomVelocity(1, 1)
        Do Until collision(inObject) = False
            inObject.move()
            inObject.WallBounce(limits)
        Loop
        inObject.Velocity = vel
        ReDim Preserve MyPixies(MyPixies.Length)

        MyPixies(MyPixies.Length - 1) = inObject
        CalcSizeAndPosition()
    End Sub
    Public Overridable Sub AddPixie(ByVal inObject As pixie, ByVal limits As Rectangle, ByVal others As PixieBucket)
        inObject.RandomPosition(limits)
        Dim vel As Point = inObject.Velocity
        inObject.RandomVelocity(1, 1)

        Do Until Collision(inObject) = False And Collision(inObject, others) = False
            inObject.move()
            inObject.WallBounce(limits)
        Loop

        inObject.Velocity = vel
        ReDim Preserve MyPixies(MyPixies.Length)

        MyPixies(MyPixies.Length - 1) = inObject
        CalcSizeAndPosition()
    End Sub
    Public Overridable Sub AddPixie(ByVal inObject As pixie, ByVal limits As Rectangle, ByVal others() As PixieBucket)
        inObject.RandomPosition(limits)
        Dim vel As Point = inObject.Velocity
        inObject.RandomVelocity(1, 1)

        Do Until Collision(inObject) = False And Collision(inObject, others) = False
            inObject.move()
            inObject.WallBounce(limits)
        Loop

        inObject.Velocity = vel
        ReDim Preserve MyPixies(MyPixies.Length)

        MyPixies(MyPixies.Length - 1) = inObject
        CalcSizeAndPosition()
    End Sub


    Public Overridable Function Collision(ByVal inPix As pixie, ByVal others() As PixieBucket) As Boolean
        Dim i As Integer
        For i = 0 To others.Length - 1
            If Collision(inPix, others(i)) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Overridable Function Collision(ByVal inPix As pixie, ByRef inbucket As PixieBucket) As Boolean
        Dim i As Integer
        For i = 0 To inbucket.MyPixies.Length - 1
            If inPix.Collision(inbucket.MyPixies(i)) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Overridable Function Collision(ByVal inpix As pixie) As Boolean
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            If MyPixies(i).Collision(inpix) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overridable Function GetPixie(ByVal InIndex As Integer) As pixie
        If InIndex >= 0 And InIndex < MyPixies.Length Then
            Return MyPixies(InIndex)
        Else
            Return New pixie()
        End If
    End Function
    Public Function indexOfPixieAtLocation(ByVal inPosition As Point) As Integer
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            If MyPixies(i).Rectangle.Contains(inPosition) Then
                Return i
            End If
        Next
        Return -1
    End Function
    Public Function GetPixieFromIndex(ByVal inPosition As Integer) As Object
        If inPosition >= 0 And inPosition <= MyPixies.Length Then
            Return MyPixies(inPosition)
        End If
        Return vbNull
    End Function
    Public Function GetPixieFromLocation(ByVal inpt As Point) As Object
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            If MyPixies(i).Rectangle.Contains(inpt) Then
                Return MyPixies(i)
            End If
        Next

        Return vbNull
    End Function

    Public Overridable Sub DrawPixies(ByRef Grafix As System.Drawing.Graphics)
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            MyPixies(i).DrawPixie(Grafix)
        Next
    End Sub
    Public Overridable Function WallBounce(ByVal walls As Rectangle) As WallBounce
        WallBounce = New WallBounce
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            WallBounce.Add(MyPixies(i).WallBounce(walls))
        Next
        Return WallBounce
    End Function
    Public Overridable Function BounceMe(ByRef InLayer As PixieBucket) As Integer
        Dim i, count As Integer

        For i = 0 To MyPixies.Length - 1

            count = count + MyPixies(i).BounceMe(InLayer)
        Next

        Return count
    End Function
    Public Overridable Function BounceMe(ByRef Inobject As pixie) As Integer
        Dim i, count As Integer
        For i = 0 To MyPixies.Length - 1
            If MyPixies(i).BounceMe(Inobject) = True Then
                count = count + 1
            End If
        Next
        Return count
    End Function
    'Public Overridable Sub BounceMeWithVelocity(ByRef Inobject As pixie)
    '    Dim i As Integer
    '    For i = 0 To MyPixies.Length - 1
    '        MyPixies(i).BounceMeWithVelocity(Inobject)
    '    Next
    'End Sub
    'Public Overridable Sub BounceBoth()
    '    Dim k, j As Integer
    '    For k = 0 To MyPixies.Length - 2
    '        For j = k + 1 To MyPixies.Length - 1
    '            MyPixies(k).BounceBoth(MyPixies(j))
    '        Next
    '    Next
    'End Sub
    'Public Overridable Sub BounceBoth(ByRef InLayer As PixieBucket)
    '    Dim k, j As Integer
    '    For k = 0 To InLayer.Length - 1
    '        For j = 0 To MyPixies.Length - 1
    '            InLayer.MyPixies(k).BounceBoth(MyPixies(j))
    '        Next
    '    Next
    'End Sub
    Public Overridable Sub MovePixies()
        Dim j As Integer

        For j = 0 To MyPixies.Length - 1
            MyPixies(j).move()
        Next
    End Sub
    Public Overridable Function removePixie(ByVal inHash As Integer) As Boolean
        Dim i As Integer
        For i = 0 To MyPixies.Length - 1
            Dim j As Integer = MyPixies(i).GetHashCode
            If MyPixies(i).GetHashCode = inHash Then
                DeletePixie(i)
                Return True
            End If
        Next
        Return False
    End Function
    Public Overridable Sub DeletePixie(ByVal InIndex As Integer)
        If MyPixies.Length > 1 Then
            If InIndex < MyPixies.Length - 1 Then
                Dim i As Integer
                For i = InIndex + 1 To MyPixies.Length - 1
                    MyPixies(i - 1) = MyPixies(i)
                Next
            End If
            ReDim Preserve MyPixies(MyPixies.Length - 2)
        Else
            ReDim MyPixies(-1)
        End If
    End Sub
    Public Overridable Sub DeleteDisabledPixies()
        Dim i As Integer
        For i = MyPixies.Length - 1 To 0 Step -1
            If MyPixies(i).Enabled Then
            Else
                DeletePixie(i)
            End If
        Next
    End Sub
    Public Overridable Sub DeleteLastPixie()
        If MyPixies.Length > 1 Then
            ReDim Preserve MyPixies(MyPixies.Length - 2)
        End If
    End Sub
   


End Class
