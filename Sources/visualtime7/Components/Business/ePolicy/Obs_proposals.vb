Option Strict Off
Option Explicit On
Public Class Obs_proposals
	Implements System.Collections.IEnumerable
	'- Objeto interno para el manejo de la colección
	Private mCol As Collection
	
	'% Add: se agrega un elemento a la colección
	Public Function Add(ByVal lclsObs_proposal As obs_proposal) As obs_proposal
		With lclsObs_proposal
			mCol.Add(lclsObs_proposal, "OP" & .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nId)
		End With
		
		Add = lclsObs_proposal
		'UPGRADE_NOTE: Object lclsObs_proposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObs_proposal = Nothing
	End Function
	
	'% Find: se buscan los datos asociados a la póliza/certificado
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaObs_proposal_a As eRemoteDB.Execute
		Dim lclsObs_proposal As obs_proposal
		
		On Error GoTo reaObs_proposal_a_Err
		
		lrecreaObs_proposal_a = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reaobs_proposal_a'
		'+Información leída el 08/11/2001
		With lrecreaObs_proposal_a
			.StoredProcedure = "reaObs_proposal_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsObs_proposal = New obs_proposal
					With lclsObs_proposal
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nCertif = nCertif
						.nId = lrecreaObs_proposal_a.FieldToClass("nId")
						.nObservation = lrecreaObs_proposal_a.FieldToClass("nObservation")
						.nNotenum = lrecreaObs_proposal_a.FieldToClass("nNotenum")
                        .dCompdate = lrecreaObs_proposal_a.FieldToClass("dCompdate")
						Call Add(lclsObs_proposal)
					End With
					'UPGRADE_NOTE: Object lclsObs_proposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsObs_proposal = Nothing
					.RNext()
				Loop 
				Find = True
			End If
		End With
		
reaObs_proposal_a_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaObs_proposal_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaObs_proposal_a = Nothing
		'UPGRADE_NOTE: Object lclsObs_proposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObs_proposal = Nothing
		On Error GoTo 0
	End Function
	
	
	'* Item: referencia un objeto de la clase en la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As obs_proposal
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el número de elemtos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: devuelve la instancia de la clase en la colección.
	'*          Utilizada en la sentencia For ... Each
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
	
	'* Count: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: se controla el acceso a la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla el la destrucción de la clase
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






