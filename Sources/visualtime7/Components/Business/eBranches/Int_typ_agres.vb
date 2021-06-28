Option Strict Off
Option Explicit On
Public Class Int_typ_agres
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Int_typ_agres.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngAgreement As Integer
	
	'%Add: Agregar un elemento a la colección
	Public Function Add(ByRef objClass As Int_typ_agre) As Int_typ_agre
		
		If objClass Is Nothing Then
			objClass = New Int_typ_agre
		End If
		
		With objClass
			mCol.Add(objClass, "ITA" & .nAgreement & .nIntertyp)
		End With
		
		'return the object created
		Add = objClass
		
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nAgreement As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaInt_typ_agre As eRemoteDB.Execute
		Dim lclsInt_typ_agre As Int_typ_agre
		
		On Error GoTo Find_Err
		If mlngAgreement <> nAgreement Or bFind Then
			lrecReaInt_typ_agre = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'ReaInt_typ_agre'
			'+Información leída el 23/10/01
			With lrecReaInt_typ_agre
				.StoredProcedure = "ReaInt_typ_agre_a"
				.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsInt_typ_agre = New Int_typ_agre
						lclsInt_typ_agre.sSel = .FieldToClass("sSel")
						lclsInt_typ_agre.nAgreement = .FieldToClass("nAgreement")
						lclsInt_typ_agre.nIntertyp = .FieldToClass("nIntertyp")
						lclsInt_typ_agre.sDescript = .FieldToClass("sDescript")
						Call Add(lclsInt_typ_agre)
						'UPGRADE_NOTE: Object lclsInt_typ_agre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsInt_typ_agre = Nothing
						.RNext()
					Loop 
					Find = True
					mlngAgreement = nAgreement
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaInt_typ_agre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaInt_typ_agre = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Int_typ_agre
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
		mlngAgreement = eRemoteDB.Constants.intNull
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






