VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmFAMwithITF 
   AutoRedraw      =   -1  'True
   Caption         =   "Преобразователь частоты с перестраиваемым преселектором"
   ClientHeight    =   4830
   ClientLeft      =   225
   ClientTop       =   855
   ClientWidth     =   7980
   LinkTopic       =   "Form1"
   ScaleHeight     =   4830
   ScaleWidth      =   7980
   StartUpPosition =   3  'Windows Default
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   1
      Top             =   4455
      Width           =   7980
      _ExtentX        =   14076
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   11
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   2
            Object.Width           =   1588
            MinWidth        =   1588
            Text            =   "Оптимум:"
            TextSave        =   "Оптимум:"
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   0
            Object.Width           =   529
            MinWidth        =   529
            Text            =   "Q="
            TextSave        =   "Q="
            Key             =   "Q"
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1411
            MinWidth        =   1411
            Object.ToolTipText     =   "Соотношение смешиваемых частот"
         EndProperty
         BeginProperty Panel4 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   0
            Object.Width           =   742
            MinWidth        =   742
            Text            =   "DF="
            TextSave        =   "DF="
            Key             =   "DF"
         EndProperty
         BeginProperty Panel5 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1411
            MinWidth        =   1411
            Object.ToolTipText     =   "Относительная полоса пропускания"
         EndProperty
         BeginProperty Panel6 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   0
            Object.Width           =   952
            MinWidth        =   952
            Text            =   "Cmin="
            TextSave        =   "Cmin="
            Key             =   "Cmin"
         EndProperty
         BeginProperty Panel7 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1411
            MinWidth        =   1411
         EndProperty
         BeginProperty Panel8 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   0
            Object.Width           =   1059
            MinWidth        =   1059
            Text            =   "Cmax="
            TextSave        =   "Cmax="
            Key             =   "Cmax"
         EndProperty
         BeginProperty Panel9 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1411
            MinWidth        =   1411
         EndProperty
         BeginProperty Panel10 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Bevel           =   0
            Object.Width           =   952
            MinWidth        =   952
            Text            =   "Fтек="
            TextSave        =   "Fтек="
         EndProperty
         BeginProperty Panel11 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   1411
            MinWidth        =   1411
         EndProperty
      EndProperty
   End
   Begin VB.PictureBox Picture1 
      AutoRedraw      =   -1  'True
      BackColor       =   &H80000009&
      Height          =   2175
      Left            =   0
      ScaleHeight     =   2115
      ScaleWidth      =   7875
      TabIndex        =   0
      Top             =   2040
      Width           =   7935
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&File"
      Begin VB.Menu mnuSave 
         Caption         =   "Save"
      End
      Begin VB.Menu mnuEmpty1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "Exit"
      End
   End
   Begin VB.Menu mnuCalc 
      Caption         =   "&Calc"
   End
   Begin VB.Menu mnuConfig 
      Caption         =   "Con&figuration"
      Begin VB.Menu mnuNomogramma 
         Caption         =   "Nomogramma"
      End
      Begin VB.Menu mnuEmpty2 
         Caption         =   "-"
      End
      Begin VB.Menu mnuOptions 
         Caption         =   "Options"
      End
      Begin VB.Menu mnuEmpty3 
         Caption         =   "-"
      End
      Begin VB.Menu mnuOptimum 
         Caption         =   "Оптимальные параметры"
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
   End
End
Attribute VB_Name = "frmFAMwithITF"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''  Входные параметры Модели 1 преобразователя с перестраиваемым преселектором(см. Model1)
'Dim C1 As Double
'Dim C2 As Double
'Dim Q As Double
'Dim Kp As Integer
'Dim M As Integer
'Dim S As Integer
'  Оптимальные параметры Модели1 преобразователя с перестраиваемым преселектором
Dim Qopt As Double
Dim DFopt As Double
' Dim Q As Double ' текущее значение соотношениея смешиваемых частот (определено в Model1.bas)
Dim DF As Double ' Текущее хначение параметра относительнй диапазонной работы
'  Максимальное и минимальное соотношения смешиваемых частот преобразователя
Dim Qmin As Double
Dim Qmax As Double
'  Минимальные и максимальные частоты на входах преобразователя
'  это входные частоты
Dim Fmin1 As Double
Dim Fmax1 As Double
Dim Fmin2 As Double
Dim Fmax2 As Double
'  это рабочая частота
Dim F1 As Double
'  это размах общего диапазона на входе и выходе преобразователя
'    и их ближайших четырех комбинационных частот
Dim Fmin As Double
Dim Fmax As Double
'  Это выходная частота преобразования при нулевом диапазоне
Dim Fbix As Double
'  Это диапазон комбинационных частот частот преобразователя
Dim Fk1min As Double
Dim Fk1max As Double
Dim Fk2min As Double
Dim Fk2max As Double
Dim Fk3min As Double
Dim Fk3max As Double
Dim Fk4min As Double
Dim Fk4max As Double
'  Параметры ближайших комбинационных частот
Dim AP(4) As Integer
Dim CP(4) As Integer
Dim Cmax As Double
Dim Cmin As Double

' Оптимальные параметры вычисленные по другой формуле
Dim QoptOther As Double
' Параметр отвечающий за расчет оптимальных параметров преобразователя или его текущих значений
Dim Optimum As Boolean
' Имя файла для сохранения информации о результатах расчета
Const FileOutModel1 = "Molel1.txt"

Public Sub CalcDfQOptim(IQ As Boolean, Qnom As Double, C1 As Double, C2 As Double, M As Integer, S As Integer, _
                        Qopt As Double, DFopt As Double)
'   Программа предназначена для вычисления оптимальных значений
'     Df - полосы пропускания преобразователя и Q - его соотношения смешиваемых частот
'     Для идеальной модели преобразователя с перестраиваемым преселектором
'   frmNomogramma.Cls
'   picNomogramma.Cls

Dim i As Integer
''  Определение ближайших комбинационных частот порядка Кр к заданному Q
''   при фиксированных знаках операций смешения M и S
     CombinFarey Kp, M, S, Qnom, AP, CP
'  Определение ближайших пораженных точек Сmin и Cmax к соотношению Q
 If M + S = 4 Then
'  Вычисление ближайших пораженных точек для суммирования
   For i = 2 To NC
     If Qnom >= CR(i - 1) / CQ(i - 1) And Qnom <= CR(i) / CQ(i) Then
       Cmin = CR(i - 1) / CQ(i - 1)
       Cmax = CR(i) / CQ(i)
       Exit For
     End If
   Next i
  Else
'  Вычисление ближайших пораженных точек для вычитания
   For i = 2 To NB
     If Qnom >= BR(i - 1) / BQ(i - 1) And Qnom <= BR(i) / BQ(i) Then
       Cmin = BR(i - 1) / BQ(i - 1)
       Cmax = BR(i) / BQ(i)
       Exit For
     End If
   Next i
 End If
'   Вычисление оптимальных параметров DFopt и Qopt
If IQ = False Then
'     Q<=1
 If M + S = 4 Then
' Суммирование частот
' вывод получен Логиновым В.И.
'   DFopt = (Cmax - Cmin) / (1 + Cmax * C2 + Cmin * (1 - C2))
'   Qopt = Cmax - (Cmax * C2 + 1 - C1) * DFopt
'   QoptOther = Cmin - (Cmin * C2 - C1) * DFopt
' вывод получен Сорокиным И.В.
    DFopt = (Cmax - Cmin) / (1 + Cmin + C2 * (Cmax - Cmin))
    Qopt = (Cmin * (Cmax + 1) + C1 * (Cmax - Cmin)) / (1 + Cmin + C2 * (Cmax - Cmin))

  Else
'  Вычитание частот
' вывод получен Логиновым В.И.
'   DFopt = (Cmax - Cmin) / (1 - Cmax * (1 - C2) - Cmin * C2)
'   Qopt = Cmax - (Cmax * (1 - C2) - (1 - C1)) * DFopt
'   QoptOther = Cmin + (Cmin * (1 - C2) + C1) * DFopt
' вывод получен Сорокиным И.В.
  If (C2 * (Cmax - Cmin) - Cmax + 1) <> 0 Then
    DFopt = (Cmax - Cmin) / (C2 * (Cmax - Cmin) - Cmax + 1)
    Qopt = (Cmin * (1 - Cmax) + C1 * (Cmax - Cmin)) / (1 - Cmax + C2 * (Cmax - Cmin))
   Else
    MsgBox "Ошибка в вычислении Qopt и DFopt", , "Ошибка деления на нуль"
'    DFopt = (Cmax - Cmin) / (1 - Cmax * (1 - C2) - Cmin * C2)
'    Qopt = Cmax - (Cmax * (1 - C2) - (1 - C1)) * DFopt
    DFopt = 100
    Qopt = 0.9
  End If
 End If
Else
'     Q>1
 If M + S = 4 Then
' Суммирование частот
' вывод получен Логиновым В.И.
    DFopt = (Cmax - Cmin) / (Cmax * Cmin + C2 * Cmax + Cmin * (1 - C2))
    Qopt = 1 / Cmax + (1 - C2 + C1 * Cmax) / Cmax * DFopt
'    QoptOther = 1 / Cmin - (C2 + (1 - C1) * Cmin) / Cmin * DFopt
  Else
'  Вычитание частот
' вывод получен Логиновым В.И.
  If (C2 * Cmax + Cmin * (1 - C2) - Cmax * Cmin) <> 0 Then
    DFopt = (Cmax - Cmin) / (C2 * Cmax + Cmin * (1 - C2) - Cmax * Cmin)
    Qopt = 1 / Cmax + (1 - C2 - (1 - C1) * Cmax) / Cmax * DFopt
'    QoptOther = 1 / Cmin - (C2 - C1 * Cmin) / Cmin * DFopt
   Else
    MsgBox "Ошибка в вычислении Qopt и DFopt", , "Ошибка деления на нуль"
'    DFopt = (Cmax - Cmin) / (1 - Cmax * (1 - C2) - Cmin * C2)
'    Qopt = Cmax - (Cmax * (1 - C2) - (1 - C1)) * DFopt
    DFopt = 100
    Qopt = 1.1
  End If
 End If

End If

End Sub

Private Sub Calc()
'  Главная программа вычисления параметров преобразователя
Dim Nf As Integer ' Номер файла для вывода информации
Dim i As Integer
'  Вычисление рядов Фарея
   FareySeries Kp
'   FileOutFareySeries Kp
'  Вычисление пораженных точек номограммы комбинационных частот
   SintezPorT Kp, TN, TC
'   FileOutSintezPorT Kp, TN, TC
'  Синтез номограммы комбинационных частот
   CalcNomogramma Kp, TN, TC
'   FileOutNomogramma Kp, TN, TC
'  Вычисление оптимального соотношения смешиваемых частот в одной точке
   CalcDfQOptim IdQ, Qnom, C1, C2, M, S, Qopt, DFopt

End Sub

Private Sub Form_Load()
' Установка параметров номограммы комбинационных частот
TN = 1
TC = 0
Optimum = True
' Начальные параметры преобразователя частоты
    Kp = 5
    C1 = 0.5
    C2 = 0.5
    M = 1
    S = 2
    Q = 0.1
    FA = 100
    StrNorma = "Известна частота гетеродина"
    Norma = 2
  Dim Nf As Integer
  Dim bbb As String
  Nf = FreeFile
  Open App.Path + "\config.cfg" For Input As #Nf
    Input #Nf, Kp
    Input #Nf, C1
    Input #Nf, C2
    Input #Nf, M
    Input #Nf, S
    Input #Nf, Q
    Input #Nf, FA
    Line Input #Nf, StrNorma
    Input #Nf, Norma
    Input #Nf, DFabs
    Input #Nf, YesDFmaxtek
    Input #Nf, bbb
    If bbb = "False" Then
       IdQ = False
      Else
       IdQ = True
    End If
  Close #Nf
End Sub

Private Sub Form_Resize()
' Программа отслеживания размеров окна
  Picture1.Left = frmFAMwithITF.ScaleLeft
  Picture1.Top = frmFAMwithITF.ScaleTop
  Picture1.Height = frmFAMwithITF.ScaleHeight - StatusBar1.Height
  Picture1.Width = frmFAMwithITF.ScaleWidth
End Sub

Private Sub Form_Unload(Cancel As Integer)
  Unload frmNomogramma
  Unload frmFAMwithITF
End Sub

Private Sub mnuCalc_Click()
' Программа вычисления оптимальных параметров преобразователя частоты
'  и построения
    Dim kk As Double
    Dim f2 As Double
'  Вычисление оптимальных параметров
  Calc
'  Программа вывода параметров расчета
Select Case Optimum
 Case True
 ' вывод оптимальных параметров
     StatusBar1.Panels.Item(3) = Format(Qopt, "0.00000")
     StatusBar1.Panels.Item(5) = Format(DFopt, "0.00000")
     StatusBar1.Panels.Item(7) = Format(Cmin, "0.00000")
     StatusBar1.Panels.Item(9) = Format(Cmax, "0.00000")
'  Пересчет знаков преобразования частоты
     If M + S = 3 Then
      If IdQ = False Then
        M = 1
        S = 2
       Else
        M = 2
        S = 1
      End If
     End If
'  Вычисление параметров области фильтрации на номограмме комбинационных частот
     If M + S = 4 Then
      '  Суммирование частот
        Fmin1 = Qopt - C1 * DFopt
        Fmax1 = Qopt + (1 - C1) * DFopt
        Fmin2 = 1 - C2 * DFopt
        Fmax2 = 1 + (1 - C2) * DFopt
        
        If IdQ = False Then
          Qmin = Fmin1 / Fmax2
          Qmax = Fmax1 / Fmin2
         Else
          Qmin = Fmin2 / Fmax1
          Qmax = Fmax2 / Fmin1
        End If
       Else
       
      ' Вычитание частот
        Fmin1 = Qopt - C1 * DFopt
        Fmax1 = Qopt + (1 - C1) * DFopt
        Fmin2 = 1 - C2 * DFopt
        Fmax2 = 1 + (1 - C2) * DFopt
        
        If IdQ = False Then
           If Fmin2 <> 0 Then
             Qmin = Fmin1 / Fmin2
            Else
             Qmin = Cmin
           End If
         Else
           If Fmin1 <> 0 Then
             Qmin = Fmin2 / Fmin1
            Else
            Qmin = Cmin
           End If
        End If
        If IdQ = False Then
           Qmax = Fmax1 / Fmax2
          Else
           Qmax = Fmax2 / Fmax1
        End If
     End If
     Q = Qopt
     DF = DFopt
'   Корректировка параметра DFabs
  '  Программа вычисления абсолютного значения гетеродинной частоты F2
         If Norma = 1 Then
           f2 = FA / Q
         End If
         If Norma = 2 Then
           f2 = FA
         End If
         If Norma = 3 Then
           kk = ((-1) ^ M * ((-1) ^ M + (-1) ^ S) / 2 - C2 * (-1) ^ S - C1 * (-1) ^ M)
           kk = ((-1) ^ M * Q + (-1) ^ S + DF * kk)
           If kk = 0 Then
             MsgBox "Ошибка в вычислении частот (примимаем F2=Fa/0.1)", , "Ошибка деления на нуль"
             f2 = FA / 0.1
            Else
             f2 = FA / kk
           End If
         End If
         ' Корректировка DFabs
         DFabs = DF * f2
Case False
   ' Расчет и вывод текуших параметров
     If YesDFmaxtek = 0 Then
     '  1) Расчет текущего максимального параметра DF по известным Q, Qopt, DFopt, Cmin и Cmax
      If IdQ = False Then
       '   Q<1
        If Qnom < Qopt Then
          DF = DFopt * (Qnom - Cmin) / (Qopt - Cmin)
         Else
          DF = DFopt * (Cmax - Qnom) / (Cmax - Qopt)
        End If
       Else
       '  Q>1
       Dim Qinv As Double
        Qinv = 1 / Q
        If Qinv < Qopt Then
          DF = DFopt * (Qinv - Cmin) / (1 / Qopt - Cmin)
         Else
          DF = DFopt * (Cmax - Qinv) / (Cmax - 1 / Qopt)
        End If
      End If
      Else
     '  1) Расчет текущего относительного параметра DF по известным DFabs, FA
     '  Программа вычисления абсолютного значения гетеродинной частоты F2
         If Norma = 1 Then
           f2 = FA / Q
         End If
         If Norma = 2 Then
           f2 = FA
         End If
         If Norma = 3 Then
           kk = ((-1) ^ M * ((-1) ^ M + (-1) ^ S) / 2 - C2 * (-1) ^ S - C1 * (-1) ^ M)
           kk = ((-1) ^ M * Q + (-1) ^ S + DF * kk)
           If kk = 0 Then
             MsgBox "Ошибка в вычислении частот (примимаем F2=Fa/0.1)", , "Ошибка деления на нуль"
             f2 = FA / 0.1
            Else
             f2 = FA / kk
           End If
         End If
         DF = DFabs / f2
     End If
   '  2) Вывод параметров
     StatusBar1.Panels.Item(3) = Format(Q, "0.0#####")
     StatusBar1.Panels.Item(5) = Format(DF, "0.0#####")
     StatusBar1.Panels.Item(7) = Format(Cmin, "0.00000")
     StatusBar1.Panels.Item(9) = Format(Cmax, "0.00000")
'  Вычисление параметров области фильтрации на номограмме комбинационных частот
     If M + S = 4 Then
     ' Для суммирования частот
        Fmin1 = Q - C1 * DF
        Fmax1 = Q + (1 - C1) * DF
        Fmin2 = 1 - C2 * DF
        Fmax2 = 1 + (1 - C2) * DF
        If IdQ = False Then
          Qmin = Fmin1 / Fmax2
           Qmax = Fmax1 / Fmin2
         Else
          Qmin = Fmin2 / Fmax1
          Qmax = Fmax2 / Fmin1
        End If
       Else
     ' Для вычитания частот
        Fmin1 = Q - C1 * DF
        Fmax1 = Q + (1 - C1) * DF
        Fmin2 = 1 - C2 * DF
        Fmax2 = 1 + (1 - C2) * DF
        If IdQ = False Then
           If Fmin2 <> 0 Then
             Qmin = Fmin1 / Fmin2
            Else
             Qmin = Cmin
           End If
         Else
           If Fmin1 <> 0 Then
             Qmin = Fmin2 / Fmin1
            Else
             Qmin = Cmin
           End If
        End If
        If IdQ = False Then
           Qmax = Fmax1 / Fmax2
          Else
           Qmax = Fmax2 / Fmax1
        End If
     End If
End Select
' Построение области фильтрации на номограмме комбинационных частот
  DrawFilter M, S, Qmin, Qmax
' Построение всех частот в абсолютном масштабе
CalcDrawFrequency Picture1, Q, DF, C1, C2, M, S, FA, AP, CP
End Sub


Private Sub mnuExit_Click()
  Unload frmFAMwithITF
End Sub

Private Sub mnuHelp_Click()

End Sub

Private Sub mnuNomogramma_Click()
  If mnuNomogramma.Checked = False Then
    mnuNomogramma.Checked = True
    '  Load frmNomogramma
    frmNomogramma.Show
   Else
    mnuNomogramma.Checked = False
    '  Unload frmNomogramma
    frmNomogramma.Hide
  End If
End Sub

Private Sub mnuOptimum_Click()
If Optimum = True Then
  Optimum = False
  StatusBar1.Panels.Item(1) = "Текущие:"
  mnuOptimum.Caption = "Текущие параметры"
 Else
  Optimum = True
  StatusBar1.Panels.Item(1) = "Оптимум:"
  mnuOptimum.Caption = "Оптимальные параметры"
 End If
End Sub

Private Sub mnuOptions_Click()
  frmOptionConvert1.Show 1
End Sub

Public Sub CalcDrawFrequency(picDraw As Object, Q As Double, DF As Double, C1 As Double, C2 As Double, _
      M As Integer, S As Integer, FA As Double, AP() As Integer, CP() As Integer)
Dim kk As Double
Dim f2 As Double
'  Программа вычисления абсолютных значений частот
     If Norma = 1 Then
       f2 = FA / Q
     End If
     If Norma = 2 Then
       f2 = FA
     End If
     If Norma = 3 Then
       kk = ((-1) ^ M * ((-1) ^ M + (-1) ^ S) / 2 - C2 * (-1) ^ S - C1 * (-1) ^ M)
       kk = ((-1) ^ M * Q + (-1) ^ S + DF * kk)
       If kk = 0 Then
         MsgBox "Ошибка в вычислении частот (примимаем F2=Fa/0.1)", , "Ошибка деления на нуль"
         f2 = FA / 0.1
        Else
         f2 = FA / kk
       End If
     End If
     
  If M + S = 4 Then
     ' Суммирование частот
       Fmin1 = f2 * (Q - C1 * DF)
       Fmax1 = f2 * (Q + (1 - C1) * DF)
       Fmin2 = f2 * (1 - C2 * DF)
       Fmax2 = f2 * (1 + (1 - C2) * DF)
       F1 = Q * f2
       kk = ((-1) ^ M * ((-1) ^ M + (-1) ^ S) / 2 - C2 * (-1) ^ S - C1 * (-1) ^ M)
       Fbix = f2 * ((-1) ^ M * Q + (-1) ^ S + DF * kk)
'       Qmin = Fmin1 / Fmax2
'       Qmax = Fmax1 / Fmin2
     If IdQ = False Then
       Fk1min = AP(1) * Fmin1 + CP(1) * Fmax2
       Fk1max = AP(1) * Fmax1 + CP(1) * Fmin2
       Fk2min = AP(2) * Fmin1 + CP(2) * Fmax2
       Fk2max = AP(2) * Fmax1 + CP(2) * Fmin2
       Fk3min = AP(3) * Fmin1 + CP(3) * Fmax2
       Fk3max = AP(3) * Fmax1 + CP(3) * Fmin2
       Fk4min = AP(4) * Fmin1 + CP(4) * Fmax2
       Fk4max = AP(4) * Fmax1 + CP(4) * Fmin2
      Else
       Fk1min = AP(1) * Fmin2 + CP(1) * Fmax1
       Fk1max = AP(1) * Fmax2 + CP(1) * Fmin1
       Fk2min = AP(2) * Fmin2 + CP(2) * Fmax1
       Fk2max = AP(2) * Fmax2 + CP(2) * Fmin1
       Fk3min = AP(3) * Fmin2 + CP(3) * Fmax1
       Fk3max = AP(3) * Fmax2 + CP(3) * Fmin1
       Fk4min = AP(4) * Fmin2 + CP(4) * Fmax1
       Fk4max = AP(4) * Fmax2 + CP(4) * Fmin1
     End If
    Else
     ' Вычитание частот
       Fmin1 = f2 * (Q - C1 * DF)
       Fmax1 = f2 * (Q + (1 - C1) * DF)
       Fmin2 = f2 * (1 - C2 * DF)
       Fmax2 = f2 * (1 + (1 - C2) * DF)
       F1 = Q * f2
       kk = ((-1) ^ M * ((-1) ^ M + (-1) ^ S) / 2 - C2 * (-1) ^ S - C1 * (-1) ^ M)
       Fbix = f2 * ((-1) ^ M * Q + (-1) ^ S + DF * kk)
'       If Fmin2 <> 0 Then
'         Qmin = Fmin1 / Fmin2
'        Else
'         Qmin = Cmin
'       End If
'       Qmax = Fmax1 / Fmax2
     If IdQ = False Then
       Fk1min = AP(1) * Fmin1 + CP(1) * Fmin2
       Fk1max = AP(1) * Fmax1 + CP(1) * Fmax2
       Fk2min = AP(2) * Fmin1 + CP(2) * Fmin2
       Fk2max = AP(2) * Fmax1 + CP(2) * Fmax2
       Fk3min = AP(3) * Fmin1 + CP(3) * Fmin2
       Fk3max = AP(3) * Fmax1 + CP(3) * Fmax2
       Fk4min = AP(4) * Fmin1 + CP(4) * Fmin2
       Fk4max = AP(4) * Fmax1 + CP(4) * Fmax2
      Else
       Fk1min = AP(1) * Fmin2 + CP(1) * Fmin1
       Fk1max = AP(1) * Fmax2 + CP(1) * Fmax1
       Fk2min = AP(2) * Fmin2 + CP(2) * Fmin1
       Fk2max = AP(2) * Fmax2 + CP(2) * Fmax1
       Fk3min = AP(3) * Fmin2 + CP(3) * Fmin1
       Fk3max = AP(3) * Fmax2 + CP(3) * Fmax1
       Fk4min = AP(4) * Fmin2 + CP(4) * Fmin1
       Fk4max = AP(4) * Fmax2 + CP(4) * Fmax1
     End If
  End If
'  Поиск диапазона (Fmin, Fmax) частот для построения графика
   Fmin = Fmin1
   Fmax = Fmax1
   If Fmin2 < Fmin Then Fmin = Fmin2
   If Fmax2 > Fmax Then Fmax = Fmax2
   If f2 < Fmin Then Fmin = f2
   If f2 > Fmax Then Fmax = f2
   If F1 < Fmin Then Fmin = F1
   If F1 > Fmax Then Fmax = F1
   If Fbix < Fmin Then Fmin = Fbix
   If Fbix > Fmax Then Fmax = Fbix
   
   If Fk1min < Fmin Then Fmin = Fk1min
   If Fk1min > Fmax Then Fmax = Fk1min
   If Fk1max < Fmin Then Fmin = Fk1max
   If Fk1max > Fmax Then Fmax = Fk1max
   
   If Fk2min < Fmin Then Fmin = Fk2min
   If Fk2min > Fmax Then Fmax = Fk2min
   If Fk2max < Fmin Then Fmin = Fk2max
   If Fk2max > Fmax Then Fmax = Fk2max
   
   If Fk3min < Fmin Then Fmin = Fk3min
   If Fk3min > Fmax Then Fmax = Fk3min
   If Fk3max < Fmin Then Fmin = Fk3max
   If Fk3max > Fmax Then Fmax = Fk3max
   
   If Fk4min < Fmin Then Fmin = Fk4min
   If Fk4min > Fmax Then Fmax = Fk4min
   If Fk4max < Fmin Then Fmin = Fk4max
   If Fk4max > Fmax Then Fmax = Fk4max
   
'  Установка математической области построения графика
  picDraw.Cls
  picDraw.Scale (Fmin - 0.05 * (Fmax - Fmin), 1.3)-(Fmax + 0.05 * (Fmax - Fmin), -0.1)
'  Рисование осей частот
  picDraw.Line (Fmin - 0.05 * (Fmax - Fmin), 0)-(Fmax + 0.05 * (Fmax - Fmin), 0)
  picDraw.Line (Fmin - 0.05 * (Fmax - Fmin), 0.4)-(Fmax + 0.05 * (Fmax - Fmin), 0.4)
  picDraw.Line (Fmin - 0.05 * (Fmax - Fmin), 0.8)-(Fmax + 0.05 * (Fmax - Fmin), 0.8)
'  Рисование начала координат
  picDraw.Line (0, 0)-(0, 1.2)

'  Программа построения абсолютных значений частот на графике
'    Построение входных частот
'     Fmin1, Fmax1 - цвет синий (1), F1 - цвет светло-синий (9), уровень 0.8 - 1.1
    picDraw.Line (Fmin1, 0.8)-(Fmin1, 1.1), QBColor(1)
    picDraw.Line (Fmin1, 1.1)-(Fmax1, 1.1), QBColor(1)
    picDraw.Line (Fmax1, 1.1)-(Fmax1, 0.8), QBColor(1)
    picDraw.Line (F1, 0.8)-(F1, 1.1), QBColor(9)
'  Подписываем входные частоты
  '  Рабочая частота
    picDraw.CurrentX = Fmin1 - picDraw.TextWidth(Format(Fmin1, "####0.##")) / 2
    picDraw.CurrentY = 0.8
    picDraw.Print Format(Fmin1, "####0.##")
    picDraw.CurrentX = Fmax1 - picDraw.TextWidth(Format(Fmax1, "####0.##")) / 2
    picDraw.CurrentY = 0.8
    picDraw.Print Format(Fmax1, "####0.##")
    picDraw.CurrentX = F1 - picDraw.TextWidth(Format(F1, "####0.##")) / 2
    picDraw.CurrentY = 0.8 + picDraw.TextHeight(Format(f2, "####0.##"))
    picDraw.Print Format(F1, "####0.##")
    picDraw.CurrentX = (Fmax1 + Fmin1) / 2 - picDraw.TextWidth("сигнал") / 2
    picDraw.CurrentY = 1.17
    picDraw.Print "сигнал"
    
'     Fmin2, Fmax2 - цвет зеленый (2), F2 - цвет светло-зеленый (10), уровень 0.8 - 1.1
    picDraw.Line (Fmin2, 0.8)-(Fmin2, 1.05), QBColor(2)
    picDraw.Line (Fmin2, 1.05)-(Fmax2, 1.05), QBColor(2)
    picDraw.Line (Fmax2, 1.05)-(Fmax2, 0.8), QBColor(2)
    picDraw.Line (f2, 0.8)-(f2, 1.05), QBColor(10)
'  Подписываем входные частоты
  '  Опорная (гетеродинна) частота
    picDraw.CurrentX = Fmin2 - picDraw.TextWidth(Format(Fmin2, "####0.##")) / 2
    picDraw.CurrentY = 0.8
    picDraw.Print Format(Fmin2, "####0.##")
    picDraw.CurrentX = Fmax2 - picDraw.TextWidth(Format(Fmax2, "####0.##")) / 2
    picDraw.CurrentY = 0.8
    picDraw.Print Format(Fmax2, "####0.##")
    picDraw.CurrentX = f2 - picDraw.TextWidth(Format(f2, "####0.##")) / 2
    picDraw.CurrentY = 0.8 + picDraw.TextHeight(Format(f2, "####0.##"))
    picDraw.Print Format(f2, "####0.##")
    picDraw.CurrentX = (Fmax2 + Fmin2) / 2 - picDraw.TextWidth("гетеродин") / 2
    picDraw.CurrentY = 1.13
    picDraw.Print "гетеродин"
'    Построение выходной частоты
'     Fbix - цвет черный (0), уровень 0.4 - 0.7
    picDraw.DrawWidth = 2
    picDraw.Line (Fbix, 0.4)-(Fbix, 0.7), QBColor(0)
    picDraw.DrawWidth = 1
'  Подписываем выходную частоту
    picDraw.CurrentX = Fbix - picDraw.TextWidth(Format(Fbix, "####0.##")) / 2
    picDraw.CurrentY = 0.4
    picDraw.Print Format(Fbix, "####0.##")
'    Построение комбинационных частот
'     Fk1min, Fk1max - цвет желтый (14), уровень 0 - 0.21
    picDraw.Line (Fk1min, 0)-(Fk1min, 0.21), QBColor(14)
    picDraw.Line (Fk1min, 0.21)-(Fk1max, 0.21), QBColor(14)
    picDraw.Line (Fk1max, 0.21)-(Fk1max, 0), QBColor(14)
'  Подписываем 1 комбинационную частоту
    picDraw.CurrentX = Fk1min - picDraw.TextWidth(Format(Fk1min, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk1min, "####0.##")
    picDraw.CurrentX = Fk1max - picDraw.TextWidth(Format(Fk1max, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk1max, "####0.##")
    picDraw.CurrentX = (Fk1max + Fk1min) / 2 - picDraw.TextWidth(Str(AP(1)) + "q" + Format(CP(1), "+###0")) / 2
    picDraw.CurrentY = 0.1
    picDraw.Print Str(AP(1)) + "q" + Format(CP(1), "+###0")
'     Fk2min, Fk2max - цвет коричневый (6), уровень 0 - 0.24
    picDraw.Line (Fk2min, 0)-(Fk2min, 0.24), QBColor(6)
    picDraw.Line (Fk2min, 0.24)-(Fk2max, 0.24), QBColor(6)
    picDraw.Line (Fk2max, 0.24)-(Fk2max, 0), QBColor(6)
'  Подписываем 2 комбинационную частоту
    picDraw.CurrentX = Fk2min - picDraw.TextWidth(Format(Fk2min, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk2min, "####0.##")
    picDraw.CurrentX = Fk2max - picDraw.TextWidth(Format(Fk2max, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk2max, "####0.##")
    picDraw.CurrentX = (Fk2max + Fk2min) / 2 - picDraw.TextWidth(Str(AP(2)) + "q" + Format(CP(2), "+###0")) / 2
    picDraw.CurrentY = 0.15
    picDraw.Print Str(AP(2)) + "q" + Format(CP(2), "+###0")
'     Fk3min, Fk3max - цвет красный (4), уровень 0 - 0.27
    picDraw.Line (Fk3min, 0)-(Fk3min, 0.27), QBColor(4)
    picDraw.Line (Fk3min, 0.27)-(Fk3max, 0.27), QBColor(4)
    picDraw.Line (Fk3max, 0.27)-(Fk3max, 0), QBColor(4)
'  Подписываем 3 комбинационную частоту
    picDraw.CurrentX = Fk3min - picDraw.TextWidth(Format(Fk3min, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk3min, "####0.##")
    picDraw.CurrentX = Fk3max - picDraw.TextWidth(Format(Fk3max, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk3max, "####0.##")
    picDraw.CurrentX = (Fk3max + Fk3min) / 2 - picDraw.TextWidth(Str(AP(3)) + "q" + Format(CP(3), "+###0")) / 2
    picDraw.CurrentY = 0.2
    picDraw.Print Str(AP(3)) + "q" + Format(CP(3), "+###0")
'     Fk4min, Fk4max - цвет светло-красный (12), уровень 0 - 0.30
    picDraw.Line (Fk4min, 0)-(Fk4min, 0.3), QBColor(12)
    picDraw.Line (Fk4min, 0.3)-(Fk4max, 0.3), QBColor(12)
    picDraw.Line (Fk4max, 0.3)-(Fk4max, 0), QBColor(12)
'  Подписываем 4 комбинационную частоту
    picDraw.CurrentX = Fk4min - picDraw.TextWidth(Format(Fk4min, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk4min, "####0.##")
    picDraw.CurrentX = Fk4max - picDraw.TextWidth(Format(Fk4max, "####0.##")) / 2
    picDraw.CurrentY = 0#
    picDraw.Print Format(Fk4max, "####0.##")
    picDraw.CurrentX = (Fk4max + Fk4min) / 2 - picDraw.TextWidth(Str(AP(4)) + "q" + Format(CP(4), "+###0")) / 2
    picDraw.CurrentY = 0.25
    picDraw.Print Str(AP(4)) + "q" + Format(CP(4), "+###0")


End Sub

Public Sub DrawFilter(M As Integer, S As Integer, Qmin As Double, Qmax As Double)
' Программа построения области фильтрации на номограмме комбинационных частот
Dim Xmin As Double
Dim Xmax As Double
Dim Ymin As Double
Dim Ymax As Double
If IdQ = False Then
' Q<1
  If M + S = 4 Then
' для суммирования частот
    Xmin = Qmin
    Xmax = Qmax
    Ymin = 1 + Qmin
    Ymax = 1 + Qmax
   Else
' для вычитания частот
    Xmin = Qmin
    Xmax = Qmax
    Ymin = 1 - Qmin
    Ymax = 1 - Qmax
  End If
 Else
' Q>1
  If M + S = 4 Then
' для суммирования частот
    Xmin = Qmin
    Xmax = Qmax
    Ymin = 1 + Qmin
    Ymax = 1 + Qmax
   Else
' для вычитания частот
    Xmin = Qmin
    Xmax = Qmax
    Ymin = 1 - Qmin
    Ymax = 1 - Qmax
  End If
 
End If
' Прорисовка линии области фильтрации на номограмме комбинационных частот
frmNomogramma.LineConvert.Visible = True
frmNomogramma.LineConvert.X1 = Xmin
frmNomogramma.LineConvert.Y1 = Ymin
frmNomogramma.LineConvert.X2 = Xmax
frmNomogramma.LineConvert.Y2 = Ymax
frmNomogramma.cirLeft.Visible = True
frmNomogramma.cirLeft.Left = Xmin - frmNomogramma.cirLeft.Width / 2
frmNomogramma.cirLeft.Top = Ymin + frmNomogramma.cirLeft.Height / 2
frmNomogramma.cirRight.Visible = True
frmNomogramma.cirRight.Left = Xmax - frmNomogramma.cirRight.Width / 2
frmNomogramma.cirRight.Top = Ymax + frmNomogramma.cirRight.Height / 2
End Sub

Private Sub Picture1_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
 StatusBar1.Panels.Item(11) = Format(X, "#####0.0#")
End Sub

'Private Sub cmdCalc_Click()
''  Главная программа вычисления параметров преобразователя
'Dim NF As Integer ' Номер файла для вывода информации
'Dim i As Integer
''  Вычисление рядов Фарея
'   FareySeries Kp
'   FileOutFareySeries Kp
''  Вычисление пораженных точек номограммы комбинационных частот
'   SintezPorT Kp, TN, TC
'   FileOutSintezPorT Kp, TN, TC
''  Синтез номограммы комбинационных частот
'   CalcNomogramma Kp, TN, TC
'   FileOutNomogramma Kp, TN, TC
''  Вычисление оптимальных соотношение смешиваемых частот для всей номограммы
''  Вычисление ближайших пораженных точек для суммирования
'   NF = FreeFile
'   Open App.Path + "\" + FileOutModel1 For Append As #NF
'   Print #NF, "Оптимальные параметры преобразователя для суммирования частот"
'   Print #NF, "При С1="; C1; " C2="; C2; " Кол-во зон ="; NC - 1
'   For i = 2 To NC
'       Q = (CR(i - 1) / CQ(i - 1) + CR(i) / CQ(i)) / 2
'       CalcDfQOptim Q, C1, C2, 2, 2, Qopt, DFopt
'       Print #NF, "Cmin="; CR(i - 1) / CQ(i - 1); " Cmax="; CR(i) / CQ(i)
'       Print #NF, " Qopt="; Qopt; " QoptOther="; QoptOther; " DFopt="; DFopt
''  Вычисление соотношений смешиваемых частот для проверки результата расчета Qopt и DFopt
'       Fmin1 = Qopt - C1 * DFopt
'       Fmax1 = Qopt + (1 - C1) * DFopt
'       Fmin2 = 1 - C2 * DFopt
'       Fmax2 = 1 + (1 - C2) * DFopt
'       Qmin = Fmin1 / Fmax2
'       Qmax = Fmax1 / Fmin2
'       Print #NF, "Qmin="; Qmin; " Qmax="; Qmax
'       Fmin1 = QoptOther - C1 * DFopt
'       Fmax1 = QoptOther + (1 - C1) * DFopt
'       Fmin2 = 1 - C2 * DFopt
'       Fmax2 = 1 + (1 - C2) * DFopt
'       Qmin = Fmin1 / Fmax2
'       Qmax = Fmax1 / Fmin2
'       Print #NF, "QminOther="; Qmin; " QmaxOther="; Qmax
'   Next i
'   Print #NF,
'   Print #NF, "Оптимальные параметры преобразователя для вычитания частот"
'   Print #NF, "При С1="; C1; " C2="; C2; " Кол-во зон ="; NB - 1
''  Вычисление ближайших пораженных точек для вычитания
'   For i = 2 To NB
'       Q = (BR(i - 1) / BQ(i - 1) + BR(i) / BQ(i)) / 2
'       CalcDfQOptim Q, C1, C2, 1, 2, Qopt, DFopt
'       Print #NF, "Cmin="; BR(i - 1) / BQ(i - 1); " Cmax="; BR(i) / BQ(i)
'       Print #NF, " Qopt="; Qopt; " QoptOther="; QoptOther; " DFopt="; DFopt
''  Вычисление соотношений смешиваемых частот для проверки результата расчета Qopt и DFopt
'       Fmin1 = Qopt - C1 * DFopt
'       Fmax1 = Qopt + (1 - C1) * DFopt
'       Fmin2 = 1 - C2 * DFopt
'       Fmax2 = 1 + (1 - C2) * DFopt
'       If Fmin2 <> 0 Then
'        Qmin = Fmin1 / Fmin2
'       Else
'        Qmin = 11111
'       End If
'       Qmax = Fmax1 / Fmax2
'       Print #NF, "Qmin="; Qmin; " Qmax="; Qmax
'       Fmin1 = QoptOther - C1 * DFopt
'       Fmax1 = QoptOther + (1 - C1) * DFopt
'       Fmin2 = 1 - C2 * DFopt
'       Fmax2 = 1 + (1 - C2) * DFopt
'       If Fmin2 <> 0 Then
'        Qmin = Fmin1 / Fmin2
'       Else
'        Qmin = 11111
'       End If
'       Qmax = Fmax1 / Fmax2
'       Print #NF, "QminOther="; Qmin; " QmaxOther="; Qmax
'   Next i
'   Print #NF,
'   Close #NF
'
'
''  Вычисление оптимального соотношения смешиваемых частот в одной точке
'   CalcDfQOptim Q, C1, C2, M, S, Qopt, DFopt
'   txtQopt.Text = Str(Qopt)
'   txtDFopt.Text = Str(DFopt)
'
'End Sub

