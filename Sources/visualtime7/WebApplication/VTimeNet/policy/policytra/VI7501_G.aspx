<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mintOrigin As String



'**% insDefineHeader: The field of the GRID is defined.
'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "VI7501_G"
	
	'**+ The column of the GRID are defined.
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddTextColumn(100745, "Fondo", "tctDescript", 30, vbNullString,  , "Descripción de los fondos permitidos para el producto",  ,  ,  , True)
		Call .AddNumericColumn(103509, "%Mínimo", "tcnPartic_min", 4, CStr(0),  , "Mínimo porcentaje de participación en el fondo", True, 2,  ,  ,  , True)
		Call .AddNumericColumn(100744, "Participación", "tcnParticip", 5, CStr(0),  , "Porcentaje de participación en el fondo",  , 2)
		
		Call .AddTextColumn(100746, "Cuenta", "tctnOrigin", 30, vbNullString,  , "Descripción cuenta",  ,  ,  , True)
		
		Call .AddHiddenColumn("tcnFunds", CStr(0))
		Call .AddHiddenColumn("tcnOrigin", CStr(0))
		Call .AddHiddenColumn("tcdNulldate", "")
		Call .AddHiddenColumn("hddnQuan_avail", CStr(0))
		Call .AddHiddenColumn("hddsActivefound", "")
		Call .AddHiddenColumn("hddnIntproy", CStr(0))
		Call .AddHiddenColumn("hddnIntproyvar", CStr(0))
        Call .AddHiddenColumn("chkGuarant", "2")
		Call .AddHiddenColumn("hddSel", "")
	End With
	
	'**+ The properties of the GRID are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = Request.QueryString("sCodispl")
		.Height = 260
		.Width = 330
		.Columns("Sel").Title = "Sel"
		.AddButton = False
		.DeleteButton = False
		.Columns("tctnOrigin").GridVisible = False
		.Columns("tctnOrigin").PopUpVisible = False
		
		If CBool(Session("bQuery")) <> True Then
			.Columns("tctDescript").EditRecord = True
		Else
			.Columns("Sel").Disabled = True
		End If
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		
	End With
End Sub

'**% insPreVI7501_G: Read the information of the policy funds.
'% insPreVI7501_G: Obtiene los datos de los fondos de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7501_G()
	'--------------------------------------------------------------------------------------------
	Dim lclsFunds As eSaapv.Saapv_funds_pol
	Dim lcolFundss As eSaapv.Saapv_funds_pols
	Dim lintOrigin As Object
	Dim lintSelected As Byte
	Dim lintCount As Short
	
	lclsFunds = New eSaapv.Saapv_funds_pol
	lcolFundss = New eSaapv.Saapv_funds_pols
	
	lintOrigin = 1
	
	If lcolFundss.Find(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintOrigin, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("dEffecdate_saapv")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nBranch_saapv")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nProduct_saapv")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nPolicy_saapv")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nCertif_saapv")), eFunctions.Values.eTypeData.etdLong), "2", mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong)) Then
		lintCount = 0
		For	Each lclsFunds In lcolFundss
			With mobjGrid
				If lclsFunds.sSel = "1" Then
					lintSelected = 1
					.Columns("hddSel").DefValue = "1"
				Else
					lintSelected = 2
					.Columns("hddSel").DefValue = ""
				End If
				.Columns("Sel").Checked = lintSelected
				.Columns("tctDescript").DefValue = lclsFunds.sDescript
				.Columns("tcnPartic_min").DefValue = CStr(lclsFunds.nPartic_min)
				.Columns("tcnParticip").DefValue = CStr(lclsFunds.nParticip)
				.Columns("tcnFunds").DefValue = CStr(lclsFunds.nFunds)
				.Columns("tcnOrigin").DefValue = CStr(lclsFunds.nOrigin)
				.Columns("tctnOrigin").DefValue = lclsFunds.sDesOrigin
				.Columns("tcdNulldate").DefValue = CStr(lclsFunds.dNulldate)
				
				.Columns("Sel").OnClick = "insSelected(this," & lintCount & ")"
				
				.sEditRecordParam = "sSel=1"
				
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		Next lclsFunds
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	'UPGRADE_NOTE: Object lcolFundss may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolFundss = Nothing
	'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsFunds = Nothing
End Sub

'%** insPreVI7501_GUpd: Show the pop up windows for the updates.
'% insPreVI7501_GUpd: Muestra la ventana Popup para las actualizaciones.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7501_GUpd()
	'--------------------------------------------------------------------------------------------
	Dim lclsSaapv_funds_Pol As eSaapv.Saapv_funds_pol
	
	lclsSaapv_funds_Pol = New eSaapv.Saapv_funds_pol
	
	With Request
		If .QueryString("Action") = "Del" Then
			
			
			Response.Write(mobjValues.ConfirmDelete())
			
			Call lclsSaapv_funds_Pol.insPostVI7501_G(.QueryString("Action"), mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString("nFunds"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.QueryString("nOrigin"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("dEffecdate_saapv")), eFunctions.Values.eTypeData.etdDate), "", mobjValues.StringToType(CStr(Session("nBranch_saapv")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nProduct_saapv")), eFunctions.Values.eTypeData.etdLong), 0, 0, 0, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), 0, vbNullString, 0, 0, vbNullString, mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valVI7501tra.aspx", Request.QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
	
	'UPGRADE_NOTE: Object lclsSaapv_funds_Pol may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsSaapv_funds_Pol = Nothing
End Sub

</script>
<%Response.Expires = 0

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
End With

mobjValues.ActionQuery = Session("bQuery")

If CStr(Session("nType_saapv")) = "6" Then
	mintOrigin = "3"
Else
	mintOrigin = "2"
End If
%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'Vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'Vtime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->


<SCRIPT>    
//**+ For the Source Safe control. 
//+ Para Control de Versiones. 

	document.VssVersion="$$Revision: 1 $|$$Date: 24/11/11 19:03 $"
	
</SCRIPT>

    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString("sCodispl")))
	
	If Request.QueryString("Type") <> "PopUp" Then
		'.Write  mobjValues.ShowWindowsName(Request.QueryString("sCodispl"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), "VI7501_G.aspx"))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
End With%>
<SCRIPT>

//**% insCheckSelClick: Show the Pop Up windows.
//% insCheckSelClick: Permite levantar la ventana Popup para actualizar el registro.
//-------------------------------------------------------------------------------------------
function insSelected(Field, Index){
//-------------------------------------------------------------------------------------------
	
	if(Field.checked)
		EditRecord(Field.value,nMainAction, 'Update', "sSel=1")
    else
        EditRecord(Field.value,nMainAction, 'Del',"sSel=2" + "&nFunds=" + marrArray[Field.value].tcnFunds + "&nParticip=" + marrArray[Field.value].tcnParticip + "&nPartic_min=" + marrArray[Field.value].tcnPartic_min + "&nOrigin=" + marrArray[Field.value].tcnOrigin + "&sGuarantee=" + "2")
    Field.checked = !Field.checked
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI7501_G" ACTION="valVI7501tra.aspx?nMainAction=301&sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VI7501_G"))

Call insDefineHeader()

If Request.QueryString("Type") <> "PopUp" Then
	Call insPreVI7501_G()
Else
	Call insPreVI7501_GUpd()
End If
%>
</FORM>
</HTML>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>




