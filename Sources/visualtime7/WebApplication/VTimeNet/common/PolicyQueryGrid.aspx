<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de funciones de los objetos generales
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen las características del grid
'--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        mobjGrid.sCodisplPage = "GE010"
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCompanyColumnCaption"), "cbeCompany", "Table5638", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeCompanyColumnToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePolitypeColumnCaption"), "cbePolitype", "Table17", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbePolitypeColumnCaption"))
            Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct")
            Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "cbeBranch")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString, , GetLocalResourceObject("tcnPolicyColumnToolTip"), False, 0)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, vbNullString, , GetLocalResourceObject("tcnCertifColumnToolTip"), False, 0)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatus_polColumnCaption"), "cbeStatus_pol", "Table181", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeStatus_polColumnToolTip"))
            Call .AddClientColumn(0, GetLocalResourceObject("dtcClientCColumnCaption"), "dtcClientC", vbNullString, False, GetLocalResourceObject("dtcClientCColumnToolTip"))
            Call .AddClientColumn(0, GetLocalResourceObject("dtcClientAColumnCaption"), "dtcClientA", vbNullString, False, GetLocalResourceObject("dtcClientAColumnToolTip"))
            Call .AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate", vbNullString, , GetLocalResourceObject("tcdStartdateColumnToolTip"))
            Call .AddHiddenColumn("hddDigit", CStr(0))
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "GE010"
            .AddButton = False
            .DeleteButton = False
            .ActionQuery = False
            .Columns("Sel").GridVisible = False
        End With
    End Sub

'% ShowData: se muestran los datos asociados a la póliza
'--------------------------------------------------------------------------------------------
Private Sub ShowData()
	'--------------------------------------------------------------------------------------------
	Dim llngIndex As Short
	Dim lcolVPolicyQuery As ePolicy.VPolicyQuerys
	Dim lclsVPolicyQuery As Object
	
	llngIndex = 0
	
	lcolVPolicyQuery = New ePolicy.VPolicyQuerys
	
	If lcolVPolicyQuery.inspreGE010(mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClientC"), Request.QueryString.Item("sClientA"), mobjValues.StringToType(Request.QueryString.Item("dStartdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sStatus_pol"), mobjValues.StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sPolitype"), Session("sInitials"), Session("sAccesswo"), mobjValues.StringToType(Request.QueryString.Item("nCompany_First"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch_First"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct_First"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy_First"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif_First"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCompany"), Request.QueryString.Item("sBranch"), Request.QueryString.Item("sProduct"), Request.QueryString.Item("sPolicy"), Request.QueryString.Item("sCertif"), mobjValues.StringToType(Request.QueryString.Item("nElement"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDirection")) Then
		With mobjGrid
			For	Each lclsVPolicyQuery In lcolVPolicyQuery
				.Columns("cbeCompany").DefValue = lclsVPolicyQuery.nCompany
				.Columns("cbePolitype").DefValue = lclsVPolicyQuery.sPolitype
				.Columns("cbeBranch").DefValue = lclsVPolicyQuery.nBranch
				.Columns("valProduct").DefValue = lclsVPolicyQuery.nProduct
				.Columns("tcnPolicy").DefValue = lclsVPolicyQuery.nPolicy
				.Columns("tcnCertif").DefValue = lclsVPolicyQuery.nCertif
				.Columns("dtcClientC").DefValue = lclsVPolicyQuery.sClientC
				.Columns("dtcClientA").DefValue = lclsVPolicyQuery.sClientA
				.Columns("cbeStatus_pol").DefValue = lclsVPolicyQuery.sStatusva
				.Columns("tcdStartdate").DefValue = lclsVPolicyQuery.dStartdate
				.Columns("hddDigit").DefValue = lclsVPolicyQuery.nDigit
				.Columns("tcnPolicy").HRefScript = "insSelectPolicy(" & llngIndex & ")"
				.Columns("tcnCertif").HRefScript = "insSelectPolicy(" & llngIndex & ")"
				
				Response.Write(mobjGrid.DoRow())
				llngIndex = llngIndex + 1
			Next lclsVPolicyQuery
		End With
		Response.Write("<SCRIPT>")
		Response.Write("top.frames[""fraFolder""].document.cmdBack.disabled=true;")
		Response.Write("top.frames[""fraFolder""].document.cmdNext.disabled=true;")
		With lcolVPolicyQuery
			'+ Si ya se han mostrado mas de un bloque de registros, se habilita la acción "Anterior"
			If .nElement > 1 Then
				Response.Write("top.frames[""fraFolder""].document.cmdBack.disabled=false;")
			End If
			'+ Si no se muestran todos los registros, es porque es el último bloque de registros o 
			'+ no existen mas registros para la condición de búsqueda.  
			'+ En el SP se buscan solo 50 registros.
			If .Count <= 50 Then
				Response.Write("top.frames[""fraFolder""].document.cmdNext.disabled=false;")
			End If
			'+ Por cada búsqueda se asignan las variables para una próxima búsqueda con la misma condición
			If .nElement > 0 Then
				Response.Write("top.frames[""fraFolder""].sNextQuery= '&nCompany_First=" & .nCompany_First & "&nBranch_First=" & .nBranch_First & "&nProduct_First=" & .nProduct_First & "&nPolicy_First=" & .nPolicy_First & "&nCertif_First=" & .nCertif_First & "&sCompany=" & .sCompany & "&sBranch=" & .sBranch & "&sProduct=" & .sProduct & "&sPolicy=" & .sPolicy & "&sCertif=" & .sCertif & "&nElement=" & .nElement & "'")
			End If
		End With
		Response.Write("</" & "Script>")
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolVPolicyQuery = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "GE010"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>
<SCRIPT>
	var mblnContinue = true;

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.34 $|$$Author: Nvaplat60 $"	
	
//% insSelectPolicy: se ejecutan las acciones al ejecutar un registro del grid
//-------------------------------------------------------------------------------------------
function insSelectPolicy(Index){
//-------------------------------------------------------------------------------------------
	var lstrParameters = '';
	
	if(marrArray[Index].cbeCompany==<%=Session("nMultiCompany")%>){
		top.opener.UpdateDiv('val<%=Request.QueryString.Item("FieldProduct")%>Desc', '');
		
		with(top.opener.document.forms[0]){
			<%=Request.QueryString.Item("FieldBranch")%>.value = marrArray[Index].cbeBranch;
			if(<%=Request.QueryString.Item("FieldProduct")%>.value!=marrArray[Index].valProduct){
				<%=Request.QueryString.Item("FieldProduct")%>.Parameters.Param1.sValue = marrArray[Index].cbeBranch;
				<%=Request.QueryString.Item("FieldProduct")%>.disabled = false;
				btn<%=Request.QueryString.Item("FieldProduct")%>.disabled = false;
				<%=Request.QueryString.Item("FieldProduct")%>.value = marrArray[Index].valProduct;
				top.opener.$('#<%=Request.QueryString("FieldProduct")%>').change();
			}
			if(<%=Request.QueryString.Item("FieldPolicy")%>.value!=marrArray[Index].tcnPolicy){
				<%=Request.QueryString.Item("FieldPolicy")%>.value = marrArray[Index].tcnPolicy;
				<%=Request.QueryString.Item("FieldPolicy")%>_Digit.value = marrArray[Index].hddDigit;
				insOnblur();
			}           


			<%If Request.QueryString.Item("FieldCertif") <> vbNullString Then%>
			<%=Request.QueryString.Item("FieldCertif")%>.value = marrArray[Index].tcnCertif;
            top.opener.$('#<%=Request.QueryString.Item("FieldCertif")%>').change();
			<%End If%>
		}
	}
	else{
//+ Se cambia a la compañía en donde se encuentra la póliza
		lstrParameters= 'sCodisplOrig=GE010|nBranch=' + marrArray[Index].cbeBranch + '|nProduct=' + marrArray[Index].valProduct + '|nPolicy=' + marrArray[Index].tcnPolicy + '|nCertif=' + marrArray[Index].tcnCertif;
		insDefValues('', 'sChangeLogin=1&sCodispl=CA001_K&nCompanyNumber=' + marrArray[Index].cbeCompany + '&sChangeLogin_Parameters=' + lstrParameters, '/VTimeNet/VisualTime', 'Login');
	}
}

//% insOnblur: se ejecuta el ONCHANGE del campo "Poliza" después de ejecutar el ONCHANGE
//%			   del campo "Producto"
//-------------------------------------------------------------------------------------------
function insOnblur(){
//-------------------------------------------------------------------------------------------
	var lstrFrame='fraFolder';
	var nZone=top.opener.top.frames["fraSequence"].pintZone;
	if(nZone==1)
		lstrFrame="fraHeader";

	if(top.opener.top.frames[lstrFrame].mstrDoSubmit==1){
		mblnContinue = false;
		top.opener.$('#<%=Request.QueryString("FieldPolicy")%>').change();
		top.close();
	}

    if (mblnContinue)
		setTimeout('insOnblur()',2000);
}
</SCRIPT>
</HEAD>
<BODY>

<%
Call insDefineHeader()
Call ShowData()
%>
<SCRIPT>
	top.frames["fraFolder"].UpdateDiv('lblWaitProcess','','');
    top.frames["fraFolder"].document.cmdBack.src = '/VTimeNet/Images/btnLargeBackOff.png'
    top.frames["fraFolder"].document.cmdNext.src = '/VTimeNet/Images/btnLargeNextOff.png'
</SCRIPT>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




