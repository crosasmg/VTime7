Option Strict Off
Option Explicit On
Public Class Relation
	'%-------------------------------------------------------%'
	'% $Workfile:: Relation.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according the table in the system 10/10/2000
	'**+ The key fields are sClient, dFinanDate y  nConcept
	'+ Propiedades según la tabla en el sistema 10/10/2000
	'+ Los campos llaves corresponden a sClient, dFinanDate y  nConcept
	
	'+ Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public sClient As String 'char      no        14                 no       yes                no
	Public sClientr As String 'char      no        14                 no       yes                no
	Public nRelaship As Integer 'smallint  no        2      5     0     no       (n/a)              (n/a)
	Public nUsercode As Integer 'number
	Public nType As Integer 'number
	Public sDigit As String 'number
	Public sRelashipDesc As String
	
	'**+ additional properties
	'+ Propiedades auxiliares
	Public sCliename As String '**Client description
	'Descripción del cliente
	Public sConcept As String '**Descript of the concept of the relationship (See table15)
	'Descripción del concepto de la relaciòn (Ver Table15)
	
	'**+ Variable definition. This variable will contain the status of each instance of the class
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	
	Public nStatusInstance As Integer
	
	'**%Find: Search for the data of the specific client
	'% Find: busca los datos correspondientes para un cliente específico
	Public Function Find(ByVal sClient As String, ByVal sClientr As String, ByVal nRelaship As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaRelation As eRemoteDB.Execute
		
		lrecreaRelation = New eRemoteDB.Execute
		
		'**+ Control break
		'+ Ruptura de control
		If Me.sClient = sClient And Me.sClientr = sClientr And Not bFind Then
			Find = True
		Else
			With lrecreaRelation
				.StoredProcedure = "reaRelations"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = .FieldToClass("sClient")
					Me.sClientr = .FieldToClass("sClientr")
					nRelaship = .FieldToClass("nRelaship")
					sCliename = .FieldToClass("sClieName")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaRelation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaRelation = Nothing
		End If
	End Function
	
	'**% Add: Adds the relationships of an specific client
	'% Add: Agrega los nexos correspondientes para un cliente en específico.
	Public Function Add() As Boolean
		Dim lreccreRelation As eRemoteDB.Execute
		Dim lrecTab_Relat As eRemoteDB.Execute
		Dim lclsClientWin As eClient.ClientWin
		
		lreccreRelation = New eRemoteDB.Execute
		lrecTab_Relat = New eRemoteDB.Execute
		lclsClientWin = New eClient.ClientWin
		
		With lreccreRelation
			.StoredProcedure = "insRelations"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'**+ Search if there is the inverse relationship to create it automaticaly
		'+ Determina si existe la relación inversa para ser creada automáticamente.
		lrecTab_Relat.StoredProcedure = "reaTab_relat"
		lrecTab_Relat.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If lrecTab_Relat.Run Then
			With lreccreRelation
				.StoredProcedure = "insRelations"
				.Parameters.Add("sClient", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClientr", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRelaship", lrecTab_Relat.FieldToClass("nRel_target"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'**+ If the inverse relationship was created the system updates the relationship window of the related client.
				'+ Si la relación inversa es creada se actualiza la ventana de nexos del sujeto relacionado.
				If .Run(False) Then
					Call lclsClientWin.insUpdClient_win(sClientr, "BC002", "2",  ,  , nUsercode)
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecTab_Relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Relat = Nothing
		'UPGRADE_NOTE: Object lreccreRelation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreRelation = Nothing
		'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClientWin = Nothing
	End Function
	
	'**% Update: Updates the relationships of an specific client
	'% Update: Actualiza los nexos para un cliente en específico.
	Public Function Update() As Boolean
		Dim lrecupdRelation As eRemoteDB.Execute
		
		lrecupdRelation = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.insRelations'
		'**+Data of 10/11/2000 9:24
		'+ Definición de parámetros para stored procedure 'insudb.insRelations'
		'+ Información leída el 11/10/2000 9:24
		
		'**+ nType : Defines the type of process to be executed in the Stored procedure (0:Insert else Update)
		'+ nType : Define el tipo de proceso a ejecutar en el SP (0:Insert else Update)
		
		With lrecupdRelation
			.StoredProcedure = "insRelations"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdRelation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdRelation = Nothing
		
	End Function
	
	'**% Delete: Deletes the data of a specific client
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Dim lrecdelRelation As eRemoteDB.Execute
		Dim lrecTab_Relat As eRemoteDB.Execute
		
		lrecdelRelation = New eRemoteDB.Execute
		lrecTab_Relat = New eRemoteDB.Execute
		
		'**+ Gets the result of the delete of the inverse relationship
		'+ Obtiene el resutado de la eliminación inversa
		Dim lblnAux As Boolean
		
		'**+Stored procedure parameters definition 'insudb.delrelations'
		'**+Data of 10/10/2000 16:56:00
		'+ Definición de parámetros para stored procedure 'insudb.delrelations'
		'+ Información leída el 10/10/2000 16:56:00
		
		'**+Deletes the relationship
		'+ Elimina la relación
		With lrecdelRelation
			.StoredProcedure = "delrelations"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		'**+ The system verifies if there is a inverse relationship to delete it
		'+ Se verifica la existencia de una relación inversa para ser esta eliminada.
		lrecTab_Relat.StoredProcedure = "reaTab_relat"
		lrecTab_Relat.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If lrecTab_Relat.Run Then
			With lrecdelRelation
				.StoredProcedure = "delrelations"
				.Parameters.Add("sClient", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClientr", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRelaship", lrecTab_Relat.FieldToClass("nRel_target"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lblnAux = .Run(False)
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecdelRelation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelRelation = Nothing
		'UPGRADE_NOTE: Object lrecTab_Relat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Relat = Nothing
	End Function
	
	'**% Delete_Relations: This function deletes the relationships of the clients
	'% Delete_Relations: Esta funcion se encarga de realizar la eliminacion de las relaciones de los clientes
	Public Function Delete_Relations(ByVal sClient As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsClientWin As ClientWin
		
		'**- Recordset definition. This recordset is used to read the table of relationships
		'-Se define el recordset para realizar la lectura de la tabla de relaciones
		Dim lrecRelations As eRemoteDB.Execute
		
		'-Se define variable para realizar operaciones a la BD
		Dim ldelRelations As eRemoteDB.Execute
		
		'**-Recordset definition. This record is used to read the related client
		'-Se define el recordset para realizar la lectura del cliente relacionado
		Dim lrecRelations_2 As eRemoteDB.Execute
		
		On Error GoTo Delete_Relations_Err
		
		lrecRelations = New eRemoteDB.Execute
		ldelRelations = New eRemoteDB.Execute
		lrecRelations_2 = New eRemoteDB.Execute
		lclsClientWin = New ClientWin
		Delete_Relations = True
		With lrecRelations
			.StoredProcedure = "reaRelations"
			.Parameters.Add("sClient", sClient)
			.Parameters.Add("sClientR", String.Empty)
			.Parameters.Add("nRelaship", 0)
			If .Run Then
				Do While Not .EOF
					ldelRelations.StoredProcedure = "delRelations"
					ldelRelations.Parameters.Add("sClient", .FieldToClass("sClientr"))
					ldelRelations.Parameters.Add("sClientr", sClient)
					ldelRelations.Parameters.Add("nRelaship", 0)
					If ldelRelations.Run(False) Then
						lrecRelations_2.StoredProcedure = "reaRelations"
						lrecRelations_2.Parameters.Add("sClient", .FieldToClass("sClientr"))
						If Not lrecRelations_2.Run Then
							If lrecRelations_2.ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound Then
								Delete_Relations = lclsClientWin.insUpdClient_win(.FieldToClass("sClientr"), "BC002", "1",  ,  , nUsercode)
							End If
						End If
						ldelRelations.StoredProcedure = "delRelations"
						ldelRelations.Parameters.Add("sClient", sClient)
						ldelRelations.Parameters.Add("sClientr", .FieldToClass("sClientr"))
						ldelRelations.Parameters.Add("nRelaship", 0)
						If Not ldelRelations.Run(False) Then
							Delete_Relations = False
							Exit Do
						End If
					Else
						Delete_Relations = False
						Exit Do
					End If
					.RNext()
				Loop 
			End If
		End With
		
Delete_Relations_Err: 
		If Err.Number Then
			Delete_Relations = False
		End If
		'UPGRADE_NOTE: Object lrecRelations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRelations = Nothing
		'UPGRADE_NOTE: Object ldelRelations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelRelations = Nothing
		'UPGRADE_NOTE: Object lrecRelations_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRelations_2 = Nothing
		'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClientWin = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_by_Relaship: Busca el cliente por relación
	Public Function Find_by_Relaship(ByVal sClient As String, ByVal nRelaship As Integer) As Boolean
		Dim lrecreaRelations As eRemoteDB.Execute
		
		On Error GoTo Find_by_Relaship_Err
		lrecreaRelations = New eRemoteDB.Execute
		
		With lrecreaRelations
			.StoredProcedure = "ReaRelations_by_relaship"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_by_Relaship = True
				Me.sClient = .FieldToClass("sClient")
				Me.nRelaship = .FieldToClass("nRelaship")
				sClientr = .FieldToClass("sClientr")
				sCliename = .FieldToClass("sCliename")
			End If
		End With
		
Find_by_Relaship_Err: 
		If Err.Number Then
			Find_by_Relaship = False
		End If
		'UPGRADE_NOTE: Object lrecreaRelations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRelations = Nothing
		On Error GoTo 0
	End Function
	
	'**% Class_Initialize: This routine controls the opening of each instance of the class
	'% Class_Initialize: el objetivo de esta rutina es la de controlar la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sClient = String.Empty
		sClientr = String.Empty
		nRelaship = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nType = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






