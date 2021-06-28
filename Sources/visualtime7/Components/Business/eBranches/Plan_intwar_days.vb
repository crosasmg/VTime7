Option Strict Off
Option Explicit On
Public Class Plan_intwar_days
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_intwar_days.cls                          $%'
	'% $Author:: Nvaplat31                                  $%'
	'% $Date:: 26/08/03 21:06                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mdtmEffecdate As Date
	Private mCol As Collection
	
	'**% Add: Adds the field to the nominal values collection
	'%Add: Agrega los campos a la colección de valores nominales
    Public Function Add(ByVal nTypeinvest As Integer, ByVal nRate As Double, Optional ByVal dEffecdate As Date = #1/1/1800#, Optional ByRef dNulldate As Object = Nothing, Optional ByVal sFoundDescript As String = "") As Plan_intwar_day
        Dim objNewMember As Plan_intwar_day
        objNewMember = New Plan_intwar_day

        If mCol Is Nothing Then
            mCol = New Collection
        End If
        With objNewMember
            .nTypeinvest = nTypeinvest
            .nRate = nRate
            .dEffecdate = dEffecdate
            .sFoundDescript = sFoundDescript
            .dNulldate = dNulldate
        End With

        mCol.Add(objNewMember, CStr(nTypeinvest) & CStr(dEffecdate))

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	
	
	'**% FindFounds: Allows to select all funds with its nominal actives values for the date
	'% FindFounds: Selecciona todos los fondos con sus valores nominales activos para la fecha
    Public Function FindFounds(ByVal ldtmEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaPlan_intwar_day_1 As eRemoteDB.Execute

        lrecreaPlan_intwar_day_1 = New eRemoteDB.Execute

        On Error GoTo FindFounds_Err

        FindFounds = True

        If ldtmEffecdate <> mdtmEffecdate Or lblnFind Then
            'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mCol = Nothing

            '**+ Parameters definition to stored procedure 'insudb.reaPlan_intwar_day_1'
            '**+ Data read on 04/09/2001 17:45:17
            '+ Definición de parámetros para stored procedure 'insudb.reaPlan_intwar_day_1'
            '+ Información leída el 09/04/2001 17:45:17

            With lrecreaPlan_intwar_day_1
                .StoredProcedure = "reaPlan_intwar_day_1"

                .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    mdtmEffecdate = ldtmEffecdate

                    Do While Not .EOF
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        Call Add(.FieldToClass("nTypeinvest", 0), .FieldToClass("nRate", 0), .FieldToClass("dEffecdate", System.DBNull.Value), .FieldToClass("dNulldate"), .FieldToClass("sDescript", String.Empty))
                        .RNext()
                    Loop

                    .RCloseRec()
                Else
                    FindFounds = False
                End If
            End With
        End If

FindFounds_Err:
        If Err.Number Then FindFounds = False

        'UPGRADE_NOTE: Object lrecreaPlan_intwar_day_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPlan_intwar_day_1 = Nothing
    End Function
	
	
	
	'*** Item: Use when making reference to an element of the collection
	'*** vntIndexKey contains the index or the password of the collection,
	'*** and that is why it is declared as a variant
	'*** Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'* Item: Se usa al hacer referencia a un elemento de la colección
	'* vntIndexKey contiene el índice o la clave de la colección,
	'* por lo que se declara como un Variant
	'* Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Plan_intwar_day
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Returns the number of elements that the collection has
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			'**+ Used when obtaining the number of elemnets of the
			'**+ collection. Sytnax: Debug.print x.Count
			'+ Se usa al obtener el número de elementos de la
			'+ colección. Sintaxis: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Enumerates the collection for use in a For Each...Next loop
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'** This property allows to enumerate
			'** this collection with the syntax For...Each
			'+ Esta propiedad permite enumerar
			'+ esta colección con la sintaxis For...Each
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'**+ Used when taking an element from the collection
		'**+ vntIndexKey contains the index or the password, and
		'**+ that is why it is declared as a variant
		'**+ Syntax: x.Remove (xyz)
		'+ Se usa al quitar un elemento de la colección
		'+ vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant
		'+ Sintaxis: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'**+ Creates the collection when the class is created
		'+ Crea la colección cuando se crea la clase
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the destruction of an instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'**+ Deletes the collection when the class is finished
		'+ Destruye la colección cuando se termina la clase
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






