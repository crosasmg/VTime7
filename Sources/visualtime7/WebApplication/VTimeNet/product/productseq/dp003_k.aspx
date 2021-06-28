<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las funciones del menú
Dim mobjMenu As eFunctions.Menues


'% LoadHeader: se cargan los datos del encabezado
'--------------------------------------------------------------------------------------------
Private Sub LoadHeader()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% ChangeValues: se controla el cambio de valor de los controles" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function ChangeValues(Option, Field){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch(Option){" & vbCrLf)
Response.Write("		case ""Branch"":" & vbCrLf)
Response.Write("//+ Se pasan los parámetros al campo ""Producto""" & vbCrLf)
Response.Write("			with(self.document.forms[0]){" & vbCrLf)
Response.Write("				valProduct.disabled=false;" & vbCrLf)
Response.Write("				btnvalProduct.disabled=false;" & vbCrLf)
Response.Write("				valProduct.value="""";" & vbCrLf)
Response.Write("				UpdateDiv(""valProductDesc"", """")" & vbCrLf)
Response.Write("				valProduct.Parameters.Param1.sValue=Field.value;" & vbCrLf)
Response.Write("				valProduct.Parameters.Param2.sValue=0;" & vbCrLf)
Response.Write("			}" & vbCrLf)
Response.Write("			break;" & vbCrLf)
Response.Write("		case ""Product"":" & vbCrLf)
Response.Write("//+ Se asigna valor por defecto al campo ""Tipo de producto""" & vbCrLf)
Response.Write("			with(self.document.forms[0]){" & vbCrLf)
Response.Write("				cbeProdType.value=valProduct_sBrancht.value;" & vbCrLf)
Response.Write("			}" & vbCrLf)
Response.Write("			break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14941>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14940>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "table10", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "ChangeValues(""Branch"", this)", True,  , GetLocalResourceObject("cbeBranchToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14943>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.ReturnValue("sBrancht", False, vbNullString, True)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "ChangeValues(""Product"", this)", True, 5, GetLocalResourceObject("valProductToolTip"),  ,  ,  , True))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14942>" & GetLocalResourceObject("cbeProdTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeProdType", "table37", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeProdTypeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	If CStr(Session("DP003_sLinkSpecial")) = "1" Then
		Response.Write("<SCRIPT>ClientRequest('301')</" & "Script>")
	End If
End Sub

</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP003_K"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP003_K", "DP003_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>

<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:01 $"
//% insStateZone: se controla el estado de los controles
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	var eTypeActions = new TypeActions()
	var lstrLinkSpecial='<%=Session("DP003_sLinkSpecial")%>'

	with(self.document.forms[0]){
		if (lstrLinkSpecial == '1'){
			tcdEffecdate.value='<%=Session("DP003_dEffecdate")%>';
			cbeProdType.value='<%=Session("DP003_nBrancht")%>';
			cbeBranch.value='<%=Session("DP003_nBranch")%>';
			valProduct.value='<%=Session("DP003_nProduct")%>';
		}
		else
		{
			tcdEffecdate.disabled=false;
			btn_tcdEffecdate.disabled=false;
			cbeBranch.disabled=false;
		
			if (cbeBranch.value != '' && cbeBranch.value != '0')
				valProduct.disabled = false;
			cbeProdType.disabled=(top.fraSequence.plngMainAction==eTypeActions.clngActionadd?false:true);
			tcdEffecdate.value="";
			cbeProdType.value="0";
			cbeBranch.value="0";
			valProduct.value="";
			valProduct.disabled = true;
			UpdateDiv("valProductDesc", "");
			btnvalProduct.disabled = valProduct.disabled;
		}
	}
}
//% insCancel: se controla la acción Cancelar de la ventana
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	var lstrLinkSpecial='<%=Session("DP003_sLinkSpecial")%>'

	if(top.frames['fraSequence'].pintZone==1)
		if (lstrLinkSpecial=='1')
			top.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=DP002'
			
		else
			return true;
	else
		ShowPopUp("/VTimeNet/Product/ProductSeq/ShowDefValues.aspx?Field=Cancel", "ShowDefValuesCancel", 1, 1,"no","no",2000,2000);
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	if(top.frames["fraSequence"].pblnQuery==true)
		return true;
	else
		ShowPopUp("/VTimeNet/Product/ProductSeq/DP999.aspx?sCodispl=DP999&nAction=392","EndProcess",400,130)
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProductProcess" ACTION="valProductSeq.aspx?sMode=1">
	<P>&nbsp;</P>
<%
Call LoadHeader()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





