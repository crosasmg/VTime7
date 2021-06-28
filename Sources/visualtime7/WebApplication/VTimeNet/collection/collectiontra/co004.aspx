<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues
    '~End Body Block VisualTimer Utility

    '- Se define las variables para la carga de datos en la ventana	
    Dim mclsDir_debit As eCollection.Dir_debit
    Dim mclsCertificat As ePolicy.Certificat
    Dim mclsPolicy As ePolicy.Policy
    Dim mclsDirdebit As ePolicy.DirDebit
    Dim mclsProduct_li As eProduct.Product
    Dim mclsAgreement_pol As ePolicy.Agreement_pols
    

    Dim lcolApv_origin As Object
    Dim lclsApv_origin As Object
    Dim mobjGrid As eFunctions.Grid
    Dim mintMonth As String
    Dim sClientPay As String
    Dim sClientEmp As String


    Dim sTyp_dirdeb As String
    Dim sWay_Pay As Object
    Dim sCod_Agree As Object
    Dim byPolicy As Object
    Dim sDisableOption As Object
    Dim bDisabledAgreement As Boolean


'% insPreCA004: hace la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------------------------
Private Sub insPreCO004()
	'----------------------------------------------------------------------------------------------   
	Dim lcolRoles As ePolicy.Roles
	With mobjValues
		Select Case Request.QueryString.Item("nWayPay")
			Case "1", "2"
				
				Response.Write(mobjValues.HiddenControl("hddblnIsIntermed", CStr(1)))
			Case Else
				Response.Write(mobjValues.HiddenControl("hddblnIsIntermed", CStr(2)))
		End Select
	End With
	
	lcolRoles = New ePolicy.Roles
	
	If lcolRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 25, "", Today, True) Then
		sClientPay = lcolRoles.sClient
	End If
	If lcolRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 85, "", Today, True) Then
		sClientEmp = lcolRoles.sClient
	End If
	lcolRoles = Nothing
	
	
End Sub


Private Sub InsPreVI8002A()
        '--------------------------------------------------------------------------------------------
        '- Objetos para el manejo de los datos repetitivos de la página
	Dim lcolApv_origin As ePolicy.Apv_origins
	Dim lclsApv_origin As Object
    Dim lcolTabGen As eGeneralForm.TabGen
	If mclsProduct_li.sApv = "1" Then
		lcolApv_origin = New ePolicy.Apv_origins
		
		If lcolApv_origin.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), 0, Today, 0) Then
			For	Each lclsApv_origin In lcolApv_origin
				With mobjGrid
					.Columns("valOrigin").Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Columns("valOrigin").Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Columns("valOrigin").DefValue = lclsApv_origin.nOrigin
					.Columns("tcnPercent").DefValue = lclsApv_origin.nPercent
					.Columns("tcnPremDeal_anu").DefValue = lclsApv_origin.nPremDeal_anu
					.Columns("tcnPremDeal").DefValue = lclsApv_origin.nPremDeal
					Response.Write(.DoRow)
				End With
			Next lclsApv_origin
		End If
		
		lcolApv_origin = Nothing
		lclsApv_origin = Nothing
	Else
		
		lcolTabGen = New eGeneralForm.TabGen
		If lcolTabGen.Find("Table5633", CStr(mclsCertificat.nOrigin)) Then
			mobjGrid.Columns("valOrigin").DefValue = lcolTabGen.sDescript
		End If
		Response.Write(mobjGrid.DoRow())
	End If
	Response.Write(mobjGrid.closeTable())
End Sub
    
    Private Sub InsDefineHeaderA()
        '--------------------------------------------------------------------------------------------
        Dim mobjGrid2 As eFunctions.Columns
        mobjGrid2 = New eFunctions.Columns
        mobjGrid = New eFunctions.Grid
        mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
        mobjGrid.AddButton = False
        mobjGrid.DeleteButton = False
	
        '+ Se definen las columnas del grid
        If mclsProduct_li.sApv = "1" Then
            With mobjGrid.Columns
                .AddPossiblesColumn(0, GetLocalResourceObject("valOriginColumnCaption"), "valOrigin", "TAB_ORD_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("valOriginColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
                .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, vbNullString, , GetLocalResourceObject("tcnPercentColumnToolTip"), True, 2, , , "insChangeValuesPop('tcnPercent', this.value, '')")
                .AddNumericColumn(0, GetLocalResourceObject("tcnPremDeal_anuColumnCaption"), "tcnPremDeal_anu", 18, vbNullString, , GetLocalResourceObject("tcnPremDeal_anuColumnToolTip"), True, 6, , , "insChangeValuesPop('tcnPremDeal_anu', this.value, '')")
                .AddNumericColumn(0, GetLocalResourceObject("tcnPremDealColumnCaption"), "tcnPremDeal", 18, vbNullString, , GetLocalResourceObject("tcnPremDealColumnToolTip"), True, 6, , , , True)
                .AddHiddenColumn("hddMonth", mintMonth)
            End With
            '+ Se definen las propiedades generales del grid
            mobjGrid.Columns("valOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("valOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        Else
            With mobjGrid.Columns
                Call .AddTextColumn(0, GetLocalResourceObject("valOriginColumnCaption"), "valOrigin", 30, vbNullString, False, GetLocalResourceObject("valOriginColumnCaption"))
            End With
        End If
	
	
        mobjGrid.Columns("sel").GridVisible = False
    End Sub

</script>
<%
    Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("co004")
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "co004"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")

    mclsDir_debit = New eCollection.Dir_debit
    mclsCertificat = New ePolicy.Certificat
    mclsPolicy = New ePolicy.Policy
    mclsDirdebit = New ePolicy.DirDebit
    mclsProduct_li = New eProduct.Product
    mclsAgreement_pol = New ePolicy.Agreement_pols
        

    Call mclsProduct_li.FindProduct_li(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), Today)

    If Request.QueryString.Item("sTypeDoc") = "1" Then
	
        Call mclsCertificat.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")))
	
        sWay_Pay = mclsCertificat.nWay_pay
	
    Else
        Call mclsCertificat.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")))
        
        sWay_Pay = Request.QueryString.Item("nWayPay")
    End If


    If sWay_Pay = "3" Then
        Call mclsPolicy.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")))
    End If

    '+Se verifica si la poliza tiene convenios en AGREEMENT_POL
    If mclsAgreement_pol.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), Today) Then
        '+ Se Habilita el campo de convenio origen 
        bDisabledAgreement = False
    Else
        '+ Se DesHabilita el campo de convenio origen 
        bDisabledAgreement = True        
    End If
    
    If sWay_Pay = "1" Or sWay_Pay = "2" Then
        Call mclsDirdebit.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), Today)
        sTyp_dirdeb = mclsDirdebit.sTyp_dirdeb
    End If

    mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
    Session("bQuery") = mobjValues.ActionQuery
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 27/10/04 12:47p $|$$Author: Nvapla10 $"

//% ShowChangeValues: Se cargan los valores de acuerdo al número de recibo introducido
//-------------------------------------------------------------------------------------------
function InsNewWayPay(lobjOption){
//-------------------------------------------------------------------------------------------
var lstQueryString


    with (document.forms[0]){
       lstQueryString  = "nBranch="   + hddnBranch.value;
       lstQueryString  = lstQueryString +  "&nPolicy="  + hddnPolicy.value;
       lstQueryString  = lstQueryString +  "&nCertif="  + hddnCertif.value;
       lstQueryString  = lstQueryString +  "&nProduct=" + hddnProduct.value;
       lstQueryString  = lstQueryString +   "&dDate="  + tcdDate.value;

     switch (lobjOption) {
//PAC
			case "PAC":
                  tctClientPACNew.disabled=false;
                  tctAccountPACNew.disabled=false;
			      tctBankAuthPACNew.disabled=false;
			      cbeBankPACNew.disabled=false;
			 
			      cbeCardTypeNew.value=''
			      cbeCardTypeNew.disabled=true;
			      tctCardNumberNew.value='';
			      tctCardNumberNew.disabled=true;
			      tcdCardExpirNew.value='';
			      tcdCardExpirNew.disabled=true;
			      tctClientCreditNew.value='';
			      tctClientCreditNew.disabled=true;
			      valAgreementNew.value = '';
				  valAgreementNew.disabled = true;
				  btnvalAgreementNew.disabled = true;
				  //tcnAFPCommiNew.disabled = true;
				  //cbeCurrencyNew.disabled = true;
				  if (hddsApv.value=='1')
					//elements["valOriginNew"].Parameters.Param3.sValue = '3';  
				  
				  //valOriginNew.disabled = false; 
				  //btnvalOriginNew.disabled = false;
				  
				  if (hddnprodclas.value == 4){
					  //valOriginNew.value = '2';
				  }

				  Disableclient();
				  break;
		    case "TBK":
   			      cbeCardTypeNew.disabled=false;
			      tctCardNumberNew.disabled=false;
			      tcdCardExpirNew.disabled=false;
			      tctClientCreditNew.disabled=false;
			      tctClientCreditNew_Digit.disabled=false;
			    
			      cbeBankPACNew.value='';
			      cbeBankPACNew.disabled=true;
			      tctClientPACNew.value='';
			      tctClientPACNew.disabled=true;
			      tctClientPACNew_Digit.value='';
			      tctClientPACNew_Digit.disabled=true;
			      tctAccountPACNew.value='';
			      tctAccountPACNew.disabled=true;
			      tctBankAuthPACNew.value='';
			      tctBankAuthPACNew.disabled=true;
			      valAgreementNew.value = '';
				  valAgreementNew.disabled = true;
				  btnvalAgreementNew.disabled = true;
				  //tcnAFPCommiNew.disabled = true;
				  //cbeCurrencyNew.disabled = true;
				  if (hddsApv.value=='1')
					//elements["valOriginNew"].Parameters.Param3.sValue = '3';  
				  
				  //valOriginNew.disabled = false; 
				 // btnvalOriginNew.disabled = false;
				  
				  if (hddnprodclas.value == 4){
					  //valOriginNew.value = '2';
				  }
				   Disableclient();

			      break;
			case "Aviso":
			      cbeCardTypeNew.value='';
			      cbeCardTypeNew.disabled=true;
			      tctCardNumberNew.value='';
			      tctCardNumberNew.disabled=true;
			      tcdCardExpirNew.value='';
			      tcdCardExpirNew.disabled=true;
			      tctClientCreditNew.value='';
			      tctClientCreditNew.disabled=true;
			      tctClientCreditNew_Digit.value='';
			      tctClientCreditNew_Digit.disabled=true;
			  
			      cbeBankPACNew.value='';
			      cbeBankPACNew.disabled=true;
			      tctClientPACNew.value='';
			      tctClientPACNew.disabled=true;
			      tctClientPACNew_Digit.value='';
			      tctClientPACNew_Digit.disabled=true;
			      tctAccountPACNew.value='';
			      tctAccountPACNew.disabled=true;
			      tctBankAuthPACNew.value='';
			      tctBankAuthPACNew.disabled=true; 
			      valAgreementNew.value = '';
				  valAgreementNew.disabled = true;
				  btnvalAgreementNew.disabled = true;  
				  //tcnAFPCommiNew.disabled = true;
				  //cbeCurrencyNew.disabled = true;
				  if (hddsApv.value=='1')
					//elements["valOriginNew"].Parameters.Param3.sValue = '3';  
				  
				  //valOriginNew.disabled = false; 
				  //btnvalOriginNew.disabled = false;
				  
				  if (hddnprodclas.value == 4){
					  //valOriginNew.value = '2';
				  }
				  Disableclient();
				  break;

		case "Descuento":

		    cbeCardTypeNew.value = '';
		    cbeCardTypeNew.disabled = true;
		    tctCardNumberNew.value = '';
		    tctCardNumberNew.disabled = true;
		    tcdCardExpirNew.value = '';
		    tcdCardExpirNew.disabled = true;
		    tctClientCreditNew.value = '';
		    tctClientCreditNew.disabled = true;
		    tctClientCreditNew_Digit.value = '';
		    tctClientCreditNew_Digit.disabled = true;

		    cbeBankPACNew.value = '';
		    cbeBankPACNew.disabled = true;
		    tctClientPACNew.value = '';
		    tctClientPACNew.disabled = true;
		    tctClientPACNew_Digit.value = '';
		    tctClientPACNew_Digit.disabled = true;
		    tctAccountPACNew.value = '';
		    tctAccountPACNew.disabled = true;
		    tctBankAuthPACNew.value = '';
		    tctBankAuthPACNew.disabled = true;
		    //tcnAFPCommiNew.disabled = true;
		    //cbeCurrencyNew.disabled = true;

		    valAgreementNew.disabled = false;
		    btnvalAgreementNew.disabled = false;
		   // alert('as');

            //si tiene convenio por poliza se habilita el combo de convenios origen para que seleccione a cual se hara el cambios
		    valAgreement.disabled = false;
		    btnvalAgreement.disabled = false;
		    

		    if (valAgreementNew.value == '') {
		        //insDefValues('GenAgreement2','' , '/VTimeNet/Policy/PolicySeq');
		        //insDefValues('GenDirect',lstQueryString , '/VTimeNet/collection/collectiontra');
		    }
		    //btnvalAgreementNew.disabled = true;
		    //valAgreementNew.disabled = true;

		    if (hddsApv.value == '1')
		    //elements["valOriginNew"].Parameters.Param3.sValue = '3';  

		    //valOriginNew.disabled = false; 
		    //btnvalOriginNew.disabled = false;

		        if (hddnprodclas.value == 4) {
		            //valOriginNew.value = '2';
		        }
		    Enableclient();
		    break;  
			
			case "AFP":
			      cbeCardTypeNew.value='';
			      cbeCardTypeNew.disabled=true;
			      tctCardNumberNew.value='';
			      tctCardNumberNew.disabled=true;
			      tcdCardExpirNew.value='';
			      tcdCardExpirNew.disabled=true;
			      tctClientCreditNew.value='';
			      tctClientCreditNew.disabled=true;
			      tctClientCreditNew_Digit.value='';
			      tctClientCreditNew_Digit.disabled=true;
			    
			      cbeBankPACNew.value='';
			      cbeBankPACNew.disabled=true;
			      tctClientPACNew.value='';
			      tctClientPACNew.disabled=true;
			      tctClientPACNew_Digit.value='';
			      tctClientPACNew_Digit.disabled=true;
			      tctAccountPACNew.value='';
			      tctAccountPACNew.disabled=true;
			      tctBankAuthPACNew.value='';
			      tctBankAuthPACNew.disabled=true;
			      valAgreementNew.value = '';
				  valAgreementNew.disabled = true;
				  btnvalAgreementNew.disabled = true;
				  
				  if(hddnprodclas.value == 4) {
    			     // tcnAFPCommiNew.value = '';
	    			  //cbeCurrencyNew.value = '';
			
					 // tcnAFPCommiNew.disabled = false;
					  //cbeCurrencyNew.disabled = false;
				  }
				  if (hddsApv.value=='1')
					//elements["valOriginNew"].Parameters.Param3.sValue = '3';  
				  
				  //valOriginNew.disabled = false; 
				  //btnvalOriginNew.disabled = false;
				  
				  if (hddnprodclas.value == 4){
					  //valOriginNew.value = '2';
				  }
				  
				  Disableclient();
			      break;  
			      
	}
	  insDirect ();
 }	
}

function Disableclient()
{
with (document.forms[0]){
//pagador y empleador 
	dtcClientEmpNew.value =  '';
    dtcClientPayNew.value   =  '';
    dtcClientEmpNew_Digit.value  = ''; 
    dtcClientPayNew_Digit.value  =  '';
    document.getElementById("dtcClientPayNew_Name").innerHTML = '';
    document.getElementById("dtcClientEmpNew_Name").innerHTML = '';
	dtcClientEmpNew.disabled  = true;
	dtcClientEmpNew_Digit.disabled  = true;
	btndtcClientEmpNew.disabled  = true;
    dtcClientPayNew.disabled  = true;
    dtcClientPayNew_Digit.disabled  = true;
    btndtcClientPayNew.disabled = true;
}
}

function Enableclient()
{ var lstQueryString
with (document.forms[0]){
   lstQueryString  = "nBranch="   + hddnBranch.value;
   lstQueryString  = lstQueryString +  "&nPolicy="  + hddnPolicy.value;
   lstQueryString  = lstQueryString +  "&nCertif="  + hddnCertif.value;
   lstQueryString  = lstQueryString +  "&nProduct=" + hddnProduct.value;
   lstQueryString  = lstQueryString +   "&dDate="   + tcdDate.value;
   lstQueryString  = lstQueryString +  "&dClientPay="  + dtcClientPay.value;
   
    dtcClientEmpNew.value =  dtcClientEmp.value;
    dtcClientPayNew.value   =  dtcClientPay.value;
    dtcClientEmpNew_Digit.value  =  dtcClientEmp_Digit.value; 
    dtcClientPayNew_Digit.value  =  dtcClientPay_Digit.value;
    document.getElementById("dtcClientPayNew_Name").innerHTML = document.getElementById("dtcClientPay_Name").innerHTML;
    document.getElementById("dtcClientEmpNew_Name").innerHTML = document.getElementById("dtcClientEmp_Name").innerHTML;
    
    if ( dtcClientPayNew.value != '' )
    {
     dtcClientEmpNew.disabled  = true;
	dtcClientEmpNew_Digit.disabled  = true;
	btndtcClientEmpNew.disabled  = true;
	// crea convenio
	insDefValues('GenAgreement',lstQueryString , '/VTimeNet/collection/collectiontra');
	
	}
	else
	{dtcClientEmpNew.disabled  = false;
	dtcClientEmpNew_Digit.disabled  = false;
	btndtcClientEmpNew.disabled  = false;
	}
	
	if ( dtcClientEmpNew.value != '' ){
	dtcClientPayNew.disabled  = true;
	dtcClientPayNew_Digit.disabled  = true;
	btndtcClientPayNew.disabled = true;
	}
	else 
	{
	dtcClientPayNew.disabled  = false;
	dtcClientPayNew_Digit.disabled  = false;
	btndtcClientPayNew.disabled = false;
	}
	dtcClientEmpNew.value =  dtcClientEmp.value;
    dtcClientPayNew.value   =  dtcClientPay.value;
}
}

//% insDirect : activa o desactiva la opcion de 
//-------------------------------------------------------------------------------------------
function insDirect(){
//-------------------------------------------------------------------------------------------

with (self.document.forms[0]){
	optDirect[0].checked=false;
	optDirect[1].checked=false ;
	if ( optWayNewPay[3].checked  )
	{  if (dtcClientEmpNew.value == dtcClientPayNew.value )
   	       optDirect[0].checked=true ; 
 		else 
		   optDirect[1].checked=true  ; 
	}
	else 
	optDirect[0].checked=true ;
	}	
	
}
//%DisaOtherWayPay: Desabilita Otras formas de pagoa cuando de trata solo de cambios 
// a la froma de pago actual
//-----------------------------------------------------------------------------------------------------------
function DisaOtherWayPay(lobjwayPay){
var sway_pay = lobjwayPay;
    with (self.document.forms[0]){
		  
             optWayNewPay[0].disabled=true;
             optWayNewPay[1].disabled=true;
             optWayNewPay[2].disabled=true;
             optWayNewPay[3].disabled=true;
             //optWayNewPay[4].disabled=true;
             
             if (sway_pay == 1)
             {
             
                optWayNewPay[0].checked="1"; 
	    		optWayNewPay[0].value="1";	
	    		
                cbeCardTypeNew.disabled     = true;
                tctCardNumberNew.disabled   = true;
                tcdCardExpirNew.disabled    = true;
                tctClientCreditNew.disabled = true;
                 
                cbeBankPACNew.disabled     = false;
                tctClientPACNew.disabled   = false;
                tctAccountPACNew.disabled  = false;
                tctBankAuthPACNew.disabled = false;
                
                valAgreementNew.disabled       = true;
			    btnvalAgreementNew.disabled    = true;
			    //tcnAFPCommiNew.disabled        = true;
	    	    //cbeCurrencyNew.disabled        = true;
	    	    //valOriginNew.disabled          = true;
	    	    //btnvalOriginNew.disabled       = true;
                 
             }
          
             if (sway_pay == 2)
             {
                optWayNewPay[1].checked="1"; 
	    		optWayNewPay[1].value="2";	
	    		
                 cbeBankPACNew.disabled     = true;
                 tctClientPACNew.disabled   = true;
                 tctAccountPACNew.disabled  = true;
                 tctBankAuthPACNew.disabled = true;
                 
                 cbeCardTypeNew.disabled     = false;
                 tctCardNumberNew.disabled   = false;
                 tcdCardExpirNew.disabled    = false;
                 tctClientCreditNew.disabled = false;
                 
                 valAgreementNew.disabled       = true;
			     btnvalAgreementNew.disabled    = true;
			     //tcnAFPCommiNew.disabled        = true;
	    	     //cbeCurrencyNew.disabled        = true;
	    	     //valOriginNew.disabled          = true;
	    	     //btnvalOriginNew.disabled       = true;
             }
             
             if (sway_pay == 4)
             {
             
                optWayNewPay[2].checked="1"; 
	    		optWayNewPay[2].value="4";	
	    		
	    		 cbeCardTypeNew.disabled     = true;
                 tctCardNumberNew.disabled   = true;
                 tcdCardExpirNew.disabled    = true;
                 tctClientCreditNew.disabled = true;
				
                 cbeBankPACNew.disabled     = true;
                 tctClientPACNew.disabled   = true;
                 tctAccountPACNew.disabled  = true;
                 tctBankAuthPACNew.disabled = true;
                 
                 valAgreementNew.disabled       = true;
			     btnvalAgreementNew.disabled    = true;
			     //tcnAFPCommiNew.disabled        = true;
	    	     //cbeCurrencyNew.disabled        = true;
	    	     //valOriginNew.disabled          = true;
	    	     //btnvalOriginNew.disabled       = true;
             }
             
             if (sway_pay == 3)
             {
             
                optWayNewPay[3].checked="1"; 
	    		optWayNewPay[3].value="3";	
	    		
                cbeCardTypeNew.disabled     = true;
                tctCardNumberNew.disabled   = true;
                tcdCardExpirNew.disabled    = true;
                tctClientCreditNew.disabled = true;
                 
                cbeBankPACNew.disabled     = true;
                tctClientPACNew.disabled   = true;
                tctAccountPACNew.disabled  = true;
                tctBankAuthPACNew.disabled = true;
                
                valAgreementNew.disabled       = false;
			    btnvalAgreementNew.disabled    = false;
			    //tcnAFPCommiNew.disabled        = true;
	    	    //cbeCurrencyNew.disabled        = true;
	    	    //valOriginNew.disabled          = false;
	    	    //btnvalOriginNew.disabled       = false;
                 
             }
             
             if (sway_pay == 7)
             {
             
                optWayNewPay[4].checked="1"; 
	    		optWayNewPay[4].value="7";	
	    		
                cbeCardTypeNew.disabled     = true;
                tctCardNumberNew.disabled   = true;
                tcdCardExpirNew.disabled    = true;
                tctClientCreditNew.disabled = true;
                 
                cbeBankPACNew.disabled     = true;
                tctClientPACNew.disabled   = true;
                tctAccountPACNew.disabled  = true;
                tctBankAuthPACNew.disabled = true;
                
                valAgreementNew.disabled       = true;
			    btnvalAgreementNew.disabled    = true;
			    //tcnAFPCommiNew.disabled        = false;
	    	    //cbeCurrencyNew.disabled        = false;
	    	    //valOriginNew.disabled          = true;
	    	    //btnvalOriginNew.disabled       = true;
                 
             }
             
             
    }
}
//%EnabOtherWayPay: Desabilita Otras formas de pagoa cuando de trata solo de cambios 
// a la froma de pago actual
//-----------------------------------------------------------------------------------------------------------
function Enableclient2(){ 
    var lstQueryString
    with (document.forms[0]){
        lstQueryString  = "dClientPay="  + dtcClientPayNew.value;
        if ( dtcClientPayNew.value != '' )
            insDefValues('GenAgreement2', lstQueryString, '/VTimeNet/collection/collectiontra');
    }
    insDirect();
}

function EnabOtherWayPay(){

    with (self.document.forms[0]){
             optWayNewPay[0].disabled = false;
             optWayNewPay[1].disabled = false;
             optWayNewPay[2].disabled = false;
             optWayNewPay[3].disabled = false;
//             optWayNewPay[4].disabled = false;
             cbeCardTypeNew.disabled     = false;
             tctCardNumberNew.disabled   = false;
             tcdCardExpirNew.disabled    = false;
             tctClientCreditNew.disabled = false;
             cbeBankPACNew.disabled      = false;
             tctClientPACNew.disabled    = false;
             tctAccountPACNew.disabled   = false;
             tctBankAuthPACNew.disabled  = false;
             valAgreementNew.disabled       = false;
			 btnvalAgreementNew.disabled    = false;
			// tcnAFPCommiNew.disabled        = false;
	    	 //cbeCurrencyNew.disabled        = false;
	    	 //valOriginNew.disabled          = false;
     }
}
// a la vía de pago actual
//% EnabledFields: Habilita los campos de acuerdo al
//-------------------------------------------------------------------------------------------
function EnabledFields(Field){
//-------------------------------------------------------------------------------------------
    var lstrLocation = "";
	with (self.document.forms[0]){
		if(Field.value==1){
				cbeCardType.value='';
				tctCardNumber.value='';
				valIntermed.value='';
				UpdateDiv('valIntermedDesc','');
				cbeCause.disabled=false;
				cbeBank.disabled=false;		
				tctAccount.disabled=false;
				tctTitular.disabled=false;
				tctTitular_Digit.disabled=false;
				valIntermed.disabled=true;
				btnvalIntermed.disabled=true;
				cbeCardType.disabled=true;		
				tctCardNumber.disabled=true;
				tctBankAuth.value = '';
				tctBankAuth.disabled = false;
				tcdCardExpir.value = '';
				tcdCardExpir.disabled = true;
				btn_tcdCardExpir.disabled = true;

				if (hddblnIsIntermed.value == 2){
					optAffect[1].checked = true;
				}
 		}
		else if(Field.value==2){
				cbeBank.value='';
				tctAccount.value='';
				tctTitular.value='';
                tctTitular_Digit.value='';
				valIntermed.value='';
				UpdateDiv('valIntermedDesc','');
				UpdateDiv('tctName','');
				cbeCause.disabled=false;
				cbeCardType.disabled=false;
				tctCardNumber.disabled=false;
				cbeBank.disabled=true;
				tctAccount.disabled=true;
				tctTitular.disabled=true;
				tctTitular_Digit.disabled=true;
				btntctTitular.disabled=true;
				valIntermed.disabled=true;
				btnvalIntermed.disabled=true;
				tctBankAuth.value = '';
				tctBankAuth.disabled = true;
				tcdCardExpir.value = '';
				tcdCardExpir.disabled = false;
				btn_tcdCardExpir.disabled = false;
				
				if (hddblnIsIntermed.value == 2){
					optAffect[1].checked = true;
				}
		}
		else {
				tctCardNumber.value='';
				cbeBank.value='';
				tctAccount.value='';
				tctTitular.value='';
				tctTitular_Digit.value='';
			    UpdateDiv('tctName','');
				cbeCardType.value = '';
				cbeCause.disabled=false;
				cbeCardType.disabled=true;
				tctCardNumber.disabled=true;
				cbeBank.disabled=true;
				tctAccount.disabled=true;
			    tctTitular.disabled=true;
			    tctTitular_Digit.disabled=true;
			    btntctTitular.disabled=true;
				tctBankAuth.value = '';
				tctBankAuth.disabled = true;
				tcdCardExpir.value = '';
				tcdCardExpir.disabled = true;
				btn_tcdCardExpir.disabled = true;
				if (hddblnIsIntermed.value == 2){
					optAffect[0].checked = true;
				}
				
		}

//+Se buscan los valores por defecto definidos
   	    if (hddsTypdirdeb.value == Field.value){
	        lstrLocation = lstrLocation + "nReceipt_CO004=" + hddnReceipt.value;
	        lstrLocation = lstrLocation + "&sCertype_CO004=" + hddsCertype.value;
	        lstrLocation = lstrLocation + "&nDigit_CO004=" + hddnDigit.value;
	        lstrLocation = lstrLocation + "&nPayNumbe_CO004=" + hddnPaynumbe.value;
	        lstrLocation = lstrLocation + "&optChange_CO004=" + Field.value;
	        lstrLocation = lstrLocation + "&dDateProcess=" + tcdDate.value;
			lstrLocation = lstrLocation + "&nContrat_CO004=" + '<%=Request.QueryString.Item("nContrat")%>'
			lstrLocation = lstrLocation + "&nDraft_CO004=" + '<%=Request.QueryString.Item("nDraft")%>'
            insDefValues("ChangeDefValues_CO004",lstrLocation);
        }
	}
}
	
//% insCancel : Controla la acción de cancelar la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true
}
//InsChangeAgreement();
//-------------------------------------------------------------------------------------------
function InsChangeAgreement() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (valAgreementNew.value != '')
			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=Agreement&nCod_Agree=" + valAgreementNew.value, "ShowDefValues", 1, 1,"no","no",2000,2000);
	}
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CO004", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing

Call insPreCO004()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmModCollect" ACTION="valCollectionTra.aspx?<%=Request.Params.Get("Query_String")%>">
    <%Response.Write(mobjValues.ShowWindowsName("CO004", Request.QueryString.Item("sWindowDescript")))%>
	<BR>    

    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A><%= GetLocalResourceObject("AnchorCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A><%= GetLocalResourceObject("Anchor2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD WIDTH="10%"></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
	    <TR>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
            <TD><% %><%=mobjValues.DateControl("tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateToolTip"))%></TD>
            <td></td>
			<TD COLSPAN="4"><%=mobjValues.OptionControl(0, "optChangeway", GetLocalResourceObject("optChangeway_1Caption"), "1", "1", "EnabOtherWayPay()", False)%> </TD>               
		</TR>
		
		
		<TR>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("cbeCauseCaption") %></LABEL></TD>
			<TD>
			   <%=mobjValues.PossiblesValues("cbeCause", "table77", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCauseToolTip"))%>
            </TD>
            <td></td>
			<TD COLSPAN="4"><%=mobjValues.OptionControl(0, "optChangeway", GetLocalResourceObject("optChangeway_2Caption"), "", "2", "DisaOtherWayPay(" & sWay_Pay & ")", False)%> </TD>               
		</TR>
                 
        <TR>
          <TD>&nbsp;</TD>
        </TR>
        
       <TR>
			<TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A><%= GetLocalResourceObject("Anchor3Caption") %></A></LABEL></TD>
            <TD  WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A><%= GetLocalResourceObject("Anchor4Caption") %></A></LABEL></TD>            
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline">
                <TD  WIDTH="10%"></TD>
                <TD COLSPAN="2" CLASS="Horline" ></TD>
            </TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2">
              <%If Request.QueryString.Item("sWayPay") = "1" Then
                    Response.Write(mobjValues.OptionControl(0, "optChangepremium", GetLocalResourceObject("optChangepremium_1Caption"), "1", "1", , False))
                Else
                    Response.Write(mobjValues.OptionControl(0, "optChangepremium", GetLocalResourceObject("optChangepremium_1Caption"), "1", "1", , True))
                End If%> 
            </TD>
            
            <TD WIDTH="10%"></TD>
            <TD ><LABEL ID=9906><%= GetLocalResourceObject("cbeWay_payCaption") %></LABEL></TD>
            <TD><%With Response
                        mobjValues.TypeList = 1
                        mobjValues.List = "1,2,3,4,7"
                        .Write(mobjValues.PossiblesValues("cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, sWay_Pay, , , , , , , True, , GetLocalResourceObject("cbeWay_payToolTip")))
                    End With%></TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2">
               <%If Request.QueryString.Item("sWayPay") = "1" Then
                       Response.Write(mobjValues.OptionControl(0, "optChangepremium", GetLocalResourceObject("optChangepremium_2Caption"), "", "2", , False))
                   Else
                       Response.Write(mobjValues.OptionControl(0, "optChangepremium", GetLocalResourceObject("optChangepremium_2Caption"), "", "2", , True))
                   End If
                %> </TD>
            <TD WIDTH="10%"></TD>
            <TD ><LABEL ID=0><%= GetLocalResourceObject("cbePayfreqCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.PossiblesValues("cbePayfreq", "table36", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nPayfreq), , , , , , , True, , GetLocalResourceObject("cbePayfreqToolTip")))%></TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2">
                 <%If Request.QueryString.Item("sWayPay") = "1" Then
                         Response.Write(mobjValues.OptionControl(0, "optChangepremium", GetLocalResourceObject("optChangepremium_3Caption"), "", "3 ", , True))
                     Else
                         Response.Write(mobjValues.OptionControl(0, "optChangepremium", GetLocalResourceObject("optChangepremium_3Caption"), "", "3", CStr(False)))
                     End If
                %> 
            </TD>
            <TD  WIDTH="10%"></TD>
             <%If mclsCertificat.sDirind = "2" Then%>
                <TD> <%Response.Write(mobjValues.OptionControl(0, "optindpolicy", GetLocalResourceObject("optindpolicy_1mclsCertificat.sDirindCaption"), "1", mclsCertificat.sDirind,, True))%> </td>
                <TD><%Response.Write(mobjValues.OptionControl(0, "optindpolicy", GetLocalResourceObject("optindpolicy_mclsCertificat.sDirindCaption"), "2", mclsCertificat.sDirind, ,True))%></TD>
               <%Else%>
                <TD><%Response.Write(mobjValues.OptionControl(0, "optindpolicy", GetLocalResourceObject("optindpolicy_1mclsCertificat.sDirindCaption"), "2", mclsCertificat.sDirind, ,True))%></TD>
                <TD><%Response.Write(mobjValues.OptionControl(0, "optindpolicy", GetLocalResourceObject("optindpolicy_mclsCertificat.sDirindCaption"), "1", mclsCertificat.sDirind, ,True))%></TD>
               <%End If%>             
        </TR>

        <TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Domiciliación bancaria"><%= GetLocalResourceObject("AnchorDomiciliación bancariaCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Tarjeta de crédito"><%= GetLocalResourceObject("AnchorTarjeta de créditoCaption") %></A></LABEL></TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD  WIDTH="10%"></TD>
            <TD COLSPAN="2" CLASS="Horline" ALIGN = RIGTH></TD>
		</TR>
		
        <TR>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("cbeBankPACCaption") %></LABEL></TD>
			<TD><%If sTyp_dirdeb = "1" Then
			            Response.Write(mobjValues.PossiblesValues("cbeBankPAC", "table7", eFunctions.Values.eValuesType.clngComboType, CStr(mclsDirdebit.nBankext), , , , , , , True, , GetLocalResourceObject("cbeBankPACToolTip")))
			        Else
			            Response.Write(mobjValues.PossiblesValues("cbeBankPAC", "table7", eFunctions.Values.eValuesType.clngComboType, vbNullString, , , , , , , True, , GetLocalResourceObject("cbeBankPACToolTip")))
			              End If%>
            </TD>
			<TD>&nbsp;</TD>
			
			<TD><LABEL><%= GetLocalResourceObject("cbeCardTypeTbkCaption") %></LABEL></TD>
			
			<TD><%If sTyp_dirdeb = "2" Then
			            Response.Write(mobjValues.PossiblesValues("cbeCardTypeTbk", "table183", eFunctions.Values.eValuesType.clngComboType, CStr(mclsDirdebit.nTyp_crecard), , , , , , , True, , GetLocalResourceObject("cbeCardTypeTbkToolTip")))
			        Else
			            Response.Write(mobjValues.PossiblesValues("cbeCardTypeTbk", "table183", eFunctions.Values.eValuesType.clngComboType, "", , , , , , , True, , GetLocalResourceObject("cbeCardTypeTbkToolTip")))
			                  End If%>                
			</TD>
		</TR>
		<TR>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctTitularPACCaption") %></LABEL></TD>
            <TD><%If sTyp_dirdeb = "1" Then
                        Response.Write(mobjValues.ClientControl("tctTitularPAC", mclsDirdebit.sClient, , GetLocalResourceObject("tctTitularPACToolTip"), , True, "tctName"))
                    Else
                        Response.Write(mobjValues.ClientControl("tctTitularPAC", "", , GetLocalResourceObject("tctTitularPACToolTip"), , True, "tctName"))
                              End If%>
            </TD>                                    
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tctCardNumberTbkCaption") %></LABEL></TD>
			<TD><%If sTyp_dirdeb = "2" Then
			            Response.Write(mobjValues.TextControl("tctCardNumberTbk", 20, mclsDirdebit.sCredi_card, , GetLocalResourceObject("tctCardNumberTbkToolTip"), , , , , True))
			        Else
			            Response.Write(mobjValues.TextControl("tctCardNumberTbk", 20, , , GetLocalResourceObject("tctCardNumberTbkToolTip"), , , , , True))
			        End If%>                			        
            </TD>
		</TR>
		<TR>
		    <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctAccountPACCaption") %></LABEL></TD>
            <TD><%If sTyp_dirdeb = "1" Then
                        Response.Write(mobjValues.TextControl("tctAccountPAC", 30, mclsDirdebit.sAccount, , GetLocalResourceObject("tctAccountPACToolTip"), , , , , True))
                    Else
                        Response.Write(mobjValues.TextControl("tctAccountPAC", 30, "", , GetLocalResourceObject("tctAccountPACToolTip"), , , , , True))
                    End If%>                    
            </TD>
   			<TD>&nbsp;</TD>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tcdCardExpirTbkCaption") %></LABEL></TD>
			<TD><%If sTyp_dirdeb = "2" Then
			            Response.Write(mobjValues.DateControl("tcdCardExpirTbk", CStr(mclsDirdebit.dCardExpir), , GetLocalResourceObject("tcdCardExpirTbkToolTip"), , , , , True))
			        Else
			            Response.Write(mobjValues.DateControl("tcdCardExpirTbk", , , GetLocalResourceObject("tcdCardExpirTbkToolTip"), , , , , True))
			                  End If%>			        
            </TD>

		</TR>
		<TR>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctBankAuthPACCaption") %></LABEL></TD>
			<TD><%If sTyp_dirdeb = "1" Then
			        Response.Write(mobjValues.TextControl("tctBankAuthPAC", 15, mclsDirdebit.sBankauth, , GetLocalResourceObject("tctBankAuthPACToolTip"), , , , , True))
			    Else
			        Response.Write(mobjValues.TextControl("tctBankAuthPAC", 15, , , GetLocalResourceObject("tctBankAuthPACToolTip"), , , , , True))
			          End If%>			        
                </TD>
           <TD>&nbsp;</TD>
           <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctTitularPACCaption") %></LABEL></TD>
            <TD><%If sTyp_dirdeb = "2" Then
                        Response.Write(mobjValues.ClientControl("dtcClient", mclsDirdebit.sClient, , GetLocalResourceObject("dtcClientToolTip"), , True, "sCliename", , , , , , , , True))
                    Else
                        Response.Write(mobjValues.ClientControl("dtcClient", "", , GetLocalResourceObject("dtcClientToolTip"), , True, "sCliename", , , , , , , , True))
                              End If%> 
            </TD> 
		</TR>
        
         <TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Domiciliación bancaria"><%= GetLocalResourceObject("AnchorDomiciliación bancaria2Caption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Tarjeta de crédito"><%= GetLocalResourceObject("AnchorTarjeta de crédito2Caption") %> </A></LABEL></TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD  WIDTH="10%"></TD>
            <TD COLSPAN="2" CLASS="Horline" ALIGN = RIGTH></TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientPayCaption") %></LABEL></TD>
			<TD><% =mobjValues.ClientControl("dtcClientPay", sClientPay,  , GetLocalResourceObject("dtcClientPayToolTip"),  , True,  ,  ,  ,  ,  ,  ,  ,  , True)%></TD>
			<TD> &nbsp; </TD>
			<TD COLSPAN="2" >
			       <%Call InsDefineHeaderA()
			           Call InsPreVI8002A()
			     %></LABEL></TD>

		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientEmpCaption") %></LABEL></TD>
			<TD><%=mobjValues.ClientControl("dtcClientEmp", sClientEmp,  , GetLocalResourceObject("dtcClientEmpToolTip"),  , True,  ,  ,  ,  ,  ,  ,  ,  , True)%>
			</TD>	
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD>

<% 
'+ Los valores de los convenios pueden ser por polizas.
    mobjValues.Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("nPolicy", Request.QueryString.Item("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("nCertif", Request.QueryString.Item("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>

			<TD><%= mobjValues.PossiblesValues("valAgreement", "tabagreementpol", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsPolicy.nCod_Agree), True, , , , , , True, 5, GetLocalResourceObject("valAgreementToolTip"))%></TD> 
		</TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted" ><LABEL><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
            
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="2" ></TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optWayNewPay", GetLocalResourceObject("optWayNewPay_1Caption"), "1", "1", "InsNewWayPay(""PAC"")")%> </TD>
            <TD  WIDTH="10%"></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optDirect", GetLocalResourceObject("optDirect_2Caption"), "1", "2",  , True)%></TD>
        </TR>
        
       <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optWayNewPay", GetLocalResourceObject("optWayNewPay_2Caption"), "", "2", "InsNewWayPay(""TBK"")")%> </TD>
            <TD WIDTH="10%"></TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optDirect", GetLocalResourceObject("optDirect_2Caption"), "2", "2",  , True)%>  </TD>
        </TR>
         <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optWayNewPay", GetLocalResourceObject("optWayNewPay_4Caption"), "", "4", "InsNewWayPay(""Aviso"")")%> </TD>
            <TD WIDTH="10%"></TD>
               <TD COLSPAN="2"></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optWayNewPay", GetLocalResourceObject("optWayNewPay_3Caption"), "", "3", "InsNewWayPay(""Descuento"")")%></TD>
            <TD WIDTH="10%"></TD>
            <TD COLSPAN="2"></TD>
        </TR>
         <TR>
            <TD COLSPAN="2"><!--%= mobjValues.OptionControl(0, "optWayNewPay", GetLocalResourceObject("optWayNewPay_7Caption"), "" , "7", "InsNewWayPay(""AFP"")")%--> </TD>
            <TD WIDTH="10%"></TD>
            <TD COLSPAN="2"></TD>
        </TR>
        
         <TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><%= GetLocalResourceObject("Anchor6Caption") %></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><%= GetLocalResourceObject("Anchor7Caption") %></LABEL></TD>
            <TD COLSPAN="2"></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        
        <TR>
            <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("cbeBankPACCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBankPACNew", "table7", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeBankPACNewToolTip"))%><TD>			
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("cbeCardTypeNewCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCardTypeNew", "table183", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCardTypeNewToolTip"))%></TD>
        </TR>
        
        <TR>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctTitularPACCaption") %></LABEL></TD>
            <TD><%=mobjValues.ClientControl("tctClientPACNew", vbNullString,  , GetLocalResourceObject("tctClientPACNewToolTip"),  , False, "tctName")%></TD>           
            <TD>&nbsp;</TD>
            <TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctCardNumberNewCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctCardNumberNew", 20,  ,  , GetLocalResourceObject("tctCardNumberNewToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        
        <TR>
           <TD ><LABEL><%= GetLocalResourceObject("tctAccountPACCaption") %></LABEL></TD>
           <TD><%=mobjValues.TextControl("tctAccountPACNew", 30, vbNullString,  , GetLocalResourceObject("tctAccountPACNewToolTip"),  ,  ,  ,  , False)%></TD>
           <TD>&nbsp;</TD>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tcdCardExpirNewCaption") %></LABEL></TD>
           <TD><%=mobjValues.DateControl("tcdCardExpirNew",  ,  , GetLocalResourceObject("tcdCardExpirNewToolTip"),  ,  ,  ,  , True)%></TD>        
        </TR>
        
        <TR>
		   <TD><LABEL><%= GetLocalResourceObject("tctBankAuthPACCaption") %></LABEL></TD>
           <TD><%=mobjValues.TextControl("tctBankAuthPACNew", 15, vbNullString,  , GetLocalResourceObject("tctBankAuthPACNewToolTip"),  ,  ,  ,  , False)%></TD>           
           <TD>&nbsp;</TD>
			<TD WIDTH="10%"><LABEL><%= GetLocalResourceObject("tctTitularPACCaption") %></LABEL></TD>
           <TD><%=mobjValues.ClientControl("tctClientCreditNew", vbNullString,  , GetLocalResourceObject("tctClientCreditNewToolTip"),  , True, "tctName")%></TD>        
        </TR>
        
        <!--TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><%= GetLocalResourceObject("Anchor8Caption") %></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><%= GetLocalResourceObject("Anchor9Caption") %></LABEL></TD>
            <TD COLSPAN="2"></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR-->
        
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Domiciliación bancaria"><%= GetLocalResourceObject("AnchorDomiciliación bancaria2Caption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Tarjeta de crédito"><%= GetLocalResourceObject("AnchorTarjeta de crédito2Caption") %> </A></LABEL></TD>
        </TR>
        
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD  WIDTH="10%"></TD>
            <TD COLSPAN="2" CLASS="Horline" ALIGN = RIGTH></TD>
		</TR>
        <TR>
        
        <TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientPayCaption") %></LABEL></TD>
			<TD> <%=mobjValues.ClientControl("dtcClientPayNew", "",  , GetLocalResourceObject("dtcClientPayNewToolTip"), " Enableclient2()", True,  ,  ,  ,  ,  ,  ,  ,  , True)%></TD>
			<TD> &nbsp; </TD>
			<TD COLSPAN="2" >
			       <%Call InsDefineHeaderA()
Call InsPreVI8002A()%></LABEL>
			<!--TD><LABEL ID=0><%= GetLocalResourceObject("tcnAFPCommiNewCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAFPCommiNew", 18,  ,  , GetLocalResourceObject("tcnAFPCommiNewToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valOriginNewCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = True
'+ Los valores de las cuentas origen se leen de la tabla "TAB_ORD_ORIGIN"
mobjValues.Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCollecDocTyp", sWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
If mclsProduct_li.nProdClas = 4 Then
	If mclsProduct_li.sApv = "1" Then
		Response.Write(mobjValues.PossiblesValues("valOriginNew", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCertificat.nOrigin), True,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valOriginNewToolTip")))
	Else
		Response.Write(mobjValues.PossiblesValues("valOriginNew", "TABLE5633", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCertificat.nOrigin), False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valOriginNewToolTip")))
	End If
Else
	Response.Write(mobjValues.PossiblesValues("valOriginNew", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsCertificat.nOrigin), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valOriginNewToolTip")))
End If
%>
			</TD-->
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientEmpCaption") %></LABEL></TD>
			<!--TD><%mobjValues.BlankPosition = True
Response.Write(mobjValues.PossiblesValues("cbeCurrencyNew", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyNewToolTip")))
%>
			</TD-->
			<TD><%=mobjValues.ClientControl("dtcClientEmpNew", "",  , GetLocalResourceObject("dtcClientEmpNewToolTip"), "insDirect()", True,  ,  ,  ,  ,  ,  ,  ,  , True)%>
			</TD>		
			
			
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD>

<% 
'+ Los valores de los convenios pueden ser por polizas.
    mobjValues.Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("nPolicy", Request.QueryString.Item("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("nCertif", Request.QueryString.Item("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    mobjValues.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
			<TD><%= mobjValues.PossiblesValues("valAgreementNew", "tabAgreementpol", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , "InsChangeAgreement();", True, 5, GetLocalResourceObject("valAgreementNewToolTip"))%></TD> 
		</TR>
    </TABLE>
    <TABLE>
	    <%

Response.Write(mobjValues.HiddenControl("hddnPolicy", Request.QueryString.Item("nPolicy")))
Response.Write(mobjValues.HiddenControl("hddnBranch", Request.QueryString.Item("nBranch")))
Response.Write(mobjValues.HiddenControl("hddnProduct", Request.QueryString.Item("nProduct")))
Response.Write(mobjValues.HiddenControl("hddsCertype", Request.QueryString.Item("sCertype")))
Response.Write(mobjValues.HiddenControl("hddnReceipt", Request.QueryString.Item("nReceipt")))
Response.Write(mobjValues.HiddenControl("hddnCertif", Request.QueryString.Item("nCertif")))
Response.Write(mobjValues.HiddenControl("hddnWay_Pay", sWay_Pay))
Response.Write(mobjValues.HiddenControl("hddnContrat", Request.QueryString.Item("nContrat")))
Response.Write(mobjValues.HiddenControl("hddnDraft", Request.QueryString.Item("nDraft")))
Response.Write(mobjValues.HiddenControl("hddsTypdirdeb", CStr(mclsDir_debit.sTyp_dirdeb)))
Response.Write(mobjValues.HiddenControl("hddnDigit", Request.QueryString.Item("nDigit")))
Response.Write(mobjValues.HiddenControl("hddnPaynumbe", Request.QueryString.Item("nPaynumbe")))
Response.Write(mobjValues.HiddenControl("hdddEffecdate_Dirdebit", CStr(mclsDir_debit.dEffecdate)))
Response.Write(mobjValues.HiddenControl("hddsTypeDoc", Request.QueryString.Item("sTypeDoc")))
Response.Write(mobjValues.HiddenControl("hddsDirind", mclsDir_debit.sDirind))
Response.Write(mobjValues.HiddenControl("hddsTitular", mclsDir_debit.sClient))
Response.Write(mobjValues.HiddenControl("hddnprodclas", CStr(mclsProduct_li.nProdClas)))
Response.Write(mobjValues.HiddenControl("hddsApv", mclsProduct_li.sApv))

%>
    </TABLE>

</FORM>
</BODY>
</HTML>
<%




mclsDir_debit = Nothing

mclsDir_debit = Nothing
mclsProduct_li = Nothing
mclsPolicy = Nothing


%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




