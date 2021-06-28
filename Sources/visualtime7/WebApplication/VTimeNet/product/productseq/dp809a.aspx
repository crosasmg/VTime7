<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lintTratypep As Object

'- Objeto para el manejo de los datos de la ventana
Dim mclsSection_prod As eProduct.Section_prod


'% insDefineHeader: Se definen las propiedades de los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctSectionColumnCaption"), "tctSection", 40, CStr(0), False, GetLocalResourceObject("tctSectionColumnToolTip"),  ,  ,  , True)
		'Call .AddTextColumn(0, GetLocalResourceObject("tctSectionColumnCaption"),"tctSection",40,0,False, GetLocalResourceObject("tctSectionColumnToolTip"),,,,True)
		Call .AddHiddenColumn("hddSel", CStr(2))
		Call .AddHiddenColumn("hddCodispl", vbNullString)
		Call .AddHiddenColumn("hddCodisplorig", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP809A"
		.Width = 380
		.Height = 200
		.DeleteButton = False
		.AddButton = False
		.ActionQuery = Session("bQuery") Or mclsSection_prod.bError
		.bOnlyForQuery = .ActionQuery
		.Columns("tctSection").GridVisible = Not Session("bQuery")
		.DeleteScriptName = vbNullString
		.MoveRecordScript = "insDefValuess()"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("Sel").OnClick = "InsSelected(this.value, this.checked)"
	End With
End Sub

'% insPreDP048: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP809A()
	'--------------------------------------------------------------------------------------------
	Dim lcolSection_prods As eProduct.Section_prods
	Dim lclsErrors As eFunctions.Errors
	Dim lintIndex As Short
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("// insCheckSelClick : Establece La acción a ejecutar dependiendo del estado del campo Sel" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCheckSelClick(Field,lintIndex){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrParam='';" & vbCrLf)
Response.Write("    var sselected=0;" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    if (!Field.checked){" & vbCrLf)
Response.Write("		with (self.document.forms [0]){" & vbCrLf)
Response.Write("			lstrParam=	""sCodispl=""+marrArray[lintIndex].hddCodispl + " & vbCrLf)
Response.Write("						""&sCodispl_orig="" + marrArray[lintIndex].hddCodisplorig +" & vbCrLf)
Response.Write("						""&sselected=2""" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    else{" & vbCrLf)
Response.Write("		with (self.document.forms [0]){" & vbCrLf)
Response.Write("			lstrParam=	""sCodispl=""+marrArray[lintIndex].hddCodispl + " & vbCrLf)
Response.Write("						""&sCodispl_orig="" + marrArray[lintIndex].hddCodisplorig +" & vbCrLf)
Response.Write("						""&sselected=1""" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    insDefValues('UpdateDP809', lstrParam )" & vbCrLf)
Response.Write("    //Field.checked = !Field.checked" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("")

	
	lintIndex = 0
	
	If mclsSection_prod.bError Then
		lclsErrors = New eFunctions.Errors
		Response.Write(mobjGrid.closeTable())
		Response.Write(lclsErrors.ErrorMessage("DP012", 11399,  ,  ,  , True))
	Else
		lcolSection_prods = New eProduct.Section_prods
		
		If lcolSection_prods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
			For	Each mclsSection_prod In lcolSection_prods
				With mobjGrid
					.Columns("tctSection").DefValue = mclsSection_prod.sCodispl & " - " & mclsSection_prod.sDescript
					.Columns("hddCodispl").DefValue = mclsSection_prod.sCodispl
					.Columns("hddCodisplorig").DefValue = Request.QueryString.Item("scodispl_orig")
					.Columns("Sel").Checked = 2
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
					.Columns("hddSel").DefValue = CStr(2)
					
					If mclsSection_prod.sCodispl_orig = Request.QueryString.Item("scodispl_orig") Then
						.Columns("Sel").Checked = 1
						.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
						.Columns("hddSel").DefValue = CStr(1)
					End If
					
					Response.Write(.DoRow)
				End With
				lintIndex = lintIndex + 1
			Next mclsSection_prod
		End If
		With Response
			.Write(mobjGrid.closeTable())
			.Write(mobjValues.BeginPageButton)
			'			.Write mobjGrid.DoFormUpd(Request.QueryString("Action"),"ValProductSeq.aspx","DP809",Request.QueryString("nMainAction"),mobjValues.ActionQuery,Request.QueryString("Index"))
		End With
	End If
	
	lcolSection_prods = Nothing
	lclsErrors = Nothing
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")

	'=mobjValues.ButtonAbout("DP809A")
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	<TABLE>")

	
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	mclsSection_prod = New eProduct.Section_prod
End With

If IsNothing(Request.QueryString.Item("nTratypep")) Then
	lintTratypep = 7
Else
	lintTratypep = Request.QueryString.Item("nTratypep")
End If


mobjGrid.sCodisplPage = "DP809"
mobjValues.sCodisplPage = "DP809"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




	<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP809", "DP809.aspx"))
	End If
	mobjMenu = Nothing
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 11/12/03 10:57 $|$$Author: Nvaplat11 $"

//% insDefValuess: se asignan los valores por defecto a los campos de la página
//-------------------------------------------------------------------------------------------
function insDefValuess(){
//-------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

	if(self.document.forms[0].tcnOrder.value==0){
//+ Se genera el número consecutivo para el campo "Orden de aparición"
		with (top.opener){
			for(var llngIndex = 0;llngIndex<marrArray.length;llngIndex++)
			    if(marrArray[llngIndex].tcnOrder>llngMax)
			        llngMax = marrArray[llngIndex].tcnOrder
		}
	
//+ Se asignan los valores a los campos de la página	
		with (self.document.forms[0]){
		    if(++llngMax.length > tcnOrder.maxLength)
				tcnOrder.value = "";
			else
				tcnOrder.value = ++llngMax;
		}
	}
}

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
	self.document.forms[0].hddMassive.value=2;
	top.frames['fraHeader'].ClientRequest(390,2);
}

//% InsSelected: Se actualiza el campo oculta imagen del campo Sel
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	with(document.forms[0]){
		if(hddSel.length>0){
			hddSel[nIndex].value =(bChecked?1:2);
		}
		else {
			hddSel.value =(bChecked?1:2);
		}			
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP809" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%'Response.Write mobjValues.ShowWindowsName(Request.QueryString("sCodispl"))
Response.Write(mobjValues.ShowWindowsName("DP048"))
Response.Write("<BR>")
Call insDefineHeader()

Call mclsSection_prod.inspreDP809(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
Call insDefineHeader()
Call insPreDP809A()

mobjValues = Nothing
mobjGrid = Nothing
mclsSection_prod = Nothing
%>
</FORM>
</BODY>
</HTML>





