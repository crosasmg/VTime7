<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'-Variable que guarda el codigo de la cobertura seleccionada
Dim mintCovergen As Integer
Dim mintRole As Integer
Dim mintCapital As Double
Dim mintAllSelected As Byte


'% insDefineHeader: se definen las caracter?sticas del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lintTyp_cumul As Object
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "DP770"
	
	'lintTyp_cumul = Request.QueryString("nTyp_cumul")
	'If Request.QueryString("nTyp_cumul") = vbNullString Then
	'    lintTyp_cumul = 3
	'End If
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddHiddenColumn("hddnBranchadd", vbNullString)
		.AddTextColumn(0, "Ramo", "txtsBranchadd", 30, vbNullString)
		.AddHiddenColumn("hddnProductadd", vbNullString)
		.AddTextColumn(0, "C?digo prod.", "txtnProductadd", 5, vbNullString)
		.AddTextColumn(0, "Nombre prod.", "txtsProductadd", 30, vbNullString)
		.AddHiddenColumn("hddnCoveradd", vbNullString)
		.AddTextColumn(0, "Cobertura", "txtsCoveradd", 120, vbNullString)
		.AddHiddenColumn("hddnRoleadd", vbNullString)
		.AddTextColumn(0, "Figura", "txtsRoleadd", 30, vbNullString)
		.AddCheckColumn(0, "C?mulo", "chknClusteradd", vbNullString)
		.AddHiddenColumn("hddnClusteradd", vbNullString)
		.AddCheckColumn(0, "Capital", "chknCapitaladd", vbNullString)
		.AddHiddenColumn("hddnCapitaladd", vbNullString)
		.AddCheckColumn(0, "?Relaci?n inversa?", "chknInverse", vbNullString)
		.AddHiddenColumn("hddnInverse", vbNullString)
		.AddHiddenColumn("hddnId", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP770"
		.Top = 100
		.Height = 280
		.Width = 450
		.nMainAction = Request.QueryString("nMainAction")
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP770: Se realiza el manejo de los datos a mostrar en la ventana
'--------------------------------------------------------------------------------------------
Private Sub insPreDP770()
	'--------------------------------------------------------------------------------------------
	Dim lcolProd_addcap As eProduct.Prod_addcaps
	Dim lclsProd_addcap As eProduct.Prod_addcap
	Dim lintIndex As Short
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=41339>" & GetLocalResourceObject("cbeCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	
	With mobjValues
		.Parameters.Add("nBranch", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        .Parameters.Add("sCovergen", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("cbeCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngComboType, CStr(mintCovergen), True,  ,  ,  ,  , "insChange_values(""Covergen"",this);",  ,  , "Cobertura definida para el producto"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=10%><LABEL ID=0>" & GetLocalResourceObject("valRoleMCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	With mobjValues
		.Parameters.Add("nBranch", .StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", .StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCover", mintCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nRole", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", .StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sCacaltyp", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valRoleM", "tabTab_Covrol6", eFunctions.Values.eValuesType.clngWindowType, CStr(mintRole), True,  ,  ,  ,  , "insChange_values(""RoleM"", this);", mintCovergen = eRemoteDB.Constants.intNull,  , "Figura o rol del cliente asociado a la cobertura"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnCapitalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnCapital", 18, mobjValues.TypeToString(Request.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble, True, 6),  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6,  ,  ,  , "insChange_values(""Capital"")", mobjValues.StringToType(Request.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        <TD></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkSelAllInverse", "Marcar/Desmarcar columna de relacion inversa", vbNullString,  , "insSelectAll(this.checked, self.document.forms[0].chknInverse, self.document.forms[0].hddnInverse)"))


Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	
	mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
	If mintCovergen <> eRemoteDB.Constants.intNull And mintRole <> eRemoteDB.Constants.intNull Then
		lcolProd_addcap = New eProduct.Prod_addcaps
		If lcolProd_addcap.Find(CInt(Session("nBranch")), CInt(Session("nProduct")), mintCovergen, CDate(Session("dEffecdate")), mintRole) Then
			lintIndex = 0
			For	Each lclsProd_addcap In lcolProd_addcap
				With lclsProd_addcap
					mobjGrid.Columns("hddnBranchadd").DefValue = CStr(.nBranchadd)
					mobjGrid.Columns("txtsBranchadd").DefValue = .sBranchadd
					mobjGrid.Columns("hddnProductadd").DefValue = CStr(.nProductadd)
					mobjGrid.Columns("txtnProductadd").DefValue = CStr(.nProductadd)
					mobjGrid.Columns("txtsProductadd").DefValue = .sProductadd
					mobjGrid.Columns("hddnCoveradd").DefValue = CStr(.nCoveradd)
					mobjGrid.Columns("txtsCoveradd").DefValue = .sCoveradd
					mobjGrid.Columns("hddnRoleadd").DefValue = CStr(.nRoleadd)
					mobjGrid.Columns("txtsRoleadd").DefValue = .sRoleadd
					mobjGrid.Columns("chknClusteradd").Checked = .nClusteradd
					mobjGrid.Columns("hddnClusteradd").DefValue = CStr(.nClusteradd)
					mobjGrid.Columns("chknClusteradd").OnClick = "chkSel(this.checked, self.document.forms[0].hddnClusteradd, " & lintIndex & ");"
					mobjGrid.Columns("chknCapitaladd").Checked = .nCapitaladd
					mobjGrid.Columns("hddnCapitaladd").DefValue = CStr(.nCapitaladd)
					mobjGrid.Columns("chknCapitaladd").OnClick = "chkSel(this.checked, self.document.forms[0].hddnCapitaladd, " & lintIndex & ");"
					mobjGrid.Columns("chknInverse").Checked = .nInverse
					mobjGrid.Columns("hddnInverse").DefValue = CStr(.nInverse)
					mobjGrid.Columns("chknInverse").OnClick = "chkSel(this.checked, self.document.forms[0].hddnInverse, " & lintIndex & ");"
					mobjGrid.Columns("hddnId").DefValue = CStr(.nId)
					If .nInverse = 1 And mintAllSelected = 0 Then
						mintAllSelected = 1
					End If
					Response.Write(mobjGrid.DoRow())
				End With
				lintIndex = lintIndex + 1
			Next lclsProd_addcap
			If mintCapital > 0 Then
				Response.Write("<SCRIPT>document.forms[0].tcnCapital.value='" & mintCapital & "'</" & "Script>")
			Else
				If lcolProd_addcap.Count > 0 Then
					Response.Write("<SCRIPT>document.forms[0].tcnCapital.value='" & mobjValues.TypeToString(lcolProd_addcap(1).nCapital, eFunctions.Values.eTypeData.etdDouble, True, 6) & "'</" & "Script>")
				End If
				If mintAllSelected = 1 Then
					Response.Write("<SCRIPT>document.forms[0].chkSelAllInverse.checked=true</" & "Script>")
				Else
					Response.Write("<SCRIPT>document.forms[0].chkSelAllInverse.checked=false</" & "Script>")
				End If
			End If
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolProd_addcap = Nothing
End Sub

'% insPreDP770Upd: Se realiza el manejo de los campos del grid, al mostrar la PopUp
'--------------------------------------------------------------------------------------------
Private Sub insPreDP770Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsProd_addcap As eProduct.Prod_addcap
	lclsProd_addcap = New eProduct.Prod_addcap
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsProd_addcap.InsPostDP770Upd(.QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"), eRemoteDB.Constants.intNull) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP770", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		Response.Write(mobjValues.HiddenControl("hddCovergen", .QueryString.Item("nCovergen")))
		Response.Write(mobjValues.HiddenControl("hddRole", .QueryString.Item("nRolegen")))
		Response.Write(mobjValues.HiddenControl("hddCapital", .QueryString.Item("nCapital")))
	End With
	lclsProd_addcap = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP770"

mobjMenu = New eFunctions.Menues
mintCovergen = mobjValues.StringToType(Request.QueryString("nCovergen"), eFunctions.Values.eTypeData.etdLong, True)
mintRole = mobjValues.StringToType(Request.QueryString("nRole"), eFunctions.Values.eTypeData.etdLong, True)
mintCapital = mobjValues.StringToType(Request.QueryString("nCapital"), eFunctions.Values.eTypeData.etdDouble, True)
mintAllSelected = 0
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP770", "DP770.aspx", CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing

%>
<SCRIPT LANGUAGE="JavaScript">
//- Variables para controlar la recarga de la p?gina al cambiar los valores del Ramo-producto    
    var mlngProduct = '<%=Request.QueryString("nProduct")%>'
    var mlngBranch = '<%=Request.QueryString("nBranch")%>'
    var mlngRole = '<%=Request.QueryString("nRole")%>'
    var mlngCovergen = '<%=Request.QueryString("nCovergen")%>'
    
    //% insChange_values: se realizan las acciones al cambiar el valor de los campos
    //----------------------------------------------------------------------------------------------------------------------
    function insChange_values(Option, Field){
    //----------------------------------------------------------------------------------------------------------------------
        var lstrstring = '';

        switch(Option){
            case "Capital":

            case "Covergen":

            case "RoleM":
                //+ Se recarga la p?gina cuando se selecciona la cobertura
                if(mlngRole != self.document.forms[0].valRoleM.value || 
                   mlngCovergen != self.document.forms[0].cbeCover.value) {
                    lstrstring += document.location;
                    lstrstring = lstrstring.replace(/&nCovergen=.*/, "");
                    lstrstring = lstrstring + "&nCovergen=" + self.document.forms[0].cbeCover.value +
                                              "&nRole=" + self.document.forms[0].valRoleM.value;
                    document.location.href = lstrstring;
                }
                break;

        }
    }

    //% chkSel: Evento para indicar la selecci?n de una columna
    //-----------------------------------------------------------------------------
    function chkSel(bChecked, ohddSel, nIndex)
    //-----------------------------------------------------------------------------
    {
        with(self.document.forms[0])
        {
            if (marrArray.length==1)
            {
                ohddSel.value = (bChecked)?"1":"0";
            }
            else
            {
                ohddSel[nIndex].value = (bChecked)?"1":"0";
            }
        }
    }

	//% insSelectAll: Se marcan/desmarcan todos los registros del grid
	//-----------------------------------------------------------------------------
	function insSelectAll(bChecked, objCheck, ohddSel)
	//-----------------------------------------------------------------------------
	{
		var lintLength = marrArray.length;
		with(self.document.forms[0])
		{
			if (lintLength==1)
			{
				objCheck.checked = bChecked;
				ohddSel.value = (bChecked)?"1":"0";
			}
			else
			{
				for (lintIndex = 0; lintIndex < lintLength; lintIndex++){
					objCheck[lintIndex].checked = bChecked;
					ohddSel[lintIndex].value = (bChecked)?"1":"0";
				}
			}
		}
	}

//- Variable para el control de versiones
document.VssVersion="$$Revision: 4 $|$$Date: 19/07/13 4:07p $|$$Author: Jsarabia $"

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmDP770" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP770", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
'If Request.QueryString("Type") = "PopUp" Then
'    Call insPreDP770Upd()
'Else
Call insPreDP770()
'End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





