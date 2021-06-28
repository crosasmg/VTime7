Option Strict Off
Option Explicit On
Public Class opt_premiu
	'%-------------------------------------------------------%'
	'% $Workfile:: opt_premiu.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 19/04/04 9:38a                               $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**Data of Opt_premiu15
	'Información de Opt_Premiu15
	Public dEffecdate As Date 'datetime                                                                                                                         no                                  8                       no                                  (n/a)                               (n/a)
	'Public dCompdate   a                   'datetime                                                                                                                         no                                  8                       yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nBank_acc As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nLower_lim As Double 'decimal                                                                                                                          no                                  9           10    2     yes                                 (n/a)                               (n/a)
	Public nUpper_lim As Double 'decimal                                                                                                                          no                                  9           10    2     yes                                 (n/a)                               (n/a)
	Public sParCollect As Double 'char                                                                                                                             no                                  1                       no                                  no                                  no
	Public sReqAmo As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sTechAffect As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nFixInt As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public nAmenLevel As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nPreReceipt As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nIntCalc As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nUpperInt As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public sMod_loLim As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nLowerInt As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public sMod_upLim As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nLower_lim_Agree As Double
	Public nUpper_lim_Agree As Double
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		find()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'% find
	'--------------------------------------------
	Public Function find() As Boolean
		'--------------------------------------------
		Dim lobjOptPremiu As eRemoteDB.Execute
		lobjOptPremiu = New eRemoteDB.Execute
		With lobjOptPremiu
			.StoredProcedure = "reaOpt_Premiu"
			If .Run Then
				nBank_acc = .FieldToClass("nAcc_bank")
				nLower_lim = .FieldToClass("nLower_lim")
				nUpper_lim = .FieldToClass("nUpper_lim")
				sParCollect = .FieldToClass("sParCollect")
				sReqAmo = .FieldToClass("sReqAmo")
				sTechAffect = .FieldToClass("sTechAffect")
				nFixInt = .FieldToClass("nFixInt")
				nAmenLevel = .FieldToClass("nAmenLevel")
				nPreReceipt = .FieldToClass("nPreReceipt")
				nIntCalc = .FieldToClass("nIntCalc")
				nUpperInt = .FieldToClass("nUpperInt")
				sMod_loLim = .FieldToClass("sMod_loLim")
				nLowerInt = .FieldToClass("nLowerInt")
				sMod_upLim = .FieldToClass("sMod_upLim")
				nLower_lim_Agree = .FieldToClass("nLower_lim_Agree")
				nUpper_lim_Agree = .FieldToClass("nUpper_lim_Agree")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lobjOptPremiu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjOptPremiu = Nothing
		
	End Function
End Class






