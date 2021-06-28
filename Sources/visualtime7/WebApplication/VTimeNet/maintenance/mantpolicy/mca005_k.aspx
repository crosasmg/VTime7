<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'----------------------------------------------------------------------------------------
'- Ventana Masiva.  Causas del estado pendiente de la poliza/certificado 
'----------------------------------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcoltab_waitpos As ePolicy.Tab_waitPos


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "mca005_k"
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 5, vbNullString,  , GetLocalResourceObject("tcnOrderColumnToolTip"))
		If Request.QueryString.Item("Action") = "Add" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnWait_CodeColumnCaption"), "tcnWait_Code", 5, vbNullString,  , GetLocalResourceObject("tcnWait_CodeColumnToolTip"),  ,  ,  ,  ,  , False)
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnWait_CodeColumnCaption"), "tcnWait_Code", 5, vbNullString,  , GetLocalResourceObject("tcnWait_CodeColumnToolTip"),  ,  ,  ,  ,  , True)
		End If
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctShort_DesColumnCaption"), "tctShort_Des", 12, vbNullString,  , GetLocalResourceObject("tctShort_DesColumnCaption"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAreaWaitColumnCaption"), "cbeAreaWait", "Table5603", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeAreaWaitColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkConvertColumnCaption"), "chkConvert", "", CShort("2"), "2", "insChangeField(this)", False)
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkConvertColumnCaption"), "chkConvert", "", CShort("2"), "2", "insChangeField(this)", True)
		End If
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "MCA005"
		.Codisp = "MCA005_k"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("tctDescript").EditRecord = True
		End If
		
		.Height = 350
		.Width = 500
		.Top = 100
		.WidthDelete = 500
		
		' parámetros para eliminación
		.sDelRecordParam = "nWait_Code='+ marrArray[lintIndex].tcnWait_Code + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMCA005: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA005()
	'--------------------------------------------------------------------------------------------
	Dim mclstab_waitpo As ePolicy.Tab_waitPo
	mclstab_waitpo = New ePolicy.Tab_waitPo
	mcoltab_waitpos = New ePolicy.Tab_waitPos
	
	If mcoltab_waitpos.Find() Then
		For	Each mclstab_waitpo In mcoltab_waitpos
			With mobjGrid
				.Columns("tcnOrder").DefValue = CStr(mclstab_waitpo.nOrder)
				.Columns("tcnWait_Code").DefValue = CStr(mclstab_waitpo.nwait_code)
				.Columns("tctDescript").DefValue = mclstab_waitpo.sdescript
				.Columns("tctShort_Des").DefValue = mclstab_waitpo.sshort_des
				.Columns("cbeAreaWait").DefValue = CStr(mclstab_waitpo.nAreaWait)
				.Columns("cbeStatregt").DefValue = mclstab_waitpo.sStatregt
				.Columns("chkConvert").DefValue = mclstab_waitpo.sConvert
				If mclstab_waitpo.sConvert = "1" Then
					.Columns("chkConvert").Checked = CShort("1")
				Else
					.Columns("chkConvert").Checked = CShort("2")
				End If
				Response.Write(.DoRow)
			End With
		Next mclstab_waitpo
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'*++ Modificar nombre de la función. Modificar "MCA005" por el código lógico de la transacción
'% insPreMCA005Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA005Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjTab_Waitpo As ePolicy.Tab_waitPo
	
	lobjTab_Waitpo = New ePolicy.Tab_waitPo
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTab_Waitpo.insPostMCA005(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nWait_Code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_Des"), mobjValues.StringToType(Request.Form.Item("cbeAreaWait"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkConvert")) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantPolicy.aspx", "MCA005", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "mca005_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:15 $|$$Author: Nvaplat61 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insPreZone: Define ubicacion de documento
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//%insChangeField: Función que maneja los estados de los controles
//------------------------------------------------------------------------------------------------------
function insChangeField(Field){
//------------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
         if (chkConvert.checked)
			chkConvert.value='1';
		 else
		 	chkConvert.value='2';
	}
}

</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCA005_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MCA005" ACTION="valMantPolicy.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCA005Upd()
Else
	Call insPreMCA005()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





