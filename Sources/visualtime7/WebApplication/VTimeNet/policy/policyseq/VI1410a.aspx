<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para obtener la información de fecuencias permitidas por vías de pago
Dim mclsPer_deposit_month As ePolicy.Per_deposit_month
Dim mcolPer_deposit_months As ePolicy.Per_deposit_months
'- Objeto para validar si el primer aporte es modificable
Dim mclsproduct As eProduct.Product
Dim mclscertificat As ePolicy.Certificat
Dim mclsobject As Boolean


'%insDefineHeader. Definición de columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	With mobjGrid
		
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 5, vbNullString,  , GetLocalResourceObject("tcnMonthColumnCaption"))
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep_auxColumnCaption"), "tcnAmountdep_aux", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep_auxColumnCaption"), True, 6)
		.Columns.AddHiddenColumn("tcnYear_ini", Request.QueryString.Item("nYear_ini"))
		.Columns.AddHiddenColumn("tcnPay", Request.QueryString.Item("nPay"))
		
		
	End With
	
	With mobjGrid
		.Codispl = "VI1410A"
		.Codisp = "VI1410A"
		.Top = 135
		.Left = 100
		.Width = 350
		.Height = 350
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		.Columns("tcnMonth").EditRecord = False
		.bCheckVisible = False
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreVI1410A: Carga los datos de la forma
'---------------------------------------------------------------------------------------
Private Sub insPreVI1410A()
	'---------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lintIndexFind As Object
	
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	<DIV ID=""Scroll"" STYLE=""width:550;height:225;overflow:auto;outset gray"">")

	
	' manejo para bloquear el aporte de primer mes 
	
	mclsobject = False
	Call mclsproduct.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	
	If mclsproduct.sApv = "1" And mclsproduct.nPayiniti > 0 Then
		mclscertificat = New ePolicy.Certificat
		
		Call mclscertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
		If mclscertificat.nPayfreq = 6 Then
			mclsobject = True
		End If
		mclscertificat = Nothing
		
	End If
	lintIndex = 0
	
	
	With mobjGrid
		If mcolPer_deposit_months.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nYear_ini"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode")) Then
			
			For	Each mclsPer_deposit_month In mcolPer_deposit_months
				.Columns("tcnMonth").HRefScript = " "
				.Columns("tcnMonth").DefValue = CStr(mclsPer_deposit_month.nMonth)
				.Columns("tcnAmountdep_aux").DefValue = CStr(mclsPer_deposit_month.nAmountdep_aux)
				.Columns("Sel").Checked = CShort(mclsPer_deposit_month.sSel)
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				If mclsPer_deposit_month.nMonth = 1 And mclsobject Then
					.Columns("tcnMonth").EditRecord = False
					.Columns("tcnMonth").Disabled = True
					.Columns("tcnAmountdep_aux").Disabled = True
				Else
					.Columns("tcnMonth").EditRecord = True
					.Columns("tcnMonth").Disabled = False
					.Columns("tcnAmountdep_aux").Disabled = False
				End If
				Response.Write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			Next mclsPer_deposit_month
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("	</DIV>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("VI1410A"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	<TABLE>")

	
End Sub

'% insPreVI1410AUpd: Realiza la eliminación de una fila de frecuencias por via de pago/producto
'----------------------------------------------------------------------------------------------
Private Sub insPreVI1410AUpd()
	'----------------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valPolicySeq.aspx", "VI1410A", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI1410")

'- Variables auxiliares

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI1410"

mclsPer_deposit_month = New ePolicy.Per_deposit_month
mcolPer_deposit_months = New ePolicy.Per_deposit_months
mclsproduct = New eProduct.Product

mobjGrid = New eFunctions.Grid
mobjGrid.sCodisplPage = "VI1410"
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI1410"

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:59 $"        

// insCheckSelClick : Establece La acción a ejecutar dependiendo del estado del campo Sel
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrParam=''
    if (!Field.checked){
		with (self.document.forms [0]){
        lstrParam = "tcnYear_ini="+marrArray[lintIndex].tcnYear_ini + 
					"&tcnMonth=" + marrArray[lintIndex].tcnMonth
        }
        EditRecord(lintIndex,nMainAction,"Del",lstrParam)
    }
    else{
		with (self.document.forms [0]){
			lstrParam=	"tcnYear_ini="+marrArray[lintIndex].tcnYear_ini + 
						"&tcnMonth=" + marrArray[lintIndex].tcnMonth
		}
        EditRecord(lintIndex,nMainAction,"Update",lstrParam)
    }
    Field.checked = !Field.checked
}

/*% ChangeValue: Recarga la página tras  cambiar valores en combos
/*---------------------------------------------------------------------------------------------------------*/
function ChangeValues(Field){
/*---------------------------------------------------------------------------------------------------------*/
	if (Field.checked==true)
// si esta desmarcado y se marca 
		Field.defvalue = "1";
	else
// si esta marcado y se desmarca 
		Field.defvalue = "2";
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("VI1410"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT> var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmVI1410A" ACTION="valProductSeq.aspx?Time=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI1410A()
Else
	Call insPreVI1410AUpd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsPer_deposit_month = Nothing
mcolPer_deposit_months = Nothing
mclsproduct = Nothing
%>
</FORM>
</BODY> 
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("VI1410")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




