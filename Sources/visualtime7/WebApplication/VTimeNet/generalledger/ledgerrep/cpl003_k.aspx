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
	mobjValues.sCodisplPage = "CPL003_K"
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
    return true    
//------------------------------------------------------------------------------------------
}
//% EnableField: Habilita y deshabilta el campo fecha de cierre
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
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CPL003", "CPL003_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CPL003" ACTION="ValLedGerRep.aspx?mode=1">
    <BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLedCompanCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeLedCompan", "tabcompanyclient", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  , False, 30, GetLocalResourceObject("cbeLedCompanToolTip"), eFunctions.Values.eTypeCode.eString, 1))
End With
%>
            </TD>
            <TD>
                <TABLE WIDTH="100%">
                    <TR>
                        <TD WIDTH="30%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Inicio del período"><%= GetLocalResourceObject("AnchorInicio del períodoCaption") %></a></LABEL></TD>
                    </TR>
			        <TR>
   				        <TD COLSPAN="2" CLASS="HorLine"></TD>
   				    </TR>    
   				    
                    <TR>
                        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
                        <TD><%=mobjValues.TextControl("tcnYear", 4, Session("Year"), False, GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  , 3)%></TD>
                    </TR>                               
                    <TR>            
                        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
                        <TD><%=mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "",  , 4)%></TD>
                    </TR>
			        
                </TABLE>
            </TD>
            
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLevelsCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeLevels", "table7007", eFunctions.Values.eValuesType.clngComboType, CStr(7),  ,  ,  ,  ,  ,  , False, 30, GetLocalResourceObject("cbeLevelsToolTip"),  , 2)%></TD>        
            
            <TD>
                <TABLE WIDTH="100%">
                    <TR>
                        <TD WIDTH="30%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Fin del período"><%= GetLocalResourceObject("AnchorFin del períodoCaption") %></a></LABEL></TD>
                    </TR>
			        <TR>
   				        <TD COLSPAN="2" CLASS="HorLine"></TD>
   				    </TR>    
   				    
                    <TR>
                        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
                        <TD><%=mobjValues.TextControl("tcnYearE", 4, Session("Year"), False, GetLocalResourceObject("tcnYearEToolTip"),  ,  ,  ,  ,  , 5)%></TD>
                    </TR>                               
                    <TR>            
                        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
                        <TD><%=mobjValues.PossiblesValues("cbeMonthE", "Table7013", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "",  , 6)%></TD>
                    </TR>
                </TABLE>
            </TD>            
        </TR>        

    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




