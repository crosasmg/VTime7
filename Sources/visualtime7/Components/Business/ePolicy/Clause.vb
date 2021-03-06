Option Strict Off
Option Explicit On
Option Compare Text
Public Class Clause
	'%-------------------------------------------------------%'
	'% $Workfile:: Clause.cls                               $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 22/06/04 1:11p                               $%'
	'% $Revision:: 41                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla clause al 09-25-2003 15:40:48
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nClause As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	'Public dCompdate            As Date       ' DATE       7    0     0    S
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nNotenum As Integer ' NUMBER     22   0     10   S
	'Public dNulldate            As Date       ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public sClient As String ' CHAR       14   0     0    S
	Public nGroup_insu As Integer ' NUMBER     22   0     5    S
	Public nModulec As Integer ' NUMBER     22   0     5    S
	Public nCover As Integer ' NUMBER     22   0     5    S
	Public nCause As Integer ' NUMBER     22   0     5    S
	Public sAgree As String ' CHAR       1    0     0    N
	Public sType_clause As String
	Public sDoc_attach As String
	
	'- Variables auxiliares
	Public nNotenumS As Integer
	Public nTransaction As Integer
	'-Indicador de si clausula aparece seleccionada por omision
	Public sDefaulti As String
	Public nNoteNumP As Integer
	
	'- Descripcion de la cobertura
	Public sDesc_cover As String
	
	'-Indicador si se puede actualizar el texto de la nota
	Public sModified As String
	
	'-Variables que almacenan descripciones asociadas al registro de clausula
	Public sDescript As String
	Public sCliename As String
	Public sModulecDesc As String
	Public sCoverDesc As String
	
	'-Fecha de t?rmino para modificaciones temporales de p?liza/certificado
	Public dNulldate As Date
	
	'-Consecutivo para evitar error de duplicidad
	Public nSeq As Integer
	
	'-Variable usadas para validar los requisitos y exclusiones
	Private mstrSel As String
	Private mstrClauses As String
	
	Private Enum eTypeRelation
		Relacion = 1
		Exclusion = 2
		Alguno = 3
	End Enum
	
	'- C?digo de error generado durante carga inicial de transaccion
	Private mlngPuntualErr As Integer
	
	'-Indicadores de existencia de algun registro y de este registro en particular
	Private mblnDataFound As Boolean
	Private mblnInTable As Boolean
	'
	'%bDataExists: Permite indicar que existia algun registro en clause
	'%             Esto permite saber que usuario ingreso datos previamente
	'%             Con esta informacion se puede determinar si marcar registro por omision
	'%             Se deja de tipo friend para que solo sea modificado
	'%             desde dentro del proyecto
	'%-------------------------------------------------------------------------
	Friend WriteOnly Property bDataFound() As Boolean
		Set(ByVal Value As Boolean)
			
			mblnDataFound = Value
			
		End Set
	End Property
	
	'%bInTable: Permite indicar que este registro en particular existe en Clause
	'%          Con esta informacion se puede determinar si marcar registro por omision
	'%          Se deja de tipo friend para que solo sea modificado
	'%          desde dentro del proyecto
	
	'%bInTable: Permite indicar que este registro en particular existe en Clause
	'%          Con esta informacion se puede determinar si marcar registro por omision
	Public Property bInTable() As Boolean
		Get
			
			bInTable = mblnInTable
			
		End Get
		Set(ByVal Value As Boolean)
			
			mblnInTable = Value
			
		End Set
	End Property
	'%sClausePreError: Retorna mensaje con errores de incializaci?n de transaccion
	Public ReadOnly Property sClausePreError() As String
		Get
			'-Variables con mensaje y c?digo de mensaje anterior para evitar acceder dos veces a la BD
			Static llngPrevErr As Integer
			Static lstrPrevMsg As String
			Dim lclsError As eFunctions.Errors
			
			If mlngPuntualErr <> 0 Then
				'+Si cambio el mensaje respecto del anterior se busca nueva descripcion
				If mlngPuntualErr <> llngPrevErr Then
					llngPrevErr = mlngPuntualErr
					
					lclsError = New eFunctions.Errors
					lstrPrevMsg = lclsError.ErrorMessage("CA022", mlngPuntualErr,  ,  ,  , True)
					'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsError = Nothing
				End If
				sClausePreError = lstrPrevMsg
			Else
				sClausePreError = ""
			End If
		End Get
	End Property
	
	'%nSel: Indica si registro se muestra seleccionado por omision
	Public ReadOnly Property nSel() As Integer
		Get
			
			'+Registro queda seleccionado por omision si estaba en la tabla Clause
			'+o si no estaba y NO existian datos en Clause
			'+Si habian datos en Clause y ?ste registro no estaba,
			'+significa que fue descartado por el usuario
			If mblnInTable Or (sDefaulti = "1" And Not mblnDataFound) Then
				nSel = 1
			Else
				nSel = 2
			End If
			
		End Get
	End Property
	
	'%InsUpdClause: Realiza la actualizaci?n de la tabla
	Private Function InsUpdClause(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdClause As eRemoteDB.Execute
		
		On Error GoTo InsUpdClause_Err
		
		lrecInsUpdClause = New eRemoteDB.Execute
		
		'+ Definici?n de par?metros del procedimiento InsUpdClause al 24-05-2002
		With lrecInsUpdClause
			.StoredProcedure = "InsUpdClause"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAgree", sAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_clause", sType_clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDoc_attach", sDoc_attach, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 45, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdClause = .Run(False)
		End With
		
InsUpdClause_Err: 
		If Err.Number Then
			InsUpdClause = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsUpdClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdClause = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdClause(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdClause(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdClause(3)
	End Function
	
	'%InsValCA022: Validaciones masivas de la transacci?n CA022
	Public Function InsValCA022(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sTransaction As String, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy_Win As ePolicy.Policy_Win
		Dim lclsClauses As ePolicy.Clauses
		
		On Error GoTo InsValCA022_Err
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy_Win = New ePolicy.Policy_Win
		lclsClauses = New ePolicy.Clauses
		
		'+Debe existir al menos una cl?usula seleccionada si exist?an cl?usulas propuestas, y si en el producto est? definida
		'+como "Requerida"
		If lclsPolicy_Win.Find_Sequen_Pol(sTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "1", sCodispl) Then
			If lclsPolicy_Win.sRequire = "1" Then
				If Not lclsClauses.valExist_Clause_All(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3877)
				Else
					'+ Se deja ventana con contenido
					Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "2")
				End If
			Else
				If lclsClauses.valExist_Clause_All(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
					'+ Se deja ventana con contenido
					Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "2")
				Else
					'+ Se deja ventana sin contenido
					Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "1")
				End If
			End If
		End If
		
		InsValCA022 = lclsErrors.Confirm
		
InsValCA022_Err: 
		If Err.Number Then
			InsValCA022 = "InsValCA022: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lclsClauses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClauses = Nothing
	End Function
	
	'%InsValCA022Upd: Validaciones de la transacci?n CA022 - PopUp
    Public Function InsValCA022Upd(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nClause As Integer, ByVal sClient As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sSelClause As String, ByVal sTransaction As String, ByVal sAction As String, ByVal sType_clause As String, ByVal sDoc_attach As String, ByVal nCause As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsReqExc As ePolicy.Tab_reqexc
        Dim lclsClauses As ePolicy.Clauses

        On Error GoTo InsValCA022Upd_Err

        lclsErrors = New eFunctions.Errors
        lclsPolicy_Win = New ePolicy.Policy_Win
        lclsClauses = New ePolicy.Clauses

        If nClause = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11090)
        End If
        If lclsClauses.valExist_Clause(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nClause, sClient, nModulec, nCover, nCause) And sAction = "Add" Then
            Call lclsErrors.ErrorMessage(sCodispl, 55970)
        End If
        If nCause <= 0 And nClause = 1 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10242)
        End If

        '+Debe existir al menos una cl?usula seleccionada si exist?an cl?usulas propuestas, y si en el producto est? definida
        '+como "Requerida"
        If lclsPolicy_Win.Find_Sequen_Pol(sTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "1", sCodispl) Then
            If lclsPolicy_Win.sRequire = "1" Then
                If Not lclsClauses.valExist_Clause_All(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 3877)
                End If
            End If
        End If

        '+Validaci?n de requisitos exclusiones
        lclsReqExc = New ePolicy.Tab_reqexc
        Call lclsReqExc.InsValTab_Reqexc(sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eProduct.Tab_reqexc.ReqexclType.cstrClause, CStr(nClause), sSelClause, lclsErrors)

        '+Validacion para el tipo de cl?usula seg?n archivo
        If sType_clause <> String.Empty And sType_clause = "1" Then
            If sDoc_attach = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 800040)
            End If
        End If

        InsValCA022Upd = lclsErrors.Confirm

InsValCA022Upd_Err:
        If Err.Number Then
            InsValCA022Upd = "InsValCA022Upd: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        'UPGRADE_NOTE: Object lclsClauses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClauses = Nothing
        'UPGRADE_NOTE: Object lclsReqExc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsReqExc = Nothing
    End Function
	
	'%InsPostCA022Upd: Actualizaci?n puntual de transacci?n CA022 - Popup
	Public Function InsPostCA022Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nId As Integer, ByVal nClause As Integer, ByVal sClient As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCause As String, ByVal sAgree As String, ByVal nGroup_insu As Integer, ByVal nNotenum As Integer, ByVal nNotenumS As Integer, ByVal nUsercode As Integer, Optional ByVal nTransaction As Integer = 0, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal sType_clause As String = "", Optional ByVal sDoc_attach As String = "") As Boolean
		On Error GoTo insPostCA022Upd_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nClause = nClause
			.dEffecdate = dEffecdate
			.nId = nId
			.nNotenum = nNotenum
			.nNotenumS = nNotenumS
			.sClient = sClient
			.nGroup_insu = nGroup_insu
			.nModulec = nModulec
			.nCover = nCover
			.nCause = CInt(nCause)
			.sAgree = sAgree
			.nUsercode = nUsercode
			.nTransaction = nTransaction
			.dNulldate = dNulldate
			.sType_clause = IIf(sType_clause = "1", "1", "2")
			.sDoc_attach = IIf(sType_clause = "1", sDoc_attach, String.Empty)
			
			Select Case sAction
				Case "Add"
					InsPostCA022Upd = .Add
				Case "Update"
					InsPostCA022Upd = .Update
				Case "Del"
					InsPostCA022Upd = .Delete
			End Select
		End With
		
insPostCA022Upd_Err: 
		If Err.Number Then
			InsPostCA022Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsPostCA022: Actualizacion masiva de la transacci?n CA022
	Public Function InsPostCA022(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nTransaction As Integer = 0, Optional ByVal dNulldate As Date = #12:00:00 AM#) As Boolean
		' por ahora este metodo no se esta usando
		On Error GoTo insPostCA022_Err
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			.nTransaction = nTransaction
		End With
		
insPostCA022_Err: 
		If Err.Number Then
			InsPostCA022 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insPreCA022: Load the preliminaries datas of the CA022
	'%insPreCA022 : Se cargan los datos preliminares de la CA022
	'%              y se ejecutan las validaciones de entrada a la ventana
	Public Function insPreCA022(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal bQuery As Boolean) As ePolicy.Clauses
		'-Objetos para el manejo de la p?liza
		Dim lclsPolicy As ePolicy.Policy
		
		'-Objeto para el manejo de los certificados.
		Dim lclsCertificat As ePolicy.Certificat

        '-Objeto que se asigna a las clausulas a desplegar.
        '-Se instancia con datos de clause y tab_clause o claus_co_p o claus_co_g
        '-seg?n metodo usado
        Dim lcolClause As ePolicy.Clauses = New ePolicy.Clauses

        '-Datos de la p?liza para liberar objeto
        Dim lstrPolitype As String
		Dim lstrTyp_clause As String
		
		'-Indicador que se obtuvieron datos de la clausula
		Dim lblnDataLoaded As Boolean
		
		On Error GoTo insPreCA022_Err
		
		'+Se inicializa la variable de m?dulo que contendr? el mensaje de error puntual
		mlngPuntualErr = 0
		
		'+Obtiene los datos de la p?liza necesarios para el proceso.
		lclsPolicy = New ePolicy.Policy
		Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
		lstrPolitype = lclsPolicy.sPolitype
		lstrTyp_clause = lclsPolicy.sTyp_Clause
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		
		'+ Si NO se trata de una p?liza individual o
		'+ es una certificado de un colectivo/multilocalidad
		'+ la ventana no debe mostrarse en la secuencia
		If Not ((lstrPolitype = "1" And nCertif = 0) Or (lstrPolitype <> "1" And nCertif <> 0)) Then
			mlngPuntualErr = 1402
		Else
			'+ P?liza individual
			If (lstrPolitype = "1" And nCertif = 0) Then
				lcolClause = New ePolicy.Clauses
				lblnDataLoaded = lcolClause.FindProduct(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, bQuery)
			Else
				
				'+ P?liza Colectiva/Multilocalidad no matriz
				Select Case lstrTyp_clause
					'**+ When the policy don't work whit clauses
					'+ Cuando la p?liza no trabaja con cl?usulas.
					Case "1"
						mlngPuntualErr = 3884
						
						'**+ When the clauses are for policy
						'+ Cuando las cl?usulas son por p?liza.
					Case "2"
						lcolClause = New ePolicy.Clauses
						lblnDataLoaded = lcolClause.FindPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, bQuery)
						'**+ When the clauses are for group.
						'+ Cuando las cl?usulas son por grupo.
					Case "3"
						
						'+ Obtiene grupo del certificado.
						lclsCertificat = New ePolicy.Certificat
						Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
						
						'+ Se cargan los grupos cuando las cl?usulas son por grupo.
						If (lclsCertificat.nGroup = eRemoteDB.Constants.intNull) Then
							mlngPuntualErr = 3889
						Else
							lcolClause = New ePolicy.Clauses
							lblnDataLoaded = lcolClause.FindGroup(sCertype, nBranch, nProduct, nPolicy, lclsCertificat.nGroup, dEffecdate, nCertif, bQuery)
						End If
						'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCertificat = Nothing
						
						'**+ When the clauses are for registered.
						'+ Cuando las cl?usulas son por certificado.
					Case "4"
						lcolClause = New ePolicy.Clauses
						lblnDataLoaded = lcolClause.FindProduct(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, bQuery)
					Case Else
						'**+ In case that the field has "null" value
						'+ En caso de que el campo tenga valor "null" no se ha ingresado datos en la ventana
						'+ de "informaci?n de colectivos" de la p?liza matriz.
						If (lstrPolitype <> "1" And nCertif <> 0) Then
							
							mlngPuntualErr = 3885
						End If
				End Select
			End If
			
			'+ Si no se encontraron registros asociados para la poliza
			'+ significa que al producto no se le asignaron clausulas
			If Not lblnDataLoaded And mlngPuntualErr = 0 Then
				mlngPuntualErr = 11347
			End If
		End If
		
insPreCA022_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object lcolClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolClause = Nothing
		End If
		On Error GoTo 0
		'+Se retorna objeto cargado con datos
		insPreCA022 = lcolClause
		'UPGRADE_NOTE: Object lcolClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolClause = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
	End Function
	
	'% insExist: Verifica la existencia de un registro en la tabla usando la clave
	Public Function insExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nClause As Integer, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nGroup_insu As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		'- Objeto de conecci?n a base de datos
		Dim lclsClause As eRemoteDB.Execute
		
		On Error GoTo insExist_Err
		lclsClause = New eRemoteDB.Execute
		
		'+ Definici?n de par?metros del procedimiento ReaClause_V al 24-05-2002
		With lclsClause
			.StoredProcedure = "ReaClause_V"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insExist = .Parameters("nExist").Value = 1
			End If
		End With
		
insExist_Err: 
		If Err.Number Then
			insExist = False
		End If
		'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClause = Nothing
		On Error GoTo 0
	End Function
	
	
	'% insExists: Retorna si hay datos para la poliza/certificado en clause
	Public Function insExistsPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		'- Objeto de conecci?n a base de datos
		Dim lclsClause As eRemoteDB.Execute
		
		On Error GoTo insExistsPolicy_Err
		lclsClause = New eRemoteDB.Execute
		
		'+ Definici?n de par?metros del procedimiento reaClause_count al 24-05-2002
		With lclsClause
			.StoredProcedure = "reaClause_count"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insExistsPolicy = .Parameters("nExist").Value = 1
			End If
		End With
		
insExistsPolicy_Err: 
		If Err.Number Then
			insExistsPolicy = False
		End If
		'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClause = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nClause = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nId = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nTransaction = eRemoteDB.Constants.intNull
		sClient = String.Empty
		nGroup_insu = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nCause = eRemoteDB.Constants.intNull
		sAgree = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






