Imports System.IO

'Sting Chameleon States
Public Enum StateSplitSC
    Intro
    Stand
    JumpUp
    JumpDown
    Disappear
    Reappear
    Hanging
    ShakeBody
    ShootStings
    LashTongue
    GroundStraightLashToungue
    WallStraightLashTongue
    OnWall
End Enum

'dummy state
Public Enum Dummy
    Run
    Stand
    Shoot
    Death
End Enum

'Spikes from ceiling state
Public Enum SpikesFromCeiling
    Create
    Fall
    Destroy
End Enum

'Spikes Shooted by SC
Public Enum SpikesShooted
    Create
    Shooted
    Destroy
End Enum

Public Enum FaceDir
    Left
    Right
End Enum

Public Class CImage
    Public Width As Integer
    Public Height As Integer
    Public Elmt(,) As System.Drawing.Color
    Public ColorMode As Integer 'not used

    Sub OpenImage(ByVal FName As String)
        Dim s As String
        Dim L As Long
        Dim BR As BinaryReader
        Dim h, w, pos As Integer
        Dim r, g, b As Integer
        Dim pad As Integer

        BR = New BinaryReader(File.Open(FName, FileMode.Open))

        Try
            BlockRead(BR, 2, s)

            If s <> "BM" Then
                MsgBox("Not a BMP file")
            Else 'BMP file
                BlockReadInt(BR, 4, L) 'size
                'MsgBox("Size = " + CStr(L))
                BlankRead(BR, 4) 'reserved
                BlockReadInt(BR, 4, pos) 'start of data
                BlankRead(BR, 4) 'size of header
                BlockReadInt(BR, 4, Width) 'width
                'MsgBox("Width = " + CStr(I.Width))
                BlockReadInt(BR, 4, Height) 'height
                'MsgBox("Height = " + CStr(I.Height))
                BlankRead(BR, 2) 'color panels
                BlockReadInt(BR, 2, ColorMode) 'colormode
                If ColorMode <> 24 Then
                    MsgBox("Not a 24-bit color BMP")
                Else

                    BlankRead(BR, pos - 30)

                    ReDim Elmt(Width - 1, Height - 1)
                    pad = (4 - (Width * 3 Mod 4)) Mod 4

                    For h = Height - 1 To 0 Step -1
                        For w = 0 To Width - 1
                            BlockReadInt(BR, 1, b)
                            BlockReadInt(BR, 1, g)
                            BlockReadInt(BR, 1, r)
                            Elmt(w, h) = Color.FromArgb(r, g, b)

                        Next
                        BlankRead(BR, pad)

                    Next

                End If

            End If

        Catch ex As Exception
            MsgBox("Error")

        End Try

        BR.Close()


    End Sub


    Sub CreateMask(ByRef Mask As CImage)
        Dim i, j As Integer

        Mask = New CImage
        Mask.Width = Width
        Mask.Height = Height

        ReDim Mask.Elmt(Mask.Width - 1, Mask.Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Elmt(i, j).R = 0 And Elmt(i, j).G = 0 And Elmt(i, j).B = 0 Then
                    Mask.Elmt(i, j) = Color.FromArgb(255, 255, 255)
                Else
                    Mask.Elmt(i, j) = Color.FromArgb(0, 0, 0)
                End If
            Next
        Next

    End Sub


    Sub CopyImg(ByRef Img As CImage)
        'copies image to Img
        Img = New CImage
        Img.Width = Width
        Img.Height = Height
        ReDim Img.Elmt(Width - 1, Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Img.Elmt(i, j) = Elmt(i, j)
            Next
        Next

    End Sub

End Class

Public Class Character
    Public PosX, PosY As Double
    Public Vx, Vy As Double
    Public FrameIndex As Integer
    Public CurrFrame As Integer
    Public ArraySprites() As CArrFrame
    Public IndexArrSprites As Integer
    Public FDirection As FaceDir
    Public Destroy As Boolean = False

    Public Sub GetNextFrame()
        CurrFrame = CurrFrame + 1
        If CurrFrame = ArraySprites(IndexArrSprites).Elmt(FrameIndex).MaxFrameTime Then
            FrameIndex = FrameIndex + 1
            If FrameIndex = ArraySprites(IndexArrSprites).N Then
                FrameIndex = 0
            End If
            CurrFrame = 0

        End If

    End Sub

    Public Overridable Sub Update()

    End Sub

End Class

Public Class CharacterSC

    Inherits Character
    Public CurrState As StateSplitSC
    Public Sub State(state As StateSplitSC, idxspr As Integer)
        CurrState = state
        IndexArrSprites = idxspr
        CurrFrame = 0
        FrameIndex = 0

    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case StateSplitSC.Intro
                GetNextFrame()
                If FrameIndex = 0 And CurrFrame = 0 Then
                    State(StateSplitSC.Stand, 1)
                End If

            Case StateSplitSC.Stand

                If PosX <= 120 Then     ' left side of megaman
                    FDirection = FaceDir.Right
                Else                    ' right side of megaman
                    FDirection = FaceDir.Left
                End If


                GetNextFrame()

            Case StateSplitSC.JumpUp 'yes
                Vy = -4

                If FDirection = FaceDir.Left Then ' right side of megaman
                    Vx = -3
                Else                              ' left side of megaman
                    Vx = 3
                End If

                GetNextFrame()

                PosX += Vx
                PosY += Vy

                If PosY <= 50 Then
                    State(StateSplitSC.Hanging, 2)
                End If

            Case StateSplitSC.Hanging 'yes

                If PosX <= 120 Then ' left side of megaman
                    FDirection = FaceDir.Right
                Else                ' right side of megamen
                    FDirection = FaceDir.Left
                End If
                GetNextFrame()

                If FrameIndex = 0 And CurrFrame = 0 Then
                    State(StateSplitSC.ShakeBody, 3)
                End If

            Case StateSplitSC.ShakeBody
                GetNextFrame()

                If FrameIndex = 0 And CurrFrame = 0 Then
                    State(StateSplitSC.Disappear, 11)
                End If


            Case StateSplitSC.JumpDown 'yes
                Vy = 4

                If FDirection = FaceDir.Left Then ' right side of megaman
                    Vx = -3
                Else                ' left side of megaman
                    Vx = 3
                End If

                GetNextFrame()

                PosX += Vx
                PosY += Vy

                If PosY >= 157 Then
                    Vx = 0
                    Vy = 0
                    State(StateSplitSC.Stand, 1)
                End If


            Case StateSplitSC.Disappear
                GetNextFrame()


            Case StateSplitSC.GroundStraightLashToungue
                GetNextFrame()

                If FrameIndex = 0 And CurrFrame = 0 Then
                    State(StateSplitSC.Stand, 1)
                End If

            Case StateSplitSC.OnWall

                If PosX <= 120 Then     ' left side of megaman
                    FDirection = FaceDir.Right
                Else                    ' right side of megaman
                    FDirection = FaceDir.Left
                End If

                GetNextFrame()



            Case StateSplitSC.WallStraightLashTongue

                GetNextFrame()


            Case StateSplitSC.LashTongue

                GetNextFrame()


            Case StateSplitSC.ShootStings

                If PosX <= 120 Then     ' left side of megaman
                    FDirection = FaceDir.Right
                Else                    ' right side of megaman
                    FDirection = FaceDir.Left
                End If

                GetNextFrame()


        End Select
    End Sub

End Class

Public Class CharacterDummy
    Inherits Character

    Public CurrState As Dummy

    Public Sub State(state As Dummy, idxspr As Integer)
        CurrState = state
        IndexArrSprites = idxspr
        CurrFrame = 0
        FrameIndex = 0
    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case Dummy.Stand
                GetNextFrame()
                FDirection = FaceDir.Left

                If FrameIndex = 0 And CurrFrame = 0 Then
                    Vx = 2
                    Vy = 0
                    State(Dummy.Run, 1)
                End If

            Case Dummy.Run
                GetNextFrame()
                PosX += Vx

                If PosX >= 195 Then
                    Vx = 0
                    State(Dummy.Shoot, 2)
                End If

                If FDirection = FaceDir.Right And PosX <= 30 Then
                    State(Dummy.Stand, 0)
                End If

            Case Dummy.Shoot
                GetNextFrame()
                FDirection = FaceDir.Right
                If FrameIndex = 0 And CurrFrame = 0 Then
                    Vx = -2
                    Vy = 0
                    State(Dummy.Run, 1)
                End If

            Case Dummy.Death
                GetNextFrame()

        End Select
    End Sub

End Class

Public Class CharacterCeilingSpikes

    Inherits Character

    Public CurrState As SpikesFromCeiling

    Public Sub State(state As SpikesFromCeiling, idxspr As Integer)
        CurrState = state
        IndexArrSprites = idxspr
        CurrFrame = 0
        FrameIndex = 0
    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case SpikesFromCeiling.Create

                GetNextFrame()

                PosY += Vy

                If PosY >= 180 Then
                    Destroy = True
                End If
        End Select
    End Sub

End Class

Public Class CharacterShootedSpikes

    Inherits Character

    Public CurrState As SpikesShooted

    Public Sub State(state As SpikesShooted, idxspr As Integer)
        CurrState = state
        IndexArrSprites = idxspr
        CurrFrame = 0
        FrameIndex = 0
    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case SpikesShooted.Create

                GetNextFrame()

                PosX += Vx
                PosY += Vy

                If FDirection = FaceDir.Left Then
                    If PosX <= 120 Then
                        Destroy = True
                    End If
                Else
                    If PosX >= 120 Then
                        Destroy = True
                    End If
                End If


        End Select
    End Sub

End Class

Public Class CElmtFrame
    Public CtrPoint As TPoint
    Public Top, Bottom, Left, Right As Integer
    Public Idx As Integer
    Public MaxFrameTime As Integer

    Public Sub New(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        CtrPoint.x = ctrx
        CtrPoint.y = ctry
        Top = t
        Bottom = b
        Left = l
        Right = r
        MaxFrameTime = mft

    End Sub
End Class

Public Class CArrFrame
    Public N As Integer
    Public Elmt As CElmtFrame()

    Public Sub New()
        N = 0
        ReDim Elmt(-1)
    End Sub

    Public Overloads Sub Insert(E As CElmtFrame)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1
    End Sub

    Public Overloads Sub Insert(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        Dim E As CElmtFrame
        E = New CElmtFrame(ctrx, ctry, l, t, r, b, mft)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1

    End Sub

End Class

Public Structure TPoint
    Dim x As Integer
    Dim y As Integer

End Structure


