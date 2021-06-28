<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga de datos del Grid de la ventana		
    Dim mclsLife_load As eProduct.Lend_Agree_Pres
Dim mcolLife_loads As Object
Dim mcolProduct As eProduct.Lend_Agree_Press

'+ Cambios en la lógica de descuento de los costos coberturas. 
Dim mstrExist_Modul As Object

Dim lclsProduct As eProduct.Product
Dim lblnModulec As Boolean
Dim lintIndex As Byte


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeCod_AgreeColumnCaption"), "cbeCod_Agree", "TABPRESCONV_PROD", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCod_AgreeColumnToolTip"))
		
		If CStr(Session("sPolitype")) = "1" Then
			.AddPossiblesColumn(0, GetLocalResourceObject("valGroupColumnCaption"), "valGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valGroupColumnToolTip"))
		Else
			.AddPossiblesColumn(0, GetLocalResourceObject("valGroupColumnCaption"), "valGroup", "tabgroups_coll", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valGroupColumnToolTip"))
		End If
		
		If lblnModulec Then
			.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valModulecColumnToolTip"))
		Else
			.AddHiddenColumn("valModulec", "")
		End If
		.AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCoverColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnPay_ConcepColumnCaption"), "tcnPay_Concep", "Table160", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnPay_ConcepColumnToolTip"))
	End With
	
	With mobjGrid
		.Codispl = "CA100"
		.Codisp = "CA100"
		.Top = 150
		.Left = 100
		.Width = 500
		.Height = 300
		.WidthDelete = 500
		
		.bCheckVisible = Request.QueryString.Item("Action") <> "Add"
		.Columns("cbeCod_Agree").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeCod_Agree").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeCod_Agree").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valGroup").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valGroup").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valGroup").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valGroup").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If CStr(Session("sPolitype")) <> "1" Then
			.Columns("valGroup").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valGroup").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		
		.Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("Sel").GridVisible = Not Session("bQuery")
		
		'+ se desabilita combo de prestaciones
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("tcnPay_Concep").disabled = True
		End If
		
		.sEditRecordParam = "ncover=" & Request.QueryString.Item("ncover") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nprestac=" & Request.QueryString.Item("nprestac") & "&ncod_Agree=" & Request.QueryString.Item("ncod_Agree") & "&nGroup=" & Request.QueryString.Item("nGroup")
		
		.sDelRecordParam = "nModulec='+ marrArray[lintIndex].valModulec + '" & "&nCover='+ marrArray[lintIndex].valCover + '" & "&nprestac='+ marrArray[lintIndex].tcnPay_Concep + '" & "&ncod_agree='+ marrArray[lintIndex].cbeCod_Agree + '" & "&nGroup='+ marrArray[lintIndex].valGroup + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP080: Obtiene los cargos de los aportes
'-----------------------------------------------------------------------------
Private Sub insPreCA100()
	'-----------------------------------------------------------------------------                                   
	If mcolProduct.FindLend_agree_Pres(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("dEffecdate")) Then
		
		If mcolProduct.Count > 0 Then
			mobjGrid.DeleteButton = True
                For Each Me.mclsLife_load In mcolProduct
                    With mobjGrid
                        .Columns("tcnPay_Concep").DefValue = mclsLife_load.nprestac
                        .Columns("cbeCod_Agree").DefValue = mclsLife_load.nCod_agree
                        .Columns("valCover").DefValue = mclsLife_load.nCover
                        .Columns("valModulec").DefValue = mclsLife_load.nModulec
                        .Columns("valGroup").DefValue = mclsLife_load.nGroup
                    End With
                    Response.Write(mobjGrid.DoRow())
                Next mclsLife_load
		End If
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP080Upd: Realiza la eliminación de cargos
'-----------------------------------------------------------------------------
Private Sub insPreCA100Upd()
	'-----------------------------------------------------------------------------
	'- Objeto para manejo de los cargos de contribuciones
	Dim mclsLife_load As eProduct.Lend_Agree_Pres
	
	Dim lblnPost As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		mclsLife_load = New eProduct.Lend_Agree_Pres
		'+ Muestra el mensaje para eliminar registros
		Response.Write(mobjValues.ConfirmDelete())
		
		Call mclsLife_load.insPostCA100(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("ncover"), eFunctions.Values.eTypeData.etdDouble), Session("npolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nprestac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("ncod_agree"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUsercode"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdLong))
	End If
	mclsLife_load = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valPolicyseq.aspx", "CA100", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
    mclsLife_load = New eProduct.Lend_Agree_Pres
mcolProduct = New eProduct.Lend_Agree_Press
mobjGrid = New eFunctions.Grid

mobjGrid.sCodisplPage = "CA100"
mobjValues.sCodisplPage = "CA100"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE=javascript>
//+ Esta línea guarda la version procedente de VSS
    document.VssVersion="$$Revision: 3 $|$$Date: 13/02/06 11:28 $"

//% Cambios en la lógica de descuento de los costos coberturas. 
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
//+ Se recarga la página para que muestre las coberturas del módulo seleccionado
//----------------------------------------------------------------------------------------------------------------------
function insChangeKey(){
//----------------------------------------------------------------------------------------------------------------------
    var lstrstring = '';
    var nModulec = 0
    var nCover = 0
    var nprestac = 0
    var ncod_Agree = 0
    if (typeof(self.document.forms[0].cbeModule) != 'undefined')
        nModulec = self.document.forms[0].cbeModule.value
    nCover = self.document.forms[0].cbeCover.value
    if (nModulec != '<%=Request.QueryString.Item("nModulec")%>' ||
        nCover != '<%=Request.QueryString.Item("nCover")%>'){
        lstrstring += document.location;        
        lstrstring = lstrstring.replace(/&nModulec=.*/, "");
        lstrstring = lstrstring.replace(/&nCover=.*/, "");
        lstrstring = lstrstring.replace(/&nprestac=.*/, "");
        lstrstring = lstrstring.replace(/&ncod_Agree=.*/, "");
        lstrstring = lstrstring.replace(/Reload=.*/, "");
        lstrstring = lstrstring + "&nModulec=" + nModulec + "&nCover=" + nCover + "&nprestac=" + nprestac + "&ncod_Agree=" + ncod_Agree ;        
        document.location.href = lstrstring;
    }
}
</SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CA100"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CA100", "CA100.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCA100" ACTION="valPolicyseq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("CA100"))

lclsProduct = New eProduct.Product

lintIndex = 0

'+ Si tiene módulos asociados
lblnModulec = lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))

%>        
    <BR>
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA100()
Else
	Call insPreCA100Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsLife_load = Nothing
mcolLife_loads = Nothing
%> 
</FORM>
</BODY>
</HTML>




