Option Strict Off
Option Explicit On
Public Class Reval_fact
	'%-------------------------------------------------------%'
	'% $Workfile:: Reval_fact.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'*+Properties according to the table 'Reval_fact' in the system 21/08/2002 04:25:17 p.m.
	'+ Propiedades según la tabla 'Reval_fact' en el sistema 21/08/2002 04:25:17 p.m.
	
	'Column_name                   Type       Computed   Length   Prec Scale  Nullable   TrimTrailingBlanks   FixedLenNullInSource  Collation
	'---------------------------- ---------- ---------- -------- ----- ----- ---------- -------------------- --------------------- ----------
	Public nEcon_area As Integer 'smallint     no         2       5     0     no               (n/a)               (n/a)             NULL
	Public nYear As Integer 'smallint     no         2       5     0     no               (n/a)               (n/a)             NULL
	Public nMonth As Integer 'smallint     no         2       5     0     no               (n/a)               (n/a)             NULL
	Public nIndexfac As Double 'decimal      no         5       5     2     yes              (n/a)               (n/a)             NULL
	Public nUsercode As Integer 'integer      no         2       5     0     no               (n/a)               (n/a)             NULL
	
	'% Add: Agrega un registro a la tabla "Reval_fact"
	Public Function Add() As Boolean
		Dim lclsReval_fact As eRemoteDB.Execute
		
		On Error GoTo AddMS012_Err
		
		lclsReval_fact = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creReval_fact'. Generated on 21/08/2002 04:25:17 p.m.
		
		With lclsReval_fact
			.StoredProcedure = "creReval_fact"
			.Parameters.Add("nEcon_area", nEcon_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndexfac", nIndexfac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
AddMS012_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReval_fact = Nothing
	End Function
	
	'% Update: Actualiza un registro a la tabla "Reval_fact" usando la clave para dicha tabla.
	Public Function Update() As Boolean
		Dim lclsReval_fact As eRemoteDB.Execute
		
		On Error GoTo UpdateMS012_Err
		
		lclsReval_fact = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updReval_fact'. Generated on 21/08/2002 04:25:17 p.m.
		With lclsReval_fact
			.StoredProcedure = "updReval_fact"
			.Parameters.Add("nEcon_area", nEcon_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndexfac", nIndexfac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
UpdateMS012_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReval_fact = Nothing
	End Function
	
	
	'% Delete: Elimina un registro a la tabla "Reval_fact" usando la clave para dicha tabla.
	Public Function Delete() As Boolean
		Dim lclsReval_fact As eRemoteDB.Execute
		
		On Error GoTo DeleteMS012_Err
		
		lclsReval_fact = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delReval_fact'. Generated on 21/08/2002 04:25:17 p.m.
		With lclsReval_fact
			.StoredProcedure = "delReval_fact"
			.Parameters.Add("nEcon_area", nEcon_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
DeleteMS012_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReval_fact = Nothing
	End Function
	
	'% IsExist: Verifica la existencia de un registro en la tabla "Reval_fact" usando la clave de dicha tabla.
	Public Function IsExist(ByVal nEcon_area As Integer, ByVal nYear As Integer, ByVal nMonth As Integer) As Boolean
		Dim lclsReval_fact As eRemoteDB.Execute
		
		On Error GoTo IsExistMS012_Err
		
		lclsReval_fact = New eRemoteDB.Execute
		
		If nMonth = eRemoteDB.Constants.intNull Then
			nMonth = nYear
		End If
		
		'+ Define all parameters for the stored procedures 'insudb.valReval_factExist'. Generated on 21/08/2002 04:25:17 p.m.
		
		With lclsReval_fact
			.StoredProcedure = "reaReval_factmonth"
			.Parameters.Add("nEcon_area", nEcon_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			IsExist = .Run
		End With
		
IsExistMS012_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReval_fact = Nothing
	End Function
	
	'% InsValMS012_k: Validación de los datos para la página del encabezado.
	Public Function InsValMS012_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nEcon_area As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolReval_facts As Reval_facts
		
		On Error GoTo InsValMS012_k_Err
		
		lclsErrors = New eFunctions.Errors
		lcolReval_facts = New Reval_facts
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			If nEcon_area = eRemoteDB.Constants.intNull Or nEcon_area = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Area económica")
			End If
		Else
			If Not lcolReval_facts.Find(nEcon_area) Then
				Call lclsErrors.ErrorMessage(sCodispl, 11240)
			End If
		End If
		
		InsValMS012_k = lclsErrors.Confirm
		
InsValMS012_k_Err: 
		If Err.Number Then
			InsValMS012_k = InsValMS012_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lcolReval_facts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolReval_facts = Nothing
	End Function
	
	'% InsValMS012: Validación de los datos para la página detalle.
	Public Function InsValMS012(ByVal sCodispl As String, ByVal sAction As String, ByVal nEcon_area As Integer, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nIndexfac As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		Dim lintYear As Integer
		Dim lblnError As Boolean
		
		On Error GoTo InsValMS012_Err
		
		lblnError = False
		
		lclsErrors = New eFunctions.Errors
		If nYear = eRemoteDB.Constants.intNull Or nYear = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "El año")
			lblnError = True
		End If
		If nMonth = eRemoteDB.Constants.intNull Or nMonth = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "El mes")
			lblnError = True
		End If
		
		If nIndexfac = eRemoteDB.Constants.intNull Or nIndexfac = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10218)
			lblnError = True
		End If
		
		If Not lblnError And sAction = "Add" Then
			If IsExist(nEcon_area, nYear, nMonth) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10217)
			End If
		End If
		
		InsValMS012 = lclsErrors.Confirm
		
InsValMS012_Err: 
		If Err.Number Then
			InsValMS012 = InsValMS012 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsPostMS012: Pase de la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostMS012(ByVal bHeader As Boolean, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nEcon_area As Integer, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nIndexfac As Double) As Boolean
		
		If bHeader Then
			InsPostMS012 = True
		Else
			With Me
				.nYear = nYear
				.nMonth = nMonth
				.nUsercode = nUsercode
				.nEcon_area = nEcon_area
				.nIndexfac = nIndexfac
				
				If nMonth = eRemoteDB.Constants.intNull Then
					nMonth = nYear
				End If
				If sAction = "Add" Then
					InsPostMS012 = .Add
				ElseIf sAction = "Update" Then 
					InsPostMS012 = .Update
				ElseIf sAction = "Del" Then 
					InsPostMS012 = .Delete
				End If
			End With
		End If
	End Function
End Class






