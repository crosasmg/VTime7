<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 6/10/03 19:20 $"        

//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{

}

//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{
	if(top.frames['fraSequence'].pintZone==1){
		with(self.document.forms[0]){
			valIntermed.value=<%="""" & Session("nIntermed") & """"%>;
			UpdateDiv("valIntermedDesc","");
		}
	}
	else		
		return true;
}   

//------------------------------------------------------------------------------------------
function insFinish()
//------------------------------------------------------------------------------------------
{
    return true;
}

//-------------------------------------------------------------------------------------------------------------
//%Setvalues: función que asigna el parámetro para los valores posibles del campo intermediario
//-------------------------------------------------------------------------------------------------------------
function insSetvalues(Field1){
//-------------------------------------------------------------------------------------------------------------

	with (document.forms[0])
	{
	    if(typeof(document.forms[0].optIntStatus)!='undefined'){
            self.document.forms[0].valIntermed.Parameters.Param1.sValue = Field1;
	    }    
	}    
}

//-------------------------------------------------------------------------------------------------------------
//%Setvalues: función que asigna el parámetro para los valores posibles del campo intermediario
//-------------------------------------------------------------------------------------------------------------
function insSetParameters(){
//-------------------------------------------------------------------------------------------------------------

	with (document.forms[0])
	{
	    if(typeof(document.forms[0].optIntStatus)!='undefined'){
            self.document.forms[0].valIntermed.Parameters.Param1.sValue = 2;
	    }    
	}    
}				
</SCRIPT>

	<%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AG011"))
	.Write(mobjMenu.MakeMenu("AG011", "AG011_k.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>    
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmIntermNull" ACTION="ValAgent.aspx?Zone=1">
    <TABLE WIDTH="100%">
        <TR>		
            <TD ROWSPAN=2 COLSPAN=2>
                <TABLE WIDTH="100%">
                    <TR>
                        <TD WIDTH="20%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Operación"><%= GetLocalResourceObject("AnchorOperaciónCaption") %></a></LABEL></TD>
                    </TR>
			        <TR>
   				        <TD COLSPAN="2" CLASS="HorLine"></TD>
   				    </TR>    
   				    
   				    <TR>
				        <TD>
					        <%With Response
	.Write(mobjValues.OptionControl(0, "optIntStatus", GetLocalResourceObject("optIntStatus_CStr2Caption"), CStr(1), CStr(2), "insSetvalues(this.value)",  , 2))
	.Write(mobjValues.OptionControl(0, "optIntStatus", GetLocalResourceObject("optIntStatus_CStr4Caption"), CStr(0), CStr(4), "insSetvalues(this.value)",  , 2))
End With
%>
				        </TD>   				    
                    </TR> 				           				    
                </TABLE>
            </TD>			
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
			<TD WIDTH="10%"><LABEL ID=8097><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
                <%
With mobjValues.Parameters
	.Add("nOption", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>  			
			<TD WIDTH="50%"><%=mobjValues.PossiblesValues("valIntermed", "tabintermedia_a", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , False, 10, GetLocalResourceObject("valIntermedToolTip"),  , 1,  , True)%></TD>            
        </TR>
    </TABLE>
    <%
Response.Write("<SCRIPT>insSetParameters()</SCRIPT>")
%>    
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




