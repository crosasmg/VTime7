<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eProduct" %>

<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

    
'-----------------------------------------------------------------------------
'- Contador de número de registros
Dim mintTotalRecordsCount As Integer

'- Contador del número de registros insertados en la página
Dim mlngOptionalBeginProcess As Object

'- Primer y último nombre mostrado en cada página.
Dim lsFirstRecord As Object
Dim lsLastRecord As Object

'- Indica el movimiento a efectuar para la búsqueda de los datos. (Next o Previous)    
Dim lsWay As Object

'- Cantidad máxima de elementos por página.
Const CN_MAXRECORDS As Short = 50

'+ Número de página que se está mostrando
Dim PageNumber As Object

'+ Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean
'-----------------------------------------------------------------------------

Dim mintBranch As Integer
Dim mintProduct As Integer
Dim mlngPolicy As Integer
Dim mlngCertif As Integer
Dim mlngDocument As Integer
Dim mintDraft As Integer
Dim mintCurrency As Integer
Dim mdtmValuedate As Date

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lintExists As Object

    Dim lclsGeneral As eGeneral.OptionsInstallation
Dim lclsT_DocTyp As Object
Dim lcolT_DocTyps As eCollection.T_DocTyps

    
    '+ insDefineHeader:Se define el encabezado del grid
    '-----------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjGrid.sCodisplPage = "CO001"
	
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	With mobjGrid
		.ActionQuery = (CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery))
		.MoveRecordScript = "insChangeTypeDoc(self.document.forms[0].cbeCollecDocTyp, 'Update');"
		With .Columns
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeCollecDocTypColumnCaption"), "cbeCollecDocTyp", "table5587", 1,  ,  ,  ,  ,  , "insChangeTypeDoc(this, ""Add"");", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("cbeCollecDocTypColumnToolTip"))
			
			lobjColumn.TypeList = CShort("1")
			Select Case Session("chkRentVital")
				Case "10"
					If CStr(Session("sRel_Type")) = "2" Then
						'+Nota: Antes en este caso solo cargaba en la lista el valor 16
						lobjColumn.List = "11,12,13,14,15,16"
					Else
						lobjColumn.List = "11,12,13,14,15,16"
					End If
				Case "9"
					If CStr(Session("sRel_Type")) = "2" Then
						lobjColumn.List = "11,12,13,14,15"
					Else
						lobjColumn.List = "11,12,13,14,15,16"
					End If
				Case "0"
					lobjColumn.List = "1,2,3,4,5,6,7,8,9,18,19,20,21,22,23,24"
			End Select
			If CStr(Session("sRel_Type")) = "2" Then
				Select Case Session("nProdClas")
					Case "4"
						lobjColumn.List = "1,18,19,20"
				End Select
			End If
            lobjColumn.BlankPosition = True
			Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),  , vbNullString,  ,  ,  , True)
			Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"),  , CStr(eRemoteDB.Constants.intNull), 5,  ,  , "insSetOrigin()", True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyColumnToolTip"), False,  ,  ,  , "insShowPolicyInf();", True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifColumnToolTip"),  ,  ,  ,  , "insDisabledCertif();", True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnDocumentColumnCaption"), "tcnDocument", 10, vbNullString,  , GetLocalResourceObject("tcnDocumentColumnToolTip"),  ,  ,  ,  , "(self.document.forms[0].cbeCollecDocTyp.value==7?insDisabledCertif():ShowDocument());", True)
			'Call .AddNumericColumn (0,"Documento","tcnDocument", 10,vbnullstring,,"Número de documento",,,,,"self.document.forms[0].cbeCollecDocTyp.value==7;ShowDocument();",True)
			
			If Request.QueryString.Item("Type") <> "PopUp" Then
				Call .AddAnimatedColumn(0, "", "btnSCO001", "/VTimeNet/Images/btn_ValuesOff.png", GetLocalResourceObject("btnSCO001ColumnToolTip"),  , "ShowDataSCO001()")
			End If
			
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, vbNullString,  , GetLocalResourceObject("tcnDraftColumnToolTip"),  ,  ,  ,  , "ShowDocument();", True)
			
                lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valCodeColumnCaption"), "valCode", "TABTAB_SCOLLECT", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "ShowDocument();", True, 10, GetLocalResourceObject("valCodeColumnToolTip"))
			
			If Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nProduct") <> vbNullString And Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nCertif") <> vbNullString Then
				lobjColumn.Parameters.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nPolicy", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nCertif", mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nAction", Session("CO001_nAction"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				lobjColumn.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				lobjColumn.Parameters.Add("nAction", Session("CO001_nAction"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			lobjColumn.Parameters.ReturnValue("nAmount",  ,  , True)
			
			Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"), "ChangeValues()", True, "lblCliename", False,  ,  ,  ,  ,  ,  , True,  ,  ,  , "/VTimeNet/Common/PremiumQuery.aspx")
			
			If Request.QueryString.Item("Type") <> "PopUp" Then
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnProponumColumnCaption"), "tcnProponum", 10, vbNullString,  , GetLocalResourceObject("tcnProponumColumnToolTip"),  ,  ,  ,  ,  , True)
			End If
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColColumnCaption"), "tcnAmountCol", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColColumnToolTip"), True, 6,  ,  ,  , True)
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", 1,  ,  ,  ,  ,  , "insExchange();", True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
			Call .AddDateColumn(0, GetLocalResourceObject("tcdValuedateColumnCaption"), "tcdValuedate", "",  , GetLocalResourceObject("tcdValuedateColumnToolTip"),  ,  , "insExchange();", False)
			
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 11, CStr(0),  , GetLocalResourceObject("tcnExchangeColumnCaption"), True, 6,  ,  ,  , True)
			
			'/********************************************CC-1097**************************************************************/ 
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountPayColumnCaption"), "tcnAmountPay", 18, CStr(0), , GetLocalResourceObject("tcnAmountPayColumnToolTip"), True, 6, , , "insCalculateLocal('Amount');", CStr(Session("chkRentVital")) <> "9" And CStr(Session("chkRentVital")) <> "10", , True)
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountLocColumnCaption"), "tcnAmountLoc", 12, CStr(0), , GetLocalResourceObject("tcnAmountLocColumnToolTip"), True, 0, , , "insCalculateLocal('AmountLoc');", CStr(Session("chkRentVital")) <> "9" And CStr(Session("chkRentVital")) <> "10", , True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnInterest_rateColumnCaption"), "tcnInterest_rate", 18, CStr(0),  , GetLocalResourceObject("tcnInterest_rateColumnToolTip"), True, 6,  ,  , "insCalculateLocal('Interest');", CStr(Session("chkRentVital")) <> "9" And CStr(Session("chkRentVital")) <> "10")
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnInterestLocColumnCaption"), "tcnInterestLoc", 12, CStr(0),  , GetLocalResourceObject("tcnInterestLocColumnToolTip"), True, 0,  ,  , "insCalculateLocal('InterestLoc');", CStr(Session("chkRentVital")) <> "9" And CStr(Session("chkRentVital")) <> "10")
			'/*******************************************Fin del CC-1097*******************************************************/             
			
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnTax_discountColumnCaption"), "tcnTax_discount", 6, CStr(0),  , GetLocalResourceObject("tcnTax_discountColumnToolTip"), True, 2)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnface_valueColumnCaption"), "tcnface_value", 12, CStr(0),  , GetLocalResourceObject("tcnface_valueColumnToolTip"), True, 0)
			Call .AddDateColumn(0, GetLocalResourceObject("tcdIssuedateColumnCaption"), "tcdIssuedate", "",  , GetLocalResourceObject("tcdIssuedateColumnToolTip"))
			Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdateColumnCaption"), "tcdExpirdate", "",  , GetLocalResourceObject("tcdExpirdateColumnToolTip"))
			
			'+ Cuando el documento de cobro sea un aporte de contribución a una póliza Unit Linked (APV), 
			'+ se debe indicar el origen del depósito.
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valOriginColumnCaption"), "valOrigin", "tab_origin", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , CStr(Session("nProdclas")) <> "4",  , GetLocalResourceObject("valOriginColumnToolTip"))
                If Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nProduct") <> vbNullString Then
                    lobjColumn.Parameters.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nCollecdoctyp", mobjValues.StringToType(Request.QueryString.Item("nCollecdoctyp"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                Else
                    lobjColumn.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nCollecdoctyp", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If


			
			Call .AddDateColumn(0, GetLocalResourceObject("tcdOriginDateColumnCaption"), "tcdOriginDate", "",  , GetLocalResourceObject("tcdOriginDateColumnToolTip"),  ,  ,  , CStr(Session("nProdclas")) <> "4")
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valInstitutionColumnCaption"), "valInstitution", "TabTab_Fn_Institu", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valInstitutionColumnToolTip"))
			
			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAmountLocDecColumnCaption"), "tcnAmountLocDec", 19, CStr(0),  , GetLocalResourceObject("tcnAmountLocDecColumnToolTip"), True, 6,,,,,,true)
			lobjColumn.GridVisible = False

                lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeTyp_ProfitColumnCaption"), "cbeTyp_Profit", "Tab_Table950", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , , GetLocalResourceObject("cbeTyp_ProfitColumnToolTip"))
                If Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nProduct") <> vbNullString And Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nCertif") <> vbNullString Then
                    lobjColumn.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nPolicy", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nCertif", mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    lobjColumn.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    lobjColumn.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If

            Call .AddCheckColumn(0, GetLocalResourceObject("chkNewReceiptColumnCaption"), "chkNewReceipt", vbNullString, , , , False, GetLocalResourceObject("chkNewReceiptColumnToolTip"))
			
			Call .AddHiddenColumn("hddsCertype", "2")
			Call .AddHiddenColumn("hddnSequence", CStr(0))
			Call .AddHiddenColumn("hddnBulletins_aux", CStr(0))
			Call .AddHiddenColumn("hddnProponum_aux", CStr(0))
			Call .AddHiddenColumn("hdddExpirDat", CStr(eRemoteDB.Constants.dtmNull))
			Call .AddHiddenColumn("hddnType", CStr(0))
			Call .AddHiddenColumn("hddnTratypei", CStr(0))
			Call .AddHiddenColumn("hddnContrat", CStr(0))
			Call .AddHiddenColumn("hddsAction", Request.QueryString.Item("Action"))
			Call .AddHiddenColumn("hddRow", CStr(0))
			Call .AddHiddenColumn("hddnReceipt", CStr(0))
			Call .AddHiddenColumn("hddnExist", CStr(0))
			Call .AddHiddenColumn("hddProdClas", CStr(0))
			Call .AddHiddenColumn("hddsApv", "0")
			
		End With
		
		.FieldsByRow = 2
		.Top = 50
		.Left = 10
		.Height = 570
		.Width = 770
		.Codispl = "CO001"
		.AddButton = True
		.DeleteButton = True 'False
		.Columns("Sel").GridVisible = Not (CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery))
		.sEditRecordParam = "lsWay=" & Request.QueryString.Item("lsWay") & "&lsFirstRecord=" & Request.QueryString.Item("lsFirstRecord")
            '    .sEditRecordParam = .sEditRecordParam & "&nBranch=" '+ marrArray[lintIndex].cbeBranch + '" & "&nProduct='+marrArray[lintIndex].valProduct + '" & "&nPolicy='+marrArray[lintIndex].tcnPolicy + '" & "&nCertif='+marrArray[lintIndex].tcnCertif + '"
            
            .sDelRecordParam = "nSequence=' + marrArray[lintIndex].hddnSequence + '"
		
		'+ Permite continuar si el check está marcado        
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub
'+ insPreCO001upd: Actualiza un dato del registro
'--------------------------------------------------------------------------
Private Sub insPreCO001Upd()
	'--------------------------------------------------------------------------
	If Request.QueryString.Item("Action") = "Del" Then
		Call insDelItem()
		Response.Write(mobjValues.ConfirmDelete())
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValCollectionSeq.aspx", "CO001", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT> insChangeTypeDoc(self.document.forms[0].cbeCollecDocTyp, '" & Request.QueryString.Item("Action") & "'); </" & "Script>")
	Else
        If Request.QueryString.Item("Action") = "Add" Then
		    Response.Write("<SCRIPT>insChangeTypeDoc('0', 'Update');</" & "Script>")
        End If                
	End If
End Sub
'+ insPreCO001: Funcion que carga los valores del Grid
'------------------------------------------------------------------------
Private Sub insPreCO001()
	'------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim sFind As String
	
	'+ Se inicializan las variables si estas no poseen valor.
	mintTotalRecordsCount = 0
	
	If lsFirstRecord = vbNullString Then
		lsFirstRecord = 1
	End If
	
	If lsLastRecord = vbNullString Then
		lsLastRecord = lsFirstRecord + CN_MAXRECORDS - 1
	End If
	
	'+ Se inicializa el número de página mostrado.       
	PageNumber = 1
	
	'+ Según el tipo de movimiento realizado se cargan el primer y el último registro.
	If Request.QueryString.Item("lsWay") = "Next" Then
		If Request.QueryString.Item("lsFirstRecord") <> vbNullString Then
			lsFirstRecord = Request.QueryString.Item("lsFirstRecord")
		Else
			lsFirstRecord = CDbl(Request.Form.Item("lsLastRecord")) + 1
		End If
		lsLastRecord = lsFirstRecord + CN_MAXRECORDS - 1
	ElseIf Request.QueryString.Item("lsWay") = "Back" Then 
		If Request.QueryString.Item("lsFirstRecord") <> vbNullString Then
			lsFirstRecord = Request.QueryString.Item("lsFirstRecord")
		Else
			lsFirstRecord = CDbl(Request.Form.Item("lsFirstRecord")) - CN_MAXRECORDS
		End If
		lsLastRecord = lsFirstRecord + CN_MAXRECORDS - 1
	End If
	
	If Request.QueryString.Item("lsWay") = vbNullString Then
		'+Realiza el proceso completo.
		sFind = "1"
	Else
		'+Lee solo la tabla temporal ya que los registros estan cargados 
		sFind = "2"
	End If
	
	'+ Se definen las propiedades generales del grid
	lcolT_DocTyps = New eCollection.T_DocTyps
	
	lintExists = 0
	
	If lcolT_DocTyps.findCO001(Session("CO001_nAction"), Session("sReceiptNum"), Session("sPolicynum"), mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate), Session("sRel_Type"), mobjValues.StringToType(Session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), Session("sStatus"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sClient"), mobjValues.StringToType(Session("nAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dValuedate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nOneTime"), eFunctions.Values.eTypeData.etdDouble), Session("chkRentVital"), mobjValues.StringToType(lsFirstRecord, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lsLastRecord, eFunctions.Values.eTypeData.etdDouble), sFind, mobjValues.StringToType(Session("dCollect"), eFunctions.Values.eTypeData.etdDate), Session("sValueDateAll")) Then
		
		Session("nOneTime") = 0
		
		
		mintTotalRecordsCount = lcolT_DocTyps.nSequence_Total
		If mintTotalRecordsCount > 0 Then
			'+ Se obtiene el número del primer elemento de la página.
			If CDbl(Request.QueryString.Item("BeginProcess")) = 1 Or Request.Form.Item("mlngOptionalBeginProcess") = vbNullString Then
				mlngOptionalBeginProcess = 1
			Else
				mlngOptionalBeginProcess = Request.Form.Item("mlngOptionalBeginProcess")
			End If
			
			Call ShowRecords()
		End If
	Else
		mblnDisabledBack = True
		mblnDisabledNext = True
	End If
	Response.Write(mobjGrid.closeTable)
	'+ Se incluyen los botones Back y Next en la página.    
	Response.Write(mobjValues.ButtonBackNext( , mblnDisabledBack, mblnDisabledNext))
	
	lcolT_DocTyps = Nothing
End Sub

'% ShowRecords: Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Private Sub ShowRecords()
	'Dim ShowTotals() As Object
	'--------------------------------------------------------------------------------------------
	Dim lintRecordShow As Integer
	Dim lintRecordIndex As Short
	Dim lstrChains As String
	Dim lintIndex As Short
	Dim lcolProduct As eProduct.Product
	Dim sApv As String
	
	'+ Se definen las propiedades generales del grid
	lcolProduct = New eProduct.Product
	
	
	'+ Estableciendo valores iniciales.    
	lintRecordShow = 0
	lstrChains = ""
	mblnDisabledBack = False
	mblnDisabledNext = False
	
	sApv = "2"
	
	If Request.QueryString.Item("BeginProcess") = vbNullString Then
		
		'+ Establece el número de página a mostrar.
		If Request.Form.Item("PageNumber") = vbNullString Then
			PageNumber = 0
		Else
			PageNumber = Request.Form.Item("PageNumber")
		End If
	Else
		PageNumber = 0
	End If
	
	'+ Según el tipo de movimiento realizado se establecen las acciones a tomar
	If Request.QueryString.Item("lsWay") = vbNullString Or Request.QueryString.Item("lsWay") = "Next" Then
		PageNumber = PageNumber + 1
	ElseIf Request.QueryString.Item("lsWay") = "Back" Then 
		mlngOptionalBeginProcess = mlngOptionalBeginProcess - (mlngOptionalBeginProcess - lsFirstRecord)
		PageNumber = PageNumber - 1
		
		'+ Si el número de la página es menor a cero, se asume que se encuentra en la primera página.
		If PageNumber <= 0 Then
			PageNumber = 1
		End If
	End If
	
	With mobjGrid
		lintIndex = 0
		lintRecordIndex = 0
		For	Each lclsT_DocTyp In lcolT_DocTyps
			lintRecordIndex = lintRecordIndex + 1
			
			'+ Si se trata de un recibo y además, este no está financiado se dará accesso a "Datos de verificación"
			
			If lclsT_DocTyp.nCollecDocTyp = 1 And lclsT_DocTyp.nContrat = 0 Then
				.Columns("btnSCO001").HRefScript = "ShowDataSCO001(" & CStr(lintIndex) & ")"
			Else
				.Columns("btnSCO001").HRefScript = ""
			End If
			
			If lclsT_DocTyp.sSel = "1" Then
				.Columns("Sel").Checked = CShort("1")
				lintExists = lintExists + 1
			Else
				.Columns("Sel").Checked = CShort("0")
			End If
			
			If lclsT_DocTyp.nCollecDocTyp <> eRemoteDB.Constants.intNull Then
				.Columns("cbeCollecDocTyp").DefValue = lclsT_DocTyp.nCollecDocTyp
				.Columns("cbeCollecDocTyp").Descript = lclsT_DocTyp.sCollecDocTyp
			End If
			
			.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
			
			
			If lclsT_DocTyp.nBranch <> eRemoteDB.Constants.intNull Then
				If lcolProduct.FindProduct_li(lclsT_DocTyp.nBranch, lclsT_DocTyp.nProduct, Session("dCollectDate"), True) Then
					sApv = lcolProduct.sApv
					
				End If
				.Columns("hddsApv").DefValue = sApv
				.Columns("cbeBranch").DefValue = lclsT_DocTyp.nBranch 'Ramo
				.Columns("cbeBranch").Descript = lclsT_DocTyp.sBranch 'Ramo
				.Columns("valProduct").DefValue = lclsT_DocTyp.nProduct 'Producto
				.Columns("valProduct").Descript = lclsT_DocTyp.sProduct 'Producto
				.Columns("valProduct").Descript = lclsT_DocTyp.sProduct 'Producto
			End If
			
			If lclsT_DocTyp.nPolicy >= 0 Then
				.Columns("tcnPolicy").DefValue = lclsT_DocTyp.nPolicy 'Póliza
			Else
				.Columns("tcnPolicy").DefValue = vbNullString
			End If
			
			.Columns("tcnCertif").DefValue = lclsT_DocTyp.nCertif 'Certificado
			.Columns("hddnContrat").DefValue = lclsT_DocTyp.nContrat 'Contrato
			.Columns("hddnReceipt").DefValue = lclsT_DocTyp.nDocument 'Contrato
			
			If lclsT_DocTyp.nDraft >= 0 Then
				.Columns("tcnDraft").DefValue = lclsT_DocTyp.nDraft 'Cuota
			Else
				.Columns("tcnDraft").DefValue = vbNullString
			End If
			
			.Columns("tcnDocument").DefValue = lclsT_DocTyp.nDocument 'Nùmero de documento
			
			'+ Cuando se trata de un prestamo
			If lclsT_DocTyp.nCollecDocTyp = 6 Then
				
				.sEditRecordParam = "nBranch='+" & lclsT_DocTyp.nBranch & "  + " & "'&nProduct='+" & lclsT_DocTyp.nProduct & "  + " & "'&nPolicy='+ '" & lclsT_DocTyp.nPolicy & "' + " & "'&nCertif='+ '" & lclsT_DocTyp.nCertif
				.Columns("valCode").Parameters("nBranch").Value =lclsT_DocTyp.nBranch
				.Columns("valCode").Parameters("nProduct").Value=lclsT_DocTyp.nProduct
				.Columns("valCode").Parameters("nPolicy").Value=lclsT_DocTyp.nPolicy
				.Columns("valCode").Parameters("nCertif").Value=lclsT_DocTyp.nCertif
				.Columns("valCode").Parameters("nAction").Value=Session("CO001_nAction")
				.Columns("valCode").DefValue = lclsT_DocTyp.nDocument 'Nùmero de documento
			Else
				.Columns("valCode").DefValue = CStr(eRemoteDB.Constants.intNull)
			End If
			
			.Columns("cbeCurrency").DefValue = lclsT_DocTyp.nCurrency 'Moneda original del documento
			.Columns("cbeCurrency").Descript = lclsT_DocTyp.sCurrency 'Moneda original del documento
			.Columns("tcnExchange").DefValue = lclsT_DocTyp.nExchange 'Factor de cambio
			.Columns("tcnAmountCol").DefValue = lclsT_DocTyp.nAmountCol 'Monto pendiente del documento
			.Columns("tcnAmountLoc").DefValue = lclsT_DocTyp.nLocalamount 'Monto a pagar en moneda local
			.Columns("tcnAmountLocDec").DefValue = lclsT_DocTyp.nLocalamountDec 'Monto a pagar en moneda local con decimales
			.Columns("tcnProponum").DefValue = lclsT_DocTyp.nProponum 'Propuesta asociado al recibo
			.Columns("hddnSequence").DefValue = lclsT_DocTyp.nSequence 'Secuencia
			.Columns("dtcClient").DefValue = lclsT_DocTyp.sClient 'Cliente asociado al documento
			.Columns("dtcClient").Digit = lclsT_DocTyp.sDigit 'Dígito verificador del cliente asociado al documento
			.Columns("dtcClient").Descript = lclsT_DocTyp.sCliename 'Cliente asociado al documento
			.Columns("hddnType").DefValue = lclsT_DocTyp.nType 'Tipo 1)Cobro 2)Devolución
			.Columns("hddnTratypei").DefValue = lclsT_DocTyp.nTratypei 'Origen del recibo
			
			.Columns("tcnAmountPay").DefValue = lclsT_DocTyp.nAmountPay 'Monto a pagar			
			
			.Columns("tcnInterest_rate").DefValue = lclsT_DocTyp.nInterest_rate 'Interes
			.Columns("tcnInterestLoc").DefValue = lclsT_DocTyp.nLocalInterest 'Importe de interes de mora en moneda local
			
			.Columns("tcdValueDate").DefValue = lclsT_DocTyp.dValueDate ' Fecha de valorización
			'+ Solo se asigan valor a estos campos cuando se trate de RV y el tipo de documento sea el permitido            
			If (lclsT_DocTyp.nProdClas = 9 Or lclsT_DocTyp.nProdClas = 10) And (lclsT_DocTyp.nCollecDocTyp = 13 Or lclsT_DocTyp.nCollecDocTyp = 14 Or lclsT_DocTyp.nCollecDocTyp = 15) Then
				.Columns("tcnTax_discount").DefValue = lclsT_DocTyp.nRate_disc 'Interes
				.Columns("tcnface_value").DefValue = lclsT_DocTyp.nNom_valbon
				.Columns("tcdIssuedate").DefValue = lclsT_DocTyp.dIssuedatbon
				.Columns("tcdExpirdate").DefValue = lclsT_DocTyp.dExpirdatbon
			End If
			
			.Columns("tcnDocument").EditRecord = (lclsT_DocTyp.nCollecDocTyp <> 3 And lclsT_DocTyp.nCollecDocTyp <> 7)
			
                If lclsT_DocTyp.nProdClas = 4 Then
                    '+ Si se tienen datos de ramo y producto se verifica si dicho documento es de APV.
                    .Columns("valOrigin").DefValue = lclsT_DocTyp.nOrigin
                    .Columns("valOrigin").Descript = lclsT_DocTyp.sOrigin
                    .Columns("tcdOriginDate").DefValue = lclsT_DocTyp.dDate_Origin
                    .Columns("valOrigin").Parameters("nBranch").Value = lclsT_DocTyp.nBranch
                    .Columns("valOrigin").Parameters("nProduct").Value = lclsT_DocTyp.nProduct
                    .Columns("valOrigin").Parameters("nCollecdoctyp").Value = lclsT_DocTyp.nCollecDocTyp
                    If sApv = "1" Then
                        .Columns("valInstitution").DefValue = lclsT_DocTyp.nInstitution
                        .Columns("valInstitution").Descript = lclsT_DocTyp.sInstitution
                        .Columns("cbeTyp_Profit").DefValue = lclsT_DocTyp.nTyp_Profitworker
                        .Columns("cbeTyp_Profit").Descript = lclsT_DocTyp.sTyp_Profitworker
                        .Columns("cbeTyp_Profit").Parameters("sCertype").Value = "2"
                        .Columns("cbeTyp_Profit").Parameters("nBranch").Value = lclsT_DocTyp.nBranch
                        .Columns("cbeTyp_Profit").Parameters("nProduct").Value = lclsT_DocTyp.nProduct
                        .Columns("cbeTyp_Profit").Parameters("nPolicy").Value = lclsT_DocTyp.nPolicy
                        .Columns("cbeTyp_Profit").Parameters("nCertif").Value = lclsT_DocTyp.nCertif
                        .Columns("cbeTyp_Profit").Parameters("dEffecdate").Value = Today
                    End If
                Else
                    .Columns("valOrigin").DefValue = String.Empty 'CStr(4)
                    .Columns("valInstitution").DefValue = CStr(eRemoteDB.Constants.intNull)
                End If
                .sEditRecordParam = "lsWay=" & Request.QueryString.Item("lsWay") & "&lsFirstRecord=" & Request.QueryString.Item("lsFirstRecord")
                .sEditRecordParam = .sEditRecordParam & "&nCollecdoctyp=" & lclsT_DocTyp.nCollecdoctyp & "&nBranch=" & lclsT_DocTyp.nBranch & "&nProduct=" & lclsT_DocTyp.nProduct & "&nPolicy=" & lclsT_DocTyp.nPolicy & "&nCertif=" & lclsT_DocTyp.nCertif
            
                If lclsT_DocTyp.sNewReceipt = "1" Then
                    .Columns("chkNewReceipt").Checked = 1
                Else
                    .Columns("chkNewReceipt").Checked = 2
                End If
			
                .Columns("hddProdClas").DefValue = lclsT_DocTyp.nProdClas
                Session("nProdClas") = lclsT_DocTyp.nProdClas
			
                lintIndex = lintIndex + 1
                Response.Write(.DoRow)
			
                lintRecordShow = lintRecordShow + 1
			
                '+ Incremento del número de registro total.
                mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
			
                '+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
                If lintRecordIndex >= CN_MAXRECORDS Then
                    Exit For
                End If
            Next lclsT_DocTyp
	End With
	Response.Write(mobjValues.HiddenControl("nItems", lintExists))
	Call ShowTotals()
	
	With mobjValues
		
		Response.Write(.HiddenControl("hddChains", lstrChains))
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>    " & vbCrLf)
Response.Write("    var sChains=""""" & vbCrLf)
Response.Write("    var sChange=""""" & vbCrLf)
Response.Write("    sChains = self.document.forms[0].hddChains.value;" & vbCrLf)
Response.Write("</" & "SCRIPT>        ")

		
		
		'+ Primer registro a cargar    
		Response.Write(.HiddenControl("lsFirstRecord", lsFirstRecord))
		
		'+ Ultimo registro a cargar        
		Response.Write(.HiddenControl("lsLastRecord", lsLastRecord))
		
		'+ Indice que indica el primer item a leer de la lista.
		Response.Write(.HiddenControl("mlngOptionalBeginProcess", mlngOptionalBeginProcess))
		
		'+ Contador de páginas
		Response.Write(.HiddenControl("PageNumber", PageNumber))
	End With
	
	'+ Determina si estará activo o no el Botón [<< Anterior]                                    
	If PageNumber <= 1 And mobjValues.StringToType(Request.QueryString.Item("lsFirstRecord"), eFunctions.Values.eTypeData.etdLong) < CN_MAXRECORDS Then
		mblnDisabledBack = True
	End If
	
	'+ Determina si estará activo o no el Botón [>> Siguiente]
	If (lintRecordShow < CN_MAXRECORDS) Then
		mblnDisabledNext = True
	Else
		If lintRecordShow = CN_MAXRECORDS And mintTotalRecordsCount = CN_MAXRECORDS And mintTotalRecordsCount = lintRecordShow Then
			mblnDisabledNext = True
		End If
	End If
End Sub

    Function ShowTotals() As Double
        Dim lobjColFormref As eCollection.ColformRef
        lobjColFormref = New eCollection.ColformRef
	
        With lobjColFormref
            .nBordereaux = Session("nBordereaux")
            .sStatus = Session("sStatus")
            .dCollect = Session("dCollectDate")
            .dValueDate = Session("dValueDate")
            .nAction = Session("CO001_nAction")
            .sRelOrigi = Session("sRelOrigi")
            .calTotals()
		
            ShowTotals = System.Math.Round(.nTotalAmount + .nDifference - .nPaidAmount, 6)
		
            Response.Write("<SCRIPT>")
			Response.Write(" function SetHeaderValues(){")			 
            Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & mobjValues.TypeToString(System.Math.Round(.nTotalAmount, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
            Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn','" & mobjValues.TypeToString(System.Math.Round(.nPaidAmount, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
            Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(System.Math.Round(ShowTotals, 0), eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
			Response.Write("}")			 
			Response.Write(" function InvokeSetHeaderValues(){try{SetHeaderValues();}" & vbCrLf & "catch(x){setTimeout('InvokeSetHeaderValues()',150);}" & vbCrLf & "finally{}}" & vbCrLf) 
			Response.Write("InvokeSetHeaderValues();")			 
            Response.Write("</" & "Script>")
		
        End With
        lobjColFormref = Nothing
    End Function
    
'+ insDelItem: función que elimina el item de la relación    
'----------------------------------------------------------------------------
Private Sub insDelItem()
	'----------------------------------------------------------------------------    
	Dim lclsT_DocTyp As eCollection.T_DocTyp
	
	lclsT_DocTyp = New eCollection.T_DocTyp
	
	lclsT_DocTyp.nBordereaux = Session("nBordereaux")
	lclsT_DocTyp.nSequence = CInt(Request.QueryString.Item("nSequence"))
	Call lclsT_DocTyp.insUpdT_DocTyp(3)
	
	lclsT_DocTyp = Nothing
End Sub

'+ insReaInitial: Inicializa los valores de las variables
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mintBranch = eRemoteDB.Constants.intNull
	mintProduct = eRemoteDB.Constants.intNull
	mlngPolicy = eRemoteDB.Constants.intNull
	mlngCertif = eRemoteDB.Constants.intNull
	mlngDocument = eRemoteDB.Constants.intNull
	mintDraft = eRemoteDB.Constants.intNull
	mintCurrency = eRemoteDB.Constants.intNull
	mdtmValuedate = eRemoteDB.Constants.dtmNull
	
End Sub

'+ insOldValues : Restaura valores anteriores
'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mintBranch <> eRemoteDB.Constants.intNull And mintProduct <> eRemoteDB.Constants.intNull And mlngPolicy <> eRemoteDB.Constants.intNull And mlngCertif <> eRemoteDB.Constants.intNull And mlngDocument <> eRemoteDB.Constants.intNull And mintDraft <> eRemoteDB.Constants.intNull And mintCurrency > 0 And mdtmValuedate <> eRemoteDB.Constants.dtmNull Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = " & CStr(mintBranch) & ";")
			.Write("var mintProduct = " & CStr(mintProduct) & ";")
			.Write("var mlngPolicy = " & CStr(mlngPolicy) & ";")
			If CStr(mlngCertif) = vbNullString Then
				.Write("var mlngCertif = 0;")
			Else
				.Write("var mlngCertif = " & CStr(mlngCertif) & ";")
			End If
			.Write("var mlngDocument = " & CStr(mlngDocument) & ";")
			.Write("var mintDraft = " & CStr(mintDraft) & ";")
			.Write("var mintCurrency = " & CStr(mintCurrency) & ";")
            .Write("var mdtmValuedate = '" & mobjvalues.DateToString(mdtmValuedate) & "';")
			.Write("var mdtmValuedate = " & Cdate(mdtmValuedate) & ";")
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = 0;")
			.Write("var mintProduct = 0;")
			.Write("var mlngPolicy = 0;")
			.Write("var mlngCertif = 0;")
			.Write("var mlngDocument = 0;")
			.Write("var mintDraft = 0;")
			.Write("var mintCurrency = 0;")
			.Write("var mdtmValuedate = 0;")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>

<%Response.Expires = -1

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CO001"
%>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>

	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE= "JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 28-08-09 0:16 $"
//+ La variable nLastFieldModify y nLastInterestModify guardan el ultimo monto modificado 1- Monto local 2-Monto Original repectivamente
    var nMainAction = 304; nAmountPayJS = VTFormat('0', '', '', '', 6, true) ; nAmountPayLocJS = VTFormat('0', '', '', '', 6, true); nInterestPayJS = VTFormat('0', '', '', '', 6, true); nInterestPayLocJS = VTFormat('0', '', '', '', 6, true); nLastAmountModify = 2; nLastInterestModify = 2;

	<%If Not IsNothing(Request.QueryString.Item("Index")) Then%>
        var nIndex = <%=Request.QueryString.Item("Index")%>
	<%End If%>        


//sPrint: Permite seleccionar los recibos que se van a imprimir.
//------------------------------------------------------------------------------------------
function sPrint(Field){
//------------------------------------------------------------------------------------------
    if(Field.checked){
        sChains = sChains + ", " + Field.value;
        self.document.forms[0].hddChains.value=sChains;
        sChange = "1";
    }else{
        sChains = sChains.replace(Field.value + "," ,"");
        self.document.forms[0].hddChains.value=sChains;
        sChange = "1";
    }
}

//ShowLoans: 
//------------------------------------------------------------------------------------------
function ShowLoans(Field){
//------------------------------------------------------------------------------------------
    if (Field.value != ""){ 
        self.document.forms[0].tcnAmountCol.value=self.document.forms[0].valCode_nAmount.value
        self.document.forms[0].tcnDocument.value=Field.value;
        self.document.forms[0].tcnAmountPay.value=self.document.forms[0].valCode_nAmount.value;
    }
}


//**% MoveRecord: Performed a submit of the page according to movement's type executed.
//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(lsWay) {
//-------------------------------------------------------------------------------------------
	var lstrLocation = '';

	lstrLocation += document.location.href;
	lstrLocation = lstrLocation.replace(/&Reload.*/,"");
	lstrLocation = lstrLocation.replace(/&ReloadAction.*/,"");
	lstrLocation = lstrLocation.replace(/&ReloadIndex.*/,"");			
	lstrLocation = lstrLocation.replace(/&lsWay.*/,"");
	lstrLocation = lstrLocation.replace(/&nMainAction=.*/, "");
	lstrLocation = lstrLocation.replace(/&lsFirstRecord=.*/, "");		
	
	var nReg = 0
//+Se actualiza la temporal T_DOCTYP de acuerdo a lo seleccionado
    if(sChange=="1"){
        with(self.document.forms[0]){
            insDefValues("Letter", "sKey=" + hddKey.value + 
                                   "&sChains=" + sChains +
                                   "&nFirstRecord=" + lsFirstRecord.value + 
                                   "&nLastRecord=" + lsLastRecord.value);
        }
    }

//+Mueve el registro a la página siguiente o anterior, según corresponda

	    
    switch (lsWay){

        case "Next":
			<%If Not IsNothing(Request.QueryString.Item("lsFirstRecord")) Then%>
			    nReg = <%=mobjValues.Stringtotype(Request.QueryString.Item("lsFirstRecord"),eFunctions.Values.eTypeData.etdLong) + CN_MAXRECORDS%>
			<%Else%>
				nReg = <%=CN_MAXRECORDS + 1%>
			<%End If%>        
			lstrLocation = lstrLocation + "&lsWay=Next&nMainAction=401&lsFirstRecord=" + nReg ;
			break;
        case "Back" :
		    <%If Not IsNothing(Request.QueryString.Item("lsFirstRecord")) Then%>
				nReg = <%=mobjValues.Stringtotype(Request.QueryString.Item("lsFirstRecord"),eFunctions.Values.eTypeData.etdLong) - CN_MAXRECORDS%>
            <%End If%>        
            lstrLocation = lstrLocation + "&lsWay=Back&nMainAction=401&lsFirstRecord=" + nReg;
            break;
  }
  document.location.href = lstrLocation;
}

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//Enlace NovaRed.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   var sCli=document.frmCO001.dtcClient.value;
   var sDig=document.frmCO001.dtcClient_Digit.value;
   var sFor=self.document.forms[0].name;
   with (self.document.forms[0]){
    	if(sCli!=""){
    		insDefValuesNR('Client', 'sClient=' + sCli, 'sDigit=' + sDig , 'sForm=' + sFor, '/VTimeNet/Collection/CollectionSeq')
    	}
    }
}

//% insShowPolicyInf: Habilita y deshabilita el campo Certificado
//-------------------------------------------------------------------------------------------
function insShowPolicyInf(){
//-------------------------------------------------------------------------------------------    
	var lstrQueryString;
	var llngPolicy
	var llngProponum
    
	with(self.document.forms[0]){
	    if (cbeCollecDocTyp.value!=1 && cbeCollecDocTyp.value!=2) {
		    llngProponum = tcnDocument.value;
		    if (cbeCollecDocTyp.value==7) {
		    	llngPolicy = tcnDocument.value;
		    } else {
		    	llngPolicy = tcnPolicy.value;
		    }
		    if (llngPolicy > 0) {
		        if (cbeCollecDocTyp.value!=6)
					if (cbeCollecDocTyp.value!=13 && cbeCollecDocTyp.value!=14 && cbeCollecDocTyp.value!=15){
						insDefValues("ShowPolicyInf", "nPolicy=" + llngPolicy + "&nCollecDocTyp=" + cbeCollecDocTyp.value + "&sCertype=2" + "&nCertif=" + tcnCertif.value, '/VTimeNet/Collection/CollectionSeq');
					}	
					else
						insDefValues("ShowPolicyRentVital", "nPolicy=" + llngPolicy + "&dValuedateProp=" + lvaluedate + "&nCollecDocTyp=" + cbeCollecDocTyp.value + "&sCertype=2" + "&nCertif=" + tcnCertif.value, '/VTimeNet/Collection/CollectionSeq');
	            else
		            insDefValues("ShowPolicyLoansInf", "nPolicy=" + llngPolicy + "&dValuedateProp=" + lvaluedate + "&sCertype=2", '/VTimeNet/Collection/CollectionSeq');	    
            }
	    }	    
	    else{
	       insDefValues("ShowDocumentInf", "nPolicy=" + tcnPolicy.value + "&dValuedateProp=" + lvaluedate + "&nCollecDocTyp=" + cbeCollecDocTyp.value + "&sCertype=2", '/VTimeNet/Collection/CollectionSeq');	    
	    }
	}
}
//% insDisabledCertif: Habilita y deshabilita el campo Certificado
//-------------------------------------------------------------------------------------------
function insDisabledCertif(){
//-------------------------------------------------------------------------------------------    
	var lstrQueryString;
	var llngPolicy
	var llngProponum
	
	with(self.document.forms[0]){
		llngProponum = tcnDocument.value;
		
		if (cbeCollecDocTyp.value==7) {
			llngPolicy = tcnDocument.value;
		} else {
			llngPolicy = tcnPolicy.value;
		}
		if (llngPolicy!=0) {
			if ((mintBranch != cbeBranch.value) ||
			    (mintProduct != valProduct.value) ||
			    (mlngPolicy != llngPolicy)) {
				mintBranch = cbeBranch.value;
			    mintProduct = valProduct.value;
			    mlngPolicy = llngPolicy;

				lstrQueryString = "sCertype="+ hddsCertype.value + 
								  "&nBranch="+ cbeBranch.value + 
			                      "&nProduct=" + valProduct.value + 
			                      "&nPolicy=" + llngPolicy +
			                      "&nCertif=" + tcnCertif.value +
			                      "&nCollecDocTyp=" + cbeCollecDocTyp.value + 
                                  "&dValuedateProp=" + lvaluedate + 
			                      "&nProponum=" + llngProponum;
				if (lstrQueryString!='')
					insDefValues("Certif", lstrQueryString, '/VTimeNet/Collection/CollectionSeq');
			}
		}
	}
}
//% ShowDocument: Se encarga de mostrar la información dependiendo del Concepto de cargo/abono seleccionado.
//-------------------------------------------------------------------------------------------
function ShowDocument(){
//-------------------------------------------------------------------------------------------
    var lstrQueryString = "";
    var lblnAdd = ('<%=Request.QueryString.Item("Action")%>'=='Add'?true:false)

    with(self.document.forms[0]){
		if ((tcnDocument.value > 0 && tcnDocument.value!=mlngDocument) ||
			(tcnDraft.value >= 0 && tcnDraft.value!=mintDraft)) {
			
			mlngDocument = tcnDocument.value
			mintDraft = tcnDraft.value
			
		    switch (cbeCollecDocTyp.value){
//+ Concepto de cargo/abono: Recibo.
			    case "1":    
					lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + "&nReceipt=" + tcnDocument.value;

					<%If Session("sReceiptNum") <> eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeGeneral Then%>
					    lstrQueryString = lstrQueryString + "&nBranch=" + document.forms[0].cbeBranch.value;
					<%End If%> 

					<%If Session("sReceiptNum") = eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeBranchProduct Then%>
					    lstrQueryString = lstrQueryString +  "&nProduct=" + document.forms[0].valProduct.value;
					<%End If%>
					break;
					
//+ Concepto de cargo/abono: Cuota de financiamiento.
			    case "2":
					lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + "&nContrat=" + tcnDocument.value +
					                                                   "&nDraft=" + tcnDraft.value + "&nReceipt=" + tcnDocument.value;
					break;
					
//+ Concepto de cargo/abono: Boletín.
			    case "3":        
					lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + "&nBulletin=" + tcnDocument.value;
					break;
					
					
//+ Concepto de cargo/abono: Abono a préstamo.
				case "6":
					if (valCode.value>0) {
						
						lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + 
																		"&nBranch=" + cbeBranch.value +
																		"&nProduct=" + valProduct.value + 
																		"&nPolicy=" + tcnPolicy.value +
																		"&nCertif=" + tcnCertif.value +
																		"&nCode=" + valCode.value;
					}
					break;
											
//+ Concepto de cargo/abono: Propuesta.
			    case "7":
					lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + 
									  "&sCertype=" + + hddsCertype.value + 
									  "&nBranch=" +  cbeBranch.value +
									  "&nProduct=" +  valProduct.value +
									  "&nPolicy=" + tcnDocument.value;

					break;

//+ Concepto de cargo/abono: Abono APV - Traspasos APV - Transferencias APV
                case "21":
                case "22":
                case "23":
				    if (lblnAdd) {
					    lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + 
					    				  "&sCertype=" + + hddsCertype.value + 
					    				  "&nBranch=" +  cbeBranch.value +
					    				  "&nProduct=" +  valProduct.value +
					    				  "&nPolicy=" + tcnPolicy.value + 
					    				  "&nProponum=" + tcnDocument.value;
				    }
				    break;
				case "18":
				case "19":
				case "20":
					lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + 
									  "&sCertype=" + + hddsCertype.value + 
									  "&nBranch=" +  cbeBranch.value +
									  "&nProduct=" +  valProduct.value +
									  "&nPolicy=" + tcnPolicy.value + 
									  "&nProponum=" + tcnDocument.value;
					break;
					
				default:
					break;                    
	        }
		} else {		  
		
			if (tcnDocument.value==0) {
				switch (cbeCollecDocTyp.value){
//+ Concepto de cargo/abono: Abono a poliza, Propuesta.
					case "7":
					case "9":
					case "24":        					    
						lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value ;
						break;
//+ Concepto de cargo/abono: Abono a préstamo.
					case "6":
						if (valCode.value>0) {
						
							lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + 
																			"&nBranch=" + cbeBranch.value +
																			"&nProduct=" + valProduct.value + 
																			"&nPolicy=" + tcnPolicy.value +
																			"&nCertif=" + tcnCertif.value +
																			"&nCode=" + valCode.value;
						}
						break;
						
						
//+ Concepto de cargo/abono: Prima adicional,Prima exceso.
					case "4":
					case "5":        
						lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value;
						break;
					default:
						break;
				}
			}
		}

        lstrQueryString = lstrQueryString + "&dValuedateProp=" + lvaluedate		
//+ Solo se busca la información del documento si se esta agregando uno nuevo		
		if (lstrQueryString!='' && nIndex==-1){
			insDefValues("Documents", lstrQueryString, '/VTimeNet/Collection/CollectionSeq');
	    }
    }
}
// %insChangeTypeDoc : función que cambia los valores de los campos dependiendo del tipo de documento
//-------------------------------------------------------------------------------------------
function insChangeTypeDoc(Field, sAction){
//-------------------------------------------------------------------------------------------
	var lblnAdd = (sAction=='Add'?true:false)
	
    with(self.document.forms[0]){
    
//+ Se deshabilitan todos los campos de la forma si se esta registrando información.
		if (lblnAdd) {
			tcnDocument.disabled = true;
			tcnDocument.value = '0';
			cbeBranch.disabled = true;
			cbeBranch.value = '';
			valProduct.disabled = true;
			btnvalProduct.disabled = valProduct.disabled
			valProduct.value = '';
			UpdateDiv('valProductDesc', '');
			tcnPolicy.disabled = true;
			tcnPolicy.value = '';
			tcnCertif.disabled = true;
			tcnCertif.value = '';
			tcnDocument.disabled = true;
			tcnDraft.value = ''
			tcnDraft.disabled = true
			dtcClient.value = ''
			dtcClient_Digit.value = ''
			UpdateDiv('lblCliename', '');
			//dtcClient.disabled = true;
			//dtcClient_Digit.disabled = true;
			tcnAmountCol.disabled = true;
			tcnAmountCol.value = VTFormat('0', '', '', '', 6, true);
			cbeCurrency.disabled = true;
			cbeCurrency.value = '1';
			tcnExchange.value = '1';
			tcnAmountPay.disabled = true;
			tcnAmountPay.value = VTFormat('0', '', '', '', 6, true);
			tcnAmountLoc.disabled = true;
			tcnAmountLoc.value = VTFormat('0', '', '', '', 0, true);
			tcnface_value.disabled = true;
			tcnface_value.value = VTFormat('0', '', '', '', 0, true);
			tcnInterest_rate.disabled = true;
			tcnInterest_rate.value = VTFormat('0', '', '', '', 6, true);
			tcnInterestLoc.disabled = true;
			tcnInterestLoc.value = VTFormat('0', '', '', '', 0, true);
			mintBranch = 0;
			mintProduct = 0;
			mlngPolicy = 0;
			mlngCertif = 0;
			mlngDocument = 0;
			mintDraft = 0;
//+ Habilitar y deshabilitar campos para el caso de rentas vitalicias
			<%If CStr(Session("chkRentVital")) = "9" Then%>
			tcnTax_discount.value = ''
			tcnface_value.value = VTFormat('0', '', '', '', 0, true);
			tcdIssuedate.value = ''
			tcdExpirdate.value = ''
			<%End If%>			
		
            valOrigin.value = '';
		    UpdateDiv('valOriginDesc', '');
        }
        
        switch (Field.value){
//+ Concepto de cargo/abono: Recibo.
            case "1":
				<%If Session("sReceiptNum") <> eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeGeneral Then%>
					cbeBranch.disabled = false;
				<%End If%> 

				<%If Session("sReceiptNum") = eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeBranchProduct Then%>
				    valProduct.disabled = false;
				    btnvalProduct.disabled = valProduct.disabled;
				    valProduct.Parameters.Param1.sValue = 0;
				<%End If%>
				tcnDocument.disabled = !lblnAdd;
				tcnPolicy.disabled = !lblnAdd;
//+ Sólo se permite realizar pagos parciales a recibos si el área de seguros es vida.
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;

/********************************************Fin-1097**************************************************************/ 								
				lstrQueryString = "nCollecDocTyp=" + cbeCollecDocTyp.value + "&nReceipt=" + tcnDocument.value;
				<%If Session("sReceiptNum") <> eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeGeneral Then%>
				    lstrQueryString = lstrQueryString + "&nBranch=" + document.forms[0].cbeBranch.value;
				<%End If%>
				<%If Session("sReceiptNum") = eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeBranchProduct Then%>
				    lstrQueryString = lstrQueryString +  "&nProduct=" + document.forms[0].valProduct.value;
				<%End If%>
				break;							
//+ Concepto de cargo/abono: Cuota de financiamiento.
            case "2":
				<%If Session("sReceiptNum") <> eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeGeneral Then%>
					cbeBranch.disabled = false;
				<%End If%> 

				<%If Session("sReceiptNum") = eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeBranchProduct Then%>
				    valProduct.disabled = false;
				    btnvalProduct.disabled = valProduct.disabled;
				    valProduct.Parameters.Param1.sValue = 0;
				<%End If%>
				
				tcnDocument.disabled = !lblnAdd;
				tcnPolicy.disabled = !lblnAdd;
//+ Sólo es posible relizar pagos totales de cuotas de financiamiento. No se permiten pagos parciales a cuotas.
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = true;
				tcnAmountLoc.disabled = true;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 								
				break;
//+ Concepto de cargo/abono: Boletin.
            case "3":
				if (sAction=='Update')
					tcnDocument.disabled = true;
				else
					tcnDocument.disabled = false;
				break;
//+ Concepto de cargo/abono: Prima adicional.
            case "4":
				if (lblnAdd) {
					cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					
					valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0)	{
						$(valProduct).change();
					}
					tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					ShowDocument();
				}
				tcnDocument.disabled = true;
				cbeCurrency.disabled = true;
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 				
				break;
//+ Concepto de cargo/abono: Prima exceso.
            case "5":
				if (lblnAdd) {
					cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0)	{
						$(valProduct).change();
					}
					tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					ShowDocument();
				}	
				tcnDocument.disabled = true;
				cbeCurrency.disabled = true;
/********************************************CC-1097**************************************************************/ 				
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 				
				break;
//+ Gastos de cobranza:				
			case "9":
				if (lblnAdd) {
					cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					
					valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0)	{
						$(valProduct).change();
					}
					tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					insShowPolicyInf();
				}
				tcnDocument.disabled = true;
				cbeCurrency.disabled = true;
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 				
				break;
//+ Concepto de cargo/abono: Cuenta individual
//+                          Reliquidación de prima 
//+                          Complemento bono reconocimiento
//+                          Bono exon. pólit. y adicional 
            case "11":
            case "12":
            case "13":            
            case "14":
            case "15":
            case "16":
				if (lblnAdd){
					cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');

					valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0){
						$(valProduct).change();
					}
					tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'');
/********************************************CC-1097**************************************************************/ 
				    tcnAmountPay.disabled = false;
				    tcnAmountLoc.disabled = false;
				    tcnInterest_rate.disabled = false;
				    tcnInterestLoc.disabled = false;
				    insShowPolicyInf();
/********************************************Fin-1097**************************************************************/ 					
				}
				tcnDocument.disabled = true;
				break;
			
//+ Concepto de cargo/abono: Abono a préstamo.
            case "6":            
				tcnPolicy.disabled=false;            
/********************************************Fin-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************CC-1097**************************************************************/ 				
				break;
				
//+ Concepto de cargo/abono: Propuesta.
            case "7":
/*				cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
				cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
				valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
				btnvalProduct.disabled = valProduct.disabled
				valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
				valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
				if (valProduct.value > 0){
					$(valProduct).change();
				}
				

				tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
				tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong)%>:'');
				tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong)%>:'');
				ShowDocument();
				
				
				
				tcnPolicy.disabled = true;
				tcnPolicy.value ='';
				tcnCertif.disabled = (<%=Session("sRel_Type")%>=='2'||<%=Session("nCertif")%>==''?true:false);
				tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'0');*/

                
                if ( lvaluedate!='') {
                    tcdValuedate.disabled = true;
		            tcdValuedate.value =lvaluedate;
               }

				tcnDocument.disabled = false;
				ShowDocument();
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
				
/********************************************Fin-1097**************************************************************/ 				
				break;
//+ Interes Financiero
			case "8":
				if (lblnAdd) {
					cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					
					valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0)	{
						$(valProduct).change();
					}
					tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					insShowPolicyInf();
				}
				tcnDocument.disabled = true;
				cbeCurrency.disabled = true;
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
			    break;
//+ Concepto de cargo/abono: Abono APV - Traspasos APV - Transferencias APV
//            case "20":
            case "21":
                if ( lvaluedate!='') {
                    tcdValuedate.disabled = true;
		            tcdValuedate.value =lvaluedate;
                }

            case "22":
            case "23":

                if (!lblnAdd)
				    tcnDocument.disabled = false;
				else
				    tcnDocument.disabled = false;
                valOrigin.Parameters.Param3.sValue=Field.value;
                valOrigin.disabled=false;
                btnvalOrigin.disabled=false;
				tcdOriginDate.disabled=false;
				btn_tcdOriginDate.disabled=false;
				valInstitution.disabled=false;
			    btnvalInstitution.disabled=false;
				    
				dtcClient.disabled = false;
				dtcClient_Digit.disabled = false;
				btndtcClient.disabled = false;
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 				
				cbeCurrency.disabled = true;
                break;

            case "18":
            case "19":
            case "20":				    
				if (lblnAdd) {
					
					cbeBranch.disabled = (<%=Session("sRel_Type")%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					valProduct.disabled = (<%=Session("sRel_Type")%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0)	{
						$(valProduct).change();
					}
					tcnPolicy.value ='';
					tcnCertif.disabled = (<%=Session("sRel_Type")%>=='2'||<%=Session("nCertif")%>==''?true:false);
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (Field.value == 18 || Field.value == 19 || Field.value == 20){
						tcnDocument.disabled = true;
						tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
						$(tcnPolicy).change();
						tcnPolicy.disabled = (<%=Session("sRel_Type")%>=='2'?true:false);
					}
					else{
						tcnPolicy.disabled = true;
						tcnDocument.disabled = false;
					}
/********************************************CC-1097**************************************************************/ 
				    tcnAmountPay.disabled = false;
				    tcnAmountLoc.disabled = false;
				    tcnInterest_rate.disabled = false;
				    tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 					
					valOrigin.disabled=false;
					btnvalOrigin.disabled=false;
                    valOrigin.Parameters.Param3.sValue=Field.value;
					cbeCurrency.disabled = !lblnAdd;
					tcdOriginDate.disabled=false;
					btn_tcdOriginDate.disabled=false;
					valInstitution.disabled=false;
					btnvalInstitution.disabled=false;
					valOrigin.Parameters.Param1.sValue = Field.value;
				}
				break;
//+ Abono a polizas:				
			case "24":
				if (lblnAdd) {
					cbeBranch.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					cbeBranch.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					
					valProduct.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					btnvalProduct.disabled = valProduct.disabled
					valProduct.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
					valProduct.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)%>:'0');
					if (valProduct.value > 0)	{
						$(valProduct).change();
					}
					tcnPolicy.disabled = (<%=(Session("sRel_Type"))%>=='2'?true:false);
					tcnPolicy.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)%>:'');
					tcnCertif.value = (<%=(Session("sRel_Type"))%>=='2'?<%=mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)%>:'');
//					ShowDocument();
					insShowPolicyInf();
				}
				tcnDocument.disabled = true;
				cbeCurrency.disabled = true;
/********************************************CC-1097**************************************************************/ 
				tcnAmountPay.disabled = false;
				tcnAmountLoc.disabled = false;
				tcnInterest_rate.disabled = false;
				tcnInterestLoc.disabled = false;
/********************************************Fin-1097**************************************************************/ 				
				break;
            default:
                break;
        }
//+ Oculta o muestra los campos segun la el tipo de documento seleccionado
//+ Documentos de APV y Renta vitalicia
        var tds = document.getElementsByTagName("td");


        /*+ Tasa de descuento*/
        tds[40].style.display='none'
        tds[41].style.display='none'

        /*+ Valor nominal*/
        tds[42].style.display='none'
        tds[43].style.display='none'
        
        /*+ Fecha de emision*/
        tds[44].style.display='none'
        tds[45].style.display='none'       
        
        /*+ Fecha de vencimiento*/
        tds[46].style.display='none'
        tds[47].style.display='none'               

        /*+ Origen*/
        tds[48].style.display='none'
        tds[49].style.display='none'
        
        /*+ Fecha Original*/
        tds[52].style.display='none'
        tds[53].style.display='none'
        
        /*+ Entidad finaciera*/
        tds("TD")[54].style.display='none'
        tds("TD")[55].style.display='none'
        
        /*+ Monto a pagar con decimales*/
        tds[58].style.display='none'
        tds[59].style.display='none'
        
        /*+ regimen tributario*/
        tds[60].style.display='none'
        tds[61].style.display='none'
        
        
        if (hddProdClas.value==4 || hddProdClas.value==9 || hddProdClas.value==10 || Field.value==18 || Field.value==19 || Field.value==20 || Field.value==21 || Field.value==22 || Field.value==23 || Field.value==24 || Field.value==13 || Field.value==14 || Field.value==15){
            
            /*if ((hddProdClas.value==4 && hddsApv.value ==1) || Field.value==18 || Field.value==19 || Field.value==20 || Field.value==21 || Field.value==22 || Field.value==23 || Field.value==24){*/
			if (hddProdClas.value==4  || Field.value==18 || Field.value==19 || Field.value==20 || Field.value==21 || Field.value==22 || Field.value==23 || Field.value==24){            
                /*+ Origen*/
                tds[48].style.display=''
                tds[49].style.display=''
               /*+ Entidad finaciera*/
                tds[54].style.display=''
            	tds[55].style.display=''            
            	/*+ regimen tributario*/
            	tds[60].style.display=''
                tds[61].style.display=''
                
                /*if (Field.value=='21'){*/
                if (Field.value=='21'  || Field.value=='18' || Field.value=='19' || Field.value=='20'  || Field.value=='21'  || Field.value=='22' || Field.value=='23'  ){
                /*+ Fecha Original*/
					tds[52].style.display=''
					tds[53].style.display=''
					}
                
				if (hddProdClas.value==4 && hddsApv.value ==1)
				{
					/*+ Fecha Original*/
					tds[52].style.display=''
					tds[53].style.display=''
        
					/*+ Entidad finaciera*/
					tds[54].style.display=''
					tds[55].style.display=''            
                    
                    /*+ Beneficio Tributario*/
                    tds[60].style.display=''
                    tds[61].style.display=''

				}                

                if (Field.value==1 || Field.value==18 || Field.value==19 || Field.value==20 || Field.value==21 || Field.value==22 || Field.value==23 || Field.value==24){

                    if (cbeBranch.value > 0 && valProduct.value > 0){  
                        valOrigin.Parameters.Param1.sValue = (cbeBranch.value<=0?1:cbeBranch.value)
                        valOrigin.Parameters.Param2.sValue = (valProduct.value<=0?1:valProduct.value)
                        $(valOrigin).change();
                        $(valInstitution).change();
                    }
                    else{ 
                        valOrigin.value = ' ';
		                UpdateDiv('valOriginDesc', '');
                    }
                }    
            }
        
            if (Field.value==13 || Field.value==14 || Field.value==15){
                /*+ Tasa de descuento*/
                tds[40].style.display=''
                tds[41].style.display=''

                /*+ Valor nominal*/
                tds[42].style.display=''
                tds[43].style.display=''
        
                /*+ Fecha de emision*/
                tds[44].style.display=''
                tds[45].style.display=''       
        
                /*+ Fecha de vencimiento*/
                tds[46].style.display=''
                tds[47].style.display=''
		    }
		}
//+ Habilitar y deshabilitar campos para el caso de rentas vitalicias
		if (Field.value==13 || Field.value==14 || Field.value==15){
            tcnInterest_rate.disabled = true;
            tcnInterestLoc.disabled = true;
            
            tcnTax_discount.disabled  = false;
            tcnface_value.disabled    = false;
            tcdIssuedate.disabled     = false;
            tcdExpirdate.disabled     = false;
            btn_tcdIssuedate.disabled = false;
            btn_tcdExpirdate.disabled = false;
		}
		
		if (hddProdClas.value!=4)
			chkNewReceipt.checked = false;
			
		
    }
    
}

// insCalculateLocal: Se encarga de obtener el monto en la moneda del pago
//-------------------------------------------------------------------------------------------
function insCalculateLocal(sType){
//-------------------------------------------------------------------------------------------
    var lblnOk = false;
	var ldblAmount
	
/*********************************CC-1097*********************************************************/
    if (nAmountPayJS==0 ||
        nAmountPayJS==''){
        if (nIndex!=-1) 
            nAmountPayJS=top.opener.marrArray[nIndex].tcnAmountPay
    }
     
    if (nAmountPayLocJS==0 ||
        nAmountPayLocJS==''){
        if (nIndex!=-1) 
            nAmountPayLocJS=top.opener.marrArray[nIndex].tcnAmountLoc
    }
    
    if (nInterestPayJS==0 ||
        nInterestPayJS==''){
        if (nIndex!=-1) 
            nInterestPayJS=top.opener.marrArray[nIndex].tcnInterest_rate
    }
       
    if (nInterestPayLocJS==0 ||
        nInterestPayLocJS==''){
        if (nIndex!=-1) 
            nInterestPayLocJS=top.opener.marrArray[nIndex].tcnInterestLoc
    }

	with(self.document.forms[0]){
	    switch (sType){
//+ Concepto de cargo/abono: Recibo.
	    	case "Amount":
	    		if (tcnAmountPay.value=='' ||
	    			tcnAmountPay.value==0) {
	    			tcnAmountLoc.value = VTFormat('0', '', '', '', 0, true);
	    			nAmountPayJS = VTFormat('-999999', '', '', '', 0, true);
	    		}
	    		else{
	    		    lblnOk=(nAmountPayJS!=tcnAmountPay.value);
	    		}
	    		break;
            case "AmountLoc":
	    		if (tcnAmountLoc.value=='' ||
	    			tcnAmountLoc.value==0) {
	    			tcnAmountPay.value = VTFormat('0', '', '', '', 0, true);
	    			nAmountPayLocJS = VTFormat('-999999', '', '', '', 0, true);
	    		}
	    		else{
	    		    lblnOk=(nAmountPayLocJS!=tcnAmountLoc.value);
	    		}
	    		break;	    		
	    	case "Interest":
	    		if (tcnInterest_rate.value=='' ||
	    			tcnInterest_rate.value==0) {
	    			tcnInterestLoc.value = VTFormat('0', '', '', '', 0, true);
	    			nInterestPayJS = VTFormat('-999999', '', '', '', 0, true);
	    		}
	    		else {
                    lblnOk = (nInterestPayJS!=tcnInterest_rate.value);
	    		}
	    		break;
	    	case "InterestLoc":
	    		if (tcnInterestLoc.value=='' ||
	    			tcnInterestLoc.value==0) {
	    			tcnInterest_rate.value = VTFormat('0', '', '', '', 6, true);
	    			nInterestPayLocJS = VTFormat('-999999', '', '', '', 0, true);
	    		}
	    		else {
                    lblnOk = (nInterestPayLocJS!=tcnInterestLoc.value);
	    		}
	    		break;
/*********************************Fin-1097*********************************************************/	    			    		
	    	default:
	    		break;
	    }
	    
	    if (lblnOk){        
	    	if (sType=="Amount") {
	    		ldblAmount = insConvertNumber(tcnAmountPay.value) *
	    		             insConvertNumber(tcnExchange.value);	    		             
				ldblAmount = ldblAmount + 0.5;
	    		tcnAmountLoc.value = VTFormat(ldblAmount, "", "", "", 0, true);
	    		tcnAmountLocDec.value = VTFormat(ldblAmount, "", "", "", 6, true);
	    		nAmountPayJS    = tcnAmountPay.value;
	    		nAmountPayLocJS = tcnAmountLoc.value;
	    		nLastAmountModify = 2;
	    	}
	    	else {
	    	    if (sType=="AmountLoc") {
	    		    ldblAmount = insConvertNumber(tcnAmountLoc.value) /
	    		                 insConvertNumber(tcnExchange.value);	    		             
				    ldblAmount = ldblAmount;
				    tcnAmountPay.value = VTFormat(ldblAmount, "", "", "", 6, true);
				    tcnAmountLocDec.value = tcnAmountLoc.value;
				    nAmountPayJS    = tcnAmountPay.value;
	    		    nAmountPayLocJS = tcnAmountLoc.value;
	    		    nLastAmountModify =1;
	    	    }
	    	    else {
	    	        if (sType=="Interest") {
	    		        ldblAmount = insConvertNumber(tcnInterest_rate.value) *
	    		                     insConvertNumber(tcnExchange.value);
					    ldblAmount = ldblAmount + 0.5;
	    		        tcnInterestLoc.value = VTFormat(ldblAmount, "", "", "", 0, true);
	    		        nInterestPayJS    = tcnInterest_rate.value;
	    		        nInterestPayLocJS = tcnInterestLoc.value;
	    		        nLastInterestModify = 2;
	    		    }
	    		    else {
	    		        if (sType=="InterestLoc") {
	    		            ldblAmount = insConvertNumber(tcnInterestLoc.value) /
	    		                         insConvertNumber(tcnExchange.value);
					        ldblAmount = ldblAmount;
	    		            tcnInterest_rate.value = VTFormat(ldblAmount, "", "", "", 6, true);
	    		            nInterestPayLocJS = tcnInterestLoc.value;
	    		            nInterestPayJS    = tcnInterest_rate.value;
	    		            nLastInterestModify = 1;
	    		        }
	    		    }
	    	    }
	    	}    
	    }
    }
    with(self.document.forms[0]){
	    if (tcnAmountLoc.value=='' ||
	    	tcnAmountLoc.value==0) 
			lblnOk=false;	    
	    else
			if (!ValNumber(tcnAmountLoc,".","'","true",0)){
	    		tcnAmountPay.value='0';	
	    		tcnAmountLoc.value='0';	
	    		nAmountPayJS=0;
			}
	}		
}
// insExchange: Se encarga de obtener el factor de cambio al modificar la moneda
//-------------------------------------------------------------------------------------------
function insExchange(){
//-------------------------------------------------------------------------------------------
	var lstrQueryString;

	with(self.document.forms[0]){
	    
		if (cbeCurrency.value!=0) {
			mintCurrency  = cbeCurrency.value;
			mdtmValuedate = tcdValuedate.value;
			lstrQueryString = "nCurrency="+ cbeCurrency.value + "&dValuedate=" + tcdValuedate.value + "&sCodispl=CO001";
			if (lstrQueryString!='') {
				insDefValues("getExchange", lstrQueryString, '/VTimeNet/Collection/CollectionSeq');
			}
		}
	}
}
//% insCheckSelClick: Actualiza el Header dependiendo de que esta columna este chequeada o no
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
	var lstrQueryString;	    
	var intCount
	
	lstrQueryString = "nSequence=" + marrArray[lintIndex].hddnSequence +
	                  "&nCollecDocTyp=" + marrArray[lintIndex].cbeCollecDocTyp +
	                  "&sSel=" + (Field.checked?'1':'0');
	    
	insDefValues("UpdateCheck", lstrQueryString, '/VTimeNet/Collection/CollectionSeq');
	                  
	if (Field.checked){
	    intCount = insConvertNumber(self.document.forms[0].nItems.value) + 1;
	}
	else{
		intCount = insConvertNumber(self.document.forms[0].nItems.value) - 1;
	}
	             
    self.document.forms[0].nItems.value = intCount;
    
 
//+ Comentado por Guber, cualquier problema comunicarse con el
//	if(marrArray[lintIndex].cbeCollecDocTyp=="3")
//	   ShowGrid();

//	<%If CStr(Session("chkRentVital")) <> "0" Then%>		
//		ShowGrid();
//	<%End If%>

}
/*% ShowReceipts: Esta función se encarga de dibujar una tabla con el contenido de los datos */
/*% del recibo seleccionado el cual se encuentra almecenado en el arreglo.                   */
/*---------------------------------------------------------------------------------------------------------*/
function ShowGrid()
/*---------------------------------------------------------------------------------------------------------*/
{
	var mstrstring = ""; 
	mstrstring += document.location; 
	document.location = mstrstring; 
} 
//% ShowDataSCO001: Muestra los datos de verificación del recibo.
//-------------------------------------------------------------------------------------------
function ShowDataSCO001(lintIndex){
//-------------------------------------------------------------------------------------------
	var lstrQuery;
	if(typeof(marrArray)!='undefined')
	{
		lstrQuery = "&sCertype=2" +
					"&nBranch=" + marrArray[lintIndex].cbeBranch +
					"&nProduct=" + marrArray[lintIndex].valProduct +
					"&nReceipt=" + marrArray[lintIndex].tcnDocument +
					"&nDigit=0" +
					"&nPaynumber=0" +  
					"&nGeneralNumerator=1";
				
		ShowPopUp("/VTimeNet/Common/SCO001.aspx?sCodispl=SCO001" + lstrQuery,"SCO001",700,400,true,false,20,20)
	}
}

//% insSetOrigin: Permite colocar los parametros apropiados a la lista de valores posibles
//% de la cuenta origen
//-------------------------------------------------------------------------------------------
function insSetOrigin(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (cbeBranch.value != '' && valProduct.value != '') { 
            valOrigin.Parameters.Param1.sValue=cbeBranch.value;
            valOrigin.Parameters.Param2.sValue=valProduct.value;
        }
    }
}

</SCRIPT>
    <%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

Response.Write(mobjValues.ShowWindowsName("CO001", Request.QueryString.Item("sWindowDescript")))
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmCO001" ACTION="valCollectionSeq.aspx?time=1">

<%
    lclsGeneral = New eGeneral.OptionsInstallation
    Call lclsGeneral.FindOptPremium()
    If lclsGeneral.sDateFix_Cash = "1" Then
        Response.Write("<script> var lvaluedate='" & DateSerial(Year(Session("dCollectDate")), Month(Session("dCollectDate")), Day(Session("dCollectDate"))) & "'</script>")
        '        Session("dValueDate") = DateSerial(Year(Session("dCollectDate")), Month(Session("dCollectDate")), 9)
    Else
        Response.Write("<script> var lvaluedate </script>")
    End If
    lclsGeneral = Nothing
    Call insReaInitial()
Call insOldValues()
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCO001()
Else
	Call insPreCO001Upd()
	'        Response.Write "<NOTSCRIPT>insChangeTypeDoc(""0"", ""Update"");</SCRIPT>"
End If
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CO001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
mobjMenu = Nothing
mobjGrid = Nothing
mobjValues = Nothing
mintTotalRecordsCount = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23

'+ Se inicializan los campos de la ventana.

Call mobjNetFrameWork.FinishPage("CO001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




