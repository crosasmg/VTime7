Option Strict Off
Option Explicit On
Public Class WinMessag
	'%-------------------------------------------------------%'
	'% $Workfile:: WinMessag.cls                            $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 15/09/03 17:54                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Public sCodispl As String 'char       no       1                    yes                                 yes                                 yes
	Public nErrorNum As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)
	Public sAction_err As String 'char       no       1                    yes                                 yes                                 yes
	Public dCompdate As Date
	Public sErrorTyp As String 'char       no       1                    yes                                 yes                                 yes
	Public nLevel As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	Public sStatregt As String 'char       no       1                    yes                                 yes                                 yes
	Public nUsercode As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	
	Private mvarWinMessags As WinMessags
	
	
	Public Property WinMessags() As WinMessags
		Get
			If mvarWinMessags Is Nothing Then
				mvarWinMessags = New WinMessags
			End If
			
			
			WinMessags = mvarWinMessags
		End Get
		Set(ByVal Value As WinMessags)
			mvarWinMessags = Value
		End Set
	End Property
    Private Sub Class_Terminate_Renamed()
        mvarWinMessags = Nothing
    End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Add: This routine creates records on the win_messag table.
	'%Add: Esta rutina crea los registros en la tabla win_messag.
	Public Function Add() As Boolean
		
		'**- Variable definition for the treatment of the parameters and the run of the SP.
		'-Se definbe la variable para el tratamiento de los parámetros y la corrida del SP
		
		Dim lrecWinMessage As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecWinMessage = New eRemoteDB.Execute
		
		With lrecWinMessage
			.StoredProcedure = "creWinMessag"
			
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction_Err", sAction_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrortyp", sErrorTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
        lrecWinMessage = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	'**% Update: This routine updates records in the win_messag table.
	'%Update: Esta rutina Actualiza los registros en la tabla win_messag.
	Public Function Update() As Boolean
		
		'**- Variable definition for the treatment of the parameters and the run of the SP.
		'-Se definbe la variable para el tratamiento de los parámetros y la corrida del SP
		
		Dim lrecWinMessage As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		lrecWinMessage = New eRemoteDB.Execute
		
		With lrecWinMessage
			.StoredProcedure = "updWinMessag"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction_Err", sAction_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrortyp", sErrorTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
        lrecWinMessage = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Delete: this routine deletes records in the table
	'%Delete: Esta rutina elimina los registros de la Tabla
	Public Function Delete() As Boolean
		'**- Variable definition for the execution of the Sp and the parameters.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		
		Dim ltempDelWinMessag As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		ltempDelWinMessag = New eRemoteDB.Execute
		
		'**- Variable definition to handle fields.
		'-Se define la variable para el tratamiento de los campos
		
		Delete = True
		With ltempDelWinMessag
			.StoredProcedure = "delWinMessag"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
        ltempDelWinMessag = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Find: Routine to find records in the win_messag table.
	'%Find: Rutina para Localizar el Registro en la Tabla win_messag
	Public Function Find(ByVal sCodispl As String, ByVal nErrorNum As Integer) As Boolean
		
		'**- Variable definition in charge of the execution of the SP and the parameters to send.
		'-Se define la variable encargada de la ejecución de los SP y de las parámetros a enviarle
		
		Dim lrecMessagewin As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		lrecMessagewin = New eRemoteDB.Execute
		
		With lrecMessagewin
			.StoredProcedure = "reaWin_Messag"
			
			.Parameters.Add("scodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nerrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
        lrecMessagewin = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMS002_K: Validates the error number.
	'% insValMS002_K: Valida el numero de Error
	Public Function insValMS002_K(ByVal sCodispl As String, ByVal sCodisp As String) As String
		
		Dim lobjSecurity As Object 'eSecurity.Windows
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS002_K_err
		
		lobjSecurity = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Windows")
		lclsErrors = New eFunctions.Errors
		
		sCodisp = Trim(sCodisp)
		
		If sCodisp = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10825)
		Else
			If Not lobjSecurity.reaWindows(sCodisp) Then
				Call lclsErrors.ErrorMessage(sCodispl, 99005)
			End If
			
		End If
		
		insValMS002_K = lclsErrors.Confirm
		
        lobjSecurity = Nothing
        lclsErrors = Nothing
		
insValMS002_K_err: 
		If Err.Number Then
			insValMS002_K = insValMS002_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMS002: Validates the Error messages window of a window.
	'% insValMS002: Valida la Ventana de Mesanjes de Error de una Ventana
	Public Function insValMS002(ByVal sCodispl As String, ByVal sCodisp As String, ByVal sAction As String, ByVal nErrorNum As Integer, ByVal nErrorTyp As Integer, ByVal nStatregt As Integer) As String
		
		Dim lclsmessage As eGeneral.Message
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS002_err
		
		lclsmessage = New eGeneral.Message
		lclsErrors = New eFunctions.Errors
		
        If nErrorNum = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 10043)
        Else
            If sAction <> "Update" Then
                If Not lclsmessage.Find(nErrorNum) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1082)
                Else
                    If Find(sCodisp, nErrorNum) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 10004)
                    End If
                End If
            End If
        End If
		
        If nErrorTyp = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 10091)
        End If

        If nStatregt = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 13423)
        End If

        insValMS002 = lclsErrors.Confirm
		
		
        lclsmessage = Nothing
        lclsErrors = Nothing
		
insValMS002_err: 
		If Err.Number Then
			insValMS002 = insValMS002 & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMS002: updates the Error message window of a window
	'% insPostMS002: Actualiza la Ventana de Mesanjes de Error de una Ventana
	Public Function insPostMS002(ByVal sCodispl As String, ByVal sCodisp As String, ByVal sAction As String, ByVal nErrorNum As Integer, ByVal sErrorTyp As String, ByVal sStatregt As String, ByVal nLevel As Integer, ByVal sAction_err As String, ByVal nUsercode As Integer) As Boolean
		
		Dim lsclValues_cache As eFunctions.Values
		
		On Error GoTo insPostMS002_err
		
		With Me
			.sCodispl = sCodisp
			.nErrorNum = nErrorNum
			.sErrorTyp = sErrorTyp
			.sStatregt = sStatregt
			.nLevel = nLevel
			.sAction_err = sAction_err
			.nUsercode = nUsercode
		End With
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS002 = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS002 = Update
				
				'**+ If the selected option is Delete.
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS002 = Delete
				
		End Select
		
		If insPostMS002 Then
			lsclValues_cache = New eFunctions.Values
			Call lsclValues_cache.DelCache(3, sCodisp)
		End If
		
insPostMS002_err: 
		If Err.Number Then
			insPostMS002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lsclValues_cache may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lsclValues_cache = Nothing
	End Function
	
	'% Find_Windowsdesc: Devuelve una cadena con las descripciones de los codispl indicados
	Public Function Find_Windowsdesc(ByVal sCodispl As String) As String
		Dim lrecFind_Windowsdesc As eRemoteDB.Execute
		'+Definición de parámetros para stored procedure 'InsSi008pkg.Find_WindowsdescUpd'
		'+Información leída el 24/04/2003
		On Error GoTo Find_Windowsdesc_Err
		lrecFind_Windowsdesc = New eRemoteDB.Execute
		With lrecFind_Windowsdesc
			.StoredProcedure = "reawindows_desc"
			.Parameters.Add("ArraysCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 400, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arraydescript", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_Windowsdesc = .Parameters("Arraydescript").Value
		End With
Find_Windowsdesc_Err: 
		If Err.Number Then
		End If
        lrecFind_Windowsdesc = Nothing
		On Error GoTo 0
	End Function
End Class






