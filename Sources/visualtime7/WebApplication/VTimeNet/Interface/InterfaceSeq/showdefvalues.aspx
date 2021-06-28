<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
Dim mclsValues As eFunctions.Values


'+ insShowFieldSheet: Se muestran los datos asociados al nSheet de tabla T_Interface
'--------------------------------------------------------------------------------------------
Sub insShowFieldSheet()
	
	Dim lclsFieldSheet As eInterface.FieldSheet
	
	lclsFieldSheet = New eInterface.FieldSheet
	With lclsFieldSheet
            'If .Find(Request.QueryString.Item("nSheet")) Then
            '	Response.Write("top.frames['fraHeader'].document.forms[0].tcsdescript.value='" & .sDescript & "';")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].tcsshortdesc.value='" & .sShortDesc & "';")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].valnopertype.value='" & mclsValues.TypeToString(.nOpertype, eFunctions.Values.eTypeData.etdDouble) & "';")
            '	Response.Write("top.frames['fraHeader'].UpdateDiv('valnopertypeDesc','" & .sopertype & "','Normal');")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].tcsprocess.value='" & .sProcess & "';")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].valnformat.value='" & mclsValues.TypeToString(.nFormat, eFunctions.Values.eTypeData.etdDouble) & "';")
            '	Response.Write("top.frames['fraHeader'].UpdateDiv('valnformatDesc','" & .sformat & "','Normal');")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].cbnsystem.value='" & mclsValues.TypeToString(.nSystem, eFunctions.Values.eTypeData.etdDouble) & "';")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].valnperiod.value='" & mclsValues.TypeToString(.nPeriod, eFunctions.Values.eTypeData.etdDouble) & "';")
            '	Response.Write("top.frames['fraHeader'].UpdateDiv('valnperiodDesc','" & .speriod & "','Normal');")
            '	Response.Write("top.frames['fraHeader'].document.forms[0].valsestado.value='" & .sStatussheet & "';")
            '	Response.Write("top.frames['fraHeader'].UpdateDiv('valsestadoDesc','" & .ssstatussheet & "','Normal');")
            'Else
            Response.Write("top.frames['fraHeader'].document.forms[0].tcsdescript.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcsshortdesc.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].optnintertype.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].valnopertype.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcsprocess.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].valnformat.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].cbnsystem.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].chksautomatic.checked='false';")
            Response.Write("top.frames['fraHeader'].document.forms[0].chksgroupby.checked='false';")
            Response.Write("top.frames['fraHeader'].document.forms[0].valnperiod.value='';")
            Response.Write("top.frames['fraHeader'].document.forms[0].valsestado.value='';")
            '+ lLmpio Label de PosiblesValues			
            Response.Write("top.frames['fraHeader'].UpdateDiv('valnopertypeDesc','','Normal');")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valnformatDesc','','Normal');")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valnperiodDesc','','Normal');")
            Response.Write("top.frames['fraHeader'].UpdateDiv('valsestadoDesc','','Normal');")
		'End If
        End With
	lclsFieldSheet = Nothing
End Sub

'+  insUpdStatusBatch_Job: Se actualiza campo Batch_Job.SStatus = 4 cuando cancela la Interfaz
'--------------------------------------------------------------------------------------------
Sub insUpdStatusBatch_Job()
	Dim lclsBatch_Job As eInterface.ValInterfaceSeq
	Dim lobjDocuments As eReports.Report
	
	lclsBatch_Job = New eInterface.ValInterfaceSeq
	lclsBatch_Job.InsUpdBatch_Job(Session("sKey"))
	lclsBatch_Job = Nothing
	
	If CStr(Session("Report")) = "S" And CStr(Session("sError")) = "S" Then
		'+ Ademas cargo reporte con errores si corresponde (solo luego de entrar a la secuencia y cuando hay error)
		lobjDocuments = New eReports.Report
		With lobjDocuments
			.sCodispl = "GI1405"
			.ReportFilename = "GIL1405.rpt"
			.setStorProcParam(1, Session("sKey"))
			
			Response.Write("</" & "Script>")
			Response.Write(.Command)
			Response.Write("<SCRIPT>")
			
		End With
		lobjDocuments = Nothing
	End If
	Response.Write("insReloadTop(false);")
	
End Sub

</script>
<%Response.Expires = -1441
mclsValues = New eFunctions.Values
mclsValues.sCodisplPage = "showdefvalues"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%--

--%>
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
	Case "FieldSheet"
		Call insShowFieldSheet()
		'+ Cambia estado de una interfaz en BATCH_JOB cuando es cancelada en pantalla
	Case "UpdStatus"
		Call insUpdStatusBatch_Job()
		
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing

%>




