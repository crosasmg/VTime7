<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mclsSaapv_transfer As eSaapv.Saapv_Transfer


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim mclspolicy As Object
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "VI7501_F"
	
	mclsSaapv_transfer = New eSaapv.Saapv_Transfer
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		Call .AddNumericColumn(0, "Folio", "tcnCod_saapv", 10, CStr(Session("nCod_saapv")),  , "Número de folio",  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, "Tipo de Ahorro", "cbeFunds", "table5633", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , Request.QueryString("Action") <> "Add",  , "Tipo de ahorro")
		Call .AddPossiblesColumn(0, "Régimen tributario", "cbeTax_regime", "table950", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , Request.QueryString("Action") <> "Add",  , "Régimen tributario")
		Call .AddPossiblesColumn(0, "Fondo de origen", "cbeAfp_type", "table5745", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , "Fondo de origen de la AFP", eFunctions.Values.eTypeCode.eString)
		Call .AddCheckColumn(0, "Traspaso Total", "chkType", "",  ,  ,  ,  , "Permite indicar si el traspaso es total")
		Call .AddNumericColumn(0, "Saldo a traspasar(Pesos)", "tcnSaving_Loc", 18, CStr(0),  , "Monto de ahorro a traspasar expresado en pesos.", True, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, "Saldo a traspasar(UF)", "tcnSaving_UF", 18, CStr(0),  , "Monto de ahorro a traspasar expresado en Unidad de Fomento.", True, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, "Saldo a traspasar(%)", "tcnSaving_PCT", 18, CStr(0),  , "Monto de ahorro a traspasar expresado en porcentaje.", True, 6,  ,  ,  , False)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI7501_F"
		.ActionQuery = Session("bQuery")
		.ActionQuery = Request.QueryString("nMainAction") = 401
		If CStr(Session("nType_saapv")) <> "4" And CStr(Session("nType_saapv")) <> "5" Then
			.ActionQuery = True
		End If
		
		.Columns("cbeFunds").EditRecord = True
		.Top = 100
		.bCheckVisible = True
		.UpdContent = True
		.WidthDelete = 280
		
		'+Se definen el ancho y Alto
		.Width = 400
		.Height = 330
		.nMainAction = Request.QueryString("nMainAction")
		.Columns("Sel").GridVisible = Not .ActionQuery
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		
		If CStr(Session("nType_saapv")) = "5" Then
			.sEditRecordParam = "nInstitut_origin='+ self.document.forms[0].valInstitution.value +'"
		End If
		
		.sDelRecordParam = "sCertype=" & Session("sCertype_saapv") & "&nBranch=" & Session("nBranch_saapv") & "&nProduct=" & Session("nProduct_saapv") & "&nPolicy=" & Session("nPolicy_saapv") & "&nCertif=" & Session("nCertif_saapv") & "&nCod_saapv=" & Session("nCod_saapv") & "&nInstitution=" & Session("nInstitution") & "&nFunds='+ marrArray[lintIndex].cbeFunds +'" & "&nTax_regime='+ marrArray[lintIndex].cbeTax_regime +'"
	End With
End Sub

'% reaSaapv: Obtiene la institución origen
'-----------------------------------------------------------------------------
Private Function reaSaapv() As Integer
	'-----------------------------------------------------------------------------
	Dim lclsSaapv As eSaapv.Saapv
	lclsSaapv = New eSaapv.Saapv
	
	If Request.QueryString("nInstitut_origin") <> vbNullString Then
		reaSaapv = Request.QueryString("nInstitut_origin")
	Else
		If lclsSaapv.Find(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong)) Then
			reaSaapv = lclsSaapv.nInstitut_origin
		End If
	End If
	'UPGRADE_NOTE: Object lclsSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsSaapv = Nothing
End Function

'% insPreVI7501_F: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7501_F()
	'--------------------------------------------------------------------------------------------
	If CStr(Session("nType_saapv")) = "5" Then
		
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>Institución origen</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.PossiblesValues("valInstitution", "TabTab_Fn_Institu", eFunctions.Values.eValuesType.clngWindowType, CStr(reaSaapv()),  ,  ,  ,  ,  ,  ,  ,  , "Institución origen"))


Response.Write("</TD>            " & vbCrLf)
Response.Write("            <TD WIDTH=""60%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>        " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write(" ")

		
	End If
	
	Dim mcolSaapv_transfers As eSaapv.Saapv_Transfers
	Dim lintIndex As Byte
	
	mcolSaapv_transfers = New eSaapv.Saapv_Transfers
	
	If mcolSaapv_transfers.Find(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong)) Then
		With mobjGrid
			For	Each mclsSaapv_transfer In mcolSaapv_transfers
				' + Se asignan los valores de las columnas del grid 
				.Columns("tcnCod_saapv").DefValue = CStr(mclsSaapv_transfer.nCod_saapv)
				.Columns("cbeFunds").DefValue = CStr(mclsSaapv_transfer.nFunds_origin)
				.Columns("cbeTax_regime").DefValue = CStr(mclsSaapv_transfer.nTax_regime)
				.Columns("cbeAfp_type").DefValue = mclsSaapv_transfer.sAfp_type
				.Columns("chkType").Checked = mclsSaapv_transfer.nType_transfer
				.Columns("tcnSaving_Loc").DefValue = CStr(mclsSaapv_transfer.nSaving_Loc)
				.Columns("tcnSaving_UF").DefValue = CStr(mclsSaapv_transfer.nSaving_UF)
				.Columns("tcnSaving_PCT").DefValue = CStr(mclsSaapv_transfer.nSaving_PCT)
				
				Response.Write(.DoRow)
				
				lintIndex = lintIndex + 1
			Next mclsSaapv_transfer
		End With
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	'UPGRADE_NOTE: Object mcolSaapv_transfers may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mcolSaapv_transfers = Nothing
End Sub

'% insPreVI7501_F_Upd: se realiza el manejo de la PopUp
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7501_F_Upd()
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	Dim lclsSaapv_transfer As eSaapv.Saapv_Transfer
	With Request
		If .QueryString("Action") = "Del" Then
			lclsSaapv_transfer = New eSaapv.Saapv_Transfer
			lblnPost = lclsSaapv_transfer.insPostVI7501_F("Del", mobjValues.StringToType(.QueryString("nCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nFunds"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString("nTax_regime"), eFunctions.Values.eTypeData.etdLong), "", 0, 0, 0, 0, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString("nInstitution"), eFunctions.Values.eTypeData.etdLong))
			
			Response.Write(mobjValues.ConfirmDelete)
			'UPGRADE_NOTE: Object lclsSaapv_transfer may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			lclsSaapv_transfer = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valVI7501tra.aspx", Request.QueryString("sCodispl"), Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "VI7501_F"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 18/11/11 13:15 $|$$Author: Ljimenez $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI7501_F", "VI7501_F.aspx"))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%> 
</HEAD> 
<BODY ONUNLOAD="closeWindows();"> 
<FORM METHOD="POST" NAME="VI7501_F" ACTION="valVI7501tra.aspx?nMainAction=301&sMode=1"> 
<%
Response.Write(mobjValues.ShowWindowsName("VI7501_F"))

Call insDefineHeader()

If Request.QueryString("Type") = "PopUp" Then
	Call insPreVI7501_F_Upd()
Else
	Call insPreVI7501_F()
End If

'UPGRADE_NOTE: Object mclsSaapv_transfer may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSaapv_transfer = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





