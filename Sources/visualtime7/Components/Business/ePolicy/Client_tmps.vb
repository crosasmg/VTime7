Option Strict Off
Option Explicit On
Public Class Client_tmps
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Client_tmps.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Local variable to hold collection
	Private mCol As Collection
	
	'- Se define la variable que contiene el Nro. total de asegurados para la Cotización
	
	Public nTotalInsured As Double
	
	'% Add: se agrega un elemento en la colección
	Public Function Add(ByVal lclsClient_tmp As Client_tmp) As Client_tmp
		With lclsClient_tmp
			mCol.Add(lclsClient_tmp, "CT" & .sCertype & .nBranch & .nProduct & .nPolicy & .nGroup & .nRole & .nId)
		End With
		'+ Return the object created
		Add = lclsClient_tmp
		'UPGRADE_NOTE: Object lclsClient_tmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient_tmp = Nothing
	End Function
	
	'% Find: se buscan los elementos asociados a una póliza para una fecha dada
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lclsClient_tmp As Client_tmp
		Dim lintAge As Integer
		Dim lintAgeDif As Integer
		Dim lclsRoles As Roles
		On Error GoTo Find_Err
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.valMaxDate_age_collect'
		'+Información leída el 07/01/2002 02:00:11 p.m.
		
		With lclsExecute
			.StoredProcedure = "reaClient_tmp_A"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nTotalInsured = 0
				Do While Not .EOF
					lclsClient_tmp = New Client_tmp
					With lclsClient_tmp
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nGroup = lclsExecute.FieldToClass("nGroup")
						.nRole = lclsExecute.FieldToClass("nRole")
						.nId = lclsExecute.FieldToClass("nId")
						.sTypeAge = lclsExecute.FieldToClass("sTypeage")
						.dBirthdate = lclsExecute.FieldToClass("dBirthdate")
						.nInitAge = lclsExecute.FieldToClass("nInitAge")
						.nEndAge = lclsExecute.FieldToClass("nEndAge")
						.nInsured = lclsExecute.FieldToClass("nInsured")
						.nRentamount = lclsExecute.FieldToClass("nRentAmount")
						.nCurrency = lclsExecute.FieldToClass("nCurrency")
						.sVIP = lclsExecute.FieldToClass("sVIP")
						If .dBirthdate = eRemoteDB.Constants.dtmNull Then
							.nAge = eRemoteDB.Constants.intNull
						Else
							'+ Se obtiene edad actuarial de asegurado
							lclsRoles = New Roles
							If lclsRoles.CalInsuAge(nBranch, nProduct, dEffecdate, .dBirthdate, "0", "") Then
								.nAge = lclsRoles.nAge(True)
							End If
							
							'                        lintAge = DateDiff("m", .dBirthdate, dEffecdate)
							'                        lintAgeDif = Int(DateDiff("m", .dBirthdate, dEffecdate) / 12) * 12
							'                        lintAgeDif = lintAge - lintAgeDif
							'                        If lintAgeDif >= 6 Then
							'                           .nAge = DateDiff("yyyy", .dBirthdate, dEffecdate) + 1
							'                        Else
							'                           .nAge = DateDiff("yyyy", .dBirthdate, dEffecdate)
							'                        End If
							
						End If
						nTotalInsured = nTotalInsured + .nInsured
					End With
					Call Add(lclsClient_tmp)
					'UPGRADE_NOTE: Object lclsClient_tmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClient_tmp = Nothing
					.RNext()
				Loop 
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
		'UPGRADE_NOTE: Object lclsClient_tmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient_tmp = Nothing
	End Function
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Client_tmp
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el Nro. de elementos que tiene la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite recorrer los elementos de la colección
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
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: se controla la creación de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla la destrucción de la colección
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






