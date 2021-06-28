<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objetos/Variables para el manejo de la transacción
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGridCov As Object
Dim mobjGridC As eFunctions.Grid
Dim mobjGridF As eFunctions.Grid
Dim mobjCoverDesc As Object
Dim mobjsCliename As Object
Dim primera As String
Dim mintnCover As String
Dim lblEnabled As Boolean
Dim nFacPerc As String
Dim nFacAmount As String
Dim nContPerc As String
Dim nContAmount As String
Dim sPercent As String
Dim sAmount As String
Dim sContract As Object
Dim sFacult As Object
Dim sPriority As String
Dim mclsReinsuran As ePolicy.Reinsuran


'%insDefineHeaderC.Esta funcion se encarga de definir las caracteristicas del Grid de los contratos obligatorios
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderC()
	'------------------------------------------------------------------------------------------------------------------------------------------------------ 
	mobjGridC = New eFunctions.Grid
	mobjGridC.sCodisplPage = "CA021A"
	mobjGridC.sArrayName = "marrArrayC"
	
	With mobjGridC.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tcncoverColumnCaption"), "tcncover", 30, " ",  , GetLocalResourceObject("tcncoverColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctContractColumnCaption"), "tctContract", 30, " ",  , GetLocalResourceObject("tctContractColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContractColumnCaption"), "tcnContract", 9, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctBranchtColumnCaption"), "tctBrancht", 30, " ",  , GetLocalResourceObject("tctBranchtColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdContractColumnCaption"), "tcdContract", "",  , GetLocalResourceObject("tcdContractColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuota_shaColumnCaption"), "tcnQuota_sha", 9, CStr(0),  , GetLocalResourceObject("tcnQuota_shaColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("nType") <> "2")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0),  , GetLocalResourceObject("tcnCapitalColumnCaption"), False, 0,  ,  ,  , Request.QueryString.Item("nType") <> "2")
		Call .AddHiddenColumn("hddnType", "0")
		Call .AddHiddenColumn("nCover", mintnCover)
		Call .AddHiddenColumn("hddnComission", "")
		Call .AddHiddenColumn("hddnInter_rate", "")
		Call .AddHiddenColumn("hddnReser_rate", "")
		Call .AddHiddenColumn("hddnCapital_rei", "")
		Call .AddHiddenColumn("hddnShare_rei", "")
		Call .AddHiddenColumn("hddnOrder", "")
	End With
	
	With mobjGridC
		
		'.Columns("hddnCapital_rei").DefValue = Request.QueryString("tcnContPer")
		.Columns("hddnShare_rei").DefValue = Request.QueryString.Item("tcnContPer")
		.Columns("hddnOrder").DefValue = Request.QueryString.Item("sPriority")
		
		.Codispl = Request.QueryString.Item("sCodispl")
		.Height = 350
		.Width = 500
		.Columns("Sel").GridVisible = False
		.Columns("tcncover").EditRecord = True
		.DeleteButton = False
		.AddButton = False
		
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sIsCOB") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.actionQuery = Session("bQuery")
	End With
	
End Sub

'%insDefineHeaderF.Esta funcion se encarga de definir las caracteristicas del GriD de los contratos facultativo
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderF()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lstrQueryString As String
	mobjGridF = New eFunctions.Grid
	
	mobjGridF.sCodisplPage = "CA021A"
	
	mobjGridF.sArrayName = "marrArrayF"
	
	With mobjGridF.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCompanyColumnCaption"), "cbeCompany", "tabCompanyClient", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnClasificColumnCaption"), "tcnClasific", "Table5563", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnClasificColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		'Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"),"tcnParticip",18,0,, GetLocalResourceObject("tcnParticipColumnCaption"),True,6,,,"insShowShare(this.value)")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentageColumnCaption"), "tcnPercentage", 9, CStr(0),  , GetLocalResourceObject("tcnPercentageColumnToolTip"), True, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnComissionColumnCaption"), "tcnComission", 8, CStr(0),  , GetLocalResourceObject("tcnComissionColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReser_rateColumnCaption"), "tcnReser_rate", 8, CStr(0),  , GetLocalResourceObject("tcnReser_rateColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInter_rateColumnCaption"), "tcnInter_rate", 8, CStr(0),  , GetLocalResourceObject("tcnInter_rateColumnToolTip"), True, 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdAcceptDateColumnCaption"), "tcdAcceptDate", Session("dEffecdate"),  , GetLocalResourceObject("tcdAcceptDateColumnToolTip"))
		Call .AddHiddenColumn("tcnType", "4")
		Call .AddHiddenColumn("nCover", mintnCover)
		Call .AddHiddenColumn("tcnCapital_rei", "")
		Call .AddHiddenColumn("tcnShare_rei", "")
		Call .AddHiddenColumn("tcnOrder", "")
	End With
	With mobjGridF
		.Height = 350
		.Width = 520
		.WidthDelete = 450
		.AddButton = True
		.Columns("cbeCompany").Parameters.Add("nCompany", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeCompany").EditRecord = True
		.Columns("cbeCompany").BlankPosition = False
		.Codispl = Request.QueryString.Item("sCodispl")
		.sDelRecordParam = "nCover=" & mintnCover & "&nType=' + marrArrayF[lintIndex].tcnType + '" & "&nCompany=' + marrArrayF[lintIndex].cbeCompany + '"
		
		
		.Columns("tcnCapital_rei").DefValue = Request.QueryString.Item("tcnFacAmount")
		.Columns("tcnShare_rei").DefValue = Request.QueryString.Item("tcnFacPer")
		.Columns("tcnOrder").DefValue = Request.QueryString.Item("sPriority")
		
		lstrQueryString = "nCover=" & mintnCover & "&tcnFacPer=" & Request.QueryString.Item("tcnFacPer") & "&tcnContPer=" & Request.QueryString.Item("tcnContPer")
		lstrQueryString = lstrQueryString & "&chkPercen=" & Request.QueryString.Item("chkPercen") & "&chkAmount=" & Request.QueryString.Item("chkAmount")
		lstrQueryString = lstrQueryString & "&tcnFacAmount=" & Request.QueryString.Item("tcnFacAmount")
		lstrQueryString = lstrQueryString & "&sPriority=" & sPriority & "&sIsFACOB=1" & "&sIsCOB=2"
		
		.sEditRecordParam = lstrQueryString
		.sQueryString = lstrQueryString
		
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sIsFACOB") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.actionQuery = Session("bQuery")
	End With
End Sub

'%insPreCA021A:
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021A()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lcolContrproc_cover_co_gs As ePolicy.Contrproc_cover_co_gs
	Dim lclsContrproc_cover_co_g As ePolicy.Contrproc_Cover_co_g
	Dim mcolContrproc_cover_co_gs As ePolicy.Contrproc_cover_co_gs
	Dim lintCount As Integer
	Dim lintIndex As Short
	
	'+ LLENA EL ENCABEZADO PUNTUAL...
	lclsContrproc_cover_co_g = New ePolicy.Contrproc_Cover_co_g
	
	
	'If lclsContrproc_cover_co_g.Find_Priority(Session("sCertype"), 	'											  Session("nBranch"), 	'											  Session("nProduct"), 	'										      Session("nPolicy"), 	'										      Session("nCertif"), 	'										      Session("dEffecdate")) Then
	
	'If Request.QueryString("sPriority") = "" Then 
	'	If lclsContrproc_cover_co_g.nPriority = 0 Then
	'		sPriority = "1"
	'	Else
	'		sPriority = lclsContrproc_cover_co_g.nPriority
	'	End If
	'End If
	
	'If lclsContrproc_cover_co_g.nPriority = 1 Then
	'	If lclsContrproc_cover_co_g.nCapital_Rei <> eRemoteDB.Constants.intNull Then
	'		sAmount = "1"
	'		sPercent = "2"
	'		nFacAmount  = lclsContrproc_cover_co_g.nCapital_Rei
	'		lblEnabled = False
	'	End If 
	'	If lclsContrproc_cover_co_g.nShare_Rei <> eRemoteDB.Constants.intNull Then
	'		sPercent = "1"
	'		sAmount = "2"
	'		nFacPerc    = lclsContrproc_cover_co_g.nShare_Rei
	
	'		nContPerc   = 100 - lclsContrproc_cover_co_g.nShare_Rei
	'		lblEnabled = False
	'	End If 
	'Else
	'	If lclsContrproc_cover_co_g.nCapital_Rei <> eRemoteDB.Constants.intNull Then
	'		sAmount = "1"
	'		sPercent = "2"
	'		nContAmount = lclsContrproc_cover_co_g.nCapital_Rei
	'		lblEnabled = False
	'	End If 
	'	If lclsContrproc_cover_co_g.nShare_Rei <> eRemoteDB.Constants.intNull Then
	'		sPercent = "1"
	'		sAmount = "2"
	'		lblEnabled = False
	'		nFacPerc   = lclsContrproc_cover_co_g.nShare_Rei
	
	'		
	'		nContPerc  = 100 - lclsContrproc_cover_co_g.nShare_Rei
	'	End If 
	'End If
	'End If
	Response.Write(mobjValues.HiddenControl("tcnContAmount", CStr(0)))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE>" & vbCrLf)
Response.Write("		<TR WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TD WIDTH=""30%""><LABEL ID=""0"">" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%""><LABEL ID=""0"">" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TD WIDTH=""30%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.CheckControl("chkPercen", GetLocalResourceObject("chkPercenCaption"), sPercent, "1", "onClickValueP()", False,  , ""))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.CheckControl("chkAmount", GetLocalResourceObject("chkAmountCaption"), sAmount, "1", "onClickValueA()", False,  , ""))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TD WIDTH=""30%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.CheckControl("chkContract", GetLocalResourceObject("chkContractCaption"), CStr(3 - CDbl(sPriority)), "1", "onClickValueC()", False,  , ""))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.NumericControl("tcnContPer", 10, nContPerc,  , GetLocalResourceObject("tcnContPerToolTip"),  , 2,  ,  ,  , "changePercent(this)", False))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<!--TD WIDTH=""20%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.NumericControl("tcnContAmount", 10, nContAmount,  , GetLocalResourceObject("tcnContAmountToolTip"),  , 6,  ,  ,  , "changeAmount(this)", False))


Response.Write("" & vbCrLf)
Response.Write("			<TD-->" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TD WIDTH=""30%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.CheckControl("chkFacultativo", GetLocalResourceObject("chkFacultativoCaption"), sPriority, "1", "onClickValueF()", False,  , ""))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.NumericControl("tcnFacPer", 10, nFacPerc,  , GetLocalResourceObject("tcnFacPerToolTip"),  , 2,  ,  ,  , "changePercent(this)", False))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.NumericControl("tcnFacAmount", 10, nFacAmount,  , GetLocalResourceObject("tcnFacAmountToolTip"),  , 6,  ,  ,  , "changeAmount(this)", False))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%""><LABEL ID=""0"">" & GetLocalResourceObject("nCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""15%"">" & vbCrLf)
Response.Write("  			  	")

	mobjValues.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write("" & vbCrLf)
Response.Write("  			  	")

	mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write("" & vbCrLf)
Response.Write("  			  	")

	mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write("" & vbCrLf)
Response.Write("  			  	")

	mobjValues.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write("" & vbCrLf)
Response.Write("  			  	")

	mobjValues.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write("" & vbCrLf)
Response.Write("  			  	")

	mobjValues.BlankPosition = True
Response.Write("" & vbCrLf)
Response.Write("				")

	'mobjValues.Parameters.ReturnValue "nCover",True,"nCover",True
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("nCover", "tabCover_co_g", eFunctions.Values.eValuesType.clngComboType, mintnCover, True,  ,  ,  ,  , "insReload(this);", lblEnabled,  , GetLocalResourceObject("nCoverToolTip"),  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=""5"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""8"" CLASS=""HighLighted""><LABEL><A NAME=""Coberturas"">" & GetLocalResourceObject("AnchorCoberturasCaption") & "</A></LABEL></td>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""100%"" COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR><TD>&nbsp;</TD></TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	
	'+ LLENA EL GRID DE LAS COBERTURAS...
	lcolContrproc_cover_co_gs = New ePolicy.Contrproc_cover_co_gs
	
	lintIndex = 0
	If lcolContrproc_cover_co_gs.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
        For each item As ePolicy.Contrproc_Cover_co_g In lcolContrproc_cover_co_gs
			With mobjGridC
				.Columns("tcncover").DefValue = item.sCoverDesc
				.Columns("tctContract").DefValue = item.sDesc_Contrato
				.Columns("tcnContract").DefValue = CStr(item.nNumber)
				.Columns("tctBrancht").DefValue = item.sBranch_reiDes
				.Columns("tcdContract").DefValue = CStr(item.dDate_Contrato)
				.Columns("tcnQuota_sha").DefValue = CStr(item.nShare)
				.Columns("tcnCapital").DefValue = CStr(item.nCapital)
				.Columns("hddnType").DefValue = CStr(item.nType)
				
				.sEditRecordParam = "sIsCOB=1" & "&sIsFACOB=2" & "&tcnContPer=" & Request.QueryString.Item("tcnContPer") & "&tcnFacAmount=" & Request.QueryString.Item("tcnFacAmount") & "&tcnFacPer=" & Request.QueryString.Item("tcnFacPer") & "&sPriority=" & Request.QueryString.Item("sPriority") & "&chkPercen=" & Request.QueryString.Item("chkPercen") & "&chkAmount=" & Request.QueryString.Item("chkAmount") & "&nType=' + marrArrayC[" & CStr(lintIndex) & "].hddnType + '"
				
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next 
		
	End If
	
	Response.Write(mobjGridC.closeTable())
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=""5"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""8"" CLASS=""HighLighted""><LABEL><A NAME=""Facultativo"">" & GetLocalResourceObject("AnchorFacultativoCaption") & "</A></LABEL></td>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""100%"" COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>	")

	
	'+ LLENA EL GRID DE LOS FACULTATIVOS...	
	mcolContrproc_cover_co_gs = New ePolicy.Contrproc_cover_co_gs

	If mcolContrproc_cover_co_gs.Find_Facultativo(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), CInt(Request.QueryString.Item("ncover")), Session("dEffecdate")) Then

        For each item As ePolicy.Contrproc_Cover_co_g In mcolContrproc_cover_co_gs
			With mobjGridF

				.Columns("cbeCompany").DefValue = CStr(item.nCompany)
				.Columns("tcnClasific").DefValue = CStr(item.nClasific)
				'.Columns("tcnParticip").DefValue    = item.nCapital
				.Columns("tcnPercentage").DefValue = CStr(item.nShare)
				.Columns("tcnComission").DefValue = CStr(item.nCommissi)
				.Columns("tcnReser_rate").DefValue = CStr(item.nReser_rate)
				.Columns("tcnInter_rate").DefValue = CStr(item.nInter_rate)
				.Columns("tcdAcceptDate").DefValue = CStr(item.dAcceDate)

				Response.Write(.DoRow)
			End With
		Next 
	End If
	
	Response.Write(mobjGridF.closeTable())
End Sub

'------------------------------------------------------------------------------------------------------------------------------------------------------
'Private Sub insPreCA021AUpd
'------------------------------------------------------------------------------------------------------------------------------------------------------
'    If Request.QueryString("sIsFACOB") = "1" Then
'        Call insPreCA021AUpdF()
'    Elseif Request.QueryString("sIsFACOB") = "2" Then
'        Call insPreCA021AUpdC()
'    Elseif Request.QueryString("sIsFACOB") = "3" Then        
'        Call insPreCA021AUpdCov()
'    End If
'End sub

'------------------------------------------------------------------------------
'---------------------------- Tratamiento de las ventanas PopUps --------------
'------------------------------------------------------------------------------

'+Grid de Coberturas
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021AUpdCov()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	If Request.QueryString.Item("Action") <> "Del" Then
		Response.Write("<SCRIPT>setTimeout('ChangeValue()', 100);</" & "Script>")
	End If
	With Request
		Response.Write(mobjValues.HiddenControl("blnContract", CStr(False)))
		Response.Write(mobjValues.HiddenControl("tctSetting", ""))
		Response.Write(mobjValues.HiddenControl("tctPopUpT", "Cov"))
		Response.Write(mobjValues.HiddenControl("hddTypeRel", ""))
		Response.Write("<SCRIPT>self.document.forms[0].hddTypeRel.value = 1;</" & "Script>")
		Response.Write(mobjGridCov.DoFormUpd(.QueryString("Action"), "valPolicySeq.aspx", .QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.actionQuery, .QueryString("Index")))
	End With
End Sub

'+Grid de Contratos
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021AUpdC()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGridC.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
		
		Response.Write(mobjValues.HiddenControl("blnContract", CStr(True)))
		Response.Write(mobjValues.HiddenControl("tctSetting", ""))
		Response.Write(mobjValues.HiddenControl("tctPopUpT", "C"))
		Response.Write(mobjValues.HiddenControl("tcnCapitalRein", .QueryString.Item("nCapitalRein")))
		Response.Write(mobjValues.HiddenControl("cbeBranchrei", .QueryString.Item("nBranchRei")))
		Response.Write(mobjValues.HiddenControl("tcnModulec", .QueryString.Item("nModulec")))
		Response.Write(mobjValues.HiddenControl("valCover", .QueryString.Item("nCover")))
		Response.Write(mobjValues.HiddenControl("valClient", .QueryString.Item("sClient")))
		Response.Write(mobjValues.HiddenControl("tcnCurrency", .QueryString.Item("nCurrency")))
		Response.Write(mobjValues.HiddenControl("tctHeap_code", .QueryString.Item("sHeapCode")))
	End With
End Sub

'+Grid de Facultativo
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021AUpd()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lclsReinsuran As ePolicy.Reinsuran
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsReinsuran = New ePolicy.Reinsuran
			
			Response.Write(mobjValues.ShowWindowsName("CA021A", Request.QueryString.Item("sWindowDescript")))
			Response.Write(mobjValues.ConfirmDelete())
			With lclsReinsuran
				Call .DelPostCA021A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUsercode"))
			End With
			lclsReinsuran = Nothing
		End If
		
		If Request.QueryString.Item("sIsCOB") = "1" Then
			Response.Write(mobjGridC.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
		Else
			Response.Write(mobjGridF.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
		End If
	End With
End Sub

</script>
<%
Response.Expires = -1441

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "CA021A"

mobjValues.actionQuery = Session("bQuery")

If Not IsNothing(Request.QueryString.Item("ncover")) Then
	lblEnabled = False
	mintnCover = Request.QueryString.Item("ncover")
Else
	lblEnabled = True
	mintnCover = ""
End If

nFacPerc = Request.QueryString.Item("tcnFacPer")
nFacAmount = Request.QueryString.Item("tcnFacAmount")
nContPerc = Request.QueryString.Item("tcnContPer")
nContAmount = Request.QueryString.Item("tcnContAmount")
sPercent = Request.QueryString.Item("chkPercen")
sAmount = Request.QueryString.Item("chkAmount")
sPriority = Request.QueryString.Item("sPriority")

%>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>


    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CA021A"))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), mobjValues.StringToType(Request.QueryString.Item("nWindowTy"), eFunctions.Values.eTypeData.etdDouble)))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
primera = "S"
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCA021A" ACTION="ValPolicyseq.aspx?blnMassive=True&nCover=<%=mintnCover%>&sPriority=<%=Request.QueryString.Item("sPriority")%>">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	    <P ALIGN="CENTER">						
	        <LABEL><A HREF="#Coberturas"> <%= GetLocalResourceObject("AnchorCoberturas2Caption") %></A></LABEL><LABEL> | </LABEL>
	        <LABEL><A HREF="#Contratos"> <%= GetLocalResourceObject("AnchorContratosCaption") %></A></LABEL><LABEL> | </LABEL>
	        <LABEL><A HREF="#Facultativo"> <%= GetLocalResourceObject("AnchorFacultativo2Caption") %></A></LABEL>
	    </P>
<%	
End If

If Request.QueryString.Item("Action") <> "Del" Or Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("CA021A", Request.QueryString.Item("sWindowDescript")))
	Response.Write("<BR>")
End If

mclsReinsuran = New ePolicy.Reinsuran

If Not Session("bQuery") Then
	Call mclsReinsuran.insPreCA021A(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdLong))
End If

mclsReinsuran = Nothing

'+Se define el grid de coberturas
'Call insDefineHeaderCov()

'+Se define el grid de contratos     
Call insDefineHeaderC()

'+Se define el grid de facultativo
Call insDefineHeaderF()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA021A()
Else
	Call insPreCA021AUpd()
End If

mobjGridC = Nothing
mobjGridF = Nothing
mobjGridCov = Nothing
mobjValues = Nothing

%>
</FORM>
</BODY>
</HTML>

<SCRIPT>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 18 $|$$Date: 8/09/04 16.12 $|$$Author: Nvaplat60 $"

//%insShowShare.Para calcular el porcentaje de participación
//-----------------------------------------------------------------------------
function insShowShare(lintCapital){
//-----------------------------------------------------------------------------
    var lintValue 
 
	if((lintCapital!='')&&(lintCapital!='0')){
		lintValue = (insConvertNumber(lintCapital) * 100) / insConvertNumber(document.forms[0].tcnCapitalRein.value);
	}else{
		lintValue = 0;
	}

	if (lintValue > 100){
		alert('El monto excede el 100% de participación del reaseguro');
		document.forms[0].tcnParticip.value = 0;
		document.forms[0].tcnPercentage.value = 0;
	}else{
		document.forms[0].tcnParticip.value = lintCapital;
		document.forms[0].tcnPercentage.value = VTFormat(lintValue,'','','',6,true);
		$(document.forms[0].tcnPercentage).change();
	}
}

//ChangeValue: Re-calcula el monto "Por Distribuir" en el grid "mobjGridCov"
//-----------------------------------------------------------------------------
function ChangeValue(){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
		ldblAmount = insConvertNumber(tcnReinCapital.value) - insConvertNumber(tcnRetention.value);
		tcnAmount.value = VTFormat(ldblAmount,'','','',6,true); 
		if(ldblAmount<0)
			tcnAmount.value=VTFormat(0,'','','',6,true); 
		else 
			$(tcnAmount).change(); 
	}
}

//ChangeValue: Re-calcula el monto "Por Distribuir" en el grid "mobjGridCov"
//-----------------------------------------------------------------------------
function changeCover(){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(cbecover.value > 0){
			insDefValues("Find_Cover", "nCover=" + cbecover.value, '/VTimeNet/Policy/PolicySeq')
		}
	}
}
//%onClickValueP: Funcion que valida la caja de texto a habilitar
//--------------------------------------------------------------------------------------------
function onClickValueP(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(chkPercen.checked == true){
			if(chkAmount.checked == true){
				chkAmount.checked = false;
			}
			if(chkFacultativo.checked == true){
				tcnFacPer.disabled = false;
				tcnContPer.disabled = true;
				tcnContAmount.disabled = true;
				tcnFacAmount.disabled = true;
			}
			if(chkContract.checked == true){
				tcnFacPer.disabled = true;
				tcnContPer.disabled = false;
				tcnContAmount.disabled = true;
				tcnFacAmount.disabled = true;
			}
			tcnContAmount.value = "";
			tcnFacAmount.value = "";
		}
	}
}
//%onClickValueA: Funcion que valida la caja de texto a habilitar
//--------------------------------------------------------------------------------------------
function onClickValueA(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(chkAmount.checked == true){
			if(chkPercen.checked == true){
				chkPercen.checked = false;
			}
			if(chkFacultativo.checked == true){
				tcnFacPer.disabled = true;
				tcnContPer.disabled = true;
				tcnContAmount.disabled = true;
				tcnFacAmount.disabled = false;
				tcnContAmount.value = "";
			}
			if(chkContract.checked == true){
				tcnFacPer.disabled = true;
				tcnContPer.disabled = true;
				tcnContAmount.disabled = false;
				tcnFacAmount.disabled = true;
				tcnFacAmount.value = "";
				
			}
			tcnFacPer.value = "";
			tcnContPer.value = "";
		}
	}
}
//%onClickValueC: Funcion que valida la caja de texto a habilitar
//--------------------------------------------------------------------------------------------
function onClickValueC(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(chkContract.checked == true){
			if(chkFacultativo.checked == true){
				chkFacultativo.checked = false;
			}
			if(chkAmount.checked == true){
				tcnFacPer.disabled = true;
				tcnContPer.disabled = true;
				tcnContAmount.disabled = false;
				tcnFacAmount.disabled = true;
				tcnFacAmount.value = "";
			}
			if(chkPercen.checked == true){
				tcnFacPer.disabled = true;
				tcnContPer.disabled = false;
				tcnContAmount.disabled = true;
				tcnFacAmount.disabled = true;
			}
		}
	}
}
//%onClickValueF: Funcion que valida la caja de texto a habilitar
//--------------------------------------------------------------------------------------------
function onClickValueF(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(chkFacultativo.checked == true){
			if(chkContract.checked == true){
				chkContract.checked = false;
			}
			if(chkAmount.checked == true){
				tcnFacPer.disabled = true;
				tcnContPer.disabled = true;
				tcnContAmount.disabled = true;
				tcnFacAmount.disabled = false;
				tcnContAmount.value = "";
			}
			if(chkPercen.checked == true){
				tcnFacPer.disabled = false;
				tcnContPer.disabled = true;
				tcnContAmount.disabled = true;
				tcnFacAmount.disabled = true;
			}
		}
	}
}
//%changeAmount: Valida que el valor ingresado sea mayor a cero
//--------------------------------------------------------------------------------------------
function changeAmount(nAmount){
//--------------------------------------------------------------------------------------------
	if(nAmount.value <= 0){
		alert('Ingrese valor mayor que cero');
	}else{
		self.document.forms[0].nCover.disabled = false;
	}
}
//%changeAmount: Valida que el valor ingresado sea mayor a cero
//--------------------------------------------------------------------------------------------
function changePercent(nPercent){
//--------------------------------------------------------------------------------------------
var nvarpercent;
nvarpercent = ''; 

	if (nPercent.value != ''){
		with(self.document.forms[0]){
			if (tcnFacPer.disabled == false && tcnFacPer.value != ''){
					nvarpercent = 'tcnContPer';
			}
			if (tcnContPer.disabled == false && tcnContPer.value != ''){
					nvarpercent = 'tcnFacPer';
			}
		}
		insDefValues("Subtraction", "nvalue=" + nPercent.value + "&ndesvalue=" + nvarpercent , '/VTimeNet/Policy/PolicySeq')
	}
}
//%insMakeURLCA021A: Funcion que es invocada desde el boton del grid de coberturas, para que recarge de la página
//--------------------------------------------------------------------------------------------
function insMakeURLCA021A(nBranch_rei, nModulec, nCover, sClient){
//--------------------------------------------------------------------------------------------
    var lstrLocation=document.location.href.replace(/sOnSeq=1.*/,'sOnSeq=1')
    
    lstrLocation = lstrLocation + "&nBranchRei=" + nBranch_rei + "&nModulec=" + nModulec + "&nCover=" + nCover + "&sClient=" + sClient + "&nMode=4";
    document.location.href = lstrLocation;
}

//%insReload: Recarga la página al seleccionar cobertura
//--------------------------------------------------------------------------------------------
function  insReload(Field){
//--------------------------------------------------------------------------------------------    
    var lstrLocation = document.location.href;
    var lstrstring;
    var lintPriority;
    var lintsPercen
    var lintsAmount
    var frm;
    
    frm = self.document.forms[0];
    if (frm.chkContract.checked == true){
		lintPriority = 2
    }
    else{
		if (frm.chkFacultativo.checked == true)
			lintPriority = 1
    }
    if (frm.chkAmount.checked == true)
		lintsAmount = 1;
    else
		lintsAmount = 2;
	
	if (frm.chkPercen.checked == true)
		lintsPercen = 1;
    else
		lintsPercen = 2;
    
	lstrLocation = lstrLocation.replace(/&nCover=.*/,'')
    with (frm){
    lstrstring = "&nCover=" + Field.value
				 + "&tcnContPer=" + tcnContPer.value 
				 + "&tcnContAmount=" + tcnContAmount.value
				 + "&tcnFacPer=" + tcnFacPer.value 
				 + "&tcnFacAmount=" + tcnFacAmount.value
				 + "&chkAmount=" + lintsAmount
				 + "&sPriority=" + lintPriority
				 + "&chkPercen=" + lintsPercen;

    }
    document.location.href = lstrLocation + lstrstring;
}

//%insReload: Recarga la página al seleccionar cobertura
//--------------------------------------------------------------------------------------------
function  insload(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (chkPercen.checked == true){
			onClickValueP();
		}else{
			onClickValueA();
		}
    }
}
</SCRIPT>
<SCRIPT>insload();</SCRIPT>




