<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As eBatch.Batch_logs

'-Variables para almacenar parámetros
Dim mstrCodispl As Object
Dim mstrKey As String
Dim mstrCodisplCancel As String


'% insDefineHeader: se definen las propiedades del grid según transacción
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	Select Case mstrCodispl
		Case "CA099A", "CO001_K"
			'+ Se definen las columnas del grid    
			With mobjGrid.Columns
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnSeqColumnCaption"), "tcnSeq", 5, CStr(0))
				Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptionColumnCaption"), "tctDescription", 80, vbNullString,  , GetLocalResourceObject("tctDescriptionColumnToolTip"))
			End With
			
			'+Se asigna transacción que se invoca al cancelar la ventana
			If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
				mstrCodisplCancel = "CA099C"
			Else
				mstrCodisplCancel = "CA099"
			End If
	End Select
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = mstrCodispl
		.ActionQuery = mobjValues.ActionQuery
		.AddButton = False
		.DeleteButton = False
		.Height = 350
		.Width = 300
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCodispl()
	'--------------------------------------------------------------------------------------------
	Dim lclsClass As Object
	Select Case mstrCodispl
		Case "CA099A", "CO001_K"
			mcolClass = New eBatch.Batch_logs
			With mobjGrid
				If mcolClass.FindKey(mstrKey) Then
					For	Each lclsClass In mcolClass
						.Columns("tcnSeq").DefValue = lclsClass.nMessseq
						.Columns("tctDescription").DefValue = lclsClass.sLog
						Response.Write(.DoRow)
					Next lclsClass
				End If
			End With
	End Select
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

'+Se toman parametros pasados a la pagina
With Request
	mstrCodispl = .QueryString.Item("sCodispl")
	mstrKey = .QueryString.Item("sKey")
End With

'+Se configura la grilla y se define la transaccion siguiente
Call insDefineHeader()

%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<TITLE>Resultados de proceso</TITLE>
<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 5-04-06 13:16 $|$$Author: Clobos $"
    
//%insEnd: Termino de proceso. Cierra ventana y recarga la invocada
//--------------------------------------------------------------------------------------------
function insEnd(sCodispl){
//--------------------------------------------------------------------------------------------    
    top.close();
    if (sCodispl!='') {
		switch (sCodispl){
//+ Acciones dependiendo del código de la ventana.
			case "CA099A":
				top.opener.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=<%=mstrCodisplCancel%>';
				break;
			default:
				break;
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="ShowResults" ACTION="">
<%=mobjValues.ShowWindowsName(mstrCodispl)%>
	<DIV ID="Scroll" STYLE="width:640;height:250;overflow:auto;outset gray">
<%Call insPreCodispl()%>
	</DIV>
	<TABLE WIDTH=100%>
		<TR>
			<TD CLASS="HORLINE"></TD>
		</TR>
		<TR>
			<TD ALIGN="RIGHT"><%Response.Write(mobjValues.ButtonAcceptCancel("insEnd('" & mstrCodispl & "')", "", True, , eFunctions.Values.eButtonsToShow.OnlyAccept))%></TD>
		</TR>
	<TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
mcolClass = Nothing
%>




