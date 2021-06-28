<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mclsComm_mod As eProduct.Comm_mod
Dim mclsProduct As eProduct.Product
Dim mintIsModule As Byte


'% insDefineHeader : Configura los datos del grid
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctModulec_excColumnCaption"), "tctModulec_exc", 30, "",  , GetLocalResourceObject("tctModulec_excColumnToolTip"))
			Call .AddHiddenColumn("valModulec_exc", "")
			Call .AddTextColumn(0, GetLocalResourceObject("tctCover_excColumnCaption"), "tctCover_exc", 30, "",  , GetLocalResourceObject("tctCover_excColumnToolTip"))
			Call .AddHiddenColumn("valCover_exc", "")
			Call .AddTextColumn(0, GetLocalResourceObject("tctRole_excColumnCaption"), "tctRole_exc", 30, "",  , GetLocalResourceObject("tctRole_excColumnToolTip"))
			Call .AddHiddenColumn("valRole_exc", "")
			
			Call .AddTextColumn(0, GetLocalResourceObject("tctModulec_incColumnCaption"), "tctModulec_inc", 30, "",  , GetLocalResourceObject("tctModulec_incColumnToolTip"))
			Call .AddHiddenColumn("valModulec_inc", "")
			Call .AddTextColumn(0, GetLocalResourceObject("tctCover_incColumnCaption"), "tctCover_inc", 30, "",  , GetLocalResourceObject("tctCover_incColumnToolTip"))
			Call .AddHiddenColumn("valCover_inc", "")
			Call .AddTextColumn(0, GetLocalResourceObject("tctRole_incColumnCaption"), "tctRole_inc", 30, "",  , GetLocalResourceObject("tctRole_incColumnToolTip"))
			Call .AddHiddenColumn("valRole_inc", "")
			Call .AddTextColumn(0, GetLocalResourceObject("tctType_CommColumnCaption"), "tctType_Comm", 30, "",  , GetLocalResourceObject("tctType_CommColumnToolTip"))
			Call .AddHiddenColumn("cbeType_Comm", "")
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulec_excColumnCaption"), "valModulec_exc", "TabModulec", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this," & mintIsModule & ")", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valModulec_excColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCover_excColumnCaption"), "valCover_exc", "Tab_Cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this," & mintIsModule & ")", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valCover_excColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valRole_excColumnCaption"), "valRole_exc", "Tabtab_Covrol3", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this," & mintIsModule & ")", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valRole_excColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulec_incColumnCaption"), "valModulec_inc", "TabModulec", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this," & mintIsModule & ")", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valModulec_incColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCover_incColumnCaption"), "valCover_inc", "Tab_Cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this," & mintIsModule & ")", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valCover_incColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valRole_incColumnCaption"), "valRole_inc", "Tabtab_Covrol3", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this," & mintIsModule & ")", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valRole_incColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_CommColumnCaption"), "cbeType_Comm", "Table5671", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeType_CommColumnToolTip"))
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP828"
		.Width = 450
		.Height = 350
		.WidthDelete = 470
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		.Columns("valModulec_exc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec_exc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec_exc").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valModulec_inc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec_inc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec_inc").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valCover_inc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover_inc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover_inc").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover_inc").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valCover_exc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover_exc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover_exc").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover_exc").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valRole_exc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_exc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_exc").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_exc").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_exc").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valRole_inc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_inc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_inc").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_inc").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valRole_inc").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP828 : Carga los datos que corresponden al grid.
'---------------------------------------------------------------------------------------------
Private Function insPreDP828() As Object
	'---------------------------------------------------------------------------------------------
	Dim lcolComm_mods As eProduct.Comm_mods
	lcolComm_mods = New eProduct.Comm_mods
	
	If lcolComm_mods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		With mobjGrid
			For	Each mclsComm_mod In lcolComm_mods
				
				.Columns("tctModulec_exc").EditRecord = True
				.Columns("tctCover_exc").EditRecord = True
				
				.Columns("tctModulec_exc").DefValue = mclsComm_mod.sDescModulec_ex
				.Columns("valModulec_exc").DefValue = CStr(mclsComm_mod.nModulec_ex)
				.Columns("tctCover_exc").DefValue = mclsComm_mod.sDescCover_ex
				.Columns("valCover_exc").DefValue = CStr(mclsComm_mod.nCover_ex)
				.Columns("tctRole_exc").DefValue = mclsComm_mod.sDescRole_ex
				.Columns("valRole_exc").DefValue = CStr(mclsComm_mod.nRole_ex)
				.Columns("tctModulec_inc").DefValue = mclsComm_mod.sDescModulec_ad
				.Columns("valModulec_inc").DefValue = CStr(mclsComm_mod.nModulec_ad)
				.Columns("tctCover_inc").DefValue = mclsComm_mod.sDescCover_ad
				.Columns("valCover_inc").DefValue = CStr(mclsComm_mod.nCover_ad)
				.Columns("tctRole_inc").DefValue = mclsComm_mod.sDescRole_ad
				.Columns("valRole_inc").DefValue = CStr(mclsComm_mod.nRole_ad)
				.Columns("tctType_Comm").DefValue = mclsComm_mod.sDescType_comm
				.Columns("cbeType_Comm").DefValue = CStr(mclsComm_mod.nType_comm)
				.sDelRecordParam = "nModulec_ex=' + marrArray[lintIndex].valModulec_exc + '&nCover_ex=' + marrArray[lintIndex].valCover_exc + '&nRole_ex=' + marrArray[lintIndex].valRole_exc + '&nModulec_ad=' + marrArray[lintIndex].valModulec_inc + '&nCover_ad=' + marrArray[lintIndex].valCover_inc + '&nRole_ad=' + marrArray[lintIndex].valRole_inc + '&nType_comm=' + marrArray[lintIndex].cbeType_Comm + '"
				
				Response.Write(mobjGrid.DoRow())
			Next mclsComm_mod
		End With
	End If
	With Response
		.Write(mobjGrid.closeTable)
		.Write(mobjValues.BeginPageButton)
	End With
	
	lcolComm_mods = Nothing
End Function

'% insPreDP038Upd: Se muestra la ventana Popup para efecto de actualización del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP828Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete)
			If mclsComm_mod.insPostDP828(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec_ex"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCover_ex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole_ex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec_ad"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCover_ad"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole_ad"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType_Comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP828", .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
		
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mclsComm_mod = New eProduct.Comm_mod
mclsProduct = New eProduct.Product

If mclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
	mintIsModule = 1
Else
	mintIsModule = 0
End If

mclsProduct = Nothing
mobjGrid.sCodisplPage = "DP828"
mobjValues.sCodisplPage = "DP828"

mobjGrid.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.49 $|$$Author: Nvaplat61 $"

//% SetValue: Se asignan los valores de los parametros de los tab_tables
//----------------------------------------------------------------------------------------
function SetValue(Field,IsModule){
//----------------------------------------------------------------------------------------
	if(Field.name == 'valModulec_exc'){
	    if(Field.value != 0 && Field.value != ''){
	    	self.document.forms[0].valCover_exc.Parameters.Param3.sValue = Field.value;
	    	self.document.forms[0].valRole_exc.Parameters.Param5.sValue = Field.value;
	    }
	    else{
			self.document.forms[0].valCover_exc.Parameters.Param3.sValue = 0;
			self.document.forms[0].valRole_exc.Parameters.Param5.sValue = 0;
			if (IsModule==1){
				self.document.forms[0].valCover_exc.value = '';
				UpdateDiv('valCover_excDesc','');
				self.document.forms[0].valRole_exc.value = '';
				UpdateDiv('valRole_excDesc','');
			}
		}
	}
	else {
	    if(Field.name == 'valModulec_inc'){
	        if(Field.value != 0 && Field.value != ''){
	        	self.document.forms[0].valCover_inc.Parameters.Param3.sValue = Field.value;
	        	self.document.forms[0].valRole_inc.Parameters.Param5.sValue = Field.value;
	        }
	        else{
				self.document.forms[0].valCover_inc.Parameters.Param3.sValue = 0;
				self.document.forms[0].valRole_inc.Parameters.Param5.sValue = 0;
				if (IsModule==1){
					self.document.forms[0].valCover_inc.value = '';
					UpdateDiv('valCover_incDesc','');
	        		self.document.forms[0].valRole_inc.value = '';
					UpdateDiv('valRole_incDesc','');
				}
			}
        }
	}

	if(Field.name == 'valCover_exc'){
	    if(Field.value != 0 && Field.value != '')
	    	self.document.forms[0].valRole_exc.Parameters.Param3.sValue = Field.value;
	    else{
			self.document.forms[0].valRole_exc.Parameters.Param3.sValue = 0;
			self.document.forms[0].valRole_exc.value = '';
			UpdateDiv('valRole_excDesc','');
		}
	}
	else {
	    if(Field.name == 'valCover_inc'){
	        if(Field.value != 0 && Field.value != '')
	        	self.document.forms[0].valRole_inc.Parameters.Param3.sValue = Field.value;
	        else{
				self.document.forms[0].valRole_inc.Parameters.Param3.sValue = 0;
	        	self.document.forms[0].valRole_inc.value = '';
				UpdateDiv('valRole_incDesc','');
			}
        }
	}
}
</SCRIPT>
<%
With Response
	.Write("<SCRIPT>var nMainAction = " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP828"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "DP828", "DP828.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP828" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP828Upd()
Else
	Call insPreDP828()
End If

mobjValues = Nothing
mobjGrid = Nothing
mclsComm_mod = Nothing

%>
</FORM>
</BODY>
</HTML>




