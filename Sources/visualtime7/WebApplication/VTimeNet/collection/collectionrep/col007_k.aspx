<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**+ Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim sCodispl As Object



'**% insPreCOL007: The controls of the window are loaded.  
'% insPreCOL007: Se cargan los controles de la ventana.
'----------------------------------------------------------------------------
Private Sub insPreCOL007()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<BR></BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">  " & vbCrLf)
Response.Write("		<BR>  " & vbCrLf)
Response.Write("		")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))


Response.Write("  " & vbCrLf)
Response.Write("		</BR> " & vbCrLf)
Response.Write("        <TR>  " & vbCrLf)
Response.Write("			<TD WIDTH=""30%""> &nbsp; </TD> " & vbCrLf)
Response.Write("    		<TD><LABEL ID=101242>" & GetLocalResourceObject("valOfficeCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("                ")

	mobjValues.BlankPosition = False
Response.Write("  " & vbCrLf)
Response.Write("			<TD> " & vbCrLf)
Response.Write("	        	")

	With Response
		mobjValues.TypeList = 2
		'mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Write(mobjValues.PossiblesValues("valOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(0), False, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valOfficeToolTip"),  , 2))
	End With
Response.Write(" " & vbCrLf)
Response.Write("		    </TD> " & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("			<TD WIDTH=""30%""> &nbsp; </TD> " & vbCrLf)
Response.Write("			<TD WIDTH=""5%""><LABEL ID=0>" & GetLocalResourceObject("valAgentCodeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		")

	
	With mobjValues.Parameters
		.Add("nIntertyp", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("valAgentCode", "tabIntermedia1", 2, "", True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valAgentCodeToolTip"),  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">		" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD WIDTH=""55%"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Listar cheques"">" & GetLocalResourceObject("AnchorListar chequesCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>	" & vbCrLf)
Response.Write("				<TD WIDTH=""15%""> &nbsp; </TD>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdEffecDate", vbNullString,  , GetLocalResourceObject("tcdEffecDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD></TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcdPendDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdPendDate", vbNullString,  , GetLocalResourceObject("tcdPendDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>			" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("")

	mobjValues = Nothing
	
End Sub

</script>
<%Response.Expires = 0
Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>

    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
    </SCRIPT>



	<%

With Response
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("COL007", "COL007_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
	
<SCRIPT>

//------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------
{
	return true;
}

//------------------------------------------------------------------------------
function insFinish()
//------------------------------------------------------------------------------
{
	return true;
}

</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="post" ID="FORM" NAME="frmPostChecks" ACTION="valCollectionRep.aspx?mode=1">

	<%
Call insPreCOL007()
%>
</FORM>

</BODY>

</HTML>





