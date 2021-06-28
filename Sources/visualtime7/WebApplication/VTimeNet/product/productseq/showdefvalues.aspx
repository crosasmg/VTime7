<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mblnRefresh As Boolean

    '% InsDP809A: Inserta los datos de la transacción DP809A a la tabla Section_prod
    '--------------------------------------------------------------------------------------------
    Sub InsDP809A()
        '--------------------------------------------------------------------------------------------
        Dim lcolSection_prod As eProduct.Section_prod
 
        lcolSection_prod = New eProduct.Section_prod
	
        If lcolSection_prod.insPostDP809(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                 mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                 Request.QueryString("sCodispl_orig"), _
                 Request.QueryString("sCodispl"), _
                 mobjValues.StringToType(Request.QueryString("sselected"), eFunctions.Values.eTypeData.etdDouble), _
                 mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                 mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
        End If
	
        lcolSection_prod = Nothing
    End Sub

    '% insCancel: acción Cancelar de la secuencia
    '%			  Se utiliza cuando se presiona el botón cancelar de la ventana principal (DP003_K)
    '--------------------------------------------------------------------------------------------
    Sub insCancelDP()
        '--------------------------------------------------------------------------------------------
        Dim lstrLocationBC003 As String
        Dim lclsProduct As eProduct.Product
	
        lclsProduct = New eProduct.Product
        lstrLocationBC003 = "'/VTimeNet/Common/GoTo.aspx?sCodispl=DP003_K'"
    
        With lclsProduct
            If .FindProdMasterActive(Session("nBranch"), _
             Session("nProduct")) Then
                If .sDescript = vbNullString Then
                    .nBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
                    .nProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
                
                    Call .UpdProdmaster_Desc()
                End If
            End If
        End With
    
        With Response
            .Write("opener.top.document.location=" & lstrLocationBC003 & ";")
            .Write("window.close();")
        End With
	
        lclsProduct = Nothing
    End Sub

    '% inscreTab_bill: se crean los conceptos de facturación del producto en base a los conceptos
    '%				   de facturación generales 
    '--------------------------------------------------------------------------------------------
    Sub inscreTab_bill()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_bill_i As eProduct.Tab_bill_i
		
        lclsTab_bill_i = New eProduct.Tab_bill_i
    
        If lclsTab_bill_i.AddDefaultValue(Session("nBranch"), _
                 Session("nProduct"), _
                 Session("deffecdate"), _
               Session("nUsercode")) Then
            Response.Write("top.frames['fraSequence'].document.location='/VTIMENET/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "&sGoToNext=NO" & "';")
            Response.Write("top.frames[""fraFolder""].location.reload();")
        End If

        lclsTab_bill_i = Nothing
    End Sub

    '% inscreTab_short: se crean la tabla de corto plazo del producto en base a la tabla de corto 
    '%					plazo general 
    '--------------------------------------------------------------------------------------------
    Sub inscreTab_short()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_short As eProduct.Tab_short
        Dim lclsProductWin As eProduct.Prod_win
		
        lclsTab_short = New eProduct.Tab_short
        lclsProductWin = New eProduct.Prod_win
    
        If lclsTab_short.AddDefaultValue(Session("nBranch"), _
                                         Session("nProduct"), _
                                         Session("deffecdate"), _
                                         Session("nUsercode")) Then
            If lclsProductWin.Add_Prod_win(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), "DP037", "2", Session("nUserCode")) Then
                Response.Write("top.frames['fraSequence'].document.location='/VTIMENET/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&nOpener=" & Request.QueryString("sCodispl") & "&sGoToNext=NO" & "';")
                Response.Write("top.frames[""fraFolder""].location.reload();")
            End If
        End If

        lclsTab_short = Nothing
        lclsProductWin = Nothing
    End Sub

    '% valAssociate: se verifica que el concepto de facturación no tenga coberturas, recargos, 
    '%				 descuentos o impuestos asociados
    '--------------------------------------------------------------------------------------------
    Sub valAssociate()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_bill_i As eProduct.Tab_bill_i
        Dim lclsValues As eFunctions.Values
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lstrMessage As String
		
        lclsTab_bill_i = New eProduct.Tab_bill_i
        lclsValues = New eFunctions.Values
        lclsGeneral = New eGeneral.GeneralFunction
    
        If lclsTab_bill_i.valAssociate(Session("sBrancht"), _
                                       Session("nBranch"), _
                 Session("nProduct"), _
                 lclsValues.StringToType(Request.QueryString("nBill_item"), eFunctions.Values.eTypeData.etdDouble), _
               Session("deffecdate")) Then
            Response.Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("Index") & "])!='undefined')?")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("Index") & "].checked=false:")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
            lstrMessage = lclsGeneral.insLoadMessage(11415)
            Response.Write("alert(""Err 11415:  " & lstrMessage & """);")
        End If

        lclsTab_bill_i = Nothing
        lclsValues = Nothing
        lclsGeneral = Nothing
    End Sub

    '% ValDeleteDisexprc: Valida si existen  envía mensaje de error
    '--------------------------------------------------------------------------------------------
    Sub ValDeleteDisexprc()
        '--------------------------------------------------------------------------------------------
        '- Objeto para manejo de requisitos y exclusiones
        Dim lclsTab_reqexc As eProduct.Tab_reqexc
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lclsValues As eFunctions.Values
        Dim lstrMessage As String

        lclsTab_reqexc = New eProduct.Tab_reqexc
        lclsGeneral = New eGeneral.GeneralFunction
        lclsValues = New eFunctions.Values
	
        lstrMessage = lclsGeneral.insLoadMessage(11372)
	
        If lclsTab_reqexc.valTab_reqexc(lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                "3", _
                lclsValues.StringToType(Request.QueryString("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), _
                lclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

            Response.Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "])!='undefined')?")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false:")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
            Response.Write("alert(""Err 11372:  " & lstrMessage & """);")
    
        End If
        lclsTab_reqexc = Nothing
    End Sub

    '% ValDelete: Valida si existen Fracuencias de Pago asociadas y envía mensaje de error
    '--------------------------------------------------------------------------------------------
    Sub ValDelete()
        '--------------------------------------------------------------------------------------------
        Dim lclsFreq_way_prod As eProduct.Freq_way_prod
        Dim lclsValues As eFunctions.Values
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lstrMessage As String

        lclsGeneral = New eGeneral.GeneralFunction
        lclsFreq_way_prod = New eProduct.Freq_way_prod
        lclsValues = New eFunctions.Values
    
        lstrMessage = lclsGeneral.insLoadMessage(55882)
        '+ Se verifica que no existan frecuencias de pago asociadas	    
        If lclsFreq_way_prod.Find_O(lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
           lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
           lclsValues.StringToType(Request.QueryString("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), _
           lclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				    			
            Response.Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "])!='undefined')?")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false:")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
            Response.Write("alert(""Err 55882:  " & lstrMessage & """);")
		 
        End If
	
        lclsFreq_way_prod = Nothing
        lclsGeneral = Nothing
    End Sub

    '% valAssociate_Cover: se verifica que las coberturas no tengan requisitos y exclusiones 
    '%                     asociados
    '--------------------------------------------------------------------------------------------
    Sub valAssociate_Cover()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_reqexc As eProduct.Tab_reqexc
        Dim lclsValues As eFunctions.Values
        Dim lclsGeneral As Object
        Dim lstrMessage As String

        lclsTab_reqexc = New eProduct.Tab_reqexc
        lclsValues = New eFunctions.Values
			
        '+ Si la cobertura tiene requisitos y exclusiones asociados
        If lclsTab_reqexc.valTab_reqexc(Session("nBranch"), _
                   Session("nProduct"), _
                "2", _
                lclsValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble), _
                lclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            lclsGeneral = Server.CreateObject("eGeneral.GeneralFunction")
            Response.Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("Index") & "])!='undefined')?")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("Index") & "].checked=false:")
            Response.Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
            lstrMessage = lclsGeneral.insLoadMessage(11374)
            Response.Write("alert(""Err 11374:  " & lstrMessage & """);")
        End If

        lclsTab_reqexc = Nothing
        lclsValues = Nothing
        lclsGeneral = Nothing
    End Sub

    '% valModulDP032: se verifica que los módulos no tengan requisitos y exclusiones 
    '%                asociados, ni coberturas activas
    '--------------------------------------------------------------------------------------------
    Sub valModulDP032()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_reqexc As eProduct.Tab_reqexc
        Dim lclsValues As eFunctions.Values
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lclsTab_modul As eProduct.Tab_modul
        Dim lstrMessage As String

        lclsTab_reqexc = New eProduct.Tab_reqexc
        lclsValues = New eFunctions.Values
        lclsGeneral = New eGeneral.GeneralFunction
        lclsTab_modul = New eProduct.Tab_modul
    			
        '+ Si el módulo tiene requisitos y exclusiones asociados
        If lclsTab_reqexc.valTab_reqexc(Session("nBranch"), _
                   Session("nProduct"), _
                "1", _
                lclsValues.StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), _
                lclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            lstrMessage = lclsGeneral.insLoadMessage(11371)
            With Response
                .Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "])!='undefined')?")
                .Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false:")
                .Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
                .Write("alert(""Err 11371:  " & lstrMessage & """);")
            End With
        End If
	
        '+ Si el el módulo tiene coberturas asociadas
        If lclsTab_modul.valModul_Cover(Session("nBranch"), _
                Session("nProduct"), _
                lclsValues.StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), _
                lclsValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                Session("sBrancht")) Then
            lstrMessage = lclsGeneral.insLoadMessage(11389)
            With Response
                .Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "])!='undefined')?")
                .Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false:")
                .Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
                .Write("alert(""Err 11389:  " & lstrMessage & """);")
            End With
        End If
    
        Response.Write("top.frames[""fraFolder""].document.cmdDelete.disabled = false;")

        lclsTab_reqexc = Nothing
        lclsValues = Nothing
        lclsGeneral = Nothing
        lclsTab_modul = Nothing
    End Sub

    '% ValDeleteClause: se verifica que las cláusulas no tengan requisitos y exclusiones asociados
    '--------------------------------------------------------------------------------------------
    Private Sub ValDeleteClause()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_reqexc As eProduct.Tab_reqexc
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lstrMessage As String
	
        lclsTab_reqexc = New eProduct.Tab_reqexc

        If lclsTab_reqexc.valTab_Reqexc(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                        mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                        "4", _
                                        mobjValues.StringToType(Request.QueryString("nClause"), eFunctions.Values.eTypeData.etdDouble), _
                                        mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            lclsGeneral = New eGeneral.GeneralFunction
            lstrMessage = lclsGeneral.insLoadMessage(11371)
            With Response
                .Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "])!='undefined')?")
                .Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") & "].checked=false:")
                .Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
                .Write("alert(""Err 11371:  " & lstrMessage & """);")
            End With
        End If
        Response.Write("top.frames[""fraFolder""].document.cmdDelete.disabled = false;")
        lclsTab_reqexc = Nothing
        lclsGeneral = Nothing
    End Sub

    '% UpdateCapital: se actualiza el capital asociado a la cobertura-rol (DP770)
    '--------------------------------------------------------------------------------------------
    Private Sub UpdateCapital()
        '--------------------------------------------------------------------------------------------
        Dim lclsProd_addcap As eProduct.Prod_addcap
	
        lclsProd_addcap = New eProduct.Prod_addcap

        If lclsProd_addcap.UpdateCapital(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                         mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                            mobjValues.StringToType(Request.QueryString("nCovergen"), eFunctions.Values.eTypeData.etdDouble), _
                                            mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                            mobjValues.StringToType(Request.QueryString("nRole"), eFunctions.Values.eTypeData.etdDouble), _
                                            mobjValues.StringToType(Request.QueryString("nCapital"), eFunctions.Values.eTypeData.etdDouble), _
                                            mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
        End If
        '	Response.Write "top.frames[""fraFolder""].document.cmdDelete.disabled = false;"
        lclsProd_addcap = Nothing
    End Sub


    '% ConsecuClause: Devuelve el numero de la proxima clausula
    '--------------------------------------------------------------------------------------------
    Sub ConsecuClause()
        '--------------------------------------------------------------------------------------------
        Dim lclsProd_tabclause As eProduct.Tab_Clause
        Dim var1 As String
        var1 = 666
	
        lclsProd_tabclause = New eProduct.Tab_Clause

        var1 = lclsProd_tabclause.Next_nclause(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                               mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        Response.Write("top.frames[""fraFolder""].document.forms[0].tcnClause.value=" & var1 & ";")
        lclsProd_tabclause = Nothing
    End Sub
    
    
'% InsDP809A: Inserta los datos de la transacción DP809A a la tabla Section_prod
'--------------------------------------------------------------------------------------------
Sub CalcPercent()
	'--------------------------------------------------------------------------------------------
	Dim npercent As Double
	
	npercent = 100 - CDbl(Request.QueryString.Item("nvalue"))
	
	If CDbl(Request.QueryString.Item("nvalue")) > 100 Then
		Response.Write("alert('Ingrese un porcentaje menor a cien');")
	Else
		If CDbl(Request.QueryString.Item("nvalue")) <= 0 Then
			Response.Write("alert('Debe ingresar un porcentaje mayor que cero');")
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0]." & Request.QueryString.Item("ndesvalue") & ".value = '" & CStr(npercent) & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].nCover.disabled = false;")
		End If
	End If
End Sub

'% InsDP809A: Inserta los datos de la transacción DP809A a la tabla Section_prod
'--------------------------------------------------------------------------------------------
Sub InsCA659A()
	'--------------------------------------------------------------------------------------------
	Dim lcolSection_pol As ePolicy.Section_pol
	
	lcolSection_pol = New ePolicy.Section_pol
	
	If lcolSection_pol.insPostCA659(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl_orig"), Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("sselected"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		
	End If
	
	lcolSection_pol = Nothing
End Sub

'% insShowPolicy: se muestran los datos asociados al número de póliza.
'%                Se utiliza para el campo Póliza de la página CA001_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowPolicy()
	'dim eRemoteDB.Constants.intNull As Integer
	Dim clngDuplPolicy As Object
	Dim clngTransHolder As Object
	'--------------------------------------------------------------------------------------------
	'- Objetos para leer informacion de base de datos 
	Dim lclsPolicy_po As ePolicy.Policy
	Dim lclsProcess As ePolicy.Process
	Dim lclsProduct_po As eProduct.Product
	Dim lclsMove_acc As eCashBank.Move_acc
	Dim lclsCertificat As ePolicy.Certificat
	Dim lclsCertificat_prop As ePolicy.Certificat
	Dim lcldPolicy_his As ePolicy.Policy_his
	
	Dim bFind_policy As Boolean
	Dim beffecdate_aux As Boolean
	'- Codigo del proceso con el que se actualizará en la tabla Process
	Dim llngCodeProce As Integer
	'-Codigo de la transaccion
	Dim lintTransaction As Object
	Dim lstrCertype As String
	Dim llngPolicy As Object
	Dim llngBranch As Object
	Dim llngProduct As Object
	
	beffecdate_aux = False
	
	With Server
		lclsPolicy_po = New ePolicy.Policy
		lclsProcess = New ePolicy.Process
		lclsProduct_po = New eProduct.Product
		lclsMove_acc = New eCashBank.Move_acc
		lclsCertificat = New ePolicy.Certificat
		lcldPolicy_his = New ePolicy.Policy_his
	End With
	
	lintTransaction = Request.QueryString.Item("nTransaction")
	
	If lintTransaction = eCollection.Premium.PolTransac.clngPolicyIssue Or lintTransaction = eCollection.Premium.PolTransac.clngdeclarations Or lintTransaction = eCollection.Premium.PolTransac.clngCertifIssue Or lintTransaction = eCollection.Premium.PolTransac.clngRecuperation Then
		llngCodeProce = 4
	End If
	
	If lintTransaction = eCollection.Premium.PolTransac.clngPolicyAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngCertifAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngQuotationConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuery Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuery Then
		llngCodeProce = 6
	End If
	
	If lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalQuery Then
		If Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
			lstrCertype = "7"
			llngPolicy = Request.QueryString.Item("nQuotProp")
		Else
			lstrCertype = Request.QueryString.Item("sCertype")
			llngPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
		End If
	Else
		lstrCertype = Request.QueryString.Item("sCertype")
		llngPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
	End If
	If Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
		If lintTransaction = "43" Or lintTransaction = "44" Then
			lstrCertype = "8"
			llngPolicy = Request.QueryString.Item("nQuotProp")
		End If
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.value = ''; ")
		Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.disabled = false; ")
		Response.Write("top.frames['fraHeader'].UpdateDiv('valType_amendDesc','');")
	End If
	
	If Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
		If lintTransaction = "37" Then
			lstrCertype = "4"
			llngPolicy = Request.QueryString.Item("nQuotProp")
		End If
	End If
	
	'+ se agrego este manejo para el numero unico de poliza
	If lclsPolicy_po.FindPolicybyPolicy(lstrCertype, llngPolicy, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
		If lclsPolicy_po.nProduct <> CDbl("") Then
			Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
		End If
		bFind_policy = True
	End If
	
	If bFind_policy Then
		Call lclsCertificat.Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
		
		'+ Asignación del campo canal de venta
		If lclsCertificat.nSellChannel <> eRemoteDB.Constants.intNull Then
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeSellchannel.value='" & lclsCertificat.nSellChannel & "';")
		End If
		'+ Asignación del digito verificador        
		If lclsCertificat.nDigit <> eRemoteDB.Constants.intNull Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy_Digit.value='" & lclsCertificat.nDigit & "';")
		End If
		'+ Asignación cantidad de renovaciones
		If lclsCertificat.nRenewalnum <> eRemoteDB.Constants.intNull Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnRenewalNum.value='" & lclsCertificat.nRenewalnum & "';")
		End If
		'+ Asignación propuesta regularizada
		
		If lintTransaction = eCollection.Premium.PolTransac.clngPolicyProposal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifProposal Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnProp_Reg.disabled=false;")
		End If
		
		If lclsCertificat.nProp_reg <> eRemoteDB.Constants.intNull Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnProp_Reg.value='" & lclsCertificat.nProp_reg & "';")
		End If
		
		'+ Fecha de ultimo cambio para proceso de modificacion
		If lintTransaction = eCollection.Premium.PolTransac.clngPolicyAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngCertifAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngTempCertifAmendment Then
			Session("dLastChange") = lclsCertificat.dChangdat
		End If
		
		Response.Write("with(top.frames['fraHeader'].document.forms[0]){")
		'+ Validaciones sobre el campo Transacción
		If (lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent) And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
			If lclsPolicy_po.dStartdate = eRemoteDB.Constants.dtmNull Then
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value=GetDateSystem();")
			Else
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy_po.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
			End If
			
		ElseIf (lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropRenewal) Then 
			If lclsPolicy_po.dExpirdat = eRemoteDB.Constants.dtmNull Then
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value=GetDateSystem();")
			Else
				beffecdate_aux = True
				If lclsProduct_po.Find(mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy_po.dExpirdat), eFunctions.Values.eTypeData.etdDate)) Then
					If CStr(lclsProduct_po.sBrancht) = "1" Then
'UPGRADE_NOTE: Date operands have a different behavior in arithmetical operations. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1023.htm
						Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(System.Date.FromOADate(lclsPolicy_po.dExpirdat.ToOADate + 1), eFunctions.Values.eTypeData.etdDate) & "';")
					Else
						Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy_po.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
					End If
				Else
					Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy_po.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
				Response.Write("tcdEffecdate.disabled=true;")
			End If
			If lcldPolicy_his.FindPropType_Hist("2", mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnQuotProp.value='" & mobjValues.TypeToString(lcldPolicy_his.nPropoNum, eFunctions.Values.eTypeData.etdDouble) & "';")
				If lcldPolicy_his.nPropoNum <> eRemoteDB.Constants.intNull Then
					If lclsPolicy_po.FindPolicybyPolicy("7", lcldPolicy_his.nPropoNum) Then
						lclsCertificat_prop = New ePolicy.Certificat
						If lclsCertificat_prop.Find("7", lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, lcldPolicy_his.nPropoNum, lclsPolicy_po.nCertif) Then
							Response.Write("top.frames['fraHeader'].document.forms[0].cbeSellchannel.value = '" & lclsCertificat_prop.nSellChannel & "';")
						End If
					End If
				End If
			End If
		End If
		'+ Realiza la busqueda asociada a la solicitud de endoso.
		If (lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion) And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
			lclsCertificat_prop = New ePolicy.Certificat
			If lclsCertificat_prop.Find("6", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), CDbl(Request.QueryString.Item("nQuotProp")), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
				If lclsCertificat_prop.dFer <> eRemoteDB.Constants.dtmNull Then
					Response.Write("tcdFer.value='" & mobjValues.TypeToString(lclsCertificat_prop.dFer, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
				If lclsCertificat_prop.dPropodat = eRemoteDB.Constants.dtmNull Then
					beffecdate_aux = True
					Response.Write("tcdEffecdate.value=GetDateSystem();")
					If lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Then
						Response.Write("tcdEffecdate.disabled=true;")
					End If
				Else
					beffecdate_aux = True
					Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsCertificat_prop.dPropodat, eFunctions.Values.eTypeData.etdDate) & "';")
					If lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Then
						Response.Write("tcdEffecdate.disabled=true;")
					End If
				End If
			Else
				beffecdate_aux = True
				If lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Then
					Response.Write("tcdEffecdate.value='';")
				Else
					Response.Write("tcdEffecdate.value=GetDateSystem();")
				End If
			End If
			lclsCertificat_prop = Nothing
		ElseIf (lintTransaction = "43" Or lintTransaction = "44") And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then 
			lclsCertificat_prop = New ePolicy.Certificat
			If lclsCertificat_prop.Find("8", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), CDbl(Request.QueryString.Item("nQuotProp")), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsCertificat_prop.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("tcdEffecdate.disabled=true;")
			End If
			lclsCertificat_prop = Nothing
		ElseIf (lintTransaction = "37") And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then 
			lclsCertificat_prop = New ePolicy.Certificat
			If lclsCertificat_prop.Find("4", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), CDbl(Request.QueryString.Item("nQuotProp")), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsCertificat_prop.dPropodat, eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("tcdEffecdate.disabled=true;")
			End If
			lclsCertificat_prop = Nothing
		End If
		
		If (lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion) And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
			lclsCertificat_prop = New ePolicy.Certificat
			If lclsCertificat_prop.Find("7", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), CDbl(Request.QueryString.Item("nQuotProp")), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("cbeSellchannel.value = " & lclsCertificat_prop.nSellChannel & ";")
				If lclsCertificat_prop.dPropodat = eRemoteDB.Constants.dtmNull Then
					beffecdate_aux = True
					Response.Write("tcdEffecdate.value=GetDateSystem();")
					If lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion Then
						Response.Write("tcdEffecdate.disabled=true;")
					End If
				Else
					beffecdate_aux = True
					Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsCertificat_prop.dPropodat, eFunctions.Values.eTypeData.etdDate) & "';")
					If lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion Then
						Response.Write("tcdEffecdate.disabled=true;")
					End If
				End If
			Else
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value=GetDateSystem();")
				If lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion Then
					Response.Write("tcdEffecdate.disabled=true;")
				End If
			End If
			lclsCertificat_prop = Nothing
		End If
		
		If (lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalQuery) And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
			If lclsPolicy_po.dStartdate = eRemoteDB.Constants.dtmNull Then
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value=GetDateSystem();")
				Response.Write("tcdEffecdate.disabled=false;")
			Else
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy_po.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("tcdEffecdate.disabled=false;")
			End If
		End If
		
		If lintTransaction = eCollection.Premium.PolTransac.clngRecuperation Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyReissue Or lintTransaction = eCollection.Premium.PolTransac.clngCertifReissue Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyProposal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifProposal Or lintTransaction = eCollection.Premium.PolTransac.clngQuotationQuery Or lintTransaction = eCollection.Premium.PolTransac.clngProposalQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotation Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotation Then
			If lclsPolicy_po.dStartdate = eRemoteDB.Constants.dtmNull Then
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value=GetDateSystem();")
				If lintTransaction <> eCollection.Premium.PolTransac.clngCertifProposal And lintTransaction <> eCollection.Premium.PolTransac.clngCertifQuotation Then
					Response.Write("tcdEffecdate.disabled=true;")
				End If
			Else
				beffecdate_aux = True
				Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy_po.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				If lintTransaction <> eCollection.Premium.PolTransac.clngCertifProposal And lintTransaction <> eCollection.Premium.PolTransac.clngCertifQuotation Then
					Response.Write("tcdEffecdate.disabled=true;")
				End If
			End If
		End If
		
		'+ Asignación del campo Oficina
		
		Response.Write("cbeOffice.value='" & lclsPolicy_po.nOffice & "';")
		Response.Write("cbeOfficeAgen.value='" & lclsPolicy_po.nOfficeagen & "';")
		Response.Write("cbeAgency.Parameters.Param1.sValue = '" & lclsPolicy_po.nOffice & "';")
		Response.Write("cbeAgency.Parameters.Param2.sValue = '" & 0 & "';")
		Response.Write("cbeAgency.value='" & lclsPolicy_po.nAgency & "';")
		If lclsPolicy_po.nAgency <> CDbl("") Then
			Response.Write("top.frames['fraHeader'].$('#cbeAgency').change();")
		End If
		If lclsPolicy_po.nOfficeagen <> CDbl("") Then
			Response.Write("cbeOfficeAgen.Parameters.Param1.sValue = '" & lclsPolicy_po.nOffice & "';")
			Response.Write("cbeOfficeAgen.Parameters.Param2.sValue = '" & eRemoteDB.Constants.intNull & "';")
			Response.Write("top.frames['fraHeader'].$('#cbeOfficeAgen').change();")
		End If
		
		'+Trae la fecha de efecto para la conversion. 
		If lintTransaction = eCollection.Premium.PolTransac.clngProposalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotationConvertion Then
			
			If lclsProduct_po.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				If CStr(lclsProduct_po.sBrancht) = "1" Then
					If lclsProduct_po.FindProduct_li(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
						If lclsProduct_po.nProdClas = 1 And lclsProduct_po.sFirst_pay = "1" And lclsProduct_po.sDatecoll = "1" Then
							
							If lclsMove_acc.Find_nProponum(lclsPolicy_po.nPropoNum) Then
								beffecdate_aux = True
								Response.Write("tcdEffecdate.value='" & mobjValues.TypeToString(lclsMove_acc.dOperdate, eFunctions.Values.eTypeData.etdDate) & "';")
							End If
						End If
					End If
				End If
			End If
		End If
		
		'+Asignación de la Compañía de seguros
		If Session("sTypeCompanyUser") = eClient.Client.eType.cstrBrokerOrBrokerageFirm And lintTransaction <> eCollection.Premium.PolTransac.clngPropQuotConvertion Then
			If lclsPolicy_po.nCompany = eRemoteDB.Constants.intNull Then
				Response.Write("valInsuranceCompany.value="""";")
			Else
				Response.Write("valInsuranceCompany.value=" & lclsPolicy_po.nCompany & ";")
			End If
			If lclsPolicy_po.sOriginal = CStr(eRemoteDB.Constants.strNull) Then
				If lintTransaction <> eCollection.Premium.PolTransac.clngQuotationConvertion Then
					Response.Write("tctOriginalPolicy.value="""";")
				End If
			Else
				'+ En caso de que sea conversión de cotización a póliza el valor de la póliza original,
				'+ no se toma de la base de datos porque no tiene valor y en tal caso la blancaría.
				If lintTransaction <> eCollection.Premium.PolTransac.clngQuotationConvertion Then
					Response.Write("tctOriginalPolicy.value=" & lclsPolicy_po.sOriginal & ";")
				End If
			End If
			
			Response.Write("valOriginalOffice.value=" & lclsPolicy_po.nOfficeIns & ";")
		End If
		
		'+Asignación del Tipo de negocio
		If lclsPolicy_po.sBussityp = CStr(eRemoteDB.Constants.strNull) Then
			Response.Write("optBussines[0].checked=true;")
			Response.Write("optBussines[0].checked=false;")
			Response.Write("optBussines[0].checked=false;")
		Else
			Select Case lclsPolicy_po.sBussityp
				Case "1"
					Response.Write("optBussines[0].checked=true;")
				Case "2"
					Response.Write("optBussines[1].checked=true;")
				Case "3"
					Response.Write("optBussines[2].checked=true;")
			End Select
		End If
		
		'+Asignación del Tipo de póliza
		If lclsPolicy_po.sPolitype = CStr(eRemoteDB.Constants.strNull) Then
			Response.Write("optType[0].checked=true;")
			Response.Write("optType[1].checked=false;")
			Response.Write("optType[2].checked=false;")
			Response.Write("tcnCertificat.disabled=true;")
		Else
			Select Case lclsPolicy_po.sPolitype
				Case "1"
					Response.Write("optType[0].checked=true;")
					Response.Write("tcnCertificat.disabled=true;")
				Case "2"
					Response.Write("optType[1].checked=true;")
					If lintTransaction <> eCollection.Premium.PolTransac.clngPolicyIssue And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyQuotation And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyProposal And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyQuery And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyAmendment And lintTransaction <> eCollection.Premium.PolTransac.clngTempPolicyAmendment Then
						
						Response.Write("tcnCertificat.disabled=false;")
					End If
				Case "3"
					Response.Write("optType[2].checked=true;")
					If lintTransaction <> eCollection.Premium.PolTransac.clngPolicyIssue And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyQuotation And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyProposal And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyQuery And lintTransaction <> eCollection.Premium.PolTransac.clngPolicyAmendment And lintTransaction <> eCollection.Premium.PolTransac.clngTempPolicyAmendment And lintTransaction <> clngDuplPolicy Then
						Response.Write("tcnCertificat.disabled=false;")
					End If
			End Select
		End If
		
		'+Asignación del campo Fecha de contabilización
		Response.Write("tcdLedgerDate.value=GetDateSystem();")
		
		'+ Asignación del tipo de endoso 
		If lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendentQuery Or lintTransaction = 37 Then
			If lcldPolicy_his.reaPolicy_his_typeamend("6", mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nQuotProp"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write(" valType_amend.value='" & mobjValues.TypeToString(lcldPolicy_his.nType_amend, eFunctions.Values.eTypeData.etdDouble) & "';")
				If (Not IsNothing(Request.QueryString.Item("dEffecdate")) Or beffecdate_aux) And lcldPolicy_his.nType_amend <> eRemoteDB.Constants.intNull Then
					Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.Parameters.Param1.sValue =" & mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble) & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.Parameters.Param2.sValue =" & mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble) & ";")
					If Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
						Response.Write(" valType_amend.disabled = true; ")
						Response.Write(" btnvalType_amend.disabled = true; ")
						Response.Write(" tcdEffecdate.disabled = true; ")
					End If
					Response.Write(" top.frames['fraHeader'].('#valType_amend').change(); ")
				End If
			End If
			
			If lcldPolicy_his.reaPolicy_his_typeamend("4", mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nQuotProp"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write(" valType_amend.value='" & mobjValues.TypeToString(lcldPolicy_his.nType_amend, eFunctions.Values.eTypeData.etdDouble) & "';")
				
				If (Not IsNothing(Request.QueryString.Item("dEffecdate")) Or beffecdate_aux) And lcldPolicy_his.nType_amend <> eRemoteDB.Constants.intNull Then
					Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.Parameters.Param1.sValue =" & mobjValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble) & ";")
					Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.Parameters.Param2.sValue =" & mobjValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble) & ";")
					Response.Write(" top.frames['fraHeader'].('#valType_amend').change(); ")
				End If
			End If
		ElseIf lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Then 
			If lcldPolicy_his.reaPolicy_his_typeamend("6", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nQuotProp"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write(" valType_amend.value='" & mobjValues.TypeToString(lcldPolicy_his.nType_amend, eFunctions.Values.eTypeData.etdDouble) & "';")
				Response.Write("valType_amend.disabled=true;")
				If Not IsNothing(Request.QueryString.Item("dEffecdate")) Or beffecdate_aux Then
					Response.Write(" top.frames['fraHeader'].('#valType_amend').change(); ")
				End If
				
			End If
		End If
		
		'+Asignación del campo Referencia, excluyendo cuando es emisión de certificado.
		If lintTransaction <> "2" Then
			If lclsProcess.Find_Policy(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), llngCodeProce, 1) Then
				With Response
					.Write("")
					.Write("if((tcnReference.value==0)||")
					.Write("   (tcnReference.value=='')&&")
					.Write("   (tcnReference.value!=" & lclsProcess.nReference & "))")
					.Write("    tcnReference.value=0" & lclsProcess.nReference)
					.Write(";")
				End With
			Else
				If lintTransaction = "8" Or lintTransaction = "9" Or lintTransaction = "10" Or lintTransaction = "11" Then
					If llngCodeProce = 4 Then
						llngCodeProce = 6
					Else
						llngCodeProce = 4
					End If
					If lclsProcess.Find_Policy(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), llngCodeProce, 1) Then
						With Response
							.Write("")
							.Write("if((tcnReference.value==0)||")
							.Write("   (tcnReference.value=='')&&")
							.Write("   (tcnReference.value!=" & lclsProcess.nReference & "))")
							.Write("    tcnReference.value=0" & lclsProcess.nReference)
							.Write(";")
						End With
					End If
				End If
			End If
		End If
		If (lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotPropRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalQuery) Or lintTransaction = "43" Or lintTransaction = "44" Then
			
			If lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendentQuery Then
				lstrCertype = "4"
			End If
			
			If lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalQuery Then
				lstrCertype = "5"
			End If
			
			If lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendentQuery Then
				lstrCertype = "6"
			End If
			
			If lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalQuery Then
				lstrCertype = "7"
			End If
			
			If lcldPolicy_his.FindLast_order(lstrCertype, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nQuotProp"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnServ_order.value =" & lcldPolicy_his.nServ_order & ";")
				
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnServ_order.value ='';")
			End If
		End If
		
		
		If lclsPolicy_po.nProduct <> CDbl("") Then
			Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
		End If
		
		If (lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotPropRenewalConvertion Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalQuery Or lintTransaction = eCollection.Premium.PolTransac.clngPropRenewalQuery) Or lintTransaction = "43" Or lintTransaction = "44" Then
			Response.Write(" tcnQuotProp.disabled=false ;")
		End If
		
		If (lintTransaction = clngDuplPolicy) Then
			'Response.Write " btnPolicyValues.disabled=true; "
			Response.Write(" tcnCertificat.disabled=true; ")
		End If
		
		'+ Traspaso de asegurado
		If lintTransaction = clngTransHolder Then
			Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param1.sValue=" & Request.QueryString.Item("sCertype") & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param2.sValue=" & lclsPolicy_po.nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param3.sValue=" & lclsPolicy_po.nProduct & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param4.sValue=" & lclsPolicy_po.nPolicy & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param5.sValue=" & lclsPolicy_po.sClient & ";")
			If Not IsNothing(Request.QueryString.Item("dEffecdate")) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param6.sValue='" & Request.QueryString.Item("dEffecdate") & "';")
			End If
		End If
		
		Response.Write("}")
	End If
	
	lclsPolicy_po = Nothing
	lclsProcess = Nothing
	lclsProduct_po = Nothing
	lclsMove_acc = Nothing
	lclsCertificat = Nothing
	lcldPolicy_his = Nothing
End Sub

'% insShowCertificat: se muestran los datos asociados al número de certificado
'%                    Se utiliza para el campo Certificado de la página CA001_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowCertificat()
	'--------------------------------------------------------------------------------------------
	Dim lclsCertificat_cer As ePolicy.Certificat
	Dim lintTransaction As Object
	
	lclsCertificat_cer = New ePolicy.Certificat
	
	'+ Validaciones sobre el campo Transacción
	lintTransaction = Request.QueryString.Item("nTransaction")
	
	If lintTransaction = eCollection.Premium.PolTransac.clngRecuperation Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotation Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyProposal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotation Or lintTransaction = eCollection.Premium.PolTransac.clngCertifReissue Or lintTransaction = eCollection.Premium.PolTransac.clngCertifProposal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngQuotationQuery Then
		
		If lclsCertificat_cer.Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If lintTransaction = eCollection.Premium.PolTransac.clngRecuperation Or lintTransaction = eCollection.Premium.PolTransac.clngCertifReissue Or lintTransaction = eCollection.Premium.PolTransac.clngCertifProposal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotation Or lintTransaction = eCollection.Premium.PolTransac.clngQuotationQuery Then
				If lclsCertificat_cer.dStartdate = eRemoteDB.Constants.dtmNull Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value=GetDateSystem();")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=true;")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsCertificat_cer.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=true;")
				End If
			End If
			
			If (lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifPropRenewal Or lintTransaction = eCollection.Premium.PolTransac.clngQuotAmendentQuery Or lintTransaction = eCollection.Premium.PolTransac.clngQuotRenewalQuery) And Not IsNothing(Request.QueryString.Item("nQuotProp")) Then
				If lclsCertificat_cer.dStartdate = eRemoteDB.Constants.dtmNull Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value=GetDateSystem();")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsCertificat_cer.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
			End If
			
			If lintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotation Or lintTransaction = eCollection.Premium.PolTransac.clngPolicyProposal Or lintTransaction = eCollection.Premium.PolTransac.clngCertifQuotation Or lintTransaction = eCollection.Premium.PolTransac.clngCertifProposal Then
				If lclsCertificat_cer.nSellChannel <> eRemoteDB.Constants.intNull Then
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeSellchannel.value='" & lclsCertificat_cer.nSellChannel & "';")
				End If
			End If
			
			'+ Fecha de ultimo cambio para proceso de modificacion
			If lintTransaction = eCollection.Premium.PolTransac.clngPolicyAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngCertifAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or lintTransaction = eCollection.Premium.PolTransac.clngTempCertifAmendment Then
				Session("dLastChange") = lclsCertificat_cer.dChangdat
			End If
			
		End If
	End If
	
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdLedgerDate.value=GetDateSystem();")
	
	lclsCertificat_cer = Nothing
End Sub

'% InsCalWayPay: se muestran los datos de la forma de pago
'--------------------------------------------------------------------------------------------
Sub InsCalWayPay()
	'--------------------------------------------------------------------------------------------
	Dim lclsDir_debit_cli As eClient.Dir_debit_cli
	
	'+ Validaciones sobre el campo Transacción
	If Request.QueryString.Item("optDirTyp") = "1" Then
		Response.Write("top.frames['fraFolder'].document.forms[0].elements[""optDirTyp""][0].checked = true;")
		If Request.QueryString.Item("hhDirTyp") = vbNullString Then
			Response.Write("top.frames['fraFolder'].document.forms[0].hhDirTyp.value = 1;")
		End If
	ElseIf Request.QueryString.Item("optDirTyp") = "2" Then 
		Response.Write("top.frames['fraFolder'].document.forms[0].elements[""optDirTyp""][1].checked = true;")
		If Request.QueryString.Item("hhDirTyp") = vbNullString Then
			If CStr(Session("sBrancht")) = "1" Then
				Response.Write("top.frames['fraFolder'].document.forms[0].hhDirTyp.value = 2;")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].hhDirTyp.value = 1;")
			End If
		End If
	Else
		lclsDir_debit_cli = New eClient.Dir_debit_cli
		If lclsDir_debit_cli.Find(Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			If CStr(Session("sBrancht")) = "1" Then
				Response.Write("top.frames['fraFolder'].document.forms[0].elements[""optDirTyp""][0].checked = true;")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].elements[""optDirTyp""][1].checked = true;")
			End If
			If Request.QueryString.Item("hhDirTyp") = vbNullString Then
				Response.Write("top.frames['fraFolder'].document.forms[0].hhDirTyp.value = 1;")
			End If
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].elements[""optDirTyp""][1].checked = true;")
			If Request.QueryString.Item("hhDirTyp") = vbNullString Then
				Response.Write("top.frames['fraFolder'].document.forms[0].hhDirTyp.value = 2;")
			End If
		End If
	End If
	
	lclsDir_debit_cli = Nothing
End Sub

'% insShowProduct: se muestran los datos asociados al número de producto
'%                 Se utiliza para el campo Producto de la página CA001_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowProduct()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct_ca As eProduct.Product
	
	lclsProduct_ca = New eProduct.Product
	
	With lclsProduct_ca
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			If Request.QueryString.Item("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Request.QueryString.Item("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Request.QueryString.Item("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Then
				'+ Se habilitan/deshabilitan los tipos de póliza permitidos para el producto
				If .sIndivind = "1" Then
					Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][0].disabled=false;")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][0].disabled=true;")
				End If
				
				If .sGroupind = "1" Then
					Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][1].disabled=false;")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][1].disabled=true;")
				End If
				
				If .sMultiind = "1" Then
					Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][2].disabled=false;")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][2].disabled=true;")
				End If
				
				'+ Se coloca el valor por defecto                
				Select Case .sPolitype
					Case "1"
						Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][0].checked = true;")
					Case "2"
						Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][1].checked = true;")
					Case "3"
						Response.Write("top.frames['fraHeader'].document.forms[0].elements[""optType""][2].checked = true;")
				End Select
			End If
		End If
		Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.Parameters.Param1.sValue =" & mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].valType_amend.Parameters.Param2.sValue =" & mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) & ";")
		
		If lclsProduct_ca.FindProdMaster(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If lclsProduct_ca.sBrancht = 1 Or lclsProduct_ca.sBrancht = 5 Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnServ_order.value = ''; ")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnServ_order.disabled = true; ")
				Response.Write("top.frames['fraHeader'].ShowDiv('divRenewalNum', 'hide');")
				Response.Write("top.frames['fraHeader'].ShowDiv('divRenewalNum2', 'hide');")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnServ_order.disabled = false; ")
				Response.Write("top.frames['fraHeader'].ShowDiv('divRenewalNum', 'show');")
				Response.Write("top.frames['fraHeader'].ShowDiv('divRenewalNum2', 'show');")
			End If
		End If
	End With
	
	lclsProduct_ca = Nothing
End Sub

'% insShowAuto: se muestran los datos asociados al auto seleccionado
'%              Se utiliza para el campo Código del vehiculo de la página AU001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	Dim lclsValpolicyseq As ePolicy.ValPolicySeq
	Dim lclsPolicyWin As Object
	
	lclsAuto = New ePolicy.Automobile
	lclsValpolicyseq = New ePolicy.ValPolicySeq
	
	If lclsAuto.Find_Tab_au_veh(Request.QueryString.Item("sVehcode")) Then
		With Response
			If Request.QueryString.Item("Field1") = "Auto1" Then
				lclsAuto.sVehcode = Request.QueryString.Item("sVehcode")
				.Write("top.frames['fraFolder'].document.forms[0].valVehcode.value=" & Request.QueryString.Item("sVehcode") & ";")
				.Write("top.frames['fraFolder'].UpdateDiv(""valVehcodeDesc"",'" & Trim(lclsAuto.sDescript) & "','Normal');")
			End If
			.Write("top.frames['fraFolder'].document.forms[0].ValVehMark.value=" & lclsAuto.nVehBrand & ";")
			.Write("top.frames['fraFolder'].document.forms[0].ValVehModel.value=" & Request.QueryString.Item("sVehcode") & ";")
			.Write("top.frames['fraFolder'].UpdateDiv(""ValVehModelDesc"",'" & lclsAuto.sVehmodel1 & "','Normal');")
			.Write("top.frames['fraFolder'].UpdateDiv(""lblType"",'" & lclsAuto.sDesTypeVeh & "','Normal');")
			.Write("top.frames['fraFolder'].document.forms[0].tcnType.value=" & lclsAuto.nVehType & ";")
			.Write("top.frames['fraFolder'].document.forms[0].tcnVehPlace.value=" & lclsAuto.nVehplace & ";")
			If lclsAuto.nVehplace > 0 Then
				.Write("top.frames['fraFolder'].document.forms[0].tcnVehPlace.disabled=true;")
			Else
				.Write("top.frames['fraFolder'].document.forms[0].tcnVehPlace.disabled=false;")
			End If
			.Write("top.frames['fraFolder'].document.forms[0].tcnVehPma.value=" & lclsAuto.nVehpma & ";")
			.Write("top.frames['fraFolder'].document.forms[0].ValVehMark.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].ValVehModel.disabled=true;")
			.Write("top.frames['fraFolder'].document.forms[0].btnValVehModel.disabled=true;")
			If lclsValpolicyseq.InsValRelapsing(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "", eRemoteDB.Constants.intNull, Request.QueryString.Item("sRegist")) Then
				.Write("top.frames['fraFolder'].document.forms[0].chksrelapsing.checked=true;")
			Else
				.Write("top.frames['fraFolder'].document.forms[0].chksrelapsing.checked=false;")
			End If
		End With
		
		If Request.QueryString.Item("sVehcode") <> vbNullString And Request.QueryString.Item("nYear") <> vbNullString And (Request.QueryString.Item("sVehcode") <> Request.QueryString.Item("sVehCode_ori") Or Request.QueryString.Item("nYear") <> Request.QueryString.Item("nYear_ori")) Then
			If lclsAuto.Find_Tab_au_val(Request.QueryString.Item("sVehcode"), mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnCapital.value=" & lclsAuto.nCapital & ";")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnCapital.value=0;")
			End If
		Else
			If Request.QueryString.Item("sVehcode") = Request.QueryString.Item("sVehCode_ori") And Request.QueryString.Item("nYear") = Request.QueryString.Item("nYear_ori") Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnCapital.value=" & Request.QueryString.Item("sCapital_ori") & ";")
			End If
		End If
	End If
	lclsAuto = Nothing
	lclsValpolicyseq = Nothing
End Sub

'% insSlicense_ty: Se genera un numero secuencial para una patente de tipo provisional 
'-------------------------------------------------------------------------------------------- 
Sub insSlicense_ty()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsAuto As ePolicy.Automobile
	lclsAuto = New ePolicy.Automobile
	If Request.QueryString.Item("Slicense_ty") = "3" Then
		If Request.QueryString.Item("sRegist") = vbNullString Or CStr(Session("AuxAuto")) <> Request.QueryString.Item("sRegist") Then
			If lclsAuto.next_seqregistauto() Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tctRegister.value=""" & lclsAuto.sRegist & """;")
				Session("AuxAuto") = lclsAuto.sRegist
			End If
		End If
	End If
	lclsAuto = Nothing
End Sub

'% insShowAuto_Regist: Se muestran los datos asociados al auto seleccionado,
'%                       si el número de placa ya está registrado en el sistema
'%                       Se utiliza en el campo Matrícula de la ventana AU001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto_Regist()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	Dim lclsAuto_db As ePolicy.Auto_db
	Dim lclsValpolicyseq As ePolicy.ValPolicySeq
	Dim blnCalDigit As Boolean
	Dim sLicense_ty_old As String
	Dim sRegist_old As String
	Dim lclsPolicyWin As Object
	
	lclsValpolicyseq = New ePolicy.ValPolicySeq
	lclsAuto = New ePolicy.Automobile
	lclsAuto_db = New ePolicy.Auto_db
	
	blnCalDigit = True
	
	If Request.QueryString.Item("Slicense_ty") = "1" Then
		
		If lclsAuto.InsCalDigitSerie(Request.QueryString.Item("sRegist")) Then
			Response.Write("top.frames['fraFolder'].document.forms[0].tctDigit.value=""" & Trim(lclsAuto.sDigit) & """;")
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].tctDigit.value=""" & """;")
			Response.Write("top.frames['fraFolder'].document.forms[0].tctRegister.value=""" & """;")
			
			Response.Write("alert(""Err 55983: " & eFunctions.Values.GetMessage(55983) & """);")
			blnCalDigit = False
		End If
	End If
	
	If blnCalDigit Then
		Call lclsAuto.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
		sLicense_ty_old = lclsAuto.sLicense_ty
		sRegist_old = lclsAuto.sRegist
		If sLicense_ty_old = vbNullString And sRegist_old = vbNullString Then
			If lclsAuto_db.Find_db1(Request.QueryString.Item("Slicense_ty"), Request.QueryString.Item("sRegist"), True) Then
				If lclsAuto.Find_Tab_au_veh(lclsAuto_db.sVehcode) Then
					With Response
						If lclsAuto_db.sMotor <> "" Then
							.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=true;")
						End If
						If lclsAuto_db.sChassis <> "" Then
							.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=true;")
						End If
						
						.Write("top.frames['fraFolder'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
						.Write("top.frames['fraFolder'].document.forms[0].tctChassis.value=""" & lclsAuto_db.sChassis & """;")
						.Write("top.frames['fraFolder'].document.forms[0].tctColor.value=""" & lclsAuto_db.sColor & """;")
						.Write("top.frames['fraFolder'].document.forms[0].valVehcode.value=""" & lclsAuto_db.sVehcode & """;")
						.Write("top.frames['fraFolder'].UpdateDiv(""valVehcodeDesc"",'" & Trim(lclsAuto.sDesbrand) & "/" & Trim(lclsAuto.sVehmodel1) & "','Normal');")
						.Write("top.frames['fraFolder'].document.forms[0].ValVehMark.value=" & lclsAuto.nVehBrand & ";")
						.Write("top.frames['fraFolder'].document.forms[0].ValVehModel.value=" & lclsAuto_db.sVehcode & ";")
						.Write("top.frames['fraFolder'].UpdateDiv(""ValVehModelDesc"",'" & lclsAuto.sVehmodel1 & "','Normal');")
						.Write("top.frames['fraFolder'].UpdateDiv(""lblType"",'" & lclsAuto.sDesTypeVeh & "','Normal');")
						.Write("top.frames['fraFolder'].document.forms[0].tcnType.value=" & lclsAuto.nVehType & ";")
						.Write("top.frames['fraFolder'].document.forms[0].tcnVehPlace.value=" & lclsAuto.nVehplace & ";")
						.Write("top.frames['fraFolder'].document.forms[0].tcnVehPma.value=" & lclsAuto.nVehpma & ";")
						.Write("top.frames['fraFolder'].document.forms[0].tcnCapital.value='" & mobjValues.TypeToString(lclsAuto.nCapital, eFunctions.Values.eTypeData.etdDouble) & "';")
						.Write("top.frames['fraFolder'].document.forms[0].tcnYear.value='" & mobjValues.TypeToString(lclsAuto.nYear, eFunctions.Values.eTypeData.etdDouble) & "';")
						
						
					End With
				Else
					With Response
						If lclsAuto_db.sMotor <> "" Then
							.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=true;")
						End If
						If lclsAuto_db.sChassis <> "" Then
							.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=true;")
						End If
						.Write("top.frames['fraFolder'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
						.Write("top.frames['fraFolder'].document.forms[0].tctChassis.value=""" & lclsAuto_db.sChassis & """;")
						.Write("top.frames['fraFolder'].document.forms[0].tctColor.value=""" & lclsAuto_db.sColor & """;")
						.Write("top.frames['fraFolder'].document.forms[0].valVehcode.value=""" & lclsAuto_db.sVehcode & """;")
						.Write("top.frames['fraFolder'].UpdateDiv(""lblVehMark"",'" & lclsAuto_db.sVehBrand & "','Normal');")
						.Write("top.frames['fraFolder'].UpdateDiv(""lblVehModel"",'" & lclsAuto_db.sVehModel & "','Normal');")
						.Write("top.frames['fraFolder'].UpdateDiv(""lblType"",'" & lclsAuto_db.sVehType & "','Normal');")
					End With
				End If
			Else
				With Response
					.Write("top.frames['fraFolder'].document.forms[0].tctMotor.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].tctChassis.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].tctColor.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].valVehcode.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].UpdateDiv('valVehcodeDesc','','popup');")
					.Write("top.frames['fraFolder'].document.forms[0].ValVehMark.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].ValVehModel.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].UpdateDiv('ValVehModelDesc','','popup');")
					.Write("top.frames['fraFolder'].UpdateDiv('lblType','','popup');")
					.Write("top.frames['fraFolder'].document.forms[0].tcnType.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].tcnVehPlace.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].tcnVehPma.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].tcnCapital.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].tcnYear.value=""" & "" & """;")
					.Write("top.frames['fraFolder'].document.forms[0].chksrelapsing.checked=false;")
					.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=false;")
					.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=false;")
				End With
			End If
		Else
			With Response
				.Write("top.frames['fraFolder'].document.forms[0].tctMotor.disabled=false;")
				.Write("top.frames['fraFolder'].document.forms[0].tctChassis.disabled=false;")
			End With
		End If
		
		If lclsValpolicyseq.InsValRelapsing(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "", eRemoteDB.Constants.intNull, Request.QueryString.Item("sRegist")) Then
			Response.Write("top.frames['fraFolder'].document.forms[0].chksrelapsing.checked=true;")
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].chksrelapsing.checked=false;")
		End If
		
	End If
	
	lclsAuto = Nothing
	lclsAuto_db = Nothing
	lclsValpolicyseq = Nothing
End Sub

'% insShowIntermed: se muestran los datos asociados al intermediario 
'%                    Se utiliza para el campo Código de la página CA024.aspx 
'-------------------------------------------------------------------------------------------- 
Function insShowIntermed() As Object
	'-------------------------------------------------------------------------------------------- 
	Dim llngIntermed As Integer
	Dim lclsCertificat As ePolicy.Certificat
	
	Dim lclsIntermedia As eAgent.Intermedia
	Dim lclsBranprod_allow As eAgent.branprod_allow
	Dim lntab_used As Byte
	
	'+ Se asignan los valores dependiendo de los datos del intermediario 
	
	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If CStr(Session("Action")) = "Update" Then
			.Parameters.Add("nTransactio", Session("nTransaction"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			.Parameters.Add("nTransactio", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
	End With
	
	lntab_used = 0
	With Response
		If CStr(Session("nInsur_area")) = "1" Then
			If mobjValues.IsValid("tabIntermedia_ca024b", Request.QueryString.Item("nCodeIntermed"), True) Then
				lntab_used = 1
			End If
		Else
			If mobjValues.IsValid("tabIntermedia_ca024", Request.QueryString.Item("nCodeIntermed"), True) Then
				lntab_used = 1
			End If
		End If
		If lntab_used = 1 Then
			llngIntermed = mobjValues.StringToType(Request.QueryString.Item("nCodeIntermed"), eFunctions.Values.eTypeData.etdDouble)
			lclsIntermedia = New eAgent.Intermedia
			If lclsIntermedia.Find(llngIntermed) Then
				.Write("opener.document.forms[0].cbeRole.value=" & lclsIntermedia.nInterTyp & ";")
				.Write("opener.document.forms[0].cbeAgency.value=" & lclsIntermedia.nAgency & ";")
				.Write("opener.document.forms[0].cbeType.value=" & Request.QueryString.Item("sTypeComm") & ";")
				.Write("opener.document.forms[0].tcnAmount.value="""";")
				.Write("opener.document.forms[0].tcnPercent.value="""";")
				
				Session("hddsType") = Request.QueryString.Item("sTypeComm")
				.Write("opener.document.forms[0].hddtcnPercent.value=opener.top.opener.top.frames[""fraFolder""].document.forms[0].tcnPercentCF.value;")
				
				'+ Si Esquema de pago de comisiones del intermediario es Producción (1) cantidad de cuotas = 1        
				If (lclsIntermedia.nLife_sche = 1 And CStr(Session("sBrancht")) = "1") Or (lclsIntermedia.nGen_sche = 1 And CStr(Session("sBrancht")) <> "1") Then
					.Write("opener.document.forms[0].tcnInstallCom.value='" & mobjValues.TypeToString(1, eFunctions.Values.eTypeData.etdDouble) & "';")
				Else
					'+ Recupera duración (en meses) de la póliza
					lclsCertificat = New ePolicy.Certificat
					If lclsCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
						
						'+ Recupera cuotas de pago de comisión del intermediario para el ramo producto  
						lclsBranprod_allow = New eAgent.branprod_allow
						If lclsBranprod_allow.Find(llngIntermed, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsCertificat.nDuration), eFunctions.Values.eTypeData.etdDouble)) Then
							.Write("opener.document.forms[0].tcnInstallCom.value='" & mobjValues.TypeToString(lclsBranprod_allow.nInstallments, eFunctions.Values.eTypeData.etdDouble) & "';")
						Else
							.Write("opener.document.forms[0].tcnInstallCom.value='" & mobjValues.TypeToString(0, eFunctions.Values.eTypeData.etdDouble) & "';")
						End If
					End If ' lclsCertificat.Find(Request.QueryString("sCertype"), ......
					lclsCertificat = Nothing
				End If ' (lclsIntermedia.nLife_sche = 1 And _ ...........
				If lclsIntermedia.sParticin = "1" Then
					.Write("opener.document.forms[0].tcnAmount.disabled=false;")
					.Write("opener.document.forms[0].tcnAmount.value='';")
					.Write("opener.document.forms[0].tcnShare.disabled=false;")
				Else
					.Write("opener.document.forms[0].tcnAmount.disabled=true;")
					.Write("opener.document.forms[0].tcnAmount.value='';")
					.Write("opener.document.forms[0].tcnShare.value='';")
					.Write("opener.document.forms[0].tcnShare.disabled=true;")
					.Write("opener.document.forms[0].cbeType.value=" & ePolicy.Commission.TypeOfIntermediaryCommissionsNoCommission & ";")
					.Write("opener.document.forms[0].tcnPercent.value="""";")
				End If
				
				Select Case Request.QueryString.Item("sTypeComm")
					
					'+ Comisión de la póliza "Fija"
					Case "2"
						.Write("opener.document.forms[0].tcnPercent.value=opener.top.opener.top.frames[""fraFolder""].document.forms[0].tcnPercentCF.value;")
						.Write("opener.document.forms[0].tcnShare.disabled=false;")
						
						'+ Comisión de la póliza "No tiene"                    
					Case "3"
						.Write("opener.document.forms[0].tcnPercent.disabled=true;")
						.Write("opener.document.forms[0].tcnShare.disabled=true;")
						.Write("opener.document.forms[0].tcnShare.value="""";")
				End Select
				
				If lclsIntermedia.sParticin = "2" Then
					.Write("opener.document.forms[0].tcnPercent.value="""";")
				End If
				
				If lclsIntermedia.nInterTyp = 2 Then
					.Write("opener.document.forms[0].cbeAgreement.disabled=false;")
				Else
					.Write("opener.document.forms[0].cbeAgreement.disabled=true;")
				End If
				
				If lclsIntermedia.nSupervis <> 0 And lclsIntermedia.nSupervis <> eRemoteDB.Constants.intNull Then
					If lclsIntermedia.sCol_Agree = "1" Then
						.Write("opener.top.opener.document.forms[0].chkConColl.checked=true;")
					End If
				End If
				
				.Write("insShowIntermed(""" & lclsIntermedia.sParticin & """);")
			End If ' lclsIntermedia.Find(llngIntermed)
		Else
			' mobjValues.IsValid("tabIntermedia_office",Request.QueryString("nCodeIntermed"),True)
			.Write("opener.document.forms[0].cbeRole.value='';")
			.Write("opener.document.forms[0].cbeAgreement.value='';")
			.Write("opener.document.forms[0].tcnInstallCom.value='';")
			.Write("opener.document.forms[0].cbeAgency.value='';")
			.Write("opener.document.forms[0].tcnShare.value='';")
			.Write("opener.document.forms[0].tcnAmount.value='';")
			.Write("opener.document.forms[0].tcnPercent_Ce.value='';")
			.Write("opener.document.forms[0].tcnPercent.disabled=true;")
			.Write("opener.document.forms[0].tcnAmount.disabled=true;")
			.Write("opener.document.forms[0].tcnShare.disabled=true;")
			.Write("opener.document.forms[0].cbeAgreement.disabled=true;")
			.Write("opener.document.forms[0].cbeAgreement.disabled=true;")
		End If ' mobjValues.IsValid("tabIntermedia_office",Request.QueryString("nCodeIntermed"),True)
	End With
	lclsIntermedia = Nothing
	lclsBranprod_allow = Nothing
End Function

'% insreaLedgerDate: busca la fecha de contabilización del recibo
'--------------------------------------------------------------------------------------------
Function insreaLedgerDate() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lclsPremium_mo As eCollection.Premium_mo
	
	lclsPremium = New eCollection.Premium
	lclsPremium_mo = New eCollection.Premium_mo
	
	insreaLedgerDate = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
	With lclsPremium
		.sCertype = Request.QueryString.Item("sCertype")
		.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
		.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
		.nPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
		If .Find_Receipt Then
			If .nReceipt > 0 Then
				If lclsPremium_mo.Find_dPosted(.nReceipt) Then
					If lclsPremium_mo.dPosted <> eRemoteDB.Constants.dtmNull Then
						insreaLedgerDate = mobjValues.TypeToString(lclsPremium_mo.dPosted, eFunctions.Values.eTypeData.etdDate)
					End If
				End If
			End If
		End If
	End With
	
	lclsPremium = Nothing
	lclsPremium_mo = Nothing
End Function

'% insShowNullAdvise: Muestra la cantidad de días de antelación para aviso de anulación de póliza
'% Se utiliza para el campo aviso de anulación de la página CA033.aspx
'------------------------------------------------------------------------------------------------
Sub insShowNullAdvise()
	'------------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	
	lclsProduct = New eProduct.Product
	
	If lclsProduct.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nNullDate"), eFunctions.Values.eTypeData.etdDate)) Then
		Response.Write("opener.UpdateDiv(""lblNullAdv"",'" & lclsProduct.nCancnoti & "','Normal');")
	End If
	
	lclsProduct = Nothing
End Sub

'% insShowAgreement: Lee los datos del convenio de pago y muestra el número de cuotas
'--------------------------------------------------------------------------------------------
Sub insShowAgreement()
	'--------------------------------------------------------------------------------------------
	Dim lclsAgreement As eCollection.Agreement
	
	lclsAgreement = New eCollection.Agreement
	
	With lclsAgreement
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nCod_agree"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("opener.document.forms[0].cbeQuota.value='" & .nQ_draft & "';")
		End If
	End With
	
	lclsAgreement = Nothing
End Sub

'% insChangeTypeInvest: Lee los los procentajes de rentabilidad
'--------------------------------------------------------------------------------------------
Sub insChangeTypeInvest()
	'--------------------------------------------------------------------------------------------
	Dim lclsPlan_IntWar As eProduct.Plan_IntWar
	
	lclsPlan_IntWar = New eProduct.Plan_IntWar
	
	With lclsPlan_IntWar
		If .Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			
			If .nIntwarr > 0 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntproject.value='" & mobjValues.StringToType(CStr(.nIntwarr), eFunctions.Values.eTypeData.etdDouble) & "';")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntproject.value='';")
			End If
			
			If .nIntwarrMin > 0 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnWarminint.value='" & mobjValues.StringToType(CStr(.nIntwarrMin), eFunctions.Values.eTypeData.etdDouble) & "';")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnWarminint.value='';")
			End If
		Else
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnIntproject.value='';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnWarminint.value='';")
		End If
	End With
	lclsPlan_IntWar = Nothing
End Sub

'% InsShowAccount_Data:
'--------------------------------------------------------------------------------------------
Sub InsShowAccount_Data()
	'--------------------------------------------------------------------------------------------
	Dim lclsbk_account As eClient.bk_account
	
	lclsbk_account = New eClient.bk_account
	
	If lclsbk_account.Find(Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sAccount")) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_Account.value='" & lclsbk_account.nTyp_acc & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_Account.disabled=true;")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_Account.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_Account.disabled=false;")
	End If
	
	lclsbk_account = Nothing
End Sub

'% InsShowCard_Data: Muestra los datos de la tarjeta de crédito
'--------------------------------------------------------------------------------------------
Sub InsShowCard_Data()
	'--------------------------------------------------------------------------------------------
	Dim lclsCred_card As eClient.cred_card
	
	lclsCred_card = New eClient.cred_card
	
	If lclsCred_card.Find(Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nBank_code"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sAccount")) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_crecard.value='" & lclsCred_card.nCard_Type & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcdDateExpir.value='" & lclsCred_card.dCardexpir & "';")
	End If
	
	lclsCred_card = Nothing
End Sub

'% InsShowCreditCard_Data: Muestra los datos de la tarjeta de crédito en la CA003
'--------------------------------------------------------------------------------------------
Sub InsShowCreditCard_Data()
	'--------------------------------------------------------------------------------------------
	Dim lclsCred_card As eClient.cred_card
	
	lclsCred_card = New eClient.cred_card
	
	With Response
		If lclsCred_card.Find(Request.QueryString.Item("sClient"), eRemoteDB.Constants.intNull, Request.QueryString.Item("sAccount")) Then
			.Write("top.frames['fraFolder'].document.forms[0].cbeTyp_crecard.value='" & lclsCred_card.nCard_Type & "';")
			.Write("top.frames['fraFolder'].document.forms[0].tcdDateExpir.value='" & mobjValues.TypeToString(lclsCred_card.dCardexpir, eFunctions.Values.eTypeData.etdDate) & "';")
		End If
	End With
	
	lclsCred_card = Nothing
End Sub

'% insUpdUserAmend: se actualiza el campo nUser_amend de Policy o Certificat, según sea el caso
'--------------------------------------------------------------------------------------------
Sub insUpdUserAmend()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsCertificat As ePolicy.Certificat
	
	If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Then
		If CStr(Session("nCertif")) = vbNullString Or CStr(Session("nCertif")) = "0" Then
			lclsPolicy = New ePolicy.Policy
			'+ Se actualiza el campo en la tabla Policy        
			With lclsPolicy
				.sCertype = Session("sCertype")
				.nBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.nPolicy = mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
				.Update_UserAmend()
			End With
		Else
			lclsCertificat = New ePolicy.Certificat
			'+ Se actualiza el campo en la tabla Certificat        
			With lclsCertificat
				.sCertype = Session("sCertype")
				.nBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.nPolicy = mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
				.nCertif = mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
				.Update_UserAmend()
			End With
		End If
	End If
	
	lclsPolicy = Nothing
	lclsCertificat = Nothing
End Sub

'% insShowEndoso: Sub para el manejo de la fecha de endoso retroactivo  
'--------------------------------------------------------------------------------------------
Sub insShowEndoso()
	'--------------------------------------------------------------------------------------------
	Dim lclsUsers As eGeneral.Users
	Dim lclsSecur_sche As eSecurity.Secur_sche
	Dim lclsType_amend As ePolicy.Type_amend
	Dim lclsValpolicyseq As ePolicy.ValPolicySeq
	Dim lclsProf_ord As eClaim.Prof_ord
	Dim lclsPolicy As ePolicy.Policy
	
	lclsUsers = New eGeneral.Users
	lclsSecur_sche = New eSecurity.Secur_sche
	lclsType_amend = New ePolicy.Type_amend
	lclsValpolicyseq = New ePolicy.ValPolicySeq
	lclsProf_ord = New eClaim.Prof_ord
	lclsPolicy = New ePolicy.Policy
	
	'+ Validaciones sobre el campo Transacción
	If Request.QueryString.Item("nTransaction") = "26" Or Request.QueryString.Item("nTransaction") = "27" Then
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdFer.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdFer.disabled=true;")
		
		If lclsUsers.Find(Session("nUsercode")) Then
			If lclsSecur_sche.Find(lclsUsers.sSche_code) Then
				If lclsSecur_sche.insReaLevels_v(lclsUsers.sSche_code, CStr(2), "CA001_K") Then
					If CDbl(lclsSecur_sche.sSupervis) = 1 Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdFer.disabled=false;")
						Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdFer.disabled=false;")
					Else
						If lclsType_amend.Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CInt(Request.QueryString.Item("nType_amend"))) Then
							If lclsType_amend.nLevel >= lclsSecur_sche.nAmelevel Then
								Response.Write("top.frames['fraHeader'].document.forms[0].tcdFer.disabled=false;")
								Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdFer.disabled=false;")
							End If
						End If
					End If
					
				ElseIf lclsSecur_sche.insReaLevels_v(lclsUsers.sSche_code, CStr(1), "5") Then 
					If CDbl(lclsSecur_sche.sSupervis) = 1 Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdFer.disabled=false;")
						Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdFer.disabled=false;")
					Else
						If lclsType_amend.Find(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CInt(Request.QueryString.Item("nType_amend"))) Then
							If lclsType_amend.nLevel >= lclsSecur_sche.nAmelevel Then
								Response.Write("top.frames['fraHeader'].document.forms[0].tcdFer.disabled=false;")
								Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdFer.disabled=false;")
							End If
						End If
					End If
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdFer.disabled=false;")
					Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdFer.disabled=false;")
				End If
			End If
		End If
		
		If IsNothing(Request.QueryString.Item("dEffecdate")) Then
			If lclsType_amend.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), Today, mobjValues.StringToType(Request.QueryString.Item("nType_amend"), eFunctions.Values.eTypeData.etdDouble)) Then
				If lclsValpolicyseq.DateType_Amend(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsType_amend.nTypeIssue), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble)) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsValpolicyseq.dtmEffecdate, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
			End If
		End If
		'+ validaciones sobre la fecha de vigencia
		If lclsType_amend.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nType_amend"), eFunctions.Values.eTypeData.etdDouble)) Then
			'+	1	:	Primer día del mes siguiente
			If lclsType_amend.nTypeIssue = 1 Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, DateSerial(Year(Today), Month(Today), 1)), eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=true;")
				'+	2	:	Fecha de próxima facturación         
			ElseIf lclsType_amend.nTypeIssue = 2 Then 
				If lclsPolicy.Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsPolicy.dNextReceip, eFunctions.Values.eTypeData.etdDate) & "';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=true;")
				End If
				'+	3	:	Fecha de la inspección              
			ElseIf lclsType_amend.nTypeIssue = 3 Then 
				If lclsProf_ord.Find_nServ(mobjValues.StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble)) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsProf_ord.dMade_date, eFunctions.Values.eTypeData.etdDate) & "';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=true;")
				End If
				'+	4	:	Fecha del día                       
			ElseIf lclsType_amend.nTypeIssue = 4 Then 
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(Today(), eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=true;")
			Else
				If IsNothing(Request.QueryString.Item("nQuotProp")) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.disabled=false;")
				End If
			End If
		End If
	End If
	
	lclsUsers = Nothing
	lclsSecur_sche = Nothing
	lclsType_amend = Nothing
	lclsValpolicyseq = Nothing
	lclsProf_ord = Nothing
End Sub

'% InsShowClientData: Muestra los datos del cliente
'--------------------------------------------------------------------------------------------
Sub InsShowClientData()
	'--------------------------------------------------------------------------------------------
	Dim lclsClientRoles As ePolicy.Roles
	Dim lclsClient As eClient.Client
	Dim lblnChecked As String
	Dim lstrClient As String
	Dim lstrSmoking As String
	Dim lstrSexClien As String
	Dim ldtmBirthdat As String
	Dim lintTypename As Integer
	Dim lintPerson_typ As Byte
	Dim lblnOk As Boolean
	Dim lstrPerson_typ As Object
	Dim lintTypeRisk As Byte
	lclsClientRoles = New ePolicy.Roles
	lclsClient = New eClient.Client
	
	lstrClient = Request.QueryString.Item("sClient")
	
	
	If Len(lstrClient) < 14 Then
		lstrClient = lclsClient.ExpandCode(lstrClient)
	End If
	
	
	If lclsClientRoles.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.TypeToString(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), lstrClient, Session("dEffecdate"), True) Then
		
		lblnOk = True
		
		lstrPerson_typ = lclsClientRoles.nPerson_typ
		'+ Si se trata de un producto de vida.
		If CStr(Session("sBrancht")) = "1" Then
			lstrSmoking = lclsClientRoles.sSmoking
			lstrSexClien = lclsClientRoles.sSexclien
			ldtmBirthdat = mobjValues.TypeToString(lclsClientRoles.dBirthdate, eFunctions.Values.eTypeData.etdDate)
		End If
		
		lintTypename = mobjValues.TypeToString(lclsClientRoles.nTypename, eFunctions.Values.eTypeData.etdDouble)
		lintPerson_typ = mobjValues.TypeToString(lclsClientRoles.nPerson_typ, eFunctions.Values.eTypeData.etdDouble)
	Else
		If lclsClient.Find(lstrClient) Then
			lstrPerson_typ = lclsClient.nPerson_typ
			lblnOk = True
			'+ Si se trata de un producto de vida.
			If CStr(Session("sBrancht")) = "1" Then
				lstrSmoking = lclsClient.sSmoking
				lstrSexClien = lclsClient.sSexclien
				ldtmBirthdat = mobjValues.TypeToString(lclsClient.dBirthdat, eFunctions.Values.eTypeData.etdDate)
			End If
			lintPerson_typ = mobjValues.TypeToString(lclsClient.nPerson_typ, eFunctions.Values.eTypeData.etdDouble)
			
			If lintPerson_typ = 2 Then
				lintTypename = 1
			Else
				lintTypename = eRemoteDB.Constants.intNull
			End If
		End If
	End If
	
	'+ Si se consigió información
	If lblnOk Then
		If lstrSmoking = "1" Then
			lblnChecked = "true"
		Else
			lblnChecked = "false"
		End If
		
		Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
		
		If lstrSmoking = "1" Then
			lintTypeRisk = 4
			Response.Write("    cbeTyperisk.value='" & lintTypeRisk & "';")
		Else
			lintTypeRisk = 3
			Response.Write("    cbeTyperisk.value='" & lintTypeRisk & "';")
		End If
		
		
		
		If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotation Then
			'+ Si el usuario que se encuentra operando en el sistema no es un intermediario	       
			If Request.QueryString.Item("sIntermedia") <> "1" Then
				If lstrSmoking = "1" Then
					Response.Write("    cbeTyperisk.disabled=true; ")
				Else
					Response.Write("    cbeTyperisk.disabled=false; ")
				End If
			Else
				Response.Write("    cbeTyperisk.disabled=true; ")
				Response.Write("    chkSmoking.disabled=true; ")
			End If
		Else
			Response.Write("    cbeTyperisk.disabled=true; ")
		End If
		
		'+ Si se trata de un producto de vida.
		If CStr(Session("sBrancht")) = "1" Then
			Response.Write("    tcdBirthdate.value='" & ldtmBirthdat & "';")
			Response.Write("    cbeSexclien.value='" & lstrSexClien & "';")
			Response.Write("    chkSmoking.checked=" & lblnChecked & ";")
		End If
		Response.Write("    cbeTypename.value='" & lintTypename & "';")
		
		If lintPerson_typ = 2 Then
			Response.Write("    cbeTypename.disabled=false")
		Else
			Response.Write("    cbeTypename.disabled=true")
		End If
		
		Response.Write("}")
	End If
	
	If lstrPerson_typ = "2" Then
		Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[8].style.display='none';")
		Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[9].style.display='none';")
		Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TR')[4].style.display='none';")
	Else
		Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[8].style.display='';")
		Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TD')[9].style.display='';")
		Response.Write("top.frames['fraFolder'].document.getElementsByTagName('TR')[4].style.display='';")
	End If
	
	lclsClient = Nothing
	lclsClientRoles = Nothing
End Sub
'% InsShowRoles: Muestra los datos del rol.
'--------------------------------------------------------------------------------------------
Sub InsShowRoles()
	'--------------------------------------------------------------------------------------------
	Dim lclsRoles As ePolicy.Roles
	Dim lblnChecked_Smoking As String
	Dim lblnChecked_Typerisk As String
	Dim nInsuAge As String
	
	lclsRoles = New ePolicy.Roles
	
	With lclsRoles
		If .Find(Request.QueryString.Item("sCertype"), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), CShort(Request.QueryString.Item("nRole")), Request.QueryString.Item("sClient"), CDate(Request.QueryString.Item("dEffecdate"))) Then
			If .sSmoking = "1" Then
				lblnChecked_Smoking = "true"
			Else
				lblnChecked_Smoking = "false"
			End If
			
			'If .sTyperisk = "1" Then
			'	lblnChecked_Typerisk = "true"
			'Else
				lblnChecked_Typerisk = "false"
			'End If
			
			If .CalInsuAge(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking) Then
				nInsuAge = CStr(.nAge(True))
			End If
			
			Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
			Response.Write("    tcdBirthdate.value='" & mobjValues.TypeToString(.dBirthdate, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("    cbeSexclien.value='" & .sSexclien & "';")
			Response.Write("    chkSmoking.checked=" & lblnChecked_Smoking & ";")
			Response.Write("    chkTyperisk.checked=" & lblnChecked_Typerisk & ";")
			Response.Write("    tcnRating.value='" & mobjValues.TypeToString(.nRating, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("    cbeRoles.value='" & mobjValues.TypeToString(.nRole, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("    tcnInsuAge.value=" & nInsuAge & ";")
			Response.Write("}")
		End If
	End With
	
	lclsRoles = Nothing
End Sub

'% InsUpdnAge: Actualiza la edad
'--------------------------------------------------------------------------------------------
Sub InsUpdnAge()
	'--------------------------------------------------------------------------------------------
	Dim lclsDisco_expr As eProduct.Disco_expr
	
	lclsDisco_expr = New eProduct.Disco_expr
	
	Response.Write("with(top.frames['fraFolder'].document.forms[0]){")
	Response.Write("      tcnAge.disabled=false;")
	Response.Write("      tcnAge.value='';")
	Response.Write("}")
	
	With lclsDisco_expr
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			If .nRate <> eRemoteDB.Constants.intNull And .nRate <> 0 Then
				Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
				Response.Write("    tcnRate.value='" & mobjValues.TypeToString(.nRate, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
				Response.Write("    tcnAge.disabled=true;")
				Response.Write("    tcnAge.value='';")
				Response.Write("}")
			Else
				If .nDisexpra <> eRemoteDB.Constants.intNull And .nDisexpra <> 0 Then
					Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
					Response.Write("    tcnAmount.value='" & mobjValues.TypeToString(.nDisexpra, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
					Response.Write("    tcnAge.disabled=true;")
					Response.Write("    tcnAge.value='';")
					Response.Write("}")
				End If
			End If
		End If
	End With
	
	lclsDisco_expr = Nothing
End Sub

'% InsCalCapital: Llama al procedimiento que calcula el capital de una cobertura dada
'--------------------------------------------------------------------------------------------
Sub InsCalCapital()
	'--------------------------------------------------------------------------------------------
	Dim lclsCover As ePolicy.Cover
	
	lclsCover = New ePolicy.Cover
	
	With Request
		If lclsCover.InsCalCapital(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nCacalfix"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sCacalfri"), .QueryString.Item("sCacalili"), mobjValues.StringToType(.QueryString.Item("nCacalcov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCacalper"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sKey"), mobjValues.StringToType(.QueryString.Item("nRolcap"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sRoucapit"), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClient"), .QueryString.Item("sBrancht"), mobjValues.StringToType(.QueryString.Item("nCurrencyOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCamaxcov"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCamaxper"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCamaxrol"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCacalmul"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrencyDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgeminins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgemaxins"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sBas_sumins"), mobjValues.StringToType(.QueryString.Item("nTypdurins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypdurpay"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital_wait"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCacalmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCacalmax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
			Response.Write("    tcnCapital.value = '" & mobjValues.TypeToString(lclsCover.nCapital, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("    hddnCapital.value = tcnCapital.value;")
			Response.Write("}")
		End If
	End With
	lclsCover = Nothing
End Sub

'% InsCalPremium: Llama al procedimiento que calcula la prima de una cobertura dada
'--------------------------------------------------------------------------------------------
Sub InsCalPremium()
	'--------------------------------------------------------------------------------------------
	Dim lclsCover As ePolicy.Cover
	
	lclsCover = New ePolicy.Cover
	
	With Request
		If lclsCover.InsCalPremium(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), mobjValues.StringToType(.QueryString.Item("nRetarif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover_in"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sRoupremi"), mobjValues.StringToType(.QueryString.Item("nCurrencyOri"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrencyDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), .QueryString("sKey"), mobjValues.StringToType(.QueryString.Item("nPremifix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremirat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCoverapl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dSeektar"), eFunctions.Values.eTypeData.etdDate), .QueryString("sBrancht"), mobjValues.StringToType(.QueryString.Item("nApply_perc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremimax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRatecove"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypdurins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypdurpay"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sExist"), .QueryString("sBas_sumins"), mobjValues.StringToType(.QueryString.Item("nDurpay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDurinsur"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRatecove_o"), eFunctions.Values.eTypeData.etdDouble), Integer.MinValue) Then
			
			Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
			Response.Write("    tcnCapital.value = '" & mobjValues.TypeToString(lclsCover.nCapital, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("    hddnCapital.value = tcnCapital.value;")
			Response.Write("    tcnRatecove.value = '" & mobjValues.TypeToString(lclsCover.nRateCove, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("    hddnRateCove.value = tcnRatecove.value;")
			Response.Write("    tcnPremium.value = '" & mobjValues.TypeToString(lclsCover.nPremium, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("    hddnPremium.value= tcnPremium.value;")
			
			'+Si el calculo se llama desde el campo Capital o retarifica se asigna tasa y prima a 
			'+los valores anteriores para evitar que se envie validaciones de permitir cambios
			If .QueryString.Item("sOrigen") = "1" Then
				Response.Write("hddnRatecove_o.value= tcnRatecove.value;")
				Response.Write("hddnPremium_o.value= tcnPremium.value;")
				
				'+Si el calculo se llama desde el campo Tasa se asigna prima a los valores anteriores
				'+para evitar que se envie validaciones de permitir cambios
			ElseIf .QueryString.Item("sOrigen") = "2" Then 
				Response.Write("hddnPremium_o.value= tcnPremium.value;")
			End If
			Response.Write("}")
		End If
	End With
	lclsCover = Nothing
End Sub

'% inscreTar_am_pol: Se crea la información por defecto de la transacción AM002
'--------------------------------------------------------------------------------------------
Sub inscreTar_am_pol()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_am_pol As eBranches.Tar_am_pol
	
	lclsTar_am_pol = New eBranches.Tar_am_pol
	
	Call lclsTar_am_pol.AddDefaultValue(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), CInt(Request.QueryString.Item("nTariff")), CInt(Request.QueryString.Item("nGroup")), CInt(Request.QueryString.Item("nRole")), Request.QueryString.Item("sDefaulti"), Session("nUsercode"), CInt(Request.QueryString.Item("nModulec")), CInt(Request.QueryString.Item("nCover")))
	
	Response.Write("top.frames[""fraSequence""].location.reload();")
	Response.Write("top.frames[""fraFolder""].location.reload();")
	
	lclsTar_am_pol = Nothing
End Sub

'% insUpdTar_am_pol_Defaulti: Actualiza el campo sDefaulti de la tabla tar_am_bas en la secuencia de póliza.
'--------------------------------------------------------------------------------------------
Sub insUpdTar_am_pol_Defaulti()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_am_bas As eBranches.Tar_am_bas
	Dim lclsPolicyWin As ePolicy.Policy_Win
	Dim lintCount As Integer
	Dim lstrDefaulti As String
	lclsTar_am_bas = New eBranches.Tar_am_bas
	
	If lclsTar_am_bas.insCreUpdTar_am_bas(Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), Session("dNulldate"), CInt(Request.QueryString.Item("nTariff")), CInt(Request.QueryString.Item("nRole")), CInt(Request.QueryString.Item("nGroup")), Request.QueryString.Item("sDefaulti"), Session("nUsercode"), CInt(Request.QueryString.Item("nModulec")), CInt(Request.QueryString.Item("nCover"))) Then
		Session("sDefaulti") = Request.QueryString.Item("sDefaulti")
	End If
	lintCount = lclsTar_am_bas.getCountTar_am_bas(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), "1")
	If lintCount > 0 Then
		lstrDefaulti = "2"
	Else
		lstrDefaulti = "1"
	End If
	
	lclsPolicyWin = New ePolicy.Policy_Win
	'+ Se actualiza la imagen de Contenido para que quede requerida
	Call lclsPolicyWin.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), "AM002", lstrDefaulti)
	
	lclsPolicyWin = Nothing
	lclsTar_am_bas = Nothing
End Sub

'% InsCreTab_am_exc: Se crea la información por defecto de la transacción AM006
'--------------------------------------------------------------------------------------------
Sub InsCreTab_am_exc()
	'--------------------------------------------------------------------------------------------
	Dim lclsInsCreTab_am_exc As ePolicy.Tab_am_exc
	
	lclsInsCreTab_am_exc = New ePolicy.Tab_am_exc
	
	If lclsInsCreTab_am_exc.GeTTab_am_exc_by_prod("AM006", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sInsured"), Session("dEffecdate"), Session("nUsercode"), Session("dNulldate"), Session("nTransaction")) Then
		Response.Write("top.frames['fraFolder'].location.reload();")
		mblnRefresh = True
	End If
	
	lclsInsCreTab_am_exc = Nothing
End Sub

'% inscreTab_am_bil: Se crea la información por defecto de la transacción AM003
'--------------------------------------------------------------------------------------------
Sub inscreTab_am_bil()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_am_bil As eBranches.Tab_Am_Bil
	
	lclsTab_am_bil = New eBranches.Tab_Am_Bil
	
	Dim lclsTab_am_bab As eBranches.Tab_Am_Bab
	If CBool(IIf(IsNothing(Request.QueryString.Item("bCreHeader")), False, Request.QueryString.Item("bCreHeader"))) Then
		lclsTab_am_bab = New eBranches.Tab_Am_Bab
		
		If lclsTab_am_bab.insCreUpdTab_am_bab(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sAutRestit"), mobjValues.StringToType(Request.QueryString.Item("nLimitH"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("dNulldate"), Session("nTransaction"), Session("nUsercode"), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), Request.QueryString.Item("sIllness"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			Response.Write("top.fraFolder.document.forms[0].hddbCreHeader.value=false;")
		End If
		lclsTab_am_bab = Nothing
	End If
	
	If lclsTab_am_bil.Load(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), CInt(Request.QueryString.Item("nTariff")), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), Request.QueryString.Item("sIllness"), Session("dEffecdate"), Session("nUsercode")) Then
		Response.Write("top.frames[""fraFolder""].location.reload();")
	End If
	
	lclsTab_am_bil = Nothing
End Sub

'% inscreTab_am_bab: Se crea la información por defecto de la transacción AM003
'--------------------------------------------------------------------------------------------
Sub inscreTab_am_bab()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_am_bab As eBranches.Tab_Am_Bab
	
	lclsTab_am_bab = New eBranches.Tab_Am_Bab
	
	With Request
		If lclsTab_am_bab.insCreUpdTab_am_bab(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sAutRestit"), mobjValues.StringToType(.QueryString.Item("nLimitH"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("dNullDate"), Session("nTransaction"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClient"), .QueryString.Item("sIllness"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("top.fraFolder.document.forms[0].hddbCreHeader.value=false;")
		Else
			Response.Write("top.fraFolder.document.forms[0].hddbCreHeader.value=true;")
		End If
	End With
	lclsTab_am_bab = Nothing
End Sub

'% insVerifySel: Verifica si un campo puede ser Borrado
'--------------------------------------------------------------------------------------------
Sub insVerifySel()
'--------------------------------------------------------------------------------------------
	Dim lclsSituation As ePolicy.Situation
	
	If Request.QueryString.Item("sCodispl") = "CA008" Then
		lclsSituation = New ePolicy.Situation
		If lclsSituation.FindCertificatCA008(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), CInt(Request.QueryString.Item("nSituation"))) Then
			With Response
				.Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "])!='undefined')?")
				.Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "].checked=false:")
				.Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
				.Write("alert(""Err 3301: " & eFunctions.Values.GetMessage(3301) & """);")
			End With
		End If
	End If
	
	lclsSituation = Nothing
End Sub

'% InsDelTCover: Marca la cobertura como eliminada en la tabla TCOVER
'--------------------------------------------------------------------------------------------
Private Sub InsDelTCover()
	'--------------------------------------------------------------------------------------------
	Dim lclsTCover As ePolicy.TCover
	
	lclsTCover = New ePolicy.TCover
	With lclsTCover
		If lclsTCover.Find(Request.QueryString.Item("sKey"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sClient")) Then
			.sKey = Request.QueryString.Item("sKey")
			.sCertype = Session("sCertype")
			.nBranch = Session("nBranch")
			.nProduct = Session("nProduct")
			.nPolicy = Session("nPolicy")
			.nCertif = Session("nCertif")
			.nGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)
			.nModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
			.nCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)
			.nRole = mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble, True)
			.sClient = Request.QueryString.Item("sClient")
			.sDefaulti = "9"
			.sCodispl = "CA014"
			Call .Add()
		End If
	End With
	lclsTCover = Nothing
End Sub

'% ValDeleteGroups: se verifica que no existan registros en el archivo de certificados
'% asociados al grupo
'--------------------------------------------------------------------------------------------
Private Sub ValDeleteGroups()
	Dim C_MESSAGE_55893 As String
	'--------------------------------------------------------------------------------------------
	Dim lclsClaus As ePolicy.Claus_co_gp
	lclsClaus = New ePolicy.Claus_co_gp
	
	Call lclsClaus.FindGroupLinks(Session("sCertype"), Session("nBranch"), Session("nPolicy"), Session("nProduct"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble))
	If lclsClaus.nLink <> 0 Then
		With Response
			.Write("(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "])!='undefined')?")
			.Write("top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "].checked=false:")
			.Write("top.frames[""fraFolder""].document.forms[0].Sel.checked=false;")
                .Write("alert(""Err 55893: " & eFunctions.Values.getMessage(55893) & """);")
		End With
	End If
	Response.Write("top.frames[""fraFolder""].document.cmdDelete.disabled = false;")
	
	lclsClaus = Nothing
End Sub

'% insUpdateCheckVI662: Se encarga de eliminar el elemnto seleccionado cuando se trata del tratamiento de un certificado.
'--------------------------------------------------------------------------------------------
Private Sub insUpdateCheckVI662()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife_levels As ePolicy.life_levels
	
	lclsLife_levels = New ePolicy.life_levels
	
	With Request
		If lclsLife_levels.InsPostvi662("Del", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sTyplevel"), mobjValues.StringToType(.QueryString.Item("nLevel"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, Session("nUsercode"), Session("sPolitype"), Session("sBrancht")) Then
		End If
	End With
	lclsLife_levels = Nothing
End Sub

'% insReaAge_collect: realiza la búsqueda de los tramos de edad para cotizaciones de vida 
'--------------------------------------------------------------------------------------------
Private Sub insReaAge_collect()
	'--------------------------------------------------------------------------------------------
	Dim lclsAge_collect As eBranches.Age_collect
	
	lclsAge_collect = New eBranches.Age_collect
	
	If lclsAge_collect.Find(Session("nBranch"), Session("nProduct"), Session("deffecdate"), mobjValues.StringToType(Request.QueryString.Item("nInitAge"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnInitAge.value=""" & lclsAge_collect.nInitAge & """;")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnEndAge.value=""" & lclsAge_collect.nEndAge & """;")
	End If
	
	lclsAge_collect = Nothing
End Sub

'%InsRefresSequence : Llama el procedimiento que refresca la secuencia
'--------------------------------------------------------------------------------------------
Private Sub InsRefresSequence(ByVal sCodispl As String)
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy_Seq As ePolicy.ValPolicySeq
	
	lclsPolicy_Seq = New ePolicy.ValPolicySeq
	
	Response.Write(lclsPolicy_Seq.RefreshSequence(sCodispl, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("sBrancht"), Session("sPolitype"), "No"))
	
	lclsPolicy_Seq = Nothing
End Sub

'% insConvertAmounting: Convierte un monto utilizando el factor de cambio.
'--------------------------------------------------------------------------------------------
Sub insConvertAmounting()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchanges As eGeneral.Exchange
	Dim nCurrency_ing As String
	Dim nAmount As Object
	
	lclsExchanges = New eGeneral.Exchange
	nCurrency_ing = Request.QueryString.Item("nCurrency_ing")
	nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble, True)
	
	If mobjValues.StringToType(nAmount, eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Then
		nAmount = 0
	End If
	
	Call lclsExchanges.Convert(0, mobjValues.StringToType(nAmount, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCurrency_ing, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dReqDate"), eFunctions.Values.eTypeData.etdDate), 0)
	Response.Write(" top.frames[""fraFolder""].document.forms[0].tcnDiscount.value = '" & mobjValues.TypeToString(lclsExchanges.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
	Response.Write("top.frames['fraFolder'].document.forms[0].hddnCurrency.value='" & nCurrency_ing & "';")
	
	lclsExchanges = Nothing
End Sub

'% insSumDate: se calcula la fecha de vencimiento de la póliza
'--------------------------------------------------------------------------------------------
Private Sub insSumDate()
	'--------------------------------------------------------------------------------------------
	Dim DateResul As Date
	Dim lclsProduct_li As eProduct.Product
	
	If mobjValues.StringToType(Request.QueryString.Item("nDuration"), eFunctions.Values.eTypeData.etdInteger) > 0 Then
		DateResul = mobjValues.SumTypeDate("m", mobjValues.StringToType(Request.QueryString.Item("nDuration"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dStartDate"), eFunctions.Values.eTypeData.etdDate))
		If CStr(Session("sBrancht")) = "1" Then
			DateResul = mobjValues.SumTypeDate("d", -1, DateResul)
		End If
		Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDate.value ='" & mobjValues.TypeToString(DateResul, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("top.frames['fraFolder'].$('#tcdExpirDate').change();")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDate.disabled=true;")
		'+ Si la renovación de la póliza matriz es independiente y el tipo de recibo es por póliza,
		'+ se puede modificar la fecha de vencimiento
		If Request.QueryString.Item("sColtimre") = "2" And Request.QueryString.Item("sColinvot") = "1" Then
			Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDate.disabled=false;")
		End If
	End If
	If CStr(Session("sBrancht")) = "1" Then
		lclsProduct_li = New eProduct.Product
		Call lclsProduct_li.FindProduct_li(Session("nbranch"), Session("nproduct"), Session("deffecdate"))
		Response.Write("top.frames['fraFolder'].document.forms[0].hhprodclas.value= '" & lclsProduct_li.nProdClas & "';")
	End If
	lclsProduct_li = Nothing
End Sub

'%insnServ_order : recupera la fecha de la orden de servicio y se la asigna a la 
'				   fecha de efecto de la poliza.
'--------------------------------------------------------------------------------------------
Private Sub insnServ_order()
	'--------------------------------------------------------------------------------------------
	Dim lclsProf_ord As eClaim.Prof_ord
	Dim lclsType_amend As ePolicy.Type_amend
	lclsProf_ord = New eClaim.Prof_ord
	lclsType_amend = New ePolicy.Type_amend
	
	If Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngPolicyProposal And Request.QueryString.Item("nTransaction") <> eCollection.Premium.PolTransac.clngCertifProposal Then
		If Not IsNothing(Request.QueryString.Item("nType_amend")) Then
			If lclsType_amend.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nType_amend"), eFunctions.Values.eTypeData.etdDouble)) Then
				If lclsType_amend.nTypeIssue = 3 Then
					If lclsProf_ord.Find_nServ(mobjValues.StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble)) Then
						If lclsProf_ord.dMade_date <> eRemoteDB.Constants.dtmNull Then
							Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsProf_ord.dMade_date, eFunctions.Values.eTypeData.etdDate) & "';")
						End If
					End If
				End If
			End If
		Else
			If lclsProf_ord.Find_nServ(mobjValues.StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble)) Then
				If lclsProf_ord.dMade_date <> eRemoteDB.Constants.dtmNull Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mobjValues.TypeToString(lclsProf_ord.dMade_date, eFunctions.Values.eTypeData.etdDate) & "';")
				End If
			End If
		End If
	End If
	lclsType_amend = Nothing
	lclsProf_ord = Nothing
End Sub

'% DeleteValues: Elimina los registros de la CA658
'--------------------------------------------------------------------------------------------
Sub DeleteValues()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Client_tmp
	lclsPolicy = New ePolicy.Client_tmp
	Call lclsPolicy.Delete_All(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	lclsPolicy = Nothing
End Sub

'% insCancel: Esta rutina es activada cuando el usuario cancela la transacción en donde
'%            está trabajando, solo para transaciones relacionadas con endoso.
'--------------------------------------------------------------------------------------------
Function insCancel() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsErrors As eFunctions.Errors
	Dim lclsPolicy As Object
	Dim lclsCertificat As ePolicy.Certificat
	
	lclsCertificat = New ePolicy.Certificat
	
	insCancel = True
	
	'+ Se realiza el llamado al procedimiento que actualiza el campo UserAmend 
	'+ de Policy o Certificat, según sea el caso
	Call insUpdUserAmend()
	'+Se realiza el reverso de la modificación
	If CBool(Trim(CStr(CStr(Session("nTransaction")) <> vbNullString))) Then
		If Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPropAmendConvertion Then
			
			If Not lclsCertificat.insReverRenModPol(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 1) Then
				lclsErrors = New eFunctions.Errors
				Response.Write(lclsErrors.ErrorMessage("CA001_K", 3616,  ,  ,  , True))
			Else
				lclsErrors = New eFunctions.Errors
				Response.Write(lclsErrors.ErrorMessage("CA001_K", 3998,  ,  ,  , True))
			End If
		End If
	End If
	
	lclsErrors = Nothing
	lclsCertificat = Nothing
End Function

'% insCalAmountUF: Permite expresar el monto de la transferencia en UF.
'--------------------------------------------------------------------------------------------
Private Sub insCalAmountUF()
	'--------------------------------------------------------------------------------------------
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
	Call lclsExchange.Convert(0, mobjValues.StringToType(Request.QueryString.Item("nAmount_peso"), eFunctions.Values.eTypeData.etdDouble), 1, 4, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0)
	Response.Write("opener.document.forms[0].tcnAmount_UF.value='" & mobjValues.TypeToString(lclsExchange.pdblResult, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
	lclsExchange = Nothing
End Sub


'% insHabilitateTax: Permite expresar el monto de la transferencia en UF.
'--------------------------------------------------------------------------------------------
Private Sub insHabilitateTax()
	'--------------------------------------------------------------------------------------------
	Dim lclsModules As ePolicy.Modules
	
	lclsModules = New ePolicy.Modules
	
	If Request.QueryString.Item("Action") = "Add" Then
		If mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
			Call lclsModules.Findtabmodul(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble))
			If lclsModules.styp_rat = "1" Then
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremirat.disabled=false;")
				
				Response.Write("top.frames['fraFolder'].document.forms[0].hddstyp_rat.value='1';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremirat.value='" & mobjValues.TypeToString(lclsModules.nPremirat, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremirat.disabled=true;")
				Response.Write("top.frames['fraFolder'].document.forms[0].hddstyp_rat.value='';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremirat.value='';")
			End If
			
			If lclsModules.sChanallo = "1" Then
				Response.Write("top.frames['fraFolder'].document.forms[0].chkChange.Checked=true;")
				Response.Write("top.frames['fraFolder'].document.forms[0].chkChange.value='1';")
				Response.Write("top.frames['fraFolder'].document.forms[0].chkChange.disabled=false;")
			Else
				Response.Write("top.frames['fraFolder'].document.forms[0].chkChange.disabled=true;")
			End If
		End If
	End If
	lclsModules = Nothing
End Sub


'%insUpdT_Life_docu: Permite modificar actualizando como pendiente 
'%					 Sin fecha de recepcion
'--------------------------------------------------------------------------
Private Sub insUpdT_Life_docu()
	'--------------------------------------------------------------------------
	'-Objeto de conversion par eliminar datos
	Dim lclsT_Life_docu As ePolicy.Life_docu
	
	Dim lstrRequire As String
	
	lclsT_Life_docu = New ePolicy.Life_docu
	
	With mobjValues
		
		If Request.QueryString.Item("sRequire") = "true" Then
			lstrRequire = "1"
		Else
			lstrRequire = "2"
		End If
		
		Call lclsT_Life_docu.InsPostVI021Upd(Request.QueryString.Item("nActionPop"), Session("sKey"), Request.QueryString.Item("sDescript"), .StringToType(Request.QueryString.Item("nCrThecni"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dRecep_date"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("sStat_docreq"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dDate_to"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dDatefree"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nEval"), eFunctions.Values.eTypeData.etdDouble), .StringToType("", eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nNotenum"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCumul"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nStatusdoc"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dDocreq"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dDocrec"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nNotenum_cli"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nEval_master"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nExist"), eFunctions.Values.eTypeData.etdDouble), lstrRequire)
		
	End With
	lclsT_Life_docu = Nothing
	
	Response.Write("var lstrURL = """" ;")
	Response.Write("lstrURL += top.frames['fraFolder'].document.location;")
	Response.Write("lstrURL = lstrURL.replace(/sCodispl=.*/, '');")
	Response.Write("lstrURL = lstrURL + '" & "sCodispl=VI021&Reload=&ReloadAction=Update&ReloadIndex=0&nMainAction=304&sKey=" & Session("sKey") & "';")
	Response.Write("top.frames['fraFolder'].document.location = lstrURL;")
End Sub

'% insValDate_End: Valida la Fecha hasta del credito buscando la menor entre la rutina 
'  y el calculado dentro de la pagina
'--------------------------------------------------------------------------------------------
Sub insValDate_End()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife As ePolicy.Life
	Dim lstrDate_End As Object
	
	lclsLife = New ePolicy.Life
	With lclsLife
		Call lclsLife.InsRoutineDuration(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("dNewDate"), eFunctions.Values.eTypeData.etdDate))
		'lstrDate_End = .dDate_end								 
		'Response.Write "top.frames['fraFolder'].document.forms[0].tcdEnd_cre.value=lstrDate_End;"
		Response.Write("top.frames['fraFolder'].document.forms[0].tcdEnd_cre.value='" & mobjValues.TypeToString(.dDate_end, eFunctions.Values.eTypeData.etdDate) & "';")
	End With
	
	lclsLife = Nothing
End Sub

'% insTabCertif: 
'--------------------------------------------------------------------------------------------
Sub insTabCertif()
	'--------------------------------------------------------------------------------------------       
	If Request.QueryString.Item("sClient") <> vbNullString Then
		Response.Write("top.frames['fraHeader'].document.forms[0].valCertif.Parameters.Param5.sValue='" & Request.QueryString.Item("sClient") & "';")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertificat.value = top.frames['fraHeader'].document.forms[0].valCertif_nCertif.value;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertificat.disabled = true;")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertificat.disabled = false;")
	End If
End Sub

'% insShowReceipt_ind: 
'--------------------------------------------------------------------------------------------
Sub insShowReceipt_ind()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	
	lclsProduct = New eProduct.Product
	
	With lclsProduct
		If .Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
			If Request.QueryString.Item("nReceipt_ind") = "1" Then
				Response.Write("opener.document.forms[0].tcnTerm_grace.value='" & .nPayable & "';")
			Else
				Response.Write("opener.document.forms[0].tcnTerm_grace.value='" & .nAdvance & "';")
			End If
			
		End If
	End With
	
	lclsProduct = Nothing
End Sub

'% Calculate_date: 
'--------------------------------------------------------------------------------------------
Sub Calculate_date()
	'--------------------------------------------------------------------------------------------
	Dim nValue As Byte
	
	nValue = mobjValues.StringToType(Request.QueryString.Item("nValue"), eFunctions.Values.eTypeData.etdLong)
	If nValue < 0 Then
		nValue = 0
	End If
	Response.Write("top.frames['fraFolder'].document.forms[0].tcdStart_GuarSav.value='" & Session("dTariffDate") & "';")
	Response.Write("top.frames['fraFolder'].document.forms[0].tcdEnd_GuarSav_to.value='" & DateAdd(Microsoft.VisualBasic.DateInterval.Month, nValue * 12, Session("dTariffDate")) & "';")
	insShowVI8000()
	
End Sub

'insShowVI8000: 
'--------------------------------------------------------------------------------------------
Sub insShowVI8000()
	'--------------------------------------------------------------------------------------------
	Dim lclsGuar_Saving_Pol As ePolicy.Guar_Saving_Pol
	lclsGuar_Saving_Pol = New ePolicy.Guar_Saving_Pol
	
	
	If lclsGuar_Saving_Pol.insShowVI8000(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nGuarsav_value"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGuarsav_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRen_guarsav"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sPay"), mobjValues.StringToType(Request.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdDouble)) Then
		If mobjValues.StringToType(Request.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnRen_guarSav.value='" & lclsGuar_Saving_Pol.nRen_guarSav & "';")
			If lclsGuar_Saving_Pol.insShowVI8000(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nGuarsav_value"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGuarsav_year"), eFunctions.Values.eTypeData.etdDouble), lclsGuar_Saving_Pol.nRen_guarSav, Request.QueryString.Item("sPay"), 2) Then
				If lclsGuar_Saving_Pol.nGuarsav_prem < 0 Then
					lclsGuar_Saving_Pol.nGuarsav_prem = 0
				End If
				Response.Write("top.frames['fraFolder'].document.forms[0].hddnCost.value='" & lclsGuar_Saving_Pol.nGuarsav_cost & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].hddnPremium.value='" & lclsGuar_Saving_Pol.nGuarsav_prem & "';")
				Response.Write("top.frames['fraFolder'].document.forms[0].tcnGuarSav_prem.value='" & lclsGuar_Saving_Pol.nGuarsav_prem + lclsGuar_Saving_Pol.nGuarsav_cost & "';")
			End If
		Else
			If lclsGuar_Saving_Pol.nGuarsav_prem < 0 Then
				lclsGuar_Saving_Pol.nGuarsav_prem = 0
			End If
			Response.Write("top.frames['fraFolder'].document.forms[0].hddnCost.value='" & lclsGuar_Saving_Pol.nGuarsav_cost & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].hddnPremium.value='" & lclsGuar_Saving_Pol.nGuarsav_prem & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnGuarSav_prem.value='" & lclsGuar_Saving_Pol.nGuarsav_prem + lclsGuar_Saving_Pol.nGuarsav_cost & "';")
		End If
		
	End If
	
	lclsGuar_Saving_Pol = Nothing
End Sub



Sub UpdateInitialPayment()
	Dim nNewValue As Double
	Dim lobjDeposit As ePolicy.Per_deposit_month
	nNewValue = mobjValues.StringToType(Request.QueryString.Item("nNewValue"), eFunctions.Values.eTypeData.etdDouble)
	lobjDeposit = New ePolicy.Per_deposit_month
	Call lobjDeposit.InsPostVI1410AUpd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 1, 1, Session("dEffecdate"), nNewValue, Session("nUserCode"))
	lobjDeposit = Nothing
End Sub

Sub GenAgreement()
	
	Dim lintCod_Agree As Object
	Dim nCod_Agree As Integer
	Dim lobjRoles As ePolicy.Roles
	Dim lobjAgreements As eCollection.Agreements
	Dim lobjAgreement As eCollection.Agreement
	nCod_Agree = 0
	lobjRoles = New ePolicy.Roles
	If lobjRoles.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 25, "", Session("dEffecdate"), True) Then
		If lobjRoles.sClient <> vbNullString Then
			lobjAgreements = New eCollection.Agreements
			Call lobjAgreements.Find_sClient(0, lobjRoles.sClient, True)
			For	Each lobjAgreement In lobjAgreements
				If lobjAgreement.nType_rec = 8 Then
					nCod_Agree = lobjAgreement.nCod_Agree
					Exit For
				End If
			Next lobjAgreement
			lobjAgreements = Nothing
			lobjAgreement = Nothing
			If nCod_Agree = 0 Then
				lobjAgreement = New eCollection.Agreement
				lobjAgreement.nTypeAgree = 1
				lobjAgreement.sClient = lobjRoles.sClient
				lobjAgreement.nUsercode = Session("nUsercode")
				lobjAgreement.nIntermed = eRemoteDB.Constants.intNull
				lobjAgreement.nType_rec = 8
				lobjAgreement.sStatregt = "1"
				lobjAgreement.dInit_date = Today
				lobjAgreement.nAgency = eRemoteDB.Constants.intNull
				lobjAgreement.sCliename = lobjRoles.sCliename
				lobjAgreement.sName_Agree = lobjRoles.sCliename
				lobjAgreement.Add()
				nCod_Agree = lobjAgreement.nCod_Agree
				lobjAgreement = Nothing
			End If
			If nCod_Agree <> 0 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].valAgreement.value='" & nCod_Agree & "';")
				Response.Write("top.frames['fraFolder'].UpdateDiv(""valAgreementDesc"",'" & lobjRoles.sCliename & "','Normal');")
			End If
		End If
	End If
	lobjRoles = Nothing
	
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"
mblnRefresh = False
mobjValues = New eFunctions.Values

%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>
<SCRIPT>

//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 40 $|$$Date: 5/11/04 12:01 $|$$Author: Iusr_llanquihue $"

//%insShowIntermed: Muestra los datos del intermediario
//--------------------------------------------------------------------------------------------
function insShowIntermed(sParticin){ 
//--------------------------------------------------------------------------------------------

//+ Se bloquea el campo % si el tipo de comisión es <> de comisión fija
    if(opener.document.forms[0].cbeType.value!="2" && 
       opener.document.forms[0].cbeRole.value!=20){   
       opener.document.forms[0].tcnPercent.disabled=true; 
    }     

//+ Se bloquea el campo Importe si el tipo de comisión es <> de comisión fija, y participa en las comisiones 
    if(opener.document.forms[0].cbeType.value!="2" && 
       opener.document.forms[0].cbeRole.value!=20 && 
       (sParticin=="1" || 
        sParticin=="")){ 
        opener.document.forms[0].tcnAmount.disabled=true;
    }        
        
    if(opener.document.forms[0].cbeType.value=="2" &&
       sParticin!="1"){
        opener.document.forms[0].tcnnAmount.value="";
        opener.document.forms[0].tcnPercent.value="";
    }
}
</SCRIPT>
</HEAD>
<BODY>
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>
<%
Response.Write(Request.QueryString.Item("Field"))
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Propolcer"
		Call insShowPolicy()
		Call insShowCertificat()
		Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
		
	Case "insValDate_End"
		Call insValDate_End()
		
	Case "Policy"
		Call insShowPolicy()
		
	Case "Certificat"
		Call insShowCertificat()
		
	Case "Product"
		Call insShowProduct()
		
	Case "Slicense_ty"
		Call insSlicense_ty()
		
	Case "Auto"
		Call insShowAuto()
		
	Case "Auto_Regist"
		Call insShowAuto_Regist()
		
	Case "Intermed"
		Call insShowIntermed()
		
	Case "NullAdvise"
		Call insShowNullAdvise()
		
	Case "Agreement"
		Call insShowAgreement()
		
	Case "Account"
		If Request.QueryString.Item("sType_debit") = "1" Then
			InsShowAccount_Data()
		Else
			InsShowCard_Data()
		End If
		
	Case "CreditCard_Data"
		InsShowCreditCard_Data()
		
	Case "UserAmend"
		Call insUpdUserAmend()
		
	Case "Endoso"
		Call insShowEndoso()
		
	Case "Modulec"
		Call insHabilitateTax()
		
	Case "ClientRoles"
		Call InsShowClientData()
		
	Case "Roles"
		Call InsShowRoles()
		
	Case "Curren_Disexprc"
		Call InsUpdnAge()
		
	Case "Premium"
		Call InsCalPremium()
		
	Case "Capital"
		Call InsCalCapital()
		
	Case "WayPay"
		Call InsCalWayPay()
		
		If Not IsNothing(Request.QueryString.Item("dStartDate")) Then
			Call insSumDate()
		End If
		
	Case "Tar_am_pol"
		Call inscreTar_am_pol()
		
	Case "Defaulti"
		Call insUpdTar_am_pol_Defaulti()
		
	Case "Tab_am_exc"
		Call InsCreTab_am_exc()
		
	Case "Tab_am_bil"
		Call inscreTab_am_bil()
		
	Case "creTab_am_bab"
		Call inscreTab_am_bab()
		
	Case "sSel"
		Call insVerifySel()
		
	Case "DelTCover"
		Call InsDelTCover()
		
	Case "DeleteCA011"
		Call ValDeleteGroups()
		
	Case "UpdateCheckVI662"
		Call insUpdateCheckVI662()
		
	Case "ReaAge_collectCA658"
		Call insReaAge_collect()
		
	Case "SumDate"
		Call insSumDate()
		
	Case "insCancel"
		Call insCancel()
		
	Case "nServ_order"
		Call insnServ_order()
		
	Case "nExchange"
		Call insConvertAmounting()
		
	Case "deleteValues"
		Call DeleteValues()
		
	Case "AmountUF"
		Call insCalAmountUF()
		
	Case "TypeInvest"
		Call insChangeTypeInvest()
		
	Case "UpdT_Life_docu"
		Call insUpdT_Life_docu()
		
	Case "UpdateCA659"
		Call InsCA659A()
		
	Case "Subtraction"
		Call CalcPercent()
		
	Case "TabCertif"
		Call insTabCertif()
		
	Case "Receipt_ind"
		Call insShowReceipt_ind()
		
	Case "Calculate_date"
		Call Calculate_date()
		
	Case "insShowVI8000"
		Call insShowVI8000()
		
	Case "UpdateInitialPayment"
		Call UpdateInitialPayment()
	Case "GenAgreement"
		Call GenAgreement()
        Case "Cancel"
            Call insCancelDP()
        Case "Tab_bill_i"
            Call inscreTab_bill()
        Case "DataAssociate"
            Call valAssociate()
        Case "CoverData"
            Call valAssociate_Cover()
        Case "validateDP032"
            Call valModulDP032()
        Case "Tab_short"
            Call inscreTab_short()
        Case "DeleteDP578"
            Call ValDelete()
        Case "DeleteDP009"
            Call ValDeleteClause()
        Case "DeleteDP008"
            Call ValDeleteDisexprc()
        Case "UpdateCapital"
            Call UpdateCapital()
        Case "UpdateDP809"
            Call InsDP809A()
        Case "ConsecuClause"
            Call ConsecuClause()
				
    End Select


Response.Write("setPointer('');")
Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
mobjValues = Nothing

'+Se valida si se refresca la secuencia
If mblnRefresh Then
	Call InsRefresSequence(Request.QueryString.Item("sCodispl"))
End If



%>





