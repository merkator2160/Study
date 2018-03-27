Attribute VB_Name = "PictureNomogramma"
Option Explicit
'   ������ ������������ ��� ��������� ������� �������������
'     �������������� ������ ��� ���������� ����������

'   ������ �������������� ������
Const MaxLines = 4800  '  ������������ ����� �������������� ������
Public ALine(MaxLines) As Integer ' �����. �������
Public CLine(MaxLines) As Integer ' ��������� ���� ��-��� ����. �������
Public NLines As Integer  ' ����� ����� �������������� ������

Public Sub SintezLines()
'  ��������� ������� ������� ������ ���������� ����������
Dim KS1 As Integer
Dim KS2 As Integer
Dim i As Integer
Dim j As Integer
Dim k As Integer
Dim A As Integer
Dim C As Integer
'  ���������� ��� ����. ������ � ���� ������
  NLines = 0
  For i = 1 To NC
   KS1 = KMP1(i)
   KS2 = KMM1(i)
   If KS1 > 0 Then
    NLines = NLines + 1
    ALine(NLines) = AX1(KS1)
    CLine(NLines) = CX1(KS1)
   End If
   If KS2 > 0 Then
    NLines = NLines + 1
    ALine(NLines) = AX1(KS2)
    CLine(NLines) = CX1(KS2)
   End If
  Next i
  For i = 1 To NB
   KS1 = KMP2(i)
   KS2 = KMM2(i)
   If KS1 > 0 Then
    NLines = NLines + 1
    ALine(NLines) = AX2(KS1)
    CLine(NLines) = CX2(KS1)
   End If
   If KS2 > 0 Then
    NLines = NLines + 1
    ALine(NLines) = AX2(KS2)
    CLine(NLines) = CX2(KS2)
   End If
  Next i
'   ������ �������������� �������������� ������ ����������
'     ����� ���������� ����� � �������� ������� Kp
   If AllCombin Then
      SintezRestCombin Kp, TN, TC
   End If
'   ���������� ������������ ������� ����. ������
  For i = 1 To NLines - 1
   A = ALine(i)
   C = CLine(i)
   For j = i + 1 To NLines
    If A = ALine(j) And C = CLine(j) Then
' ���������� ��� ���������� ����. ������
      For k = j + 1 To NLines
       ALine(k - 1) = ALine(k)
       CLine(k - 1) = CLine(k)
      Next k
      NLines = NLines - 1
    End If
   Next j
  Next i
End Sub

Public Sub SintezRestCombin(Kp As Integer, _
                      TypeOfNonLinearity As Integer, TypeOfConvertor As Integer)
'  ��������� ������� �������������� �������������� ������������
'   ���������� ����� ���������� ����� ��� �������� ����� ���������� �����
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
  ' �������� ������ �������� �������������� ������
If TypeOfNonLinearity = 1 Then
'  ������ ������������������ ���������� ����� ��� ������������ ������
'   �������� ���������� �������������� ������
  For i = 1 To NC
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
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
       If YesPlus Then
          NLines = NLines + 1
          ALine(NLines) = MPlus
          CLine(NLines) = NPlus
       End If
       If YesMinus Then
          NLines = NLines + 1
          ALine(NLines) = MMinus
          CLine(NLines) = NMinus
       End If
     Next k
  Next i
'  ������ ������������������ ���������� ����� ��� ��������� ������
'   �������� ���������� �������������� ������
  For i = 1 To NB
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
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
       If YesPlus Then
          NLines = NLines + 1
          ALine(NLines) = MPlus
          CLine(NLines) = NPlus
       End If
       If YesMinus Then
          NLines = NLines + 1
          ALine(NLines) = MMinus
          CLine(NLines) = NMinus
       End If
     Next k
  Next i
 Else
'  ������ ������������������ ���������� ����� ��� ��������� ������
'   ������ ���������� �������������� ������
  For i = 1 To NB
'  ��� ������ ���������� �����
'  ���������� ������������� �������������� ������ � ������������� �����������
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
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
       If YesPlus Then
          NLines = NLines + 1
          ALine(NLines) = MPlus
          CLine(NLines) = NPlus
       End If
       If YesMinus Then
          NLines = NLines + 1
          ALine(NLines) = MMinus
          CLine(NLines) = NMinus
       End If
     Next k
  Next i
'  ������ ������������������ ���������� ����� ��� ������������ ������
'   ������ ���������� �������������� ������
  For i = 1 To NC
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
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
       If YesPlus Then
          NLines = NLines + 1
          ALine(NLines) = MPlus
          CLine(NLines) = NPlus
       End If
       If YesMinus Then
          NLines = NLines + 1
          ALine(NLines) = MMinus
          CLine(NLines) = NMinus
       End If
     Next k
  Next i
End If
End Sub
