Option Strict Off
Option Explicit On
Public Class Coinsurans
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Coinsurans.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase "Coinsuran" a la colección
	Public Function Add(ByVal objCoinsuran As Coinsuran) As Coinsuran
		With objCoinsuran
			mCol.Add(objCoinsuran, "CI" & .sCertype & .nBranch & .nPolicy & .nProduct & .nCompany & .dEffecdate)
		End With
		Add = objCoinsuran
		'UPGRADE_NOTE: Object objCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objCoinsuran = Nothing
	End Function
	
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Coinsuran"
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsCoinsuran As Coinsuran
		Dim lrecReaCoinsuran As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'+Definición de parámetros para stored procedure 'insudb.ReaCoinsuran'
		'+Información leída el 01/12/2000 15:01:33
		lrecReaCoinsuran = New eRemoteDB.Execute
		With lrecReaCoinsuran
			.StoredProcedure = "reaCoinsuran_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsCoinsuran = New Coinsuran
					lclsCoinsuran.sCertype = sCertype
					lclsCoinsuran.nBranch = nBranch
					lclsCoinsuran.nPolicy = nPolicy
					lclsCoinsuran.nProduct = nProduct
					lclsCoinsuran.dEffecdate = dEffecdate
					lclsCoinsuran.nCompany = .FieldToClass("nCompany")
					lclsCoinsuran.nExpenses = .FieldToClass("nExpenses")
					lclsCoinsuran.nShare = .FieldToClass("nShare")
					Call Add(lclsCoinsuran)
					'UPGRADE_NOTE: Object lclsCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCoinsuran = Nothing
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
		'UPGRADE_NOTE: Object lclsCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCoinsuran = Nothing
		'UPGRADE_NOTE: Object lrecReaCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCoinsuran = Nothing
	End Function
	
	'%TotalShare: Devuelve el porcentaje de participación de la póliza
	Public Function TotalShare(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCompany As Integer, ByVal dEffecdate As Date) As Double
		Dim lclsCoinsuran As Coinsuran
		Dim lcolCoinsuran As Coinsurans
		Dim ldblTotalShare As Double
		
		lcolCoinsuran = New Coinsurans
		ldblTotalShare = 0
		If lcolCoinsuran.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
			For	Each lclsCoinsuran In lcolCoinsuran
				If nCompany <> lclsCoinsuran.nCompany Then
					ldblTotalShare = ldblTotalShare + lclsCoinsuran.nShare
				End If
			Next lclsCoinsuran
		End If
		
		TotalShare = ldblTotalShare
		
		'UPGRADE_NOTE: Object lclsCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCoinsuran = Nothing
		'UPGRADE_NOTE: Object lcolCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCoinsuran = Nothing
	End Function
	
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Coinsuran
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
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






