Option Strict Off
Option Explicit On
Public Class eval_masters
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: eval_masters.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As eval_master) As eval_master
		If objClass Is Nothing Then
			objClass = New eval_master
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nEval)
			
		End With
		'Return the object created
		Add = objClass
		
	End Function
	
	'% Find: Lee los datos de la tabla
	Public Function Find(ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaEval_master As eRemoteDB.Execute
		Dim lclsEval_master As eval_master
		
		On Error GoTo reaEval_master_Err
		lrecreaEval_master = New eRemoteDB.Execute
		
		With lrecreaEval_master
			.StoredProcedure = "reaEval_master"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", npolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", ncertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsEval_master = New eval_master
					lclsEval_master.nEval = .FieldToClass("nEval")
					lclsEval_master.sClient = .FieldToClass("sClient")
					lclsEval_master.nBranch = .FieldToClass("nBranch")
					lclsEval_master.nProduct = .FieldToClass("nProduct")
					lclsEval_master.npolicy = .FieldToClass("nPolicy")
					lclsEval_master.dStartdate = .FieldToClass("dStartdate")
					lclsEval_master.ncertif = .FieldToClass("nCertif")
					lclsEval_master.dExpirdat = .FieldToClass("dExpirdat")
					lclsEval_master.nStatus_eval = .FieldToClass("nStatus_eval")
					lclsEval_master.nCapital = .FieldToClass("nCapital")
					lclsEval_master.nNoterest = .FieldToClass("nNoterest")
					lclsEval_master.nCurrency = .FieldToClass("nCurrency")
					lclsEval_master.nCumul = .FieldToClass("nCumul")
					lclsEval_master.nUsercode = .FieldToClass("nUsercode")
					lclsEval_master.sCertype = .FieldToClass("sCertype")
					lclsEval_master.sExist = .FieldToClass("sExist")
					Call Add(lclsEval_master)
					'UPGRADE_NOTE: Object lclsEval_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsEval_master = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaEval_master_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaEval_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaEval_master = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As eval_master
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: enumera los elementos dentro de la colección
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
	
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: elimina la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






