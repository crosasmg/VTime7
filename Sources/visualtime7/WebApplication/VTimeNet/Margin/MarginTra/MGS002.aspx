<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">

'**+ ----------------------------------------------------------------------------------------
'**+ MGS002.  Movimientos de ajustes del margen de solvencia 
'**+ ----------------------------------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
'- Objeto para el manejo particular de los datos de la página
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMovementColumnCaption"), "tcnMovement", 10,  ,  , GetLocalResourceObject("tcnMovementColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAdjustamooriColumnCaption"), "tcnAdjustamoori", 18, "Monto del ajuste a realizar al monto inicial",  ,  , True, 6,  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCompdateColumnCaption"), "tcdCompdate", CStr(Today),  , GetLocalResourceObject("tcdCompdateColumnToolTip"),  ,  ,  , True)
		Call .AddHiddenColumn("tcnAdjustamoloc", CStr(0))
		Call .AddHiddenColumn("nAdjAmoLoc", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MGS002"
		.Width = 550
		.Height = 250
		.sCodisplPage = "MGS002"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.WidthDelete = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		
		.sDelRecordParam = "nInsur_area=" & Session("nInsur_area") & "&dInitDate=" & Request.QueryString.Item("dInitDate") & "&nIdTable=" & Request.QueryString.Item("nIdTable") & "&nIdrec=" & Request.QueryString.Item("nIdrec") & "&dValDate=" & Request.QueryString.Item("dValDate") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&nMovement=' + marrArray[lintIndex].tcnMovement + '" & "&nAdjustamoori=' + marrArray[lintIndex].tcnAdjustamoori + '" & "&sDescript=' + marrArray[lintIndex].tctDescript + '"
		
		.sEditRecordParam = "nInsur_area=" & Session("nInsur_area") & "&dInitDate=" & Request.QueryString.Item("dInitDate") & "&nIdTable=" & Request.QueryString.Item("nIdTable") & "&nIdrec=" & Request.QueryString.Item("nIdrec") & "&dValDate=" & Request.QueryString.Item("dValDate") & "&nTableTyp=" & Request.QueryString.Item("nTableTyp") & "&nSource=" & Request.QueryString.Item("nSource") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&nTyperec=" & Request.QueryString.Item("nTyperec") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover")
		
		'		.Columns("tcnMovement").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMGS002: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMGS002()
	'--------------------------------------------------------------------------------------------
	Dim lcolMargin_Adj As eMargin.Margin_Adjs
	Dim lclsMargin_Adj As Object
	
	Dim ldblTotAdjustamoori As Object
	lcolMargin_Adj = New eMargin.Margin_Adjs
	ldblTotAdjustamoori = 0
	
	If lcolMargin_Adj.Find(mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nIdtable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIdrec"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsMargin_Adj In lcolMargin_Adj
			With mobjGrid
				.Columns("tcnMovement").DefValue = lclsMargin_Adj.nMovement
				'				.Columns("nAdjustamoloc").DefValue   = lclsMargin_Adj.nAdjustamoloc 
				.Columns("tcnAdjustamoori").DefValue = lclsMargin_Adj.nAdjustamoori
				.Columns("tctDescript").DefValue = lclsMargin_Adj.sDescript
				.Columns("tcdCompdate").DefValue = lclsMargin_Adj.dCompdate
				'session("nIdtable") = lclsMargin_Adj.nIdtable
				ldblTotAdjustamoori = ldblTotAdjustamoori + lclsMargin_Adj.nAdjustamoori
				
				Response.Write(.DoRow)
			End With
		Next lclsMargin_Adj
	End If
	Response.Write(mobjGrid.closeTable())
	
Response.Write("   " & vbCrLf)
Response.Write("	<TABLE WIDTH=100%> " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("			<TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("tcnTotAdjustamooriCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD WIDTH=25%>")


Response.Write(mobjValues.NumericControl("tcnTotAdjustamoori", 18, ldblTotAdjustamoori,  , GetLocalResourceObject("tcnTotAdjustamooriToolTip"), True, 6, True,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD> " & vbCrLf)
Response.Write("			<TD WIDTH=25%></TD> " & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("	</TABLE> ")

	
	Response.Write(mobjValues.BeginPageButton)
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
		Response.Write("<SCRIPT>top.opener.document.forms[0].tcnAdjAmoOri.value= VTFormat(" & ldblTotAdjustamoori & ",'', '', '', 6, true);")
		Response.Write("top.opener.ShowChangeAmount();</" & "Script>")
	End If
	lcolMargin_Adj = Nothing
	lclsMargin_Adj = Nothing
End Sub

'% insPreMGS002Upd: Se realiza el manejo de la ventana PopUp asociada al grid 
'-------------------------------------------------------------------------------------------- 
Private Sub insPreMGS002Upd()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsMargin_Adj As eMargin.Margin_Adj
	With Request
		
		If Request.QueryString.Item("Action") = "Del" Then
			lclsMargin_Adj = New eMargin.Margin_Adj
			
			If lclsMargin_Adj.inspostMGS002(.QueryString.Item("Action"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nIdtable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nIdrec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMovement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAdjustamoori"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sDescript"), mobjValues.StringToType(.QueryString.Item("dValDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode")) Then
				
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMarginTra.aspx", "MGS002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		'+ Según la acción se actualizan los valores de la página luego de diseñada.
		If Request.QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insDefAdd();</" & "Script>")
		End If
	End With
	lclsMargin_Adj = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MGS002"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT> 

 

 


<SCRIPT LANGUAGE=JavaScript>
	var nMainAction=<%=Request.QueryString.Item("nMainAction")%>;

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 9/12/03 17:59 $|$$Author: Nvaplat15 $"

// insDefAdd: Establece el estado de la forma cuando se agrega.
//--------------------------------------------------------------------------------------------
function insDefAdd(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax    = 0
    
//+ Se genera el número consecutivo del Order
	for(var llngIndex = 0;llngIndex < top.opener.marrArray.length;llngIndex++)
	    if(top.opener.marrArray[llngIndex].tcnMovement>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].tcnMovement

	    if(++llngMax.length > self.document.forms[0].tcnMovement.maxLength){
//+ Se asignan null
			self.document.forms[0].tcnMovement.value = "";						//+ null			
	    }		
		else{
//+ Se asignan el valor por defecto del Order			
			self.document.forms[0].tcnMovement.value = ++llngMax;				//+ Consecutivo			
		}
	}

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%Response.Write(mobjValues.StyleSheet())
'**+ Si se trata de una ventana que no forma parte del encabezado de la transacción colocar:
'	Response.Write mobjMenu.setZone(2,"MGS002","MGS002.aspx") 

'**+ Si la ventana pertenece al encabezado de la transacción colocar:
'	Response.Write mobjMenu.MakeMenu("MGS002", "MGS002.aspx", 1, vbNullString)

mobjMenu = Nothing
'		Response.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>"
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Nombre_de_la_página" ACTION="Página_de_validaciones.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MGS002"))
Response.Write(mobjValues.WindowsTitle("MGS002"))
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjValues.ActionQuery = True
	%>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeTableTypCaption") %></LABEL></TD>
            <TD><%	Response.Write(mobjValues.PossiblesValues("cbeTableTyp", "Table5607", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTableTyp"),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTableTypToolTip")))
	Response.Write(" - ")
	Response.Write(mobjValues.PossiblesValues("cbeSource", "Table5608", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nSource"),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSourceToolTip")))
	%>
			</TD>
        </TR>
		<%	If Request.QueryString.Item("nTableTyp") <> "5" Then%>
			<TR>
				<TD><LABEL ID=13764><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
				<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct", True)%></TD>
				<TD><LABEL ID=13771><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
				<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct"), True)%></TD>
			</TR>
		<%	End If%>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCurrency"))%> </TD>
			<%	If Request.QueryString.Item("nTableTyp") <> "5" Then%>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeTyperecCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeTyperec", "Table5610", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nTyperec"), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTyperecToolTip"))%></TD>
			<%	End If%>
        </TR>
		<%	If (Request.QueryString.Item("nTableTyp") <> "5" And Request.QueryString.Item("nTyperec") = "1") Then%>
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeModulecCaption") %></LABEL></TD> 
			    <TD><%		
		With mobjValues
			Call .Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Call .Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Call .Parameters.Add("dEffecdate", Request.QueryString.Item("dInitDate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nModulec"), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeModulecToolTip")))
		End With
		%>
				<TD><LABEL ID=0><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
				<TD><%		With mobjValues
			.Parameters.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Response.Write(mobjValues.PossiblesValues("valCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nCover"), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCoverToolTip")))
		End With
		%>
				</TD>
			</TR>
		<%	End If%>
	</TABLE>
<%	
End If
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
' Variables pasadas por QueryString desde pagina MGS001
' nInsur_area, nTableTyp, nSource, dInitDate, nIDTable, 
' nBranch, nProduct, nCurrency, nTyperec, nModulec, nCover, nIdRec, dValDate 
' nMainAction: 302 si es desde la PopUp, 401 si es desde la grilla

'Área de seguros 
Response.Write(mobjValues.HiddenControl("hddInsur_area", Session("nInsur_area")))
'Fecha de inicio 
Response.Write(mobjValues.HiddenControl("hddInitDate", Request.QueryString.Item("dInitDate")))
'Secuencia de Margin_master 
Response.Write(mobjValues.HiddenControl("hddIdtable", Request.QueryString.Item("nIdTable")))
'Secuencia de Margin_detail 
Response.Write(mobjValues.HiddenControl("hddIdrec", Request.QueryString.Item("nIdrec")))
'Fecha de valorización   
Response.Write(mobjValues.HiddenControl("hddValDate", Request.QueryString.Item("dValDate")))
'Tipo de tabla 
'    Response.Write mobjValues.HiddenControl("hddTableTyp", Request.QueryString("nTableTyp")) 
'Origen de la tabla 
'    Response.Write mobjValues.HiddenControl("hddSource", Request.QueryString("nSource")) 
'Moneda  
'    Response.Write mobjValues.HiddenControl("hddCurrency", Request.QueryString("nCurrency")) 

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMGS002Upd()
Else
	Call insPreMGS002()
End If

If Request.QueryString.Item("Type") <> "PopUp" Then
	%>  <TABLE WIDTH="100%">
				<TR>
					<TD COLSPAN="3" CLASS="HORLINE"></TD>
				</TR>
				<TR>
					<TD WIDTH=5%><%=mobjValues.ButtonAbout("MGS002")%></TD>
					<TD WIDTH=5%><%=mobjValues.ButtonHelp("MGS002")%></TD>
					<%	If mobjValues.ActionQuery Then
		mobjValues.ActionQuery = False
		%>
						<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel( , "window.close();",  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD>
						<%		mobjValues.ActionQuery = True%>
					<%	Else%>
						<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel("window.close();", "window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.All)%></TD>
					<%	End If%>
				</TR>
			</TABLE>
		<%	
End If
mobjValues = Nothing
mobjGrid = Nothing
%> 
</FORM> 
</BODY>
</HTML>






