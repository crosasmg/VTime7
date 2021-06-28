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
	
	mobjGrid.sCodisplPage = "CAC958"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("sCertypeColumnCaption"), "sCertype", 15, "",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctProductoColumnCaption"), "tctProducto", 40, "",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(40613, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  ,  , False)
		Call .AddNumericColumn(40614, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, "",  ,  , False)
		Call .AddTextColumn(40616, GetLocalResourceObject("tctCausalColumnCaption"), "tctCausal", 15, "",  ,  ,  ,  ,  , True)
		Call .AddDateColumn(40620, GetLocalResourceObject("dEffecdateColumnCaption"), "dEffecdate")
		Call .AddTextColumn(0, GetLocalResourceObject("tctUsuarioColumnCaption"), "tctUsuario", 15, "",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(40620, GetLocalResourceObject("dCompDateColumnCaption"), "dCompDate", 15, "",  ,  ,  ,  ,  , True)
		
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CAC958_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 520
		.Width = 400
		.Top = 10
		.Left = 10
	End With
End Sub

'% insPreCAC958: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC958()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.wait_code_hist
	Dim lcolPolicy As ePolicy.wait_code_hists
	Dim lintCount As Short
	
	lclsPolicy = New ePolicy.wait_code_hist
	lcolPolicy = New ePolicy.wait_code_hists
	
	'+ Se ejecuta el select preparado
	If lcolPolicy.Find(mobjValues.StringToType(Request.QueryString.Item("sCertype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintCount = 0
		
		For	Each lclsPolicy In lcolPolicy
			With lclsPolicy
				mobjGrid.Columns("sCertype").DefValue = .sCertype
				mobjGrid.Columns("tctProducto").DefValue = .sProducto
				mobjGrid.Columns("tcnPolicy").DefValue = CStr(.nPolicy)
				mobjGrid.Columns("tcnCertif").DefValue = CStr(.nCertif)
				mobjGrid.Columns("tctCausal").DefValue = .sCausal
				mobjGrid.Columns("dEffecdate").DefValue = CStr(.dEffecdate)
				mobjGrid.Columns("tctUsuario").DefValue = .sUsuario
				mobjGrid.Columns("dCompdate").DefValue = .sCompdate
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
Call mobjNetFrameWork.BeginPage("CAC958")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAC958"

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
Response.Write(mobjMenu.setZone(2, "CAC958", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

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
Call insPreCAC958()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("CAC958")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




