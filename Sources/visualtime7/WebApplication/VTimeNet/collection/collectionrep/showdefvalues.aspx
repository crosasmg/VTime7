<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values


'% insShowData: Se obtiene y muestra la última fecha de ejecución del proceso de "Preparación de ctas. ctes. de Cobradores"
'--------------------------------------------------------------------------------------------------------------------------
Private Sub insShowData()
	'--------------------------------------------------------------------------------------------------------------------------
	Dim lobjCtrol_Date As eGeneral.Ctrol_date
	Dim lobjValues As eFunctions.Values
	
	With Server
		lobjCtrol_Date = New eGeneral.Ctrol_date
		lobjValues = New eFunctions.Values
		
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
		lobjValues.sSessionID = Session.SessionID
		lobjValues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjValues.sCodisplPage = "showdefvalues"
	End With
	
	With lobjCtrol_Date
		If .Find(lobjValues.stringToType(Request.QueryString.Item("nType_Process"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdInitDate.value = '" & lobjValues.DateToString(.dEffecdate) & "';")
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdInitDate.value = '" & " " & "';")
		End If
	End With
	
	lobjCtrol_Date = Nothing
	lobjValues = Nothing
End Sub

'% insUpdSelCOL502: Se encarga de actualizar el campo sel de la transacción COL502.
'-----------------------------------------------------------------------------------------------------------------------------------
Private Sub insUpdSelCOL502()
	'-----------------------------------------------------------------------------------------------------------------------------------
	Dim lclsCollectionRep As eCollection.CollectionRep
	Dim lobjValues As eFunctions.Values
	
	lobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
	lobjValues.sSessionID = Session.SessionID
	lobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lobjValues.sCodisplPage = "showdefvalues"
	lclsCollectionRep = New eCollection.CollectionRep
	'+ Se actualiza el campo ncommission de la tabla temporal TMP_COL502.
	If lclsCollectionRep.insPostTCOL502Upd("Limpiar", lobjValues.stringToType(Request.QueryString.Item("nId_Register"), eFunctions.Values.eTypeData.etdDouble), lobjValues.stringToType(Request.QueryString.Item("nCommission"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.fraFolder.document.location=top.fraFolder.document.location;")
	End If
	
	lclsCollectionRep = Nothing
	lobjValues = Nothing
End Sub

'% insShowReports: Se encarga de mostrar los reportes de los procesos pendientes de cobranzas.
'-----------------------------------------------------------------------------------------------------------------------------------
Private Sub insShowReports()
	'-----------------------------------------------------------------------------------------------------------------------------------
	'    Dim lstrCadena 
	'    lstrCadena = Request.QueryString(SReports)
	'    Response.Write = lstrCadena
	'Response.Write "ShowPopUp('/VTimeNet/Common/Reports/Report.aspx?URL=/Collection/CollectionRep/Reports/COL636.rpt&ServerName=&DataBase=&Server=0','COL636',660,330, '', '',70,150);"
	'Response.Write "setTimeout("ShowPopUp('/VTimeNet/Common/Reports/Report.aspx?URL=/Collection/CollectionRep/Reports/COL500E.rpt&ServerName=&DataBase=&Server=0&p=&p=1&sp=t_200820031610271178&sp=20030304&sp=1&sp=2&sp=20030805','COL500E',660,330, '', '',70,150)",1000);"
	Dim lcolJobs_Pends As Object
	Dim lclsJobs_Pend As Object
	
	lcolJobs_Pends = Server.CreateObject("eJobs.Jobs_Pends")
	
	If lcolJobs_Pends.Find(Session("nUserCode")) Then
		For	Each lclsJobs_Pend In lcolJobs_Pends
			'Response.Write "setTimeout(""" & lclsJobs_Pend.sReport  & """,50);"
			Response.Write(lclsJobs_Pend.sReport & ";")
		Next lclsJobs_Pend
	End If
	lcolJobs_Pends = Nothing
	
End Sub


'% insShowSucAgen: Asigna sucursal y agencia perteneciente a un intermediario
'--------------------------------------------------------------------------------------------------------------------------
Private Sub insShowSucAgen()
	'--------------------------------------------------------------------------------------------------------------------------
	Dim lobjIntermedia As eAgent.Intermedia
	Dim lobjValues As eFunctions.Values
	
	lobjValues = New eFunctions.Values
	lobjIntermedia = New eAgent.Intermedia
	
	If lobjIntermedia.Find(lobjValues.stringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value = '" & lobjIntermedia.nAgency & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeZone.value = '" & lobjIntermedia.nOffice & "';")
	End If
	
	lobjIntermedia = Nothing
	lobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 3 $|$$Date: 28/11/03 17:45 $|$$Author: Nvaplat56 $"
</SCRIPT>	
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>

<%Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "Type_Process"
		Call insShowData()
	Case "UpdSelCOL502"
		Call insUpdSelCOL502()
	Case "Reports"
		Call insShowReports()
	Case "SucAgen"
		Call insShowSucAgen()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%>




<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("showdefvalues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




