<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>

<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>

<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores

    Dim mobjValues As eFunctions.Values
    Dim mclsPolicyTarifDetail As ePolicy.PolicyTarifDetail
    Dim mobjGrid As eFunctions.Grid
    Dim mclsProduct As eProduct.Product
    Dim mobjMenu As eFunctions.Menues


    '% insDefineHeader : Configura los datos del grid.
    '---------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------------
        mobjGrid.ActionQuery = Session("bQuery")

        '+ Se definen las columnas del grid
        With mobjGrid.Columns
            'Call .AddHiddenColumn("hddIdPolicyTarif", "")
            'Call .AddTextColumn(41459, GetLocalResourceObject("tcnPercentReturnColumnCaption"), "tcnPercentReturn", 9, "",  , GetLocalResourceObject("tcnPercentReturnColumnCaption"))
            Call .AddNumericColumn(420, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 5,  ,  , GetLocalResourceObject("tcnMonthColumnCaption"), True, 0)
            Call .AddNumericColumn(421, GetLocalResourceObject("tcnPercentReturnColumnCaption"), "tcnPercentReturn", 9,  ,  , GetLocalResourceObject("tcnPercentReturnColumnCaption"), True, 0)
            Call .AddNumericColumn(422, GetLocalResourceObject("tcnAmountReturnColumnCaption"), "tcnAmountReturn", 18,  ,  , GetLocalResourceObject("tcnAmountReturnColumnCaption"), True, 2)
        End With
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "CA072"
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = False
            '.Columns("Sel").OnClick = "insSelect(this);"
        End With
    End Sub

    '% insPreDP055 : Muestra los datos repetitivos de la página.
    '---------------------------------------------------------------------------------------------
    Private Sub InspreCA072()
        '---------------------------------------------------------------------------------------------
        Dim mclsPolicyTarifDetail_count As ePolicy.PolicyTarifDetail
        Dim lintCount As Integer

        mclsPolicyTarifDetail_count = New ePolicy.PolicyTarifDetail
        '+ Define las colmnas del grid.    
        Call insDefineHeader()

        '+ Se obtienen los datos de la grilla
        If mclsPolicyTarifDetail_count.LoadTarifDetail(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
            '+ Se obtienen los datos del producto
            'Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

            '+ Se verifica si existen registros en Curren_pol                              
            'Call mclsCurren_pol_count.Count_Curren_pol(mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
            lintCount = 0
            For lintCount = 0 To mclsPolicyTarifDetail_count.CountPolicyTarif
                If mclsPolicyTarifDetail_count.Val_Policy_Tarif(lintCount) Then
                    With mobjGrid
                        '.Columns("hddIdPolicyTarif").DefValue = CStr(mclsPolicyTarifDetail_count.nIdPolicyTarif)
                        .Columns("tcnMonth").DefValue = CStr(mclsPolicyTarifDetail_count.nMonth)
                        .Columns("tcnPercentReturn").DefValue = CStr(mclsPolicyTarifDetail_count.nPercentReturn)
                        .Columns("tcnAmountReturn").DefValue = CStr(mclsPolicyTarifDetail_count.nAmountReturn)

                        Response.Write(mobjGrid.DoRow)
                    End With
                End If
            Next

        End If
        Response.Write(mobjGrid.closeTable())
        mobjValues = Nothing
        mclsPolicyTarifDetail = Nothing
        mclsPolicyTarifDetail_count = Nothing
        mobjGrid = Nothing
        mclsProduct = Nothing
        mobjMenu = Nothing
    End Sub

</script>

<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA072")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mclsPolicyTarifDetail = New ePolicy.PolicyTarifDetail
    mobjGrid = New eFunctions.Grid
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjGrid.sSessionID = Session.SessionID
    mobjGrid.nUsercode = Session("nUsercode")
    mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
    Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
    '~End Body Block VisualTimer Utility
    mclsProduct = New eProduct.Product

    mobjValues.ActionQuery = Session("bQuery")
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<!DOCTYPE html>

<HTML>
<HEAD>

 <%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
	<FORM METHOD="post" ID="FORM" NAME="CA072" ACTION="valPolicySeq.aspx?">	
<%
    Call InspreCA072()
%>
</TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Call mobjNetFrameWork.FinishPage("CA072")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>