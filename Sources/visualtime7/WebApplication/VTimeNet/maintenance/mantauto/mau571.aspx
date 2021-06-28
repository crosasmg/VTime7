<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim mstrMarca As String


'% insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
              
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		'+ Módulo
		Call .AddNumericColumn(0, GetLocalResourceObject("nModulecColumnCaption"), "nModulec", 5, CStr(0),  , GetLocalResourceObject("nModulecColumnToolTip"),  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nModulec").PopUpVisible = False
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeModulecColumnCaption"), "cbeModulec", "TabTab_Modul", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  , "insChangeField(this);",  , 5, GetLocalResourceObject("cbeModulecColumnToolTip"))
			
			With mobjGrid.Columns("cbeModulec").Parameters
				.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
		Else
			.AddTextColumn(0, GetLocalResourceObject("tctModulecColumnCaption"), "tctModulec", 30, "")
			.AddHiddenColumn("cbeModulec", CStr(0))
		End If
		
		'+ Cobertura
		Call .AddNumericColumn(0, GetLocalResourceObject("nCoverColumnCaption"), "nCover", 5, CStr(0),  , GetLocalResourceObject("nCoverColumnToolTip"),  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nCover").PopUpVisible = False
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeCoverColumnCaption"), "cbeCover", "TabGen_Cover2", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCoverColumnToolTip"))
			
			With mobjGrid.Columns("cbeCover").Parameters
				.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
		Else
			.AddTextColumn(0, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", 100, "")
			.AddHiddenColumn("cbeCover", CStr(0))
		End If
		
		.AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 5, "",  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnPrem_fixColumnCaption"), "tcnPrem_fix", 18, "",  , GetLocalResourceObject("tcnPrem_fixColumnToolTip"),  , 6)
		                
            .AddHiddenColumn("hddnId", CStr(0))
            .AddHiddenColumn("sParam", vbNullString)
            
            '.AddHiddenColumn("hddnBranch", Request.QueryString.Item("nBranch"))
            '.AddHiddenColumn("hddnProduct", Request.QueryString.Item("nProduct"))
            '.AddHiddenColumn("hdddEffecdate", Request.QueryString.Item("dEffecdate"))
            '.AddHiddenColumn("hddnCurrency", Request.QueryString.Item("nCurrency"))
            '.AddHiddenColumn("hddsVehcode", Request.QueryString.Item("sVehcode"))
            '.AddHiddenColumn("hddsoptTyp_var", Request.QueryString.Item("optTyp_var"))
            '.AddHiddenColumn("hddnRateAddSub", Request.QueryString.Item("tctRateAddSub"))
            
		
            Session("hddnId") = CStr(0)
            Session("hddnBranch") = Request.QueryString.Item("nBranch")
            Session("hddnProduct") = Request.QueryString.Item("nProduct")
            Session("hdddEffecdate") = Request.QueryString.Item("dEffecdate")
            Session("hddnCurrency") = Request.QueryString.Item("nCurrency")
            Session("hddsVehcode") = Request.QueryString.Item("sVehcode")
            Session("hddsoptTyp_var") = Request.QueryString.Item("optTyp_var")
            Session("hddnRateAddSub") = Request.QueryString.Item("tctRateAddSub")
            Session("sParam") = vbNullString
	End With
	
	With mobjGrid
		.Codispl = "MAU571"
		.Codisp = "MAU571"
		.sCodisplPage = "MAU571"
		.Top = 100
		.Height = 288
		.Width = 390
		.AddButton = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 306
		.DeleteButton = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 306
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 306
		.Columns("nModulec").EditRecord = True
		.Columns("cbeModulec").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbeCover").Disabled = Request.QueryString.Item("Action") = "Update"
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&sVehcode=" & Request.QueryString.Item("sVehcode")
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMAU571. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMAU571()
	'------------------------------------------------------------------------------
	Dim lcolTar_autos As ePolicy.Tar_autos
	Dim lclsTar_auto As Object
	
	With Request
		lcolTar_autos = New ePolicy.Tar_autos
		With mobjGrid
			If lcolTar_autos.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sVehcode")) Then
				
				For	Each lclsTar_auto In lcolTar_autos
					.Columns("hddnId").DefValue = lclsTar_auto.nId
					.Columns("nModulec").DefValue = lclsTar_auto.nModulec
					.Columns("cbeModulec").DefValue = lclsTar_auto.nModulec
					.Columns("tctModulec").DefValue = lclsTar_auto.sDesc_modulec
					.Columns("nCover").DefValue = lclsTar_auto.nCover
					.Columns("cbeCover").Parameters.Add("nModulec", lclsTar_auto.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Columns("cbeCover").DefValue = lclsTar_auto.nCover
					.Columns("tctCover").DefValue = lclsTar_auto.sDesc_cover
					.Columns("tcnRate").DefValue = lclsTar_auto.nRate
					.Columns("tcnPrem_fix").DefValue = lclsTar_auto.nPrem_fix
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMAU571Upd cuando se eliminen los registros seleccionados 
					.Columns("sParam").DefValue = "nBranch=" & lclsTar_auto.nBranch & "&nProduct=" & lclsTar_auto.nProduct & "&nCurrency=" & lclsTar_auto.nCurrency & "&sVehcode=" & lclsTar_auto.sVehcode & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nModulec=" & lclsTar_auto.nModulec & "&nCover=" & lclsTar_auto.nCover & "&nId=" & lclsTar_auto.nId & "&nUserCode=" & Session("nUsercode")
					
					Response.Write(mobjGrid.DoRow())
				Next lclsTar_auto
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsTar_auto = Nothing
	lcolTar_autos = Nothing
	
End Sub

'% insPreMAU571Upd. Se define esta funcion para contruir el contenido de la 
'% ventana UPD de tarifa de automóvil
'------------------------------------------------------------------------------
Private Sub insPreMAU571Upd()
	'------------------------------------------------------------------------------
	Dim lclsTar_auto As ePolicy.Tar_auto
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTar_auto = New ePolicy.Tar_auto
			Call lclsTar_auto.insPostMAU571Upd("Del", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("hddsoptTyp_var"), mobjValues.StringToType(.QueryString.Item("hddnRateAddSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sVehcode"), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantAuto.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTar_auto = Nothing
End Sub

'% PrintHead: copia la cabecera en la parte superior de la grilla, cuando la acción es duplicar
'--------------------------------------------------------------------------------------------
Sub PrintHead()
	'--------------------------------------------------------------------------------------------
	With Request
		mobjValues.ActionQuery = False
		
Response.Write("" & vbCrLf)
Response.Write("		<DIV ID=""DivHeaderDup"" >" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranch_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch_aux", "Table10", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "ChangeControl()",  ,  , GetLocalResourceObject("cbeBranch_auxToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("				    <TD></TD>" & vbCrLf)
Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("valProduct_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")

		
		With mobjValues
                .Parameters.Add("nBranch", 6, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("valProduct_aux", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, Session("nProduct"), True, , , , , , True, 4, GetLocalResourceObject("valProduct_auxToolTip")))
		End With
		
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>" & GetLocalResourceObject("valVehcode_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>            " & vbCrLf)
Response.Write("				        ")

		
		With mobjValues.Parameters
			.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.ReturnValue("nVehbrand",  , "Marca", True)
			.ReturnValue("sVehmodel",  , "Modelo", True)
			Response.Write(mobjValues.PossiblesValues("valVehcode_aux", "TabTab_au_veh", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insChangeField_aux(this);", False, 6, GetLocalResourceObject("valVehcode_auxToolTip")))
		End With
		
Response.Write("" & vbCrLf)
Response.Write("				    </TD>               " & vbCrLf)
Response.Write("				    <TD></TD>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeVehbrand_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeVehbrand_aux", "Table7042", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mstrMarca, eFunctions.Values.eTypeData.etdDouble), False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeVehbrand_auxToolTip")))


Response.Write("</TD>            " & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>        " & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>" & GetLocalResourceObject("tctVehmodel_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.TextControl("tctVehmodel_aux", 10, Session("sVehmodel"),  , GetLocalResourceObject("tctVehmodel_auxToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    <TD></TD>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdate_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate_aux", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdate_auxToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TR>        " & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>" & GetLocalResourceObject("valCurrency_auxCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.PossiblesValues("valCurrency_aux", "Table11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"),  ,  ,  ,  ,  ,  , False, 2, GetLocalResourceObject("valCurrency_auxToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.OptionControl(0, "optTyp_var_aux", GetLocalResourceObject("optTyp_var_aux_1Caption"), "1", "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.OptionControl(0, "optTyp_var_aux", GetLocalResourceObject("optTyp_var_aux_2Caption"), "2", "2"))


Response.Write("</TD>               " & vbCrLf)
Response.Write("				    <TD></TD>" & vbCrLf)
Response.Write("       			    <TD><LABEL ID=0>" & GetLocalResourceObject("tctRateAddSub_auxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.NumericControl("tctRateAddSub_aux", 5, CStr(0),  , GetLocalResourceObject("tctRateAddSub_auxToolTip"), True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("		</DIV>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		")

		
	End With
	mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAU571"
%>

<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MAU571", "MAU571.aspx"))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMAU571" ACTION="valMantAuto.aspx?sZone=2">
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"

//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
            case "cbeModulec":
                cbeCover.Parameters.Param5.sValue=(cbeModulec.value==''?0:cbeModulec.value);
                break;
		}
	}
}

//% insChangeField_aux: Se recargan los valores cuando cambia el campo
//-----------------------------------------------------------------------------
function insChangeField_aux(Field){
//-----------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
            case "valVehcode_aux":
                self.document.forms[0].cbeVehbrand_aux.value = valVehcode_aux_nVehbrand.value
                self.document.forms[0].tctVehmodel_aux.value = valVehcode_aux_sVehmodel.value
                break;
		}
	}
}
</SCRIPT>
<%
Response.Write(mobjValues.ShowWindowsName("MAU571"))
Call insDefineHeader()
Call PrintHead()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAU571()
Else
	Call insPreMAU571Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<SCRIPT LANGUAGE="JAVASCRIPT">

    if('<%=Request.QueryString.Item("nMainAction")%>'!='306')
		ShowDiv('DivHeaderDup', 'hide');
		
//% ChangeControl: Habilita/Deshabilita los controles dependientes de la página
//-------------------------------------------------------------------------------------------
function ChangeControl(){
//-------------------------------------------------------------------------------------------
	UpdateDiv("valProductDesc","");
	with(self.document.forms[0]){
		valProduct_aux.value="";
		if(cbeBranch_aux.value=="0"){
			valProduct_aux.disabled=true;
			self.document.btnvalProduct_aux.disabled=true;
		}
		else{
			valProduct_aux.disabled=false;
			document.btnvalProduct_aux.disabled=false;
			valProduct_aux.Parameters.Param1.sValue=cbeBranch_aux.value;
		}
	}
}

</SCRIPT>





