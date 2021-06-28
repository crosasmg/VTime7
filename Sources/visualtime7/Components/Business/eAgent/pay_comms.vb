Option Strict Off
Option Explicit On
Public Class pay_comms
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: pay_comms.cls                            $%'
	'% $Author:: Nvaplat53                                  $%'
	'% $Date:: 8/09/04 4:17p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Private mcolpay_comm As Collection
	
	'%Add: Añade una nueva instancia de la clase "pay_comm" a la colección
	Public Function Add(ByVal nIntermed As Integer, ByVal nId As Integer, ByVal nPay_Comm As Integer, ByVal nBranch As Integer, ByVal sDesBranch As String, ByVal nProduct As Integer, ByVal sDesProduct As String, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nOricurr As Integer, ByVal sTitularc As String, ByVal nDocnumbe As Integer, ByVal dPay_date As Date, ByVal nCom_Afec As Double, ByVal nCom_exen As Double, ByVal nTotorigi As Double, ByVal nTotlocal As Double, ByVal dCompdate As Date, ByVal nDoctype As Integer, ByVal dVal_Date As Date, Optional ByVal sKey As String = "", Optional ByVal nTax As Double = 0, Optional ByVal nTaxloc As Double = 0) As pay_comm
		Dim lclsPay_comm As pay_comm
		
		lclsPay_comm = New pay_comm
		
		With lclsPay_comm
			.nIntermed = nIntermed
			.nId = nId
			.nPay_Comm = nPay_Comm
			.nBranch = nBranch
			.sDesBranch = sDesBranch
			.nProduct = nProduct
			.sDesProduct = sDesProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nOricurr = nOricurr
			.sTitularc = sTitularc
			.nDocnumbe = nDocnumbe
			.dPay_date = dPay_date
			.nCom_Afec = nCom_Afec
			.nCom_exen = nCom_exen
			.nTotorigi = nTotorigi
			.nTotlocal = nTotlocal
			.dCompdate = dCompdate
			.nDoctype = nDoctype
			.dVal_Date = dVal_Date
			.nTax = nTax
			.nTaxloc = nTaxloc
		End With
		
		'set the properties passed into the method
		If sKey = String.Empty Then
			mcolpay_comm.Add(lclsPay_comm)
		Else
			mcolpay_comm.Add(lclsPay_comm, sKey)
		End If
		
		'return the object created
		Add = lclsPay_comm
		'UPGRADE_NOTE: Object lclsPay_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPay_comm = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'pay_comm'
	Public Function Find(ByVal nIntermed As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date, ByVal nPay_Comm As Integer) As Boolean
		Dim lclsPay_comm As eRemoteDB.Execute
		Dim nreg As Integer
		lclsPay_comm = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reapay_comm'. Generated on 14/02/2002 09:49:04 a.m.
		
		With lclsPay_comm
			.StoredProcedure = "reapay_comm_a"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateIni", dEffecdateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateEnd", dEffecdateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_Comm", nPay_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				nreg = 0
				Do While Not .EOF
					nreg = nreg + 1
					Call Add(.FieldToClass("nIntermed"), .FieldToClass("nid"), .FieldToClass("nPay_Comm"), .FieldToClass("nBranch"), .FieldToClass("sDesBranch"), .FieldToClass("nProduct"), .FieldToClass("sDesProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nOricurr"), .FieldToClass("sTitularc"), .FieldToClass("nDocnumbe"), .FieldToClass("dPay_Date"), .FieldToClass("nCom_Afec"), .FieldToClass("nCom_exen"), .FieldToClass("nTotorigi"), .FieldToClass("nTotlocal"), .FieldToClass("dCompdate"), .FieldToClass("nDoctype"), .FieldToClass("dVal_Date"), CStr(nreg), .FieldToClass("nTax"), .FieldToClass("nTaxloc"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsPay_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPay_comm = Nothing
    End Function
    'Find: Función que realiza la busqueda en la tabla 'pay_comm'
    Public Function Findagc621a(ByVal nIntermed As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date, ByVal nPay_Comm As Integer) As Boolean
        Dim lclsPay_comm As eRemoteDB.Execute
        Dim nreg As Integer
        lclsPay_comm = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reapay_comm'. Generated on 14/02/2002 09:49:04 a.m.

        With lclsPay_comm
            .StoredProcedure = "reapay_comm_l"
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdateIni", dEffecdateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdateEnd", dEffecdateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_Comm", nPay_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                nreg = 0
                Do While Not .EOF
                    nreg = nreg + 1
                    Call Add(.FieldToClass("nIntermed"), .FieldToClass("nid"), .FieldToClass("nPay_Comm"), .FieldToClass("nBranch"), .FieldToClass("sDesBranch"), .FieldToClass("nProduct"), .FieldToClass("sDesProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nOricurr"), .FieldToClass("sTitularc"), .FieldToClass("nDocnumbe"), .FieldToClass("dPay_Date"), .FieldToClass("nCom_Afec"), .FieldToClass("nCom_exen"), .FieldToClass("nTotorigi"), .FieldToClass("nTotlocal"), dtmNull, .FieldToClass("nDoctype"), dtmNull, CStr(nreg), .FieldToClass("nTax"), .FieldToClass("nTaxloc"))
                    .RNext()
                Loop
                Findagc621a = True
                .RCloseRec()

            Else
                Findagc621a = False
            End If
        End With
        'UPGRADE_NOTE: Object lclsPay_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPay_comm = Nothing
    End Function
	'**%Add: Adds a new class instance to the collection
	'%Add: se añade una nueva instancia de la clase a la colección
	Public Function Add_AGL703(ByVal objClass As pay_comm) As pay_comm
		With objClass
			mcolpay_comm.Add(objClass, .nIntertyp & .sIntertyp & .nPay_Comm & .dPay_date & .dProcSup & .dVal_Date & .dCompdate)
		End With
		
		'return the object created
		Add_AGL703 = objClass
	End Function
	'%FindAGL703: Permite obtener los Procesos de Liquidacion para LT de Comisiones Pagadas.
	Public Function FindAGL703(ByVal dInitDate As Date, ByVal dEnddate As Date) As Boolean
		Dim lrecFindAGL703 As eRemoteDB.Execute
		Dim lclsPay_comm As pay_comm
		
		lrecFindAGL703 = New eRemoteDB.Execute
		
		FindAGL703 = True
		
		On Error GoTo FindAGL703_Err
		
		'**+Stored procedure parameters definition 'insudb.insReaCOC747'.
		'+Definición de parámetros para stored procedure 'insudb.insReaCOC747'.
		
		With lrecFindAGL703
			.StoredProcedure = "insfindAGL703"
			.Parameters.Add("ddateini", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ddateend", dEnddate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsPay_comm = New pay_comm
					With lclsPay_comm
						.nIntertyp = lrecFindAGL703.FieldToClass("nInterTyp")
						.sIntertyp = lrecFindAGL703.FieldToClass("sIntertyp")
						.nPay_Comm = lrecFindAGL703.FieldToClass("nPay_Comm")
						.dPay_date = lrecFindAGL703.FieldToClass("dPay_Date")
						.dProcSup = lrecFindAGL703.FieldToClass("dProcSup")
						.dVal_Date = lrecFindAGL703.FieldToClass("dVal_Date")
						.dCompdate = lrecFindAGL703.FieldToClass("dCompdate")
					End With
					Call Add_AGL703(lclsPay_comm)
					.RNext()
					'UPGRADE_NOTE: Object lclsPay_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPay_comm = Nothing
				Loop 
				.RCloseRec()
			Else
				FindAGL703 = False
				
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecFindAGL703 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindAGL703 = Nothing
		
		
FindAGL703_Err: 
		If Err.Number Then
			FindAGL703 = False
		End If
		
		On Error GoTo 0
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As pay_comm
		Get
			Item = mcolpay_comm.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolpay_comm.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolpay_comm._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolpay_comm.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolpay_comm.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolpay_comm = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolpay_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolpay_comm = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






