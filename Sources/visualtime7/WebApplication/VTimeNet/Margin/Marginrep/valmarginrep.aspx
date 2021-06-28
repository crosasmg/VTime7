<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjValues As eFunctions.Values

Private mstrErrors As String

'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca.

Dim mstrCommand As String
Dim mobjMarginRep As eMargin.Margin_master


'% insValMargin: Se realizan las validaciones masivas de la forma
'-------------------------------------------------------------------------------------------
Function insValMargin() As String
	'-------------------------------------------------------------------------------------------
	insValMargin = vbNullString
	With Request
		Select Case Request.QueryString.Item("sCodispl")
			
			'+RPT_MGSL001: Margen de Solvencia
			Case "MGSL001"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				
				'+RPT_MGSL002: Margen de Solvencia
			Case "MGSL002"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL002(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				
				'+RPT_MGSL003: Margen de Solvencia
			Case "MGSL003"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL003(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				
				'+RPT_MGSL004: Margen de Solvencia
			Case "MGSL004"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				'+RPT_MGSL006: Cuadros de Margen de Solvencia
			Case "MGSL006"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				
				'+RPT_MGSL007: Listado de Margen de Solvencia Historico y Actualizado
			Case "MGSL007"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL007(mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble))
				
				'+RPT_MGSL008: Listado de Ajuste al Margen de Solvencia
			Case "MGSL008"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				'+RPT_MGSL009: primas y capitales cedidos para el Margen de Solvencia
			Case "MGSL009"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				
			Case "MGSL010"
				mobjMarginRep = New eMargin.Margin_master
				insValMargin = mobjMarginRep.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
				
			Case Else
				insValMargin = "insValMargin: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
		End Select
	End With
End Function

'% insPostMargin: Se realizan las actualizaciones de las ventanas
'-------------------------------------------------------------------------------------------
Function insPostMargin() As Boolean
	'-------------------------------------------------------------------------------------------
	With Request
		Select Case Request.QueryString.Item("sCodispl")
			
			'+RPT_MGSL001:  Margen de Solvencia
			Case "MGSL001"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
				'+RPT_MGSL002:  Margen de Solvencia
			Case "MGSL002"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL002(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
				'+RPT_MGSL003:  Margen de Solvencia
			Case "MGSL003"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL003(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
				
				'+RPT_MGSL004:  Margen de Solvencia
			Case "MGSL004"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL004(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
				'+RPT_MGSL006: Cuadros de Margen de Solvencia
			Case "MGSL006"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL006(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				Session("P_SKEY") = mobjMarginRep.sKey
				
				'+RPT_MGSL007: Se llena la tabla temporal para el Reporte de Margen de Solvencia Historico/Actualizado
			Case "MGSL007"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL007(Session("nInsur_Area"), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTipoInfo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optProcessType"), eFunctions.Values.eTypeData.etdDouble))
				
				Session("P_SKEY") = mobjMarginRep.sKey
				'+RPT_MGSL008: Listado de Ajuste al Margen de Solvencia
			Case "MGSL008"
				insPostMargin = True
				
				'+RPT_MGSL009: primas y capitales cedidos para el Margen de Solvencia
			Case "MGSL009"
				mobjMarginRep = New eMargin.Margin_master
				
				insPostMargin = mobjMarginRep.inspostMGSL009(mobjValues.StringToType(CStr(2), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
			Case "MGSL010"
				mobjMarginRep = New eMargin.Margin_master
				insPostMargin = mobjMarginRep.inspostMGSL010(mobjValues.StringToType(Request.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), "1", 2, mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdLong))
		End Select
	End With
	
	If insPostMargin Then
		insPrintDocuments()
	End If
	
End Function

'%insPrintDocuments : Realiza la ejecución del reporte
'-------------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-------------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	mobjDocuments = New eReports.Report
	
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			'+RPT_MGSL006: Se lee la tabla temporal para el Reporte de Margen de Solvencia Historico/Actualizado
			Case "MGSL006"
				If Session("nInsur_Area") = 2 Then
					
					.sCodispl = "MGSL006"
					.ReportFilename = "REPMGSL006_LIFE.RPT"
					.setStorProcParam(1, Session("P_SKEY"))
					.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnAction"), eFunctions.Values.eTypeData.etdInteger))
					
					Response.Write((.Command))
					
				Else
					.sCodispl = "MGSL006"
					.ReportFilename = "REPMGSL006_GENERAL.RPT"
					.setStorProcParam(1, Session("P_SKEY"))
					.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnAction"), eFunctions.Values.eTypeData.etdInteger))
					
					Response.Write((.Command))
				End If
				'+RPT_MGSL007: Se lee la tabla temporal para el Reporte de Margen de Solvencia Historico/Actualizado
			Case "MGSL007"
				.sCodispl = "MGSL007"
				.ReportFilename = "MGSL007.RPT"
				.setStorProcParam(1, Session("nInsur_Area"))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeTipoInfo"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("optProcessType"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(6, Session("P_SKEY"))
				
				Response.Write((.Command))
				
				'+RPT_MGSL008: Listado de Ajuste al Margen de Solvencia
			Case "MGSL008"
				.sCodispl = "MGSL008"
				.ReportFilename = "RPT_MGSL008.rpt"
				.setStorProcParam(1, .setdate(Request.Form.Item("tcdInitDate")))
				.setStorProcParam(2, .setdate(Request.Form.Item("tcdEndDate")))
				Response.Write((.Command))
		End Select
	End With
	mobjDocuments = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valmarginrep")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valmarginrep"

mstrCommand = "&sModule=Margin&sProject=Margin&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>




	
<SCRIPT>
// Función que retorna a la pagina anterior
//------------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------------
    self.history.go(-1)}

// Función que define la ubicación de la Pagina
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>
</HEAD>
<BODY>

	<%
If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
	
	'+ Si no se han validado los campos de la página
	
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValMargin
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MarginErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMargin Then
		Response.Write(("<SCRIPT>setTimeout('insReloadTop(true,false);',5000);</SCRIPT>"))
	End If
End If

mobjValues = Nothing
mobjMarginRep = Nothing
%>
	</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("valMarginrep")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




