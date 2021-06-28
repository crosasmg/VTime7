Option Strict Off
Option Explicit On
Imports System.Text.RegularExpressions
Public Class User
	'%-------------------------------------------------------%'
	'% $Workfile:: User.cls                                 $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 25/11/03 1:49p                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Public nUsercode As Integer
	Public sAccesswo As String
	Public nOffice As Integer
	Public nOfficeAgen As Integer
	Public nAgency As Integer
	Public sClient As String
	Public sInitials As String
	Public sSche_code As String
	Public sStatregt As String
	Public sType As String
	Public nDepartme As String
	Public sMenu As String
	Public sChNextLogon As String
	Public sNeverChange As String
	Public sNeverExpires As String
	Public sLockedOut As String
	Public dFromDate As Date
	
	'**-Auxiliaries variables
	'-Variables auxiliares
	Public sMenu_descript As String
	Public sCliename As String
	Public pstrOldPassword As String
	Public sOldAccesswo As String
	Public sOld As String
	Public sNew As String
	Public nCurrenUserCode As Integer
    Public pstrStatregt As String
    Public nFailedLogonAttempts As Integer
    Public dPasswordExpires As Date

    Public bPasswordSet As Boolean
	
	'**-Auxiliaries properties definition use in the SGC002 window- User consultation.
	'-Se definen las propiedades auxiliares utilizadas en la ventana SGC002 - Consulta de usuarios.
	Public sOffice As String
	Public sDepartme As String
	
	'**-Define the eTypValConst enumerated list, to differentiate the field type that
	'**-is going to validate in the insConstruct function
	'-Se define la lista enumerada eTypValConst, para diferenciar el tipo de campo que se
	'-va a validar en la funcion insConstruct.
	Enum eTypValConst
		ConstNumeric
		ConstDate
		ConstString
	End Enum

    '**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Users"
    '%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%tabla "Users"
    Public Function Find(ByVal nUsercode As Double, Optional ByRef lblnAll As Boolean = False) As Boolean
        Dim lrecReaUser As eRemoteDB.Execute
        Dim lclsUserValidate As eSecurity.UserValidate
        Dim lstrPass As String

        On Error GoTo Find_Err
        lrecReaUser = New eRemoteDB.Execute
        lclsUserValidate = New eSecurity.UserValidate

        '**+Parameters definiton to stored prcoedure 'insudb.reaUsers'
        '**+Data read on 01/15/2001 15.29.55
        '+Definición de parámetros para stored procedure 'insudb.reaUsers'
        '+Información leída el 15/01/2001 15.29.55
        With lrecReaUser
            .StoredProcedure = "reaUsers"
            .Parameters.Add("Users", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nUsercode = nUsercode
                lstrPass = .FieldToClass("sAccesswo")
                sAccesswo = lclsUserValidate.StrDecode(lstrPass)
                pstrOldPassword = lclsUserValidate.StrDecode(lstrPass)
                nOffice = .FieldToClass("nOffice", -1)
                sClient = .FieldToClass("sClient")
                sInitials = .FieldToClass("sInitials")
                sSche_code = .FieldToClass("sSche_code")
                sStatregt = .FieldToClass("sStatregt")
                sType = .FieldToClass("sType")
                nDepartme = .FieldToClass("nDepartme")
                sChNextLogon = .FieldToClass("sChNextLogon")
                sNeverChange = .FieldToClass("sNeverChange")
                sNeverExpires = .FieldToClass("sNeverExpires")
                sLockedOut = .FieldToClass("sLockedOut")
                sMenu = .FieldToClass("sMenu")
                dFromDate = .FieldToClass("dFromDate")
                sMenu_descript = .FieldToClass("sMenuDes")
                sCliename = .FieldToClass("sUsername")
                nOfficeAgen = .FieldToClass("nOfficeagen", eRemoteDB.Constants.intNull)
                nAgency = .FieldToClass("nAgency", eRemoteDB.Constants.intNull)
                nFailedLogonAttempts = .FieldToClass("NFAILED_LOGON_ATTEMPTS", eRemoteDB.Constants.intNull)
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecReaUser may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaUser = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecReaUser may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaUser = Nothing
        'UPGRADE_NOTE: Object lclsUserValidate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUserValidate = Nothing
        On Error GoTo 0
    End Function

    '**%ADD: This method is in charge of adding new records to the table "Users".  It returns TRUE or FALSE
    '**%depending on whether the stored procedure executed correctly.
    '%ADD: Este método se encarga de agregar nuevos registros a la tabla "Users". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Add() As Boolean
        Dim lrecUsers As eRemoteDB.Execute

        On Error GoTo Add_Err
        lrecUsers = New eRemoteDB.Execute
        '**+ Parameters defintion to stored procedure 'insudb.creUsers'
        '+ Definición de parámetros para stored procedure 'insudb.creUsers'

        With lrecUsers
            .StoredProcedure = "creUsers"
            .Parameters.Add("nUsersCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAccesswo", sAccesswo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDepartament", nDepartme, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInitials", sInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShe_Code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChNextLogon", sChNextLogon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNeverChange", sNeverChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNeverExpires", sNeverExpires, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLockedOut", sLockedOut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMenu", sMenu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFromDate", dFromDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DPASSWORD_EXPIRES", dPasswordExpires, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_Err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUsers = Nothing
    End Function

    '**%Update: This method is in charge of updating records in the table "Users".  It returns TRUE or FALSE
    '**%depending on whether the stored procedure executed correctly.
    '%Update: Este método se encarga de actualizar registros en la tabla "Users". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Update() As Boolean
		Dim lrecUpdUsers As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecUpdUsers = New eRemoteDB.Execute
		
		'**+Parameters definition to stored procedure "insudb.updUsers"
		'+Definición de parámetros para stored procedure "insudb.updUsers"
		With lrecUpdUsers
			.StoredProcedure = "updUsers"
			.Parameters.Add("nUsersCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccesswo", sAccesswo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDepartme", nDepartme, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitials", sInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOldAccesswo", sOldAccesswo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChNextLogon", sChNextLogon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNeverChange", sNeverChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNeverExpires", sNeverExpires, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLockedOut", sLockedOut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMenu", sMenu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOld", sOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNew", sNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrenUserCode", nCurrenUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFromDate", dFromDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeagen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DPASSWORD_EXPIRES", dPasswordExpires, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SSET_PASSWORD", IIf(Me.bPasswordSet, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUpdUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdUsers = Nothing
	End Function
	
	'**%Delete: This method is in charge of Deleting records in the table "Users".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "Users". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecDelUsers As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecDelUsers = New eRemoteDB.Execute
		
		'**+Delete the record from the table "Users"
		'+Eliminar el registro de la tabla "Users"
		With lrecDelUsers
			.StoredProcedure = "delUsers"
			.Parameters.Add("nUsersCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDelUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelUsers = Nothing
	End Function
	
	'**%reaUser_vl: This method searches the initials that heve been introduced
	'**%to verify that they don't exist
	'%reaUsers_v1: Esta funcion se encarga de ralizar la búsqueda de las iniciales introducidas
	'%para verificar que no existan previamente.
	Public Function reaUsers_v1(ByVal nUsercode As Integer, ByVal sInitials As String) As Boolean
		Dim lrecReaInitials As eRemoteDB.Execute
		
		On Error GoTo reaUsers_v1_err
		lrecReaInitials = New eRemoteDB.Execute
		'**+Parameters definition to stored procedure ' insudb.reaInitials'
		'**+Data read on 09/22/2000 9:05:18
		'+Definición de parámetros para stored procedure 'insudb.reaInitials'
		'+Información leída el 22/09/2000 9:05:18
		With lrecReaInitials
			.StoredProcedure = "reaInitials"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitials", sInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				reaUsers_v1 = .Parameters("nCount").Value > 0
			End If
		End With
		
reaUsers_v1_err: 
		If Err.Number Then
			reaUsers_v1 = False
		End If
		'UPGRADE_NOTE: Object lrecReaInitials may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaInitials = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsValSG001_K: This method validates the header section of the page "SG001_K" as described in the
	'**%functional specifications
	'%InsValSG001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "SG001_K"
	Public Function InsValSG001_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nUsercode As Integer = 0) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo InsValSG001_K_Err
		lobjErrors = New eFunctions.Errors
		
		'**+Field validation of the branch code
		'+Se valida el campo Código del Ramo.
		With lobjErrors
			If nUsercode = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 12049)
			Else
				If insReaUsers_v(nUsercode) Then
					If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
						.ErrorMessage(sCodispl, 12001)
					End If
				Else
					If nAction = eFunctions.Menues.TypeActions.clngActionQuery Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActioncut Then
						.ErrorMessage(sCodispl, 12002)
					End If
				End If
			End If
			InsValSG001_K = .Confirm
		End With
		
InsValSG001_K_Err: 
		If Err.Number Then
			InsValSG001_K = "InsValSG001_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**%insReaUsers_v: This method validates if the user exists
	'%insReaUsers_v: Esta funcion se encarga de validar si el usuario existe.
	Public Function insReaUsers_v(ByVal nUsercode As Integer) As Boolean
		Dim lrecReaUsers As eRemoteDB.Execute
		
		On Error GoTo insReaUsers_v_Err
		lrecReaUsers = New eRemoteDB.Execute
		With lrecReaUsers
			.StoredProcedure = "reaUsers"
			.Parameters.Add("Users", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
				insReaUsers_v = True
			End If
		End With
		
insReaUsers_v_Err: 
		If Err.Number Then
			insReaUsers_v = False
		End If
		'UPGRADE_NOTE: Object lrecReaUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUsers = Nothing
		On Error GoTo 0
	End Function

    '**%InsValSG001: This method validates the page "SG001" as described in the functional specifications
    '%InsValSG001: Este metodo se encarga de realizar las validaciones descritas en el funcional
    '%de la ventana "SG001"
    Public Function InsValSG001(ByVal sCodispl As String, Optional ByRef nUsercode As Integer = 0, Optional ByVal sSche_code As String = "", Optional ByVal sType As String = "", Optional ByVal nOffice As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal sInitials As String = "", Optional ByVal sAccesswo As String = "", Optional ByVal dFromDate As Date = #12:00:00 AM#, Optional ByVal nDepartme As Integer = 0, Optional ByVal sStatregt As String = "", Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim sRx As String = New eRemoteDB.VisualTimeConfig().LoadSetting("PasswordStrength", "", "Security")
        Dim lobjClient As Object
        lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")

        On Error GoTo InsValSG001_Err
        lobjErrors = New eFunctions.Errors

        With lobjErrors
            '**+Validates the field "Schema"
            '+Se realizan las validaciones del campo "Esquema".
            If sSche_code = String.Empty Then
                .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Esquema: ")
            Else
                If Not reaScheCode_v(sSche_code) Then
                    .ErrorMessage(sCodispl, 99001)
                Else
                    If pstrStatregt <> "1" Then
                        .ErrorMessage(sCodispl, 12152)
                    End If
                End If
            End If

            '**+Validates the field "Type"
            '+Se realizan las validaciones del campo "Tipo".
            If sType = String.Empty Or sType = "0" Then
                .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo: ")
            End If

            '**+Validates the field "Branch office"
            '+Se realizan las validaciones del campo "Sucursal".
            If nOffice = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Sucursal: ")
            End If

            '+Se realizan las validaciones del campo "Oficina".
            If nOfficeAgen = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 55519)
            End If

            '+Se realizan las validaciones del campo "Agencia".
            If nAgency = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 55518)
            End If

            '**+Validates the field "Client"
            '+Se realizan las validaciones del campo "Cliente".
            If sClient = String.Empty Then
                .ErrorMessage(sCodispl, 2001)
            Else
                If Not lobjClient.ValidateClientStruc(sClient) Then
                    lobjErrors.ErrorMessage(sCodispl, 2012)
                    '+Si la acción no es "registrar", debe existir en el archivo de clientes
                ElseIf Not lobjClient.Find(lobjClient.sClient, True) Then
                    lobjErrors.ErrorMessage(sCodispl, 1007)
                End If
            End If

            '**+Validates the field "Initials"
            '+Se realizan las validaciones del campo "Iniciales".
            If sInitials = String.Empty Then
                .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Iniciales: ")
            Else
                If reaUsers_v1(nUsercode, sInitials) Then
                    .ErrorMessage(sCodispl, 12058)
                End If
            End If

            '**+Validates the field "Countable Limits_ since date"
            '+Se realizan las validaciones de la fecha "Límite Contable - Desde".
            If dFromDate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 3237)
            End If

            '**+Validates the field "Departament"
            '+Se realizan las validaciones del campo "Departamento".
            If nDepartme = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 12004)
            End If

            '+Se valida el campo Estado
            If sStatregt = "0" Or sStatregt = String.Empty Then
                .ErrorMessage(sCodispl, 1922)
            End If


            If Me.bPasswordSet Then
                If Not Regex.IsMatch(sAccesswo, sRx) Then
                    Call .ErrorMessage(sCodispl, 978899)
                End If

                '**+Validates the field "Key"
                '+Se realizan las validaciones del campo "Clave".

                If sAccesswo = String.Empty Then
                    .ErrorMessage(sCodispl, 12064)
                End If

                If Me.sNeverChange = "1" AndAlso Me.Find(nUsercode) Then
                    Call .ErrorMessage(sCodispl, 55994)
                End If
            End If

            InsValSG001 = .Confirm
        End With

InsValSG001_Err:
        If Err.Number Then
            InsValSG001 = "InsValSG001: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

        On Error GoTo 0
    End Function

    '**%reaScheCode_v(): This method validates if the schema code exists
    '%reaScheCode_v(): Esta funcion se encarga de validar si el cédigo de esquema  existe.
    Public Function reaScheCode_v(ByVal sScheCode As String) As Boolean
		Dim lrecReaSecurSche As eRemoteDB.Execute
		
		On Error GoTo reaSchecode_v_err
		lrecReaSecurSche = New eRemoteDB.Execute
		
		'**+Parameters definition to stored procedure 'insudb.reaSecurSche'
		'**+Data read on 09/22/2000 9:41:02
		'+Definición de parámetros para stored procedure 'insudb.reaSecurSche'
		'+Información leída el 22/09/2000 9:41:02
		With lrecReaSecurSche
			.StoredProcedure = "reaSecurSche"
			.Parameters.Add("strSchema", sScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				pstrStatregt = .FieldToClass("sStatregt")
				reaScheCode_v = True
				.RCloseRec()
			End If
		End With
		
reaSchecode_v_err: 
		If Err.Number Then
			reaScheCode_v = False
		End If
		'UPGRADE_NOTE: Object lrecReaSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaSecurSche = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsPostSG001: This method stores the data in the tables, in this case "Users"
	'**%according to the data that has been introduced in the window SG001 - System users.
	'%InsPostSG001: Esta función se encarga de almacenar los datos en las tablas, en este caso Users
	'%según los datos introducidos en la ventana SG001 - Usuarios del sistema.
	Public Function InsPostSG001(ByVal nMainAction As Integer, ByVal nUsercode As Integer, ByVal sSche_code As String, ByVal sType As String, ByVal nOffice As Integer, ByVal sClient As String, ByVal sInitials As String, ByVal sAccesswo As String, ByVal dFromDate As Date, ByVal sStatregt As String, ByVal nDepartment As Integer, ByVal sMenu As String, ByVal sChNextLogon As String, ByVal sNeverChange As String, ByVal sNeverExpires As String, ByVal sLockedOut As String, ByVal nCurrenUserCode As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer) As Boolean
        Dim lclsUserValidate As eSecurity.UserValidate
        Dim nPasswordExpiresAfter As Integer = New eRemoteDB.VisualTimeConfig().LoadSetting("PasswordExpiresAfter", 10000, "Security")
		
		On Error GoTo InsPostSG001_Err
		lclsUserValidate = New eSecurity.UserValidate
		
		With Me
			.nUsercode = nUsercode
			.sSche_code = sSche_code
			.sType = sType
			.nOffice = nOffice
			.sClient = sClient
			.sInitials = sInitials

            If Me.bPasswordSet Then
                .sAccesswo = lclsUserValidate.StrEncode(Trim(sAccesswo))
                dPasswordExpires = Date.Today.AddDays(nPasswordExpiresAfter)
            Else
                .sAccesswo = String.Empty
            End If

            .dFromDate = dFromDate
            If nMainAction = eFunctions.Menues.TypeActions.clngActionadd Then
                .sStatregt = "1"
            Else
                .sStatregt = sStatregt
            End If
            .nDepartme = CStr(nDepartment)
            .sMenu = sMenu
            .sChNextLogon = IIf(sChNextLogon = "", "2", "1")
            .sNeverChange = IIf(sNeverChange = "" Or sNeverChange = "0", "2", "1")
            .sNeverExpires = IIf(sNeverExpires = "", "2", "1")
            .sLockedOut = IIf(sLockedOut = "", "2", "1")
            .nCurrenUserCode = nCurrenUserCode
            .sOldAccesswo = lclsUserValidate.StrEncode(Trim(pstrOldPassword))
            .sOld = pstrOldPassword
            .sNew = sAccesswo
            .nOfficeAgen = nOfficeAgen
            .nAgency = nAgency


            Select Case nMainAction
                '**+If the Action is Add
                '+Si la acción es Registrar.
                Case eFunctions.Menues.TypeActions.clngActionadd
                    InsPostSG001 = .Add()

                    '**+If the action is modify
                    '+Si la acción es Modificar.

                Case eFunctions.Menues.TypeActions.clngActionUpdate
                    InsPostSG001 = .Update()
                    '**+If the action is delete
                    '+Si la acción es Eliminar.

                Case eFunctions.Menues.TypeActions.clngActioncut
                    InsPostSG001 = .Delete()
            End Select
        End With

InsPostSG001_Err: 
		If Err.Number Then
			InsPostSG001 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Funcion InsConstruct. This method validates that the search condition for the inquiry
	'**%is valid depending on the type of value that is in process (numerical, Character chain or date )
	'%Funcion InsConstruct. Esta función se encarga de validar que la condición de búsqueda para la consulta
	'%sea válida dependiendo del tipo de valor que se está tratando (numérico, Cadena de caracteres o fecha).
	Public Function InsConstruct(ByRef pstrField As String, ByRef pstrValue As String, ByRef pTypValue As eTypValConst, Optional ByRef pstrWhere As String = "") As Boolean
		Dim lblnBetween As Boolean
		Dim lintIniPos As Integer
		Dim llngSubscript As Integer
		
		On Error GoTo InsConstruct_Err
		lintIniPos = 0
		Select Case pTypValue
			Case eTypValConst.ConstNumeric '-- Numero
				llngSubscript = InStr(1, pstrValue, ".")
				Do While llngSubscript > 0
					pstrValue = Mid(pstrValue, 1, llngSubscript - 1) & Mid(pstrValue, llngSubscript + 1)
					llngSubscript = InStr(1, pstrValue, ".")
				Loop 
				llngSubscript = InStr(1, pstrValue, ",")
				Do While llngSubscript > 0
					pstrValue = Mid(pstrValue, 1, llngSubscript - 1) & "." & Mid(pstrValue, llngSubscript + 1)
					llngSubscript = InStr(1, pstrValue, ",")
				Loop 
				
				If Mid(pstrValue, 1, 1) = ">" Or Mid(pstrValue, 1, 1) = "<" Or Mid(pstrValue, 1, 1) = "=" Then
					If Mid(pstrValue, 1, 1) <> "=" Then
						If Mid(pstrValue, 2, 1) = "=" Or (Mid(pstrValue, 1, 1) = "<" And Mid(pstrValue, 2, 1) = ">") Then
							lintIniPos = 2
						Else
							lintIniPos = 1
						End If
					Else
						lintIniPos = 1
					End If
					If IsNumeric(Mid(pstrValue, 1 + lintIniPos)) Then
						pstrWhere = Trim(pstrField) & Trim(pstrValue)
						InsConstruct = True
					End If
				Else
					lblnBetween = False
					For lintIniPos = 1 To Len(pstrValue)
						If Mid(pstrValue, lintIniPos, 1) = ":" Then
							lblnBetween = True
							Exit For
						End If
					Next lintIniPos
					If lblnBetween Then
						If IsNumeric(Mid(pstrValue, 1, lintIniPos - 1)) And IsNumeric(Mid(pstrValue, lintIniPos + 1)) Then
							
							pstrWhere = Trim(pstrField) & " BETWEEN " & Trim(Mid(pstrValue, 1, lintIniPos - 1)) & " AND " & Trim(Mid(pstrValue, lintIniPos + 1))
							InsConstruct = True
						End If
					Else
						If IsNumeric(pstrValue) Then
							pstrWhere = Trim(pstrField) & " = " & Trim(pstrValue)
							InsConstruct = True
						End If
					End If
				End If
				
			Case eTypValConst.ConstDate '-- Fecha
				If Mid(pstrValue, 1, 1) = ">" Or Mid(pstrValue, 1, 1) = "<" Or Mid(pstrValue, 1, 1) = "=" Then
					If Mid(pstrValue, 1, 1) <> "=" Then
						If Mid(pstrValue, 2, 1) = "=" Or (Mid(pstrValue, 1, 1) = "<" And Mid(pstrValue, 2, 1) = ">") Then
							lintIniPos = 2
						Else
							lintIniPos = 1
						End If
					Else
						lintIniPos = 1
					End If
					If IsDate(Mid(pstrValue, 1 + lintIniPos)) Then
						pstrWhere = "CONVERT(varchar(10)," & Trim(pstrField) & ",112) " & Mid(pstrValue, 1, lintIniPos) & " '" & Format(Trim(Mid(pstrValue, 1 + lintIniPos)), "yyyyMMdd") & "'"
						InsConstruct = True
					End If
				Else
					If IsDate(pstrValue) Then
						pstrWhere = "CONVERT(varchar(10)," & Trim(pstrField) & ",112) = '" & Format(Trim(pstrValue), "yyyyMMdd") & "'"
						InsConstruct = True
					End If
				End If
				
			Case eTypValConst.ConstString '-- Cadena de caracteres
				If Mid(pstrValue, 1, 1) = ">" Or Mid(pstrValue, 1, 1) = "<" Or Mid(pstrValue, 1, 1) = "=" Then
					If Mid(pstrValue, 1, 1) <> "=" Then
						If Mid(pstrValue, 2, 1) = "=" Or (Mid(pstrValue, 1, 1) = "<" And Mid(pstrValue, 2, 1) = ">") Then
							lintIniPos = 2
						Else
							lintIniPos = 1
						End If
					Else
						lintIniPos = 1
					End If
					pstrWhere = Trim(pstrField) & Mid(pstrValue, 1, lintIniPos) & " '" & Trim(Mid(pstrValue, 1 + lintIniPos)) & "'"
					InsConstruct = True
				Else
					If InStr(Trim(pstrValue), "%") <> 0 Then
						pstrWhere = Trim(pstrField) & " LIKE '" & Trim(pstrValue) & "'"
					Else
						pstrWhere = Trim(pstrField) & " = '" & Trim(pstrValue) & "'"
					End If
					InsConstruct = True
				End If
		End Select
		
InsConstruct_Err: 
		If Err.Number Then
			InsConstruct = False
		End If
		On Error GoTo 0
	End Function
	
	'**%InsValSGC001_K: This method validates the header section of the page "SGC001_K" as described in the
	'**%functional specifications
	'%InsValSGC001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "SGC001_K"
	Public Function InsValSGC001_K(ByVal sCodispl As String, ByVal nOffice As Integer, ByVal nDepartmen As Integer, ByVal sSche_code As String) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo InsValSGC001_K_Err
		
		With lobjErrors
			'**+Validates the field "Zone"
			'+Se realizan las validaciones del campo "Zona".
			If nOffice <> eRemoteDB.Constants.intNull Then
				If Not InsConstruct("U.nOffice", CStr(nOffice), eTypValConst.ConstNumeric) Then
					.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Zona) ")
				End If
			End If
			
			'**+Validates the field "Departament"
			'+Se realizan las validaciones del campo "Departamento".
			If nDepartmen <> eRemoteDB.Constants.intNull Then
				If Not InsConstruct("U.nDepartme", CStr(nDepartmen), eTypValConst.ConstNumeric) Then
					.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Departamento) ")
				End If
			End If
			
			'**+Validates the field "Scheme"
			'+Se realizan las validaciones del campo "Esquema"
			If Trim(sSche_code) <> String.Empty Then
				If Not InsConstruct("U.sSche_code", sSche_code, eTypValConst.ConstString) Then
					.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Esquema) ")
				End If
			End If
			
			InsValSGC001_K = .Confirm
		End With
		
InsValSGC001_K_Err: 
		If Err.Number Then
			InsValSGC001_K = "InsValSGC001_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsValSG099_K: This method validates the header section of the page "SG099_K" as described in the
	'**%functional specifications
	'%InsValSG099_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "SG099_K"
	Public Function InsValSG099_K(ByVal sCodispl As String, ByVal sOldPass As String, ByVal sNewPass As String, ByVal sRNewPass As String, ByVal nUsercode As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim sRx As String = New eRemoteDB.VisualTimeConfig().LoadSetting("PasswordStrength", "", "Security")
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo InsValSG099_K_Err
		
		With lobjErrors
			
			'**+Validates the field "previous password"
			'+Se realizan las validaciones del campo "Contraseña anterior".
			If sOldPass = String.Empty Then
				.ErrorMessage(sCodispl, 12176)
			End If
			
			'**+Validates the field "new password"
			'+Se realizan las validaciones del campo "Contraseña nueva".
			If sNewPass = String.Empty Then
				.ErrorMessage(sCodispl, 12177)
			Else
				If Len(sNewPass) < 5 Then
					.ErrorMessage(sCodispl, 12178)
				End If
				
				If sRNewPass = String.Empty Then
                    .ErrorMessage(sCodispl, 12179)
                ElseIf Not Regex.IsMatch(sRNewPass, sRx) Then
                    Call .ErrorMessage(sCodispl, 978899)
                End If
            End If
			
			'**+Validates the field "Confirm new password"
			'+Se realizan las validaciones del campo "Confirmar contraseña nueva".
			If sRNewPass <> String.Empty Then
				If sNewPass <> sRNewPass Then
					.ErrorMessage(sCodispl, 12180)
				End If
			End If
			
			If sOldPass <> String.Empty Then
				If Find(nUsercode) Then
					If sOldPass <> Me.sAccesswo Then
						Call .ErrorMessage(sCodispl, 12123)
					End If
				End If
			End If
			
			If Me.Find(nUsercode) Then
				If Me.sNeverChange = "1" Then
					Call .ErrorMessage(sCodispl, 55994)
				End If
			End If
			
			InsValSG099_K = .Confirm
		End With
		
InsValSG099_K_Err: 
		If Err.Number Then
			InsValSG099_K = "InsValSG099_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsPostSG099: This method updates the database (as described in the functional specifications)
	'**%for the page "SG099"
	'%InsPostSG099: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "SG099"
	Public Function InsPostSG099(ByVal nUsercode As Integer, ByVal sInitials As String, ByVal sNewPass As String, ByVal sOldPass As String) As Boolean
        Dim lclsUserValidate As eSecurity.UserValidate
        Dim nPasswordExpiresAfter As Integer = New eRemoteDB.VisualTimeConfig().LoadSetting("PasswordExpiresAfter", 10000, "Security")

		On Error GoTo InsPostSG099_Err
		
		lclsUserValidate = New eSecurity.UserValidate
		InsPostSG099 = True
		
		With Me
			.nUsercode = nUsercode
			.sInitials = sInitials
			.sNew = sNewPass
			.sOld = sOldPass
            .sAccesswo = lclsUserValidate.StrEncode(Trim(sNewPass))
            dPasswordExpires = Date.Today.AddDays(nPasswordExpiresAfter)
            InsPostSG099 = .ChangePassword
		End With
		
InsPostSG099_Err: 
		If Err.Number Then
			InsPostSG099 = False
		End If
		'UPGRADE_NOTE: Object lclsUserValidate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUserValidate = Nothing
		On Error GoTo 0
	End Function
	
	'**%ChangePassword: Changes the password if the user wants to
	'%ChangePassword: Permite cambiar la contraseña si el usuario así lo desea.
	Public Function ChangePassword() As Boolean
		Dim lrecUpdUsers As eRemoteDB.Execute
		
		On Error GoTo ChangePassword_Err
		lrecUpdUsers = New eRemoteDB.Execute
		'**+Parameters definition for stored procedure 'insudb.insPassword'
		'+Definición de parámetros para stored procedure 'insudb.insPassword'
		
		With lrecUpdUsers
			.StoredProcedure = "insPassword"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitials", sInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccesswo", sAccesswo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOldPass", sOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNewPass", sNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DPASSWORD_EXPIRES", dPasswordExpires, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ChangePassword = .Run(False)
		End With
		
ChangePassword_Err: 
		If Err.Number Then
			ChangePassword = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUpdUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdUsers = Nothing
	End Function

    '**%Reauser_Intial: Knows the users name according to their initials
    '%Reauser_Initial: Permite saber el Nombre del Usuario de acuerdo a sus Iniciales
    Public Function Reauser_Initial(ByVal sUsersInitials As String) As Boolean
        Dim lrecreaUsersInitials As eRemoteDB.Execute

        On Error GoTo Reauser_Initial_Err
        lrecreaUsersInitials = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure 'insub.reaUsersInitials'
        '**+Data read on 06/13/2001 02:37:25 PM
        '+Definición de parámetros para stored procedure 'insudb.reaUsersInitials'
        '+Información leída el 13/06/2001 02:37:25 PM
        With lrecreaUsersInitials
            .StoredProcedure = "reaUsersInitials"
            .Parameters.Add("sInitials", sUsersInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sCliename = .FieldToClass("sUsername")
                Reauser_Initial = True
                .RCloseRec()
            End If
        End With

Reauser_Initial_Err:
        If Err.Number Then
            Reauser_Initial = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaUsersInitials may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaUsersInitials = Nothing
    End Function

    '% ValidateClientStruc: Esta función valida el código del cliente
    Public Function ValidateClientStruc(ByVal sClientCode As String) As Boolean
        ValidateClientStruc = True

        If Not IsNumeric(sClientCode) Then
            ValidateClientStruc = False
        Else
            sClient = sClientCode
        End If
    End Function

    '% Find: Función que realiza la busqueda en la tabla client dado un coigo de cliente....
    Public Function Find(ByVal sClient As String, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaClient As eRemoteDB.Execute
        Dim lclsBusinessFun As Object

        On Error GoTo Find_Err

        If sClient <> Me.sClient Or bFind Then

            lrecreaClient = New eRemoteDB.Execute
            lclsBusinessFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")

            With lrecreaClient
                .StoredProcedure = "reaClient"
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sClient = sClient
                    .RCloseRec()
                    Find = True
                Else
                    Find = False
                End If
            End With
            lclsBusinessFun = Nothing
        Else
            Find = True
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaClient = Nothing
    End Function
End Class