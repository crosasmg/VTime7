<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
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
	
	mobjGrid.sCodisplPage = "bvc001_k"
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddTextColumn(40053, GetLocalResourceObject("tctChassisColumnCaption"), "tctChassis", 40, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctChassisColumnToolTip"),  ,  , "InsChangeValues(this);")
		Call .AddTextColumn(40054, GetLocalResourceObject("tctMotorColumnCaption"), "tctMotor", 40, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctMotorColumnToolTip"),  ,  , "InsChangeValues(this);")
		Call .AddPossiblesColumn(40047, GetLocalResourceObject("cbeLicense_tyColumnCaption"), "cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  , "InsChangeValues(this);",  ,  , GetLocalResourceObject("cbeLicense_tyColumnToolTip"))
		Call .AddTextColumn(40055, GetLocalResourceObject("tctRegistColumnCaption"), "tctRegist", 10, "",  , GetLocalResourceObject("tctRegistColumnToolTip"),  ,  , "InsChangeValues(this);")
		Call .AddClientColumn(40052, GetLocalResourceObject("tcnClientColumnCaption"), "tcnClient", "",  , GetLocalResourceObject("tcnClientColumnToolTip"),  ,  , "tctClieName")
		Call .AddPossiblesColumn(40048, GetLocalResourceObject("cboVehCodeColumnCaption"), "cboVehCode", "tabtab_au_veh", eFunctions.Values.eValuesType.clngComboType, CStr(0), True,  ,  ,  , "InsChangeValues(this);",  ,  , GetLocalResourceObject("cboVehCodeColumnToolTip"))
		Call .AddPossiblesColumn(40049, GetLocalResourceObject("cboDescBrandColumnCaption"), "cboDescBrand", "table7042", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboDescBrandColumnToolTip"))
		Call .AddTextColumn(40056, GetLocalResourceObject("tctVehmodelColumnCaption"), "tctVehmodel", 20, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctVehmodelColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(40057, GetLocalResourceObject("tctColorColumnCaption"), "tctColor", 20, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctColorColumnToolTip"))
		Call .AddNumericColumn(40051, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, "",  , GetLocalResourceObject("tcnYearColumnToolTip"), False, 0)
		Call .AddPossiblesColumn(40050, GetLocalResourceObject("cbeVestatusColumnCaption"), "cbeVestatus", "table220", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeVestatusColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BVC001_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 420
		.Width = 400
		.Top = 10
		.Left = 10
	End With
End Sub

'% insPreBVC001: Se cargan los datos en el grid de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreBVC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As ePolicy.Auto_db
	Dim lcolAuto_dbs As ePolicy.Auto_dbs
	Dim lCountReg As Short
	Dim lclsacc As Object
	
	lclsAuto_db = New ePolicy.Auto_db
	lcolAuto_dbs = New ePolicy.Auto_dbs
	
	'+ Se ejecuta el SELECT preparado
	If Not IsNothing(Request.QueryString.Item("sql")) Then
		If lcolAuto_dbs.FindCondition(Session("Sql")) Then
			lCountReg = 1
			For	Each lclsAuto_db In lcolAuto_dbs
				With lclsAuto_db
					mobjGrid.Columns("tctChassis").DefValue = .sChassis
					mobjGrid.Columns("tctMotor").DefValue = .sMotor
					mobjGrid.Columns("cbeLicense_ty").DefValue = .sLicense_ty
					mobjGrid.Columns("tctRegist").DefValue = .sRegist
					mobjGrid.Columns("tctRegist").DefValue = .sRegist
					mobjGrid.Columns("tcnClient").DefValue = .sVeh_own
					mobjGrid.Columns("cboVehCode").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					mobjGrid.Columns("cboVehCode").DefValue = .sVehCode
					mobjGrid.Columns("tctVehmodel").DefValue = .sVehModel
					mobjGrid.Columns("cboDescBrand").DefValue = .nVehBrand
					mobjGrid.Columns("tctColor").DefValue = .sColor
					mobjGrid.Columns("tcnYear").DefValue = CStr(.nYear)
					mobjGrid.Columns("cbeVestatus").DefValue = CStr(.nVestatus)
					Response.Write(mobjGrid.DoRow())
				End With
				lCountReg = lCountReg + 1
				If lCountReg = 100 Then
					Exit For
				End If
			Next lclsAuto_db
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	
	lclsAuto_db = Nothing
	lcolAuto_dbs = Nothing
End Sub

'% insPreBVC001Upd: Se controla el manejo de la ventana PopUp
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreBVC001Upd()
	'------------------------------------------------------------------------------------------------------------------
	Response.Write("<BR>")
	mobjGrid.Columns("cboVehCode").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValBranchQue.aspx", "BVC001", Request.QueryString.Item("nMainAction"), False, -1))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "bvc001_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%=mobjValues.StyleSheet() & vbCrLf%>

<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
    EditRecord(-1, top.frames['fraSequence'].plngMainAction, 'Add')
}

//% InsChangeValues: Se actualizan los parametros de las listas de valores 
//------------------------------------------------------------------------------------------- 
function InsChangeValues(sField){ 
//------------------------------------------------------------------------------------------- 
	var strParams; 
	with (self.document.forms[0]) {
	    switch(sField.name){
			case "cboVehCode": 
    			strParams = "sVehcode=" + cboVehCode.value 
				insDefValues(sField.name,strParams,'/VTimeNet/Branches/BranchQue'); 
 				break;
			case "tctMotor":
    			strParams = "sMotor=" + tctMotor.value 
				insDefValues(sField.name,strParams,'/VTimeNet/Branches/BranchQue')
				break;
			case "tctChassis":
				strParams = "sChassis=" + self.document.forms[0].tctChassis.value 
				insDefValues(sField.name,strParams,'/VTimeNet/Branches/BranchQue')
				break;
			case "cbeLicense_ty":
			case "tctRegist":
				strParams = "sRegist=" + self.document.forms[0].tctRegist.value 
				insDefValues(sField.name,strParams,'/VTimeNet/Branches/BranchQue')
				break;
 		} 
	}
} 
</SCRIPT>
<%
With Response
	.Write(mobjValues.WindowsTitle("BVC001"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tMenu.js""></SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "BVC001.aspx"))
		.Write(mobjMenu.MakeMenu("BVC001", "BVC001_k.aspx", 2, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmQDBVehicle" ACTION="ValBranchQue.aspx?Zone=1">
<%If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName("BVC001"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreBVC001()
Else
	Call insPreBVC001Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>




