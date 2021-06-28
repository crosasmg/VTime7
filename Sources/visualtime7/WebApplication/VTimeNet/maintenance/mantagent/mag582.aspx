<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboType_histColumnCaption"), "cboType_hist", "Table165", 1,  ,  ,  ,  ,  , "ChangedValues(this)",  , 2, GetLocalResourceObject("cboType_histColumnToolTip"), 1)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDet_transacColumnCaption"), "cbeDet_transac", "TabDet_Transac", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeDet_transacColumnToolTip"), 1)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitRangeColumnCaption"), "tcnInitRange", 18, CStr(0), True, GetLocalResourceObject("tcnInitRangeColumnToolTip"), False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndRangeColumnCaption"), "tcnEndRange", 18, CStr(0), False, GetLocalResourceObject("tcnEndRangeColumnToolTip"), False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, CStr(0), False, GetLocalResourceObject("tcnPercentColumnToolTip"), False, 2,  ,  , "EnabledFields(this)", False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0), False, GetLocalResourceObject("tcnAmountColumnToolTip"), False, 6,  ,  , "EnabledFields(this)", False)
	End With
	
	With mobjGrid
		.Codispl = "MAG582"
		.Codisp = "MAG582"
		.sCodisplPage = "MAG582"
		.Top = 100
		.Height = 320
		.Width = 410
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("nMainAction") = "302" Then
			.Columns("cboType_hist").EditRecord = True
		End If
		.Columns("cboType_hist").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbeDet_transac").Parameters.Add("nType_hist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeDet_transac").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeDet_transac").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeDet_transac").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnInitRange").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "pnDet_transac='+ marrArray[lintIndex].cbeDet_transac + '" & "&pnInitRange='+ marrArray[lintIndex].tcnInitRange + '" & "&pnType_hist=' + marrArray[lintIndex].cboType_hist + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG582. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMAG582()
	'------------------------------------------------------------------------------
	Dim lcolexcess_maints As eAgent.excess_maints
	Dim lclsexcess_maint As Object
	
	lcolexcess_maints = New eAgent.excess_maints
	
	With mobjGrid
		If lcolexcess_maints.Find(mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			For	Each lclsexcess_maint In lcolexcess_maints
				
				.Columns("cboType_hist").DefValue = mobjValues.StringToType(lclsexcess_maint.nType_hist, eFunctions.Values.eTypeData.etdDouble)
				
				If lclsexcess_maint.nDet_transac <> 0 Then
					.Columns("cbeDet_transac").Parameters.Add("nType_hist", mobjValues.StringToType(lclsexcess_maint.nType_hist, eFunctions.Values.eTypeData.etdDouble))
					.Columns("cbeDet_transac").Parameters.Add("nBranch", Session("nBranch"))
					.Columns("cbeDet_transac").Parameters.Add("nProduct", Session("nProduct"))
				End If
				
				.Columns("cbeDet_transac").DefValue = mobjValues.StringToType(lclsexcess_maint.nDet_transac, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnInitRange").DefValue = mobjValues.StringToType(lclsexcess_maint.nInitRange, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnEndRange").DefValue = mobjValues.StringToType(lclsexcess_maint.nEndRange, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnPercent").DefValue = mobjValues.StringToType(lclsexcess_maint.nPercent, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAmount").DefValue = mobjValues.StringToType(lclsexcess_maint.nAmount, eFunctions.Values.eTypeData.etdDouble)
				Response.Write(mobjGrid.DoRow())
			Next lclsexcess_maint
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolexcess_maints = Nothing
End Sub

'% insPreMAG582Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMAG582Upd()
	'------------------------------------------------------------------------------
	Dim lclsexcess_maint As eAgent.excess_maint
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsexcess_maint = New eAgent.excess_maint
			Call lclsexcess_maint.insPostMAG582(.QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnType_hist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnDet_transac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnInitRange"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", "MAG582", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsexcess_maint = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
mobjValues.sCodisplPage = "MAG582"
%>


<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:35 $"

// EnabledFields: Habilita los campos "Porcentaje" y "Monto" 
//--------------------------------------------------------------------------------
function EnabledFields(Field){
//--------------------------------------------------------------------------------		

		if(Field.name=='tcnPercent'){
			if(Field.value!=0){		
				self.document.forms[0].tcnAmount.disabled=true;
				self.document.forms[0].tcnAmount.value=0;
			}	
			else
				self.document.forms[0].tcnAmount.disabled=false;
		}
		else if(Field.name=='tcnAmount'){
			if(Field.value!=0 && Field.value!=''){
				self.document.forms[0].tcnPercent.disabled=true;
				self.document.forms[0].tcnPercent.value=0;
			}	
			else
				self.document.forms[0].tcnPercent.disabled=false;
		}
}
// ChangedValues: Habilita el campo "Detalle" si el combo de "Transacción", es "Anulación" o "Endoso"
//---------------------------------------------------------------------------------------------------
function ChangedValues(Field){
//---------------------------------------------------------------------------------------------------		

	self.document.forms[0].cbeDet_transac.Parameters.Param1.sValue=Field.value
	if(Field.value!=11 && Field.value!=12 && 
	   Field.value!=25 && Field.value!=29 && 
	   Field.value!=30 && Field.value!=54 && 
	   Field.value!=55 && Field.value!=62 && 
	   Field.value!=65 )
	{
		self.document.forms[0].elements["cbeDet_transac"].value="";
		UpdateDiv('cbeDet_transacDesc','');
		self.document.forms[0].cbeDet_transac.disabled=true;
		self.document.forms[0].btncbeDet_transac.disabled=true;		
	}	
	else	
	{
		if(sAction!="Update")
		{
			self.document.forms[0].elements["cbeDet_transac"].value="";
			UpdateDiv('cbeDet_transacDesc','');
			self.document.forms[0].cbeDet_transac.disabled=false;
		    self.document.forms[0].btncbeDet_transac.disabled=false;
			self.document.forms[0].cbeDet_transac.value='';
		}
	}
}


</SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">

<%="<script>var sAction='" & Request.QueryString.Item("Action") & "'</script>"%>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MAG582", "MAG582.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("MAG582"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMAG582" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG582()
Else
	Call insPreMAG582Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





