<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mclsQueryClients As eClient.QueryClients


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePerson_typColumnCaption"), "cbePerson_typ", "Table5006", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "InsOptionValue();",  ,  , GetLocalResourceObject("cbePerson_typColumnToolTip"))
			'+No se usa control de cliente para mejorar rendimiento de transacción
			Call .AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 40, "",  , GetLocalResourceObject("tctClientColumnToolTip"))
			Call .AddDateColumn(40342, GetLocalResourceObject("tcdBirthdatColumnCaption"), "tcdBirthdat", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdBirthdatColumnToolTip"))
			Call .AddPossiblesColumn(40339, GetLocalResourceObject("cboSexclienColumnCaption"), "cboSexclien", "Table18", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboSexclienColumnToolTip"))
			Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddressColumnCaption"), "cmdAddress", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddressColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePerson_typColumnCaption"), "cbePerson_typ", "Table5006", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "InsOptionValue();",  ,  , GetLocalResourceObject("cbePerson_typColumnToolTip"))
			Call .AddTextColumn(40340, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 14, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  , "InsPutCero(this);")
			Call .AddTextColumn(0, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 19, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctClienameColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctLastnameColumnCaption"), "tctLastname", 19, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctLastnameColumnCaption"))
			Call .AddTextColumn(40341, GetLocalResourceObject("tctLastname2ColumnCaption"), "tctLastname2", 19, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctLastname2ColumnToolTip"))
			Call .AddDateColumn(40342, GetLocalResourceObject("tcdBirthdatColumnCaption"), "tcdBirthdat", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdBirthdatColumnToolTip"))
			Call .AddPossiblesColumn(40339, GetLocalResourceObject("cboSexclienColumnCaption"), "cboSexclien", "Table18", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboSexclienColumnToolTip"))
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BCC001"
		.Codisp = "BCC001_K"
		.AddButton = False
		.DeleteButton = False
		.Top = 10
		.Left = 10
		.Width = 350
		.Height = 400
		.bOnlyForQuery = True
		.bCheckVisible = False
		.Columns("Sel").GridVisible = False
		.Columns("cbePerson_typ").BlankPosition = 0
	End With
End Sub

'% insPreBCC001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreBCC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsAddress As Object
	Dim lcolAddress As Object
	Dim lclsQueryClient As Object
	Dim lstrAlert As Object
	Dim ldblTotal As Double
	Dim lintIndex As Short
	
	With Response
		.Write(mobjValues.HiddenControl("hddFirstRecord", Request.QueryString.Item("nFirstRecord")))
		.Write(mobjValues.HiddenControl("hddQueryString", Request.Params.Get("Query_String")))
		.Write(mobjValues.HiddenControl("hddTotalRecord", CStr(0)))
	End With
	
	ldblTotal = 0
	If Not IsNothing(Request.QueryString.Item("tctClient")) Or Not IsNothing(Request.QueryString.Item("tctCliename")) Or Not IsNothing(Request.QueryString.Item("tcdBirthdat")) Or Not IsNothing(Request.QueryString.Item("cboSexclien")) Or Not IsNothing(Request.QueryString.Item("cbePerson_typ")) Then
		lintIndex = 0
		If mclsQueryClients.FindCondition(Request.QueryString.Item("tctClient"), Request.QueryString.Item("tctCliename"), Request.QueryString.Item("tctLastName"), Request.QueryString.Item("tctLastName2"), Request.QueryString.Item("tcdBirthdat"), Request.QueryString.Item("cboSexclien"), mobjValues.StringToType(Request.QueryString.Item("cbePerson_typ"), eFunctions.Values.eTypeData.etdDouble),  ,  , CInt(Request.QueryString.Item("nFirstRecord")), CInt(Request.QueryString.Item("nLastRecord")), 1) Then
			For	Each lclsQueryClient In mclsQueryClients
				With mobjGrid
					.Columns("tctClient").DefValue = lclsQueryClient.sClient & " - " & lclsQueryClient.sCliename
					.Columns("tcdBirthdat").DefValue = lclsQueryClient.dBirthdat
					.Columns("cboSexclien").DefValue = mobjValues.StringToType(lclsQueryClient.sSexclien, eFunctions.Values.eTypeData.etdDouble)
					.Columns("cbePerson_typ").DefValue = Request.QueryString.Item("cbePerson_typ")
					.Columns("cmdAddress").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&nMainAction=401&sClient=" & lclsQueryClient.sClient & "','ShowAddress',500,400,'yes','yes',100,100)"
					Response.Write(mobjGrid.DoRow())
					lintIndex = lintIndex + 1
				End With
			Next lclsQueryClient
			lclsQueryClient = Nothing
			ldblTotal = mclsQueryClients.nRecordCount
		End If
	End If
	With Response
		.Write(mobjGrid.closeTable())
		.Write("<SCRIPT>self.document.forms[0].hddTotalRecord.value=" & ldblTotal & "</" & "Script>")
		.Write(mobjValues.BeginPageButton)
	End With
End Sub

'% insPreBCC001Upd: Se cargan los controles de la página, para evaluar la condición de búsqueda
'--------------------------------------------------------------------------------------------
Private Sub insPreBCC001Upd()
	'--------------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valClient.aspx", "BCC001", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
End Sub

</script>
<%Response.Expires = 0

mclsQueryClients = New eClient.QueryClients
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\client\client\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $|$$Author: Nvaplat60 $"

//% insCancel: se controla la acción Cancelar de la ventana
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insCancel: se controla el estado de los campos de la ventana
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
	EditRecord(-1, nMainAction,'Add')
}

//%	InsOptionValue: habilita y deshabilita campos de la busqueda de cliente.
//-------------------------------------------------------------------------------------------
function InsOptionValue() {
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (cbePerson_typ.value==1){
			tctClient.disabled    = false;
			tctCliename.disabled  = false;
			tctLastname.disabled  = false;
			tctLastname2.disabled = false;
			tcdBirthdat.disabled  = false;
			cboSexclien.disabled  = false;
//+ Limpia los valores ya ingresados				
			tctClient.value    = '';
			tctCliename.value  = '';
			tctLastname.value  = '';
			tctLastname2.value = '';
			tcdBirthdat.value  = '';
			cboSexclien.value  = '';
		}		
        else{
			tctClient.disabled    = false;
			tctCliename.disabled  = false;
			tctLastname.disabled  = true;
			tctLastname2.disabled = true;
			tcdBirthdat.disabled  = false;
			cboSexclien.disabled  = true;
//+ Limpia los valores ya ingresados				
			tctClient.value    = '';
			tctCliename.value  = '';
			tctLastname.value  = '';
			tctLastname2.value = '';
			tcdBirthdat.value  = '';
			cboSexclien.value  = '';
		}
    }
}

//%	InsPutCero: Llena con cero el codigo del cliente
//-------------------------------------------------------------------------------------------
function InsPutCero(sCodClient) {
//-------------------------------------------------------------------------------------------		
	if (sCodClient.value!='')
		self.document.forms[0].tctClient.value = InsValuesCero(sCodClient);
}

</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("BCC001"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "BCC001_K.aspx"))
		.Write(mobjMenu.MakeMenu("BCC001", "BCC001_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSearchCli" ACTION="valClient.aspx?sMode=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("BCC001"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreBCC001()
Else
	Call insPreBCC001Upd()
End If
%>
</FORM>
</BODY>
</HTML>




