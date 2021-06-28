<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores.

    Dim mobjValues As eFunctions.Values

    Dim mobjMenu As eFunctions.Menues

    Dim mclsLife_speci As eProduct.Life_speci

    Dim mcolLife_specis As eProduct.Life_specis

    '- Objeto para el manejo del grid. 

    Dim mobjGrid As eFunctions.Grid

    Dim mstrBranch As Object
    Dim mstrProduct As Object
    Dim mstrCover As String
    Dim mstrEffecdate As Object
    Dim mstrModulec As String


    '% insDefineHeader: Se definen los campos del grid.
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------  
        mobjGrid = New eFunctions.Grid

        mobjGrid.sCodisplPage = "DP027"
        '+ Se definen las columnas del grid.

        With mobjGrid.Columns
            If IsNothing(Request.QueryString.Item("Action")) Then
                Call .AddNumericColumn(0, GetLocalResourceObject("nConsecColumnCaption"), "nConsec", 10, CStr(0),  , GetLocalResourceObject("nConsecColumnToolTip"),  ,  ,  ,  ,  , True)
            Else
                Call .AddHiddenColumn("nConsec", CStr(0))
            End If
            Call .AddPossiblesColumn(100406, GetLocalResourceObject("sSexinsurColumnCaption"), "sSexinsur", "Table18", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("sSexinsurColumnToolTip"))
            Call .AddNumericColumn(100408, GetLocalResourceObject("nAgestartColumnCaption"), "nAgestart", 3, "",  , GetLocalResourceObject("nAgestartColumnToolTip"))
            Call .AddNumericColumn(100409, GetLocalResourceObject("nAgeendColumnCaption"), "nAgeend", 3, "",  , GetLocalResourceObject("nAgeendColumnToolTip"))
            Call .AddNumericColumn(100410, GetLocalResourceObject("nCapstartColumnCaption"), "nCapstart", 18, "",  , GetLocalResourceObject("nCapstartColumnToolTip"), True, 6)
            Call .AddNumericColumn(100411, GetLocalResourceObject("nCapendColumnCaption"), "nCapend", 18, "",  , GetLocalResourceObject("nCapendColumnToolTip"), True, 6)
            Call .AddPossiblesColumn(100407, GetLocalResourceObject("nCrthecniColumnCaption"), "nCrthecni", "Table32", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  ,  , 5, GetLocalResourceObject("nCrthecniColumnToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "tabtab_covrol3", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
        End With

        '+ Se definen las propiedades generales del grid.

        With mobjGrid
            .AddButton = False
            .DeleteButton = False
            .Height = 350
            .Width = 550
            .Codispl = "DP027"
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .sEditRecordParam = "cbenModule='+ self.document.forms[0].cbenModule.value + '&cbenCover=' + self.document.forms[0].cbenCover.value + '"
            .sDelRecordParam = "nConsec='+ marrArray[lintIndex].nConsec + '&cbenModule='+ self.document.forms[0].cbenModule.value + '&cbenCover=' + self.document.forms[0].cbenCover.value + '"

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .Columns("Sel").GridVisible = Not Session("bQuery")
            .Columns("sSexinsur").EditRecord = True

            '+ Se quita el registro 3 "No Informado" para Sexo            
            '.Columns("sSexinsur").TypeList = 2
            '.Columns("sSexinsur").List = 3        

            With .Columns("cbeRole").Parameters
                .Add("nBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", mstrProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCover", mstrCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", mstrEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nModulec", mstrModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With

        End With
    End Sub

    '% insPreDP027: Se cargan los controles de la página.
    '--------------------------------------------------------------------------------------------
    Private Sub insPreDP027()
        '--------------------------------------------------------------------------------------------
        Dim lblnDataFound As Boolean
        Dim lintIndex As Short
        Dim mclsProductli As eProduct.Product
        Dim mclsProduct_ge As eProduct.Product_ge
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim mclsProduct As eProduct.Product
        Dim lindexnModule As Object
        Dim lindexnCover As Object
        Dim lblnModulec As Boolean

        lintIndex = 0
        lclsGeneral = New eGeneral.GeneralFunction

        mclsProductli = New eProduct.Product
        mclsProduct = New eProduct.Product
        mclsProduct_ge = New eProduct.Product_ge

        '+ Si tiene módulos asociados
        lblnModulec = mclsProduct.IsModule(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))


        '+ Setea las variables, si son nulas le asignan 0

        If mobjValues.StringToType(Request.QueryString.Item("cbenModule"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
            lindexnModule = 0
        Else
            lindexnModule = Request.QueryString.Item("cbenModule")
        End If

        If mobjValues.StringToType(Request.QueryString.Item("cbenCover"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
            lindexnCover = 0
        Else
            lindexnCover = Request.QueryString.Item("cbenCover")
        End If

        Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

        'If IIf(IsNothing(Request.QueryString.Item("nMainAction")),False,Request.QueryString.Item("nMainAction")) <> Not Session("bQuery") Then
        '+ Si es de vida 
        If CStr(Session("sBrancht")) = "1" Then
            If mclsProductli.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                '+ Si no se ha llenado la ventana de características de vida se envía error	                             
                If mclsProductli.nCurrency = 0 Then
                    Response.Write("<SCRIPT> alert(""" & lclsGeneral.insLoadMessage(11414) & """); </" & "Script> ")
                    mobjGrid.AddButton = False
                    mobjGrid.DeleteButton = False
                Else

                    lblnDataFound = mcolLife_specis.FindLife_speci(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(lindexnModule, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble))
                    '+ Si ingresó la cobertura se habilitan los botones
                    If mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                        '+ Se habilitan los botones
                        mobjGrid.AddButton = True
                        mobjGrid.DeleteButton = True
                    End If
                    '+ Si encuentra datos en Life_speci					            					       
                    If lblnDataFound Then
                        If mclsProductli.nCurrency <> mcolLife_specis.nCurrencyAux Then
                            Response.Write("<SCRIPT> alert(""" & "11407: " & lclsGeneral.insLoadMessage(11407) & """); </" & "Script> ")
                        End If
                        '+ Se habilitan los botones
                        mobjGrid.AddButton = True
                        mobjGrid.DeleteButton = True
                    Else
                        '+ Si no tiene módulo asociado e ingresó una cobertura se habilitan los botones
                        If Not lblnModulec And lindexnCover <> "0" Then
                            mobjGrid.AddButton = True
                            mobjGrid.DeleteButton = True
                        End If
                    End If
                End If
            End If
        Else
            '+ Si el producto es de generales	      
            If mclsProduct_ge.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

                lblnDataFound = mcolLife_specis.FindLife_speci(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(lindexnModule, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble))
                '+ Si ingresó la cobertura se habilitan los botones
                If mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                    '+ Se habilitan los botones
                    mobjGrid.AddButton = True
                    mobjGrid.DeleteButton = True
                End If
            End If
        End If
        'End If

        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TD VALIGN=TOP>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH =""15%""><LABEL ID=14390>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        '+ Si el producto es de vida se asocia la moneda que tiene Product_li            
        If CStr(Session("sBrancht")) = "1" Then
            Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProductli.nCurrency),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 1))
            Response.Write(mobjValues.HiddenControl("nCurrency", CStr(mclsProductli.nCurrency)))
        Else
            '+ Si el producto es de generales se asocia la moneda que tine Product_ge                
            Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct_ge.nCurrency),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 1))
            Response.Write(mobjValues.HiddenControl("nCurrency", CStr(mclsProduct_ge.nCurrency)))
        End If
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>            " & vbCrLf)
        Response.Write("			<TD WIDTH =""15%""><LABEL>" & GetLocalResourceObject("cbenModuleCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        With mobjValues
            Call .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Response.Write(mobjValues.PossiblesValues("cbenModule", "tabtab_modul", eFunctions.Values.eValuesType.clngComboType, .StringToType(lindexnModule, eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "ShowReceipts(cbenModule,cbenCover,""" & lblnModulec & """)", Not lblnModulec, 5, GetLocalResourceObject("cbenModuleToolTip")))

        End With

        Response.Write("</TD> " & vbCrLf)
        Response.Write("            <TD WIDTH =""15%""><LABEL>" & GetLocalResourceObject("cbenCoverCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        With mobjValues
            '+ Verifica si el producto es de "vida" o de "generales".
            If CStr(Session("sBrancht")) = "1" Then
                '+ Si es un producto de vida.
                .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModule", lindexnModule, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("cbenCover", "tablife_covmod", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "ShowReceipts(cbenModule,cbenCover,""" & lblnModulec & """)", lblnModulec And lindexnModule = 0, 5, GetLocalResourceObject("cbenCoverToolTip")))
            Else
                '+ Si es un producto de generales.
                .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModule", lindexnModule, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("cbenCover", "tabgen_cover3", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "ShowReceipts(cbenModule,cbenCover,""" & lblnModulec & """)", lblnModulec And lindexnModule = 0, 5, GetLocalResourceObject("cbenCoverToolTip")))
            End If
        End With

        Response.Write("</TD>                   " & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("    </TD>		" & vbCrLf)
        Response.Write("</TABLE>											       " & vbCrLf)
        Response.Write("")


        If lblnDataFound Then
            For Each mclsLife_speci In mcolLife_specis
                With mobjGrid
                    .Columns("nConsec").DefValue = CStr(mclsLife_speci.nConsec)
                    .Columns("sSexinsur").DefValue = mclsLife_speci.sSexinsur
                    .Columns("nAgestart").DefValue = CStr(mclsLife_speci.nAgeStart)
                    .Columns("nAgeend").DefValue = CStr(mclsLife_speci.nAgeend)
                    .Columns("nCapstart").DefValue = CStr(mclsLife_speci.nCapstart)
                    .Columns("nCapend").DefValue = CStr(mclsLife_speci.nCapEnd)
                    .Columns("nCrthecni").DefValue = CStr(mclsLife_speci.nCrthecni)
                    .Columns("cbeRole").DefValue = CStr(mclsLife_speci.nRole)

                    Response.Write(.DoRow)
                End With

                lintIndex = lintIndex + 1

                '  			If lintIndex = 200 Then
                '  				Exit For
                '  			End If

            Next mclsLife_speci
        End If

        Response.Write(mobjGrid.closeTable)

        mcolLife_specis = Nothing
        mclsLife_speci = Nothing
    End Sub

    '% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
    '% un registro. 
    '-----------------------------------------------------------------------------------------
    Private Sub insPreDP027Upd()
        '-----------------------------------------------------------------------------------------
        Dim lclsProduct_Win As eProduct.Prod_win
        lclsProduct_Win = New eProduct.Prod_win

        If Request.QueryString.Item("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())

            Call mclsLife_speci.insPostDP027("Delete", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0, 0, 0, 0, "", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbenModule"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbenCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbeRole"), eFunctions.Values.eTypeData.etdLong))

            '+ Se verifica si existen registros
            Call mclsLife_speci.FindCurrency(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
            '+ Si se eliminaron todos los registros de la tabla se actualiza Prod_win con el valor sin contenido		
            If mclsLife_speci.nCurrency = 0 Then
                Call lclsProduct_Win.Add_Prod_win(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), "DP027", "1", Session("nUsercode"))
            End If
        End If

        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))

        Response.Write(mobjValues.HiddenControl("cbenModule", Request.QueryString.Item("cbenModule")))
        Response.Write(mobjValues.HiddenControl("cbenCover", Request.QueryString.Item("cbenCover")))
        Response.Write(mobjValues.HiddenControl("nCurrencyAux", "0"))

        Response.Write("<SCRIPT>self.document.forms[0].nCurrencyAux.value=top.opener.document.forms[0].nCurrency.value;</" & "Script>")
        '+ Se actualiza la página del menú    
        Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")

        lclsProduct_Win = Nothing
        mclsLife_speci = Nothing
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsLife_speci = New eProduct.Life_speci
mcolLife_specis = New eProduct.Life_specis

mobjValues.sCodisplPage = "DP027"

mstrBranch = Session("nBranch")
mstrProduct = Session("nProduct")
mstrEffecdate = Session("dEffecdate")
mstrCover = Request.QueryString.Item("cbenCover")
mstrModulec = Request.QueryString.Item("cbenModule")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 25-09-09 23:53 $|$$Author: Ljimenez $"

//% ShowReceipts: Esta función se encarga de dibujar una tabla con el contenido de los datos 
//% del coverturas seleccionadas el cual se encuentra almecenado en el arreglo.                   
//---------------------------------------------------------------------------------------------------------*/
function ShowReceipts(cbenModule,cbenCover,lbnModulec)
/*---------------------------------------------------------------------------------------------------------*/
{
    var lstrstring = "";
    if (cbenModule.value == "0" && lbnModulec != "False" && lbnModulec != "Falso")
		cbenCover.value = "";
    lstrstring += document.location;
	lstrstring = lstrstring.replace(/&cbenModule=.*/, "");
	lstrstring = lstrstring.replace(/&cbenCover=.*/, "");
	lstrstring = lstrstring.replace(/&Reload=.*/, "");
	lstrstring = lstrstring + "&cbenCover="+cbenCover.value + "&cbenModule="+cbenModule.value  + "&reload=2";
	document.location = lstrstring;	
}

/*---------------------------------------------------------------------------------------------------------*/
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP027", "DP027.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP027" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nCurrency=<%=Request.Form.Item("cbeCurrency")%>">
<%

Response.Write(mobjValues.ShowWindowsName("DP027"))
Call insDefineHeader()

mobjGrid.ActionQuery = Session("bQuery")
    
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP027Upd()
Else
	Call insPreDP027()
End If

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






