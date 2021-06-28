<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
   

    '- Objeto para el manejo particular de los datos de la página
    Dim mcolDepreciatedCapital As ePolicy.DepreciatedCapital


    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
	
        mobjGrid = New eFunctions.Grid
        Dim lclsRoles As New ePolicy.Roles
        Dim nExistBenefAcre As New Integer
        mobjGrid.sCodisplPage = "CA054"
        nExistBenefAcre  = 0 
        
        If lclsRoles.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 15, 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
              nExistBenefAcre = 1
        End If
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddDateColumn(1, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate", , , GetLocalResourceObject("tcdStartdateColumnToolTip"), , , , True)
            Call .AddDateColumn(2, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", , , GetLocalResourceObject("tcdExpirdatColumnToolTip"), , , , True)
            Call .AddNumericColumn(3, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 30, vbNullString, , GetLocalResourceObject("tcnCapitalColumnToolTip"), , 6 , , , , IIf( Request.QueryString.Item("Index") = 0 , True , False))
            If nExistBenefAcre = 1 Then
                Call .AddNumericColumn(3, GetLocalResourceObject("tcnEndorsementValueColumnCaption"), "tcnEndorsementValue", 30, vbNullString, , GetLocalResourceObject("tcnEndorsementValueColumnToolTip"),  , 6 )
            Else
                  Call .AddHiddenColumn("tcnEndorsementValue", "0")
            End If
            Call .AddHiddenColumn("tcnInitialCapital", "")
            Call .AddHiddenColumn("hddCModulec", "")
            Call .AddHiddenColumn("hddCGroup_insu", "")
            Call .AddHiddenColumn("hddCCover", "")
      
        End With

	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
		
            .Codispl = "CA054"
            .Codisp = "CA054"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 320
            .Width = 360
            .AddButton = False
            .DeleteButton = False
            .sEditRecordParam = "sCertype=" & Request.QueryString.Item("sCertype") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dEffecdate='+marrArray[mintArrayCount].tcdEddecdate + '" & "&dExpirdat='+marrArray[mintArrayCount].tcdExpirdat + '" & "&nInitialCapital='+marrArray[mintArrayCount].tcnInitialCapital + '" & "&nRow='+marrArray[mintArrayCount].hddRow + '"  
		        
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = False
            .Columns("tcdStartdate").EditRecord = True
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
        End With
    End Sub

    '% insPreCA054: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA054()
        '--------------------------------------------------------------------------------------------
        Dim lclsDepreciatedCapital As Object
        Dim mcolDepreciatedCapital As Object
        Dim nRow As Integer = 0
        Dim lstrCover As String
        Dim lintModulec As Integer
        Dim lintGroup As Integer
        Dim lintCover As Integer
        Dim lclsCertif As ePolicy.Certificat
        lclsCertif = New ePolicy.Certificat
        
        mobjValues.mblnActionQuery = False
        'mobjValues.BlankPosition = false
        
        Call lclsCertif.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
        lintGroup = mobjValues.StringToType(lclsCertif.nGroup , eFunctions.Values.eTypeData.etdDouble)
        mcolDepreciatedCapital = New ePolicy.DepreciatedCapitals
        Response.Write("" & vbCrLf)
        Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=9501>Cobertura</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD COLSPAN=""3"">")

        lstrCover = Request.QueryString("nModulec") & "/" & Request.QueryString("nCover")
        lintModulec = mobjValues.StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble)
        lintCover = mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble)        
		
        mobjValues.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nGroup_insu", lintGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
        

        With Response
          
            .Write(mobjValues.PossiblesValues("cbeCover", "TABCOVER_MODULE", eFunctions.Values.eValuesType.clngComboType, "" & lstrCover, True, , , , , "insParam(this.value)", , , "Cobertura con los capitales depreciados"))
            .Write(mobjValues.HiddenControl("hddModulec", CStr(lintModulec)))
            .Write(mobjValues.HiddenControl("hddcbeCover", lstrCover))
            .Write(mobjValues.HiddenControl("hddCover", CStr(lintCover)))
        End With
		
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("</TABLE> ")
        If mcolDepreciatedCapital.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), lintGroup, lintModulec, lintCover, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
            For Each lclsDepreciatedCapital In mcolDepreciatedCapital
                With mobjGrid
                    .Columns("tcdStartdate").DefValue = lclsDepreciatedCapital.dStartdate
                    .Columns("tcdExpirdat").DefValue = lclsDepreciatedCapital.dExpirdat
                    .Columns("tcdStartdate").EditRecord = True
                    If nRow = 0 Then
                        .Columns("tcnInitialCapital").DefValue = lclsDepreciatedCapital.nCapital
                    End If
                    .Columns("tcnCapital").DefValue = lclsDepreciatedCapital.nCapital
                    .Columns("tcnEndorsementValue").DefValue = lclsDepreciatedCapital.nEndorsementValue
                    .Columns("hddCModulec").DefValue = lclsDepreciatedCapital.nModulec
                    .Columns("hddCGroup_insu").DefValue = lclsDepreciatedCapital.nGroup_insu
                    .Columns("hddCCover").DefValue = lclsDepreciatedCapital.nCover
                    Response.Write(.DoRow)
                    nRow = nRow + 1
                End With
            Next lclsDepreciatedCapital
        End If
	
        Response.Write(mobjGrid.closeTable())
    End Sub

    '% insPreCA054Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA054Upd()
        '--------------------------------------------------------------------------------------------
        Dim lobjDepreciatedCapital As ePolicy.DepreciatedCapital
	
        lobjDepreciatedCapital = New ePolicy.DepreciatedCapital
	
        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA054")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjValues.ActionQuery = Session("bQuery")
%>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script>
        //%insParam: Asigna los valores a los campos ocultos
        //%------------------------------------------------------------------------------------------
        function insParam(Case)
        //%------------------------------------------------------------------------------------------
        {
            var lstrLocation = '';
            var lstrString = '';

            var lstrCampo = self.document.forms[0].cbeCover.value;
            
            var lstrStart = lstrCampo.indexOf("/");
            var lstrModulec = unescape(lstrCampo.substring(0, lstrStart));
            var lstrCover = lstrCampo.substring(lstrStart + 1, lstrCampo.legth);

            if (self.document.forms[0].cbeCover.value == 0) {
                self.document.forms[0].hddModulec.value = -32768;
                self.document.forms[0].hddCover.value = -32768;                
            }
            else {
                
                self.document.forms[0].hddModulec.value = lstrModulec
                self.document.forms[0].hddCover.value = lstrCover
                lstrLocation += document.location.href
                lstrLocation = lstrLocation.replace(/&nModulec.*/, "")
                lstrLocation = lstrLocation + "&nModulec=" + lstrModulec + "&nCover=" + lstrCover
                document.location.href = lstrLocation;
            }
        }        
    </script>
<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />
    <%
        Response.Write("<script>" & " var mstrThousandSep = """ & mobjValues.msUserThousandSeparator & """;" & " var mstrDecimalSep = """ & mobjValues.msUserDecimalSeparator & """</script>")

        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        With Response
            .Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
            .Write(mobjValues.StyleSheet() & vbCrLf)
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
                .Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction;</script>")
            End If
        End With
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <form method="POST" id="FORM" name="CA009" action="valPolicySeq.aspx?Mode=1">
    <%
        Call insDefineHeader()
        If Request.QueryString.Item("Type") <> "PopUp" Then
            Call insPreCA054()
        Else
            Call insPreCA054Upd()
        End If

        mobjGrid = Nothing
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Call mobjNetFrameWork.FinishPage("CA009")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
