<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'Dim LoadHeader() As Object
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Valores parametros
Dim mstrDocType As Object
Dim mstrOperat As Object
Dim mstrAction As Object
Dim mstrOrigin As Object
Dim mstrBranch As Object
Dim mstrProduct As Object
Dim mstrEffecdate As Object
Dim mstrBrancht As Object
Dim mstrdateCont As Object
Dim mstrQs As Object


'% LoadFolder: Carga datos del detalle
'-----------------------------------------------------------------------------------
Private Sub LoadFolder()
	'-----------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0> " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=13871>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"">")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=13872>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"">")

	
	'+ Se crea parametro de salida para retornar el ramo tecnico (sBrancht)
	With mobjValues.Parameters
		.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.ReturnValue("sBrancht", False, "Ramo técnico", True)
	End With
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valProductToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 9, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "ShowChangeValues(""Policy"")", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("        </TR>                    " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"">")


Response.Write(mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""50%"">")


Response.Write(mobjValues.DIVControl("lblCliename", False, ""))


Response.Write("&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valIntermedCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valIntermed", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")

	
	mobjValues.Parameters.Add("nOfficeagen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valAgency", "TabAgencies_T5555", 2, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAgencyToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    ")

	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA789_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA789_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 21/09/04 13:14 $|$$Author: Nvaplat15 $"

//% insCancel: realiza el manejo en caso que el usuario cancele la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insCancel: realiza el manejo en caso que el usuario cancele la transacción
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
//% ShowChangeValues: realiza la busqueda de la información de la propuesta
//------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (sField = 'Policy') 
            insDefValues("Policy_CA789", "sCertype=" + "1" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + "0" ,'/VTimeNet/Policy/PolicyTra')
     }  
}  
//% insStateZone: controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(nAction){
//------------------------------------------------------------------------------------------      
//- Tipo de acciones del menu
	var eTypeActions = new TypeActions()	
    
	with(self.document.forms[0]){
//+ Para accion consulta solo se habilita la operacion consultar		
		if (top.frames["fraSequence"].plngMainAction==eTypeActions.clngActionQuery)
		{
//+ Se habilitan campos para consulta general y se deshabilitan para actualización 
			dtcClient.disabled      = 
			btndtcClient.disabled   = 
			valIntermed.disabled    = 
			btnvalIntermed.disabled = 
			valAgency.disabled      = 
			btnvalAgency.disabled   = false;
        }		    
		else
		{
			dtcClient.disabled      = 
			btndtcClient.disabled   = 
			valIntermed.disabled    = 
			btnvalIntermed.disabled = 
			valAgency.disabled      = 
			btnvalAgency.disabled   = true;
		}
//+ Se habilitan controles 
		tcnPolicy.disabled          = 
		cbeBranch.disabled          = false;
            
		tcnPolicy.value             = "";
		tcdEffecdate.value          = "";
	}
}

</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA789", "CA789_k.aspx", 1, ""))
	.Write("<BR><BR>")
End With
mobjMenu = Nothing
%>
</SCRIPT>      
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPropoOperat" ACTION="ValPolicyTra.aspx?sTime=1<%=mstrQs%>">
<%If Request.QueryString.Item("sConfig") = "InSequence" Then
	'Call LoadHeader()
Else
	Call LoadFolder()
End If

mobjValues = Nothing

%>	
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("CA789_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





