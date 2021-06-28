<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas    
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga de datos de la forma    
Dim mclsContrmaster As eCoReinsuran.Contrmaster

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'dim eRemoteDB.Constants.intNull As String
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "crc003_k"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnNumberColumnCaption"), "tcnNumber", 6, eRemoteDB.Constants.intNull,  , GetLocalResourceObject("tcnNumberColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboTypeColumnCaption"), "cboType", "Table173", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboBranchColumnCaption"), "cboBranch", "Table5000", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboCurrencyColumnCaption"), "cboCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		Call .AddDateColumn(0, GetLocalResourceObject("dStartdateColumnCaption"), "dStartdate", Session("dEffecdate"),  , GetLocalResourceObject("dStartdateColumnToolTip"))
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		End If
		Call .AddHiddenColumn("tcnType_rel", eRemoteDB.Constants.intNull)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdCompaniesColumnCaption"), "cmdCompanies", "/VTimeNet/images/Companies.gif", GetLocalResourceObject("cmdCompaniesColumnToolTip"))
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CRC003"
		.Codisp = "CRC003_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 250
		.Width = 500
		.Top = 10
		.Left = 10
		.bOnlyForQuery = True
		.bCheckVisible = False
	End With
End Sub

'% insPreCRC003: Se cargan los controles y los registros existentes de la página 
'--------------------------------------------------------------------------------------------
Private Sub insPreCRC003()
	'--------------------------------------------------------------------------------------------
	
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	Dim strType As String
	Dim strBranch As String
	Dim strCurrency As String
	
	If IsNothing(Request.QueryString.Item("nNumber")) And IsNothing(Request.QueryString.Item("nType")) And IsNothing(Request.QueryString.Item("nBranch")) And IsNothing(Request.QueryString.Item("nCurrency")) And IsNothing(Request.QueryString.Item("nYear_contr")) Then
		
		lblnFind = mclsContrmaster.FindTreaties(vbNullString)
		
	Else
		
		If CDbl(Request.QueryString.Item("nType")) = 0 Then
			strType = vbNullString
		Else
			strType = Request.QueryString.Item("nType")
		End If
		If CDbl(Request.QueryString.Item("nBranch")) = 0 Then
			strBranch = vbNullString
		Else
			strBranch = Request.QueryString.Item("nBranch")
		End If
		If CDbl(Request.QueryString.Item("nCurrency")) = 0 Then
			strCurrency = vbNullString
		Else
			strCurrency = Request.QueryString.Item("nCurrency")
		End If
		
		lblnFind = mclsContrmaster.insPreparedQuery(Request.QueryString.Item("nNumber"), strType, strBranch, strCurrency, mobjValues.StringToType(Request.QueryString.Item("dStartdate"), eFunctions.Values.eTypeData.etdDate))
	End If
	
	If lblnFind Then
		lintCount = 0
		For lintCount = 0 To mclsContrmaster.Count - 1
			If mclsContrmaster.ItemContrmaster(lintCount) Then
				With mobjGrid
					.Columns("tcnNumber").DefValue = CStr(mclsContrmaster.nNumber)
					.Columns("cboType").DefValue = CStr(mclsContrmaster.nType)
					.Columns("cboBranch").DefValue = CStr(mclsContrmaster.nBranch)
					.Columns("cboCurrency").DefValue = CStr(mclsContrmaster.nCurrency)
					.Columns("dStartdate").DefValue = CStr(mclsContrmaster.dStartdate)
					.Columns("tcnAmount").DefValue = CStr(mclsContrmaster.nAmount)
					.Columns("tcnType_rel").DefValue = CStr(mclsContrmaster.nType_rel)
					
					'+Si el contrato es diferente de retencion se muestra la informacion
					If mclsContrmaster.nType <> 1 Then
						.Columns("cmdCompanies").HRefScript = "ShowPopUp('/VTimeNet/CoReinsuran/CoReinsuran/Companies.aspx?nNumber=" & mclsContrmaster.nNumber & "&nType=" & mclsContrmaster.nType & "&dStartdate=" & mclsContrmaster.dStartdate & "&nBranch=" & mclsContrmaster.nBranch & "&dEffecdate=" & Today & "&nType_rel=" & mclsContrmaster.nType_rel & "','ShowCompanies',350,300,'yes','no','no','no')"
					Else
						.Columns("cmdCompanies").HRefScript = ""
					End If
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	End If
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            		
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCRC003Upd. Se define esta funcion para contruir el contenido de la ventana UPD
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCRC003Upd()
	'--------------------------------------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CRC003", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsContrmaster = New eCoReinsuran.Contrmaster
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "crc003_k"
%>	    



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<%If Request.QueryString.Item("Type") <> "PopUp" Then%>

	<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\coreinsuran\coreinsuran\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	
<%End If%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
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
		EditRecord(-1, nMainAction,'Add')
}
</SCRIPT>
</HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("CRC001"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(1, "CRC003", "CRC003_k.aspx"))
		.Write(mobjMenu.MakeMenu("CRC003", "CRC003_k.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
		
	End If
End With
mobjMenu = Nothing

%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQCompanies" ACTION="valCoReinsuran.aspx?x=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("CRC003"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreCRC003()
Else
	Call insPreCRC003Upd()
End If
%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 2 $|$$Date: 13/09/03 17:30 $"     
</SCRIPT>
</FORM>
</BODY>
</HTML>




