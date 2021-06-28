<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mclsProduct As eProduct.Product
Dim mintLife As Object
Dim mobjMenu As eFunctions.Menues
Dim mclsErrors As eFunctions.Errors
Dim mblnError As Boolean
Dim mintRepinsur As Object


'% insPreDP023: se controla la carga de valores de la página
'----------------------------------------------------------------------------------------------
Private Sub insPreDP023()
	'----------------------------------------------------------------------------------------------
	mintLife = 2
	If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
		mintLife = 1
		Call mclsProduct.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End If
	Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	If mclsProduct.nRepInsured = eRemoteDB.Constants.intNull Then
		mintRepinsur = 1
	Else
		mintRepinsur = mclsProduct.nRepInsured
	End If
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsProduct = New eProduct.Product
mobjMenu = New eFunctions.Menues

Call insPreDP023()

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "DP023"

mblnError = False

If Not mobjValues.ActionQuery Then
	If mclsProduct.sGroupind <> "1" And mclsProduct.sMultiind <> "1" Then
		mblnError = True
		mobjValues.ActionQuery = True
	End If
End If
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP023", "DP023.aspx"))
	mobjMenu = Nothing
End With
%>	
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 19/04/04 15:32 $|$$Author: Nvaplat11 $"

//% ShowUseGroups: Habilita/deshabilita el checkbox de "Usar Grupos" si en alguno de los combos
//				Coberturas, Cláusulas o Recargos/Descuentos se selecciona el valor "por grupos"
//---------------------------------------------------------------------------------------------
function ShowUseGroups(){
//---------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if(elements["cbeCoverType"].value == 3 ||
		   elements["cbeXpremType"].value == 3 ||
		   elements["cbeClauseType"].value == 3)
			elements["chkUseGroups"].checked = true;
    }
}   
//% ShowOptPremium: Habilita/deshabilita los radiobotones de "Método para Cálculo de Prima"
//					dependiendo si el producto es de "Vida"
//-----------------------------------------------------------------------------------------
function ShowOptPremium(Value){
//-----------------------------------------------------------------------------------------
	var lblnDisabled = (Value==1)?false:true;
    with (self.document.forms[0]){
        elements["optPremCalc"][0].disabled = lblnDisabled;
        elements["optPremCalc"][1].disabled = lblnDisabled;
        elements["optPremCalc"][2].disabled = lblnDisabled;
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP023" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5"><%=mobjValues.CheckControl("chkUseGroups", GetLocalResourceObject("chkUseGroupsCaption"), mclsProduct.sGroupsi, "1",  ,  , 1, GetLocalResourceObject("chkUseGroupsToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41300><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41301><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(41305, "optPremCalc", GetLocalResourceObject("optPremCalc_3Caption"), mclsProduct.sMethprin, "3",  ,  , 4, GetLocalResourceObject("optPremCalc_3ToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnMinInsuredCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnMinInsured", 10, CStr(mclsProduct.nInsminiq),  , GetLocalResourceObject("tcnMinInsuredToolTip"),  , 0,  ,  ,  ,  ,  , 5)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(41303, "optPremCalc", GetLocalResourceObject("optPremCalc_1Caption"), mclsProduct.sMethprav, "1",  ,  , 2, GetLocalResourceObject("optPremCalc_1ToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14367><%= GetLocalResourceObject("tcnMaxInsuredCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnMaxInsured", 10, CStr(mclsProduct.nInsmaxiq),  , GetLocalResourceObject("tcnMaxInsuredToolTip"),  , 0,  ,  ,  ,  ,  , 6)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(41304, "optPremCalc", GetLocalResourceObject("optPremCalc_2Caption"), mclsProduct.sMethagav, "2",  ,  , 3, GetLocalResourceObject("optPremCalc_2ToolTip"))%></TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=14369><%= GetLocalResourceObject("cbeRepInsuredCaption") %></LABEL></TD>            
            <TD><%
mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeRepInsured", "Table5677", eFunctions.Values.eValuesType.clngComboType, mintRepinsur,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRepInsuredToolTip")))
%>
			</TD>            
        </TR>
        
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=41302><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14369><%= GetLocalResourceObject("cbeReceiptTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeReceiptType", "Table50", 1, mclsProduct.sColinvot,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeReceiptTypeToolTip"),  , 7)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14366><%= GetLocalResourceObject("cbeCoverTypeCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.TypeList = 2
	.List = CStr(1)
	Response.Write(mobjValues.PossiblesValues("cbeCoverType", "Table92", 1, mclsProduct.sTyp_module,  ,  ,  ,  ,  , "ShowUseGroups()",  ,  , GetLocalResourceObject("cbeCoverTypeToolTip"),  , 8))
End With
%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID=14370><%= GetLocalResourceObject("cbeXpremTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeXpremType", "Table92", 1, mclsProduct.sTyp_discxp,  ,  ,  ,  ,  , "ShowUseGroups()",  ,  , GetLocalResourceObject("cbeXpremTypeToolTip"),  , 9)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14365><%= GetLocalResourceObject("cbeClauseTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeClauseType", "Table92", 1, mclsProduct.sTyp_clause,  ,  ,  ,  ,  , "ShowUseGroups()",  ,  , GetLocalResourceObject("cbeClauseTypeToolTip"),  , 10)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
If Not mobjValues.ActionQuery Then
	With Response
		.Write("<SCRIPT>")
		.Write("ShowOptPremium(" & mintLife & ")")
		.Write("</SCRIPT>")
	End With
End If

'+ Si el producto no acepta colectivos, se le informa al usuario.  No puede incluirse
'+ información en esta ventana.
If mblnError Then
	mclsErrors = New eFunctions.Errors
	Response.Write(mclsErrors.ErrorMessage("DP023", 11059,  ,  ,  , True))
	mclsErrors = Nothing
End If

mobjValues = Nothing
mclsProduct = Nothing
%>




