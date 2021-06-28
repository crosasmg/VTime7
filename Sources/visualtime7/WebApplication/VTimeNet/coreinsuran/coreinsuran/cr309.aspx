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

'- Se define la variable para la carga del Grid de la ventana 'CR309'
    Dim mcolContrnp_Riskss As eCoReinsuran.Contrnp_Riskss
    Dim mclsContrnp_Risks As eCoReinsuran.Contrnp_Risks


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------	
        mobjGrid.sCodisplPage = "CR309"
	
        With mobjGrid
            .Codispl = "CR309"
            .Width = 400
            .Height = 260
            .Top = 170
        End With
	
        '+Se definen todas las columnas del Grid

        With mobjGrid.Columns
            Call .AddClientColumn(0, GetLocalResourceObject("tctCodeColumnCaption"), "tctCode", vbNullString, , GetLocalResourceObject("tctCodeColumnToolTip"), , , "lblCliename", False)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnSumInsuredColumnCaption"), "tcnSumInsured", 18, , , GetLocalResourceObject("tcnSumInsuredColumnToolTip"), True, 2)
            Call .AddHiddenColumn("sParam", vbNullString)
            Call .AddCheckColumn(0, GetLocalResourceObject("chkSpApply"), "chkSpApply", GetLocalResourceObject("chkSpApplyToolTip"))
        End With
	
        With mobjGrid
            .Columns("tctCode").EditRecord = True
            .DeleteButton = True
            .AddButton = True
            .sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
            .Columns("chkSpApply").GridVisible = False
            If Session("bQuery") Then
                .DeleteButton = False
                .AddButton = False
                .Columns("Sel").GridVisible = False
                .bOnlyForQuery = True
            End If
		
            If Request.QueryString.Item("Action") <> "Add" Then
                .Columns("tctCode").Disabled = True
            End If
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
        End With
    End Sub

    '%insPreCR309: Esta función se encarga de cargar los datos en la forma "Folder" 
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCR309()
        '--------------------------------------------------------------------------------------------
	
        Dim lintCount As Integer
	
        If mcolContrnp_Riskss.Find(Session("nNumber"), Session("nBranch_rei"), Session("dEffecdate"), Session("nType")) Then
            For Each mclsContrnp_Risks In mcolContrnp_Riskss
                With mobjGrid
                    .Columns("tctCode").DefValue = mclsContrnp_Risks.sClient
                    .Columns("tcnSumInsured").DefValue = CStr(mclsContrnp_Risks.nSumInsured)
                    .Columns("chkSpApply").DefValue = mclsContrnp_Risks.sSpcApply
                    If mclsContrnp_Risks.sSpcApply = "1" Then
                        .Columns("chkSpApply").Checked = 1
                    End If
                    '+ Se "arma" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
                    '+ función insPostCR309 cuando se eliminen los registros seleccionados
                    .Columns("sParam").DefValue = "sClient=" & mclsContrnp_Risks.sClient
					
                End With
                Response.Write(mobjGrid.DoRow())
            Next
				
        End If
        'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
        Response.Write(mobjGrid.closeTable())
    End Sub

'% insPreCR309Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías partocipantes
'--------------------------------------------------------------------------------------------------------------------
    Private Sub insPreCR309Upd()
        '--------------------------------------------------------------------------------------------------------------------		
        Dim lblnPost As Boolean
        Dim lintSel As Integer
        
        If Request.QueryString.Item("Action") = "Del" Then
            lintSel = 2
		
            Response.Write(mobjValues.ConfirmDelete())
            With Request
                Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR309", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            End With
		
            With Request
                lblnPost = mclsContrnp_Risks.insPostCR309(.QueryString.Item("Action"), Session("nNumber"), Session("nBranch_rei"), Session("dEffecdate"), Session("nType"), .QueryString.Item("sClient"), CDbl(.QueryString.Item("nSumInsured")), Session("nUsercode"),"")
            End With
            If lblnPost Then
                Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/CoReinsuran/CoReinsuran/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No&nOpener=" & Request.QueryString.Item("sCodispl") & """;</" & "Script>")
            End If
		
        Else
            With Request
                Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR309", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
                mobjGrid.Columns("tctCode").Disabled = False
            End With
        End If
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
    mcolContrnp_Riskss = New eCoReinsuran.Contrnp_Riskss
    mclsContrnp_Risks = New eCoReinsuran.Contrnp_Risks

mobjValues.sCodisplPage = "CR309"

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR309", "CR309.aspx"))
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

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCR309" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR309"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call insPreCR309()
Else
        Call insPreCR309Upd()
End If
%>
</FORM>
</BODY>
</HTML>
	




