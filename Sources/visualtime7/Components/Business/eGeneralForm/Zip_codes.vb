Option Strict Off
Option Explicit On
Public Class Zip_codes
	Implements System.Collections.IEnumerable
	
	'+ Local variable to hold collection
	Private mCol As Collection
	
	'% Add: A�ade una nueva instancia de la clase Zip_code a la colecci�n
	Public Function Add(ByVal nZip_Code As Integer, ByVal nLocal As Integer, ByVal nOffice As Integer, ByVal nAuto_zone As Integer, ByVal nOrder As Integer, ByVal sShort_des As String, Optional ByVal sKey As String = "") As Zip_code
		'create a new object
		Dim objNewMember As Zip_code
		objNewMember = New Zip_code
		
		With objNewMember
			.nZip_Code = nZip_Code
			.nLocal = nLocal
			.nOffice = nOffice
			.nAuto_zone = nAuto_zone
			.nOrder = nOrder
			.sShort_des = sShort_des
		End With
		
		'set the properties passed into the method
		mCol.Add(objNewMember, "ZC" & nZip_Code & nLocal)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Find: Esta funci�n se encarga de buscar los codigos postales
	Public Function Find() As Boolean
		Dim lrecreaZip_codeA As eRemoteDB.Execute
		
		lrecreaZip_codeA = New eRemoteDB.Execute
		
		
		Find = False
		
		'Definici�n de par�metros para stored procedure 'insudb.reaZip_codeA'
		'Informaci�n le�da el 15/11/2000 11:03:58 AM
		With lrecreaZip_codeA
			.StoredProcedure = "reaZip_codeA"
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nZip_code"), .FieldToClass("nLocal"), .FieldToClass("nOffice"), .FieldToClass("nAuto_zone"), .FieldToClass("nOrder"), .FieldToClass("sShort_des"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaZip_codeA may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaZip_codeA = Nothing
	End Function
	
	'%Update : Permite actualizar los registros de la colecci�n en la tabla
	'% Zip_code.
	Public Function Update() As Boolean
		
		Dim lclsZip_code As Zip_code
		
		Update = True
		
		For	Each lclsZip_code In mCol
			With lclsZip_code
				Select Case .nStatInstanc
					Case Zip_code.eStatusInstance.eftNew
						Update = .Add()
						.nStatInstanc = Zip_code.eStatusInstance.eftQuery
					Case Zip_code.eStatusInstance.eftUpDate
						Update = .Update()
					Case Zip_code.eStatusInstance.eftDelete
						Update = .Delete()
						mCol.Remove((CStr("ZC" & .nZip_Code & .nLocal)))
				End Select
			End With
		Next lclsZip_code
		
	End Function
	'* Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Zip_code
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	'* Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colecci�n
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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






