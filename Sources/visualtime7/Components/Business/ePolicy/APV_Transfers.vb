Option Strict Off
Option Explicit On
Public Class APV_Transfers
	Implements System.Collections.IEnumerable
	'- Variable que almacena la colección
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
    Public Function Add(ByVal dNulldate As Date, ByVal nAmount_UF As Double, ByVal nAmount_Peso As Double, ByVal nType_transf As Integer, ByVal nOrigin As Integer, ByVal nInstitution As Integer, ByVal dEffecdate As Date, ByVal nCertif As Double, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal sCertype As String, ByVal nTyp_ProfitWorker As Short) As APV_Transfer

        Dim objNewMember As APV_Transfer

        objNewMember = New APV_Transfer

        objNewMember.dNulldate = dNulldate
        objNewMember.nAmount_UF = nAmount_UF
        objNewMember.nAmount_Peso = nAmount_Peso
        objNewMember.nType_transf = nType_transf
        objNewMember.nOrigin = nOrigin
        objNewMember.nInstitution = nInstitution
        objNewMember.dEffecdate = dEffecdate
        objNewMember.nCertif = nCertif
        objNewMember.nPolicy = nPolicy
        objNewMember.nProduct = nProduct
        objNewMember.nBranch = nBranch
        objNewMember.sCertype = sCertype
        objNewMember.nTyp_ProfitWorker = nTyp_ProfitWorker

        mCol.Add(objNewMember, sCertype & RTrim(CStr(nBranch)) & RTrim(CStr(nProduct)) & RTrim(CStr(nPolicy)) & RTrim(CStr(nCertif)) & RTrim(CStr(dEffecdate)) & RTrim(CStr(nInstitution)) & RTrim(CStr(nOrigin)) & RTrim(CStr(nTyp_ProfitWorker)))

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As APV_Transfer
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'* Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Lee los registros de la tabla APV_Transfer
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nInstitution As Integer, ByVal nOrigin As Integer, ByVal nTyp_ProfitWorker As Integer) As Boolean
		Dim lrecreaAPV_Transfer_a As eRemoteDB.Execute
		
		lrecreaAPV_Transfer_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecreaAPV_Transfer_a
			.StoredProcedure = "reaAPV_Transfer"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_ProfitWorker", nTyp_ProfitWorker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				
				Do While Not .EOF
					Call Add(.FieldToClass("dNulldate"), .FieldToClass("nAmount_UF"), .FieldToClass("nAmount_Peso"), .FieldToClass("nType_Transf"), .FieldToClass("nOrigin"), .FieldToClass("nInstitution"), .FieldToClass("dEffecdate"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("sCertype"), .FieldToClass("nTyp_ProfitWorker"))
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaAPV_Transfer_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAPV_Transfer_a = Nothing
		
		On Error GoTo 0
	End Function
End Class






