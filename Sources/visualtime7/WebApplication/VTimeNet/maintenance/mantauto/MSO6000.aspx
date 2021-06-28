<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

'- Variable para el manejo de los errores y las advertencias
Dim lstrAlert As String

'- Objeto para el manejo de los errores y las advertencias       
Dim lobjErrors As eGeneral.GeneralFunction


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddPossiblesColumn(9183, "Localidad", "valLocal_Type", "TabTypeLocation", eFunctions.Values.eValuesType.clngWindowType, "", False, , , , "", False, 10, "Indica el código correspondiente a la localida", , , , False)
            Call .AddNumericColumn(9184, "Código inicial", "tcnZipCode_Ini", 10, "", True, "Indica el valor mínimo del rango del código postal", False, 0, , , , False)
            Call .AddNumericColumn(9185, "Codigo final", "tcnZipCode_End", 10, "", False, "Indica el valor máximo del rango del código postal", False, 0, , , , False)
		
            Call .AddHiddenColumn("hddNullDate", "")
            Call .AddHiddenColumn("hddEditRecord", "")
        End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MSO6000"
		.Codisp = "MSO6000"
		.Top = 150
		.Height = 220
		.Width = 360
		.MoveRecordScript = "insDefUpdate()"
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valLocal_Type").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnZipCode_Ini").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nLocal_Type=' + marrArray[lintIndex].valLocal_Type + '" & "&nZipCode_Ini='+ marrArray[lintIndex].tcnZipCode_Ini + '"
		
		.sEditRecordParam = "dEffecDate=" & Request.QueryString.Item("dEffecDate")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMSO6000. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMSO6000()
	'------------------------------------------------------------------------------
	Dim lcolLocateTar_Soats As eBranches.LocateTar_Soats
	Dim lclsLocateTar_Soat As Object
	Dim lintIndex As Short
	lintIndex = 0
	
	With Request
		lcolLocateTar_Soats = New eBranches.LocateTar_Soats
		With mobjGrid
			If lcolLocateTar_Soats.Find(mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
				For	Each lclsLocateTar_Soat In lcolLocateTar_Soats
					.Columns("valLocal_Type").DefValue = lclsLocateTar_Soat.nLocal_Type
					.Columns("tcnZipCode_Ini").DefValue = lclsLocateTar_Soat.nZipCode_Ini
					.Columns("tcnZipCode_End").DefValue = lclsLocateTar_Soat.nZipCode_End
					.Columns("valLocal_Type").Descript = lclsLocateTar_Soat.sDescript
					.Columns("hddNullDate").DefValue = lclsLocateTar_Soat.dNullDate
					.Columns("hddEditRecord").DefValue = lclsLocateTar_Soat.bEditRecord
					.Columns("valLocal_Type").HRefScript = "insTextClick(this," & CStr(lintIndex) & ");"
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ");"
					lintIndex = lintIndex + 1
					.sEditRecordParam = .sEditRecordParam & "&nLocal_Type=" & .Columns("valLocal_Type").DefValue
					Response.Write(mobjGrid.DoRow())
				Next lclsLocateTar_Soat
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
        lclsLocateTar_Soat = Nothing
        lcolLocateTar_Soats = Nothing
End Sub

'% insPreMSO6000Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
    Private Sub insPreMSO6000Upd()
        '------------------------------------------------------------------------------
        Dim lclsLocateTar_Soat As eBranches.LocateTar_Soat
	
        With Request
            
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclsLocateTar_Soat = New eBranches.LocateTar_Soat
                Call lclsLocateTar_Soat.InsPostMSO6000(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nLocal_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nZipCode_Ini"), eFunctions.Values.eTypeData.etdDouble), 0)
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmantauto.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
	
        lclsLocateTar_Soat = Nothing
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MSO6000")

lobjErrors = New eGeneral.GeneralFunction


mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MSO6000"
'~End Body Block VisualTimer Utility	
%>
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%="<SCRIPT>"%>


//**% insTextClick: Method to bring up to date a registration of the Grid 
//% insTextClick: Método para actualizar un registro del Grid
//-----------------------------------------------------------------------------
function insTextClick(Field, lintIndex){
//-----------------------------------------------------------------------------		
    if (marrArray[lintIndex].hddEditRecord == 'True'){     
	    EditRecord(lintIndex,nMainAction,'Update',"dEffecDate=" + "<%=Request.QueryString.Item("dEffecDate")%>");
    }       
    else{
		<%lstrAlert = "Err. 90083 " & lobjErrors.insLoadMessage(90083)%>		
		alert('<%=lstrAlert%>' + " (" + marrArray[lintIndex].hddNullDate + ")" );
    }        
}

//**% insCheckSelClick: This function selects or de-selects the column "Sel"
//% insCheckSelClick: Esta función marca o desmarca la columna "Sel"
//-----------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-----------------------------------------------------------------------------    
    if (marrArray[lintIndex].hddEditRecord == 'False'){
		<%lstrAlert = "Err. 90083 " & lobjErrors.insLoadMessage(90083)%>		
		alert('<%=lstrAlert%>' + " (" + marrArray[lintIndex].hddNullDate + ")" );
		Field.checked = false
		marrArray[lintIndex].Sel = false
    }
}

//**% Objetive: This function establishes the status of the form when it is updated.
//%	Objetivo: Establece el estado de la forma cuando se actualiza.
//--------------------------------------------------------------------------------------------
function insDefUpdate(){
//--------------------------------------------------------------------------------------------
    var lblnDisabled = false
    with(self.document.forms[0]){
        lblnDisabled=hddEditRecord.value!='True'?true:false
		tcnZipCode_End.disabled=lblnDisabled
		//tctDescript.disabled=lblnDisabled		
	    if (typeof(cmdAccept)!='undefined')
	        cmdAccept.disabled=lblnDisabled;
    }
}


</SCRIPT>	
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MSO6000", "MSO6000.aspx"))
            mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSO6000" ACTION="valmantauto.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
    Call insDefineHeader()
    
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreMSO6000()
    Else
        Call insPreMSO6000Upd()
    End If
    
    mobjValues = Nothing
    mobjGrid = Nothing
    lobjErrors = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MSO6000")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





