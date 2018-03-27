Attribute VB_Name = "FareyNomogramma"
Option Explicit
'   Программы синтеза номограммы комбинационных частот и
'     расчета ближайших комбинационных частот к
'     рабочему соотношению смешиваемых частот Q

'   Список констант
'     Константы, описывающие тип нелинейности
Public Const TNFullNonLinearity = 0 '  N<KP & M<KP
Public Const TNTriangleNonLinearity = 1  ' N+M<KP
'  Public Const TNHalfNonLinearity =2 '  N<(KP-1)/2 & M<(KP-1)/2

'     Константы, описывающие тип преобразователя
Public Const TCSimple = 0
Public Const TCBallanceGeterodin = 1
Public Const TCBallanceSignal = 2
Public Const TCDoubleBallance = 3

Public Kp As Integer ' Допустимый порядок комбинационных частот
Public TN As Integer ' Тип нелинейности
Public TC As Integer ' Тип преобразователя
Public AllCombin As Boolean  ' Учет синтеза всех комбинационных частот
                          '  в каждой пораженной точке

'     Константа определения имени выходного файла номограммы
'       комбинационных частот для хранения промежуточных результатов
Public Const FileOut = "Nomogramma.txt"
Public Const FileOutFarey = "FareySeries.txt"
Public Const FileOutPorT = "SintezPorT.txt"
Public Const FileOutAllCombins = "NomogrammaAllCombins.txt"


'' Общие переменные для удаления комб. частот из списка
'Public IRPR As Integer
'Public IA1(20) As Integer
'Public IC1(20) As Integer
'Public NN As Integer

'  Выходные массивы ближайших комбинационных частот
'    к заданному соотношению смешиваемых частот Q
Public AP(4) As Integer
Public CP(4) As Integer


'   Общие переменные программ Sinnom и Combin
'      Максимальное количество пораженных точек и
'       комбинационных частот при KQ = 120, NX = 240 -- KP <=20
'       комбинационных частот при KQ = 1200, NX = 2400 -- KP <=80-85 для неполной номограммы
    Const KQ = 1200
    Const NX = 2400
'    Массивы коэффициентов комб. частот через пор. точки (внутренний массив)
    Public KMP1(KQ) As Integer  '  с положительной производной
    Public KMP2(KQ) As Integer
    Public KMM1(KQ) As Integer  '  с отрицательной производной
    Public KMM2(KQ) As Integer
'    Массив пораженных точек (внутренний массив)
    Public CR(KQ) As Double ' при суммировании числитель
    Public CQ(KQ) As Double ' при суммировании знаменатель
    Public BR(KQ) As Double  ' при вычитании числитель
    Public BQ(KQ) As Double  ' при вычитании знаменатель
    Public NC As Integer  ' фактическое число пораженных точек при суммировании
    Public NB As Integer  '  и вычитании частот соответственно
'    Массив рядов Фарея (внутренний исходный массив пораженных точек)
    Public FR(KQ) As Integer '  числитель
    Public FQ(KQ) As Integer '  знаменатель
    Public FN As Integer  ' фактическое число членов ряда Фарея при
'    Массив коэффициентов комбинационных частот
    Public AX1(NX) As Integer  ' при суммировании
    Public CX1(NX) As Integer
    Public AX2(NX) As Integer  ' при вычитании
    Public CX2(NX) As Integer
    Public NX1 As Integer  ' фактическое число комбинационных частот
    Public NX2 As Integer  '    соответственно при суммировании и вычитании
         
Public Sub FareySeries(Kp As Integer)
'------------------------------------------------------------------------+
' Программа  FareySeries  предназначена для синтеза последовательности
' дробей ряда Фарея индекса Kp, используя классический последовательный
' алгоритм добавления медиант соседних дробей
'------------------------------------------------------------------------+
'     ВХОДНЫЕ ДАННЫЕ:
'       Kp - индекс синтезируемого ряда Фарея
'     ВЫХОДНЫЕ ДАННЫЕ:
'       FR(I) - числители ряда Фарея
'       FQ(I) - знаменатели ряда Фарея
'       FN   - текущее число членов ряда Фарея
'------------------------------------------------------------------------+
Dim k As Integer
Dim i As Integer
Dim j As Integer
Dim N As Integer
Dim jk As Integer
' Инициализация начальной дроби Фарея индекса Kp=1
  FN = 2
  FR(1) = 0
  FQ(1) = 1
  FR(2) = 1
  FQ(2) = 1
  N = FN
' Синтез следующих рядов Фарея
 For k = 2 To Kp
  i = 1
  Do
   i = i + 1
   If ((FQ(i - 1) + FQ(i)) <= k) Then
'  Вставить новый член ряда Фарея в последовательность
    N = N + 1   ' Увеличить число членов ряда Фарея на 1
    jk = i + 1  ' Установить нижний предел сдвига ряда
    j = N
'  Сместить остаток ряда на один член выше
    Do
      FR(j) = FR(j - 1)
      FQ(j) = FQ(j - 1)
      j = j - 1
    Loop While j >= jk
'  Вставить новый член ряда Фарея
    FR(i) = FR(i - 1) + FR(i + 1)
    FQ(i) = FQ(i - 1) + FQ(i + 1)
    i = i + 1
   End If
  Loop While i <> N
 Next k
FN = N
End Sub

Public Sub FileOutFareySeries(Kp As Integer)
'  Программа вывода результатов синтеза ряда Фарея
   
   Dim i As Integer
   Dim KS1 As Integer
   Dim KS2 As Integer
   Dim RP1 As Integer
   Dim RP2 As Integer
   Dim Nf As Integer
     Nf = FreeFile
      Open App.Path + "\" + FileOutFarey For Append As Nf
      Print #Nf, "Входные данные п/п FareySeries: "
      Print #Nf, "Ряд Фарея индекса Kр="; Kp
      Print #Nf, "Общее количество членов ряда Фарея Fn="; FN
    For i = 1 To FN
      Print #Nf, " R("; i; ")/Q("; i; ")="; FR(i); "/"; FQ(i); "="; FR(i) / FQ(i)
    Next i
    Print #Nf,
   Close #Nf
End Sub

Public Sub SintezPorT(Kp As Integer, _
                      TypeOfNonLinearity As Integer, TypeOfConvertor As Integer)
' Программа синтезе массива пораженных точек номограммы комбинационных частот
'    Используется подход на основе рядов Фарея
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

k = 1   ' Проверка только основных комбинационных частот
If TypeOfNonLinearity = 1 Then
'  Синтез последовательности пораженных точек для суммирования частот
'   неполная номограмма комбинационных частот
  FareySeries Kp
  i = 2
  N = FN
  Do
   Do
    YesComb = False
    If FR(i) + FQ(i) < Kp Then YesComb = True
'   Дополнительная проверка на вычеркивание по остальным параметрам
      If YesComb Then
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MPlus = k * FQ(i) + 1
       NPlus = -k * FR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * FQ(i) + 1
       NMinus = k * FR(i) + 1
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
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
     End If
'   Вычеркивание членов ряда Фарея
'    При суммировании вычеркиваются R+Q>=Kp
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
'   Сохранение пораженных точек в соотв. массивах
  NC = FN
  For i = 1 To NC
   CR(i) = FR(i)
   CQ(i) = FQ(i)
  Next i
'  Синтез последовательности пораженных точек для вычитания частот
'   неполная номограмма комбинационных частот
  FareySeries Kp + 1
  i = 1
  N = FN
  Do
   Do
    YesComb = False
    If FR(i) + FQ(i) <= Kp + 1 Then YesComb = True
'   Вычеркивание членов ряда Фарея
'    При вычитании вычеркиваются R+Q>Kp+1
'   Дополнительная проверка на вычеркивание по остальным параметрам
      If YesComb Then
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MPlus = k * FQ(i) - 1
       NPlus = -k * FR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * FQ(i) - 1
       NMinus = k * FR(i) + 1
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
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
     End If
'   Вычеркивание членов ряда Фарея
'    При суммировании вычеркиваются R+Q>=Kp
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
'   Сохранение пораженных точек в соотв. массивах
  NB = FN
  For i = 1 To NB
   BR(i) = FR(i)
   BQ(i) = FQ(i)
  Next i
 Else
'  Синтез последовательности пораженных точек для вычитания частот
'   полная номограмма комбинационных частот
  FareySeries Kp
'   Сохранение пораженных точек в соотв. массивах
  NB = FN
  For i = 1 To NB
   BR(i) = FR(i)
   BQ(i) = FQ(i)
  Next i
'  Процедура вычеркивания пораженных точек по другим условиям
  N = NB
  i = 1
  Do
   Do
    YesComb = False
'   Дополнительная проверка на вычеркивание по остальным параметрам
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
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
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
'   Вычеркивание членов ряда Фарея
'    При суммировании вычеркиваются R+Q>=Kp
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

'  Синтез последовательности пораженных точек для суммирования частот
'   полная номограмма комбинационных частот
  FareySeries Kp
'   Сохранение пораженных точек в соотв. массивах
  NC = FN
  For i = 1 To NC
   CR(i) = FR(i)
   CQ(i) = FQ(i)
  Next i
 NC = NC - 1
 CR(NC) = CR(NC + 1)
 CQ(NC) = CQ(NC + 1)
End If
'  Процедура вычеркивания пораженных точек по другим условиям
  N = NC
  i = 1
  Do
   Do
    YesComb = False
'   Дополнительная проверка на вычеркивание по остальным параметрам
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
       If YesPlus Or YesMinus Then
         YesComb = True
        Else
         YesComb = False
       End If
'   Вычеркивание членов ряда Фарея
'    При суммировании вычеркиваются R+Q>=Kp
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
'  Программа вывода результатов синтеза массива пораженных точек
'    номограммы комбинационных частот во вспомогательный файл
   
   Dim i As Integer
   Dim KS1 As Integer
   Dim KS2 As Integer
   Dim RP1 As Integer
   Dim RP2 As Integer
   Dim Nf As Integer
     Nf = FreeFile
      Open App.Path + "\" + FileOutPorT For Append As Nf
      Print #Nf, "Входные данные п/п SintezPorT: "
      Print #Nf, "Номограмма комбинационных частот для Kp="; Kp

      If TypeOfNonLinearity = 0 Then
        Print #Nf, "Синтезируется полная номограмма комбинационных частот"
       Else
        Print #Nf, "Синтезируется неполная номограмма комбинационных частот"
      End If
      If TypeOfConvertor = 0 Then
        Print #Nf, "простого преобразователя"
      End If
      If TypeOfConvertor = 1 Then
        Print #Nf, "с учетом балансности по гереродинному входу"
      End If
      If TypeOfConvertor = 2 Then
        Print #Nf, "с учетом балансности по сигнальному входу"
      End If
      If TypeOfConvertor = 3 Then
        Print #Nf, "для кольцевого балансного пробразователя"
      End If
      Print #Nf,
      Print #Nf, "Суммирование частот"
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
      Print #Nf, "Вычитание частот"
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
'   Программа синтеза комбинационных частот, проходящих через пораженные точки
'     номограммы комбинационных частот
Dim MPlus As Integer  ' коэффициент комбинационной частоты с положительной производной
Dim NPlus As Integer  ' свободный член комбинационной частоты
Dim MMinus As Integer  ' коэффициент комбинационной частоты с отрицательной производной
Dim NMinus As Integer  ' свободный член комбинационной частоты
Dim YesPlus As Boolean
Dim YesMinus As Boolean
Dim k As Integer ' номер комбинационной частоты проходящей через анализируемую пор. точку
Dim i As Integer
'  Обнуление промежуточных массивов
k = 1  ' анализируются только пораженные точки с мин. порядками комбинационных частот
'  Обнуление массивов и переменных комбинационных частот
  For i = 1 To NC  ' Для суммирования частот
    KMP1(i) = 0  '  с положительной производной
    KMM1(i) = 0  '  с отрицательной производной
  Next i
  For i = 1 To NB  ' Для вычитания частот
    KMP2(i) = 0  '  с положительной производной
    KMM2(i) = 0  '  с отрицательной производной
  Next i
  NX1 = 0 ' Для суммирования частот число комбинационных частот
  NX2 = 0 ' Для вычитания частот
'  Анализ каждой пораженной точки для суммирования частот
  For i = 1 To NC
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
   If TypeOfNonLinearity = 0 Then
'  Синтезируется полная номограмма комбинационных частот
     If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
       YesMinus = True
     End If
    Else
'  Синтезируется неполная номограмма комбинационных частот
     If Abs(MPlus) + Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) + Abs(NMinus) < Kp Then
       YesMinus = True
     End If
   End If
'  Сохранение коэффициентов комбинационных чатот в из массивах
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
'  Анализ каждой пораженной точки для вычитания частот
  For i = 1 To NB
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
   MPlus = k * BQ(i) - 1
   NPlus = -k * BR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
   MMinus = -k * BQ(i) - 1
   NMinus = k * BR(i) + 1
'   Проверка удовлетворяет ли данная комб. частота входным условиям
   YesPlus = False
   YesMinus = False
   If TypeOfNonLinearity = 0 Then
'  Синтезируется полная номограмма комбинационных частот
     If Abs(MPlus) < Kp And Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) < Kp And Abs(NMinus) < Kp Then
       YesMinus = True
     End If
    Else
'  Синтезируется неполная номограмма комбинационных частот
     If Abs(MPlus) + Abs(NPlus) < Kp Then
       YesPlus = True
     End If
     If Abs(MMinus) + Abs(NMinus) < Kp Then
       YesMinus = True
     End If
   End If
'  Сохранение коэффициентов комбинационных чатот в из массивах
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
'  Программа вывода результатов синтеза номограммы комбинационных частот
   
   Dim i As Integer
   Dim KS1 As Integer
   Dim KS2 As Integer
   Dim RP1 As Integer
   Dim RP2 As Integer
   Dim Nf As Integer
     Nf = FreeFile
      Open App.Path + "\" + FileOut For Append As Nf
      Print #Nf, "Входные данные п/п CalcNomogramma: "
      Print #Nf, "Номограмма комбинационных частот для Kp="; Kp

      If TypeOfNonLinearity = 0 Then
        Print #Nf, "Синтезируется полная номограмма комбинационных частот"
       Else
        Print #Nf, "Синтезируется неполная номограмма комбинационных частот"
      End If
      If TypeOfConvertor = 0 Then
        Print #Nf, "простого преобразователя"
      End If
      If TypeOfConvertor = 1 Then
        Print #Nf, "с учетом балансности по гереродинному входу"
      End If
      If TypeOfConvertor = 2 Then
        Print #Nf, "с учетом балансности по сигнальному входу"
      End If
      If TypeOfConvertor = 3 Then
        Print #Nf, "для кольцевого балансного пробразователя"
      End If
      Print #Nf,
      Print #Nf, "Суммирование частот"
      Print #Nf, "Общее количество комбинационных частот NX1="; NX1
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
      Print #Nf, "Вычитание частот"
      Print #Nf, "Общее количество комбинационных частот NX2="; NX2
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
'     Программа Combin предназначена для вычисления коэффициентов
'     уравнений четырех ближайших комбинационных частот к соотношению
'     смешиваемых частот Q, для суммирования частот
'     M=2,IS=2; и для вычитания частот M=1,IS=2; M=2,IS=1
'
'     Входные параметры:
'       KP    - допустимый порядок комбинационных частот;
'       M,IS  - идентификаторы видов преобразования частоты;
'       Q     - соотношения смешиваемых частот (рабочих к опорным);
'
'     Выходные параметры:
'       A(4) - массив соотн., пораженных комбинационными частотами;
'       AP(4) - массив коэфф. частоты в уравнении комб. частот FK=AP*Q+CP
'       CP(4) - массив свободных членов в ур-нии комб. частот FK=AP*Q+CP
'
'     Внутренние параметры п/программы:
'       R    - промежуточное соотношение смешиваемых частот;
'       NC   - кол-во соотн. смешиваемых частот, пораж. комб. при суммировании
'       NB   - кол-во соотн. смешиваемых частот, пораж. комб. при вычитании
'       A1, A2 - промежуточные переменные
' Массивы:
'      K(4)   - массив номеров соотн., пораженных комбин. частотами
'      C,B    - массив соотн. смешив. частот, пораж. комбин. частотами
'      AX1,CX1 - массивы коэфф. ур-ний комбин. частот при суммировании
'      AX2,CX2 - - массивы коэфф. ур-ний комбин. частот при вычитании
'     KMP1,KMM1,KMP2,KMM2 - массивы, в которых хранятся номера ур-ний
'          комбин. частот с положительными(P) и отрицательными (M) наклонами
'

Dim r As Double
Dim NA As Integer
Dim k(4) As Integer
Dim A(4) As Double
Dim A1 As Double
Dim A2 As Double
Dim j As Integer
'
'  Вычисление ближайших соотношений смешиваемых частот, пораженных
'  комбинационными частотами недопустимого порядка
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
'  Контроль границ интервала преобразования частоты,
'  вычисление коэфф. ближайших комбинационных частот
'
If M + S = 4 Then
'    Суммирование частот
100
 If KMP1(k(1)) <> 0 Then
   A(1) = CR(k(1)) / CQ(k(1))
   AP(1) = AX1(KMP1(k(1)))
   CP(1) = CX1(KMP1(k(1)))
  Else
'   Цикл перебора пораженных точек
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
'    Вычитание частот
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

'    Печать информации из п/п Combin в файл

Dim Nf As Integer
   Nf = FreeFile
   Open App.Path + "\" + FileOut For Append As Nf
   Print #Nf, "Входные данные п/п CombinFarey: "
   Print #Nf, " KP="; Kp; ", M="; M; ", IS="; S; ", Q="; Q
   Print #Nf, "Выходные данные:"
   Print #Nf, " Fc1="; AP(1); "*q+"; CP(1); ",  Fc2="; AP(2); "*q+"; CP(2)
   Print #Nf, " Fc3="; AP(3); "*q+"; CP(3); ",  Fc4="; AP(4); "*q+"; CP(4)
   Print #Nf,
   Close #Nf
End Sub

Public Sub SintezAllCombins(Kp As Integer, TypeOfNonLinearity As Integer)
'   Программа синтеза комбинационных частот, проходящих через пораженные точки
'     управление только по типу нелинейности без фиксации самой пораженной точки
'   Программа для отладки подхода изложенного в диссертации
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
Dim KmaxPlus As Integer  '  Предельный цикл перебора комб. частот в пор. точке
                         ' с положительной производной
Dim KmaxMinus As Integer  '  Предельный цикл перебора комб. частот в пор. точке
                         ' с отрицательной производной
Dim KolPlusBase As Integer '  Количество основных комбинационных частот при суммировании
Dim KolPlusAll As Integer '  Общее число комбинационных частот при суммировании
Dim KolMinusBase As Integer '  Количество основных комбинационных частот при вычитании
Dim KolMinusAll As Integer '  Общее число комбинационных частот при вычитании
   Dim Nf As Integer
   Nf = FreeFile
   Open App.Path + "\" + FileOutAllCombins For Append As Nf
   Print #Nf, "Входные данные п/п SintezAllCombins: "
   Print #Nf, "Номограмма комбинационных частот для Kp="; Kp
  
  ' Проверка только основных комбинационных частот
If TypeOfNonLinearity = 1 Then
'  Синтез последовательности пораженных точек для суммирования частот
'   неполная номограмма комбинационных частот
        
  Print #Nf, "Синтезируется неполная номограмма комбинационных частот"
  Print #Nf, "Для суммирования частот"
  Print #Nf, "Общее количество пораженных точек NC="; NC
  
  KolPlusBase = 0
  KolPlusAll = 0
  For i = 1 To NC
    Print #Nf, " I="; i; CR(i); "/"; CQ(i); " Q="; CR(i) / CQ(i)
' Определение максимального количества комб. прямых проходящих через пораженную точку
    KmaxPlus = Int((Kp - 1) / (CR(i) + CQ(i)))
    KmaxMinus = KmaxPlus
    If CR(i) = 0 And CQ(i) = 1 Then
'  Оценка для положительной производной
       KmaxPlus = KmaxPlus - 2
'   Оценка для отрицательной производной
       KmaxMinus = KmaxMinus - 2
    End If
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
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
'  Синтезируется неполная номограмма комбинационных частот
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
'  Распечатка комбинационных частот проходящих через пораженные точки
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
  Print #Nf, " Кол-во основных комб. частот KolPlusBase="; KolPlusBase
  Print #Nf, " Общее количество комб. частот KolPlusAll="; KolPlusAll
  Print #Nf,
'  Синтез последовательности пораженных точек для вычитания частот
'   неполная номограмма комбинационных частот
      
  Print #Nf, "Для вычитания частот"
  Print #Nf, "Общее количество пораженных точек NB="; NB
         
  KolMinusBase = 0
  KolMinusAll = 0
  For i = 1 To NB
    Print #Nf, " I="; i; BR(i); "/"; BQ(i); " Q="; BR(i) / BQ(i)
'  Определение максимального количества комб. прямых проходящих через пораженную точку
    KmaxPlus = Int((Kp + 1) / (BR(i) + BQ(i)))
    KmaxMinus = Int((Kp - 3) / (BR(i) + BQ(i)))
    If BR(i) = 0 And BQ(i) = 1 Then
'  Оценка для положительной производной
       KmaxPlus = KmaxPlus - 2
    End If
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
'  Для каждой пораженной точки
'  Вычисление коэффициентов комбинационных частот с положительной производной
       MPlus = k * BQ(i) - 1
       NPlus = -k * BR(i) + 1
'  Вычисление коэффициентов комбинационных частот с отрицательной производной
       MMinus = -k * BQ(i) - 1
       NMinus = k * BR(i) + 1
'  Проверка удовлетворяет ли данная комб. частота входным условиям
       YesPlus = False
       YesMinus = False
'  Синтезируется неполная номограмма комбинационных частот
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
'  Распечатка комбинационных частот проходящих через пораженные точки
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
  Print #Nf, " Кол-во основных комб. частот KolMinusBase="; KolMinusBase
  Print #Nf, " Общее количество комб. частот KolMinusAll="; KolMinusAll
  Print #Nf,
 
 Else
'  Синтез последовательности пораженных точек для вычитания частот
'   полная номограмма комбинационных частот
  
  Print #Nf, "Синтезируется полная номограмма комбинационных частот"
'  Синтез последовательности пораженных точек для суммирования частот
'   полная номограмма комбинационных частот
  
  Print #Nf, "Для суммирования частот"
  Print #Nf, "Общее количество пораженных точек NC="; NC
  
  KolPlusBase = 0
  KolPlusAll = 0
  For i = 1 To NC
    Print #Nf, " I="; i; CR(i); "/"; CQ(i); " Q="; CR(i) / CQ(i)
' Определение максимального количества комб. прямых проходящих через пораженную точку
'  Оценка для положительной производной
       KmaxPlus = Int((Kp - 2) / (CQ(i)))
'   Оценка для отрицательной производной
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
'  Синтезируется полная номограмма комбинационных частот
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
'  Распечатка комбинационных частот проходящих через пораженные точки
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
  Print #Nf, " Кол-во основных комб. частот KolPlusBase="; KolPlusBase
  Print #Nf, " Общее количество комб. частот KolPlusAll="; KolPlusAll
  Print #Nf,
  
  Print #Nf, "Для вычитания частот"
  Print #Nf, "Общее количество пораженных точек NB="; NB

  KolMinusBase = 0
  KolMinusAll = 0
  For i = 1 To NB
    Print #Nf, " I="; i; BR(i); "/"; BQ(i); " Q="; BR(i) / BQ(i)
' Определение максимального количества комб. прямых проходящих через пораженную точку
'  Оценка для положительной производной
       KmaxPlus = Int(Kp / (BQ(i)))
'  Оценка для отрицательной производной
       KmaxMinus = Int((Kp - 2) / (BQ(i)))
    If KmaxPlus > KmaxMinus Then
      Kmax = KmaxPlus
     Else
      Kmax = KmaxMinus
    End If
    Print #Nf, " KmaxPlus="; KmaxPlus; "  KmaxMinus="; KmaxMinus
    For k = 1 To Kmax
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
'  Синтезируется полная номограмма комбинационных частот
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
'  Распечатка комбинационных частот проходящих через пораженные точки
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
  Print #Nf, " Кол-во основных комб. частот KolMinusBase="; KolMinusBase
  Print #Nf, " Общее количество комб. частот KolMinusAll="; KolMinusAll
  Print #Nf,
End If
Print #Nf,
Close #Nf
End Sub

