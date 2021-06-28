Option Strict Off
Option Explicit On
Public Class Recovers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Recovers.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'%Find_RecoverCover: Realiza la consulta de los ingresos por recobro que se han realizado
    Public Function Find_RecoverCover(ByVal nClaim As Double, Optional nRecover As Integer = eRemoteDB.Constants.intNull) As Boolean
        Dim lrecRecoversCover As eRemoteDB.Execute

        On Error GoTo Find_RecoverCover_err

        lrecRecoversCover = New eRemoteDB.Execute

        Find_RecoverCover = True

        With lrecRecoversCover
            .StoredProcedure = "reaRecoverCovers"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecover", nRecover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                While Not .EOF
                    Call Add(eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CDate(Nothing), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CDate(Nothing), .FieldToClass("sClient"), String.Empty, CDate(Nothing), eRemoteDB.Constants.intNull, CDate(Nothing), CDbl(Nothing), eRemoteDB.Constants.intNull, String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("nCover"), .FieldToClass("sDescript"), .FieldToClass("nModuleC"), String.Empty, .FieldToClass("sCliename"))
                    .RNext()
                End While
            Else
                Find_RecoverCover = False
            End If
        End With

Find_RecoverCover_err:
        If Err.Number Then
            Find_RecoverCover = False
        End If
        On Error GoTo 0
        lrecRecoversCover = Nothing
    End Function
	
	'% Add :Incluye un nuevo elemento en la colección
	Public Function Add(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nTransac As Integer, ByVal dCompdate As Date, ByVal nRecover_typ As Double, ByVal nCost_recu As Double, ByVal nCostl_recu As Double, ByVal nCurrency As Integer, ByVal nEs_cos_re As Double, ByVal nEs_inc_re As Double, ByVal nEsl_cos_re As Double, ByVal nEsl_inc_re As Double, ByVal dEstdate As Date, ByVal sClient As String, ByVal sNum_case As String, ByVal dPresdate As Date, ByVal nRec_amount As Double, ByVal dRecdate As Date, ByVal nRecl_amoun As Double, ByVal nProvider As Integer, ByVal sStatregt As String, ByVal sTribunal As String, ByVal nUsercode As Integer, ByVal nNotenum As Double, ByVal sCurrencyDescript As String, ByVal nCover As Integer, ByVal sCoverDescript As String, ByVal nModulec As Integer, Optional ByVal sKey As String = "", Optional ByVal sCliename As String = "") As Recover
		Dim objNewMember As Recover
		objNewMember = New Recover
		
		With objNewMember
			.nModulec = nModulec
			.sCoverDescript = sCoverDescript
			.nCover = nCover
			.sCurrencyDescript = sCurrencyDescript
			.nNotenum = nNotenum
			.nUsercode = nUsercode
			.sTribunal = sTribunal
			.sStatregt = sStatregt
			.nProvider = nProvider
			.nRecl_amoun = nRecl_amoun
			.dRecdate = dRecdate
			.nRec_amount = nRec_amount
			.dPresdate = dPresdate
			.sNum_case = sNum_case
			.sClient = sClient
			.dEstdate = dEstdate
			.nEsl_inc_re = nEsl_inc_re
			.nEsl_cos_re = nEsl_cos_re
			.nEs_inc_re = nEs_inc_re
			.nEs_cos_re = nEs_cos_re
			.nCurrency = nCurrency
			.nCostl_recu = nCostl_recu
			.nCost_recu = nCost_recu
			.nRecover_typ = nRecover_typ
			.dCompdate = dCompdate
			.nTransac = nTransac
			.nDeman_type = nDeman_type
			.nCase_num = nCase_num
			.nClaim = nClaim
			.sCliename = sCliename
		End With
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		
		'+ Devuelve el elemento de la colección
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	
	'%Item: Obtiene el valor del elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Recover
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Obtiene el número de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Obtiene el número de un elemento de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Inicializa los elementos de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Destruye los elementos involucrados en la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






