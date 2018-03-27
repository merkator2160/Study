Attribute VB_Name = "FareyNomogramma"
Option Explicit
'   ��������� ������� ���������� �������������� ������ �
'     ������� ��������� �������������� ������ �
'     �������� ����������� ����������� ������ Q

'   ������ ��������
'     ���������, ����������� ��� ������������
Public Const TNFullNonLinearity = 0 '  N<KP & M<KP
Public Const TNTriangleNonLinearity = 1  ' N+M<KP
'  Public Const TNHalfNonLinearity =2 '  N<(KP-1)/2 & M<(KP-1)/2

'     ���������, ����������� ��� ���������������
Public Const TCSimple = 0
Public Const TCBallanceGeterodin = 1
Public Const TCBallanceSignal = 2
Public Const TCDoubleBallance = 3

Public Kp As Integer ' ���������� ������� �������������� ������
Public TN As Integer ' ��� ������������
Public TC As Integer ' ��� ���������������
Public AllCombin As Boolean  ' ���� ������� ���� �������������� ������
                          '  � ������ ���������� �����

'     ��������� ����������� ����� ��������� ����� ����������
'       �������������� ������ ��� �������� ������������� �����������
Public Const FileOut = "Nomogramma.txt"
Public Const FileOutFarey = "FareySeries.txt"
Public Const FileOutPorT = "SintezPorT.txt"
Public Const FileOutAllCombins = "NomogrammaAllCombins.txt"


'' ����� ���������� ��� �������� ����. ������ �� ������
'Public IRPR As Integer
'Public IA1(20) As Integer
'Public IC1(20) As Integer
'Public NN As Integer

'  �������� ������� ��������� �������������� ������
'    � ��������� ����������� ����������� ������ Q
Public AP(4) As Integer
Public CP(4) As Integer


'   ����� ���������� �������� Sinnom � Combin
'      ������������ ���������� ���������� ����� �
'       �������������� ������ ��� KQ = 120, NX = 240 -- KP <=20
'       �������������� ������ ��� KQ = 1200, NX = 2400 -- KP <=80-85 ��� �������� ����������
    Const KQ = 1200
    Const NX = 2400
'    ������� ������������� ����. ������ ����� ���. ����� (���������� ������)
    Public KMP1(KQ) As Integer  '  � ������������� �����������
    Public KMP2(KQ) As Integer
    Public KMM1(KQ) As Integer  '  � ������������� �����������
    Public KMM2(KQ) As Integer
'    ������ ���������� ����� (���������� ������)
    Public CR(KQ) As Double ' ��� ������������ ���������
    Public CQ(KQ) As Double ' ��� ������������ �����������
    Public BR(KQ) As Double  ' ��� ��������� ���������
    Public BQ(KQ) As Double  ' ��� ��������� �����������
    Public NC As Integer  ' ����������� ����� ���������� ����� ��� ������������
    Public NB As Integer  '  � ��������� ������ ��������������
'    ������ ����� ����� (���������� �������� ������ ���������� �����)
    Public FR(KQ) As Integer '  ���������
    Public FQ(KQ) As Integer '  �����������
    Public FN As Integer  ' ����������� ����� ������ ���� ����� ���
'    ������ ������������� �������������� ������
    Public AX1(NX) As Integer  ' ��� ������������
    Public CX1(NX) As Integer
    Public AX2(NX) As Integer  ' ��� ���������
    Public CX2(NX) As Integer
    Public NX1 As Integer  ' ����������� ����� �������������� ������
    Public NX2 As Integer  '    �������������� ��� ������������ � ���������
         
Public Sub FareySeries(Kp As Integer)
'------------------------------------------------------------------------+
' ���������  FareySeries  ������������� ��� ������� ������������������
' ������ ���� ����� ������� Kp, ��������� ������������ ����������������
' �������� ���������� ������� �������� ������
'------------------------------------------------------------------------+
'     ������� ������:
'       Kp - ������ �������������� ���� �����
'     �������� ������:
'       FR(I) - ��������� ���� �����
'       FQ(I) - ����������� ���� �����
'       FN   - ������� ����� ������ ���� �����
'------------------------------------------------------------------------+
Dim k As Integer
Dim i As Integer
Dim j As Integer
Dim N As Integer
Dim jk As Integer
' ������������� ��������� ����� ����� ������� Kp=1
  FN = 2
  FR(1) = 0
  FQ(1) = 1
  FR(2) = 1
  FQ(2) = 1
  N = FN
' ������ ��������� ����� �����
 For k = 2 To Kp
  i = 1
  Do
   i = i + 1
   If ((FQ(i - 1) + FQ(i)) <= k) Then
'  �������� ����� ���� ���� ����� � ������������������
    N = N + 1   ' ��������� ����� ������ ���� ����� �� 1
    jk = i + 1  ' ���������� ������ ������ ������ ����
    j = N
'  �������� ������� ���� �� ���� ���� ����
    Do
      FR(j) = FR(j - 1)
      FQ(j) = FQ(j - 1)
      j = j - 1
    Loop While j >= jk
'  �������� ����� ���� ���� �����
    FR(i) = FR(i - 1) + FR(i + 1)
    FQ(i) = FQ(i - 1) + FQ(i + 1)
    i = i + 1
   End If
  Loop While i <> N
 Next k
FN = N
End Sub

Public Sub FileOutFareySeries(Kp As Integer)
'  ��������� ������ ����������� ������� ���� �����
   
   Dim i As Integer
   Dim KS1 As Integer
   Dim KS2 As Integer
   Dim RP1 As Integer
   Dim RP2 As Integer
   Dim Nf As Integer
     Nf = FreeFile
      Open App.Path + "\" + FileOutFarey For Append As Nf
      Print #Nf, "������� ������ �/� FareySeries: "
      Print #Nf, "��� ����� ������� K�="; Kp
      Print #Nf, "����� ���������� ������ ���� ����� Fn="; FN
    For i = 1 To FN
      Print #Nf, " R("; i; ")/Q("; i; ")="; FR(i); "/"; FQ(i); "="; FR(i) / FQ(i)
    Next i
    Print #Nf,
   Close #Nf
End Sub

Public Sub SintezPorT(Kp As Integer, _
                      TypeOfNonLinearity As Integer, TypeOfConvertor As Integer)
' ��������� ������� ������� ���������� ����� ���������� �������������� ������
'    ������������ ������ �� ������ ����� �����
Dim i As Integer
Dim j As Integer
Dim N As Integer    ' ������ �������� ���������� �����
Dim YesComb As Boolean  ' �������� �� ����� ��� ���� ������
Dim MPlus As Integer  ' ����������� �������������� ������� � ������������� �����������
Dim NPlus As Integer  ' ��������� ���� �������������� �������
Dim MMinus As Integer  ' ����������� �������������� ������� � ������������� �����������
Dim NMinus As Integer  ' ��������� ���� �������������� �������
Dim YesPlus As Boolean  ' �������� �� ����. ������� � ������������� �����������
Dim YesMinus As Boolean ' �������� �� ����. ������� � ������������� �����������
Dim k As Integer  ' ����������� ���������� ����. ������ ����� �������. �����

k = 1   ' �������� ������ �������� �������������� ������
If TypeOfNonLinearity = 1 Then
'  ������ ������������������ ���������� ����� ��� ������������ ������
'   �������� ���������� �������������� ������
  FareySeries Kp
  i = 2
  N = FN
  Do
   Do
    YesComb = False
    If FR(i) + FQ(i) < Kp Then YesComb = True
'   �������������� �������� �� ������������ �� ��������� ����������
      If YesComb Then
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * FQ(i) + 1
       NPlus = -k * FR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * FQ(i) + 1
       NMinus = k * FR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  ������������� �������� ���������� �������������� ������
       Case 0 ' ������� ���������������
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' ��������� �� ������������� �����
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' ��������� �� ����������� �����
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' ������� ��������� ���������������
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 And (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 And (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       End Select
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
     End If
'   ������������ ������ ���� �����
'    ��� ������������ ������������� R+Q>=Kp
    If YesComb Then Exit Do
      N = N - 1
      For j = i To N
        FR(j) = FR(j + 1)
        FQ(j) = FQ(j + 1)
      Next j
   Loop While i < N
   i = i + 1
  Loop While i < N
  FN = N
'   ���������� ���������� ����� � �����. ��������
  NC = FN
  For i = 1 To NC
   CR(i) = FR(i)
   CQ(i) = FQ(i)
  Next i
'  ������ ������������������ ���������� ����� ��� ��������� ������
'   �������� ���������� �������������� ������
  FareySeries Kp + 1
  i = 1
  N = FN
  Do
   Do
    YesComb = False
    If FR(i) + FQ(i) <= Kp + 1 Then YesComb = True
'   ������������ ������ ���� �����
'    ��� ��������� ������������� R+Q>Kp+1
'   �������������� �������� �� ������������ �� ��������� ����������
      If YesComb Then
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * FQ(i) - 1
       NPlus = -k * FR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * FQ(i) - 1
       NMinus = k * FR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  ������������� �������� ���������� �������������� ������
       Case 0 ' ������� ���������������
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' ��������� �� ������������� �����
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' ��������� �� ����������� �����
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' ������� ��������� ���������������
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 And (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 And (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       End Select
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
     End If
'   ������������ ������ ���� �����
'    ��� ������������ ������������� R+Q>=Kp
    If YesComb Then Exit Do
      N = N - 1
      For j = i To N
        FR(j) = FR(j + 1)
        FQ(j) = FQ(j + 1)
      Next j
   Loop While i < N
   i = i + 1
  Loop While i < N
  FN = N
'   ���������� ���������� ����� � �����. ��������
  NB = FN
  For i = 1 To NB
   BR(i) = FR(i)
   BQ(i) = FQ(i)
  Next i
 Else
'  ������ ������������������ ���������� ����� ��� ��������� ������
'   ������ ���������� �������������� ������
  FareySeries Kp
'   ���������� ���������� ����� � �����. ��������
  NB = FN
  For i = 1 To NB
   BR(i) = FR(i)
   BQ(i) = FQ(i)
  Next i
'  ��������� ������������ ���������� ����� �� ������ ��������
  N = NB
  i = 1
  Do
   Do
    YesComb = False
'   �������������� �������� �� ������������ �� ��������� ����������
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * BQ(i) - 1
       NPlus = -k * BR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * BQ(i) - 1
       NMinus = k * BR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  ������������� �������� ���������� �������������� ������
       Case 0 ' ������� ���������������
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' ��������� �� ������������� �����
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' ��������� �� ����������� �����
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' ������� ��������� ���������������
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 And (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 And (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       End Select
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
'   ������������ ������ ���� �����
'    ��� ������������ ������������� R+Q>=Kp
    If YesComb Then Exit Do
      N = N - 1
      For j = i To N
        BR(j) = BR(j + 1)
        BQ(j) = BQ(j + 1)
      Next j
   Loop While i < N
   i = i + 1
  Loop While i < N
  NB = N

'  ������ ������������������ ���������� ����� ��� ������������ ������
'   ������ ���������� �������������� ������
  FareySeries Kp
'   ���������� ���������� ����� � �����. ��������
  NC = FN
  For i = 1 To NC
   CR(i) = FR(i)
   CQ(i) = FQ(i)
  Next i
 NC = NC - 1
 CR(NC) = CR(NC + 1)
 CQ(NC) = CQ(NC + 1)
End If
'  ��������� ������������ ���������� ����� �� ������ ��������
  N = NC
  i = 1
  Do
   Do
    YesComb = False
'   �������������� �������� �� ������������ �� ��������� ����������
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * CQ(i) + 1
       NPlus = -k * CR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * CQ(i) + 1
       NMinus = k * CR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  ������������� �������� ���������� �������������� ������
       Case 0 ' ������� ���������������
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' ��������� �� ������������� �����
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' ��������� �� ����������� �����
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' ������� ��������� ���������������
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 And (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 And (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       End Select
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
'   ������������ ������ ���� �����
'    ��� ������������ ������������� R+Q>=Kp
    If YesComb Then Exit Do
      N = N - 1
      For j = i To N
        CR(j) = CR(j + 1)
        CQ(j) = CQ(j + 1)
      Next j
   Loop While i < N
   i = i + 1
  Loop While i < N
  NC = N
End Sub

Public Sub FileOutSintezPorT(Kp As Integer, _
                      TypeOfNonLinearity As Integer, TypeOfConvertor As Integer)
'  ��������� ������ ����������� ������� ������� ���������� �����
'    ���������� �������������� ������ �� ��������������� ����
   
   Dim i As Integer
   Dim KS1 As Integer
   Dim KS2 As Integer
   Dim RP1 As Integer
   Dim RP2 As Integer
   Dim Nf As Integer
     Nf = FreeFile
      Open App.Path + "\" + FileOutPorT For Append As Nf
      Print #Nf, "������� ������ �/� SintezPorT: "
      Print #Nf, "���������� �������������� ������ ��� Kp="; Kp

      If TypeOfNonLinearity = 0 Then
        Print #Nf, "������������� ������ ���������� �������������� ������"
       Else
        Print #Nf, "������������� �������� ���������� �������������� ������"
      End If
      If TypeOfConvertor = 0 Then
        Print #Nf, "�������� ���������������"
      End If
      If TypeOfConvertor = 1 Then
        Print #Nf, "� ������ ����������� �� ������������� �����"
      End If
      If TypeOfConvertor = 2 Then
        Print #Nf, "� ������ ����������� �� ����������� �����"
      End If
      If TypeOfConvertor = 3 Then
        Print #Nf, "��� ���������� ���������� ��������������"
      End If
      Print #Nf,
      Print #Nf, "������������ ������"
    For i = 1 To NC
'      KS1 = KMP1(i)
'      KS2 = KMM1(i)
'      RP1 = Abs(AX1(KS1)) + Abs(CX1(KS1))
'      RP2 = Abs(AX1(KS2)) + Abs(CX1(KS2))
      Print #Nf, " I="; i; CR(i); "/"; CQ(i); " Q="; CR(i) / CQ(i)
'      If KS1 > 0 Then
'        Print #NF, "     +   A="; AX1(KS1); " C="; CX1(KS1)
'       Else
'        Print #NF, "     +   A=   ---       C=      ---    "
'      End If
'      If KS2 > 0 Then
'        Print #NF, "     -   A="; AX1(KS2); " C="; CX1(KS2)
'       Else
'        Print #NF, "     -   A=   ---       C=      ---    "
'      End If
    Next i
      Print #Nf,
      Print #Nf, "��������� ������"
     For i = 1 To NB
'      KS1 = KMP2(i)
'      KS2 = KMM2(i)
'      RP1 = Abs(AX2(KS1)) + Abs(CX2(KS1))
'      RP2 = Abs(AX2(KS2)) + Abs(CX2(KS2))
      Print #Nf, " I="; i; BR(i); "/"; BQ(i); " Q="; BR(i) / BQ(i)
'      If KS1 > 0 Then
'        Print #NF, "     +   A="; AX2(KS1); " C="; CX2(KS1)
'       Else
'        Print #NF, "     +   A=   ---       C=      ---    "
'      End If
'      If KS2 > 0 Then
'        Print #NF, "     -   A="; AX2(KS2); " C="; CX2(KS2)
'       Else
'        Print #NF, "     -   A=   ---       C=      ---    "
'      End If
     Next i
      Print #Nf,
      Print #Nf,
   Close #Nf
End Sub

Public Sub CalcNomogramma(Kp As Integer, _
      TypeOfNonLinearity As Integer, TypeOfConvertor As Integer)
'   ��������� ������� �������������� ������, ���������� ����� ���������� �����
'     ���������� �������������� ������
Dim MPlus As Integer  ' ����������� �������������� ������� � ������������� �����������
Dim NPlus As Integer  ' ��������� ���� �������������� �������
Dim MMinus As Integer  ' ����������� �������������� ������� � ������������� �����������
Dim NMinus As Integer  ' ��������� ���� �������������� �������
Dim YesPlus As Boolean
Dim YesMinus As Boolean
Dim k As Integer ' ����� �������������� ������� ���������� ����� ������������� ���. �����
Dim i As Integer
'  ��������� ������������� ��������
k = 1  ' ������������� ������ ���������� ����� � ���. ��������� �������������� ������
'  ��������� �������� � ���������� �������������� ������
  For i = 1 To NC  ' ��� ������������ ������
    KMP1(i) = 0  '  � ������������� �����������
    KMM1(i) = 0  '  � ������������� �����������
  Next i
  For i = 1 To NB  ' ��� ��������� ������
    KMP2(i) = 0  '  � ������������� �����������
    KMM2(i) = 0  '  � ������������� �����������
  Next i
  NX1 = 0 ' ��� ������������ ������ ����� �������������� ������
  NX2 = 0 ' ��� ��������� ������
'  ������ ������ ���������� ����� ��� ������������ ������
  For i = 1 To NC
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
   MPlus = k * CQ(i) + 1
   NPlus = -k * CR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
   MMinus = -k * CQ(i) + 1
   NMinus = k * CR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
   YesPlus = False
   YesMinus = False
   If TypeOfNonLinearity = 0 Then
'  ������������� ������ ���������� �������������� ������
     If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
       YesMinus = True
     End If
    Else
'  ������������� �������� ���������� �������������� ������
     If Abs(MPlus) + Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) + Abs(NMinus) < Kp Then
       YesMinus = True
     End If
   End If
'  ���������� ������������� �������������� ����� � �� ��������
   If YesPlus Then
     NX1 = NX1 + 1
     KMP1(i) = NX1
     AX1(NX1) = MPlus
     CX1(NX1) = NPlus
   End If
   If YesMinus Then
     NX1 = NX1 + 1
     KMM1(i) = NX1
     AX1(NX1) = MMinus
     CX1(NX1) = NMinus
   End If
  Next i
'  ������ ������ ���������� ����� ��� ��������� ������
  For i = 1 To NB
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
   MPlus = k * BQ(i) - 1
   NPlus = -k * BR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
   MMinus = -k * BQ(i) - 1
   NMinus = k * BR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
   YesPlus = False
   YesMinus = False
   If TypeOfNonLinearity = 0 Then
'  ������������� ������ ���������� �������������� ������
     If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
       YesMinus = True
     End If
    Else
'  ������������� �������� ���������� �������������� ������
     If Abs(MPlus) + Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) + Abs(NMinus) < Kp Then
       YesMinus = True
     End If
   End If
'  ���������� ������������� �������������� ����� � �� ��������
   If YesPlus Then
     NX2 = NX2 + 1
     KMP2(i) = NX2
     AX2(NX2) = MPlus
     CX2(NX2) = NPlus
   End If
   If YesMinus Then
     NX2 = NX2 + 1
     KMM2(i) = NX2
     AX2(NX2) = MMinus
     CX2(NX2) = NMinus
   End If
  Next i
End Sub
                     
Public Sub FileOutNomogramma(Kp As Integer, _
                      TypeOfNonLinearity As Integer, TypeOfConvertor As Integer)
'  ��������� ������ ����������� ������� ���������� �������������� ������
   
   Dim i As Integer
   Dim KS1 As Integer
   Dim KS2 As Integer
   Dim RP1 As Integer
   Dim RP2 As Integer
   Dim Nf As Integer
     Nf = FreeFile
      Open App.Path + "\" + FileOut For Append As Nf
      Print #Nf, "������� ������ �/� CalcNomogramma: "
      Print #Nf, "���������� �������������� ������ ��� Kp="; Kp

      If TypeOfNonLinearity = 0 Then
        Print #Nf, "������������� ������ ���������� �������������� ������"
       Else
        Print #Nf, "������������� �������� ���������� �������������� ������"
      End If
      If TypeOfConvertor = 0 Then
        Print #Nf, "�������� ���������������"
      End If
      If TypeOfConvertor = 1 Then
        Print #Nf, "� ������ ����������� �� ������������� �����"
      End If
      If TypeOfConvertor = 2 Then
        Print #Nf, "� ������ ����������� �� ����������� �����"
      End If
      If TypeOfConvertor = 3 Then
        Print #Nf, "��� ���������� ���������� ��������������"
      End If
      Print #Nf,
      Print #Nf, "������������ ������"
      Print #Nf, "����� ���������� �������������� ������ NX1="; NX1
    For i = 1 To NC
      KS1 = KMP1(i)
      KS2 = KMM1(i)
      RP1 = Abs(AX1(KS1)) + Abs(CX1(KS1))
      RP2 = Abs(AX1(KS2)) + Abs(CX1(KS2))
      Print #Nf, " I="; i; CR(i); "/"; CQ(i); " Q="; CR(i) / CQ(i)
      If KS1 > 0 Then
        Print #Nf, "     +   m="; AX1(KS1); " n="; CX1(KS1)
       Else
        Print #Nf, "     +   m=   ---       n=      ---    "
      End If
      If KS2 > 0 Then
        Print #Nf, "     -   m="; AX1(KS2); " n="; CX1(KS2)
       Else
        Print #Nf, "     -   m=   ---       n=      ---    "
      End If
    Next i
      Print #Nf,
      Print #Nf, "��������� ������"
      Print #Nf, "����� ���������� �������������� ������ NX2="; NX2
     For i = 1 To NB
      KS1 = KMP2(i)
      KS2 = KMM2(i)
      RP1 = Abs(AX2(KS1)) + Abs(CX2(KS1))
      RP2 = Abs(AX2(KS2)) + Abs(CX2(KS2))
      Print #Nf, " I="; i; BR(i); "/"; BQ(i); " Q="; BR(i) / BQ(i)
      If KS1 > 0 Then
        Print #Nf, "     +   m="; AX2(KS1); " n="; CX2(KS1)
       Else
        Print #Nf, "     +   m=   ---       n=      ---    "
      End If
      If KS2 > 0 Then
        Print #Nf, "     -   m="; AX2(KS2); " n="; CX2(KS2)
       Else
        Print #Nf, "     -   m=   ---       n=      ---    "
      End If
     Next i
      Print #Nf,
      Print #Nf,
   Close #Nf

End Sub

Public Sub CombinFarey(Kp As Integer, M As Integer, S As Integer, _
                  Q As Double, AP() As Integer, CP() As Integer)
'
'     ��������� Combin ������������� ��� ���������� �������������
'     ��������� ������� ��������� �������������� ������ � �����������
'     ����������� ������ Q, ��� ������������ ������
'     M=2,IS=2; � ��� ��������� ������ M=1,IS=2; M=2,IS=1
'
'     ������� ���������:
'       KP    - ���������� ������� �������������� ������;
'       M,IS  - �������������� ����� �������������� �������;
'       Q     - ����������� ����������� ������ (������� � �������);
'
'     �������� ���������:
'       A(4) - ������ �����., ���������� ��������������� ���������;
'       AP(4) - ������ �����. ������� � ��������� ����. ������ FK=AP*Q+CP
'       CP(4) - ������ ��������� ������ � ��-��� ����. ������ FK=AP*Q+CP
'
'     ���������� ��������� �/���������:
'       R    - ������������� ����������� ����������� ������;
'       NC   - ���-�� �����. ����������� ������, �����. ����. ��� ������������
'       NB   - ���-�� �����. ����������� ������, �����. ����. ��� ���������
'       A1, A2 - ������������� ����������
' �������:
'      K(4)   - ������ ������� �����., ���������� ������. ���������
'      C,B    - ������ �����. ������. ������, �����. ������. ���������
'      AX1,CX1 - ������� �����. ��-��� ������. ������ ��� ������������
'      AX2,CX2 - - ������� �����. ��-��� ������. ������ ��� ���������
'     KMP1,KMM1,KMP2,KMM2 - �������, � ������� �������� ������ ��-���
'          ������. ������ � ��������������(P) � �������������� (M) ���������
'

Dim r As Double
Dim NA As Integer
Dim k(4) As Integer
Dim A(4) As Double
Dim A1 As Double
Dim A2 As Double
Dim j As Integer
'
'  ���������� ��������� ����������� ����������� ������, ����������
'  ��������������� ��������� ������������� �������
If Q > 1 Then r = 1 / Q
If Q <= 1 Then r = Q
If M + S = 3 Then NA = NB
If M + S = 4 Then NA = NC
For j = 2 To NA
  If M + S = 3 Then
    A1 = BR(j - 1) / BQ(j - 1)
    A2 = BR(j) / BQ(j)
   Else
    A1 = CR(j - 1) / CQ(j - 1)
    A2 = CR(j) / CQ(j)
  End If
  If (r >= A1 And r <= A2) Then
    A(1) = A1
    A(4) = A1
    A(2) = A2
    A(3) = A2
    k(1) = j - 1
    k(4) = j - 1
    k(2) = j
    k(3) = j
    Exit For
  End If
Next j
'
'  �������� ������ ��������� �������������� �������,
'  ���������� �����. ��������� �������������� ������
'
If M + S = 4 Then
'    ������������ ������
100
 If KMP1(k(1)) <> 0 Then
   A(1) = CR(k(1)) / CQ(k(1))
   AP(1) = AX1(KMP1(k(1)))
   CP(1) = CX1(KMP1(k(1)))
  Else
'   ���� �������� ���������� �����
    k(1) = k(1) - 1
    If k(1) > 0 Then GoTo 100
      AP(1) = 0
      CP(1) = 2
      A(1) = 0#
 End If
120
 If KMM1(k(2)) <> 0 Then
   A(2) = CR(k(2)) / CQ(k(2))
   AP(2) = AX1(KMM1(k(2)))
   CP(2) = CX1(KMM1(k(2)))
  Else
   k(2) = k(2) + 1
   If k(2) <= NC Then GoTo 120
     AP(2) = 0
     CP(2) = 2
     A(2) = 1#
  End If
140
 If KMP1(k(3)) <> 0 Then
   A(3) = CR(k(3)) / CQ(k(3))
   AP(3) = AX1(KMP1(k(3)))
   CP(3) = CX1(KMP1(k(3)))
  Else
   k(3) = k(3) + 1
   If k(3) <= NC Then GoTo 140
      AP(3) = 2
      CP(3) = 0
      A(3) = 1#
 End If
160
 If KMM1(k(4)) <> 0 Then
   A(4) = CR(k(4)) / CQ(k(4))
   AP(4) = AX1(KMM1(k(4)))
   CP(4) = CX1(KMM1(k(4)))
  Else
   k(4) = k(4) - 1
   If k(4) > 0 Then GoTo 160
      AP(4) = 0
      CP(4) = 1
      A(4) = 0#
 End If
Else
'    ��������� ������
200
 If KMP2(k(1)) <> 0 Then
   A(1) = BR(k(1)) / BQ(k(1))
   AP(1) = AX2(KMP2(k(1)))
   CP(1) = CX2(KMP2(k(1)))
  Else
   k(1) = k(1) - 1
   If k(1) > 0 Then GoTo 200
      AP(1) = 0
      CP(1) = 1
      A(1) = 0#
 End If
220
 If KMM2(k(2)) <> 0 Then
   A(2) = BR(k(2)) / BQ(k(2))
   AP(2) = AX2(KMM2(k(2)))
   CP(2) = CX2(KMM2(k(2)))
  Else
   k(2) = k(2) + 1
   If k(2) <= NB Then GoTo 220
      AP(2) = 0
      CP(2) = 1
      A(2) = 1#
 End If
240
 If KMP2(k(3)) <> 0 Then
   A(3) = BR(k(3)) / BQ(k(3))
   AP(3) = AX2(KMP2(k(3)))
   CP(3) = CX2(KMP2(k(3)))
  Else
   k(3) = k(3) + 1
   If k(3) <= NB Then GoTo 240
      AP(3) = 0
      CP(3) = 0
      A(3) = 1#
 End If
260
 If KMM2(k(4)) <> 0 Then
   A(4) = BR(k(4)) / BQ(k(4))
   AP(4) = AX2(KMM2(k(4)))
   CP(4) = CX2(KMM2(k(4)))
  Else
   k(4) = k(4) - 1
   If k(4) > 0 Then GoTo 260
      AP(4) = 0
      CP(4) = 0
      A(4) = 0#
 End If
End If
End Sub

Public Sub FileOutCombinFarey(Kp As Integer, M As Integer, S As Integer, _
                  Q As Double, AP() As Integer, CP() As Integer)

'    ������ ���������� �� �/� Combin � ����

Dim Nf As Integer
   Nf = FreeFile
   Open App.Path + "\" + FileOut For Append As Nf
   Print #Nf, "������� ������ �/� CombinFarey: "
   Print #Nf, " KP="; Kp; ", M="; M; ", IS="; S; ", Q="; Q
   Print #Nf, "�������� ������:"
   Print #Nf, " Fc1="; AP(1); "*q+"; CP(1); ",  Fc2="; AP(2); "*q+"; CP(2)
   Print #Nf, " Fc3="; AP(3); "*q+"; CP(3); ",  Fc4="; AP(4); "*q+"; CP(4)
   Print #Nf,
   Close #Nf
End Sub

Public Sub SintezAllCombins(Kp As Integer, TypeOfNonLinearity As Integer)
'   ��������� ������� �������������� ������, ���������� ����� ���������� �����
'     ���������� ������ �� ���� ������������ ��� �������� ����� ���������� �����
'   ��������� ��� ������� ������� ����������� � �����������
Dim i As Integer
Dim j As Integer
Dim N As Integer    ' ������ �������� ���������� �����
Dim YesComb As Boolean  ' �������� �� ����� ��� ���� ������
Dim MPlus As Integer  ' ����������� �������������� ������� � ������������� �����������
Dim NPlus As Integer  ' ��������� ���� �������������� �������
Dim MMinus As Integer  ' ����������� �������������� ������� � ������������� �����������
Dim NMinus As Integer  ' ��������� ���� �������������� �������
Dim YesPlus As Boolean  ' �������� �� ����. ������� � ������������� �����������
Dim YesMinus As Boolean ' �������� �� ����. ������� � ������������� �����������
Dim k As Integer  ' ����������� ���������� ����. ������ ����� �������. �����
Dim Kmax As Integer  '  ���������� ���� �������� ����. ������ � ���. �����
Dim KmaxPlus As Integer  '  ���������� ���� �������� ����. ������ � ���. �����
                         ' � ������������� �����������
Dim KmaxMinus As Integer  '  ���������� ���� �������� ����. ������ � ���. �����
                         ' � ������������� �����������
Dim KolPlusBase As Integer '  ���������� �������� �������������� ������ ��� ������������
Dim KolPlusAll As Integer '  ����� ����� �������������� ������ ��� ������������
Dim KolMinusBase As Integer '  ���������� �������� �������������� ������ ��� ���������
Dim KolMinusAll As Integer '  ����� ����� �������������� ������ ��� ���������
   Dim Nf As Integer
   Nf = FreeFile
   Open App.Path + "\" + FileOutAllCombins For Append As Nf
   Print #Nf, "������� ������ �/� SintezAllCombins: "
   Print #Nf, "���������� �������������� ������ ��� Kp="; Kp
  
  ' �������� ������ �������� �������������� ������
If TypeOfNonLinearity = 1 Then
'  ������ ������������������ ���������� ����� ��� ������������ ������
'   �������� ���������� �������������� ������
        
  Print #Nf, "������������� �������� ���������� �������������� ������"
  Print #Nf, "��� ������������ ������"
  Print #Nf, "����� ���������� ���������� ����� NC="; NC
  
  KolPlusBase = 0
  KolPlusAll = 0
  For i = 1 To NC
    Print #Nf, " I="; i; CR(i); "/"; CQ(i); " Q="; CR(i) / CQ(i)
' ����������� ������������� ���������� ����. ������ ���������� ����� ���������� �����
    KmaxPlus = Int((Kp - 1) / (CR(i) + CQ(i)))
    KmaxMinus = KmaxPlus
    If CR(i) = 0 And CQ(i) = 1 Then
'  ������ ��� ������������� �����������
       KmaxPlus = KmaxPlus - 2
'   ������ ��� ������������� �����������
       KmaxMinus = KmaxMinus - 2
    End If
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * CQ(i) + 1
       NPlus = -k * CR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * CQ(i) + 1
       NMinus = k * CR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
'  ������������� �������� ���������� �������������� ������
       If Abs(MPlus) + Abs(NPlus) < Kp Then
         YesPlus = True
         If k = 1 Then
            KolPlusBase = KolPlusBase + 1
            KolPlusAll = KolPlusAll + 1
          Else
            KolPlusAll = KolPlusAll + 1
         End If
       End If
       If Abs(MMinus) + Abs(NMinus) < Kp Then
         YesMinus = True
         If k = 1 Then
            KolPlusBase = KolPlusBase + 1
            KolPlusAll = KolPlusAll + 1
          Else
            KolPlusAll = KolPlusAll + 1
         End If
       End If
'  ���������� �������������� ������ ���������� ����� ���������� �����
      If YesPlus Then
        Print #Nf, "     +   m="; MPlus; " n="; NPlus; " k="; k
       Else
        Print #Nf, "     +   m=  ---   n=  ---  "; " k="; k
      End If
      If YesMinus Then
        Print #Nf, "     -   m="; MMinus; " n="; NMinus
       Else
        Print #Nf, "     -   m=  ---   n=  ---  "
      End If
     Next k
  Next i
  Print #Nf, " ���-�� �������� ����. ������ KolPlusBase="; KolPlusBase
  Print #Nf, " ����� ���������� ����. ������ KolPlusAll="; KolPlusAll
  Print #Nf,
'  ������ ������������������ ���������� ����� ��� ��������� ������
'   �������� ���������� �������������� ������
      
  Print #Nf, "��� ��������� ������"
  Print #Nf, "����� ���������� ���������� ����� NB="; NB
         
  KolMinusBase = 0
  KolMinusAll = 0
  For i = 1 To NB
    Print #Nf, " I="; i; BR(i); "/"; BQ(i); " Q="; BR(i) / BQ(i)
'  ����������� ������������� ���������� ����. ������ ���������� ����� ���������� �����
    KmaxPlus = Int((Kp + 1) / (BR(i) + BQ(i)))
    KmaxMinus = Int((Kp - 3) / (BR(i) + BQ(i)))
    If BR(i) = 0 And BQ(i) = 1 Then
'  ������ ��� ������������� �����������
       KmaxPlus = KmaxPlus - 2
    End If
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * BQ(i) - 1
       NPlus = -k * BR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * BQ(i) - 1
       NMinus = k * BR(i) + 1
'  �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
'  ������������� �������� ���������� �������������� ������
       If Abs(MPlus) + Abs(NPlus) < Kp Then
         YesPlus = True
         If k = 1 Then
            KolMinusBase = KolMinusBase + 1
            KolMinusAll = KolMinusAll + 1
          Else
            KolMinusAll = KolMinusAll + 1
         End If
       End If
       If Abs(MMinus) + Abs(NMinus) < Kp Then
         YesMinus = True
         If k = 1 Then
            KolMinusBase = KolMinusBase + 1
            KolMinusAll = KolMinusAll + 1
          Else
            KolMinusAll = KolMinusAll + 1
         End If
       End If
'  ���������� �������������� ������ ���������� ����� ���������� �����
      If YesPlus Then
        Print #Nf, "     +   m="; MPlus; " n="; NPlus; " k="; k
       Else
        Print #Nf, "     +   m=  ---  n=  ---  "; " k="; k
      End If
      If YesMinus Then
        Print #Nf, "     -   m="; MMinus; " n="; NMinus
       Else
        Print #Nf, "     -   m=  ---  n=  ---  "
      End If
     Next k
  Next i
  Print #Nf, " ���-�� �������� ����. ������ KolMinusBase="; KolMinusBase
  Print #Nf, " ����� ���������� ����. ������ KolMinusAll="; KolMinusAll
  Print #Nf,
 
 Else
'  ������ ������������������ ���������� ����� ��� ��������� ������
'   ������ ���������� �������������� ������
  
  Print #Nf, "������������� ������ ���������� �������������� ������"
'  ������ ������������������ ���������� ����� ��� ������������ ������
'   ������ ���������� �������������� ������
  
  Print #Nf, "��� ������������ ������"
  Print #Nf, "����� ���������� ���������� ����� NC="; NC
  
  KolPlusBase = 0
  KolPlusAll = 0
  For i = 1 To NC
    Print #Nf, " I="; i; CR(i); "/"; CQ(i); " Q="; CR(i) / CQ(i)
' ����������� ������������� ���������� ����. ������ ���������� ����� ���������� �����
'  ������ ��� ������������� �����������
       KmaxPlus = Int((Kp - 2) / (CQ(i)))
'   ������ ��� ������������� �����������
       KmaxMinus = Int(Kp / (CQ(i)))
    If CR(i) = 1 And CQ(i) = 1 Then
       KmaxMinus = KmaxMinus - 2
    End If
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * CQ(i) + 1
       NPlus = -k * CR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * CQ(i) + 1
       NMinus = k * CR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
'  ������������� ������ ���������� �������������� ������
       If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
         YesPlus = True
         If k = 1 Then
            KolPlusBase = KolPlusBase + 1
            KolPlusAll = KolPlusAll + 1
          Else
            KolPlusAll = KolPlusAll + 1
         End If
       End If
       If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
         YesMinus = True
         If k = 1 Then
            KolPlusBase = KolPlusBase + 1
            KolPlusAll = KolPlusAll + 1
          Else
            KolPlusAll = KolPlusAll + 1
         End If
       End If
'  ���������� �������������� ������ ���������� ����� ���������� �����
      If YesPlus Then
        Print #Nf, "     +   m="; MPlus; " n="; NPlus; " k="; k
       Else
        Print #Nf, "     +   m=  ---  n=  ---  "; " k="; k
      End If
      If YesMinus Then
        Print #Nf, "     -   m="; MMinus; " n="; NMinus
       Else
        Print #Nf, "     -   m=  ---  n=  ---  "
      End If
     Next k
  Next i
  Print #Nf, " ���-�� �������� ����. ������ KolPlusBase="; KolPlusBase
  Print #Nf, " ����� ���������� ����. ������ KolPlusAll="; KolPlusAll
  Print #Nf,
  
  Print #Nf, "��� ��������� ������"
  Print #Nf, "����� ���������� ���������� ����� NB="; NB

  KolMinusBase = 0
  KolMinusAll = 0
  For i = 1 To NB
    Print #Nf, " I="; i; BR(i); "/"; BQ(i); " Q="; BR(i) / BQ(i)
' ����������� ������������� ���������� ����. ������ ���������� ����� ���������� �����
'  ������ ��� ������������� �����������
       KmaxPlus = Int(Kp / (BQ(i)))
'  ������ ��� ������������� �����������
       KmaxMinus = Int((Kp - 2) / (BQ(i)))
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
       MPlus = k * BQ(i) - 1
       NPlus = -k * BR(i) + 1
'  ���������� ������������� �������������� ������ � ������������� �����������
       MMinus = -k * BQ(i) - 1
       NMinus = k * BR(i) + 1
'   �������� ������������� �� ������ ����. ������� ������� ��������
       YesPlus = False
       YesMinus = False
'  ������������� ������ ���������� �������������� ������
       If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
         YesPlus = True
         If k = 1 Then
            KolMinusBase = KolMinusBase + 1
            KolMinusAll = KolMinusAll + 1
          Else
            KolMinusAll = KolMinusAll + 1
         End If
       End If
       If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
         YesMinus = True
         If k = 1 Then
            KolMinusBase = KolMinusBase + 1
            KolMinusAll = KolMinusAll + 1
          Else
            KolMinusAll = KolMinusAll + 1
         End If
       End If
'  ���������� �������������� ������ ���������� ����� ���������� �����
      If YesPlus Then
        Print #Nf, "     +   m="; MPlus; " n="; NPlus; " k="; k
       Else
        Print #Nf, "     +   m=  ---  n=  ---  "; " k="; k
      End If
      If YesMinus Then
        Print #Nf, "     -   m="; MMinus; " n="; NMinus
       Else
        Print #Nf, "     -   m=  ---  n=  ---  "
      End If
     Next k
  Next i
  Print #Nf, " ���-�� �������� ����. ������ KolMinusBase="; KolMinusBase
  Print #Nf, " ����� ���������� ����. ������ KolMinusAll="; KolMinusAll
  Print #Nf,
End If
Print #Nf,
Close #Nf
End Sub

