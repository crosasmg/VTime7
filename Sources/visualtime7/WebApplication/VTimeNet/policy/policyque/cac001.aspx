<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20 ***
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As ePolicy.Policy
'~End Body Block VisualTimer Utility
Dim nRow As Integer


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cac001"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 20, vbNullString,  , GetLocalResourceObject("tctClientColumnToolTip"))
		If Request.QueryString.Item("sState") = "1" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusvaColumnCaption"), "cbeStatusva", "table181", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatusvaColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeNwait_codeColumnCaption"), "cbeNwait_code", "tab_waitpo", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeNwait_codeColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeNullcodeColumnCaption"), "cbeNullcode", "table13", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeNullcodeColumnToolTip"))
		End If
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate",  ,  , GetLocalResourceObject("tcdStartdateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat",  ,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
		Call .AddAssociateColumn(0, "Inf.Adicional", "btnquery", 15)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CAC001"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		.Splits_Renamed.AddSplit(0, "", 4)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		.Splits_Renamed.AddSplit(0, "", 3)
		
	End With
End Sub

'% insPreCAC001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsClass As Object
	Dim lintCount As Short
	Dim lintTransaction As Byte
	Dim lclsPolicy_Security As eSecurity.Policy_security
	
	mcolClass = New ePolicy.Policy
	lclsPolicy_Security = New eSecurity.Policy_security
	
	Session("Pol_security") = lclsPolicy_Security.ValPolicySecur(mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	If mcolClass.Find_Certificat_pol(Request.QueryString.Item("sCertype"), Request.QueryString.Item("sState"), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nCurrrent"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCreditnum"), Request.QueryString.Item("sAccnum"), nRow) Then
		
		
Response.Write("" & vbCrLf)
Response.Write("		    <TABLE WIDTH=""100%"" CELLSPACING=""10"">" & vbCrLf)
Response.Write("		        <TR ALIGN=RIGTH>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.CheckControl("chkSel", GetLocalResourceObject("chkSelCaption"),  , "1", "insCheckAll(this);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.HiddenControl("hddsKey", mcolClass.sKey))


Response.Write("</TD>" & vbCrLf)
Response.Write("		        </TR>" & vbCrLf)
Response.Write("		    </TABLE>" & vbCrLf)
Response.Write("		")

		
		
		lintCount = 0
		For	Each lclsClass In mcolClass
			With mobjGrid
				.Columns("tcnCertif").DefValue = lclsClass.nCertif
				.Columns("tctClient").DefValue = lclsClass.sCliename
				If Request.QueryString.Item("sState") = "1" Then
					.Columns("cbeStatusva").DefValue = lclsClass.sStatusva
					.Columns("cbeNwait_code").DefValue = lclsClass.Nwait_code
				Else
					.Columns("cbeNullcode").DefValue = lclsClass.nNullcode
				End If
				.Columns("tcdStartdate").DefValue = lclsClass.dStartdate
				.Columns("tcdExpirdat").DefValue = lclsClass.dExpirdat
				If Session("Pol_security") And Request.QueryString.Item("sCertype") = "2" Then
					.Columns("tcnCapital").DefValue = CStr(0)
					.Columns("tcnPremium").DefValue = CStr(0)
				Else
					.Columns("tcnCapital").DefValue = lclsClass.nCapital
					.Columns("tcnPremium").DefValue = lclsClass.nPremium
				End If
				
				If lclsClass.nCertif = 0 Then
					lintTransaction = 8
				Else
					lintTransaction = 9
				End If
				.Columns("btnquery").sQueryString = "sCertype=" & Request.QueryString.Item("sCertype") & "!nBranch=" & Request.QueryString.Item("nBranch") & "!nProduct=" & Request.QueryString.Item("nProduct") & "!nPolicy=" & Request.QueryString.Item("nPolicy") & "!dStartdate=" & Request.QueryString.Item("dStartdate") & "!LoadWithAction=" & Request.QueryString.Item("nMainAction") & "!sCodisplOrig=CAC001" & "!nTransaction=" & lintTransaction & "!nCertif='+marrArray[" & CStr(lintCount) & "].tcnCertif+'"
				Response.Write(.DoRow)
				lintCount = lintCount + 1
				
			End With
		Next lclsClass
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolClass = Nothing
End Sub

'% insPreCAC001Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC001Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjClass As Object
	
'UPGRADE_NOTE: The 'eDll.Class' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lobjClass = Server.CreateObject("eDll.Class")
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.insPostCAC001() Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyQue.aspx", "CAC001", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjClass = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac001"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 11/05/04 19:20 $|$$Author: Nvaplat7 $"

//% insDisableAll: Permite deshabilitar todos los controles de la ventana.
//--------------------------------------------------------------------------------------------
function insCheckAll(Field){
//--------------------------------------------------------------------------------------------
	var bChecked = Field.checked;
	var lstrURL = "sKey=" + document.forms[0].hddsKey.value
		lstrURL = lstrURL + <%="'&sCertype=" & Request.QueryString.Item("sCertype") & "&sState=" & Request.QueryString.Item("sState") & "&sClient=" & Request.QueryString.Item("sClient") & "'"%>
		lstrURL = lstrURL + <%="'&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "'"%>
		lstrURL = lstrURL + <%="'&nPolicy=" & Request.QueryString.Item("nPolicy") & "&dStartdate=" & Request.QueryString.Item("dStartdate") & "'"%>
		lstrURL = lstrURL + <%="'&nCurrrent=" & Request.QueryString.Item("nCurrrent") & "&sCreditnum=" & Request.QueryString.Item("sCreditnum") & "'"%>
		lstrURL = lstrURL + <%="'&sAccnum=" & Request.QueryString.Item("sAccnum") & "'"%>

	if (bChecked)
		insDefValues("PrintCac001",lstrURL);

}
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
</SCRIPT>


<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CAC001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CAC001" ACTION="ValPolicyQue.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("CAC001", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCAC001Upd()
Else
	Call insPreCAC001()
End If
%>	
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')")%>
<%
mobjGrid = Nothing
mobjValues = Nothing
%>

</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




