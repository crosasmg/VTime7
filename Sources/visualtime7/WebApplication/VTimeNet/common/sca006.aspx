<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para obtener la información de deportes más frecuentes del cliente
'	Dim mclsWay_pay_prod
Dim mclsSport As Object
Dim mcolSports As Object


'%insDefineHeader. Definición de columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	
	mobjGrid.sCodisplPage = "sca006"
	
	With mobjGrid
		.Columns.AddTextColumn(0, GetLocalResourceObject("tcnSportColumnCaption"), "tcnSport", 5, "",  , GetLocalResourceObject("tcnSportColumnToolTip"))
		.Columns.AddTextColumn(0, GetLocalResourceObject("tctSportColumnCaption"), "tctSport", 20, "",  , GetLocalResourceObject("tctSportColumnToolTip"))
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Codispl = "SCA006"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
	
	With mobjGrid
		.Codispl = "SCA006"
		.Codisp = "SCA006"
		.Top = 135
		.Left = 100
		.Width = 350
		.Height = 250
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreSCA006: Carga los datos de la forma
'---------------------------------------------------------------------------------------
Private Sub insPreSCA006()
	'---------------------------------------------------------------------------------------
	
	
Response.Write("" & vbCrLf)
Response.Write("    <SCRIPT>" & vbCrLf)
Response.Write("    </" & "SCRIPT>" & vbCrLf)
Response.Write("	<DIV ID=""Scroll"" STYLE=""width:430;height:225;overflow:auto;outset gray""> ")

	
	With Server
		mcolSports = New eClient.Sports
		mclsSport = New eClient.Sport
	End With
	
	'+ Se buscan los deportes más frecuentes del cliente
	If mcolSports.Find_by_client(Request.QueryString.Item("sClient"), True) Then
		For	Each mclsSport In mcolSports
			With mobjGrid
				.Columns("tcnSport").DefValue = mclsSport.nSport
				.Columns("tctSport").DefValue = mclsSport.sDescript
				Response.Write(.DoRow)
			End With
		Next mclsSport
	End If
	Response.Write(mobjGrid.closeTable)
	
	mcolSports = Nothing
	mclsSport = Nothing
	
	
Response.Write("" & vbCrLf)
Response.Write("	</DIV>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("SCA006"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	<TABLE>")

	
End Sub

</script>
<%Response.Expires = -1

'- Variables auxiliares
mobjValues = New eFunctions.Values
mclsSport = New eProduct.Freq_way_prod
mcolSports = New eProduct.Freq_way_prods
mobjGrid = New eFunctions.Grid
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "sca006"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 14/11/03 12:55 $"        
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SCA006"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT> var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSCA006" ACTION="valProductSeq.aspx?Time=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreSCA006()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsSport = Nothing
mcolSports = Nothing
%>
</FORM>
</BODY> 
</HTML>





