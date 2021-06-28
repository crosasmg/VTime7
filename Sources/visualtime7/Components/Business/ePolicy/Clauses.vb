Option Strict Off
Option Explicit On
Public Class Clauses
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Clauses.cls                              $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 22/06/04 1:12p                               $%'
	'% $Revision:: 23                                       $%'
	'%-------------------------------------------------------%'
	
	'-Variable local de la coleccion
	Private mCol As Collection
	
	'-Permite indicar que existian registros en clause
	'-cuando se busca por grupo, poliza o producto
	Private mblnDataExists As Boolean
	
	'% Add: Añade una nueva instancia de la clase Clause a la colección
	Public Function Add(ByRef objClass As Clause) As Clause
		
		If objClass Is Nothing Then
			objClass = New Clause
		End If
		
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nClause & .nId & .dEffecdate.ToString("yyyyMMdd") & .nSeq)
			
		End With
		Add = objClass
	End Function
	
	'% Find: Obtiene las cláusulas asociadas a la póliza/certificado
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		
		Dim lrecreaClause As eRemoteDB.Execute
		Dim lclsClause As Clause
		
		
		On Error GoTo Find_Err
		
		'+Se inicializa la coleccion por si ya fue cargada en otro metodo
		mCol = New Collection
		
		lclsPolicy_Win = New Policy_Win
		
		lrecreaClause = New eRemoteDB.Execute
		'+ Definición de los parámetros del procedimiento reaClause_a al 24-05-2002
		With lrecreaClause
			.StoredProcedure = "ReaClause_A"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sParamcrea", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					
					lclsClause = New Clause
					lclsClause.sCertype = .FieldToClass("sCertype")
					lclsClause.nBranch = .FieldToClass("nBranch")
					lclsClause.nProduct = .FieldToClass("nProduct")
					lclsClause.nPolicy = .FieldToClass("nPolicy")
					lclsClause.nCertif = .FieldToClass("nCertif")
					lclsClause.nClause = .FieldToClass("nClause")
					lclsClause.dEffecdate = .FieldToClass("dEffecdate")
					lclsClause.nId = .FieldToClass("nId")
					lclsClause.nNotenum = .FieldToClass("nNotenum")
					lclsClause.sClient = .FieldToClass("sClient")
					lclsClause.nGroup_insu = .FieldToClass("nGroup_insu")
					lclsClause.nModulec = .FieldToClass("nModulec")
					lclsClause.nCover = .FieldToClass("nCover")
					lclsClause.sDescript = .FieldToClass("sDescript")
					lclsClause.nCause = .FieldToClass("nCause")
					lclsClause.sAgree = .FieldToClass("sAgree")
					lclsClause.sDesc_cover = .FieldToClass("sDesc_cover")
					lclsClause.sModulecDesc = .FieldToClass("sDescModul")
					lclsClause.sType_clause = .FieldToClass("sType_clause")
					lclsClause.sDoc_attach = .FieldToClass("sDoc_attach")
                    lclsClause.sModified = .FieldToClass("sModified")
                    'TODO: Antes usaba una propiedad de nombre NumberOfRecords
                    lclsClause.nSeq = .RecordCount
					Call Add(lclsClause)
					'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClause = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA022", "1")
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClause = Nothing
		On Error GoTo 0
	End Function
	
	'**% valGroupExist_a: This function validates if there are groups associated to a policy
	'% valGroupExist_a: Valida si existen grupos asociados a una póliza
    Public Function valExist_Clause(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nClause As Integer, ByVal sClient As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCause As Integer) As Boolean
        Dim lrecreaClause As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valExist_Clause_Err

        lrecreaClause = New eRemoteDB.Execute

        With lrecreaClause
            .StoredProcedure = "valExist_Clause"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            If .Parameters("nExists").Value = 1 Then
                valExist_Clause = True
            End If
        End With

valExist_Clause_Err:
        If Err.Number Then
            valExist_Clause = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecreaClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaClause = Nothing
    End Function
	
	'**% valGroupExist_a: This function validates if there are groups associated to a policy
	'% valGroupExist_a: Valida si existen grupos asociados a una póliza
	Public Function valExist_Clause_All(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaClause As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExist_Clause_All_Err
		
		lrecreaClause = New eRemoteDB.Execute
		
		With lrecreaClause
			.StoredProcedure = "valExist_Clause_All"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valExist_Clause_All = True
			End If
		End With
		
valExist_Clause_All_Err: 
		If Err.Number Then
			valExist_Clause_All = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClause = Nothing
	End Function
	
	
	
	'% Load: Devuelve las cláusulas de una poliza definidas por producto
	'--------------------------------------------------------
	Public Function FindProduct(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal bQuery As Boolean) As Boolean
		'--------------------------------------------------------
		'- Se define la variable lrecreatab_clause_clause_a
		Dim lrecreatab_clause_clause_a As eRemoteDB.Execute
		Dim lclsClause As ePolicy.Clause
		
		On Error GoTo Load_Err
		
		'+Se inicializa la coleccion por si ya fue cargada en otro metodo
		mCol = New Collection
		
		lrecreatab_clause_clause_a = New eRemoteDB.Execute
		
		With lrecreatab_clause_clause_a
			.StoredProcedure = "reatab_clause_clause_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuery", IIf(bQuery, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindProduct = Not .EOF
				Do While Not .EOF
					lclsClause = New ePolicy.Clause
					lclsClause.sCertype = sCertype
					lclsClause.nBranch = nBranch
					lclsClause.nProduct = nProduct
					lclsClause.nPolicy = nPolicy
					lclsClause.nCertif = nCertif
					lclsClause.nClause = .FieldToClass("nClauseP")
					lclsClause.sDescript = .FieldToClass("sDescript")
					lclsClause.bInTable = .FieldToClass("nClauseS") <> eRemoteDB.Constants.intNull
					lclsClause.sDefaulti = .FieldToClass("sDefaulti")
					lclsClause.dEffecdate = .FieldToClass("dEffecdate")
					lclsClause.nId = .FieldToClass("nId")
					lclsClause.nNotenum = .FieldToClass("nNotenumP")
					lclsClause.nNotenumS = .FieldToClass("nNotenumS")
					lclsClause.sClient = .FieldToClass("sClient")
					lclsClause.sCliename = .FieldToClass("sCliename")
					lclsClause.nModulec = .FieldToClass("nModulec")
					lclsClause.sModulecDesc = .FieldToClass("sModulecDesc")
					lclsClause.nCover = .FieldToClass("nCover")
					lclsClause.sCoverDesc = .FieldToClass("sCoverDesc")
					lclsClause.nGroup_insu = .FieldToClass("nGroup_insu")
					lclsClause.sModified = .FieldToClass("sModified")
					lclsClause.nCause = .FieldToClass("nCause")
					lclsClause.sAgree = .FieldToClass("sAgree")
					
					'+Se marca si existe al menos un registro en Clause
					If lclsClause.bInTable Then
						mblnDataExists = True
					End If
					
					Call Add(lclsClause)
					'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClause = Nothing
					
					.RNext()
				Loop 
				
				.RCloseRec()
				
				'+ Se actualiza la información para determinar si se deja marcado por omision
				updateExists()
				
			Else
				FindProduct = False
			End If
		End With
		
Load_Err: 
		If Err.Number Then
			FindProduct = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreatab_clause_clause_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_clause_clause_a = Nothing
	End Function
	
	'% Findpolicy: Devuelve las cláusulas por póliza de una poliza colectiva
	'--------------------------------------------------------
	Public Function FindPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal bQuery As Boolean) As Boolean
		'--------------------------------------------------------
		'- Se define la variable lrecreaClaus_co_p_Clause_a
		Dim lrecreaClaus_co_p_Clause_a As eRemoteDB.Execute
		Dim lclsClause As ePolicy.Clause
		
		On Error GoTo findPolicy_Err
		
		'+Se inicializa la coleccion por si ya fue cargada en otro metodo
		mCol = New Collection
		
		lrecreaClaus_co_p_Clause_a = New eRemoteDB.Execute
		
		With lrecreaClaus_co_p_Clause_a
			.StoredProcedure = "reaClaus_co_p_Clause_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuery", IIf(bQuery, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindPolicy = Not .EOF
				Do While Not .EOF
					lclsClause = New ePolicy.Clause
					lclsClause.sCertype = sCertype
					lclsClause.nBranch = nBranch
					lclsClause.nProduct = nProduct
					lclsClause.nPolicy = nPolicy
					lclsClause.nCertif = nCertif
					lclsClause.nClause = .FieldToClass("nClauseP")
					lclsClause.sDescript = .FieldToClass("sDescript")
					lclsClause.bInTable = .FieldToClass("nClauseS") <> eRemoteDB.Constants.intNull
					lclsClause.sDefaulti = .FieldToClass("sDefaulti")
					lclsClause.dEffecdate = .FieldToClass("dEffecdate")
					lclsClause.nId = .FieldToClass("nId")
					lclsClause.nNotenum = .FieldToClass("nNotenumP")
					lclsClause.nNotenumS = .FieldToClass("nNotenumS")
					lclsClause.sClient = .FieldToClass("sClient")
					lclsClause.sCliename = .FieldToClass("sCliename")
					lclsClause.nModulec = .FieldToClass("nModulec")
					lclsClause.sModulecDesc = .FieldToClass("sModulecDesc")
					lclsClause.nCover = .FieldToClass("nCover")
					lclsClause.sCoverDesc = .FieldToClass("sCoverDesc")
					lclsClause.nGroup_insu = .FieldToClass("nGroup_insu")
					lclsClause.sModified = .FieldToClass("sModified")
					
					'+Se marca si existe al menos un registro en Clause
					If lclsClause.bInTable Then
						mblnDataExists = True
					End If
					
					Call Add(lclsClause)
					'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClause = Nothing
					
					.RNext()
				Loop 
				
				'+ Se actualiza la información para determinar si se deja marcado por omision
				updateExists()
				
				.RCloseRec()
			Else
				FindPolicy = False
			End If
		End With
		
findPolicy_Err: 
		If Err.Number Then
			FindPolicy = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaClaus_co_p_Clause_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClaus_co_p_Clause_a = Nothing
		On Error GoTo 0
	End Function
	
	'% Findgroup: Devuelve las cláusulas por grupo colectivo de una poliza
	'--------------------------------------------------------
	Public Function FindGroup(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal nCertif As Double, ByVal bQuery As Boolean) As Boolean
		'--------------------------------------------------------
		'- Se define la variable lrecreaClaus_co_g_Clause_a
		Dim lrecreaClaus_co_g_Clause_a As eRemoteDB.Execute
		Dim lclsClause As ePolicy.Clause
		
		On Error GoTo findgroup_Err
		
		'+Se inicializa la coleccion por si ya fue cargada en otro metodo
		mCol = New Collection
		
		lrecreaClaus_co_g_Clause_a = New eRemoteDB.Execute
		
		With lrecreaClaus_co_g_Clause_a
			.StoredProcedure = "reaClaus_co_g_Clause_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuery", IIf(bQuery, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindGroup = Not .EOF
				Do While Not .EOF
					lclsClause = New ePolicy.Clause
					lclsClause.sCertype = sCertype
					lclsClause.nBranch = nBranch
					lclsClause.nProduct = nProduct
					lclsClause.nPolicy = nPolicy
					lclsClause.nCertif = nCertif
					lclsClause.nClause = .FieldToClass("nClauseP")
					lclsClause.sDescript = .FieldToClass("sDescript")
					lclsClause.bInTable = .FieldToClass("nClauseS") <> eRemoteDB.Constants.intNull
					lclsClause.sDefaulti = .FieldToClass("sDefaulti")
					lclsClause.dEffecdate = .FieldToClass("dEffecdate")
					lclsClause.nId = .FieldToClass("nId")
					lclsClause.nNotenum = .FieldToClass("nNotenumP")
					lclsClause.nNotenumS = .FieldToClass("nNotenumS")
					lclsClause.sClient = .FieldToClass("sClient")
					lclsClause.sCliename = .FieldToClass("sCliename")
					lclsClause.nModulec = .FieldToClass("nModulec")
					lclsClause.sModulecDesc = .FieldToClass("sModulecDesc")
					lclsClause.nCover = .FieldToClass("nCover")
					lclsClause.sCoverDesc = .FieldToClass("sCoverDesc")
					lclsClause.nGroup_insu = .FieldToClass("nGroup_insu")
					lclsClause.sModified = .FieldToClass("sModified")
					
					'+Se marca si existe al menos un registro en Clause
					If lclsClause.bInTable Then
						mblnDataExists = True
					End If
					
					Call Add(lclsClause)
					'UPGRADE_NOTE: Object lclsClause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClause = Nothing
					
					.RNext()
				Loop 
				
				.RCloseRec()
				
				'+ Se actualiza la información para determinar si se deja marcado por omision
				updateExists()
			Else
				FindGroup = False
			End If
		End With
		
findgroup_Err: 
		If Err.Number Then
			FindGroup = False
		End If
		'UPGRADE_NOTE: Object lrecreaClaus_co_g_Clause_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClaus_co_g_Clause_a = Nothing
		On Error GoTo 0
	End Function
	
	'*% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Clause
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*% Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*% NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'*% Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%updateExists: Permite marcar en colección la existencia de datos
	Private Sub updateExists()
		Dim lclsClause As ePolicy.Clause
		
		'+Ahora que ya están cargados los datos, se actualiza
		'+el indicador de si existian datos de la poliza en clause
		For	Each lclsClause In mCol
			lclsClause.bDataFound = mblnDataExists
		Next lclsClause
		
	End Sub
	
	'*% Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*% Class_Terminate: controla el fin de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






