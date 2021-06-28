Option Strict Off
Option Explicit On
Public Class T_Bulletin
	'%-------------------------------------------------------%'
	'% $Workfile:: T_Bulletin.cls                           $%'
	'% $Author:: Nvaplat19                                  $%'
	'% $Date:: 25/08/03 6:46p                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Public Function Del(ByRef nBordereaux As Double, ByRef nBulletin As Double) As Boolean
		Dim lrecT_Bulletin As New eRemoteDB.Execute
		
		On Error GoTo Del_err
		
		With lrecT_Bulletin
			.StoredProcedure = "delT_Bulletin"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Del = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecT_Bulletin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Bulletin = Nothing
		
		Dim mobjColformRef As ColformRef
		If Del Then
			lrecT_Bulletin = New eRemoteDB.Execute
			
			With lrecT_Bulletin
				.StoredProcedure = "reaT_Bulletins"
				
				.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If Not .Run(True) Then
					
					mobjColformRef = New ColformRef
					
					With mobjColformRef
						.findColFormRef(nBordereaux)
						
						.sConwin = "2" & Mid(.sConwin, 2)
						.UpdateConWin()
					End With
					
					'UPGRADE_NOTE: Object mobjColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mobjColformRef = Nothing
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecT_Bulletin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecT_Bulletin = Nothing
		End If
		
Del_err: 
		If Err.Number Then
			Del = False
		End If
		
		On Error GoTo 0
	End Function
	
	Public Function Add(ByRef nBordereaux As Double, ByRef nBulletin As Double) As Boolean
		Dim lrecT_Bulletin As New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		With lrecT_Bulletin
			.StoredProcedure = "creT_Bulletin"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
	End Function
	
	Public Function Find(ByRef nBordereaux As Double, ByRef nBulletin As Double) As Boolean
		Dim lrecT_Bulletin As New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecT_Bulletin
			.StoredProcedure = "reaT_Bulletin"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find = .Run(False)
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
	
	Public Function insPostCO513(ByVal ldblBordereaux As Double, ByVal ldblBulletin As Double, ByVal ldtmPaydate As Date, ByVal lintCurrpay As Integer, ByVal ldblExchange As Double, ByVal lintUsercode As Integer) As Boolean
		insPostCO513 = Add(ldblBordereaux, ldblBulletin)
		
		Dim mobjColformRef As ColformRef
		If insPostCO513 Then
			'Dim lclsBulletin As eCollection.Bulletin
			
			'Set lclsBulletin = New eCollection.Bulletin
			
			'With lclsBulletin
			'    .dPaydate = ldtmPaydate
			'    .nCurrpay = lintCurrpay
			'    .nExchange = ldblExchange
			'    .nUsercode = lintUsercode
			
			'    insPostCO513 = .UpdateStatBulletin(ldblBordereaux, ldblBulletin, 1)
			
			'    Set lclsBulletin = Nothing
			'End With
			
			'If insPostCO513 Then
			
			mobjColformRef = New ColformRef
			
			With mobjColformRef
				.findColFormRef(ldblBordereaux)
				
				.sConwin = "1" & Mid(.sConwin, 2)
				
				insPostCO513 = .UpdateConWin
			End With
			
			'UPGRADE_NOTE: Object mobjColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mobjColformRef = Nothing
			'End If
		End If
	End Function
End Class






