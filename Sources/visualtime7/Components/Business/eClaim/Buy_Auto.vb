Option Strict Off
Option Explicit On
Public Class Buy_Auto
	'%-------------------------------------------------------%'
	'% $Workfile:: Buy_Auto.cls                             $%'
	'% $Author:: Nvaplat22                                   $%'
	'% $Date:: 5/12/03 1:20a                                 $%'
	'% $Revision:: 2                                         $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.buy_auto al 12-02-2003 18:34:41
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nServ_ord As Integer ' NUMBER     22   0     10   N
	Public nNum_Ord As Integer ' NUMBER     22   0     10   N
	Public dBuydate As Date ' DATE       7    0     0    N
	Public sClient As String ' CHAR       14   0     0    N
	Public sSel As String ' CHAR       1    0     0    S
	Public sCondic As String ' CHAR       60   0     0    S
	Public sClient1 As String ' CHAR       14   0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	'
	Public nArea_code As Integer
	Public sPhone As String
	'
	
	'%InsValSI831_K: Validaciones de la transacción(Header)
	Public Function InsValSI831_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal dBuydate As Date, ByVal nServ_ord As Double) As String
		Dim lrecInsSI831_K As eRemoteDB.Execute
		Dim lclsErrors As New eFunctions.Errors
		Dim lstrError As String
		
		On Error GoTo insValSI831_K_Err
		
		lrecInsSI831_K = New eRemoteDB.Execute
		
		With lrecInsSI831_K
			.StoredProcedure = "insSi831pkg.insvalSI831_K"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBuydate", dBuydate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_Ord", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lclsErrors = New eFunctions.Errors
				With lclsErrors
					.ErrorMessage("SI831_K",  ,  ,  ,  ,  , lstrError)
					InsValSI831_K = lclsErrors.Confirm
				End With
			End If
		End With
		
insValSI831_K_Err:
        If Err.Number Then
            InsValSI831_K = ""
            InsValSI831_K = InsValSI831_K & " " & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
		lrecInsSI831_K = Nothing
	End Function
	
	'% insPostSI830: Ejecuta la actualización de registros sobre las tablas Buy_Auto, Prof_ord y
	'%               Client, Address en caso de ser necesario
	Public Function insPostSI831(ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nServiceOrder As Integer, ByVal dOrderDate As String, ByVal sClientCode As String, ByVal sCondic As String, ByVal nUsercode As Integer, Optional ByVal nIVA As Double = 0, Optional ByVal sClientcon As String = "", Optional ByVal sName_Cont As String = "", Optional ByVal sPhone_Cont As String = "", Optional ByVal sAdd_Contact As String = "", Optional ByVal nMunicipality As Integer = 0) As Boolean
		Dim lrecinsPostSI831 As New eRemoteDB.Execute
		Dim lclsProf_ord As Prof_ord
		Dim nServ_Order As Double
		Dim nValid As Short
		
		On Error GoTo insPostSI831_err
		
		lclsProf_ord = New Prof_ord
		lrecinsPostSI831 = New eRemoteDB.Execute
		
		If lclsProf_ord.Find_nServ(nServiceOrder) Then
			lclsProf_ord.nTransac = 0
			lclsProf_ord.nServ_Order = 0
			lclsProf_ord.nStatus_ord = 3 '+ Realizada
			lclsProf_ord.nOrdertype = 7 '+ Orden de Compra del Vehículo
			lclsProf_ord.nAction = 1
			lclsProf_ord.sName_Cont = sName_Cont
			lclsProf_ord.sPhone_Cont = sPhone_Cont
			lclsProf_ord.sAdd_Contact = sAdd_Contact
			lclsProf_ord.nMunicipality = nMunicipality
			lclsProf_ord.nIVA = nIVA
			lclsProf_ord.nQuotpart_order = nServiceOrder
			insPostSI831 = lclsProf_ord.Update_ProfOrdGeneric()
			Me.nServ_ord = lclsProf_ord.nServ_Order
			
			If insPostSI831 Then
				With lrecinsPostSI831
					.StoredProcedure = "insSi831pkg.InsPostSI831"
					.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCase_Num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nDeman_Type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nServ_Order", lclsProf_ord.nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nNum_Ord", nServiceOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dBuydate", dOrderDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCondic", sCondic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClientcon", sClientcon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nValid", nValid, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Run(False)
					insPostSI831 = .Parameters("nValid").Value = 1
				End With
			End If
		End If
		
insPostSI831_err: 
		If Err.Number Then
			insPostSI831 = False
		End If
		On Error GoTo 0
		lclsProf_ord = Nothing
		lrecinsPostSI831 = Nothing
	End Function
	
	'% Find: Permite buscar registros en la tabla de Phones
	Function FindPhone(ByVal sKeyAddress As String, ByVal nKeyPhones As Integer, ByVal nRecOwner As eGeneralForm.Address.eTypeRecOwner, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaPhones As eRemoteDB.Execute
		lrecreaPhones = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'Definición de parámetros para stored procedure 'insudb.reaPhones'
		'Información leída el 12/07/2000 14:40:30
		With lrecreaPhones
			.StoredProcedure = "reaPhonesk"
			.Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeyPhones", nKeyPhones, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAll", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			FindPhone = .Run
			If FindPhone Then
				Me.nArea_code = .FieldToClass("nArea_code")
				Me.sPhone = .FieldToClass("sPhone")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			FindPhone = False
		End If
		On Error GoTo 0
		lrecreaPhones = Nothing
	End Function
	
	'% Find: Permite buscar registros en la tabla de Phones
	Function Find(ByVal nServ_ord As Double, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecFind As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecFind = New eRemoteDB.Execute
		
		With lrecFind
			.StoredProcedure = "reaBuy_Auto"
			.Parameters.Add("nServ_ord", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				Me.nServ_ord = .FieldToClass("nServ_Ord")
				Me.nNum_Ord = .FieldToClass("nNum_Ord")
				Me.dBuydate = .FieldToClass("dBuydate")
				Me.sClient = .FieldToClass("sClient")
				Me.sSel = .FieldToClass("sSel")
				Me.sCondic = .FieldToClass("sCondic")
				Me.sClient1 = .FieldToClass("sClient1")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.dCompdate = .FieldToClass("dCompdate")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecFind = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_ord = eRemoteDB.Constants.intNull
		nNum_Ord = eRemoteDB.Constants.intNull
		dBuydate = eRemoteDB.Constants.dtmNull
		sClient = String.Empty
		sSel = String.Empty
		sCondic = String.Empty
		sClient1 = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






