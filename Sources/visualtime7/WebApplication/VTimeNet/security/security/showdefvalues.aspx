<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% FindWindowsPseudo: Si se colocó el campo sCodispl se realiza la consulta para mostrar
'% el Pseudónimo y el tipo de ventana, caso contrario si la consulta es por Pseudónimo se realiza
'% la consulta para mostrar el código lógico y el tipo de ventana.
'--------------------------------------------------------------------------------------------
Private Sub FindWindows()
	'--------------------------------------------------------------------------------------------
	Dim lclsWindows As eSecurity.Windows
	
	lclsWindows = New eSecurity.Windows
	
	If lclsWindows.insReaWindowsPseudo1(Request.QueryString.Item("sCodispl"), "") Then
		Response.Write("top.frames['fraHeader'].document.forms[0].valCodispl.value='" & lclsWindows.sCodispl & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctPseudo.value='" & lclsWindows.sPseudo & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWindowty.value=" & lclsWindows.nWindowTy & ";")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].valCodispl.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctPseudo.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWindowty.value='';")
	End If
	
	lclsWindows = Nothing
End Sub

'% FindWindowsPseudo: Si se colocó el campo sCodispl se realiza la consulta para mostrar
'% el Pseudónimo y el tipo de ventana, caso contrario si la consulta es por Pseudónimo se realiza
'% la consulta para mostrar el código lógico y el tipo de ventana.
'--------------------------------------------------------------------------------------------
Private Sub FindPseudo()
	'--------------------------------------------------------------------------------------------
	Dim lclsWindows As eSecurity.Windows
	
	lclsWindows = New eSecurity.Windows
	
	If lclsWindows.insReaWindowsPseudo1("", Request.QueryString.Item("sPseudo")) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].tctPseudo.value='" & lclsWindows.sPseudo & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].valCodispl.value='" & lclsWindows.sCodispl & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWindowty.value=" & lclsWindows.nWindowTy & ";")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tctPseudo.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].valCodispl.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeWindowty.value='';")
	End If
	
	lclsWindows = Nothing
End Sub

'% Update_sStatregt: Acción Finalizar de la secuencia
'%					 Se utiliza cuando se presiona el botón Finalizar de la ventana principal
'%					 correspondiente a la subsecuencia de Esquema de seguridad (SG013_K).
'--------------------------------------------------------------------------------------------
Sub Update_sStatregt()
	'--------------------------------------------------------------------------------------------
	Dim lclsSecur_sche As eSecurity.Secur_sche
	Dim lstrStatus As String
	lclsSecur_sche = New eSecurity.Secur_sche
	
	If CStr(Session("sStatus")) = vbNullString Then
		lstrStatus = "1"
	Else
		lstrStatus = Session("sStatus")
	End If
	
	With lclsSecur_sche
		If .insUpdSecur_scheStatregt(Session("sSche_codeWin"), lstrStatus, mclsValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble)) Then
		End If
	End With
	
	lclsSecur_sche = Nothing
End Sub

'% insShowAgency: Sub para el manejo de la fecha de la agencia
'--------------------------------------------------------------------------------------------
Sub insShowAgency()
	'--------------------------------------------------------------------------------------------
	Dim lclsAgencies As eGeneralForm.Agencies
	Dim lblvalor As Boolean
	Dim lobjValues As eFunctions.Values
	
	lobjValues = New eFunctions.Values
	lclsAgencies = New eGeneralForm.Agencies
	
	lobjValues.Parameters.Add("nOfficeAgen", Request.QueryString.Item("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	lobjValues.Parameters.Add("nAgency", Request.QueryString.Item("nAgency"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
	lblvalor = lclsAgencies.find(Request.QueryString.Item("nAgency"))
	If lclsAgencies.nOfficeagen > 0 Then
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeOffice.value='" & lclsAgencies.nBran_off & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsAgencies.nBran_off & ";")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.Parameters.Param2.sValue =" & lobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble) & ";")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeOfficeAgen.value='" & lclsAgencies.nOfficeagen & "';")
		Response.Write("top.frames['fraFolder'].$('#cbeOfficeAgen').change();")
	End If
	
	lclsAgencies = Nothing
End Sub

</script>
<%Response.Expires = -1
mclsValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17.43 $|$$Author: Nvaplat60 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>

<%
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "sCodispl"
		Call FindWindows()
	Case "sPseudo"
		Call FindPseudo()
	Case "Finish"
		Call Update_sStatregt()
	Case "Agency"
		Call insShowAgency()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing

%>




