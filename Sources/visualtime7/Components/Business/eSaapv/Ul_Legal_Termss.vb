Option Strict Off
Option Explicit On
Public Class Ul_Legal_Termss
	Implements System.Collections.IEnumerable
	
	
	
	
	Private mCol As Collection
	
	Public nCount As Integer
	
	'% Add: Añade una nueva instancia de la clase Type_commiss_det a la colección
	Public Function Add(ByVal nType_saapv As Integer, ByVal dEffecdate As Date, ByVal nValuesmo As Integer, ByVal nValuesty As Integer, ByVal nDayadd As Integer) As Ul_Legal_Terms
		'create a new object
		
		Dim objNewMember As Ul_Legal_Terms
		objNewMember = New Ul_Legal_Terms
		
		With objNewMember
			.nType_saapv = nType_saapv
			.dEffecdate = dEffecdate
			.nValuesmo = nValuesmo
			.nValuesty = nValuesty
			.nDayadd = nDayadd
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	Public Function Find(ByVal dEffecdate As Date) As Boolean
		'- Se define la variable lrecUl_Legal_Termss que se utilizará como cursor.
		Dim lrecReaUl_Legal_Termss As eRemoteDB.Execute
		
		lrecReaUl_Legal_Termss = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
		
		With lrecReaUl_Legal_Termss
			.StoredProcedure = "insmvi7300pkg.ReaUl_Legal_Terms"
			
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nType_saapv"), .FieldToClass("dEffecdate"), .FieldToClass("nValuesmo"), .FieldToClass("nValuesty"), .FieldToClass("nDayadd"))
					.RNext()
				Loop 
			End If
		End With
		lrecReaUl_Legal_Termss = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ul_Legal_Terms
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






