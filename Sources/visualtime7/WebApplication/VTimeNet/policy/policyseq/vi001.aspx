<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de la tabla Life
Dim mclsProduct As eProduct.Product
Dim mclsProduct_li As eProduct.Product
Dim mclsGroups As ePolicy.Groups
Dim mclsSituation As ePolicy.Situation
Dim mclsCertificat As ePolicy.Certificat
Dim mblnGroups As Boolean
Dim mblnSituation As Boolean
    Dim nTypdurins As Object
    


'% insPreVI001: Realiza la lectura de los campos a mostrar en pantalla
'---------------------------------------------------------------------
    Private Sub insPreVI001()
        '---------------------------------------------------------------------
        Dim mclsRoleses As ePolicy.Roleses
        Dim lblnInitial As Boolean
        With mobjValues
            lblnInitial = mclsProduct.insInitialVI001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
            nTypdurins = mclsProduct.nTypdurins
            If nTypdurins = eRemoteDB.Constants.intNull Then
                nTypdurins = 1
            End If
            mblnGroups = mclsGroups.valGroupExist(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"))
            mblnSituation = mclsSituation.insReaSituation(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"))
            Call mclsProduct_li.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), True)
            If mclsProduct.ncount_insu = eRemoteDB.Constants.intNull Or mclsProduct.ncount_insu = 0 Then
                mclsRoleses = New ePolicy.Roleses
                Call mclsRoleses.Find_Tab_Covrol(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), 0)
                mclsProduct.ncount_insu = mclsRoleses.Count
                mclsRoleses = Nothing
            End If
            Call mclsCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"))
        End With
    End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI001")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsProduct = New eProduct.Product
mclsProduct_li = New eProduct.Product
mclsGroups = New ePolicy.Groups
mclsSituation = New ePolicy.Situation
mclsCertificat = New ePolicy.Certificat
mobjValues.ActionQuery = Session("bQuery")
Call insPreVI001()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

//% insSetParam: Se pasan los parámetros al cargar la página
//-------------------------------------------------------------------------------------------
function insSetParam(nTypDurins, nInsur_Time, nTypDurpay){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
/*+ Se verifica si los campos existen, ya que pueden ser campos numéricos en vez de */
/*+ valores posibles                                                                */
		if(typeof(btntcnInsur_Time)!='undefined'){
			tcnInsur_Time.Parameters.Param4.sValue = cbeTypDurins.value;
		}
		if(typeof(btntcnPay_Time)!='undefined'){
			tcnPay_Time.Parameters.Param4.sValue = tcnInsur_Time.value;
			tcnPay_Time.Parameters.Param5.sValue = cbeTypDurins.value;
			tcnPay_Time.Parameters.Param6.sValue = cbeTypDurpay.value;
		}
	}
}
//% insChangeValues: Se controla el estado de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeValues(Option, nTypDurins, Field) {
//-------------------------------------------------------------------------------------------
    
    with (self.document.forms[0]) {
        switch (Option) {
            case "Dur_insu":
/*+ Se verifica si los campos existen, ya que pueden ser campos numéricos en vez de */
/*+ valores posibles                                                                */

				if(typeof(btntcnPay_Time)!='undefined'){
				    tcnPay_Time.Parameters.Param4.sValue = (tcnInsur_Time.value == '') ? 0 : tcnInsur_Time.value;
					tcnPay_Time.Parameters.Param5.sValue = (cbeTypDurins.value == '') ? 0 : cbeTypDurins.value;
					UpdateDiv('btntcnPay_Time', '');
					tcnPay_Time.value='';
					insDefValues("Pay_Time", "nInsur_Time=" + tcnInsur_Time.value + "&nTypDurins=" + cbeTypDurins.value + "&nTypDurpay=" + cbeTypDurpay.value, '/VTimeNet/Policy/PolicySeq');					
					if(cbeTypDurpay.value=='' ||
					cbeTypDurpay.value==0){
					tcnPay_Time.disabled=true;
					btntcnPay_Time.disabled=true;
					}
				}
				if(typeof(btncbeTypDurpay)!='undefined'){
					cbeTypDurpay.Parameters.Param5.sValue = (tcnInsur_Time.value == '')? 0 : tcnInsur_Time.value;
					cbeTypDurpay.disabled = false;
					btncbeTypDurpay.disabled = false;
					//UpdateDiv('cbeTypDurpayDesc', '');
					//cbeTypDurpay.value='';
				}
				break;

case "Dur_insumix":
    if (typeof (btntcnInsur_Time) != 'undefined') {
        tcnInsur_Time.Parameters.Param4.sValue = (nTypDurins == '') ? 0 : nTypDurins;
    }
    tcnInsur_Time.value = '';

    if (typeof (btncbeTypDurpay) != 'undefined') {
        cbeTypDurpay.value = '';
        UpdateDiv('cbeTypDurpayDesc', '');
        cbeTypDurpay.Parameters.Param4.sValue = (nTypDurins == '') ? 0 : nTypDurins;
        cbeTypDurpay.Parameters.Param5.sValue = 0;
        insDefValues("Pay_Time", "nInsur_Time=" + tcnInsur_Time.value + "&nTypDurins=" + cbeTypDurins.value + "&nTypDurpay=" + cbeTypDurpay.value, '/VTimeNet/Policy/PolicySeq');
    }
    if (typeof (btntcnPay_Time) != 'undefined') {
        UpdateDiv('btntcnPay_Time', '');
        tcnPay_Time.value = '';
        if (cbeTypDurpay.value == '' ||
                       cbeTypDurpay.value == 0) {
            tcnPay_Time.disabled = true;
            btntcnPay_Time.disabled = true;
        }
    }
    break;

			case "Dur_paymix":				
				if(typeof(btntcnPay_Time)!='undefined'){
					if(cbeTypDurpay.value=='' ||
                       cbeTypDurpay.value==0){
						tcnPay_Time.disabled=true;
						btntcnPay_Time.disabled=true;
					}
					else{
						tcnPay_Time.disabled=false;
						btntcnPay_Time.disabled=false;
					}
					tcnPay_Time.value='';
					tcnPay_Time.Parameters.Param4.sValue = (tcnInsur_Time.value == '') ? 0 : tcnInsur_Time.value;
					tcnPay_Time.Parameters.Param5.sValue = (cbeTypDurins.value == '') ? 0 : cbeTypDurins.value;
					tcnPay_Time.Parameters.Param6.sValue = (cbeTypDurpay.value == '') ? 0 : cbeTypDurpay.value;
					UpdateDiv('btntcnPay_Time', '');
					insDefValues("Pay_Time", "nInsur_Time=" + tcnInsur_Time.value + "&nTypDurins=" + cbeTypDurins.value + "&nTypDurpay=" + cbeTypDurpay.value, '/VTimeNet/Policy/PolicySeq');					
					
					
				}
		}
	}
}
//% DisabledFields: Inhabilita los campos de acuerdo a las características del producto
//-------------------------------------------------------------------------------------------
function DisabledFields(sOption){
//-------------------------------------------------------------------------------------------

	switch(sOption){
		case "sId2":
            self.document.forms[0].tcnInsur_Time.disabled=false
			
			if(typeof(self.document.forms[0].btntcnInsur_Time)!='undefined')
			    self.document.forms[0].btntcnInsur_Time.disabled=false		
		
			self.document.forms[0].tcdexpirdat.disabled=true
			self.document.forms[0].btn_tcdexpirdat.disabled=true
			self.document.forms[0].tctInsurTimeRoutine.disabled=true
			break;
		case "sId3":
            self.document.forms[0].tcnInsur_Time.disabled=false
			
			if(typeof(self.document.forms[0].btntcnInsur_Time)!='undefined')
			    self.document.forms[0].btntcnInsur_Time.disabled=false
			    		
			self.document.forms[0].tcdexpirdat.disabled=true
			self.document.forms[0].btn_tcdexpirdat.disabled=true
			self.document.forms[0].tctInsurTimeRoutine.disabled=true
			break;
		case "sId4":
			self.document.forms[0].tcnInsur_Time.disabled=true
			self.document.forms[0].tcdexpirdat.disabled=true
			self.document.forms[0].btn_tcdexpirdat.disabled=true
			self.document.forms[0].tctInsurTimeRoutine.disabled=true
			break;
		case "sId5":
			self.document.forms[0].tcnInsur_Time.disabled=true
			self.document.forms[0].tcdexpirdat.disabled=true
			self.document.forms[0].btn_tcdexpirdat.disabled=true
			self.document.forms[0].tctInsurTimeRoutine.disabled=true
			break;
		case "sId6":
			self.document.forms[0].tcnInsur_Time.disabled=true
			self.document.forms[0].tcdexpirdat.disabled=true
			self.document.forms[0].btn_tcdexpirdat.disabled=true
			self.document.forms[0].tctInsurTimeRoutine.disabled=true
			break;
		case "sId7":
			self.document.forms[0].tcnInsur_Time.disabled=false
			
			if(typeof(self.document.forms[0].btntcnInsur_Time)!='undefined')
			    self.document.forms[0].btntcnInsur_Time.disabled=false			
			
			self.document.forms[0].tcdexpirdat.disabled=true
			self.document.forms[0].btn_tcdexpirdat.disabled=true
			self.document.forms[0].tctInsurTimeRoutine.disabled=true
			break;
	}
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

    <%
    If CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401 Then
        mobjValues.ActionQuery = True
    End If

    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues
        Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
    mobjMenu = Nothing
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
%>  

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI001" ACTION="valPolicySeq.aspx?nMainAction=301&nHolder=1">
	<P ALIGN="Center">    
    <LABEL ID=0><A HREF="#Duración de los pagos"><%= GetLocalResourceObject("AnchorDuración de los pagosCaption") %></A></LABEL>
    <LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
    <LABEL ID=0><A HREF="#Recargos"><%= GetLocalResourceObject("AnchorRecargosCaption") %></A></LABEL>
    </P>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=13530><%= GetLocalResourceObject("cbovalGroupCaption") %></LABEL></TD>
			<TD>
			<%With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbovalGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nGroup), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbovalGroupToolTip"),  , 1))%>
			</TD>
			<TD WIDTH=8%>&nbsp;</TD>
			<TD><LABEL ID=13531><%= GetLocalResourceObject("cbovalSituationCaption") %></LABEL></TD>
			<TD>
			<%With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbovalSituation", "tabSituation", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nSituation), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbovalSituationToolTip"),  , 2))%>
			</TD>
		</TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41076><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41077><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD></TD>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=19338><%= GetLocalResourceObject("tctInsurTimeRoutineCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctInsurTimeRoutine", 12, mclsProduct.sInsurTimeRoutine,  , GetLocalResourceObject("tctInsurTimeRoutineToolTip"),  ,  ,  ,  , True, 3)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=13518><%= GetLocalResourceObject("tcnPerNunMiCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPerNunMi", 18, CStr(mclsProduct.nPernunmi),  , GetLocalResourceObject("tcnPerNunMiToolTip"), True, 6,  ,  ,  ,  , mclsProduct.nProdClas = 1, 11)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypDurinsCaption") %></LABEL></TD>
            <%If mclsProduct_li.nTypdurins = 7 Then%>
				<TD><%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeTypDurins", "tabDurInsu_Prod_mix", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nTypdurins), True,  ,  ,  ,  , "insChangeValues(""Dur_insumix"", this.value, this)",  ,  , GetLocalResourceObject("cbeTypDurinsToolTip")))%>
				</TD>
			<%Else%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypDurins", "Table5589", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct_li.nTypdurins),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypDurinsToolTip")))%></TD>
			<%End If%>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=13521><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCapital", 18, mobjValues.StringToType(CStr(mclsProduct.nCapital_ca), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6,  ,  ,  ,  ,  , 12)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=><%= GetLocalResourceObject("tcnInsur_TimeCaption") %></LABEL></TD>
            <%If mclsProduct_li.sIdurvari = "1" Or mclsProduct_li.nTypdurins = 4 Then%>
				<TD><%=mobjValues.NumericControl("tcnInsur_Time", 5, CStr(mclsProduct.nInsur_Time),  , GetLocalResourceObject("tcnInsur_TimeToolTip"),  ,  ,  ,  ,  , "insChangeValues(""Dur_insu""," & mclsProduct_li.nTypdurins & ", this)", mclsProduct.bInsur_time)%></TD>
			<%Else%>
				<TD><%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("tcnInsur_Time", "Tabdurinsu_prod", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nInsur_Time), True,  ,  ,  ,  , "insChangeValues(""Dur_insu""," & mclsProduct_li.nTypdurins & ", this)", mclsProduct.bInsur_time, 5, GetLocalResourceObject("tcnInsur_TimeToolTip"),  ,  , False))
	%>
				</TD>
			<%End If%>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnrentamountCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnrentamount", 18, mobjValues.StringToType(CStr(mclsProduct.nrentamount), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnrentamountToolTip"), True, 6,  ,  ,  ,  , mclsProduct.nProdClas = 1, 13)%>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdexpirdatCaption") %></LABEL></TD>
            <TD><%=mobjValues.dateControl("tcdexpirdat", CStr(mclsProduct.dexpirdat),  , GetLocalResourceObject("tcdexpirdatToolTip"),  ,  ,  ,  , mclsProduct.bexpirdat, 6)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbocurrrentCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbocurrrent", "table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mclsProduct.ncurrrent), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  , mclsProduct.nProdClas = 1,  , GetLocalResourceObject("cbocurrrentToolTip"),  , 14)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Duración de los pagos"><%= GetLocalResourceObject("AnchorDuración de los pagos2Caption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcncount_insuCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcncount_insu", 5, CStr(mclsProduct.ncount_insu),  , GetLocalResourceObject("tcncount_insuToolTip"),  ,  ,  ,  ,  ,  , mclsProduct.nProdClas = 1, 14)%></TD>
        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD COLSPAN="3"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=19338><%= GetLocalResourceObject("tctInsurTimeRoutineCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctRoutPay", 12, mclsProduct.sRoutPay,  , GetLocalResourceObject("tctRoutPayToolTip"),  ,  ,  ,  , True, 7)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnperc_capCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnperc_cap", 9, mobjValues.StringToType(CStr(mclsProduct.nperc_cap), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnperc_capToolTip"), True, 6,  ,  ,  ,  , mclsProduct.nProdClas = 1, 15)%>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypDurpayCaption") %></LABEL></TD>
			<%If mclsProduct_li.nTypdurpay = 7 Then%>
				<TD><%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", mclsProduct.nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nIdurafix", mclsProduct.nInsur_Time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeTypDurpay", "tabDurpay_Prod_mix", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nTypDurpay), True,  ,  ,  ,  , "insChangeValues(""Dur_paymix"", this.value, this)",  ,  , GetLocalResourceObject("cbeTypDurpayToolTip")))
	%>
				</TD>
			<%Else%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypDurpay", "Table5589", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct_li.nTypDurpay),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypDurpayToolTip")))%></TD>
			<%End If%>
			<TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPay_TimeCaption") %></LABEL></TD>
			<%If mclsProduct_li.sPdurvari = "1" Or mclsProduct_li.nTypDurpay = 4 Then%>
			    <TD><%=mobjValues.NumericControl("tcnPay_Time", 5, CStr(mclsProduct.nPay_Time),  , GetLocalResourceObject("tcnPay_TimeToolTip"),  ,  ,  ,  ,  ,  , mclsProduct.bPay_Time, 9)%></TD>
			<%Else%>
                <TD>
                <%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nIdurafix", mclsProduct.nInsur_Time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurins", mclsProduct.nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nTypdurpay", mclsProduct.nTypDurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("tcnPay_Time", "tabDurpay_prod", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nPay_Time), True,  ,  ,  ,  ,  , mclsProduct.bPay_Time, 5, GetLocalResourceObject("tcnPay_TimeToolTip"),  ,  , False))%>
				</TD>
			<%End If%>
            <TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdexpirdatCaption") %></LABEL></TD>
            <TD><%=mobjValues.dateControl("tcdDate_pay", CStr(mclsProduct.dDate_pay),  , GetLocalResourceObject("tcdDate_payToolTip"),  ,  ,  ,  , mclsProduct.bExpirdatpay, 10)%></TD>
            <TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
		</TR>
    </TABLE>
<%
Response.Write(mobjValues.HiddenControl("tctIduraind", nTypdurins))
Response.Write(mobjValues.HiddenControl("hddTypDurpay", CStr(mclsProduct_li.nTypDurpay)))
Response.Write(mobjValues.BeginPageButton)
If Not mobjValues.ActionQuery Then
	Response.Write("<SCRIPT>insSetParam('" & mobjValues.TypeToString(mclsProduct_li.nTypdurins, eFunctions.Values.eTypeData.etdDouble) & "','" & mobjValues.StringToType(CStr(mclsProduct.nInsur_Time), eFunctions.Values.eTypeData.etdDouble) & "','" & mobjValues.TypeToString(mclsProduct_li.nTypdurins, eFunctions.Values.eTypeData.etdDouble) & "')</SCRIPT>")
	If Not mblnGroups Then
		With Response
			.Write("<SCRIPT>")
			.Write("self.document.forms[0].cbovalGroup.disabled=true;")
			.Write("self.document.btncbovalGroup.disabled=true;")
			.Write("</SCRIPT>")
		End With
	End If
	If Not mblnSituation Then
		With Response
			.Write("<SCRIPT>")
			.Write("self.document.forms[0].cbovalSituation.disabled=true;")
			.Write("self.document.btncbovalSituation.disabled=true;")
			.Write("</SCRIPT>")
		End With
	End If
	'+ Si la duración del seguro es "Edad alcanzada"
	If nTypdurins = 1 Then
		Response.Write("<SCRIPT>DisabledFields(""sId2"")</SCRIPT>")
		'+ Si la duración del seguro es "Años"
	ElseIf nTypdurins = 2 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId3"")</SCRIPT>")
		'+ Si la duración del seguro es "Meses"
	ElseIf nTypdurins = 8 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId3"")</SCRIPT>")
		'+ Si la duración del seguro es "Dias"
	ElseIf nTypdurins = 9 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId3"")</SCRIPT>")
		'+ Si la duración del seguro es "Según póliza"
	ElseIf nTypdurins = 3 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId4"")</SCRIPT>")
		'+ Si la duración del seguro es "Según rutina"
	ElseIf nTypdurins = 4 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId5"")</SCRIPT>")
		'+ Si la duración del seguro es "Abierta"
	ElseIf nTypdurins = 5 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId6"")</SCRIPT>")
		'+ Si la duración del seguro es "Mixta"
	ElseIf nTypdurins = 7 Then 
		Response.Write("<SCRIPT>DisabledFields(""sId7"")</SCRIPT>")
	End If
End If
mobjValues = Nothing
mobjMenu = Nothing
mclsProduct = Nothing
mclsGroups = Nothing
mclsSituation = Nothing
mclsCertificat = Nothing
mclsProduct_li = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




