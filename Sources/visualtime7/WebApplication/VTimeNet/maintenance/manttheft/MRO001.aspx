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
            Call .AddNumericColumn(8596, GetLocalResourceObject("tcnCap_initColumnCaption"), "tcnCap_init", 18, "", True, GetLocalResourceObject("tcnCap_initColumnToolTip"), True, 6, , , , False)
            Call .AddNumericColumn(8597, GetLocalResourceObject("tcnCap_endColumnCaption"), "tcnCap_end", 18, "", False, GetLocalResourceObject("tcnCap_endColumnToolTip"), True, 6, , , , False)
            Call .AddNumericColumn(8598, GetLocalResourceObject("tcnTar_theftColumnCaption"), "tcnTar_theft", 4, "", False, GetLocalResourceObject("tcnTar_theftColumnToolTip"), True, 0, , , , False)
	End With
	    
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MRO001"
		.Codisp = "MRO001"
		.Top = 100
		.Height = 224
		.Width = 505
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnCap_init").EditRecord = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302
		.Columns("tcnCap_init").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nCap_init='+ marrArray[lintIndex].tcnCap_init + '"
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	    .AddButton = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd)		
	End With
End Sub

'%insPreMRO001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMRO001()
	'------------------------------------------------------------------------------
	Dim lcolTar_theft_caps As eBranches.Tar_theft_caps
	Dim lclsTar_theft_cap As Object
	
	With Request
		lcolTar_theft_caps = New eBranches.Tar_theft_caps
		With mobjGrid
			If lcolTar_theft_caps.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				For	Each lclsTar_theft_cap In lcolTar_theft_caps
					.Columns("tcnCap_init").DefValue = lclsTar_theft_cap.nCap_init
					.Columns("tcnCap_end").DefValue = lclsTar_theft_cap.nCap_end
					.Columns("tcnTar_theft").DefValue = lclsTar_theft_cap.nTar_theft
					Response.Write(mobjGrid.DoRow())
				Next lclsTar_theft_cap
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
        lclsTar_theft_cap = Nothing
	lcolTar_theft_caps = Nothing
End Sub

'% insPreMRO001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
    Private Sub insPreMRO001Upd()
        '------------------------------------------------------------------------------
        Dim lclsTar_theft_cap As eBranches.Tar_theft_cap
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclsTar_theft_cap = New eBranches.Tar_theft_cap
                Call lclsTar_theft_cap.InsPostMRO001(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCap_init"), eFunctions.Values.eTypeData.etdDouble), 0, 0)
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmanttheft.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
        
        lclsTar_theft_cap = Nothing
        
    End Sub

</script>
<%
    Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MRO001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MRO001"
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
            .Write(mobjMenu.setZone(2, "MRO001", "MRO001.aspx"))
            mobjMenu = Nothing
        End If
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMRO001" ACTION="valmanttheft.aspx?sZone=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
    
    Call insDefineHeader()
    
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreMRO001()
    Else
        Call insPreMRO001Upd()
    End If
    
    mobjValues = Nothing
    mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Call mobjNetFrameWork.FinishPage("MRO001")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








