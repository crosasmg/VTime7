Option Strict Off
Option Explicit On
Public Class Guar_Saving_Pols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Guar_Saving_Pols.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	Public ncount As Short
	'-Local variable to hold collection
	Private mCol As Collection
	
	
	'**%Add: Add a new instance of the benefit class to the collection
	'%Add: Añade una nueva instancia de la clase Guar_Saving_Pol a la colección
	Public Function Add(ByVal lclsGuar_Saving_Pol As Guar_Saving_Pol) As Guar_Saving_Pol
		With lclsGuar_Saving_Pol
			mCol.Add(lclsGuar_Saving_Pol, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .dEffecdate & .nGuarsavid)
		End With
		Add = lclsGuar_Saving_Pol
		'UPGRADE_NOTE: Object lclsGuar_Saving_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGuar_Saving_Pol = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Guar_Saving_Pol" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Guar_Saving_Pol" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaGuar_Saving_Pol As eRemoteDB.Execute
		Dim lclsGuar_Saving_Pol As Guar_Saving_Pol
		Dim llngIndex As Integer
		
		On Error GoTo Find_Err
		
		lrecReaGuar_Saving_Pol = New eRemoteDB.Execute
		Me.ncount = 0
		'+Definición de parámetros para stored procedure 'insudb.reaGuar_Saving_Pol_a'
		'+Información leída el 27/03/2002
		
		With lrecReaGuar_Saving_Pol
			.StoredProcedure = "INSVI8000PKG.REAGUAR_SAVING_POL"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				llngIndex = 0
				Do While Not .EOF
					Me.ncount = 1
					lclsGuar_Saving_Pol = New Guar_Saving_Pol
					lclsGuar_Saving_Pol.sCertype = sCertype
					lclsGuar_Saving_Pol.nBranch = nBranch
					lclsGuar_Saving_Pol.nProduct = nProduct
					lclsGuar_Saving_Pol.nPolicy = nPolicy
					lclsGuar_Saving_Pol.nCertif = nCertif
					lclsGuar_Saving_Pol.dEffecdate = .FieldToClass("dEffecdate")
					lclsGuar_Saving_Pol.nGuarsavid = .FieldToClass("nGuarsavid")
					lclsGuar_Saving_Pol.nGuarsav_year = .FieldToClass("nGuarsav_year")
					lclsGuar_Saving_Pol.dStart_guarsav = .FieldToClass("dStart_guarsav")
					lclsGuar_Saving_Pol.dEnd_guarsav = .FieldToClass("dEnd_guarsav")
					lclsGuar_Saving_Pol.nGuarsav_value = .FieldToClass("nGuarsav_value")
					lclsGuar_Saving_Pol.nCurrency = .FieldToClass("nCurrency")
					lclsGuar_Saving_Pol.nGuarsav_cost = .FieldToClass("nGuarsav_cost")
					lclsGuar_Saving_Pol.nGuarsav_stat = .FieldToClass("nGuarsav_stat")
					lclsGuar_Saving_Pol.nRen_guarsav = .FieldToClass("nRen_guarsav")
					lclsGuar_Saving_Pol.sDeppremind = .FieldToClass("sDeppremind")
					lclsGuar_Saving_Pol.nGuarsav_prem = .FieldToClass("nGuarsav_prem")
					
					Call Add(lclsGuar_Saving_Pol)
					
					'UPGRADE_NOTE: Object lclsGuar_Saving_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsGuar_Saving_Pol = Nothing
					llngIndex = llngIndex + 1
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaGuar_Saving_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaGuar_Saving_Pol = Nothing
		'UPGRADE_NOTE: Object lclsGuar_Saving_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGuar_Saving_Pol = Nothing
	End Function
	
	'***Item: Returns a element of the collection (according Index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Guar_Saving_Pol
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of the element the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**%Remove: Delete the element of the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Control the creation of a collection instance
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Control the destruction of the collection instance
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
End Class






