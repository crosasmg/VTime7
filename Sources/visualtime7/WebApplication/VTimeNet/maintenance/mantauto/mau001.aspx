<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGridTab_au_val As eFunctions.Grid
Dim mobjGridVeh_Allow As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenu As eFunctions.Menues

'-Objeto para recuperar la información de la página
Dim mclsTab_au_veh As eBranches.Tab_au_veh


'% insDefineHeaderTab_au_val:Este procedimiento se encarga de definir las columnas del grid
'% de valoresde vehículos, y de habilitar o inhabilitar los botones de añadir y eliminar.
'-------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderTab_au_val()
	'-------------------------------------------------------------------------------------------------------------------
	mobjGridTab_au_val = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid
	mobjGridTab_au_val.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	mobjGridTab_au_val.sArrayName = "marrVeh_va"
	With mobjGridTab_au_val.Columns
            .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString, , GetLocalResourceObject("tcnYearColumnToolTip"), , , , , , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGridTab_au_val
		.Codispl = "MAU001"
		.Codisp = "MAU001"
		.sCodisplPage = "MAU001"
		.Columns("tcnCapital").EditRecord = True
		mobjGridTab_au_val.ActionQuery = mobjValues.ActionQuery
		'+ Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "sInGrid=1&sVehcode=" & Request.QueryString.Item("sVehcode") & "&nYear='+marrVeh_va[lintIndex].tcnYear + '"
		.Height = 180
		.Width = 350
		.sEditRecordParam = "sInGrid=1&sVehcode=" & Request.QueryString.Item("sVehcode")
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sInGrid") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insDefineHeaderVeh_allow:Este procedimiento se encarga de definir las columnas del grid 
'% de vehículos permitidos e inhabilitar los botones de añadir y eliminar.
'-------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderVeh_allow()
	'-------------------------------------------------------------------------------------------------------------------
	mobjGridVeh_Allow = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid
	mobjGridVeh_Allow.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	mobjGridVeh_Allow.sArrayName = "marrVa_allow"
	With mobjGridVeh_Allow.Columns
		.AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas generales del Grid
	With mobjGridVeh_Allow
		.Codispl = "MAU001"
		.Codisp = "MAU001"
		mobjGridVeh_Allow.ActionQuery = mobjValues.ActionQuery
		
		'+ Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "sInGrid=2&sVehcode=" & Request.QueryString.Item("sVehcode") & "&nBranch=' +marrVa_allow[lintIndex].cbeBranch + '" & "&nProduct=' +marrVa_allow[lintIndex].valProduct + '"
		.Height = 180
		.Width = 350
		.sEditRecordParam = "sInGrid=2&sVehcode=" & Request.QueryString.Item("sVehcode")
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sInGrid") = "2" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMAU001Tab_au_val: Se cargan los datos iniciales de la página de la parte repetitiva 1
'-------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAU001Tab_au_val()
	'-------------------------------------------------------------------------------------------------------------------
    Dim lclsTab_au_val As eBranches.Tab_au_val
	
	With mobjGridTab_au_val
		If mclsTab_au_veh.mcolTab_au_val.Count > 0 Then
			For	Each lclsTab_au_val In mclsTab_au_veh.mcolTab_au_val
				.Columns("tcnYear").DefValue = CStr(lclsTab_au_val.nYear)
				.Columns("tcnCapital").DefValue = CStr(lclsTab_au_val.nCapital)
				Response.Write(.DoRow)
			Next lclsTab_au_val
		End If
		Response.Write(.closeTable)
	End With
	Response.Write("<SCRIPT>document.forms[0].action=document.forms[0].action + '&nCountTab_au_val=" & mclsTab_au_veh.mcolTab_au_val.Count & "'</" & "Script>")
End Sub

'%insPreMAU001Veh_allow: Se cargan los datos iniciales de la página de la parte repetitiva 1
'-------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAU001Veh_allow()
	'-------------------------------------------------------------------------------------------------------------------
	Dim lclsVeh_allow As eBranches.Veh_allow
	
	With mobjGridVeh_Allow
		If mclsTab_au_veh.mcolVeh_allow.Count > 0 Then
			For	Each lclsVeh_allow In mclsTab_au_veh.mcolVeh_allow
				.Columns("cbeBranch").DefValue = CStr(lclsVeh_allow.nBranch)
				.Columns("valProduct").DefValue = CStr(lclsVeh_allow.nProduct)
				Response.Write(.DoRow)
			Next lclsVeh_allow
		End If
		Response.Write(.closeTable)
	End With
End Sub

'% insPreVi662Upd: Se realiza el manejo de la ventana PopUp asociada a los diferentes grid
'------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAU001Upd()
	'------------------------------------------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsTab_au_veh.InsPostMAU001Upd(.QueryString.Item("Action"), .QueryString.Item("sInGrid"), .QueryString.Item("sVehcode"), mobjValues.StringToType(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
		End If
		If .QueryString.Item("sInGrid") = "1" Then
			Response.Write(mobjGridTab_au_val.DoFormUpd(.QueryString.Item("Action"), "ValMantAuto.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGridVeh_Allow.ActionQuery, CShort(.QueryString.Item("Index"))))
		Else
			Response.Write(mobjGridVeh_Allow.DoFormUpd(.QueryString.Item("Action"), "ValMantAuto.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGridVeh_Allow.ActionQuery, CShort(.QueryString.Item("Index"))))
		End If
	End With
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
Response.Write("    var sStatregt" & vbCrLf)
Response.Write("    var nVehBrand" & vbCrLf)
Response.Write("    var sVehmodel" & vbCrLf)
Response.Write("    var sDescript" & vbCrLf)
Response.Write("    var nVehType" & vbCrLf)
Response.Write("    var nVehplace" & vbCrLf)
Response.Write("    var nVehpma" & vbCrLf)
Response.Write("    var nNational    " & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].cbeStatregt)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.sStatregt = top.opener.document.forms[0].cbeStatregt.value" & vbCrLf)
Response.Write("        top.opener.top.bStatregt = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].cbeVehbrand)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nVehBrand = top.opener.document.forms[0].cbeVehbrand.value" & vbCrLf)
Response.Write("        top.opener.top.bBrand = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].tctVehmodel)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.sVehmodel = top.opener.document.forms[0].tctVehmodel.value" & vbCrLf)
Response.Write("        top.opener.top.bModel = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].tctDescript)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.sDescript = top.opener.document.forms[0].tctDescript.value" & vbCrLf)
Response.Write("        top.opener.top.bDescript = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].cbeVehtype)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nVehType = top.opener.document.forms[0].cbeVehtype.value" & vbCrLf)
Response.Write("        top.opener.top.bType = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].tcnVehplace)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nVehplace = top.opener.document.forms[0].tcnVehplace.value" & vbCrLf)
Response.Write("        top.opener.top.bPlace = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].tcnVehpma)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nVehpma = top.opener.document.forms[0].tcnVehpma.value" & vbCrLf)
Response.Write("        top.opener.top.bPma = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].tcnVehpma)!=""undefined""){    " & vbCrLf)
Response.Write("        top.opener.top.nNational = top.opener.document.forms[0].chkNational.checked" & vbCrLf)
Response.Write("        top.opener.top.bNational = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("</" & "SCRIPT>    ")

	
End Sub

'% InsPreMAU001: Esta función permite realizar la lectura de la tabla principal de la transacción. 
'---------------------------------------------------------------------------------------------------
Private Sub InsPreMAU001()
	'---------------------------------------------------------------------------------------------------
	Call mclsTab_au_veh.InsPreMAU001(Request.QueryString.Item("sVehcode"))
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeStatregtCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

	
	mobjValues.BlankPosition = False
	mobjValues.TypeList = CShort("2")
	mobjValues.List = "2"
	Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsTab_au_veh.sStatregt,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeVehbrandCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeVehbrand", "Table7042", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_au_veh.nVehBrand),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeVehbrandToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tctVehmodelCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctVehmodel", 20, mclsTab_au_veh.sVehmodel,  , GetLocalResourceObject("tctVehmodelToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeVehtypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeVehtype", "Table226", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTab_au_veh.nVehType),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeVehtypeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnVehplaceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


        Response.Write(mobjValues.TextControl("tcnVehplace", 2, mobjValues.TypeToString(mclsTab_au_veh.nVehplace, Values.eTypeData.etdInteger), , GetLocalResourceObject("tcnVehplaceToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnVehpmaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


        Response.Write(mobjValues.TextControl("tcnVehpma", 5, mobjValues.TypeToString(mclsTab_au_veh.nVehpma, Values.eTypeData.etdInteger), , GetLocalResourceObject("tcnVehpmaToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkNational", GetLocalResourceObject("chkNationalCaption"), CStr(mclsTab_au_veh.nNational), "1",  ,  ,  , GetLocalResourceObject("chkNationalToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>            " & vbCrLf)
Response.Write("        <TD CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        <TD CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>  " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("            <DIV ID=""Scroll"" STYLE=""width:250;height:140;overflow:auto; outset gray"">" & vbCrLf)
Response.Write("            ")

	Call insPreMAU001Tab_au_val()
Response.Write("" & vbCrLf)
Response.Write("            </DIV>" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("            <DIV ID=""Scroll"" STYLE=""width:250;height:140;background-color:ivory;overflow:auto; outset gray"">" & vbCrLf)
Response.Write("            ")

	Call insPreMAU001Veh_allow()
Response.Write("" & vbCrLf)
Response.Write("            </DIV>" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	mclsTab_au_veh = Nothing
	Response.Write("<SCRIPT>")
	Response.Write("with (document.forms[0]){")
	Response.Write("if (top.bStatregt) cbeStatregt.value = top.sStatregt;")
	Response.Write("if (top.bBrand) cbeVehbrand.value = top.nVehBrand;")
	Response.Write("if (top.bModel) tctVehmodel.value = top.sVehmodel;")
	Response.Write("if (top.bDescript) tctDescript.value = top.sDescript;")
	Response.Write("if (top.bType) cbeVehtype.value = top.nVehType;")
	Response.Write("if (top.bPlace) tcnVehplace.value = top.nVehplace;")
	Response.Write("if (top.bPma) tcnVehpma.value = top.nVehpma;")
	Response.Write("if (top.bNational) chkNational.checked = top.nNational;")
	Response.Write("}")
	Response.Write("</" & "Script>")
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsTab_au_veh = New eBranches.Tab_au_veh
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCut)
mobjValues.sCodisplPage = "MAU001"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Response.Write(mobjMenu.setZone(2, "MAU001", ""))
	mobjMenu = Nothing
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:57 $|$$Author: Nvaplat18 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAutoTable" ACTION="ValMantAuto.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sVehcode=<%=Request.QueryString.Item("sVehcode")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeaderTab_au_val()
Call insDefineHeaderVeh_allow()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call InsPreMAU001()
Else
	Call insPreMAU001Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




