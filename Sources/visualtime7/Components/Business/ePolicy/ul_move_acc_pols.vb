Option Strict Off
Option Explicit On
Public Class ul_Move_Acc_pols
	Implements System.Collections.IEnumerable
	'- Variable local donde se almacena la colección
	
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase "ul_Move_Acc_pol" a la colección
	Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, ByVal dOperDate As Date, ByVal nType_Move As Integer, ByVal nIdconsec As Integer, ByVal nCredit As Double, ByVal nDebit As Double, ByVal dCompdate As Date, ByVal nUsercode As Integer, ByVal nReceipt As Integer, ByVal sPayer As String, ByVal nInstitution As Integer, ByVal nIntermei As Integer, ByVal nOrigin As Integer, ByVal dDate_Origin As Date, ByVal nInvested As Integer, ByVal dPosted As Date) As ul_Move_Acc_pol
		'- Se crea un objeto nuevo
		
		Dim objNewMember As ul_Move_Acc_pol
		
		objNewMember = New ul_Move_Acc_pol
		
		'+ Se setean las propiedades pasado al metodo
		
		objNewMember.sCertype = sCertype
		objNewMember.nBranch = nBranch
		objNewMember.nProduct = nProduct
		objNewMember.nPolicy = nPolicy
		objNewMember.nCertif = nCertif
		objNewMember.nCurrency = nCurrency
		objNewMember.dOperDate = dOperDate
		objNewMember.nType_Move = nType_Move
		objNewMember.nIdconsec = nIdconsec
		objNewMember.nCredit = nCredit
		objNewMember.nDebit = nDebit
		objNewMember.dCompdate = dCompdate
		objNewMember.nUsercode = nUsercode
		objNewMember.nReceipt = nReceipt
		objNewMember.sPayer = sPayer
		objNewMember.nInstitution = nInstitution
		objNewMember.nIntermei = nIntermei
		objNewMember.nOrigin = nOrigin
		objNewMember.dDate_Origin = dDate_Origin
		objNewMember.nInvested = nInvested
		objNewMember.dPosted = dPosted
		
		mCol.Add(objNewMember, sCertype & RTrim(CStr(nBranch)) & RTrim(CStr(nProduct)) & RTrim(CStr(nPolicy)) & RTrim(CStr(nCertif)) & RTrim(CStr(dOperDate)) & RTrim(CStr(nIdconsec)))
		
		'+ Se retorna el objeto creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Find_v: Este metodo carga la coleccion de elementos de la tabla "Move_Acc" devolviendo
	'%         Verdadero o falso, dependiendo de la existencia de los registros.
    Public Function Find_v(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal nCurrency As Integer = 0, Optional ByVal nType_Move As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = -1, Optional ByVal dOperDate As Date = #12:00:00 AM#) As Boolean
        Find_v = False

        Dim lrecReaMove_Acc As eRemoteDB.Execute

        lrecReaMove_Acc = New eRemoteDB.Execute

        On Error GoTo Find_v_Err

        With lrecReaMove_Acc
            .StoredProcedure = "reaUl_Move_Acc_pol1"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If nCurrency <> 0 Then
                .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If nType_Move <> 0 Then
                .Parameters.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nType_Move", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If nPolicy <> 0 Then
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If nCertif <> -1 Then
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If Not IsNothing(dOperDate) Then
                .Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("dOperdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If .Run Then
                Do While Not .EOF
                    Call Add(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nCurrency"), .FieldToClass("dOperdate"), .FieldToClass("nType_move"), .FieldToClass("nIdconsec"), .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("dCompdate"), .FieldToClass("nUsercode"), .FieldToClass("nReceipt"), .FieldToClass("sPayer"), .FieldToClass("nInstitution"), .FieldToClass("nIntermei"), .FieldToClass("nOrigin"), .FieldToClass("dDate_origin"), .FieldToClass("nInvested"), .FieldToClass("dPosted"))
                    .RNext()
                Loop

                .RCloseRec()
                Find_v = True
            End If
        End With

        'UPGRADE_NOTE: Object lrecReaMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaMove_Acc = Nothing

Find_v_Err:
        If Err.Number Then
            Find_v = False
        End If
    End Function
	
	'% Item: Toma un elemento de la colección
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ul_Move_Acc_pol
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Enumera los elementos dentro de la colección
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
	
	'% Remove: Elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Elimina la colección
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






