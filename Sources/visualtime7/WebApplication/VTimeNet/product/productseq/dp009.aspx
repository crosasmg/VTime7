<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mblnDisabled As Boolean
Dim mblnDisabledCheck As Boolean
Dim mblnModule As Boolean

Dim mstrType_clause As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lstrTab_Cover As String
	Dim lclsProduct As eProduct.Product
	If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
		lstrTab_Cover = "TABLIFE_COVER"
	Else
		lstrTab_Cover = "TABGEN_COVER2"
	End If
	mobjGrid.sCodisplPage = "DP009"
	'+ Se definen las columnas del grid
	lclsProduct = New eProduct.Product
	mblnModule = lclsProduct.IsModule(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	With mobjGrid.Columns
		.AddNumericColumn(41265, GetLocalResourceObject("tcnClauseColumnCaption"), "tcnClause", 5, CStr(1),  , GetLocalResourceObject("tcnClauseColumnToolTip"),  ,  ,  ,  ,  , mblnDisabled)
		.AddTextColumn(41266, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		.AddTextColumn(41267, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		
		If mblnModule Then
			.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "insChangeModulec(this)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valModulecColumnToolTip"))
		Else
			.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , True,  , GetLocalResourceObject("valModulecColumnToolTip"))
		End If
		.AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", lstrTab_Cover, eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , mblnModule or Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCoverColumnToolTip"))
		
		
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "table7509", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypeColumnToolTip"))
		.AddCheckColumn(41268, GetLocalResourceObject("chkDefaultiColumnCaption"), "chkDefaulti", vbNullString,  ,  , "insChangeDef(this)", mblnDisabledCheck, GetLocalResourceObject("chkDefaultiColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkmodifiedColumnCaption"), "chkmodified", vbNullString,  ,  ,  , mblnDisabledCheck, GetLocalResourceObject("chkmodifiedColumnToolTip"))
		.AddNumericColumn(41269, GetLocalResourceObject("tcnOrdenColumnCaption"), "tcnOrden", 5,  ,  , GetLocalResourceObject("tcnOrdenColumnToolTip"),  ,  ,  ,  ,  , Session("bQuery"))
		
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddButtonColumn(0, GetLocalResourceObject("SCA2-GColumnCaption"), "SCA2-G", 0,  , False)
		Else
			.AddButtonColumn(0, GetLocalResourceObject("SCA2-GColumnCaption"), "SCA2-G", 0,  , True)
		End If
		
		.AddCheckColumn(0, GetLocalResourceObject("chkType_ClauseColumnCaption"), "chkType_Clause", vbNullString,  ,  , "insChangeType(this)", mblnDisabledCheck, GetLocalResourceObject("chkType_ClauseColumnToolTip"))
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			'   And _ Request.QueryString("Action") = "Add" 
			'Then
			.AddFileColumn(0, GetLocalResourceObject("tctFileColumnCaption"), "tctFile", 45,  , True,,"insSelectFile(this);")
			'.AddTextColumn 0,"Nombre","tctDoc_attach",45,strnull,,"Ruta y nombre del documento o archivo",,,,True
		End If
		
		
		If Not (Request.QueryString.Item("Type") = "PopUp") Then
			'And _Request.QueryString("Action") = "Add")
			'Then
			'mobjGrid.Columns("tctDoc_attach").GridVisible = False    
			.AddTextColumn(0, GetLocalResourceObject("tctDoc_attachColumnCaption"), "tctDoc_attach", 45, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctDoc_attachColumnToolTip"),  ,  ,  , True)
		End If
		
		
		.AddHiddenColumn("tcnAuxClause", CStr(0))
		.AddHiddenColumn("tctAuxDescript", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("tctAuxShort_des", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("tctAuxDefaulti", CStr(2))
		.AddHiddenColumn("tctAuxSel", CStr(2))
		.AddHiddenColumn("hdsCheckFile", "2")
		.AddHiddenColumn("hdsCheckDefaulti", "2")
        .AddHiddenColumn("hdsFileName", "")
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP009"
		.Top = 50
		.Width = 650
		.Height = 430
		
		.Splits_Renamed.AddSplit(0, "", 5)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("5ColumnCaption"), 5)
		
		.sQueryString = "nClause=' + self.document.forms[0].tcnClause.value + '" & "&nModulec=' + self.document.forms[0].valModulec.value + '" & "&nCover=' + self.document.forms[0].valCover.value + '"
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		Else
			.Columns("tctDescript").EditRecord = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		' .Columns("Sel").OnClick = "valClause(this);"
		If mblnModule Then
			.Columns("valModulec").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valModulec").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valModulec").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
			.Columns("valCover").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nCoverMax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			.Columns("valCover").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		.Columns("chkDefaulti").OnClick = "if(document.forms[0].tctAuxSel.length>0)document.forms[0].tctAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].tctAuxSel.value =(this.checked?1:2);"
		
	End With
End Sub
'% insPreDP009: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP009()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_Clause As eProduct.Tab_Clause
	Dim lcolTab_Clauses As eProduct.Tab_Clauses
	With Server
		lclsTab_Clause = New eProduct.Tab_Clause
		lcolTab_Clauses = New eProduct.Tab_Clauses
	End With
	If lcolTab_Clauses.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
		For	Each lclsTab_Clause In lcolTab_Clauses
			With mobjGrid
				.Columns("tcnClause").DefValue = CStr(lclsTab_Clause.nClause)
				.Columns("tctDescript").DefValue = lclsTab_Clause.sDescript
				.Columns("tctShort_des").DefValue = lclsTab_Clause.sShort_des
                    .Columns("chkDefaulti").Checked = mobjValues.StringToType(lclsTab_Clause.sDefaulti, eFunctions.Values.eTypeData.etdLong)
				.Columns("btnNotenum").nNoteNum = lclsTab_Clause.nNoteNum
				.Columns("tcnAuxClause").DefValue = CStr(lclsTab_Clause.nClause)
				.Columns("tctAuxDescript").DefValue = lclsTab_Clause.sDescript
				.Columns("tctAuxShort_des").DefValue = lclsTab_Clause.sShort_des
				.Columns("tctAuxDefaulti").DefValue = lclsTab_Clause.sDefaulti
				.Columns("valModulec").DefValue = CStr(lclsTab_Clause.nModulec)
				.Columns("tcnOrden").DefValue = CStr(lclsTab_Clause.nOrden)
				
                If Not String.IsNullOrEmpty(lclsTab_Clause.smodified) Then
                        .Columns("chkmodified").Checked = mobjValues.StringToType(lclsTab_Clause.sModified, eFunctions.Values.eTypeData.etdLong)
                End If
				
				If mblnModule Then
					.Columns("valCover").Parameters.Add("nModulec", lclsTab_Clause.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				.Columns("valCover").DefValue = CStr(lclsTab_Clause.nCover)
				.Columns("cbeType").DefValue = CStr(lclsTab_Clause.nType)
				
                    .Columns("chkType_Clause").Checked = mobjValues.StringToType(lclsTab_Clause.sType_clause, eFunctions.Values.eTypeData.etdLong)
				
				.Columns("tctDoc_attach").DefValue = lclsTab_Clause.sDoc_attach
                .Columns("hdsFileName").DefValue = lclsTab_Clause.sDoc_attach
				
				If lclsTab_Clause.sType_clause = "1" Then
					.Columns("btnNotenum").Disabled = True
				Else
					.Columns("btnNotenum").Disabled = False
				End If
				
				.sDelRecordParam = "nClause=' + marrArray[lintIndex].tcnAuxClause + '&sDescript=' + marrArray[lintIndex].tctAuxDescript + '&sShort_des=' + marrArray[lintIndex].tctAuxShort_des + '&sDefaulti=' + marrArray[lintIndex].tctAuxDefaulti + '&nNotenum=' + marrArray[lintIndex].tcnAuxNotenum + '&nModulec=' + marrArray[lintIndex].valModulec + '&nCover=' + marrArray[lintIndex].valCover + '"
				.sQueryString = "nClause=" & lclsTab_Clause.nClause
				Response.Write(.DoRow)
			End With
		Next lclsTab_Clause
	End If
	Response.Write(mobjGrid.closeTable())
	lclsTab_Clause = Nothing
	lcolTab_Clauses = Nothing
End Sub
'% insPreDP009Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP009Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_Clause As eProduct.Tab_Clause
	lclsTab_Clause = New eProduct.Tab_Clause
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		lclsTab_Clause.insPostDP009(CShort(eFunctions.Menues.TypeActions.clngActionCut), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nClause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), Request.QueryString.Item("sDefaulti"), Request.QueryString.Item("sDescript"), Request.QueryString.Item("sShort_des"), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), "D")
	End If
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valDP009Seq.aspx", "DP009", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
		End If
		
		If .QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>ConsecClause();</" & "Script>")
		End If
	End With
	
	lclsTab_Clause = Nothing
End Sub

</script>
<%Response.Expires = -1
With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
End With
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
mblnDisabledCheck = True

mobjValues.sCodisplPage = "DP009"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">



<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction = 304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP009", "DP009.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 5 $|$$Date: 28/10/03 19:29 $"

//%InsShowClauseNumber: Asigna un contador a la clausula del producto
//--------------------------------------------------------------------------------------------------
function InsShowClauseNumber(){
//--------------------------------------------------------------------------------------------------
//	self.document.forms[0].tcnClause.value = top.opener.marrArray.length + 1;

//- Se define la variable para almacenar el consecutivo más alto existente en el grid
	var llngMax    = 500
	var llngMaxUlt = 501
	    
//+ Se genera el número consecutivo del Order
	for(var llngIndex = 0;llngIndex < top.opener.marrArray.length;llngIndex++)
	    if(top.opener.marrArray[llngIndex].tcnClause>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].tcnClause

	    if(++llngMax.length > self.document.forms[0].tcnClause.maxLength){
//+ Se asignan null
			self.document.forms[0].tcnClause.value = "";
		}
		else{
//+ Se asignan el valor por defecto del Order			
			self.document.forms[0].tcnClause.value = ++llngMax;
		}
}
//%valClause: Se verifica si se puede borrar o no la cláusula
//--------------------------------------------------------------------------------------------------
//function valClause(Field){
//--------------------------------------------------------------------------------------------------//
//	if(Field.checked){
//		self.document.cmdDelete.disabled = true;
//		insDefValues('DeleteDP009', 'nClause=' + marrArray[Field.value].tcnClause + '&nIndex=' + Field.value)
//	}
//}
//%ConsecClause: Trae el consecutivo de la clausula
//--------------------------------------------------------------------------------------------------
function ConsecClause(Field){
//--------------------------------------------------------------------------------------------------
	
	    
	    //insDefValues("ConsecuClause", 'nClause=' + marrArray[Field.value].tcnClause + '&nIndex=' + Field.value,'/VTimeNet/product/productseq')
	    insDefValues('ConsecuClause','ConsecuClause' ,'/VTimeNet/product/productseq')
		
}
//%insChangeModulec: se controla el cambio de valor del campo "Módulo"
//--------------------------------------------------------------------------------------------------
function insChangeModulec(Field){
//--------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
<%
If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
	%>
		valCover.Parameters.Param4.sValue=Field.value;
<%Else%>
		valCover.Parameters.Param5.sValue=Field.value;
<%End If
If Request.QueryString.Item("Action") <> "Update" Then
	%>
		valCover.value="";
		UpdateDiv("valCoverDesc","");
		valCover.disabled=(Field.value=="" || Field.value==0)?true:false;
		btnvalCover.disabled=(Field.value=="" || Field.value==0)?true:false;
<%End If%>
	}
}

//%insChangeType: se controla el cambio de tipo de cláusula según archivo
//--------------------------------------------------------------------------------------------------
function insChangeType(Field){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    
        if(Field.checked){
            hdsCheckFile.value = "1";
            tctFile.disabled = false;
            btnNotenum.disabled = true;
        } else {
            hdsCheckFile.value = "2";
            tctFile.disabled = true;
            tctFile.value = "";
            btnNotenum.disabled = false;
        }
    }
}

//%insChangeDef: 
//--------------------------------------------------------------------------------------------------
function insChangeDef(Field){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    
        if(Field.checked){
            hdsCheckDefaulti.value = "1";
        } else {
            hdsCheckDefaulti.value = "2";
        }
    }
}

//%insChangeDef: 
//--------------------------------------------------------------------------------------------------
function insSelectFile(Field){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    var fullPath = Field.value;    
    var filename;

        if (fullPath) { 
                var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/')); 
                var filename = fullPath.substring(startIndex); 
                if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) { 
                        filename = filename.substring(1); 
                } 
                alert(filename); 
                hdsFileName.value = filename; 
        } 

    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="fraContent" ACTION="valDP009Seq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" ENCTYPE="multipart/form-data">
<%

Response.Write(mobjValues.ShowWindowsName("DP009"))
With Request
	If .QueryString.Item("Action") = "Update" Then
		mblnDisabled = True
		mblnDisabledCheck = False
	ElseIf .QueryString.Item("Action") = "Add" Then 
		mblnDisabledCheck = False
		mblnDisabled = True
	End If
	Call insDefineHeader()
	If .QueryString.Item("Type") = "PopUp" Then
		Call insPreDP009Upd()
	Else
		Call insPreDP009()
	End If
End With
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>





