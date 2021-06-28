<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

Dim mintAcc_Cash As Object
Dim mintOffice As Object
Dim mintCurrency As Object
Dim mlngTransac As Object
Dim mdmtEffecadte As Object
Dim mdblAmount As Object
Dim mdmtOrigCollecDate As Date


'----------------------------------------------------------------------------
Private Sub insLoadOP015()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("    </P>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""6"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH = ""20%""></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""30%""><LABEL ID=8618>" & GetLocalResourceObject("gmnChekAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""15%"">")


Response.Write(mobjValues.NumericControl("gmnChekAmount", 24, mobjValues.StringToType(mdblAmount, eFunctions.Values.eTypeData.etdDouble),  , "", True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("lblCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mintCurrency, eFunctions.Values.eTypeData.etdDouble),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("gmtCurrency", mintCurrency))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8621>" & GetLocalResourceObject("gmdOrigCollecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("gmdOrigCollecDate", mdmtEffecadte,  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8620>" & GetLocalResourceObject("gmdNewCollectDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN = 3>")


Response.Write(mobjValues.DateControl("gmdNewCollectDate", "",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("nAcc_cash", mobjValues.StringToType(mintAcc_Cash, eFunctions.Values.eTypeData.etdDouble)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("nOffice", mobjValues.StringToType(mintOffice, eFunctions.Values.eTypeData.etdDouble)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("nTransac", mobjValues.StringToType(mlngTransac, eFunctions.Values.eTypeData.etdDouble)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("dEffecdate", CStr(mobjValues.StringToDate(mdmtEffecadte))))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    ")

	
End Sub
'------------------------------------------------------------------------------------------------------------------
Private Function insPreOP015() As Object
	'------------------------------------------------------------------------------------------------------------------
	Dim lclsCash_mov As eCashBank.Cash_mov
	lclsCash_mov = New eCashBank.Cash_mov
	
	lclsCash_mov.FindByDocument(10, Session("gmnCheNum"), mobjValues.StringToType(Session("cboBank"), eFunctions.Values.eTypeData.etdDouble))
	
	With lclsCash_mov
		mintAcc_Cash = .nAcc_cash
		mintOffice = .nOffice
		mintCurrency = .nCurrency
		mlngTransac = .nTransac
		mdmtEffecadte = .dEffecdate
		mdblAmount = .nAmount
		mdmtOrigCollecDate = .dDoc_date
	End With
	lclsCash_mov = Nothing
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "op015"

%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP015", "OP015.aspx"))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
    <%=mobjValues.ShowWindowsName("OP015")%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmdateChange" ACTION="ValCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Call insPreOP015()
Call insLoadOP015()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>    




