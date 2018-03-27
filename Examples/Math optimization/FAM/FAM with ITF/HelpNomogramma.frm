VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form frmHelp 
   Caption         =   "Краткая справка ""Nomogramma-Farey"""
   ClientHeight    =   4710
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6450
   LinkTopic       =   "Form1"
   ScaleHeight     =   4710
   ScaleWidth      =   6450
   StartUpPosition =   3  'Windows Default
   Begin RichTextLib.RichTextBox rtbHelp 
      Height          =   4695
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   6375
      _ExtentX        =   11245
      _ExtentY        =   8281
      _Version        =   393217
      Enabled         =   -1  'True
      ReadOnly        =   -1  'True
      ScrollBars      =   3
      TextRTF         =   $"HelpNomogramma.frx":0000
   End
End
Attribute VB_Name = "frmHelp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_Load()
'  Программа загрузки файла помощи
  rtbHelp.Left = frmHelp.ScaleLeft
  rtbHelp.Top = frmHelp.ScaleTop
  rtbHelp.LoadFile App.Path + "\" + "NomogrammaHelp.txt", 1
End Sub

Private Sub Form_Resize()
  rtbHelp.Height = frmHelp.ScaleHeight
  rtbHelp.Width = frmHelp.ScaleWidth
End Sub

