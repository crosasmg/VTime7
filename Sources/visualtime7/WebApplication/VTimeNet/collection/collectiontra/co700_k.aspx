<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.47
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo particular de los datos de la página
    Dim mcolClass As Object

    '- Variables de trabajo para almacenar los códigos de los documentos a tratar
    Dim mintBranch As Integer
    Dim mintProduct As Integer
    Dim mlngPolicy As Integer
    Dim mlngBill As Integer
    Dim mintInsur_area As Integer

    Dim mintBillType As Integer

    '- Objeto para el almacenar el string de la propiedad QueryString
    Dim mstrString As Object

    Dim lintPos As Integer


    Sub LoadHeader()

        Response.Write("" & vbCrLf)
        Response.Write("	<BR><BR>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"" border=""0"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("		 <!--   <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>-->" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		   <!-- <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>-->" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("					" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sDocType") = "1" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD COLSPAN=""3"">")


            Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_1Caption"), CStr(1), "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD COLSPAN=""3"">")


            Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_1Caption"), , "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sBillType") = "1" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_1Caption"), CStr(1), "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_1Caption"),  , "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sProcess") = "1" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"),  , "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sModeT") = "1" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_1Caption"), CStr(1), "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_1Caption"),  , "1",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("<!--		    <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("	-->		" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sDocType") = "3" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD COLSPAN=""3"">")


            Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_3Caption"), CStr(1), "3",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD COLSPAN=""3"">")


            Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_3Caption"), , "3",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sBillType") = "2" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_2Caption"), CStr(1), "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_2Caption"),  , "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sProcess") = "2" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"), CStr(1), "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sModeT") = "2" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_2Caption"), CStr(1), "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_2Caption"),  , "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        'Response.Write("			<TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sDocType") = "2" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD COLSPAN=""3"">")


            Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_2Caption"), CStr(1), "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD COLSPAN=""3"">")


            Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_2Caption"), , "2",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("		    <TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			" & vbCrLf)
        Response.Write("			")

        If Request.QueryString.Item("sBillType") = "3" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_3Caption"), CStr(1), "3",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_3Caption"),  , "3",  , True))


            Response.Write(" </TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("		    " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("    " & vbCrLf)
        Response.Write("    <TABLE  WIDTH=""100%"" border=""0"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""8"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>        " & vbCrLf)
        Response.Write("            <TD COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>        " & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDateIniCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdDateIni", Request.QueryString.Item("dDateIni"),  , GetLocalResourceObject("tcdDateIniToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDateEndCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdDateEnd", Request.QueryString.Item("dDateEnd"),  , GetLocalResourceObject("tcdDateEndToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdValDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdValDate", Request.QueryString.Item("dValDate"),  , GetLocalResourceObject("tcdValDateToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDatePrintCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdDatePrint", Request.QueryString.Item("dDatePrint"),  , GetLocalResourceObject("tcdDatePrintToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("    <BR>		" & vbCrLf)
        Response.Write("	" & vbCrLf)
        Response.Write("	")


        Response.Write(mobjValues.HiddenControl("tctKey", Request.QueryString.Item("sKey")))


        Response.Write("" & vbCrLf)
        Response.Write("	")

        mstrString = Request.Params.Get("Query_String")

        lintPos = InStr(mstrString, "&sCodispl")
        If lintPos > 0 Then
            mstrString = Mid(mstrString, 1, lintPos - 1)
        End If

        With Response
            .Write("<SCRIPT>")
            .Write("top.fraSequence.plngMainAction=" & Request.QueryString.Item("nMainAction") & ";top.fraFolder.document.location =""CO700A.aspx?" & mstrString & """;")
            .Write("</" & "Script>")
        End With
        Response.Write("	")

    End Sub

    Sub LoadFolder()

        Response.Write("" & vbCrLf)
        Response.Write("	<BR><BR><BR>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"" border=""0"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor8Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("					" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"">")


        Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_1Caption"), CStr(1), "1", "insChangeDocType()", True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_1Caption"), CStr(1), "1", "insChangeBillsType()", True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1", "insChangeProcess()", True))


        Response.Write(" </TD>		    " & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_1Caption"), CStr(1), "1",  , True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"">")


        Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_3Caption"),  , "3", "insChangeDocType()", True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_2Caption"),  , "2", "insChangeBillsType()", True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2", "insChangeProcess()", True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(0, "optMode", GetLocalResourceObject("optMode_2Caption"),  , "2",  , True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""3"">")

        Response.Write(mobjValues.OptionControl(0, "optDocType", GetLocalResourceObject("optDocType_2Caption"),  , "2", "insChangeDocType()", True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</td>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.OptionControl(0, "optBillType", GetLocalResourceObject("optBillType_3Caption"),  , "3", "insChangeBillsType()", True))


        Response.Write(" </TD>                " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("    <TABLE  WIDTH=""100%"" border=""0"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""8"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>        " & vbCrLf)
        Response.Write("            <TD COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>        " & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDateIniCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdDateIni",  ,  , GetLocalResourceObject("tcdDateIniToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDateEndCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdDateEnd",  ,  , GetLocalResourceObject("tcdDateEndToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdValDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdValDate",  ,  , GetLocalResourceObject("tcdValDateToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDatePrintCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.DateControl("tcdDatePrint", CStr(Today),  , GetLocalResourceObject("tcdDatePrintToolTip"),  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("    <BR>	" & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"" border=""0"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor12Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""6"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnBillCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnBill", 15, vbNullString,  , GetLocalResourceObject("tcnBillToolTip"),  ,  ,  ,  ,  , "ShowBills()", True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		  	    " & vbCrLf)
        Response.Write("			<TD COLSPAN=""3""><LABEL>" & GetLocalResourceObject("tcnLastBillCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnLastBill", 15, vbNullString,  , GetLocalResourceObject("tcnLastBillToolTip"),  ,  ,  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=9689>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""1"">")


        Response.Write(mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""4"">")


        Response.Write(mobjValues.CheckControl("chkBill_Ind", GetLocalResourceObject("chkBill_IndCaption"),  , "1",  , True, 13, GetLocalResourceObject("chkBill_IndToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD> <LABEL ID=41200>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL> </TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=40010>" & GetLocalResourceObject("valProductCaption") & "</LABEL> </TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=13381>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnPolicy", 8, Request.Form.Item("tcnPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"), False,  ,  ,  ,  , "ShowClient()", False))


        Response.Write("</td>" & vbCrLf)
        Response.Write("               " & vbCrLf)
        Response.Write("                " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=13372>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("  		    <TD>")


        Response.Write(mobjValues.PossiblesValues("cbeAgency", "table5555", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAgencyToolTip")))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("	   </TR>" & vbCrLf)
        Response.Write("    </TABLE>")


    End Sub

    '% insReaInitial: Se encarga de inicializar las variables de trabajo
    '---------------------------------------------------------------------------------------------
    Private Sub insReaInitial()
        '---------------------------------------------------------------------------------------------
        mintBranch = eRemoteDB.Constants.intNull
        mintProduct = eRemoteDB.Constants.intNull
        mlngPolicy = eRemoteDB.Constants.intNull
        mlngBill = eRemoteDB.Constants.intNull
        mintBillType = eRemoteDB.Constants.intNull
    End Sub

    '% insOldValues: Se encarga de asignar los valores obtenidos en vbscript a javascript
    '---------------------------------------------------------------------------------------------
    Private Sub insOldValues()
        '---------------------------------------------------------------------------------------------
        If mintBranch <> eRemoteDB.Constants.intNull And mintProduct <> eRemoteDB.Constants.intNull And mlngPolicy <> eRemoteDB.Constants.intNull And mlngBill <> eRemoteDB.Constants.intNull And mintBillType <> eRemoteDB.Constants.intNull And mintInsur_area <> eRemoteDB.Constants.intNull Then
            With Response
                .Write("<SCRIPT>")
                .Write("var mintBranch = " & CStr(mintBranch) & ";")
                .Write("var mintProduct = " & CStr(mintProduct) & ";")
                .Write("var mlngPolicy = " & CStr(mlngPolicy) & ";")
                .Write("var mlngBill = " & CStr(mlngBill) & ";")
                .Write("var mintInsur_area = " & CStr(mintInsur_area) & ";")
                .Write("var mintBillType = " & CStr(mintBillType) & ";")
                .Write("</" & "Script>")
            End With
        Else
            With Response
                .Write("<SCRIPT>")
                .Write("var mintBranch = 0;")
                .Write("var mintProduct = 0;")
                .Write("var mlngPolicy = 0;")
                .Write("var mlngBill = 0;")
                .Write("var mintInsur_area = 0;")
                .Write("var mintBillType = 0;")
                .Write("</" & "Script>")
            End With
        End If
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co700_k")

mintInsur_area = 2

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co700_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		



<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 2/03/04 10:33 $|$$Author: Nvaplat40 $"
	
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(nAction){
//--------------------------------------------------------------------------------------------
	var lblnDisabled = (nAction==401?true:false)
	with (self.document.forms[0]){
		chkBill_Ind.disabled=false;
		optDocType[0].disabled = false;
		optDocType[1].disabled = false;
		optDocType[2].disabled = false;
		optBillType[0].disabled = false;
		optBillType[1].disabled = false;
		//optBillType[2].disabled = false;
		optProcess[0].disabled = lblnDisabled;
		optProcess[0].checked = true;
		optProcess[1].disabled = lblnDisabled;
		optMode[0].disabled = true;
		optMode[1].disabled = true;
		optMode[1].checked = true;
		tcdDateIni.disabled = lblnDisabled;
		btn_tcdDateIni.disabled = lblnDisabled;
		tcdDateEnd.disabled = lblnDisabled;
		btn_tcdDateEnd.disabled = lblnDisabled;
		tcdDatePrint.disabled = lblnDisabled;
		btn_tcdDatePrint.disabled = lblnDisabled;
		tcnBill.disabled = !lblnDisabled;
		dtcClient.disabled = lblnDisabled;
		cbeBranch.disabled = lblnDisabled;
		valProduct.disabled = lblnDisabled;
		tcnPolicy.disabled = lblnDisabled;
		tcdValDate.disabled = false;
		btn_tcdValDate.disabled = false;
		cbeAgency.disabled = true;
	}
	ShowLastBill();
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	with (document.forms[0]){
		if (typeof(tctKey)!='undefined') {
			insDefValues("ShowDataCO700", "sField=" + "DelTmp_CO700" + "&sKey=" + tctKey.value);
		}
	}
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insChangeDocType: Actualiza los objetos de la forma, según el tipo del documento
//-------------------------------------------------------------------------------------------
function insChangeDocType() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
//+ Si el tipo de documento corresponde a factura
	    if (optDocType[0].checked || optDocType[1].checked) {
			optBillType[0].disabled = false;
			optBillType[1].disabled = false;
			//optBillType[2].disabled = false;
			optBillType[0].checked = true;
			optMode[0].disabled = false;
			optMode[1].disabled = false;
			dtcClient.value = '';
			$(dtcClient).change();
		    tcnBill.value = '';
			tcnBill.disabled = true;
			optProcess[0].disabled = false;
			optProcess[1].disabled = false;
			if (optDocType[1].checked){
				optProcess[0].checked = true;
			}
			if (top.frames["fraSequence"].plngMainAction!=401) {
				optProcess[0].disabled = false;
				optProcess[1].disabled = false;
				dtcClient.disabled = false;
				btndtcClient.disabled = false;
				cbeBranch.disabled = false;
				tcnPolicy.disabled = false;
				tcnBill.disabled = true;
				tcdDateIni.disabled = false;
				btn_tcdDateIni.disabled = false;
				tcdDateEnd.disabled = false;
				btn_tcdDateEnd.disabled = false;
				if (optDocType[0].checked) {
					tcdDatePrint.disabled = false;
					btn_tcdDatePrint.disabled = false;
				}
				else {
					tcdDatePrint.disabled = true;
					btn_tcdDatePrint.disabled = true;
				}
			} else {
				tcnBill.disabled = true;
				optProcess[0].disabled = true;
				optProcess[1].disabled = true;
			}
		}
		else {
//+ Si el tipo de documento corresponde a nota de crédito
		    //if (optDocType[1].checked) {
		    if (optDocType[2].checked){
				optBillType[0].checked = false;
				optBillType[1].checked = false;
				optBillType[2].checked = false;
				optBillType[0].disabled = true;
				optBillType[1].disabled = true;
				//optBillType[2].disabled = true;
				optProcess[0].disabled = true;
				optProcess[0].checked = true;
				optProcess[1].disabled = true;
				optMode[0].checked = true;
				optMode[0].disabled = true;
				optMode[1].disabled = true;
				tcnBill.disabled = false;
				tcdDatePrint.disabled = true;
				btn_tcdDatePrint.disabled = true;
				
				tcdDateIni.disabled = false;
				btn_tcdDateIni.disabled = false;
				tcdDateEnd.disabled = false;
				btn_tcdDateEnd.disabled = false;
				
				chkBill_Ind.disable=false;
							
				dtcClient.disabled = true;
				dtcClient.value = '';
				$(dtcClient).change();
				btndtcClient.disabled = true;
				cbeBranch.disabled = true;
				cbeBranch.value = '';
				valProduct.disabled = true;
				valProduct.value = '';
				UpdateDiv("valProductDesc",'');
				btnvalProduct.disabled = true;
				tcnPolicy.disabled = true;
				tcnPolicy.value = '';
			}
		}
//+ Si el proceso es puntual
		if (optProcess[0].checked){
			optMode[1].checked = true;
			optMode[0].disabled = true;
			optMode[1].disabled = true;
			cbeAgency.disabled = true;
		}
		else {
			optMode[0].disabled = false;
			optMode[1].disabled = false;
			cbeAgency.disabled = false;		
		}
		ShowLastBill();
	}
}

//% insChangeBillsType: Habilita o deshabilita los campos según el tipo de factura a tratar
//--------------------------------------------------------------------------------------------
function insChangeBillsType() {
//--------------------------------------------------------------------------------------------	
	var ddate = '<%=Today%>'
	
	with (self.document.forms[0]){
//+ Si la acción no es consulta
		if (top.frames["fraSequence"].plngMainAction!=401) {	
//+ Si el tipo de documento corresponde a factura afecta o exenta.
			if (optBillType[0].checked || optBillType[1].checked){
				tcdDatePrint.disabled = false;
				btn_tcdDatePrint.disabled = false;
//				tcdValDate.disabled = true;
//				btn_tcdValDate.disabled = true;
//				tcdValDate.value='';
			}
			else {
//			    tcdValDate.value=ddate;
				tcdDatePrint.disabled = true;
				btn_tcdDatePrint.disabled = true;
//				tcdValDate.disabled = false;
//				btn_tcdValDate.disabled = false;
			}
		}	
		else{
//				tcdValDate.disabled = true;
//				btn_tcdValDate.disabled = true;
//				tcdValDate.value='';
		}
	}
	ShowLastBill();
}

// insChangeProcess: Habilita o deshabilita los campos según el tipo de proceso a efectuar.
//-------------------------------------------------------------------------------------------
function insChangeProcess() {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
//+ Si el tipo de proceso corresponde a puntual.
		if (optProcess[0].checked){
			cbeAgency.disabled = true;
			optMode[0].disabled = true;
			optMode[1].disabled = true;
			optMode[1].checked = true;
			
			dtcClient.disabled = false;
			btndtcClient.disabled = false;
			cbeBranch.disabled = false;
			tcnPolicy.disabled = false;
		}
		else {
			cbeAgency.disabled = false;
			optMode[0].checked = true;
			optMode[0].disabled = false;
			optMode[1].disabled = false;
			
			dtcClient.disabled = true;
			dtcClient.value = '';
			$(dtcClient).change();
			btndtcClient.disabled = true;
			cbeBranch.disabled = true;
			cbeBranch.value = '';
			valProduct.disabled = true;
			valProduct.value = '';
			btnvalProduct.disabled = true;
			UpdateDiv("valProductDesc",'');
			tcnPolicy.disabled = true;
			tcnPolicy.value = '';
		}
	}
}

//%	ShowClient: Obtiene el contratante de la póliza
//-------------------------------------------------------------------------------------------
function ShowClient(){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
		if ((cbeBranch.value!=mintBranch) ||
			(valProduct.value!=mintProduct) ||
			(tcnPolicy.value!=mlngPolicy)) {

			mintBranch = cbeBranch.value
			mintProduct = valProduct.value
			mlngPolicy = tcnPolicy.value

         	insDefValues("ShowDataCO700", "sField=" + "FindClient" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value);
		}
	}
}

//%	OnChangeInsur_area: Obtiene el último número procesado de las facturas según el tipo y área de seguros
//--------------------------------------------------------------------------------------------
function OnChangeInsur_area(){
//--------------------------------------------------------------------------------------------
	ShowLastBill();
}

//%	ShowLastBill: Obtiene el último número procesado de las facturas según el tipo y área de seguros
//--------------------------------------------------------------------------------------------
function ShowLastBill(){
//--------------------------------------------------------------------------------------------
	var lintBillType
    var lintInsur_area = '<%=Session("nInsur_area")%>'
			
    with (document.forms[0]){
        //if (optDocType[1].checked == true)
        if (optDocType[2].checked == true)
			lintBillType = 3;	//+ Nota de crédito
		else
			if (optBillType[0].checked==true)
				lintBillType = 1;	//+ Factura afecta
			else
				if (optBillType[1].checked==true)
					lintBillType = 2;	//+ Factura exenta
				else
					if (optBillType[2].checked==true)
						lintBillType = 4;	//+ Proforma
		if (lintBillType!=mintBillType) {
				mintBillType = lintBillType
        		insDefValues("ShowDataCO700", "sField=FindLastBill" + "&nInsur_area=" + lintInsur_area + "&sBillType=" + lintBillType);
		}
	}
}

</SCRIPT>

<%="<SCRIPT>"%>
//%	ShowBills: Verifica la existencia de una factura
//--------------------------------------------------------------------------------------------
function ShowBills(){
//--------------------------------------------------------------------------------------------
	var lintBillType
	var lintInsur_area = '<%=Session("nInsur_area")%>'
    alert(optDocType.value);
    with (document.forms[0]){
//+ Se obtiene el tipo de factura a tratar
		if (optDocType[2].checked==true)
			lintBillType = 3;	//+ Nota de crédito   
		else
			if (optBillType[0].checked==true)
				lintBillType = 1;	//+ Factura afecta
			else
				if (optBillType[1].checked==true)
					lintBillType = 2;	//+ Factura exenta
				else
					if (optBillType[2].checked==true)
						lintBillType = 4;	//+ Proforma
						
            if (top.frames["fraSequence"].plngMainAction!=401) {
			    if (lintBillType=='3')
			    lintBillType=0;
			}    
			
			if ((tcnBill.value!=mlngBill)   ||
			   (lintBillType!=mintBillType)) 
			{
				mlngBill=tcnBill.value;
				mintBillType=lintBillType;
        		insDefValues("ShowDataCO700", "sField=FindBill" +  "&nBill=" + tcnBill.value + "&sBillType=" + lintBillType + "&nInsur_area=" + lintInsur_area +
        		                              "&nMainAction=" + top.frames["fraSequence"].plngMainAction);
        	}

	}
}
<%="</SCRIPT>"%>

	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CO700", "CO700_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Call insReaInitial()
Call insOldValues()

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO700" ACTION="valCollectionTra.aspx?sMode=2">
<%
If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadHeader()
Else
	Call LoadFolder()
End If
mobjMenu = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.47
Call mobjNetFrameWork.FinishPage("co700_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




