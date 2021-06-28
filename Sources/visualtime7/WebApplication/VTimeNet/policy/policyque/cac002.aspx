<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mstrAction As String


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cac002"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddCheckColumn(40621, GetLocalResourceObject("chkRecupColumnCaption"), "chkRecup", "", 0, CStr(1))
		Call .AddHiddenColumn("hddOffice", "")
		Call .AddTextColumn(40611, GetLocalResourceObject("tctDescOfficeInsColumnCaption"), "tctDescOfficeIns", 15, "",  ,  ,  ,  ,  , True)
		Call .AddHiddenColumn("hddIntermed", "")
		Call .AddTextColumn(40612, GetLocalResourceObject("tctIntermediaColumnCaption"), "tctIntermedia", 40, "",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(40613, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  ,  , False)
		Call .AddNumericColumn(40614, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, "",  ,  , False)
		Call .AddPossiblesColumn(100692, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddDateColumn(40620, GetLocalResourceObject("dCompDateColumnCaption"), "dCompDate")
		Call .AddNumericColumn(40615, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 19, "",  ,  , True, 6)
		Call .AddTextColumn(40616, GetLocalResourceObject("tctWait_desColumnCaption"), "tctWait_des", 15, "",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(40617, GetLocalResourceObject("tcnWait_CodeColumnCaption"), "tcnWait_Code", 10, "",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(40618, GetLocalResourceObject("tctClieNameColumnCaption"), "tctClieName", 15, "",  ,  ,  ,  ,  , True)
		Call .AddHiddenColumn("hddBranch", "")
		Call .AddHiddenColumn("hddProduct", "")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CAC002_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 520
		.Width = 400
		.Top = 10
		.Left = 10
		.Columns("tcnWait_Code").GridVisible = False
		.Columns("tctClieName").GridVisible = False
	End With
End Sub

'% insPreCAC002: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC002()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lcolPolicy As ePolicy.Policys
	Dim lintCount As Short
	
	lclsPolicy = New ePolicy.Policy
	lcolPolicy = New ePolicy.Policys
	
	'+ Se ejecuta el select preparado
	If lcolPolicy.Find_CAC002(mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInterm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nOption"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		lintCount = 0
		
		For	Each lclsPolicy In lcolPolicy
			With lclsPolicy
				mobjGrid.Columns("chkRecup").OnClick = "ShowPages(this," & CStr(lintCount) & ")"
				mobjGrid.Columns("hddOffice").DefValue = CStr(.nOffice)
				mobjGrid.Columns("hddIntermed").DefValue = CStr(.nIntermed)
				mobjGrid.Columns("tctIntermedia").DefValue = .sCliename_Inter
				mobjGrid.Columns("cbeCurrency").DefValue = CStr(.nCurrency)
				mobjGrid.Columns("tcnPolicy").DefValue = CStr(.nPolicy)
				mobjGrid.Columns("tcnCertif").DefValue = CStr(.nCertif)
				mobjGrid.Columns("dCompDate").DefValue = CStr(.dCompdate)
				mobjGrid.Columns("tcnCapital").DefValue = CStr(.nCapital)
				mobjGrid.Columns("tctWait_des").DefValue = .swait_des
				mobjGrid.Columns("tcnWait_Code").DefValue = CStr(.nWait_code)
				mobjGrid.Columns("tctClieName").DefValue = .sCliename
				mobjGrid.Columns("tctDescOfficeIns").DefValue = .sDesOfficeIns
				mobjGrid.Columns("hddBranch").DefValue = CStr(.nBranch)
				mobjGrid.Columns("hddProduct").DefValue = CStr(.nProduct)
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsPolicy
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsPolicy = Nothing
	lcolPolicy = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac002")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac002"

mstrAction = Request.QueryString.Item("nMainAction")

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:37 $|$$Author: Nvaplat61 $"
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "CAC002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% ShowPages: Llama a las ventanas de pago de siniestro y/o cualquiera que sea el caso
//-------------------------------------------------------------------------------------------
function ShowPages(sField,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrParam = new String()

	with (self.document.forms[0]){    
		lstrParam = "&sCertype=2" + 
		            "&nBranch=" + marrArray[lintIndex].hddBranch +
		            "&nProduct=" + marrArray[lintIndex].hddProduct +		            		
		            "&nPolicy=" + marrArray[lintIndex].tcnPolicy +
		            "&nCertif=" + marrArray[lintIndex].tcnCertif +
		            "&nTransaction=3" + 
					"&LoadWithAction=" + "<%=mstrAction%>";

		}

	ShowPopUp("/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=PolicySeq&sCodispl=CA001_K&" + lstrParam,"",700,500,true)
		
	if (sField.checked){		
		sField.checked=false;
	} 

}   

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
    EditRecord(-1, nMainAction,'Add')
}
function insShowHeader(){
    var lblnContinue=true
    if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].valIntermed)!='undefined'){
				top.fraHeader.document.forms[0].valOffice.value= '<%=Session("nOffice")%>'
				top.fraHeader.document.forms[0].cbeBranch.value=  '<%=Session("nBranch")%>'
				top.fraHeader.document.forms[0].valProduct.value= '<%=Session("nProduct")%>' 
				top.fraHeader.document.forms[0].valProduct.Parameters.Param1.sValue = '<%=Session("nBranch")%>'
				top.fraHeader.$('#valProduct').change()
				top.fraHeader.document.forms[0].valIntermed.value= '<%=Session("nInterm")%>' 
				top.fraHeader.$('#valIntermed').change()
				if ('<%=Session("nOption")%>' == '1') top.fraHeader.document.forms[0].Option[0].checked = true
				else top.fraHeader.document.forms[0].Option[1].checked = true;
				lblnContinue = false
			}
		}
	}
    if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
insShowHeader();
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQDBVehicle" ACTION="ValPolicyQue.aspx?Zone=1">
<%
Call insDefineHeader()
Call insPreCAC002()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




