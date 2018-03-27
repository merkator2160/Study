VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmNomogramma 
   AutoRedraw      =   -1  'True
   BackColor       =   &H80000009&
   Caption         =   "���������� �������������� ������"
   ClientHeight    =   6195
   ClientLeft      =   165
   ClientTop       =   735
   ClientWidth     =   5370
   LinkTopic       =   "Form1"
   ScaleHeight     =   6195
   ScaleWidth      =   5370
   StartUpPosition =   3  'Windows Default
   Begin VB.PictureBox picNomogramma 
      Appearance      =   0  'Flat
      AutoRedraw      =   -1  'True
      BackColor       =   &H80000005&
      ForeColor       =   &H80000008&
      Height          =   1455
      Left            =   3360
      ScaleHeight     =   1425
      ScaleWidth      =   1305
      TabIndex        =   1
      Top             =   840
      Width           =   1335
      Begin VB.Shape cirRight 
         BackColor       =   &H000000FF&
         BackStyle       =   1  'Opaque
         Height          =   75
         Left            =   600
         Shape           =   3  'Circle
         Top             =   960
         Visible         =   0   'False
         Width           =   75
      End
      Begin VB.Shape cirLeft 
         BackColor       =   &H000000FF&
         BackStyle       =   1  'Opaque
         Height          =   75
         Left            =   240
         Shape           =   3  'Circle
         Top             =   960
         Visible         =   0   'False
         Width           =   75
      End
      Begin VB.Line LineConvert 
         BorderColor     =   &H000000FF&
         BorderWidth     =   2
         Visible         =   0   'False
         X1              =   240
         X2              =   240
         Y1              =   240
         Y2              =   720
      End
      Begin VB.Line Line1 
         BorderStyle     =   3  'Dot
         Visible         =   0   'False
         X1              =   240
         X2              =   720
         Y1              =   1200
         Y2              =   1200
      End
      Begin VB.Shape Shape1 
         BorderStyle     =   3  'Dot
         Height          =   495
         Left            =   600
         Top             =   360
         Visible         =   0   'False
         Width           =   375
      End
   End
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   0
      Top             =   5820
      Width           =   5370
      _ExtentX        =   9472
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   4
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            AutoSize        =   2
            Bevel           =   2
            Object.Width           =   397
            MinWidth        =   388
            Text            =   "q"
            TextSave        =   "q"
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1270
            MinWidth        =   1270
            Key             =   "X"
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   2
            Object.Width           =   1058
            MinWidth        =   1058
            Text            =   "q ���"
            TextSave        =   "q ���"
         EndProperty
         BeginProperty Panel4 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1270
            MinWidth        =   1270
            Key             =   "Y"
         EndProperty
      EndProperty
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   4440
      Top             =   5160
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&File"
      Begin VB.Menu mnuSave 
         Caption         =   "&Save as ..."
      End
      Begin VB.Menu mnuOptions 
         Caption         =   "&Options"
         Begin VB.Menu mnuKP 
            Caption         =   "Kp"
         End
         Begin VB.Menu mnuEmpty2 
            Caption         =   "-"
         End
         Begin VB.Menu mnuFull 
            Caption         =   "Full"
         End
         Begin VB.Menu mnuHalf 
            Caption         =   "Half"
            Checked         =   -1  'True
         End
         Begin VB.Menu mnuEmpty3 
            Caption         =   "-"
         End
         Begin VB.Menu mnuSimple 
            Caption         =   "Simple"
            Checked         =   -1  'True
         End
         Begin VB.Menu mnuBalanceGeterodin 
            Caption         =   "Balance Geterodin"
         End
         Begin VB.Menu mnuBalanceSignal 
            Caption         =   "Balance Signal"
         End
         Begin VB.Menu mnuDoubleBalance 
            Caption         =   "Double Balance"
         End
         Begin VB.Menu mnuEmpty4 
            Caption         =   "-"
         End
         Begin VB.Menu mnuAllCombin 
            Caption         =   "All Combinations"
         End
      End
      Begin VB.Menu mnuEmpty1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "Exit"
      End
   End
   Begin VB.Menu mnuDraw 
      Caption         =   "&Draw"
   End
   Begin VB.Menu mnuZoomPlus 
      Caption         =   "Zoom +"
   End
   Begin VB.Menu mnuZoomMinus 
      Caption         =   "Zoom -"
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
      Begin VB.Menu mnuHelp1 
         Caption         =   "Help ..."
      End
      Begin VB.Menu mnuEmpty5 
         Caption         =   "-"
      End
      Begin VB.Menu mnuAbout 
         Caption         =   "About ..."
      End
   End
End
Attribute VB_Name = "frmNomogramma"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim DH As Integer
Dim DW As Integer
Dim XNt As Double ' ��������� ����� ��� ������������ ������� ����. �������
Dim YNt As Double
Dim XKt As Double ' �������� ����� ��� ������������ ������� ����. �������
Dim YKt As Double
Dim Text As String ' ����� ������� ����� ����. �������
Dim WT As Double '  ������� ����� W �� ����������� �
Dim HT As Double '    ���������
Dim SBH As Double '   ������ StatusBar
Dim Xt As Double  '  ��������� ����� ��� �������� ������ ��������� �������������� ������������
Dim Yt As Double  '    ��� ������� ����� ����� ������� ������������
Dim Xk As Double  '  �������� ����� ��� �������� ������ ��������� �������������� ������������
Dim Yk As Double  '    ��� ������� ����� ����� ������� ������������
Dim Zoom As Boolean ' ������������ �� ������������ ����������

Private Sub Form_Load()
'  ���������� �������� ����� ��������� ����� � �������� ����������
   DW = frmNomogramma.Width - frmNomogramma.ScaleWidth
   DH = frmNomogramma.Height - frmNomogramma.ScaleHeight
   Kp = 5
   frmNomogramma.Caption = "���������� �������������� ������ Kp=" + Str(Kp)
   mnuHalf.Checked = True
   mnuFull.Checked = False
   TN = 1
   mnuSimple.Checked = True
   mnuBalanceGeterodin.Checked = False
   mnuBalanceSignal.Checked = False
   mnuDoubleBalance.Checked = False
   TC = 0
   mnuAllCombin.Checked = False
   AllCombin = False
  '  ���������� �������� ������ � ������ ������ ��� ����������
  '     ������� ���������� �������
   WT = frmNomogramma.TextWidth("W")
   HT = Abs(frmNomogramma.TextHeight("W"))  ' �.�. �� ��������� ��� �����������
  ' ��������� �������������� ��������� ������ ����� ���������� ����������
  '  ��� ������������� ��������� ���. ���������
  ' frmNomogramma.Scale (-5 * WT, 2 + 2 * HT)-(1 + 5 * WT, -4 * HT)
  
  '  ��������� ������� ������� ���������� �������� picNomogramma
  picNomogramma.Left = 5 * WT
  picNomogramma.Top = 2 * HT
   '  ����������� ������ StatusBar
  SBH = StatusBar1.Height
  picNomogramma.Width = frmNomogramma.ScaleWidth - 10 * WT
  picNomogramma.Height = frmNomogramma.ScaleHeight - 4 * HT - SBH
 
  '  ��������� ��������� ���. ��������� ������� ��������� �������
  '    �� �������� �������� picNomogramma
  SetScaleBegin
  DrawCaption
  DrawGrid

   
End Sub

Private Sub Form_Resize()
'  �������� ������������ ������� ������� ��������� �������
'   If (frmNomogramma.Height - DH) / 2 <> _
'       frmNomogramma.Width - DW Then
'      frmNomogramma.Height = (frmNomogramma.Width - DW) * 2 + DH
'   End If
  '  ��������� ������� ������� ���������� �������� picNomogramma
  picNomogramma.Left = 5 * WT
  picNomogramma.Top = 2 * HT
   '  ����������� ������ StatusBar
  SBH = StatusBar1.Height
  picNomogramma.Width = frmNomogramma.ScaleWidth - 10 * WT
  picNomogramma.Height = frmNomogramma.ScaleHeight - 4 * HT - SBH
' ������� ��������� ������� ���������� ���������� ���������������
  LineConvert.Visible = False
  cirLeft.Visible = False
  cirRight.Visible = False
'  frmNomogramma.Cls
'  picNomogramma.Cls
'  SetScale
'  DrawGrid
'  DrawCaption
 End Sub

Private Sub mnuAllCombin_Click()
  If mnuAllCombin.Checked = False Then
     mnuAllCombin.Checked = True
     AllCombin = True
   Else
     mnuAllCombin.Checked = False
     AllCombin = False
  End If
End Sub


Private Sub mnuZoomMinus_Click()
  picNomogramma.Cls
  SetScaleBegin
  Zoom = False
End Sub

Private Sub mnuZoomPlus_Click()
  Zoom = True
End Sub

Private Sub picNomogramma_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
 '  ��������� ������ ��������� �������������� ������������
     If Button = vbLeftButton And Zoom = False Then
       Qnom = X
       If Y < 1 Then
        ' ��� ��������� ������
         M = 1
         S = 2
        Else
        ' ��� ������������ ������
         M = 2
         S = 2
       End If
       
     End If
     If Button = vbLeftButton And Zoom = True Then
       Shape1.Visible = True
       Xt = X
       Yt = Y
       Shape1.Left = Xt
       Shape1.Top = Yt
       Shape1.Width = 0
       Shape1.Height = 0
     End If
'  ��������� ���������� ��������� ����� ��� ������������ ������� �������������� �������
   If Button = vbRightButton Then
     XNt = X
     YNt = Y
     Line1.X1 = XNt
     Line1.Y1 = YNt
     Line1.X2 = XNt
     Line1.Y2 = YNt
     Line1.Visible = True
   End If
End Sub

Private Sub picNomogramma_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
 '   ��������� ��������� �������������� ������������
     If Button = vbLeftButton And Zoom = True Then
       Shape1.Visible = True
       If X - Shape1.Left < 0 Then
          Shape1.Left = X
       End If
       Shape1.Width = Abs(X - Xt)
       If Shape1.Top - Y < 0 Then
          Shape1.Top = Y
       End If
       Shape1.Height = Abs(Y - Yt)
     End If
'  ������������ ����������� ����� ��� ������� ������ ������
 If X >= 0 And X <= 1 And Y >= 0 And Y <= 2 Then
    StatusBar1.Panels.Item(2) = Format(X, "0.0000")
    StatusBar1.Panels.Item(4) = Format(Y, "0.0000")
 End If
 If Button = vbRightButton Then
     Line1.X2 = X
     Line1.Y2 = Y
 End If
End Sub

Private Sub picNomogramma_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
 If Button = vbRightButton Then
   XKt = X
   YKt = Y
     '  ����������� ��������� �������������� ������� � XNt � YNt
    Dim Rmin As Double
    Dim r As Double
    Dim i As Integer
    Dim it As Integer
    Rmin = 1
    For i = 1 To NLines
      r = Abs((ALine(i) * XNt - YNt + CLine(i)) / Sqr(ALine(i) ^ 2 + 1))
      If r < Rmin Then
       Rmin = r
       it = i
      End If
    Next i
    ' �������� ����������� ����������
    If Rmin < 0.01 Then
      If CLine(it) < 0 Then
         Text = Str(ALine(it)) + "q" + Str(CLine(it))
       Else
         Text = Str(ALine(it)) + "q" + "+" + Trim(Str(CLine(it)))
      End If
      picNomogramma.Line (XNt, YNt)-(XKt, YKt), QBColor(6)
      picNomogramma.CurrentY = picNomogramma.CurrentY - picNomogramma.TextHeight("q") / 2
      picNomogramma.Print Text
    End If
 End If
     Line1.Visible = False
 If Button = vbLeftButton And Zoom = True Then
   Xk = Xt + Shape1.Width
   Yk = Yt - Shape1.Height
 End If
End Sub

Private Sub mnuAbout_Click()
   frmAbout.Show 1
End Sub

Private Sub mnuBalanceGeterodin_Click()
   mnuSimple.Checked = False
   mnuBalanceGeterodin.Checked = True
   mnuBalanceSignal.Checked = False
   mnuDoubleBalance.Checked = False
   TC = 1
End Sub

Private Sub mnuBalanceSignal_Click()
   mnuSimple.Checked = False
   mnuBalanceGeterodin.Checked = False
   mnuBalanceSignal.Checked = True
   mnuDoubleBalance.Checked = False
   TC = 2
End Sub

Private Sub mnuDoubleBalance_Click()
   mnuSimple.Checked = False
   mnuBalanceGeterodin.Checked = False
   mnuBalanceSignal.Checked = False
   mnuDoubleBalance.Checked = True
   TC = 3
End Sub

Private Sub mnuDraw_Click()
'  ������ ���������� �������������� ������
   
   frmNomogramma.Cls
   picNomogramma.Cls
   FareySeries Kp
   FileOutFareySeries Kp
   SintezPorT Kp, TN, TC
   FileOutSintezPorT Kp, TN, TC
   CalcNomogramma Kp, TN, TC
   FileOutNomogramma Kp, TN, TC
'  �������
Dim Q As Double
Dim AP(4) As Integer
Dim CP(4) As Integer
   For Q = 0.1 To 0.9 Step 0.09
     CombinFarey Kp, 2, 2, Q, AP, CP
     FileOutCombinFarey Kp, 2, 2, Q, AP, CP
   Next Q
   For Q = 0.1 To 0.9 Step 0.09
     CombinFarey Kp, 1, 2, Q, AP, CP
     FileOutCombinFarey Kp, 1, 2, Q, AP, CP
   Next Q
   SintezAllCombins Kp, TN
'
   SintezLines
   SetScale
   DrawNomogramma
End Sub

Public Sub DrawNomogramma()
'  ��������� ���������� ���������� �������������� ������
'  ��������� ����� ������� ���������� �������
    DrawGrid
    DrawCaption
'    DrawCombin
    DrawLines
'  ��������� ������� ���������� �������
    picNomogramma.Line (0, 0)-(1, 0), QBColor(7) ' �������������� ������
    picNomogramma.Line (1, 0)-(1, 2), QBColor(7)
'  ���������� ������ ��������� ��������������
    picNomogramma.Line (0, 1)-(1, 0), QBColor(0) ' ���������
    picNomogramma.Line (0, 1)-(1, 2), QBColor(0) ' ������������
End Sub

Public Sub DrawCombin()
'  ��������� ���������� �������������� ������
Dim Xn As Double
Dim Xk As Double
Dim Yn As Double
Dim Yk As Double
Dim KS1 As Integer
Dim KS2 As Integer
Dim i As Integer
    Xn = 0
    Xk = 1
    For i = 1 To NC
      KS1 = KMP1(i)
      KS2 = KMM1(i)
      If KS1 > 0 Then
       Yn = AX1(KS1) * Xn + CX1(KS1)
       Yk = AX1(KS1) * Xk + CX1(KS1)
       picNomogramma.Line (Xn, Yn)-(Xk, Yk), QBColor(7)
      End If
      If KS2 > 0 Then
       Yn = AX1(KS2) * Xn + CX1(KS2)
       Yk = AX1(KS2) * Xk + CX1(KS2)
       picNomogramma.Line (Xn, Yn)-(Xk, Yk), QBColor(7)
      End If
     Next i
     For i = 1 To NB
      KS1 = KMP2(i)
      KS2 = KMM2(i)
      If KS1 > 0 Then
       Yn = AX2(KS1) * Xn + CX2(KS1)
       Yk = AX2(KS1) * Xk + CX2(KS1)
       picNomogramma.Line (Xn, Yn)-(Xk, Yk), QBColor(7)
      End If
      If KS2 > 0 Then
       Yn = AX2(KS2) * Xn + CX2(KS2)
       Yk = AX2(KS2) * Xk + CX2(KS2)
       picNomogramma.Line (Xn, Yn)-(Xk, Yk), QBColor(7)
      End If
     Next i
End Sub

Public Sub DrawLines()
'  ��������� ���������� �������������� ������
Dim Xn As Double
Dim Xk As Double
Dim Yn As Double
Dim Yk As Double
Dim i As Integer
    For i = 1 To NLines
       Xn = 0#
       Xk = 1#
       Yn = CDbl(ALine(i) * Xn + CLine(i))
       Yk = CDbl(ALine(i) * Xk + CLine(i))
       If Yn < 0# Then
         Yn = 0#
         Xn = CDbl(-CLine(i)) / CDbl(ALine(i))
       End If
       If Yn > 2# Then
         Yn = 2#
         Xn = CDbl(2# - CLine(i)) / CDbl(ALine(i))
       End If
       If Yk < 0# Then
         Yk = 0#
         Xk = CDbl(-CLine(i)) / CDbl(ALine(i))
       End If
       If Yk > 2# Then
         Yk = 2#
         Xk = CDbl(2# - CLine(i)) / CDbl(ALine(i))
       End If
'       Debug.Print i; " Xn="; Xn; " Yn="; Yn; " Xk="; Xk; " Yk="; Yk; " A="; ALine(i); " C="; CLine(i)
       picNomogramma.Line (Xn, Yn)-(Xk, Yk), QBColor(7)
    Next i
End Sub

Private Sub mnuExit_Click()
   frmNomogramma.Hide
End Sub

Private Sub mnuFull_Click()
   mnuFull.Checked = True
   mnuHalf.Checked = False
   TN = 0
End Sub

Private Sub mnuHalf_Click()
   mnuHalf.Checked = True
   mnuFull.Checked = False
   TN = 1
End Sub

Private Sub mnuHelp1_Click()
   frmHelp.Show 0
End Sub

Private Sub mnuKP_Click()
'   ���� ����������� ������� �������������� ������
    Kp = InputBox("������� ���������� ������� �������������� ������", "���� ������", Kp)
    frmNomogramma.Caption = "���������� �������������� ������ Kp=" + Str(Kp)
End Sub

Private Sub mnuSave_Click()
'  ���������� ������� � �����
  
' Load file of picture from HD
' Set CancelError is True
   CommonDialog1.CancelError = True
   On Error GoTo ErrHandler
' Set flags
   CommonDialog1.Flags = cdlOFNHideReadOnly
' Set filters
   CommonDialog1.Filter = "All Files (*.*)|*.*|Text Files" & _
   "(*.txt)|*.txt|Rezult Files (*.bmp)|*.bmp"
' Specify default filter
   CommonDialog1.FilterIndex = 3
' Display the Open dialog box
   CommonDialog1.ShowSave
' Display name of selected file
' Load the picture that you want to display.
    SavePicture frmNomogramma.Image, CommonDialog1.FileName
    SavePicture picNomogramma.Image, CommonDialog1.FileName + "1"
   Exit Sub

ErrHandler:
'User pressed the Cancel button
    MsgBox "�� ������ ��� ��������� �����", vbOKOnly, "�������� ������"
    Exit Sub
   
End Sub

Private Sub mnuSimple_Click()
   mnuSimple.Checked = True
   mnuBalanceGeterodin.Checked = False
   mnuBalanceSignal.Checked = False
   mnuDoubleBalance.Checked = False
   TC = 0
End Sub

Public Sub DrawCaption()
'  ��������� ��������� ��������� ������ ���������� ����. ������
Dim WTek As Double
Dim HTek As Double
WTek = picNomogramma.Width
HTek = picNomogramma.Height
Dim i As Integer
Dim jb As Integer
Dim je As Integer
Dim it As Single
'   ��������� �������� ���������� �� ���� ������� ������������ �����������
    frmNomogramma.CurrentY = picNomogramma.Top - 2 * HT
    frmNomogramma.CurrentX = frmNomogramma.Width / 2 - _
    frmNomogramma.TextWidth("���������� �������������� ������ ��� Kp=" + Str(Kp)) / 2
    frmNomogramma.Print "���������� �������������� ������ ��� Kp=" + Str(Kp)
'  ������� ��� X ��� ������ ������
    frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height + HT
    frmNomogramma.CurrentX = picNomogramma.Left + picNomogramma.Width + WT / 2
    frmNomogramma.Print "q=f1/f2"
'  ������� ��� Y ��� ������ ������
    frmNomogramma.CurrentY = picNomogramma.Top - HT * 1.75
    frmNomogramma.CurrentX = picNomogramma.Left - 4.75 * WT
    frmNomogramma.Print "q"
    frmNomogramma.CurrentY = picNomogramma.Top - HT * 1.3
    frmNomogramma.CurrentX = picNomogramma.Left - 4 * WT
    frmNomogramma.Print "���"

If Xt = 0 And Yt = 2 And Xk = 1 And Yk = 0 Then
'  ��������� ���� ��������� � ������ ������ ������� ���������� ����������
'  ��������� ����� X ��� ������ ������ ����� ������� ���������� ����������
   For i = 0 To 10
    frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height
    frmNomogramma.CurrentX = picNomogramma.Left + i * 0.1 * WTek - _
                             frmNomogramma.TextWidth(Trim(Str(i * 0.1))) / 2
    frmNomogramma.Print Trim(Str(i * 0.1))
   Next i
'  ������� � ��������� ��� Y ��� ������ ������ ����� ������� ���������� ����������
    ' ��������� ��� Y ����� ���������� 0.2
    For i = 1 To 10
     frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height - _
                             i * 2 * picNomogramma.Height / 20 - HT / 2
     frmNomogramma.CurrentX = picNomogramma.Left - frmNomogramma.TextWidth(Trim(Str(i * 0.2))) - WT / 2
     frmNomogramma.Print Trim(Str(i * 0.2))
    Next i
    frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height * 0.25 - HT / 2
    frmNomogramma.CurrentX = picNomogramma.Left - 3 * WT
    frmNomogramma.Print "1+q"
    frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height * 0.75 - HT / 2
    frmNomogramma.CurrentX = picNomogramma.Left - 3 * WT
    frmNomogramma.Print "1-q"
   Else
' ��������� �������� ������� ���������� ���������� �������������� ������
  '  ��������� ����� X ��� ������ �������� ����� ������� ���������� ����������
    ' ��������� ������� �����
      frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height + frmNomogramma.TextHeight("8") / 2
      frmNomogramma.CurrentX = picNomogramma.Left - frmNomogramma.TextWidth("8.888")
      frmNomogramma.Print Format$(Xt, "0.000")
      frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height
      frmNomogramma.CurrentX = picNomogramma.Left + picNomogramma.Width
                               ' - frmNomogramma.TextWidth("8.888")
      frmNomogramma.Print Format$(Xk, "0.000")
    ' ��������� ���������� ����� �� ��� X � ��������� 0.1
      ' ����������� �������� �������� ����� X
'       jk = Int((Xk - Xt) * 10)  '  ����� �����
       jb = Int(Xt * 10) + 1   ' ��������� �����
       je = Int(Xk * 10)    '  �������� �����
      ' ��������� X
       For i = jb To je
        it = i / 10
        frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height
        frmNomogramma.CurrentX = picNomogramma.Left + (it - Xt) / (Xk - Xt) * picNomogramma.Width - _
                                 frmNomogramma.TextWidth(Trim(Str(i * 0.1))) / 2
        frmNomogramma.Print Trim(Str(i * 0.1))
       Next i
  '  ��������� ����� Y ��� ������ �������� ����� ������� ���������� ����������
    ' ��������� ������� �����
      frmNomogramma.CurrentY = picNomogramma.Top + picNomogramma.Height - frmNomogramma.TextHeight("8") / 2
      frmNomogramma.CurrentX = picNomogramma.Left - frmNomogramma.TextWidth("8.888") * 1.1
      frmNomogramma.Print Format$(Yk, "0.000")
      frmNomogramma.CurrentY = picNomogramma.Top - frmNomogramma.TextHeight("8") / 2
      frmNomogramma.CurrentX = picNomogramma.Left - frmNomogramma.TextWidth("8.888") * 1.1
                               ' - frmNomogramma.TextWidth("8.888")
      frmNomogramma.Print Format$(Yt, "0.000")
    ' ��������� ���������� ����� �� ��� Y � ��������� 0.1
      ' ����������� �������� �������� ����� Y
'       jk = Int((Xk - Xt) * 10)  '  ����� �����
       je = Int(Yt * 10)    '  �������� �����
       jb = Int(Yk * 10) + 1  ' ��������� �����
      ' ��������� Y
       For i = jb To je
        it = i / 10
        frmNomogramma.CurrentY = picNomogramma.Top - (it - Yk) / (Yt - Yk) * picNomogramma.Height + picNomogramma.Height - HT / 2
        frmNomogramma.CurrentX = picNomogramma.Left - frmNomogramma.TextWidth(Trim(Str(i * 0.1))) * 1.2
        frmNomogramma.Print Trim(Str(i * 0.1))
       Next i
 End If
End Sub

Public Sub DrawGrid()
'  ��������� �����
   Dim i As Integer
'  ��������� �������������� �����
   For i = 1 To 19
    picNomogramma.Line (0, i * 0.1)-(1, i * 0.1), RGB(240, 240, 240) ' �������������� �����
   Next i
'  ��������� ������������ �����
   For i = 1 To 9
    picNomogramma.Line (i * 0.1, 0)-(i * 0.1, 2), RGB(240, 240, 240) ' ������������ �����
   Next i
End Sub

Public Sub SetScale()
' ��������� ��������� ����� ������� ���������� �������
'   If Shape1.Visible Then
      If Xt - Xk <> 0 And Yt - Yk <> 0 Then
        picNomogramma.Scale (Xt, Yt)-(Xk, Yk)
      End If
      Shape1.Visible = False
'   End If
End Sub

Public Sub SetScaleBegin()
' ��������� ��������� ��������� ����� ������� ���������� �������
   Xt = 0
   Yt = 2
   Xk = 1
   Yk = 0
   picNomogramma.Scale (Xt, Yt)-(Xk, Yk)
   Shape1.Visible = False
End Sub

