<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: Object for the handling of the general functions of load of values
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


'[ DEBE COMPLEMENTARSE EL OBJETIVO DE LA SIGUIENTE FUNCION EN ESPAÑOL E INGLES.
'**%Objective: This function shows ....
'%Objetivo: Esta función muestra ...
'--------------------------------------------------------------------------------------------
Private Sub insShowPolicy()
	'--------------------------------------------------------------------------------------------
	Dim lclsOpt_system As eGeneral.Opt_system
	Dim lstrPolicyNum As String
	Dim lblnExist As Boolean
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrCertype As String
	
	lclsOpt_system = New eGeneral.Opt_system
	Call lclsOpt_system.Find()
	lstrPolicyNum = lclsOpt_system.sPolicyNum
	
	
	
	lblnExist = False
	
	If lstrPolicyNum = "1" Then '+Generales
		If (Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nPolicy") <> "0" And Request.QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
			lblnExist = True
		End If
	Else
		If lstrPolicyNum = "2" Then '+ Ramo 
			If (Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nBranch") <> "0" And Request.QueryString.Item("nBranch") <> CStr(eRemoteDB.Constants.intNull) And Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nPolicy") <> "0" And Request.QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
				lblnExist = True
			End If
		Else
			If lstrPolicyNum = "3" Then '+ Producto
				If (Request.QueryString.Item("nBranch") <> vbNullString And Request.QueryString.Item("nBranch") <> "0" And Request.QueryString.Item("nBranch") <> CStr(eRemoteDB.Constants.intNull) And Request.QueryString.Item("nProduct") <> vbNullString And Request.QueryString.Item("nProduct") <> "0" And Request.QueryString.Item("nProduct") <> CStr(eRemoteDB.Constants.intNull) And Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nPolicy") <> "0" And Request.QueryString.Item("nPolicy") <> CStr(eRemoteDB.Constants.intNull)) Then
					lblnExist = True
				End If
			End If
		End If
	End If
	
	If lblnExist Then
		lclsPolicy = New ePolicy.Policy
		
		lstrCertype = Request.QueryString.Item("sCertype")
		If lstrCertype = vbNullString Then
			lstrCertype = "2"
		End If
		
		If lclsPolicy.FindPolicyOptSystem(lstrCertype, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy.nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy.nProduct & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdDate.value='" & Today & "';")
			If lclsPolicy.nProduct <> 0 Then
				Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
				
                    If lstrPolicyNum <> "1" Then
                        If lstrPolicyNum = "2" Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled=false;")
                            Response.Write("top.frames['fraHeader'].document.forms[0].btnvalProduct.disabled=false;")
                        Else
                            Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled=true;")
                            Response.Write("top.frames['fraHeader'].document.forms[0].btnvalProduct.disabled=true;")
                        End If
                    End If
                End If
            End If
            lclsPolicy = Nothing
        End If
	
        lclsOpt_system = Nothing
End Sub

</script>
<%
'----------------------------------------------------------------------------------------------------
'**+Objective: This page allows search of values by default for the fields of the page, also it allows to process the 
'**+           logic to enabled or to disable the fields.
'**+Version: $$Revision: $
'+Objetivo: Esta página permite busqueda de valores por "default" para los campos de la pagina, tambien permite 
'+          procesar la logica para habilitar o deshabilitar los campos.
'+Version: $$Revision: $
'----------------------------------------------------------------------------------------------------
Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage("ShowDefValues")

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "ShowDefValues"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Visual TIME Templates">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//**-Objetive: This line keep the source safe version
//-Objetivo: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 1 $|$$Date: 09/16/03 1:00p|$$Author: nsoler $"
//------------------------------------------------------------------------------------------
</SCRIPT>
</HEAD>
<BODY>
<FORM NAME="ShowValues">
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Policy"
		insShowPolicy()
End Select
Response.Write("setPointer('');")
Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("document.location.href='/VTimeNet/Common/blank.htm';")
Response.Write("</SCRIPT>")
mobjValues = Nothing

mobjNetFrameWork.FinishPage("ShowDefValues")
mobjNetFrameWork = Nothing
%>
</FORM>
</BODY>
</HTML>





