Option Strict Off
Option Explicit On
Public Class Disc_pbs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_pbs.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'-Local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngIntertyp As Integer
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agregar un elemento a la colección
	Public Function Add(ByRef objClass As Disc_pb) As Disc_pb
		If objClass Is Nothing Then
			objClass = New Disc_pb
		End If
		
		With objClass
			mCol.Add(objClass, "DP" & .nIntertyp & .nBranch & .nProduct & .nModulec & .nAgreement & .nQPB & .dEffecdate.ToString("yyyyMMdd"))
		End With
		'return the object created
		Add = objClass
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaDisc_pb As eRemoteDB.Execute
		Dim lclsDisc_pb As Disc_pb
		
		On Error GoTo Find_Err
		If mlngIntertyp <> nIntertyp Or mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecReaDisc_pb = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'ReaDisc_pb'
			With lrecReaDisc_pb
				.StoredProcedure = "ReaDisc_pb"
				.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mlngIntertyp = nIntertyp
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngModulec = nModulec
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
						lclsDisc_pb = New Disc_pb
						lclsDisc_pb.nIntertyp = nIntertyp
						lclsDisc_pb.nBranch = nBranch
						lclsDisc_pb.nProduct = nProduct
						lclsDisc_pb.nModulec = nModulec
						lclsDisc_pb.nAgreement = .FieldToClass("nAgreement")
						lclsDisc_pb.dEffecdate = .FieldToClass("dEffecdate")
						lclsDisc_pb.dNulldate = .FieldToClass("dNulldate")
						lclsDisc_pb.nQPB = .FieldToClass("nQPB")
						lclsDisc_pb.nPercent = .FieldToClass("nPercent")
						Call Add(lclsDisc_pb)
						.RNext()
						'UPGRADE_NOTE: Object lclsDisc_pb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsDisc_pb = Nothing
					Loop 
					.RCloseRec()
					Find = True
				End If
			End With
		Else
			Find = True
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaDisc_pb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaDisc_pb = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Disc_pb
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






