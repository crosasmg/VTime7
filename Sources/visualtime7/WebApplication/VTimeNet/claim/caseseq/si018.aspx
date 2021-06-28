<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsAuto As ePolicy.Automobile
Dim mclsClaim_auto As eClaim.Claim_auto
Dim mclsClient As eClient.Client
Dim mclsClient2 As eClient.Client
Dim mstrQueryString As String

'- Variables auxiliares

Dim mstrDriverCode As String
Dim mstrDriverName As String
Dim mstrLicense As String
Dim mdtmDriverDate As Object


'%Procedimiento insPreSI018. Este procedimiento se encarga de cargar los valores de las
'%tablas en los controles de la ventana
'--------------------------------------------------------------------------------------------
Private Sub insPreSI018()
	'--------------------------------------------------------------------------------------------
	
	mstrDriverCode = vbNullString
	mstrDriverName = vbNullString
	mstrLicense = vbNullString
	
	Call mclsAuto.Find("2", mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(CStr(Session("dEffecdate"))))
	
	'+ Se asignan los valores del código y nombre de conductor, número de licencia y fecha de la misma
	'+ tomados de los datos particulares de la póliza
	If mclsAuto.sClient <> vbNullString Then
		mstrDriverCode = mclsAuto.sClient
		mstrDriverName = mclsAuto.sCliename
	End If
	
	If mclsAuto.sLicense <> vbNullString Then
		mstrLicense = mclsAuto.sLicense
	End If
	
	If mclsAuto.dDriverDat <> CStr(eRemoteDB.Constants.dtmNull) Then
		mdtmDriverDate = mclsAuto.dDriverDat
	End If
	
	Call mclsClaim_auto.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble))
	
	'+ Si ya existe información previamente registrada en los datos del auto involucrado en el siniestro,
	'+ se asignan los valores del código y nombre de conductor, número de licencia y fecha de la misma
	
	If mclsClaim_auto.sDriver_cod <> vbNullString Then
		mstrDriverCode = mclsClaim_auto.sDriver_cod
		mstrDriverName = mclsClaim_auto.sCliename
	End If
	
	If mclsClaim_auto.sLicense <> vbNullString Then
		mstrLicense = mclsClaim_auto.sLicense
	End If
	
	If mclsClaim_auto.dDriverDat <> eRemoteDB.Constants.dtmNull Then
		mdtmDriverDate = mclsClaim_auto.dDriverDat
	End If
	
	'+ Se obtienen los datos personales del conductor al momento del siniestro    
	Call mclsClient.Find(mclsClaim_auto.sDriver_claim)
	'+ Se obtienen los datos personales del testigo al momento del siniestro        
	Call mclsClient2.Find(mclsClaim_auto.sWitness)
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si018")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "si018"
	mclsAuto = New ePolicy.Automobile
	mclsClaim_auto = New eClaim.Claim_auto
	mclsClient = New eClient.Client
	mclsClient2 = New eClient.Client
End With
mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 21/01/04 10:41 $"        

//% insCheckDriver: Verifica si el cliente se encuentra previamente registrado; en caso 
//%                 contrario es generado automáticamente.
//--------------------------------------------------------------------------------------------------
function insCheckDriver(sClient){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
	    if (sClient!='')
	    {
		    insDefValues('CheckClient','sClient=' + tctDriver.value ,'/VTimeNet/Claim/CaseSeq');			
		}
	    else
	    {
			tctFatherLastName.value='';
			tctMotherLastName.value='';
			tctNames.value='';			
            dtcBirthdayDate.value='';            

			tctFatherLastName.disabled = true;
			tctMotherLastName.disabled = true;
			tctNames.disabled = true;
			btn_dtcBirthdayDate.disabled = true;
			
			UpdateDiv('lblCliename2','','Normal');
		}
	}
}
//% insCheckDriver: Verifica si el cliente se encuentra previamente registrado; en caso 
//%                 contrario es generado automáticamente.
//--------------------------------------------------------------------------------------------------
function insCheckWitness(sClient){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0])
    {
		if (sClient!='')
			insDefValues('CheckWitness','sClient=' + tctWitness.value ,'/VTimeNet/Claim/CaseSeq')
		else
		{

			tctFatherLastNameWitness.value='';
			tctMotherLastNameWitness.value='';
			tctNamesWitness.value='';			

			tctFatherLastNameWitness.disabled = true;
			tctMotherLastNameWitness.disabled = true;
			tctNamesWitness.disabled = true;
			
			UpdateDiv('lblCliename3','','Normal');
		}
	}
}

//--------------------------------------------------------------------------------------------------
		function DriverOnChangeCustomHandler()
//--------------------------------------------------------------------------------------------------
{
    with (self.document.forms[0])
    {

		if (tctFatherLastName.disabled)
		{
			insCheckDriver("");
		}
		else if (tctFatherLastName.value!="" || tctMotherLastName.value!="" || tctNames.value!="" || dtcBirthdayDate.value!="" ) 
		{
			alert("Ha ingresado un nuevo rut. Se limpiarán los datos del conductor que fueron digitados recientemente.");
				insCheckDriver("");
		}
	}
}

$(function() {
	$("[name=tctDriver]").get(0).OnChangeCustomHandler = DriverOnChangeCustomHandler;
});

</SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Claim.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
	
	<%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.setZone(2, "SI018", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI018", Request.QueryString("sWindowDescript")))
End With
%>	
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI018" ACTION="valCaseSeq.aspx?smode=1">
    <P ALIGN="Center">
		<LABEL ID=40217><A HREF="#Datos del vehículo asegurado"> Datos del vehículo asegurado</A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40219><A HREF="#Datos sobre el accidente"> Datos sobre el accidente</A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40221><A HREF="#Licencia"> Licencia</A></LABEL></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40223><A HREF="#Denuncia"> Denuncia</A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40225><A HREF="#Constancia policial"> Constancia policial</A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40225><A HREF="#Datos del testigo"> Datos del testigo</A></LABEL>
		
		
	</P>
    	<%=mobjValues.ShowWindowsName("SI018", Request.QueryString("sWindowDescript"))%>
	<%Call insPreSI018()%>
    <TABLE WIDTH="100%">
        <TR></TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40222><A NAME="Datos del vehículo asegurado">Datos del vehículo asegurado</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9572>Patente</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRegist", 10, mclsAuto.sRegist,  , "Matrícula o placa del vehículo asegurado", True)%></TD>
            <%Session("sRegist")= mclsAuto.sRegist%>
            <TD COLSPAN="2"></TD>
        </TR>    
            <TD><LABEL ID=9574>Motor</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctMotor", 40, mclsAuto.sMotor,  , "Número identificativo del motor", True)%></TD>
			<TD><LABEL ID=9568>Chasis</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctChassis", 40, mclsAuto.sChassis,  , "Número de chasis o motor del vehículo", True)%></TD><TR>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40223><A NAME="Datos sobre el accidente">Datos sobre el accidente</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=9569>Conductor habitual</LABEL></TD>	
			<TD COLSPAN="3"><%
mstrQueryString = "&nClaim=" & Session("nClaim") & "&nCase_num=" & Session("nCase_num") & "&nDeman_type=" & Session("nDeman_type") & "&sBene_type="
Response.Write(mobjValues.ClientControl("tctDriverCod", mstrDriverCode,  , "Datos correspondientes al conductor habitual",  , True,  ,  ,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientClaim,  ,  ,  , mstrQueryString, True))%></TD>
			<%="<SCRIPT>UpdateDiv('lblCliename','" & Replace(mstrDriverName, "'", "´") & "','Normal');</SCRIPT>"%>
        </TR>
        <TR>
			<TD><LABEL ID=0>Conductor</LABEL></TD>
			    <%
mstrQueryString = "&nClaim=" & Session("nClaim") & "&nCase_num=" & Session("nCase_num") & "&nDeman_type=" & Session("nDeman_type") & "&sBene_type="
%>                      
			<TD COLSPAN="3"><%=mobjValues.ClientControl("tctDriver", mclsClaim_auto.sDriver_claim,  , "Código del conductor al momento del siniestro", "insCheckDriver(this.value)",  ,  , True,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient,  ,  ,  , mstrQueryString, True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Apellido paterno</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctFatherLastName", 19, mclsClient.sLastName,  , "Apellido paterno del conductor",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Apellido materno</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctMotherLastName", 19, mclsClient.sLastName2,  , "Apellido materno del conductor",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Nombres</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctNames", 19, mclsClient.sFirstname,  , "Nombres del conductor",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Fecha de nacimiento</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.DateControl("dtcBirthdayDate", CStr(mclsClient.dBirthdat),  , "Fecha de nacimiento del conductor del vehículo al momento del siniestro",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40224><A NAME="Licencia">Licencia</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
            <TD><LABEL ID=9573>Número</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctLicense", 10, mclsClient.sLicense,  , "Número de la licencia de conducir",  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=9570>Fecha</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDriverDate", CStr(mclsClient.dDriverDat),  , "Fecha de la licencia de conducir",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9567>Responsabilidad del conductor</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboBlame", "Table204", 1, mclsClaim_auto.sBlame,  ,  ,  ,  ,  ,  ,  ,  , "Indicador de culpabilidad del asegurado")%></TD>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkDenunc", "Denuncia policial", mclsClaim_auto.sPoliceDem)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9571>Infracción de tránsito</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboInfraction", "Table205", 1, mclsClaim_auto.sInfraction,  ,  ,  ,  ,  ,  ,  ,  , "Indica si se realizó una infracción de tránsito")%></TD>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkSummary", "Sumario", mclsClaim_auto.sSummary)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9566>Nro. de vehículos involucrados</LABEL></TD>
            <%If mclsClaim_auto.nAuto_quant = 0 Then%>
                <TD><%=mobjValues.NumericControl("tcnAutoQuant", 2, "1",  , "Número de vehículos implicados en el accidente",  , 0)%></TD>
            <%Else%>
                <TD><%=mobjValues.NumericControl("tcnAutoQuant", 2, CStr(mclsClaim_auto.nAuto_quant),  , "Número de vehículos implicados en el accidente",  , 0)%></TD>
            <%End If%>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkIntervEIR", "Asistencia en viaje", mclsClaim_auto.sInd_Eir)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9575>Taller asignado</LABEL></TD>
            <TD><%
With mobjValues
	.Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nBene_type", eClaim.Claim_case.eClaimRole.clngClaimRWorkShop, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("sBene_type", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0,  , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeWorksh", "TabClaimbenef", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsClaim_auto.nWorksh), True,  ,  ,  ,  ,  ,  ,  , "Taller en donde se encuentra en reparación el vehículo"))
End With
%>
            </TD>
            <TD></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><A NAME="Denuncia">Denuncia</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>No.parte</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.NumericControl("tcnPartNumber", 10, CStr(mclsClaim_auto.nFine),  , "Número de parte asociado a la denuncia")%></TD>
            <% Session("nFine")= CStr(mclsClaim_auto.nFine)%>
        </TR>
        <TR>
			<TD><LABEL ID=0>Juzgado</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctTribunal", 40, mclsClaim_auto.sCourt,  , "Juzgado que lleva la denuncia")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Fecha de denuncia</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.DateControl("dtcAccusationDate", CStr(mclsClaim_auto.dDemand_date),  , "Fecha en que se realiza la denuncia")%></TD>
            <%Session("dDemand_date") = CStr(mclsClaim_auto.dDemand_date) %>
        </TR>
        <TR>
			<TD><LABEL ID=0>Comisaría</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctPoliceStation", 40, mclsClaim_auto.sPolStat_deman,  , "Comisaría donde se realizó la denuncia")%></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><A NAME="Constancia policial">Constancia policial</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Folio</LABEL></TD>			
			<TD><%=mobjValues.NumericControl("tcnFolio", 5, CStr(mclsClaim_auto.nPage),  , "Número de folio en la constancia policial")%></TD>
			<TD><LABEL ID=0>Párrafo</LABEL></TD>			
			<TD><%=mobjValues.NumericControl("tcnParagraph", 5, CStr(mclsClaim_auto.nParagraph),  , "Número de párrafo en la constancia policial")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Comisaría</LABEL></TD>			
			<TD><%=mobjValues.TextControl("tctPoliceStation2", 40, mclsClaim_auto.sPol_Station,  , "Comisaría donde se emitió la constanca policial")%></TD>
			<TD><LABEL ID=0>No. de constancia</LABEL></TD>			
			<TD><%=mobjValues.NumericControl("tcnEnduranceNumber", 10, CStr(mclsClaim_auto.nPoliceDoc),  , "Número de la constancia policial")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Fecha de constancia</LABEL></TD>			
			<TD><%=mobjValues.DateControl("dtcEnduranceDate", CStr(mclsClaim_auto.dPoldoc_date),  , "Fecha de emisión de la constancia policial")%></TD>
			<TD COLSPAN="2"></TD>
        </TR>


        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><A NAME="Datos del testigo">Datos del testigo</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Testigo</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.ClientControl("tctWitness", mclsClaim_auto.sWitness,  , "Código del testigo al momento del siniestro", "insCheckWitness(this.value)",  ,  , True,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Apellido paterno</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctFatherLastNameWitness", 19, mclsClient2.sLastName,  , "Apellido paterno del testigo",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Apellido materno</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctMotherLastNameWitness", 19, mclsClient2.sLastName2,  , "Apellido materno del testigo",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0>Nombres</LABEL></TD>			
			<TD COLSPAN="3"><%=mobjValues.TextControl("tctNamesWitness", 19, mclsClient2.sFirstname,  , "Nombres del testigo",  ,  ,  ,  , True)%></TD>
        </TR>
        

        <TR>
			<TD><%=mobjValues.CheckControl("chkAlcohol", "Alcoholemia", mclsClaim_auto.sAlcoholic)%></TD>
			<TD></TD>
			<TD><LABEL ID=0>Notas</LABEL></TD>
			<TD><% =mobjValues.ButtonNotes("SCA2-10", mclsClaim_auto.nNotenum, False, CBool(Session("bQuery")))%></TD>
        </TR>

    </TABLE>
    <%Response.Write(mobjValues.BeginPageButton)%>
</FORM>
</BODY>
</HTML>

<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mclsAuto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsAuto = Nothing
'UPGRADE_NOTE: Object mclsClaim_auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsClaim_auto = Nothing
'UPGRADE_NOTE: Object mclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsClient = Nothing
'UPGRADE_NOTE: Object mclsClient2 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsClient2 = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si018")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




