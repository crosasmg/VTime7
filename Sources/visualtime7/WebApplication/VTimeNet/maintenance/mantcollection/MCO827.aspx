<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la págin
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las características del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReject_causeColumnCaption"), "tcnReject_cause", 5, vbNullString,  , GetLocalResourceObject("tcnReject_causeColumnToolTip"), False, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString,  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .addcheckcolumn(0, GetLocalResourceObject("chkEndeavourColumnCaption"), "chkEndeavour", "", CShort("2"), "2", "Change(this)", True)
		Else
			Call .addcheckcolumn(0, GetLocalResourceObject("chkEndeavourColumnCaption"), "chkEndeavour", "", CShort("2"), "2", "Change(this)", False)
		End If
	End With
	
	'+ Se definen las columns del Grid
	With mobjGrid
		.Codispl = "MCO827"
		.Codisp = "MCO827"
		.sCodisplPage = "MCO827"
		.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
		.Columns("tctDescript").EditRecord = True
		.Height = 300
		.Width = 600
		.Columns("Sel").GridVisible = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401
		.sEditRecordParam = "nBank_code=" & Request.QueryString.Item("nBank_code") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay")
		.sDelRecordParam = "nReject_cause=' + marrArray[lintIndex].tcnReject_cause + '" & "&nBank_code=" & Request.QueryString.Item("nBank_code") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		Response.Write(mobjValues.HiddenControl("hddBank_code", Request.QueryString.Item("nBank_code")))
		Response.Write(mobjValues.HiddenControl("hddWay_pay", Request.QueryString.Item("nWay_pay")))
	End With
End Sub

'% insPreMCO827: se realiza el manejo de los campos de la zona masiva de la transacción
'--------------------------------------------------------------------------------------------
Private Sub insPreMCO827()
	'--------------------------------------------------------------------------------------------
	Dim lcolReject_cause As eCollection.Reject_causes
	Dim lclsReject_cause As Object
	
	lcolReject_cause = New eCollection.Reject_causes
	
	If lcolReject_cause.Find(mobjValues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsReject_cause In lcolReject_cause
			With mobjGrid
				.Columns("tcnReject_cause").DefValue = lclsReject_cause.nRejectcause
				.Columns("tctDescript").DefValue = lclsReject_cause.sDescript
				.Columns("tctShort_des").DefValue = lclsReject_cause.sShort_des
				.Columns("cbeStatregt").DefValue = lclsReject_cause.sStatregt
				.Columns("chkEndeavour").DefValue = lclsReject_cause.sNO_Endeavour
				
				If lclsReject_cause.sNO_Endeavour = "1" Then
					.Columns("chkEndeavour").checked = CShort("1")
				Else
					.Columns("chkEndeavour").checked = CShort("2")
				End If
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsReject_cause
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lcolReject_cause = Nothing
	lclsReject_cause = Nothing
End Sub

'% insPreMCO827Upd: se realiza el manejo de los campos de la transacción
'--------------------------------------------------------------------------------------------
Private Sub insPreMCO827Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsReject_cause As eCollection.Reject_cause
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsReject_cause = New eCollection.Reject_cause
			If lclsReject_cause.inspostMCO827(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nReject_cause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeStatregt")) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantCollection.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("MainAction"),  , CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insDefAdd();</" & "Script>")
		End If
	End With
	lclsReject_cause = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCO827"

%>


<HTML>
<HTML>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:57 $|$$Author: Nvaplat61 $"

//--------------------------------------------------------------------------------------------
function insDefAdd(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el consecutivo más alto existente en el grid
	    var llngMax    = 0
	    var llngMaxUlt = 0

//+ Se genera el número consecutivo del Order
		for(var llngIndex = 0;llngIndex<eval(top.opener.marrArray.length);llngIndex++)
		    if(eval(top.opener.marrArray[llngIndex].tcnReject_cause)>llngMax)
		        llngMax = top.opener.marrArray[llngIndex].tcnReject_cause

		if(++llngMax.length > self.document.forms[0].tcnReject_cause.maxLength)
//+ Se asignan null
			self.document.forms[0].tcnReject_cause.value = "";						//+ null			
		else
//+ Se asignan el valor por defecto del Order			
			self.document.forms[0].tcnReject_cause.value = ++llngMax;				//+ Consecutivo			
	}

//--------------------------------------------------------------------------------------------
function Change(Field){
//--------------------------------------------------------------------------------------------
	if (Field.value=='1')
		Field.value=2
	else		
		Field.value=1

}

</SCRIPT> 
<%
Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write(mobjMenu.setZone(2, "MCO827", "MCO827"))
	mobjMenu = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();" bgColor=" ">
<FORM METHOD="POST" ID="FORM" NAME="MCO827" ACTION="valMantCollection.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCO827()
Else
	Call insPreMCO827Upd()
	
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





