<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreDP7001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP7001()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct_li As eProduct.Product
	lclsProduct_li = New eProduct.Product
	Dim lblDisabled As Boolean
	
	With mobjValues
		Call lclsProduct_li.FindProduct_li(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
	
	With lclsProduct_li
		
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcnSaving_pctCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnSaving_pct", 3, CStr(.nSaving_pct),  , GetLocalResourceObject("tcnSaving_pctToolTip"),  , 0,  ,  ,  , "insEnabled(this.value)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.CheckControl("chkS_allwchng", GetLocalResourceObject("chkS_allwchngCaption"), .sS_allwchng,  ,  ,  ,  , GetLocalResourceObject("chkS_allwchngToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("cbeIndex_tableCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.PossiblesValues("cbeIndex_table", "TABLE5520", eFunctions.Values.eValuesType.clngComboType, CStr(.nIndex_table),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeIndex_tableToolTip"), eFunctions.Values.eTypeCode.eNumeric))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.CheckControl("chkIx_allwchng", GetLocalResourceObject("chkIx_allwchngCaption"), .sIx_allwchng,  ,  ,  ,  , GetLocalResourceObject("chkIx_allwchngToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("valWarrn_tableCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>" & vbCrLf)
Response.Write("			    ")

		
		With mobjValues.Parameters
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Response.Write(mobjValues.PossiblesValues("valWarrn_table", "TABTAB_APV_WARRAN", eFunctions.Values.eValuesType.clngWindowType, CStr(.nWarrn_table), True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valWarrn_tableToolTip"), eFunctions.Values.eTypeCode.eNumeric))
		
Response.Write("" & vbCrLf)
Response.Write("			    </TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.CheckControl("chkW_allwchng", GetLocalResourceObject("chkW_allwchngCaption"), .sW_allwchng,  ,  ,  ,  , GetLocalResourceObject("chkW_allwchngToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("                <TD>")

		If .nSaving_pct <= 0 Then
			lblDisabled = True
		Else
			lblDisabled = False
		End If
		Response.Write(mobjValues.CheckControl("chkAccount_mirror", GetLocalResourceObject("chkAccount_mirrorCaption"), .sAccount_mirror,  , "ChangeValue();", lblDisabled,  , GetLocalResourceObject("chkAccount_mirrorToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("valWarrn_table_mirrorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>" & vbCrLf)
Response.Write("			    ")

		
		With mobjValues.Parameters
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
            Response.Write(mobjValues.PossiblesValues("valWarrn_table_mirror", "TABTAB_APV_WARRAN", eFunctions.Values.eValuesType.clngWindowType, CStr(.nwarrn_table_mirror), True, , , , , , .sAccount_mirror <> "1", 5, GetLocalResourceObject("valWarrn_table_mirrorToolTip"), eFunctions.Values.eTypeCode.eNumeric))
		
Response.Write("" & vbCrLf)
Response.Write("			    </TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>")

		
		If Not Session("bQuery") Then
			Response.Write("<SCRIPT>insEnabled('" & .nSaving_pct & "')</" & "Script>")
		End If
	End With
	lclsProduct_li = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "DP7001"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//**+ For the Source Safe control.
//+ Para Control de Versiones. 
//------------------------------------------------------------------------------
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.48 $"
//------------------------------------------------------------------------------

//%insEnabled: Si no incluye información en el campo "%Inversión en cuentas de ahorro", 
//% (nSaving_pct) no se habilitan el resto de los campos de esta ventana.
//------------------------------------------------------------------------------
function insEnabled(sSaving_pct){
//------------------------------------------------------------------------------
	var lblnDisabled;
	lblnDisabled = (sSaving_pct=='0' || sSaving_pct=='' || sSaving_pct=='-32768')?true:false;
	with (self.document.forms[0]){
	    if (lblnDisabled){
	        cbeIndex_table.value='';
	        valWarrn_table.value='';
	        UpdateDiv('valWarrn_tableDesc','','NoPopUp');
	        chkS_allwchng.checked=false;
	        chkIx_allwchng.checked=false;
	        chkW_allwchng.checked=false;
	        chkAccount_mirror.checked=false;
			valWarrn_table_mirror.value='';
			UpdateDiv('valWarrn_table_mirrorDesc','','NoPopUp');
	    }
	    cbeIndex_table.disabled=lblnDisabled;
	    valWarrn_table.disabled=lblnDisabled;
	    btnvalWarrn_table.disabled=lblnDisabled;
	    chkS_allwchng.disabled=lblnDisabled;
	    chkIx_allwchng.disabled=lblnDisabled;
	    chkW_allwchng.disabled=lblnDisabled;
	    chkAccount_mirror.disabled = lblnDisabled;
	    
        if (chkAccount_mirror.checked) {
	        valWarrn_table_mirror.disabled = lblnDisabled;
	        btnvalWarrn_table_mirror.disabled = lblnDisabled;
	    }
	}
}


function ChangeValue(){
//------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    if (chkAccount_mirror.checked)
			{
			valWarrn_table_mirror.disabled=false;
			btnvalWarrn_table_mirror.disabled=false;
			}
        else
			{
			valWarrn_table_mirror.value='';
			UpdateDiv('valWarrn_table_mirrorDesc','','NoPopUp');
			valWarrn_table_mirror.disabled=true;
			btnvalWarrn_table_mirror.disabled=true;
			}
	}
}

</SCRIPT>




    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	.Write(mobjMenu.setZone(2, "DP7001", "DP7001.aspx"))
	mobjMenu = Nothing
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP7001" ACTION="valProdLifeSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP7001"))
Call insPreDP7001()
mobjValues = Nothing
%></FORM>
</BODY>
</HTML>




