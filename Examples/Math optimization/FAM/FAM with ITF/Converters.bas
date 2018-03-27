Attribute VB_Name = "Converters"
Option Explicit


Public Sub Model3()

      COMMON/COMB/ AK(4),CK(4)
      COMMON/BIX/ D(5),Q(4)
      COMMON/NOMA/AQM1(300),AQM2(300),NQ1,NQ2
      ASG = 1000#
      SP1 = 1#
      SPB = 1#
      IS=2
      C1 = 0.5
      C2 = 0.5
      IYQ = 1
      Kp = 11
      DO 10 IY=1,2
      IYT = IY - 1
      DO 10 IB=1,4
      IYB = IB - 1
      IDEBUG = 5
      Call SINNOM(Kp, IYT, IYB, IDEBUG)
      DO 10 M=1,2
      IF(M+IS.EQ.3) NK=NQ2
      IF(M+IS.EQ.4) NK=NQ1
      DO 20 I=2,NK
      IF(M+IS.EQ.3) P=(AQM2(I)+AQM2(I-1))/2
      IF(M+IS.EQ.4) P=(AQM1(I)+AQM1(I-1))/2
      IF(M+IS.EQ.3) CMIN=AQM2(I-1)
      IF(M+IS.EQ.3) CMAX=AQM2(I)
      IF(M+IS.EQ.4) CMIN=AQM1(I-1)
      IF(M+IS.EQ.4) CMAX=AQM1(I)
      MP = M
      ISP=IS
      IDEBUG = 1
      CALL COMBIM(KP,M,IS,P,AK,CK,IDIAGN,IDEBUG)
      IDEBUG = 3
      CALL IDPRM3(IYQ,CMIN,CMAX,C1,M,IS,DF,QOPT,AK,CK,IDEBUG)
      IDEBUG = 0
      CALL PROVKP(KP,1.,QOPT,1.,DF,MP,ISP,C1,1,AK,CK,SPD,
     *IDIAGN,IDEBUG)
      IF(M+IS.EQ.4) GOTO 145
      QHMI = AQM2(i - 1)
      QHMA = AQM2(i)
      GoTo 146
145   QHMI = AQM1(i - 1)
      QHMA = AQM1(i)
146   CONTINUE
      DO 142 J=1,10
      E2 = (QHMA - QHMI) / 20
      Qmax = (QHMA + QHMI) / 2 + E2
      Qmin = Qmax - 2 * E2
      QMI = Qmin
      QMA = Qmax
      IF(IYQ.EQ.2)QMA=1/QMAX
      IF(IYQ.EQ.2)QMI=1/QMIN
      IDEBUG = 0
      CALL RASPPP(KP,SP1,SPB,QMA,MP,ISP,C1,YMAX,CB,AK,CK,
     *IDIAGN,IDEBUG)
      CALL RASPPP(KP,SP1,SPB,QMI,MP,ISP,C1,YMIN,CB,AK,CK,
     *IDIAGN,IDEBUG)
      IF(IYQ.EQ.2) YMIN=YMIN*QMIN
      IF(IYQ.EQ.2) YMAX=YMAX*QMAX
      IF(YMAX-YMIN)155,156,157
155   QHMA = Qmax
      GoTo 142
157   QHMI = Qmin
142   CONTINUE
156   QM = (QHMA + QHMI) / 2
      QL = QM
      IF(IYQ.EQ.2)QL=1/QM
      CALL RASPPP(KP,SP1,SPB,QL,MP,ISP,C1,DF1,CBIX,AK,CK,
     *IDIAGN,0)
      WRITE(3,32)QM,QL,DF1
   32 FORMAT(2X,'QOPT=',G14.6,2X,'1/QOPT=',G14.6,2X,'DFOPT=',G14.6)
      CALL PROVKP(KP,1.,QL,1.,DF1,MP,ISP,C1,1,AK,CK,SPD,
     *IDIAGN,IDEBUG)
30    CONTINUE
20    CONTINUE
10    CONTINUE
      Stop
      End
End Sub

Public Sub IDPRM3(IYQ As Integer, Cmin As Double, Cmax As Double, C1 As Double, M As Integer, S As Integer, _
                 DFopt As Double, Qopt As Double, AK() As Integer, CK() As Integer)
'  Ìîäåëü ïðåîáðàçîâàòåëÿ-ïåðåíîñ÷èêà ÷àñòîòû
Dim FKI(3) As Double
Dim FKJ(3) As Double
Dim SFI(3) As Double
Dim SFJ(3) As Double
Dim DF1(9) As Double
Dim Q1(9) As Double
Dim FJ(9) As Double
Dim SJ(9) As Double
Dim IM As Integer
Dim IK As Integer
Dim i As Integer
Dim j As Integer
      If IYQ = 1 Then
      If IYQ = 2 Then IM = 3
If IYQ = 1 Then 'GoTo 300
  IM = 2
  If M + S = 4 Then ' GoTo 100
     FKI(1) = C1 - 1
     SFI(1) = (CK(2) - 1) / (AK(2) - 1)
     FKI(2) = C1 - AK(3) / (AK(3) - 1)
     SFI(2) = (CK(3) - 1) / (AK(3) - 1)
     FKJ(1) = C1 + 1 / (AK(1) - 1)
     SFJ(1) = (CK(1) - 1) / (AK(1) - 1)
     FKJ(2) = C1
     SFJ(2) = (CK(4) - 1) / (AK(4) - 1)
   Else '     GoTo 400
     FKI(1) = C1 - 1 + 1 / (AK(2) + 1)
     SFI(1) = (CK(2) - 1) / (AK(2) + 1)
     FKI(2) = C1 - 1
     SFI(2) = -Cmax
     FKJ(1) = C1
     SFJ(1) = -Cmin
     FKJ(2) = C1 - 1 / (AK(4) + 1)
     SFJ(2) = (CK(4) - 1) / (AK(4) + 1)
  End If '     GoTo 400
 Else
  If M + S = 3 Then GoTo 500
     FKI(1) = C1 + 1 / (CK(2) - 1)
     SFI(1) = (AK(2) - 1) / (CK(2) - 1)
     FKI(2) = C1
     SFI(2) = (AK(3) - 1) / (CK(3) - 1)
     FKI(3) = C1 - CK(3) / (CK(3) - 1)
     SFI(3) = SFI(2)
     If CK(1) <> 1 Then ' GoTo 770
      FKJ(1) = C1 + 1 / (CK(1) - 1)
      SFJ(1) = (AK(1) - 1) / (CK(1) - 1)
      FKJ(2) = C1 - 1
      SFJ(2) = SFJ(1)
     End If
     If CK(4) <> 1 Then ' GoTo 780
      FKJ(3) = C1 - CK(4) / (CK(4) - 1)
      SFJ(3) = (AK(4) - 1) / (CK(4) - 1)
     End If
     If CK(1) = 1 Or CK(4) = 1 Then GoTo 700
   Else '   GoTo 400
     FKI(1) = C1 + 1 / (CK(2) - 1)
     SFI(1) = (AK(2) + 1) / (CK(2) - 1)
     FKI(2) = C1
     SFI(2) = (AK(3) + 1) / (CK(3) - 1)
     FKI(3) = C1 - CK(3) / (CK(3) - 1)
     SFI(3) = SFI(2)
     If CK(1) <> 1 Then 'GoTo 870
      FKJ(1) = C1 + 1 / (CK(1) - 1)
      SFJ(1) = (AK(1) + 1) / (CK(1) - 1)
      FKJ(2) = C1 - 1
      SFJ(2) = SFJ(1)
     End If
     If CK(4) <> 1 Then 'GoTo 880
      FKJ(3) = C1 - CK(4) / (CK(4) - 1)
      SFJ(3) = (AK(4) + 1) / (CK(4) - 1)
     End If
     If CK(1) = 1 Or CK(4) = 1 Then GoTo 800
End If
 For i = 1 To IM
  For j = 1 To IM
    IK = j + IM * (i - 1)
    If FKI(i) - FKJ(j) = 0 Then
       DF1(IK) = 1000#
       Q1(IK) = 100#
     Else
       DF1(IK) = (SFI(i) - SFJ(j)) / (FKI(i) - FKJ(j))
       Q1(IK) = FKI(i) * DF1(IK) - SFI(i)
    End If
  Next j
 Next i
 DFM = 1000#
 IP = 0
 For i = 1 To IM
  For j = 1 To IM
    IK = j + IM * (i - 1)
    If Not (IYQ = 1 And (Q1(IK) < Cmin Or Q1(IK) > Cmax)) Then
    If Not (IYQ = 2 And (1 / Q1(IK) < Cmin Or 1 / Q1(IK) > Cmax)) Then
    If DF1(IK) > 0# Then
     If DF1(IK) < DFM Then
      DFM = DF1(IK)
      QM = Q1(IK)
      IP = IK
     End If
    End If
    End If
    End If
   Next j
 Next i
 Qopt = QM
 DFopt = DFM
 GoTo 1000
800
If CK(1) <> 1 Then GoTo 810
      FJ(1) = C1
      SJ(1) = (AK(1) - AK(2)) / (CK(1) - CK(2))
      FJ(2) = C1 + 1 / (CK(1) - CK(3))
      SJ(2) = (AK(1) - AK(3)) / (CK(1) - CK(3))
      FJ(3) = C1 - (CK(3) + 1) / (CK(3) - CK(1))
      SJ(3) = (AK(3) - AK(1)) / (CK(3) - CK(1))
      FJ(4) = C1 + CK(1) / (CK(2) - CK(1))
      SJ(4) = SJ(1)
      FJ(5) = C1 + (1 - CK(1)) / (CK(1) - CK(3))
      SJ(5) = (AK(1) - AK(3)) / (CK(1) - CK(3))
      FJ(6) = C1 + (CK(1) - CK(3) - 1) / (CK(3) - CK(1))
      SJ(6) = (AK(3) - AK(1)) / (CK(3) - CK(1))
810   If CK(4) <> 1 Then GoTo 820
      FJ(7) = C1 + (CK(4) + 1) / (CK(2) - CK(4))
      SJ(7) = (AK(2) - AK(4)) / (CK(2) - CK(4))
      FJ(8) = C1 + CK(4) / (CK(3) - CK(4))
      SJ(8) = (AK(3) - AK(4)) / (CK(3) - CK(4))
      FJ(9) = C1 - 1
      SJ(9) = (AK(3) - AK(4)) / (CK(3) - CK(4))
820   CONTINUE
      GoTo 900
700   CONTINUE
      If CK(1) <> 1 Then GoTo 720
      FJ(1) = C1
      SJ(1) = (AK(2) - AK(1)) / (CK(2) - CK(1))
      FJ(2) = C1 - 1 / (CK(3) - CK(1))
      SJ(2) = (AK(3) - AK(1)) / (CK(3) - CK(1))
      FJ(3) = C1 - (CK(3) + 1) / (CK(3) - CK(1))
      SJ(3) = (AK(3) - AK(1)) / (CK(3) - CK(1))
      FJ(4) = C1 + CK(1) / (CK(2) - CK(1))
      SJ(4) = SJ(1)
      FJ(5) = C1 + (1 - CK(1)) / (CK(1) - CK(3))
      SJ(5) = (AK(3) - AK(1)) / (CK(3) - CK(1))
      FJ(6) = C1 + (CK(1) - CK(3) - 1) / (CK(3) - CK(1))
      SJ(6) = (AK(3) - AK(1)) / (CK(3) - CK(1))
720   If CK(4) <> 1 Then GoTo 730
      FJ(7) = C1 + (CK(4) + 1) / (CK(2) - CK(4))
      SJ(7) = (AK(2) - AK(4)) / (CK(2) - CK(4))
      FJ(8) = C1 + CK(4) / (CK(3) - CK(4))
      SJ(8) = (AK(3) - AK(4)) / (CK(3) - CK(4))
      FJ(9) = C1 - 1
      SJ(9) = (AK(3) - AK(4)) / (CK(3) - CK(4))
730   CONTINUE
900   CONTINUE
IDN = 0
IDEN = 0
If CK(1) = 1 Or CK(4) = 1 Then IDEN = 1
If CK(1) = 1 And CK(4) = 1 Then IDN = 1
For j = 1 To 3 '60
 For i = 1 To 3 '60
  IK = i + 3 * (j - 1)
  If IDEN = 0 Then GoTo 61
  If IDN = 1 Then GoTo 62
  If IK <= 6 Then '160,160,161
   If CK(1) = 1 Then
     GoTo 62
    Else
     GoTo 61
   End If
  Else
   If CK(4) = 1 Then
     GoTo 62
    Else
     GoTo 61
   End If
  End If
62
  If FKI(i) - FJ(IK) = 0 Then '662,170,662
    DF1(IK) = 1000#
    Q1(IK) = 100#
    GoTo 60
   Else
    DF1(IK) = (SFI(i) - SJ(IK)) / (FKI(i) - FJ(IK))
    Q1(IK) = DF1(IK) * FJ(IK) - SJ(IK)
    GoTo 60
  End If
61
  If FKI(i) - FKJ(j) = 0 Then '661,180,661
    DF1(IK) = 1000#
    Q1(IK) = 100#
    GoTo 60
   Else
    DF1(IK) = (SFI(i) - SFJ(j)) / (FKI(i) - FKJ(j))
    Q1(IK) = DF1(IK) * FKI(i) - SFI(i)
  End If
60
 Next i
Next j  '60    CONTINUE
DFM = 1000#
IK = 0
For i = 1 To 9
  If Not (1 / Q1(i) < Cmin Or 1 / Q1(i) > Cmax) Then
  If (DF1(i) > 0#) Then
  If (DF1(i) < DFM) Then
   DFM = DF1(i)
   QM = Q1(i)
   IK = i
  End If
  End If
  End If
Next i '620   CONTINUE
DFopt = DFM
Qopt = QM
1000
End Sub


Public Sub test()

      DIMENSION AK(4), CK(4)
      COMMON/BIX/D(9),Q(9)
      COMMON/NOMA/AQM1(300),AQM2(300),NQ1,NQ2
      IDEBUG = 10
      CBIX = 0.5
      IYQ = 2
      ASG = 1000#
      SP1 = 1#
      SPB = 1#
      S = 2
      C1 = 0.5
      C2 = 0.5
      Kp = 5
      Call SINNOM(Kp, IDIAGN, IDEBUG)
      DO 10 M=1,2
      IF(M+IS.EQ.3) NK=NQ2
      IF(M+IS.EQ.4) NK=NQ1
      DO 20 I=2,NK
      IF(M+IS.EQ.3) P=(AQM2(I)+AQM2(I-1))/2
      IF(M+IS.EQ.4) P=(AQM1(I)+AQM1(I-1))/2
      IF(M+IS.EQ.3) CMIN=AQM2(I-1)
      IF(M+IS.EQ.3) CMAX=AQM2(I)
      IF(M+IS.EQ.4) CMIN=AQM1(I-1)
      IF(M+IS.EQ.4) CMAX=AQM1(I)
      IF(IYQ.EQ.2) P=1/P
      CALL COMBIM(KP,M,IS,P,AK,CK,IDIAGN,IDEBUG)
      CALL IDPRM2(IYQ,CMIN,CMAX,C1,C2,CBIX,M,IS,QOPT,DF,AK,CK,
     PIDEBUG)
      WRITE(3,406)QOPT,DF
  406 FORMAT(2X,'MOÄEËÜ N2',2X,'QOPT=',G14.6,2X,'DF=',G14.6)
      CALL IDPR(IYQ,CMIN,CMAX,C1,C2,CBIX,M,IS,Q1,DF1,IDEBUG)
      WRITE(3,405)Q1,DF1
  405 FORMAT(2X,'MOÄEËÜ N1',4X,'Q=',G14.6,2X,'DF=',G14.6)
      IF(IYQ.EQ.2) QOPT=1/QOPT
      IF(IYQ.EQ.2)DF=DF*QOPT
      IF(M+IS.EQ.4)GOTO 11
      IF(IYQ.EQ.1)MP=1
      IF(IYQ.EQ.1)ISP=2
      IF(IYQ.EQ.2)MP=2
      IF(IYQ.EQ.2)ISP=1
      GOTO14
11    MP = 2
      ISP = 2
14    CONTINUE
      CALL RASM(1.,1.,1.,QOPT,ASG,MP,ISP,C1,C2,CBIX,DF,F1HP,F1H,
     PF1B,F1BP,F2H,F2B,FBHP,FBH,FBB,FBBP,IDEBUG)
      CALL PROVTR(KP,F1H,F1B,F2H,F2B,FBH,FBB,1.,1.,SPBD,AK,CK,
     PIDIAGN,IDEBUG,2)
      IF(M+IS.EQ.4) GOTO 145
      QHMI = AQM2(i - 1)
      QHMA = AQM2(i)
      GoTo 146
145   QHMI = AQM1(i - 1)
      QHMA = AQM1(i)
146   CONTINUE
      DO 142 J=1,10
      E2 = (QHMA - QHMI) / 20
      Qmax = (QHMA + QHMI) / 2 + E2
      Qmin = Qmax - 2 * E2
      QMA = Qmax
      QMI = Qmin
      IF(IYQ.EQ.2)QMA=1/QMAX
      IF(IYQ.EQ.2)QMI=1/QMIN
      WRITE(3,33)QMA,QMI
   33 FORMAT(2X,10('-'),2X,'QMA=',G14.6,2X,'QMI=',G14.6)
      CALL RASTRS(KP,SP1,SPB,QMA,ASG,M,IS,C1,C2,CB,YMAX,AK,CK,
     *IDIAGN,0)
      CALL RASTRS(KP,SP1,SPB,QMI,ASG,M,IS,C1,C2,CB,YMIN,AK,CK,
     *IDIAGN,0)
      IF(YMAX-YMIN)155,156,157
155   QHMA = Qmax
      GoTo 142
157   QHMI = Qmin
142   CONTINUE
156   QM = (QHMA + QHMI) / 2
      QL = QM
      IF(IYQ.EQ.2) QL=1/QM
      CALL RASTRS(KP,SP1,SPB,QL,ASG,M,IS,C1,C2,CBIX,DF1,AK,CK,
     *IDIAGN,0)
      WRITE(3,32)QL,DF1
   32 FORMAT(2X,'QOPT=',G14.6,2X,'DFOPT=',G14.6)
      IF(M+IS.EQ.4)GOTO 12
      IF(IYQ.EQ.1)MP=1
      IF(IYQ.EQ.1)ISP=2
      IF(IYQ.EQ.2)MP=2
      IF(IYQ.EQ.2)ISP=1
      GoTo 13
12    MP = 2
      ISP = 2
13    CONTINUE
      CALL RASM(1.,1.,1.,QL,ASG,MP,ISP,C1,C2,CBIX,DF1,F1HP,F1H,
     PF1B,F1BP,F2H,F2B,FBHP,FBH,FBB,FBBP,IDEBUG)
      CALL PROVTR(KP,F1H,F1B,F2H,F2B,FBH,FBB,1.,1.,SPBD,AK,CK,
     PIDIAGN,IDEBUG,2)
600   CONTINUE
30    CONTINUE
20    CONTINUE
10    CONTINUE


End Sub

Public Sub IDPR(Q, C1, C2, M, S, AP, CP, DF, Q1, IDEBUG)

'
'  ÏPOÃPAMMA IDPR2 ÏPEÄHAÇHA×EHA ÄËß OÏTÈMÈÇAÖÈÈ ÈÄEAËÜHOÃO ÏPEOÁPAÇOBA-
'   TEËß ×ACTOTÛ (KOÝÔ.ÏPßM.ÔÈËÜTPOB=1)(ÏEPECTPAÈBAEMÛÉ ÏPECEËEKTOP)
'
'  BXOÄHÛE ÏAPAMETPÛ:
'  Q    -COOTHOØEHÈE CMEØÈBAEMÛX ×ACTOT(PAÁO×ÈX K OÏOPHÛM)
'  C1,C2 -KOÝÔ.BKËAÄA HÈÆHÈX ×ACTOT B ÄÈAÏAÇOH DF
'  M,IS  -ÈÄEHTÈÔÈKATOPÛ BÈÄOB ÏPEOÁPAÇOBAHÈß ×ACTOTÛ
'  AP,CP -MACCÈBÛ KOÝÔ.B ÓPABHEHÈßX 4-X KOMÁÈH.×ACTOT FK=AP*Q+CP
'        ÁËÈÆAÉØÈX K Q
'  IDEBUG-OTËAÄO×HAß ÏEPEMEHHAß(0-OTËAÄKÈ HET,>0-OTËAÄO×HAß ÏE×ATÜ)
'
'  BÛXOÄHÛE ÏAPAMETPÛ:
'  DF   -OÏTÈMAËÜHÛÉ HOPMÈPOBAHHÛÉ KOÝÔÔ.ÄÈAÏAÇOHA ×ACTOT
'  Q1   -OÏTÈMAËÜHOE COOTHOØEHÈE CMEØÈBAEMÛX ×ACTOT
'
'  BHÓTPEHHÈE ÏAPAMETPÛ Ï/ÏPOÃPAMMÛ:
'  C(4)  -MACCÈB KOOPÄ.ÏO OCÈ AÁÖÈCC TO×EK ÏOPAÆEHHÛX KOMÁÈH.×ACTOTAMÈ
'  CMIN,CMAX-KOOPÄ.ÏOPAÆEHHÛX TO×EK ÁËÈÆAÉØÈX K Q,CËEBA È CÏPABA OT Q
'     COFR-MACCÈB KOOPÄ.ÇOHÛ ÔÈËÜTPAÖÈÈ( C 1 ÏO 4-AÁÖÈCCÛ,5-6-OPÄÈHATÛ)
'     FL1,FL2,FH1,FH2-HOPMÈPOBAHHÛE ÇHA×EHÈß KPAÈHÈX ×ACTOT ÄÈAÏAÇOHOB
'     CUT-KOÝÔ.BKËAÄA BÛXOÄHOÈ ×ACTOTÛ B ÄÈAÏAÇOH DF
'     FUT-HOPMÈPOBAHHOE ÇHA×EHÈE BÛXOÄHOÈ ×ACTOTÛ
'
'  ÏPOÃPAMMÈCT:ËOÃÈHOB            ÄATA: 28.03.84
'
Dim COFR(6)
Dim AP(4), CP(4), C(4)
'
'C  HAXOÆÄEHÈE KOOPÄ.ÏOPAÆEHHÛX TO×EK
'
      Q2 = Q
      If (Q > 1#) Then
       Q = 1# / Q
       M = S
       S = 2
      End If
      For i = 1 To 4
      C(i) = (CP(i) - (-1) ^ S) / ((-1) ^ M - AP(i))
      Next i
      Cmin = AMAX1(C(1), C(4))
      Cmax = AMIN1(C(2), C(3))
      If (Not (Q = AMAX1(Cmin, Q) And Q = AMIN1(Cmax, Q))) Then GoTo 902
      If (Q2 > 1) Then
       Q = 1# / Q
      End If
'
'  HAXOÆÄEHÈE HOMEPOB KOÝÔÔ.ÓPABHEHÈÉ KOMÁÈH.×ACTOT OTHOCÈTEËÜHO KOTOPÛX
'  ÏPOÈÇBOÄÈTCß OÏTÈMÈÇAÖÈß
106
  If (M + S = 3) Then
'  BÛ×ÈTAHÈE ×ACTOT
     If (C(1) = C(4)) Then GoTo 201
      If (C(4) = Cmin) Then GoTo 201
      I2 = 1
      GoTo 300
201   I2 = 4
  
300   If (C(2) = C(3)) Then GoTo 301
      If (C(2) = Cmax) Then GoTo 301
      I1 = 3
      GoTo 302
301   I1 = 2
302   X1 = AP(I1)
      X2 = CP(I2)
  Else ' GoTo 500
'
'  CÓMMÈPOBAHÈE ×ACTOT
'
      If (C(1) = C(4)) Then GoTo 401
      If (C(1) = Cmin) Then GoTo 401
      I1 = 4
      GoTo 402
401   I1 = 1
402   If (C(2) = C(3)) Then GoTo 403
      If (C(3) = Cmax) Then GoTo 403
      I2 = 2
      GoTo 404
403   I2 = 3
404   X1 = -1#
      X2 = AP(I2) + CP(I2) - 1#
 End If
'
'  BÛ×ÈCËEHÈE OÏTÈMAËÜHÛX Q1 È DF
'
500
  XZ = AP(I2) * (1# - CP(I1)) + AP(I1) * (CP(I2) - 1#) + (CP(I1) - CP(I2)) * (-1) ^ M
      X = C1 * XZ
      Y = X2 * (CP(I1) - 1#) + X1 * (1 - CP(I2))
      Z = C2 * XZ
      W = X2 * ((-1) ^ M - AP(I1)) + X1 * (AP(I2) - (-1) ^ M)
      Q1 = (X + Y) / (Z + W)
      FX = 1# - CP(I1) + Q1 * (1# - AP(I1) * (-1) ^ M) * (-1) ^ M
      FY = C2 * (1 - CP(I1)) + C1 * (1# - AP(I1) * (-1) ^ M) * (-1) ^ M + X1
      If (FY <> 0) Then GoTo 311
      DF = 1#
      GoTo 312
311   DF = FX / FY
'  312 IF(IDEBUG.GE.4) WRITE(5,501)I1,I2,X1,X2,X,Y,Z,W,FX,FY
'  501 FORMAT(/1X,'I1=',I2,'I2=',I2,3X,'X1=',G12.4,2X,'X2=',G12.4
'     P/1X,'X=',G12.4,2X,'Y=',G12.4,2X,'Z=',G12.4,2X,'W=',G12.4
'     P/1X,'FX=',G12.4,2X,'FY=',G12.4)
'C
'C  ÄÈAÃHOCTÈKA ÏPOÃPAMMÛ.KOPPEKTÈPOBKA DF È Q1
'C
      ID = 0
605   FL1 = Q1 - DF * C1
      FL2 = 1# - DF * C2
      FH1 = Q1 + (1# - C1) * DF
      FH2 = 1 + (1# - C2) * DF
      CUT = (C1 - 0.5) * (-1) ^ M + (C2 - 0.5) * (-1) ^ S
      FUT = (-1) ^ S + Q1 * (-1) ^ M - DF * CUT
      COFR(4) = FL1 / FH2
      COFR(1) = FL1 / FL2
      COFR(6) = FUT / FH2
      COFR(5) = FUT / FL2
      COFR(2) = FH1 / FL2
      COFR(3) = FH1 / FH2
      DO 620 I=1,4
      COFR(i) = COFR(i) * 100000#
      COFR(i) = AINT(COFR(i))
620   COFR(i) = COFR(i) / 100000#
      Cmin = AINT(Cmin * 100000#) / 100000#
      Cmax = AINT(Cmax * 100000#) / 100000#
'      IF(IDEBUG.LE.1) GO TO 701
'      WRITE(5,702)FL1,FL2,FH1,FH2,FUT,CUT
'      WRITE(5,700)COFR,CMIN,CMAX,AP,CP
'  702 FORMAT(/' FL=',2G14.6,3X,'FH=',2G14.6,/,' FUT=',G14.6,3X,'CUT=
'     *',G14.6)
'  700 FORMAT(/' COFR=',G14.6,1X,5G14.6,/,' CMIN=',G14.6,3X,'CMAX=',G14.6
'     */' AP=',4G12.4/' CP=',4G12.4)
'701   CONTINUE
      If (M + S = 4) Then GoTo 600
      If (ID = 1) Then GoTo 660
      If (Cmin <= COFR(1) And Cmax >= COFR(3)) Then GoTo 601
      ID = ID + 1
      DF = (Cmax - Cmin) / (1 - Cmax * (1 - C2) - C2 * Cmin)
      Q1 = Cmin - (C2 * Cmin - C1) * DF
      GoTo 605
  660 DO 661 J=1,4,3
      IF(AP(J))662,661,662
662   CN = (COFR(6) - CP(j)) / AP(j)
      CN = AINT(CN * 100000#) / 100000#
      If (CN.GT.COFR(4)) Then GoTo 604
661   CONTINUE
      DO 663 J=2,3,1
      IF(AP(J))664,663,664
664   CN = (COFR(5) - CP(j)) / AP(j)
      CN = AINT(CN * 100000#) / 100000#
      If (CN.LT.COFR(2)) Then GoTo 604
663   CONTINUE
      GoTo 601
600   If (ID = 1) Then GoTo 650
      If (Cmin <= COFR(4) And Cmax >= COFR(2)) Then GoTo 601
      ID = ID + 1
      DF = (Cmax - Cmin) / (1 + Cmin * (1 - C2) + Cmax * C2)
      Q1 = Cmax - (Cmax * C2 + 1 - C2) * DF
      GoTo 605
  650 DO 651 J=1,4,3
      IF(AP(J))652,651,652
652   CN = (COFR(5) - CP(j)) / AP(j)
      CN = AINT(CN * 100000#) / 100000#
      If (CN.GT.COFR(1)) Then GoTo 604
651   CONTINUE
      DO 653 J=2,3,1
      IF(AP(J))654,653,654
654   CN = (COFR(6) - CP(j)) / AP(j)
      CN = AINT(CN * 100000#) / 100000#
      If (CN.LT.COFR(3)) Then GoTo 604
653   CONTINUE
      GoTo 601
  604 IF(IDEBUG.GE.4)WRITE(5,603)
  603 FORMAT(//2X,'*BHÈMAHÈE!*B Ï/Ï IDPR2 ÇAÖÈKËÈBAHÈE.ÓMEHÜØÈTE'/13X,
     P 'ÏOPßÄOK KOMÁÈH.×ACTOT')
601   CONTINUE
      IF(Q.LE.1) GO TO 505
      DF = DF / Q1
      Q1 = 1# / Q1
C
C  ÏE×ATÜ PEÇÓËÜTATOB È ÄÈAÃHOCTÈ×ECKAß ÏE×ATÜ
C
505   CONTINUE
      IF(IDEBUG.LE.4) GO TO 904
      WRITE(5,80)
   80 FORMAT(//)
      WRITE(5,       3)
      WRITE(5,       4)(I1,COFR(I1),COFR((2*I1-(-1)^I1-3)/4+5),I1=1,4)
    3 FORMAT(/1X,35('-')/1X,'I',1X,'N',1X,'I','   AÁÖÈCCA    I  OPÄÈHAT
     PA i  '/35('-'))
    4 FORMAT(1X,'I',I2,' I ',G12.4,' I ',G12.4,' I ')
      WRITE(5,       5)
    5 FORMAT(1X,35('-'))
  800 WRITE(5,801)
  801 FORMAT(1X,'*OÏTÈMÈÇAÖÈß ÈÄEAËÜHOÃO ÏPEOÁPAÇOBATEËß ×ACTOTÛ*'//1X
     P,9('*'),'C HEÏEPECTPAÈBAEMÛM ÏPECEËEKTOPOM',11('*'))
      IF(M.EQ.2.AND.IS.EQ.2)GO TO 804
      WRITE(5,803)Q
      GoTo 806
  804 WRITE(5,802)Q
  802 FORMAT(//8X,'*CÓMMÈPOBAHÈE ×ACTOT,Q=',E10.3,'*')
  803 FORMAT(//9X,'*BÛ×ÈTAHÈE ×ACTOT,Q=',E10.3,'*')
  806 WRITE(5,805)DF,Q1
  805 FORMAT(1X/3X,'OÏTÈMAËÜHÛÉ ÄÈAÏAÇOH BXOÄHÛX ×ACTOT:'/3X,'DF=',E15.8
     P,//3X,'OÏTÈMAËÜHOE COOTHOØEHÈE ×ACTOT:'/3X,'Q1=',E15.8)
      GoTo 904
  902 WRITE(5,903)Q,(I,AP(I),CP(I),I=1,4)
  903 FORMAT(1X,'*BHÈMAHÈE!*OØÈÁKA*BBEÄEHÛ KOÝÔ.ÓPABHEHÈÉ KOMÁÈH.×ACTOT'
     P/2X,'/FK=AP*Q+CP/HE ÁËÈÆAÉØÈX K Q'/2X,'Q=',F6.1,/9X,'AP',8X,'CP'//
     P(3X,I2,2(3X,F7.3)))
      WRITE(5,104)CMIN,CMAX,(I,C(I),I=1,4)
904   Return

End Sub

