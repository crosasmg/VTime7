<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsClaim As Object
Dim lcolClaims As Object


'% insDefineHeader: Se definen los campos del grid
    
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(40291, "Siniestro", "tcnClaim", 10, CStr(0),  , "Número identificativo del siniestro de la línea en tratamiento.")
		Call .AddCheckColumn(40299, "Aprobar", "chkStatus", CStr(eRemoteDB.Constants.strNull),  , "1")
		Call .AddAnimatedColumn(0, "Consultar", "sLink", "/VTimeNet/Images/clfolder.png", "Operaciones sobre siniestros(Consulta).")
		Call .AddDateColumn(40298, "Fecha de declaración", "tcdDecladat", CStr(eRemoteDB.Constants.dtmNull),  , "Fecha en que fue declarado el siniestro.")
		Call .AddTextColumn(40294, "Sucursal", "tctOfficeDesc", 50, CStr(eRemoteDB.Constants.strNull),  , "Sucursal a la que pertenece la póliza del siniestro en tratamiento.")
		Call .AddTextColumn(40295, "Ramo", "tctBranchDesc", 50, CStr(eRemoteDB.Constants.strNull),  , "Ramo al que pertenece la póliza del siniestro en tratamiento")
		Call .AddTextColumn(40296, "Producto", "tctProductDesc", 50, CStr(eRemoteDB.Constants.strNull),  , "Producto al que pertenece la póliza del siniestro en tratamiento.")
		Call .AddNumericColumn(40292, "Póliza", "tcnPolicy", 8, CStr(0),  , "Número identificativo de la póliza del siniestro en tratamiento.")
		Call .AddNumericColumn(40293, "Certif.", "tcnCertif", 8, CStr(0),  , "Número del certificado de la póliza del siniestro.")
		Call .AddTextColumn(40297, "Asegurado", "tctCliename", 50, CStr(eRemoteDB.Constants.strNull),  , "Nombre del asegurado de la póliza siniestrada.")
		Call .AddHiddenColumn("tcnAuxClaim", CStr(0))
		Call .AddHiddenColumn("tcnAuxBranch", CStr(0))
		Call .AddHiddenColumn("tcnAuxProduct", CStr(0))
		Call .AddHiddenColumn("tcnAuxPolicy", CStr(0))
		Call .AddHiddenColumn("tcnAuxCertif", CStr(0))
		Call .AddHiddenColumn("tctClaimtyp", " ")
		Call .AddHiddenColumn("tcnMovement", CStr(0))
		Call .AddHiddenColumn("tctClient", " ")
		Call .AddHiddenColumn("chkAuxStatus", CStr(2))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "SI051"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreSI051: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI051()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsClaim As eClaim.Claim
	Dim lcolClaims As eClaim.Claims
	
	lintIndex = 0
	With Server
		lclsClaim = New eClaim.Claim
		lcolClaims = New eClaim.Claims
	End With
	
	If lcolClaims.Find_SIO51(CDbl(Session("nClaim")), CInt(Session("nBranch")), CInt(Session("nProduct")), CInt(Session("nPolicy")), CDate(Session("dInitial_date")), CDate(Session("dFinal_date")), "8") Then
		For	Each lclsClaim In lcolClaims
			With mobjGrid
				.Columns("tcnClaim").DefValue = CStr(lclsClaim.nClaim)
				.Columns("chkStatus").Checked = 2
				.Columns("sLink").HRefScript = "ShowSI001(" & lintIndex & ")"
				.Columns("tcdDecladat").DefValue = CStr(lclsClaim.dDecladat)
				.Columns("tctOfficeDesc").DefValue = lclsClaim.sOfficeDesc
				.Columns("tctBranchDesc").DefValue = lclsClaim.sBranchDesc
				.Columns("tctProductDesc").DefValue = lclsClaim.sProductDesc
				.Columns("tcnPolicy").DefValue = CStr(lclsClaim.nPolicy)
				.Columns("tcnCertif").DefValue = CStr(lclsClaim.nCertif)
				.Columns("tctCliename").DefValue = lclsClaim.sCliename
				.Columns("tcnAuxClaim").DefValue = CStr(lclsClaim.nClaim)
				.Columns("tcnAuxBranch").DefValue = CStr(lclsClaim.nBranch)
				.Columns("tcnAuxProduct").DefValue = CStr(lclsClaim.nProduct)
				.Columns("tcnAuxPolicy").DefValue = CStr(lclsClaim.nPolicy)
				.Columns("tcnAuxCertif").DefValue = CStr(lclsClaim.nCertif)
				.Columns("tctClaimtyp").DefValue = IIf(lclsClaim.sClaimtyp = String.Empty," ",lclsClaim.sClaimtyp)
				.Columns("tcnMovement").DefValue = CStr(IIf(lclsClaim.nMovement <= 0, 0,lclsClaim.nMovement))
				.Columns("tctClient").DefValue = IIf(lclsClaim.sClient = String.Empty," ", lclsClaim.sClient) 
				.Columns("chkAuxStatus").Checked = 2
				.Columns("chkStatus").OnClick = "ChangeValue(this," & lintIndex & "," & lcolClaims.Count & ")"
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsClaim
	End If
	Response.Write(mobjGrid.closeTable())
	
	'UPGRADE_NOTE: Object lcolClaims may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolClaims = Nothing
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si051_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si051_k"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si051_k"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
</SCRIPT>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

<SCRIPT>
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//% ChangeValue: se cambia el valor del campo auxiliar del Check, para poder utilizarlo en 
//				 valClaim.aspx
//-------------------------------------------------------------------------------------------
function ChangeValue(Field,Index,Count){
//-------------------------------------------------------------------------------------------	
	if(Count > 1)
        (Field.checked)?self.document.forms[0].chkAuxStatus[Index].value=1:self.document.forms[0].chkAuxStatus[Index].value=2
    else
        (Field.checked)?self.document.forms[0].chkAuxStatus.value=1:self.document.forms[0].chkAuxStatus.value=2
}

//% ShowSI001: Muestra la pantalla de Operaciones sobre sinientros como consulta
//--------------------------------------------------------------------------------------------
function ShowSI001(Index){
//--------------------------------------------------------------------------------------------    		
	insDefValues('DP051_Claim','nClaim=' + marrArray[Index].tcnClaim);
    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Claim&sProject=ClaimSeq&sCodispl=SI001&DP051_nClaim=' + marrArray[Index].tcnClaim, 'Claim', 1011, 709, 'no', 'no', 0, 0);
}
</SCRIPT>
<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI051", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	End If
	
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI051", Request.QueryString("sWindowDescript")))
	
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmPenApprCla" ACTION="valClaim.aspx?zone=2">
    <%Response.Write(mobjValues.ShowWindowsName("SI051", Request.QueryString("sWindowDescript")))%>
    <TABLE WIDTH="100%">
      <%Call insDefineHeader()
Call insPreSI051()%>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
  
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.12
Call mobjNetFrameWork.FinishPage("si051_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




