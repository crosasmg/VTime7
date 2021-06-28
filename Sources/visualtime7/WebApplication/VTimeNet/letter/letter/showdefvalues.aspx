<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim a As eFunctions.Values


'% insOpenDocument
'-------------------------------------
Private Function insOpenDocument() As Object
	'-------------------------------------
	Dim lobjLetter As eLetter.Letter
	
	lobjLetter = New eLetter.Letter
	With lobjLetter
		Response.Write("<SCRIPT>")
		
Response.Write("    var mblnExistDocument;" & vbCrLf)
Response.Write("      var mstrFileName;" & vbCrLf)
Response.Write("      var lobjFS;" & vbCrLf)
Response.Write("      var lobjFile;" & vbCrLf)
Response.Write("      lobjFS = new ActiveXObject(""Scripting.FileSystemObject"");" & vbCrLf)
Response.Write("      mstrFileName = lobjFS.GetSpecialFolder(2) + ""\\"" + lobjFS.GetTempName() + "".rtf"";" & vbCrLf)
Response.Write("      lobjFile = lobjFS.OpenTextFile(mstrFileName, 2, true);" & vbCrLf)
Response.Write("</" & "script>      " & vbCrLf)
Response.Write("<BR>")

		
		'If .Find("0" & Request.QueryString.Item("nLetterNum"), Today) Then
		If .Find("0", Request.QueryString.Item("nLetterNum"), Today) Then
			
			Response.Write(a.TextAreaControl("a", 200, 200, .tLetter))
			Response.Write("<SCRIPT>" & vbCrLf)
			
Response.Write("" & vbCrLf)
Response.Write("                mblnExistDocument=true;                " & vbCrLf)
Response.Write("                lobjFile.write(document.forms[0].a.value);                " & vbCrLf)
Response.Write("")

		Else
			Response.Write("<SCRIPT>")
			
Response.Write("" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""{\\rtf1\\ansi\\ansicpg1252\\uc1 \\deff0\\deflang3082\\deflangfe3082{\\fonttbl{\\f0\\froman\\fcharset0\\fprq2{\\*\\panose 02020603050405020304}Times New Roman;}{\\f30\\froman\\fcharset238\\fprq2 Times New Roman CE;}{\\f31\\froman\\fcharset204\\fprq2 Times New Roman Cyr;}"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""{\\f33\\froman\\fcharset161\\fprq2 Times New Roman Greek;}{\\f34\\froman\\fcharset162\\fprq2 Times New Roman Tur;}{\\f35\\froman\\fcharset177\\fprq2 Times New Roman (Hebrew);}{\\f36\\froman\\fcharset178\\fprq2 Times New Roman (Arabic);}"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""{\\f37\\froman\\fcharset186\\fprq2 Times New Roman Baltic;}}{\\colortbl;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\green255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;\\red255\\green255\\blue255;"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\green128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\\blue0;\\red128\\green128\\blue0;\\red128\\green128\\blue128;\\red192\\green192\\blue192;}{\\stylesheet{"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""\\ql \\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang3082\\langfe3082\\cgrid\\langnp3082\\langfenp3082 \\snext0 Normal;}{\\*\\cs10 \\additive Default Paragraph Font;}}{\\info"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""{\\creatim\\yr2001\\mo7\\dy9\\hr23\\min40}{\\revtim\\yr2001\\mo7\\dy9\\hr23\\min40}{\\version2}{\\edmins0}{\\nofpages1}{\\nofwords0}{\\nofchars0}{\\*\\company EASE}{\\nofcharsws0}{\\vern8249}}\\paperw11906\\paperh16838\\margl1701\\margr1701\\margt1417\\margb1417 "");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""\\deftab708\\widowctrl\\ftnbj\\aenddoc\\hyphhotz425\\noxlattoyen\\expshrtn\\noultrlspc\\dntblnsbdb\\nospaceforul\\hyphcaps0\\formshade\\horzdoc\\dgmargin\\dghspace180\\dgvspace180\\dghorigin1701\\dgvorigin1984\\dghshow1\\dgvshow1"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""\\jexpand\\viewkind4\\viewscale100\\pgbrdrhead\\pgbrdrfoot\\splytwnine\\ftnlytwnine\\htmautsp\\nolnhtadjtbl\\useltbaln\\alntblind\\lytcalctblwd\\lyttblrtgr\\lnbrkrule \\fet0\\sectd \\linex0\\headery708\\footery708\\colsx708\\endnhere\\sectlinegrid360\\sectdefaultcl "");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""{\\*\\pnseclvl1\\pnucrm\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl2\\pnucltr\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl3\\pndec\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl4\\pnlcltr\\pnstart1\\pnindent720\\pnhang{\\pntxta )}}{\\*\\pnseclvl5"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""\\pndec\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl6\\pnlcltr\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl7\\pnlcrm\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl8\\pnlcltr\\pnstart1\\pnindent720\\pnhang"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl9\\pnlcrm\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}\\pard\\plain \\ql \\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang3082\\langfe3082\\cgrid\\langnp3082\\langfenp3082 {"");" & vbCrLf)
Response.Write("            lobjFile.WriteLine(""\\par }}"");" & vbCrLf)
Response.Write("            mblnExistDocument = false;")

			
		End If
		
Response.Write("" & vbCrLf)
Response.Write("      lobjFile.close();" & vbCrLf)
Response.Write("      //opener.document.forms[0].tctFileName.value=mstrFileName;" & vbCrLf)
Response.Write("      " & vbCrLf)
Response.Write("</" & "script>      ")

		
	End With
	lobjLetter = Nothing
End Function

'%insGetVariables
'-----------------------------------
Private Function insGetVariables() As String
	'-----------------------------------    
	Dim lcolVariables As eLetter.GroupVariabless
	Dim lobjVariable As eLetter.GroupVariables
	lcolVariables = New eLetter.GroupVariabless
	lobjVariable = New eLetter.GroupVariables
	If lcolVariables.Find(-32768, -32768) Then
		For	Each lobjVariable In lcolVariables
			insGetVariables = insGetVariables & lobjVariable.sVariable & ","
		Next lobjVariable
		
	End If
	lobjVariable = Nothing
	lcolVariables = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("ShowDefValues")
a = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
a.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

a.sCodisplPage = "ShowDefValues"
%>
<HTML>
<HEAD>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
		
</HEAD>
<BODY>
<FORM>    
<%
Select Case Request.QueryString.Item("Field")
	Case "Document"
		insOpenDocument()
End Select

Response.Write("<SCRIPT>")
%>
    function insLoadWord(){
        var lobjWord; 
        lobjWord = new ActiveXObject("Word.Application");
	    lobjWord.visible = true;
        lobjWord.activate();
        lobjWord.Documents.open(mstrFileName);
    	lobjWord.ActiveDocument.MailMerge.MainDocumentType = 0;
	    lobjWord.ActiveDocument.MailMerge.CreateDataSource (lobjWord.ActiveDocument.Name,"","", "<%=Mid(insGetVariables(), 1, 255)%>",0,"" ,"" ,"" , true);
    }
    insLoadWord();
    window.close();
</script>
</FORM>    
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Call mobjNetFrameWork.FinishPage("ShowDefValues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







