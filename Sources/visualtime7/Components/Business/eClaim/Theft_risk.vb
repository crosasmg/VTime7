Option Strict Off
Option Explicit On
Public Class Theft_risk
	'%-------------------------------------------------------%'
	'% $Workfile:: Theft_risk.cls                           $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 13/11/03 19.39                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla clobos.theft_risk al 04-23-2002 15:47:46
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0     10   N
	Public nSector_type As Integer ' NUMBER     22   0     5    S
	Public nLevel_sector As Integer ' NUMBER     22   0     5    S
	Public nLock_type As Integer ' NUMBER     22   0     5    S
	Public nWinprot As Integer ' NUMBER     22   0     1    N
	Public nProtec_type As Integer ' NUMBER     22   0     5    S
	Public nSta_elecpub As Integer ' NUMBER     22   0     1    N
	Public nSta_elecpriv As Integer ' NUMBER     22   0     1    N
	Public nDist_pol As Integer ' NUMBER     22   0     2    S
	Public sCorner As String ' CHAR       1    0     0    N
	Public sUrban As String ' CHAR       1    0     0    N
	Public sServer As String ' CHAR       1    0     0    N
	Public nNum_inhab As Double ' NUMBER     22   0     10   N
	Public nNum_beds As Double ' NUMBER     22   0     10   N
	
	'%InsUpdTheft_risk: Se encarga de actualizar la tabla Theft_risk
	Private Function InsUpdTheft_risk(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpdtheft_risk As eRemoteDB.Execute
		
		On Error GoTo insUpdtheft_risk_Err
		
		lrecinsUpdtheft_risk = New eRemoteDB.Execute
		
		With lrecinsUpdtheft_risk
			.StoredProcedure = "insUpdtheft_risk"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSector_type", nSector_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel_sector", nLevel_sector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLock_type", nLock_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWinprot", nWinprot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProtec_type", nProtec_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_elecpub", nSta_elecpub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_elecpriv", nSta_elecpriv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDist_pol", nDist_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCorner", sCorner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUrban", sUrban, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sServer", sServer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNum_inhab", nNum_inhab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNum_beds", nNum_beds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTheft_risk = .Run(False)
		End With
		
insUpdtheft_risk_Err: 
		If Err.Number Then
			InsUpdTheft_risk = False
		End If
		lrecinsUpdtheft_risk = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTheft_risk(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTheft_risk(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTheft_risk(3)
	End Function
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaTheft_risk As eRemoteDB.Execute
		Dim lclsTheft_risk As Theft_risk
		
		On Error GoTo reaTheft_risk_Err
		
		lrecreaTheft_risk = New eRemoteDB.Execute
		
		With lrecreaTheft_risk
			.StoredProcedure = "reaTheft_risk"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nServ_Order = .FieldToClass("nServ_order")
				nSector_type = .FieldToClass("nSector_type")
				nLevel_sector = .FieldToClass("nLevel_sector")
				nLock_type = .FieldToClass("nLock_type")
				nWinprot = .FieldToClass("nWinprot")
				nProtec_type = .FieldToClass("nProtec_type")
				nSta_elecpub = .FieldToClass("nSta_elecpub")
				nSta_elecpriv = .FieldToClass("nSta_elecpriv")
				nDist_pol = .FieldToClass("nDist_pol")
				sCorner = .FieldToClass("sCorner")
				sUrban = .FieldToClass("sUrban")
				sServer = .FieldToClass("sServer")
				nNum_inhab = .FieldToClass("nNum_inhab")
				nNum_beds = .FieldToClass("nNum_beds")
			Else
				Find = False
			End If
		End With
		
reaTheft_risk_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreaTheft_risk = Nothing
		On Error GoTo 0
	End Function
	'%InsPostOS592_3: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(OS592_3)
	Public Function InsPostOS592_3(ByVal sAction As String, ByVal nServ_Order As Double, ByVal nSector_type As Integer, ByVal nLevel_sector As Integer, ByVal nLock_type As Integer, ByVal nWinprot As Integer, ByVal nProtec_type As Integer, ByVal nSta_elecpub As Integer, ByVal nSta_elecpriv As Integer, ByVal nDist_pol As Integer, ByVal sCorner As String, ByVal sUrban As String, ByVal sServer As String, ByVal nNum_inhab As Double, ByVal nNum_beds As Double) As Boolean
		
		On Error GoTo InsPostOS592_3_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nSector_type = nSector_type
			.nLevel_sector = nLevel_sector
			.nLock_type = nLock_type
			.nWinprot = nWinprot
			.nProtec_type = nProtec_type
			.nSta_elecpub = nSta_elecpub
			.nSta_elecpriv = nSta_elecpriv
			.nDist_pol = nDist_pol
			.sCorner = sCorner
			.sUrban = sUrban
			.sServer = sServer
			.nNum_inhab = nNum_inhab
			.nNum_beds = nNum_beds
		End With
		
		Select Case sAction
			Case "Add"
				InsPostOS592_3 = Add
			Case "Update"
				InsPostOS592_3 = Update
			Case "Del"
				InsPostOS592_3 = Delete
		End Select
		
InsPostOS592_3_Err: 
		If Err.Number Then
			InsPostOS592_3 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		nSector_type = eRemoteDB.Constants.intNull
		nLevel_sector = eRemoteDB.Constants.intNull
		nLock_type = eRemoteDB.Constants.intNull
		nWinprot = eRemoteDB.Constants.intNull
		nProtec_type = eRemoteDB.Constants.intNull
		nSta_elecpub = eRemoteDB.Constants.intNull
		nSta_elecpriv = eRemoteDB.Constants.intNull
		nDist_pol = eRemoteDB.Constants.intNull
		sCorner = CStr(eRemoteDB.Constants.strNull)
		sUrban = CStr(eRemoteDB.Constants.strNull)
		sServer = CStr(eRemoteDB.Constants.strNull)
		nNum_inhab = eRemoteDB.Constants.intNull
		nNum_beds = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






