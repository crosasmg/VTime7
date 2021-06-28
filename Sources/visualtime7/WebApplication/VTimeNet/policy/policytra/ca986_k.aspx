<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>

<script language="VB" runat="Server">
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
</script>

<%
    mobjValues = New eFunctions.Values
    Response.Expires = -1
%>

<html>
    <head>
        <script type="text/javascript">		
            //- Variable para el control de versiones
            document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
            //%insCancel:
            //------------------------------------------------------------------------------------------
            function insCancel(){
	            return true;
            }
            //%insStateZone:
            //------------------------------------------------------------------------------------------
            function insStateZone(){
            //------------------------------------------------------------------------------------------
            }
        </script>
        
        <meta name = "GENERATOR" content = "Microsoft Visual Studio 6.0"/>
        
        <%=mobjValues.StyleSheet()%>

        <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
        <script type="text/javascript" src="/VTimeNet/Scripts/tmenu.js"></script>

        <%mobjMenu = New eFunctions.Menues
            With Response
                .Write(mobjMenu.MakeMenu("CA986", "CA986_K.aspx", 1, ""))
            End With
            mobjMenu = Nothing%>
    </head>

    <body onunload="closeWindows();">
        
        <form method="POST" id="FORM" name="frmReahPolicy_K" action="ValPolicyTra.aspx?Zone=1">
            <br />
            <table width="100%">
                <tr>
                    <td width="3%">
                        <label id=Label1>
                            <%=GetLocalResourceObject("cbeTypeVehCaption")%>
                        </label>
                    </td>
                    <td width="17%">
                        <%= mobjValues.PossiblesValues("cbeTypeVeh", "table78109", eFunctions.Values.eValuesType.clngComboType, "", , , , , , , , , GetLocalResourceObject("cbeTypeVehToolTip"))%>
                    </td>
                </tr>
            </table>
        </form>
    </body>
</html>
<%mobjValues = Nothing%>
