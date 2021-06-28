<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales del grid
Dim mobjGrid As eFunctions.Grid


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "vac631_k"
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR><TD ALIGN=""CENTER"" COLSPAN=10>")

	
	With mobjGrid.Columns
		.AddTextColumn(101497, GetLocalResourceObject("txtBranchgridColumnCaption"), "txtBranchgrid", 10, "",  , "",  ,  ,  , True)
		.AddTextColumn(102055, GetLocalResourceObject("txtProductgridColumnCaption"), "txtProductgrid", 10, "",  ,  ,  ,  ,  , True)
		.AddAssociateColumn(0, "Consultas asociadas", "btnQuery", 2)
		.AddNumericColumn(101495, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		.AddTextColumn(102065, GetLocalResourceObject("txtClientColumnCaption"), "txtClient", 10, "Código del contratante de la póliza",  ,  ,  ,  ,  , True)
		.AddTextColumn(101498, GetLocalResourceObject("txtClientNameColumnCaption"), "txtClientName", 10, "Nombre del contratante de la póliza",  ,  ,  ,  ,  , True)
	End With
	
	With mobjGrid
		.Splits_Renamed.AddSplit(0, "", 5)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.bOnlyForQuery = True
	End With
	
	
Response.Write("" & vbCrLf)
Response.Write("    </TD></TR>   " & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("</DIV>")

	
End Sub

Private Sub insDefineHeader1()
	Dim lintBranch As String
	lintBranch = Request.QueryString.Item("nBranch")
	If lintBranch = vbNullString Then
		lintBranch = "40"
	End If
	
Response.Write("    " & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=102054>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), lintBranch,  ,  ,  ,  , "InsChangeField();", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=8%>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=102055>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), lintBranch,  ,  , Request.QueryString.Item("nProduct"),  ,  ,  , "InsChangeField();",  ,  ,  , eFunctions.Values.eProdClass.clngActiveLife))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 8, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"), False,  ,  ,  ,  ,  , True))


Response.Write("</td>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnCertif", 8, Request.QueryString.Item("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"), False,  ,  ,  ,  ,  , True))


Response.Write("</td>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("valClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=4>")


Response.Write(mobjValues.ClientControl("valClient", Request.QueryString.Item("sClient"),  , GetLocalResourceObject("valClientToolTip"),  , True, "lblCliename"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeStatusvaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeStatusva", "Table181", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("sStatusva"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatusvaToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>            " & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">        " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Datos del plan"">" & GetLocalResourceObject("AnchorDatos del planCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14431>" & GetLocalResourceObject("valModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

	
	With mobjValues
		.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nModulec"), True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valModulecToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeOptionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeOption", "Table5519", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nOption"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOptionToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbePayFreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayFreq", "Table36", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nPayFreq"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayFreqToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCapitalDeathCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnCapitalDeath", 18, Request.QueryString.Item("nCapitalDeath"),  , GetLocalResourceObject("tcnCapitalDeathToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremdealCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremdeal", 18, Request.QueryString.Item("nPremdeal"),  , GetLocalResourceObject("tcnPremdealToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeAgreementCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

	
	mobjValues.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeAgreement", "tabAgreement_al", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nAgreement"), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAgreementToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Rentabilidad"">" & GetLocalResourceObject("AnchorRentabilidadCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeTypeinvestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTypeinvest", "Table5520", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTypeinvest"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeinvestToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnIntprojectCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnIntproject", 5, Request.QueryString.Item("nIntproject"),  , GetLocalResourceObject("tcnIntprojectToolTip"), True, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnWarminintCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnWarminint", 5, Request.QueryString.Item("nWarminint"),  , GetLocalResourceObject("tcnWarminintToolTip"), True, 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

End Sub

'% insPreVAC631: Se cargan los datos en el grid de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreVAC631()
	'--------------------------------------------------------------------------------------------
	Dim lclsActiveLife As eBranches.ActiveLife
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	
	lblnFind = False
	lclsActiveLife = New eBranches.ActiveLife
	
	With Request
		If .QueryString.Item("bFind") = "1" Then
			If lclsActiveLife.Find_VAC631(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCapitalDeath"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPremdeal"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nIntproject"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypeinvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nWarminint"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString.Item("sStatusva"), 1, .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPayFreq"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				lintCount = 0
				
Response.Write("" & vbCrLf)
Response.Write("<DIV ID=""Scroll"" STYLE=""height:150;overflow:auto; outset gray"">")

				
				With lclsActiveLife
                    'For lintCount = 1 To .Count
					For lintCount = 0 To .Count-1
						If .Item(lintCount) Then
							mobjGrid.Columns("txtBranchgrid").DefValue = .sDescbranch
							mobjGrid.Columns("txtProductgrid").DefValue = .sDescproduct
							mobjGrid.Columns("tcnPolicy").DefValue = CStr(.nPolicy)
							mobjGrid.Columns("txtClient").DefValue = .sClient
							mobjGrid.Columns("txtClientName").DefValue = .sCliename
							mobjGrid.Columns("tcnPolicy").HRefScript = "InsUpdPolicyData(" & .nBranch & "," & .nProduct & ",'" & Trim(.sDescproduct) & "'," & .nPolicy & "," & .nCertif & "," & .sStatusva & ",'" & .sClient & "','" & Trim(.sCliename) & "','" & .sDigit & "');"
							mobjGrid.Columns("tcnPolicy").HRefScript = mobjGrid.Columns("tcnPolicy").HRefScript & "InsUpdModuleData(" & 1 & "," & .nModulec & "," & .nOption & "," & .nPayFreq & ",'" & .nCapitalDeath & "','" & .nPremdeal & "'," & .nAgreement & ");"
							mobjGrid.Columns("tcnPolicy").HRefScript = mobjGrid.Columns("tcnPolicy").HRefScript & "InsUpdRentData(" & .nTypeinvest & ",'" & .nIntproject & "','" & .nWarminint & "');"
							
							mobjGrid.Columns("btnQuery").sQueryString = "sCertype=2" & "!nBranch=" & Request.QueryString.Item("nBranch") & "!nProduct=" & Request.QueryString.Item("nProduct") & "!nPolicy=" & .nPolicy & "!nCertif=" & .nCertif & "!dStartdate=" & Today & "!LoadWithAction=Cons" & "!nTransaction=8"
							
							Response.Write(mobjGrid.DoRow())
						End If
					Next 
				End With
				
Response.Write("</DIV>")

				
			End If
		End If
		Response.Write(mobjGrid.closeTable())
		Call insDefineHeader()
	End With
	
	lclsActiveLife = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "vac631_k"
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT LANGUAGE="JavaScript">

//%insCancel: Función que se ejecuta cuando se cancela la transacción
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
    return true;
}   

//%insFinish: Función que se ejecuta cuando se finaliza la transacción
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}

//%insStateZone: Función que se ejecuta cuando indica una acción del menú
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
    var lintIndex;
    var error;
    try {
        for(lintIndex=1;lintIndex < self.document.forms[0].elements.length;lintIndex++){
            self.document.forms[0].elements[lintIndex].disabled=false;
            if(self.document.images.length>0)
                if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined')
                   self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
        }
     }catch(error){}
     self.document.forms[0].cbeBranch.disabled=false;
     self.document.forms[0].btnvalProduct.disabled=false;
     self.document.forms[0].btnvalModulec.disabled=false;
}

//%InsChangeField: Función que se actualiza los parámetros de los campos dependientes
//--------------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
	    valModulec.Parameters.Param1.sValue = cbeBranch.value;
        valModulec.Parameters.Param2.sValue = valProduct.value;
    }
}   

//%InsUpdPolicyData: Función que actualiza los campos puntuales de la póliza seleccionada
//--------------------------------------------------------------------------------------------------
function InsUpdPolicyData(nBranch,nProduct,sDescProduct,nPolicy,nCertif,sStatusva,sClient,sCliename,sDigit){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        cbeBranch.value = nBranch;
        valProduct.value = nProduct;
        UpdateDiv('valProductDesc', sDescProduct);
        UpdateDiv('lblCliename', sCliename);
        tcnPolicy.value = nPolicy;
        tcnCertif.value = nCertif;
        cbeStatusva.value = sStatusva;
        valClient.value = sClient;
        valClient_Digit.value = sDigit
    }
}

//%InsUpdModuleData: Función que actualiza los campos puntuales de la póliza seleccionada
//--------------------------------------------------------------------------------------------------
function InsUpdModuleData(nCurrency,nModulec,nOption,nPayFreq,nCapitalDeath,nPremdeal,nAgreement){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        cbeCurrency.value = nCurrency;
        valModulec.value = nModulec;
        cbeOption.value = nOption;
        cbePayFreq.value = nPayFreq;
        tcnCapitalDeath.value = VTFormat(nCapitalDeath, '', '', '', 0);
        tcnPremdeal.value = VTFormat(nPremdeal, '', '', '', 0);
        cbeAgreement.value = nAgreement;
    }
}

//%InsUpdRentData: Función que actualiza los campos puntuales de la póliza seleccionada
//--------------------------------------------------------------------------------------------------
function InsUpdRentData(nTypeinvest,nIntproject,nWarminint){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        cbeTypeinvest.value = nTypeinvest;
        tcnIntproject.value = VTFormat(nIntproject, '', '', '', 2);
        tcnWarminint.value = VTFormat(nWarminint, '', '', '', 2);
    }
}

//+ Variable para el control de versiones
document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:54 $|$$Author: Nvaplat18 $"

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VAC631_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VAC631" ACTION="ValBranchQue.aspx?x=1">
<BR><BR>
<%=mobjValues.ShowWindowsName("VAC631")%>
<BR><BR>
<%
Call insDefineHeader1()
Call insDefineHeader()
Call insPreVAC631()
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





