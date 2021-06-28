<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

    Dim mobjValues As New eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

Dim mlngCount As Integer
Dim mdblPaidAmount As Double
Dim mdblTotalAmount As Double
Dim mdblTotalAmountGen As Double

Dim mcolT_Conceptss As eCollection.T_conceptss


'% insPrevInf: Se encarga de obtener la información inicial de la carga de la transacción.
'---------------------------------------------------------------------------------------------------------
Private Sub insPrevInf()
	'---------------------------------------------------------------------------------------------------------      
	'+ Se definen las propiedades generales del grid
	Call mcolT_Conceptss.FindCO823(Session("co001_nAction"), Session("nBordereaux"), Session("dCollectDate"), Session("dValueDate"), Session("sRelOrigi"))
	
	mlngCount = mcolT_Conceptss.nCount
	mdblPaidAmount = System.Math.Round(mcolT_Conceptss.nPaidAmount)
	mdblTotalAmount = System.Math.Round(mcolT_Conceptss.nTotalAmount)
	mdblTotalAmountGen = System.Math.Round(mcolT_Conceptss.nTotalAmountGen)
	
End Sub

'+ insDefineHeader:Se define el encabezado del grid
'-----------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "CO823"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		.ActionQuery = CStr(Session("co001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery)
		With .Columns
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", "tabconceptscash", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ChangeConcept(this.value, '" & Request.QueryString.Item("Action") & "');", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("valConceptColumnToolTip"))
			lobjColumn.Parameters.Add("nCompany", Session("nCompanyUser"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "inShowAmouting(this,""Currency"");", True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountOrigColumnCaption"), "tcnAmountOrig", 18, CStr(0),  , GetLocalResourceObject("tcnAmountOrigColumnToolTip"), True, 6,  ,  , "inShowAmouting(this,""Amount"");", True)
			Call .AddDateColumn(0, GetLocalResourceObject("tcdValuedateColumnCaption"), "tcdValuedate", CStr(Today),  , GetLocalResourceObject("tcdValuedateColumnToolTip"),  ,  , "inShowAmouting(this,""Valuedate"");", False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountLocColumnCaption"), "tcnAmountLoc", 18, CStr(0),  , GetLocalResourceObject("tcnAmountLocColumnToolTip"), True,  ,  ,  , "inShowAmouting(this,""AmountLoc"");", False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 11, CStr(0),  , GetLocalResourceObject("tcnExchangeColumnCaption"), True, 6,  ,  ,  , True)
                'lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valBank_agreeColumnCaption"), "valBank_agree", "tabBank_Agree_Banks", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "GetCod_Agree()",  ,  , GetLocalResourceObject("valBank_agreeColumnToolTip"))
                'lobjColumn.Parameters.Add("sType_BankAgree", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
                'lobjColumn = .AddPossiblesColumn(40595, GetLocalResourceObject("valAccount_AgreeColumnCaption"), "valAccount_Agree", "tabBank_Agree", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , True, 5, GetLocalResourceObject("valAccount_AgreeColumnToolTip"))
                'lobjColumn.Parameters.Add("nBank_code", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valAgreementColumnCaption"), "valAgreement", "tabAgreementCO823", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  , "GetClientAgreement()",  ,  , GetLocalResourceObject("valAgreementColumnToolTip"))
			Call .AddDateColumn(0, GetLocalResourceObject("tcdCollectColumnCaption"), "tcdCollect", "",  , GetLocalResourceObject("tcdCollectColumnToolTip"),  ,  ,  , False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnBulletinsColumnCaption"), "tcnBulletins", 10, vbNullString,  , GetLocalResourceObject("tcnBulletinsColumnToolTip"), False,  ,  ,  ,  , True)
			Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"), "ChangeValues()", True, "lblCliename",  ,  ,  ,  ,  ,  ,  , True)
			
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnClaimColumnCaption"), "tcnClaim", 10, vbNullString,  , GetLocalResourceObject("tcnClaimColumnToolTip"),  ,  ,  ,  , "self.document.forms[0].valCases.value='';UpdateDiv('valCasesDesc','');self.document.forms[0].btnvalCases.disabled=(this.value=='');self.document.forms[0].valCases.Parameters.Param1.sValue=this.value;", True)
			
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valCasesColumnCaption"), "valCases", "TabClaim_cases", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  , 16, "GetCase_Info(this.value);", True, 20, GetLocalResourceObject("valCasesColumnToolTip"), eFunctions.Values.eTypeCode.eString)
			If mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
				lobjColumn.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				lobjColumn.Parameters.Add("nClaim", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valCurrAccColumnCaption"), "valCurrAcc", "TabTable400", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ChangeType_Acco(this.value);", True,  , GetLocalResourceObject("valCurrAccColumnToolTip"))
			lobjColumn.Parameters.Add("nConcept", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermedColumnCaption"), "valIntermed", "TabIntermedia", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  , "ChangeIntermed(this.value);self.document.forms[0].valLoans.Parameters.Param1.sValue=this.value;", Request.QueryString.Item("Action") <> "Add", 10, GetLocalResourceObject("valIntermedColumnToolTip"))
			
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valLoansColumnCaption"), "valLoans", "TabLoans_IntBPB", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ChangeLoan('" & Request.QueryString.Item("Action") & "');", True, 5, GetLocalResourceObject("valLoansColumnToolTip"))
			lobjColumn.Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			lobjColumn.Parameters.ReturnValue("nBalanLoan", True, "Saldo", True)
			lobjColumn.Parameters.ReturnValue("nCurrency", False, "", True)
			
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCompanyCRColumnCaption"), "valCompanyCR", "Company", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCompanyCRColumnToolTip"))
			Call .AddButtonColumn(0, GetLocalResourceObject("SCA2-NColumnCaption"), "SCA2-N", eRemoteDB.Constants.intNull, True, Request.QueryString.Item("Type") <> "PopUp",  ,  ,  ,  , "btnNotenum")
			
			Call .AddHiddenColumn("nTransac", "0")
			Call .AddHiddenColumn("hddnProponum", "")
			'Call .AddHiddenColumn("tcnExchange","")
			Call .AddHiddenColumn("hddCase_num", "")
			Call .AddHiddenColumn("hddDeman_type", "")
                Call .AddHiddenColumn("valBank_agree", "")
                Call .AddHiddenColumn("valAccount_Agree", "")
            End With
		
		.FieldsByRow = 2
		.Top = 40
		.Left = 20
		.Height = 490
		.Width = 760
		.Codispl = "CO823"
		.AddButton = True
		.Columns("Sel").GridVisible = Not (CStr(Session("co001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery))
		
		.sDelRecordParam = "nTransac=' + marrArray[lintIndex].nTransac + '"
		
		'+ Permite continuar si el check está marcado        
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'+ insPreCO823upd: Actualiza un dato del registro
'--------------------------------------------------------------------------
Private Sub insPreCO823Upd()
	'--------------------------------------------------------------------------
	If Request.QueryString.Item("Action") = "Del" Then
		Call insDelItem()
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValCollectionSeq.aspx", "CO823", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

'+ insPreCO823: Funcion que carga los valores del Grid
'------------------------------------------------------------------------------------------------------
Private Sub insPreCO823()
	'------------------------------------------------------------------------------------------------------
	Dim ldblTotals As Double
	Dim lintIndex As Integer
	Dim lclsT_Concepts As Object
	
	ldblTotals = System.Math.Abs(mdblTotalAmountGen)
	
	'+ Se definen las propiedades generales del grid
	If mlngCount > 0 Then
		With mobjGrid
			lintIndex = 0
			For	Each lclsT_Concepts In mcolT_Conceptss
				lintIndex = lintIndex + 1
				
				.Columns("nTransac").DefValue = lclsT_Concepts.nTransac
				.Columns("valConcept").DefValue = lclsT_Concepts.nConcept
				.Columns("valConcept").Descript = lclsT_Concepts.sConcept
				.Columns("cbeCurrency").DefValue = lclsT_Concepts.nCurrency
				.Columns("cbeCurrency").Descript = lclsT_Concepts.sCurrency
				.Columns("tcnAmountOrig").DefValue = lclsT_Concepts.nOriAmount
				.Columns("tcdValuedate").DefValue = lclsT_Concepts.dValDate
				.Columns("tcnAmountLoc").DefValue = lclsT_Concepts.nAmount
                    '				.Columns("valBank_agree").DefValue = lclsT_Concepts.nBank_Agree
                    '				.Columns("valBank_agree").Descript = lclsT_Concepts.sBank_Agree
                    '				.Columns("valAccount_Agree").Parameters.Add("nBank_code", lclsT_Concepts.nBank_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    '                    '				.Columns("valAccount_Agree").DefValue = lclsT_Concepts.nAccount
                    '				.Columns("valAccount_Agree").Descript = lclsT_Concepts.sAccount
				.Columns("valAgreement").DefValue = lclsT_Concepts.nAgreement
				
				If lclsT_Concepts.nAgreement > 0 Then
					.Columns("valAgreement").Descript = lclsT_Concepts.nAgreement & "-" & lclsT_Concepts.sAgreement
				End If
				
				.Columns("tcdCollect").DefValue = lclsT_Concepts.Dcollection
				.Columns("dtcClient").DefValue = lclsT_Concepts.sClient
				.Columns("dtcClient").Descript = lclsT_Concepts.sCliename
				.Columns("tcnClaim").DefValue = lclsT_Concepts.nClaim
				
				.Columns("valCases").DefValue = lclsT_Concepts.sCasenum
				.Columns("valCases").Descript = lclsT_Concepts.sCasenum
				.Columns("valCurrAcc").DefValue = lclsT_Concepts.nTyp_acco
				.Columns("valIntermed").DefValue = lclsT_Concepts.nIntermed
				.Columns("valIntermed").Descript = lclsT_Concepts.sIntermed
				
				.Columns("valLoans").DefValue = lclsT_Concepts.nLoan
				.Columns("valLoans").Descript = lclsT_Concepts.sLoan
				
				.Columns("valCompanyCR").DefValue = lclsT_Concepts.nCompany
				.Columns("tcnBulletins").DefValue = lclsT_Concepts.nBulletins
				.Columns("valAccount_Agree").DefValue = lclsT_Concepts.nAccount
				.Columns("valAccount_Agree").Descript = lclsT_Concepts.sAccount
				.Columns("btnNotenum").nNotenum = lclsT_Concepts.nNotenum
				.Columns("tcnExchange").DefValue = lclsT_Concepts.nExchange
				.Columns("tcnAmountOrig").EditRecord = True
				
				mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(ldblTotals, eFunctions.Values.eTypeData.etdDouble, True, 0) & "&nClaim=" & lclsT_Concepts.nClaim
				
				Response.Write(.DoRow)
			Next lclsT_Concepts
		End With
		Response.Write(mobjValues.HiddenControl("nItems", lintIndex))
	End If
	Response.Write(mobjGrid.closeTable)
	
	Response.Write("<SCRIPT>")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & mobjValues.TypeToString(mdblTotalAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn','" & mobjValues.TypeToString(mdblPaidAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(mdblTotalAmountGen, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
	Response.Write("</" & "Script>")
	
	mcolT_Conceptss = Nothing
End Sub

'+ insDelItem: función que elimina el item de la relación    
'----------------------------------------------------------------------------
Private Sub insDelItem()
	'----------------------------------------------------------------------------    
	Dim lclsT_Concepts As eCollection.T_concepts
	
	lclsT_Concepts = New eCollection.T_concepts
	
	lclsT_Concepts.Del_T_Concepts(Session("nBordereaux"), CInt(Request.QueryString.Item("nTransac")))
	lclsT_Concepts = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO823")

mcolT_Conceptss = New eCollection.T_conceptss

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "CO823"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>





	<%=mobjValues.StyleSheet()%>

<SCRIPT LANGUAGE= "JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 15 $|$$Date: 16/04/04 11:08 $"
    
    var nMainAction = 304; nAmountPayJS = VTFormat('0', '', '', '', 6, true) ; nAmountPayLocJS = VTFormat('0', '', '', '', 6, true) ; nInterestPayJS = VTFormat('0', '', '', '', 6, true); nLastAmountModify = 1;
    
	<%If Not IsNothing(Request.QueryString.Item("Index")) Then%>
        var nIndex = <%=Request.QueryString.Item("Index")%>
	<%End If%>        


//%ChangeLoan: Obtiene la información al abandonar el campo de anticipo 
//-------------------------------------------------------------------------------------------------------------------------
function ChangeLoan(sAction){
//-------------------------------------------------------------------------------------------------------------------------
    if (sAction=='Add'){ 
        with (self.document.forms[0]){
//+ Se se trata de 46)Abono de anticipo de comisiones
        	if(valConcept.value=="46"){
        		cbeCurrency.value = valLoans_nCurrency.value;
                cbeCurrency.disabled = true;                
                
                if(valLoans_nCurrency.value!=1){
                    tcnAmountOrig.value = valLoans_nBalanLoan.value;                    
	                if (nAmountPayJS!=valLoans_nBalanLoan.value){
	        	        insDefValues("ConvertAmounting", "nCurrency_ing=" + valLoans_nCurrency.value + "&nAmount=" + valLoans_nBalanLoan.value + "&dReqDate=" + tcdValuedate.value, "/VTIME/Collection/Collectionseq");
	        	        nAmountPayJS=valLoans_nBalanLoan.value;
	        	        nLastAmountModify = 2;
	        	    }
                }
                else{
                    tcnAmountLoc.value = VTFormat(valLoans_nBalanLoan.value, '', '', '', 0, true);                    
	                if (nAmountPayLocJS!=tcnAmountLoc.value){
	        	        insDefValues("ConvertAmountingLoc", "nCurrency_ing=" + valLoans_nCurrency.value + "&nAmount=" + valLoans_nBalanLoan.value + "&dReqDate=" + tcdValuedate.value, "/VTIME/Collection/Collectionseq");
	        	        nAmountPayLocJS=tcnAmountLoc.value;
	        	        nLastAmountModify = 1;
	        	    }
                }
        	}
        }
    }    
}

//%ChangeLoan: Obtiene la información al abandonar el campo de anticipo 
//-------------------------------------------------------------------------------------------------------------------------
function ConvertAmounting(sAction){
//-------------------------------------------------------------------------------------------------------------------------
    if (sAction=='Add'){ 
        with (self.document.forms[0]){
//+ Se se trata de 46)Abono de anticipo de comisiones
        	if(valConcept.value=="46"){
        		cbeCurrency.value = valLoans_nCurrency.value;
                cbeCurrency.disabled = true;                
                
                if(valLoans_nCurrency.value!=1){
                    tcnAmountOrig.value = valLoans_nBalanLoan.value;                    
	                if (nAmountPayJS!=valLoans_nBalanLoan.value){
	        	        insDefValues("ConvertAmounting", "nCurrency_ing=" + valLoans_nCurrency.value + "&nAmount=" + valLoans_nBalanLoan.value + "&dReqDate=" + tcdValuedate.value, "/VTimeNet/Collection/Collectionseq");
	        	        nAmountPayJS=valLoans_nBalanLoan.value;
	        	        nLastAmountModify = 2;
	        	    }
                }
                else{
                    tcnAmountLoc.value = VTFormat(valLoans_nBalanLoan.value, '', '', '', 0, true);                    
	                if (nAmountPayLocJS!=tcnAmountLoc.value){
	        	        insDefValues("ConvertAmountingLoc", "nCurrency_ing=" + valLoans_nCurrency.value + "&nAmount=" + valLoans_nBalanLoan.value + "&dReqDate=" + tcdValuedate.value, "/VTimeNet/Collection/Collectionseq");
	        	        nAmountPayLocJS=tcnAmountLoc.value;
	        	        nLastAmountModify = 1;
	        	    }
                }
        	}
        }
    }    
}

//%GetCod_Agree: Obtiene el código interno de la cuenta bancaria para asignarla al número de convenio 
//               Si los conceptos son: Pago en ventanilla o Deposito PAC/Transbank
//-------------------------------------------------------------------------------------------------------------------------
function GetCod_Agree(){
//-------------------------------------------------------------------------------------------------------------------------
    if(self.document.forms[0].valConcept.value == "36" || self.document.forms[0].valConcept.value == "29" || self.document.forms[0].valConcept.value == "38")
        insDefValues("Cod_Agree", "nBank_Agree=" + self.document.forms[0].valBank_agree.value + "&nConcept=" + self.document.forms[0].valConcept.value, '/VTimeNet/Collection/Collectionseq');
}

//%GetCase_Info: Obtiene la informacion correspondiente al caso
//-------------------------------------------------------------------------------------------------------------------------
function GetCase_Info(sCase){
//-------------------------------------------------------------------------------------------------------------------------
    var sCasenum, sDeman_type, nClaim;
            
    sCasenum = new String;
    sDeman_type = new String;

    nClaim = document.frmCO823.tcnClaim.value;
    
    if(sCase!=""){
// Se obtiene el número de caso nCase_num    
	    sCasenum = sCase.substr(0,sCase.length-(sCase.length-sCase.indexOf('/')));
// Se obtiene el número de nDeman_type    
		sDeman_type = sCase.substr(sCase.indexOf('/')+1,(sCase.indexOf('/',sCase.indexOf('/')+1)-sCase.indexOf('/'))-1);

        insDefValues("Case_Info", "sCase_num=" + sCasenum + "&sDeman_type=" + sDeman_type + "&nClaim="+nClaim, '/VTimeNet/Collection/Collectionseq');		
    }    
        
}
//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//Enlace NovaRed.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   var sCli=document.frmCO823.dtcClient.value;
   var sDig=document.frmCO823.dtcClient_Digit.value;
   var sFor=self.document.forms[0].name;
   with (self.document.forms[0]){
    	if(sCli!=""){
    		insDefValuesNR('Client', 'sClient=' + sCli, 'sDigit=' + sDig , 'sForm=' + sFor, '/VTimeNet/Collection/CollectionSeq')
    	}
    }
}

//%	inShowAmouting: Cuando se cambia  de la moneda de ingreso o la fecha de valorización
//-------------------------------------------------------------------------------------------
function inShowAmouting(Field,sChange){ 
//-------------------------------------------------------------------------------------------   
	switch(sChange){
		case "Currency":
	        with (self.document.forms[0]){
	            if (cbeCurrency.value!=0){
	                if (nLastAmountModify==1){
	        	        insDefValues("ConvertAmountingLoc", "nCurrency_ing=" + Field.value + "&nAmount=" + tcnAmountLoc.value + "&dReqDate=" + tcdValuedate.value, "/VTimeNet/Collection/Collectionseq");
	        	    }
	        	    else{
	        	        insDefValues("ConvertAmounting", "nCurrency_ing=" + Field.value + "&nAmount=" + tcnAmountOrig.value + "&dReqDate=" + tcdValuedate.value, "/VTimeNet/Collection/Collectionseq");
	        	    }
	        	}    
		    }	
		break;
		case "Amount":
	        with (self.document.forms[0]){
	            if (cbeCurrency.value!=0 && tcnAmountOrig.value!=0){
	                if (nAmountPayJS!=Field.value){
	        	        insDefValues("ConvertAmounting", "nCurrency_ing=" + cbeCurrency.value + "&nAmount=" + Field.value + "&dReqDate=" + tcdValuedate.value, "/VTimeNet/Collection/Collectionseq");
	        	        nAmountPayJS=Field.value;	        	            
	        	        nLastAmountModify = 2;
	        	    }
	        	}    
	        	else{    
	        	    tcnAmountOrig.value = VTFormat('0', '', '', '', 0, true); 
	        	    tcnAmountLoc.value  = VTFormat('0', '', '', '', 0, true);
	    			nAmountPayJS = VTFormat('-999999', '', '', '', 0, true);
	        	}
		    }
		break;    
		case "AmountLoc":
	        with (self.document.forms[0]){
	            if (cbeCurrency.value!=0 && tcnAmountLoc.value!=0){
	                if (nAmountPayLocJS!=Field.value){
	        	        insDefValues("ConvertAmountingLoc", "nCurrency_ing=" + cbeCurrency.value + "&nAmount=" + Field.value + "&dReqDate=" + tcdValuedate.value, "/VTimeNet/Collection/Collectionseq");
	        	        nAmountPayLocJS=Field.value;
	        	        nLastAmountModify = 1;
	        	    }
	        	}    
	        	else{
	        	    tcnAmountOrig.value = VTFormat('0', '', '', '', 0, true); 
	        	    tcnAmountLoc.value  = VTFormat('0', '', '', '', 0, true);
	    			nAmountPayLocJS = VTFormat('-999999', '', '', '', 0, true);
	        	}
		    }
		break;
	    case "Valuedate":
	        with (self.document.forms[0]){
	            if (cbeCurrency.value!=0 && tcnAmountOrig.value!=0){
	                if (nLastAmountModify==1){
	        	        insDefValues("ConvertAmountingLoc", "nCurrency_ing=" + cbeCurrency.value + "&nAmount=" + tcnAmountLoc.value + "&dReqDate=" + Field.value, "/VTimeNet/Collection/Collectionseq");
	        	    }
	        	    else{
	        	        insDefValues("ConvertAmounting", "nCurrency_ing=" + cbeCurrency.value + "&nAmount=" + tcnAmountOrig.value + "&dReqDate=" + Field.value, "/VTimeNet/Collection/Collectionseq");
	        	    }    
	        	}    
		    }
		break;
	}
}
// ChangeType_Acco: Verifica el tipo de cuenta para habilitar o no el campo intermediario
//-------------------------------------------------------------------------------------------
function ChangeType_Acco(sType_acco){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (valConcept.value==10) {
            if (sType_acco==1 || sType_acco==10){
                valIntermed.disabled = false;
                btnvalIntermed.disabled = false;
            }
            else{
                valIntermed.value="";
                UpdateDiv('valIntermedDesc',"");
                valIntermed.disabled = true;
                btnvalIntermed.disabled = true;
            }
        }
    }
}

//ChangeConcept: Cambia y asigna los valores según la opción seleccionada.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeConcept(sConcept, sAction){
//-------------------------------------------------------------------------------------------
	var lblnAdd = (sAction=='Add'?true:false);
    
    with(self.document.forms[0]){
       if (sConcept !="") {
           if (sConcept==46)
               cbeCurrency.disabled = true;
           else
               cbeCurrency.disabled = false;
                    
           tcnAmountOrig.disabled = false;
           tcnAmountLoc.disabled = false;
           dtcClient.disabled = false;
           dtcClient_Digit.disabled = false;
           btndtcClient.disabled = false;
           
/*+Conceptos: 29-Pac/trans bank 36-Pago en ventanilla*/
//           if (sConcept=="29" || sConcept=="36"){
//               valBank_agree.disabled = false;
 //              btnvalBank_agree.disabled = false;
			   
//			   if (sConcept=="29")
//			       valBank_agree.Parameters.Param1.sValue=1;
//			   else    
//			       valBank_agree.Parameters.Param1.sValue=2;
 //          }    
  //         else
  //         {
 //               valBank_agree.disabled = true;
 //               btnvalBank_agree.disabled = true;
                dtcClient.disabled = false;
                dtcClient_Digit.disabled = false;
			   
                if (lblnAdd) {
//                    valBank_agree.value="";
 //                   UpdateDiv('valBank_agreeDesc',"");
  //                  valAccount_Agree.value="";
   //                 UpdateDiv('valAccount_AgreeDesc',"");
   			        dtcClient.value = "";
			        dtcClient_Digit.value = "";
			        UpdateDiv('lblCliename',"");
//			    }
           }
           
/*+Conceptos: 29-Pac/trans bank 36-Pago en ventanilla 38-descuento por planilla*/
           if (sConcept=="29" || sConcept=="36" || sConcept=="38"|| sConcept=="241"|| sConcept=="243"){
               tcdCollect.disabled = false;
               btn_tcdCollect.disabled = false;
           }    
           else
           {
                if (lblnAdd) {
                    tcdCollect.value="";
                }
                
                tcdCollect.disabled = true;
                btn_tcdCollect.disabled = true;
   			    dtcClient.disabled = false;
			    dtcClient_Digit.disabled = false;
			    btndtcClient.disabled = false;			   
           }           
           
/*+Conceptos: 4-Recobro de siniestro 31-Salvatje 32-Deducible 114-Depreciación*/
           if (sConcept=="4" || sConcept=="30" || sConcept=="31" || sConcept=="32" || sConcept=="114"){
               tcnClaim.disabled = false;
           }    
           else
           {
                if (lblnAdd) {
                    tcnClaim.value="";
                    valCases.value="";
                }
                
                tcnClaim.disabled = true;
                valCases.disabled = true;
                btnvalCases.disabled = true;
           }
           
/*+Conceptos: 35-Gastos de cobranza*/
           if (sConcept=="35"){
               tcnBulletins.disabled = false;
           }    
           else
           {
                if (lblnAdd) {
                    tcnBulletins.value="";
                }
                
                tcnBulletins.disabled = true;
           }           
/*se comenta hasta nuevo analisis           
+Conceptos: 26-Pago por propuesta
           if (sConcept=="26"){
               tcnProponum.disabled = false;
           }    
           else
           {
                if (lblnAdd) {
                    tcnProponum.value="";
                }
                
                tcnProponum.disabled = true;
           }           
*/

/*+Conceptos: 38-Descuento por planilla*/
           if (sConcept=="38"|| sConcept=="241"|| sConcept=="243"){
               valAgreement.disabled = false;
               btnvalAgreement.disabled = false;
           }    
           else
           {
                valAgreement.disabled = true;
                btnvalAgreement.disabled = true;
   			    dtcClient.disabled = false;
			    dtcClient_Digit.disabled = false;
                if (lblnAdd) {
                    valAgreement.value="";
                    UpdateDiv('valAgreementDesc',"");
                    dtcClient.value = "";
			        dtcClient_Digit.value = "";
			        UpdateDiv('lblCliename',"");
                }
			        
           }
           
/*+Conceptos: 10-Ingreso por cuenta corriente*/
           if (sConcept=="10"){
               valCurrAcc.disabled = false;
               btnvalCurrAcc.disabled = false;
           }    
           else
           {
                if (lblnAdd) {
                    valCurrAcc.value="";
                    UpdateDiv('valCurrAccDesc',"");
                }
                valCurrAcc.disabled = true;
                btnvalCurrAcc.disabled = true;
           }
           
/*+Conceptos: 2-Remesa de agente o 46-Abono de anticipo de comisión*/
           if (sConcept=="2" ||
               sConcept=="46"){
               valIntermed.disabled = !lblnAdd;
               btnvalIntermed.disabled = !lblnAdd;
           }    
           else
           {
               if (lblnAdd) {
                    valIntermed.value="";
                    UpdateDiv('valIntermedDesc',"");
               }
               
               valIntermed.disabled = true;
               btnvalIntermed.disabled = true;
           }
           
/*+Conceptos: 3-Remesa de Co/reaseguro*/
           if (sConcept=="3"){
               valCompanyCR.disabled = false;
               btnvalCompanyCR.disabled = false;
           }    
           else
           {
                if (lblnAdd) {
                    valCompanyCR.value="";
                    UpdateDiv('valCompanyCRDesc',"");
                }
                
                valCompanyCR.disabled = true;
                btnvalCompanyCR.disabled = true;
           }
           
           insDefValues("Valuedate", "nConcept=" + sConcept, "/VTimeNet/Collection/Collectionseq");           
       }
       else{
/*+ se inhabilitan los campos si no se ha seleccionado un concepto*/                  
            cbeCurrency.disabled = true;
            tcnAmountOrig.disabled = true;
            tcnAmountLoc.disabled = true;
            dtcClient.disabled = true;
            dtcClient_Digit.disabled = true;
            btndtcClient.disabled = true;
//            valBank_agree.disabled = true;
 //           btnvalBank_agree.disabled = true;
            valAgreement.disabled = true;
            btnvalAgreement.disabled = true;
            tcdCollect.disabled = true;
            btn_tcdCollect.disabled = true;
            tcnBulletins.disabled = true;  
            tcnClaim.disabled = true;
            btnvalCases.disabled = true;
            valCurrAcc.disabled = true;
            btnvalCurrAcc.disabled = true;
            valIntermed.disabled = true;
            btnvalIntermed.disabled = true;
            valCompanyCR.disabled = true;
            btnvalCompanyCR.disabled = true;
           
//           tcnProponum.disabled = true;                    
//           tcnProponum.value="";

/*+ se limpian los campos si no se ha seleccionado un concepto*/
            if (lblnAdd) {
                valCompanyCR.value="";
                UpdateDiv('valCompanyCRDesc',"");
                valIntermed.value="";
                UpdateDiv('valIntermedDesc',"");
                valCurrAcc.value="";
                UpdateDiv('valCurrAccDesc',"");
                tcnClaim.value="";
                valCases.value="";
                tcnBulletins.value="";
                tcdCollect.value="";
                cbeCurrency.value ="";
                tcnAmountOrig.value ="";
                tcnAmountLoc.value ="";
                dtcClient.value ="";
                dtcClient_Digit.value ="";
                UpdateDiv('lblCliename',"");
   //             valBank_agree.value="";
    //            UpdateDiv('valBank_agreeDesc',"");
   //             valAccount_Agree.value="";
   //             UpdateDiv('valAccount_AgreeDesc',"");
                valAgreement.value="";
                UpdateDiv('valAgreementDesc',"");
            }
        }
    }
}

// GetClientAgreement: Obtiene el cliente del Convenio-descuento por planilla
//-------------------------------------------------------------------------------------------
function GetClientAgreement(){
//-------------------------------------------------------------------------------------------
    if(self.document.forms[0].valConcept.value == "38"|| self.document.forms[0].valConcept.value=="241"|| self.document.forms[0].valConcept.value=="243")        
        if (self.document.forms[0].valAgreement.value!=0)
			insDefValues("Client_Agree", "nAgreement=" + self.document.forms[0].valAgreement.value, '/VTimeNet/Collection/Collectionseq');
		else{
            self.document.forms[0].dtcClient.value ="";
            self.document.forms[0].dtcClient_Digit.value ="";
            UpdateDiv('lblCliename',"");
            }
}

// ChangeIntermed: Cuando se cambia el intermediario
//-------------------------------------------------------------------------------------------
function ChangeIntermed(nItermed){
//-------------------------------------------------------------------------------------------
   if (nItermed!=0 &&
       nItermed!=''&&
       self.document.forms[0].valConcept.value==46){
       self.document.forms[0].valLoans.disabled=false;
       self.document.forms[0].btnvalLoans.disabled=false;
   }
}

</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CO823", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	End If
	mobjMenu = Nothing
	.Write(mobjValues.ShowWindowsName("CO823", Request.QueryString.Item("sWindowDescript")))
End With
%>            
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCO823" ACTION="valCollectionSeq.aspx?time=1">
<%
Call insPrevInf()
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCO823()
Else
	Call insPreCO823Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage("CO823")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




