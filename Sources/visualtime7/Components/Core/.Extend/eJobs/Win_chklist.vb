Option Strict Off
Option Explicit On
Public Class Win_chklist
	'%-------------------------------------------------------%'
	'% $Workfile:: Win_chklist.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:19p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla win_chklist al 09-07-2002 18:28:36
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCodispl As String ' VARCHAR2   8    0     0    N
	Public nModules As Integer ' NUMBER     22   0     5    N
	Public sComments As String ' LONG       0    0     0    S
	
	'-Variables de los campos de la tabla Detail_chklist
	Public sObject_type As String ' VARCHAR2   2    0     0    N
	Public sObject_name As String ' VARCHAR2   100  0     0    N
	Public sPath As String
	Public nId As Integer
	Public nSequence As Integer
	Public sAction As String
	Public sDescript As String
	
	Private mstrHeader As String
	Private mstrPagesAddUpd As String
	Private mstrPagesDel As String
	Private mstrProcAdd As String
	Private mstrProcUpd As String
	Private mstrProcDel As String
	Private mstrProcChk As String
	Private mstrTabAdd As String
	Private mstrTabUpd As String
	Private mstrTabDel As String
	Private mstrTabDelChk As String
	Private mstrDataAdd As String
	Private mstrDataUpd As String
	Private mstrDataDel As String
	
	'%InsUpdDetail_chklist: Se encarga de actualizar la tabla Win_chklist
	Private Function InsUpdDetail_chklist(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdDetail_chklist As eRemoteDB.Execute
		
		'+ Definición de store procedure InsUpdDetail_chklist al 09-07-2002 20:32:46
		On Error GoTo InsUpdDetail_chklist_Err
		lrecInsUpdDetail_chklist = New eRemoteDB.Execute
		With lrecInsUpdDetail_chklist
			.StoredProcedure = "insUpddetail_chklist"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModules", nModules, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComments", sComments, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2550, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sObject_type", sObject_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sObject_name", sObject_name, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPath", sPath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdDetail_chklist = .Run(False)
		End With
		
InsUpdDetail_chklist_Err: 
		If Err.Number Then
			InsUpdDetail_chklist = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdDetail_chklist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdDetail_chklist = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdDetail_chklist(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdDetail_chklist(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdDetail_chklist(3)
	End Function
	
	'%InsPostMA6000Upd: Ejecuta el post de la transacción
	Public Function InsPostMA6000Upd(ByVal sAction As String, ByVal sCodispl As String, ByVal nModules As Integer, ByVal sComments As String, ByVal sObject_type As String, ByVal sObject_name As String, ByVal nId As Integer, ByVal sPath As String, ByVal nSequence As Integer, ByVal sActionchk As String) As Boolean
		On Error GoTo InsPostMA6000Upd_Err
		With Me
			.sCodispl = sCodispl
			.nModules = nModules
			.sComments = sComments
			.sObject_type = sObject_type
			.sObject_name = sObject_name
			.nId = nId
			.sPath = sPath
			.nSequence = nSequence
			.sAction = sActionchk
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMA6000Upd = Add
			Case "Update"
				InsPostMA6000Upd = Update
			Case "Del"
				InsPostMA6000Upd = Delete
		End Select
		
InsPostMA6000Upd_Err: 
		If Err.Number Then
			InsPostMA6000Upd = False
		End If
		On Error GoTo 0
	End Function
	
	Private Sub ObjectIsPage(ByVal sObject As String, ByVal sAction As String)
		Select Case sAction
			Case "90", "91", "93"
				mstrPagesAddUpd = mstrPagesAddUpd & "    " & sObject & vbCrLf
			Case CStr(92)
				mstrPagesDel = mstrPagesDel & "    " & sObject & vbCrLf
		End Select
	End Sub
	
	Private Sub ObjectIsProc(ByVal sObject As String, ByVal sAction As String)
		Select Case sAction
			Case "90"
				mstrProcAdd = mstrProcAdd & "    " & sObject & vbCrLf
			Case "91"
				mstrProcUpd = mstrProcUpd & "    " & sObject & vbCrLf
			Case "92"
				mstrProcDel = mstrProcDel & "    " & sObject & vbCrLf
			Case "93"
				mstrProcChk = mstrProcChk & "    " & sObject & vbCrLf
		End Select
	End Sub
	
	Private Sub ObjectIsTable(ByVal sObject As String, ByVal sAction As String)
		Select Case sAction
			Case "90"
				mstrTabAdd = mstrTabAdd & "    " & sObject & vbCrLf
			Case "91"
				mstrTabUpd = mstrTabUpd & "    " & sObject & vbCrLf
			Case "92"
				mstrTabDel = mstrTabDel & "    " & sObject & vbCrLf
		End Select
	End Sub
	
	Private Sub ObjectIsData(ByVal sObject As String, ByVal sAction As String)
		Select Case sAction
			Case "90"
				mstrDataAdd = mstrDataAdd & "    " & sObject & vbCrLf
			Case "91"
				mstrDataUpd = mstrDataUpd & "    " & sObject & vbCrLf
			Case "92"
				mstrDataDel = mstrDataDel & "    " & sObject & vbCrLf
		End Select
	End Sub
	
	'%InsConstructFile: Construye la información a mostrar
	Public Function InsConstructFile(ByVal sCodispl As String, ByVal sFile As String, ByVal sModules As String) As Boolean
		Dim lcolWin_chklists As Win_chklists
		Dim lclsWin_chklist As Win_chklist
		
		On Error GoTo InsConstructFile_Err
		lcolWin_chklists = New Win_chklists
		If lcolWin_chklists.Find(sCodispl) Then
			mstrHeader = "Se ha finalizado la corrección de la Transacción " & sCodispl & vbCrLf & vbCrLf & "    Módulo: " & sModules & vbCrLf & "    Inicio: yyyy/MM/dd" & vbCrLf & "    Fin   : yyyy/MM/dd" & vbCrLf & vbCrLf & "Errores corregidos" & vbCrLf & vbCrLf & "Dlls modificados" & vbCrLf & vbCrLf
			For	Each lclsWin_chklist In lcolWin_chklists
				Select Case lclsWin_chklist.sObject_type
					'+Page
					Case CStr(1)
						Call ObjectIsPage(lclsWin_chklist.sPath & "\" & lclsWin_chklist.sObject_name, lclsWin_chklist.sAction)
						
						'+Package, Package body, Procedure, Function, View, Trigger
					Case CStr(2), CStr(3), CStr(4), CStr(5), CStr(6), CStr(7)
						Call ObjectIsProc(lclsWin_chklist.sDescript & " " & lclsWin_chklist.sObject_name, lclsWin_chklist.sAction)
						
						'+Table
					Case CStr(8)
						Call ObjectIsTable(lclsWin_chklist.sObject_name, lclsWin_chklist.sAction)
						
						'+Datos
					Case CStr(9)
						Call ObjectIsData(lclsWin_chklist.sObject_name, lclsWin_chklist.sAction)
				End Select
			Next lclsWin_chklist
			Call InsGenerateFlatFile(sFile)
		End If
		'UPGRADE_NOTE: Object lcolWin_chklists may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolWin_chklists = Nothing
		'UPGRADE_NOTE: Object lclsWin_chklist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsWin_chklist = Nothing
		InsConstructFile = True
		
InsConstructFile_Err: 
		If Err.Number Then
			InsConstructFile = False
		End If
	End Function
	
    Private Sub InsGenerateFlatFile(ByVal sFile As String)
        Dim ConfigSettings As New eRemoteDB.VisualTimeConfig
        Dim lobjfso As Object
        Dim lobjf As Object


        sFile = ConfigSettings.LoadSetting("LogPath") & "\" & sFile
        lobjfso = CreateObject("Scripting.FileSystemObject")
        lobjf = lobjfso.OpenTextFile(sFile, 2, True)
        lobjf.Write(mstrHeader)
        lobjf.Write("Páginas creadas/modificadas" & vbCrLf)
        lobjf.Write(mstrPagesAddUpd & vbCrLf)
        lobjf.Write("Páginas eliminadas" & vbCrLf)
        lobjf.Write(mstrPagesDel & vbCrLf)
        lobjf.Write("Programas creados" & vbCrLf)
        lobjf.Write(mstrProcAdd & vbCrLf)
        lobjf.Write("Programas modificados" & vbCrLf)
        lobjf.Write(mstrProcUpd & vbCrLf)
        lobjf.Write("Programas eliminados" & vbCrLf)
        lobjf.Write(mstrProcDel & vbCrLf)
        lobjf.Write("Programas revisados" & vbCrLf)
        lobjf.Write(mstrProcChk & vbCrLf)
        lobjf.Write("Tablas creadas" & vbCrLf)
        lobjf.Write(mstrTabAdd & vbCrLf)
        lobjf.Write("Tablas modificadas" & vbCrLf)
        lobjf.Write(mstrTabUpd & vbCrLf)
        lobjf.Write("Tablas eliminadas" & vbCrLf)
        lobjf.Write(mstrTabDel & vbCrLf)
        lobjf.Write("Datos creados" & vbCrLf)
        lobjf.Write(mstrDataAdd & vbCrLf)
        lobjf.Write("Datos modificados" & vbCrLf)
        lobjf.Write(mstrDataUpd & vbCrLf)
        lobjf.Write("Datos eliminados" & vbCrLf)
        lobjf.Write(mstrDataDel & vbCrLf)
    End Sub
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCodispl = String.Empty
		nModules = eRemoteDB.Constants.intNull
		sComments = String.Empty
		sObject_type = String.Empty
		sObject_name = String.Empty
		sPath = String.Empty
		nId = eRemoteDB.Constants.intNull
		nSequence = eRemoteDB.Constants.intNull
		sAction = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






