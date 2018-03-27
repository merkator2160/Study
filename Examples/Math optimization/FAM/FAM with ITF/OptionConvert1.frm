VERSION 5.00
Begin VB.Form frmOptionConvert1 
   AutoRedraw      =   -1  'True
   Caption         =   "Параметры преобразователя (Модель 1)"
   ClientHeight    =   4380
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4815
   LinkTopic       =   "Form1"
   ScaleHeight     =   4380
   ScaleWidth      =   4815
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame frmQ 
      BorderStyle     =   0  'None
      Height          =   645
      Left            =   120
      TabIndex        =   23
      Top             =   1920
      Width           =   4575
      Begin VB.TextBox txtQreal 
         Height          =   285
         Left            =   3600
         Locked          =   -1  'True
         TabIndex        =   29
         Top             =   360
         Width           =   975
      End
      Begin VB.OptionButton optQGt1 
         Caption         =   "Q>1"
         Height          =   255
         Left            =   1800
         TabIndex        =   28
         Top             =   360
         Width           =   615
      End
      Begin VB.OptionButton optQLt1 
         Caption         =   "Q<1"
         Height          =   255
         Left            =   1200
         TabIndex        =   27
         Top             =   360
         Value           =   -1  'True
         Width           =   615
      End
      Begin VB.TextBox txtQ 
         Height          =   285
         Left            =   3960
         TabIndex        =   24
         Text            =   "0.1"
         Top             =   0
         Width           =   615
      End
      Begin VB.Label Label10 
         Caption         =   "Итоговое Q="
         Height          =   255
         Left            =   2520
         TabIndex        =   30
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label9 
         Caption         =   "Управление Q:"
         Height          =   255
         Left            =   0
         TabIndex        =   26
         Top             =   360
         Width           =   1215
      End
      Begin VB.Label Label6 
         Caption         =   "Рабочее соотношение смешиваемых частот Q="
         Height          =   255
         Left            =   0
         TabIndex        =   25
         Top             =   0
         Width           =   3735
      End
   End
   Begin VB.Frame frmDF 
      BorderStyle     =   0  'None
      Height          =   375
      Left            =   2760
      TabIndex        =   20
      Top             =   2520
      Width           =   1935
      Begin VB.OptionButton optMaxDF 
         Caption         =   "макс."
         Height          =   255
         Left            =   1080
         TabIndex        =   22
         Top             =   120
         Value           =   -1  'True
         Width           =   735
      End
      Begin VB.OptionButton optTekDF 
         Caption         =   "тек."
         Height          =   255
         Left            =   120
         TabIndex        =   21
         Top             =   120
         Width           =   735
      End
   End
   Begin VB.TextBox txtDFabs 
      Height          =   285
      Left            =   1800
      TabIndex        =   18
      Text            =   "10"
      Top             =   2640
      Width           =   855
   End
   Begin VB.Frame frmF 
      BorderStyle     =   0  'None
      Height          =   495
      Left            =   120
      TabIndex        =   14
      Top             =   3285
      Width           =   4575
      Begin VB.OptionButton optFrequency 
         Caption         =   "Промежуточной"
         Height          =   375
         Index           =   2
         Left            =   3000
         TabIndex        =   17
         Top             =   120
         Width           =   1575
      End
      Begin VB.OptionButton optFrequency 
         Caption         =   "Гетеродина"
         Height          =   375
         Index           =   1
         Left            =   1440
         TabIndex        =   16
         Top             =   120
         Value           =   -1  'True
         Width           =   1215
      End
      Begin VB.OptionButton optFrequency 
         Caption         =   "Сигнала"
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   15
         Top             =   120
         Width           =   1095
      End
   End
   Begin VB.TextBox txtF2 
      Height          =   285
      Left            =   2880
      TabIndex        =   13
      Text            =   "100"
      Top             =   3000
      Width           =   1815
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "Cancel"
      Height          =   255
      Left            =   2400
      TabIndex        =   11
      Top             =   3840
      Width           =   735
   End
   Begin VB.TextBox txtKp 
      Height          =   285
      Left            =   4080
      TabIndex        =   5
      Text            =   "5"
      Top             =   120
      Width           =   615
   End
   Begin VB.TextBox txtC1 
      Height          =   285
      Left            =   4080
      TabIndex        =   4
      Text            =   "0.5"
      Top             =   480
      Width           =   615
   End
   Begin VB.TextBox txtC2 
      Height          =   285
      Left            =   4080
      TabIndex        =   3
      Text            =   "0.5"
      Top             =   840
      Width           =   615
   End
   Begin VB.TextBox txtM 
      Height          =   285
      Left            =   4080
      TabIndex        =   2
      Text            =   "1"
      Top             =   1200
      Width           =   615
   End
   Begin VB.TextBox txtS 
      Height          =   285
      Left            =   4080
      TabIndex        =   1
      Text            =   "2"
      Top             =   1560
      Width           =   615
   End
   Begin VB.CommandButton cmdOk 
      Caption         =   "Ok"
      Height          =   255
      Left            =   1560
      TabIndex        =   0
      Top             =   3840
      Width           =   615
   End
   Begin VB.Label Label8 
      Caption         =   "Диапазон частот DF="
      Height          =   255
      Left            =   120
      TabIndex        =   19
      Top             =   2640
      Width           =   1695
   End
   Begin VB.Label Label7 
      Caption         =   "Абсолютное значение частоты F="
      Height          =   255
      Left            =   120
      TabIndex        =   12
      Top             =   3000
      Width           =   2775
   End
   Begin VB.Label Label1 
      Caption         =   "Порядок допустимых комбинационных частот Kp="
      Height          =   255
      Left            =   120
      TabIndex        =   10
      Top             =   120
      Width           =   3975
   End
   Begin VB.Label Label2 
      Caption         =   "Положение частоты F1 внутри ее диапазона C1="
      Height          =   255
      Left            =   120
      TabIndex        =   9
      Top             =   480
      Width           =   3735
   End
   Begin VB.Label Label3 
      Caption         =   "Положение частоты F2 внутри ее диапазона C2="
      Height          =   255
      Left            =   120
      TabIndex        =   8
      Top             =   840
      Width           =   3735
   End
   Begin VB.Label Label4 
      Caption         =   "Идент. знака преобразования по частоте F1 M="
      Height          =   255
      Left            =   120
      TabIndex        =   7
      Top             =   1200
      Width           =   3735
   End
   Begin VB.Label Label5 
      Caption         =   "Идент. знака преобразования по частоте F2 S="
      Height          =   255
      Left            =   120
      TabIndex        =   6
      Top             =   1560
      Width           =   3735
   End
End
Attribute VB_Name = "frmOptionConvert1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdCancel_Click()
  Unload frmOptionConvert1
End Sub

Private Sub cmdOk_Click()
  
  If optQLt1.Value = True Then
    IdQ = False
    If M + S = 3 Then
     txtM.Text = 1
     txtS.Text = 2
    End If
  End If
  
  If optQGt1.Value = True Then
    IdQ = True
    If M + S = 3 Then
     txtM.Text = 2
     txtS.Text = 1
    End If
  End If
  
  C1 = Val(txtC1.Text)
  C2 = Val(txtC2.Text)
  Kp = Val(txtKp.Text)
  
  If IdQ = False Then
    Qnom = Val(txtQ.Text)
    Q = Val(txtQreal.Text)
   Else
    Qnom = Val(txtQ.Text)
    Q = Val(txtQreal.Text)
  End If
  
  M = Val(txtM.Text)
  S = Val(txtS.Text)
  FA = Val(txtF2.Text)
  DFabs = Val(txtDFabs.Text)
  
  Dim Nf As Integer
    Nf = FreeFile
    Open App.Path + "\config.cfg" For Output As #Nf
    Print #Nf, Kp
    Print #Nf, C1
    Print #Nf, C2
    Print #Nf, M
    Print #Nf, S
    Print #Nf, Q
    Print #Nf, FA
    Print #Nf, StrNorma
    Print #Nf, Norma
    Print #Nf, DFabs
    Print #Nf, YesDFmaxtek
    Print #Nf, IdQ
Close #Nf
  Unload frmOptionConvert1
End Sub

Private Sub Form_Load()
    txtC1.Text = C1
    txtC2.Text = C2
    txtKp.Text = Kp
    txtM.Text = M
    txtS.Text = S
    txtF2.Text = FA
    txtDFabs.Text = DFabs
  If IdQ = False Then
    optQLt1.Value = True
    txtQreal.Text = Q
    Qnom = Q
    txtQ.Text = Qnom
   Else
    optQGt1.Value = True
    If Q = 0 Then
      txtQreal.Text = ""
     Else
      txtQreal.Text = Q
      Qnom = 1 / Q
      txtQ.Text = Qnom
    End If
  End If
  Select Case Norma
    Case 1
      optFrequency(0).Value = True
    Case 2
      optFrequency(1).Value = True
    Case 3
      optFrequency(2).Value = True
  End Select
  Select Case YesDFmaxtek
    Case 0
      optMaxDF.Value = True
    Case 1
      optTekDF.Value = True
  End Select

End Sub

Private Sub optFrequency_Click(Index As Integer)
  Select Case Index
    Case 0
      StrNorma = "Известна частота сигнала"
      Norma = 1
    Case 1
      StrNorma = "Известна частота гетеродина"
      Norma = 2
    Case 2
      StrNorma = "Известна промежуточная частота"
      Norma = 3
  End Select
End Sub

Private Sub optMaxDF_Click()
  optMaxDF.Value = True
  YesDFmaxtek = 0
End Sub

Private Sub optQGt1_Click()
   IdQ = True
   If Q <> 0 Then
     txtQreal = 1 / Q
   End If
End Sub

Private Sub optQLt1_Click()
   IdQ = False
   If Val(txtQreal.Text) > 1 Then
     txtQreal = 1 / Q
    Else
     txtQreal = Q
   End If
End Sub

Private Sub optTekDF_Click()
  optTekDF.Value = True
  YesDFmaxtek = 1
End Sub
