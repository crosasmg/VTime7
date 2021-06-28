<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 470
		.WidthDelete = 500
		.Height = 450
		.Top = 120
	End With
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("ValInsur_areaColumnCaption"), "ValInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "ChangeType(this.value);", False,  , GetLocalResourceObject("ValInsur_areaColumnToolTip"))
		
		'+ Si la variable de sesión "nBranch_rei" es igual a cualquiera de los ramos asociados a VIDA,
		'+ se ejecuta la lectura del SP "TabTab_lifCov_rei", de lo contrario, se ejecuta el SP "TabTab_GenCov_rei" - ACM - 15/01/2003
		'++++++++++++++++++++++OJO CON ESTAS VALIDACIONES....JRUGERO 30/07/2004+++++++++++++++++++
		
		
		Select Case Session("nInsur_area")
			'Case 1,2,3,4,6,7,8,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,35,36,37,38,39,40,41,42,43,44
			Case 2
				Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabTab_lifCov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "EnabledRelatedCover(this.value);", True,  , GetLocalResourceObject("valCoverColumnToolTip"))
			Case Else
				'Case 9,10,50,51,52,53,54,55,60,61,62,63,64,70,71,72,73,74,80,81,82,91,101,111
				Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabTab_GenCov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "EnabledRelatedCover(this.value);", True,  , GetLocalResourceObject("valCoverColumnToolTip"))
		End Select
		
		mobjGrid.Columns("valCover").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		
		'% Sección de Facultativo - Cuota parte.
		If Session("nInsur_area") = 2 Then
			Call mobjGrid.Columns.AddPossiblesColumn(0, GetLocalResourceObject("valRelatedCoverColumnCaption"), "valRelatedCover", "TabTab_lifCov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "VerifyCover(this.value);", True,  , GetLocalResourceObject("valRelatedCoverColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valRelatedCoverColumnCaption"), "valRelatedCover", "TabTab_GenCov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "VerifyCover(this.value);", True,  , GetLocalResourceObject("valRelatedCoverColumnToolTip"))
		End If
		
		mobjGrid.Columns("valRelatedCover").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentage_RelatedCoverColumnCaption"), "tcnPercentage_RelatedCover", 4, "",  , GetLocalResourceObject("tcnPercentage_RelatedCoverColumnToolTip"),  , 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnnMaxAmount_RelatedCoverColumnCaption"), "tcnnMaxAmount_RelatedCover", 19, "",  , GetLocalResourceObject("tcnnMaxAmount_RelatedCoverColumnToolTip"), True, 6,  ,  , "InsCalLimits(this);")
		
		'% Sección de Facultativo / Stop loss especifico.
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLimitColumnCaption"), "tcnLimit", 18, "",  , GetLocalResourceObject("tcnLimitColumnToolTip"), True, 6,  ,  , "EnabledFields(this.value);")
		
		'% Sección Excedentes.
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExcessColumnCaption"), "tcnExcess", 9, "",  , GetLocalResourceObject("tcnExcessColumnToolTip"), False, 6)
		
		'% Sección de Cuota_Parte.
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCuota_parteColumnCaption"), "tcnCuota_parte", 9, "",  , GetLocalResourceObject("tcnCuota_parteColumnToolTip"),  , 6,  ,  , "InsCalLimits(this);")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 19, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  , "InsCalLimits(this);")
		
		
		'% Rutina
		Call .AddTextColumn(0, GetLocalResourceObject("tctRoutineColumnCaption"), "tctRoutine", 12, "",  , GetLocalResourceObject("tctRoutineColumnToolTip"))
		
		'% Variable auxiliar
		Call .AddHiddenColumn("hddType", Session("nType"))
		Call .AddHiddenColumn("hddsLimitCov", "")
	End With
	
	With mobjGrid
		.DeleteButton = True
		.AddButton = True
		.Columns("ValCover").EditRecord = True
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("ValInsur_area").Disabled = True
		End If
		
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			'.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		
		'% Si el tipo de contrato corresponde a "Cuota_Parte". se habilita solo la sección de "Cuota_Parte". 
		'% No se habilitan las secciones correspondientes a Excedentes ni Facultativo / Stop loss especifico. 
		If Session("nType") = 2 Or Session("nType") = 3 Then
			.Columns("tcnExcess").Disabled = True
			.Columns("tcnLimit").Disabled = True
			'	        .Columns("valRelatedCover").Disabled = True
			'	        .Columns("tcnPercentage_RelatedCover").Disabled = True
		End If
		
		'% Si el tipo de contrato corresponde a "Excedentes". se habilita solo la sección "Excedentes" 
		'% no se habilitan las secciones correspondientes a Cuota_Parte ni Facultativo / Stop loss especifico
		If Session("nType") = 5 Or Session("nType") = 6 Or Session("nType") = 7 Or Session("nType") = 8 Then
			.Columns("tcnCuota_parte").Disabled = True
			'.Columns("tctRoutine").Disabled = True
			.Columns("tcnAmount").Disabled = True
			.Columns("tcnLimit").Disabled = True
			.Columns("valRelatedCover").Disabled = True
			.Columns("tcnPercentage_RelatedCover").Disabled = True
			.Columns("tcnnMaxAmount_RelatedCover").Disabled = True
		End If
		
		'% Si el tipo de contrato corresponde a "Facultativo ó Stop loss especifico" se habilitan solo la sección
		'% de "Facultativo / Stop loss especifico" no se habilitan las secciones correspondientes a Cuota_Parte ni 
		'% Excedentes.
		If Session("nType") = 9 Or Session("nType") = 10 Then
			.Columns("tcnCuota_parte").Disabled = True
			.Columns("tcnExcess").Disabled = True
			.Columns("tcnAmount").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "ValInsur_area='+ marrArray[lintIndex].ValInsur_area + '" & "&valCover='+ marrArray[lintIndex].valCover + '"
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% DoFormCR724: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub DoFormCR724()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim lclsContr_LimCov As eCoReinsuran.Contr_LimCov
	
	lclsContr_LimCov = New eCoReinsuran.Contr_LimCov
	
	Dim lclsContrproc As eCoReinsuran.Contrproc
	If lclsContr_LimCov.ReaContr_LimCov(Session("nNumber"), Session("nBranch_rei"), Session("nType"), Session("dEffecdate")) Then
            For lintCount = 1 To lclsContr_LimCov.Count
                'For lintCount = 0 To lclsContr_LimCov.Count - 1
                With mobjGrid
                    If lclsContr_LimCov.Item_CR724(lintCount) Then
                        .Columns("ValInsur_area").DefValue = CStr(lclsContr_LimCov.nInsur_area)
                        .Columns("valCover").DefValue = CStr(lclsContr_LimCov.nCovergen)
					
                        If Session("nType") = 9 Or Session("nType") = 10 Then
                            .Columns("tcnLimit").DefValue = CStr(lclsContr_LimCov.nAmount)
                        Else
                            .Columns("tcnAmount").DefValue = CStr(lclsContr_LimCov.nAmount)
                        End If
                        'patita
                        .Columns("valRelatedCover").DefValue = CStr(lclsContr_LimCov.nCoverApp)
                        .Columns("tcnPercentage_RelatedCover").DefValue = CStr(lclsContr_LimCov.nPercent)
                        .Columns("tcnnMaxAmount_RelatedCover").DefValue = CStr(lclsContr_LimCov.nMaxAmount)
                        .Columns("tcnExcess").DefValue = CStr(lclsContr_LimCov.nLines)
                        .Columns("tcnCuota_parte").DefValue = CStr(lclsContr_LimCov.nQuota_sha)
                        .Columns("tctRoutine").DefValue = lclsContr_LimCov.sRoutine
                        .Columns("hddsLimitCov").DefValue = lclsContr_LimCov.sLimitCov
                    End If
                End With
                Response.Write(mobjGrid.DoRow)
            Next
		
        Else
            'Se verifica el campo sLimitCov en la tabla Contr_pro
            lclsContrproc = New eCoReinsuran.Contrproc
		
            Call lclsContrproc.insPreCR301(CInt(Request.QueryString.Item("nMainAction")), Session("nNumber"), Session("nType"), Session("nBranch_rei"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		
            mobjGrid.Columns("hddsLimitCov").DefValue = lclsContrproc.sLimitCov
            lclsContrproc = Nothing
        End If
	
	'Si el campo no tiene valor entonces se desabilitan los botones de agregar y eliminar.
	If mobjGrid.Columns("hddsLimitCov").DefValue = "2" Then
		mobjGrid.AddButton = False
		mobjGrid.DeleteButton = False
	End If
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            		
	Response.Write(mobjGrid.CloseTable())
	lclsContr_LimCov = Nothing
End Sub

'% DoFormCR724Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías participantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub DoFormCR724Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lclsContr_LimCov As eCoReinsuran.Contr_LimCov
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			lclsContr_LimCov = New eCoReinsuran.Contr_LimCov
			
			lblnPost = lclsContr_LimCov.PostCR724(3, Session("nNumber"), Session("nBranch_rei"), Session("nType"), mobjValues.StringToType(Request.QueryString.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.StrNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, Session("nUsercode"), eRemoteDB.Constants.intNull)
			If lblnPost Then
				Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR724", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
			End If
		End With
		lclsContr_LimCov = Nothing
	Else
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR724", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
	End If
End Sub

</script>
<%Response.Expires = -1


'- Objeto para el manejo de las funciones generales de carga de valores
mobjValues = New eFunctions.Values

'- Objeto para el manejo de las rutinas del menú
mobjMenu = New eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
mobjGrid = New eFunctions.Grid

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR724", "CR724.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

mobjValues.sCodisplPage = "CR724"
mobjGrid.sCodisplPage = "CR724"

%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())%>
<SCRIPT>
document.VssVesion="$$Revision: 2 $|$$Date: 25-08-09 1:19 $|$$Author: Mpalleres $"

//%EnabledFields: Si el campo límite esta lleno, deshabilita los campos "Cobertura relacionada y % de cobertura relacionada."
//--------------------------------------------------------------------------------------------------------------------------------------
function EnabledFields(nField)
//--------------------------------------------------------------------------------------------------------------------------------------
{
	if (nField > '')
	{
		self.document.forms[0].valRelatedCover.disabled = true;
		self.document.forms[0].btnvalRelatedCover.disabled = true;
		self.document.forms[0].tcnPercentage_RelatedCover.disabled = true;
		self.document.forms[0].tcnPercentage_RelatedCover.value='';
		self.document.forms[0].valRelatedCover.value='';
		UpdateDiv('valRelatedCoverDesc','');
	}
	else
	{
		if(self.document.forms[0].valCover.value > 0)
		{
			self.document.forms[0].valRelatedCover.disabled = false;
			self.document.forms[0].btnvalRelatedCover.disabled = false;
			self.document.forms[0].tcnLimit.value = VTFormat(self.document.forms[0].tcnLimit.value, '', '', '', 2);
		}

		self.document.forms[0].tcnPercentage_RelatedCover.disabled = false;	
	}
}
//%VerifyCover: El codigo de la cobertura asociada no puede ser el mismo al de la cobertura.
//--------------------------------------------------------------------------------------------------------------------------------------
function VerifyCover(nField)
//--------------------------------------------------------------------------------------------------------------------------------------
{
	if(nField != 0)
	{
		if(self.document.forms[0].valCover.value==nField)
		{
		    alert('La cobertura relacionada debe ser diferente a la cobertura a la que se le define el límite.');
			self.document.forms[0].valRelatedCover.value='';
			UpdateDiv('valRelatedCoverDesc','');
		}
	}
}

//%EnabledRelatedCover: El codigo de la cobertura asociada no puede ser el mismo al de la cobertura.
//--------------------------------------------------------------------------------------------------------------------------------------
function EnabledRelatedCover(nField)
//--------------------------------------------------------------------------------------------------------------------------------------
{
	if (nField > 0)
	{
		if (self.document.forms[0].hddType.value != 2 && self.document.forms[0].hddType.value != 3 && 
		    self.document.forms[0].hddType.value != 5 && self.document.forms[0].hddType.value != 6 && 
		    self.document.forms[0].hddType.value != 7 && self.document.forms[0].hddType.value != 8)
		{
			self.document.forms[0].valRelatedCover.disabled = false;
			self.document.forms[0].btnvalRelatedCover.disabled = false;		
		}
	}
	else
	{

		self.document.forms[0].valRelatedCover.disabled = false;
		self.document.forms[0].btnvalRelatedCover.disabled = false;		
	}
}

//--------------------------------------------------------------------------------------------------------------------------------------
function ChangeType(nField)
//--------------------------------------------------------------------------------------------------------------------------------------
{
	switch(nField)
	{
		case "0":
		{
			self.document.forms[0].valCover.disabled = true;
			self.document.forms[0].btnvalCover.disabled = true;
			self.document.forms[0].valCover.value='';
			UpdateDiv('valCoverDesc','');
			break;			
		}
		case "1":
		{
			self.document.forms[0].valCover.disabled = false;
			self.document.forms[0].btnvalCover.disabled = false;
			self.document.forms[0].elements['valCover'].sTabName = 'TabTab_GenCov_rei';
			self.document.forms[0].elements['valRelatedCover'].sTabName = 'TabTab_GenCov_rei';
			break;
		}

		case "2":
		{
			self.document.forms[0].valCover.disabled = false;
			self.document.forms[0].btnvalCover.disabled = false;
			self.document.forms[0].elements['valCover'].sTabName = 'Tabtab_lifcov_rei';
			self.document.forms[0].elements['valRelatedCover'].sTabName = 'Tabtab_lifcov_rei';
			break;
		}
	}
}

// insCalAmount: Calcula el porcentaje cedido y/o el importe límite
//-----------------------------------------------------------------------------------------
function InsCalLimits(Field){
//-----------------------------------------------------------------------------------------
	var nQuota_sha
	var nAmount 
	var nReten 	
	
	nQuota_sha = insConvertNumber(self.document.forms[0].tcnCuota_parte.value)
	nAmount    = insConvertNumber(self.document.forms[0].tcnAmount.value)
	nReten     = insConvertNumber(<%=Session("dblRetention")%>)

    if (nReten>0){
	    if (Field.value!=0 && !isNaN(nReten))
	    	if (Field.name=='tcnCuota_parte'){
	    		self.document.forms[0].tcnCuota_parte.value = VTFormat(nQuota_sha, '', '', '', 6, true)
	    		self.document.forms[0].tcnAmount.value = VTFormat((nReten * (nQuota_sha /100)), '', '', '', 0, true);
	    		self.document.forms[0].tcnAmount.IsReq=0;
	    		self.document.forms[0].tcnAmount.Alias="Corresponde al monto límite que puede amparar el contrato";
	    		self.document.forms[0].tcnAmount.HolePlace="015";
	    		self.document.forms[0].tcnAmount.DecimalPlace=06;
	    		self.document.forms[0].tcnAmount.ShowThousand="1";
	    		if (!ValNumber(self.document.forms[0].tcnAmount,".","'","false",0))
	    			self.document.forms[0].tcnCuota_parte.value='';			

			    if (nQuota_sha > 100){
			        alert('El porcentaje de cesion no puede ser superior a 100%');
			        self.document.forms[0].tcnCuota_parte.value='';
			        self.document.forms[0].tcnAmount.value='';
	    		}
	    	}else{
	    		self.document.forms[0].tcnCuota_parte.value = VTFormat(((nAmount * 100) / nReten), '', '', '', 6, true)
	    		self.document.forms[0].tcnCuota_parte.IsReq=0;
	    		self.document.forms[0].tcnCuota_parte.Alias="Porcentaje que se cede al contrato";
	    		self.document.forms[0].tcnCuota_parte.HolePlace="015";
	    		self.document.forms[0].tcnCuota_parte.DecimalPlace=06;
	    		self.document.forms[0].tcnCuota_parte.ShowThousand="1";
	    		if(!ValNumber(self.document.forms[0].tcnCuota_parte,".",",","false",6))
	    			self.document.forms[0].tcnAmount.value = '';
                if(nAmount > nReten){
                    alert('El porcentaje de cesion no puede ser superior a 100%');
			        self.document.forms[0].tcnCuota_parte.value='';
			        self.document.forms[0].tcnAmount.value='';
	    		}
	    	}
	    else
	    	if (Field.value==0){
	    		self.document.forms[0].tcnCuota_parte.value='';
	    		self.document.forms[0].tcnAmount.value='';
    		}
    }
}	
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCR725" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR724"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call DoFormCR724()
Else
	Response.Write("<TD><BR></TD>")
	Call DoFormCR724Upd()
End If
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





