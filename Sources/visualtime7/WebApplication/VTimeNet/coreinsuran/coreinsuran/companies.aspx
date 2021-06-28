<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga del Grid de la ventana 'Companies'
Dim mclsPart_contr As eCoReinsuran.Part_contr


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "companies"
	
	With mobjGrid
		.AddButton = False
		.DeleteButton = False
		.sCodisplPage = "CRC003"
	End With
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(100539, GetLocalResourceObject("tcnCompanyColumnCaption"), "tcnCompany", 4, CStr(0))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCompanyNameColumnCaption"), "tctCompanyName", 60, "")
		Call .AddNumericColumn(100539, GetLocalResourceObject("tcnShareColumnCaption"), "tcnShare", 5, "",  ,  ,  , 2)
	End With
	
End Sub

'%insPreCompanies: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCompanies()
	'--------------------------------------------------------------------------------------------
	Dim lstraux As Object
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"" COLS=3>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""1""><LABEL ID=""0"">" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2""><LABEL CLASS=""FIELD"">")


Response.Write(Request.QueryString.Item("nNumber"))


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""1""><LABEL ID=""0"">" & GetLocalResourceObject("lblTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2""><LABEL CLASS=""FIELD"">")


Response.Write(mobjValues.PossiblesValues("lblType", "table173", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nType"),  , True,  ,  ,  , "",  ,  , ""))


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""1""><LABEL ID=""0"">" & GetLocalResourceObject("lblBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2""><LABEL CLASS=""FIELD"">")


Response.Write(mobjValues.PossiblesValues("lblBranch", "table5000", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nBranch"),  , True,  ,  ,  , "",  ,  , ""))


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write(" </TABLE>	")

	Dim lblnFind As Boolean
	Dim lintCount As Integer
	'dStartdate
	With mobjValues
		lblnFind = mclsPart_contr.Find(vbNullString, .StringToType(Request.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dStartdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nType_rel"), eFunctions.Values.eTypeData.etdDouble))
	End With
	
	If lblnFind Then
		lintCount = 0
		For lintCount = 0 To mclsPart_contr.Count - 1
			If mclsPart_contr.ItemCR307(lintCount) Then
				With mobjGrid
					.Columns("tcnCompany").DefValue = CStr(mclsPart_contr.nCompany)
					.Columns("tctCompanyName").DefValue = mclsPart_contr.sCliename
					.Columns("tcnShare").DefValue = CStr(mclsPart_contr.nShare)
					.Columns("Sel").GridVisible = False
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	Else
		mobjGrid.Columns("Sel").GridVisible = False
	End If
Response.Write("	" & vbCrLf)
Response.Write(" 	")

	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mclsPart_contr = New eCoReinsuran.Part_contr

mobjValues.sCodisplPage = "companies"

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmCompanies" ACTION="valCoReinsuran.aspx">
<%
Response.Write(mobjValues.ShowWindowsName("CR307"))
Response.Write(mobjValues.WindowsTitle("CR307"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreCompanies()
End If
%>
</FORM>
</BODY>
</HTML>
	




