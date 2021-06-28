<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
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
		Call .AddPossiblesColumn(0, "Enfermedad", "valIllness", "TAB_AM_ILL", eFunctions.Values.eValuesType.clngWindowType, "", False,  ,  ,  ,  , False, 8, "Código de la enfermedad", eFunctions.Values.eTypeCode.eString)
		Call .AddNumericColumn(0, "En clínicas", "tcnResAveClin", 6, "", False, "Indica el valor promedio en clínicas", False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, "En hospitales", "tcnResAveHosp", 6, "", False, "Indica el valor promedio en hospitales", False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, "Sin proveedor", "tcnRes_Average", 6, "", False, "Indica el valor promedio sin proveedor", False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, "Incapacidad T", "tcnResAveTDis", 6, "", False, "Indica el valor pomedio de IT", False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, "Días de IT", "tcnDaysAveDis", 4, "", False, "Indica los días de Incapacidad temporal", False, 0,  ,  ,  , False)
		Call .AddPossiblesColumn(0, "Estado", "valStatRegt", "Table26", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  , False, 10, "Estado del registro", eFunctions.Values.eTypeCode.eString)
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MSI6002"
		.Codisp = "MSI6002"
		.Top = 100
		.Height = 352
		.Width = 610
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valIllness").EditRecord = True
            .Columns("valIllness").Disabled = False
            .Columns("valStatRegt").BlankPosition = False
		.sDelRecordParam = "nCli_category=" & Session("nCli_category") & "&nCurrency=" & Session("nCurrency") & "&sIllness='+ marrArray[lintIndex].valIllness + '"
		.sEditRecordParam = "nCli_category=" & Session("nCli_category") & "&nCurrency=" & Session("nCurrency")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMSI6002. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMSI6002()
	'------------------------------------------------------------------------------
	Dim lcolRes_AverageSoats As  eClaim.Res_AverageSoats
	Dim lclsRes_AverageSoat As  eClaim.Res_AverageSoat
	Dim lintCount as Integer
	
    lcolRes_AverageSoats = New eClaim.Res_AverageSoats
	lclsRes_AverageSoat = New eClaim.Res_AverageSoat
	
	With Request
		If lcolRes_AverageSoats.Find(mobjValues.StringToType(Session("nCli_category"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger)) then
		    lintCount = 0
		    For	Each lclsRes_AverageSoat In lcolRes_AverageSoats
		        With mobjGrid
		            .Columns("valIllness").DefValue     = lclsRes_AverageSoat.sIllness
		            .Columns("tcnResAveClin").DefValue  = lclsRes_AverageSoat.nResAveClin
		            .Columns("tcnResAveHosp").DefValue  = lclsRes_AverageSoat.nResAveHosp
		            .Columns("tcnRes_Average").DefValue = lclsRes_AverageSoat.nRes_Average
		            .Columns("tcnResAveTDis").DefValue  = lclsRes_AverageSoat.nResAveTDis
		            .Columns("tcnDaysAveDis").DefValue  = lclsRes_AverageSoat.nDaysAveDis
		            .Columns("valStatRegt").DefValue    = lclsRes_AverageSoat.sStatRegt
			    End With
			    lintCount = lintCount + 1
			    '+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			    Response.Write(mobjGrid.DoRow())
			Next lclsRes_AverageSoat
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsRes_AverageSoat = Nothing
	lcolRes_AverageSoats = Nothing
End Sub

'% insPreMSI6002Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMSI6002Upd()
	Dim lcolRes_AverageSoats As eClaim.Res_AverageSoats
	'------------------------------------------------------------------------------
	Dim lclsRes_AverageSoat As eClaim.Res_AverageSoat
	
	lcolRes_AverageSoats = New eClaim.Res_AverageSoats
	lclsRes_AverageSoat = New eClaim.Res_AverageSoat
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsRes_AverageSoat.insPostMSI6002(.QueryString("Action"), Session("nUsercode"), mobjValues.StringToType(Session("nCli_category"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger), .QueryString("sIllness"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantClaim.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsRes_AverageSoat = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MSI6002")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MSI6002"
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
		.Write(mobjMenu.setZone(2, "MSI6002", "MSI6002.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSI6002" ACTION="valmantclaim.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI6002()
Else
	Call insPreMSI6002Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MSI6002")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





