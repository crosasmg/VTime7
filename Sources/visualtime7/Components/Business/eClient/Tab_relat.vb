Option Strict Off
Option Explicit On
Public Class Tab_relat
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_relat.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on November 09,2000
	'+ Propiedades según la tabla en el sistema 09/11/2000
	
	'+ Column_name                                Type        Computed  Length  Prec  Scale Nullable                          TrimTrailingBlanks                  FixedLenNullInSource
	'-------------------------------------------- ----------- --------- ------- ----- ----- --------------------------------- ----------------------------------- -----------------------------------
	Public nRelaship As Integer 'smallint     no        2           5     0     no                                  (n/a)                               (n/a)
	Public nRel_target As Integer 'smallint     no        2           5     0     yes                                 (n/a)                               (n/a)
	Public sStatregt As String 'char         no        1                       yes                                  no                                  yes
	Public nUsercode As Integer ' NUMBER   22   0     5    N
	'- Se define para obtener indicador desde un SP
    Public sExist As String

    Public sStatregt_target As String
	
	'% Find: Busca una relación en tab_relat
	Public Function Find(ByVal nRelaship As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		
		Dim lrecreaTab_relat As eRemoteDB.Execute
		lrecreaTab_relat = New eRemoteDB.Execute
		
		If nRelaship <> Me.nRelaship Or bFind Then
			
			Me.nRelaship = nRelaship
			
			With lrecreaTab_relat
				.StoredProcedure = "reaTab_relat"
				.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nRelaship = .FieldToClass("nRelaship")
					Me.nRel_target = .FieldToClass("nRel_target")
                    Me.sStatregt = .FieldToClass("sStatregt")
                    Me.sStatregt_target = .FieldToClass("SSTATREG_TARGET")
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			
		End If
		
		Find = lblnRead
		
		'UPGRADE_NOTE: Object lrecreaTab_relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_relat = Nothing
		
	End Function
	
	'% Update: actualiza los datos de la tabla Tab_relat
	Public Function Update() As Boolean
		Dim lrecTab_Relat As eRemoteDB.Execute
		
		lrecTab_Relat = New eRemoteDB.Execute
		
		With lrecTab_Relat
			.StoredProcedure = "updTab_relat"
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecTab_Relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Relat = Nothing
	End Function
	
	'% Delete: borra los datos de la tabla Tab_relat
	Public Function Delete() As Boolean
		Dim lrecTab_Relat As eRemoteDB.Execute
		
		lrecTab_Relat = New eRemoteDB.Execute
		
		With lrecTab_Relat
			.StoredProcedure = "delTab_relat"
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecTab_Relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Relat = Nothing
	End Function
	
	'% Add: agrega los datos de la tabla Tab_relat
	Public Function Add() As Boolean
		Dim lrecCreTab_Relat As eRemoteDB.Execute
		
		lrecCreTab_Relat = New eRemoteDB.Execute
		
		With lrecCreTab_Relat
			.StoredProcedure = "creTab_relat"
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRel_target", nRel_target, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecCreTab_Relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreTab_Relat = Nothing
	End Function
	
	'%Find_Relation: Valida si la relacion esta siendo usada en relations
	Public Function Find_Relation(ByRef nRelation As Integer) As Boolean
		Dim lrecreaRelations_3 As eRemoteDB.Execute
		
		lrecreaRelations_3 = New eRemoteDB.Execute
		
		With lrecreaRelations_3
			.StoredProcedure = "reaRelations_3"
			.Parameters.Add("nRelaship", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_Relation = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaRelations_3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRelations_3 = Nothing
	End Function
	
	'%insValMBC003: esta función se encarga de validar, masiva y puntualmente, los campos del grid
	Public Function insValMBC003(ByVal sCodispl As String, ByVal nRelaship As Integer, ByVal nRel_target As Integer, ByVal sStatregt As String, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMBC003_K_Err
		lclsErrors = New eFunctions.Errors
		
		If nRelaship <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 2803)
		End If
		
		If nRel_target <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 2804)
		End If
		
		If nRelaship > 0 And sAction = "Add" And Find(nRelaship) Then
			lclsErrors.ErrorMessage(sCodispl, 8307)
		End If
		
		If nRel_target = nRelaship And sAction = "Add" Then
			'+ Si la relación es diferente a Hermano, Primo, Amigo, Conyugue, Concubino, Socio
			'+ no pueden ser iguales
			If nRel_target <> 2 And nRel_target <> 8 And nRel_target <> 9 And nRel_target <> 4 And nRel_target <> 11 And nRel_target <> 12 Then
				lclsErrors.ErrorMessage(sCodispl, 10253)
			End If
		End If
		
		insValMBC003 = lclsErrors.Confirm
		
insValMBC003_K_Err: 
		If Err.Number Then
			insValMBC003 = insValMBC003 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'% insPostMBC003: Crea/actualiza los registros correspondientes en la tabla de Int_fixval
	Public Function insPostMBC003(ByVal sAction As String, ByVal nRelaship As Integer, ByVal nRel_target As Integer, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMBC003_Err
		With Me
			.nRelaship = nRelaship
			.nRel_target = nRel_target
			.sStatregt = sStatregt
			.nUsercode = nUsercode
		End With
		
		If (sAction = "Add") Then
			insPostMBC003 = Add
		Else
			insPostMBC003 = Update
		End If
		
insPostMBC003_Err: 
		If Err.Number Then
			insPostMBC003 = False
		End If
		On Error GoTo 0
	End Function
End Class






