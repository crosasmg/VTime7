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
            
            Call .AddNumericColumn(8604, GetLocalResourceObject("tcnInsuredColumnCaption"), "tcnInsured", 4, "", True, GetLocalResourceObject("tcnInsuredColumnToolTip"), False, 0, , , , False)
            Call .AddPossiblesColumn(8605, GetLocalResourceObject("cbeRiskClassColumnCaption"), "cbeRiskClass", "Table241", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeRiskClassColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(8606, GetLocalResourceObject("cbeUbicationColumnCaption"), "cbeUbication", "Table239", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeUbicationColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(8607, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "", False, GetLocalResourceObject("tcnRateColumnToolTip"), False, 6, , , , False)
            
        End With
	
        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MRO002"
            .Codisp = "MRO002"
            .Top = 100
            .Height = 256
            .Width = 595
            .WidthDelete = 400
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("tcnInsured").EditRecord = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 302
            .Columns("tcnInsured").Disabled = Request.QueryString.Item("Action") = "Update"
            .Columns("cbeRiskClass").Disabled = Request.QueryString.Item("Action") = "Update"
            .Columns("cbeUbication").Disabled = Request.QueryString.Item("Action") = "Update"
            .sDelRecordParam = "nTar_theft=" & Request.QueryString.Item("nTar_theft") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nInsured='+ marrArray[lintIndex].tcnInsured + '" & "&nRiskClass='+ marrArray[lintIndex].cbeRiskClass + '" & "&nUbication='+ marrArray[lintIndex].cbeUbication + '"
            .sEditRecordParam = "nTar_theft=" & Request.QueryString.Item("nTar_theft") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .AddButton = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd)
        End With
End Sub

'%insPreMRO002. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMRO002()
	'------------------------------------------------------------------------------
	Dim lcolTar_theft_cons As eBranches.Tar_theft_cons
	Dim lclsTar_theft_con As Object
	lcolTar_theft_cons = New eBranches.Tar_theft_cons
	With Request
		With mobjGrid
			
			Call lcolTar_theft_cons.Find(mobjValues.StringToType(Request.QueryString.Item("nTar_theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
			
			For	Each lclsTar_theft_con In lcolTar_theft_cons
				
				.Columns("tcnInsured").DefValue = lclsTar_theft_con.ninsured
				.Columns("cbeRiskClass").DefValue = lclsTar_theft_con.nriskclass
				.Columns("cbeUbication").DefValue = lclsTar_theft_con.nubication
				.Columns("tcnRate").DefValue = lclsTar_theft_con.nrate
				
				Response.Write(mobjGrid.DoRow())
			Next lclsTar_theft_con
			
		End With
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsTar_theft_con = Nothing
	lcolTar_theft_cons = Nothing
End Sub

'% insPreMRO002Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMRO002Upd()
	'------------------------------------------------------------------------------
	Dim lclsTar_theft_con As eBranches.Tar_theft_con
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsTar_theft_con = New eBranches.Tar_theft_con
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsTar_theft_con.insPostMRO002(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nTar_theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nInsured"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nRiskClass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nUbication"), eFunctions.Values.eTypeData.etdInteger), 0)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmanttheft.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTar_theft_con = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MRO002")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MRO002"
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
            .Write(mobjMenu.setZone(2, "MRO002", "MRO002.aspx"))
            mobjMenu = Nothing
        End If
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMRO002" ACTION="valmanttheft.aspx?sZone=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
    
    Call insDefineHeader()
    
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreMRO002()
    Else
        Call insPreMRO002Upd()
    End If
    mobjValues = Nothing
    mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Call mobjNetFrameWork.FinishPage("MRO002")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








