<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values


'% insValTranProduct: Se valida la existencia de algunas transacciones para un ramo-producto
'%				    Se utiliza para los campos Ramo y Producto de la página SIL009_K.aspx
'--------------------------------------------------------------------------------------------
Private Sub insValTranProduct()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.ValClaimRep
	Dim lclsValues As eFunctions.Values
	
	lclsValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
	lclsValues.sSessionID = Session.SessionID
	lclsValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lclsValues.sCodisplPage = "showdefvalues"
	lclsClaim = New eClaim.ValClaimRep
	
	With lclsClaim
		'Response.Write"alert('" & lclsValues.StringToType(Request.QueryString("nProduct"),eFunctions.Values.eTypeData.etdDouble) & "');"
		If .insValTranSIL009(lclsValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If .nIndBDraft = 1 Then
				Response.Write("top.fraHeader.document.forms[0].tcnIndic.value = '1';")
				Response.Write("top.fraHeader.document.forms[0].cbeB_draft.disabled=false ;")
				Response.Write("top.fraHeader.document.forms[0].chkBDraft.disabled=false ;")
			Else
				
				Response.Write("top.fraHeader.document.forms[0].tcnIndic.value = '0';")
				Response.Write("top.fraHeader.document.forms[0].cbeB_draft.disabled=true ;")
				Response.Write("top.fraHeader.document.forms[0].cbeB_draft.value=0 ;")
				Response.Write("top.fraHeader.document.forms[0].chkBDraft.disabled=true ;")
			End If
		End If
	End With
	
	'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsValues = Nothing
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

'% PoliType:Se busca el tipo de la poliza.
'--------------------------------------------------------------------------------------------
Private Sub PoliType()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	
	With lclsPolicy
		If .Find(Request.QueryString("sCertype"), Request.QueryString("nBranch"), Request.QueryString("nProduct"), Request.QueryString("nPolicy"), True) Then
			If .sPolitype = "1" Then
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='0';")
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='0';")
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=false;")
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.focus();")
			End If
		Else
			Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='';")
			Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
		End If
	End With
	'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsPolicy = Nothing
End Sub

'% UpdateCase:Actualiza el combo de los Casos
'--------------------------------------------------------------------------------------------
Private Sub UpdateCase()
	'--------------------------------------------------------------------------------------------
	
	Dim lobjTables As eFunctions.Tables
	lobjTables = New eFunctions.Tables
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
	lobjTables.sSessionID = Session.SessionID
	lobjTables.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	lobjTables.Parameters.Add("nClaim", Request.QueryString("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If lobjTables.reaTable("tabClaim_cases") Then
		Response.Write("top.fraHeader.document.forms[0].cbeCase.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].cbeCase.options.length=0;")
		If Not Request.QueryString("nPage") = "SIL762" Then
			Response.Write("var option = new Option('','0');")
			Response.Write("top.fraHeader.document.forms[0].cbeCase.options.add(option,0);")
		End If
		Do While Not lobjTables.EOF
			Response.Write("var option = new Option('" & lobjTables.Fields("sDescript") & "','" & lobjTables.Fields("sKey") & "');")
			Response.Write("top.fraHeader.document.forms[0].cbeCase.options.add(option," & lobjTables.Fields("sKey") & ");")
			lobjTables.NextRecord()
		Loop 
		'+ Se asigna el caso cuando es la primera vez       
		If Request.QueryString("nPage") = "SIL762" Then
			Response.Write("if(top.fraHeader.document.forms[0].cbeCase.value!=''){ ")
			Response.Write("var tcnCaseNum = top.fraHeader.document.forms[0].cbeCase.value;")
			Response.Write("var tcnCaseNum = tcnCaseNum.indexOf('/');")
			Response.Write("top.fraHeader.document.forms[0].tcnCaseNum.value = tcnCaseNum;")
			Response.Write("}")
		End If
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeCase.options.length=0;")
		Response.Write("top.fraHeader.document.forms[0].cbeCase.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].tcnCaseNum.value=-32768;")
		Response.Write("top.fraHeader.document.forms[0].tcnDeman_Type.value=-32768;")
	End If
	'UPGRADE_NOTE: Object lobjTables may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjTables = Nothing
End Sub

'% ValBrancht:Rescata el ramo tecnico
'--------------------------------------------------------------------------------------------
Public Sub ValBrancht()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	lclsProduct = New eProduct.Product
	
	'+ Se obtiene el ramo técnico
	Call lclsProduct.insValProdMaster(Request.QueryString("nBranch"), Request.QueryString("nProduct"))
	If CStr(lclsProduct.sBrancht) = "3" Then
		Response.Write("top.fraHeader.document.forms[0].cbeOrderType.disabled=false;")
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeOrderType.disabled=true;")
	End If
	'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsProduct = Nothing
End Sub

'% insFindDataClaim: Encuentra Poliza, Ramo , Producto del siniestro. 
'-------------------------------------------------------------------------------------------- 
Private Sub insFindDataClaim()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	lclsClaim = New eClaim.Claim
	
	
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value='" & lclsClaim.nPolicy & "';")
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.disabled='" & False & "';")
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.value='" & lclsClaim.nBranch & "';")
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.disabled='" & False & "';")
		Response.Write("top.fraHeader.document.forms[0].valProduct.value='" & lclsClaim.nProduct & "';")
		
		
		Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='" & lclsClaim.nCertif & "';")
		Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled='" & False & "';")
		
		
	Else
		Response.Write("alert('Adv. Siniestro no esta registrado');")
	End If
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"

%>
<HTML>
<HEAD>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")

Select Case Request.QueryString("Field")
	Case "ChgProduct"
		Call insValTranProduct()
	Case "SIL005", "SIL762"
		If Request.QueryString("nPolicy") <> eRemoteDB.Constants.intNull Then
			Call PoliType()
		End If
	Case "ValBrancht"
		Call ValBrancht()
	Case "UpdateCase"
		Call UpdateCase()
	Case "ShowClaim"
		Call insFindDataClaim()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString("sFrameCaller")))
Response.Write("</SCRIPT>")

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
				
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage("showdefvalues")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




