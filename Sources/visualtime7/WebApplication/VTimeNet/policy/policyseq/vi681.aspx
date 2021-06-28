<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mclsInsured_expdis As ePolicy.Insured_expdis
Dim mstrBirthdate As String
Dim mstrSexClien As String
Dim mstrTyperisk As Object
Dim mstrSmoking As String
Dim mstrRating As Object
Dim mstrInsuAge As Object
Dim mstrRoles As Object
Dim mblnShowAgree As Boolean
Dim mstrShowAgree As String
Dim mstrClient As String

'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsCover  As ePolicy.Cover
	    
        mobjGrid = New eFunctions.Grid
	
        mobjGrid.sCodisplPage = "vi681"
	
        mclsInsured_expdis = New ePolicy.Insured_expdis
        lclsCover = New ePolicy.Cover
	    
        mstrClient = Request.QueryString.Item("sClient")
        
	    If IsNothing(mstrClient) Then
            If lclsCover.valExistOnlyOneInsured(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
                mstrClient = lclsCover.SCLIENT
            End If
        End If    

        lclsCover = Nothing        
        
        If Request.QueryString.Item("Type") <> "PopUp" Then
            If Not IsNothing(mstrClient) Then
                Call insGetRolesInfo()
            End If
        End If
	
        '+ Rescata de policy fecha de expiración de la poliza 
        lclsCertificat = New ePolicy.Certificat
        Call lclsCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), False)
	
        '+ Se definen las columnas del grid 
        With mobjGrid.Columns
            '+ Recargo/Descuento 
            Call .AddNumericColumn(100406, GetLocalResourceObject("tcnDisexprcColumnCaption"), "tcnDisexprc", 5, CStr(0), , GetLocalResourceObject("tcnDisexprcColumnToolTip"), , , , , , True)
            mobjGrid.Columns("tcnDisexprc").PopUpVisible = False
		
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cboDisexprcColumnCaption"), "cboDisexprc", "TabDisco_exprc_a", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , "insChangeField(this);", , , GetLocalResourceObject("cboDisexprcColumnToolTip"))
            With mobjGrid.Columns("cboDisexprc").Parameters
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("sDefpol", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .ReturnValue("sDisexpri", True, "Tipo", True)
                .ReturnValue("nCurrency", False, vbNullString, True)
            End With
		
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cboDisexpriColumnCaption"), "cboDisexpri", "table30", eFunctions.Values.eValuesType.clngComboType, "", , , , , "insChangeField(this);", True, , GetLocalResourceObject("cboDisexpriColumnToolTip"), eFunctions.Values.eTypeCode.eString)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", 5, "", , GetLocalResourceObject("tcnModulecColumnToolTip"), , , , , , True)
		
            '+ Cobertura
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", 5, CStr(0), , GetLocalResourceObject("tcnCoverColumnToolTip"), , , , , , True)
            mobjGrid.Columns("tcnCover").PopUpVisible = False
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCoverColumnCaption"), "cbeCover", "TabCover_Client", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True, , , , "insChangeField(this);", , , GetLocalResourceObject("cbeCoverColumnCaption"))
            With mobjGrid.Columns("cbeCover").Parameters
                .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("sClient", mstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .ReturnValue("nModulec", True, "Modulo", True)
                .ReturnValue("sCoveruse", False, , True)
            End With
		
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 4, "", , GetLocalResourceObject("tcnAgeColumnToolTip"), , , , , "insChangeField(this);")
            Call .AddCheckColumn(0, GetLocalResourceObject("chkPermTempColumnCaption"), "chkPermTemp", "", , , "DisableClick(this);", Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkPermTempColumnToolTip"))
            Call .AddCheckColumn(0, GetLocalResourceObject("chkUnitColumnCaption"), "chkUnit", "", CShort("2"), , "CheckForRating()", Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkUnitColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "", , GetLocalResourceObject("tcnRateColumnToolTip"), , 6, , , "insChangeField(this);")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "", , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6, , , "insChangeField(this);")
            Call .AddDateColumn(0, GetLocalResourceObject("tcdDate_FrColumnCaption"), "tcdDate_Fr", Session("dEffecdate"), , GetLocalResourceObject("tcdDate_FrColumnToolTip"))
            Call .AddDateColumn(0, GetLocalResourceObject("tcdDate_toColumnCaption"), "tcdDate_to", CStr(lclsCertificat.dExpirdat), , GetLocalResourceObject("tcdDate_toColumnToolTip"))
            ' Solo aplica para los recargo no aplica para descuentos e impuestos 
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCauseColumnCaption"), "cbeCause", "table5631", eFunctions.Values.eValuesType.clngComboType, "", , , , , "ChgRiskyBussiness(this.value);", , , GetLocalResourceObject("cbeCauseColumnToolTip"))
            'Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCauseColumnCaption"), "cbeCause", "table5631", eFunctions.Values.eValuesType.clngComboType, "", , , , , , , , GetLocalResourceObject("cbeCauseColumnToolTip"))
			
			
			'+Nuevos campos rutina de cálculo            
            
            Call .AddCheckColumn(0, GetLocalResourceObject("chkDateEffecColumnCaption"), "chkDateEffec", "", 1, CStr(1), , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkDateEffecColumnToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valSportColumnCaption"), "valSport", "TAB_SPORT", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , "ChgPercent(1)", True, , GetLocalResourceObject("valSportColumnToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valActivityColumnCaption"), "valActvity", "TAB_ACTIVITY", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , "ChgPercent(2)", True, , GetLocalResourceObject("valActvityColumnToolTip"))
            With mobjGrid.Columns("valSport").Parameters
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCover", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .ReturnValue("nPercent", False, , True)
            End With
            
            With mobjGrid.Columns("valActvity").Parameters
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCover", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .ReturnValue("nPercent", False, , True)
            End With
            If mblnShowAgree Then
                Call .AddCheckColumn(0, GetLocalResourceObject("chkAgreeColumnCaption"), "chkAgree", "", 1, CStr(1), , True, GetLocalResourceObject("chkAgreeColumnToolTip"))
            Else
                Call .AddHiddenColumn("chkAgree", "1")
            End If
		
            '+ Notas        
            '.AddButtonColumn(0, GetLocalResourceObject("SCA2-XColumnCaption"), "SCA2-X", Request.QueryString.Item("nNoteNum"), , Not Request.QueryString.Item("Type") = "PopUp")
            .AddButtonColumn(0, GetLocalResourceObject("SCA2-XColumnCaption"), "SCA2-X", 0, True, Not Request.QueryString.Item("Type") = "PopUp" Or Session("bQuery"))
            .AddHiddenColumn("hddNoteNum", "")
            .AddHiddenColumn("nOriginalNotenum", "")
            .AddHiddenColumn("nCopyNotenum", "")
		
            '+ Se definen las columnas ocultas del grid
            Call .AddHiddenColumn("hddnExist", "")
            Call .AddHiddenColumn("hddoldnDisexprc", CStr(0))
            Call .AddHiddenColumn("hddDisexpri_old", CStr(0))
            Call .AddHiddenColumn("hddoldnModulec", CStr(0))
            Call .AddHiddenColumn("hddoldnCover", CStr(0))
            Call .AddHiddenColumn("hddnRate", CStr(0))
            Call .AddHiddenColumn("hddnAmount", CStr(0))
            Call .AddHiddenColumn("hddsClient", mstrClient)
            Call .AddHiddenColumn("hddDate_to", mobjValues.TypeToString(lclsCertificat.dExpirdat, eFunctions.Values.eTypeData.etdDate))
            Call .AddHiddenColumn("hddCoverUse_old", vbNullString)
            Call .AddHiddenColumn("hddCoverUse", vbNullString)
            Call .AddHiddenColumn("hddUnit_old", vbNullString)
            Call .AddHiddenColumn("hddCause_old", CStr(0))
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "VI681"
            .ActionQuery = Session("bQuery")
            .Columns("cboDisexprc").EditRecord = True
            .Top = 100
            .bCheckVisible = True
            .UpdContent = True
            .WidthDelete = 480
		
            '+Se definen el ancho y Alto
            .FieldsByRow = 2
            .Width = 890   '850
            .Height = 440  '550
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
            .sDelRecordParam = "sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&sClient='+ marrArray[lintIndex].hddsClient + '" & "&nModulec='+ marrArray[lintIndex].tcnModulec + '" & "&nDisexprc='+ marrArray[lintIndex].cboDisexprc + '" & "&nCover='+ marrArray[lintIndex].cbeCover + '" & "&dEffecdate=" & mobjValues.TypeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nRate='+ marrArray[lintIndex].hddnRate +'" & "&nRole=' + self.document.forms[0].cbeRole.value + '" & "&nTotalRate=' + self.document.forms[0].hddTotalRate.value + '" & "&nDisexpri='+ marrArray[lintIndex].cboDisexpri + '" & "&sUnit=' + (marrArray[lintIndex].chkUnit?'1':'2') + '" & "&nDisexpri_old='+ marrArray[lintIndex].hddDisexpri_old + '" & "&sCoveruse_old='+ marrArray[lintIndex].hddCoverUse_old + '" & "&sUnit_old='+ marrArray[lintIndex].hddUnit_old + '" & "&nCause_old='+ marrArray[lintIndex].hddCause_old + '" & "&sCoveruse='+ marrArray[lintIndex].hddCoverUse + '" & "&nCause='+ marrArray[lintIndex].cbeCause + '"
		
            .sEditRecordParam = "nRole=' + self.document.forms[0].cbeRole.value + '" & "&sClient=" & mstrClient & "&nTotalRate=' + self.document.forms[0].hddTotalRate.value + '"
		
            .Columns("chkAgree").Disabled = Request.QueryString.Item("Type") <> "PopUp"
        End With
	
        '+Se crea campo oculto con datos de cliente
        If Request.QueryString.Item("Type") = "PopUp" Then
            Response.Write(mobjValues.HiddenControl("hddTotalRate", Request.QueryString.Item("nTotalRate")))
            Response.Write(mobjValues.HiddenControl("hddRole", Request.QueryString.Item("nRole")))
        End If
	
        lclsCertificat = Nothing
End Sub

'% insPreVI681: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI681()
	'--------------------------------------------------------------------------------------------
	Dim mcolInsured_expdiss As ePolicy.Insured_expdiss
	Dim lintIndex As Byte
	Dim lstrQueryString As String
	Dim ldblTotalRate As Double
	
	mcolInsured_expdiss = New ePolicy.Insured_expdiss
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=6> " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">" & vbCrLf)
Response.Write("            ")

	mobjValues.TypeList = 2
	mobjValues.ClientRole = "1,13,16,25"
	lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
	Response.Write(mobjValues.ClientControl("tctClient", mstrClient,  , GetLocalResourceObject("tctClientToolTip"), "InsChangeClient();",  ,  ,  ,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy,  ,  ,  , lstrQueryString))
	
Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeRoleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, mstrRoles,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRoleToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdBirthdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdBirthdate", mstrBirthdate,  , GetLocalResourceObject("tcdBirthdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeSexclienCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeSexclien", "table18", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mstrSexClien, eFunctions.Values.eTypeData.etdInteger),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSexclienToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""83%"" COLS=6>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnInsuAgeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnInsuAge", 2, mstrInsuAge,  , GetLocalResourceObject("tcnInsuAgeToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnRatingCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnRating", 5, mstrRating,  , GetLocalResourceObject("tcnRatingToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeTyperiskCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTyperisk", "TABLE5639", eFunctions.Values.eValuesType.clngComboType, mstrTyperisk,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTyperiskToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), mstrSmoking,  ,  , True,  , GetLocalResourceObject("chkSmokingToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>  " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("nHiddenRole", CStr(23)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

	
	If mcolInsured_expdiss.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mstrClient, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		With mobjGrid
			For	Each mclsInsured_expdis In mcolInsured_expdiss
				' + Se asignan los valores de las columnas del grid 
				.Columns("cboDisexprc").DefValue = CStr(mclsInsured_expdis.nDisexprc)
				.Columns("tcnDisexprc").DefValue = CStr(mclsInsured_expdis.nDisexprc)
				.Columns("cboDisexpri").DefValue = mclsInsured_expdis.sDisexpri
				.Columns("tcnModulec").DefValue = CStr(mclsInsured_expdis.nModulec)
				.Columns("tcnCover").DefValue = CStr(mclsInsured_expdis.nCover)
				.Columns("cbeCover").DefValue = CStr(mclsInsured_expdis.nCover)
				.Columns("tcnAge").DefValue = CStr(mclsInsured_expdis.nAge)
				.Columns("chkPermTemp").Checked = CShort(mclsInsured_expdis.sPerm_Temp)
				.Columns("chkUnit").Checked = CShort(mclsInsured_expdis.sUnit)
				.Columns("tcnRate").DefValue = CStr(mclsInsured_expdis.nRate)
				.Columns("tcnAmount").DefValue = CStr(mclsInsured_expdis.nAmount)
				.Columns("tcdDate_Fr").DefValue = CStr(mclsInsured_expdis.dDate_Fr)
				.Columns("tcdDate_to").DefValue = CStr(mclsInsured_expdis.dDate_to)
				.Columns("cbeCause").DefValue = CStr(mclsInsured_expdis.nCause)
				If mclsInsured_expdis.sAgree = "1" Then
					.Columns("chkAgree").Checked = CShort("1")
				Else
					.Columns("chkAgree").Checked = 2
				End If
				'				If mblnShowAgree Then
				'					If mclsInsured_expdis.sDisexpri <> "1" And '					   mclsInsured_expdis.sDisexpri <> "4"  Then 
				'					    .Columns("chkAgree").Disabled = True 
				'					Else 
				'					    .Columns("chkAgree").Disabled = False 
				'					End If 
				'				End If 
				.Columns("hddnRate").DefValue = CStr(mclsInsured_expdis.nRate)
				.Columns("hddnAmount").DefValue = CStr(mclsInsured_expdis.nAmount)
				.Columns("hddnExist").DefValue = mclsInsured_expdis.sSel
				.Columns("hddoldnDisexprc").DefValue = CStr(mclsInsured_expdis.nDisexprc)
				.Columns("hddoldnModulec").DefValue = CStr(mclsInsured_expdis.nModulec)
				.Columns("hddoldnCover").DefValue = CStr(mclsInsured_expdis.nCover)
				.Columns("btnNotenum").nNotenum = mclsInsured_expdis.nNotenum
				
				.Columns("hddDisexpri_old").DefValue = mclsInsured_expdis.sDisexpri
				.Columns("hddCoverUse_old").DefValue = mclsInsured_expdis.sCoverBase
				.Columns("hddCoverUse").DefValue = mclsInsured_expdis.sCoverBase
				.Columns("hddUnit_old").DefValue = mclsInsured_expdis.sUnit
				.Columns("hddCause_old").DefValue = CStr(mclsInsured_expdis.nCause)
				
				If CDbl(mclsInsured_expdis.sPerm_Temp) = 1 Then
					.Columns("tcdDate_Fr").Disabled = False
					.Columns("tcdDate_To").Disabled = False
				Else
					.Columns("tcdDate_Fr").Disabled = True
					.Columns("tcdDate_To").Disabled = True
                End If
                    
				If mclsInsured_expdis.sInitdate_Calc = "1" Then
					.Columns("chkDateEffec").Checked = CShort("1")
				Else
					.Columns("chkDateEffec").Checked = 2
				End If
				.Columns("valActvity").Parameters(3).Value = mclsInsured_expdis.nCover
				.Columns("valSport").Parameters(3).Value = mclsInsured_expdis.nCover
				.Columns("valActvity").DefValue = mclsInsured_expdis.nActivity
				.Columns("valSport").DefValue = mclsInsured_expdis.nSport
				
				ldblTotalRate = ldblTotalRate + mclsInsured_expdis.nRateForRating
				Response.Write(.DoRow)
				
				lintIndex = lintIndex + 1
			Next mclsInsured_expdis
		End With
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	'+Se crea campo para almacenar total de tasa. De aqui se pasa como parametro a eliminar y editar
	Response.Write(mobjValues.HiddenControl("hddTotalRate", mobjValues.TypeToString(ldblTotalRate, eFunctions.Values.eTypeData.etdDouble, False, 6)))
	mcolInsured_expdiss = Nothing
	
End Sub

'% insPreVI681Upd: se realiza el manejo de la PopUp
'--------------------------------------------------------------------------------------------
Private Sub insPreVI681Upd()
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lstrContent As String
	lstrContent = vbNullString
	
	Dim lclsInsured_expdis As ePolicy.Insured_expdis
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsInsured_expdis = New ePolicy.Insured_expdis
			lblnPost = lclsInsured_expdis.InsPostVI681Upd("Del", mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddnExist"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), .QueryString.Item("nDisexpri"), .QueryString.Item("sUnit"), 0, mobjValues.StringToType(.QueryString.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("chkPermTemp"), mobjValues.StringToType(.QueryString.Item("tcdDate_Fr"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcnAge"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("tcnNotenum"), mobjValues.StringToType(.QueryString.Item("hddnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddoldnDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddoldnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddoldnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCause"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("chkAgree"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTotalRate"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nDisexpri_old"), .QueryString.Item("sCoveruse_old"), .QueryString.Item("sUnit_old"), mobjValues.StringToType(.QueryString.Item("nCause_old"), eFunctions.Values.eTypeData.etdLong), .QueryString.Item("sCoveruse"), vbNullString, 0, 0)
			'lblnPost = lclsInsured_expdis.insPostVI681Upd("Del", mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddnExist"), eFunctions.Values.eTypeData.etdInteger), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), .QueryString.Item("nDisexpri"), .QueryString.Item("sUnit"), 0, mobjValues.StringToType(.QueryString.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("chkPermTemp"), mobjValues.StringToType(.QueryString.Item("tcdDate_Fr"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcdDate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcnAge"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("tcnNotenum"), mobjValues.StringToType(.QueryString.Item("hddnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddoldnDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddoldnModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddoldnCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCause"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("chkAgree"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTotalRate"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nDisexpri_old"), .QueryString.Item("sCoveruse_old"), .QueryString.Item("sUnit_old"), mobjValues.StringToType(.QueryString.Item("nCause_old"), eFunctions.Values.eTypeData.etdLong), .QueryString.Item("sCoveruse"))
			
			lstrContent = lclsInsured_expdis.sContent
			Response.Write(mobjValues.ConfirmDelete)
			lclsInsured_expdis = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index")), lstrContent))
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>DisabledAgree(top.opener.marrArray[CurrentIndex].cboDisexpri)</" & "Script>")
		Response.Write("<SCRIPT>ChgRiskyBussiness(top.opener.marrArray[CurrentIndex].cbeCause)</" & "Script>")
	End If
End Sub

'% insGetRolesInfo: Rescata datos del cliente 
'--------------------------------------------------------------------------------------------
Sub insGetRolesInfo()
	'--------------------------------------------------------------------------------------------
	Dim lcolRoles As ePolicy.Roleses
	Dim lclsRoles As ePolicy.Roles
	
	lcolRoles = New ePolicy.Roleses
	If lcolRoles.Find_by_Policy(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mstrClient, Session("dEffecdate"), eRemoteDB.Constants.intNull, 2, "1,13,16,25", True) Then
		lclsRoles = lcolRoles.Item(1)
		With lclsRoles
			mstrBirthdate = mobjValues.TypeToString(.dBirthdate, eFunctions.Values.eTypeData.etdDate)
			mstrSexClien = .sSexclien
			mstrTyperisk = .nTyperisk
			mstrSmoking = .sSmoking
			mstrRating = .nRating
			mstrRoles = .nRole
			mstrInsuAge = .nAge(True)
		End With
	End If
	
	lclsRoles = Nothing
	lcolRoles = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "vi681"

'+Se determina si se muestra columna de Aceptado de sobreprima     
'+ Solo aplica para vida y vidactiva 
mstrShowAgree = "1"
If CStr(Session("sbrancht")) = "1" Then
	If CStr(Session("sPolitype")) = "1" Then
		mblnShowAgree = True
		mstrShowAgree = "2"
	End If
End If

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 12 $|$$Date: 22/11/03 13:15 $|$$Author: Nvaplat18 $"

//% CheckForRating: Se cargan los valores de acuerdo producto seleccionado
//------------------------------------------------------------------------------------------- 
function CheckForRating(){
//------------------------------------------------------------------------------------------- 
    var bIsRating;
	
	with (document.forms[0])
	{
	    bIsRating = (hddCoverUse.value =="1" && !chkUnit.checked && cboDisexpri.value=="1" && cbeCause.value=="");
	    //cbeCause.disabled  = bIsRating;
		if (bIsRating)
		    cbeCause.value = 1;
	}
}				

	
//% ShowChangeValues: Se cargan los valores de acuerdo producto seleccionado
//------------------------------------------------------------------------------------------- 
function EditRecord_CU(Field, nMainAction,Action,Param){ 
//------------------------------------------------------------------------------------------- 
	if (typeof(Action)=='undefined') 
	    Action='Update';
    if (typeof(Param)=='undefined')
	    Param='';
    else
	{
	    Param=(Param==''?'':'&' + Param)
		if(Field>-1)
	        Param+='&nNoteNum=' + marrArray[Field].btnNotenum;
	}
    //%Se establece el Largo y Ancho de la ventana PopUp
    ShowPopUp("/VTimeNet/Common/EditRecord.aspx?Type=PopUp&Action=" + Action + "&Index=" + Field + "&nMainAction=" + nMainAction + "&sCodispl=VI681" + Param,"VI681Upd", (Action=='Del'?480:1050), (Action=='Del'?110:440) ,'no','no',100, (Action=='Del'?200:100));
} 

//% ShowChangeValues: Se cargan los valores de acuerdo producto seleccionado
//------------------------------------------------------------------------------------------- 
function ShowChangeValues(sField){ 
//------------------------------------------------------------------------------------------- 
    var strParams; 
    
    switch(sField){  
        case "Curren_Disexprc":    
            if (self.document.forms[0].cboDisexprc.value != 0 &&
                self.document.forms[0].cboDisexprc.value != '') { 
                 strParams = "sCertype=" + <%=Session("sCertype")%> +  
                             "&nBranch=" + <%=Session("nBranch")%> +  
                             "&nProduct=" + <%=Session("nProduct")%> + 
                             "&nPolicy=" + <%=Session("nPolicy")%> + 
                             "&nCertif=" + <%=Session("nCertif")%> + 
                             "&dEffecdate=" + "<%=Session("dEffecdate")%>" + 
                             "&nDisexprc=" + self.document.forms[0].cboDisexprc.value  
                insDefValues(sField, strParams,'/VTimeNet/Policy/PolicySeq'); 
            }
    } 
} 
//%ChgPercent(#): Asigna el valor de la tasa según al actividad/deporte dealto riesgo seleccionada
//--------------------------------------------------------------------------------------------------
function ChgPercent(Field){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){ 
        switch(Field){
           case 2:
                tcnRate.value = valActvity_nPercent.value;  
                break;
           case 1:
                tcnRate.value = valSport_nPercent.value;
                break;
        }
    }
}
//%ChgRiskyBussiness: Habilita los campos actividad y deporte de alto riesgo según el valor del campo Causa
//------------------------------------------------------------------------------------------
function ChgRiskyBussiness (Field){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){ 
        switch(Field){
            case "2":
                valSport.disabled=false;
                btnvalSport.disabled=false;
                valActvity.disabled=true;
                btnvalActvity.disabled=true;
                valActvity.value="";
                UpdateDiv('valActvityDesc','');
                break;
            case "4": 
                valActvity.disabled=false;
                btnvalActvity.disabled=false;
                valSport.disabled=true;
                btnvalSport.disabled=true;
                valSport.value='';
                UpdateDiv('valSportDesc','');
                break;
            default:
                valSport.disabled=true;
                btnvalSport.disabled=true;
                valSport.value='';
                valActvity.disabled=true;
                btnvalActvity.disabled=true;
                valActvity.value='';
                UpdateDiv('valSportDesc','');
                UpdateDiv('valActvityDesc','');

                break;             
        }
    }

}

//% insChangeField: Se recargan los valores cuando cambia el campo 
//------------------------------------------------------------------------------------------- 
function insChangeField(Field){ 
//------------------------------------------------------------------------------------------- 
	var lstrOperat 
	var lstrOperat = '<%=Request.QueryString.Item("Action")%>'
    with (self.document.forms[0]){ 
        switch(Field.name){ 
            case "cboDisexprc": 
                cboDisexpri.value = cboDisexprc_sDisexpri.value; 
                ShowChangeValues("Curren_Disexprc"); 
				if (lstrOperat == "Add") {
					if(cboDisexpri.value==1 || 
					   cboDisexpri.value==4){ 
				        chkAgree.disabled=false; 
 						chkAgree.checked=false;
					} 
					else {
					    chkAgree.disabled=true; 
 						chkAgree.checked=true;
					} 
                } 
                CheckForRating();
                break; 
            case "tcnAge": 
//+ Si campo años tiene valor se inabilitan los campos tasa, factor y monto 
                with (top.frames['fraFolder'].document.forms[0]){
					if (tcnAge.value != "" && tcnAge.value != 0){ 
					      chkUnit.disabled = true 
					      chkUnit.value = "0" 
					      tcnRate.value = "" 
					      tcnRate.disabled = true 
					      tcnAmount.value = "" 
					      tcnAmount.disabled = true 
					} 
					else {
					      chkUnit.disabled = false
					      tcnRate.disabled = false 
					      tcnAmount.disabled = false
					} 
                } 
                break; 
            case "cbeCover":
                tcnModulec.value = cbeCover_nModulec.value;
                hddCoverUse.value = cbeCover_sCoveruse.value;
				if(Field.value>0){
                    valSport.Parameters.Param3.sValue=Field.value;
                    valActvity.Parameters.Param3.sValue=Field.value;
                }
                else{
                    valSport.Parameters.Param3.sValue=0;
                    valActvity.Parameters.Param3.sValue=0;
                    valSport.value='';
                    UpdateDiv('valSportDesc','');
                    valActvity.value='';
                    UpdateDiv('valActvityDesc','');
                }
				CheckForRating();
                break; 
            case "tcnRate","tcnAmount": 
//+ Si campo factor tiene valor se inabilitan el campo años  
//+ Si campo monto tiene valor se inabilitan el campo años   
                with (top.frames['fraFolder'].document.forms[0]){ 
					if ((tcnRate.value != "" && tcnRate.value != 0) ||
					    (tcnAmount.value != "" && tcnAmount.value != 0)){  
					      tcnAge.disabled = true  
					      tcnAge.value = ""  
			        } 
					else
						tcnAge.disabled = false  
				} 
                break;
        }
    }
}

//% InsChangeClient: Se recarga la página cuando se modifica el cliente
//-------------------------------------------------------------------------------------------
function InsChangeClient(){
//-------------------------------------------------------------------------------------------
    var lstrstring = "";
    lstrstring += document.location;
    lstrstring = lstrstring.replace(/&sClient=.*/, "");
    lstrstring = lstrstring + "&sClient=" + self.document.forms[0].tctClient.value + 
                              "&Reload=2";
    document.location = lstrstring;
}

//% DisableClick: Habilita o deshabilita campos de la pagina
//-------------------------------------------------------------------------------------------
function DisableClick(Field){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (Field.checked==true){
            tcdDate_Fr.value = "<%=Session("dEffecdate")%>" 
            tcdDate_to.value = hddDate_to.value
            tcdDate_Fr.disabled = true 
            tcdDate_to.disabled = true 
            btn_tcdDate_Fr.disabled = true 
            btn_tcdDate_to.disabled = true 
        }
        else{ 
            btn_tcdDate_Fr.disabled = false
            btn_tcdDate_to.disabled = false
            tcdDate_Fr.disabled = false
            tcdDate_to.disabled = false
        } 
    } 
} 
//% DisabledAgree: Habilita o deshabilita 
//----------------------------------------------------------------------------------------------------
function DisabledAgree(Field){
//----------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if(Field==1 || Field==4){
            chkAgree.disabled=false;
        }
        else {
            chkAgree.disabled=true;
        }
    }
}

</SCRIPT>
<%

Session("sOriginalForm") = vbNullString
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI681", "VI681.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%> 
</HEAD> 
<BODY ONUNLOAD="closeWindows();"> 
<FORM METHOD="POST" NAME="VI681" ACTION="ValPolicySeq.aspx?sMode=1"> 
<%
Response.Write(mobjValues.ShowWindowsName("VI681"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI681Upd()
Else
	Call insPreVI681()
End If

mclsInsured_expdis = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>

<SCRIPT>
   EditRecord = EditRecord_CU
</SCRIPT>




