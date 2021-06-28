Option Strict Off
Option Explicit On
Public Class Way_pay_prods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Way_pay_prods.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales para la colecci�n
	Private mCol As Collection
	
	'%Add: Agrega una nueva instancia de la clase a la colecci�n
	Public Function Add(ByRef objClass As Way_pay_prod) As Way_pay_prod
		'+ Se crea un nuevo objeto
		If objClass Is Nothing Then
			objClass = New Way_pay_prod
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .nWay_pay & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ Se retorna el objeto creado
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'%Find: Lee los datos de la tabla para la transacci�n DP578
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaWay_pay_prod_a As eRemoteDB.Execute
		Dim lclsWay_pay_prod As Way_pay_prod
		lrecReaWay_pay_prod_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+Definici�n de par�metros para stored procedure 'ReaWay_pay_prod_a'
		'+Informaci�n le�da el 07/05/2002
		With lrecReaWay_pay_prod_a
			.StoredProcedure = "ReaWay_pay_prod_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsWay_pay_prod = New Way_pay_prod
					lclsWay_pay_prod.nBranch = .FieldToClass("nBranch")
					lclsWay_pay_prod.nProduct = .FieldToClass("nProduct")
					lclsWay_pay_prod.nWay_pay = .FieldToClass("nWay_pay")
					lclsWay_pay_prod.dEffecdate = .FieldToClass("dEffecdate")
					lclsWay_pay_prod.nRate_ex = .FieldToClass("nRate_ex")
					lclsWay_pay_prod.nRate_disc = .FieldToClass("nRate_disc")
					lclsWay_pay_prod.sPrem_first = .FieldToClass("sPrem_first")
					lclsWay_pay_prod.nNull_day = .FieldToClass("nNull_day")
					lclsWay_pay_prod.sOneReceipt = .FieldToClass("sOneReceipt")
                    lclsWay_pay_prod.sLastReceipt = .FieldToClass("sLastReceipt")
                    lclsWay_pay_prod.sCollection = .FieldToClass("sCollection")
					
					Call Add(lclsWay_pay_prod)
					.RNext()
					'UPGRADE_NOTE: Object lclsWay_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsWay_pay_prod = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaWay_pay_prod_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaWay_pay_prod_a = Nothing
		On Error GoTo 0
	End Function
	
	'%Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Way_pay_prod
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colecci�n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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






