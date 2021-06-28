<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga de datos del Grid de la ventana		
Dim mclsDisco_expr As eProduct.Disco_expr
Dim mcolDisco_exprs As eProduct.Disco_exprs


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid.ActionQuery = Session("bQuery")
	mobjGrid.sCodisplPage = "DP008"
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddAnimatedColumn(0, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("sLinkColumnToolTip"))
		End If
		.AddNumericColumn(0, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 5, CStr(0), False, GetLocalResourceObject("tcnCodeColumnToolTip"))
        .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "table30", eFunctions.Values.eValuesType.clngComboType, "0",  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeTypeColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		.AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, "",  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnOrder_aplColumnCaption"), "tcnOrder_apl", 5, CStr(0),  , GetLocalResourceObject("tcnOrder_aplColumnToolTip"), False)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "table26", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeStatregtColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		.AddHiddenColumn("tcnExist", CStr(0))
		.AddHiddenColumn("tctOldStatregt", "")
	End With
	
	With mobjGrid
		.Codispl = "DP008"
		.Codisp = "DP008"
		.Top = 135
		.Left = 100
		.Width = 450
		.Height = 300
		.DeleteButton = False
		.bCheckVisible = Request.QueryString.Item("Action") <> "Add"
		.Columns("Sel").GridVisible = True
		.Columns("tctDescript").EditRecord = True
		.Columns("cbeStatregt").BlankPosition = False
		.Columns("tcnCode").Disabled = Request.QueryString.Item("Action") = "Update"
		.WidthDelete = 570
		
		'+ Se establece es estado inicial del campo "Estado" según la acción y el estado		
		.Columns("cbeStatregt").Disabled = Request.QueryString.Item("Action") = "Add" Or Request.QueryString.Item("sStatregt") = "2"
		
		.sDelRecordParam = "nDisexprc='+ marrArray[lintIndex].tcnCode + '&nConcept='+ marrArray[lintIndex].cbeConcept + '"
		
		'+ El estado "En proceso de instalación" (sStatregt = 2) solo es usado por el sistema
		If Request.QueryString.Item("Action") = "Update" And Request.QueryString.Item("sStatregt") <> "2" Then
			.Columns("cbeStatregt").TypeList = 2
			.Columns("cbeStatregt").List = CStr(2)
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'% insPreDP008: Obtiene los recargos/descuentos/impuestos del producto
'-----------------------------------------------------------------------------
Private Sub insPreDP008()
	'-----------------------------------------------------------------------------
	
	Dim lintIndex As Integer
	If mcolDisco_exprs.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		If mcolDisco_exprs.Count > 0 Then
			lintIndex = 0
			mobjGrid.DeleteButton = True
			For lintIndex = 1 To mcolDisco_exprs.Count
				With mobjGrid
					.Columns("tcnCode").DefValue = CStr(mcolDisco_exprs.item(lintIndex).nDisexprc)
					.Columns("cbeType").DefValue = mcolDisco_exprs.item(lintIndex).sDisexpri
					.Columns("tctDescript").DefValue = mcolDisco_exprs.item(lintIndex).sDescript
					.Columns("tctShort_des").DefValue = mcolDisco_exprs.item(lintIndex).sShort_des
					.Columns("tcnOrder_apl").DefValue = CStr(mcolDisco_exprs.item(lintIndex).nOrder_apl)
					.Columns("cbeStatregt").DefValue = mcolDisco_exprs.item(lintIndex).sStatregt
					.Columns("tctOldStatregt").DefValue = mcolDisco_exprs.item(lintIndex).sStatregt
					.Columns("tcnExist").DefValue = CStr(1)
					.Columns("sLink").HRefScript = "ShowSubSequence(" & lintIndex - 1 & ")"
					.Columns("Sel").OnClick = "valDelete(" & lintIndex - 1 & ")"
					mobjGrid.sEditRecordParam = "sStatregt=" & mcolDisco_exprs.item(lintIndex).sStatregt
				End With
				Response.Write(mobjGrid.DoRow())
			Next 
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	With Request
		If .QueryString.Item("ReloadAction") = "Add" Then
			Response.Write("<SCRIPT>ShowSubSequence(-1,'" & .QueryString.Item("nDisexprc") & "','" & .QueryString.Item("nOrderApl") & "','" & .QueryString.Item("sDescript") & "','" & .QueryString.Item("nType") & "')</" & "Script>")
		End If
	End With
End Sub

'% insPreDP008Upd: Realiza la eliminación de recargos/descuentos
'-----------------------------------------------------------------------------
Private Sub insPreDP008Upd()
	'-----------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	If Request.QueryString.Item("Action") = "Del" Then
		'+ Muestra el mensaje para eliminar registros		
		Response.Write(mobjValues.ConfirmDelete())
		
		lblnPost = mclsDisco_expr.insPostDP008("Del", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString, eRemoteDB.Constants.intNull, vbNullString, vbNullString)
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valProductSeq.aspx", "DP008", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	If Request.QueryString.Item("Action") = "Add" Then
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("	FindDiscNumber();" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	End If
End Sub

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsDisco_expr = New eProduct.Disco_expr
mcolDisco_exprs = New eProduct.Disco_exprs
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "DP008"

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP008"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP008", "DP008.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:01 $|$$Author: Nvaplat61 $"
   	    
//% valDelete: Se verifica si se puede eliminar el registro
//------------------------------------------------------------------------------------------
function valDelete(nIndex){
//------------------------------------------------------------------------------------------
	insDefValues('DeleteDP008', 'nDisexprc=' + marrArray[nIndex].tcnCode + '&nIndex=' + nIndex)
}

//% FindDiscNumber: calcula el código del recargo
//--------------------------------------------------------------------------------------------
function FindDiscNumber(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

//+ Se genera el número consecutivo del recargo/descuento (el Nº consecutivo más alto +1)   
	if (self.document.forms[0].tcnCode.value == 0 || self.document.forms[0].tcnCode.value == ""){
		for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++)
			if(insConvertNumber(top.opener.marrArray[llngIndex].tcnCode)>llngMax)
				llngMax = top.opener.marrArray[llngIndex].tcnCode
		self.document.forms[0].tcnCode.value = ++llngMax
	}
}

//% ShowSubSequence: muestra la subsecuencia para la cobertura en tratamiento
//--------------------------------------------------------------------------------------------
function ShowSubSequence(Index,nDisexprc,nOrderApl,sDescript,nType){
//--------------------------------------------------------------------------------------------
	if(typeof(nDisexprc)=='undefined'){
	    //ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/DiscoExprSeq&bAutomatic=false&sCodispl=DP08B1&nDisexprc=' + marrArray[Index].tcnCode + '&nOrderApl='+ marrArray[Index].tcnOrder_apl + '&sDescript=' + marrArray[Index].tctDescript + '&nType=' + marrArray[Index].cbeType, 'DiscoExprSeq', 750, 500, 'no', 'no', 20, 20, 'yes')
	    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/DiscoExprSeq&bAutomatic=false&sCodispl=DP08B1&nDisexprc=' + marrArray[Index].tcnCode + '&nOrderApl=' + marrArray[Index].tcnOrder_apl + '&sDescript=' + marrArray[Index].tctDescript + '&nType=' + marrArray[Index].cbeType, 'DiscoExprSeq', 750, 500, 'no', 'no', 20, 20, 'yes')
	}
	else{
	    //ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/DiscoExprSeq&bAutomatic=true&sCodispl=DP08B1&nDisexprc=' + nDisexprc + '&nOrderApl='+ nOrderApl + '&sDescript=' + sDescript  + '&nType=' + nType, 'DiscoExprSeq', 750, 500, 'no', 'no', 20, 20, 'yes')
	    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/DiscoExprSeq&bAutomatic=true&sCodispl=DP08B1&nDisexprc=' + nDisexprc + '&nOrderApl=' + nOrderApl + '&nType=' + nType, 'DiscoExprSeq', 750, 500, 'no', 'no', 20, 20, 'yes')
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP008" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP008"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP008()
Else
	Call insPreDP008Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsDisco_expr = Nothing
mcolDisco_exprs = Nothing
%> 
</FORM>
</BODY>
</HTML>




