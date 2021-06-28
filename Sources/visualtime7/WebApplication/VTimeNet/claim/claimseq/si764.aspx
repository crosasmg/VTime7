<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eProduct" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Objeto para el manejo del Grid de la ventana
    Dim mobjGrid As eFunctions.Grid
    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues


    '% insDefineHeader: Define las columnas del Grid
    '-----------------------------------------------
    Private Sub insDefineHeader()
        '-----------------------------------------------
        mobjGrid = New eFunctions.Grid
        mobjGrid.sCodisplPage = "SI764"
		
        '+ Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddHiddenColumn("hddCover", "")
            Call .AddNumericColumn(40789, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", 5, CStr(0), True, GetLocalResourceObject("tcnModulecColumnToolTip"), True, 0, , , , True)
            Call .AddTextColumn(40785, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", 5, vbNullString, , GetLocalResourceObject("tctCoverColumnToolTip"), , , , True)
            Call .AddNumericColumn(41461, GetLocalResourceObject("tcnId_SettleColumnCaption"), "tcnId_Settle", 10, "", , GetLocalResourceObject("tcnId_SettleColumnToolTip"), , , , , , )
            Call .AddTextColumn(41462, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, "", , GetLocalResourceObject("tctDescriptColumnToolTip"))
             
        End With
        With mobjGrid
            
            .DeleteButton = False
            .AddButton = False
            .Top = 70
            .Codispl = "SI764"
            .Width = 500
            .Height = 300
            .ActionQuery = Session("bQuery")
            .Columns("Sel").GridVisible = True
            
         
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
                  
        End With
    End Sub
    '% insPreSI764: Carga los datos en le grid de la forma "Folder"
    '--------------------------------------------------------------
    Private Sub insPreSI764()
        '--------------------------------------------------------------
        Dim lclsTab_Settlement As eClaim.Cl_Settlement
        Dim lcolTab_Settlements As eClaim.Cl_Settlements
        Dim lstrDefValueCase As String
        Dim lintCase_num As Integer
        Dim lintDeman_type As Integer
        Dim lstrClient As Object
        Dim lintCount As Short
        Dim nNumber As Integer
        nNumber = 1
        
        Response.Write("" & vbCrLf)
        Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=9501>Caso</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD COLSPAN=""3"">")

	
	
        If CStr(Session("nClaim")) = vbNullString Then
            mobjValues.Parameters.Add("nClaim", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        Else
            lstrDefValueCase = Request.QueryString("nCase_num") & "/" & Request.QueryString("nDeman_type") & "/" & Request.QueryString("sClient")
            lintCase_num = mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble)
            lintDeman_type = mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)
            lstrClient = Request.QueryString("sClient")
		
            mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
        End If
        With Response
            .Write(mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "" & lstrDefValueCase, True, , , , , "insParam(this.value)", CStr(Session("nClaim")) = vbNullString, , "Caso asociado al beneficiario, del cual sale el finiquito"))
            .Write(mobjValues.HiddenControl("hddCaseNum", CStr(lintCase_num)))
            .Write(mobjValues.HiddenControl("hddcbeCase", lstrDefValueCase))
            .Write(mobjValues.HiddenControl("hddDeman_Type", CStr(lintDeman_type)))
            .Write(mobjValues.HiddenControl("hddClient", lstrClient))
        End With
	
	
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("</TABLE> ")
        lintCount = 0
        lclsTab_Settlement = New eClaim.Cl_Settlement
        lcolTab_Settlements = New eClaim.Cl_Settlements
       
        If lcolTab_Settlements.Find_SI764(CDbl(Session("nClaim")), lintDeman_type, lintCase_num, CInt(Session("nUsercode"))) Then
            For Each lclsTab_Settlement In lcolTab_Settlements
                With mobjGrid
                    .Columns("Sel").Checked = CShort(lclsTab_Settlement.sSel)
                    .Columns("Sel").OnClick = "Cl_Settlement(this," & lclsTab_Settlement.nId_Settle & ", " & lclsTab_Settlement.nModulec & " ," & lclsTab_Settlement.nCover & ", " & lintDeman_type & " ," & lintCase_num & ")"
                    .Columns("tcnId_Settle").DefValue = lclsTab_Settlement.nId_Settle
                    .Columns("tctDescript").DefValue = lclsTab_Settlement.sDescript
                    .Columns("tcnModulec").DefValue = lclsTab_Settlement.nModulec
                    .Columns("tctCover").DefValue = lclsTab_Settlement.sCover
                    .Columns("hddCover").DefValue = lclsTab_Settlement.nCover
                    Response.Write(.DoRow)
                End With
            Next lclsTab_Settlement
        End If
        '+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
        Response.Write(mobjGrid.closeTable())
        
        lcolTab_Settlements = Nothing
        lclsTab_Settlement = Nothing
    End Sub
    

</script>    
    
    <% mobjMenu = New eFunctions.Menues
        mobjValues = New eFunctions.Values
        With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
            .Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
            .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "SI764.aspx"))
        End With
        mobjMenu = Nothing
    %>
    
<html>
<head>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script>
        //% Verifica si está seleccionado el registro
        //-------------------------------------------------------------------------------------------
        function Cl_Settlement(Field, nId_Settle, nModulec, nCover, nDeman_type, nCase_num) {
            //-------------------------------------------------------------------------------------------
            
            if (Field.checked){                
					lstrQString = 'nId_Settle=' + nId_Settle +
					              '&nModulec=' + nModulec +
					              '&nCover=' + nCover +
                                  '&nDeman_type=' + nDeman_type +
					              '&nCase_num=' + nCase_num + 
                                  '&sAction=Add'
					insDefValues('settlement', lstrQString, '/VTimeNet/claim/claimseq');
            }else{
	                lstrQString = 'nId_Settle=' + nId_Settle +
					              '&nModulec=' + nModulec +
					              '&nCover=' + nCover +
                                  '&nDeman_type=' + nDeman_type +
					              '&nCase_num=' + nCase_num + 
					              '&sAction=Delete'
	                insDefValues('settlement', lstrQString, '/VTimeNet/claim/claimseq');
	            }
            }
            //%insParam: Asigna los valores a los campos ocultos
            //%------------------------------------------------------------------------------------------
            function insParam(Case)
            //%------------------------------------------------------------------------------------------
            {
                var lstrLocation = '';
                var lstrString = '';
                var lstrClient = '';
                var lstrCampo = self.document.forms[0].cbeCase.value;
                var lstrStart = lstrCampo.indexOf("/");
                var lstrCase_num = unescape(lstrCampo.substring(0, lstrStart));
                var lstrCampo1 = lstrCampo.substring(lstrStart + 1, lstrCampo.legth);
                var lstrStart1 = lstrCampo1.indexOf("/");
                var lstrDemanType = unescape(lstrCampo1.substring(0, lstrStart1));

                if (self.document.forms[0].cbeCase.value == 0) {
                    self.document.forms[0].hddCaseNum.value = -32768;
                    self.document.forms[0].hddDeman_Type.value = -32768;
                    self.document.forms[0].hddClient.value = '';
                }
                else {
                    lstrString += Case
                    lstrClient += lstrString.replace(/.*\//, "")
                    self.document.forms[0].hddCaseNum.value = lstrCase_num
                    self.document.forms[0].hddDeman_Type.value = lstrDemanType
                    self.document.forms[0].hddClient.value = lstrClient

                    lstrLocation += document.location.href
                    lstrLocation = lstrLocation.replace(/&nCase_num.*/, "")
                    lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDemanType + "&sClient=" + lstrClient
                    document.location.href = lstrLocation;
                }
            }        
    </script>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">    
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="SI764" action="valclaimseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%        

        Call insDefineHeader()
        Call insPreSI764()      
        mobjGrid = Nothing
        mobjMenu = Nothing
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
