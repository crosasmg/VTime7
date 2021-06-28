<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'---------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddBranchColumn(40599, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),  , "",  ,  , "insOnChangeBranch(this)", Request.QueryString.Item("Action") = "Update")
		If Request.QueryString.Item("Action") <> "Add" And Request.QueryString.Item("Action") <> "Update" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 3, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnProductColumnToolTip"))
		End If
            Call .AddProductColumn(40600, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), , CStr(eRemoteDB.Constants.intNull), 4, , , "insOnChangeProduct(this)", True)
            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valAgreementColumnCaption"), "valAgreement", "tabAgreement_al", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("valAgreementColumnToolTip"))
            
		Call .AddPossiblesColumn(40601, GetLocalResourceObject("cbeWay_PayColumnCaption"), "cbeWay_Pay", "Table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeWay_PayColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePayfreqColumnCaption"), "cbePayfreq", "Table36", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "EnabledPayfreq(this)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbePayfreqColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInstallmentsColumnCaption"), "tcnInstallments", 5,  ,  , GetLocalResourceObject("tcnInstallmentsColumnToolTip"))
		Call .AddPossiblesColumn(40602, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insOnChangeModulec(this)", True, 4, GetLocalResourceObject("valModulecColumnToolTip"),  ,  , True)
		Call .AddPossiblesColumn(40603, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", "tabGen_cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True, 4, GetLocalResourceObject("tcnCoverColumnToolTip"),  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDurationColumnCaption"), "tcnDuration", 5, CStr(0),  , GetLocalResourceObject("tcnDurationColumnToolTip"))
		Call .AddNumericColumn(40604, GetLocalResourceObject("tcnInit_MonthColumnCaption"), "tcnInit_Month", 4, CStr(eRemoteDB.Constants.intNull), True, GetLocalResourceObject("tcnInit_MonthColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(40605, GetLocalResourceObject("tcnFinal_MonthColumnCaption"), "tcnFinal_Month", 4, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnFinal_MonthColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            Call .AddNumericColumn(40606, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, CStr(0), , GetLocalResourceObject("tcnPercentColumnToolTip"), , , , , "insOnChangePercent(this);", False)
		Call .AddPossiblesColumn(40607, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "EnabledAmount(this)",  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            Call .AddNumericColumn(40608, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0), , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6, , , , True)
		Call .AddHiddenColumn("tcdEffecdate", CStr(eRemoteDB.Constants.dtmNull))
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
            .Columns("valAgreement").Parameters.Add("sStatregt", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("tcnCover").Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nCoverGen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valProduct").Parameters.ReturnValue("sBrancht", False, vbNullString, True)
		
		.Columns("cbeBranch").EditRecord = True
		.Codispl = "MAG003"
		.Codisp = "MAG003"
		.sCodisplPage = "MAG003"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		'+Pase de parámetros necesarios para la eliminación de registros
            .sDelRecordParam = "dEffecdate=' + marrArray[lintIndex].tcdEffecdate + '" & "&nBranch=' + marrArray[lintIndex].cbeBranch + '" & "&nProduct=' + marrArray[lintIndex].valProduct + '" & "&nWay_Pay=' + marrArray[lintIndex].cbeWay_Pay + '" & "&nModulec=' + marrArray[lintIndex].valModulec + '" & "&nInit_Month=' + marrArray[lintIndex].tcnInit_Month + '" & "&nCover=' + marrArray[lintIndex].tcnCover + '" & "&nDuration=' + marrArray[lintIndex].tcnDuration + '" & "&nAgreement=' + marrArray[lintIndex].valAgreement + '"
		
		.Top = 50
		.Left = 200
		.Height = 490
		.Width = 450
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMAG003: Esta función se encarga de cargar los datos en la forma "Folder" 
'-------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG003()
	'-------------------------------------------------------------------------------------------------------------------
	Dim lcolDet_comgens As eAgent.Det_comgens
	Dim lclsDet_comgen As Object
	Dim lintCount As Short
	
	lcolDet_comgens = New eAgent.Det_comgens
	
	lintCount = 0
	
	If lcolDet_comgens.Find(mobjValues.StringToType(Session("nComtabge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsDet_comgen In lcolDet_comgens
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsDet_comgen.nBranch
				.Columns("valProduct").DefValue = lclsDet_comgen.nProduct
				
				If Request.QueryString.Item("Action") <> "Add" And Request.QueryString.Item("Action") <> "Update" Then
					.Columns("tcnProduct").DefValue = lclsDet_comgen.nProduct
				End If
				
				.Columns("cbeWay_Pay").DefValue = lclsDet_comgen.nWay_Pay
				.Columns("cbePayfreq").DefValue = lclsDet_comgen.nPayfreq
				.Columns("tcnInstallments").DefValue = lclsDet_comgen.nInstallments
				
				.Columns("valModulec").Parameters.Add("nBranch", lclsDet_comgen.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valModulec").Parameters.Add("nProduct", lclsDet_comgen.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valModulec").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valModulec").DefValue = lclsDet_comgen.nModulec
				
				.Columns("tcnCover").Parameters.Add("nBranch", lclsDet_comgen.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("tcnCover").Parameters.Add("nProduct", lclsDet_comgen.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("tcnCover").Parameters.Add("nModulec", lclsDet_comgen.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("tcnCover").Parameters.Add("nCoverGen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("tcnCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("tcnCover").DefValue = lclsDet_comgen.nCover
				
				.Columns("tcnDuration").DefValue = lclsDet_comgen.nDuration
				.Columns("tcnInit_Month").DefValue = lclsDet_comgen.nInit_Month
				.Columns("tcnFinal_Month").DefValue = lclsDet_comgen.nFinal_Month
				.Columns("tcnPercent").DefValue = lclsDet_comgen.nPercent
				.Columns("cbeCurrency").DefValue = lclsDet_comgen.nCurrency
                .Columns("tcnAmount").DefValue = lclsDet_comgen.nAmount        
                .Columns("tcdEffecdate").DefValue = lclsDet_comgen.dEffecdate

                .Columns("valAgreement").Parameters.Add("sStatregt", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valAgreement").DefValue = lclsDet_comgen.nAgreement
				
                .sEditRecordParam = "nBranch=' + marrArray[" & CStr(lintCount) & "].cbeBranch + '"
				
            End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
			lintCount = lintCount + 1
		Next lclsDet_comgen
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMAG003Upd: Se define esta función para contruir el contenido de la ventana "UPD"
'--------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG003Upd()
	'--------------------------------------------------------------------------------------------------------------
	Dim lclsDet_comgen As eAgent.Det_comgen
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsDet_comgen = New eAgent.Det_comgen
		Response.Write(mobjValues.ConfirmDelete())
            Call lclsDet_comgen.insPostMAG003("Del", mobjValues.StringToType(Session("nComtabge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nInit_Month"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDuration"), eFunctions.Values.eTypeData.etdDouble), 0, 0, mobjValues.StringToType(Request.QueryString.Item("nAgreement"), eFunctions.Values.eTypeData.etdDouble))
		lclsDet_comgen = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
		If Request.QueryString.Item("Action") = "Add" Then
			.Write("<SCRIPT>if (document.forms[0].cbeBranch.value!=0)document.forms[0].cbeBranch.onchange();</" & "Script>")
			.Write("<SCRIPT>if (document.forms[0].valProduct.value!=0){document.forms[0].valProduct.focus();}</" & "Script>")
		End If
		If Request.QueryString.Item("Action") = "Update" Then
                .Write("<SCRIPT>EnabledPayfreq(document.forms[0].cbePayfreq)</" & "Script>")
                .Write("<SCRIPT>EnabledAmount(document.forms[0].cbeCurrency)</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 19/01/04 13:55 $|$$Author: Nvaplat11 $"

// insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
// posibles que lo requieran y habilitar los campos que dependan del ramo.
//-------------------------------------------------------------------------------------------------------------------
function insOnChangeBranch(Field){
//-------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){		
        tcnCover.Parameters.Param1.sValue = Field.value
        valModulec.Parameters.Param1.sValue = Field.value
    }
}
// insOnChangeProduct: Esta función se encarga de pasar el parametro PRODUCT a los valores 
// posibles que lo requieran y habilitar los campos que dependan del producto.
//-------------------------------------------------------------------------------------------------------------------
function insOnChangeProduct(Field){
//-------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        tcnCover.Parameters.Param1.sValue = cbeBranch.value
        tcnCover.Parameters.Param2.sValue = valProduct.value
        tcnCover.Parameters.Param3.sValue = 0
        valModulec.Parameters.Param2.sValue = Field.value

		<%If Request.QueryString.Item("Action") = "Add" Then%>
			if(Field.value != 0 && 
			   Field.value !=''){       
				tcnCover.disabled = false
				btntcnCover.disabled = false
				insDefValues("ProductMAG003", "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value, '/VTimeNet/Maintenance/MantAgent')
			}
		 <%End If%>	 

		if(Field.value=='' || 
		   Field.value==0){		
		    valModulec.value=''
		    tcnCover.value=''
		    $(valModulec).change();
		    $(tcnCover).change();
	        valModulec.disabled=true
	        btnvalModulec.disabled=true		    
			tcnCover.disabled = true
			btntcnCover.disabled = true
		}        
    }
}

// insOnChangeModulec: Esta función se encarga de pasar el parametro MODULEC a los valores 
// posibles que lo requieran y habilitar los campos que dependan del ramo.
//-------------------------------------------------------------------------------------------------------------------
function insOnChangeModulec(Field){
//-------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){        
        if(Field.value!='' && Field.value>0)
            tcnCover.Parameters.Param3.sValue = Field.value        
		else
		    tcnCover.Parameters.Param3.sValue = 0;
    }
}
//EnabledAmount: Habilita el campo "Comisión fija", si se indica una moneda
//------------------------------------------------------------------------------------------------------------------
function EnabledAmount(Field){
//------------------------------------------------------------------------------------------------------------------
	if(Field.value!=0){
		self.document.forms[0].tcnAmount.disabled=false;
        //self.document.forms[0].tcnPercent.disabled=true;
        //self.document.forms[0].tcnPercent.value="0,00";
    }
	else {	
        //self.document.forms[0].tcnPercent.disabled=false;
		self.document.forms[0].tcnAmount.disabled=true;
		self.document.forms[0].tcnAmount.value="0,00";
	}		
}

//EnabledAmount: Coloca cero en el campo cuando se blanquea el campo
//------------------------------------------------------------------------------------------------------------------
function insOnChangePercent(Field){
//------------------------------------------------------------------------------------------------------------------
	if(Field.value==""){
		self.document.forms[0].tcnPercent.value="0";
	}		
}

//EnabledPayfreq: Habilita el campo "Cantidad de cuotas", de acuerdo a la frecuencia  de pago
//------------------------------------------------------------------------------------------------------------------
function EnabledPayfreq(Field){
//------------------------------------------------------------------------------------------------------------------
//+ Si la frecuencia de pago es por cuotas	
	if(Field.value==8)
		self.document.forms[0].tcnInstallments.disabled=false;
	else{
		self.document.forms[0].tcnInstallments.disabled=true;
		self.document.forms[0].tcnInstallments.value='';
	}
}

//insPreZone: Manejo de la accion Condicional
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(nAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + nAction
	        break;
	}
}

</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>        
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MAG003", "MAG003"))
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTabGralComm" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write("<SCRIPT>var sAction='" & Request.QueryString.Item("Action") & "'</script>")
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG003()
Else
	Call insPreMAG003Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




