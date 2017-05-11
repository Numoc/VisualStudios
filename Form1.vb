
Public Class Form1
    Public myGame As JungGame

    Private Sub AddPixie_Click(sender As Object, e As EventArgs) Handles AddPixie.Click
        myGame.addsprite(New Pixie(DrawFrame.DisplayRectangle))
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        myGame = New JungGame(New Size(DrawFrame.Width, DrawFrame.Height))
        AddPixie.Text = "Add Pixie"
        AddPixie.Location = New Point(10, DrawFrame.Height - AddPixie.Height - 10)
        keybox.Select()
        keybox.SendToBack()
        FrameTimer.Start()
    End Sub
    Private Sub FrameTimer_Tick(sender As Object, e As EventArgs) Handles FrameTimer.Tick
        DrawFrame.Image = myGame.getImage()
    End Sub

    Private Sub keybox_KeyDown(sender As Object, e As KeyEventArgs) Handles keybox.KeyDown
        myGame.keydown(e)
    End Sub
    Private Sub keybox_KeyUp(sender As Object, e As KeyEventArgs) Handles keybox.KeyUp
        myGame.keyup(e)
    End Sub
End Class


