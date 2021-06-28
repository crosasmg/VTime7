<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
    
    Dim lclsProdmaster As eProduct.Product
    lclsProdmaster = New eProduct.Product

    Call lclsProdmaster.FindProdMaster(mobjValues.StringToType(Session("nBranch_CA985"), eFunctions.Values.eTypeData.etdDouble), _
                                       mobjValues.StringToType(Session("nProduct_CA985"), eFunctions.Values.eTypeData.etdDouble))

	mobjGrid.sCodisplPage = "CA985"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePolitypeColumnCaption"), "cbePolitype", "Table17", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbePolitypeColumnToolTip"))
            Call .AddNumericColumn(0, "Folio inicial", "tcnStart", 10, "", , GetLocalResourceObject("tcnStartColumnToolTip"))
            Call .AddNumericColumn(0, "Folio final", "tcnEnd", 10, "", , GetLocalResourceObject("tcnEndColumnToolTip"))
            Call .AddNumericColumn(0, "Póliza inicial", "tcnStartPolNumber", 10, "", , GetLocalResourceObject("tcnStartColumnToolTip"))
            Call .AddNumericColumn(0, "Póliza final", "tcnEndPolNumber", 10, "", , GetLocalResourceObject("tcnEndColumnToolTip"))

        End With
	
	'+ Se definen las propiedades generales del grid
	
        With mobjGrid
            .Codispl = "CA985"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 400
            .Width = 280
            .AddButton = True
            .DeleteButton = True
            
            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
                .ActionQuery = True
            End If
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
			
            .Columns("cbePolitype").Disabled = (Request.QueryString.Item("Action") = "Update")
            .Columns("tcnStart").Disabled = (Request.QueryString.Item("Action") = "Update")
            
            
            
            
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbePolitype").EditRecord = True
            .sDelRecordParam = "nStart='+ marrArray[lintIndex].tcnStart + '" + "&sPolitype='+ marrArray[lintIndex].cbePolitype + '"

'+  El campo "Tipo de póliza" para los productos SOAP solo salga la opción "Individual"
            If lclsProdmaster.sBrancht = "6" Then
                .Columns("cbePolitype").TypeList = Values.ecbeTypeList.Inclution
                .Columns("cbePolitype").List = "1"
            End If
           
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
                
            End If
        End With
        lclsProdmaster = Nothing
End Sub

'% insPreCA985: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
    Private Sub insPreCA985()
        '--------------------------------------------------------------------------------------------
        Dim lclsFolios_Agent As ePolicy.Folios_Agent
        Dim lcolFolios_Agents As ePolicy.Folios_Agents
	
        lcolFolios_Agents = New ePolicy.Folios_Agents
	
        If lcolFolios_Agents.Find(mobjValues.StringToType(Session("nBranch_CA985"), eFunctions.Values.eTypeData.etdDouble), _
                              mobjValues.StringToType(Session("nProduct_CA985"), eFunctions.Values.eTypeData.etdDouble), _
                              mobjValues.StringToType(Session("nIntermed_CA985"), eFunctions.Values.eTypeData.etdDouble), _
                              mobjValues.StringToType(Session("dAssign_date_CA985"), eFunctions.Values.eTypeData.etdDate)) Then
		
            For Each lclsFolios_Agent In lcolFolios_Agents
                With mobjGrid
                    
                    ' Si el movimiento esta procesado no se puede editar ni eliminar
                    'If lclsFolios_Agent.sProcessInd = "1" Then
                    '    .Columns("Sel").Disabled = True
                    '    .Columns("cbePolitype").EditRecord = False
                    'Else
                    '    .Columns("Sel").Disabled = False
                    '    .Columns("cbePolitype").EditRecord = true
                    'End If
                    
                    .Columns("cbePolitype").DefValue = lclsFolios_Agent.sPolitype
                    .Columns("tcnStart").DefValue = lclsFolios_Agent.nStart
                    .Columns("tcnEnd").DefValue = lclsFolios_Agent.nEnd
                    .Columns("tcnStartPolNumber").DefValue = lclsFolios_Agent.nStartPolNumber
                    .Columns("tcnEndPolNumber").DefValue = lclsFolios_Agent.nEndPolNumber

                    Response.Write(.DoRow)
                End With
            Next lclsFolios_Agent
        End If
	
        Response.Write(mobjGrid.closeTable())
        '	End With
	
        mcolClass = Nothing
    End Sub
    
    '% insPreCA980Upd: Gestiona lo relacionado a la actualización de un registro del Grid
    '------------------------------------------------------------------------------------
    Private Sub insPreCA985Upd()
        '------------------------------------------------------------------------------------
        Dim lclsFolios_Agent As ePolicy.Folios_Agent
        lclsFolios_Agent = New ePolicy.Folios_Agent
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                
                lclsFolios_Agent.nBranch = mobjValues.StringToType(Session("nBranch_CA985"), eFunctions.Values.eTypeData.etdDouble)
                lclsFolios_Agent.nProduct = mobjValues.StringToType(Session("nProduct_CA985"), eFunctions.Values.eTypeData.etdDouble)
                lclsFolios_Agent.nIntermed = mobjValues.StringToType(Session("nIntermed_CA985"), eFunctions.Values.eTypeData.etdDouble)
                lclsFolios_Agent.dAssign_date = mobjValues.StringToType(Session("dAssign_date_CA985"), eFunctions.Values.eTypeData.etdDate)
                lclsFolios_Agent.sPolitype = mobjValues.StringToType(.QueryString.Item("sPolitype"), eFunctions.Values.eTypeData.etdDouble)
                lclsFolios_Agent.nStart = mobjValues.StringToType(.QueryString.Item("nStart"), eFunctions.Values.eTypeData.etdDouble)
                
                lclsFolios_Agent.Delete()
            End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valpolicytra.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), , CShort(.QueryString.Item("Index"))))
		
        End With
	
        lclsFolios_Agent = Nothing
    End Sub


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "CA985"
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 11/05/04 19:20 $|$$Author: Nvaplat7 $"

</SCRIPT>
<!-- aca va el include -->

<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA985", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA985" ACTION="ValPolicyTra.aspx?x=1">
    <%Response.Write(mobjValues.ShowWindowsName("CA985", Request.QueryString.Item("sWindowDescript")))

        Call insDefineHeader()
        
        If Request.QueryString.Item("Type") <> "PopUp" Then
            Call insPreCA985()
        Else
            Call insPreCA985Upd()
        End If

mobjGrid = Nothing
mobjValues = Nothing
%>

</FORM> 
</BODY>
</HTML>






