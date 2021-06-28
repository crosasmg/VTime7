Option Strict Off
Option Explicit On
Public Class Financ_Cli
	'%-------------------------------------------------------%'
	'% $Workfile:: Financ_Cli.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+Properties according to the table in the system 01/11/2000
	'**+ The key fields are sClient, dFinanDate y  nConcept
	'+ Propiedades según la tabla en el sistema 11/01/2000
	'+ Los campos llaves corresponden a sClient, dFinanDate y  nConcept
	
	'+ Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public sClient As String 'char      no        14                 no       yes                no
	Public dFinanDate As Date 'datetime  no        8                  no       (n/a)              (n/a)
	Public nConcept As Integer 'int       no        4      10    0     no       (n/a)              (n/a)
	Public nUnits As Double 'decimal   no        9      10    2     yes      (n/a)              (n/a)
	Public nUsercode As Integer ' NUMBER   22   0     5    N
	Public nNotenum As Integer 'int       no        4      10    0     yes      (n/a)              (n/a)
	Public nCurrency As Integer 'smallint  no        2      5     0     yes      (n/a)              (n/a)
	Public nAmount As Double 'decimal   no        9      10    2     yes      (n/a)              (n/a)
	Public nFinanStat As Integer 'smallint  no        2      5     0     yes      (n/a)              (n/a)
	
	'**+ Additional properties
	'+ Propiedades auxiliares
	Public sCliename As String 'Client description
	'Descripción del cliente
	Public sConcept As String 'Financing information concept description (see table416)
	'Descripción del concepto de información financiera (ver table416)
	
	'**Variable definition. This variable contains the status of each instance of the class
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	
	Public nStatusInstance As Integer
	
	'**% Find: This function searches for the data of a client, year and specific concept
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find(ByVal sClient As String, ByVal dFinanDate As Date, ByVal nConcept As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaFinanc_cli As eRemoteDB.Execute
		
		lrecreaFinanc_cli = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If sClient = Me.sClient And dFinanDate = Me.dFinanDate And nConcept = Me.nConcept And Not bFind Then
			Find = True
		Else
			With lrecreaFinanc_cli
				.StoredProcedure = "reaFinanc_cli"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dFinanDate", dFinanDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sClient = .FieldToClass("sClient")
					dFinanDate = .FieldToClass("dFinanDate")
					nConcept = .FieldToClass("nConcept")
					nUnits = .FieldToClass("nUnits")
					nNotenum = .FieldToClass("nNotenum")
					nCurrency = .FieldToClass("nCurrency")
					nAmount = .FieldToClass("nAmount")
					nFinanStat = .FieldToClass("nFinanStat")
					sConcept = .FieldToClass("sDescript")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaFinanc_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaFinanc_cli = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Add: Adds the data of a client, year and specific concept
	'% Add: Agrega los datos correspondientes para un cliente, año y concepto específico
	Public Function Add() As Boolean
		Dim lreccreFinanc_cli As eRemoteDB.Execute
		
		lreccreFinanc_cli = New eRemoteDB.Execute
		On Error GoTo Add_Err
		
		With lreccreFinanc_cli
			.StoredProcedure = "creFinanc_cli"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFinanDate", dFinanDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnits", nUnits, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFinanStat", nFinanStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreFinanc_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFinanc_cli = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This function updates the data of a client, year and specific concept
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Dim lrecupdFinanc_cli As eRemoteDB.Execute
		
		lrecupdFinanc_cli = New eRemoteDB.Execute
		On Error GoTo Update_Err
		
		With lrecupdFinanc_cli
			.StoredProcedure = "updFinanc_cli"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFinanDate", dFinanDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnits", nUnits, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFinanStat", nFinanStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdFinanc_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFinanc_cli = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Delete: This function deletes the data of a client, year and specific concept
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Dim lrecdelFinanc_cli As eRemoteDB.Execute
		
		lrecdelFinanc_cli = New eRemoteDB.Execute
		On Error GoTo Delete_Err
		
		With lrecdelFinanc_cli
			.StoredProcedure = "delFinanc_cli"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFinanDate", dFinanDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelFinanc_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFinanc_cli = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Find_Creditline: This funtion searches for the credit limit amount of a client
	'% Find_Creditline: busca el importe correspondiente al límite de crédito del cliente
	Public Function Find_Creditline(ByVal sClient As String) As Boolean
		Dim lrecreaCreditLine_sclient As eRemoteDB.Execute
		
		lrecreaCreditLine_sclient = New eRemoteDB.Execute
		On Error GoTo Find_Creditline_Err
		
		With lrecreaCreditLine_sclient
			.StoredProcedure = "reaCreditLine_sclient"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nAmount = .FieldToClass("nAmount")
				.RCloseRec()
				Find_Creditline = True
			Else
				Find_Creditline = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCreditLine_sclient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCreditLine_sclient = Nothing
		
Find_Creditline_Err: 
		If Err.Number Then
			Find_Creditline = False
		End If
		On Error GoTo 0
	End Function
End Class






