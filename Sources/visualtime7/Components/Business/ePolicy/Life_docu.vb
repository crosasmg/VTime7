Option Strict Off
Option Explicit On
Public Class Life_docu
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_docu.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 11/11/04 11:26a                              $%'
	'% $Revision:: 60                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla life_docu al 20-08-2002
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCrthecni As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public dRecep_date As Date ' DATE       7    0     0    S
	Public nRole As Integer ' NUMBER     22   0     5    N
	Public nStat_docReq As Integer ' NUMBER     22   0     5    S
	Public sClient As String ' CHAR       14   0     0    N
	Public dDate_to As Date ' DATE       7    0     0    N
	Public dDatefree As Date ' DATE       7    0     0    S
	Public nEval As Double ' NUMBER     22   0     5    S
	Public dDatevig As Date ' DATE       7    0     0    S
	Public nNotenum As Integer ' NUMBER     22   0     10   S
	Private mlngUsercode As Integer ' NUMBER     22   0     5    N
	
	'-Variables auxiliares
	Public nExist As Integer
	Public sKey As String
	Public sDescript As String
	Public nCumul As Double
	Public nStatusdoc As Integer
	Public dDocreq As Date
	Public dDocrec As Date
	Public dExpirdat As Date
	Public nNotenum_cli As Integer
	Public nEval_master As Double
	Public nId As Integer
	Private mlngTransaction As Integer
	Public nErrorNum As Integer
	Public sRequest As String
	Public sDel_docu As String
	Public nStatus_eval As String
	Public nEval_Gen As String
	
	'-Objeto para obtener la colección de documentos solicitados
	Public mcolLife_docus As Life_docus
	
	'%InsValPrevInfo: Valida la existencia previa de información en la ventana de coberturas
	Public Function InsValPrevInfo(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsCover As Cover
		
		On Error GoTo InsValPrevInfo_Err
		lclsCover = New Cover
		InsValPrevInfo = lclsCover.CountCovers(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "CA014", String.Empty)
		
InsValPrevInfo_Err: 
		If Err.Number Then
			InsValPrevInfo = False
		End If
		'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCover = Nothing
		On Error GoTo 0
	End Function
	
	'%InsUpdt_life_docu: Realiza la actualización de la tabla
	Private Function InsUpdt_life_docu(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdt_life_docu As eRemoteDB.Execute
		
		On Error GoTo InsUpdt_life_docu_Err
		lrecInsUpdt_life_docu = New eRemoteDB.Execute
		'+ Definición de store procedure InsUpdt_life_docu al 08-20-2002 18:13:56
		With lrecInsUpdt_life_docu
			.StoredProcedure = "InsUpdt_life_docu"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dRecep_date", dRecep_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_docreq", nStat_docReq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_to", dDate_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDatefree", dDatefree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDatevig", dDatevig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCumul", nCumul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatusdoc", nStatusdoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDocreq", dDocreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDocrec", dDocrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum_cli", nNotenum_cli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEval_master", nEval_master, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequest", sRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdt_life_docu = .Run(False)
		End With
		
InsUpdt_life_docu_Err: 
		If Err.Number Then
			InsUpdt_life_docu = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdt_life_docu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdt_life_docu = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdt_life_docu(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdt_life_docu(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdt_life_docu(3)
	End Function
	
	'%InsPreVI021: Obtiene la información de la transacción de documentos solicitados
	Public Function InsPreVI021(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSessionId As String, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sKey As String) As Boolean
		Dim sExec As String
		Dim lclsPolicy_Win As Policy_Win
		Dim lblnValPrevInfo As Boolean
		
		On Error GoTo InsPrevi021_Err
		If sKey = String.Empty Then
			sExec = "1"
			sKey = "TMP" & nSessionId & nUsercode
			lblnValPrevInfo = InsValPrevInfo(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		Else
			sExec = "2"
			lblnValPrevInfo = True
		End If
		
		If lblnValPrevInfo Then
			mcolLife_docus = New Life_docus
			mcolLife_docus.sDel_docu = Me.sDel_docu
			
			InsPreVI021 = mcolLife_docus.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sKey, nTransaction, sExec)
			
			' trae el estado de  los documentos solicitados
			If mcolLife_docus.Count > 0 Then
				Me.nStatus_eval = mcolLife_docus.Item(1).nStatus_eval
			Else
				' si no encutra lo deja pendiente de evaluacion
				Me.nStatus_eval = CStr(3)
			End If
			Me.sKey = sKey
			If Not InsPreVI021 Then
				nErrorNum = 3956
				lclsPolicy_Win = New Policy_Win
				InsPreVI021 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI021", "4")
			End If
		Else
			InsPreVI021 = False
			nErrorNum = 3955
		End If
		
InsPrevi021_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object mcolLife_docus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mcolLife_docus = Nothing
			InsPreVI021 = False
		End If
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVI021Upd: valida la información de la ventana de documentos solicitados
	Public Function InsValVI021Upd(ByVal sCodispl As String, ByVal nStat_docReq As Integer, ByVal dRecep_date As Date, ByVal dDate_to As Date, ByVal dDatevig As Date, ByVal dDatefree As Date, Optional ByVal sClient As String = "", Optional ByVal nDocument As Integer = 0, Optional ByVal sAction As String = "", Optional ByVal sKey As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo InsValVI021Upd_Err
		lclsErrors = New eFunctions.Errors
		lblnError = True
		With lclsErrors
			If dRecep_date = eRemoteDB.Constants.dtmNull Then
				'+ Si el estado del documento es "Aprobado" o "Recibido" (Table275), la fecha de recepción
				'+ debe estar llena
				If nStat_docReq = 2 Or nStat_docReq = 8 Then
					.ErrorMessage(sCodispl, 4101)
				End If
			End If
			
			If dDatevig <> eRemoteDB.Constants.dtmNull Then
				If dDate_to > dDatevig Then
					.ErrorMessage(sCodispl, 55800)
				End If
				
				If dDatefree > dDatevig Then
					.ErrorMessage(sCodispl, 55801)
				End If
			End If
			
			If sAction = "Add" Then
				If sClient = String.Empty Then
					.ErrorMessage(sCodispl, 2792)
					lblnError = False
				Else
					If nDocument = 0 Or nDocument = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 1956)
						lblnError = False
					End If
				End If
				If lblnError Then
					If insExistst_life_docu(sKey, sClient, nDocument) Then
						.ErrorMessage(sCodispl, 55734)
					End If
				End If
			End If
			
			If sClient <> String.Empty Then
				If nStat_docReq = eRemoteDB.Constants.intNull Or nStat_docReq = 0 Then
					.ErrorMessage(sCodispl, 55699)
					lblnError = False
				End If
			End If
			
			InsValVI021Upd = .Confirm
		End With
		
InsValVI021Upd_Err: 
		If Err.Number Then
			InsValVI021Upd = "InsValVI021Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostVI021Upd: Se realiza la actualización de los datos en la ventana VI021
	Public Function InsPostVI021Upd(ByVal sAction As String, ByVal sKey As String, ByVal sDescript As String, ByVal nCrthecni As Integer, ByVal dRecep_date As Date, ByVal nStat_docReq As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal nUsercode As Integer, ByVal dDate_to As Date, ByVal dDatefree As Date, ByVal nEval As Double, ByVal dDatevig As Date, ByVal nNotenum As Integer, ByVal nCumul As Double, ByVal nStatusdoc As Integer, ByVal dDocreq As Date, ByVal dDocrec As Date, ByVal dExpirdat As Date, ByVal nNotenum_cli As Integer, ByVal nEval_master As Double, ByVal nId As Integer, ByVal nExist As Integer, ByVal sRequest As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0) As Boolean
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo InsPostVI021Upd_Err
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nPolicy = nPolicy
			.nProduct = nProduct
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.dNulldate = dNulldate
			.sKey = sKey
			.dRecep_date = dRecep_date
			.nStat_docReq = nStat_docReq
			.nModulec = nModulec
			.nCover = nCover
			.nRole = nRole
			.sClient = sClient
			mlngUsercode = nUsercode
			.dDate_to = dDate_to
			.dDatefree = dDatefree
			.nEval = nEval
			.dDatevig = dDatevig
			.nNotenum = nNotenum
			.nCumul = nCumul
			.nStatusdoc = nStatusdoc
			.dDocreq = dDocreq
			.dDocrec = dDocrec
			.dExpirdat = dExpirdat
			.nNotenum_cli = nNotenum_cli
			.nEval_master = nEval_master
			.nId = nId
			.nExist = nExist
			
			Select Case sAction
				Case "Add"
					lclsValues = New eFunctions.Values
					.sDescript = lclsValues.getMessage(CShort(sDescript), "Table32")
					.nCrthecni = CShort(sDescript)
					.nModulec = 0
					.nCover = 0
					.sRequest = "2"
					InsPostVI021Upd = .Add
				Case "Update"
					.nCrthecni = nCrthecni
					.sDescript = sDescript
					.sRequest = sRequest
					InsPostVI021Upd = .Update
				Case "Del"
					.nCrthecni = nCrthecni
					.sRequest = "2"
					
					InsPostVI021Upd = .Delete
			End Select
		End With
		
InsPostVI021Upd_Err: 
		If Err.Number Then
			InsPostVI021Upd = False
		End If
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		On Error GoTo 0
	End Function

    '%InsPostVI021: Se realiza la actualización de los datos en la ventana VI021 de la tabla
    '%              temporal a la definitiva
    Public Function InsPostVI021(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sKey As String, ByVal nEval As Integer, ByVal nStatus_eval As Object) As Boolean
        InsPostVI021 = InsUpdVI021(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nUsercode, nTransaction, sKey, nEval, nStatus_eval)
    End Function

    '%InsUpdVI021: Se realiza la actualización de los datos en la ventana VI021 de la tabla
    '%                 temporal a la definitiva
    Private Function InsUpdVI021(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sKey As String, ByVal nEval As Integer, ByVal nStatus_eval As Object) As Boolean
		Dim lrecInsPostVI021 As eRemoteDB.Execute
		On Error GoTo InsUpdVI021_Err
		
		lrecInsPostVI021 = New eRemoteDB.Execute
		'+ Definición de store procedure InsPostVI021 al 08-29-2002 12:30:42
		With lrecInsPostVI021
			.StoredProcedure = "InsPostVI021"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_eval", nStatus_eval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdVI021 = .Run(False)
		End With
		
InsUpdVI021_Err: 
		If Err.Number Then
			InsUpdVI021 = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostVI021 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostVI021 = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValLife_docu: This function validates the documents
	'%insValLife_docu: Función que realiza la validación de los documentos.
	Public Function insValLife_docu(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaLife_docu As eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaLife_docu'
		'**+Data of 01/15/2001 09:53:19 a.m.
		'Definición de parámetros para stored procedure 'insudb.reaLife_docu'
		'Información leída el 15/01/2001 09:53:19 a.m.
		On Error GoTo insValLife_docu_Err
		lrecreaLife_docu = New eRemoteDB.Execute
		With lrecreaLife_docu
			.StoredProcedure = "insValLife_docu"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndicador", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValLife_docu = .Parameters("nIndicador").Value = 0
			End If
		End With
		
insValLife_docu_Err: 
		If Err.Number Then
			insValLife_docu = CBool(Err.Description)
		End If
		'UPGRADE_NOTE: Object lrecreaLife_docu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_docu = Nothing
	End Function
	
	'%InitValues: Inicializa los valores de las variables publicas de la clase
	Private Sub InitValues()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nModulec = eRemoteDB.Constants.intNull
		nCrthecni = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dRecep_date = eRemoteDB.Constants.dtmNull
		nRole = eRemoteDB.Constants.intNull
		nStat_docReq = eRemoteDB.Constants.intNull
		sClient = String.Empty
		dDate_to = eRemoteDB.Constants.dtmNull
		dDatefree = eRemoteDB.Constants.dtmNull
		nEval = eRemoteDB.Constants.intNull
		dDatevig = eRemoteDB.Constants.dtmNull
		nNotenum = eRemoteDB.Constants.intNull
		nEval_master = eRemoteDB.Constants.intNull
		mlngTransaction = eRemoteDB.Constants.intNull
		mlngUsercode = eRemoteDB.Constants.intNull
		nErrorNum = eRemoteDB.Constants.intNull
	End Sub
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Se ejecuta cuando se destruye la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolLife_docus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolLife_docus = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%insExistsPolicy: Verifica la existencia de documentos solicitados de la póliza en tratamiento
	Public Function insExistsPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lreLife_docu As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo insExistsPolicy_Err
		
		lreLife_docu = New eRemoteDB.Execute
		
		With lreLife_docu
			.StoredProcedure = "valExistsLife_docu"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insExistsPolicy = .Parameters("nExists").Value = 1
		End With
		
insExistsPolicy_Err: 
		If Err.Number Then
			insExistsPolicy = False
		End If
		'UPGRADE_NOTE: Object lreLife_docu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreLife_docu = Nothing
	End Function
	
	'%insExistst_life_docu: Verifica la existencia de documentos solicitados en la de la póliza en tratamiento
	Public Function insExistst_life_docu(ByVal sKey As String, ByVal sClient As String, ByVal nCrthecni As Integer) As Boolean
		Dim lreLife_docu As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo insExistst_life_docu_Err
		
		lreLife_docu = New eRemoteDB.Execute
		
		With lreLife_docu
			.StoredProcedure = "reaCount_life_docu"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nCount") > 0 Then
					insExistst_life_docu = True
				End If
			End If
		End With
		
insExistst_life_docu_Err: 
		If Err.Number Then
			insExistst_life_docu = False
		End If
		'UPGRADE_NOTE: Object lreLife_docu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreLife_docu = Nothing
	End Function
	
	'%Insdoc_In_Eval_Master: Verifica la existencia de documentos solicitados en el modulo de clientes
	Public Function Insdoc_In_Eval_Master(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nTypeDoc As Integer) As Boolean
		Dim lrecInsdoc_In_Eval_Master As eRemoteDB.Execute
		
		On Error GoTo Insdoc_In_Eval_Master_Err
		
		lrecInsdoc_In_Eval_Master = New eRemoteDB.Execute
		
		With lrecInsdoc_In_Eval_Master
			.StoredProcedure = "Insdoc_In_Eval_Master"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypedoc", nTypeDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Insdoc_In_Eval_Master = True
				nEval_master = .FieldToClass("nEval")
				nStatusdoc = .FieldToClass("nStatusdoc")
				dDocreq = .FieldToClass("dDocreq")
				dDocrec = .FieldToClass("dDocrec")
				dExpirdat = .FieldToClass("dExpirdat")
				nNotenum_cli = .FieldToClass("nNotenum")
				nId = .FieldToClass("nId")
			End If
		End With
		
Insdoc_In_Eval_Master_Err: 
		If Err.Number Then
			Insdoc_In_Eval_Master = False
		End If
		'UPGRADE_NOTE: Object lrecInsdoc_In_Eval_Master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsdoc_In_Eval_Master = Nothing
	End Function
End Class






