<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

'- Declaraciòn de Variables locales
Dim mblnVisible As Boolean
Dim mblnDisabled As Boolean


    '% insDefineHeader: Se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        '+ Se definen las columnas del grid
        With mobjGrid.Columns
            Call .AddPossiblesColumn(41461, GetLocalResourceObject("tcnRoleColumnCaption"), "tcnRole", "Table184", eFunctions.Values.eValuesType.clngWindowType, "", , , , , , CBool(mblnDisabled))
            Call .AddCheckColumn(41462, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", "", 2, CStr(2), , CBool(mblnDisabled))
            Call .AddCheckColumn(0, "Siniestrado por defecto", "chkAuxDefaultClaInd", "", 2, CStr(2), , CBool(mblnDisabled))
            Call .AddHiddenColumn("chkAuxExist", CStr(2))
            Call .AddHiddenColumn("chkAuxRequire", CStr(2))
            Call .AddHiddenColumn("tcnAuxRole", CStr(0))
            Call .AddHiddenColumn("sAuxSel", CStr(2))
            Call .AddHiddenColumn("tctAuxDefaultClaInd", CStr(2))
        End With
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            If Request.QueryString.Item("Action") = "Update" Then
                .Columns("Sel").GridVisible = False
            End If
            .Columns("Sel").GridVisible = True
            .Codispl = "DP056"
            .Width = 350
            .Height = 150
            .DeleteButton = False
            .AddButton = False
            If Session("bQuery") Then
                .DeleteButton = False
                .AddButton = False
                .bOnlyForQuery = True
                .Columns("Sel").Disabled = True
                .Columns("chkRequire").Disabled = True
            End If
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .Columns("Sel").OnClick = "SelectedRequire(this);if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
            .Columns("chkRequire").OnClick = "SelectedSel(this);if(document.forms[0].chkAuxRequire.length>0)document.forms[0].chkAuxRequire[this.value].value =(this.checked?1:2); else document.forms[0].chkAuxRequire.value =(this.checked?1:2);"
            .Columns("chkAuxDefaultClaInd").OnClick = "SelectedDef(this);"
        End With
    End Sub

    '% insPreDP056: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreDP056()
        '--------------------------------------------------------------------------------------------
        Dim lclsCliallocla As eProduct.Cliallocla
        Dim lcolClialloclas As eProduct.Clialloclas
        Dim lintIndex As Short
        With Server
            lclsCliallocla = New eProduct.Cliallocla
            lcolClialloclas = New eProduct.Clialloclas
        End With
        If lcolClialloclas.FindDP056(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
            lintIndex = 0
            For Each lclsCliallocla In lcolClialloclas
                With mobjGrid
                    .Columns("tcnRole").DefValue = CStr(lclsCliallocla.nRole)
                    .Columns("tcnRole").Descript = lclsCliallocla.sDescript
                    If lclsCliallocla.nBranch = eRemoteDB.Constants.intNull Or lclsCliallocla.nProduct = eRemoteDB.Constants.intNull Then
                        .Columns("chkAuxExist").DefValue = CStr(2)
                        .Columns("Sel").Checked = 2
                        .Columns("sAuxSel").DefValue = CStr(2)
                    Else
                        .Columns("chkAuxExist").DefValue = CStr(1)
                        .Columns("Sel").Checked = 1
                        .Columns("sAuxSel").DefValue = CStr(1)
                    End If
                    .Columns("chkRequire").DefValue = CStr(lintIndex)
                    .Columns("chkRequire").Checked = mobjValues.StringToType(lclsCliallocla.sRequire, eFunctions.Values.eTypeData.etdDouble)
                    .Columns("chkAuxDefaultClaInd").DefValue = CStr(lintIndex)
                    .Columns("chkAuxDefaultClaInd").Checked = lclsCliallocla.SDEFAULT_CLA_IND
                    .Columns("tctAuxDefaultClaInd").DefValue = lclsCliallocla.SDEFAULT_CLA_IND
                    .Columns("chkAuxRequire").DefValue = lclsCliallocla.sRequire
                    .Columns("tcnAuxRole").DefValue = CStr(lclsCliallocla.nRole)
                    .sDelRecordParam = "nRole=' + marrArray[lintIndex].tcnAuxRole + '&sRequire=' + marrArray[lintIndex].chkRequire + '"
                    Response.Write(.DoRow)
                    lintIndex = lintIndex + 1
                End With
            Next lclsCliallocla
        Else
            mblnVisible = True
        End If
        Response.Write(mobjGrid.closeTable())
        Response.Write(mobjValues.BeginPageButton)
        lclsCliallocla = Nothing
        lcolClialloclas = Nothing
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP056"

mobjValues.sCodisplPage = "DP056"

mobjValues.ActionQuery = Session("bQuery")
mobjGrid.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 17:02 $"

// + %SelectedSel: Si selecciona el campo obligatorio y la columna sel no esta seleccionada,ésta se selecciona 
// +               (Creada por CTMC para corrección de errores)
//--------------------------------------------------------------------------------------------------
function SelectedSel(Field){
//--------------------------------------------------------------------------------------------------
	if (Field.checked){
		marrArray[Field.value].Sel = Field.checked;
		self.document.forms[0].Sel[Field.value].checked=1;
		marrArray[Field.value].sAuxSel = Field.checked;
		self.document.forms[0].sAuxSel[Field.value].checked=1;
		self.document.forms[0].sAuxSel[Field.value].value=1;
		marrArray[Field.value].chkAuxRequire = Field.checked;
		self.document.forms[0].chkAuxRequire[Field.value].checked=1;
	}
	else
		marrArray[Field.value].Sel = !Field.checked;
}

//%SelectedRequire: Si des-selecciona el campo sel y la columna obligatoria  esta seleccionada, ésta se des-selecciona 
//               (Creada por CTMC para corrección de errores)
//--------------------------------------------------------------------------------------------------
function SelectedRequire(Field)
//--------------------------------------------------------------------------------------------------
{
	if (!Field.checked)
	{
		marrArray[Field.value].chkRequire = Field.checked;
		marrArray[Field.value].chkAuxRequire = Field.checked;
		marrArray[Field.value].sAuxSel = Field.checked;
		self.document.forms[0].chkAuxRequire[Field.value].value=2;
		self.document.forms[0].chkAuxRequire[Field.value].checked=2;
		self.document.forms[0].chkRequire[Field.value].checked=0;
		self.document.forms[0].sAuxSel[Field.value].checked=0;
		self.document.forms[0].sAuxSel[Field.value].value = 0;
	}
}

//%SelectedRequire: Si des-selecciona el campo sel y la columna obligatoria  esta seleccionada, ésta se des-selecciona 
//               (Creada por CTMC para corrección de errores)
//--------------------------------------------------------------------------------------------------
function SelectedRequire(Field)
//--------------------------------------------------------------------------------------------------
{
    if (!Field.checked) {
        marrArray[Field.value].chkRequire = Field.checked;
        marrArray[Field.value].chkAuxRequire = Field.checked;
        marrArray[Field.value].sAuxSel = Field.checked;
        self.document.forms[0].chkAuxRequire[Field.value].value = 2;
        self.document.forms[0].chkAuxRequire[Field.value].checked = 2;
        self.document.forms[0].chkRequire[Field.value].checked = 0;
        self.document.forms[0].sAuxSel[Field.value].checked = 0;
        self.document.forms[0].sAuxSel[Field.value].value = 0;
    }
}

//%SelectedRequire: Si des-selecciona el campo sel y la columna obligatoria  esta seleccionada, ésta se des-selecciona 
//               (Creada por CTMC para corrección de errores)
//--------------------------------------------------------------------------------------------------
function SelectedDef(Field)
//--------------------------------------------------------------------------------------------------
{
    if (Field.checked) {
        $("[name=tctAuxDefaultClaInd]")[Field.value].value= "1";
    }
    else{
        $("[name=tctAuxDefaultClaInd]")[Field.value].value="2";
    }
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD='POST' ID='FORM' NAME='fraContent' ACTION='valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>'>
	<%=mobjValues.ShowWindowsName("DP056", Request.QueryString.Item("sWindowDescript"))%>
    <BR>
    <TABLE WIDTH='100%'>
        <%If Request.QueryString.Item("Action") = "Update" Then
	mblnDisabled = True
End If
Call insDefineHeader()
Call insPreDP056()
%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




