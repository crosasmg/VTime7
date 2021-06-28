<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim mnTypeCompany As Object


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	
	Response.Write(mobjValues.HiddenControl("tcdTypeCompany", mnTypeCompany))
	
        With mobjGrid.Columns
            Call .AddPossiblesColumn(8954, GetLocalResourceObject("cbeTypClientDocColumnCaption"), "cbeTypClientDoc", "TABTYPEDOC", eFunctions.Values.eValuesType.clngComboType, , True, , , , , , 2, GetLocalResourceObject("cbeTypClientDocColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
            mobjGrid.Columns("cbeTypClientDoc").Parameters.Add("sClient", Session("sClient"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .AddTextColumn(8955, GetLocalResourceObject("tctCliNumDocuColumnCaption"), "tctCliNumDocu", 12, "", True, GetLocalResourceObject("tctCliNumDocuColumnCaption"), , , , False)
            Call .AddDateColumn(8956, GetLocalResourceObject("tcdIssueDatColumnCaption"), "tcdIssueDat", , True, GetLocalResourceObject("tcdIssueDatColumnCaption"), , , , False)
            Call .AddDateColumn(8957, GetLocalResourceObject("tcdExpirDatColumnCaption"), "tcdExpirDat", , False, GetLocalResourceObject("tcdExpirDatColumnCaption"), , , False)
        End With
        
        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "BC6000"
            .Codisp = "BC6000"
            .Top = 100
            .Height = 256
            .Width = 400
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbeTypClientDoc").EditRecord = True
            .Columns("cbeTypClientDoc").Disabled = Request.QueryString.Item("Action") = "Update"
            .Columns("tctCliNumDocu").Disabled = Request.QueryString.Item("Action") = "Update"
		
            .sDelRecordParam = "nFastRecord=" & Request.QueryString.Item("nFastRecord") & "&nTypClientDoc='+ marrArray[lintIndex].cbeTypClientDoc + '"
            .sDelRecordParam = .sDelRecordParam & "&nTypeCompany='+ self.document.forms[0].tcdTypeCompany.value + '"
		
            .sEditRecordParam = "nFastRecord=" & Request.QueryString.Item("nFastRecord")
            .sEditRecordParam = .sEditRecordParam & "&nTypeCompany='+ self.document.forms[0].tcdTypeCompany.value + '"
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

'%insPreBC6000. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreBC6000()
	'------------------------------------------------------------------------------
	Dim lcolCliDocumentss As eClient.CliDocumentss
	Dim lclsCliDocuments As Object
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	lcolCliDocumentss = New eClient.CliDocumentss
	
	With mobjGrid
		If lcolCliDocumentss.Find(Session("sClient")) Then
			For	Each lclsCliDocuments In lcolCliDocumentss
				.Columns("cbeTypClientDoc").DefValue = lclsCliDocuments.nTypClientDoc
				.Columns("tctCliNumDocu").DefValue = lclsCliDocuments.sCliNumDocu
				.Columns("tcdIssueDat").DefValue = lclsCliDocuments.dIssueDat
				.Columns("tcdExpirDat").DefValue = lclsCliDocuments.dExpirDat
				Response.Write(mobjGrid.DoRow())
			Next lclsCliDocuments
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsCliDocuments = Nothing
	lcolCliDocumentss = Nothing
End Sub

'% insPreBC6000Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreBC6000Upd()
	'------------------------------------------------------------------------------
	Dim lclsCliDocuments As eClient.CliDocuments
	Dim lobjClient As eClient.ClientWin
	Dim lstrContetn As String
	Dim lintTypeCompany As Object
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsCliDocuments = New eClient.CliDocuments
			
			If (CDbl(.QueryString.Item("nTypeComany")) = 0 Or CStr(.QueryString.Item("nTypeCompany")) = vbNullString) And .QueryString.Item("nFastRecord") = "1" Then
				lintTypeCompany = Session("nTypeCompany")
			Else
				lintTypeCompany = .QueryString.Item("nTypeComany")
			End If
			
			
			If lclsCliDocuments.InsPostBC6000(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), Session("sClient"), mobjValues.StringToType(.QueryString.Item("nTypClientDoc"), eFunctions.Values.eTypeData.etdInteger), "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write(mobjValues.ConfirmDelete())
				lstrContetn = lclsCliDocuments.insBC6000Content(Session("sClient"), Session("nUsercode"), mobjValues.StringToType(lintTypeCompany, eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"))
				lobjClient = New eClient.ClientWin
				lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), lstrContetn)
				'Response.Write("<SCRIPT>top.opener.top.fraSequence.UpdContent('" & Request.QueryString.Item("sCodispl") & "','" & lstrContetn & "');</" & "Script>")
				lobjClient = Nothing
			End If
		Else
			Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")) & "<BR>")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValClientSeq.aspx", "BC6000", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	
	lclsCliDocuments = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("BC6000")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "BC6000"
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%> 

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		If CDbl(Request.QueryString.Item("nFastRecord")) <> 1 Then
			.Write(mobjMenu.setZone(2, "BC6000", "BC6000.aspx"))
		End If
		
		mobjMenu = Nothing
	End If
End With

%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST" ID="FORM" NAME="frmBC6000" ACTION="valclientseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nFastRecord=<%=Request.QueryString.Item("nFastRecord")%>">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreBC6000()
Else
	Call insPreBC6000Upd()
End If

mobjMenu = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("BC6000")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>











