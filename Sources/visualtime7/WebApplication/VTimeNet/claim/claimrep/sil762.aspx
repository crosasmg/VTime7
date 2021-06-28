<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de los casos.
Dim mlngClaim As Double 

'- Objeto para el manejo del siniestro 
Dim mlngBranch As Byte
Dim mlngProduct As Byte
Dim mlngPolicy As Object
Dim mstrClaim As String


Dim mblnDisabled As Boolean


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil762")

mlngClaim = eRemoteDB.dblNull 
mstrClaim = ""
mblnDisabled = True

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sil762"

Response.Write("<SCRIPT>var mlngClaim</SCRIPT>")
If Request.QueryString("nClaim") <> vbNullString Then
	mlngBranch = Request.QueryString("nBranch")
	mlngProduct = Request.QueryString("nProduct")
	mlngPolicy = Request.QueryString("nPolicy")
	mlngClaim = Request.QueryString("nClaim")
	mstrClaim = Request.QueryString("nClaim")
	Response.Write("<SCRIPT>mlngClaim=" & Request.QueryString("nClaim") & "</SCRIPT>")
	mblnDisabled = False
Else
	mlngClaim = 0
	mstrClaim = vbNullString
End If

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->


<HTML>
<HEAD>

<SCRIPT> 
//% insStateZone: se manejan los campos de la página
//-----------------------------------------------------------------------------
function insStateZone()
//-----------------------------------------------------------------------------
{
}
//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//-----------------------------------------------------------------------------
function insPreZone(llngAction)
//-----------------------------------------------------------------------------
{
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel()
//-----------------------------------------------------------------------------
{
   return true
}
//%ShowChangeValues: Evento OnChange de CbeBranch
//-----------------------------------------------------------------------------
function ShowChangeValues(Control)
//-----------------------------------------------------------------------------
{
	if(Control=="40"){
	    self.document.forms[0].chkClaim.disabled=false
	}
	else{
	    self.document.forms[0].cbeBranch.disabled=false
	    self.document.forms[0].valProduct.disabled=false
	    self.document.forms[0].tcnPolicy.disabled=false
	    self.document.forms[0].tcnClaim.disabled=false
	    self.document.forms[0].chkClaim.disabled=true
   }     
}

//% ReloadPage: se recarga la página para asignar valor al combo de Casos
//-------------------------------------------------------------------------------------------
function ReloadPage(Claim){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(Claim.value==0)
			cbeCase.value=0
		else
			if(mlngClaim!=Claim.value)
			    self.document.location.href = "SIL762_K.aspx?sCodispl=SIL762_K" +
											             "&nBranch=" + cbeBranch.value +
														 "&nProduct=" + valProduct.value +
														 "&nPolicy=" + tcnPolicy.value +
														 "&nClaim=" + Claim.value
    }	
}
</SCRIPT>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SIL762", "SIL762_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>    
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $" 
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL762" ACTION="valClaimRep.aspx?sMode=1">
<BR><BR><BR>
	<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="25%">&nbsp</TD>
            <TD><LABEL ID=0>Ramo</LABEL></TD>
            <TD><%If mlngBranch = 0 Then
	Response.Write(mobjValues.BranchControl("cbeBranch", "Código del Ramo.", "Table10",  ,  ,  ,  , "ShowChangeValues(this.value);",  , 1))
Else
	Response.Write(mobjValues.BranchControl("cbeBranch", "Código del Ramo.", CStr(mlngBranch),  ,  ,  ,  , "ShowChangeValues(this.value);",  , 1))
End If%>
            </TD>
            <TD WIDTH="25%">&nbsp</TD>
        </TR>
        <TR>
            <TD WIDTH="25%">&nbsp</TD>
            <TD><LABEL ID=0>Producto</LABEL></TD>
            <TD><%If mlngProduct = 0 Then
	Response.Write(mobjValues.ProductControl("valProduct", "Código del Producto a procesar.",  , eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , 2))
Else
	Response.Write(mobjValues.ProductControl("valProduct", "Código del Producto a procesar.", CStr(mlngBranch), eFunctions.Values.eValuesType.clngWindowType,  , CStr(mlngProduct),  ,  ,  ,  , 2))
End If%>
		    </TD>
            <TD WIDTH="25%">&nbsp</TD>
        </TR>
        <TR>
            <TD WIDTH="25%">&nbsp</TD> 
            <TD><LABEL ID=0>Póliza</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, mlngPolicy,  , "Póliza que declara el siniestro",  , 0,  ,  ,  ,  ,  , 3)%></TD>
            <TD WIDTH="25%">&nbsp</TD>
        </TR>
        <TR>
            <TD WIDTH="25%">&nbsp</TD>
            <TD><LABEL ID=0>Siniestro</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnClaim", 10, mstrClaim,  , "Número del Siniestro",  , 0,  ,  ,  , "ReloadPage(this)",  , 4)%></TD>              
            <TD WIDTH="25%">&nbsp</TD>
        </TR>
        <TR>
            <TD WIDTH="25%">&nbsp</TD>
            <TD><LABEL ID=0>Caso</LABEL></TD>
            <TD><%
With mobjValues
	.BlankPosition = False
	.Parameters.Add("nClaim", mlngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, CStr(1), True,  ,  ,  ,  ,  , CBool(mblnDisabled),  ,  , eFunctions.Values.eTypeCode.eNumeric, 5))
End With
%>
			</TD>
            <TD WIDTH="25%">&nbsp</TD>
        </TR>
        <TR>
            <TD WIDTH="25%">&nbsp</TD>
            <TD><LABEL ID=0>Siniestro Oncologico</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkClaim", "Siniestro Oncologico",  ,  ,  , True, 6)%></TD>        
            <TD WIDTH="25%">&nbsp</TD>
        </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sil762")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




