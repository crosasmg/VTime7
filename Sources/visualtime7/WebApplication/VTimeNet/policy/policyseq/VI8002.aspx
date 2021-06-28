<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjProduct_li As Object

Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
Dim mobjCertificat As ePolicy.Certificat
Dim mobjLife As ePolicy.Apv_origin
Dim mclsProduct As eProduct.Product

Dim mobjCurren_pol As ePolicy.Curren_pol
Dim mintMonth As Object


'+Se definen las variables locales que nos permiten manejar campos a habilitar o deshabilitar segun su contenido

Dim lOptClient_work As Object
Dim lOptClient_ind As Object
Dim ldtmDate_work As Object
Dim lblnDisable As Boolean
Dim lblnAFPdis As Boolean
Dim lintYear As Object
Dim lintMonth As Object
Dim lintAct_date As Object
Dim lOptDirect As Object


'% InsDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineHeaderA()
	'--------------------------------------------------------------------------------------------
	Dim lobjColumn As Object
	Dim mobjGrid2 As eFunctions.Columns
	Dim ldtmDate_work As Object
	mobjGrid2 = New eFunctions.Columns
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("valOriginColumnCaption"), "valOrigin", "TAB_ORD_ORIGIN", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valOriginColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		'      .AddNumericColumn 0, "%", "tcnPercent",5, vbNullString, , "Porcentaje de ahorro",True, 2,,,"insChangeValuesPop('tcnPercent', this.value, '')"
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremDeal_anuColumnCaption"), "tcnPremDeal_anu", 18, vbNullString,  , GetLocalResourceObject("tcnPremDeal_anuColumnToolTip"), True, 6,  ,  , "insChangeValuesPop('tcnPremDeal_anu', this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremDealColumnCaption"), "tcnPremDeal", 18, vbNullString,  , GetLocalResourceObject("tcnPremDealColumnToolTip"), True, 6,  ,  ,  , True)
		.AddHiddenColumn("hddMonth", mintMonth)
		.AddHiddenColumn("tcnPercent", "")
	End With
	mobjGrid.Columns("valOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjGrid.Columns("valOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		
		.Codispl = "VI8002"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("valOrigin").EditRecord = True
		.Columns("valOrigin").Disabled = Request.QueryString.Item("Action") = "Update"
		.Height = 300
		.Width = 380
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		
		
		.sEditRecordParam = "nDepend='        + self.document.forms[0].chkDepend.value                    + '" & "&nIndep='        + self.document.forms[0].chkIndep.value                     + '" & "&nAct_date='     + self.document.forms[0].hddAct_date.value                  + '" & "&sClient='       + self.document.forms[0].hddClient.value                    + '" & "&nOption='       + self.document.forms[0].valOption.value                    + '" & "&nTaxRegime='    + self.document.forms[0].valTyp_ProfitWorker.value          + '" & "&nCapital='      + insConvertNumber(self.document.forms[0].tcnCapital.value) + '" & "&nYear='         + self.document.forms[0].tcnYear.value     + '" & "&nMonth='        + self.document.forms[0].tcnMonth.value        + '" & "&nYearMonth='    + self.document.forms[0].hddYearMonth.value        + '" & "&sFolio='        + self.document.forms[0].tctFolio.value + '" & "&nPayfreq='      + self.document.forms[0].hddPayFreq.value + '" & "&nWaypay='    + self.document.forms[0].hddWayPay.value                    + '" & "&dDate_work=' + self.document.forms[0].tcdDate_origi.value                + '"
		
		If mobjLife.nAFP <= 0 Then
			.sEditRecordParam = .sEditRecordParam & "&hAFP=' + self.document.forms[0].cbeAFP.value           + '" & "&nAFP=0"
		Else
			.sEditRecordParam = .sEditRecordParam & "&hAFP=' + self.document.forms[0].hddAfp.value           + '" & "&nAFP=' + self.document.forms[0].hddAfp.value           + '"
			
		End If
		
		If mobjLife.dDependant <> eRemoteDB.Constants.dtmNull And mobjLife.dIndependant <> eRemoteDB.Constants.dtmNull Then
			.sEditRecordParam = .sEditRecordParam & "&dDate_work='  + self.document.forms[0].tcdDate_origi.value        + '"
		Else
			.sEditRecordParam = .sEditRecordParam & "&dDate_work='  + self.document.forms[0].hddDate_work.value        + '"
		End If
		
		.sDelRecordParam = "Popup=A&nOrigin=' + marrArray[lintIndex].valOrigin +  '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	
End Sub


'% InsPreVI8002B: Se realiza el manejo de las edades
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI8002B()
	With mobjValues
		Call mclsProduct.insInitialVI7001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"))
	End With
End Sub
'--------------------------------------------------------------------------------------------

'% InsPreVI8002A: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI8002A()
	'--------------------------------------------------------------------------------------------
	'- Objetos para el manejo de los datos repetitivos de la página
	Dim lcolApv_origin As ePolicy.Apv_origins
	Dim lclsApv_origin As Object
	Dim ldblPrem_deal As Object
	Dim llngYear_end As Object
	
	lcolApv_origin = New ePolicy.Apv_origins
	If lcolApv_origin.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), 0) Then
		For	Each lclsApv_origin In lcolApv_origin
			With mobjGrid
				.Columns("valOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valOrigin").DefValue = lclsApv_origin.nOrigin
				.Columns("tcnPercent").DefValue = lclsApv_origin.nPercent
				.Columns("tcnPremDeal_anu").DefValue = lclsApv_origin.nPremDeal_anu
				.Columns("tcnPremDeal").DefValue = lclsApv_origin.nPremDeal
				
				Response.Write(.DoRow)
			End With
		Next lclsApv_origin
	End If
	Response.Write(mobjGrid.closeTable())
	lcolApv_origin = Nothing
	lclsApv_origin = Nothing
	
End Sub
'% InsPreVI8002AUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI8002AUpd()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la VI8002" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function UpdateFieldsA(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------")

	If Request.QueryString.Item("Type") <> "PopUp" Then
Response.Write("" & vbCrLf)
Response.Write("    with(self.document.forms[0]){" & vbCrLf)
Response.Write("        hddnDepend.value = top.opener.document.forms[0].chkDepend.value;" & vbCrLf)
Response.Write("        hddnIndep.value = top.opener.document.forms[0].chkIndep.value;        " & vbCrLf)
Response.Write("        hdddtcdDate_origi.value = top.opener.document.forms[0].tcdDate_origi.value;" & vbCrLf)
Response.Write("        hdddtnAct_date.value = top.opener.document.forms[0].hddAct_date.value;" & vbCrLf)
Response.Write("        hddsClient.value = top.opener.document.forms[0].tctClient.value;" & vbCrLf)
Response.Write("        hddnAFP.value = top.opener.document.forms[0].cbeAFP.value;" & vbCrLf)
Response.Write("        hddhAFP.value = top.opener.document.forms[0].hddAFP.value;" & vbCrLf)
Response.Write("        hddnOption.value = top.opener.document.forms[0].valOption.value;" & vbCrLf)
Response.Write("        hddnTaxRegime.value = top.opener.document.forms[0].valTyp_ProfitWorker.value;" & vbCrLf)
Response.Write("        hddnCapital.value = top.opener.document.forms[0].tcnCapital.value;" & vbCrLf)
Response.Write("		hddnYear.value = top.opener.document.forms[0].tcnYear.value;" & vbCrLf)
Response.Write("		hddnMonth.value = top.opener.document.forms[0].tcnMonth.value;" & vbCrLf)
Response.Write("		hddnYearMonth.value = top.opener.document.forms[0].hddYearMonth.value;" & vbCrLf)
Response.Write("		hddsFolio.value = top.opener.document.forms[0].tctFolio.value;" & vbCrLf)
Response.Write("    }													 ")

	End If
Response.Write("" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lclsApv_origin As ePolicy.Apv_origin
	
	lclsApv_origin = New ePolicy.Apv_origin
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsApv_origin.InsPostVI8002Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdLong), 0, 0, 0, Session("dNulldate"), Session("nUsercode")) Then
			End If
		End If
		mobjGrid.Columns("valOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("valOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI8002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
	End With
	
	With Response
		
		.Write(mobjValues.HiddenControl("hddnDepend", Request.QueryString.Item("nDepend")))
		.Write(mobjValues.HiddenControl("hddnIndep", Request.QueryString.Item("nIndep")))
		.Write(mobjValues.HiddenControl("hdddtcdDate_origi", Request.QueryString.Item("dDate_work")))
		.Write(mobjValues.HiddenControl("hdddtnAct_date", Request.QueryString.Item("nAct_date")))
		.Write(mobjValues.HiddenControl("hddsClient", Request.QueryString.Item("sClient")))
		.Write(mobjValues.HiddenControl("hddnAFP", Request.QueryString.Item("nAFP")))
		.Write(mobjValues.HiddenControl("hddhAFP", Request.QueryString.Item("hAFP")))
		.Write(mobjValues.HiddenControl("hddnOption", Request.QueryString.Item("nOption")))
		.Write(mobjValues.HiddenControl("hddnTaxRegime", Request.QueryString.Item("nTaxRegime")))
		.Write(mobjValues.HiddenControl("hddnCapital", Request.QueryString.Item("nCapital")))
		.Write(mobjValues.HiddenControl("hddnYear", Request.QueryString.Item("nYear")))
		.Write(mobjValues.HiddenControl("hddnMonth", Request.QueryString.Item("nMonth")))
		.Write(mobjValues.HiddenControl("hddnYearMonth", Request.QueryString.Item("nYearMonth")))
		.Write(mobjValues.HiddenControl("hddsFolio", Request.QueryString.Item("sFolio")))
		.Write("<SCRIPT>UpdateFieldsA()</" & "Script>")
	End With
	lclsApv_origin = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
mclsProduct = New eProduct.Product

Call InsPreVI8002B()
%>   



	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT LANGUAGE=javascript>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: $|$$Date: $|$$Author: $"

//% insChangeValues: Se controla el estado de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeValues(Option, Field){
//-------------------------------------------------------------------------------------------
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
	with(self.document.forms[0]){
		switch(Option){
			case "chkDepend":
				if (Field.checked)
				{
				    chkIndep.checked       = false; 
				    chkIndep.value         = 2;
				    chkDepend.value        = 1;
				    tcdDate_origi.disabled = false;
				    hddAct_date.value= 1;
				}

//+Si no estan seleccionados ninguno de los campos de tipo de trabajo se blanquean las variables

				if ((!Field.checked)&&(!chkIndep.checked)) 
				{
				    tcdDate_origi.value    = '';
				    hddAct_date.value      = 0;
				    chkIndep.value         = 2;
				    chkDepend.value        = 2;
				    tcdDate_origi.disabled = true;
				}
				
				break;
			case "chkIndep":
				if (Field.checked)
				{ 
				    hddAct_date.value = 2;
				    chkDepend.checked = false;
				    chkIndep.value         = 1;
				    chkDepend.value        = 2;				    
				    tcdDate_origi.disabled=false;
				}

//+Si no estan seleccionados ninguno de los campos de tipo de trabajo se blanquean las variables				

				if ((!Field.checked)&&(!chkDepend.checked))
				{
				    tcdDate_origi.value=''; 
				    hddAct_date.value = 0;
				    chkIndep.value         = 2;
				    chkDepend.value        = 2;				    
				    tcdDate_origi.disabled=true;
				}
				break;

			case "tcnMonth":
			    if ((Field.value > 0)&&(tcnYear.value > 0))
			    {
			        hddYearMonth.value = (insConvertNumber(tcnYear.value) * 100) + insConvertNumber(tcnMonth.value);
			    }
			    else
			    {
			        hddYearMonth.value = 0;
			    }
               break;
               
			case "tcnYear":
			    if ((Field.value > 0)&&(tcnMonth.value > 0))
			    {
                    hddYearMonth.value = (insConvertNumber(tcnYear.value) * 100) + insConvertNumber(tcnMonth.value);			    
                }                    
			    else
			    {
			        hddYearMonth.value = 0;
			    }

               break;
		}		
	}
<%End If%>
}
//% insChangeValues: Se controla el estado de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeValuesPop(Option, Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){	
		switch(Option){
			case "tcnPercent":
				if (Field > 0)
				{		
				    tcnPremDeal_anu.value = 0;
				    tcnPremDeal.value = 0;
				}
				break;
			case "tcnPremDeal_anu":
			    if (insConvertNumber(Field) > 0)
			    {
                   tcnPremDeal.value = (parseInt((insConvertNumber(tcnPremDeal_anu.value) / insConvertNumber(hddMonth.value))*1000000)/1000000);
                   tcnPremDeal.value = VTFormat(tcnPremDeal.value,'','','',6,true);
//                   tcnPercent.value = 0;
                }

               break;
		}
	}
}

</SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI8002º" ACTION = "valPolicySeq.aspx?nMainAction=301&amp;nHolder=1">

<%
mobjCertificat = New ePolicy.Certificat
mobjLife = New ePolicy.Apv_origin
mobjCurren_pol = New ePolicy.Curren_pol

mobjValues.ActionQuery = Session("bQuery")

Call mobjCurren_pol.findCurrency(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))

Call mobjCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), True)
Select Case (mobjCertificat.nPayfreq)
	Case 2
		
		mintMonth = 2
	Case 3
		
		mintMonth = 4
	Case 4
		
		mintMonth = 6
	Case 5
		
		mintMonth = 12
	Case Else
		mintMonth = 1
End Select

Call mobjLife.findVI8002(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))

If Request.QueryString.Item("sReloadPage") = "1" And Request.QueryString.Item("Type") <> "PopUp" Then
	lOptClient_work = 2
	lOptClient_ind = 2
	lblnDisable = True
	
	lintAct_date = mobjValues.StringToType(Request.QueryString.Item("nAct_date"), eFunctions.Values.eTypeData.etdInteger)
	
	If lintAct_date = 1 Or lintAct_date = 3 Then
		mobjLife.dDependant = mobjValues.StringToType(Request.QueryString.Item("dDate_work"), eFunctions.Values.eTypeData.etdDate)
		lOptClient_work = 1
	End If
	If lintAct_date = 2 Or lintAct_date = 4 Then
		mobjLife.dIndependant = mobjValues.StringToType(Request.QueryString.Item("dDate_work"), eFunctions.Values.eTypeData.etdDate)
		lOptClient_ind = 1
	End If
	If lintAct_date = 0 Or lintAct_date = 1 Or lintAct_date = 2 Then
		lblnDisable = False
	End If
	
	ldtmDate_work = mobjValues.StringToType(Request.QueryString.Item("dDate_work"), eFunctions.Values.eTypeData.etdDate)
	
	lblnAFPdis = True
	
	If CDbl(Request.QueryString.Item("nAFP")) = 0 Then
		lblnAFPdis = False
	End If
	mobjLife.nAFP = CInt(Request.QueryString.Item("hAFP"))
	
	mobjLife.nOption = mobjValues.StringToType(Request.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdLong)
	mobjLife.nTaxregime = mobjValues.StringToType(Request.QueryString.Item("nTaxRegime"), eFunctions.Values.eTypeData.etdDouble)
	If Request.QueryString.Item("nCapital") <> "NaN" Then
		mobjLife.nCapital = mobjValues.StringToType(Request.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble, True)
	End If
	mobjLife.nYearMonth_fPay = mobjValues.StringToType(Request.QueryString.Item("nYearMonth"), eFunctions.Values.eTypeData.etdDouble)
	mobjLife.sFolionumber = Request.QueryString.Item("sFolio")
Else
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		lOptClient_work = 2
		lOptClient_ind = 2
		lintAct_date = 0
		lblnDisable = False
		
		If mobjLife.dDependant <> eRemoteDB.Constants.dtmNull Then
			ldtmDate_work = mobjLife.dDependant
			lOptClient_work = 1
			lblnDisable = True
			lintAct_date = 3
		End If
		
		If mobjLife.dIndependant <> eRemoteDB.Constants.dtmNull Then
			ldtmDate_work = mobjLife.dIndependant
			lOptClient_ind = 1
			lblnDisable = True
			lintAct_date = 4
		End If
		
		If mobjLife.dIndependant <> eRemoteDB.Constants.dtmNull And mobjLife.dDependant <> eRemoteDB.Constants.dtmNull Then
			If mobjLife.dDependant > mobjLife.dIndependant Then
				lOptClient_ind = 2
				lintAct_date = 3
				ldtmDate_work = mobjLife.dDependant
			Else
				lOptClient_work = 2
				lintAct_date = 4
				ldtmDate_work = mobjLife.dIndependant
			End If
            End If
            
            lblnAFPdis = False
            
		If mobjLife.nAFP > 0 Then
			lblnAFPdis = True
		End If
	End If
End If

If mobjLife.nYearMonth_fPay > 0 Then
	lintYear = CShort(mobjLife.nYearMonth_fPay / 100)
	lintMonth = (mobjLife.nYearMonth_fPay Mod 100)
End If

If Request.QueryString.Item("Type") <> "PopUp" Then
	
	%>
	<TABLE WIDTH="100%">
	    <TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("tctFolioCaption") %></LABEL></TD>
	        <TD>
	        <%=mobjValues.TextControl("tctFolio", 10, mobjLife.sFolionumber, False, GetLocalResourceObject("tctFolioToolTip"),  ,  ,  ,  , False, 1)%>
	        </TD>
	    </TR>
	    
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"><%= GetLocalResourceObject("AnchorVarios0Caption") %></A></LABEL></TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"></A></LABEL></TD>     	    
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"><%= GetLocalResourceObject("AnchorVarios02Caption") %></A></LABEL></TD>     	    
		</TR>			
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD COLSPAN="2"></TD>			
			<TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>	
		<TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkDepend", GetLocalResourceObject("chkDependCaption"), lOptClient_work, lOptClient_work, "insChangeValues(""chkDepend"",this)", lblnDisable,  , GetLocalResourceObject("chkDependToolTip"))%></TD>
			<TD></TD>
			<TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnAgeCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAge", 2, CStr(mclsProduct.nAge),  , GetLocalResourceObject("tcnAgeToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
		</TR>

		<TR>
			<TD><%=mobjValues.CheckControl("chkIndep", GetLocalResourceObject("chkIndepCaption"), lOptClient_ind, lOptClient_ind, "insChangeValues(""chkIndep"",this)", lblnDisable,  , GetLocalResourceObject("chkIndepToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDate_origiCaption") %></LABEL>
 	        <%=mobjValues.DateControl("tcdDate_origi", ldtmDate_work,  , GetLocalResourceObject("tcdDate_origiToolTip"),  ,  ,  ,  , lblnDisable)%>
 	        <%=mobjValues.HiddenControl("hddDate_work", ldtmDate_work)%> 
 	        <%=mobjValues.HiddenControl("hddAct_date", lintAct_date)%> 
 	        <%=mobjValues.HiddenControl("hddClient", mobjLife.sClient)%></TD>
 	        <TD></TD>
 	        <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnAgeReinsuCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAgeReinsu", 2, CStr(mclsProduct.nAge_reinsu),  , GetLocalResourceObject("tcnAgeReinsuToolTip"),  , 0,  ,  ,  ,  , True)%>
			    </TD>
		</TR>
		
		<TR>
			<TD></TD>
			<TD><LABEL ID=0></LABEL></TD>
 	        <TD><LABEL></LABEL></TD>
			 	        <TD COLSPAN="2"><LABEL><%= GetLocalResourceObject("tcnAgeLimitCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAgeLimit", 2, CStr(mclsProduct.nAgeLimit),  , GetLocalResourceObject("tcnAgeLimitToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
		</TR>    
	    
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"><%= GetLocalResourceObject("AnchorVarios03Caption") %></A></LABEL></TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"></A></LABEL></TD>     	    
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"><%= GetLocalResourceObject("tctClient4Caption") %></A></LABEL></TD>     	    
		</TR>			
		
				<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD COLSPAN="2"></TD>			
			<TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>	
			<TD COLSPAN="2"><%=mobjValues.ClientControl("tctClient", mobjLife.sClientBos,  , GetLocalResourceObject("tctClientToolTip"),  , True,  ,  , True,  ,  , eFunctions.Values.eTypeClient.SearchClient, 6)%></TD>
			<TD></TD>
			<TD></TD>
	        <TD COLSPAN="2">
<%	
	
	'+Si no hay contenido en AFP, se crera la etiqueta del campo para que se introduzca valor en el mismo.
	If Not (lblnAFPdis) Then
		%>
			<LABEL><%= GetLocalResourceObject("cbeAFPCaption") %></LABEL>			
<%		
	End If
	%>
			<%=mobjValues.PossiblesValues("cbeAFP", "Table5524", eFunctions.Values.eValuesType.clngComboType, CStr(mobjLife.nAFP), False,  ,  ,  ,  , "insChangeValues(""cbeAFP"",this)", lblnAFPdis,  , GetLocalResourceObject("cbeAFPToolTip"),  , 17)%>
			<%=mobjValues.HiddenControl("hddAfp", CStr(mobjLife.nAFP))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"></TD>
			<TD></TD>
			<TD></TD>			
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAFPCommiCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAFPCommi", 18, CStr(mobjCertificat.nAFP_Commiss), False, GetLocalResourceObject("tcnAFPCommiToolTip"), True, 6, True,  ,  ,  , True)%></TD>
		</TR>

		<TR>
			<TD COLSPAN="2"></TD>
			<TD><LABEL></LABEL></TD>
			<TD><LABEL></LABEL></TD>
			<TD COLSPAN="2"></TD>

		</TR>			
		<TR>
			<TD></TD>
			<TD><LABEL ID=0></LABEL></TD>
		</TR>	
		<TR>
			<TD COLSPAN="6" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios"><%= GetLocalResourceObject("AnchorVariosCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="6" CLASS="Horline"></TD>	 			
		</TR>		
		<TR>
            <TD><LABEL ID=13050><%= GetLocalResourceObject("cbeCurrencDesCaption") %></LABEL></TD>
            <TD>
            <%	
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrencDes", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCurren_pol.nCurrency), False, True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencDesToolTip")))
	%>			
			</TD>
			<TD COLSPAN="2"><LABEL></LABEL></TD>
			<TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD><% Response.Write(mobjValues.NumericControl("tcnCapital", 12, mobjLife.nCapital , , GetLocalResourceObject("AnchorToolTip"), True, 2, , , , , False))  %>
            <%--<LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>--%>
            </TD>
		</TR>
		<TR>
            <TD><LABEL ID=13050><%= GetLocalResourceObject("valOptionCaption") %></LABEL></TD>
            <TD>
			<%	
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valOption", "TAB_OPTION", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjLife.nOption), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valOptionToolTip")))
	%>			
			</TD>
			<TD COLSPAN="2"></TD>
			<TD><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
			<TD>
				<%=mobjValues.NumericControl("tcnMonth", 2, lintMonth,  , "Mes del primer descuento",  ,  ,  ,  ,  , "insChangeValues(""tcnMonth"",this)")%><LABEL><%= GetLocalResourceObject("tcnYearCaption") %><<%= GetLocalResourceObject("tcnYearCaption") %>LABEL>
				<%=mobjValues.NumericControl("tcnYear", 4, lintYear,  , GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  , "insChangeValues(""tcnYear"",this)")%>
				<%=mobjValues.HiddenControl("hddYearMonth", CStr(mobjLife.nYearMonth_fPay))%>
			</TD>
		</TR>
		<TR>
            <TD><LABEL ID=13050><%= GetLocalResourceObject("valTyp_ProfitWorkerCaption") %></LABEL></TD>
            <TD>
            <%	
	mobjValues.BlankPosition = False
	mobjValues.TypeList = 1
	mobjValues.List = "1,2"
	Response.Write(mobjValues.PossiblesValues("valTyp_ProfitWorker", "Table950", eFunctions.Values.eValuesType.clngComboType, CStr(mobjLife.nTaxregime),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valTyp_ProfitWorkerToolTip")))
	%>			
			
			</TD>
			<TD></TD>
			<TD></TD>
		</TR>
        <TR>	       
		</TR>
		<TR>
     	    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"><%= GetLocalResourceObject("AnchorVarios05Caption") %></A></LABEL></TD>
     	    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"></A></LABEL></TD>     	    
     	    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios0"><%= GetLocalResourceObject("AnchorVarios06Caption") %></A></LABEL></TD>     	    
		</TR>			
		<TR>
			<TD COLSPAN="2" CLASS="Horline"></TD>
			<TD COLSPAN="2"></TD>			
			<TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>
		<TR>	
			<TD><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL><BR><BR>
			    <LABEL><%= GetLocalResourceObject("cbeWayPayCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeWayPay", "Table5002", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nWay_pay), False, True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWayPayToolTip"),  , 17)%>
			    <%=mobjValues.HiddenControl("hddWayPay", CStr(mobjCertificat.nWay_pay))%>
			    <BR><BR>
			    <%=mobjValues.PossiblesValues("cbePayFreq", "table36", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nPayfreq), False, False,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayFreqToolTip"),  , 10)%>
			    <%=mobjValues.HiddenControl("hddPayFreq", CStr(mobjCertificat.nPayfreq))%>			    
			    </TD>
			    <TD></TD>
	        <TD><%End If%></TD>
	        <TD COLSPAN = "2"><%If Request.QueryString.Item("Type") = "PopUp" Then
	Call InsDefineHeaderA()
	Call InsPreVI8002AUpd()
Else
	Call InsDefineHeaderA()
	Call InsPreVI8002A()
End If%></TD>
		</TR>
		<TR><%If Request.QueryString.Item("Type") <> "PopUp" Then%>
			<TD><%=mobjValues.OptionControl(0, "optDirecta", GetLocalResourceObject("optDirecta_Caption"), CStr(mobjLife.nDirect),  ,  , True, 7)%></TD>
			<TD><%=mobjValues.OptionControl(0, "optDirectb", GetLocalResourceObject("optDirectb_Caption"), CStr(mobjLife.nIndirect),  ,  , True, 7)%></TD>
			<TD></TD><%End If%>
			<%=mobjValues.HiddenControl("hddoptDirecta", CStr(mobjLife.nDirect))%>
			<TD></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
mobjValues = Nothing
mobjCertificat = Nothing
mobjLife = Nothing
mobjProduct_li = Nothing
mobjCurren_pol = Nothing
mclsProduct = Nothing
%>





