<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las zonas de la página    
    Dim mobjMenu As eFunctions.Menues
    
    '- Se define la variable mobjGrid para el manejo del Grid de la ventana
    Dim mobjGrid as eFunctions.Grid
    
    '- Objeto para el manejo de los errores y las advertencias       
    Dim lobjErrors As eGeneral.GeneralFunction
	
    '- Variable para el manejo de los errores y las advertencias
    Dim lstrAlert As String
    
    '%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MSO6001"
	
'+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            
            Call .AddPossiblesColumn(9220, "Clase de vehículo", "valclass", "Table226", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, "Indica la clase del vehículo", eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(9221, "Grupo del vehículo", "valGroupAuto", "Table6028", 1, "", , , , , , , , "Indica el grupo del vehículo")

            Call .AddPossiblesColumn(9222, "Localidad", "valLocateSoat", "TABLOCATE_SOAT", eFunctions.Values.eValuesType.clngComboType, "", True, , , , , , , "Indica el código correspondiente a la localidad")
            Call mobjGrid.Columns("valLocateSoat").Parameters.Add("dEffecdate", mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Call .AddPossiblesColumn(9223, "Tipo de Cálculo", "valTypeCalculate", "Table6030", 1, "", , , , , , , , "Indica el tipo de cálculo a ser aplicado en la tarifa se Soat")
            Call .AddNumericColumn(9224, "Prima neta", "tcnpremiumn", 18, CStr(eRemoteDB.Constants.intNull), False, "Indica el valor de la prima neta", True, 6, , , , False)
            Call .AddNumericColumn(9225, "Prima", "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull), False, "Indica el valor de la prima total de la póliza", True, 6, , , , False)
            Call .AddNumericColumn(9226, "Tarifa", "tcnTarif", 5, CStr(eRemoteDB.Constants.intNull), False, "Corresponde al valor de la tarifa", False, 0, , , , False)
            Call .AddHiddenColumn("hddNullDate", vbNullString)
            Call .AddHiddenColumn("hddEditRecord", vbNullString)

        End With
    
	With mobjGrid
	    .nMainAction = Request.QueryString("nMainAction")
		.Codispl = "MSO6001"
        .Codisp  = "MSO6001"
        .Top    = 100	        
		.Height = 352
		.Width  = 730
		.ActionQuery = mobjValues.ActionQuery
		.MoveRecordScript = "insDefUpdate()"
            .bOnlyForQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery
        .Columns("Sel").GridVisible = Not .ActionQuery
        .Columns("valclass").Disabled = Request.QueryString("Action") = "Update"
        .Columns("valGroupAuto").Disabled = Request.QueryString("Action") = "Update"
        .Columns("valLocateSoat").Disabled = Request.QueryString("Action") = "Update"
	    
	    .sDelRecordParam = "ncurrency=" + Request.QueryString("ncurrency") & _
	                       "&dEffecDate=" + Request.QueryString("dEffecDate") & _ 
	                       "&nBranch=" + Request.QueryString("nBranch") & _ 
	                       "&nProduct=" + Request.QueryString("nProduct") & _
	                       "&nGroupAuto='+ marrArray[lintIndex].valGroupAuto + '" & _ 
	                       "&nclass='+ marrArray[lintIndex].valclass + '" & _
	                       "&nLocateSoat='+ marrArray[lintIndex].valLocateSoat + '"
	    
        .sEditRecordParam = "ncurrency=" + Request.QueryString("ncurrency") & _
                            "&dEffecDate=" + Request.QueryString("dEffecDate") & _
                            "&nBranch=" + Request.QueryString("nBranch") & _
                            "&nProduct=" + Request.QueryString("nProduct")
        
        If Request.QueryString("Reload") = "1" then
            .sReloadIndex = Request.QueryString("ReloadIndex")
        End If	    
	End With
end Sub

'%insPreMSO6001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private sub insPreMSO6001()
'------------------------------------------------------------------------------
    Dim lcolTar_SOATs
    Dim lclsTar_SOAT
    Dim lintIndex
    lintIndex = 0
    With Request
        lcolTar_SOATs = New eBranches.Tar_SOATs    
        With mobjGrid
        
            if lcolTar_SOATs.Find(mobjValues.StringToType(Request.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                  mobjValues.StringToType(Request.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                  mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                  mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger)) then

                For each lclsTar_SOAT in lcolTar_SOATs

                        If lclsTar_SOAT.dNulldate > mobjValues.StringToType(Request.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate) Then
                            .Columns("valclass").EditRecord = False
                        Else
                            .Columns("valclass").EditRecord = True
                        End If
					
                    .Columns("valTypeCalculate").DefValue = lclsTar_SOAT.nTypeCalculate
                    .Columns("valGroupAuto").DefValue = lclsTar_SOAT.nGroupVeh
                    .Columns("tcnTarif").DefValue = lclsTar_SOAT.nTariff
                    .Columns("valclass").DefValue = lclsTar_SOAT.nVehType
                    .Columns("valLocateSoat").DefValue = lclsTar_SOAT.nLocat_Type
                    .Columns("tcnpremiumn").DefValue = lclsTar_SOAT.npremiumn
                    .Columns("tcnPremium").DefValue = lclsTar_SOAT.nPremiumTar
                    .Columns("hddNullDate").DefValue = lclsTar_SOAT.dNullDate
                    .Columns("hddEditRecord").DefValue = lclsTar_SOAT.bEditRecord                                        
                    
                    '.Columns("valclass").HRefScript = "insTextClick(this," & cstr(lintIndex) & ");"
                    .Columns("Sel").OnClick = "insCheckSelClick(this," & cstr(lintIndex) & ");"
                    lintIndex = lintIndex + 1
                    
                        Response.Write(mobjGrid.DoRow())
                Next
            End If
        End With

    End With
        Response.Write(mobjGrid.closeTable())
    
        lclsTar_SOAT = Nothing
        lcolTar_SOATs = Nothing
End Sub

'% insPreMSO6001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMSO6001Upd()
'------------------------------------------------------------------------------
    Dim lclsTar_SOAT
    
    With Request
        If .QueryString("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
            lclsTar_SOAT = New eBranches.Tar_SOAT
            Call lclsTar_SOAT.InsPostMSO6001(False, .QueryString("sCodispl"), _
                                             .QueryString("nMainAction"), _
                                             .QueryString("Action"), _
                                             Session("nUsercode"), _
                                             mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                             mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                             mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                             mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                             0, _
                                             mobjValues.StringToType(.QueryString("nGroupAuto"), eFunctions.Values.eTypeData.etdInteger), _
                                             0, _
                                             mobjValues.StringToType(.QueryString("nclass"), eFunctions.Values.eTypeData.etdInteger), _
                                             mobjValues.StringToType(.QueryString("nLocateSoat"), eFunctions.Values.eTypeData.etdInteger), _
                                             0, 0)            
        End If
        Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valmantauto.aspx", .QueryString("sCodispl"), .QueryString("nMainAction"), mobjGrid.ActionQuery, .QueryString("Index")))
    End With
        lclsTar_SOAT = Nothing
End Sub

</script>

<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("MSO6001")

    mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MSO6001"
'~End Body Block VisualTimer Utility
	
    lobjErrors = New eGeneral.GeneralFunction
%>

<%
    mobjValues.ActionQuery = (Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
	With Response
		.Write(mobjValues.StyleSheet())
		.Write("<SCRIPT>var	nMainAction	= " & CInt("0" & Request.QueryString("nMainAction")) & "</SCRIPT>")
		If Request.QueryString("Type") <> "PopUp" Then
			mobjMenu = New eFunctions.Menues
			.Write(mobjMenu.setZone(2,"MSO6001","MSO6001.aspx"))
			mobjMenu = Nothing
		End If
	End	With
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT>	

//**% insCheckSelClick: This function selects or de-selects the column "Sel"
//% insCheckSelClick: Esta función marca o desmarca la columna "Sel"
//-----------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-----------------------------------------------------------------------------    
    if (marrArray[lintIndex].hddEditRecord == 'False'){
		<%lstrAlert = "Err. 90083 " & lobjErrors.insLoadMessage(90083)%>		
		alert('<%= lstrAlert %>' + " (" + marrArray[lintIndex].hddNullDate + ")" );
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
		valGroupAuto.disabled=lblnDisabled;
		valclass.disabled=lblnDisabled;		
		valLocateSoat.disabled=lblnDisabled;		
	    if (typeof(cmdAccept)!='undefined')
	        cmdAccept.disabled=lblnDisabled;
    }
}

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSO6001" ACTION="valmantauto.aspx?sZone=2">

<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
    Call insDefineHeader()
	If Request.QueryString("Type") <> "PopUp" Then
        Call insPreMSO6001()
	Else
        Call insPreMSO6001Upd()
	End	If	
	
	mobjValues = Nothing
	mobjGrid = Nothing
	lobjErrors = Nothing    
	
    Call mobjNetFrameWork.FinishPage("MSO6001")
%>	  
</FORM>
</BODY>
</HTML>