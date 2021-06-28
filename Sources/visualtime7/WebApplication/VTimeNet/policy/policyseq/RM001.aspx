<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddPossiblesColumn(11686,"Descripción de Maquinaria", "valMachineCode", "Table9007", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  ,"Descripción de la máquina				")
		.AddNumericColumn(11687,"Año de Fabricación", "tcnFabYear", 4,  ,  ,"Año de fabricación de la máquina", False, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(11688,"Cantidad", "tcnQuantityMachine", 4,  ,  ,"Cantidad de máquinas del tipo especificado", False, 0)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = .ActionQuery
		.Top = 100
		.Height = 220
		.Width = 480
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valMachineCode").EditRecord = True
		.sDelRecordParam = "nMachineCode=' + marrArray[lintIndex].valMachineCode + '" & "&nFabYear='+ marrArray[lintIndex].tcnFabYear + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreRM001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreRM001()
	'------------------------------------------------------------------------------
	Dim lcolDetail_Machines As ePolicy.Detail_Machines
	Dim lclsDetail_Machine As Object
	Dim lclsMachine As ePolicy.Machine
	Dim lblnFound As Boolean
	
	lclsMachine = New ePolicy.Machine
	lblnFound = lclsMachine.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sCodispl"))
	
	lcolDetail_Machines = New ePolicy.Detail_Machines
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	If lcolDetail_Machines.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		With mobjGrid
			For	Each lclsDetail_Machine In lcolDetail_Machines
				.Columns("valMachineCode").DefValue = lclsDetail_Machine.nMachineCode
				If lclsDetail_Machine.nFabYear = 0 Then 'No se llenó el Año de Fabricación (si es Cero se considera que no se llenó) 
					.Columns("tcnFabYear").DefValue = "" 'Se muestra vacio el campo en la grilla
				Else
					.Columns("tcnFabYear").DefValue = lclsDetail_Machine.nFabYear
				End If
				.Columns("tcnQuantityMachine").DefValue = lclsDetail_Machine.nQuantityMachine
				Response.Write(.DoRow)
			Next lclsDetail_Machine
		End With
	End If
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsDetail_Machine = Nothing
	lcolDetail_Machines = Nothing
	lclsMachine = Nothing
End Sub

'% insPreRM001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreRM001Upd()
	'------------------------------------------------------------------------------
	Dim lclsDetail_Machine As ePolicy.Detail_Machine
	Dim liFabYear As Object
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsDetail_Machine = New ePolicy.Detail_Machine
			If String.IsNullOrEmpty(.QueryString("nFabYear")) Then
				liFabYear = 0
			Else
				liFabYear = .QueryString.Item("nFabYear")
			End If
			If lclsDetail_Machine.InsPostDetail_Machine(Request.QueryString.Item("sCodispl"), CInt(Request.QueryString.Item("nMainAction")), Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nMachineCode"), eFunctions.Values.eTypeData.etdInteger), liFabYear, 0, Session("nUserCode")) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		Else
			Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")) & "<BR>")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsDetail_Machine = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")

Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
'~End Body Block VisualTimer Utility   
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%> 
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">

<%With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenues = New eFunctions.Menues
		.Write(mobjMenues.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenues = Nothing
	End If
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST" ID="FORM" NAME="frmRM001" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreRM001()
Else
	Call insPreRM001Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








