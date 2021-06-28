<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values

Dim mobjCertificat As ePolicy.Certificat

'- String que envia a control de cliente llave de busca de la poliza 
'*** borrrar despues
Dim lstrQueryString As Object
Dim mstrClient As Object
Dim bPolicy As Boolean
Dim clsPolicy As ePolicy.Certificat


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjCertificat = New ePolicy.Certificat

mobjValues.ActionQuery = Session("bQuery")

clsPolicy = New ePolicy.Certificat

Call mobjCertificat.insPreVI7010(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"), VbNullString)

bPolicy = clsPolicy.FindPolicyVI7010(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjCertificat.sClient)


%>  



	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT LANGUAGE=javascript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 8 $|$$Date: 25/08/06 15:48 $|$$Author: Clobos $"


// ChangeSubmit: Cambia la accion de la forma
//-------------------------------------------------------------------------------------------
function ChangeSubmit(Option, Holder) {
//-------------------------------------------------------------------------------------------	
	switch (Option) {
		case "Add":
			document.forms[0].action = "valPolicySeq.aspx?&nMainAction=301&nHolder=" + Holder
	}
}
function Shownames(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    with(self.document.forms[0]){
        if(tctClient.value!=""){
            insDefValues('Client','sClient=' + tctClient.value,'/VTimeNet/Policy/PolicySeq')}
        else{
            tctLastName.value="";
            tctLastName2.value="";
            tctFirstName.value="";
        }
    }              
}
function InschangeValue(Field){
	Field.value = Field.value.toUpperCase();
}
</SCRIPT>
</SCRIPT>
<HTML>
<HEAD>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY>
<FORM METHOD="post" ID="FORM" NAME="frmVI7010" ACTION = "valPolicySeq.aspx?nMainAction=301&amp;nHolder=1">
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
    <%Response.Write(mobjValues.HiddenControl("tctCliename", ""))%>
    <TABLE WIDTH="100%">
        <TR>
	        <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD> 
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="Horline"></TD>
		</TR>        	
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
			<TD COLSPAN="4">
	        <%mobjValues.TypeList = CShort("2")
mobjValues.List = "1,11"
Response.Write(mobjValues.ClientControl("tctClient", mobjCertificat.sClient, True, GetLocalResourceObject("tctClientToolTip"), "Shownames();", False,  ,  ,  ,  ,  ,  ,  , False,  ,  , True))%></TD>
		</TR>
		<TR>
	        <TD><LABEL><%= GetLocalResourceObject("tctFirstNameCaption") %></LABEL></TD>
	        <TD COLSPAN = 3> <%=mobjValues.TextControl("tctFirstName", 19, mobjCertificat.sFirstName, True, GetLocalResourceObject("tctFirstNameToolTip"),  ,  ,  , "InschangeValue(this)", bPolicy)%> </TD>
		</TR>
		<TR>
        	<TD><LABEL ID=12971><%= GetLocalResourceObject("tctLastNameCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctLastName", 19, mobjCertificat.sLastname, True, GetLocalResourceObject("tctLastNameToolTip"),  ,  ,  , "InschangeValue(this)", bPolicy)%> </TD>
	        
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctLastName2Caption") %></LABEL></TD>
			<TD WIDTH="25%"> <%=mobjValues.TextControl("tctLastName2", 19, mobjCertificat.sLastname2, True, GetLocalResourceObject("tctLastName2ToolTip"),  ,  ,  , "InschangeValue(this)", bPolicy)%> </TD>
			
		</TR>
		<TR>
        	<TD><LABEL ID=12972><%= GetLocalResourceObject("tcdBirthDateCaption") %></LABEL></TD>
			<TD> <%=mobjValues.DateControl("tcdBirthDate", CStr(mobjCertificat.dBirthDat),  , GetLocalResourceObject("tcdBirthDateToolTip"),  ,  ,  ,  , bPolicy)%> </TD>
			
        	<TD><LABEL ID=12973><%= GetLocalResourceObject("tctAgeCaption") %></LABEL></TD>
			<TD WIDTH="25%"> <%=mobjValues.TextControl("tctAge", 3, CStr(mobjCertificat.nAge), False, GetLocalResourceObject("tctAgeToolTip"),  ,  ,  ,  , True)%> </TD>
		</TR>
        <TR>
        	<TD><LABEL ID=12974><%= GetLocalResourceObject("cbeSexCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeSex", "Table18", 1, mobjCertificat.sSexclie,  ,  ,  ,  ,  ,  , bPolicy,  , GetLocalResourceObject("cbeSexToolTip"))%></TD>
			  
			<TD><%=mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), mobjCertificat.sSmoking, CStr(1),  , bPolicy)%>  </TD>
        </TR>
		<TR>
        	<TD><LABEL ID=12975><%= GetLocalResourceObject("cbeOccupatCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOccupat", "Table16", 1, CStr(mobjCertificat.nSpeciality),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOccupatToolTip"))%></TD>
            
        	<TD><LABEL ID=12976><%= GetLocalResourceObject("cbeCivilstaCaption") %></LABEL></TD>
			  <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeCivilsta", "Table14", 1, CStr(mobjCertificat.nCivilsta),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCivilstaToolTip"))%></TD>
		</TR>
        <TR>
     	    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><A NAME="Varios"><%= GetLocalResourceObject("AnchorVariosCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="5" CLASS="Horline"></TD>
		</TR>		
		<TR>
        	<TD><LABEL ID=12977><%= GetLocalResourceObject("valOptionCaption") %></LABEL></TD>
			<TD><%With mobjValues.Parameters
	mobjValues.BlankPosition = False
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valOption", "TAB_OPTION", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nOption), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valOptionToolTip")))%></TD>
        	<TD><LABEL ID=12978><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCapital", 18, CStr(mobjCertificat.nCapital),  , GetLocalResourceObject("tcnCapitalToolTip"), True, 2)%></TD></TD>
       </TR>			
        <TR><TD><LABEL ID=12978><%= GetLocalResourceObject("cbenCurrencyCaption") %></LABEL></TD>
            <TD><%With mobjValues.Parameters
	mobjValues.BlankPosition = False
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbenCurrency", "TABCUR_ALLOW_GEN", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nCurrency), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenCurrencyToolTip")))%></TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeTyperiskCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTyperisk", "Table5639", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nTyperisk),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTyperiskToolTip"))%></TD>
		</TR>
    </TABLE> 
   
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
mobjCertificat = Nothing

Response.Write("<SCRIPT>Shownames(); </SCRIPT>")
'^End Footer Block VisualTimer

%>




