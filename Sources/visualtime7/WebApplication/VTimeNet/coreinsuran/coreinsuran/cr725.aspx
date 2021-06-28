<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga del Grid de la ventana 'CR725'
Dim mobjcontr_cescovs As eCoReinsuran.contr_cescovs
'- Se define la variable en que se carga la colección
Dim mclscontr_cescov As eCoReinsuran.contr_cescov


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "cr725"
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 450
		.Height = 350
		.Top = 170
	End With
	
	
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("ValInsur_areaColumnCaption"), "ValInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, Session("nInsur_area"),  ,  , "", True, "OnChangeValInsur_area()", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("ValInsur_areaColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCompanyColumnCaption"), "valCompany", " reaCompanycontr", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCompanyColumnToolTip"))
		
		mobjGrid.Columns("valCompany").Parameters.Add("nNumber", Session("nNumber"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("valCompany").Parameters.Add("ntype_rel", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("valCompany").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) '
		mobjGrid.Columns("valCompany").Parameters.Add("nType", Session("nType"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If CStr(Session("nInsur_area")) = "1" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "tabtab_gencov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCoverColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "tabtab_lifcov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCoverColumnToolTip"))
		End If
		mobjGrid.Columns("valCover").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddCheckColumn(100658, GetLocalResourceObject("opcInOtherCovColumnCaption"), "opcInOtherCov", "",  ,  , "OnCheckInOtherCov();", True)
		
            If CStr(Session("nInsur_area")) = "1" Then
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverOtherColumnCaption"), "valCoverOther", "tabtab_gencov_rei", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "", True, , GetLocalResourceObject("valCoverOtherColumnToolTip"))
            Else
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverOtherColumnCaption"), "valCoverOther", "tabtab_lifcov_rei", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "", True, , GetLocalResourceObject("valCoverOtherColumnToolTip"))
            End If
            
            mobjGrid.Columns("valCoverOther").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("optnTypecapColumnCaption"), "optnTypecap", "table5552", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("optnTypecapColumnToolTip"))
            
		Call .AddNumericColumn(100661, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "",  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6)
		Call .AddNumericColumn(100662, GetLocalResourceObject("tcnCessPrFixColumnCaption"), "tcnCessPrFix", 18, "",  , GetLocalResourceObject("tcnCessPrFixColumnToolTip"), True, 6)
		Call .AddTextColumn(100660, GetLocalResourceObject("tctRoucessColumnCaption"), "tctRoucess", 12, "",  , GetLocalResourceObject("tctRoucessColumnToolTip"))
	End With
	
	With mobjGrid
		.Columns("ValInsur_area").BlankPosition = False
		.DeleteButton = True
		.AddButton = True
		.Columns("ValInsur_area").EditRecord = True
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "ValInsur_area='+ marrArray[lintIndex].ValInsur_area + '" & "&valCover='+ marrArray[lintIndex].valCover + '" & "&valCompany='+ marrArray[lintIndex].valCompany + '" & "&opcInOtherCov='+ marrArray[lintIndex].opcInOtherCov + '" & "&tctRoucess='+ marrArray[lintIndex].tctRoucess + '" & "&tcnRate='+ marrArray[lintIndex].tcnRate + '" & "&tcnCessPrFix='+ marrArray[lintIndex].tcnCessPrFix + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'%insPreCR007: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCR725()
	'--------------------------------------------------------------------------------------------
	Dim lblnFind As Boolean
	Dim lintCount As Object
	With mobjValues
		lblnFind = mobjcontr_cescovs.Find(.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, True, eRemoteDB.Constants.intNull)
	End With
	For	Each mclscontr_cescov In mobjcontr_cescovs
		With mobjGrid
			.Columns("ValInsur_area").DefValue = CStr(mclscontr_cescov.nInsur_area)
			.Columns("valCover").DefValue = CStr(mclscontr_cescov.nCovergen)
			.Columns("valCompany").DefValue = CStr(mclscontr_cescov.nCompany)
			If mclscontr_cescov.sInothercov = "1" Then
				.Columns("opcInOtherCov").Checked = CShort(mclscontr_cescov.sInothercov)
			Else
				.Columns("opcInOtherCov").Checked = CShort("2")
			End If
			.Columns("tctRoucess").DefValue = mclscontr_cescov.sRoucess
			.Columns("tcnRate").DefValue = CStr(mclscontr_cescov.nRate)
			.Columns("tcnCessPrFix").DefValue = CStr(mclscontr_cescov.nCessprfix)
			.Columns("optnTypecap").DefValue = CStr(mclscontr_cescov.nTypecap)
			If mclscontr_cescov.nInsur_area = 1 Then
				.Columns("valCover").TableName = "tabtab_gencov_rei"
			Else
				.Columns("valCover").TableName = "tabtab_lifcov_rei"
			End If
                .Columns("valCompany").TableName = "reaCompanycontr"
                .Columns("valCoverOther").DefValue = CStr(mclscontr_cescov.nCovergen_Other)
                
                If mclscontr_cescov.nInsur_area = 1 Then
                    .Columns("valCoverOther").TableName = "tabtab_gencov_rei"
                Else
                    .Columns("valCoverOther").TableName = "tabtab_lifcov_rei"
                End If
		End With
		Response.Write(mobjGrid.DoRow())
	Next mclscontr_cescov
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
End Sub

'% insPreCR725Upd. Se define esta funcion para contruir el contenido de la ventana UPD 
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCR725Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lintSel As Byte
	If Request.QueryString.Item("Action") = "Del" Then
		lintSel = 2
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR725", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
		
		
		
		With Request
			lblnPost = mclscontr_cescov.InsPostCR725("Del", mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("tctRoutine"), mobjValues.StringToType(.QueryString.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnCessPrFix"), eFunctions.Values.eTypeData.etdDouble), "", mobjValues.StringToType(.QueryString.Item("optnTypecap"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),0)
		End With
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&nContraType=" & Session("nType") & "&sCodispl_CR=" & Session("sCodispl_CR") & "&nNumber=" & Session("nNumber") & "&nYear_contr=" & Session("nYear_contr") & "&nBranch=" & Session("nBranch") & "&dContrDate=" & Session("dEffecdate") & "&nYearSer=" & Session("nYearSer") & "&nCompany=" & Session("nCompany") & "&nPerType=" & Session("nPerType") & "&nPerNum=" & Session("nPerNum") & "&sBussiType=" & Session("sBussiType") & "&nCurrency=" & Session("nCurrency") & "&sGoToNext=NO&sCodispl=" & Session("sCodispl") & """;</" & "Script>")
	Else
		
		
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR725", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
			
Response.Write("<script>InsPreCR725();</" & "script>")

			
		End With
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mobjcontr_cescovs = New eCoReinsuran.contr_cescovs
mclscontr_cescov = New eCoReinsuran.contr_cescov

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR725", "CR725.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

mobjValues.sCodisplPage = "cr725"

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 28/03/06 22:03 $"        
    
//OnCheckInOtherCov: Función que activa o desactiva los campos de acuerdo al check de 
//                   inclusión en otra cobertura.     
//-----------------------------------------------------------------------------------------
function OnCheckInOtherCov() {
//-----------------------------------------------------------------------------------------
    if (self.document.forms[0].opcInOtherCov.checked)
        {
		self.document.forms[0].optnTypecap.disabled=true;
		self.document.forms[0].optnTypecap.value="";
		self.document.forms[0].tctRoucess.disabled=true;
		self.document.forms[0].tctRoucess.value="";
		self.document.forms[0].tcnRate.disabled=true;
		self.document.forms[0].tcnRate.value="";			
		self.document.forms[0].tcnCessPrFix.disabled=true;
		self.document.forms[0].tcnCessPrFix.value = "";
		self.document.forms[0].valCoverOther.disabled = false;
		self.document.forms[0].btnvalCoverOther.disabled = false;
        }
    else
        {
		self.document.forms[0].optnTypecap.disabled=false;
		self.document.forms[0].tctRoucess.disabled=false;
		self.document.forms[0].tcnRate.disabled=false;
		self.document.forms[0].tcnCessPrFix.disabled = false;
		self.document.forms[0].valCoverOther.disabled = true;
		self.document.forms[0].btnvalCoverOther.disabled = true;
		self.document.forms[0].valCoverOther.value = "";
		UpdateDiv('valCoverOtherDesc', '');
        }
    }
    
//OnChangeValInsur_area: Función que activa o desactiva los campos de acuerdo al campo 
//                       tipo de cobertura.    
//-----------------------------------------------------------------------------------------        
function OnChangeValInsur_area(){
//-----------------------------------------------------------------------------------------    


		self.document.forms[0].optnTypecap.disabled=false;
		self.document.forms[0].ValInsur_area.disabled=true;
		self.document.forms[0].valCover.disabled =false;
		self.document.forms[0].btnvalCover.disabled =false;
 		self.document.forms[0].valCover.sTabName='tabtab_lifcov_rei';
 		self.document.forms[0].valCompany.disabled =false;
 		self.document.forms[0].btnvalCompany.disabled =false;
		self.document.forms[0].valCompany.sTabName='reaCompanycontr';   	    

	
}
//InsPreCR725: Función que cambia el package del valores posibles de coberturas dependiendo
//			   del tipo de cobertura seleccionado.	
//-----------------------------------------------------------------------------------------        
function InsPreCR725(){
//-----------------------------------------------------------------------------------------        

	self.document.forms[0].opcInOtherCov.disabled = false;
	self.document.forms[0].valCover.disabled = false;

    if (self.document.forms[0].ValInsur_area.value == 1) 
		self.document.forms[0].valCover.sTabName='tabtab_gencov_rei'
    else
		self.document.forms[0].valCover.sTabName='tabtab_lifcov_rei';
	
	self.document.forms[0].valCompany.sTabName ='reaCompanycontr';	
	
	if('<%=Request.QueryString.Item("nMainAction")%>' == '302')
	{
		self.document.forms[0].ValInsur_area.disabled =true;
		self.document.forms[0].valCover.disabled =true;
		self.document.forms[0].valCompany.disabled =true;
    }

    if (self.document.forms[0].ValInsur_area.value == 1)
        self.document.forms[0].valCoverOther.sTabName = 'tabtab_gencov_rei'
    else
        self.document.forms[0].valCoverOther.sTabName = 'tabtab_lifcov_rei';

}

</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCR725" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR725"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call insPreCR725()
Else
	Call insPreCR725Upd()
End If
%>
</FORM>
</BODY>
</HTML>






