Option Strict Off
Option Explicit On
Friend Class Note
	'%-------------------------------------------------------%'
	'% $Workfile:: Note.cls                                 $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used for read operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		'    Select Case lvartag
		'        Case Else
		Find = insReaNotes_o((Parameters("nNoteNum").Valor), (Parameters("nConsec").Valor))
		'    End Select
	End Function
	
	'**% insReaNotes_o. This function reads of the notes table, to obtain the associated data to this.
	'**%                Receives as a parameter the number of the note and a consecutive,
	'**%                to complete the primary key.
	'%insReaNotes_o. Esta funcion se encarga de realizar la lectura a la tabla de notas, para obtener
	'%Los datos asociados a la misma. Recibe como parametros el número de la nota y un consescutivo,
	'%para completar la llave primaria
	Private Function insReaNotes_o(ByRef llngNotenum As Integer, ByRef lintConsec As Integer) As eRemoteDB.Execute
		insReaNotes_o = New eRemoteDB.Execute
		With insReaNotes_o
			.StoredProcedure = "queDatNotes"
			.Parameters.Add("nNoteNum", llngNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", lintConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIsNote", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaNotes_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaNotes_o = Nothing
			End If
		End With
	End Function
End Class






