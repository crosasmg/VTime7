Option Strict Off
Option Explicit On
Public Class Settlements
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	'% Add: se agrega un nuevo objeto a la colección
	Public Function Add(ByVal nClaim As Double, ByVal nSettlement As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nPaid_Amoun As Double, ByVal dPrinted_Da As Date, ByVal dPropou_Dat As Date, ByVal sStatus_Fin As String, ByVal nId As Integer) As Settlement
		Dim objNewMember As Settlement
		objNewMember = New Settlement
		
		With objNewMember
			.nClaim = nClaim
			.nSettlement = nSettlement
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.sClient = sClient
			.nAmount = nAmount
			.nCurrency = nCurrency
			.nPaid_Amoun = nPaid_Amoun
			.dPrinted_Da = dPrinted_Da
			.dPropou_Dat = dPropou_Dat
			.sStatus_Fin = sStatus_Fin
			.nId = nId
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		objNewMember = Nothing
    End Function

    Public Function AddSI764(ByVal nSettlecode As Integer, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal nId_settle As Double, ByVal sFormatname As String, ByVal sTips As String, ByVal dPropou_Dat As Date, ByVal nId As Integer, ByVal nCurrency As Integer, ByVal sStatus_Fin As String) As Settlement
        Dim objNewMember As Settlement
        objNewMember = New Settlement

        With objNewMember
            .nSettlecode = nSettlecode
            .nId_settle = nId_settle
            .sFormatname = sFormatname
            .dPropou_Dat = dPropou_Dat
            .nId = nId
            .nCurrency = nCurrency
            .sStatus_Fin = sStatus_Fin
            .nPay_concep = nPay_concep
            .nCover = nCover
            .sTips = sTips

        End With

        mCol.Add(objNewMember)

        AddSI764 = objNewMember
        objNewMember = Nothing
    End Function

    Public Function AddCheck(ByVal Exist As Integer)
        Dim objNewMember As Settlement
        objNewMember = New Settlement

        With objNewMember
            .Exist = Exist

        End With

        mCol.Add(objNewMember)

        AddCheck = objNewMember
        objNewMember = Nothing
    End Function
	
	'% Find: Localiza todos los finiquitos de un siniestro-caso
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnRead As Boolean = True) As Boolean
		Dim lrecSettlement As eRemoteDB.Execute
		
		Static ldblOldClaim As Double
		Static llngOldCase_num As Integer
		Static llngOldDeman_type As Integer
		
		On Error GoTo Find
		
		If ldblOldClaim <> nClaim Or llngOldCase_num <> nCase_num Or llngOldDeman_type <> nDeman_type Or lblnRead Then
			
			ldblOldClaim = nClaim
			llngOldCase_num = nCase_num
			llngOldDeman_type = nDeman_type
			
			lrecSettlement = New eRemoteDB.Execute
			
			With lrecSettlement
				.StoredProcedure = "reaSettlement_1" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then 'Listo
					Do While Not .EOF
						'**+ Add the record to the class - ACM - 01/30/2001
						'+ Se añade el registro a la clase - ACM - 30/01/2001
						Call Add(.FieldToClass("nClaim"), .FieldToClass("nSettle_num"), .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), .FieldToClass("sClient"), .FieldToClass("nAmount"), .FieldToClass("nCurrency"), .FieldToClass("nPaid_Amoun"), .FieldToClass("dPrinted_da"), .FieldToClass("dPropou_dat"), .FieldToClass("sStatus_Fin"), .FieldToClass("nId"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			
			lrecSettlement = Nothing
		End If
Find: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
    End Function

    '% Find: Localiza todos los finiquitos de un siniestro-caso

    Public Function Find_SI764(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal lblnRead As Boolean = True) As Boolean
        Dim lrecSettlement As eRemoteDB.Execute

        Static ldblOldClaim As Double
        Static llngOldCase_num As Integer
        Static llngOldDeman_type As Integer

        On Error GoTo Find_SI764

        If nCase_num > 0 Or nDeman_type > 0 Then

            lrecSettlement = New eRemoteDB.Execute

            With lrecSettlement
                .StoredProcedure = "reasettlement_det" 'Listo
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then 'Listo
                    Do While Not .EOF
                        '**+ Add the record to the class - ACM - 01/30/2001
                        '+ Se añade el registro a la clase - ACM - 30/01/2001
                        Call AddSI764(.FieldToClass("nSettlecode"), _
                                      .FieldToClass("nCover"), _
                                      .FieldToClass("nPay_concep"), _
                                      .FieldToClass("nId_settle"), _
                                      .FieldToClass("sFormatname"), _
                                      .FieldToClass("Tips"), _
                                      .FieldToClass("dPropou_Dat"), _
                                      .FieldToClass("nId"), _
                                      .FieldToClass("nCurrency"), _
                                      .FieldToClass("sStatus_Fin"))

                        .RNext()
                    Loop
                    .RCloseRec()
                    Find_SI764 = True
                Else
                    Find_SI764 = False
                End If
            End With

            lrecSettlement = Nothing
        End If
Find_SI764:
        If Err.Number Then
            Find_SI764 = False
        End If
        On Error GoTo 0
    End Function

    Public Function FindCheck(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nSettlecode As Integer) As Boolean
        Dim lrecSett_Check As eRemoteDB.Execute
        lrecSett_Check = New eRemoteDB.Execute

        With lrecSett_Check

            .StoredProcedure = "reasettlement_count" 'Listo
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSettlecode", nSettlecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then 'Listo
                Do While Not .EOF
                    '**+ Add the record to the class - ACM - 01/30/2001
                    '+ Se añade el registro a la clase - ACM - 30/01/2001
                    Call AddCheck(.FieldToClass("Exist"))

                    .RNext()
                Loop
                .RCloseRec()
                FindCheck = True
            Else
                FindCheck = False
            End If
        End With

        lrecSett_Check = Nothing
FindCheck:
        If Err.Number Then
            FindCheck = False
        End If
        On Error GoTo 0
    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Settlement
        Get
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'this property allows you to enumerate
    'this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
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
        mCol.Remove(vntIndexKey)
    End Sub

    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Private Sub Class_Terminate_Renamed()
        'destroys collection when this class is terminated
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






