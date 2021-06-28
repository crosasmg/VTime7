Option Strict Off
Option Explicit On
Friend Class NotesImages
	'%-------------------------------------------------------%'
	'% $Workfile:: NotesImages.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used to make the reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Dim lblnNotes As Boolean
		Dim llngNotes As Integer
		Dim ldtmEffecdate As Date
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(Parameters("HdEffecdate").Valor) Then
			ldtmEffecdate = Today
		Else
			ldtmEffecdate = CDate(Parameters("HdEffecdate").Valor)
		End If
		
		lblnNotes = (Parameters("nCurrentFolder").Valor = 36)
		On Error Resume Next
		llngNotes = Parameters("nNotenum").Valor
		If Err.Number Then
			On Error GoTo 0
			On Error Resume Next
			llngNotes = Parameters("nImageNum").Valor
			If Err.Number Then
				llngNotes = 0
			End If
		End If
		On Error GoTo 0
		Select Case nParentFolder
			Case 4 'Notas del Cliente
				Find = insReaNotesImagesCli(String.Empty & Parameters("sClient").Valor, lblnNotes)
			Case 1 'Notas de una Póliza
				Find = insReaNotesImagesPol((Parameters("HsCertype").Valor), (Parameters("HnBranch").Valor), (Parameters("HnProduct").Valor), (Parameters("HnPolicy").Valor), lblnNotes)
			Case 6 'Notas de un Siniestro
				Find = insReaNotesImagesClaim((Parameters("HnClaim").Valor), lblnNotes)
			Case 22 'Notas de una Póliza
				Find = insReaNotesImagesClauses((Parameters("HsCertype").Valor), (Parameters("HnBranch").Valor), (Parameters("HnProduct").Valor), (Parameters("HnPolicy").Valor), (Parameters("HnCertif").Valor), ldtmEffecdate, (Parameters("nClause").Valor), (Parameters("nId").Valor), lblnNotes)
			Case Else
				Find = insReaNotesImages(llngNotes, lblnNotes, Parameters("nConsec").Valor)
		End Select
	End Function
	
	'**% insReaNotesImagesCli. This function reads of the notes table, to obtain
	'**% the associated data to this. (By clients)
	'%insReaNotesImagesCli. Esta funcion se encarga de realizar la lectura a la tabla de notas, para obtener
	'%Los datos asociados a la misma (Por cliente).
	Private Function insReaNotesImagesCli(ByRef lstrClient As Object, ByRef lblnNotes As Boolean) As eRemoteDB.Execute
		If lstrClient <> String.Empty Then
			insReaNotesImagesCli = New eRemoteDB.Execute
			With insReaNotesImagesCli
				.StoredProcedure = "queDatNotesImagesCli"
				.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIsNote", IIf(lblnNotes, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					.RCloseRec()
					'UPGRADE_NOTE: Object insReaNotesImagesCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					insReaNotesImagesCli = Nothing
				End If
			End With
		Else
			'UPGRADE_NOTE: Object insReaNotesImagesCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insReaNotesImagesCli = Nothing
		End If
	End Function
	
	'**% insReaNotesImagesPol. This function reads of the notes table, to obtain
	'**% the associated data to this. (By policies)
	'%insReaNotesImagesPol. Esta funcion se encarga de realizar la lectura a la tabla de notas, para obtener
	'%Los datos asociados a la misma (Por polizas).
	Private Function insReaNotesImagesPol(ByRef lstrCertype As String, ByRef llngBranch As Integer, ByRef llngProduct As Integer, ByRef ldblPolicy As Double, ByRef lblnNotes As Boolean) As eRemoteDB.Execute
		If lstrCertype <> String.Empty Then
			insReaNotesImagesPol = New eRemoteDB.Execute
			With insReaNotesImagesPol
				.StoredProcedure = "queDatNotesImagespol"
				.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", ldblPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIsNote", IIf(lblnNotes, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					.RCloseRec()
					'UPGRADE_NOTE: Object insReaNotesImagesPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					insReaNotesImagesPol = Nothing
				End If
			End With
		Else
			'UPGRADE_NOTE: Object insReaNotesImagesPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insReaNotesImagesPol = Nothing
		End If
	End Function
	'**% insReaNotesImagesClauses. This function reads of the notes table, to obtain
	'**% the associated data to this. (By policies)
	'%insReaNotesImagesPol. Esta funcion se encarga de realizar la lectura a la tabla de notas, para obtener
	'%Los datos asociados a sus clausulas. Por ahora no se emplea parametro llngClause
	Private Function insReaNotesImagesClauses(ByRef lstrCertype As String, ByRef llngBranch As Integer, ByRef llngProduct As Integer, ByRef ldblPolicy As Double, ByRef ldblCertif As Double, ByRef ldmtEffecdate As Date, ByRef llngClause As Integer, ByRef llngId As Integer, ByRef lblnNotes As Boolean) As eRemoteDB.Execute
		If lstrCertype <> String.Empty Then
			insReaNotesImagesClauses = New eRemoteDB.Execute
			With insReaNotesImagesClauses
				.StoredProcedure = "queDatNotesImagesClauses"
				.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", ldblPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", ldblCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", ldmtEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nClause", llngClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nId", llngId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIsNote", IIf(lblnNotes, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					.RCloseRec()
					'UPGRADE_NOTE: Object insReaNotesImagesClauses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					insReaNotesImagesClauses = Nothing
				End If
			End With
		Else
			'UPGRADE_NOTE: Object insReaNotesImagesClauses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insReaNotesImagesClauses = Nothing
		End If
	End Function
	
	'**% insReaNotesImagesClaim. This function reads of the notes table, to obtain
	'**% the associated data to this. (By Claims)
	'%insReaNotesImagesClaim. Esta funcion se encarga de realizar la lectura a la tabla de notas, para obtener
	'%Los datos asociados a la misma (Por siniestros).
	Private Function insReaNotesImagesClaim(ByRef ldblClaim As Double, ByRef lblnNotes As Boolean) As eRemoteDB.Execute
		If ldblClaim > eRemoteDB.Constants.intNull Then
			insReaNotesImagesClaim = New eRemoteDB.Execute
			With insReaNotesImagesClaim
				.StoredProcedure = "queDatNotesImagesclaim"
				.Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIsNote", IIf(lblnNotes, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					.RCloseRec()
					'UPGRADE_NOTE: Object insReaNotesImagesClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					insReaNotesImagesClaim = Nothing
				End If
			End With
		Else
			'UPGRADE_NOTE: Object insReaNotesImagesClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insReaNotesImagesClaim = Nothing
		End If
	End Function
	
	'**% insReaNotesImages_o. This function reads of the notes table, to obtain
	'**% the associated data to the same. Receipt as a parameter the number of the note and a consecutive,
	'**% to complete the primary key.
	'%insReaNotesImages_o. Esta funcion se encarga de realizar la lectura a la tabla de notas, para obtener
	'%Los datos asociados a la misma. Recibe como parametros el número de la nota y un consescutivo,
	'%para completar la llave primaria
	Private Function insReaNotesImages(ByRef llngNotenum As Integer, ByRef lblnNotes As Boolean, ByVal llngConsec As Integer) As eRemoteDB.Execute
		If llngNotenum <> 0 Then
			insReaNotesImages = New eRemoteDB.Execute
			With insReaNotesImages
				.StoredProcedure = "queDatNotesImages"
				.Parameters.Add("nNoteNum", llngNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nConsec", llngConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIsNote", IIf(lblnNotes, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					.RCloseRec()
					'UPGRADE_NOTE: Object insReaNotesImages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					insReaNotesImages = Nothing
				End If
			End With
		Else
			'UPGRADE_NOTE: Object insReaNotesImages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insReaNotesImages = Nothing
		End If
	End Function
End Class






