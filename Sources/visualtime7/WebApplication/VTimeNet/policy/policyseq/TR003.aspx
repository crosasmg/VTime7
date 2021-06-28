<%@ Page Language="VB" explicit="true"  Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	
	'+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddPossiblesColumn(17674, GetLocalResourceObject("cbeClassMerchColumnCaption"), "cbeClassMerch", "TABTRAN_CLASSTR003", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , True, 2, GetLocalResourceObject("cbeClassMerchColumnToolTip"), , eFunctions.Values.eTypeCode.eNumeric)
                
            ' Call .AddPossiblesColumn(17674, GetLocalResourceObject("cbeClassMerchColumnCaption"), "cbeClassMerch", "Table232", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeClassMerchColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(17675, GetLocalResourceObject("cbePackingColumnCaption"), "cbePacking", "Table237", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbePackingColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddTextColumn(17676, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "", False, GetLocalResourceObject("tctDescriptColumnToolTip"), , , , False)
            Call .AddNumericColumn(17677, GetLocalResourceObject("tcnQuanTransColumnCaption"), "tcnQuanTrans", 4, "", False, GetLocalResourceObject("tcnQuanTransColumnToolTip"), False, 0, , , , False)
            Call .AddPossiblesColumn(17678, GetLocalResourceObject("cbeUnitColumnCaption"), "cbeUnit", "Table6013", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeUnitColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(17679, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "", False, GetLocalResourceObject("tcnAmountColumnToolTip"), False, 6, , , , False)
            Call .AddPossiblesColumn(17680, GetLocalResourceObject("cbeFranDediColumnCaption"), "cbeFranDedi", "Table64", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2,GetLocalResourceObject("cbeFranDediColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(17681, GetLocalResourceObject("tcnFranDedRateColumnCaption"), "tcnFranDedRate", 5, "", False, GetLocalResourceObject("tcnFranDedRateColumnToolTip"), False, 2, , , , False)
            Call .AddNumericColumn(17682, GetLocalResourceObject("tcnMinAmountColumnCaption"), "tcnMinAmount", 10, "", False, GetLocalResourceObject("tcnMinAmountColumnToolTip"), , , , , , False)
        End With
	
        With mobjGrid
            
            With .Columns("cbeClassMerch").Parameters
                .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "TR003"
            .Codisp = "TR003"
            .Top = 100
            .Height = 416
            .Width = 425
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbeClassMerch").EditRecord = True
            .Columns("cbeClassMerch").Disabled = Request.QueryString.Item("Action") = "Update"
            .Columns("cbePacking").Disabled = Request.QueryString.Item("Action") = "Update"
            .sDelRecordParam = "nClassMerch='+ marrArray[lintIndex].cbeClassMerch + '" & "&nPacking='+ marrArray[lintIndex].cbePacking + '" & "&nCurrency= " & Request.QueryString.Item("nCurrency")
		
            .sEditRecordParam = "nCurrency= ' + self.document.forms[0].cbeCurrency.value + '"
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
End Sub

'%insPreTR003. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreTR003()
	'------------------------------------------------------------------------------
	Dim lcolTran_merchs As ePolicy.Tran_merchs
	Dim lclsTran_merch As Object
	
	lcolTran_merchs = New ePolicy.Tran_merchs
	
	If IsNothing(Request.QueryString.Item("nCurrency")) Then
		Response.Write("<script>ReloadPage(self.document.forms[0].cbeCurrency);</" & "Script>")
	Else
		With mobjGrid
		If (mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong, True) <> eRemoteDB.Constants.intNull) Then		
			If lcolTran_merchs.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), CInt(Request.QueryString.Item("nCurrency")), Session("dEffecdate")) Then
				For	Each lclsTran_merch In lcolTran_merchs
					.Columns("cbeClassMerch").DefValue = lclsTran_merch.nClassMerch
					.Columns("cbePacking").DefValue = lclsTran_merch.nPacking
					.Columns("tctDescript").DefValue = lclsTran_merch.sDescript
					.Columns("tcnQuanTrans").DefValue = lclsTran_merch.nQuanTrans
					.Columns("cbeUnit").DefValue = lclsTran_merch.nUnit
					.Columns("tcnAmount").DefValue = lclsTran_merch.nAmount
					.Columns("cbeFranDedi").DefValue = lclsTran_merch.sFranDedi
					.Columns("tcnFranDedRate").DefValue = lclsTran_merch.nFranDedRate
					.Columns("tcnMinAmount").DefValue = lclsTran_merch.nMinAmount
					Response.Write(mobjGrid.DoRow())
				Next lclsTran_merch
			End If
        End If			
		End With
	End If
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsTran_merch = Nothing
	lcolTran_merchs = Nothing
End Sub

'% insPreTR003Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreTR003Upd()
	'------------------------------------------------------------------------------
	Dim lclsTran_merch As ePolicy.Tran_merch
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTran_merch = New ePolicy.Tran_merch
			Call lclsTran_merch.InsPostTR003(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nClassMerch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nPacking"), eFunctions.Values.eTypeData.etdInteger), "", 0, 0, 0, "", 0, CStr(0), CInt(Request.QueryString.Item("nCurrency")))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyseq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsTran_merch = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("TR003")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "TR003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<script>var nMainAction = top.frames['fraSequence'].plngMainAction</script>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

%> 

<script  type="text/javascript">
//-------------------------------------------------------------------------------------------
function ReloadPage(Field){
//-------------------------------------------------------------------------------------------
	with(document.location){
		href = href.replace(/&nCurrency.*/,'') + '&nCurrency=' + Field.value
	}
}
</script>


<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<html>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<script>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</script>")
End With
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST"	ID="FORM" NAME="frmTR003" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nCurrency=<%=Request.QueryString.Item("nCurrency")%>">
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
		<TABLE ALIGN="CENTER" WIDTH="50%">
			<TR>
				<TD><LABEL ID=17683>Moneda</LABEL></TD>
 				<%	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With%>                  
				<TD><%	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCurren_pol", 1, Request.QueryString.Item("nCurrency"), True, False,  ,  ,  , "ReloadPage(this)", False,,vbNullString))%></TD>
			</TR>        
		</TABLE>
<%	If IsNothing(Request.QueryString.Item("nCurrency")) Then
		Response.Write("<script>ReloadPage(self.document.forms[0].cbeCurrency);</script>")
	End If
End If
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreTR003()
Else
	Call insPreTR003Upd()
End If


mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("TR003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









