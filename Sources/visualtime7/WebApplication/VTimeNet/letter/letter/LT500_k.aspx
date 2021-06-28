<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: Object for the handling of the general functions of load of values
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**-Objetive: Object for the handling of the generics routines
'-Objetivo: Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'**%Objetive: The controls of the page are loaded
'%Objetivo: Se cargan los controles de la página
'----------------------------------------------------------------------------------------------------------------------
Private Sub insPreLT500_K()
        '----------------------------------------------------------------------------------------------------------------------
	
        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"" border=0>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("          <TD><LABEL ID=17389>Forma de envío</LABEL></TD>" & vbCrLf)
        Response.Write("          <TD>")

        Response.Write(mobjValues.PossiblesValues("cbeShipmentType", "Table5020", eFunctions.Values.eValuesType.clngWindowType, "", , , , , , , False, ,"Forma de envío de los documentos."))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("   		  <TD></TD>" & vbCrLf)
        Response.Write("		  <TD><LABEL ID=17390>Sucursal</LABEL></TD>" & vbCrLf)
        Response.Write("		  <TD>")

        Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "Table9", eFunctions.Values.eValuesType.clngComboType, , , , , , , "setCAP(1,this)", False, ,"Sucursal a la que debe estar asociada la información que esta pendiente por imprimir."))

        Response.Write(" </TD>" & vbCrLf)
        Response.Write("		  <TD></TD>" & vbCrLf)
        Response.Write("		  <TD><LABEL ID=17391>Oficina</LABEL></TD>" & vbCrLf)
        Response.Write("		  <TD>")
		
        With mobjValues
            .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True, , , , , "setCAP(2,this)", False, ,"Oficina a la que pertenece el intermediario en tratamiento"))
        End With
	
        Response.Write("" & vbCrLf)
        Response.Write("		  </TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=17392>Intermediario</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        With mobjValues
            .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("valIntermedia", "TABINTERAGENCIES", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , False, ,vbNullString))
        End With
        
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=17393>Código del cliente</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.ClientControl("tctClient", "", ,"Código del cliente", , False, "tctCliename"))

        Response.Write("</TD>		" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=17394>Tipo de documento</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.PossiblesValues("cbeTypeDocument", "Table5026", eFunctions.Values.eValuesType.clngComboType, CStr(0), , , , , , , False, ,"Tipo de documento que desea imprimir.", eFunctions.Values.eTypeCode.eString))
        
        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=17395>Tipo de registro</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""6"">&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=2 CLASS=""HORLINE""></TD>		" & vbCrLf)
        Response.Write("			<TD COLSPAN=6></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD> ")

        Response.Write(mobjValues.OptionControl(17396, "optCertype","Póliza", CStr(1), CStr(2), , , ,"Póliza a procesar"))

        Response.Write("</TD>		" & vbCrLf)
        Response.Write("			<TD COLSPAN=2></TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=17397>Ramo</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        Response.Write(mobjValues.BranchControl("cbeBranch","Ramo al que debe estar asociada la información de la Póliza/Solicitud/Cotización pendiente por imprimir.", , , , , , , False))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=17398>Producto</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        With mobjValues
            Response.Write(mobjValues.ProductControl("valProduct","Producto al que debe estar asociada la información de la Póliza/Solicitud/Cotización pendiente por imprimir."))
        End With
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD> ")

        Response.Write(mobjValues.OptionControl(17399, "optCertype","Cotización", CStr(2), CStr(3), , , ,"Cotización"))

        Response.Write("</TD>				" & vbCrLf)
        Response.Write("			<TD COLSPAN=2> </TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=17400>Póliza/Cotización</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        Response.Write(mobjValues.PolicyControl("tcnPolicy","Número de la póliza/cotización pendiente por imprimir.", "cbeBranch", CStr(0), "valProduct", CStr(0), , "", "tcnCertif", , , , , "", , , False))
    
        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=17401>Certificado/Cotización</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

'        Response.Write(mobjValues.CertificatControl("tcnCertif", "Número de certificado/cotización pendiente por imprimir.", "cbeBranch", CStr(0), "valProduct", CStr(0), CStr(2), , , , , , , , False, , , False, "eFunctions.Values.ePolControlType.eCertificate"))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=17402>Estado de Documentos</LABEL></TD>" & vbCrLf)
        Response.Write("				")

        mobjValues.BlankPosition = False
        Response.Write("" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.PossiblesValues("cbeStatusDocument", "Table5031", eFunctions.Values.eValuesType.clngComboType, CStr(2), , , , , , , False, ,"Estado de documentos que desea mostrar.", eFunctions.Values.eTypeCode.eString))

        Response.Write("</TD>		" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("    </TABLE>")

    End Sub

</script>
<%
'----------------------------------------------------------------------------------------------------
'**+Objective: 
'**+Version: $$Revision: $
'+Objetivo: Impresión de documentos
'+Version: $$Revision: $
'----------------------------------------------------------------------------------------------------
Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values

mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<html>
<head>
    <meta name="GENERATOR" content="Visual TIME Templates" >
    <%=mobjValues.StyleSheet()%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
<script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script language="JavaScript" src="/VTimeNet/Scripts/tmenu.js"></script> 
<script>

//**-Objetive: This line keeps the version coming from VSS
//-Objeto: Esta línea guarda la versión procedente de VSS
//----------------------------------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 1 $|$$Date: 09/16/03 1:00p|$$Author: nsoler $"
//----------------------------------------------------------------------------------------------------------------------

//**%Objetive: This function enables or disables the fields of the page, depending on the action
//%Objetivo: Esta función habilita o deshabilita los campos de la página, dependiendo de la acción
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	}
}

//**%Objetive: This function executes the necesary code when the action is to cancel
//%Objetivo: Esta función ejecuta el código necesario cuando la acción es cancelar
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//**%Objetive: This function executes the necessary code when the action is to finalize
//%Objetivo: Esta función ejecuta el código necesario cuando la acción es finalizar
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//**% setCAP: 
//% setCAP: Asigna los valores de los parámetros del campo "Promotor" o "Intermediario" para la póliza
//------------------------------------------------------------------------------
function setCAP(nOrder, Field){
//------------------------------------------------------------------------------
	with(self.document.forms[0]){
		valIntermedia.value='';
		UpdateDiv("valIntermediaDesc","")
		
		if (nOrder==1){
		    cbeAgency.Parameters.Param2.sValue = Field.value;
		}
		  
		if (nOrder==2){
		    valIntermedia.Parameters.Param1.sValue = Field.value;
			valIntermedia.Parameters.Param2.sValue = cbeOfficeAgen.value;
		}	
			
	}
}


</script>
<%
With Request
	mobjMenu = New eFunctions.Menues
	mobjMenu.sSessionID = Session.SessionID
	Response.Write(mobjMenu.MakeMenu("LT500","LT500_K.aspx",1, Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"), Session("sSche_code")))
	mobjMenu = Nothing
End With
%>
</head>    
<body onunload="closeWindows();">

<form method="post" id="form" name="<%=Request.QueryString.Item("sCodispl")%>" action="valLetter.aspx?sZone=1">
<%
Response.Write("<BR><BR>")
insPreLT500_K()
mobjValues = Nothing

mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
%>
</form>
</body>
</html>



