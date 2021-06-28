<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values

    '- Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    Dim mstrErrors As String
    Dim mobjMantTables As Object


    '% insValMantTables: Se realizan las validaciones masivas de la forma
    '--------------------------------------------------------------------------------------------
    Function insValMantTables() As String
        '--------------------------------------------------------------------------------------------
        Dim lclsTabGen As eGeneralForm.TabGen

        lclsTabGen = New eGeneralForm.TabGen

        insValMantTables = vbNullString

        insValMantTables = lclsTabGen.insValMA1000_k(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Request.Form.Item("tcnCodigint"), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("cbeStatregt"))
        lclsTabGen = Nothing
    End Function

    '% insPostMantTables: Se realizan las actualizaciones a las tablas
    '--------------------------------------------------------------------------------------------
    Function insPostMantTables() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lclsTabGen As eGeneralForm.TabGen
        Dim sDescript, sShortDes As String

        insPostMantTables = True

        lclsTabGen = New eGeneralForm.TabGen

        sDescript = Mid(Request.Form.Item("tctDescript"), 1, Request.Form.Item("hSizeDes"))
        sShortDes = Mid(Request.Form.Item("tctShort_des"), 1, Request.Form.Item("hSizeShortDes"))

        insPostMantTables = lclsTabGen.insPostMA1000(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"),
                                                     mobjValues.StringToType(Request.Form.Item("tcnCodigint"), eFunctions.Values.eTypeData.etdDouble),
                                                     sDescript, sShortDes, Request.Form.Item("cbeStatregt"),
                                                     mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                     mobjValues.StringToType(Request.Form.Item("cbeTableNew"), eFunctions.Values.eTypeData.etdDouble),
                                                     Session("companyId"),
                                                     mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdInteger))
        lclsTabGen = Nothing
    End Function

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mstrCommand = "&sModule=Maintenance&sProject=MantTables&sCodisplReload=" & Request.QueryString.Item("sCodispl")

%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<SCRIPT>
//%CancelErrors: Va a la ventana anterior si se produce un error.
//------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------
	self.history.go(-1)}

//%NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantTables
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
Else
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantTablesError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostMantTables Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
			Else
				'+ Se recarga la página que invocó la PopUp
				Response.Write("<SCRIPT>top.opener.document.location.href='MA1000_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
			End If
		End If
	End If
End If
mobjValues = Nothing
mobjMantTables = Nothing
%>
</BODY>
</HTML>





