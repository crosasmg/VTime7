<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% ValTables: Valida que el registro que se va a borrar no contenga registros asociados
'% en la tabla Client
'--------------------------------------------------------------------------------------------
    Sub ShowCurrency()
        '--------------------------------------------------------------------------------------------
        Dim mclsGen_Cover As eProduct.Gen_cover
        mclsGen_Cover = New eProduct.Gen_cover
	
        Dim lintBranch As Integer
        Dim lintProduct As Integer
        Dim lintCover As Integer
        Dim ldtmEffecdate As Date
	
        lintBranch = CLng(Request.QueryString.Item("nBranch"))
        lintProduct = CLng(Request.QueryString.Item("nProduct"))
        lintCover = CLng(Request.QueryString.Item("nCover"))
        
        If Request.QueryString.Item("dEffecdate") = vbNullString Then
            ldtmEffecdate = Today
        Else
            ldtmEffecdate = Request.QueryString.Item("dEffecdate")
        End If
	
        If mclsGen_Cover.Find_Currency(lintBranch, lintProduct, lintCover, ldtmEffecdate) Then
            Response.Write("top.frames[""fraHeader""].document.forms[0].cbeCurrency.value=" + CStr(mclsGen_Cover.nCurrency) + ";")
        End If
	
        mclsGen_Cover = Nothing
    End Sub

</script>

<%Response.Expires = -1
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
    Select Case Request.QueryString.Item("Field")
        Case "ShowCurrency"
            Call ShowCurrency()
    End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>







