Option Strict Off
Option Explicit On
Public Class Errors
	Implements System.Collections.IEnumerable
	Private mCol As Collection
	
	
	
	'% Add: Carga la coleccion de los registros especificos de una transcaccion con un estado
	'%      determinado
    Public Function Add(ByVal nErrorNum As Integer, ByVal sDescript As String, Optional ByVal sSrc_Descript As String = "") As ErrorTyp
        Dim objNewMember As ErrorTyp

        If Not IsIDEMode() Then
        End If
        objNewMember = New ErrorTyp

        With objNewMember
            .nErrorNum = nErrorNum
            .sDescript = sDescript
            .sSrc_Descript = sSrc_Descript
        End With

        mCol.Add(objNewMember, "Err" & nErrorNum)

        Add = objNewMember

        objNewMember = Nothing

        Exit Function
    End Function
	
	'% Add: Carga la coleccion de los registros especificos de una transcaccion con un estado
	'%      determinado
	Public Function Add_T_Errors(ByVal objError As ErrorTyp) As ErrorTyp
		If Not IsIDEMode Then
		End If
		
		mCol.Add(objError, "Err" & objError.nErrorNum)
		Add_T_Errors = objError
		objError = Nothing
		
		Exit Function
	End Function
	
	'%insPreEr007: Lee los datos de la t_errors
	Public Function Find_T_Errors(ByVal nSessionId As String, ByVal nUsercode As Integer) As Boolean
		Dim skey1 As Object
		Dim lrecEr007 As eRemoteDB.Execute
		Dim lclsError As ErrorTyp
		
		If Not IsIDEMode Then
		End If
		
		lrecEr007 = New eRemoteDB.Execute
		
		lclsError = New ErrorTyp
		skey1 = lclsError.sKey(nUsercode, nSessionId)
		lclsError = Nothing
		
		With lrecEr007
			.StoredProcedure = "Find_T_Errors"
			.Parameters.Add("sKey", skey1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			
			If .Run Then
				Do While Not .EOF
					lclsError = New ErrorTyp
					lclsError.nErrorNum = .FieldToClass("nErrorNum")
					lclsError.sCodisp = .FieldToClass("sCodisp")
					lclsError.sDescript_win = .FieldToClass("sDescript_win")
					lclsError.sStat_error = .FieldToClass("sStat_error")
					lclsError.nSource = .FieldToClass("nSource")
					lclsError.nPriority = .FieldToClass("nPriority")
					lclsError.nSeverity = .FieldToClass("nSeverity")
					lclsError.nModule_Err = .FieldToClass("nModule_Err")
					lclsError.dDat_assign = .FieldToClass("dDat_assign")
					lclsError.nDays_user = .FieldToClass("nDays_user")
					lclsError.sHour_user = .FieldToClass("sHour_user")
                    lclsError.sUse_assign = .FieldToClass("sUse_asign")

                    Call Add_T_Errors(lclsError)
					
					lclsError = Nothing
					.RNext()
				Loop 
				Find_T_Errors = True
				.RCloseRec()
			End If
		End With
		
		lrecEr007 = Nothing
		lclsError = Nothing
		
		Exit Function
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ErrorTyp
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
			Exit Property
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Errors.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		If Not IsIDEMode Then
		End If
		
		mCol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Localiza todos los Errores relaciionados a una Transaccion y a un Estado
    Public Function Find(ByVal sCodisp As String, ByVal sStat_error As String, ByVal nSrc_Error As Integer) As Boolean
        Dim lrecreaErrorq As eRemoteDB.Execute

        If Not IsIDEMode() Then
        End If
        lrecreaErrorq = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaErrorq'
        '+ Información leída el 06/06/2001 05:05:17 PM

        mCol = Nothing
        mCol = New Collection

        With lrecreaErrorq
            .StoredProcedure = "reaErrorq"
            .Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStat_error", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSrc_error", nSrc_Error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .bErr_Module = True

            If .Run Then
                Find = True
                While Not .EOF
                    Call Add(.FieldToClass("nErrorNum"), .FieldToClass("sDescript"), .FieldToClass("sSrc_Descript"))
                    .RNext()
                End While
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lrecreaErrorq = Nothing

        Exit Function
    End Function
	
	'% Find_Er007: Localiza todos los Errores relaciionados a una Transaccion y a un Estado
	Public Function Find_Er007(ByVal sCodisp As String, ByVal sStat_error As String) As Boolean
		Dim lrecreaErrorq As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecreaErrorq = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaErrorq'
		'+ Información leída el 06/06/2001 05:05:17 PM
		
		mCol = Nothing
		mCol = New Collection
		
		With lrecreaErrorq
            .StoredProcedure = "reaEr_007"  'DEATH_CODE: Procedure not found
            .Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStat_error", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .bErr_Module = True

            If .Run Then
                Find_Er007 = True
                While Not .EOF
                    Call Add(.FieldToClass("nErrorNum"), .FieldToClass("sDescript"))
                    .RNext()
                End While
                .RCloseRec()
            Else
                Find_Er007 = False
            End If
        End With
		
		lrecreaErrorq = Nothing
		
		Exit Function
	End Function
End Class











