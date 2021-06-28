<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues
Dim nRow As Integer



'% insDefineHeader: Se definen las columnas del grid de la ventana.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddHiddenColumn("cbeBranch", CStr(eRemoteDB.Constants.strNull))
			Call .AddHiddenColumn("valProduct", CStr(eRemoteDB.Constants.strNull))
			Call .AddHiddenColumn("valModulec", CStr(eRemoteDB.Constants.strNull))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnBranchColumnCaption"), "tctnBranch", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnBranchColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnProductColumnCaption"), "tctnProduct", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnProductColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnModulecColumnCaption"), "tctnModulec", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnModulecColumnToolTip"))
		Else
			Call .AddBranchColumn(40599, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),  , "",  ,  , "Setvalues(""Branch"")", Request.QueryString.Item("Action") = "Update")
			Call .AddProductColumn(40600, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"),  , CStr(eRemoteDB.Constants.intNull), 4,  ,  , "Setvalues(""Product"")", True)
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  , "Setvalues(""Modulec"")", Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valModulecColumnToolTip"),  ,  , True)
		End If
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddHiddenColumn("valCover", CStr(eRemoteDB.Constants.strNull))
			Call .AddHiddenColumn("valWay_Pay", CStr(eRemoteDB.Constants.strNull))
			Call .AddHiddenColumn("valSellchannell", CStr(eRemoteDB.Constants.strNull))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnCoverColumnCaption"), "tctnCover", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnCoverColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnWay_PayColumnCaption"), "tctnWay_Pay", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnWay_PayColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnSellchannellColumnCaption"), "tctnSellchannell", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnSellchannellColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TABLIFE_COVMOD", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  , "Setvalues(""Cover"")", Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valCoverColumnToolTip"),  ,  , True)
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valWay_PayColumnCaption"), "valWay_Pay", "table5002", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valWay_PayColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valSellchannellColumnCaption"), "valSellchannell", "table5532", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valSellchannellColumnToolTip"))
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMin_duratColumnCaption"), "tcnMin_durat", 4, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnMin_duratColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMax_duratColumnCaption"), "tcnMax_durat", 4, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnMax_duratColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(40597, GetLocalResourceObject("tcnPolicy_durColumnCaption"), "tcnPolicy_dur", 4, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnPolicy_durColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(40598, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddHiddenColumn("valCurrency", CStr(eRemoteDB.Constants.strNull))
			Call .AddTextColumn(0, GetLocalResourceObject("tctnCurrencyColumnCaption"), "tctnCurrency", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctnCurrencyColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCurrencyColumnCaption"), "valCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  , "EnabledAmount(this)",  , 4, GetLocalResourceObject("valCurrencyColumnToolTip"))
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tctnBranch").EditRecord = True
		Else
			.Columns("cbeBranch").EditRecord = True
		End If
		.Codispl = "MAG002"
		.Codisp = "MAG002"
		.sCodisplPage = "MAG002"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		.sDelRecordParam = "nComtabli=" & mobjValues.typeToString(Session("nComtabli"), eFunctions.Values.eTypeData.etdInteger) & "&dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nBranch='+ marrArray[lintIndex].cbeBranch + '" & "&nProduct='+ marrArray[lintIndex].valProduct + '" & "&nMin_durat='+marrArray[lintIndex].tcnMin_durat + '" & "&nPolicy_dur='+marrArray[lintIndex].tcnPolicy_dur + '" & "&nModulec='+marrArray[lintIndex].valModulec + '" & "&nCover='+marrArray[lintIndex].valCover + '" & "&nWay_Pay='+marrArray[lintIndex].valWay_Pay + '" & "&nSellChannell='+marrArray[lintIndex].valSellchannell +'"
		.Top = 50
		.Left = 200
		.Height = 460
		.Width = 450
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMAG002: Se carga el grid con los valores de la Base de datos.
'------------------------------------------------------------------------------
Private Sub insPreMAG002()
	'------------------------------------------------------------------------------
	Dim lcolDet_comlifs As eAgent.Det_comlifs
	Dim lclsDet_comlif As Object
	
	lcolDet_comlifs = New eAgent.Det_comlifs
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	If lcolDet_comlifs.Find(mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), nRow) Then
		
		For	Each lclsDet_comlif In lcolDet_comlifs
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsDet_comlif.nBranch
				.Columns("tctnBranch").DefValue = lclsDet_comlif.sDesc_Branch
				.Columns("valProduct").DefValue = lclsDet_comlif.nProduct
				.Columns("tctnProduct").DefValue = lclsDet_comlif.sProductDes
				.Columns("tcnMin_durat").DefValue = lclsDet_comlif.nMin_durat
				.Columns("tcnPolicy_dur").DefValue = lclsDet_comlif.nPolicy_dur
				.Columns("tcnPercent").DefValue = lclsDet_comlif.nPercent
				
				.Columns("valModulec").Parameters.Add("nBranch", lclsDet_comlif.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valModulec").Parameters.Add("nProduct", lclsDet_comlif.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valModulec").Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				.Columns("valModulec").DefValue = lclsDet_comlif.nModulec
				.Columns("tctnModulec").DefValue = lclsDet_comlif.sDesc_Modulec
				
				.Columns("valCover").Parameters.Add("nBranch", lclsDet_comlif.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valCover").Parameters.Add("nProduct", lclsDet_comlif.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valCover").Parameters.Add("nModulec", lclsDet_comlif.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valCover").Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				.Columns("valCover").DefValue = lclsDet_comlif.nCover
				.Columns("tctnCover").DefValue = lclsDet_comlif.sDesc_Cover
				.Columns("valWay_Pay").DefValue = lclsDet_comlif.nWay_Pay
				.Columns("tctnWay_Pay").DefValue = lclsDet_comlif.sDesc_Way_Pay
				.Columns("valSellchannell").DefValue = lclsDet_comlif.nSellChannel
				.Columns("tctnSellchannell").DefValue = lclsDet_comlif.sDesc_Sellchannel
				
				.Columns("tcnMax_durat").DefValue = lclsDet_comlif.nMax_durat
				If lclsDet_comlif.nCurrency <> eRemoteDB.Constants.intNull Then
					.Columns("valCurrency").DefValue = lclsDet_comlif.nCurrency
				End If
				.Columns("tctnCurrency").DefValue = lclsDet_comlif.sDesc_Currency
				.Columns("tcnAmount").DefValue = lclsDet_comlif.nAmount
				
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos en el grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsDet_comlif
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolDet_comlifs = Nothing
	
End Sub

'% insPreMAG002Upd: Se carga los valores de los parámetros para la eliminación
'% y luego se muestra la ventana de eliminación realizada.
'------------------------------------------------------------------------------
Private Sub insPreMAG002Upd()
	'------------------------------------------------------------------------------
	Dim lclsDet_comlif As eAgent.Det_comlif
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsDet_comlif = New eAgent.Det_comlif
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsDet_comlif
			.nComtabli = mobjValues.StringToType(Request.QueryString.Item("nComtabli"), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
			.nMin_durat = mobjValues.StringToType(Request.QueryString.Item("nMin_Durat"), eFunctions.Values.eTypeData.etdDouble)
			.nPolicy_dur = mobjValues.StringToType(Request.QueryString.Item("nPolicy_dur"), eFunctions.Values.eTypeData.etdDouble)
			.nModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdInteger)
			.nCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger)
			.nWay_Pay = mobjValues.StringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdInteger)
			.nSellChannel = mobjValues.StringToType(Request.QueryString.Item("nSellchannell"), eFunctions.Values.eTypeData.etdInteger)
			.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger)
			
			.Delete()
		End With
		
		lclsDet_comlif = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
		
		If Request.QueryString.Item("Action") = "Upd" Then
			.Write("<SCRIPT>Disabled();</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:34 $"

//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}	
//% Disabled: Se deshabilitan los campos de la página con la acción de "Update"
//------------------------------------------------------------------------
function Disabled()
//------------------------------------------------------------------------
{
    with (self.document.forms[0])
    {   
        cbeBranch.value = true
        valProduct.disabled = true   
        valModulec.disabled = true       
        valCover.disabled = true 
        valWay_Pay.disabled = true    
        valSellchanell.disabled = true   
        tcnMin_durat.disabled = true   
        tcnMax_durat.disabled = true   
        tcnPolicy_dur.disabled = true      
    }
}

//-------------------------------------------------------------------------------------------------------------
//%Setvalues: función que asigna los parámetros para los valores posibles de los campos
//            "Ramo", "Producto" y "Modulo" 
//-------------------------------------------------------------------------------------------------------------
function Setvalues(FieldValue)
//-------------------------------------------------------------------------------------------------------------
{
	with (self.document.forms[0]){
		if(FieldValue=='Branch'){
		    if(typeof(document.forms[0].cbeBranch)!='undefined'){
		        if(cbeBranch.value > 0){
		            valModulec.Parameters.Param1.sValue = cbeBranch.value;
		            valCover.Parameters.Param1.sValue = cbeBranch.value;
		            
            		<%If Request.QueryString.Item("Action") <> "Update" Then%>

                        self.document.forms[0].valModulec.value = '';
                        UpdateDiv('valModulecDesc','','Popup');
                
                        self.document.forms[0].valCover.value = '';
		                UpdateDiv('valCoverDesc','','Popup');
	    	            
	    	        <%End If%>
		        } else {
		            
                    valModulec.value = '';
                    valModulec.disabled = true;
                    btnvalModulec.disabled = true;
		            UpdateDiv('valModulecDesc','','Popup');
                
                    valCover.value = '';
                    valCover.disabled = true;
                    btnvalCover.disabled = true;
		            UpdateDiv('valCoverDesc','','Popup');
		        }
		    }    
		}
	}
    
    if(FieldValue=='Product'){
        if(typeof(document.forms[0].valProduct)!='undefined'){
            if(self.document.forms[0].valProduct.value > 0){
		        UpdateDiv('valModulecDesc','','Popup');
                UpdateDiv('valCoverDesc','','Popup');

      			self.document.forms[0].valModulec.Parameters.Param1.sValue = self.document.forms[0].cbeBranch.value;
				self.document.forms[0].valModulec.Parameters.Param2.sValue = self.document.forms[0].valProduct.value;
				self.document.forms[0].valModulec.Parameters.Param3.sValue = '<%=Session("deffecdate")%>';
                self.document.forms[0].valCover.Parameters.Param2.sValue=self.document.forms[0].valProduct.value;
                
            	<%If Request.QueryString.Item("Action") = "Update" Then%>
                    self.document.forms[0].valCover.disabled = true;
                    self.document.forms[0].btnvalCover.disabled = true;

	    	    <%Else%>
                    self.document.forms[0].valCover.disabled = false;
                    self.document.forms[0].btnvalCover.disabled = false;
                    self.document.forms[0].valModulec.value = '';
                    UpdateDiv('valModulecDesc','','Popup');
                
                    self.document.forms[0].valCover.value = '';
		            UpdateDiv('valCoverDesc','','Popup');
            	    ShowPopUp("/VTimeNet/Maintenance/MantAgent/ShowDefValues.aspx?Field=Modulec" + "&nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value, "ShowDefValuesModules", 1, 1,"no","no",2000,2000);
	    	    <%End If%>
            } else {
                self.document.forms[0].valModulec.value = '';
                self.document.forms[0].valModulec.disabled = true;
                self.document.forms[0].btnvalModulec.disabled = true;
		        UpdateDiv('valModulecDesc','','Popup');
                
                self.document.forms[0].valCover.value = '';
                self.document.forms[0].valCover.disabled = true;
                self.document.forms[0].btnvalCover.disabled = true;
		        UpdateDiv('valCoverDesc','','Popup');
            }
        }    
    }
    if(FieldValue=='Modulec'){
        if(self.document.forms[0].valModulec.value > 0){
			self.document.forms[0].valCover.Parameters.Param1.sValue = self.document.forms[0].cbeBranch.value;
			self.document.forms[0].valCover.Parameters.Param2.sValue = self.document.forms[0].valProduct.value;
			self.document.forms[0].valCover.Parameters.Param3.sValue=self.document.forms[0].valModulec.value;
			self.document.forms[0].valCover.Parameters.Param4.sValue = '<%=Session("deffecdate")%>';
        }
        else{
			UpdateDiv('valCoverDesc','','Popup');
			self.document.forms[0].valCover.Parameters.Param1.sValue = self.document.forms[0].cbeBranch.value;
			self.document.forms[0].valCover.Parameters.Param2.sValue = self.document.forms[0].valProduct.value;
			self.document.forms[0].valCover.Parameters.Param3.sValue = 0;
			self.document.forms[0].valCover.Parameters.Param4.sValue = '<%=Session("deffecdate")%>';
		}
    }
    if(FieldValue=='Cover'){
        if(self.document.forms[0].valCover.value == 0){
			self.document.forms[0].valCover.value = '';
			UpdateDiv('valCoverDesc','','Popup');
		}
    }
}

//EnabledAmount: Habilita el campo "Comisión fija", si se indica una moneda
//------------------------------------------------------------------------------------------------------------------
function EnabledAmount(Field)
//------------------------------------------------------------------------------------------------------------------
{
	if(Field.value!=0)
		self.document.forms[0].tcnAmount.disabled=false
	else {	
		self.document.forms[0].tcnAmount.disabled=true
		self.document.forms[0].tcnAmount.value='0,00'
	}		
}

</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>
    <%="<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>"%>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MAG002", "MAG002"))
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabLifeComm" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG002()
Else
	Call insPreMAG002Upd()
End If
%>	
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')")%>
<%
mobjValues = Nothing%>

</FORM>
</BODY>
</HTML>
<%
mobjGrid = Nothing
%>




