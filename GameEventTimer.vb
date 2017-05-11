Public Class GameEventTimer
    Protected mainTimer As Integer
    Protected timerCountResetValue As Integer
    Protected timerMode As Integer 'modes are: 0 = stopped, 1 = paused, 2=running, 

    Public Sub New(ByVal timerPeriod As Integer)
        timerCountResetValue = timerPeriod
        ResetTimer()
    End Sub
    Public Overridable Sub ResetTimer()
        mainTimer = timerCountResetValue
        timerMode = 0
    End Sub
    Public Overridable Property Running As Boolean
        Get
            If timerMode = 2 Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
    Public Overridable Sub StartTimer()
        timerMode = 2 'start timer
    End Sub
    Public Overridable Sub StopTimer()
        timerMode = 0
    End Sub
    Public Overridable Sub StopTimerAndReset()
        ResetTimer()
    End Sub

    Public Overridable Function incrementTimer() As Boolean
        If timerMode > 1 Then
            mainTimer = mainTimer - 1
            If mainTimer <= 0 Then
                mainTimer = timerCountResetValue
                Return True
            End If
        End If

        Return False
    End Function
End Class
