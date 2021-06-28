Option Strict Off
Option Explicit On
Public Class Tab_req_doc
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_req_doc.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on November 09,2000
	'+ Propiedades según la tabla en el sistema 09/11/2000
	
	'+ Column_name                                Type        Computed  Length  Prec  Scale Nullable                          TrimTrailingBlanks                  FixedLenNullInSource
	'-------------------------------------------- ----------- --------- ------- ----- ----- --------------------------------- ----------------------------------- -----------------------------------
	Public nTypedoc As Integer 'smallint     no        5           5     0     no                                  (n/a)                               (n/a)
	Public sRequire As String 'char         no        1                       No
	Public nQDays As Integer 'smallint     no        5           5     0     No                                 (n/a)                               (n/a)
	Public sStatregt As String 'char         no        1                       No                                  no                                  yes
	Public nCost As Double 'smallint     no        12          2     0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer ' NUMBER   22   0     5    N
	
	
	'% Find: Busca unregistro en tab_req_doc
	Public Function Find(ByVal nTypedoc As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		
		'- Se define variable para realizar operaciones a la BD
		Dim lrecreaTab_req_doc As eRemoteDB.Execute
		
		If nTypedoc = Me.nTypedoc And Not bFind Then
			Find = True
		Else
			lrecreaTab_req_doc = New eRemoteDB.Execute
			
			With lrecreaTab_req_doc
				.StoredProcedure = "reatab_req_doc"
				.Parameters.Add("nTypeDoc", nTypedoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nTypedoc = .FieldToClass("nTypeDoc")
					Me.sRequire = .FieldToClass("sRequire")
					Me.nQDays = .FieldToClass("nQDays")
					Me.sStatregt = .FieldToClass("sStatregt")
					Me.nCost = .FieldToClass("nCost")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaTab_req_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaTab_req_doc = Nothing
		End If
		
	End Function
	
	'% Update: Actualiza los datos de la tabla tab_req_doc
	Public Function Update(ByVal nTypedoc As Integer, ByVal sRequire As String, ByVal nQDays As Integer, ByVal nCost As Double, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		
		'- Se define variable para realizar operaciones a la BD
		Dim lrecTab_req_doc As eRemoteDB.Execute
		
		lrecTab_req_doc = New eRemoteDB.Execute
		
		With lrecTab_req_doc
			.StoredProcedure = "updtab_req_doc"
			.Parameters.Add("nTypeDoc", nTypedoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQDays", nQDays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCost", nCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecTab_req_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_req_doc = Nothing
	End Function
	
	
	'% Delete: Borra los datos de la tabla tab_req_doc
	Public Function Delete(ByVal nTypedoc As Integer) As Boolean
		
		'- Se define variable para realizar operaciones a la BD
		Dim lrecTab_req_doc As eRemoteDB.Execute
		
		lrecTab_req_doc = New eRemoteDB.Execute
		
		With lrecTab_req_doc
			.StoredProcedure = "deltab_req_doc"
			.Parameters.Add("nTypeDoc", nTypedoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecTab_req_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_req_doc = Nothing
		
	End Function
	
	'% Add: Agrega los datos de la tabla tab_req_doc
	Public Function Add(ByVal nTypedoc As Integer, ByVal sRequire As String, ByVal nQDays As Integer, ByVal sStatregt As String, ByVal nCost As Double, ByVal nUsercode As Integer) As Boolean
		
		'- Se define variable para realizar operaciones a la BD
		Dim lrecCretab_req_doc As eRemoteDB.Execute
		
		lrecCretab_req_doc = New eRemoteDB.Execute
		
		With lrecCretab_req_doc
			.StoredProcedure = "cretab_req_doc"
			.Parameters.Add("nTypeDoc", nTypedoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQDays", nQDays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCost", nCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecCretab_req_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCretab_req_doc = Nothing
	End Function
	
	'% insValMBC667: Realiza las validaciones de la transaccion
	Public Function insValMBC667(ByVal sCodispl As String, ByVal sAction As String, ByVal nTypedoc As Integer, ByVal sRequire As String, ByVal nQDays As Integer, ByVal sStatregt As String, ByVal nCost As Double) As String
		
		'- Se define variable que instancia al DLL
		Dim lclsTab_req_doc As eClient.Tab_req_doc
		
		'- Se define variable que instancia al DLL
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMBC667_Err
		
		lclsTab_req_doc = New eClient.Tab_req_doc
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Tipo Documento debe tener valor
			If nTypedoc = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1925)
			Else
				If sAction = "Add" And Find(nTypedoc) Then
					.ErrorMessage(sCodispl, 10259)
				End If
			End If
			
			insValMBC667 = .Confirm
			
		End With
		
insValMBC667_Err: 
		If Err.Number Then
			insValMBC667 = insValMBC667 & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_req_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_req_doc = Nothing
		
	End Function
	
	'% insPostMBC667: Crea/actualiza los registros correspondientes en la tabla de documentos a solicitar
	Public Function insPostMBC667(ByVal sAction As String, ByVal nTypedoc As Integer, ByVal sRequire As String, ByVal nQDays As Integer, ByVal sStatregt As String, ByVal nCost As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMBC667_Err
		With Me
			.nTypedoc = nTypedoc
			.sRequire = sRequire
			.nQDays = nQDays
			.sStatregt = sStatregt
			.nCost = nCost
			.nUsercode = nUsercode
		End With
		
		insPostMBC667 = True
		
		If (sAction = "Add") Then
			insPostMBC667 = Add(nTypedoc, IIf(Trim(sRequire) = "1", "1", "2"), nQDays, sStatregt, nCost, nUsercode)
		Else
			If (sAction = "Update") Then
				insPostMBC667 = Update(nTypedoc, IIf(Trim(sRequire) = "1", "1", "2"), nQDays, nCost, sStatregt, nUsercode)
			Else
				insPostMBC667 = Delete(nTypedoc)
			End If
		End If
		
insPostMBC667_Err: 
		If Err.Number Then
			insPostMBC667 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Class_Terminate: elimina la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nTypedoc = eRemoteDB.Constants.intNull
		nQDays = eRemoteDB.Constants.intNull
		sRequire = String.Empty
		sStatregt = String.Empty
		nCost = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






