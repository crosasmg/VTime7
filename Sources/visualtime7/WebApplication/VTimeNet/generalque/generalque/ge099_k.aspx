<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsSche_Transac As eSecurity.Secur_sche

Dim mstrOption As Object


'% insPrepareQueryString: se controla el cambio de valor del campo "Consulta por" 
'--------------------------------------------------------------------------------------------
Private Function insPrepareQueryString() As String
	'--------------------------------------------------------------------------------------------
	Dim lstrStr As String
	Dim lstrOut As String
	Dim lintPos As Integer
	lstrStr = "GE099_K.ASPX?" & Request.Params.Get("Query_String")
	lintPos = InStr(1, lstrStr, "&sOption")
	If lintPos > 0 Then
		lstrOut = Mid(lstrStr, lintPos + 1)
		lstrStr = Mid(lstrStr, 1, lintPos - 1)
		lintPos = InStr(1, lstrOut, "&")
		If lintPos > 0 Then
			lstrOut = Mid(lstrOut, lintPos)
		Else
			lstrOut = vbNullString
		End If
	End If
	lstrOut = "&sOption="" + cbeTypeQuery.value" & lstrOut
	lstrStr = "self.location.href=""" & lstrStr & lstrOut
	insPrepareQueryString = lstrStr
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "GE099"

'+ Se realiza la validacion de operaciones permitidas al esquema del usuario
mclsSche_Transac = New eSecurity.Secur_sche%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/GeneralQue/GeneralQue/Scripts/GeneralQue.js"></SCRIPT>
<script LANGUAGE="JavaScript" SRC="../../Scripts/tmenu.js"></script>




<SCRIPT>
//% insFinish: se controla la acción Finalizar de la transacción
//-------------------------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------------------------
    return true
}
//%insStateZone. Esta funcion se encarga de habilitar los botones de la forma
//-------------------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------------------
    var lintIndex
    with (document.forms[0]){
        for (lintIndex=0;lintIndex<elements.length;lintIndex++){
            elements[lintIndex].disabled = false
            if (typeof(document.images["btn" + elements[lintIndex].name])!='undefined') document.images["btn" + elements[lintIndex].name].disabled = false
            if (typeof(document.images["btn_" + elements[lintIndex].name])!='undefined') document.images["btn_" + elements[lintIndex].name].disabled = false
        }
    }
}
//% insCancel: se controla la acción Cancelar de la transacción
//-------------------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------------------
    document.location.reload()
    return true
}
//-------------------------------------------------------------------------------------------------------------
function insAddValue(Value,Key,ParentFolder,Params,lstrImagesSrc){
//-------------------------------------------------------------------------------------------------------------
    var lobjBC003_K
    if (typeof(Key)=='undefined')Key=Value
    if (typeof(CurrentFolder)=='undefined') CurrentFolder = foldersTree
    if (typeof(lstrImagesSrc)=='undefined') lstrImagesSrc = ''
    lobjBC003_K = appendChild(CurrentFolder, folderNode(Value,lstrImagesSrc,lstrImagesSrc,0,Params,Key,ParentFolder))
    redrawTree()
}
//-------------------------------------------------------------------------------------------------------------
function initializeTree(lstrName,lstrImagesSrc,lstrParams,lstrKey){
//-------------------------------------------------------------------------------------------------------------
    generateTree(lstrName,lstrImagesSrc,lstrParams,lstrKey)
    redrawTree()
}
// ClearDescCompany: Limpia el DIV del control.
//-------------------------------------------------------------------------------------------
function ClearDescCompany(){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].cbeCompany.value==""){
		UpdateDiv ("tctCompanyName","");
	}
}
// ClearDescPolicy: Limpia el DIV del control.
//-------------------------------------------------------------------------------------------
function ClearDescPolicy(){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].cbePolicy.value==""){
		UpdateDiv ("tctPolicyName","");
	}
}
//-------------------------------------------------------------------------------------------------------------
function generateTree(lstrName,lstrImagesSrc,lstrParams,lstrKey){
//-------------------------------------------------------------------------------------------------------------
    foldersTree = folderNode(lstrName,lstrImagesSrc,lstrImagesSrc,1,lstrParams,lstrKey,0)
}
//-------------------------------------------------------------------------------------------------------------
function insShowPolicy(sValue){
//-------------------------------------------------------------------------------------------------------------
var lstrCertype    
    with (self.document.forms[0]){
		switch(cbeTypeQuery.value){
			case "1":
			case "3": //Poliza/Certificado
				lstrCertype = "2";
				break;
			case "5": //Solicitud
				lstrCertype = "1";
				break;
			case "11": // Cotización
				lstrCertype = "3"
				break;
    
		}
		
		insDefValues("Policy", "sCertype=" + lstrCertype + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertif.value);
    }
}
//% ShowPolicies: Muestra pólizas de un asegurado
//-----------------------------------------------------------------------------------------------------------------
function ShowPolicies(sField) {
//-----------------------------------------------------------------------------------------------------------------
	var lstrCertype    

    with (self.document.forms[0]){
		switch(cbeTypeQuery.value){
			case "1":
			case "3": //Poliza/Certificado
				lstrCertype = "2";
				break;
			case "5": //Solicitud
				lstrCertype = "1";
				break;
			case "11": // Cotización
				lstrCertype = "3"
				break;
    
		}
			if (tctRegister.value != '')
				ShowPopUp('/VTimeNet/Common/PoldataSI001.aspx?' + lstrCertype + "&sregist=" + tctRegister.value +
			    													"&sdigit=" + tctDigit.value +
					     											"&dEffecdate=" + tcdDate.value +
						    										"&sCodispl=SI001", 'PolicyData', 800, 450, "yes", "no", 100, 50)


	}
}
</SCRIPT>
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("GE099", "GE099_K.aspx", 1, ""))
mobjMenu = Nothing%>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmGE099" ACTION="valGeneralQue.aspx?Time=1">
<TABLE WIDTH="100%" BORDER="0">
  <TR>
    <TD WIDTH="25%"><LABEL ID=11246><%= GetLocalResourceObject("cbeTypeQueryCaption") %></LABEL></TD>
<%If Request.QueryString.Item("sOption") = vbNullString Then
	mstrOption = "4" ' Cliente
Else
	mstrOption = Request.QueryString.Item("sOption")
End If
%>
    <TD WIDTH="30%">
    <%
Response.Write(mobjValues.ComboControl("cbeTypeQuery", mclsSche_Transac.Sche_Transac(Session("sSche_code"), Request.QueryString.Item("sCodispl")), mstrOption + 0,  ,  ,  , insPrepareQueryString, True))
'Response.Write mobjvalues.PossiblesValues("cbeTypeQuery","Table418",1 , mstrOption + 0 ,,,,,,insPrepareQueryString,true)
%>
    </TD>
    <TD WIDTH="1%">&nbsp;</TD>
    <%Select Case mstrOption
	Case "4" '"Client"%> 
			<TD WIDTH="10%"><LABEL ID=11243><%= GetLocalResourceObject("valClienameCaption") %></LABEL></TD>
			<TD><%=mobjValues.ClientControl("valCliename", Session("sClient"), True, "",  , True)%></TD>
		<%	Case "1", "3", "11", "5" '"Policy"%>
			<TD><LABEL ID=11244><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Session("nBranch"), "valProduct",  ,  ,  ,  , True)%></TD>
		<%	Case "8" '"Cheque"%>
			<TD><LABEL ID=11250><%= GetLocalResourceObject("tctChequeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctCheque", 30, vbNullString,  , "",  ,  ,  ,  , True)%></TD>
		<%	Case "2" '"PolicyO"%>    
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyOCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnPolicyO", 30, vbNullString,  , "",  ,  ,  ,  , True)%></TD>
		<%	Case "10" '"ReceiptO"%>    
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnReceiptOCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tcnReceiptO", 30, vbNullString,  , "",  ,  ,  ,  , True)%></TD>
		<%	Case "40" '"Provider"%>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProviderCaption") %></LABEL></TD>
			<%		mobjValues.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjValues.Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
			<TD><%=mobjValues.PossiblesValues("valProvider", "tabTab_provider", 2,  , True,  ,  ,  ,  ,  , True)%></TD>        
		<%	Case "6" '"Claim"%>
			<TD><LABEL ID=11251><%= GetLocalResourceObject("tcnClaimCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnClaim", 9, vbNullString,  , "",  , 0,  ,  ,  ,  , True)%></TD>    
		<%	Case "7" '"Receipt"%>
			<TD><LABEL ID=40412><%= GetLocalResourceObject("tcnClaimCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnClaim", 9, vbNullString,  , "",  , 0,  ,  ,  ,  , True)%></TD>    
		<%	Case "9" '"Contr"%>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnContrCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnContr", 9, vbNullString,  , "",  , 0,  ,  ,  ,  , True)%></TD>    
		<%	Case "60" '"Contr"%>
			<TD><LABEL ID=100760><%= GetLocalResourceObject("tctChequeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctCheque", 20, vbNullString,  , "",  ,  ,  ,  , True)%></TD>    
	    <%	Case "77" '"Intermediario"%>
	    	<TD><LABEL ID=100760><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
	    	<TD><%=mobjValues.PossiblesValues("valIntermed", "tabintermedia", 2,  , True,  ,  ,  ,  ,  , True, 10)%></TD>        
		<%	Case "13" '"Reaseguro- Compañia de Reaseguro"%>	    	
	    	<TD><LABEL ID=100760><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
            <TD><%=mobjValues.CompanyControl("cbeCompany", "",  , GetLocalResourceObject("cbeCompanyToolTip"), "ClearDescCompany();", True, "tctCompanyName", False,  ,  ,  , 8)%></TD>
		<%	Case "80" '"Reaseguro- Prima Cedida"%>	    	
	    	<TD><LABEL ID=100760><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , True)%></TD>    			
		<%	Case "81" '"Reaseguro- Siniestro Cedido"%>	    	
	    	<TD><LABEL ID=100760><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , True)%></TD>    			
		<%	Case "82" '"Reaseguro- Distribucion del Capital"%>	    	
	    	<TD><LABEL ID=100760><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , "",  , 0,  ,  ,  ,  , True)%></TD>    			
		</TR>   
		<%	Case Else%>
		<TD>&nbsp;</TD>
		<TD>&nbsp;</TD>
    <%End Select%>
    <TD WIDTH="1%">&nbsp;</TD>
    <TD>&nbsp;</TD>
    <TD>&nbsp;</TD>    
  </TR>
    <TD WIDTH="15%"><LABEL ID=11247><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
<TD WIDTH="30%"><% %>
<%=mobjValues.DateControl("tcdDate", CStr(Today),  , "",  ,  ,  ,  , True)%></TD>
    <TD WIDTH="1%">&nbsp;</TD>
    <%Select Case mstrOption
	Case "1", "3", "11", "5" '"Policy"%>
			<TD><LABEL ID=11244><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Session("nBranch"), eFunctions.Values.eValuesType.clngWindowType, True, Session("nProduct"))%></TD>			
		<%	Case Else%>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>        
	<%End Select%>
    <TD WIDTH="1%">&nbsp;</TD>
    <TD>&nbsp;</TD>
    <TD>&nbsp;</TD>
  </TR>
  <TR>
    <%Select Case mstrOption
	Case "1", "3", "11", "5" '"Policy"%>
    <TD WIDTH="10%">&nbsp;</TD>
    <TD WIDTH="30%">&nbsp;</TD>
    <TD WIDTH="1%">&nbsp;</TD>
    <%		If mstrOption = "11" Then%>
		<TD WIDTH="10%"><LABEL ID=11247><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>	
	<%		ElseIf mstrOption = "5" Then %>
		<TD WIDTH="10%"><LABEL ID=11247><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
	<%		Else%>
		<TD WIDTH="10%"><LABEL ID=11247><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
    <%		End If%>
    <TD COLSPAN=3><%=mobjValues.NumericControl("tcnPolicy", 10, Session("nPolicy"),  , "",  ,  ,  ,  ,  , "insShowPolicy(this.value)", 0)%><LABEL ID=40587> <%= GetLocalResourceObject("Anchor4Caption") %> <<%= GetLocalResourceObject("Anchor4Caption") %>LABEL> <%=mobjValues.NumericControl("tcnCertif", 7, vbNullString,  , "",  , 0)%></TD>
<%		
	Case Else
		Response.Write("<TD>&nbsp;</TD><TD>&nbsp;</TD><TD WIDTH=""1%"">&nbsp;</TD><TD>&nbsp;</TD>")
End Select
%>    
  </TR>
  <TR>
    <%Select Case mstrOption
	Case "1", "3", "11", "5" '"Policy"%>
    <TD WIDTH="10%">&nbsp;</TD>
    <TD WIDTH="30%">&nbsp;</TD>
    <TD WIDTH="1%">&nbsp;</TD>
		<TD WIDTH="10%"><LABEL ID=LABEL1><%= GetLocalResourceObject("tctRegisterCaption")%></LABEL>&nbsp;<%=mobjValues.AnimatedButtonControl("btnAutoRegist", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("tctRegisterToolTip"), , "ShowPolicies(""regist"")", False)%></TD>	
    <TD COLSPAN=3><%=mobjValues.TextControl("tctRegister", 10, "", , GetLocalResourceObject("tctRegisterToolTip")) & "-" & mobjValues.TextControl("tctDigit", 1, "", , GetLocalResourceObject("btnAutoRegistTooltip"), , , , , True)%></TD>
<%		
	Case Else
		Response.Write("<TD>&nbsp;</TD><TD>&nbsp;</TD><TD WIDTH=""1%"">&nbsp;</TD><TD>&nbsp;</TD>")
End Select
%>    
  </TR>

<%
If Request.QueryString.Item("sOption") <> vbNullString Then
        Response.Write("<SCRIPT>insStateZone()</SCRIPT>")
    Else
        Response.Write("<Script>if(insDisabledButton(document.A401,0)){ ClientRequest(401,1);}; </Script>")
    End If
'Set mclsSche_Transac = Nothing
%>    
  </TR>
</TABLE>
</FORM>
</BODY>
</HTML>





