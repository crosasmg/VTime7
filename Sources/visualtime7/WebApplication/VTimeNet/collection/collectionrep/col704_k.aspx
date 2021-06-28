<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col704_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col704_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("COL704", "COL704_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction; var toggle= false </SCRIPT>")
End If
%>
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(action){
//--------------------------------------------------------------------------------------------
	switch(action){
		case 304: toggle = !toggle
 	}

	with (document.forms[0]){
		//cbeInsur_Area.disabled	= false;
		tcdPayDate.disabled		= false;
		btn_tcdPayDate.disabled	= false;
		cbeWayPay.disabled		= false;
		tctFileName.disabled	= false;
	}
	
	if (toggle == true){
	
		with (document.forms[0]){
			//cbeInsur_Area.value ='';
			cbeWayPay.value		='';
			valBank.value		='';
			tctFileName.value	='';
			valAgreement.value  ='';
		}	
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insChangeWayPay(value){
//--------------------------------------------------------------------------------------------

//+ Si es PAC se activa el banco.    

	with (document.forms[0]){
	
// Descuento por Planilla	
		if (value==1){
			
			valBank.disabled	= false;
			btnvalBank.disabled = false;
			valAgreement.disabled = true;
			btnvalAgreement.disabled = true;
            valAgreement.value = '';
            UpdateDiv('valAgreementDesc','','Normal')
		}
		else{
// PAC
            if (value==3){
				valBank.disabled	= true;
				valBank.value		= '';
				btnvalBank.disabled = true;
				UpdateDiv('valBankDesc','','Normal');
				btnvalAgreement.disabled = false;
				valAgreement.disabled = false;
				UpdateDiv('valAgreementDesc','','Normal');
			};	

		}
		
// Transbank		
		if (value==2){
			valBank.disabled	= true;
			valBank.value		= '';
			btnvalBank.disabled = true;
			UpdateDiv('valBankDesc','','Normal');

			valAgreement.value = '';
			UpdateDiv('valAgreementDesc','','Normal');
			btnvalAgreement.disabled = true;
			valAgreement.disabled = true;
    
		}
	}    
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<BR>

<FORM METHOD="POST" NAME="COL704" ACTION="ValCollectionRep.aspx?sMode=2">
    <%=mobjValues.ShowWindowsName("COL704", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_AreaCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeInsur_Area", "Table5001", eFunctions.Values.eValuesType.clngComboType, Session("nInsur_area"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInsur_AreaToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdPayDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdPayDate", CStr(Today),  , GetLocalResourceObject("tcdPayDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeWayPayCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 1
'mobjValues.List		= "1,2,3"
mobjValues.List = "3"
Response.Write(mobjValues.PossiblesValues("cbeWayPay", "Table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeWayPay(this.value)", True,  , GetLocalResourceObject("cbeWayPayToolTip")))%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valBankCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBank", "tabBank_Agree_Pac", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valBankToolTip"))%></TD> 
        </TR>
        <TR>
		   <TD><LABEL ID=12973><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD> 
           <TD><%=mobjValues.PossiblesValues("valAgreement", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAgreementToolTip"))%></TD> 
           <TD><%=mobjValues.HiddenControl("tcnsheet", "")%></TD>
           <TD><%=mobjValues.HiddenControl("tctFileName", "")%></TD>
        </TR>   
	</TABLE>

</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col704_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




