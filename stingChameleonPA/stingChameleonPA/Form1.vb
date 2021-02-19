Imports System.IO
Public Class Form1


    Dim bmp As Bitmap
    Dim Bg, Bg1, Img As CImage
    Dim SpriteMap As CImage
    Dim SpriteMask As CImage
    'Sting Chameleon
    Dim SCintro, SCstand, SCJumpUp, SCHanging, SCShakeBody, SCDisappear, SCReappear, SCOnWall, SCLashTongue, SCWallStraightLashTongue, SCGroundStraightLashTongue, SCShootStings, SCJumpDown As CArrFrame
    Dim SC As CharacterSC

    'Ceiling Spikes
    Dim CeilingSpikesCreate1, CeilingSpikesCreate2, CeilingSpikesCreate3 As CArrFrame


    'Dummy
    Dim dummyRun, dummyStand, dummyShoot, dummyDeath As CArrFrame
    Dim DM As CharacterDummy

    'Shooted Spikes
    Dim SpikesCreate As CArrFrame

    Dim ListChar As New List(Of Character)

    Dim listSC As New List(Of CharacterSC)
    Dim listDummy As New List(Of CharacterDummy)
    Dim listCS As New List(Of CharacterCeilingSpikes)
    Dim listSP As New List(Of CharacterShootedSpikes)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Panel1.Visible = True
        Panel2.Visible = False


        'open image for background, assign to bg

        Bg = New CImage
        Bg.OpenImage("C:\Users\puspakirana\Downloads\background.bmp")

        Bg.CopyImg(Img)

        Bg.CopyImg(Bg1)

        SpriteMap = New CImage
        SpriteMap.OpenImage("C:\Users\puspakirana\Downloads\newSC.bmp")

        SpriteMap.CreateMask(SpriteMask)

        'initialize sprites for Sting Chameleon

        SCintro = New CArrFrame
        SCintro.Insert(40, 266, 14, 240, 68, 284, 1)
        SCintro.Insert(103, 267, 76, 240, 128, 284, 1)
        SCintro.Insert(164, 266, 137, 240, 191, 284, 2)
        SCintro.Insert(289, 267, 259, 239, 316, 285, 3)
        SCintro.Insert(466, 267, 439, 241, 494, 285, 1)
        SCintro.Insert(528, 267, 501, 240, 555, 285, 1)
        SCintro.Insert(586, 268, 560, 241, 611, 285, 1)
        SCintro.Insert(641, 267, 617, 242, 668, 285, 1)
        SCintro.Insert(699, 267, 675, 242, 726, 285, 1)
        SCintro.Insert(36, 328, 15, 299, 60, 346, 1)
        SCintro.Insert(88, 328, 68, 298, 113, 346, 1)
        SCintro.Insert(140, 328, 120, 295, 165, 345, 1)
        SCintro.Insert(196, 328, 170, 305, 237, 345, 1)
        SCintro.Insert(246, 329, 242, 301, 288, 346, 1)
        SCintro.Insert(314, 331, 294, 299, 338, 346, 1)
        SCintro.Insert(368, 335, 346, 302, 395, 346, 1)
        SCintro.Insert(432, 330, 401, 302, 457, 347, 1)
        SCintro.Insert(514, 330, 478, 300, 540, 346, 1)
        SCintro.Insert(634, 328, 554, 300, 660, 346, 2)
        SCintro.Insert(432, 330, 401, 302, 457, 347, 1)
        SCintro.Insert(514, 330, 478, 300, 540, 346, 1)
        SCintro.Insert(634, 328, 554, 300, 660, 346, 2)

        SCstand = New CArrFrame
        SCstand.Insert(40, 266, 14, 240, 68, 284, 5)

        SCDisappear = New CArrFrame
        SCDisappear.Insert(664, 714, 642, 684, 684, 741, 1)

        SCReappear = New CArrFrame
        SCReappear.Insert(95, 442, 72, 412, 126, 475, 5)

        SCOnWall = New CArrFrame
        SCOnWall.Insert(95, 442, 72, 412, 126, 475, 5)

        SCJumpUp = New CArrFrame
        SCJumpUp.Insert(368, 332, 346, 302, 395, 346, 5)

        SCJumpDown = New CArrFrame
        SCJumpDown.Insert(368, 325, 346, 302, 395, 346, 5)

        SCHanging = New CArrFrame
        SCHanging.Insert(490, 713, 472, 674, 515, 739, 5)

        SCShakeBody = New CArrFrame
        SCShakeBody.Insert(440, 717, 420, 684, 465, 739, 2)
        SCShakeBody.Insert(385, 718, 634, 680, 411, 738, 2)
        SCShakeBody.Insert(440, 717, 420, 684, 465, 739, 2)
        SCHanging.Insert(490, 713, 472, 674, 515, 739, 2)
        SCShakeBody.Insert(535, 718, 517, 675, 561, 739, 2)
        SCShakeBody.Insert(539, 721, 573, 686, 618, 742, 2)
        SCShakeBody.Insert(535, 718, 517, 675, 561, 739, 2)
        SCHanging.Insert(490, 713, 472, 674, 515, 739, 2)
        SCShakeBody.Insert(440, 717, 420, 684, 465, 739, 2)
        SCShakeBody.Insert(385, 718, 634, 680, 411, 738, 2)
        SCShakeBody.Insert(440, 717, 420, 684, 465, 739, 2)
        SCHanging.Insert(490, 713, 472, 674, 515, 739, 4)

        SCShootStings = New CArrFrame
        SCShootStings.Insert(39, 716, 20, 685, 72, 755, 1)
        SCShootStings.Insert(99, 712, 80, 680, 126, 767, 1)
        SCShootStings.Insert(160, 710, 139, 678, 186, 768, 1)
        SCShootStings.Insert(217, 713, 192, 680, 243, 766, 1)
        SCShootStings.Insert(277, 718, 247, 684, 303, 754, 10)

        SCLashTongue = New CArrFrame
        SCLashTongue.Insert(287, 436, 266, 403, 319, 473, 1)
        SCLashTongue.Insert(341, 437, 323, 407, 376, 467, 1)
        SCLashTongue.Insert(493, 586, 464, 554, 526, 615, 2)
        SCLashTongue.Insert(400, 586, 351, 554, 433, 660, 2)
        SCLashTongue.Insert(295, 587, 260, 555, 329, 628, 3)
        SCLashTongue.Insert(341, 437, 323, 407, 376, 467, 3)
        SCLashTongue.Insert(202, 587, 110, 554, 235, 627, 2)
        SCLashTongue.Insert(65, 587, 8, 553, 98, 616, 3)
        SCLashTongue.Insert(341, 437, 323, 407, 376, 467, 2)

        SCGroundStraightLashTongue = New CArrFrame
        SCGroundStraightLashTongue.Insert(368, 335, 346, 302, 395, 345, 1)
        SCGroundStraightLashTongue.Insert(432, 330, 401, 302, 456, 347, 1)
        SCGroundStraightLashTongue.Insert(514, 330, 478, 300, 540, 346, 1)
        SCGroundStraightLashTongue.Insert(634, 328, 554, 300, 660, 346, 2)

        SCWallStraightLashTongue = New CArrFrame
        SCWallStraightLashTongue.Insert(287, 436, 266, 403, 319, 473, 1)
        SCWallStraightLashTongue.Insert(341, 437, 323, 407, 376, 467, 1)
        SCWallStraightLashTongue.Insert(431, 442, 409, 410, 464, 470, 1)
        SCWallStraightLashTongue.Insert(514, 442, 486, 410, 547, 470, 1)
        SCWallStraightLashTongue.Insert(633, 442, 561, 411, 666, 470, 2)


        SC = New CharacterSC
        ReDim SC.ArraySprites(12)
        SC.ArraySprites(0) = SCintro
        SC.ArraySprites(1) = SCstand
        SC.ArraySprites(2) = SCHanging
        SC.ArraySprites(3) = SCShakeBody
        SC.ArraySprites(4) = SCShootStings
        SC.ArraySprites(5) = SCLashTongue
        SC.ArraySprites(6) = SCGroundStraightLashTongue
        SC.ArraySprites(7) = SCWallStraightLashTongue
        SC.ArraySprites(8) = SCJumpUp
        SC.ArraySprites(9) = SCJumpDown
        SC.ArraySprites(10) = SCReappear
        SC.ArraySprites(11) = SCDisappear
        SC.ArraySprites(12) = SCOnWall

        SC.PosX = 195   '195 ground  |  ceiling 80 |  on wall 130
        SC.PosY = 164   '164 ground  |  ceiling 50  |  on wall 120
        SC.Vx = 0
        SC.Vy = 0
        SC.State(StateSplitSC.Intro, 0)
        SC.FDirection = FaceDir.Left

        ListChar.Add(SC)
        listSC.Add(SC)

        'initialize sprites for Ceiling Spikes

        CeilingSpikesCreate1 = New CArrFrame
        CeilingSpikesCreate1.Insert(329, 703, 325, 698, 333, 708, 1)

        CeilingSpikesCreate2 = New CArrFrame
        CeilingSpikesCreate1.Insert(329, 703, 325, 698, 333, 708, 1)

        CeilingSpikesCreate3 = New CArrFrame
        CeilingSpikesCreate1.Insert(329, 703, 325, 698, 333, 708, 1)



        'initialize sprites for Dummy

        dummyStand = New CArrFrame
        dummyStand.Insert(145, 50, 123, 27, 162, 70, 1)
        dummyStand.Insert(197, 50, 173, 27, 213, 70, 50)

        dummyRun = New CArrFrame
        dummyRun.Insert(34, 124, 20, 103, 51, 142, 1)
        dummyRun.Insert(86, 124, 67, 103, 104, 142, 1)
        dummyRun.Insert(138, 124, 112, 103, 159, 142, 1)
        dummyRun.Insert(186, 124, 166, 103, 201, 142, 1)

        dummyShoot = New CArrFrame
        dummyShoot.Insert(393, 825, 370, 807, 417, 849, 1)
        dummyShoot.Insert(45, 827, 21, 807, 69, 849, 1)

        dummyDeath = New CArrFrame
        dummyDeath.Insert(534, 83, 510, 62, 556, 117, 1)
        dummyDeath.Insert(585, 81, 561, 62, 605, 117, 1)


        DM = New CharacterDummy

        DM.PosX = 30
        DM.PosY = 157
        DM.Vx = 5
        DM.CurrState = Dummy.Stand
        DM.FDirection = FaceDir.Left

        ReDim DM.ArraySprites(3)
        DM.ArraySprites(0) = dummyStand
        DM.ArraySprites(1) = dummyRun
        DM.ArraySprites(2) = dummyShoot
        DM.ArraySprites(3) = dummyShoot

        ListChar.Add(DM)
        listDummy.Add(DM)

        'initialize sprites for Shooted Spikes

        SpikesCreate = New CArrFrame
        SpikesCreate.Insert(409, 183, 396, 167, 432, 199, 10)

        bmp = New Bitmap(Img.Width, Img.Height)

        DisplayImg()
        ResizeImg()

    End Sub

    Public Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

        If Asc(e.KeyChar) = 32 Then ' space
            Timer1.Enabled = True
            Panel1.Visible = False
            Panel2.Visible = False
        End If

        If SC.CurrState = StateSplitSC.Stand Then
            If Asc(e.KeyChar) = 97 Or Asc(e.KeyChar) = 65 Then ' a or A
                SC.State(StateSplitSC.GroundStraightLashToungue, 6)
            ElseIf SC.CurrState = StateSplitSC.Stand And Asc(e.KeyChar) = 74 Or Asc(e.KeyChar) = 106 Then ' j or J
                SC.State(StateSplitSC.JumpUp, 8)
            End If

        End If

        If Not SC.CurrState = StateSplitSC.ShakeBody Then
            If SC.PosY < 135 Then
                If Asc(e.KeyChar) = 68 Or Asc(e.KeyChar) = 100 Then ' d or D
                    SC.State(StateSplitSC.Disappear, 11)
                ElseIf Not SC.CurrState = StateSplitSC.Disappear Then
                    If Asc(e.KeyChar) = 70 Or Asc(e.KeyChar) = 102 Then ' f or F
                        SC.State(StateSplitSC.JumpDown, 9)
                    End If
                End If
            End If
        End If


        If SC.CurrState = StateSplitSC.OnWall Or SC.CurrState = StateSplitSC.LashTongue Or SC.CurrState = StateSplitSC.WallStraightLashTongue Then
            If Asc(e.KeyChar) = 76 Or Asc(e.KeyChar) = 108 Then ' l or L
                SC.State(StateSplitSC.LashTongue, 5)
            ElseIf Asc(e.KeyChar) = 87 Or Asc(e.KeyChar) = 119 Then ' w or W
                SC.State(StateSplitSC.WallStraightLashTongue, 7)
            End If
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As MouseEventArgs) Handles PictureBox1.Click
        If SC.CurrState = StateSplitSC.Disappear And e.Y <= 120 Then
            If e.Y <= 50 And e.X <= 50 Then
                SC.PosY = 50
                SC.PosX = 50
                SC.State(StateSplitSC.ShootStings, 4)
            ElseIf e.Y <= 50 And e.x >= 195 Then
                SC.PosY = 50
                SC.PosX = 195
                SC.State(StateSplitSC.ShootStings, 4)
            Else
                SC.PosX = e.X
                SC.PosY = e.Y
                SC.State(StateSplitSC.OnWall, 12)
            End If

        End If
    End Sub

    Sub PutSprites()

        Dim cc As Character
        Dim i, j As Integer

        'set background
        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                Img.Elmt(i, j) = Bg1.Elmt(i, j)
            Next
        Next

        For Each cc In ListChar
            Dim EF As CElmtFrame = cc.ArraySprites(cc.IndexArrSprites).Elmt(cc.FrameIndex)
            Dim spritewidth = EF.Right - EF.Left
            Dim spriteheight = EF.Bottom - EF.Top

            If cc.FDirection = FaceDir.Left Then
                Dim spriteleft As Integer = cc.PosX - EF.CtrPoint.x + EF.Left
                Dim spritetop As Integer = cc.PosY - EF.CtrPoint.y + EF.Top

                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Left + i, EF.Top + j))
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Left + i, EF.Top + j))
                    Next
                Next

            Else 'facing right
                Dim spriteleft = cc.PosX + EF.CtrPoint.x - EF.Right
                Dim spritetop = cc.PosY - EF.CtrPoint.y + EF.Top

                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Right - i, EF.Top + j))
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Right - i, EF.Top + j))
                    Next
                Next

            End If

        Next
    End Sub

    Sub DisplayImg()
        'display bg and sprite on picturebox
        Dim i, j As Integer
        PutSprites()

        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                bmp.SetPixel(i, j, Img.Elmt(i, j))
            Next
        Next

        PictureBox1.Refresh()

        PictureBox1.Image = bmp
        PictureBox1.Width = bmp.Width
        PictureBox1.Height = bmp.Height
        PictureBox1.Top = 0
        PictureBox1.Left = 0

    End Sub

    Sub ResizeImg()
        Dim w, h As Integer

        w = PictureBox1.Width
        h = PictureBox1.Height

        Me.ClientSize = New Size(w, h)

    End Sub

    Sub createSpikes()
        Dim SP As CharacterShootedSpikes

        SP = New CharacterShootedSpikes
        SP.PosY = 50
        SP.Vy = 5

        If SC.FDirection = FaceDir.Left Then
            SP.PosX = 200
            SP.Vx = -5
            SP.FDirection = FaceDir.Left
        ElseIf SC.FDirection = FaceDir.Right Then
            SP.PosX = 50
            SP.Vx = 5
            SP.FDirection = FaceDir.Right
        End If

        SP.CurrState = SpikesShooted.Create
        ReDim SP.ArraySprites(2)
        SP.ArraySprites(0) = SpikesCreate

        ListChar.Add(SP)
        listSP.Add(SP)

    End Sub

    Sub CreateCeilingSpikes(n As Integer)
        Dim CS As CharacterCeilingSpikes

        CS = New CharacterCeilingSpikes


        CS.PosY = SC.PosY - 10

        CS.Vx = 0
        CS.Vy = 20
        CS.CurrState = SpikesFromCeiling.Create
        ReDim CS.ArraySprites(2)
        If n = 1 Then
            CS.ArraySprites(0) = CeilingSpikesCreate1
            If SC.FDirection = FaceDir.Left Then
                CS.PosX = 200
            Else
                CS.PosX = 100
            End If
        ElseIf n = 2 Then
            CS.PosX = 150
            CS.ArraySprites(0) = CeilingSpikesCreate2

        ElseIf n = 3 Then
            CS.PosX = 100
            CS.ArraySprites(0) = CeilingSpikesCreate3
        End If

        ListChar.Add(CS)
        listCS.Add(CS)

    End Sub


    Async Function dummyRespawn() As Task

        Await Task.Delay(TimeSpan.FromSeconds(5))

        Dim rndm As Integer = (PictureBox1.Width * Rnd())

        If rndm < 30 Then
            rndm = 30
        ElseIf rndm > 190 Then
            rndm = 190
        End If

        DM.Destroy = False

        DM.PosX = rndm
        DM.PosY = 157

        DM.Vx = 2
        DM.CurrState = Dummy.Stand

        If DM.PosX <= 120 Then
            DM.FDirection = FaceDir.Right
        Else
            DM.FDirection = FaceDir.Left
        End If

        ListChar.Add(DM)
        listDummy.Add(DM)

    End Function

    Sub collisionDetection()
        Dim listDummy1 As New List(Of CharacterDummy)

        For Each DM In listDummy
            If DM.Destroy = False Then
                listDummy1.Add(DM)
            End If
        Next

        listDummy = listDummy1

        For Each DM In listDummy

            Dim EFSC As CElmtFrame = SC.ArraySprites(SC.IndexArrSprites).Elmt(SC.FrameIndex)
            Dim EFDM As CElmtFrame = DM.ArraySprites(DM.IndexArrSprites).Elmt(DM.FrameIndex)

            Dim dmLeft As Integer = DM.PosX - EFSC.CtrPoint.x + EFSC.Left
            Dim dmRight As Integer = DM.PosX + EFSC.CtrPoint.x - EFSC.Right
            Dim dmTop As Integer = DM.PosY - EFSC.CtrPoint.y + EFSC.Top

            Dim scLeft As Integer = SC.PosX - EFSC.CtrPoint.x + EFSC.Left
            Dim scRight As Integer = SC.PosX + EFSC.CtrPoint.x - EFSC.Right
            Dim scBottom As Integer = SC.PosY + EFSC.CtrPoint.y - EFSC.Bottom

            If SC.FDirection = FaceDir.Left Then
                If scLeft <= dmRight + 40 And scLeft >= dmLeft And scBottom >= dmTop - 40 Then
                    DM.Destroy = True
                End If
            Else
                If scRight >= dmLeft - 40 And scRight <= dmRight And scBottom >= dmTop - 40 Then
                    DM.Destroy = True
                End If
            End If

        Next

        Dim listSP1 As New List(Of CharacterShootedSpikes)

        For Each sp In listSP
            If sp.Destroy = False Then
                listSP1.Add(sp)
            End If
        Next

        listSP = listSP1

        For Each SP In listSP

            Dim EFSP As CElmtFrame = SP.ArraySprites(SP.IndexArrSprites).Elmt(SP.FrameIndex)
            Dim EFDM As CElmtFrame = DM.ArraySprites(DM.IndexArrSprites).Elmt(DM.FrameIndex)

            Dim dmLeft As Integer = DM.PosX - EFSP.CtrPoint.x + EFSP.Left
            Dim dmRight As Integer = DM.PosX + EFSP.CtrPoint.x - EFSP.Right
            Dim dmTop As Integer = DM.PosY - EFSP.CtrPoint.y + EFSP.Top

            Dim spLeft As Integer = SP.PosX - EFSP.CtrPoint.x + EFSP.Left
            Dim spRight As Integer = SP.PosX + EFSP.CtrPoint.x - EFSP.Right
            Dim spBottom As Integer = SP.PosY + EFSP.CtrPoint.y - EFSP.Bottom

            If SP.FDirection = FaceDir.Left Then
                If spLeft <= dmRight + 40 And spLeft >= dmLeft And spBottom >= dmTop - 40 Then
                    DM.Destroy = True
                End If
            Else
                If spRight >= dmLeft - 40 And spRight <= dmRight And spBottom >= dmTop - 40 Then
                    DM.Destroy = True
                End If
            End If

        Next

        Dim listCS1 As New List(Of CharacterCeilingSpikes)

        For Each CS In listCS
            If CS.Destroy = False Then
                listCS1.Add(CS)
            End If
        Next

        listCS = listCS1

        For Each CS In listSP

            Dim EFCS As CElmtFrame = SC.ArraySprites(SC.IndexArrSprites).Elmt(SC.FrameIndex)
            Dim EFDM As CElmtFrame = DM.ArraySprites(DM.IndexArrSprites).Elmt(DM.FrameIndex)

            Dim dmLeft As Integer = DM.PosX - EFCS.CtrPoint.x + EFCS.Left
            Dim dmRight As Integer = DM.PosX + EFCS.CtrPoint.x - EFCS.Right
            Dim dmTop As Integer = DM.PosY - EFCS.CtrPoint.y + EFCS.Top

            Dim scLeft As Integer = SC.PosX - EFCS.CtrPoint.x + EFCS.Left
            Dim scRight As Integer = SC.PosX + EFCS.CtrPoint.x - EFCS.Right
            Dim scBottom As Integer = SC.PosY + EFCS.CtrPoint.y - EFCS.Bottom

            If CS.FDirection = FaceDir.Left Then
                If scLeft <= dmRight + 40 And scLeft >= dmLeft And scBottom >= dmTop - 40 Then
                    DM.Destroy = True
                End If
            Else
                If scRight >= dmLeft - 40 And scRight <= dmRight And scBottom >= dmTop - 40 Then
                    DM.Destroy = True
                End If
            End If

        Next

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim CC As Character

        PictureBox1.Refresh()

        For Each CC In ListChar
            CC.Update()
        Next

        If SC.CurrState = StateSplitSC.ShootStings And SC.CurrFrame = 5 Then
            createSpikes()
        End If

        If SC.CurrState = StateSplitSC.ShakeBody And SC.CurrFrame = 0 Then
            CreateCeilingSpikes(1)
        ElseIf SC.CurrState = StateSplitSC.ShakeBody And SC.CurrFrame = 7 Then
            CreateCeilingSpikes(2)
        ElseIf SC.CurrState = StateSplitSC.ShakeBody And SC.CurrFrame = 14 Then
            CreateCeilingSpikes(3)
        End If

        collisionDetection()

        If DM.Destroy = True Then
            dummyRespawn()
        End If

        Dim Listchar1 As New List(Of Character)

        For Each CC In ListChar
            If CC.Destroy = False Then
                Listchar1.Add(CC)
            End If
        Next

        ListChar = Listchar1

        DisplayImg()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Panel2.Visible = True
        Panel1.Visible = False
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Panel2.Visible = False
        Panel1.Visible = True
    End Sub

End Class

