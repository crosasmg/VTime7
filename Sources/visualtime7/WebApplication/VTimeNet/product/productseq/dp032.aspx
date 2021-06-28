<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "DP032"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(41331, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", 4, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnModulecColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(41332, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(41333, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString,  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCondSVSColumnCaption"), "tctCondSVS", 30, vbNullString,  , GetLocalResourceObject("tctCondSVSColumnToolTip"))
		Call .AddCheckColumn(41334, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString,  ,  , "inschangeValues(""Required"",this)",  , GetLocalResourceObject("chkRequireColumnToolTip"))
		Call .AddCheckColumn(41335, GetLocalResourceObject("chkDefaultiColumnCaption"), "chkDefaulti", vbNullString,  ,  ,  ,  , GetLocalResourceObject("chkDefaultiColumnToolTip"))
		Call .AddCheckColumn(41336, GetLocalResourceObject("chkChanalloColumnCaption"), "chkChanallo", vbNullString,  ,  ,  ,  , GetLocalResourceObject("chkChanalloColumnToolTip"))
		
		Call .AddCheckColumn(0, GetLocalResourceObject("chkStyp_ratColumnCaption"), "chkStyp_rat", vbNullString,  ,  , "inscheck(1);",  , GetLocalResourceObject("chkStyp_ratColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiratColumnCaption"), "tcnPremirat", 9, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiratColumnToolTip"),  , 6,  ,  ,  , False)
		Call .AddCheckColumn(0, GetLocalResourceObject("chknRateaddColumnCaption"), "chknRateadd", vbNullString,  ,  , "inscheck(2);", True, GetLocalResourceObject("chknRateaddColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatepreaddColumnCaption"), "tcnRatepreadd", 6, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnRatepreaddColumnToolTip"),  , 2,  ,  ,  , True)
		Call .AddCheckColumn(0, GetLocalResourceObject("chknRatesubColumnCaption"), "chknRatesub", vbNullString,  ,  , "inscheck(3);", True, GetLocalResourceObject("chknRatesubColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatepresubColumnCaption"), "tcnRatepresub", 6, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnRatepresubColumnToolTip"),  , 2,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnchprelevColumnCaption"), "tcnchprelev", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnchprelevColumnToolTip"),  , 0,  ,  ,  , False)
		Call .AddCheckColumn(41337, GetLocalResourceObject("chksVigenColumnCaption"), "chksVigen", vbNullString,  ,  ,  , True, GetLocalResourceObject("chksVigenColumnToolTip"))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP032"
		.Width = 600
		.Height = 500
		.bOnlyForQuery = Session("bQuery")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("tctDescript").EditRecord = True
		.Columns("chkRequire").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		.Columns("chkDefaulti").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		.Columns("chkChanallo").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		.Columns("chkStyp_rat").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		.Columns("chknRateadd").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		.Columns("chknRatesub").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		.Columns("chksVigen").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		
	End With
End Sub

'% insPreDP032: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP032()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_Modul As eProduct.Tab_modul
	Dim lcolTab_moduls As eProduct.Tab_moduls
	Dim lintIndex As Short
	Dim mcolGen_covers As eProduct.Gen_covers
	Dim lclsErrors As eFunctions.Errors
	
	mcolGen_covers = New eProduct.Gen_covers
	
	If mcolGen_covers.insPreDP033(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), 0) Then
		lclsErrors = New eFunctions.Errors
		Response.Write(lclsErrors.ErrorMessage("DP032", 55935,  ,  ,  , True))
		mobjGrid.ActionQuery = True
		lclsErrors = Nothing
		
	End If
	
	lclsTab_Modul = New eProduct.Tab_modul
	lcolTab_moduls = New eProduct.Tab_moduls
	
	If lcolTab_moduls.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
		lintIndex = 0
		For	Each lclsTab_Modul In lcolTab_moduls
			With mobjGrid
				.Columns("tcnModulec").DefValue = CStr(lclsTab_Modul.nModulec)
				.Columns("tctDescript").DefValue = lclsTab_Modul.sDescript
				.Columns("tctShort_des").DefValue = lclsTab_Modul.sShort_des
				.Columns("tctCondSVS").DefValue = lclsTab_Modul.sCondSVS
				.Columns("chkRequire").Checked = mobjValues.StringToType(lclsTab_Modul.sRequire, eFunctions.Values.eTypeData.etdDouble)
				.Columns("chkDefaulti").Checked = mobjValues.StringToType(lclsTab_Modul.sDefaulti, eFunctions.Values.eTypeData.etdDouble)
				.Columns("chkChanallo").Checked = mobjValues.StringToType(lclsTab_Modul.sChanallo, eFunctions.Values.eTypeData.etdDouble)
				.sDelRecordParam = "nModulec=' + marrArray[lintIndex].tcnModulec + '&sDescript=' + marrArray[lintIndex].tctDescript + '&sShort_des=' + marrArray[lintIndex].tctShort_des + '&sRequire=' + marrArray[lintIndex].chkRequire + '&sDefaulti=' + marrArray[lintIndex].chkDefaulti + '&sChanallo=' + marrArray[lintIndex].chkChanallo + '&sCondSVS=' + marrArray[lintIndex].tctCondSVS +'"
				.Columns("Sel").OnClick = "inschangeValues(""Sel"", this)"
				.Columns("chkStyp_rat").Checked = mobjValues.StringToType(lclsTab_Modul.styp_rat, eFunctions.Values.eTypeData.etdDouble)
				
				.Columns("tcnPremirat").DefValue = CStr(lclsTab_Modul.npremirat)
				
				If lclsTab_Modul.schangetyp = "4" Or lclsTab_Modul.schangetyp = "2" Then
					.Columns("chknRateadd").Checked = 1
				Else
					.Columns("chknRateadd").Checked = 2
				End If
				
				.Columns("tcnRatepreadd").DefValue = CStr(lclsTab_Modul.nratepreadd)
				
				If lclsTab_Modul.schangetyp = "4" Or lclsTab_Modul.schangetyp = "3" Then
					.Columns("chknRatesub").Checked = 1
				Else
					.Columns("chknRatesub").Checked = 2
				End If
				
				.Columns("tcnRatepresub").DefValue = CStr(lclsTab_Modul.nratepresub)
				
				.Columns("tcnchprelev").DefValue = CStr(lclsTab_Modul.nchprelev)
				
				If lclsTab_Modul.sVigen = "1" Then
					.Columns("chksVigen").Checked = 1
				Else
					.Columns("chksVigen").Checked = 2
				End If
				
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsTab_Modul
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("hddCountRecord", CStr(lcolTab_moduls.Count)))
	
	lclsTab_Modul = Nothing
	lcolTab_moduls = Nothing
End Sub

'% insPreDP032Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP032Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_Modul As eProduct.Tab_modul
	lclsTab_Modul = New eProduct.Tab_modul
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		If lclsTab_Modul.insPostDP032("Delete", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sChanallo"), Request.QueryString.Item("sDefaulti"), Request.QueryString.Item("sRequire"), Request.QueryString.Item("sDescript"), Request.QueryString.Item("sShort_des"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCondSVS"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, vbNullString, vbNullString) Then
			
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End If
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP032", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insDefValuesAdd()</" & "Script>")
		End If
		If Request.QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>inscheck(1);</" & "Script>")
		End If
	End With
	lclsTab_Modul = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "DP032"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP032", "DP032.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 6 $|$$Date: 10/11/04 14:17 $|$$Author: Nvaplat15 $"

//% inscheck: Controla los check habilitados e inhabilitados
//-------------------------------------------------------------------------------------------
function inscheck(Field){
//-------------------------------------------------------------------------------------------    

	with(self.document.forms[0]){
		if (Field==1){
			if (chkStyp_rat.checked){
				chknRateadd.disabled=false;
				chknRatesub.disabled=false;
				tcnPremirat.disabled=false;
				tcnchprelev.disabled=false;
				if (chknRateadd.checked){
					tcnRatepreadd.disabled=false;
				}
				else{
					tcnRatepreadd.value="";
					tcnRatepreadd.disabled=true;
				}
				if (chknRatesub.checked){
					tcnRatepresub.disabled=false;
				}
				else{
					tcnRatepresub.value="";
					tcnRatepresub.disabled=true;
				}
			}
			else{
				tcnPremirat.value="";
				
				chknRateadd.checked   =false	
				tcnRatepreadd.value   ="";
				tcnRatepresub.value   ="";
				chknRatesub.checked   =false;	
				tcnchprelev.value     ="";
				tcnPremirat.disabled  =true;
				tcnRatepreadd.disabled=true;
				tcnRatepresub.disabled=true;
				tcnchprelev.disabled  =true;
				chknRateadd.disabled  =true;
				chknRatesub.disabled  =true;
			}
	    }
		if (Field==2){
			if (chknRateadd.checked){
				tcnRatepreadd.disabled=false;
			}
			else{
				tcnRatepreadd.value="";
				tcnRatepreadd.disabled=true;
			}
	    }
		if (Field==3){
			if (chknRatesub.checked){
				tcnRatepresub.disabled=false;
			}
			else{
				tcnRatepresub.value="";
				tcnRatepresub.disabled=true;
			}
	    }
	}
}

//% insDefValuesAdd: se asignan los valores por defecto a los campos de la página
//-------------------------------------------------------------------------------------------
function insDefValuesAdd(){
//-------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

//+ Se genera el número consecutivo para el campo "Orden de aparición"
	with (top.opener){
		for(var llngIndex = 0;llngIndex<marrArray.length;llngIndex++)
		    if(marrArray[llngIndex].tcnModulec>llngMax)
		        llngMax = marrArray[llngIndex].tcnModulec
	}

//+ Se asignan los valores a los campos de la página	
	with (self.document.forms[0]){
	    if(++llngMax.length > tcnModulec.maxLength)
			tcnModulec.value = "";
		else
			tcnModulec.value = ++llngMax;
	}
}

//% inschangeValues: se controla el manejo de valor de los campos de la página
//-------------------------------------------------------------------------------------------
function inschangeValues(Option, Field){
//-------------------------------------------------------------------------------------------
	switch(Option){
		case "Required":
			if(Field.checked)
				self.document.forms[0].chkDefaulti.checked=true;
			break;
		case "Sel":
			if ('<%=Request.QueryString.Item("WindowType")%>' != 'PopUp')
				if(Field.checked){
					self.document.cmdDelete.disabled = true;
					insDefValues('validateDP032','nIndex=' + Field.value + '&nModulec=' + marrArray[Field.value].tcnModulec)
				}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valProductSeq.aspx?mode=2&sContent=1">
<%=mobjValues.ShowWindowsName("DP032")%>
    <TABLE WIDTH="100%">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP032Upd()
Else
	Call insPreDP032()
End If
%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




