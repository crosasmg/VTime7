<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid

'- Se declara la variable que guarda el módulo seleccionado
Dim mlngModulec As Object

'- Se declara la variable que guarda la cobertura seleccionado
Dim mlngCover As Object

'- Se declara la variable que guarda la figura seleccionads
Dim mlngRole As Object


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "DP705"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddAnimatedColumn(0, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("sLinkColumnCaption"))
		End If
		.AddHiddenColumn("hddnExist", "")
		.AddHiddenColumn("hddnRole", "")
		.AddHiddenColumn("hddsSel", "")
		.AddTextColumn(0, GetLocalResourceObject("tctRoleColumnCaption"), "tctRole", 30, vbNullString,  , GetLocalResourceObject("tctRoleColumnToolTip"),  ,  ,  , True)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "table26", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeStatregtColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkDefaultiColumnCaption"), "chkDefaulti", "",  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkDefaultiColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkRequiredColumnCaption"), "chkRequired", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkRequiredColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnMax_roleColumnCaption"), "tcnMax_role", 5, vbNullString,  , GetLocalResourceObject("tcnMax_roleColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("valCovActiv_relColumnCaption"), "valCovActiv_rel", "tabTab_covrol4", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCovActiv_relColumnToolTip"))
	End With
	
	'+ Parametros de valores posibles
	With mobjGrid.Columns("valCovActiv_rel").Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecDate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nRole", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.ReturnValue("nRole",  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Height = 350
		.Width = 350
		.Codispl = "DP705"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bCheckVisible = CDbl(Request.QueryString.Item("nExist")) = 1
		.AddButton = False
		.DeleteButton = False
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("ReloadAction") <> "Add" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		'+ Se establece es estado inicial del campo "Estado" según la acción y el estado        
		.Columns("cbeStatregt").Disabled = Request.QueryString.Item("sStatregt") = "2"
          
		
            .Columns("Sel").GridVisible = Not Session("bQuery")
            .Columns("tctRole").EditRecord = True
            .Columns("cbeStatregt").BlankPosition = False
        End With
End Sub

'% insPreDP705: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP705()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	Dim lcolTab_covrol As eProduct.Tab_covrols
	Dim lclsProduct As eProduct.Product
	Dim lblnModulec As Boolean
	Dim lintIndex As Short
	lcolTab_covrol = New eProduct.Tab_covrols
	lclsTab_covrol = New eProduct.Tab_covrol
	lclsProduct = New eProduct.Product
	lintIndex = 0
	'+ Si tiene módulos asociados
	lblnModulec = lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
	
Response.Write("        " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <BR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        ")

	If lblnModulec Then
Response.Write("" & vbCrLf)
Response.Write("            <TD WIDTH=10%><LABEL ID=14431>" & GetLocalResourceObject("cbeModuleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("            ")

		
		With mobjValues
			.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Response.Write(mobjValues.PossiblesValues("cbeModule", "tabTab_modul", eFunctions.Values.eValuesType.clngComboType, CStr(mlngModulec), True,  ,  ,  ,  , "insChangeKey();",  , 5, GetLocalResourceObject("cbeModuleToolTip")))
		End With
		
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        ")

	End If
Response.Write("" & vbCrLf)
Response.Write("            <TD WIDTH=10%><LABEL ID=41339>" & GetLocalResourceObject("cbeCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("            ")

	
	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nModulec", mlngModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngComboType, CStr(mlngCover), True,  ,  ,  ,  , "insChangeKey();", lblnModulec And mlngModulec = 0, 5, GetLocalResourceObject("cbeCoverToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>")

	mobjValues.ActionQuery = Session("bQuery")
	If ((lblnModulec And mlngModulec > 0) Or (Not lblnModulec And mlngModulec = 0)) And mlngCover > 0 Then
		
		If lcolTab_covrol.Find(Session("nBranch"), Session("nProduct"), mlngModulec, mlngCover, Session("dEffecdate")) Then
			
			For	Each lclsTab_covrol In lcolTab_covrol
				With mobjGrid
					.Columns("sLink").HRefScript = "ShowSubSequence(2, '" & mlngModulec & "','" & mlngCover & "','" & lclsTab_covrol.nRole & "','" & lclsTab_covrol.sSel & "')"
					.Columns("hddnExist").DefValue = lclsTab_covrol.sSel
					.Columns("hddsSel").DefValue = lclsTab_covrol.sSel
					.Columns("Sel").Checked = CShort(lclsTab_covrol.sSel)
					.Columns("Sel").OnClick = "InsChangeSel(this," & lintIndex & "," & mlngModulec & "," & mlngCover & "," & lclsTab_covrol.nRole & "," & lclsTab_covrol.sSel & ")"
					.Columns("hddnRole").DefValue = CStr(lclsTab_covrol.nRole)
					.Columns("tctRole").DefValue = lclsTab_covrol.sDescrole
					.Columns("cbeStatregt").DefValue = lclsTab_covrol.sStatregt
					.Columns("chkDefaulti").DefValue = lclsTab_covrol.sDefaulti
					.Columns("chkDefaulti").Checked = CShort(lclsTab_covrol.sDefaulti)
					.Columns("chkRequired").DefValue = lclsTab_covrol.sRequired
					.Columns("chkRequired").Checked = CShort(lclsTab_covrol.sRequired)
					.Columns("tcnMax_role").DefValue = CStr(lclsTab_covrol.nMax_role)
					.Columns("valCovActiv_rel").Parameters("nRole").Value = (lclsTab_covrol.nRolActiv_rel)
					.Columns("valCovActiv_rel").DefValue = CStr(lclsTab_covrol.nCovActiv_rel)
					.sEditRecordParam = "nModulec=" & mlngModulec & "&nCover=" & mlngCover & "&nRole=" & lclsTab_covrol.nRole & "&sStatregt=" & lclsTab_covrol.sStatregt
					Response.Write(.DoRow)
				End With
				lintIndex = lintIndex + 1
			Next lclsTab_covrol
		End If
	End If
	Response.Write(mobjGrid.closeTable)
	
	If Request.QueryString.Item("ReloadAction") = "Update" And Request.QueryString.Item("nExist") = "2" Then
		'+ Se invoca la subsecuencia de asegurados
		Response.Write("<SCRIPT>ShowSubSequence(1,'" & mlngModulec & "','" & mlngCover & "','" & mlngRole & "','1')</" & "Script>")
	End If
	
	lcolTab_covrol = Nothing
	lclsTab_covrol = Nothing
	lclsProduct = Nothing
End Sub
'% insPreDP705Upd: Se realiza el manejo de los campos del grid 
'--------------------------------------------------------------------------------------------
Private Sub insPreDP705Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%InsLoadDefaultValues: Asigna los valores por defecto de la página" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsLoadDefaultValues(){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with (self.document.forms[0]){" & vbCrLf)
Response.Write("        if (hddnExist.value == 2 || cbeStatregt.value == 2){" & vbCrLf)
Response.Write("            cbeStatregt.value = 2;" & vbCrLf)
Response.Write("            cbeStatregt.disabled = true;" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTab_covrol = New eProduct.Tab_covrol
			Call lclsTab_covrol.insPostDP705(.QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mlngModulec, mlngCover, mlngRole, Session("dEffecdate"), vbNullString, Session("sBrancht"), vbNullString, vbNullString, mobjValues.StringToType(.QueryString.Item("nExist"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))
			'+ Se actualiza la tabla prod_win
			Call lclsTab_covrol.InsPostDP705_K(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nUsercode"))
			lclsTab_covrol = Nothing
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValProductSeq.aspx", "DP705", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>InsLoadDefaultValues();</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP705"

mlngModulec = 0
mlngCover = 0
mlngRole = 0

If Request.QueryString.Item("nModulec") <> vbNullString Then
	mlngModulec = CShort(Request.QueryString.Item("nModulec"))
End If
If Request.QueryString.Item("nCover") <> vbNullString Then
	mlngCover = CShort(Request.QueryString.Item("nCover"))
End If
If Request.QueryString.Item("nRole") <> vbNullString Then
	mlngRole = CShort(Request.QueryString.Item("nRole"))
End If
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 2 $|$$Date: 15/10/03 17:02 $"

    //+ Se recarga la página para que muestre las coberturas del módulo seleccionado
    //----------------------------------------------------------------------------------------------------------------------
    function insChangeKey() {
        //----------------------------------------------------------------------------------------------------------------------
        var lstrstring = '';
        var nModulec = 0
        var nCover = 0
        if (typeof (self.document.forms[0].cbeModule) != 'undefined')
            nModulec = self.document.forms[0].cbeModule.value
        nCover = self.document.forms[0].cbeCover.value
        if (nModulec != '<%=Request.QueryString.Item("nModulec")%>' ||
        nCover != '<%=Request.QueryString.Item("nCover")%>') {
            lstrstring += document.location;
            lstrstring = lstrstring.replace(/&nModulec=.*/, "");
            lstrstring = lstrstring.replace(/&nCover=.*/, "");
            lstrstring = lstrstring + "&nModulec=" + nModulec + "&nCover=" + nCover;
            document.location.href = lstrstring;
        }
    }
    //%InsChangeSel : Cambia el indicador de seleción
    //-------------------------------------------------------------
    function InsChangeSel(Field, nIndex, nModulec, nCover, nRole, nExist) {
        //-------------------------------------------------------------
        var lstrQuery = 'nModulec=' + nModulec + '&nCover=' + nCover + '&nRole=' + nRole + '&nExist=' + nExist
        with (self.document.forms[0]) {
            if (marrArray.length > 1)
                hddsSel[nIndex].value = (Field.checked ? 1 : 2);
            else
                hddsSel.value = (Field.checked ? 1 : 2);
        }
        if (Field.checked)
            EditRecord(Field.value, nMainAction, 'Update', lstrQuery);
        else
            EditRecord(nIndex, nMainAction, 'Del', lstrQuery);
        Field.checked = !Field.checked;
    }
</SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP705", "DP705.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
    //% ShowSubSequence: muestra la subsecuencia para la cobertura en tratamiento
    //--------------------------------------------------------------------------------------------
    function ShowSubSequence(nAutomatic, nModulec, nCover, nRole, nSel) {
        //--------------------------------------------------------------------------------------------
        if (nSel == 1)
            if (nAutomatic == 1)
                ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/RolesSeq&bAutomatic=true&sCodispl=DP705&nModulec=' + nModulec + '&nRole=' + nRole + '&nCover=' + nCover, 'RolesSeq', 950, 650, 'no', 'no', 20, 20, 'yes')
            else
                ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/RolesSeq&bAutomatic=false&sCodispl=DP705&nModulec=' + nModulec + '&nRole=' + nRole + '&nCover=' + nCover, 'RolesSeq', 950, 650, 'no', 'no', 20, 20, 'yes')
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP705" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.HiddenControl("hddnModulec", mlngModulec))
Response.Write(mobjValues.HiddenControl("hddnCover", mlngCover))
Response.Write(mobjValues.HiddenControl("hddnRoleSel", mlngRole))
Response.Write(mobjValues.ShowWindowsName("DP705"))
'+Se llaman los procedimientos que definen el grid
Call insDefineHeader()

mobjGrid.ActionQuery = Session("bQuery")
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP705Upd()
Else
	Call insPreDP705()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




