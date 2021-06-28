Option Strict Off
Option Explicit On
Public Class Fund_invs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_invs.cls                            $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 31/10/03 11.38                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**- local variable to hold collection
	'- Variable local para contener colección
	
	Private mCol As Collection
	
	'**- The variable to store the last inquiry date id defined
	'- Variable que contiene la última fecha consultada
	
	Private mdtmInpDate As Date

    '**% Add: Adds the object to the collection
    '% Add: Permite añadir un registro a la colección
    Public Function Add(ByVal nStatInstanc As Fund_inv.eStatusInstance_f, ByVal nQuan_avail As Double, ByVal sStatregt As String, ByVal nQuan_min As Double, ByVal nQuan_max As Double, ByVal dInpdate As Date, ByVal sDescript As String, ByVal nFunds As Integer, ByVal nSeries As Double, ByVal nRun As Double, ByVal nCountry As Integer, ByRef sRoutine As String, ByRef sGuaranteed As String, ByVal sTicker As String, ByVal sISIN_code As String) As Fund_inv
        Dim lobjFund_inv As Fund_inv
        lobjFund_inv = New Fund_inv
        If mCol Is Nothing Then
            mCol = New Collection
        End If

        With lobjFund_inv
            .nStatInstanc = nStatInstanc
            .nQuan_avail = nQuan_avail
            .sStatregt = sStatregt
            .nQuan_min = nQuan_min
            .nQuan_max = nQuan_max
            .dInpdate = dInpdate
            .sDescript = sDescript
            .nFunds = nFunds
            .nSeries = nSeries
            .nRun = nRun
            .nCountry = nCountry
            .sRoutine = sRoutine
            .sGuaranteed = sGuaranteed
            .sTicker = sTicker
            .sISIN_code = sISIN_code
        End With

        mCol.Add(lobjFund_inv, CStr(nFunds))
        'UPGRADE_NOTE: Object lobjFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjFund_inv = Nothing
    End Function

    '**% FindDateMax: Seachs the last date of modification in the table Fund_inv
    '% FindDateMax: Selecciona la última fecha de modificación de Fund_inv
    Public Function FindDateMax() As Date
		Dim lrecvalFund_inv As eRemoteDB.Execute
		Dim ldtmDate As Object
		
		lrecvalFund_inv = New eRemoteDB.Execute
		
		ldtmDate = Today
		
		With lrecvalFund_inv
			.StoredProcedure = "valFund_inv"
			
			.Parameters.Add("dInpdate", ldtmDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				FindDateMax = IIf(IsDbNull(.Parameters.Item("dInpdate").Value), #1/1/1800#, .Parameters.Item("dInpdate").Value)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecvalFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalFund_inv = Nothing
	End Function
	
	'**% Find: Searchs all valid funds according to the date given from
	'% Find: Permite seleccionar todos los fondos válidos para la fecha
    Public Function Find(Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaFund_inv As eRemoteDB.Execute
        On Error GoTo Find_Err
        lrecreaFund_inv = New eRemoteDB.Execute

        If lblnFind Then
            With lrecreaFund_inv
                .StoredProcedure = "reaFund_inv"
                If .Run Then
                    Do While Not .EOF
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        Call Add((Fund_inv.eStatusInstance_f.eftExist_f), .FieldToClass("nQuan_avail", 0), .FieldToClass("sStatregt", String.Empty), .FieldToClass("nQuan_min", 0), .FieldToClass("nQuan_max", eRemoteDB.Constants.intNull), .FieldToClass("dInpdate", System.DBNull.Value), .FieldToClass("sDescript", String.Empty), .FieldToClass("nFunds", 0), .FieldToClass("nSeries", 0), .FieldToClass("nRun", 0), .FieldToClass("nCountry", 0), .FieldToClass("sRoutine", String.Empty), .FieldToClass("sGuaranteed", String.Empty), .FieldToClass("sTicker", String.Empty), .FieldToClass("sISIN_code", String.Empty))
                        .RNext()
                    Loop
                    .RCloseRec()
                    Find = True
                End If
            End With
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFund_inv = Nothing
        On Error GoTo 0
    End Function
	
	'**% Update: Updates the information in the table Fund_inv.
	'% Update: Permite actualizar los registros de la colección en la tabla Fund_inv.
	Public Function Update() As Boolean
		
		Dim lclsFund_inv As Fund_inv
		
		Update = True
		
		On Error GoTo Update_Err
		
		For	Each lclsFund_inv In mCol
			With lclsFund_inv
				Select Case .nStatInstanc
					Case Fund_inv.eStatusInstance_f.eftNew_f
						Update = .Add()
						.nStatInstanc = Fund_inv.eStatusInstance_f.eftQuery_f
					Case Fund_inv.eStatusInstance_f.eftUpDate_f
						Update = .Update()
					Case Fund_inv.eStatusInstance_f.eftDelete_f
						Update = .Delete()
						mCol.Remove((CStr(.nFunds)))
				End Select
			End With
		Next lclsFund_inv
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
	End Function
	
	'*** Item: Use when making reference to an element of the collection
	'*** vntIndexKey contains the index or the password of the collection,
	'*** and that is why it is declared as a variant
	'*** Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'* Item: Se usa al hacer referencia a un elemento de la colección
	'* vntIndexKey contiene el índice o la clave de la colección,
	'* por lo que se declara como un Variant
	'* Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Fund_inv
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






