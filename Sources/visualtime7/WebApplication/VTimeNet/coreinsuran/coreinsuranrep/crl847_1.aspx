<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objetos genéricos para manejo de valores, menú y grilla.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
Dim mstrExist_Modul As Object


'%insDefineHeader: Definición de las columnas del Grid.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddTextColumn(100010, GetLocalResourceObject("tcsCod_cumuloColumnCaption"), "tcsCod_cumulo", 14, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcsCod_cumuloColumnToolTip")) ', , , , , , Request.QueryString("Type") = "PopUp" And Request.QueryString("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnVal_Max_CesColumnCaption"), "tcnVal_Max_Ces", 18,  ,  , GetLocalResourceObject("tcnVal_Max_CesColumnCaption"),  , 6)
	End With
	
	With mobjGrid
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		.Codispl = "CRL847_1"
		.Codisp = "CRL847_1"
		.sCodisplPage = "CRL847_1"
		.AddButton = True
		.DeleteButton = True
		.Height = 180 '200
		.Width = 340
		.sDelRecordParam = "sCod_cumulo='+ marrArray[lintIndex].tcsCod_cumulo + '" & "&nVal_max_ces_uf='+ marrArray[lintIndex].tcnVal_Max_Ces + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub


'% insPreCRL847_1: Muestra la grilla con datos.
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCRL847_1()
	'--------------------------------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lobjObject As Object
	Dim lcolCRL847_1s As eCoReinsuran.Tmp_Crl847As
	
	lcolCRL847_1s = New eCoReinsuran.Tmp_Crl847As
	
	If lcolCRL847_1s.Find(Session("sKey")) Then
		lintCount = 0
		For	Each lobjObject In lcolCRL847_1s
			With lobjObject
				mobjGrid.Columns("tcsCod_cumulo").DefValue = lobjObject.sCod_cumulo
				mobjGrid.Columns("tcnVal_Max_Ces").DefValue = lobjObject.nVal_max_ces_uf
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 1000 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolCRL847_1s = Nothing
	lobjObject = Nothing
End Sub

'% insPreCRL847_1Upd: Muestra ventana para actualizar registros.
'-----------------------------------------------------------------------------------------
Private Sub insPreCRL847_1Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsTmp_Crl847 As eCoReinsuran.Tmp_Crl847A
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTmp_Crl847 = New eCoReinsuran.Tmp_Crl847A
		
		If lclsTmp_Crl847.insPostCRL847_1("Del", Request.QueryString.Item("sCod_cumulo"), mobjValues.StringToType(Request.QueryString.Item("nVal_max_ces_uf"), eFunctions.Values.eTypeData.etdDouble), Session("sKey")) Then
			
			Response.Write(mobjValues.ConfirmDelete())
		End If
	End If
	lclsTmp_Crl847 = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuranRep.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	
End Sub

</script>
<%Response.Expires = -1

'- Nombre de tabla general.

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "CRL847_1"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>

//- Variable para el control de versiones

    document.VssVersion="$$Revision: 6 $|$$Date: 7/01/04 10:52 $|$$Author: Nvaplat17 $"

//% insCancel: Ejecuta la acción del botón cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone: Habilita o deshabilita los controles.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(vObj, sField){
//--------------------------------------------------------------------------------------------    
	var sValue;
	
	sValue = vObj.value;
	if (vObj.disabled==false) {
	with (self.document.forms[0]){
		switch (sField){
			case 'Module':
				valCover.Parameters.Param4.sValue=sValue;
				break;
		}
	}
	}
	else{
	    vObj.value=0;
	}    
}
//'+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
//% insDisabled: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function insDisabled(vObj){
//--------------------------------------------------------------------------------------------
    var sValue = vObj.value;
    with (self.document.forms[0]){
		switch (sValue){
		    case '1':
		        valCover.disabled=false;
		        btnvalCover.disabled=false;
		        break;
		    default:
		        valCover.disabled=true;
		        btnvalCover.disabled=true;
		        valCover.value='';
		        $(valCover).change();
		}
    }
}
</SCRIPT>
<%Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))

With Response
	.Write(mobjValues.StyleSheet())
	
	.Write("<SCRIPT>var sAction='" & Request.QueryString.Item("Action") & "'</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.setZone(2, "CRL847_1", "CRL847_1"))
		
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MS7000" ACTION="valCoReinsuranRep.aspx?mode=1">
<%

'+ '+[APV2] HAD 1014. TABLA DE COMISIONES ESPECIALES DE VIDA. DBLANCO 10-09-2003
'+ Busqueda de los modulos de un producto

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreCRL847_1()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreCRL847_1Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






