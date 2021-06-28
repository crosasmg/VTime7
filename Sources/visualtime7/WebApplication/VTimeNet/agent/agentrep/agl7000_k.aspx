<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGen As eGeneral.Ctrol_date
Dim mstrMonth As Short
Dim mstrYear As Short
Dim mblnFind As Boolean


'%insPreAGL7000: Se cargan los controles de la ventana.
'----------------------------------------------------------------------------
Private Sub insPreAGL7000()
	'----------------------------------------------------------------------------
	
	'+[APV2] 1014_BB. Calculo de comisiones de APV
	Dim lblnFind As Boolean
	lblnFind = mobjGen.Find(70)
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))


Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<!--[APV2] 1014_BB. Calculo de comisiones de APV!-->" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD></TD>        " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>             		   " & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "insChargeProduct(this)",  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>             " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>             		    " & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""60%"" COLSPAN=""2""> ")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "GetDate_value();", 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>                   " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeMonthCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

	If lblnFind Then
Response.Write("" & vbCrLf)
Response.Write("                  <TD>")


Response.Write(mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, CStr(Month(mobjGen.dEffecdate)),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")

	Else
Response.Write("" & vbCrLf)
Response.Write("                  <TD>")


Response.Write(mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, CStr(Month(mobjGen.dEffecdate)),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")

	End If
Response.Write("" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>          " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>   		       " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnYearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

	If lblnFind Then
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnYear", 4, CStr(Year(mobjGen.dEffecdate)), True, GetLocalResourceObject("tcnYearToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")

	Else
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnYear", 4, "", True, GetLocalResourceObject("tcnYearToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")

	End If
Response.Write("" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl7000_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache) 

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "agl7000_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjGen = New eGeneral.Ctrol_date
	mblnFind = mobjGen.Find(70)
	If mblnFind Then
		mstrMonth = Month(mobjGen.dEffecdate)
		mstrYear = Year(mobjGen.dEffecdate)
	End If
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 18/10/04 8:51 $"

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
}

//%insCancel: Acciones a efectuar al cancelar la transacción.
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return true;
}

//%insFinish: Acciones a efectuar al finalizar la transacción.
//------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------
	return true;
}

//% insChargeProduct: Se cargan los parámetros del campo producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	var lstrMonth = '<%=mstrMonth%>'
	var lstrYear  = '<%=mstrYear%>'
						    						
	if (lobject.value!=0) {
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct.value="";
			UpdateDiv("valProductDesc", "");						
		}
    }    
        
	with(self.document.forms[0]){
        if (lobject.name=="cbeBranch") {
           if (typeof(cbeBranch)=='undefined' || cbeBranch.value=='0') {        
            cbeMonth.value=lstrMonth;
            tcnYear.value=lstrYear;
           } 
        }    
    }        
}

//% ChangeProduct: se maneja el cambio de valor del Producto 
//-------------------------------------------------------------------------------------------
 function ChangeProduct(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (cbeBranch.value != ''){
			if (cbeBranch.value != "0"){ 
  			    tcnPolicy.value = '';
  			    tcnCertif.value = '';			
				tcnPolicy.disabled = false;				
			}
		}
	}
}

//% GetDate_value: Obtiene la última fecha válida y le suma 1 día
//------------------------------------------------------------------------------------------
function GetDate_value(){
//------------------------------------------------------------------------------------------        
    insDefValues("Date_value_Product", "nBranch=" + self.document.forms[0].elements['cbeBranch'].value +
	                            "&nProduct=" + self.document.forms[0].elements['valProduct'].value, '/VTimeNet/agent/agentrep');	
}

</SCRIPT>	
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL7000_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	
	'+ Se agrega zona para dejar des-habilitado el botón aceptar
	
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With

mobjMenu = Nothing
%>
</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmRIntermAccount" ACTION="ValAgentRep.aspx?Mode=1">
<%
Call insPreAGL7000()
mobjValues = Nothing
mobjGen = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl7000_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




