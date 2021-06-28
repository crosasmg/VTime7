<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		'+ Ramo 
		If Request.QueryString.Item("Type") = "PopUp" Then
			'Call .AddBranchColumn(40599, GetLocalResourceObject("cbeBranchColumnCaption"),"cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),,"",,,,Request.QueryString("Action")="Update")
			
			Call .AddPossiblesColumn(40599, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "tabtable10_t", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  , "insChangeField(this);", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBranchColumnToolTip"))
		Else
			Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "")
			Call .AddHiddenColumn("cbeBranch", CStr(0))
		End If
		
		'+ Producto 
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddProductColumn(40600, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"),  , CStr(eRemoteDB.Constants.intNull), 4,  ,  , "insChangeField(this);", True)
		Else
			Call .AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "")
			Call .AddHiddenColumn("valProduct", CStr(0))
		End If
		
		'+ Módulo
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "TabTab_Modul", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  , "insChangeField(this);", True, 5, GetLocalResourceObject("valModulecColumnToolTip"))
		Else
			Call .AddTextColumn(0, GetLocalResourceObject("tctModulecColumnCaption"), "tctModulec", 30, "")
			Call .AddHiddenColumn("valModulec", CStr(0))
		End If
		
		'+ Cobertura
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddPossiblesColumn(40785, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  ,  , True,  , GetLocalResourceObject("valCoverColumnToolTip"))
		Else
			Call .AddTextColumn(0, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", 100, "")
			Call .AddHiddenColumn("valCover", CStr(0))
		End If
		Call .AddHiddenColumn("hddIdRec", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MMGS002"
		.sCodisplPage = "MMGS002"
		.ActionQuery = mobjValues.ActionQuery
		.AddButton = CDbl(Request.QueryString.Item("nTableTyp")) <> 5
		.Height = 250
		.Width = 550
		.WidthDelete = 550
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		.sDelRecordParam = "nInsur_area=" & Request.QueryString.Item("nInsur_area") & "&nTableTyp=" & Request.QueryString.Item("nTableTyp") & "&nSource=" & Request.QueryString.Item("nSource") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nIdRec=' + marrArray[lintIndex].hddIdRec + '"
		
		.sEditRecordParam = "nInsur_area=" & Request.QueryString.Item("nInsur_area") & "&nTableTyp=" & Request.QueryString.Item("nTableTyp") & "&nSource=" & Request.QueryString.Item("nSource") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nClaimClass=" & Request.QueryString.Item("nClaimClass")
		With .Columns("cbeBranch").Parameters
			If Session("nInsur_area") = 2 Then
				.Add("sbrancht", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add(" sbrancht_not", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Add("sbrancht", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add(" sbrancht_not", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
		End With
		With mobjGrid.Columns("valModulec").Parameters
			.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		With .Columns("valCover").Parameters
			.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		.Columns("cbeBranch").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	Response.Write(mobjValues.HiddenControl("hddInsur_area", Request.QueryString.Item("nInsur_area")))
	Response.Write(mobjValues.HiddenControl("hddTableTyp", Request.QueryString.Item("nTableTyp")))
	Response.Write(mobjValues.HiddenControl("hddSource", Request.QueryString.Item("nSource")))
	Response.Write(mobjValues.HiddenControl("hddClaimClass", Request.QueryString.Item("nClaimClass")))
	Response.Write(mobjValues.HiddenControl("hddEffecdate", Request.QueryString.Item("dEffecdate")))
	
End Sub

'% insPreMMGS002: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMMGS002()
	'--------------------------------------------------------------------------------------------
	Dim lcolMargin_Allow As eMargin.Margin_Allows
	Dim lclsMargin_Allow As Object
	lcolMargin_Allow = New eMargin.Margin_Allows
	If lcolMargin_Allow.Find(mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTableTyp"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nClaimClass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsMargin_Allow In lcolMargin_Allow
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsMargin_Allow.nBranch
				.Columns("tctBranch").DefValue = lclsMargin_Allow.sBranch
				.Columns("valProduct").DefValue = lclsMargin_Allow.nProduct
				.Columns("tctProduct").DefValue = lclsMargin_Allow.sProduct
				.Columns("valModulec").DefValue = lclsMargin_Allow.nModulec
				.Columns("tctModulec").DefValue = lclsMargin_Allow.sModulec
				.Columns("valCover").DefValue = lclsMargin_Allow.nCover
				.Columns("tctCover").DefValue = lclsMargin_Allow.sCover
				.Columns("hddIdRec").DefValue = lclsMargin_Allow.nIdRec
				
				With .Columns("cbeBranch").Parameters
					If Session("nInsur_area") = 2 Then
						.Add("sbrancht", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Add(" sbrancht_not", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Else
						.Add("sbrancht", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Add(" sbrancht_not", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
				End With
				
				With .Columns("valModulec").Parameters
					.Add("nBranch", lclsMargin_Allow.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nProduct", lclsMargin_Allow.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End With
				With .Columns("valCover").Parameters
					.Add("nBranch", lclsMargin_Allow.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nProduct", lclsMargin_Allow.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nModulec", lclsMargin_Allow.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End With
				
				Response.Write(.DoRow)
			End With
		Next lclsMargin_Allow
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lcolMargin_Allow = Nothing
	lclsMargin_Allow = Nothing
End Sub

'% insPreMMGS002Upd: Se realiza el manejo de la ventana PopUp asociada al grid 
'-------------------------------------------------------------------------------------------- 
Private Sub insPreMMGS002Upd()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsMargin_Allow As eMargin.Margin_Allow
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsMargin_Allow = New eMargin.Margin_Allow
			If lclsMargin_Allow.inspostMMGS002(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTableTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nSource"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nIdRec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nClaimClass"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode")) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantMargin.aspx", "MMGS002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsMargin_Allow = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MMGS002"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>






<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 5 $|$$Date: 27/11/03 14:17 $|$$Author: Nvaplat15 $"

//% insChangeField: Accciones a seguir en los cambios de valores de los campos  
//--------------------------------------------------------------------------------------------
function insChangeField(sField){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField.name){
			case 'valProduct':
				valModulec.value = '';
				UpdateDiv('valModulecDesc','','Normal');
				valModulec.Parameters.Param1.sValue = cbeBranch.value;
				valModulec.Parameters.Param2.sValue = valProduct.value;
                if(valProduct.value > 0){
// Se verificar que el producto sea modular 
					strParams = "nBranch=" + cbeBranch.value + 
								"&nProduct=" + valProduct.value + 
								"&dEffecdate=" + hddEffecdate.value
					insDefValues("TabModul",strParams,'/VTimeNet/maintenance/mantmargin/');
					valCover.value = '';
					UpdateDiv('valCoverDesc','','Normal');
					valCover.Parameters.Param1.sValue = cbeBranch.value;
					valCover.Parameters.Param2.sValue = valProduct.value;
                }
				break;

			case 'valModulec':
				valCover.value = '';
				UpdateDiv('valCoverDesc','','Normal');
                if(valModulec.value > 0){
					valCover.disabled = btnvalCover.disabled = false;
					valCover.Parameters.Param3.sValue = valModulec.value;
				}
				break;
			case 'cbeBranch':
					valProduct.value = '';
					UpdateDiv('valProductDesc','','');
			    if (cbeBranch.value > 0){ 
			        valProduct.disabled = false;
			        btnvalProduct.disabled = false;
			        valProduct.Parameters.Param1.sValue = cbeBranch.value;
			    }
			    else{
			        valProduct.disabled = true;
			        btnvalProduct.disabled = true;
			    }
				break;
		}
    }
}
</SCRIPT>
<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MMGS002", "MMGS002.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MMGS002" ACTION="valMantMargin.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MMGS002"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMMGS002Upd()
Else
	Call insPreMMGS002()
End If
mobjMenu = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





