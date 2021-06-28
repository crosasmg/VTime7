<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'Dim InsPreCA003AUpd() As Object

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 16, vbNullString)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		.AddTextColumn(0, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 60, vbNullString)
		.AddHiddenColumn("hddsDigit", vbNullString)
		.AddHiddenColumn("hddnRole", vbNullString)
		.AddHiddenColumn("hdddBirthdate", vbNullString)
		.AddHiddenColumn("hddsSexclien", vbNullString)
		.AddHiddenColumn("hddsTyperisk", vbNullString)
		.AddHiddenColumn("hddsSmoking", vbNullString)
		.AddHiddenColumn("hddnRating", vbNullString)
		.AddHiddenColumn("hddnInsuAge", vbNullString)
		.AddHiddenColumn("hddnAge", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "Codispl"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'% InsPreCA003A: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreCA003A()
	'--------------------------------------------------------------------------------------------
	Dim lcolRoles As ePolicy.Roleses
	Dim lclsRoles As Object
	Dim lintIndex As Short
	
	lcolRoles = New ePolicy.Roleses
	If lcolRoles.Find_by_Policy(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), vbNullString, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nTypeList"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sClientRole"), Request.QueryString.Item("sCalAge") = "1") Then
		lintIndex = 0
		For	Each lclsRoles In lcolRoles
			With mobjGrid
				.Columns("tctClient").DefValue = lclsRoles.sClient
				' &  "-"& lclsRoles.sDigit
				.Columns("tctClient").HRefScript = "RecordFound(" & lintIndex & ",'" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("ControlClieName") & "');"
				.Columns("tctClient").HRefScript = .Columns("tctClient").HRefScript & "CloseWindow();"
				.Columns("cbeRole").DefValue = lclsRoles.nRole
				.Columns("tctCliename").DefValue = lclsRoles.sCliename
				.Columns("hddsDigit").DefValue = lclsRoles.sDigit
				.Columns("hddnRole").DefValue = lclsRoles.nRole
				.Columns("hdddBirthdate").DefValue = lclsRoles.dBirthdate
				.Columns("hddsSexclien").DefValue = lclsRoles.sSexclien
				.Columns("hddsTyperisk").DefValue = lclsRoles.nTyperisk
				.Columns("hddsSmoking").DefValue = lclsRoles.sSmoking
				.Columns("hddnRating").DefValue = lclsRoles.nRating
				.Columns("hddnInsuAge").DefValue = lclsRoles.nAge(True)
				.Columns("hddnAge").DefValue = lclsRoles.nAge(False)
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsRoles
	End If
	Response.Write(mobjGrid.closeTable())
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"" BORDER=""0"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=""5%"">")


Response.Write(mobjValues.ButtonAbout("CA003A"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD ALIGN=""RIGHT"">")

	Response.Write(mobjValues.ButtonAcceptCancel( ,  , True,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 4 $|$$Date: 6/04/04 20:01 $|$$Author: Nvaplat37 $"

//%	RecordFound: Retorna el código del cliente seleccionado. 
//------------------------------------------------------------------------------------------- 
function RecordFound(nIndex,ControlName,ControlClieName) { 
//------------------------------------------------------------------------------------------ 
	with(opener.document.forms[0]){
		elements[ControlName].value = marrArray[nIndex].tctClient; 
		elements[ControlName + '_Digit'].value = marrArray[nIndex].hddsDigit;
		elements[ControlName + '_Old'].value = marrArray[nIndex].tctClient; 
		elements[ControlName + '_Digit'].value = marrArray[nIndex].hddsDigit;
		elements[ControlName + '_Digit_Old'].value = marrArray[nIndex].hddsDigit;

		try{
			elements[ControlName + '_Role'].value = marrArray[nIndex].hddnRole;
		}catch(error){}
		try{
			elements[ControlName + '_Sexclien'].value = marrArray[nIndex].hddsSexclien;
		}catch(error){}
		try{
			elements[ControlName + '_Birthdate'].value = marrArray[nIndex].hdddBirthdate;
		}catch(error){}
		try{
			elements[ControlName + '_Typerisk'].value = marrArray[nIndex].hddsTyperisk;
		}catch(error){}
		try{
			elements[ControlName + '_Smoking'].value = marrArray[nIndex].hddsSmoking;
		}catch(error){}
		try{
			elements[ControlName + '_Rating'].value = marrArray[nIndex].hddnRating;
		}catch(error){}
		try{
			elements[ControlName + '_InsuAge'].value = marrArray[nIndex].hddnInsuAge;
		}catch(error){}
		try{
			elements[ControlName + '_nAge'].value = marrArray[nIndex].hddnAge;
		}catch(error){}

		if(ControlClieName!="" && typeof(opener.document.getElementById(ControlClieName))!="undefined"){ 
//		    opener.document.getElementById(ControlClieName).innerHTML = marrArray[nIndex].tctCliename;
            opener.$("#" + ControlClieName).html(marrArray[nIndex].tctCliename);
		}
	}
	<%
If Request.QueryString.Item("sOnChange") <> vbNullString Then
	Response.Write("opener." & Request.QueryString.Item("sOnChange") & ";")
End If
%>
}

//%	CloseWindow: Cierra la ventana
//------------------------------------------------------------------------------------------- 
function CloseWindow(){
//------------------------------------------------------------------------------------------- 
    window.close(); 
}
</SCRIPT>
<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmClientPolicySel" ACTION="CA003A.aspx?ControlName=<%=Request.QueryString.Item("ControlName")%>&ControlClieName=<%=Request.QueryString.Item("ControlClieName")%>">
<%Response.Write(mobjValues.ShowWindowsName("CA003A"))
Call insDefineHeader()

'If Request.QueryString.Item("Type") = "PopUp" Then
'	Call InsPreCA003AUpd()
'Else
	Call InsPreCA003A()
'End If
%>
</FORM> 
</BODY>
</HTML>




