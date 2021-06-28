<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de la página. 
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas. 
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo de las zonas de la pantalla. 
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de último número de factura.    
Dim mobjBills_Num As eCollection.Bills_Num

Dim nGetLastBill As Object


'%insDefineHeader: Configura los títulos del encabezado del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCompDateColumnCaption"), "tcdCompDate", CStr(Today),  , GetLocalResourceObject("tcdCompDateColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitNumbColumnCaption"), "tcnInitNumb", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnInitNumbColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndNumbColumnCaption"), "tcnEndNumb", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnEndNumbColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLastBillColumnCaption"), "tcnLastBill", 10, nGetLastBill,  , GetLocalResourceObject("tcnLastBillColumnToolTip"),  ,  ,  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MCO689"
		.Codisp = "MCO689"
		.sCodisplPage = "MCO689"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 280
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		'+Pase de parametros necesarios para la eliminación de registros	                       
		.sDelRecordParam = "nInsur_area=" & mobjValues.TypeToString(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble) & "&sBilltype=" & Session("sBilltype") & "&nInitnumb='   + marrArray[lintIndex].tcnInitNumb + '" & "&nEndnumb='    + marrArray[lintIndex].tcnEndNumb  + '" & "&nLastbill='   + marrArray[lintIndex].tcnLastBill + '" & "&dLastclosed=' + marrArray[lintIndex].tcdLastclosed + '" & "&dCompDate='   + marrArray[lintIndex].tcdCompDate + '"
		
		'+ Permite continuar si el check está marcado
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMCO689: Se cargan los datos repetitivos de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreMCO689()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lclsBills_Num As eCollection.Bills_Num
	Dim lclsBills_Nums As eCollection.Bills_Nums
	
	lclsBills_Num = New eCollection.Bills_Num
	lclsBills_Nums = New eCollection.Bills_Nums
	
	If lclsBills_Nums.Find(mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("sBilltype"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsBills_Num In lclsBills_Nums
			With mobjGrid
				.Columns("tcdCompDate").DefValue = CStr(lclsBills_Num.dCompDate)
				.Columns("tcnInitNumb").DefValue = CStr(lclsBills_Num.nInitnumb)
				.Columns("tcnEndNumb").DefValue = CStr(lclsBills_Num.nEndnumb)
				.Columns("tcnLastBill").DefValue = CStr(lclsBills_Num.nLastbill)
				
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsBills_Num
	End If
	Response.Write(mobjGrid.closeTable())
	' Boton de inicio
	
	Response.Write(mobjValues.BeginPageButton)
	
	lclsBills_Num = Nothing
	lclsBills_Nums = Nothing
	
End Sub

'% insPreMCO689Upd : Permite realizar las actualizaciones sobre los aranceles Fonasa.
'-------------------------------------------------------------------------------------------
Private Sub insPreMCO689Upd()
	'-------------------------------------------------------------------------------------------
	Dim lclsBills_NumDel As eCollection.Bills_Num
	lclsBills_NumDel = New eCollection.Bills_Num
	
	' Accion para eliminacion de datos del grid
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsBills_NumDel.insPostMCO689(.QueryString.Item("Action"), CInt(.QueryString.Item("nInsur_area")), .QueryString.Item("sBilltype"), CDbl(.QueryString.Item("nInitNumb")), CDbl(.QueryString.Item("nEndNumb")), CDbl(.QueryString.Item("nLastBill")), CDate(.QueryString.Item("dCompDate")), CDate(.QueryString.Item("dCompDate")), Session("nUsercode")) Then
				
			End If
			
			lclsBills_NumDel = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCollection.aspx", "MCO689", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjBills_Num = New eCollection.Bills_Num

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MCO689"
%>  
<HTML> 
<HEAD> 
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:57 $|$$Author: Nvaplat61 $"
   </SCRIPT>
    


    
    <%Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT> var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCO689", "MCO689.aspx"))
	mobjMenu = Nothing
End If
Response.Write(mobjValues.ShowWindowsName("MCO689"))
Response.Write(mobjValues.WindowsTitle("MCO689"))
%>
	
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frm689" ACTION="valMantCollection.aspx?sZone=2">
<%

'+ Se obtiene el último número de factura

nGetLastBill = mobjBills_Num.getLastBill(mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("sBilltype"), eFunctions.Values.eTypeData.etdDouble))

'+ Se configura la estructura del grid, deacuerdo al tipo de ventana.
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCO689Upd()
Else
	Call insPreMCO689()
End If

mobjValues = Nothing
mobjGrid = Nothing
mobjBills_Num = Nothing
%>	
</FORM>
</BODY>
</HTML>

 





