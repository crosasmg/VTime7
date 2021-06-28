<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objetos genéricos para manejo de valores, menú y grilla.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
Dim mstrExist_Modul As String


'%insDefineHeader: Definición de las columnas del Grid.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Dim sTypetable As String
	mobjGrid = New eFunctions.Grid
	
	If Request.QueryString.Item("nTypetable") = "2" Then
		sTypetable = "Prima básica pagada "
	Else
		sTypetable = "Año póliza "
	End If
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_commColumnCaption"), "cbeType_comm", "Table5662", eFunctions.Values.eValuesType.clngComboType, "0", False,  ,  ,  , "insDisabled(this)", Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeType_commColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicy_year_iniColumnCaption"), "tcnPolicy_year_ini", 5,  ,  , GetLocalResourceObject("tcnPolicy_year_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicy_year_endColumnCaption"), "tcnPolicy_year_end", 5,  ,  , GetLocalResourceObject("tcnPolicy_year_endColumnToolTip"))
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_initColumnCaption"), "tcnAge_init", 5,  ,  , GetLocalResourceObject("tcnAge_initColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 5,  ,  , GetLocalResourceObject("tcnAge_endColumnToolTip"))
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, "0", True,  ,  ,  , "InsChangeField(this,""Module"");", mstrExist_Modul = "0",  , GetLocalResourceObject("valModulecColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCoverColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommiss_PctColumnCaption"), "tcnCommiss_Pct", 8, vbNullString, True, GetLocalResourceObject("tcnCommiss_PctColumnCaption"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMax_AmountColumnCaption"), "tcnMax_Amount", 18,  , True, GetLocalResourceObject("tcnMax_AmountColumnToolTip"),  , 6,  ,  , "InsChangeField(this,""Max_Amount"");")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		
		Call .AddHiddenColumn("dEffecdate", vbNullString)
		Call .AddHiddenColumn("dNulldate", vbNullString)
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("nPolicy_year_end_Aux", vbNullString)
		Call .AddHiddenColumn("nSlc_Tab_nr", vbNullString)
		Call .AddHiddenColumn("tctExist_Modul", mstrExist_Modul)
		Call .AddHiddenColumn("tcnId", "0")
		Call .AddHiddenColumn("hddnTypetable", Request.QueryString.Item("nTypetable"))
		
	End With
	
	With mobjGrid
		
		.Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valCover").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG7000"
		.sCodisplPage = "MAG7000"
		.Columns("cbeType_comm").EditRecord = True
		.AddButton = True
		.DeleteButton = True
		.Height = 400
		.Width = 450
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.sEditRecordParam = "nSlc_Tab_nr=" & Request.QueryString.Item("nSlc_Tab_nr") & "&nTypetable=" & Request.QueryString.Item("nTypetable")
		
		.Columns("tcnAge_init").GridVisible = Request.QueryString.Item("nTypetable") = "3"
		.Columns("tcnAge_end").GridVisible = Request.QueryString.Item("nTypetable") = "3"
		
		.Columns("tcnAge_init").PopUpVisible = Request.QueryString.Item("nTypetable") = "3"
		.Columns("tcnAge_end").PopUpVisible = Request.QueryString.Item("nTypetable") = "3"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% reaModules: Verifica la existencia de modulos para el producto
'-----------------------------------------------------------------------------
Private Function reaModules() As Object
	'-----------------------------------------------------------------------------
	Dim lclsTab_moduls As eProduct.Tab_moduls
	lclsTab_moduls = New eProduct.Tab_moduls
	If lclsTab_moduls.Find(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		mstrExist_Modul = "1"
	Else
		mstrExist_Modul = "0"
	End If
	lclsTab_moduls = Nothing
End Function

'% insPreMAG7000: Muestra la grilla con datos.
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG7000()
	'--------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insPreZone: Define ubicación del documento." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lintCount As Short
	Dim lobjObject As Object
	Dim lcolTab_Spec_Comms As eAgent.Tab_Spec_Comms
	
	lcolTab_Spec_Comms = New eAgent.Tab_Spec_Comms
	
	If lcolTab_Spec_Comms.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), CInt(Session("nSlc_Tab_nr")), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = 0
		For	Each lobjObject In lcolTab_Spec_Comms
			With lobjObject
				mobjGrid.Columns("tcnCommiss_Pct").DefValue = .nCommiss_Pct
				mobjGrid.Columns("dEffecdate").DefValue = .dEffecdate
				mobjGrid.Columns("dNulldate").DefValue = .dNulldate
				mobjGrid.Columns("cbeType_comm").DefValue = .nType_comm
				mobjGrid.Columns("tcnPolicy_year_ini").DefValue = .nPolicy_year_ini
				mobjGrid.Columns("tcnPolicy_year_end").DefValue = .nPolicy_year_end
				mobjGrid.Columns("valModulec").DefValue = .nModulec
				mobjGrid.Columns("valCover").DefValue = .nCover
				mobjGrid.Columns("sParam").DefValue = "nSlc_Tab_nr=" & .nSlc_Tab_nr & "&nType_comm=" & .nType_comm & "&nPolicy_year_ini=" & .nPolicy_year_ini & "&nPolicy_year_end=" & .nPolicy_year_end & "&nId=" & .nId
				mobjGrid.Columns("nPolicy_year_end_Aux").DefValue = .nPolicy_year_end
				mobjGrid.Columns("tctExist_Modul").DefValue = mstrExist_Modul
				mobjGrid.Columns("tcnId").DefValue = .nId
				mobjGrid.Columns("cbeCurrency").DefValue = .nCurrency
				mobjGrid.Columns("tcnMax_Amount").DefValue = .nMax_Amount
				mobjGrid.Columns("tcnAge_init").DefValue = .nAge_init
				mobjGrid.Columns("tcnAge_end").DefValue = .nAge_end
				
				If .dNulldate <> eRemoteDB.Constants.dtmNull Then
					mobjGrid.Columns("Sel").Disabled = True
					mobjGrid.Columns("cbeType_comm").EditRecord = False
				Else
					mobjGrid.Columns("Sel").Disabled = False
					mobjGrid.Columns("cbeType_comm").EditRecord = True
				End If
				
				If .nMax_Amount = 0 Or .nMax_Amount = eRemoteDB.Constants.intNull Then
					mobjGrid.Columns("cbeCurrency").Disabled = False
				Else
					mobjGrid.Columns("cbeCurrency").Disabled = True
				End If
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 1000 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolTab_Spec_Comms = Nothing
	lobjObject = Nothing
End Sub

'% insPreMAG7000Upd: Muestra ventana para actualizar registros.
'-----------------------------------------------------------------------------------------
Private Sub insPreMAG7000Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsTab_Spec_Comm As eAgent.Tab_Spec_Comm
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTab_Spec_Comm = New eAgent.Tab_Spec_Comm
		
		If lclsTab_Spec_Comm.insPostMAG7000("Del", CInt(Request.QueryString.Item("nSlc_Tab_nr")), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), CInt(Request.QueryString.Item("nPolicy_year_ini")), CInt(Request.QueryString.Item("nPolicy_year_end")), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CInt(Request.QueryString.Item("nType_comm")), CInt(Request.QueryString.Item("nId")), CDbl(Request.QueryString.Item("nCurrency")), CDbl(Request.QueryString.Item("nMax_Amount")), mobjValues.StringToType(Request.Form.Item("hddnTypetable"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
			
			Response.Write(mobjValues.ConfirmDelete())
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
		End If
		
		lclsTab_Spec_Comm = Nothing
	Else
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	End If
End Sub

</script>
<%Response.Expires = -1

'- Nombre de tabla general.

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG7000"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>

//- Variable para el control de versiones

    document.VssVersion="$$Revision: 5 $|$$Date: 5/07/04 22:25 $|$$Author: Nvaplat22 $"

//% insCancel: Ejecuta la acción del botón cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone: Habilita o deshabilita los controles.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(vObj, sField){
//--------------------------------------------------------------------------------------------    
	var sValue;
	
	sValue = vObj.value;
	if (vObj.disabled==false) {
	with (self.document.forms[0]){
		switch (sField){
			case 'Module':
				valCover.Parameters.Param4.sValue=sValue;
				break;
			case 'Max_Amount':
			    if (vObj.value=='' || vObj.value==0){
					cbeCurrency.disabled=true;
					cbeCurrency.value='';
				}
				else{
					cbeCurrency.disabled=false;
				}
				break;
		}
	}
	}
	else{
	    vObj.value=0;
	}    
}
//'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
//% insDisabled: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function insDisabled(vObj){
//--------------------------------------------------------------------------------------------
    var sValue = vObj.value;
    with (self.document.forms[0]){
		switch (sValue){
		    case '1':
		        valCover.disabled=false;
		        btnvalCover.disabled=false;
		        break;
		    default:
		        valCover.disabled=true;
		        btnvalCover.disabled=true;
		        valCover.value='';
		        $(valCover).change();
		}
    }
}
</SCRIPT>
<%Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))

With Response
	.Write(mobjValues.StyleSheet())
	
	.Write("<SCRIPT>var sAction='" & Request.QueryString.Item("Action") & "'</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MAG7000", "MAG7000"))
		
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MS7000" ACTION="valMantAgent.aspx?mode=1">
<%

'+ '+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
'+ Busqueda de los modulos de un producto
Call reaModules()

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMAG7000()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMAG7000Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






