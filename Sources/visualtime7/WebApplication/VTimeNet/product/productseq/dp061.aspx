<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object


'% insDefineHEADer: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineheader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(41463, GetLocalResourceObject("cboNullcodeColumnCaption"), "cboNullcode", "Table13", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  , "Disabled(0);",  ,  , GetLocalResourceObject("cboNullcodeColumnCaption"))
		Call .AddPossiblesColumn(41464, GetLocalResourceObject("cboReturn_indColumnCaption"), "cboReturn_ind", "Table96", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  , "Disabled(0);",  ,  , GetLocalResourceObject("cboReturn_indColumnToolTip"))
        Call .AddTextColumn(0, GetLocalResourceObject("tctRoutine_PayColumnCaption"), "tctRoutine_Pay",12,vbNullString,,GetLocalResourceObject("tctRoutine_PayColumnToolTip"),,,,True)            
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnRetractionColumnCaption"), "tcnRetraction", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnRetractionColumnToolTip"),  , 2)
		Call .AddNumericColumn(41465, GetLocalResourceObject("tcnReturn_ratColumnCaption"), "tcnReturn_rat", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnReturn_ratColumnToolTip"),  , 2)
		Call .AddNumericColumn(41466, GetLocalResourceObject("tcnAmelevelColumnCaption"), "tcnAmelevel", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnAmelevelColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkNotRehabColumnCaption"), "chkNotRehab", "", CShort("1"), "1", "EnablechkReaAut(this.value);", True, GetLocalResourceObject("chkNotRehabColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkReaAutColumnCaption"), "chkReaAut", "",  ,  ,  , True, GetLocalResourceObject("chkReaAutColumnToolTip"))
		Call .AddHiddenColumn("cboAuxNullcode", CStr(0)) 
		Call .AddHiddenColumn("cboAuxReturn_ind", CStr(0))
		Call .AddHiddenColumn("tcnAuxReturn_rat", CStr(0))
		Call .AddHiddenColumn("tcnAuxAmelevel", CStr(0))
		Call .AddHiddenColumn("sAuxSel", CStr(2))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP061"
		.Width = 500
		.Height = 300
		.DeleteButton = False
		.AddButton = False
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		ElseIf mstrTypeFind = "1" Then 
			.Columns("cboNullcode").EditRecord = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
	End With
End Sub

'% insPreDP01: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP061()
	'--------------------------------------------------------------------------------------------
	Dim lclsNull_condi As eProduct.Null_condi
	Dim lcolNull_condi As eProduct.Null_condis
	
	With Server
		lclsNull_condi = New eProduct.Null_condi
		lcolNull_condi = New eProduct.Null_condis
	End With
	mobjGrid.AddButton = True
	
	If lcolNull_condi.find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), "1") Then
		mobjGrid.DeleteButton = True
		For	Each lclsNull_condi In lcolNull_condi
			With mobjGrid
				.Columns("cboNullcode").DefValue = CStr(lclsNull_condi.nNullcode)
				.Columns("cboReturn_ind").DefValue = lclsNull_condi.sReturn_ind
				.Columns("tcnReturn_rat").DefValue = CStr(lclsNull_condi.nReturn_rat)
                .Columns("tctRoutine_Pay").DefValue = CStr(lclsNull_condi.sRoutine_Pay)
                .Columns("tcnRetraction").DefValue = CStr(lclsNull_condi.nRetraction)
				.Columns("tcnAmelevel").DefValue = CStr(lclsNull_condi.nAmelevel)
				.Columns("chkNotRehab").checked = CShort(lclsNull_condi.sNotRehab)
				.Columns("chkReaAut").checked = CShort(lclsNull_condi.sReaAuto)
				.Columns("cboAuxNullcode").DefValue = CStr(lclsNull_condi.nNullcode)
				.Columns("cboAuxReturn_ind").DefValue = lclsNull_condi.sReturn_ind
				.Columns("tcnAuxReturn_rat").DefValue = CStr(lclsNull_condi.nReturn_rat)
				.Columns("tcnAuxAmelevel").DefValue = CStr(lclsNull_condi.nAmelevel)
				
				.sDelRecordParam = "nNullcode=' + marrArray[lintIndex].cboAuxNullcode + '&nAmelevel=' + marrArray[lintIndex].tcnAuxAmelevel + '&nReturn_ind=' + marrArray[lintIndex].cboAuxReturn_ind + '&nReturn_rat=' + marrArray[lintIndex].tcnAuxReturn_rat + '"
				
				Response.Write(.DoRow)
			End With
		Next lclsNull_condi
	Else
		mblnVisible = True
	End If
	Response.Write(mobjGrid.closeTable())
	
	lclsNull_condi = Nothing
	lcolNull_condi = Nothing
End Sub

'% insPreDP010Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP061Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsNull_condi As eProduct.Null_condi
	Dim nAction As Byte
	
	lclsNull_condi = New eProduct.Null_condi
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		
		If lclsNull_condi.insPostDP061(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nNullcode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nAmelevel"), eFunctions.Values.eTypeData.etdDouble), "1", CStr(Request.QueryString.Item("nReturn_ind")), mobjValues.StringToType(Request.QueryString.Item("nReturn_rat"), eFunctions.Values.eTypeData.etdDouble), "1", "", "","",0) Then
			
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			
		End If
    Else
        mobjGrid.Columns("chkNotRehab").Disabled = False
	End If

	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP061", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		nAction = 0
		If Request.QueryString.Item("Action") = "Update" Then
			nAction = 1
			Response.Write("<SCRIPT>Disabled(" & nAction & ");</" & "Script>")
		End If
	End With
	
	lclsNull_condi = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP061"
mobjValues.sCodisplPage = "DP061"

If IsNothing(Request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If

mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT">

//%EnablechkReaAut: Habilita o deshabilita el campo "Rehabilitacion automatica".
//------------------------------------------------------------------------------------------
function EnablechkReaAut(Field)
//------------------------------------------------------------------------------------------
{
	with (self.document.forms[0]){
	    if(elements["chkNotRehab"].checked==true){
	        elements["chkReaAut"].checked=false;
	        elements["chkReaAut"].value="2";
	        elements["chkReaAut"].disabled=true;
	    }else{
	        elements["chkReaAut"].disabled=false;
	        elements["chkReaAut"].value="1";
	    }
    }	
}

//%Disabled: Habilita o deshabilita el campo "Porcentaje" dependiendo de la acción
//------------------------------------------------------------------------------------------
function Disabled(nAction)
//------------------------------------------------------------------------------------------
{	
	with (self.document.forms[0]) {
		if (nAction == 1)
			elements["cboNullcode"].disabled=true;
		if (elements["cboReturn_ind"].value != 4 && elements["cboReturn_ind"].value != 0) 
		{
			elements["tcnReturn_rat"].value = "0";
			elements["tcnReturn_rat"].disabled=true;
		}
		if (elements["cboReturn_ind"].value == 4)
		    elements["tcnReturn_rat"].disabled = false;

		if (elements["cboReturn_ind"].value == 9)
		    elements["tctRoutine_Pay"].disabled = false;
		else {
		    elements["tctRoutine_Pay"].disabled = true;
		    elements["tctRoutine_Pay"].value = "";
		}

	}
}

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"

</SCRIPT>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP061", "DP061.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP061" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%
Response.Write(mobjValues.ShowWindowsName("DP061"))

Call insDefineheader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP061Upd()
	If Request.QueryString.Item("Action") <> "Del" Then%> <SCRIPT>EnablechkReaAut(self.document.forms[0].elements["chkNotRehab"].checked==true);</SCRIPT><%	
    End If
Else
	Call insPreDP061()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




