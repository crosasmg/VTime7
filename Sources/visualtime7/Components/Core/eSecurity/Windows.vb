Option Strict Off
Option Explicit On
Public Class Windows
	'%-------------------------------------------------------%'
	'% $Workfile:: Windows.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 4/11/03 5:37p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties definition corresponds to the table "Windows"
	'-Se definen las propiedades correspondientes a la tabla Windows.
	
	' Column_name                     Type        Computed   Length      Prec  Scale Nullable   TrimTrailingBlanks  FixedLenNullInSource
	' ------------------------------  ----------- ---------- ----------- ----- ----- ---------- ------------------- ------------------------
	Public sCodispl As String 'char        no         8                       no         no                  no
	Public nAmelevel As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public sCodisp As String 'char        no         8                       yes        no                  yes
	Public nImg_index As Integer 'Int         no         4           10    0     yes        (n/a)               (n/a)
	Public sCodmen As String 'char        no         8                       yes        no                  yes
	Public sDescript As String 'char        no         40                      yes        no                  yes
	Public sDirectgo As String 'char        no         1                       yes        no                  yes
	Public nG_identi As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public nInqlevel As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public nModules As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public sPseudo As String 'char        no         12                      yes        no                  yes
	Public nSequence As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public sShort_des As String 'char        no         12                      yes        no                  yes
	Public sStatregt As String 'char        no         1                       yes        no                  yes
	Public nUsercode As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public nWindowTy As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public sHelpPath As String 'varchar     no         30                      yes        no                  no
	Public nHeight As Integer 'smallint    no         2           5     0     yes        (n/a)               (n/a)
	Public sPathImage As String 'char        no         50                      yes        no                  yes
    Public sAutorep As String 'char        no         1                       yes        no                  yes
    Public nLength_Notes As Integer
    Public ntype_report As Integer
    Public sfilepath As String

    '- Se definen las propiedades auxiliares utilizadas en la carga del menu
    Public nIndPermitted As Integer
	
	'**-Auxiliaries Properties definition used in the SG005 Window -  system transactions
	'-Se definen las propiedades auxiliares utilizadas en la ventana SG005 - Transacciones del sistema.
	Public nIndic As Integer
	
	'**-Auxiliaries variables definition use in the SG009 window - restrict shedule of time of the transactions.
	'-Se definen las variables auxiliares utilizadas en la ventana SG009 - Horario restringido de transacciones.
	Public sHour_start As String
	Public sHour_end As String
	Public nStatusInstance As Integer
	
	'**-Auxiliaries properties definition use in the SG016 window -  Actions in a window.
	'-Se definen las propiedades auxiliares utilizadas en la ventana SG016 - Acciones de una ventana.
	
	Public sType_actio As String
	Public nAction As Integer
	Public sControlkey As String
	Public sHel_actio As String
	Public sShort_acti As String
	Public nActions As Integer
	Public nCounter As Integer
	Public sSel As String
	
	'**-Variables definition that will contain the image quantity
	'-Se define esta variable que contendrá la cantidad de imágenes existentes.
	
	Public nQuantImages As Integer
	
	'-Variables para obtener la ruta de las páginas
	Public sExe_name As String
	Public sFoldername As String
	
	'**%ADD: This method is in charge of adding new records to the table "Windows".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Windows". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lrecWindows As eRemoteDB.Execute
		
		lrecWindows = New eRemoteDB.Execute
		'**+Parameters definition to stored procedure ' insudb.creWindows'
		'+Definición de parámetros para stored procedure 'insudb.creWindows'
		
		On Error GoTo Add_Err
		
		With lrecWindows
			.StoredProcedure = "creWindows"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPseudo", sPseudo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWindowty", nWindowTy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutorep", sAutorep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%reaWindows: Searches by the transaction code in the table "Windows"
	'%reaWindows: Realiza la lectura por el código de la transacción en la tabla Windows.
	Public Function reaWindows(ByVal sCodispl As String) As Boolean
		Dim lrecReaWindows As eRemoteDB.Execute

        On Error GoTo reaWindows_Err
        lrecReaWindows = New eRemoteDB.Execute
		
		With lrecReaWindows
			.StoredProcedure = "reaWindows"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reaWindows = True
				sPseudo = .FieldToClass("sPseudo")
				nWindowTy = .FieldToClass("nWindowty")
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				sCodisp = .FieldToClass("sCodisp")
				nModules = .FieldToClass("nModules")
				sStatregt = .FieldToClass("sStatregt")
				nAmelevel = .FieldToClass("nAmelevel")
				nInqlevel = .FieldToClass("nInqLevel")
				sDirectgo = .FieldToClass("sDirectgo")
				nSequence = .FieldToClass("nSequence")
				sCodmen = .FieldToClass("sCodmen")
				nImg_index = .FieldToClass("nImg_index")
				nG_identi = .FieldToClass("nG_identi")
                sAutorep = .FieldToClass("sAutorep")
                nLength_Notes = .FieldToClass("nLength_Notes")
                sHelpPath = .FieldToClass("sHelpPath")
                nHeight = .FieldToClass("nHeight")
                ntype_report = .FieldToClass("ntype_report")
                sfilepath = .FieldToClass("sfilepath")
                .RCloseRec()
			Else
				reaWindows = False
			End If
		End With
		
reaWindows_Err: 
		If Err.Number Then
			reaWindows = False
		End If
		'UPGRADE_NOTE: Object lrecReaWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaWindows = Nothing
		On Error GoTo 0
	End Function
	
	'**%Delete: This method is in charge of Deleting records in the table "Windows".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "Windows". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete(Optional ByVal lstrCodispl As String = "") As Boolean
		Dim lrecDelWindows As eRemoteDB.Execute
		
		lrecDelWindows = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'**+Parameters definition to stored procedure 'insudb.delWindows'
		'**+Data read on 08/22/2000 14:49:31
		'+Definición de parámetros para stored procedure 'insudb.delWindows'
		'+Información leída el 22/08/2000 14:49:31
		
		sCodispl = lstrCodispl
		
		With lrecDelWindows
			.StoredProcedure = "delWindows"
			
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				nIndic = .FieldToClass("nIndOutput")
				Delete = True
				.RCloseRec()
			Else
				Delete = False
			End If
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecDelWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelWindows = Nothing
		On Error GoTo 0
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Windows".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Windows". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update(ByVal lstrCodispl As String) As Boolean
		Dim lrecUpdWindows As eRemoteDB.Execute
		
		lrecUpdWindows = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		'**+Parameters definition to stored procedure 'insudb.updUsers'
		'+Definición de parámetros para stored procedure 'insudb.updUsers'
		
		With lrecUpdWindows
			Select Case lstrCodispl
				Case "General"
					.StoredProcedure = "updWindows"
					
					.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sPseudo", sPseudo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nModules", nModules, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nAmelevel", nAmelevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nInqLevel", nInqlevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDirectgo", sDirectgo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCodmen", sCodmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nImg_index", nImg_index, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sAutorep", sAutorep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nLength_Notes", nLength_Notes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sHelpPath", sHelpPath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nHeight", nHeight, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("ntype_report", ntype_report, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sfilepath", sfilepath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    Update = .Run(False)
					
				Case "Tablas Generales"
					.StoredProcedure = "updWindowsG_identi"
					
					.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nG_identi", nG_identi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					Update = .Run(False)
					
				Case "Estado"
					.StoredProcedure = "updWindowsStat"
					
					.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					Update = .Run(False)
			End Select
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecUpdWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdWindows = Nothing
		On Error GoTo 0
	End Function
	
	'**%reaWindowsG_identi: Verifies if the identified record is assinged to another table
	'%reaWindowsG_identi: Permite verificar si el identificativo esta asigando a otra tabla.
	Public Function reaWindowsG_identi(ByVal lintIdemTab As Integer) As Boolean
		Dim lrecReaWindowsG_identi As New eRemoteDB.Execute
		
		On Error GoTo reaWindowsG_identi_Err
		lrecReaWindowsG_identi = New eRemoteDB.Execute
		
		reaWindowsG_identi = False
		
		With lrecReaWindowsG_identi
			.StoredProcedure = "reaWindowsG_identi"
			.Parameters.Add("nG_identi", lintIdemTab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				reaWindowsG_identi = True
				sCodispl = .FieldToClass("sCodispl")
				
				.RCloseRec()
			End If
		End With
		
reaWindowsG_identi_Err: 
		If Err.Number Then
			reaWindowsG_identi = False
		End If
		'UPGRADE_NOTE: Object lrecReaWindowsG_identi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaWindowsG_identi = Nothing
		On Error GoTo 0
	End Function
	
	'**%AddWin_Hour: Adds the time restriction information of the transactions
	'%AddWin_Hour: Permite registrar la información de los horarios restringidos de las transacciones.
	Public Function AddWin_Hour() As Boolean
		Dim lrecWin_hour As eRemoteDB.Execute
		
		lrecWin_hour = New eRemoteDB.Execute
		'**+Parameters definition to stored procedure 'insudb.creWindows'
		'+Definición de parámetros para stored procedure 'insudb.creWindows'
		
		On Error GoTo AddWin_Hour_Err
		
		With lrecWin_hour
			.StoredProcedure = "creWin_hour"
			
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_start", sHour_start, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_end", sHour_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AddWin_Hour = .Run(False)
		End With
		
AddWin_Hour_Err: 
		If Err.Number Then
			AddWin_Hour = False
		End If
		'UPGRADE_NOTE: Object lrecWin_hour may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWin_hour = Nothing
		On Error GoTo 0
	End Function
	
	'**%AddWin_actions: Adds the information of the actions of the window.
	'%AddWin_actions: Permite registrar la información de las acciones de una ventana.
	Public Function AddWin_actions() As Boolean
		Dim lrecWin_actions As eRemoteDB.Execute
		
		lrecWin_actions = New eRemoteDB.Execute
		'**+Parameters definiton to stored procedure 'insudb.creWindows'
		'+Definición de parámetros para stored procedure 'insudb.creWindows'
		
		On Error GoTo AddWin_actions_Err
		
		With lrecWin_actions
			.StoredProcedure = "creWin_actions"
			
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeq", nCounter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AddWin_actions = .Run(False)
		End With
		
AddWin_actions_Err: 
		If Err.Number Then
			AddWin_actions = False
		End If
		'UPGRADE_NOTE: Object lrecWin_actions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWin_actions = Nothing
		On Error GoTo 0
	End Function
	
	'**%DeleteWin_actions: This method deletes the information of the table "Win_actions" - Windows actions.
	'%DeleteWin_actions: Esta función permite borrar la información de la tabla "Win_actions" - Acciones de
	'%una ventana.
	Public Function DeleteWin_actions() As Boolean
		Dim lrecDelWin_actions As eRemoteDB.Execute
		
		lrecDelWin_actions = New eRemoteDB.Execute
		
		On Error GoTo DeleteWin_actions_Err
		'**+Parameters Definiton to stored procedure 'insudb.delWindows'
		'**+Data read on 08/22/2000 14:49:31
		'+Definición de parámetros para stored procedure 'insudb.delWindows'
		'+Información leída el 22/08/2000 14:49:31
		
		With lrecDelWin_actions
			.StoredProcedure = "delWin_actions"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteWin_actions = .Run(False)
		End With
		
DeleteWin_actions_Err: 
		If Err.Number Then
			DeleteWin_actions = False
		End If
		'UPGRADE_NOTE: Object lrecDelWin_actions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelWin_actions = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValSG005_K: This method validates the header section of the page "SG005_K" as described in the
	'%               functional specifications
	'%InsValSG005_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%               descritas en el funcional de la ventana "SG005_K"
	Public Function InsValSG005_K(ByVal sCodisLog As String, ByVal nAction As Integer, ByVal sCodispl As String, ByVal sPseudo As String, ByVal nWindowTy As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lblnIndic As Boolean
		
		On Error GoTo InsValSG005_K_Err
		
		lerrTime = New eFunctions.Errors
		
		lblnIndic = False
		'+ Validates the "Logical" code.
		'+ Se realizan las validaciones del código "Lógico".
		
		Select Case nAction
			'+ If the action is Add
			'+ Si la acción es Registrar.
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				If Trim(sCodispl) = String.Empty Then
					Call lerrTime.ErrorMessage(sCodisLog, 12060)
				Else
					If InsValWindows(sCodispl) Then
						Call lerrTime.ErrorMessage(sCodisLog, 12013)
					End If
				End If
				'+ If the action is Modify or Cut
				'+ Si la acción es Modificar o cortar
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate, eFunctions.Menues.TypeActions.clngActioncut
				If Trim(sCodispl) = String.Empty Then
					Call lerrTime.ErrorMessage(sCodisLog, 12060)
				Else
					If Not InsValWindows(sCodispl) Then
						Call lerrTime.ErrorMessage(sCodisLog, 12014)
						lblnIndic = True
					End If
				End If
				'+ If the action is Inquire
				'+ Si la acción es Consultar.
				
			Case eFunctions.Menues.TypeActions.clngActionQuery
				If Trim(sCodispl) <> String.Empty Then
					If Not InsValWindows(sCodispl) Then
						Call lerrTime.ErrorMessage(sCodisLog, 12014)
					End If
				End If
		End Select
		'+ Validates the "Alias"
		'+ Se realizan las validaciones del "Pseudónimo.
		
		Select Case nAction
			'+ If the action is Inquire or Modify.
			'+ Si la acción es Registrar o Modificar.
			
			Case eFunctions.Menues.TypeActions.clngActionadd, eFunctions.Menues.TypeActions.clngActionUpdate
				If Not lblnIndic Or Trim(sCodispl) <> String.Empty Then
					If Not insReaWindowsPseudo1(sCodispl) Then
						If Trim(sPseudo) = String.Empty Then
							Call lerrTime.ErrorMessage(sCodisLog, 12061)
						Else
							If Not lblnIndic Then
								If InsReaWindowsPseudo(sCodispl, sPseudo) Then
									Call lerrTime.ErrorMessage(sCodisLog, 12015)
								End If
							End If
						End If
					End If
				End If
				'+ If the action is Inquire
				'+ Si la acción es Consultar
				
			Case eFunctions.Menues.TypeActions.clngActionQuery
				If Trim(sCodispl) = String.Empty Then
					If Trim(sPseudo) = String.Empty Then
						Call lerrTime.ErrorMessage(sCodisLog, 80030)
					Else
						If Trim(sCodispl) = String.Empty Then
							Call lerrTime.ErrorMessage(sCodisLog, 12014)
						Else
							If Not InsReaWindowsPseudo(sCodispl, sPseudo) Then
								Call lerrTime.ErrorMessage(sCodisLog, 12014)
							End If
						End If
					End If
				End If
		End Select
		
		'+ Validates the field "Transaction Type"
		'+ Se realizan las validaciones del campo Tipo de transacción.
		If Not lblnIndic Then
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActioncut Then
				If Not insReaWindowsPseudo1(sCodispl) Then
					If nWindowTy = 0 Or nWindowTy = eRemoteDB.Constants.intNull Then
						Call lerrTime.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "(Tipo de transacción) ")
					End If
				End If
			End If
		End If
		
		InsValSG005_K = lerrTime.Confirm
		
InsValSG005_K_Err: 
		If Err.Number Then
			InsValSG005_K = "InsValSG005_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsValWindows: Searches by the transaction code.
	'%InsValWindows: Realiza la lectura por el código de la transacción.
	Public Function InsValWindows(ByRef pstrCodispl As String) As Boolean
		Dim lrecRecordset As eRemoteDB.Execute
		
		lrecRecordset = New eRemoteDB.Execute
		On Error GoTo InsValWindows_Err
		
		With lrecRecordset
			.StoredProcedure = "reaWindows"
			.Parameters.Add("sCodispl", pstrCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsValWindows = True
				nWindowTy = .FieldToClass("nWindowty")
				.RCloseRec()
			End If
		End With
		
InsValWindows_Err: 
		If Err.Number Then
			InsValWindows = False
		End If
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsReaWindowsPseudo: Searches by the alias
	'%InsReaWindowsPseudo: Realiza la lectura por el pseudonimo
	Public Function InsReaWindowsPseudo(ByVal pstrCodispl As String, ByVal pstrPseudo As String) As Boolean
		Dim lrecRecordset As eRemoteDB.Execute
		
		On Error GoTo InsReaWindowsPseudo_err
		lrecRecordset = New eRemoteDB.Execute
		
		With lrecRecordset
			.StoredProcedure = "reaWindowsPseudo"
			.Parameters.Add("sPseudo", pstrPseudo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Trim(.FieldToClass("sCodispl")) <> Trim(pstrCodispl) Then
					InsReaWindowsPseudo = True
				End If
				.RCloseRec()
			End If
		End With
		
InsReaWindowsPseudo_err: 
		If Err.Number Then
			InsReaWindowsPseudo = False
		End If
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostSG005_K: This method updates the database (as described in the functional specifications)
	'**%for the page "SG005_K"
	'%insPostSG005_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "SG005_K"
	Public Function insPostSG005_k(ByVal nAction As Integer, ByVal lstrCodispl As String, ByVal lstrPseudo As String, ByVal lintWindowty As Integer, ByVal lintUsercode As Integer) As Boolean
		insPostSG005_k = True
		
		On Error GoTo insPostSG005_k_Err
		
		sCodispl = lstrCodispl
		sPseudo = lstrPseudo
		nWindowTy = lintWindowty
		nUsercode = lintUsercode
		sAutorep = "2"
		'**+If the select option is Add or Duplicate
		'+Si la opción seleccionada es Registrar o Duplicar.
		
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				insPostSG005_k = Add()
			Case eFunctions.Menues.TypeActions.clngActionQuery
				
		End Select
		
insPostSG005_k_Err: 
		If Err.Number Then
			insPostSG005_k = False
		End If
		On Error GoTo 0
	End Function
	
	'**%LoadTabs: Constructs the sequence in HTML code
	'%LoadTabs: arma la secuencia en código HTML
	Public Function LoadTabs(ByVal nMainAction As Integer, ByVal sCodispLog As String, ByVal sPseudo As String, ByVal nWindowTy As Integer, ByVal sUserSchema As String, ByVal nUsercode As Integer) As String
		Dim lclsSecurSche As eSecurity.Secur_sche
		Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lrecWindows As eRemoteDB.Query
		Dim lclsSequence As eFunctions.Sequence
		Dim lintCountWindows As Integer
		Dim lstrHTMLCode As String
		Dim lstrSequence As String
        Dim lstrCodispl As String
        Dim lstrCodisp As String = ""
        Dim lstrShort_desc As String = ""
        Dim lblnProcess As Boolean
		Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		
		On Error GoTo LoadTabs_Err
		
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		lclsSecurSche = New eSecurity.Secur_sche
		
		lstrHTMLCode = String.Empty
		
		lstrSequence = "SG005   "
		
		If nWindowTy <> eFunctions.Menues.TypeForm.clngFraSpecific And nWindowTy <> eFunctions.Menues.TypeForm.clngFraRepetitive And nWindowTy <> eFunctions.Menues.TypeForm.clngGeneralTable And nWindowTy <> eFunctions.Menues.TypeForm.clngWindowsPopUp And nWindowTy <> 0 Then
			lstrSequence = lstrSequence & "SG016   "
		End If
		
		If nWindowTy = eFunctions.Menues.TypeForm.clngGeneralTable Then
			lstrSequence = lstrSequence & "SG006   "
		End If
		
		If nWindowTy <> eFunctions.Menues.TypeForm.clngFraSpecific And nWindowTy <> eFunctions.Menues.TypeForm.clngFraRepetitive And nWindowTy <> eFunctions.Menues.TypeForm.clngWindowsPopUp And nWindowTy <> 0 Then
			lstrSequence = lstrSequence & "SG009   "
		End If
		
		lstrHTMLCode = lclsSequence.makeTable
		
		lintCountWindows = 1
		lstrCodispl = Trim(Mid(lstrSequence, lintCountWindows, 8))
		
		If ValSequenWin(sCodispLog) Then
			Do While lstrCodispl <> String.Empty
				lblnProcess = True
				lblnRequired = False
				lblnContent = False
				'**+SG005 - System transaction
				'+SG005 - Transacciones del sistema.
				
				If lstrCodispl = "SG005" Then
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(sDescript) Or Trim(sDescript) = String.Empty Then
						If nMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
							lblnProcess = False
						Else
							lblnRequired = True
							lblnContent = False
						End If
					Else
						lblnRequired = False
						lblnContent = True
					End If
				End If
				'**+Actions in a windows.
				'+Acciones de una ventana.
				
				If lstrCodispl = "SG016" Then
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If IsDbNull(nAction) Or nAction = 0 Then
						If nMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
							lblnProcess = False
						End If
					Else
						lblnContent = True
					End If
				End If
				'**+General tables information
				'+Información de tablas generales.
				
				If lstrCodispl = "SG006" Then
					If nG_identi = 0 Then
						If nMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
							lblnProcess = False
						Else
							lblnRequired = True
							lblnContent = False
						End If
					Else
						lblnRequired = False
						lblnContent = True
					End If
				End If
				'**+Restrices time of the Transactions
				'+Horario restringido de transacciones.
				
				If lstrCodispl = "SG009" Then
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Trim(sHour_start) = String.Empty Or IsDbNull(sHour_start) Then
						If nMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
							lblnProcess = False
						End If
					Else
						lblnContent = True
					End If
				End If
				
				If lblnProcess Then
					'**+Assing the values to the description variables
					'+Se asignan los valores a las variables de descripción.
					
					If lrecWindows.OpenQuery("windows", "sCodisp, sShort_des", "scodispl='" & lstrCodispl & "'") Then
						lstrCodisp = lrecWindows.FieldToClass("sCodisp")
						lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
						
						lrecWindows.CloseQuery()
					End If
					'**+Search for the image to put in the links.
					'+Se busca la imagen a colocar en los links.
					
					With lclsSecurSche
						If .FindLevels(sUserSchema) Then
							If .ItemLevels(sUserSchema, Secur_sche.eTypeCode.Window, lstrCodispl) Then
								If lblnContent Then
									mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
								Else
									If lblnRequired Then
										mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
									Else
										mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
									End If
								End If
							Else
								If Not lblnContent Then
									If lblnRequired Then
										mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
									Else
										mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
									End If
								Else
									mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
								End If
							End If
						Else
							If Not lblnContent Then
								If lblnRequired Then
									mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
								Else
									mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
								End If
							Else
								mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
							End If
						End If
					End With
					
					lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nMainAction, lstrShort_desc, mintPageImage)
				End If
				'**+Move to the following record that has been found
				'+Se mueve al siguiente registro encontrado
				
				lintCountWindows = lintCountWindows + 8
				lstrCodispl = Trim(Mid(lstrSequence, lintCountWindows, 8))
			Loop 
			
			lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		End If
		
		LoadTabs = lstrHTMLCode
		
LoadTabs_Err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		On Error GoTo 0
	End Function
	
	'**%ValSequenWin: This method verifies if any required frame has information, for the Tab that is not required
	'%ValSequenWin: Esta funcion se encarga de verificar si alguno de los frames requeridos tienen información,
	'%para que el Tab no sea requerido.
	Public Function ValSequenWin(ByVal sCodispl As String) As Boolean
		Dim lrecValSequenWin As eRemoteDB.Execute
		
		lrecValSequenWin = New eRemoteDB.Execute
		On Error GoTo ValSequenWin_Err
		
		With lrecValSequenWin
			.StoredProcedure = "ValSequenWin"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				ValSequenWin = True
				sDescript = .FieldToClass("sDescript")
				nAction = .FieldToClass("nAction")
				nG_identi = .FieldToClass("nG_identi")
				sHour_start = .FieldToClass("sHour_start")
				.RCloseRec()
			End If
		End With
		
ValSequenWin_Err: 
		If Err.Number Then
			ValSequenWin = False
		End If
		'UPGRADE_NOTE: Object lrecValSequenWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValSequenWin = Nothing
		On Error GoTo 0
	End Function
	
	'**%ValSequenWinFinish: This method validates as described in the functional specifications
	'%ValSequenWinFinish: Este metodo se encarga de realizar las validaciones descritas en el funcional
	Public Function ValSequenWinFinish(ByVal sCodispl As String, ByVal nWindowTy As Integer) As Boolean
		Dim lrecValSequenWin As eRemoteDB.Execute
		
		lrecValSequenWin = New eRemoteDB.Execute
		On Error GoTo ValSequenWinFinish_Err
		
		With lrecValSequenWin
			.StoredProcedure = "valSequenWin"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sDescript = .FieldToClass("sDescript")
				nG_identi = .FieldToClass("nG_identi")
				If nWindowTy <> eFunctions.Menues.TypeForm.clngGeneralTable Then
					'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(sDescript) And Not IsNothing(sDescript) And Trim(sDescript) <> String.Empty And Trim(sDescript) <> "0" Then
						ValSequenWinFinish = True
					End If
				Else
					'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(sDescript) And Not IsNothing(sDescript) And Trim(sDescript) <> String.Empty And Trim(sDescript) <> "0" Then
						ValSequenWinFinish = True
					End If
					
					If nG_identi <> 0 And nG_identi <> eRemoteDB.Constants.intNull Then
						ValSequenWinFinish = True
					Else
						ValSequenWinFinish = False
					End If
				End If
				.RCloseRec()
			End If
		End With
		
ValSequenWinFinish_Err: 
		If Err.Number Then
			ValSequenWinFinish = False
		End If
		'UPGRADE_NOTE: Object lrecValSequenWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValSequenWin = Nothing
		On Error GoTo 0
	End Function
	
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nQuantImages = 8
		sCodispl = String.Empty
		nAmelevel = eRemoteDB.Constants.intNull
		sCodisp = String.Empty
		nImg_index = eRemoteDB.Constants.intNull
		sCodmen = String.Empty
		sDescript = String.Empty
		sDirectgo = String.Empty
		nG_identi = eRemoteDB.Constants.intNull
		nInqlevel = eRemoteDB.Constants.intNull
		nModules = eRemoteDB.Constants.intNull
		sPseudo = String.Empty
		nSequence = eRemoteDB.Constants.intNull
		sShort_des = String.Empty
		sStatregt = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		nWindowTy = eRemoteDB.Constants.intNull
		sHelpPath = String.Empty
		nHeight = eRemoteDB.Constants.intNull
		sAutorep = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%PathImages: Keeps the images route
	'%PathImages: Permite guardar la ruta de las imágenes.
	Public Function PathImages(ByVal nIndex As Integer) As String
		PathImages = String.Empty
		
		Select Case nIndex
			Case 1
                PathImages = "/VTimeNet/images/menu_transaction.png"
            Case 2
                PathImages = "/VTimeNet/images/menu_query.png"
            Case 3
                PathImages = "/VTimeNet/images/menu_maintance.png"
            Case 4
                PathImages = "/VTimeNet/images/Printer.png"
            Case 5
                PathImages = "/VTimeNet/images/batchStat03.png"
            Case 6
                PathImages = "/VTimeNet/images/btnWNotes.png"
            Case 7
                PathImages = "/VTimeNet/images/Opfolder.png"
            Case 8
                PathImages = "/VTimeNet/images/clfolder.png"
        End Select
	End Function
	
	'**%insValSG005: This method validates the page "SG005" as described in the functional specifications
	'%InsValSG005: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG005"
	Public Function insValSG005(ByVal sCodispl As String, ByVal sCodispLog As String, ByVal sPseudo As String, ByVal lintWindowty As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sPseudoF As String, ByVal sCodisp As String, ByVal nModules As Integer, ByVal sDirectgo As String, ByVal sCodmen As String, ByVal nSequence As Integer, ByVal nAmelevel As Integer, ByVal nInqlevel As Integer, ByVal nImage_index As Integer, ByVal sAutorep As String, Optional ByRef llngMainAction As Integer = 0) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValSG005_Err
		
		lerrTime = New eFunctions.Errors
		
		If llngMainAction > 0 And llngMainAction <> 303 Then
			'**+Validates the field "Description"
			'+Se realizan las validaciones de la "Descripción".
			If Trim(sDescript) = String.Empty Then
				Call lerrTime.ErrorMessage(sCodispl, 12018)
			End If
			
			'**+Validates the field "Short description"
			'+Se realizan las validaciones de la "Descripción corta".
			If Trim(sShort_des) = String.Empty Then
				Call lerrTime.ErrorMessage(sCodispl, 12019)
			End If
			
			'**+Validates the field "Alias"
			'+Se realizan las validaciones del "Pseudónimo".
			If Trim(sPseudoF) = String.Empty Then
				Call lerrTime.ErrorMessage(sCodispLog, 12061)
			Else
				If InsReaWindowsPseudo(sCodispLog, sPseudo) Then
					Call lerrTime.ErrorMessage(sCodispl, 12015)
				End If
			End If
			
			'**+Validates the "Physical" code.
			'+Se realizan las validaciones del código "Físico".
			If Trim(sCodisp) = String.Empty Then
				Call lerrTime.ErrorMessage(sCodispl, 12017)
			End If
			
			'**+Validates the "Module".
			'+Se realizan las validaciones del "Módulo".
			If nModules = eRemoteDB.Constants.intNull Then
				Call lerrTime.ErrorMessage(sCodispl, 1901)
			End If
			
			'**+Validates the "Menu that invoked it"
			'+Se realizan las validaciones del "Menú que lo invoca".
			If sDirectgo = "1" Then
				If Trim(sCodmen) = String.Empty Then
					Call lerrTime.ErrorMessage(sCodispl, 12065)
				Else
					If lintWindowty <> eFunctions.Menues.TypeForm.clngFraSpecific And lintWindowty <> eFunctions.Menues.TypeForm.clngFraRepetitive And lintWindowty <> eFunctions.Menues.TypeForm.clngWindowsPopUp And lintWindowty <> eFunctions.Menues.TypeForm.clngSeqWithOutHeader And lintWindowty <> 0 Then
						If Not InsValWindows(sCodmen) Then
							Call lerrTime.ErrorMessage(sCodispl, 12020)
						Else
							If Trim(UCase(sCodmen)) = Trim(sCodispLog) Then
								Call lerrTime.ErrorMessage(sCodispl, 12020)
							Else
								If nWindowTy <> 8 Then
									Call lerrTime.ErrorMessage(sCodispl, 12024,  , eFunctions.Errors.TextAlign.LeftAling, "Menú que lo invoca: ")
								End If
							End If
						End If
					End If
				End If
			End If
			
			'**+Validates the "sequence"
			'+Se realizan las validaciones de la "Secuencia".
			If lintWindowty <> eFunctions.Menues.TypeForm.clngWindowsPopUp And lintWindowty <> eFunctions.Menues.TypeForm.clngSeqWithOutHeader And lintWindowty <> 0 Then
				If nSequence <> eRemoteDB.Constants.intNull Then
					If insReaWindowsSequence(sCodmen, nSequence, sCodispLog) Then
						Call lerrTime.ErrorMessage(sCodispl, 12021)
					End If
				End If
			End If
			
			'**+Validates the field "Updates".
			'+Se realizan las validaciones del campo "Actualización".
			If nAmelevel = eRemoteDB.Constants.intNull Then
				Call lerrTime.ErrorMessage(sCodispl, 12059)
			Else
				If nAmelevel < 0 Or nAmelevel > 9 Then
					Call lerrTime.ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.LeftAling, "[0-9]")
				End If
			End If
			
			'**+Validates the field "Consultation"
			'+Se realizan las validaciones del campo "Consulta".
			If nInqlevel = eRemoteDB.Constants.intNull Then
				Call lerrTime.ErrorMessage(sCodispl, 12059)
			Else
				If nInqlevel < 0 Or nInqlevel > 9 Then
					Call lerrTime.ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.LeftAling, "[0-9]")
				End If
			End If
			
			'**+Validates the "Asociate image"
			'+Se realizan las validaciones de la "Imagen asociada".
			If lintWindowty <> eFunctions.Menues.TypeForm.clngFraSpecific And lintWindowty <> eFunctions.Menues.TypeForm.clngFraRepetitive And lintWindowty <> eFunctions.Menues.TypeForm.clngWindowsPopUp And lintWindowty <> eFunctions.Menues.TypeForm.clngSeqWithOutHeader And lintWindowty <> 0 And lintWindowty <> eFunctions.Menues.TypeForm.clngMenu Then
				If nImage_index = eRemoteDB.Constants.intNull Then
					Call lerrTime.ErrorMessage(sCodispl, 80032)
				End If
			End If
		End If
		
		insValSG005 = lerrTime.Confirm
		
insValSG005_Err: 
		If Err.Number Then
			insValSG005 = insValSG005 & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsReaWindowsMaxSeq: Searches for the menu and transaction code to select
	'**%the maximun sequence number that exists
	'%InsReaWindowsMaxSeq: Realiza la lectura por el codigo de la menu y transaccion para seleccionar
	'%el máximo número de secuencia existente.
	Public Function InsReaWindowsMaxSeq(ByVal pstrCodmen As String, ByVal pstrCodispl As String) As Integer
		Dim lrecRecordset As eRemoteDB.Execute
		
		On Error GoTo InsReaWindowsMaxSeq_err
		lrecRecordset = New eRemoteDB.Execute
		
		With lrecRecordset
			.StoredProcedure = "reaWindowsMaxSeq"
			.Parameters.Add("sCodmen", pstrCodmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("scodispl", pstrCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsReaWindowsMaxSeq = .FieldToClass("Max")
				.RCloseRec()
			Else
				InsReaWindowsMaxSeq = 0
			End If
		End With
		
InsReaWindowsMaxSeq_err: 
		If Err.Number Then
			InsReaWindowsMaxSeq = 0
		End If
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		On Error GoTo 0
	End Function
	
	'**%insReaWindowSequence: Searches for the menu and transaction code.
	'%insReaWindowsSequence: Realiza la lectura por el codigo de la menu y transaccion
	Public Function insReaWindowsSequence(ByVal pstrCodmen As String, ByVal pintSequence As Integer, ByVal pstrCodispl As String) As Boolean
		Dim lrecRecordset As eRemoteDB.Execute
		
		On Error GoTo insReaWindowsSequence_err
		lrecRecordset = New eRemoteDB.Execute
		With lrecRecordset
			.StoredProcedure = "reaWindowsSequence"
			.Parameters.Add("sCodmen", pstrCodmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", pintSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insReaWindowsSequence = Not Trim(.FieldToClass("sCodispl")) = Trim(pstrCodispl)
				.RCloseRec()
			Else
				insReaWindowsSequence = False
			End If
		End With
		
insReaWindowsSequence_err: 
		If Err.Number Then
			insReaWindowsSequence = False
		End If
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		On Error GoTo 0
	End Function

    '**%insPostSG005: This method updates the database (as described in the functional specifications)
    '**%for the page "SG005"
    '%insPostSG005: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
    '%especificaciones funcionales)de la ventana "SG005"
    Public Function insPostSG005(ByVal lstrCodispl As String, ByVal lstrDescript As String, ByVal lstrShort_des As String, ByVal lstrPseudo As String, ByVal lstrCodisp As String, ByVal lintModules As Integer, ByVal lstrStatregt As String, ByVal lintAmelevel As Integer, ByVal lintInqlevel As Integer, ByVal lintSequence As Integer, ByVal lstrDirectgo As String, ByVal lstrCodmen As String, ByVal lintImg_index As Integer, ByVal lintUsercode As Integer, ByVal lstrAutorep As String, Optional ByRef llngMainAction As Integer = 0, Optional lintLength_Notes As Integer = 0, Optional lintHeight As Integer = 0, Optional linttype_report As Integer = 0, Optional lstrfilepath As String = "", Optional lstrHelpPath As String = "") As Boolean

        Dim lsclValues_cache As eFunctions.Values

        On Error GoTo insPostSG005_Err

        insPostSG005 = True
        sCodispl = lstrCodispl
        sDescript = lstrDescript
        sShort_des = lstrShort_des
        sPseudo = lstrPseudo
        sCodisp = lstrCodisp
        nModules = lintModules
        sStatregt = lstrStatregt
        nAmelevel = lintAmelevel
        nInqlevel = lintInqlevel
        nLength_Notes = lintLength_Notes

        sDirectgo = lstrDirectgo
        sAutorep = lstrAutorep
        sCodmen = lstrCodmen
        nUsercode = lintUsercode
        nImg_index = lintImg_index

        nHeight = lintHeight
        ntype_report = linttype_report
        sfilepath = lstrfilepath
        sHelpPath = lstrHelpPath

        If lintSequence = 0 Then
            nSequence = InsReaWindowsMaxSeq(lstrCodmen, lstrCodispl) + 4
        Else
            nSequence = lintSequence
        End If

        If llngMainAction <> eFunctions.Menues.TypeActions.clngActioncut And llngMainAction > 0 Then
            insPostSG005 = Update("General")
        End If

        If llngMainAction = eFunctions.Menues.TypeActions.clngActioncut And llngMainAction > 0 Then
            insPostSG005 = Delete(sCodispl)
        End If

        If insPostSG005 Then
            lsclValues_cache = New eFunctions.Values
            Call lsclValues_cache.DelCache(4, lstrCodispl, lstrCodmen)
        End If

insPostSG005_Err:
        If Err.Number Then
            insPostSG005 = False
        End If
        'UPGRADE_NOTE: Object lsclValues_cache may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lsclValues_cache = Nothing
        On Error GoTo 0
    End Function
    '**%DeleteWin_Hour: Delete the information from the table "Win_hour" -
    '**%Restricted hours of the Transactions
    '%DeleteWin_Hour: Esta función permite borrar la información de la tabla Win_hour - Horario
    '%restringido de transacciones.
    Public Function DeleteWin_Hour(ByVal lstrCodispl As String, ByVal lstrHour_Start As String) As Boolean
		Dim lrecDelWin_hour As eRemoteDB.Execute
		
		lrecDelWin_hour = New eRemoteDB.Execute
		
		On Error GoTo DeleteWin_Hour_Err
		
		'**+Parameters definition to stored procedure 'insudb.delWindows'
		'**+Data read on 08/22/2000 14:49:31
		'+Definición de parámetros para stored procedure 'insudb.delWindows'
		'+Información leída el 22/08/2000 14:49:31
		
		With lrecDelWin_hour
			.StoredProcedure = "delWin_hour1"
			
			.Parameters.Add("sCodispl", lstrCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_Start", lstrHour_Start, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				DeleteWin_Hour = True
			Else
				DeleteWin_Hour = False
			End If
		End With
		
DeleteWin_Hour_Err: 
		If Err.Number Then
			DeleteWin_Hour = False
		End If
		'UPGRADE_NOTE: Object lrecDelWin_hour may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelWin_hour = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValSG009: This method validates the page "SG009" as described in the functional specifications
	'%InsValSG009: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG009"
	Public Function insValSG009(ByVal nAction As String, ByVal sCodispl As String, ByVal sCodispLog As String, ByVal sHour_start As String, ByVal sHour_end As String) As String
		Dim lerrTime As eFunctions.Errors
		Dim lstrNumber As String
		
		On Error GoTo insValSG009_Err
		
		lerrTime = New eFunctions.Errors
		'**+Validates the "Initial hour"
		'+Se realizan las validaciones de la "Hora Inicial".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If (IsDbNull(sHour_start) Or IsNothing(sHour_start) Or Trim(sHour_start) = String.Empty) And (IsDbNull(sHour_end) Or IsNothing(sHour_end) Or Trim(sHour_end) = String.Empty) Then
			Call lerrTime.ErrorMessage(sCodispl, 12160)
		Else
			If Not IsDate(sHour_start) Or Len(sHour_start) < 5 Then
				lstrNumber = Mid(sHour_start, 1, 2)
				
				If IsNumeric(lstrNumber) Then
					If CShort(lstrNumber) >= 24 Then
						Call lerrTime.ErrorMessage(sCodispl, 12140)
					Else
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Hora inicial) ")
					End If
				Else
					'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(sHour_end) And Not IsNothing(sHour_end) And Trim(sHour_end) <> String.Empty And Len(sHour_end) = 5 Then
						Call lerrTime.ErrorMessage(sCodispl, 12051)
					Else
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Hora inicial) ")
					End If
				End If
			Else
				If nAction = "Add" Then
					If insReaWin_hour(sCodispLog, sHour_start) Then
						Call lerrTime.ErrorMessage(sCodispl, 12085)
					End If
				End If
			End If
		End If
		'**+Validates the "end hour"
		'+Se realizan las validaciones de la "Hora Final".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sHour_end) Or IsNothing(sHour_end) Or Trim(sHour_end) = String.Empty Then
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If Not IsDbNull(sHour_start) And Not IsNothing(sHour_start) And Trim(sHour_start) <> String.Empty And Len(sHour_start) = 5 Then
				Call lerrTime.ErrorMessage(sCodispl, 12167)
			End If
		Else
			If Not IsDate(sHour_end) Or Len(sHour_end) < 5 Then
				lstrNumber = Mid(sHour_end, 1, 2)
				
				If IsNumeric(lstrNumber) Then
					If CShort(lstrNumber) >= 24 Then
						Call lerrTime.ErrorMessage(sCodispl, 12140)
					Else
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Hora final) ")
					End If
				Else
					'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(sHour_start) And Not IsNothing(sHour_start) And Trim(sHour_start) <> String.Empty And Len(sHour_start) = 5 Then
						Call lerrTime.ErrorMessage(sCodispl, 12167)
					Else
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Hora final) ")
					End If
				End If
			Else
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(sHour_start) And Not IsNothing(sHour_start) And Trim(sHour_start) <> String.Empty And Len(sHour_start) = 5 Then
					If sHour_end <= sHour_start Then
						Call lerrTime.ErrorMessage(sCodispl, 12084)
					End If
				End If
			End If
		End If
		
		insValSG009 = lerrTime.Confirm
		
insValSG009_Err: 
		If Err.Number Then
			insValSG009 = insValSG009 & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostSG009: This method updates the database (as described in the functional specifications)
	'**%for the page "SG009"
	'%insPostSG009: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "SG009"
	Public Function insPostSG009(ByVal lstrAction As String, ByVal lstrCodispl As String, ByVal lstrHour_Start As String, ByVal lstrHour_End As String, ByVal lintUsercode As Integer) As Boolean
		insPostSG009 = True
		
		On Error GoTo insPostSG009_Err
		
		sCodispl = lstrCodispl
		sHour_start = lstrHour_Start
		sHour_end = lstrHour_End
		nUsercode = lintUsercode
		
		Select Case lstrAction
			Case "Add"
				insPostSG009 = AddWin_Hour
				
			Case "Update"
				insPostSG009 = UpdateWin_Hour
		End Select
		
insPostSG009_Err: 
		If Err.Number Then
			insPostSG009 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%UpdateWin_Hour: Update the information for the restricted hours of the transactions
	'%UpdateWin_Hour: Permite actualizar la información de los horarios restringidos de las transacciones.
	Public Function UpdateWin_Hour() As Boolean
		Dim lrecUpdWin_Hour As eRemoteDB.Execute
		
		lrecUpdWin_Hour = New eRemoteDB.Execute
		
		On Error GoTo UpdateWin_Hour_Err
		'**+Parameters Definition to stored procedure 'insudb.UpdWin_hour'
		'+Definición de parámetros para stored procedure 'insudb.UpdWin_hour'
		
		With lrecUpdWin_Hour
			.StoredProcedure = "UpdWin_hour"
			
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_Start", sHour_start, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_End", sHour_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateWin_Hour = .Run(False)
		End With
		
UpdateWin_Hour_Err: 
		If Err.Number Then
			UpdateWin_Hour = False
		End If
		'UPGRADE_NOTE: Object lrecUpdWin_Hour may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdWin_Hour = Nothing
		On Error GoTo 0
	End Function
	
	'**%insReaWin_hour: Verifies if a duplicate exists in the table "Win_hour"
	'%insReaWin_hour: Permite verificar si existe duplicado en la tabla Win_hour.
	Public Function insReaWin_hour(ByVal pstrCodispl As String, ByVal pstrHour_start As String) As Boolean
		Dim lrecRecordset As eRemoteDB.Execute
		
		lrecRecordset = New eRemoteDB.Execute
		On Error GoTo insReaWin_hour_Err
		
		insReaWin_hour = False
		
		With lrecRecordset
			.StoredProcedure = "reaWin_hour1"
			
			.Parameters.Add("sCodispl", pstrCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHour_start", pstrHour_start, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insReaWin_hour = True
				.RCloseRec()
			End If
		End With
		
insReaWin_hour_Err: 
		If Err.Number Then
			insReaWin_hour = False
		End If
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		On Error GoTo 0
	End Function
	
	'**%insReaWindowsPseudo1: Searches by the logical code "sCodispl" or by alias
	'%insReaWindowsPseudo1: Permite realizar la lectura bien sea por Código lógico (sCodispl) o por
	'%Pseudónimo.
	Public Function insReaWindowsPseudo1(Optional ByVal lstrCodispl As String = "", Optional ByVal lstrPseudo As String = "") As Boolean
		Dim lrecWindows As eRemoteDB.Execute
		
		lrecWindows = New eRemoteDB.Execute
		On Error GoTo insReaWindowsPseudo1_Err
		
		insReaWindowsPseudo1 = False
		
		With lrecWindows
			.StoredProcedure = "reaWindowsPseudo1"
			
			.Parameters.Add("sCodispl", lstrCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPseudo", lstrPseudo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insReaWindowsPseudo1 = True
				
				sCodispl = .FieldToClass("sCodispl")
				sPseudo = .FieldToClass("sPseudo")
				nWindowTy = .FieldToClass("nWindowty")
				sDescript = .FieldToClass("sDescript")
				
				.RCloseRec()
			End If
		End With
		
insReaWindowsPseudo1_Err: 
		If Err.Number Then
			insReaWindowsPseudo1 = False
		End If
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValSG006: This method validates the page "SG006" as described in the functional specifications
	'%InsValSG006: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG006"
	Public Function insValSG006(ByVal sCodispTrans As String, ByVal sCodispLog As String, ByVal nG_identi As Integer) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValSG006_Err
		
		lerrTime = New eFunctions.Errors
		'**+Validates the field "table informations"
		'+Se realizan las validaciones del campo "identificación de la Tabla".
		
		If nG_identi = 0 Or nG_identi = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispTrans, 12026)
		Else
			If reaWindowsG_identi(nG_identi) Then
				If Trim(sCodispl) <> Trim(UCase(sCodispLog)) Then
					Call lerrTime.ErrorMessage(sCodispTrans, 12027)
				End If
			End If
		End If
		
		insValSG006 = lerrTime.Confirm
		
insValSG006_Err: 
		If Err.Number Then
			insValSG006 = "insValSG006: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostSG006: This method updates the database (as described in the functional specifications)
	'**%for the page "SG006"
	'%insPostSG006: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "SG006"
	Public Function insPostSG006(ByVal lstrCodispLog As String, ByVal lintG_identi As Integer, ByVal lintUsercode As Integer) As Boolean
		insPostSG006 = True
		
		On Error GoTo insPostSG006_Err
		
		sCodispl = lstrCodispLog
		nG_identi = lintG_identi
		nUsercode = lintUsercode
		
		insPostSG006 = Update("Tablas Generales")
		
insPostSG006_Err: 
		If Err.Number Then
			insPostSG006 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insValSGC002_K: This method validates the header section of the page "SGC002_K" as described in the
	'**%functional specifications
	'%InsValSGC002_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "SGC002_K"
	Public Function insValSGC002_K(ByVal sCodisplTrans As String, ByVal nModules As Integer, ByVal sCodispl As String, ByVal sCodisp As String, ByVal sPseudo As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsUser As eSecurity.User
		
		lobjErrors = New eFunctions.Errors
		lclsUser = New eSecurity.User
		
		On Error GoTo insValSGC002_K_Err
		
		insValSGC002_K = CStr(True)
		'**+Validates the field "Module"
		'+Se realizan las validaciones del campo "Módulo".
		
		If nModules <> 0 And nModules <> eRemoteDB.Constants.intNull Then
			If Not lclsUser.InsConstruct("Windows.nModules", CStr(nModules), User.eTypValConst.ConstNumeric) Then
				Call lobjErrors.ErrorMessage(sCodisplTrans, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Módulo) ")
			End If
		End If
		'**+Validates the field "Logical"
		'+Se realizan las validaciones del campo "Lógico"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sCodispl) And Not IsNothing(sCodispl) And Trim(sCodispl) <> String.Empty And Trim(sCodispl) <> "0" Then
			If Not lclsUser.InsConstruct("Windows.sCodispl", sCodispl, User.eTypValConst.ConstString) Then
				Call lobjErrors.ErrorMessage(sCodisplTrans, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Lógico) ")
			End If
		End If
		'**+Validates the field "Physical"
		'+Se realizan las validaciones del campo "Físico"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sCodisp) And Not IsNothing(sCodisp) And Trim(sCodisp) <> String.Empty And Trim(sCodisp) <> "0" Then
			If Not lclsUser.InsConstruct("Windows.sCodisp", sCodisp, User.eTypValConst.ConstString) Then
				Call lobjErrors.ErrorMessage(sCodisplTrans, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Físico) ")
			End If
		End If
		
		'**+Validates the field "Alias"
		'+Se realizan las validaciones del campo "Pseudónimo"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sPseudo) And Not IsNothing(sPseudo) And Trim(sPseudo) <> String.Empty And Trim(sPseudo) <> "0" Then
			If Not lclsUser.InsConstruct("Windows.sPseudo", sPseudo, User.eTypValConst.ConstString) Then
				Call lobjErrors.ErrorMessage(sCodisplTrans, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Pseudónimo) ")
			End If
		End If
		
		insValSGC002_K = lobjErrors.Confirm
		
insValSGC002_K_Err: 
		If Err.Number Then
			insValSGC002_K = "insValSGC002_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsUser may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUser = Nothing
		On Error GoTo 0
	End Function
End Class






