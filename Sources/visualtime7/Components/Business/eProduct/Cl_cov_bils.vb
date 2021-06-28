Option Strict Off
Option Explicit On
Public Class Cl_cov_bils
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cl_cov_bils.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales de la colección
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase "Cl_cov_bil" a la colección
	Public Function Add(ByRef nModulec As Integer, ByRef nCover As Integer, ByRef nBranch As Integer, ByRef nPay_concep As Integer, ByRef nProduct As Integer, ByRef dEffecdate As Date, ByRef dCompdate As Date, ByRef dNulldate As Date, ByRef nUsercode As Integer, ByRef sStatregt As String, ByRef sDescript As String, ByRef sShort_des As String, ByRef nSelection As Integer) As Cl_cov_bil
		'+ Se crea el nuevo objeto
		Dim objNewMember As Cl_cov_bil
		objNewMember = New Cl_cov_bil
		With objNewMember
			.nModulec = nModulec
			.nCover = nCover
			.nBranch = nBranch
			.nPay_concep = nPay_concep
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.dCompdate = dCompdate
			.dNulldate = dNulldate
			.nUsercode = nUsercode
			.sStatregt = sStatregt
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nSelection = nSelection
		End With
		
		'+ Se setean las propiedades pasadas por parámetro
		mCol.Add(objNewMember)
		
		'+ Se retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Find: Este metodo carga la coleccion de elementos de la tabla "cl_cov_bil" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindCl_cov_bil2(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecreacl_cov_bil As eRemoteDB.Execute
		Dim lclsCl_cov_bil As Cl_cov_bil
		
		On Error GoTo reaCl_cov_bil_Err
		
		lrecreacl_cov_bil = New eRemoteDB.Execute
		lclsCl_cov_bil = New Cl_cov_bil
		
		'+Definición de parámetros para stored procedure 'insudb.reacl_cov_bil'
		'+Información leída el 07/05/2001 04:50:04 p.m.
		
		With lrecreacl_cov_bil
			.StoredProcedure = "reacl_cov_bil"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsCl_cov_bil = Add(nProduct, nCover, nBranch, .FieldToClass("npay_concep"), nProduct, dEffecdate, .FieldToClass("dCompdate"), System.Date.FromOADate(eRemoteDB.Constants.intNull), .FieldToClass("nUsercode"), .FieldToClass("sStatre"), .FieldToClass("sDescript"), .FieldToClass("sShort_des"), .FieldToClass("selection"))
					lclsCl_cov_bil.nSelection = .FieldToClass("selection")
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
reaCl_cov_bil_Err: 
		If Err.Number Then
			FindCl_cov_bil2 = False
		End If
		
		'UPGRADE_NOTE: Object lrecreacl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreacl_cov_bil = Nothing
		'UPGRADE_NOTE: Object lclsCl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCl_cov_bil = Nothing
		
		On Error GoTo 0
	End Function
	
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cl_cov_bil
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
			'
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






