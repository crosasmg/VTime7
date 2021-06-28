<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsProtection As ePolicy.Protection
Dim lcolProtections As ePolicy.Protections



'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkAuxSelColumnCaption"), "chkAuxSel", vbNullString, False)
		End If
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnElementColumnCaption"), "tcnElement", 5, CStr(lclsProtection.nElement),  , GetLocalResourceObject("tcnElementColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, lclsProtection.sDescript,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDisrateColumnCaption"), "tcnDisrate", 4, "", True, GetLocalResourceObject("tcnDisrateColumnToolTip"), True, 2)
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, CStr(1), True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, CStr(1), True,  ,  ,  , "ShowChangeValues(this);",  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		End If
		
		mobjGrid.Columns("cbeCurrency").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeCurrency").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeCurrency").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeCurrency").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeCurrency").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeCurrency").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDiscountColumnCaption"), "tcnDiscount", 18, "",  , GetLocalResourceObject("tcnDiscountColumnToolTip"), True, 6)
		Call .AddHiddenColumn("hddnElement", vbNullString)
		Call .AddHiddenColumn("hddnCurrency", vbNullString)
		Call .AddHiddenColumn("hddnDiscount", vbNullString)
		Call .AddHiddenColumn("hddnDisrate", vbNullString)
		Call .AddHiddenColumn("hddnMaxAmount", vbNullString)
		Call .AddHiddenColumn("hddnMinAmount", vbNullString)
		Call .AddHiddenColumn("hddsAuxSelh", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA012"
		.Width = 380
		.Height = 280
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.bOnlyForQuery = Session("bQuery")
		.Columns("tcnElement").EditRecord = False
		.ActionQuery = Session("bQuery")
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("chkAuxSel").GridVisible = Not .ActionQuery
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA012: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCA012()
	'--------------------------------------------------------------------------------------------
	Dim lblnExist As Boolean
	Dim lintCount As Object
	Dim lintDisrate As Double
	Dim lintDiscount As Double
	Dim lintCurrency As Integer
	Dim lclsGeneral As eGeneral.Exchange
	Dim lclsCurren_pol As ePolicy.Curren_pol
	Dim lintCurrency_aux As Integer
	Dim ldtmDate As Object
	Dim lintValue As Double
	Dim lintCount1 As Integer
	Dim lintExist As Byte
	lblnExist = False
	
	
	ldtmDate = Session("dEffecdate")
	lclsGeneral = New eGeneral.Exchange
	lclsCurren_pol = New ePolicy.Curren_pol
	'+ Se buscan los elementos de protección de la póliza.
	If lcolProtections.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
		
		lintCount = 0
		lblnExist = True
		With mobjGrid
			For	Each lclsProtection In lcolProtections
				.Columns("tcnElement").DefValue = CStr(lclsProtection.nElement)
				.Columns("tctDescript").DefValue = lclsProtection.sDescript
				lintExist = 0
				If lclsProtection.sSelection <> vbNullString Then
					lintDisrate = lclsProtection.PnDisrate
					lintCurrency = lclsProtection.PnCurrency
					lintDiscount = lclsProtection.PnDiscount
					.Columns("hddsAuxSelh").DefValue = "1"
					.Columns("chkAuxSel").Checked = CShort("1")
				Else
					lintDisrate = lclsProtection.nDisRate
					lintCurrency = lclsProtection.nCurrency
					lintDiscount = lclsProtection.nDiscount
					.Columns("hddsAuxSelh").DefValue = "2"
					.Columns("chkAuxSel").Checked = CShort("2")
					If lintCurrency > 0 Then
						If lclsCurren_pol.LoadCurrency(Session("nPolicy"), Session("nBranch"), Session("nProduct"), Session("sCertype"), Session("nCertif"), Session("dEffecdate"), True, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)) Then
							
							For lintCount1 = 0 To lclsCurren_pol.CountCurrenPol
								If lclsCurren_pol.Val_Curren_pol(lintCount1) Then
									lintCurrency_aux = lclsCurren_pol.nCurrency
									If lintCurrency_aux = lintCurrency Then
										lintExist = 1
									End If
								End If
							Next 
						End If
					End If
				End If
				If lintDiscount > 0 And lintExist = 0 Then
					If lintCurrency_aux > 0 Then
						Call lclsGeneral.Convert(eRemoteDB.Constants.intNull, lintDiscount, lintCurrency, lintCurrency_aux, ldtmDate, lintValue, True)
						lintDiscount = lclsGeneral.pdblResult
						lintCurrency = lintCurrency_aux
					End If
				End If
				
				.Columns("tcnDisrate").DefValue = CStr(lintDisrate)
				.Columns("cbeCurrency").DefValue = CStr(lintCurrency)
				.Columns("tcnDiscount").DefValue = CStr(lintDiscount)
				
				.Columns("hddnElement").DefValue = CStr(lclsProtection.nElement)
				.Columns("hddnCurrency").DefValue = CStr(lintCurrency)
				.Columns("hddnDiscount").DefValue = CStr(lintDiscount)
				.Columns("hddnDisrate").DefValue = CStr(lintDisrate)
				.Columns("chkAuxSel").OnClick = "insSelected(this, " & lintCount & ")"
				lintCount = lintCount + 1
				Response.Write(.DoRow)
			Next lclsProtection
		End With
	End If
	
	Response.Write(mobjValues.HiddenControl("hddnCount", lintCount))
	Response.Write(mobjGrid.closeTable())
	
	lclsProtection = Nothing
	lcolProtections = Nothing
	lclsGeneral = Nothing
	lclsCurren_pol = Nothing
End Sub

'% insPreCA012Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreCA012Upd()
	'--------------------------------------------------------------------------------------------
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA012", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	lclsProtection = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA012")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
lclsProtection = New ePolicy.Protection
lcolProtections = New ePolicy.Protections

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 6 $|$$Date: 8/10/04 12:58 $|$$Author: Nvaplat15 $"

//%insSelected: realiza el manejo para la edición de un registro particular del grid 
//%para eliminarlo, agregarlo o modificarlo
//------------------------------------------------------------------------------------------
function insSelected(Field, nIndex){
//------------------------------------------------------------------------------------------

	if (marrArray.length > 1){
		if (Field.checked)
		    self.document.forms[0].hddsAuxSelh[nIndex].value = "1";                              
		else
		    self.document.forms[0].hddsAuxSelh[nIndex].value = "2";
	}
	else
	{
		if (Field.checked)
		    self.document.forms[0].hddsAuxSelh.value = "1";                              
		else
		    self.document.forms[0].hddsAuxSelh.value = "2";
	}
}

//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function ShowChangeValues(field){
//-------------------------------------------------------------------------------------------
	var strParams; 
  		strParams = "nCurrency=" + self.document.forms[0].hddnCurrency.value +  
			        "&nCurrency_ing=" + field.value +  
			        "&nAmount=" + self.document.forms[0].tcnDiscount.value  
		insDefValues("nExchange",strParams,'/VTimeNet/Policy/PolicySeq'); 

}   
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="fraContent" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA012Upd()
Else
	Call insPreCA012()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("CA012")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




