<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: The Object to handling the load values general functions is defined
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores        
Dim mobjValues As eFunctions.Values

'**-Objetive: Definition of the object to handle the grid and its properties
'-Objetivo: Se define la variable para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'**-Objetive: The object to handling the page zones is defined
'-Objeto: para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'**%Objetive: Defines the columns of the grid 
'%Objetivo: Define las columnas del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'Dim eRemoteDB As Object
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid
		.sSessionID = Session.SessionID
		.sCodisplPage = Request.QueryString.Item("sCodispl")
	End With
	
	'**+ The columns of the grid are defined
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		.AddPossiblesColumn(17374,"Sucursal", "cbeOfficeAgen", "Table9", 1, "", False,  ,  ,  ,  , True,  ,"Sucursal asociada al documento de la linea en tratamiento.")
		.AddPossiblesColumn(17375,"Oficina", "cbeAgency", "Table5556", 1, "", False,  ,  ,  ,  , True,  ,"Oficina asociada al documento de la linea en tratamiento.")
		.AddPossiblesColumn(17376,"Intermediario", "valIntermedia", "TabIntermedia", 1, "", False,  ,  ,  ,  , True,  ,"Intermediario asociado al documento de la linea en tratamiento.")
		.AddClientColumn(17377,"Cliente", "tctClient", vbNullString,  ,"Cliente asociado al documento de la línea en tratamiento.")
		.AddPossiblesColumn(17378,"Tipo de documento", "valTypeDocument", "Table5026", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  ,  ,"Tipo de docuemento.",  ,  ,  ,  ,  , True)
		.AddTextColumn(17379,"Tipo de registro", "tctCertype", 30, vbNullString,  ,vbNullString)
		.AddPossiblesColumn(17380,"Ramo", "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,"Ramo asociado al documento de la línea en tratamiento.")
		.AddPossiblesColumn(17381,"Producto", "valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  ,"Producto asociado al documento de la línea en tratamiento.")
		.AddNumericColumn(17382,"Póliza/ Cotización", "tcnPolicy", 10, "",  ,"Póliza asociada al documento de la línea en tratamiento.",  ,  ,  ,  ,  , False)
		.AddNumericColumn(17383,"Certificado/ Cotización", "tcnCertif", 5, "",  ,"Certificado asociado al documento de la línea en tratamiento.",  ,  ,  ,  ,  , False)
		.AddNumericColumn(17384,"Factura", "tcnReceipt", 10, "",  ,"Factura asociada al documento de la línea en tratamiento.",  ,  ,  ,  ,  , False)
		.AddPossiblesColumn(17385,"Carta", "valLetterNum", "tabletters", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  ,  ,"Código y descripción de la carta asociada al documento.",  ,  ,  ,  ,  , True)
		.AddPossiblesColumn(17386,"Estado de Impresión", "valsPrintStatus", "Table5031", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  ,  ,"Estado del documento.",  ,  ,  ,  ,  , True)
		.AddAnimatedColumn(17387,"Ver documento", "btnLocation", "/VTimeNet/Images/btn_ValuesOff.png",vbNullString)
		
		.AddHiddenColumn("sAuxSel", "2")
		.AddHiddenColumn("hddsCertype", "")
		.AddHiddenColumn("hddnLettRequest", "")
		.AddHiddenColumn("hddnLetterNum", "")
		.AddHiddenColumn("hddsClient", "")
		.AddHiddenColumn("hddnBranch", "")
		.AddHiddenColumn("hddnProduct", "")
		.AddHiddenColumn("hddnPolicy", "")
		.AddHiddenColumn("hddnCertif", "")
		.AddHiddenColumn("hddnIntermedia", "")
		.AddHiddenColumn("hddnOfficeAgen", "")
		.AddHiddenColumn("hddnAgency", "")
		.AddHiddenColumn("hddsOfficialCer", "")
		.AddHiddenColumn("hddsAddress", "")
		.AddHiddenColumn("hddTypeDocument", "")
		.AddHiddenColumn("hddnReceipt", "")
		.AddHiddenColumn("hddnShipmentType", "")
		.AddHiddenColumn("hddnType", "")
		.AddHiddenColumn("hddnCodForm", "")
		.AddHiddenColumn("hddnConsec", "")
		.AddHiddenColumn("hddsDitribution", "")
		.AddHiddenColumn("hddnSituation", "")
		
	End With
	
	'**+ The general properties of the grid are defined
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = .ActionQuery
		
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("Sel").OnClick = "InsSelected(this.value, this.checked)"
		.AddButton = False
		.DeleteButton = False
		
		.Columns("valProduct").Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
End Sub

'%**Objetive: The controls of the page are load
'%Objetivo: Se cargan los controles de la página
'-------------------------------------------------------------------------------------------
Private Sub insPreLT500()
	'-------------------------------------------------------------------------------------------
	Dim lcolPrintDocumentss As Object
	Dim lclsPrintDocuments As Object
	Dim lstrCertype As String
	Dim lintindex As Short
	
	lintindex = 0
	lcolPrintDocumentss = New eLetter.PrintDocumentss
	With mobjGrid
		
		If lcolPrintDocumentss.Find(Request.QueryString.Item("nShipmentType"), Request.QueryString.Item("sTypeDocument"), Request.QueryString.Item("nOfficeAgen"), Request.QueryString.Item("nAgency"), Request.QueryString.Item("nIntermed"), Request.QueryString.Item("sClient"), Request.QueryString.Item("sCertype"), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), Request.QueryString.Item("nPolicy"), Request.QueryString.Item("nCertif"), Request.QueryString.Item("sStatusDocument")) Then
			
			For	Each lclsPrintDocuments In lcolPrintDocumentss
				'.Columns("Sel").Checked  = 1
				'.Columns("Sel").DefValue = 1
				.Columns("cbeOfficeAgen").DefValue = lclsPrintDocuments.nOfficeAgen
				.Columns("cbeAgency").DefValue = lclsPrintDocuments.nAgency
				.Columns("valIntermedia").DefValue = lclsPrintDocuments.nIntermed
				.Columns("tctClient").DefValue = lclsPrintDocuments.sClient
				.Columns("valTypeDocument").DefValue = lclsPrintDocuments.sTypeDocument
				.Columns("valsPrintStatus").DefValue = lclsPrintDocuments.sPrintStatus
				
				Select Case (lclsPrintDocuments.sCerType)
					Case "1"
						lstrCertype = "Solicitud"
					Case "2"
						lstrCertype = "Póliza"
					Case Else
						lstrCertype = "Cotización"
				End Select
				
				.Columns("tctCertype").DefValue = lstrCertype
				.Columns("cbeBranch").DefValue = lclsPrintDocuments.nBranch
				.Columns("valProduct").Parameters.Add("nBranch", lclsPrintDocuments.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = lclsPrintDocuments.nProduct
				.Columns("tcnPolicy").DefValue = lclsPrintDocuments.nPolicy
				.Columns("tcnCertif").DefValue = lclsPrintDocuments.nCertif
				.Columns("tcnReceipt").DefValue = lclsPrintDocuments.nReceipt
				.Columns("valLetterNum").DefValue = lclsPrintDocuments.nLetterNum
				.Columns("hddnLettRequest").DefValue = lclsPrintDocuments.nLettRequest
				'.Columns("hddsDitribution").DefValue = lclsPrintDocuments.sDitribution
				.Columns("hddnSituation").DefValue = lclsPrintDocuments.nSituation
				
				'+ Si el tipo de documento es una carta
				If lclsPrintDocuments.sTypeDocument = "3" Then
					.Columns("btnLocation").Disabled = False
				    mobjGrid.Columns("btnLocation").HRefScript = "insOpenDocument(" & lintindex & ")"
				Else
					.Columns("btnLocation").Disabled = True
				End If
				
				.Columns("hddnLettRequest").DefValue = lclsPrintDocuments.nLettRequest
				.Columns("hddnLetterNum").DefValue = lclsPrintDocuments.nLetterNum
				.Columns("hddsClient").DefValue = lclsPrintDocuments.sClient
				.Columns("hddsCertype").DefValue = lclsPrintDocuments.sCerType
				.Columns("hddnBranch").DefValue = lclsPrintDocuments.nBranch
				.Columns("hddnProduct").DefValue = lclsPrintDocuments.nProduct
				.Columns("hddnPolicy").DefValue = lclsPrintDocuments.nPolicy
				.Columns("hddnCertif").DefValue = lclsPrintDocuments.nCertif
				.Columns("hddnIntermedia").DefValue = lclsPrintDocuments.nIntermed
				.Columns("hddnOfficeAgen").DefValue = lclsPrintDocuments.nOfficeAgen
				.Columns("hddnAgency").DefValue = lclsPrintDocuments.nAgency
				.Columns("hddsOfficialCer").DefValue = lclsPrintDocuments.sOfficialCer
				.Columns("hddsAddress").DefValue = lclsPrintDocuments.sAddress
				.Columns("hddTypeDocument").DefValue = lclsPrintDocuments.sTypeDocument
				.Columns("hddnReceipt").DefValue = lclsPrintDocuments.nReceipt
				.Columns("hddnShipmentType").DefValue = lclsPrintDocuments.nShipmentType
				.Columns("hddnType").DefValue = lclsPrintDocuments.nType
				.Columns("hddnCodForm").DefValue = lclsPrintDocuments.nCodForm
				.Columns("hddnConsec").DefValue = lclsPrintDocuments.nConsecutive
				
				lintindex = lintindex + 1
				Response.Write(.DoRow)
			Next lclsPrintDocuments
			
		End If
	End With
	Response.Write(mobjGrid.closeTable)
	
	lclsPrintDocuments = Nothing
	lcolPrintDocumentss = Nothing
End Sub

'**%Objetive: The fields of the PopUp are defined
'%Objetivo: Se definen los campos de la PopUp del detalle
'-------------------------------------------------------------------------------------------
Private Sub insPreLT500Upd()
	'-------------------------------------------------------------------------------------------
	Dim lclsPrintDocuments As Object
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsPrintDocuments = New eLetter.PrintDocuments
			If lclsPrintDocuments.insPostLT500(.QueryString("Action"), Session("nUsercode")) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
			lclsPrintDocuments = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valLetter.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
'----------------------------------------------------------------------------------------------------
'**+Objective: 
'**+Version: $$Revision: $
'+Objetivo: 
'+Version: $$Revision: $
'----------------------------------------------------------------------------------------------------
Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))


mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Visual TIME Templates">
    <%=mobjValues.StyleSheet()%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>

//**-Objetive: This line keep the source safe version
//-Objeto: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 1 $|$$Date: 10/29/03 4:00p|$$Author: mgonzalez $"
//------------------------------------------------------------------------------------------

//**% SelectAll: 
//% SelectAll: 
//------------------------------------------------------------------------------------------		
function SelectAll(bValue)
//------------------------------------------------------------------------------------------		
{   
    var lintLength = marrArray.length

    if (lintLength==1){
        self.document.forms[0].Sel.checked=bValue;
        InsSelected(self.document.forms[0].Sel.value,bValue,'All')
    }
    else{
        for (nIndex = 0; nIndex < lintLength; nIndex++){
            self.document.forms[0].Sel[nIndex].checked=bValue;
            InsSelected(self.document.forms[0].Sel[nIndex].value,bValue,'All')
        }        
    }
}

var procSelection = true;

</SCRIPT>
<%
With Request
	mobjValues.ActionQuery = (.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
	
	If .QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		mobjMenu.sSessionID = Session.SessionID
		
		Response.Write(mobjMenu.setZone(2, "LT500", "LT500.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<SCRIPT>

//**% InsSelected:
//%InsSelected:
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------	
	if(document.forms[0].sAuxSel.length>0){
		document.forms[0].sAuxSel[nIndex].value =(bChecked?1:2);		
	}
	else{ 
		document.forms[0].sAuxSel.value =(bChecked?1:2);		
	}
}

//--------------------------------------------------------------------------------    
function insOpenDocument(lintIndex){
//--------------------------------------------------------------------------------    
    var lstrQueryString;
    var lstrAction;

    alert(lintIndex);
    alert(lintIndex.value);
    
    lstrAction = "<%=Request.QueryString.Item("Action")%>";

	lstrQueryString = "/VTimeNet/Letter/Letter/Variables.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&Type=Qry&Action=" + lstrAction + "&Location=" + "&nLetterNum=" + marrArray[lintIndex].valLetterNum + "&nLanguage=1" + "&nLettRequest=" + marrArray[lintIndex].hddnLettRequest;

    ShowPopUp(lstrQueryString,"Values", 425,400,"no","no", 100, 100);  
}

</SCRIPT>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="<%=Request.QueryString.Item("sCodispl")%>" ACTION="valLetter.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>
																								&nShipmentType=<%=Request.QueryString.Item("nShipmentType")%>
																								&sTypeDocument=<%=Request.QueryString.Item("sTypeDocument")%>
																								&nOfficeAgen=<%=Request.QueryString.Item("nOfficeAgen")%>
																								&nAgency=<%=Request.QueryString.Item("nAgency")%>
																								&nIntermed=<%=Request.QueryString.Item("nIntermed")%>
																								&sClient=<%=Request.QueryString.Item("sClient")%>
																								&sCertype=<%=Request.QueryString.Item("sCertype")%>
																								&nBranch=<%=Request.QueryString.Item("nBranch")%>
																								&nProduct=<%=Request.QueryString.Item("nProduct")%>
																								&nPolicy=<%=Request.QueryString.Item("nPolicy")%>
																								&nCertif=<%=Request.QueryString.Item("nCertif")%>">
<TD>
<%
    Response.Write(mobjValues.CheckControl("chkSelectAll","Seleccionar Todo", CStr(False),  , "SelectAll(this.checked);",  ,  ,"Permite la selección de todas las lineas de la consulta"))
%>
</TD>
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
    insDefineHeader()
    If Request.QueryString.Item("Type") <> "PopUp" Then
	    insPreLT500()
    Else
	    insPreLT500Upd()
    End If
    mobjGrid = Nothing
    mobjValues = Nothing

    mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
    mobjNetFrameWork = Nothing
%>
</FORM>
</BODY>
</HTML>



