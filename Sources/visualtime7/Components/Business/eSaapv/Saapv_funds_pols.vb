Option Strict Off
Option Explicit On
Public Class Saapv_funds_pols
	Implements System.Collections.IEnumerable
	
	
	Private mCol As Collection
	
	Public nCount As Integer
	
	Public Function Add(ByVal nCod_saapv As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nFunds As Integer, ByVal dEffecdate As Date, ByVal nBuy_cost As Double, ByVal dNulldate As Date, ByVal nPartic_min As Double, ByVal nParticip As Double, ByVal nSell_cost As Double, ByVal sDescript As String, ByVal nOrigin As Integer, ByVal sDesOrigin As String, ByVal nIntproy As Double, ByVal nIntproyvar As Double, ByVal sGuarantee As String, ByVal sSel As String, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nQuan_avail As Double, ByVal sReaddress As String, ByVal sActivfound As String, ByVal nInstitution As Integer) As Saapv_funds_pol
		Dim objNewMember As Saapv_funds_pol
		
		On Error GoTo ErrorHandler
		
		objNewMember = New Saapv_funds_pol
		
		With objNewMember
			.nCod_saapv = nCod_saapv
			.nBranch = nBranch
			.nProduct = nProduct
			.nFunds = nFunds
			.dEffecdate = dEffecdate
			.nBuy_cost = nBuy_cost
			.dNulldate = dNulldate
			.nPartic_min = nPartic_min
			.nParticip = nParticip
			.nSell_cost = nSell_cost
			.sDescript = sDescript
			.nOrigin = nOrigin
			.sDesOrigin = sDesOrigin
			.nIntproy = nIntproy
			.nIntproyvar = nIntproyvar
			.sGuarantee = sGuarantee
			.sSel = sSel
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nQuan_avail = nQuan_avail
			.sReaddress = sReaddress
			.sActivfound = sActivfound
			.nInstitution = nInstitution
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		
		objNewMember = Nothing
		
		Exit Function
ErrorHandler: 
		objNewMember = Nothing
		Add = Nothing
	End Function
	
	Public Function Find(ByVal nCod_saapv As Double, ByVal nOrigin As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sVigen As String, Optional ByVal nUsercode As Integer = 0, Optional ByVal nInstitution As Integer = 0) As Boolean
		'+ Se define la variable lrecSaapv_funds_pols que se utilizará como cursor.
		Dim lrecReaSaapv_funds_pols As eRemoteDB.Execute
		
		lrecReaSaapv_funds_pols = New eRemoteDB.Execute
		
		'Find = True
		
		With lrecReaSaapv_funds_pols
			.StoredProcedure = "insVI7501_G_pkg.ReaSaapv_funds_pol"
			
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", sVigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find = .Run
			
			If Find Then
				Do While Not .EOF
					
					Call Add(.FieldToClass("nCod_saapv"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nFunds"), .FieldToClass("dEffecdate"), .FieldToClass("nBuy_cost"), .FieldToClass("dNulldate"), .FieldToClass("nPartic_min"), .FieldToClass("nParticip"), .FieldToClass("nSell_cost"), .FieldToClass("sDescript"), .FieldToClass("nOrigin"), .FieldToClass("sDesOrigin"), .FieldToClass("nIntproy"), .FieldToClass("nIntproyvar"), .FieldToClass("sGuarantee"), .FieldToClass("sSel"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nQuan_avail"), .FieldToClass("sReaddress"), .FieldToClass("sActivfound"), .FieldToClass("nInstitution"))
					
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		lrecReaSaapv_funds_pols = Nothing
		
		Exit Function
ErrorHandler: 
		lrecReaSaapv_funds_pols = Nothing
		Find = False
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Saapv_funds_pol
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
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






