<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsretentioncov As eCoReinsuran.Retentioncov
Dim mobjRetentioncovs As eCoReinsuran.Retentioncovs



'%insDefineHeader: Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "cr572"
	
	With mobjGrid
		.Codispl = "CR572"
		.Width = 420
		.Height = 365
		.Top = 170
	End With
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboInsur_areaColumnCaption"), "cboInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  , "Tipo de cobertura a tratar (Vida / No Vida)", False, "onChangecboInsur_area()")
		Call .AddPossiblesColumn(100657, GetLocalResourceObject("cboCoverColumnCaption"), "cboCover", "tabtab_lifcov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "onChangecbocboCover()", False,  , GetLocalResourceObject("cboCoverColumnToolTip"))
		mobjGrid.Columns("cboCover").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddNumericColumn(100657, GetLocalResourceObject("tcnRetentionColumnCaption"), "tcnRetention", 18, CStr(0),  , GetLocalResourceObject("tcnRetentionColumnToolTip"), True, 6,  ,  , "onChangetcnRetention(this.value)")
		Call .AddTextColumn(100658, GetLocalResourceObject("tctRoutineColumnCaption"), "tctRoutine", 12, vbNullString,  , GetLocalResourceObject("tctRoutineColumnToolTip"),  ,  , "OnChangetctRoutine(this.value)")
		Call .AddPossiblesColumn(100659, GetLocalResourceObject("cboCovProporColumnCaption"), "cboCovPropor", "tabtab_lifcov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "OnChangecboCovPropor(this.value)", False,  , GetLocalResourceObject("cboCovProporColumnToolTip"))
		mobjGrid.Columns("cboCovPropor").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddNumericColumn(100660, GetLocalResourceObject("tcnComlimColumnCaption"), "tcnComlim", 18, CStr(0),  , GetLocalResourceObject("tcnComlimColumnToolTip"), True, 6,  ,  , "OnChangeComlim()")
		Call .AddPossiblesColumn(100661, GetLocalResourceObject("cboCoverCLColumnCaption"), "cboCoverCL", "tabtab_lifcov_rei", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "OnChangeComlim(this.value)", False,  , GetLocalResourceObject("cboCoverCLColumnToolTip"))
		mobjGrid.Columns("cboCoverCL").Parameters.Add("nBranch_rei", Session("nBranch_rei"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
	End With
	
	With mobjGrid
		.Columns("cboInsur_area").BlankPosition = False
		.DeleteButton = True
		.AddButton = True
		.Columns("cboInsur_area").EditRecord = True
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "cboInsur_area='+ marrArray[lintIndex].cboInsur_area + '&cboCover='+ marrArray[lintIndex].cboCover + '&tcnRetention='+ marrArray[lintIndex].tcnRetention + '&tctRoutine='+ marrArray[lintIndex].tctRoutine + '&cboCovPropor='+ marrArray[lintIndex].cboCovPropor + '&tcnComlim='+ marrArray[lintIndex].tcnComlim + '&cboCoverCL='+ marrArray[lintIndex].cboCoverCL + '"
	End With
End Sub

'% insPreCR007: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCR572()
	'--------------------------------------------------------------------------------------------
	
	Dim lblnFind As Boolean
	Dim lintCount As Object
	
	lblnFind = mobjRetentioncovs.Find(mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"))
	
	For	Each mclsretentioncov In mobjRetentioncovs
		With mobjGrid
			.Columns("cboInsur_area").DefValue = CStr(mclsretentioncov.nInsur_area)
			.Columns("cboCover").DefValue = CStr(mclsretentioncov.nCovergen)
			.Columns("tcnRetention").DefValue = CStr(mclsretentioncov.nRetention)
			If mclsretentioncov.nCovpropor <> eRemoteDB.Constants.intNull Then
				.Columns("cboCovPropor").DefValue = CStr(mclsretentioncov.nCovpropor)
			Else
				.Columns("cboCovPropor").DefValue = ""
			End If
			.Columns("tcnComlim").DefValue = CStr(mclsretentioncov.nComblim)
			
			If mclsretentioncov.nCovercl <> eRemoteDB.Constants.intNull Then
				.Columns("cboCoverCL").DefValue = CStr(mclsretentioncov.nCovercl)
			Else
				.Columns("cboCoverCL").DefValue = ""
			End If
			.Columns("tctRoutine").DefValue = mclsretentioncov.sRoutine
			.Columns("tcnComLim").DefValue = CStr(mclsretentioncov.nComblim)
			
			
			If mclsretentioncov.nInsur_area = 1 Then
				.Columns("cboCover").TableName = "tabtab_gencov_rei"
				.Columns("cboCovPropor").TableName = "tabtab_gencov_rei"
				.Columns("cboCoverCL").TableName = "tabtab_gencov_rei"
			Else
				.Columns("cboCover").TableName = "tabtab_lifcov_rei"
				.Columns("cboCovPropor").TableName = "tabtab_lifcov_rei"
				.Columns("cboCoverCL").TableName = "tabtab_lifcov_rei"
			End If
		End With
		Response.Write(mobjGrid.DoRow())
	Next mclsretentioncov
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
End Sub

'% insPreCR572Upd: Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías partocipantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCR572Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lintSel As Byte
	Dim lstrAction As String
	
	If Request.QueryString.Item("Action") = "Del" Or Request.QueryString.Item("Action") = "Delete" Then
		lintSel = 2
		lstrAction = "Del"
		Response.Write(mobjValues.ConfirmDelete())
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR572", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		
		With Request
			lblnPost = mclsretentioncov.InspostCR572Upd(lstrAction, mobjValues.StringToType(.QueryString.Item("cboInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cboCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("tctRoutine"), mobjValues.StringToType(.QueryString.Item("cboCovPropor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnComlim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cboCoverCL"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End With
	Else
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR572", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		If Request.QueryString.Item("Action") <> "Del" And Request.QueryString.Item("Action") <> "Delete" And Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>self.document.forms[0].elements['cboInsur_area'].disabled=true;</" & "Script>")
			Response.Write("<SCRIPT>self.document.forms[0].elements['btncboCover'].disabled=true;</" & "Script>")
			Response.Write("<SCRIPT>self.document.forms[0].elements['cboCover'].disabled=true;</" & "Script>")
			Response.Write("<SCRIPT>$(self.document.forms[0].elements['tctRoutine']).change();</" & "Script>")
		End If
	End If
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mobjRetentioncovs = New eCoReinsuran.Retentioncovs
mclsretentioncov = New eCoReinsuran.Retentioncov

mobjValues.sCodisplPage = "cr572"

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR572", "CR572.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>

//% onChangecboInsur_area: Asigna los SPs respectivos a los campos cboCover, cboCovPropor y
//%                        cboCoverCL dependiendo del área de seguro seleccionada por el usuario
//---------------------------------------------------------------------------------------
function onChangecboInsur_area()
//---------------------------------------------------------------------------------------
{
	if (self.document.forms[0].cboInsur_area.value == 1)
	{
	    self.document.forms[0].cboCover.sTabName='tabtab_gencov_rei';
	    self.document.forms[0].cboCovPropor.sTabName='tabtab_gencov_rei';
	    self.document.forms[0].cboCoverCL.sTabName='tabtab_gencov_rei';
	}
	else
	{
		self.document.forms[0].cboCover.sTabName='tabtab_lifcov_rei';
		self.document.forms[0].cboCovPropor.sTabName='tabtab_lifcov_rei';
		self.document.forms[0].cboCoverCL.sTabName='tabtab_lifcov_rei';
	}
}

//% onChangecbocboCover:
//---------------------------------------------------------------------------------------
function onChangecbocboCover()
//---------------------------------------------------------------------------------------
{
	self.document.forms[0].cboCovPropor.TypeList='2';
	self.document.forms[0].cboCovPropor.List=self.document.forms[0].cboCover.value;
	self.document.forms[0].cboCoverCL.TypeList='2';
	self.document.forms[0].cboCoverCL.List=self.document.forms[0].cboCover.value;
}    

//% onChangetcnRetention: Se desactivan algunos campos de la ventana
//---------------------------------------------------------------------------------------
function onChangetcnRetention(nRetention)
//---------------------------------------------------------------------------------------
{
	if(nRetention!="" && nRetention>0)
	{
		self.document.forms[0].tctRoutine.value ='';
		self.document.forms[0].tctRoutine.disabled =true;
		self.document.forms[0].cboCovPropor.value ='';
		self.document.forms[0].cboCovPropor.disabled =true;
		self.document.forms[0].btncboCovPropor.disabled =true;
		self.document.forms[0].tcnComlim.value ='';
		self.document.forms[0].tcnComlim.disabled =true;
		self.document.forms[0].cboCoverCL.value ='';
		self.document.forms[0].cboCoverCL.disabled =true;
		self.document.forms[0].btncboCoverCL.disabled =true;
	}
	else
	{
		self.document.forms[0].tctRoutine.disabled =false;
		self.document.forms[0].cboCovPropor.disabled =false;
		self.document.forms[0].btncboCovPropor.disabled =false;
		self.document.forms[0].tcnComlim.disabled =false;
		self.document.forms[0].cboCoverCL.disabled =false;
		self.document.forms[0].btncboCoverCL.disabled =false;
	}
}

//% OnChangetctRoutine: Se activan o desactivan algunos campos de la ventana dependiendo
//%                     del valor del parámetro "sRoutine"
//---------------------------------------------------------------------------------------
function OnChangetctRoutine(sRoutine)
//---------------------------------------------------------------------------------------
{
	if(sRoutine!='')
	{
		self.document.forms[0].tcnRetention.value ='';
		self.document.forms[0].tcnRetention.disabled =true;

		self.document.forms[0].cboCovPropor.value ='';
		self.document.forms[0].cboCovPropor.disabled =true;
		self.document.forms[0].btncboCovPropor.disabled =true;
		
		self.document.forms[0].tcnComlim.value ='';
		self.document.forms[0].tcnComlim.disabled =true;
		
		self.document.forms[0].cboCoverCL.value ='';
		self.document.forms[0].cboCoverCL.disabled =true;
		self.document.forms[0].btncboCoverCL.disabled =true;
	}
	else
	{
		self.document.forms[0].tcnRetention.disabled =false;

		self.document.forms[0].cboCovPropor.disabled =false;
		self.document.forms[0].btncboCovPropor.disabled =false;
		
		self.document.forms[0].tcnComlim.disabled =false;
		
		self.document.forms[0].cboCoverCL.disabled =false;
		self.document.forms[0].btncboCoverCL.disabled =false;
	}
}

//% OnChangecboCovPropor: Se desactivan algunos campos de la ventana
//---------------------------------------------------------------------------------------
function OnChangecboCovPropor(nCover)
//---------------------------------------------------------------------------------------
{
	if(nCover!="" && nCover>0)
	{
		self.document.forms[0].tctRoutine.value ='';
		self.document.forms[0].tctRoutine.disabled =true;
		self.document.forms[0].tcnRetention.value ='';
		self.document.forms[0].tcnRetention.disabled =true;
		self.document.forms[0].tcnComlim.value ='';
		self.document.forms[0].tcnComlim.disabled =true;
		self.document.forms[0].cboCoverCL.value ='';
		self.document.forms[0].cboCoverCL.disabled =true;
		self.document.forms[0].btncboCoverCL.disabled =true;
	}
	else
	{
		self.document.forms[0].tctRoutine.disabled =false;
		self.document.forms[0].tcnRetention.disabled =false;
		self.document.forms[0].tcnComlim.disabled =false;
		self.document.forms[0].cboCoverCL.disabled =false;
		self.document.forms[0].btncboCoverCL.disabled =false;
	}
	
	if((self.document.forms[0].elements['cboCovPropor'].value!="" && self.document.forms[0].elements['cboCovPropor'].value>0) &&
		(self.document.forms[0].elements['cboCover'].value!="" && self.document.forms[0].elements['cboCovPropor'].value>0) &&
		(self.document.forms[0].elements['cboCovPropor'].value==self.document.forms[0].elements['cboCover'].value))
	{
		alert('Las coberturas no pueden ser iguales');
		self.document.forms[0].elements['cboCovPropor'].value='';
		UpdateDiv('cboCovProporDesc','','Normal');
	}
		
}

//% OnChangeComlim: Se desactivan algunos campos de la ventana
//---------------------------------------------------------------------------------------
function OnChangeComlim(nLimit)
//---------------------------------------------------------------------------------------
{
	if(nLimit!="" && nLimit>0)
	{
		self.document.forms[0].tctRoutine.value ='';
		self.document.forms[0].tctRoutine.disabled =true;
		self.document.forms[0].tcnRetention.value ='';
		self.document.forms[0].tcnRetention.disabled =true;
		self.document.forms[0].cboCovPropor.value ='';
		self.document.forms[0].cboCovPropor.disabled =true;
		self.document.forms[0].btncboCovPropor.disabled =true;
	}
	else
	{
		self.document.forms[0].tctRoutine.disabled =false;
		self.document.forms[0].tcnRetention.disabled =false;
		self.document.forms[0].cboCovPropor.disabled =false;
		self.document.forms[0].btncboCovPropor.disabled =false;
	}
}
    
//% InsPreCR572: Se inicializan los campos de la ventana
//---------------------------------------------------------------------------------------
function InsPreCR572()
//---------------------------------------------------------------------------------------
{
    if (self.document.forms[0].cboInsur_area.value == 1) 
	{
		self.document.forms[0].cboCover.sTabName='tabtab_gencov_rei';
		self.document.forms[0].cboCovPropor.sTabName='tabtab_gencov_rei';
		self.document.forms[0].cboCoverCL.sTabName='tabtab_gencov_rei';
	}
	else
	{
		self.document.forms[0].cboCover.sTabName='tabtab_lifcov_rei';
		self.document.forms[0].cboCovPropor.sTabName='tabtab_lifcov_rei';
		self.document.forms[0].cboCoverCL.sTabName='tabtab_lifcov_rei';
	}

	if('<%=Request.QueryString.Item("nMainAction")%>' == '302')
    {
       self.document.forms[0].cboInsur_area.disabled =true;
       self.document.forms[0].cboCover.disabled =true;
       self.document.forms[0].btncboCover.disabled=true;
    }
}
	
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCR572" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR572"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call insPreCR572()
Else
	Call insPreCR572Upd()
	If Request.QueryString.Item("Action") <> "Del" And Request.QueryString.Item("Action") <> "Delete" Then
		%><SCRIPT>InsPreCR572()</SCRIPT><%		
	End If
End If
%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.59 $"     
</SCRIPT>
</FORM>
</BODY>
</HTML>





