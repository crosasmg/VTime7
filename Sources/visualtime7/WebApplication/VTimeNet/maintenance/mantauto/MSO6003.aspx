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
	
	mobjGrid.sCodisplPage = "MSO6003"
	
'+Se definen todas las columnas del Grid
    With mobjGrid.Columns
        
        Call .AddPossiblesColumn(0, "Clasificación", "cbeSOATClass", "Table6022", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, "Clasificación del del cliente", eFunctions.Values.eTypeCode.eNumeric)
		Call .AddPossiblesColumn(0, "Frecuencia", "cbePayFreq", "tabPay_Fracti", eFunctions.Values.eValuesType.clngComboType, "0", True, , , , , True, 5, "Indica la frecuencia de pago", eFunctions.Values.eTypeCode.eString)
        
        With mobjGrid.Columns("cbePayFreq").Parameters 
            .Add("nBranch", mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput,	eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0,	0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nProduct", mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nQuota", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("dEffecdate", mobjValues.StringToType(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
               	
		Call .AddHiddenColumn("hddNullDate", vbNullString)
		Call .AddHiddenColumn("hddEditRecord", vbNullString)
    End With
    
	With mobjGrid
	    .nMainAction = Request.QueryString("nMainAction")
		.Codispl = "MSO6003"
        .Codisp  = "MSO6003"
        .Top    = 100	        
		.Height = 192
		.Width  = 400
		.WidthDelete = 600
		
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery        
        .Columns("Sel").GridVisible = Not .ActionQuery
        
		.Columns("cbePayFreq").Disabled = false	  
	    .sDelRecordParam =   "nBranch=" + Request.QueryString("nBranch") + "&dEffecDate=" + Request.QueryString("dEffecDate") + "&nProduct=" + Request.QueryString("nProduct") + "&nPayFreq='+ marrArray[lintIndex].cbePayFreq + '" + "&nSOATClass='+ marrArray[lintIndex].cbeSOATClass + '" & _
	                         "&dNullDate='+ marrArray[lintIndex].hddNullDate + '"	
	                         
		.sEditRecordParam = "nBranch=" + Request.QueryString("nBranch") + "&dEffecDate=" + Request.QueryString("dEffecDate") + "&nProduct=" + Request.QueryString("nProduct")
        If Request.QueryString("Reload") = "1" then
            .sReloadIndex = Request.QueryString("ReloadIndex")
        End If	    
	End With
end Sub

'%insPreMSO6003. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private sub insPreMSO6003()
'------------------------------------------------------------------------------
    Dim lcolFPay_AllowClasss
    Dim lclsFPay_AllowClass
    Dim lintIndex   
    lintIndex = 0
    With Request
        lcolFPay_AllowClasss = New eBranches.FPay_AllowClasss   
        With mobjGrid
            if lcolFPay_AllowClasss.Find(mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                         mobjValues.StringToType(Request.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                         mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger)) then
                For each lclsFPay_AllowClass in lcolFPay_AllowClasss
                    .Columns("cbeSOATClass").DefValue  = lclsFPay_AllowClass.nSOATClass
                    .Columns("cbePayFreq").DefValue    = lclsFPay_AllowClass.nPayFreq                    
                    .Columns("hddNullDate").DefValue   = lclsFPay_AllowClass.dNullDate                    
                    .Columns("hddEditRecord").DefValue = lclsFPay_AllowClass.bEditRecord
                    .Columns("Sel").OnClick            = "insCheckSelClick(this," & CStr(lintIndex) & ")"
                    lintIndex = lintIndex + 1
                    Response.Write(mobjGrid.DoRow())
                Next
            End If
        End With

    End With
    Response.Write(mobjGrid.CloseTable())
    
    lclsFPay_AllowClass = Nothing
    lcolFPay_AllowClasss = Nothing
End Sub

'% insPreMSO6003Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMSO6003Upd()
'------------------------------------------------------------------------------
    Dim lclsFPay_AllowClass
    
    With Request
        If .QueryString("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())
            lclsFPay_AllowClass = New eBranches.FPay_AllowClass   
            Call lclsFPay_AllowClass.InsPostMSO6003(False, .QueryString("sCodispl"), .QueryString("nMainAction"), .QueryString("Action"), _
                                        Session("nUsercode"), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                        mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                        mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                        mobjValues.StringToType(.QueryString("nPayFreq"), eFunctions.Values.eTypeData.etdInteger), _
                                        mobjValues.StringToType(.QueryString("nSOATClass"), eFunctions.Values.eTypeData.etdInteger))
        End If
        Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valmantauto.aspx", .QueryString("sCodispl"), .QueryString("nMainAction"), mobjGrid.ActionQuery, .QueryString("Index")))
    End With
    lclsFPay_AllowClass = Nothing
End Sub

</script>

<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("MSO6003")

    mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MSO6003"
'~End Body Block VisualTimer Utility
	
    lobjErrors = New eGeneral.GeneralFunction
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!--#INCLUDE VIRTUAL ="~/VTimeNet/Includes/General.aspx"-->

<%
    mobjValues.ActionQuery = (Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
	With Response
		.Write(mobjValues.StyleSheet())
		.Write("<SCRIPT>var	nMainAction	= " & CInt("0" & Request.QueryString("nMainAction")) & "</SCRIPT>")
		If Request.QueryString("Type") <> "PopUp" Then
			mobjMenu = New eFunctions.Menues
			.Write(mobjMenu.setZone(2,"MSO6003","MSO6003.aspx"))
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
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSO6003" ACTION="valmantauto.aspx?sZone=2">

<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
    Call insDefineHeader()
	If Request.QueryString("Type") <> "PopUp" Then
		Call insPreMSO6003()
	Else
		Call insPreMSO6003Upd()	
	End	If	
	
	mobjValues = Nothing
	mobjGrid = Nothing
	lobjErrors = Nothing    
%>	  
</FORM>
</BODY>
</HTML>