<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eApvc" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las mercado generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mobjErrors As eFunctions.Errors

Dim mstrMsgLevel As Object


'**% insDefineHeader: The field of the GRID is defined.
'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'**+ The column of the GRID are defined.
	'+ Se definen las columnas del grid.
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctOriginColumnCaption"), "tctOrigin", 30, vbNullString,  , GetLocalResourceObject("tctOriginColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(100745, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(103509, GetLocalResourceObject("tcnPartic_minColumnCaption"), "tcnPartic_min", 4, CStr(0),  , GetLocalResourceObject("tcnPartic_minColumnToolTip"), True, 2,  ,  ,  , True)
		Call .AddNumericColumn(100744, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 5, CStr(0),  , GetLocalResourceObject("tcnParticipColumnToolTip"),  , 2,  ,  ,  , False)
		Call .AddHiddenColumn("tcnOrigin", CStr(0))
		Call .AddHiddenColumn("tcnFunds", CStr(0))
		Call .AddHiddenColumn("hddPartic_min", CStr(0))
		Call .AddHiddenColumn("hddParticip", CStr(0))
		If CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Then
			Call .AddNumericColumn(100746, GetLocalResourceObject("tcnIntProyColumnCaption"), "tcnIntProy", 5, CStr(0),  , GetLocalResourceObject("tcnIntProyColumnToolTip"),  , 2,  ,  ,  , True)
			Call .AddNumericColumn(100747, GetLocalResourceObject("tcnIntProyVarColumnCaption"), "tcnIntProyVar", 5, CStr(0),  , GetLocalResourceObject("tcnIntProyVarColumnToolTip"),  , 2,  ,  ,  , True)
			Call .AddHiddenColumn("chkActivFound", "1")
		Else
			Call .AddHiddenColumn("tcnIntProy", CStr(0))
			Call .AddHiddenColumn("tcnIntProyVar", CStr(0))
			Call .AddCheckColumn(0, GetLocalResourceObject("chkActivFoundColumnCaption"), "chkActivFound", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkActivFoundColumnToolTip"))
		End If
	End With
	
	'**+ The properties of the GRID are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Height = 300
		.Width = 400
		.Columns("Sel").Title = "Sel"
		.AddButton = False
		.DeleteButton = False
		.bCheckVisible = False
		.sEditRecordParam = "nDisable=' + self.document.forms[0].hddDisable.value + '"
		If mobjValues.ActionQuery <> True Then
			.Columns("tctDescript").EditRecord = True
			.Columns("tctOrigin").EditRecord = True
		Else
			.Columns("Sel").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		If CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Then
			.Splits_Renamed.AddSplit(0, "", 4)
			.Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		End If
		
		.Columns("Sel").OnClick = "insSelected(this)"
		.Columns("chkActivFound").OnClick = "insChangeValues(this)"
	End With
End Sub

'**% insPreVI006A: Read the information of the policy funds.
'% insPreVI006: Obtiene los datos de los fondos de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI006A()
	'--------------------------------------------------------------------------------------------
	Dim lclsFunds As eApvc.Funds
	Dim lcolFundss As eApvc.Fundss
	
	lclsFunds = New eApvc.Funds
	lcolFundss = New eApvc.Fundss
	
	If lcolFundss.Find_FundstoPolMat(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sSche_code"), "VI006A") Then
		
		For	Each lclsFunds In lcolFundss
			With mobjGrid
				.Columns("Sel").checked = lclsFunds.nSelected
				.Columns("tctOrigin").DefValue = lclsFunds.nOrigin & " - " & lclsFunds.sOrigin
				.Columns("tcnOrigin").DefValue = CStr(lclsFunds.nOrigin)
				.Columns("tctDescript").DefValue = lclsFunds.nFunds & " - " & lclsFunds.sDescript
				.Columns("tcnFunds").DefValue = CStr(lclsFunds.nFunds)
				.Columns("tcnPartic_min").DefValue = CStr(lclsFunds.nPartic_min)
				.Columns("tcnParticip").DefValue = CStr(lclsFunds.nParticip)
				.Columns("hddPartic_min").DefValue = CStr(lclsFunds.nPartic_min)
				.Columns("hddParticip").DefValue = CStr(lclsFunds.nParticip)
				.Columns("hddParticip").DefValue = CStr(lclsFunds.nParticip)
				.Columns("tcnIntProy").DefValue = CStr(lclsFunds.nIntProy)
				.Columns("tcnIntProyvar").DefValue = CStr(lclsFunds.nIntProyvarMax)
				
				If lclsFunds.sActivFound = "1" Then
					.Columns("chkActivFound").checked = CShort("1")
				Else
					.Columns("chkActivFound").checked = CShort("2")
				End If
				
				If lclsFunds.nSelected = CDbl("1") Then
					If lclsFunds.sActivFound <> "1" Then
						.Columns("tcnPartic_min").DefValue = ""
						If lclsFunds.nSelected <> 1 Then
							.Columns("tcnParticip").DefValue = ""
						End If
					End If
				Else
					.Columns("chkActivFound").checked = CShort("1")
				End If
				Response.Write(.DoRow)
			End With
		Next lclsFunds
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	If lcolFundss.bUpdateFound Then
		Response.Write(mobjValues.HiddenControl("hddDisable", "2"))
	Else
		Response.Write(mobjValues.HiddenControl("hddDisable", "1"))
	End If
	
	lcolFundss = Nothing
	lclsFunds = Nothing
End Sub

'%** insPreVI006AUpd: Show the pop up windows for the updates.
'% insPreVI006AUpd: Muestra la ventana Popup para las actualizaciones.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI006AUpd()
	'--------------------------------------------------------------------------------------------
	Dim lclsFunds_Co_P As eApvc.Funds_CO_P
	lclsFunds_Co_P = New eApvc.Funds_CO_P
	
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			Call lclsFunds_Co_P.insPostVI006A(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), CInt(.QueryString.Item("nFunds")), CInt(.QueryString.Item("nParticip")), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), vbNullString, vbNullString, CInt(.QueryString.Item("nOrigin")))
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeqapvc.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>self.document.forms[0].tcnParticip.disabled = false;</" & "Script>")
		End If
	End With
	
	
	lclsFunds_Co_P = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	mobjErrors = New eFunctions.Errors
End With

mobjValues.ActionQuery = Session("bQuery")
mstrMsgLevel = mobjErrors.ErrorMessage("VI006A", 56209, 0, 2, "", True, "")


%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




<SCRIPT>    
//**+ For the Source Safe control. 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 13 $|$$Date: 17/06/06 1:35p $|$$Author: MVazquez $"
	
//% insCheckSelClick: Permite levantar la ventana Popup para actualizar el registro.
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
	if(Field.checked) 
		EditRecord(Field.value,nMainAction, 'Update')
    else{ 
        EditRecord(Field.value,nMainAction, 'Del',
                   "nFunds=" + marrArray[Field.value].tcnFunds + 
                   "&nParticip=" + marrArray[Field.value].tcnParticip + 
                   "&nPartic_min=" + marrArray[Field.value].tcnPartic_min +
                   "&nOrigin=" + marrArray[Field.value].tcnOrigin +  
                   "&nIntProy=" + marrArray[Field.value].tcnIntProy + 
                   "&nIntProyVar=" + marrArray[Field.value].tcnIntProyVar)
    }
    Field.checked = !Field.checked
}

//% insChangeValues: Permite actualizar los campos al hacer el check del active found 
//-------------------------------------------------------------------------------------------
function insChangeValues(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(Field.name){
			case "chkActivFound":
				if ((Field.checked==true) || (session(sCertype) == "3")){ 
// si esta desmarcado y se marca 
					chkActivFound.defvalue = "1";
					tcnPartic_min.value=hddPartic_min.value;
					tcnParticip.value=hddParticip.value;
					tcnParticip.disabled=false;
					tcnIntProy.disabled=false;
				}
				else{
// si esta marcado y se desmarca 
					chkActivFound.defvalue = "2";
					tcnPartic_min.value="";
					tcnParticip.value="";
					tcnParticip.disabled=true;																				
				}
				break;
		}
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "VI006A.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI006A" ACTION="ValPolicySeqapvc.aspx?mode=2">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI006A()
Else
	Call insPreVI006AUpd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




