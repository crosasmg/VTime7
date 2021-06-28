Option Strict Off
Option Explicit On
Option Compare Text
Public Class GenFunct
	'%-------------------------------------------------------%'
	'% $Workfile:: GenFunct.cls                             $%'
	'% $Author:: Mgonzalez                                  $%'
	'% $Date:: 29-09-09 21:42                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Public Enum eQueryType
		qtPolicy = 1
		qtOriginalPolicy = 2
		qtcertificat = 3
		qtClient = 4
		qtProposal = 5
		qtClaim = 6
		qtReceipt = 7
		qtcheque = 8
		qtContract = 9
		qtOriginalReceipt = 10
		qtQuotation = 11
		qtprovider = 40
		qtLoanLease = 60
		qtIntermed = 77
		qtnCompany = 13
		'        qtnreinsuran = 33
		qtnPrimaCedida = 80
		qtnSiniestroCedido = 81
		qtnDistribCapital = 82
		qtnDistReaseg_Poliza = 90
		qtnDistReaseg_Siniestro = 91
	End Enum
	
	Private Enum eDisplay
		clngDisplayPolicyWithShortCut = 1
		clngDisplayProposalWithShortCut = 2
		clngDisplayQuotationWithShortCut = 3
		clngDisplayPolicy = 4
		clngDisplayProposal = 5
		clngDisplayQuotation = 6
		clngDisplayReceipt = 7
		clngDisplayMovement = 8
		clngDisplayClaimWithShortCut = 9
		clngDisplayContratWithShortCut = 10
		clngDisplayProviderWithShortCut = 11
		clngDisplayCheqWithShortCut = 12
		clngDisplayHeaderNoRecords = 23
		clngDisplayHeaderField = 24
		clngDisplayHeaderValue = 25
	End Enum
	
	Private mstrFolderKey As String
	
	Private oClass As Collection
	Private oSubQueries As Collection
	Private oViewProperties As ViewProperties
	Private mstrQuery As String
	
	Public Parameters As Properties
	Public bCreateSubFolder As Boolean
    Public sBrancht As String
    Public bDisplayTitle As Boolean = True

	
	'%insConstructKey. Esta funcion genera la llave que se va a colocar a determinado
	'% del treeview. Para ello recibe el padre del nodo que se está generando
	Private Function insConstructKey(ByRef sParentKey As String, ByRef lintFolder As Integer) As String
		If sParentKey = String.Empty Then
			insConstructKey = "N"
		Else
			insConstructKey = sParentKey
		End If
		insConstructKey = Trim(insConstructKey) & "-" & lintFolder
	End Function
	
	'%insShowData. Esta Funcion se encarga de actualizar los valores del listview y
	'%el treeView de la ventana, dependiendo de los registros y el tipo ed ventana
	'%retornado
	Private Function insShowData(ByVal lintCurrentQuery As Integer, ByVal nParentFolder As Integer, ByVal sParentKey As String, ByVal lintType As Integer, ByRef lobjSubQueries As Collection, ByVal nUserCode As Integer) As Boolean
		Dim lpropObject As Propiedad
        Dim oProperties As Properties
        Dim lintColumns As Integer
		Dim lintShowQua As Integer
		Dim lintChild As Integer
		Dim lblnCreSubFolders As Boolean
		Dim lblnFirst As Boolean
		Dim lstrKey As String
		Dim lstrFolderName As String
		Dim lstrRootName As String
		Dim lblnSubfolders As Boolean
		Dim lstrValue As String
		Dim lstrChildName As String

        Dim lobjGrid As eFunctions.Grid
        Dim lstrGrid As String
        Dim lstrFolders As String = ""
        Dim lstrParamNames As String
		Dim lstrParamValues As String
        Dim lobjFolder As Folder = New Folder
        Dim lobjValues As eFunctions.Values = New eFunctions.Values
        Dim sAuxTittleMenu = ""
        Dim sAuxNameMenu = ""

        lobjGrid = New eFunctions.Grid
		With lobjGrid
			.bOnlyForQuery = True
			.AddButton = False
			.DeleteButton = False
			.Codispl = "GE099"
			.AltRowColor = True
		End With
		lstrGrid = String.Empty
		insShowData = True
		lintShowQua = 0
		lintChild = 1
		lstrParamNames = String.Empty
		lstrParamValues = String.Empty
		lstrFolderName = String.Empty
		lblnCreSubFolders = True
		lblnSubfolders = False
		
		lintChild = 1
		
		If lobjFolder Is Nothing Then
			lobjFolder = New Folder
			Call lobjFolder.Find(lintType)
		End If
		
		lstrFolderName = lobjFolder.sFolderName
		lstrRootName = lobjFolder.sRootName
		
		
		'**+ In case that it is passed as a subqueries multiple parameteres, verifiy the type of
		'**+ folder  that is being used to load the treeview or the listview.
		'+En el caso que la se pasen como parametros multiples subqueries, se verifica el tipo de
		'+Carpeta que se está utilizando para cargar el treeview o el listview
		
		If Not lobjSubQueries Is Nothing Then
			If lobjSubQueries.Count() > 1 Then
				
				'**+ Verifies if the nodo owns subfolders. If it does, load a subfolder for each element
				'**+ of the collection oSubQueries
				'+Se verifica si el nodo posee subcarpetas. Si Posee, se carga una subcarpeta por cada
				'+elemento de la coleccion oSubQueries
				
				lblnSubfolders = insExistsSubFolders(lintCurrentQuery, lintType, nUserCode)
				lblnCreSubFolders = (lblnCreSubFolders And lblnSubfolders)
				lblnFirst = True
				
				'**+ Run all the subfolders of the collection osubqueries, to show the associated properties.
				'+Se recorren todas las subcarpetas de la colección osubqueries, para mostrar las propiedades asociadas
				
				For	Each oProperties In lobjSubQueries
					lintColumns = 0
					lstrParamValues = String.Empty
					lstrParamNames = String.Empty
					
					'**+ Run the subconsult fields, to show them in the listview.
					'+Se recorren los campos de la subconsulta, para mostarlos en el listView
					
					For	Each lpropObject In oProperties
						With lobjGrid
							
							'**+ Keep the parameters that work as a key for the subfolders.
							'+Se guardan los parametors que sirven como llave para las subcarpetas
							
							If bCreateSubFolder And lpropObject.IsKey = "1" Then
								lstrParamValues = lstrParamValues & "&PV=" & insHTMLEncode(Trim(lpropObject.ValorConFormato))
								lstrParamNames = lstrParamNames & "&PN=" & Trim(lpropObject.Key)
							End If
							
							If String.Empty & lpropObject.visible = "1" Then
                                If lblnFirst Then
                                    Call .Columns.AddTextColumn(0, lpropObject.Caption, lpropObject.Key, 0, Trim(lpropObject.ValorConFormato))
                                    '                                .ColumnHeaders(1).Tag = 2

                                    .Columns("Sel").GridVisible = False
                                    .AddButton = False
                                    .DeleteButton = False
                                    .Codispl = "GE099"
                                    .AltRowColor = True
                                End If

                                If lpropObject.Caption.ToUpper = "FIGURA" Then
                                    sAuxNameMenu = lpropObject.Valor
                                    sAuxTittleMenu = lpropObject.Caption
                                End If

                                .Columns((lpropObject.Key)).DefValue = Trim(lpropObject.ValorConFormato)
                                    ' Se formatea el valor que viene de la BD quitandole los decimales para el tipo de moneda :"Pesos chilenos"
                                    Dim nCurrecyView As Integer
                                    If Trim(lpropObject.ValorConFormato) = "Pesos chilenos" Or nCurrecyView = 1 Then
                                        nCurrecyView = 1
                                    Else
                                        nCurrecyView = 0
                                    End If
                                    If Trim(lpropObject.ValorConFormato) <> "Pesos chilenos" And nCurrecyView = 1 Then
                                        .Columns((lpropObject.Key)).DefValue = FormatNumber(lpropObject.ValorConFormato, 0)
                                        nCurrecyView = 0
                                    End If

                                    lintColumns = lintColumns + 1
                                End If
                        End With
					Next lpropObject
                    If lblnFirst And bDisplayTitle Then
                        lobjGrid.Splits_Renamed.AddSplit(0, lstrRootName, lintColumns)
                    End If
					
					lstrGrid = lstrGrid & lobjGrid.DoRow()

                    '**+ Use the variable lblncreSubFolder, to verify if the associated folder must be created
                    '+Se utiliza la variable lblncreSubFolder, para verificar si se deden crear las carpetas asociadas

                    If lblnCreSubFolders Then
                        lstrKey = insConstructKey(sParentKey, lintType) & "-C" & lintChild
                        If lobjFolder.sFolderKey <> String.Empty Then
                            lstrChildName = Trim(lstrFolderName) & " " & Trim(oProperties(Trim(lobjFolder.sFolderKey)).Valor)
                        Else
                            If sAuxTittleMenu.ToUpper = "FIGURA" Then
                                lstrChildName = Trim(sAuxNameMenu)
                            Else
                                lstrChildName = Trim(lstrFolderName)
                            End If

                        End If
                        If bCreateSubFolder Then
                            lstrFolders = lstrFolders & "top.frames[""fraHeader""].insAddValue(""" & lstrChildName & """,""" & lstrKey & """," & CStr(lintType) & ",""" & lstrParamNames & lstrParamValues & "&nFolder=" & CStr(lintType) & """,'" & insFindImage((lobjFolder.nImage)) & "')" & vbCrLf
                        End If
                        ' lstrChildName
                    End If
                    sAuxNameMenu = ""
                    sAuxTittleMenu = ""
                    lintChild = lintChild + 1
					lblnFirst = False
				Next oProperties
				lstrGrid = lstrGrid & lobjGrid.closeTable
				lstrFolders = "<SCRIPT>" & lstrFolders & "</SCRIPT>"
				
				'**+ If the collection of objects has just one element, preceed to fill the listview.
				'+Si la coleccion de objetos tiene un solo elemento, se procede a llenar el listview
				
			Else
				With lobjGrid.Columns
					If lobjValues Is Nothing Then
						lobjValues = New eFunctions.Values
					End If
                    lstrValue = eFunctions.Values.GetMessage(eDisplay.clngDisplayHeaderField)
                    .AddTextColumn(0, Trim(lstrValue), "tctField", 30, String.Empty)
                    lstrValue = eFunctions.Values.GetMessage(eDisplay.clngDisplayHeaderValue)
                    .AddTextColumn(0, Trim(lstrValue), "tctValue", 30, String.Empty)
				End With
				If lintType <> 32 And lintType <> 37 Then
					
					lobjGrid.Columns("Sel").GridVisible = False
					lobjGrid.AddButton = False
					lobjGrid.DeleteButton = False
					lobjGrid.Codispl = "GE099"
                    lobjGrid.AltRowColor = True
                    If bDisplayTitle Then
                        lobjGrid.Splits_Renamed.AddSplit(0, lstrRootName, 2)
                    End If

                    For Each lpropObject In lobjSubQueries.Item(1)
                        If bCreateSubFolder And lpropObject.IsKey = "1" Then
                            lstrParamValues = lstrParamValues & "&PV=" & insHTMLEncode(Trim(lpropObject.ValorConFormato))
                            lstrParamNames = lstrParamNames & "&PN=" & Trim(lpropObject.Key)
                        End If
                        If lpropObject.visible = "1" Then
                            '                    Set litem = llistView.ListItems.Add()
                            With lobjGrid
                                .Columns("tctField").DefValue = lpropObject.Caption
                                .Columns("tctValue").DefValue = Trim(lpropObject.ValorConFormato)
                                lstrGrid = lstrGrid & lobjGrid.DoRow()
                            End With
                        End If
                    Next lpropObject
                    lstrGrid = lstrGrid & lobjGrid.closeTable()
                Else
                    If lintType = 37 Then
                        For Each lpropObject In lobjSubQueries.Item(1)
                            lstrGrid = lstrGrid & "<PRE>" & lpropObject.ValorConFormato & "</PRE>"
                        Next lpropObject
                    End If
                End If


                If lintType <> 32 And lintType <> 37 Then
                    lstrFolders = insChargeNodes(lintCurrentQuery, lintType, sParentKey, lstrParamNames & lstrParamValues, nUserCode)
                End If
                End If
		Else
			insShowData = True
			With lobjGrid
				.Columns.AddTextColumn(0, "Mensaje", "tctDescript", 30, String.Empty)
				.Columns("Sel").GridVisible = False
				.DeleteButton = False
				.AddButton = False
				lstrGrid = .closeTable()
			End With
		End If
		'UPGRADE_NOTE: Object lobjFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjFolder = Nothing
		mstrQuery = lstrFolders & lstrGrid
	End Function
	
	'%insExistsSubFolders. Esta funcion recibe como parametro un tipo de carpeta,
	'% y devuelve verdarero o falso, dependiendo de las esistencia de registros
	'%asociados a esta
	Private Function insExistsSubFolders(ByRef lintCurrentQuery As Integer, ByRef lintParent As Integer, ByRef nUserCode As Integer) As Boolean
		
		Dim lrecreaSeqFolder As eRemoteDB.Execute
		
		lrecreaSeqFolder = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.ReaSeqFolder'
		'Información leída el 25/11/1999 10:17:31 a.m.
		
		With lrecreaSeqFolder
			.StoredProcedure = "ReaSeqFolder"
			.Parameters.Add("nQueryType", lintCurrentQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParent", lintParent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insExistsSubFolders = .Run
			If insExistsSubFolders Then
				insExistsSubFolders = Not .EOF
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSeqFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSeqFolder = Nothing
	End Function
	
	'%insCreintanceClass. Esta funcion se encarga de crear una clase especifica, para cada item
	'%seleccionado del listview.
	Private Function insCreInstanceClass(ByVal sClass As String, ByVal nFolder As Integer) As Object
        Dim lobjGeneric As Object = New Object
        If Trim(sClass) <> String.Empty Then
			On Error Resume Next
			lobjGeneric = oClass.Item("oClass" & nFolder)
			If Err.Number Then
				On Error GoTo 0
				Select Case Trim(sClass)
					Case "CLIENT"
						lobjGeneric = New Client
					Case "CLIENTS"
                        lobjGeneric = New Clients
                    Case "ClassificationClient"
                        lobjGeneric = New ClassificationClient
					Case "CLIENT_EVALRISK"
						lobjGeneric = New Client_EvalRisk
					Case "RELATIONS"
						lobjGeneric = New Relations
					Case "POLICY"
						lobjGeneric = New Policy
						lobjGeneric.sBrancht = sBrancht
					Case "POLICIES"
						lobjGeneric = New Policies
						lobjGeneric.sBrancht = sBrancht
					Case "ADDRESSES"
						lobjGeneric = New Addresses
					Case "COVERS"
						lobjGeneric = New Covers
					Case "CLAIMS"
						lobjGeneric = New Claims
					Case "PREMIUMS"
						lobjGeneric = New Premiums
					Case "DETAIL_PRE"
						lobjGeneric = New Detail_pre
					Case "PHONES"
						lobjGeneric = New Phones
					Case "DISCO_EXPR"
						lobjGeneric = New Disco_expr
					Case "CLAUSES"
						lobjGeneric = New Clauses
					Case "INTERMEDIARIES"
						lobjGeneric = New Intermediaries
					Case "CLAIM_CASE"
						lobjGeneric = New Claim_case
					Case "HISTORY"
						lobjGeneric = New History
					Case "CHEQUES"
						lobjGeneric = New Cheques
					Case "CONTRATS"
						lobjGeneric = New Contrats
					Case "DRAFTS"
						lobjGeneric = New Drafts
					Case "COINSURAN"
						lobjGeneric = New Coinsuran
					Case "CL_COVER"
						lobjGeneric = New Cl_cover
					Case "PREMIUM_MO"
						lobjGeneric = New Premium_mo
					Case "CERTIFICATS"
						lobjGeneric = New Certificats
					Case "CERTIFICNN"
						lobjGeneric = New Certificnn
					Case "REINSURANCES"
						lobjGeneric = New Reinsurances
					Case "REINSURAN"
						lobjGeneric = New Reinsuran
					Case "NOTE"
						lobjGeneric = New Note
					Case "NOTESIMAGES"
						lobjGeneric = New NotesImages
					Case "PROVIDER"
						lobjGeneric = New Provider
					Case "Move_Acc"
						lobjGeneric = New Move_Acc
					Case "CURR_ACC"
						lobjGeneric = New Curr_acc
					Case "BK_ACCOUNTS"
						lobjGeneric = New Bk_accounts
					Case "CRED_CARDS"
						lobjGeneric = New Cred_cards
					Case "BULLETINS"
						lobjGeneric = New Bulletins
					Case "HISTORY_AMEND"
						lobjGeneric = New History_amend
					Case "COVER_AMEND"
						lobjGeneric = New Cover_amend
					Case "CLAUSE_AMEND"
						lobjGeneric = New Clause_amend
					Case "DISC_XPREM_AMEND"
						lobjGeneric = New Disc_xprem_amend
					Case "ROLES_AMEND"
						lobjGeneric = New Roles_amend
					Case "DATE_PARTICULAR_AMEND"
						lobjGeneric = New Date_particular_amend
					Case "Loans"
						lobjGeneric = New Loans
					Case "LIFECOV_VAR"
						lobjGeneric = New LifeCov_Var
					Case "INSURED_EXPDIS"
						lobjGeneric = New Insured_expdis
					Case "Move_AccPOL"
						lobjGeneric = New Move_Accpol
					Case "FUNDS"
						lobjGeneric = New Funds
					Case "FUNDS2"
						lobjGeneric = New Funds2
					Case "REQUEST"
						lobjGeneric = New Request
					Case "GUARANT_VAL"
						lobjGeneric = New Guarant_val
					Case "LIFE_DOCU"
						lobjGeneric = New Life_docu
					Case "LIFE_DOCU"
						lobjGeneric = New Life_docu
					Case "Comm_pol"
						lobjGeneric = New Comm_pol
					Case "COMPANY"
						lobjGeneric = New Company
					Case "CESSION_PR"
						lobjGeneric = New Cession_pr
					Case "CLAIM_CES"
						lobjGeneric = New Claim_Ces
					Case "DISTR_CAP"
						lobjGeneric = New Distr_Cap
					Case "PART_CONTR"
						lobjGeneric = New Part_contr
					Case "REA_COVER"
						lobjGeneric = New Rea_Cover
					Case "REA_CTACTE"
						lobjGeneric = New Rea_Ctacte
					Case "BENEFICIAR"
						lobjGeneric = New Beneficiar
					Case "GUAR_SAVING_POL"
						lobjGeneric = New Guar_saving_pol
					Case "GUAR_SAVING_DETPOL"
						lobjGeneric = New Guar_saving_detpol
					Case "UL_SAVING_MOVE_POL"
						lobjGeneric = New ul_saving_move_pol
					Case "DIR_DEBIT"
						lobjGeneric = New Dir_debit
					Case "COST"
						lobjGeneric = New Cost
					Case "ORIGIN"
						lobjGeneric = New Origin
					Case "FUNDSMATRIX"
						lobjGeneric = New FUNDSMATRIX
					Case "FUNDS3"
						lobjGeneric = New Funds3
                    Case "FISCALRESIDENCEPOL"
                        lobjGeneric = New FiscalResidencePol
                    Case "FISCAL_RESIDENCE"
                        lobjGeneric = New Fiscal_Residence
                    Case Else
                        'UPGRADE_NOTE: Object lobjGeneric may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lobjGeneric = Nothing
				End Select
				If oClass Is Nothing Then
					oClass = New Collection
				End If
				oClass.Add(lobjGeneric, "oClass" & nFolder)
			End If
			On Error GoTo 0
		End If
		insCreInstanceClass = lobjGeneric
	End Function
	
	'%insChargeNodes. Esta función se encarga de llenar las carpetas de un nodo determinado.
	Private Function insChargeNodes(ByVal nCurrencyQuery As Integer, ByVal nType As Integer, ByVal sParentKey As String, ByVal lstrParam As String, ByVal nUserCode As Integer) As String
		Dim lrecreaSeqFolder As eRemoteDB.Execute
		Dim lstrKey As String
		Dim lstrOrigi As String
		Dim lstrScript As String
		
		'Definición de parámetros para stored procedure 'insudb.ReaSeqFolder'
		'Información leída el 22/11/1999 11:54:56 AM
		lrecreaSeqFolder = New eRemoteDB.Execute
		insChargeNodes = String.Empty
		With lrecreaSeqFolder
			.StoredProcedure = "ReaSeqFolder"
			.Parameters.Add("nQueryType", nCurrencyQuery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParent", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lstrKey = insConstructKey(sParentKey, .FieldToClass("nFolder"))
					lstrOrigi = .FieldToClass("sOrigi")
					lstrScript = "top.frames[""fraHeader""].insAddValue(""" & Trim(.FieldToClass("sRootName")) & """,""" & lstrKey & """," & CStr(nType) & ",""" & lstrParam & "&nFolder=" & CStr(.FieldToClass("nFolder")) & """,'" & insFindImage(.FieldToClass("nImage", eFunctions.Values.eTypeData.etdInteger)) & "')" & vbCrLf
					If sBrancht = String.Empty Then
						If bCreateSubFolder Then
							insChargeNodes = insChargeNodes & lstrScript
						End If
					Else
						If lstrOrigi = "1" And sBrancht = "1" Then
							If bCreateSubFolder Then
								insChargeNodes = insChargeNodes & lstrScript
							End If
						Else
							If lstrOrigi = "2" And sBrancht <> "1" Then
								If bCreateSubFolder Then
									insChargeNodes = insChargeNodes & lstrScript
								End If
							Else
								If lstrOrigi = "3" Then
									If bCreateSubFolder Then
										insChargeNodes = insChargeNodes & lstrScript
									End If
								End If
							End If
						End If
					End If
					.RNext()
				Loop 
				.RCloseRec()
				insChargeNodes = "<SCRIPT>" & insChargeNodes & "</SCRIPT>"
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSeqFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSeqFolder = Nothing
	End Function
	
	'%insHTMEncode. Esta funcion se encarga de realizar la conversión de los caracteres
	'%no soportadoe por el browser
	Private Function insHTMLEncode(ByVal lstrString As String) As String
		lstrString = Replace(lstrString, "&", "%26")
		lstrString = Replace(lstrString, """", "%22")
		lstrString = Replace(lstrString, "'", "%27")
		lstrString = Replace(lstrString, "/", "%2f")
		lstrString = Replace(lstrString, "\", "%5c")
		lstrString = Replace(lstrString, "á", "%e1")
		lstrString = Replace(lstrString, "é", "%e9")
		lstrString = Replace(lstrString, "í", "%ed")
		lstrString = Replace(lstrString, "ó", "%f3")
		lstrString = Replace(lstrString, "ú", "%fa")
		lstrString = Replace(lstrString, "Á", "%c1")
		lstrString = Replace(lstrString, "É", "%c9")
		lstrString = Replace(lstrString, "Í", "%cd")
		lstrString = Replace(lstrString, "Ó", "%d3")
		lstrString = Replace(lstrString, "Ú", "%da")
		insHTMLEncode = lstrString
	End Function
	'%insExistsSubFolders. Esta funcion recibe como parametro un tipo de carpeta,
	'% y devuelve verdarero o falso, dependiendo de las esistencia de registros
	'%asociados a esta
	Private Function insReaFoldersName(ByRef lintFolder As Integer) As String
		
		Dim lrecreaFolders_o As eRemoteDB.Execute
		
		lrecreaFolders_o = New eRemoteDB.Execute
		
		mstrFolderKey = String.Empty
		insReaFoldersName = String.Empty
		
		'Definición de parámetros para stored procedure 'insudb.reaFolders_o'
		'Información leída el 25/11/1999 01:13:13 p.m.
		
		With lrecreaFolders_o
			.StoredProcedure = "reaFolders_o"
			.Parameters.Add("nFolder", lintFolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Not .EOF Then
					insReaFoldersName = Trim(.FieldToClass("sFolderName"))
					mstrFolderKey = Trim(.FieldToClass("sFolderKey"))
				End If
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaFolders_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFolders_o = Nothing
	End Function

    '%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
    '%de propiedades
    Public Function Find(ByVal sCodispl As String, ByRef nCurrentQuery As Integer, ByVal nFolder As Integer, ByVal nParentFolder As Integer, ByVal sParentKey As String, ByVal nUserCode As Integer) As Boolean
        Dim Count As Integer
        Dim lclsPropiedad As Propiedad
        Dim lexeTime As eRemoteDB.Execute
        Dim lobjClaims As Properties
        Dim lpropClaims As Propiedad
        Dim lobjParent As Properties
        Dim lcolViewProperties As ViewProperties
        Dim lViewProperty As ViewProperty
        Dim lcollection As Collection
        Dim lblnRead As Boolean
        Dim lobjGeneric As Object
        Dim lobjFolder As Folder
        Dim lobjProduct As Object

        lcolViewProperties = New ViewProperties
        If lcolViewProperties.Find(sCodispl, nFolder) Then
            lobjParent = Parameters
            Count = 0
            For Each lclsPropiedad In lobjParent
                If nCurrentQuery = 1 Or nCurrentQuery = 3 Or nCurrentQuery = 5 Or nCurrentQuery = 11 Then
                    If lclsPropiedad.Key = "HnCertif" Then
                        If lclsPropiedad.Valor.ToString() = "" Then
                            lobjParent("HnCertif").Valor = 0
                        End If
                    End If
                    If lclsPropiedad.Key = "HnBranch" Or lclsPropiedad.Key = "HnProduct" Then
                        Count = Count + 1
                    End If
                    If Count = 2 Then
                        Count = 3
                        sBrancht = "1"
                        'If lobjProduct Is Nothing Then
                        '	lobjProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
                        'End If
                        'With lobjProduct
                        '	If .insValProdMaster(lobjParent("Hnbranch").Valor, lobjParent("HnProduct").Valor) Then
                        '		sBrancht = .sBrancht
                        '	End If
                        'End With
                    End If
                Else
                    If lclsPropiedad.Key = "nCertif" Then
                        If lclsPropiedad.Valor = "" Then
                            lobjParent("nCertif").Valor = 0
                        End If
                    End If
                    If lclsPropiedad.Key = "nBranch" Or lclsPropiedad.Key = "nProduct" Then
                        Count = Count + 1
                    End If
                    If Count = 2 Then
                        Count = 3
                        sBrancht = "1"
                        'If lobjProduct Is Nothing Then
                        '	lobjProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
                        'End If
                        'With lobjProduct
                        '	If .insValProdMaster(lobjParent("nBranch").Valor, lobjParent("nProduct").Valor) Then
                        '		sBrancht = .sBrancht
                        '	End If
                        'End With
                    End If
                End If
            Next lclsPropiedad
            'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjProduct = Nothing

            '+Se realiza el método Find del objeto perteneciente a la carpeta seleccionada
            lobjFolder = New Folder
            Call lobjFolder.Find(nFolder)
            lobjGeneric = insCreInstanceClass(lobjFolder.sClass, nFolder)
            If Trim(lobjFolder.sClass) = "POLICIES" Then
                lobjGeneric.nUserCode = nUserCode
            End If
            If Not lobjGeneric Is Nothing Then
                lexeTime = lobjGeneric.Find(nParentFolder, Parameters)
            Else
                'UPGRADE_NOTE: Object lexeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lexeTime = Nothing
                Find = False
                mstrQuery = "La clase asociada a la carpeta no está programada"
            End If

            '+En el caso de que existan registros
            If Not lexeTime Is Nothing Then
                Do While Not lexeTime.EOF
                    lobjClaims = New Properties
                    For Each lViewProperty In lcolViewProperties
                        lpropClaims = lobjClaims.Add(lViewProperty.Key)
                        With lpropClaims
                            .Key = lViewProperty.Key
                            .Format_Renamed = lViewProperty.Format_Renamed
                            .Caption = lViewProperty.Caption
                            .visible = lViewProperty.visible
                            .IsKey = lViewProperty.IsKey
                            On Error Resume Next
                            Select Case .Key
                                Case Else
                                    lexeTime.HideErrorMsg = True
                                    .Valor = lexeTime.FieldToClass(.Key)
                                    lexeTime.HideErrorMsg = False
                            End Select
                            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                            If IsNothing(.Valor) Then
                                Call lobjClaims.Remove((lViewProperty.Key))
                            End If
                            On Error GoTo 0
                        End With
                    Next lViewProperty
                    If lcollection Is Nothing Then
                        lcollection = New Collection
                    End If
                    lcollection.Add(lobjClaims)
                    lexeTime.RNext()
                Loop
            End If
            'UPGRADE_NOTE: Object lexeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lexeTime = Nothing
        End If
        Find = insShowData(nCurrentQuery, nParentFolder, sParentKey, nFolder, lcollection, nUserCode)

    End Function

    Public ReadOnly Property HTMLQuery() As String
        Get
            HTMLQuery = mstrQuery
        End Get
    End Property

    Public ReadOnly Property HTMLParentNode(ByVal nFolder As Integer) As String
        Get
            Dim lobjFolder As Folder
            Dim lstrParameters As String
            Dim lobjParameter As Propiedad

            lobjFolder = New Folder

            '+Se inicializan los parametros

            lstrParameters = String.Empty

            If lobjFolder.Find(nFolder) Then

                '+Se recorre la lista de parametros, para contruir la cadena de inicialización del arbol

                For Each lobjParameter In Parameters
                    With lobjParameter
                        lstrParameters = lstrParameters & "&" & .Key & "=" & .Valor
                    End With
                Next lobjParameter
                lstrParameters = Trim(lstrParameters) & IIf(Trim(lstrParameters) = String.Empty, String.Empty, "&") & "nFolder=" & CStr(nFolder)
                'HTMLParentNode = "<SCRIPT>" & "if (typeof(top.frames['fraHeader'].foldersTree)=='undefined' ) top.frames['fraHeader'].initializeTree('" & lobjFolder.sFolderName & "','" & insFindImage((lobjFolder.nImage)) & "','" & lstrParameters & "','C1')" & "</SCRIPT>"
                HTMLParentNode = "<SCRIPT>" & "if (top.frames['fraHeader'].foldersTree==0) top.frames['fraHeader'].initializeTree('" & lobjFolder.sFolderName & "','" & insFindImage((lobjFolder.nImage)) & "','" & lstrParameters & "','C1')" & "</SCRIPT>"

            End If
            'UPGRADE_NOTE: Object lobjFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjFolder = Nothing
        End Get
    End Property

    Private Function insFindImage(ByRef nImage As Integer) As String
        If nImage <> 0 And nImage <> eRemoteDB.Constants.intNull Then
            Select Case nImage
                Case 10 ' Poliza
                    insFindImage = "/VTimeNet/images/GenQue10.gif"
                Case 11 ' Cliente
                    insFindImage = "/VTimeNet/images/FindClientOn.png"
                Case 13 ' Intermediarios
                    insFindImage = "/VTimeNet/images/batchStat05.png"
                Case 15
                    insFindImage = "/VTimeNet/images/GenQue15.gif"
                Case 16
                    insFindImage = "/VTimeNet/images/GenQue16.gif"
                Case 17 ' Siniestros
                    insFindImage = "/VTimeNet/images/DMESINT.gif"
                Case 19
                    insFindImage = "/VTimeNet/images/GenQue19.gif"
                Case 21
                    insFindImage = "/VTimeNet/images/GenQue21.gif"
                Case Else
                    insFindImage = String.Empty
            End Select
        Else
            insFindImage = String.Empty
        End If
    End Function

    '%Class_Initialize. Se inicializan los valores de las variables.
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'UPGRADE_NOTE: Object oClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oClass = Nothing
        '    Set oFolders = Nothing
        'UPGRADE_NOTE: Object oSubQueries may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oSubQueries = Nothing
        Parameters = New Properties
        mstrQuery = String.Empty
        bCreateSubFolder = True
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub
End Class






