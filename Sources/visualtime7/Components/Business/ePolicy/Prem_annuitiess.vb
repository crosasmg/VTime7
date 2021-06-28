Option Strict Off
Option Explicit On
Public Class Prem_annuitiess
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Prem_annuitiess.cls                      $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'-Variable que guarda la prima básica
	Public nPremiumbas As Object
	
	'% Add: Este método permite añadir registros a la colección.
	Public Function Add(ByRef objClass As Prem_annuities) As Prem_annuities
		If objClass Is Nothing Then
			objClass = New Prem_annuities
		End If
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nId & .nReceipt & .nCurrency)
			
		End With
		
		'retorna el objeto creado
		Add = objClass
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Prem_annuities
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
	
	'%Find: Lee los datos particulares de rentas vitalicias
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaPrem_annuities As eRemoteDB.Execute
		Dim lclsPrem_annuities As Prem_annuities
		
		On Error GoTo Find_Err
		lrecreaPrem_annuities = New eRemoteDB.Execute
		'+Definición de parámetros para stored procedure 'insudb.reaPrem_annuities_a'
		'+Información leída el 08/07/2002
		With lrecreaPrem_annuities
			.StoredProcedure = "reaPrem_annuities_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsPrem_annuities = New Prem_annuities
					lclsPrem_annuities.sCertype = sCertype
					lclsPrem_annuities.nBranch = nBranch
					lclsPrem_annuities.nProduct = nProduct
					lclsPrem_annuities.nPolicy = nPolicy
					lclsPrem_annuities.nCertif = nCertif
					lclsPrem_annuities.nReceipt = .FieldToClass("nReceipt")
					lclsPrem_annuities.nIndrecdep = .FieldToClass("nIndrecdep")
					lclsPrem_annuities.nPrem_quot = .FieldToClass("nPrem_quot")
					lclsPrem_annuities.nRate_disc = .FieldToClass("nRate_disc")
					lclsPrem_annuities.nNom_valbon = .FieldToClass("nNom_valbon")
					lclsPrem_annuities.dIssuedatbon = .FieldToClass("dIssuedatbon")
					lclsPrem_annuities.dExpirdatbon = .FieldToClass("dExpirdatbon")
					lclsPrem_annuities.nCurrency = .FieldToClass("nCurrency")
					lclsPrem_annuities.nId = .FieldToClass("nId")
					Call Add(lclsPrem_annuities)
					'UPGRADE_NOTE: Object lclsPrem_annuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPrem_annuities = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPrem_annuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPrem_annuities = Nothing
	End Function
	
	'%InsPreRV778: Obtiene los datos particulares de rentas vitalicias
	Public Function InsPreRV778(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsAnnuities As Annuities
		
		On Error GoTo InsPreRV778_Err
		lclsAnnuities = New Annuities
		nPremiumbas = eRemoteDB.Constants.intNull
		If lclsAnnuities.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
			
			nPremiumbas = lclsAnnuities.nPremiumbas
			InsPreRV778 = Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
		End If
InsPreRV778_Err: 
		If Err.Number Then
			InsPreRV778 = False
		End If
		'UPGRADE_NOTE: Object lclsAnnuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAnnuities = Nothing
		On Error GoTo 0
	End Function
End Class






