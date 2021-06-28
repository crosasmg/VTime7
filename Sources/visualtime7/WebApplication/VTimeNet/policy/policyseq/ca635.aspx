<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo particular de los datos de la página
Dim mcolCond_cover_premium As ePolicy.Cond_cover_premiums

'+declaracion de los objectos con referencias a tablas.
Dim lclsPolicy As ePolicy.Policy
Dim lclsProduct As eProduct.Product
Dim lclsGroups As ePolicy.Groups

Dim lblnModul As Boolean
Dim lblnGroups As Boolean

Dim lclsFindCond_cover_premium As ePolicy.Cond_cover_premium
    
Dim lblnOk As Boolean


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
'--------------------------------------------------------------------------------------------
	
        '+ Variable para controlar la actualización de la información de manera puntual (desde el botón de la ventana)
    Response.Write(mobjValues.HiddenControl("hddbPuntual", CStr(False)))        
    Response.Write(mobjValues.HiddenControl("hddbCopiar", CStr(False)))
        
    mobjGrid = New eFunctions.Grid        
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID        
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
    With mobjGrid.Columns            
		'+ parametro para el campo de nCover
		'+ Columnas para el tipo de asegurado.
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCoverColumnCaption"), "cbeCover", "tabtab_covrol5", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  , "insChangeField(this);",  ,  , GetLocalResourceObject("cbeCoverColumnToolTip"))
		With mobjGrid.Columns("cbeCover").Parameters
			.add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nModulec", mobjValues.StringToType(Request.QueryString.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nRole", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("sCacaltyp", "4", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "tabtab_covrol6", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		With mobjGrid.Columns("cbeRole").Parameters
			.add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nModulec", mobjValues.StringToType(Request.QueryString.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("nRole", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.add("sCacaltyp", "4", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		'+parametro para el campo de Tipo de Prima
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTipPremColumnCaption"), "cbeTipPrem", "Table5582", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "insChangeField(this)",  ,  , GetLocalResourceObject("cbeTipPremColumnToolTip"))
        '+ moneda permitidas por la poliza.
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "tabcurren_pol", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnCurrencyColumnToolTip"))
		mobjGrid.Columns("tcnCurrency").Parameters.add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("tcnCurrency").Parameters.add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("tcnCurrency").Parameters.add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("tcnCurrency").Parameters.add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("tcnCurrency").Parameters.add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("tcnCurrency").Parameters.add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
         
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)            
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_minColumnCaption"), "tcnCapital_min", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_minColumnToolTip"), True, 2)                        
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_maxColumnCaption"), "tcnCapital_max", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_maxColumnToolTip"), True, 2)                                    
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 6)                
        
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoutineColumnCaption"), "cbeRoutine", "TABTAB_ROUTINE", Values.eValuesType.clngWindowType, , True, , , , , False, 12, GetLocalResourceObject("tctRoupremiToolTip"), eFunctions.Values.eTypeCode.eString)
        mobjGrid.Columns("cbeRoutine").Parameters.Add("NROUTINETYPE", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)                                    
        
        Call .AddPossiblesColumn(0, GetLocalResourceObject("valId_tableColumnCaption"), "valId_table", "TABLE5800", Values.eValuesType.clngWindowType, ,True, , , , , True, 12, GetLocalResourceObject("valId_tableColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)            
            Call .AddHiddenColumn("hddnID", vbNullString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.sEditRecordParam = "cbeGroup='+ self.document.forms[0].cbeGroup.value + '&cbeModulec=' + self.document.forms[0].cbeModulec.value + '"
		.Codispl = "CA635"
		.ActionQuery = Session("bQuery") 'mobjValues.ActionQuery
		.AddButton = True
		.DeleteButton = True
		.Columns("cbeCover").EditRecord = True
        .Columns("cbeTipPrem").BlankPosition = False
		.Height = 380
		.Width = 420
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
            .sDelRecordParam = "cbeGroup='+ self.document.forms[0].cbeGroup.value + '" & "&cbeModulec='+ self.document.forms[0].cbeModulec.value + '" & "&cbeCover='+ marrArray[lintIndex].cbeCover + '" & "&cbeRole='+ marrArray[lintIndex].cbeRole + '" & "&nID='+ marrArray[lintIndex].hddnID + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreCA635: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA635()
	'--------------------------------------------------------------------------------------------
	Dim lclsCond_cover_premium As ePolicy.Cond_cover_premium
	Dim nCoverGroup As Object
	
	lclsCond_cover_premium = New ePolicy.Cond_cover_premium
	mcolCond_cover_premium = New ePolicy.Cond_cover_premiums
	
	'+ Si Request.QueryString("nCharge") <> 1, se asigna por default el valor encontrado en FindGroupCover
	'+ Si es igual, entonces se trata del grupo actual
	
	If CDbl(Request.QueryString.Item("nCharge")) <> 1 Then
		nCoverGroup = lclsFindCond_cover_premium.nGroup
	Else
		nCoverGroup = Request.QueryString.Item("cbeGroup")
	End If
	
	'+clase que busca en la tabla Cond_cover_premium
	'  mobjValues.StringToType(Request.QueryString("cbeGroup"),eFunctions.Values.eTypeData.etdDouble),     
    If mcolCond_cover_premium.Find(Session("sCertype"),
                                   mobjValues.StringToType(Session("nBranch"),eFunctions.Values.eTypeData.etdDouble),                            
                                   mobjValues.StringToType(Session("nProduct"),eFunctions.Values.eTypeData.etdDouble),                            
                                   mobjValues.StringToType(session("npolicy"), eFunctions.Values.eTypeData.etdDouble),                            
                                   mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),                            
                                   nCoverGroup,                            
                                   mobjValues.StringToType(Request.QueryString("cbeModulec"),eFunctions.Values.eTypeData.etdDouble),                            
                                   mobjValues.StringToType(Session("dEffecdate"),eFunctions.Values.eTypeData.etdDate)) Then
	
    For	Each lclsCond_cover_premium In mcolCond_cover_premium
		With mobjGrid
			.Columns("cbeCover").DefValue = CStr(lclsCond_cover_premium.nCover)
			.Columns("cbeRole").DefValue = CStr(lclsCond_cover_premium.nRole)
			.Columns("cbeRole").Parameters.add("nCover", lclsCond_cover_premium.nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeTipPrem").DefValue = CStr(lclsCond_cover_premium.nTypcond)
			.Columns("tcnCurrency").DefValue = CStr(lclsCond_cover_premium.nCurrency)
            .Columns("tcnPremium").DefValue = CStr(lclsCond_cover_premium.nPremium)                    
			.Columns("tcnCapital_min").DefValue = CStr(lclsCond_cover_premium.nCapital_min)
			.Columns("tcnCapital_max").DefValue = CStr(lclsCond_cover_premium.nCapital_max)
            .Columns("tcnRate").DefValue = CStr(lclsCond_cover_premium.nRate)                    
            .Columns("cbeRoutine").DefValue = CStr(lclsCond_cover_premium.sRoutine)                    
                    .Columns("valId_table").DefValue = CStr(lclsCond_cover_premium.nId_table)
                    .Columns("hddnID").DefValue = CStr(lclsCond_cover_premium.nId)
                    Response.Write(.DoRow)
		End With
	Next lclsCond_cover_premium
	
    End If
        
	Response.Write(mobjGrid.closeTable())
	mcolCond_cover_premium = Nothing
	lclsCond_cover_premium = Nothing
End Sub

'% insPreCA635Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA635Upd()
	'--------------------------------------------------------------------------------------------
	'+objecto con referencia a la tabla "Cond_cover_premium"
	Dim lobjCond_cover_premium As ePolicy.Cond_cover_premium
	lobjCond_cover_premium = New ePolicy.Cond_cover_premium
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
                If lobjCond_cover_premium.insPostCA635(.QueryString.Item("Action"),
                                                       Session("sCertype"),
                                                       mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(.QueryString.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(.QueryString.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(.QueryString.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(.QueryString.Item("cbeRole"), eFunctions.Values.eTypeData.etdDouble),
                                                       mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                                       mobjValues.StringToType(.QueryString.Item("hddnID"), eFunctions.Values.eTypeData.etdDouble), ) Then
                End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "CA635", .QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
	End With
	lobjCond_cover_premium = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA635")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
'mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JAVASCRIPT" SRC="/VTimeNet/SCRIPTS/GENFUNCTIONS.JS"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA635", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeField(vObj){
//--------------------------------------------------------------------------------------------
	var sValue, bNullValue;
	var frm = self.document.forms[0];
	
	sValue = vObj.value;
	
	with (frm){
		switch (vObj.name){
		    case 'cbeTipPrem':
		        //tcnCurrency.disabled = (sValue == '3') || (sValue == '4');
		        //tcnPremium.disabled = (sValue != '1');
		        //tcnCapital_min.disabled = (sValue != '2');
		        //tcnCapital_max.disabled = (sValue != '2');
		        //tcnRate.disabled = (sValue != '2');
		        //cbeRoutine.disabled = (sValue != '3');
		        //btncbeRoutine.disabled = (sValue != '3');
		        //valId_table.disabled = (sValue != '4');
		        //btnvalId_table.disabled = (sValue != '4');

		        if (tcnCurrency.disabled) tcnCurrency.value = '';
                if (tcnPremium.disabled) tcnPremium.value = '';
                if (tcnCapital_min.disabled) tcnCapital_min.value = '';
                if (tcnCapital_max.disabled) tcnCapital_max.value = '';
                if (tcnRate.disabled) tcnRate.value = '';
                if (cbeRoutine.disabled) cbeRoutine.value = '';
                if (cbeRoutine.disabled) UpdateDiv('cbeRoutineDesc', ''); 
                if (valId_table.disabled) valId_table.value = '';
                if (valId_table.disabled) UpdateDiv('valId_tableDesc', '');  
		       
		        break;
                
            case "cbeCover":
                //cbeRole.disabled = (sValue=='0');
                //btncbeRole.disabled = (sValue=='0');
				if (sValue=='0') {
					cbeRole.value = '';
					UpdateDiv('cbeRoleDesc', '');
				}
				if (sValue!='0'){
				if(typeof(cbeRole)!='undefined')				   	   
				   	   cbeRole.Parameters.Param4.sValue=cbeCover.value;
				   } 
				break
		}
	}
}
//---------------------------------------------------------------------------------------------------------*/
//% ShowReceipts: Esta función se encarga de dibujar una tabla con el contenido de los datos */
//% del coberturas seleccionadas el cual se encuentra almecenado en el arreglo.                   */
/---------------------------------------------------------------------------------------------------------*/
function ShowReceipts(cbeGroup,cbeModulec)
/*---------------------------------------------------------------------------------------------------------*/
{

    self.document.forms[0].target = 'fraGeneric';
    UpdateDiv('lblWaitProcess', '<MARQUEE>Procesando, por favor espere...</MARQUEE>', '');
    
    var lstrstring = "";
    lstrstring += document.location;
	lstrstring = lstrstring.replace(/&cbeGroup=.*/, "");
	lstrstring = lstrstring.replace(/&cbeModulec=.*/, "");
	lstrstring = lstrstring.replace(/&Reload=.*/, "");

// Si se asigna a nCharge = 1, entonces realiza la busqueda por el grupo actual. Si no, por
// default busca el primer grupo donde exista condicion de capital
 	
	lstrstring = lstrstring + "&cbeGroup="+cbeGroup.value + "&cbeModulec="+cbeModulec.value  + "&reload=2" + "&nCharge=1";
		document.location = lstrstring;

}
//% insCopy: Se copian las coberturas en todos los grupos
//------------------------------------------------------------------------------------------
function insCopy() {
    //------------------------------------------------------------------------------------------

    self.document.forms[0].target = 'fraGeneric';
    UpdateDiv('lblWaitProcess', '<MARQUEE>Procesando, por favor espere...</MARQUEE>', '');

    with (self.document.forms[0]) {
        self.document.forms[0].hddbCopiar.value = true;
        self.document.forms[0].hddbPuntual.value = true;
    }
    top.frames['fraHeader'].ClientRequest(390, 2);
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA635" ACTION="ValPolicySeq.aspx?X=1">
	<%
Response.Write(mobjValues.ShowWindowsName("CA635", Request.QueryString.Item("sWindowDescript")))

lclsPolicy = New ePolicy.Policy
lclsProduct = New eProduct.Product
lclsGroups = New ePolicy.Groups

Call lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"))

lblnModul = True
If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
	lblnModul = False
End If
lblnGroups = True
If lclsGroups.valGroupExist(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate")) Then
	lblnGroups = False
End If

'+Busca si hay grupos con comisiones asignadas
lclsFindCond_cover_premium = New ePolicy.Cond_cover_premium

'+ Si Request.QueryString("nCharge") <> 1, busca si algun grupo contiene
'+ condición de capital y es el que muestra por default en la página

If CDbl(Request.QueryString.Item("nCharge")) <> 1 Then
	Call lclsFindCond_cover_premium.FindGroupCover(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))
End If

'+Define la cabezera del Frame
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA635Upd()
	Response.Write(mobjValues.HiddenControl("cbeGroup", Request.QueryString.Item("cbeGroup")))
	Response.Write(mobjValues.HiddenControl("cbeModulec", Request.QueryString.Item("cbeModulec")))
	'+ Condicion para ejecutar la llamada al procedimiento que configura si un control esta activado
	'+ o desactivado. Solo para agregar y modificar.
	Select Case Request.QueryString.Item("Action")
		Case "Add", "Update"
			Response.Write("<SCRIPT>")
			Response.Write("insChangeField(self.document.forms[0].cbeTipPrem);")
			Response.Write("insChangeField(self.document.forms[0].cbeCover);")
			Response.Write("</script>")
	End Select
Else
	
	%>
	<TABLE WIDTH="100%">
	  <TR>
	    <TD><LABEL><%= GetLocalResourceObject("cbeGroupCaption") %></LABEL></TD>
	    <TD>
		<%	
	With mobjValues
		Call .Parameters.add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		'+ Si Request.QueryString("nCharge") <> 1, se asigna por default el valor encntrado en FindGroupCover
		'+ Si es igual, entonces se trata del grupo actual
		
		If CDbl(Request.QueryString.Item("nCharge")) <> 1 Then
		    Response.Write(mobjValues.PossiblesValues("cbeGroup", "tabgroups", eFunctions.Values.eValuesType.clngComboType, CStr(lclsFindCond_cover_premium.nGroup), True, , , , , "ShowReceipts(cbeGroup,cbeModulec)", lclsPolicy.sTyp_module <> "3" Or lblnGroups, , GetLocalResourceObject("cbeGroupToolTip")))
		Else
			Response.Write(mobjValues.PossiblesValues("cbeGroup", "tabgroups", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("cbeGroup"), True,  ,  ,  ,  , "ShowReceipts(cbeGroup,cbeModulec)", lclsPolicy.sTyp_module <> "3" Or lblnGroups,  , GetLocalResourceObject("cbeGroupToolTip")))
		End If
		
	End With    
	%>
	   </TD>
       <TD><LABEL><%= GetLocalResourceObject("cbeModulecCaption") %></LABEL></TD>
	   <TD>
		<%	
	With mobjValues
		        .Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        Response.Write(mobjValues.PossiblesValues("cbeModulec", "TABTABMODUL_CO_PG_DI", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("cbeModulec"), True, , , , , "ShowReceipts(cbeGroup,cbeModulec)", False, , GetLocalResourceObject("cbeModulecToolTip")))
		    End With
	%>
	   </TD>
	  </TR>
      <%If Not lclsPolicy.sTyp_module <> "3" And Not lblnGroups Then%>
		<TR>
            <TD><LABEL ID="0"><% = GetLocalResourceObject("btn_ApplyCaption")%></LABEL></TD>
		    <TD><%= mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insCopy()") %></TD>
        </TR>
        <%End If%>
    </TABLE>
	<%	
	Call insPreCA635()
End If

mobjGrid = Nothing
mobjValues = Nothing
lclsPolicy = Nothing
lclsProduct = Nothing
lclsGroups = Nothing
lclsFindCond_cover_premium = Nothing

%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA635")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




