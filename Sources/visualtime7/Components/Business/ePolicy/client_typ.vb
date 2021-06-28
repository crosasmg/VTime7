Option Strict Off
Option Explicit On
Public Class client_typ
	'%-------------------------------------------------------%'
	'% $Workfile:: client_typ.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**-The primay key fields correspond to "sType"
	'-Los campos llaves corresponden a sType
	
	'  Column_name              Type                    Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'  ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public sType As String 'char      no        1                  no       yes                no
	Public sDescript As String 'char      no        30                 no       yes                no
	Public sNatural As String 'char      no        1                  no       yes                no
	Public sTysProvision As String 'char      no        1                  no       yes                no
	Public sStatregt As String 'char      no        1                  no       yes                no
	Public dCompdate As Date 'date      no        8                  no       yes                no
	Public nUsercode As Integer 'int       no        2                  no       yes                no
	'   -----------------------------------------------------------------------------------------------------------------------------
	'   usado para retornar el codigo y nombre del cliente
	Public sClientRole As String 'int       no        2                  no       yes                no
	
	
	'**%FindTypClient: Searches for the type of client
	'%FindTypClient: Busca el tipo de cliente
	' --------------------------------------------------------------------------------------------
	Public Function FindTypClient(ByVal lstrFirst As String) As Boolean
		' --------------------------------------------------------------------------------------------
		
		Dim lrecreaClient_typ As eRemoteDB.Execute
		
		On Error GoTo FindTypClient_Err
		
		lrecreaClient_typ = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaClient_typ'
		'**+Data of 11/02/2000 10:28:49 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaClient_typ'
		'+Información leída el 02/11/2000 10:28:49 a.m.
		
		With lrecreaClient_typ
			.StoredProcedure = "reaClient_typ"
			.Parameters.Add("sType", lstrFirst, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			FindTypClient = .Run(False)
		End With
		
FindTypClient_Err: 
		If Err.Number Then
			FindTypClient = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaClient_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_typ = Nothing
	End Function
	
	'**%FindClient_roles: Searches for the type of client
	'%%FindClient_roles: Busca el codigo y nombre del cliente cuando es el contratante de la poliza
	' --------------------------------------------------------------------------------------------
	Public Function FindClient_roles(ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCertif As Double) As Boolean
		' --------------------------------------------------------------------------------------------
		
		Dim lrecreaClient_roles As eRemoteDB.Execute
		
		On Error GoTo FindClient_Err
		
		lrecreaClient_roles = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaClient_typ'
		'**+Data of 11/02/2000 10:28:49 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaClient_typ'
		'+Información leída el 02/11/2000 10:28:49 a.m.
		
		With lrecreaClient_roles
			.StoredProcedure = "TABCLIENT_ROL"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			FindClient_roles = .Run
			If FindClient_roles Then
				sClientRole = .FieldToClass("sClient")
				.RCloseRec()
			End If
		End With
		
FindClient_Err: 
		If Err.Number Then
			FindClient_roles = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaClient_roles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_roles = Nothing
	End Function
End Class






