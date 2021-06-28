Option Strict Off
Option Explicit On
Public Class TabGens
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'**- Auxiliary variables
	'- Propiedades auxiliares
	
	Private mstrTable As String
	
	'**%Add: adds a new instance of the "TabGen" class to the collection
	'%Add: Añade una nueva instancia de la clase "TabGen" a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal key As Double, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal dCompdate As Date, Optional ByVal sValorAdic As String = "") As TabGen
		Dim lclsTabGen As TabGen
		
		lclsTabGen = New TabGen
		
		On Error GoTo Add_err
		
		With lclsTabGen
			.nStatusInstance = nStatusInstance
			.key = CStr(key)
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.dCompdate = dCompdate
			.sValorAdic = sValorAdic
		End With
		
		mCol.Add(lclsTabGen, Trim(CStr(key)))
		
		Add = lclsTabGen
		'UPGRADE_NOTE: Object lclsTabGen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTabGen = Nothing
		
Add_err: 
		If Err.Number Then
            Add = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This method updates the records of the collection in the table "TabGen"
	'%Update: Permite actualizar los registros de la colección en la tabla "TabGen"
	Public Function Update(ByVal sTable As String) As Boolean
        Dim lclsTabGen As TabGen = New TabGen
        Dim lstrKey As String
		Dim lcolAux As Collection
		
		On Error GoTo Update_err
		
		lstrKey = lclsTabGen.insSearchKeyValues(sTable)
		
		Update = True
		
		lcolAux = New Collection
		
		For	Each lclsTabGen In mCol
			With lclsTabGen
				Select Case .nStatusInstance
					
					'**+ If the action is Add
					'+ Si la acción es Agregar
					
					Case 1
						Update = .Add(sTable, lstrKey)
						
						'**+ If the action is Update.
						'+ Si la acción es Actualizar
						
					Case 2
						Update = .Update(sTable, lstrKey)
						
						'**+ If the action is Delete
						'+ Si la acción es Eliminar
						
					Case 3
						Update = .Delete(sTable, lstrKey)
				End Select
				
				If .nStatusInstance <> 3 Then
					If Update Then
						.nStatusInstance = 0
					End If
					
					lcolAux.Add(lclsTabGen, Trim(.key))
				End If
			End With
		Next lclsTabGen
		
		'UPGRADE_NOTE: Object lclsTabGen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTabGen = Nothing
		mCol = lcolAux
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TabGen
		Get
			Item = mCol.Item(Trim(vntIndexKey))
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
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
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
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

    '**%Find: This method returns TRUE or FALSE depending if the records exists in the table "TabGen"
    '%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%tabla "TabGen"
    Public Function Find(ByVal sTable As String, Optional ByVal lblnFind As Boolean = False, Optional sOrigin As String = "1") As Boolean
        Dim lstrKey As String
        Dim lstrKeyColumName As String = ""
        Dim lKeyName As TabGen

        lKeyName = New TabGen

        On Error GoTo Find_err

        lstrKey = lKeyName.insSearchKeyValues(sTable)

        If lKeyName.ReaTable_NameXXX(sTable, sOrigin) Then
            lstrKeyColumName = lKeyName.sColumna
        End If

        Dim lrecReadArray As eRemoteDB.Execute
        If lstrKey = String.Empty Then
            Find = False
        Else

            lrecReadArray = New eRemoteDB.Execute

            Find = True

            If sTable <> mstrTable Or lblnFind Then

                With lrecReadArray
                    .StoredProcedure = "REATABLESPKG.REATABLES"
                    .Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sOrder", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run Then
                        mstrTable = sTable

                        Do While Not .EOF

                            If lstrKeyColumName <> String.Empty Then
                                Call Add(0, .FieldToClass(lstrKey, 0), .FieldToClass("sDescript", String.Empty), .FieldToClass("sShort_des", String.Empty), .FieldToClass("sStatregt", String.Empty), .FieldToClass("nUsercode", 0), .FieldToClass("dCompdate", dtmNull), .FieldToClass(lstrKeyColumName, String.Empty))
                            Else
                                Call Add(0, .FieldToClass(lstrKey, 0), .FieldToClass("sDescript", String.Empty), .FieldToClass("sShort_des", String.Empty), .FieldToClass("sStatregt", String.Empty), .FieldToClass("nUsercode", 0), .FieldToClass("dCompdate", dtmNull))
                            End If
                            .RNext()
                        Loop

                        .RCloseRec()
                    Else
                        Find = False

                        mstrTable = String.Empty
                    End If
                End With

                'UPGRADE_NOTE: Object lrecReadArray may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lrecReadArray = Nothing
            End If
        End If

        'UPGRADE_NOTE: Object lKeyName may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lKeyName = Nothing

Find_err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
    End Function
End Class






