Option Strict Off
Option Explicit On
Public Class Users
	'%-------------------------------------------------------%'
	'% $Workfile:: Users.cls                                $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on June 21,2000
	'- Propiedades según la tabla en el sistema el 21/06/2000.
	'**- The key field corresponds to a nUsercode.
	'- El campo llave corresponde a nUsercode
	
	'Column_name                      Type                 Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'-------------------------------- -------------------- -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nUsercode As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	Public sAccesswo As String 'char     no       12                      yes      yes                yes
	Public nOffice As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	Public nOfficeagen As Integer 'smallint no       5           5     0     no       (n/a)              (n/a)
	Public nAgency As Integer 'smallint no       5           5     0     no       (n/a)              (n/a)
	Public sClient As String 'char     no       14                      no       yes                no
	Public dCompdate As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public sInitials As String 'char     no       12                      yes      yes                yes
	Public sSche_code As String 'char     no       6                       no       yes                no
	Public sStatregt As String 'char     no       1                       yes      yes                yes
	Public sType As String 'char     no       1                       yes      yes                yes
	Public nDepartme As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public sMenu As String 'char     no       8                       yes      yes                yes
	Public sChNextLogon As String 'char     no       1                       yes      yes                yes
	Public sNeverChange As String 'char     no       1                       yes      yes                yes
	Public sNeverExpires As String 'char     no       1                       yes      yes                yes
	Public sLockedOut As String 'char     no       1                       yes      yes                yes
    Public dFromDate As Date 'datetime no       8                       yes      (n/a)              (n/a)
    Public nFailedLogonAttempts As Integer
    Public dPasswordExpires As Date
	
	'**+ Additional properties
	'+ Propiedades auxiliares
	
	'**- Variable definition for keeping the user's description
	'- Se define la variable para almacenar la descripción del usuario
	
	Public sCliename As String
	
	'**- Variable definition for keeping the user associated menu record.
	'- Se define la variable para almacenar la descripción del menú asociado al usuario
	
	Public sMenuDes As String
	
	Public sLogin As String
	Public sPassWord As String
	
	'- Se define la variable para almacenar el numero de la caja asociado al usuario
	Public nCashNum As Integer
	
	'**% FindUserName: searches the user's code name.
	'% FindUserName: busca el nombre del código de usuario
	Public Function FindUserName(ByVal nUsercode As Integer, Optional ByVal lblnFind As Boolean = False) As String
        Dim lrecreaUsersName As eRemoteDB.Execute = New eRemoteDB.Execute
        Dim varAux As String = ""
        '**+ Parameter definition for stored procedure 'insud.reaUsersName'
        '+ Definición de parámetros para stored procedure 'insudb.reaUsersName'
        '**+ Information read on June 07,2000  05:27:53 p.m.
        '+ Información leída el 07/06/2000 05:27:53 PM

        With lrecreaUsersName
            .StoredProcedure = "reaUsersName"
            .Parameters.Add("nUsers", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                varAux = .FieldToClass("sCliename", eRemoteDB.Constants.strNull)
                .RCloseRec()
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaUsersName may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaUsersName = Nothing
        Return varAux
    End Function
	
	'**% Find: searches all the user data.
	'% Find: busca todos los datos del usuario
	Public Function Find(ByVal nUsercode As Integer) As Boolean
		Dim lrecreaUsers As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		If Me.nUsercode <> nUsercode Then
			'**+ Parameter definition for the stored procedure 'insudb.reaUsers'
			'+ Definición de parámetros para stored procedure 'insudb.reaUsers'
			'**+ Information read on November 09,2000  09:01:40 a.m.
			'+ Información leída el 09/11/2000 09:01:40 a.m.
			lrecreaUsers = New eRemoteDB.Execute
			With lrecreaUsers
				.StoredProcedure = "reaUsers"
				.Parameters.Add("Users", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					sAccesswo = .FieldToClass("sAccesswo")
					nOffice = .FieldToClass("nOffice")
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
					sMenuDes = .FieldToClass("sMenuDes")
					sCliename = .FieldToClass("sUsername")
					nOfficeagen = .FieldToClass("nOfficeagen", eRemoteDB.Constants.intNull)
					nAgency = .FieldToClass("nAgency", eRemoteDB.Constants.intNull)
					Me.nUsercode = nUsercode
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaUsers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaUsers = Nothing
	End Function
	
	'**% FindUserInitials: This method searches for clients by the indicated initials as a parameter.
	'% FindUserInitials: Este método realiza la busqueda del cliente por las iniciales indicadas
	'% como parámetro.
	Public Function FindUserInitial(ByVal sInitials As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaUsersName As eRemoteDB.Execute
		
        'On Error GoTo FindUserInitial_Err
		
		lrecreaUsersName = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.reaUsersName'
		'+ Definición de parámetros para stored procedure 'insudb.reaUsersName'
		'**+ Information read on June 07, 2000  05:27:53  p.m.
		'+ Información leída el 07/06/2000 05:27:53 PM
		
		With lrecreaUsersName
			.StoredProcedure = "reaUsersInitials"
			.Parameters.Add("sInitials", sInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			FindUserInitial = .Run
			If FindUserInitial Then
				sAccesswo = .FieldToClass("sAccesswo", eRemoteDB.Constants.strNull)
				nOffice = .FieldToClass("nOffice", eRemoteDB.Constants.intNull)
				sClient = .FieldToClass("sClient", eRemoteDB.Constants.strNull)
				sInitials = .FieldToClass("sInitials", eRemoteDB.Constants.strNull)
				sSche_code = .FieldToClass("sSche_code", eRemoteDB.Constants.strNull)
				sStatregt = .FieldToClass("sStatregt", eRemoteDB.Constants.strNull)
				sType = .FieldToClass("sType", eRemoteDB.Constants.strNull)
				nDepartme = .FieldToClass("nDepartme", eRemoteDB.Constants.intNull)
				sMenu = .FieldToClass("sMenu", eRemoteDB.Constants.strNull)
				sLockedOut = .FieldToClass("sLockedOut", eRemoteDB.Constants.strNull)
				nUsercode = .FieldToClass("nUsercode", eRemoteDB.Constants.intNull)
				dFromDate = .FieldToClass("dFromDate", eRemoteDB.Constants.dtmNull)
				nOfficeagen = .FieldToClass("nOfficeagen", eRemoteDB.Constants.intNull)
				nAgency = .FieldToClass("nAgency", eRemoteDB.Constants.intNull)
                nCashNum = .FieldToClass("nCashnum", 0)
                nFailedLogonAttempts = .FieldToClass("NFAILED_LOGON_ATTEMPTS", eRemoteDB.Constants.intNull)
                dPasswordExpires = .FieldToClass("DPASSWORD_EXPIRES", eRemoteDB.Constants.dtmNull)
				.RCloseRec()
			End If
		End With
		
FindUserInitial_Err: 
		If Err.Number Then
			FindUserInitial = False
		End If
		'UPGRADE_NOTE: Object lrecreaUsersName may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaUsersName = Nothing
	End Function
	
	'**% FindUserErrors :Este método devuelve el usuario y password para conectarse al sistema de errores
	Public Function FindUserErrors() As Boolean
		Dim lclsRegistry As eFunctions.Values
		Dim lsecTime As Object
		
		On Error Resume Next
		
		lsecTime = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.UserValidate")
		lclsRegistry = New eFunctions.Values
		sLogin = lclsRegistry.insGetSetting("INITIALS", String.Empty, "SECURITY")
		sPassWord = lsecTime.StrDecode(Trim(lclsRegistry.insGetSetting("ACCESSWO", String.Empty, "SECURITY")))
		'UPGRADE_NOTE: Object lclsRegistry may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRegistry = Nothing
		'UPGRADE_NOTE: Object lsecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lsecTime = Nothing
		
	End Function
End Class






