<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid


    '% insDefineHeader:Permite definir las columnas del grid, así como también de habilitar o inhabilitar
    '% los botones de agregar y cancelar.
    '-----------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '-----------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid

        mobjGrid.sCodisplPage = "dp001_k"

        '+ Se definen las columnas del Grid
        With mobjGrid.Columns
            Call .AddNumericColumn(41208, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 5, vbNullString, False, GetLocalResourceObject("tcnBranchColumnToolTip"))
            Call .AddTextColumn(41209, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, False, GetLocalResourceObject("tctDescriptColumnToolTip"))
            Call .AddTextColumn(41210, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString, False, GetLocalResourceObject("tctShort_desColumnToolTip"))
            Call .AddTextColumn(41211, GetLocalResourceObject("tctTabNameColumnCaption"), "tctTabName", 15, vbNullString, False, GetLocalResourceObject("tctTabNameColumnToolTip"),  ,  , "insUcase(this)")
            Call .AddPossiblesColumn(41207, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnCaption"))
        End With

        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "DP001_K"
            .Columns("tcnBranch").Disabled = Not (Request.QueryString.Item("Action") = "Add")

            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
                .Columns("Sel").GridVisible = False
                .ActionQuery = True
            End If

            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
                .Columns("tctDescript").EditRecord = True
                .AddButton = True
                .DeleteButton = True
                .Height = 270
                .Width = 320
                .sDelRecordParam = "nBranch=' + marrArray[lintIndex].tcnBranch + '"
                If Request.QueryString.Item("Reload") = "1" Then
                    .sReloadIndex = Request.QueryString.Item("ReloadIndex")
                End If
            End If
        End With
    End Sub

    '% insPreDP001: Se definen los objetos a ser utilizados a lo largo de la transacción.
    '-----------------------------------------------------------------------------------------
    Private Sub insPreDP001()
        '-----------------------------------------------------------------------------------------
        Dim lintCount As Short
        Dim lobjObject As Object
        Dim lintIndex As Object
        Dim lcolPolicys As ePolicy.Policys
        Dim lclsBranches As eProduct.Branches


        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("//% insPreZone: Se definen las acciones." & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insPreZone(llngAction){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	switch (llngAction){" & vbCrLf)
        Response.Write("	    case 301:" & vbCrLf)
        Response.Write("	    case 302:" & vbCrLf)
        Response.Write("	    case 401:" & vbCrLf)
        Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
        Response.Write("	        break;" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "SCRIPT>")



        '+ Se setean los objetos y se realiza el llamado al método que realiza la 
        '+ lectura de los registros a mostrar en las columnas del grid.
        lclsBranches = New eProduct.Branches
        lcolPolicys = New ePolicy.Policys

        If lcolPolicys.reaTable10(True) Then
            lintCount = 0

            For Each lobjObject In lcolPolicys
                With lobjObject
                    mobjGrid.Columns("tcnBranch").DefValue = .nBranch
                    mobjGrid.Columns("tctDescript").DefValue = .sDescript
                    mobjGrid.Columns("tctShort_des").DefValue = .sShort_des
                    mobjGrid.Columns("tctTabName").DefValue = .sTabname
                    mobjGrid.Columns("cbeStatregt").DefValue = .sStatregt
                    mobjGrid.Columns("Sel").OnClick = "valDelete(" & lintCount & ")"

                    Response.Write(mobjGrid.DoRow())
                End With

                lintCount = lintCount + 1

                If lintCount = 200 Then
                    Exit For
                End If
            Next lobjObject
        End If

        Response.Write(mobjGrid.closeTable())

        lcolPolicys = Nothing
        lobjObject = Nothing
        lclsBranches = Nothing
    End Sub

    '% insPreDP001Upd: Permite realizar el llamado a la ventana PopUp. Esta transacción posee una serie
    '% de validaciones cuando se está eliminando un registro del grid, es por eso que se agregó el manejo
    '% de la misma.
    '-----------------------------------------------------------------------------------------
    Private Sub insPreDP001Upd()
        '-----------------------------------------------------------------------------------------
        Dim lclsBranches As eProduct.Branches
        If Request.QueryString.Item("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())
            lclsBranches = New eProduct.Branches
            Call lclsBranches.insPostDP001("Delete", CInt(Request.QueryString.Item("nBranch")), " ", " ", " ", " ", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdInteger))
        End If

        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProduct.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))

        lclsBranches = Nothing
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp001_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\product\product\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
%>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>




<%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.MakeMenu("DP001", "DP001_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $|$$Author: Nvaplat61 $"

//% valDelete: Se verifica si se puede eliminar el registro
//------------------------------------------------------------------------------------------
function valDelete(nIndex){
//------------------------------------------------------------------------------------------
	self.document.cmdDelete.disabled = true;
	insDefValues('DeleteDP001', 'nBranch=' + marrArray[nIndex].tcnBranch + '&nIndex=' + nIndex)
}

//% insUcase: Conviete el contenido a mayúscula.
//------------------------------------------------------------------------------------------
function  insUcase(sField){
//------------------------------------------------------------------------------------------
	var lstrText=sField.value
	self.document.forms[0].tctTabName.value = lstrText.toUpperCase();
}

//% insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP001_K" ACTION="valProduct.aspx?mode=1">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreDP001()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreDP001Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




