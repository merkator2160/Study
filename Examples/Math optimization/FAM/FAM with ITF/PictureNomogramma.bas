Attribute VB_Name = "PictureNomogramma"
Option Explicit
'   Модуль предназначен для получения массива коэффициентов
'     комбинационных частот для построения номограммы

'   Массив комбинационных прямых
Const MaxLines = 4800  '  Максимальное число комбинационных прямых
Public ALine(MaxLines) As Integer ' коэфф. частоты
Public CLine(MaxLines) As Integer ' свободный член ур-ния комб. частоты
Public NLines As Integer  ' Общее число комбинационных прямых

Public Sub SintezLines()
'  Программа синтеза массива прямых образующих номограмму
Dim KS1 As Integer
Dim KS2 As Integer
Dim i As Integer
Dim j As Integer
Dim k As Integer
Dim A As Integer
Dim C As Integer
'  Приведение все комб. прямых в один массив
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
'   Синтез дополнительных комбинационных прямых проходящих
'     через пораженные точки с порядком меньшим Kp
   If AllCombin Then
      SintezRestCombin Kp, TN, TC
   End If
'   Исключение избыточности массива комб. частот
  For i = 1 To NLines - 1
   A = ALine(i)
   C = CLine(i)
   For j = i + 1 To NLines
    If A = ALine(j) And C = CLine(j) Then
' Обнаружены две одинаковые комб. прямые
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
'  Программа синтеза дополнительных комбинационных составляющих
'   проходящих через пораженные точки без фиксации самой пораженной точки
Dim i As Integer
Dim j As Integer
Dim N As Integer    ' Предел перебора пораженных точек
Dim YesComb As Boolean  ' проходят ли через нее комб прямые
Dim MPlus As Integer  ' коэффициент комбинационной частоты с положительной производной
Dim NPlus As Integer  ' свободный член комбинационной частоты
Dim MMinus As Integer  ' коэффициент комбинационной частоты с отрицательной производной
Dim NMinus As Integer  ' свободный член комбинационной частоты
Dim YesPlus As Boolean  ' Проходит ли комб. частота с положительной производной
Dim YesMinus As Boolean ' Проходит ли комб. частота с отрицательной производной
Dim k As Integer  ' Коэффициент нескольких комб. частот через поражен. точку
Dim Kmax As Integer  '  Предельный цикл перебора комб. частот в пор. точке
  ' Проверка только основных комбинационных частот
If TypeOfNonLinearity = 1 Then
'  Синтез последовательности пораженных точек для суммирования частот
'   неполная номограмма комбинационных частот
  For i = 1 To NC
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с положительной производной
       MPlus = k * CQ(i) + 1
       NPlus = -k * CR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * CQ(i) + 1
       NMinus = k * CR(i) + 1
'   Проверка удовлетворяет ли данная комб. частота входным условиям
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  Синтезируется неполная номограмма комбинационных частот
       Case 0 ' простой преобразователь
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' балансный по гетеродинному входу
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' балансный по сигнальному входу
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' двойной балансный преобразователь
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
'  Синтез последовательности пораженных точек для вычитания частот
'   неполная номограмма комбинационных частот
  For i = 1 To NB
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с положительной производной
       MPlus = k * BQ(i) - 1
       NPlus = -k * BR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * BQ(i) - 1
       NMinus = k * BR(i) + 1
'   Проверка удовлетворяет ли данная комб. частота входным условиям
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  Синтезируется неполная номограмма комбинационных частот
       Case 0 ' простой преобразователь
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' балансный по гетеродинному входу
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' балансный по сигнальному входу
          If Abs(MPlus) + Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' двойной балансный преобразователь
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
'  Синтез последовательности пораженных точек для вычитания частот
'   полная номограмма комбинационных частот
  For i = 1 To NB
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с положительной производной
       MPlus = k * BQ(i) - 1
       NPlus = -k * BR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * BQ(i) - 1
       NMinus = k * BR(i) + 1
'   Проверка удовлетворяет ли данная комб. частота входным условиям
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  Синтезируется неполная номограмма комбинационных частот
       Case 0 ' простой преобразователь
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' балансный по гетеродинному входу
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' балансный по сигнальному входу
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' двойной балансный преобразователь
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
'  Синтез последовательности пораженных точек для суммирования частот
'   полная номограмма комбинационных частот
  For i = 1 To NC
'    Kmax = (Kp + 2) / CQ(i) + 3
    Kmax = Kp
    For k = 2 To Kmax
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MPlus = k * CQ(i) + 1
       NPlus = -k * CR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * CQ(i) + 1
       NMinus = k * CR(i) + 1
'   Проверка удовлетворяет ли данная комб. частота входным условиям
       YesPlus = False
       YesMinus = False
       Select Case TypeOfConvertor
'  Синтезируется неполная номограмма комбинационных частот
       Case 0 ' простой преобразователь
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            YesMinus = True
          End If
       Case 1 ' балансный по гетеродинному входу
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(NPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) + Abs(NMinus) < Kp Then
            If (Abs(NMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 2 ' балансный по сигнальному входу
          If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
            If (Abs(MPlus) Mod 2) > 0 Then YesPlus = True
          End If
          If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
            If (Abs(MMinus) Mod 2) > 0 Then YesMinus = True
          End If
       Case 3 ' двойной балансный преобразователь
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
