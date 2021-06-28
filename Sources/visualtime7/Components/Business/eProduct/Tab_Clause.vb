Option Strict Off
Option Explicit On
Public Class Tab_Clause
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Clause.cls                           $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 27/08/03 13.44                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema al 05/12/2000.
	'- Los campos llave de la tabla corresponden a: nBranch, nProduct, nClause y dEffecdate.
	'   Column_name                   Type       Computed   Length  Prec  Scale Nullable    TrimTrailingBlanks    FixedLenNullInSource
	Public nBranch As Integer 'smallint      no         2      5     0     no              (n/a)                    (n/a)
	Public nProduct As Integer 'smallint      no         2      5     0     no              (n/a)                    (n/a)
	Public nClause As Integer 'smallint      no         2      5     0     no              (n/a)                    (n/a)
	Public dEffecdate As Date 'datetime      no         8                  no              (n/a)                    (n/a)
	Public sDefaulti As String 'char          no         1                  yes             no                       yes
	Public sDescript As String 'char          no        30                  yes             no                       yes
	Public nNotenum As Integer 'int           no         4     10     0     yes             (n/a)                    (n/a)
	Public dNulldate As Date 'datetime      no         8                  yes             (n/a)                    (n/a)
	Public sShort_des As String 'char          no        12                  yes             no                       yes
	Public nusercode As Integer 'smallint      no         2      5     0     no              (n/a)                    (n/a)
	Public sModified As String 'char          no        30                  yes             no                       yes
	Public nModulec As Integer
	Public nCover As Integer
	Public nType As Integer
	Public sType_clause As String
	Public sDoc_attach As String
	Public nOrden As Integer
	
	
	'-Tipo de accion: "U"pdate, "D"elete
	Public sType As String
	
	
	'% Update: Realiza las actualizaciones para la cláusula definida en el producto
	'--------------------------------------------------------
	Public Function Update() As Boolean
		'--------------------------------------------------------
		'-Objeto de acceso a la base de datos
		Dim lrecinsTab_clause As eRemoteDB.Execute
		
		lrecinsTab_clause = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+Definición de parámetros para stored procedure 'insudb.insTab_clause'
		'+Información leída el 11/04/2001 10:01:25
		
		With lrecinsTab_clause
			.StoredProcedure = "insTab_clause"
			.Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_clause", sType_clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDoc_attach", sDoc_attach, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 45, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrden", nOrden, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sModified", sModified, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinsTab_clause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_clause = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Exist: Realiza la lectura en la tabla 'Tab_clause' para comprobar la
	'%existencia del registro
	'--------------------------------------------------------
	Public Function Find_Exist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClause As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'--------------------------------------------------------
		'- Objeto de acceso a la base de datos
		Dim lrecreaTab_Clause_o As eRemoteDB.Execute
		
		On Error GoTo Find_Exist_Err
		
		lrecreaTab_Clause_o = New eRemoteDB.Execute
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nClause <> nClause Or Me.dEffecdate <> dEffecdate Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or lblnFind Then
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nClause = nClause
			Me.dEffecdate = dEffecdate
			Me.nModulec = nModulec
			Me.nCover = nCover
			
			'+Definición de parámetros para stored procedure 'insudb.reaTab_Clause_o'
			'+Información leída el 16/04/2001 15:40:58
			
			With lrecreaTab_Clause_o
				.StoredProcedure = "reaTab_Clause_o"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nClause = nClause
					Me.dEffecdate = dEffecdate
					Me.nModulec = nModulec
					Me.nCover = nCover
					sDefaulti = .FieldToClass("sDefaulti")
					sDescript = .FieldToClass("sDescript")
					nNotenum = .FieldToClass("nNoteNum")
					dNulldate = .FieldToClass("dNulldate")
					sShort_des = .FieldToClass("sShort_des")
					sModified = .FieldToClass("sModified")
					nOrden = .FieldToClass("nOrden")
					Find_Exist = True
					.RCloseRec()
				End If
			End With
		Else
			Find_Exist = True
		End If
		
Find_Exist_Err: 
		If Err.Number Then
			Find_Exist = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_Clause_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Clause_o = Nothing
		On Error GoTo 0
	End Function
	
	'% Next_Nclause: Realiza la lectura para Nueva Clausula
	
	'--------------------------------------------------------
	Public Function Next_nclause(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Short
		'--------------------------------------------------------
		'- Objeto de acceso a la base de datos
		Dim lrecreaTab_Clause_o As eRemoteDB.Execute
		
		On Error GoTo Next_nclause_Err
		
		lrecreaTab_Clause_o = New eRemoteDB.Execute
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.dEffecdate = dEffecdate
		
		
		'+Definición de parámetros para stored procedure 'insudb.NEXT_NCLAUSE'
		'+Información leída el 23/08/2007
		
		With lrecreaTab_Clause_o
			.StoredProcedure = "NEXT_NCLAUSE"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NClause_ADD", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Next_nclause = CShort(Trim(.Parameters("NClause_ADD").Value))
			End If
		End With
		
Next_nclause_Err: 
		If Err.Number Then
			Next_nclause = 999
		End If
		'UPGRADE_NOTE: Object lrecreaTab_Clause_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Clause_o = Nothing
		On Error GoTo 0
	End Function
	
	'%insValDP009: Esta función se encarga de validar la información de las cláusulas que aplican
	'%sobre un producto en particular.
	Public Function insValDP009(ByVal sCodispl As String, ByVal nClause As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal sAction As String = "", Optional ByVal nType As Integer = 0, Optional ByVal sType_clause As String = "", Optional ByVal sDoc_attach As String = "", Optional ByVal nOrden As Integer = 0) As String
		'-Objeto de control de errores
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP009_Err
		
		lobjErrors = New eFunctions.Errors
		
		'+Validacion del Clausulas
		If nClause = 0 Or nClause = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11090)
		End If
		
		'+Si la acción es registrar, no debe estar registrado en la tabla de cláusulas
		If nClause <> 0 And nClause <> eRemoteDB.Constants.intNull And sAction = "Add" Then
			If Find_Exist(nBranch, nProduct, nClause, dEffecdate, nModulec, nCover) Then
				Call lobjErrors.ErrorMessage(sCodispl, 10004)
			End If
		End If
		
		'+Validacion para Descripción
		If sDescript = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10010)
		End If
		
		'+Validacion para la descripción abreviada
		If sShort_des = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10011)
		End If
		
		'+Validacion para el tipo de clausula
		If nType = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11334)
		End If
		
		'+Validacion para el orden de impresión
		If nOrden = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 12036)
		End If
		
		'+Validacion para el tipo de cláusula según archivo
		If sType_clause <> String.Empty And sType_clause = "1" Then
			If sDoc_attach = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 800040)
			End If
		End If
		
		insValDP009 = lobjErrors.Confirm
		
insValDP009_Err: 
		If Err.Number Then
			insValDP009 = "insValDP009: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostDP009: Permite las realizar las actualizaciones en las tablas
	Public Function insPostDP009(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClause As Integer, ByVal dEffecdate As Date, ByVal sDefaulti As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal nNotenum As Integer, ByVal nusercode As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal sType As String = "", Optional ByVal nType As Integer = 0, Optional ByVal sType_clause As String = "", Optional ByVal sDoc_attach As String = "", Optional ByVal nOrden As Integer = 0, Optional ByVal sModified As String = "") As Boolean
		'-Objeto para actualizar estado de ventana
		Dim lclsProd_win As Prod_win
		'-Objeto para validar clausulas
		Dim lcolTab_Clauses As Tab_Clauses
		
		On Error GoTo insPostDP009_Err
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			lclsProd_win = New Prod_win
			lcolTab_Clauses = New Tab_Clauses
			
			With Me
				.sType = IIf(sType <> "D", "U", "D")
				.nBranch = nBranch
				.nProduct = nProduct
				.nClause = nClause
				.dEffecdate = dEffecdate
				.sDefaulti = IIf(sDefaulti = "1", "1", 2)
				.nNotenum = nNotenum
				.sDescript = sDescript
				.sShort_des = sShort_des
				.nusercode = nusercode
				.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
				.nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
				.nType = nType
				.sType_clause = IIf(sType_clause = "1", "1", "2")
				.sDoc_attach = IIf(sType_clause = "1", sDoc_attach, String.Empty)
				.nOrden = nOrden
				.sModified = IIf(sModified = "1", "1", "2")
				insPostDP009 = .Update
				If lcolTab_Clauses.Find(nBranch, nProduct, dEffecdate) Then
					Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP009", "2", nusercode)
				Else
					Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP009", "1", nusercode)
				End If
			End With
		End If
		
insPostDP009_Err: 
		If Err.Number Then
			insPostDP009 = False
		End If
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		'UPGRADE_NOTE: Object lcolTab_Clauses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_Clauses = Nothing
		On Error GoTo 0
	End Function
	
	'%GetLoadFile: Obtiene la ruta del servidor donde se van a insertar los archivos
	Public Function GetLoadFile(Optional ByVal nOrigin As Boolean = False) As String
		
		Dim lclsValue As eFunctions.Values
		Dim lstrName As String
		Dim lintlength As Integer
        Dim lstrFileName As String
        Dim strResult As String = ""

        Try

            lclsValue = New eFunctions.Values

            lstrFileName = Trim(UCase(lclsValue.insGetSetting("CLAUSELOAD", String.Empty, "PATHS")))
            If lstrFileName = String.Empty Then
                lstrFileName = Trim(UCase(lclsValue.insGetSetting("CLAUSELOAD", String.Empty, "Config")))
            End If

            lintlength = Len(lstrFileName)
            If Mid(lstrFileName, lintlength, 1) <> "\" Then
                lstrFileName = lstrFileName & "\"
            End If
            If nOrigin Then
                Do While lstrFileName <> String.Empty
                    lstrName = Mid(lstrFileName, 1, 1)
                    strResult = strResult & IIf(lstrName = "\", "\\", lstrName)
                    lstrFileName = Mid(lstrFileName, 2)
                Loop
            Else
                strResult = lstrFileName
            End If

            Return strResult
        Catch ex As Exception
            Return False
        Finally
            lclsValue = Nothing
        End Try
    End Function
End Class






