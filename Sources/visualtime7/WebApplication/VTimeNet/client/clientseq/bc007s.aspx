<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjClient As eClient.Client
Dim mobjGrid_S As eFunctions.Grid
Dim mobjGrid_H As eFunctions.Grid


'%insPreSi007. Esta funcion se encarga de realizar la busqueda de los datos de cliente
'--------------------------------------------------------------------------------------------
Private Sub insPreSi007()
	'--------------------------------------------------------------------------------------------
	mobjClient = New eClient.Client
	mobjClient.Find(Session("sClient"))
End Sub

'%GetValue. Esta funcion se encarga de formatear el valor de los campos nulos que 
'%vienen de la clase Client.
'--------------------------------------------------------------------------------------------
Private Function GetValue(ByRef lvntValue As Object) As Object
	'--------------------------------------------------------------------------------------------
	If lvntValue = -32768.3276 Then
		GetValue = ""
	Else
		GetValue = lvntValue
	End If
End Function

'% insDefineHeader: Define la estructura del grid para los deportes.
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	mobjGrid_S = New eFunctions.Grid
	With mobjGrid_S
		
		.Columns.AddCheckColumn(0, "", "sSel", "",  , "")
		.Columns.AddTextColumn(0, GetLocalResourceObject("tctSportColumnCaption"), "tctSport", 20, "",  , GetLocalResourceObject("tctSportColumnToolTip"))
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Codispl = "BC007S"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Function

'%InsPreBC007S: Obtiene la información según los datos ingresados para los deportes
'--------------------------------------------------------------------------------------------
Private Sub InsPreBC007S()
	'--------------------------------------------------------------------------------------------
	Dim lobjSports As eClient.Sports
	Dim lobjSport As eClient.Sport
	
	With Server
		lobjSports = New eClient.Sports
		lobjSport = New eClient.Sport
	End With
	
	'+ Se buscan las relaciones del cliente
	If lobjSports.Find(Session("sClient")) Then
		For	Each lobjSport In lobjSports
			With mobjGrid_S
				.Columns("sSel").DefValue = CStr(lobjSport.nSport)
				.Columns("sSel").Checked = CShort(lobjSport.sSel)
				.Columns("tctSport").DefValue = lobjSport.sDescript
				
				Response.Write(.DoRow)
			End With
		Next lobjSport
	End If
	Response.Write(mobjGrid_S.closeTable)
	
	lobjSports = Nothing
	lobjSport = Nothing
End Sub

'% insDefineHeader_H: Se define el grid para los hobbys.
'------------------------------------------------------------------------
Private Function insDefineHeader_H() As Object
	'------------------------------------------------------------------------
	mobjGrid_H = New eFunctions.Grid
	With mobjGrid_H
		
		.Columns.AddCheckColumn(0, "", "sSel_H", "",  , "")
		.Columns.AddTextColumn(0, GetLocalResourceObject("tctHobbyColumnCaption"), "tctHobby", 20, "",  , GetLocalResourceObject("tctHobbyColumnToolTip"))
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Codispl = "BC007S"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Function

'%InsPreBC007S: Obtiene la información según los datos ingresados para los hobbys
'--------------------------------------------------------------------------------------------
Private Sub InsPreBC007S_H()
	'--------------------------------------------------------------------------------------------
	Dim lobjHobbys As eClient.Hobbys
	Dim lobjHobby As eClient.Hobby
	
	With Server
		lobjHobbys = New eClient.Hobbys
		lobjHobby = New eClient.Hobby
	End With
	
	'+ Se buscan las relaciones del cliente
	If lobjHobbys.Find(Session("sClient")) Then
		For	Each lobjHobby In lobjHobbys
			With mobjGrid_H
				.Columns("sSel_H").DefValue = CStr(lobjHobby.nHobby)
				.Columns("sSel_H").Checked = CShort(lobjHobby.sSel)
				.Columns("tctHobby").DefValue = lobjHobby.sDescript
				
				Response.Write(.DoRow)
			End With
		Next lobjHobby
	End If
	Response.Write(mobjGrid_H.closeTable)
	
	lobjHobbys = Nothing
	lobjHobby = Nothing
End Sub

</script>
<%Response.Expires = 0


mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

Call insPreSi007()
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>   
<STYLE type="text/css">
    input[type=text] { width: 30%}
</STYLE> 




<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjMenu.setZone(2, "BC007S", "BC007S.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
	
//% insEnabledFields: Inhabilita los campos de la ventana que estén llenos si la variable
//%					  de sesión "sOriginalForm" es diferente de blanco - ACM - 07/08/2001
//**% insEnabledFields: Disable form fields if they're not empty and the session variable named
//**%					"sOriginalForm" is not blank - ACM - 07-Aug-2001
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
//+ Fumador
		if(elements["chkSmoking"].value!=1 && elements["chkSmoking"].value == "")
			elements["chkSmoking"].disabled=false
		else
			elements["chkSmoking"].disabled=true;
			
//+ Peso
		if(elements["tcnWeight"].value=="" || elements["tcnWeight"].value==0)
			elements["tcnWeight"].disabled=false
		else
			elements["tcnWeight"].disabled=true;

//+ Estatura
		if(elements["tcnHeight"].value=="" || self.document.forms[0].elements["tcnHeight"].value==0)
			elements["tcnHeight"].disabled=false
		else
			elements["tcnHeight"].disabled=true;

//+ Deportes
		if(elements["cbeSport"].value=="" || elements["cbeSport"].value==0)
			elements["cbeSport"].disabled=false
		else
			elements["cbeSport"].disabled=true;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmBC001N" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="25%" colspan="1"><LABEL ID=9800><%= GetLocalResourceObject("tcnWeightCaption") %></LABEL></TD>
			<TD WIDTH="20%" colspan="1"><%=mobjValues.NumericControl("tcnWeight", 5, GetValue((mobjClient.nWeight)), False, GetLocalResourceObject("tcnWeightToolTip"), True, 2)%></TD>
			<TD WIDTH="10%" COLSPAN="1"> </TD>
            <TD WIDTH="25%" colspan="1"><LABEL ID=9798><%= GetLocalResourceObject("tcnHeightCaption") %></LABEL></TD>
			<TD WIDTH="20%" colspan="1"><%=mobjValues.NumericControl("tcnHeight", 4, GetValue((mobjClient.nHeight)), False, GetLocalResourceObject("tcnHeightToolTip"), True, 2)%></TD>
		</TR>
	</TABLE>
	<BR>
	<TABLE WIDTH="100%">  
		<TR>    
			<TD WIDTH="45%" COLSPAN="1" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD WIDTH="10%" COLSPAN="1"> </TD>
		    <TD WIDTH="45%" COLSPAN="1" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		<TR>
		    <TD WIDTH="45%" COLSPAN="1" CLASS="HORLINE"></TD>
            <TD WIDTH="10%" COLSPAN="1"> </TD>
		    <TD WIDTH="45%" COLSPAN="1" CLASS="HORLINE"></TD>
		</TR>
		<TR>
			<TD COLSPAN="1" VALIGN=TOP WIDTH=45%>
				<%
insDefineHeader()
InsPreBC007S()
mobjGrid_S = Nothing
%>
		    </TD>
		    <TD WIDTH=10%>&nbsp;</TD>
		    <TD COLSPAN="1" VALIGN=TOP WIDTH=45%>
				<%
insDefineHeader_H()
InsPreBC007S_H()
mobjGrid_H = Nothing
%>
		    </TD>   
		</TR>        			
	</TABLE>
<%
mobjValues = Nothing
mobjClient = Nothing%>
</FORM>    
</BODY>
</HTML>
<%
If CStr(Session("sOriginalForm")) <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields();</script>")
End If
%>




