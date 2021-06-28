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
	
	mobjGrid.sCodisplPage = "MSO8500"
	
'+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            
            Call .AddPossiblesColumn(9221, "Grupo", "valGroupAuto", "Table6028", 1, "", , , , , , , , "Grupo al que pertenece el vehículo según el uso que este tiene")
            Call .AddPossiblesColumn(9220, "Marca", "valTrademarks", "Table7042", eFunctions.Values.eValuesType.clngComboType, , , , , ,"InsChangeValues(this);" , , 2, "Marca del vehículo", eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(9220, "Clase", "valclass", "Table226", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, "Clase del vehículo", eFunctions.Values.eTypeCode.eNumeric)
                        
            Call .AddPossiblesColumn(9220, "Modelo", "valModel", "tabTab_au_model", eFunctions.Values.eValuesType.clngWindowType, ,True , , , , , , , "Modelo del vehículo", eFunctions.Values.eTypeCode.eNumeric)
            With mobjGrid.Columns("valModel").Parameters
				.Add("sStatregt","1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nVehbrand", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With

            'Call .AddPossiblesColumn(9222, "Localidad", "valLocateSoat", "TABLOCATE_SOAT", eFunctions.Values.eValuesType.clngComboType, "", True, , , , , , , "Indica el código correspondiente a la localidad")
            'Call mobjGrid.Columns("valLocateSoat").Parameters.Add("dEffecdate", mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            
            Call .AddNumericColumn(9224, "Número de asientos", "tcnSeats", 2, CStr(eRemoteDB.Constants.intNull), False, "Número de asientos del vehículo.", True, 0, , , , False)
            
            Call .AddPossiblesColumn(9220, "Zona de circulación", "valMovement", "Table224", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, "Departamento/provincia que aparece en la tarjeta de propiedad del vehículo", eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(9220, "Tipo de persona", "valTypeperson", "Table5006", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, "Tarifa aplica para una persona de tipo natural, jurídica o para ambos", eFunctions.Values.eTypeCode.eNumeric)
            
            Call .AddPossiblesColumn(9220, "Tipo de prima", "valTypepremiun", "Table8505", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, "Manera en que será calculada la prima de la tarifa SOAT", eFunctions.Values.eTypeCode.eNumeric)
            
            Call .AddPossiblesColumn(9223, "Tipo de Cálculo de prima", "valTypeCalculate", "Table8502", 1, "", , , , , , , , " Trata de una prima para emisión o renovación de póliza")
            Call .AddNumericColumn(9225, "Prima", "tcnPremium", 8, CStr(eRemoteDB.Constants.intNull), False, "Valor de la prima total de la póliza", True, 2, , , , False)
            Call .AddHiddenColumn("cbeBranch", vbNullString)
            Call .AddHiddenColumn("valProduct", vbNullString)
            Call .AddHiddenColumn("tcdEffecdate", vbNullString)
            Call .AddHiddenColumn("cbecurrency", vbNullString)
            Call .AddHiddenColumn("hddNullDate", vbNullString)
            Call .AddHiddenColumn("hddEditRecord", vbNullString)

        End With
    
	With mobjGrid
	    .nMainAction = Request.QueryString("nMainAction")
		.Codispl = "MSO8500"
        .Codisp  = "MSO8500"
        .Top    = 100	        
            .Height = 400
		.Width  = 730
		.ActionQuery = mobjValues.ActionQuery
            .MoveRecordScript = "insDefUpdate()"
            .bOnlyForQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery
        .Columns("Sel").GridVisible = Not .ActionQuery
        '.columns("valTypeperson").BlankDescript = "Ambos"	    
            .Columns("cbecurrency").DefValue = Request.QueryString("ncurrency")
            .Columns("tcdEffecdate").DefValue = Request.QueryString("dEffecDate")
            .Columns("cbeBranch").DefValue = Request.QueryString("nBranch")
            .Columns("valProduct").DefValue = Request.QueryString("nProduct")
            
            .sDelRecordParam = "ncurrency=" + Request.QueryString("ncurrency") & _
                               "&dEffecDate=" + Request.QueryString("dEffecDate") & _
                               "&nBranch=" + Request.QueryString("nBranch") & _
                               "&nProduct=" + Request.QueryString("nProduct") & _
                               "&nGroupAuto='+ marrArray[lintIndex].valGroupAuto + '" & _
                               "&nclass='+ marrArray[lintIndex].valclass + '" & _
                               "&nLocateSoat='+ marrArray[lintIndex].valMovement + '"& _
                               "&nTrademarks='+ marrArray[lintIndex].valTrademarks + '"& _
                               "&nModel='+ marrArray[lintIndex].valModel + '"& _
                               "&nSeats='+ marrArray[lintIndex].tcnSeats + '"& _
                               "&nTypeperson='+ marrArray[lintIndex].valTypeperson + '"& _
                               "&Typepremiun='+ marrArray[lintIndex].valTypepremiun + '"& _
                               "&nTypeCalculate='+ marrArray[lintIndex].valTypeCalculate + '"
            
        .sEditRecordParam = "ncurrency=" + Request.QueryString("ncurrency") & _
                            "&dEffecDate=" + Request.QueryString("dEffecDate") & _
                            "&nBranch=" + Request.QueryString("nBranch") & _
                            "&nProduct=" + Request.QueryString("nProduct")
        
        If Request.QueryString("Reload") = "1" then
            .sReloadIndex = Request.QueryString("ReloadIndex")
        End If	    
	End With
end Sub

'%insPreMSO8500. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private sub insPreMSO8500()
'------------------------------------------------------------------------------
        Dim lcolTar_SOATs
    Dim lclsTar_SOAT
    Dim lintIndex
    lintIndex = 0
    With Request
            lcolTar_SOATs = New eBranches.Tar_SOATs
        With mobjGrid
        
                If lcolTar_SOATs.FindMSO8500(mobjValues.StringToType(Request.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                  mobjValues.StringToType(Request.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                  mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                  mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger)) Then

                    For Each lclsTar_SOAT In lcolTar_SOATs

                        If lclsTar_SOAT.dNulldate > mobjValues.StringToType(Request.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate) Then
                            .Columns("valclass").EditRecord = False
                        Else
                            .Columns("valclass").EditRecord = True
                        End If
					
                        .Columns("valTypeCalculate").DefValue = lclsTar_SOAT.nTypeCalculate
                        .Columns("valTrademarks").DefValue = lclsTar_SOAT.nVehBrand
                        .Columns("valGroupAuto").DefValue = lclsTar_SOAT.nGroupVeh
                        .Columns("valclass").DefValue = lclsTar_SOAT.nVehType
                        .Columns("valModel").DefValue = lclsTar_SOAT.sVehModel
                        '.Columns("valLocateSoat").DefValue = lclsTar_SOAT.nLocat_Type
                        .Columns("tcnSeats").DefValue = lclsTar_SOAT.nPlace
                        .Columns("valMovement").DefValue = lclsTar_SOAT.nLocat_Type
                        .Columns("valTypeperson").DefValue = lclsTar_SOAT.nPersontyp
                        .Columns("valTypepremiun").DefValue = lclsTar_SOAT.nTypePremium
                        .Columns("tcnPremium").DefValue = lclsTar_SOAT.nPremiumn
                        .Columns("hddNullDate").DefValue = lclsTar_SOAT.dNullDate
                        .Columns("hddEditRecord").DefValue = lclsTar_SOAT.bEditRecord
                        
                    
                        '.Columns("valclass").HRefScript = "insTextClick(this," & cstr(lintIndex) & ");"
                        .Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ");"
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

'% insPreMSO8500Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMSO8500Upd()
'------------------------------------------------------------------------------
    Dim lclsTar_SOAT
    
    With Request
        If .QueryString("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
            lclsTar_SOAT = New eBranches.Tar_SOAT
                Call lclsTar_SOAT.InsPostMSO8500(False, .QueryString("sCodispl"), _
                                                        mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), _
                                                        .QueryString("Action"), _
                                                        mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                                        mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nGroupAuto"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nclass"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nTrademarks"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nModel"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nSeats"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nLocateSoat"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nTypeperson"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("Typepremiun"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.QueryString("nTypeCalculate"), eFunctions.Values.eTypeData.etdInteger), _
                                                        mobjValues.StringToType(.Form("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))                                                       
        End If
        Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valmantauto.aspx", .QueryString("sCodispl"), .QueryString("nMainAction"), mobjGrid.ActionQuery, .QueryString("Index")))
    End With
        lclsTar_SOAT = Nothing
End Sub

</script>

<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("MSO8500")

    mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MSO8500"
'~End Body Block VisualTimer Utility
	
    lobjErrors = New eGeneral.GeneralFunction

    mobjValues.ActionQuery = (Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
	With Response
		.Write(mobjValues.StyleSheet())
		.Write("<SCRIPT>var	nMainAction	= " & CInt("0" & Request.QueryString("nMainAction")) & "</SCRIPT>")
		If Request.QueryString("Type") <> "PopUp" Then
			mobjMenu = New eFunctions.Menues
			.Write(mobjMenu.setZone(2,"MSO8500","MSO8500.aspx"))
			mobjMenu = Nothing
		End If
	End	With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
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

//% InsChangeValues: Se actualizan los parametros de las listas de valores 
//------------------------------------------------------------------------------------------- 
function InsChangeValues(Field){ 
//------------------------------------------------------------------------------------------- 

	self.document.forms[0].valModel.Parameters.Param2.sValue = Field.value ;
} 

//**% Objetive: This function establishes the status of the form when it is updated.
//%	Objetivo: Establece el estado de la forma cuando se actualiza.
//--------------------------------------------------------------------------------------------
function insDefUpdate(){
//--------------------------------------------------------------------------------------------
    
    //var lblnDisabled = false
    with(self.document.forms[0]){
	     valTypeperson.disabled= true
    }
}
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSO8500" ACTION="valmantauto.aspx?sZone=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
    Call insDefineHeader()
	If Request.QueryString("Type") <> "PopUp" Then
        Call insPreMSO8500()
	Else
        Call insPreMSO8500Upd()
        If Request.QueryString("Action") = "Update" Then
            Response.Write("<SCRIPT>insDefUpdate() </SCRIPT>")
        End If
    End If
	
    mobjValues = Nothing
    mobjGrid = Nothing
    lobjErrors = Nothing
%>	  
</FORM>
</BODY>
</HTML>