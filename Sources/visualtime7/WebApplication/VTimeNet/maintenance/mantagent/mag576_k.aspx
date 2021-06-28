<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


Dim lclsInterm_param As eAgent.interm_param
Dim lblndisabled As Boolean


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG576"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

//* Funcion que cancela las las acciones de la Pagina
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//+ Controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    var lintIndex;
    var error;
    if (top.frames['frasequence'].plngMainAction == 401) {
		{
		try {
			for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
				self.document.forms[0].elements[lintIndex].disabled=true;
				if(self.document.images.length>0)
				    if(typeof(self.document.images["btn" + self.document.forms[0].elements[lintIndex].name])!='undefined')
				       self.document.images["btn" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
			}
		} catch(error){}
		}

    }
    else
    {
    try {
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=false;
			if(self.document.images.length>0)
			    if(typeof(self.document.images["btn" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
		}
	} catch(error){}
	}
    <%If IsNothing(Request.QueryString.Item("nMainAction")) Then%>
	    self.document.location.href = self.document.location.href + "&nMainAction=" + top.frames['frasequence'].plngMainAction;
    <%End If%>

 }
</SCRIPT> 

<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">

<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG576.aspx", 1, ""))
	mobjMenu = Nothing
	.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	.Write("<BR><BR>")
End With


%>    

</HEAD>
<BODY ONUNLOAD="closeWindows();">

<%lclsInterm_param = New eAgent.interm_param

If Not IsNothing(Request.QueryString.Item("nMainAction")) Then
	Call lclsInterm_param.Find()
	If Request.QueryString.Item("nMainAction") = "401" Then
		lblndisabled = True
	Else
		lblndisabled = False
	End If
Else
	lblndisabled = True
End If

Response.Write(mobjValues.ShowWindowsName("MAG576"))
%>
	<FORM METHOD="POST" ID="FORM" NAME="MAG576" ACTION="valMantAgent.aspx?sMode=1">
	<BR><BR>	
	  <TABLE WIDTH="100%">
	    <TR>
	      <TD WIDTH="15%"></TD>
	      <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcnInsu_AssistCaption") %></LABEL></TD>
	      <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnInsu_Assist", 5, CStr(lclsInterm_param.nInsu_Assist), True, GetLocalResourceObject("tcnInsu_AssistToolTip"),  , 2,  ,  ,  ,  , lblndisabled)%></TD>
		</TR>		

	    <TR>
	      <TD WIDTH="15%"></TD>
	      <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcnMinAmountCaption") %></LABEL></TD>
	      <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnMinAmount", 18, CStr(lclsInterm_param.nMinAmount), False, GetLocalResourceObject("tcnMinAmountToolTip"), True, 6,  ,  ,  ,  , lblndisabled)%></TD>
	    </TR>
	      <TD WIDTH="15%"></TD>  
	      <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcnDay_DiscloanCaption") %></LABEL></TD>
	      <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnDay_Discloan", 5, CStr(lclsInterm_param.nDay_Discloan), False, GetLocalResourceObject("tcnDay_DiscloanToolTip"),  ,  ,  ,  ,  ,  , lblndisabled)%></TD>
	    </TR>

		<TR><TD><BR></TD>
		</TR>		
		<TR>
		  <TABLE WIDTH="100%">
		    <TR>
			  <TD WIDTH="100%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Incentivos/Bonos-Generales"><%= GetLocalResourceObject("AnchorIncentivos/Bonos-GeneralesCaption") %></a></LABEL></td>
		    </TR>
		    <TR>
			  <TD COLSPAN="2" CLASS="HorLine"></TD>
		    </TR>	
		    <TR><TD><BR></TD></TR>
		  </TABLE>			
		</TR>
		
		<TABLE WIDTH="100%">
		  <TR>	      
		    <TD WIDTH="15%"></TD>
	        <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcnBonus_CurrCaption") %></LABEL></TD>
	        <TD WIDTH="25%"><%=mobjValues.PossiblesValues("tcnBonus_Curr", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsInterm_param.nBonus_Curr),  ,  ,  ,  ,  ,  , lblndisabled,  , GetLocalResourceObject("tcnBonus_CurrToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
	      </TR>
	      <TR>
	        <TD WIDTH="15%"></TD>
	        <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcnMax_BonusCaption") %></LABEL></TD>
	        <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnMax_Bonus", 18, CStr(lclsInterm_param.nMax_Bonus), False, GetLocalResourceObject("tcnMax_BonusToolTip"), True, 6,  ,  ,  ,  , lblndisabled)%></TD>
		  </TR>	      
		    <TD WIDTH="15%"></TD>
	        <TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcnMax_AccompCaption") %></LABEL></TD>
	        <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnMax_Accomp", 18, CStr(lclsInterm_param.nMax_Accomp), False, GetLocalResourceObject("tcnMax_AccompToolTip"), True, 6,  ,  ,  ,  , lblndisabled)%></TD>
	      </TR>
        </TABLE>
	  </TABLE>
	</FORM>

<%
lclsInterm_param = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>





