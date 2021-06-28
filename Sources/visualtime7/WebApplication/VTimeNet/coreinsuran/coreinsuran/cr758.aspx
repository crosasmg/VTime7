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

'- Se define la variable para la carga del Grid de la ventana 'CR758'
Dim mobjcontr_cumuls As eCoReinsuran.Contr_Cumuls

'- Se define la variable en que se carga la colección
Dim mclscontr_cumul As eCoReinsuran.Contr_Cumul


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "cr758"
	
	With mobjGrid
		.Codispl = "CR758" 'Request.QueryString("sCodispl")
		.Width = 420
		.Height = 200
		.Top = 170
	End With
	
	'+     
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valBranchColumnCaption"), "valBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "OnChangevalBranch(this)",  ,  , GetLocalResourceObject("valBranchColumnToolTip"))
		Call .AddPossiblesColumn(100657, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , True,  , GetLocalResourceObject("valProductColumnToolTip"))
		
		mobjGrid.Columns("valProduct").Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
	With mobjGrid
		.Columns("valBranch").BlankPosition = True
		.DeleteButton = True
		.AddButton = True
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "valBranch='+ marrArray[lintIndex].valBranch + '" & "&valProduct='+ marrArray[lintIndex].valProduct  + '"
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'%insPreCR758: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCR758()
	'--------------------------------------------------------------------------------------------
	
	Dim lblnFind As Boolean
	Dim lintCount As Object
	
	With mobjValues
		
		lblnFind = mobjcontr_cumuls.Find(.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
	For	Each mclscontr_cumul In mobjcontr_cumuls
		With mobjGrid
			.Columns("valBranch").DefValue = CStr(mclscontr_cumul.nBranch)
			.Columns("valProduct").DefValue = CStr(mclscontr_cumul.nProduct)
			mobjGrid.Columns("valProduct").Parameters.Add("nBranch", mclscontr_cumul.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Response.Write(mobjGrid.DoRow())
	Next mclscontr_cumul
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
	mclscontr_cumul = Nothing
	mobjcontr_cumuls = Nothing
End Sub

'% insPreCR758Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías partocipantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCR758Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lintSel As Byte
	
	If Request.QueryString.Item("Action") = "Del" Then
		lintSel = 2
		Response.Write(mobjValues.ConfirmDelete())
		
		With Request
			lblnPost = mclscontr_cumul.InspostCR758Upd(.QueryString.Item("Action"), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("ValBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("ValProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			If lblnPost Then
				Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR758", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
			End If
		End With
		
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No&nOpener=" & Request.QueryString.Item("sCodispl") & "&nContraType=" & Session("nType") & "&sCodispl_CR=" & Session("sCodispl_CR") & "&nNumber=" & Session("nNumber") & "&dContrDate=" & Session("dEffecdate") & """;</" & "Script>")
		
		mclscontr_cumul = Nothing
		mobjcontr_cumuls = Nothing
	Else
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR758", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mobjcontr_cumuls = New eCoReinsuran.Contr_Cumuls
mclscontr_cumul = New eCoReinsuran.Contr_Cumul

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR758", "CR758.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

mobjValues.sCodisplPage = "cr758"

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.59 $"
</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
function OnChangevalBranch(lcolumn){
   
    with (self.document.forms[0]){		
        valProduct.Parameters.Param1.sValue = lcolumn.value
        
        if(lcolumn.value!='' && lcolumn.value!=0){        
            if(sAction.value!="Update"){
                valProduct.disabled = false
	            btnvalProduct.disabled = false
	        }    
	    }    
	    
	    if(lcolumn.value=='' || lcolumn.value==0){
	        valProduct.value=''
	        $(valProduct).change();	
            valProduct.disabled = true
	        btnvalProduct.disabled = true	                
	    }
    }
}
	
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCR758" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

<%="<script>var sAction='" & Request.QueryString.Item("Action") & "'</script>"%>

<%

Response.Write(mobjValues.ShowWindowsName("CR758"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call insPreCR758()
Else
	Response.Write("<TD><BR></TD>")
	Call insPreCR758Upd()
End If
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






