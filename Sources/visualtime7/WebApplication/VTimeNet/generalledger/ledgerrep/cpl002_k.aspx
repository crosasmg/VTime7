<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	
	mobjValues.sCodisplPage = "CPL002_K"
End With

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
}

//% ShowValuesField: Refresca el valor de la fecha de Cierre
//------------------------------------------------------------------------------------------
function ShowValuesField(Field){
//------------------------------------------------------------------------------------------
    insDefValues("CPL002","nLedCompan=" + Field.value ,"/VTimeNet/GeneralLedGer/LedgerRep")
}
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CPL002", "CPL002_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CPL002" ACTION="ValLedGerRep.aspx?mode=1">
<SCRIPT>
//------------------------------------------------------------------------------------------
function EnableField(Field){

    switch(Field.value){
    
		case "0":    
            self.document.forms[0].tcdClosedate.disabled = false
            break
            
		case "1":                
            self.document.forms[0].tcdClosedate.disabled = true
            self.document.forms[0].tcdClosedate.value = self.document.forms[0].tcdDate.value
    }            
}
</SCRIPT>    
    <BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLedCompanCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeLedCompan", "tabcompanyclient", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "ShowValuesField(this)", False, 30, GetLocalResourceObject("cbeLedCompanToolTip"), eFunctions.Values.eTypeCode.eString, 1))
End With
%>
            </TD>
            <TD ROWSPAN=2>
                <TABLE WIDTH="100%">
                    <TR>
                        <TD WIDTH="30%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Tipo de cierre"><%= GetLocalResourceObject("AnchorTipo de cierreCaption") %></a></LABEL></TD>
                    </TR>
			        <TR>
   				        <TD COLSPAN="2" CLASS="HorLine"></TD>
   				    </TR>    
   				    
   				    <TR>
				        <TD>
					        <%With Response
	.Write(mobjValues.OptionControl(0, "optClose", GetLocalResourceObject("optClose_CStr0Caption"), CStr(1), CStr(0), "EnableField(this)",  , 2))
	.Write(mobjValues.OptionControl(0, "optClose", GetLocalResourceObject("optClose_CStr1Caption"), CStr(0), CStr(1), "EnableField(this)",  , 2))
End With
%>
				        </TD>   				    
                    </TR> 				           				    
                </TABLE>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdClosedateCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.DateControl("tcdClosedate",  ,  , GetLocalResourceObject("tcdClosedateToolTip"),  ,  ,  ,  , False, 3))%></TD>
            <TD><%=mobjValues.HiddenControl("tcdDate", "")%></TD>            
        </TR>        

    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




