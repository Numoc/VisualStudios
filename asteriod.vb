Public Class Asteriod
    Inherits ExplodingPixie

    Public Sub New()

        SetExplosion()
        MySize = New Size(20 * Rnd() + 20, 20 * Rnd() + 20)
        RandomVelocity(3, 10)

        'ScaleImage(My.Resources.asteroid_1)
        Explosion.StoppedImage = myImage


    End Sub
    Public Sub New(ByVal inwindow As Rectangle, ByVal minimumSpeed As Integer, ByVal MaximumSpeed As Integer) 'makes a pixie with a random speed between min and max and at a random position within the window
        RandomPosition(inwindow)
        RandomVelocity(minimumSpeed, MaximumSpeed)

     
        SetExplosion()
        Dim autoSize As Integer = 80 * Rnd() + 20
        MySize = New Size(autoSize, autoSize)

        'ScaleImage(My.Resources.asteroid_1)
        Explosion.StoppedImage = myImage

    End Sub
End Class
