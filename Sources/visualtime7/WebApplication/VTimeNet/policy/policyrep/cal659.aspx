<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'-	Objeto para el manejo de las funciones asociadas a la grilla

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la tabla temporal
Dim mobjBatch As eBatch.MasiveCharge

Dim mstrType As String
Dim mstrAction As String
Dim mcolMasiveCharges As eBatch.MasiveCharges


Private Sub insPreCAL659()
	
	mobjBatch.bManualProc = (CStr(Session("sManual")) = "1")	
	Response.Write(mobjBatch.MakeCa0659(Session("sKey"), Session("nWorksheet"), mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)))	
	Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow"))))
	Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')"))
End Sub

Private Sub insPreCAL659Upd()
	Dim clsvalBatch As eBatch.ValBatch
	Dim mobjGrid As eFunctions.Grid
	
	clsvalBatch = New eBatch.ValBatch
	mobjGrid = New eFunctions.Grid
	
	If mstrAction = "Del" Then
		With Request
			Response.Write(mobjValues.ConfirmDelete(False))
			Call clsvalBatch.InsPostCAL659(Session("sKey"), mstrAction, CStr(0), 0, CInt(Request.QueryString.Item("DelRow")))
			
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & 0 & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			Response.Write(mobjGrid.DoFormUpd(mstrAction, "valPolicySeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		End With
	Else
		
	End If
	
	clsvalBatch = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal659")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cal659"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjBatch = New eBatch.MasiveCharge

mobjValues.ActionQuery = Session("bQuery")

mstrType = Request.QueryString.Item("Type")
mstrAction = Request.QueryString.Item("Action")

%>	
<HTML>
<HEAD>
<SCRIPT>
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}

//% delRecordParams: Entrega los parametros para elimnar un registro 
//-------------------------------------------------------------------------------------------
function delRecordParams(lidx) {
//-------------------------------------------------------------------------------------------
	var sRet;
	
	if (self.document.forms[0].hddRow.length > 0) {
		sRet = 'DelRow=' + self.document.forms[0].hddRow[lidx].value;
	}
	else {
		sRet = 'DelRow=' + self.document.forms[0].hddRow.value;
	}	

	return (sRet);
}

</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CAL659", "Nómina del Cliente"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Nomina del Cliente" ACTION="ValPolicyRepSeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("CAL659", Request.QueryString.Item("sWindowDescript")))

If mstrType = "PopUp" Then
	Call insPreCAL659Upd()
Else
	Call insPreCAL659()
End If

mobjValues = Nothing

%>
</FORM>
</BODY>
</HTML>            


<%
If Session("nContent") = 0 Then
	mcolMasiveCharges = New eBatch.MasiveCharges
	If mcolMasiveCharges.FindInconsist(Session("sKey"), Session("nUsercode"), Session("nWorksheet")) Then
		
		If mcolMasiveCharges.Count > 0 Then
			'+Como al procesar cada inconsistencia esta desaparece del grid
			'+sólo se usa el índice de carga si aun quedan registros
			Session("nContent") = 2
		Else
			Session("nContent") = 1
		End If
	Else
		Session("nContent") = 1
	End If
	mcolMasiveCharges = Nothing
	Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
	If CStr(Session("nContent")) = "1" Then
		Response.Write("<SCRIPT>top.frames[""fraHeader""].nContent= 1; </SCRIPT>")
	End If
	
End If
mobjValues = Nothing
mobjBatch = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("cal659")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





