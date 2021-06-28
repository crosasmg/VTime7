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

'- Se define la variable para la carga del Grid de la ventana 'CR307'
Dim mclsPart_contr As eCoReinsuran.Part_contr


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "cr307"
	
	With mobjGrid
		.Codispl = "CR307"
		.Width = 400
		.Height = 560
		.Top = 170
	End With
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddCheckColumn(0, GetLocalResourceObject("opcPartCiaCedenteColumnCaption"), "opcPartCiaCedente", "",  ,  , "OnCheckPartCiaCedente()", False)
		Call .AddCompanyColumn(0, GetLocalResourceObject("valCompanyColumnCaption"), "valCompany", vbNullString,  , GetLocalResourceObject("valCompanyColumnToolTip"), "OnClasific()",  , "tctCompanyName", False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnClasificColumnCaption"), "tcnClasific", "Table5563", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnClasificColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnShareColumnCaption"), "tcnShare", 5,  ,  , GetLocalResourceObject("tcnShareColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 4,  ,  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnComisionColumnCaption"), "tcnComision", 4,  ,  , GetLocalResourceObject("tcnComisionColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnArr_perdColumnCaption"), "tcnArr_perd", 4,  ,  , GetLocalResourceObject("tcnArr_perdColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRate_beneColumnCaption"), "tcnRate_bene", 4,  ,  , GetLocalResourceObject("tcnRate_beneColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPr_inOutColumnCaption"), "tcnPr_inOut", 4,  ,  , GetLocalResourceObject("tcnPr_inOutColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCl_inOutColumnCaption"), "tcnCl_inOut", 4,  ,  , GetLocalResourceObject("tcnCl_inOutColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRate_fixColumnCaption"), "tcnRate_fix", 9,  ,  , GetLocalResourceObject("tcnRate_fixColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_fixColumnCaption"), "tcnAmount_fix", 18,  ,  , GetLocalResourceObject("tcnAmount_fixColumnToolTip"), True, 2)
            Call .AddTextColumn(0, GetLocalResourceObject("tctRoucessColumnCaption"), "tctRoucess", 12, "", , GetLocalResourceObject("tctRoucessColumnToolTip"), , , "")
            Call .AddTextColumn(0, GetLocalResourceObject("tctRouprofitColumnCaption"), "tctRouprofit", 12, "", , GetLocalResourceObject("tctRouprofitColumnToolTip"), , , "")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmoProfitColumnCaption"), "tcnAmoProfit", 18, , , GetLocalResourceObject("tcnAmoProfitColumnToolTip"), True, 2)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeFreqProfitColumnCaption"), "cbeFreqProfit", "Table97", 1, 0, False, , , , , , , GetLocalResourceObject("cbeFreqProfitColumnToolTip"))
            
            Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Columns("valCompany").EditRecord = True
		.DeleteButton = True
		.AddButton = True
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("valCompany").Disabled = True
			.Columns("opcPartCiaCedente").Disabled = True
		End If
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("opcPartCiaCedente").Disabled = True
		Else
			If Request.QueryString.Item("Action") <> "Add" Then
				.Columns("opcPartCiaCedente").Disabled = True
			Else
				.Columns("opcPartCiaCedente").Disabled = False
			End If
		End If
		
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'%insPreCR007: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCR307()
	'--------------------------------------------------------------------------------------------
	
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	With mobjValues
		
		lblnFind = mclsPart_contr.Find(Session("sCodispl_CR"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), Session("dEffecdate"))
	End With
	
	If lblnFind Then
		lintCount = 0
		For lintCount = 0 To mclsPart_contr.Count - 1
			If mclsPart_contr.ItemCR307(lintCount) Then
				With mobjGrid
					.Columns("Sel").DefValue = CStr(mclsPart_contr.nSel)
					.Columns("valCompany").DefValue = CStr(mclsPart_contr.nCompany)
					.Columns("tcnClasific").DefValue = CStr(mclsPart_contr.nClasific)
					.Columns("tcnShare").DefValue = CStr(mclsPart_contr.nShare)
					.Columns("tcnRate").DefValue = CStr(mclsPart_contr.nRate)
					.Columns("tcnComision").DefValue = CStr(mclsPart_contr.nComision)
					.Columns("tcnArr_perd").DefValue = CStr(mclsPart_contr.nArr_perd)
					.Columns("tcnRate_bene").DefValue = CStr(mclsPart_contr.nRate_bene)
					.Columns("tcnPr_inOut").DefValue = CStr(mclsPart_contr.nPr_inOut)
					.Columns("tcnCl_inOut").DefValue = CStr(mclsPart_contr.nCl_inOut)
					
					.Columns("tcnRate_fix").DefValue = CStr(mclsPart_contr.nCessrate)
					.Columns("tcnAmount_fix").DefValue = CStr(mclsPart_contr.nCessprfix)
					.Columns("tctRoucess").DefValue = mclsPart_contr.sRoucess
					
                        .Columns("tctRouprofit").DefValue = mclsPart_contr.sRouProfit
                        .Columns("tcnAmoProfit").DefValue = CStr(mclsPart_contr.nAmountProfit)
                        .Columns("cbeFreqProfit").DefValue = mclsPart_contr.sfreqProfit
                        
                    '+ Se "arma" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostCR307 cuando se eliminen los registros seleccionados - VCVG - 07/06/2001
					.Columns("sParam").DefValue = "nCompany=" & mclsPart_contr.nCompany & "&nShare=" & mclsPart_contr.nShare & "&nRate=" & mclsPart_contr.nRate & "&nComision=" & mclsPart_contr.nComision & "&nArr_perd=" & mclsPart_contr.nArr_perd & "&nRate_bene=" & mclsPart_contr.nRate_bene & "&nPr_inOut=" & mclsPart_contr.nPr_inOut & "&nCl_inOut=" & mclsPart_contr.nCl_inOut
					
				End With
				Response.Write(mobjGrid.DoRow())
				
				
			End If
		Next 
	End If
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
End Sub

'% insPreCR307Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías partocipantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCR307Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lintSel As Integer
	
	If Request.QueryString.Item("Action") = "Del" Then
		lintSel = 2
		
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR307", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
		
		With Request
                lblnPost = mclsPart_contr.insPostCR307("CR307", Session("sCodispl_CR"), lintSel, Session("nUsercode"), Session("dEffecdate"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), CInt(.QueryString.Item("nCompany")), CDbl(.QueryString.Item("nShare")), CDbl(.QueryString.Item("nRate")), CDbl(.QueryString.Item("nComision")), CInt(.QueryString.Item("nArr_perd")), CDbl(.QueryString.Item("nRate_bene")), CDbl(.QueryString.Item("nPr_inOut")), CDbl(.QueryString.Item("nCl_inOut")), CDbl(.QueryString.Item("tcnRate_fix")), CDbl(.QueryString.Item("tcnAmount_fix")), .QueryString.Item("tctRoucess"), .QueryString.Item("tctRouProfit"), CDbl(.QueryString.Item("tcnAmoProfit")), .QueryString.Item("cbeFreqProfit"))
		End With
		If lblnPost Then
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No&nOpener=" & Request.QueryString.Item("sCodispl") & """;</" & "Script>")
		End If
		
	Else
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR307", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mclsPart_contr = New eCoReinsuran.Part_contr

mobjValues.sCodisplPage = "cr307"

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR307", "CR307.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

%>
<SCRIPT>
//- Variable para el control de versiones.
document.VssVersion="$$Revision: 2 $|$$Date: 27/03/06 19:34 $|$$Author: Vvera $"
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>

<SCRIPT>
//OnCheckPartCiaCedente: Función que activa o desactiva los campos de acuerdo al check de 
//                   De Participación de la compañia cedente o principal.     
//-----------------------------------------------------------------------------------------
function OnCheckPartCiaCedente() {
//-----------------------------------------------------------------------------------------

    if (self.document.forms[0].opcPartCiaCedente.checked)
        {
		self.document.forms[0].valCompany.disabled=true;
	    insDefValues("Company", "CompanyName=" + self.document.forms[0].valCompany.value, '/VTimeNet/coreinsuran/coreinsuran');
        }
    else
        {
		self.document.forms[0].valCompany.disabled=false;
		self.document.forms[0].valCompany.value="";
		self.document.forms[0].tcnClasific.value="";
        }
    }

//OnClasific: Función que rescata la clasificacion de la compañia     
//-----------------------------------------------------------------------------------------
function OnClasific() {
//-----------------------------------------------------------------------------------------
	if (self.document.forms[0].valCompany.value!='')
	    insDefValues("Clasific", "nCompany=" + self.document.forms[0].valCompany.value, '/VTimeNet/coreinsuran/coreinsuran');
    }

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCR307" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR307"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call insPreCR307()
Else
	Call insPreCR307Upd()
End If
%>
</FORM>
</BODY>
</HTML>
	




