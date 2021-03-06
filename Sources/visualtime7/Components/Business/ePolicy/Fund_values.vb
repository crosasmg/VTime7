Option Strict Off
Option Explicit On
Public Class Fund_values
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_values.cls                          $%'
	'% $Author:: Nvaplat31                                  $%'
	'% $Date:: 26/08/03 21:06                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mdtmEffecdate As Date
	Private mCol As Collection
	
	'**% Add: Adds the field to the nominal values collection
	'%Add: Agrega los campos a la colecci?n de valores nominales
    Public Function Add(ByVal nStatInstanc As Fund_inv.eStatusInstance_f, ByVal nFunds As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, Optional ByVal dEffecdate As Date = #1/1/1800#, Optional ByRef dNulldate As Object = Nothing, Optional ByVal sFoundDescript As String = "", Optional ByVal nQuan_avail As Double = 0) As Fund_value
        Dim objNewMember As Fund_value
        objNewMember = New Fund_value

        If mCol Is Nothing Then
            mCol = New Collection
        End If
        With objNewMember
            .nStatInstanc = nStatInstanc
            .nFunds = nFunds
            .nCurrency = nCurrency
            .nAmount = nAmount
            .dEffecdate = dEffecdate
            .sFoundDescript = sFoundDescript
            .nQuan_avail = nQuan_avail
            .dNulldate = dNulldate
        End With

        mCol.Add(objNewMember, CStr(nFunds) & CStr(dEffecdate))

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	'**% Find: This function is in charge of searching all nominal values belonging to a fund
	'% Find: Esta funci?n se encarga de buscar todos los Valores nominales pertenecientes a un Fondo.
    Public Function Find(ByVal nFunds As Integer, Optional ByVal nCurrency As Integer = 0) As Boolean
        Dim lrecreaFund_values As eRemoteDB.Execute

        lrecreaFund_values = New eRemoteDB.Execute

        On Error GoTo Find_Err

        Find = False

        '**+ Parameters definition to stored procedure 'insudb.reaFund_values'
        '**+ Data read on 04/09/2001 08:59:33 AM
        '+ Definici?n de par?metros para stored procedure 'insudb.reaFund_values'
        '+ Informaci?n le?da el 09/04/2001 08:59:33 AM

        With lrecreaFund_values
            .StoredProcedure = "reaFund_values"

            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If nCurrency = 0 Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If .Run Then
                Do While Not .EOF
                    Call Add(Fund_inv.eStatusInstance_f.eftQuery_f, nFunds, .FieldToClass("nCurrency"), .FieldToClass("nAmount"), .FieldToClass("dEffecDate"), eRemoteDB.Constants.dtmNull, String.Empty, eRemoteDB.Constants.intNull)
                    .RNext()
                Loop

                .RCloseRec()
                Find = True
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaFund_values may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFund_values = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function
	
	'**% FindFounds: Allows to select all funds with its nominal actives values for the date
	'% FindFounds: Selecciona todos los fondos con sus valores nominales activos para la fecha
    Public Function FindFounds(ByVal ldtmEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaFund_value_1 As eRemoteDB.Execute

        lrecreaFund_value_1 = New eRemoteDB.Execute

        On Error GoTo FindFounds_Err

        FindFounds = True

        If ldtmEffecdate <> mdtmEffecdate Or lblnFind Then
            'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mCol = Nothing

            '**+ Parameters definition to stored procedure 'insudb.reaFund_value_1'
            '**+ Data read on 04/09/2001 17:45:17
            '+ Definici?n de par?metros para stored procedure 'insudb.reaFund_value_1'
            '+ Informaci?n le?da el 09/04/2001 17:45:17

            With lrecreaFund_value_1
                .StoredProcedure = "reaFund_value_1"

                .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



                If .Run Then
                    mdtmEffecdate = ldtmEffecdate

                    Do While Not .EOF
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        Call Add(Fund_inv.eStatusInstance_f.eftExist_f, .FieldToClass("nFunds", 0), .FieldToClass("nCurrency", 0), .FieldToClass("nAmount", 0), .FieldToClass("dEffecdate", System.DBNull.Value), .FieldToClass("dNulldate"), .FieldToClass("sDescript", String.Empty), .FieldToClass("nQuan_avail", 0))
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

        'UPGRADE_NOTE: Object lrecreaFund_value_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFund_value_1 = Nothing
    End Function
	
	'**% FindDateMax: Allows to select the last date of Fund_values modification
	'% FindDateMax: Selecciona la ?ltima fecha de modificaci?n de Fund_values
	Public Function FindDateMax(Optional ByVal nFunds As Integer = 0, Optional ByVal ldtmDate As Date = #12:00:00 AM#) As Date
		Dim lrecvalFund_value As eRemoteDB.Execute
		
		lrecvalFund_value = New eRemoteDB.Execute
		
		'**+ Parameters definition to stored procedure 'insudb.valFund_value'
		'**+ Data read on 04/09/2001 11:21:06
		'+ Definici?n de par?metros para stored procedure 'insudb.valFund_value'
		'+ Informaci?n le?da el 09/04/2001 11:21:06
		
		With lrecvalFund_value
			.StoredProcedure = "valFund_value"
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				FindDateMax = IIf(IsDbNull(.Parameters.Item("dEffecdate").Value), #1/1/1800#, .Parameters.Item("dEffecdate").Value)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecvalFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalFund_value = Nothing
	End Function
	
	'**% Update: Updates the collection record in the nominal table of the nominals values fund " Fund_value".
	'% Update : Permite actualizar los registros de la colecci?n en la tabla de valores nominales de fondo "Fund_value".
	Public Function Update() As Boolean
		Dim lclsFund_value As Fund_value
		
		On Error GoTo Update_Err
		
		Update = True
		
		For	Each lclsFund_value In mCol
			With lclsFund_value
				Select Case .nStatInstanc
                    Case eBranches.Insured_he.eStatusInstance.eftNew
                        Update = .Add()
                    Case eBranches.Insured_he.eStatusInstance.eftUpDate
                        Update = .Update()
                    Case eBranches.Insured_he.eStatusInstance.eftDelete
                        Update = .Delete()
                        mCol.Remove((CStr(.nFunds)))
                End Select
			End With
		Next lclsFund_value
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
	End Function
	
	'*** Item: Use when making reference to an element of the collection
	'*** vntIndexKey contains the index or the password of the collection,
	'*** and that is why it is declared as a variant
	'*** Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'* Item: Se usa al hacer referencia a un elemento de la colecci?n
	'* vntIndexKey contiene el ?ndice o la clave de la colecci?n,
	'* por lo que se declara como un Variant
	'* Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Fund_value
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Returns the number of elements that the collection has
	'* Count: Devuelve el n?mero de elementos que posee la colecci?n
	Public ReadOnly Property Count() As Integer
		Get
			
			'**+ Used when obtaining the number of elemnets of the
			'**+ collection. Sytnax: Debug.print x.Count
			'+ Se usa al obtener el n?mero de elementos de la
			'+ colecci?n. Sintaxis: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Enumerates the collection for use in a For Each...Next loop
	'* NewEnum: Permite enumerar la colecci?n para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'** This property allows to enumerate
			'** this collection with the syntax For...Each
			'+ Esta propiedad permite enumerar
			'+ esta colecci?n con la sintaxis For...Each
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colecci?n
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'**+ Used when taking an element from the collection
		'**+ vntIndexKey contains the index or the password, and
		'**+ that is why it is declared as a variant
		'**+ Syntax: x.Remove (xyz)
		'+ Se usa al quitar un elemento de la colecci?n
		'+ vntIndexKey contiene el ?ndice o la clave, por lo que se
		'+ declara como un Variant
		'+ Sintaxis: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creaci?n de una instancia de la colecci?n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'**+ Creates the collection when the class is created
		'+ Crea la colecci?n cuando se crea la clase
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the destruction of an instance of the collection
	'% Class_Terminate: Controla la destrucci?n de una instancia de la colecci?n
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'**+ Deletes the collection when the class is finished
		'+ Destruye la colecci?n cuando se termina la clase
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






