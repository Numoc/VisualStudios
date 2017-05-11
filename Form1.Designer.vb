<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DrawFrame = New System.Windows.Forms.PictureBox()
        Me.FrameTimer = New System.Windows.Forms.Timer(Me.components)
        Me.AddPixie = New System.Windows.Forms.Button()
        Me.keybox = New System.Windows.Forms.TextBox()
        CType(Me.DrawFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DrawFrame
        '
        Me.DrawFrame.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DrawFrame.BackColor = System.Drawing.SystemColors.Control
        Me.DrawFrame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DrawFrame.Cursor = System.Windows.Forms.Cursors.Default
        Me.DrawFrame.Location = New System.Drawing.Point(-1, -1)
        Me.DrawFrame.Name = "DrawFrame"
        Me.DrawFrame.Size = New System.Drawing.Size(900, 900)
        Me.DrawFrame.TabIndex = 0
        Me.DrawFrame.TabStop = False
        '
        'FrameTimer
        '
        Me.FrameTimer.Enabled = True
        Me.FrameTimer.Interval = 10
        '
        'AddPixie
        '
        Me.AddPixie.Location = New System.Drawing.Point(430, 428)
        Me.AddPixie.Name = "AddPixie"
        Me.AddPixie.Size = New System.Drawing.Size(75, 30)
        Me.AddPixie.TabIndex = 1
        Me.AddPixie.Text = "Button1"
        Me.AddPixie.UseVisualStyleBackColor = True
        '
        'keybox
        '
        Me.keybox.Location = New System.Drawing.Point(31, 24)
        Me.keybox.Name = "keybox"
        Me.keybox.Size = New System.Drawing.Size(100, 20)
        Me.keybox.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(897, 896)
        Me.Controls.Add(Me.AddPixie)
        Me.Controls.Add(Me.keybox)
        Me.Controls.Add(Me.DrawFrame)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DrawFrame, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DrawFrame As PictureBox
    Friend WithEvents FrameTimer As Timer
    Friend WithEvents AddPixie As Button
    Friend WithEvents keybox As TextBox
End Class
