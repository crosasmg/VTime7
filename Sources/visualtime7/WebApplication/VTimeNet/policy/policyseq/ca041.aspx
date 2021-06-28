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
Dim mclsCurren_pol As ePolicy.Curren_pol
Dim mobjGrid As eFunctions.Grid
Dim mclsProduct As eProduct.Product
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader : Configura los datos del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddHiddenColumn("hddExist", "2")
		Call .AddHiddenColumn("hddChange", "")
		Call .AddTextColumn(41459, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 11,  ,  , GetLocalResourceObject("tcnExchangeColumnToolTip"), True, 6)
		Call .AddHiddenColumn("hddCurrency", "")
		Call .AddHiddenColumn("hddExchange", "")
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP041"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") <> "401"
		.Columns("Sel").OnClick = "insSelect(this);"
	End With
End Sub

'% insPreDP055 : Muestra los datos repetitivos de la página.
'---------------------------------------------------------------------------------------------
Private Sub InspreCA041()
	'---------------------------------------------------------------------------------------------
	Dim mclsCurren_pol_count As ePolicy.Curren_pol
	Dim lintCount As Integer
	
	mclsCurren_pol_count = New ePolicy.Curren_pol
	'+ Define las colmnas del grid.    
	Call insDefineHeader()
	
	'+ Se obtienen los datos de la grilla
	If mclsCurren_pol.LoadCurrency(mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True, mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)) Then
		'+ Se obtienen los datos del producto
		Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		'+ Se verifica si existen registros en Curren_pol                              
		Call mclsCurren_pol_count.Count_Curren_pol(mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		lintCount = 0
		For lintCount = 0 To mclsCurren_pol.CountCurrenPol
			If mclsCurren_pol.Val_Curren_pol(lintCount) Then
				With mobjGrid
					.Columns("tctDescript").DefValue = mclsCurren_pol.sDescript
					.Columns("tcnExchange").DefValue = CStr(mclsCurren_pol.nExchange)
					.Columns("hddExchange").DefValue = CStr(mclsCurren_pol.nExchange)
					.Columns("hddChange").DefValue = ""
					If Request.QueryString.Item("nMainAction") <> "401" Then
						.Columns("Sel").Checked = mclsCurren_pol.nExist
						.Columns("hddExist").DefValue = CStr(mclsCurren_pol.nExist)
						.Columns("hddChange").DefValue = CStr(mclsCurren_pol.nExist)
					End If
					.Columns("hddCurrency").DefValue = CStr(mclsCurren_pol.nCurrency)
					'+ Si es póliza matriz y no existen registros en Curren_pol se deja chequeada
					'+ la moneda preseleccionada por omisión en Curr_allow
					If mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble) = 0 And mobjValues.StringToType(CStr(mclsCurren_pol_count.nCount), eFunctions.Values.eTypeData.etdDouble) = 0 And mclsCurren_pol.sDefaulti = CDbl("1") Then
						
						.Columns("Sel").Checked = CShort("1")
						.Columns("hddExist").DefValue = "1"
					End If
					'+ Si es un certificado y se seleccionó sólo una moneda en la póliza matriz
					'+ éste debe estar chequeado	                
					If mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(CStr(mclsCurren_pol_count.nCount), eFunctions.Values.eTypeData.etdDouble) = 1 Then
						.Columns("Sel").Checked = CShort("1")
						.Columns("hddExist").DefValue = "1"
					End If
					'+ Si es un certificado y se seleccionó mas de una moneda en la poliza matriz
					'+ y el producto permite 1 sola moneda se asocia la moneda por omisión del producto	                
					If mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(CStr(mclsCurren_pol_count.nCount), eFunctions.Values.eTypeData.etdDouble) > 1 And mobjValues.StringToType(CStr(mclsProduct.nqmaxcurr), eFunctions.Values.eTypeData.etdDouble) > 1 Then
						If mobjValues.StringToType(CStr(mclsCurren_pol.sDefaulti), eFunctions.Values.eTypeData.etdDouble) = 1 Then
							.Columns("Sel").Checked = CShort("1")
							.Columns("hddExist").DefValue = "1"
						End If
					End If
					Response.Write(mobjGrid.DoRow)
				End With
			End If
		Next 
		
	End If
	Response.Write(mobjGrid.closeTable())
	mobjValues = Nothing
	mclsCurren_pol = Nothing
	mclsCurren_pol_count = Nothing
	mobjGrid = Nothing
	mclsProduct = Nothing
	mobjMenu = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA041")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mclsCurren_pol = New ePolicy.Curren_pol
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
'~End Body Block VisualTimer Utility
mclsProduct = New eProduct.Product

mobjValues.ActionQuery = Session("bQuery")
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript">
    var marrCA041 = []
    var mintCount = -1

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

//%insSelect: Controla la selección/des-selección de los elementos del grid
//-------------------------------------------------------------------------------------------
function insSelect(Field){
//-------------------------------------------------------------------------------------------    
	var lerrVar;
	try{
		self.document.forms[0].hddExist[Field.value].value = (Field.checked?1:2)
	}catch(lerrVar){
		self.document.forms[0].hddExist.value = (Field.checked?1:2)
	}
}
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
	<FORM METHOD="post" ID="FORM" NAME="CA041" ACTION="valPolicySeq.aspx?">	
<%
Call InspreCA041()
%>
</TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA041")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




