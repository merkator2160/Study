Attribute VB_Name = "Model1"
Option Explicit
'  Входные параметры Модели 1 преобразователя с перестраиваемым преселектором
Public C1 As Double
Public C2 As Double
Public Q As Double
Public Qnom As Double
Public IdQ As Boolean ' Идентификатор = True, если Q > 1
'  Public Kp As Integer '  Уже объявлена в модуле FareyNomogramma.bas
Public M As Integer
Public S As Integer
Public FA As Double  ' Значение абсолютной частоты
' Параметры нормировки
Public Norma As Integer ' к какой частоте будет производится нормировка
Public StrNorma As String ' Пояснение к какой частоте будет проводится нормировка
Public DFabs As Double
Public YesDFmaxtek As Integer
