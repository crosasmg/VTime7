<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim mstrFieldtype As String



'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lclsFieldSheetId As eInterface.FieldSheet
	
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "MGI1407"
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnfieldColumnCaption"), "tcnfield", 5, "", True, GetLocalResourceObject("tcnfieldColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsfielddescColumnCaption"), "tcsfielddesc", 30, vbNullString,  , GetLocalResourceObject("tcsfielddescColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbestableColumnCaption"), "cbestable", "TABTABLESHEET", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , False,  , GetLocalResourceObject("cbestableColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcscolumnnameColumnCaption"), "tcscolumnname", 30, vbNullString,  , GetLocalResourceObject("tcscolumnnameColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcsvalueColumnCaption"), "tcsvalue", 40, vbNullString,  , GetLocalResourceObject("tcsvalueColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcsrutineColumnCaption"), "tcsrutine", 40, vbNullString,  , GetLocalResourceObject("tcsrutineColumnToolTip"))
		If mstrFieldtype = "1" Or mstrFieldtype = "3" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tcsfieldcommenColumnCaption"), "tcsfieldcommen", 50, vbNullString,  , GetLocalResourceObject("tcsfieldcommenColumnToolTip"))
		Else
			Call .AddHiddenColumn("tcsfieldcommen", vbNullString)
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnfieldorderColumnCaption"), "tcnfieldorder", 5, "", True, GetLocalResourceObject("tcnfieldorderColumnToolTip"), False, 0)
		If mstrFieldtype = "2" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnrowdorderColumnCaption"), "tcnrowdorder", 5, "", True, GetLocalResourceObject("tcnrowdorderColumnToolTip"), False, 0)
		Else
			Call .AddHiddenColumn("tcnrowdorder", vbNullString)
		End If
		If mstrFieldtype = "2" Or mstrFieldtype = "3" Then
			If mstrFieldtype = "2" Then
				Call .AddTextColumn(0, GetLocalResourceObject("tcsvalueslistColumnCaption"), "tcsvalueslist", 30, vbNullString,  , GetLocalResourceObject("tcsvalueslistColumnToolTip"))
			Else
				Call .AddTextColumn(0, GetLocalResourceObject("tcsvalueslistColumnCaption"), "tcsvalueslist", 80, vbNullString,  , GetLocalResourceObject("tcsvalueslistColumnToolTip"))
			End If
		Else
			Call .AddHiddenColumn("tcsvalueslist", vbNullString)
		End If
		If mstrFieldtype = "3" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenobjtypeColumnCaption"), "cbenobjtype", "Table5703", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbenobjtypeColumnToolTip"))
		Else
			Call .AddHiddenColumn("cbenobjtype", vbNullString)
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbendatatypeColumnCaption"), "cbendatatype", "Table324", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbendatatypeColumnToolTip"))
		If mstrFieldtype <> "1" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnfieldlargeColumnCaption"), "tcnfieldlarge", 5, "", True, GetLocalResourceObject("tcnfieldlargeColumnToolTip"), False, 0)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnDecimalColumnCaption"), "tcnDecimal", 5, "", True, GetLocalResourceObject("tcnDecimalColumnToolTip"), False, 0)
		Else
			Call .AddHiddenColumn("tcnfieldlarge", vbNullString)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnDecimalColumnCaption"), "tcnDecimal", 5, vbNullString)
		End If
		If mstrFieldtype = "2" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbentablehomoColumnCaption"), "cbentablehomo", "Table5706", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbentablehomoColumnToolTip"))
		Else
			Call .AddHiddenColumn("cbentablehomo", vbNullString)
		End If
		If mstrFieldtype = "2" Or mstrFieldtype = "3" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chksobligatoryColumnCaption"), "chksobligatory", "", 1, "1",  , False, GetLocalResourceObject("chksobligatoryColumnToolTip"))
		Else
			Call .AddHiddenColumn("chksobligatory", vbNullString)
		End If
		If CStr(session("nIntertype")) = "2" Then
			If mstrFieldtype <> "2" Then
				Call .AddCheckColumn(0, GetLocalResourceObject("chkslastmoveColumnCaption"), "chkslastmove", "", 2, "1",  , False, GetLocalResourceObject("chkslastmoveColumnCaption"))
			Else
				Call .AddCheckColumn(0, GetLocalResourceObject("chkslastmoveColumnCaption"), "chkslastmove", "", 2, "1",  , True, GetLocalResourceObject("chkslastmoveColumnCaption"))
			End If
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkslastmoveColumnCaption"), "chkslastmove", "", 2, "1",  , True, GetLocalResourceObject("chkslastmoveColumnCaption"))
		End If
		If mstrFieldtype = "1" Or mstrFieldtype = "3" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenoperatorColumnCaption"), "cbenoperator", "table5704", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull), False,  ,  ,  ,  , False,  , GetLocalResourceObject("cbenoperatorColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tcsfieldrelColumnCaption"), "tcsfieldrel", 50, vbNullString,  , GetLocalResourceObject("tcsfieldrelColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenconditColumnCaption"), "cbencondit", "table5704", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull), False,  ,  ,  ,  , False,  , GetLocalResourceObject("cbenconditColumnToolTip"))
		Else
			Call .AddHiddenColumn("cbenoperator", vbNullString)
			Call .AddHiddenColumn("tcsfieldrel", vbNullString)
			Call .AddHiddenColumn("cbencondit", vbNullString)
		End If
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MGI1407"
		.Left = 200
		.Top = 70
		.Width = 570
		.Height = 550
		
		If Request.QueryString.Item("Action") <> "Update" And Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcnfield").EditRecord = True
		End If
		
		'+Si es popup e ingreso, trae correlativo(nField) por nSheet desde tabla FieldSheet
		If Request.QueryString.Item("Type") = "PopUp" Then
			lclsFieldSheetId = New eInterface.FieldSheet
			.Columns("tcnfield").DefValue = CStr(lclsFieldSheetId.InsCalFieldSheetId(session("nSheet")))
			lclsFieldSheetId = Nothing
		End If
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("chksobligatory").Disabled = False
			If CStr(session("nIntertype")) = "2" Then
				If mstrFieldtype <> "2" Then
					.Columns("chkslastmove").Disabled = False
				Else
					.Columns("chkslastmove").Disabled = True
				End If
			End If
		Else
			.Columns("chksobligatory").Disabled = True
			.Columns("chkslastmove").Disabled = True
		End If
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401") 'Or Request.QueryString("nMainAction") = "")				
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		'+ armo querystring para pasar a pop up
		.sEditRecordParam = "nFieldType=" & mstrFieldtype
		.sDelRecordParam = "nField=' + marrArray[lintIndex].tcnfield + '"
		
		'+Parametros para posibles values de un NO tableXXX.
		.Columns("cbestable").Parameters.Add("nSheet", session("nSheet"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbenobjtype").BlankPosition = False
		
		'+Seteo para mostrar o excluir registros para el Table5704, campos relacion y condicion.
		.Columns("cbenoperator").List = "1,2,3,4,5,6,7,8"
		.Columns("cbenoperator").TypeList = 1 'Incluir
		.Columns("cbencondit").List = "1,2,3,4,5,6,7,8"
		.Columns("cbencondit").TypeList = 2 'Excluir
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMGI1407: Esta función se encarga de cargar los datos en la forma "Folder" de la SEC2
'------------------------------------------------------------------------------
Private Sub insPreMGI1407()
	'------------------------------------------------------------------------------
	Dim lcolfieldsheet As eInterface.FieldSheets
	Dim lclsfieldsheet As eInterface.FieldSheet
	
	lcolfieldsheet = New eInterface.FieldSheets
	lclsfieldsheet = New eInterface.FieldSheet
	
	If lcolfieldsheet.Find(mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrFieldtype, eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsfieldsheet In lcolfieldsheet
			With mobjGrid
				
				.Columns("tcnfield").DefValue = CStr(lclsfieldsheet.nfield)
				.Columns("tcsfielddesc").DefValue = lclsfieldsheet.sfielddesc
				.Columns("cbestable").DefValue = lclsfieldsheet.stable
				.Columns("tcscolumnname").DefValue = lclsfieldsheet.scolumnname
				.Columns("tcsvalue").DefValue = lclsfieldsheet.svalue
				.Columns("tcsrutine").DefValue = lclsfieldsheet.srutine
				.Columns("tcsfieldcommen").DefValue = lclsfieldsheet.sfieldcommen
				.Columns("tcnfieldorder").DefValue = CStr(lclsfieldsheet.nfieldorder)
				.Columns("tcnrowdorder").DefValue = CStr(lclsfieldsheet.nroworder)
				.Columns("tcsvalueslist").DefValue = lclsfieldsheet.svalueslist
				.Columns("cbenobjtype").DefValue = CStr(lclsfieldsheet.nobjtype)
				.Columns("cbendatatype").DefValue = CStr(lclsfieldsheet.ndatatype)
				.Columns("tcnfieldlarge").DefValue = CStr(lclsfieldsheet.nfieldlarge)
				.Columns("tcnDecimal").DefValue = CStr(lclsfieldsheet.ndecimal)
				.Columns("cbentablehomo").DefValue = CStr(lclsfieldsheet.ntablehomo)
				'+MANEJO DE CAMPOS CHECK
				If CDbl(lclsfieldsheet.sobligatory) = 1 Then
					.Columns("chksobligatory").Checked = 1
				Else
					.Columns("chksobligatory").Checked = 2
				End If
				.Columns("cbenoperator").DefValue = CStr(lclsfieldsheet.noperator)
				If CDbl(lclsfieldsheet.slastmove) = 1 Then
					.Columns("chkslastmove").Checked = 1
				Else
					.Columns("chkslastmove").Checked = 2
				End If
				.Columns("tcsfieldrel").DefValue = lclsfieldsheet.sfieldrel
				.Columns("cbencondit").DefValue = CStr(lclsfieldsheet.ncondit)
				
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsfieldsheet
	End If
	Response.Write(mobjGrid.closeTable())
	lcolfieldsheet = Nothing
	lclsfieldsheet = Nothing
End Sub

'%insPreMGI1407B: Esta función se encarga de crear campo solo en pagino y no en popup
'--------------------------------------------------------------------------------------------
Private Sub insPreMGI1407B()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write(" <TABLE WIDTH=""30%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><label>" & GetLocalResourceObject("cbenfieldtypeCaption") & "</label></td>" & vbCrLf)
Response.Write("		<TD>")

	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbenfieldtype", "Table5702", eFunctions.Values.eValuesType.clngComboType, mstrFieldtype,  ,  ,  ,  ,  , "ReloadPage()", False,  , GetLocalResourceObject("cbenfieldtypeToolTip"),  , 14))
Response.Write("</td>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>")

	
End Sub

'% insPreMGI1407Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreMGI1407_Upd()
	'---------------------------------------------------------------------------------------
	Dim lclsfieldsheet As eInterface.FieldSheet
	lclsfieldsheet = New eInterface.FieldSheet
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		Call lclsfieldsheet.insPostMGI1407("Del", mobjValues.StringToType(session("nSheet"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nField"), eFunctions.Values.eTypeData.etdLong, True), eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strnull), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strnull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull))
	End If
	
	lclsfieldsheet = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valmantinterfaceseq.aspx", "MGI1407", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGI1407"
%>
<HTML>
<HEAD>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("MGI1407")%>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MGI1407", "MGI1407.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<script>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 3 $|$$Date: 21/10/09 6:17p $"

    //-------------------------------------------------------------------------------------------------------------------
    function insStateZone() { }

    //-------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------
    function insPreZone(llngAction) {
        //-------------------------------------------------------------------------------------------------------------------
        switch (llngAction) {
            case 301:
            case 302:
            case 401:
                document.location.href = document.location.href.replace(/&nMainAction.*/, '') + '&nMainAction=' + llngAction
                break;
        }
    }
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //------------------------------------------------------------------------------------------
    function insFinish() {
        //------------------------------------------------------------------------------------------
        return true;

    }

    /*% ReloadPage: Recarga la página tras  cambiar valores en combos
    /*---------------------------------------------------------------------------------------------------------*/
    function ReloadPage() {
        /*---------------------------------------------------------------------------------------------------------*/
        var lstrstring = "";

        with (self.document.forms[0]) {
            lstrstring += document.location;
            lstrstring = lstrstring.replace(/&nFieldType=.*/, "");
            lstrstring = lstrstring + "&nFieldType=" + cbenfieldtype.value + "&reload=2";
        }
        document.location = lstrstring;
    }

</SCRIPT>		

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>

<FORM METHOD="POST" ID="FORM" NAME="MGI1407" ACTION="valmantinterfaceseq.aspx?mode=2">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MGI1407"))
If IsNothing(Request.QueryString.Item("nFieldType")) Then
	mstrFieldtype = "2"
Else
	mstrFieldtype = Request.QueryString.Item("nFieldType")
End If

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGI1407B()
	Call insPreMGI1407()
Else
	Call insPreMGI1407_Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





