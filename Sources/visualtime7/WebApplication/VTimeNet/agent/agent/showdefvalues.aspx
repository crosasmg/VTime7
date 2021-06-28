<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eProduct" %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<script language="VB" runat="Server">

'-Variable para el manejo de funciones generales
Dim mobjValues As eFunctions.Values


'% inscalExchange: Se calcula el factor de cambio para una fecha-moneda.
'%				   Se invoca desde la MGS001
'--------------------------------------------------------------------------------------------
Sub inscalExchange()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
        Call lclsExchange.Convert(0, 0, mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0)
	Response.Write("top.frames['fraFolder'].document.forms[0].hddExchange.value=" & lclsExchange.pdblExchange & ";")
	Response.Write("top.frames['fraFolder'].ShowChangeAmount();")
	lclsExchange = Nothing
End Sub

    '% ChangesAmount: Realiza la conversion de acuerdo a la moneda especificada.
    '--------------------------------------------------------------------------------------------
    Sub ChangesAmount()
        '--------------------------------------------------------------------------------------------
        Dim lclsExchanges As eGeneral.Exchange

        lclsExchanges = New eGeneral.Exchange
    
        Call lclsExchanges.Convert(eRemoteDB.Constants.intNull, _
                                   Request.QueryString("nLoanAmount"), _
                                   Session("hddCurrCommBase"), _
                                   Request.QueryString("nCurrencyDes"), _
                                   Request.QueryString("Date"), _
                                   0)
        Response.Write("top.frames['fraFolder'].document.forms[0].tcnCommBase.value = VTFormat('" & lclsExchanges.pdblResult & "', '', '', '', 6, true);")
    
        lclsExchanges = Nothing
    End Sub

    '% insInterm_typ: Actualiza el tipo de intermediario en el control
    '--------------------------------------------------------------------------------------------
    Sub insInterm_typ()
        '--------------------------------------------------------------------------------------------

        Dim lobjIntermedia As New eAgent.Intermedia
        Dim lobjInterm_typ As New eAgent.Interm_typ
	
        If lobjIntermedia.Find(mobjValues.StringToType(request.QueryString("nIntermed"), eFunctions.Values.eTypeData.etdDouble)) Then
            If lobjInterm_typ.Find(lobjIntermedia.nIntertyp) Then
                Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
                Response.Write("    cbeIntertyp.Parameters.Param1.sValue='" & mobjValues.StringToType(Request.QueryString("nIntermed"), eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("    cbeIntertyp.value='" & lobjInterm_typ.nIntertyp & "';")
                Response.Write("}")
                response.Write("top.fraHeader.UpdateDiv('cbeIntertypDesc','" & lobjInterm_typ.sDescript & "','Normal');")
            End If
        End If
	
        lobjIntermedia = Nothing
        lobjInterm_typ = Nothing
    End Sub


    '% insDelLoans:
    '--------------------------------------------------------------------------------------------
    Sub insDelLoans(ByVal Intermed, ByVal Loans)
        '--------------------------------------------------------------------------------------------
        Dim lobjLoans_int As New eAgent.Loans_int

        'lobjLoans_int.Delete(mobjValues.StringToType(Intermed, eFunctions.Values.eTypeData.etdDouble), _
        '                        mobjValues.StringToType(Loans, eFunctions.Values.eTypeData.etdDouble))
	
        lobjLoans_int = Nothing
    End Sub

    '% insPolicy: AG004, Rescata monto de la comision del intermediario para la poliza indicada 
    '% en la moneda seleccionada.
    '-----------------------------------------------------------------------------------------------
    Sub insPolicy()
        '--------------------------------------------------------------------------------------------
        Dim lobjLoans_int As New eAgent.Loans_int
        Dim nTotAmount
        Dim nValue
        Dim nPercentP
    

        If lobjLoans_int.Find_Commiss(mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                      mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                 mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                      mobjValues.StringToType(Session("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), _
                                      mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble), _
                                      mobjValues.StringToType(Request.QueryString("Date"), eFunctions.Values.eTypeData.etdDate)) Then
            '+Se guarda la moneda del monto de comision de la poliza
            Session("hddCurrCommBase") = lobjLoans_int.nCurr_amount
            '    	nTotAmount = cdbl(lobjLoans_int.nCommBase)-cdbl(lobjLoans_int.nAmount_loans)    	

            If mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                nTotAmount = CDbl(Math.Round(lobjLoans_int.nCommBase, 0))
            Else
                nTotAmount = lobjLoans_int.nCommBase
            End If
    	
            nPercentP = mobjValues.StringToType(Request.QueryString("nPercent"), eFunctions.Values.eTypeData.etdDouble)
            If mobjValues.StringToType(Request.QueryString("nPercent"), eFunctions.Values.eTypeData.etdDouble) <> 0 And _
               mobjValues.StringToType(Request.QueryString("nPercent"), eFunctions.Values.eTypeData.etdDouble) <> "" Then
                nValue = nTotAmount * nPercentP / 100
                If mobjValues.StringToType(Request.QueryString("nCurrency"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                    nValue = CDbl(Math.Round(nValue, 0))
                End If
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnLoanAmount.value = '" & mobjValues.TypeToString(nValue, eFunctions.Values.eTypeData.etdDouble, , 6) & "';")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnLoanBalance.value = '" & mobjValues.TypeToString(nValue, eFunctions.Values.eTypeData.etdDouble, , 6) & "';")
                If mobjValues.StringToType(Request.QueryString("nLoanType"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                    Response.Write(" top.frames['fraFolder'].document.forms[0].tcnMonthly.value = '" & mobjValues.TypeToString(nValue, eFunctions.Values.eTypeData.etdDouble, , 6) & "';")
                    Response.Write(" top.frames['fraFolder'].document.forms[0].cbePayForm.value = 2;")
                End If
            End If

            If lobjLoans_int.nRentVita <> 9 And _
               lobjLoans_int.nRentVita <> 10 Or _
               lobjLoans_int.nRentVita = eRemoteDB.Constants.intNull Then
                Response.Write("with (top.frames['fraFolder'].document.forms[0]) {")
                Response.Write(" cbeMode.disabled=true;")
                Response.Write(" tcnCommBase.value = '" & mobjValues.TypeToString(nTotAmount, eFunctions.Values.eTypeData.etdDouble, , 6) & "';")
                Response.Write(" tcnPercent.disabled=false;")
                Response.Write(" tcnPercent.focus();")
                Response.Write(" } ")
            Else
                'Si ramo pertenece a Rentas vitalicias se habilita modalidad de anticipo
                Response.Write("with (top.frames['fraFolder'].document.forms[0]) {")
                Response.Write(" cbeMode.disabled=false;")
                Response.Write(" cbeMode.value = 0;")
                Response.Write(" tcnCommBase.value = '" & mobjValues.TypeToString(nTotAmount, eFunctions.Values.eTypeData.etdDouble, , 6) & "';")
                Response.Write(" tcnPercent.disabled=false;")
                Response.Write(" cbeMode.focus();")
                Response.Write(" } ")
            End If
        Else
            Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
            Response.Write(" tcnPercent.value=VTFormat(0, '', '', '', 2, true);")
            Response.Write(" cbeMode.value=0;")
            Response.Write(" tcnCommBase.value=VTFormat(0, '', '', '', 6, true);")
            Response.Write(" cbeMode.disabled=true;")
            Response.Write(" tcnPercent.disabled=true;")
            Response.Write(" }")
            Dim lobjErrors As New eGeneral.GeneralFunction
            Response.Write(" alert('Adv. 56034: " & lobjErrors.insLoadMessage(56034) & "');")
            lobjErrors = Nothing
        End If

        lobjLoans_int = Nothing
    End Sub

    '% updInterType:
    '--------------------------------------------------------------------------------------------
    Sub updInterType(ByVal Intermed)
        '----------------------------------- ---------------------------------------------------------
        Dim lobjintermed As New eAgent.Intermedia
        Dim lobjintertype As New eAgent.Interm_typ
        If lobjintermed.Find(Intermed) Then
            If lobjintertype.Find(lobjintermed.nIntertyp) Then
                Response.Write("top.fraHeader.UpdateDiv('lblInterType','" & lobjintertype.sDescript & "','Normal');")
                Session("lblInterType") = lobjintertype.sDescript
            End If
        Else
            Response.Write("top.fraHeader.UpdateDiv(""lblInterType"",'" & "" & "','PopUp');")
            Session("lblInterType") = ""
        End If
        lobjintermed = Nothing
        lobjintertype = Nothing
    End Sub

    '% insInterm_typ: Actualiza el tipo de intermediario en el control
    '--------------------------------------------------------------------------------------------
    Sub insInterm_Goals()
        '--------------------------------------------------------------------------------------------
        Dim lobjIntermedia As New eAgent.Intermedia
        Dim lobjGoals As New eAgent.Goals
        Dim nGoal
	
	
        If lobjIntermedia.Find(mobjValues.StringToType(request.QueryString("nIntermed"), eFunctions.Values.eTypeData.etdDouble)) Then
            If session("nMulticompany") = 1 Then
                nGoal = lobjIntermedia.nGoal_Life
            Else
                nGoal = lobjIntermedia.nGoal_Gen
            End If
            Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
            Response.Write("    valGoals.value='" & mobjValues.TypeToString(nGoal, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("    top.frames['fraHeader'].$('#valGoals').change();")
            Response.Write("}")
        End If
	
        lobjIntermedia = Nothing
        lobjGoals = Nothing
    End Sub
    '% insDelLoans:
    '--------------------------------------------------------------------------------------------
    Sub insProduct()
        '--------------------------------------------------------------------------------------------
        Dim lobjProduct As New eProduct.Product

        If lobjProduct.FindProdMasterActive(mobjValues.StringToType(request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
              mobjValues.StringToType(request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
            If lobjProduct.sBrancht = "1" Then
                If lobjProduct.FindProduct_li(mobjValues.StringToType(request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                            mobjValues.StringToType(request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                         mobjValues.StringToType(request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
                    If (lobjProduct.nProdClas = 9 Or _
                       lobjProduct.nProdClas = 10) Then
                        Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.value='3';")
                        Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.disabled=false;")
                    Else
                        Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.value='0';")
                        Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.disabled=false;")
                    End If
                Else
                    Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.value='0';")
                    Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.disabled=false;")
                End If
            Else
                Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.value='0';")
                Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.disabled=false;")
            End If
        Else
            Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.value='0';")
            Response.Write("top.frames['fraFolder'].document.forms[0].cbePayOrder.disabled=false;")
        End If
        lobjProduct = Nothing
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
        Case "nExchange"
            Call inscalExchange()
        Case "delLoans"
            Call insDelLoans(Request.QueryString("Intermed"), _
                             Request.QueryString("Loans"))
        Case "inter_type"
            Call updInterType(mobjValues.StringToType(Request.QueryString("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
        Case "Interm_typ"
            Call insInterm_typ()
        Case "Policy"
            Call insPolicy()
        Case "Intermed"
            Call insInterm_Goals()
        Case "Exchange"
            Call ChangesAmount()
        Case "Product"
            Call insProduct()
    End Select

    
    
    
    
    Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
    Response.Write("</SCRIPT>")

    mobjValues = Nothing

%>






