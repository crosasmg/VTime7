<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
    
    Dim mobjValues As New eFunctions.Values
    Dim mobjGrid As New eFunctions.Grid
    Dim mobjMenues As New eFunctions.Menues
    Dim lclsDocument_Pay As New eClaim.Document_Pay
    Dim lcolDocument_Pays As New eClaim.Document_Pays


'+ insDefineHeader: Definición del Grid de consulta de documentos asignados
'-------------------------------------------------------------------------------------------
 Sub insDefineHeader()
	
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "NC002"
	
	With mobjGrid
		With .Columns
			'	Call .AddCheckColumn(0,"Anular","chkDelete","")
                Call .AddClientColumn(0, "Proveedor", "cbeClient_Provider", "")
			Call .AddPossiblesColumn(0, "Tipo de documento", "cbeTypesupport", "table5570", 1)
			Call .AddNumericColumn(0, "Numero", "tcnN_Document", 10, 0,  , "Número de inicio")
			Call .AddPossiblesColumn(0, "Estado", "cbeStatus", "Table334", 1)
			Call .AddDateColumn(0, "Fecha estado", "tcdDate_Status")
			Call .AddNumericColumn(0, "Monto", "tcnMount_Document", 10, 0,  , "Número de inicio", 1)
			Call .AddPossiblesColumn(0, "Moneda", "cbeCurrency", "Table11", 1)
			Call .AddDateColumn(0, "Fecha", "tcdDate_Document")
			Call .AddHiddenColumn("sParam", vbNullString)
		End With
		.AddButton = False
		.DeleteButton = True
		.Codispl = "NC002"
		.Width = 650
		.Height = 300
		.ActionQuery = mobjValues.ActionQuery
		
		'.Columns("cbeBranch").EditRecord = True
		.nMainAction = Request.QueryString.Item("nMainAction")
		.Columns("Sel").GridVisible = True
		'.WidthDelete = 500
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
	
	
End Sub

'%inspreNC002: Se Actualiza el registro seleccionado en el Grid
'-------------------------------------------------------------------------------------------
'--------------------------------------------------------------------------------------------
  PRIVATE  Sub inspreNC002()
        Dim res As Boolean
	
        res = lcolDocument_Pays.Find(mobjValues.StringToType(Request.QueryString.Item("nTypesupport"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nDocument"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nStatus"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToDate(Request.QueryString.Item("dDate_dStatus1")), mobjValues.StringToDate(Request.QueryString.Item("dDate_dStatus2")), mobjValues.StringToType(Request.QueryString.Item("nCodeuser"), eFunctions.Values.eTypeData.etdLong))
	
        With mobjGrid
            If res Then
                For Each lclsDocument_Pay In lcolDocument_Pays
				    
                    .Columns("Sel").Disabled = True
				
                    If lclsDocument_Pay.nStatus = 2 Then
                        .Columns("Sel").Disabled = False
                    End If
				
                    .Columns("cbeClient_Provider").DefValue = lclsDocument_Pay.sClient
                    .Columns("cbeTypesupport").DefValue = lclsDocument_Pay.nTypesupport
                    .Columns("tcnN_Document").DefValue = lclsDocument_Pay.nDocument
                    .Columns("cbeStatus").DefValue = lclsDocument_Pay.nStatus
                    .Columns("tcdDate_Status").DefValue = lclsDocument_Pay.dStatdate
                    .Columns("tcnMount_Document").DefValue = lclsDocument_Pay.nAmount
                    .Columns("cbeCurrency").DefValue = lclsDocument_Pay.nCurrency
                    .Columns("tcdDate_Document").DefValue = lclsDocument_Pay.dDocument
				
                    'Parametros para anular registros'        
				
                    .Columns("sParam").DefValue = "nTypesupport=" & lclsDocument_Pay.nTypesupport & "&sClient=" & lclsDocument_Pay.sClient & "&nProvider=" & lclsDocument_Pay.nProvider & "&nDocument=" & lclsDocument_Pay.nDocument
				
                    Response.Write(.DoRow)
                Next lclsDocument_Pay
			
            End If
        End With
        Response.Write(mobjGrid.closeTable())
	
        'Set lclsDocument_Pay = Nothing  
        'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjGrid = Nothing
        Response.Write(mobjValues.BeginPageButton)
	
    End Sub

'inspreNC002_upd: Se Actualiza el registro seleccionado en el Grid
'--------------------------------------------------------------------------------------------
Private Sub inspreNC002_upd()
	'--------------------------------------------------------------------------------------------
	      
      Dim   lclsDoc_Pay As New eClaim.Document_Pay
	
	With mobjValues
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsDoc_Pay.insPostNC002(1, .StringToType(Request.QueryString.Item("nTypesupport"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("sClient"), .StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdLong), .StringToType(Request.QueryString.Item("nDocument"), eFunctions.Values.eTypeData.etdDouble))
			
		End If
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valNC002tra.aspx", "NC002", Request.QueryString.Item("nMainAction"), .ActionQuery, Request.QueryString.Item("Index")))
	End With
	lclsDoc_Pay = Nothing
End Sub

</script>
<%Response.Expires = -1441

%>

<HTML>
<HEAD>


	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


	<%


 Dim lcolDocument_Pays As New eClaim.Document_Pays

Response.Write(mobjValues.ShowWindowsName("NC002"))

mobjValues.sCodisplPage = "NC002"

Response.Write(mobjValues.StyleSheet())

'UPGRADE_NOTE: The 'eFunctions.Menues' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
'mobjMenues = Server.CreateObject("eFunctions.Menues")
 Dim mobjMenues As New eFunctions.Menues
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "NC002", "NC002.aspx"))
End If
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

'UPGRADE_NOTE: Object mobjMenues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenues = Nothing

%>

</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="NC002" ACTION="valNC002Tra.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%
Response.Write(mobjValues.HiddenControl("hddReport", Request.QueryString.Item("chkReport")))
Response.Write(mobjValues.HiddenControl("hddnTypesupport", Request.QueryString.Item("nTypesupport")))
Response.Write(mobjValues.HiddenControl("hddsClient", Request.QueryString.Item("sClient")))
Response.Write(mobjValues.HiddenControl("hddnDocument", Request.QueryString.Item("nDocument")))
Response.Write(mobjValues.HiddenControl("hddnStatus", Request.QueryString.Item("nStatus")))
Response.Write(mobjValues.HiddenControl("hdddDate_dStatus1", Request.QueryString.Item("dDate_dStatus1")))
Response.Write(mobjValues.HiddenControl("hdddDate_dStatus2", Request.QueryString.Item("dDate_dStatus2")))
Response.Write(mobjValues.HiddenControl("hddnCodeuser", Request.QueryString.Item("nCodeuser")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreNC002()
Else
	inspreNC002_upd()
End If

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing

%>
	</FORM>
</BODY>
</HTML>






