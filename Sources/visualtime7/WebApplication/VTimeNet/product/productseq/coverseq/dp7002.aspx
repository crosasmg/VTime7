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
        mobjGrid.sCodisplPage = "DP7002"
		
        '+ Se definen todas las columnas del Grid
        With mobjGrid.Columns                        
            Call .AddNumericColumn(41461, GetLocalResourceObject("tcnId_SettleColumnCaption"), "tcnId_Settle", 10, "", , GetLocalResourceObject("tcnId_SettleColumnToolTip"), , , , , , )
            Call .AddTextColumn(41462, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, "", , GetLocalResourceObject("tctDescriptColumnToolTip"))                        
        End With
        With mobjGrid
            
            .DeleteButton = False
            .AddButton = False
            .Top = 70
            .Codispl = "DP7002"
            .Width = 500
            .Height = 300
            .ActionQuery = Session("bQuery")
            .Columns("Sel").GridVisible = True
            
         
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
                  
        End With
    End Sub
    '% insPreDP7002: Carga los datos en le grid de la forma "Folder"
    '--------------------------------------------------------------
    Private Sub insPreDP7002()
        '--------------------------------------------------------------
        Dim lclsTab_Settlement As eClaim.Tab_Settlement
        Dim lcolTab_Settlements As eClaim.Tab_Settlements
        
        lclsTab_Settlement = New eClaim.Tab_Settlement
        lcolTab_Settlements = New eClaim.Tab_Settlements
       
        If lcolTab_Settlements.Find_DP7002(CShort(Session("nBranch")), CShort(Session("nProduct")), CShort(Session("nModulec")), CShort(Session("nCover"))) Then
            For Each lclsTab_Settlement In lcolTab_Settlements
                With mobjGrid
                    .Columns("Sel").Checked = CShort(lclsTab_Settlement.sSel)
                    .Columns("Sel").OnClick = "ProdSettlement(this," & lclsTab_Settlement.nId_Settle & ")"
                    .Columns("tcnId_Settle").DefValue = lclsTab_Settlement.nId_Settle
                    .Columns("tctDescript").DefValue = lclsTab_Settlement.sDescript
                    
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
            .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP7002.aspx"))
        End With
        mobjMenu = Nothing
    %>
    
<html>
<head>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script>
        //% Verifica si está seleccionado el registro
        //-------------------------------------------------------------------------------------------
        function ProdSettlement(Field, nId_Settle){
            //-------------------------------------------------------------------------------------------
            
            if (Field.checked){                
					lstrQString = 'nId_Settle=' + nId_Settle + 
					              '&sAction=Add' 
					insDefValues('Settlement',lstrQString,'/VTimeNet/product/productseq/coverseq');
            }else{
					lstrQString = 'nId_Settle=' + nId_Settle + 
					              '&sAction=Delete' 
					insDefValues('Settlement',lstrQString,'/VTimeNet/product/productseq/coverseq');
	            }
            }
        
    </script>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">    
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="DP7002" action="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%        

        Call insDefineHeader()
        Call insPreDP7002()      
        mobjGrid = Nothing
        mobjMenu = Nothing
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
