<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para manejo del menú	
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del Grid
Dim mobjGrid As eFunctions.Grid
Dim mobjGrid_Grilla As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid_Grilla = New eFunctions.Grid'Se usara una variable grid diferente: para no hacer conexiones a la BD de mas:	
	
	mobjGrid.sCodisplPage = "opc720_k"
	mobjGrid_Grilla.sCodisplPage = "opc720_k"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeAgenColumnCaption"), "cbeOfficeAgen", "Table5556", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeAgenColumnToolTip"))
		Call .AddClientColumn(CInt("0"), GetLocalResourceObject("tctClientCodeColumnCaption"), "tctClientCode", "",  , GetLocalResourceObject("tctClientCodeColumnToolTip"), "insShowCashnum();", True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCashnumColumnCaption"), "tcnCashnum", 5, "",  , GetLocalResourceObject("tcnCashnumColumnCaption"), False,  ,  ,  , "insShowClient();", False, 2)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate",  ,  , GetLocalResourceObject("tcdStartdateColumnToolTip"),  ,  ,  ,  , 3)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInitCloseCashColumnCaption"), "tcdInitCloseCash",  ,  , GetLocalResourceObject("tcdInitCloseCashColumnToolTip"),  ,  ,  ,  , 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEndCloseCashColumnCaption"), "tcdEndCloseCash",  ,  , GetLocalResourceObject("tcdEndCloseCashColumnToolTip"),  ,  ,  ,  , 7)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCloseOKCashColumnCaption"), "tcdCloseOKCash",  ,  , GetLocalResourceObject("tcdCloseOKCashColumnToolTip"),  ,  ,  ,  , 8)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCash_idColumnCaption"), "tcnCash_id", 10, "",  , GetLocalResourceObject("tcnCash_idColumnToolTip"), False,  ,  ,  ,  , False, 4)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusColumnCaption"), "cbeStatus", "table5562", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeStatusColumnToolTip"),  , 5)
		Call .AddClientColumn(CInt("0"), GetLocalResourceObject("tctSupCodeColumnCaption"), "tctSupCode", "",  , GetLocalResourceObject("tctSupCodeColumnToolTip"), "insShowCashnum();", False,  ,  ,  ,  ,  , 9)
		Call .AddClientColumn(CInt("0"), GetLocalResourceObject("tctHeadCodeColumnCaption"), "tctHeadCode", "",  , GetLocalResourceObject("tctHeadCodeColumnToolTip"), "insShowCashnum();", False,  ,  ,  ,  ,  , 10)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OPC720"
		.Codisp = "OPC720_K"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 350
		.Width = 400
		.Top = 100
		.Left = 300
		.Columns("tctSupCode").PopUpVisible = False
		.Columns("tctHeadCode").PopUpVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	'+ Se definen las columnas del grid que se usa exclusivamente para laGrilla (no para la ventana PopUp)
	With mobjGrid_Grilla.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeAgenColumnCaption"), "cbeOfficeAgen", "Table5556", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeAgenColumnToolTip"))
		Call .AddTextColumn(CInt("0"), GetLocalResourceObject("tctClientCodeColumnCaption"), "tctClientCode", 50, "",  , GetLocalResourceObject("tctClientCodeColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCashnumColumnCaption"), "tcnCashnum", 5, "",  , GetLocalResourceObject("tcnCashnumColumnCaption"), False,  ,  ,  , "insShowClient();", False, 2)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate",  ,  , GetLocalResourceObject("tcdStartdateColumnToolTip"),  ,  ,  ,  , 3)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInitCloseCashColumnCaption"), "tcdInitCloseCash",  ,  , GetLocalResourceObject("tcdInitCloseCashColumnToolTip"),  ,  ,  ,  , 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEndCloseCashColumnCaption"), "tcdEndCloseCash",  ,  , GetLocalResourceObject("tcdEndCloseCashColumnToolTip"),  ,  ,  ,  , 7)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCloseOKCashColumnCaption"), "tcdCloseOKCash",  ,  , GetLocalResourceObject("tcdCloseOKCashColumnToolTip"),  ,  ,  ,  , 8)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCash_idColumnCaption"), "tcnCash_id", 10, "",  , GetLocalResourceObject("tcnCash_idColumnToolTip"), False,  ,  ,  ,  , False, 4)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusColumnCaption"), "cbeStatus", "table5562", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeStatusColumnToolTip"),  , 5)
		Call .AddTextColumn(CInt("0"), GetLocalResourceObject("tctSupCodeColumnCaption"), "tctSupCode", 50, "",  , GetLocalResourceObject("tctSupCodeColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(CInt("0"), GetLocalResourceObject("tctHeadCodeColumnCaption"), "tctHeadCode", 50, "",  , GetLocalResourceObject("tctHeadCodeColumnToolTip"),  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid_Grilla
		.Codispl = "OPC720"
		.Codisp = "OPC720_K"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 350
		.Width = 400
		.Top = 100
		.Left = 300
		.Columns("tctSupCode").PopUpVisible = False
		.Columns("tctHeadCode").PopUpVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub


'% insPreOPC720: Lectura de la tabla de estados de cajas basada en las condiciones indicadas por el usuario
'----------------------------------------------------------------------------------------------------------
Private Sub insPreOPC720()
	'----------------------------------------------------------------------------------------------------------
	Dim lclsCash_stat As eCashBank.Cash_stat
	Dim lcolCash_stats As eCashBank.Cash_stats
	
	With Server
		lclsCash_stat = New eCashBank.Cash_stat
		lcolCash_stats = New eCashBank.Cash_stats
	End With
	If (Request.QueryString.Item("sCashnum") <> vbNullString And Request.QueryString.Item("sCashnum") <> "0") Or (Request.QueryString.Item("sOfficeAgen") <> vbNullString And Request.QueryString.Item("sOfficeAgen") <> "0") Or Request.QueryString.Item("sStartdate") <> vbNullString Or (Request.QueryString.Item("sStatus") <> vbNullString And Request.QueryString.Item("sStatus") <> "0") Or (Request.QueryString.Item("sCash_id") <> vbNullString And Request.QueryString.Item("sCash_id") <> "0") Or Request.QueryString.Item("sInitCloseCash") <> vbNullString Or Request.QueryString.Item("sEndCloseCash") <> vbNullString Or Request.QueryString.Item("sCloseOkCash") <> vbNullString Then
		
		If lcolCash_stats.FindOPC720(mobjValues.StringToType(Request.QueryString.Item("sOfficeAgen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("sCashnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("sStartdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("sCash_id"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("sStatus"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("sInitCloseCash"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("sEndCloseCash"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("sCloseOkCash"), eFunctions.Values.eTypeData.etdDate)) Then
			
			For	Each lclsCash_stat In lcolCash_stats
				With mobjGrid_Grilla
					.Columns("cbeOfficeAgen").DefValue = CStr(lclsCash_stat.nOfficeagen)
					.Columns("tctClientCode").DefValue = lclsCash_stat.sClient & lclsCash_stat.sDigit & " " & lclsCash_stat.sCliename
					.Columns("tcnCashnum").DefValue = CStr(lclsCash_stat.nCashnum)
					.Columns("tcdStartdate").DefValue = CStr(lclsCash_stat.dStartdate)
					.Columns("tcnCash_id").DefValue = CStr(lclsCash_stat.nCash_id)
					.Columns("cbeStatus").DefValue = CStr(lclsCash_stat.nStatus)
					.Columns("cbeStatus").Descript = lclsCash_stat.sDsp_Status
					.Columns("tcdInitCloseCash").DefValue = CStr(lclsCash_stat.dInitCloseCash)
					.Columns("tcdEndCloseCash").DefValue = CStr(lclsCash_stat.dEndCloseCash)
					.Columns("tcdCloseOKCash").DefValue = CStr(lclsCash_stat.dCloseOkCash)
					.Columns("tctSupCode").DefValue = lclsCash_stat.sClientSup & lclsCash_stat.sDigitSup & " " & lclsCash_stat.sClienameSup
					.Columns("tctHeadCode").DefValue = lclsCash_stat.sClientHeadSup & lclsCash_stat.sDigitHeadSup & " " & lclsCash_stat.sClienameHeadSup
				End With
				Response.Write(mobjGrid_Grilla.DoRow())
			Next lclsCash_stat
		End If
	End If
	
	Response.Write(mobjGrid_Grilla.closeTable())
	
	lclsCash_stat = Nothing
	lcolCash_stats = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("opc720_k")

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "OPC720_K"
%>
<HTML>
<HEAD>
   
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
	<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\cashbank\cashbank\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>	
<%End If%>

	<SCRIPT>
	      document.VssVersion="$$Revision: 2 $|$$Date: 5/03/04 12:23 $"
	       


//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
}
//------------------------------------------------------------------------------------------
function insPreZone(llngMainAction){
//------------------------------------------------------------------------------------------
    switch (llngMainAction){
        case 402:
	        EditRecord(-1, llngMainAction,'Add')
	        break;
	}
}

//------------------------------------------------------------------------------------------
function insShowCashnum(){
//------------------------------------------------------------------------------------------
    insDefValues("Client_OPC720", "sClient=" + self.document.forms[0].tctClientCode.value,'/VTimeNet/Cashbank/Cashbank')
}

//------------------------------------------------------------------------------------------
function insShowClient(){
//------------------------------------------------------------------------------------------
    insDefValues("Cashnum_OPC720", "nCashnum=" + self.document.forms[0].tcnCashnum.value,'/VTimeNet/Cashbank/Cashbank')
}

</SCRIPT>

<%

With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("OPC720"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "OPC720_k.aspx"))
		.Write(mobjMenu.MakeMenu("OPC720", "OPC720_k.aspx", 2, ""))
		.Write("<BR>")
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCash_stat" ACTION="ValCashBank.aspx?Zone=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreOPC720()
Else
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCashBank.aspx", "OPC720", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
End If
mobjGrid = Nothing
mobjGrid_Grilla = Nothing
%>    
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.55
Call mobjNetFrameWork.FinishPage("opc720_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





