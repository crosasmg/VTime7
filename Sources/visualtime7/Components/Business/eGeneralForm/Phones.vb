Option Strict Off
Option Explicit On
Public Class Phones
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'-Se definen las variables auxiliares para evitar una b�squeda innecesaria
	Private lauxKeyAddress As String
	Private lAuxKeyPhones As Integer
	Private lAuxRecowner As Integer
	Private lAuxEffecdate As Date
	
	'**%Add: adds a new instance of the "Phone" class to the collection
	'%Add: A�ade una nueva instancia de la clase "Phone" a la colecci�n
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nRecowner As Integer, ByVal sKeyAddress As String, ByVal nKeyPhones As Integer, ByVal nArea_code As Integer, ByVal dEffecdate As Date, ByVal sPhone As String, ByVal nOrder As Integer, ByVal nExtens1 As Integer, ByVal nPhone_type As Integer, ByVal nExtens2 As Integer, ByVal dNulldate As Date) As Phone
		
		'+ Create a new object
		
		Dim objNewMember As Phone
		objNewMember = New Phone
		
		'+ Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nRecowner = nRecowner
			.sKeyAddress = sKeyAddress
			.nKeyPhones = nKeyPhones
			.nArea_code = nArea_code
			.dEffecdate = dEffecdate
			.sPhone = sPhone
			.nOrder = nOrder
			.nExtens1 = nExtens1
			.nPhone_type = nPhone_type
			.nExtens2 = nExtens2
			.dNulldate = dNulldate
		End With
		
		mCol.Add(objNewMember, "A" & nKeyPhones)
		
		'Return the object created
		
		Add = objNewMember
		
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'% tabla "Phones"
	Public Function Find(ByVal nRecowner As Integer, ByVal sKeyAddress As String, ByVal dEffecdate As Date, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecreaPhones_All As eRemoteDB.Execute
		If lAuxRecowner = nRecowner And lAuxEffecdate = dEffecdate And lauxKeyAddress = sKeyAddress Then
			Find = True
		Else
			lrecreaPhones_All = New eRemoteDB.Execute
			
			'+ Definici�n de par�metros para stored procedure 'insudb.reaPhones'
			'+ Informaci�n le�da el 12/07/2000 15:03:59
			With lrecreaPhones_All
				.StoredProcedure = "reaPhones"
				.Parameters.Add("nRecowner", nRecowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nKeyPhones", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAll", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					Do While Not .EOF
						Call Add(0, CInt(.FieldToClass("nRecowner")), .FieldToClass("sKeyAddress"), CInt(.FieldToClass("nKeyPhones")), CInt(.FieldToClass("nArea_code")), .FieldToClass("dEffecdate"), .FieldToClass("sPhone"), CInt(.FieldToClass("nOrder")), .FieldToClass("nExtens1", 0), CInt(.FieldToClass("nPhone_type")), .FieldToClass("nExtens2", 0), .FieldToClass("dNulldate"))
						.RNext()
					Loop 
					.RCloseRec()
					
					'+ Se asignan los valores a las variables auxiliares, para futuras b�squedas
					lauxKeyAddress = sKeyAddress
					lAuxRecowner = nRecowner
					lAuxEffecdate = dEffecdate
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaPhones_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaPhones_All = Nothing
		End If
	End Function
	'% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'% tabla "Phones"
	Public Function GetFromAddress(ByVal nRecowner As Integer, ByVal sKeyAddress As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaPhones_All As eRemoteDB.Execute
		
		lrecreaPhones_All = New eRemoteDB.Execute
		
		'+ Definici�n de par�metros para stored procedure 'insudb.creaTmp_Phones'
		With lrecreaPhones_All
			.StoredProcedure = "creaTmp_Phones"
			.Parameters.Add("nRecowner", nRecowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			GetFromAddress = .Run()
			If GetFromAddress Then
				Do While Not .EOF
					Call Add(0, CInt(.FieldToClass("nRecowner")), .FieldToClass("sKeyAddress"), CInt(.FieldToClass("nKeyPhones")), CInt(.FieldToClass("nArea_code")), .FieldToClass("dEffecdate"), .FieldToClass("sPhone"), CInt(.FieldToClass("nOrder")), .FieldToClass("nExtens1", 0), CInt(.FieldToClass("nPhone_type")), .FieldToClass("nExtens2", 0), .FieldToClass("dNulldate"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaPhones_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPhones_All = Nothing
	End Function
	
	'% Update: Este m�todo se encarga de actualizar registros en la tabla "Phones". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecut� correctamente.
	Public Function Update() As Boolean
		Dim lclsPhones As Phone
		Dim lcolAux As Collection
		Update = True
		lcolAux = New Collection
		For	Each lclsPhones In mCol
			With lclsPhones
				
				If lAuxKeyPhones = 0 Then
					lAuxEffecdate = .dEffecdate
					lAuxKeyPhones = .nKeyPhones
					lAuxRecowner = .nRecowner
				End If
				
				Select Case .nStatusInstance
					
					'+ Si la acci�n es Agregar
					Case 1
						Update = .Add()
						
						'+ Si la acci�n es Actualizar
					Case 2
						Update = .Update()
						
						'+ Si la acci�n es Eliminar
					Case 3
						Update = .Delete()
				End Select
				If .nStatusInstance <> 3 Then
					If Update Then
						.nStatusInstance = 0
					End If
					lcolAux.Add(lclsPhones, "A" & .nKeyPhones)
				End If
			End With
		Next lclsPhones
		mCol = lcolAux
	End Function
	
	'%insMaxPhone: Esta funci�n se encarga de buscar el maximo valor encontrado en los tel�fonos
    Public Function insMaxPhone(ByVal Recowner As Address.eTypeRecOwner, ByVal rectype As Addresss.eTypeRecType, ByVal KeyAddress As String, Optional ByVal Effecdate As Date = dtmNull) As Integer
        Dim lrecreaMaxPhone As eRemoteDB.Execute

        If Effecdate = dtmNull Then
            Effecdate = Today
        End If

        lrecreaMaxPhone = New eRemoteDB.Execute

        '+Definici�n de par�metros para stored procedure 'insudb.reaMaxPhone'
        '+Informaci�n le�da el 28/08/2000 16:27:46
        With lrecreaMaxPhone
            .StoredProcedure = "reaMaxPhone"
            .Parameters.Add("nRecOwner", Recowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRecType", rectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKeyAddress", KeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                insMaxPhone = .FieldToClass("MaxPhone", 0) + 1
                .RCloseRec()
            Else
                insMaxPhone = 1
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaMaxPhone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaMaxPhone = Nothing
    End Function
	
	'*Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Phones
		Get
			'Used when referencing an element in the collection.
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			'Used when retrieving the number of elements in the collection.
			'Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'This property allows you to enumerate this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colecci�n
	Public Sub Remove(ByRef vntIndexKey As Object)
		'Used when removing an element from the collection.
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'Creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'Destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






