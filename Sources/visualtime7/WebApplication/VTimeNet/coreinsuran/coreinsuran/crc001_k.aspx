<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas    
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga de datos de la forma    
Dim mclsCompany As eCoReinsuran.Company

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'dim eRemoteDB.Constants.intNull As String
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "crc001_k"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100663, GetLocalResourceObject("tcnCompanyColumnCaption"), "tcnCompany", 6, eRemoteDB.Constants.intNull,  , GetLocalResourceObject("tcnCompanyColumnToolTip"))
		Call .AddTextColumn(100664, GetLocalResourceObject("tctCompanyNameColumnCaption"), "tctCompanyName", 30, "",  , GetLocalResourceObject("tctCompanyNameColumnToolTip"))
		Call .AddPossiblesColumn(100663, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table219", eFunctions.Values.eValuesType.clngComboType)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddressColumnCaption"), "cmdAddress", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddressColumnToolTip"))
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CRC001"
		.Codisp = "CRC001_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 210
		.Width = 420
		.Top = 10
		.Left = 10
		.bOnlyForQuery = True
		.bCheckVisible = False
	End With
End Sub

'% insPreCRC001: Se cargan los controles y los registros existentes de la página 
'--------------------------------------------------------------------------------------------
Private Sub insPreCRC001()
	'--------------------------------------------------------------------------------------------
	
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	Dim strType As String
	
	If Not IsNothing(Request.QueryString.Item("nCompany")) Or Not IsNothing(Request.QueryString.Item("sCliename")) Or Not IsNothing(Request.QueryString.Item("sType")) Then
		
		If CDbl(Request.QueryString.Item("sType")) = 0 Then
			strType = ""
		Else
			strType = Request.QueryString.Item("sType")
		End If
		
		lblnFind = mclsCompany.insPreparedQuery(Request.QueryString.Item("nCompany"), Request.QueryString.Item("sCliename"), strType)
		
		
		If lblnFind Then
			lintCount = 0
			For lintCount = 0 To mclsCompany.Count - 1
				If mclsCompany.ItemCompany(lintCount) Then
					With mobjGrid
						.Columns("tcnCompany").DefValue = CStr(mclsCompany.nCompany)
						.Columns("tctCompanyName").DefValue = mclsCompany.sCliename
						.Columns("cbeType").DefValue = mclsCompany.sType
						.Columns("cmdAddress").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&sRectype=1&sClient=" & mclsCompany.sClient & "','ShowAddress',500,500,'yes','yes','no','no')"
					End With
					Response.Write(mobjGrid.DoRow())
				End If
			Next 
		End If
	End If
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            		
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCRC001Upd. Se define esta funcion para contruir el contenido de la ventana UPD
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCRC001Upd()
	'--------------------------------------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CRC001", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsCompany = New eCoReinsuran.Company
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "crc001_k"
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
		.Write(mobjMenu.setZone(1, "CRC001", "CRC001_k.aspx"))
		.Write(mobjMenu.MakeMenu("CRC001", "CRC001_k.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
		
	End If
End With
mobjMenu = Nothing

%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQCompanies" ACTION="valCoReinsuran.aspx?nZone=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("CRC001"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreCRC001()
Else
	Call insPreCRC001Upd()
End If
%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"     
</SCRIPT>
</FORM>
</BODY>
</HTML>





