<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mclsPolicy As ePolicy.Policy
Dim mclsNull_condi As ePolicy.Null_condi


'- Variables que contendrán la información que está en las variables de Sesión
Dim mintBranch As Integer
Dim mintProduct As Integer
Dim mlngPolicy As Double
Dim mlngCertif As Double
    Dim lclsProduct As eProduct.Product
Dim mstrClienCode As String
Dim mstrClienName As String
Dim mvntIntermedCode As Object
Dim mstrIntermedName As String
Dim mstrBenefExist As String
Dim mblnDisabled As Boolean
Dim lnDisabled As String
Dim mlintReturn As String
Dim mintRate As Object

Dim mdtmdEffecdateRequest As Object
    Dim mintNullcodeRequest As String
Dim mintTyp_recRequest As Object
Dim mstrNull_recRequest As String


'% insPreFolder: Ejecuta las rutinas necesarias para la carga de la página
'-------------------------------------------------------------------------
Private Function insPreFolder() As Object
	'dim eRemoteDB.Constants.intNull As Integer
	'-------------------------------------------------------------------------
	Dim lclsIntermedia As eAgent.Intermedia
	Dim lclsClient As eClient.Client
	Dim lclsRequest As ePolicy.Request
	Dim lclsBeneficiar As ePolicy.Beneficiar
	Dim lintRole As Integer
	
	mclsNull_condi = New ePolicy.Null_condi
	lclsBeneficiar = New ePolicy.Beneficiar
	
	mlintReturn = ""
	mintRate = ""
	
	Call mclsPolicy.Find("2", mintBranch, mintProduct, mlngPolicy)
	
	
	If CStr(Session("sCodisplOri")) <> "CA033" Then
		'+Se toma información de check según lo ingresado en solicitud	
		lclsRequest = New ePolicy.Request
		
		Call lclsRequest.Find_nProponum("8", mintBranch, mintProduct, mlngPolicy, mlngCertif)
		
		mlintReturn = lclsRequest.sReturn_ind
		
		If lclsRequest.nReturn_Rat > 0 Then
			mintRate = lclsRequest.nReturn_Rat
		Else
			mintRate = ""
		End If
		mdtmdEffecdateRequest = lclsRequest.dEffecdate
            mintNullcodeRequest = lclsRequest.nNullcode
		mintTyp_recRequest = lclsRequest.nTyp_rec
		mstrNull_recRequest = lclsRequest.sNull_rec
		
		lclsRequest = Nothing
	Else
		'+Se ingresa información por defecto
		mintTyp_recRequest = 2
		mdtmdEffecdateRequest = ""
            mintNullcodeRequest = ""
		
		'+Anulación de recibo pendiente
		If mlngCertif > 0 Then
			'+Si es poliza colectiva/multilocalidad con renovación simultánea debe 
			'+quedar desmarcado
			If mclsPolicy.sPolitype <> "1" And mclsPolicy.sColtimre = "1" Then
				mstrNull_recRequest = "2"
			Else
				mstrNull_recRequest = "1"
			End If
		Else
			mstrNull_recRequest = "1"
		End If
	End If
	With Response
		.Write("<SCRIPT>")
		.Write("mintBranch_j = '" & mintBranch & "';")
		.Write("mintProduct_j = '" & mintProduct & "';")
		.Write("</" & "Script>")
	End With
	
	If mclsPolicy.nIntermed <> eRemoteDB.Constants.intNull Then
		mvntIntermedCode = mclsPolicy.nIntermed
	Else
		mvntIntermedCode = vbNullString
	End If
	
	'+Se buscan los datos del intermediario
	If mvntIntermedCode <> vbNullString Then
		lclsIntermedia = New eAgent.Intermedia
		lclsClient = New eClient.Client
		
		If lclsIntermedia.Find(mvntIntermedCode) Then
			mstrClienCode = Trim(lclsIntermedia.sClient)
		End If
		
		If lclsClient.FindClientName(mstrClienCode) Then
			mstrIntermedName = lclsClient.sCliename
		Else
			mstrIntermedName = vbNullString
		End If
		
		lclsIntermedia = Nothing
		lclsClient = Nothing
	End If
	If mlngCertif > 0 Then
		lintRole = 2
	Else
		lintRole = 1
	End If
	'+ Se busca el cliente de la póliza
	If mclsNull_condi.FindClientName("2", mintBranch, mintProduct, mlngPolicy, mlngCertif, lintRole, mclsPolicy.dStartdate) Then
		mstrClienName = mclsNull_condi.sCliename
		mstrClienCode = mclsNull_condi.sClient
	End If
	
	'+ Se verifica si la póliza tiene beneficiarios
	If lclsBeneficiar.valExist("2", mintBranch, mintProduct, mlngPolicy, mlngCertif, mclsPolicy.dStartdate, "0") Then
		mstrBenefExist = "1"
	Else
		mstrBenefExist = "0"
	End If
	
	mclsNull_condi = Nothing
	lclsBeneficiar = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca033")



mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca033"
mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mclsPolicy = New ePolicy.Policy

With mobjValues
	mintBranch = .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
	mintProduct = .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
	mlngPolicy = .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
	mlngCertif = .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
End With

    lclsProduct = New eProduct.Product
    lclsProduct.Find(mintBranch,mintProduct,Today)
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
    var mintBranch_j
    var mintProduct_j
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 25 $|$$Date: 16/09/04 13:19 $"
//% ShowNullDateChanges: Rutinas que se ejecutan cuando el control Fecha de Anulación toma valor
//--------------------------------------------------------------------------------------------
function ShowNullDateChanges()
//--------------------------------------------------------------------------------------------
{   
    var ldtmstartdate = '<%=Session("dStartdate")%>';
    
    with (document.forms[0])
	{
		if (tcdNullDate.value != '')
		{    
/*		    optReceipt[0].disabled = tcdNullDate.value == ldtmstartdate;
	        optReceipt[1].disabled = optReceipt[0].disabled;
	        optReceipt[2].disabled = optReceipt[0].disabled;
	        optReceipt[0].checked = tcdNullDate.value == ldtmstartdate;
*/	        
	        if(tcdNullDate.value == ldtmstartdate){
				optDev[0].disabled = true;
				optDev[1].disabled = true;
				optDev[2].disabled = true;
				optDev[0].checked  = false;
				optDev[1].checked  = false;
				optDev[2].checked  = false;
				tcnPercent.value='';
				tcnPercent.disabled=true;
	        }
	        
		    valNullCode.disabled = false;
		    btnvalNullCode.disabled = false;
			
//busca los dias de aviso de anulacion
			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=NullAdvise&nBranch=" + mintBranch_j + "&nProduct=" + mintProduct_j + "&nNullDate=" + self.document.forms[0].tcdNullDate.value, "ShowDefValuesPolicyNullDate", 1, 1,"no","no",2000,2000);
			self.document.forms[0].valNullCode.Parameters.Param3.sValue = tcdNullDate.value;
		 }
		else
		{
		    valNullCode.disabled = true;
		    btnvalNullCode.disabled = true;
		 }
		 UpdateDiv("valNullCodeDesc","","Normal")  
		 valNullCode.value = '';
	 }
 }
//% ShowLockDevolution: Habilita/Inhabilita los botones de opción de Devolución de Prima
//--------------------------------------------------------------------------------------
function ShowLockDevolution(blnDisabled)
//--------------------------------------------------------------------------------------
{   
    with (document.forms[0])
    {
		elements["optDev"][0].disabled = blnDisabled;
		elements["optDev"][1].disabled = blnDisabled;
		elements["optDev"][2].disabled = blnDisabled;
		if (elements["optDev"][2].checked){
		elements["tcnPercent"].disabled = blnDisabled;}

        if (blnDisabled)
		{
			elements["tcnPercent"].value = '';
			elements["optDev"][0].checked = blnDisabled;
			elements["optDev"][1].checked = !blnDisabled;
			elements["optDev"][2].checked = !blnDisabled;			
		 }
     }
 }
//% ShowLockPercent: Habilita/Inhabilita el cuadro de Porcentaje Fijo
//-------------------------------------------------------------------
function ShowLockPercent(blnDisabled)
//-------------------------------------------------------------------
{
    document.forms[0].elements["tcnPercent"].disabled = blnDisabled;
    document.forms[0].elements["tcnPercent"].value = '';
 }
//% ChangeHeaderValues: re-asigna los valores al header
//-------------------------------------------------------------------
function ChangeHeaderValues(){
//-------------------------------------------------------------------
    var frm = top.fraHeader.document.forms[0]
    
    frm.cbeBranch.value = '<%=Session("nBranch")%>'
    frm.valProduct.value = '<%=Session("nProduct")%>'
    frm.tcnPolicy.value = '<%=Session("nPolicy")%>'
    frm.tcnCertif.value = '<%=Session("nCertif")%>'
}

//% ShowDevValue: Muestra el valor por defecto para la forma de cálculo tomada del tipo de anulación
//--------------------------------------------------------------------------------------------
function ShowDevValue(nNullCode){
//--------------------------------------------------------------------------------------------
	if (nNullCode!='')  
		ShowPopUp("/VTimeNet/Policy/PolicyTra/ShowDefValues.aspx?Field=OptDev&nNullCode=" + nNullCode + "&nBranch=" + <%=Session("nBranch")%> + "&nProduct=" + <%=Session("nProduct")%> + "&dNullDate=" + self.document.forms[0].tcdNullDate.value,"ShowDefValuesPolicyNullDate", 1, 1,"no","no",2000,2000);
		//InsDefValues('OptDev',"nNullCode=" + nNullCode + "&nBranch=" + <%=Session("nBranch")%> + "&nProduct=" + <%=Session("nProduct")%> + "&dNullDate=" + self.document.forms[0].tcdNullDate.value,'/VTimeNet/Policy/PolicyTra');
}

//--------------------------------------------------------------------------------------------
function DisabledSOAT(){
//--------------------------------------------------------------------------------------------
    with (document.forms[0])
    {
		elements["optDev"][2].disabled = true;
		elements["optDev"][2].checked = true;
		elements["optReceipt"][0].disabled = true;
		elements["optReceipt"][0].checked = true;
		elements["tcnPercent"].disabled = true;
		elements["chkNullRequest"].disabled = true;
        elements["chkNullReceipt"].disabled = false;
        elements["chkNullReport"].disabled = true;
        elements["chkNullRequest"].checked = false;
        elements["chkNullReceipt"].checked = true;
        elements["chkNullReport"].checked = true;
    }
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CA033", "CA033.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
	<%Call insPreFolder()%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmNullPolicy" ACTION="ValPolicyTra.aspx?x=1&nTransacion=<%=mclsPolicy.nTransactio%>&nProponum=<%=Request.QueryString.Item("nProponum")%>"> 
    	<%=mobjValues.ShowWindowsName("CA033", Request.QueryString.Item("sWindowDescript"))%>
<%
'+ Variable que habilita o deshabilita el check de generacion de solicitud dependiendo si
'+ la ejecucion es preliminar o definitiva

If CStr(Session("optExecute")) = "1" Then
	mblnDisabled = False
	lnDisabled = "1"
Else
	mblnDisabled = True
	lnDisabled = "2"
End If
%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5"CLASS="HighLighted"><LABEL ID=41119><A NAME="Anulación"><%= GetLocalResourceObject("AnchorAnulaciónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HORLINE"></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcdNullDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdNullDate", mdtmdEffecdateRequest,  , GetLocalResourceObject("tcdNullDateToolTip"),  ,  ,  , "ShowNullDateChanges();")%></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD><LABEL ID=13800><%= GetLocalResourceObject("valNullCodeCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nBranch", mintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", mintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", mdtmdEffecdateRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(.PossiblesValues("valNullCode", "tabNull_condi", 2, mintNullcodeRequest, True, , , , , "ShowDevValue(this.value)", , , GetLocalResourceObject("valNullCodeToolTip")))
End With%>
			</TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=41120><A NAME="Devolución"><%= GetLocalResourceObject("AnchorDevoluciónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HORLINE"></TD>
        </TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41121><A NAME="Recibo"><%= GetLocalResourceObject("AnchorReciboCaption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41122><A NAME="Cálculo"><%= GetLocalResourceObject("AnchorCálculoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HORLINE"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HORLINE"></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(41129, "optReceipt", GetLocalResourceObject("optReceipt_1Caption"), mintTyp_recRequest, "1", "ShowLockDevolution(true)")%></TD>
            <TD>&nbsp;</TD>
			<%If mlintReturn = "2" Then%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(41132, "optDev", GetLocalResourceObject("optDev_2Caption"), "1", "2", "ShowLockPercent(true)", True)%></TD>
			<%Else%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(41132, "optDev", GetLocalResourceObject("optDev_2Caption"), "2", "2", "ShowLockPercent(true)", True)%></TD>
			<%End If%>

        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(41130, "optReceipt", GetLocalResourceObject("optReceipt_2Caption"), mintTyp_recRequest - 1, "2", "ShowLockDevolution(false)")%></TD>
			<TD>&nbsp;</TD>
            <%If mlintReturn = "3" Then%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(41133, "optDev", GetLocalResourceObject("optDev_3Caption"), "1", "3", "ShowLockPercent(true)", True)%></TD>
			<%Else%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(41133, "optDev", GetLocalResourceObject("optDev_3Caption"), "2", "3", "ShowLockPercent(true)", True)%></TD>
			<%End If%>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(41131, "optReceipt", GetLocalResourceObject("optReceipt_3Caption"), mintTyp_recRequest - 2, "3", "ShowLockDevolution(true)")%></TD>
            <TD>&nbsp;</TD>
            <%If mlintReturn = "4" Then%>
				<TD><%= mobjValues.OptionControl(41134, "optDev", GetLocalResourceObject("optDev_4Caption"), "1", "4", "ShowLockPercent(false)", True)%></TD>
				<TD><%=mobjValues.NumericControl("tcnPercent", 2, mintRate,  , GetLocalResourceObject("tcnPercentToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
			<%Else%>	
				<TD><%=mobjValues.OptionControl(41134, "optDev", GetLocalResourceObject("optDev_4Caption"), "2", "4", "ShowLockPercent(false)", True)%></TD>
				<TD><%=mobjValues.NumericControl("tcnPercent", 2, mintRate,  , GetLocalResourceObject("tcnPercentToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <%End If%>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>            
            <%If mlintReturn = "9" Then%>
				<TD><%=mobjValues.OptionControl(41135, "optDev", GetLocalResourceObject("optDev_5Caption"), "1", "9", "ShowLockPercent(True)", True)%></TD>
			<%Else%>	
				<TD><%=mobjValues.OptionControl(41135, "optDev", GetLocalResourceObject("optDev_5Caption"), "2", "9", "ShowLockPercent(True)", True)%></TD>
            <%End If%>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=41123><A NAME="Otros datos"><%= GetLocalResourceObject("AnchorOtros datosCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HORLINE"></TD>
        </TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNullRequest", GetLocalResourceObject("chkNullRequestCaption"), lnDisabled, CStr(1),  , mblnDisabled)%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNullReceipt", GetLocalResourceObject("chkNullReceiptCaption"), mstrNull_recRequest, CStr(1))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><%= mobjValues.CheckControl("chkNullReport", GetLocalResourceObject("chkNullReportCaption"), "1", CStr(1))%></TD>
			<TD COLSPAN="2">&nbsp;</TD>
			<TD><%=mobjValues.HiddenControl("valNullLetter", "")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=41124><A NAME="Datos de verificación"><%= GetLocalResourceObject("AnchorDatos de verificaciónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HORLINE"></TD>
        </TR>
        <TR>
			<TD COLSPAN="1"><%Response.Write(mobjValues.CheckControl("chkBeneficiaries", GetLocalResourceObject("chkBeneficiariesCaption"), mstrBenefExist, "1",  , True))%></TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%=mobjValues.DIVControl("lblNullAdv")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=13794><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD><LABEL ID=41125><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
			<TD COLSPAN="3"><LABEL ID=41126><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
        </TR>
        <TR>
			<TD><LABEL ID=13797><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
			<TD><LABEL ID=41127><%= GetLocalResourceObject("Anchor6Caption") %></LABEL></TD>
			<TD COLSPAN="3"><LABEL ID=41128><%= GetLocalResourceObject("Anchor7Caption") %></LABEL></TD>
        </TR>
    </TABLE>
    <%=mobjValues.HiddenControl("hddClient", mstrClienCode)%>
    <%=mobjValues.HiddenControl("hddCapital", CStr(mclsPolicy.nCapital))%>
    <%
Response.Write(mobjValues.BeginPageButton)
Response.Write("<SCRIPT>ChangeHeaderValues();</SCRIPT>")
        
        If mintTyp_recRequest = 2 Then
            Response.Write("<SCRIPT>ShowLockDevolution(false);</SCRIPT>")
        End If
        
        If lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmSegurosProvisionales Then
            Response.Write("<SCRIPT>DisabledSOAT();</SCRIPT>")
        End If
%>
</FORM>
</BODY>
</HTML>
<%

'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("ca033")

mobjNetFrameWork = Nothing
mclsPolicy = Nothing
mobjValues = Nothing

'^End Footer Block VisualTimer
%>




