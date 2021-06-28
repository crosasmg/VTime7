<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader
'----------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------        
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "VariablesII"
	
	With mobjGrid
		.Codispl = "LT001"
		.Codisp = "LT001_K"
		.Width = 500
		.Height = 250
		.AddButton = False
		.DeleteButton = False
	End With
	
	With mobjGrid.Columns
		.AddCheckColumn(101151, "", "chkSel", "'",  ,  , "insSelVar(this)")
		.AddPossiblesColumn(7287,"Group", "valGroup", "tabGroupParams", 1,  ,  ,  ,  ,  ,  ,  ,  ,vbNullString, eFunctions.Values.eTypeCode.eString)
		.AddTextColumn(7288,"Variable", "tctVar", 12, "",,vbNullString)
		.AddTextColumn(7289,"Descript", "tctDescript", 30, "",,vbNullString)
	End With
	mobjGrid.Columns("Sel").GridVisible = False
End Sub

'%insGetVariables
'-----------------------------------
Private Function insGetVariables() As Object
	'-----------------------------------    
	Dim lcolVariables As eLetter.GroupVariabless
	Dim lobjVariable As eLetter.GroupVariables
	
	lcolVariables = New eLetter.GroupVariabless
	lobjVariable = New eLetter.GroupVariables
	If lcolVariables.find(-32768, -32768) Then
		For	Each lobjVariable In lcolVariables
			With mobjGrid
				.Columns("chkSel").onClick = "insSelVar(this,""" & lobjVariable.sVariable & """)"
				.Columns("valGroup").DefValue = CStr(lobjVariable.nLett_group)
				.Columns("tctVar").DefValue = lobjVariable.sVariable
				.Columns("tctDescript").DefValue = lobjVariable.sDescript
				Response.Write(.DoRow)
			End With
		Next lobjVariable
	End If
	Response.Write(mobjGrid.closeTable)
	lobjVariable = Nothing
	lcolVariables = Nothing
End Function


'% insOpenDocument
'-------------------------------------
Private Function insOpenDocument() As Object
	'-------------------------------------
	Dim lobjLetter As eLetter.Letter
	
	lobjLetter = New eLetter.Letter
	With lobjLetter
		Response.Write("<SCRIPT>")
		
Response.Write("    var mstrFileName;" & vbCrLf)
Response.Write("      var mobjFS;" & vbCrLf)
Response.Write("      var mobjFile;" & vbCrLf)
Response.Write("      mobjFS = new ActiveXObject(""Scripting.FileSystemObject"");" & vbCrLf)
Response.Write("      mstrFileName = mobjFS.GetSpecialFolder(2) + ""\\"" + mobjFS.GetTempName() + "".rtf"";" & vbCrLf)
Response.Write("      mobjFile = mobjFS.OpenTextFile(mstrFileName, 2, true);" & vbCrLf)
Response.Write("</" & "script>      " & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("")

		
		If .Find("0", Request.QueryString.Item("nLetterNum"), Today) Then
Response.Write("" & vbCrLf)
Response.Write("                <DIV Id=hdiv visibility='hide' style=""display:hide"">" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("    ")

			'        Dim llngIndex as long 
			'                for llngIndex = 1 to len(.tletter) step 10000
			Response.Write(mobjValues.TextAreaControl("tctLetter", 2, 2, .tletter))
			'                next
			
Response.Write("" & vbCrLf)
Response.Write("                    " & vbCrLf)
Response.Write("                </DIV>")

			Response.Write("<SCRIPT>" & vbCrLf)
			
Response.Write("              ShowDiv('hdiv','hide');" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("                function insWriteDoc(){" & vbCrLf)
Response.Write("                var lintIndex;" & vbCrLf)
Response.Write("                for(lintIndex=0;lintIndex<=document.forms[0].tctLetter.value.length;lintIndex++)" & vbCrLf)
Response.Write("                {" & vbCrLf)
Response.Write("                mobjFile.write(document.forms[0].tctLetter.value.substring(lintIndex,1));" & vbCrLf)
Response.Write("                }")

		Else
			Response.Write("<SCRIPT>")
			
Response.Write("" & vbCrLf)
Response.Write("            function insWriteDoc(){" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""{\\rtf1\\ansi\\ansicpg1252\\uc1 \\deff0\\deflang3082\\deflangfe3082{\\fonttbl{\\f0\\froman\\fcharset0\\fprq2{\\*\\panose 02020603050405020304}Times New Roman;}{\\f30\\froman\\fcharset238\\fprq2 Times New Roman CE;}{\\f31\\froman\\fcharset204\\fprq2 Times New Roman Cyr;}"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""{\\f33\\froman\\fcharset161\\fprq2 Times New Roman Greek;}{\\f34\\froman\\fcharset162\\fprq2 Times New Roman Tur;}{\\f35\\froman\\fcharset177\\fprq2 Times New Roman (Hebrew);}{\\f36\\froman\\fcharset178\\fprq2 Times New Roman (Arabic);}"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""{\\f37\\froman\\fcharset186\\fprq2 Times New Roman Baltic;}}{\\colortbl;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\green255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;\\red255\\green255\\blue255;"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\green128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\\blue0;\\red128\\green128\\blue0;\\red128\\green128\\blue128;\\red192\\green192\\blue192;}{\\stylesheet{"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""\\ql \\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang3082\\langfe3082\\cgrid\\langnp3082\\langfenp3082 \\snext0 Normal;}{\\*\\cs10 \\additive Default Paragraph Font;}}{\\info"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""{\\creatim\\yr2001\\mo7\\dy9\\hr23\\min40}{\\revtim\\yr2001\\mo7\\dy9\\hr23\\min40}{\\version2}{\\edmins0}{\\nofpages1}{\\nofwords0}{\\nofchars0}{\\*\\company EASE}{\\nofcharsws0}{\\vern8249}}\\paperw11906\\paperh16838\\margl1701\\margr1701\\margt1417\\margb1417 "");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""\\deftab708\\widowctrl\\ftnbj\\aenddoc\\hyphhotz425\\noxlattoyen\\expshrtn\\noultrlspc\\dntblnsbdb\\nospaceforul\\hyphcaps0\\formshade\\horzdoc\\dgmargin\\dghspace180\\dgvspace180\\dghorigin1701\\dgvorigin1984\\dghshow1\\dgvshow1"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""\\jexpand\\viewkind4\\viewscale100\\pgbrdrhead\\pgbrdrfoot\\splytwnine\\ftnlytwnine\\htmautsp\\nolnhtadjtbl\\useltbaln\\alntblind\\lytcalctblwd\\lyttblrtgr\\lnbrkrule \\fet0\\sectd \\linex0\\headery708\\footery708\\colsx708\\endnhere\\sectlinegrid360\\sectdefaultcl "");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""{\\*\\pnseclvl1\\pnucrm\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl2\\pnucltr\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl3\\pndec\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl4\\pnlcltr\\pnstart1\\pnindent720\\pnhang{\\pntxta )}}{\\*\\pnseclvl5"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""\\pndec\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl6\\pnlcltr\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl7\\pnlcrm\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl8\\pnlcltr\\pnstart1\\pnindent720\\pnhang"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl9\\pnlcrm\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}\\pard\\plain \\ql \\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang3082\\langfe3082\\cgrid\\langnp3082\\langfenp3082 {"");" & vbCrLf)
Response.Write("            mobjFile.WriteLine(""\\par }}"");")

			
		End If
		
Response.Write("" & vbCrLf)
Response.Write("      mobjFile.close();" & vbCrLf)
Response.Write("      //opener.document.forms[0].tctFileName.value=mstrFileName;" & vbCrLf)
Response.Write("      }      " & vbCrLf)
Response.Write("</" & "script>      ")

	End With
	lobjLetter = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("VariablesII")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VariablesII"
%>
<HTML>
<HEAD>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
		
	<%="<SCRIPT>"%>
	    var mstrList = "";
        function insLoadWord(){
            var lobjWord; 
            lobjWord = new ActiveXObject("Word.Application");
	        lobjWord.visible = true;
            lobjWord.activate();
            lobjWord.Documents.open(mstrFileName);
    	    lobjWord.ActiveDocument.MailMerge.MainDocumentType = 0;
    	    if (mstrList!="")
	            lobjWord.ActiveDocument.MailMerge.CreateDataSource (lobjWord.ActiveDocument.Name,"","",mstrList,0,"" ,"" ,"" , true);

        }	        
	</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>    
<%insOpenDocument()
If Request.QueryString.Item("Type") = "upd" Then
	Response.Write(mobjValues.StyleSheet)%>
<SCRIPT>
//--------------------------------------------------
	    function insSelVar(oObject, sValue) {
//--------------------------------------------------
            if (oObject.checked)
                mstrList +=  sValue + ", " ;
            else
                mstrList = mstrList.replace(sValue+", ","");
	    }
//--------------------------------------------------
	    function insAccept() {
//--------------------------------------------------
        insWriteDoc(); 
        insLoadWord();         
        window.close();
	    }

//--------------------------------------------------
	    function insCancel() {
//--------------------------------------------------
            window.close();
	    }
</SCRIPT>        
       <CENTER> 
        <TABLE COLS="2">
    		<TR><td COLSPAN="2" ALIGN="right"><LABEL ID=15752>Variables available</LABEL></td></TR>
    		<TR><td COLSPAN="2"><hr></td></TR>
            <TR><td COLSPAN="2"></td></TR>
    		<TR><td ALIGN="middle" COLSPAN=2>
                <DIV ID="Scroll" style="OVERFLOW: auto; WIDTH: 400px; HEIGHT: 250px; BACKGROUND-COLOR: ivory" 
     >
                <%	insDefineHeader()
	insGetVariables()%>
                </DIV>
    		    </td>
    		</TR>
    		<TR>
    		    <TD><BR></TD>
    		</TR>
    	        <TR>
    	            <TD COLSPAN="2" ALIGN=right><%=mobjValues.ButtonAcceptCancel("insAccept()", "insCancel()", False)%></TD>
    	        </TR>		
    	</TABLE>
        </CENTER>     
<%Else%>
    <SCRIPT>
        insWriteDoc(); 
        insLoadWord();         
        window.close();        
    </SCRIPT> 
<%End If%>
</FORM>    
</BODY>
</HTML>
<%mobjGrid = Nothing
mobjValues = Nothing%>




<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Call mobjNetFrameWork.FinishPage("VariablesII")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







