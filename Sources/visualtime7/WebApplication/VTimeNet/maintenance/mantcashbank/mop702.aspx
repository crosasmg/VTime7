<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen las columnas del Grid
	
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", 5, "0",  , GetLocalResourceObject("valConceptColumnToolTip"),  ,  ,  ,  ,  ,  , 1)
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDescriptColumnCaption"), "cbeDescript", "table22", 1,  ,  ,  ,  ,  ,  ,  , 30, GetLocalResourceObject("cbeDescriptColumnToolTip"), 2)
		Else
			If Request.QueryString.Item("Action") <> "Add" Then
				Call .AddNumericColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", 5, "0",  , GetLocalResourceObject("valConceptColumnToolTip"),  ,  ,  ,  ,  ,  , 1)
			Else
				Call .AddPossiblesColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", "TabtabConcept", 2,  ,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valConceptColumnToolTip"), 1)
			End If
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"), 2)
		
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
		.Codispl = "MOP702"
		.Codisp = "MOP702"
		.sCodisplPage = "MOP702"
		.Height = 300
		.Width = 350
		.Columns("valConcept").EditRecord = True
		.Columns("cbeStatregt").TypeList = 2
		.Columns("cbeStatregt").List = "2"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("valConcept").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMOP702()
	'------------------------------------------------------------------------------
	Dim lcolCash_conclasss As eCashBank.Cash_conclasss
	Dim lclsCash_conclass As Object
	Dim lintIndex As Short
	
	lcolCash_conclasss = New eCashBank.Cash_conclasss
	If lcolCash_conclasss.Find(mobjValues.StringToType(Session("nClass_concept"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsCash_conclass In lcolCash_conclasss
			With mobjGrid
				.Columns("valConcept").DefValue = lclsCash_conclass.nConcept
				If Request.QueryString.Item("Type") <> "PopUp" Then
					.Columns("cbeDescript").DefValue = lclsCash_conclass.nConcept
				End If
				.Columns("cbeStatregt").DefValue = lclsCash_conclass.sStatregt
				
				.sDelRecordParam = "nClass_Concept=" & mobjValues.typeToString(Session("nClass_Concept"), eFunctions.Values.eTypeData.etdDouble) & "&nConcept='+marrArray[lintIndex].valConcept + '"
				Response.Write(.DoRow)
				lintIndex = lintIndex + 1
			End With
		Next lclsCash_conclass
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsCash_conclass = Nothing
	lcolCash_conclasss = Nothing
End Sub

'------------------------------------------------------------------------------
Private Sub insPreMOP702Upd()
	'------------------------------------------------------------------------------
	Dim lclsCash_conclass As eCashBank.Cash_conclass
	Dim lstrErrors As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsCash_conclass = New eCashBank.Cash_conclass
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsCash_conclass
			.nClass_concept = mobjValues.StringToType(Session("nClass_concept"), eFunctions.Values.eTypeData.etdDouble)
			
			.nConcept = mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCashBank.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	End With
	lclsCash_conclass = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MOP702"

%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

    <%=mobjValues.StyleSheet()%>
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MOP702", "MOP702"))
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="MOP702" ACTION="valMantCashBank.aspx?Mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOP702()
Else
	Call insPreMOP702Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




