<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
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


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
            Call .AddPossiblesColumn(8610, GetLocalResourceObject("cbeUbicationColumnCaption"), "cbeUbication", "Table239", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeUbicationColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(8611, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "", False, GetLocalResourceObject("tcnRateColumnToolTip"), False, 6, , , , False)
	End With
	
        
        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MRO003"
            .Codisp = "MRO003"
            .Top = 100
            .Height = 192
            .Width = 595
            .WidthDelete = 600
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbeUbication").EditRecord = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 302
            .Columns("cbeUbication").Disabled = Request.QueryString.Item("Action") = "Update"
            .sDelRecordParam = "nTar_Theft=" & Request.QueryString.Item("nTar_Theft") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nUbication='+ marrArray[lintIndex].cbeUbication + '"
            .sEditRecordParam = "nTar_Theft=" & Request.QueryString.Item("nTar_Theft") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate")
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .AddButton = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd)
        End With
End Sub

'%insPreMRO003. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMRO003()
	'------------------------------------------------------------------------------
	Dim lcoltar_theft_cashs As eBranches.tar_theft_cashs
	Dim lclstar_theft_cash As Object
	
        With Request
            lcoltar_theft_cashs = New eBranches.tar_theft_cashs
            With mobjGrid
                If lcoltar_theft_cashs.Find(mobjValues.StringToType(Request.QueryString.Item("nTar_Theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
                    For Each lclstar_theft_cash In lcoltar_theft_cashs
                        .Columns("cbeUbication").DefValue = lclstar_theft_cash.nUbication
                        .Columns("tcnRate").DefValue = lclstar_theft_cash.nRate
                        Response.Write(mobjGrid.DoRow())
                    Next lclstar_theft_cash
                End If
            End With
		
        End With
	Response.Write(mobjGrid.CloseTable())
	
	'UPGRADE_NOTE: Object lclstar_theft_cash may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclstar_theft_cash = Nothing
	'UPGRADE_NOTE: Object lcoltar_theft_cashs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcoltar_theft_cashs = Nothing
End Sub

'% insPreMRO003Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMRO003Upd()
	'------------------------------------------------------------------------------
	Dim lclstar_theft_cash As eBranches.tar_theft_cash
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclstar_theft_cash = New eBranches.tar_theft_cash
			Call lclstar_theft_cash.InsPostMRO003(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nTar_Theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nUbication"), eFunctions.Values.eTypeData.etdInteger), 0)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmanttheft.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	'UPGRADE_NOTE: Object lclstar_theft_cash may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclstar_theft_cash = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MRO003")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MRO003"
'~End Body Block VisualTimer Utility	
%>

<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MRO003", "MRO003.aspx"))
            mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMRO003" ACTION="valmanttheft.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMRO003()
Else
	Call insPreMRO003Upd()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MRO003")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








