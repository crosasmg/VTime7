<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% insShowCPL002: Asigna valores a parámetros de la página CPL002.
'--------------------------------------------------------------------------------------
Sub insShowCPL002()
	'--------------------------------------------------------------------------------------
	
	Dim lclsLed_compan As eLedge.Led_compan
	lclsLed_compan = New eLedge.Led_compan
	
	If lclsLed_compan.Find(mobjValues.StringToType(Request.QueryString.Item("nLedcompan"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdClosedate.value='" & mobjValues.DateToString(lclsLed_compan.dDate_end) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].elements[""tcdDate""].value='" & mobjValues.DateToString(lclsLed_compan.dDate_end) & "';")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdClosedate.value='" & " " & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].elements[""tcdDate""].value='" & " " & "';")
	End If
	
End Sub


'% insShowLastDate_Process: Obtiene la última fecha de ejecución del proceso según el área contable
'--------------------------------------------------------------------------------------------------
Sub insShowLastDate_Process()
	'--------------------------------------------------------------------------------------------------
	
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	lclsCtrol_date = New eGeneral.Ctrol_date
	
        Select Case mobjValues.StringToType(Request.QueryString.Item("nArea_Led"), eFunctions.Values.eTypeData.etdDouble)
		
            '**+ Automatic premium entries.
            '+   Asientos automáticos de "Primas".
		
            Case 1
                Call lclsCtrol_date.Find(1)
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=true;")
			
			
                '**+ Automatic claim entries.
                '+   Asientos automáticos de "Siniestros".
			
            Case 2
                Call lclsCtrol_date.Find(2)
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=true;")
			
			
                '**+ Automatic current account entries.
                '+   Asientos automáticos de "Cuentas corrientes".
			
            Case 3
                Call lclsCtrol_date.Find(3)
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=true;")
                
                '**+ Co-Reinsuran
                '+   Co-Reaseguro
			
            Case 4
                Call lclsCtrol_date.Find(4)
			
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=true;")

                '**+ Automatic Cash entries (premiums)      
                '+   Asientos automáticos de "Caja ingreso".
			
            Case 5
                Call lclsCtrol_date.Find(5)
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=false;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=false;")
			
                '**+ Automatic Cash expend entries.
                '+   Asientos automáticos de "Caja egreso".
			
            Case 6
                Call lclsCtrol_date.Find(6)
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=true;")
			
                '**+ Automatic current account entries - APV.
                '+   Asientos automáticos de "Cuentas corrientes" - APV.
			
            Case 40
                Call lclsCtrol_date.Find(71)
			
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.value='" & mobjValues.DateToString(lclsCtrol_date.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdInit_date.disabled=true;")
                Response.Write("top.fraHeader.document.forms[0].btn_tcdInit_date.disabled=true;")
                
        End Select
	
	lclsCtrol_date = Nothing
	
End Sub

</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
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
	Case "CPL002"
		Call insShowCPL002()
	Case "CPL999"
		Call insShowLastDate_Process()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>




