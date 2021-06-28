<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">
Dim mclsValues As eFunctions.Values



'% insShowMasterSheet: se muestran los datos asociados al nSheet de tabla MasterSheet (PK)
'--------------------------------------------------------------------------------------------
Sub insShowMasterSheet()
	'--------------------------------------------------------------------------------------------
	Dim lclsMasterSheet As eInterface.MasterSheet
	
	lclsMasterSheet = New eInterface.MasterSheet
	With lclsMasterSheet
		
		If .Find(mclsValues.StringToType(Request.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			
			Response.Write("top.frames['fraHeader'].UpdateDiv('tcnsheetDesc','" & .sDescript & "','Normal');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcsdescript.value='" & .sDescript & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcsshortdesc.value='" & .sShortDesc & "';")
			
			If .nIntertype = 1 Then
				Response.Write("top.frames['fraHeader'].document.forms[0].optnintertype[0].checked=true;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].optnintertype[1].checked=true;")
			End If
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeOpertype.value='" & mclsValues.TypeToString(.nOpertype, eFunctions.Values.eTypeData.etdDouble) & "';")
			
			Response.Write("top.frames['fraHeader'].document.forms[0].tcsprocess.value='" & .sProcess & "';")
			
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeFormat.value='" & mclsValues.TypeToString(.nFormat, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbnsystem.value='" & mclsValues.TypeToString(.nSystem, eFunctions.Values.eTypeData.etdDouble) & "';")
			If .sAutomatic = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].chksautomatic.checked=true;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].chksautomatic.checked=false;")
			End If
			Response.Write("top.frames['fraHeader'].InsChangeField2(top.frames['fraHeader'].document.forms[0].chksautomatic);")
			If .sOnline = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].chksonline.checked=true;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].chksonline.checked=false;")
			End If
			
			If .sGroupby = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].chksgroupby.checked=true;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].chksgroupby.checked=false;")
			End If
			Response.Write("top.frames['fraHeader'].document.forms[0].cbePeriod.value='" & mclsValues.TypeToString(.nPeriod, eFunctions.Values.eTypeData.etdDouble) & "';")
			
		Else
			Response.Write("top.frames['fraHeader'].UpdateDiv('tcnsheetDesc','','Normal');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcsdescript.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcsshortdesc.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].optnintertype.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeOpertype.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcsprocess.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeFormat.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbnsystem.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].chksautomatic.checked=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].chksonline.checked=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].chksgroupby.checked=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbePeriod.value='';")
		End If
	End With
	
	lclsMasterSheet = Nothing
End Sub

</script>
<%Response.Expires = -1441
mclsValues = New eFunctions.Values
mclsValues.sCodisplPage = "showdefvalues"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
    //+ Variable para el control de versiones
        document.VssVersion="$$Revision: 8 $|$$Date: 24/05/04 1:53p $|$$Author: Pvillegas $"  
</SCRIPT>	
</HEAD>
<BODY>
    <FORM NAME="ShowValues">
    </FORM>
</BODY>
</HTML>
<%
Response.Write(mclsValues.StyleSheet() & vbCrLf)
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "MasterSheet"
		Call insShowMasterSheet()
		
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing

%>




