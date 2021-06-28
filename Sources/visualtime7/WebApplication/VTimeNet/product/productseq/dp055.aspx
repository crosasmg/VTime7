<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mobjCur_allow As eProduct.Cur_Allow
Dim mobjCur_allows As eProduct.Cur_Allows


'% insDefineHeader : Configura los datos del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddTextColumn(41459, GetLocalResourceObject("tctChangeColumnCaption"), "tctChange", 30, "")
		Call .AddHiddenColumn("hSel", "")
		Call .AddCheckColumn(41460, GetLocalResourceObject("tctPreSelectionColumnCaption"), "tctPreSelection", "",  ,  ,  ,  , GetLocalResourceObject("tctPreSelectionColumnToolTip"))
		Call .AddHiddenColumn("htctPreSelection", "")
		Call .AddHiddenColumn("hCodigint", "")
		Call .AddHiddenColumn("hCurrency", "")
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP055"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
	End With
End Sub
'% insPreDP055 : Muestra los datos repetitivos de la página.
'---------------------------------------------------------------------------------------------
Private Sub insPreDP055()
	'---------------------------------------------------------------------------------------------
	'+ Contador local que indica el índice de la línea en proceso
	Dim lintCount As Short
	Dim lstrState As String
	'+ Objeto para el manejo de productos
	Dim lobjProduct As eProduct.Product
	Dim lobjGeneralFunction As eGeneral.GeneralFunction
	lobjProduct = New eProduct.Product
	lobjGeneralFunction = New eGeneral.GeneralFunction
	'+ Define las colmnas del grid.
	Call insDefineHeader()
	If mobjCur_allows.Find_DP005(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
		'+ Se obtienen los datos del producto
		Call lobjProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		
		'+ Realiza la validación 11386 : Deben existir datos en la DP005
		lstrState = mobjCur_allow.insStateDP055(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble), lobjProduct)
		If Not Trim(lstrState) = vbNullString Then
			Response.Write(lstrState)
			mobjGrid.ActionQuery = True
		End If
		'+ Obtiene el máximo de monedas preseleccionadas para el producto.
		Response.Write("<SCRIPT>mintQmaxcurr = " & lobjProduct.nQmaxcurr & "</" & "Script>")
		'+ Obtiene la descripción asociada con el producto.
		Response.Write("<SCRIPT>mstrErrDescription = '11208 : " & lobjGeneralFunction.insLoadMessage(11208) & "'</" & "Script>")
		lintCount = 0
		For	Each mobjCur_allow In mobjCur_allows
			With mobjGrid
				'+ Cogigint
				.Columns("hCodigint").DefValue = CStr(mobjCur_allow.nCodigInt)
				'+ Monedas
				.Columns("hCurrency").DefValue = CStr(mobjCur_allow.nCurrency)
				'+ Descripción
				.Columns("tctChange").DefValue = mobjCur_allow.sDescript
				'+ Selección
				If CShort(mobjCur_allow.nCurrency) > 0 Then
					.Columns("Sel").Checked = CShort("1")
					.Columns("hSel").DefValue = "1"
				ElseIf Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And mobjCur_allow.nCodigInt = 1 Then 
					If lobjProduct.sStyle_comm = "2" Or lobjProduct.sStyle_prem = "2" Or lobjProduct.sStyle_tax = "2" Then
						.Columns("Sel").Checked = CShort("1")
						.Columns("hSel").DefValue = "1"
					Else
						.Columns("Sel").Checked = CShort("0")
						.Columns("hSel").DefValue = ""
					End If
				Else
					.Columns("Sel").Checked = CShort("0")
					.Columns("hSel").DefValue = ""
				End If
				'+ Preseleccionada
				If Not mobjCur_allow.sDefaulti = "1" Then
					.Columns("tctPreSelection").Checked = CShort("0")
					.Columns("htctPreSelection").DefValue = "0"
				Else
					.Columns("tctPreSelection").Checked = CShort(mobjCur_allow.sDefaulti)
					.Columns("htctPreSelection").DefValue = "1"
					Response.Write("<SCRIPT>mCount++;</" & "Script>")
				End If
				.Columns("Sel").OnClick = "UpdateSel(this," & lintCount & ")"
				.Columns("tctPreSelection").OnClick = "insvalPresel(this," & lintCount & ")"
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		Next mobjCur_allow
	End If
	Response.Write(mobjGrid.closeTable)
	Response.Write(mobjValues.BeginPageButton)
	lobjProduct = Nothing
	lobjGeneralFunction = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
mobjCur_allow = New eProduct.Cur_Allow
mobjCur_allows = New eProduct.Cur_Allows
mobjValues.ActionQuery = Session("bQuery")

mobjGrid.sCodisplPage = "DP055"
mobjValues.sCodisplPage = "DP055"

%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $"

var mCount = 0;
// Máximo de monedas predefinidas para el producto
var mintQmaxcurr = 0;
var mstrErrDescription
//% insValPresel: Valida que las modenas preseleccionadas por el usuario no excedan las
//%               definidas por el producto.
//-------------------------------------------------------------------------------------------
function insvalPresel(objCheck, Index){
//-------------------------------------------------------------------------------------------
//+ Actualiza el estado del contador y de los hidden.
    if(objCheck.checked){
        mCount++;
        self.document.forms[0].htctPreSelection[Index].value = 1;
        self.document.forms[0].hSel[Index].value = '1';
        self.document.forms[0].Sel[Index].checked = true;
    }else{
        mCount--;
        self.document.forms[0].htctPreSelection[Index].value = 0;
    }
//+ Si el excede el número de monedas preseleccionadas establecidas para el producto, 
//+ se muestra el mensaje de error correspondiente y se reversa el check    
    if(mCount>mintQmaxcurr){
        alert(mstrErrDescription);
        objCheck.checked = false;
        self.document.forms[0].htctPreSelection[Index].value = 0;
        mCount--;
    }
}
//%	UpdateCheck: Actualiza el campo hidden relacionado con el check en selección
//-------------------------------------------------------------------------------------------
function UpdateSel(ObjCheck, Index){
//-------------------------------------------------------------------------------------------
    if(ObjCheck.checked){
        self.document.forms[0].hSel[Index].value = '1'
    }else{
        self.document.forms[0].hSel[Index].value = ''
         if (self.document.forms[0].htctPreSelection[ObjCheck.value].value == 1)
		{  mCount--;}
		self.document.forms[0].htctPreSelection[ObjCheck.value].checked=0;
		self.document.forms[0].tctPreSelection[ObjCheck.value].checked=0;
    }
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP055"))
	.Write(mobjMenu.setZone(2, "DP055", "DP055.aspx"))
End With
mobjMenu = Nothing
%>    
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmDP055" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
            <%Call insPreDP055()%>
        </FORM>
    </BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
mobjCur_allow = Nothing
mobjCur_allows = Nothing
%>





