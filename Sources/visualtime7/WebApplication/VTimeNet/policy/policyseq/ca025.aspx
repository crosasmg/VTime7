<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eClient" %>
<%@ Import Namespace="eGeneral" %>
<%@ Import Namespace="eSecurity" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eProduct" %>
<%@ Import Namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As eFunctions.Grid

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo particular de los datos de la página
    Dim mcolRoleses As ePolicy.Roleses
    Dim mclsRoles As ePolicy.Roles

    Dim lobjErrors As eGeneral.GeneralFunction

    Dim mstrCompon As String

    Dim mstrAlert As String

    Dim mobjUser As eSecurity.User

    Dim mstrBrancht As Object
    Dim mstrProdclas As Object
    Dim mstrTransaction As Object
    Dim mstrCertif As Object

    Dim mstrBranch As Object
    Dim mstrProduct As Object
    Dim mstrPolitype As Object
    Dim mstrCertype As Object
    Dim mstrPolicy As Object
    Dim mstrEffecdate As Object
    Dim mstrRole As String
    Dim mstrType As String
    Dim mstrAction As String
    Dim mstrCodispl As String
    Dim mblnIntermedia As Object
    Dim mobjClient As eClient.Client
    Dim mstrSmoking As String
    Dim mobjProduct As eProduct.Product
    Dim mobjSecur_sche As eSecurity.Secur_sche

    Dim bDisabledByLevels As Boolean


    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
	
        Dim lblnDisable As Boolean
        Dim nTypeRisk As Object
        Dim lobjColumn As eFunctions.Column
	
	
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
	
        mobjGrid.sCodisplPage = mstrCodispl
        Call mobjGrid.SetWindowParameters(mstrCodispl, Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
        mclsRoles = New ePolicy.Roles
	
        lblnDisable = mclsRoles.DisabledField("ClientData", mobjValues.StringToType(mstrTransaction, eFunctions.Values.eTypeData.etdDouble))
	
        If Not lblnDisable Then
            '+ Si no tiene asociado un cliente o la acción es agregar de la ventana PopUp o el cliente corresponde con un intermediario; se coloca la variable lblnDisable en true
            If Request.QueryString.Item("sClient") = vbNullString Or mstrAction = "Add" Or mstrRole = "13" Then
                lblnDisable = True
            End If
        End If
	
        '+ Se definen las columnas del grid
        With mobjGrid.Columns
            If mstrType <> "PopUp" Then
                Call .AddAnimatedColumn(100693, GetLocalResourceObject("btnQueryColumnCaption"), "btnQuery", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("btnQueryColumnToolTip"))
            End If
            Call .AddPossiblesColumn(40979, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "tabCliallopro", eFunctions.Values.eValuesType.clngComboType, , True, , , ,"insChangeRole();", , , GetLocalResourceObject("cbeRoleColumnToolTip"))
		
            If mstrRole = "13" Then
                Call .AddPossiblesColumn(40991, GetLocalResourceObject("tctCodeColumnCaption"), "tctCode", "TabIntermedia_Office", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , 10, GetLocalResourceObject("tctCodeColumnCaption"))
            Else
                Call .AddClientColumn(40981, GetLocalResourceObject("tctCodeColumnCaption"), "tctCode", vbNullString, , GetLocalResourceObject("tctCodeColumnToolTip"), "InsChangeClient(this)", , "lblCliename", False, , , , , True, "&sForm=CA025")
            End If
            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypenameColumnCaption"), "cbeTypename", "table5592", eFunctions.Values.eValuesType.clngComboType, CStr(1), , , , , , CBool(lblnDisable), , GetLocalResourceObject("cbeTypenameColumnToolTip"))
            Call .AddTextColumn(0, GetLocalResourceObject("tctPrintNameColumnCaption"), "tctPrintName", 50, vbNullString, , GetLocalResourceObject("tctPrintNameColumnToolTip"), , , , True)
            
            '+Se definen las columnas que se muestran sólo si el producto es vida
            If mstrBrancht = "1" Then
                Call .AddDateColumn(0, GetLocalResourceObject("tcdBirthdateColumnCaption"), "tcdBirthdate", "", , GetLocalResourceObject("tcdBirthdateColumnToolTip"), , , , lblnDisable)
                Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSexclienColumnCaption"), "cbeSexclien", "table18", eFunctions.Values.eValuesType.clngComboType, , , , , , , CBool(lblnDisable), , GetLocalResourceObject("cbeSexclienColumnToolTip"))
                Call .AddCheckColumn(0, GetLocalResourceObject("chkSmokingColumnCaption"), "chkSmoking", "", , , "changeSmoking(this);", mstrType <> "PopUp" , GetLocalResourceObject("chkSmokingColumnToolTip"))
						
                'If Request.QueryString.Item("sSmoking") = "2" Then
                'lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeTyperiskColumnCaption"), "cbeTyperisk", "Table5639", eFunctions.Values.eValuesType.clngComboType, , , , , , , mstrType <> "PopUp" Or mblnIntermedia, , GetLocalResourceObject("cbeTyperiskColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
                'lobjColumn.TypeList = CShort("2")
                'lobjColumn.List = "4"
                'Else
                Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTyperiskColumnCaption"), "cbeTyperisk", "Table5639", eFunctions.Values.eValuesType.clngComboType, , , , , , , False, , GetLocalResourceObject("cbeTyperiskColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
                'End If
                mobjGrid.Columns("cbeTyperisk").BlankPosition = False
                Call .AddAnimatedColumn(0, "Dirección", "cmdAddress", "/VTimeNet/Images/ShowAddress.png", "Dirección asociada al cliente para la póliza/certificado")
                mobjGrid.Columns("cmdAddress").HRefScript = "ShowAddress('Popup');"
                Call .AddHiddenColumn("hddsReqAddress", vbNullString)
                
			
                Call .AddCheckColumn(0, GetLocalResourceObject("chkVIPColumnCaption"), "chkVIP", "", , , , mstrType <> "PopUp", GetLocalResourceObject("chkVIPColumnToolTip"))
			
                If Session("sPolitype") = "1" Or mstrProdclas = 3 Or mstrProdclas = 4 Or mstrProdclas = 5 Then
                    Call .AddCheckColumn(0, GetLocalResourceObject("chksContinuedColumnCaption"), "chksContinued", "", , , "OnClicksVip()", True, GetLocalResourceObject("chksContinuedColumnToolTip"))
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdContinueColumnCaption"), "tcdContinue", "", , GetLocalResourceObject("tcdContinueColumnToolTip"), , , , True)
                Else
                    Call .AddCheckColumn(0, GetLocalResourceObject("chksContinuedColumnCaption"), "chksContinued", "", , , "OnClicksVip()", mstrType <> "PopUp", GetLocalResourceObject("chksContinuedColumnToolTip"))
                    Call .AddDateColumn(0, GetLocalResourceObject("tcdContinueColumnCaption"), "tcdContinue", "", , GetLocalResourceObject("tcdContinueColumnToolTip"), , , , lblnDisable)
                End If 
                    
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatingColumnCaption"), "tcnRating", 4, "", , GetLocalResourceObject("tcnRatingColumnToolTip"), , , , , , True)
			
                '+ Se muetra solo cuando es poliza matriz o individual
                If mstrCertif = 0 Then
                    '+ Solo se muestra si No es Vida No tradicional
                    If mstrProdclas = 3 Or mstrProdclas = 4 Or mstrProdclas = 5 Then
                        Call .AddHiddenColumn("valContrat_Pay", "")
                    Else
                        Call .AddPossiblesColumn(0, GetLocalResourceObject("valContrat_PayColumnCaption"), "valContrat_Pay", "tabcontrat_pay", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , Not (bDisabledByLevels), , GetLocalResourceObject("valContrat_PayColumnToolTip"))
                    End If
                Else
                    Call .AddHiddenColumn("valContrat_Pay", "")
                End If
                If Session("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyAmendment And Session("nTransaction") <> eCollection.Premium.PolTransac.clngTempPolicyAmendment And Session("nTransaction") <> eCollection.Premium.PolTransac.clngCertifAmendment And Session("nTransaction") <> eCollection.Premium.PolTransac.clngTempCertifAmendment And Session("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyPropAmendent And Session("nTransaction") <> eCollection.Premium.PolTransac.clngCertifPropAmendent Then
                    Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusrolColumnCaption"), "cbeStatusrol", "table5561", eFunctions.Values.eValuesType.clngComboType, CStr(1), False, , , , , True, , GetLocalResourceObject("cbeStatusrolColumnToolTip"))
                Else
                    Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusrolColumnCaption"), "cbeStatusrol", "table5561", eFunctions.Values.eValuesType.clngComboType, CStr(1), False, , , , , mstrRole = "13", , GetLocalResourceObject("cbeStatusrolColumnToolTip"))
                End If
            Else
                Call .AddHiddenColumn("cbeStatusrol", CStr(1))
            End If

            '+ Se Agrego la Columna "Codigo del Asegurado"
            Call .AddTextColumn(0, GetLocalResourceObject("tctItemColumnCaption"), "tctItem", 30, "", , GetLocalResourceObject("tctItemColumnToolTip"))
            
            '+ Se definen las columnas ocultas del grid
            Call .AddHiddenColumn("hddsOldCode", "")
            Call .AddHiddenColumn("hddnMaxRole", CStr(0))
            Call .AddHiddenColumn("hddnExist", "")
            Call .AddHiddenColumn("hddsOldRole", "")
            Call .AddHiddenColumn("hddnCoverPos", CStr(0))
            Call .AddHiddenColumn("hddsRequire", vbNullString)
            Call .AddHiddenColumn("hddsInterClient", vbNullString)
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            With .Columns("cbeRole").Parameters
                .Add("nBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", mstrProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("sPolitype", mstrPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("sCompon", mstrCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            .Columns("cbeRole").BlankPosition = False
            With .Columns("tctCode").Parameters
                .Add("sCertype", mstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", mstrProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nPolicy", mstrPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", mstrEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .ReturnValue("sClient", , , True)
            End With
            .Columns("cbeStatusrol").BlankPosition = False
            .Codispl = "CA025"
            .ActionQuery = mobjValues.ActionQuery
            .Columns("cbeRole").EditRecord = True
            .Top = 100
            .bCheckVisible = False
            '+Se definen el ancho si el producto es vida	    
            'Response.Write "<NOTSCRIPT>alert('" & mstrBrancht & "')</" & "Script>"		
            If mstrBrancht = "1" Then
                .FieldsByRow = 2
                .Width = 660
                If mstrCertif > 0 Then
                    .Height = 520
                Else
                    .Height = 480
                End If
            Else
                .Width = 630
                .Height = 280
            End If
            .AddButton = Not .ActionQuery
            .DeleteButton = Not .ActionQuery
            If Request.QueryString.Item("nMainAction") = "undefined" Then
                .nMainAction = 0
            Else
                .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            End If
            .Columns("Sel").GridVisible = Not .ActionQuery
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .sDelRecordParam = "sCode=' + marrArray[lintIndex].tctCode + '&nCoverPos=' + marrArray[lintIndex].hddnCoverPos + '&nRole=' + marrArray[lintIndex].cbeRole + '&sRequire=' + marrArray[lintIndex].hddsRequire + '&sInterClient=' + marrArray[lintIndex].hddsInterClient  +  '"
            .MoveRecordScript = "DisabledItem()"
            If mstrType = "PopUp" Then
                If mstrBrancht = "1" Then
                    If bDisabledByLevels Then
                        If mstrCertif = "0" Then
                            ' Contratante, Administrador de póliza y Administrador de recaudación
                            If mstrRole = "1" OrElse mstrRole = "71" OrElse mstrRole = "73" Then
                                .Columns("valContrat_Pay").Disabled = False
                            Else
                                .Columns("valContrat_Pay").Disabled = True
                            End If
                        Else
                            .Columns("valContrat_Pay").Disabled = True
                        End If
                    Else
                        .Columns("valContrat_Pay").Disabled = True
                    End If
                End If
                ' Asegurado, Co-Asegurado, Padres, Hijo(a), Cónyuge, Hermano, Hijo Discapacitado
                If mstrRole = "2" Or mstrRole = "7" Or mstrRole = "21" Or mstrRole = "22" Or mstrRole = "23" Or mstrRole = "24" Or mstrRole = "60" Then
                    If mstrBrancht = "1" Then
                        If mstrProdclas = 8 Or mstrProdclas = 11 Then
                            '+Se permite modificación del cliente, del sexo, fecha de nacimiento, etc, para 
                            '+transacciones con manejo de colectivos.
                            If (mstrTransaction = 12 Or mstrTransaction = 13 Or mstrTransaction = 14 Or
                                mstrTransaction = 15 Or mstrTransaction = 18 Or mstrTransaction = 19 Or
                                mstrTransaction = 26 Or mstrTransaction = 27 Or mstrTransaction = 28 Or
                                mstrTransaction = 29 Or mstrTransaction = 31) Then
                                .Columns("tctCode").Disabled = False
                                .Columns("cbeRole").Disabled = False
                                .Columns("cbeTypename").Disabled = False
                                .Columns("tcdBirthdate").Disabled = False
                                .Columns("cbeSexclien").Disabled = False
                                .Columns("chkSmoking").Disabled = False
							
                                'If Not mblnIntermedia Then
                                '.Columns("cbeTyperisk").Disabled = False
                                'End If
							
                                If bDisabledByLevels Then
                                    .Columns("chkVIP").Disabled = False
                                Else
                                    .Columns("chkVIP").Disabled = True
                                End If
							
                                .Columns("tcnRating").Disabled = False
                                .Columns("cbeStatusrol").Disabled = False
                                ' Asegurado
                                If mstrRole = "2" Then
                                    Response.Write("<SCRIPT>alert('" & mstrAlert & "');</" & "Script>")
                                End If
                            Else
                                .Columns("tcdBirthdate").Disabled = True
                                .Columns("cbeSexclien").Disabled = True
                                '.Columns("chkSmoking").Disabled = True
                                .Columns("chkSmoking").Disabled = False
                            End If
                        Else
                            If (mstrTransaction = 12 Or mstrTransaction = 26) Then
                                .Columns("tctCode").Disabled = False
                                .Columns("cbeRole").Disabled = False
                                .Columns("cbeTypename").Disabled = False
                                .Columns("tcdBirthdate").Disabled = False
                                .Columns("cbeSexclien").Disabled = False
                                .Columns("chkSmoking").Disabled = False
                                'If Not mblnIntermedia Then
                                '.Columns("cbeTyperisk").Disabled = False
                                'End If
                                If bDisabledByLevels Then
                                    .Columns("chkVIP").Disabled = False
                                Else
                                    .Columns("chkVIP").Disabled = True
                                End If
							
                                .Columns("tcnRating").Disabled = False
                                .Columns("cbeStatusrol").Disabled = False
                                If mstrRole = "2" Then
                                    Response.Write("<SCRIPT>alert('" & mstrAlert & "');</" & "Script>")
                                End If
                            Else
                                .Columns("tcdBirthdate").Disabled = True
                                .Columns("cbeSexclien").Disabled = True
                                .Columns("chkSmoking").Disabled = mblnIntermedia
                                '                                .Columns("cbeTyperisk").Disabled = mblnIntermedia
                        End If
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    '% insPreCA025: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA025()
        '--------------------------------------------------------------------------------------------
        Dim lintPerson As Integer
        Dim lclsObject As Object
        Dim lsSmoking As String
	
        mcolRoleses = New ePolicy.Roleses
	
        If mcolRoleses.InsPreCA025(mstrCertype, mstrBranch, mstrProduct, mstrPolicy, mstrCertif, mstrEffecdate, mstrPolitype, mstrCompon, mstrTransaction, Session("nUserCode"), mstrBrancht) Then
            If mstrTransaction = 2 Then
                mobjGrid.ActionQuery = mcolRoleses.bNopayroll
            End If
            For Each Me.mclsRoles In mcolRoleses
                With mobjGrid
                    
                    '+ Se asignan los valores de las columnas del grid
                    .Columns("Sel").Disabled = mclsRoles.sSel = "2"
                    .Columns("cbeRole").EditRecord = True
                    .Columns("cbeRole").DefValue = CStr(mclsRoles.nRole)
                    .Columns("cbeRole").Descript = mclsRoles.sDesT12
                    '.Columns("cbeRole").OnChange = "insChangeRole(0,"& mclsRoles.nRole &");"
                    .Columns("hddsOldRole").DefValue = CStr(mclsRoles.nRole)
                    .Columns("tctCode").ClientRole = CStr(mclsRoles.nRole)
                    If mclsRoles.nRole = 13 Then
                        .Columns("tctCode").DefValue = mobjValues.TypeToString(mclsRoles.nIntermed, eFunctions.Values.eTypeData.etdDouble)
                        .Columns("tctCode").Digit = ""
                        .Columns("hddsInterClient").DefValue = mclsRoles.SCLIENT
                        .Columns("hddsOldCode").DefValue = mobjValues.TypeToString(mclsRoles.nIntermed, eFunctions.Values.eTypeData.etdDouble)
                        .Columns("btnQuery").HRefScript = "insShowAgentSequence('" & mobjValues.TypeToString(mclsRoles.nIntermed, eFunctions.Values.eTypeData.etdDouble) & "', lblnQuery);"
                        
                        If Session("sTypeUser") = "3" And mclsRoles.nIntermed <> eRemoteDB.Constants.intNull Then
                            .Columns("Sel").Disabled = True
                            .Columns("cbeRole").EditRecord = False
                            .EditRecordDisabled = True
                        End If
                    Else
                        .Columns("tctCode").DefValue = mclsRoles.SCLIENT
                        .Columns("hddsInterClient").DefValue = vbNullString
                        .Columns("hddsOldCode").DefValue = mclsRoles.SCLIENT
                        .Columns("tctCode").Digit = mclsRoles.sDigit
                        lintPerson = mclsRoles.nPerson_typ
                        .Columns("btnQuery").HRefScript = "insShowClientSequence('" & mstrBranch & "','" & mstrProduct & "','" & mclsRoles.nRole & "','" & mclsRoles.SCLIENT & "', lblnQuery,'','" & mclsRoles.sDigit & "','" & lintPerson & "');"
                    End If
				
                    If mclsRoles.SCLIENT = vbNullString Then
                        lsSmoking = vbNullString
                    Else
                        lsSmoking = mclsRoles.sSmoking
                    End If
				
                    mobjGrid.sEditRecordParam = "nRole=" & mclsRoles.nRole & "&sClient=" & mclsRoles.SCLIENT & "&nCoverPos=" & mclsRoles.nCoverPos & "&sSmoking=" & lsSmoking
                    .Columns("tctCode").Descript = mclsRoles.sCliename
                    .Columns("cbeTypename").DefValue = CStr(mclsRoles.nTypename)
                    .Columns("cbeTypename").Descript = mclsRoles.sDesT5592
                    '.Columns("tctItem").DefValue = mclsRoles.sItem
                    If mclsRoles.nTypename = 1 Then
                        .Columns("tctPrintName").Disabled = False
                    End If
                    .Columns("tctPrintName").DefValue = mclsRoles.sPrintName
				
                    '+Se definen las columnas que se muestran sólo si el producto es vida
                    If mstrBrancht = "1" Then
                        .Columns("tcdBirthdate").DefValue = CStr(mclsRoles.dBirthdate)
                        .Columns("cbeSexclien").DefValue = mclsRoles.sSexclien
                        .Columns("cbeSexclien").Descript = mclsRoles.sDesT18
                        .Columns("chkSmoking").Checked = CShort(mclsRoles.sSmoking)
                        .Columns("cbeTyperisk").DefValue = CStr(mclsRoles.nTyperisk)
                        .Columns("hddsReqAddress").DefValue = mclsRoles.sReqAddress
                        .Columns("cmdAddress").HRefScript = "ShowAddress('Grid', '" & mclsRoles.sReqAddress & "', '" & mclsRoles.nRole & "', '" & mclsRoles.SCLIENT & "', '" & mobjValues.sCodisplPage & "');"

                        .Columns("chkVIP").Checked = CShort(mclsRoles.sVIP)
                        .Columns("chksContinued").Checked = CShort(mclsRoles.sContinued)
                        .Columns("tcnRating").DefValue = CStr(mclsRoles.nRating)
                        .Columns("valContrat_Pay").DefValue = CStr(mclsRoles.nContrat_Pay)
                        If mclsRoles.nContrat_Pay > 0 Then
                            .Columns("valContrat_Pay").Descript = CStr(mclsRoles.nContrat_Pay)
                        Else
                            .Columns("valContrat_Pay").Descript = vbNullString
                        End If
                        .Columns("cbeStatusrol").DefValue = CStr(mclsRoles.nStatusrol)
                        .Columns("cbeStatusrol").Descript = mclsRoles.sDesT5561
                        .Columns("tcdContinue").DefValue = CStr(mclsRoles.dContinue)

                    End If
                    
                    .Columns("tctItem").DefValue = CStr(mclsRoles.sItem)
                    
                    .Columns("hddnCoverPos").DefValue = CStr(mclsRoles.nCoverPos)
                    .Columns("hddsRequire").DefValue = mclsRoles.sRequire
                    .Columns("hddnMaxRole").DefValue = CStr(mclsRoles.nMax_role)
                    .Columns("hddnExist").DefValue = mclsRoles.sSel
                    .Columns("Sel").OnClick = "InsChangeSel(this," & mclsRoles.nRole & ")"
                    
                    Response.Write(.DoRow)
                End With
            Next mclsRoles
		
            If mcolRoleses.nChange = 1 Then
                lclsObject = New ePolicy.ValPolicySeq
                Response.Write(lclsObject.RefreshSequence(mstrCodispl, mstrCertype, mstrBranch, mstrProduct, mstrPolicy, mstrCertif, mstrEffecdate, mstrBrancht, mstrPolitype, "No"))
                lclsObject = Nothing
            End If
        End If
	
        If mcolRoleses.bNopayroll And mstrTransaction = 2 Then
            If mcolRoleses.bFirst Then
                lclsObject = New ePolicy.AutoCharge
                Call lclsObject.AutoUpdGeneral(mstrCodispl, mstrCertype, mstrBranch, mstrProduct, mstrPolicy, mstrCertif, mobjValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdDouble), mstrPolitype, mstrEffecdate, Session("dNulldate"), mstrTransaction, Session("nUsercode"), mstrBrancht, Session("SessionId"), Session("sBussityp"), mobjValues.StringToType(Session("nType_amend"), eFunctions.Values.eTypeData.etdLong))
                lclsObject = Nothing
                Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & mstrCodispl & "&sGoToNext=Yes" & "';</" & "Script>")
            End If
        End If
        mcolRoleses = Nothing
    End Sub

    '% insPreCA025Upd: Se realiza el manejo del grid en la POPUP
    '------------------------------------------------------------------------------
    Private Sub insPreCA025Upd()
        '------------------------------------------------------------------------------
        Dim lintIntermedia As Integer
        Dim lstrClient As String
	
        If mstrAction = "Del" Then
            With Request
                Response.Write(mobjValues.ConfirmDelete(False))
                If mobjValues.StringToType(mstrRole, eFunctions.Values.eTypeData.etdDouble) = 13 Then
                    lstrClient = vbNullString
                    lintIntermedia = mobjValues.StringToType(.QueryString.Item("sCode"), eFunctions.Values.eTypeData.etdDouble, True)
                Else
                    lstrClient = .QueryString.Item("sCode")
                    lintIntermedia = eRemoteDB.Constants.intNull
                End If
                Call mclsRoles.InsPostCA025Upd(mstrAction, mstrTransaction, mobjValues.StringToType(.Form.Item("hddnExist"), eFunctions.Values.eTypeData.etdDouble), mstrCertype, mstrBranch, mstrProduct, mstrPolicy, mstrCertif, mobjValues.StringToType(mstrRole, eFunctions.Values.eTypeData.etdDouble), lstrClient, mstrEffecdate, lintIntermedia, mstrBrancht, Nothing, vbNullString, vbNullString, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mstrPolitype, mstrCompon, vbNullString, eRemoteDB.Constants.intNull, Session("nUsercode"), eRemoteDB.Constants.intNull, Session("dNulldate"), mobjValues.StringToType(.QueryString.Item("nCoverPos"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sInterClient"), .QueryString("sRequire"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, "2", "2")
			
                Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & mstrCodispl & "&sGoToNext=NO" & "';</" & "Script>")
                Response.Write(mobjGrid.DoFormUpd(mstrAction, "valPolicySeq.aspx", mstrCodispl, Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
            End With
        Else
		
            '+ Si el campo cliente tiene valor, se asigna valor false a la variable 
            If Request.QueryString.Item("sClient") <> vbNullString Then
                Response.Write("<SCRIPT>lblnShowDefValues=false;</" & "Script>")
            End If
		
            Response.Write(mobjGrid.DoFormUpd(mstrAction, "valPolicySeq.aspx", mstrCodispl, Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
            'If Request.QueryString("sClient") = vbNullString Then
            Response.Write("<SCRIPT>InsDefValues();OnClicksVip();</" & "Script>")
            'end if	
        End If
	
        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("var lintIndex=0" & vbCrLf)
        Response.Write("var larrlinks = document.links" & vbCrLf)
        Response.Write("for (lintIndex==0;lintIndex<larrlinks.length;lintIndex++){" & vbCrLf)
        Response.Write("    if (larrlinks[lintIndex].href.indexOf(""MoveRecord"")>=0)" & vbCrLf)
        Response.Write("        larrlinks[lintIndex].href = larrlinks[lintIndex].href.replace(""MoveRecord("",""MoveRecordCA025("")" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "SCRIPT>")

	
        mobjClient = Nothing
    End Sub

</script>
<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA025")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjClient = New eClient.Client
    lobjErrors = New eGeneral.GeneralFunction
    mobjUser = New eSecurity.User

    mstrAlert = "Err. 56000 " & lobjErrors.insLoadMessage(56000)

    lobjErrors = Nothing

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    Session("sCodispl")= mobjValues.sCodisplPage 
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mstrBrancht = Session("sBrancht")
    mstrProdclas = Session("nProdclas")
    mstrTransaction = Session("nTransaction")
    mstrCertif = Session("nCertif")
    mstrBranch = Session("nBranch")
    mstrProduct = Session("nProduct")
    mstrPolitype = Session("sPolitype")
    mstrCertype = Session("sCertype")
    mstrPolicy = Session("nPolicy")
    mstrEffecdate = Session("dEffecdate")

    mstrRole = Request.QueryString.Item("nRole")
    mstrType = Request.QueryString.Item("Type")
    mstrAction = Request.QueryString.Item("Action")
    mstrCodispl = Request.QueryString.Item("sCodispl")

    mstrSmoking = Request.QueryString.Item("sSmoking")



    If mstrCertif = "0" Then
        mstrCompon = "1"
    Else
        mstrCompon = "2"
    End If
    mobjValues.ActionQuery = Session("bQuery")

    If mobjUser.Find(Session("nUsercode")) Then
        'mblnIntermedia = mobjUser.sType = "3" '+ Intermediario
        If mobjUser.sType = "3" Then
            mblnIntermedia = True
        Else
            mblnIntermedia = False
        End If
    Else
        mblnIntermedia = False
    End If

    bDisabledByLevels = False
    mobjProduct = New eProduct.Product
    mobjSecur_sche = New eSecurity.Secur_sche
    If mobjProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
        If mobjSecur_sche.GetLevelsByTransac(Session("sSche_code"), "2", Request.QueryString.Item("sCodispl")) Then
            If mobjSecur_sche.nAmelevel >= mobjProduct.nChUserLev Then
                bDisabledByLevels = True
            End If
        End If
    End If

    mobjProduct = Nothing
    mobjSecur_sche = Nothing
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />
    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        Response.Write(mobjValues.StyleSheet())
        If mstrType <> "PopUp" Then
            Response.Write(mobjMenu.setZone(2, "CA025", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
            mobjMenu = Nothing
            Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
        End If
    %>
    <script>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 4-04-06 14:00 $|$$Author: Clobos $"

	var lblnShowDefValues = true;
	var Brancht= '<%=mstrBrancht%>'; 
	var Transac= '<%=mstrTransaction%>';
//-------------------------------------------------------------------------------------------
function changeName(Field)
//-------------------------------------------------------------------------------------------
{
	with(self.document.forms[0])
	{ 
		if (Field.value==1)
		{
			if (typeof(lblCliename.innerHTML)!='undefined') tctPrintName.value = lblCliename.innerHTML;
			tctPrintName.disabled = false;
		}
		else
		{
			tctPrintName.value = '';
			tctPrintName.disabled = true;
		}
	}
}

//%DisabledItem: Desabilita los campos de cuando es asegurado
//-------------------------------------------------------------------------------------------
function changeSmoking(Field)
	{
	if (Field.checked)
		{
//		self.document.forms[0].cbeTyperisk.value='4';
//		self.document.forms[0].cbeTyperisk.disabled=true;
		}
	else
		{
//		self.document.forms[0].cbeTyperisk.value='3';
//		self.document.forms[0].cbeTyperisk.disabled=false;
		}
}
//%DisabledItem: Desabilita los campos de cuando es asegurado
//-------------------------------------------------------------------------------------------
function DisabledItem(){
//-------------------------------------------------------------------------------------------
	var Type='<%=mstrType%>';
	var Certif='<%=mstrCertif%>';
    var nRoleValue = self.document.forms[0].cbeRole.value
 	 	
 	with(self.document.forms[0]){		
		if (Type=="PopUp"){
			if (cbeTypename.value==1) 
			{
				tctPrintName.disabled = false;
			}
			else
			{
				tctPrintName.disabled = true;
			}
		    if (Brancht==1)
		    {
            if (Certif == '0')
            //+ Contratante, Administrador de póliza y Administrador de recaudación
               if (nRoleValue == '1' || nRoleValue == '71' || nRoleValue == '73')
                  valContrat_Pay.disabled=false;
               else
                  valContrat_Pay.disabled=true;
            else
               valContrat_Pay.disabled=true;
            }
			if ((nRoleValue == '2'  || nRoleValue == '7' || nRoleValue == '21' || 
			     nRoleValue == '22' || nRoleValue == '23' || nRoleValue == '24' || 
			     nRoleValue == '60') && Brancht == '1' && (Transac == '12' || Transac == '26'))
			{
				tctCode.disabled=false;
				tctCode_Digit.disabled=false;				
				cbeRole.disabled=false;
				cbeTypename.disabled=false;
				tcdBirthdate.disabled=false;
				cbeSexclien.disabled=false;
				
				if(!<%=LCase(mblnIntermedia)%>) 
					{
					chkSmoking.disabled=false;
					//cbeTyperisk.disabled=false;
					}
				else	
					{
					chkSmoking.disabled=true;
					//cbeTyperisk.disabled=true;
					}
				chksContinued.disabled=false;
				chkVIP.disabled=false;
				tcnRating.disabled=false;
				cbeStatusrol.disabled=false;
				if(nRoleValue == '2')
				    alert('<%=mstrAlert%>');
			}
			else{
				tcdBirthdate.disabled=true;
				cbeSexclien.disabled=true;
				chkSmoking.disabled=true;
			}
		}
	}
}        

// % insShowClientSequence: Invoca dentro de una PopUp a la secuencia de clientes
//-------------------------------------------------------------------------------------------
function insShowClientSequence(nBranchCode, nProductCode, sRoleCode, sClientCode, blnQuery, nMainAction, sDigit, nPerson_typ){
//-------------------------------------------------------------------------------------------
    nMainAction = (blnQuery?401:302)
    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sCodispl=BC003_K&sModule=Client&sProject=ClientSeq&sRoleCode='+sRoleCode+'&sClientCode='+sClientCode+'&nMainAction='+nMainAction+'&sDigit='+sDigit+'&nPerson_typ='+nPerson_typ+'&sOriginalForm=CA025', 'ClientSeq', 750, 500, 'no', 'yes', 20, 20)
}

// % insShowAgentSequence: Invoca dentro de una PopUp a la secuencia de intermediarios
//-------------------------------------------------------------------------------------------
function insShowAgentSequence(sClientCode, blnQuery, nMainAction){
//-------------------------------------------------------------------------------------------
    nMainAction = (blnQuery?401:302)
    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Agent&sProject=AgentSeq&sCodispl=AG001_K&sAgentCode='+sClientCode+'&nMainAction='+nMainAction+'&sOriginalForm=CA025', 'AgentSeq', 750, 500, 'no', 'yes', 20, 20)
}

// % InsChangeClient: Despliega los datos del cliente
//-------------------------------------------------------------------------------------------
function InsChangeClient(){
//-------------------------------------------------------------------------------------------
	var lstrAction = '<%=mstrAction%>'
	var lstrIntermedia 
	
	if ('<%=mblnIntermedia%>' == 'False'){
	    lstrIntermedia = '2';
	}    
	else{    
        lstrIntermedia = '1';
    }           
        
//+ Si la acción es agregar siempre se habilita la variable para llamar al showDefValues.
	if (lstrAction=='Add') 
		lblnShowDefValues=true;

//+ Esto con la idea de que la primera vez que entre a la popup no lo haga
    if (lblnShowDefValues==true){
		with (self.document.forms[0])
		{
			if (cbeRole.value != '13' && Brancht == '1')
			{
			    insDefValues('ClientRoles', "sClient=" + tctCode.value +
			                                "&nRole=" + cbeRole.value +
			                                "&sIntermedia=" + lstrIntermedia, '/VTimeNet/Policy/PolicySeq');
				if (Brancht == '1' && lstrIntermedia == '2')
				{
					chkSmoking.disabled=false;
				}
			}
		}
    } else 
		{
		 lblnShowDefValues=true;
         
         if (typeof(self.document.forms[0].chkSmoking)!='undefined'){
//		    if (self.document.forms[0].chkSmoking.checked)	self.document.forms[0].cbeTyperisk.disabled=true;
		    }
        }
		
}

// % OnClicksVip: Despliega los datos del cliente
//-------------------------------------------------------------------------------------------
function OnClicksVip(){
//-------------------------------------------------------------------------------------------
	
    if (typeof(self.document.forms[0].tcdContinue)!='undefined' && typeof(self.document.forms[0].chksContinued)!='undefined'){
        if (self.document.forms[0].chksContinued.checked){
		    self.document.forms[0].tcdContinue.disabled = false;
		    self.document.forms[0].btn_tcdContinue.disabled = false;
	    }
	    else{
		    self.document.forms[0].tcdContinue.disabled = true;
		    self.document.forms[0].btn_tcdContinue.disabled = true;
		    self.document.forms[0].tcdContinue.value='';
	    }
    }
}

//% InsDefValues: Se asignan valores segun el ramo
//--------------------------------------------------------------------------
function InsDefValues(){
//--------------------------------------------------------------------------
    with (self.document.forms[0]){
        cbeRole.value = '<%=mstrRole%>';
        cbeRole.tag   = cbeRole.value
        
      if (Brancht == '1'){
			if (tcnRating.value == '') tcnRating.value = 100;
			if (cbeStatusrol.value == '' && cbeRole.value != 13) cbeStatusrol.value = 1;
            if (cbeRole.value == '2' && cbeTyperisk.value == '' ) cbeTyperisk.value = 1;
		  
	    };
    };
}
//% MoveRecordCA025: Calcula la posición
//--------------------------------------------------------------------------
 function MoveRecordCA025(Option){
//--------------------------------------------------------------------------
    var lintIndex = CurrentIndex;
    switch (Option){
       case "Back":
            lintIndex--; 
            break;
       case "Next":
            lintIndex++;
    }
    
    if (lintIndex >= 0)
        if (lintIndex < top.opener.marrArray.length){
            CurrentIndex = lintIndex
            insChangeRole(CurrentIndex,top.opener.marrArray[CurrentIndex].cbeRole)
        }
}
//% insChangeRole: Cambia el QueryString 
//--------------------------------------------------------------------------
function insChangeRole(sIndex,sRole) {
//--------------------------------------------------------------------------
    var lstrHref = ""
    var lstrRole = '<%=mstrRole%>'
    
    if (typeof sRole =='undefined'){
        sRole = document.forms[0].cbeRole.value
    }
    lstrHref = document.location.href.replace(/&nRole=\d+/,"") + '&nRole=' + sRole
    if (typeof sIndex !='undefined'){
        lstrHref = lstrHref.replace(/&Index=\d+/,"") + '&Index=' + sIndex
    }   
    document.location.href = lstrHref
}
function ShowAddress(sType, sReqAddress, nRole, sClient, sCodispl){

    if (sType == 'Grid'){
        if (sReqAddress == '1' && sClient != '' || (sReqAddress =='2' && sClient != '' )){
            ShowPopUp('/VTimeNet/Common/SCA001PopUp.aspx?sCodispl=SCA109&nMainAction=302&sOnSeq=2&sClient=' + sClient + '&nRole=' + nRole, 'ShowAddress', 1000, 700, 'yes', 'no', 'no', 'no');
            //ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA109&nMainAction=302&sOnSeq=2&sClient=' + sClient + '&nRole=' + nRole, 'ShowAddress', 1000, 700, 'yes', 'no', 'no', 'no');
        }
    }
    else{
        with (self.document.forms[0]){
            if (hddsReqAddress.value == '1' && tctCode.value != ''){
                ShowPopUp('/VTimeNet/Common/SCA001PopUp.aspx?sCodispl=SCA109&nMainAction=302&sOnSeq=2&sClient=' + tctCode.value + '&nRole=' + cbeRole.value, 'ShowAddress', 1000, 700, 'yes', 'no', 'no', 'no');
                //ShowPopUp('/VTimNet/Common/SCA001.aspx?sCodispl=SCA109&nMainAction=302&sOnSeq=2&sClient=' + tctCode.value + '&nRole=' + cbeRole.value, 'ShowAddress', 1000, 700, 'yes', 'no', 'no', 'no');
            }
        }
    }
}
    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" name="CA025" action="valPolicySeq.aspx?sMode=1">
    <%
        Response.Write(mobjValues.ShowWindowsName("CA025", Request.QueryString.Item("sWindowDescript")))
        Response.Write(mobjValues.HiddenControl("hddsCompon", mstrCompon))
        Call insDefineHeader()

        If mstrType = "PopUp" Then
            Call insPreCA025Upd()
        Else
            Call insPreCA025()
        End If
        mobjValues = Nothing
        mclsRoles = Nothing
        mobjGrid = Nothing
    %>
    </form>
</body>
</html>
<script>
    //%InsChangeSel: Se envía mensaje de validación al no poder eliminar un registro
    //------------------------------------------------------------------------------
    function InsChangeSel(Field, sInd) {
        //------------------------------------------------------------------------------

        if (Field.checked && sInd == "2") {
            if (Brancht == "1" && (Transac == '12' || Transac == '26')) {
                alert('<%=mstrAlert%>');
                Field.checked = false
            }
        }
    }
</script>
<%  '^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Call mobjNetFrameWork.FinishPage("CA025")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>