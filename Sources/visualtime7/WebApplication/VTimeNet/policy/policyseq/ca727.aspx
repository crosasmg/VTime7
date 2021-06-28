<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Objeto para el manejo del grid.

Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim PolReport As ePolicy.PolReport
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	PolReport = New ePolicy.PolReport
	
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactypeColumnCaption"), "cbeTransactype", "Table221", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTransactypeColumnCaption"))
		Call .AddTextColumn(0, GetLocalResourceObject("cbeCodCodisplColumnCaption"), "cbeCodCodispl", 8, CStr(0), False, GetLocalResourceObject("cbeCodCodisplColumnCaption"),  ,  ,  , True)
		mobjGrid.Columns("cbeCodCodispl").PopUpVisible = False
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCodisplColumnCaption"), "cbeCodispl", "tabReportprod_scod", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , 8, GetLocalResourceObject("cbeCodisplColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		mobjGrid.Columns("cbeCodispl").Parameters.Add("nbranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeCodispl").Parameters.Add("nproduct", Session("nproduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.AddButton = True
		.DeleteButton = True
		.Height = 200
		.Width = 400
		.Codispl = "CA727"
		.UpdContent = True
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("cbeTransactype").BlankPosition = False
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.sDelRecordParam = "sDelCodispl='+ marrArray[lintIndex].cbeCodispl + '"
		.sDelRecordParam = .sDelRecordParam & "&nDeltransactype='+ marrArray[lintIndex].cbeTransactype + '"
	End With
	
	PolReport = Nothing
End Sub

'% insPreCA727: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreCA727()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolPolReports As ePolicy.PolReports
	Dim lclsPolReport As Object
	Dim mclsProductli As eProduct.Product
	
	lintIndex = 0
	
	If IIf(IsNothing(Request.QueryString.Item("nMainAction")),False,Request.QueryString.Item("nMainAction")) <> Not Session("bQuery") Then
		mclsProductli = New eProduct.Product
		If mclsProductli.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
			lcolPolReports = New ePolicy.PolReports
			If lcolPolReports.FindPolReport(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				
				For	Each lclsPolReport In lcolPolReports
					With mobjGrid
						
						.Columns("cbeTransactype").DefValue = lclsPolReport.nTransactype
						.Columns("cbeCodispl").DefValue = lclsPolReport.sCodispl
						.Columns("cbeCodCodispl").DefValue = lclsPolReport.sCodispl
						Response.Write(.DoRow)
					End With
					
					lintIndex = lintIndex + 1
					
					If lintIndex = 200 Then
						Exit For
					End If
				Next lclsPolReport
			End If
			Response.Write(mobjGrid.closeTable)
			
			lcolPolReports = Nothing
			lclsPolReport = Nothing
		End If
		mclsProductli = Nothing
	End If
End Sub

'% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreCA727Upd()
	Dim lstrContent As String
	'-----------------------------------------------------------------------------------------
	Dim lclsPolReport As ePolicy.PolReport
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		
		lclsPolReport = New ePolicy.PolReport
		
		Call lclsPolReport.insPostCA727("Delete", Session("scertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sDelCodispl"), mobjValues.StringToType(Request.QueryString.Item("nDeltransactype"), eFunctions.Values.eTypeData.etdDouble))
		lstrContent = lclsPolReport.sContent
		lclsPolReport = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index")), lstrContent))
	
	lclsPolReport = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca727")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $"
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA727", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CA727" ACTION="ValPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nCurrency=<%=Request.Form.Item("cbeCurrency")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CA727", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()

mobjGrid.ActionQuery = Session("bQuery")
mobjValues.ActionQuery = Session("bQuery")

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA727Upd()
Else
	Call insPreCA727()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("ca727")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




