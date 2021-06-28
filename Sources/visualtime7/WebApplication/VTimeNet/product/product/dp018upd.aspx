<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

Dim mstrTab_table As String
Dim mclsTab_Cover As Object


'% insPreDP018Upd: se controla el acceso a la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP018Upd()
	'--------------------------------------------------------------------------------------------
	If CStr(Session("nTypeCover")) = "1" Then
		mstrTab_table = "tabTab_LifCov"
		Response.Write(mobjValues.ShowWindowsName("DP018G_K"))
		Response.Write(mobjValues.WindowsTitle("DP018G_K"))
		mclsTab_Cover = New eProduct.Tab_lifcov
	Else
		mstrTab_table = "tabTabGenCov"
		Response.Write(mobjValues.ShowWindowsName("DP029_K"))
		Response.Write(mobjValues.WindowsTitle("DP029_K"))
		mclsTab_Cover = New eProduct.Tab_gencov
	End If
	
	mclsTab_Cover.Find(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble, True))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

Call insPreDP018Upd()

mobjValues.sCodisplPage = "dp018upd"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $|$$Author: Nvaplat61 $"

//%Cancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function Cancel(){
//------------------------------------------------------------------------------------------
	opener.top.frames["fraHeader"].A391.disabled=false;
	opener.top.frames["fraHeader"].A392.disabled=false;
	top.close();
}
//%ShowCover: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function ShowCover(nCover, sDescript){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcnCover.value = nCover;
		UpdateDiv('tcnCoverDesc',sDescript);
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP002" ACTION="valCoverSeq.aspx?nAction=392&nMainAction=306&sDup=1&sZone=2">
	<TABLE WIDTH=100%>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnCoverCaption") %><LABEL></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.PossiblesValues("tcnCover", CStr(mstrTab_table), eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnCoverToolTip"))%></TD>
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnCoverNewCaption") %><LABEL></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.NumericControl("tcnCoverNew", 5, vbNullString,  , GetLocalResourceObject("tcnCoverNewToolTip"))%></TD>
		</TR>
	</TABLE>
	<BR>
	<TABLE WIDTH=100%>
		<TR><TD CLASS="HorLine"></TD>
		<TR>
			<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel( , "Cancel()")%></TD>
		</TR>
	</TABLE>
<%
With Response
	.Write("<SCRIPT>")
	.Write("ShowCover('" & Session("nCover") & "','" & mclsTab_Cover.sDescript & "')")
	.Write("</SCRIPT>")
End With
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsTab_Cover = Nothing
%>




