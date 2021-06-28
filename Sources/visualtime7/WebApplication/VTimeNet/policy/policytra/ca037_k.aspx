<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eOptionSystem" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjError As eFunctions.Errors
Dim mclsPolicy As ePolicy.Policy
Dim mclsCertificat As ePolicy.Certificat
Dim mPoliType As Boolean

Dim mintOffice As Object
Dim mintOfficeAgen As Object
Dim mdtmIssuedat As String
Dim mdtmStartDate As String
Dim mdtmExpirdat As String
Dim mdtmExpirdat2 As Object
Dim mdtmNulldate As String
    Dim mintNullCod As String
    Dim mdtmNextReceip As Object
    Dim mdtmEffecDate As Date
    Dim mintBranch As Object
    Dim mintProduct As Object
    Dim mstrProductDesc As Object
    Dim mlngPolicy As Object
    Dim mlngCertif As String
    Dim lblnEnabledManualReceipt As Boolean
    Dim mstrColtimere As String

    Dim mclsOpt_system As eGeneral.Opt_system


    '% insPreCA037: Carga los datos en la ventana
    '----------------------------------------------------------------------------
    Private Sub insPreCA037()
        '----------------------------------------------------------------------------
        Call insReaPolData()
	
	
        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"" border= 0>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9227>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write("				")

	
        Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), , , , , "ChangeValues(""Branch"")"))
        'Response.Write mobjvalues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9228>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("  			<TD>" & vbCrLf)
        Response.Write("  				")

	
        Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), eFunctions.Values.eValuesType.clngWindowType, , mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), , , , "ShowChangeValues(""Policy_CA099"");ChangeValues(""Product"");ChangeValuesSOAT();"))
        'Response.Write mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),"",eFunctions.Values.eValuesType.clngWindowType)
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9229>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write("				")

	
        Response.Write(mobjValues.NumericControl("tcnPolicy", 10, mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), , GetLocalResourceObject("tcnPolicyToolTip"), , 0, , , , "LoadPolicy(this)"))
        'Response.Write mobjvalues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbeBranch", 0, "valProduct", 0,,mobjValues.StringToType(mlngPolicy,eFunctions.Values.eTypeData.etdLong),"tcnCertif",,,,,"LoadPolicy(this);",,,False)
        '					Response.Write mobjvalues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbeBranch", 0, "valProduct", 0,,mobjValues.StringToType(mlngPolicy,eFunctions.Values.eTypeData.etdLong),"tcnCertif",,,,,"ChangeValuesMultipleLocation(1);ClearCertif();",,,False)
        'Response.Write mobjvalues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbeBranch", 0, "valProduct", 0,,mobjValues.StringToType(mlngPolicy,eFunctions.Values.eTypeData.etdLong),"tcnCertif",,,,,,,,False)
        'Response.Write mobjvalues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbeBranch", 0, "valProduct", 0,,,"",,,,,"",,,False)
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("            " & vbCrLf)
        Response.Write("            <TD><LABEL ID=9230>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			")

        If mlngCertif <> vbNullString Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.NumericControl("tcnCertif", 10, mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdLong), , GetLocalResourceObject("tcnCertifToolTip"), , 0, , , , "ChangeValuesMultipleLocation(1);", mPoliType))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.NumericControl("tcnCertif", 10, CStr(0), , GetLocalResourceObject("tcnCertifToolTip"), , 0, , , , "ChangeValuesMultipleLocation(1);", mPoliType))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("			" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9231>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdEffecDate", "", , GetLocalResourceObject("tcdEffecDateToolTip"), , , , "insCalExpirdateNew()"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9232>" & GetLocalResourceObject("tcdExpirdateNewCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("				")

	
        Dim lblnDisabled As Boolean
	
        lblnDisabled = CDbl(Request.QueryString.Item("nDisabled")) = 1
	
        lblnDisabled = CBool(lblnDisabled)
	
        Response.Write(mobjValues.DateControl("tcdExpirdateNew", "", , GetLocalResourceObject("tcdExpirdateNewToolTip"), , , , "insTurnOnDevolution()", True))
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			 <TD><LABEL ID=9233>" & GetLocalResourceObject("tcdNextReceipCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("tcdNextReceip", mobjValues.TypeToString(mclsCertificat.dNextReceip, eFunctions.Values.eTypeData.etdDate), , GetLocalResourceObject("tcdNextReceipToolTip"), , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.HiddenControl("optReceiptType", ""))


        Response.Write("</TD>			" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""7"" CLASS=""HighLighted""><LABEL ID=9238><A NAME=""Datos de verificación de la póliza"">" & GetLocalResourceObject("AnchorDatos de verificación de la pólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		")

        mobjValues.ActionQuery = True
        Response.Write("" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""7"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        	<TD>" & vbCrLf)
        Response.Write("				<LABEL ID=9239>" & GetLocalResourceObject("txtOfficeCaption") & "</LABEL>" & vbCrLf)
        Response.Write("        	</TD>" & vbCrLf)
        Response.Write("			")

        If mintOffice > 0 Then
            With Response
			
                mobjValues.TypeList = 2
                mobjValues.List = mintOffice.ToString
                'mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Write("<TD COLSPAN = 3>" & mobjValues.PossiblesValues("txtOffice", "table9", 1, mobjValues.StringToType(mintOffice, eFunctions.Values.eTypeData.etdLong), False, False, , , , , False, , GetLocalResourceObject("txtOfficeToolTip")))
			
                mobjValues.Parameters.Add("nOfficeAgen", mintOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Write("/" & mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngComboType, mintOfficeAgen, True, True, , , , , , , GetLocalResourceObject("cbeOfficeAgenToolTip")) & "</TD>")
			
            End With
        Else
            With Response
                mobjValues.TypeList = 2
                mobjValues.List = "1"
                'mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Write("<TD>" & mobjValues.PossiblesValues("txtOffice", "table9", 1, CStr(0),  False, False, , , , , False) & "</TD>")
            End With
        End If
        Response.Write("" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9240>" & GetLocalResourceObject("lblIssuedateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("lblIssuedate", mdtmIssuedat, , GetLocalResourceObject("lblIssuedateToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD ALIGN = RIGHT><LABEL ID=9241>" & GetLocalResourceObject("txtColtimreCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("txtColtimre", "table25", 1, mobjValues.StringToType(mstrColtimere, eFunctions.Values.eTypeData.etdLong), , False, , , , , , , GetLocalResourceObject("txtColtimreToolTip")))


        Response.Write("</TD>  " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=9242><A NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=9243><A NAME=""Anulación"">" & GetLocalResourceObject("AnchorAnulaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""4"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9244>" & GetLocalResourceObject("tcdStartDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdStartDate", mdtmStartDate, , GetLocalResourceObject("tcdStartDateToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9245>" & GetLocalResourceObject("tcdNulldatCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("tcdNulldat", mdtmNulldate, , GetLocalResourceObject("tcdNulldatToolTip")))


        Response.Write("</TD>   " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9246>" & GetLocalResourceObject("tcdExpirdateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("tcdExpirdate", mdtmExpirdat, , GetLocalResourceObject("tcdExpirdateToolTip")))


        Response.Write("" & vbCrLf)
        Response.Write("				")


        Response.Write(mobjValues.HiddenControl("HddExpirdate", mdtmExpirdat))


        Response.Write("</TD>            " & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("            <TD><LABEL ID=9247>" & GetLocalResourceObject("txtNulldescCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("txtNulldesc", "table13", 1, mobjValues.StringToType(mintNullCod, eFunctions.Values.eTypeData.etdLong), , False, , , , , , , GetLocalResourceObject("txtNulldescToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""7"" CLASS=""HighLighted""><LABEL ID=9248><A NAME=""Figuras presentes en la póliza"">" & GetLocalResourceObject("AnchorFiguras presentes en la pólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""7"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("		")

        Call insReaPolDataGrid()
        Response.Write("" & vbCrLf)
        Response.Write("    </TABLE>")

        Response.Write(mobjValues.BeginPageButton)
    End Sub

    '% insReaInitial: Asigna las variables de sesión que serán utilizadas
    '------------------------------------------------------------------------------------------------------------------
    Private Function insReaInitial() As Boolean
        '------------------------------------------------------------------------------------------------------------------
        Dim mdtProdMaster As Object
	
        lblnEnabledManualReceipt = True
	
        Dim lbytCount As Double
	
        lbytCount = 0
        If Not Request.QueryString.Item("mintBranch") = vbNullString Then
            mintBranch = Request.QueryString.Item("mintBranch")
            lbytCount = lbytCount + 1
            '		Session("nBranch")=mintBranch
        End If
        If Not Request.QueryString.Item("mintProduct") = vbNullString Then
            mintProduct = Request.QueryString.Item("mintProduct")
            lbytCount = lbytCount + 1
            '		Session("nProduct")=mintProduct
        End If
        If Not Request.QueryString.Item("mlngPolicy") = vbNullString Then
            mlngPolicy = Request.QueryString.Item("mlngPolicy")
            lbytCount = lbytCount + 1
            '		Session("nPolicy")=mlngPolicy
        End If
        If Not Request.QueryString.Item("mlngCertif") = vbNullString Then
            mlngCertif = Request.QueryString.Item("mlngCertif")
            lbytCount = lbytCount + 1
            '		Session("nCertif")=mlngCertif
        End If
        'If mobjValues.StringToType(Session("nBranch"),eFunctions.Values.eTypeData.etdLong) > 0 Then
        '    mintBranch = mobjValues.StringToType(Session("nBranch"),eFunctions.Values.eTypeData.etdLong)
        '    lbytCount = lbytCount + 1
        '    If mobjValues.StringToType(Session("nProduct"),eFunctions.Values.eTypeData.etdLong) > 0 Then
        '        mintProduct = mobjValues.StringToType(Session("nProduct"),eFunctions.Values.eTypeData.etdLong)
        '        lbytCount = lbytCount + 1
        '		Set mdtProdMaster = Server.CreateObject("eProduct.Product")
        '        If mdtProdMaster.insValProdMaster(mintBranch, mintProduct) Then
        '			If mdtProdMaster.blnError Then
        '			    mstrProductDesc = mdtProdMaster.sDescript 
        '			    lbytCount = lbytCount + 1
        '			End If
        '		End If
        '		Set mdtProdMaster = Nothing
        '    End If
        'End If
	
        'If mobjValues.StringToType(Session("nPolicy"),eFunctions.Values.eTypeData.etdDouble) > 0 Then
        '    mlngPolicy = mobjValues.StringToType(Session("nPolicy"),eFunctions.Values.eTypeData.etdDouble)
        '    lbytCount = lbytCount + 1
        'End If
        'If mobjValues.StringToType(Session("nCertif"),eFunctions.Values.eTypeData.etdDouble) <> "0" Then
        '    mlngCertif = mobjValues.StringToType(Session("nCertif"),eFunctions.Values.eTypeData.etdDouble)
        '    lbytCount = lbytCount + 1
        'End If
	
        If lbytCount = 5 Then
            insReaInitial = True
        Else
            insReaInitial = False
        End If
	
    End Function

    '%insReaPolData: Muestra en pantalla la información de los Datos de Verificación de la Póliza.
    '%Este procedimiento podrá ser llamado desde otra forma que posea los mismos campos, es decir, que
    '%incluya una consulta de Datos de Verificación de Póliza (como por ejemplo: CA037).
    '--------------------------------------------------------------------------------------------
    Public Function insReaPolData() As Object
        '--------------------------------------------------------------------------------------------
        '+Si se trata de una póliza matriz o una individual
        If mclsPolicy.find("2", mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble)) Then
            Session("sPolitype") = mclsPolicy.sPolitype
            If mclsCertificat.find("2", mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble)) Then
            Else
                mclsPolicy.nOffice = CShort(0)
            End If
        End If
	
        With mclsCertificat
            If mclsPolicy.sPolitype = "1" Then
                mPoliType = True
            Else
                mPoliType = False
            End If
            mintOffice = mclsPolicy.nOffice

            mintOfficeAgen = mclsPolicy.nOfficeAgen
            
            mdtmIssuedat = .dIssuedat
            
            mdtmStartDate = .dStartdate
		
            mdtmExpirdat = .dExpirdat

            mdtmExpirdat2 = .dExpirdat
		
            mdtmNulldate = .dNulldate

            mdtmNextReceip = .dNextReceip
		
            mdtmEffecDate = .dStartdate
            
            mintNullCod = .nNullcode
            
            mstrColtimere = mclsPolicy.sColtimre
        End With
	
    End Function

'--------------------------------------------------------------------------------------------
Public Sub insReaPolDataGrid()
	'--------------------------------------------------------------------------------------------
	Dim lobjGrid As eFunctions.Grid
	Dim lclsRoles As ePolicy.Roles
	Dim lcolRoleses As ePolicy.Roleses
	
	With Server
		lobjGrid = New eFunctions.Grid
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
		lobjGrid.sSessionID = Session.SessionID
		lobjGrid.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjGrid.sCodisplPage = "ca037_k"
		Call lobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
		lclsRoles = New ePolicy.Roles
		lcolRoleses = New ePolicy.Roleses
	End With
	
	With lobjGrid.Columns
		Call .AddTextColumn(3130, GetLocalResourceObject("deClientColumnCaption"), "deClient", 60, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("deClientColumnToolTip"))
		Call .AddPossiblesColumn(3131, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
	End With
	
	lobjGrid.Columns("Sel").GridVisible = False
	lobjGrid.DeleteButton = False
	lobjGrid.AddButton = False
	
	If lcolRoleses.Find_by_Policy("2", mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), vbNullString, Today) Then
		For	Each lclsRoles In lcolRoleses
			With lobjGrid
				.Columns("deClient").DefValue = lclsRoles.sClient & " " & lclsRoles.sCliename
				.Columns("cbeRole").DefValue = CStr(lclsRoles.nRole)
			End With
			Response.Write(lobjGrid.DoRow())
		Next lclsRoles
	End If
	
	Response.Write(lobjGrid.closeTable())
	lobjGrid = Nothing
	lclsRoles = Nothing
	lcolRoleses = Nothing
End Sub


'% insOldValues: Se encarga de asignar el valor de las variables  vbscript, a las
'% variables JavaScript
'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mintBranch <> 0 And mintProduct <> 0 And mlngPolicy <> 0 Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = " & CStr(mintBranch) & ";")
			.Write("var mintProduct = " & CStr(mintProduct) & ";")
			.Write("var mlngPolicy = " & CStr(mlngPolicy) & ";")
			If CStr(mlngCertif) = vbNullString Then
				.Write("var mlngCertif = 0;")
			Else
				.Write("var mlngCertif = " & CStr(mlngCertif) & ";")
			End If
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = 0;")
			.Write("var mintProduct = 0;")
			.Write("var mlngPolicy = 0;")
			.Write("var mlngCertif = 0;")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca037_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "ca037_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjError = New eFunctions.Errors
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjError.sSessionID = Session.SessionID
	mobjError.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mclsPolicy = New ePolicy.Policy
	mclsCertificat = New ePolicy.Certificat
	mobjError = Nothing
End With
mclsOpt_system = New eGeneral.Opt_system

Call mclsOpt_system.find()

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
    var marrCA037 = new Array(0)
    var mintCount = -1
    var mstrFieldName

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 7/06/04 4:42p $|$$Author: Nsoler $"

//% ChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%					  el producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ChangeValues(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){ 
		case "Branch":
			 with (self.document.forms[0]){
				elements["tcnCertif"].disabled = true
				elements["tcnCertif"].value = 0
				elements["tcdEffecDate"].value = ""
				elements["tcdEffecDate"].disabled = true
				elements["tcdNextReceip"].value = ""
				elements["tcdExpirdateNew"].value = ""

				if (elements["tcnPolicy"].value != "")
					insDefValues("ValuesMultiple", "nBranch=" +elements["cbeBranch"].value + "&nProduct=" + elements["valProduct"].value + "&nPolicy=" + elements["tcnPolicy"].value + "&nCertif=" + elements["tcnCertif"].value + "&sIndicator=1" + "&sCodispl=CA037",'/VTimeNet/Policy/PolicyTra');
			 }
			 break;
		case "Product":
			 with (self.document.forms[0]){
				elements["tcdEffecDate"].disabled = false
			 }
			 
	}
}   

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%					  la producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){
		case "Product":
			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=" + sField + "&mintBranch=" + self.document.forms[0].cbeBranch.value + "&mintProduct=" + self.document.forms[0].valProduct.value, "ShowDefValuesProduct", 1, 1,"no","no",2000,2000);
			break; 
	}
}   

//% insCalExpirdateNew: Calcula la nueva fecha hasta y la nueva fecha de próxima facturación en base a la nueva fecha de efecto
//------------------------------------------------------------------------------------------------------------------------------
function insCalExpirdateNew(){
//------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		insDefValues("ExpirdateNew", "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&dEffecDate=" + tcdEffecDate.value + "&dExpirdat=" + tcdExpirdateNew.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertif.value,'/VTimeNet/Policy/PolicyTra');
	}
}
//%ClearCertif: se limpia el valor del campo certificado si cambia el número de póliza
//-------------------------------------------------------------------------------------------
function ClearCertif(){
//-------------------------------------------------------------------------------------------
	if(typeof(self.document.forms[0].tcnCertif)!= 'undefined')
	{
		self.document.forms[0].tcnCertif.value=0;
	}
}

//% ChangeValuesSOAT: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%					  el producto de SOAT
//-------------------------------------------------------------------------------------------
function ChangeValuesSOAT(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		
		insDefValues("ValuesSOAT", "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertif.value + "&sCodispl=CA037",'/VTimeNet/Policy/PolicyTra');
	}
}

//% insTurnOnDevolution: se valida si debe encenderse la variable de sesión de devolución
//-------------------------------------------------------------------------------------------
function insTurnOnDevolution(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		
		insDefValues("TurnOnDevolution", "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nCertif=" + tcnCertif.value + "&nPolicy=" + tcnPolicy.value + "&dExpirdateNew=" + tcdExpirdateNew.value + "&dEffecDate=" + tcdEffecDate.value + "&sCodispl=CA037",'/VTimeNet/Policy/PolicyTra');
	}
}

//% ChangeValuesMultipleLocation: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%								  el producto de SOAT
//-------------------------------------------------------------------------------------------
function ChangeValuesMultipleLocation(sIndicator){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		insDefValues("ValuesMultiple", "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertif.value + "&sIndicator=" + sIndicator + "&sCodispl=CA037",'/VTimeNet/Policy/PolicyTra');
	}
}

function LoadPolicy(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if ((mintBranch != cbeBranch.value) ||
		    (mintProduct != valProduct.value) ||
		    (mlngPolicy != tcnPolicy.value) ||
		    (mlngCertif != tcnCertif.value)){
		if (tcnCertif.value=="")
		    tcnCertif.value="0"
		    self.document.location.href="CA037_K.aspx?sCodispl=CA037&mlngPolicy="+tcnPolicy.value+"&mintBranch="+cbeBranch.value+"&mintProduct="+valProduct.value+"&mlngCertif="+tcnCertif.value + "&sField=" + Field.name
		}
    }
}
</SCRIPT>
<%
'	Response.Write "<NOTSCRIPT>"
'	Response.Write "function insCancel(){"
'	Session("nBranch")= "0"
'	Session("nProduct")= 0
'	Session("nPolicy")= ""
'	Session("nCertif")= 0		
'	Response.Write	" return true; } "
'	Response.Write " </SCRIPT> "
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>


<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA037", "CA037_K.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing%>
</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CA037" ACTION="ValPolicyTra.aspx?&sPolitype=<%=mclsPolicy.sPolitype%>">

    <%Response.Write(mobjValues.ShowWindowsName("CA037", Request.QueryString.Item("sWindowDescript")))
Call insReaInitial()%>
	
<FORM METHOD="post" ID="FORM" NAME="CA037" ACTION="ValPolicyTra.aspx?">
<%Call insPreCA037()
Call insOldValues()

%>  <script>ChangeValuesSOAT();ChangeValuesMultipleLocation();</script><%

mclsCertificat = Nothing
mclsPolicy = Nothing
mobjValues = Nothing
mclsOpt_system = Nothing
%>	
	
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca037_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





