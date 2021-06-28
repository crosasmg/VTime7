<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para menjo de grid
Dim mobjGrid As eFunctions.Grid

'- Variables para almacenar parametros de pagina
Dim mstrCertype As String
Dim mintBranch As String
Dim mintProduct As String
Dim mlngPolicy As String
Dim mlngCertif As String
Dim mdteEffecdate As Date
Dim mstrClient As String

'- Variables utilizadas para guardar el valor de los distintos checked 
Dim nInd_cobra As Object
Dim nGen_cobra As Object



'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "poldata"
	
	With mobjGrid
		.Codispl = "SCA003"
		.AddButton = False
		.DeleteButton = False
	End With
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", 10, "",  , GetLocalResourceObject("tcnCoverColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescCoverColumnCaption"), "tctDescCover", 30, "",  , GetLocalResourceObject("tctDescCoverColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 10, "", True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 30, "",  , GetLocalResourceObject("tctCurrencyColumnToolTip"))
	End With
	mobjGrid.Columns("Sel").GridVisible = False
End Sub

'% insPreSCA003: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA003()
	'--------------------------------------------------------------------------------------------
	Dim lcolCovers As ePolicy.Covers
	Dim lclsCover As ePolicy.Cover
	
	lclsCover = New ePolicy.Cover
	lcolCovers = New ePolicy.Covers
	
	If lcolCovers.Find_CovSI001(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdLong), mdteEffecdate, mstrClient) Then
		For	Each lclsCover In lcolCovers
			With mobjGrid
				.Columns("tcnCover").DefValue = CStr(lclsCover.nCover)
				.Columns("tcnCapital").DefValue = CStr(lclsCover.nCapital)
				.Columns("tctDescCover").DefValue = lclsCover.sDescript
				.Columns("tctCurrency").DefValue = lclsCover.sShort_Des
				
				response.Write(.DoRow)
			End With
		Next lclsCover
	End If
	response.Write(mobjGrid.closeTable())
	lcolCovers = Nothing
	lclsCover = Nothing
End Sub

</script>
<%response.Expires = -1

mobjValues = New eFunctions.Values

'+ Se deja la pagina en modo consulta     
mobjValues.ActionQuery = True

'+ Se asignan valores de parámetros     
mstrCertype = Request.QueryString.Item("sCertype")
mintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
mintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
mlngPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
mlngCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
mdteEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
mstrClient = Request.QueryString.Item("sClient")


mobjValues.sCodisplPage = "poldata"
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 9/02/04 18:14 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
    <%=mobjValues.WindowsTitle("SCA847")%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPolData" ACTION="PolData.aspx">

<%With response
	.Write(mobjValues.ShowWindowsName("SCA847"))
	.Write("<BR>")
End With
%>

<%

Call insDefineHeader()
Call insPreSCA003()

With response
	
	.Write(mobjGrid.closeTable())
	.Write("<P ALIGN=""RIGHT"">")
	mobjValues.ActionQuery = False
	.Write(mobjValues.ButtonAcceptCancel("window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
	.Write("</P>")
	
End With

mobjGrid = Nothing
mobjValues = Nothing

%>
</FORM>
</BODY>
</HTML>






