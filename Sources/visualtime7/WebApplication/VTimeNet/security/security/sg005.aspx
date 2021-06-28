<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues


    '% InsPreSG005:
    '--------------------------------------------------------------------------------------------
    Private Sub InsPreSG005()
        '--------------------------------------------------------------------------------------------
        Dim lclsSecurity As eSecurity.Windows

        lclsSecurity = New eSecurity.Windows
        Response.Write(mobjValues.ShowWindowsName("SG005"))
        If CStr(Session("sCodispLog")) = "" Then
            Call lclsSecurity.insReaWindowsPseudo("", Session("sPseudo"))
        Else
            Call lclsSecurity.reaWindows(Session("sCodispLog"))
        End If
        If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
            mobjValues.ActionQuery = True
        Else
            mobjValues.ActionQuery = False
        End If

        Response.Write("" & vbCrLf)
        Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("	    <TD><LABEL ID=15000>" & GetLocalResourceObject("tctDescriptCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD COLSPAN=4>")


        Response.Write(mobjValues.TextControl("tctDescript", 60, lclsSecurity.sDescript, False, GetLocalResourceObject("tctDescriptToolTip"),  ,  ,  ,  ,  , 1))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("        <TD><LABEL ID=15007>" & GetLocalResourceObject("tctShort_desCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")


        Response.Write(mobjValues.TextControl("tctShort_des", 12, lclsSecurity.sShort_des, False, GetLocalResourceObject("tctShort_desToolTip"),  ,  ,  ,  ,  , 2))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=15005>" & GetLocalResourceObject("tctPseudoCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        ")


        If lclsSecurity.sPseudo = vbNullString Then
            Response.Write(mobjValues.TextControl("tctPseudo", 12, Session("sPseudo"), False, GetLocalResourceObject("tctPseudoToolTip"),  ,  ,  , "UpperCase()",  , 3))
        Else
            Response.Write(mobjValues.TextControl("tctPseudo", 12, lclsSecurity.sPseudo, False, GetLocalResourceObject("tctPseudoToolTip"),  ,  ,  , "UpperCase()",  , 3))
        End If

        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=14999>" & GetLocalResourceObject("tctCodispCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        ")


        If lclsSecurity.sCodisp = vbNullString Then
            Response.Write(mobjValues.TextControl("tctCodisp", 8, Session("sCodispLog"), False, GetLocalResourceObject("tctCodispToolTip"),  ,  ,  , "UpperCase()",  , 4))
        Else
            Response.Write(mobjValues.TextControl("tctCodisp", 8, lclsSecurity.sCodisp, False, GetLocalResourceObject("tctCodispToolTip"),  ,  ,  , "UpperCase()",  , 4))
        End If

        Response.Write("" & vbCrLf)
        Response.Write("        </TD>        " & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("        <TD><LABEL ID=15004>" & GetLocalResourceObject("cbeModulesCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD COLSPAN=3>")


        Response.Write(mobjValues.PossiblesValues("cbeModules", "Table87", eFunctions.Values.eValuesType.clngComboType, CStr(lclsSecurity.nModules),  ,  ,  ,  ,  ,  ,  ,  , "",  , 5))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>         " & vbCrLf)
        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("        <TD><LABEL ID=15008>" & GetLocalResourceObject("cbeStatregtCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD COLSPAN=3>")


        Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, lclsSecurity.sStatregt,  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 302,  , GetLocalResourceObject("cbeStatregtToolTip"),  , 6))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR><BR></TR>        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("            ")


        If Session("nWindowty") <> 7 And Session("nWindowty") <> 9 And Session("nWindowty") <> 11 And Session("nWindowty") <> 4 Then
            Response.Write(mobjValues.CheckControl("chkDirectGo", GetLocalResourceObject("chkDirectGoCaption"), lclsSecurity.sDirectgo, CStr(1), "DisabledMenu()", False, 7))
        Else
            Response.Write(mobjValues.CheckControl("chkDirectGo", GetLocalResourceObject("chkDirectGoCaption"), lclsSecurity.sDirectgo, CStr(1), "DisabledMenu()", True, 7))
        End If

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""35%"" CLASS=""HighLighted""><LABEL ID=100438><A NAME=""Niveles"">" & GetLocalResourceObject("AnchorNivelesCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><HR></TD>" & vbCrLf)
        Response.Write("        </TR>            " & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""21%""><LABEL ID=15003>" & GetLocalResourceObject("valCodMenCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""41%"">")


        Response.Write(mobjValues.PossiblesValues("valCodMen", "tabWindows_menu", eFunctions.Values.eValuesType.clngWindowType, lclsSecurity.sCodmen,  ,  ,  ,  ,  , "UpperCase()", True, 8, GetLocalResourceObject("valCodMenToolTip"), eFunctions.Values.eTypeCode.eString, 8))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=14998>" & GetLocalResourceObject("tcnAmelevelCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnAmelevel", 2, CStr(lclsSecurity.nAmelevel),  , GetLocalResourceObject("tcnAmelevelToolTip"),  ,  ,  ,  ,  ,  ,  , 9))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>                " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=15006>" & GetLocalResourceObject("tcnSequenceCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnSequence", 4, CStr(lclsSecurity.nSequence),  , GetLocalResourceObject("tcnSequenceToolTip"),  ,  ,  ,  ,  ,  ,  , 10))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=15002>" & GetLocalResourceObject("tcnInqLevelCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnInqLevel", 2, CStr(lclsSecurity.nInqlevel),  , GetLocalResourceObject("tcnInqLevelToolTip"),  ,  ,  ,  ,  ,  ,  , 11))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)







        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=15006>" & GetLocalResourceObject("tcnLength_NotesCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.NumericControl("tcnLength_Notes", 4, CStr(lclsSecurity.nLength_Notes),  , GetLocalResourceObject("tcnLength_NotesToolTip"),  ,  ,  ,  ,  ,  ,  , 10))

        Response.Write("</TD>" & vbCrLf)

        Response.Write("        </TR>" & vbCrLf)


        Response.Write("        </TABLE>" & vbCrLf)
        Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.CheckControl("chkAutorep", GetLocalResourceObject("chkAutorepCaption"), lclsSecurity.sAutorep, CStr(1), "DisabledMenu()", False, 12))


        Response.Write("  </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""80%"">")


        Response.Write(mobjValues.CheckControl("chkUserMen", GetLocalResourceObject("chkUserMenCaption"), CStr(0), CStr(1),  , Session("nWindowty") <> 8, 12))


        Response.Write("</TD>                " & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("            ")


        mobjValues.Height = 40
        mobjValues.Width = 40
        If lclsSecurity.nImg_index = 0 Then
            Response.Write(mobjValues.AnimatedButtonControl("btnImg_index", "/VTimeNet/images/blank.gif"))
        Else
            Response.Write(mobjValues.AnimatedButtonControl("btnImg_index", lclsSecurity.PathImages(lclsSecurity.nImg_index)))
        End If
        mobjValues.Height = 0
        mobjValues.Width = 0

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD ALIGN=CENTER><LABEL ID=15001>" & GetLocalResourceObject("btnVp_ImageCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("            ")


        If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
            Response.Write(mobjValues.AnimatedButtonControl("btnVp_Image", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnVp_ImageToolTip"),  , "ShowImages()", True))
        Else
            If Session("nWindowty") <> 7 And Session("nWindowty") <> 9 And Session("nWindowty") <> 11 And Session("nWindowty") <> 4 And Session("nWindowty") <> 8 Then
                Response.Write(mobjValues.AnimatedButtonControl("btnVp_Image", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnVp_ImageToolTip"),  , "ShowImages()", False))
            Else
                Response.Write(mobjValues.AnimatedButtonControl("btnVp_Image", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnVp_ImageToolTip"),  , "ShowImages()", True))
            End If
        End If

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>            " & vbCrLf)
        Response.Write("</TABLE>")




        '--------------------------------------------------------------------------------------------------
        'Realizado Por: Gherson Isaac Mendoza Nery
        'Fecha: 15-11-2019
        'Razon: Error 127652: CHI VT SG005_K Transacciones del sistema errores varios
        '--------------------------------------------------------------------------------------------------
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR><BR></TR>        " & vbCrLf)

        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("            ")
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""35%"" CLASS=""HighLighted""><LABEL ID=15019><A NAME=""Niveles"">" & GetLocalResourceObject("AnchorNivelesCaption2") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)

        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><HR></TD>" & vbCrLf)
        Response.Write("        </TR>            " & vbCrLf)

        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("	    <TD WIDTH=""10%""><LABEL ID=15020>" & GetLocalResourceObject("tcsHelpPathCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.TextControl("tcsHelpPath", 60, lclsSecurity.sHelpPath, False, GetLocalResourceObject("tcsHelpPathToolTip"),  ,  ,  ,  ,  , 1))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)

        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("	    <TD><LABEL ID=15021>" & GetLocalResourceObject("tcnHeightCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.NumericControl("tcnHeight", 2, CStr(lclsSecurity.nHeight),  , GetLocalResourceObject("tcnHeightToolTip"),  ,  ,  ,  ,  ,  ,  , 11))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)

        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("	    <TD><LABEL ID=15022>" & GetLocalResourceObject("tcnType_ReportCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.PossiblesValues("cbetypereport", "Table98", eFunctions.Values.eValuesType.clngComboType, CStr(lclsSecurity.ntype_report),  ,  ,  ,  ,  ,  ,  ,  , "",  , 5))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)

        Response.Write("    <TR>            " & vbCrLf)
        Response.Write("	    <TD><LABEL ID=15023>" & GetLocalResourceObject("tcsFilePathCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>")
        Response.Write(mobjValues.TextControl("tcsFilePath", 60, lclsSecurity.sfilepath, False, GetLocalResourceObject("tcsFilePathToolTip"),  ,  ,  ,  ,  , 1))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)

        Response.Write("    </TABLE>" & vbCrLf)
        '--------------------------------------------------------------------------------------------------

        Response.Write(mobjValues.HiddenControl("nImage_index", CStr(lclsSecurity.nImg_index)))
        lclsSecurity = Nothing
        mobjValues = Nothing
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG005"
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 7/01/04 18:35 $|$$Author: Nvaplat11 $"

//% DisabledMenu: Permite habilitar e inhabilitar los campos "Menú que lo invoca" y
//% "Orden de aparición".
//------------------------------------------------------------------------------------------
function DisabledMenu(){
//------------------------------------------------------------------------------------------
//+ 4- Secuencia sin Encabezado
//+ 7 - Carpeta Específica
//+ 9 - Carpeta Masiva
//+ 11- Ventana Emergente
//+ 8 - Menu

	with (self.document.forms[0])
	{
		if ((top.fraHeader.document.forms[0].cbeWindowty.value!=11) &&
		    (top.fraHeader.document.forms[0].cbeWindowty.value!=4)  &&
		    (top.fraHeader.document.forms[0].cbeWindowty.value!=7)  &&
		    (top.fraHeader.document.forms[0].cbeWindowty.value!=8)  &&
		    (top.fraHeader.document.forms[0].cbeWindowty.value!=9))
		{
			if(top.frames['fraSequence'].plngMainAction!=401 && 
				top.frames['fraSequence'].plngMainAction!=303)
			{
				valCodMen.disabled = !chkDirectGo.checked;
				btnvalCodMen.disabled = valCodMen.disabled;
			}
			if(typeof(valCodMen)!='undefined')
				if(valCodMen.disabled)
			 		valCodMen.value = "";
	   }
		else
		{
			chkDirectGo.disabled = true;
			chkDirectGo.checked  = false;
			btnVp_Image.disabled = true;
			if(top.fraSequence.plngMainAction!=401)
				tcnSequence.disabled = true;
		}
		if(top.fraSequence.plngMainAction!=401){
			if(!chkDirectGo.disabled && chkDirectGo.checked)
				tcnSequence.disabled = false
			else
				tcnSequence.disabled = true;
		}		
	}
}

//% ShowImages: Este evento permite realizar el llamado a la ventana PopUp que muestrará las 
//% imagenes a asociar a la transacción.
//-------------------------------------------------------------------------------------------
function ShowImages(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("SG098_k.aspx","SG098",500,115,"no","no",100,100);
}

//% UpperCase: Permite colocar en mayúscula los campos Descripción corta,
//% Pseudónimo y Físico.
//--------------------------------------------------------------------------------------------
function UpperCase(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		tctPseudo.value = tctPseudo.value.toUpperCase();
		tctCodisp.value = tctCodisp.value.toUpperCase();
		valCodMen.value = valCodMen.value.toUpperCase();
		top.fraHeader.document.forms[0].tctPseudo.value = tctPseudo.value;
	}
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.setZone(2, "SG005", "SG005.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SG005" ACTION="valSecuritySeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call InsPreSG005()
%>
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>DisabledMenu()</SCRIPT>")
%>





