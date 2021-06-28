<%@ Page explicit="true" %>
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
		.Height = 350
		.Top = 120
	End With
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("ValInsur_areaColumnCaption"), "ValInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "ChangeType(this.value);", False,  , GetLocalResourceObject("ValInsur_areaColumnToolTip"))
		'+ Si la variable de sesión "nBranch_rei" es igual a cualquiera de los ramos asociados a VIDA,
		'+ se ejecuta la lectura del SP "TabTab_lifCov_rei", de lo contrario, se ejecuta el SP "TabTab_GenCov_rei" - ACM - 15/01/2003
		If Session("nBranch_rei") = 1 Or Session("nBranch_rei") = 2 Or Session("nBranch_rei") = 3 Or Session("nBranch_rei") = 4 Or Session("nBranch_rei") = 6 Or Session("nBranch_rei") = 40 Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabTab_lifCov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("valCoverColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabTab_GenCov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("valCoverColumnToolTip"))
		End If
		mobjGrid.Columns("valCover").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .AddTextColumn(0, GetLocalResourceObject("tctRoutineColumnCaption"), "tctRoutine", 12, "",  , GetLocalResourceObject("tctRoutineColumnToolTip"),  ,  , "EnabledFields(this);")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFirstYearColumnCaption"), "tcnFirstYear", 4, "",  , GetLocalResourceObject("tcnFirstYearColumnToolTip"),  , 2,  ,  , "EnabledRutine(this);")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnNextYearColumnCaption"), "tcnNextYear", 4, "",  , GetLocalResourceObject("tcnNextYearColumnToolTip"),  , 2,  ,  , "EnabledRutine(this);")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPermExpColumnCaption"), "tcnPermExp", 4, "",  , GetLocalResourceObject("tcnPermExpColumnToolTip"),  , 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTempExpColumnCaption"), "tcnTempExp", 4, "",  , GetLocalResourceObject("tcnTempExpColumnToolTip"),  , 2)
		
		Call .AddHiddenColumn("hddsCommCov", "")
		'% Variable auxiliar
		Call .AddHiddenColumn("hddType", Session("nType"))
	End With
	
	With mobjGrid
		.DeleteButton = True
		.AddButton = True
		.Columns("ValCover").EditRecord = True
		.WidthDelete = 450
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("ValInsur_area").Disabled = True
		End If
		
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "ValInsur_area='+ marrArray[lintIndex].ValInsur_area + '" & "&valCover='+ marrArray[lintIndex].valCover + '"
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% DoFormCR731: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub DoFormCR731()
	'--------------------------------------------------------------------------------------------
	Dim lclsContr_comm As eCoReinsuran.Contr_comm
	Dim lcolContr_comms As eCoReinsuran.Contr_comms
	Dim lclsContr_comm1 As Object
	Dim lclsContrproc As eCoReinsuran.Contrproc
	Dim lstrComm_cover As String
	
	lclsContr_comm = New eCoReinsuran.Contr_comm
	lcolContr_comms = New eCoReinsuran.Contr_comms
	lclsContrproc = New eCoReinsuran.Contrproc
	
	If lcolContr_comms.Find(Session("nNumber"), Session("nBranch_rei"), Session("nType"), Session("dEffecdate")) Then
		For	Each lclsContr_comm In lcolContr_comms
			With mobjGrid
				.Columns("ValInsur_area").DefValue = CStr(lclsContr_comm.nInsur_area)
				.Columns("valCover").DefValue = CStr(lclsContr_comm.nCovergen)
				.Columns("tctRoutine").DefValue = lclsContr_comm.sRoutine
				.Columns("tcnFirstYear").DefValue = CStr(lclsContr_comm.nFirstYear)
				.Columns("tcnNextYear").DefValue = CStr(lclsContr_comm.nNextYear)
				.Columns("tcnPermExp").DefValue = CStr(lclsContr_comm.nPermExp)
				.Columns("tcnTempExp").DefValue = CStr(lclsContr_comm.nTempexp)
				.Columns("hddsCommCov").DefValue = lclsContr_comm.sCommCov
				lstrComm_cover = lclsContr_comm.sCommCov
			End With
			Response.Write(mobjGrid.DoRow)
		Next lclsContr_comm
	Else
		'Se verifica el campo sCommCov en la tabla Contr_pro
		Call lclsContrproc.Find(Session("nNumber"), Session("nType"), Session("nBranch"), Session("dEffecdate"), True)
		mobjGrid.Columns("hddsCommCov").DefValue = lclsContrproc.sCommCov
	End If
	If (lstrComm_cover = vbNullString Or lstrComm_cover = "2") And (lclsContrproc.sCommCov = vbNullString Or lclsContrproc.sCommCov = "2") Then
		mobjGrid.DeleteButton = False
		mobjGrid.AddButton = False
	End If
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)
	Response.Write(mobjGrid.CloseTable())
	
	lclsContr_comm = Nothing
	lcolContr_comms = Nothing
	lclsContr_comm1 = Nothing
	lclsContrproc = Nothing
	
End Sub

'% DoFormCR731Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías participantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub DoFormCR731Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lclsContr_comm As eCoReinsuran.Contr_comm
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			lclsContr_comm = New eCoReinsuran.Contr_comm
			lblnPost = lclsContr_comm.InsPostCR731("Del", mobjValues.StringToType(Request.QueryString.Item("ValInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("ValCover"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.StrNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nNumber"), Session("nBranch_rei"), Session("nType"), Session("dEffecdate"), eRemoteDB.Constants.dtmNull, Session("nUsercode"))
			If lblnPost Then
				Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR731", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
			End If
		End With
		lclsContr_comm = Nothing
	Else
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR731", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
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
		.Write(mobjMenu.setZone(2, "CR731", "CR731.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())%>
<SCRIPT>
//'Variable para el control de versiones
	document.VssVesion="$$Revision: 2 $|$$Date: 15/10/03 16.59 $|$$Author: Nvaplat60 $"

//%EnabledFields: Si el campo rutina esta lleno, deshabilita los campos "% 1er Año y % años subsiguientes."
//--------------------------------------------------------------------------------------------------------------------------------------
function EnabledFields(oField)
//--------------------------------------------------------------------------------------------------------------------------------------
{
	if (oField.value > '0')
	{
		self.document.forms[0].tcnFirstYear.disabled = true;
		self.document.forms[0].tcnNextYear.disabled = true;
		self.document.forms[0].tcnFirstYear.value='';
		self.document.forms[0].tcnNextYear.value='';
	}
	else
	{
		self.document.forms[0].tcnFirstYear.disabled = false;
		self.document.forms[0].tcnNextYear.disabled = false;
		self.document.forms[0].tcnFirstYear.value='';
		self.document.forms[0].tcnNextYear.value='';
	}
}
//%EnabledRutine: cuando los campos de porcentaje 1er año o años subsiguientes tienen valor se desabilita el campo rutina
//--------------------------------------------------------------------------------------------------------------------------------------
function EnabledRutine(oField)
//--------------------------------------------------------------------------------------------------------------------------------------
{
	if (oField.value > '0')
	{
		self.document.forms[0].tctRoutine.disabled = true;
		self.document.forms[0].tctRoutine.value='';
	}
	else{
		if(self.document.forms[0].tcnNextYear.value == '')
		{
			self.document.forms[0].tctRoutine.disabled = false;
			self.document.forms[0].tctRoutine.value='';
		}
		if(self.document.forms[0].tcnFirstYear.value == '')
		{
			self.document.forms[0].tctRoutine.disabled = false;
			self.document.forms[0].tctRoutine.value='';
		}		
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
			break;
		}

		case "2":
		{
			self.document.forms[0].valCover.disabled = false;
			self.document.forms[0].btnvalCover.disabled = false;
			self.document.forms[0].elements['valCover'].sTabName = 'tabtab_lifcov_rei';
			break;
		}
	}
}

</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCR725" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR731"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call DoFormCR731()
Else
	Response.Write("<TD><BR></TD>")
	Call DoFormCR731Upd()
End If
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





