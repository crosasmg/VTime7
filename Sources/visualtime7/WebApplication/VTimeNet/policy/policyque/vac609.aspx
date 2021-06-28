<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Parametros de transaccion
Dim mstrBranch As String
Dim mstrProduct As String
Dim mstrPolicy As String
Dim mstrCertif As String


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddAnimatedColumn(0, "", "btnDetail", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("btnDetailColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIdMovColumnCaption"), "tcnIdMov", 5, vbNullString,  , GetLocalResourceObject("tcnIdMovColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdMovdateColumnCaption"), "tcdMovdate", vbNullString,  , GetLocalResourceObject("tcdMovdateColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctTypeMoveColumnCaption"), "tctTypeMove", 30, vbNullString,  , GetLocalResourceObject("tctTypeMoveColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctYearMonthColumnCaption"), "tctYearMonth", 8, vbNullString,  , GetLocalResourceObject("tctYearMonthColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, vbNullString,  , GetLocalResourceObject("tcnReceiptColumnToolTip"))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VAC609"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreVAC609: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVAC609()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lcolMove_Accpol As ePolicy.Move_accpols
	Dim lclsMove_Accpol As Object
	lcolMove_Accpol = New ePolicy.Move_accpols
	If lcolMove_Accpol.FindByDate(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dMovedate"), eFunctions.Values.eTypeData.etdDate, True)) Then
		For	Each lclsMove_Accpol In lcolMove_Accpol
			With mobjGrid
				.Columns("tcnIdMov").DefValue = lclsMove_Accpol.nIdmov
				.Columns("tcdMovDate").DefValue = lclsMove_Accpol.dMovDate
				.Columns("tctTypeMove").DefValue = lclsMove_Accpol.sTypemove
				.Columns("tctYearMonth").DefValue = lclsMove_Accpol.nYear & "/" & lclsMove_Accpol.nMonth
				.Columns("tcnAmount").DefValue = lclsMove_Accpol.nAmount
				.Columns("tcnReceipt").DefValue = lclsMove_Accpol.nReceipt
				.Columns("btnDetail").HRefScript = "insShowDetail(" & lclsMove_Accpol.nIdmov & ");"
				Response.Write(.DoRow)
			End With
		Next lclsMove_Accpol
	End If
	Response.Write(mobjGrid.closeTable())
	lcolMove_Accpol = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vac609")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vac609"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = True 'Request.QueryString("nMainAction") = 401
'+ Se cargan datos de parametros
With Request
	mstrBranch = .QueryString.Item("nBranch")
	mstrProduct = .QueryString.Item("nProduct")
	mstrPolicy = .QueryString.Item("nPolicy")
	mstrCertif = .QueryString.Item("nCertif")
End With
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:37 $"

//%insShowDetail: Muestra ventana de desglose de movimiento de Valor Póliza
//-----------------------------------------------------------------------
function insShowDetail(nMovement){
//-----------------------------------------------------------------------
//- Variables para almacenar parametros
    var lstrBranch = '<%=mstrBranch%>';
    var lstrProduct = '<%=mstrProduct%>';
    var lstrPolicy = '<%=mstrPolicy%>';
    var lstrCertif = '<%=mstrCertif%>';
    var lstrParams = new String;
    lstrParams += 'nBranch=' +  lstrBranch + 
                  '&nProduct=' + lstrProduct + 
                  '&nPolicy=' + lstrPolicy + 
                  '&nCertif=' + lstrCertif +
                  '&nMovement=' + nMovement;
    ShowPopUp('/VTimeNet/Common/GoTo.aspx?sModule=Policy&sProject=PolicyQue&bAutomatic=false&sCodispl=VAC610&' + lstrParams, 'Desglose', 795, 540, 'no', 'no', 0, 0)
}

function insChangeField(){
//-------------------------------------------------------------------------------
//- Variable para parametros
    var lstrParams = new String;
    with(document.forms[0]){
        lstrParams = 'sCertype=2';
        lstrParams +='&nBranch=<%=Request.QueryString.Item("nBranch")%>';
		lstrParams += '&nProduct=<%=Request.QueryString.Item("nProduct")%>' ;
		lstrParams += '&nPolicy=<%=Request.QueryString.Item("nPolicy")%>' ;
		lstrParams += '&nCertif=<%=Request.QueryString.Item("nCertif")%>' ;
		lstrParams += '&nRole=2' ;
lstrParams += '&dEffecdate=<% %>
<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>' ;
		lstrParams += '&sExecCertif=1';
		insDefValues('insValPolitype',lstrParams);
	    insDefValues('AccPolDat',lstrParams);				
    }
}


</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VAC609", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();" ONLOAD="insChangeField();">
<FORM METHOD="POST" NAME="VAC609" ACTION="ValPolicyQue.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VAC609", Request.QueryString.Item("sWindowDescript")))
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "vac609"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
Call insDefineHeader()
Call insPreVAC609()
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("vac609")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




