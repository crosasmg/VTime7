<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues



'+ Definición del Encabezado del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lclsBulletins_det As eCollection.Bulletins_det
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "co514"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	lclsBulletins_det = New eCollection.Bulletins_det
	
Response.Write(" <TR>    " & vbCrLf)
Response.Write("          <TD><LABEL ID=0>" & GetLocalResourceObject("tctNullCodeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("             ")

	Call lclsBulletins_det.insFindC0514_K("CO514", Session("nBulletins"))
Response.Write("" & vbCrLf)
Response.Write("          <TD>")


Response.Write(mobjValues.PossiblesValues("tctNullCode", "table5005", eFunctions.Values.eValuesType.clngComboType, CStr(lclsBulletins_det.nNull_Cod),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("tctNullCodeToolTip")))


Response.Write("<BR></TD>                      " & vbCrLf)
Response.Write("       </TR>" & vbCrLf)
Response.Write("       <TR>" & vbCrLf)
Response.Write("          <TD WIDTH=""100%"" BORDER=4 >&nbsp;</TD>" & vbCrLf)
Response.Write("       </TR> " & vbCrLf)
Response.Write("   ")

	
	With mobjGrid
		.Codispl = "CO514"
		.Codisp = "CO514"
		.DeleteButton = False
		.AddButton = False
	End With
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctReceiptColumnCaption"), "tctReceipt", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctReceiptColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDocumentColumnCaption"), "tctDocument", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctDocumentColumnToolTip"),  ,  ,  , True)
        Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctBranchColumnCaption"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 10, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctProductColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctPolicyColumnCaption"), "tctPolicy", 10, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctPolicyColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctTratypeiColumnCaption"), "tctTratypei", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctTratypeiColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecDateColumnCaption"), "tcdEffecDate",  ,  ,  ,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat",  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tctPremiumColumnCaption"), "tctPremium", 20, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6,  ,  ,  , True)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		
		'+ Si la transacción es "Consulta", se oculta la columna SEL 
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		Else
			.Columns("Sel").GridVisible = False
		End If
		
		'	    .Top=100
		.Height = 390
		.Width = 420
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
	
	lclsBulletins_det = Nothing
End Sub

'+ Proceso que llena los Campos del Grid
'------------------------------------------------------------------------------
Private Sub insPreCO514()
	'------------------------------------------------------------------------------
	Dim lcolBulletins_dets As eCollection.Bulletins_dets
	Dim lclsBulletins_det As eCollection.Bulletins_det
	Dim lclsPremium As eCollection.Premium
	lcolBulletins_dets = New eCollection.Bulletins_dets
	lclsBulletins_det = New eCollection.Bulletins_det
	lclsPremium = New eCollection.Premium
	
	If lcolBulletins_dets.Find(mobjValues.StringToType(Session("nBulletins"), eFunctions.Values.eTypeData.etdDouble), "", "") Then
		For	Each lclsBulletins_det In lcolBulletins_dets
			With lclsBulletins_det
				
				If lclsPremium.Find_Receipt_det(.sCertype, .nReceipt, .nBranch, .nProduct) Then
					
					mobjGrid.Columns("tctReceipt").DefValue = CStr(lclsPremium.nReceipt)
					mobjGrid.Columns("tctBranch").DefValue = lclsPremium.sDesBranch
					mobjGrid.Columns("tctProduct").DefValue = lclsPremium.sdescProd
					mobjGrid.Columns("tctPolicy").DefValue = CStr(lclsPremium.nPolicy)
					mobjGrid.Columns("tctTratypei").DefValue = lclsPremium.sDesTratypei
					mobjGrid.Columns("tcdEffecDate").DefValue = CStr(lclsPremium.dEffecDate)
					mobjGrid.Columns("tcdExpirdat").DefValue = CStr(lclsPremium.dExpirdat)
					mobjGrid.Columns("tctPremium").DefValue = CStr(lclsPremium.nPremium)
					
					If .nContrat > 0 Then
						mobjGrid.Columns("tctDocument").DefValue = .nContrat & "(" & .nDraft & ")"
					End If
					
				End If
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsBulletins_det
	End If
	'+ Se debe tener el número de boletín              
	With mobjGrid
		.Columns("Sel").GridVisible = False
	End With
	Response.Write(mobjGrid.closeTable())
	
	lclsBulletins_det = Nothing
	lcolBulletins_dets = Nothing
	lclsPremium = Nothing
	
End Sub

'+ Proceso que actualiza el Grid
'------------------------------------------------------------------------------
Private Sub insPreCO514Upd()
	'------------------------------------------------------------------------------
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCollectionTra.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co514")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co514"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
    </SCRIPT>

  
    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenues = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
		mobjMenues.sSessionID = Session.SessionID
		mobjMenues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenues.setZone(2, "CO514", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenues = Nothing
	End If
	.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>")
End With
%>
<SCRIPT>
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//-------------------------------------------------------------------------------------------------------------------
{
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

</SCRIPT>
   
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabCommission" ACTION="ValCollectionTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CO514", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCO514()
Else
	Call insPreCO514Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co514")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




