Option Strict Off
Option Explicit On
Public Class Det_liness
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_liness.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Local variable of the collection
	'-   variable local para contener colección
	
	Private mCol As Collection
	
	'**% Add: This method allow to add items of the class Det_lines to the colection
	'%   Add: Añade una nueva instancia de la clase Det_lines a la colección
	Public Function Add(ByVal nUsercode As Integer, ByVal nParameter As Integer, ByVal nLine_Type As Integer, ByVal nComplement As Integer, ByVal dCompdate As Date, ByVal sAux_accoun As String, ByVal nConsec As Integer, ByVal sAccount As String, ByVal nTyp_acco As Integer, ByVal sPay_type As String, ByVal nProduct_ty As Integer, ByVal nReceipt_ty As Integer, ByVal nTratypei As Integer, ByVal nTransac_Ty As Integer, ByVal nArea_Led As Integer, ByVal nLed_compan As Integer, ByVal sDescript As String, ByVal sPay_form As String) As Det_lines
		Dim objNewMember As Det_lines
		
		objNewMember = New Det_lines
		
		With objNewMember
			.nUsercode = nUsercode
			.nParameter = nParameter
			.nLine_Type = nLine_Type
			.nComplement = nComplement
			.dCompdate = dCompdate
			.sAux_accoun = sAux_accoun
			.nConsec = nConsec
			.sAccount = sAccount
			.nTyp_acco = nTyp_acco
			.sPay_type = sPay_type
			.nProduct_ty = nProduct_ty
			.nReceipt_ty = nReceipt_ty
			.nTratypei = nTratypei
			.nTransac_Ty = nTransac_Ty
			.nArea_Led = nArea_Led
			.nLed_compan = nLed_compan
			.sDescript = sDescript
			.sPay_form = sPay_form
		End With
		
		mCol.Add(objNewMember, "A" & Trim(CStr(nLed_compan)) & Trim(CStr(nArea_Led)) & Trim(CStr(nTransac_Ty)) & Trim(CStr(nTratypei)) & Trim(CStr(nReceipt_ty)) & Trim(CStr(nProduct_ty)) & sPay_type & Trim(CStr(nTyp_acco)) & Trim(CStr(nConsec)))
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Item: This method allows to find a element in the collection
	'%   Item: Permite buscar un elemento en la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Det_lines
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: This method allows to count the elements of the collection
	'%   Count: Permite contar los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: This method allows to enumarate the elements of the collection
	'%   NewEnum: Permite enumerar los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: This method allows to remove a element in the collection
	'%   Remove: Permite eliminar un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: This method allows to generate the collection from the class
	'%   Class_Initialize: Crea la colección cuando se crea la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: This method allows to destroy the collection when the class down
	'%   Class_Terminate: Destruye la colección cuando se termina la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Find: return a objects collection type Det_lines
	'%   Find: Devuelve una coleccion de objetos de tipo Det_lines
	Public Function Find(ByVal nArea As Integer, ByVal nTransac_Ty As Integer, ByVal nTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer) As Boolean
		
		'**- Define the variable lrecDet_lines
		'-   Se define la variable lrecDet_lines
		
		Dim lstrPay_type As String
		Dim lrecDet_lines As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecDet_lines = New eRemoteDB.Execute
		
		Find = False
		
		If sPay_type = String.Empty Then
			lstrPay_type = "0"
		Else
			lstrPay_type = sPay_type
		End If
		
		'**+ Parameters definition for the stored procedure 'insudb.reaDet_lines'
		'+   Definicion de parametros para stored procedure 'insudb.reaDet_lines'
		
		With lrecDet_lines
			.StoredProcedure = "reaDet_Lines"
			
			.Parameters.Add("nAreaLed", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", lstrPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nConsec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nUsercode"), .FieldToClass("nParameter"), .FieldToClass("nLine_type"), .FieldToClass("nComplement"), .FieldToClass("dCompdate"), .FieldToClass("sAux_accoun"), .FieldToClass("nConsec"), .FieldToClass("sAccount"), .FieldToClass("nTyp_acco"), .FieldToClass("sPay_type"), .FieldToClass("nProduct_ty"), .FieldToClass("nReceipt_ty"), .FieldToClass("nTratypei"), .FieldToClass("nTransac_ty"), .FieldToClass("nArea_led"), .FieldToClass("nLed_compan"), .FieldToClass("sDescript"), .FieldToClass("sPay_form"))
					.RNext()
				Loop 
				
				.RCloseRec()
				Find = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecDet_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_lines = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
End Class






