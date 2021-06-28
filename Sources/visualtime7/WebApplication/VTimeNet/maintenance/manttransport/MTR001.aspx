<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
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
		Call .AddPossiblesColumn(15988,"Clase", "valClassMerch", "Table232", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , 2,"Indica la clase de mercancía que se transporta.", eFunctions.Values.eTypeCode.eNumeric)
		Call .AddPossiblesColumn(15989,"Embalaje", "cbePacking", "Table237", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 2,"Embalaje posible de utilización en el transporte de la mercancía", eFunctions.Values.eTypeCode.eNumeric)
		Call .AddNumericColumn(15990,"Tasa", "tcnRate", 4, "", False,"Tasa a aplicar a la mercancía transportada", False, 2,  ,  ,  , False)
	End With
	
	With mobjGrid
		.nMainAction = CShort(Request.QueryString.Item("nMainAction"))
		.Codispl = "MTR001"
		.Codisp = "MTR001"
		.Top = 100
		.Height = 224
		.Width = 350
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valClassMerch").EditRecord = True
		.Columns("valClassMerch").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbePacking").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & _
		                   "&nProduct=" & Request.QueryString.Item("nProduct") & _
		                   "&nCurrency=" & Request.QueryString.Item("nCurrency") & _
		                   "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & _
		                   "&nClassMerch='+ marrArray[lintIndex].valClassMerch + '" & _
		                   "&nPacking='+ marrArray[lintIndex].cbePacking + '"
		
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & _
		                    "&nProduct=" & Request.QueryString.Item("nProduct") & _
		                    "&nCurrency=" & Request.QueryString.Item("nCurrency") & _
		                    "&dEffecDate=" & Request.QueryString.Item("dEffecDate")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMTR001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMTR001()
	'------------------------------------------------------------------------------
	Dim lcoltar_tr_mers As eBranches.tar_tr_mers
	Dim lclstar_tr_mer As Object
	
	With Request
		lcoltar_tr_mers = New eBranches.tar_tr_mers
		With mobjGrid
			If lcoltar_tr_mers.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
			                        mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
			                        mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), _
			                        mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
				For	Each lclstar_tr_mer In lcoltar_tr_mers
					.Columns("valClassMerch").DefValue = lclstar_tr_mer.nClassMerch
					.Columns("cbePacking").DefValue = lclstar_tr_mer.nPacking
					.Columns("tcnRate").DefValue = lclstar_tr_mer.nRate
					Response.Write(mobjGrid.DoRow())
				Next lclstar_tr_mer
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclstar_tr_mer = Nothing
	lcoltar_tr_mers = Nothing
End Sub

'% insPreMTR001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMTR001Upd()
	'------------------------------------------------------------------------------
	Dim lclstar_tr_mer As eBranches.tar_tr_mer
	
	With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclstar_tr_mer = New eBranches.tar_tr_mer
                Call lclstar_tr_mer.InsPostMTR001(False, _
                                                  .QueryString.Item("sCodispl"), _
                                                  CInt(.QueryString.Item("nMainAction")), _
                                                  .QueryString.Item("Action"), _
                                                  Session("nUsercode"), _
                                                  mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                                  mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                                  mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                                  mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                                  mobjValues.StringToType(.QueryString.Item("nClassMerch"), eFunctions.Values.eTypeData.etdInteger), _
                                                  mobjValues.StringToType(.QueryString.Item("nPacking"), eFunctions.Values.eTypeData.etdInteger), _
                                                  0)
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmanttransport.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclstar_tr_mer = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MTR001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MTR001"
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
		.Write(mobjMenu.setZone(2, "MTR001", "MTR001.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMTR001" ACTION="valmanttransport.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMTR001()
Else
	Call insPreMTR001Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MTR001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




